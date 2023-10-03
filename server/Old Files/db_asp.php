<?php

header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

/**
 * Perfomrs select query on specified by table retreving specified coloumns
 * @param {String} $tableName table name
 * @param {Array} $coloumns array of coloumn names
 * @param {Integer} $divisionCode division code
 * @param {String} [$sfCode] sfcode if any
 * @param {Array} [$orderBy] array of coloumn names to order by
 * @return {Array} array of results
 */
function getFromTable($tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null) {

    $query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
	if (!is_null($sfCode)) {
        $query .=" WHERE tab.SF_Code='$sfCode'";
    }
	else
	{
	    $query.= " WHERE tab.Division_Code=" . $divisionCode;
	}
    if (!is_null($wt)) {
		if(strtolower($tableName)=="mas_worktype")
			$query.=" AND tab.type_code='field work'";
		else
        	$query.=" AND tab.worktype_code='field work'";
    }
    if (!is_null($where)) {
        $query.=" and " . join(" and ", $where);
    }
    if (!is_null($today)) {
        $today = date('Y-m-d 00:00:00');
        $query.=" and cast(tab.dcr_activity_date as datetime)>=cast('$today' as datetime)";
    }
    if (!is_null($orderBy)) {
        $query .=" ORDER BY " . join(",", $orderBy);
    }
	return performQuery($query);
}

function getEntryCount() {
    $sfCode =$_GET['sfCode'];
    $today = date('Y-m-d 00:00:00');
    $results = array();
    $query = "SELECT count(*) as doctor_count FROM Activity_Doctor_Report adr inner JOIN activity_report_APP ar on adr.activity_report_code=ar.activity_report_code where sf_code='" . $sfCode . "' and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "SELECT count(*) as chemist_count from activity_chemist_report adr inner JOIN activity_report_APP ar on adr.activity_report_code=ar.activity_report_code where sf_code='" . $sfCode . "' and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "SELECT count(*) as stockist_count from activity_stockist_report  adr inner JOIN activity_report_APP ar on adr.activity_report_code=ar.activity_report_code where sf_code='" . $sfCode . "' and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "SELECT count(*) as uldoctor_count from activity_unlisteddoctor_Report  adr inner JOIN activity_report_APP ar on adr.activity_report_code=ar.activity_report_code where sf_code='" . $sfCode . "' and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
    $temp = performQuery($query);
    $results[] = $temp[0];
	$query = "select isnull((SELECT top 1 isnull(remarks,'') from vwActivity_Report where sf_code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)),'') as remarks";
    $temp = performQuery($query);
    $results[] =$temp[0];
	$query = "select isnull((SELECT top 1 HalfDay_FW_Type from vwActivity_Report where sf_code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)),'') as halfdaywrk";
    $temp = performQuery($query);
    $results[] =$temp[0];
    return $results;
}

function getPreCallDet() {
    $SF =$_GET['sfCode'];
    $MSL =$_GET['Msl_No'];

	$result = array();
	$query="select Doc_Sl_No SVL,(select Cat_Name from Doctor_category where Cat_Code=Doctor_category and Division_Code=D.Division_Code) DrCat,(select Specialty_Name from Doctor_Specialty where Specialty_Code=Doctor_Specialty and Division_Code=D.Division_Code) DrSpl,isnull(stuff((select ', '+Drs_Sub_CatName from Map_DoctorSubCat_Doctor_Detail CD	inner join Mas_Doctor_Sub_Category S on CD.Drs_Sub_CatCode=S.Drs_Sub_CatCode where Doctor_Code=D.Doctor_Code for XML Path('')),1,2,''),'') DrCamp,isnull(stuff((select ', '+Product_Name from Doctor_Products_Map M	inner join product_master P on M.Product_Code=P.Product_Code and P.Division_Code=M.Division_Code where Doctor_Code=D.Doctor_Code for XML Path('')),1,2,''),'') DrProd from Doctor_Master D where Doctor_Code='".$MSL."'";
	$as = performQuery($query);	
	if (count($as) > 0) {
		$result['SVL'] = (string) $as[0]['SVL'];
		$result['DrCat'] = (string) $as[0]['DrCat'];
		$result['DrSpl'] = (string) $as[0]['DrSpl'];
		$result['DrCamp'] = (string) $as[0]['DrCamp'];
		$result['DrProd'] = (string) $as[0]['DrProd'];
		
		$result['success']=true;

		$query = "select Activity_Report_code,Activity_MSL_Code,convert(varchar,Activity_Date,0) Adate,convert(varchar,cast((case when isnumeric(PCR_Value)=1 then (case when (cast(PCR_Value as numeric(7,2))- cast(cast(PCR_Value as numeric(7,2)) AS int))>0.59 or cast(cast(PCR_Value as numeric(7,2)) AS int)>24 then	null else convert(varchar,Activity_Date,101)+' '+replace(cast(cast(PCR_Value as numeric(7,2)) as varchar),'.',':') End) when isdate(PCR_Value)=1 then PCR_Value else null End) as datetime),20) as DtTm,(Select content from report_template_master where ID=Rx) CalFed,Activity_Remarks from vwLastVstDet where rw=1 and msl_no='". $MSL ."' and SF_Code='".$SF."'";
		$as = performQuery($query);	

		if (count($as) > 0) {
			$ARC = (string) $as[0]['Activity_Report_code'];
			$AMSL = (string) $as[0]['Activity_MSL_Code'];
			$result['LVDt'] = date('d / m / Y g:i a',strtotime((string)$as[0]['DtTm']));
			
			$result['CallFd'] = (string) $as[0]['CalFed'];
			$result['Rmks'] = (string) $as[0]['Activity_Remarks'];
			
			
			$query = "select isnull(stuff((select ', '+Product_Name from vwActivity_Sample_Details v inner join product_master P on v.Product_Code=P.Product_Code and P.Division_Code=v.Division_Code where Product_Qty>0 and Activity_MSL_Code='" . $AMSL . "' for XML Path('')),1,2,''),'') DrProd";
			$as = performQuery($query);
			$result['ProdSmp'] = (string) $as[0]['DrProd'];
			
			$query = "select isnull(stuff((select ', '+Product_Name from vwActivity_Sample_Details v inner join product_master P on v.Product_Code=P.Product_Code and P.Division_Code=v.Division_Code where Product_Qty=0 and Activity_MSL_Code='" . $AMSL . "' for XML Path('')),1,2,''),'') DrProd";
			$as = performQuery($query);
			$result['Prodgvn'] = (string) $as[0]['DrProd'];	

			$query = "select isnull(stuff((select ', '+Gift_Name from vwActivity_Gift_Details v inner join Gift_Master P on v.Gift_Code=P.Gift_Code and P.Division_Code=v.Division_Code where Activity_MSL_Code='" . $AMSL . "' for XML Path('')),1,2,''),'') DrGft";
			$as = performQuery($query);
			$result['DrGft'] = (string) $as[0]['DrGft'];	
		}else{	
			$result['success']=false;
		}
	}else{	
		$result['success']=false;
	}
		return outputJSON($result);

}
function getAPPSetups() {
    $rqSF =$_GET['rSF'];
    $query = "exec getAPPSetups '".$rqSF."'";
    return performQuery($query);
}

function getJointWork() {
    $sfCode =$_GET['sfCode'];
    $rqSF =$_GET['rSF'];
    $query = "exec getJointWork '".$sfCode."','".$rqSF."'";
    return performQuery($query);
}

function getSubordinateMgr() {
    $sfCode =$_GET['rSF'];
	$param = array($sfCode);
    $query = "exec getMRsUnderandMGRSf_APP '".$sfCode."',''";
    return performQuery($query);
}

function getSubordinate() {
    $sfCode =$_GET['rSF'];
	$param = array($sfCode);
    $query = "exec getMRsUnderSf_APP '".$sfCode."'";
    return performQuery($query);
}  

function getMonthSummary() {
    $sfCode =$_GET['rptSF'];
    $dyDt =$_GET['rptDt'];
    $query = "exec getMonthSummaryApp '".$sfCode."','".$dyDt."'";
    return performQuery($query);
}

function getDayReport() {
    $sfCode =$_GET['sfCode'];
    $dyDt =$_GET['rptDt'];
    $query = "exec getDayReportApp '".$sfCode."','".$dyDt."'";
    return performQuery($query);
}

function getVstDets() {
    $ACd =$_GET['ACd'];
    $typ =$_GET['typ'];
    $query = "exec spGetVstDetApp '".$ACd."','".$typ."'";
    return performQuery($query);
}

function getDoctorPCount() {
    $sfCode =$_GET['sfCode'];
    $today = date('Y-m-d 00:00:00');
    $query = "SELECT doctor_code as code,count(Product_Code) as product_count,town_code from activity_report_APP ar inner join activity_doctor_report adr on ar.activity_report_code=adr.activity_report_code left join activity_unlistedsample_report asr on adr.Activity_MSL_Code=asr.Activity_MSL_Code and sf_code='" . $sfCode . "' and cast(ar.dcr_activity_date as datetime)>=cast('$today' as datetime) group by doctor_code,town_code";
    return performQuery($query);
}

function getUlDoctorPCount() {
    $sfCode =$_GET['sfCode'];
    $today = date('Y-m-d 00:00:00');
    $query = "SELECT uldoctor_code as code,count(Product_Code) as product_count,town_code from activity_report_APP ar inner join activity_unlisteddoctor_report adr on ar.activity_report_code=adr.activity_report_code left join activity_sample_report asr on adr.Activity_MSL_Code=asr.Activity_MSL_Code and sf_code='" . $sfCode . "' and cast(ar.dcr_activity_date as datetime)>=cast('$today' as datetime) group by uldoctor_code,town_code";
    return performQuery($query);
}

function getChemistPCount() {
    $sfCode =$_GET['sfCode'];
    $today = date('Y-m-d 00:00:00');

    $query = "SELECT chemist_code as code,town_code from activity_report_APP ar inner join  activity_chemist_report adr on ar.activity_report_code=adr.activity_report_code and sf_code='" . $sfCode . "' and cast(ar.dcr_activity_date as datetime)>=cast('$today' as datetime) group by chemist_code,town_code";

    return performQuery($query);
}

function getStockistPCount() {
    $sfCode =$_GET['sfCode'];
    $today = date('Y-m-d 00:00:00');
    $query = "SELECT stockist_code as code,town_code from activity_report_APP ar inner join  activity_stockist_report adr on ar.activity_report_code=adr.activity_report_code and sf_code='" . $sfCode . "' and cast(ar.dcr_activity_date as datetime)>=cast('$today' as datetime) group by stockist_code,town_code";
    return performQuery($query);
}

function getCallReport() {

    $sfCode =$_GET['sfCode'];
    $query = "SELECT day(dcr_activity_date) as day_dcr,convert(varchar,dcr_activity_date,103) as date_sub,count(doc_type) as doc_count,(
          SELECT T.town_name+''
          FROM town_master T
          WHERE ar.town_code = T.town_code
          FOR XML PATH('')) as town_code,

count(chm_type) as chm_count,count(stk_type) as stk_count,count(uldoc_type) as uldoc_count,mwt.wtype worktype_name from activity_report_APP ar left join 
(select activity_report_code,1 as doc_type from activity_doctor_report where doctor_code is not null)adr on 
adr.activity_report_code=ar.activity_report_code
 left join (select activity_report_code,2 as chm_type from activity_chemist_report where chemist_code is not null)acr 
on acr.activity_report_code=ar.activity_report_code
 left join (select activity_report_code,3 as stk_type from activity_stockist_report where stockist_code is not null)asr 
on asr.activity_report_code=ar.activity_report_code
 left join (select activity_report_code,3 as uldoc_type from activity_unlisteddoctor_report where uldoctor_code is not null)aulr 
on aulr.activity_report_code=ar.activity_report_code
 left join (select activity_report_code,4 as uldoctor_type from activity_unlisteddoctor_report where uldoctor_code is 
not null)audr on audr.activity_report_code=ar.activity_report_code
 left join mas_worktype mwt on mwt.type_code=ar.worktype_code and Ar.division_code=mwt.Division_code
where ar.sf_code='" . $sfCode . "' and month(dcr_activity_date)=month(getdate()) and year(dcr_activity_date)=year(getdate())
roup by day(dcr_activity_date),ar.town_code,mwt.wtype,convert(varchar,dcr_activity_date,103)
order by day(dcr_activity_date)";
//echo $query;
    return performQuery($query);
}

function getFromTableWR ($tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null) {

    $query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
    $query.= " WHERE tab.Division_Code=" . $divisionCode;
//    if (!is_null($sfCode)) {
//        $query .=" AND tab.SF_Code='$sfCode'";
//    }
    if (!is_null($wt)) {
		if(strtolower($tableName)=="mas_worktype")
			$query.=" AND tab.type_code='field work'";
		else
        	$query.=" AND tab.worktype_code='field work'";
    }
    if (!is_null($where)) {
        $query.=" and " . join(" or ", $where);
    }

    if (!is_null($today)) {
        $today = date('Y-m-d 00:00:00');

        $query.="and cast(tab.dcr_activity_date as datetime)>=cast('$today' as datetime)";
    }

    if (!is_null($orderBy)) {
        $query .=" ORDER BY " . join(", ", $orderBy);
    }
    return performQuery($query);
}

/**
 *  Used in consecutive save scenarios where last inserted table id for specific table name is needed
 * @param {Array} $TABLE_KEYS_MAPPING array that stores taable name=> primary key values
 */
$TABLE_KEYS_MAPPING = array();

/**
 * Saves data to table
 * @param {Array} $tableData array of data with signature array(
 *                                                                "table_name" => array(
 *                                                                                       "field" => "value"
 *                                                                                     )
 *                                                                "f_key" => array(
 *                                                                                       "field" => "table_name"
 *                                                                                     )
 * 
 *  )
 * @param {String} $tableName table name
 * @param {Boolean} $update update flag
 * @param {String} $row_id row to update
 * @param {String} $primary_key Primary key coloumn name for table
 * @return {String} returns inserted rows primary key 
 */
function saveTableData($tableData, $tableName, $update = false, $row_id = '', $primary_key = "") {
    global $conn, $TABLE_KEYS_MAPPING;


    $cols = array();
    $values = array();
    $sql = "";
    unset($tableData[
            $tableName]['f_key']);
    foreach ($tableData[$tableName] as $col => $val) {
        $cols[] = $col;
        $values[] = $val;
    }

    if ($update == false) {
        $sql = "INSERT INTO $tableName"
                . " ( " . join(", ", $cols) . ") "
                . "VALUES(" . join(", ", $values) . ")";
        $sql .=";
                SELECT SCOPE_IDENTITY()";
//        if($tableName=='Activity_Unlistedsample_Report')
//        {
//            var_dump($sql);
//            die;
//        }      
        $res = sqlsrv_query($conn, $sql);
        $errors = sqlsrv_errors();
        if ($errors != null) {
            var_dump(sqlsrv_errors());
            die;
        }
        $rows_affected = sqlsrv_rows_affected($res);
        sqlsrv_next_result($res);
        sqlsrv_fetch($res);
        $rowID = sqlsrv_get_field($res, 0);
    } else {
        foreach ($tableData[$tableName] as $col => $val) {
            $cols[] = $col . " = " . $val;
//            $values[] = $val;
        }

        $sql = "UPDATE $tableName set "
                . join(", ", $cols)
                . "where $primary_key = $row_id";
        $res = sqlsrv_query($conn, $sql);
        $errors = sqlsrv_errors();
        if ($errors != null) {
            var_dump(sqlsrv_errors());
            die;
        }
        $rows_affected = sqlsrv_rows_affected($res);
        sqlsrv_fetch($res);
        $rowID = $row_id;
    }
//    $TABLE_KEYS_MAPPING[$tableName] = $rowID;

    return array(
        "num_rows" => $rows_affected,
        "id" => $rowID
    );
}


function deleteEntry($arc, $amc) {
    $DivisionCode = (int) $_GET['divisionCode'];
    if (!is_null($amc)) {
        $query = "delete from activity_input_report WHERE Activity_MSL_Code ='" . $amc . "'";
        performQuery($query);
        $query = "delete from activity_sample_report WHERE Activity_MSL_Code ='" . $amc . "'";
        performQuery($query);
        $query = "delete from activity_unlisteddoctor_report WHERE  activity_msl_unlistedcode ='" . $amc . "'";
        performQuery($query);
        $query = "delete from activity_unlistedsample_Report WHERE  activity_msl_unlistedcode ='" . $amc . "'";
        performQuery($query);
        $query = "delete from activity_pob_report WHERE activity_chemist_code ='" . $amc . "'";
        performQuery($query);
        $query = "delete from activity_chm_sample_report WHERE activity_chemist_code ='" . $amc . "'";
        performQuery($query);
        $query = "delete from activity_stk_pob_report WHERE activity_stockist_code ='" . $amc . "'";
        performQuery($query);
        $query = "delete from activity_stk_sample_report WHERE activity_stockist_code ='" . $amc . "'";
        performQuery($query);
    }
    if (!is_null($arc)) {
        $query = "delete from activity_doctor_report WHERE Division_Code = " . $DivisionCode . " and activity_report_code ='" . $arc . "'";
        performQuery($query);
        $query = "delete from activity_unlisteddoctor_report WHERE Division_Code = " . $DivisionCode . " and activity_report_code ='" . $arc . "'";
        performQuery($query);
        $query = "delete from activity_chemist_report WHERE Division_Code = " . $DivisionCode . " and activity_report_code ='" . $arc . "'";
        performQuery($query);
        $query = "delete from activity_stockist_report WHERE Division_Code = " . $DivisionCode . " and activity_report_code ='" . $arc . "'";
        performQuery($query);
		$query = "delete from Activity_Event_Captures WHERE Division_Code = " . $DivisionCode . " and activity_report_code ='" . $arc . "'";
        performQuery($query);
		$query = "delete from activity_report_APP WHERE Division_Code = " . $DivisionCode . " and activity_report_code ='" . $arc . "'";
        performQuery($query);
        
    }
}

function addEntry() {
    //date_default_timezone_set("UTC");
    //Expect an array in the form
    //array(
//            array(
//                   "table_name" =>array(
//                                  "field"=>"value"
//                                  "field"=>"value"
//                                    
//                      ),
//                      "f_key"=>array(
//                             "field1"=>"table_name",
//                             "field2"=>"table_name2"
//                           )
//                 ),
//             array("table_name2" =>array(
//                                  "field"=>"value"
//                                  "field"=>"value"
//                                    
//                   ),
//                   "f_key"=>array(
//                             "field1"=>"table_name",
//                             "field2"=>"table_name2"
//                           )
//                 ),
//        )
    $sfCode =$_GET['sfCode'];
    $data = json_decode($_POST['data'], true);
    $today = date('Y-m-d 00:00:00');
    if (!isset($_GET['replace'])) {
        $today = date('Y-m-d 00:00:00');
        $sql = "SELECT * FROM activity_report_APP where SF_Code='" . $sfCode . "' and lower(worktype_code) <>lower(" . $data[0]['Activity_Report_APP']['Worktype_code'] . ") and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
        $result = performQuery($sql);

        if (count($result) > 0) {
            //Data Exists
            //Return data to user as is
            $result = array();
            $result['success'] = false;
            $result['type'] = 1;
            $result['msg'] = 'Already There is a Data For other Work do you want to replace....?';
            $result['data'] = $data;
            outputJSON($result);
            die;
        }
    } else {

		$sql = "SELECT * FROM activity_report_APP where SF_Code='" . $sfCode . "' and lower(worktype_code) <>lower(" . $data[0]['Activity_Report_APP']['Worktype_code'] . ") and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
        $result = performQuery($sql);
        if (count($result) > 0) {

		    $sqlH = "SELECT activity_report_code FROM activity_report_APP where SF_Code='" . $sfCode . "' and lower(worktype_code) <> lower(" . $data[0]['Activity_Report_APP']['Worktype_code'] . ") and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
			$sql = "DELETE FROM activity_doctor_report where activity_report_code in (".$sqlH.")";performQuery($sql);
			$sql = "DELETE FROM activity_chemist_report where activity_report_code in (".$sqlH.")";performQuery($sql);
			$sql = "DELETE FROM activity_stockist_report where activity_report_code in (".$sqlH.")";performQuery($sql);
			$sql = "DELETE FROM activity_unlisteddoctor_Report where activity_report_code in (".$sqlH.")";performQuery($sql);
			$sql = "DELETE FROM Activity_Event_Captures where activity_report_code in (".$sqlH.")";performQuery($sql);			
		    $sql = "DELETE FROM activity_report_APP where SF_Code='" . $sfCode . "' and lower(worktype_code) <> lower(" . $data[0]['Activity_Report_APP']['Worktype_code'] . ") and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
	        performQuery($sql);
		}

    }
    $temp = array_keys($data[1]);
    switch ($temp[0]) {
        case "Activity_Doctor_Report":
            $sql = "SELECT * FROM Activity_Doctor_Report adr right JOIN activity_report_APP ar on adr.activity_report_code=ar.activity_report_code where SF_Code='" . $sfCode . "' and doctor_code = " . $data[1]['Activity_Doctor_Report']['doctor_code'] . " and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
            $result = performQuery($sql);
            if (!isset($_GET['replace'])) {
                $today = date('Y-m-d 00:00:00');

                if (count($result) > 0) {
                    //Data Exists
                    //Return data to user as is
                    $result = array();
                    $result['success'] = false;
                    $result['type'] = 2;
                    $result['msg'] = 'Call Already Exist';
                    outputJSON($result);
                    die;
                }
            } else {
                $sql = "DELETE FROM activity_report_APP where activity_report_code = '" . $result[0]['activity_report_code'] . "'";
                performQuery($sql);
            }
            break;
        case "Activity_Stockist_Report":
            $sql = "SELECT * FROM Activity_Stockist_Report adr right JOIN activity_report_APP ar on adr.activity_report_code=ar.activity_report_code where SF_Code='" . $sfCode . "' and stockist_code = " . $data[1]['Activity_Stockist_Report']['stockist_code'] . " and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
            $result = performQuery($sql);
            if (!isset($_GET['replace'])) {
                $today = date('Y-m-d 00:00:00');

                if (count($result) > 0) {
                    //Data Exists
                    //Return data to user as is
                    $result = array();
                    $result['success'] = false;
                    $result['type'] = 2;
                    $result['msg'] = 'Call Already Exist';
                    outputJSON($result);
                    die;
                }
            } else {
                $sql = "DELETE FROM activity_report_APP where activity_report_code = '" . $result[0]['activity_report_code'] . "'";
                performQuery($sql);
            }
            break;
        case "Activity_Chemist_Report":
            $sql = "SELECT * FROM Activity_Chemist_Report adr right JOIN activity_report_APP ar on adr.activity_report_code=ar.activity_report_code where SF_Code='" . $sfCode . "' and chemist_code = " . $data[1]['Activity_Chemist_Report']['chemist_code'] . " and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
            $result = performQuery($sql);
            if (!isset($_GET['replace'])) {
                $today = date('Y-m-d 00:00:00');
                if (count($result) > 0) {
                    //Data Exists
                    //Return data to user as is
                    $result = array();
                    $result['success'] = false;
                    $result['type'] = 2;
                    $result['msg'] = 'Call Already Exist';
                    outputJSON($result);
                    die;
                }
            } else {
                $sql = "DELETE FROM activity_report_APP where activity_report_code = '" . $result[0]['activity_report_code'] . "'";
                performQuery($sql);
            }
            break;
        case "Activity_UnListedDoctor_Report":
            $sql = "SELECT * FROM activity_unlisteddoctor_Report adr right JOIN activity_report_APP ar on adr.activity_report_code=ar.activity_report_code where SF_Code='" . $sfCode . "' and uldoctor_code = " . $data[1]['Activity_UnListedDoctor_Report']['uldoctor_code'] . " and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
            $result = performQuery($sql);
            if (!isset($_GET['replace'])) {
                $today = date('Y-m-d 00:00:00');

                if (count($result) > 0) {
                    //Data Exists
                    //Return data to user as is
                    $result = array();
                    $result['success'] = false;
                    $result['type'] = 2;
                    $result['msg'] = 'Call Already Exist';
                    outputJSON($result);
                    die;
                }
            } else {
                $sql = "DELETE FROM activity_report_APP where activity_report_code = '" . $result[0]['activity_report_code'] . "'";
                performQuery($sql);
            }
            break;
    }
    $resp = array();
    if (isset($_GET['draft_id'])) {
        $resp['id'] = $_GET['draft_id'];
    }
    $type = is_array($data);
    $check = array_keys($data[1]);
    for ($i = 0; $i < count($data); $i++) {
        $tableData = $data[$i];
        $tablename = "";
        foreach ($tableData as $k => $v) {
            if ($k != "f_key") {
                $tableName = $k;
                break;
            }
        }

        if (isset($tableData['tbRCPADetails'])) {
            $tableData['tbRCPADetails']['sf_code'] = "'" . $sfCode . "'";
            $tableData['tbRCPADetails']['EntryDt'] = "'" . date('Y-m-d H:i:s') . "'";
        }
        if (isset($tableData['tbMyDayPlan'])) {
            $tableData['tbMyDayPlan']['sf_code'] = "'" . $sfCode . "'";
            $tableData['tbMyDayPlan']['Pln_Date'] = "'" . date('Y-m-d H:i:s') . "'";
        }
		if (isset($tableData['tbRemdrCall'])) {
            $tableData['tbRemdrCall']['sf_code'] = "'" . $sfCode . "'";
            $tableData['tbRemdrCall']['CallDate'] = "'" . date('Y-m-d H:i:s') . "'";
        }
        if (isset($tableData['Activity_Report_APP'])) {
            $tableData['Activity_Report_APP']['sf_code'] = "'" . $sfCode . "'";
            $tableData['Activity_Report_APP']['dcr_activity_date'] = "'" . date('Y-m-d H:i:s') . "'";
        }
        if (isset($tableData['chemists_master'])) {
            $tableData['chemists_master']['sf_code'] = "'" . $sfCode . "'";
        }
        if (isset($tableData['unlisted_doctor_master'])) {
            $tableData['unlisted_doctor_master']['sf_code'] = "'" . $sfCode . "'";
        }
        if (is_array($tableData[$tableName]) && isset($tableData[$tableName][0])) {
            $tableData = $tableData[$tableName];
            for ($j = 0; $j < count($tableData); $j++) {
                $_tableData = $tableData[$j];
                $_tableData = $tableData[$j];
                //Check if $tableData has foreignKey
                if (isset($_tableData['f_key'])) {
                    //Add field values from other tables ie fill in foreign key values
                    foreach ($_tableData['f_key'] as $f => $v) {
                        $_tableData[$f] = $TABLE_KEYS_MAPPING ["'" . $v . "'"];
                    }
                }
                $_tableData['Division_Code'] = $_GET['divisionCode'];
                //Call Save Fuction for tableData
                $fk = saveTableData(array($tableName => $_tableData), $tableName);
            }
        } else {

            //Check if $tableData has foreignKey
            if (isset($tableData[$tableName])) {
                $_tmpTable = $tableData [$tableName];
                if (isset($_tmpTable['f_key'])) {
                    //Add field values from other tables ie fill in foreign key values
                    foreach ($_tmpTable['f_key'] as $f => $v) {
                        $tableData[$tableName][$f] = $TABLE_KEYS_MAPPING[$v];
                    }
                }
            }
//Call Save Fuction for tableData
            $tableData[$tableName]['Division_Code'] = $_GET['divisionCode'];

			$sql4 = "SELECT IdNo FROM Mas_IdNos where SF_code='" . $sfCode . "'";
	        $as = performQuery($sql4);
			$IdNo = (string) $as[0]['IdNo'];

//Set Primary Keys Here
            switch ($tableName) {
                case "chemists_master":
			        $sql4 = "SELECT Territory_Code FROM Territory_Master where SF_Member_code='" . $sfCode . "'";
			        $as = performQuery($sql4);
					$terr = (string) $as[0]['Territory_Code'];
                    $sql = "SELECT isNull(max(cast(replace(Chemists_Code,'$terr/CA','') as numeric)),0)+1 as chemists_code FROM chemists_master where isnumeric(replace(Chemists_Code,'".$terr."/CA',''))=1";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['chemists_code'];

					if($pk<1000) $pkcd=$terr . "/CA0".$pk;
					if($pk<100) $pkcd=$terr . "/CA00".$pk;
					if($pk<10) $pkcd=$terr . "/CA000".$pk;
					
                    $tableData[$tableName]["chemists_code"] = "'".$pkcd."'";
					
					$sql = "SELECT isNull(max(Chemist_SVL_No),0)+1 as svl_code FROM chemists_master where sf_code='" . $sfCode . "'";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['svl_code']; /*$respon['msg'] = $pk;return outputJSON($respon);*/

                    $tableData[$tableName]["Chemist_SVL_No"] =$pk;
                    break;
                case "unlisted_doctor_master" :

                    $sql = "SELECT isNull(max(cast(replace(unlisted_doctor_code,'UL".$IdNo."/','') as numeric)),0)+1 unlisted_doctor_code FROM unlisted_doctor_master where SF_code='" . $sfCode . "'";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['unlisted_doctor_code'];					
                    $tableData[$tableName]["unlisted_doctor_code"] = "'UL".$IdNo."/".$pk."'";
                    break;
                case "Activity_Report_APP":
					$sql = "SELECT isNull(max(cast(replace(APP_ARCd,'PKA/".$IdNo."/','') as numeric)),0)+1 as Activity_Report_Code FROM ((select APP_ARCd from tbActivityCodes where APP_ARCd like 'PKA/".$IdNo."/%')union(select activity_report_code from Activity_Report_App where activity_report_code like 'PKA/".$IdNo."/%'))as stb";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['Activity_Report_Code'];

                    $tableData[$tableName] ["Activity_Report_Code"] = "'PKA/".$IdNo."/".$pk."'";
                    $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["Activity_Report_Code"];
                    break;
                case "Activity_Doctor_Report":
					$sql = "SELECT isNull(max(cast(replace(APP_Code,'PKM/".$IdNo."/','') as numeric)),0)+1 as Activity_MSL_Code FROM tbActivityCodes where APP_Code like 'PKM/".$IdNo."/%'";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['Activity_MSL_Code'];

                    $tableData[$tableName]["Activity_MSL_Code"] = "'PKM/".$IdNo."/".$pk."'";
                    $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["Activity_MSL_Code"];
                    break;
                case "Activity_Stockist_Report":
					$sql = "SELECT isNull(max(cast(replace(APP_Code,'PKS/".$IdNo."/','') as numeric)),0)+1 as activity_stockist_code FROM tbActivityCodes where APP_Code like 'PKS/".$IdNo."/%'";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['activity_stockist_code'];

                    $tableData[$tableName]["activity_stockist_code"] = "'PKS/".$IdNo."/".$pk."'";
                    $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["activity_stockist_code"];
                    break;
                case "Activity_Chemist_Report":
					$sql = "SELECT isNull(max(cast(replace(APP_Code,'PKC/".$IdNo."/','') as numeric)),0)+1 as activity_chemist_code FROM tbActivityCodes where APP_Code like 'PKC/".$IdNo."/%'";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['activity_chemist_code'];

                    $tableData[$tableName]["activity_chemist_code"] = "'PKC/".$IdNo."/".$pk."'";
                    $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["activity_chemist_code"];

                    break;
                case "Activity_UnListedDoctor_Report":
					$sql = "SELECT isNull(max(cast(replace(APP_Code,'PKN/".$IdNo."/','') as numeric)),0)+1 as Activity_MSL_UnListedCode FROM tbActivityCodes where APP_Code like 'PKN/".$IdNo."/%'";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['Activity_MSL_UnListedCode'];

                    $tableData[$tableName]["Activity_MSL_UnListedCode"] = "'PKN/".$IdNo."/".$pk."'";
                    $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["Activity_MSL_UnListedCode"];
                    break;
				case "tbRemdrCall":
					$sql = "SELECT isNull(max(cast(replace(RwID,'RC/".$IdNo."/','') as numeric)),0)+1 as RwID FROM tbRemdrCall where RwID like 'RC/".$IdNo."/%'";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['RwID'];

                    $tableData[$tableName]["RwID"] = "'RC/".$IdNo."/".$pk."'";
                    $TABLE_KEYS_MAPPING["'" . $tableName . "'"] = $tableData[$tableName]["RwID"];
					break;
            }

            $fk = saveTableData($tableData, $tableName, isset($_GET['update']) ? $_GET['update'] :
                            false);
        }
    } $resp["success"] = true;
    echo json_encode($resp);
}

function getDtTPview() {
    $data = json_decode($_POST['data'], true);
    $sfCode = (string) $data['sfCode'];
	$t = strtotime(str_replace("Z","",str_replace("T"," ",$data['tpDate'])));
	$TpDt =  date('Y-m-d 00:00:00',$t);
    $Qry = "exec spTPViewDtws '$sfCode','$TpDt'";
    $respon = performQuery($Qry);
    return outputJSON($respon);
}
function getTPview() {
    $data = json_decode($_POST['data'], true);
    $sfCode = (string) $data['sfCode'];
	$t = strtotime(str_replace("Z","",str_replace("T"," ",$data['mnthYr'])));
	$TpDt =  date('Y-m-d 00:00:00',$t);
    $Qry = "SELECT distinct convert(varchar,[date],103) date,wtype,towns,PlnNo from ((SELECT Tour_Date [date],Tour_Schedule wtype,stuff((select ', '+Town_Name from town_master IT inner join Call_plan C on IT.Town_Code=C.Town_Code  where C.SF_Code=T.SF_Code and Working_Day_no=Tour_Schedule group by town_name for xml path('')),1,2,'') towns,stuff((select ', '+Ref_Plan_Name from Call_plan C where C.SF_Code=T.SF_Code and Working_Day_no=Tour_Schedule group by Ref_Plan_Name for xml path('')),1,2,'') PlnNo,SF_Code sf_code from Tour_plan T where SF_Code='$sfCode' and Tour_Month=month('$TpDt') and Tour_year=year('$TpDt'))union(SELECT Tour_Date [date],Tour_Schedule wtype,stuff((select ', '+Town_Name from town_master T inner join Call_plan C on T.Town_Code=C.Town_Code where Working_Day_no=Tour_Schedule and C.sf_Code=TM.SF_member_Code group by town_name for xml path('')),1,2,'') towns,stuff((select ', '+Ref_Plan_Name from Call_plan C where Working_Day_no=Tour_Schedule and C.sf_Code=TM.SF_member_Code group by Ref_Plan_Name for xml path('')),1,2,'') PlnNo,SF_Code sf_code from Tour_plan_mgr TM where sf_code='$sfCode' and Tour_Month=month('$TpDt') and Tour_year=year('$TpDt'))) as TP";
	$respon = performQuery($Qry);
    return outputJSON($respon);
}

function updEntry()
{	global $conn;
	$today = date('Y-m-d 00:00:00');
    $data = json_decode($_POST['data'], true);
    $SFCode = (string) $data[0]['Activity_Report']['SF_code'];
	$sql = "select SF_Code from Activity_Report1 where sf_Code=".$SFCode." and cast(activity_date as datetime)=cast('$today' as datetime)";
	$result = performQuery($sql);
	if (count($result) <1) {
		$result = array();
        $result['success'] = false;
        $result['type'] = 2;
        $result['msg'] = 'No Call Report Submited...';
        outputJSON($result);
        die;
    }

    $Remarks = (string) $data[0]['Activity_Report']['remarks'];
    $HalfDy = (string) $data[0]['Activity_Report']['HalfDay_FW_Type'];
	
	$sql = "update Activity_Report_App set daywise_remarks=$Remarks where sf_Code=".$SFCode." and worktype_code<>'field work' and cast(convert(varchar,dcr_activity_date,101) as datetime)=cast('$today' as datetime)";
	$result = performQuery($sql);

	$sql = "update Activity_Report1 set Remarks=$Remarks,HalfDay_FW_Type=$HalfDy where sf_Code=".$SFCode." and cast(activity_date as datetime)=cast('$today' as datetime)";
	$result = performQuery($sql);
	$resp["success"] = true;
    echo json_encode($resp);
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
	    //if ($arr[0]['SFStat'] ==0) {
	    	$respon['success'] = true;
	        $respon['sfCode'] = $arr[0]['SF_Code'];
	        $respon['sfName'] = $arr[0]['SF_Name'];
			$respon['divisionCode'] = $arr[0]['Division_Code'];
	        $respon['call_report'] = $arr[0]['call_report'];
	        $respon['desigCode'] = $arr[0]['desig_Code'];
	        $respon['HlfNeed'] = $arr[0]['MGRHlfDy'];
			if($arr[0]['desig_Code']=="MR")
			{
				$query = "Select count(SF_Code) Cnt from Salesforce_Master where sf_Tp_Reporting='".$arr[0]['SF_Code']."'";
				$dsgc = performQuery($query);
				if($dsgc[0]['Cnt']>0) $respon['desigCode']="AM"; else  $respon['HlfNeed'] = $arr[0]['MRHlfDy'];
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
			$respon['SFStat'] =$arr[0]['SFStat'];
	        $respon['days'] = $arr[0]['days'];
	        return outputJSON($respon);
	  //  } else {
	  //  	$respon['success'] = false;
	  //      $respon['msg'] = 'Your status is not active';
	  //      return outputJSON($respon);
	  //  }
    } else {
            $respon['success'] = false;
            $respon['msg'] = "Check User and Password";
            return outputJSON($respon);
    }
}

//date_default_timezone_set("UTC");
$axn = $_GET['axn'];
$value = explode(":", $axn);
switch ($value[0]) {
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
		$sf=$_GET['sf_code'];
		move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photos/".$sf."_".$_FILES["imgfile"]["name"]);
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
        $data = json_decode($_POST['data'], true);

        $divisionCode = (int) $_GET['divisionCode'];
        //$divisionCode = 1;

        $sfCode = (isset($data['sfCode']) && $data['sfCode'] == 0) ? null : $_GET['sfCode'];
        $today = (isset($data['today']) && $data['today'] == 0) ? null : $data['today'];
        $or = (isset($data['or']) && $data['or'] == 0) ? null : $data['or'];
        $wt = (isset($data['wt']) && $data['wt'] == 0) ? null : $data['wt'];
        $tableName = $data['tableName'];
        $coloumns = json_decode($data['coloumns']);

        $where = isset($data['where']) ? json_decode($data['where']) : null;

        $join = isset($data['join']) ? $data['join'] : null;
        $orderBy = isset($data['orderBy']) ? json_decode($data['orderBy']) : null;
        $results;


        if (!is_null($or)) {

            $results = getFromTableWR($tableName, $coloumns, $divisionCode, $sfCode, $orderBy, $where, $join, $today, $wt);
        } else {
            $results = getFromTable($tableName, $coloumns, $divisionCode, $sfCode, $orderBy, $where, $join, $today, $wt);
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

        $query = "SELECT *,isnull((SELECT HalfDay_FW_Type from vwActivity_Report where sf_code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)),'') halfdaywrk from activity_report_APP where SF_Code='".$sfCode."' and worktype_code <> 'field work' and cast(dcr_activity_date as datetime)>=cast('$today' as datetime)";
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