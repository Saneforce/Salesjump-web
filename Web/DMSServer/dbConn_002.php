<?php
$serverName =  "10.0.2.50, 1433";
function getDB(){
	
	$HostUrl=explode(".", str_replace("www.","",$_SERVER['HTTP_HOST']));
	$DbID=str_replace("apps","",$HostUrl[0]);
	$DBNm="FMCG_Live";
	if($DbID=="fmcg") $DBNm="FMCG_Live";
	else if($DbID=="arasan") $DBNm="FMCG_ArasanSanitry";
	else if($DbID=="tiesar") $DBNm="FMCG_TSR";
	else if($DbID=="allen") $DBNm="FMCG_AllenLab";
	else if($DbID=="afripipe") $DBNm="FMCG_Afripipes";
	else if($DbID=="pgdb") $DBNm="FMCG_BGDB";
	else if($DbID=="pgkala") $DBNm="FMCG_Kala";
	else
		$DBNm="FMCG_".strtoupper($DbID);
	return $DBNm;
}
$DBName=getDB();
$connectionInfo = array(
    "Database" => $DBName,
    "LoginTimeout" => 30,
    "UID" => "sa",
    "PWD" => "SanMedia#123",
	"CharacterSet" => "UTF-8"
);
/* Connect using Windows Authentication. */
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
    echo "Unable to connect.</br>";
    die(print_r(sqlsrv_errors(), true));
}
?>

