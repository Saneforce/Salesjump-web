<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
date_default_timezone_set("Asia/Kolkata");
include "dbConn.php";
include "utils.php";
$authUrl = 'https://pp-apig.jiomoney.com/jfs/v1/app/authenticate';
$intentUrl = 'https://pp-apig.jiomoney.com/payments/jfs/v1/payments/intent';
$traceId = $_GET['uuid'];
$invoice = $_GET['invoice'];
$credentials = performQuery("select top 1 * from Mas_JioMoneyCredentials");
$merchantId = $credentials[0]['merchantId'];
$clientId = $credentials[0]['clientId'];
$secretCode = $credentials[0]['secretCode'];
$returnUrl = $credentials[0]['returnUrl'];
$invoiceInfo = performQuery("select top 1 oh.Cust_Code, oh.Order_Value amount, dr.ListedDr_Name name, isnull(dr.ListedDr_Email, 'email@gmail.com') email, isnull(dr.ListedDr_Mobile, '9876543210') mobile from Trans_Order_Head oh inner join Mas_ListedDr dr on oh.Cust_Code = dr.ListedDrCode where oh.Trans_Sl_No = '$invoice'");
$custCode = $invoiceInfo[0]['Cust_Code'];
$amount = $invoiceInfo[0]['amount'];
$name = $invoiceInfo[0]['name'];
$email = $invoiceInfo[0]['email'];
$mobile = $invoiceInfo[0]['mobile'];
$authRequestData = [
    "application" => [
        "clientId" => "$clientId"
    ],
    "authenticateList" => [
        [
            "mode" => 22,
            "value" => "$secretCode"
        ]
    ],
    "scope" => "SESSION",
    "purpose" => 2
];
$curl = curl_init();
curl_setopt($curl, CURLOPT_URL, $authUrl);
curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($curl, CURLOPT_HTTPHEADER, [
    'x-trace-id: ' . $traceId,
    'Content-Type: application/json'
]);
curl_setopt($curl, CURLOPT_POST, true);
curl_setopt($curl, CURLOPT_POSTFIELDS, json_encode($authRequestData));
$authResponse = curl_exec($curl);
if (curl_errno($curl)) {
    $result['success'] = false;
    $result['message'] = curl_error($curl);
    outputJSON($result);
}
curl_close($curl);
if ($authResponse) {
	$jsonAuthResult = json_decode($authResponse, true);
	if ($jsonAuthResult['status'] == 'SUCCESS') {
		$accessTokenValue = $jsonAuthResult['session']['accessToken']['tokenValue'];
		$appIdentifierToken = $jsonAuthResult['session']['appIdentifierToken'];
		$idempotentKey = rand(11111111, 99999999);
        $currentDateTime = (string) date('Y-m-d\TH:i:s.v\Z');
        $headers = [
            'x-trace-id: ' . $traceId,
            'x-appid-token: ' . $appIdentifierToken,
            'x-app-access-token: ' . $accessTokenValue,
            'Content-Type: application/json'
        ];
        $intentRequestData = [
            "transaction" => [
                "idempotentKey" => "$idempotentKey",
                "invoice" => "$invoice",
                "initiatingEntityTimestamp" => "$currentDateTime",
                "initiatingEntity" => [
                    "returnUrl" => "$returnUrl"
                ],
                "checkout" => [
                    "template" => [
                        "id" => "100"
                    ],
                    "allowed" => [
                        [
                            "rank" => "1",
                            "methodType" => "110",
                            "methodSubType" => "566"
                        ],
                        [
                            "rank" => "2",
                            "methodType" => "110",
                            "methodSubType" => "579"
                        ],
                        [
                            "rank" => "3",
                            "methodType" => "110",
                            "methodSubType" => "581"
                        ],
                        [
                            "rank" => "4",
                            "methodType" => "110",
                            "methodSubType" => "582",
                            "cardType" => [110, 130]
                        ]
                    ]
                ],
                "payer" => [
                    "externalId" => "$custCode",
                    "name" => "$name",
                    "email" => "$email",
                    "mobile" => [
                        "number" => "$mobile",
                        "countryCode" => "91"
                    ]
                ]
            ],
            "amount" => [
                "netAmount" => "$amount"
            ],
            "payee" => [
                "merchantId" => "$merchantId"
            ]
        ];
        $intentCurl = curl_init();
        curl_setopt($intentCurl, CURLOPT_URL, $intentUrl);
        curl_setopt($intentCurl, CURLOPT_POST, true);
        curl_setopt($intentCurl, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($intentCurl, CURLOPT_SSL_VERIFYPEER, false);
        curl_setopt($intentCurl, CURLOPT_HTTPHEADER, $headers);
        curl_setopt($intentCurl, CURLOPT_POSTFIELDS, json_encode($intentRequestData));
        $intentResponse = curl_exec($intentCurl);
        if (curl_errno($intentCurl)) {
            $result['success'] = false;
            $result['message'] = curl_error($intentCurl);
            outputJSON($result);
        }
        curl_close($intentCurl);
        if ($intentResponse) {
            $intentJsonData = json_decode($intentResponse, true);
			$transactionStatus = $intentJsonData['transaction']['status'];
			$transactionId = $intentJsonData['transaction']['id'];
			$xAppIdToken = $intentJsonData['transaction']['metadata']['x-appid-token'];
			$xAppAccessToken = $intentJsonData['transaction']['metadata']['x-app-access-token'];
			$saveToDB = performQuery("insert into JioMoneyTxns (tid, Trans_Sl_No, uniqueKey, appAccessToken, appIdToken, crDate, totalValue) values ('$transactionId', '$invoice', '$traceId', '$xAppAccessToken', '$xAppIdToken', getdate(), '$amount')");
			if ($saveToDB) {
				$result['success'] = true;
				$result['html'] = '
<!DOCTYPE html><html lang="en"><head><meta charset="UTF-8"><meta name="viewport" content="width=device-width, initial-scale=1.0"><title>Document</title></head>
<body style="user-select: none;" class="container m-5">
<form name="payment" method="POST" action="https://pp-checkout.jiopay.com:8443"
enctype="application/x-www-form-urlencoded">
<input type="hidden" name="mid" value="100001000293397" />
<input type="hidden" name="appidtoken" value="' . $xAppIdToken . '" />
<input type="hidden" name="appaccesstoken" value="' . $xAppAccessToken . '" />
<input type="hidden" name="intentid" value="' . $transactionId . '" />
<input type="hidden" id="brandColor" name="brandColor" value="#1997CE" />
<input type="hidden" id="bodyBgColor" name="bodyBgColor" value="#FFFFFF" />
<input type="hidden" id="bodyTextColor" name="bodyTextColor" value="#000000" />
<input type="hidden" id="headingText" name="headingText" value="#FFFFFF" />
<center><img style="margin-top: 50px; width: 110px; height: 100px;" src="https://www.ppro.com/wp-content/uploads/2021/06/JioMoney.png" alt="Jio Money"></center>
<center><p style="margin-top: 50px; font-size:24px;">Redirecting to payment page...</p></center>
<center><p style="margin-top: 25px;">Please don\'t press the back button</p></center>
<center><input style="margin-top: 50px; display:none;" id="myButton" type="submit" value="Pay Now" /></center></form></body></html>';
				outputJSON($result);
			} else {
				$result['success'] = false;
				$result['message'] = 'Unknown error';
				outputJSON($result);
			}
        } else {
			$result['success'] = false;
			$result['message'] = 'Unknown error';
			outputJSON($result);
        }
	} else {
        $result['success'] = false;
        $result['message'] = $jsonAuthResult['error']['message'];
        outputJSON($result);
	}
} else {
    $result['success'] = false;
    $result['message'] = 'Unknown error';
    outputJSON($result);
}
?>