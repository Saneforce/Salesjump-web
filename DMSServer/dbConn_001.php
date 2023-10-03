<?php

	/*$myfile = fopen("../server/elog/errlog_".date('Y_m_d_H_i_s')."_d.txt", "a+");
	fwrite($myfile, "new req ".$_GET['axn']."|");
	fclose($myfile);*/
	
$serverName = "85.195.119.251, 10433";
$connectionInfo = array(
    "Database" => "FMCG_Live",
    "LoginTimeout" => 30,
    "UID" => "sa",
    "PWD" => "dUdr#crewO7-oC",
	"CharacterSet" => "UTF-8"
);

/* Connect using Windows Authentication. */
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
    echo "Unable to connect.</br>";

	/*$myfile = fopen("../server/elog/errlog_".date('Y_m_d_H_i_s').".txt", "a+");
	$sqlsp=sqlsrv_errors();
	fwrite($myfile, $sqlsp);
	fclose($myfile);*/
    die(print_r(sqlsrv_errors(), true));
}
?>

