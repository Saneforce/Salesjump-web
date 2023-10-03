<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
//session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

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
    sqlsrv_close();
    $respon = array();
    $count = count($arr);
    $DIVV=str_replace(",", "", $arr[0]['Division_Code']);
    $query = "insert into Login_Time_Table(Sf_Code,Division_Code,Start_Time,End_Time,Start_Lat,Start_Long,End_Lat,End_Long,login_date) VALUES ('" . $arr[0]['SF_Code'] . "', '" .  $DIVV . "', '".date("h:i:sa")."','','','','','','".date('Y-m-d')."')";
    performQuery($query);
        
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
  	  $respon['Selfie'] = $arr[0]['Selfie'];
        return outputJSON($respon);
    } else {
        $respon['success'] = false;
        $respon['msg'] = "Check User and Password";
        return outputJSON($respon);
    }
}



$axn = $_GET['axn'];
$value = explode(":", $axn);
switch ($value[0]) {
    case "login":
        actionLogin();
        break;	
}
?>