<?php

header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

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
        $respon['sfName'] = $arr[0]['SF_Name'];
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

        $respon['AppTyp'] = 1;
        $respon['TBase'] = $arr[0]['TBase'];
        $respon['GeoChk'] = $arr[0]['GeoNeed'];
        $respon['ChmNeed'] = $arr[0]['ChmNeed'];
        $respon['StkNeed'] = $arr[0]['StkNeed'];
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

        $respon['DRxCap'] = $arr[0]['DrRxQCap'];
        $respon['DSmpCap'] = $arr[0]['DrSmpQCap'];
        $respon['CQCap'] = $arr[0]['ChmQCap'];
        $respon['SQCap'] = $arr[0]['StkQCap'];
        $respon['NRxCap'] = $arr[0]['NLRxQCap'];
        $respon['NSmpCap'] = $arr[0]['NLSmpQCap'];
        $respon['SFStat'] = $arr[0]['SFStat'];
        $respon['days'] = $arr[0]['days'];
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
    $Qry = "SELECT convert(varchar,Tour_Date,103) [date],Worktype_Name_B wtype,replace(isnull(Tour_Schedule1,''),'0','') towns,replace(isnull(Tour_Schedule1,''),'0','') PlnNo,SF_Code sf_code from Trans_TP T where sf_code='$sfCode' and Tour_Month=month('$TpDt') and Tour_year=year('$TpDt') order by Tour_Date";
    $respon = performQuery($Qry);
    return outputJSON($respon);
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
function getCurrentStockDet()
{
    $SF = $_GET['sfCode'];
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
        $query = "select SUM(Order_value) sumoforder,stockist_code from vwTransOrderHead where id<7 and Cust_Code=$MSL group by stockist_code order by stockist_code";
        $transHead = performQuery($query);

        $query = "select Stockist_Code,Last_Order_Amt,Out_standing_Amt from dbo.Order_Collection_Details where Cust_Code=$MSL order by Stockist_Code";
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
        $query = "select Trans_SlNo,Trans_Detail_Slno,convert(varchar,Activity_Date,0) Adate,convert(varchar,cast(convert(varchar,Activity_Date,101)+' '+Time  as datetime),20) as DtTm,(Select content from vwFeedTemplate where ID=Rx) CalFed,Activity_Remarks,products,gifts from vwLastVstDet where rw=1 and Trans_Detail_Info_Code='" . $MSL . "' and SF_Code='" . $SF . "'";
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
    $query = "select Count(Trans_Detail_Info_Code) doctor_count from vwActivity_MSL_Details D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)";
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

function getFromTable($tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null) {

    $query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
    if (!is_null($sfCode)) {
        $query .=" WHERE tab.SF_Code='$sfCode'";
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
function getDistReport(){
    $filter= json_decode($_GET['filter'],true);
     $sfCode =$filter['sfCode'];
     $stockist=$filter['stockist'];
    $dyDt = $filter['date'];
    $sql="select * from vwDistReport where Sf_Code='$sfCode' and Order_Date='$dyDt' and Stockist_Code='$stockist'";
    $res=performQuery($sql);
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

    $sql = "SELECT Employee_Id,case sf_type when 1 then 'MR' else 'MGR' End SF_Type FROM Mas_Salesforce where SF_code='" . $sfCode . "'";
    $as = performQuery($sql);
    $IdNo = (string) $as[0]['Employee_Id'];
    $SFTyp = (string) $as[0]['SF_Type'];
    switch ($temp[0]) {
        case "stockUpdation":
            for ($i = 0; $i < count($vals); $i++) {
                $sql = "SELECT isNull(max(Tran_Slno),0)+1 as RwID FROM Trans_Stock_Updation_Details";
                $tRw = performQuery($sql);
                $pk = (int) $tRw[0]['RwID'];
                $productCode = $vals[$i]['product'];
                $productName = $vals[$i]['product_Nm'];
                $cbQty = $vals[$i]['cb_qty'];
                $recvQty = $vals[$i]['recv_qty'];
                $sql = "insert into Trans_Stock_Updation_Details(Tran_Slno,Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,Division_Code) select '" . $pk . "','" . $sfCode . "'," . $productCode . ",'" . $productName . "',$recvQty,$cbQty," . $Owndiv . "";
                performQuery($sql);
                $sql = "SELECT Stockist_Code as stockist FROM Trans_Current_Stock_details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "'";
                $tRw = performQuery($sql);
                if (!empty($tRw)) {
                    $sql = "DELETE FROM Trans_Current_Stock_details where Stockist_Code='" . $sfCode . "' and Product_Code='" . $productCode . "'";
                    performQuery($sql);
                }
                $sql = "insert into Trans_Current_Stock_details(Stockist_Code,Product_Code,Product_Name,Rec_Qty,Cb_Qty,Division_Code) select '" . $sfCode . "'," . $productCode . ",'" . $productName . "',$recvQty,$cbQty," . $Owndiv . "";
                performQuery($sql);
            }
            break;
        case "tbMyDayPlan":
            $sql = "insert into tbMyDayPlan select '" . $sfCode . "'," . $vals["sf_member_code"] . ",'" . date('Y-m-d H:i:s') . "'," . $vals["cluster"] . "," . $vals["remarks"] . ",'" . $Owndiv . "'," . $vals["wtype"] . "," . $vals["FWFlg"] . "," . $vals["ClstrName"] . "," . $vals["stockist"];
            performQuery($sql);
            if (str_replace("'", "", $vals["FWFlg"]) != "F") {
                delAREntry($sfCode, $vals["wtype"], $today);
                $ARCd = 0;
                $sql = "{call  svDCRMain_App(?,?," . $vals["wtype"] . ",'" . str_replace("'", "", $vals["cluster"]) . "',?,'" . str_replace("'", "", $vals["remarks"]) . "',?)}";
                $params = array(array($sfCode, SQLSRV_PARAM_IN),
                    array($today, SQLSRV_PARAM_IN),
                    array($Owndiv, SQLSRV_PARAM_IN),
                    array($ARCd, SQLSRV_PARAM_OUT));
                performQueryWP($sql, $params);
            }
            break;
        case "chemists_master":
            $sql = "SELECT isNull(max(Chemists_Code),0)+1 as RwID FROM Mas_Chemists";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];

            $sql = "insert into Mas_Chemists(Chemists_Code,Chemists_Name,Chemists_Address1,Territory_Code,Chemists_Phone,Chemists_Contact,Division_Code,Cat_Code,Chemists_Active_Flag,Sf_Code,Created_Date,Created_By) select '" . $pk . "'," . $vals["chemists_name"] . "," . $vals["Chemists_Address1"] . "," . $vals["town_code"] . "," . $vals["Chemists_Phone"] . ",'','" . $Owndiv . "','',0,'" . $sfCode . "','" . date('Y-m-d H:i:s') . "','Apps'";
            performQuery($sql);
            break;
        case "unlisted_doctor_master":
            $sql = "{call  svListedDR_APP('$sfCode'," . $vals['unlisted_doctor_name'] . "," . $vals['unlisted_doctor_address'] . "," . $vals['unlisted_doctor_phone'] . "," . $vals["unlisted_specialty_code"] . "," . $vals["unlisted_cat_code"] . "," . $vals["town_code"] . ",0,'" . date('Y-m-d H:i:s') . "',$Owndiv," . $vals["unlisted_class"] . ",0," . $vals['wlkg_sequence'] . ")}";
//            $sql = "SELECT isNull(max(UnListedDrCode),0)+1 as RwID FROM Mas_UnListedDr";
//            $tRw = performQuery($sql);
//            $pk = (int) $tRw[0]['RwID'];
//
//            $sql = "insert into Mas_UnListedDr(UnListedDrCode,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code,Territory_Code,UnListedDr_Active_Flag,UnListedDr_Sl_No,Division_Code,SLVNo,Doc_QuaCode,Doc_ClsCode,Sf_Code,UnListedDr_Created_Date,Created_By) select '" . $pk . "'," . $vals["unlisted_doctor_name"] . ",''," . $vals["unlisted_specialty_code"] . "," . $vals["unlisted_cat_code"] . "," . $vals["town_code"] . ",0,'" . $pk . "','" . $Owndiv . "','" . $pk . "'," . $vals["unlisted_qulifi"] . "," . $vals["unlisted_class"] . ",'" . $sfCode . "','" . date('Y-m-d H:i:s') . "','Apps'";
//print_r($sql);die; 
            performQueryWP($sql);
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
            $townCode=$data[0]['Activity_Report_APP']['Town_code'];
            $custCode = $data[1]["Activity_Doctor_Report"]['doctor_code'];
            $stockistCode = $data[1]["Activity_Doctor_Report"]['Order_Stk'];
            $routeTarget = $data[1]["Activity_Doctor_Report"]['rootTarget'];
            $orderNo = $data[1]["Activity_Doctor_Report"]['Order_No'];
            $orderValue = $data[1]["Activity_Doctor_Report"]['orderValue'];
            $collectedAmount = $data[1]["Activity_Doctor_Report"]['Doctor_POB'];
            $orderDate = date("Y-m-d H:i");
            $transOrderDetails = $data[2]['Activity_Sample_Report'];
            $sql = "SELECT isNull(max(Trans_Sl_No),0)+1 as RwID FROM Trans_Order_Head";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
            $sql = "insert into Trans_Order_Head(Trans_Sl_No,Sf_Code,Cust_Code,Stockist_Code,Route_Target,Order_No,Collected_Amount,Order_Value,Order_Date,Route) select '" . $pk . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",'" . $routeTarget . "'," . $orderNo . ", $collectedAmount , $orderValue,'" . $orderDate . "', $townCode ";
            performQuery($sql);
            $totalval = 0;
            for ($i = 0; $i < count($transOrderDetails); $i++) {
                $sql = "SELECT isNull(max(Trans_Order_No),0)+1 as RwID FROM Trans_Order_Details";
                $tRw = performQuery($sql);
                $pk1 = (int) $tRw[0]['RwID'];
                $qty = $transOrderDetails[$i]['Product_Rx_Qty'];
                $value = $transOrderDetails[$i]['Product_Sample_Qty'];
                $sql = "insert into Trans_Order_Details( Trans_Order_No, Trans_Sl_No,Product_Code,Product_Name,Quantity,value,Rate) select '" . $pk1 . "','" . $pk . "','" . $transOrderDetails[$i]['product_code'] . "','" . $transOrderDetails[$i]['product_Name'] . "', $qty,$value,0";
                performQuery($sql);
            }
            $sql = "SELECT Total_Order_Amount as totalOrder,Amt_Collect FROM Order_Collection_Details where Sf_Code='$sfCode' and Stockist_Code=$stockistCode and Cust_Code=$custCode";
            $tRw = performQuery($sql);
            if (!empty($tRw[0]['totalOrder'])) {
                $oldTotalOrder = (int) $tRw[0]['totalOrder'];
                $oldCollectedAmount = (int) $tRw[0]['Amt_Collect'];
                $total = $orderValue + $oldTotalOrder;
                $collectAmount = $collectedAmount + $oldCollectedAmount;
                $outstand = $total - $collectAmount;
                $sql = "update Order_Collection_Details set Total_Order_Amount=$total,Out_standing_Amt=$outstand,Amt_Collect=$collectAmount,Last_order_Amt=$orderValue where Sf_Code='$sfCode' and Stockist_Code=$stockistCode and Cust_Code=$custCode";
                $result = performQuery($sql);
            } else {
                $outstandinAmt = $orderValue - $collectedAmount;
                $sql = "SELECT isNull(max(Trans_Collection_No),0)+1 as RwID FROM Order_Collection_Details";
                $tRw = performQuery($sql);
                $pk2 = (int) $tRw[0]['RwID'];
                $sql = "insert into Order_Collection_Details(Trans_Collection_No,Sf_Code,Cust_Code,Stockist_Code,Total_Order_Amount,Amt_Collect,Out_standing_Amt,Last_Order_Amt,Route) select '" . $pk2 . "','" . $sfCode . "'," . $custCode . "," . $stockistCode . ",$orderValue,$collectedAmount,$outstandinAmt,$orderValue, $townCode";
                performQuery($sql);
            }
            die;
            $sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "' and lower(Work_Type) <>lower(" . $vals["Worktype_code"] . ")  and cast(activity_date as datetime)=cast('$today' as datetime)";

            $result = performQuery($sql);
            if (count($result) > 0) {
                if (!isset($_GET['replace'])) {
                    $result = array();
                    $result['success'] = false;
                    $result['type'] = 1;
                    $result['msg'] = 'Already There is a Data For other Work do you want to replace....?';
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
                    $sql = "SELECT stockiest_name name from vwstockiest_Master_APP where stockiest_code=" . $cCode;
                }
                if (isset($tableData['Activity_UnListedDoctor_Report'])) {
                    $vTyp = 4;
                    $DetTB = $tableData['Activity_UnListedDoctor_Report'];
                    $cCode = $DetTB["uldoctor_code"];
                    $vTm = $DetTB["UnListed_Doc_Meet_Time"];
                    $pob = $DetTB["UnListed_Doctor_POB"];
                    $proc = "svDCRUnlstDet_App";
                    $sql = "SELECT unlisted_doctor_name name from vwunlisted_doctor_master_APP where unlisted_doctor_code=" . $cCode;
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
                            $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"] . "#";
                            $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"] . "#";
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
            $sql = "{call  svDCRMain_App(?,?," . $vals["Worktype_code"] . ",'" . str_replace("'", "", $vals["Town_code"]) . "',?,'" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?)}";
            $params = array(array($sfCode, SQLSRV_PARAM_IN),
                array($today, SQLSRV_PARAM_IN),
                array($Owndiv, SQLSRV_PARAM_IN),
                array($ARCd, SQLSRV_PARAM_OUT));
            performQueryWP($sql, $params);

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
                $sqlsp = $sqlsp . $proc . " (?,?,?," . $vTyp . "," . $cCode . ",'" . $cName . "'," . $vTm . "," . $pob . ",'" . str_replace("'", "", $DetTB["Worked_With"]) . "',?,?,?,?,";
                if ($vTyp == 1 || $vTyp == 4)
                    $sqlsp = $sqlsp . "?,?,?,?,?,";
                $sqlsp = $sqlsp . "'" . str_replace("'", "", $vals["Town_code"]) . "','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,'" . str_replace("'", "", $vals["rx_t"]) . "'," . $DetTB["modified_time"] . ",?,?," . $vals["DataSF"] . ",'" . $DetTB["geoaddress"] . "')}";

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
                performQueryWP($sqlsp, $params);
                if (sqlsrv_errors() != null) {
                    echo($sqlsp . "<br>");
                    outputJSON($params . "<br>");
                    outputJSON(sqlsrv_errors());
                    die;
                }
                if ($ARDCd == "Exists") {
                    $resp["msg"] = "Call Already Exists";
                    $resp["success"] = false;
                    echo json_encode($resp);
                    die;
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
        $arc = (isset($data['arc']) && strlen($data['arc']) == 0) ? null : $data['arc'];
        $amc = (isset($data['amc']) && strlen($data['amc']) == 0) ? null : $data['amc'];
        $result = deleteEntry($arc, $amc);
        break;
    case "get/doctorCount":
        outputJSON(getDoctorPCount());
        break;
    case "get/setup":
        outputJSON(getAPPSetups());
        break;
    case "imgupload":
        $sf = $_GET['sf_code'];
        print_r(sf);
        move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photos/" . $sf . "_" . $_FILES["imgfile"]["name"]);
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
    case "table/list":
        $results;
        $data = json_decode($_POST['data'], true);
        $sfCode = $_GET['sfCode'];
        $RSF = $_GET['rSF'];
        $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        switch (strtolower($data['tableName'])) {
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
                break;
            case "gift_master":
                $query = "exec getAppGift '" . $sfCode . "'";
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
            default:
                $sfCode = (isset($data['sfCode']) && $data['sfCode'] == 0) ? null : $_GET['sfCode'];
                $divisionCode = (int) $Owndiv;
                //$divisionCode = 1;

                $today = (isset($data['today']) && $data['today'] == 0) ? null : $data['today'];
                $or = (isset($data['or']) && $data['or'] == 0) ? null : $data['or'];
                $wt = (isset($data['wt']) && $data['wt'] == 0) ? null : $data['wt'];
                $tableName = $data['tableName'];
                $coloumns = json_decode($data['coloumns']);

                $where = isset($data['where']) ? json_decode($data['where']) : null;

                $join = isset($data['join']) ? $data['join'] : null;
                $orderBy = isset($data['orderBy']) ? json_decode($data['orderBy']) : null;

                if (!is_null($or)) {
                    $results = getFromTableWR($tableName, $coloumns, $divisionCode, $sfCode, $orderBy, $where, $join, $today, $wt);
                } else {
                    $results = getFromTable($tableName, $coloumns, $divisionCode, $sfCode, $orderBy, $where, $join, $today, $wt);
                }
                break;
        }
        outputJson($results);
        break;
    case "dcr/updateEntry":
        $data = json_decode($_POST['data'], true);
        $arc = (isset($_GET['arc']) && strlen($_GET['arc']) == 0) ? null : $_GET['arc'];
        $amc = (isset($_GET['amc']) && strlen($_GET['amc']) == 0) ? null : $_GET['amc'];
        deleteEntry($arc, $amc);
        addEntry();
        break;
    case "dcr/callReport":
        outputJSON(getCallReport());
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
    case "get/precall":
        getPreCallDet();
        break;
     case "get/currentStock":
        getCurrentStockDet();
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
    case "entry/count":
        $today = date('Y-m-d 00:00:00');
        $sfCode = $_GET['sfCode'];

        $sql = "SELECT Employee_Id,case sf_type when 1 then 'MR' else 'MGR' End SF_Type FROM Mas_Salesforce where SF_code='" . $sfCode . "'";
        $as = performQuery($sql);
        $SFTyp = (string) $as[0]['SF_Type'];

        $query = "SELECT work_Type worktype_code,Remarks daywise_remarks,Half_Day_FW halfdaywrk from vwActivity_Report H where SF_Code='" . $sfCode . "' and FWFlg <> 'F' and cast(activity_date as datetime)=cast('$today' as datetime)";
        $data = performQuery($query);
        $result = array();
        if (count($data) > 0) {
            $result["success"] = false;
            $result['data'] = $data;
            outputJSON($result);
            die;
        }
        $result["success"] = true;
        $result['data'] = getEntryCount();
        outputJSON($result);
        break;
}
?>