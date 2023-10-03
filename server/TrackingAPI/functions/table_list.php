<?php

function Master_Sync( $SF_Code, $DivisionCode ) {
    $data = $GLOBALS[ 'Data' ];
    switch ( strtolower( $data[ 'TableName' ] ) ) {
		case "setup_master":
            $sql = "SELECT Division_Code,Division_Name,Company_Code,WeekOffDays,tracking_time,tracking_interval,DeviceId_Need,CheckInActivity,ISNULL(route_api_key,'') route_api_key,ISNULL(map_api_key,'') map_api_key FROM GPS_Access_Master WHERE Division_Code = '" . $DivisionCode . "'";
            outputJSON( performQuery( $sql ) );
            break;
        case "subordinate_master":
			$query = "SELECT Employee_Id, sf_emp_id, sf_type, Sf_Code, Sf_Name, CONCAT(Sf_Name, ' - ', sf_Designation_Short_Name,' - ', Sf_HQ) SF_Name_HQ, FORMAT(SF_DOB, 'yyyy-MM-dd') SF_DOB, SF_Email, SF_Mobile, CASE WHEN ISNULL(ProfileImagePath,'') <> '' THEN CONCAT('server/functions/profile/', ProfileImagePath) ELSE '' END AS ProfileImagePath FROM GPS_FieldForce WHERE Reporting_To_SF='" . $SF_Code . "'";
            outputJSON( performQuery( $query ) );
            break;
        default:
            $today = ( isset( $data[ 'today' ] ) && $data[ 'today' ] == 0 ) ? null : $data[ 'today' ];
            $or = ( isset( $data[ 'or' ] ) && $data[ 'or' ] == 0 ) ? null : $data[ 'or' ];
            $wt = ( isset( $data[ 'wt' ] ) && $data[ 'wt' ] == 0 ) ? null : $data[ 'wt' ];
            $coloumns = json_decode( $data[ 'coloumns' ] );
            $where = isset( $data[ 'where' ] ) ? json_decode( $data[ 'where' ] ) : null;
            $join = isset( $data[ 'join' ] ) ? $data[ 'join' ] : null;
            $orderBy = isset( $data[ 'orderBy' ] ) ? json_decode( $data[ 'orderBy' ] ) : null;
            if ( !is_null( $or ) ) {
                $results = getFromTableWR( $GLOBALS[ 'TableName' ], $coloumns, $DivisionCode, $SF_Code, $orderBy, $where, $join, $today, $wt );
                outputJSON( $results );
            } else {
                $results = getFromTable( $GLOBALS[ 'TableName' ], $coloumns, $DivisionCode, $SF_Code, $orderBy, $where, $join, $today, $wt );
                outputJSON( $results );
            }
            break;
    }
}

function getFromTableWR( $tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null ) {
    $query = "SELECT " . join( ",", $coloumns ) . " FROM $tableName as tab";
    if ( !is_null( $join ) ) {
        $query .= " join " . join( " join ", $join );
    }
    $query .= " WHERE tab.Division_Code=" . $divisionCode;
    if ( !is_null( $where ) ) {
        $query .= " and " . join( " or ", $where );
    }
    if ( !is_null( $today ) ) {
        $today = date( 'Y-m-d 00:00:00' );
        $query .= "and cast(tab.activity_date as datetime)=cast('$today' as datetime)";
    }
    if ( !is_null( $orderBy ) ) {
        $query .= " ORDER BY " . join( ", ", $orderBy );
    }
    return performQuery( $query );
}

function getFromTable( $tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today, $wt = null ) {
    $query = "SELECT " . join( ",", $coloumns ) . " FROM $tableName as tab";
    if ( !is_null( $join ) ) {
        $query .= " join " . join( " join ", $join );
    }
    if ( !is_null( $sfCode ) ) {
        $query .= " WHERE tab.SF_Code='$sfCode'";
    } else {
        $query .= " WHERE tab.Division_Code=" . $divisionCode;
    }
    if ( !is_null( $where ) ) {
        $query .= " and " . join( " and ", $where );
    }
    if ( !is_null( $today ) ) {
        $query .= " and cast(tab.activity_date as datetime)=cast('$today' as datetime)";
    }
    if ( !is_null( $orderBy ) ) {
        $query .= " ORDER BY " . join( ",", $orderBy );
    }
    return performQuery( $query );
}
?>