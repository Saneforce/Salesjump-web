<?php

$serverName = "10.0.2.5, 10433";
$DBName = "FMCG_RAD";

$connectionInfo = array(
    "Database" => $DBName,
    "LoginTimeout" => 30,
    "UID" => "sa",
    "PWD" => "SanMedia#123",
	"CharacterSet" => "UTF-8"
);

$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
    echo "Unable to connect.</br>";
    die(print_r(sqlsrv_errors(), true));
}
?>

