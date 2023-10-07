<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: text/html');
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

$status = $_GET['status'];
$message = $_GET['message'];
$intentid = $_GET['intentid'];
$tid = $_GET['tid'];

$transDetails = performQuery("select top 1 * from JioMoneyTxns where tid = '$intentid'");
$traceId = $transDetails[0]['uniqueKey'];
$accessTokenValue = $transDetails[0]['appAccessToken'];
$appIdentifierToken = $transDetails[0]['appIdToken'];

$headers = [
	'x-trace-id: ' . $traceId,
	'x-app-access-token: ' . $accessTokenValue,
	'x-appid-token: ' . $appIdentifierToken,
	'Content-Type: application/json'
];

$statusRequestData = [
	"transactionId" => "$tid"
];

$statusURL = "https://pp-apig.jiomoney.com/payments/jfs/v2/payments/status";
$statusCurl = curl_init();
curl_setopt($statusCurl, CURLOPT_URL, $statusURL);
curl_setopt($statusCurl, CURLOPT_POST, true);
curl_setopt($statusCurl, CURLOPT_RETURNTRANSFER, true);
curl_setopt($statusCurl, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($statusCurl, CURLOPT_HTTPHEADER, $headers);
curl_setopt($statusCurl, CURLOPT_POSTFIELDS, json_encode($statusRequestData));
$statusResponse = curl_exec($statusCurl);
if (curl_errno($statusCurl)) {
	$result['success'] = false;
	$result['message'] = curl_error($statusCurl);
	outputJSON($result);
}
curl_close($statusCurl);
if ($statusResponse) {
	$statusResponse = json_decode($statusResponse, true);
	$status = $statusResponse['transactionList'][0]['transaction']['status'];
	$netAmount = $statusResponse['transactionList'][0]['amount']['netAmount'];
	$transactionId = $statusResponse['transactionList'][0]['transaction']['id'];
	$intentId = $statusResponse['transactionList'][0]['transaction']['intentId'];
	echo '
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
<title>Transaction Status</title>
</head>
<body style="user-select: none;" class=".container m-5">
<center><h2>Transaction Result</h2></center>
<table class="table table-bordered mt-4">
<tr class="row">
<td class="col">Transaction Status</td>
<td class="col">' . $status . '</td>
</tr>
<tr class="row">
<td class="col">Transaction Amount</td>
<td class="col">₹ ' . $netAmount . '</td>
</tr>
<tr class="row">
<td class="col">Transaction ID</td>
<td class="col">' . $transactionId . '</td>
</tr>
<tr class="row">
<td class="col">Intent ID</td>
<td class="col">' . $intentId . '</td>
</tr>
</table>
</body>
</html>';
} else {
	$result['success'] = false;
	$result['message'] = 'Unknown error';
	outputJSON($result);
}
?>