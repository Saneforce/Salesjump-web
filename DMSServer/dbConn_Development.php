<?php

$serverName = "172.31.23.249, 10433";
$connectionInfo = array(
    "Database" => "HAP_Test",
    "LoginTimeout" => 30,
    "UID" => "sa",
    "PWD" => "#r6Raspojl2hi?e0ra",
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

