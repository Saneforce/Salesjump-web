<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST');
header('Content-Type: application/json; Charset="UTF-8"');
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

$jsonResponse = file_get_contents("php://input");
$data = json_decode($jsonResponse, true);

$id = $data['transaction']['id'];
$status = $data['transaction']['status'];
$intentId = $data['transaction']['intentId'];
$invoice = $data['transaction']['invoice'];

$sQry="update JioMoneyTxns set message = '', status = '$status', tidResponse = '$id', updatedDate = getdate() where tid = '$intentId'";
performQuery($sQry);

if($status === "SUCCESS") {
	performQuery("update Trans_Order_Head set isPaid = 'paid' where Trans_Sl_No = '$invoice'");
}
?>
