<?php

$serverName = "37.61.220.198,1433";
$connectionInfo = array(
    "Database" => "FMCG_Dev",
    "LoginTimeout" => 30,
    "UID" => "sa",
    "PWD" => "sG3jMzft?9wPV8N",
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