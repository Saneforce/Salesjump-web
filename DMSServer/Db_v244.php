<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
//session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

 //$query = "insert into reqDetails select 'DBV244','".$_GET["axn"]."','".$_POST['data']."'"; 
 //performQuery($query);
function sendFCM_notify($registrationIds,$message,$title,$msgId,$event_id){
	// API access key from Google API's Console
	define('FIREBASE_API_KEY', 'AAAAFZqP7mw:APA91bG9q_tSrr3sjaHiMo8fqBI18z8z3KWxsmFeL3L_9AMuLnUoT3kRsmTH2DmvRHZrqSJn9nfVqQZGPmYvG4Z6skH304xWp2Wa7kz_jTVIsGI63t4PwZxtHjbeH-sqXMESogFb5QJ_');

	$msg = array
	(
		'text' 	=> $message,
		'title'		=> $title,
		'subtitle'	=> 'Request Alert!',
		'tickerText'	=> 'Request Alert!',
		'vibrate'	=> 1,
		'sound'		=> 1,
		'largeIcon'	=> 'large_icon',
		'smallIcon'	=> 'small_icon',
		'id'		=> $msgId,
		'event_id'	=> $event_id
	);

	$fields = array
	(
		'registration_ids' 	=> array($registrationIds),
		'data'			=> $msg
	);
	
	$headers = array
	(
		'Authorization: key=' . FIREBASE_API_KEY,
		'Content-Type: application/json'
	);
	 
	$ch = curl_init();
	curl_setopt( $ch,CURLOPT_URL, 'https://fcm.googleapis.com/fcm/send' );
	curl_setopt( $ch,CURLOPT_POST, true );
	curl_setopt( $ch,CURLOPT_HTTPHEADER, $headers );
	curl_setopt( $ch,CURLOPT_RETURNTRANSFER, true );
	curl_setopt( $ch,CURLOPT_SSL_VERIFYPEER, false );
	curl_setopt( $ch,CURLOPT_POSTFIELDS, json_encode( $fields ) );
	$result = curl_exec($ch );
	curl_close( $ch );	
}
function send_gcm_notify($reg_id, $message,$title) {
    define("GOOGLE_API_KEY", "AIzaSyAqfZ3MIMN418_WkoRMpAHz-399EfsW_XQ");
    define("GOOGLE_GCM_URL", "https://android.googleapis.com/gcm/send");

    $fields = array(
        'registration_ids' => array($reg_id),
        'data' => array("message" => $message, "title" => $title),
    );

    $headers = array(
        'Authorization: key=' . GOOGLE_API_KEY,
        'Content-Type: application/json'
    );

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, GOOGLE_GCM_URL);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
    curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($fields));

    $result = curl_exec($ch);
    if ($result === FALSE) {
        die('Problem occurred: ' . curl_error($ch));
    }
    curl_close($ch);
    //echo $result;
}

function actionLogin() {
    global $URL_BASE;
    $data = json_decode($_POST['data'], true);
    $username = (string) $data['name'];
    $password = (string) $data['password'];
    $DeviceRegId = (string) $data['DeviceRegId'];
    
    $query = "exec LoginAPP '$username','$password'";
    global $conn;
    $arr;
	
    $res = sqlsrv_query($conn, $query);
    if ($res) {
        $result = array();
        while ($row = sqlsrv_fetch_array($res, SQLSRV_FETCH_ASSOC)) {
            $result[] = $row;
        }
        $arr = $result;
    }
    //sqlsrv_close();
    $respon = array();
	
    //$respon['qry']=$query;
    $count = count($arr);
    if ($count == 1) {
        $respon['success'] = true;
        $respon['sfCode'] = $arr[0]['SF_Code'];
		$respon['SFFType']=$arr[0]['SFFType'];
		
        $sfName = utf8_encode(trim(preg_replace("/[\r\n]+/", " ", $arr[0]['SF_Name'])));
        $respon['sfName'] = $sfName;
		
		$respon['sf_emp_id'] = $arr[0]['sf_emp_id'];
		$respon['Geo_Fencing'] = $arr[0]['Geo_Fencing'];
		
        $respon['divisionCode'] = $arr[0]['Division_Code'];
        $respon['call_report'] = $arr[0]['call_report'];
        $respon['desigCode'] = $arr[0]['desig_Code'];
        $respon['HlfNeed'] = $arr[0]['MGRHlfDy'];
        if ($DeviceRegId!=null&&!empty($DeviceRegId)) {
            $sql = "update Access_Table set  DeviceRegId='$DeviceRegId' where sf_code='" . $arr[0]['SF_Code'] . "'";
            performQuery($sql);
        }
        if ($arr[0]['desig_Code'] == "MR") {
            $query = "Select count(SF_Code) Cnt from Salesforce_Master where sf_Tp_Reporting='" . $arr[0]['SF_Code'] . "'";
            $dsgc = performQuery($query);
            if ($dsgc[0]['Cnt'] > 0)
                $respon['desigCode'] = "AM";
            else
                $respon['HlfNeed'] = $arr[0]['MRHlfDy'];
        }
        $query = "select * from dcrmain_trans where sf_code='" . $arr[0]['SF_Code'] . "' and Activity_Date=cast(GETDATE() as date)";
        $dcrtoday = performQuery($query);
        $respon['dcrtoday']=count($dcrtoday);
        $respon['edit_sumry'] = $arr[0]['edit_sumry'];
        $respon['prod'] = $arr[0]['prod'];
        $respon['JWNeed'] = $arr[0]['JWNeed'];
        
        $dat=date('Y-m-d');
        $sql="select * from TP_Attendance_App where Sf_Code='" . $arr[0]['SF_Code'] . "' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$dat'";
        $attendance=performQuery($sql);
        if(count($attendance)==0)
            $respon['attendanceView'] = 0;
        else
            $respon['attendanceView'] = 1;

        $respon['NetweightVal'] = $arr[0]['NetweightVal'];
        $respon['OrderVal'] = $arr[0]['OrderVal'];
        $respon['VisitDist'] = $arr[0]['VisitDist'];
        $respon['template'] = $arr[0]['template'];
        $respon['AppTyp'] = 1;
        $respon['TBase'] = $arr[0]['TBase'];
        $respon['GeoChk'] = $arr[0]['GeoNeed'];
        $respon['ChmNeed'] = $arr[0]['ChmNeed'];
        $respon['StkNeed'] = $arr[0]['StkNeed'];
        $respon['CollectedAmount'] = $arr[0]['CollectedAmount'];
        $respon['jointwork'] = $arr[0]['jointwork'];
        $respon['UNLNeed'] = $arr[0]['UNLNeed'];
        $respon['DPNeed'] = $arr[0]['DPNeed'];
        $respon['DINeed'] = $arr[0]['DINeed'];
        $respon['CPNeed'] = $arr[0]['CPNeed'];
        $respon['CINeed'] = $arr[0]['CINeed'];
        $respon['SPNeed'] = $arr[0]['SPNeed'];
        $respon['SINeed'] = $arr[0]['SINeed'];
        $respon['NPNeed'] = $arr[0]['NPNeed'];
        $respon['NINeed'] = $arr[0]['NINeed'];
        $respon['DrCap'] = $arr[0]['DrCap'];
        $respon['ChmCap'] = $arr[0]['ChmCap'];
        $respon['StkCap'] = $arr[0]['StkCap'];
        $respon['NLCap'] = $arr[0]['NLCap'];
        $respon['DrSmpQ'] = $arr[0]['DrSmpQ'];
        $respon['DRxCap'] = $arr[0]['DrRxQCap'];
        $respon['DSmpCap'] = $arr[0]['DrSmpQCap'];
        $respon['CQCap'] = $arr[0]['ChmQCap'];
        $respon['SQCap'] = $arr[0]['StkQCap'];
        $respon['NRxCap'] = $arr[0]['NLRxQCap'];
        $respon['NSmpCap'] = $arr[0]['NLSmpQCap'];
        $respon['SFStat'] = $arr[0]['SFStat'];
        $respon['days'] = $arr[0]['days'];
        $respon['State_Code'] = $arr[0]['State_Code'];
        $respon['CusOrder'] = $arr[0]['CusOrder'];
        $respon['SF_type'] = $arr[0]['SF_type'];
        $respon['SFTPDate'] = $arr[0]['sf_TP_Active_Dt'];
        $respon['closing'] = $arr[0]['closing'];
        $respon['recv'] = $arr[0]['recv'];
        $respon['DisRad'] = $arr[0]['DisRad'];
		$respon['Permission_hours'] = $arr[0]['Permission_hours'];
		$respon['Permission_Split'] = $arr[0]['Permission_Split'];
		if($respon['sfCode']=="MGR0016"){
			$respon['HOLocation'] = "13.01806351:80.25219262";
		}else {
		    $respon['HOLocation'] = "12.91729615:80.23030043";
		}
		$respon['Selfie'] = $arr[0]['Selfie'];
        return outputJSON($respon);
    } else {
        $respon['success'] = false;
        $respon['msg'] = "Check User and Password";
        return outputJSON($respon);
    }
}

function getProducts($desig) {
    $sfCode = $_GET['sfCode'];
    $DivisionCode = $_GET['divisionCode'];
    if ($desig == "superstock" || $desig == "stockist" ){
	$div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
if($desig == "superstock")
   $query="select Sub_Division_Code from Supplier_Master  where SL_MaxNo='" . $sfCode . "'";
else
$query="select subdivision_code from mas_stockist where Stockist_Code='" . $sfCode . "' ";

   $subdivision=performQuery($query);
   $query = "exec getAppProdStockist    '".$Owndiv."','".$subdivision[0]['subdivision_code']."'   "; 


		}
    else{
        $query = "exec getAppProd '" . $sfCode . "'"; //,'".$DivisionCode."'";
     
	}
 return performQuery($query);
}
function getAPPSetups() {
    $rqSF = $_GET['rSF'];
    $userType = $_GET['userType'];
    $query = "exec getAPPSetups '" . $rqSF . "'";
    return performQuery($query);
}
function dateDiffInDays($date1, $date2) 
{ 
	// Calculating the difference in timestamps 
	$diff = strtotime($date2) - strtotime($date1); 
	
	// 1 day = 24 hours 
	// 24 * 60 * 60 = 86400 seconds 
	return abs(round($diff / 86400)); 
} 

function getTrackSetUP(){
$data = json_decode($_POST['data'], true);
$sfCode = (string) $data['SF'];
$div = (string) $data['div'];
$query = "exec getTrackSetup '".$sfCode."','".$div."'";
return performQuery($query);
}
function getSchemeDets() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    
    $query = "exec getProdScheme '" . $Owndiv . "'";
    return performQuery($query);
}
function getBrandLittersWiseProd($desig){
    $date = $_GET['rptDt'];
    $sfCode = $_GET['sfCode'];
 $rptSF = $_GET['rptSF'];
    if ($desig == 'stockist')
        $stockistCode = $sfCode;
    else
        $stockistCode = $_GET['stockistCode'];
    if ($desig == 'mr')
        $str = "sf_code='$sfCode'";
    else
        $str = "Stockist_Code='$stockistCode'";
if($_GET['stockistCode']==-1)
 $str = "sf_code='$sfCode'";

if($desig == 'mgr' && $_GET['stockistCode']==-1)
$str= "sf_code='$rptSF'";

    $query = "select BrandName,sum(quantity) OQty,sum(ltrs) OVal from vwBrandWiseOrder where orderDate='$date' and $str group by BrandName";
    $daywise = performQuery($query);

    $query = "select BrandName,sum(quantity) TOQty,sum(ltrs) TOVal from vwBrandWiseOrder where MONTH(orderDate) = MONTH('$date') and YEAR(orderDate) = YEAR('$date') and $str group by BrandName";
    $monthwise = performQuery($query);
    $det = array();
    for ($i = 0; $i < count($monthwise); $i++) {
        $OQty = 0;
        $OVal = 0;
        for ($j = 0; $j < count($daywise); $j++) {
            if ($monthwise[$i]['BrandName'] == $daywise[$j]['BrandName']) {
                $OQty = $daywise[$j]['OQty'];
                $OVal = $daywise[$j]['OVal'];
                array_splice($daywise, $j, 1);
            }
        }
        $det[$i]['Brand'] = $monthwise[$i]['BrandName'];
        $det[$i]['OQty'] = $OQty;
        $det[$i]['OVal'] = $OVal;
        $det[$i]['TOQty'] = $monthwise[$i]['TOQty'];
        $det[$i]['TOVal'] = $monthwise[$i]['TOVal'];
    }
    return $det;
}
function getAttndTDyDets(){
	$RSF = $_GET['sfCode'];
    $query = "exec [GetMyDyAttndDets] '" . $RSF . "'";
    return performQuery($query);
}
function getAttndStatus(){
	$RSF = $_GET['sfCode'];
	$Status = $_GET['Status'];
	$Priod = $_GET['Priod'];
    $querystatus = "exec [GetAttnDetsByStatus] '" . $RSF . "','".$Status."','".$Priod."'";
	
	$result['AttenStatus']=performQuery($querystatus);
	$result['GateEntry']=[];
	
	if($Status=="Leave" && $RSF =="MGR0013" ){
		
		$mod_date=date('Y-m-23');
		if($Priod==-1) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
		if(date('d')<23) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
		$fDt=date('Y-m-d',strtotime($mod_date));
		$mod_date = date('Y-m-d',strtotime($mod_date."+ 1 months"));
		$tDDt=date('Y-m-d',strtotime($mod_date));
		$mod_date = strtotime($mod_date."- 1 day");
		$tDt=date('Y-m-d',$mod_date);
		$query = "exec [ViewLeavestatus] '" . $RSF . "','".$fDt."','".$tDt."'";
		$result['AttenStatus']=performQuery($query);
		 
	}
	
	
	if($Status=="On-Duty" || $Status=="" ){
		
		
		$mod_date=date('Y-m-23');
		
		if(date('d')<23) $mod_date = date('Y-m-d',strtotime($mod_date."- 2 months"));
		$fromDt=date('Y-m-d',strtotime($mod_date));
		
		$mod_datee=date('Y-m-23');
        if(date('d')<23) $mod_datee = date('Y-m-d',strtotime($mod_datee."- 1 months"));
        
        $mod_datee = date('Y-m-d',strtotime($mod_datee."+ 1 months"));
		$tDDt=date('Y-m-d',strtotime($mod_datee));
		$mod_datee = strtotime($mod_datee."- 1 day");
		$toDt=date('Y-m-d',$mod_datee);
	$query = "select convert(varchar(10),convert(date,IEntryDate), 105) date,cast (convert(time,IServerDate) as varchar) Intime, cast (convert(time,OServerDate) as varchar)  OutTime,IHQLoc,IlatLng,OHQLoc,OlatLng from QRGateEntry where  sf_code='" . $RSF . "'   and  IEntryDate between  '".$fromDt."' and '".$toDt."' order by IEntryDate desc ";
	$result['GateEntry']=performQuery($query);
	}
	
    return $result;
}

function getMyTmCntDet(){
	$RSF = $_GET['sfCode'];
	//$RSF="MGR0017";
    $query = "exec [getHyrCntDet] '" . $RSF . "'";
    return performQuery($query);
}
function getMyTmStatus(){
	$RSF = $_GET['sfCode'];
	//$RSF="MGR0017";
    $t = strtotime(str_replace("Z", "", str_replace("T", " ", $_GET['RptDt'])));
    $TpDt = date('Y-m-d 00:00:00', $t);
    $query = "exec [getMyTeamSummary] '" . $RSF . "','".$TpDt."'";
    return performQuery($query);
}
function getMyTmStatusDet(){
	$RSF = $_GET['sfCode'];
	//$RSF="MGR0017";
	$mod = $_GET['mod'];
	$Typ = $_GET['typ'];
    $t = strtotime(str_replace("Z", "", str_replace("T", " ", $_GET['RptDt'])));
    $TpDt = date('Y-m-d 00:00:00', $t);
    $query = "exec [getMyTeamSummaryDet] '" . $RSF . "','".$TpDt."','".$Typ."','".$mod."'";
    return performQuery($query);
}
function mWkDysCnt($fDt,$tDt){	
	$no = 0;$tDys=0;
	$ett=date('Y-m-d');
	$start = new DateTime($fDt);
	$end   = new DateTime($tDt);
	$interval = DateInterval::createFromDateString('1 day');
	$period = new DatePeriod($start, $interval, $end);
	foreach ($period as $dt)
	{	
		$dtt=$dt->format('Y-m-d');
		//if(strtotime($dtt)<=strtotime($ett)){
			$tDys++;
			if ($dt->format('N') == 7)
			{
				$no++;
			}
		//}
	}
	return array('total'=>$tDys, 'wkoff'=>$no);
}
function WkDysCnt($fDt,$tDt){	
	$no = 0;$tDys=0;
	$ett=date('Y-m-d');
	$start = new DateTime($fDt);
	$end   = new DateTime($tDt);
	$interval = DateInterval::createFromDateString('1 day');
	$period = new DatePeriod($start, $interval, $end);
	foreach ($period as $dt)
	{	
		$dtt=$dt->format('Y-m-d');
		if(strtotime($dtt)<=strtotime($ett)){
			$tDys++;
			if ($dt->format('N') == 7)
			{
				$no++;
			}
		}
	}
	return array('total'=>$tDys, 'wkoff'=>$no);
}
function FilterArryNT($array,$key,$val){
    $fRes = array_filter($array, function ($item) use ($key,$val) {
		if ($item[$key]!=$val) {
		//if (stripos($item[$key], "0") !== false) {	
			return true;
		}
		return false;
	});
	return $fRes;
}
function FilterArry($array,$key,$val){
    $fRes = array_values(array_filter($array, function ($item) use ($key,$val) {
		if ($item[$key]==$val) {
		//if (stripos($item[$key], "0") !== false) {	
			return true;
		}
		return false;
	}));
	return $fRes;
}
function getAttndCnts(){
	$RSF = $_GET['rSF'];
	$md = $_GET['Priod'];
	//if($RSF=='MGR0016' || $RSF=='MGR0013' || $RSF=='MGR0020') {//||$RSF=='MGR5146'||$RSF=='MGR0017'||$RSF=='MGR0018'||$RSF=='MR0032'){
		$result=[];
		$mod_date=date('Y-m-23');
		if($md==-1) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
		if(date('d')<23) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
		$fDt=date('Y-m-d',strtotime($mod_date));
		$mod_date = date('Y-m-d',strtotime($mod_date."+ 1 months"));
		$tDDt=date('Y-m-d',strtotime($mod_date));
		$mod_date = strtotime($mod_date."- 1 day");
		$tDt=date('Y-m-d',$mod_date);
		//$result[]=array('SF_Code' => $RSF, 'Status' => 'from :'.$fDt.' To :'.$tDt,'StatusCnt' => 0, 'StusClr' => '#089c08 !important');
		
		/*$fDt=date('Y').'-'.(date('m')-1).'-23';
		$tDt=date('Y').'-'.date('m').'-22';
		$tDDt=date('Y').'-'.date('m').'-23';
		if($md==-1){	
			$fDt=date('Y').'-'.(date('m')-2).'-23';
			$tDt=date('Y').'-'.(date('m')-1).'-22';
			$tDDt=date('Y').'-'.(date('m')-1).'-23';
		}
		*/
		$DyDet=WkDysCnt($fDt,$tDDt);
		$query = "exec [getHQMnAttnDetails] '" . $RSF . "','".$fDt."','".$tDt."'";// MGR1510 MGR0017
		$res=performQuery($query);
		$query = "exec [getLvAndLOPDets] '" . $RSF . "','".$fDt."','".$tDt."'";// MGR1510 MGR0017
		$rlv=performQuery($query);
		$query = "exec [getDeviation_Entry_Status] '" . $RSF . "','".$fDt."','".$tDt."'";// MGR1510 MGR0017
		$rdevi=performQuery($query);
		
		$query = "select count(Week_date) wkCnt from Trans_Weekoff where sf_code='" . $RSF . "' and cast(Week_date as dateTime)>='".$fDt."' and cast(Week_date as dateTime)<='".$tDt."'";// MGR1510 MGR0017
		$rWk=performQuery($query);
		$wkCnt=$rWk[0]["wkCnt"];
		
		$Devi_Count=0;
		
		if ($rdevi[0]["DeviCount"]!=null){
			$Devi_Count=$rdevi[0]["DeviCount"];
		}
		
		
		$aOnTm=FilterArry($res,"Stat",'8');$cHfDy=count($aOnTm)*0.5;
		$aOnTm=FilterArry($res,"OutSt",'2');$cMiss=count($aOnTm);
		$aOnTm=FilterArry($res,"OutSt",'1');$cErly=count($aOnTm);
		$aOnTm=FilterArry($res,"Stat",'10');$cONDy=count($aOnTm);
		$aOnTm=FilterArry($res,"Stat",'0');$TotPDys=count($aOnTm)+$cONDy+$cHfDy;
		$lvDys=0;$lopDys=0;$cONTm=0;$hlyDys=0;
		$aOnTm=FilterArry($rlv,"DedTyp",'0');if(count($aOnTm)>0){$lvDys=$aOnTm[0]["lv"];};
		$aOnTm=FilterArry($rlv,"DedTyp",'1');if(count($aOnTm)>0){$lopDys=$aOnTm[0]["lv"];};
		$aOnTm=FilterArry($rlv,"DedTyp",'2');if(count($aOnTm)>0){//$lvDys=$lvDys+$aOnTm[0]["lv"];
		$hlyDys=$aOnTm[0]["lv"];};
		//$aOnTm=FilterArry($rdevi,"DedTyp",'3');if(count($aOnTm)>0){$Dv_Count=$aOnTm[0]["DeviCount"];};
		///$result[]=array('SF_Code' => $RSF, 'Status' => 'Total Days','StatusCnt' => $DyDet['total']-$wkCnt, 'StusClr' => '#089c08 !important');
		//$result[]=array('SF_Code' => $RSF, 'Status' => 'Present Days','StatusCnt' => $TotPDys, 'StusClr' => '#089c08 !important');
		
		$RRes=FilterArryNT($res,"Stat",10);
		$aOnTm=FilterArry($RRes,"OnTm",11);
		if(count($aOnTm)>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'On-Time','StatusCnt' => count($aOnTm), 'StusClr' => '#089c08 !important');}
		if($wkCnt>0)
		$result[]=array('SF_Code' => $RSF, 'Status' => 'Weekly off ('.$DyDet['wkoff'].' days)','StatusCnt' => $wkCnt, 'StusClr' => 'rgb(255, 146, 11) !important');
		
		if($cONDy>0){
			$result[]=array('SF_Code' => $RSF, 'Status' => 'On-Duty','StatusCnt' => $cONDy,'StusClr' => 'rgb(255, 146, 11) !important');
		}
		$aOnTm=FilterArry($RRes,"AInSt",1);
		if(count($aOnTm)>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'Grace time','StatusCnt' => count($aOnTm), 'StusClr' => 'rgb(255, 146, 11) !important');}
		$phrs=0;foreach($RRes as $a){$phrs=$phrs+$a['Perm'];}
		if($phrs>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'Permission (2 Hrs)', 'StatusCnt' => $phrs, 'StusClr' => 'rgb(255, 146, 11) !important');}
		$aOnTm=FilterArry($RRes,"AInSt",2);
		if(count($aOnTm)>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'Late', 'StatusCnt' => count($aOnTm), 'StusClr' => '#ff0000 !important');}// ( '.(count($aOnTm)*0.5).' Days )
		if($cMiss>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'Missed Punch', 'StatusCnt' => $cMiss, 'StusClr' => '#ff0000 !important');}
		if($cErly>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'Early Going', 'StatusCnt' => $cErly, 'StusClr' => '#ff0000 !important');}	
		if($lvDys>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'Leave', 'StatusCnt' => str_replace(".0", " ",$lvDys), 'StusClr' => 'rgb(255, 146, 11) !important');}
		if($hlyDys>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'Holiday', 'StatusCnt' => str_replace(".0", " ", $hlyDys), 'StusClr' => 'rgb(255, 146, 11) !important');}
		if($lopDys>0){$result[]=array('SF_Code' => $RSF, 'Status' => 'LOP Leave','StatusCnt' => str_replace(".0", " ",$lopDys),'StusClr' => 'rgb(255, 146, 11) !important');}
		
		if($Devi_Count>0)
		$result[]=array('SF_Code' => $RSF, 'Status' => 'Deviation Entry','StatusCnt' => $Devi_Count, 'StusClr' => 'rgb(255, 146, 11) !important');
		
		//if(($DyDet['total']-($wkCnt+$TotPDys+$lvDys))>0){
		///    $result[]=array('SF_Code' => $RSF, 'Status' => 'Loss Of Pay Days','StatusCnt' => ($DyDet['total']-($wkCnt+$TotPDys+$lvDys+$hlyDys)), 'StusClr' => '#ff0000 !important');
		//}
		
		//$result[]=array('SF_Code' => $RSF, 'Status' => 'Total Payable Days','StatusCnt' => ($DyDet['wkoff']+$TotPDys+$lvDys), 'StusClr' => '#089c08 !important');
		
		return $result;
	/*}
    $query = "exec GetMyAttndDets '" . $RSF . "','',''";
    return performQuery($query);*/
}
function getBrandWiseProd($desig) {
    $date = $_GET['rptDt'];
    $sfCode = $_GET['sfCode'];
    if ($desig == 'stockist')
        $stockistCode = $sfCode;
    else
        $stockistCode = $_GET['stockistCode'];
    if ($desig == 'mr')
        $str = "sf_code='$sfCode'";
    else
        $str = "Stockist_Code='$stockistCode'";
if($_GET['stockistCode']==-1)
 $str = "sf_code='$sfCode'";

if($desig == 'mgr' && $_GET['stockistCode']==-1)
$str= "sf_code='$rptSF'";

if($desig == 'mgr' && $_GET['stockistCode']==-1){


    $query = "select BrandName,quantity OQty,orderValue OVal from vwBrandWiseOrder where orderDate='$date' and sf_code='$sfCode'";

    $queryy = "select BrandName,sum(quantity) TOQty,sum(orderValue) TOVal from vwBrandWiseOrder where MONTH(orderDate) = MONTH('$date') and YEAR(orderDate) = YEAR('$date') and sf_code='$sfCode' group by BrandName";
   
}
else{

 $query = "select BrandName,quantity OQty,orderValue OVal from vwBrandWiseOrder where orderDate='$date' and sf_code='$sfCode' and Stockist_Code='$stockistCode'";

 $queryy = "select BrandName,sum(quantity) TOQty,sum(orderValue) TOVal from vwBrandWiseOrder where MONTH(orderDate) = MONTH('$date') and YEAR(orderDate) = YEAR('$date') and sf_code='$sfCode' and Stockist_Code='$stockistCode' group by BrandName";
}


    $daywise = performQuery($query);

    
 $monthwise = performQuery($queryy);


    $det = array();
    for ($i = 0; $i < count($monthwise); $i++) {
        $OQty = 0;
        $OVal = 0;
        for ($j = 0; $j < count($daywise); $j++) {
            if ($monthwise[$i]['BrandName'] == $daywise[$j]['BrandName']) {
                $OQty = $daywise[$j]['OQty'];
                $OVal = $daywise[$j]['OVal'];
                array_splice($daywise, $j, 1);
            }
        }
        $det[$i]['Brand'] = $monthwise[$i]['BrandName'];
        $det[$i]['OQty'] = $OQty;
        $det[$i]['OVal'] = $OVal;
        $det[$i]['TOQty'] = $monthwise[$i]['TOQty'];
        $det[$i]['TOVal'] = $monthwise[$i]['TOVal'];
    }
    return $det;
}

function getSubordinateMgr() {
    $sfCode = $_GET['rSF'];
    $param = array($sfCode);
    $query = "exec getHyrSF_APP '" . $sfCode . "'";
    return performQuery($query);
}
function getSubordinateStk() {
    $sfCode = $_GET['rSF'];
    $param = array($sfCode);
    $query = "exec getStockistSF_APP '" . $sfCode . "'";
    return performQuery($query);
}
function getSubordinate() {
    $sfCode = $_GET['rSF'];
    $param = array($sfCode);
    $query = "exec getBaseLvlSFs_APP '" . $sfCode . "'";
    return performQuery($query);
}

function getJointWork() {
    $sfCode = $_GET['sfCode'];
    $rqSF = $_GET['rSF'];
    $query = "exec getJointWork_App '" . $sfCode . "','" . $rqSF . "'";
    return performQuery($query);
}

function GetDailyInv() {
    $sfCode = $_GET['rptSF'];
    $Md = $_GET['Mod'];
    if($Md==1 || $Md==3  ){
        if($Md==3) $Md=0;
	
        $query = "exec getDailyBeginInv '" . $sfCode . "','".$Md."'";
    }
    else
        $query = "exec getDailyEndInv '" . $sfCode . "'";
    return performQuery($query);
}

function getDtTPview() {
    $data = json_decode($_POST['data'], true);
    $sfCode = (string) $data['sfCode'];
    $t = strtotime(str_replace("Z", "", str_replace("T", " ", $data['tpDate'])));
    $TpDt = date('Y-m-d 00:00:00', $t);
    $Qry = "exec spTPViewDtws '$sfCode','$TpDt'";
    $respon = performQuery($Qry);
    return outputJSON($respon);
}

function getTPview() {
    $data = json_decode($_POST['data'], true);
    $sfCode = (string) $data['sfCode'];
    $t = strtotime(str_replace("Z", "", str_replace("T", " ", $data['mnthYr'])));
    $TpDt = date('Y-m-d 00:00:00', $t);
    $Qry = "SELECT convert(varchar,Tour_Date,103) [date],Worktype_Name_B wtype,replace(isnull(Territory_Code1,''),'0','') towns,replace(isnull(Tour_Schedule1,''),'0','') townsId,replace(isnull(Tour_Schedule1,''),'0','') PlnNo,replace(isnull(Worked_With_SF_Name,''),'0','') distributor,Confirmed,SF_Code sf_code from vwTrans_TP_View T where sf_code='$sfCode' and Tour_Month=month('$TpDt') and Tour_year=year('$TpDt') order by Tour_Date";
    $respon = performQuery($Qry);
    return outputJSON($respon);
}

function getMonthSummary() {
    $sfCode = $_GET['rptSF'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getMonthSummaryApp '" . $sfCode . "','" . $dyDt . "'";
    return performQuery($query);
}
function getDaycallSummary() {
    $sfCode = $_GET['HQ'];
    $dyDt = $_GET['rptDt'];
    
   $div = $_GET['divisionCode'];
   $divisionCode = explode(",", $div);
    $date = date('Y-m-d');
    $query = "exec GET_Date_Call_Rep '" . $divisionCode[0] . "','" . $sfCode . "' ,'".$date."'";
    return  performQuery($query);
}
function getDayPlan(){
    $sfCode = $_GET['rptSF'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getDayPlan '" . $sfCode . "','" . $dyDt . "'";
    return performQuery($query);
}


function getDayPlanRoute(){
    $sfCode = $_GET['rptSF'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getDayPlanRoute '" . $sfCode . "','" . $dyDt . "'";


$result['ToRoute']=performQuery($query);


    return  $result;
}


function getDailySalesSummry() {
    $sfCode = $_GET['sfCode'];
    $dyDt = $_GET['rptDt'];

    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

    $query = "exec DaySaleSummary  '" . $sfCode . "',".$Owndiv.",'"  .  date('m', strtotime($dyDt )) . "','"  .  date('Y', strtotime($dyDt )) . "'";
    return performQuery($query);
}

function getDayReport() {
    $sfCode = $_GET['sfCode'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getDayReportApp '" . $sfCode . "','" . $dyDt . "'";
    return performQuery($query);
}

function DayReport() {
    $sfCode = $_GET['sfCode'];
    $dyDt = $_GET['rptDt'];
 $today = date('Y-m-d 00:00:00');
    $query = "exec getDayReportApp '" . $sfCode . "','" . $dyDt . "'";
    $dayrep= performQuery($query);
    $query = "exec getDayBrandWiseReportApp '" . $sfCode . "','" . $dyDt . "'";
    $brndwise= performQuery($query);

    $query = "exec getDayDCR_TLSD_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_TLSD= performQuery($query);
    $query = "exec getDayDCR_LPC_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_LPC= performQuery($query);

    $query = "exec getDaySummaryReportApp '" . $sfCode . "','" . $dyDt . "'";
    $summary= performQuery($query);
    $query = "Select *  from Trans_Inshop_Activity where sfCode='" . $sfCode . "' and InsertDate='" . $dyDt . "' ";
    $InshopCount= performQuery($query);
    $query = "Select * from Trans_Product_Display where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $ProductCount= performQuery($query);
    
    $query = "Select * from Trans_Door_To_Door where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $Door_To_Door= performQuery($query);
    $query = "select Count(Trans_Detail_Info_Code) SuperStokit from vwActivity_SuperCSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=8";
    $temp = performQuery($query);
    $result=array();


    $result['dayrep']=$dayrep;
    $result['brndwise']=$brndwise;
    $result['DCR_TLSD']=$DCR_TLSD;
    $result['DCR_LPC']=$DCR_LPC;
    $result['summary']=$summary;
    $result['Inshop']=$InshopCount;
    $result['ProductDiisplay']=$ProductCount;
    $result['DoorToDoor']=$Door_To_Door;
     $result['SS']=$temp[0]['SuperStokit'];
    return $result;
}


function  ProductRemarksRpt(){
    $sfCode = $_GET['SF_Code_Type'];
    $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    
    $query = "exec SP_ProductRemarksView '" . $sfCode . "','" . $Owndiv . "','".$fromdate."'";


    $result['RemarksReport']=performQuery($query);
    return $result;


}

function  ProductRCPARpt(){

    $sfCode = $_GET['SF_Code_Type'];
    $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    
    $query = "exec SP_ProductRCPAView '" . $sfCode . "','" . $Owndiv . "','".$fromdate."'";


    $result['RemarksReport']=performQuery($query);
    return $result;


}


function  ProductRemarksView(){

    $sfCode = $_GET['sfCode'];
    $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Pcode = $_GET['Pcode'];
	$Remarks_Id = $_GET['Remark_Id'];
    $fromdate = $_GET['fromdate'];
    
    $query = "exec SP_View_ProductRemarksView '" . $sfCode . "','" . $Owndiv . "','".$fromdate."' ,'" . $Pcode . "','".$Remarks_Id."'  ";

    $result['GetRemarksView']=performQuery($query);
    return $result;


}

function  ProductRCPAView(){

    $sfCode = $_GET['sfCode'];
    $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Pcode = $_GET['Pcode'];
$Remarks_Id = $_GET['Remark_Id'];
    $fromdate = $_GET['fromdate'];
    
    $query = "exec SP_View_ProductRCPAView '" . $sfCode . "','" . $Owndiv . "','".$fromdate."' ,'" . $Pcode . "','".$Remarks_Id."'  ";

    $result['GetRCPAView']=performQuery($query);
    return $result;


}



function  PrimarySalesFpWise(){

$sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
    $Mr_SFCode = $_GET['Mr_SFCode'];
    $Stockist_Code=$_GET['Stockist_Code'];

if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}
if($Type==3 ){
$fromdate = $_GET['fromdate'];
}

    $query = "exec getPrimarySalesFPWise '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";
//

$result['primaryfpwise']=performQuery($query);
return $result;


}
function  PrimarySalesStateWise(){

   $sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
    $Mr_SFCode = $_GET['Mr_SFCode'];
    $Stockist_Code=$_GET['Stockist_Code'];
if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}
if($Type==3 ){
$fromdate = $_GET['fromdate'];
}
    $query = "exec getPrimaryStateWise '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'";
    $result['primarystatewise']=performQuery($query);
	
	


    return $result;
}

function  PrimarytargetDistiWise(){
    $sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
    $Mr_SFCode = $_GET['Mr_SFCode'];
    $Stockist_Code=$_GET['Stockist_Code'];

if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}
/* if($Type==3 ){
$fromdate=date('Y-m',  strtotime($fromdate));
$todate=date('Y-m',  strtotime($todate));
} */

    $query = "exec getPrimaryDistiWise '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";
   
 $result['distributortarget']=performQuery($query);
    return $result;
    
}






function  OrderAnalysisView(){
    $sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
    $Mr_SFCode = $_GET['Mr_SFCode'];
    $Stockist_Code=$_GET['Stockist_Code'];

if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}
/* if($Type==3 ){
$fromdate=date('Y-m',  strtotime($fromdate));
$todate=date('Y-m',  strtotime($todate));
} */

    $query = "exec getOrderAnalysisView '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";
   
 $result['OrderAnalysis']=performQuery($query);
    return $result;
    
}

function  AttendanceAnalysisView(){
    $sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
    $Mr_SFCode = $_GET['Mr_SFCode'];
    $Stockist_Code=$_GET['Stockist_Code'];

if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}
/* if($Type==3 ){
$fromdate=date('Y-m',  strtotime($fromdate));
$todate=date('Y-m',  strtotime($todate));
} */

    $query = "exec getAttendanceAnalysisView '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";
   
 $result['OrderVisitAnalysis']=performQuery($query);
    return $result;
    
}



function  VisitAnalysisView(){
    $sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
    $Mr_SFCode = $_GET['Mr_SFCode'];
    $Stockist_Code=$_GET['Stockist_Code'];

if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}
/* if($Type==3 ){
$fromdate=date('Y-m',  strtotime($fromdate));
$todate=date('Y-m',  strtotime($todate));
} */

    $query = "exec getVisitAnalysisView '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";
   
 $result['OrderVisitAnalysis']=performQuery($query);
    return $result;
    
}




function  getOrderFFView(){
	
    $sfCode = $_GET['FSf_Code'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
   

if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}


    $query = "exec getOrderFFView '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";
   
   
 
    return performQuery($query);
    
}



function  getVisitFFView(){
	
    $sfCode = $_GET['FSf_Code'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
   

if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}


    $query = "exec getVisitFFView '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";
   
   
 
    return performQuery($query);;
    
}






function  getSalesforceTargetVsTotal(){
    $sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
  
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
  


$fromdate=date('m', strtotime($fromdate));
$todate=date('Y', strtotime( $todate ));



    $query = "exec getSalesforceTargetVsTotal '" . $sfCode . "','" . $Owndiv . "','".$fromdate."','".$todate."'  ";
   
   
  
   
 $result['distributortarget']=performQuery($query);
    return $result;
    
}
function  GetvanLedger(){
    $sfCode = $_GET['Rsf_code'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
  
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
  






    $query = "exec van_stock_ledger '" . $sfCode . "','" . $Owndiv . "','".$fromdate."','".$todate."','0'  ";
   
   
  
   
 $result['VanLedger']=performQuery($query);
    return $result;
    
}

function  GetvanLedgerView(){
    $sfCode = $_GET['Rsf_code'];
	
	
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
  
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
	
  
$query = "exec trans_van_stock_bal '" . $sfCode . "','" . $Owndiv . "','".$fromdate."','".$todate."','".$_GET['ProductCode']."'";


$result['TotalVanProduct']=performQuery($query);


    $query = "exec trans_van_stock '" . $sfCode . "','" . $Owndiv . "','".$fromdate."','".$todate."','".$_GET['ProductCode']."'";
   
   
  
   
 $result['CreditDebit']=performQuery($query);
    return $result;
    
}


function  PrimaryProductWise(){

   $sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $Type = $_GET['Type'];
    $fromdate = $_GET['fromdate'];
    $todate = $_GET['todate'];
    $Mr_SFCode = $_GET['Mr_SFCode'];
    $Stockist_Code=$_GET['Stockist_Code'];

if($Type==2 ){
$fromdate=date('m', strtotime($today ));
$todate=date('Y', strtotime($today ));
}
if($Type==3 ){
$fromdate = $_GET['fromdate'];
}


    $query = "exec getPrimaryproductwise '" . $sfCode . "','" . $Owndiv . "' , '" .$Type. "','".$fromdate."','".$todate."' ";
   
  $result['productwisesales']=performQuery($query);
    return $result;
}



function DayPragnancyReport() {
    $sfCode = $_GET['sfCode'];
    $dyDt = $_GET['rptDt'];
 $today = date('Y-m-d 00:00:00');
  
    
    $query = "select *  from vwActivity_Report where SF_Code='" .$sfCode . "' and   DATEDIFF(day,Activity_Date,'" . $dyDt . "')=0";
    $temp = performQuery($query);
    $result=array();



     $result['TodayPragnancy']=$temp[0];
    return $result;
}

function TpRouteNotify() {

  $sfCode = $_GET['sfCode'];
  $today = date('Y-m-d H:i:s');
    $divCode = $_GET['divisionCode'];
    $divisionCode = explode(",", $divCode);
	
	
  
    $sql = "SELECT Sf_Name,Reporting_To_SF,sf_type FROM Mas_Salesforce where Sf_Code='$sfCode'";

   $sfType = performQuery($sql);
   $sql = "SELECT  CONCAT('N',MAX(CONVERT(INT, RIGHT(NotifyID, LEN(NotifyID)-1)))+1) as id FROM Mas_AppNotifyList";
   $Maxid = performQuery($sql);
  $NtfyMsg=  $sfType[0]['Sf_Name'].'has changed route instad of Tour plan route';
   
   
   
   $sql = "insert into Mas_AppNotifyList(SF_Code,NotifyID,Subject,NtfyMsg,CrDt,DivCode,typ) values( '".$sfType[0]['Reporting_To_SF'] . "','" . $Maxid[0]['id'] . "','Wrong! Route','$NtfyMsg','".$today."'," . $divisionCode[0] . "," . $sfType[0]['sf_type'] . ")";
     performQuery($sql); 
	
$sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
            $device = performQuery($sql);

            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                
                    
                sendFCM_notify($reg_id, $NtfyMsg,'Wrong! Route',0,'#sign-in');
            }
				

}

function DayUpdatePragnancyReport() {
    $sfCode = $_GET['sfCode'];
    $OrderID = $_GET['OrderID'];
  $Confirm = $_GET['Confirm'];
    $Flag = $_GET['Flag'];
    

     $sql = "update Trans_Pragnancy__Head set Order_Flag=1 ,Message='".$Confirm."',Order_No='".$Flag."'  where sf_Code='" . $sfCode . "' and  OrderID='".$OrderID."' ";
   
$result = performQuery($sql);
    $result=array();



     $result['TodayPragnancy']=true;
    return $result;
}





function getsummary() {
    $sfCode = $_GET['sfCode'];
    $date=date('Y-m-d');
    $query = "select * from dcr_summary where sf_code='$sfCode' and cast(submission_date as date)='$date'";
    return performQuery($query);
}

function getDaySummary() {
    $sfCode = $_GET['sfCode'];
    $date=date('Y-m-d');

    $query = "select * from dcr_summary where sf_code='$sfCode' and cast(submission_date as date)='$date'";
    $dcrSummary=performQuery($query);
    $result=array();
    $result['dcrSummary']=$dcrSummary;
    if($dcrSummary[0]['editFlag']==0){
        $query="exec getDaySummaryDCR_TLSD_ReportApp '$sfCode'";
        $DCR_TLSD=performQuery($query);
        $query="exec getDaySummaryDCR_LPC_ReportApp '$sfCode'";
        $DCR_LPC=performQuery($query);
        $query="exec getDaySummaryBrandWiseReportApp '$sfCode'";
        $brandwise=performQuery($query);

        $result['DCR_TLSD']=$DCR_TLSD;
        $result['DCR_LPC']=$DCR_LPC;
        $result['brandwise']=$brandwise;
    }
    return $result;
}

function getDailyAllow() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
	$date=date('Y-m-d');
	$sfCode = $_GET['sfCode'];
    $query = "select ID,Allowance_Name Name,Max_Allowance,Attachemnt from Mas_Allowance_Type where Type=1 and Division_Code=". $Owndiv ." and user_enter=1 and active_flag=0";
	$ExpenseWeb=performQuery($query);
	$query = "select * from Expense_Start_Activity where Sf_code='$sfCode'  and convert(date,Date_Time)='".$date."'";
	$TodayExpense=performQuery($query);
	
	 $result['ExpenseWeb']=$ExpenseWeb;
        $result['TodayExpense']=$TodayExpense;
    return $result;
	
}

function getPendingBills(){
    $sfCode = $_GET['rptSF'];
    $query = "exec getPendingBills '" . $sfCode . "'";
    return performQuery($query);

}

function getInvFlags(){
    $sfCode = $_GET['rSF'];
    $eDt = $_GET['edts'];
    $Flags=array();
    $query = "exec getInvFlags '" . $sfCode . "','" . $eDt . "'";
    $res=performQuery($query);
    $Flags["BInvFlg"]=$res[0]["BInvFlg"];
    $Flags["EInvFlg"]=$res[0]["EInvFlg"];
    $Flags["AInvFlg"]=$res[0]["AInvFlg"];
    return $Flags;
}

function getVstDets() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
    $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";
    return performQuery($query);
}


function getVstPragnancyDets() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];




       $query = "select  convert(varchar, convert(date,TP.Order_Date),5) as Activity_Date,TP.*,dr.ListedDr_Name as name  from Trans_Pragnancy__Head   TP inner join Mas_ListedDr dr  on  TP.Trans_Sl_No ='".$ACd."' and    Tp.Order_Flag is null and dr.ListedDrCode=Tp.Cust_Code";
    return performQuery($query);
}

function getVstPragnancyDetReport() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];

       $query = "select  convert(varchar, convert(date,TP.Order_Date),5) as Activity_Date,TP.*,dr.ListedDr_Name as name  from Trans_Pragnancy__Head   TP inner join Mas_ListedDr dr  on  TP.Trans_Sl_No ='".$ACd."'  and    Tp.Order_Flag IS NOT NULL and    dr.ListedDrCode=Tp.Cust_Code";
    return performQuery($query);
}

function getVstPendingPragnancy() {
    
$sfCode = $_GET['sfCode'];
    $query = "select  TP.Order_Date as Activity_Date, TP.Remarks as remarks,TP.*,dr.ListedDr_Name as name  from Trans_Pragnancy__Head   TP inner join Mas_ListedDr dr  on      Tp.Order_Flag is null and dr.ListedDrCode=Tp.Cust_Code and  TP.Sf_Code='".$sfCode."' and convert(date,Order_Date)!='".date('Y-m-d')."'";
    


return performQuery($query);
}



function getVwOrderDet() {
    $dcrCode = $_GET['DCR_Code'];
    $query = "select * from vwOrderDetails where Dcr_Code='".$dcrCode."'";
    return performQuery($query);
}

function getCurrentStockDet() {
    if ($_GET['scode'] == "0")
        $SF = $_GET['sfCode'];
    else
        $SF = $_GET['scode'];

    $CLDT = $_GET['cldt'];

    if($CLDT=='') $CLDT=date('Y-m-d 00:00:00');
    $query = "exec getClStkVw '$SF','$CLDT'";
    $transHead = performQuery($query);
    return outputJSON($transHead);
}
function getCurrentSSStockDet() {
    if ($_GET['scode'] == "0")
        $SF = $_GET['sfCode'];
    else
        $SF = $_GET['scode'];

    $CLDT = $_GET['cldt'];

    if($CLDT=='') $CLDT=date('Y-m-d 00:00:00');
    $query = "exec getSSClStkVw '$SF','$CLDT'";
    $transHead = performQuery($query);
    return outputJSON($transHead);
}
function getPreCallDet() {
    $SF = $_GET['sfCode'];
    $MSL = $_GET['Msl_No'];

    $result = array();
   $query = "select SLVNo SVL,Doc_Class_ShortName DrCat,Doc_Spec_ShortName DrSpl,isnull(stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory S where CHARINDEX(cast(Doc_SubCatCode as varchar),D.Doc_SubCatCode)>0 for XML Path('')),1,2,''),'') DrCamp,isnull(stuff((select ', '+Product_Detail_Name from Map_LstDrs_Product M  inner join Mas_Product_Detail P on M.Product_Code=P.Product_Detail_Code and P.Division_Code=M.Division_Code where Listeddr_Code=D.ListedDrCode for XML Path('')),1,2,''),'') DrProd,Case WHEN ListedDr_Address1!=''  and ListedDr_Address1 IS NOT NULL THEN ListedDr_Address1 WHEN ListedDr_Address2!=''  and ListedDr_Address2 IS NOT NULL THEN ListedDr_Address2 ELSE ListedDr_Address3 END as Address  from mas_listeddr  D where ListedDrCode='" . $MSL . "'";
  $as = performQuery($query);

    if (count($as) > 0) {
        $result['SVL'] = (string) $as[0]['SVL'];
        $result['DrCat'] = (string) $as[0]['DrCat'];
        $result['DrSpl'] = (string) $as[0]['DrSpl'];
        $result['DrCamp'] = (string) $as[0]['DrCamp'];
        $result['DrProd'] = (string) $as[0]['DrProd'];
        $result['Address'] = (string) $as[0]['Address'];

         $dateYeabbr=date('m');
$now = new DateTime();
$year = $now->format("Y");
$Month=$now->format("M");
        $result['success'] = true;
        $query = "select SUM(Order_value)  sumoforder,stockist_code from vwTransOrderHead where id<7 and Cust_Code=$MSL group by stockist_code order by stockist_code"; // and Sf_Code='$SF'
        $transHead = performQuery($query);

        $query = "select Stockist_Code,Last_Order_Amt,Out_standing_Amt from Order_Collection_Details where Cust_Code=$MSL order by Stockist_Code";  // and Sf_Code='$SF' 
        $collectionOrder = performQuery($query);
        $query = "select * from vwOffTakeQtys where cust_Code='".$MSL."' ";
        $OpeningStock = performQuery($query);
        $query = "select * from Trans_MOQ where Cust_Code='".$MSL."' ";


        $MOQ = performQuery($query);
        
        $queryy="select SUM (Order_Value) AS MorderSum from Trans_Order_Head  where Cust_Code='".$MSL."' and MONTH(Order_Date) = '".$dateYeabbr."' AND YEAR(Order_Date) = '".$year."'";
        $MOV = performQuery($queryy);
$query = "select *,case when (isnull(contactperson,'') +','+ isnull(contactperson2,'')+','+ isnull(contactperson3,'')=',,') then (' ') else isnull(contactperson,'') +','+ isnull(contactperson2,'')+','+ isnull(contactperson3,'') end  as ContactPersionn  from Mas_ListedDr where ListedDrCode='".$MSL."' ";
        $POT = performQuery($query);
        if (count($collectionOrder) > 0) {
            for ($i = 0; $i < count($transHead); $i++) {
                $query = "select Product_Code,Product_Name,Quantity,value from ( select ROW_NUMBER() OVER(PARTITION BY Cust_Code,Stockist_Code order by Cust_Code,Stockist_Code,Order_Date desc) RW,Trans_Sl_No,sf_code,Cust_Code,Stockist_Code,Order_Date,route from Trans_Order_Head where Cust_Code='".$MSL."' and Stockist_Code='".$transHead[$i]['stockist_code']."') as H inner join Trans_Order_details D on H.Trans_Sl_No=D.Trans_Sl_No and RW=1";
                $RES = performQuery($query);
                $stockist[] = array(
                    "stockist_code" => $transHead[$i]['stockist_code'],
                    "Sum" => $transHead[$i]['sumoforder'],
                    "Avg" => floor($transHead[$i]['sumoforder'] / 6),
                    "LastOrderAmt" => $collectionOrder[$i]['Last_Order_Amt'],
                    "outStandingAmt" => $collectionOrder[$i]['Out_standing_Amt'],
                    "OrderDetails" => $RES
                );                  
            }
        }
		 $query = "select  top 1 convert(varchar,convert(date,Order_Date))  as Cdate from Trans_Order_Head where Cust_Code='".$MSL."' order by Order_Date ASC";
		$getcustdte=performQuery($query);
		$lastOrder_Date= $getcustdte[0]['Cdate'];
		
		
		$query ="select Product_Code,Product_Name,Quantity,value , H.Trans_Sl_No,sf_code,Cust_Code,Stockist_Code,Order_Date,MPD.Product_Brd_Code from Trans_Order_Head   H inner join Trans_Order_details D on H.Trans_Sl_No=D.Trans_Sl_No
 inner join Mas_Product_Detail MPD on MPD.Product_Detail_Code=D.Product_Code
 where Cust_Code='".$MSL."'  and convert(date,Order_Date)='".$lastOrder_Date."' ";
		
		
		$result['PreviousOrder'] = performQuery($query);
        $result['StockistDetails'] = $stockist;
        $result['OpeningStock'] =  $OpeningStock;
        $result['MOQ'] = $MOQ;
        $result['POTENTIAL'] = $POT;
        $result['MOV'] = $MOV;
        $query = "select Trans_SlNo,Trans_Detail_Slno,convert(varchar,Time,0) Adate,convert(varchar,cast(convert(varchar,Activity_Date,101)  as datetime),20) as DtTm,(Select content from vwFeedTemplate where ID=Rx) CalFed,Activity_Remarks,products,gifts from vwLastVstDet where rw=1 and Trans_Detail_Info_Code='" . $MSL . "'"; //and SF_Code='" . $SF . "'
        $as = performQuery($query);
        if (count($as) > 0) {
            $result['LVDt'] = $as[0]['Adate'];
            $Prods = (string) $as[0]['products'];
            $sProds = explode("#", $Prods . '#');
            $sSmp = '';
            $sProm = '';
            for ($il = 0; $il < count($sProds); $il++) {
                if ($sProds[$il] != '') {
                    $spr = explode("~", $sProds[$il]);
                    $Qty = 0;
                    if (count($spr) > 0) {
                        $QVls = explode("$", $spr[1]);
                        $Qty = $QVls[0];
                        $Vals = $QVls[1];
                    }
                    if ($Qty > 0)
                        $sSmp = $sSmp . $spr[0] . " ( " . $Qty . " )" . (($Vals > 0) ? " ( " . $Vals . " )" : "");
                    else
                        $sProm = $sProm . $spr[0] . ", ";
                }
            }

            $result['CallFd'] = (string) $as[0]['CalFed'];
            $result['Rmks'] = (string) $as[0]['Activity_Remarks'];
            $result['ProdSmp'] = $sSmp;
            $result['Prodgvn'] = $sProm;
            $result['DrGft'] = (string) $as[0]['gifts'];
        }else {
            $result['success'] = false;
        }
    } else {
        $result['success'] = false;
    }
    return outputJSON($result);
}

function getEntryCount() {
    $sfCode = $_GET['sfCode'];
    $today = date('Y-m-d 00:00:00');
    $results = array();
    $query = "select Count(Trans_Detail_Info_Code) doctor_count from vwActivity_MSL_Details D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(H.activity_date as datetime)=cast('$today' as datetime)";
 
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) chemist_count from vwActivity_CSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=2";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) stockist_count from vwActivity_CSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=3";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) uldoctor_count from vwActivity_Unlst_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=4";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select isnull((SELECT top 1 isnull(remarks,'') from vwActivity_Report where sf_code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)),'') as remarks";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select isnull((SELECT top 1 Half_Day_FW from vwActivity_Report where sf_code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)),'') as halfdaywrk";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) hospital_count from vwActivity_CSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=5";
    $temp = performQuery($query);
    $results[] = $temp[0];


$query = "select Count(Trans_Detail_Info_Code) SuperStokit from vwActivity_SuperCSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=8";
    $temp = performQuery($query);
    $results[] = $temp[0];
    return $results;
}

function getaddress($lat, $lng) {
    $url = 'http://maps.googleapis.com/maps/api/geocode/json?latlng=' . trim($lat) . ',' . trim($lng) . '&sensor=false';
    $json = @file_get_contents($url);
    $data = json_decode($json);
    $status = $data->status;
    if ($status == "OK") {
        return $data->results[0]->formatted_address;
    } else {
        return false;
    }
}

function updEntry() {
    $today = date('Y-m-d 00:00:00');
    $data = json_decode($_POST['data'], true);
    $SFCode = (string) $data[0]['Activity_Report']['SF_code'];
    $sql = "select SF_Code from vwActivity_report where sf_Code=" . $SFCode . " and cast(activity_date as datetime)=cast('$today' as datetime)";
    $result = performQuery($sql);
    if (count($result) < 1) {
        $result = array();
        $result['success'] = false;
        $result['type'] = 2;
        $result['msg'] = 'No Call Report Submited...';
        outputJSON($result);
        die;
    }

    $Remarks = (string) $data[0]['Activity_Report']['remarks'];
    $HalfDy = (string) $data[0]['Activity_Report']['HalfDay_FW_Type'];

    $sql = "update DCRMain_Temp set Remarks=$Remarks,Half_Day_FW=$HalfDy where sf_Code=" . $SFCode . " and cast(activity_date as datetime)=cast('$today' as datetime)";
    $result = performQuery($sql);
    $sql = "update DCRMain_Trans set Remarks=$Remarks,Half_Day_FW=$HalfDy where sf_Code=" . $SFCode . " and cast(activity_date as datetime)=cast('$today' as datetime)";
    $result = performQuery($sql);
    $resp["success"] = true;
    echo json_encode($resp);
}

function GetNotifyLists(){
    $sfCode = $_GET['sfCode'];
    $query = "exec getAppNotifyLists '" . $sfCode . "'";
    return performQuery($query);
}

function GetTPNotifyLists(){

  $sfCode = $_GET['sfCode'];
$sql = "SELECT  MAX(CONVERT(INT, RIGHT(NotifyID, LEN(NotifyID)-1))) as id FROM Mas_AppNotifyList";
   $Maxid = performQuery($sql);
$Nid=  'N'.$Maxid[0]['id'];
$sql = "select top 1 * from Mas_AppNotifyList where SF_Code='".$sfCode."'  ORDER BY CrDt DESC";
   
    return performQuery($sql);
}



function getFromTableWR($tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null) {

    $query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
    $query.= " WHERE tab.Division_Code=" . $divisionCode;

    if (!is_null($where)) {
        $query.=" and " . join(" or ", $where);
    }

    if (!is_null($today)) {
        $today = date('Y-m-d 00:00:00');
        $query.="and cast(tab.activity_date as datetime)=cast('$today' as datetime)";
    }

    if (!is_null($orderBy)) {
        $query .=" ORDER BY " . join(", ", $orderBy);
    }

    return performQuery($query);
}

function getFromTable($tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null, $desig) {
    $query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
	if ($desig == 'stockist' ){
		
    $sql ="select Field_Code from mas_stockist where Stockist_Code='".$sfCode."'";
    $Field_Code = performQuery($sql);
	$sfCode=$Field_Code[0]['Field_Code'];
		
		
	}
	
    if (!is_null($sfCode)) {
        if ($tableName == 'vwTown_Master_APP' )                                  
            $query .=" WHERE (charindex('$sfCode',','+tab.SF_Code+',')>0 or charindex(',$sfCode,',','+tab.DSM)>0)";
        else if ($tableName == "vwDoctor_Master_APP" && $desig != 'stockist')
            $query .=" WHERE (charindex(',$sfCode,',','+tab.Field_Code+',')>0  or charindex(',$sfCode,',','+tab.DSM)>0)";
		  else if ($desig == 'stockist')
            $query .=" WHERE charindex('$sfCode',','+tab.Field_Code+',')>0";
		
        else {
            $query .=" WHERE tab.SF_Code='$sfCode'";
        }
    } else {
        $query.= " WHERE tab.Division_Code=" . $divisionCode;
    }
    if (!is_null($where)) {
        $query.=" and " . join(" and ", $where);
    }
    if (!is_null($today)) {
        $today = date('Y-m-d 00:00:00');
        $query.=" and cast(tab.activity_date as datetime)=cast('$today' as datetime)";
    }
    if (!is_null($orderBy)) {
        $query .=" ORDER BY " . join(",", $orderBy);
    }

    return performQuery($query);


}

function updateEntry($sfCode) {
    $dt = date('Y-m-d');
    $sql = "select SUM(cast(Amount as int)) amt from Trans_Additional_Exp where CAST(Created_Date as date)='$dt' and Cal_Type=0 and Sf_Code='$sfCode'";
    $positiveVal = performQuery($sql);
    $sql = "select SUM(cast(Amount as int)) amt from Trans_Additional_Exp where CAST(Created_Date as date)='$dt' and Cal_Type=1 and Sf_Code='$sfCode'";
    $negativeVal = performQuery($sql);
    $updateAdditionalAmt = $positiveVal[0]['amt'] - $negativeVal[0]['amt'];
    $sql = "select Expense_Allowance,Expense_Distance,Expense_Fare,Expense_Total from  Trans_FM_Expense_Detail where CAST(Created_Date as date)='$dt' and Sf_Code='$sfCode'";
    $expenseDetail = performQuery($sql);
    $query = "delete from Trans_Additional_Exp where CAST(Created_Date as date)='$dt' and Sf_Code='$sfCode'";
    performQuery($query);
    $query = "delete from Trans_FM_Expense_Detail where CAST(Created_Date as date)='$dt' and Sf_Code='$sfCode'";
    performQuery($query);
    $query = "delete from Trans_FM_Expense_Head where CAST(snd_dt as date)='$dt' and Sf_Code='$sfCode'";
    performQuery($query);
    $sql = "select Total_Allowance,Total_Distance,Total_Fare,Total_Expense,Total_Additional_Amt,Grand_Total from Trans_Expense_Amount_Detail where Month=MONTH('$dt') and Year=YEAR('$dt') and Sf_Code='$sfCode'";
    $amountDetail = performQuery($sql);
    $Total_Allowance = $amountDetail[0]['Total_Allowance'] - $expenseDetail[0]['Expense_Allowance'];
    $Total_Distance = $amountDetail[0]['Total_Distance'] - $expenseDetail[0]['Expense_Distance'];
    $Total_Fare = $amountDetail[0]['Total_Fare'] - $expenseDetail[0]['Expense_Fare'];
    $Total_Expense = $amountDetail[0]['Total_Expense'] - $expenseDetail[0]['Expense_Total'];
    $Total_Additional_Amt = $amountDetail[0]['Total_Additional_Amt'] - $updateAdditionalAmt;
    $Grand_Total = $Total_Expense - $Total_Additional_Amt;
    $sql = "update Trans_Expense_Amount_Detail set Total_Allowance=$Total_Allowance,Total_Distance=$Total_Distance,Total_Fare=$Total_Fare,Total_Expense=$Total_Expense,Total_Additional_Amt=$Total_Additional_Amt,Grand_Total=$Grand_Total where Month=MONTH('$dt') and Year=YEAR('$dt') and Sf_Code='$sfCode'";
    performQuery($sql);
}

function orderDetailsDelete($sfCode, $custCode,$arc, $amc) {
    $date = date('Y-m-d');
    $sql = "select Trans_Sl_No,Order_Value,Collected_Amount from Trans_Order_Head where dcr_code='$amc'";
    $trDet = performQuery($sql);

    for ($t = 0; $t < count($trDet); $t++) {
        $Trans_Sl_No = $trDet[$t]['Trans_Sl_No'];
    
        $orderValue =$trDet[$t]['Order_Value'];
        $collectedAmount = $trDet[$t]['Collected_Amount'];
        $sql = "select Total_Order_Amount,Out_standing_Amt,Amt_Collect,Last_order_Amt from Order_Collection_Details where Sf_Code='$sfCode' and Cust_Code=$custCode";
        $tr = performQuery($sql);
        $total = $tr[0]['Total_Order_Amount'] - $orderValue;
    
        $collectAmount = $tr[0]['Amt_Collect'] - $collectedAmount;
        $outstand = $total - $collectAmount;
        $sql = "update Order_Collection_Details set Total_Order_Amount=$total,Out_standing_Amt=$outstand,Amt_Collect=$collectAmount,Last_Order_Amt=0 where Sf_Code='$sfCode' and Cust_Code=$custCode";
        $tr = performQuery($sql);
        $query = "delete from Trans_Order_Head WHERE Trans_Sl_No ='" . $Trans_Sl_No . "'";
        performQuery($query);
        $sql = "select Trans_Order_No from Trans_Order_Details where Trans_Sl_No='$Trans_Sl_No'";
        $tr = performQuery($sql);
        for ($i = 0; $i < count($tr); $i++) {
            $Trans_Order_No = $tr[$i]['Trans_Order_No'];
            $sql = "delete from Trans_Order_Details WHERE Trans_Order_No='$Trans_Order_No'";
            performQuery($sql);
        }
    }
}

function orderDetailsDeleteOtherWorkType($sfCode) {
    $date = date('Y-m-d');
    $sql = "select Cust_Code from Trans_Order_Head where Sf_Code='$sfCode' and cast(convert(varchar,Order_Date,101) as datetime)=cast('$date' as datetime)";
    $custCodes = performQuery($sql);

    for ($c = 0; $c < count($custCodes); $c++) {
        $custCode = $custCodes[$c]['Cust_Code'];
        $sql = "select Trans_Sl_No,Order_Value,Collected_Amount from Trans_Order_Head where Sf_Code='$sfCode' and Cust_Code=$custCode and cast(convert(varchar,Order_Date,101) as datetime)=cast('$date' as datetime)";
       
        $tr = performQuery($sql);
        $Trans_Sl_No = $tr[0]['Trans_Sl_No'];
        $orderValue = $tr[0]['Order_Value'];
        $collectedAmount = $tr[0]['Collected_Amount'];
        $sql = "select Total_Order_Amount,Out_standing_Amt,Amt_Collect,Last_order_Amt from Order_Collection_Details where Sf_Code='$sfCode' and Cust_Code=$custCode";
        $tr = performQuery($sql);
        $total = $tr[0]['Total_Order_Amount'] - $orderValue;

        $collectAmount = $tr[0]['Amt_Collect'] - $collectedAmount;
        $outstand = $total - $collectAmount;
        $sql = "update Order_Collection_Details set Total_Order_Amount=$total,Out_standing_Amt=$outstand,Amt_Collect=$collectAmount,Last_Order_Amt=0 where Sf_Code='$sfCode' and Cust_Code=$custCode";
        $tr = performQuery($sql);
        $query = "delete from Trans_Order_Head WHERE Trans_Sl_No ='" . $Trans_Sl_No . "'";
        performQuery($query);
        
        $sql = "delete from Trans_Order_Details where Trans_Sl_No='$Trans_Sl_No'";;
        performQuery($sql);
    }
}

function deleteEntry($arc, $amc) {
    if (!is_null($amc)) {
        $sql = "DELETE FROM DCRDetail_Lst_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_Lst_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

        $sql = "DELETE FROM DCRDetail_CSH_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_CSH_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

        $sql = "DELETE FROM DCRDetail_Unlst_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_Unlst_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

        $sql = "DELETE FROM Trans_Pragnancy__Head where Trans_Sl_No='" . $arc . "'";
        performQuery($sql);
    }
}

function getDistReport() {
    $filter = json_decode($_GET['filter'], true);
    $sfCode = $filter['sfCode'];
    $stockist = $filter['stockist'];
    $dyDt = $filter['date'];
    if ($filter['checkStock'] == 1)
        $sql = "select * from vwDistReport where cast(convert(varchar,Order_Date,101) as datetime)='$dyDt' and Stockist_Code=$stockist";
    if ($filter['checkStock'] != 1)
        $sql = "select * from vwDistReport where Sf_Code='$sfCode' and cast(convert(varchar,Order_Date,101) as datetime)='$dyDt' and Stockist_Code=$stockist";
    $res = performQuery($sql);
    return outputJSON($res);
}

function delAREntry($SF, $WT, $Dt) {
    $sqlH = "SELECT Trans_SlNo FROM vwActivity_Report where SF_Code='" . $SF . "' and lower(Work_Type) <> lower(" . $WT . ") and cast(activity_date as datetime)=cast('$Dt' as datetime)";

    $sql = "DELETE FROM DCRDetail_Lst_Temp where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
    $sql = "DELETE FROM DCRDetail_Lst_Trans where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCRDetail_CSH_Temp where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
    $sql = "DELETE FROM DCRDetail_CSH_Trans where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCRDetail_Unlst_Temp where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
    $sql = "DELETE FROM DCRDetail_Unlst_Trans where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCREvent_Captures where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCRMain_Temp where SF_Code='" . $SF . "' and lower(Work_Type) <> lower(" . $WT . ") and cast(activity_date as datetime)=cast('$Dt' as datetime)";
    performQuery($sql);
    $sql = "DELETE FROM DCRMain_Trans where SF_Code='" . $SF . "' and lower(Work_Type) <> lower(" . $WT . ") and cast(activity_date as datetime)=cast('$Dt' as datetime)";
    performQuery($sql);
    $sql = "DELETE FROM DCRDetail_Distributors_Hunting where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
}

function ViewGeoTag(){

global $data;
    $data = json_decode($_POST['data'], true);
$SF=(string) $data['SF'];
$cust=(string)$data['cust'];

$query="exec getViewTag '".$SF."','".$cust."'";

return performQuery($query);;
}

function addEntry() {
    $sfCode = $_GET['sfCode'];
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $data = json_decode($_POST['data'], true);
    $today = date('Y-m-d 00:00:00');
    $temp = array_keys($data[0]);
    $vals = $data[0][$temp[0]];
    $SFState='0';
    $sql = "SELECT Employee_Id,case sf_type when 4 then 'MR' else 'MGR' End SF_Type,State_Code FROM vwUserDetails where SF_code='" . $sfCode . "'";
    $as = performQuery($sql);
    $IdNo = (string) $as[0]['Employee_Id'];
    $SFTyp = (string) $as[0]['SF_Type'];
    $SFState=(string) $as[0]['State_Code'];
    switch ($temp[0]) {
        case "stockUpdation":
            $stateCode = $_GET['State_Code'];
            $editable = $_GET['editable'];
            $date = date('Y-m-d H:i:s');
            if ($_GET['sCode'] != "0") {
                $staffCode = $sfCode;
                $sfCode = $_GET['sCode'];
            } else
                $staffCode = null;

            if($sfCode!="undefined")
            {
                if ($editable == 1) {
                    $sql = "SELECT * FROM Trans_Current_Stock_details where Stockist_Code='" . $sfCode . "'";
                    $tRw = performQuery($sql);
                    for ($i = 0; $i < count($tRw); $i++) {
                        $updateDat = $tRw[$i]['Last_Updation_Date'];
                        $updateDate = $updateDat->format('Y-m-d H:i:s');
                        $productCode = $tRw[$i]['Product_Code'];
                        $sql = "delete from Trans_Stock_Updation_Details where Stockist_code='$sfCode' and Product_Code='$productCode' and Purchase_Date='$updateDate'";
                        performQuery($sql);
                        $sql = "delete from Trans_Secondary_Sales_Details where Stockist_Code='$sfCode' and Product_Code='$productCode' and date='$updateDate'";
                        performQuery($sql);
                    }
                }
                $sql = "delete from Trans_Current_Stock_details where Stockist_Code='$sfCode'";
                performQuery($sql);
                for ($i = 0; $i < count($vals); $i++) 
                {
                    $sql = "SELECT isNull(max(Tran_Slno),0)+1 as RwID FROM Trans_Stock_Updation_Details";
                    $tRw = performQuery($sql);
                    $pk = (int) $tRw[0]['RwID'];
                    $productCode = $vals[$i]['product'];
                    $productName = $vals[$i]['product_Nm'];
                    $cbQty = !empty($vals[$i]['cb_qty']) ? $vals[$i]['cb_qty'] : 0;
                    $pieces = !empty($vals[$i]['pieces']) ? $vals[$i]['pieces'] : 0;
                   $Mfg=($vals[$i]['Mgf_date']==null? '':$vals[$i]['Mgf_date']);
                   
                     if(!empty($vals[$i]['conversionQty']))
                    {
                        $sampleErpCode = $vals[$i]['conversionQty'];
                    }else{
                        $sql = "SELECT Sample_Erp_Code FROM Mas_Product_Detail where Product_Detail_Code='$productCode'";
                        $sampleCode = performQuery($sql);
                        $sampleErpCode = $sampleCode[0]['Sample_Erp_Code'];
                    }
                    $recvQty = $vals[$i]['recv_qty'];

                    $sql = "select isnull(cast(DistCasePrice as float),0) Distributor_Price,isnull(cast(RetailCasePrice as float),0) Retailor_Price from vwProductStateRates where State_Code=$stateCode and Product_Detail_Code='" . $productCode . "'";
                    $tr = performQuery($sql);
                    if (empty($tr)) {
                            $distPrice = 0;
                            $retPrice = 0;
                    }
                    else
                    {
                        $distPrice = $tr[0]['Distributor_Price'];
                        $retPrice = $tr[0]['Retailor_Price'];
                    }
        
                    $sql = "SELECT TOP(1)Cl_Qty,pieces FROM Trans_Secondary_Sales_Details where Stockist_Code='$sfCode' and Product_Code='$productCode' order by date desc";
                    $tRw = performQuery($sql);
                    $Op_Qty = 0;$OpP_Qty =0;
                    if (empty($tRw))
                    {
                            $Op_Qty = 0;$OpP_Qty =0;
                    }
                    else
                    {
                        $Op_Qty = $tRw[0]['Cl_Qty'];
                        $OpP_Qty = $tRw[0]['pieces'];
                    }

                    $sql = "select count(Sale_Code) Trans_Secondary_Sales_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and date='" . $date . "'";
                    $updFlag=false;
                    if((int)$tRw[0]['RwID']>0){
                        $updFlag=true;
                        $sql = "select Rec_Qty,Cl_Qty,pieces,Op_Qty,Sale_Qty,OP_Pieces Trans_Secondary_Sales_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and date='" . $date . "'";
                        $tRw = performQuery($sql);
                        if($recvQty<=0 && $tRw[0]['Rec_Qty']>0) $recvQty=$tRw[0]['Rec_Qty'];
                    }

                    $sql = "delete Trans_Stock_Updation_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and Purchase_Date='" . $date . "'";
                    performQuery($sql);

                    $sql = "insert into Trans_Stock_Updation_Details(Tran_Slno,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Distributer_Rate,Retailor_Rate,Purchase_Date,Conversion_Qty,SfCode,Mgf_date) select '" . $pk . "','" . $sfCode . "','" . $productCode . "','" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",$distPrice,$retPrice,'" . $date . "',$sampleErpCode,'$staffCode','$Mfg'";
                    performQuery($sql);
                
                    $sql = "SELECT Stockist_Code FROM Trans_Current_Stock_details where Stockist_Code='$sfCode' and Product_Code='$productCode'";
                    $tRw = performQuery($sql);
                    if (!empty($tRw)) {
                        $sql = "DELETE FROM Trans_Current_Stock_details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "'";
                        performQuery($sql);
                    }


                   $sql = "select Distributor_Price,MRP_Price from mas_product_detail PMD inner join Mas_Product_State_Rates MPS  on PMD.Product_Detail_Code=MPS.Product_Detail_Code   where PMD.Product_Detail_Code='" . $productCode . "' and MPS.State_Code='$stateCode' ";
 
                   $CandPrate=performQuery($sql);
                   $Prate= $CandPrate[0]['MRP_Price'];
                   $Crate= $CandPrate[0]['Distributor_Price'];


                    $sql = "insert into Trans_Current_Stock_details(Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Last_Updation_Date,Conversion_Qty,SfCode,Mgf_date,Crate,Prate) select '" . $sfCode . "','" . $productCode . "','" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",'" . $date . "',$sampleErpCode,'$staffCode','$Mfg','$Crate','$Prate'";
                    performQuery($sql);

                    $sql = "SELECT isNull(max(Sale_Code),0)+1 as RwID FROM Trans_Secondary_Sales_Details";
                    $tRw = performQuery($sql);
                    $pk = (int) $tRw[0]['RwID'];
    
                    if (($pieces-$OpP_Qty) > 0) {
                        $saleQty = (($recvQty + $Op_Qty) - ($cbQty + 1));
                        $salepieces = $sampleErpCode - ($pieces-$OpP_Qty);
                    } else {
                        $saleQty = ($recvQty + $Op_Qty) - ($cbQty);
                        $salepieces = 0 - ($pieces-$OpP_Qty);
                    }
                    if($saleQty<0){$saleQty = 0;$salepieces=0;}
                    if($salepieces<0){$salepieces=0;}
                    $sql = "delete Trans_Secondary_Sales_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and date='" . $date . "'";
                    performQuery($sql);
                    $sql = "insert into Trans_Secondary_Sales_Details(Sale_Code,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cl_Qty,pieces,Distributer_Rate,Retailor_Rate,Op_Qty,Sale_Qty,OP_Pieces,date,Conversion_Qty,sale_pieces,SfCode,RwFlg,Rec_Pieces) select $pk,'" . $sfCode . "','" . $productCode . "','" . $productName . "',$recvQty,$cbQty,$pieces,$distPrice,$retPrice,$Op_Qty,$saleQty,$OpP_Qty,'" . $date . "',$sampleErpCode,$salepieces,'$staffCode',1,0";
                    performQuery($sql);
                }
                $resp["success"] = true;
            }else{
                $resp["success"] = false;
            }
            echo json_encode($resp);
            die;
            break;


case "SSstockUpdation":
            $stateCode = $_GET['State_Code'];
            $editable = $_GET['editable'];
            $date = date('Y-m-d H:i:s');
            if ($_GET['sCode'] != "0") {
                $staffCode = $sfCode;
                $sfCode = $_GET['sCode'];
            } else
                $staffCode = null;

            if($sfCode!="undefined")
            {
                if ($editable == 1) {
                    $sql = "SELECT * FROM Trans_Current_SSStock_details where Stockist_Code='" . $sfCode . "'";
                    $tRw = performQuery($sql);
                    for ($i = 0; $i < count($tRw); $i++) {
                        $updateDat = $tRw[$i]['Last_Updation_Date'];
                        $updateDate = $updateDat->format('Y-m-d H:i:s');
                        $productCode = $tRw[$i]['Product_Code'];
                        $sql = "delete from Trans_SS_Stock_Updation_Details where Stockist_code='$sfCode' and Product_Code='$productCode' and Purchase_Date='$updateDate'";
                        performQuery($sql);
                        $sql = "delete from Trans_Secondary_SSSales_Details where Stockist_Code='$sfCode' and Product_Code='$productCode' and date='$updateDate'";
                        performQuery($sql);
                    }
                }
                $sql = "delete from Trans_Current_SSStock_details where Stockist_Code='$sfCode'";
                performQuery($sql);
                for ($i = 0; $i < count($vals); $i++) 
                {
                    $sql = "SELECT isNull(max(Tran_Slno),0)+1 as RwID FROM Trans_SS_Stock_Updation_Details";
                    $tRw = performQuery($sql);
                    $pk = (int) $tRw[0]['RwID'];
                    $productCode = $vals[$i]['product'];
                    $productName = $vals[$i]['product_Nm'];
                    $cbQty = !empty($vals[$i]['cb_qty']) ? $vals[$i]['cb_qty'] : 0;
                    $pieces = !empty($vals[$i]['pieces']) ? $vals[$i]['pieces'] : 0;
                    
                    if(!empty($vals[$i]['conversionQty']))
                    {
                        $sampleErpCode = $vals[$i]['conversionQty'];
                    }else{
                        $sql = "SELECT Sample_Erp_Code FROM Mas_Product_Detail where Product_Detail_Code='$productCode'";
                        $sampleCode = performQuery($sql);
                        $sampleErpCode = $sampleCode[0]['Sample_Erp_Code'];
                    }
                    $recvQty = $vals[$i]['recv_qty'];

                    $sql = "select isnull(cast(DistCasePrice as float),0) Distributor_Price,isnull(cast(RetailCasePrice as float),0) Retailor_Price from vwProductStateRates where State_Code=$stateCode and Product_Detail_Code='" . $productCode . "'";
                    $tr = performQuery($sql);
                    if (empty($tr)) {
                            $distPrice = 0;
                            $retPrice = 0;
                    }
                    else
                    {
                        $distPrice = $tr[0]['Distributor_Price'];
                        $retPrice = $tr[0]['Retailor_Price'];
                    }
        
                    $sql = "SELECT TOP(1)Cl_Qty,pieces FROM Trans_Secondary_SSSales_Details where Stockist_Code='$sfCode' and Product_Code='$productCode' order by date desc";
                    $tRw = performQuery($sql);
                    $Op_Qty = 0;$OpP_Qty =0;
                    if (empty($tRw))
                    {
                            $Op_Qty = 0;$OpP_Qty =0;
                    }
                    else
                    {
                        $Op_Qty = $tRw[0]['Cl_Qty'];
                        $OpP_Qty = $tRw[0]['pieces'];
                    }

                    $sql = "select count(Sale_Code) Trans_Secondary_SSSales_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and date='" . $date . "'";
                    $updFlag=false;
                    if((int)$tRw[0]['RwID']>0){
                        $updFlag=true;
                        $sql = "select Rec_Qty,Cl_Qty,pieces,Op_Qty,Sale_Qty,OP_Pieces Trans_Secondary_SSSales_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and date='" . $date . "'";
                        $tRw = performQuery($sql);
                        if($recvQty<=0 && $tRw[0]['Rec_Qty']>0) $recvQty=$tRw[0]['Rec_Qty'];
                    }

                    $sql = "delete Trans_SS_Stock_Updation_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and Purchase_Date='" . $date . "'";
                    performQuery($sql);

                    $sql = "insert into Trans_SS_Stock_Updation_Details(Tran_Slno,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Distributer_Rate,Retailor_Rate,Purchase_Date,Conversion_Qty,SfCode) select '" . $pk . "','" . $sfCode . "','" . $productCode . "','" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",$distPrice,$retPrice,'" . $date . "',$sampleErpCode,'$staffCode'";
                    performQuery($sql);



                
                    $sql = "SELECT Stockist_Code FROM Trans_Current_SSStock_details where Stockist_Code='$sfCode' and Product_Code='$productCode'";
                    $tRw = performQuery($sql);
                    if (!empty($tRw)) {
                        $sql = "DELETE FROM Trans_Current_SSStock_details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "'";
                        performQuery($sql);
                    }
                    $sql = "insert into Trans_Current_SSStock_details(Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Last_Updation_Date,Conversion_Qty,SfCode) select '" . $sfCode . "','" . $productCode . "','" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",'" . $date . "',$sampleErpCode,'$staffCode'";
                    performQuery($sql);
                    $sql = "SELECT isNull(max(Sale_Code),0)+1 as RwID FROM Trans_Secondary_SSSales_Details";
                    $tRw = performQuery($sql);
                    $pk = (int) $tRw[0]['RwID'];
    
                    if (($pieces-$OpP_Qty) > 0) {
                        $saleQty = (($recvQty + $Op_Qty) - ($cbQty + 1));
                        $salepieces = $sampleErpCode - ($pieces-$OpP_Qty);
                    } else {
                        $saleQty = ($recvQty + $Op_Qty) - ($cbQty);
                        $salepieces = 0 - ($pieces-$OpP_Qty);
                    }
                    if($saleQty<0){$saleQty = 0;$salepieces=0;}
                    if($salepieces<0){$salepieces=0;}
                    $sql = "delete Trans_Secondary_SSSales_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and date='" . $date . "'";
                    performQuery($sql);
                    $sql = "insert into Trans_Secondary_SSSales_Details(Sale_Code,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cl_Qty,pieces,Distributer_Rate,Retailor_Rate,Op_Qty,Sale_Qty,OP_Pieces,date,Conversion_Qty,sale_pieces,SfCode,RwFlg,Rec_Pieces) select $pk,'" . $sfCode . "','" . $productCode . "','" . $productName . "',$recvQty,$cbQty,$pieces,$distPrice,$retPrice,$Op_Qty,$saleQty,$OpP_Qty,'" . $date . "',$sampleErpCode,$salepieces,'$staffCode',1,0";
                    performQuery($sql);
                }
                $resp["success"] = true;
            }else{
                $resp["success"] = false;
            }
            echo json_encode($resp);
            die;
            break;



        case "tbMyDayPlan":

            //$sql="delete from tbmydayplan where sf_code='$sfCode' and cast(pln_date as date)=cast(GETDATE() as date)";
            // performQuery($sql);

            $ww=$vals["worked_with"];
            $datesumm=date('Y-m-d');
            $today=date('Y-m-d H:i:s');

            if($vals["dcr_activity_date"]!=null && $vals["dcr_activity_date"]!=''){
                    $today=str_replace("'", "", $vals["dcr_activity_date"]);
                    $datesumm=date('Y-m-d ',strtotime($today));
            }
 if( $vals['profilepic']!=null && $vals['profilepic']!="undefined")
            {
                                  
                   
                    
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,Division_Code,Identification,Insert_Date_Time) values( '" . $sfCode . "','" . $State_Code . "','" . $vals['profilepic']['imgurl'] . "','" . $Owndiv . "','Profile Pic','".$today."')";
                    performQuery($sql); 
     
            }

            if (str_replace("'", "", $vals["FWFlg"]) != "F") {
                    $sql="select count(*) count from dcr_summary where sf_code='$sfCode' and cast(submission_date as date)='$datesumm' and editFlag=1";
                    $cunSumm = performQuery($sql);
                    $cunSumm=$cunSumm[0]['count'];
                    if($cunSumm>0){
                            $result['msg'] = "DCR Summary Already Updated";
                            outputJSON($result);
                            die;
                    }
            }
            if($vals["dcrtype"]==null)
                    $dcrtype="";
            else
                    $dcrtype=$vals["dcrtype"];
				
				if($vals["OtherWors"]==null)
                    $OtherWors="";
            else
                    $OtherWors=$vals["OtherWors"];
				
				
				
				

            if($vals["location"]==null)
                    $location="";
            else
                    $location=$vals["location"];


 $sql = "insert into tbMyDayPlan(sf_code,sf_member_code,Pln_Date,cluster,remarks,Division_Code,wtype,FWFlg,ClstrName,stockist,worked_with_code,worked_with_name,dcrtype,location,CustId,CustName,StkName,Sprstk,Other_Works) select '" . $sfCode . "'," . $vals["sf_member_code"] . ",'".$today."'," . $vals["cluster"] . "," . $vals["remarks"] . ",'" . $Owndiv . "'," . $vals["wtype"] . "," . $vals["FWFlg"] . "," . $vals["ClstrName"] . "," . $vals["stockist"].",$ww,(Select SF_Name+', ' from vwUserDetails where charindex('$$'+sf_Code+'$$','$$'+$ww+'$$')>0 for XML path('')),'$dcrtype','$location','".$vals["custid"]."','".$vals["custName"]."','".$vals["stkName"]."' ,'".$vals["superstockistid"]."' ,'".$OtherWors."' ";  
performQuery($sql);

$missedquery="exec svPostMissAndAuto  '$sfCode','".$today."','" . $Owndiv . "' ";

performQuery($missedquery);


            if (str_replace("'", "", $vals["FWFlg"]) != "F") {
                $sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "'  and cast(activity_date as datetime)=cast('$today' as datetime)";
                $result1 = performQuery($sql);

                if (count($result1) > 0) {
                        if ($result1[0]['FWFlg'] == 'L' && $result1[0]['Confirmed'] != 2 && $result1[0]['Confirmed'] != 3) {
                            $result['msg'] = "Leave Post Already Updated";
                            outputJSON($result);
                            die;
                        } else {
                            orderDetailsDeleteOtherWorkType($sfCode);
                            delAREntry($sfCode, $vals["wtype"], $today);
                            $sql="delete from dcr_summary where sf_code='$sfCode' and cast(submission_date as date)='$datesumm'";
                            performQuery($sql);
                            $ARCd = "";
                            $sql = "{call  svDCRMain_App(?,?," . $vals["wtype"] . ",'" . str_replace("'", "", $vals["cluster"]) . "',?,'" . str_replace("'", "", $vals["remarks"]) . "',?,'" . str_replace("'", "", $vals["superstockistid"]) . "')}";
                            $params = array(array($sfCode, SQLSRV_PARAM_IN),
                                array($today, SQLSRV_PARAM_IN),
                                array($Owndiv, SQLSRV_PARAM_IN),
                                array(&$ARCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
                            performQueryWP($sql, $params);
                        }
                    } else {
                        delAREntry($sfCode, $vals["wtype"], $today);

                        $ARCd = "";
                        $sql = "{call  svDCRMain_App(?,?," . $vals["wtype"] . ",'" . str_replace("'", "", $vals["cluster"]) . "',?,'" . str_replace("'", "", $vals["remarks"]) . "',?,'" . str_replace("'", "", $vals["superstockistid"]) . "')}";
                        $params = array(array($sfCode, SQLSRV_PARAM_IN),
                            array($today, SQLSRV_PARAM_IN),
                            array($Owndiv, SQLSRV_PARAM_IN),
                            array(&$ARCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
                        performQueryWP($sql, $params);
                    }
            }
            break;
        case "chemists_master":
            $sql = "SELECT isNull(max(Chemists_Code),0)+1 as RwID FROM Mas_Chemists";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];

            $sql = "insert into Mas_Chemists(Chemists_Code,Chemists_Name,Chemists_Address1,Territory_Code,Chemists_Phone,Chemists_Contact,Division_Code,Cat_Code,Chemists_Active_Flag,Sf_Code,Created_Date,Created_By) select '" . $pk . "'," . $vals["chemists_name"] . "," . $vals["Chemists_Address1"] . "," . $vals["town_code"] . "," . $vals["Chemists_Phone"] . ",'','" . $Owndiv . "','',0,'" . $sfCode . "','" . date('Y-m-d H:i:s') . "','Apps'";
            performQuery($sql);
            break;
        case "DCRDetail_Distributors_Hunting":
            if($vals['slno']!=null ){
                $sql = "update DCRDetail_Distributors_Hunting set Shop_Name=" . $vals['name'] . ",Contact_Person=" . $vals['contact'] . ",Phone_Number=" . $vals['phone'] . ",area=" . $vals["area"] . ",remarks=" . $vals["remarks"] . " where slno=".$vals['slno']."";
                performQuery($sql);
            }
            else{
                $sql = "{call  svNewDistirbutorsHunting(" . $vals['name'] . "," . $vals['contact'] . "," . $vals['phone'] . "," . $vals["area"] . "," . $vals["remarks"] . ",'" . date('Y-m-d H:i:s') . "','$sfCode')}";
                performQueryWP($sql,[]);
            }
            break;
        case "unlisted_doctor_master":
            if($vals["unlisted_cat_code"]==null)$vals["unlisted_cat_code"]=0;
            $Ukey=$vals['unlisted_doctor_name'].date('Y-m-d H:i:s');
            $divCode = $_GET['divisionCode'];
            $divisionCode = explode(",", $divCode);
            $sqlrnd="SELECT Retailer_add_ND from Access_Master where division_code='".$divisionCode[0]."'";
            $flagofretailerND= performQuery($sqlrnd)[0]['Retailer_add_ND'];  
 	$today=date('Y-m-d H:i:s');
   

       if($vals['lat']==null || $vals['lat']=="undefined"){
          $Lat='';
           $Long='';
       }else{
          $Lat=$vals["lat"];
          $Long=$vals["long"];
        }

   $visitpath='';

     
        //$myfile = fopen("../server/LdrPhotos.txt", "a+");
        $sqlsp=$vals['unlisted_doctor_name']." message:".$sql."sf_code=".$_GET['sfCode']."\n";
if($vals['Retailerphoto']!=null && $vals['Retailerphoto']!="undefined")
            {
                      
        
                   $visitpath=$sfCode.'_'.''.$vals['Retailerphoto']['imgurl'];
                    
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,Division_Code,Identification,Insert_Date_Time) values( '$sfCode','" . $State_Code . "','" . $vals['Retailerphoto']['imgurl'] . "','" . $Owndiv . "','Retailer Pic','".$today."')";
                    performQuery($sql); 
					
        $sqlsp=$vals['unlisted_doctor_name']." Query 1:".$sql;
        //fwrite($myfile, $sqlsp);
            }


         $ListedDr_Active_Flag=0;
         if($flagofretailerND==1){
          $ListedDr_Active_Flag=3;
        }
            $sql = "{call  svListedDR_APP('$sfCode'," . $vals['unlisted_doctor_name'] . "," . $vals['unlisted_doctor_address'] . "," . $vals['unlisted_doctor_phone'] . ",'" . str_replace("'","",$vals['unlisted_doctor_cityname']) . "','" . str_replace("'","",$vals['unlisted_doctor_areaname']) . "','" . str_replace("'","",$vals['unlisted_doctor_contactperson']) . "', ' " . str_replace("'","",$vals['unlisted_doctor_designation']) . "','" . str_replace("'","",$vals['unlisted_doctor_gst']) . "','" . str_replace("'","",$vals['unlisted_doctor_pincode']) . "'," . $vals["unlisted_specialty_code"] . "," . $vals["unlisted_cat_code"] . "," . $vals["town_code"] . ",$ListedDr_Active_Flag,'" . date('Y-m-d H:i:s') . "',$Owndiv," . $vals["unlisted_class"] . ",0," . $vals['wlkg_sequence'] . ",'',''," . $vals['DrKeyId'] . ",'" . str_replace("'","",$vals['unlisted_doctor_phone2']) . "','" . str_replace("'","",$vals['unlisted_doctor_contactperson2']) . "','" . str_replace("'","",$vals['unlisted_doctor_designation2']) . "',
            '" .str_replace("'","",$vals['unlisted_doctor_landmark']) . "','" . str_replace("'","",$Ukey) . "','','','','" . $visitpath . "','".str_replace("'","",$Lat)."','".str_replace("'","",$Long)."','" . str_replace("'","",$vals['unlisted_doctor_contactperson2']) . "','" . str_replace("'","",$vals['unlisted_doctor_phone3']) . "')}";
           performQueryWP($sql,[]);
		$sqlsp=$vals['unlisted_doctor_name']." Query 2:".$sql;
        //fwrite($myfile, $sqlsp);
        //fclose($myfile);
		
		
		$sql = "SELECT ListedDrCode from Mas_ListedDr where Ukey='" . str_replace("'","",$Ukey) . "'";
                    $res = performQuery($sql);
                    if (count($res) > 0){
                        $cCode=$res[0]["ListedDrCode"];
						
			$vals["addrs"]="'".getaddress(str_replace("'","",$Lat),str_replace("'","",$Long))."'";
            $sql = "SELECT isNull(max(MapId),0)+1 as MapId FROM Map_GEO_Customers";
            $topr = performQuery($sql);
            $pk = (int) $topr[0]['MapId'];
            $sql = "insert into Map_GEO_Customers(MapId,Cust_Code,lat,long,addrs,StatFlag,Division_code) select " . $pk . "," . $cCode . ",'".str_replace("'","",$Lat)."','".str_replace("'","",$Long)."'," . $vals["addrs"] . ",0,'" . $Owndiv . "'" ;
            performQueryWP($sql,[]);
                    }
		
		
		   
		
		
		
		
		
		
    $resp["Qry"] = $sql;
    $resp["success"] = true;
    echo json_encode($resp);
    die;


        break;
 case "unlisted_Expense_start":
   $Datetime=date('Y-m-d H:i:s');
  $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
   $divisionCode = explode(",", $divCode);
 

    $sql = "insert into Expense_Start_Activity select '$sfCode',".$Owndiv.",'".$Datetime."',".$vals['Km'].",'".$vals['Retailerphoto']['imgurl']."' ,".$vals['Remarks'].",".$vals['MOT']."";
		      performQuery($sql);
 
 $resp["success"] = true;
 $resp["Query"] = $sql;
    echo json_encode($resp);
 die;
 break;
	case "ChangePassword": 
		
		  $newpassword=$vals['newpassword'];
		  
		    $divCode = $_GET['divisionCode'];
		   


		    $sql = "update Mas_salesforce set Sf_Password='" . $newpassword . "'  where Sf_Code= '$sfCode' and Division_Code='$divCode'";
		      performQuery($sql);
				 
               $resp["success"] = true;
                echo json_encode($resp);
                die;
		
		break;

	case "ProductAvailablity":
		
		
		  $worktype=$vals['worktype'];
		  $doctorid=$vals['doctorid'];
		  $stockist=$vals['stockist'];
		  $cluster=$vals['cluster'];
		  $entryDate=$vals['entryDate'];
		  $divCode = $_GET['divisionCode'];   
          $ProductList=$vals['ProductList'];
          $divCode = $_GET['divisionCode'];
          $divisionCode = explode(",", $divCode);  
          $today=date('Y-m-d H:i');
          $todayDCR=date('Y-m-d 00:00:00.000');
                
      $ARCd="";
      $sql = "{call  svDCRMain_App(?,?,'" . $worktype . "','" . $cluster  . "',?,'Product Availability',?,'0')}";
            $params = array(array($sfCode, SQLSRV_PARAM_IN),
                array($todayDCR, SQLSRV_PARAM_IN),
                array($divisionCode[0], SQLSRV_PARAM_IN),
                array(&$ARCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_NVARCHAR(50)));

            $tr = performQueryWP($sql, $params);
                if (sqlsrv_errors() != null) {
                    echo($sql . "<br>");
                    outputJSON($params );
                    echo( "<br>");
                    outputJSON(sqlsrv_errors());
                    die;
                }  

          $sql="exec ProductAvail_Visit  '" .  $sfCode  . "','".$divisionCode[0]."' ,'".$today." ' , '" . $cluster  . "', '" . $stockist  . "' ,'" . $worktype . "','" . $doctorid . "', '" .  $ARCd  . "'";
                   performQuery($sql); 
			
						
        for ($i = 0; $i < count($ProductList); $i++) {
		
       if($ProductList[$i]['NewCustomer']==null ||$ProductList[$i]['NewCustomer']=="undefined"){
	   $Newcustomer=0;
          }else{
	     $Newcustomer=$ProductList[$i]['NewCustomer'];
           }


       $sql = "SELECT isNull(max(Sl_No),0)+1 as Sl_No FROM Trans_Product_Availability";
            $topr = performQuery($sql);
            $pk = (int) $topr[0]['Sl_No'];
          $sql="insert into Trans_Product_Availability select '" .  $sfCode  . "','".$divisionCode[0]."'  ,'" . $doctorid . "', '" . $cluster . "' ,'" . $stockist . "', '" . $ProductList[$i]['id'] . "','" . $ProductList[$i]['name'] . "','" . $worktype . "', '".date('Y-m-d H:i')." ','" . $ProductList[$i]['Available'] . "','" . $ProductList[$i]['Visibility'] . "','" . $Newcustomer . "','" . $pk . "' ";
	
		      performQuery($sql);
          }
				 
               $resp["success"] = true;
                echo json_encode($resp);
                die;
		
		break;
        case "TP_Attendance":
			/*$myfile = fopen("../server/log_Trans_".date('Y-m-d').".txt", "a+");
			$sqlsp=$_POST['data']."|message:CIN|sf_code=".$vals['sfCode']."\n";
			fwrite($myfile, $sqlsp);
			fclose($myfile);*/
		
			if ($vals['eDate']!=null){
				$eDate=$vals['eDate'];
				$dateTime=date($eDate,'Y-m-d H:i:s');
				$date=date($eDate,'Y-m-d');
				$login_date=date($eDate,'Y-m-d');
			}else{
				$eDate=date('Y-m-d H:i');
				$dateTime=date('Y-m-d H:i');
				$date=date('Y-m-d');
				$login_date=date('Y-m-d');
			}
            $lat=$vals['lat'];
            $long=$vals['long'];
            $sfCode=$vals['sfCode'];
            $slfy=$vals['slfy'];
            $wrkType=$vals['WrkType'];
			$App_Version=$vals['App_Version'];
            $update=$_GET['update'];
			$CheckDutyFlag=$vals['CheckDutyFlag'];
			$Shift_Selected_Id=$vals['Shift_Selected_Id'];
			$PlcID=$vals['PlcID'];
			$PlcNm=$vals['PlcNm'];
			$vstRmks=$vals['vstRmks'];
			
			
			 if($vals['On_Duty_Flag']=="" || $vals['On_Duty_Flag']==null){
				  $On_Duty_Flag=0;
			 }else{
				  $On_Duty_Flag=$vals['On_Duty_Flag'];
			 }
			if($vals['HolidayFlag']==1){
				$On_Duty_Flag=1;
			}
			$HMSG="";
			if($On_Duty_Flag==1){
				$sql = "select count(sf_code) HCount from TP_Attendance_App where sf_code='$sfCode' and convert(date,Start_Time)=convert(date,'$eDate')  and Duty_Mode=0";
				$device = performQuery($sql);
				$HCount = $device[0]['HCount'];
			}else{
				$sql = "select count(sf_code) HCount from TP_Attendance_App where sf_code='$sfCode' and convert(date,Start_Time)=convert(date,'$eDate')  and Duty_Mode>0";
				$device = performQuery($sql);
				$HCount = $device[0]['HCount'];
			}
			$sql = "select isnull(sum(No_of_Days),0)Lcount from mas_Leave_Form where Sf_Code='$sfCode'  and convert(date,From_Date)=convert(date,getdate()) and Leave_Active_Flag=2 ";
            $LeaveCount = performQuery($sql);
            $Lcount = $LeaveCount[0]['Lcount'];
            if($HCount==0 && ($Lcount==0|| $Lcount==0.5 ) ){
				$flag=0;
				if($On_Duty_Flag==1){
					$mn=date('m',strtotime($eDate));
					$yr=date('Y',strtotime($eDate));
					$mDt=date('Y-m-d',strtotime($yr.'-'.$mn.'-23'));
					if(date('d',strtotime($eDate))<23) $mDt = date('Y-m-d',strtotime($mDt."- 1 months"));
					$fDt=date('Y-m-d',strtotime($mDt));
					$mDt = date('Y-m-d',strtotime($mDt."+ 1 months"));
					$tDDt=date('Y-m-d',strtotime($mDt));
					$mDt = strtotime($mDt."- 1 day");
					$tDt=date('Y-m-d',$mDt);
					$ct=0;
					$query = "select count(distinct Login_date) Cnt from TP_Attendance_App where Duty_Mode in (1,2) and SF_Code='$sfCode' and login_date between '".$fDt."' and '".$tDt."'";
					$res = performQuery($query);
					$ct=$res[0]["Cnt"];
					$query = "select count(distinct Week_date) Cnt from Trans_Weekoff where SF_Code='$sfCode' and Week_date between '".$fDt."' and '".$tDt."'";
					$res = performQuery($query); 
					$ct=$ct+$res[0]["Cnt"];
					$DyDet=mWkDysCnt($fDt,$tDDt);
					
					
					$query = "select count(distinct HolidayDate) Cnt from Mas_Holiday_Dates_Detail H inner join Mas_Salesforce S on H.stateCode=S.State_Code where sf_Code='$sfCode' and HolidayDate between '".$fDt."' and '".$tDt."'";
					$res = performQuery($query);
					$ctn=$res[0]["Cnt"];
					$ctn=$ctn+$DyDet['wkoff'];
					
					if($ct>=$ctn){
						$HMSG = "Holiday Entry Limit Exceeded.";
						$flag=1;
					}
				}
				if($flag==0){
				
					
					$sql ="exec Attendance_entry '$sfCode','$Owndiv','$eDate',$lat,$long,'$eDate','$slfy','".$wrkType."' ,'".$On_Duty_Flag."' ,'".$Shift_Selected_Id."','$App_Version','".$PlcID."','".str_replace("'","''",$PlcNm)."','".str_replace("'","''",$vstRmks)."' ";
					$result=performQuery($sql);    
					$result["qry"]=$sqlh;
				}				
				$sqlh ="insert into TP_Attendance_App_log select '$sfCode','$eDate',$lat,$long,'$slfy','".$wrkType."','".str_replace("'","''",$vstRmks)."','".$Shift_Selected_Id."','$App_Version','".$PlcID."','".str_replace("'","''",$PlcNm)."','CI'";
				$res=performQuery($sqlh);

			} else{
				$HMSG="Can't DO Check-In With Holiday Entry ";
			}

			
			if($Lcount>=1){
				$HMSG="Go to Leave Status!Cancel The Today Leave and DO Checkin.";
			}
			
           /* }
            else{
				
                $sql="select id from TP_Attendance_App where Sf_Code='$sfCode' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$date' order by id desc";
                $tr=performQuery($sql);
                $id=$tr[0]['id'];
            
                $sql="update TP_Attendance_App set End_Lat=$lat,End_Long=$long,End_Time='$eDate' where id=$id";
                performQuery($sql);
            
                $sql1="select ID from Attendance_history where Sf_Code='$sfCode' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$date' order by id desc";
                $tr1=performQuery($sql1);
                $id1=$tr1[0]['ID'];
                $sql1="update Attendance_history set End_Lat=$lat,End_Long=$long,End_Time='$eDate' where ID=$id1";
                performQuery($sql1);
                $result=[];
                $result["msg"]="1";
            }*/
		
			$result['Mssg'] = $HMSG;
			$result['Msg'] = $HMSG;
            outputJSON($result);
            die;
            break;
			
			
	
			 case "TP_Attendancee":
			/*$myfile = fopen("../server/log_Trans_".date('Y-m-d').".txt", "a+");
			$sqlsp=$_POST['data']."|message:CIN|sf_code=".$vals['sfCode']."\n";
			fwrite($myfile, $sqlsp);
			fclose($myfile);*/
	
			if ($vals['eDate']!=null){
				$eDate=$vals['eDate'];
				$dateTime=date($eDate,'Y-m-d H:i:s');
				$date=date($eDate,'Y-m-d');
				$login_date=date($eDate,'Y-m-d');
			}else{
				$eDate=date('Y-m-d H:i');
				$dateTime=date('Y-m-d H:i');
				$date=date('Y-m-d');
				$login_date=date('Y-m-d');
			}
            $lat=$vals['lat'];
            $long=$vals['long'];
            $sfCode=$vals['sfCode'];
            $slfy=$vals['slfy'];
            $wrkType=$vals['WrkType'];
			$App_Version=$vals['App_Version'];
            $update=$_GET['update'];
			$CheckDutyFlag=$vals['CheckDutyFlag'];
			$Shift_Selected_Id=$vals['Shift_Selected_Id'];
			$PlcID=$vals['PlcID'];
			$PlcNm=$vals['PlcNm'];
			$vstRmks=$vals['vstRmks'];
			
			
			 if($vals['On_Duty_Flag']=="" || $vals['On_Duty_Flag']==null){
				  $On_Duty_Flag=0;
			 }else{
				  $On_Duty_Flag=$vals['On_Duty_Flag'];
			 }
			
			
            //if($update==0){
				
                $sqlh ="insert into TP_Attendance_App_log select '$sfCode','$eDate',$lat,$long,'$slfy','".$wrkType."','".str_replace("'","''",$vstRmks)."','".$Shift_Selected_Id."','$App_Version','".$PlcID."','".str_replace("'","''",$PlcNm)."','CI'";
				$res=performQuery($sqlh);
				
                $sql ="exec Attendance_entry '$sfCode','$Owndiv','$eDate',$lat,$long,'$eDate','$slfy','".$wrkType."' ,'".$On_Duty_Flag."' ,'".$Shift_Selected_Id."','$App_Version','".$PlcID."','".str_replace("'","''",$PlcNm)."','".str_replace("'","''",$vstRmks)."' ";
                $result=performQuery($sql);    
				$result["qry"]=$sqlh;			
           /* }
            else{
				
                $sql="select id from TP_Attendance_App where Sf_Code='$sfCode' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$date' order by id desc";
                $tr=performQuery($sql);
                $id=$tr[0]['id'];
            
                $sql="update TP_Attendance_App set End_Lat=$lat,End_Long=$long,End_Time='$eDate' where id=$id";
                performQuery($sql);
            
                $sql1="select ID from Attendance_history where Sf_Code='$sfCode' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$date' order by id desc";
                $tr1=performQuery($sql1);
                $id1=$tr1[0]['ID'];
                $sql1="update Attendance_history set End_Lat=$lat,End_Long=$long,End_Time='$eDate' where ID=$id1";
                performQuery($sql1);
                $result=[];
                $result["msg"]="1";
            }*/

            outputJSON($result);
            die;
            break;
			
case "extended_entry":
			
		
			if ($vals['eDate']!=null){
				$eDate=$vals['eDate'];
				$dateTime=date($eDate,'Y-m-d H:i:s');
				$date=date($eDate,'Y-m-d');
				$login_date=date($eDate,'Y-m-d');
			}else{
				$eDate=date('Y-m-d H:i');
				$dateTime=date('Y-m-d H:i');
				$date=date('Y-m-d');
				$login_date=date('Y-m-d');
			}
            $lat=$vals['lat'];
            $long=$vals['long'];
			$Ekey=$vals['Ekey'];
            $sfCode=$vals['sfCode'];
            $slfy=$vals['slfy'];
            $wrkType=$vals['WrkType'];
			$App_Version=$vals['App_Version'];
            $update=$vals['update'];
			$CheckDutyFlag=$vals['CheckDutyFlag'];
			$Shift_Selected_Id=$vals['Shift_Selected_Id'];
			$PlcID=$vals['PlcID'];
			$PlcNm=$vals['PlcNm'];
			$vstRmks=$vals['vstRmks'];
			$result=[];/*
			$sql ="select login_date from TP_Attendance_App where sf_Code='$sfCode' and login_date='$login_date'";
			$res=performQuery($sql);
			if(count($res)<1){
                $result["msg"]="Check-In Not Done.kindly do Check-In";							
			}else{*/
                $sqlh ="insert into TP_Attendance_App_log select '$sfCode','$eDate',$lat,$long,'$slfy','".$wrkType."','".str_replace("'","''",$vstRmks)."','".$Shift_Selected_Id."','$App_Version','".$PlcID."','".str_replace("'","''",$PlcNm)."','EI'";
				$res=performQuery($sqlh);
				if($update==0){
					$sql ="exec Extend_entry '$sfCode','$Owndiv','$eDate',$lat,$long,'$eDate','$slfy','".$wrkType."' ,'0' ,'".$Shift_Selected_Id."','$App_Version','".$PlcID."','".str_replace("'","''",$PlcNm)."','".str_replace("'","''",$vstRmks)."',$Ekey ";
					$result=performQuery($sql);   

					$sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
					$device = performQuery($sql);
					$reg_id = $device[0]['DeviceRegId'];
					if (!empty($reg_id)) {
						$msg = "Extended Shift request from ".$sf_name." for approval";
						sendFCM_notify($reg_id, $msg,"Extended Shift request",1,'#sign-in');
					}	
					$result["msg"]="";					
				}
				else{
					
					
					$sql="update Extended_entry set End_Lat=$lat,End_Long=$long,End_Time='$eDate' where Ekey=$Ekey";
					performQuery($sql);				
					$result["msg"]="1";
				}
			//}
            outputJSON($result);
            die;
            break;
        case "Map_GEO_Customers":
            $vals["addrs"]="'".getaddress(str_replace("'","",$vals["lat"]),str_replace("'","",$vals["long"]))."'";
            $sql = "SELECT isNull(max(MapId),0)+1 as MapId FROM Map_GEO_Customers";
            $topr = performQuery($sql);
            $pk = (int) $topr[0]['MapId'];

            $tableData[$tableName]["MapId"] = $pk ;
            $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["MapId"];
            $sql = "insert into Map_GEO_Customers(MapId,Cust_Code,lat,long,addrs,StatFlag,Division_code) select " . $pk . "," . $vals["Cust_Code"] . "," . $vals["lat"]  . "," . $vals["long"] . "," . $vals["addrs"] . ",0,'" . $Owndiv . "'" ;
            performQueryWP($sql,[]);
            break;

case "Map_GEO_Distributor":
            $vals["addrs"]="'".getaddress(str_replace("'","",$vals["lat"]),str_replace("'","",$vals["long"]))."'";
            $sql = "SELECT isNull(max(MapId),0)+1 as MapId FROM Map_GEO_Distributors";
            $topr = performQuery($sql);
            $pk = (int) $topr[0]['MapId'];

            $tableData[$tableName]["MapId"] = $pk ;
            $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["MapId"];
            $sql = "insert into Map_GEO_Distributors(MapId,Cust_Code,lat,long,addrs,StatFlag,Division_code) select " . $pk . "," . $vals["Cust_Code"] . "," . $vals["lat"]  . "," . $vals["long"] . "," . $vals["addrs"] . ",0,'" . $Owndiv . "'" ;
            performQueryWP($sql,[]);
            break;
        case "dailyExpensee": 

$tempp = array_keys($data[1]);
$va =$data[1][$tempp[0]];
            for ($i = 0; $i < count($vals); $i++) 
            {
                    $sql = "insert into Trans_Daily_User_Expense(SF_Code,eDate,expCode,expName,Amt,Division_Code,LUpdtDate) select '" . $sfCode . "',cast(convert(varchar,getdate(),101) as datetime),'" . $vals[$i]["ID"] . "','" . $vals[$i]["Name"]  . "','" . $vals[$i]["amt"] . "','" . $Owndiv . "',getDate()" ;
                    performQueryWP($sql,[]);
            }
          

  $sql1="insert into  Trans_Expense_Traveldetails(sf_code,Division_code,Leos,Type,MOC,Remarks,FromPlace,FromDeparture,ToPlace,ToArrival,KM,Amount,Fare,Edate)values('".$sfCode."','".$Owndiv ."','" . $va["LEOS"] . "','" . $va["selectVal"] . "','" . $va["MOC"] . "'
            ,'".$va["remarks"]."' ,'".$va["FromPlace"]."','".$va["FromDeparture"]."','".$va["ToPlace"]."','".$va["ToArrival"]."', '".$va["KA"]."', '".$va["TotalKMofbike"]."' , '".$va["Fare"]."','".date('Y-m-d')."')";
        performQuery($sql1);


            break;

case "dailyExpense": 
 $dateTime=date('Y-m-d H:i');
$tempp = array_keys($data[1]);
$va =$data[1][$tempp[0]];
            for ($i = 0; $i < count($vals); $i++) 
            {
                    $sql = "insert into Trans_Daily_User_Expense(SF_Code,eDate,expCode,expName,Amt,Division_Code,LUpdtDate,Ukey,Image_Url) select '" . $sfCode . "',cast(convert(varchar,getdate(),101) as datetime),'" . $vals[$i]["ID"] . "','" . $vals[$i]["Name"]  . "','" . $vals[$i]["amt"] . "','" . $Owndiv . "',getDate(),'" . $va["Ukey"] . "' ,'" . $vals[$i]["imgData"] . "' " ;
                    performQueryWP($sql,[]);
            }
          
  $sql1="insert into  Trans_Expense_Traveldetails(sf_code,Division_code,MOC,Remarks,Start_Image,FromDeparture,End_Image,ToArrival,Date_Time,Fare,FromPlace,ToPlace,Leos,Ukey,FromPlace_Name,ToPlace_Name,Bus_Support_Doc,DateofExpense)values('".$sfCode."','".$Owndiv ."','" . $va["MOT"] . "'
            ,'".$va["remarks"]."' ,'".$va["Start_image"]."','".$va["Start_Km"]."','".$va["Stop_image"]."','".$va["Stop_km"]."', '$dateTime' ,'".$va["BusFare"]."','".$va["Workplace"]."' ,'".$va["TodayworkRoute"]."' ,'" . $va["LEOS"] . "','" . $va["Ukey"] . "','".$va["Workplace_Name"]."' ,'".$va["TodayworkName"]."' ,'".$va["EventcaptureUrl"]."','".$va["DateOfExp"]."')";
        performQuery($sql1);


 if( $data[2]['ActivityCaptures']!=null && $data[2]['ActivityCaptures']!="undefined")
            {
                $Event_Captures = $data[2]['ActivityCaptures'];
                for ($j = 0; $j < count($Event_Captures); $j++) {                       
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    $visitpath=$sfCode.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,DCRDetNo) values( '".$sfCode."','" . $visitpath . "','" . $ev_title . "','" . $ev_remarks . "','".$Owndiv."','Expense','".$dateTime."','" . $va["Ukey"] . "')";
                    performQuery($sql); 
                }
            }



            break;






        case "EA":



        $sql1="insert into  TransExpenseEntry(sf_code,Division_code,Leos,Type,MOC,Remarks)values('".$sfCode."','".$Owndiv ."','" . $vals["LEOS"] . "','" . $vals["selectVal"] . "','" . $vals["MOC"] . "'
            ,'".$vals["remarks"]."')";
        performQuery($sql1);


           break;

	

        case "tbTrackLoction":
            $sql = "select sf_emp_id,Employee_Id from Mas_Salesforce where Sf_Code='$sfCode'";
            $sf = performQuery($sql);
            $empid = $sf[0]['sf_emp_id'];
            $employeeid = $sf[0]['Employee_Id'];
            $lng = $vals['Lon'];
            $lat = $vals['Lat'];
            $address = getaddress($lat, $lng);
            $sql = "insert into tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr,Auc,deg,DvcID) select '$sfCode','$empid','$employeeid'," . $vals['DtTm'] . ",$lat,$lng,'$address','" . str_replace("'", "", $vals['Auc']) . "','" . str_replace("'", "", $vals['deg']) . "','" . str_replace("'", "", $vals['DvcID']) . "'";
            $resp["qry"] =$sql;
            performQuery($sql);
            break;
        case "expense":
            $res = $data[0]['expense'];

            $date = date('Y-m-d H:i:s');
            $update = $_GET['update'];
            $dcrdate = date('d-m-Y');
            $divCode = $_GET['divisionCode'];
            $divisionCode = explode(",", $divCode);
            $desig = $_GET['desig'];
            $sfCode = $_GET['sfCode'];
            $sfName = $res['sfName'];
            $expenseAllowance = $res['allowance'];
            $expenseDistance = $res['distance'];
            $expenseFare = $res['fare'];
            $total = $res['tot'];
            $additionalTot = $res['additionalTot'];
            $wcode = $res['worktype'];
            $wname = $res['worktype_name'];
            $place = $res['place'];
            $placeno = $res['placeno'];
            $sql = "SELECT isNull(max(Sl_No),0)+1 as RwID FROM Trans_FM_Expense_Head";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
            if ($update == 1) {
                updateEntry($sfCode);
            }

            $sql = "insert into Trans_FM_Expense_Head(Sf_Code,Month,Year,sndhqfl,Division_Code,snd_dt,Sf_Name) select '$sfCode',MONTH('$date'),YEAR('$date'),0,$divisionCode[0],'$date'," . $sfName . "";
            performQuery($sql);

            $sql = "insert into Trans_FM_Expense_Detail(DCR_Date,Expense_wtype_Code,Expense_wtype_Name,Place_of_Work,Expense_Place_No,Division_Code,Expense_Allowance,Expense_Distance,Expense_Fare,Created_Date,LastUpdt_Date,Sf_Name,Sf_Code,Expense_Total) select '$dcrdate',$wcode,$wname,$place,$placeno,$divisionCode[0],$expenseAllowance,$expenseDistance,$expenseFare,'$date','$date',$sfName,'$sfCode',$total";
            performQuery($sql);

            $sql = "SELECT * FROM Trans_Expense_Amount_Detail where Month=MONTH('$date') and year=YEAR('$date') and Sf_Code='$sfCode'";
            $tRw = performQuery($sql);
            if (empty($tRw)) {
                $additionalAmount = $additionalTot + $total;
                $sql = "insert into Trans_Expense_Amount_Detail(Sf_Code,Month,Year,Division_Code,Sf_Name,Total_Allowance,Total_Distance,Total_Fare,Total_Expense,Total_Additional_Amt,Grand_Total) select '$sfCode',MONTH('$date'),YEAR('$date'),$divisionCode[0], $sfName,$expenseAllowance,$expenseDistance,$expenseFare,$total,$additionalTot,$additionalAmount";
                performQuery($sql);
            } else {
                $totAllowance = $tRw[0]['Total_Allowance'] + $expenseAllowance;
                $totDistance = $tRw[0]['Total_Distance'] + $expenseDistance;
                $totFare = $tRw[0]['Total_Fare'] + $expenseFare;
                $totalExpense = $tRw[0]['Total_Expense'] + $total;
                $totAdditionalAmt = $tRw[0]['Total_Additional_Amt'] + $additionalTot;
                $grandTotal = $totalExpense + $totAdditionalAmt;
                $slNo = $tRw[0]['Sl_No'];
                $sql = "update Trans_Expense_Amount_Detail set Total_Allowance=$totAllowance,Total_Distance=$totDistance,Total_Fare=$totFare,Total_Expense=$totalExpense,Total_Additional_Amt=$totAdditionalAmt,Grand_Total=$grandTotal where Sl_No='$slNo'";
                performQuery($sql);
            }
            $extraDet = $res['extraDetails'];
            for ($i = 0; $i < count($extraDet); $i++) {
                $parameterName = $extraDet[$i]['parameter'];

                $amount = $extraDet[$i]['amount'];
                $type = $extraDet[$i]['type'];
                if ($type == true)
                    $type = 0;
                else
                    $type = 1;
                if (!empty($parameterName))
                    $sql = "insert into Trans_Additional_Exp(Sf_Code,Month,Year,Division_Code,Created_Date,LastUpdt_Date,Created_By,Parameter_Name,Amount,Cal_Type,Confirmed) select '$sfCode',MONTH('$date'),YEAR('$date'),$divisionCode[0],'$date','$date','$sfCode','$parameterName','$amount','$type',0";
                performQuery($sql);
            }
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "Tour_Plan":
            $divCode = $_GET['divisionCode'];

            $divisionCode = explode(",", $divCode);
            $desig = $_GET['desig'];
            //$market = $data[0]['Tour_Plan']['market'];
            $objective = $data[0]['Tour_Plan']['objective'];
            $tourDate = $data[0]['Tour_Plan']['Tour_Date'];
            $worktype_code = $data[0]['Tour_Plan']['worktype_code'];
            $worktype_name = $data[0]['Tour_Plan']['worktype_name'];
            $worked_with_code = $data[0]['Tour_Plan']['Worked_with_Code'];
            $worked_with_name = $data[0]['Tour_Plan']['Worked_with_Name'];
            $RouteCode = $data[0]['Tour_Plan']['RouteCode'];
            $RouteName = $data[0]['Tour_Plan']['RouteName'];
            $sfName = $data[0]['Tour_Plan']['sfName'];
            $HQ_Code = $data[0]['Tour_Plan']['HQ_Code'];

              $ww=$data[0]['Tour_Plan']['worked_with'];
		$RName=$data[0]['Tour_Plan']['Multiretailername'];
		$RCode=$data[0]['Tour_Plan']['MultiretailerCode'];
            $HQ_Name = str_replace(")", "", str_replace("(", "", $data[0]['Tour_Plan']['HQ_Name']));



            $sql = "delete from Trans_TP_One WHERE SF_Code ='" . $sfCode . "' and Tour_Date=cast($tourDate as datetime)";
            performQuery($sql);
            $sql = "insert into Trans_TP_One(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Tour_Schedule1,Objective,Worked_With_SF_Code,Division_Code,Territory_Code1,Worked_With_SF_Name,TP_Sf_Name,HQ_Code,HQ_Name,Confirmed,Change_Status,JointWork_Name,Retailer_Code,Retailer_Name) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),getdate(),$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,$HQ_Code,$HQ_Name,0,0,$ww,'".$RCode."','".$RName."'";
            performQuery($sql);




 $sql = "insert into Trans_TP_Hist(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Tour_Schedule1,Objective,Worked_With_SF_Code,Division_Code,Territory_Code1,Worked_With_SF_Name,TP_Sf_Name,HQ_Code,HQ_Name,Confirmed,Change_Status,JointWork_Name,Retailer_Code,Retailer_Name) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),getdate(),$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,$HQ_Code,$HQ_Name,0,0,$ww,'".$RCode."','".$RName."'";
            performQuery($sql);

            $resp["success"] = true;
           // $resp["REs"]=$sql;

            echo json_encode($resp);
            die;
            break;
        case "TourPlanSubmit":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $sql = "update Trans_TP_One set Change_Status=1,Confirmed=1 where Tour_Month=$month and Tour_Year=$year and Sf_Code='$sfCode'";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;

        case "ExpenseApproval":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $code = $_GET['code'];
        
            $sql = "update Trans_FM_Expense_Head set sndhqfl=1 where sf_Code='$code' and month=$month and year=$year";
            performQuery($sql);
          
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "ExpenseReject":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $code = $_GET['code'];
            $sql = "update Trans_FM_Expense_Head set sndhqfl=2 where sf_Code='$code' and month=$month and year=$year";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "DCRSummary":
            $data=$data[0]['DCRSummary'];
            $pcalls=$data['pcalls'];
            $upcalls=$data['upcalls'];
            $tcalls=$data['tcalls'];
            $date=date('Y-m-d');
            $DCR_TLSD=$data['DCR_TLSD'];
            $DCR_LPC=$data['DCR_LPC'];
            $brandwise=$data['brandwise'];
            $brandIds='';
            $brandNames='';
            if($brandwise!=null)
            {
                for ($j = 0; $j < count($brandwise); $j++) {
                    $brandIds = $brandIds . $brandwise[$j]["product_brd_code"] . "~" . $brandwise[$j]["RetailCount"] . "#";
                    $brandNames = $brandNames . $brandwise[$j]["product_brd_sname"] . "~" . $brandwise[$j]["RetailCount"] . "#";
                }
            }
            $sql="update dcr_summary set brand_ec='$brandNames',brand_id='$brandIds',dcr_tlsd=$DCR_TLSD,dcr_lpc=$DCR_LPC,productive_calls=$pcalls,unproductive_cals=$upcalls,total_cals=$tcalls,editFlag=1 where sf_code='$sfCode' and cast(submission_date as date)='$date'";

            performQuery($sql);
             $resp["success"] = true;
            echo json_encode($resp);
            die;
        break;
        case "TPApproval":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $code = $_GET['code'];
            $sql = "insert into Trans_TP(Division_Code,SF_Code,Worked_With_SF_Code,Worked_With_SF_Name,Tour_Date,Tour_Month,Tour_Year,WorkType_Code_B,Worktype_Name_B,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Objective,Confirmed,Confirmed_Date,Rejection_Reason,Territory_Code1,Territory_Code2,Territory_Code3,TP_Sf_Name,TP_Approval_MGR,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Submission_date,Change_Status)
                        select Division_Code,SF_Code,Worked_With_SF_Code,Worked_With_SF_Name,Tour_Date,Tour_Month,Tour_Year,WorkType_Code_B,Worktype_Name_B,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Objective,Confirmed,GETDATE(),Rejection_Reason,Territory_Code1,Territory_Code2,Territory_Code3,TP_Sf_Name,TP_Approval_MGR,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Submission_date,Change_Status
                            from Trans_TP_One where sf_Code='$code' and Tour_Month=$month and Tour_Year=$year";
            $trs = performQuery($sql);
            if (count($trs) > 0) {
                $sql = "delete from Trans_TP_One where sf_Code='$code' and Tour_Month=$month and Tour_Year=$year";
                performQuery($sql);
            }
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "TPReject":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $code = $_GET['code'];
            $sql = "insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Reject_date,Division_Code,Rejection_Reason) select '" . $code . "',$month,$year,'" . date('Y-m-d H:i') . "',$Owndiv," . $vals['reason'] . "";
            performQuery($sql);

            $sql = "update Trans_TP_One set Change_Status=2,Confirmed=0,Rejection_Reason=" . $vals['reason'] . " where Tour_Month=$month and Tour_Year=$year and Sf_Code='$code'";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "LeaveApproval":
			$leaveid = $_GET['leaveid'];
			$Mrsfcode = $_GET['sfCode'];
			$flg=0;
			
			$mod_date=date('Y-m-23');
			if(date('d')<25) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
			
			$fDt=date('Y-m-d',strtotime($mod_date));
			$sql = "select count(Leave_Id) cnt from Mas_Leave_Form where Leave_Id=$leaveid and From_Date<'$fDt'";
			$res=performQuery($sql);
			if($res[0]["cnt"]>0) $flg=8;
			/*
			$mod_date=date('Y-m-23');
			if(date('d')<25){
			$mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mod_date));
			$sql = "select count(Leave_Id) cnt from Mas_Leave_Form where Leave_Id=$leaveid and (From_Date>='$fDt' OR To_Date>='$fDt' )";
			$res=performQuery($sql);
			if($res[0]["cnt"]<1) $flg=8;
			}*/
			$query = "exec UpdLvDays '$leaveid','" . $Mrsfcode . "'";
			performQuery($query);
            //$sql = "update Mas_Leave_Form set Leave_Active_Flag=$flg,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to= where Leave_Id=$leaveid";
            //performQuery($sql);

            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Mas_leave_Form where Leave_Id='$leaveid')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Your leave has been Approved";
                sendFCM_notify($reg_id, $msg,"Leave Request",0,'#sign-in');
            }
			
            
            break;
        case "LeaveReject":
            $leaveid = $_GET['leaveid'];
           $Mrsfcode = $_GET['sfCode'];
            $sql = "update Mas_Leave_Form set Leave_Active_Flag=1,Rejected_Reason=" . $vals['reason'] . "  ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Leave_Id=$leaveid ";
            performQuery($sql);
            
			 $sql = "update Mas_Coff_Form   set Leave_Active_Flag=1,Rejected_Reason=" . $vals['reason'] . ",LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where  Leave_Parent_Id is not null and  Leave_Parent_Id=$leaveid";
			 performQuery($sql); 
			
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Mas_leave_Form where Leave_Id='$leaveid')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Leave Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Leave Request Rejected",0,'#sign-in');
            }
			break;
			
			case "DeviationApproval":
              $deviationid = $_GET['deviationid'];
              $Mrsfcode = $_GET['sfCode'];
			  
			  $sql = "update Mas_Deviation_Form set Devi_Active_Flag=0,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Deviation_Id=$deviationid";
				performQuery($sql);

            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Mas_Deviation_Form where Deviation_Id=$deviationid)";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Your Deviation has been Approved";
                sendFCM_notify($reg_id, $msg,"Leave Request",0,'#sign-in');
            }
			
            
			
            break;
			case "DeviationReject":
            $deviationid = $_GET['deviationid'];
           $Mrsfcode = $_GET['sfCode'];
            $sql = "update Mas_Deviation_Form set Devi_Active_Flag=1,Rejected_Reason=" . $vals['reason'] . "  ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Deviation_Id=$deviationid ";
            performQuery($sql);
            
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Mas_Deviation_Form where Deviation_Id=$deviationid)";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Deviation Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Deviation Request Rejected",0,'#sign-in');
            }
			break;
			case "NewjoinApproval":
             $Sl_No = $_GET['Sl_No'];
			 $Mrsfcode = $_GET['sfCode'];
			  
			  $sql = "update Trans_NewJoin set Flag=0,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where  where Sl_No=$Sl_No";
				performQuery($sql);

            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Sf_Code from Trans_NewJoin where  where Sl_No=$Sl_No)";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Your New Join has been Approved";
                sendFCM_notify($reg_id, $msg,"Leave Request",0,'#sign-in');
            }
			
            
			
            break;
			case "NewjoinReject":
            $Sl_No = $_GET['Sl_No'];
           $Mrsfcode = $_GET['sfCode'];
            $sql = "update Trans_NewJoin set Flag=1,Rejected_Reason=" . $vals['reason'] . "  ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Sl_No=$Sl_No ";
            performQuery($sql);
            
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Sf_Code from Trans_NewJoin where Sl_No=$Sl_No)";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Newjoin Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Deviation Request Rejected",0,'#sign-in');
            }
			break;
			
			
			case "ReleasedToaudit":
            $Sl_No = $_GET['Sl_No'];
           $Mrsfcode = $_GET['sfCode'];
            $sql = "update Blocked_Entry set SF_Status=1,Release_Reason=" . $vals['reason'] . "  ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Sl_No=$Sl_No ";
            performQuery($sql);
            
            /* $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Blocked_Entry where Sl_No=$Sl_No)";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Blocked Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Blocked Request Rejected",0,'#sign-in');
            } */
			break;
			
			case "RejectfromMGR":
			$Sl_No = $_GET['Sl_No'];
			$Mrsfcode = $_GET['sfCode'];
             $sql = "update Blocked_Entry set SF_Status=2,AuditReason=" . $vals['reason'] . "  ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Sl_No=$Sl_No ";
            performQuery($sql);
            
           /*  $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Blocked_Entry where Sl_No=$Sl_No)";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Deviation Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Deviation Request Rejected",0,'#sign-in');
            } */
			break;
			
			case "AuditApproved":
			$Sl_No = $_GET['Sl_No'];
			$Mrsfcode = $_GET['sfCode'];
             $sql = "update Blocked_Entry set SF_Status=3 , AudiLstupdate='" .date('Y-m-d H:i:s'). "',AuditSf_Code='" . $Mrsfcode . "' where Sl_No=$Sl_No ";
            performQuery($sql);
            
			
			$sql="select Sf_code from  Blocked_Entry where Sl_No=$Sl_No";
			$sf_codeSf=performQuery($sql);
			$sql = "update Mas_Salesforce set SF_Status=0 , LastUpdt_Date='" .date('Y-m-d H:i:s'). "'  where sf_code='".$sf_codeSf[0]["Sf_code"]."'";
            performQuery($sql);
			
           /*  $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Blocked_Entry where Sl_No=$Sl_No)";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Deviation Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Deviation Request Rejected",0,'#sign-in');
            } */
			break;
			
			
			case "Auditreject":
            $Sl_No = $_GET['Sl_No'];
			$Mrsfcode = $_GET['sfCode'];
            $sql = "update Blocked_Entry set SF_Status=2,AuditReason=" . $vals['reason'] . "  ,AudiLstupdate='" .date('Y-m-d H:i:s'). "',AuditSf_Code='" . $Mrsfcode . "' where Sl_No=$Sl_No ";
            performQuery($sql);
            
            /* $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Blocked_Entry where Sl_No=$Sl_No)";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Blocked Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Blocked Request Rejected",0,'#sign-in');
            } */
			break;
			case "LeaveCancelApproval":
              $leaveid = $_GET['leaveid'];
              $Mrsfcode = $_GET['sfCode'];
			  
			  
			/* $sql = "select * into Mas_Leave_Form_Cancel  from Mas_Leave_Form  where Leave_Id=$leaveid";
			performQuery($sql);  */
            $sql = "delete from Mas_Leave_Form  where Leave_Id=$leaveid";
            performQuery($sql); 
			$sql = "update Leave_Cancel_Form set Leavecancel_Id=0 ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Leave_Id=$leaveid ";
            performQuery($sql);
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Mas_leave_Form where Leave_Id='$leaveid')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Your Cancel leave has been Approved";
                sendFCM_notify($reg_id, $msg,"Leave Request",0,'#sign-in');
            }
			$query = "exec UpdcancelLvDays '$leaveid'";
			performQuery($query);
           
			
            break;
        case "LeaveCancelReject":
            $leaveid = $_GET['leaveid'];
           $Mrsfcode = $_GET['sfCode'];
           /*  $sql = "update Mas_Leave_Form set Leave_Active_Flag=1 ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Leave_Id=$leaveid ";
            performQuery($sql); */
			/* $sql = "select * into Mas_Leave_Form_Cancel  from Mas_Leave_Form  where Leave_Id=$leaveid";
			performQuery($sql); */
			
			
			
			$sql = "delete from Mas_Leave_Form  where Leave_Id=$leaveid";
            performQuery($sql); 
			
            $sql = "update Leave_Cancel_Form set Leavecancel_Id=1 , Rejected_Reason=" . $vals['reason'] . ", LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Leave_Id=$leaveid ";
            performQuery($sql);
			
			
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Mas_leave_Form where Leave_Id='$leaveid')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Leave Cancel Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Leave Request Rejected",0,'#sign-in');
            }
			break;



		case "PermissionApproval":

            $Sl_no = $_GET['Sl_no'];
            $Mrsfcode = $_GET['sfCode'];
			$flg=0;
			$mod_date=date('Y-m-23');
			if(date('d')<25) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mod_date));
			$sql = "select count(Sl_no) cnt from PermissionEntry where Sl_no=$Sl_no and cast(Permissiondate as datetime)<'$fDt'";
			$res=performQuery($sql);
			if($res[0]["cnt"]>0) $flg=8;
			
            $sql = "update PermissionEntry set Approval_Flag=$flg,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approvedby='" . $Mrsfcode . "' where Sl_no=$Sl_no";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from PermissionEntry where Sl_no='$Sl_no')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your Permission has been Approved";
                sendFCM_notify($reg_id, $msg,"Permission Request",0,'#sign-in');
            }
			break;

		break;
		
		
		case "PermissionReject":
            $Sl_no = $_GET['Sl_no'];
           $Mrsfcode = $_GET['sfCode'];
            $sql = "update PermissionEntry set Approval_Flag=1,Rejected_Reason=" . $vals['reason'] . "  ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approvedby='" . $Mrsfcode . "' where Sl_no=$Sl_no ";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from PermissionEntry where Sl_no='$Sl_no')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Permission Reject for " . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Permission Request Rejected",0,'#sign-in');
            }
			break;
		
		case "ondutyApproval":

            $duty_id = $_GET['duty_id'];
            $Mrsfcode = $_GET['sfCode'];
			
			$flg=2;
			$mod_date=date('Y-m-23');
			if(date('d')<25) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mod_date));
			$sql = "select count(id) cnt from TP_Attendance_App where id=$duty_id and login_date<'$fDt'";
			$res=performQuery($sql);
			if($res[0]["cnt"]>0) $flg=8;
			
            $sql = "update TP_Attendance_App set WrkType=".$flg.",Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $Mrsfcode . "' where id=$duty_id";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from TP_Attendance_App where id='$duty_id')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your On-Duty has been Approved";
                sendFCM_notify($reg_id, $msg,"ON-Duty Request",0,'#sign-in');
            }
		 break;
		 case "ondutyApprovalR":

            $duty_id = $_GET['duty_id'];
            $Mrsfcode = $_GET['sfCode'];
            $sql = "update TP_Attendance_App set WrkType=3,Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $Mrsfcode . "',Reject_reason=" . $vals['reason'] . " where id=$duty_id";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from TP_Attendance_App where id='$duty_id')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your ON-Duty has been Reject for \n" . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"ON-Duty Request",0,'#sign-in');
            }
		 break;
		  case "HolidayApprovalR":

            $duty_id = $_GET['duty_id'];
            $Mrsfcode = $_GET['sfCode'];
            $sql = "update TP_Attendance_App set Duty_Mode=3,Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $Mrsfcode . "',Reject_reason=" . $vals['reason'] . " where id=$duty_id";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from TP_Attendance_App where id='$duty_id')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your Holiday Entry has been Reject for \n" . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Holiday Entry Request",0,'#sign-in');
            }
		 break;
		 
		 case "HolidayApproval":

            $duty_id = $_GET['duty_id'];
            $Mrsfcode = $_GET['sfCode'];
			
			$flg=2;
			$mod_date=date('Y-m-23');
			if(date('d')<25) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mod_date));
			$sql = "select count(id) cnt from TP_Attendance_App where id=$duty_id and login_date<'$fDt'";
			$res=performQuery($sql);
			if($res[0]["cnt"]>0) $flg=8;
			
            $sql = "update TP_Attendance_App set Duty_Mode=".$flg.",Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $Mrsfcode . "' where id=$duty_id";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from TP_Attendance_App where id='$duty_id')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your  Holiday Entry has been Approved";
                sendFCM_notify($reg_id, $msg,"Holiday Entry Request",0,'#sign-in');
            }
		 break;
		 case "MissedApproval":

            $Sl_No = $_GET['Missed_Id'];
            $Mrsfcode = $_GET['sfCode'];
			
			$flg=2;
			$mod_date=date('Y-m-23');
			if(date('d')<25) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mod_date));
			$sql = "select count(Sl_No) cnt from Missed_punch_Entry where Sl_No=$Sl_No and Missed_punch_date<'$fDt'";
			$res=performQuery($sql);
			if($res[0]["cnt"]>0) $flg=8;
			
            $sql = "update Missed_punch_Entry set Missed_punch_Flag=".$flg.",Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $Mrsfcode . "' where Sl_No=$Sl_No";
            performQuery($sql);
			//cast(Missed_punch_date+' '+Checkout_Tme as datetime)
            $sql = "update TP_Attendance_App set End_Time=replace(Checkout_Tme,'T',' ')
			from Missed_punch_Entry where Sl_No=$Sl_No and Missed_punch_Entry.Sf_Code=TP_Attendance_App.sf_Code and End_Time is null and login_date=cast(Missed_punch_date as datetime)";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Missed_punch_Entry where Sl_No='$Sl_No')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your Missed Punch has been Approved";
                sendFCM_notify($reg_id, $msg,"Missed Punch Request",0,'#sign-in');
            }
		 break;
		 case "MissedApprovalR":

            $Sl_No = $_GET['Missed_Id'];
            $Mrsfcode = $_GET['sfCode'];
            $sql = "update Missed_punch_Entry set Missed_punch_Flag=3,Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $Mrsfcode . "',RejectReason=" . $vals['reason'] . " where Sl_No=$Sl_No";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Missed_punch_Entry where Sl_No='$Sl_No')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your Missed Punch has been Reject for \n" . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Missed Punch Request",0,'#sign-in');
            }
		 break;
		 case "ExtendedApproval":

            $Sl_No = $_GET['Extend_Id'];
			$Numbers = $_GET['Numbers'];
            $Mrsfcode = $_GET['sfCode'];
			
			$flg=2;
			$mod_date=date('Y-m-23');
			if(date('d')<25) $mod_date = date('Y-m-d',strtotime($mod_date."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mod_date));
			$sql = "select count(id) cnt from Extended_entry where id=$Sl_No and login_date<'$fDt'";
			$res=performQuery($sql);
			if($res[0]["cnt"]>0) $flg=8;
			
            $sql = "update Extended_entry set WrkType=".$flg.",Total_Numbers='" . $Numbers . "'
			,Approved_Date='" .date('Y-m-d H:i:s'). "',
			Approved_by='" . $Mrsfcode . "' where id=$Sl_No";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Extended_entry where id='$Sl_No')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your Extended Shift has been Approved";
                sendFCM_notify($reg_id, $msg,"Extended Shift Request",0,'#sign-in');
            }
		 break;
		 case "ExtendedApprovalR":

             $Sl_No = $_GET['Extend_Id'];
            $Mrsfcode = $_GET['sfCode'];
            $sql = "update Extended_entry set WrkType=3,Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $Mrsfcode . "',Reject_reason=" . $vals['reason'] . " where id=$Sl_No";
            performQuery($sql);
			
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select SF_Code from Extended_entry where id='$Sl_No')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
				$msg = "Your Extended Shift has been Reject for \n" . $vals['reason'] . "";
                sendFCM_notify($reg_id, $msg,"Extended Shift Request",0,'#sign-in');
            }
		break;
		case "LeaveFormValidate":
            $leaveid = $_GET['leaveid'];
			$Todate=str_replace("undefined-undefined-","",$vals['To_Date']);
			/* if($vals['Leave_Type']=='35'){
				$Todate=str_replace("undefined-undefined-","",$vals['To_Date']);
			}else{
				$Todate=$vals['To_Date'];
			} */
			
            $query = "exec iOS_getLvlValidate '$sfCode','" . $vals['From_Date'] . "'," . $Todate . "," . $vals['Leave_Type'] . ",'".$vals['PChk']."','".$vals['Shift']."'";
            $results = performQuery($query);
            $resp['Msg'] = $results[0]['Msg'];
            $resp["success"] = true;
            echo json_encode($resp);
			
		/*	
		$myfile = fopen("../server/TP_FormValidate.txt", "a+");
        $txt = "Vasan\n";
   
        fwrite($myfile, $query);
        fclose($myfile);*/
            die;
            break;
			
		case "PermissionFormValidate":
             
             $query = "exec iOS_getPermissionlValidate '$sfCode'," . $vals['pdate'] . ",".$vals['Noof_Count']."";
              $results = performQuery($query); 
             $resp['Msg'] = $results[0]['Msg'];
             $resp["success"] = true;
			 $resp["Query"] = $query;
            echo json_encode($resp);
            die;
                
            break;
			
			
			
			
			case "PermissionEntry":
			 $sql = "SELECT isNull(max(Sl_No),0)+1 as RwID FROM PermissionEntry";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
		    $divCode = $_GET['divisionCode'];
            $sf_name = $_GET['sf_name'];
            $divisionCode = explode(",", $divCode);
			$Owndiv = (string) $divs[0];
			$selectedTime = $vals['start_at'];
             if($vals['Shift_TimeFlag']=='1'){
				$endTime = strtotime("+60 minutes", strtotime($selectedTime));
			 }else{
				 $endTime = strtotime("+120 minutes", strtotime($selectedTime));
			 }
			
			
			$EndTimecal= date('H:i', $endTime);
			 
			 
			$query = "insert into PermissionEntry  select '".$pk."','$sfCode','".$Owndiv."','".$vals['pdate']."','".$vals['start_at']."','".$EndTimecal."','".$vals['No_of_Hrs']."','2','" . date('Y-m-d H:i') . "', '".str_replace("'","''",$vals['Reason'])."',' ','".$sf_name."','','' ";
            $results = performQuery($query); 
            
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "Permission request from ".$sf_name." for approval";
                sendFCM_notify($reg_id, $msg,"Permission request",1,'#sign-in');
            }
			$resp["Msg"] = "Permission Submitted Successfully";
            $resp["success"] = true ;
			$resp["Query"] = $query ;
            echo json_encode($resp);
            die;
                
            break;
			
			case "LeaveCancel":
			
			$Leave_Id = $_GET['Leave_Id'];
			$Reason = $_GET['Reason'];
			
			$Sf_Code = $_GET['Sf_Code'];
			 $sql = "SELECT isNull(max(Sl_No),0)+1 as RwID FROM Leave_Cancel_Form";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			$query = "select Count(Leave_Id) Cnt from Leave_Cancel_Form where Leave_Id='".$Leave_Id."'";
			$res = performQuery($query); 
			if($res[0]['Cnt']>0){
				$resp["Msg"]="Leave Cancel Date Already Submited";
            $resp["success"] = true ;
			$resp["Query"] = $query ;
			}else{
			
			$sql = "update Mas_Leave_Form set Leave_Active_Flag=3,LastUpdt_Date='" .date('Y-m-d H:i:s'). "' where Leave_Id='".$Leave_Id."'";
            performQuery($sql);
			$query = "insert into Leave_Cancel_Form  select *,'".$pk."','".date('Y-m-d H:i:s')."','2','".$Reason."' from mas_Leave_Form  where Leave_Id='".$Leave_Id."' and sf_code='$sfCode'";
            $results = performQuery($query);
			$sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                $msg = "LeaveCancel request from for approval";
                sendFCM_notify($reg_id, $msg,"Leave Cancel",1,'#sign-in');
            }
			$resp["Msg"]="Leave Cancel Submited Successfully";
            $resp["success"] = true ;
			$resp["Query"] = $query ;
			}
            echo json_encode($resp);
            die;			
			break;
			
			case "GateIn":
			$sql = "SELECT isNull(max(Sl_No),0)+1 as RwID FROM QRGateEntry";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
		    $divCode = $_GET['divisionCode'];
          
            $divisionCode = explode(",", $divCode);
			$Owndiv = (string) $divs[0];
             
			$query = "insert into QRGateEntry (Sl_No,Sf_Code,Division_Code,IHQLoc,ILocation,IlatLng,IEntryDate,IServerDate,IHQLocID) values ('".$pk."','$sfCode','".$Owndiv."','".$vals['HQLoc']."','".$vals['Location']."','".$vals['latLng']."','".$vals['time']."','".date('Y-m-d H:i:s')."','".$vals['HQLocID']."')  ";
            $results = performQuery($query); 
            
            
			$resp["Msg"] = "Gate In Submitted Successfully";
            $resp["success"] = true ;
			$resp["Query"] = $query ;
            echo json_encode($resp);
            die;
			
			case "GateOut":
			$sql = "SELECT isNull(count(Sl_No),0)+1 as RwID FROM QRGateEntry where  Sf_Code='$sfCode' and convert(date,IEntryDate)=convert(date,'".$vals['time']."')";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
		    $divCode = $_GET['divisionCode'];
            
            $divisionCode = explode(",", $divCode);
			$Owndiv = (string) $divs[0];
             
			$updatequery = "update QRGateEntry set OHQLoc='".$vals['HQLoc']."',OHLocID='".$vals['HQLocID']."', OLocation='".$vals['Location']."',OlatLng='".$vals['latLng']."',OEntryDate='".$vals['time']."',OServerDate='".date('Y-m-d H:i:s')."' where  Sf_Code='$sfCode' and convert(date,IEntryDate)=convert(date,'".$vals['time']."') and IHQLocID='".$vals['HQLocID']."'";
			$results = performQuery($updatequery);
			
			/* $query = "insert into QRGateEntry (Sl_No,Sf_Code,Division_Code,IHQLoc,ILocation,IlatLng,IEntryDate,IServerDate) values ('".$pk."','$sfCode','".$Owndiv."','".$vals['HQLoc']."','".$vals['Location']."','".$vals['latLng']."','".$vals['time']."','".date('Y-m-d H:i:s')."')  ";
            $results = performQuery($query);  */
            
			$query = "insert into QRGateEntryHistory select * from QRGateEntry where Sf_Code='$sfCode' and convert(date,IEntryDate)=convert(date,'".$vals['time']."') ";
            $results = performQuery($query);
			$resp["Msg"] = "Gate Out Submitted Successfully";
            $resp["success"] = true ;
			$resp["Query"] = $query ;
			$resp["UQuery"] = $updatequery ;
            echo json_encode($resp);
            die;
                
            break;
case "MissedPunchEntry":
			 $sql = "SELECT isNull(max(Sl_No),0)+1 as RwID FROM Missed_punch_Entry";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
		    $divCode = $_GET['divisionCode'];
            $sf_name = $_GET['sf_name'];
            $divisionCode = explode(",", $divCode);
			$Owndiv = (string) $divs[0];
			
            $mn=date('m',strtotime($vals['missed_date']));
			$yr=date('Y',strtotime($vals['missed_date']));
			$mDt=date('Y-m-d',strtotime($yr.'-'.$mn.'-23'));
			if(date('d',strtotime($vals['missed_date']))<23) $mDt = date('Y-m-d',strtotime($mDt."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mDt));
			$mDt = date('Y-m-d',strtotime($mDt."+ 1 months"));
			$tDDt=date('Y-m-d',strtotime($mDt));
			$mDt = strtotime($mDt."- 1 day");
			$tDt=date('Y-m-d',$mDt); 
			$query = "select Count(Missed_punch_date) Cnt from Missed_punch_Entry where sf_Code='$sfCode' and Missed_punch_date between '".$fDt."' and '".$tDt."' and Missed_punch_Flag!=3";
			$res = performQuery($query); 
			if($res[0]["Cnt"]>=3){
				$resp["Msg"] = "Your Missed Punch Limit Exceeded. Kindly Apply Leave" ;
			}else{
			 
				$query = "insert into Missed_punch_Entry  select '".$pk."','$sfCode','".$Owndiv."','".$sf_name."','".$vals['missed_date']."'
				,'".$vals['checkouttime']."','".str_replace("'", "''",$vals['reason'])."','0','','','" . date('Y-m-d H:i') . "',null,'".$vals['Shift_Name']."','".$vals['checkinTime']."'";
				$results = performQuery($query); 
		 
				$sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
				$device = performQuery($sql);
				$reg_id = $device[0]['DeviceRegId'];
				$resp["Msg"] = "Missed Punch Submited Successfully" ;
				if (!empty($reg_id)) {
					$msg = "Missed Punch request from ".$sf_name."\n for approval";
					sendFCM_notify($reg_id, $msg,"Missed Punch",1,'#sign-in');
				}
            }
            $resp["success"] = true ;
			$resp["Query"] = $query ;
            echo json_encode($resp);
            die;
                
            break;

		case "WeekofPunch":
			$sql = "SELECT isNull(max(Sl_No),0)+1 as RwID FROM Trans_Weekoff";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
		    $divCode = $_GET['divisionCode'];
            $sf_name = $_GET['sf_name'];
            $divisionCode = explode(",", $divCode);
			$Owndiv = (string) $divs[0];
			$mn=date('m',strtotime($vals['WKDate']));
			$yr=date('Y',strtotime($vals['WKDate']));
			$mDt=date('Y-m-d',strtotime($yr.'-'.$mn.'-23'));
			if(date('d',strtotime($vals['WKDate']))<23) $mDt = date('Y-m-d',strtotime($mDt."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mDt));
			$mDt = date('Y-m-d',strtotime($mDt."+ 1 months"));
			$tDDt=date('Y-m-d',strtotime($mDt));
			$mDt = strtotime($mDt."- 1 day");
			$tDt=date('Y-m-d',$mDt);
		
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code='$sfCode'";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
			
			$query = "select count(SF_code) Cnt from Trans_Weekoff where SF_Code='$sfCode' and Week_date='".$vals['WKDate']."'";
			$res = performQuery($query); 
            if($res[0]["Cnt"]>0){
				$resp["success"] = false;
				$resp["Msg"] = "Weeklyoff Already Submited.";
				/*if (!empty($reg_id)) {
					sendFCM_notify($reg_id, "Weeklyoff Can't Submit. ".$resp["msg"],"Notification",1,'#sign-in');
				}*/
			}
			else{	 
			
				$query="select CONVERT(varchar,Sf_Joining_Date,23)Joining_Date from Mas_Salesforce where Sf_Code='$sfCode'";
				$resdt = performQuery($query); 
				
				$ct=0;
				$query = "select count(distinct Login_date) Cnt from TP_Attendance_App where Duty_Mode in (1,2) and SF_Code='$sfCode' and login_date between '".$fDt."' and '".$tDt."'";
				$res = performQuery($query);
				$ct=$res[0]["Cnt"];
				
				$query = "select count(distinct from_Date) Cnt from Mas_Leave_Form where SF_Code='$sfCode' and Leave_type=29 and from_Date between '".$fDt."' and '".$tDt."'";
				$res = performQuery($query);
				$ct=$ct+$res[0]["Cnt"];
				
				$query = "select count(distinct HolidayDate) Cnt from Mas_Holiday_Dates_Detail H inner join Mas_Salesforce S on H.stateCode=S.State_Code where sf_Code='$sfCode' and HolidayDate between '".$fDt."' and '".$tDt."' and HolidayDate>='".date('Y-m-d',resdt[0]["Joining_Date"])."'";
				$res = performQuery($query);
				$ctn=$res[0]["Cnt"];
				
				if($ct>=$ctn){ 
					$ct=$ct-$ctn;
					$ctn=0;
				}else if($ctn>0){
					$ct=0;
				}
				
				$query = "select count(distinct Week_date) Cnt from Trans_Weekoff where SF_Code='$sfCode' and cast(Week_date as date) between '".$fDt."' and '".$tDt."'";
				$res = performQuery($query); 
				$ct=$ct+$res[0]["Cnt"];
				if(date('Y-m-d',resdt[0]["Joining_Date"])>$fDt){
					$fDt=date('Y-m-d',resdt[0]["Joining_Date"]);
				}
				$DyDet=mWkDysCnt($fDt,$tDDt);

				$ctn=$DyDet['wkoff'];
				
				if($ct>=$ctn){					
					$resp["success"] = false;
					$resp["Msg"] = "Weeklyoff Limit Exceeded.";
					$resp["Fdt"] = $fDt;
					$resp["Tdt"] = $tDDt;
					$resp["Ecnt"] =$res[0]["Cnt"];
					$resp["TCnt"] =$DyDet['wkoff'];				
					/*if (!empty($reg_id)) {
						sendFCM_notify($reg_id, "Weeklyoff Can't Submit. ".$resp["Msg"],"Notification",1,'#sign-in');
					}*/
				}else{
					$query = "select HolidayDate,HolidayName,HolidayRemarks from Mas_Holiday_Dates_Detail H inner join Mas_Salesforce S on H.stateCode=S.State_Code where sf_Code='$sfCode' and HolidayDate='".$vals['WKDate']."'";
					$res = performQuery($query); 
					if(count($res)>0){				
						$resp["success"] = false;
						$resp["Msg"] = $vals['WKDate']." is holiday date. Kindly submit holiday in leave form.";
					}
					else if(date('Y-m-d',resdt[0]["Joining_Date"])>date('Y-m-d',$vals['WKDate'])){
						$resp["success"] = false;
						$resp["Msg"] = "Week Off Cannot Be Applied For This Date";
					}
					else{
						$query = "insert into Trans_Weekoff  select '".$pk."','".$sf_name."','$sfCode','".$vals['WKDate']."','" . date('Y-m-d H:i') . "','".$Owndiv."','".str_replace("'","''",$vals['reason'])."' ";
						$results = performQuery($query); 
						$resp["success"] = true ;
					}
				}
			}
			$resp["Query"] = $query ;
            echo json_encode($resp);
            die;
                
            break;


  case "DCRMissedDates":
    $sfCode = $_GET['sfCode'];
    $div = $_GET['divisionCode'];
    $Remarks = $_GET['REmarks'];
    $Worktype = $_GET['Worktype'];
    $Typ = $_GET['desig'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $sql = "SELECT type_code,Wtype,FWFlg FROM vwMas_WorkType_App where division_code= '". $Owndiv ."' and SFTyp=(select SF_Type from Mas_Salesforce where sf_Code='".$sfCode."') and  type_code= '". $Worktype ."'";
    $RSr = performQuery($sql);
    $WNm= "";$WFlg= "";
    if(count($RSr)>0){
        $Worktype= $RSr[0]['type_code'];
        $WNm= $RSr[0]['Wtype'];
        $WFlg= $RSr[0]['FWFlg'];
    }
    else
    {
        $sql = "SELECT type_code,Wtype,FWFlg FROM vwMas_WorkType_App where division_code= '". $Owndiv ."' and SFTyp=(select SF_Type from Mas_Salesforce where sf_Code='".$sfCode."') and    Wtype= '". $Worktype ."'";
        $RSr = performQuery($sql);

        $Worktype= $RSr[0]['type_code'];
        $WNm= $RSr[0]['Wtype'];
        $WFlg= $RSr[0]['FWFlg'];
    }
    $sql = "insert into tbMyDayPlan select '" . $sfCode . "','','".$vals['misseddate']."','','".$Remarks."','" . $Owndiv . "','" . $Worktype . "','" . $WFlg . "','','','','','','','','',''";
    performQuery($sql);
    $sql = "exec  svDCRMain_App '" . $sfCode . "','".$vals['misseddate']."','" . $Worktype . "','','".$Owndiv."','".$Remarks."',''";
    performQuery($sql);
    $resp["success"] = true;
    echo json_encode($resp);
    die;
                
    break;



   case "LeaveForm":
            $name = $_GET['sf_name'];
            $divCode = $_GET['divisionCode'];
			
			$vals['To_Date']=str_replace("undefined-undefined-","",$vals['To_Date']);
            $sql = "SELECT LeaveAppproval FROM Access_Master where division_code= '". str_replace(",", "",$divCode)."'";
            $LA = performQuery($sql);
            $leaveType= $LA[0]['LeaveAppproval'];
            $lvappp=2;
            if($leaveType==1) $lvappp=0;
			$query = "select HolidayDate,HolidayName,HolidayRemarks from Mas_Holiday_Dates_Detail H inner join Mas_Salesforce S on H.stateCode=S.State_Code where sf_Code='$sfCode' and HolidayDate between '" . $vals['From_Date'] . "' and " . $vals['To_Date'] . "";
			$res = performQuery($query); 
			if(count($res)>0 && "'" . $vals['From_Date'] . "'"!=$vals['To_Date']){				
				$resp["success"] = false;
				$resp["Msg"] = "Holidays in between your applied leave period. Kindly make it separate leave.";
				echo json_encode($resp);
				die;
			}
            $sql = "SELECT isNull(max(Leave_Id),0)+1 as RwID FROM Mas_Leave_Form";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			
            $sql = "SELECT isNull(DedTyp,'2') DedTyp FROM mas_Leave_Type where Leave_code=".$vals['Leave_Type']."";
            $tLt = performQuery($sql);
            $apl =$tLt[0]['DedTyp'];
			if($apl=='2'){ 
				$lvappp=0;

				$query = "select HolidayDate,HolidayName,HolidayRemarks from Mas_Holiday_Dates_Detail H inner join Mas_Salesforce S on H.stateCode=S.State_Code where sf_Code='$sfCode' and HolidayDate='" . $vals['From_Date'] . "'";
				$res = performQuery($query); 
				if(count($res)<1){				
					$resp["success"] = false;
					$resp["Msg"] = $vals['From_Date']." is not holiday date. Kindly submit another leave type.";
					echo json_encode($resp);
					die;
				}
			}
			//if($vals['Leave_Type']==29) $lvappp=0;
			
			
			$Todate=$vals['To_Date'];
			$half_dayFlag=$vals['HalfDay'];
			if( $vals['No_of_Days']==0.5 && $vals['HalfDay']==0){
				$half_dayFlag=1;
			}
			if($vals['value']!='undefined' && $vals['value']!='')
			{	
				$sql = "SELECT isNull(max(Leave_Id),0)+1 as RwID FROM Mas_Coff_Form";
				$tRw = performQuery($sql);
				$pk1 = (int) $tRw[0]['RwID'];
				
				
				$workeddate=  date('Y-m-d', strtotime($vals['value']));
				
				$sql = "insert into Mas_Coff_Form(Leave_Id,Leave_Type,From_Date,To_Date,Reason,sf_code,Division_Code,
				Leave_Active_Flag,Created_Date,No_of_Days,Address,Half_Day_Flag,First_or_Second,Workeddate,Intime,Outime,NoofHrs,EligDys,Leave_Parent_Id) 
				select '$pk1'," . $vals['Leave_Type'] . ",'" . $vals['From_Date'] . "'," . $Todate . "," . $vals['Reason'] . ",'$sfCode','$Owndiv','".$lvappp."','" . date('Y-m-d') . "'," . $vals['No_of_Days'] . "," . $vals['address'] . "," . $half_dayFlag . "," . $vals['HalfDay_Type'] . ",'" . $workeddate . "',
				'" . $vals['Intime'] . "','" . $vals['Outime'] . "','" . $vals['NoofHrs'] . "','" . $vals['EligDys'] . "','$pk'";
				performQuery($sql);
			}
			$sql = "insert into Mas_Leave_Form(Leave_Id,Leave_Type,From_Date,To_Date,Reason,sf_code,Division_Code,
			Leave_Active_Flag,Created_Date,No_of_Days,Address,Half_Day_Flag,First_or_Second) 
			select '$pk'," . $vals['Leave_Type'] . ",'" . $vals['From_Date'] . "'," . $Todate . "," . $vals['Reason'] . ",'$sfCode','$Owndiv','".$lvappp."','" . date('Y-m-d') . "'," . $vals['No_of_Days'] . "," . $vals['address'] . "," . $half_dayFlag. "," . $vals['HalfDay_Type'] . "";
            performQuery($sql);
			/*if( $vals['No_of_Days']=='0.5' && $vals['HalfDay']=='0'){
			  $myfile = fopen("../server/NewWk.txt", "a+");
        $txt = "Vasan\n";
        fwrite($myfile, $sql);
        fclose($myfile); 
			}*/
            if($leaveType==1){
                $sql = "SELECT sf_type FROM Mas_Salesforce where Sf_Code='$sfCode'";

                $sfType = performQuery($sql);
                $days = $vals['No_of_Days'];
                $date = $vals['From_Date'];
                for ($i = 1; $i <= $days; $i++) {
                    $query = "exec ChkandPostLeaveDt 0,'$sfCode'," . $sfType[0]['sf_type'] . ",$Owndiv,'$date','','apps'";
                    $results = performQuery($query);
                    $date = date('Y-m-d', strtotime($date . ' + 1 days'));
                }

				$sql = "select a.*,b.Leave_SName from MasEntitlement  a inner join mas_Leave_Type   b ON a.LeaveCode = b.Leave_code where a.SFCode='$sfCode' and DivisionCode='". str_replace(",", "",$divCode)."' and a.LeaveCode=" . $vals['Leave_Type'] . "";
				$LeaveAvailability = performQuery($sql);
				$LAC= $LeaveAvailability[0]['LeaveAvailability']-1;
		 
				$query = "update MasEntitlement set LeaveAvailability='".$LAC."' where  SFCode='$sfCode' and  DivisionCode='". str_replace(",", "",$divCode)."'  and LeaveCode=" . $vals['Leave_Type'] . "";
				performQuery($query);
            }
			if($vals['Leave_Type']=="'25'"||$vals['Leave_Type']=="'39'"){
				$sql = "SELECT DeviceRegId FROM Access_Table where sf_code='$sfCode'";
				$device = performQuery($sql);
				$reg_id = $device[0]['DeviceRegId'];
				sendFCM_notify($reg_id,"Request to submit RT-PCR negative report to HR for joining" ,"Leave Alert",1,'#sign-in');
			}
			if($lvappp!=0){
				$sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
				$device = performQuery($sql);
				$reg_id = $device[0]['DeviceRegId'];
				
				if (!empty($reg_id)) {
					if($leaveType==1)
						$msg = "Leave Application Received";// from "+$name;
					else
						$msg = "Leave Application Received For Approval";// from "+$name;
					sendFCM_notify($reg_id, $msg,"Leave Application",1,'#sign-in');
				}
			}
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
			
			case "DeviationEntry":
			 $name = $_GET['sf_name'];
            $divCode = $_GET['divisionCode'];
			$sql = "SELECT isNull(max(Deviation_Id),0)+1 as RwID FROM Mas_Deviation_Form";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			$dt=date("Y-m-d h:i:sa");
			$sql = "exec getDeviationDates '$sfCode','" . $vals['From_Date'] . "' ,'" . $vals['Deviation_Type'] . "'";
            $Previous = performQuery($sql);
			
			if($Previous[0]["Flag"]=='0'){
			$sqld = "insert into Mas_Deviation_Form(Deviation_Id,Sf_Code,Sf_Name,Deviation_Type,Deviation_Date,MobileDate,Server_Date,LatLng,Reason,Division_Code,Devi_Active_Flag)values('$pk','$sfCode','".$name."','" . $vals['Deviation_Type'] . "','" . $vals['From_Date'] . "'," . $vals['Time'] . ",'".$dt."',".$vals['LatLng'].",'" . str_replace("'","",$vals['reason']) . "', '$Owndiv','2')";
            performQuery($sqld);
				$MSG="";
			}else{
				$MSG="Eligible not available";
				
			}
			$resp["Msg"] = $MSG;
			$resp["success"] = true;
			//$resp["Query"] = $sql;
            echo json_encode($resp);
            die;
			break;
			
			case "NewJointEntry":
			 $name = $_GET['sf_name'];
            $divCode = $_GET['divisionCode'];
			$sql = "SELECT isNull(max(Sl_No),0)+1 as RwID FROM Trans_NewJoin";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			$dt=date("Y-m-d h:i:sa");
			$sql = "insert into Trans_NewJoin(Sl_No,Sf_Code,Sf_Name,Division_Code,EntryName,JointDate,MobileDate,Server_Date,LatLng,Reason,Flag)
			values('$pk','$sfCode','".$name."','$Owndiv','" . $vals['newjoinname'] . "','" . $vals['From_Date'] . "'," . $vals['Time'] . ",'".$dt."',".$vals['LatLng']."," . $vals['reason'] . ",'2')";
            performQuery($sql);
			
			 $resp["Msg"] = "";
			 $resp["success"] = true;
			 $resp["Query"] = $sql;
            echo json_encode($resp);
            die;
			break;
        case "DCRApproval":
            $date = $_GET['date'];
            $code = $_GET['code'];
            $date = str_replace('/', '-', $date);
            $date = date('Y-m-d', strtotime($date));
            $sql = "exec ApproveDCRByDt '" . $code . "','$date'";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "DCRReject":
            $date = $_GET['date'];
            $code = $_GET['code'];
            $date = str_replace('/', '-', $date);
            $date = date('Y-m-d', strtotime($date));
            $sql = "update DCRMain_Temp set Confirmed=2,ReasonforRejection=" . $vals['reason'] . " where Sf_Code='$code' and Activity_Date='$date'";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "OrderReturn":
            $sql = "SELECT isNull(max(Trans_SlNo),0)+1 as RwID FROM SalesReturn_Head";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
            $sql = "insert into SalesReturn_Head select ".$pk.",".$pk.",'" . $sfCode . "','" . $vals["StkId"] . "','" . $vals["DrId"] . "','" . date('Y-m-d H:i:s') . "','0',0";
            performQuery($sql);
            $Clim="N";
            $tVal=0;
            for($il=0;$il<count($vals["RetProducts"]);$il++)
            {
                $PDets=$vals["RetProducts"][$il];

                $sql = "SELECT Division_Code FROM Mas_Product_Detail where Product_Detail_Code='" . $PDets["id"] . "'";
                $tRw = performQuery($sql);
                $Divc = (int) $tRw[0]['Division_Code'];

                if($PDets["Qty"]=="" || $PDets["Qty"]==null) $PDets["Qty"]=0;
                if($PDets["PQty"]=="" || $PDets["PQty"]==null) $PDets["PQty"]=0;

                if($PDets["QType"]=="" || $PDets["QType"]==null) $PDets["QType"]="D";
                if($PDets["ClaimType"]=="" || $PDets["ClaimType"]==null) $PDets["ClaimType"]="N";
                if($PDets["remark"]=="" || $PDets["remark"]==null) $PDets["remark"]="";
   if($PDets["S_name"]=="" || $PDets["S_name"]==null) $PDets["S_name"]="";

if($PDets["damagebatch"]=="" || $PDets["damagebatch"]==null) $PDets["damagebatch"]="";
if($PDets["damageorderdate"]=="" || $PDets["damageorderdate"]==null) $PDets["damageorderdate"]="";

                
                $sql = "insert into SalesReturn_Detail select ".$pk.",m.Product_Detail_Code," . $PDets["Qty"] . "," . 
                        $PDets["PQty"] . ",Sample_Erp_Code,isnull(RetailCasePrice,0)RCP,isnull(Retailor_Price,0) RPP,isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0) DPP ".
                        ",'" . $PDets["S_name"] . "','" . $PDets["ClaimType"] . "','" . $PDets["remark"] . "','" . $PDets["damagebatch"] . "' ,'" . $PDets["damageorderdate"] . "' , '" . $PDets["QType"] . "' from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code ".
                        " and v.State_Code=".$SFState." Where m.Product_Detail_Code='" . $PDets["id"] . "'";


                performQuery($sql);

                $rQty=$PDets["Qty"];
                $rPQty=$PDets["PQty"];
                
                if($PDets["Qty"]=="" || $PDets["Qty"]==null) $rQty=0;
                if($PDets["PQty"]=="" || $PDets["PQty"]==null) $rPQty=0;

                $sql = "select (" . $PDets["Qty"] . "*isnull(Sample_Erp_Code,1))+" . $PDets["PQty"] . " PicQty from Mas_Product_Detail m Where m.Product_Detail_Code='" . $PDets["id"] . "'";
                $ar = performQuery($sql);
                $Qtys=$ar[0]["PicQty"];

                //$Qtys=($rQty*$PDets["conversionQty"])+$rPQty;
                $sql = "exec UpdRetReturnStock '".$pk."','" . $vals["StkId"] . "','" . $PDets["id"] . "','" . date('Y-m-d H:i:s') . "','".$PDets["QType"]."'," . $Qtys . ",".$Divc.",'',''";

                performQueryWP($sql,[]);
                
                if($PDets["ClaimType"]=="Y"){
                    $sql = "select " . $Qtys . "*isnull(cast(Retailor_Price as float),0) Val ".
                        " from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code ".
                        " and v.State_Code=".$SFState." Where m.Product_Detail_Code='" . $PDets["id"] . "'";

                    performQuery($sql);

                    $oval = performQuery($sql);
                    $tVal = $tVal+$oval[0]['Val'];
                    $Clim="Y";
                }
            }
            if($Clim=="Y"){
                $sql = "insert into Customer_Ledger_Details select '" . $vals["DrId"] . "',".$tVal.",'C',getDate(),'Return',''";
                performQuery($sql);
            }

            break;
        case "DailyInventory":
            $mods=$vals["svMode"];
            $tblnm="Trans_Prod_DailyInventory";
			$SFCd=$vals["SF"];
			if($vals["SF"]=="" || $vals["SF"]==null)
				$SFCd=$vals["sfCode"];
            if($mods=="26"){
                $tblnm="Trans_Prod_DailyEndInventory";
				
				
				$query = "select * from Trans_VanStock_Ledger where from_loc='" . $SFCd . "'  and convert(date,eDate)='" . date('Y-m-d 00:00:00') . "'    and EntryBy='DayEndInv'";
        $DEI = performQuery($query);
			
        if (count($DEI) > 0) {
		
		   $result = array();
            $result['Msg'] = 'Daily End Inventory Already Submitted';
            $result["success"] = true;
            outputJSON($result);
            die; 	
			
			
		}
		 
            }

            $SFCd=$vals["SF"];
            

            $sql = "delete FROM ". $tblnm ." where SFCode='" . $SFCd . "' and EntryDt='" . date('Y-m-d 00:00:00') . "'";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];

 


            for($il=0;$il<count($vals["InvProducts"]);$il++)
            {

                $PDets=$vals["InvProducts"][$il];

                $sql = "SELECT Division_Code FROM Mas_Product_Detail where Product_Detail_Code='" . $PDets["id"] . "'";
                $tRw = performQuery($sql);
                $Divc = (int) $tRw[0]['Division_Code'];

                $sql = "SELECT isNull(max(SLNo),0)+1 as RwID FROM ". $tblnm;
                $tRw = performQuery($sql);
                $pk = (int) $tRw[0]['RwID'];

                if($PDets["Totalqty"]=="") $PDets["Totalqty"]=0;
				 if($PDets["Qty"]=="") $PDets["Qty"]=0;
                if($PDets["PQty"]=="") $PDets["PQty"]=0;
                if($vals["AprlFlag"]=="" || $vals["AprlFlag"]==null) $vals["AprlFlag"]=0;


                $sql = "insert into ". $tblnm ." select ".$pk.",'" . $SFCd . "','" . date('Y-m-d 00:00:00') . "',m.Product_Detail_Code," . $PDets["Qty"] . "," . 
                            $PDets["PQty"] . ",Sample_Erp_Code,isnull(RetailCasePrice,0)RCP,isnull(Retailor_Price,0) RPP,isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0) DPP,'1','" . $vals["StkId"] . "','" . $vals["StkNm"] . "'".
                            "," . $PDets["Totalqty"] . " from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code ".
                            " and v.State_Code=".$SFState." Where m.Product_Detail_Code='" . $PDets["id"] . "'";
                performQuery($sql);

                if($mods=="27"){
                    $sql = "select (" . $PDets["Qty"] . "*isnull(Sample_Erp_Code,1))+" . $PDets["PQty"] . " PicQty from Mas_Product_Detail m Where m.Product_Detail_Code='" . $PDets["id"] . "'";
                    $ar = performQuery($sql);
                    $Qtys=$ar[0]["PicQty"];
    
                    $sql = "exec UpdVanStock '".$pk."','" . $vals["StkId"] . "','D','" . $SFCd . "','F','" . date('Y-m-d 00:00:00') . "','" . $PDets["id"] . "','G'," . $Qtys . ",".$Divc.",'" . $vals["StkNm"] . "',''";
                    performQueryWP($sql,[]);
					
					
					
                }
                if($mods=="26"){
                    $sql = "select (" . $PDets["Qty"] . "*isnull(Sample_Erp_Code,1))+" . $PDets["PQty"] . " PicQty from Mas_Product_Detail m Where m.Product_Detail_Code='" . $PDets["id"] . "'";
                    $ar = performQuery($sql);
                    $Qtys=$ar[0]["PicQty"];
    
                    $sql = "exec UpdVanCLStock '".$pk."','" . $SFCd . "','F','" . $vals["StkId"] . "','D','" . date('Y-m-d 00:00:00') . "','" . $PDets["id"] . "','G'," . $Qtys . ",".$Divc.",'" . $vals["StkNm"] . "',''";
                    performQueryWP($sql,[]);
                }

            }
            if($mods=="24"){
                $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='" . $SFCd . "')";
                $device = performQuery($sql);
                $reg_id = $device[0]['DeviceRegId'];
                if (!empty($reg_id)) {
                    $msg = "Daily Inventory Approval Received";
                    sendFCM_notify($reg_id, $msg,"Daily Inventory",0,'#sign-in');
                }
            }
            if($mods=="27"){
                $sql = "SELECT DeviceRegId FROM Access_Table where sf_code='" . $SFCd . "'";
                $device = performQuery($sql);
                $reg_id = $device[0]['DeviceRegId'];
                if (!empty($reg_id)) {
                    //   $msg = $name . " Applied Leave for " . $vals['No_of_Days'] . " days";
                    $msg = "Your Daily Inventory Approved...";
                    sendFCM_notify($reg_id, $msg,"Daily Inventory",0,'#sign-in');
                }
            }
			
			
            break;
        case "InvoiceEntry":
            $SFCd = $_GET['sfCode'];

            $PDet="<ROOT>";
            $TaxDet="<ROOT>";
            for($il=0;$il<count($vals["productSelectedList"]);$il++)
            {
                $PDets=$vals["productSelectedList"][$il];
                $PDet=$PDet."<Prod PCode=\"".$PDets["product"]."\" PName=\"".$PDets["product_Nm"]."\" PRate=\"".$PDets["Rate"]."\" Disc=\"".$PDets["discount"]."\" Free=\"".$PDets["free"]."\" PQty=\"".$PDets["rx_qty"]."\" PValue=\"".$PDets["sample_qty"]."\" />";
                $TxDet=$PDets["taxDet"];
                for($ij=0;$ij<count($TxDet);$ij++)
                {
                    $TaxDet=$TaxDet."<TaxDet TaxId=\"".$TxDet[$ij]["txId"]."\" TaxName=\"".$TxDet[$ij]["txName"]."\" TaxAmt=\"".$TxDet[$ij]["txAmt"]."\" PCode=\"". $PDets["product"] ."\" />";
                    
                }
            }
            $PDet=$PDet."</ROOT>";
            $TaxDet=$TaxDet."</ROOT>";
            $pk = "0";

            $sql = "{call svVanInvoice(?,'" . $SFCd . "','" . $vals["stockist_code"] . "','" . $vals["stockist_name"] . "','" . $vals["id"] . "','" . $vals["name"] . "','" . $vals["vtime"] . "','" . $vals["BillDt"] . "','" . $vals["BillAmt"] . "','" . $vals["TaxAmt"] . "','" . $vals["Disc"] . "','" . $vals["NetAmt"] . "','','".$PDet."','".$TaxDet."','".$vals["Order_No"] ."')}";
            $params = array(array(&$pk, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
            performQueryWP($sql, $params);

                   /* echo($sql . "<br>");
                    outputJSON($params );*/
            for($il=0;$il<count($vals["productSelectedList"]);$il++)
            {
                $PDets=$vals["productSelectedList"][$il];

                $sql = "SELECT Division_Code FROM Mas_Product_Detail where Product_Detail_Code='" . $PDets["product"] . "'";
                $tRw = performQuery($sql);
                $Divc = (int) $tRw[0]['Division_Code'];

                $Qtys =$PDets["rx_qty"];
                $sql = "exec [UpdVanInvStock] '".$pk."','" . $SFCd . "','F','" . $vals["id"] . "','R','" . date('Y-m-d 00:00:00') . "','" . $PDets["product"] . "','G'," . $Qtys . ",".$Divc.",'','" . $vals["name"] . "'";
                performQueryWP($sql,[]);

                $FQtys =$PDets["free"];
                if($FQtys>0){
                    $sql = "exec [UpdVanInvStock] '".$pk."','" . $SFCd . "','F','" . $vals["id"] . "','R','" . date('Y-m-d 00:00:00') . "','" . $PDets["product"] . "','G'," . $FQtys . ",".$Divc.",'','" . $vals["name"] . " -- FREE'";
                    performQueryWP($sql,[]);
                }
            }
            

            $result = array();
            $result['InvNo'] = $pk;
            $result["success"] = true;
            outputJSON($result);
            die;   
            break;
            
            
            
            
               case "CallPreviewEkey":
                
                
                echo $dataqtwtwetwetewt[0];
        $data1 = json_decode($_POST['data'], true);
          $sfCode = $_GET['sfCode'];
          $divCode = $_GET['divisionCode'];
             $CallPreviewEkey=$data[0]['CallPreviewEkey'];
          
    $result = array();
             $query = "select Trans_Sl_No,(Order_Value+isnull(discount_price,0)) Order_Value ,Order_Value netamount  from Trans_Order_Head where  OrderID='".$CallPreviewEkey."'  union  select Trans_Sl_No,Order_Value,0 from Trans_PriOrder_Head where  OrderID='".$CallPreviewEkey."' union select Trans_Sl_No,Order_Value,0 from Trans_Pragnancy__Head where  OrderID='".$CallPreviewEkey."' ";
           
$Output=performQuery($query);
             
          $result['Output']=$Output; 
           $result['success']=true; 
               outputJSON($result);
die;   
 

        break;
            
            
            
            
            
            
            
        case "DOOR_TO_DOOR":
            $divCode = $_GET['divisionCode'];
            $State_Code = $_GET['State_Code'];
            $PERSIONID=1;
            $Current_Date_And_Time =date("Y-m-d");
            $FWFlg=$data[2]['FWFlg'];
            $Remarks=$data[1]['Remarks'];
            $copen=$data[3]['No0fcoupen'];
            $Retailer=$data[4]['Retailer'];
            $Distributors=$data[5]['Distributors'];
            $Route=$data[6]['Route'];
             
             
            if( $data[count($data)-1]['ActivityCaptures']!=null && $data[count($data)-1]['ActivityCaptures']!="undefined")
            {
                $Event_Captures = $data[count($data)-1]['ActivityCaptures'];
                for ($j = 0; $j < count($Event_Captures); $j++) {                       
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time) values( '".$State_Code."','".$sfCode."','" . $ev_imgurl . "','" . $ev_title . "','" . $ev_remarks . "','". str_replace(",", "",$divCode)."','DTD','".$Current_Date_And_Time."')";
                    performQuery($sql); 
                }
            }
 
            for ($il = 0;$il < count($data[0]['DOOR_TO_DOOR']);$il++) {
                $Pendingbills = $data[0]['DOOR_TO_DOOR'][$il];
                $PromotorName = str_replace("'", "", $Pendingbills["PromotorName"]);
                 $Place = str_replace("'", "", $Pendingbills["Place"]);

                $Taken = str_replace("'", "", $Pendingbills["Taken"]);
                $Issue = str_replace("'", "", $Pendingbills["Issue"]);
                $Stime = str_replace("'", "", $Pendingbills["Stime"]);
                $Etime = str_replace("'", "", $Pendingbills["Etime"]);
                
                
                $sql="insert into Trans_Door_To_Door (PersonID,sfCode, SfName, State_Code, Division_Code, Promotorname, place, taken, issue,starttime,endtime,Remarks,Retailer,Current_Date_And_Time,Distributors,Route)
                        values (  '".$copen."', '".$sfCode."','".$FWFlg."', '".$State_Code."','". str_replace(",", "",$divCode)."','".$PromotorName."','".$Place."','".$Taken."','".$Issue."','".$Stime."','".$Etime."','".$Remarks."','".$Retailer."','".$Current_Date_And_Time."','".$Distributors."','".$Route."')";
                performQuery($sql);
                 
            }
            break;
        case "Inshop_Activity":
            $divCode = $_GET['divisionCode'];
            $State_Code = $_GET['State_Code'];
            $FWFlg=$data[2]['FWFlg'];
            $Remarks=$data[1]['Remarks'];
            $Current_Date_And_Time =date("Y-m-d");
            $Current_Date_And_only =date("Y-m-d");
            $retailername=$data[5]['RetailerName'];
            $Distributors=$data[6]['Distributors'];
            $Route=$data[7]['Route'];
            $Identification='Inshop_Activity';
            if( $data[3]['ActivityCaptures']!=null && $data[3]['ActivityCaptures']!="undefined")
            {
                $Event_Captures = $data[3]['ActivityCaptures'];
                for ($j = 0; $j < count($Event_Captures); $j++) {                       
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time) values( '" . $sfCode . "','" . $State_Code . "','" . $ev_imgurl . "','" . $ev_title . "','" . $ev_remarks . "','". str_replace(",", "",$divCode)."','" . $Identification . "','" . $Current_Date_And_Time . "')";
                    performQuery($sql); 
                }
            }

            for ($il = 0;$il < count($data[0]['Inshop_Activity']);$il++) {
                $Pendingbills = $data[0]['Inshop_Activity'][$il];
                $PromotorName = str_replace("'", "", $Pendingbills["PromotorName"]);
                 
                $Issue = str_replace("'", "", $Pendingbills["Value"]);
                $Stime = str_replace("'", "", $Pendingbills["Stime"]);
                $Etime = str_replace("'", "", $Pendingbills["Etime"]);
                
                $sql="insert into Trans_Inshop_Activity (sfCode, SfName, State_Code, Division_Code, Promotorname,issue,starttime,endtime,Remarks,Retailer_Name,Distributors,Route,InsertDate)
                        values (  '".$sfCode."','".$FWFlg."', '".$State_Code."','". str_replace(",", "",$divCode)."','".$PromotorName."','".$Issue."','".$Stime."','".$Etime."','".$Remarks."','".$retailername."','".$Distributors."','".$Route."','" . $Current_Date_And_only . "')";
                performQuery($sql);
                 
            }
              
            for ($il = 0;$il < count($data[4]['Inshop_Activity_Orders']);$il++) {
                $Inshop_Activity_Orders = $data[4]['Inshop_Activity_Orders'][$il];
                $product_code = str_replace("'", "", $Inshop_Activity_Orders["product_code"]);
                 
                $Product_Rx_Qty = str_replace("'", "", $Inshop_Activity_Orders["Product_Rx_Qty"]);
                $Product_Sample_Qty = str_replace("'", "", $Inshop_Activity_Orders["Product_Sample_Qty"]);
                $Rate = str_replace("'", "", $Inshop_Activity_Orders["Rate"]);
                
                
                $sql="insert into Trans_Inshop_Activity_Orders (sfCode, SfName, State_Code, Division_Code, product_code,Product_Rx_Qty,Product_Sample_Qty,Rate,Insert_Date_And_time,Retailer_Name)
                        values (  '".$sfCode."','".$FWFlg."', '".$State_Code."','". str_replace(",", "",$divCode)."','".$product_code."','".$Product_Rx_Qty."','".$Product_Sample_Qty."','".$Rate."','" . $Current_Date_And_Time . "','" . $retailername . "')";
                performQuery($sql);
            }
            break;
            
                case "Supplier_Master":
                        $divCode = $_GET['divisionCode'];
                        $State_Code = $_GET['State_Code'];
                    
                        $Remarks=$data[1]['Remarks'];
                        $Current_Date_And_Time =date("Y-m-d");
                       
                        $SupplierName=$data[3]['SupplierName'];
                        
                        $Route=$data[4]['Route'];
                       
                        if (!$data[2]['Supplier_Master_Orders']) {
                 $sql="insert into SupplierMaster_Order (sfCode, State_Code, Division_Code,Insert_Date_And_time,Suppliername,Route,Remarks)
                                    values (  '".$sfCode."', '".$State_Code."','". str_replace(",", "",$divCode)."','".$Current_Date_And_Time."','".$SupplierName."','".$Route."','".$Remarks."')";
                            performQuery($sql);
            
        
            } else {
                for ($il = 0;$il < count($data[2]['Supplier_Master_Orders']);$il++) {
                            $Supplier_Master_Orders = $data[2]['Supplier_Master_Orders'][$il];
                            $product_code = str_replace("'", "", $Supplier_Master_Orders["product_code"]);
                             
                            $Product_Rx_Qty = str_replace("'", "", $Supplier_Master_Orders["Product_Rx_Qty"]);
                            $Product_Sample_Qty = str_replace("'", "", $Supplier_Master_Orders["Product_Sample_Qty"]);
                            $Rate = str_replace("'", "", $Supplier_Master_Orders["Rate"]);
                            
                            
                            $sql="insert into SupplierMaster_Order (sfCode, State_Code, Division_Code, product_code,Product_Rx_Qty,Product_Sample_Qty,Rate,Insert_Date_And_time,Suppliername,Route,Remarks)
                                    values (  '".$sfCode."', '".$State_Code."','". str_replace(",", "",$divCode)."','".$product_code."','".$Product_Rx_Qty."','".$Product_Sample_Qty."','".$Rate."','" . $Current_Date_And_Time . "','".$SupplierName."','".$Route."','".$Remarks."')";
                            performQuery($sql);
        
                        }
            }
                          
                        
                        break;
            
            case "Inshop_Activity_CheckIn":
            $divCode = $_GET['divisionCode'];
            $State_Code = $_GET['State_Code'];
            $FWFlg=$data[2]['FWFlg'];
            $Remarks=$data[1]['Remarks'];
            $Current_Date_And_only =date("Y-m-d");
            $retailername=$data[5]['RetailerName'];
            $Distributors=$data[6]['Distributors'];
            $Route=$data[7]['Route'];
            $Identification='Inshop_Activity_CheckIn';
            if( $data[3]['ActivityCaptures']!=null && $data[3]['ActivityCaptures']!="undefined")
            {
                $Event_Captures = $data[3]['ActivityCaptures'];
                for ($j = 0; $j < count($Event_Captures); $j++) {                       
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time) values( '" . $sfCode . "','" . $State_Code . "','" . $ev_imgurl . "','" . $ev_title . "','" . $ev_remarks . "','". str_replace(",", "",$divCode)."','" . $Identification . "','" . $Current_Date_And_Time . "')";
                    performQuery($sql); 
                }
            }

            for ($il = 0;$il < count($data[0]['Inshop_Activity_CheckIn']);$il++) {
                $Pendingbills = $data[0]['Inshop_Activity_CheckIn'][$il];
               
                 
                
                $Stime = str_replace("'", "", $Pendingbills["Stime"]);
                $Etime = str_replace("'", "", $Pendingbills["Etime"]);
                
                $sql="insert into Trans_Inshop_Activity_CheckIn (sfCode, SfName, State_Code, Division_Code,starttime,endtime,Remarks,Retailer_Name,Distributors,Route,InsertDate)
                        values (  '".$sfCode."','".$FWFlg."', '".$State_Code."','". str_replace(",", "",$divCode)."','".$Stime."','".$Etime."','".$Remarks."','".$retailername."','".$Distributors."','".$Route."','" . $Current_Date_And_only . "')";
                performQuery($sql);
                 
            }
              
            for ($il = 0;$il < count($data[4]['Inshop_Activity_Orders']);$il++) {
                $Inshop_Activity_Orders = $data[4]['Inshop_Activity_Orders'][$il];
                $product_code = str_replace("'", "", $Inshop_Activity_Orders["product_code"]);
                 
                $cb_qty = str_replace("'", "", $Inshop_Activity_Orders["cb_qty"]);
                $Product_Sample_Qty = str_replace("'", "", $Inshop_Activity_Orders["Product_Sample_Qty"]);
                $Rate = str_replace("'", "", $Inshop_Activity_Orders["Rate"]);
                
                
                $sql="insert into Trans_Inshop_CheckIn_Orders (sfCode, SfName, State_Code, Division_Code, product_code,Product_Rx_Qty,Product_Sample_Qty,Rate,Insert_Date_And_time,Retailer_Name)
                        values (  '".$sfCode."','".$FWFlg."', '".$State_Code."','". str_replace(",", "",$divCode)."','".$product_code."','".$cb_qty."','".$Product_Sample_Qty."','".$Rate."','" . $Current_Date_And_Time . "','" . $retailername . "')";
                performQuery($sql);
            }
            break;
        case "NewContact":
             $divCode = $_GET['divisionCode'];
            


            $State_Code = $_GET['State_Code'];
             
            $FormarName=$data[1]['FormarName'];
            $ContactNumber=$data[2]['ContactNumber'];
            $Remarks=$data[3]['Remarks'];
            $Seeman=$data[4]['Seeman'];
            $Address=$data[5]['Address'];
            $Catogoryformer=$data[6]['Catogoryformer'];
            $Crop=$data[7]['Crop'];
            $Route=$data[8]['Route'];
            $Breed=$data[9]['Breed'];
            $Class=$data[10]['Class'];
            $Ukey=$data[11]['Ukey'];
            $unlisted_cat_code="null";

      $temp = array_keys($data[1]);
   
    $Field = $data[1][$temp[0]];




            $Current_Date_And_Time =date("Y-m-d");

            
           

$sql = "{call  svListedDR_APP('$sfCode','".$Field["FormarName"]."','".$Field["Address"]."','" . $Field["ContactNumber"] . "','".$Field["CityName"]."','".$Field["AreaName"]."','', '','','',".$Field["Catogoryformer"].",".$unlisted_cat_code.",'".$Field["Route"]."','0','" . date('Y-m-d H:i:s') . "',". str_replace(",", "",$divCode)."," .$Field["Class"]. ",0,'','','','','','','','".$Field["Landmark"]."','" . str_replace("'","",$Field["Ukey"]) . "','".$Field["Seeman"]."','".$Field["Breed"]."','".$Field["Crop"]."')}";
performQueryWP($sql,[]);

$sql = "SELECT ListedDrCode from Mas_ListedDr where Ukey=".$Field["Ukey"];
                    $res = performQuery($sql);
                    if (count($res) > 0){
                        $cCode=$res[0]["ListedDrCode"];
                    }
	$sql="insert into NewContact_Dr (ListedDrCode,Ukey,FormarName, DFDairyMP, MonthlyAI,AITCU,AITMP,MCCNFPM,MCCMilkColDaily,VetsMP,CreatedDate,CustomerCategory,PotentialFSD,CurrentlyUFSD,FrequencyOfVisit)
                            values ('".$cCode."','" . str_replace("'","",$Field["Ukey"]) . "', '".$Field["FormarName"]."','". $Field["DFDairyMP"]."','". $Field["MonthlyAI"]."','".$Field["AITCU"]."', '".$Field["AITMP"]."', '".$Field["MCCNFPM"]."','".$Field["MCCMilkColDaily"]."','".$Field["VetsMP"]."',   '" . date('Y-m-d H:i:s') . "',  '".$Field["Catogoryformer"]."','".$Field["DFPFSDM"]."','".$Field["CUFSD"]."','".$Field["FrequencyOfVisit"]."')";
 performQuery($sql);


           /* if($data[2]['GIFTCARD']!=1){
                    


                    $sql="insert into Trans_Gift_Issue_Details (Sf_code,Division_Code, Name, Ukey,Additional_Prod_Dtls,Created_Date)
                            values ('$sfCode',  ". str_replace(",", "",$divCode).",'". $Field["FormarName"]."','" . str_replace("'","",$Field["Ukey"]) . "','".$data[2]['GIFTCARD']."','" . date('Y-m-d H:i:s') . "')";
                    performQuery($sql);
}*/
            
            break;

    case "FieldDemo_Activity_Orders":
            $divCode = $_GET['divisionCode'];
            
            $State_Code = $_GET['State_Code'];
             
            $FormarName=$data[1]['FormarName'];
            $ContactNumber=$data[2]['ContactNumber'];
            $Remarks=$data[3]['Remarks'];
            $Current_Date_And_Time =date("Y-m-d");

            $sql = "SELECT isNull(max(ID),0)+1 as RwID FROM Trans_Demo_Head";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID']; 


            $sql="insert into Trans_Demo_Head(ID,sf_code, division_code,formarname,contactnumber,remarks,current_dateand_time)
                values ('".$pk."','".$sfCode."','". str_replace(",", "",$divCode)."','".$FormarName."','".$ContactNumber."','".$Remarks."','".$Current_Date_And_Time."')";
            performQuery($sql);
            
            for ($il = 0;$il < count($data[0]['FieldDemo_Activity_Orders']);$il++) {
                $FieldDemo_Activity_Orders = $data[0]['FieldDemo_Activity_Orders'][$il];
                $product_code = str_replace("'", "", $FieldDemo_Activity_Orders["product_code"]);
                $Product_Rx_Qty = str_replace("'", "", $FieldDemo_Activity_Orders["Product_Rx_Qty"]);
                $Rate = str_replace("'", "", $FieldDemo_Activity_Orders["Rate"]);
                
                
                $sql="insert into Trans_Demo_Samples (TransID,sf_code, division_code, productcode,qty,current_dateand_time)
                        values ('".$pk."',  '".$sfCode."','". str_replace(",", "",$divCode)."','".$product_code."','".$Product_Rx_Qty."','" . $Current_Date_And_Time . "')";
                performQuery($sql);
            }
            break;




        case "BatteryStatus":
            $divCode = $_GET['divisionCode'];
            $State_Code = $_GET['State_Code'];
            $Current_Date_Time=date("Y-m-d h:i:sa");
            $BatteryStatus=$data[0]['BatteryStatus'];
            $sql="insert into Trans_Battery_Status (sf_code,Div_Code,DateandTime,Battery_Status) values ('".$sfCode."','". str_replace(",", "",$divCode)."','".$Current_Date_Time."' ,'".$BatteryStatus."' )";
            performQuery($sql);
            break;
        case "Product_Display_Activity":
            $divCode = $_GET['divisionCode'];
            $State_Code = $_GET['State_Code'];
            $Current_Date_And_Time =date("Y-m-d");
            $FWFlg=$data[2]['FWFlg'];
            $Remarks=$data[1]['Remarks'];
            $copen=$data[3]['No0fcoupen'];
            $Retailer=$data[4]['Retailer'];
            $Distributors=$data[5]['Distributors'];
            $Route=$data[6]['Route'];
            $FieldForceName=$data[7]['FieldForceName'];
            $HQSfCode=$data[8]['HQSfCode'];
            if( $data[3]['ActivityCaptures']!=null && $data[3]['ActivityCaptures']!="undefined")
            {
                $Event_Captures = $data[3]['ActivityCaptures'];
                for ($j = 0; $j < count($Event_Captures); $j++) {                       
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time) values( '".$State_Code."','".$sfCode."','" . $ev_imgurl . "','" . $ev_title . "','" . $ev_remarks . "','". str_replace(",", "",$divCode)."','PD','".$Current_Date_And_Time."')";
                    performQuery($sql); 
                }
            }

            for ($il = 0;$il < count($data[0]['Product_Display_Activity']);$il++) {
                $Pendingbills = $data[0]['Product_Display_Activity'][$il];
                $SDate = str_replace("'", "", $Pendingbills["SDate"]);
                $EDate = str_replace("'", "", $Pendingbills["EDate"]);
                $CurrentVolume = str_replace("'", "", $Pendingbills["CurrentVolume"]);
                $AddVolume = str_replace("'", "", $Pendingbills["AddVolume"]);
                $DiscountAmount = str_replace("'", "", $Pendingbills["DiscountAmount"]);
                $sql="insert into Trans_Product_Display (sfCode, SfName, State_Code, Division_Code, SDate, EDate, CurrentVolume, AddVolume,DiscountAmount,Current_Date_And_Time,Distributors,Route,Activation_Flag,Retailer,Remarks,FieldForceName,HQ_SfCode)
                        values ('".$sfCode."','".$FWFlg."', '".$State_Code."','". str_replace(",", "",$divCode)."','".$SDate."','".$EDate."','".$CurrentVolume."','".$AddVolume."','".$DiscountAmount."','".$Current_Date_And_Time."','".$Distributors."','".$Route."','false','".$Retailer."','".$Remarks."','".$FieldForceName."','".$HQSfCode."')";
                performQuery($sql);
                 
            }
            break;
        case "ProductDisplayApprovals":
            $sfCode = $_GET['sfCode'];
            $flag=$data[0]['ProductDisplayApprovals']['Flag'];
            $FieldforceSfcode=$data[0]['ProductDisplayApprovals']['Sf_Code'];
            $sql = "UPDATE Trans_Product_Display SET Activation_Flag = '".$flag."' WHERE HQ_SfCode = '".$sfCode."' and sfCode=  '".$FieldforceSfcode."'";
            performQuery($sql);
            break;
        case "ProductDisplayReject":
            $sfCode = $_GET['sfCode'];
            $flag=$data[0]['ProductDisplayReject']['Flag'];
            $FieldforceSfcode=$data[0]['ProductDisplayReject']['Sf_Code'];
            $RejectReson=$data[0]['ProductDisplayReject']['Reson'];
            $sql = "UPDATE Trans_Product_Display SET Activation_Flag = '".$flag."',RejectReson= '".$RejectReson."' WHERE HQ_SfCode = '".$sfCode."' and sfCode=  '".$FieldforceSfcode."'";
            performQuery($sql);
            break;
        case "tbRCPADetails":
            $sql = "insert into tbRCPADetails select '" . $sfCode . "','" . date('Y-m-d H:i:s') . "'," . $vals["RCPADt"] . "," .
                    $vals["ChmId"] . "," . $vals["DrId"] . "," . $vals["CmptrName"] . "," . $vals["CmptrBrnd"] . "," . $vals["CmptrPriz"] . "," .
                    $vals["ourBrnd"] . "," . $vals["ourBrndNm"] . "," . $vals["Remark"] . ",'" . $div . "'," . $vals["CmptrQty"] . "," . $vals["CmptrPOB"] . "," . $vals["ChmName"] . "," . $vals["DrName"];
            performQuery($sql);
            break;
        case "tbRemdrCall":
            $sql = "SELECT isNull(max(cast(replace(RwID,'RC/" . $IdNo . "/','') as numeric)),0)+1 as RwID FROM tbRemdrCall where RwID like 'RC/" . $IdNo . "/%'";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];

            $sql = "insert into tbRemdrCall select 'RC/" . $IdNo . "/" . $pk . "','" . $sfCode . "','" . date('Y-m-d H:i:s') . "'," . $vals["Doctor_ID"] . "," .
                    $vals["WWith"] . "," . $vals["WWithNm"] . "," . $vals["Prods"] . "," . $vals["ProdsNm"] . "," . $vals["Remarks"] . "," .
                    $vals["location"] . ",'" . $div . "'";
            performQuery($sql);
            break;
         case "Activity_Report_APP":
            $sfCode;
            $div = $_GET['divisionCode'];
            $divs = explode(",", $div . ",");
            $divisionCode = (string) $divs[0];
            $eKeyID=$vals["eKey"];
            $today;
            if($vals["dcr_activity_date"]!=null && $vals["dcr_activity_date"]!=''){
          $today=str_replace("'", "", $vals["dcr_activity_date"]);
              

            }

            $sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "' and lower(Work_Type) <>lower(" . $vals["Worktype_code"] . ")  and cast(activity_date as datetime)=cast('$today' as datetime)";
            $result1 = performQuery($sql);

            $datesumm=$today;

            $routewise="'".$data[1]['Activity_Doctor_Report']['routeWise']."'";
            $docor_code=str_replace("'", "", $data[1]['Activity_Doctor_Report']['doctor_code']);
            $dcr_activity_date=str_replace("'", "", $data[0]['Activity_Report_APP']['dcr_activity_date']);
            $datasf = str_replace("'", "", $vals["DataSF"]);

            // Pending Collection Part
            for ($il = 0;$il < count($data[6]['PENDING_Bills']);$il++) {
                $Pendingbills = $data[6]['PENDING_Bills'][$il];
                $Bill_No = str_replace("'", "", $Pendingbills["Bill_No"]);
                $Bill_Amount = str_replace("'", "", $Pendingbills["Bill_Amount"]);
                $Balance_Amount = str_replace("'", "", $Pendingbills["Balance_Amount"]);
                $Collected_Amount = str_replace("'", "", $Pendingbills["Collected_Amount"]);
                $Bill_Date = str_replace("'", "", $Pendingbills["Bill_Date"]);
                $date_for_database = date ("Y-m-d H:i:s", strtotime($Bill_Date));
                
                $query = "select coll_amt,bal_amt,billamt from mas_pending_bills where sf='" . $datasf . "' and billno='" . $Bill_No . "'";
                $pendinglistt = performQuery($query);
                if (count($pendinglistt) > 0) {
                    for ($i = 0;$i < count($pendinglistt);$i++) {
                        $new = $pendinglistt[$i]['coll_amt'];
                        $billamt=$pendinglistt[$i]['billamt'];
                        $date = date('m/d/Y h:i:s', time());
                        
                        $dateonlyyear=date('y/m/d');
                        if($Collected_Amount>0){
                            $sql = "SELECT isNull(max(cast(replace(Trans_SlNo,'RC/" . $IdNo . "/','') as numeric)),0)+1 as Trans_SlNo FROM Trans_CusBill_CollectionDet where Trans_SlNo like 'RC/" . $IdNo . "/%'";
                            $tRw = performQuery($sql);
                            $pk = (int) $tRw[0]['Trans_SlNo']; 
                            $query="INSERT INTO Trans_CusBill_CollectionDet   ( Trans_SlNo,Cust_Code, ColltDt, BillNo,BillDt,BillAmt,CollAmt,BalAmt,Division_Code,SF_CODE) VALUES (  '".$pk."','".$docor_code."', '".$date."', '".$Bill_No."', '".$date_for_database."', '".$billamt."', '".$Collected_Amount."','".$Balance_Amount."','".$divisionCode."','".$datasf."'  )";
                            performQuery($query);
                        }
                        
                        $query = "UPDATE mas_pending_bills  SET  coll_amt =coll_amt+" .$Collected_Amount . " , bal_amt =bal_amt-" .$Collected_Amount. ",lastupdateddate='".$date."'   where sf='" . $datasf . "' and billno='" . $Bill_No . "' ";
                        performQuery($query);
                    }
                }
            }
            //End Bill Collection Part
            //Checking Routwise Entry Exsists
            if($routewise=="'0'"){
                $townCode = $data[0]['Activity_Report_APP']['Town_code'];
                $sql = "SELECT activity.trans_slno FROM vwActivity_Report activity  inner join vwactivity_msl_details msl on msl.trans_slno=activity.trans_slno where activity.SF_Code='" . $sfCode . "' and cast(activity.activity_date as datetime)=cast('$today' as datetime) and sdp=$townCode";
                $res = performQuery($sql);
                if (count($res) > 0){
                    $result = array();
                    $result['type'] = 2;
                    $result['msg']  = "Already Call Exist..";
                    outputJSON($result);
                    die;   
                }           
            }
            //End Checking Routwise Entry Exsists
            
            $PPXML = '';$pProd = '';$npProd = '';$pGCd = '';$pGNm = '';$pGQty = '';$SPProds = '';$nSPProds = '';$Inps = '';$nInps = '';$vTyp = 0;$DemoTo='';
            
            for ($i = 1; $i < count($data); $i++) {
                $tableData = $data[$i];
                if (isset($tableData['Activity_Doctor_Report'])) {
                    $vTyp = 1;
                    $sql = "SELECT sf_Designation_Short_Name FROM Mas_salesforce where Sf_Code='" . $sfCode . "' ";
            $Desig_Check = performQuery($sql);

if($Desig_Check[0]['sf_Designation_Short_Name']=='AIT'){
     $AITEST=9;
}
                    $DetTB = $tableData['Activity_Doctor_Report'];  
                   $POT=$DetTB["PhoneOrderTypes"];                  
                    if($DetTB['discount_price']==null)
                        $discount_price=0;
                    else
                        $discount_price =$DetTB['discount_price'];

                    if($DetTB['rateMode']==null)
                        $rateMode="Nil";
                    else
                        $rateMode = $DetTB['rateMode'];     

                    $routewise="'".$DetTB['routeWise']."'";
                    // if Routewise then create town as a Doctor name 
                    if($routewise=="'0'")
                    {
                        $sql="select * from Mas_ListedDr where EntryMode_Code=" . $DetTB['doctor_code'] . "";
                        $code=performQuery($sql);
                        $townCode = $data[0]['Activity_Report_APP']['Town_code'];
                        if(count($code)==0){
                                $sql = "{call  svListedDR_APP_Route('$sfCode'," . $DetTB['doctor_name'] . ",'test','',2,null,$townCode,'0','" . date('Y-m-d H:i:s') . "',$Owndiv,16,0,null,'',''," . $DetTB['doctor_code'] . ")}";
                                performQueryWP($sql,[]);    
                        }
                        $sql="select * from Mas_ListedDr where EntryMode_Code=" . $DetTB['doctor_code'] . "";
                        $code=performQuery($sql);
                        $DetTB['doctor_code']=$code[0]['ListedDrCode'];
                    }
                    
                    $cCode = $DetTB["doctor_code"];
                    $sql = "SELECT ListedDrCode from Mas_ListedDr where ListedDr_Profile=" . $cCode;
                    $res = performQuery($sql);
                    if (count($res) > 0){
                        $cCode=$res[0]["ListedDrCode"];
                    }
                    $vTm = $DetTB["Doc_Meet_Time"];
                    $pob = $DetTB["Doctor_POB"];
                    $POB_Value = $DetTB["orderValue"];
                    $DemoTo= $DetTB["demo_given_name"];
                    if($DetTB["net_weight_value"]==null||$DetTB["net_weight_value"]==''||$DetTB["net_weight_value"]=="null"||$DetTB["net_weight_value"]=="undefined"||empty($DetTB["net_weight_value"]))
                        $net_weight_value=0;
                    else
                        $net_weight_value = $DetTB["net_weight_value"];
                    $proc = "svDCRLstDet_App";
                    $sql = "SELECT Doctor_Name name from vwDoctor_Master_APP where Doctor_Code=" . $cCode;
                }
                if (isset($tableData['Activity_Chemist_Report'])) {
                        $vTyp = 2;
                        $DetTB = $tableData['Activity_Chemist_Report'];
                        $cCode = $DetTB["chemist_code"];
                        $vTm = $DetTB["Chm_Meet_Time"];
                        $pob = $DetTB["Chemist_POB"];
                        $sql = "SELECT Chemists_Name name from vwChemists_Master_APP where Chemists_Code=" . $cCode;
                }
                if (isset($tableData['Activity_Stockist_Report'])) {
                    $vTyp = 3;
                    $DetTB = $tableData['Activity_Stockist_Report'];
                    $cCode = $DetTB["stockist_code"];
                    $vTm = $DetTB["Stk_Meet_Time"];
                    $pob = $DetTB["Stockist_POB"];
                    $POT=$DetTB["PhoneOrderTypes"];
                    $Super_Stck_code=$DetTB["Super_Stck_code"];


                    //PhoneOrderTypes
 if($DetTB["Super_Stck_code"]==null||$DetTB["Super_Stck_code"]=="null"||$DetTB["Super_Stck_code"]=="undefined"||empty($DetTB["Super_Stck_code"])){

    $Super_Stck_code='';

 }



                    

         if (!empty($DetTB["doctor_id"]))
                    $doctor_id=$DetTB["doctor_id"];

if (!empty($DetTB["superstockistid"]))
$superstk=$DetTB["superstockistid"];

                    if( $DetTB["orderValue"]==null)
                        $POB_Value=0;
                    else
                        $POB_Value = $DetTB["orderValue"];
                    $sql = "SELECT stockiest_name name from vwstockiest_Master_APP where Distributor_Code=" . $cCode;
                    
                    if(!empty($DetTB["version"])){
                                $sql = "SELECT S_Name name from supplier_master where S_No=$doctor_id";
                                $vTyp = 8;

                }
                    
                }
                if (isset($tableData['Activity_UnListedDoctor_Report'])) {
                        $vTyp = 4;
                        $DetTB = $tableData['Activity_UnListedDoctor_Report'];
                        $cCode = $DetTB["uldoctor_code"];
                        $vTm = $DetTB["UnListed_Doc_Meet_Time"];
                        $pob = $DetTB["UnListed_Doctor_POB"];
                        $POB_Value = $DetTB["orderValue"];
                        $proc = "svDCRUnlstDet_App";
                        $sql = "SELECT unlisted_doctor_name name from vwunlisted_doctor_master_APP where unlisted_doctor_code=" . $cCode;
                }
                if(isset($tableData['DCRDetail_Distributors_Hunting'])){
                        $distHunting=$tableData['DCRDetail_Distributors_Hunting'];
                        $vTyp = 7;
                }
                $tRw = performQuery($sql);

                $cName = str_replace("'", " ", $tRw[0]["name"]);

                if (isset($tableData['Activity_Sample_Report']) || isset($tableData['Activity_Unlistedsample_Report'])) {
                    
                    if (isset($tableData['Activity_Sample_Report']))
                            $samp = $tableData['Activity_Sample_Report'];
                    if (isset($tableData['Activity_Unlistedsample_Report']))
                            $samp = $tableData['Activity_Unlistedsample_Report'];

                    $PPXML = "<ROOT>";
                    for ($j = 0; $j < count($samp); $j++) {
                        //$PPXML = $PPXML . "<Prod Cd=\"".$samp[$j]["product_code"]."\" Nm=\"".$samp[$j]["product_Name"]."\" Q=\"".$samp[$j]["Product_Rx_Qty"]."\" V=\"".$samp[$j]["Product_Sample_Qty"]."\"  fr=\"".$samp[$j]["Product_Sample_Qty"]."\"  Dis=\"".$samp[$j]["Product_Sample_Qty"]."\" />";

                        if ($j < 3) {
                            $pProd = $pProd . (($pProd != "") ? "#" : '') . $samp[$j]["product_code"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"];
                            $npProd = $npProd . (($npProd != "") ? "#" : '') . $samp[$j]["product_Name"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"];
                        } else {
                            $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"] . "#";
                            $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"] . "#";
                        }
                    }
                    $PPXML = $PPXML . "</ROOT>";
                }

            // if (isset($tableData['Activity_POB_Report'])) {

                    if (isset($tableData['Activity_POB_Report'])){
                        $samp = $tableData['Activity_POB_Report'];
                        for ($j = 0; $j < count($samp); $j++) {
                                $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Qty"] . "#";
                                $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Qty"] . "#";
                        }
                    }
                    if (isset($tableData['Activity_Stk_POB_Report'])){
                        $samp = $tableData['Activity_Stk_POB_Report'];
                        $PPXML = "<ROOT>";
                        for ($j = 0; $j < count($samp); $j++) {
							$cl_qty_stock=$samp[$j]["cb_qty"];
                            $PPXML = $PPXML . "<Prod PCode=\"".$samp[$j]["product_code"]."\" PName=\"".$samp[$j]["product_Name"]."\" CQty=\"".$samp[$j]["Qty"]."\" PQty=\"".$samp[$j]["PQty"]."\" CL=\"".$cl_qty_stock."\"   />";


                            $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Qty"] . "#";
                            $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Qty"] . "#";
                        }
                        $PPXML = $PPXML . "</ROOT>";
                    }
               // }
                if (isset($tableData['Activity_Input_Report']) || isset($tableData['Activity_Chm_Sample_Report']) || isset($tableData['Activity_Chm_Sample_Report']) || isset($tableData['activity_unlistedGift_Report'])) {
                    if (isset($tableData['Activity_Input_Report']))
                            $inp = $tableData['Activity_Input_Report'];
                    if (isset($tableData['Activity_Chm_Sample_Report']))
                            $inp = $tableData['Activity_Chm_Sample_Report'];
                    if (isset($tableData['Activity_Stk_Sample_Report']))
                            $inp = $tableData['Activity_Stk_Sample_Report'];
                    if (isset($tableData['activity_unlistedGift_Report']))
                            $inp = $tableData['activity_unlistedGift_Report'];

                    for ($j = 0; $j < count($inp); $j++) {
                        if ($j == 0 && ($vTyp == 1 || $vTyp == 4 )) {
                            $pGCd = $inp[$j]["Gift_Code"];
                            $pGNm = $inp[$j]["Gift_Name"];
                            $pGQty = $inp[$j]["Gift_Qty"];
                        } else {
                            $Inps = $Inps . $inp[$j]["Gift_Code"] . "~" . $inp[$j]["Gift_Qty"] . "#";
                            $nInps = $nInps . $inp[$j]["Gift_Name"] . "~" . $inp[$j]["Gift_Qty"] . "#";
                        }
                    }
                }
            }


            $ARCd ="";
            $ARDCd = (strlen($_GET['amc']) == 0) ? "0" : $_GET['amc'];
           
if (!empty($data[1]["Activity_Doctor_Report"]['superstockistid']))
 $superstk=$data[1]["Activity_Doctor_Report"]['superstockistid'];



	




            $sql = "{call  svDCRMain_App(?,?," . $vals["Worktype_code"] . ",'" . str_replace("'", "", $vals["Town_code"]) . "',?,'" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?," . $superstk . ")}";
            $params = array(array($sfCode, SQLSRV_PARAM_IN),
                array($today, SQLSRV_PARAM_IN),
                array($Owndiv, SQLSRV_PARAM_IN),
                array(&$ARCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_NVARCHAR(50)));

            $tr = performQueryWP($sql, $params);
                if (sqlsrv_errors() != null) {
                    echo($sql . "<br>");
                    outputJSON($params );
                    echo( "<br>");
                    outputJSON(sqlsrv_errors());
                    die;
                }
            $loc = explode(":", str_replace("'", "", $DetTB["location"]) . ":");
            $lat = $loc[0]; //latitude
            $lng = $loc[1]; //longitude
            $address = getaddress($lat, $lng);
            if ($address) {
                $DetTB["geoaddress"] = $address;
            } else {
                $DetTB["geoaddress"] = "NA";
            }
            if ($vTyp==7) {
                $sql = "exec  svDCRDetailDistirbutorsHunting '$ARCd'," .$distHunting['name'] . "," . $distHunting['contact'] . "," . $distHunting['phone'] . "," .$distHunting["area"] . "," . $vals["Daywise_Remarks"] . "," . $distHunting["address"] . ",'" . date('Y-m-d H:i:s') . "','$sfCode'," . $vals["DataSF"] . ",".$distHunting["Worked_With"]."";
                performQuery($sql);
                $resp["success"] = true;
                echo json_encode($resp);die;
            }
            $sqlsp = "{call  ";
            if ($vTyp != 0 && $vTyp!=7 && $vTyp != 9) {
                if ($vTyp == 2 || $vTyp == 3 || $vTyp == 8)
                    $proc = "svDCRCSHDet_App";

                if ($pob == '')
                    $pob = '0';
                if ($POB_Value == '')
                    $POB_Value = '0';
                $sqlsp = $sqlsp . $proc . " (?,?,?," . $vTyp . ",$cCode,'" . $cName . "'," . $vTm . "," . $pob . ",'" . str_replace("'", "", $DetTB["Worked_With"]) . "',?,?,?,?,";
                if ($vTyp == 1 || $vTyp == 4)
                    $sqlsp = $sqlsp . "?,?,?,?,?,";

                if ($vTyp == 1)
                    $sqlsp = $sqlsp . "'" . str_replace("'", "", $vals["Town_code"]) . "','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,'" . str_replace("'", "", $vals["rx_t"]) . "'," . $DetTB["modified_time"] . ",?,?," . $vals["DataSF"] . ",'" . $DetTB["geoaddress"] . "'," . $POB_Value . ",$net_weight_value,".$DetTB['stockist_code'].",".$DetTB['stockist_name'].",$discount_price,'$rateMode','".$DemoTo."')}";
                else if($vTyp==3){
                    if($DetTB["intrumenttype"]==null)
                        $DetTB["intrumenttype"]="";
                    if($DetTB["date_of_intrument"]==null)
                        $DetTB["date_of_intrument"]="";
                    $sqlsp = $sqlsp . "'" . str_replace("'", "", $vals["Town_code"]) . "','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,'" . str_replace("'", "", $vals["rx_t"]) . "'," . $DetTB["modified_time"] . ",?,?," . $vals["DataSF"] . ",'" . $DetTB["geoaddress"] . "'," . $POB_Value . ",'" . $DetTB["intrumenttype"] . "','" . $DetTB["date_of_intrument"] . "')}";
                }
                else
                    $sqlsp = $sqlsp . "'" . str_replace("'", "", $vals["Town_code"]) . "','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,'" . str_replace("'", "", $vals["rx_t"]) . "'," . $DetTB["modified_time"] . ",?,?," . $vals["DataSF"] . ",'" . $DetTB["geoaddress"] . "'," . $POB_Value . ")}";

                $params = array(array($ARCd, SQLSRV_PARAM_IN),
                    array(&$ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_NVARCHAR(50)),
                    array($sfCode, SQLSRV_PARAM_IN));
                if ($vTyp == 1 || $vTyp == 4) {
                    array_push($params, array($pProd, SQLSRV_PARAM_IN));
                    array_push($params, array($npProd, SQLSRV_PARAM_IN));
                }

                array_push($params, array($SPProds, SQLSRV_PARAM_IN));
                array_push($params, array($nSPProds, SQLSRV_PARAM_IN));

                if ($vTyp == 1 || $vTyp == 4) {
                    array_push($params, array($pGCd, SQLSRV_PARAM_IN));
                    array_push($params, array($pGNm, SQLSRV_PARAM_IN));
                    array_push($params, array($pGQty, SQLSRV_PARAM_IN));
                }
                array_push($params, array($Inps, SQLSRV_PARAM_IN));
                array_push($params, array($nInps, SQLSRV_PARAM_IN));
                array_push($params, array($Owndiv, SQLSRV_PARAM_IN));
                array_push($params, array($loc[0], SQLSRV_PARAM_IN));
                array_push($params, array($loc[1], SQLSRV_PARAM_IN));

                performQueryWP($sqlsp, $params);
                if (sqlsrv_errors() != null) {
                    echo($sqlsp . "<br>");
                    outputJSON($params );
                    echo( "<br>");
                    outputJSON(sqlsrv_errors());
                    die;
                }
                $DCRCode = $ARDCd;

/*                if ($ARDCd == "Exists") {
                    $resp["Msg"] = "Call Already Exists";
                    $resp["success"] = false;
                    echo json_encode($resp);
                    die;
                }*/
                if($vTyp == 3  || $vTyp == 8){
                    if($vTyp == 3){

                    $sqlOrd = "{call svPriOrder(?,$cCode,?," . $POB_Value . ",'" . $DetTB["intrumenttype"] . "','" . $DetTB["date_of_intrument"] . "'," . $pob . ",'" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,?,?,'".$eKeyID."','".$POT."',".$Super_Stck_code.",)}";
                   }else{

                   $sqlOrd = "{call svSuperStockist(?,$cCode,?," . $POB_Value . ",'" . $DetTB["intrumenttype"] . "','" . $DetTB["date_of_intrument"] . "'," . $pob . ",'" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,?,?,'".$eKeyID."','".$POT."',)}";
                  

}
                    $params1 = array(
                        array($sfCode, SQLSRV_PARAM_IN),
                        array($today, SQLSRV_PARAM_IN),
                        array($ARCd, SQLSRV_PARAM_IN),
                        array($Owndiv, SQLSRV_PARAM_IN),
                        array($PPXML, SQLSRV_PARAM_IN)
                    );

                    performQueryWP($sqlOrd, $params1);

                }
                
            }

            if( $data[count($data)-3]["Activity_Event_Captures"]!=null && $data[count($data)-3]["Activity_Event_Captures"]!="undefined")
            {
                $Event_Captures = $data[count($data)-3]["Activity_Event_Captures"];
                for ($j = 0; $j < count($Event_Captures); $j++) {                       
                    $ev_imgurl=$Event_Captures[$j]["imgurl"];
                    $ev_title=$Event_Captures[$j]["title"];
                    $ev_remarks=$Event_Captures[$j]["remarks"];
                    
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,lat,lon) select '" . $ARCd . "','" . $ARDCd . "'," . $ev_imgurl . "," . $ev_title . "," . $ev_remarks . ",'" . $Owndiv . "' ,'DCR','".date('Y-m-d H:i:s')."','".$lat."','".$lng."' ";
                    performQuery($sql); 
                }
            }
            $townCode = $data[0]['Activity_Report_APP']['Town_code'];
            $custCode = $cCode;
            $stockistCode = $DetTB['stockist_code'];
			$Discountpercent = $DetTB['Discountpercent'];
            $routeTarget = $data[1]["Activity_Doctor_Report"]['rootTarget'];
            $orderNo = $data[1]["Activity_Doctor_Report"]['Order_No'];
            $orderValue = $data[1]["Activity_Doctor_Report"]['orderValue'];
            $collectedAmount = $data[1]["Activity_Doctor_Report"]['Doctor_POB'];
            $PayType = $data[1]["Activity_Doctor_Report"]['PayType'];
            $PayTypeNm = $data[1]["Activity_Doctor_Report"]['PayTypeNm'];
            $PayDt = $data[1]["Activity_Doctor_Report"]['PayDt'];
            $PayNo= $data[1]["Activity_Doctor_Report"]['PayRefNo'];
            
            if($orderValue=='') $orderValue=0;
            
            if($data[1]["Activity_Doctor_Report"]['net_weight_value']==null||$data[1]["Activity_Doctor_Report"]['net_weight_value']==''||$data[1]["Activity_Doctor_Report"]['net_weight_value']=="null"||$data[1]["Activity_Doctor_Report"]['net_weight_value']=="undefined"||empty($data[1]["Activity_Doctor_Report"]['net_weight_value']))
                $net_weight_value=0;
            else
                $net_weight_value = $data[1]["Activity_Doctor_Report"]['net_weight_value'];

            if($data[1]["Activity_Doctor_Report"]['discount_price']==null)
                $discount_price=0;
            else
                $discount_price = $data[1]["Activity_Doctor_Report"]['discount_price'];

            if($data[1]["Activity_Doctor_Report"]['rateMode']==null)
                $rateMode="Nil";
            else
                $rateMode = $data[1]["Activity_Doctor_Report"]['rateMode'];

            $orderDate = $data[1]["Activity_Doctor_Report"]["Doc_Meet_Time"];
            $transOrderDetails = $data[2]['Activity_Sample_Report'];
			 $Compititor_Product = $data[7]['Compititor_Product'];
			


            if (count($transOrderDetails) > 0 ) {
                $sql = "SELECT OrderID FROM Trans_Order_Head where OrderID='".$eKeyID."' and isnull(OrderID,'')<>''";
                $eRw = performQuery($sql);
                if(count($eRw)<1 || $eKeyID==""){
                    $sql = "SELECT isnull(OrdSl,0)+1 RwID,Division_SName+cast(SLNo as varchar) SLNo FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "'";
                    $tRw = performQuery($sql);
                    $pk =$tRw[0]['SLNo']."-".$tRw[0]['RwID'];
                    $sql = "update Mas_SF_SlNo set OrdSl=".$tRw[0]['RwID']." where SF_Code='" . $sfCode . "'";
                    performQuery($sql);


    
                    $sql = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value,discount_price,rateMode,Div_ID,OrderID,OrderType,TDPercent,Order_Flag) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue," . $orderDate . ", $townCode,'" . $DCRCode . "','$net_weight_value','$discount_price','$rateMode','$Owndiv','".$eKeyID."','".$POT."',".$Discountpercent.",'0'";
                    


                    performQuery($sql);
					
					for ($i = 0; $i < count($Compititor_Product); $i++) {
	 

	  $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Compititor";
                            $tcRw = performQuery($sql);
                            $tcpk1 = (int) $tcRw[0]['RwID'];

$sql = "insert into Trans_Compititor(Trans_Sl_No,Trans_Order_No,Sf_Code,Product_Code,Name,Cat_Id,Qty,Rate,Order_Date,Division_Code,Brand_Code,Type,Ekey,Cust_Code,Route_Code) select '" . $pk . "','" .$tcpk1 . "','" . $sfCode . "','" . $Compititor_Product[$i]['Pid'] . "','" . $Compititor_Product[$i]['Name'] . "','" . $Compititor_Product[$i]['Cat_Id'] . "','" . $Compititor_Product[$i]['Rate'] . "','" . $Compititor_Product[$i]['Qty'] . "',$orderDate,'$Owndiv','" . $Compititor_Product[$i]['bid'] . "','" . $Compititor_Product[$i]['CCType'] . "' ,'".$eKeyID."'," . $custCode . ",$townCode";
                            performQuery($sql);
							
							

	 
								}
					if($AITEST==9){
						 $sql = "select Trans_SlNo  from DCRMain_Trans  where  SF_Code='" . $sfCode . "' and Activity_Date='".date('Y-m-d')."' ";
                $eRw = performQuery($sql);
						
						
                if(count($eRw)>0 || $eKeyID==""){
                  
                    $pkk =$eRw[0]['Trans_SlNo'];
						
						
						  $sql = "insert into Trans_Pragnancy__Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value,discount_price,rateMode,Div_ID,OrderID,OrderType,Remarks) select '" . $pkk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue," . $orderDate . ", $townCode,'" . $DCRCode . "','$net_weight_value','$discount_price','$rateMode','$Owndiv','".$eKeyID."','".$POT."','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "'";
                    
                    performQuery($sql);
				}
					}
    
                    $totalval = 0;$orderValue=0;
					
					
					
                    for ($i = 0; $i < count($transOrderDetails); $i++) {
                        $qty = $transOrderDetails[$i]['Product_Rx_Qty'];
						
						
						$Oldqty = $transOrderDetails[$i]['Product_Rx_NQty'];
                        $value = $transOrderDetails[$i]['Product_Sample_Qty'];
                        if($transOrderDetails[$i]['free']==null)
                            $free=0;
                        else
                            $free=$transOrderDetails[$i]['free'];
                        if($transOrderDetails[$i]['cb_qty']==null)
                           $cbQty=0;
                        else
                           $cbQty=$transOrderDetails[$i]['cb_qty'];
					   
					    if($transOrderDetails[$i]['Ccb_qty']==null)
                           $Ccb_qty=0;
                        else
                           $Ccb_qty=$transOrderDetails[$i]['Ccb_qty'];
    
	                    if($transOrderDetails[$i]['RcpaId']==null)
                           $RcpaId=0;
                        else
                           $RcpaId=$transOrderDetails[$i]['RcpaId'];
	
	                     if($RcpaId=="") $RcpaId="0";
                        if($cbQty=="") $cbQty="0";
						
						
						 if($Ccb_qty=="") $Ccb_qty="0";
    
                        if($transOrderDetails[$i]['PromoVal']==null)
                           $Promo=0;
                        else
                           $Promo=$transOrderDetails[$i]['PromoVal'];
    
                        if($Promo=="") $Promo="0";
    
        
            if($net_weight=="" || $net_weight==null){

		$sql = "select product_netwt from Mas_Product_Detail where Product_Detail_Code='" . $transOrderDetails[$i]['product_code'] . "'";
                  $Nwt = performQuery($sql);
 		$net_weight=$Nwt[0]['product_netwt'];

            }
			
			
			//$productremarks=($transOrderDetails[$i]['rx_remarks']==null? 0:$transOrderDetails[$i]['rx_remarks']);
                           $productremarks=$transOrderDetails[$i]['rx_remarks'];
                             $productremarks_Id=$transOrderDetails[$i]['rx_remarks_Id'];

                             $remarkspd = 0;
			
    
 
                        if($qty>0 || $Oldqty>0 || $free>0   || $cbQty>0  ||  $Ccb_qty>0 || $Promo>0  || $productremarks!==$remarkspd ||  $RcpaId>0 ){
    
			
                            $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Order_Details";
                            $tRw = performQuery($sql);
                            $pk1 = (int) $tRw[0]['RwID'];
                            
                              


                            if($transOrderDetails[$i]['net_weight']==null||$transOrderDetails[$i]['net_weight']==""||$transOrderDetails[$i]['net_weight']=="null"||$transOrderDetails[$i]['net_weight']=="undefined"||empty($transOrderDetails[$i]['net_weight']))
                                $net_weight=0;
                            else
                                $net_weight = $transOrderDetails[$i]['net_weight'];
        
                        
                            if($transOrderDetails[$i]['discount']==null)
                               $discount=0;
                            else
                               $discount=$transOrderDetails[$i]['discount'];
                            
                            if($transOrderDetails[$i]['discount_price']==null)
                               $discountprice=0;
                            else
                               $discountprice=$transOrderDetails[$i]['discount_price'];
        
                            
                            if($transOrderDetails[$i]['Rate']==null)
                               $rate=0;
                            else
                                $rate=$transOrderDetails[$i]['Rate'];
                            
							
							
							
                            
                            $sql = "select Product_Detail_Code,isnull(Distributor_Discount_Price,'0') DistDisc,isnull(MRP_Price,'0') DistPRate from Mas_Product_State_Rates r inner join Mas_Salesforce s on r.State_Code=s.State_Code and r.Division_Code=cast(left(s.Division_Code,charindex(',',s.Division_Code+',')-1) as int) where Sf_Code='" . $sfCode . "' and Product_Detail_Code='" . $transOrderDetails[$i]['product_code'] . "'";
                            $rateDet = performQuery($sql);
                            $Disc ="0";
                            $DistRate ="0";
                            if(count($rateDet)>0){
                                $Disc = (string) $rateDet[0]['DistDisc'];
                                $DistRate = (string) $rateDet[0]['DistPRate'];
                            }
        
                            
				$FreeP_Code = $transOrderDetails[$i]['FreeP_Code'];
				$Fname = $transOrderDetails[$i]['Fname'];
				
				
                           if($productremarks!==$remarkspd){
							    
							  $P_Code=  $transOrderDetails[$i]['product_code'];
							 $P_Name=  $transOrderDetails[$i]['product_Name'] ;
							  
                           if($productremarks_Id!=null && $productremarks_Id!=''){
                             $sqlPR = "insert into Trans_Product_Remarks( SF_Code,Cust_Code,Route_Code,Trans_Order_No,Trans_Sl_No, Product_Code,Product_Name,Div_ID,Created_Date,Remarks,Remarks_Id) select   '" . $sfCode . "'," . $custCode . ",$townCode,'" . $pk1 . "','" . $pk . "','" . $P_Code . "','" .$P_Name . "','$Owndiv','".date('Y-m-d')."','$productremarks','$productremarks_Id'";
                                performQuery($sqlPR);
						   }
							   
                             }
				
                           
						   
						   
						   
						   
						   
						   
						   
						   
						   
                           if($qty>0 || $Oldqty>0  || $free>0   || $cbQty>0 || $Promo>0   ||  $Ccb_qty>0   ||  $RcpaId>0  ){
                         
                            $sql = "insert into Trans_Order_Details(Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock,New_Qty) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product_code'] . "','" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,$rate,'$net_weight','$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','". $cbQty  ."',$Promo,'$Owndiv','" . $Fname . "','" . $FreeP_Code . "' ,'". $Ccb_qty  ."' ,$Oldqty";
                            performQuery($sql);
							
							
							
							
							 if($POT==2){
								 
								  $sql = "SELECT isNull(max(cast(Legr_ID as numeric)),0)+1 as MaxRwID FROM Trans_VanStock_Ledger";
                            $tRww = performQuery($sql);
                            $MaxRwID = (int) $tRww[0]['MaxRwID'];
							 $docor_Name=str_replace("'", "", $data[1]['Activity_Doctor_Report']['doctor_name']);
							$sql = "insert into   Trans_VanStock_Ledger (Legr_ID,from_loc,fLocTyp,To_Loc,tLocTyp,eDate,PCode,GQty,Division_Code,EntryBy,CalcTyp,TNm)  select '" . $MaxRwID . "' , '" . $sfCode . "','F' ," . $custCode . ",'R' , " . $orderDate . ",'" . $transOrderDetails[$i]['product_code'] . "',$qty,'$Owndiv','VanInv','0','" . $docor_Name . "'  ";
							
							
							
							performQuery($sql);
							
						$sql = "update Trans_Prod_DailyInventory   set Totalqty=(select Totalqty-$qty   from Trans_Prod_DailyInventory where sfcode= '" . $sfCode . "' and EntryDt='" . $today . "' and Prod_Code='" . $transOrderDetails[$i]['product_code'] . "')
where sfcode= '" . $sfCode . "' and convert(date,EntryDt)='" . $today . "' and Prod_Code='" . $transOrderDetails[$i]['product_code'] . "' ";
							
							performQuery($sql);	 
							
							
							 }
							
							
							
					}		
							if($AITEST==9){
								
								
								
								 $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Pragnancy__Details";
                            $tRw = performQuery($sql);
                            $pk11 = (int) $tRw[0]['RwID'];
								
								$sql = "insert into Trans_Pragnancy__Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID) select '" . $pk11 . "','".$eKeyID."','" . $transOrderDetails[$i]['product_code'] . "','" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,$rate,'$net_weight','$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','". $cbQty  ."',$Promo,'$Owndiv'";
                            performQuery($sql);
							}
                        }
                    }
    
                    $sql = "update Trans_Order_Head set Order_Value=((select sum(value) from Trans_Order_Details where Trans_Sl_No=Trans_Order_Head.Trans_Sl_No and Div_ID=Trans_Order_Head.Div_ID)-'$discount_price') where  Trans_Sl_No='" . $pk . "' and Div_ID='$Owndiv'";
                    $resp["HeadQryy"]=$today;
                    performQuery($sql);
                }
            }
            if ($collectedAmount <>"0"){                 
                //$PayTypeNm 
                $sql = "SELECT isNull(max(cast(Sl_No as numeric)),0)+1 as RwID FROM Trans_Payment_Detail";
                $tRw = performQuery($sql);
                $pk1 = (int) $tRw[0]['RwID'];
                $sql = "insert into Trans_Payment_Detail(Sl_No,Sf_Code,Sf_Name,Cust_Id,Cus_Name,Amount,Pay_Mode,Pay_Date,Pay_Ref_No,Remarks,Distributor_Code,Route_code,eDate,PaymentName)  select '" . $pk1 . "','" . $sfCode . "',SF_Name," . $custCode . ",(select  ListedDr_Name from Mas_ListedDr where ListedDrCode=" . $custCode . "),$collectedAmount,'".$PayType."','".$PayDt."','".$PayNo."','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "'," . $stockistCode . ", ".$townCode."," . $orderDate . ",'".$PayTypeNm."' from Mas_Salesforce where SF_Code='" . $sfCode . "'";
                performQuery($sql);
            }
                
            $sql = "SELECT Total_Order_Amount as totalOrder,Amt_Collect FROM Order_Collection_Details where Sf_Code='$sfCode' and Stockist_Code=$stockistCode and Cust_Code=$custCode";
            $tRw = performQuery($sql);
            if (!empty($tRw)) {
                $oldTotalOrder = (int) $tRw[0]['totalOrder'];
                $oldCollectedAmount = (int) $tRw[0]['Amt_Collect'];
                $total = $orderValue + $oldTotalOrder;
                $collectAmount = $collectedAmount + $oldCollectedAmount;
                $outstand = $total - $collectAmount;
                $sql = "update Order_Collection_Details set Total_Order_Amount=$total,Out_standing_Amt=$outstand,Amt_Collect=$collectAmount,Last_order_Amt=$orderValue where Sf_Code='$sfCode' and Stockist_Code=$stockistCode and Cust_Code=$custCode";
                $result = performQuery($sql);
                $resp["success"] = true;
                echo json_encode($resp);
                die;
            } else {
                $outstandinAmt = $orderValue - $collectedAmount;
                $sql = "SELECT isNull(max(cast(Trans_Collection_No as numeric)),0)+1 as RwID FROM Order_Collection_Details";
                $tRw = performQuery($sql);
                $pk2 = (int) $tRw[0]['RwID'];
                $sql = "insert into Order_Collection_Details(Trans_Collection_No,Sf_Code,Cust_Code,Stockist_Code,Total_Order_Amount,Amt_Collect,Out_standing_Amt,Last_Order_Amt,Route) select '" . $pk2 . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",$orderValue,$collectedAmount,$outstandinAmt,$orderValue, $townCode";
                performQuery($sql);
                $resp["success"] = true;


                echo json_encode($resp);
                die;
            }  
			$resp["SPSQl"] = $sqlsp;          
            break;
    }
    $resp["success"] = true;
    echo json_encode($resp);
}

$axn = $_GET['axn'];
$value = explode(":", $axn);
switch ($value[0]) {
    case "getDistReport":
        getDistReport();
        break;
    case "login":
        actionLogin();

        break;
    case "get/track":
        $SF=$_GET['SF_Code'];
        $Dt = str_replace(".000000","",$_GET['Dt']);
        $query = "select * from ((select replace(convert(varchar,Time,9),':000',' ') DtTime,lati lat,Long lng,Trans_Detail_Name,(Select  Doc_Spec_ShortName  from Mas_ListedDr where ListedDrCode=m.Trans_Detail_Info_Code) ChannelName,POB_Value,net_weight_value,GeoAddrs,Time DtTm,0 ordfld,1 typ from vwActivity_MSL_Details m  where  cast(convert(varchar,Time,101) as datetime)=cast(convert(varchar,cast('$Dt' as datetime),101) as datetime) and Time>'$Dt'  and sf_code='$SF')union(select replace(convert(varchar,DtTm,9),':000',' ') DtTime,lat,Lon lng,'','','0','0','',DtTm,1,0 typ from tbTrackLoction where SF_Code='$SF'  and cast(convert(varchar,DtTm,101) as datetime)=cast(convert(varchar,cast('$Dt' as datetime),101) as datetime) and DtTm>'$Dt' and cast(Auc as float)<=30)   union( select replace(convert(varchar,ModTime,9),':000',' ') DtTime,lati lat,Long lng,Trans_Detail_Name, ''ChannelName,POB_Value,'0' net_weight_value,GeoAddrs,ModTime DtTm,0 ordfld,2 typ from vwActivity_CSH_Detail h where  cast(convert(varchar,ModTime,101) as datetime)=cast(convert(varchar,cast('$Dt' as datetime),101) as datetime) and ModTime>'$Dt'   and sf_code='$SF')) as t order by DtTm,ordfld";
        $result = performQuery($query);
        outputJSON($result);
        break;
	
	case "ValidateExtended":
		$sfcode = $_GET['sfCode'];
		//$sql ="select login_date from TP_Attendance_App where sf_Code='$sfcode' and login_date=cast(convert(varchar,getdate(),101) as datetime) and End_Time>SFTE_Time";
		$sql ="exec ValidateShiftExtn '$sfcode'";
		$res=performQuery($sql);
		$result=[];
		$result["success"] = true;
		$result["msg"]="";
		if(count($res)<1 and $sfcode!='MGR0016'){
			$result["msg"]="Check-In Not Done.kindly do Check-In";
			$result["qry"]=$sql;
			$result["success"] = true;
		}
		echo json_encode($result);
		die;
		break;
    case "deleteEntry":
        $data = json_decode($_POST['data'], true);
        $sfCode = $_GET['sfCode'];
        $custCode = "'" . $data['custId'] . "'";
    
        $arc = (isset($data['arc']) && strlen($data['arc']) == 0) ? null : $data['arc'];
        $amc = (isset($data['amc']) && strlen($data['amc']) == 0) ? null : $data['amc'];
		orderDetailsDelete($sfCode, $custCode,$arc, $amc);
        $result = deleteEntry($arc, $amc);
        break;
 case "DeleteNotify":
 $data = json_decode($_POST['data'], true);
 $NotifyID="'" . $data['NotifyID'] . "'";
 $query = "update  vwMasAppNotifyLists  set Active_Flag='0' where  NotifyID=".$NotifyID."";

performQuery($query);
	break;


 case "fileAttachment":
        $sf = $_GET['sf_code'];
        $file = $_FILES['imgfile']['name'];
        $info = pathinfo($file);
        $file_name = basename($file, '.' . $info['extension']);
        $ext = $info['extension'];
        $fileName = $file_name . "_" . $sf . "_" . date('d_m_Y') . "." . $ext;
        $file_src = '../Mail/uploaded/' . $fileName;
        move_uploaded_file($_FILES['imgfile']['tmp_name'], $file_src);
        break;
    case "get/doctorCount":
        outputJSON(getDoctorPCount());
        break;
    case "get/setup":
        outputJSON(getAPPSetups());
        break;
    case "imgupload":
        $sf = $_GET['sf_code'];
        $filNm = $_GET['Slfypath'];
		if($filNm=="")$filNm=$_FILES["imgfile"]["name"];
		//if($sf=="MGR0016"){
			move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photos/" . $sf . "_" . $filNm);
		//}else{
			//move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photos/" . $sf . "_" . $_FILES["imgfile"]["name"]);
		//}
		//if($filNm==null || $filNm="") $filNm=$_FILES["imgfile"]["name"]; ///$_FILES["imgfile"]["name"]
        break;
    case "get/jointwork":
        outputJSON(getJointWork());
        break;
    case "get/subordinate":
        outputJSON(getSubordinate());
        break;
    case "get/submgr":
        outputJSON(getSubordinateMgr());
        break;
		case "get/subStk":
		
		outputJSON(getSubordinateStk());
		break;
    case "get/uldoctorCount":
        outputJSON(getUlDoctorPCount());
        break;
    case "get/chemistCount":
        outputJSON(getChemistPCount());
        break;
    case "get/stockistCount":
        outputJSON(getStockistPCount());
        break;
case "spLostProducts":
$custCode=$_GET['custCode'];
$stockistCode=$_GET['stockistCode'];
$sfCode=$_GET['sfCode'];
$sql="exec spLostProduct $custCode,$sfCode,$stockistCode";

$det=performQuery($sql);
outputJSON($det);
break;

    case "get/AttndMn":
        outputJSON(getAttndCnts());
        break;
    case "get/AttnDySty":
        outputJSON(getAttndTDyDets());
        break;
		
		
	case "get/AttnStatus":
		outputJSON(getAttndStatus());
		break;
	
	case "get/MyTMStatus":
		outputJSON(getMyTmStatus());
		break;
	case "get/MyTMStatusDet":
		outputJSON(getMyTmStatusDet());
		break;
	case "get/MyTMCntDet":
		outputJSON(getMyTmCntDet());
		break;


    case "get/BrndSumm":
        $data = json_decode($_POST['data'], true);
        $desig = $data['desig'];
        outputJSON(getBrandWiseProd($desig));
        break;

    case "get/BrndSummLitters":
        $data = json_decode($_POST['data'], true);
        $desig = $data['desig'];
        outputJSON(getBrandLittersWiseProd($desig));
        break;
    case "table/list":
        $results;
        $data = json_decode($_POST['data'], true);
        $sfCode = $_GET['sfCode'];
        $RSF = $_GET['rSF'];
        $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];

        switch (strtolower($data['tableName'])) {
            case "vwproductstaterates":
                $State_Code = $_GET['State_Code'];
                $query = "select * from vwProductStateRates where Division_code='" . $Owndiv . "' and State_Code=$State_Code";
                $results = performQuery($query);
                break;
            case "dcrdetail_distributors_hunting":
                $Dt = date('Y-m-d');
                $sfCode=$_GET['rSF'];
                $sqlH = "SELECT Trans_SlNo FROM vwActivity_Report where SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$Dt' as datetime)";
                $sql = "select * FROM DCRDetail_Distributors_Hunting where Trans_SlNo in (" . $sqlH . ")";
                $results = performQuery($sql);
            
                break;
            case "mas_worktype":
                $query = "exec GetWorkTypes_App '" . $RSF . "'";
                $results = performQuery($query);
                break;
            case "product_master":
                $desig = $data['desig'];
                $results = getProducts($desig);
                break;
            case "mas_product_brand":
                $query = "select product_brd_code,product_brd_sname from mas_product_brand where division_code='" . $Owndiv . "' and Product_Brd_Active_Flag=0";
                $results = performQuery($query);
                break;
            case "taxmaster":
                $query = "exec getTaxMaster '" . $Owndiv . "'";
                $results = performQuery($query);
                break;
            case "prodtaxdets":
                $State_Code = $_GET['State_Code'];
                $query = "exec getTaxProdStruct '" . $State_Code . "','" . $Owndiv . "'";
                $results = performQuery($query);
                break;



 case "pendingbils":
                    $Rsf = $_GET['rSF'];
                    
                    //ttth
                      //$query = "select billno,custcode,sf,billamt,coll_amt,bal_amt,inv_dt from mas_pending_bills  where sf='" . $Rsf . "' ";
                      $query = "select billno,custcode,sf,billamt,coll_amt,bal_amt,inv_dt from mas_pending_bills  where sf='" . $Rsf . "' ";
        $pendinglist = performQuery($query);
        if (count($pendinglist) > 0) {
            for ($i = 0;$i < count($pendinglist);$i++) {
                
                
                if(!($pendinglist[$i]['bal_amt']==0)){
            $pendinglistt[] = array("billno" => $pendinglist[$i]['billno'],"custcode" => $pendinglist[$i]['custcode'], "billamt" => $pendinglist[$i]['billamt'], "coll_amt" => 0,"date" => $pendinglist[$i]['inv_dt']->format('Y-m-d'), "BalanceAmout" => $pendinglist[$i]['bal_amt']);

                }

                
            }
        } else {
            $pendinglistt[] = array("Nopending" => "No Pending Bills");
        }
         $result['PendingList'] = $pendinglistt;
                    
                    //$query = "select billno,sf,billamt,custcode,coll_amt from mas_pending_bills  where sf='" . $Rsf . "' ";
                    //$results = performQuery($query);
                    // $result['PendingList'] = $pendinglistt;
                    outputJSON($result);
                    die;
                break;
                
                case "productcategoryrates":
                
                  $divisionCode=str_replace(",","",$_GET['divisionCode']);
                  $state=$_GET['State_Code'];
                    
                
        $query = "select * from ((select Cate_Code,product_detail_code,discount,rp_base_rate,rp_case_rate,mrp_rate,effective_from_date ,SS_Base_Rate,SS_Case_Rate  from mas_product_category_rates  where division_code='" . $divisionCode . "') union (select 0 Cate_Code,product_detail_code,'0' discount,cast(Retailor_Price as varchar) rp_base_rate,cast(RetailCasePrice as varchar) rp_case_rate,cast(MRP_Price as varchar) mrp_rate,getdate() effective_from_date ,cast(SS_Base_Rate as varchar) SS_Base_Rate,cast(SS_Case_Rate as varchar) SS_Case_Rate from vwProductStateRates where division_code='" . $divisionCode . "' and State_Code='".$state."')) as stb";
        $productcategoryrates = performQuery($query);
        if (count($productcategoryrates) > 0) {
            for ($i = 0;$i < count($productcategoryrates);$i++) {
                $ProductCategoryRates[] = array("Cate_Code" => $productcategoryrates[$i]['Cate_Code'],"Product_Detail_Code" => $productcategoryrates[$i]['product_detail_code'],"Discount" => $productcategoryrates[$i]['discount'], "RP_Base_Rate" => $productcategoryrates[$i]['rp_base_rate'], "RP_Case_Rate" => $productcategoryrates[$i]['rp_case_rate'], "MRP_Rate" => $productcategoryrates[$i]['mrp_rate'], "Effective_From_Date" => $productcategoryrates[$i]['effective_from_date'], "SS_Base_Rate" => $productcategoryrates[$i]['SS_Base_Rate'], "SS_Case_Rate" => $productcategoryrates[$i]['SS_Case_Rate']);  
}
        } else {
            $ProductCategoryRates[] = array("No ProductCategoryRates" => "No ProductCategoryRates");
        }
         $result['ProductCategoryRates'] = $ProductCategoryRates;
                    
                   
                    outputJSON($result);
                    die;
                break;
                
                
                
                
            case "gift_master":
                $query = "exec getAppGift '" . $sfCode . "'";
                $results = performQuery($query);
                break;
            case "category_master":

                $sfCode = $_GET['sfCode'];
                $query = "exec GetProdBrand_App '" . $sfCode . "'";
                $results = performQuery($query);
                if ($data['desig'] != stockist) {
                    $dummy = Array(
                        "id" => -1,
                        "name" => "Promoted Products"
                    );
                    array_unshift($results, $dummy);
                }
                break;
            case "vwfolders":
                $folders = array();
                $sql = "select Move_MailFolder_Id id,Move_MailFolder_Name name from Mas_Mail_Folder_Name where Division_code='$Owndiv'";
                $folders = performQuery($sql);
                array_unshift($folders, array("id" => "0", "name" => "Inbox"), array("id" => "Sent", "name" => "Sent Item"), array("id" => "View", "name" => "Viewed"));
                outputJSON($folders);
                die;
                break;
            case "getmailsf":
                $sfCode = $_GET['sfCode'];
                $sfCode="Admin";
                $divCode = $_GET['divisionCode'];
                $sql = "exec getHyrSF_APP '$sfCode'";
                $mailsSF = performQuery($sql);
                //foreach ($mailsSF as $k => $v) {
                  //  $mailsSF[$k] ['id'] = $mailsSF[$k] ['sf_code'];
                   // unset($mailsSF[$k]['sf_code']);
                    //$mailsSF[$k] ['name'] = $mailsSF[$k] ['Sf_Name'];
                  //  unset($mailsSF[$k]['Sf_Name']);
               // }
                outputJSON($mailsSF);
                die;
                break;
            case "gift_master":
                $query = "exec getAppGift '" . $sfCode . "'";
                $results = performQuery($query);
                break;
            case "vwlastupdationdate":
                if ($data['desig'] == stockist) {
                    $query = "select convert(varchar,Last_Updation_Date,103) Last_Updation_Date from Trans_Current_Stock_Details where Stockist_code= '" . $sfCode . "' group by Last_Updation_Date,Stockist_code";
                } else {
                    $query = "select convert(varchar,Last_Updation_Date,103) Last_Updation_Date,cast(Stockist_code as int) stockistCode from Trans_Current_Stock_Details c
                    inner join vwstockiest_Master_APP as tab on tab.stockiest_code=c.Stockist_code
                    where tab.SF_Code='$sfCode' group by c.Last_Updation_Date,c.Stockist_code";
                }
                $results = performQuery($query);
                break;

	


            case "doctor_category":
                $query = "select Doc_Cat_Code id,Doc_Cat_Name name from Mas_Doctor_Category where Division_code='" . $Owndiv . "' and Doc_Cat_Active_Flag=0";
                $results = performQuery($query);
                break;
            case "doctor_specialty":
                $query = "select Doc_Special_Code id,Doc_Special_Name name from Mas_Doctor_Speciality where Division_code='" . $Owndiv . "' and Doc_Special_Active_Flag=0";
                $results = performQuery($query);
                break;
            case "mas_doc_class":
                $query = "select Doc_ClsCode id,Doc_ClsName name from Mas_Doc_Class where Division_code='" . $Owndiv . "' and Doc_Cls_ActiveFlag=0";
                $results = performQuery($query);
                break;
            case "mas_doc_qualification":
                $query = "select Doc_QuaCode id,Doc_QuaName name from Mas_Doc_Qualification where Division_code='" . $Owndiv . "' and Doc_Qua_ActiveFlag=0";
                $results = performQuery($query);
                break;
            case "vwactivity_csh_detail":
                $or = (isset($data['or']) && $data['or'] == 0) ? null : $data['or'];
                $where = isset($data['where']) ? json_decode($data['where']) : null;
                $query = "select * from vwActivity_CSH_Detail where Trans_Detail_Info_Type=" . $or . " and " . join(" or ", $where) . " order by vstTime";
                $results = performQuery($query);
                break;
           case "vwactivity_supercsh_detail":
                $or = (isset($data['or']) && $data['or'] == 0) ? null : $data['or'];
                $where = isset($data['where']) ? json_decode($data['where']) : null;

                
                $query = "select * from vwActivity_SuperCSH_Detail where Trans_Detail_Info_Type=" . $or . " and " . join(" or ", $where) . " order by vstTime";

         
                $results = performQuery($query);
                break;


            case "vwtourplan":
                $CMn=$_GET['CMonth'];
                $CYr=$_GET['CYr'];

                $query = "exec TourPlan_Holyday_Auto_Insert '$sfCode' ,'$CMn','$CYr',' $stateCode'";
                $resultss = performQuery($query);

                $query = "select  * from vwTourPlan where SF_Code='$sfCode' and Tmonth=$CMn and Tyear=$CYr";
                $results = performQuery($query);
                outputJSON($results);
                die;
                break;


            case "vwtourplanfcd":
            $stateCode = $_GET['State_Code'];



                $CMn=$_GET['CMonth'];
                $CYr=$_GET['CYr'];
                $query = "exec TourPlan_Holyday_Auto_Insert '$sfCode' ,'$CMn','$CYr',' $stateCode'";
                $resultss = performQuery($query);


                $query = "select  TOP 1*  from Trans_TP where SF_Code='$sfCode' and Tour_Month=$CMn and Tour_Year=$CYr and Confirmed=1 and 
Worktype_Name_B!='Holiday' and Worktype_Name_B!='Leave' and Worktype_Name_B!='Weekly Off' UNION
               select  TOP 1*  from Trans_TP_One where SF_Code='$sfCode' and Tour_Month=$CMn and Tour_Year=$CYr and Confirmed=1 and 
Worktype_Name_B!='Holiday' and Worktype_Name_B!='Leave' and Worktype_Name_B!='Weekly Off' ";




                $results = performQuery($query);
                outputJSON($results);
				
                die;
                break;

           

				case "vwleavetype":
				$query="select isnull(SF_Per_City_Pincode,'S') MarriedFLag,isnull(sf_short_name,'M') MaleorFemale,ISNULL(cf.Paternity,0)Pflag from Mas_Salesforce ms left join Mas_CombOff cf on cf.Sf_Code=ms.Sf_Code where ms.Sf_Code='".$sfCode."'";
				$result = performQuery($query); 
				$MarriedFLag=$result[0]['MarriedFLag'];
				$MaleorFemale=$result[0]['MaleorFemale']; 
				$PaternityEligible=$result[0]['Pflag']; 
                $query = "select * from ((select Leave_Code id,Leave_Name name,Leave_SName,COffType from vwLeaveType where Division_code='" . $Owndiv . "')
				union (select Leave_Code id,Leave_Name name,Leave_SName,COffType from mas_Leave_Type l inner join Mas_CombOff c on c.Comb_off=COffType and COffType=1 where Active_Flag=0 and SF_Code='$sfCode' and Division_code='" . $Owndiv . "')) as tb
                union(select Leave_Code id,Leave_Name name,Leave_SName,COffType from mas_Leave_Type where Leave_code='38' and 'S'='$MarriedFLag')
				union(select Leave_Code id,Leave_Name name,Leave_SName,COffType from mas_Leave_Type where Leave_code='37' and 'M'='$MarriedFLag' and 1=$PaternityEligible)";
				$results = performQuery($query);
                break;
				case "testvwleavetype":
                $query = "select Leave_Code id,Leave_Name name,Leave_SName,COffType from testvwLeaveType where Division_code='" . $Owndiv . "'";
                $results = performQuery($query);
                break;
			case "vwdeviationtype":
                $query = "select Deviation_code id,DeviationName name from vwDeviationType where Division_code='" . $Owndiv . "'";
			    $results = performQuery($query);
                break;
            case "vwnumber_of_hours":

                $query = "select Id id,HoursName name from vwnumber_of_hours where Division_code='" . $Owndiv . "'";
                $results = performQuery($query);
                break;
            case "gettpdet":
                $date = date('Y-m-d');
                $query = "select * from vwGetTodayTP where Sf_Code='$RSF'";
                $results = performQuery($query);
                outputJSON($results);
                die;
            case "getexpensedet":
                $date = date('Y-m-d');
                $sfCode = $_GET['rSF'];
                $query = "select Expense_Allowance,Expense_Distance,Expense_Fare,Expense_Total from  Trans_FM_Expense_Detail where cast(Created_Date as date)='$date' and Sf_Code='$sfCode'";
                $head = performQuery($query);
                $query = "select Cal_Type type,Parameter_Name parameter,cast(Amount as int) amount from Trans_Additional_Exp
                    where cast(Created_Date as date)='$date' and Sf_Code='$sfCode'";
                $additional = performQuery($query);
                $results = array();
                if (!empty($head)) {
                    $results['head'] = $head[0];
                    $results['extraDetails'] = $additional;
                }
                outputJSON($results);
                die;
                break;
            case "viewstock":
                  $stockistCode=$_GET['stockistCode'];
                  $sql="select Product_Code,Product_Name,Cl_Qty,pieces from (select date,Product_Code,Product_Name,Cl_Qty,pieces,ROW_NUMBER() 
                   over(partition by Product_Code order by date desc) rw from Trans_Secondary_Sales_Details 
                   where Stockist_Code='$stockistCode') as tbl where rw=1";
                  $results = performQuery($sql);
                  outputJSON($results);die;
                   break;
            case "vwtour_plan_mgr_app":
                $query = "select  * from vwTour_Plan_Mgr_App where SF_Code='$sfCode'";
                $results = performQuery($query);
                outputJSON($results);
                die;
                break;
            default:
                $sfCode = (isset($data['sfCode']) && $data['sfCode'] == 0) ? null : $_GET['sfCode'];
                $divisionCode = (int) $Owndiv;
                //$divisionCode = 1;

                $today = (isset($data['today']) && $data['today'] == 0) ? null : $data['today'];
                $or = (isset($data['or']) && $data['or'] == 0) ? null : $data['or'];
                $wt = (isset($data['wt']) && $data['wt'] == 0) ? null : $data['wt'];
                $tableName = $data['tableName'];
                $coloumns = json_decode($data['coloumns']);
                $desig = $data['desig'];
                $where = isset($data['where']) ? json_decode($data['where']) : null;

                $join = isset($data['join']) ? $data['join'] : null;
                $orderBy = isset($data['orderBy']) ? json_decode($data['orderBy']) : null;
                if (!is_null($or)) {
                    $results = getFromTableWR($tableName, $coloumns, $divisionCode, $sfCode, $orderBy, $where, $join, $today, $wt);
                } else {
                    $results = getFromTable($tableName, $coloumns, $divisionCode, $sfCode, $orderBy, $where, $join, $today, $wt, $desig);
                }
                break;
        }
        outputJson($results);
        break;
    case "dcr/updateEntry":
        $data = json_decode($_POST['data'], true);
        $townCode = $data[0]['Activity_Report_APP']['Town_code'];
        $custCode = $data[1]["Activity_Doctor_Report"]['doctor_code'];
        $stockistCode = $data[1]["Activity_Doctor_Report"]['Order_Stk'];
        $sfCode = $_GET['sfCode'];
       
        $arc = (isset($_GET['arc']) && strlen($_GET['arc']) == 0) ? null : $_GET['arc'];
        $amc = (isset($_GET['amc']) && strlen($_GET['amc']) == 0) ? null : $_GET['amc'];
        if (count($data[2]['Activity_Sample_Report']) != 0)
            orderDetailsDelete($sfCode, $custCode,$arc, $amc);
        deleteEntry($arc, $amc);
        addEntry();
        break;
		case "dcr/PrimaryStockistProducts":
		$data = json_decode($_POST['data'], true);
        $stockistCode = $data['Stockist'];
		$Remarks = $data['Remarks'];
        $transOrderDetails = $data['Products'];
        $orderValue = $data['Ordervalue'];
        $sfCode = $_GET['sfCode'];
		$Address = $data['Address'];
		$CompanyName = $data['CompanyName'];
		$ExpDate = $data['EXpDate'];
       
        $orderNo = '0';
		$div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $orderDate = date("Y-m-d H:i");
		$orderTodayDate = date("Y-m-d");
		$query = "SELECT Division_SName+cast(SLNo as varchar)+'-'+cast(isnull(POrdSl,0)+1 as varchar) as Max_No FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "' ";
        $Max_Sl_no=performQuery($query);
		
		$query = "select count(Trans_Sl_No)  CNT from Trans_PriOrder_Head  where sf_code='" . $sfCode . "' and convert(date,Order_Date)='".$orderTodayDate ."' ";
		$TodayCount=performQuery($query);
		
	
		if($TodayCount[0]['CNT']==0){
        $pk=$Max_Sl_no[0]['Max_No'];
        $query = "update Mas_SF_SlNo set POrdSl=(isnull(POrdSl,0)+1) where SF_Code='" . $sfCode . "'";
		performQuery($query);
	
		$sql = "insert into Trans_PriOrder_Head(Trans_Sl_No,Sf_Code,Stockist_Code,Order_No,Order_Date,Order_Value,Order_Flag,Remarks,DCR_Code,Division_Code,Shipping_Address,Expect_Date,company_add) select '" . $pk . "','" . $sfCode . "','" . $stockistCode . "','" . $pk . "','" . $orderDate . "','".$orderValue."','0', '". $Remarks."','" . $pk . "' ,'".$Owndiv."','".$Address."','".$ExpDate."','".$CompanyName."'  ";
	
         performQuery($sql);
		   for ($i = 0; $i < count($transOrderDetails); $i++) {
			$qty = $transOrderDetails[$i]['Qty'];
            $sql = "SELECT isNull(max(cast(Trans_POrd_No as numeric)),0)+1 as RwID FROM Trans_PriOrder_Details";
            $tRw = performQuery($sql);
            $pk1 = (int) $tRw[0]['RwID'];
            $value = $transOrderDetails[$i]['Qty'] * $transOrderDetails[$i]['Rate'];
            $rate=$transOrderDetails[$i]['Rate'];
            $sql = "insert into Trans_PriOrder_Details(Trans_POrd_No, Trans_Sl_No,Product_Code,Product_Name,CQty,value,Rate,Order_Flag,Division_Code) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['Product_Code'] . "','" . $transOrderDetails[$i]['Product_Name'] . "', $qty,$value, $rate,'0','".$Owndiv."'";
			
            performQuery($sql);
			
        }
		 $resp["MSG"]='Primary Submitted Successfully';
		}else{
			 $resp["MSG"]='Already Submitted Today Order';
		}
		 $resp["success"] = true;
         echo json_encode($resp);
		  break;
		  case "dcr/StockistProducts":
		$data = json_decode($_POST['data'], true);
        $townCode = $data['Route'];
        $custCode = $data['Cust_Code'];
        $stockistCode = $data['Stockist'];
        $routeTarget = $data['target'];
        $transOrderDetails = $data['Products'];
        $Trans_Sl_No = $data['Trans_Sl_No'];
        $orderValue = $data['Value'];
        $collectedAmount = $data['POB'];
        $Discountpercent=$data['Discountpercent'];
        $sfCode = $_GET['sfCode'];
        $custCode = $data['Cust_Code'];
        $orderNo = '0';
		$superstk='';
		$div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
       
        $DCRCode = $data['DCR_Code'];
		$Wcode='21';
		$Remarks='AUTO DCR';
		if( $data['entryDate']!=null &&  $data['entryDate']!=''){
          $orderDate=str_replace("'", "",  $data['entryDate']);
              

            }
		$Tcode='21';
        if($data['rateMode']==null)
            $rateMode="Nil";
        else
            $rateMode = $data['rateMode'];  
        if($data['discount_price']==null)
            $discount_price="0";
        else
            $discount_price = $data['discount_price'];
		
		

		
		 $query = "SELECT Division_SName+cast(SLNo as varchar)+'-'+cast(isnull(POrdSl,0)+1 as varchar) as Max_No FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "' ";
        $Max_Sl_no=performQuery($query);
        $pk=$Max_Sl_no[0]['Max_No'];
		
 $query = "update Mas_SF_SlNo set POrdSl=(isnull(POrdSl,0)+1) where SF_Code='" . $sfCode . "'";
		performQuery($query);
		
      
		   $sql = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value,discount_price,rateMode,CL_Stock,Div_ID,OrderID,OrderType,TDPercent,Order_Flag) select '" . $pk . "','" . $sfCode . "','" . $custCode . "'," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", '".$collectedAmount."' , '".$orderValue."','" . $orderDate . "', $townCode,'" . $DCRCode . "','".$net_weight_value."','".$discount_price."','$rateMode','" . $CL_Stock."','".$Owndiv."','" .$OrderID."','".$OrderType."','".$Discountpercent."','0'";
   
       performQuery($sql);
	   
	   
	   
	   
	    $ARCd ="";
	   $sql = "{call  svDCRMain_App(?,?," . $Wcode . ",'" . str_replace("'", "", $Tcode) . "',?,'" . str_replace("'", "", $Remarks) . "',?," . $superstk . ")}";
            $params = array(array($sfCode, SQLSRV_PARAM_IN),
                array($orderDate, SQLSRV_PARAM_IN),
                array($Owndiv, SQLSRV_PARAM_IN),
                array(&$ARCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_NVARCHAR(50)));

            $tr = performQueryWP($sql, $params);
                if (sqlsrv_errors() != null) {
                    echo($sql . "<br>");
                    outputJSON($params );
                    echo( "<br>");
                    outputJSON(sqlsrv_errors());
                    die;
                }
	   
	   
        for ($i = 0; $i < count($transOrderDetails); $i++) {
			
			$productremarks=$transOrderDetails[$i]['rx_remarks'];
            $productremarks_Id=$transOrderDetails[$i]['rx_remarks_Id'];

             $remarkspd = 0;
			 $qty = $transOrderDetails[$i]['rx_qty'];
			 $free = $transOrderDetails[$i]['free'];
			    if($transOrderDetails[$i]['cb_qty']==null)
                       $cbQty=0;
                    else
                       $cbQty=$transOrderDetails[$i]['cb_qty'];
				   
				    if($transOrderDetails[$i]['Ccb_qty']==null)
                       $CcbQty=0;
                    else
                       $CcbQty=$transOrderDetails[$i]['Ccb_qty'];
				   
				   $Promo=$transOrderDetails[$i]['PromoVal'];
			   if($qty>0 || $free>0   || $cbQty>0 || $Promo>0   || $CcbQty>0 ){
		
			
            $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Order_Details";
            $tRw = performQuery($sql);
            $pk1 = (int) $tRw[0]['RwID'];
           
            $value = $transOrderDetails[$i]['sample_qty'];
            $net_weight = $transOrderDetails[$i]['product_netwt'];
            
            $discount = $transOrderDetails[$i]['discount'];
            $discountprice = $transOrderDetails[$i]['discount_price'];
            $discountpriceall =$discountpriceall+ $transOrderDetails[$i]['discount_price'];
            $net_weight_value=$net_weight_value+$transOrderDetails[$i]['netweightvalue'];
            $net_weight_value=$net_weight_value+$transOrderDetails[$i]['netweightvalue'];
            $FreeP_Code = $transOrderDetails[$i]['FreeP_Code'];
            $Fname = $transOrderDetails[$i]['Fname'];

                    if($cbQty=="") $cbQty="0";
					 if($CcbQty=="") $CcbQty="0";
                    $rate=$transOrderDetails[$i]['Rate'];
            $sql = "select Product_Detail_Code,isnull(Distributor_Discount_Price,'0') DistDisc,isnull(MRP_Price,'0') DistPRate from Mas_Product_State_Rates r inner join Mas_Salesforce s on r.State_Code=s.State_Code and r.Division_Code=cast(left(s.Division_Code,charindex(',',s.Division_Code+',')-1) as int) where Sf_Code='" . $sfCode . "' and Product_Detail_Code='" . $transOrderDetails[$i]['product'] . "'";
            $rateDet = performQuery($sql);
            $Disc ="0";
            $DistRate ="0";
            if(count($rateDet)>0){
                $Disc = (string) $rateDet[0]['DistDisc'];
                $DistRate = (string) $rateDet[0]['DistPRate'];
            }
            
			
			
			
				
			
			
			
			
			   if($qty>0 || $free>0   || $cbQty>0 || $Promo>0  || $CcbQty>0 ){
			
            $sql = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product'] . "','" . $transOrderDetails[$i]['product_Nm'] . "', $qty,$value,$rate,$net_weight,'$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','" . $cbQty . "','0','". $Div_ID."','0','0','" . $CcbQty . "'";
            performQuery($sql);
			
			
			   }
        }
        }
		
		
		 $resp["success"] = true;
            echo json_encode($resp);
		  break;
		
     case "dcr/updateProducts":
        $data = json_decode($_POST['data'], true);
        $townCode = $data['Route'];
        $custCode = $data['Cust_Code'];
        $stockistCode = $data['Stockist'];
        $routeTarget = $data['target'];
        $transOrderDetails = $data['Products'];
        $Trans_Sl_No = $data['Trans_Sl_No'];
        $orderValue = $data['Value'];
        $collectedAmount = $data['POB'];
        $Discountpercent=$data['Discountpercent'];
        $sfCode = $_GET['sfCode'];
        $custCode = $data['Cust_Code'];
        $orderNo = '0';
        $orderDate = date("Y-m-d H:i");
        $DCRCode = $data['DCR_Code'];
        if($data['rateMode']==null)
            $rateMode="Nil";
        else
            $rateMode = $data['rateMode'];  
        if($data['discount_price']==null)
            $discount_price="0";
        else
            $discount_price = $data['discount_price'];
        $query = "select Order_value pob_value,CL_Stock,Div_ID,OrderID,OrderType from Trans_Order_Head WHERE Trans_Sl_No ='" . $Trans_Sl_No . "'";
        $pobValSummoutput=performQuery($query);
        $pobValSumm=$pobValSummoutput[0]['pob_value'];
        $datesumm=date('Y-m-d');
        $CL_Stock=$pobValSummoutput[0]['CL_Stock'];
        $Div_ID=$pobValSummoutput[0]['Div_ID'];
        $OrderID=$pobValSummoutput[0]['OrderID'];
        $OrderType=$pobValSummoutput[0]['OrderType'];
        $query = "update dcr_summary set pob_value=(pob_value-$pobValSumm)+$orderValue WHERE sf_code ='$sfCode' and cast(submission_date as date)='$datesumm'";
        performQuery($query);
        $query = "delete from Trans_Order_Head WHERE Trans_Sl_No ='" . $Trans_Sl_No . "'";
        performQuery($query);
		$query = "delete from Trans_Product_Remarks WHERE Trans_Sl_No ='" . $Trans_Sl_No . "'";
        performQuery($query);
		
		
        $sql = "delete from Trans_Order_Details WHERE Trans_Sl_No='$Trans_Sl_No'";
        performQuery($sql);
        $sql = "select Total_Order_Amount,Out_standing_Amt,Amt_Collect,Last_order_Amt from Order_Collection_Details where Sf_Code='$sfCode' and Cust_Code=$custCode";
        $tr = performQuery($sql);
        $total = $tr[0]['Total_Order_Amount'] - $orderValue;
        $outstand = $tr[0]['Out_standing_Amt'] + $collectedAmount;
        $collectAmount = $tr[0]['Amt_Collect'] - $collectedAmount;
        $sql = "update Order_Collection_Details set Total_Order_Amount=$total,Out_standing_Amt=$outstand,Amt_Collect=$collectAmount where Sf_Code='$sfCode' and Cust_Code=$custCode";
        $tr = performQuery($sql);

        $pk = $Trans_Sl_No;
       
        $totalval = 0;
        $net_weight_value=0;
        $discountpriceall=0;
                 
        $sql = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value,discount_price,rateMode,CL_Stock,Div_ID,OrderID,OrderType,TDPercent) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue,'" . $orderDate . "', $townCode,'" . $DCRCode . "',$net_weight_value,'".$discount_price."','$rateMode','" . $CL_Stock."','". $Div_ID."','" .$OrderID."','".$OrderType."','".$Discountpercent."'";
   
       performQuery($sql);
        for ($i = 0; $i < count($transOrderDetails); $i++) {
			
			$productremarks=$transOrderDetails[$i]['rx_remarks'];
            $productremarks_Id=$transOrderDetails[$i]['rx_remarks_Id'];

             $remarkspd = 0;
			 $qty = $transOrderDetails[$i]['rx_qty'];
			 $free = $transOrderDetails[$i]['free'];
			    if($transOrderDetails[$i]['cb_qty']==null)
                       $cbQty=0;
                    else
                       $cbQty=$transOrderDetails[$i]['cb_qty'];
				   
				    if($transOrderDetails[$i]['Ccb_qty']==null)
                       $CcbQty=0;
                    else
                       $CcbQty=$transOrderDetails[$i]['Ccb_qty'];
				   
				   $Promo=$transOrderDetails[$i]['PromoVal'];
			   if($qty>0 || $free>0   || $cbQty>0 || $Promo>0   || $CcbQty>0 || $productremarks!==$remarkspd){
		
			
            $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Order_Details";
            $tRw = performQuery($sql);
            $pk1 = (int) $tRw[0]['RwID'];
           
            $value = $transOrderDetails[$i]['sample_qty'];
            $net_weight = $transOrderDetails[$i]['product_netwt'];
            
            $discount = $transOrderDetails[$i]['discount'];
            $discountprice = $transOrderDetails[$i]['discount_price'];
            $discountpriceall =$discountpriceall+ $transOrderDetails[$i]['discount_price'];
            $net_weight_value=$net_weight_value+$transOrderDetails[$i]['netweightvalue'];
            $net_weight_value=$net_weight_value+$transOrderDetails[$i]['netweightvalue'];
            $FreeP_Code = $transOrderDetails[$i]['FreeP_Code'];
            $Fname = $transOrderDetails[$i]['Fname'];

                    if($cbQty=="") $cbQty="0";
					 if($CcbQty=="") $CcbQty="0";
                    $rate=$transOrderDetails[$i]['Rate'];
            $sql = "select Product_Detail_Code,isnull(Distributor_Discount_Price,'0') DistDisc,isnull(MRP_Price,'0') DistPRate from Mas_Product_State_Rates r inner join Mas_Salesforce s on r.State_Code=s.State_Code and r.Division_Code=cast(left(s.Division_Code,charindex(',',s.Division_Code+',')-1) as int) where Sf_Code='" . $sfCode . "' and Product_Detail_Code='" . $transOrderDetails[$i]['product'] . "'";
            $rateDet = performQuery($sql);
            $Disc ="0";
            $DistRate ="0";
            if(count($rateDet)>0){
                $Disc = (string) $rateDet[0]['DistDisc'];
                $DistRate = (string) $rateDet[0]['DistPRate'];
            }
            
			
			           if($productremarks!==$remarkspd){
							  
                           
                             $sqlPR = "insert into Trans_Product_Remarks( SF_Code,Cust_Code,Route_Code,Trans_Order_No,Trans_Sl_No, Product_Code,Product_Name,Div_ID,Created_Date,Remarks,Remarks_Id) select   '" . $sfCode . "'," . $custCode . ",$townCode,'" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product'] . "','" . $transOrderDetails[$i]['product_Nm'] . "','". $Div_ID."','".date('Y-m-d')."','$productremarks','$productremarks_Id'";
                                performQuery($sqlPR);
							   
                             }
				
			
			
			
			
			   if($qty>0 || $free>0   || $cbQty>0 || $Promo>0  || $CcbQty>0 ){
			
            $sql = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product'] . "','" . $transOrderDetails[$i]['product_Nm'] . "', $qty,$value,$rate,$net_weight,'$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','" . $cbQty . "','0','". $Div_ID."','0','0','" . $CcbQty . "'";
            performQuery($sql);
			
			
			   }
        }
        }
        $sql = "update Trans_Order_Head  set Collected_Amount=$collectedAmount,Order_Value=$orderValue,net_weight_value=$net_weight_value,Order_Flag='0'   where Trans_Sl_No='" . $pk . "'";
        performQuery($sql);
        $sql = "SELECT Total_Order_Amount as totalOrder,Amt_Collect FROM Order_Collection_Details where Sf_Code='$sfCode' and Stockist_Code=$stockistCode and Cust_Code=$custCode";
        $tRw = performQuery($sql);
        $sql = "select * from vwProductDetails where DCR_Code='$DCRCode'";
        $tr = performQuery($sql);
        $SPProds = '';
        $nSPProds = '';
        $pob = 0;
        $pobVal = 0;
        $net_weight_value=0;
        $discountprize=0;
        for ($k = 0; $k < count($tr); $k++) {
            $SPProds = $SPProds . $tr[$k]['Additional_Prod_Code1'] . "#";
            $nSPProds = $nSPProds . $tr[$k]['Additional_Prod_Dtls1'] . "#";
            $pob = $tr[$k]['POB'] + $pob;
            $pobVal = $tr[$k]['POB_Value'] + $pobVal;
            $net_weight_value = $tr[$k]['net_weight_value'] + $net_weight_value;
            $discountprize = $tr[$k]['discount_price'] + $discount_price;
        }
        $sql = "update DCRDetail_Lst_Trans set POB=" . $pob . ",POB_Value=" . $pobVal . ",Additional_Prod_Code='" . $SPProds . "',Additional_Prod_Dtls='" . $nSPProds . "',Product_Code='',Product_Detail='',net_weight_value=$net_weight_value,discount_price='$discountprize',rateMode='$rateMode' where Trans_Detail_Slno='$DCRCode'";
        performQuery($sql);
        if (!empty($tRw)) {
            $oldTotalOrder = (int) $tRw[0]['totalOrder'];
            $oldCollectedAmount = (int) $tRw[0]['Amt_Collect'];
            $total = $orderValue + $oldTotalOrder;
            $collectAmount = $collectedAmount + $oldCollectedAmount;
            $outstand = $total - $collectAmount;
            $sql = "update Order_Collection_Details set Total_Order_Amount=$total,Out_standing_Amt=$outstand,Amt_Collect=$collectAmount,Last_order_Amt=$orderValue where Sf_Code='$sfCode' and Stockist_Code=$stockistCode and Cust_Code=$custCode";
            $result = performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
        } else {
            $outstandinAmt = $orderValue - $collectedAmount;
            $sql = "SELECT isNull(max(cast(Trans_Collection_No as numeric)),0)+1 as RwID FROM Order_Collection_Details";
            $tRw = performQuery($sql);
            $pk2 = (int) $tRw[0]['RwID'];
            $sql = "insert into Order_Collection_Details(Trans_Collection_No,Sf_Code,Cust_Code,Stockist_Code,Total_Order_Amount,Amt_Collect,Out_standing_Amt,Last_Order_Amt,Route) select '" . $pk2 . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",$orderValue,$collectedAmount,$outstandinAmt,$orderValue, $townCode";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
        }
        die;
        break;
    case "dcr/callReport":
        outputJSON(getCallReport());
        break;
    case "vwLeaveStatus":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwLeaveEntitle where Sf_Code='$sfCode'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
    case "vwCheckLeave":
        $sfCode = $_GET['sfCode'];
        $disableDates = array();
        $sql = "exec getLvDisabelDays '$sfCode'";
        $leaveDays = performQuery($sql);
        for ($i = 0; $i < count($leaveDays); $i++) {
           array_push($disableDates, $leaveDays[$i]['Dt']);
        }
        outputJSON($disableDates);
        break;
    case "getProductDetailing":
        $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $sql="select * from vwProductDetailing where Division_Code=$Owndiv";
         $products = performQuery($sql);
        outputJSON($products);
        die;
        break;
    case "getMailsApp":
        $sfCode = $_GET['sfCode'];
        $divCode = $_GET['divisionCode'];
        $folder = $_GET['folder'];
        $month = $_GET['month'];
        $year = $_GET['year'];
        $sql = "exec MailInbox '$sfCode','$divCode','','$folder',$year,$month,'','',''";
        $mails = performQuery($sql);
        outputJSON($mails);
        die;
        break;
    case "vwLeave":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwLeave where Reporting_To_SF='$sfCode'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
		case "vwBlockRelease":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwBlockRelease where Reporting_To_SF='$sfCode'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
		case "vwReleasedfromMGR":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwAprovedBlockRelease where sf_code='$sfCode' ";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
		
		
		case "vwDeviation":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwDeviation where Reporting_To_SF='$sfCode'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
		case "vwNewJoin":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwNewJoin where Reporting_To_SF='$sfCode'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
		
		case "vwCancelLeave":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwCancelLeave where Reporting_To_SF='$sfCode'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
		
		case "Get/WkoffStatus":
        $sfCode = $_GET['sfCode'];
        $sql = "exec getWkoffStatus '$sfCode'";
        $Permission = performQuery($sql);
        outputJSON($Permission);
		
		break;
		case "ViewPermission":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from ViewPermission where Reporting_To_SF='$sfCode'";
        $Permission = performQuery($sql);
        outputJSON($Permission);
        break;
		case "vwOnduty":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwOn_Duty_Approval where Reporting_To_SF='$sfCode'";
        $vwOnduty = performQuery($sql);
        outputJSON($vwOnduty);
        break;
		
		
		case "vwHolidayEntry":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwWeekoffEntry_Approval where Reporting_To_SF='$sfCode'";
        $vwOnduty = performQuery($sql);
        outputJSON($vwOnduty);
        break;
		case "vwmissedpunch":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwOn_Missed_Approval where Reporting_To_SF='$sfCode'";
        $vwmissedpunch = performQuery($sql);
        outputJSON($vwmissedpunch);
        break;
	case "vwExtended":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwExtended_Approval where Reporting_To_SF='$sfCode' ";
        $vwExtended = performQuery($sql);
        outputJSON($vwExtended);
        break;
	    case "ViewAllCount":
		$sfCode = $_GET['sfCode'];
		$BlkRelease =null;
		if (isset($_GET['BlkRelease'])) $BlkRelease=$_GET['BlkRelease'];
	    $sql = "select count(Reporting_To_SF) LeaveC from  vwLeave where Reporting_To_SF='$sfCode'";
        $leave = performQuery($sql);
		$sql = "select count(Reporting_To_SF) PermiC  from ViewPermission where Reporting_To_SF='$sfCode'";
        $Permission = performQuery($sql);
		$sql = "select count(Reporting_To_SF) OndutyC from vwOn_Duty_Approval where Reporting_To_SF='$sfCode'";
        $vwOnduty = performQuery($sql);
		$sql = "select count(Reporting_To_SF) MspC from vwOn_Missed_Approval where Reporting_To_SF='$sfCode'";
        $vwmissedpunch = performQuery($sql);
		$sql = "select count(Reporting_To_SF) ExtC from vwExtended_Approval where Reporting_To_SF='$sfCode'";
        $ExtendedCount = performQuery($sql);
		$sql = "select count(Reporting_To_SF) HolidayC from vwWeekoffEntry_Approval where Reporting_To_SF='$sfCode'";
        $HolidayCount = performQuery($sql);
		
		$sql = "select count(Reporting_To_SF) CancelLeave from vwCancelLeave where Reporting_To_SF='$sfCode'";
        $CancelLeave = performQuery($sql);
		$sql = "select count(Reporting_To_SF) DeviationC from vwDeviation where Reporting_To_SF='$sfCode'";
        $Deviation_C = performQuery($sql);
		
		
		if($BlkRelease!=null && $BlkRelease=="1"){
		$sql = "select count(Reporting_To_SF) BlockRC from  vwAprovedBlockRelease  where sf_code='$sfCode'";
        $BlockR = performQuery($sql);
		}else{
		$sql = "select count(Reporting_To_SF) BlockRC from vwBlockRelease  where Reporting_To_SF='$sfCode'";
        $BlockR = performQuery($sql);
		}
		
		$ViewAllCount=array();
        $ViewAllCount['leave']=$leave[0]['LeaveC'];
        $ViewAllCount['Permission']=$Permission[0]['PermiC'];
		$ViewAllCount['vwOnduty']=$vwOnduty[0]['OndutyC'];
        $ViewAllCount['vwmissedpunch']=$vwmissedpunch[0]['MspC'];
		$ViewAllCount['vwExtended']=$ExtendedCount[0]['ExtC'];
		$ViewAllCount['HolidayCount']=$HolidayCount[0]['HolidayC'];
		$ViewAllCount['CancelLeave']=$CancelLeave[0]['CancelLeave'];
		$ViewAllCount['DeviationC']=$Deviation_C[0]['DeviationC'];
		$ViewAllCount['BlockR']=$BlockR[0]['BlockRC'];
		outputJSON($ViewAllCount);
	     break;
	
	
		case "GetPermission_Status":
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){
        	$sql = "select p.Sf_Code,Sf_Name SFNm,Permissiondate,
			Approval_Flag,convert(varchar(10), cast(Permissiondate as datetime), 105) Submission_date ,
			convert(varchar(10), p.Submission_date, 105) Created_Date, 
			convert(varchar(10), p.LastUpdt_Date, 105) Approveddate, 
			CONVERT(varchar(15),CAST(FromTime AS time),100) FromTime,
			CONVERT(varchar(15),CAST(ToTime AS time),100)  ToTime,Remarks Reason,Noof_hours,
			case when Approval_Flag='2' then 'Pending' when Approval_Flag='8' then 'Late Approval' when Approval_Flag='0' then 'Approved' else 'Reject' end PStatus,
			case when Approval_Flag='2' then 'rgb(255,165,0) !important'  when Approval_Flag='0' or Approval_Flag='8' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
			Rejected_Reason from PermissionEntry p where p.Approvedby='$rSF' order by Permissiondate desc";
		}else{
			$sql = "select Sf_Code,Permissiondate,
			Approval_Flag,convert(varchar(10), Permissiondate, 105) Submission_date ,
			convert(varchar(10), Submission_date, 105) Created_Date, 
			convert(varchar(10), LastUpdt_Date, 105) Approveddate, 
			CONVERT(varchar(15),CAST(FromTime AS time),100) FromTime,
			CONVERT(varchar(15),CAST(ToTime AS time),100)  ToTime,
			Remarks Reason,Noof_hours,
			case when Approval_Flag='2' then 'Pending' when Approval_Flag='8' then 'Late Approval' when Approval_Flag='0' then 'Approved' else 'Reject' end PStatus,
			case when Approval_Flag='2' then 'rgb(255,165,0) !important'  when Approval_Flag='0' or Approval_Flag='8' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
			Rejected_Reason from PermissionEntry where Sf_Code='$sfCode' order by Permissiondate desc";
		}
        $PermissionST = performQuery($sql);
        outputJSON($PermissionST);
        break;
		
		case "GetExtended_Status":
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){
        	
        $sql = "select Sf_Name SFNm,(select top 1 (select Sft_Name from Mas_Shift_Timings where Sft_ID=ShiftSelected_Id) 
		from TP_Attendance_App where sf_code=EE.Sf_Code and convert(date,Start_Time)= convert(date,EE.Start_Time)) Shiftnames,
		EE.Sf_Code,WrkType,convert(varchar(20),cast(Approved_Date as datetime), 105) Approveddate,
		Total_Numbers,convert(varchar(10), Start_Time, 105) Submission_date ,
		CONVERT(varchar(15),CAST(Start_Time AS time),100)  STime,
		CONVERT(varchar(15),CAST(End_Time AS time),100)  ETime,
		(isnull(Start_Lat,'')+','+isnull(Start_Long,''))checkin,isnull(DATEDIFF(hh, Start_Time,End_Time),0)NumberofH,
		(isnull(End_Lat,'')+','+isnull(End_Long,''))checkout,
		case when WrkType='0' then 'Pending'  when WrkType='2' then 'Approved' else 'Reject' end EStatus,
		case when WrkType='0' then 'rgb(255,165,0) !important'  when WrkType='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		Reject_reason from Extended_entry EE
		inner join Mas_Salesforce s on s.Sf_Code=EE.SF_Code where EE.Approved_by='$rSF' order By Start_Time desc";
		}else{
        $sql = "select (select top 1 (select Sft_Name from Mas_Shift_Timings where Sft_ID=ShiftSelected_Id) 
		from TP_Attendance_App where sf_code=EE.Sf_Code and convert(date,Start_Time)= convert(date,EE.Start_Time)) Shiftnames,
		Sf_Code,WrkType,convert(varchar(20),cast(Approved_Date as datetime), 105) Approveddate,
		Total_Numbers,convert(varchar(10), Start_Time, 105) Submission_date ,
		CONVERT(varchar(15),CAST(Start_Time AS time),100)  STime,
		CONVERT(varchar(15),CAST(End_Time AS time),100)  ETime,
		(isnull(Start_Lat,'')+','+isnull(Start_Long,''))checkin,isnull(DATEDIFF(hh, Start_Time,End_Time),0)NumberofH,
		(isnull(End_Lat,'')+','+isnull(End_Long,''))checkout,
		case when WrkType='0' then 'Pending'  when WrkType='2' then 'Approved' else 'Reject' end EStatus,
		case when WrkType='0' then 'rgb(255,165,0) !important'  when WrkType='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		Reject_reason from Extended_entry EE where Sf_Code='$sfCode' order By Start_Time desc";
        }
		$PermissionST = performQuery($sql);
        outputJSON($PermissionST);
        break;
		case "GetMissedPunch_Status":
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){ //CONVERT(varchar,cast(replace(Checkout_Tme,'T',' ') as datetime),103) +' '+ 
        $sql = "select Sf_Code,Sf_Name SFNm,Shift_Name,convert(varchar(10), Approved_Date, 103) Rejectdate,convert(varchar,Missed_punch_date,103)  Missed_punch_date,convert(varchar,Missed_punch_date,103) Missed_punch_Flag,convert(varchar(10), Entry_Date, 120) Submission_date ,Reason,Checkin_Time,
		left(CONVERT(varchar,cast(replace(Checkout_Tme,'T',' ') as datetime),8),5) Checkout_Tme,
		case when Missed_punch_Flag='0' then 'Pending'  when Missed_punch_Flag='2' then 'Approved'  when Missed_punch_Flag='8' then 'Late Approval'  else 'Reject' end MPStatus,
		case when Missed_punch_Flag='0' then 'rgb(255,165,0) !important'  when Missed_punch_Flag='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,RejectReason 
		from Missed_punch_Entry where Approved_by='$rSF' order by Missed_punch_date desc";
        }else{
        $sql = "select Sf_Code,Shift_Name,convert(varchar(10), Approved_Date, 103) Rejectdate,convert(varchar,Missed_punch_date,103) Missed_punch_date,Missed_punch_Flag,convert(varchar(10), Entry_Date, 120) Submission_date ,Reason,Checkin_Time,
		left(CONVERT(varchar,cast(replace(Checkout_Tme,'T',' ') as datetime),8),5) Checkout_Tme,
		case when Missed_punch_Flag='0' then 'Pending'  when Missed_punch_Flag='2' then 'Approved'  when Missed_punch_Flag='8' then 'Late Approval'  else 'Reject' end MPStatus,
		case when Missed_punch_Flag='0' then 'rgb(255,165,0) !important'  when Missed_punch_Flag='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,RejectReason 
		from Missed_punch_Entry where sf_Code='$sfCode' order by Missed_punch_date desc";
        }
		$MPST = performQuery($sql);
        outputJSON($MPST);
        break;
		case "GetMissed_Punch":
        $sfCode = $_GET['sfCode'];
		$sql="select convert(varchar,Sf_TP_DCR_Active_Dt,23) SDt from Mas_Salesforce where SF_code='$sfCode' and Sf_TP_DCR_Active_Dt>'2020-07-22 00:00:00'";
		$stDts = performQuery($sql);
		$stDt="2020-07-22 00:00:00";
		if(count($stDts)>0){
		$stDt=$stDts[0]["SDt"];
		}
		$sql="select convert(varchar, login_date, 23) name,
			(case when WrkTyp>0 then '[ ON-Duty ]' else '[ '+Sft_Name+' ]' end)
			+case when Missed_punch_Flag is null then '' when Missed_punch_Flag=0 then ' - Pending' 
			when Missed_punch_Flag=2 then ' - Approved' when Missed_punch_Flag=3 then ' - Rejected' end name1,
			End_Time,convert(varchar, Start_Time, 24) Checkin_Time,replace(convert(varchar, SFTE_Time, 120),' ','T') COutTime
		 from (
		 select login_date,Sft_Name,max(Missed_punch_Flag) Missed_punch_Flag,max(WrkType) WrkTyp,max(Leave_Active_Flag)Leave_Active_Flag,
		 Min(Start_Time) Start_Time,Max(End_Time) End_Time,max(SFTE_Time) SFTE_Time,MAX(Devi_Active_Flag)Devi_Active_Flag from TP_Attendance_App T
		 left outer join Mas_Shift_Timings on ShiftSelected_Id=Sft_ID
		 left outer join Missed_punch_Entry m on m.Sf_Code=T.Sf_Code and m.Missed_punch_date=login_date
		 and Missed_punch_Flag!=3 
		 left outer join mas_Leave_Form lf on lf.sf_code=t.Sf_Code 
		 and ((From_Date>=login_date and to_date<=login_date) or (From_Date<=login_date  and To_Date>=login_date) or (From_Date<=login_date and From_Date>=login_date))  
         and Leave_Active_Flag<>1 
		 left join Mas_Deviation_Form df on df.Sf_Code=T.Sf_Code and CONVERT(date,CAST(df.Deviation_Date as datetime))=T.login_date and df.Devi_Active_Flag<>'1'		 
		 where T.sf_code='$sfCode' and login_date>'$stDt' 
		 and getdate()>dateadd(hour,iif(ACtOffTyp=1,ACtOffHrs,0),SFTE_Time) 
		 and login_date<cast(convert(varchar,getdate(),101) as dateTime) 
		 group by login_date,Sft_Name) as t 
		 where End_Time is null and Missed_punch_Flag is null and Leave_Active_Flag is null and Devi_Active_Flag is null";//and convert(varchar, Start_Time, 23)=convert(varchar, Missed_punch_date, 23)
		 
		$GetMissed_Punch = performQuery($sql);
        outputJSON($GetMissed_Punch);
        break;
		case "CheckWeekof":
        $sfCode = $_GET['sfCode'];
		//$sql="select  count(Start_Time) Ycount  ,convert (varchar,convert(date,dateadd(day,datediff(day,1,GETDATE()),0))) WKDate, from TP_Attendance_App  where sf_code='$sfCode' and convert(date,Start_Time)=convert(date,dateadd(day,datediff(day,1,GETDATE()),0))";
       $sql="exec getMissingAttDays '$sfCode'";
	   $getWF = performQuery($sql);
	   
		//$sql="select  Reason,convert (varchar,Week_date) WKDate  from Trans_Weekoff where sf_code='$sfCode' and  convert(date,Week_date)=convert(date,dateadd(day,datediff(day,1,GETDATE()),0))";
		$wkFl=0;
		if(count(getWF)>0){
		$sql="select iif(datediff(day,max(cast(Week_date as datetime)),'".$getWF[0]["WKDate"]."')>6,1,0) flg from Trans_Weekoff where sf_Code='$sfCode'";
	    $getWFD = performQuery($sql);
		$wkFl=$getWFD[0]["flg"];
		}
	    $Weekof=array();
        $Weekof['CheckWK']=$getWF;
        //$Weekof['GetWK']=0$getWFD;
		$Weekof['WKFlg']=$wkFl;
        outputJSON($Weekof);
        
        break;
		case "CheckWeekofandmis":
        $sfCode = $_GET['sfCode'];
		$stDt="2020-11-22 00:00:00";
		/*$sql="select  count(Start_Time) Ycount  ,
		convert (varchar,convert(date,dateadd(day,datediff(day,1,GETDATE()),0))) WKDate 
		from TP_Attendance_App  where sf_code='$sfCode' 
		and convert(date,Start_Time)=convert(date,dateadd(day,datediff(day,1,GETDATE()),0))";
		
		*/
		
		$mDt=date('Y-m-23');
		
		if(date('d')<23) $mDt = date('Y-m-d',strtotime($mDt."- 1 months"));
		$stDt=date('Y-m-d',strtotime($mDt));
		
		$sql="exec getMissingAttDays '$sfCode'";
		$getWF = performQuery($sql);
		
		
		
		/* if($_GET['sfCode']=='MGR6183'){
		$sql="select top 1 0 Ycount,'2021-01-11' WKDate,'11/01/2021' EDt,'11/01/2021 [ Monday ]'  WKDateNM from TP_Attendance_App";
		$getWF = performQuery($sql);
		} */
		
		$sql="select convert(varchar,Sf_TP_DCR_Active_Dt,23) SDt,SF_HQ from Mas_Salesforce where SF_code='$sfCode' and Sf_TP_DCR_Active_Dt>='".$stDt."'";
		$stDts = performQuery($sql);
		if(count($stDts)>0){
			$stDt=$stDts[0]["SDt"];
			if($stDts[0]["SDt"]=="464"){
				$stDt=new Date('Y-m-d 00:00:00');
			}
		}
		$sql="select convert(varchar, login_date, 23) name,
			(case when WrkTyp>0 then '[ ON-Duty ]' else '[ '+Sft_Name+' ]' end)
			+case when Missed_punch_Flag is null then '' when Missed_punch_Flag=0 then ' - Pending' 
			when Missed_punch_Flag=2 then ' - Approved' when Missed_punch_Flag=3 then ' - Rejected' end name1,
			convert(varchar, Start_Time, 24) CInTm,replace(convert(varchar, SFTE_Time, 120),' ','T') COutTm,End_Time 
		 from (
		 select login_date,Sft_Name,max(Missed_punch_Flag) Missed_punch_Flag,max(WrkType) WrkTyp,max(Leave_Active_Flag)Leave_Active_Flag,
		 Min(Start_Time) Start_Time,Max(End_Time) End_Time,max(SFTE_Time) SFTE_Time,MAX(Devi_Active_Flag)Devi_Active_Flag  from TP_Attendance_App T
		 left outer join Mas_Shift_Timings on ShiftSelected_Id=Sft_ID
		 left outer join Missed_punch_Entry m on m.Sf_Code=T.Sf_Code and m.Missed_punch_date=login_date
		 and Missed_punch_Flag!=3 
		 left outer join mas_Leave_Form lf on lf.sf_code=t.Sf_Code 
		 and ((From_Date>=login_date and to_date<=login_date) or (From_Date<=login_date  and To_Date>=login_date) or (From_Date<=login_date and From_Date>=login_date))  
         and Leave_Active_Flag<>1
		left join Mas_Deviation_Form df on df.Sf_Code=T.Sf_Code and CONVERT(date,CAST(df.Deviation_Date as datetime))=T.login_date and df.Devi_Active_Flag<>'1'		 
		 where T.sf_code='$sfCode' 
		 and login_date>='$stDt' 
		 and getdate()>dateadd(hour,iif(ACtOffTyp=1,ACtOffHrs,0),SFTE_Time) 
		 and login_date<cast(convert(varchar,getdate(),101) as dateTime) 
		 group by login_date,Sft_Name) as t 
		 where End_Time is null and Missed_punch_Flag is null and Leave_Active_Flag is null and Devi_Active_Flag is null"; //and convert(varchar, Start_Time, 23)=convert(varchar, Missed_punch_date, 23)
		 
		$GetMissed_Punch = performQuery($sql);
		//$sql="select  Reason,convert (varchar,Week_date) WKDate  from Trans_Weekoff where sf_code='$sfCode' and  convert(date,Week_date)=convert(date,dateadd(day,datediff(day,1,GETDATE()),0))";
	    //$getWFD = performQuery($sql);
		
		$wkFl=0;
		if(count($getWF)>0){
		$sql="select iif(datediff(day,isnull(max(cast(Week_date as datetime)),dateadd(day,-8,'".$getWF[0]["WKDate"]."')),'".$getWF[0]["WKDate"]."')>6,1,1) flg from Trans_Weekoff where sf_Code='$sfCode'";
	    $getWFD = performQuery($sql);
		$wkFl=1;
		if(count($getWFD)>0){
			$wkFl=$getWFD[0]["flg"];
		}
		}
	    $GetMissed=array();
		if($sfCode=='MGR5197' || $sfCode=='MGR4763' || $sfCode=='MGR5257' ){
        $GetMissed['CheckWK']=[];
		}else{
		$GetMissed['CheckWK']=$getWF;
		}
		
        $GetMissed['GetMissed']=$GetMissed_Punch;
		//$GetMissed['GetWK']=$getWFD;
		$resp["MsgID"] =0;
		if(count($GetMissed_Punch)>0){
			$GetMissed['Msg']="You are Missed Out Punch on \n" . date('d/m/Y',strtotime($GetMissed_Punch[0]["name"])) . "\n";
			
            $mn=date('m',strtotime($GetMissed_Punch[0]['name']));
			$yr=date('Y',strtotime($GetMissed_Punch[0]['name']));
			$mDt=date('Y-m-d',strtotime($yr.'-'.$mn.'-23'));
			if(date('d',strtotime($GetMissed_Punch[0]['name']))<23) $mDt = date('Y-m-d',strtotime($mDt."- 1 months"));
			$fDt=date('Y-m-d',strtotime($mDt));
			$mDt = date('Y-m-d',strtotime($mDt."+ 1 months"));
			$tDDt=date('Y-m-d',strtotime($mDt));
			$mDt = strtotime($mDt."- 1 day");
			$tDt=date('Y-m-d',$mDt); 
			$query = "select Count(Missed_punch_date) Cnt from Missed_punch_Entry where sf_Code='$sfCode' and Missed_punch_date between '".$fDt."' and '".$tDt."' and Missed_punch_Flag!=3";
			$res = performQuery($query); 
			if($res[0]["Cnt"]>=3){
				$resp["Msg"] = "Your Missedpunch Limit Exceeded. Kindly Apply Leave" ;
				$resp["MsgID"] =2;
			}
			
		}
		else if(count($getWF)>0)
			$GetMissed['Msg']="You are Missed Check-In on \n".$getWF[0]["WKDateNM"]."\n\n Please select the below type";
		else
			$GetMissed['Msg']="";	
		$GetMissed['WKFlg']=1;//$wkFl;
		
		if(count($getWF)>0){
		$sql="exec getMissingnewjoing '$sfCode'";
		$getSfjoining = performQuery($sql);
		//$Wkdatee = date('Y-m-d',strtotime('2020-12-23' ));
		$Wkdatee = date('Y-m-d',strtotime($getWF[0]["WKDate"] ));
		$FromDate= date('Y-m-d',strtotime($getSfjoining[0]["fromdate"] ));
		$Todate = date('Y-m-d',strtotime($getSfjoining[0]["todate"]));
		$Joningdate = date('Y-m-d',strtotime($getSfjoining[0]["Joiningdate"]));
		//if ($Wkdatee >= $FromDate && $Wkdatee <= $Todate)
		if($Wkdatee >= $Joningdate && dateDiffInDays($Wkdatee, $Joningdate)<13)
		{
			$sql = "select * from mas_Leave_Type  where  Leave_SName='NJE'";
			$LeaveType = performQuery($sql);
		
		
			$GetMissed['Leave_code']=$LeaveType[0]['Leave_code'];
			$GetMissed['Leave_Name']=$LeaveType[0]['Leave_Name'];
				
			$GetMissed['WKFlg']=3;	 
		}
		else
		{
			$GetMissed['WKFlg']=1;
			
		}
	}/* 
		
		if($sfCode=='MGR4762'){
			
			$sql = "select * from mas_Leave_Type  where  Leave_SName='NJE'";
			$LeaveType = performQuery($sql);
		
		
        $GetMissed['Leave_code']=$LeaveType[0]['Leave_code'];
		$GetMissed['Leave_Name']=$LeaveType[0]['Leave_Name'];
			
        $GetMissed['WKFlg']=3;
		}else{
		$GetMissed['WKFlg']=1;
		} */
		
		
		
		
        outputJSON($GetMissed);
        
        break;
		
		case "Getonduty_Status":
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){
        	$sql = "select TA.Sf_Code,Sf_Name SFNm,WrkType,login_date,iif(ODLocCd=0,'Other Location','HAP Location') ondutytype,
		convert(varchar(20), cast(Approved_Date as datetime), 105) Approveddate,
		ODLocName,Rmks Reason,convert(varchar(8),cast(Start_Time as time)) Start_Time, 
		convert(varchar(8), cast(End_Time  as time)) End_Time,(isnull(Start_Lat,'')+','+isnull(Start_Long,''))checkin,
		(isnull(End_Lat,'')+','+isnull(End_Long,''))checkout,
		WrkType,convert(varchar(20), cast(Start_Time as datetime), 105) Submission_date ,
		( select Sft_Name from Mas_Shift_Timings  where Sft_ID=TA.ShiftSelected_Id) as Shift_name,
				case when WrkType='1' then 'Pending'
				when WrkType='2' then 'Approved' else 'Reject' end OStatus,
				case when WrkType='1' then 'rgb(255,165,0) !important'  when WrkType='2' then 'rgb(3,192,60)  !important' when WrkType='8' then 'Late Approval' else 'rgb(255,0,0)  !important' end StusClr,Reject_reason
				from TP_Attendance_App  TA
inner join Mas_Salesforce s on s.Sf_Code=TA.SF_Code where  WrkType>0 and TA.Approved_by='$rSF' order by login_date desc";
		}else{
        $sql = "select Sf_Code,WrkType,login_date,iif(ODLocCd=0,'Other Location','HAP Location') ondutytype,
		convert(varchar(20), cast(Approved_Date as datetime), 105) Approveddate,
		ODLocName,Rmks Reason,convert(varchar(8),cast(Start_Time as time)) Start_Time, 
		convert(varchar(8), cast(End_Time  as time)) End_Time,(isnull(Start_Lat,'')+','+isnull(Start_Long,''))checkin,
		(isnull(End_Lat,'')+','+isnull(End_Long,''))checkout,
		WrkType,convert(varchar(20), cast(Start_Time as datetime), 105) Submission_date ,
		( select Sft_Name from Mas_Shift_Timings  where Sft_ID=TA.ShiftSelected_Id) as Shift_name,
				case when WrkType='1' then 'Pending'
				when WrkType='2' then 'Approved' when WrkType='8' then 'Late Approval' else 'Reject' end OStatus,
				case when WrkType='1' then 'rgb(255,165,0) !important'  when WrkType='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,Reject_reason
				from TP_Attendance_App  TA where  WrkType>0 and Sf_Code='$sfCode' order by login_date desc";
        }
		$ondutystatus = performQuery($sql);
        outputJSON($ondutystatus);
        break;
		
		
		case "Getholiday_Status":
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){
        	$sql = "select TA.Sf_Code,Sf_Name SFNm,WrkType,login_date,iif(ODLocCd=0,'Holiday Entry','HAP Location') ondutytype,
		convert(varchar(20), cast(Approved_Date as datetime), 105) Approveddate,
		ODLocName,Rmks Reason,convert(varchar(8),cast(Start_Time as time)) Start_Time, 
		convert(varchar(8), cast(End_Time  as time)) End_Time,(isnull(Start_Lat,'')+','+isnull(Start_Long,''))checkin,
		(isnull(End_Lat,'')+','+isnull(End_Long,''))checkout,
		Duty_Mode,convert(varchar(20), cast(Start_Time as datetime), 105) Submission_date ,
		( select Sft_Name from Mas_Shift_Timings  where Sft_ID=TA.ShiftSelected_Id) as Shift_name,
				case when Duty_Mode='1' then 'Pending'
				when Duty_Mode='2' then 'Approved' else 'Reject' end OStatus,
				case when Duty_Mode='1' then 'rgb(255,165,0) !important'  when Duty_Mode='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,Reject_reason
				from TP_Attendance_App  TA
inner join Mas_Salesforce s on s.Sf_Code=TA.SF_Code where  (Duty_Mode=1 or Duty_Mode=2 or Duty_Mode=3) and TA.Approved_by='$rSF' order by login_date desc";
		}else{
        $sql = "select Sf_Code,login_date,iif(ODLocCd=0,'Holiday Entry','HAP Location') ondutytype,
		convert(varchar(20), cast(Approved_Date as datetime), 105) Approveddate,
		ODLocName,Rmks Reason,convert(varchar(8),cast(Start_Time as time)) Start_Time, 
		convert(varchar(8), cast(End_Time  as time)) End_Time,(isnull(Start_Lat,'')+','+isnull(Start_Long,''))checkin,
		(isnull(End_Lat,'')+','+isnull(End_Long,''))checkout,
		Duty_Mode,convert(varchar(20), cast(Start_Time as datetime), 105) Submission_date ,
		( select Sft_Name from Mas_Shift_Timings  where Sft_ID=TA.ShiftSelected_Id) as Shift_name,
				case when Duty_Mode='1' then 'Pending'
				when Duty_Mode='2' then 'Approved' else 'Reject' end OStatus,
				case when Duty_Mode='1' then 'rgb(255,165,0) !important'  when Duty_Mode='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,Reject_reason
				from TP_Attendance_App  TA where  (Duty_Mode=1 or Duty_Mode=2 or Duty_Mode=3) and Sf_Code='$sfCode' order by login_date desc";
        }
		$ondutystatus = performQuery($sql);
        outputJSON($ondutystatus);
        break;
		
		case "GetLeave_Status":
        $sfCode = $_GET['sfCode'];
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){
        $sql = "select l.sf_code,l.Leave_Id,SF_Name SFNm,case when convert(date,getdate())<= convert(date,From_Date) then 1 else 0 end showflag,convert(varchar(10), l.Created_Date, 105)  Created_Date,'0' cancelreason,Reason,(select Leave_Name from mas_Leave_Type where Leave_code=Leave_Type)Leave_Type,convert(varchar(10), From_Date, 105)  From_Date,convert(varchar(10), l.LastUpdt_Date, 105)  LastUpdt_Date,convert(varchar(10), To_Date, 105)  To_Date,No_of_Days,case when
		 Leave_Active_Flag='2' or Leave_Active_Flag='4' then 'Pending'  when Leave_Active_Flag='0' then 'Approved' when Leave_Active_Flag='3'  then 'CancelPedning'  when Leave_Active_Flag='8' then 'Late Approval' else   'Reject' end LStatus ,
		 case when Leave_Active_Flag='2' or Leave_Active_Flag='4' then 'rgb(255,165,0) !important' when Leave_Active_Flag='3' then 'rgb(255,165,0) !important' when Leave_Active_Flag='0' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		 Rejected_Reason,Leave_Active_Flag from mas_Leave_Form l inner join Mas_Salesforce s on s.SF_Code=l.SF_Code
		 where Approved_to='$rSF'  order by cast(From_Date as datetime) desc";
        }else{
        $sql = "select sf_code,Leave_Id,case when convert(date,getdate())<= convert(date,From_Date) then 1 else 0 end showflag,convert(varchar(10), Created_Date, 105)  Created_Date,'0' cancelreason,  Reason,(select Leave_Name from mas_Leave_Type where Leave_code=Leave_Type)Leave_Type,convert(varchar(10), From_Date, 105)  From_Date,convert(varchar(10), LastUpdt_Date, 105)  LastUpdt_Date,convert(varchar(10), To_Date, 105)  To_Date,No_of_Days,case when
		 Leave_Active_Flag='2' or Leave_Active_Flag='4' then 'Pending'  when Leave_Active_Flag='0' then 'Approved' when Leave_Active_Flag='3'  then 'CancelPedning' when Leave_Active_Flag='8' then 'Late Approval'  else 'Reject' end LStatus ,
		 case when Leave_Active_Flag='2' or Leave_Active_Flag='4' then 'rgb(255,165,0) !important'  when Leave_Active_Flag='3' then 'rgb(255,165,0) !important'  when Leave_Active_Flag='0' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		 Rejected_Reason,Leave_Active_Flag from mas_Leave_Form where Sf_Code='$sfCode'   order by cast(From_Date as datetime) desc";
        }
		$PermissionST = performQuery($sql);
        outputJSON($PermissionST);
        break;
		case "GetLeaveCancel_Status":
        $sfCode = $_GET['sfCode'];
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){
        $sql = "select l.sf_code,l.Leave_Id,SF_Name SFNm,case when convert(date,getdate())< convert(date,From_Date) then 1 else 0 end showflag,convert(varchar(10), l.Created_Date, 105)  Created_Date,'0' cancelreason,Reason,(select Leave_Name from mas_Leave_Type where Leave_code=Leave_Type)Leave_Type,convert(varchar(10), From_Date, 105)  From_Date,convert(varchar(10), l.LastUpdt_Date, 105)  LastUpdt_Date,convert(varchar(10), To_Date, 105)  To_Date,No_of_Days,case when
		 Leavecancel_Id='2' then 'Pending'  when Leavecancel_Id='0' then 'Approved'   else 'Reject' end LStatus ,
		 case when Leavecancel_Id='2' then 'rgb(255,165,0) !important' when Leavecancel_Id='3' then 'rgb(255,165,0) !important' when Leavecancel_Id='0' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		 Rejected_Reason,Leavecancel_Id from Leave_Cancel_Form l inner join Mas_Salesforce s on s.SF_Code=l.SF_Code
		 where Approved_to='$rSF' and Leavecancel_Id<>'2'  order by cast(From_Date as datetime) desc ";
        }else{
        $sql = "select sf_code,Leave_Id,case when convert(date,getdate())< convert(date,From_Date) then 1 else 0 end showflag,convert(varchar(10), Created_Date, 105)  Created_Date,'0' cancelreason,  Reason,(select Leave_Name from mas_Leave_Type where Leave_code=Leave_Type)Leave_Type,convert(varchar(10), From_Date, 105)  From_Date,convert(varchar(10), LastUpdt_Date, 105)  LastUpdt_Date,convert(varchar(10), To_Date, 105)  To_Date,No_of_Days,case when
		 Leavecancel_Id='2' then 'Pending'  when Leavecancel_Id='0' then 'Approved'   else 'Reject' end LStatus ,
		 case when Leavecancel_Id='2' then 'rgb(255,165,0) !important'  when Leavecancel_Id='3' then 'rgb(255,165,0) !important'  when Leavecancel_Id='0' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		 Rejected_Reason,Leavecancel_Id from Leave_Cancel_Form where Sf_Code='$sfCode'   order by cast(From_Date as datetime) desc";
        }
		$PermissionST = performQuery($sql);
        outputJSON($PermissionST);
        break;
		case "GetViewBlock_Status":
        $sfCode = $_GET['sfCode'];
        
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		 if($AMod==1){
         $sql = "select     Sf_Name SFNm , convert(varchar(10),convert(date,Createddate),105) Createddate,  SF_Status,SF_Mobile,DeptName,blockeddate,AuditApproveddate  Approveddate  ,Release_Reason,AuditReason Rejected_Reason,SF_Status,AuditSf_Code,MGR_Sf_Code,case when
		 SF_Status='2' then 'Reject'  else 'Approved' end LStatus ,
		 case when SF_Status='2' then 'rgb(255,165,0) !important'  else 'rgb(3,192,60)  !important' end StusClr  from vwBlockReleaseStatus where AuditSf_Code='$sfCode'  ";
        }else{
        $sql = "select     Sf_Name SFNm ,convert(varchar(10),convert(date,Createddate),105) Createddate,SF_Status,SF_Mobile,DeptName,blockeddate,AuditApproveddate Approveddate ,Release_Reason,AuditReason Rejected_Reason,SF_Status,AuditSf_Code,MGR_Sf_Code,case when
		 SF_Status='2' then 'Reject'  when SF_Status='1'  then 'Pending With Audit'  else 'Approved' end LStatus ,
		 case when SF_Status='2' then 'rgb(255,0,0) !important' when SF_Status='1' then 'rgb(255,165,0) !important' else 'rgb(3,192,60)  !important' end StusClr    from vwBlockReleaseStatus where  MGR_Sf_Code='$sfCode'";
        }
		$PermissionST = performQuery($sql);
        outputJSON($PermissionST);
        break;
		case "GetDeviation_Status":
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){
        $sql = "select l.sf_code,l.Deviation_Id,l.SF_Name SFNm,convert(varchar(10), l.Server_Date, 105)  Created_Date,Reason,(select DeviationName from Mas_Devation where Deviation_code=Deviation_Type)Deviation_Type,convert(varchar(10), Deviation_Date, 105)  Deviation_Date,convert(varchar(10), l.MobileDate, 105)  MobileDate,convert(varchar(10), l.LastUpdt_Date, 105)  LastUpdt_Date,case when
		 Devi_Active_Flag='2' then 'Pending'  when Devi_Active_Flag='0' then 'Approved' else 'Reject' end DStatus ,
		 case when Devi_Active_Flag='2' then 'rgb(255,165,0) !important'  when Devi_Active_Flag='0' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		 Rejected_Reason,Devi_Active_Flag from Mas_Deviation_Form l inner join Mas_Salesforce s on s.SF_Code=l.SF_Code
		 where Approved_to='$rSF' and Devi_Active_Flag<>3 order by cast(Deviation_Date as datetime) desc";
        }else{
        $sql = "select sf_code,Deviation_Id,convert(varchar(10), Server_Date, 105)  Created_Date,Reason,(select DeviationName from Mas_Devation where Deviation_code=Deviation_Type)Deviation_Type,convert(varchar(10), Deviation_Date, 105)  Deviation_Date,convert(varchar(10), LastUpdt_Date, 105)  LastUpdt_Date,case when
		 Devi_Active_Flag='2' then 'Pending'  when Devi_Active_Flag='0' then 'Approved' else 'Reject' end DStatus ,
		 case when Devi_Active_Flag='2' then 'rgb(255,165,0) !important'  when Devi_Active_Flag='0' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		 Rejected_Reason,Devi_Active_Flag from Mas_Deviation_Form where Sf_Code='$sfCode'  and Devi_Active_Flag<>3 order by cast(Deviation_Date as datetime) desc";
        }
		$PermissionST = performQuery($sql);
        outputJSON($PermissionST);
        break;
		
		case "GetNewjoin_Status":
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==1){
		$sql = "select l.sf_code,l.Sl_No,l.SF_Name SFNm,convert(varchar(10), l.MobileDate, 105)  Created_Date,Reason,EntryName,convert(varchar(10), JointDate, 105)  JointDate,convert(varchar(10), l.MobileDate, 105)  MobileDate,convert(varchar(10), l.LastUpdt_Date, 105)  LastUpdt_Date,case when
		 Flag='2' then 'Pending'  when Flag='0' then 'Approved' else 'Reject' end DStatus ,
		 case when Flag='2' then 'rgb(255,165,0) !important'  when Flag='0' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		 Rejected_Reason,Flag from Trans_NewJoin l inner join Mas_Salesforce s on s.SF_Code=l.SF_Code
		 where Approved_to='$rSF' and Flag<>3 order by cast(JointDate as datetime) desc";
        }else{
			$sql = "select sf_code,Sl_No,convert(varchar(10), MobileDate, 105)  Created_Date,Reason,EntryName,convert(varchar(10), JointDate, 105)  JointDate,convert(varchar(10), LastUpdt_Date, 105)  LastUpdt_Date,case when
		 Flag='2' then 'Pending'  when Flag='0' then 'Approved' else 'Reject' end DStatus ,
		 case when Flag='2' then 'rgb(255,165,0) !important'  when Flag='0' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,
		 Rejected_Reason,Flag from Trans_NewJoin where Sf_Code='$sfCode'  and Flag<>3 order by cast(JointDate as datetime) desc";
        }
		$NewJoin = performQuery($sql);
        outputJSON($NewJoin);
        break;
		
		
		
	case "GetERTList":
	$sfCode = $_GET['sfCode'];
		//$sql = "select Cat_Code ERTCat,Cat_Name ERTCatNm,SF_name name,DeptName Desig,SF_Mobile,('https://hap.sanfmcg.com/SalesForce_Profile_Img/'+Profile_Pic)Profile_Pic from ERTMaster_Mapping inner join Mas_Salesforce on charindex(','+sf_Code+',',','+mapped_sf_Codes+',')>0 inner join Mas_Department M on sf_desgn=cast(m.DeptID as varchar)";
		$sql = "exec getMobileERTOUTDets '$sfCode','1' ";
		
		$leave = performQuery($sql);
        outputJSON($leave);
        break;
	case "GetHelpList":
        $sfCode = $_GET['sfCode'];
        $sql = "select id,HlpCat_Name+ ' [ Click here ]' text from Mas_Help_Category where Division_Code=3 and Active_Flag=0";
		$leave = performQuery($sql);
        outputJSON($leave);
        break;
	case "get/helpdet":
        $sfCode = $_GET['sfCode'];
        $id = $_GET['id'];
        $sql = "select Hlp_Name name,Hlp_Detail text from Mas_Help_Details where Cat_Id='$id' and Division_Code=3 and Active_Flag=0 order by cast(Hlp_ID as numeric)";
		$leave = performQuery($sql);
        outputJSON($leave);
        break;
    case "Productdisplayapprovals":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from Trans_Product_Display where HQ_SfCode= '" . $sfCode . "'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
        
        
        
    case "vwDcr":
        $sfCode = $_GET['sfCode'];
        $sql = "select d.Plan_Name,d.Trans_SlNo,d.Sf_Code,d.FieldWork_Indicator,d.WorkType_Name,d.Sf_Name,convert(varchar,Activity_Date,103) Activity_Date,s.Reporting_To_SF from DCRMain_Temp d
            inner join Mas_Salesforce s on d.Sf_Code=s.Sf_Code
             where d.Confirmed=1 and s.Reporting_To_SF='$sfCode'";
        $dcr = performQuery($sql);
        outputJSON($dcr);
        break;
    case "vwDcrOne":
        $TransSlNo = $_GET['Trans_SlNo'];
        $sql = "exec getDCRApprovalApp '" . $TransSlNo . "'";
        $dcr = performQuery($sql);
        outputJSON($dcr);

        break;
    case "vwChkTransApproval":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwChkTransApproval where Reporting_To_SF='$sfCode'";
        $tp = performQuery($sql);
        outputJSON($tp);
        break;
    case "vwExpenseApproval":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwExpenseApproval where Reporting_To_SF='$sfCode'";
        $tp = performQuery($sql);
        outputJSON($tp);
        break;
    case "vwChkTransApprovalOne":
        $sfCode = $_GET['code'];
        $month = $_GET['month'];
        $year = $_GET['year'];
        $sql = "select Objective,Worked_With_SF_Name,Worktype_Name_B,Territory_Code1,convert(varchar,Tour_Date,103) Tour_Date from Trans_TP_One where Sf_Code='$sfCode' order by Tour_Date";
        $tp = performQuery($sql);
        outputJSON($tp);
        break;
    case "vwChkExpenseApprovalOne":
        $sfCode = $_GET['code'];
        $month = $_GET['month'];
        $year = $_GET['year'];
        $sql = "select dcr_date,Expense_wtype_Name,Place_of_Work,Expense_Allowance,Expense_Distance,Expense_Fare,Expense_Total from Trans_FM_Expense_Detail where sf_code='$sfCode' and month(convert (date, dcr_date,103))=$month and year(convert (date, dcr_date,103))=$year order by cast(dcr_date as date)";
     
        $head= performQuery($sql);

        $sql="select Parameter_Name,sum(cast(case when (cal_type=1) then '-' else  '+' end+cast(Amount as varchar(200)) as numeric)) amt from Trans_Additional_Exp where sf_code='$sfCode' and month=$month and year=$year group by Parameter_Name";
        $summary=performQuery($sql);
        $expense=array();
        $expense['head']=$head;
        $expense['summary']=$summary;
        outputJSON($expense);
        break;
    case "tpview":
        getTPview();
        break;
    case "tpviewdt":
        getDtTPview();
        break;
    case "dcr/updRem":
        updEntry();
        break;
    case "error/data":
        $myfile = fopen("../server/errorfile.txt", "a+");
        $txt = "John Doe\n";
        $sqlsp=$_POST['data']." message:".$_GET['msg']."sf_code=".$_GET['sfCode']."\n";
        fwrite($myfile, $sqlsp);
        fclose($myfile);
        die;
        break;
    case "dcr/save":
        addEntry();
        break;
    case "mailView":
        $id = $_GET['id'];
        $sql = "update Trans_Mail_Detail set Mail_Active_Flag='1' where Open_Mail_Id='$sfCode' and Tras_Sl_No=$num";
        performQuery($sql);
        $result['success'] = true;
        outputJSON($result);
        break;
    case "mailMove":
        $folder = $_GET['folder'];
        $id = $_GET['id'];
        $sql = "update Trans_Mail_Detail set Mail_moved_to=$folder where Open_Mail_Id='$sfCode' and Tras_Sl_No=$num";
        performQuery($sql);
        $result['success'] = true;
        outputJSON($result);
        break;
    case "mailDel":
        $folder = $_GET['folder'];
        $id = $_GET['id'];
        if ($folder == "Sent")
            $sql = "update MailBox_Details set Mail_SentItem_DelFlag=1 where Mail_int_No=$id";
        else
            $sql = "update MailBox_Details set Mail_vc_DeleteFlag=1 where Mail_int_Det_No=$id";

        performQuery($sql);
        $result['success'] = true;
        outputJSON($result);
        break;

    case "createMail":
        $sf = $_GET['sfCode'];
        $date = date('Y-m-d H:i');
        $divCode = $_GET['divisionCode'];
        $file = $_POST['fileName'];
        if (!empty($file)) {
            $info = pathinfo($file);
            $file_name = basename($file, '.' . $info['extension']);
            $ext = $info['extension'];
            $fileName = $file_name . "_" . $sf . "_" . date('d_m_Y') . "." . $ext;
        } else
            $fileName = "";
        $msg1 = urldecode($_POST['message']);
        $msg = trim($msg1, '"');
        $sub1 = urldecode($_POST['subject']);
        $sub = trim($sub1, '"');
        $sql = "insert into MailBox(Mail_Dt_Date, Mail_Vc_From, Mail_Vc_To, Mail_Vc_CC, Mail_Vc_BCC, Mail_Vc_Subject, Mail_Vc_Message, Mail_From, Staffid_Id, Loc_Code, Division_Code, Mail_Attachment)
        select '$date','" . $_POST['from'] . "','" . $_POST['to'] . "'," . $_POST['cc'] . "," . $_POST['bcc'] . ",'$sub','$msg','$sf','$sf','',$divCode,'$fileName'";
        performQuery($sql);
        $sql = "SELECT isNull(max(Mail_int_No),0) as id FROM MailBox";
        $tr = performQuery($sql);
        $maxIntNo = $tr[0]['id'];
        $ToCcBcc = explode(', ', $_POST['ToCcBcc']);
        for ($i = 0; $i < count($ToCcBcc); $i++) {
            if ($ToCcBcc[$i]) {
                //  Mail_int_Det_No,Mail_View_Color
                $sql = "insert into MailBox_Details( Mail_int_No, Mail_ToCcBcc, Mail_Vc_ViewFlag, Mail_vc_DeleteFlag, Staffid_Id, Division_Code, Mail_SentItem_DelFlag,Mail_Moved_To)
                                                   select $maxIntNo,'" . $ToCcBcc[$i] . "',0,0,'" . $sf . "',$divCode,0,0";
                performQuery($sql);
            }
        }
        $result["success"] = true;
        outputJSON($result);
        break;
    case "get/precall":
        getPreCallDet();
        break;
    case "get/currentStock":
        getCurrentStockDet();
        break;

 case "get/currentSSStock":
        getCurrentSSStockDet();
        break;
    case "get/dayplan":
        outputJSON(getDayPlan());
        break;
case "get/dayplanRoute":
        outputJSON(getDayPlanRoute());
        break;
    case "get/MnthSumm":
        outputJSON(getMonthSummary());
        break;


  case "get/daycallreport":
        outputJSON(getDaycallSummary());
        break;

    case "get/DayRpt":
        outputJSON(getDayReport());
        break;
    case "get/DayReport":
        outputJSON(DayReport());
        break;

case "get/ProductRemarksView":


        outputJSON(ProductRemarksView());
        break;
		case "get/ProductRCPAView":


        outputJSON(ProductRCPAView());
        break;
case "get/ProductRemarksRpt":


        outputJSON(ProductRemarksRpt());
        break;
case "get/ProductRCPARpt":


        outputJSON(ProductRCPARpt());
        break;
case "get/PrimarySalesFpWise":


        outputJSON(PrimarySalesFpWise());
        break;


case "get/PrimarySalesStateWise":


        outputJSON(PrimarySalesStateWise());
        break;


case "get/PrimaryDistitarget":


        outputJSON(PrimarytargetDistiWise());
        break;

case "get/PrimaryProductwise":

        outputJSON(PrimaryProductWise());
        break;

case "get/getSalesforceTargetVsTotal":


        outputJSON(getSalesforceTargetVsTotal());
        break;
		case "get/GetvanLedger":


        outputJSON(GetvanLedger());
        break;
		
		
		case "get/GetvanLedgerView":


        outputJSON(GetvanLedgerView());
        break;
		
		case "get/VisitAnalysisView":


        outputJSON(VisitAnalysisView());
        break;
		
		case "get/AttendanceAnalysisView":


        outputJSON(AttendanceAnalysisView());
        break;
		case "get/getVisitFFView":


        outputJSON(getVisitFFView());
        break;
		case "get/getOrderFFView":


        outputJSON(getOrderFFView());
        break;
		
		case "get/OrderAnalysisView":


        outputJSON(OrderAnalysisView());
		 break;
		 	case "get/OrderAnalysisView":


        outputJSON(OrderAnalysisView());
		 break;
		 
case "get/DayPragnancyReport":
        outputJSON(DayPragnancyReport());
        break;
case "get/UpdateDayPragnancyReport":
        outputJSON(DayUpdatePragnancyReport());
        break;

case "get/TpRouteNotify":
        outputJSON(TpRouteNotify());
        break;

    case "summaryDet":
        outputJSON(getsummary());
        break;
    case "DaySummaryDet":
        outputJSON(getDaySummary());
        break;
    case "get/vwVstDet":
        outputJSON(getVstDets());
        break;
case "get/vwVstPragnancyDet":
        outputJSON(getVstPragnancyDets());
        break;

case "get/vwVstPragnancyDetReport":
        outputJSON(getVstPragnancyDetReport());
        break;
case "get/vwVstPendingPragnancy":
        outputJSON(getVstPendingPragnancy());
        break;
    case "get/vwOrderDetails":
        outputJSON(getVwOrderDet());
        break;
    case "save/trackloc1":
        $data1 = json_decode($_POST['data'], true);
        $TrcLocs=$data1[0]['TrackLoction'];
        $sfCode=$TrcLocs['SF_code'];
        $TLocs=$TrcLocs['TLocations'];
        $sql = "select sf_emp_id,Employee_Id from Mas_Salesforce where Sf_Code='$sfCode'";
        $sf = performQuery($sql);
        $empid = $sf[0]['sf_emp_id'];
        $employeeid = $sf[0]['Employee_Id'];
        for($ik=0;$ik<count($TLocs);$ik++){
            $lng = $TLocs[$ik]['Longitude'];
            $lat = $TLocs[$ik]['Latitude'];
            
            $address = getaddress($lat, $lng);
            $sql = "insert into tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr,Auc,deg,DvcID) select '$sfCode','$empid','$employeeid','" . $TLocs[$ik]['Time'] . "','$lat','$lng','','" . str_replace("'", "", $TLocs[$ik]['Accuracy']) . "','" . str_replace("'", "", $TLocs[$ik]['Bearing']) . "','" . str_replace("'", "", $TrcLocs['DvcID']) . "'";
            
            performQuery($sql);
        }
        break;
    case "get/calls":
        $sfCode = $_GET['sfCode'];
        $date = date('Y-m-d');
        $result = array();
        $query = "select convert(varchar,h.Activity_Date,103) Adate,order_Value orderValue,h.Trans_SlNo from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and h.Activity_Date='$date'";
        $data = performQuery($query);

        $order = 0;
        $orderVal = 0;
        $calls = 0;
        if (count($data) > 0) {
            for ($i = 0; $i < count($data); $i++) {
                if ($data[$i]['orderValue'] > 0) {
                    $order = $order + 1;
                    $orderVal = $orderVal + $data[$i]['orderValue'];
                }
                $Acode = $data[$i]['Trans_SlNo'];
                $Adate = $data[$i]['Adate'];
            }
        }
        $calls = count($data);

    $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $query = "select top 1 Pln_Date,sf_code,cluster,sf_member_code,(select count(ListedDrCode) from Mas_ListedDr MLDR where  charindex(','+ TP.cluster+',',','+ cast(MLDR.Territory_Code as varchar)+',')>0  and ListedDr_Active_Flag=0 and  division_code='".$Owndiv."'  group by Territory_Code) cnt  from TbMyDayPlan TP  where division_code='".$Owndiv."' and sf_code='".$sfCode."' and cast(convert(varchar,Pln_Date,101)as datetime)='".$date."'    order by Pln_Date desc";
        $rccount = performQuery($query);
		
	
		 $query = "exec getDayReportApp '" . $sfCode . "','" . $date . "'";
        $dayrep= performQuery($query);
		$result['today']['TotalEc']= $dayrep;
        $result['today']['RCCOUNT'] =  $rccount[0]['cnt'];
        $result['today']['order'] = $order;
        $result['today']['orderVal'] = $orderVal;
        $result['today']['calls'] = $calls;
        $result['today']['Adate'] = $Adate;
        $result['today']['Acode'] = $Acode;

        $query = "exec GetTargetAchDy '".$sfCode."','".date('d')."','".date('m')."','".date('Y')."'";
        $order = performQuery($query);
        $result['today']['AQty'] = $order[0]['AQty'];
        $result['today']['AAmt'] = $order[0]['AAmt'];

        $query = "select sum(order_Value) orderValue,count(h.Trans_SlNo) Trans_SlNo from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and MONTH(h.Activity_Date) = MONTH(getDate()) and YEAR(h.Activity_Date) = YEAR(getDate())";
        $month = performQuery($query);
        $query = "select count(h.Trans_SlNo) Trans_SlNo from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and MONTH(h.Activity_Date) = MONTH(getDate()) and YEAR(h.Activity_Date) = YEAR(getDate()) and POB_Value>0";
        $order = performQuery($query);
        $result['month']['order'] = $order[0]['Trans_SlNo'];
        $result['month']['orderVal'] = $month[0]['orderValue'];
        $result['month']['calls'] = $month[0]['Trans_SlNo'];
        $query = "exec GetTargetAch '".$sfCode."','".date('m')."','".date('Y')."'";
        $order = performQuery($query);

        $result['month']['TQty'] = $order[0]['TQty'];
        $result['month']['TAmt'] = $order[0]['TAmt'];
        $result['month']['AQty'] = $order[0]['AQty'];
        $result['month']['AAmt'] = $order[0]['AAmt'];
        outputJSON($result);
        break;
    case "entry/count":
        $today = date('Y-m-d 00:00:00');
        $sfCode = $_GET['sfCode'];

        $sql = "SELECT Employee_Id,case sf_type when 4 then 'MR' else 'MGR' End SF_Type FROM vwUserDetails where SF_code='" . $sfCode . "'";
        $as = performQuery($sql);
        $SFTyp = (string) $as[0]['SF_Type'];

        $query = "SELECT work_Type worktype_code,Remarks daywise_remarks,Half_Day_FW halfdaywrk,FWFlg from vwActivity_Report H where SF_Code='" . $sfCode . "'  and FWFlg <> 'F'and FWFlg <> 'HG' and FWFlg <> 'DS' and FWFlg <> 'DD' and cast(activity_date as datetime)=cast('$today' as datetime)";
        $data = performQuery($query);
        $result = array();
        if (count($data) > 0) {
            if($data[0]['FWFlg']=='DH'){
                $result["success"] = false;
                $query = "select Count(slno) hunt_count from DCRDetail_Distributors_Hunting D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)";
                $temp = performQuery($query);
                $result['count'] = $temp[0];
                $result['data'] = $data;
                outputJSON($result);
                die;
            }
            else{
                $result["success"] = false;
                $result['data'] = $data;
                outputJSON($result);
                die;
            }
        }
        $result["success"] = true;
        $result['data'] = getEntryCount();
        outputJSON($result);
        break;
        
    case "get/retailer":
          $id=$_POST['data'];
          $id = str_replace('"', "", $id);
          
           
        
         $query="exec GetRetailerDetails  '" . $id . "' ";
        outputJSON(performQuery($query));
        
        break;



    case "get/misseddates":
            $sfCode = $_GET['sfCode'];
          
           
        
         $query="select CONVERT(VARCHAR(10),Dcr_Missed_Date,111) as Dcr_Missed_Date  from DCR_MissedDates where Sf_Code='".$sfCode ."'  and Status=1 ";
        outputJSON(performQuery($query));
        
        break;
        case "get/AIretailer":
            $UKEY = $_GET['UKEY'];
          $Sf_Code=$_GET['sfCode'];
           
        $sql="select * from Mas_ListedDr where Ukey='" . $UKEY . "'";
        $code=performQuery($sql);
        outputJSON($code[0]);
        
        break;
        case "get/logouttime":
			$sfCode = $_GET['sfCode'];
			$data = json_decode($_POST['data'], true);
			$dt=date("Y-m-d h:i:sa");
			$ldt=date("Y-m-d");
			if($data["eDate"]!=null){
				$dt=$data["eDate"];
				$ldt=$data["eDate"];
				$sfCode=$data["sfCode"];
				$slfy=$data['slfy'];
				$Rmks=$data['Rmks'];
			}
			
			/*$myfile = fopen("../server/log_Trans_".date('Y-m-d').".txt", "a+");
			$sqlsp=$_POST['data']."|message:COT|sf_code=".$sfCode."\n";
			fwrite($myfile, $sqlsp);
			fclose($myfile);*/
			
			$sqlh ="insert into TP_Attendance_App_log select '$sfCode','$dt','" . $data["Lattitude"] . "','" . $data["Langitude"] . "','".$slfy."','0','','','','','','OUT'";
			$res=performQuery($sqlh);
			if (sqlsrv_errors() != null) {
				$result["success"]=false;
				$result["Msg"]="Some Error Occured";
				outputJSON($result);
				die;
			}
			$queryy = "exec [updAttnCheckOut] '" . $sfCode . "','" .  $dt . "','" . $data["Lattitude"] . "','" . $data["Langitude"] . "','".$slfy."'";
			performQuery($queryy);
			if (sqlsrv_errors() != null) {
				$result["success"]=false;
				$result["Msg"]="Some Error Occured";
				outputJSON($result);
				die;
			}
			/*
			$queryy = "insert into TB_AttnTrk select '" . $sfCode . "','" .  $dt . "','" . $data["Lattitude"] . "','" . $data["Langitude"] . "','".$slfy."','','".$ldt."',getDate()";
			performQuery($queryy);
			
			$queryy = "update Attendance_history set End_Time='" .  $dt . "',End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "',ESlfyPath='".$slfy."',Rmks='".$Rmks."' where  Sf_Code='" . $sfCode . "'  and login_date=cast(convert(varchar,cast('".$ldt."' as datetime),101) as datetime) and End_Time is null";
			performQuery($queryy);
			
			$query = "update TP_Attendance_App set End_Time='" .  $dt . "' ,End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "',ESlfyPath='".$slfy."',Rmks='".$Rmks."' where  Sf_Code='" . $sfCode . "'  and login_date=cast(convert(varchar,cast('".$ldt."' as datetime),101) as datetime) and End_Time is null";
			performQuery($query);
			
			$query = "update Login_Time_Table set End_Time='" .  $dt. "' ,End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "'  where  Sf_Code='" . $sfCode . "'  and login_date=cast(convert(varchar,cast('".$ldt."' as datetime),101) as datetime) and End_Time is null"; 
			performQuery($query);
			*/
			$result["success"] = true;
			$result["Query"] = $queryy;
			outputJSON($result);
        break;

case "get/Extendlogout":
			$sfCode = $_GET['sfCode'];
			$data = json_decode($_POST['data'], true);
			$dt=date("Y-m-d h:i:sa");
			$ldt=date("Y-m-d");
			$Ekey=$data["Ekey"];
			if($data["eDate"]!=null){
				$dt=$data["eDate"];
				$ldt=$data["eDate"];
				$sfCode=$data["sfCode"];
				$slfy=$data['slfy'];
				$Rmks=$data['Rmks'];
			}
			$queryy = "update Extended_entry set End_Time='" .  $dt . "',End_Lat='" . $data["Lattitude"] . "',End_Long='" . $data["Langitude"] . "',ESlfyPath='".$slfy."' where Ekey='".$Ekey."'";
			performQuery($queryy);
			
			$result["success"] = true;			
			outputJSON($result);
        break;
         case "upd/retailer":
        $data = json_decode($_POST['data'], true);
        $result = array();
        $query = "update Mas_ListedDr set ListedDr_Name='" . $data["name"] . "' , ListedDr_Address1='" . $data["Address"] . "', Doc_ClsCode='" . $data["ClassCode"] . "',doc_special_code='" . $data["SpecCode"] . "', ListedDr_Phone='" . $data["Phone"] . "',Doc_Spec_ShortName='" . $data["SpecName"] . "',Doc_Class_ShortName='" . $data["ClassName"] . "' ,
        
        
        cityname='" . $data["Cityname"] . "' ,areaname='" . $data["AreaName"] . "',contactperson='" . $data["ContactPerson"] . "',designation='" . $data["Designation"] . "',gst='" . $data["GSTno"] . "',pin_code='" . $data["PINcode"] . "',ListedDr_Phone2='" . $data["Phone2"] . "',contactperson2='" . $data["ContactPerson2"] . "',designation2='" . $data["Designation2"] . "',Land_Mark='" . $data["Land_Mark"] . "',contactperson3='" . $data["ContactPerson3"] . "',ListedDr_Phone3='" . $data["Phone3"] . "'
        
        
        
        where  ListedDrCode='" . $data["id"] . "'";
        $temp = performQuery($query);
        $result["success"] = true;
        outputJSON($result);
        break;  
        
        
    case "dcr/delorder":
        $sfCode = $_GET['sfCode'];
        $OrdNo = $_GET['OrdNo'];
        
        $query = "delete from Trans_Order_Details where Trans_Sl_No='".$OrdNo."'";
        performQuery($query);
        $query = "delete from Trans_Order_Head where Trans_Sl_No='".$OrdNo."'";
        performQuery($query);
        $result["success"] = true;
        outputJSON($result);
        break;
    case "get/Attendance":
        $sfCode = $_GET['sfCode'];
        $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $uRDt = $_GET['rptDt'];
        $query = "exec getMyDayPlanVwSFHry '" . $sfCode . "','" . $Owndiv . "','" . $uRDt . "'";
        outputJSON(performQuery($query));
        break;
    case "get/DYBInv":
        outputJSON(GetDailyInv());
        break;
    case "get/notify":
        outputJSON(GetNotifyLists());
        break;
case "get/TPnotify":
        outputJSON(GetTPNotifyLists());
        break;
    case "get/DAExp":
        outputJSON(getDailyAllow());
        break;
    case "get/Bills":
        outputJSON(getPendingBills());
        break;
    case "get/InvFlags":
        outputJSON(getInvFlags());
        break;
    case "get/Scheme":
        outputJSON(getSchemeDets());
        break;
    case "get/DailySales":
        outputJSON(getDailySalesSummry());
        break;

case "get/Todaycollectionreport":
    $sfCode = $_GET['sfCode'];
    $today = date('Y-m-d');
    $query = "exec GetToday_Collection_details '" . $sfCode . "','".$today."'";

outputJSON(performQuery($query));
        break;


case "get/DisabledDates":
    $sfCode = $_GET['sfCode'];
    $query = "select convert(varchar,login_date,23) dt from Tp_Attendance_App where SF_Code='".$sfCode. "' and login_date between '2020-06-01' and '2020-09-30'";
	outputJSON(performQuery($query));
	break;

case "get/LeaveAvailabilityCheck":
    $sfCode = $_GET['sfCode'];
    $Year = $_GET['Year'];
    
    $query = "select SFCode,LeaveCode,LeaveValue,LeaveAvailability,LeaveTaken,Leave_SName,Leave_Name from MasEntitlement  inner join mas_Leave_Type l on Leave_code=LeaveCode where SFCode='".$sfCode. "' and year(createDate)=year(getdate()) and l.Leave_code not in (39,25)";

outputJSON(performQuery($query)); 
        break;

case 'get/retailercountfdc':
   $sfCode = $_GET['sfCode'];
   $div = $_GET['divisionCode'];
   $today = date('Y-m-d');
   $divs = explode(",", $div . ",");
 $Owndiv = (string) $divs[0];
 $result = array();
  
               

  $query="exec [GET_Total_OutLetdashboard] '" . $Owndiv . "','','" . $sfCode . "','".$today."'";
  $Total_Cal=performQuery($query);
 
  $result['TotalCalls'] = $Total_Cal[0]['Total_Cal'];

  $query="exec [GET_Total_Prodashboard] '" . $Owndiv . "','','" . $sfCode . "','".$today."'";
  $proandunpro=performQuery($query);
  $result['Productive'] =$proandunpro[0]['Productive'];
  $result['Nonproductive'] =$proandunpro[0]['Nonproductive'];
  $query ="select count(distinct ListedDrCode) lrdr_count from Mas_ListedDr Ld where ld.division_code = '" . $Owndiv . "' and cast(CONVERT(varchar, ListedDr_Created_Date, 101) as datetime) = '".$today."'" ;
 $retailercount=performQuery($query);


 $result['Retailercount'] =$retailercount[0]['lrdr_count'];
 outputJSON($result);
    break;


case "save/ver":
    $sfCode = $_GET['sfCode'];
    $ver = $_GET['ver'];
    $today = date('Y-m-d H:i:s');
    $query = "insert into Trans_Version_Used select '" . $sfCode . "','".$today."','".$ver."'";

outputJSON(performQuery($query)); 
        break;
case "get/FieldDemoCategory":
  $div = $_GET['divisionCode'];
           $divs = explode(",", $div . ",");
 $Owndiv = (string) $divs[0];
           
        
         $query="select * from FieldDemoCategory where Divsion_Code='".$Owndiv."'";
        outputJSON(performQuery($query));
break;

case "get/BreedCategory":
  $div = $_GET['divisionCode'];
           $divs = explode(",", $div . ",");
 $Owndiv = (string) $divs[0];
           
        
         $query="select * from FieldDemoBreed where Divsion_Code='".$Owndiv."'";
        outputJSON(performQuery($query));
break;

case "get/SupplierMster":
  $div = $_GET['divisionCode'];
           $divs = explode(",", $div . ",");
 $Owndiv = (string) $divs[0];
            $sfCode = $_GET['sfCode'];
 $SF_code = $_GET['SF_code'];
 /* if($_GET['SF_Type']==0){
 $query ="SELECT sf_code = STUFF((SELECT     ',' + Sf_Code FROM mas_salesforce  where Reporting_To_SF='".$sfCode."'   FOR XML PATH ('')), 1, 1, '' ) FROM mas_salesforce   where Reporting_To_SF='".$sfCode."' " ;
 $SS_Sfcode=performQuery($query);
 $SS_map=$SS_Sfcode[0]['sf_code'].',';
 }else{
	$SS_map= $sfCode;
 } */
  

  
 //$query="select S_No id, S_Name name from supplier_master where charindex(','+sf_Code+',',','+'".$SS_map."'+',')>0  ";
 $query="exec [GetSuperStockistName] '" . $sfCode . "','" .  $Owndiv . "'  ";
 
outputJSON(performQuery($query));
break;

case "get/GiftCard":
        $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
         $query="select Gift_Name as name,Gift_Code as id  from Mas_Gift where Division_Code='" . $Owndiv . "'";
        outputJSON(performQuery($query));
break;

case "get/Mas_ClaimType":
       
         $query="select name as name,id,S_name from Mas_ClaimType";
        outputJSON(performQuery($query));
break;

case "get/Mas_RCPA":
$div = $_GET['divisionCode'];
       $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $query="select Product_Name as name,Sl_No as id  from Trans_RCPA_Product where Division_Code='" . $Owndiv . "'";

        outputJSON(performQuery($query));
break;
case "get/Mas_RCPA_Brand":
$div = $_GET['divisionCode'];
       $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $query="select Brand_Name as name,Sl_No as id ,Product_Id  from Trans_RCPA_Brand where Division_Code='" . $Owndiv . "'";

        outputJSON(performQuery($query));
break;
 case "get/mas_payment":
$div = $_GET['divisionCode'];
       $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
                $query = "select  id,Name name from Tarns_Payment_Type where Division_code='" . $Owndiv . "' and ActiveFlag=0";
               outputJSON(performQuery($query));
                break;
				
case "get/Productreason":
     $div = $_GET['divisionCode'];
     $divs = explode(",", $div . ",");
     $Owndiv = (string) $divs[0];
     $Trans_Sl_No = $_GET['Trans_Sl_No'];
     $query = "select Trans_Sl_No,Product_Code,Remarks,Trans_Order_No,Remarks_Id from Trans_Product_Remarks where Trans_Sl_No='" . $Trans_Sl_No . "'";
	 
               outputJSON(performQuery($query));
                break;
case "get/tracksetup";
	      outputJSON(getTrackSetUP());
	      break;
				
case "get/RetailerPaymentCollection":
      $SF_code = $_GET['sfCode'];
      $today = date('Y-m-d');
     $query = "select Stockist_Name Name,sum(Collected_Amount) Amout,Pay_Type from trans_priorder_head  PH inner join mas_stockist Ms on MS.Stockist_Code=PH.Stockist_Code where PH.sf_code='" .  $SF_code  . "' and convert(date,Order_Date)='" .  $today   . "'   group by PH.Stockist_Code,Pay_Type,Collected_Amount,Stockist_Name";
	 
     outputJSON(performQuery($query));
			   
                break;
	case "get/LitterVsValues":
		$SF_code = $_GET['SF_Code'];
	  
		$today =  $_GET['Date'];
		$query = "exec getLittervsValue '" .  $SF_code  . "' ,'" .  $today . "' ";
		outputJSON(performQuery($query));
        break;	
		
 case "get/GoogleLogin":
		$Email = $_GET['Email'];  //CASE WHEN  UsrDfd_UserName='HAPHTNSAO2' THEN '18.7357416:78.9700081' ELSE '12.91729615:80.23030043' END 
		$DeviceRegId = $_GET['DvID'];
		//(select count(Sf_Code) from Audit_Permission where sf_code=ms.sf_code)
		$sql = "SELECT  UsrDfd_UserName Sf_UserName,(select count(Sf_Code) from Audit_Permission where sf_code=ms.sf_code) BlkRelease ,iif(SF_Status!=0,3,SF_Status) SF_Status,Sf_Name,Sf_Password,ms.Division_Code,DisRad,ms.Sf_code,(select count(Reporting_To_SF) from mas_salesforce where   Reporting_To_SF=ms.Sf_code)CheckCount,Geo_Fencing,
		case when Approved_By='FF' then 1 else 0 end SFFType,case when Approved_By!='FF' and Approved_By!='I' then 1 else 1 end OTFlg,
		isnull(latlong,'12.91729615:80.23030043') AS HOLocation from mas_salesforce ms inner join Mas_HQuarters m on cast(HQ_ID as varchar)=SF_HQ  inner join access_master am on  charindex(','+  cast(am.division_code as varchar) +',',','+ms.Division_Code+',')>0   inner join   access_table AT on At.Sf_code= ms.Sf_code where SF_Email='".$Email."' and Approved_By<>'I'";
		$res = performQuery($sql);
		
		$result["success"]=true;
			
		if (count($res) > 0) {
			$result["Data"]=$res;
			$sql = "insert into LoginDetails select '" . $res[0]['Sf_code'] . "',getdate(),getdate()";
			performQuery($sql);
				$result["Qry"]=$sql;
			if ($DeviceRegId!=null&&!empty($DeviceRegId)) {
				$sql = "update Access_Table set  DeviceRegId='$DeviceRegId' where sf_code='" . $res[0]['Sf_code'] . "'";
				performQuery($sql);
			}
			if($res[0]["SF_Status"]!=0){
				$result["success"]=false;
				$result["BlockMsg"]='Your Check-In ID is blocked. For further details contact HO HR Department';
			}else {$result["BlockMsg"]='';}
		}else{
		   $result["Query"]=$sql;
			$result["BlockMsg"]='Your Check-In ID is blocked. For further details contact HO HR Department';
		   $result["success"]=false;
		}
		/*if($_GET['Email']=="anbu@saneforce.com"){
			$result["Query"]=$sql;
			$result["BlockMsg"]='New app update available for Leave. please install from google playstore <a href="https://hap.sanfmcg.com/" target="_blanck">Click here</a>';
		   $result["success"]=true;
		}*/
		$result["PayServer"]="sf.hap.in";//"182.73.72.3";
		$result["PayUser"]="hapftp";
		$result["PayPW"]='VFAY$du3@=9^';
		$result["PayRMPath"]="/home/hapftp/Payroll/";
			
		outputJSON($result);			
		break;
case "get/calpriod":
		$typ = $_GET['id'];  //CASE WHEN  UsrDfd_UserName='HAPHTNSAO2' THEN '18.7357416:78.9700081' ELSE '12.91729615:80.23030043' END 
		$Dt=date('Y-m-23');
		$query = "select convert(varchar,dateadd(month,iif(day(getdate())<23,-2,-1),cast('$Dt' as datetime)),23) MinDate,
		convert(varchar,dateadd(day,-1,dateadd(month,iif(day(getdate())<23,1,2),cast('$Dt' as datetime))),23) MaxDate ";//convert(varchar,getdate(),23)
		outputJSON(performQuery($query));		
		break;		
	case "get/geotag":
		outputJSON(ViewGeoTag());
		break;					
	case "get/Stockist_Primary":
		$SF_code = $_GET['sfCode'];
		$today = date('Y-m-d H:i:s');
	  
		$div = $_GET['divisionCode'];
		$divs = explode(",", $div . ",");
		$Owndiv = (string) $divs[0];
		$query ="exec get_new_qnty '".$SF_code."','".$today."' ,'".$Owndiv."' ";
		outputJSON(performQuery($query));
			   
		break;
	case "get/FieldForce_HQ":
		$div = $_GET['divisionCode'];
		$divs = explode(",", $div . ",");
		$SF_code = $_GET['sfCode'];
		$Owndiv = (string) $divs[0];
		$query = "select Sf_HQ from mas_salesforce where   sf_code='".$SF_code."'";
		$Sf_HQ=performQuery($query);

		$query = "select HQ_ID id,HQ_Name name,AvlShifts ODFlag from Mas_HQuarters where Division_code='" . $Owndiv . "' and HQ_Active_Flag=0 order by HQ_Name";
		outputJSON(performQuery($query));
		break;		
	case "get/Signout_Check":
        $div = $_GET['divisionCode'];
		$divs = explode(",", $div . ",");
	    $SF_code = $_GET['Sf_code'];
        $Owndiv = (string) $divs[0];
		$today = date('Y-m-d');
		$Flag=0;
		$query = "exec CheckClearFlag '".$SF_code."' ";  
        $Response=performQuery($query);
		if (count($Response) > 0) {
		   $Flag=$Response[0]['flag'];	
		}
		else{
			$Flag=1;
			
		}
		if ($Flag==0){
			$query = "select top 1 isnull(Sft_Name,'ON-Duty') Sft_Name,Start_Time,  convert(varchar, Start_Time, 8) Edate ,End_Time,isnull(cast(convert(varchar,login_date-1,101) +' '+ convert (varchar,sft_STime,108) as datetime),Start_Time) as Sft_STime,isnull(sft_ETime,cast(convert(varchar,Login_date,101) +' 23:59:59' as datetime)) sft_ETime,0 Wtp ,Sft_ID ,case when  isnull(End_Time,0)=0 then 0 else 1 end [Type],(case when ACtOffTyp=0 or ShiftSelected_Id=0 then cast(convert(varchar,Login_date,101) +' 23:59:59' as datetime) else dateadd(hour,ACtOffHrs,cast(convert(varchar,(case when(Sft_STime>sft_ETime) then dateadd(day,1,login_date) else login_date end),101) +' '+ convert (varchar,sft_ETime,108) as datetime)) End) ACutOff  from TP_Attendance_App Tp left outer join mas_shift_timings mst on  mst.Sft_ID=Tp.ShiftSelected_Id where sf_code='".$SF_code."'  order by  Start_Time  desc";//'".$today."'
			$result=performQuery($query);
		$result[0]["PayServer"]="sf.hap.in";//"182.73.72.3";
		$result[0]["PayUser"]="hapftp";
		$result[0]["PayPW"]='VFAY$du3@=9^';
		$result[0]["PayRMPath"]="/home/hapftp/Payroll/";
			//$query = "select top 1 Sft_Name,Start_Time,  convert(varchar, Start_Time, 8) Edate ,End_Time,cast(convert(varchar,login_date-1,101) +' '+ convert (varchar,sft_STime,108) as datetime) as Sft_STime,sft_ETime,0 Wtp ,Sft_ID ,case when  isnull(End_Time,0)=0 then 0 else 1 end [Type],(case when ACtOffTyp=0 then cast(convert(varchar,Login_date,101) +' 23:59:59' as datetime) else dateadd(hour,ACtOffHrs,cast(convert(varchar,(case when(Sft_STime>sft_ETime) then dateadd(day,1,login_date) else login_date end),101) +' '+ convert (varchar,sft_ETime,108) as datetime)) End) ACutOff  from TP_Attendance_App Tp inner join mas_shift_timings mst on  mst.Sft_ID=Tp.ShiftSelected_Id where sf_code='".$SF_code."'  order by  Start_Time  desc";//'".$today."'
			outputJSON($result);
		}	
		else{
			$arr=[];
			outputJSON($arr);
		}
     break;
case "get/ClearData":
        $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
	    $SF_code = $_GET['Sf_code'];
        $Owndiv = (string) $divs[0];
		$today = date('Y-m-d');
		
        $query = "exec CheckClearFlag '".$SF_code."' ";  
        $Response=performQuery($query);
		if (count($Response) > 0) {
		   $result["success"] = true;
		   $result["Flag"]=$Response[0]['flag'];	
		}else{
			
			$result["Flag"]=1;
			$result["success"] = true;
		}
        outputJSON($result);
     break;

	case "get/tknPerm":
        $PDt = $_GET['PDt'];
		$sfCode = $_GET['rSF'];
		$mn=date('m',strtotime($PDt));
		$yr=date('Y',strtotime($PDt));
		$mDt=date('Y-m-d',strtotime($yr.'-'.$mn.'-23'));
		//$mDt=date('Y-m-23');
		if(date('d',strtotime($PDt))<23) $mDt = date('Y-m-d',strtotime($mDt."- 1 months"));
		$fDt=date('Y-m-d',strtotime($mDt));
		$mDt = date('Y-m-d',strtotime($mDt."+ 1 months"));
		$mDt = strtotime($mDt."- 1 day");
		$tDt=date('Y-m-d',$mDt);
		$query = "select isnull(sum(cast(Noof_hours as int)),0)  tknHrS from PermissionEntry where Approval_Flag!=1 and SF_Code='".$sfCode."' and cast(Permissiondate as date) between '".$fDt."' and '".$tDt."'"; 
		//echo($query);
		outputJSON(performQuery($query));
		break;	

	case "get/Shift_timing":
        $div = $_GET['divisionCode'];
		$divs = explode(",", $div . ",");
	    $Owndiv = (string) $divs[0];
		$sfCode = $_GET['Sf_code'];
		//;
        $query = "select Sft_ID id,Sft_Name name,Sft_STime, cast(convert(varchar,(case when Sft_STime>Sft_ETime then dateadd(day,1,getDate()) else getdate() End),101) +' '+ convert (varchar,sft_ETime,108) as datetime) as   sft_ETime from mas_shift_timings where Division_code='" . $Owndiv . "' and ActiveFlag=0 and Sft_ID<17"; 
		if ($sfCode!=null){
			$query = "exec getShiftTimesBySF '" . $sfCode . "'";
		}
		outputJSON(performQuery($query));
		break;	
case "Save/DivInf":
	$Mod = $_GET['Mode'];
	$data = json_decode($_POST['data'], true);
	$emails =$data['accounts'];
	for($i=0;$i<count($emails);$i++){
		$Email=$Email.$emails[$i]["email"].',';
	}
	$query = "select isnull(Max(Id),0)+1 id from Trans_Fault_Report";
	$arr=performQuery($query);
	$query = "insert into Trans_Fault_Report select ".$arr[0][id].",'".$Mod."','".date('Y-m-d H:i:s')."','".$Email."','".$data['deviceID']."','".$data['phoneNo']."','".$data['netCountry']."','".$data['netName']."','".$data['simNo']."','".$data['simCountry']."','".$data['simName']."','".$data['userAgent']."','".$data['systemBoardID']."','".$data['systemManufacturer']."','".$data['systemVersion']."','".$data['systemName']."','".$data['adId']."'";
	performQuery($query);
	$result["success"] = true;
    outputJSON($result);
	break;	
case "save/newtrack":

	$data = json_decode($_POST['data'], true);
	$sfCode = (string) $data['SF'];
	$sql = "select sf_emp_id,Employee_Id from Mas_Salesforce where Sf_Code='$sfCode'";
            $sf = performQuery($sql);
            $empid = $sf[0]['sf_emp_id'];
            $employeeid = $sf[0]['Employee_Id'];
		$date = (string) $data['date'];
		$lat = (string) $data['latitude'];
		$lng = (string) $data['longitude'];
		$acc = (string) $data['theAccuracy'];
		$bat = (string) $data['Btry'];
		$mock = (string) $data['mock'];


		$sql = "insert into tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr,Auc,deg,DvcID,Battery,Mock) select '$sfCode','$empid','$employeeid','".$data['date']."','" . $lat . "','" . $lng . "','null','" . $acc . "','null','null','".$bat."','".$mock."'";
 performQuery($sql);
$result["success"]=true;
outputJSON($result);

	break;
	
	
	
	case "Store/IME":
        $Sf_Code = $_GET['Sf_Code'];
        $IME = $_GET['IME'];
		$Sf_Name = $_GET['Sf_Name'];
		$Email = $_GET['Email'];
        $today = date('Y-m-d H:i:s');
		$sql = "SELECT isNull(max(ID)+1,0) as id FROM Trans_IME_Info";
        $tr = performQuery($sql);
        $maxIntNo = $tr[0]['id'];
        $query = "insert into Trans_IME_Info select $maxIntNo,'".$Sf_Code."','".$today."','".$IME."' ,'".$Sf_Name."' ,'".$Email."' ";
        performQuery($query);
        
        $result["success"] = true;
        outputJSON($result);
        break;
	
	
	case "Check/Version":
		//$Sf_Code = $_GET['Version'];
		$Version = json_decode($_POST['Version'], true);
		$VERSION = (string)$Version;
		$Ver = 'Ver 3.0.4';
        
		$result["btTitle"]="UPDATE YOUR APP";//"Can't open at this time."
		if($VERSION==$Ver || $Ver == 'Ver 3.0.5' || $Ver=='Ver 2.4.0' || $VERSION=='Ver 3.0.6'){ //|| $VERSION=="Ver 2.3.1" || $VERSION=="Ver 2.3.2" || $VERSION=="Ver 2.2.2"
			$result["Msg"] = "";
			$result["success"] = true;
		}else{
			$result["Msg"] = "APP Update Available. Kindly Update the APP.";//\n if not open playstore kindly goto playstore from app menu";//a href='https://play.google.com/store/apps/details?id=com.hap.checkin'&gt;click hear&lt;/a&gt;";
			$result["success"] = false;
		}
       
        outputJSON($result);
        break;
		
		
		case "CalTotime":
		$selectedTime = $_GET['start_at'];
		if($_GET['Shift_TimeFlag']=='1'){
				$endTime = strtotime("+60 minutes", strtotime($selectedTime));
        }else{
				 $endTime = strtotime("+120 minutes", strtotime($selectedTime));
			}
			
			
			//$EndTimecal= date('g:i a', $endTime);
			$EndTimecal= date('H:i:s', $endTime);
			$result["success"] = true;
			$result["Totime"] =$EndTimecal;
			outputJSON($result);
		break;
		
		case "get/WrkDates":
        $Sf_Code = $_GET['sfCode'];
        //$query = "select case when NoofHrs<8 then 0.5 else 1 end EligDys,* from ( select convert(varchar(10),convert(date,Start_Time),105) name,id ,  convert(char(5), cast(Start_Time as time),108) InTime ,convert(char(5), cast(End_Time as time),108) OutTime,CAST(DATEDIFF(HOUR, cast(Start_Time as time), cast(End_Time as time)) as INT) NoofHrs from TP_Attendance_App where Sf_Code='".$Sf_Code."' and    Duty_Mode=1  and End_Time is not null)A where NoofHrs>4";
		$query = "exec GetholidayCount '".$Sf_Code."'";
		
		outputJSON(performQuery($query));
        break;
}
?>