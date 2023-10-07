<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');

//session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

function sendFCM_notify($registrationIds,$message,$title,$msgId,$event_id){
	// API access key from Google API's Console
	// saneforceapps@gmail.com
	define('FIREBASE_API_KEY', 'AAAAFK0ZQHQ:APA91bFW4GKUQJIVmqp0NcuCeyPr6iPvX-3YBEzwZiRJjlIMMbrIhON5Kmeej4yilS6aXGuag-8ZPBVGM7kGm6iiG1hSwu7E3CgatGtgBq43viEqq6eBP4MkoRh_yPDQuarBzCuYCdgZ');
	// apps@saneforce
	//	define('FIREBASE_API_KEY', 'AAAAcGR_GsE:APA91bF1TfP0uyfAjiAHyaXTFsx3RDQa60_YddGg7v_ZEj_OkkKo7b87tuaEMCgWPn3bPcnBBR5eFjguoHCEPpFdqUYru4ienr6Buylb5JMFEoNNIq-taMkFA-Ozh2n7S9ywjAO50jW5');
	//define('FIREBASE_API_KEY', 'AAAAEDz9WFs:APA91bEKbRUwSNA4kU8_q6B6TZqU95zIU8eYCcVAa-ioUt1L9o8hO_VHHOwi8gxaa89HeswDEcldNfNRExY-SfJRpesZ2FUXgFf3QsgCOtsBC-0fE0ZOFhHfSBUpubD1HAq3a_oMTUSP');
	//define('FIREBASE_API_KEY', 'AAAAFZqP7mw:APA91bG9q_tSrr3sjaHiMo8fqBI18z8z3KWxsmFeL3L_9AMuLnUoT3kRsmTH2DmvRHZrqSJn9nfVqQZGPmYvG4Z6skH304xWp2Wa7kz_jTVIsGI63t4PwZxtHjbeH-sqXMESogFb5QJ_');

	$msg = array
	(
		'text' 	=> $message,
		'title'		=> $title,
		'subtitle'	=> 'Request Alert!',
		'tickerText' => 'Request Alert!',
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
function sendSMS(){
    $curl = curl_init();
	$txt=urlencode("Your DCR of vari) is time barred. Now, it can not be accepted or registered - EFORCE");
	$url="http://49.50.67.32/smsapi/httpapi.jsp?username=ESOFT&password=160440&from=EFORCE&to=9962111203&text=".$txt."&pe_id=1001750393149918382&template_id=1007055380941095008";
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
	$result = curl_exec($curl);
	curl_close($curl);
    echo($result);

}
function getPmOrderDet() {
    $orderNo = $_GET['Order_No'];
    $trans_detail_slNo = $_GET['Trans_Detail_SlNo'];
    $query = "exec get_pm_order_details'" . $orderNo . "','" . $trans_detail_slNo . "'";
    return performQuery($query);
}
function getCheckinList(){
$Sf_Code=$_GET['sfCode'];

$C_date=$_GET['c_date'];
//$query="select * from Trans_Cus_Checkin  where Sf_Code='" .$Sf_Code . "' and Entry_Date = cast(GETDATE() as date)";
$query="select * from Trans_Cus_Checkin  where Sf_Code='" .$Sf_Code . "' and Entry_Date = '".$C_date."' ";  

return performQuery($query);
}

function getInshopStockList(){
	$slNo = $_GET['Checkin_No'];
      $Sf_Code = $_GET['SF'];
   
    $query = "exec getInshopDet '".$Sf_Code."','" . $slNo . "'"; 
    return performQuery($query);
}

function getSecCallDets(){
$Sf_Code=$_GET['sfCode'];
$Tran_Slno=$_GET['trans_SlNo'];

$query="select dcr.Trans_SlNo,dcr.Trans_Detail_Slno,dcr.Trans_Detail_Name,dcr.Trans_Detail_Info_Code,dcr.SDP_Name,dcr.POB,dcr.Worked_with_Name,dcr.StartOrder_Time,head.Trans_Sl_No,head.DCR_Code,head.Stockist_Code,head.Order_Value,head.Order_In_Time,head.Order_Out_Time,dcr.Activity_Remarks,isnull(aec.imgurl,'')audioName
      from DCRDetail_Lst_Trans dcr left outer join Trans_Order_Head head on dcr.Trans_Detail_Slno=head.DCR_Code left join Activity_event_captures aec on aec.DCRDetNo=dcr.Trans_Detail_Slno and  aec.Identification='audio' where dcr.Trans_SlNo='".$Tran_Slno."' ";
return performQuery($query);
}
function getSecOrderDets(){
$Sf_Code=$_GET['sfCode'];
$Order_No=$_GET['Order_No'];

$query="SELECT  Product_Code,Product_Name,Quantity,value  FROM trans_order_details where Trans_Sl_No='".$Order_No."' ";
return performQuery($query);
}
function getrptCheckinList(){
$Sf_Code=$_GET['sfCode'];
$C_date=$_GET['c_date'];	
$type=$_GET['Entry_Type'];	
$query="select * from Trans_Cus_Checkin  where Sf_Code='" .$Sf_Code . "' and Entry_Date = '".$C_date."' and Entry_Type='" .$type. "' ";  
return performQuery($query);
}
function getPopList(){
$div=$_GET['divisionCode'];
 $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

$query="select * from POP_Material_master where Division_Code='".$Owndiv."'";
return performQuery($query);
}
function getPopStock(){
$SfCode=$_GET['sfCode'];
  $date=date('Y-m-d');
//$query="select * from Trans_Pop_Stock_Loading where Sf_Code='".$SfCode."' and Stock_Date = cast(GETDATE() as date)";
$query="exec getPopStockAndSale '".$SfCode."','".$date."'";
return performQuery($query);

}
function getPopOrderDetails(){
      $Sf_Code=$_GET['sfCode'];
 $pop_date=$_GET['popDate'];
      $query="select * from Trans_Pop_dcr_details where Sf_Code='" .$Sf_Code . "' and Dcr_Date='" .$pop_date."'";
      return performQuery($query);
}
function getInshopDetails(){
 $Sf_Code=$_GET['sfCode'];
 $sl_no=$_GET['slNo'];
 $sql="exec getInshopDet '".$Sf_Code."','".$sl_no."' ";
 return performQuery($sql);
}
function getPendingPaymentDets(){
      $div=$_GET['divisionCode'];
      $divs = explode(",", $div . ",");
      $Owndiv = (string) $divs[0];
      $stk_code=$_GET['stockist_code'];
      $cus_code=$_GET['Customer_Code'];
      $From_Year=$_GET['From_Year'];
      $To_Year=$_GET['To_Year'];
      $From_Month=$_GET['From_Month'];
      $To_Month=$_GET['To_Month'];
      $Type=$_GET['Type'];
      $sql="exec get_pending_payment_details '".$stk_code."','".$Owndiv."','".$cus_code."','".$From_Year."','".$To_Year."','".$From_Month."','".$To_Month."','".$Type."' ";
      return performQuery($sql);
}
function actionLogin() {
    global $URL_BASE;
    $data = json_decode($_POST['data'], true);
    $username = (string) $data['name'];
    $password = (string) $data['password'];
    $DeviceRegId = (string) $data['DeviceRegId'];
    $loginDeviceId = '';
	if($data['deviceId'] != null)
    $loginDeviceId = (string) $data['deviceId'];

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
    $respon = array();
        $respon['Qry'] = $query;
    $count = count($arr);
    $DIVV=str_replace(",", "", $arr[0]['Division_Code']);
	if($data['date']!=null)
		$logindate=str_replace("'", "", $data['date']);
	else
	$logindate=date('Y-m-d');
    if($data['time']!=null)
			$logintime=str_replace("'", "", $data['time']);
	else
	$logintime=date("h:i:sa");
    $query = "insert into Login_Time_Table(Sf_Code,Division_Code,Start_Time,End_Time,Start_Lat,Start_Long,End_Lat,End_Long,login_date) VALUES ('" . $arr[0]['SF_Code'] . "', '" .  $DIVV . "', '".$logintime."','','','','','','".$logindate."')";
    performQuery($query);

	 $updatedeviceIdQuery = "update Access_Table set DeviceId = '". $loginDeviceId ."' where sf_code = '" . $arr[0]['SF_Code'] . "'";
     performQuery($updatedeviceIdQuery);

    if ($count == 1) {
        $respon['success'] = true;
        $respon['sfCode'] = $arr[0]['SF_Code'];
        $sfName = utf8_encode(trim(preg_replace("/[\r\n]+/", " ", $arr[0]['SF_Name'])));
        $respon['sfName'] = $sfName;
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
		
        $queryseccall="select count(*)sec_ord from DCRDetail_lst_Trans where sf_code='" . $arr[0]['SF_Code'] . "' and cast(ModTime as date)=cast(GETDATE() as date)";
		$callcount=performQuery($queryseccall);
		$respon['secOrdToday'] =$callcount[0]['sec_ord'];
		
		$querypricall="select count(*)pri_ord from DCRDetail_CSH_Trans where sf_code='" . $arr[0]['SF_Code'] . "' and cast(ModTime as date)=cast(GETDATE() as date)";
		$pricallcount=performQuery($querypricall);
		$respon['priOrdToday'] =$pricallcount[0]['pri_ord'];
        $dat=date('Y-m-d');
        $sql="select * from TP_Attendance_App where Sf_Code='" . $arr[0]['SF_Code'] . "' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$dat'";
        $attendance=performQuery($sql);
        if(count($attendance)==0)
            $respon['attendanceView'] = 0;
        else
            $respon['attendanceView'] = 0;


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
       $respon['Selfie'] = $arr[0]['Selfie'];
       $respon['quiz_need_mandt'] = $arr[0]['quiz_need_mandt'];
	   $respon['quiz_need'] = $arr[0]['quiz_need'];
	   	   $respon['scheme_name'] = $arr[0]['scheme_name'];
		   $respon['retailer_only'] = $arr[0]['retailer_only'];
		   $respon['Url_Short_Name'] = $arr[0]['Url_Short_Name'];
		   
		     $respon['SecOrdValConfAppr'] = $arr[0]['SecOrdValConfAppr'];
			 $respon['TPDCR_Deviation'] = $arr[0]['TPDCR_Deviation'];
			  $respon['MyplnEventCapture'] = $arr[0]['Mypln_Event_Capture'];
			   $respon['proddet'] = $arr[0]['Prod_Det'];
			   $respon['TP_Remainder_Date'] = $arr[0]['TP_Remainder_Date'];
			   $respon['TP_Mandatory_ND'] = $arr[0]['TP_Mandatory_ND'];
			   $respon['TP_ND'] = $arr[0]['TP_ND'];
			   
			   $query = "select Profile_Pic from Mas_Salesforce where Sf_Code = '". $arr[0]['SF_Code'] ."'";
			   $queryRes = performQuery($query);
			   $profilePic = $queryRes[0]['Profile_Pic'];
			   if($profilePic!=null && $profilePic != ''){
				   $profilePic = "http://www.fmcg.sanfmcg.com/SalesForce_Profile_Img/" . $profilePic;
			   }
				   
			   $respon['Profile_Pic'] = $profilePic;


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
   $query = "exec getAppProdStockist    '".$Owndiv."','".$subdivision[0]['Sub_Division_Code']."'   ";


		}
    else{
        $query = "exec getAppProdNative '" . $sfCode . "'"; //,'".$DivisionCode."'";

	}
 return performQuery($query);
}


function getAPPSetups() {
    $rqSF = $_GET['rSF'];
    $userType = $_GET['userType'];
    $query = "exec getAPPSetups '" . $rqSF . "'";
    return performQuery($query);
}

function getSlidesGroups(){
	$data = json_decode($_POST['data'], true);
	$sfCode = (string) $data['SF'];
	$div = (string) $data['div'];
	$query = "exec Detl_getGroupName '".$sfCode."'";
	return performQuery($query);
}

function getSlidesDetails(){
	$data = json_decode($_POST['data'], true);
	$sfCode = (string) $data['SF'];
	$div = (string) $data['div'];
	$query = "exec Detl_getGroupSlides '".$sfCode."'";
	return performQuery($query);
}
function saveSlidesTrack(){
	$data = json_decode($_POST['data'], true);
	$SF_Code = $_GET['SF_Code'];
	$Div_Code = $_GET['Div_Code'];
	$OrderType=$_GET['OrderType'];
    $RetailerCode=$_GET['RetailerID'];
    $RetailerName=$_GET['RetailerName'];
	$CreatedDate = date("Y-m-d H:i:s");

	$Trans_SlNo = 'SD-'.str_replace(['PGDB','MGR','MR'],'',$SF_Code).date("ymds");
	$query_1 = "INSERT INTO Trans_Detailing_Head SELECT '".$Trans_SlNo."','".$SF_Code."','".$Div_Code."','".$OrderType."','".$RetailerCode."','".$RetailerName."','".$CreatedDate."'";
	performQuery($query_1);

	$SlideDetailing = (array) $data['Detailing'];
	for($i=0; $i<count($SlideDetailing); $i++){
      $GroupCode=$SlideDetailing[$i]['Code'];
      $GroupName=$SlideDetailing[$i]['Name'];
      $Group_Timesline=$SlideDetailing[$i]['Timesline'];
      $GroupStartTime=$Group_Timesline['sTm'];
      $GroupEndTime=$Group_Timesline['eTm'];
      $GroupRating=$SlideDetailing[$i]['Rating'];
      $GroupFeedbk=$SlideDetailing[$i]['ProdFeedbk'];
      $Mod=$SlideDetailing[$i]['Mod'];
      $SmpQty=$SlideDetailing[$i]['SmpQty'];
      $Type=$SlideDetailing[$i]['Type'];
	  //$TransDG_SlNo = 'DG-'.str_replace(['PGDB','MGR','MR'],'',$SF_Code).substr(time(),4).$i;
	  $TransDG_SlNo = 'DG-'.str_replace(['PGDB','MGR','MR'],'',$SF_Code).date("dHms");

      $query_2 = "INSERT INTO Trans_DetailingGroup_Detail SELECT '".$Trans_SlNo."','".$TransDG_SlNo."','".$GroupCode."','".$GroupName."','".$GroupStartTime."','".$GroupEndTime."',DATEDIFF(S, '".$GroupStartTime."','".$GroupEndTime."'),'".$GroupRating."','".$GroupFeedbk."','".$Mod."','".$SmpQty."','".$Type."'";
      performQuery($query_2);

      $Slides = (array) $SlideDetailing[$i]['Slides'];

      for($j=0; $j<count($Slides); $j++){
        $Slide_Timesline = $Slides[$j]['Times'];
        $SlideStartTime = $Slide_Timesline['sTm'];
        $SlideEndTime = $Slide_Timesline['eTm'];

		$query_3 = "INSERT INTO Trans_DetailingSlide_Detail SELECT '".$TransDG_SlNo."','".$GroupCode."','".$Slides[$j]['SlideId']."','".$Slides[$j]['Slide']."','".$Slides[$j]['SlideType']."','".$SlideStartTime."','".$SlideEndTime."',DATEDIFF(S,'".$SlideStartTime."','".$SlideEndTime."'),'".$Slides[$j]['SlidePath']."','".$Slides[$j]['SlideRating']."','".$Slides[$j]['SlideRem']."'";
		performQuery($query_3);
      }
    }
	$result = array();
	$result['success'] = true;
    return $result;
}

function getDetailingGroupwiseMonthlyRpt(){
	$data = json_decode($_POST['data'], true);
	$sfCode = (string) $data['SF'];
	$div = (string) $data['div'];
	$rpt_date = (string) $data['rpt_date'];
	$query = "exec Detl_getGroupwiseMonthlyRpt '".$sfCode."','".$rpt_date."'";
	return performQuery($query);
}
function getDetailingSlidesRpt(){
	$data = json_decode($_POST['data'], true);
	$sfCode = (string) $data['SF'];
	$div = (string) $data['div'];
	$rpt_date = (string) $data['rpt_date'];
	$GroupCode = (string) $data['GroupCode'];
	$query = "exec Detl_getGroupWiseSlideRpt '".$sfCode."','".$rpt_date."','".$GroupCode."'";
	return performQuery($query);
}

function getPaymentTypeList(){
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

	$query = "select Code,Name from Mas_Payment_Type where Division_Code = '". $Owndiv ."'";

	return performQuery($query);
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
function getPriSchemeDets() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

    $query = "exec getProdPriScheme '" . $Owndiv . "'";
    return performQuery($query);
}
function getStockistSchemeDets(){
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

    $query = "exec getProdStockistScheme '" . $Owndiv . "'";
    return performQuery($query);

}
function getCaseSchemeDets() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

    $query = "exec getProdPriCaseScheme '" . $Owndiv . "'";
    return performQuery($query);
}
function getRandomnumber() {
	$query = "select  ID id,Name name  from RandomNumber order by ID";
    return performQuery($query);
}
function getUnitConversion() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

    $query = "select  UOM_Id id,PCode Product_Code,UOM_Nm name,CnvQty ConQty  from vwMas_MUnitDets where Division_Code='" . $Owndiv . "'";
    return performQuery($query);
}



function getSlabDetails() {
$div = $_GET['divisionCode'];
$divs = explode(",", $div . ",");
$Owndiv = (string) $divs[0];
$sfCode = $_GET['sfCode'];
$query="select Sf_HQ,Hq_Code from mas_salesforce where Sf_Code='" . $sfCode . "' ";
$Sf_Hq=performQuery($query);
$Hqcode=$Sf_Hq[0]['Hq_Code'];
/*
if(strpos($Sf_Hq[0]['Sf_HQ'],"DELHI")>=0 && strpos($Sf_Hq[0]['Sf_HQ'],"DELHI")!=""){
$query = "select RetSlabID id,SlabName name, SlabName ,month(From_dt)SlabMonth,year(From_dt)SlabYear,SlabMinVal,SlabMaxVal,CONVERT(varchar,To_Dt, 101)To_Date from Mas_Retail_Business_Slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and RetSlabID not in (89,90,91,92,93,99,100,101,102,103) and RetSlabID<>94
union
select RetSlabID id,SlabName name, SlabName ,month(From_dt)SlabMonth,year(From_dt)SlabYear,SlabMinVal,SlabMaxVal,CONVERT(varchar,To_Dt, 101)To_Date from Mas_Retail_Business_Slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and RetSlabID not in (89,90,91,92,93) and RetSlabID=94 and 'MGr1459'='" . $sfCode . "'";

}else{
	if((strpos($Sf_Hq[0]['Sf_HQ'],"DELHI")>=0 && strpos($Sf_Hq[0]['Sf_HQ'],"DELHI")!="") || (strpos($Sf_Hq[0]['Sf_HQ'],"Jabalpur")>=0 && strpos($Sf_Hq[0]['Sf_HQ'],"Jabalpur")!="") || (strpos($Sf_Hq[0]['Sf_HQ'],"Gwalior")>=0 && strpos($Sf_Hq[0]['Sf_HQ'],"Gwalior")!="") || (strpos($Sf_Hq[0]['Sf_HQ'],"Indore - 1")>=0&& strpos($Sf_Hq[0]['Sf_HQ'],"Indore - 1")!="") || (strpos($Sf_Hq[0]['Sf_HQ'],"Indore - 2")>=0 &&strpos($Sf_Hq[0]['Sf_HQ'],"Indore - 2")!="")){
	$query = "select RetSlabID id,SlabName name, SlabName ,month(From_dt)SlabMonth,year(From_dt)SlabYear,SlabMinVal,SlabMaxVal,CONVERT(varchar,To_Dt, 101)To_Date from Mas_Retail_Business_Slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and RetSlabID not in (89,90,91,92,93,99,100,101,102,103) and RetSlabID<>94";
	}else{
	$query = "select RetSlabID id,SlabName name, SlabName ,month(From_dt)SlabMonth,year(From_dt)SlabYear,SlabMinVal,SlabMaxVal,CONVERT(varchar,To_Dt, 101)To_Date from Mas_Retail_Business_Slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and RetSlabID<>94  ";
	}
}
*/
$query="select RetSlabID id,SlabName name, SlabName ,month(From_dt)SlabMonth,year(From_dt)SlabYear,SlabMinVal,SlabMaxVal,CONVERT(varchar,To_Dt, 101)To_Date from Mas_Retail_Business_Slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and (CHARINDEX(',".$Hqcode.",',','+sf_hq+',')>0 or sf_hq is null)";

return performQuery($query);
}
function getmodetraveltype() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

    $query = "select  id,typename name,Km_Flag  from modeoftraveltype where ActiveFlag='0' and  Division_Code='" . $Owndiv . "' ";

    return performQuery($query);
}


function Giftproduct() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $query = "select Product_Code id,Product_Name name from Mas_Gift_Products where Active_Flag='0' and Division_Code='" . $Owndiv . "'  ";
    return performQuery($query);
}




function GiftEnrolment() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

  $sfCode = $_GET['sfCode'];
 $query="select Sf_HQ,Hq_Code from mas_salesforce   where Sf_Code='" . $sfCode . "' ";
 $Sf_Hq=performQuery($query);
$Hqcode=$Sf_Hq[0]['Hq_Code'];
/*
 if((strpos($Sf_Hq[0]['Sf_HQ'],"DELHI")>=0&& strpos($Sf_Hq[0]['Sf_HQ'],"DELHI")!="")){
    $query = "select  GiftSlabID id,month(From_Date)GiftMonth, GiftMinVal,year(From_Date)GiftYear, GiftName name,isnull(Claim_deadline,convert(date,getdate())) Claim_deadline , DATEDIFF(day, getdate(), isnull(Claim_deadline,convert(date,getdate())))daydif, GiftDesc GiftName,CONVERT(varchar,To_Date, 101)To_Date  from mas_gift_slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and GiftSlabID not in (91,92,88,89,90,98,99,100,101,102) and GiftSlabID<>93
    union
     select  GiftSlabID id,month(From_Date)GiftMonth, GiftMinVal,year(From_Date)GiftYear, GiftName name,isnull(Claim_deadline,convert(date,getdate())) Claim_deadline , DATEDIFF(day, getdate(), isnull(Claim_deadline,convert(date,getdate())))daydif, GiftDesc GiftName,CONVERT(varchar,To_Date, 101)To_Date  from mas_gift_slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and GiftSlabID not in (91,92,88,89,90) and GiftSlabID=93 and 'MGr1459'='" . $sfCode . "'";

 }else{

	 if((strpos($Sf_Hq[0]['Sf_HQ'],"DELHI")>=0 && strpos($Sf_Hq[0]['Sf_HQ'],"DELHI")!="") || (strpos($Sf_Hq[0]['Sf_HQ'],"Jabalpur")>=0 && strpos($Sf_Hq[0]['Sf_HQ'],"Jabalpur")!="") || (strpos($Sf_Hq[0]['Sf_HQ'],"Gwalior")>=0 && strpos($Sf_Hq[0]['Sf_HQ'],"Gwalior")!="") || (strpos($Sf_Hq[0]['Sf_HQ'],"Indore - 1")>=0 && strpos($Sf_Hq[0]['Sf_HQ'],"Indore - 1")!="") || (strpos($Sf_Hq[0]['Sf_HQ'],"Indore - 2")>=0 &&strpos($Sf_Hq[0]['Sf_HQ'],"Indore - 2")!="")){
	 $query = "select  GiftSlabID id,month(From_Date)GiftMonth, GiftMinVal,year(From_Date)GiftYear, GiftName name,isnull(Claim_deadline,convert(date,getdate())) Claim_deadline , DATEDIFF(day, getdate(), isnull(Claim_deadline,convert(date,getdate())))daydif, GiftDesc GiftName,CONVERT(varchar,To_Date, 101)To_Date  from mas_gift_slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and GiftSlabID not in (91,92,88,89,90,98,99,100,101,102) and GiftSlabID<>93";
	 }else{

	 $query = "select  GiftSlabID id,month(From_Date)GiftMonth, GiftMinVal,year(From_Date)GiftYear, GiftName name,isnull(Claim_deadline,convert(date,getdate())) Claim_deadline , DATEDIFF(day, getdate(), isnull(Claim_deadline,convert(date,getdate())))daydif, GiftDesc GiftName,CONVERT(varchar,To_Date, 101)To_Date  from mas_gift_slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and  GiftSlabID<>93 ";
	 }
 }*/
	$query = "select  GiftSlabID id,month(From_Date)GiftMonth, GiftMinVal,year(From_Date)GiftYear, GiftName name,isnull(Claim_deadline,convert(date,getdate())) Claim_deadline , DATEDIFF(day, getdate(), isnull(Claim_deadline,convert(date,getdate())))daydif, GiftDesc GiftName,CONVERT(varchar,To_Date, 101)To_Date  from mas_gift_slab where ActiveFlag='0' and Division_Code='" . $Owndiv . "' and isnull(Claim_deadline,convert(date,getdate()))>=convert(date,getdate()) and (CHARINDEX(',".$Hqcode.",',','+sf_hq+',')>0 or sf_hq is null)";
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
function dateDiffInDays($date1, $date2)
{
	// Calculating the difference in timestamps
	$diff = strtotime($date2) - strtotime($date1);

	// 1 day = 24 hours
	// 24 * 60 * 60 = 86400 seconds
	return abs(round($diff / 86400));
}
function getSubordinate() {
    $sfCode = $_GET['rSF'];
    $param = array($sfCode);
    $query = "exec getBaseLvlSFs_APP '" . $sfCode . "'";
    return performQuery($query);
}
function getSubordinateblockwise() {
    $sfCode = $_GET['rSF'];
    $param = array($sfCode);
    $query = "exec getBaseLvlSFs_Block '" . $sfCode . "'";
    return performQuery($query);
}
function getJointWork() {
    $sfCode = $_GET['sfCode'];
    $rqSF = $_GET['rSF'];
    $query = "exec getJointWork_App '" . $sfCode . "','" . $rqSF . "'";
    return performQuery($query);
}



function GetDailyInv() {
    $sfCode = $_GET['sfCode'];
	$Op_Flag = $_GET['Op_Flag'];
	$div = $_GET['divisionCode'];
	$divisionCode = explode(",", $div);
    $Md = $_GET['Mod'];
    $dt = $_GET['date'];

	if($Md==5){
		$query = "exec van_stock_ledger_native_new '". $sfCode ."','". $divisionCode[0]. "','". $dt . "','". $dt . "'";
	}else if($Md==1 || $Md == 3){
        $query = "exec getDailyBeginInvNative '" . $sfCode . "','".$Md."'";
    } else if($Md == 2 || $Md == 4){
        $query = "exec getDailyBeginInvApprlNative '" . $sfCode . "','".$Md."'";
	} else
        $query = "exec getDailyEndInv '" . $sfCode . "','" . $Op_Flag . "'";
    return  performQuery($query);
}


function GetApprvDone() {
    $sfCode = $_GET['rptSF'];
	$div = $_GET['divisionCode'];
	$divisionCode = explode(",", $div);
    $dt = $_GET['date'];
	$query = "exec van_stock_check_invoice_unloading '". $sfCode ."','". $divisionCode[0]. "','". $dt. "'";

    return  performQuery($query);
}


function GetBeginInvDone() {
    $sfCode = $_GET['rptSF'];
	$div = $_GET['divisionCode'];
	$divisionCode = explode(",", $div);
    $dt = $_GET['date'];
	//$query = "exec van_stock_check_loading '". $sfCode ."','". $divisionCode[0]. "','". $dt. "'";
	
      if(date('Y-m-d')!=$dt){
		$qry="select * from Trans_Prod_DailyInventoryNative where Div_Id='". $divisionCode[0]. "' and SFCode='". $sfCode ."' and cast(EntryDt as date) ='". $dt. "' and ApproveFlag =0";
		$pt=performQuery($qry);
		if(count($pt)>0){
		
		$qrydt= "delete from Trans_Prod_DailyInventoryNative where Div_Id='". $divisionCode[0]. "' and SFCode='". $sfCode ."' and cast(EntryDt as date) ='". $dt. "' and ApproveFlag =0";
		$pts=performQuery($qrydt);
		}
		
	   $query = "exec van_stock_check_loading '". $sfCode ."','". $divisionCode[0]. "','". $dt. "'";
		
			
	}else{
		$query = "exec van_stock_check_loading '". $sfCode ."','". $divisionCode[0]. "','". $dt. "'";
	}

    return  performQuery($query);
}

function GetRejectedList() {
    $sfCode = $_GET['rptSF'];
	$div = $_GET['divisionCode'];
	$divisionCode = explode(",", $div);
    $dt = $_GET['date'];
	//$query = "select SLNo 'id', Prod_Code, CaseQty, PiceQty, Totalqty 'CurrentStock',OPTotalQty 'OpStock', StkId from Trans_Prod_DailyInventoryNative where Div_Id='". $divisionCode[0] . "' and SFCode='". $sfCode . "' and cast(EntryDt as date) = '". $dt . "' and ApproveFlag = '2'";
	$query = "select SLNo 'id', Prod_Code, CaseQty, PiceQty, Totalqty 'CurrentStock',OPTotalQty 'OpStock', StkId, tvr.Reject_Reason  
from Trans_Prod_DailyInventoryNative left outer join Trans_VanReject tvr on tvr.Sf_Code = '". $sfCode . "' 
and cast(Reject_Date as date) = '". $dt . "' where Div_Id='". $divisionCode[0] . "' and SFCode='". $sfCode . "' and 
cast(EntryDt as date) = '". $dt . "' and ApproveFlag IN('2','3') ";

    return  performQuery($query);
}



function GetApprovalDetails() {
    $sfCode = $_GET['rptSF'];
	$div = $_GET['divisionCode'];
	$divisionCode = explode(",", $div);
    $dt = $_GET['date'];
    $query = "select SLNo 'id', Prod_Code, CaseQty, PiceQty, Totalqty 'CurrentStock' from Trans_Prod_DailyInventoryNative where SFCode='". $sfCode ."' and ApproveFlag ='0' and cast(convert(varchar,EntryDt,101) as datetime)='". $dt ."' and Div_Id = '" .$divisionCode[0]. "'";

    return  performQuery($query);
}
function GetApprovalDetProducts(){
	$sfCode = $_GET['rptSF'];
	$div = $_GET['divisionCode'];
	$divisionCode = explode(",", $div);
    $dt = $_GET['date'];
	$query="exec getApprovalDetPrdts '". $sfCode ."','". $dt ."','" .$divisionCode[0]. "'";
	 return  performQuery($query);
}
function GetApprovalDetProductBrnds(){
	$sfCode = $_GET['rptSF'];
	$div = $_GET['divisionCode'];
	$divisionCode = explode(",", $div);
    $dt = $_GET['date'];
	$query="exec getApprovalDetPrdtBrnds '". $sfCode ."','". $dt ."','" .$divisionCode[0]. "'";
	 return  performQuery($query);
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
	$t1 = strtotime(str_replace("Z", "", str_replace("T", " ", $data['mnthYr1'])));
    $TpDt1 = date('Y-m-d 00:00:00', $t1);
	  $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

	if(( $Owndiv=="156," or  $Owndiv=="156")){
		$Qry = "SELECT convert(varchar,Tour_Date,103) [date],Worktype_Name_B wtype,replace(isnull(Territory_Code1,''),'0','') towns,replace(isnull(Tour_Schedule1,''),'0','') townsId,replace(isnull(Tour_Schedule1,''),'0','') PlnNo,replace(isnull(Worked_With_SF_Name,''),'0','') distributor,Confirmed,SF_Code sf_code from vwTrans_TP_View T where sf_code='$sfCode' and Tour_Date>='$TpDt1'  and Tour_Date<='$TpDt' order by Tour_Date";
	}
	else{
		$Qry = "SELECT convert(varchar,Tour_Date,103) [date],Worktype_Name_B wtype,replace(isnull(Territory_Code1,''),'0','') towns,replace(isnull(Tour_Schedule1,''),'0','') townsId,replace(isnull(Tour_Schedule1,''),'0','') PlnNo,replace(isnull(Worked_With_SF_Name,''),'0','') distributor,Confirmed,SF_Code sf_code,JointWork_Name,JointWork_Name1,HQ_Code from vwTrans_TP_View T where sf_code='$sfCode' and Tour_Month=month('$TpDt')  and Tour_year=year('$TpDt') order by Tour_Date";
	}
	//echo $Qry;
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
function getRetailerprecall(){
    $sfCode = $_GET['rptSF'];
    $dyDt = $_GET['rptDt'];
    $query = "exec GetPrecall_CustomerRpt '" . $sfCode . "','" . $dyDt . "'";
    return performQuery($query);
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
  $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

	  if(( $Owndiv=="156," or  $Owndiv=="156")){

		   $sfCodes = explode(",", $sfCode . ",");
    $sfCode1 = (string) $sfCodes[0];
	if($sfCode1=="All"){
	  $sfCode = (string) $sfCodes[1];
	    $query = "exec getDayReportApp_t '" . $sfCode . "','" . $dyDt . "'";
    $dayrep= performQuery($query);
    $query = "exec getDayBrandWiseReportApp '" . $sfCode . "','" . $dyDt . "','" . $Owndiv . "'";
    $brndwise= performQuery($query);

    $query = "exec getDayDCR_TLSD_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_TLSD= performQuery($query);
    $query = "exec getDayDCR_LPC_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_LPC= performQuery($query);

    $query = "exec getDaySummaryReportApp '" . $sfCode . "','" . $dyDt . "'";
    $summary= performQuery($query);
    /* $query = "Select *  from Trans_Inshop_Activity where sfCode='" . $sfCode . "' and InsertDate='" . $dyDt . "' ";
    $InshopCount= performQuery($query);
    $query = "Select * from Trans_Product_Display where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $ProductCount= performQuery($query);

    $query = "Select * from Trans_Door_To_Door where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $Door_To_Door= performQuery($query); */
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
	return $result;die;
	}
	else
		  $sfCode=$sfCode1;
	  if($sfCode1=="route"){
		   $route = (string) $sfCodes[1];
		   $result=array();
   /* $query = "exec getDayReportApp_route '" . $route . "','" . $dyDt . "'";
  $query = "exec [getDayReportApp_town] '1594','2022-07-15'";
    $dayrep= performQuery($query);
 $query = "exec getDaySummaryReportApp 'TSRMR0076','2022-07-15'";
    $summary= performQuery($query);

 $result['dayrep']=$dayrep;
    $result['brndwise']=[];
    $result['DCR_TLSD']=[];
    $result['DCR_LPC']=[];
    $result['summary']=[];
    $result['Inshop']=$summary;
    $result['ProductDiisplay']=[];
    $result['DoorToDoor']=[];
     $result['SS']=[];*/
	 $sf = (string) $sfCodes[2];

	   $query = "exec [getDayReportApp_town] '" . $route . "','" . $dyDt . "','" . $sf . "'";
	    // $query = "exec [getDayReportApp_town] 543,'2022-07-25','TSRMGR0005'";
    $dayrep= performQuery($query);
    $brndwise= array();

    $DCR_TLSD= array();
    $DCR_LPC= array();
    $summary= array();

    $temp = array();

	 $result=array();

if($dayrep[0]['FTm']==null)
	 $result['dayrep']=[];
	else
    $result['dayrep']=$dayrep;
    $result['brndwise']=$brndwise;
	if(count($DCR_TLSD)>0)
    $result['DCR_TLSD']=$DCR_TLSD;
else{
	$result['DCR_TLSD'][0]['total_lines']=0;
	$result['DCR_TLSD'][0]['sf_code']=$sfCode;
}
    $result['DCR_LPC']=$DCR_LPC;
    $result['summary']=$summary;
    $result['Inshop']=$InshopCount;
    $result['ProductDiisplay']=$ProductCount;
    $result['DoorToDoor']=$Door_To_Door;
     $result['SS']=0;
	  }
	  else{
		 $query = "exec getDayReportApp_single '" . $sfCode . "','" . $dyDt . "'";
    $dayrep= performQuery($query);
    $query = "exec getDayBrandWiseReportApp_single '" . $sfCode . "','" . $dyDt . "','" . $Owndiv . "'";
    $brndwise= performQuery($query);

    $query = "exec getDayDCR_TLSD_ReportApp_single '" . $sfCode . "','" . $dyDt . "'";
    $DCR_TLSD= performQuery($query);
    $query = "exec getDayDCR_LPC_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_LPC= performQuery($query);

    $query = "exec getDaySummaryReportApp '" . $sfCode . "','" . $dyDt . "'";
    $summary= performQuery($query);
    /* $query = "Select *  from Trans_Inshop_Activity where sfCode='" . $sfCode . "' and InsertDate='" . $dyDt . "' ";
    $InshopCount= performQuery($query);
    $query = "Select * from Trans_Product_Display where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $ProductCount= performQuery($query);

    $query = "Select * from Trans_Door_To_Door where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $Door_To_Door= performQuery($query); */
    $query = "select Count(Trans_Detail_Info_Code) SuperStokit from vwActivity_SuperCSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=8";
    $temp = performQuery($query);

	 $result=array();

if($dayrep[0]['FTm']==null)
	 $result['dayrep']=[];
	else
    $result['dayrep']=$dayrep;
    $result['brndwise']=$brndwise;
	if(count($DCR_TLSD)>0)
    $result['DCR_TLSD']=$DCR_TLSD;
else{
	$result['DCR_TLSD'][0]['total_lines']=0;
	$result['DCR_TLSD'][0]['sf_code']=$sfCode;
}
    $result['DCR_LPC']=$DCR_LPC;
    $result['summary']=$summary;
    $result['Inshop']=$InshopCount;
    $result['ProductDiisplay']=$ProductCount;
    $result['DoorToDoor']=$Door_To_Door;
     $result['SS']=$temp[0]['SuperStokit'];
	/*}
	else{
		 $result=array();


    $result['dayrep']=[];
    $result['brndwise']=[];
    $result['DCR_TLSD']=[];
    $result['DCR_LPC']=[];
    $result['summary']=[];
    $result['Inshop']=[];
    $result['ProductDiisplay']=[];
    $result['DoorToDoor']=[];
     $result['SS']=[];
	}*/
	  }
}
	  else{
    $query = "exec getDayReportApp_t '" . $sfCode . "','" . $dyDt . "'";
    $dayrep= performQuery($query);
    $query = "exec getDayBrandWiseReportApp '" . $sfCode . "','" . $dyDt . "','" . $Owndiv . "'";
    $brndwise= performQuery($query);

    $query = "exec getDayDCR_TLSD_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_TLSD= performQuery($query);
    $query = "exec getDayDCR_LPC_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_LPC= performQuery($query);

    $query = "exec getDaySummaryReportApp '" . $sfCode . "','" . $dyDt . "'";
    $summary= performQuery($query);
    /* $query = "Select *  from Trans_Inshop_Activity where sfCode='" . $sfCode . "' and InsertDate='" . $dyDt . "' ";
    $InshopCount= performQuery($query);
    $query = "Select * from Trans_Product_Display where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $ProductCount= performQuery($query);

    $query = "Select * from Trans_Door_To_Door where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $Door_To_Door= performQuery($query); */
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
	  }

    return $result;
}

function iOSDayReport() {
    $sfCode = $_GET['sfCode'];
    $dyDt1 = $_GET['rptDt'];
	$today = date('Y-m-d 00:00:00');

	$dyDt = date('Y-m-d 00:00:00',strtotime($dyDt1));
	$div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
	$itm=[];
	$query = "select isnull(convert(varchar,Start_Time,108),'') StTm,isNull(convert(varchar,End_Time,108),'') EndTm,Start_Lat,Start_Long,End_Lat,End_Long,SSlfyPath,ESlfyPath from TP_Attendance_App where login_date='".$dyDt."' and SF_code='" . $sfCode . "'";
	$dayrep= performQuery($query);
	if(count($dayrep)>0) {
		$itm["StartTime"]=$dayrep[0]["StTm"];
		$itm["EndTime"]=$dayrep[0]["EndTm"];
		$itm["Start_Loc"]=$dayrep[0]["Start_Lat"].",".$dayrep[0]["Start_Long"];
		$itm["End_Loc"]=$dayrep[0]["End_Lat"].",".$dayrep[0]["End_Long"];
		$itm["SSlfy"]=$dayrep[0]["SSlfyPath"];
		$itm["ESlfy"]=$dayrep[0]["ESlfyPath"];
	}else{
		$itm["StartTime"]="-";
		$itm["EndTime"]="-";
		$itm["Start_Loc"]="";
		$itm["End_Loc"]="";
		$itm["SSlfy"]="";
		$itm["ESlfy"]="";

	}
    $query = "select top 1 convert(varchar,Pln_Date,108) attpln,Worktype_Name_B wtype from TbMyDayPlan  t inner join Mas_WorkType_BaseLevel l on wtype=WorkType_Code_B where cast(Pln_Date as date)='".$dyDt."' and SF_code='" . $sfCode . "'  order by Pln_Date";
	$dayrep= performQuery($query);
	if(count($dayrep)>0) {
		$itm["attTM"]=$dayrep[0]["attpln"];
		$itm["wtype"]=$dayrep[0]["wtype"];

	}else{
		$itm["attTM"]="-";$itm["wtype"]="";
	}
    $query = "select isnull(min(Time),'-') FC,isnull(sum(iif(Vst=1,1,0)),0) vc,isnull(sum(iif(Ord=1 and isnull(Cust_Code,0)>0,1,0)),0) pc,isnull(sum(Order_Value),0) oVal from (
	select h.SF_code,cast(Activity_Date as date) Activity_Date,Time,Trans_Detail_Info_Code,oh.Cust_Code,oh.Order_Value
	,ROW_NUMBER() over(partition by Trans_Detail_Info_Code order by Trans_Detail_Info_Code) Vst
	,ROW_NUMBER() over(partition by Cust_Code order by Cust_Code) Ord
	from DCRMain_Trans h inner join DCRDetail_Lst_Trans d on H.Trans_SlNo=d.Trans_SlNo
	left outer join Trans_Order_Head oh on h.Sf_Code=oh.Sf_Code and cast(Activity_Date as date)=cast(Order_Date as date) and Trans_Detail_Info_Code=oh.Cust_Code
	where cast(Activity_Date as date)='".$dyDt."' and h.SF_code='" . $sfCode . "'
	) as Sec";
	$dayrep= performQuery($query);

		$itm["secQry"]=$query;
	if(count($dayrep)>0) {
		$itm["secFC"]=$dayrep[0]["FC"];
		$itm["secVC"]=$dayrep[0]["vc"];
		$itm["secPC"]=$dayrep[0]["pc"];
		$itm["secoVal"]=$dayrep[0]["oVal"];
	}else{
		$itm["secFC"]="-";
		$itm["secVC"]=0;
		$itm["secPC"]=0;
		$itm["secoVal"]=0;
	}
	$query = "select isnull(min(vstTime),'-') FC,isnull(sum(iif(Vst=1,1,0)),0) vc,isnull(sum(iif(Ord=1 and isnull(Stockist_Code,0)>0,1,0)),0) pc,isnull(sum(Order_Value),0) oVal from (
	select h.SF_code,cast(Activity_Date as date) Activity_Date,vstTime,Trans_Detail_Info_Code,oh.Stockist_Code,oh.Order_Value
	,ROW_NUMBER() over(partition by Trans_Detail_Info_Code order by Trans_Detail_Info_Code) Vst
	,ROW_NUMBER() over(partition by Stockist_Code order by Stockist_Code) Ord
	from DCRMain_Trans h inner join DCRDetail_CSH_Trans d on H.Trans_SlNo=d.Trans_SlNo
	left outer join Trans_PriOrder_Head oh on h.Sf_Code=oh.Sf_Code and cast(Activity_Date as date)=cast(Order_Date as date) and cast(Trans_Detail_Info_Code as varchar)=oh.Stockist_Code and oh.Order_value>0
	where cast(Activity_Date as date)='".$dyDt."' and h.SF_code='" . $sfCode . "'
	) as Pri";
	$dayrep= performQuery($query);
	if(count($dayrep)>0) {
		$itm["priFC"]=$dayrep[0]["FC"];
		$itm["priVC"]=$dayrep[0]["vc"];
		$itm["priPC"]=$dayrep[0]["pc"];
		$itm["prioVal"]=$dayrep[0]["oVal"];
	}else{
		$itm["priFC"]="-";
		$itm["priVC"]=0;
		$itm["priPC"]=0;
		$itm["prioVal"]=0;
	}


    $query = "exec getDayBrandWiseReportApp '" . $sfCode . "','" . $dyDt . "','" . $Owndiv . "'";
    $brndwise= performQuery($query);
	/*
    $query = "exec getDayReportApp '" . $sfCode . "','" . $dyDt . "'";
    $dayrep= performQuery($query);



    $query = "exec getDayDCR_TLSD_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_TLSD= performQuery($query);
    $query = "exec getDayDCR_LPC_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_LPC= performQuery($query);

    $query = "exec getDaySummaryReportApp '" . $sfCode . "','" . $dyDt . "'";
    $summary= performQuery($query);
    /* $query = "Select *  from Trans_Inshop_Activity where sfCode='" . $sfCode . "' and InsertDate='" . $dyDt . "' ";
    $InshopCount= performQuery($query);
    $query = "Select * from Trans_Product_Display where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $ProductCount= performQuery($query);

    $query = "Select * from Trans_Door_To_Door where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $Door_To_Door= performQuery($query); *
    $query = "select Count(Trans_Detail_Info_Code) SuperStokit from vwActivity_SuperCSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=8";
    $temp = performQuery($query);*/
    $result=array();


    $result['daydets']=$itm;
	$result['rptdt']=$dyDt1;
    $result['brndwise']=$brndwise;
    /*$result['dayrep']=$dayrep;
    $result['DCR_TLSD']=$DCR_TLSD;
    $result['DCR_LPC']=$DCR_LPC;
    $result['summary']=$summary;
    $result['Inshop']=$InshopCount;
    $result['ProductDiisplay']=$ProductCount;
    $result['DoorToDoor']=$Door_To_Door;
     $result['SS']=$temp[0]['SuperStokit'];*/
    return $result;
}
function DayOrderReport() {
    $sfCode = $_GET['RsfCode'];
    $dyDt = $_GET['rptDt'];
 $today = date('Y-m-d 00:00:00');
  $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

    $query = "exec getDayReportApp '" . $sfCode . "','" . $dyDt . "'";
    $dayrep= performQuery($query);
    $query = "exec getDayBrandWiseReportApp '" . $sfCode . "','" . $dyDt . "','" . $Owndiv . "'";
    $brndwise= performQuery($query);

    $query = "exec getDayDCR_TLSD_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_TLSD= performQuery($query);
    $query = "exec getDayDCR_LPC_ReportApp '" . $sfCode . "','" . $dyDt . "'";
    $DCR_LPC= performQuery($query);

    $query = "exec getDaySummaryReportApp '" . $sfCode . "','" . $dyDt . "'";
    $summary= performQuery($query);
    /* $query = "Select *  from Trans_Inshop_Activity where sfCode='" . $sfCode . "' and InsertDate='" . $dyDt . "' ";
    $InshopCount= performQuery($query);
    $query = "Select * from Trans_Product_Display where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $ProductCount= performQuery($query);

    $query = "Select * from Trans_Door_To_Door where sfCode='" . $sfCode . "' and Current_Date_And_Time='" . $dyDt . "' ";
    $Door_To_Door= performQuery($query); */
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
$fromdate=date('m', strtotime($_GET['fromdate'] ));
$todate=date('Y', strtotime($_GET['todate'] ));
}
if($Type==3 ){
$fromdate = $_GET['fromdate'];
}

    $query = "exec getPrimarySalesFPWise '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";
//

$result['primaryfpwise']=performQuery($query);
return $result;


}
function  GetSecondaryTarget(){

$sfCode = $_GET['sfCode'];
      $today = date('Y-m-d');
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

    $Month = $_GET['Month'];
    $Year = $_GET['Year'];


    $query = "exec Get_FieldForce_Target_vs_Sal '" . $sfCode . "', '" . $Year . "','" . $Month . "', '" . $Year . "','" . $Month . "' ,  '" . $Owndiv . "' ";
//
//

$result['SecondaryTarget']=performQuery($query);
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
$fromdate=date('m', strtotime($_GET['fromdate'] ));
$todate=date('Y', strtotime($_GET['todate'] ));
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
$fromdate=date('m', strtotime($_GET['fromdate'] ));
$todate=date('Y', strtotime($_GET['todate'] ));
}
  if($Type==3 ){
$fromdate=date(('Y-n-d'),  strtotime($fromdate));
$todate=date(('Y-n-d'),  strtotime($todate));
}

    $query = "exec getPrimaryDistiWise '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";

   $GETtarger=performQuery($query);
    $countres= count($GETtarger);
	if($countres>0){
		$result['distributortarget']=$GETtarger;
	}else{
		$result['distributortarget']=[];
	}

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


function  getVisitAttendanceView(){

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


    $query = "exec getAttendanceView '" . $sfCode . "','" . $Owndiv . "', '" .$Type. "','".$fromdate."','".$todate."'  ";



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
$fromdate=date('m', strtotime($_GET['fromdate'] ));
$todate=date('Y', strtotime($_GET['todate'] ));
}
if($Type==3 ){
$fromdate = $_GET['fromdate'];
}


    $query = "exec getPrimaryproductwise '" . $sfCode . "','" . $Owndiv . "' , '" .$Type. "','".$fromdate."','".$todate."' ";

  $result['productwisesales']=performQuery($query);
    return $result;
}

function SecondaryProductWise(){

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
$fromdate=date('m', strtotime($_GET['fromdate'] ));
$todate=date('Y', strtotime($_GET['todate'] ));
}
if($Type==3 ){
$fromdate = $_GET['fromdate'];
}
  $query = "exec getSecondaryproductwise '" . $sfCode . "','" . $Owndiv . "' , '" .$Type. "','".$fromdate."','".$todate."' ";

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
  $NtfyMsg=  $sfType[0]['Sf_Name'].'has changed Route ( TP Deviation )';



   $sql = "insert into Mas_AppNotifyList(SF_Code,NotifyID,Subject,NtfyMsg,CrDt,DivCode,typ) values( '".$sfType[0]['Reporting_To_SF'] . "','" . $Maxid[0]['id'] . "','Wrong! Route','$NtfyMsg','".$today."'," . $divisionCode[0] . "," . $sfType[0]['sf_type'] . ")";
     performQuery($sql);

$sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
            $device = performQuery($sql);

            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {


                send_gcm_notify($reg_id, $NtfyMsg,'Wrong! Route');
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
function getQuizcs() {
	$div = $_GET['divisionCode'];
$sfCode = $_GET['sfCode'];
 $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
//$query = "select survey_id,quiz_title,substring(filepath,charindex(')',filepath)+1,len(filepath)) FileName from QuizTitleCreation where division_code='" . $Owndiv . "' and active=0  and convert(date,effective_date)<convert(date,GETDATE())  order by survey_id desc";
	$query="select QT.survey_id,quiz_title,substring(filepath,charindex(')',filepath)+1,len(filepath)) FileName from QuizTitleCreation QT inner join Processing_UserList UT on UT.SurveyId=QT.Survey_Id where division_code='" . $Owndiv . "' and active=0  and CONVERT(date,GETDATE()) between CONVERT(date,From_Date) and convert(date,To_Date) group by QT.survey_id,quiz_title,substring(filepath,charindex(')',filepath)+1,len(filepath)) order by QT.survey_id desc";
               $quiztitle = performQuery($query);
$quiztitle1=array();
$processUser1=array();
for($i=0;$i<count($quiztitle);$i++){
$surveyid=$quiztitle[$i]['survey_id'];
 $query = "select NoOfAttempts type,Type NoOfAttempts,timelimit from Processing_UserList where surveyid='" . $surveyid . "' and sf_code='" . $sfCode . "' and process_status='P' and cast(from_date as date)>=cast(GETDATE() as date) ";
                $processUser = performQuery($query);
if(count($processUser)>0){
$processUser1=$processUser;
$quiztitle1=$quiztitle[$i];
$quiztitle=array();
}
}

$processUser=array();
$processUser=$processUser1;
$quiztitle=array();
$quiztitle[0]=$quiztitle1;

	$surveyid=$quiztitle[0]['survey_id'];
if($quiztitle[0]['FileName']!=""){
$extn=end(explode('.', $quiztitle[0]['FileName']));

 if ($extn == "png" || $extn == "jpg")
             $quiztitle[0]['mimetype']= "image/png";
 else if($extn=="doc"||$extn=="dot")
			  $quiztitle[0]['mimetype']= "application/msword";
 else if($extn=="docx"|| $extn == "DOCX")
			  $quiztitle[0]['mimetype']="application/msword";
 else if ($extn == "xls"||$extn == "xlt"||$extn == "xla")
             $quiztitle[0]['mimetype']= "application/vnd.ms-excel";
		  else if ($extn == "xlsx")
             $quiztitle[0]['mimetype']= "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
         else if ($extn == "mp4")
             $quiztitle[0]['mimetype']= "video/mp4";
else if($extn == "pptx")
  $quiztitle[0]['mimetype']= "application/vnd.openxmlformats-officedocument.presentationml.presentation";
         else
             $quiztitle[0]['mimetype']= "application/" + $extn;

}


                $query = "select Question_Type_id,Question_Id,Question_Text,surveyid from AddQuestions where surveyid='" . $surveyid . "' order by question_id asc";
                $questions = performQuery($query);
                $query = "select input_id,Question_Id,Input_Text,Correct_Ans from AddInputOptions where question_id in (select question_id from AddQuestions where surveyid='" . $surveyid . "') order by question_id asc";
                $answers = performQuery($query);
				$results=array();
				$results['quiztitle'][0]=$quiztitle[0];
				$results['processUser']=$processUser;
				$results['questions']=$questions;
				$results['answers']=$answers;
if(count($processUser)==0)
$results=array();

return $results;

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

function getDailyAlloww() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $query = "select ID,Allowance_Name Name ,Photo_Nd from Mas_Allowance_Type where Type=1 and Division_Code=". $Owndiv ." and user_enter=1 and active_flag=0";
    return performQuery($query);
}
function getDailyAllow() {
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
	 $sfCode = $_GET['sfCode'];
	$date=date('Y-m-d');
    $query = "select ID,Allowance_Name Name,Photo_Nd from Mas_Allowance_Type where Type=1 and Division_Code=". $Owndiv ." and user_enter=1 and active_flag=0";
	$ExpenseWeb=performQuery($query);
	$query = "select * from Expense_Start_Activity where Sf_code='" . $sfCode . "'  and convert(date,Date_Time)='".$date."'";
	$TodayExpense=performQuery($query);

	 $result['ExpenseWeb']=$ExpenseWeb;
        $result['TodayExpense']=$TodayExpense;
    return $result;
}


function getChannelList(){
	$query = "select ID,Allowance_Name Name,Photo_Nd from Mas_Allowance_Type where Type=1 and Division_Code=". $Owndiv ." and user_enter=1 and active_flag=0";
	$ExpenseWeb=performQuery($query);
	 $result['ChannelList']=$ExpenseWeb;
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

function DateofExpense(){
    $sfCode = $_GET['rSF'];
    $eDt = $_GET['Dateofexp'];
	$Type = $_GET['Type'];
	$query ="update tb set tb.ClstrName=SDP_Name,tb.cluster=SDP from  TbMyDayPlan tb inner join   DCRDetail_Lst_Trans  dc on dc.sf_code=tb.sf_code where tb.sf_code='" . $sfCode . "' and ClstrName='Missed Entry'  and convert(date,tb.Pln_Date)=convert(date,dc.ModTime)";
	performQuery($query);
	/* if($Type=='1'){
		$query = "select top 1 isnull(ClstrName,'') ClstrName , (select  isnull(Allowance_Type,'') from Mas_Territory_Creation where   CONVERT(VARCHAR(50),Territory_Code)=isnull(TB.cluster,'')) Allowance_Type , (select Worktype_Name_B from   Mas_WorkType_BaseLevel where WorkType_Code_B=TB.wtype ) WorkType from TbMyDayPlan    TB  where TB.sf_code='" . $sfCode . "' and convert(date,TB.Pln_Date)='" . $eDt . "' order by TB.Pln_Date ASC ";
	}else{
		 $query = "select top 1 ClstrName,  isnull((select  Allowance_Type from Mas_Territory_Creation where CAST(Territory_Code AS varchar(50))=isnull(TB.cluster,'')),'') Allowance_Type,(select Worktype_Name_M from   Mas_WorkType_Mgr where WorkType_Code_M=TB.wtype ) WorkType from TbMyDayPlan  TB  where TB.sf_code='" . $sfCode . "' and convert(date,TB.Pln_Date)='" . $eDt . "' order by TB.Pln_Date ASC ";
	} */


	//$typeQuery = "select sf_type from Mas_Salesforce where sf_code = '". $sfCode . "'";
	//$typeQueryRes = performQuery($typeQuery);
    //$Type = $typeQueryRes[0]['sf_type'];

	if($Type=='1'){
  $query = "select case when   (WorkType='Meeting' or  WorkType='Admin Work' or WorkType='Transit' ) and ClstrName=''  and  Division_Code='109' then 'HQ' ELSE Allowance_Type end Allowance_Type,ClstrName,WorkType  from (select top 1 isnull(ClstrName,'') ClstrName ,Division_Code, (select  isnull(Allowance_Type,'HQ') from Mas_Territory_Creation where   CONVERT(VARCHAR(50),Territory_Code)=isnull(TB.cluster,'')) Allowance_Type , (select Worktype_Name_B from   Mas_WorkType_BaseLevel where WorkType_Code_B=TB.wtype ) WorkType from TbMyDayPlan    TB  where TB.sf_code='" . $sfCode . "' and convert(date,TB.Pln_Date)='" . $eDt . "'
  and (select Worktype_Name_B from Mas_WorkType_BaseLevel where WorkType_Code_B=TB.wtype)!= 'Weekly Off' order by TB.Pln_Date ASC ) A ";
 }else{
   $query = "select case when   (WorkType='Meeting' or  WorkType='Admin Work' or WorkType='Transit') and ClstrName='' and  Division_Code='109'  then 'HQ' ELSE Allowance_Type end Allowance_Type,ClstrName,WorkType  from (select top 1 ClstrName,Division_Code,  isnull((select  Allowance_Type from Mas_Territory_Creation where CAST(Territory_Code AS varchar(50))=isnull(TB.cluster,'')),'HQ') Allowance_Type,(select Worktype_Name_M from   Mas_WorkType_Mgr where WorkType_Code_M=TB.wtype ) WorkType from TbMyDayPlan  TB  where TB.sf_code='" . $sfCode . "' and convert(date,TB.Pln_Date)='" . $eDt . "' order by TB.Pln_Date desc) A ";
 }


    return performQuery($query);
}


function getVstDets() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
    $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";
    return performQuery($query);
}

function getiOSVstDets() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
	$sfCode = $_GET['sfCode'];
	$Dt = $_GET['rptDt'];
	$StrMode = $_GET['Mode'];
   // $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";
	if($StrMode == "VstRet" || $StrMode == "VstPRet"){
		$query = "exec getVisitDetByDt '" . $sfCode . "','" . $Dt . "'";
	}else{
		$query = "exec getPriVisitDetByDt '" . $sfCode . "','" . $Dt . "'";
	}
    return performQuery($query);
}

function getItemSummaryByDt() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
	$sfCode = $_GET['sfCode'];
	$Dt = $_GET['rptDt'];
	$StrMode = $_GET['Mode'];
   // $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";
	if($StrMode == "VstRet" || $StrMode == "VstPRet"){
		$query = "exec getItemSummaryByDt '" . $sfCode . "','" . $Dt . "'";
	}else{
		$query = "exec getPriItemSummaryByDt '" . $sfCode . "','" . $Dt . "'";
	}
    return performQuery($query);
}
function getOrderDetByDt(){
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
	$sfCode = $_GET['sfCode'];
	$Dt = $_GET['rptDt'];
	$CusCd = $_GET['CusCd'];
	$StrMode = $_GET['Mode'];
	if($StrMode == "VstRet" || $StrMode == "VstPRet"){
		$query = "select h.SF_Code,Cust_Code,l.ListedDr_Name CusName,l.ListedDr_Address1 CusAddr,l.ListedDr_Mobile CusMobile,
		s.Stockist_Name StkName,s.Stockist_Address StkAddr,l.ListedDr_Mobile StkMob,h.Trans_Sl_No,iif(OrderType=0,'Field Order','Phone Order') OrderTypeNm from Trans_Order_Head h
		inner join Mas_ListedDr l on Cust_Code=ListedDrCode
		inner join Mas_Stockist s on h.Stockist_Code=s.Stockist_Code
		where h.Sf_Code='$sfCode' and Cust_Code='$CusCd' and cast(Order_Date as date)='$Dt'";
		$Res = performQuery($query);
		for ($j = 0; $j < count($Res); $j++) {
			$query = "select Trans_Order_No,Product_Code PCode,Product_Name PName,Uom_Id,Unit_Name,isnull(qty,Quantity) Qty,
			Quantity PSCQty,Rate,isnull(Tax_value,0) Tax,discount_price Disc,value,Con_Qty,
			Offer_ProductCd OffPCode,iif(isnull(Offer_ProductNm,'')='',Product_Name,Offer_ProductNm) OffPName,off_pro_unit OffUntID,m.Product_Sale_Unit OffUntName,free offQty,Conf_Fac OffConv
			from Trans_Order_Details d
				inner join Mas_Product_Detail m on m.Product_Detail_Code=iif(isnull(Offer_ProductCd,'0')='0',Product_Code,Offer_ProductCd) where Trans_Sl_No='".$Res[$j]["Trans_Sl_No"]."'";
			$ResDet = performQuery($query);
			$Res[$j]["Items"]=$ResDet;
		}
	}else{
		$query = "select h.SF_Code,h.Stockist_code Cust_Code,l.Stockist_Name CusName,l.Stockist_Address CusAddr,l.Stockist_Mobile CusMobile,
		isnull(s.S_Name,'') StkName,isnull(s.Contact_Person,'') StkAddr,isnull(s.Mobile,'') StkMob,h.Trans_Sl_No,iif(OrderType=0,'Field Order','Phone Order') OrderTypeNm from Trans_PriOrder_Head h
		inner join Mas_Stockist l on l.Stockist_code=h.Stockist_code
		left join Supplier_Master s on h.Super_Stockist_Code=s.S_No
		where h.Sf_Code='$sfCode' and l.Stockist_code='$CusCd' and cast(Order_Date as date)='$Dt'";
		$Res = performQuery($query);
		for ($j = 0; $j < count($Res); $j++) {
			$query = "select Trans_POrd_No,Product_Code PCode,Product_Name PName,'' Uom_Id,Product_Unit_Name Unit_Name,isnull(qty,CQty) Qty,
			CQty PSCQty,Rate,isnull(Tax,0) Tax,discount_price Disc,value,Product_Unit_Value Con_Qty,
			Offer_ProductCd OffPCode,iif(isnull(Offer_ProductNm,'')='',Product_Name,Offer_ProductNm) OffPName,
			'' OffUntID,m.Product_Sale_Unit OffUntName,free offQty,1 OffConv
			from Trans_PriOrder_Details d
			inner join Mas_Product_Detail m on m.Product_Detail_Code=iif(isnull(Offer_ProductCd,'0')='0',Product_Code,Offer_ProductCd) where Trans_Sl_No='".$Res[$j]["Trans_Sl_No"]."'";
			$ResDet = performQuery($query);
			$Res[$j]["Items"]=$ResDet;
		}
	}
	return $Res;
}
function getVstDetsNative_dm() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
	$sfCode = $_GET['sfCode'];

    
		$activityDate = $_GET['activityDate'];
	
    $query = "exec spGetVstDetAppnative_dm '" . $activityDate . "','" . $sfCode . "'";

	$viewResponse = performQuery($query);

	for ($j = 0; $j < count($viewResponse); $j++) {
		$orderNo= $viewResponse[$j]["id"];
		$productListQuery = [];

			$productListQuery = "exec get_sec_order_dts_native_dm '" . $orderNo . "','" . $sfCode . "'";
		

			$viewResponse[$j]["productList"] = performQuery($productListQuery);
				$ekey= $viewResponse[$j]["ekey"];
			$imagesQuery="select  Activity_Report_Code+'_'+imgurl imgurl,title,remarks from Activity_Event_Captures where Identification='promotion' and DCRDetNo='".$ekey."'";
						$viewResponse[$j]["imageList"] = performQuery($imagesQuery);
			//$viewResponse[$j]["productList"]=array();
	}
    return $viewResponse;
}


function getVstDetsNative() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
	$sfCode = $_GET['sfCode'];
	if($ACd=="sec_order"){
		$activityDate = $_GET['activityDate'];
		 $query = "select trans_slno from dcrmain_trans where sf_code='" . $sfCode . "' and cast(activity_date as date)='" . $activityDate . "'";

	$ACDD = performQuery($query);
$ACd=$ACDD[0]['trans_slno'];
	}
		$div = $_GET['divisionCode'];
if($div=="90,")
    $query = "exec spGetVstDetApp_Hybrid '" . $ACd . "','" . $typ . "'";
else
    $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";

	$viewResponse = performQuery($query);

	for ($j = 0; $j < count($viewResponse); $j++) {
		$orderNo= $viewResponse[$j]["Order_No"];
		$productListQuery = [];

		if($typ == 8){
			$productListQuery = "exec get_ss_order_dts '" . $orderNo . "','" . $sfCode . "'";
		}else if($typ == 1){
			$productListQuery = "exec get_sec_order_dts_native '" . $orderNo . "','" . $sfCode . "'";
		}else{
			$productListQuery = "exec get_pri_order_dts '" . $orderNo . "','" . $sfCode . "'";
        }

			$viewResponse[$j]["productList"] = performQuery($productListQuery);
			$viewResponse[$j]["imageList"]=array();
	}
	//$viewResponse['sql']=$query;
	//$viewResponse['sqll']=$productListQuery;
    return $viewResponse;
}
function getVstDetsNative_scheme() {
  //  $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
	$sfCode = $_GET['sfCode'];
	//if($ACd=="sec_order"){
		$activityDate = $_GET['activityDate'];
		 $query = "select trans_slno from dcrmain_trans where sf_code='" . $sfCode . "' and cast(activity_date as date)='" . $activityDate . "'";

	$ACDD = performQuery($query);
$ACd=$ACDD[0]['trans_slno'];
	//}
    $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";

	$viewResponse = performQuery($query);
$viewResponse1=array();
$k=0;
	for ($j = 0; $j < count($viewResponse); $j++) {
		$orderNo= $viewResponse[$j]["Order_No"];
		$productListQuery = [];

			$productListQuery = "exec get_sec_order_dts_native_scheme '" . $orderNo . "','" . $sfCode . "'";
		$prods=performQuery($productListQuery);
		
			$viewResponse[$j]["productList"] = $prods;
			$viewResponse[$j]["imageList"]=array();
			if(count($prods)>0){
				$viewResponse1[$k]=$viewResponse[$j];
				$k=$k+1;
			}
	}
	//$viewResponse['sql']=$query;
	//$viewResponse['sqll']=$productListQuery;
    return $viewResponse1;
}


function getVstOrderDets() {
    $order_date = $_GET['orderDt'];
    $order_no = $_GET['orderNo'];
	 $stockist = $_GET['stockist_code'];
	  $sf = $_GET['sfCode'];
	   $typ = $_GET['typ'];
	   if($typ=="1")
		   $query = "exec spGetVstOrderDetSecApp '" . $order_date . "','" . $order_no . "','" . $stockist . "','" . $sf . "'";
	   else
    $query = "exec spGetVstOrderDetApp_dms '" . $order_date . "','" . $order_no . "','" . $stockist . "','" . $sf . "'";
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
 
 	 $divCode = $_GET['divisionCode'];
             $divisionCode = explode(",", $divCode);
			 $Owndiv = (string) $divisionCode[0];
			 $sql="select isnull(closing_monthwise,1) closing_monthwise from Access_Master where division_code=$Owndiv";
			 $monthws=performQuery($sql);
			 $clsng=$monthws[0]['closing_monthwise'];
    if($CLDT=='') $CLDT=date('Y-m-d 00:00:00');
	if($clsng=="1")
		 $query = "exec getClStkVw_native '$SF','$CLDT'";
	 else
    $query = "exec getClStkVw '$SF','$CLDT'";
    $transHead = performQuery($query);
    return outputJSON($transHead);
}


function getCurrentStockDetSS() {
    if ($_GET['scode'] == "0")
        $SF = $_GET['sfCode'];
    else
        $SF = $_GET['scode'];

    $CLDT = $_GET['cldt'];

    if($CLDT=='') $CLDT=date('Y-m-d 00:00:00');
    $query = "exec getClStkVwSS '$SF','$CLDT'";
    $transHead = performQuery($query);
    return outputJSON($transHead);
}

function  getVanLstLdDt(){
 $SF = $_GET['sfCode'];
 $divCode = $_GET['divisionCode'];
$divs = explode(",", $divCode . ",");
    $Owndiv = (string) $divs[0];
$query="select max(cast(EntryDt as date)) EntryDt fROM Trans_Prod_DailyInventoryNative where Div_Id='".$Owndiv."' and SFCode='".$SF."' 
							 and EntryDt is not null"; //and ApproveFlag !=2";
$transHead = performQuery($query);
return outputJSON($transHead);
}




function getStockBasedOrder() {

    $SF = $_GET['sfCode'];
	$divisionCode = $_GET['divisionCode'];

    $query = "exec getDistStockAndSal '".$SF."','".date('Y-m-d')."'";
    return outputJSON(performQuery($query));
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
function getPreCallDetTSR() {
    $SF = $_GET['sfCode'];
    $MSL = $_GET['Msl_No'];
	$Divc="";
    $result = array();
   $query = "select SLVNo SVL,status_updated status_doc,case when status_updated=1 then 'Updated' else 'Not' end status_name,Division_Code,Doc_Class_ShortName DrCat,PIN_Code pincode,isnull(GST,'') GST,ListedDrCode,Slan_Name,Giftenrol_Name,Doc_Spec_ShortName DrSpl,isnull(stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory S where CHARINDEX(cast(Doc_SubCatCode as varchar),D.Doc_SubCatCode)>0 for XML Path('')),1,2,''),'') DrCamp,isnull(stuff((select ', '+Product_Detail_Name from Map_LstDrs_Product M  inner join Mas_Product_Detail P on M.Product_Code=P.Product_Detail_Code and P.Division_Code=M.Division_Code where Listeddr_Code=D.ListedDrCode for XML Path('')),1,2,''),'') DrProd,Case WHEN ListedDr_Address1!=''  and ListedDr_Address1 IS NOT NULL THEN ListedDr_Address1 WHEN ListedDr_Address2!=''  and ListedDr_Address2 IS NOT NULL THEN ListedDr_Address2 ELSE ListedDr_Address3 END as Address  from mas_listeddr  D where ListedDrCode='" . $MSL . "'";
  $as = performQuery($query);
  $query = "select top 1 Slan_Name   from Gift_Mas_ListedDr where  ListedDrCode='" . $MSL . "' ";
  $gift = performQuery($query);
    if (count($as) > 0) {
        $result['SVL'] = (string) $as[0]['SVL'];
		$Divc = (string) $as[0]['Division_Code'];
		$result['ListedDrCode'] = (string) $as[0]['ListedDrCode'];
        $result['DrCat'] = (string) $as[0]['DrCat'];
        $result['DrSpl'] = (string) $as[0]['DrSpl'];
        $result['DrCamp'] = (string) $as[0]['DrCamp'];
        $result['DrProd'] = (string) $as[0]['DrProd'];
        $result['Address'] = (string) $as[0]['Address'];
		$result['SlabName'] = (string) $as[0]['Slan_Name'];
		$result['GST'] = (string) $as[0]['GST'];
		$result['pincode'] = (string) $as[0]['pincode'];
         $result['Giftenrol_Name'] = (string) $as[0]['Giftenrol_Name'];
		 $result['status'] = (string) $as[0]['status_doc'];
		 $result['status_name'] = (string) $as[0]['status_name'];
         $dateYeabbr=date('m');
		$now = new DateTime();
		$year = $now->format("Y");
		$Month=$now->format("M");
        $result['success'] = true;


		$query="select top 2 trans_sl_no,dcr_code,cast(order_value as int) order_value,CONVERT(VARCHAR(10), order_date, 105) visit_date,LTRIM(RIGHT(CONVERT(VARCHAR(20), order_date, 100), 7)) visit_time,order_date from trans_order_head  where sf_code='".$SF."' and Cust_Code='".$MSL."'    order by Order_Date desc";

		 $LAST2ORDER=performQuery($query);

			 $sql="select Product_Code,Product_Name,Quantity,cast(value as int) value from Trans_Order_Details det
inner join Mas_Product_Detail prod on prod.Product_Detail_Code=det.Product_Code
inner join Mas_Product_Brand brnd on brnd.Product_Brd_Code=prod.Product_Brd_Code where trans_sl_no='".$LAST2ORDER[0]['trans_sl_no']."'";

 $products=performQuery($sql);
$sql="select Product_Brd_Name name,cast(sum(value) as int) total,count(Product_Brd_Name) qty from (select product_code,product_name,quantity,value,Product_Brd_Name from Trans_Order_Details det
inner join Mas_Product_Detail prod on prod.Product_Detail_Code=det.Product_Code
inner join Mas_Product_Brand brnd on brnd.Product_Brd_Code=prod.Product_Brd_Code where trans_sl_no='".$LAST2ORDER[0]['trans_sl_no']."') test
group by Product_Brd_Name";
 $brands=performQuery($sql);
 $lastorders=array();
 $lastorders['last_products']=$products;
 $lastorders['last_brands']=$brands;

		 $sql="select Product_Code,Product_Name,Quantity,cast(value as int) value,Product_Brd_Name from Trans_Order_Details det
inner join Mas_Product_Detail prod on prod.Product_Detail_Code=det.Product_Code
inner join Mas_Product_Brand brnd on brnd.Product_Brd_Code=prod.Product_Brd_Code where trans_sl_no='".$LAST2ORDER[1]['trans_sl_no']."'";

 $products1=performQuery($sql);
$sql="select Product_Brd_Name name,cast(sum(value) as int) total,count(Product_Brd_Name) qty from (select product_code,product_name,quantity,value,Product_Brd_Name from Trans_Order_Details det
inner join Mas_Product_Detail prod on prod.Product_Detail_Code=det.Product_Code
inner join Mas_Product_Brand brnd on brnd.Product_Brd_Code=prod.Product_Brd_Code where trans_sl_no='".$LAST2ORDER[1]['trans_sl_no']."') test
group by Product_Brd_Name";
 $brands1=performQuery($sql);
 $lastprevorders=array();
 $lastprevorders['last_products']=$products1;
 $lastprevorders['last_brands']=$brands1;
		$query = "select *,case when (isnull(contactperson,'') +','+ isnull(contactperson2,'')+','+ isnull(contactperson3,'')=',,') then (' ') else isnull(contactperson,'') +','+ isnull(contactperson2,'')+','+ isnull(contactperson3,'') end  as ContactPersionn  from Mas_ListedDr where ListedDrCode='".$MSL."' ";
        $POT = performQuery($query);
 $stockist[0] = array(
                    "stockist_code" => "120",
                    "Sum" => "12000",
                    "Avg" => "50",
                    "LastOrderAmt" =>"100",
                    "outStandingAmt" => "340",
                    "OrderDetails" => $products,
					 "Brands" => $brands,
					 "order_value"=>$LAST2ORDER[0]['order_value'],
					 "visit_date"=>$LAST2ORDER[0]['visit_date'],
					 "visit_time"=>$LAST2ORDER[0]['visit_time']
                );
				 $stockist[1] = array(
                    "stockist_code" => "120",
                    "Sum" => "12000",
                    "Avg" => "50",
                    "LastOrderAmt" =>"100",
                    "outStandingAmt" => "340",
                    "OrderDetails" => $products1,
					 "Brands" => $brands1,
					  "order_value"=>$LAST2ORDER[1]['order_value'],
					 "visit_date"=>$LAST2ORDER[1]['visit_date'],
					 "visit_time"=>$LAST2ORDER[1]['visit_time']
                );

        $result['StockistDetails'] = $stockist;
		 $result['last_order'] = $lastorders;
	 $result['last_prev_order'] = $lastprevorders;
       // $result['StockistDetails'] = array();
        $result['OpeningStock'] = array();
        $result['MOQ'] = array();
        $result['POTENTIAL'] =$POT;
		$result['LastorderAmount'] = 100;
        $result['MOV'] = $MOV;
		$result['CallFd'] ="test";
            $result['Rmks'] = "test";
            $result['ProdSmp'] = array();
            $result['Prodgvn'] = array();
            $result['DrGft'] = "test";

    } else {
        $result['success'] = false;
    }
    return outputJSON($result);
}

function getPreCallDet() {
    $SF = $_GET['sfCode'];
    $MSL = $_GET['Msl_No'];
	$Divc="";
    $result = array();
   $query = "select SLVNo SVL,Division_Code,Doc_Class_ShortName DrCat,PIN_Code pincode,isnull(GST,'') GST,ListedDrCode,Slan_Name,Giftenrol_Name,Doc_Spec_ShortName DrSpl,isnull(stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory S where CHARINDEX(cast(Doc_SubCatCode as varchar),D.Doc_SubCatCode)>0 for XML Path('')),1,2,''),'') DrCamp,isnull(stuff((select ', '+Product_Detail_Name from Map_LstDrs_Product M  inner join Mas_Product_Detail P on M.Product_Code=P.Product_Detail_Code and P.Division_Code=M.Division_Code where Listeddr_Code=D.ListedDrCode for XML Path('')),1,2,''),'') DrProd,Case WHEN ListedDr_Address1!=''  and ListedDr_Address1 IS NOT NULL THEN ListedDr_Address1 WHEN ListedDr_Address2!=''  and ListedDr_Address2 IS NOT NULL THEN ListedDr_Address2 ELSE ListedDr_Address3 END as Address  from mas_listeddr  D where ListedDrCode='" . $MSL . "'";
  $as = performQuery($query);
  $query = "select top 1 Slan_Name   from Gift_Mas_ListedDr where  ListedDrCode='" . $MSL . "' ";
  $rvtQuery = "select top 1 convert(varchar, Last_Visit_Date,101) Last_Visit_Date, convert(varchar,Last_Order_Date,101) Last_Order_Date from retailer_visit_time where RetailerCode='" . $MSL . "' ";
 $rvtQueryRes = performQuery($rvtQuery);
  $gift = performQuery($query);
    if (count($as) > 0) {
        $result['SVL'] = (string) $as[0]['SVL'];
		$Divc = (string) $as[0]['Division_Code'];
		$result['ListedDrCode'] = (string) $as[0]['ListedDrCode'];
        $result['DrCat'] = (string) $as[0]['DrCat'];
        $result['DrSpl'] = (string) $as[0]['DrSpl'];
        $result['DrCamp'] = (string) $as[0]['DrCamp'];
        $result['DrProd'] = (string) $as[0]['DrProd'];
        $result['Address'] = (string) $as[0]['Address'];
		$result['SlabName'] = (string) $as[0]['Slan_Name'];
		$result['GST'] = (string) $as[0]['GST'];
		$result['pincode'] = (string) $as[0]['pincode'];
        $result['Giftenrol_Name'] = (string) $as[0]['Giftenrol_Name'];
		$result['Last_Visit_Date'] = (string) $rvtQueryRes[0]['Last_Visit_Date'];
		$result['Last_Order_Date'] = (string) $rvtQueryRes[0]['Last_Order_Date'];
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

		$query="select top 1 Order_Value from trans_order_head  where sf_code='".$SF."' and Cust_Code='".$MSL."'    order by Order_Date desc";

		 $LASTORDER=performQuery($query);



        $queryy="select SUM (Order_Value) AS MorderSum from Trans_Order_Head  where Cust_Code='".$MSL."' and MONTH(Order_Date) = '".$dateYeabbr."' AND YEAR(Order_Date) = '".$year."'";
        $MOV = performQuery($queryy);
$query = "select *,case when (isnull(contactperson,'') +','+ isnull(contactperson2,'')+','+ isnull(contactperson3,'')=',,') then (' ') else isnull(contactperson,'') +','+ isnull(contactperson2,'')+','+ isnull(contactperson3,'') end  as ContactPersionn  from Mas_ListedDr with(nolock)  where ListedDrCode='".$MSL."' ";
        $POT = performQuery($query);
        if (count($collectionOrder) > 0) {
            for ($i = 0; $i < count($transHead); $i++) {
                $query = "select Product_Code,Product_Name,Quantity,value from ( select ROW_NUMBER() OVER(PARTITION BY Cust_Code,Stockist_Code order by Cust_Code,Stockist_Code,Order_Date desc) RW,Trans_Sl_No,sf_code,Cust_Code,Stockist_Code,Order_Date,route from Trans_Order_Head with(nolock) where Cust_Code='".$MSL."' and Stockist_Code='".$transHead[$i]['stockist_code']."') as H inner join Trans_Order_details D with(nolock) on H.Trans_Sl_No=D.Trans_Sl_No and RW=1";
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


        $result['StockistDetails'] = $stockist;
        $result['OpeningStock'] =  $OpeningStock;
        $result['MOQ'] = $MOQ;
        $result['POTENTIAL'] = $POT;
		$result['LastorderAmount'] = $LASTORDER[0]['Order_Value'];
        $result['MOV'] = $MOV;
		$query = "select top 1 D.Activity_Remarks from DCRMain_Trans h with(nolock) inner join DCRDetail_Lst_Trans d with(nolock) on h.Trans_SlNo=d.Trans_SlNo where Trans_Detail_Info_Code='" . $MSL . "' and Activity_Date>=dateadd(month,-3,getdate()) order by Activity_Date desc"; 
		$Rems = [];///performQuery($query);
		$result['last_order_remarks'] ="-";
		if(count($Rems)>0)
		$result['last_order_remarks'] =$Rems[0]["Activity_Remarks"];
      /*   $query = "select Trans_SlNo,Trans_Detail_Slno,convert(varchar,Time,0) Adate,
convert(varchar,cast(convert(varchar,Activity_Date,101)  as datetime),20) as DtTm,
(Select content from vwFeedTemplate where ID=Rx) CalFed,Activity_Remarks,products,gifts
from vwLastVstDet left outer join vwFeedTemplate on  ID=RX where rw=1 and Trans_Detail_Info_Code='" . $MSL . "' order by Adate"; //and SF_Code='" . $SF . "'*/
        $query="exec getRetailerPrecall '" . $MSL . "'";
		if($Divc=="73"){
			$as = performQuery($query);
		}
		else{
			$as=[];
		}
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


	if($divisionCode != 143 || $tableName != "vwTown_Master_APP")
		$filtereColumns = join(",", $coloumns);
	else {

		$coloumns[1] = "CONCAT(town_name,' - ',Allowance_Type) as name";

		$filtereColumns = join(",", $coloumns);

	}


    $query = "SELECT " . $filtereColumns . " FROM $tableName as tab";
	//print($query);
    //$query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
    if (!is_null($sfCode)) {
			if($sfCode=="") $sfCode="$$$$";
        if ($tableName == 'vwTown_Master_APP' )                                   //if ($desig == "mgr" && ($tableName == 'vwTown_Master_APP' || $tableName == "vwDoctor_Master_APP"))
            $query .=" WHERE charindex(',$sfCode,',','+tab.SF_Code+',')>0";
        else if ($tableName == "vwDoctor_Master_APP")
			$query .=" WHERE charindex(',$sfCode,',','+tab.Field_Code+',')>0";
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

function orderDetailsDelete($sfCode, $custCode,$arc, $amc,$sec) {
    $date = date('Y-m-d');
	
	if($sec!="1"){
		 $sql = "select Trans_Sl_No,Order_Value,Collected_Amount from Trans_Order_Head where DCR_Code ='$amc'";
		 
	}else{
       $sql = "select Trans_Sl_No,Order_Value,Collected_Amount from Trans_Order_Head where Trans_Sl_No ='$amc'";
	   
	}
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
function priOrderDetailDelete($sfCode,$arc,$amc,$secVal){
    $date = date('Y-m-d');
    if($secVal=="1"){
		// $sql = "select Trans_Sl_No from Trans_PriOrder_Head where DCR_Code ='$arc'";
		  $sql = "select Trans_Sl_No from Trans_PriOrder_Head where Trans_Sl_No ='$amc'";
	   }

      $trDet = performQuery($sql);

 	 
 for ($t = 0; $t < count($trDet); $t++){
        $Trans_Sl_No = $trDet[$t]['Trans_Sl_No'];
	
        $query = "delete from Trans_PriOrder_Head WHERE Trans_Sl_No ='" . $Trans_Sl_No . "'";
         performQuery($query);
        $sql = "select Trans_POrd_No from Trans_PriOrder_Details where Trans_Sl_No='" . $Trans_Sl_No . "'";
        $tr = performQuery($sql);
		
        for ($i = 0; $i < count($tr); $i++) {
            $Trans_POrd_No = $tr[$i]['Trans_POrd_No'];
		
            $sql = "delete from Trans_PriOrder_Details WHERE Trans_POrd_No='" . $Trans_POrd_No . "'";
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


function svTrackDetail() {
    global $data;

    $sfCode = (string) $data['SF'];
    $device = (string) $data['device'];
    $gps = (string) $data['gps'];
    $time = (string) $data['time'];
    $net = (string) $data['net'];
    $battery = (string) $data['battery'];
    $query = "exec Map_SvTrackDetail '".$sfCode."','".$device."','".$gps."','".$time."','".$net."','".$battery."'";
    performQuery($query);
    $respon['msg'] = "success";
    return $respon;
}
function ViewGeoTag(){

global $data;
    $data = json_decode($_POST['data'], true);
$SF=(string) $data['SF'];
$cust=(string)$data['cust'];

$query="exec getViewTag '".$SF."','".$cust."'";
return performQuery($query);;
}
function SaveGEOTagging(){
	$vals = json_decode($_POST['data'], true);
	
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
	
	$result = array();
	
    $latlng = explode(",", $vals["latlng"] . ",");
	$lat = str_replace(" ","",$latlng[0]);
	$lng = str_replace(" ","",$latlng[1]);
	
	if($vals["Addr"] == '')
		$vals["Addr"]="'".getaddress(str_replace("'","",$vals["lat"]),str_replace("'","",$vals["long"]))."'";
	else
		$vals["Addr"]= "'".str_replace("'","",$vals["Addr"])."'";
	if($vals["Mode"]=="R"){
		$sql = "SELECT isNull(max(MapId),0)+1 as MapId FROM Map_GEO_Customers";
		$topr = performQuery($sql);
		$pk = (int) $topr[0]['MapId'];

		$sql ="select count(Cust_Code) AS [Count] from Map_GEO_Customers   where Cust_Code=" . $vals["Cust_Code"] . "";
		$tr1=performQuery($sql);
		if(!($tr1[0]['Count']>0)){
			$sql = "insert into Map_GEO_Customers(MapId,Cust_Code,lat,long,addrs,StatFlag,Division_code) select " . $pk . "," . $vals["Cust_Code"] . ",'" . $lat  . "','" . $lng . "'," . $vals["Addr"] . ",0,'" . $Owndiv . "'" ;
			performQueryWP($sql,[]);

			$updateAddress = "update vwDoctor_Master_APP set ListedDr_Address1 =". $vals["address"] . " where Doctor_Code=". $vals["Cust_Code"] ;
			performQuery($updateAddress);
			$result['success'] = true;
			$result['Qry'] = $sql;
			$result['msg'] = 'Tagged Successfully...';

		}else{
			
			$result['success'] = false;
			$result['msg'] = 'Already tagged this customer. kindly reload master data.';
		}
	} else {
		$sql = "SELECT isNull(max(MapId),0)+1 as MapId FROM Map_GEO_Distributors";
		$topr = performQuery($sql);
		$pk = (int) $topr[0]['MapId'];

		$sql ="select count(Cust_Code) AS [Count] from Map_GEO_Distributors   where Cust_Code=" . $vals["Cust_Code"] . "";
		$tr1=performQuery($sql);
		if(!($tr1[0]['Count']>0)){
			$sql = "insert into Map_GEO_Distributors(MapId,Cust_Code,lat,long,addrs,StatFlag,Division_code) select " . $pk . "," . $vals["Cust_Code"] . ",'" . $lat  . "','" . $lng . "'," . $vals["Addr"] . ",0,'" . $Owndiv . "'" ;
			performQuery($sql);

			$result['success'] = true;
			$result['Qry'] = $sql;
			$result['msg'] = 'Tagged Successfully...';

		}else{
			
			$result['success'] = false;
			$result['msg'] = 'Already tagged this customer. kindly reload master data.';
		}

	}
	return $result;
}

function addEntry() {
    $sfCode = $_GET['sfCode'];
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $data = json_decode($_POST['data'], true);
    $today = date('Y-m-d 00:00:00');
    $temp = array_keys($data[0]);
    $temp1 = array_keys($data[4]);
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
            //$date = date('Y-m-d H:i:s');
			if($_GET['Selectdate']!=null && $_GET['Selectdate']!="undefined"){
			$date = date('Y-m-d H:i:s',strtotime($_GET['Selectdate']));
			}else{
			$date = date('Y-m-d H:i:s');
			}

			 $Sales_Code = $_GET['sfCode'];

            if ($_GET['sCode'] != "0") {
                $staffCode = $sfCode;
                $sfCode = $_GET['sCode'];
            } else
                $staffCode = null;

            if($sfCode!="undefined")
            {
                if ($editable == 1) {
                    $sql = "SELECT * FROM Trans_Current_Stock_details where Stockist_Code='" . $sfCode . "' and convert(date,Last_Updation_Date)=convert(date,'" . $date . "')";
                    $tRw = performQuery($sql);

                    for ($i = 0; $i < count($tRw); $i++) {
                        $updateDat = $tRw[$i]['Last_Updation_Date'];
                        $updateDate = $updateDat->format($date);
                        $productCode = $tRw[$i]['Product_Code'];
                        $sql = "delete from Trans_Stock_Updation_Details where Stockist_code='$sfCode' and Product_Code='$productCode' and Purchase_Date='$updateDate'";
                        performQuery($sql);
						$DeleteCUrrent=$sql;
                        $sql = "delete from Trans_Secondary_Sales_Details where Stockist_Code='$sfCode' and Product_Code='$productCode' and date='$updateDate'";
                        performQuery($sql);
                    }
                }
                $sql = "delete from Trans_Current_Stock_details where Stockist_Code='$sfCode' and convert(date,Last_Updation_Date)=convert(date,'" . $date . "') ";
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
					$sample_qty = !empty($vals[$i]['sample_qty']) ? $vals[$i]['sample_qty'] : 0;
                   $Mfg=($vals[$i]['Mgf_date']==null? '':$vals[$i]['Mgf_date']);
                   $batch_no=($vals[$i]['batch_no']==null? '':$vals[$i]['batch_no']);
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



                    $sql = "insert into Trans_Stock_Updation_Details(Tran_Slno,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Distributer_Rate,Retailor_Rate,Purchase_Date,Conversion_Qty,SfCode,Mgf_date,TotalValue,batch_no) select '" . $pk . "','" . $sfCode . "','" . $productCode . "',N'" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",$distPrice,$retPrice,'" . $date . "',$sampleErpCode,'$staffCode','$Mfg',$sample_qty,'$batch_no'";
                    performQuery($sql);
					$sqlthiru= $sql;
                    $sql = "SELECT Stockist_Code FROM Trans_Current_Stock_details where Stockist_Code='$sfCode' and Product_Code='$productCode'  and convert(date,Last_Updation_Date)=convert(date,'" . $date . "')";
                    $tRw = performQuery($sql);
                    if (!empty($tRw)) {
                        $sql = "DELETE FROM Trans_Current_Stock_details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and convert(date,Last_Updation_Date)=convert(date,'" . $date . "') ";
                        performQuery($sql);
                    }


                   $sql = "select Distributor_Price,MRP_Price from mas_product_detail PMD inner join Mas_Product_State_Rates MPS  on PMD.Product_Detail_Code=MPS.Product_Detail_Code   where PMD.Product_Detail_Code='" . $productCode . "' and MPS.State_Code='$stateCode' ";

                   $CandPrate=performQuery($sql);
                   $Prate= $CandPrate[0]['MRP_Price'];
                   $Crate= $CandPrate[0]['Distributor_Price'];


                    $sql = "insert into Trans_Current_Stock_details(Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Last_Updation_Date,Conversion_Qty,SfCode,Mgf_date,Crate,Prate,TotalValue,batch_no) select '" . $sfCode . "','" . $productCode . "',N'" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",'" . $date . "',$sampleErpCode,'$staffCode','$Mfg','$Crate','$Prate',$sample_qty,'$batch_no'";
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
                    $sql = "insert into Trans_Secondary_Sales_Details(Sale_Code,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cl_Qty,pieces,Distributer_Rate,Retailor_Rate,Op_Qty,Sale_Qty,OP_Pieces,date,Conversion_Qty,sale_pieces,SfCode,RwFlg,Rec_Pieces,EntryDate) select $pk,'" . $sfCode . "','" . $productCode . "',N'" . $productName . "',$recvQty,$cbQty,$pieces,$distPrice,$retPrice,$Op_Qty,$saleQty,$OpP_Qty,'" . $date . "',$sampleErpCode,$salepieces,'$staffCode',1,0,'". date('Y-m-d H:i:s'). "'";
                    performQuery($sql);
                }


				 if( $data[1]['Activity_Event_Captures']!=null && $data[1]['Activity_Event_Captures']!="undefined")
            {
                $Event_Captures = $data[1]['Activity_Event_Captures'];
                for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    $visitpath=$Sales_Code.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);

                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,DCRDetNo) values( '".$sfCode."','" . $visitpath . "','" . $ev_title . "','" . $ev_remarks . "','".$Owndiv."','Closing Stock','" . $date . "','" .$Sales_Code."')";
                    performQuery($sql);
                }
            }



                $resp["success"] = true;
				$resp["sqlthiru"] = $sqlthiru;
				$resp["DeleteCUrrent"] = $DeleteCUrrent;



            }else{
                $resp["success"] = false;
            }
            echo json_encode($resp);
            die;
            break;

case "stockUpdationSSNative":
                        $stateCode = $_GET['State_Code'];
                        $editable = $_GET['editable'];
                        //$date = date('Y-m-d H:i:s');
            			if($_GET['Selectdate']!=null && $_GET['Selectdate']!="undefined"){
            			$date = date('Y-m-d H:i:s',strtotime($_GET['Selectdate']));
            			}else{
            			$date = date('Y-m-d H:i:s');
            			}

            			 $Sales_Code = $_GET['sfCode'];

                        if ($_GET['sCode'] != "0") {
                            $staffCode = $sfCode;
                            $sfCode = $_GET['sCode'];
                        } else
                            $staffCode = null;

                        if($sfCode!="undefined")
                        {
                            if ($editable == 1) {
                                $sql = "SELECT * FROM Trans_Current_SSStock_details where Stockist_Code='" . $sfCode . "' and convert(date,Last_Updation_Date)=convert(date,'" . $date . "')";
                                $tRw = performQuery($sql);

                                for ($i = 0; $i < count($tRw); $i++) {
                                    $updateDat = $tRw[$i]['Last_Updation_Date'];
                                    $updateDate = $updateDat->format($date);
                                    $productCode = $tRw[$i]['Product_Code'];
                                    $sql = "delete from Trans_SS_Stock_Updation_Details where Stockist_code='$sfCode' and Product_Code='$productCode' and Purchase_Date='$updateDate'";
                                    performQuery($sql);
            						$DeleteCUrrent=$sql;
                                    $sql = "delete from Trans_Secondary_SSSales_Details where Stockist_Code='$sfCode' and Product_Code='$productCode' and date='$updateDate'";
                                    performQuery($sql);
                                }
                            }
                            $sql = "delete from Trans_Current_SSStock_details where Stockist_Code='$sfCode' and convert(date,Last_Updation_Date)=convert(date,'" . $date . "') ";
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
            					$sample_qty = !empty($vals[$i]['sample_qty']) ? $vals[$i]['sample_qty'] : 0;
                               $Mfg=($vals[$i]['Mgf_date']==null? '':$vals[$i]['Mgf_date']);
                               $batch_no=($vals[$i]['batch_no']==null? '':$vals[$i]['batch_no']);
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



                                $sql = "insert into Trans_SS_Stock_Updation_Details(Tran_Slno,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Distributer_Rate,Retailor_Rate,Purchase_Date,Conversion_Qty,SfCode,Mgf_date,TotalValue,batch_no) select '" . $pk . "','" . $sfCode . "','" . $productCode . "',N'" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",$distPrice,$retPrice,'" . $date . "',$sampleErpCode,'$staffCode','$Mfg',$sample_qty,'$batch_no'";
                                performQuery($sql);
            					$sqlthiru= $sql;
                                $sql = "SELECT Stockist_Code FROM Trans_Current_SSStock_details where Stockist_Code='$sfCode' and Product_Code='$productCode'  and convert(date,Last_Updation_Date)=convert(date,'" . $date . "')";
                                $tRw = performQuery($sql);
                                if (!empty($tRw)) {
                                    $sql = "DELETE FROM Trans_Current_SSStock_details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and convert(date,Last_Updation_Date)=convert(date,'" . $date . "') ";
                                    performQuery($sql);
                                }


                               $sql = "select Distributor_Price,MRP_Price from mas_product_detail PMD inner join Mas_Product_State_Rates MPS  on PMD.Product_Detail_Code=MPS.Product_Detail_Code   where PMD.Product_Detail_Code='" . $productCode . "' and MPS.State_Code='$stateCode' ";

                               $CandPrate=performQuery($sql);
                               $Prate= $CandPrate[0]['MRP_Price'];
                               $Crate= $CandPrate[0]['Distributor_Price'];


                                $sql = "insert into Trans_Current_SSStock_details(Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Last_Updation_Date,Conversion_Qty,SfCode,Mgf_date,Crate,Prate,TotalValue,batch_no) select '" . $sfCode . "','" . $productCode . "',N'" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",'" . $date . "',$sampleErpCode,'$staffCode','$Mfg','$Crate','$Prate',$sample_qty,'$batch_no'";
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
                                $sql = "insert into Trans_Secondary_SSSales_Details(Sale_Code,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cl_Qty,pieces,Distributer_Rate,Retailor_Rate,Op_Qty,Sale_Qty,OP_Pieces,date,Conversion_Qty,sale_pieces,SfCode,RwFlg,Rec_Pieces,EntryDate) select $pk,'" . $sfCode . "','" . $productCode . "',N'" . $productName . "',$recvQty,$cbQty,$pieces,$distPrice,$retPrice,$Op_Qty,$saleQty,$OpP_Qty,'" . $date . "',$sampleErpCode,$salepieces,'$staffCode',1,0,'". date('Y-m-d H:i:s'). "'";
                                performQuery($sql);
                            }


            				 if( $data[1]['Activity_Event_Captures']!=null && $data[1]['Activity_Event_Captures']!="undefined")
                        {
                            $Event_Captures = $data[1]['Activity_Event_Captures'];
                            for ($j = 0; $j < count($Event_Captures); $j++) {
                                $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                                $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                                $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                                $visitpath=$Sales_Code.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);

                                $sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,DCRDetNo) values( '".$sfCode."','" . $visitpath . "','" . $ev_title . "','" . $ev_remarks . "','".$Owndiv."','Closing Stock','" . $date . "','" .$Sales_Code."')";
                                performQuery($sql);
                            }
                        }



                            $resp["success"] = true;
            				$resp["sqlthiru"] = $sqlthiru;
            				$resp["DeleteCUrrent"] = $DeleteCUrrent;



                        }else{
                            $resp["success"] = false;
                        }
                        echo json_encode($resp);
                        die;
                        break;


			case "SecOrderValueConfirm":
			$sf_code = $_GET['sfCode'];
			 $divCode = $_GET['divisionCode'];
             $divisionCode = explode(",", $divCode);
			 $Owndiv = (string) $divs[0];
			 $order_value=$vals["order_value"];
			 $acheived_value=$vals["acheived_value"];
			 $balance=$vals["balance"];
			 $month=$vals["month"];
			 $year=$vals["year"];
			 if($month=="00"){
				 $month=12;
				 $year=2021;
			 }
			 $sql = "SELECT isNull(max(cast(id as numeric)),0)+1 as RwID FROM sec_order_val_confirmation";
            $tRw = performQuery($sql);
            $pk1 = (int) $tRw[0]['RwID'];
			 $sql="insert into sec_order_val_confirmation(id,sf_code,division_code,created_date,tot_order_value,acheived_value,diff_value,approve_flag,month,year,created_by)
                    select '$pk1','$sf_code',$Owndiv,getdate(),'$order_value','$acheived_value','$balance',0,'$month','$year','$sf_code'";
			performQuery($sql);
			$resp["success"] = true;
			   echo json_encode($resp);
			die;
			break;
            case "OrderDespatchDetails":
			$date = date('Y-m-d H:i:s');
			 $sf_code = $_GET['sfCode'];
			 $divCode = $_GET['divisionCode'];
             $divisionCode = explode(",", $divCode);
			 $Owndiv = (string) $divs[0];
			 $cust_code=$vals["cust_code"];
			 $cust_name=$vals["cust_name"];
			 $order_value=$vals["order_value"];
			 $order_date=$vals["order_date"];
			 $trans_slno=$vals["trans_slno"];
			 $order_no=$vals["order_no"];
			  $remarks=$vals["remarks"];
			  $invoice_value=$vals["invoice_value"];
			 $imgurl="";

				 if( $data[1]['Activity_Event_Captures']!=null && $data[1]['Activity_Event_Captures']!="undefined")
            {
                $Event_Captures = $data[1]['Activity_Event_Captures'];
             //   for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=str_replace("'", "",$Event_Captures[0]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[0]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[0]["remarks"]);
                    $imgurl=$sf_code.'_'.''.str_replace("'", "",$Event_Captures[0]["imgurl"]);

                //}
            }
			 $sql="insert into Order_Despatch_Confirmation(Trans_Sl_No,Order_No,Sf_Code,Cust_Code,Division_Code,Order_value,Order_Date,Remarks,Cust_Name,imgurl,invoice_value,created_date) select '".$trans_slno."','".$order_no."','".$sf_code."','".$cust_code."','".$Owndiv."','".$order_value."','".$order_date."','".$remarks."','".$cust_name."','".$imgurl."','".$invoice_value."','".$date."'";
			performQuery($sql);
			$resp["success"] = true;
			   echo json_encode($resp);
			die;
			break;
			case "Gift_Claim":

             $date = date('Y-m-d H:i:s');
			 $Sales_Code = $_GET['sfCode'];
			 $divCode = $_GET['divisionCode'];
             $divisionCode = explode(",", $divCode);
			 $Retailer_Id=$vals["Retailer_Id"];
			 $Retailer_Name=$vals["Retailer_Name"];
			 $Distributor_Id=$vals["Distributor_Id"];
			 $StartDate=$vals["StartDate"];
			 $End_Date=$vals["End_Date"];
			  $Ekey=$vals["Ekey"];
			 $ExptBusiness=$vals["ExptBusiness"];
			 $DisplayScheme=$vals["DisplayScheme"];

			$GiftFlag=$vals["GiftFlag"];
			if($vals["Slab_Select_Id"]=='undefined' && $vals["Slab_Select_Id"]==null)
				$Slab_Select_Id='';
			else
				$Slab_Select_Id=$vals["Slab_Select_Id"];
			if($vals["Gift_Select_Id"]=='undefined' && $vals["Gift_Select_Id"]==null)
				$Gift_Select_Id='';
			else
			$Gift_Select_Id=$vals["Gift_Select_Id"];

			$sql = "SELECT isNull(max(Sl_No),0)+1 as RwID FROM Trans_Gift_Claim";
                    $tRw = performQuery($sql);
                    $pk = (int) $tRw[0]['RwID'];


			$sql = "insert into Trans_Gift_Claim(Sl_No,Sf_Code,Division_Code,ListedDrCode,Stockist,Startt_Date,End_Date,Display_Scheme,Expected_Business,Slab_Id,Gift_Id,Insert_Date,Gift_Type,Aproval_Flag,Ekey,Retailer_Name) select '" . $pk . "','" . $sfCode . "','" . $Owndiv . "','" . $Retailer_Id . "','" . $Distributor_Id . "','" . $StartDate . "','" . $End_Date . "' ,'" . $DisplayScheme . "','" . $ExptBusiness . "' ,'" . $Slab_Select_Id . "'  ,'" . $Gift_Select_Id . "' ,'".$date."','".$GiftFlag."','0','" . $Ekey . "','" . $Retailer_Name . "'";

			performQuery($sql);



				 if( $data[1]['Activity_Event_Captures']!=null && $data[1]['Activity_Event_Captures']!="undefined")
            {
                $Event_Captures = $data[1]['Activity_Event_Captures'];
                for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    $visitpath=$sfCode.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,DCRDetNo,lat) values( '".$pk."','" . $visitpath . "','" . $ev_title . "','" . $ev_remarks . "','".$Owndiv."','Gift_Claim','" . $date . "','" .$sfCode."','" . $Ekey . "')";
                    performQuery($sql);
                }
            }


			$Invoice=$vals["Invoice"];
			 if( $Invoice!=null && $Invoice!="undefined")
            {

                for ($j = 0; $j < count($Invoice); $j++) {
                    $invoicedate=str_replace("'", "",$Invoice[$j]["date"]);
                    $number=str_replace("'", "",$Invoice[$j]["number"]);
                    $Amount=str_replace("'", "",$Invoice[$j]["Amount"]);

                    $sql = "insert into trans_Gift_invoice(Sf_Code,Sl_No,Amount,Invoice_date,Invoice_number,Ekey) values( '".$sfCode."','" . $pk . "','" . $Amount . "','" . $invoicedate . "','".$number."','" . $Ekey . "')";
                    performQuery($sql);


				}
			}

			$GiftProductList=$vals["GiftProductList"];

			 if( $GiftProductList!=null && $GiftProductList!="undefined")
            {

                for ($j = 0; $j < count($GiftProductList); $j++) {
                    $Product_Id=str_replace("'", "",$GiftProductList[$j]["jointwork"]);
                    $Product_name=str_replace("'", "",$GiftProductList[$j]["jointworkname"]);


                    $sql = "insert into trans_Gift_Product(Sf_Code,Sl_No,Product_Id,Product_name,Ekey) values( '".$sfCode."','" . $pk . "','" . $Product_Id . "','" . $Product_name . "','" . $Ekey . "')";
                    performQuery($sql);


				}
			}
			$MultiProductGift=$vals["MultiProductGift"];

			 if( $MultiProductGift!=null && $MultiProductGift!="undefined")
            {

                for ($j = 0; $j < count($MultiProductGift); $j++) {
                    $Product_Id=str_replace("'", "",$MultiProductGift[$j]["giftPid"]);
                    $Product_name=str_replace("'", "",$MultiProductGift[$j]["gifPName"]);

                    $GiftQty=str_replace("'", "",$MultiProductGift[$j]["GiftQty"]);
                    $gifExpRate=str_replace("'", "",$MultiProductGift[$j]["gifExpRate"]);
					 $gifExpDate=str_replace("'", "",$MultiProductGift[$j]["gifExpDate"]);

                    $sql = "insert into trans_Gift_Product(Sf_Code,Sl_No,Product_Id,Product_name,Ekey,Rate,Qty,gifExpDate) values( '".$sfCode."','" . $pk . "','" . $Product_Id . "','" . $Product_name . "','" . $Ekey . "' ,'$gifExpRate','$GiftQty','" . $gifExpDate . "')";
                    performQuery($sql);


				}
			}



			$MultiGift=$vals["MultiGift"];

			 if( $MultiGift!=null && $MultiGift!="undefined")
            {

                for ($j = 0; $j < count($MultiGift); $j++) {
                    $Product_Id=str_replace("'", "",$MultiGift[$j]["giftid"]);
                    $Product_name=str_replace("'", "",$MultiGift[$j]["gifName"]);
                  $GiftQty=str_replace("'", "",$MultiGift[$j]["GiftQty"]);

                    $sql = "insert into Trans_Billed_Gift(Sf_code,Sl_no,Product_Id,Product_name,Ekey,Qty) values( '".$sfCode."','" . $pk . "','" . $Product_Id . "','" . $Product_name . "','" . $Ekey . "','$GiftQty')";
                    performQuery($sql);


				}
			}
		$MultiProductlGift=$vals["MultiProductlGift"];

			 if( $MultiProductlGift!=null && $MultiProductlGift!="undefined")
            {

                for ($j = 0; $j < count($MultiProductlGift); $j++) {
                    $Product_Id=str_replace("'", "",$MultiProductlGift[$j]["giftplid"]);
                    $Product_name=str_replace("'", "",$MultiProductlGift[$j]["giftplName"]);
                  $GiftQty=str_replace("'", "",$MultiProductlGift[$j]["GiftplQty"]);

                    $sql = "insert into Trans_Product_Gift(Sf_code,Sl_no,Product_Id,Product_name,Ekey,Qty) values( '".$sfCode."','" . $pk . "','" . $Product_Id . "','" . $Product_name . "','" . $Ekey . "','$GiftQty')";
                    performQuery($sql);


				}
			}
		$MultiSlab=$vals["MultiSlab"];

			 if( $MultiSlab!=null && $MultiSlab!="undefined")
            {

                for ($j = 0; $j < count($MultiSlab); $j++) {
                    $Product_Id=str_replace("'", "",$MultiSlab[$j]["slabid"]);
                    $Product_name=str_replace("'", "",$MultiSlab[$j]["slabName"]);
                  $slabQty=str_replace("'", "",$MultiSlab[$j]["slabQty"]);

                    $sql = "insert into Trans_Slab_Gift(Sf_code,Sl_no,Product_Id,Product_name,Ekey,Qty) values( '".$sfCode."','" . $pk . "','" . $Product_Id . "','" . $Product_name . "','" . $Ekey . "','$GiftQty')";
                    performQuery($sql);


				}
			}

	       $resp["success"] = true;

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

			$sql="select sf_type from mas_salesforce where sf_code='$sfCode'";
			$Sf_type=performQuery($sql);

			$ww=$vals["worked_with"];
			$datesumm=date('Y-m-d');
			$today=date('Y-m-d H:i:s');
			if(str_replace("'", "", $Sf_type[0]['sf_type']) == "2"){

			$missedquery="exec svPostMissAndAuto  '$sfCode','".$today."','" . $Owndiv . "'";

			performQuery($missedquery);

			}
            if($vals["dcr_activity_date"]!=null && $vals["dcr_activity_date"]!=''){
                    $today=str_replace("'", "", $vals["dcr_activity_date"]);
                    $datesumm=date('Y-m-d ',strtotime($today));
            }else{
				
				$sqlsp="data:".$_POST['data'];
				file_put_contents("../server/logdt/errdate_".date('Y_m_d_H_i_s').".txt",$sqlsp,FILE_APPEND);
				
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
			if (str_replace("'", "", $vals["FWFlg"]) == "DH" &&   ( $vals["Place_Inv"]!=null && $vals["Place_Inv"]=="Y") &&(str_replace("'", "", $vals["cluster"])=="" )) {
				 $result['msg'] = "Select The Route! Route Is Manditory For DH Because Of Expense Claim ";
				 $result["success"] = false;
                 outputJSON($result);
                  die;
			}
			if($sfCode=="MR3533") $vals["SundayWorkigFlag"]="0";
			if($vals["SundayWorkigFlag"]!=null && str_replace("'", "", $vals["SundayWorkigFlag"]) == "1"){
			if(date('w', strtotime($today))=="0"){
			$sqlGetC="select count(Sf_Code) Cnt,ActiveFlag from Trans_SundayWorking where Sf_Code='$sfCode' and  convert(date,sundaydate)= convert(date,'".$today."')  group by ActiveFlag";
			$CountCheck=  performQuery($sqlGetC);
			if(count($CountCheck)> 0){
				if($CountCheck[0]["ActiveFlag"]=="0"){
				 $result['msg'] = "Waiting For Approval(Sunday Working)";
				 $result["success"] = false;
                 outputJSON($result);
                  die;
				}else if($CountCheck[0]["ActiveFlag"]=="2"){
					 $result['msg'] = "Request has been Rejected By Admin!";
				 $result["success"] = false;
                 outputJSON($result);
                  die;
				}
			 }else{
				 $result['msg'] = "Raise Request For Sunday Working!";
				 $result["success"] = false;
                 outputJSON($result);
                 die;

			 }
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

				if($vals["AppVersion"]==null)
                    $Appversion="";
              else
				$Appversion=$vals["AppVersion"];
			if($vals["Place_Inv"]==null)
                    $Place_Inv="";
              else
				$Place_Inv=$vals["Place_Inv"];

			$sql = "insert into tbMyDayPlan(sf_code,sf_member_code,Pln_Date,cluster,remarks,Division_Code,wtype,FWFlg,ClstrName,stockist,worked_with_code,worked_with_name,dcrtype,location,CustId,CustName,StkName,Sprstk,Other_Works,Appversion,Place_Inv) select '" . $sfCode . "'," . $vals["sf_member_code"] . ",'".$today."'," . $vals["cluster"] . "," . $vals["remarks"] . ",'" . $Owndiv . "'," . $vals["wtype"] . "," . $vals["FWFlg"] . "," . $vals["ClstrName"] . "," . $vals["stockist"].",$ww,(Select SF_Name+', ' from vwUserDetails where charindex('$$'+sf_Code+'$$','$$'+$ww+'$$')>0 for XML path('')),'$dcrtype','$location','".$vals["custid"]."','".$vals["custName"]."','".$vals["stkName"]."' ,'".$vals["superstockistid"]."' ,'".$OtherWors."','".$Appversion."' , '".$Place_Inv."'";
			performQuery($sql);

			$loc = explode(":", str_replace("'", "", $location) . ":");
            $lat = $loc[0]; //latitude
            $lng = $loc[1]; //longitude

			$dateTime11=date('Y-m-d H:i');

            $login_date11=date('Y-m-d');

			 $sql="select * from TP_Attendance_App where Sf_Code='$sfCode' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$login_date11' order by id desc";
             $tpa=performQuery($sql);

            $sqlsrt="select isnull(SrtNd,1) SrtNd from Access_Master where division_code=$Owndiv";
		    $setuppps=performQuery($sqlsrt);
			 $srtnd = $setuppps[0]['SrtNd'];
            if(count($tpa)==0&&$srtnd==1){
                $sql ="exec Attendance_entry '$sfCode','$Owndiv','$dateTime11','" . $lat . "','" . $lng . "','$login_date11'";
                $result=performQuery($sql);
            }
            $sql = "select sf_emp_id,Employee_Id from Mas_Salesforce where Sf_Code='$sfCode'";
            $sf = performQuery($sql);
            $empid = $sf[0]['sf_emp_id'];
            $employeeid = $sf[0]['Employee_Id'];

			$sql = "insert into tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Auc) select '$sfCode','$empid','$employeeid','" . $today . "','".$lat."','".$lng."','1'";

			 performQuery($sql);

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
			//$Ukey=str_replace("'", "",$vals['unlisted_doctor_name']).str_replace("'", "", $vals['DrKeyId']);
            $divCode = $_GET['divisionCode'];
            $divisionCode = explode(",", $divCode);
            $sqlrnd="SELECT Retailer_add_ND from Access_Master where division_code='".$divisionCode[0]."'";
            $flagofretailerND= performQuery($sqlrnd)[0]['Retailer_add_ND'];

			$today=date('Y-m-d H:i:s');

			$sql = "select * from Mas_ListedDr where listeddr_name=" . $vals['unlisted_doctor_name'] . " and Territory_Code=" . $vals["town_code"] . "";
			$tr=performQuery($sql);
			if(count($tr)>0&&($divCode=="4,"||$divCode=="29,")){
				$resp["success"] = false;
				$resp["sql"] = $sql;
				$resp["msg"]="Retailer already exists..";
				echo json_encode($resp);
				die;
			}
			if($vals['lat']==null || $vals['lat']=="undefined"){
				$Lat='';
				$Long='';
			}else{
				$Lat=$vals["lat"];
				$Long=$vals["long"];
			}

   $visitpath='';

if($vals['Retailerphoto']!=null && $vals['Retailerphoto']!="undefined")
            {


                   $visitpath=$sfCode.'_'.''.$vals['Retailerphoto']['imgurl'];

                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,Division_Code,Identification,Insert_Date_Time) values( '$sfCode','" . $State_Code . "','" . $vals['Retailerphoto']['imgurl'] . "','" . $Owndiv . "','Retailer Pic','".$today."')";
                    performQuery($sql);

            }


         $ListedDr_Active_Flag=0;
         if($flagofretailerND==1){
          $ListedDr_Active_Flag=3;
        }
		   $div = $_GET['divisionCode'];
		if($div=="101,"){

			$vals["unlisted_doctor_phone"]="0".str_replace("'", "", $vals["unlisted_doctor_phone"]);

		}
		if($vals["geoTaggingAddress"] ==null || $vals["geoTaggingAddress"] ==''){
			$vals["geoTaggingAddress"] = 0;
		}
		$sqlFGT = "select count(division_code) cnt from Access_Master where division_code='".$divisionCode[0]."' and isnull(ForceGeoTag,0)=1";
		$rsett=performQuery($sqlFGT);
		if($rsett[0]["cnt"]>0){
			$vals["geoTaggingAddress"] = 1;
		}
		$sql = "{call  svListedDR_APP_Native('$sfCode'," . $vals['unlisted_doctor_name'] . "," . $vals['unlisted_doctor_address'] . ",'" . str_replace("'","",$vals['unlisted_doctor_phone']) . "','" . str_replace("'","",$vals['unlisted_doctor_cityname']) . "','" . str_replace("'","",$vals['unlisted_doctor_areaname']) . "','" . str_replace("'","",$vals['unlisted_doctor_contactperson']) . "', ' " . str_replace("'","",$vals['unlisted_doctor_designation']) . "','" . str_replace("'","",$vals['unlisted_doctor_gst']) . "','" . str_replace("'","",$vals['unlisted_doctor_pincode']) . "'," . $vals["unlisted_specialty_code"] . "," . $vals["unlisted_cat_code"] . "," . $vals["town_code"] . ",$ListedDr_Active_Flag,'" . date('Y-m-d H:i:s') . "',$Owndiv," . $vals["unlisted_class"] . ",0," . $vals['wlkg_sequence'] . ",'',''," . $vals['DrKeyId'] . ",'" . str_replace("'","",$vals['unlisted_doctor_phone2']) . "','" . str_replace("'","",$vals['unlisted_doctor_contactperson2']) . "','" . str_replace("'","",$vals['unlisted_doctor_designation2']) . "', '" .str_replace("'","",$vals['unlisted_doctor_landmark']) . "','" . str_replace("'","",$Ukey) . "','','','','" . $visitpath . "',".$Lat.",".$Long.",'" . str_replace("'","",$vals['unlisted_doctor_contactperson2']) . "','" . str_replace("'","",$vals['unlisted_doctor_phone3']) . "' ,'" . str_replace("'","",$vals['unlisted_doctor_email']) . "','" . str_replace("'","",$vals['ListedDr_DOB']) . "','" . str_replace("'","",$vals['ListedDr_DOW']) . "','" . str_replace("'","",$vals['unlisted_doctor_address3']) . "','','','','". $vals['geoTaggingAddress'] . "','" . $vals['Retailerphoto']['imgurl'] . "','" . $vals['Retailerphoto']['unlisted_doctor_keyoutlet'] . "'  )}";
		performQueryWP($sql,[]);

		$resp["success"] = true;
		$resp["sqlFGT"] = $sqlFGT;
		$resp["Query"] = $sql;
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

    echo json_encode($resp);
 die;
 break;

case "Quiz_Results":

				$quizresults=$vals[0];
				$first=$vals[1][0];
			    $surveyId=$first['survey_id'];
                $firstStartTime=$first['start'];
				$firstEndTime=$first['end'];
				if($first['NoOfAttempts']=="2"){
				$second=$vals[2][0];
				$secStartTime=$second['start'];
				$secEndTime=$second['end'];
				}else{
					$secStartTime="";
					$secEndTime="";
				}
			for($i=0;$i<count($quizresults);$i++){
				$quesid=$quizresults[$i]['Question_Id'];
				$inputid=$quizresults[$i]['input_id'];
				$secinputid=$quizresults[$i]['Sec_input_id'];
				$sql="exec svQuizResult '$sfCode','$Owndiv','$quesid','$inputid','$surveyId','$secinputid','$firstStartTime','$firstEndTime','$secStartTime','$secEndTime'";

			   performQuery($sql);
			}

$sql="update Processing_UserList set Process_Status='F' where SurveyId='$surveyId' and sf_code='$sfCode'";

			   performQuery($sql);
			break;



			case "quiz":

                $query = "select survey_id,quiz_title,substring(filepath,charindex(')',filepath)+1,len(filepath)) FileName from QuizTitleCreation where division_code='" . $Owndiv . "' and active=0 and month(effective_date)=Month(GETDATE()) and year(effective_date)=Year(GETDATE()) order by survey_id desc";

                $quiztitle = performQuery($query);
$quiztitle1=array();
$processUser1=array();
for($i=0;$i<count($quiztitle);$i++){
$surveyid=$quiztitle[$i]['survey_id'];
 $query = "select NoOfAttempts type,Type NoOfAttempts,timelimit from Processing_UserList where surveyid='" . $surveyid . "' and sf_code='" . $sfCode . "' and process_status='P' and cast(from_date as date)>=cast(GETDATE() as date) ";
                $processUser = performQuery($query);
if(count($processUser)>0){
$processUser1=$processUser;
$quiztitle1=$quiztitle[$i];
$quiztitle=array();
}
}

$processUser=array();
$processUser=$processUser1;
$quiztitle=array();
$quiztitle[0]=$quiztitle1;

	$surveyid=$quiztitle[0]['survey_id'];
if($quiztitle[0]['FileName']!=""){
$extn=end(explode('.', $quiztitle[0]['FileName']));

 if ($extn == "png" || $extn == "jpg")
             $quiztitle[0]['mimetype']= "image/png";
 else if($extn=="doc"||$extn=="dot")
			  $quiztitle[0]['mimetype']= "application/msword";
 else if($extn=="docx"|| $extn == "DOCX")
			  $quiztitle[0]['mimetype']="application/msword";
 else if ($extn == "xls"||$extn == "xlt"||$extn == "xla")
             $quiztitle[0]['mimetype']= "application/vnd.ms-excel";
		  else if ($extn == "xlsx")
             $quiztitle[0]['mimetype']= "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
         else if ($extn == "mp4")
             $quiztitle[0]['mimetype']= "video/mp4";
else if($extn == "pptx")
  $quiztitle[0]['mimetype']= "application/vnd.openxmlformats-officedocument.presentationml.presentation";
         else
             $quiztitle[0]['mimetype']= "application/" + $extn;

}


                $query = "select Question_Type_id,Question_Id,Question_Text,surveyid from AddQuestions where surveyid='" . $surveyid . "' order by question_id asc";
                $questions = performQuery($query);
                $query = "select input_id,Question_Id,Input_Text,Correct_Ans from AddInputOptions where question_id in (select question_id from AddQuestions where surveyid='" . $surveyid . "') order by question_id asc";
                $answers = performQuery($query);
				$results=array();
				$results['quiztitle'][0]=$quiztitle[0];
				$results['processUser']=$processUser;
				$results['questions']=$questions;
				$results['answers']=$answers;
if(count($processUser)==0)
$results=array();
  echo json_encode($results);
    die;
                break;
 case "Activity_Return_Report_App":

	$SfCode = $_GET['sfCode'];
	 // $distId = $data[0]['Activity_Return_Report_APP']['distributor_id'];
	 //  $time = $data[0]['Activity_Return_Report_APP']['CheckoutTime'];
	   $sql="insert into Trans_Primary_Return_Head Values('".$SfCode. "', '" . str_replace("'", "", $vals["distributor_id"]) . "', '" . str_replace("'", "", $vals["CheckoutTime"]) . "')";
	 performQuery($sql);
	for ($i = 1; $i < count($data); $i++) {
                $tableData = $data[$i];
	if (isset($tableData['Activity_Sample_Report'])){
        $samp = $tableData['Activity_Sample_Report'];

            for ($j = 0; $j < count($samp); $j++) {
							$productCode=$samp[$j]["product_code"];
							$productName=$samp[$j]["product_Name"];
							$ReturnQty=$samp[$j]["ReturnQty"];
							$ReturnType=$samp[$j]["ReturnType"];
							$Remarks=$samp[$j]["Remarks"];
							$spinPos=$samp[$j]["spinPos"];


				   $sql="Exec savePrimaryReturnStock '".$productName."','".$productCode."','".$ReturnQty."','".$spinPos."','".$ReturnType."','".$Remarks."' ";
				   performQuery($sql);

		    }
	  }
	  }


			$resp["success"] = true;
            echo json_encode($resp);


    break;
	 case "Activity_Check_In_App":
  $SfCode = $_GET['sfCode'];
  $retailer_code=str_replace("'", "", $vals["retailer_code"]);
  $checkin_time=str_replace("'", "", $vals["checkin_time"]);
  $entry_date=str_replace("'", "", $vals["entry_date"]);
  $ekey=str_replace("'", "", $vals["ekey"]);
  $CheckoutTime=str_replace("'", "", $vals["CheckoutTime"]);
 $flag=str_replace("'", "", $vals["c_flag"]);
 $div=$_GET['divisionCode'];
 $divs = explode(",", $div . ",");
 $Owndiv = (string) $divs[0];
 $retailer_code=str_replace("'", "", $vals["retailer_name"]);
 $sf_name=str_replace("'", "", $vals["sf_name"]);
 $entry_type=str_replace("'", "", $vals["entry_type"]);
 $sample='';
 $ev_imgurl="";
 

    for ($i = 1; $i < count($data); $i++) {
                $tableData = $data[$i];
              if (isset($tableData['Activity_Event_Captures'])){
                   $Event_Captures  = $tableData['Activity_Event_Captures'];
				     for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    $visitpath=$SfCode.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);

                  // $sql="Update Trans_Cus_Checkin set Cin_Img_Url='".$ev_imgurl."',C_Flag='".$flag."' where Sl_No='".$Sl_No."'";
				//   $sql="Insert into Trans_cus_checkIn(Cin_Img_Url)values('".$ev_imgurl."')";
		//performQuery($sql);
                }
				   }
				   }
	   $sql="insert into Trans_Cus_Checkin(Sf_Code,Retailer_Code,CIn_Time,Entry_Date,eKey,COut_Time,C_Flag,Retailer_Name,Sf_Name,Entry_Type,Cin_Img_Url,Cout_Img_Url) Values('".$SfCode."','".str_replace("'", "", $vals["retailer_code"])."','".str_replace("'", "", $vals["checkin_time"])."','".str_replace("'", "", $vals["entry_date"])."','".str_replace("'", "", $vals["ekey"])."','".$sample."','".str_replace("'", "", $vals["c_flag"])."','".str_replace("'", "", $vals["retailer_name"])."','".str_replace("'", "", $vals["sf_name"])."','".str_replace("'", "", $vals["entry_type"])."','".$ev_imgurl."','')";
       performQuery($sql);
	     // if($entry_type=='I'){
        $query="exec saveInshopCheckIn'".$SfCode."','','".$Owndiv."','".str_replace("'", "", $vals["checkin_time"])."','','','".str_replace("'", "", $vals["sf_name"])."','".str_replace("'", "", $vals["retailer_name"])."','','','".str_replace("'", "", $vals["entry_date"])."'";
	    performQuery($query);
	  // }

       $resp["success"] = true;
        echo json_encode($resp);
        die;

break;
  case "Activity_Check_Out_App":
        $SfCode=$_GET['sfCode'];  
		$CheckoutTime=str_replace("'", "", $vals["CheckoutTime"]);
		$Sl_No=str_replace("'", "", $vals["Sl_No"]);
		$flag=str_replace("'", "", $vals["c_flag"]);
		$sql="Update Trans_Cus_Checkin set COut_Time='".$CheckoutTime."',C_Flag='".$flag."' where Sl_No='".$Sl_No."'";
		performQuery($sql);
		$query="Update Trans_Inshop_Activity_CheckIn set endtime='".str_replace("'", "", $vals["CheckoutTime"])."' where ChIn_SlNo='".$Sl_No."'";
		performQuery($query);
		
			 for ($i = 1; $i < count($data); $i++) {
                $tableData = $data[$i];
              if (isset($tableData['Activity_Event_Captures'])){
                   $Event_Captures  = $tableData['Activity_Event_Captures'];
				     for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    $visitpath=$SfCode.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);

                   $sql="Update Trans_Cus_Checkin set Cout_Img_Url='".$ev_imgurl."',C_Flag='".$flag."' where Sl_No='".$Sl_No."'";
		performQuery($sql);
                }
				   }
				   }
		 $resp["success"] = true;
        echo json_encode($resp);
        die;
	break;	

case "Activity_OutletOrder_Report_App":
 $SfCode = $_GET['sfCode'];
 $div=$_GET['divisionCode'];
 $divs = explode(",", $div . ",");
          $Owndiv = (string) $divs[0];
 $RetailerCode=str_replace("'","",$vals["retailer_id"]);
     $OrderTime=str_replace("'","",$vals["order_time"]);
 $OrderDate=  str_replace("'","",$vals["order_date"]);
 $RetailerName= str_replace("'","",$vals["retailer_name"]);
 $StockType=str_replace("'","",$vals["stock_type"]);
 $eKey=str_replace("'","",$vals["eKey"]);
 //$DcrCode=$vals["dcr_code"];
// str_replace("'","",$vals["retailer_id"])
 
 
  $sql = "select Trans_SlNo  from DCRMain_Trans  where  SF_Code='" . $sfCode . "' and Activity_Date='".date('Y-m-d')."' ";
           $eRw = performQuery($sql);
           if(count($eRw)>0){
              $pkk =$eRw[0]['Trans_SlNo'];
  }
  for ($i = 1; $i < count($data); $i++) {
                $tableData = $data[$i];
 if (isset($tableData['Activity_Sample_Report'])){
          $samp = $tableData['Activity_Sample_Report'];
                     
            for ($j = 0; $j < count($samp); $j++) {
$productCode=$samp[$j]["product_code"];
$productName=$samp[$j]["product_Name"];
$Qty=$samp[$j]["Qty"];
$product_unit_name=$samp[$j]["product_unit_name"];
$product_unit_conv=$samp[$j]["product_unit_conv"];



  $sql="insert into Trans_Pop_Ledger values('".$SfCode."','".$SfCode."','".$productCode."','".$Qty."','".$StockType."','".$OrderDate."','".$OrderTime."')";
                   performQuery($sql);
   $query="Exec saveOutletDcr'".$SfCode."','".$Owndiv."','".$RetailerCode."','".$productCode."','".$Qty."','".$pkk."','".$OrderDate."','".$OrderTime."','".$eKey."' ";
 // $query="insert into Trans_Pop_Dcr_Details Values('".$SfCode."','".$Owndiv."','".$RetailerCode."','".$productCode."','".$Qty."','".$pkk."','".$OrderDate."','".$OrderTime."')";
                   performQuery($query);  

   }
 }
 if (isset($tableData['Activity_Event_Captures'])){
                   $Event_Captures  = $tableData['Activity_Event_Captures'];
				     for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    $visitpath=$SfCode.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);
					
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,DCRDetNo) values( '".$SfCode."','" . $visitpath . "','" . $ev_title . "','" . $ev_remarks . "','".$Owndiv."','Merchandise Activity','" . $OrderTime . "','" .$eKey."')";
                    performQuery($sql);
                }
				   }

}
   $resp["success"] = true;
        echo json_encode($resp);
        die;

break;    
case "Activity_OutletStock_Report_App":
 $SfCode = $_GET['sfCode'];
 $div=$_GET['divisionCode'];
 $divs = explode(",", $div . ",");
          $Owndiv = (string) $divs[0];
 $Retailercode=str_replace("'","",$vals["retailer_id"]);
     $StockTime=str_replace("'","",$vals["stock_time"]);
 $StockDate=str_replace("'","",$vals["stock_date"]);
 $RetailerName=str_replace("'","",$vals["retailer_name"]);
 $StockType=str_replace("'","",$vals["stock_type"]);


 
 for ($i = 1; $i < count($data); $i++) {
          $tableData = $data[$i];
 if (isset($tableData['Activity_Sample_Report'])){
          $samp = $tableData['Activity_Sample_Report'];
                     
            for ($j = 0; $j < count($samp); $j++) {
$productCode=$samp[$j]["product_code"];
$productName=$samp[$j]["product_Name"];
$Stock_Qty=$samp[$j]["product_stock_qty"];


$sql="insert into Trans_Pop_Stock_Loading Values('".$SfCode."','".$Owndiv."','".$StockDate."','".$StockTime."','".$productCode."','".$Stock_Qty."','".$StockDate."')";
                    performQuery($sql);
                   
                    $sqlCode="insert into Trans_Pop_Ledger Values('".$SfCode."','".$SfCode."','".$productCode."','".$Stock_Qty."','".$StockType."','".$StockDate."','".$StockTime."')";
                    performQuery($sqlCode);
 

   }
 }
}
$resp["success"] = true;
        echo json_encode($resp);
        die;

break; 
case "Activity_Inshop_Order_Report_App":
   $SfCode = $_GET['sfCode'];
   $OrderDate=  str_replace("'","",$vals["OrderDate"]); 
   $key=  str_replace("'","",$vals["ekey"]); 
for ($i = 1; $i < count($data); $i++) {
          $tableData = $data[$i];
 if (isset($tableData['Activity_Sample_Report'])){
          $samp = $tableData['Activity_Sample_Report'];
                     
            for ($j = 0; $j < count($samp); $j++) {
                 $productCode=$samp[$j]["product_code"];
                 $productName=$samp[$j]["product_Name"];
                  $Qty=$samp[$j]["Qty"];
				  $product_unit_conv=$samp[$j]["product_unit_conv"];
                  $product_price=$samp[$j]["product_price"];
                  $product_unit_code=$samp[$j]["UnitId"];
                  $product_unit_name=$samp[$j]["product_unit_name"];

                  $sql="exec saveInshopOrders '".$SfCode."','".$OrderDate."','".$key."','".$productCode."','".$Qty."','".$product_unit_conv."','".$product_price."','".$product_unit_code."','".$product_unit_name."' ";
               performQuery($sql);
                   
                }
	}
}
$resp["success"] = true;
        echo json_encode($resp);
        die;
break;
case "Activity_Inshop_OpeningStock_Report_App":
   $SfCode = $_GET['sfCode'];
   $OrderDate=  str_replace("'","",$vals["OrderDate"]); 
   $key=  str_replace("'","",$vals["ekey"]); 
for ($i = 1; $i < count($data); $i++) {
          $tableData = $data[$i];
 if (isset($tableData['Activity_Sample_Report'])){
          $samp = $tableData['Activity_Sample_Report'];
                     
            for ($j = 0; $j < count($samp); $j++) {
                 $productCode=$samp[$j]["product_code"];
                 $productName=$samp[$j]["product_Name"];
                  $Qty=$samp[$j]["Qty"];
				  $product_unit_conv=$samp[$j]["product_unit_conv"];
                  $product_unit_code=$samp[$j]["UnitId"];
                  $product_unit_name=$samp[$j]["product_unit_name"];

                  $sql="exec saveInshopOpStock '".$SfCode."','".$OrderDate."','".$key."','".$productCode."','".$Qty."','".$product_unit_conv."','".$product_unit_code."','".$product_unit_name."' ";
               performQuery($sql);
                   
                }
	}
}
$resp["success"] = true;
        echo json_encode($resp);
        die;
break;
case "Activity_Inshop_ClosingStock_Report_App":
   $SfCode = $_GET['sfCode'];
   $OrderDate=  str_replace("'","",$vals["OrderDate"]); 
   $key=  str_replace("'","",$vals["ekey"]); 
for ($i = 1; $i < count($data); $i++) {
          $tableData = $data[$i];
 if (isset($tableData['Activity_Sample_Report'])){
          $samp = $tableData['Activity_Sample_Report'];
                     
            for ($j = 0; $j < count($samp); $j++) {
                 $productCode=$samp[$j]["product_code"];
                 $productName=$samp[$j]["product_Name"];
                  $Qty=$samp[$j]["Qty"];
				  $product_unit_conv=$samp[$j]["product_unit_conv"];
                  $product_unit_code=$samp[$j]["UnitId"];
                  $product_unit_name=$samp[$j]["product_unit_name"];

                  $sql="exec saveInshopClStock '".$SfCode."','".$OrderDate."','".$key."','".$productCode."','".$Qty."','".$product_unit_conv."','".$product_unit_code."','".$product_unit_name."' ";
               performQuery($sql);
                   
                }
	}
}
$resp["success"] = true;
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


		case "UpdateSlab":

		  $Slab_Id=$vals['Slab_Id'];
		  $Slab_Name=$vals['Slab_Name'];
		   $Retailer_Id=$vals['Retailer_Id'];
		  $GiftClaim_Id=$vals['GiftClaim_Id'];
		  $GiftClaim_Name=$vals['GiftClaim_Name'];
		 $sfCode=$_GET['sfCode'];
		  $month = date('m');
		  $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];

		  if($vals['Retailer_name']=='undefined' && $vals['Retailer_name']==null)
				$Retailer_name='';
			else
				$Retailer_name= $vals['Retailer_name'];

 $sql = "select count(*) C from Gift_Mas_ListedDr where  month(ListedDr_Created_Date)='".$month."' and ListedDrCode='" . $Retailer_Id . "'";
 $CountforMonth=performQuery($sql);

 if($CountforMonth[0]['C']>0){
	 $sql = "update Gift_Mas_ListedDr set Slab_Id='" . $Slab_Id . "'   ,Slan_Name='" . $Slab_Name . "' ,Giftenrol_Id='" . $GiftClaim_Id . "',Giftenrol_Name='" . $GiftClaim_Name . "'  where ListedDrCode='" . $Retailer_Id . "' and month(ListedDr_Created_Date)='".$month."' ";
		      performQuery($sql);
 }else{

	 $sql = "insert into Gift_Mas_ListedDr(ListedDrCode,ListedDr_Name,ListedDr_Created_Date,Slab_Id,Slan_Name,Giftenrol_Id,Giftenrol_Name,SLVNo,ListedDr_Active_Flag,Division_Code,Sf_Code)values('" . $Retailer_Id . "','" . $Retailer_name . "','".date("Y-m-d H:i:s")."','" . $Slab_Id . "','" . $Slab_Name . "','" . $GiftClaim_Id . "','" . $GiftClaim_Name . "','".$month."','0','".$Owndiv."','".$sfCode."' )";
	  performQuery($sql);

 }

 /*
		    $sql = "update Mas_ListedDr set Slab_Id='" . $Slab_Id . "'   ,Slan_Name='" . $Slab_Name . "' ,Giftenrol_Id='" . $GiftClaim_Id . "',Giftenrol_Name='" . $GiftClaim_Name . "'  where ListedDrCode='" . $Retailer_Id . "' ";
		      performQuery($sql); */

               $resp["success"] = true;
			   $resp["Query"] = $sql;
                echo json_encode($resp);
                die;

		break;


case "UpdateSlabRetailer":

		  $Slab_Id=$vals['Slab_Id'];
		  $Slab_Name=$vals['Slab_Name'];
		   $Retailer_Id=$vals['Retailer_Id'];
		 if($vals['Retailer_name']=='undefined' && $vals['Retailer_name']==null)
				$Retailer_name='';
			else
				$Retailer_name= $vals['Retailer_name'];


		     $sfCode=$_GET['sfCode'];
		  $month = date('m');
		  $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];


		  $sql = "select count(*) C from Gift_Mas_ListedDr where  month(ListedDr_Created_Date)='".$month."' and ListedDrCode='" . $Retailer_Id . "'";
 $CountforMonth=performQuery($sql);

 if($CountforMonth[0]['C']>0){
	 $sql = "update Gift_Mas_ListedDr set Slab_Id='" . $Slab_Id . "'   ,Slan_Name='" . $Slab_Name . "'   where ListedDrCode='" . $Retailer_Id . "' and month(ListedDr_Created_Date)='".$month."' ";
		      performQuery($sql);
 }else{

	 $sql = "insert into Gift_Mas_ListedDr(ListedDrCode,ListedDr_Name,ListedDr_Created_Date,Slab_Id,Slan_Name,Giftenrol_Id,Giftenrol_Name,SLVNo,ListedDr_Active_Flag,Division_Code,Sf_Code)values('" . $Retailer_Id . "','" . $Retailer_name . "','".date("Y-m-d H:i:s")."','" . $Slab_Id . "','" . $Slab_Name . "','" . $GiftClaim_Id . "','" . $GiftClaim_Name . "','".$month."','0','".$Owndiv."','".$sfCode."' )";
	  performQuery($sql);

 }



		  /*   $sql = "update Mas_ListedDr set Slab_Id='" . $Slab_Id . "'   ,Slan_Name='" . $Slab_Name . "'   where ListedDrCode='" . $Retailer_Id . "' ";
		      performQuery($sql); */

               $resp["success"] = true;
			   $resp["Query"] = $sql;
                echo json_encode($resp);
                die;

		break;




	case "UpdateChannel":

		 $Rchannel_Id=$vals['Rchannel_Id'];
	     $Rchannel_Name=$vals['Rchannel_Name'];
		 $Retailer_Id=$vals['Retailer_Id'];
		 $sql = "update Mas_ListedDr set Doc_Spec_ShortName='" . $Rchannel_Name . "' ,Doc_Special_Code='" . $Rchannel_Id . "'   where ListedDrCode='" . $Retailer_Id . "'  ";
		 performQuery($sql);
	     $resp["success"] = true;
	    $resp["Query"] = $sql;
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
		   $Remarks = $vals['Remarks'];


		    if($vals['Remarks']==null || $vals['Remarks']=="undefined"){
          $Remarks="Product Availability";
       }else{
          $Remarks=$vals['Remarks'];
        }


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

          $sql="exec ProductAvail_Visit  '" .  $sfCode  . "','".$divisionCode[0]."' ,'".$today." ' , '" . $cluster  . "', '" . $stockist  . "' ,'" . $worktype . "','" . $doctorid . "', '" .  $ARCd  . "','" . $Remarks. "' ";
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
		case "PrimaryClosing":

		 $worktype=$vals['worktype'];
		  $Ekey=$vals['Ekey'];
		  $stockist=$vals['stockist'];
		  $cluster=$vals['cluster'];
		  $entryDate=$vals['entryDate'];
		  $divCode = $_GET['divisionCode'];
          $ProductList=$vals['ProductList'];
          $divCode = $_GET['divisionCode'];
          $divisionCode = explode(",", $divCode);
		   $today=date('Y-m-d H:i');
          $todayDCR=date('Y-m-d 00:00:00.000');
		   $Remarks = $vals['Remarks'];
		   if($vals['Aob']==null)
              $Aob=0;
            else
            $Aob=$vals['Aob'];


		   $ARCd="";
      $sql = "{call  svDCRMain_App(?,?,'" . $worktype . "','" . $cluster  . "',?,'PrimaryCLosing',?,'0')}";
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


		  $sql="exec SV_primary_Closing  '" .  $sfCode  . "','".$divisionCode[0]."' ,'".$entryDate." ' , '" . $cluster  . "', '" . $stockist  . "' ,'" . $worktype . "', '" .  $ARCd  . "','" . $Remarks. "','" . $Aob. "' ";
                   performQuery($sql);






		for ($i = 0; $i < count($ProductList); $i++) {

           $sql = "SELECT isNull(max(Sl_No),0)+1 as Sl_No FROM Trans_Primary_Closing";
            $topr = performQuery($sql);
            $pk = (int) $topr[0]['Sl_No'];
          $sql="insert into Trans_Primary_Closing select '" . $pk . "','" .  $sfCode  . "','".$divisionCode[0]."'  , '" . $cluster . "' ,'" . $stockist . "', '" . $ProductList[$i]['id'] . "','" . $ProductList[$i]['name'] . "','" . $worktype . "', '".date('Y-m-d H:i')." ','" . $ProductList[$i]['Closing']. "','" . $Ekey . "' ,'" . $Remarks. "' ";

		      performQuery($sql);
          }
			$resp["success"] = true;
        echo json_encode($resp);
         die;
		break;
        case "New_TP_Attendance":
           $lat = $vals['lat'];
           $long = $vals['long'];
           $img_url = $vals['imgurl'];
           $mod_name = $vals['mode_name'];
           $mod_id = $vals['mod_id'];
           $daily_allowance = $vals['daily_allowance'];
           $from_place = $vals['from_place'];
           $to_place = $vals['to_place'];
           $to_place_id = $vals['to_placeID'];
           $day_start_km = $vals['day_start_km'];
           $driver_allowance = $vals['driver_allowance'];
           $stEndNeed = $vals['stEndNeed'];
           $update = $_GET['update'];
           $div = $vals['division_code'];
           $today = date('Y-m-d 00:00:00');
           $dateTime=str_replace("'", "", $vals['date_time']);
           $login_date=str_replace("'", "", $vals['date']);
		   $milli_sec = $vals['milli_sec'];

            if($update==0){
                $sql ="exec Attendance_entry '$sfCode','$Owndiv','$dateTime',$lat,$long,'$login_date','$day_start_km'";
                $result['Attendance']=performQuery($sql);
                $insertQuery = "exec INSERT_ExpenseStartAct '$sfCode','$div','$dateTime','$day_start_km','$img_url','$mod_id','$from_place','$to_place','$mod_name','$daily_allowance','$to_place_id','$today','$driver_allowance','$stEndNeed','$milli_sec'";
                $result['query'] = $insertQuery;        				 
                $result['Expenses'] = performQuery($insertQuery);
            }
            outputJSON($result);
        die;
        break;



        case "TP_Attendance":
           
            $lat=$vals['lat'];
            $long=$vals['long'];
            $update=$_GET['update'];

			$today = date('Y-m-d 00:00:00');
			$div = $_GET['divisionCode'];
			$divs = explode(",", $div . ",");
			$Owndiv = (string) $divs[0];

			$day_start_km = '';
	if($vals['date']!=null){
		    $date=str_replace("'", "", $vals['date']);
		    $login_date=str_replace("'", "", $vals['date']);
			}
	else{
	    $date=date('Y-m-d');
        $login_date=date('Y-m-d');
	    }
if($vals['date_time']!=null)
		$dateTime=str_replace("'", "", $vals['date_time']);
	else
            $dateTime=date('Y-m-d H:i');
    if($vals['time']!=null)
			$logintime=str_replace("'", "", $vals['time']);
	else
	$logintime=date("h:i:sa");
			if($vals['day_start_km'] !=null)
			$day_start_km=$vals['day_start_km'];
            if($update==0){
                $sql ="exec Attendance_entry '$sfCode','$Owndiv','$dateTime',$lat,$long,'$login_date','$day_start_km'";
                $result=performQuery($sql);

			if($vals['startKmPhoto']!=null && $vals['startKmPhoto']!="undefined")
            {
                $photoResult = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time) values( '" . $sfCode . "','" . $vals['startKmPhoto']['imgurl'] . "','" .  $vals['startKmPhoto']['title'] . "','" . $vals['startKmPhoto']['remarks'] . "','" . $Owndiv . "','Day Start km Pic','".$today."')";
                performQuery($photoResult);
            }
            }
            else{

                $sql="select id from TP_Attendance_App where Sf_Code='$sfCode' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$date' order by id desc";
                $tr=performQuery($sql);
                $id=$tr[0]['id'];

                $sql="update TP_Attendance_App set End_Lat=$lat,End_Long=$long,End_Time='$dateTime' where id=$id";
                performQuery($sql);

                $sql1="select ID from Attendance_history where Sf_Code='$sfCode' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$date' order by id desc";
                $tr1=performQuery($sql1);
                $id1=$tr1[0]['ID'];
                $sql1="update Attendance_history set End_Lat=$lat,End_Long=$long,End_Time='$dateTime' where ID=$id1";
                performQuery($sql1);
                $result=[];
                $result["msg"]="1";
            }

            outputJSON($result);
            die;
            break;

        case "Map_GEO_Customers":
             if($vals["address"] == '')
				$vals["address"]="'".getaddress(str_replace("'","",$vals["lat"]),str_replace("'","",$vals["long"]))."'";
			else
				$vals["address"]= "'".$vals["address"]."'";
                    $sql = "SELECT isNull(max(MapId),0)+1 as MapId FROM Map_GEO_Customers";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['MapId'];

                    $tableData[$tableName]["MapId"] = $pk ;
                    $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["MapId"];

               $sql ="select count(Cust_Code) AS [Count] from Map_GEO_Customers   where Cust_Code=" . $vals["Cust_Code"] . "";
               $tr1=performQuery($sql);
			   $sql2 ="select StatFlag from Map_GEO_Customers   where Cust_Code=" . $vals["Cust_Code"] . "";
		        $tr2=performQuery($sql2);
              if(!($tr1[0]['Count']>0)||$tr2[0]['StatFlag']==1){
				  if($tr2[0]['StatFlag']==1){
				$sqldel="delete from Map_GEO_Customers where Cust_Code=" . $vals["Cust_Code"] . " and Division_code='" . $Owndiv . "'";
				performQuery($sqldel);
			}
				  
                $sql = "insert into Map_GEO_Customers(MapId,Cust_Code,lat,long,addrs,StatFlag,Division_code) select " . $pk . "," . $vals["Cust_Code"] . "," . $vals["lat"]  . "," . $vals["long"] . "," . $vals["address"] . ",0,'" . $Owndiv . "'" ;
                    performQueryWP($sql,[]);

                    $updateAddress = "update vwDoctor_Master_APP set ListedDr_Address1 =". $vals["address"] . " where Doctor_Code=". $vals["Cust_Code"] ;
                    performQuery($updateAddress);

              }


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
        case "dailyExpense":
		  $dateTime=date('Y-m-d H:i');

          $divCode = $_GET['divisionCode'];
          $divisionCode = explode(",", $divCode);
		  $tempp = array_keys($data[1]);
		  $va =$data[1][$tempp[0]];
		  $temppp = array_keys($data[3]);
		  $val =$data[3][$temppp[0]];
		  		if($divCode=="29,"||$divCode=="4,"){
							$entryDate=$val["Enterdate"];

				$sql="select * from trans_Expense_head1 where sf_code='$sfCode' and expense_month=month('$entryDate') and expense_year=year('$entryDate')
				and (approve_flag=1 or approve_flag=2)";
				//print_r($sql);die;
		$tr=performQuery($sql);
		if(count($tr)>0){
				$resp["msg"]= "Expense Already Approved";
				$resp["Query"]= $sql;
				$resp["success"] = true;
				echo json_encode($resp);
				die;
			}
			}
		  $sql="select sf_type from mas_salesforce where sf_code='".$sfCode."'";
 $tr1=performQuery($sql);
//if($Owndiv=="162"){
	$Enterdate = $val["Enterdate"];
	if($sfCode=="ALLAMGR0149"&&($Enterdate=="2022-12-01"||$Enterdate=="2022-12-1"||$Enterdate=="2022-11-29"||$Enterdate=="2022-11-30")){
		$vall=0;
	}
else if($Owndiv=="162" && $tr1[0]['sf_type']=="1"){
$sql="select trans_slno,FieldWork_Indicator from dcrmain_trans where sf_code='".$sfCode."' and cast(activity_date as date)='".$val["Enterdate"]."'";
 $tr=performQuery($sql);
 $transslno=$tr[0]['trans_slno'];
  $fwflg=$tr[0]['FieldWork_Indicator'];
 $sql="select count(Trans_Detail_Info_Code) cnt from DCRDetail_Lst_Trans where Trans_SlNo='".$transslno."' group by Trans_Detail_Info_Code";
$tr=performQuery($sql);
 $cnt=count($tr);
 $mydate = strtotime('2022-9-01');
 $Enterdate = strtotime($val["Enterdate"]);
if($cnt<15&&$Enterdate>=$mydate&&$fwflg=="F"){
	$resp["result"]= $sql;
           $resp["msg"]= "Less than 15 calls, Expense cannot submitted";
          // $resp["Query"]= $val['Billamount'];
           //$resp["dailyEnteredExp"]= $dailyEnteredExp;
           $resp["success"] = true;
			echo json_encode($resp);die;

}
}

            $dailyEnteredExp="<ROOT>";

            for ($i = 0; $i < count($vals); $i++)
            {
                $dailyEnteredExp=$dailyEnteredExp."<Prod ID=\"".$vals[$i]["ID"]."\" Amt=\"".$vals[$i]["amt"]."\" />";
            }

            $dailyEnteredExp=$dailyEnteredExp."</ROOT>";


  $sql1="insert into  Trans_Expense_Traveldetails(sf_code,Division_code,MOC,Remarks,FromPlace,FromDeparture,ToPlace,ToArrival,Date_Time,Fare)values('".$sfCode."','".$Owndiv ."','" . $va["MOT"] . "'
            ,'".$va["remarks"]."' ,'".$va["Start_image"]."','".$va["Start_Km"]."','".$va["Stop_image"]."','".$va["Stop_km"]."', '$dateTime' ,'".$va["BusFare"]."')";
        performQuery($sql1);


 if( $data[2]['ActivityCaptures']!=null && $data[2]['ActivityCaptures']!="undefined")
            {
                $Event_Captures = $data[2]['ActivityCaptures'];
                for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
                    $ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
                    $visitpath=$sfCode.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,Division_Code,Identification,Insert_Date_Time,Dateandtime) values( '".$sfCode."','" . $visitpath . "','".$Owndiv."','Expense','".$dateTime."','".$val["Enterdate"]."')";
                    performQuery($sql);
                }
            }


		if( $data[4]['HotelBillAttachment']!=null && $data[4]['HotelBillAttachment']!="undefined")
					{
						$Event_Captures = $data[4]['HotelBillAttachment'];
						for ($j = 0; $j < count($Event_Captures); $j++) {
							$ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
							$ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
							$ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);
							$visitpath=$sfCode.'_'.''.str_replace("'", "",$Event_Captures[$j]["imgurl"]);
							$sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,Division_Code,Identification,Insert_Date_Time,Dateandtime) values( '".$sfCode."','" . $visitpath . "','".$Owndiv."','BillAmount','".$dateTime."','".$val["Enterdate"]."')";
							performQuery($sql);
						}
					}

			if($val['Remarks']==null)
              $Remarks='';
            else
            $Remarks=$vals['Remarks'];

		if($val['stayingtype']==null){
          $stayingtype='3';
		}else{
		$stayingtype=$val['stayingtype'];
		}
		if($val['Billamount']==null){
              $Billamount='0';
		}else{
		$Billamount=$val['Billamount'];
			}

if($Owndiv=="156"){
	 $sql ="exec Post_Expense_Tsr '$sfCode','".$divisionCode[0]."','".$val["Enterdate"]."','".$val["TotalAmount"]."','".$val["HQ"]."','".$val["Routename"]."','".$val["Toworkplace"]."','".$val["KM"]."','".$val["WorkType"]."' ,'".$val["mydayplanWorkPlace"]."','".$val["Remarks"]."','".$val["MOT"]."','".$Billamount."','".$stayingtype."','".$dailyEnteredExp."'";
}
else{
           $sql ="exec Post_Expense_Native '$sfCode','".$divisionCode[0]."','".$val["Enterdate"]."','".$val["TotalAmount"]."','".$val["HQ"]."','".$val["Routename"]."','".$val["Toworkplace"]."','".$val["KM"]."','".$val["WorkType"]."' ,'".$val["mydayplanWorkPlace"]."','".$val["Remarks"]."','".$val["MOT"]."','".$Billamount."','".$stayingtype."','".$dailyEnteredExp."'";
}
           $result=performQuery($sql);
           $resp["result"]= $result;
           $resp["msg"]= $result[0]['msg'];
           $resp["Query"]= $val['Billamount'];
           $resp["dailyEnteredExp"]= $dailyEnteredExp;
           $resp["success"] = true;
			echo json_encode($resp);

			if($resp["msg"] == 'Expense Submitted Successfully')
			{

				for ($i = 0; $i < count($vals); $i++)
				{
                    $sql = "insert into Trans_Daily_User_Expense(SF_Code,eDate,expCode,expName,Amt,Division_Code,LUpdtDate,Image_Url) select '" . $sfCode . "','". $val["Enterdate"]. "','" . $vals[$i]["ID"] . "','" . $vals[$i]["Name"]  . "','" . $vals[$i]["amt"] . "','" . $Owndiv . "',getDate(),'" . $vals[$i]["imgData"] . "'" ;
                    performQueryWP($sql,[]);
				}
				$sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
				$device = performQuery($sql);
                $reg_id = $device[0]['DeviceRegId'];
                if (!empty($reg_id)) {
					$sqlSfName = "select Sf_Name from Mas_Salesforce where Sf_Code='$sfCode'";
					$sfName = performQuery($sqlSfName);

					$sfNameStr = $sfName[0]['Sf_Name'];
                    $text = "Expense Applied by ". $sfNameStr;

					sendFCM_notify($reg_id, $text,'Expense Notification',0,'#ViewExpense');

				}
            }
            die;

            break;

			case "ChannelDCR":
				$tempp = array_keys($data[1]);
				$DCRCalls =$data[1][$tempp[0]];
				$Retailers_Call =$data[2][$tempp[0]];
				$divCode = $_GET['divisionCode'];
          $divisionCode = explode(",", $divCode);

		  $ChannelNameList=$vals["ChannelNameList"];
		  $TotalsCallsForm=$vals["TotalsCallsForm"];
		  $retailer_Orders=$vals["retailer_Orders"];
		   $loc = $vals["location"];
		  if($loc==null)
			  $loc = '';

			for ($i = 0; $i < count($ChannelNameList); $i++)
            {
			$sql = "SELECT isNull(max(SL_No),0)+1 as SL_No FROM ChannelDCR";
            $topr = performQuery($sql);
            $pk = (int) $topr[0]['SL_No'];
            $sql = "insert into ChannelDCR(SL_No,Sf_Code,Division_Code,ChannelId,ChannelName,ValuesC,DCR_Date,Ekey, location)
			select '" . $pk . "','".$sfCode."','" . $divisionCode[0] . "','" . $ChannelNameList[$i]["id"]  . "','" .$ChannelNameList[$i]["name"] . "','" .$ChannelNameList[$i]["calls"] . "',getDate(),'".$vals["EKEY"]."','" .$loc ."'";
			performQuery($sql);
            }

			for ($i = 0; $i < count($TotalsCallsForm); $i++)
            {
			$sql = "SELECT isNull(max(SL_No),0)+1 as SL_No FROM ChannelDCRCalls";
            $topr = performQuery($sql);
            $pk = (int) $topr[0]['SL_No'];
            $sql = "insert into ChannelDCRCalls(SL_No,Sf_Code,Division_Code,CallsId,CallslName,ValuesC,DCR_Date,Ekey, location)
			select '" . $pk . "','".$sfCode."','" . $divisionCode[0] . "','" . $TotalsCallsForm[$i]["id"]  . "','" .$TotalsCallsForm[$i]["name"] . "','" .$TotalsCallsForm[$i]["total"] . "',getDate(),'".$vals["EKEY"]."','" .$loc ."'";
			performQuery($sql);
            }


			for ($i = 0; $i < count($retailer_Orders); $i++)
            {

            $sql = "insert into Trans_Dcr_Retailers_Values(Sf_Code,Division_Code,RetailerId,RetailersName,Order_values,DcrDate,Ekey, location)
			select '".$sfCode."','" . $divisionCode[0] . "','" . $retailer_Orders[$i]["retailerid"]  . "','" .$retailer_Orders[$i]["name"] . "','" .$retailer_Orders[$i]["Values"] . "',getDate(),'".$vals["EKEY"]."','" .$loc ."'";
			performQuery($sql);
            }


			 $resp["msg"]= "";
			  $resp["sql"]=  $sql;
			 $resp["success"] = true;
            echo json_encode($resp);
			die;
			 break;
			case "SundayWorkingRequest":
			$sqlGetC="select count(Sf_Code) Cnt from Trans_SundayWorking where Sf_Code='".$sfCode."' and  convert(date,sundaydate)= convert(date,'".$vals["sundaydate"]."')";
			$CountCheck=  performQuery($sqlGetC);
			if($CountCheck[0]["Cnt"]==0){
			$sql = "insert into Trans_SundayWorking (Sf_Code,sundaydate,insert_date,Remarks,ActiveFlag) select '".$sfCode."','".$vals["sundaydate"]."',getDate(),'".$vals["remarks"]."','0'";
			performQuery($sql);
			$resp["msg"]= "";
			$resp["sql"]=  $sql;
			}else{
			$resp["msg"]= "Already Raised For The Date";
			}
			 $resp["success"] = true;
            echo json_encode($resp);
			die;

			break;

			case "Addnewroute":
			$sql = "select isNull(max(Territory_Code),0)+1 as RwID from Mas_Territory_Creation";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			 $sfCode = $_GET['sfCode'];
			 $divCode = $_GET['divisionCode'];
            $divisionCode = explode(",", $divCode);

			$sql = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Division_Code,SF_Code,Territory_Active_Flag,Created_date,Dist_Name,Target,Min_Prod,Population,Allowance_Type, Territory_SName)select '".$pk."','".$vals["routename"]."','".$divisionCode[0]."','".$vals["Sf_code"]."','0',getDate(),'".$vals["Stockist_code"]."','".$vals["Target"]."','".$vals["minprod"]."','".$vals["Populations"]."','".$vals["Allowance_Type"]. "','". $vals["Territory_SName"] ."'";


			performQuery($sql);
			$resp["msg"]= "Route Added Successfully";
			$resp["query"]= $sql;
			 $resp["success"] = true;
            echo json_encode($resp);
			die;

			break;
					case "ASMReport_Pri_Order":
					$sql = "SELECT isNull(max(id),0)+1 as RwID FROM ASM_Report_Template";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			 $sfCode = $_GET['sfCode'];
			 $divCode = $_GET['divisionCode'];
            $divisionCode = explode(",", $divCode);
			$ownDiv= $divisionCode[0];
				$sql="insert into ASM_Report_Template select '".$pk."','".$vals["worktype_code"]."','".$vals["worktype"]."','".$vals["town_code"]."','".$vals["town"]."'
,'".$vals["tc"]."','".$vals["ec"]."','".$vals["tsrfeed"]."','".$vals["marketfeed"]."','".$vals["dbfeed"]."','".$vals["visit"]."'
,'".$vals["closingStock"]."','".$vals["pendClaim"]."','$sfCode','$ownDiv',getdate()";
	performQuery($sql);
$details=$vals["pri_orders"];
for($i=0;$i<count($details);$i++){
	$det=$details[$i];
$sql="insert into ASM_Report_Pri_Order select '".$pk."','".$det["db"]."','".$det["town"]."','".$det["tsr"]."','".$det["hq"]."','".$det["value"]."'";
	performQuery($sql);
}
$resp["success"] = true;
            echo json_encode($resp);
die;
			break;
			case "AddnewDistributor":



			$sql = "SELECT CASE WHEN COUNT(Distributor_Code)>0 THEN MAX(Distributor_Code)+1 ELSE 0 END  RwID FROM mas_stockist";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			 $sfCode = $_GET['sfCode'];
			 $divCode = $_GET['divisionCode'];
            $divisionCode = explode(",", $divCode);

			if($vals["erp_code"]!=null)
				$erpCode =  $vals["erp_code"];
			else
				$erpCode =  '';

			$sql = "select Territory,Territory_Code,subdivision_code,Sf_HQ,State_Code,sf_Designation_Short_Name,Sf_Name from Mas_Salesforce where Sf_Code='".$vals["Sf_code"]."'";
            $salesforce = performQuery($sql);
			$sql = "INSERT INTO mas_stockist(Division_Code,Stockist_Code,SF_Code,Stockist_Name, Stockist_Address,Stockist_ContactPerson, Stockist_Designation, Stockist_Active_Flag, Stockist_Mobile, Territory, Created_Date,ERP_Code,Dist_Name,Username,Password,Territory_Code,Distributor_Code,subdivision_code,Norm_Val,Field_Name,Field_Code,State_Code,Head_Quaters,Type,gstn) select '".$divisionCode[0]."',  '".$pk."','".$pk."','".$vals["distributorname"]."','".$vals["Address"]."','".$vals["contactpname"]."','".$salesforce[0]["sf_Designation_Short_Name"]."','0','".$vals["mobilenumber"]."','".$salesforce[0]["Territory"]."',getdate(),'".$erpCode."','".$vals["distributorname"]."','".$vals["Username"]."','".$vals["Password"]."','".$salesforce[0]["Territory_Code"]."','".$pk."','".str_replace(",", "",$salesforce[0]["subdivision_code"])."','".$vals["normvalue"]."','".$salesforce[0]["Sf_Name"]."','".$vals["Sf_code"]."','".$salesforce[0]["State_Code"]."','".$salesforce[0]["Sf_HQ"]."','".$vals["type"]."','".$vals["gst"]."'";
			$QueryRes = performQuery($sql);
			$resp["msg"]= "Distributor Added Successfully";
			 $resp["success"] = true;
			 $resp["Query"] = $sql;
            echo json_encode($resp);
			die;

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
				if($data[0]['Tour_Plan']['worked_withname']==null)
              $ww_name="";
            else
            $ww_name=str_replace("'", "",$data[0]['Tour_Plan']['worked_withname']);;
		                    $RName=str_replace("'", "",$data[0]['Tour_Plan']['Multiretailername']);
$RCode=str_replace("'", "",$data[0]['Tour_Plan']['MultiretailerCode']);
			//$RName=$data[0]['Tour_Plan']['Multiretailername'];
			//$RCode=$data[0]['Tour_Plan']['MultiretailerCode'];
            $HQ_Name = str_replace(")", "", str_replace("(", "", $data[0]['Tour_Plan']['HQ_Name']));

			if($data[0]['Tour_Plan']['Place_Inv']==null)
              $Place_Inv='';
            else
            $Place_Inv=$data[0]['Tour_Plan']['Place_Inv'];

            $sql = "delete from Trans_TP_One WHERE SF_Code ='" . $sfCode . "' and Tour_Date=cast($tourDate as datetime)";
            performQuery($sql);
			$sql = "select YEAR($tourDate) yr";
			$year= performQuery($sql);
			$PYr=$year[0]['yr'];
			if(($divCode=="156" or $divCode=="156,")||($divCode=="207,"&&$PYr=="2022")){
				 $sql = "insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Tour_Schedule1,Objective,Worked_With_SF_Code,Division_Code,Territory_Code1,Worked_With_SF_Name,TP_Sf_Name,HQ_Code,HQ_Name,Confirmed,Change_Status,JointWork_Name,Retailer_Code,Retailer_Name,Place_Inv) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),getdate(),$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,$HQ_Code,$HQ_Name,3,1,$ww,'".$RCode."','".$RName."' ,''";

			}
			else{
            $sql = "insert into Trans_TP_One(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Tour_Schedule1,Objective,Worked_With_SF_Code,Division_Code,Territory_Code1,Worked_With_SF_Name,TP_Sf_Name,HQ_Code,HQ_Name,Confirmed,Change_Status,JointWork_Name,Retailer_Code,Retailer_Name,Place_Inv,Worktype_Name_B2) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),getdate(),$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,$HQ_Code,$HQ_Name,0,0,$ww,'".$RCode."','".$RName."' ,'','".$ww_name."'";
			}
		   performQuery($sql);
/* $myfile = fopen("../server/TP_NewEntry.txt", "a+");
        $txt = "Vasan\n";

        fwrite($myfile, $sql);
        fclose($myfile); */

$Trans_TP_One= $sql;

 $sql = "insert into Trans_TP_Hist(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Tour_Schedule1,Objective,Worked_With_SF_Code,Division_Code,Territory_Code1,Worked_With_SF_Name,TP_Sf_Name,HQ_Code,HQ_Name,Confirmed,Change_Status,JointWork_Name,Retailer_Code,Retailer_Name,Place_Inv) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),getdate(),$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,$HQ_Code,$HQ_Name,0,0,$ww,'".$RCode."','".$RName."' ,''";
            performQuery($sql);

            $resp["success"] = true;
           // $resp["REs"]=$sql;
$resp["Query"] = $Trans_TP_One;
            echo json_encode($resp);
            die;
            break;

			case "Tour_Plannew":
            $divCode = $_GET['divisionCode'];

            $divisionCode = explode(",", $divCode);
            $desig = $_GET['desig'];
            //$market = $data[0]['Tour_Plan']['market'];
            $objective = $data[0]['Tour_Plannew']['objective'];
            $tourDate = $data[0]['Tour_Plannew']['Tour_Date'];
            $worktype_code = $data[0]['Tour_Plannew']['worktype_code'];
            $worktype_name = $data[0]['Tour_Plannew']['worktype_name'];
            $worked_with_code = $data[0]['Tour_Plannew']['Worked_with_Code'];
            $worked_with_name = $data[0]['Tour_Plannew']['Worked_with_Name'];
            $RouteCode = $data[0]['Tour_Plannew']['RouteCode'];
            $RouteName = $data[0]['Tour_Plannew']['RouteName'];
            $sfName = $data[0]['Tour_Plannew']['sfName'];
            $HQ_Code = $data[0]['Tour_Plannew']['HQ_Code'];

         $ww=$data[0]['Tour_Plannew']['worked_with'];
		$RName=$data[0]['Tour_Plannew']['Multiretailername'];
		$RCode=$data[0]['Tour_Plannew']['MultiretailerCode'];
		$DName=$data[0]['Tour_Plannew']['Multidistiname'];
		$DCode=$data[0]['Tour_Plannew']['MultidistiCode'];
		$POB=$data[0]['Tour_Plannew']['POB'];
		$SOB=$data[0]['Tour_Plannew']['SOB'];
        $HQ_Name = str_replace(")", "", str_replace("(", "", $data[0]['Tour_Plannew']['HQ_Name']));


            $sql = "delete from Trans_TP_One WHERE SF_Code ='" . $sfCode . "' and Tour_Date=cast($tourDate as datetime)";
            performQuery($sql);
            $sql = "insert into Trans_TP_One(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Tour_Schedule1,Objective,Worked_With_SF_Code,Division_Code,Territory_Code1,Worked_With_SF_Name,TP_Sf_Name,HQ_Code,HQ_Name,Confirmed,Change_Status,JointWork_Name,Retailer_Code,Retailer_Name,Distributor_Name,Distributor_Code,TPOB,TSOB) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),getdate(),$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,$HQ_Code,$HQ_Name,0,0,$ww,$RCode,$RName,$DName,$DCode,$POB,$SOB";
            performQuery($sql);

        /* $myfile = fopen("../server/TP_Entry.txt", "a+");
        $txt = "John Doe\n";

        fwrite($myfile, $sql);
        fclose($myfile); */

 $sql = "insert into Trans_TP_Hist(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Tour_Schedule1,Objective,Worked_With_SF_Code,Division_Code,Territory_Code1,Worked_With_SF_Name,TP_Sf_Name,HQ_Code,HQ_Name,Confirmed,Change_Status,JointWork_Name,Retailer_Code,Retailer_Name,Distributor_Name,Distributor_Code,TPOB,TSOB) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),getdate(),$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,$HQ_Code,$HQ_Name,0,0,$ww,$RCode,$RName ,$DName,$DCode,$POB,$SOB";
            performQuery($sql);

            $resp["success"] = true;
           // $resp["REs"]=$sql;

            echo json_encode($resp);
            die;
            break;




        case "TourPlanSubmit":
            $month = $_GET['month'];
            $year = $_GET['year'];
			$sfCode = $_GET['sfCode'];
			$div = $_GET['divisionCode'];
			$divs = explode(",", $div . ",");
			$Owndiv = (string) $divs[0];
            $sql = "update Trans_TP_One set Change_Status=1,Confirmed=1 where Tour_Month=$month and Tour_Year=$year and Sf_Code='$sfCode'";
            performQuery($sql);

			$sql = "delete from TP_Status where SF_Code='".$sfCode."' and Division_Code='".$Owndiv."' and Tour_Month='".$month."' and Tour_Year='".$year."'";
			performQuery($sql);
			$sql = "SELECT isNull(max(SLNo),0)+1 as RwID FROM TP_Status";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			 $sql = "insert into TP_Status select '".$pk."' ,'".$sfCode."','".$Owndiv."','".$month."','".$year."','1'";
			 performQuery($sql);

             $resp["success"] = true;


            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
            $sqlSfName = "select Sf_Name from Mas_Salesforce where Sf_Code='$sfCode'";

                  $device = performQuery($sql);
                  $sqlSfName = performQuery($sqlSfName);
                  $reg_id = $device[0]['DeviceRegId'];
                    if (!empty($reg_id)) {
                        $msg = "TP Applied by ". $sqlSfName[0]['Sf_Name'];

                    sendFCM_notify($reg_id, $msg,'TP Notification',0,'#ViewTP');
            }

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
            $sql = "insert into Trans_TP(Division_Code,SF_Code,Worked_With_SF_Code,Worked_With_SF_Name,Tour_Date,Tour_Month,Tour_Year,WorkType_Code_B,Worktype_Name_B,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Objective,Confirmed,Confirmed_Date,Rejection_Reason,Territory_Code1,Territory_Code2,Territory_Code3,TP_Sf_Name,TP_Approval_MGR,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Submission_date,Change_Status,Retailer_Code,Retailer_Name,Distributor_Name,Distributor_Code,HQ_Code,HQ_Name,JointWork_Name)
             select Division_Code,SF_Code,Worked_With_SF_Code,Worked_With_SF_Name,Tour_Date,Tour_Month,Tour_Year,WorkType_Code_B,Worktype_Name_B,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Objective,'3',GETDATE(),Rejection_Reason,Territory_Code1,Territory_Code2,Territory_Code3,TP_Sf_Name,TP_Approval_MGR,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Submission_date,Change_Status,Retailer_Code,Retailer_Name,Distributor_Name,Distributor_Code,HQ_Code,HQ_Name,JointWork_Name
                            from Trans_TP_One where sf_Code='$code' and Tour_Month=$month and Tour_Year=$year";
            $trs = performQuery($sql);
			$resp["sqlInsert"] = $sql;

			$sql = "update TP_Status set Tp_Status=3 where Tour_Month=$month and Tour_Year=$year and SF_Code='$code'";
            performQuery($sql);
			$resp["sqlUpdate"] = $sql;
            if (count($trs) > 0) {
                $sql = "delete from Trans_TP_One where sf_Code='$code' and Tour_Month=$month and Tour_Year=$year";
                performQuery($sql);
            }
            $resp["success"] = true;
            echo json_encode($resp);



			$sql = "SELECT DeviceRegId FROM Access_Table where sf_code='$code'";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                    $msg = "TP Approved on ". $month . "-". $year;

                sendFCM_notify($reg_id, $msg,'TP Notification',0,'#Approve');
            }



            die;
            break;
        case "TPReject":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $code = $_GET['code'];
            $sql1 = "insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Reject_date,Division_Code,Rejection_Reason) select '" . $code . "',$month,$year,'" . date('Y-m-d H:i') . "',$Owndiv," . $vals['reason'] . "";
            performQuery($sql1);

            $sql2 = "update Trans_TP_One set Change_Status=2,Confirmed=0,Rejection_Reason=" . $vals['reason'] . " where Tour_Month=$month and Tour_Year=$year and Sf_Code='$code'";
            performQuery($sql2);

			$sql3 = "update TP_Status set Tp_Status=2 where Tour_Month=$month and Tour_Year=$year and SF_Code='$code'";
            performQuery($sql3);

            $resp["success"] = true;
            $resp["sql1"] = $sql1;
            $resp["sql2"] = $sql2;
            $resp["sql3"] = $sql3;
            echo json_encode($resp);


			$sql = "SELECT DeviceRegId FROM Access_Table where sf_code='$code'";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                    $msg = "TP Rejected for ". $month . "-". $year;

                sendFCM_notify($reg_id, $msg,'TP Notification',0,'#Reject');
            }


            die;
            break;
        case "LeaveApproval":
              $leaveid = $_GET['leaveid'];
              $Mrsfcode = $_GET['sfCode'];
            $sql = "update Mas_Leave_Form set Leave_Active_Flag=0,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Leave_Id=$leaveid";
            performQuery($sql);
  $sql = "select sf_code from Mas_Leave_Form where Leave_Id=$leaveid";
            $sf=performQuery($sql);
$vals['Sf_Code']= $sf[0]['sf_code'];
             $divCode = $_GET['divisionCode'];
            $sql = "SELECT sf_type FROM Mas_Salesforce where Sf_Code='" . $vals['Sf_Code'] . "'";

            $sfType = performQuery($sql);
            $days = $vals['No_of_Days'];
            $date = preg_split("[/]", $vals['From_Date']);
            $date = $date[2] . "-" . $date[1] . "-" . $date[0];
            for ($i = 1; $i <= $days; $i++) {
                $query = "exec ChkandPostLeaveDt 0,'" . $vals['Sf_Code'] . "'," . $sfType[0]['sf_type'] . ",$Owndiv,'$date','','apps'";
                $results = performQuery($query);
                $date = date('Y-m-d', strtotime($date . ' + 1 days'));
            }

      $sqll = "select Leave_Type from Mas_Leave_Form where  Leave_Id= '$leaveid'  and Sf_Code='" . $vals['Sf_Code'] ."'";
      $LeaveType =performQuery($sqll);
    $query = "update MasEntitlement set LeaveAvailability=LeaveAvailability-'" . $days . "',LeaveTaken=LeaveTaken+'" . $days . "' where  SFCode='" . $vals['Sf_Code'] . "' and  DivisionCode='". str_replace(",", "",$divCode)."'  and LeaveCode=" . $LeaveType[0]['Leave_Type']. "";

      performQuery($query);

			$salesPersonSfcode = $vals['Sf_Code'];

			$sql = "SELECT DeviceRegId FROM Access_Table where sf_code='$salesPersonSfcode'";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                    $msg = "Leave Application Approved";
                //send_gcm_notify($reg_id, $msg,"Leave Application");
                sendFCM_notify($reg_id, $msg,'Leave Notification',0,'#ViewLeave');
            }

            break;


			 case "GiftApproval":
			 $Mrsfcode = $_GET['sfCode'];
			 $Sl_No = $_GET['Sl_No'];
			 $query = "update Trans_Gift_Claim set Aproval_Flag='" . $vals['Aproval_Flag'] ."', Approved_Date='" .date('Y-m-d H:i:s'). "',Approvedby = '" . $Mrsfcode . "',Reject_reason='" . $vals['reason'] ."' where  Sf_Code='" . $vals['FSf_Code'] . "'   and Sl_No='" . $Sl_No. "' ";

           performQuery($query);

			 break;


        case "LeaveReject":
            $leaveid = $_GET['leaveid'];
           $Mrsfcode = $_GET['sfCode'];
            $sql = "update Mas_Leave_Form set Leave_Active_Flag=1,Rejected_Reason=" . $vals['reason'] . "  ,LastUpdt_Date='" .date('Y-m-d H:i:s'). "',Approved_to='" . $Mrsfcode . "' where Leave_Id=$leaveid ";
            performQuery($sql);

			$salesPersonSfcode = $vals['Sf_Code'];

			$sql = "SELECT DeviceRegId FROM Access_Table where sf_code='$salesPersonSfcode'";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                    $msg = "Leave Application Rejected";
                //send_gcm_notify($reg_id, $msg,"Leave Application");
                sendFCM_notify($reg_id, $msg,'Leave Notification',0,'#ViewLeave');
            }


            break;
case "Vansalesreject":


			 for($il=0;$il<count($vals["InvProducts"]);$il++)
                {
                    $PDets=$vals["InvProducts"][$il];
                    $updateAprlQuery = "update Trans_Prod_DailyInventoryNative set ApproveFlag = '2' where SLNo ='". $PDets["id"] . "'";
                    performQuery($updateAprlQuery);
                }

 			 $ApprvSFCode = $_GET['ApprvSFCode'];
			 
			 
			 $deleterejectMsgSql = "delete Trans_VanReject where Sf_Code = '" . $ApprvSFCode . "' and cast(Reject_Date as date) = '" .date('Y-m-d'). "'";
			 performQuery($deleterejectMsgSql);

			
			 $rejectMsgSql = "insert into Trans_VanReject select '" . $ApprvSFCode . "' ,'" .date('Y-m-d'). "','" . $vals['Reason'] . "'";
			 performQuery($rejectMsgSql);


			
			if($ApprvSFCode !=null && $ApprvSFCode != ""){
								
				$sql = "SELECT DeviceRegId FROM Access_Table where Sf_Code='" . $ApprvSFCode . "'";
                $device = performQuery($sql);
                $reg_id = $device[0]['DeviceRegId'];
					if (!empty($reg_id)) 
						{
						$msg = "Daily Inventory Rejected";
						sendFCM_notify($reg_id, $msg,'Daily Inventory',0,'#van-sales');
						}
			}


			$resp['Msg'] = $updateAprlQuery;
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;


 case "LeaveFormValidate":
            $leaveid = $_GET['leaveid'];
			$div = $_GET['divisionCode'];
			if($div=="156,"){
				$sql="select * from mas_Leave_Type  where DedTyp=1 and Leave_code=" . $vals['Leave_Type'] . "";
				$tr= performQuery($sql);
				if(count($tr)>0){
					$resp['Msg'] = "";
					$resp["success"] = true;
					echo json_encode($resp);
					die;
				}
			}
            $query = "exec iOS_getLvlValidate '$sfCode','" . str_replace("'", "",$vals['From_Date']) . "'," . $vals['To_Date'] . "," . $vals['Leave_Type'] . "";
            $results = performQuery($query);
			$resp['Msg'] = $results[0]['Msg'];
			$resp["success"] = true;
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
		$sqlm = "insert into tbMyDayPlan (sf_code,Pln_Date,remarks,Division_Code,ClstrName,wtype,FWFlg)  select '" . $sfCode . "','".$vals['misseddate']."','".$Remarks."','" . $Owndiv . "','Missed Entry','" . $Worktype . "','" . $WFlg . "'";
		performQuery($sqlm);

		$sql = "exec  svDCRMain_App '" . $sfCode . "','".$vals['misseddate']."','" . $Worktype . "','','".$Owndiv."','".$Remarks."',''";
		performQuery($sql);
		$resp["success"] = true;
		$resp["sqlm"] = $sqlm;
		echo json_encode($resp);
		die;
		break;
        case "LeaveForm":
             $name = $_GET['sf_name'];
            $divCode = $_GET['divisionCode'];
            $sql = "SELECT LeaveAppproval FROM Access_Master where division_code= '". str_replace(",", "",$divCode)."'";
            $LA = performQuery($sql);
            $leaveType= $LA[0]['LeaveAppproval'];
            $lvappp=2;
            if($leaveType==1) $lvappp=0;

			if($vals['halfday']==null){
				$halfdayFlag="";
				$halfday="''";
			}
			else{
				$halfdayFlag=0;
				$halfday=$vals['halfday'];
			}
            $sql = "SELECT isNull(max(Leave_Id),0)+1 as RwID FROM Mas_Leave_Form";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
			$sql = "insert into Mas_Leave_Form(Leave_Id,Leave_Type,From_Date,To_Date,Reason,sf_code,Division_Code,Leave_Active_Flag,Created_Date,No_of_Days,Address,First_or_Second) select '$pk'," . $vals['Leave_Type'] . ",'" . str_replace("'", "",$vals['From_Date']) . "'," . $vals['To_Date'] . "," . $vals['Reason'] . ",'$sfCode','$Owndiv','".$lvappp."','" . date('Y-m-d H:i:s') . "'," . $vals['No_of_Days'] . "," . $vals['address'] . ",".$halfday."";
            performQuery($sql);
			$resp["qry"]=$sql;
            if($leaveType==1){
                $sql = "SELECT sf_type FROM Mas_Salesforce where Sf_Code='$sfCode'";

                $sfType = performQuery($sql);
                $days = $vals['No_of_Days'];
                $date = str_replace("'", "",$vals['From_Date']);
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
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                if($leaveType==1)
                    $msg = "Leave Application Received";
                else
                    $msg = "Leave Application Received For Approval";
                //send_gcm_notify($reg_id, $msg,"Leave Application");
                sendFCM_notify($reg_id, $msg,'Leave Notification',0,'#ViewLeave');
            }

            $resp["success"] = true;
			$resp["Interval"] = dateDiffInDays("2020-07-01","2020-07-02");
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


			case "primaryOrderReturn":
            $sql = "SELECT isNull(max(Trans_SlNo),0)+1 as RwID FROM SalesStockistReturn_Head";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
            $sql = "insert into SalesStockistReturn_Head select ".$pk.",".$pk.",'" . $sfCode . "','" . $vals["StkId"] . "','" . $vals["DrId"] . "','" . date('Y-m-d H:i:s') . "','0',0";
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


                $sql = "insert into SalesStockistReturn_Detail
 select ".$pk.",m.Product_Detail_Code," . $PDets["Qty"] . "," .
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
                $sql = "exec UpdStockistRetReturnStock '".$pk."','" . $vals["StkId"] . "','" . $PDets["id"] . "','" . date('Y-m-d H:i:s') . "','".$PDets["QType"]."'," . $Qtys . ",".$Divc.",'',''";

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
                $sql = "insert into Customer_Stockist_Ledger_Details select '" . $vals["DrId"] . "',".$tVal.",'C',getDate(),'Return',''";
                performQuery($sql);
            }

            break;

        case "DailyInventory":
            $mods=$vals["svMode"];
			$CrryForword=$vals["CrryForword"];
            $tblnm="Trans_Prod_DailyInventory";
			$SFCd=$vals["SF"];
			if($vals["SF"]=="" || $vals["SF"]==null)
				$SFCd=$vals["sfCode"];
            if($mods=="26"){
                $tblnm="Trans_Prod_DailyEndInventory";


				$query = "select * from Trans_VanStock_Ledger where from_loc='" . $SFCd . "'  and convert(date,eDate)='" . date('Y-m-d 00:00:00') . "'    and EntryBy='DayEndInv'";
        $DEI = performQuery($query);

        /*if (count($DEI) > 0) {

		   $result = array();
            $result['Msg'] = 'Daily End Inventory Already Submitted';
            $result["success"] = true;
            outputJSON($result);
            die;


		}*/

            }

            $SFCd=$vals["SF"];


if($mods!="26"){

            $sql = "delete FROM ". $tblnm ." where SFCode='" . $SFCd . "' and EntryDt='" . date('Y-m-d 00:00:00') . "'";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
}



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
                            $PDets["PQty"] . ",Sample_Erp_Code,isnull(RetailCasePrice,0)RCP,isnull(Retailor_Price,0) RPP,isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0) DPP,'" . $vals["AprlFlag"] . "','" . $vals["StkId"] . "','" . $vals["StkNm"] . "'".
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
                if($mods=="26" ){
                    $sql = "select (" . $PDets["Qty"] . "*isnull(Sample_Erp_Code,1))+" . $PDets["PQty"] . " PicQty from Mas_Product_Detail m Where m.Product_Detail_Code='" . $PDets["id"] . "'";
                    $ar = performQuery($sql);
                    $Qtys=$ar[0]["PicQty"];
                   $Caltypfl=0;
                    $sql = "exec UpdVanCLStock '".$pk."','" . $SFCd . "','F','" . $vals["StkId"] . "','D','" . date('Y-m-d 00:00:00') . "','" . $PDets["id"] . "','G'," . $Qtys . ",".$Divc.",'" . $vals["StkNm"] . "','','DayEndInv',".$Caltypfl."";
                    performQueryWP($sql,[]);
                }




            }
            if($mods=="24"){
                $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='" . $SFCd . "')";
                $device = performQuery($sql);
                $reg_id = $device[0]['DeviceRegId'];
                if (!empty($reg_id)) {
                    $msg = "Daily Inventory Approval Received";
                    //send_gcm_notify($reg_id, $msg,"Daily Inventory");
					sendFCM_notify($reg_id, $msg,'Daily Inventory',0,'#sign-in');
                }
            }
            if($mods=="27"){
                $sql = "SELECT DeviceRegId FROM Access_Table where sf_code='" . $SFCd . "'";
                $device = performQuery($sql);
                $reg_id = $device[0]['DeviceRegId'];
                if (!empty($reg_id)) {
                    //   $msg = $name . " Applied Leave for " . $vals['No_of_Days'] . " days";
                    $msg = "Your Daily Inventory Approved...";
                   // send_gcm_notify($reg_id, $msg,"Daily Inventory");

					sendFCM_notify($reg_id, $msg,'Daily Inventory',0,'#sign-in');
                }
            }

			if($CrryForword==2){
				$tblnm="Trans_Prod_DailyEndInventory";
				$SFCd=$vals["SF"];

 /* $myfile = fopen("../server/vanstock.txt", "a+");
        $txt = "John Doe\n";
        $sqlsp=$vals["InvProducts"]." message:".$SFCd."sf_code=".$SFCd."\n";
        fwrite($myfile, $sqlsp);
        fclose($myfile); */

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
				 if($PDets["COP"]=="")
					 $PDets["COP"]=0;
                if($vals["AprlFlag"]=="" || $vals["AprlFlag"]==null) $vals["AprlFlag"]=0;


          $sql = "insert into ". $tblnm ." select ".$pk.",'" . $SFCd . "','" . date('Y-m-d 00:00:00',strtotime("-1 days")) . "',m.Product_Detail_Code,'0'," . $PDets["COP"] . ",Sample_Erp_Code,isnull(RetailCasePrice,0)RCP,isnull(Retailor_Price,0) RPP,isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0) DPP,'" . $vals["AprlFlag"] . "','" . $vals["StkId"] . "','" . $vals["StkNm"] . "'".
                            "," . $PDets["Totalqty"] . " from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code ".
                            " and v.State_Code=".$SFState." Where m.Product_Detail_Code='" . $PDets["id"] . "'";
                performQuery($sql);



                    $sql = "select (" . $PDets["Qty"] . "*isnull(Sample_Erp_Code,1))+" . $PDets["PQty"] . " PicQty from Mas_Product_Detail m Where m.Product_Detail_Code='" . $PDets["id"] . "'";
                    $ar = performQuery($sql);
                    $Qtys=$ar[0]["PicQty"];
                  $Caltypfl=2;
            $sql = "exec UpdVanCLStock '".$pk."','" . $SFCd . "','F','" . $vals["StkId"] . "','D','" . date('Y-m-d 00:00:00',strtotime("-1 days")) . "','" . $PDets["id"] . "','G'," . $PDets["COP"] . ",".$Divc.",'" . $vals["StkNm"] . "','','Carry Forword',".$Caltypfl."";
                    performQueryWP($sql,[]);






            }

			}


            break;

case "PendingPaymentDetailsNative":
                         $SfCode = $_GET['sfCode'];
						 $SfName=$_GET['sfName'];
						 $div = $_GET['divisionCode'];
                         $divs = explode(",", $div . ",");
                         $Owndiv = (string) $divs[0];
                          $Stk_Code=$vals["Stockist_Id"];
                          $Stk_Name=$vals["Stockist_Name"];
                          $Cust_ID=$vals["Cust_ID"];
						  $Cust_Name=$vals["Cust_Name"];
						  $Total_amount=$vals["Total_amount"];
						  $Mode=$vals["Mode"];
						  $Reference_No=$vals["Reference_No"];
						  $Bk_name=$vals["Bk_name"];
						  $Pay_Date=$vals["Pay_Date"];
						  $collect_by=$vals["collect_by"];
						  $Remark=$vals["Remark"];
						  $Advance_pay=$vals["Advance_pay"];
						  $invoice_no=$vals["invoice_no"];
						  $Route_code=$vals["Route_code"];
						  
						   $sql = "SELECT ISNULL(MAX(Sl_No),0)+1 as slNo FROM trans_payment_detail ";
                            $tRw = performQuery($sql);
                            $pk = (int) $tRw[0]['slNo'];
							
							  $sqlQry = "insert into trans_payment_detail(Sl_No,Sf_Code,Sf_Name,Cust_Id,Cus_Name,Amount,Pay_Mode,Pay_Date,Pay_Ref_No,Remarks,Distributor_Code,Route_code,eDate,PaymentName,advance_pay,div_code,invoice_no,Collected_by) Values('".$pk."','".$SfCode."','".$SfName."','".$Cust_ID."','".$Cust_Name."','".$Total_amount."','".$Mode."','". date('m-d-Y H:i:s') . "','".$Reference_No."','".$Remark."','".$Stk_Code."','".$Route_code."','".$Pay_Date."','".$Bk_name."','".$Advance_pay."','".$Owndiv."','".$invoice_no."','".$collect_by."')";
									
									performQuery($sqlQry);
								
                             $PPXML = "<ROOT>";
                            for($il=0;$il<count($vals["Payment_Details"]);$il++)
                            {
                              $PDets=$vals["Payment_Details"][$il];
							  
							   $Newsql = "insert into trans_payment_detail_view(Sl_No,bill_no,bill_date,bill_amt,Pen_amt,paid_amt) Values('".$pk."','" . $PDets["bill_no"] . "','". date('Y-m-d H:i:s') . "','" . $PDets["bill_amt"] . "','" . $PDets["Pen_amt"] . "','" . $PDets["paid_amt"] ."')";
							   
							   
							  performQuery($Newsql);

 $PPXML = $PPXML . "<Bill Sf_Code=\"".$Stk_Code."\" Cust_Code=\"".$Cust_ID."\" Received_amt=\"".$PDets["paid_amt"]."\" Pay_Date=\"".date('Y-m-d H:i:s')."\" Bill_No=\"".$PDets["bill_no"]."\" />";
                              $Upsql= "exec Update_Pending_bill '" .$PPXML. "', '".$Stk_Code."' ,'".$Cust_ID."','".$Total_amount."','".$Advance_pay."','" .$Type. "'";
performQuery($Upsql);
							}
                                          $PPXML = $PPXML . "</ROOT>";
							
							 $result = array();
							 $result['sql'] = $sql;
                             $result['sqlQry'] = $sqlQry;
                             $result['Newsql'] = $Newsql;
                              $result['Upsql'] = $Upsql;
                             $result["success"] = true;
                             outputJSON($result);
							die;
                        break;




 case "DailyInventoryLoadingNative":
                            $SFCd=$vals["SF"];
                            if($vals["SF"]==null || $vals["SF"]=="")
                                $SFCd=$vals["sfCode"];
                            else
                                $SFCd=$vals["SF"];

							if($vals["AprlFlag"]==null || $vals["AprlFlag"]=="")
								$vals["AprlFlag"]=0;

							if($vals["isAlreadyRejected"]==null || $vals["isAlreadyRejected"]=="")
								$vals["isAlreadyRejected"]=0;

                            for($il=0;$il<count($vals["InvProducts"]);$il++)
                            {

                            $PDets=$vals["InvProducts"][$il];

                            $sql = "SELECT Division_Code FROM Mas_Product_Detail where Product_Detail_Code='" . $PDets["id"] . "'";
                            $tRw = performQuery($sql);
                            $Divc = (int) $tRw[0]['Division_Code'];

                            $sql = "SELECT isNull(max(SLNo),0)+1 as RwID FROM Trans_Prod_DailyInventoryNative";
                            $tRw = performQuery($sql);
                            $pk = (int) $tRw[0]['RwID'];

                            if($PDets["Totalqty"]=="") $PDets["Totalqty"]=0;
                            if($PDets["Qty"]=="") $PDets["Qty"]=0;
                            if($PDets["PQty"]=="") $PDets["PQty"]=0;

                            if($vals["isAlreadyRejected"]==1)
                            {
                                $deleteRejectedQuery = "delete Trans_Prod_DailyInventoryNative where SLNo ='". $PDets["loadingStockId"] . "' and ApproveFlag ='2'";
                                performQuery($deleteRejectedQuery);
                            }
                            if($vals["isAlreadyRejected"]==2)
                            {
                                $deleteRejectedQuerys = "delete Trans_Prod_DailyInventoryNative where SLNo ='". $PDets["loadingStockId"] . "' and ApproveFlag ='3' and SFCode='". $SFCd ."' and cast(EntryDt as date)='". date('Y-m-d') ."'";
                                performQuery($deleteRejectedQuerys);
                            }


                            if($vals["AprlFlag"] ==0  && $Divc != 4){
                            $dailyInvNewsql = "insert into Trans_Prod_DailyInventoryNative (SLNo, SFCode, EntryDt, Prod_Code, CaseQty, PiceQty, ConvQty, RateC_Retail, RateP_Retail, RateC_Dist, RateP_Dist, ApproveFlag, StkId, StkNm, Totalqty, TotalCase, TotalPcs, CarryFwdCase, CarryFwdPcs, Div_Id, Max_Time) select '" . $pk ."','". $SFCd ."','". date('Y-m-d 00:00:00') ."','". $PDets["id"] . "','" . $PDets["opening_case_qty"] . "','" . $PDets["opening_piece_qty"]. "','" .$PDets["conversionQty"] . "',isnull(RetailCasePrice,0), isnull(Retailor_Price,0),isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0), 0,'". $vals["StkId"] . "','" . $vals["StkNm"] . "','" . $PDets["Totalqty"] . "','" . $PDets["total_case_qty"] . "','" .$PDets["total_piece_qty"]. "','" . $PDets["carry_fwd_case_qty"] . "','" .$PDets["carry_fwd_piece_qty"].  "','" . $PDets["Division_Code"] .  "', '". date('Y-m-d H:i:s') . "' from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code and v.State_Code=". $SFState." Where m.Product_Detail_Code='". $PDets["id"] . "'";
                            performQuery($dailyInvNewsql);

                            //$dailyInvsql = "insert into Trans_Prod_DailyInventory (SLNo, SFCode, EntryDt, Prod_Code, CaseQty, PiceQty, ConvQty, RateC_Retail, RateP_Retail, RateC_Dist, RateP_Dist, ApproveFlag, StkId, StkNm, Totalqty) select '" . $pk ."','". $SFCd ."','". date('Y-m-d 00:00:00') ."','". $PDets["id"] . "','" . $PDets["opening_case_qty"] . "','" . $PDets["opening_piece_qty"]. "','" .$PDets["conversionQty"] . "',isnull(RetailCasePrice,0), isnull(Retailor_Price,0),isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0), 0,'". $vals["StkId"] . "','" . $vals["StkNm"] . "','" . $PDets["Totalqty"] . "' from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code and v.State_Code=". $SFState." Where m.Product_Detail_Code='". $PDets["id"] . "'";
                            //performQuery($dailyInvsql);

                            }else{
                            $sql = "SELECT isNull(max(cast(Legr_ID as numeric)),0)+1 as MaxRwID FROM Trans_VanStock_Ledger";
                            $tRww = performQuery($sql);
                            $MaxRwID = (int) $tRww[0]['MaxRwID'];

							if($Divc != 4){
                                    $updateAprlQuery = "update Trans_Prod_DailyInventoryNative set ApproveFlag = '1' where SLNo ='". $PDets["loadingStockId"] . "'";
                                    performQuery($updateAprlQuery);
                            }else{
                                        $dailyInvNewsql = "insert into Trans_Prod_DailyInventoryNative (SLNo, SFCode, EntryDt, Prod_Code, CaseQty, PiceQty, ConvQty, RateC_Retail, RateP_Retail, RateC_Dist, RateP_Dist, ApproveFlag, StkId, StkNm, Totalqty, TotalCase, TotalPcs, CarryFwdCase, CarryFwdPcs, Div_Id, Max_Time) select '" . $pk ."','". $SFCd ."','". date('Y-m-d 00:00:00') ."','". $PDets["id"] . "','" . $PDets["opening_case_qty"] . "','" . $PDets["opening_piece_qty"]. "','" .$PDets["conversionQty"] . "',isnull(RetailCasePrice,0), isnull(Retailor_Price,0),isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0), 1,'". $vals["StkId"] . "','" . $vals["StkNm"] . "','" . $PDets["Totalqty"] . "','" . $PDets["total_case_qty"] . "','" .$PDets["total_piece_qty"]. "','" . $PDets["carry_fwd_case_qty"] . "','" .$PDets["carry_fwd_piece_qty"].  "','" . $PDets["Division_Code"] .  "', '". date('Y-m-d H:i:s') . "'
                                        from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code and v.State_Code=". $SFState." Where m.Product_Detail_Code='". $PDets["id"] . "'";
                                        performQuery($dailyInvNewsql);

                            }

                            $Ledgersql = "insert into Trans_VanStock_Ledger (Legr_ID,from_loc,fLocTyp,To_Loc,tLocTyp,eDate,PCode,GQty,Division_Code,EntryBy,CalcTyp,TNm) select '" . $MaxRwID ."','". $vals["StkId"] ."','D','". $SFCd ."','F','" . date('Y-m-d H:i:s') . "','" . $PDets["id"] . "','" .$PDets["g_qty"]. "','". $Divc . "','BeginInv','1','". $vals["StkNm"] ."'" ;
                            performQuery($Ledgersql);

                            $sql = "SELECT isNull(max(cast(BatchSlNo as numeric)),0)+1 as BatchSlNo FROM Trans_VanCurrStock";
                            $tRww = performQuery($sql);
                            $MaxRwID = (int) $tRww[0]['BatchSlNo'];

                            $vanCurrsql = "insert into Trans_VanCurrStock (SF_Code,PCode,Batch_No,GQty,DQty,Division_Code,BatchSlNo) select '" . $SFCd ."','". $PDets["id"] . "','','" . $PDets["g_qty"] . "','0','".$Divc. "','". $MaxRwID."'";
                            performQuery($vanCurrsql);
							
							
                            }

                            }
							
							if($vals["AprlFlag"] ==0  && $Divc != 4){
								
							
							$sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='" . $SFCd . "')";
                            $device = performQuery($sql);
                            $reg_id = $device[0]['DeviceRegId'];
                            if (!empty($reg_id)) {
                            $msg = "Daily Inventory Received";
                            //send_gcm_notify($reg_id, $msg,"Daily Inventory");
                            sendFCM_notify($reg_id, $msg,'Daily Inventory',0,'#van-sales');
                            }
								
							}else{
								$ApprvSFCode = $_GET['ApprvSFCode'];
							if($ApprvSFCode !=null && $ApprvSFCode != ""){
								
							$sql = "SELECT DeviceRegId FROM Access_Table where Sf_Code='" . $ApprvSFCode . "'";
                            $device = performQuery($sql);
                            $reg_id = $device[0]['DeviceRegId'];
								if (!empty($reg_id)) {
									$msg = "Daily Inventory Approved";
									//send_gcm_notify($reg_id, $msg,"Daily Inventory");
									sendFCM_notify($reg_id, $msg,'Daily Inventory',0,'#van-sales');
								}
							}
							
							}
							
							
							
							
                            

                            $result = array();
                            $result['$dailyInvNewsql'] = $dailyInvNewsql;
                            $result['dailyInvsql'] = $dailyInvsql;
                            $result['Ledgersql'] = $Ledgersql;
                            $result['vanCurrsql'] = $vanCurrsql;
                            $result['ApprvSFCodel'] = $ApprvSFCode;
                            $result['reg_id'] = $reg_id;
                            $result["success"] = true;
                            outputJSON($result);
							die;
                        break;

 case "DailyInventoryUNLoadingNative":
                            $SFCd=$vals["SF"];
                            if($vals["SF"]==null || $vals["SF"]=="")
                                $SFCd=$vals["sfCode"];
                            else
                                $SFCd=$vals["SF"];

                            if(count($vals["InvProducts"]) == 0){
                                $updateLatestTime = "update Trans_Prod_DailyEndInventoryNative set Max_Time = '". date('Y-m-d H:i:s') . "' where cast(EntryDt as date) = '". date('Y-m-d') . "'";
                                $updateLatestTimeQuery = performQuery($updateLatestTime);

                            }else{

                            for($il=0;$il<count($vals["InvProducts"]);$il++)
                                                        {

                                                        $PDets=$vals["InvProducts"][$il];

                                                        $sql = "SELECT Division_Code FROM Mas_Product_Detail where Product_Detail_Code='" . $PDets["id"] . "'";
                                                        $tRw = performQuery($sql);
                                                        $Divc = (int) $tRw[0]['Division_Code'];

                                                        $sql = "SELECT isNull(max(SLNo),0)+1 as RwID FROM Trans_Prod_DailyEndInventoryNative";
                                                        $tRw = performQuery($sql);
                                                        $pk = (int) $tRw[0]['RwID'];

                                                        if($PDets["Totalqty"]=="") $PDets["Totalqty"]=0;
                                                        if($PDets["Qty"]=="") $PDets["Qty"]=0;
                                                        if($PDets["PQty"]=="") $PDets["PQty"]=0;

                                                        $dailyInvNewsql = "insert into Trans_Prod_DailyEndInventoryNative (SLNo, SFCode, EntryDt, Prod_Code, CaseQty, PiceQty, ConvQty, RateC_Retail, RateP_Retail, RateC_Dist, RateP_Dist, ApproveFlag, StkId, StkNm, Totalqty, TotalCase, TotalPcs, CarryFwdCase, CarryFwdPcs, Div_Id, Max_Time ) select '" . $pk ."','". $SFCd ."','". date('Y-m-d 00:00:00') ."','". $PDets["id"] . "','" . $PDets["opening_case_qty"] . "','" . $PDets["opening_piece_qty"]. "','" .$PDets["conversionQty"] . "',isnull(RetailCasePrice,0), isnull(Retailor_Price,0),isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0), 0,'". $vals["StkId"] . "','" . $vals["StkNm"] . "','" . $PDets["Totalqty"] . "','" . $PDets["total_case_qty"] . "','" .$PDets["total_piece_qty"]. "','" . $PDets["carry_fwd_case_qty"] . "','" .$PDets["carry_fwd_piece_qty"].  "','" . $PDets["Division_Code"] .  "', '" . date('Y-m-d H:i:s') . "' from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code and v.State_Code=". $SFState." Where m.Product_Detail_Code='". $PDets["id"] . "'";
                                                        performQuery($dailyInvNewsql);

                            							//$dailyInvsql = "insert into Trans_Prod_DailyEndInventory (SLNo, SFCode, EntryDt, Prod_Code, CaseQty, PiceQty, ConvQty, RateC_Retail, RateP_Retail, RateC_Dist, RateP_Dist, ApproveFlag, StkId, StkNm, Totalqty) select '" . $pk ."','". $SFCd ."','". date('Y-m-d 00:00:00') ."','". $PDets["id"] . "','" . $PDets["opening_case_qty"] . "','" . $PDets["opening_piece_qty"]. "','" .$PDets["conversionQty"] . "',isnull(RetailCasePrice,0), isnull(Retailor_Price,0),isnull(DistCasePrice,0) DCP,isnull(Distributor_Price,0), 0,'". $vals["StkId"] . "','" . $vals["StkNm"] . "','" . $PDets["Totalqty"] . "' from Mas_Product_Detail m left outer join vwProductStateRates v on m.Product_Detail_Code=v.Product_Detail_Code and m.Division_Code=v.Division_Code and v.State_Code=". $SFState." Where m.Product_Detail_Code='". $PDets["id"] . "'";
                            							//performQuery($dailyInvsql);


                                                        $sql = "SELECT isNull(max(cast(Legr_ID as numeric)),0)+1 as MaxRwID FROM Trans_VanStock_Ledger";
                                                        $tRww = performQuery($sql);
                                                        $MaxRwID = (int) $tRww[0]['MaxRwID'];

                                                        $Ledgersql = "insert into Trans_VanStock_Ledger (Legr_ID,from_loc,fLocTyp,To_Loc,tLocTyp,eDate,PCode,GQty,Division_Code,EntryBy,CalcTyp,TNm) select '" . $MaxRwID ."','". $vals["StkId"] ."','D','". $SFCd ."','F','" . date('Y-m-d H:i:s') . "','" . $PDets["id"] . "','" .$PDets["g_qty"]. "','". $Divc . "','EndInv','0','". $vals["StkNm"] ."'" ;
                                                        performQuery($Ledgersql);


                                                        }


                            }


                            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='" . $SFCd . "')";
                            $device = performQuery($sql);
                            $reg_id = $device[0]['DeviceRegId'];
                            if (!empty($reg_id)) {
                            $msg = "Daily Inventory Unloaded";
                            //send_gcm_notify($reg_id, $msg,"Daily Inventory");
                            sendFCM_notify($reg_id, $msg,'Daily Inventory',0,'#sign-in');
                            }

                            $result = array();
                            $result['$dailyInvNewsql'] = $dailyInvNewsql;
                            $result['dailyInvsql'] = $dailyInvsql;
                            $result['Ledgersql'] = $Ledgersql;
                            $result['vanCurrsql'] = $vanCurrsql;
                            $result['updateLatestTime'] = $updateLatestTime;
                            $result["success"] = true;
                            outputJSON($result);

							die;
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
            //    echo $dataqtwtwetwetewt[0];
        $data1 = json_decode($_POST['data'], true);
        $sfCode = $_GET['sfCode'];
        $divCode = $_GET['divisionCode'];
        $CallPreviewEkey=$data[0]['CallPreviewEkey'];
        $result = array();
        $query = "select Trans_Sl_No,(Order_Value+isnull(discount_price,0)) Order_Value ,Order_Value netamount  from Trans_Order_Head with(nolock) where  OrderID='".$CallPreviewEkey."'  union select Trans_Sl_No,Order_Value,0 from Trans_PriOrder_Head with(nolock) where  OrderID='".$CallPreviewEkey."' union select Trans_Sl_No,Order_Value,0 from Trans_SPriOrder_Head with(nolock) where  OrderID='".$CallPreviewEkey."' union select Trans_Sl_No,Order_Value,0 from Trans_Pragnancy__Head with(nolock) where  OrderID='".$CallPreviewEkey."'";
        $Output=performQuery($query);
        $result['query']=$query;
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

			$sqlsp="Error:".$query."\n|param:\n|error:".json_encode_unicode(sqlsrv_errors())."\n";
			$sqlsp=$sqlsp."data:".$_POST['data'];
			file_put_contents("../server/log/errlog_".date('Y_m_d_H_i_s').".txt",$sqlsp,FILE_APPEND);
$sql = "SELECT ListedDrCode from Mas_ListedDr where Ukey=".$Field["Ukey"];
                    $res = performQuery($sql);
                    if (count($res) > 0){
                        $cCode=$res[0]["ListedDrCode"];
                    }
	$sql="insert into NewContact_Dr (ListedDrCode,Ukey,FormarName, DFDairyMP, MonthlyAI,AITCU,AITMP,MCCNFPM,MCCMilkColDaily,VetsMP,CreatedDate,CustomerCategory,PotentialFSD,CurrentlyUFSD,FrequencyOfVisit)
                            values ('".$cCode."','" . str_replace("'","",$Field["Ukey"]) . "', '".$Field["FormarName"]."','". $Field["DFDairyMP"]."','". $Field["MonthlyAI"]."','".$Field["AITCU"]."', '".$Field["AITMP"]."', '".$Field["MCCNFPM"]."','".$Field["MCCMilkColDaily"]."','".$Field["VetsMP"]."',   '" . date('Y-m-d H:i:s') . "',  '".$Field["Catogoryformer"]."','".$Field["DFPFSDM"]."','".$Field["CUFSD"]."','".$Field["FrequencyOfVisit"]."')";
 performQuery($sql);


            if($data[2]['GIFTCARD']!=1){



                    $sql="insert into Trans_Gift_Issue_Details (Sf_code,Division_Code, Name, Ukey,Additional_Prod_Dtls,Created_Date)
                            values ('$sfCode',  ". str_replace(",", "",$divCode).",'". $Field["FormarName"]."','" . str_replace("'","",$Field["Ukey"]) . "','".$data[2]['GIFTCARD']."','" . date('Y-m-d H:i:s') . "')";
                    performQuery($sql);
}

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
		if($div=="156,"){
			$sql = "insert into tbRCPADetails select '" . $sfCode . "','" .  $vals["entryDate"]  . "'," . $vals["RCPADt"] . "," .
                    $vals["ChmId"] . "," . $vals["DrId"] . "," . $vals["CmptrName"] . "," . $vals["CmptrBrnd"] . "," . $vals["CmptrPriz"] . "," .
                    $vals["ourBrnd"] . "," . $vals["ourBrndNm"] . "," . $vals["Remark"] . ",'" . $div . "'," . $vals["CmptrQty"] . "," . $vals["CmptrPOB"] . "," . $vals["ChmName"] . "," . $vals["DrName"].",'',''," . $vals["hq"]."," . $vals["db"]."," . $vals["route"]."";
	
		}
		else{
            $sql = "insert into tbRCPADetails select '" . $sfCode . "','" . date('Y-m-d H:i:s') . "'," . $vals["RCPADt"] . "," .
                    $vals["ChmId"] . "," . $vals["DrId"] . "," . $vals["CmptrName"] . "," . $vals["CmptrBrnd"] . "," . $vals["CmptrPriz"] . "," .
                    $vals["ourBrnd"] . "," . $vals["ourBrndNm"] . "," . $vals["Remark"] . ",'" . $div . "'," . $vals["CmptrQty"] . "," . $vals["CmptrPOB"] . "," . $vals["ChmName"] . "," . $vals["DrName"].",'',''";
		}         
		 performQuery($sql);
		 $Event_Captures = json_decode($vals["photosList"],true);

	for ($j = 0; $j < count($Event_Captures); $j++) {

		$ev_imgurl=$Event_Captures[$j]["imgurl"];
		$ev_title=$Event_Captures[$j]["title"];
		$ev_remarks=$Event_Captures[$j]["remarks"];
		$DatewithTime = date('Y-m-d H:i:s');
		$ev_imgurl=str_replace("file:///storage/emulated/0/Android/data/com.saneforce.dnetfmcg/cache/", "", $Event_Captures[$j]["imgurl"]);
		$sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,identification,title,remarks,Division_Code,Dateandtime) select '" . $sfCode . "'," . $vals["DrId"]  . "," . $ev_imgurl . ",'rcpa'," . $ev_title . "," . $ev_remarks . ",'" . $Owndiv . "','" .  $vals["entryDate"]  . "'";
		performQuery($sql);

	}
			$resp["success"] = true;
			
			$resp["sql"] = $sql;
                echo json_encode($resp);die;
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
				 case "DCRTPDevReason":
		    $Reason=$vals['reason'];
			$TPWType=$vals['wtype'];
			$TPAreaCode=$vals['clusterid'];
			$TPArea=$vals['ClstrName'];
			$ADate = date('Y-m-d');
			$DCRMissed=$vals['status'];
			  $div = $_GET['divisionCode'];
			if($div=="156" || $div=="156,")
				$DCRMissed=4;
			else
				$DCRMissed=3;

		    $sql="exec svDCRTPDevReason '$sfCode',$TPWType,$TPAreaCode,$TPArea,'$ADate',$Reason,$DCRMissed";
			performQuery($sql);
			 $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
               
                    $msg = "Deviation Received";
              
                //send_gcm_notify($reg_id, $msg,"Leave Application");
                sendFCM_notify($reg_id, $msg,'Deviation Notification',0,'#ViewLeave');
            }
			break;
			case "svCallRevw":
			$sfCode = $_GET['sfCode'];
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    /*$data = json_decode($_POST['data'], true);
    $temp = array_keys($data[0]);
    $vals = $data[0][$temp[0]];*/
	$BLst=json_decode($vals["BrandList"],true);
	$Blsts="";
	$ARCd="";
	$ARDCd="";
	$ECC=0;
	for ($i = 0; $i < count($BLst); $i++) {
		if($BLst[$i]["id"]>-1){
			$Avai=0;
			$EC=0;
			if($BLst[$i]["Avai"]==true) $Avai=1;
			if($BLst[$i]["EC"]==true) {$EC=1;$ECC=1;}

			$Blsts=$Blsts.$BLst[$i]["id"]."#".$BLst[$i]["name"]."#".$Avai."#".$EC."$";
		}
	}

	$ARCd="";$Msg="";//"Call Review Submited Successfully";
	$sql = "{call  svDCRMain_App('".$sfCode."','". $vals['entryDate']."'," . $vals["worktype"] . ",'" . str_replace("'", "", $vals["cluster"]) . "','".$Owndiv."','" . str_replace("'", "", $vals["remarks"]) . "',?)}";
            $params = array(
				array(&$ARCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));

	performQueryWP($sql, $params);
	$query = "{call svRetaRevw('" . $sfCode . "','". $vals['entryDate']."','".$vals["doctorid"]."','".$Blsts."','','".$vals['remarks']."',".$ECC.",'".$ARCd."',?,?,'".$vals["lat"]."','".$vals["long"]."')}";
	$params = array(
		array(&$ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)),
		array(&$Msg, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(150))
	);
    performQueryWP($query, $params);
	$Event_Captures = json_decode($vals["photosList"],true);

	for ($j = 0; $j < count($Event_Captures); $j++) {

		$ev_imgurl=$Event_Captures[$j]["imgurl"];
		$ev_title=$Event_Captures[$j]["title"];
		$ev_remarks=$Event_Captures[$j]["remarks"];
		$DatewithTime = date('Y-m-d H:i:s');
		$ev_imgurl=str_replace("file:///storage/emulated/0/Android/data/com.saneforce.dnetfmcg/cache/", "", $Event_Captures[$j]["imgurl"]);
		$sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,identification,title,remarks,Division_Code,Dateandtime) select '" . $ARCd . "','" . $ARDCd . "'," . $ev_imgurl . ",'[ Review ]'," . $ev_title . "," . $ev_remarks . ",'" . $Owndiv . "','".$DatewithTime."'";
		performQuery($sql);

	}
	$result["Msg"] = $query;
	$result["success"] = true;
   echo json_encode($result);die;
			break;
			 case "svSesFeedbk":
				$Ekey =date('Y-m-d H:i:s').$sfCode;
				if($vals["SalVal"]!=null && $vals["SalVal"]!="undefined"){
					$vals["SalVal"]=$vals["SalVal"];
				}else{
					$vals["SalVal"]='0';
				}
				$sql="select isnull(max(RwID),0)+1 rwid from tbINHSessFeedbk";
				$result=performQuery($sql);
				$rwid=$result[0]['rwid'];
				$sFeed="<ROOT><INH SID=\"".$vals["SessID"] ."\" SF=\"".$vals["dataSF"] ."\" eDtTM=\"" . $vals["eDtTM"] . "\" Skid=\"".$vals["Stkid"] ."\" RID=\"".$vals["RouteID"] ."\"  RNm=\"".$vals["RouteNm"] ."\" DrCd=\"".$vals["DrCode"] ."\"  DrNm=\"".$vals["DrName"] ."\" DrAd=\"".$vals["DrAddress"] ."\" CM=\"".$vals["CusMeet"] ."\" IC=\"".$vals["IntCus"] ."\" PC=\"".$vals["PCus"] ."\" SV=\"".$vals["SalVal"] ."\" rmks=\"".$vals["remarks"] ."\"  SMV=\"".$vals["SamVal"] ."\" /></ROOT>";
				$query = "exec svINHSessFeed '" . $sFeed . "','" .  $sfCode . "','" . $vals["eDt"] . "','" .$Ekey. "','".$rwid."'";
				performQuery($query);

				  $Current_Date_And_Time =date('Y-m-d H:i:s');

				  $divCode = $_GET['divisionCode'];

				 	if($data[1]['Activity_Stk_POB_Report']!=null && $data[1]['Activity_Stk_POB_Report']!="undefined")
			{
				$products = $data[1]['Activity_Stk_POB_Report'];
				for ($j = 0; $j < count($products); $j++) {
 $sql="select isnull(max(cast(trans_order_no as numeric)),0)+1 orderno from tbINHSessFeedbk_order_details";
				$result=performQuery($sql);
				$orderNo=$result[0]['orderno'];
					$product_code=str_replace("'", "",$products[$j]["product_code"]);
				 	$product_Name=str_replace("'", "",$products[$j]["product_Name"]);
				 	$Qty=str_replace("'", "",$products[$j]["Qty"]);
				 	$free=str_replace("'", "",$products[$j]["free"]);
					$PieseRate=str_replace("'", "",$products[$j]["PieseRate"]);

								 	$Rate=str_replace("'", "",$products[$j]["Rate"]);
									$value=$Qty*$Rate;
				$sql = "insert into tbINHSessFeedbk_order_details select '" . $orderNo . "','" . $rwid . "','" . $product_code . "','" . $product_Name . "','" . $Qty . "',
'". $value."','" . $Rate . "','" . $free . "','" . $PieseRate . "','" . $sfCode . "'";
		           performQuery($sql);
				}
			}
				if($data[2]['ActivityCaptures']!=null && $data[2]['ActivityCaptures']!="undefined")
			{
				$Event_Captures = $data[2]['ActivityCaptures'];
				for ($j = 0; $j < count($Event_Captures); $j++) {
					$ev_imgurl=str_replace("'", "",$Event_Captures[$j]["imgurl"]);
				 	$ev_title=str_replace("'", "",$Event_Captures[$j]["title"]);
				 	$ev_remarks=str_replace("'", "",$Event_Captures[$j]["remarks"]);

					$sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Dateandtime) values( '" . $sfCode . "','" . $Ekey . "','" . $ev_imgurl . "','" . $ev_title . "','" . $ev_remarks . "','". str_replace(",", "",$divCode)."','Promotion','" . $Current_Date_And_Time . "')";
		            performQuery($sql);
				}
			}
			$resp=array();
				if ($resp["msg"] != ""){
				$resp["msg"] = "Submitted Successfully";
			}

                $resp["success"] = true;
                echo json_encode($resp);
                die;
				die;
        break;

         case "Activity_Report_APP":
		 $divs = explode(",", $div . ",");
         $divisionCode = (string) $divs[0];

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

$missed="0";
			if($vals["mydayplan"]!=null && $vals["mydayplan"]!=''){
				$missed="1";
			   $workTypFlag_Missed=str_replace("'", "", $vals["workTypFlag_Missed"]);
			   $mypln_town=str_replace("'", "", $vals["mypln_town"]);
			   $mypln_town_id=str_replace("'", "", $vals["mypln_town_id"]);
				if($vals["hq_code"]!=null)
					$hq_code=str_replace("'", "", $vals["hq_code"]);
				else
					$hq_code="";
				$sqlmi = "delete from tbMyDayPlan where sf_code='" . $sfCode . "' and cast(Pln_Date as date)='".$today."' and cluster='" . $mypln_town_id . "'";
				performQuery($sqlmi);
				    $sqlm = "delete from dcrmain_trans where sf_code='" . $sfCode . "' and cast(activity_date as date)='".$today."' and FieldWork_Indicator='w'";
                                                  performQuery($sqlm);
				$sqlm = "insert into tbMyDayPlan (sf_code,Pln_Date,remarks,Division_Code,ClstrName,wtype,FWFlg,cluster,sf_member_code)  select '" . $sfCode . "','".$today."','Missed Entry','" . $Owndiv . "','" . $mypln_town . "'," . $vals["Worktype_code"] . ",'" . $workTypFlag_Missed . "','" . $mypln_town_id . "','" . $hq_code . "'";
				performQuery($sqlm);
			}



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

 $Slab_types=$DetTB["SlabTypes_Id"];
  $SlabNames=$DetTB["SlabNames"];
 if($Slab_types!="undefined" &&  $Slab_types !=null){
	 $sql = "update Mas_ListedDr set Slab_Id='" . $Slab_types . "'   ,Slan_Name='" . $SlabNames . "' where ListedDrCode='" .  $docor_code. "' "; performQuery($sql);

 }
				     if($DetTB['visit_id']==null){
                        $visit_id="''";
						$visit_name="''";
					 }
                    else{
                        $visit_id =$DetTB['visit_id'];
						 $visit_name =$DetTB['visit_name'];
					}
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
                     $sql = "SELECT ListedDrCode from Mas_ListedDr where ListedDr_Profile=" . $cCode." and sf_code='" . $sfCode . "' ";
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
                //primary
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
					$OrdEF=0;
                    if (isset($tableData['Activity_Stk_POB_Report'])){
                        $samp = $tableData['Activity_Stk_POB_Report'];
                        $PPXML = "<ROOT>";
                        for ($j = 0; $j < count($samp); $j++) {
							$cl_qty_stock=$samp[$j]["cb_qty"];
							$Free_qty=$samp[$j]["free"];
							$discount=$samp[$j]["discount"];
							$discount_price=$samp[$j]["discount_price"];
							$tax_price=$samp[$j]["tax_price"];

							$FreeP_Code=$samp[$j]["FreeP_Code"];
							$Fname=$samp[$j]["Fname"];
							 if($discount==null)
                             $discount=0;
                            else
                            $discount = $discount;

							if($discount_price==null)
                             $discount_price=0;
                            else
                            $discount_price = $discount_price;

							if($tax_price==null)
								$tax_price=0;
                            else
								$tax_price = $tax_price;


							$PieceQty=0;
							$CaseQty=0;
							if($samp[$j]["Qty"]==$samp[$j]["rx_Conqty"]){  
								$PieceQty=$samp[$j]["rx_Conqty"];
							}
							else{
								$CaseQty=$samp[$j]["rx_Conqty"];
							}
                            //$PPXML = $PPXML . "<Prod PCode=\"".$samp[$j]["product_code"]."\" PName=\"".$samp[$j]["product_Name"]."\" CQty=\"".$CaseQty."\" Rate=\"".$samp[$j]["Rate"]."\" PQty=\"".$samp[$j]["Qty"]."\" CL=\"".$cl_qty_stock."\"  Free=\"".$Free_qty."\"  discount_price=\"".$discount_price."\"  discount=\"".$discount."\"   Fname=\"".$Fname."\"  FreeP_Code=\"".$FreeP_Code."\"    tax=\"".$tax_price."\"   Qty=\"".$samp[$j]["Qty"]."\"  />";
                            $PPXML = $PPXML . "<Prod PCode=\"".$samp[$j]["product_code"]."\" PName=\"".$samp[$j]["product_Name"]."\" CQty=\"".$samp[$j]["Qty"]."\" Rate=\"".$samp[$j]["Rate"]."\" PQty=\"".$samp[$j]["PQty"]."\" CL=\"".$cl_qty_stock."\"  Free=\"".$Free_qty."\"  discount_price=\"".$discount_price."\"  discount=\"".$discount."\"   Fname=\"".$Fname."\"  FreeP_Code=\"".$FreeP_Code."\"    tax=\"".$tax_price."\"  Qty=\"".$samp[$j]["rx_Conqty"]."\"  Product_Unit_Name=\"".$samp[$j]["product_unit_name"]."\"  Product_Unit_Value=\"".$samp[$j]["OrdConv"]."\" />";
							//$PPXML = $PPXML . "<Prod PCode=".$samp[$j]["product_code"]." PName=".$samp[$j]["product_Name"]." CQty=".$samp[$j]["Qty"]." Rate=".$samp[$j]["Rate"]." PQty=".$samp[$j]["PQty"]." CL=".$cl_qty_stock."  Free=".$Free_qty."  discount_price=".$discount_price."  discount=".$discount."   Fname=".$Fname."  FreeP_Code=".$FreeP_Code." tax=".$tax_price."  Qty=".$samp[$j]["rx_Conqty"]."  Product_Unit_Name=".$samp[$j]["product_unit_name"]."  Product_Unit_Value=".$samp[$j]["OrdConv"]."/>";


                            $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Qty"] . "#";
                            $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Qty"] . "#";
							$OrdEF=1;
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
                if (sqlsrv_errors() != null && $tr != '') {
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

                $sqlquery = "insert into DCRDetail_Distributors_Hunting select '$ARCd'," .$distHunting['name'] . "," . $distHunting['contact'] . "," . $distHunting['phone'] . "," . $vals["Daywise_Remarks"] .",'" . date('Y-m-d H:i:s') . "'," .$distHunting["area"] . ",(select isnull(max(slno),0)+1 from DCRDetail_Distributors_Hunting)," . $distHunting["address"] . ",'$sfCode'," . $vals["DataSF"] . ",".$distHunting["Worked_With"]."," . $distHunting['lat'] . "," . $distHunting['long'] . ",". $distHunting['distype']."";
                //$sql = "exec  svDCRDetailDistirbutorsHunting '$ARCd'," .$distHunting['name'] . "," . $distHunting['contact'] . "," . $distHunting['phone'] . "," .$distHunting["area"] . "," . $vals["Daywise_Remarks"] . "," . $distHunting["address"] . ",'" . date('Y-m-d H:i:s') . "','$sfCode'," . $vals["DataSF"] . ",".$distHunting["Worked_With"]."," . $distHunting['lat'] . "," . $distHunting['long'] . "";         
				//$result = performQuery($sql);
				$resultcheck = performQuery($sqlquery);

					if( $data[count($data)-3]["Activity_Event_Captures"]!=null && $data[count($data)-3]["Activity_Event_Captures"]!="undefined")
            {
                $Event_Captures = $data[count($data)-3]["Activity_Event_Captures"];
                for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=$Event_Captures[$j]["imgurl"];
                    $ev_title=$Event_Captures[$j]["title"];
                    $ev_remarks=$Event_Captures[$j]["remarks"];
                    $DatewithTime=$Event_Captures[$j]["DatewithTime"];
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,lat,lon,Dateandtime) select '" . $ARCd . "','" . $ARDCd . "'," . $ev_imgurl . "," . $ev_title . "," . $ev_remarks . ",'" . $Owndiv . "' ,'DH','".date('Y-m-d H:i:s')."','".$lat."','".$lng."' ,'".$DatewithTime."'";
                    performQuery($sql);
                }
            }
                $resp["success"] = true;
				$resp ["query"] = $sql;
                echo json_encode($resp);
				die;
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
                    $sqlsp = $sqlsp . "'" . str_replace("'", "", $vals["Town_code"]) . "','" . str_replace("'", "''", $vals["Daywise_Remarks"]) . "',?,'" . str_replace("'", "", $vals["rx_t"]) . "'," . $DetTB["modified_time"] . ",?,?," . $vals["DataSF"] . ",'" . $DetTB["geoaddress"] . "'," . $POB_Value . ",$net_weight_value,".$DetTB['stockist_code'].",".$DetTB['stockist_name'].",$discount_price,'$rateMode','".$DemoTo."',$visit_id,$visit_name,'".$POT."')}";
                else if($vTyp==3){
                    if($DetTB["intrumenttype"]==null)
                        $DetTB["intrumenttype"]="";
                    if($DetTB["date_of_intrument"]==null)
                        $DetTB["date_of_intrument"]="";
                    $sqlsp = $sqlsp . "'" . str_replace("'", "", $vals["Town_code"]) . "','" . str_replace("'", "''", $vals["Daywise_Remarks"]) . "',?,'" . str_replace("'", "", $vals["rx_t"]) . "'," . $DetTB["modified_time"] . ",?,?," . $vals["DataSF"] . ",'" . $DetTB["geoaddress"] . "'," . $POB_Value . ",'" . $DetTB["intrumenttype"] . "','" . $DetTB["date_of_intrument"] . "')}";
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
                    $resp["msg"] = "Call Already Exists";
                    $resp["success"] = false;
                    echo json_encode($resp);
                    die;
					($vTyp == 3  || $vTyp == 8) && $OrdEF==1)
                }*/
                if($vTyp == 3  || $vTyp == 8){
                    if($vTyp == 3){
                    $query = "SELECT code = Division_SName+cast(SLNo as varchar)+'-'+cast(isnull(POrdSl,0)+1 as varchar) FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "'";	
                    $result = performQuery($query);
					$resp["priTransNo"] = $result;
					$sqlOrd = "{call svPriOrderNative(?,$cCode,?," . $POB_Value . ",'" . $DetTB["intrumenttype"] . "','" . $DetTB["date_of_intrument"] . "'," . $pob . ",'" . str_replace("'", "''", $vals["Daywise_Remarks"]) . "',?,?,N'".$PPXML."','".$eKeyID."','".$POT."',".$Super_Stck_code.")}";
                    }else{

                   $sqlOrd = "{call svSuperStockist(?,$cCode,?," . $POB_Value . ",'" . $DetTB["intrumenttype"] . "','" . $DetTB["date_of_intrument"] . "'," . $pob . ",'" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,?,N'".$PPXML."','".$eKeyID."','".$POT."')}";


}
                    $params1 = array(
                        array($sfCode, SQLSRV_PARAM_IN),
                        array($today, SQLSRV_PARAM_IN),
                        array($ARCd, SQLSRV_PARAM_IN),
                        array($Owndiv, SQLSRV_PARAM_IN)
                        /*,array($PPXML, SQLSRV_PARAM_IN)*/
                    );

                    performQueryWP($sqlOrd, $params1);


                }

            }
			
			if($DetTB["audioFileName"]!=null||$DetTB["audioFileName"]!="){
						 $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,lat,lon,Dateandtime) select '" . $ARCd . "','" . $ARDCd . "'," . $DetTB["audioFileName"] . ",'','','" . $Owndiv . "' ,'audio','".date('Y-m-d H:i:s')."','".$lat."','".$lng."' ,'".$DatewithTime."'";
                   $qry= performQuery($sql);
				 $resp["audioSave"] =$sql;		 
					 }
					 
					 


            if( $data[count($data)-3]["Activity_Event_Captures"]!=null && $data[count($data)-3]["Activity_Event_Captures"]!="undefined")
            {
                $Event_Captures = $data[count($data)-3]["Activity_Event_Captures"];
                for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=$Event_Captures[$j]["imgurl"];
                    $ev_title=$Event_Captures[$j]["title"];
                    $ev_remarks=$Event_Captures[$j]["remarks"];
                    $DatewithTime=$Event_Captures[$j]["DatewithTime"];
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,lat,lon,Dateandtime) select '" . $ARCd . "','" . $ARDCd . "'," . $ev_imgurl . "," . $ev_title . "," . $ev_remarks . ",'" . $Owndiv . "' ,'DCR','".date('Y-m-d H:i:s')."','".$lat."','".$lng."' ,'".$DatewithTime."'";
                    performQuery($sql);
                }
            }
			
			
 //$resp["callphoto"] =$data[count($data)-1]["Activity_Event_Captures_Call"];
            if( $data[count($data)-1]["Activity_Event_Captures_Call"]!=null && $data[count($data)-1]["Activity_Event_Captures_Call"]!="undefined")
            {
                $Event_Captures = $data[count($data)-1]["Activity_Event_Captures_Call"];
                for ($j = 0; $j < count($Event_Captures); $j++) {
                    $ev_imgurl=$Event_Captures[$j]["imgurl"];
                    $ev_title=$Event_Captures[$j]["title"];
                    $ev_remarks=$Event_Captures[$j]["remarks"];
                    $DatewithTime=$Event_Captures[$j]["DatewithTime"];
                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,lat,lon,Dateandtime) select '" . $ARCd . "','" . $ARDCd . "'," . $ev_imgurl . "," . $ev_title . "," . $ev_remarks . ",'" . $Owndiv . "' ,'FirstCallSelfie','".date('Y-m-d H:i:s')."','".$lat."','".$lng."' ,'".$DatewithTime."'";
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
			$taxTotalValue = $data[1]["Activity_Doctor_Report"]['taxTotalValue'];
			$discTotalValue = $data[1]["Activity_Doctor_Report"]['discTotalValue'];
			$subTotal = $data[1]["Activity_Doctor_Report"]['subTotal'];
			$no_of_items = $data[1]["Activity_Doctor_Report"]['No_Of_items'];	

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

			if($data[1]["Activity_Doctor_Report"]['CheckinTime']==null){
				$OrderStrat_Time="";
			}else{
			$OrderStrat_Time = $data[1]["Activity_Doctor_Report"]["CheckinTime"];
			}

			if($data[1]["Activity_Doctor_Report"]["CheckoutTime"]==null){
				$OrderEnd_Time="";
			}else{
			$OrderEnd_Time = $data[1]["Activity_Doctor_Report"]["CheckoutTime"];
			}

			if ($vTyp == 1 ){
				$sql = "update DCRDetail_Lst_Trans set StartOrder_Time='$OrderStrat_Time',EndOrder_Time= '$OrderEnd_Time'  where Sf_Code='" . $sfCode . "' and Trans_Detail_Slno='$ARDCd'";
				performQuery($sql);
			}
            if (count($transOrderDetails) > 0 ) {
                $sql = "SELECT OrderID FROM Trans_Order_Head with(nolock) where OrderID='".$eKeyID."' and isnull(OrderID,'')<>''";
                $eRw = performQuery($sql);
                if(count($eRw)<1 || $eKeyID==""){
                    /* $sql = "SELECT isnull(OrdSl,0)+1 RwID,Division_SName+cast(SLNo as varchar) SLNo FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "'";
                    $tRw = performQuery($sql);
                    $pk =$tRw[0]['SLNo']."-".$tRw[0]['RwID'];
                    $sql = "update Mas_SF_SlNo set OrdSl=".$tRw[0]['RwID']." where SF_Code='" . $sfCode . "'";
                    performQuery($sql);
 */

//$sql = "SELECT isnull(OrdSl,0)+1 RwID,Division_SName+cast(SLNo as varchar) SLNo FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "'";
if($stockistCode=="''"||$stockistCode==null){
$sql = "SELECT isnull(OrdSl,0)+1 RwID,Division_SName+cast(SLNo as varchar) SLNo FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "'";
                    $tRw = performQuery($sql);
                    $pk =$tRw[0]['SLNo']."-".$tRw[0]['RwID'];
                    $sql = "update Mas_SF_SlNo set OrdSl=".$tRw[0]['RwID']." where SF_Code='" . $sfCode . "'";
                    performQuery($sql);
                    
}
else{
$sql="exec  getMxH'" . $sfCode . "'," . $stockistCode . ",'".$Owndiv."'";

                    $tRw = performQuery($sql);
                   
				//$SFSl=$tRw[0]['SLNo']."-";
                    $pk =$tRw[0]['MxH'];
					  $sql = "update Mas_SF_SlNo set OrdSl=(ISNULL(OrdSl,0)+1) where SF_Code='" . $sfCode . "'";
                    performQuery($sql);

}
                   


                    /*$sqlll = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value,discount_price,rateMode,Div_ID,OrderID,OrderType,TDPercent,Order_In_Time,Order_Out_Time,Order_Flag) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue," . $orderDate . ", $townCode,'" . $DCRCode . "','$net_weight_value','$discount_price','$rateMode','$Owndiv','".$eKeyID."','".$POT."',".$Discountpercent.",'".$OrderStrat_Time."','".$OrderEnd_Time."','0'";*/
                     $sqlll = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value,discount_price,rateMode,Div_ID,OrderID,OrderType,TDPercent,Order_In_Time,Order_Out_Time,Order_Flag,Sub_Total,Dis_Total,Tax_Total,No_Of_items) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue," . $orderDate . ", $townCode,'".$DCRCode."','$net_weight_value','$discount_price','$rateMode','$Owndiv','".$eKeyID."','".$POT."',".$Discountpercent.",'".$OrderStrat_Time."','".$OrderEnd_Time."','0','".$subTotal."','".$discTotalValue."','".$taxTotalValue."','".$no_of_items."'";


                    performQuery($sqlll);

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


					   if($transOrderDetails[$i]['UnitId']==null)
                           $UnitId=0;
                        else
                           $UnitId=$transOrderDetails[$i]['UnitId'];
					   if($transOrderDetails[$i]['rx_Conqty']==null)
                           $rx_Conqty=0;
                        else
                           $rx_Conqty=$transOrderDetails[$i]['rx_Conqty'];
					   if($transOrderDetails[$i]['UnitName']==null)
                           $UnitName='';
                        else
                           $UnitName=$transOrderDetails[$i]['UnitName'];

	                     if($RcpaId=="") $RcpaId="0";
                        if($cbQty=="") $cbQty="0";
						 if($UnitId=="") $UnitId="0";

						 if($rx_Conqty=="") $rx_Conqty="0";

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



                        if($qty>0 || $free>0   || $cbQty>0  ||  $Ccb_qty>0 || $Promo>0  || $productremarks!==$remarkspd ||  $RcpaId>0 ){


                            /* $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Order_Details ";
                            $tRw = performQuery($sql);
                            $pk1 = (int) $tRw[0]['RwID']; */

                              $sql = "SELECT isnull(OrdDsl,0)+1  RwID,Division_SName+cast(SLNo as varchar) SLNo FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "'";
							$tRw = performQuery($sql);
							$SFSl=$tRw[0]['SLNo']."-";
							$pk1 =$SFSl.$tRw[0]['RwID'];
							$sql = "update Mas_SF_SlNo set OrdDsl=".$tRw[0]['RwID']." where SF_Code='" . $sfCode . "'";
							performQuery($sql);


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




                            $sql = "select Product_Detail_Code,case when ltrim(rtrim(isnull(cast (Distributor_Discount_Price as float),'')))='' then 0 else isnull(cast (Distributor_Discount_Price as float),'0') End DistDisc,case when ltrim(rtrim(isnull(cast (MRP_Price as float),'')))='' then 0 else isnull(cast (MRP_Price as float),'0') End DistPRate from Mas_Product_State_Rates r inner join Mas_Salesforce s on r.State_Code=s.State_Code and r.Division_Code=cast(left(s.Division_Code,charindex(',',s.Division_Code+',')-1) as int) where Sf_Code='" . $sfCode . "' and Product_Detail_Code='" . $transOrderDetails[$i]['product_code'] . "'";
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



                            if($transOrderDetails[$i]['tax_price']==null)
                               $tax_price=0;
                            else
                                $tax_price=$transOrderDetails[$i]['tax_price'];



                           if($qty>0 || $free>0   || $cbQty>0 || $Promo>0   ||  $Ccb_qty>0   ||  $RcpaId>0  ){

//                            $sqlOrd = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock,Uom_Id,Con_Qty,Unit_Name,Tax_value) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product_code'] . "',N'" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,$rate,'$net_weight','$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','". $cbQty  ."',$Promo,'$Owndiv','" . $Fname . "','" . $FreeP_Code . "' ,'". $Ccb_qty  ."' ,'".  $UnitId  ."' ,'".  $rx_Conqty."','".  $UnitName  ."','".  $tax_price ."'";
$orderValue=$orderValue+$value;
if($Owndiv=="156"){
	/*$sqlOrd = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock,Uom_Id,Con_Qty,Unit_Name,Tax_value,qty,sl_no) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product_code'] . "',N'" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,$rate,'$net_weight','$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','". $cbQty  ."',$Promo,'$Owndiv','" . $Fname . "','" . $FreeP_Code . "' ,'". $Ccb_qty  ."' ,'".  $UnitId  ."' ,'".  $transOrderDetails[$i]['OrdConv'] . "','".  $UnitName  ."','".  $tax_price ."','".  $rx_Conqty ."','".  $transOrderDetails[$i]['schemeswitch'] . "'";*/
	$sqlOrd = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock,Uom_Id,Con_Qty,Unit_Name,Tax_value,qty,sl_no,umo_unit,New_Qty,unit,Conf_Fac) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product_code'] . "',N'" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,$rate,'$net_weight','$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','". $cbQty  ."',$Promo,'$Owndiv','" . $Fname . "','" . $FreeP_Code . "' ,'". $Ccb_qty  ."' ,'".  $UnitId  ."' ,'".  $transOrderDetails[$i]['OrdConv'] . "','".  $UnitName  ."','".  $tax_price ."','".  $rx_Conqty ."','".  $transOrderDetails[$i]['schemeswitch'] . "','".  $UnitId  ."','".  $rx_Conqty ."','".  $UnitName  ."','".  $transOrderDetails[$i]['OrdConv'] . "'";
                           
	
}
else{
							/*$sqlOrd = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock,Uom_Id,Con_Qty,Unit_Name,Tax_value,qty) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product_code'] . "',N'" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,$rate,'$net_weight','$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','". $cbQty  ."',$Promo,'$Owndiv','" . $Fname . "','" . $FreeP_Code . "' ,'". $Ccb_qty  ."' ,'".  $UnitId  ."' ,'".  $transOrderDetails[$i]['OrdConv'] . "','".  $UnitName  ."','".  $tax_price ."','".  $rx_Conqty ."'";*/
							$sqlOrd = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock,Uom_Id,Con_Qty,Unit_Name,Tax_value,qty,umo_unit,New_Qty,unit,Conf_Fac) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product_code'] . "',N'" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,$rate,'$net_weight','$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','". $cbQty  ."',$Promo,'$Owndiv','" . $Fname . "','" . $FreeP_Code . "' ,'". $Ccb_qty  ."' ,'".  $UnitId  ."' ,'".  $transOrderDetails[$i]['OrdConv'] . "','".  $UnitName  ."','".  $tax_price ."','".  $rx_Conqty ."','".  $UnitId  ."','".  $rx_Conqty ."','".  $UnitName  ."','".  $transOrderDetails[$i]['OrdConv'] . "'";
}                          
						  performQuery($sqlOrd);


							if($transOrderDetails[$i]['vanSalesOrder']==null)
                               $vanSalesOrder=0;
                            else
                                $vanSalesOrder=$transOrderDetails[$i]['vanSalesOrder'];


							 if(($POT==2 || $POT==3) && $vanSalesOrder == 1){


								$docor_Name=str_replace("'", "", $data[1]['Activity_Doctor_Report']['doctor_name']);
							 
							 if($qty > 0){
								$sql3 = "SELECT isNull(max(cast(Legr_ID as numeric)),0)+1 as MaxRwID FROM Trans_VanStock_Ledger";
								$tRww = performQuery($sql3);
								$MaxRwID = (int) $tRww[0]['MaxRwID'];

								$qsql = "insert into Trans_VanStock_Ledger (Legr_ID,from_loc,fLocTyp,To_Loc,tLocTyp,eDate,PCode,GQty,Division_Code,EntryBy,CalcTyp,TNm)  select '" . $MaxRwID . "' , '" . $sfCode . "','F' ," . $custCode . ",'R' , " . $orderDate . ",'" . $transOrderDetails[$i]['product_code'] . "','".$qty ."','$Owndiv','VanInv','0','" . $docor_Name . "'";
								 performQuery($qsql);	 
								 }

							 if($free > 0){
								$sql = "SELECT isNull(max(cast(Legr_ID as numeric)),0)+1 as MaxRwID FROM Trans_VanStock_Ledger";
								$tRww = performQuery($sql);
								$MaxRwID = (int) $tRww[0]['MaxRwID'];

								 
								$fsql = "insert into Trans_VanStock_Ledger (Legr_ID,from_loc,fLocTyp,To_Loc,tLocTyp,eDate,PCode,GQty,Division_Code,EntryBy,CalcTyp,TNm)  select '" . $MaxRwID . "' , '" . $sfCode . "','F' ," . $custCode . ",'R' , " . $orderDate . ",'" . $transOrderDetails[$i]['product_code'] . "','" . $free . "','$Owndiv','Free','0','" . $docor_Name . "'";
								 performQuery($fsql);	 
								 }
								 
							  if($cbQty > 0){
								  
								$sql = "SELECT isNull(max(cast(Legr_ID as numeric)),0)+1 as MaxRwID FROM Trans_VanStock_Ledger";
								$tRww = performQuery($sql);
								$MaxRwID = (int) $tRww[0]['MaxRwID'];

								$csql = "insert into Trans_VanStock_Ledger (Legr_ID,from_loc,fLocTyp,To_Loc,tLocTyp,eDate,PCode,GQty,Division_Code,EntryBy,CalcTyp,TNm)  select '" . $MaxRwID . "' , '" . $sfCode . "','F' ," . $custCode . ",'R' , " . $orderDate . ",'" . $transOrderDetails[$i]['product_code'] . "','" . $cbQty . "','$Owndiv','Return','0','" . $docor_Name . "'";
								performQuery($csql);	 
								}

							

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
						$sql = "update Trans_Order_Head set Order_Value=".$orderValue." where  Trans_Sl_No='" . $pk . "'";
						//$sql = "update Trans_Order_Head set Order_Value=(select sum(value) from Trans_Order_Details where Trans_Sl_No='" . $pk . "') where  Trans_Sl_No='" . $pk . "'";
						//$sql = "update Trans_Order_Head set Order_Value=((select sum(value) from Trans_Order_Details where Trans_Sl_No=Trans_Order_Head.Trans_Sl_No and Div_ID=Trans_Order_Head.Div_ID)) where  Trans_Sl_No='" . $pk . "' and Div_ID='$Owndiv'";
						$resp["HeadQryy"]=$today;
						performQuery($sql);
						//if($sfCode=="MR3532"){
						//sendSMS();
						//}
					}
            }
			$orderValue=0;
			if($collectedAmount =="")
			   $collectedAmount = "0";
            if ($collectedAmount <>"0"){
                //$PayTypeNm
                $sql = "SELECT isNull(max(cast(Sl_No as numeric)),0)+1 as RwID FROM Trans_Payment_Detail";
                $tRw = performQuery($sql);
                $pk1 = (int) $tRw[0]['RwID'];
                $sql = "insert into Trans_Payment_Detail(Sl_No,Sf_Code,Sf_Name,Cust_Id,Cus_Name,Amount,Pay_Mode,Pay_Date,Pay_Ref_No,Remarks,Distributor_Code,Route_code,eDate,PaymentName)  select '" . $pk1 . "','" . $sfCode . "',SF_Name," . $custCode . ",(select  ListedDr_Name from Mas_ListedDr where ListedDrCode=" . $custCode . "),$collectedAmount,'".$PayType."','".$PayDt."','".$PayNo."','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "'," . $stockistCode . ", ".$townCode."," . $orderDate . ",'".$PayTypeNm."' from Mas_Salesforce where SF_Code='" . $sfCode . "'";
                performQuery($sql);
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
				$resp["priquery"]=$sql;
            } else {
                $outstandinAmt = $orderValue - $collectedAmount;
                $sql = "SELECT isNull(max(cast(Trans_Collection_No as numeric)),0)+1 as RwID FROM Order_Collection_Details";
                $tRw = performQuery($sql);
                $pk2 = (int) $tRw[0]['RwID'];
                $sql = "insert into Order_Collection_Details(Trans_Collection_No,Sf_Code,Cust_Code,Stockist_Code,Total_Order_Amount,Amt_Collect,Out_standing_Amt,Last_Order_Amt,Route) select '" . $pk2 . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",$orderValue,$collectedAmount,$outstandinAmt,$orderValue, $townCode";
                performQuery($sql);
                $resp["success"] = true;

            }
            }
			if ($resp["msg"] != ""){
				$resp["msg"] = "Submitted Successfully";
			}

                $resp["success"] = true;
                $resp["sqlOrd"] = $sqlOrd;
                $resp["trans_slno"] = $pk;
                $resp["params1"] = $sqlll;
                echo json_encode($resp);
                die;


            break;



    }

    $resp["success"] = true;
   $resp["SPSQl"] = $sqlsp;
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
		 case "get/nonOutlets":
		//$SF=$_GET['SF_Code'];
    	$routecode = $_GET['routecode'];
		$query = "select doctor_code,doctor_name,isnull((select top 1 REPLACE(CONVERT(varchar(11), ModTime, 113 ),' ',' ')  from DCRDetail_Lst_Trans where Trans_Detail_Info_Code=Doctor_Code order by modtime desc),'Last Visit') lastvisit from vwDoctor_Master_APP l
left join visited_dr_two_months v on l.Doctor_Code=v.trans_detail_info_code
where Trans_Detail_Info_Code is null and l.Town_Code='$routecode'  and doctor_active_flag=0 group by  Doctor_Code,Doctor_Name";

		$results = performQuery($query);
		echo json_encode($results);die;
        break;
    case "get/track":
            $SF=$_GET['SF_Code'];
        $Dt = str_replace(".000000","",$_GET['Dt']);
        $query = "select * from ((select replace(convert(varchar,Time,9),':000',' ') DtTime,lati lat,Long lng,Trans_Detail_Name,(Select  Doc_Spec_ShortName  from Mas_ListedDr where ListedDrCode=m.Trans_Detail_Info_Code) ChannelName,POB_Value,net_weight_value,GeoAddrs,Time DtTm,0 ordfld,1 typ from vwActivity_MSL_Details m  where  cast(convert(varchar,Time,101) as datetime)=cast(convert(varchar,cast('$Dt' as datetime),101) as datetime) and Time>'$Dt'  and sf_code='$SF')union(select replace(convert(varchar,DtTm,9),':000',' ') DtTime,lat,Lon lng,'','','0','0','',DtTm,1,0 typ from tbTrackLoction where SF_Code='$SF'  and cast(convert(varchar,DtTm,101) as datetime)=cast(convert(varchar,cast('$Dt' as datetime),101) as datetime) and DtTm>'$Dt' and cast(Auc as float)<=30)   union( select replace(convert(varchar,ModTime,9),':000',' ') DtTime,lati lat,Long lng,Trans_Detail_Name, ''ChannelName,POB_Value,'0' net_weight_value,GeoAddrs,ModTime DtTm,0 ordfld,2 typ from vwActivity_CSH_Detail h where  cast(convert(varchar,ModTime,101) as datetime)=cast(convert(varchar,cast('$Dt' as datetime),101) as datetime) and ModTime>'$Dt'   and sf_code='$SF')) as t order by DtTm,ordfld";
        $result = performQuery($query);
        outputJSON($result);
        break;
        	case "get/approvalList":
                $SF=$_GET['sfCode'];
//                $query = "select DISTINCT StkId as distributor_id, StkNm as distributor_name, convert(varchar, EntryDt, 101) as submitted_date,SFCode as hq_id from Trans_Prod_DailyInventoryNative where ApproveFlag = 0";
				
				$query = "select DISTINCT StkId as distributor_id, StkNm as distributor_name, convert(varchar, EntryDt, 101) as submitted_date,SFCode as hq_id from Trans_Prod_DailyInventoryNative tpd inner join Mas_Salesforce m on ApproveFlag = 0 and Sf_Code =tpd.SFCode
				and m.Reporting_To_SF = '" . $SF . "'  and  convert(date,EntryDt)='". date('Y-m-d')."'";
                $result = performQuery($query);
                outputJSON($result);
                break;


    case "deleteEntry":
        $data = json_decode($_POST['data'], true);
        $sfCode = $_GET['sfCode'];
        $custCode = "'" . $data['custId'] . "'";
		$sec = "'" . $data['sec'] . "'";
		$secVal=str_replace("'", "",$sec);
		

        $arc = (isset($data['arc']) && strlen($data['arc']) == 0) ? null : $data['arc'];
        $amc = (isset($data['amc']) && strlen($data['amc']) == 0) ? null : $data['amc'];
		orderDetailsDelete($sfCode, $custCode,$arc, $amc,$secVal);
		priOrderDetailDelete($sfCode,$arc,$amc,$secVal);
		if($secVal!="1"){
        $result = deleteEntry($arc,$amc);
		 outputJSON($secVal);
		}
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
	case "get/Visits":
		$sql="select * from mas_visits";
		$det=performQuery($sql);
		outputJSON($det);
		break;
	case "get/GoldenSchemeDoctor":
		 $sfCode = $_GET['sfCode'];
		$sql="SELECT doctor_code as id,town_code,'Golden' name FROM vwDoctor_Master_APP as tab WHERE charindex(',$sfCode,',','+tab.Field_Code+',')>0 and slan_name like '%golden%' and isnull(Doctor_Active_flag,0)=0 ORDER BY id asc";

		$det=performQuery($sql);
		outputJSON($det);
		break;
	case "get/LEOS":
		 $divisionCode = $_GET['divisionCode'];
		 if($divisionCode=="107,")
		$sql="select slno id,Allowance_Type_Name name,newname from Mas_Allowance_Types where division_code=107";
		else
			$sql="select slno id,Allowance_Type_Name name,newname from Mas_Allowance_Types";
		$det=performQuery($sql);
		if(count($det)==0){
			$div = $_GET['divisionCode'];
				$divs = explode(",", $div . ",");
				$Owndiv = (string) $divs[0];
			$sql="insert into Mas_Allowance_Types select 1,'HQ',$Owndiv,'HQ1'";
		performQuery($sql);
		$sql="insert into Mas_Allowance_Types select 2,'OS',$Owndiv,'OS1'";
				performQuery($sql);
				$sql="insert into Mas_Allowance_Types select 3,'EX',$Owndiv,'EX1'";
				performQuery($sql);
$sql="select slno id,Allowance_Type_Name name,newname from Mas_Allowance_Types";
		$det=performQuery($sql);
		}
		outputJSON($det);
		break;
    case "get/setup":
        outputJSON(getAPPSetups());
        break;
		case "getAppProdClosingRep":
		$sfCode = $_GET['sfCode'];
			$stockistid = $_GET['stockist_code'];
		$sql="exec getAppProdClosingRep '".$sfCode."',".$stockistid."";
		//$sql = "exec getAPPSetups '" . $sfCode . "'";
		$det=performQuery($sql);
		outputJSON($det);die;
		break;
	case "get/distributorRate":
		$div = $_GET['divisionCode'];
				$divs = explode(",", $div . ",");
				$Owndiv = (string) $divs[0];

				$sfCode = $_GET['sfCode'];
				$query = "select State_Code,sf_type from Mas_Salesforce where  Sf_Code='$sfCode'";
                $head = performQuery($query);
				$State_Code=$head[0]["State_Code"];
                $query = "select isnull(Distributor_Price, '0') SS_Base_Rate,isnull(SS_Case_Rate, '0') SS_Case_Rate,State_Code, Division_Code,Distributor_Price,Retailor_Price,DistCasePrice,
RetailCasePrice, MRP_Price, Product_Detail_Code from vwProductStateRates where Division_code='" . $Owndiv . "' and State_Code=$State_Code";
                $results = performQuery($query);

				$distQuery = "select Price_List_Name, Stockist_Code from Mas_stockist ms right join
                        vwstockiest_Master_APP vw on  vw.stockiest_code = ms.Stockist_Code
                        where Stockist_Status =0 and vw.SF_Code = '". $sfCode ."'";


                $distList =performQuery($distQuery);

            for($i=0;$i<count($results);$i++){
                $productCode = $results[$i]['Product_Detail_Code'];


                    for($j=0; $j<count($distList); $j++)
                    {
						$price = 0;
						$Rate_in_piece=0;
						$MRP_Price=0;
                        $priceListCode = $distList[$j]['Price_List_Name'];
                        if($priceListCode!=null && $priceListCode!=''){
                            $distRateQuery = "select pwd.dis_rate_in_piece,pwd.Rate_in_piece,pwd.MRP_Price, pwd.Product_Code, pwd.Product_Name from Mas_Product_Wise_Bulk_rate_head pwh inner join
                                              Mas_Product_Wise_Bulk_rate_details pwd on pwh.Price_list_Sl_No = pwd.Price_list_Sl_No
                                              where pwd.Product_Code ='". $productCode ."' and pwh.Price_list_Sl_No ='". $priceListCode ."'
											  and (convert(varchar,getdate(),23) <= Effective_From_Date or convert(varchar,getdate(),23)<=Effective_To_Date) ";

                            $distRate = performQuery($distRateQuery);
							if(count(distRate)>0){
							if($distRate[0]['dis_rate_in_piece']!=null && $distRate[0]['dis_rate_in_piece'] !='')
								$price = $distRate[0]['dis_rate_in_piece'];
							
							 if($distRate[0]['Rate_in_piece']!=null && $distRate[0]['Rate_in_piece'] !=''){
                                           $Rate_in_piece = $distRate[0]['Rate_in_piece'];
							              }
										  
										   if($distRate[0]['MRP_Price']!=null && $distRate[0]['MRP_Price'] !=''){
                                             $MRP_Price = $distRate[0]['MRP_Price'];
							              }
							}
                        }
							$distList[$j]['price'] = $price;
							 $distList[$j]['Rate_in_piece'] = $Rate_in_piece;
                             $distList[$j]['dis_rate_in_piece'] = $price;
                            $distList[$j]['MRP_Price'] = $MRP_Price;
							
							
							
							/*$price = 0;
                        $priceListCode = $distList[$j]['Price_List_Name'];
                        if($priceListCode!=null && $priceListCode!=''){
                            $distRateQuery = "select pwd.dis_rate_in_piece, pwd.Product_Code, pwd.Product_Name from Mas_Product_Wise_Bulk_rate_head pwh inner join
                                              Mas_Product_Wise_Bulk_rate_details pwd on pwh.Price_list_Sl_No = pwd.Price_list_Sl_No
                                              where pwd.Product_Code ='". $productCode ."' and pwh.Price_list_Sl_No ='". $priceListCode ."'
											  and (convert(varchar,getdate(),23) <= Effective_From_Date or convert(varchar,getdate(),23)<=Effective_To_Date) ";

                            $distRate = performQuery($distRateQuery);
							if(count(distRate)>0){
							if($distRate[0]['dis_rate_in_piece']!=null && $distRate[0]['dis_rate_in_piece'] !='')
								$price = $distRate[0]['dis_rate_in_piece'];
							}
                        }
							$distList[$j]['price'] = $price;*/
                    }

                    $results[$i]['distList'] = $distList;

              }
				outputJSON($results);
                break;
    case "imgupload":
        $sf = $_GET['sf_code'];
		/*if($sf=="MR3532")
        move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photostest/" . $sf . "_" . $_FILES["imgfile"]["name"]);
	else*/


	if (strpos($imgName, $sf) !== false)
		{
			$imgName = $_FILES["imgfile"]["name"];
		}else{
			$imgName = $sf . "_" . $_FILES["imgfile"]["name"];
		}

		move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photos/" . $imgName);


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
	case "get/subblockwise":
        outputJSON(getSubordinateblockwise());
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
	case "updateDispatchFlag":
 	$Trans_Detail_Slno= $_GET['Trans_Slno'];
	$query = "update Trans_Order_Head  set Order_Flag='1',Dispatch_Date= GETDATE() where Trans_Sl_No='". $Trans_Detail_Slno ."'";
    $insertQuery = "insert into Trans_OrderCNF_Head 
    select Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Order_Date,DCR_Code,Order_Value,Collected_Amount,Route,net_weight_value,Order_Flag,Remarks,discount_price,rateMode,CL_Stock,Div_ID,OrderID,OrderType,TDPercent,rejRemarks from Trans_Order_Head where Trans_Sl_No='". $Trans_Detail_Slno ."'";
	performQuery($query);
    performQuery($insertQuery);
	$result['success'] = true;
    outputJSON($result);
	break;
	case "updRejOrderFlag":
 	$Trans_Detail_Slno= $_GET['Trans_Slno'];
    $rejRemarks = $_GET['rejectedreason'];
	$query = "update Trans_Order_Head  set Order_Flag='2',Dispatch_Date= GETDATE(),rejRemarks='". $rejRemarks ."' where Trans_Sl_No='". $Trans_Detail_Slno ."'";
    $insertQuery = "insert into Trans_OrderCNF_Head 
    select Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Order_Date,DCR_Code,Order_Value,Collected_Amount,Route,net_weight_value,Order_Flag,Remarks,discount_price,rateMode,CL_Stock,Div_ID,OrderID,OrderType,TDPercent,rejRemarks from Trans_Order_Head where Trans_Sl_No='". $Trans_Detail_Slno ."'";
	performQuery($query);
    performQuery($insertQuery);
	$result['success'] = true;
    outputJSON($result);
	break;
	case "RetailerDynamicData":    
	    $sfcode = $_GET['sfcode'];
        $columns = $_GET['column'];
        $values = $_GET['values'];
        $retQuery = "select Top 1 ListedDrCode from Mas_ListedDr where Sf_Code = '$sfcode' order by ListedDr_Created_Date desc";
        $data = performQuery($retQuery);
        $retailer_code = $data[0] ['ListedDrCode'];
        //in the columns variable has the , so no needed to add comma in query
        //in the values variable has the ,'' so no needed to add in query
        $insertQuery = "insert into Trans_Retailer_Custom_Field (RetailerID$columns) select '$retailer_code'$values";
        performQuery($insertQuery);
		$resp["msg"]= "Inserted Successfully";
		$resp["query"]= $insertQuery;
		$resp["success"] = true;
        outputJSON($resp);
    break; 
    case "RouteDynamicData":
        $sfcode = $_GET['sfcode'];
        $columns = $_GET['column'];
        $values = $_GET['values'];
        $retQuery = "select Top 1 Territory_Code from Mas_Territory_Creation where Sf_Code = '$sfcode' order by Created_date desc";
        $data = performQuery($retQuery);
        $route_code = $data[0] ['Territory_Code'];
        //in the columns variable has the , so no needed to add comma in query
        //in the values variable has the ,'' so no needed to add in query
        $insertQuery = "insert into Trans_Route_Custom_Field (RouteID$columns) select '$route_code'$values";
        performQuery($insertQuery);
		$resp["msg"]= "Inserted Successfully";
		$resp["query"]= $insertQuery;
		$resp["success"] = true;
        outputJSON($resp);
        break; 
    case "DisDynamicData":
            $sfcode = $_GET['sfcode'];
            $columns = $_GET['column'];
            $values = $_GET['values'];
            $retQuery = "select Top 1 Stockist_Code from mas_stockist where Field_Code = '$sfcode' order by Created_date desc";
            $data = performQuery($retQuery);
            $dis_code = $data[0] ['Stockist_Code'];
            //in the columns variable has the , so no needed to add comma in query
            //in the values variable has the ,'' so no needed to add in query
            $insertQuery = "insert into Trans_Dis_Custom_Field (DisID$columns) select '$dis_code'$values";
            performQuery($insertQuery);
            $resp["msg"]= "Inserted Successfully";
            $resp["query"]= $insertQuery;
            $resp["success"] = true;
            outputJSON($resp);
        break;  		
	case "ManagerAttendanceData";
	$date = $_GET['date'];
	$sfcode= $_GET['sfcode'];
	$div_code= $_GET['division'];
	$TotalSizeQuery = "exec GET_Total_Userdashboard '$div_code','','$sfcode','$date'";
	$Cat_Lv_F_Ot_Query = "exec GET_Total_Userdashboard_Att '$div_code','','$sfcode','$date'";
	$result['TotalSize'] = performQuery($TotalSizeQuery);
	$result['CatWise'] = performQuery($Cat_Lv_F_Ot_Query);
	outputJSON($result);
	break;
	case  "get/performance":
        $sf_code = $_GET['sfcode'];
        $start_date = $_GET['startDate'];
        $end_date = $_GET['endDate'];
		$type = $_GET['Type'];
        if($type>0){
        $targetquery = "select SUM(targetVal) target_val,kk.sf_code,Reporting_To_SF from (select  t.SF_CODE,PRODUCT_CODE,s.Reporting_To_SF,sum((CASETARGET * cast(r.Distributor_Price as float))+(PieceTARGET * cast(r.MRP_Price as float))) as targetVal ,cast ( CAST( YEAR as varchar) +'/'+ cast( month as varchar) +'/1' as date) as tdate from PRODUCT_PRITARGET_MONTHLY T inner join mas_salesforce s on s.sf_code=t.sf_code inner join Mas_Product_State_Rates r on r.Product_Detail_Code=t.PRODUCT_CODE  and s.state_code=r.state_code where  t.SF_CODE in($sf_code) and cast(CAST(YEAR as varchar) +'/'+ cast(month as varchar) +'/1' as date) between '$start_date'  and '$end_date' group by  t.SF_CODE,PRODUCT_CODE, month,YEAR,s.Reporting_To_SF) kk where cast(convert(varchar, tdate,101)as datetime) between '$start_date' and '$end_date' and kk.SF_CODE in ($sf_code) group by kk.SF_CODE,Reporting_To_SF";
        $achievedquery = "select sum(order_value) order_value,ord.Sf_Code,ms.Reporting_To_SF from Trans_PriOrder_Head ord inner join Mas_Salesforce ms on ord.Sf_Code = ms.Sf_Code where ord.Sf_Code in ($sf_code) and  MONTH(Order_Date)='".date('m')."' and YEAR(Order_Date)='".date('Y')."' group by ord.Sf_Code,ms.Reporting_To_SF";
        }
		else{
        $targetquery = "select SUM(targetVal) target_val,kk.sf_code,Reporting_To_SF from (select  t.SF_CODE,PRODUCT_CODE,s.Reporting_To_SF, target, sum(target * mrp_price) targetVal ,cast(CAST(YEAR as varchar) +'/'+ cast(month as varchar) +'/1' as date)as tdate from PRODUCT_TARGET_MONTHLY T inner join mas_salesforce s on s.sf_code=t.sf_code inner join Mas_Product_State_Rates r on r.Product_Detail_Code=t.PRODUCT_CODE and s.state_code=r.state_code where  t.SF_CODE in($sf_code) and cast(CAST(YEAR as varchar) +'/'+ cast(month as varchar) +'/1' as date) between '$start_date'  and '$end_date' group by t.SF_CODE,PRODUCT_CODE,target,month,YEAR,s.Reporting_To_SF)kk where cast(convert(varchar,tdate,101)as datetime) between '$start_date'and'$end_date' and kk.SF_CODE in ($sf_code)group by kk.SF_CODE,Reporting_To_SF";
        $achievedquery = "select sum(order_value) order_value,ord.Sf_Code,ms.Reporting_To_SF from Trans_Order_Head ord inner join Mas_Salesforce ms on ord.Sf_Code = ms.Sf_Code where ord.Sf_Code in ($sf_code) and  MONTH(Order_Date)='".date('m')."' and YEAR(Order_Date)='".date('Y')."' group by ord.Sf_Code,ms.Reporting_To_SF";
		}
        $result['Target'] = performQuery($targetquery);
        $result['Achieved'] = performQuery($achievedquery);
        outputJSON($result);
    break;
	case "get/fieldForceData";
	$date = $_GET['date'];
	$sfcode = $_GET['sfcode'];
		$query = "select convert(varchar,h.Activity_Date,103) Adate,h.Trans_SlNo,SDP,
	        COUNT(Trans_Detail_Info_Code) TC,COUNT(distinct Cust_Code) UPC,COUNT(Cust_Code)PC,cast(isnull(sum(order_Value),0) as decimal) orderValue from DCRMain_Trans h
			inner join DCRDetail_Lst_Trans d on h.Trans_SlNo=d.Trans_SlNo 
			left outer join Trans_Order_Head o on DCR_Code=d.Trans_Detail_Slno and Cust_Code=Trans_Detail_Info_Code
			where h.Sf_Code in ($sfcode) and convert(date,h.Activity_Date)='$date' GROUP BY convert(varchar,h.Activity_Date,103),h.Trans_SlNo,SDP";
		$priquery = "select Order_Value,Order_No from Trans_PriOrder_Head where sf_code in ($sfcode) and CONVERT(date,Order_Date)='$date'";	
	    
	$output['SEC'] = performQuery($query);
	$output['PRI'] = performQuery($priquery);
	outputJSON($output);
	break;
	case "get/performance";
	$sfcode = _GET['sfcode'];
	$Owndiv = _GET['owndiv'];
    $query = "exec Get_FieldForce_Target_vs_Sal '" . $sfcode . "', '" . date("Y") . "','" .date('m') . "', '" .  date("Y") . "','" . date('m') . "' ,  '" . $Owndiv . "' ";
	$Target = performQuery($query);
	$result['month']['target_val'] = $Target[0]['target_val'];
	$result['month']['order_val'] = $Target[0]['order_val'];
	$result['month']['query'] = $query;
	outputJSON($result);
	break;
	case "get/travelmode":
	$div_code = $_GET['Division_Code'];
	$query = "Select * from Mas_Modeof_Travel where division_code='$div_code' and Active_Flag = '0'";
	outputJSON(performQuery($query));
	break;
         case "get/travel_approval":
            $sf_code = $_GET['sf_code'];
            $date = $_GET['date'];
            $div_code = $_GET['div_code'];
	    $dsg_code = $_GET['dsg_code'];
	    $state_code = $_GET['state_code'];
            $query = "exec GetTravelApproval  '".$sf_code."','".$date."','".$div_code."','".$state_code."','".$dsg_code."'";
            $result = performQuery($query);
            outputJSON($result);
        break;
case "send_forApproval":
	    $sf_code = $_GET['sf_code'];
        $period_id = $_GET['period_id'];	
        $query = "exec Update_Expense_ApprovalFlag '$sf_code','$period_id'";
		$result['msg'] = performQuery($query);
        $result["query"] = $query;
        $result["success"] = true;	
        outputJSON($result);		
	break;
case "get/periodicWise":
         $div_code = $_GET['div_code'];
         $mon = $_GET['month'];
         $year = $_GET['year'];
         $query = "exec get_periodic_list '$div_code','$mon','$year'";
         $result['query'] = $query;
         $result['data'] = performQuery($query);
         outputJSON($result);
    break;
case "get/new_DateofExpense":
	$sfCode = $_GET['rSF'];
    $eDt = $_GET['Dateofexp'];
	$div_code = $_GET['div_code'];
	$Type = $_GET['Type'];
	$query ="update tb set tb.ClstrName=SDP_Name,tb.cluster=SDP from  TbMyDayPlan tb inner join   DCRDetail_Lst_Trans  dc on dc.sf_code=tb.sf_code where tb.sf_code='" . $sfCode . "' and ClstrName='Missed Entry'  and convert(date,tb.Pln_Date)=convert(date,dc.ModTime)";
	performQuery($query);
	
	if($Type=='1'){
  $query = "select case when   (WorkType='Meeting' or  WorkType='Admin Work' or WorkType='Transit' ) and ClstrName=''  and  Division_Code='109' then 'HQ' ELSE Allowance_Type end Allowance_Type,ClstrName,WorkType  from (select top 1 isnull(ClstrName,'') ClstrName ,Division_Code, (select  isnull(Allowance_Type,'HQ') from Mas_Territory_Creation where   CONVERT(VARCHAR(50),Territory_Code)=isnull(TB.cluster,'')) Allowance_Type , (select Worktype_Name_B from   Mas_WorkType_BaseLevel where WorkType_Code_B=TB.wtype ) WorkType from TbMyDayPlan    TB  where TB.sf_code='" . $sfCode . "' and convert(date,TB.Pln_Date)='" . $eDt . "'
  and (select Worktype_Name_B from Mas_WorkType_BaseLevel where WorkType_Code_B=TB.wtype)!= 'Weekly Off' order by TB.Pln_Date ASC ) A ";
 }else{
   $query = "select case when   (WorkType='Meeting' or  WorkType='Admin Work' or WorkType='Transit') and ClstrName='' and  Division_Code='109'  then 'HQ' ELSE Allowance_Type end Allowance_Type,ClstrName,WorkType  from (select top 1 ClstrName,Division_Code,  isnull((select  Allowance_Type from Mas_Territory_Creation where CAST(Territory_Code AS varchar(50))=isnull(TB.cluster,'')),'HQ') Allowance_Type,(select Worktype_Name_M from   Mas_WorkType_Mgr where WorkType_Code_M=TB.wtype ) WorkType from TbMyDayPlan  TB  where TB.sf_code='" . $sfCode . "' and convert(date,TB.Pln_Date)='" . $eDt . "' order by TB.Pln_Date desc) A ";
 }
 $per_query = "select Top 1 period_id,period_name,from_date,iif(to_date = 'end of month',day((EOMONTH('$eDt'))),to_date)to_date,eff_month,eff_year,division_code  from Mas_Expense_Calender where
(To_Date = 'end of month'and Division_code = '$div_code' and Eff_Month <= month('$eDt') and Eff_year <= year('$eDt') and From_Date <= day('$eDt') and day(EOMONTH('$eDt')) >=day('$eDt'))
or (Division_code = '$div_code' and Eff_Month <=month('$eDt') and Eff_year <= year('$eDt')and From_Date <= day('$eDt') and To_Date >= day('$eDt')) order by Eff_Month desc";
 $result["day_plan"] = performQuery($query);
 $result["period_wise"] = performQuery($per_query);
 outputJSON($result);
        break;
        case "post/endDayExpense":
            $sf_code = $_GET['sf_code'];
            $date_time=  $_GET['date_time'];
            $date= $_GET['date'];
            $end_km = $_GET['endKM'];
            $end_remarks = $_GET['endRemarks'];
            $personal_km = $_GET['personalKM'];
            $end_imgUrl = $_GET['imageUrl'];
            $end_modId = $_GET['endModID'];
            $query = "update Expense_Start_Activity set Enddateand_time='$date_time',End_Km='$end_km',End_Image_Url='$end_imgUrl',End_remarks='$end_remarks'
            ,End_MOT='$end_modId',Personal_Km='$personal_km' where Sf_code='$sf_code' and convert(date,ExpenseDate)='$date'";
            performQuery($query);
            $result['success'] = true;
            outputJSON($result);
         break; 
        case "get/srtExpenseData":
            $sf_code = $_GET['sf_code'];
            $date = $_GET['date'];
            $query = "select * from EXpense_Start_Activity where convert(date,ExpenseDate)='$date' and sf_code= '$sf_code'";
            $result = performQuery($query);
            outputJSON($result);
        break;
	case "get/CustomDetails";
	$div_code = $_GET['divisionCode'];
	$moduleId = $_GET['moduleId'];
	$query = "select ModuleId ModuleId,Field_Name Field_Name,Fld_Type Fld_Type,Fld_Symbol Fld_Symbol,Field_Col Field_Col,Fld_Length Fld_Length,Mandate Mandate,flag flag,Fld_Src_Name Fld_Src_Name,Fld_Src_Field Fld_Src_Field
    from Trans_Custom_Fields_Details where Div_code = '$div_code' and ModuleId = '$moduleId'";
	$result = performQuery($query);
	outputJSON($result);
	break;
	case "get/oldlocations":
	$sf_code = $_GET['sfcode'];
	$date = $_GET['date'];
	$query = "select  tl.Sf_Code sfcode, ms.Sf_Name username,MAX(tl.Lat) lat,MAX(tl.Lon) long,MAX(tl.DtTm) lastdate,MAX(ms.Profile_Pic) profilepic from Mas_Salesforce ms with (nolock)
inner join tbTrackLoction  tl with (nolock) on tl.SF_code = ms.Sf_Code
where ms.Sf_Code in ($sf_code) and convert(date,tl.DtTm)='".$date."' group by tl.SF_code,ms.Sf_Name";
	$result['result'] = performQuery($query);
	$result['query'] = $query;
	outputJSON($result);
	break;
	case "get/locations":
        $sf_code = $_GET['sfcode'];
        $date = $_GET['date'];
        $user_field_list = array();
        $field_query = "select tbl.sf_code,tbl.Pln_Date,Tb.FWFlg from (select max(rw)rw,t.sf_code,Max(t.Pln_Date)Pln_Date from 
        (select ROW_NUMBER() over(partition by TB.Sf_Code order by TB.Pln_Date asc)rw,TB.* from TbMyDayPlan TB 
        where CONVERT(date,Pln_Date)='".$date."' and sf_code in ($sf_code)) as t
        Group by t.sf_code) as tbl inner join TbMyDayPlan TB on tbl.Pln_Date=TB.Pln_Date";
        $Users_FieldWork = performQuery($field_query);
        $result['data'] = $Users_FieldWork;
        if(count($Users_FieldWork)>0){
            $size = count($Users_FieldWork);
			$result['size'] = $size;
            for($i=0;$i<count($Users_FieldWork);$i++){
                if($Users_FieldWork[$i]['FWFlg'] !== 'W' && $Users_FieldWork[$i]['FWFlg'] !== 'H' && $Users_FieldWork[$i]['FWFlg'] !== 'L'){
                    if($size == $i){
                        $all_sfcodes .= "'".$Users_FieldWork[$i]['sf_code']."'";
                    }
                    else{
                      $all_sfcodes .= "'".$Users_FieldWork[$i]['sf_code']."',";
                    }
                }
            }
			 $all_sfcodes = substr_replace($all_sfcodes, "", -1);
            $query = "select  tl.Sf_Code sfcode, ms.Sf_Name username,MAX(tl.Lat) lat,MAX(tl.Lon) long,MAX(tl.DtTm) lastdate,MAX(ms.Profile_Pic) profilepic from Mas_Salesforce ms with (nolock) inner join tbTrackLoction  tl with (nolock) on tl.SF_code = ms.Sf_Code where ms.Sf_Code in ($all_sfcodes) and convert(date,tl.DtTm)='".$date."' group by tl.SF_code,ms.Sf_Name";
        }
        $result['result'] = performQuery($query);
        $result['query'] = $query;
        outputJSON($result);
        break;
    case "get/manager_performance":
        $sf_code = $_GET['sfcode'];
        $divCode = $_GET['Div_code'];
        $date = $_GET['date'];
        $type = $_GET['Type'];
        $sec_or_pri = $_GET['sec_or_pri'];
        if($sec_or_pri>0){
            $targetquery = "exec manager_target_pri '$sf_code',$divCode,'$date','$type'";
            $achievedquery = "exec manager_performance_pri '$sf_code',$divCode,'$date','$type'";
        }
        else{
            $targetquery = "exec manager_target_sec '$sf_code',$divCode,'$date','$type'";
            $achievedquery = "exec manager_performance_sec '$sf_code',$divCode,'$date','$type'";
        }
        $result['Target'] = performQuery($targetquery);
        $result['Achieved'] = performQuery($achievedquery);
        outputJSON($result);
    break;
	 case "get/CoverageDetails":
        $sfcode = $_GET['sfcode'];
        $fromDate = $_GET['fromdate'];
        $toDate = $_GET['todate'];
        $totRetail = "select count(distinct mld.ListedDrCode)total_retailer from Mas_SalesForce ms with(nolock) 
        inner join Mas_ListedDr mld with(nolock)  on  charindex(','+ms.Sf_Code+',',','+mld.Sf_Code+',')>0  
        where ms.Sf_Code in ($sfcode) and ISNULL(mld.ListedDr_Active_Flag,0) = '0'";
        $totRoute = "select count(distinct mt.Territory_Code)total_route from Mas_SalesForce ms with(nolock) 
        inner join Mas_Territory_Creation mt with(nolock)  on  charindex(','+ms.Sf_Code+',',','+mt.SF_Code+',')>0  
        where ms.Sf_Code in ($sfcode) and ISNULL(mt.Territory_Active_Flag,0) = '0'";
        $totDis = "select count(distinct dis.Stockist_Code)total_dis from Mas_SalesForce ms with(nolock) 
        inner join Mas_Stockist dis with(nolock)  on charindex(','+ms.Sf_Code+',',','+dis.Field_Code+',')>0  			
        where ms.Sf_Code in ($sfcode) and ISNULL(dis.Stockist_Active_Flag,0) = '0'";
        $newRetail = "select count(distinct mld.ListedDrCode)new_retailer from Mas_SalesForce ms with(nolock) 
        inner join Mas_ListedDr mld with(nolock)  on  charindex(','+ms.Sf_Code+',',','+mld.Sf_Code+',')>0  
        where convert(date,ListedDr_Created_Date) between '$fromDate' and '$toDate' and ms.Sf_Code in ($sfcode)";
        $newRoute = "select count(distinct mt.Territory_Code)new_route from Mas_SalesForce ms with(nolock) 
        inner join Mas_Territory_Creation mt with(nolock) on charindex(','+ms.Sf_Code+',',','+mt.SF_Code+',')>0  
        where convert(date,mt.Created_date) between '$fromDate' and '$toDate' and ms.Sf_Code in ($sfcode)";
        $newDis = "select count(distinct dis.Stockist_Code)new_dis from Mas_SalesForce ms with(nolock) 
        inner join Mas_Stockist dis with(nolock)  on charindex(','+ms.Sf_Code+',',','+dis.Field_Code+',')>0  			
        where convert(date,dis.Created_Date) between '$fromDate' and '$toDate' and ms.Sf_Code in ($sfcode)";
        $visit_Details = "select count(distinct stockist_code)dis,count(distinct SDP)rout,count(distinct Trans_Detail_Info_Code)Ret
         from vwActivity_Msl_Details ad with(nolock) inner join Mas_Salesforce ms on ad.sf_code=ms.Sf_Code  where 
         convert(date,Activity_Date)between '$fromDate' and '$toDate' and ms.Sf_Code in ($sfcode)";
        $result['totRetail'] = performQuery($totRetail);
        $result['totRoute'] = performQuery($totRoute);
        $result['totDis'] = performQuery($totDis);
        $result['newRetail'] = performQuery($newRetail);
        $result['newRoute'] = performQuery($newRoute);
        $result['newDis'] = performQuery($newDis);
        $result['visit_Details'] = performQuery($visit_Details);
        outputJSON($result);
        break;
	case "get/circularlist":
	    $divisionCode = $_GET['divisioncode'];
        $query = "select FileName,FileSubject,ID from file_info where Div_Code = '$divisionCode'";
        $output = performQuery($query);
        $dir = dirname(dirname(__FILE__))."\MasterFiles\Files\Circular";
        if (count($output) > 0) {
            for ($i = 0;$i < count($output);$i++) {
                   $file_name = $output[$i]['FileName'];
                   $path = realpath($dir.DIRECTORY_SEPARATOR.$file_name);
                   $results[] = array('FileName'=>$output[$i]['FileName'],'FileSubject'=>$output[$i]['FileSubject'],'ID'=>$output[$i]['ID'],'FileSize'=>filesize($path),'FilePath'=>$path);
            }
        } else {
            $results = null;
        }
         outputJSON($results);
        break;
	case "CircularDownloadInfo":
        $CircularID = $_GET['circularid'];
        $SfCode = $_GET['sfcode'];
        $Date = $_GET['date'];
        $FileName = $_GET['filename'];
        $query = "insert into Circular_DownloadInfo (CircularID,SfCode,Date,FileName) values ('$CircularID','$SfCode','$Date','$FileName')";
        $output = performQuery($query);
        $result['output'] = $output;
		outputJSON($result);
        break;	
	case "get/Monthly_ItemWise":
        	$Date = $_GET['date'];
			$SfCode = $_GET['sfcode'];
			$Type = $_GET['type'];
			if($Type == 1){
			$query = "select d.Product_Name Product_Name,d.Quantity Qty,d.Free,h.Order_Date,h.Order_Value OrderVal from Trans_Order_Head h
                      inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No
                      where Convert(varchar(7),h.Order_Date,126)= '$Date' and h.Sf_Code= '$SfCode' order by h.Order_Date asc";
			}
			else {
			$query = "select d.Product_Name Product_Name,d.CQty Qty,d.Free,h.Order_Date,h.Order_Value OrderVal from Trans_PriOrder_Head h
                      inner join Trans_PriOrder_Details d on h.Trans_Sl_No=d.Trans_Sl_No
                      where Convert(varchar(7),h.Order_Date,126)= '$Date' and h.Sf_Code= '$SfCode' order by h.Order_Date asc";
			}
			$result = performQuery($query);
			outputJSON($result);
			break;
    case "get/BrndSumm":
        $data = json_decode($_POST['data'], true);
        $desig = $data['desig'];
        outputJSON(getBrandWiseProd($desig));
        break;
 case "get/tpdetails":
            $stateCode = $_GET['State_Code'];

$sfCode=$_GET['rSF'];
$divisionCode=$_GET['divisionCode'];

	     $query1 = "select  TOP 1 convert(varchar,cast(convert(varchar,Tour_Date,101)  as date),20) as date,Objective remarks,SF_Code,WorkType_Code_B worktype_code,Worktype_Name_B worktype_name,Tour_Schedule1 RouteCode,Territory_Code1 RouteRouteNameCode,Territory_Code1 RouteName,Worked_With_SF_Code Worked_with_Code,Worked_With_SF_Name Worked_with_Name,Confirmed,HQ_Code,HQ_Name,JointWork_Name,MONTH(Tour_Date) Tmonth,YEAR(Tour_Date) Tyear,Retailer_Code,Retailer_Name,isnull(Place_Inv,'')Place_Inv from Trans_TP where SF_Code='$sfCode' and cast(tour_date as date)=cast(getdate() as date)";
         $results = performQuery($query1);
		 if($divisionCode=="156,")
		  $query = "select  TOP 1 *  from DCR_MissedDates where SF_Code='$sfCode' and cast(dcr_missed_date as date)=cast(getdate() as date) and status=4 order by Status desc";
			 else
		  $query = "select  TOP 1 *  from DCR_MissedDates where SF_Code='$sfCode' and cast(dcr_missed_date as date)=cast(getdate() as date) order by Status desc";
         $status = performQuery($query);
if($status[0]['Status']=="6"){
	$query = "select  TOP 1 isnull(Rejection_Reason,'Deviation Rejected but reason not given') Rejection_Reason from Deviation_Rejection_Reason where SF_Code='$sfCode' and cast(dcr_missed_date as date)=cast(getdate() as date) order by dcr_missed_date desc";
         $reason = performQuery($query);
		 if(count($reason)==0)
			 $status[0]['reject_reason']="Deviation Rejected but reason not given";
		 else
		 $status[0]['reject_reason']=$reason[0]['Rejection_Reason'];
}
		 $res=array();
				$res['status']=$status;
				$res['tp']=$results;
				$res['tpQ']=$query1;
                outputJSON($res);

                die;
                break;
				 case "get/tpdetails_mand":
           $stateCode = $_GET['State_Code'];

$sfCode=$_GET['rSF'];
$divisionCode=$_GET['divisionCode'];
                $CMn=$_GET['CMonth'];
                $CYr=$_GET['CYr'];
				 $PMn=$_GET['PMonth'];
                $PYr=$_GET['PYr'];
              //  $query = "exec TourPlan_Holyday_Auto_Insert '$sfCode' ,'$CMn','$CYr',' $stateCode'";
              //  $resultss = performQuery($query);

	     $query = "select  TOP 1*  from Trans_TP where SF_Code='$sfCode' and Tour_Month=month(getdate()) and Tour_Year=year(getdate())  and
Worktype_Name_B!='Holiday' and Worktype_Name_B!='Leave' and Worktype_Name_B!='Weekly Off' UNION
               select  TOP 1*  from Trans_TP_One where SF_Code='$sfCode' and Tour_Month=month(getdate()) and Tour_Year=year(getdate()) and Confirmed=1 and
Worktype_Name_B!='Holiday' and Worktype_Name_B!='Leave' and Worktype_Name_B!='Weekly Off' ";

                $current = performQuery($query);
				 $query = "select  TOP 1*  from Trans_TP where SF_Code='$sfCode' and MONTH(Tour_Date) = MONTH(dateadd(MONTH,1,getdate())) and YEAR(Tour_Date) = YEAR(dateadd(MONTH,1,getdate()))  and
Worktype_Name_B!='Holiday' and Worktype_Name_B!='Leave' and Worktype_Name_B!='Weekly Off' UNION
               select  TOP 1*  from Trans_TP_One where SF_Code='$sfCode' and MONTH(Tour_Date) = MONTH(dateadd(MONTH,1,getdate())) and YEAR(Tour_Date) = YEAR(dateadd(MONTH,1,getdate())) and Confirmed=1 and
Worktype_Name_B!='Holiday' and Worktype_Name_B!='Leave' and Worktype_Name_B!='Weekly Off' ";

                $next = performQuery($query);
				$res=array();
				$res['current']=$current;
				$res['next']=$next;
                outputJSON($res);


                die;
                break;
    case "get/BrndSummLitters":
        $data = json_decode($_POST['data'], true);
        $desig = $data['desig'];
        outputJSON(getBrandLittersWiseProd($desig));
        break;
	case "get/RejectedOrders":
		$fromDate = $_GET['From_Date'];
		$toDate = $_GET['To_Date'];
		$sfCode = $_GET['Sf_Code'];
		$query = "select Trans_Sl_No,Order_Value,isnull(j.Stockist_Name,'') stockist_name,Trans_Detail_Name,Activity_Date,Order_Flag from DCRMain_Trans h with(nolock) inner join DCRDetail_Lst_Trans d on h.Trans_SlNo = d.Trans_SlNo inner join Trans_Order_Head k on k.DCR_Code = d.Trans_Detail_Slno left outer join Mas_Stockist j on j.Stockist_Code = k.Stockist_Code where h.sf_code ='".$sfCode."' and Activity_Date BETWEEN '".$fromDate."'and '".$toDate."' and Order_Flag > 0 order by Activity_Date asc";
		$result = performQuery($query);
        outputJSON($result);
		break;
	case "get/selectedordersdetail":
	   $trans_details_slno = $_GET['Trans_Details_SlNo'];
	   $query = "select * from vwActivity_MSL_Details where Order_No = '".$trans_details_slno."'";
	   $result = performQuery($query);
	   outputJSON($result);
		break;
	case "get/callpreviewhierarchy":
		$trans_slno = $_GET['Trans_SlNo'];
		$screentype = $_GET['type'];
		if($screentype == 1){
		$query = "select * from Trans_PriOrder_Details d inner join Mas_Product_Detail h on d.Product_Code = h.Product_Detail_Code where d.Trans_Sl_No = '".$trans_slno."' order by h.Product_Code_SlNo ";	
		}
		else{
		$query = "select * from Trans_Order_Details d inner join Mas_Product_Detail h on d.Product_Code = h.Product_Detail_Code where d.Trans_Sl_No = '".$trans_slno."' order by h.Product_Code_SlNo ";
		}
		$result = performQuery($query);
		outputJSON($result);
		break;
	case "getDiscType":
            $query = "select * from discType";
            $result = performQuery($query);
            outputJson($result);
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
			case "vwactivity_msl_details":
		     $sfCode = $_GET['Sf_Code'];
		     $query = $query = "select Order_Flag,Trans_Sl_No,Order_Value,Activity_Date,Trans_Detail_Name,j.Stockist_Name stockist_name,OrderType OrderTyp from DCRMain_Trans h with(nolock) 
             inner join DCRDetail_Lst_Trans d on h.Trans_SlNo = d.Trans_SlNo 
             inner join Trans_Order_Head k on k.DCR_Code = d.Trans_Detail_Slno 
             left outer join Mas_Stockist j on j.Stockist_Code = k.Stockist_Code
             where h.sf_code ='".$sfCode."' and Activity_Date > DATEADD (MONTH,-2,GETDATE()) and ISNULL(k.Order_Flag,0) =0  order by Activity_Date asc
             ";
	         $result = performQuery($query);
		     outputJSON($result);
			 break;;
            case "vwproductstaterates":
                $State_Code = $_GET['State_Code'];
				$sfCode = $_GET['sfCode'];
				$query = "select State_Code,sf_type from Mas_Salesforce where  Sf_Code='$sfCode'";
                $head = performQuery($query);
				$State_Code=$head[0]["State_Code"];
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
                     $sfCode = $_GET['sfCode'];
					 $query = "select State_Code,sf_type from Mas_Salesforce where  Sf_Code='$sfCode'";
                $head = performQuery($query);
				if($head[0]["sf_type"]=="2"){
						$state=$head[0]["State_Code"];
					}

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

				 $divCode = $_GET['divisionCode'];
	  if( $divCode=="156," ||  $divCode=="156"){
		  $results = performQuery($query);
	  }
	  else{
		    $results = performQuery($query);
                if ($data['desig'] != stockist) {
                    $dummy = Array(
                        "id" => -1,
                        "name" => "Promoted Products"
                    );
                    array_unshift($results, $dummy);
                }
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
                $query = "select Doc_Cat_Code id,Doc_Cat_Name name from Mas_Doctor_Category where Division_code='" . $Owndiv . "' and Doc_Cat_Active_Flag=0 order by Doc_Cat_Name";
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

                //$query = "select  * from vwTourPlan where SF_Code='$sfCode' and Tmonth=$CMn and Tyear=$CYr";
				$query = "select  t.Rejection_Reason, CONVERT(VARCHAR(20),m.Sf_Joining_Date,111) joining_date,* from vwTourPlan v inner join Mas_Salesforce m on m.Sf_Code = v.SF_Code left join TP_Reject_B_Mgr t on v.Sf_Code = t.Sf_Code and v.Tmonth = t.Tour_Month and v.Tyear = t.Tour_Year
where v.SF_Code='$sfCode' and v.Tmonth=$CMn and v.Tyear=$CYr";


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


                $query = "select  TOP 1*  from Trans_TP where SF_Code='$sfCode' and Tour_Month=$CMn and Tour_Year=$CYr and Confirmed=1 or Confirmed=3 and
Worktype_Name_B!='Holiday' and Worktype_Name_B!='Leave' and Worktype_Name_B!='Weekly Off' UNION
               select  TOP 1*  from Trans_TP_One where SF_Code='$sfCode' and Tour_Month=$CMn and Tour_Year=$CYr and Confirmed=1 and
Worktype_Name_B!='Holiday' and Worktype_Name_B!='Leave' and Worktype_Name_B!='Weekly Off' ";




                $results = performQuery($query);
                outputJSON($results);

                die;
                break;



            case "vwleavetype":
                $query = "select Leave_Code id,Leave_SName name,Leave_Name from vwLeaveType where Division_code='" . $Owndiv . "' and active_flag=0";
                $results = performQuery($query);
				outputJSON($results);
				die;
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

					if($tableName == "vwMyDayPlan" && ($results==null || count($results) == 0))
				{
					//print($tableName=="vwMyDayPlan");
					$todayDate = date('Y-m-d');

					$query = "select remarks, RouteName as ClstrName, Worked_with_Code as stockistid, Place_Inv,
						  worktype_code as worktype, RouteCode as clusterid, JointWork_Name as worked_with_code,
						  HQ_Code as subordinateid, HQ_Name,date as name,convert(varchar,date,20) plnDate,'' custId,'' custName
						  from vwTourPlan  where SF_Code = '". $sfCode ."' and date = '". $todayDate ."'";

						$results = performQuery($query);
						if(count($results)>0){
							$results[0]['tourplanDone'] = 1;

							$desig = 'MR';

							if($_GET['desig']!=null)
							    $desig = $_GET['desig'];

							$FWFlgQuery = "select top 1 FWFlg from vwMas_WorkType_App where type_code ='". $results[0]['worktype']."' and typ = '". $desig ."'";

							$FWFlg = performQuery($FWFlgQuery);

							 $results[0]['FWFlg'] = $FWFlg[0]['FWFlg'];

							$routeName = $results[0]['ClstrName'];



							if(substr_count($routeName, "$$")>0){
								$routeNameArray = explode("$$", $routeName);
								for($i =0; $i<count($routeNameArray); $i++ ){
									if($routeNameArray[$i] !=null && $routeNameArray[$i] !=''){
										$results[0]['ClstrName'] = $routeNameArray[$i];
										break;
									}
								}

							}

							$routeId = $results[0]['clusterid'];


							if(substr_count($routeId, "$$")>0){
								$routeIdArray = explode("$$", $routeId);
								for($i =0; $i<count($routeIdArray); $i++ ){
									if($routeIdArray[$i] !=null && $routeIdArray[$i] !='' ){
										$results[0]['clusterid'] = $routeIdArray[$i];
										break;
									}
								}

							}


							$results[0]['name'] = array(

							"date" => $results[0]['name'] . " 00:00:00"

								);
							}
							}


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
		$sec="0";

        $arc = (isset($_GET['arc']) && strlen($_GET['arc']) == 0) ? null : $_GET['arc'];
        $amc = (isset($_GET['amc']) && strlen($_GET['amc']) == 0) ? null : $_GET['amc'];
        if (count($data[2]['Activity_Sample_Report']) != 0)
            orderDetailsDelete($sfCode, $custCode,$arc, $amc,$sec);
        deleteEntry($arc, $amc);
        addEntry();
        break;
		case "dcr/updatePrimaryProducts":
		$data = json_decode($_POST['data'],true);
		$DCRCode = $data['DCR_Code'];
		 $orderDate = date("Y-m-d H:i");
		  $collectedAmount = $data['POB'];
                $Discountpercent=$data['Discountpercent'];
		 $stockistCode = $data['Stockist'];
                 $routeTarget = $data['target'];
                 $transOrderDetails = $data['Products'];
		 $Trans_Sl_No = $data['Trans_Sl_No'];
                 $orderValue = $data['Value'];
		 $sfCode = $_GET['sfCode'];
                 $orderNo = $data['order_No'];
		$Trans_Detail_slNo=$data['Trans_Detail_slNo'];
		$submission_date=$data['dcr_activity_date'];
		$checkoutTime=$data['CheckoutTime'];
		$modified_time=$data['modified_time'];
		$dis_type='';

		 if($data['rateMode']==null)
            $rateMode="Nil";
        else
            $rateMode = $data['rateMode'];
        if($data['discount_price']==null)
            $discount_price="0";
        else
			$discount_price = $data['discount_price'];
		$query="select Submission_Date from DCRMain_Trans where Trans_SlNo='" . $Trans_Sl_No . "'";
		performQuery($query);
		$query="update DCRMain_Trans set Submission_Date='".$orderDate."' where  Trans_SlNo='" . $Trans_Sl_No . "'" ;
		performQuery($query);
		$query="Update Trans_priorder_head set Order_Date='".$orderDate."',Order_Value='".$orderValue."' where DCR_Code='".$DCRCode."' AND Order_No='".$orderNo."'";
		performQuery($query);
		$sql = "delete from Trans_PriOrder_Details WHERE Trans_Sl_No='".$orderNo."'";
                performQuery($sql);
		 for ($i = 0; $i < count($transOrderDetails); $i++) {
			 $rx_qty = $transOrderDetails[$i]['rx_Conqty'];
		     $product_code=$transOrderDetails[$i]['product_code'];
			 $product_Name=$transOrderDetails[$i]['product_Name'];
			 $free = $transOrderDetails[$i]['free'];
			 $CQty = $transOrderDetails[$i]['Qty'];
			  $PQty = $transOrderDetails[$i]['PQty'];
			  $cb_qty=$transOrderDetails[$i]['cb_qty'];
			  $Rate=$transOrderDetails[$i]['Rate'];
			  $discount=$transOrderDetails[$i]['discount'];
			  $FreeP_Code=$transOrderDetails[$i]['FreeP_Code'];
			  $Fname=$transOrderDetails[$i]['Fname'];
			  $discount_price=$transOrderDetails[$i]['discount_price'];
			  $tax=$transOrderDetails[$i]['tax'];
			  $tax_price=$transOrderDetails[$i]['tax_price'];
		          $OrdConv=$transOrderDetails[$i]['OrdConv'];
		          $product_unit_name=$transOrderDetails[$i]['product_unit_name'];
			  $selectedScheme=$transOrderDetails[$i]['selectedScheme'];
			  $selectedOffProCode=$transOrderDetails[$i]['selectedOffProCode'];
			  $selectedOffProName=$transOrderDetails[$i]['selectedOffProName'];
			  $selectedOffProUnit=$transOrderDetails[$i]['selectedOffProUnit'];
			  $value=$transOrderDetails[$i]['sample_qty'];
			  $Trans_POrd_No=$transOrderDetails[$i]['Trans_POrd_No'];
			  $Order_Flag=$transOrderDetails[$i]['Order_Flag'];
			  $Division_code=$transOrderDetails[$i]['Division_code'];
                    $sqlp="select isnull(Max(cast(Trans_POrd_No as int)),0)+1 MxD from Trans_PriOrder_Details";
			  $twp=performQuery($sqlp);
			  $pkp =$twp[0]['MxD'];

			  $query="insert into Trans_PriOrder_Details Values('".$pkp."','".$orderNo."','".$product_code."','".$product_Name."','".$transOrderDetails[$i]['Qty']."','".$PQty."','".$value."','".$Rate."','".$Order_Flag."','".$Division_code."','".$cb_qty."','".$free."','".$discount_price."','".$discount."','0','0','','".$product_unit_name."','".$tax_price."','','','".$rx_qty."','".$OrdConv."')";
			  performQuery($query);
			  $SPProds = $SPProds . $transOrderDetails[$i]['product_code'] . "~" .$transOrderDetails[$i]['Qty'] . "#";
                          $nSPProds = $nSPProds . $transOrderDetails[$i]['product_Name'] . "~" .$transOrderDetails[$i]['Qty']. "#";



			}
			  $query="Update DCRDetail_CSH_Trans set Additional_Prod_Code='".$SPProds."',Additional_Prod_Dtls='".$nSPProds."',vstTime='".$orderDate."',ModTime='".$orderDate."',POB_Value='".$orderValue."' where  Trans_SlNo='" . $Trans_Sl_No . "' AND Trans_Detail_Slno='".$Trans_Detail_slNo."'";
			  performQuery($query);
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
        $orderNo = '0';
        $orderDate = date("Y-m-d H:i");
        $DCRCode = $data['DCR_Code'];
		$taxTotalValue = $data['taxTotalValue'];
	    $discTotalValue = $data['discTotalValue'];
	    $subTotal = $data['subTotal'];
	    $no_of_items = $data['No_Of_items'];
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
		if($pobValSumm==null || $pobValSumm=='')
			$pobValSumm = 0;

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

       /* $sql = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value,discount_price,rateMode,CL_Stock,Div_ID,OrderID,OrderType,TDPercent,Order_Out_Time) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue,'" . $orderDate . "', $townCode,'" . $DCRCode . "',$net_weight_value,'".$discount_price."','$rateMode','" . $CL_Stock."','". $Div_ID."','" .$OrderID."','".$OrderType."','".$Discountpercent."','" . $orderDate . "'";*/
	   $sql = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value,discount_price,rateMode,CL_Stock,Div_ID,OrderID,OrderType,TDPercent,Order_Out_Time,Sub_Total,Dis_Total,Tax_Total,No_Of_items) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue,'" . $orderDate . "', $townCode,'" . $DCRCode . "',$net_weight_value,'".$discount_price."','$rateMode','" . $CL_Stock."','". $Div_ID."','" .$OrderID."','".$OrderType."','".$Discountpercent."','" . $orderDate . "','".$subTotal."','".$discTotalValue."','".$taxTotalValue."','".$no_of_items."'";

       performQuery($sql);
        for ($i = 0; $i < count($transOrderDetails); $i++) {

			$productremarks=$transOrderDetails[$i]['rx_remarks'];
            $productremarks_Id=$transOrderDetails[$i]['rx_remarks_Id'];

             $remarkspd = 0;
			 $qty = $transOrderDetails[$i]['rx_qty'];
			 $free = $transOrderDetails[$i]['free'];
			 $conversionQty=$transOrderDetails[$i]['conversionQty'];
			    if($transOrderDetails[$i]['cb_qty']==null)
                       $cbQty=0;
                    else
                       $cbQty=$transOrderDetails[$i]['cb_qty'];

				    if($transOrderDetails[$i]['Ccb_qty']==null)
                       $CcbQty=0;
                    else
                       $CcbQty=$transOrderDetails[$i]['Ccb_qty'];
				    if($transOrderDetails[$i]['UnitId']==null)
                           $UnitId=0;
                        else
                           $UnitId=$transOrderDetails[$i]['UnitId'];
					   if($transOrderDetails[$i]['rx_Conqty']==null)
                           $rx_Conqty=0;
                        else
                           $rx_Conqty=$transOrderDetails[$i]['rx_Conqty'];
					   if($transOrderDetails[$i]['UnitName']==null)
                           $UnitName='';
                        else
                           $UnitName=$transOrderDetails[$i]['UnitName'];

				   $Promo=$transOrderDetails[$i]['PromoVal'];
			   if($qty>0 || $free>0   || $cbQty>0 || $Promo>0   || $CcbQty>0 || $productremarks!==$remarkspd){


            /* $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Order_Details";
            $tRw = performQuery($sql);
            $pk1 = (int) $tRw[0]['RwID']; */
            $sql = "SELECT isnull(OrdDsl,0)+1  RwID,Division_SName+cast(SLNo as varchar) SLNo FROM Mas_SF_SlNo where SF_Code='" . $sfCode . "'";
							$tRw = performQuery($sql);
							$SFSl=$tRw[0]['SLNo']."-";
							$pk1 =$SFSl.$tRw[0]['RwID'];
							$sql = "update Mas_SF_SlNo set OrdDsl=".$tRw[0]['RwID']." where SF_Code='" . $sfCode . "'";
							performQuery($sql);
            $value = $transOrderDetails[$i]['sample_qty'];
            $net_weight = $transOrderDetails[$i]['product_netwt'];

            $discount = $transOrderDetails[$i]['discount'];
            $discountprice = $transOrderDetails[$i]['discount_price'];
            $discountpriceall =$discountpriceall+ $transOrderDetails[$i]['discount_price'];
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


			                if($transOrderDetails[$i]['tax_price']==null)
                               $tax_price=0;
                            else
                                $tax_price=$transOrderDetails[$i]['tax_price'];



			   if($qty>0 || $free>0   || $cbQty>0 || $Promo>0  || $CcbQty>0 ){

            /*$sql = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock,Uom_Id,Con_Qty,Unit_Name, Tax_value) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product'] . "',N'" . $transOrderDetails[$i]['product_Nm'] . "', $qty,$value,$rate,$net_weight,'$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','" . $cbQty . "','0','". $Div_ID."','0','0','" . $CcbQty . "','".  $UnitId  ."' ,'".  $rx_Conqty."','".  $UnitName  ."','".  $tax_price ."'";*/
			$sql = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight,free,discount,discount_price,rateMode,DistDisc,DistPicRate,MfgDt,ClStock,PromoVal,Div_ID,Offer_ProductNm,Offer_ProductCd,CClStock,Uom_Id,Con_Qty,Unit_Name, Tax_value,umo_unit,unit,Conf_Fac) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product'] . "',N'" . $transOrderDetails[$i]['product_Nm'] . "', $qty,$value,$rate,$net_weight,'$free','$discount','$discountprice','$rateMode',$Disc,$DistRate,'" . $transOrderDetails[$i]['Mfg_Date'] . "','" . $cbQty . "','0','". $Div_ID."','0','0','" . $CcbQty . "','".  $UnitId  ."' ,'".  $rx_Conqty."','".  $UnitName  ."','".  $tax_price ."','".  $UnitId  ."','".  $UnitName  ."','".  $rx_Conqty."'";
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
        $date = date('Y-m-d');
        $sql = "select From_Date,To_Date,No_of_Days from mas_Leave_Form where To_Date>='$date' and sf_code='$sfCode' and Leave_Active_Flag<>1 order by From_Date";
        $leaveDays = performQuery($sql);
        $currentDate = date_create($date);
        $disableDates = array();
        $sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$date' as datetime)";
        $dcrEntry = performQuery($sql);
        if (count($dcrEntry) > 0)
            array_push($disableDates, $currentDate->format('d/m/Y'));
        for ($i = 0; $i < count($leaveDays); $i++) {
            $fromDate = $leaveDays[$i]['From_Date'];
            $toDate = $leaveDays[$i]['To_Date'];
            $noOfDays = $leaveDays[$i]['No_of_Days'];
            if ($currentDate > $fromDate)
                $fromDate = $currentDate;
            $diff = date_diff($fromDate, $toDate, TRUE);
            $days = $diff->format("%a") + 1;
            for ($j = 0; $j < $days; $j++) {
                array_push($disableDates, $fromDate->format('d/m/Y'));
                $fromDate->modify('+1 day');
            }
        }
        outputJSON($disableDates);
        break;
    case "getProductDetailing":
        $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $sql="select * from vwProductDetailing where Division_Code=$Owndiv";
         $products = performQuery($sql);
		 $all=array();
        $all['data']=$products;

        outputJSON($all);
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
        $sql = "select Sf_Code,Leave_Id,Reason,Address,FieldForceName,Reporting_To_SF,Designation,HQ,EmpCode,Reporting_To_SF,Designation,cast(LeaveDays as int) LeaveDays,Division_Code,sf_Designation_Short_Name
		,Leave_Type,Leave_Name,Leave_Id LeaveCode,From_Date,To_Date
		from vwLeave where Reporting_To_SF='$sfCode'";
        $leave = performQuery($sql);

        outputJSON($leave);
        break;

		case "gettour_month_value":
        $sfCode = $_GET['sfCode'];
		$Tyear = $_GET['Tyear'];
		$TourMont = $_GET['TourMont'];
        $sql = "exec gettour_month_value '$sfCode','$Tyear','$TourMont'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;


    case "Productdisplayapprovals":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from Trans_Product_Display where HQ_SfCode= '" . $sfCode . "'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
		case "SecOrderValueConfirm_appr_rej":
			 $id=$_GET["id"];
			 $status=$_GET["status"];
			 $sql="update sec_order_val_confirmation set approve_flag=$status where id='$id'";

			 performQuery($sql);
			 $all=array();

				$all['msg']="No Records...";

			    outputJSON($all);
			break;
			   case "DevApproval":
		 $slno = $_GET['slno'];
		 		 $sfCode = $_GET['sfCode'];

		  $sql = "update DCR_MissedDates set status=4 where sl_no='$slno'";

		 performQuery($sql);
		 $notifyMsg="Deviation Approved";
		 $sql = "SELECT DeviceRegId FROM Access_Table where sf_code='" . $sfCode. "'";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                sendFCM_notify($reg_id, $notifyMsg,'Deviation Notification',0,'#ApproveOrReject');
            }

		 $all=array();
		 $all['success']="true";
		 $all['msg']="Approved Successfully";

		 outputJSON($all);
		 break;
		case "DevReject":
		 $slno = $_GET['slno'];
		 $sfCode = $_GET['sfCode'];
		 $rejectReason = $_GET['rejectionReason'];
		 $date = date('Y-m-d 00:00:00');
		 $sql = "update DCR_MissedDates set status=6 where sl_no='$slno'";
		 performQuery($sql);

		  $insertSqlQuery = "INSERT INTO Deviation_Rejection_Reason (Sf_Code, Dcr_Missed_Date, Rejection_Reason) VALUES ('$sfCode','$date','$rejectReason')";
		  performQuery($insertSqlQuery);
 $notifyMsg="Deviation Rejected";
		 $sql = "SELECT DeviceRegId FROM Access_Table where sf_code='" . $sfCode. "'";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                sendFCM_notify($reg_id, $notifyMsg,'Deviation Notification',0,'#ApproveOrReject');
            }

		  $all=array();
		  $all['success']="true";
		  $all['insertSqlQuery']=$insertSqlQuery;
		  $all['msg']="Rejected Successfully";
		  outputJSON($all);
		 break;

		          case "GetSecOrderConfirmView":
	     $sfCode = $_GET['sfCode'];
		 $Year = $_GET['year'];
		 $Month = $_GET['month'];
		 if( $Month=="00"){
			  $Month=12;
			   $Year = 2021;
		 }
		  $sql = "select cast(ROUND(CAST(sum(order_value) AS DECIMAL(20,2)), 2) as float) ordervalue from trans_order_head h
inner join vwactivity_report r on r.sf_code=h.Sf_Code and cast(activity_date as date)=cast(order_date as date)
   where h.sf_code='$sfCode' and month(order_date)=$Month and year(order_date)=$Year";

			  $order = performQuery($sql);
			  
			    $sql = "select * from sec_order_val_confirmation
   where sf_code='$sfCode' and month=$Month and year=$Year and approve_flag!=2";
			  $orderConfirmation = performQuery($sql);

		$all=array();
        $all['trans_order']=$order;
        $all['sec_ord_confirm']=$orderConfirmation;
		if(count($orderConfirmation)>0||count($order)==0||$order[0]['ordervalue']==null||$order[0]['ordervalue']=="")
			if(count($order)==0||$order[0]['ordervalue']==null||$order[0]['ordervalue']=="")
				$all['msg']="No Records...";
			else
			  $all['msg']="Already Secondary Order Value Confirmation Submitted";
			    outputJSON($all);
	   break;
	       case "GetSecOrderConfirmView_all":
	     $sfCode = $_GET['sfCode'];
		 $Year = $_GET['year'];
		 $Month = $_GET['month'];
		  if( $Month=="00"){
			  $Month=12;
			   $Year = 2021;
		 }
		  $sql = "select * from vwSecOrderConfirmApproval_all
   where (reporting_to_sf='$sfCode' or sf_code='$sfCode') and monthnum=$Month and year=$Year";
			  $order = performQuery($sql);

		$all=array();
        $all['trans_order']=$order;
        $all['sec_ord_confirm']=[];
		if(count($order)==0)
			  $all['msg']="No Data....";
			    outputJSON($all);
	   break;
 case "get/TargetSales":
                $sfCode = $_GET['sfCode'];
               // $div = $_GET['divisionCode'];
               // $divs = explode(",", $div . ",");
               //$Owndiv = (string) $divs[0];
              //  $uRDt = $_GET['rptDt'];
        	
                $query = "exec Get_Product_Target_vs_Sal_app '" . $sfCode . "'";
                 $targetSale = performQuery($query);
		$all=array();
        $all['target_sales']=$targetSale;
        $all['sal']=[];
		$all['query']=$query;
		if(count($targetSale)==0)
			  $all['msg']="No Data....";
			    outputJSON($all);die;
                break;
	    case "GetSecOrderConfirm_Approval":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwSecOrderConfirmApproval where Reporting_To_SF='$sfCode'";
        $leave = performQuery($sql);
		$all=array();
        $all['trans_order']=$leave;
        $all['sec_ord_confirm']=[];
        outputJSON($all);
        break;
       case "GetGift_Status":
        //$sfCode = $_GET['sfCode'];
		if($_GET['Sf_code']==null){
			  $sfCode = $_GET['sfCode'];
		 }else{
			  $sfCode=$_GET['Sf_code'];
		 }

        $sql = "select convert(varchar(10), Insert_Date, 120)  Created_Date,convert(varchar(10), Startt_Date, 120)  Startt_Date,Gift_Type,Retailer_Name,  convert(varchar(10), End_Date, 120)  End_Date,Expected_Business,case when
 Aproval_Flag='4' then 'Approved'  when Aproval_Flag='2' then 'Reject' else 'Pending' end GStatus ,case when
 Gift_Type='1' then 'Display'  when Gift_Type='2' then 'Billed' else 'Slab' end GiftName,
 Reject_reason,Aproval_Flag from Trans_Gift_Claim where Sf_Code ='" . $sfCode . "'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;

		  case "GetExpenseView":
        $sfCode = $_GET['sfCode'];
		 $EYear = $_GET['EYear'];
		 $EMonth = $_GET['EMonth'];
		  $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
		 if($_GET['Sf_code']==null){
			  $sfCode = $_GET['sfCode'];
		 }else{
			  $sfCode=$_GET['Sf_code'];
		 }
		 if($Owndiv=="107"){

			//$sql = "exec KtvGet_Expense_Details '" . $sfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','".$Owndiv."' ";
			 $sql = "exec Get_Expense_Details '" . $sfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','".$Owndiv."' ";
			  $View = performQuery($sql);
			 $sql = "select isnull(Applied_ExpAmnt,0) Applied_ExpAmnt,Expense_Amt from   trans_expense_head1  where SF_Code='" . $sfCode . "' and Expense_Month='" . $EMonth . "' and Expense_Year='" . $EYear . "'";
			 $Total = performQuery($sql);

		 }else{
			 $sql = "exec Get_Expense_Details '" . $sfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','".$Owndiv."' ";
			  $View = performQuery($sql);
			 $sql = "select isnull(Applied_ExpAmnt,0) Applied_ExpAmnt,Expense_Amt,Trans_Sl_No from   trans_expense_head1  where SF_Code='" . $sfCode . "' and Expense_Month='" . $EMonth . "' and Expense_Year='" . $EYear . "'";
		$Total = performQuery($sql);

		 }

$Trans_Sl_No=$Total[0]['Trans_Sl_No'];
$sql="select isnull(sum(cast(trans_exp_amt as decimal)),0) amt from Trans_Monthly_Allowance_Details where Trans_Dt_SlNo='$Trans_Sl_No'";
$tr = performQuery($sql);
$amt=$tr[0]['amt'];

$Total[0]['Applied_ExpAmnt']=$Total[0]['Applied_ExpAmnt']+$amt;
$Total[0]['Expense_Amt']=$Total[0]['Expense_Amt']+$amt;
 $sql="select cast(Add_Sub+Exp_Amnt as numeric) amt from expense_additional where transh_exp='$trans_sl_no'";
		 $val = performQuery($sql);
		 if(count($val)>0){
		  $amtt=$val[0]['amt'];
		  $Total[0]['Expense_Amt']=$Total[0]['Applied_ExpAmnt']+$amtt;
		 }
		 else
			  $Total[0]['Expense_Amt']=$Total[0]['Applied_ExpAmnt'];
		$expense=array();
        $expense['Monthwise']=$View;
        $expense['ExpenseTotal']=$Total;
        outputJSON($expense);
        break;
  case "GetExpenseView1":
        $sfCode = $_GET['sfCode'];
		 $EYear = $_GET['EYear'];
		 $EMonth = $_GET['EMonth'];
		  $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
		 if($_GET['Sf_code']==null){
			  $sfCode = $_GET['sfCode'];
		 }else{
			  $sfCode=$_GET['Sf_code'];
		 }
		 if($Owndiv=="107"){
		 
			//$sql = "exec KtvGet_Expense_Details '" . $sfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','".$Owndiv."' ";
			 $sql = "exec Get_Expense_Details '" . $sfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','".$Owndiv."' ";
			  $View = performQuery($sql);
			 $sql = "select isnull(Applied_ExpAmnt,0) Applied_ExpAmnt,Expense_Amt,trans_sl_no from   trans_expense_head1  where SF_Code='" . $sfCode . "' and Expense_Month='" . $EMonth . "' and Expense_Year='" . $EYear . "'";
			 $Total = performQuery($sql);
		
		 }else{
			 $sql = "exec Get_Expense_Details '" . $sfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','".$Owndiv."' ";
			  $View = performQuery($sql);
			 $sql = "select isnull(Applied_ExpAmnt,0) Applied_ExpAmnt,Expense_Amt,trans_sl_no from   trans_expense_head1  where SF_Code='" . $sfCode . "' and Expense_Month='" . $EMonth . "' and Expense_Year='" . $EYear . "'";
		$Total = performQuery($sql);
			 
		 }
       $trans_sl_no=$Total[0]['trans_sl_no'];
	//	if($Owndiv=="109"||$Owndiv=="100"){
		$EDate="$EYear"."-"."$EMonth"."-01";
		$sql="select sum(cast(allowance_value as float)) val from Mas_Allowance_Entry E 
inner join Mas_Allowance_Type T on Allowance_Code=cast(T.id as varchar(30)) where   User_enter=0 and Sf_Code='$sfCode'
and Effective_Date=(select  max(Effective_Date) from Mas_Allowance_Entry where Sf_Code = '$sfCode' and CONVERT(date,Effective_Date)<=CAST('$EDate' as date))";
 $val = performQuery($sql);
 if(count($val)>0)
 $amt=$val[0]['val'];
 else 
	 $amt=0;
		/*if($Owndiv=="100"){
			
 $Total[0]['Applied_ExpAmnt']=$Total[0]['Applied_ExpAmnt']-$amt;
		$Total[0]['Expense_Amt']= $Total[0]['Expense_Amt']-$amt;
		$sql="select cast(Add_Sub+Exp_Amnt as numeric) amt from expense_additional where transh_exp='$trans_sl_no'";
		 $val = performQuery($sql);
		 if(count($val)>0){
		  $amtt=$val[0]['amt'];
		  $Total[0]['Expense_Amt']=$Total[0]['Expense_Amt']+$amtt;
		 }
		}
		else{*/
				 $value=0;
for($i=0;$i<count($View);$i++){
	if($View[$i]['Exp_Status']!="Rejected")
$value=$value+$View[$i]['Daily_Total'];
}
 $Total[0]['Applied_ExpAmnt']=$amt+$value;
 $sql="select cast(Add_Sub+Exp_Amnt as numeric) amt from expense_additional where transh_exp='$trans_sl_no'";
		 $val = performQuery($sql);
		 if(count($val)>0){
		  $amtt=$val[0]['amt'];
		  $Total[0]['Expense_Amt']=$Total[0]['Applied_ExpAmnt']+$amtt;
		 }
		 else
			  $Total[0]['Expense_Amt']=$Total[0]['Applied_ExpAmnt'];
		//}
	//	}
			//$Trans_Sl_No=$Total[0]['Trans_Sl_No'];
$sql="select isnull(sum(cast(trans_exp_amt as decimal)),0) amt from Trans_Monthly_Allowance_Details where Trans_Dt_SlNo='$trans_sl_no'";
$tr = performQuery($sql);
$amt=$tr[0]['amt'];
if(count($amt)>0){
$Total[0]['Applied_ExpAmnt']=$Total[0]['Applied_ExpAmnt']+$amt;
$Total[0]['Expense_Amt']=$Total[0]['Expense_Amt']+$amt;
}
//	print_r($sql);
		$expense=array();
        $expense['Monthwise']=$View;
        $expense['ExpenseTotal']=$Total;
        outputJSON($expense);
        break;
        
    case "GetExpenseApprovals":
        $sfCode = $_GET['sfCode'];
		 $EYear = $_GET['EYear'];
		 $EMonth = $_GET['EMonth'];
		  $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
		 if($_GET['Sf_code']==null){
			  $sfCode = $_GET['sfCode'];
		 }else{
			  $sfCode=$_GET['Sf_code'];
		 }

		$sql2 = "exec Get_Expense_Approvals_App '" . $sfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','".$Owndiv."' ";
		$View = performQuery($sql2);
		$sql = "select isnull(Applied_ExpAmnt,0) Applied_ExpAmnt,Expense_Amt from   trans_expense_head1  where SF_Code='" . $sfCode . "' and Expense_Month='" . $EMonth . "' and Expense_Year='" . $EYear . "'";
    	$Total = performQuery($sql);


		$expense=array();
        $expense['Monthwise']=$View;
        $expense['ExpenseTotal']=$Total;
        outputJSON($expense);
        break;

		case "get/expenseApproveReject":
			$loginsfCode = $_GET['sfCode'];
			$EYear = $_GET['EYear'];
			$EMonth = $_GET['EMonth'];
			$div = $_GET['divisionCode'];
			$flag = $_GET['flag'];
			$divs = explode(",", $div . ",");
			$Owndiv = (string) $divs[0];
			 if($_GET['Sf_code']==null){
				  $sfCode = $_GET['sfCode'];
			 }else{
				  $sfCode=$_GET['Sf_code'];
			 }

			$data = json_decode($_POST['data'], true);
			if($flag == 0){
				$type = 0;
				$notifyMsg = 'Expense Rejected on '. $data[0];
				if(count($data)>1){
					$type = 1;
					$notifyMsg = 'Expense Rejected for some dates';
				}

				$rejectQuery = "exec RejectExpense '" . $sfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','".$data[0]."','".$type."' ";
				$rejectRes = performQuery($rejectQuery);

			}else{
                $notifyMsg = 'Expense Approved for some dates';

				$approveQuery = "exec ExpenseApproveNativeApp '" . $sfCode . "' ,'" . $loginsfCode . "' ,'" . $EMonth . "' ,'" . $EYear . "','". $Owndiv . "' ";
				$approveQueryRes = performQuery($approveQuery);
			}

			$sql = "SELECT DeviceRegId FROM Access_Table where sf_code='" . $sfCode. "'";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                sendFCM_notify($reg_id, $notifyMsg,'Expense Notification',0,'#ApproveOrReject');
            }


			$result['success']=true;
			$result['msg']="Successfully Done";
			$result['sql']=$sql;
			$result['reg_id']=$reg_id;
			$result['notifyMsg']=$notifyMsg;
			outputJSON($result);
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
        $sql = "select Objective,SF_Code,Worked_With_SF_Name,Worktype_Name_B,Territory_Code1,convert(varchar,Tour_Date,103) Tour_Date,JointWork_Name,isnull(Worktype_Name_B2,'') JointWork_Name1,HQ_Code from Trans_TP_One where Sf_Code='$sfCode' and Tour_Month='$month' and Tour_year=$year order by Tour_Date";
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
		case "vwChkDevApproval":
        $sfCode = $_GET['sfCode'];
		$month = $_GET['month'];
		$year = $_GET['year'];
        /*$sql = "select Dcr_Missed_Date,CONVERT(VARCHAR(10), Dcr_Missed_Date, 103) missed_date,s.sf_code,s.sf_name,sl_no,Deviation_Reason reason from DCR_MissedDates d
            inner join Mas_Salesforce s on d.Sf_Code=s.Sf_Code
			left join DCR_TPDev_Reason r on r.sf_code=d.sf_code and cast(Dcr_Missed_Date as date)=cast(Activity_Date as date)
             where d.status=3 and Reporting_To_SF='$sfCode' and month(Dcr_Missed_Date)=(case when 'All'='".$month."'then month(Dcr_Missed_Date) else '".$month."' end) and year(Dcr_Missed_Date)=(case when 'All'='".$year."'then year(Dcr_Missed_Date) else '".$year."' end)"; */
			$sql = "select Dcr_Missed_Date,CONVERT(VARCHAR(10), Dcr_Missed_Date, 103) missed_date,s.sf_code,s.sf_name,sl_no,Deviation_Reason reason from
			DCR_MissedDates d inner join Mas_Salesforce s on d.Sf_Code=s.Sf_Code left join
			(select ROW_NUMBER() over(partition by sf_code,cast(Activity_Date as date) order by sf_code,cast(Activity_Date as date),created_date desc) rw , sf_code, Activity_Date, Deviation_Reason  from DCR_TPDev_Reason) r on r.sf_code=d.sf_code and
			cast(Dcr_Missed_Date as date)=cast(Activity_Date as date) and rw=1 where d.status=3 and Reporting_To_SF='$sfCode' and
			month(Dcr_Missed_Date)=(case when 'All'='".$month."'then month(Dcr_Missed_Date) else '".$month."' end)
			and year(Dcr_Missed_Date)=(case when 'All'='".$year."'then year(Dcr_Missed_Date) else '".$year."' end)";

/* $sql = "select Dcr_Missed_Date,CONVERT(VARCHAR(10), Dcr_Missed_Date, 103) missed_date,s.sf_code,s.sf_name,sl_no from DCR_MissedDates d
            inner join Mas_Salesforce_One s on d.Sf_Code=s.Sf_Code
             where d.status=3 and Reporting_To_SF='$sfCode' and month(Dcr_Missed_Date)=$month and year(Dcr_Missed_Date)=$year";*/

        $tp = performQuery($sql);
			$all=array();
        $all['dev']=$tp;
        $all['sql']=$sql;
        outputJSON($all);die;
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
        $myfile = "";//fopen("../server/errorfile.txt", "a+");
        $txt = "John Doe\n";
        $sqlsp=$_POST['data']." message:".$_GET['msg']."sf_code=".$_GET['sfCode']."\n";
        //fwrite($myfile, $sqlsp);
        //fclose($myfile);
        die;
        break;
    case "dcr/save":
        addEntry();
        break;
	case "get/pmOrderDetails":
		outputJSON(getPmOrderDet());
		break;
		case "get/checkInList":
        outputJSON(getCheckinList());
    break;
	  case "get/InshopStockList":
        outputJSON(getInshopStockList());
    break;
	case "get/SecCallDets":
        outputJSON(getSecCallDets());
    break;
	case "get/SecOrderDets":
        outputJSON(getSecOrderDets());
    break;
		case "get/rptCheckInList":
        outputJSON(getrptCheckinList());
    break;
	
     case "get/popList":
    outputJSON(getPopList());
      break;
	  Case "get/PopStock":
    outputJSON(getPopStock());
break;
    case "get/popOrderDetails":
    outputJSON(getPopOrderDetails());
   break;
    case "get/InshopDetails":
          outputJSON(getInshopDetails());
		  break;
    case "get/PendingPaymentDets":
          	outputJSON(getPendingPaymentDets());
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
	 $divCode = $_GET['divisionCode'];
	  if( $divCode=="156," || $divCode=="156")
        getPreCallDetTSR();
	else
        getPreCallDet();
        break;
   case "get/vanLstLdDt":
        getVanLstLdDt();
        break;
    case "get/currentStock":
        getCurrentStockDet();
        break;
	case "get/StockDetails":
	if ($_GET['scode'] == "0")
        $SF = $_GET['sfCode'];
    else
        $SF = $_GET['scode'];

    $CLDT = $_GET['cldt'];
 
 	 $divCode = $_GET['divisionCode'];
             $divisionCode = explode(",", $divCode);
			 $Owndiv = (string) $divisionCode[0];
			 $sql="select isnull(closing_monthwise,1) closing_monthwise from Access_Master where division_code=$Owndiv";
			 $monthws=performQuery($sql);
			 $clsng=$monthws[0]['closing_monthwise'];
    //if($CLDT=='') $CLDT=date('Y-m-d 00:00:00');
	if($clsng=="1")
		 $query = "select Stockist_Code,Product_Name product_Nm,Rec_Qty recv_qty,Cb_Qty cb_qty,pieces,Product_Code product,cast(convert(varchar,Purchase_Date,101) as datetime) updateDate,
Conversion_Qty conversionQty,Mgf_date,Distributer_Rate Rate,CAST(TotalValue AS float)sample_qty,batch_no 
from Trans_Stock_Updation_Details where (Purchase_Date = '".$CLDT."' or Purchase_Date = (select max(Purchase_Date) from Trans_Stock_Updation_Details where Stockist_code='".$SF."' and Purchase_Date <= '".$CLDT."' )) 
and Stockist_code = '".$SF."' group by Stockist_Code,Product_Name,Rec_Qty,cb_qty,pieces,Product_Code,Purchase_Date,Conversion_Qty,Distributer_Rate,CAST(TotalValue AS float),
			 Mgf_date,batch_no";
	 else
    $query = "exec getClStkVw '$SF','$CLDT'";
    $transHead = performQuery($query);
    return outputJSON($transHead);
		break;
	case "get/currentStockSS":
        getCurrentStockDetSS();
        break;
    case "get/StockDetailsSS":
		 if ($_GET['scode'] == "0")
        $SF = $_GET['sfCode'];
    else
        $SF = $_GET['scode'];

    $CLDT = $_GET['cldt'];

    //if($CLDT=='') $CLDT=date('Y-m-d 00:00:00');
    $query = "select Stockist_Code,Product_Name product_Nm,Rec_Qty recv_qty,Cb_Qty cb_qty,  
pieces,Product_Code product,cast(convert(varchar,Purchase_Date,101) as datetime) updateDate,
Conversion_Qty conversionQty,Mgf_date,Distributer_Rate Rate1,CAST(TotalValue AS float)sample_qty, batch_no,isnull(SS_Base_Rate,0) Rate 
from Trans_SS_Stock_Updation_Details where (Purchase_Date = '".$CLDT."' or Purchase_Date = (select max(Purchase_Date) from Trans_SS_Stock_Updation_Details where Stockist_code = '".$SF."' and Purchase_Date <= '".$CLDT."'))
and Stockist_code = '".$SF."' group by Stockist_Code,Product_Name,Rec_Qty,cb_qty,pieces,Product_Code,Purchase_Date,Conversion_Qty,Distributer_Rate,CAST(TotalValue AS float), 
Mgf_date,batch_no,SS_Base_Rate";
    $transHead = performQuery($query);
    return outputJSON($transHead);
		break;
	case "get/stockBasedOrder":
        getStockBasedOrder();
        break;

	case "get/logout_day":
		$sf = $_GET['sfCode'];
		$div = $_GET['divisionCode'];
        $sql = "select count(*) cnt from TP_Attendance_App   where  Sf_Code='".$sf."' and cast(login_date as date)=cast(getdate() as date) and cast(End_Time as date)=cast(getdate() as date)";
                $tr=performQuery($sql);
				if($tr[0]['cnt']>0)
					$tr[0]['cnt']=1;
				//if($div=='4,')
					//$tr[0]['cnt']=0;
				outputJSON($tr[0]);
        break;

	case "get/brandavail":
		$sf = $_GET['sfCode'];
		$div = $_GET['divisionCode'];
		$dyDt = $_GET['activityDate'];
        $sql = "select SFCode,BId,(select Product_Brd_SName from Mas_Product_Brand where Product_Brd_Code=BId) BName,Sum(Avail) Avail,Sum(EC) EC,0 tc from 
(
SELECT SFCode,s.c.value('(b)[1]', 'nvarchar(20)' ) AS BId,s.c.value('(b)[3]', 'int' ) AS Avail,s.c.value('(b)[4]', 'int' ) AS EC
FROM ( select SFCode,cast(replace('<a><b>'+replace(replace(BrndRevw,'#','</b><b>'),'$','</b></a><a><b>')+'</b></a>','<a><b></b></a>','') as xml) AS msg from Trans_OutletReview_Details
where cast(convert(varchar,Adt,101) as datetime)='" . $dyDt . "' and sfcode='".$sf."'
) a
    CROSS APPLY a.msg.nodes('a') s(c)
) as Stb
where Avail>0 or EC>0
group by SFCode,BId";
                $tr=performQuery($sql);

$result1=array();
if(count($tr)>0){
$query="select top 1 ClstrName Ruts,StkName Stks from TbMyDayPlan where SF_Code='".$sf."' and cast(pln_date as date)='" . $dyDt . "' order by pln_date desc";
  $tr1=performQuery($query);
  $result1[0]['Ruts']= $tr1[0]['Ruts'];
   $result1[0]['Stks']= $tr1[0]['Stks'];
   $sql="select count(retid) tc from Trans_OutletReview_Details
where SFCode='".$sf."' and cast(Adt as date)='" . $dyDt . "' group by SFCode,RetID";
 $tr2=performQuery($sql);
 $result1[0]['tc']= count($tr2);
 $result1[0]['name']= $tr2[0]['tc'];
 $result1[0]['value']= $tr;
 $result1[0]['value'][0]['tc']= count($tr2);
}
				outputJSON($result1);
        break;
 case "get/currentSSStock":
        getCurrentSSStockDet();
        break;
    case "get/dayplan":
        outputJSON(getDayPlan());
        break;
case "get/retailerprecall":
        outputJSON(getRetailerprecall());
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

    case "get/iOSDayReport":
        outputJSON(iOSDayReport());
        break;
    case "get/DayReport":
        outputJSON(DayReport());
        break;
case "get/OrderDayReport":
       outputJSON(DayOrderReport());
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

case "get/GetSecondaryTarget":


        outputJSON(GetSecondaryTarget());
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
		case "get/SecondaryProductwise":

        outputJSON(SecondaryProductWise());
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
		case "get/getVisitAttendanceView":


        outputJSON(getVisitAttendanceView());

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
case "get/quizcs":
        outputJSON(getQuizcs());
        break;
    case "summaryDet":
        outputJSON(getsummary());
        break;
    case "DaySummaryDet":
        outputJSON(getDaySummary());
        break;
	case "get/vwVstDetNative":
	$ACd = $_GET['ACd'];
	if($ACd=="dm"){
		  outputJSON(getVstDetsNative_dm());die;
	}
	else if($ACd=="scheme"){
		  outputJSON(getVstDetsNative_scheme());die;
	}
		else
        outputJSON(getVstDetsNative());
        break;
    case "get/vwVstDet":
        outputJSON(getVstDets());
		break;

	case "get/vwItemSummmary":
        outputJSON(getItemSummaryByDt());
        break;
	case "get/vwOrderDet":
        outputJSON(getOrderDetByDt());
        break;
    case "get/vwiOSVstDet":
        outputJSON(getiOSVstDets());
		break;

	case "get/vwVstDet_order":
        outputJSON(getVstOrderDets());
        break;
	case "get/vwVstEventDet":
        $trans_slno=$_GET['trans_slno'];
		 $trans_details_slno=$_GET['trans_details_slno'];
		 $sfCode=$_GET['sfCode'];
		 $sql="exec getEventDetails '$trans_slno','$trans_details_slno','$sfCode'";

		 $results=performQuery($sql);
		 outputJSON($results);
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
    case "save/trackloc":
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

		$batteryPercent=$TrcLocs['battery_percent'];
        $divCode = $_GET['divisionCode'];
        $State_Code = $_GET['State_Code'];
        $Current_Date_Time=date("Y-m-d h:i:sa");
        $BatteryStatus=$data[0]['BatteryStatus'];
        $sql="insert into Trans_Battery_Status (sf_code,Div_Code,DateandTime,Battery_Status) values ('".$sfCode."','". str_replace(",", "",$divCode)."','".$Current_Date_Time."' ,'".$batteryPercent."' )";
        performQuery($sql);




        break;
		
    case "get/newcalls":
        $sfCode = $_GET['sfCode'];
        $date = date('Y-m-d');
        $result = array();
       $div = $_GET['divisionCode'];
	   
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
		
		
		$query = "select Trans_Detail_Info_Code DrCode,count(oh.Trans_Sl_No) pc from DCRMain_Trans h with(nolock) inner join DCRDetail_Lst_Trans d with(nolock) on h.Trans_SlNo=d.Trans_SlNo left outer join Trans_Order_Head oh with(nolock) on d.Trans_Detail_Slno=DCR_Code and Order_Value>0 where h.SF_code='".$sfCode."' and  convert(date,h.Activity_Date)='".$date."' group by Trans_Detail_Info_Code";
		$res = performQuery($query);
		$result['today']["drlist"]= $res;
		

$query = "select Pln_Date Adate,'' Acode,
	cluster,AC RCCOUNT,
	ClstrName RouteName,
	sum(isnull(TC,0)) calls,
	sum(isnull(EC,0)) [order],
	sum(isnull(orderValue,0)) orderVal from  
	(
		SELECT convert(varchar,isnull(Pln_Date,Adate),103) Pln_Date,isnull(ClstrName,SDP_Name) ClstrName,isnull(TP.sf_code,d.Sf_Code) sf_code,isnull(cluster,SDP) cluster,count(ListedDrCode) AC,isnull(TC,0) TC,isnull(EC,0) EC,isnull(orderValue,0) orderValue 
		FROM (
			select cast(Pln_Date as date) Pln_Date,Sf_Code,sf_member_code,ClstrName,cluster from TbMyDayPlan TP with(nolock) where TP.Sf_Code='".$sfCode."' and convert(date,Pln_Date)='".$date."' group by cast(Pln_Date as date),Sf_Code,sf_member_code,ClstrName,cluster
		) as tp full outer join (
			select h.Activity_Date Adate,h.SF_Code,h.Trans_SlNo,SDP,SDP_Name,
			COUNT(distinct Trans_Detail_Info_Code) TC,COUNT(distinct Cust_Code) EC,cast(isnull(sum(order_Value),0) as decimal) orderValue from DCRMain_Trans h with(nolock) inner join DCRDetail_Lst_Trans d with(nolock) on h.Trans_SlNo=d.Trans_SlNo left outer join Trans_Order_Head o with(nolock) on DCR_Code=d.Trans_Detail_Slno and Cust_Code=Trans_Detail_Info_Code and Order_Value>0 where h.Sf_Code='".$sfCode."' and convert(date,h.Activity_Date)='".$date."' GROUP BY h.Activity_Date,h.SF_Code,h.Trans_SlNo,SDP,SDP_Name
		) as d on TP.cluster=d.SDP and Pln_Date=Adate and d.Sf_Code=TP.sf_code
		left outer join ( select Territory_Code,sf_code SF,ListedDrCode from Mas_ListedDr with(nolock) where ListedDr_Active_Flag=0 and division_code='".$Owndiv."' ) as L on charindex(','+ isnull(TP.cluster,SDP)+',',','+ cast(L.Territory_Code as varchar)+',')>0 and charindex(','+  isnull(TP.sf_member_code,d.SF_Code) +',',','+ SF +',')>0 group by convert(varchar,isnull(Pln_Date,Adate),103),isnull(ClstrName,SDP_Name),isnull(TP.sf_code,d.Sf_Code),isnull(cluster,SDP),isnull(TC,0),isnull(EC,0), isnull(orderValue,0)) as t group by Pln_Date,cluster,AC,ClstrName";
		$res = performQuery($query);
		$result['today']["calls"]= $res;

        $query = "exec GetTargetAchDy '".$sfCode."','".date('d')."','".date('m')."','".date('Y')."'";
        $order = performQuery($query);




        $result['today']['AQty'] = $order[0]['AQty'];
        $result['today']['AAmt'] = $order[0]['AAmt'];
	if($Owndiv=="162"||$Owndiv=="29"){
			 $targetquery = "exec Get_FieldForce_Target_vs_Sal_app '" . $sfCode . "', '" . date("Y") . "','" .date('m') . "', '" .  date("Y") . "','" . date('m') . "' ,  '" . $Owndiv . "' ";
		}
		else{
			 $targetquery = "exec Get_FieldForce_Target_vs_Sal '" . $sfCode . "', '" . date("Y") . "','" .date('m') . "', '" .  date("Y") . "','" . date('m') . "' ,  '" . $Owndiv . "' ";
		}
		 $Target = performQuery($targetquery);
		     $query = "exec GetTargetAchMonth '".$sfCode."','".date('m')."','".date('Y')."'";
        $tc_pc = performQuery($query);
		 $query = "exec Get_FieldForce_Acheive '".$sfCode."','".date('m')."','".date('Y')."','" . $Owndiv . "' ";
        $orderVal = performQuery($query);
        $result['month']['order'] = $tc_pc[0]['pc'];
        $result['month']['orderVal'] = $orderVal[0]['order_value'];
        $result['month']['calls'] = $tc_pc[0]['tc'];
        $result['month']['TQty'] = 0;
        $result['month']['TAmt'] =0;
        $result['month']['AQty'] =0;
        $result['month']['AAmt'] = 0;
		$result['month']['target_val'] = $Target[0]['target_val'];
		$result['month']['order_val'] = $Target[0]['order_val'];
		$result['month']['query'] = $targetquery;
		$result['success'] = true;
		$result['msg'] = 'success';
        outputJSON($result);
        break;
    case "get/calls":
        $sfCode = $_GET['sfCode'];
        $date = date('Y-m-d');
        $result = array();
        $query = "select convert(varchar,h.Activity_Date,103) Adate,cast(isnull(sum(order_Value),0) as decimal) orderValue,h.Trans_SlNo,Trans_Detail_Slno from vwActivity_Report h
		inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and convert(date,h.Activity_Date)='$date' group by h.Trans_SlNo,convert(varchar,h.Activity_Date,103) ,Trans_Detail_Slno";
        $data = performQuery($query);
/*if($sfCode=="MR3532"){
	 $query = "select convert(varchar,h.Activity_Date,103) Adate,cast(order_Value as decimal) orderValue,h.Trans_SlNo from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='MR4775' and h.Activity_Date='2021-07-23'";
        $data = performQuery($query);

}*/
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
        $query = "select top 1 Pln_Date,sf_code,cluster,sf_member_code,(select count(ListedDrCode) from Mas_ListedDr MLDR with(nolock) where  charindex(','+ TP.cluster+',',','+ cast(MLDR.Territory_Code as varchar)+',')>0  and ListedDr_Active_Flag=0 and  division_code='".$Owndiv."' and charindex(','+  TP.sf_member_code+',',','+ sf_code+',')>0 group by Territory_Code) cnt  from TbMyDayPlan TP with(nolock)  where division_code='".$Owndiv."' and sf_code='".$sfCode."' and cast(convert(varchar,Pln_Date,101)as datetime)='".$date."'    order by Pln_Date desc";
        $rccount = performQuery($query);

		/* if($Owndiv==20){
		$myfile = fopen("../server/Callsfile.txt", "a+");
        $txt = "John Doe\n";
        $sqlsp=$query." Calls_Count:". $calls."sf_code=".$_GET['sfCode']."\n";
        fwrite($myfile, $sqlsp);
        fclose($myfile);
		} */




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




	//	if($Owndiv=="175"||$Owndiv=="162"){
		if($Owndiv=="162"||$Owndiv=="29"){
			 $targetquery = "exec Get_FieldForce_Target_vs_Sal_app '" . $sfCode . "', '" . date("Y") . "','" .date('m') . "', '" .  date("Y") . "','" . date('m') . "' ,  '" . $Owndiv . "' ";
		}
		else{
			 $targetquery = "exec Get_FieldForce_Target_vs_Sal '" . $sfCode . "', '" . date("Y") . "','" .date('m') . "', '" .  date("Y") . "','" . date('m') . "' ,  '" . $Owndiv . "' ";
		}
		 $Target = performQuery($targetquery);
		     $query = "exec GetTargetAchMonth '".$sfCode."','".date('m')."','".date('Y')."'";
        $tc_pc = performQuery($query);
		 $query = "exec Get_FieldForce_Acheive '".$sfCode."','".date('m')."','".date('Y')."','" . $Owndiv . "' ";
        $orderVal = performQuery($query);
        $result['month']['order'] = $tc_pc[0]['pc'];
        $result['month']['orderVal'] = $orderVal[0]['order_value'];
        $result['month']['calls'] = $tc_pc[0]['tc'];
        /*$query = "exec GetTargetAch '".$sfCode."','".date('m')."','".date('Y')."'";
        $order = performQuery($query);*/
        $result['month']['TQty'] = 0;
        $result['month']['TAmt'] =0;
        $result['month']['AQty'] =0;
        $result['month']['AAmt'] = 0;
		$result['month']['target_val'] = $Target[0]['target_val'];
		$result['month']['order_val'] = $Target[0]['order_val'];
		$result['month']['query'] = $targetquery;
		/*}
		else{
			 $targetquery = "exec Get_FieldForce_Target_vs_Sal '" . $sfCode . "', '" . date("Y") . "','" .date('m') . "', '" .  date("Y") . "','" . date('m') . "' ,  '" . $Owndiv . "' ";

		 $Target = performQuery($targetquery);
        $query = "select ROUND(sum(order_Value), 1) orderValue,count(h.Trans_SlNo) Trans_SlNo 
		from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and MONTH(h.Activity_Date) = MONTH(getDate()) and YEAR(h.Activity_Date) = YEAR(getDate())";
		$query = "select 0 orderValue,0 Trans_SlNo ";       
	   $month = performQuery($query);
        $query = "select count(h.Trans_SlNo) Trans_SlNo from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and MONTH(h.Activity_Date) = MONTH(getDate()) and YEAR(h.Activity_Date) = YEAR(getDate()) and order_Value>0";
        $query = "select 0 Trans_SlNo ";
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
		$result['month']['target_val'] = $Target[0]['target_val'];
		$result['month']['order_val'] = $Target[0]['order_val'];
		$result['month']['query'] = $targetquery;
		
		}*/
		$result['success'] = true;
		$result['msg'] = 'success';
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
	case "get/Cusretailer":
          $id=$_POST['data'];
          $id = str_replace('"', "", $id);
          $div_code = $_POST['divcode'];
          $cus_view_query = "select ModuleId ModuleId,Field_Name Field_Name,Fld_Type Fld_Type,Fld_Symbol Fld_Symbol,Field_Col Field_Col,Fld_Length Fld_Length,Mandate Mandate,flag flag,Fld_Src_Name Fld_Src_Name,Fld_Src_Field Fld_Src_Field
          from Trans_Custom_Fields_Details where Div_code = '$div_code' and ModuleId = '3'";
         $query="exec GetRetailerDetails  '" . $id . "' ";
         $cus_view_data_query = "select * from Trans_Retailer_Custom_Field where RetailerID = '$id'";
         $result['Static_Data'] = performQuery($query);
         $result['Dynamic_View'] = performQuery($cus_view_query);
         $result['Dynamic_view_Data'] = performQuery($cus_view_data_query);
        outputJSON($result);

        break;
	case "UpdateRetailerDynamicData":
        $retailer_id = $_GET['retailerId'];
        $sf_code = $_GET['sfcode'];
        $column_value = $_GET['column_values'];
        $query = "update Trans_Retailer_Custom_Field set $column_value where RetailerID='$retailer_id'";
        performQuery($query);
        $result['query'] = $query;
        $result['success'] = true;
        outputJSON($result);
        break;
 case "get/Deactivate":
          $id=$_POST['data'];
          $Retailer_Id = str_replace('"', "", $id);
          $sfCode = $_GET['sfCode'];
          $sql = "update Mas_ListedDr set NoOfAnimal='" . $sfCode . "' ,ListedDr_Deactivate_Date='" .  date("Y-m-d h:i:sa"). "' ,ListedDr_Active_Flag='1'  where ListedDrCode='" . $Retailer_Id . "'  ";
		  performQuery($sql);
		  $resp["success"] = true;
		  $resp["Query"] = $sql;
        echo json_encode($resp);
        die;
        break;



    case "get/misseddates":
            $sfCode = $_GET['sfCode'];


$data = json_decode($_POST['data'], true);
//	$sfCode = (string) $data['SF'];
	$div = (string) $data['div'];
         $query="select CONVERT(VARCHAR(10),Dcr_Missed_Date,111) as id,CONVERT(VARCHAR(10),Dcr_Missed_Date,120) as name  from DCR_MissedDates where Sf_Code='".$sfCode ."'  and Status=1 order by Dcr_Missed_Date";
		  $all=array();
        $all['data']=performQuery($query);

        outputJSON($all);
        //outputJSON(performQuery($query));

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
        $today = date('Y-m-d 00:00:00');
		$div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];

		if($_GET['day']==null)
		 $day =2;
	 else
         $day = $_GET['day'];
         $SrtEndNd = $_GET['SrtEndNd'];
		 if($data['date']!=null){
		    $date=str_replace("'", "", $data['date']);
			}
	else{
	    $date=date('Y-m-d');
	    }
if($data['date_time']!=null)
		$dateTime=str_replace("'", "", $data['date_time']);
	else
            $dateTime=date("Y-m-d h:i:sa");
    if($data['time']!=null)
			$logintime=str_replace("'", "", $data['time']);
	else
	$logintime=date("h:i:sa");
		 if($SrtEndNd==0){
			// if($day==1){
			$queryy = "update Attendance_history set End_Time='" . $dateTime. "',End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "'    where  Sf_Code='" . $sfCode . "'  and login_date='".$date."'";
			performQuery($queryy);

			 //}

			if($data['endKmPhoto']!=null && $data['endKmPhoto']!="undefined")
            {
				$query = "update TP_Attendance_App set Duty_Mode=$day,Rmks='" . $data["remarks"] . "',End_Time='" . $dateTime. "' ,End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "' ,day_end_km='" . $data["day_end_km"] . "' where  Sf_Code='" . $sfCode . "'  and login_date='".$date."'";
				performQuery($query);
				
                $sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time) values( '" . $sfCode . "','" . $data['endKmPhoto']['imgurl'] . "','" .  $data['endKmPhoto']['title'] . "','" . $data['endKmPhoto']['remarks'] . "','" . $Owndiv . "','Day End km Pic','".$today."')";
                performQuery($sql);
				
			}else{
				
				$query = "update TP_Attendance_App set Duty_Mode=$day,Rmks='" . $data["remarks"] . "',End_Time='" . $dateTime. "' ,End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "' where  Sf_Code='" . $sfCode . "'  and login_date='".$date."'";
				performQuery($query);
            }
		 }
		 else{
			$queryy = "update Attendance_history set End_Time='" .  $dateTime. "',End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "'    where  Sf_Code='" . $sfCode . "'  and login_date='".$date."'";
			performQuery($queryy);

			$query = "update TP_Attendance_App set End_Time='" . $dateTime. "' ,End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "'    where  Sf_Code='" . $sfCode . "'  and login_date='".$date."'";
			performQuery($query);

		 }


        $query = "update Login_Time_Table set End_Time='" . $dateTime. "' ,End_Lat= '" . $data["Lattitude"] . "' ,End_Long ='" . $data["Langitude"] . "'  where  Sf_Code='" . $sfCode . "'  and login_date='".$date."'";


        performQuery($query);
		if($data['StopWorkPhoto']!=null && $data['StopWorkPhoto']!="undefined")
            {



                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,Division_Code,Identification,Insert_Date_Time) values( '" . $sfCode . "','" . $data['StopWorkPhoto']['imgurl'] . "','" . $Owndiv . "','Logout Pic','".$today."')";
                    performQuery($sql);

            }

        $result["success"] = true;
        $result["Query"] = $queryy;
        outputJSON($result);

        break;



         case "upd/retailer":
        $data = json_decode($_POST['data'], true);
        $result = array();
		$today = date('Y-m-d H:i:s');
		   $div = $_GET['divisionCode'];
		if($div=="101,")
			$data["Phone"]="0".$data["Phone"];

		$visitpath = '';
		$sfCode = $_GET['sfCode'];
		$State_Code = $_GET['stateCode'];

		$divCode = $_GET['divisionCode'];
        $divisionCode = explode(",", $divCode);



		if($data['imgurl']!=null && $data['imgurl']!="undefined")
            {


                   $visitpath=$sfCode.'_'.''.$data['imgurl'];

                    $sql = "insert into Activity_Event_Captures(Activity_Report_Code,DCRDetNo,imgurl,Division_Code,Identification,Insert_Date_Time) values( '$sfCode','" . $State_Code . "','" . $visitpath . "','" . $divisionCode . "','Retailer Pic','".$today."')";
                    performQuery($sql);

            }

$sql_edit = "select * from Mas_ListedDr where listeddr_name='" . $data["name"] . "' and Territory_Code=(select Territory_Code from
Mas_ListedDr where listeddrcode='" . $data["id"] . "')";
	$tr=performQuery($sql_edit);
	$sql="select listeddr_name name from Mas_ListedDr where listeddrcode='" . $data["id"] . "'";
	$name=performQuery($sql);
	if(count($tr)>0&&($divCode=="4,"||$divCode=="29,")){
		if(count($tr)==1&&$name[0]['name']!= $data["name"]){
	$result["success"] = false;
	$result["sql"] = $sql_edit;
	$result["msg"]="Same Retailer Multiple time exists.. Please contact Admin..";
		
	
	outputJSON($result);
	 die;
	}}
		 $query = "update Mas_ListedDr set ListedDr_Name='" . $data["name"] . "' ,ListedDr_DOB='" . $data["ListedDr_DOB"] . "'  ,ListedDr_DOW='" . $data["ListedDr_DOW"] . "', ListedDr_Address1='" . $data["Address"] . "', Doc_ClsCode='" . $data["ClassCode"] . "',doc_special_code='" . $data["SpecCode"] . "', ListedDr_Phone='" . $data["Phone"] . "',Doc_Spec_ShortName='" . $data["SpecName"] . "',Doc_Class_ShortName='" . $data["ClassName"] . "' , cityname='" . $data["Cityname"] . "' ,areaname='" . $data["AreaName"] . "',contactperson='" . $data["ContactPerson"] . "',designation='" . $data["Designation"] . "',gst='" . $data["GSTno"] . "',pin_code='" . $data["PINcode"] . "',ListedDr_Phone2='" . $data["Phone2"] . "',contactperson2='" . $data["ContactPerson2"] . "',designation2='" . $data["Designation2"] . "',Land_Mark='" . $data["Land_Mark"] . "',LastUpdt_Date='".$today."',contactperson3='" . $data["ContactPerson3"] . "',ListedDr_Phone3='" . $data["Phone3"] . "' ,ListedDr_Email='" . $data["email"] . "', Visit_Hours='" . $visitpath . "', ListedDr_Class_Patients='" . $data["lat"] . "', ListedDr_Consultation_Fee='" . $data["long"] . "', ListedDr_Address3='" . $data["street"] . "' where  ListedDrCode='" . $data["id"] . "'";
	//		if($data["imgurl"]!=null)
	//		$query = $query +"', Visit_Hours='" . $data["imgurl"] . "', ListedDr_Class_Patients='" . $data["lat"] . "', ListedDr_Consultation_Fee='" . $data["long"] . "', ListedDr_Address3='" . $data["street"] . "'";
	//    $query = $query + "'where  ListedDrCode='" . $data["id"] . "'";

        $temp = performQuery($query);
		  $data["addrs"]="'".getaddress(str_replace("'","",$data["lat"]),str_replace("'","",$data["long"]))."'";
		  $Owndiv=$divisionCode[0];
		  		if($Owndiv=="156")
              {
            $sql = "SELECT isNull(max(MapId),0)+1 as MapId FROM Map_GEO_Customers";
            $topr = performQuery($sql);
            $pk = (int) $topr[0]['MapId'];

            $tableData[$tableName]["MapId"] = $pk ;
        //    $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["MapId"];

			 $sql ="select count(Cust_Code) AS [Count] from Map_GEO_Customers   where Cust_Code=" .$data["id"] . "";
			 $tr1=performQuery($sql);
			if(!($tr1[0]['Count']>0)){
				$sql = "insert into Map_GEO_Customers(MapId,Cust_Code,lat,long,addrs,StatFlag,Division_code) select " . $pk . "," . $data["id"] . "," . $data["lat"]  . "," . $data["long"] . "," . $data["addrs"] . ",0,'" . $Owndiv . "'" ;
            performQueryWP($sql,[]);
		  }}
        $result["success"] = true;
		$result["Query"] = $query;
		$result['sql_edit']=$sql_edit;
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
		if($Owndiv=="156")
	     $query = "exec getMyDayPlanVwSFHry_tsr '" . $sfCode . "','" . $Owndiv . "','" . $uRDt . "'";
        else
        $query = "exec getMyDayPlanVwSFHry '" . $sfCode . "','" . $Owndiv . "','" . $uRDt . "'";
        outputJSON(performQuery($query));
        break;
		 case "getDayEvtReport":
	     $rsf = $_GET['rSF'];
		  $date = $_GET['date'];
		  
		   $sfCodes = explode(",", $rsf . ",");
    $sfCode1 = (string) $sfCodes[0];
	if($sfCode1=="All"){
		 $rsf=$sfCodes[1];
				  $sql = "exec [getDayEventCaptureReportAppAll] '$rsf','$date'";

	}
	else
		  $sql = "exec [getDayEventCaptureReportApp] '$rsf','$date'";
		//  echo $sql;die;
			  $order = performQuery($sql);

			    outputJSON($order);
	   break;

    case "get/DYBInv":
        outputJSON(GetDailyInv());
        break;
        case "get/beginDone":
                outputJSON(GetBeginInvDone());
                break;
        	case "get/ApproveDone":
                outputJSON(GetApprvDone());
                break;
        	case "get/rejectedList":
                outputJSON(GetRejectedList());
                break;
        	case "get/ApprovalDetails":
                outputJSON(GetApprovalDetails());
                break;
           case "get/ApprovalDetProducts":
                outputJSON(GetApprovalDetProducts());
                break;
          case "get/ApprovalDetProductBrnds":
                outputJSON(GetApprovalDetProductBrnds());
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

		 case "get/ChannelList":
        outputJSON(getChannelList());
        break;
    case "get/Bills":
        outputJSON(getPendingBills());
        break;
    case "get/InvFlags":
        outputJSON(getInvFlags());
        break;

		case "get/DateofExpense":
        outputJSON(DateofExpense());
        break;
    case "get/Scheme":
        outputJSON(getSchemeDets());
        break;
 case "get/PriScheme":
        outputJSON(getPriSchemeDets());
        break;
		case "get/StockistScheme":
        outputJSON(getStockistSchemeDets());
        break;


 case "get/PriCaseScheme":
        outputJSON(getCaseSchemeDets());
        break;
		case "get/UnitConversion":
        outputJSON(getUnitConversion());
        break;

		case "get/Randomnumber":
        outputJSON(getRandomnumber());
        break;
    case "get/DailySales":
        outputJSON(getDailySalesSummry());
        break;
case "get/SlabDetails":
        outputJSON(getSlabDetails());
        break;

case "get/modetraveltype":
        outputJSON(getmodetraveltype());
        break;
case "get/GiftEnrolment":
        outputJSON(GiftEnrolment());
        break;
		case "get/Giftproduct":
        outputJSON(Giftproduct());
        break;
case "get/Todaycollectionreport":
    $sfCode = $_GET['sfCode'];
    $today = date('Y-m-d');
    $query = "exec GetToday_Collection_details '" . $sfCode . "','".$today."'";

outputJSON(performQuery($query));
        break;

case "get/LeaveAvailabilityCheck":
    $sfCode = $_GET['sfCode'];
    $Year = $_GET['Year'];
 $div = $_GET['divisionCode'];
if($div=="156,"){
	
 $query = "select SFCode,LeaveCode,LeaveValue,LeaveAvailability,LeaveTaken,Leave_SName,Leave_Name from MasEntitlement  inner join mas_Leave_Type l on Leave_code=LeaveCode 
 where SFCode='".$sfCode. "' and year(createDate)='".$Year."' and  l.Active_Flag in (0,3) and l.UsrCanAply=1 and l.DedTyp=0";
}
else
// $query = "select SFCode,LeaveCode,LeaveValue,LeaveAvailability,LeaveTaken,Leave_SName,Leave_Name from MasEntitlement  inner join mas_Leave_Type l on Leave_code=LeaveCode where SFCode='".$sfCode. "' and year(createDate)='".$Year."'";
$query="exec get_leave_det '".$sfCode. "','".$Year."' ";
$result=performQuery($query);
if (count($result) > 0) {
	outputJSON(performQuery($query));
}else{
	$query="select top 1 SFCode,LeaveCode,LeaveValue,LeaveAvailability,LeaveTaken,Leave_SName,Leave_Name from MasEntitlement";
	outputJSON(performQuery($query));
}




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

case "save/ProductBrand":
    $sfCode = $_GET['sfCode'];
    $ver = $_GET['ver'];
    $today = date('Y-m-d H:i:s');

	$div = $_GET['divisionCode'];
           $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $data = json_decode($_POST['data'], true);
    $query = "exec Sv_RCPA_Brand '" . $Owndiv . "','".$data["CompiName"]."','".$data["Compi_Code"]."' ,'".$data["Pname"]."' ";

    performQuery($query);

 $result["success"] = true;
 outputJSON($result);
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

 case "get/distords":
    $SF_code = $_GET['sfCode'];
	    $stockistCode = $_GET['stockist_code'];
		$order_date=$_GET['order_date'];
		if($stockistCode!=null)
	$query = "exec getDistPriOrder_Distws_app '".$SF_code."','".date('Y-m-d 00:00:00')."','".$stockistCode."'";
else
	$query = "exec getDistPriOrder_app '".$SF_code."','".$order_date."'";
	outputJSON(performQuery($query));
	break;
		 case "get/orderDespatchDetails":
    $SF_code = $_GET['sfCode'];
	   $order_date = $_GET['order_date'];
	    $order_from_date = $_GET['order_from_date'];
	$query = "exec getOrderDespatchDetails '".$SF_code."','".$order_date."','".$order_from_date."'";
	outputJSON(performQuery($query));
	break;
	 case "get/distords_sec":
    $SF_code = $_GET['sfCode'];
	 $code = $_GET['code'];
	  if($code==null)
		   $SF_code = $_GET['sfCode'];
	   else
		   $SF_code =  $code ;
	    $stockistCode = $_GET['stockist_code'];
		$orderDate = $_GET['order_date'];
		if($orderDate==null)
			$orderDate = date('Y-m-d 00:00:00');
		if($stockistCode!=null)
	$query = "exec getDistSecOrder_Distws_app '".$SF_code."','".$orderDate."','".$stockistCode."'";
else
	$query = "exec getDistSecOrder_app1 '".$SF_code."','".$orderDate."'";
	outputJSON(performQuery($query));
	break;
case "get/geotag":
	outputJSON(ViewGeoTag());
break;
case "save/geotag":
	outputJSON(SaveGEOTagging());
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
     $query = "select   Stockist_Name Cus_Name ,sum (Collected_Amount) Amount,Pay_Type PaymentName from trans_priorder_head  PH inner join mas_stockist Ms on MS.Stockist_Code=PH.Stockist_Code where PH.sf_code='" .  $SF_code  . "' and convert(date,Order_Date)='" .  $today   . "'   group by PH.Stockist_Code,Pay_Type,Collected_Amount,Stockist_Name";

               outputJSON(performQuery($query));

                break;


	case "get/RetailerPendingList":
	$SF_code = $_GET['sfCode'];
	$query = "select ListedDr_Active_Flag,case when ListedDr_Active_Flag='4' then 'rgb(255,0,0)  !important' when ListedDr_Active_Flag=0 then 'rgb(3,192,60)  !important'  else 'rgb(211 146 17)  !important' end StusClr,case when ListedDr_Active_Flag='4' then 'Reject' when ListedDr_Active_Flag='0' then  'Approve' else 'Pending' end [Status], ListedDr_Name [Name] ,Territory_Name [Route],isnull(reject_remarks,'') Reason from Mas_ListedDr  DR inner join Mas_Territory_Creation TC on TC.Territory_Code=DR.Territory_Code where   charindex(',$SF_code,',','+Dr.Sf_Code+',')>0 and ListedDr_Active_Flag in ('0','3','4') and Entry_Mode='App'";
	outputJSON(performQuery($query));
	break;

	case "get/RetailerCollection":
      $SF_code = $_GET['sfCode'];
      $today = date('Y-m-d');
      $query = "select   Stockist_Name Cus_Name ,sum (Collected_Amount) Amount,Pay_Type PaymentName from trans_priorder_head  PH inner join mas_stockist Ms on MS.Stockist_Code=PH.Stockist_Code where PH.sf_code='" .  $SF_code  . "' and convert(date,Order_Date)='" .  $today   . "'   group by PH.Stockist_Code,Pay_Type,Collected_Amount,Stockist_Name";
     outputJSON(performQuery($query));

    break;
	case "get/vansalescollection":
$SF_code=$_GET['sfCode'];
      $today = date('Y-m-d');
     $query = "select Cust_Id,Cus_Name,PaymentName,sum (Amount) Amount from Trans_Payment_Detail where Sf_Code='" .  $SF_code  . "' and convert(date,eDate)='" .  $today   . "'   group by Cust_Id,Cus_Name,PaymentName";

             outputJSON(performQuery($query));

                break;
case "get/LitterVsValues":
      $SF_code = $_GET['SF_Code'];

      $today =  $_GET['Date'];

      $query = "exec getLittervsValue '" .  $SF_code  . "' ,'" .  $today . "' ";

     outputJSON(performQuery($query));


            break;

case "get/retailergiftStatus":
      $SF_code = $_GET['sfCode'];

      $query = "select * from vwDoctor_SlabMaster_APP where  charindex(',$SF_code,',','+Field_Code+',')>0 ";

     outputJSON(performQuery($query));

            break;

case "get/EndInventoryCheck":
      $SF_code = $_GET['sfCode'];
	  $today = date('Y-m-d');
      $query = "select count(SLNo)CountCk from Trans_Prod_DailyEndInventory where SFCode='" .  $SF_code  . "' and convert(date,EntryDt)='".$today."'";

     outputJSON(performQuery($query));

       break;

case "get/GoogleLogin":
			 $Email = $_GET['Email'];
			$sql = "SELECT Sf_UserName,Sf_Password  from mas_salesforce where SF_Email='".$Email."' ";
             $res = performQuery($sql);

			if (count($res) > 0) {
				$result["Data"]=$res;
				$result["success"]=true;
				 $result["Query"]=$sql;
			}else{
			   $result["Query"]=$sql;
			   $result["success"]=false;
			 }
		outputJSON($result);

break;

case "get/giftList":

			$sql = "select * from Gift_Master";
             $res = performQuery($sql);

			if (count($res) > 0) {
				$result["Data"]=$res;
				$result["success"]=true;
				 //$result["Query"]=$sql;
			}else{
			   //$result["Query"]=$sql;
			   $result["success"]=false;
			 }
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

		$sql = "insert into tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr,Auc,deg,DvcID,Battery,Mock) select '$sfCode','$empid','$employeeid','" . $date . "','" . $lat . "','" . $lng . "','null','" . $acc . "','null','null','".$bat."','".$mock."'";

 performQuery($sql);
$result["success"]=true;
outputJSON($result);

	break;

	case "save/trackdetail";
        outputJSON(svTrackDetail());
        break;
	case "get/getGiftApproval":
      $SF_code = $_GET['SF_Code'];

     $today = date('Y-m-d');

      $query = "exec getGiftApproval '" .  $SF_code  . "' ,'" .  $today . "' ";

     outputJSON(performQuery($query));

            break;
			case "sendsms":
			sendSMS();
			break;

			case "get/orderDetails":
			 $Sf_code = $_GET['sfCode'];
			 $Order_Id = $_GET['orderNo'];

			$sql = "exec get_pri_order_dts '".$Order_Id."','".$Sf_code."' ";
            $res = performQuery($sql);
			outputJSON($res);
			break;
	case "get/ViewReport_Details":
			 $Sf_code = $_GET['Sf_code'];
			 $Order_Id = $_GET['Order_Id'];

			$sql = "exec get_pri_order_dts '".$Order_Id."','".$Sf_code."' ";
            $res = performQuery($sql);
			$result["Data"]=$res;
			outputJSON($result);
			break;
	case "get/secviewreport_details":
			$Sf_code = $_GET['Sf_code'];
			$Order_Id = $_GET['Order_Id'];

			$sql = "exec get_sec_order_dts_native '".$Order_Id."','".$Sf_code."' ";
            $res = performQuery($sql);
			$result["Data"]=$res;
			outputJSON($result);
			break;
			case "apply/redeem":

        	$data = json_decode($_POST['data'], true);
       	    $custCode =  $data['cus_code'];
    		$cusAddress =  $data['cus_address'];
    		$giftId =  $data['gift_id'];
    		$redeemDate =  $data['redeem_date'];
    		$redeemPoints =  $data['redeem_points'];
    		$redeemFlag =  $data['redeem_flag'];
    		$redeemDeliveryDate = $data['redeem_delivery_date'];
            $redeemCancelDate = $data['redeem_cancel_date'];

            $sql = "SELECT isNull(max(Redeem_Id),0)+1 as Redeem_Id FROM Trans_Redeem_Gift_Rewards";
            $tRw = performQuery($sql);
//{"cus_code": "527851", "cus_address":"no. 1", "gift_id":"1", "redeem_date":"",
//"redeem_points":221, "redeem_flag":"1", "redeem_delivery_date":"2021-11-01", "redeem_cancel_date":"2021-11-01"}
			$tRwCnt = (int)$tRw[0]['Redeem_Id'];
    		$sql = "insert into Trans_Redeem_Gift_Rewards (Redeem_Id,Cus_Code,Cus_Address,Gift_Id,Redeem_Dt,Redeem_Points,Redeem_flag,Redeem_delv_Dt,Redeem_Cancel_Dt)
                    values($tRwCnt,'$custCode','$cusAddress','$giftId',convert(datetime,'$redeemDate',103),'$redeemPoints',$redeemFlag,convert(datetime,'$redeemDeliveryDate',103),convert(datetime,'$redeemCancelDate',103))";

     performQuery($sql);
    $result["success"]=true;
	$result["query"]=$sql;
    outputJSON($result);
    	break;
	case "get/slidegroups":
		outputJSON(getSlidesGroups());
    	break;
	case "get/slides":
		outputJSON(getSlidesDetails());
		break;
	case "save/slidetrackdetails":
		outputJSON(saveSlidesTrack());
		break;
	case "get/detailinggroupwisemonthly_rpt":
		outputJSON(getDetailingGroupwiseMonthlyRpt());
		break;
	case "get/detailingslides_rpt":
		outputJSON(getDetailingSlidesRpt());
    	break;
	case "get/paymentmode":
    	outputJSON(getPaymentTypeList());
    	break;
}
?>
