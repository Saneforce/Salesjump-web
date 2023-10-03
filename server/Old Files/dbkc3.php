<?php

header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

function send_gcm_notify($reg_id, $message) {
    define("GOOGLE_API_KEY", "AIzaSyAqfZ3MIMN418_WkoRMpAHz-399EfsW_XQ");
    define("GOOGLE_GCM_URL", "https://android.googleapis.com/gcm/send");

    $fields = array(
        'registration_ids' => array($reg_id),
        'data' => array("message" => $message, "title" => "Leave Application"),
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
    sqlsrv_close();
    $respon = array();
    $count = count($arr);
    if ($count == 1) {
        $respon['success'] = true;
        $respon['sfCode'] = $arr[0]['SF_Code'];
$date=date("Y-m-d H:i");
$sql="insert into logindetails select '" . $arr[0]['SF_Code'] . "','$date',''";
performQuery($sql);
        $sfName = utf8_encode(trim(preg_replace("/[\r\n]+/", " ", $arr[0]['SF_Name'])));
        $respon['sfName'] = $sfName;
        $respon['divisionCode'] = $arr[0]['Division_Code'];
        $respon['call_report'] = $arr[0]['call_report'];
        $respon['desigCode'] = $arr[0]['desig_Code'];
        $respon['HlfNeed'] = $arr[0]['MGRHlfDy'];
        if ($arr[0]['desig_Code'] == "MR") {
            $query = "Select count(SF_Code) Cnt from Salesforce_Master where sf_Tp_Reporting='" . $arr[0]['SF_Code'] . "'";
            $dsgc = performQuery($query);
            if ($dsgc[0]['Cnt'] > 0)
                $respon['desigCode'] = "AM";
            else
                $respon['HlfNeed'] = $arr[0]['MRHlfDy'];
        }
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
    if ($desig == "stockist")
        $query = "exec getAppProdStockist"; //,'".$DivisionCode."'";
    else
        $query = "exec getAppProd '" . $sfCode . "'"; //,'".$DivisionCode."'";
    return performQuery($query);
}

function getAPPSetups() {
    $rqSF = $_GET['rSF'];
    $userType = $_GET['userType'];
    $query = "exec getAPPSetups '" . $rqSF . "'";
    return performQuery($query);
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
    $query = "select BrandName,sum(quantity) OQty,sum(orderValue) OVal from vwBrandWiseOrder where orderDate='$date' and $str group by BrandName";
    $daywise = performQuery($query);
    $query = "select BrandName,sum(quantity) TOQty,sum(orderValue) TOVal from vwBrandWiseOrder where MONTH(orderDate) = MONTH('$date') and YEAR(orderDate) = YEAR('$date') and $str group by BrandName";
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

function getSubordinateMgr() {
    $sfCode = $_GET['rSF'];
    $param = array($sfCode);
    $query = "exec getHyrSF_APP '" . $sfCode . "'";
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
function getDayPlan(){
 $sfCode = $_GET['rptSF'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getDayPlan '" . $sfCode . "','" . $dyDt . "'";

    return performQuery($query);
}
function getMonthSummary() {
    $sfCode = $_GET['rptSF'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getMonthSummaryApp '" . $sfCode . "','" . $dyDt . "'";

    return performQuery($query);
}

function getDayReport() {
    $sfCode = $_GET['sfCode'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getDayReportApp '" . $sfCode . "','" . $dyDt . "'";
    return performQuery($query);
}

function getVstDets() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
    $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";
    return performQuery($query);
}

function getVwOrderDet() {
    $dcrCode = $_GET['DCR_Code'];
    $query = "select * from vwOrderDetails where Dcr_Code=$dcrCode";
    return performQuery($query);
}

function getCurrentStockDet() {
    if ($_GET['scode'] == "0")
        $SF = $_GET['sfCode'];
    else
        $SF = $_GET['scode'];
    $query = "select * from vwTransCurrentStock where Stockist_Code='$SF'";
    $transHead = performQuery($query);
    return outputJSON($transHead);
}

function getPreCallDet() {
    $SF = $_GET['sfCode'];
    $MSL = $_GET['Msl_No'];

    $result = array();
    $query = "select SLVNo SVL,Doc_Cat_ShortName DrCat,Doc_Spec_ShortName DrSpl,isnull(stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory S where CHARINDEX(cast(Doc_SubCatCode as varchar),D.Doc_SubCatCode)>0 for XML Path('')),1,2,''),'') DrCamp,isnull(stuff((select ', '+Product_Detail_Name from Map_LstDrs_Product M	inner join Mas_Product_Detail P on M.Product_Code=P.Product_Detail_Code and P.Division_Code=M.Division_Code where Listeddr_Code=D.ListedDrCode for XML Path('')),1,2,''),'') DrProd from mas_listeddr D where ListedDrCode='" . $MSL . "'";
    $as = performQuery($query);

    if (count($as) > 0) {

        $result['SVL'] = (string) $as[0]['SVL'];
        $result['DrCat'] = (string) $as[0]['DrCat'];
        $result['DrSpl'] = (string) $as[0]['DrSpl'];
        $result['DrCamp'] = (string) $as[0]['DrCamp'];
        $result['DrProd'] = (string) $as[0]['DrProd'];

        $result['success'] = true;
        $query = "select SUM(Order_value) sumoforder,stockist_code from vwTransOrderHead where id<7 and Cust_Code=$MSL and Sf_Code='$SF' group by stockist_code order by stockist_code";
        $transHead = performQuery($query);

        $query = "select Stockist_Code,Last_Order_Amt,Out_standing_Amt from Order_Collection_Details where Cust_Code=$MSL and Sf_Code='$SF'  order by Stockist_Code";
        $collectionOrder = performQuery($query);

        if (count($collectionOrder) > 0) {
            for ($i = 0; $i < count($collectionOrder); $i++) {
                $stockist[] = array(
                    "stockist_code" => $transHead[$i]['stockist_code'],
                    "Sum" => $transHead[$i]['sumoforder'],
                    "Avg" => floor($transHead[$i]['sumoforder'] / 6),
                    "LastOrderAmt" => $collectionOrder[$i]['Last_Order_Amt'],
                    "outStandingAmt" => $collectionOrder[$i]['Out_standing_Amt']
                );
            }
        }
        $result['StockistDetails'] = $stockist;
        $query = "select Trans_SlNo,Trans_Detail_Slno,convert(varchar,Activity_Date,0) Adate,convert(varchar,cast(convert(varchar,Activity_Date,101)  as datetime),20) as DtTm,(Select content from vwFeedTemplate where ID=Rx) CalFed,Activity_Remarks,products,gifts from vwLastVstDet where rw=1 and Trans_Detail_Info_Code='" . $MSL . "' and SF_Code='" . $SF . "'";
        $as = performQuery($query);
        
if (count($as) > 0) {
            $result['LVDt'] = date('d / m / Y g:i a', strtotime((string) $as[0]['DtTm']));
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
    $query = "select isnull((SELECT worktype_name Half_Day_FW from vwActivity_Report where Half_Day_FW=2 and sf_code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)),'') as halfdaywrk";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) hospital_count from vwActivity_CSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=5";
    $temp = performQuery($query);
    $results[] = $temp[0];
$query = "select Count(sl_no) door_count from DoorToDoor D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(H.activity_date as datetime)=cast('$today' as datetime)";
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

    //if($tableName=="vwActivity_Unlst_Detail") echo($query);
    return performQuery($query);
}

function getFromTable($tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null, $desig) {
    $query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
    if (!is_null($sfCode)) {
        if ($desig == "mgr" && ($tableName == 'vwTown_Master_APP' || $tableName == "vwDoctor_Master_APP"))
            $query .=" WHERE tab.Field_Code='$sfCode'";
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

//	if(strtolower($tableName)=="vwActivity_Unlst_Detail") echo($query);
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
        $sql = "select Trans_Order_No from Trans_Order_Details where Trans_Sl_No='$Trans_Sl_No'";
        $tr = performQuery($sql);
        for ($i = 0; $i < count($tr); $i++) {
            $Trans_Order_No = $tr[$i]['Trans_Order_No'];

            $sql = "delete from Trans_Order_Details WHERE Trans_Order_No='$Trans_Order_No'";
            performQuery($sql);
        }
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

        /* $sql = "DELETE FROM DCREvent_Captures where Trans_Detail_Slno='".$amc."'";performQuery($sql); */
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
//        $sql = "select * from vwDistReport where Sf_Code='$sfCode' and cast(convert(varchar,Order_Date,101) as datetime)='$dyDt' and Stockist_Code=$stockist";
        $sql = "select * from vwDistReport where cast(convert(varchar,Order_Date,101) as datetime)='$dyDt' and Stockist_Code=$stockist";
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
$sql = "DELETE FROM DCRDetail_Distributors_Hunting where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
$sql = "DELETE FROM DoorToDoor where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
    $sql = "DELETE FROM DCREvent_Captures where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCRMain_Temp where SF_Code='" . $SF . "' and lower(Work_Type) <> lower(" . $WT . ") and cast(activity_date as datetime)=cast('$Dt' as datetime)";
    performQuery($sql);
    $sql = "DELETE FROM DCRMain_Trans where SF_Code='" . $SF . "' and lower(Work_Type) <> lower(" . $WT . ") and cast(activity_date as datetime)=cast('$Dt' as datetime)";
    performQuery($sql);

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

    $sql = "SELECT Employee_Id,case sf_type when 4 then 'MR' else 'MGR' End SF_Type FROM vwUserDetails where SF_code='" . $sfCode . "'";
    $as = performQuery($sql);
    $IdNo = (string) $as[0]['Employee_Id'];
    $SFTyp = (string) $as[0]['SF_Type'];

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
				/*
				if($cbQty<=0 && $tRw[0]['Cl_Qty']>0) $cbQty=$tRw[0]['Cl_Qty'];
				if($pieces<=0 && $tRw[0]['pieces']>0) $pieces=$tRw[0]['pieces'];*/
			}

			$sql = "delete Trans_Stock_Updation_Details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "' and Purchase_Date='" . $date . "'";
			performQuery($sql);

                $sql = "insert into Trans_Stock_Updation_Details(Tran_Slno,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Distributer_Rate,Retailor_Rate,Purchase_Date,Conversion_Qty,SfCode) select '" . $pk . "','" . $sfCode . "','" . $productCode . "','" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",$distPrice,$retPrice,'" . $date . "',$sampleErpCode,'$staffCode'";
                performQuery($sql);
                
                $sql = "SELECT Stockist_Code FROM Trans_Current_Stock_details where Stockist_Code='$sfCode' and Product_Code='$productCode'";
                $tRw = performQuery($sql);
                if (!empty($tRw)) {
                    $sql = "DELETE FROM Trans_Current_Stock_details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "'";
                    performQuery($sql);
                }
                $sql = "insert into Trans_Current_Stock_details(Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,pieces,Division_Code,Last_Updation_Date,Conversion_Qty,SfCode) select '" . $sfCode . "','" . $productCode . "','" . $productName . "',$recvQty,$cbQty,$pieces," . $Owndiv . ",'" . $date . "',$sampleErpCode,'$staffCode'";
                performQuery($sql);
                $sql = "SELECT isNull(max(Sale_Code),0)+1 as RwID FROM Trans_Secondary_Sales_Details";
                $tRw = performQuery($sql);
                $pk = (int) $tRw[0]['RwID'];

                if (($pieces-$OpP_Qty) > 0) {
                    //$saleQty = (($recvQty + $Op_Qty) - ($cbQty + 1)) . "." . ($sampleErpCode - $pieces);
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
case "LoginUpdateDetails":
$date=date("Y-m-d H:i");
$sql="update logindetails set logout_date='$date' where sf_code='" . $sfCode . "' and slno=(select max(slno) from logindetails)";
performQuery($sql);
break;
case "LoginDetails":
$date=date("Y-m-d H:i");
$sql="insert into logindetails select '" . $sfCode . "','$date',''";
performQuery($sql);
break;
        case "tbMyDayPlan":
            orderDetailsDeleteOtherWorkType($sfCode);
			$ww=$vals["worked_with"];
            $sql = "insert into tbMyDayPlan select '" . $sfCode . "'," . $vals["sf_member_code"] . ",'" . date('Y-m-d H:i:s') . "'," . $vals["cluster"] . "," . $vals["remarks"] . ",'" . $Owndiv . "'," . $vals["wtype"] . "," . $vals["FWFlg"] . "," . $vals["ClstrName"] . "," . $vals["stockist"].",$ww,(Select SF_Name+', ' from vwUserDetails where charindex('$$'+sf_Code+'$$','$$'+$ww+'$$')>0 for XML path(''))";
            performQuery($sql);
            if (str_replace("'", "", $vals["FWFlg"]) != "F"&&str_replace("'", "", $vals["FWFlg"]) != "HG") {
                $sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "'  and cast(activity_date as datetime)=cast('$today' as datetime)";
                $result1 = performQuery($sql);
                if (count($result1) > 0) {
                    if ($result1[0]['FWFlg'] == 'L' && $result1[0]['Confirmed'] != 2 && $result1[0]['Confirmed'] != 3) {
                        $result = 'Leave Post Already Updated';
                        outputJSON($result);
                        die;
                    } else {
                        delAREntry($sfCode, $vals["wtype"], $today);

                        $ARCd = "0";
                        $sql = "{call  svDCRMain_App(?,?," . $vals["wtype"] . ",'" . str_replace("'", "", $vals["cluster"]) . "',?,'" . str_replace("'", "", $vals["remarks"]) . "',?)}";
                        $params = array(array($sfCode, SQLSRV_PARAM_IN),
                            array($today, SQLSRV_PARAM_IN),
                            array($Owndiv, SQLSRV_PARAM_IN),
                            array($ARCd, SQLSRV_PARAM_OUT));
                        performQueryWP($sql, $params);
                    }
                } else {
                    delAREntry($sfCode, $vals["wtype"], $today);

                    $ARCd = "0";
                    $sql = "{call  svDCRMain_App(?,?," . $vals["wtype"] . ",'" . str_replace("'", "", $vals["cluster"]) . "',?,'" . str_replace("'", "", $vals["remarks"]) . "',?)}";
                    $params = array(array($sfCode, SQLSRV_PARAM_IN),
                        array($today, SQLSRV_PARAM_IN),
                        array($Owndiv, SQLSRV_PARAM_IN),
                        array($ARCd, SQLSRV_PARAM_OUT));
                    performQueryWP($sql, $params);
                }
            }
            break;
 case "Mas_Territory_Creation":
$sql = "SELECT isNull(max(Territory_Code),0)+1 as RwID FROM Mas_Territory_Creation";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
 $sql = "insert into Mas_Territory_Creation(Territory_Code,Territory_Name,Division_Code,SF_Code,Territory_Active_Flag,Created_date,Dist_Name,Target,Min_Prod,Route_Code,Town_Code,Population) select '" . $pk . "'," . $vals["Territory_Name"] . ",$Owndiv," . $vals["SF_Code"] . ",0,'" . date('Y-m-d H:i:s') . "'," . $vals["Dist_Name"] . "," . $vals["Target"] . "," . $vals["Min_Prod"] . "," . $vals["Route_Code"] . "," . $vals["Town_Code"] . "," . $vals["Population"] . "";
     
   performQuery($sql);

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
 $sql = "update DCRDetail_Distributors_Hunting set Shop_Name=" . $vals['name'] . ",Contact_Person=" . $vals['contact'] . ",Phone_Number=" . $vals['phone'] . ",area=" . $vals["area"] . ",remarks=" . $vals["remarks"] . ",address=" . $vals["address"] . " where slno=".$vals['slno']."";
         performQuery($sql);
}

break;
case "distributor":
$sfCode=$_GET['sfCode'];
$state=$_GET['State_Code'];
 $sql = "{call  svNewDistributor('$sfCode',$Owndiv,$state," . $vals['name'] . "," . $vals['contact'] . "," . $vals['phone'] . "," . $vals['address'] . "," . $vals['dist_code'] . "," . $vals['dist_name'] . "," . $vals['username'] . "," . $vals['password'] . ")}";

 performQueryWP($sql);
break;

        case "unlisted_doctor_master":
            $sql = "{call  svListedDR_APP('$sfCode'," . $vals['unlisted_doctor_name'] . "," . $vals['unlisted_doctor_address'] . "," . $vals['unlisted_doctor_phone'] . "," . $vals["unlisted_specialty_code"] . "," . $vals["unlisted_cat_code"] . "," . $vals["town_code"] . ",'0','" . date('Y-m-d H:i:s') . "',$Owndiv," . $vals["unlisted_class"] . ",0," . $vals['wlkg_sequence'] . "," . $vals['Contact_Person_Name'] . ")}";

//            $sql = "SELECT isNull(max(UnListedDrCode),0)+1 as RwID FROM Mas_UnListedDr";
//            $tRw = performQuery($sql);
//            $pk = (int) $tRw[0]['RwID'];
//
//            $sql = "insert into Mas_UnListedDr(UnListedDrCode,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code,Territory_Code,UnListedDr_Active_Flag,UnListedDr_Sl_No,Division_Code,SLVNo,Doc_QuaCode,Doc_ClsCode,Sf_Code,UnListedDr_Created_Date,Created_By) select '" . $pk . "'," . $vals["unlisted_doctor_name"] . ",''," . $vals["unlisted_specialty_code"] . "," . $vals["unlisted_cat_code"] . "," . $vals["town_code"] . ",0,'" . $pk . "','" . $Owndiv . "','" . $pk . "'," . $vals["unlisted_qulifi"] . "," . $vals["unlisted_class"] . ",'" . $sfCode . "','" . date('Y-m-d H:i:s') . "','Apps'";
            performQueryWP($sql);
            break;
        case "locationDetails":
            $sql = "select sf_emp_id,Employee_Id from Mas_Salesforce where Sf_Code='$sfCode'";
            $sf = performQuery($sql);
            $empid = $sf[0]['sf_emp_id'];
            $employeeid = $sf[0]['Employee_Id'];
            for ($i = 0; $i < count($vals); $i++) {
                $lng = $vals[$i]['longitude'];
                $lat = $vals[$i]['lattitude'];
                $address = getaddress($lat, $lng);
                $sql = "insert into tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr) select '$sfCode','$empid','$employeeid','" . $vals[$i]['date'] . "','$lat','$lng','$address'";

                performQuery($sql);
            }
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

            $sql = "insert into Trans_FM_Expense_Head(Sf_Code,Month,Year,sndhqfl,Division_Code,snd_dt,Sf_Name) select '$sfCode',MONTH('$date'),YEAR('$date'),1,$divisionCode[0],'$date'," . $sfName . "";
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
//            $market = $data[0]['Tour_Plan']['market'];
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
            $HQ_Name = str_replace(")", "", str_replace("(", "", $data[0]['Tour_Plan']['HQ_Name']));
            $sql = "delete from Trans_TP_One WHERE SF_Code ='" . $sfCode . "' and Tour_Date=cast($tourDate as datetime)";
            performQuery($sql);
            $sql = "insert into Trans_TP_One(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Tour_Schedule1,Objective,Worked_With_SF_Code,Division_Code,Territory_Code1,Worked_With_SF_Name,TP_Sf_Name,HQ_Code,HQ_Name,Confirmed,Change_Status) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),'" . date('Y-m-d') . "',$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,$HQ_Code,$HQ_Name,0,0";
            performQuery($sql);
            $resp["success"] = true;
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
            $sql = "update Mas_Leave_Form set Leave_Active_Flag=0 where Leave_Id=$leaveid";
            performQuery($sql);
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
            break;
        case "LeaveReject":
            $leaveid = $_GET['leaveid'];
            $sql = "update Mas_Leave_Form set Leave_Active_Flag=1,Rejected_Reason=" . $vals['reason'] . " where Leave_Id=$leaveid";
            performQuery($sql);
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
            break;
        case "LeaveForm":
            $name = $_GET['sf_name'];

            $sql = "SELECT isNull(max(Leave_Id),0)+1 as RwID FROM Mas_Leave_Form";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
            $sql = "insert into Mas_Leave_Form(Leave_Id,Leave_Type,From_Date,To_Date,Reason,sf_code,Division_Code,Leave_Active_Flag,Created_Date,No_of_Days,Address) select '$pk'," . $vals['Leave_Type'] . ",'" . $vals['From_Date'] . "'," . $vals['To_Date'] . "," . $vals['Reason'] . ",'$sfCode','$Owndiv',2,'" . date('Y-m-d') . "'," . $vals['No_of_Days'] . "," . $vals['address'] . "";
            performQuery($sql);
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce where Sf_Code='$sfCode')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                //   $msg = $name . " Applied Leave for " . $vals['No_of_Days'] . " days";
                $msg = "Leave Application Received";
                send_gcm_notify($reg_id, $msg);
            }
            $sql = "SELECT sf_type FROM Mas_Salesforce where Sf_Code='$sfCode'";

            $sfType = performQuery($sql);
            $days = $vals['No_of_Days'];
            $date = $vals['From_Date'];
            for ($i = 1; $i <= $days; $i++) {
                $query = "exec ChkandPostLeaveDt 0,'$sfCode'," . $sfType[0]['sf_type'] . ",$Owndiv,'$date','','apps'";
                $results = performQuery($query);
                $date = date('Y-m-d', strtotime($date . ' + 1 days'));
            }
            $resp["success"] = true;
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

//            die;
            $sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "' and lower(Work_Type) <>lower(" . $vals["Worktype_code"] . ")  and cast(activity_date as datetime)=cast('$today' as datetime)";

            $result1 = performQuery($sql);

            if (count($result1) > 1) {
                if (!isset($_GET['replace'])) {
                    $result = array();
                    $result['success'] = false;
                    if ($result1[0]['FWFlg'] == 'L' && $result1[0]['Confirmed'] != 2 && $result1[0]['Confirmed'] != 3) {
                        $result['type'] = 2;
                        $result['msg'] = 'Leave Post Already Updated';
                    } else {
                        $result['type'] = 1;
                        $result['msg'] = 'Already There is a Data For other Work do you want to replace....?';
                    }
                    $result['data'] = $data;
                    outputJSON($result);
                    die;
                } else {

                    delAREntry($sfCode, $vals["Worktype_code"], $today);
                }
            }

            $pProd = '';
            $npProd = '';
            $pGCd = '';
            $pGNm = '';
            $pGQty = '';
            $SPProds = '';
            $nSPProds = '';
            $Inps = '';
            $nInps = '';
            $vTyp = 0;

            for ($i = 1; $i < count($data); $i++) {
                $tableData = $data[$i];

                if (isset($tableData['Activity_Doctor_Report'])) {
                    $vTyp = 1;
                    $DetTB = $tableData['Activity_Doctor_Report'];
                    $cCode = $DetTB["doctor_code"];
                    $vTm = $DetTB["Doc_Meet_Time"];
                    $pob = $DetTB["Doctor_POB"];
                    $POB_Value = $DetTB["orderValue"];
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

                    $sql = "SELECT stockiest_name name from vwstockiest_Master_APP where Distributor_Code=" . $cCode;
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
if(isset($tableData['DoorToDoor'])){
 $vTyp = 5;



}
if(isset($tableData['survey'])){
 $vTyp = 6;



}
if(isset($tableData['DCRDetail_Distributors_Hunting'])){
 $vTyp = 7;



}

                $tRw = performQuery($sql);
                $cName = $tRw[0]["name"];

                if (isset($tableData['Activity_Sample_Report']) || isset($tableData['Activity_Unlistedsample_Report'])) {

                    if (isset($tableData['Activity_Sample_Report']))
                        $samp = $tableData['Activity_Sample_Report'];
                    if (isset($tableData['Activity_Unlistedsample_Report']))
                        $samp = $tableData['Activity_Unlistedsample_Report'];

                    for ($j = 0; $j < count($samp); $j++) {
                        if ($j < 3) {
                            $pProd = $pProd . (($pProd != "") ? "#" : '') . $samp[$j]["product_code"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"];
                            $npProd = $npProd . (($npProd != "") ? "#" : '') . $samp[$j]["product_Name"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"];
                        } else {
                            $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"].  "#";
                            $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"]. "#";
                        }
                    }
                }

                if (isset($tableData['Activity_POB_Report'])) {

                    if (isset($tableData['Activity_POB_Report']))
                        $samp = $tableData['Activity_POB_Report'];
                    if (isset($tableData['Activity_Stk_POB_Report']))
                        $samp = $tableData['Activity_Stk_POB_Report'];

                    for ($j = 0; $j < count($samp); $j++) {
                        $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Qty"] . "#";
                        $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Qty"] . "#";
                    }
                }
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


            $ARCd = 0;
            $ARDCd = (strlen($_GET['amc']) == 0) ? "0" : $_GET['amc'];
            $sql = "{call  svDCRMain_App1(?,?," . $vals["Worktype_code"] . ",'" . str_replace("'", "", $vals["Town_code"]) . "',?,'" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?)}";
      
  $params = array(array($sfCode, SQLSRV_PARAM_IN),
                array($today, SQLSRV_PARAM_IN),
                array($Owndiv, SQLSRV_PARAM_IN),
                array($ARCd, SQLSRV_PARAM_OUT));
   
            $tr = performQueryWP($sql, $params);


            $loc = explode(":", str_replace("'", "", $DetTB["location"]) . ":");
            $lat = $loc[0]; //latitude
            $lng = $loc[1]; //longitude
            $address = getaddress($lat, $lng);
            if ($address) {
                $DetTB["geoaddress"] = $address;
            } else {
                $DetTB["geoaddress"] = "NA";
            }
            $sqlsp = "{call  ";
            if ($vTyp != 0) {
                if ($vTyp == 2 || $vTyp == 3)
                    $proc = "svDCRCSHDet_App";

                if ($pob == '')
                    $pob = '0';
                if ($POB_Value == '')
                    $POB_Value = '0';
                $sqlsp = $sqlsp . $proc . " (?,?,?," . $vTyp . ",$cCode,'" . $cName . "'," . $vTm . "," . $pob . ",'" . str_replace("'", "", $DetTB["Worked_With"]) . "',?,?,?,?,";
                if ($vTyp == 1 || $vTyp == 4)
                    $sqlsp = $sqlsp . "?,?,?,?,?,";
                $sqlsp = $sqlsp . "'" . str_replace("'", "", $vals["Town_code"]) . "','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,'" . str_replace("'", "", $vals["rx_t"]) . "'," . $DetTB["modified_time"] . ",?,?," . $vals["DataSF"] . ",'" . $DetTB["geoaddress"] . "'," . $POB_Value . ",$net_weight_value,".$DetTB['stockist_code'].",".$DetTB['stockist_name'].")}";

                $params = array(array($ARCd, SQLSRV_PARAM_IN),
                    array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_NVARCHAR(50)),
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

if($vTyp==5){

$sql = "{call  svDoorToDoor_Dup('" . $sfCode . "'," . $vals["DataSF"] . ",'" . date('Y-m-d H:i:s') . "'," . $tableData['DoorToDoor']["Town_code"] . "," . $vals["Daywise_Remarks"] . ",'" . $Owndiv . "'," . $tableData['DoorToDoor']["Town_name"] . "," . $tableData['DoorToDoor']["stockist"].",".$tableData['DoorToDoor']["Worked_With"].",".$tableData['DoorToDoor']['numberofcoupens'].",".$tableData['DoorToDoor']['imagString'].",".$tableData['DoorToDoor']['doctor_code'].", ".$tableData['DoorToDoor']['doctor_name'].")}";

 performQueryWP($sql);
 $resp["success"] = true;
    echo json_encode($resp);die;
}
else if($vTyp==7){

$sql = "{call  svNewDistirbutorsHunting_Dup(" . $tableData['DCRDetail_Distributors_Hunting']['name'] . "," . $tableData['DCRDetail_Distributors_Hunting']['contact'] . "," . $tableData['DCRDetail_Distributors_Hunting']['phone'] . "," . $tableData['DCRDetail_Distributors_Hunting']["area"] . "," . $tableData['DCRDetail_Distributors_Hunting']["remarks"] . "," . $tableData['DCRDetail_Distributors_Hunting']["address"] . ",'" . date('Y-m-d H:i:s') . "','$sfCode'," . $vals["DataSF"] . ",".$tableData['DCRDetail_Distributors_Hunting']["Worked_With"].")}";

 performQueryWP($sql);
 $resp["success"] = true;
    echo json_encode($resp);die;
}
else if($vTyp==6){
  $loc = explode(":", str_replace("'", "", $vals["location"]) . ":");
            $lat = $loc[0]; //latitude
            $lng = $loc[1]; //longitude
            $address = getaddress($lat, $lng);
            if ($address) {
                $address = $address;
            } else {
                $address = "NA";
            }
$sql="select isnull(max(ListedDrCode),0)+1 doctorCode from Mas_ListedDr";
$tr=performQuery($sql);
$doctorCode=$tr[0]['doctorCode'];

$RCPAdetails=$tableData['survey']['RCPAdetails'];

            $sql = "{call  svSurvey_Dup('$sfCode'," . $tableData['survey']['unlisted_doctor_name'] . "," . $tableData['survey']['unlisted_doctor_address'] . "," . $tableData['survey']['unlisted_doctor_phone'] . "," . $tableData['survey']["unlisted_specialty_code"] . "," . $tableData['survey']["unlisted_cat_code"] . "," . $tableData['survey']["sdp"] . ",'0','" . date('Y-m-d H:i:s') . "',$Owndiv," . $tableData['survey']["unlisted_class"] . ",0," . $tableData['survey']['wlkg_sequence'] . "," . $tableData['survey']['Contact_Person_Name'] . "," . $tableData['survey']['Worked_With'] . "," . $tableData['survey']['dateStr'] . "," . $tableData['survey']['mdateStr'] . ",'$lat','$lng','$address'," . $tableData['survey']['sdp'] . "," . $tableData['survey']['sdp_name'] . "," . $vals['DataSF'] . ",'$doctorCode')}";

performQueryWP($sql);
for($i=0;$i<count($RCPAdetails);$i++){
$id=$RCPAdetails[$i]['CpmptId'];
$name=$RCPAdetails[$i]['CpmptName'];
$litters=$RCPAdetails[$i]['CpmptLitters'];
$pouch=$RCPAdetails[$i]['CpmptPouch'];
$cup=$RCPAdetails[$i]['CpmptCup'];
 $sql = "insert into tbRCPADetails select '" . $sfCode . "','" . date('Y-m-d H:i:s') . "'," . $tableData['survey']["RCPADt"] . ",'','$doctorCode','" . $name . "','','','','',''," . $Owndiv . "," .$litters. ",0,''," . $tableData['survey']["unlisted_doctor_name"].",$pouch,$cup";

  performQuery($sql);

}
 $resp["success"] = true;
    echo json_encode($resp);die;
}
else
             $tr=performQueryWP($sqlsp, $params);
                if (sqlsrv_errors() != null) {
                    echo($sqlsp . "<br>");
                    outputJSON($params . "<br>");
                    outputJSON(sqlsrv_errors());
                    die;
                }
                $DCRCode = $ARDCd;

                if ($ARDCd == "Exists") {
                    $resp["msg"] = "Call Already Exists";
                    $resp["success"] = false;
                    echo json_encode($resp);
                    die;
                }
            }
if($vTyp!=5&&$vTyp!=6&&$vTyp!=7){
            $townCode = $data[0]['Activity_Report_APP']['Town_code'];
            $custCode = $data[1]["Activity_Doctor_Report"]['doctor_code'];
            $stockistCode = $data[1]["Activity_Doctor_Report"]['Order_Stk'];
            $routeTarget = $data[1]["Activity_Doctor_Report"]['rootTarget'];
            $orderNo = $data[1]["Activity_Doctor_Report"]['Order_No'];
            $orderValue = $data[1]["Activity_Doctor_Report"]['orderValue'];
            $net_weight_value = $data[1]["Activity_Doctor_Report"]['net_weight_value'];
            $collectedAmount = $data[1]["Activity_Doctor_Report"]['Doctor_POB'];
            $orderDate = date("Y-m-d H:i");
            $transOrderDetails = $data[2]['Activity_Sample_Report'];
            if (count($transOrderDetails) > 0) {
                $sql = "SELECT isNull(max(cast(Trans_Sl_No as numeric)),0)+1 as RwID FROM Trans_Order_Head";
                $tRw = performQuery($sql);
                $pk = (int) $tRw[0]['RwID'];
                $sql = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue,'" . $orderDate . "', $townCode,'" . $DCRCode . "',$net_weight_value";
            
    performQuery($sql);
                $totalval = 0;
                for ($i = 0; $i < count($transOrderDetails); $i++) {
                    $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Order_Details";
                    $tRw = performQuery($sql);
                    $pk1 = (int) $tRw[0]['RwID'];
                    $qty = $transOrderDetails[$i]['Product_Rx_Qty'];
                    $value = $transOrderDetails[$i]['Product_Sample_Qty'];
 $net_weight = $transOrderDetails[$i]['net_weight'];
                    $sql = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product_code'] . "','" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,0,$net_weight";
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
}
            }
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
    case "deleteEntry":
        $data = json_decode($_POST['data'], true);
        $sfCode = $_GET['sfCode'];
        $custCode = "'" . $data['custId'] . "'";
             
        $arc = (isset($data['arc']) && strlen($data['arc']) == 0) ? null : $data['arc'];
        $amc = (isset($data['amc']) && strlen($data['amc']) == 0) ? null : $data['amc'];
   orderDetailsDelete($sfCode, $custCode,$arc, $amc);
        $result = deleteEntry($arc, $amc);
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
    case "imguploadDcrDave":
        $sf = $_GET['sf_code'];
  $worktype = $_GET['worktype'];
 $doctor = $_GET['doctor'];
$divCode=$_GET['divCode'];
   
        move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photos/" . $sf . "_" . $_FILES["imgfile"]["name"]);
$result=array();
$result['filename'] = $sf . "_" . $_FILES["imgfile"]["name"];
$date=date('Y-m-d');
$sql="select trans_slno from dcrmain_trans where sf_code='$sf' and activity_date='$date'";
$maintran=performQuery($sql);
$arc=$maintran[0]['trans_slno'];
if($worktype=="DD")
{
$sql="select sl_no trans_detail_slno from doortodoor where trans_slno='$arc' and retailor_code='$doctor'";
$maintran=performQuery($sql);

$amc=$maintran[0]['trans_detail_slno'];
$sql="insert into DCREvent_Captures select '$arc','','$sf','".$result['filename'] ."','','',$divCode,'$amc'";
}
else
{
$sql="select trans_detail_slno from DCRDetail_Lst_Trans where trans_slno='$arc' and trans_detail_info_code='$doctor'";
$maintran=performQuery($sql);

$amc=$maintran[0]['trans_detail_slno'];
$sql="insert into DCREvent_Captures select '$arc','$amc','$sf','".$result['filename'] ."','','',$divCode,''";
}
performQuery($sql);
    
        break;

    case "imgupload":
        $sf = $_GET['sf_code'];
  $amc = $_GET['amc'];
 $arc = $_GET['arc'];
$divCode=$_GET['divCode'];
   
        move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photos/" . $sf . "_" . $_FILES["imgfile"]["name"]);
$result=array();
$result['filename'] = $sf . "_" . $_FILES["imgfile"]["name"];
$sql="insert into DCREvent_Captures select '$arc','$amc','$sf','".$result['filename'] ."','','',$divCode,''";
performQuery($sql);
    
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
$sfCode=$_GET['code'];
$sql="exec spLostProduct $custCode,$sfCode,$stockistCode";
$det=performQuery($sql);
outputJSON($det);
break;
    case "get/BrndSumm":
        $data = json_decode($_POST['data'], true);
        $desig = $data['desig'];
        outputJSON(getBrandWiseProd($desig));
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
$sfCode=$_GET['rSF'];
 $Dt = date('Y-m-d');
$sqlH = "SELECT Trans_SlNo FROM vwActivity_Report where SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$Dt' as datetime)";
$sql = "select * FROM DCRDetail_Distributors_Hunting where Trans_SlNo in (" . $sqlH . ")";
   $results = performQuery($sql);

break;
case "doortodoor":
$sfCode=$_GET['rSF'];
 $Dt = date('Y-m-d');
$sqlH = "SELECT Trans_SlNo FROM vwActivity_Report where SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$Dt' as datetime)";
$sql = "select * FROM doortodoor
 where Trans_SlNo in (" . $sqlH . ")";
   $results = performQuery($sql);

break;
case "mas_district":
$stateCode=$_GET['State_Code'];
$sql = "select Dist_code id,dist_name name FROM mas_district where div_code='$Owndiv' and state_code=$stateCode";
   $results = performQuery($sql);

break;
case "competitor_details":
$sql = "select * FROM competitor_details";
   $results = performQuery($sql);

break;
case "vwmasdsm":
if($data['desig']=='mr')
{
$query = "select DSM_Code id,DSM_name name,Town_Code town from mas_dsm where DSM_Code='$sfCode'";
          
     $results = performQuery($query);
}
else{
 $query = "select DSM_Code id,DSM_name name,Town_Code town from vwMasDCM where distributor_code in (select stockiest_code from vwstockiest_Master_APP where sf_code='$sfCode')";
          
     $results = performQuery($query);
}
break;
            case "mas_worktype":
                $query = "exec GetWorkTypes_App '" . $RSF . "'";
                $results = performQuery($query);
                break;
            case "product_master":
                $desig = $data['desig'];
                $results = getProducts($desig);
                break;
            case "category_master":
                $query = "exec GetProdBrand_App '" . $div . "'";
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
                $query = "select Doc_Special_Code id,Doc_Special_Name name, CASE WHEN doc_special_code=41 THEN 0 WHEN doc_special_code=42 THEN 0 WHEN doc_special_code=45 THEN 0 ELSE 1 END AS horeca from Mas_Doctor_Speciality where Division_code='" . $Owndiv . "' and Doc_Special_Active_Flag=0";
                $results = performQuery($query);
                break;
            case "mas_doc_class":
                $query = "select Doc_ClsCode id,Doc_ClsSName name from Mas_Doc_Class where Division_code='" . $Owndiv . "' and Doc_Cls_ActiveFlag=0";
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
            case "vwtourplan":
$sfCode=$_GET['rSF'];
                $query = "select  * from vwTourPlan where SF_Code='$sfCode'";
                $results = performQuery($query);
                outputJSON($results);
                die;
                break;
            case "vwleavetype":

                $query = "select Leave_Code id,Leave_SName name,Leave_Name from vwLeaveType where Division_code='" . $Owndiv . "'";
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
        $sfCode = $_GET['sfCode'];
        $custCode = $data['Cust_Code'];
        $orderNo = '0';
        $orderDate = date("Y-m-d H:i");
        $DCRCode = $data['DCR_Code'];
        $query = "delete from Trans_Order_Head WHERE Trans_Sl_No ='" . $Trans_Sl_No . "'";
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

        $sql = "SELECT isNull(max(cast(Trans_Sl_No as numeric)),0)+1 as RwID FROM Trans_Order_Head";
        $tRw = performQuery($sql);
        $pk = (int) $tRw[0]['RwID'];
       
        $totalval = 0;
$net_weight_value=0;
            
                   
        for ($i = 0; $i < count($transOrderDetails); $i++) {
            $sql = "SELECT isNull(max(cast(Trans_Order_No as numeric)),0)+1 as RwID FROM Trans_Order_Details";
            $tRw = performQuery($sql);
            $pk1 = (int) $tRw[0]['RwID'];
            $qty = $transOrderDetails[$i]['rx_qty'];
            $value = $transOrderDetails[$i]['sample_qty'];
$net_weight = $transOrderDetails[$i]['product_netwt'];
$net_weight_value=$net_weight_value+$transOrderDetails[$i]['netweightvalue'];

            $sql = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate,net_weight) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product'] . "','" . $transOrderDetails[$i]['product_Nm'] . "', $qty,$value,0,$net_weight";
      
performQuery($sql);
        }
 $sql = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route,DCR_Code,net_weight_value) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue,'" . $orderDate . "', $townCode,'" . $DCRCode . "',$net_weight_value";
        performQuery($sql);
        $sql = "SELECT Total_Order_Amount as totalOrder,Amt_Collect FROM Order_Collection_Details where Sf_Code='$sfCode' and Stockist_Code=$stockistCode and Cust_Code=$custCode";
        $tRw = performQuery($sql);
        $sql = "select * from vwProductDetails where DCR_Code='$DCRCode'";
        $tr = performQuery($sql);
        $SPProds = '';
        $nSPProds = '';
        $pob = 0;
        $pobVal = 0;
        for ($k = 0; $k < count($tr); $k++) {
            $SPProds = $SPProds . $tr[$k]['Additional_Prod_Code1'] . "#";
            $nSPProds = $nSPProds . $tr[$k]['Additional_Prod_Dtls1'] . "#";
            $pob = $tr[$k]['POB'] + $pob;
            $pobVal = $tr[$k]['POB_Value'] + $pobVal;
        }
        $sql = "update DCRDetail_Lst_Trans set POB=" . $pob . ",POB_Value=" . $pobVal . ",Additional_Prod_Code='" . $SPProds . "',Additional_Prod_Dtls='" . $nSPProds . "',Product_Code='',Product_Detail='',net_weight_value=$net_weight_value where Trans_Detail_Slno='$DCRCode'";
     
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
        $sql = "select From_Date,To_Date,No_of_Days from mas_Leave_Form where To_Date>='$date' and sf_code='$sfCode' order by From_Date";
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
        outputJSON($products);
        die;
break;

 case "getMailsApp":
        $sfCode = $_GET['sfCode'];
        $divCode = $_GET['divisionCode'];
        $folder = $_GET['folder'];
        $month = $_GET['month'];
        $year = $_GET['year'];
if($folder=="Sent")
$sql = "exec MailInbox '$sfCode','$divCode','sent','',$year,$month,'Mail_Sent_Time','Desc',''";
  else if($folder=="0")      
$sql = "exec MailInbox '$sfCode','$divCode','inbox','',$year,$month,'Mail_Sent_Time','Desc',''";
else if($folder=="View")
$sql = "exec MailInbox '$sfCode','$divCode','view','',$year,$month,'Mail_Sent_Time','Desc',''";
else
$sql = "exec MailInbox '$sfCode','$divCode','Flder','$folder',$year,$month,'Mail_Sent_Time','Desc',''";
//print_r($sql);die;
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
    case "vwChkTransApprovalOne":
        $sfCode = $_GET['code'];
        $month = $_GET['month'];
        $year = $_GET['year'];
        $sql = "select Objective,Worked_With_SF_Name,Worktype_Name_B,Territory_Code1,convert(varchar,Tour_Date,103) Tour_Date from Trans_TP_One where Sf_Code='$sfCode' order by Tour_Date";
        $tp = performQuery($sql);
        outputJSON($tp);
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
        $div = $_GET['divisionCode'];
$divs = explode(",", $div . ",");
        $divCode = (string) $divs[0];
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
$sql = "SELECT isNull(max(Trans_Sl_No),0)+1 as id FROM Trans_Mail_Head";
        $tr = performQuery($sql);
        $maxIntNo = $tr[0]['id'];
        $sql = "insert into Trans_Mail_Head(Trans_Sl_No,Mail_Sent_Time, Mail_SF_From, To_SFName, CC_SFName, Bcc_SFName, Mail_Subject, Mail_Content, Mail_SF_Name, Division_Code, Mail_Attachement,Mail_SF_TO,Mail_CC,Mail_BCC,System_Ip)
        select $maxIntNo,'$date','$sf','" . $_POST['to'] . "'," . $_POST['cc'] . "," . $_POST['bcc'] . ",'$sub','$msg','" . $_POST['from'] . "',$divCode,'$fileName'," . $_POST['toId'] . "," . $_POST['ccId'] . "," . $_POST['bccId'] . ",''";
      
performQuery($sql);
        
        $ToCcBcc = explode(', ', $_POST['ToCcBcc']);
        for ($i = 0; $i < count($ToCcBcc); $i++) {
            if ($ToCcBcc[$i]) {
                //  Mail_int_Det_No,Mail_View_Color
                $sql = "insert into Trans_Mail_Detail(Trans_Sl_No, Open_Mail_Id, Mail_Active_Flag,Division_Code)
                                                   select $maxIntNo,'" . $ToCcBcc[$i] . "',0,$divCode";
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
 case "get/dayplan":
        outputJSON(getDayPlan());
        break;
    case "get/MnthSumm":
        outputJSON(getMonthSummary());
        break;
    case "get/DayRpt":
        outputJSON(getDayReport());
        break;
    case "get/vwVstDet":
        outputJSON(getVstDets());
        break;
    case "get/vwOrderDetails":
        outputJSON(getVwOrderDet());
        break;
    case "get/calls":
        $sfCode = $_GET['sfCode'];
        $date = date('Y-m-d');
        $result = array();
        $query = "select convert(varchar,h.Activity_Date,103) Adate,POB_Value orderValue,h.Trans_SlNo from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and h.Activity_Date='$date'";
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
        $result['today']['order'] = $order;
        $result['today']['orderVal'] = $orderVal;
        $result['today']['calls'] = $calls;
        $result['today']['Adate'] = $Adate;
        $result['today']['Acode'] = $Acode;
        $query = "select sum(POB_Value) orderValue,count(h.Trans_SlNo) Trans_SlNo from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and MONTH(h.Activity_Date) = MONTH(getDate()) and YEAR(h.Activity_Date) = YEAR(getDate())";
        $month = performQuery($query);
        $query = "select count(h.Trans_SlNo) Trans_SlNo from vwActivity_Report h inner join vwActivity_MSL_Details d on h.Trans_SlNo=d.Trans_SlNo where h.Sf_Code='$sfCode' and MONTH(h.Activity_Date) = MONTH(getDate()) and YEAR(h.Activity_Date) = YEAR(getDate()) and POB_Value>0";
        $order = performQuery($query);
        $result['month']['order'] = $order[0]['Trans_SlNo'];
        $result['month']['orderVal'] = $month[0]['orderValue'];
        $result['month']['calls'] = $month[0]['Trans_SlNo'];
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
}
?>

