<?php

$serverName = "85.195.119.251, 10433";
$connectionInfo = array(
    "Database" => "FMCG_Live_HAP",
    "LoginTimeout" => 30,
    "UID" => "sa",
    "PWD" => "dUdr#crewO7-oC",
	"CharacterSet" => "UTF-8"
);

/* Connect using Windows Authentication. */
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
    echo "Unable to connect.</br>";
    die(print_r(sqlsrv_errors(), true));
}
?>