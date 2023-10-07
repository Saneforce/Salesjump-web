<?php

// API access key from Google API's Console
define('FIREBASE_API_KEY', 'AAAAFZqP7mw:APA91bG9q_tSrr3sjaHiMo8fqBI18z8z3KWxsmFeL3L_9AMuLnUoT3kRsmTH2DmvRHZrqSJn9nfVqQZGPmYvG4Z6skH304xWp2Wa7kz_jTVIsGI63t4PwZxtHjbeH-sqXMESogFb5QJ_');



$registrationIds = array( $_GET['id'] );
// prep the bundle
$msg = array
(
	'text' 	=> 'here is a message. message',
	'title'		=> 'Thirumalaivasan',
	'subtitle'	=> 'This is a subtitle. subtitle',
	'tickerText'	=> 'Ticker text here...Ticker text here...Ticker text here',
	'vibrate'	=> 1,
	'sound'		=> 1,
	'largeIcon'	=> 'large_icon',
	'smallIcon'	=> 'small_icon'
);


$fields = array
(
	'registration_ids' 	=> $registrationIds,
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

echo $result;