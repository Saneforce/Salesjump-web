<?php
$url = "https://secure.ccavenue.com/transaction/getRSAKey";
$fields = array(
        'access_code'=>"AVQV82KF49CC66VQCC",
        'order_id'=>$_GET['order_id']
);

$postvars='';
$sep='';
foreach($fields as $key=>$value)
{
	$postvars.= $sep.urlencode($key).'='.urlencode($value);
	$sep='&';
}

$ch = curl_init();
curl_setopt($ch,CURLOPT_URL,$url);
curl_setopt($ch,CURLOPT_POST,count($fields));
curl_setopt($ch, CURLOPT_CAINFO, 'https://lactalisindia.salesjump.in/server/cacert.pem');
curl_setopt($ch,CURLOPT_POSTFIELDS,$postvars);
curl_setopt($ch,CURLOPT_RETURNTRANSFER,true);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
$result = curl_exec($ch);

echo $result;
?>
