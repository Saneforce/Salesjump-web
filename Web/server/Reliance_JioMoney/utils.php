<?php
function outputJSON($array) {  
	echo str_replace(".000000","",json_encode_unicode($array));
	closeConn();
}
function closeConn(){ 
    	global $conn; 
	sqlsrv_close($conn);
	$conn=null;
	if(session_status() === PHP_SESSION_ACTIVE) {
		session_unset();
		session_destroy();
		if (function_exists('fastcgi_finish_request')) {
				ignore_user_abort(true);
    			fastcgi_finish_request();
		}
	}
}
/**
 * Returns an  array of results. result signature : array( array( DB_COLOUMN1=>VALUE1,DB_COLOUMN2=>VALU2   ) )
 * @global {sqlsrv_conn} $conn db conection object
 * @param {String} $query sql query string
 * @return Boolean|Array
 */
function performQuery($query) {
    global $conn,$DBName;
    $result = array();
if ($res = sqlsrv_query($conn, $query)) {

        $qt = explode(" ", $query);
 if (strcmp(strtolower($qt[0]), "select") != 0 && strcmp($qt[0], "exec") != 0) {
        if (sqlsrv_errors() != null){
                return false;
	}else{
		sqlsrv_free_stmt( $res);
                return true;
	}
        }
        else {

            $result = array();
            while ($row = sqlsrv_fetch_array($res, SQLSRV_FETCH_ASSOC)) {
                $arr = array();
                foreach ($row as $key => $value) {
                    if (is_string($value))
                       	$arr[$key] = trim(preg_replace("/[\r\n]+/", " ", str_replace("&","",$value)));
                    else {
                        $arr[$key] = $value;
                    }
                }

                $result[] = $arr;
            }

		sqlsrv_free_stmt( $res);
            return $result;
        }
    }else{

		//sqlsrv_free_stmt( $res);
			$sqlsp=$DBName." Error:".$query."\n|param:\n|error:".json_encode_unicode(sqlsrv_errors())."\n";
			$sqlsp=$sqlsp."data:".$_POST['data'];
			file_put_contents("../server/log/errlogn_".date('Y_m_d_H_i_s').".txt",$sqlsp,FILE_APPEND);
		//sqlsrv_rollback( $conn );
		
	}
}
function json_encode_unicode($data) {
	if (defined('JSON_UNESCAPED_UNICODE')) {
		return json_encode($data, JSON_UNESCAPED_UNICODE);
	}
	return preg_replace_callback('/(?<!\\\\)\\\\u([0-9a-f]{4})/i',
		function ($m) {
			$d = pack("H*", $m[1]);
			$r = mb_convert_encoding($d, "UTF8", "Windows-1256");
			return $r!=="?" && $r!=="" ? $r : $m[0];
		}, json_encode($data)
	);
}
function performQueryWP($query, $pram) {
    global $conn,$DBName;
    $result = array();

    if ($res = sqlsrv_query($conn, $query, $pram)) {

        $qt = explode(" ", $query);

        if (sqlsrv_errors() != null) {
		echo("Error:".$query."<br>");
		outputJSON($pram);
            outputJSON(sqlsrv_errors());
			
			$sqlsp=$DBName." Error:".$query."\n|param:\n|error:".json_encode_unicode(sqlsrv_errors())."\n";
			$sqlsp=$sqlsp."data:".$_POST['data'];
			file_put_contents("../server/log/errlognk_".date('Y_m_d_H_i_s').".txt",$sqlsp,FILE_APPEND);

		//sqlsrv_free_stmt( $res);
            return false;
        } else{

		sqlsrv_free_stmt( $res);
            return true;}
    }
}
