<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');

//session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

 $otp = rand(100000, 999999);
 $mobileNumber = $_GET['mobile'];

 $curl = curl_init();
 //$txt = urlencode("OTP for Retailer app login is " . $otp . "");
$txt = urlencode("Your DCR has been submitted for " . $otp . " -EFORCE");
 $url = "http://bulk.smswave.in/http-api.php?username=SANEFO&password=123123&senderid=EFORCE&route=1&number=" . $mobileNumber . "&message=" . $txt;
 curl_setopt($curl, CURLOPT_URL, $url);
 curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
 $result = curl_exec($curl);

 $res['success'] = true;
 $res['otp'] = $otp;
 $res['mobile'] = $mobileNumber;
 $res['result'] = $result;
 $res['msg'] = "otp sent successfully";
 curl_close($curl);
 echo json_encode($res);
?>