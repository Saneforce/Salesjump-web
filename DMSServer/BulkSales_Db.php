<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

$data = json_decode($_POST['data'], true);

function getDistCustomers(){
	global $data;
	$StkCode=$data[0]["StkCode"];
	$Qry="exec getCustomersByStk '".$StkCode."'";
	return performQuery($Qry);
}

function getDistProducts(){
	global $data;
	$StkCode=$data[0]["StkCode"];
	$Owndiv =$data[0]["DivCode"];
	$Qry="exec GetProductStk '".$StkCode."'";
	$res=performQuery($Qry);
	for($il=0;$il<count($res);$il++){		
		$PCode=$res[$il]["id"];
		$Qry = "select  UOM_Id id,UOM_Nm name,CnvQty ConQty  from vwMas_MUnitDets where PCode='".$PCode."' and Division_Code='" . $Owndiv . "'";
		$Uoms=performQuery($Qry);
		$res[$il]["UOMList"]=$Uoms;
		
		/*$Qry="exec GetProdScheme '".$PCode."','".$StkCode."'";
		$Schs=performQuery($Qry);
		$res["SchemesList"]=$Schs;*/
 
		
	}
	return $res;
}
$axn = $_GET['axn'];
$value = explode(":", $axn);
switch (strtolower($value[0])) {
    case "get/customers":
        outputJSON(getDistCustomers());
        break;
    case "get/products":
        outputJSON(getDistProducts());
        break;
	
	
}
?>