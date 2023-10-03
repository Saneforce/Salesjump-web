<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
//session_start();
date_default_timezone_set("Asia/Kolkata");

$URL_BASE = "/";

include "dbConn_test.php";
include "utils.php";

function sendFCM_notify($registrationIds,$message,$title,$msgId,$event_id){
	// API access key from Google API's Console
	define('FIREBASE_API_KEY', 'AAAAFZqP7mw:APA91bG9q_tSrr3sjaHiMo8fqBI18z8z3KWxsmFeL3L_9AMuLnUoT3kRsmTH2DmvRHZrqSJn9nfVqQZGPmYvG4Z6skH304xWp2Wa7kz_jTVIsGI63t4PwZxtHjbeH-sqXMESogFb5QJ_');

	$msg = array
	(
		'text' 	=> $message,
		'title'		=> $title,
		'subtitle'	=> 'Request Alert!',
		'tickerText'	=> 'Request Alert!',
		'vibrate'	=> 1,
		'sound'		=> 1,
		'largeIcon'	=> 'large_icon',
		'smallIcon'	=> 'small_icon',
		'id'		=> $msgId,
		'event_id'	=> $event_id
	);

	$fields = array
	(
		'registration_ids' 	=> array($registrationIds),
		'data'			=> $msg
	);
	
	$headers = array
	(
		'Authorization: key=' . FIREBASE_API_KEY,
		'Content-Type: application/json'
	);
	 
	$ch = curl_init();
	curl_setopt( $ch,CURLOPT_URL, 'https://fcm.googleapis.com/fcm/send' );
	curl_setopt( $ch,CURLOPT_POST, true );
	curl_setopt( $ch,CURLOPT_HTTPHEADER, $headers );
	curl_setopt( $ch,CURLOPT_RETURNTRANSFER, true );
	curl_setopt( $ch,CURLOPT_SSL_VERIFYPEER, false );
	curl_setopt( $ch,CURLOPT_POSTFIELDS, json_encode( $fields ) );
	$result = curl_exec($ch );
	curl_close( $ch );	
}
function getAccBalance(){
	global $data;
	$data = json_decode($_POST['data'], true);
	
	$aStkErp=$data["StkERP"];
	$user_name = 'adminhap';
	$password = '6MyBdmD4haWkTsG';
	$StkErp=str_repeat("0",10-strlen($aStkErp)).$aStkErp;
	
	$host="https://hapi.sanfmcg.com/api/Invoice/Get_Customer_Balance/$StkErp/".date('Y-m-d');
	$headers = array(
	'Content-Type: application/json',
	'Authorization: Basic '. base64_encode("$user_name:$password")
	);
	$ch = curl_init();
	curl_setopt( $ch,CURLOPT_URL, $host );
	curl_setopt( $ch,CURLOPT_POST, false );
	curl_setopt( $ch,CURLOPT_HTTPHEADER, $headers );
	curl_setopt( $ch,CURLOPT_RETURNTRANSFER, true );
	curl_setopt( $ch,CURLOPT_SSL_VERIFYPEER, false );
	
	//$result = curl_exec($ch );
	
	$result = json_decode(curl_exec($ch ),true);
	$sql = "select isnull(sum(Order_Amount),0) Order_Amount from Trans_Distributor_OrderWallet WITH (NOLOCK) where Dist_ERPCode='".$aStkErp."' and flag=0";
	$Pnd=performQuery($sql);
	$LCBAL=floatval(0);
	for($i=0;$i<count($result);$i++){
		$LCBAL=($LCBAL+(floatval($result[$i]["LC_BAL"])));
	}
	$LCBAL=($LCBAL);
	//echo $LCBAL;
	$result[0]["host"]=$host;
	$result[0]["sql"]=$sql;
	$result[0]["Pending"]=$Pnd[0]["Order_Amount"];
	
	if (floatval($LCBAL)>0){
		$result[0]["Balance"]=floatval($LCBAL)+floatval($Pnd[0]["Order_Amount"]);
	}else{
		$result[0]["Balance"]=0-((abs(floatval($LCBAL)))-$Pnd[0]["Order_Amount"]);
	}
	echo json_encode_unicode($result);
	curl_close( $ch );	
}
function getAccountBalance(){
	global $data;
	$data = json_decode($_POST['data'], true);
	
	$aStkErp=$data["StkERP"];
	$user_name = 'adminhap';
	$password = '6MyBdmD4haWkTsG';
	$StkErp=str_repeat("0",10-strlen($aStkErp)).$aStkErp;
	//$host="https://hapi.sanfmcg.com/api/Invoice?CusERPCode=$StkErp&date=".date('Y-m-d');
	
	$host="https://hapi.sanfmcg.com/api/Invoice/Get_Customer_Balance/$StkErp/".date('Y-m-d');
	$headers = array(
	'Content-Type: application/json',
	'Authorization: Basic '. base64_encode("$user_name:$password")
	);
	$ch = curl_init();
	curl_setopt( $ch,CURLOPT_URL, $host );
	curl_setopt( $ch,CURLOPT_POST, false );
	curl_setopt( $ch,CURLOPT_HTTPHEADER, $headers );
	curl_setopt( $ch,CURLOPT_RETURNTRANSFER, true );
	curl_setopt( $ch,CURLOPT_SSL_VERIFYPEER, false );
	
	//$result = curl_exec($ch );
	
	$result = json_decode(curl_exec($ch ),true);
	
	/*$result =[];
	$itm=[];
	$result[]=$itm;
	*/
	$sql = "select isnull(sum(Order_Amount),0) Order_Amount from Trans_Distributor_OrderWallet WITH (NOLOCK) where Dist_ERPCode='".$aStkErp."' and flag=0";
	$Pnd=performQuery($sql);
	if(count($result)>0){
		$LCBAL=floatval(0);
		for($i=0;$i<count($result);$i++){
			$LCBAL=($LCBAL+(floatval($result[$i]["LC_BAL"])));
		}
		$LCBAL=($LCBAL);
		$result[0]["host"]=$host;
		$result[0]["sql"]=$sql;
		$result[0]["Pending"]=$Pnd[0]["Order_Amount"];
		
		if (floatval($LCBAL)>0){
			$result[0]["Balance"]=floatval($LCBAL)+floatval($Pnd[0]["Order_Amount"]);
		}else{
			$result[0]["Balance"]=0-((abs(floatval($LCBAL)))-$Pnd[0]["Order_Amount"]);
		}
		$result[0]["LC_BAL"]=$LCBAL;
		//$result[$i]["T_CURR_BAL"]=$LCBAL;
		//$result[$i]["T_CURR_BAL_LONG"]=$LCBAL;
	}
	else{
		$result[0]["host"]=$host;
		$result[0]["sql"]=$sql;
		$result[0]["Pending"]=$Pnd[0]["Order_Amount"];
		
		if (floatval($result[0]["LC_BAL"])>0){
			$result[0]["Balance"]=floatval($result[0]["LC_BAL"])+$Pnd[0]["Order_Amount"];
		}else{
			$result[0]["Balance"]=0-((abs(floatval($result[0]["LC_BAL"])))-$Pnd[0]["Order_Amount"]);
		}
	}
	$result[0]["BalanceChk"]=true;
	echo json_encode_unicode($result);
	curl_close( $ch );	
	
	/*$ch = curl_init($host);
	$headers = array(
	'Content-Type: application/json',
	'Authorization: Basic '. base64_encode("$user_name:$password")
	);
	curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
	$response = curl_exec($ch);
	if(curl_errno($ch)){
	// throw the an Exception.
	throw new Exception(curl_error($ch));
	}
	curl_close($ch);
	return $response;*/
}

function SavePrimaryOrderSAP($SAPOrd){
	global $data;
	$data = json_decode($_POST['data'], true);
	$StkErp=$data["StkErp"];
	$user_name = 'adminhap';
	$password = '6MyBdmD4haWkTsG';
	$host="https://hapi.sanfmcg.com/api/sap/PrimaryOrder?value="."{\"mainOrder\":".json_encode_unicode($SAPOrd)."}";

	$headers = array(
	'Content-Type: application/json',
	'Authorization: Basic '. base64_encode("$user_name:$password")
	);
	$ch = curl_init();
	curl_setopt( $ch,CURLOPT_URL, $host );
	curl_setopt( $ch,CURLOPT_POST, false );
	curl_setopt( $ch,CURLOPT_HTTPHEADER, $headers );
	curl_setopt( $ch,CURLOPT_RETURNTRANSFER, true );
	curl_setopt( $ch,CURLOPT_SSL_VERIFYPEER, false );
	$result = curl_exec($ch );
	curl_close( $ch );	
	
	return $result;
}

function GetAuditStock(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$plant=$data["plant"];
	$loc=$data["loc"];
	$user_name = 'adminhap';
	$password = '6MyBdmD4haWkTsG';
	$host="https://hapi.sanfmcg.com/api/sap/StockForAudit?plant=$plant&loc=$loc";

	$headers = array(
	'Content-Type: application/json',
	'Authorization: Basic '. base64_encode("$user_name:$password")
	);
	$ch = curl_init();
	curl_setopt( $ch,CURLOPT_URL, $host );
	curl_setopt( $ch,CURLOPT_POST, false );
	curl_setopt( $ch,CURLOPT_HTTPHEADER, $headers );
	curl_setopt( $ch,CURLOPT_RETURNTRANSFER, true );
	curl_setopt( $ch,CURLOPT_SSL_VERIFYPEER, false );
	
	$res = json_decode(curl_exec($ch ),true);
	if(is_array($res)>0)
	{
		$result["success"]=true;
		$result["data"]=$res;
	}
	else
	{		
		$result["success"]=false;
		$result["data"]=[];
	}
	curl_close( $ch );	
	
	return $result;
}
function SaveStockAudit(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	
	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" UOM=\"".$OrderDetails[$j]["product_uom"]."\" Aud=\"".$OrderDetails[$j]["Product_Qty"]."\" Diff=\"".$OrderDetails[$j]["Product_Diff"]."\" SAP=\"".$OrderDetails[$j]["Product_OnHand"]."\" Matr=\"".$OrderDetails[$j]["product_matnr"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	
	$sql = "exec save_Stock_Audit '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."','".$divisionCode."','".$DCRDetails["mcscfaCode"]."',
			'".$DCRDetails["mcscfaName"]."','".$PPXML."','".$DCRHead["UKey"]."','".$DCRDetails["plantType"]."','".$DCRDetails["Lat"]."','".$DCRDetails["Long"]."','".$DCRDetails["No_Of_items"]."'";
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="Stock Audit Submission Failed";
		outputJSON($result);
		die;
	}
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function SavePOSOrder(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	$distot=$DCRDetails["CashDiscount"];
	
	$taxtot=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
	}
	$subtot=$DCRDetails["NetAmount"]-$taxtot;
	
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	$LEGXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" UOM_Nm=\"".$OrderDetails[$j]["UOM_Nm"]."\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["MRP"]."\" PValue=\"".$OrderDetails[$j]["Product_Amount"]."\" Free=\"".$OrderDetails[$j]["free"]."\"  Dis=\"".$OrderDetails[$j]["dis"]."\" Dis_value=\"".$OrderDetails[$j]["dis_value"]."\" con_fac=\"".$OrderDetails[$j]["ConversionFactor"]."\" tax=\"".$prodtxtotal."\" Opc=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Opn=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Opun=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" OScun=\"".$OrderDetails[$j]["Off_Scheme_Unit"]."\" Dtyp=\"".$OrderDetails[$j]["discount_type"]."\" UOM_Id=\"".$OrderDetails[$j]["UOM_Id"]."\" />";
		$LEGXML = $LEGXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Batch=\"\" MFGDate=\"\" ExpDate=\"\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["MRP"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
	$LEGXML = $LEGXML . "</ROOT>";
		
	$HPXML = "<ROOT><Head Sf_Code=\"".$DCRHead["SF"]."\" Stk_Code=\"".$DCRDetails["stockist_code"]."\" Stkname=\"".str_replace("&","and",$DCRDetails["stockist_name"])."\"  Order_Date=\"".$DCRHead["dcr_activity_date"]."\" Order_Value=\"".$DCRDetails["NetAmount"]."\"  Div_Code=\"".$divisionCode."\" Sub_Total=\"".$subtot."\" Tax_Total=\"".$taxtot."\" Dis_Total=\"".$distot."\" /></ROOT>";
	$sql = "exec sp_saveCounterSales_APP '".$DCRDetails["stockist_code"]."','".$divisionCode."','".$HPXML."','".$PPXML."','".$DCRHead["SF"]."','".$TXXML."','".str_replace("&","and",str_replace("'","''",$DCRDetails["name"]))."','".str_replace("&","and",str_replace("'","''",$DCRDetails["address"]))."','".$DCRDetails["phoneNo"]."','".$DCRDetails["payMode"]."','".$DCRDetails["RecAmt"]."','".$DCRDetails["Balance"]."'";
	$params=[];
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="POS Order Submission Failed";
		outputJSON($result);
		die;
	}
	$INVNO=$res[0]["OrderNo"];
	$sql = "exec StockLedgerInv '".$INVNO."','".$DCRDetails["stockist_code"]."','".$DCRHead["dcr_activity_date"]."','".$divisionCode."','".$LEGXML."','POS'";
	$res=performQuery($sql); 
	$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where SF_code='MGR5256' or sf_code in(select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
	$device = performQuery($sql);
	for($ia=0;$ia<count($device);$ia++){
		$reg_id = $device[$ia]['DeviceRegId'];
		if (!empty($reg_id)) {
			//sendFCM_notify($reg_id, "reloadSale","Data Raload",0,'#sign-in');
		}
	}
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	$result["OrderID"]=$INVNO;
	return $result;
}
function SaveStockRotate(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	//$taxtot=$DCRDetails["CGST_TOT"]+$DCRDetails["SGST_TOT"]+$DCRDetails["IGST_TOT"];
	$distot=$DCRDetails["CashDiscount"];
	
	
	$sql = "select st.Stockist_Code,dr.ListedDrCode from Mas_ListedDr dr with (nolock) inner join Mas_Stockist st with (nolock) on st.ERP_Code=dr.CustomerCode where CustomerCode='".$DCRDetails["to"]."' and ListedDr_Active_Flag=0 and dr.Dist_name='".$DCRDetails["from"]."'";
	$stks=performQuery($sql);
	$toCustCode=$stks[0]["ListedDrCode"];
	$tostk=$stks[0]["Stockist_Code"];
	
	$sql = "select dr.ListedDrCode,dr.ListedDr_Name from Mas_ListedDr dr with (nolock) inner join Mas_Stockist st with (nolock) on st.ERP_Code=dr.CustomerCode where st.Stockist_Code='".$DCRDetails["from"]."' and ListedDr_Active_Flag=0 and dr.Dist_name='".$tostk."'";
	$stks=performQuery($sql);
	$fromCust=$stks[0]["ListedDrCode"];
	$fromCustNm=$stks[0]["ListedDr_Name"];
	
	$taxtot=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
	}
	$subtot=$DCRDetails["NetAmount"]-$taxtot;
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	$LEGXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Prod Cd=\"".$OrderDetails[$j]["product_code"]."\" Q=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" NQ=\"".$OrderDetails[$j]["Product_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" V=\"".$OrderDetails[$j]["Product_Amount"]."\"  fr=\"".$OrderDetails[$j]["free"]."\"  Dis=\"".$OrderDetails[$j]["dis"]."\" DVal=\"".$OrderDetails[$j]["dis_value"]."\" tax=\"".$prodtxtotal."\" Opc=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Opn=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Opun=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" OScun=\"".$OrderDetails[$j]["Off_Scheme_Unit"]."\" Dtyp=\"".$OrderDetails[$j]["discount_type"]."\" />";
		
		$LEGXML = $LEGXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Confac=\"1\" Batch=\"\" MFGDate=\"\" ExpDate=\"\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
	$LEGXML = $LEGXML . "</ROOT>";
	
	$sql = "exec [sv_StockRotate]  '".$DCRDetails["from"]."','".$toCustCode."','".$DCRDetails["from"]."','".$DCRHead["Town_code"]."','0','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."',0,0,'',0,0,'1','','".$divisionCode."','".$PPXML."','1','".$subtot."','".$distot."','".$taxtot."','".$DCRDetails["No_Of_items"]."','".$TXXML."','".$DCRDetails["from"]."','".$tostk."'";
	$result["Qry3"]=$sql;
	$res=performQuery($sql); 

	$INVNO=$res[0]["IndentOrderID"];
	$sql = "exec StockLedgerInv '".$INVNO."','".$DCRDetails["from"]."','".$DCRHead["dcr_activity_date"]."','".$divisionCode."','".$LEGXML."','Rotate'";
	$res=performQuery($sql);
	
	$sql = "exec StockLedger '".$INVNO."','".$tostk."','".$DCRHead["dcr_activity_date"]."','".$divisionCode."','".$LEGXML."','Rotate'";	
	$res=performQuery($sql); 
	$result["Qry3"]=$sql;
	$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where SF_code='MGR5256' or sf_code in(select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
	$device = performQuery($sql);
	for($ia=0;$ia<count($device);$ia++){
		$reg_id = $device[$ia]['DeviceRegId'];
		if (!empty($reg_id)) {
			//sendFCM_notify($reg_id, "reloadSale","Daily Inventory",0,'#sign-in');
		}
	}
	
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function saveProjectionEntry(){
	global $data;
    $data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	$taxtot=0;

	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$PPXML = $PPXML . "<Prod Code=\"".$OrderDetails[$j]["product_code"]."\" PQty=\"".$OrderDetails[$j]["Product_Qty"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$sql = "exec saveProjection_App  '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."','".$divisionCode."','".$PPXML."','".$DCRDetails["No_Of_items"]."','".$DCRDetails["plantId"]."','".$DCRDetails["plantName"]."','".$DCRHead["UKey"]."'";
	//$result["Qry3"]=$sql;
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="Projection Submission Failed";
		outputJSON($result);
		die;
	}
	if($res[0]["RetNO"]!=""){
		$result["success"]=true;
		$result["Msg"]="Submitted Successfully";
	}
	else{
		$result["success"]=false;
		$result["Msg"]="Not Authorized.";
	}
	return $result;
}
function saveProjectionEntryNew(){
	global $data;
    $data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	$taxtot=0;

	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$PPXML = $PPXML . "<Prod Code=\"".$OrderDetails[$j]["product_code"]."\" PQty=\"".$OrderDetails[$j]["Product_Qty"]."\" ConFc=\"".$OrderDetails[$j]["ConversionFactor"]."\" UOMId=\"".$OrderDetails[$j]["UOM_Id"]."\" UOMNm=\"".$OrderDetails[$j]["UOM_Nm"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$sql = "exec saveProjection_App_New  '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."','".$divisionCode."','".$PPXML."','".$DCRDetails["No_Of_items"]."','".$DCRDetails["plantId"]."','".$DCRDetails["plantName"]."','".$DCRHead["UKey"]."'";
	//$result["Qry3"]=$sql;
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="Projection Submission Failed";
		outputJSON($result);
		die;
	}
	if($res[0]["RetNO"]!=""){
		$result["success"]=true;
		$result["Msg"]="Submitted Successfully";
	}
	else{
		$result["success"]=false;
		$result["sql"]=$sql;
		$result["Msg"]="Not Authorized.";
	}
	return $result;
}
function SaveIndentOrder(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	//$taxtot=$DCRDetails["CGST_TOT"]+$DCRDetails["SGST_TOT"]+$DCRDetails["IGST_TOT"];
	$distot=$DCRDetails["CashDiscount"];
	
	
	
	$sql = "select st.Stockist_Code,dr.ListedDrCode from Mas_ListedDr dr with (nolock) inner join Mas_Stockist st with (nolock) on st.ERP_Code=dr.CustomerCode where CustomerCode='".$DCRDetails["to"]."' and ListedDr_Active_Flag=0 and dr.Dist_name='".$DCRDetails["from"]."'";
	$stks=performQuery($sql);
	$toCustCode=$stks[0]["ListedDrCode"];
	$tostk=$stks[0]["Stockist_Code"];
	
	$sql = "select dr.ListedDrCode,dr.ListedDr_Name from Mas_ListedDr dr with (nolock) inner join Mas_Stockist st with (nolock) on st.ERP_Code=dr.CustomerCode where st.Stockist_Code='".$DCRDetails["from"]."' and ListedDr_Active_Flag=0 and dr.Dist_name='".$tostk."'";
	$stks=performQuery($sql);
	$fromCust=$stks[0]["ListedDrCode"];
	$fromCustNm=$stks[0]["ListedDr_Name"];
	//$sql = "select Territory_Code from Mas_Stockist with (nolock) where ERP_Code='".$DCRDetails[0]["distCode"]."'";
	//$fromstk=performQuery($sql);
	if($toCustCode=="" || $tostk==""){		
		$result["success"]=false;
		$result["Msg"]="Invalid TO Customer Details";
		
		$sqlsp="data:".$_POST['data'];
		file_put_contents("../server/log/IndentInvlLog_".date('Y_m_d_H_i_s').".txt",$sqlsp,FILE_APPEND);
		return $result;
	}
	if($fromCust==""){		
		$result["success"]=false;
		$result["Msg"]="Invalid From Customer Details";
		$sqlsp="data:".$_POST['data'];
		file_put_contents("../server/log/IndentInvLog_".date('Y_m_d_H_i_s').".txt",$sqlsp,FILE_APPEND);
		return $result;
	}
	
	$sql = "exec svDCRMain_NApp '".$tostk."','".$DCRHead["dcr_activity_date"]."',46,'".$DCRHead["Town_code"]."','',''";
	$result["Qry1"]=$sql;
	$res=performQuery($sql);
	$ACd=$res[0]["ARCode"];
	
	$taxtot=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
	}
	$subtot=$DCRDetails["NetAmount"]-$taxtot;
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Prod Cd=\"".$OrderDetails[$j]["product_code"]."\" Q=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" NQ=\"".$OrderDetails[$j]["Product_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" V=\"".$OrderDetails[$j]["Product_Amount"]."\"  fr=\"".$OrderDetails[$j]["free"]."\" UOM_Cd=\"". $OrderDetails[$j]["UOM_Id"]."\" UOM_Nm=\"". $OrderDetails[$j]["UOM_Nm"]."\" Conv=\"". $OrderDetails[$j]["ConversionFactor"]."\"  Dis=\"".$OrderDetails[$j]["dis"]."\" DVal=\"".$OrderDetails[$j]["dis_value"]."\" tax=\"".$prodtxtotal."\" Opc=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Opn=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Opun=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" OScun=\"".$OrderDetails[$j]["Off_Scheme_Unit"]."\" Dtyp=\"".$OrderDetails[$j]["discount_type"]."\" />";
		
		$pProd = $pProd . (($pProd != "") ? "#" : '') . $OrderDetails[$j]["product_code"] . "~" . $OrderDetails[$j]["Product_Qty"] . "$" . $OrderDetails[$j]["Product_Total_Qty"];
		$npProd = $npProd . (($npProd != "") ? "#" : '') . $OrderDetails[$j]["product_Name"] . "~" . $OrderDetails[$j]["Product_Sample_Qty"] . "$" . $OrderDetails[$j]["Product_Total_Qty"];
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
	
	$sql = "exec [svDCRLstDet_NApp] '".$ACd."','".$tostk."','0','".$fromCust."','".$fromCustNm."','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."','','".$pProd."','".$npProd."','".$DCRHead["Town_code"]."','".$DCRHead["Daywise_Remarks"]."','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["Lat"]."','".$DCRDetails["Long"]."','','".$DCRDetails["orderValue"]."','0','".$tostk."','".str_replace("'","''",$DCRDetails["doctor_name"])."','$divisionCode'";
	$result["Qry2"]=$sql;
	$res=performQuery($sql);
	
	$sql = "exec [sv_IndentOrder]  '".$DCRDetails["from"]."','".$toCustCode."','".$DCRDetails["from"]."','".$DCRHead["Town_code"]."','0','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."',0,0,'',0,0,'1','','".$divisionCode."','".$PPXML."','1','".$subtot."','".$distot."','".$taxtot."','".$DCRDetails["No_Of_items"]."','".$TXXML."','".$DCRDetails["from"]."','".$tostk."'";
	$result["Qry3"]=$sql;
	$res=performQuery($sql); 
	/* $sqlsp="Error:".$sql."\n";
		$sqlsp=$sqlsp."data:".$_POST['data'];
		file_put_contents("../server/log/IndentLog_".date('Y_m_d_H_i_s').".txt",$sqlsp,FILE_APPEND); */
		
	$sql = "exec [svSecOrder_NApp]  '".$tostk."','".$fromCust."','".$tostk."','".$DCRHead["Town_code"]."','0','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."',0,0,'',0,0,'1','".$res[0]["IndentOrderID"]."','".$divisionCode."','".$PPXML."','1','".$subtot."','".$distot."','".$taxtot."','".$DCRDetails["No_Of_items"]."','".$TXXML."'";
	$result["Qry4"]=$sql;
	$res=performQuery($sql); 
/* 	$sqlsp="Error:".$sql."\n";
		$sqlsp=$sqlsp."data:".$_POST['data'];
		file_put_contents("../server/log/IndentLog_".date('Y_m_d_H_i_s').".txt",$sqlsp,FILE_APPEND);
 */	
	$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where SF_code='MGR5256' or sf_code in(select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
	$device = performQuery($sql);
	for($ia=0;$ia<count($device);$ia++){
		$reg_id = $device[$ia]['DeviceRegId'];
		if (!empty($reg_id)) {
			//sendFCM_notify($reg_id, "reloadSale","Daily Inventory",0,'#sign-in');
		}
	}
	
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function saveSalesReturn(){
	global $data;
    $data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	$ReturnImgs=$data[0]["file_Details"];
	$taxtot=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
	}
	$subtot=$DCRDetails["NetAmount"]-$taxtot;
	$returnImageNames="";
	for ($j = 0; $j < count($ReturnImgs); $j++) {
		$returnImageNames=$returnImageNames.",".$ReturnImgs[$j]["SalesReturnImg"];
	}
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	$LEGXML = "<ROOT>";
	
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Prod Code=\"".$OrderDetails[$j]["product_code"]."\" PQty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" Val=\"".$OrderDetails[$j]["Product_Amount"]."\" tax=\"".$prodtxtotal."\" />";
		$LEGXML = $LEGXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Batch=\"\" MFGDate=\"\" ExpDate=\"\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
	$LEGXML = $LEGXML . "</ROOT>";
	$sql = "exec save_SalesReturn_App  '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."','".$DCRDetails["stockist_code"]."','".$DCRDetails["doctor_code"]."','".$DCRDetails["orderValue"]."','".$divisionCode."','".$PPXML."','".$TXXML."','".$DCRDetails["No_Of_items"]."','".$taxtot."','".$subtot."','".$returnImageNames."'";
	$result["Qry3"]=$sql;
	$res=performQuery($sql); 
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="Sales Return Submission Failed";
		outputJSON($result);
		die;
	}
	$INVNO=$res[0]["RetNO"];
	$sql = "exec StockLedgerInv '".$INVNO."','".$DCRDetails["stockist_code"]."','".$DCRHead["dcr_activity_date"]."','".$divisionCode."','".$LEGXML."','Return'";
	$res=performQuery($sql); 
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function SavePrimaryOrderChk(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	//$taxtot=$DCRDetails["CGST_TOT"]+$DCRDetails["SGST_TOT"]+$DCRDetails["IGST_TOT"];
	$distot=$DCRDetails["CashDiscount"];
	$tcstax=$DCRDetails["totAmtTax"];
	$uomlist=$DCRDetails["uom_details"];
	$UKey=$DCRHead["SF"]."-".$DCRHead["UKey"];
	$sql ="insert into PrimaryOrder_Data select '".$DCRHead["SF"]."',getdate(),'".$DCRDetails["Doc_Meet_Time"]."','".str_replace("'","",$_POST['data'])."'";
	performQuery($sql);
	
	$CutoffTime = date("Y-m-d ".$DCRDetails["cutoff_time"]);
	$orderDate=$DCRDetails["Doc_Meet_Time"];
	
	$sql = "select ERP_Code,Plant_id,DivERP from Mas_Stockist where Stockist_Code='".$DCRHead["SF"]."' and Stockist_Active_Flag=0";
	$StkDets=performQuery($sql);
	if(count($StkDets)>0){
		$sql="exec sp_validatePurchaseOrder '".$DCRHead["SF"]."','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["cutoff_time"]."','".$DCRDetails["groupCode"]."'";
		$orderValidate=performQuery($sql);
		echo $sql;
		if(count($orderValidate)>0)
		{
			$orderCount=count($orderValidate);
			$CutoFF=$orderValidate[0]["CutoFF"];
			if($orderValidate[0]["SAPOrderNO"]!="")
			{
				$result["success"]=false;
				$result["Msg"]="Order Already Posted.";
				outputJSON($result);
				die;
			}
			else if(($orderCount>0 && $CutoFF==1) || $orderDate > $CutoffTime){
				$result["success"]=false;
				$result["Msg"]="CutoFF Time Over.";
				outputJSON($result);
				die;
			}
			else if($CutoFF==1){
				$result["success"]=false;
				$result["Msg"]="CutoFF Time Over.";
				outputJSON($result);
				die;
			}
			else if($orderCount>0 && $CutoFF==0 && $DCRDetails["mode"]=="new"){
				$result["success"]=false;
				$result["Msg"]="Order Already Created. Please Edit the Existing Order.";
				outputJSON($result);
				die;
			}
		}
		
		$DCRHead["Worktype_code"]="46";
		$sql = "exec svDCRMain_NApp '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."',".$DCRHead["Worktype_code"].",'".$DCRHead["Town_code"]."','',''";
		$result["Qry1"]=$sql;
		//$res=performQuery($sql);
		$ACd="";
		$taxtot=0;
		$txtotal=$DCRDetails["TOT_TAX_details"];
		for($j = 0; $j < count($txtotal); $j++) {
			$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
		}
		$subtot=$DCRDetails["NetAmount"]-$taxtot;
		
		//StkERP,Plant,Trans_sl_no,OrdItems[]
		$sapOrdDet=[];
		$PPXML = "<ROOT>";
		$TXXML = "<ROOT>";
		for ($j = 0; $j < count($OrderDetails); $j++) {
			$prodtxtotal=0;
			$TAX_details=$OrderDetails[$j]["TAX_details"];
			for($k = 0; $k < count($TAX_details); $k++)
			{
				$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
				$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
			}
			$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" PValue=\"".$OrderDetails[$j]["Product_Amount"]."\" Free=\"".$OrderDetails[$j]["free"]."\"  Dis=\"".$OrderDetails[$j]["dis"]."\" Dis_value=\"".$OrderDetails[$j]["dis_value"]."\" con_fac=\"".$OrderDetails[$j]["ConversionFactor"]."\" tax=\"".$prodtxtotal."\" Opc=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Opn=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Opun=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" OScun=\"".$OrderDetails[$j]["Off_Scheme_Unit"]."\" Dtyp=\"".$OrderDetails[$j]["discount_type"]."\" />";

			array_push($sapOrdDet,array("EPCd"=>$OrderDetails[$j]["Product_ERP"],"OQty"=>$OrderDetails[$j]["Product_Total_Qty"]));
		}
		$PPXML = $PPXML . "</ROOT>";
		$TXXML = $TXXML . "</ROOT>";

		$HPXML = "<ROOT><Head Sf_Code=\"".$DCRHead["SF"]."\" Stk_Code=\"".$DCRDetails["stockist_code"]."\" Order_Date=\"".$DCRDetails["Doc_Meet_Time"]."\" Order_Value=\"".$DCRDetails["NetAmount"]."\"  Div_Code=\"".$divisionCode."\" Sub_Total=\"".$subtot."\" Tax_Total=\"".$taxtot."\" Dis_Total=\"".$distot."\" /></ROOT>";
		$sql = "exec svPrimaryOrder_NApp '".$DCRDetails["stockist_code"]."','".$divisionCode."','".$HPXML."','".$PPXML."','".$DCRHead["SF"]."','".$TXXML."','".$DCRDetails["cutoff_time"]."','".$DCRDetails["mode"]."','".$DCRDetails["orderId"]."','".$DCRHead["AppVer"]."','".$tcstax."','".$UKey."'";
		$params=[];
		$result["Qry3"]=$sql;
		//$res=performQuery($sql);		
		if (sqlsrv_errors() != null) {
			$result["success"]=false;
			$result["Msg"]="Order Submission Failed";
			outputJSON($result);
			die;
		}
		
		//StkERP,Plant,Trans_sl_no,OrdItems[]
		$SAPOrd=[];
		if(count($res)>0 && $res[0]["Msg"]=="Success"){
			$result["RetArr"]=$res;
			if($res[0]["OrderNo"]!=""){
				array_push($SAPOrd,array("StkERP"=>$StkDets[0]["ERP_Code"],"Plant"=>$StkDets[0]["Plant_id"],"DivERPID"=>$StkDets[0]["DivERP"],"Trans_sl_no"=>$res[0]["OrderNo"],"OrdItems"=>$sapOrdDet));
				
				$rtStr="";//SavePrimaryOrderSAP($SAPOrd);
				
				//$salOrder=json_decode($rtStr, true);
				
				$sql="update Trans_Distributor_OrderWallet set flag=2 where Dist_Code='".$DCRDetails["stockist_code"]."' and Order_No='".$res[0]["OrderNo"]."'";
				performQuery($sql);
				
				$sql = "insert into Trans_Distributor_OrderWallet (Dist_Code,Dist_ERPCode,Order_Date,Order_No,SAPOrder_No,Order_Amount,flag) select '".$DCRDetails["stockist_code"]."','".$StkDets[0]["ERP_Code"]."','".$DCRDetails["Doc_Meet_Time"]."','".$res[0]["OrderNo"]."','".$rtStr."',".$DCRDetails["NetAmount"].",0";
				performQuery($sql);
				
				$result["RetQry"]=$sql;
				
				$sql = "delete from Trans_Primary_Order_Uom where Trans_Sl_No='".$res[0]["OrderNo"]."'";
				performQuery($sql);
				for($j = 0; $j < count($uomlist); $j++) 
				{
					$sql = "insert into Trans_Primary_Order_Uom (Trans_Sl_No,uom_name,uom_qty,Created_Date) select '".$res[0]["OrderNo"]."','".$uomlist[$j]["uom_name"]."','".$uomlist[$j]["uom_qty"]."',getdate()";
					performQuery($sql);
				}
				$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where SF_code='MGR5256' or sf_code in(select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
				$device = performQuery($sql);
				for($ia=0;$ia<count($device);$ia++){
					$reg_id = $device[$ia]['DeviceRegId'];
					if (!empty($reg_id)) {
						//sendFCM_notify($reg_id, "reloadSale","Data Raload",0,'#sign-in');
					}
				}
				$result["orderData"]="{\"mainOrder\":".json_encode_unicode($SAPOrd)."}";
				$result["RetStr"]=$rtStr;
				$result["success"]=true;
				$result["Msg"]="Submitted Successfully";
			}
		}
		else{		
			$result["orderData"]="";
			$result["RetStr"]="";
			$result["success"]=false;
			$result["Msg"]=$res[0]["Msg"];
		}
	}
	else{		
		$result["orderData"]="";
		$result["RetStr"]="";
		$result["success"]=false;
		$result["Msg"]="Check Your Login ID";
	}
	return $result;
}


function SavePrimaryOrder(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	//$taxtot=$DCRDetails["CGST_TOT"]+$DCRDetails["SGST_TOT"]+$DCRDetails["IGST_TOT"];
	$distot=$DCRDetails["CashDiscount"];
	$tcstax=$DCRDetails["totAmtTax"];
	$uomlist=$DCRDetails["uom_details"];
	$UKey=$DCRHead["SF"]."-".$DCRHead["UKey"];
	$sql ="insert into PrimaryOrder_Data select '".$DCRHead["SF"]."',getdate(),'".$DCRDetails["Doc_Meet_Time"]."','".str_replace("'","",$_POST['data'])."'";
	performQuery($sql);
	
	$CutoffTime = date("Y-m-d ".$DCRDetails["cutoff_time"]);
	$orderDate=date("Y-m-d h:i:s");
	
	$sql = "select ERP_Code,Plant_id,0 DivERP from Mas_Stockist where Stockist_Code='".$DCRHead["SF"]."' and Stockist_Active_Flag=0";
	$StkDets=performQuery($sql);
	if(count($StkDets)>0){
		$sql="exec sp_validatePurchaseOrder '".$DCRHead["SF"]."','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["cutoff_time"]."','".$DCRDetails["groupCode"]."'";
		$orderValidate=performQuery($sql);
		
		if(count($orderValidate)>0)
		{
			$orderCount=count($orderValidate);
			$CutoFF=$orderValidate[0]["CutoFF"];
			if($orderValidate[0]["SAPOrderNO"]!="")
			{
				$result["success"]=false;
				$result["Msg"]="Order Already Posted.";
				outputJSON($result);
				die;
			}
			else if(($orderCount>0 && $CutoFF==1) || ($orderDate > $CutoffTime)){
				$result["success"]=false;
				$result["Msg"]="CutoFF Time Over.";
				outputJSON($result);
				die;
			}
			else if($CutoFF==1){
				$result["success"]=false;
				$result["Msg"]="CutoFF Time Over.";
				outputJSON($result);
				die;
			}
			/*else if($orderCount>0 && $CutoFF==0 && $DCRDetails["mode"]=="new"){
				$result["success"]=false;
				$result["Msg"]="Order Already Created. Please Edit the Existing Order.";
				outputJSON($result);
				die;
			}*/
		}
		else if($orderDate > $CutoffTime){
			$result["success"]=false;
			$result["Msg"]="CutoFF Time Over.";
			outputJSON($result);
			die;
		}
		
		$DCRHead["Worktype_code"]="46";
		$sql = "exec svDCRMain_NApp '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."',".$DCRHead["Worktype_code"].",'".$DCRHead["Town_code"]."','',''";
		$result["Qry1"]=$sql;
		//$res=performQuery($sql);
		$ACd="";
		$taxtot=0;
		$txtotal=$DCRDetails["TOT_TAX_details"];
		for($j = 0; $j < count($txtotal); $j++) {
			$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
		}
		$subtot=$DCRDetails["NetAmount"]-$taxtot;
		
		//StkERP,Plant,Trans_sl_no,OrdItems[]
		$sapOrdDet=[];
		$PPXML = "<ROOT>";
		$TXXML = "<ROOT>";
		for ($j = 0; $j < count($OrderDetails); $j++) {
			$prodtxtotal=0;
			$TAX_details=$OrderDetails[$j]["TAX_details"];
			for($k = 0; $k < count($TAX_details); $k++)
			{
				$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
				$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
			}
			$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" TQty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" PValue=\"".$OrderDetails[$j]["Product_Amount"]."\" Free=\"".$OrderDetails[$j]["free"]."\"  Dis=\"".$OrderDetails[$j]["dis"]."\" Dis_value=\"".$OrderDetails[$j]["dis_value"]."\" con_fac=\"".$OrderDetails[$j]["ConversionFactor"]."\" tax=\"".$prodtxtotal."\" Opc=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Opn=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Opun=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" OScun=\"".$OrderDetails[$j]["Off_Scheme_Unit"]."\" Dtyp=\"".$OrderDetails[$j]["discount_type"]."\" Qty=\"".$OrderDetails[$j]["Product_Qty"]."\" Prd_unit_nm=\"".$OrderDetails[$j]["Product_unit_name"]."\"/>";

			array_push($sapOrdDet,array("EPCd"=>$OrderDetails[$j]["Product_ERP"],"OQty"=>$OrderDetails[$j]["Product_Total_Qty"]));
		}
		$PPXML = $PPXML . "</ROOT>";
		$TXXML = $TXXML . "</ROOT>";

		$HPXML = "<ROOT><Head Sf_Code=\"".$DCRHead["SF"]."\" Stk_Code=\"".$DCRDetails["stockist_code"]."\" Order_Date=\"".$DCRDetails["Doc_Meet_Time"]."\" Order_Value=\"".$DCRDetails["NetAmount"]."\"  Div_Code=\"".$divisionCode."\" Sub_Total=\"".$subtot."\" Tax_Total=\"".$taxtot."\" Dis_Total=\"".$distot."\" /></ROOT>";
		$sql = "exec svPrimaryOrder_NApp '".$DCRDetails["stockist_code"]."','".$divisionCode."','".$HPXML."','".$PPXML."','".$DCRHead["SF"]."','".$TXXML."','".$DCRDetails["cutoff_time"]."','".$DCRDetails["mode"]."','".$DCRDetails["orderId"]."','".$DCRHead["AppVer"]."','".$tcstax."','".$UKey."'";
		$params=[];
		$result["Qry3"]=$sql;
		$res=performQuery($sql);		
		if (sqlsrv_errors() != null) {
			$result["success"]=false;
			$result["Msg"]="Order Submission Failed";
			outputJSON($result);
			die;
		}
		
		//StkERP,Plant,Trans_sl_no,OrdItems[]
		$SAPOrd=[];
		if(count($res)>0 && $res[0]["Msg"]=="Success"){
			$result["RetArr"]=$res;
			if($res[0]["OrderNo"]!=""){
				array_push($SAPOrd,array("StkERP"=>$StkDets[0]["ERP_Code"],"Plant"=>$StkDets[0]["Plant_id"],"DivERPID"=>$StkDets[0]["DivERP"],"Trans_sl_no"=>$res[0]["OrderNo"],"OrdItems"=>$sapOrdDet));
				
				$rtStr="";//SavePrimaryOrderSAP($SAPOrd);
				
				//$salOrder=json_decode($rtStr, true);
				
				$sql="update Trans_Distributor_OrderWallet set flag=2 where Dist_Code='".$DCRDetails["stockist_code"]."' and Order_No='".$res[0]["OrderNo"]."'";
				performQuery($sql);
				
				$sql = "insert into Trans_Distributor_OrderWallet (Dist_Code,Dist_ERPCode,Order_Date,Order_No,SAPOrder_No,Order_Amount,flag) select '".$DCRDetails["stockist_code"]."','".$StkDets[0]["ERP_Code"]."','".$DCRDetails["Doc_Meet_Time"]."','".$res[0]["OrderNo"]."','".$rtStr."',".$DCRDetails["NetAmount"].",0";
				performQuery($sql);
				
				$result["RetQry"]=$sql;
				
				$sql = "delete from Trans_Primary_Order_Uom where Trans_Sl_No='".$res[0]["OrderNo"]."'";
				performQuery($sql);
				for($j = 0; $j < count($uomlist); $j++) 
				{
					$sql = "insert into Trans_Primary_Order_Uom (Trans_Sl_No,uom_name,uom_qty,Created_Date) select '".$res[0]["OrderNo"]."','".$uomlist[$j]["uom_name"]."','".$uomlist[$j]["uom_qty"]."',getdate()";
					performQuery($sql);
				}
				$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where SF_code='MGR5256' or sf_code in(select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
				$device = performQuery($sql);
				for($ia=0;$ia<count($device);$ia++){
					$reg_id = $device[$ia]['DeviceRegId'];
					if (!empty($reg_id)) {
						//sendFCM_notify($reg_id, "reloadSale","Data Raload",0,'#sign-in');
					}
				}
				$result["orderData"]="{\"mainOrder\":".json_encode_unicode($SAPOrd)."}";
				$result["RetStr"]=$rtStr;
				$result["success"]=true;
				$result["Msg"]="Submitted Successfully";
			}
		}
		else{		
			$result["orderData"]="";
			$result["RetStr"]="";
			$result["success"]=false;
			$result["Msg"]=$res[0]["Msg"];
		}
	}
	else{		
		$result["orderData"]="";
		$result["RetStr"]="";
		$result["success"]=false;
		$result["Msg"]="Check Your Login ID";
	}
	return $result;
}

/*function SavePrimaryOrder(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	//$taxtot=$DCRDetails["CGST_TOT"]+$DCRDetails["SGST_TOT"]+$DCRDetails["IGST_TOT"];
	$distot=$DCRDetails["CashDiscount"];
	$sql = "select wtype from tbmydayplan where sf_code='".$DCRHead["SF"]."' and convert(date,pln_date)=Convert(date,'".$DCRHead["dcr_activity_date"]."')";
	$plnwtype=performQuery($sql);
	if(count($plnwtype)>0){
		$wtypecode=$plnwtype[0]["wtype"];
	}
	else{
		$wtypecode="46";
	}
	$DCRHead["Worktype_code"]=$wtypecode;
	$sql = "exec svDCRMain_NApp '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."',".$DCRHead["Worktype_code"].",'".$DCRHead["Town_code"]."','',''";
	$result["Qry1"]=$sql;
	$res=performQuery($sql);
	$ACd=$res[0]["ARCode"];
	$taxtot=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
	}
	$subtot=$DCRDetails["NetAmount"]-$taxtot;
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" PName=\"".$OrderDetails[$j]["product_Name"]."\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" PValue=\"".$OrderDetails[$j]["Product_Amount"]."\" Free=\"".$OrderDetails[$j]["free"]."\"  Dis=\"".$OrderDetails[$j]["dis"]."\" Dis_value=\"".$OrderDetails[$j]["dis_value"]."\" con_fac=\"".$OrderDetails[$j]["ConversionFactor"]."\" tax=\"".$prodtxtotal."\" Opc=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Opn=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Opun=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" OScun=\"".$OrderDetails[$j]["Off_Scheme_Unit"]."\" Dtyp=\"".$OrderDetails[$j]["discount_type"]."\" />";

		//$pProd = $pProd . (($pProd != "") ? "#" : '') . $OrderDetails[$j]["product_code"] . "~" . $OrderDetails[$j]["Product_Qty"] . "$" . $OrderDetails[$j]["Product_Total_Qty"];
		//$npProd = $npProd . (($npProd != "") ? "#" : '') . $OrderDetails[$j]["product_Name"] . "~" . $OrderDetails[$j]["Product_Sample_Qty"] . "$" . $OrderDetails[$j]["Product_Total_Qty"];
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
		
	//$sql = "exec [svDCRLstDet_NApp] '".$ACd."','".$DCRHead["SF"]."','".$DCRDetails["EType"]."','".$DCRDetails["doctor_code"]."','".$DCRDetails["doctor_name"]."','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."','','".$pProd."','".$npProd."','".$DCRHead["Town_code"]."','".$DCRHead["Daywise_Remarks"]."','".$DCRDetails["modified_time"]."','".$DCRDetails["Lat"]."','".$DCRDetails["Long"]."','','".$DCRDetails["orderValue"]."','0','".$DCRDetails["stockist_code"]."','".$DCRDetails["stockist_name"]."'";
	$HPXML = "<ROOT><Head Sf_Code=\"".$DCRHead["SF"]."\" Stk_Code=\"".$DCRDetails["stockist_code"]."\" Order_Date=\"".$DCRDetails["Doc_Meet_Time"]."\" Order_Value=\"".$DCRDetails["NetAmount"]."\"  Div_Code=\"".$divisionCode."\" Sub_Total=\"".$subtot."\" Tax_Total=\"".$taxtot."\" Dis_Total=\"".$distot."\" /></ROOT>";
	$sql = "{call svPrimaryOrder_NApp  ('".$DCRDetails["stockist_code"]."','".$divisionCode."','".$HPXML."','".$PPXML."','".$DCRHead["SF"]."','".$TXXML."','".$DCRDetails["cutoff_time"]."','".$DCRDetails["mode"]."','".$DCRDetails["orderId"]."')}";
	$params=[];
	$result["Qry3"]=$sql;
	$res=performQueryWP($sql, $params);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="Order Submission Failed";
		outputJSON($result);
		die;
	}
	$sql = "SELECT DeviceRegId FROM Access_Table where SF_code='MGR5256' or sf_code in(select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
	$device = performQuery($sql);
	for($ia=0;$ia<count($device);$ia++){
		$reg_id = $device[$ia]['DeviceRegId'];
		if (!empty($reg_id)) {
			sendFCM_notify($reg_id, "reloadSale","Daily Inventory",0,'#sign-in');
		}
	}
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}*/
function SaveDCRCalls(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	
	if($DCRDetails["doctor_code"]==""){		
		$result["success"]=false;
		$result["Msg"]="Invalid Customer Details";
		return $result;
	}
	//$taxtot=$DCRDetails["CGST_TOT"]+$DCRDetails["SGST_TOT"]+$DCRDetails["IGST_TOT"];
	$distot=$DCRDetails["CashDiscount"];
	$sql = "select wtype from tbmydayplan WITH (NOLOCK) where sf_code='".$DCRHead["SF"]."' and convert(date,pln_date)=Convert(date,'".$DCRHead["dcr_activity_date"]."')";
	$plnwtype=performQuery($sql);
	if(count($plnwtype)>0){
		$wtypecode=$plnwtype[0]["wtype"];
	}
	else{
		$wtypecode="46";
	}
	
	$DCRHead["Worktype_code"]=$wtypecode;
	$sql = "exec svDCRMain_NApp '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."',".$DCRHead["Worktype_code"].",'".$DCRHead["Town_code"]."','',''";
	$result["Qry1"]=$sql;
	$res=performQuery($sql);
	$ACd=$res[0]["ARCode"];
	$taxtot=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
	}
	$subtot=$DCRDetails["NetAmount"]-$taxtot;
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Prod Cd=\"".$OrderDetails[$j]["product_code"]."\" Q=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" NQ=\"".$OrderDetails[$j]["Product_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" V=\"".$OrderDetails[$j]["Product_Amount"]."\"  fr=\"".$OrderDetails[$j]["free"]."\" UOM_Cd=\"". $OrderDetails[$j]["UOM_Id"]."\" UOM_Nm=\"". $OrderDetails[$j]["UOM_Nm"]."\" Conv=\"". $OrderDetails[$j]["ConversionFactor"]."\"  Dis=\"".$OrderDetails[$j]["dis"]."\" DVal=\"".$OrderDetails[$j]["dis_value"]."\" tax=\"".$prodtxtotal."\" Opc=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Opn=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Opun=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" OScun=\"".$OrderDetails[$j]["Off_Scheme_Unit"]."\" Dtyp=\"".$OrderDetails[$j]["discount_type"]."\" />";

		$pProd = $pProd . (($pProd != "") ? "#" : '') . $OrderDetails[$j]["product_code"] . "~" . $OrderDetails[$j]["Product_Qty"] . "$" . $OrderDetails[$j]["Product_Total_Qty"];
		$npProd = $npProd . (($npProd != "") ? "#" : '') . $OrderDetails[$j]["product_Name"] . "~" . $OrderDetails[$j]["Product_Sample_Qty"] . "$" . $OrderDetails[$j]["Product_Total_Qty"];
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
		
	$sql = "exec [svDCRLstDet_NApp] '".$ACd."','".$DCRHead["SF"]."','".$DCRDetails["EType"]."','".$DCRDetails["doctor_code"]."','".str_replace("'","''",$DCRDetails["doctor_name"])."','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."','','".$pProd."','".$npProd."','".$DCRHead["Town_code"]."','".$DCRHead["Daywise_Remarks"]."','".$DCRDetails["modified_time"]."','".$DCRDetails["Lat"]."','".$DCRDetails["Long"]."','','".$DCRDetails["orderValue"]."','0','".$DCRDetails["stockist_code"]."','".str_replace("'","''",$DCRDetails["stockist_name"])."','$divisionCode'";
	$result["Qry2"]=$sql;
	$res=performQuery($sql); 
	$ACd=$res[0]["ARDCd"];
	if($DCRDetails["ordertype"] == "order"){
		$sql = "exec [svSecOrder_NApp]  '".$DCRHead["SF"]."','".$DCRDetails["doctor_code"]."','".$DCRDetails["stockist_code"]."','".$DCRHead["Town_code"]."','0','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."',0,0,'',0,0,'1','".$ACd."','".$divisionCode."','".$PPXML."','1','".$subtot."','".$distot."','".$taxtot."','".$DCRDetails["No_Of_items"]."','".$TXXML."'";
		$result["Qry3"]=$sql;
		$res=performQuery($sql); 
		
		$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where SF_code='MGR5256' or sf_code in(select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
		$device = performQuery($sql);
		for($ia=0;$ia<count($device);$ia++){
			$reg_id = $device[$ia]['DeviceRegId'];
			if (!empty($reg_id)) {
				//sendFCM_notify($reg_id, "reloadSale","Daily Inventory",0,'#sign-in');
			}
		}

	}
	
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function savePOPApproval(){
	global $data;
    $data = json_decode($_POST['data'], true);
    $result=[];
	$pophead = $data[0]["POP_Header"];
	$popfiles = $data[0]["file_Details"];
	$popImages = "";
	for($i=0;$i<count($popfiles);$i++){
		if($popfiles[$i]["pop_filename"]!=null)
		{
			$popImages=$popImages.",".$popfiles[$i]["pop_filename"];
		}
	}
	$div = $pophead["divisionCode"];
	$popSlNo = $pophead["pop_reqId"];
	$popReqNo = $pophead["pop_TransNo"];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$sql="update Trans_POP_Entry_Detail set POP_Status_Flag=1,Approved_Date='".$pophead["date"]."',Approved_by='".$pophead["sfCode"]."',Images='".$popImages."' where Trans_Sl_No='".$popReqNo."' and Sl_No='".$popSlNo."'";
	$res=performQuery($sql);
	$result["data"]=$sql;
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function updateDeliverySequence(){
	global $data;
    $data = json_decode($_POST['data'], true);
    $result=[];
	$retid = $data["RetailerID"];
	$slno = $data["SlNo"];
	$sql="update Mas_ListedDr set ListedDr_Sl_No=".$slno." where ListedDrCode='$retid'";
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["sql"]=$sql;
		$result["Msg"]="Failed to Update Delivery Sequence.";
		outputJSON($result);
		die;
	}
	$result["success"]=true;
	$result["Msg"]="Delivery Sequence Updated.";
	return $result;
}
function GRNEntry(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	
	$taxtot=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtot = $taxtot + $txtotal[$j]["Tax_Amt"];
	}
	$subtot=$DCRDetails["NetAmount"]-$taxtot;
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	$SPXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Prcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Prod Code=\"".$OrderDetails[$j]["product_code"]."\" UOM_Id=\"".$OrderDetails[$j]["UOM_Id"]."\" UOM_Nm=\"".$OrderDetails[$j]["UOM_Nm"]."\" PQty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" InQty=\"".$OrderDetails[$j]["Product_InvQty"]."\" Dev=\"".$OrderDetails[$j]["deviation"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" DVal=\"0\" Val=\"".$OrderDetails[$j]["Product_Amount"]."\" fr=\"".$OrderDetails[$j]["free"]."\" Dis=\"".$OrderDetails[$j]["dis"]."\" Conf=\"".$OrderDetails[$j]["ConversionFactor"]."\" OScun=\"\" Dtyp=\"".$OrderDetails[$j]["discount_type"]."\" Opc=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Opn=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Opun=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" tax=\"".$prodtxtotal."\" Batch=\"".$OrderDetails[$j]["batch_no"]."\" MFG=\"".$OrderDetails[$j]["mfg"]."\" />";
		
		$SPXML = $SPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Batch=\"".$OrderDetails[$j]["batch_no"]."\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" MFGDate=\"".$OrderDetails[$j]["mfg"]."\" ExpDate=\"".$OrderDetails[$j]["exp"]."\" Confac=\"".$OrderDetails[$j]["ConversionFactor"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
	$SPXML = $SPXML . "</ROOT>";
	$sql = "exec saveGrnDetails_App '".$divisionCode."','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["TransSlNo"]."','".$subtot."','".$taxtot."','".$DCRDetails["NetAmount"]."','".$DCRDetails["stockist_code"]."','".$DCRDetails["stockist_name"]."','".$PPXML."','".$TXXML."'";
	
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Error"]=sqlsrv_errors();
		$result["SQL"]=$sql;
		$result["Msg"]="GRN Submission Failed";
		outputJSON($result);
		die;
	}
	
	$sql = "exec StockLedger '".$DCRDetails["TransSlNo"]."','".$DCRDetails["stockist_code"]."','".$DCRDetails["Doc_Meet_Time"]."','".$divisionCode."','".$SPXML."'";	
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Error"]=sqlsrv_errors();
		$result["SQL"]=$sql;
		$result["Msg"]="Stock Updation Submission Failed";
		outputJSON($result);
		die;
	}
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function SaveInvoice(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$loginType = $_GET["loginType"];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	$taxtotal=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	$orderType=$DCRDetails["ordertype"];$wtypecode="46";
	$DCRHead["Worktype_code"]=$wtypecode;
	$sql = "exec svDCRMain_NApp '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."',".$DCRHead["Worktype_code"].",'".$DCRHead["Town_code"]."','',''";
	$res=performQuery($sql);
	$ACd=$res[0]["ARCode"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtotal = $taxtotal + $txtotal[$j]["Tax_Amt"];
	}
	$HeadXML = "<ROOT>";
	$HeadXML = $HeadXML . "<Details SfCode=\"".$DCRHead["SF"]."\" Dis_Code=\"".$DCRDetails["stockist_code"]."\" Dis_Name=\"".str_replace("'","",str_replace("&","and",$DCRDetails["stockist_name"]))."\" Cust_Code=\"".$DCRDetails["doctor_code"]."\" Cust_Name=\"".str_replace("'","",str_replace("&","and",$DCRDetails["doctor_name"]))."\" Inv_Date=\"".$DCRHead["dcr_activity_date"]."\" Pay_Term=\"".$DCRDetails["payType"]."\" Dis_total=\"".$DCRDetails["CashDiscount"]."\"  Tax_Total=\"".$DCRDetails["txtot"]."\" Total=\"".$DCRDetails["NetAmount"]."\" Div_Code=\"".$divisionCode."\" taxtotal=\"".$taxtotal."\" TCS=\"0\" TDS=\"0\" />";
	$HeadXML = $HeadXML . "</ROOT>";
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Products Product_code=\"".$OrderDetails[$j]["product_code"]."\" Qty=\"".$OrderDetails[$j]["Product_Qty"]."\" Quantity=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" BatchNo=\"".$OrderDetails[$j]["BatchNo"]."\" Price=\"".$OrderDetails[$j]["Rate"]."\" MRP=\"".$OrderDetails[$j]["MRP"]."\" Pro_Unit=\"\" Amount=\"".$OrderDetails[$j]["Product_Amount"]."\" Free=\"".$OrderDetails[$j]["free"]."\" Discount=\"".$OrderDetails[$j]["dis"]."\" Offer_Pro_Code=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Offer_Pro_Name=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Offer_Pro_Unit=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" txtotal=\"".$prodtxtotal."\" UOM_Id=\"".$OrderDetails[$j]["UOM_Id"]."\" UOM_Nm=\"".$OrderDetails[$j]["UOM_Nm"]."\" ConversionFactor=\"".$OrderDetails[$j]["ConversionFactor"]."\" />";
		
		$pProd = $pProd . (($pProd != "") ? "#" : '') . $OrderDetails[$j]["product_code"] . "~" . $OrderDetails[$j]["Product_Qty"] . "$" . $OrderDetails[$j]["Product_Total_Qty"];
		$npProd = $npProd . (($npProd != "") ? "#" : '') . $OrderDetails[$j]["product_Name"] . "~" . $OrderDetails[$j]["Product_Qty"] . "$" . $OrderDetails[$j]["Product_Total_Qty"];
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
	
	$sql = "exec [svDCRLstDet_NApp] '".$ACd."','".$DCRHead["SF"]."','1','".$DCRDetails["doctor_code"]."','".str_replace("'","''",$DCRDetails["doctor_name"])."','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."','','".$pProd."','".$npProd."','".$DCRHead["Town_code"]."','".$DCRHead["Daywise_Remarks"]."','".$DCRDetails["modified_time"]."','".$DCRDetails["Lat"]."','".$DCRDetails["Long"]."','','".$DCRDetails["orderValue"]."','0','".$DCRDetails["stockist_code"]."','".str_replace("'","''",$DCRDetails["stockist_name"])."','$divisionCode'";
	$res=performQuery($sql);
	$sql = "exec [Sv_Invoice_App] '".$DCRDetails["doctor_code"]."','".$loginType."','".$DCRDetails["orderId"]."','".$HeadXML."','".$PPXML."','".$TXXML."','".$DCRDetails["Lat"]."','".$DCRDetails["Long"]."','".$DCRHead["UKey"]."','".$orderType."','".$DCRDetails["stockist_code"]."'";
	$result["invqry"]=$sql;
	$res=performQuery($sql);
	$result["data"]=$res;
        $result["success"]=$res[0]['success'];
	$result["Msg"]=$res[0]['msg'];


	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$INVNO=$res[0]["InvNO"];
	//if($DCRDetails["payType"]!="" && $res[0]["InvNO"]!="")
	//{
		if($DCRDetails["PAYAmount"]!="") {
			$sql = "exec saveAppPayment '".$DCRHead["SF"]."','".$loginType."','".$DCRDetails["doctor_code"]."',".$DCRDetails["PAYAmount"].",'".$DCRDetails["payType"]."','".$DCRHead["dcr_activity_date"]."','".$DCRDetails["stockist_code"]."','".$divisionCode."','".$res[0]["InvNO"]."'";
			$result["Qry3"]=$sql;
			$res=performQuery($sql);
		}
	if($orderType!="Van Sales")
	{
		$sql = "exec StockLedgerInv '".$INVNO."','".$DCRDetails["stockist_code"]."','".$DCRDetails["Doc_Meet_Time"]."','".$divisionCode."','".$PPXML."','INV'";
		$res=performQuery($sql);
	}
	//}
	$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where  SF_code='MGR5256'";// sf_code in (select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
	$device = performQuery($sql);
	for($ia=0;$ia<count($device);$ia++){
		$reg_id = $device[$ia]['DeviceRegId'];
		if (!empty($reg_id)) {
			//sendFCM_notify($reg_id, "reloadSale","Daily Inventory",0,'#sign-in');
		}
	}
	/*$sql = "select device_id DeviceRegId  from  Mas_CusFCMToken with(nolock) where CustID='".$DCRDetails["doctor_code"]."'";
	$device = performQuery($sql);
	for($ia=0;$ia<count($device);$ia++){
		$reg_id = $device[$ia]['DeviceRegId'];
		if (!empty($reg_id)) {
			sendFCM_notify($reg_id, "Your Order Invoiced by ".str_replace("'","''",$DCRDetails["stockist_name"])." - ".$INVNO,"Sales Invoice",0,'#sign-in');
		}
	}*/
	//$result["success"]=true;
	//$result["Msg"]="Submitted Successfully";
	return $result;
}
function SaveVanInvoice(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$result=[];
	$loginType = $_GET["loginType"];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	$taxtotal=0;
	$txtotal=$DCRDetails["TOT_TAX_details"];
	$orderType=$DCRDetails["ordertype"];
	for($j = 0; $j < count($txtotal); $j++) {
		$taxtotal = $taxtotal + $txtotal[$j]["Tax_Amt"];
	}
	$HeadXML = "<ROOT>";
	$HeadXML = $HeadXML . "<Details SfCode=\"".$DCRHead["SF"]."\" Dis_Code=\"".$DCRDetails["stockist_code"]."\" Dis_Name=\"".str_replace("&","and",$DCRDetails["stockist_name"])."\" Cust_Code=\"".$DCRDetails["doctor_code"]."\" Cust_Name=\"".str_replace("&","and",$DCRDetails["doctor_name"])."\" Inv_Date=\"".$DCRHead["dcr_activity_date"]."\" Pay_Term=\"".$DCRDetails["payType"]."\" Dis_total=\"".$DCRDetails["CashDiscount"]."\"  Tax_Total=\"".$DCRDetails["txtot"]."\" Total=\"".$DCRDetails["NetAmount"]."\" Div_Code=\"".$divisionCode."\" taxtotal=\"".$taxtotal."\" TCS=\"0\" TDS=\"0\" />";
	$HeadXML = $HeadXML . "</ROOT>";
	$PPXML = "<ROOT>";
	$TXXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$prodtxtotal=0;
		$TAX_details=$OrderDetails[$j]["TAX_details"];
		for($k = 0; $k < count($TAX_details); $k++)
		{
			$prodtxtotal=$prodtxtotal+$TAX_details[$k]["Tax_Amt"];
			$TXXML = $TXXML . "<Products Pcode=\"".$OrderDetails[$j]["product_code"]."\" txcd=\"".$TAX_details[$k]["Tax_Id"]."\" txval=\"".$TAX_details[$k]["Tax_Val"]."\" txnm=\"".$TAX_details[$k]["Tax_Type"]."\" txamt=\"".$TAX_details[$k]["Tax_Amt"]."\"  />";
		}
		$PPXML = $PPXML . "<Products Product_code=\"".$OrderDetails[$j]["product_code"]."\" Quantity=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Price=\"".$OrderDetails[$j]["Rate"]."\" Pro_Unit=\"\" Amount=\"".$OrderDetails[$j]["Product_Amount"]."\" Free=\"".$OrderDetails[$j]["free"]."\" Discount=\"".$OrderDetails[$j]["dis"]."\" Offer_Pro_Code=\"".$OrderDetails[$j]["Off_Pro_code"]."\" Offer_Pro_Name=\"".$OrderDetails[$j]["Off_Pro_name"]."\" Offer_Pro_Unit=\"".$OrderDetails[$j]["Off_Pro_Unit"]."\" txtotal=\"".$prodtxtotal."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$TXXML = $TXXML . "</ROOT>";
	$sql = "exec [Sv_Invoice_App] '".$DCRDetails["doctor_code"]."','".$loginType."','".$DCRDetails["orderId"]."','".$HeadXML."','".$PPXML."','".$TXXML."','".$DCRDetails["Lat"]."','".$DCRDetails["Long"]."','".$DCRHead["UKey"]."','".$orderType."','".$DCRDetails["stockist_code"]."'";
	$result["invqry"]=$sql;
	$res=performQuery($sql);
	$result["data"]=$res;
	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Qty=\"".$OrderDetails[$j]["Product_Total_Qty"]."\" Rate=\"".$OrderDetails[$j]["Rate"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$INVNO=$res[0]["InvNO"];
	//if($DCRDetails["payType"]!="" && $res[0]["InvNO"]!="")
	//{
		if($DCRDetails["PAYAmount"]!="") {
			$sql = "exec saveAppPayment '".$DCRHead["SF"]."','".$loginType."','".$DCRDetails["doctor_code"]."',".$DCRDetails["PAYAmount"].",'".$DCRDetails["payType"]."','".$DCRHead["dcr_activity_date"]."','".$DCRDetails["stockist_code"]."','".$divisionCode."','".$res[0]["InvNO"]."'";
			$result["Qry3"]=$sql;
			$res=performQuery($sql);
		}
	if($orderType!="Van Sales")
	{
		$sql = "exec StockLedgerInv '".$INVNO."','".$DCRDetails["stockist_code"]."','".$DCRDetails["Doc_Meet_Time"]."','".$divisionCode."','".$PPXML."','INV'";
		$res=performQuery($sql);
	}
	//}
	$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where  SF_code='MGR5256'";// sf_code in (select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
	$device = performQuery($sql);
	for($ia=0;$ia<count($device);$ia++){
		$reg_id = $device[$ia]['DeviceRegId'];
		if (!empty($reg_id)) {
			//sendFCM_notify($reg_id, "reloadSale","Daily Inventory",0,'#sign-in');
		}
	}
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function savePaymentEntry(){
    global $data;
    $data = json_decode($_POST['data'], true);
    $result=[];
	$div = $data['divCode'];
	$loginType = $_GET["loginType"];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
    $sql = "exec saveAppPayment '".$data["sfCode"]."','".$loginType."','".$data["retailorCode"]."','".$data["amtReceived"]."','".$data["payMode"]."','".$data["dateOfPay"]."','".$data["distributorCode"]."',$divisionCode,null";
    $res=performQuery($sql);
   // if($res) {
        $result["success"]=true;
		$result["sql"]=$sql;
        $result["Msg"]="Submitted Successfully";
        return $result;        
   /*}
   else{
	$result["success"]=false;
	$result["sql"]=$sql;
	$result["Msg"]="Some Error Occured.";
	return $result;       
   }*/
}
function saveOtherBrandEntry(){
    global $data;
    $data = json_decode($_POST['data'], true);
    $result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$JsonHead=$data[0]["Json_Head"];
	$EntryDetails=$data[0]["Entry_Details"];
	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($EntryDetails); $j++) {
		$imgdata = $EntryDetails[$j]["file_Details"];
		$uniqueDes = join(",",array_map( function( $obj ) {
				return $obj["ob_filename"];
			}, $imgdata ));
		$PPXML = $PPXML . "<Brd BNm=\"".$EntryDetails[$j]["BrandNm"]."\" BSKU=\"".$EntryDetails[$j]["BrandSKU"]."\" Qty=\"".$EntryDetails[$j]["Qty"]."\" Price=\"".$EntryDetails[$j]["Price"]."\"  Amt=\"".$EntryDetails[$j]["Amt"]."\"  Sch=\"".$EntryDetails[$j]["Sch"]."\" Img=\"".$uniqueDes."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
    $sql = "exec svOtherBrandEntry '".$JsonHead["SF"]."','".$JsonHead["CustCode"]."','".$JsonHead["CustName"]."','".$divisionCode."','".$JsonHead["StkCode"]."','".$PPXML."','".$JsonHead["Datetime"]."'";
    $res=performQuery($sql);
   // if($res) {
        $result["success"]=true;
        $result["Msg"]="Submitted Successfully";
        return $result;        
   /*}
   else{
	$result["success"]=false;
	$result["sql"]=$sql;
	$result["Msg"]="Some Error Occured.";
	return $result;       
   }*/
}
function saveQPSEntry(){
    global $data;
    $data = json_decode($_POST['data'], true);
    $result=[];
	$div = $data['divCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
    $sql = "exec saveQPSEntry '".$data["sfCode"]."','".$data["retailorCode"]."','".$data["distributorCode"]."','".$divisionCode."','".$data["bookingDate"]."','".$data["currentTime"]."','".$data["otherBrand"]."','".$data["hapBrand"]."',	'".$data["newOrder"]."','".$data["acheive"]."','".$data["gift"]."','".$data["period"]."','".$data["QPS_Code"]."'";
    $res=performQuery($sql);
   // if($res) {
        $result["success"]=true;
		$result["sql"]=$sql;
        $result["Msg"]="Submitted Successfully";
        return $result;        
   /*}
   else{
	$result["success"]=false;
	$result["sql"]=$sql;
	$result["Msg"]="Some Error Occured.";
	return $result;       
   }*/
}
function approveQPSEntry(){
    global $data;
    $data = json_decode($_POST['data'], true);
    $result=[];
	$qpsdata=$data[0]["QPS_Header"];
	$retailerCode=$qpsdata["retailorCode"];
	$qpsdate=$qpsdata["date"];
	$approvedBy=$qpsdata["sfCode"];
	$qpsImages=$data[0]["file_Details"];
	$qpsRequestNo=$qpsImages[0]["qps_reqNo"];
	$qpsFiles="";
	for($i=0;$i<count($qpsImages);$i++){
		$qpsFiles=$qpsFiles.$qpsImages[$i]["qps_filename"].",";
	}
    $sql = "update Trans_QPS_Entry set QPS_Status='1',Approved_By='$approvedBy',Approved_Date='$qpsdate',Images='$qpsFiles' where Retailer_Code='$retailerCode' and Trans_sl_No=$qpsRequestNo";
    $res=performQuery($sql);
	$result["success"]=true;
	$result["sql"]=$sql;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function savePOPEntry(){
    global $data;
    $data = json_decode($_POST['data'], true);
    $result=[];
	$JsonHead=$data[0]["Json_Head"];
	$div = $JsonHead['divCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$EntryDetails=$data[0]["Entry_Details"];
	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($EntryDetails); $j++) {
		$PPXML = $PPXML . "<Brd PopNm=\"".$EntryDetails[$j]["material"]."\" Popid=\"".$EntryDetails[$j]["id"]."\" Qty=\"".$EntryDetails[$j]["Qty"]."\" Dt=\"".$JsonHead["date"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
    $sql = "exec savePOPEntry '".$JsonHead["SF"]."','".$JsonHead["CustCode"]."','".$JsonHead["CustName"]."','".$divisionCode."','".$JsonHead["StkCode"]."','".$PPXML."','".$JsonHead["Datetime"]."'";
    $res=performQuery($sql);
   // if($res) {
        $result["success"]=true;
        $result["Msg"]="Submitted Successfully";
        return $result;        
   /*}
   else{
	$result["success"]=false;
	$result["sql"]=$sql;
	$result["Msg"]="Some Error Occured.";
	return $result;       
   }*/
}
function SaveCoolerInfo(){
	global $data;
    $data = json_decode($_POST['data'], true);
    $result=[];
	$Cooler_Header=$data[0]["Cooler_Header"];
	$div = $Cooler_Header['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$purityArr=$data[0]["purity_file_Details"];
	$frontageArr=$data[0]["frontage_file_Details"];
	$notworkingArr=$data[0]["notworking_file_Details"];
	$purity="";
	$frontage="";
	$notworking="";
	for ($j = 0; $j < count($purityArr); $j++) {
		$purity=$purity.",".$purityArr[$j]["cooler_filename"];
	}
	for ($j = 0; $j < count($frontageArr); $j++) {
		$frontage=$frontage.",".$frontageArr[$j]["cooler_filename"];
	}
	for ($j = 0; $j < count($notworkingArr); $j++) {
		$notworking=$notworking.",".$notworkingArr[$j]["cooler_filename"];
	}
	$SXML="<ROOT><CINFO Info=\"Purity\" Verified=\"".$Cooler_Header["cbPurity"]."\" Img=\"".$purity."\" /><CINFO Info=\"Frontage\" Verified=\"".$Cooler_Header["cbFrontage"]."\" Img=\"".$frontage."\" /><CINFO Info=\"Not Available\" Verified=\"".$Cooler_Header["cbNotAvailable"]."\" Img=\"\" /><CINFO Info=\"Not Working\" Verified=\"".$Cooler_Header["cbNotWorking"]."\" Img=\"".$notworking."\" /></ROOT>";
	
	$sql="exec savecoolerinfo '$divisionCode','".$Cooler_Header["sfCode"]."','".$Cooler_Header["retailorCode"]."','".$Cooler_Header["distributorcode"]."','".$Cooler_Header["date"]."','".$Cooler_Header["lat"]."','".$Cooler_Header["lng"]."','".$Cooler_Header["remarks"]."','".$Cooler_Header["tagNo"]."','".$Cooler_Header["make"]."','".$Cooler_Header["coolerType"]."','$SXML'";
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="Action Failed";
		$result["sql"]=$sql;
		outputJSON($result);
		die;
	}
	if(count($res)>0)
	{
		$result["success"]=true;
		$result["Msg"]="Submitted Successfully";
	}
	else
	{
		$result["success"]=false;
		$result["Msg"]="Action Failed";
	}
	return $result;
}
function getFoodExp(){
		global $data;
		$data = json_decode($_POST['data'], true);
		$SF=$data['SF'];
		$sql="select SF_Emp_id from Mas_Salesforce where sf_Code='".$SF."'";
		$res=performQuery($sql);
		
		$host = 'https://ccms.hap.in/getEmployeeCateenReport';
		$user_name = "CCMS_CANTEEN";
		$password = "orD@#r2o2!";
		$ch = curl_init($host);
		$headers = array(
		'Content-Type: application/json',
		'Authorization: Basic '. base64_encode("$user_name:$password")
		);
		// Setup request to send json via POST
		/*$data = array(
			'empId' => $res[0]["SF_Emp_id"],
			'year' => date('Y'),
			'month' => date('m')
		);*/
		$data = array(
			'empId' => $res[0]["SF_Emp_id"],
			'fromDate' => $data['fdt'],
			'toDate' => $data['tdt']
		);
		$payload = json_encode($data);
		// Set the content type to application/json
		curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
		$certificate_location = "cacert.pem";
		curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, $certificate_location);
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, $certificate_location);
		// Attach encoded JSON string to the POST fields
		curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
		// Return response instead of outputting
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		// Execute the POST request
		$result = curl_exec($ch);
		
		// Get the POST request header status
		$status = curl_getinfo($ch, CURLINFO_HTTP_CODE);
		
		if ( $status == 201 || $status == 200 ) {
			
		}else {
		   die("Error: call to URL $url failed with status $status, response $result, curl_error " . curl_error($ch) . ", curl_errno " . curl_errno($ch));
		}
		
		echo $result;
		// Close cURL resource
		curl_close($ch);
}

function getAdvanceType(){
	$res=[];
	$item=[];
	$item["id"]="1";
	$item["name"]="Travel Advance";
	$res[]=$item;
	$item=[];
	$item["id"]="2";
	$item["name"]="Meeting Advance";
	$res[]=$item;
	$item=[];
	$item["id"]="3";
	$item["name"]="Others Advance";
	$res[]=$item;
	return $res;
}
function saveAdvDetails(){
	global $data;
	$data = json_decode($_POST['data'], true);
	
	$qry="insert into Trans_AdvanceRequest(eKey,SF,eDate,eTime,AdvFrDt,AdvToDt,AdvTyp,AdvLoc,AdvPurp,AdvAmt,AdvSettle,flag,ServTime) select '".$data['eKey']."','".$data['SF']."','".date("Y-m-d 00:00:00",strtotime($data['eDate']))."','". $data['eDate'] ."','".$data['AdvFrom']."','".$data['AdvTo']."','".$data['AdvTyp']."','".$data['AdvLoc']."','".$data['AdvPurp']."','".$data['AdvAmt']."','".$data['AdvSettle']."',0,getdate()";
    performQuery($qry);	
	$res=[];
	$result["success"]=true;
	$result["Qry"]=$qry;
	$result["Msg"]="Submitted Successfully...";
	$res[]=$result;
	return $res; 
} 
function approveAdvance(){
	
	global $data;
	$data = json_decode($_POST['data'], true);
	$qry="select SF_Name from Mas_Salesforce where SF_Code='".$data["rSF"]."'";
	$SFdet=performQuery($qry);
	
	$qry="update Trans_AdvanceRequest set flag='".$data["flag"]."',ApprDt=getDate(),ApprBy='".$data["rSF"]."',ApprByNm='".$SFdet[0]["SF_Name"]."',reason='".$data["reason"]."' where eKey='".$data['eid']."'";
    performQuery($qry);	
	$res=[];
	$result["success"]=true;
	$result["Qry"]=$qry;
	$result["Msg"]="Submitted Successfully...";
	$res[]=$result;
	return $res; 
}
function getVanStock(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$Stk=$data['Stk'];
	
	$qry="exec GetVanStock '$Stk','".date("Y-m-d 00:00:00")."'";
	$res = performQuery($qry);	
	if(count($res)>0){
		$result["success"]=true;
		$result["Data"]=$res;
	}
	else{
		$result["success"]=false;
		$result["Data"]=[];
		$result["Msg"]="Stock Not Available";
	}
	return $result;
}
function getDistStockLedger(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$Stk=$data['Stk'];
	
	$qry="exec getDistStockLedger '$Stk','2021-04-01','".date("Y-m-d 00:00:00")."'";
	$res = performQuery($qry);	
	if(count($res)>0){
		$result["success"]=true;
		$result["Data"]=$res;
	}
	else{
		$result["success"]=false;
		$result["Data"]=[];
		$result["Msg"]="Stock Not Available";
	}
	return $result;
}
function getOutletOutstanding(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data['SF'];
	$Stk=$data['Stk'];
	$Cus=$data['Cus'];
	if($Cus<>'' && $Cus<>'OutletCode'){
		$qry="exec GetCustPayLedger '$Stk','".$Cus."','".date("Y-m-01 00:00:00")."','".date("Y-m-d 00:00:00")."'";
		$res = performQuery($qry);
	}
	else{
		$res=array();
	}
	return $res;
}
function getOutletwiseLedger(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data['SF'];
	$Stk=$data['Stk'];
	$FDT=$data['FDT'];
	$TDT=$data['TDT'];
	$result=[];
	$qry="exec GetCustomerwisePayLedger '$Stk','".$FDT."','".$TDT."'";
	$res = performQuery($qry);	
	$custCode="";
	for($il=0;$il<count($res);$il++){
		if($custCode!=$res[$il]["Cust_Code"]){
			$item=[];
			$custCode=$res[$il]["Cust_Code"];
			$item["CustName"]=$res[$il]["CustName"];
			$OBAmt=0;$ClAmt=0;
			$OPArr = array_values(array_filter($res, function($obj)  use ($custCode,$FDT){
						if(($obj["Cust_Code"] === $custCode) && (strtotime($obj["TransDate"])<strtotime($FDT))) return true;
					}));
			if(count($OPArr)>0){
				$OBAmt=$OPArr[count($OPArr)-1]["Balance"];
			}
			$item["OBAmt"]=$OBAmt;
			
			
			$ldgArr = array_values(array_filter($res, function($obj)  use ($custCode,$FDT,$TDT){
						if(($obj["Cust_Code"] === $custCode) && (strtotime($obj["TransDate"])>=strtotime($FDT)) && (strtotime($obj["TransDate"])<=strtotime($TDT))) return true;
					}));
			if(count($ldgArr)>0){
				$ClAmt=$ldgArr[count($ldgArr)-1]["Balance"];
			}else{
				$ClAmt=$OBAmt;
			}
			$item["ClAmt"]=$ClAmt;
			$item["Details"]=$ldgArr;
			array_push($result,$item);
		}
	}
	return $result;
}

function getFencedOutlet(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data['SF'];
	$Div=$data['Div'];
	$lat=$data['Lat'];
	$lng=$data['Lng'];
	
	$qry="exec getRetailByFencing '$SF','".$Div."','$lat','$lng'";
	$res = performQuery($qry);
	for($il=0;$il<count($res);$il++){
		$qry="exec getCatwsTdyInvRetail '".$res[$il]["Code"]."'";
		$restdy = performQuery($qry);
		$res[$il]['todaydata']=$restdy;
		$qry="exec getCatwsMnthInvRetail '".$res[$il]["Code"]."'";
		$restMn = performQuery($qry);
		$res[$il]['monthdata']=$restMn;
		
	}
	return $res;
}

//Expense Section
function getExpensedatenew($Stat){
	global $data;
	$data = json_decode($_POST['data'], true);
	$div =(string) $data['divisionCode'];
	$divs = explode(",", $div . ",");
	$SF_code = (string) $data['sfCode'];
	$Owndiv = (string) $divs[0];
	if($Stat==0){
		$query = "select convert(varchar, Date_Time, 23) id,convert(varchar, Date_Time, 23)+'  '+FORMAT(convert(datetime,Date_Time), 'dddd') Datewithname,case when MOT_Name='Two Wheeler' then 300 else 800 end Maxkm from Expense_Start_Activity WITH (NOLOCK) where   Sf_code='".$SF_code."'   and isnull(Approval_Flag,0)=1";
		return performQuery($query);
	}else{
		$Selectdate = (string) $data['Selectdate'];
		
		$query = "exec GetDailyAllowanceDetails '".$SF_code."','".$Selectdate."'";
	 	$TSD = performQuery($query);
		$expense['TodayStart_Details']=$TSD;	
		$query = "exec GetDailyFuelAllowanceDetails '".$SF_code."','".$Selectdate."'";
		$FTSD = performQuery($query);
		$expense['FuelAllowance']=$FTSD;
		
		//$Selectdate
		$res=[];
		array_push($res,array("TRFlag"=>0,"NgtOnlyFlag"=>0,"TWMax_Km"=>300,"FWMax_Km"=>1000));
		
		$expense['Settings']=$res;
		//$query="select convert(varchar,isnull((select top 1 convert(varchar,isnull(Stay_Date,getdate()),23) from Trans_Lodging_Details where Sf_Code='".$SF_code."' and convert(date,Stay_Date)=DATEADD(day,-1,cast('".$Selectdate."' as date)) and Continuous_Stay=1),cast('".$Selectdate."' as date)),23) Stay_Date_time";
		$query="select CONVERT(varchar,isnull(st,'".$Selectdate."'),23) Stay_Date_time,CONVERT(varchar,isnull(st,'".$Selectdate."'),103) CInDate
		,CONVERT(varchar,COutDt,103) uCOutDate,
		convert(varchar,COutDt,23) COutDt,left(COutTm,5) COutTm,
		left(CONVERT(varchar,isnull(st,'".$Selectdate."'),114),5) CInTime,isnull(Continuous_Stay,0) ContStay,Add_SF_Emp_ID LocId,
		Ldg_Stay_Loc StayLoc from (select top 1 case when Continuous_Stay=1 then Stay_Date else '".$Selectdate."' end st,
		Continuous_Stay,Add_SF_Emp_ID,Ldg_Stay_Loc,CONVERT(varchar, To_Date, 8) COutTm,
		lat_check_out COutDt from Trans_Lodging_Details WITH (NOLOCK) where Sf_Code='".$SF_code."' 
		and (cast(convert(varchar,To_Date,101) as datetime)=dateadd(day,-1,'".$Selectdate."') 
		or cast(convert(varchar,To_Date,101) as datetime)='".$Selectdate."') and Continuous_Stay=1 order by To_Date) t";
		$STDT = performQuery($query);
		$expense['Stay_Qry']=$query;
		$expense['Stay_Date_time']=$STDT;	
		$queryn = "exec getLodingContinuous '".$SF_code."','".$Selectdate."'";
		$expense['Stay_Q']=$queryn;
		$expense['LodDtlist'] = performQuery($queryn);
		$ExpH= "select case when DA_Mode='HQ' then HQNAME else  To_Locc end To_Loc,*   from (select     Sl_No,th.Sf_Code,convert(varchar,Expense_Date,23)Expense_Date,Flag,isnull(Reason,'')Reason,isnull(th.Approved_By,'')Approved_By,isnull(convert(varchar,Approved_Date,23),'')Approved_Date,isnull(Total_Amount,0)Total_Amount,isnull(DA_Mode,'')DA_Mode,isnull(Alw_Type,'')Alw_Type,isnull(From_Loc,'')From_Loc,isnull(To_Loc,'')To_Locc,isnull(Alw_Amount,0)Alw_Amount,isnull(Boarding_Amt,0)Boarding_Amt,isnull( Lc_totalAmt,0)Lc_totalAmt,isnull(Oe_totalAmt,0)Oe_totalAmt,isnull(Ldg_totalAmt,0)Ldg_totalAmt,isnull(Ta_totalAmt,0)Ta_totalAmt,isnull(trv_lc_amt,0)trv_lc_amt,(select HQ_Name from Mas_HQuarters  where HQ_ID=ms.Sf_HQ)HQNAME from Trans_Expense_Head2 th WITH (NOLOCK)  inner join Mas_Salesforce ms WITH (NOLOCK) on th.Sf_Code=ms.Sf_Code  where  th.sf_code='".$SF_code."' and convert(date,Expense_Date)='".$Selectdate."') A";
		$Expense_Head = performQuery($ExpH);
		if(count($Expense_Head)<1){
			$expense['Expense_Head']=[];	
			$expense['Additional_ExpenseLC']=[];
			$expense['Lodging_Head']=[];
			$expense['Additional_ExpenseOE']=[];
			$expense['Travelled_Details']=[];
			$expense['Travelled_Loc']=[];
			$expense['Travelled_Plcs']=[];
			$expense['Da_Claim']=[];
		}
		else{
			
			$expense['Expense_Head']=$Expense_Head;	
//			$TAAddsql = "select   SlNo,Exp_Date,Exp_Code,Exp_Amt,isnull(Attachments,0)Attachments,Fk_Sl_No,Sf_Code,Expense_Type,isnull(Ukey,'')Ukey  from Trans_Additional_Expense where Fk_Sl_No='".$Expense_Head[0]["Sl_No"]."' and Expense_Type='LC'   ";
			$TAAddsql = "select   SlNo,Exp_Date,Exp_Code,Exp_Amt,isnull(Attachments,0)Attachments,Fk_Sl_No,Sf_Code,Expense_Type,iif((select count(imgurl) from Activity_Event_Captures where lat=isnull(Ukey,'') and SF_Code=T.SF_Code and Insert_Date_Time=convert(varchar,Exp_Date,23))>0,isnull(Ukey,''),'') Ukey,250 Max_Allowance  from Trans_Additional_Expense T where Fk_Sl_No='".$Expense_Head[0]["Sl_No"]."' and Expense_Type='LC'   ";
			$TAAditional_Exp =performQuery($TAAddsql);
			if(count($TAAditional_Exp)<1){
				$expense['Additional_ExpenseLC']=[];
			}else{
				$expense['Additional_ExpenseLC']=$TAAditional_Exp;
				for ($k = 0; $k < count($expense['Additional_ExpenseLC']); $k++){
					$TARefsql = "select  * from Trans_Ref_Details WITH (NOLOCK) where SlNo='".$expense['Additional_ExpenseLC'][$k]['SlNo']."'";
					$TSDPerfomance = performQuery($TARefsql);
					if(count($TSDPerfomance)<1){
						$expense['Additional_ExpenseLC'][$k]['Additional']=[];
					}else{
						$expense['Additional_ExpenseLC'][$k]['Additional']=$TSDPerfomance;
					}
				}
			}
		
			//$Lod = "select  SlNo,FK_SlNo,Lodging_Type,Eligible,Bill_Amt,WOB_Amt, isnull(Ukey,'')Ukey, Add_SF_Emp_ID,Ldg_Stay_Loc,CONVERT(varchar, Stay_Date, 8) Stay_Date,CONVERT(varchar, Stay_Date, 103)+' '+ CONVERT(varchar, Stay_Date, 108),Driver_Ldg_Amount,Joining_Ldg_Amount,Total_Ldg_Amount,Attachment,Sf_Code,CONVERT(varchar, To_Date, 8) To_Date ,NO_Of_Days ,convert(varchar, Stay_Date, 23) Tadate,isnull(Continuous_Stay,0)Continuous_Stay,isnull(Early_Checkin,0)Early_Checkin,isnull(Late_Checkout,0)Late_Checkout,convert(varchar,Erly_Check_in,8)Erly_Check_in,convert(varchar,Erly_Check_out,8)Erly_Check_out,isnull(Ear_bill_amt,0)Ear_bill_amt,convert(varchar,lat_chec_in,8)lat_chec_in,convert(varchar,lat_check_out,8)lat_check_out,isnull(lat_bill_amt,0)lat_bill_amt from Trans_Lodging_Details where FK_SlNo='".$Expense_Head[0]["Sl_No"]."' and Sf_Code='".$SF_code."' and Lodging_Type<>''  ";
			$Lod="select  SlNo,FK_SlNo,Lodging_Type,Eligible,Bill_Amt,WOB_Amt, 
			iif((select count(imgurl) from Activity_Event_Captures where lat=isnull(Ukey,'') and SF_Code=L.SF_Code and Insert_Date_Time=convert(varchar,To_Date,23))>0,isnull(Ukey,''),'')
			Ukey, Add_SF_Emp_ID LocId,Ldg_Stay_Loc,CONVERT(varchar, Stay_Date, 8) Stay_Date,Driver_Ldg_Amount,Joining_Ldg_Amount,Total_Ldg_Amount,Attachment,Sf_Code,CONVERT(varchar, To_Date, 8) To_Date ,NO_Of_Days ,convert(varchar, Stay_Date, 23) Tadate,convert(varchar,lat_check_out,23) ActChkOutDate,isnull(Continuous_Stay,0)Continuous_Stay,isnull(Early_Checkin,0)Early_Checkin,isnull(Late_Checkout,0)Late_Checkout,convert(varchar,Erly_Check_in,8)Erly_Check_in,convert(varchar,Erly_Check_out,8)Erly_Check_out,
		isnull(Ear_bill_amt,0)Ear_bill_amt,convert(varchar,lat_chec_in,8)lat_chec_in,convert(varchar,lat_check_out,8)lat_check_out,isnull(lat_bill_amt,0)lat_bill_amt from Trans_Lodging_Details L WITH (NOLOCK) where FK_SlNo='".$Expense_Head[0]["Sl_No"]."' and Sf_Code='".$SF_code."' and Lodging_Type<>'' ";
			$Lod_Head =performQuery($Lod);
			if(count($Lod_Head)<1){
				$expense['Lodging_Head']=[];
			}else{
				$expense['Lodging_Head']=$Lod_Head;
				for ($k = 0; $k < count($expense['Lodging_Head']); $k++){
					$Lodad = "select  * from Trans_Lod_Join_Details WITH (NOLOCK) where Fl_Ldg_Sl_No='".$expense['Lodging_Head'][$k]['SlNo']."'";
					$Lodad = performQuery($Lodad);
					if(count($Lodad)<1){
					$expense['Lodging_Head'][$k]['Additional']=[];
					}else{
						$expense['Lodging_Head'][$k]['Additional']=$Lodad;
					}
				}
			}
			$TAAddsql = "select  SlNo,Exp_Date,Exp_Code,Exp_Amt,isnull(Attachments,0)Attachments,Fk_Sl_No,Sf_Code,Expense_Type,iif((select count(imgurl) from Activity_Event_Captures where lat=isnull(Ukey,'') and SF_Code=T.SF_Code and Insert_Date_Time=convert(varchar,Exp_Date,23))>0,isnull(Ukey,''),'') Ukey,250 Max_Allowance from Trans_Additional_Expense T WITH (NOLOCK) where Fk_Sl_No='".$Expense_Head[0]["Sl_No"]."' and Expense_Type='OE'   ";
			$TAAditional_ExpOE =performQuery($TAAddsql);
			if(count($TAAditional_ExpOE)<1){
				$expense['Additional_ExpenseOE']=[];
			}else{
				$expense['Additional_ExpenseOE']=$TAAditional_ExpOE;
				for ($k = 0; $k < count($expense['Additional_ExpenseOE']); $k++){
					$TARefsql = "select  * from Trans_Ref_Details WITH (NOLOCK) where SlNo='".$expense['Additional_ExpenseOE'][$k]['SlNo']."'";
					$TSDPerfomance = performQuery($TARefsql);
					if(count($TSDPerfomance)<1){
						$expense['Additional_ExpenseOE'][$k]['Additional']=[];
					}else{
						$expense['Additional_ExpenseOE'][$k]['Additional']=$TSDPerfomance;
					}
				}
			}
			
			$TAtravelsql = "select  * from Trans_Travelled_Details WITH (NOLOCK) where FK_Exp_SlNo='".$Expense_Head[0]["Sl_No"]."' and Sf_Code='".$SF_code."'";
			$Travelled =performQuery($TAtravelsql);
			if(count($Travelled)<1){
				$expense['Travelled_Details']=[];
			}else{
				$expense['Travelled_Details']=$Travelled;
			}
			
			$TAtravelLocsql = "select   SlNo,FK_Trv_SlNo,From_P,To_P,isnull(Attachments,0)Attachments,Sf_Code,Mode,Fare  , isnull(Ukey,'') Ukey from Trans_Travelled_Loc WITH (NOLOCK) where FK_Trv_SlNo='".$Expense_Head[0]["Sl_No"]."' and iif(Mode='','0',Mode)='0' and Sf_Code='".$SF_code."'";
			$Travelled_Loc =performQuery($TAtravelLocsql);
			$expense['Travelled_Plcs']=$Travelled_Loc;
			
			$TAtravelLocsql = "select SlNo,FK_Trv_SlNo,From_P,To_P,ISNULL(NeedAttachment,'0') Attachments,Sf_Code,Mode,Fare,isnull(Ukey,'') Ukey,50000 Max_Allowance,ISNULL(Alw_Eligibilty,'0')Alw_Eligibilty from Trans_Travelled_Loc lc WITH (NOLOCK) left join Mas_Modeof_Travel mt WITH (NOLOCK)  on mt.MOT=lc.Mode and mt.Active_Flag=0 and mt.Division_Code=3 where FK_Trv_SlNo='".$Expense_Head[0]["Sl_No"]."' and iif(Mode='','0',Mode)<>'0' and Sf_Code='".$SF_code."'";
			$Travelled_Loc =performQuery($TAtravelLocsql);
			$expense['Travelled_Loc']=$Travelled_Loc;
		
			$TAclaimsql = "select  * from Da_Claim WITH (NOLOCK) where FK_Trv_SlNo='".$Expense_Head[0]["Sl_No"]."'";
			$TAclaimRes =performQuery($TAclaimsql);
			if(count($TAclaimRes)<1){
				$expense['Da_Claim']=[];
			}else{
				$expense['Da_Claim']=$TAclaimRes;
			}	
		}
		return $expense;
	}	   
} 
function SaveExceptionEntry(){
	global $data;
    $sfCode = $_GET['sfCode'];
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $data = json_decode($_POST['data'], true);
    $vals = $data[0]["DeviationEntry"];
	
	$name = $_GET['sf_name'];
	$sql = "SELECT isNull(max(Deviation_Id),0)+1 as RwID FROM Mas_Deviation_Form";
	$tRw = performQuery($sql);
	$pk = (int) $tRw[0]['RwID'];
	$dt=date("Y-m-d h:i:sa");
	$sql = "exec getDeviationDates '$sfCode','" . $vals['From_Date'] . "','" . $vals['Deviation_Type'] . "'";
	$Previous = performQuery($sql);
	
	if($Previous[0]["Flag"]=='0'){
	$sqld = "insert into Mas_Deviation_Form(Deviation_Id,Sf_Code,Sf_Name,Deviation_Type,Deviation_Date,MobileDate,Server_Date,LatLng,Reason,Division_Code,Devi_Active_Flag)values('$pk','$sfCode','".$name."','" . $vals['Deviation_Type'] . "','" . $vals['From_Date'] . "'," . $vals['Time'] . ",'".$dt."',".$vals['LatLng'].",'" . str_replace("'","",$vals['reason']) . "', '$Owndiv','2')";
	performQuery($sqld);
		$MSG="Your Exception Entry Submitted Successfully";
	}else{
		$MSG="Your Exception Entry Not Submitted.Try Later.";
		
	}
	$resp["Msg"] = $MSG;
	$resp["success"] = true;
	return $resp;
}

function saveAllowance($Stat){
	global $data;
	$data = json_decode($_POST['data'], true);
	$div=(string) $data['div'];
	$Datetime=date('Y-m-d H:i:s');
	$Dateonly=date('Y-m-d');
	$divs = explode(",", $div . ",");
	$ActDate=(string) $data['Activity_Date'];
	$Owndiv = (string) $divs[0];
	$divisionCode = explode(",", $divCode);
	$sfCode=(string) $data['sf'];
	$km=(string) $data['km'];
	$pkm=(string) $data['pkm'];
	$url=(string) $data['url'];
	$rmk=(string) $data['rmk'];
	$mod=(string) $data['mod'];
	$from=(string) $data['from'];
	$to=(string) $data['to'];
	$fare=(string) $data['fare'];
	$StEndNeed=(string) $data['StEndNeed'];
	$dailyAllowance=(string) $data['dailyAllowance'];
	$driverAllowance=(string) $data['driverAllowance'];
	$MOT_Name=(string) $data['mode_name'];
	$to_code=(string) $data['to_code'];
	$Ukey=(string) $data['UKey'];
	$sql="";
	if($Stat==0){
		$sql = "SELECT max(Sl_No)+1 as Sl_No FROM Expense_Start_Activity";
		$topr = performQuery($sql);
		$pk =  (int)$topr[0]['Sl_No'];
		$sqldub ="select Sf_code from Expense_Start_Activity WITH (NOLOCK) where Sf_code='".$sfCode."' and Date_Time='".$Datetime."' and UKey='".$Ukey."'";
		$resAlready=performQuery($sqldub);
		if(count($resAlready)>0 && $Ukey!=''){
			$resp["success"] = true;
			$resp["Query"] =$sql;
			$sql = "insert into Exp_Start_backlog(Sf_code,Division_Code,Date_Time,Start_Km,Image_Url,Remarks,MOT,Approval_Flag,Sl_No,From_Place,To_Place,Fare,StEndNeed,dailyAllowance,driverAllowance,MOT_Name,To_Place_Id,Identification,Ukey) values( '".$sfCode."','".$Owndiv."','".$Datetime."','".$km."','".$url."' ,'".$rmk."','".$mod."','1','0','".$from."','".$to."','".$fare."','".$StEndNeed."','".$dailyAllowance."','".$driverAllowance."' ,'".$MOT_Name."','".$to_code."',0,'".$Ukey."')";
			performQuery($sql);
			return $resp;
			die;
			/*$sql="delete from Expense_Start_Activity where  Sf_code='".$sfCode."' and convert(date,Date_Time)='".$Datetime ."'";
			performQuery($sql);*/
		}
		
		$sql = "insert into Expense_Start_Activity(Sf_code,Division_Code,Date_Time,Start_Km,Image_Url,Remarks,MOT,Approval_Flag,Sl_No,From_Place,To_Place,Fare,StEndNeed,dailyAllowance,driverAllowance,MOT_Name,To_Place_Id,Identification,Ukey,ExpenseDate) values( '".$sfCode."','".$Owndiv."','".$Datetime."','".$km."','".$url."' ,'".$rmk."','".$mod."','1','" . $pk . "','".$from."','".$to."','".$fare."','".$StEndNeed."','".$dailyAllowance."','".$driverAllowance."' ,'".$MOT_Name."','".$to_code."',0,'".$Ukey."',cast('".$Datetime."' as date))";
	}
	else if($Stat==3){
		$sqldub ="select Sf_code from Expense_Start_Activity WITH (NOLOCK) where Sf_code='".$sfCode."' and Date_Time='".$Datetime."' and UKey='".$Ukey."'";
		$resAlready=performQuery($sqldub);
		if(count($resAlready)>0 && $Ukey!=''){
			$resp["success"] = true;
			$resp["Query"] =$sql;
			$sql = "insert into Exp_Start_backlog(Sf_code,Division_Code,Date_Time,Start_Km,Image_Url,Remarks,MOT,Approval_Flag,Sl_No,From_Place,To_Place,Fare,StEndNeed,dailyAllowance,driverAllowance,MOT_Name,To_Place_Id,Identification,Ukey) values( '".$sfCode."','".$Owndiv."','".$Datetime."','".$km."','".$url."' ,'".$rmk."','".$mod."','1','0','".$from."','".$to."','".$fare."','".$StEndNeed."','".$dailyAllowance."','".$driverAllowance."' ,'".$MOT_Name."','".$to_code."',1,'".$Ukey."')";
			performQuery($sql);
			return $resp;
			die;
			/*$sql="delete from Expense_Start_Activity where  Sf_code='".$sfCode."' and convert(date,Date_Time)='".$Datetime ."'";
			performQuery($sql);*/
		}
		$sql = "SELECT max(Sl_No)+1 as Sl_No FROM Expense_Start_Activity";
		$topr = performQuery($sql);
		$pk =  (int)$topr[0]['Sl_No'];
		$sql = "insert into Expense_Start_Activity(Sf_code,Division_Code,Date_Time,Start_Km,Image_Url,Remarks,MOT,Approval_Flag,Sl_No,From_Place,To_Place,Fare,StEndNeed,dailyAllowance,driverAllowance,MOT_Name,To_Place_Id,Identification,Ukey,ExpenseDate) values( '".$sfCode."','".$Owndiv."','".$Datetime."','".$km."','".$url."' ,'".$rmk."','".$mod."','1','" . $pk . "','".$from."','".$to."','".$fare."','".$StEndNeed."','".$dailyAllowance."','".$driverAllowance."' ,'".$MOT_Name."','".$to_code."',1,'".$Ukey."',cast('".$Datetime."' as date))";
	}
	else if($Stat==2){
		$sql = "update Expense_Start_Activity set Enddateand_time='".$Datetime."',End_Km='".$km."',Personal_Km='".$pkm."',End_Image_Url='".$url."',End_MOT='".$mod."',End_remarks='".str_replace("'","''",$rmk)."',To_Place='".$to."',To_Place_Id='".$to_code."' where sf_Code='".$sfCode."' and convert(date,Date_Time)=convert(date,'".$ActDate."') and End_Km is null";
	}
	else{
		$sql = "update Expense_Start_Activity set Enddateand_time='".$Datetime."',End_Km='".$km."',Personal_Km='".$pkm."',End_Image_Url='".$url."',End_MOT='".$mod."',End_remarks='".str_replace("'","''",$rmk)."',To_Place='".$to."',To_Place_Id='".$to_code."' where sf_Code='".$sfCode."' and convert(date,Date_Time)='".$Dateonly."' and End_Km is null";
	}
	performQuery($sql);
	$resp["success"] = true;
	$resp["Query"] =$sql;
	return $resp;
}
function getStayAllwDet(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$sfCode=(string) $data["sfCode"];
	$HQID=(string) $data["HQID"];
	$ExpDt=(string) $data["ExpDt"];
	$sql="exec getStayAllowDets '".$sfCode."','".$HQID."','".$ExpDt."'";
	$res =  performQuery($sql);
	if(count($res)<1)
	{
		$res=[];
	}
	return $res;
}

function getUpdateDA(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=(string) $data["SF"];
	$DALocId=(string) $data["DALocId"];
	$DALoc=(string) $data["DALoc"];
	$ExpDt=(string) $data["ExpDt"];
	$DATyp=(string) $data["DAType"];
	$sql="exec UpdateDAAlw '".$SF."','".$DALocId."','".$DALoc."','".$ExpDt."','".$DATyp."'";
	performQuery($sql);
	$sql="exec [GetDailyAllowOnly] '".$SF."','".$ExpDt."'";
	return performQuery($sql);
}
function getEmpDetByID(){
	$SF=$_GET['sfCode'];
	$Empno=$_GET['rSF'];
	$dt=$_GET['desig'];
	$sLoc=$_GET['State_Code'];
	
	$sql = "exec getEmpDetByID '" . $Empno . "','$dt','".$sLoc."'";//,'".$SF."'";
	$res=performQuery($sql);
	/*if(count($res)>0){
		$sql = "select Order_By,Grade_Name from Mas_SF_Designation d WITH (NOLOCK) inner join Mas_Grade WITH (NOLOCK) on Grade_ID=Group_Name where Designation_Code='" . $res[0]["DesigCd"] . "'";
		$arr=performQuery($sql);
		$sql = "select Order_By,Grade_Name from Mas_Salesforce s WITH (NOLOCK) inner join  Mas_SF_Designation d WITH (NOLOCK) on s.Designation_Code=d.Designation_Code inner join Mas_Grade on Grade_ID=Group_Name where Sf_Code='".$SF."'";
		$arr1=performQuery($sql);
		if($arr[0]["Order_By"]<$arr1[0]["Order_By"]){
			$res=[];
			array_push($res,array("Msg"=>"Can't apply for this Grade Level","apply"=>$arr1[0]["Order_By"],"Joint"=>$arr[0]["Order_By"]));
		}
	}*/
	return $res;
}

function saveApprove(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$sfCode=(string) $data["sfCode"];
	$Sl_No =(string) $data['Sl_No'];
	$AAmount = (string) $data['AAmount'];
	$Flag = (string) $data['Flag'];
	$Reason = (string) $data['Reason'];
	
	$sql = "select SF_Code,convert(varchar,Expense_Date,23) Expense_Date from Trans_Expense_Head2 WITH (NOLOCK) where Sl_No='".$Sl_No."'";
	$res=performQuery($sql);
	$SFCd=$res[0]["SF_Code"];
	$ExpDate=$res[0]["Expense_Date"];
	if($Flag=="1"){
		$sql = "update Expense_Start_Activity set Approval_Flag=3,Approval_Amount='" . $AAmount . "',Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $sfCode . "' where SF_Code='".$SFCd."' and  cast(convert(varchar,Date_Time,101) as datetime)='".$ExpDate."'";
		performQuery($sql);
		$query = "update Trans_Expense_Head2 set Flag='3' where sf_code='".$SFCd."' and Expense_Date='".$ExpDate."'";
		performQuery($query);
	}else{
		$sql = "update Expense_Start_Activity set Approval_Flag=4,Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $sfCode . "',Reject_reason='" . $Reason . "' where SF_Code='".$SFCd."' and  cast(convert(varchar,Date_Time,101) as datetime)='".$ExpDate."'";   
		performQuery($sql);
		$query = "update Trans_Expense_Head2 set Flag='4'  where sf_code='".$SFCd."' and Expense_Date='".$ExpDate."'";
		performQuery($query);
	}
	$res["STQRY"]=$sql;
	$res["EXPHD"]=$query;
	$res["success"]="true";
	
	return $res;
}
function getcategorywiseretailerdata(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$distributorCode =(string) $data['distributorCode'];
	$divisionCode = (string) $data['divisionCode'];
	
	$qry="exec getProductGroupList '$distributorCode'";
	$resg = performQuery($qry);
	$qry="exec getcategorywiseretailerdata '$distributorCode','$divisionCode'";
	$res = performQuery($qry);
	$qry="exec getTodaycategorywiseretailerdata '$distributorCode','$divisionCode'";
	$resdy = performQuery($qry);
	$resA = [];//array('Cust_Code' => '','Mnth' => '','Milk' => '','Curd' => '','Others' => '','MilkVal' => '','CurdVal' => '','OthersVal' => '');
	$restdy = [];
	$existcust ="";
	$i=0;

	for($j = 0; $j < count($res); $j++){
		$pid =(string) $res[$j]['Cust_Code'];
		$mnth =(string) $res[$j]['Mnth'];
		
		if(stripos($existcust,$pid.",".$mnth.";")>-1){}
		else{
			$new_array = array();
			$new_array['Cust_Code']=$res[$j]['Cust_Code'];
			$new_array['Mnth']=$res[$j]['Mnth'];
			$existcust = $existcust.$pid.",".$mnth.";";
			$new_array['Items']=[];
			$itmP=array();
			for($k=0;$k<count($resg);$k++){
				$itm=array();
				$itm["name"]=$resg[$k]['name'];
				$itm["Vals"]=[];
				$pNm=$resg[$k]['name'];
				$filtarray = array_values(array_filter($res, function($obj)  use ($pid,$mnth,$pNm){
							if(($obj["Cust_Code"] === $pid) && ($obj["Mnth"] === $mnth) && ($obj["Product_Cat_Name"] ===$pNm)) return true;
						}));
						
				for($ij=0;$ij<count($filtarray);$ij++)
				{
					$itmval=array();
					$itmval["Qty"]=$filtarray[$ij]['Ltrs'];
					$itmval["Val"]=$filtarray[$ij]['oval'];
					array_push($itm["Vals"],$itmval);
				}
				array_push($new_array['Items'],$itm);
			}
			$i++;
			array_push($resA,$new_array);
		}
	}
	$i=0;
	$existcust="";
	for($j = 0; $j < count($resdy); $j++){
		$pid =(string) $resdy[$j]['Cust_Code'];
		if(stripos($existcust,$pid.";")>-1){}
		else{
			$new_array = array();
			$new_array['Cust_Code']=$resdy[$j]['Cust_Code'];
			$existcust = $existcust.$pid.";";
			$new_array['Items']=[];
			for($k=0;$k<count($resg);$k++){
				$itm=array();
				$itm["name"]=$resg[$k]['name'];
				$itm["Vals"]=[];
				$pNm=$resg[$k]['name'];
				$filtarray = array_values(array_filter($resdy, function($obj)  use ($pid,$pNm){
							if(($obj["Cust_Code"] === $pid) && ($obj["Product_Cat_Name"] ===$pNm)) return true;
						}));
						
				for($ij=0;$ij<count($filtarray);$ij++)
				{
					$itmval=array();
					$itmval["OQty"]=$filtarray[$ij]['OQty'];
					$itmval["NQty"]=$filtarray[$ij]['NQty'];
					$itmval["Qty"]=$filtarray[$ij]['Ltrs'];
					$itmval["Val"]=$filtarray[$ij]['oval'];
					array_push($itm["Vals"],$itmval);
				}
				array_push($new_array['Items'],$itm);
			}
			$i++;
			array_push($restdy,$new_array);
		}
	}
	$result['success']=true;
	$result['data']=$resA;
	$result['todaydata']=$restdy;
	$result['Group']=$resg;
	return $result;
}
//End Expense Section
function getcategorywiseretailerdata1(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$distributorCode =(string) $data['distributorCode'];
	$divisionCode = (string) $data['divisionCode'];
	
	$qry="exec getProductGroupList '$distributorCode'";
	$resg = performQuery($qry);
	$qry="exec getcategorywiseretailerdata '$distributorCode','$divisionCode'";
	$res = performQuery($qry);
	$qry="exec getTodaycategorywiseretailerdata '$distributorCode','$divisionCode'";
	$resdy = performQuery($qry);
	$resA = [];//array('Cust_Code' => '','Mnth' => '','Milk' => '','Curd' => '','Others' => '','MilkVal' => '','CurdVal' => '','OthersVal' => '');
	$restdy = [];
	$existcust ="";
	$i=0;

	/*for($j = 0; $j < count($res); $j++){
		$pid =(string) $res[$j]['Cust_Code'];
		$mnth =(string) $res[$j]['Mnth'];
		
		if(stripos($existcust,$pid.",".$mnth.";")>-1){}
		else{
			$new_array = array();
			$new_array['Cust_Code']=$res[$j]['Cust_Code'];
			$new_array['Mnth']=$res[$j]['Mnth'];
			$existcust = $existcust.$pid.",".$mnth.";";
			
			$itmP=array();
			for($k=0;$k<count($resg);$k++){
				$itm=array();
				$itm["itemname"]=$resg[$k]['name'];
				
				$filtarray = array_values(array_filter($res, function($obj)  use ($pid,$mnth){
							if(($obj["Cust_Code"] === $pid) && ($obj["Mnth"] === $mnth) && ($obj["Product_Cat_Name"] ===$resg[$k]['name']) return true;
						}));
						
				for($j=0;$j<count($filtarray);$j++)
				{
					$itmval=array();
					$itmval["Qty"]=$filtarray[$j]['Ltrs'];
					$itmval["Val"]=$filtarray[$j]['oval'];
					array_push($itm["itemVals"],$itmval);
				}
				array_push($new_array['Mnth'],$itm);
			}
			$i++;
			array_push($resA,$new_array);
		}
	}
	$i=0;
	$existcust="";
	for($j = 0; $j < count($resdy); $j++){
		$pid =(string) $resdy[$j]['Cust_Code'];
		if(stripos($existcust,$pid.";")>-1){}
		else{
			$new_array = array();
			$new_array['Cust_Code']=$resdy[$j]['Cust_Code'];
			$existcust = $existcust.$pid.";";
			$filtarray = array_values(array_filter($resdy, function($obj)  use ($pid){
							if(($obj["Cust_Code"] === $pid)) return true;
						}));
			for($k=0;$k<count($filtarray);$k++)
			{
				$new_array[$filtarray[$k]['Product_Cat_Name']]=$filtarray[$k]['Ltrs'];
				$new_array[$filtarray[$k]['Product_Cat_Name'].'Val']=$filtarray[$k]['oval'];
			}
			$i++;
			array_push($restdy,$new_array);
		}
	}
	$result['success']=true;
	$result['data']=$resA;
	$result['todaydata']=$restdy;
	$result['Group']=$resg;
	
	return $result;*/
}
function getNoOrderRmks(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$Div=str_replace(",","",$data["Div"]);
	$sql = "select id,content name from vwRmksTemplate WITH (NOLOCK) where Division_Code='" . $Div . "' and ActFlag=0";
	$res=performQuery($sql);
	return $res;
}
function getSalesSummary(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$logintype=$data["loginType"];
	if($logintype=="Distributor"){
		$sql = "select isnull(sum(Orders),0) Orders,isnull(sum(NOrders),0) NOrders,isnull(sum(NoOrder),0) NoOrder,isnull(sum(isnull(InvCnt,0)),0) InvCnt,isnull(sum(isnull(InvVal,0)),0) InvVal from (
				select ListedDrCode OutletID,sum(iif(POB_Value>0,1,0)) Orders,sum(iif(POB_Value>0,1,0)) NOrders,sum(iif(POB_Value<=0,1,0)) NoOrder from DCRMain_Trans th WITH (NOLOCK)
				inner join DCRDetail_Lst_Trans td WITH (NOLOCK) on th.Trans_SlNo=td.Trans_SlNo 
				and td.stockist_code='$SF'
				inner join Mas_ListedDr d WITH (NOLOCK) on d.ListedDrCode=td.Trans_Detail_Info_Code
				where cast(Activity_Date as date)='".date('Y-m-d 00:00:00')."' group by ListedDrCode) as t
				full outer join (select Cus_Code,count(Cus_Code) InvCnt,isnull(sum(isnull(Total,0)),0) InvVal
				from Trans_Invoice_Head  WITH (NOLOCK)
				where Dis_Code='$SF'  and Invoice_Date>='".date('Y-m-d 00:00:00')."' and Invoice_Date<DATEADD(DAY,1,'".date('Y-m-d 00:00:00')."') group by Cus_Code) as Inv on CAST(OutletID as varchar)=inv.Cus_Code";
                      
                         
	}
	else{
		$sql = "select isnull(sum(Orders),0) Orders,isnull(sum(NOrders),0) NOrders,isnull(sum(NoOrder),0) NoOrder,isnull(sum(isnull(InvCnt,0)),0) InvCnt,isnull(sum(isnull(InvVal,0)),0) InvVal from (
					select ListedDrCode OutletID,sum(iif(POB_Value>0,1,0)) Orders,sum(iif(POB_Value>0,1,0)) NOrders,sum(iif(POB_Value<=0,1,0)) NoOrder from DCRMain_Trans th WITH (NOLOCK)
					inner join DCRDetail_Lst_Trans td WITH (NOLOCK) on th.Trans_SlNo=td.Trans_SlNo 
					inner join Customer_Hierarchy_Details C WITH (NOLOCK) on stockist_code=Dist_Code and C.SF_Code='$SF'
					inner join Mas_ListedDr d WITH (NOLOCK) on d.ListedDrCode=td.Trans_Detail_Info_Code
					where cast(Activity_Date as date)='".date('Y-m-d 00:00:00')."' group by ListedDrCode) as t
					full outer join (select Cus_Code,count(Cus_Code) InvCnt,isnull(sum(isnull(Total,0)),0) InvVal
					from Trans_Invoice_Head  WITH (NOLOCK)
					inner join Customer_Hierarchy_Details C on Dis_Code=Dist_Code and C.SF_Code='$SF' where Invoice_Date>='".date('Y-m-d 00:00:00')."' and Invoice_Date<DATEADD(DAY,1,'".date('Y-m-d 00:00:00')."') group by Cus_Code) as Inv on CAST(OutletID as varchar)=inv.Cus_Code";
	}
	$res=performQuery($sql);
	return $res;
}
function getPendingPaymentDets(){
      $div=$_GET['divisionCode'];
      $divs = explode(",", $div . ",");
      $Owndiv = (string) $divs[0];
      $stk_code=$_GET['stockist_code'];
      $cus_code=$_GET['Customer_Code'];
      $From_Year=$_GET['From_Year'];
      $To_Year=$_GET['To_Year'];
      $From_Month=$_GET['From_Month'];
      $To_Month=$_GET['To_Month'];
      $Type=$_GET['Type'];
      $sql="exec get_pending_payment_details_App '".$stk_code."','".$Owndiv."','".$cus_code."' ";
      return performQuery($sql);

      //$result['success']=true;
	//$result['todaydata']=$sql;
     // $result['data']=$resA;
	//return $result;
      
}
function getAdvanceAmtDets(){
	$cus_code=$_GET['Customer_Code'];
	 $sql="exec sp_Ret_adv '".$cus_code."' ";
      return performQuery($sql);
}

function saveAdvPayments(){
	 $div=$_GET['divisionCode'];
      $divs = explode(",", $div . ",");
      $Owndiv = (string) $divs[0];
	   $cus_code=$_GET['Customer_Code'];
	   $adv_amt=$_GET['Advance_Amt'];
	    $sql="exec sp_Save_adv_for_retailer '".$cus_code."','".$adv_amt."','".$Owndiv."' ";
        return performQuery($sql);
}
function getCollectPaymentList(){
	 $stk_code=$_GET['stockist_code'];
	  $From_Date=$_GET['From_Date'];
      $To_Date=$_GET['To_Date'];
	   $sql="exec get_payment_list '".$stk_code."','".$From_Date."','".$To_Date."' ";
      return performQuery($sql);
}
function getCollectPaymentDets(){ 
     $stk_code=$_GET['stockist_code'];
	  $div=$_GET['divisionCode'];
      $divs = explode(",", $div . ",");
	  $Owndiv = (string) $divs[0];
	  $order_no=$_GET['order_no'];
	  
	   $sql="exec Sp_Get_Payment_View '".$stk_code."','".$Owndiv."','".$order_no."' ";
      return performQuery($sql);
	
}

function addEntry() {
 $sfCode = $_GET['sfCode'];
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $data = json_decode($_POST['data'], true);
    $today = date('Y-m-d 00:00:00');
    $temp = array_keys($data[0]);
    $vals = $data[0][$temp[0]];
    switch ($temp[0]) {
case "PendingPaymentDetailsNative":
                         $SfCode = $_GET['sfCode'];
						 $SfName=$_GET['sfName'];
						 $div = $_GET['divisionCode'];
                         $divs = explode(",", $div . ",");
                         $Owndiv = (string) $divs[0];
                         $Type=$_GET['Type'];
                          $Stk_Code=$vals["Stockist_Id"];
                          $Stk_Name=$vals["Stockist_Name"];
                          $Cust_ID=$vals["Cust_ID"];
						  $Cust_Name=$vals["Cust_Name"];
						  $Total_amount=$vals["Total_amount"];
						  $Mode=$vals["Mode"];
						  $Reference_No=$vals["Reference_No"];
						  $Bk_name=$vals["Bk_name"];
						  $Pay_Date=$vals["Pay_Date"];
						  $collect_by=$vals["collect_by"];
						  $Remark=$vals["Remark"];
						  $Advance_pay=$vals["Advance_pay"];
						  $invoice_no=$vals["invoice_no"];
						  $Route_code=$vals["Route_code"];
                                      $eKey=$vals["eKey"];
						  
						   $sql = "SELECT ISNULL(MAX(Sl_No),0)+1 as slNo FROM trans_payment_detail ";
                            $tRw = performQuery($sql);
                            $pk = (int) $tRw[0]['slNo'];
							
							  $sqlQry = "insert into trans_payment_detail(Sl_No,Sf_Code,Sf_Name,Cust_Id,Cus_Name,Amount,Pay_Mode,Pay_Date,Pay_Ref_No,Remarks,Distributor_Code,Route_code,eDate,PaymentName,advance_pay,div_code,invoice_no,Collected_by,type,eKey) Values('".$pk."','".$SfCode."','".$SfName."','".$Cust_ID."','".$Cust_Name."','".$Total_amount."','".$Mode."','". date('m-d-Y H:i:s') . "','".$Reference_No."','".$Remark."','".$Stk_Code."','".$Route_code."','".$Pay_Date."','".$Bk_name."','".$Advance_pay."','".$Owndiv."','".$invoice_no."','".$collect_by."','".$Type."','".$eKey."')";
									
									performQuery($sqlQry);
								
                             $PPXML = "<ROOT>";
                            for($il=0;$il<count($vals["Payment_Details"]);$il++)
                            {
                              $PDets=$vals["Payment_Details"][$il];
							  
							   $Newsql = "insert into trans_payment_detail_view(Sl_No,bill_no,bill_date,bill_amt,Pen_amt,paid_amt,eKey) Values('".$pk."','" . $PDets["bill_no"] . "','". date('Y-m-d H:i:s') . "','" . $PDets["bill_amt"] . "','" . $PDets["Pen_amt"] . "','" . $PDets["paid_amt"] ."','".$eKey."')";
							   
							   
							  performQuery($Newsql);

 $PPXML = $PPXML . "<Bill Sf_Code=\"".$Stk_Code."\" Cust_Code=\"".$Cust_ID."\" Received_amt=\"".$PDets["paid_amt"]."\" Pay_Date=\"".date('Y-m-d H:i:s')."\" Bill_No=\"".$PDets["order_no"]."\" Inv_no=\"".$PDets["bill_no"]."\" />";

							}
                                          $PPXML = $PPXML . "</ROOT>";
                              $Upsql= "exec Update_Pending_bill '" .$PPXML. "','','', '".$Stk_Code."' ,'".$Cust_ID."','".$Total_amount."','".$Advance_pay."','1','Payment'";
                               performQuery($Upsql);
							
							 $result = array();
							 $result['sql'] = $sql;
                             $result['sqlQry'] = $sqlQry;
                             $result['Newsql'] = $Newsql;
                              $result['Upsql'] = $Upsql;
                             $result["success"] = true;
                             outputJSON($result);
							die;
                        break;




    }

    $resp["success"] = true;
   $resp["SPSQl"] = $sqlsp;
    echo json_encode($resp);
	}
function getDisributorwiseSales(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$distributorid=$data["distributorid"];
	$FDT=$data["FDT"];
	$TDT=$data["TDT"];
	$restdy = [];
	
	$new_array = array();
	
	$sql="select COUNT(Trans_Detail_Info_Code)Cnt from (
			select Trans_Detail_Info_Code from DCRMain_Trans mt with(nolock)
			inner join DCRDetail_Lst_Trans lt with(nolock) on lt.Trans_SlNo=mt.Trans_SlNo
			where Activity_Date>='".$FDT."' and Activity_Date<DATEADD(DAY,1,'".$TDT."') and lt.stockist_code='".$distributorid."'
			group by Trans_Detail_Info_Code)as t";	
	$VisitArr=performQuery($sql);
	if(count($VisitArr)>0){
		$new_array['Calls']=$VisitArr[0]['Cnt'];		
	}
	else{		
		$new_array['Calls']=0;
	}
	$sql="select COUNT(Cust_Code)Cnt from (
			select Cust_Code from Trans_Order_Head with(nolock)
			where Stockist_Code='".$distributorid."' and Order_Date>='".$FDT."' and Order_Date<DATEADD(DAY,1,'".$TDT."')
			group by Cust_Code)as t";	
	$OrderArr=performQuery($sql);
	if(count($OrderArr)>0){
		$new_array['Orders']=$OrderArr[0]['Cnt'];		
	}
	else{		
		$new_array['Orders']=0;
	}
	
	$sql="select COUNT(Cus_Code)Cnt from (
			select Cus_Code from Trans_Invoice_Head with(nolock)
			where Dis_Code='".$distributorid."' and Invoice_Date>='".$FDT."' and Invoice_Date<DATEADD(DAY,1,'".$TDT."')
			group by Cus_Code)as t";	
	$InvoiceArr=performQuery($sql);	
	if(count($InvoiceArr)>0){
		$new_array['Invoices']=$InvoiceArr[0]['Cnt'];		
	}
	else{		
		$new_array['Invoices']=0;
	}
	
	$sql="select COUNT(Retailer_Code)Cnt from Trans_POP_Entry_Head with(nolock) where Stockist_Code='".$distributorid."' and Entry_Date>='".$FDT."' and Entry_Date<DATEADD(DAY,1,'".$TDT."')";	
	$POPArr=performQuery($sql);	
	if(count($POPArr)>0){
		$new_array['POP']=$POPArr[0]['Cnt'];		
	}
	else{		
		$new_array['POP']=0;
	}
	
	$sql="select COUNT(Retailer_Code)Cnt from Trans_OtherBrand_Entry_Head with(nolock) where Stockist_Code='".$distributorid."' and Entry_Date>='".$FDT."' and Entry_Date<DATEADD(DAY,1,'".$TDT."')";	
	$OtherBrandArr=performQuery($sql);	
	if(count($OtherBrandArr)>0){
		$new_array['Otherbrand']=$OtherBrandArr[0]['Cnt'];	
	}
	else{		
		$new_array['Otherbrand']=0;
	}
	
	$sql="select COUNT(Retailer_Code)Cnt from Trans_QPS_Entry where Stockist_Code='".$distributorid."' and Entry_Date>='".$FDT."' and Entry_Date<DATEADD(DAY,1,'".$TDT."')";	
	$QPSArr=performQuery($sql);	
	if(count($QPSArr)>0){
		$new_array['QPS']=$QPSArr[0]['Cnt'];	
	}
	else{		
		$new_array['QPS']=0;
	}
	
	$sql="select COUNT(CustCode)Cnt from Trans_Cooler_Info_Head with(nolock) where Dist_Code='".$distributorid."' and Created_Date>='".$FDT."' and Created_Date<DATEADD(DAY,1,'".$TDT."')";	
	$CoolerArr=performQuery($sql);	
	if(count($CoolerArr)>0){
		$new_array['Cooler']=$CoolerArr[0]['Cnt'];	
	}
	else{		
		$new_array['Cooler']=0;
	}
	
	$sql="exec getCategorywiseInvoiceVaue '".$distributorid."','".$FDT."','".$TDT."'";	
	$CategorySalesArr=performQuery($sql);		
	if(count($CategorySalesArr)>0){
		$new_array['CategorySales']=$CategorySalesArr;	
	}
	else{		
		$new_array['CategorySales']=[];
	}
	array_push($restdy,$new_array);
	
	return $restdy;
}
function getsalessummarydetail(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$mode=$data["mode"];
	$logintype=$data["loginType"];
	if($logintype=="Distributor"){
		switch ($mode) {
			case "order":
				$sql = "select Trans_Sl_No TransactionNo,Code OutletCode,ListedDr_Name OutletName,Order_Value TransactionAmt,st.Stockist_Name FranchiseName,
						ISNULL(ms.Sf_Name,st.Stockist_Name)Sf_Name,CONVERT(varchar,th.Order_Date,103)+' '+CONVERT(varchar,th.Order_Date,108)Date_Time,
						st.Stockist_Address [Address],st.Stockist_Mobile Mobile
						from Trans_Order_Head th WITH (NOLOCK)
						inner join Mas_ListedDr d WITH (NOLOCK) on CAST(d.ListedDrCode as varchar)=th.Cust_Code
						inner join Mas_Stockist st WITH (NOLOCK) on st.Stockist_Code=th.Stockist_Code
						left join Mas_Salesforce ms WITH (NOLOCK) on ms.Sf_Code=th.Sf_Code
						where th.stockist_code='$SF' and cast(Order_Date as date)='".date('Y-m-d 00:00:00')."'";
						
				$psql = "select th.Trans_Sl_No TransactionNo,pd.Product_Detail_Name Product_Name,td.Quantity
						from Trans_Order_Head th WITH (NOLOCK)
						inner join Trans_Order_Details td WITH (NOLOCK) on td.Trans_Sl_No=th.Trans_Sl_No
						inner join Mas_Product_Detail pd WITH (NOLOCK) on pd.Product_Detail_Code=td.Product_Code
						where th.stockist_code='$SF' and cast(Order_Date as date)='".date('Y-m-d 00:00:00')."'";
			break;
			case "noorder":
				$sql = "select Trans_Detail_Name OutletName,st.Stockist_Name FranchiseName,isnull(Activity_Remarks,'') Remarks,Sf_Name,
						CONVERT(varchar,lt.ModTime,103)+' '+CONVERT(varchar,lt.ModTime,108)Date_Time,st.Stockist_Address [Address],st.Stockist_Mobile Mobile
						from DCRMain_Trans mt WITH (NOLOCK)
						inner join DCRDetail_Lst_Trans lt WITH (NOLOCK) on mt.Trans_SlNo=lt.Trans_SlNo
						inner join Mas_Stockist st WITH (NOLOCK) on st.Stockist_Code=lt.stockist_code
						where lt.stockist_code='$SF' and cast(Activity_Date as date)='".date('Y-m-d 00:00:00')."' and POB_Value=0";
			break;
			case "invoice":
				$sql = "select Trans_Inv_Slno TransactionNo,Cus_Name OutletName,Stockist_Name FranchiseName,ISNULL(ms.Sf_Name,Stockist_Name)Sf_Name,Total TransactionAmt,
						CONVERT(varchar,th.Invoice_Date,103)+' '+CONVERT(varchar,th.Invoice_Date,108)Date_Time,st.Stockist_Address [Address],st.Stockist_Mobile Mobile
						from Trans_Invoice_Head th WITH (NOLOCK)
						inner join Mas_Stockist st WITH (NOLOCK) on st.Stockist_Code=th.Dis_Code
						left join Mas_Salesforce ms WITH (NOLOCK) on ms.Sf_Code=th.Sf_Code
						where th.Dis_Code='$SF' and convert(Date,Invoice_Date)='".date('Y-m-d 00:00:00')."'";
				
						
				$psql = "select th.Trans_Inv_Slno TransactionNo,pd.Product_Detail_Name Product_Name,td.Quantity
						from Trans_Invoice_Head th WITH (NOLOCK)
						inner join Trans_Invoice_Details td WITH (NOLOCK) on td.Trans_Inv_Slno=th.Trans_Inv_Slno
						inner join Mas_Product_Detail pd WITH (NOLOCK) on pd.Product_Detail_Code=td.Product_Code
						where th.Dis_Code='$SF' and convert(Date,Invoice_Date)='".date('Y-m-d 00:00:00')."'";
			break;
			case "neworder":
				$sql = "select Trans_Sl_No TransactionNo,Code OutletCode,ListedDr_Name OutletName,Order_Value TransactionAmt,st.Stockist_Name FranchiseName,
						ISNULL(ms.Sf_Name,st.Stockist_Name)Sf_Name,CONVERT(varchar,th.Order_Date,103)+' '+CONVERT(varchar,th.Order_Date,108)Date_Time,
						st.Stockist_Address [Address],st.Stockist_Mobile Mobile
						from Trans_Order_Head th WITH (NOLOCK)
						inner join Mas_ListedDr d WITH (NOLOCK) on CAST(d.ListedDrCode as varchar)=th.Cust_Code
						inner join Mas_Stockist st WITH (NOLOCK) on st.Stockist_Code=th.Stockist_Code
						left join Mas_Salesforce ms WITH (NOLOCK) on ms.Sf_Code=th.Sf_Code
						where th.stockist_code='$SF' and cast(Order_Date as date)='".date('Y-m-d 00:00:00')."'";
				
						
				$psql = "select th.Trans_Sl_No TransactionNo,pd.Product_Detail_Name Product_Name,td.Quantity
						from Trans_Order_Head th WITH (NOLOCK)
						inner join Trans_Order_Details td WITH (NOLOCK) on td.Trans_Sl_No=th.Trans_Sl_No
						inner join Mas_Product_Detail pd WITH (NOLOCK) on pd.Product_Detail_Code=td.Product_Code
						where th.stockist_code='$SF' and cast(Order_Date as date)='".date('Y-m-d 00:00:00')."'";
			break;
		}																							
	}
	else
	{
		switch ($mode) {
			case "order":
				$sql = "select Trans_Sl_No TransactionNo,Code OutletCode,ListedDr_Name OutletName,Order_Value TransactionAmt,
						st.Stockist_Name FranchiseName,ISNULL(ms.Sf_Name,st.Stockist_Name)Sf_Name,						
						CONVERT(varchar,th.Order_Date,103)+' '+CONVERT(varchar,th.Order_Date,108)Date_Time,
						st.Stockist_Address [Address],st.Stockist_Mobile Mobile
						from Trans_Order_Head th WITH (NOLOCK)
						inner join Customer_Hierarchy_Details C WITH (NOLOCK) on th.stockist_code=C.Dist_Code and C.SF_Code='$SF'
						inner join Mas_ListedDr d WITH (NOLOCK) on CAST(d.ListedDrCode as varchar)=th.Cust_Code
						inner join Mas_Stockist st WITH (NOLOCK) on st.Stockist_Code=th.Stockist_Code
						left join Mas_Salesforce ms WITH (NOLOCK) on ms.Sf_Code=th.Sf_Code
						where cast(Order_Date as date)='".date('Y-m-d 00:00:00')."'";
						
				$psql = "select th.Trans_Sl_No TransactionNo,pd.Product_Detail_Name Product_Name,td.Quantity
						from Trans_Order_Head th WITH (NOLOCK)
						inner join Customer_Hierarchy_Details C WITH (NOLOCK) on th.stockist_code=C.Dist_Code and C.SF_Code='$SF'
						inner join Trans_Order_Details td WITH (NOLOCK) on td.Trans_Sl_No=th.Trans_Sl_No
						inner join Mas_Product_Detail pd WITH (NOLOCK) on pd.Product_Detail_Code=td.Product_Code
						where cast(Order_Date as date)='".date('Y-m-d 00:00:00')."'";
			break;
			case "noorder":
				$sql = "select Trans_Detail_Name OutletName,st.Stockist_Name FranchiseName,Activity_Remarks Remarks,Sf_Name,
						CONVERT(varchar,lt.ModTime,103)+' '+CONVERT(varchar,lt.ModTime,108)Date_Time,st.Stockist_Address [Address],st.Stockist_Mobile Mobile
						from DCRMain_Trans mt WITH (NOLOCK)
						inner join DCRDetail_Lst_Trans lt WITH (NOLOCK) on mt.Trans_SlNo=lt.Trans_SlNo
						inner join Customer_Hierarchy_Details C WITH (NOLOCK) on lt.stockist_code=Dist_Code and C.SF_Code='$SF'
						inner join Mas_Stockist st WITH (NOLOCK) on st.Stockist_Code=lt.stockist_code
						where cast(Activity_Date as date)='".date('Y-m-d 00:00:00')."' and POB_Value=0";
			break;
			case "invoice":
				$sql = "select Trans_Inv_Slno TransactionNo,Cus_Name OutletName,Stockist_Name FranchiseName,
						ISNULL(ms.Sf_Name,Stockist_Name)Sf_Name,Total TransactionAmt,
						CONVERT(varchar,th.Invoice_Date,103)+' '+CONVERT(varchar,th.Invoice_Date,108)Date_Time,st.Stockist_Address [Address],st.Stockist_Mobile Mobile
						from Trans_Invoice_Head th WITH (NOLOCK)
						inner join Customer_Hierarchy_Details C WITH (NOLOCK) on th.Dis_Code=C.Dist_Code and C.SF_Code='$SF'
						inner join Mas_Stockist st WITH (NOLOCK) on st.Stockist_Code=th.Dis_Code
						left join Mas_Salesforce ms WITH (NOLOCK) on ms.Sf_Code=th.Sf_Code
						where convert(Date,Invoice_Date)='".date('Y-m-d 00:00:00')."'";
				
						
				$psql = "select th.Trans_Inv_Slno TransactionNo,pd.Product_Detail_Name Product_Name,td.Quantity
						from Trans_Invoice_Head th WITH (NOLOCK)
						inner join Customer_Hierarchy_Details C WITH (NOLOCK) on th.Dis_Code=C.Dist_Code and C.SF_Code='$SF'
						inner join Trans_Invoice_Details td WITH (NOLOCK) on td.Trans_Inv_Slno=th.Trans_Inv_Slno
						inner join Mas_Product_Detail pd WITH (NOLOCK) on pd.Product_Detail_Code=td.Product_Code
						where convert(Date,Invoice_Date)='".date('Y-m-d 00:00:00')."'";
			break;
			case "neworder":
				$sql = "select Trans_Sl_No TransactionNo,Code OutletCode,ListedDr_Name OutletName,Order_Value TransactionAmt,
						st.Stockist_Name FranchiseName,ISNULL(ms.Sf_Name,st.Stockist_Name)Sf_Name,						
						CONVERT(varchar,th.Order_Date,103)+' '+CONVERT(varchar,th.Order_Date,108)Date_Time,
						st.Stockist_Address [Address],st.Stockist_Mobile Mobile
						from Trans_Order_Head th WITH (NOLOCK)
						inner join Customer_Hierarchy_Details C WITH (NOLOCK) on th.stockist_code=C.Dist_Code and C.SF_Code='$SF'
						inner join Mas_ListedDr d WITH (NOLOCK) on CAST(d.ListedDrCode as varchar)=th.Cust_Code
						inner join Mas_Stockist st WITH (NOLOCK) on st.Stockist_Code=th.Stockist_Code
						left join Mas_Salesforce ms WITH (NOLOCK) on ms.Sf_Code=th.Sf_Code
						where cast(Order_Date as date)='".date('Y-m-d 00:00:00')."'";
						
				$psql = "select th.Trans_Sl_No TransactionNo,pd.Product_Detail_Name Product_Name,td.Quantity
						from Trans_Order_Head th WITH (NOLOCK)
						inner join Customer_Hierarchy_Details C WITH (NOLOCK) on th.stockist_code=C.Dist_Code and C.SF_Code='$SF'
						inner join Trans_Order_Details td WITH (NOLOCK) on td.Trans_Sl_No=th.Trans_Sl_No
						inner join Mas_Product_Detail pd WITH (NOLOCK) on pd.Product_Detail_Code=td.Product_Code
						where cast(Order_Date as date)='".date('Y-m-d 00:00:00')."'";
			break;
		}
	}
	$res=performQuery($sql);$prodarr=array();
	if($mode!="noorder"){
		$prodarr=performQuery($psql);
	}
	for($i=0;$i<count($res);$i++){
		$OrderNo=$res[$i]["TransactionNo"];
		$new_array = array_values(array_filter($prodarr, function($obj)  use ($OrderNo){
					if(($obj["TransactionNo"] === $OrderNo)) return true;
				}));
		$res[$i]["Products"]=$new_array;
	}
	$result["success"]=true;
	$result["sql"]=$sql;
	$result["data"]=$res;
	return $result;
}
function getOrderAndInvoice(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$stkcode=$data["distributorid"];
	$fdt=$data["fdt"];
	$tdt=$data["tdt"];
	
	$sql = "select h.Trans_Sl_No OrderID,dr.ListedDr_Name OutletName,CONVERT(varchar,h.Order_Date,103)[Date],h.Order_Value,
			case when h.Order_Flag=1 then 'Completed' when h.Order_Flag=0 then 'Pending' else '' end [Status] from Trans_Order_Head h WITH (NOLOCK) 
			left join Mas_ListedDr dr WITH (NOLOCK) on h.Cust_Code=CAST(dr.ListedDrCode as varchar)
			where CONVERT(date,h.Order_Date) between '".$fdt."' and '".$tdt."'  and h.Stockist_Code='".$stkcode."'
			order by Order_Date";
	$Orders=performQuery($sql);
	$sql = "select h.Trans_Sl_No OrderID,d.Product_Code,d.Product_Name,d.Quantity from Trans_Order_Head h WITH (NOLOCK)
			inner join Trans_Order_Details d WITH (NOLOCK) on h.Trans_Sl_No=d.Trans_Sl_No
			where CONVERT(date,h.Order_Date) between '".$fdt."' and '".$tdt."'  and h.Stockist_Code='".$stkcode."'
			order by Order_Date";
	$OrdersDetails=performQuery($sql);
	
	$sql = "select '' OrderID,ListedDr_Name OutletName,CONVERT(varchar,Activity_Date,103)[Date],td.POB_Value Order_Value,'No Order' [Status],'' Details from DCRMain_Trans th WITH (NOLOCK)
			inner join DCRDetail_Lst_Trans td WITH (NOLOCK) on th.Trans_SlNo=td.Trans_SlNo 
			inner join Mas_ListedDr d WITH (NOLOCK) on d.ListedDrCode=td.Trans_Detail_Info_Code
			where CONVERT(date,Activity_Date) between '".$fdt."' and '".$tdt."' and POB_Value<=0 and td.stockist_code='".$stkcode."'
			order by Activity_Date";
	$NoOrders=performQuery($sql);
	
	$sql = "select h.Trans_Inv_Slno InvoiceID,h.Order_No OrderID,dr.ListedDr_Name,dr.Code,CONVERT(varchar,h.Invoice_Date,103)[Date],Dis_Code Stockist_Code,
			h.Total Order_Value,'Completed' [Status] from Trans_Invoice_Head h  WITH (NOLOCK)
			inner join Mas_ListedDr dr WITH (NOLOCK) on h.Cus_Code=CAST(dr.ListedDrCode as varchar)
			where h.Invoice_Date>='".$fdt."' and h.Invoice_Date<DATEADD(DAY,1,'".$tdt."')  and h.Dis_Code='".$stkcode."'
			order by Invoice_Date";
	$Invoice=performQuery($sql);
	
	$sql = "select  h.Trans_Inv_Slno InvoiceID,d.Product_Code,d.Product_Name,d.Quantity from Trans_Invoice_Head h  WITH (NOLOCK)
			inner join Trans_Invoice_Details d WITH (NOLOCK) on d.Trans_Inv_Slno=h.Trans_Inv_Slno
			where h.Invoice_Date>='".$fdt."' and h.Invoice_Date<DATEADD(DAY,1,'".$tdt."')  and h.Dis_Code='".$stkcode."'
			order by Invoice_Date";
	$InvoiceDetails=performQuery($sql);
	
	for($k=0;$k<count($Orders);$k++){
		$orderid=$Orders[$k]['OrderID'];
		$ord_array = array_values(array_filter($OrdersDetails, function($obj)  use ($orderid){
									if(($obj["OrderID"] === $orderid)) return true;
								}));
		$Orders[$k]["Details"]= $ord_array;
	}
	
	for($k=0;$k<count($Invoice);$k++){
		$invoiceID=$Invoice[$k]['InvoiceID'];
		$inv_array = array_values(array_filter($InvoiceDetails, function($obj)  use ($invoiceID){
									if(($obj["InvoiceID"] === $invoiceID)) return true;
								}));
		$Invoice[$k]["Details"]= $inv_array;
	}
	$OrderArray=array_merge($Orders,$NoOrders);
	$res["success"]=true;
	$res["Orders"]=$OrderArray;
	$res["Invoice"]=$Invoice;
	return $res;
}

function getWorkTypeList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$sql = "exec GetWorkTypes_App '".$SF."'";
	$res=performQuery($sql);
	return $res;
}
function getProdGrpList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec getProductGroupList '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}
function getPOSProdGrpList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec getPOSProductGroupList '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}
function getPOSProdCateList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec GetPOSProdBrand_App '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}
function getPOSProdTypList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec getPOSProductTypeList '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}
function getProdTypList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec getProductTypeList '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}

function getProdCateList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec GetProdBrand_DMSApp '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}
function getPOSProdDetList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec GetPOSProducts_App '".$Stk."'";
	$res=performQuery($sql);
	for($i=0;$i<count($res);$i++){
		$pid=$res[$i]['id'];
		$Qry = "select * from vwMas_MUnitDets WITH (NOLOCK) where PCode='".$pid."'";
		$Uoms=performQuery($Qry);
		$res[$i]["UOMList"]=$Uoms;
	}
	return $res;
}
function getProdDetList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$outletID=$data["outletId"];
	/*$sql = "select  m.Product_Detail_Code id,Product_Detail_Name name,Sale_Erp_Code ERP_Code,Sample_Erp_Code ConversionFactor,m.Product_Brd_Code Product_Cat_Code,
								ROW_NUMBER() OVER (ORDER BY m.Product_Detail_Code) row_num,isNull(r.Target_Price,0) MRP,isNull(r.Retailor_Price,0) Rate,isNull(r.MRP_Price,0) SBRate,0 Qty,0 Amount,0 RegularQty,Product_Sale_Unit,product_unit,Prod_Detail_Sl_No,
								ISNULL(Unit_code,0)Unit_code,ISNULL(Default_UOMQty,0)Default_UOMQty,ISNULL(Default_UOM,0)Default_UOM,iif(isnull(Product_Image,'')<>'','http://hap.sanfmcg.com/MasterFiles/PImage/'+Product_Image,iif(isnull(image_path,'')<>'','http://hap.sanfmcg.com/MasterFiles/PImage/'+image_path,'')) PImage,isNull(m.Bar_Code,'')Bar_Code
								from  Mas_Product_Detail m 
								inner join Mas_Product_Brand b on m.Product_Brd_Code=b.Product_Brd_Code 
								
	inner join Mas_Product_Category1 C on CHARINDEX(','+cast(C.Product_Cat_Code as varchar)+',' ,','+P.Product_Cat_Code+',')>0 and C.Product_Cat_Div_Code=@subdiv
	inner join Mas_Product_Group G on CHARINDEX(','+cast(G.Product_Grp_Code as varchar)+',' ,','+P.Product_Grp_Code+',')>0 and g.SubDiv=@subdiv 
								left outer join Mas_Product_State_Rates r on m.Product_Detail_Code=r.Product_Detail_Code
								and charindex(','+cast(r.State_Code as varchar)+',',','+m.State_Code+',')>0 where Product_Active_Flag=0 and m.Division_Code='3' and cast(replace(Retailor_Price,' ','') as float)>0.0 and charindex(',24,',','+m.State_Code+',')>0 order by Prod_Detail_Sl_No";
	if($outletID<>'' && $outletID<>'OutletCode'){
		$sql = "select COUNT(OutletCode)Ct from Outlet_Category_Mapping where OutletCode='".$outletID."' and Price_List_No is not null";
		$res=performQuery($sql);
	}
	$sql = "select d.Price_List_SlNo from Mas_Stockist st WITH (NOLOCK) inner join SalesDistrict d WITH (NOLOCK) on d.DistrictCode=st.SalesDistrictCode where st.Division_Code=3 and DivERP='20' and Stockist_Code='".$Stk."' and ISNULL(d.Price_List_SlNo,'')<>''";
	$salesdistrictprice=performQuery($sql);
	if(count($salesdistrictprice)>0){
		$sql = "exec getProductAppforOutlet '".$Stk."','0'";
	}
	else if ($res[0]["Ct"]>0)
	{
		$sql = "exec getProductAppforOutlet '".$Stk."','".$outletID."'";
	}
	else{*/
		$sql = "exec GetProducts_App '".$Stk."'";		
	//}
	$res=performQuery($sql);
	$sql = "select * from vwMas_MUnitDets WITH (NOLOCK) where Division_Code=3";
	$uoms=performQuery($sql);	
	for($i=0;$i<count($res);$i++){
		$pid=$res[$i]['id'];		
		$uom_array = array_values(array_filter($uoms, function($obj)  use ($pid){
									if(($obj["PCode"] === $pid)) return true;
								}));
		$res[$i]["UOMList"]=$uom_array;
	}
	return $res;
}
function getPrimaryProdDetList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$div = $data['div'];
		$divs = explode(",", $div . ",");
		$Owndiv = (string) $divs[0];
         $logintype=$data["loginType"];
	$sql = "select  m.Product_Detail_Code id,Product_Detail_Name name,Sale_Erp_Code ERP_Code,Sample_Erp_Code ConversionFactor,m.Product_Brd_Code Product_Cat_Code,
								ROW_NUMBER() OVER (ORDER BY m.Product_Detail_Code) row_num,isNull(r.Target_Price,0) MRP,isNull(r.Retailor_Price,0) Rate,isNull(r.Distributor_Price,0) SBRate,0 Qty,0 Amount,0 RegularQty,Product_Sale_Unit,product_unit,Prod_Detail_Sl_No,
								ISNULL(Unit_code,0)Unit_code,ISNULL(Default_UOMQty,0)Default_UOMQty,ISNULL(Default_UOM,0)Default_UOM,iif(isnull(Product_Image,'')<>'','http://hap.sanfmcg.com/MasterFiles/PImage/'+Product_Image,iif(isnull(image_path,'')<>'','http://hap.sanfmcg.com/MasterFiles/PImage/'+image_path,'')) PImage  
								from  Mas_Product_Detail m inner join Mas_Product_Brand b on m.Product_Brd_Code=b.Product_Brd_Code left outer join Mas_Product_State_Rates r on m.Product_Detail_Code=r.Product_Detail_Code
								and charindex(','+cast(r.State_Code as varchar)+',',','+m.State_Code+',')>0 where Product_Active_Flag=0 and m.Division_Code='3' and cast(replace(Retailor_Price,' ','') as float)>0.0 and charindex(',24,',','+m.State_Code+',')>0 order by Prod_Detail_Sl_No";
								
	$sqls = "exec GetPriProducts_App_Mod '".$Stk."','".$logintype."'";
	$res=performQuery($sqls);	
	$sql = "exec GetConversion '".$Owndiv."'";
	$uoms=performQuery($sql);	
	/*for($i=0;$i<count($res);$i++){
		$pid=$res[$i]['id'];		
		$uom_array = array_values(array_filter($uoms, function($obj)  use ($pid){
									if(($obj["PCode"] === $pid)) return true;
								}));
		$res[$i]["UOMList"]=$uom_array;
	}*/
	return $res;
}

function getDistributor(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$sql = "exec getDistributorByHry '".$SF."'";
	$res=performQuery($sql);
	return $res;
}
function getindentProdDetList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec GetIndentProducts_App '".$Stk."','".$SF."'";
	$res=performQuery($sql);
	$sql = "select * from vwMas_MUnitDets WITH (NOLOCK) where Division_Code=3";
	$uoms=performQuery($sql);
	for($i=0;$i<count($res);$i++){
		$pid=$res[$i]['id'];		
		$uom_array = array_values(array_filter($uoms, function($obj)  use ($pid){
									if(($obj["PCode"] === $pid)) return true;
								}));
		$res[$i]["UOMList"]=$uom_array;
	}
	return $res;
}
function getindentProdGrpList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec getIndentProductGroupList '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}
function getindentProdCateList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec GetIndentProdBrand_App '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}
function getindentProdTypList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec getIndentProductTypeList '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}
function getRouteList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$Stk=$data["Stk"];
	$Div=$data["Div"];
	if($Stk=='') $Stk='-';
	$sql = "exec getRouteListByDis '$Stk'";//,'$Div'";
	$res=performQuery($sql);
	return $res;
}

function getProjectionProdGrpList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$sql = "exec getProjectionGroups '".$SF."'";
	$res=performQuery($sql);
	return $res;
}
function getProjectionProdTypList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$sql = "exec getProjectionTypeList '".$SF."'";
	$res=performQuery($sql);
	return $res;
}
function getProjectionProdCateList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$sql = "exec getProjectionBrandList '".$SF."'";
	$res=performQuery($sql);
	return $res;
}
function getProjectionProdDetList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec GetProjection_Products_App '".$SF."'";		
	$res=performQuery($sql);
	$sql = "select * from vwMas_MUnitDetsFromMaster WITH (NOLOCK) where Division_Code=3";
	$uoms=performQuery($sql);	
	for($i=0;$i<count($res);$i++){
		$pid=$res[$i]['id'];		
		$uom_array = array_values(array_filter($uoms, function($obj)  use ($pid){
									if(($obj["PCode"] === $pid)) return true;
								}));
		$res[$i]["UOMList"]=$uom_array;
	}
	return $res;
}
function getAuditProdGrpList(){
	$div = $_GET['div'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$sql = "exec getAuditGroups '".$divisionCode."'";
	$res=performQuery($sql);
	return $res;
}
function getAuditProdTypList(){
	$div = $_GET['div'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$sql = "exec getAuditTypeList '".$divisionCode."'";
	$res=performQuery($sql);
	return $res;
}
function getAuditProdCateList(){
	$div = $_GET['div'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$sql = "exec getAuditBrandList '".$divisionCode."'";
	$res=performQuery($sql);
	return $res;
}
function getAuditProdDetList(){
	$div = $_GET['div'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$sql = "exec GetAudit_Products_App '".$divisionCode."'";		
	$res=performQuery($sql);
	$sql = "select * from vwMas_MUnitDets WITH (NOLOCK) where Division_Code=3";
	$uoms=performQuery($sql);	
	for($i=0;$i<count($res);$i++){
		$pid=$res[$i]['id'];		
		$uom_array = array_values(array_filter($uoms, function($obj)  use ($pid){
									if(($obj["PCode"] === $pid)) return true;
								}));
		$res[$i]["UOMList"]=$uom_array;
	}
	return $res;
}
function saveStockLoading(){
	global $data;
    $data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];
	$taxtot=0;

	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Qty=\"".$OrderDetails[$j]["Product_Qty"]."\" Amt=\"".$OrderDetails[$j]["Product_Amount"]."\" confac=\"".$OrderDetails[$j]["ConversionFactor"]."\" UOMNm=\"".$OrderDetails[$j]["UOM_Nm"]."\" UOMId=\"".$OrderDetails[$j]["UOM_Id"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$sql = "exec saveStockLoading  '".$DCRHead["SF"]."','".$divisionCode."','".$DCRHead["dcr_activity_date"]."','".$DCRDetails["stockist_code"]."','".$PPXML."'";
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="Stock Loading Failed";
		outputJSON($result);
		die;
	} 
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
function saveStockUnLoading(){
	global $data;
    $data = json_decode($_POST['data'], true);
	$result=[];
	$div = $_GET['divisionCode'];
	$divs = explode(",", $div . ",");
	$divisionCode = (string) $divs[0];
	$DCRHead=$data[0]["Activity_Report_Head"];
	$DCRDetails=$data[0]["Activity_Doctor_Report"];
	$OrderDetails=$data[0]["Order_Details"];

	$PPXML = "<ROOT>";
	for ($j = 0; $j < count($OrderDetails); $j++) {
		$PPXML = $PPXML . "<Prod PCode=\"".$OrderDetails[$j]["product_code"]."\" Qty=\"".$OrderDetails[$j]["Product_Qty"]."\" Amt=\"".$OrderDetails[$j]["Product_Amount"]."\" confac=\"".$OrderDetails[$j]["ConversionFactor"]."\" UOMNm=\"".$OrderDetails[$j]["UOM_Nm"]."\" UOMId=\"".$OrderDetails[$j]["UOM_Id"]."\" />";
	}
	$PPXML = $PPXML . "</ROOT>";
	$sql = "exec saveStockUnLoading  '".$DCRHead["SF"]."','".$divisionCode."','".$DCRHead["dcr_activity_date"]."','".$DCRDetails["stockist_code"]."','".$PPXML."'";
	$res=performQuery($sql);
	if (sqlsrv_errors() != null) {
		$result["success"]=false;
		$result["Msg"]="Stock Loading Failed";
		outputJSON($result);
		die;
	} 
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}
$axn = $_GET['axn'];
$value = explode(":", $axn);
switch (strtolower($value[0])) {
	case "get/distributor":
		outputJSON(getDistributor());
		break;
	case "get/routelist":
		outputJSON(getRouteList());
		break;
	case "get/worktypes":
		outputJSON(getWorkTypeList());
		break;
	case "get/prodgroup":
		outputJSON(getProdGrpList());
		break;
	case "get/prodtypes":
		outputJSON(getProdTypList());
		break;
	case "get/prodcate":
		outputJSON(getProdCateList());
		break;
	case "get/posprodgroup":
		outputJSON(getPOSProdGrpList());
		break;
	case "get/posprodtypes":
		outputJSON(getPOSProdTypList());
		break;
	case "get/posprodcate":
		outputJSON(getPOSProdCateList());
		break;
	case "get/proddets":
		outputJSON(getProdDetList());
		break;
	case "get/prodprimarydets":
		outputJSON(getPrimaryProdDetList());
		break;
    case "save/salescalls":
        outputJSON(SaveDCRCalls());
	
        break;
	case "save/invoice":
		outputJSON(SaveInvoice());
        break;
	case "get/foodexp":
		getFoodExp();
		break;	
	case "get/noordrmks":
		outputJSON(getNoOrderRmks());
        break;
	case "get/salessumry":
		outputJSON(getSalesSummary());
		break;
	case "get/exceptravel":
		$sfCode = $_GET['sfCode'];
		$divisionCode=$_GET['divisionCode'];
		$qry="select g.Grade_ID from Mas_Salesforce s inner join Mas_SF_Designation d WITH (NOLOCK) on s.Designation_Code=d.Designation_Code inner join Mas_Grade g WITH (NOLOCK) on d.Group_Name=g.Grade_ID where s.Sf_Code='".$sfCode."'";
		$res=performQuery($qry);
		$query = "select Sl_No id,MOT name,StEndNeed,DriverNeed,isnull(FuelAmt,0) FuelAmt,isnull(Alw_Eligibilty,0) Alw_Eligibilty,(case when Charindex(',".$res[0]["Grade_ID"].",',','+GradeID+',')>0 then 1 else 0 end) Eligible from Mas_Modeof_Travel WITH (NOLOCK) where Active_Flag='0' and Deviation=1";
		outputJSON(performQuery($query));
	break;
	
	case "get/expensedatedetailsnew":
		outputJSON(getExpensedatenew(1));
		break;
	case "save/otherbrandentry":
		outputJSON(saveOtherBrandEntry());
		break;
	case "save/qpsentry":
		outputJSON(saveQPSEntry());
		break;
	case "save/popentry":
		outputJSON(savePOPEntry());
		break;
	case "approve/qpsentry":
		outputJSON(approveQPSEntry());
		break;
	case "get/stayallw":
		outputJSON(getStayAllwDet());
		break;
	case "get/empbyid":
		outputJSON(getEmpDetByID());
		break;
	
	case "save/allowance":
		outputJSON(saveAllowance(0));
		break;
	case "save/taapprove":
        outputJSON(saveApprove());
        break;
	case "save/recallallowance":
		outputJSON(saveAllowance(3));
		break;
	case "update/da":
		outputJSON(getUpdateDA());
		break;
	case "update/allowance":
		outputJSON(saveAllowance(1));
		break;
		
	case "update/predayallowance":
		outputJSON(saveAllowance(2));
		break;
    case "save/exception":
        outputJSON(SaveExceptionEntry());
        break;
	case "get/qpsmaster":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$divisionCode=(string) $data['divisionCode'];
		$divs = explode(",", $divisionCode . ",");
		$Owndiv=$divs[0];
		$qry="select QPS_Code,QPS_Name,ERP_Code,Division_Code from QPS_Master WITH (NOLOCK) where Active_flag=0 and Division_Code=$Owndiv";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
		break;
	case "get/popmaster":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$divisionCode=(string) $data['divisionCode'];
		$divs = explode(",", $divisionCode . ",");
		$Owndiv=$divs[0];
		$distributorCode=(string) $data['distributorcode'];
		$qry="select POP_Code,POP_Name,ERP_Code,Division_Code,POP_UOM from POP_Material_master WITH (NOLOCK) where Active_flag=0 and Division_Code=$Owndiv";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/qpsallocation":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$divisionCode=(string) $data['divisionCode'];
		$divs = explode(",", $divisionCode . ",");
		$Owndiv=$divs[0];
		$retailorCode = (string) $data['retailorCode'];
		$distributorcode=(string) $data['distributorcode'];
		$qry="select Stockist_Code,Retailer_Code,qa.QPS_Code,qm.QPS_Name,qa.Division_Code,Days_Period,Per_Day_Ltrs,Total_Ltrs 
			  from QPS_Master_Allocation qa WITH (NOLOCK) inner join QPS_Master qm WITH (NOLOCK) on CAST(qm.QPS_Code as varchar)=qm.QPS_Code 
			  where qm.Division_Code=$Owndiv and Retailer_Code='$retailorCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/qpshaplitres":
		$data = json_decode($_POST['data'], true);
		$retailorCode = (string) $data['retailorCode'];
		$qry="select 0 HapLtr";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/prevorderqty":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$retailorCode = (string) $data['retailorCode'];
		$qry="exec getPreviousOrderQty '$sfCode','$retailorCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Qry"]=$qry;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=true;
			$result["Data"]=[];
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/cumulativevalues":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$div = (string) $data['divCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$Dt =(string) $data['dt'];
		$qry="exec getCumulative_AppDashboard '$sfCode','$divisionCode','$Dt'";
		//$res=array();
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/dashboardvalues":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$div = (string) $data['divCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$Dt =(string) $data['dt'];
		$qry="exec getSFA_App_DashBoard '$sfCode','$divisionCode','$Dt'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/categorywiseretailerdata":
		outputJSON(getcategorywiseretailerdata());
	break;
	case "get/outletsummary":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$div = (string) $data['divCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$Dt =(string) $data['dt'];
		$logintype=$data["loginType"];
		if($logintype=="Distributor"){}
		$qry="exec getService_Universe_OutletSummary '$sfCode','$divisionCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/serviceoutletsummary":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$div = (string) $data['divCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$Dt =(string) $data['dt'];
		$qry="exec getService_OutletSummary '$sfCode','$divisionCode','$Dt'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/channelwiseoutletsummary":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$div = (string) $data['divCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$Dt =(string) $data['dt'];
		$qry="exec getService_OutletSummary_Channelwise '$sfCode','$divisionCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/posscheme":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$DT=date("Y-m-d");
		$qry = "exec get_secondary_scheme '" . $sfCode . "','".$divisionCode."','".$DT."'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
			$result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/secondaryscheme":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$DT=date("Y-m-d");
		$qry = "exec get_secondary_scheme '" . $sfCode . "','".$divisionCode."','".$DT."'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			//$result["success"]=true;
			//$result["Data"]=$res;
			outputJSON($res);
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
			$result["Msg"]="No Records Found";
			outputJSON($result);
		}
		//outputJSON($result);
	break;
	case "get/primaryscheme":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfCode'];
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$DT=date("Y-m-d");
		$qry = "exec get_scheme_by_stk '" . $sfCode . "','".$divisionCode."','".$DT."'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			//$result["success"]=true;
			//$result["Data"]=$res;
			outputJSON($res);
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
			$result["Msg"]="No Records Found";
			outputJSON($result);
		}
		//outputJSON($result);
	break;
	case "get/orderdetailsfrinv":
		$data = json_decode($_POST['data'], true);
		$OrderID =(string) $data['OrderID'];
		$qry="exec getOrderDetailsFrInv '$OrderID'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/primaryproducttaxdetails":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['distributorid'];
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$qry="exec getAppPrimaryTax '$divisionCode','$sfCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/producttaxdetails":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['distributorid'];
		$retCode =(string) $data['retailorId'];
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$qry="exec get_App_tax_Details '$divisionCode','$sfCode','$retCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/posproducttaxdetails":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['distributorid'];
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$qry="exec geAppPOSTaxDetails '$divisionCode','$sfCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/paymenttype":
		$data = json_decode($_POST['data'], true);
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$qry="select Code,[Name] from Mas_Payment_Type WITH (NOLOCK) where CHARINDEX(',$divisionCode,',','+Division_Code+',')>0 and Active_Flag=0";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/outstanding":
		outputJSON(getOutletOutstanding());
	break;
	case "get/outletwiseledger":
		outputJSON(getOutletwiseLedger());
	break;
	case "get/customeroutstanding":
		$data = json_decode($_POST['data'], true);
		$retCode =(string) $data['retailerCode'];
		$distributorcode =(string) $data['distributorcode'];
		if($retCode<>'' && $retCode<>'OutletCode'){
			$qry="exec GetCustPayLedger '$distributorcode','$retCode','".date("Y-m-d 00:00:00")."','".date("Y-m-d 00:00:00")."'";
			$ress=performQuery($qry);
		}
		else{
			$ress=array();
		}
		if (count($ress) > 0) {
			$res=[];
			$res[0]=array("Outstanding"=>$ress[0]["Balance"]);
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/fencedoutlet":
		outputJSON(getFencedOutlet());
	break;
	case "get/orderandinvoice":
		outputJSON(getOrderAndInvoice());
	break;
	case "get/savepaymententry":
	outputJSON(savePaymentEntry());
	break;
	case "get/qpsentrystatus":
		$data = json_decode($_POST['data'], true);
		$retCode =(string) $data['retailerCode'];
		$qry="exec getQPS_Status '$retCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			for($i=0;$i<count($res);$i++){
				$images=explode(",",$res[$i]["Images"]);
				array_walk($images, function (&$value, $key) {
					if($value!=""){
						$value="QPS_Images/$value";
					}
				});
				$res[$i]["Images"]=$images;
			}
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "save/popapprove":
		outputJSON(savePOPApproval());
		break;
	case "get/popentrystatus":
		$data = json_decode($_POST['data'], true);
		$retCode =(string) $data['retailerCode'];
		$qry="select ROW_NUMBER() over(order by Trans_sl_No)SlNo,Trans_sl_No Requset_No,CONVERT(varchar,Entry_Date,103) Booking_Date from Trans_POP_Entry_Head WITH (NOLOCK) where retailer_Code='$retCode'";
		$resh=performQuery($qry);
		$qry="select d.Sl_No POP_Req_ID,d.Trans_Sl_No,POP_Name POP_ID,POP_ID POP_Name,Quantity,CONVERT(varchar,Booking_Dt,103) Received_Date,
			  case ISNULL(POP_Status_Flag,0) when 0 then 'Pending' when 1 then 'Approved' when 2 then 'Rejected' end as POP_Status,ISNULL(REPLACE(Images,',',',POP_Images/'),'')POP_Image,ISNULL(REPLACE(Images,',',',https://checkin.hap.in/POP_Images/'),'')Images
			  from Trans_POP_Entry_Detail d WITH (NOLOCK) inner join Trans_POP_Entry_Head h WITH (NOLOCK) on h.Trans_sl_No=d.Trans_Sl_No where h.retailer_Code='$retCode'";
		$restr=performQuery($qry);
		if (count($resh) > 0) {
			for($i=0;$i<count($resh);$i++){
				$reqID=$resh[$i]['Requset_No'];
				$filtarray = array_values(array_filter($restr, function($obj)  use ($reqID){
							if(($obj["Trans_Sl_No"] === $reqID)) return true;
						}));
				$resh[$i]["Details"]=$filtarray;
			}
			$result["success"]=true;
			$result["Data"]=$resh;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "save/placeid":
		$data = json_decode($_POST['data'], true);
		$placeid=$data["placeid"];
		$lat=$data["lat"];
		$lng=$data["lng"];
		$div = (string) $data['divCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$insDt=$data["date"];
		$sql = "insert into Mas_PlaceIDs select '$placeid','$lat','$lng','$divisionCode','$insDt'";
		$resord = performQuery($sql);
		$result["success"]=true;
		$result["sql"]=$sql;
		$result["Msg"]="Marked As Existing";
		outputJSON($result);
	break;	
	case "get/placeid":
		$data = json_decode($_POST['data'], true);
		$lat=$data["lat"];
		$lng=$data["lng"];
		$div = (string) $data['divCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$sql = "exec getPlaceIDByfencing '$divisionCode','$lat','$lng'";
		$resord = performQuery($sql);
		if(count($resord)>0){
			$result["Data"]=$resord[0]["Place_ID"];
		}
		else{
			$result["Data"]=[];
		}
		$result["success"]=true;
		outputJSON($result);
	break;	
	case "get/states":
		$sql = "select State_Code,StateName from Mas_State WITH (NOLOCK) where State_Active_Flag=0 order by 2";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["Data"]=$res;
		}
		else{
			$result["Data"]=[];
		}
		$result["success"]=true;
		outputJSON($result);
		
	break;	
	case "get/retailerorderstatus":
		$data = json_decode($_POST['data'], true);
		$distname=$data["distname"];
		$sql = "exec getRetailerOrderStatus '$distname'";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["Data"]=$res;
		}
		else{
			$result["Data"]=[];
		}
		$result["success"]=true;
		outputJSON($result);
	break;
	case "get/custbalance":getAccountBalance();
	break;
	case "get/salessummarydetails":
		outputJSON(getsalessummarydetail());
	break;
    case "save/primaryorder":
        outputJSON(SavePrimaryOrder());
	break;
	case "get/myteamlocation":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfcode'];
		$dt = $data['date'];
		$qry="exec getTeamCurrLOC '$sfCode','$dt'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
			$uniqueDes = array_map( function( $obj ) {
				return $obj["shortname"];
			}, $res );
			$uniqueDesArr = array_unique( $uniqueDes );
			$result["Designation"]=array_values($uniqueDesArr);
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["Designation"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/newmyteamlocation":
		$data = json_decode($_POST['data'], true);
		$sfCode =(string) $data['sfcode'];
		$dt = $data['date'];
		$lat = $data['lat'];
		$lng = $data['lng'];
		$div = $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$qry="exec getTeamCurrLOC '$sfCode','$dt'";
		$teamArr=performQuery($qry);
		$qry="exec getTeamOutlets '$sfCode','$divisionCode','$lat','$lng'";
		$outletArr=performQuery($qry);
		$qry="exec getTeamFranchiseLoc '$sfCode','$divisionCode','$lat','$lng'";
		$distArr=performQuery($qry);
		$res=array_merge($teamArr,$outletArr,$distArr);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
			$uniqueDes = array_map( function( $obj ) {
				return $obj["shortname"];
			}, $res );
			$uniqueDesArr = array_unique( $uniqueDes );
			$result["Designation"]=array_values($uniqueDesArr);
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["Designation"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/productuom":
		$data = json_decode($_POST['data'], true);
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$qry="select * from vwMas_MUnitDets WITH (NOLOCK) where Division_Code='$divisionCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			//$result["success"]=true;
			//$result["Data"]=$res;
			outputJSON($res);
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
			$result["data"]=$data;
			$result["div"]=$div;
			$result["divisionCode"]=$divisionCode;
		   $result["Msg"]="No Records Found";
		   outputJSON($result);
		}
		//outputJSON($result);
	break;
    case "save/posorder":
        outputJSON(SavePOSOrder());
	break;
	case "get/posproddets":
		outputJSON(getPOSProdDetList());
	break;
	case "get/flightpbookings":
		global $data;
		$data = json_decode($_POST['data'], true);
		$SF=$data["SF"];
		$FDT=$data["FDT"];
		$TDT=$data["TDT"];
		$sql="exec getFlightPendBookings '".$SF."'";
		$res=performQuery($sql);
		$sql="exec getFlightPendBookingPassengers '".$SF."'";
		$respass=performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			for ($il=0;$il<count($res);$il++)
			{
				$pid=$res[$il]["BookID"];
				$new_array = array_values(array_filter($respass, function($obj)  use ($pid){
								if(($obj["BookID"] === $pid)) return true;
							}));
				$res[$il]["Travellers"]=count($new_array);
				$res[$il]["TrvDetails"]=$new_array;
			}
			$result["data"]=$res;
		}
		else {
			$result["success"]=false;
			$result["data"]=[];
		}
		outputJSON($result);
	break;
	case "get/fbapprhist":
		global $data;
		$data = json_decode($_POST['data'], true);
		$SF=$data["SF"];
		$FDT=$data["FDT"];
		$TDT=$data["TDT"];
		$sql="exec getFlightBookingsHist '".$SF."','".$FDT."','".$TDT."'";
		$res=performQuery($sql);
		$sql="exec getFlightBookingPassengersHist '".$SF."','".$FDT."','".$TDT."'";
		$respass=performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			for ($il=0;$il<count($res);$il++)
			{
				$pid=$res[$il]["BookID"];
				$new_array = array_values(array_filter($respass, function($obj)  use ($pid){
								if(($obj["BookID"] === $pid)) return true;
							}));
				$res[$il]["Travellers"]=count($new_array);
				$res[$il]["TrvDetails"]=$new_array;
			}
			$result["data"]=$res;
		}
		else {
			$result["success"]=false;
			$result["data"]=[];
		}
		outputJSON($result);
	break;
	case "get/flightbookings":
		global $data;
		$data = json_decode($_POST['data'], true);
		$SF=$data["SF"];
		$FDT=$data["FDT"];
		$TDT=$data["TDT"];
		$sql="exec getFlightBookings '".$SF."','".$FDT."','".$TDT."'";
		$res=performQuery($sql);
		$sql="exec getFlightBookingPassengers '".$SF."','".$FDT."','".$TDT."'";
		$respass=performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			for ($il=0;$il<count($res);$il++)
			{
				$pid=$res[$il]["BookID"];
				$new_array = array_values(array_filter($respass, function($obj)  use ($pid){
								if(($obj["BookID"] === $pid)) return true;
							}));
				$res[$il]["Travellers"]=count($new_array);
				$res[$il]["TrvDetails"]=$new_array;
			}
			$result["data"]=$res;
		}
		else {
			$result["success"]=false;
			$result["data"]=[];
		}
		outputJSON($result);
	break;
	case "get/freezercapacity":
		$data = json_decode($_POST['data'], true);
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$qry="select ID,FCapacity from FreezerCapacity where DivisionCode='$divisionCode' and ActiveFlag=0";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/freezerstatus":
		$data = json_decode($_POST['data'], true);
		$div = (string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$qry="select ID,FStatus,isNull(ApprovalNeed,0)ApprovalNeed from FreezerStatus where DivisionCode='$divisionCode' and ActiveFlag=0";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/customerdetails":
		$data = json_decode($_POST['data'], true);
		$erpCode=$data["ERP_Code"];
		$custCode=$data["customer_code"];
		$qry="select ERP_Code Code,Stockist_Name [Name],Stockist_Address [Address],Stockist_Mobile [Mobile],isNull(Fssai_No,'')Fssai_No,isnull(gstn,'')gstn,Head_Quaters,State_Code from Mas_Stockist where ERP_Code='$custCode' and ERP_Code<>'$erpCode'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
		   $result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
		   $result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/indentproddets":
		outputJSON(getindentProdDetList());
	break;
	case "get/indentprodgroup":
		outputJSON(getindentProdGrpList());
	break;
	case "get/indentprodtypes":
		outputJSON(getindentProdTypList());
	break;
	case "get/indentprodcate":
		outputJSON(getindentProdCateList());
	break;
	case "save/indentorder":
		outputJSON(SaveIndentOrder());
	break;
	
	case "get/advtypes":
		outputJSON(getAdvanceType());
	break;
	case "save/advance":
		outputJSON(saveAdvDetails());
	break;
	
	case "approve/advance":
		outputJSON(approveAdvance());
	break;
	
	case "get/vanstock":
		outputJSON(getVanStock());
	break;
	case "get/stockistledger":
		outputJSON(getDistStockLedger());
	break;
	case "save/grnentry":
		outputJSON(GRNEntry());
	break;
	case "save/deliverysequence":
		outputJSON(updateDeliverySequence());
	break;
	case "save/salesreturn":
		outputJSON(saveSalesReturn());
	break;
	case "get/stockreturn":
		$data = json_decode($_POST['data'], true);
		
		$stkcode=$data["Stk"];
		$Dt=$data["Dt"];
		$RetID=$data["RetID"];
		$CustomerCode=$data["CustomerCode"];
		
		$qry="exec getPendingProducts '".$stkcode."','".$Dt."','".$RetID."','".$CustomerCode."'";
		$res=performQuery($qry);
		if (count($res) > 0) {
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["qry"]=$qry;
			$result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/retailerorderstatusch":
		$data = json_decode($_POST['data'], true);
		$distname=$data["distname"];
		$sql = "exec getOrder_Stat '$distname'";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["Data"]=$res;
		}
		else{
			$result["Data"]=[];
		}
		$result["success"]=true;
		outputJSON($result);
	break;
	case "get/outletcategory":
		$div = $_GET['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$sql = "select Doc_ClsCode CategoryCode,Doc_ClsSName CategoryName,OutletCategory,ISNULL(DivErp,'')DivErp from Mas_Doc_Class where Division_Code='$divisionCode' and Doc_Cls_ActiveFlag=0 order by 2";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
		}
		outputJSON($result);
	break;
	case "get/tcstax":
		$div = $_GET['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$sql = "select Tax_Id,Tax_Name,Tax_Type,Value from Tax_Master where Tax_Id=49";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
		}
		outputJSON($result);
	break;
	case "get/currentstock":
		$Stk = $_GET['Stk'];
		$Dt=$_GET["Dt"];
		$Dt=date('Y-m-d');
		$sql = "exec getStockCurrentStatus '$Stk','$Dt'";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
		}
		outputJSON($result);
	break;
	case "get/plantmaster":
		$div = $_GET['divisionCode'];
		$sfcode = $_GET['login_sfCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$sql = "exec getPlantsforProjection '$sfcode','$divisionCode'";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
		}
		outputJSON($result);
	break;
	case "get/coolerinfo":
		$data = json_decode($_POST['data'], true);
		$retailerCode=$data["retailerCode"];
		$sql = "exec getCoolerDetails '".$retailerCode."'";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/slottimes":
		$data = json_decode($_POST['data'], true);
		$distributorCode=$data["distributorCode"];
		$GrpCode=$data["GrpCode"];
		$sql = "exec getSlottime '".$distributorCode."','".$GrpCode."'";
		
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/weeklyexpense":
		//$data = json_decode($_POST['data'], true);
		$sfCode=$_GET["sfCode"];
		$sql = "exec getExpenseforApproval '".$sfCode."'";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "save/tacumulativeapprove":
		$data = json_decode($_POST['data'], true);
		$sfCode= (string) $data["emp_sfCode"];
		$appsfCode= (string) $data["login_sfCode"];
		$approvalFlag=$data["Flag"];
		$FDate=$data["FDate"];
		$TDate=$data["TDate"];
		$ApDate=$data["Date"];
		$RejectReason= (string) $data["Reason"];
		$RejectReason = str_replace("'","''",$RejectReason);
		$sql = "exec saveExpenseApprovalBulk '".$sfCode."','".$appsfCode."','".$FDate."','".$TDate."','".$approvalFlag."','".$ApDate."','".$RejectReason."'";
		$res = performQuery($sql);
		if (sqlsrv_errors() != null) {
			$result["success"]=false;
			$result["Msg"]="Submission Failed";
			outputJSON($result);
			die;
		}
		$result["success"]=true;
		$result["Msg"]="Success";		
		outputJSON($result);
	break;
	case "update/distlatlng":
		$data = json_decode($_POST['data'], true);
		$distributor_Id=$data["distributor_Id"];
		$sfCode=$data["sfCode"];
		$lat=$data["lat"];
		$lng=$data["lng"];
		$current_date=$data["current_date"];
		$flag=$data["flag"];
		$sql = "exec updateFranchiseLatLng '$distributor_Id','$sfCode','$lat','$lng','$current_date',".$flag."";
		$res = performQuery($sql);
		$result["Msg"]="Franchise Location Updated.";
		$result["success"]=true;
		outputJSON($result);
	break;
	case "get/primarydashboardvalues":
		$data = json_decode($_POST['data'], true);
		$sfcode=$data["login_sfCode"];
		$Dt=$data["Dt"];
		$Grpcode=$data["Grpcode"];
		$loginType=$data["loginType"];
		$sql = "exec getPrimaryOrderDashBoard '$sfcode','$Dt','$Grpcode','$loginType'";
		$res = performQuery($sql);
            $sqls = "exec getSecOrderDashBoard '$sfcode','$Dt'";
            $ress = performQuery($sqls);		
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
                  $result["DataSec"]=$ress;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
                  $result["DataSec"]=[];
			$result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
case "get/secOrderDashBoard":
		$data = json_decode($_POST['data'], true);
           // $sfCode=$_GET["sfCode"];
		//$Dt=$_GET["Dt"];
		$sfCode=$data["SF"];
		$Dt=$data["Dt"];
		$sql = "exec getSecOrderDashBoard '$sfCode','$Dt'";
            
		$res = performQuery($sql);
             	
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/groupfilter":
		$data = json_decode($_POST['data'], true);
		$distributor_Id=$data["distributorid"];
		$sql = "select subdivision_code from Mas_Stockist where Stockist_Code='$distributor_Id'";
		$newarr = performQuery($sql);
		$subDiv=$newarr[0]["subdivision_code"];
		$sql = "select Product_Grp_Code GroupCode,Product_Grp_Name name from Mas_Product_Group where SubDiv='$subDiv' and Product_Grp_Active_Flag=0";
		
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["Data"]=$res;
		}
		else{
			$result["success"]=false;
			$result["Data"]=[];
			$result["Msg"]="No Records Found";
		}
		outputJSON($result);
	break;
	case "get/planttype":
		$sql = "Select 'MFS' [Type] union all Select 'CFA'";
		$res = performQuery($sql);
		$result["success"]=true;
		$result["data"]=$res;
		outputJSON($result);
	break;
	case "get/mfscfa":
		$type=$_GET["Type"];
		$div = $_GET['divisionCode'];
		$divs = explode(",", $div . ",");
		$divisionCode = (string) $divs[0];
		$sql = "exec getMFSCFA '$type','$divisionCode'";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"]=true;
			$result["data"]=$res;
		}
		else{
			$result["success"]=true;
			$result["Msg"]="No Data Found";			
		}
		outputJSON($result);
	break;
	case "cancel/tapending":
		$data = json_decode($_POST['data'], true);
		$sfcode=$data["Sf_code"];
		$expdt=$data["Expdt"];
		
		$sql = "exec cancelTAClaim '$sfcode','$expdt'";
		$res = performQuery($sql);		
		if (sqlsrv_errors() != null) {
			$result["success"]=false;
			$result["Msg"]="TA Cancel Failed";
			outputJSON($result);
			die;
		}		
		$result["success"]=true;
		$result["Msg"]="TA Cancel Success";
		outputJSON($result);
	break;
	case "save/stockrotate":
		outputJSON(SaveStockRotate());
	break;
	case "save/vansales":
		outputJSON(SaveVanInvoice());
	break;
	case "get/projectionprodgroup":
		outputJSON(getProjectionProdGrpList());
	break;
	case "get/projectionprodtypes":
		outputJSON(getProjectionProdTypList());
	break;
	case "get/projectionprodcate":
		outputJSON(getProjectionProdCateList());
	break;
	case "get/projectionproddets":
		outputJSON(getProjectionProdDetList());
	break;
	case "save/projection":
		outputJSON(saveProjectionEntry());
	break;
	case "save/projectionnew":
		outputJSON(saveProjectionEntryNew());
	break;
	case "get/acbalance":
		getAccBalance();
	break;
	case "save/stockloading":
		outputJSON(saveStockLoading());
	break;
	case "save/stockunloading":
		outputJSON(saveStockUnLoading());
	break;
	case "save/coolerinfo":
		outputJSON(SaveCoolerInfo());
	break;
	case "save/primarycheck":
		outputJSON(SavePrimaryOrderChk());
	break;
	case "get/auditprodgroup":
		outputJSON(getAuditProdGrpList());
	break;
	case "get/auditprodtypes":
		outputJSON(getAuditProdTypList());
	break;
	case "get/auditprodcate":
		outputJSON(getAuditProdCateList());
	break;
	case "get/auditproddets":
		outputJSON(getAuditProdDetList());
	break;
	case "get/getauditstock":
		outputJSON(GetAuditStock());
	break;
	case "save/stockaudit":
		outputJSON(SaveStockAudit());
	break;
	case "get/disributorwisesales":
		outputJSON(getDisributorwiseSales());
	break;
     case "get/pendingpaymentdets":
          	outputJSON(getPendingPaymentDets());
    break;
    case "get/collectpaymentlist":
          outputJSON(getCollectPaymentList());
    break;
   case "get/collectpaymentdets":
          outputJSON(getCollectPaymentDets());
   break;
    case "get/saveadvpayment":
          	outputJSON(saveAdvPayments());
    break;
    case "get/advanceamtdets":
          	outputJSON(getAdvanceAmtDets());
    break;
	
	case "get/retailerrate":

				$div = $_GET['divisionCode'];
				$divs = explode(",", $div . ",");
				$Owndiv = (string) $divs[0];
                $sfCode = $_GET['sfCode'];
				//$data = json_decode($_POST['data'], true);
              //  $date = $data['date'];
				
				$query = "select State_Code  from Mas_Stockist where Stockist_Code='".$sfCode."'";
                $head = performQuery($query);
				$State_Code=$head[0]["State_Code"];
                $query = "select State_Code, Division_Code,Distributor_Price,Retailor_Price,MRP_Price,Product_Detail_Code from vwProductStateRates where Division_code='" . $Owndiv . "' and State_Code=$State_Code";
// $query = "select Product_Detail_Code from Mas_Product_Detail where Division_Code='" . $Owndiv . "' and Product_Active_Flag='0' and CHARINDEX(','+'".$State_Code."'+',' ,','+cast(State_Code as varchar)+',')>0";


                $results = performQuery($query);

				$retQuery = "select Price_List_Name,cat_id,Dist_name from Retailer_cat_Rate where  Dist_name ='". $sfCode ."' and cat_id is not null ";


                $retList =performQuery($retQuery);

            for($i=0;$i<count($results);$i++){
                $productCode = $results[$i]['Product_Detail_Code'];


                    for($j=0; $j<count($retList); $j++)
                    {
						$MRP_Price=0;
                        $Rate_in_piece=0;
						$priceListCode = $retList[$j]['Price_List_Name'];
                        if($priceListCode!=null && $priceListCode!=''){
                            $retRateQuery = "select pwd.Rate_in_piece,pwd.MRP_Price, pwd.Product_Code from Mas_Product_Wise_Bulk_rate_head pwh inner join Mas_Product_Wise_Bulk_rate_details pwd on pwh.Price_list_Sl_No = pwd.Price_list_Sl_No where pwd.Product_Code ='". $productCode ."' and pwh.Price_list_Sl_No ='". $priceListCode ."' and (convert(varchar,getdate(),23) >= Effective_From_Date and convert(varchar,getdate(),23)<=Effective_To_Date)";

                            $retRate = performQuery($retRateQuery);
							if(count($retRate)>0){
							
                                            if($retRate[0]['Rate_in_piece']!=null && $retRate[0]['Rate_in_piece'] !=''){
                                           $Rate_in_piece = $retRate[0]['Rate_in_piece'];
							              }
										  
										   if($retRate[0]['MRP_Price']!=null && $retRate[0]['MRP_Price'] !=''){
                                             $MRP_Price = $retRate[0]['MRP_Price'];
							              }



 
							}
                        }
							
                                          $retList[$j]['Rate_in_piece'] = $Rate_in_piece;
                                         
                                          $retList[$j]['MRP_Price'] = $MRP_Price;
                    }

                    $results[$i]['retList'] = $retList;

               }
				outputJSON($results);
                break;

   case "dcr/save":
        addEntry();
        break;

}
?>