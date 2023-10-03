<?php
function outputJSON($array) {
    echo json_encode($array);
	sqlsrv_close($conn);
	if(session_status() === PHP_SESSION_ACTIVE) {
		session_unset();
		session_destroy();
	}
}

/**
 * Returns an  array of results. result signature : array( array( DB_COLOUMN1=>VALUE1,DB_COLOUMN2=>VALU2   ) )
 * @global {sqlsrv_conn} $conn db conection object
 * @param {String} $query sql query string
 * @return Boolean|Array
 */
function performQuery($query) {
    global $conn;
    $result = array();

    if ($res = sqlsrv_query($conn, $query)) {

        $qt = explode(" ", $query);
        if (strcmp(strtolower($qt[0]), "select") != 0 && strcmp($qt[0], "exec") != 0) {
            if (sqlsrv_errors() != null)
                return false;
            else
                return true;
        }
        else {

            $result = array();
            while ($row = sqlsrv_fetch_array($res, SQLSRV_FETCH_ASSOC)) {
                $arr = array();
                foreach ($row as $key => $value) {
                    if (is_string($value))
                          $arr[$key] = utf8_encode(trim(preg_replace("/[\r\n]+/", " ", $value)));
                    else {
                        $arr[$key] = $value;
                    }
                }

                $result[] = $arr;
            }

		sqlsrv_free_stmt( $res);
            return $result;
        }
		
	//sqlsrv_commit( $conn );
    }else{
	//	sqlsrv_rollback( $conn );
		
	}

}

function performQueryWP($query, $pram) {
    global $conn;
    $result = array();

    if ($res = sqlsrv_query($conn, $query, $pram)) {

        $qt = explode(" ", $query);

        if (sqlsrv_errors() != null) {
			echo("Error:".$query."<br>");
			outputJSON($pram);
            outputJSON(sqlsrv_errors());
            return false;
        } else{
		sqlsrv_free_stmt( $res);
            return true;
		 //sqlsrv_commit( $conn );
	}
    	}else{
		//sqlsrv_rollback( $conn );
	}
}
