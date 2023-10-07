<?php
$serverName = "85.195.119.251, 10433";
$DBName="FMCG_Live";
$HostUrl=explode(".", strtolower(str_replace("www.","",$_SERVER['HTTP_HOST'])));
if($HostUrl[0]=="appsshivatex"||$HostUrl[0]=="shivatex") $DBName="FMCG_shivatex";
if($HostUrl[0]=="appsorganomix"||$HostUrl[0]=="organomix") $DBName="FMCG_Organomix";
if($HostUrl[0]=="appsarasan"||$HostUrl[0]=="arasan") $DBName="FMCG_ArasanSanitry";
if($HostUrl[0]=="appsavantika"||$HostUrl[0]=="avantika") $DBName="FMCG_Avantika";
if($HostUrl[0]=="appstrident"||$HostUrl[0]=="trident") $DBName="FMCG_Trident";
if($HostUrl[0]=="appsmarie"||$HostUrl[0]=="marie") $DBName="FMCG_Marie";
if($HostUrl[0]=="appstiesar"||$HostUrl[0]=="tiesar") $DBName="FMCG_TSR";
if($HostUrl[0]=="appseasysol"||$HostUrl[0]=="easysol") $DBName="FMCG_Easysol";
if($HostUrl[0]=="appsdurga"||$HostUrl[0]=="durga") $DBName="FMCG_Durga";
if($HostUrl[0]=="appspgdb"||$HostUrl[0]=="pgdb") $DBName="FMCG_BGDB";
if($HostUrl[0]=="appspgkala"||$HostUrl[0]=="pgkala") $DBName="FMCG_Kala";

if($HostUrl[0]=="appsvhpatel"||$HostUrl[0]=="vhpatel") $DBName="FMCG_VHPatel";
if($HostUrl[0]=="appsallen"||$HostUrl[0]=="allen") $DBName="FMCG_AllenLab";
if($HostUrl[0]=="appsafripipe"||$HostUrl[0]=="afripipe") $DBName="FMCG_Afripipes";

if($HostUrl[0]=="appsprecision"||$HostUrl[0]=="precision") $DBName="FMCG_Precision";
if($HostUrl[0]=="appsshini"||$HostUrl[0]=="shini") $DBName="FMCG_Shini";
if($HostUrl[0]=="appsolzwell"||$HostUrl[0]=="olzwell") $DBName="FMCG_Olzwell";
if($HostUrl[0]=="appsvarnika"||$HostUrl[0]=="varnika") $DBName="FMCG_varnika";
if($HostUrl[0]=="appswalko"||$HostUrl[0]=="walko") $DBName="FMCG_walko";
if($HostUrl[0]=="appsmulberry"||$HostUrl[0]=="mulberry") $DBName="FMCG_mulberry";
if($HostUrl[0]=="appsvasuorg"||$HostUrl[0]=="vasuorg") $DBName="FMCG_vasuorg";
if($HostUrl[0]=="appsbindu"||$HostUrl[0]=="bindu") $DBName="FMCG_bindu";
if($HostUrl[0]=="appsnataraj"||$HostUrl[0]=="nataraj") $DBName="FMCG_nataraj";
if($HostUrl[0]=="appsbgdbl"||$HostUrl[0]=="bgdbl") $DBName="FMCG_bgdbl";
if($HostUrl[0]=="appsgipla"||$HostUrl[0]=="gipla") $DBName="FMCG_GIPLA";
if($HostUrl[0]=="appsrajam"||$HostUrl[0]=="rajam") $DBName="FMCG_RAJAM";
if($HostUrl[0]=="appsarbro"||$HostUrl[0]=="arbro") $DBName="FMCG_ARBRO";
if($HostUrl[0]=="appsleo"||$HostUrl[0]=="leo") $DBName="FMCG_LEO";
if($HostUrl[0]=="appspgsandesh"||$HostUrl[0]=="pgsandesh") $DBName="FMCG_pgsandesh";
if($HostUrl[0]=="appsbrawn"||$HostUrl[0]=="brawn") $DBName="FMCG_BRAWN";
if($HostUrl[0]=="appsfranch"||$HostUrl[0]=="franch") $DBName="FMCG_franch";
if($HostUrl[0]=="appsktzc"||$HostUrl[0]=="ktzc") $DBName="FMCG_KTZC";
if($HostUrl[0]=="appsajanta"||$HostUrl[0]=="ajanta") $DBName="FMCG_AJANTA";
if($HostUrl[0]=="appseverclean"||$HostUrl[0]=="everclean") $DBName="FMCG_EVERCLEAN";
if($HostUrl[0]=="appssimco"||$HostUrl[0]=="simco") $DBName="FMCG_SIMCO";
if($HostUrl[0]=="appsagamahil"||$HostUrl[0]=="agamahil") $DBName="FMCG_AGAMAHIL";
if($HostUrl[0]=="appspgts"||$HostUrl[0]=="pgts") $DBName="FMCG_PGTS";
if($HostUrl[0]=="appspgem"||$HostUrl[0]=="pgem") $DBName="FMCG_PGEM";

$connectionInfo = array(
    "Database" => $DBName,
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

