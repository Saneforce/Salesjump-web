<?php
header( 'Access-Control-Allow-Origin: *' );
header( 'Access-Control-Allow-Methods: POST, GET, OPTIONS' );
//ini_set( 'error_reporting', E_ALL );
//ini_set( 'display_errors', true );
session_start();

include 'db_conn_gps.php';
include 'functions/utils.php';

date_default_timezone_set( "Asia/Kolkata" );
$Date_Format1 = date( 'Y-m-d H:i:s' );
$Date_Format2 = date( 'Y-m-d H:i' );
$Date_Format3 = date( 'Y-m-d' );
$Date_Format4 = date( 'd_m_Y' );
$Date_Format5 = date( 'Ymd' );
$Date_Format6 = date( 'Y-m-d 00:00:00.000' );
$URL_BASE = "/";

$axn = $_GET[ 'axn' ];
$value = explode( ":", $axn );

if ( isset( $_GET[ 'SF_Code' ] ) ) {
    $SF_Code = $_GET[ 'SF_Code' ];
}

if ( isset( $_GET[ 'rSF' ] ) ) {
    $RSF_Code = $_GET[ 'rSF' ];
}

if ( isset( $_GET[ 'DivisionCode' ] ) ) {
    $Div_Code = $_GET[ 'DivisionCode' ];
    $DivCode = explode( ",", $Div_Code . "," );
    $DivisionCode = ( string )$DivCode[ 0 ];
}

$data = json_decode( $_POST[ 'data' ], true );
$GLOBALS[ 'Data' ] = $data;

switch ( strtolower( $value[ 0 ] ) ) {
    case "login":
		$UserName = ( string )$data[ 'Name' ];
		$Password = ( string )$data[ 'Password' ];
		$VersionNo = ( string )$data[ 'VersionNo' ];
		$Mode = ( string )$data[ 'Mode' ];
		$Device_ID = ( string )$data[ 'Device_ID' ];
		$AppDeviceRegID = ( string )$data[ 'AppDeviceRegID' ];
		$Device_Name = '';
		$Device_Version = '';
		if ( ( isset( $data[ 'Device_Name' ] ) ) && ( isset( $data[ 'Device_Version' ] ) ) ) {
			$Device_Name = $data[ 'Device_Name' ];
			$Device_Version = $data[ 'Device_Version' ];
		}
		$sql = "exec GPS_LoginAPP '$UserName','$Password'";
		$response = performQuery( $sql );
		$count = count( $response );
		if ( $count >= 1 ) {
			$result[ 'success' ] = true;
			// $query1 = "UPDATE gps_FieldForce SET DeviceRegId='$AppDeviceRegID', app_device_id='$Device_ID' WHERE sf_code='" . $response[ 0 ][ 'Sf_Code' ] . "'";
			// performQuery( $query1 );

			// $query2 = "INSERT INTO version_ctrl_GPS (sf_code,active_date,signin_time,version,mode,device_version,device_name) SELECT '" . $response[ 0 ][ 'Sf_Code' ] . "','" . $Date_Format6 . "','" . $Date_Format1 . "','" . $VersionNo . "','" . $Mode . "', '" . $Device_Version . "','" . $Device_Name . "'";
			// performQuery( $query2 );
		} else {
			$result[ 'success' ] = false;
			$result[ 'msg' ] = "Check User and Password";
			return outputJSON( $result );
			die;
		}
		$response_result[ 'data' ] = $response;
		$output = ( object )array_merge( ( array )$result, ( array )$response_result );
		outputJSON( $output );
        break;
	case "table/list":
        include 'functions/table_list.php';
        Master_Sync( $SF_Code, $DivisionCode );
        break;
	case "save/live_tracking":
        include 'functions/live_tracking.php';
		LiveTracking_Save( $SF_Code, $DivisionCode );
        break;
	case "save/live_tracking_test":
        include 'functions/live_tracking.php';
		LiveTracking_Save( $SF_Code, $DivisionCode );
        break;
	case "get/sf_track_location":
		$sql = "EXEC GetTrackingDetails '".$SF_Code."','".$_GET[ 'SF_Type' ]."','".$_GET[ 'ReportDate' ]."'";
		$result = performQuery( $sql );
		outputJSON( $result );
        break;
	case "upload/profile":
		$response = array();
		if ( isset( $_FILES[ "file" ] ) ) {
			$File_Path = $_FILES[ 'file' ][ 'name' ];
			$Extension = pathinfo( $File_Path, PATHINFO_EXTENSION );
			$File_Name = "Profile" . "_" . $SF_Code . "." . $Extension;
			if ( move_uploaded_file( $_FILES[ "file" ][ "tmp_name" ],  "functions/profile/" . $File_Name ) ) {
				$response[ "success" ] = true;
				$response[ "message" ] = "Successfully Uploaded";
				$query = "UPDATE GPS_FieldForce SET ProfileImagePath = '".$File_Name."' WHERE SF_Code = '".$SF_Code."'";
				performQuery($query);
			} else {
				$response[ "success" ] = false;
				$response[ "message" ] = "Error while uploading";
			}
		} else {
			$response[ "success" ] = false;
			$response[ "message" ] = "Required Field Missing";
		}
		outputJSON( $response );
        break;
	default:      
        $results[ 'success' ] = false;
        outputJSON( $results );
        break;
}
?>