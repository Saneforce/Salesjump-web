<?php

function LiveTracking_Save( $SF_Code, $DivisionCode ) {
    $data = $GLOBALS[ 'Data' ];
    $query = "SELECT sf_emp_id,Employee_Id FROM Mas_salesforce WHERE SF_Code='" . $SF_Code . "'";
    $response = performQuery( $query );
    $empid = $response[ 0 ][ 'sf_emp_id' ];
    $employeeid = $response[ 0 ][ 'Employee_Id' ];
	for ( $ik = 0; $ik < count( $data ); $ik++ ) {
        $query = "INSERT INTO tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr,Auc,EMod,Battery,SF_Mobile,updatetime,IsOnline,Division_code,Track_type,TrackStatus) 
        SELECT '$SF_Code ','$empid','$employeeid','" . $data[ $ik ][ 'Time' ] . "','" . $data[ $ik ][ 'Latitude' ] . "','" . $data[ $ik ][ 'Longitude' ] . "','" . $data[ $ik ][ 'Address' ] . "',
        '','Apps','" . $data[ $ik ][ 'Battery' ] . "','" . $data[ $ik ][ 'Mobile' ] . "',getdate(),'" . $data[ $ik ][ 'IsOnline' ] . "','$DivisionCode','L','".$data[ $ik ][ 'TrackStatus' ]."'";
        performQuery( $query );
    }
    $result = array();
    $result[ 'qry' ] = $query;
    $result[ 'success' ] = true;
    outputJSON( $result );
}
?>