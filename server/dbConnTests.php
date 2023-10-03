<?php
$serverName = "10.0.2.5, 10433";
$connectionInfo = array(
    "Database" => "FMCG_VAPT",
    "LoginTimeout" => 60,
    "UID" => "sa",
    "PWD" => "SanMedia#123",
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
}else{
//closeConn();
echo("Connected..");
}
?>