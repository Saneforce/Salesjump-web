<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
//session_start();
date_default_timezone_set("Asia/Kolkata");

$URL_BASE = "/";

include "dbConn.php";
include "utils.php";

function sendFCM_notify($registrationIds,$message,$title,$msgId,$event_id){
	// API access key from Google API's Console
	//define('FIREBASE_API_KEY', 'AAAAFZqP7mw:APA91bG9q_tSrr3sjaHiMo8fqBI18z8z3KWxsmFeL3L_9AMuLnUoT3kRsmTH2DmvRHZrqSJn9nfVqQZGPmYvG4Z6skH304xWp2Wa7kz_jTVIsGI63t4PwZxtHjbeH-sqXMESogFb5QJ_');
	define('FIREBASE_API_KEY','AAAAFZqP7mw:APA91bH_9ZhbM_DuW7zgK8dLwMeOZe8dbPeWeeJtbC7e_5IoTA65QDsr0UQyj67PM0NnvHJ9mKBqUBNzmCO4grg1XbpAO7sznzXC7DUWFXQKBfzqMhDpliU_A2odMPSbFn6W7dE_BQ-h');
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

function getLogin(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$mob=(string) $data[0]["mobile"];
	$deviceid=(string) $data[0]["deviceid"];
	$sql="select ListedDrCode CusID,ListedDr_Name CusName,ListedDr_Address1 Addr,ListedDr_Mobile Mob,l.Dist_name StkID,Stockist_Name StkNm,Stockist_Mobile StkMob,l.Division_Code Div,ERP_Code,Stockist_Address StkAddr from Mas_Listeddr l inner join Mas_Stockist s on l.Dist_name=s.Stockist_Code where ListedDr_Mobile='$mob'";
	$res=performQuery($sql);
	
	if($deviceid!=""){
		$sql="select count(CustID) Cnt from Mas_CusFCMToken where CustID='" . $res[0]["CusID"] . "'";
		$darr=performQuery($sql);
		if($darr[0]["Cnt"]>0){
		$sql="update Mas_CusFCMToken set device_id='" . $deviceid . "'  where CustID='" . $res[0]["CusID"] . "'";performQuery($sql);
		}else{
		$sql="insert into Mas_CusFCMToken select '" . $res[0]["CusID"] . "','" . $deviceid . "'";performQuery($sql);
		}
	}
	$result=[];
	if(count($res)>0){
		$result["success"]=true;
		$result["result"]=$res;
		$result["msg"]="";
	}else{
		$result["success"]=false;
		$result["result"]=[];
		$result["msg"]="The entered mobile number does not match with our records. Please check your Franchise/Sales Executive.";
	}
	return $result;
}
function sendSMS() {
    $otp = rand(100000, 999999);
    $mobileNumber = $_GET['mobile'];

    $curl = curl_init();
    //$txt = urlencode("OTP for Retailer app login is " . $otp . "");
$txt = urlencode("Your DCR has been submitted for " . $otp . " -EFORCE");
    $url = "http://bulk.smswave.in/http-api.php?username=SANEFO&password=123123&senderid=EFORCE&route=1&number=" . $mobileNumber . "&message=" . $txt;
    curl_setopt($curl, CURLOPT_URL, $url);
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
    $result = curl_exec($curl);

    $res['success'] = true;
    $res['otp'] = $otp;
    $res['mobile'] = $mobileNumber;
    $res['result'] = $result;
    $res['msg'] = "otp sent successfully";
    curl_close($curl);
    return $res;
}
function getOfferNotify(){
    $sfCode = $_GET['CusCode'];
    $query = "select 'http://hap.sanfmcg.com/offerimg/'+offerimg offerimg from Mas_Retail_AppOffers where Eff_from<='".date("Y-m-d 00:00:00")."' and isnull(Eff_to,getdate())>='".date("Y-m-d 00:00:00")."'";
    return performQuery($query);
}
function GetNotifyLists(){
    $sfCode = $_GET['CusCode'];
    $query = "exec getCusAppNotifyLists '" . $sfCode . "'";
    return performQuery($query);
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

function getProdTypList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data["SF"];
	$Stk=$data["Stk"];
	$sql = "exec getRetProductTypeList '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}

function getProdCateList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$Stk=$data["Stk"];
	$sql = "exec GetProdRetailBrand_App '".$Stk."'";
	$res=performQuery($sql);
	return $res;
}

function getProdDetList(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$Stk=$data["Stk"];
	$outletID=$data["CusID"];
	//$sql = "exec getProductAppforOutlet '".$Stk."','".$outletID."'";
	//$sql = "exec getProductAppforOutletOrder '".$Stk."','".$outletID."'";
	$sql = "exec getAppProdNative '$Stk'";

	$res=performQuery($sql);
	$sql = "select * from vwMas_MUnitDets WITH (NOLOCK) where Division_Code=227 and UOM_Nm!='CRT'";
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

function getOrderAndInvoice(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$RetCode=$data["RetailId"];
	$fdt=$data["fdt"];
	$tdt=$data["tdt"];
	
	$sql = "select h.Trans_Sl_No OrderID,dr.ListedDr_Name OutletName,CONVERT(varchar,h.Order_Date,103)[Date],h.Order_Value,
			case when h.Order_Flag=1 then 'Completed' when h.Order_Flag=0 then 'Pending' else '' end [Status] from Trans_Order_Head h WITH (NOLOCK) 
			left join Mas_ListedDr dr WITH (NOLOCK) on h.Cust_Code=CAST(dr.ListedDrCode as varchar)
			where CONVERT(date,h.Order_Date) between '".$fdt."' and '".$tdt."' and h.Order_Flag in (0,1) and h.Cust_Code='".$RetCode."'
			order by Order_Date";
	$Orders=performQuery($sql);
	$sql = "select h.Trans_Sl_No OrderID,d.Product_Code,d.Product_Name,d.Quantity from Trans_Order_Head h WITH (NOLOCK)
			inner join Trans_Order_Details d WITH (NOLOCK) on h.Trans_Sl_No=d.Trans_Sl_No
			where CONVERT(date,h.Order_Date) between '".$fdt."' and '".$tdt."' and h.Order_Flag in (0,1) and h.Cust_Code='".$RetCode."'
			order by Order_Date";
	$OrdersDetails=performQuery($sql);
	
	$sql = "select '' OrderID,ListedDr_Name OutletName,CONVERT(varchar,Activity_Date,103)[Date],td.POB_Value Order_Value,'No Order' [Status],'' Details from DCRMain_Trans th WITH (NOLOCK)
			inner join DCRDetail_Lst_Trans td WITH (NOLOCK) on th.Trans_SlNo=td.Trans_SlNo 
			inner join Mas_ListedDr d WITH (NOLOCK) on d.ListedDrCode=td.Trans_Detail_Info_Code
			where CONVERT(date,Activity_Date) between '".$fdt."' and '".$tdt."' and POB_Value<=0 and td.Trans_Detail_Info_Code='".$RetCode."'
			order by Activity_Date";
	$NoOrders=performQuery($sql);
	
	$sql = "select h.Trans_Inv_Slno InvoiceID,h.Order_No OrderID,dr.ListedDr_Name,dr.Code,CONVERT(varchar,h.Invoice_Date,103)[Date],Dis_Code Stockist_Code,
			h.Total Order_Value,'Completed' [Status] from Trans_Invoice_Head h  WITH (NOLOCK)
			inner join Mas_ListedDr dr WITH (NOLOCK) on h.Cus_Code=CAST(dr.ListedDrCode as varchar)
			where h.Invoice_Date>='".$fdt."' and h.Invoice_Date<DATEADD(DAY,1,'".$tdt."')  and h.Cus_Code='".$RetCode."'
			order by Invoice_Date";
	$Invoice=performQuery($sql);
	
	$sql = "select  h.Trans_Inv_Slno InvoiceID,d.Product_Code,d.Product_Name,d.Quantity from Trans_Invoice_Head h  WITH (NOLOCK)
			inner join Trans_Invoice_Details d WITH (NOLOCK) on d.Trans_Inv_Slno=h.Trans_Inv_Slno
			where h.Invoice_Date>='".$fdt."' and h.Invoice_Date<DATEADD(DAY,1,'".$tdt."')  and h.Cus_Code='".$RetCode."'
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

function getOutletwiseLedger(){
	global $data;
	$data = json_decode($_POST['data'], true);
	$SF=$data['SF'];
	$Stk=$data['Stk'];
	$FDT=$data['FDT'];
	$TDT=$data['TDT'];
	$result=[];
	$qry="exec GetCustomerwisePayLedger '$Stk','".$FDT."','".$TDT."','".$SF."'";
	$res = performQuery($qry);	
	$custCode="";
	for($il=0;$il<count($res);$il++){
		if($custCode!=$res[$il]["Cust_Code"]){
			$item=[];
			$custCode=$SF;
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
	$sql = "exec svDCRMain_RetailApp '".$DCRHead["SF"]."','".$DCRHead["dcr_activity_date"]."',".$DCRHead["Worktype_code"].",'".$DCRHead["Town_code"]."','',''";
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
		$sql = "exec [svSecOrder_NApp]  '".$DCRHead["SF"]."','".$DCRDetails["doctor_code"]."','".$DCRDetails["stockist_code"]."','".$DCRHead["Town_code"]."','0','".$DCRDetails["Doc_Meet_Time"]."','".$DCRDetails["NetAmount"]."',0,0,'',0,0,'1','".$ACd."','".$divisionCode."','".$PPXML."','1','".$subtot."','".$distot."','".$taxtot."','".$DCRDetails["No_Of_items"]."','".$TXXML."','".$DCRHead["billingAddress"]."','".$DCRHead["shippingAddress"]."'";
		$result["Qry3"]=$sql;
		$res=performQuery($sql); 
		$result["invoice"]=$res[0]['Trans_Sl_No'];
		
		$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where SF_code='MGR5256' or sf_code in(select SF_Code from Customer_Hierarchy_Details where Dist_Code='" . $DCRDetails["stockist_code"] . "')";
		$sql = "SELECT DeviceRegId FROM Access_Table WITH (NOLOCK) where SF_code='" . $DCRDetails["stockist_code"] . "'";
		//$sql = "select device_id DeviceRegId  from  Mas_CusFCMToken where CustID='".$DCRDetails["doctor_code"]."'";
		$device = performQuery($sql);
		$result["Qry4"]=$sql;
		for($ia=0;$ia<count($device);$ia++){
			$reg_id = $device[$ia]['DeviceRegId'];
			if (!empty($reg_id)) {
				$result["reg_id"]= $reg_id;
				$result["msgnoti"]= "You have receive the order from ".str_replace("'","''",$DCRDetails["doctor_name"]);
				sendFCM_notify($reg_id,"You have receive the order from ".str_replace("'","''",$DCRDetails["doctor_name"]), "Sales Order",0,'#sign-in');
				
			}
		}

	}
	$result["success"]=true;
	$result["Msg"]="Submitted Successfully";
	return $result;
}


$axn = $_GET['axn'];
$value = explode(":", $axn);
switch (strtolower($value[0])) {
    case "get/login":
        outputJSON(getLogin());
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
    case "get/proddets":
        outputJSON(getProdDetList());
        break;
    case "get/notify":
        outputJSON(GetNotifyLists());
        break;
    case "get/offernotify":
        outputJSON(getOfferNotify());
        break;
	case "get/outletwiseledger":
		outputJSON(getOutletwiseLedger());
		break;
	case "get/orderandinvoice":
		outputJSON(getOrderAndInvoice());
		break;
    case "save/salescalls":
        outputJSON(SaveDCRCalls());
        break;
		
	case "get/orderlst":
		$Sf_code = $_GET['sfCode'];
		$fromdate = $_GET['fromdate'];
		$todate = $_GET['todate'];	
		$distributorid = $_GET['distributorId'];	
		
		$sql = "exec Get_Ret_Total_Orders '".$Sf_code."','".$fromdate."','".$todate."' ";
		$resord = performQuery($sql);
		$sql = "exec Get_Ret_Total_Invoices '".$Sf_code."','".$fromdate."','".$todate."' ";
		$resinv = performQuery($sql);
		$results = array_merge($resord, $resinv);
		outputJSON($results);
	break;
	case "get/orderdet":
		$Sf_code = $_GET['sfCode'];
		$fromdate = $_GET['fromdate'];
		$todate = $_GET['todate'];
		$orderID = $_GET['orderID'];
		$txtotal = 0; 
		 
		$sql = "exec Get_Order_DetailsFF '".$Sf_code."','".$fromdate."','".$todate."','".$orderID."'";
		$results = performQuery($sql);
		
		$sql = "exec getOrderINVTaxDetails '".$Sf_code."','".$fromdate."','".$todate."','".$orderID."'";
		$taxresult = performQuery($sql);
		
		for($i=0;$i<count($results);$i++){
			$txtotal=$txtotal+$results[$i]["Tax_value"];
			$pid=$results[$i]["Product_Code"];
			$OrderNo=$results[$i]["Trans_Order_No"];
			$new_array = array_values(array_filter($taxresult, function($obj)  use ($pid,$OrderNo){
						if(($obj["Product_Code"] === $pid) && ($obj["Trans_Order_No"] === $OrderNo)) return true;
					}));
			$results[$i]["TAX_details"]=$new_array;
		}
		outputJSON($results);
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
    	case "send/sms":
        	outputJSON(sendSMS());
        	break;


	case "get_ret_addresses":
		$listedDrCode = $_GET['listedDrCode'];
		$id = $_GET['id'];
		$results = performQuery("select * from Mas_RetDeliveryAddress where ListedDrCode = '$listedDrCode' and flag = '0'");
		$res["response"] = $results;
		$res["success"] = true;
		outputJSON($res);
		break;

	case "insert_ret_address":

		$listedDrCode = $_GET['listedDrCode'];
		$address = $_GET['address'];

		$stateCode = $_GET['stateCode'];
		$stateName = $_GET['stateName'];
		$divCode = $_GET['divCode'];

		$results = performQuery("insert into Mas_RetDeliveryAddress (ListedDrCode, Address, CrDate, ModDate, flag, Division_code, State_Code, State_Name) values ('$listedDrCode', '$address', GETDATE(), null, 0, '$divCode', '$stateCode', '$stateName')");
		$res["success"] = true;

		$lastAdded = performQuery("select top 1 * from Mas_RetDeliveryAddress where ListedDrCode = '$listedDrCode' order by id desc");
		$res["response"] = $lastAdded[0];
		outputJSON($res);
		break;

	case "update_ret_address":
		$listedDrCode = $_GET['listedDrCode'];
		$address = $_GET['address'];
		$id = $_GET['id'];

		$stateCode = $_GET['stateCode'];
		$stateName = $_GET['stateName'];
		$divCode = $_GET['divCode'];

		$results = performQuery("update Mas_RetDeliveryAddress set Address = '$address', ModDate = GETDATE(), Division_code = '$divCode', State_Code = '$stateCode', State_Name = '$stateName' where ListedDrCode = '$listedDrCode' and id = '$id'");
		$res["success"] = true;
		outputJSON($res);
		break;

	case "delete_ret_address":
		$listedDrCode = $_GET['listedDrCode'];
		$id = $_GET['id'];
		$results = performQuery("delete Mas_RetDeliveryAddress where ListedDrCode = '$listedDrCode' and id = '$id'");
		$res["success"] = true;
		outputJSON($res);
		break;

	case "save_payment_info":
		$tid = $_GET['tid'];
		$orderValue = $_GET['orderValue'];
		$Trans_Sl_No = $_GET['Trans_Sl_No'];
		$uniqueKey = $_GET['uniqueKey'];
		$appAccessToken = $_GET['appAccessToken'];
		$appIdToken = $_GET['appIdToken'];

		$results = performQuery("insert into JioMoneyTxns (tid, Trans_Sl_No, uniqueKey, appAccessToken, appIdToken, crDate, totalValue) values ('$tid', '$Trans_Sl_No', '$uniqueKey', '$appAccessToken', '$appIdToken', getdate(), '$orderValue')");
		$res["success"] = true;
		outputJSON($res);
		break;
		
	case "get_jio_money_credentials":
		$results = performQuery("select top 1 * from Mas_JioMoneyCredentials");
		if(count($results) > 0) {
			$res["success"] = true;
			$res["response"] = $results[0];
		} else {
			$res["success"] = false;
			$res["response"] = [];
		}
		outputJSON($res);
		break;
		
	case "get_ccavenue_credentials":
		$results = performQuery("select top 1 * from Mas_CCAvenueCredentials");
		if(count($results) > 0) {
			$res["success"] = true;
			$res["response"] = $results[0];
		} else {
			$res["success"] = false;
			$res["response"] = [];
		}
		outputJSON($res);
		break;
		
	case "get_payment_method":
		$sql = "select top 1 PaymentGateway from Access_Master";
		$res = performQuery($sql);
		if(count($res)>0){
			$result["success"] = true;
			$result["response"] = $res[0];
			$result["msg"] = "";
		}
		else{
			$result["success"] = false;
			$result["response"] = [];
			$result["msg"] = "Payment Method not set";
		}
		outputJSON($result);
		break;
		
	case "update_jiomoney_payment_status":
		$tid = $_GET['tid'];
		$transId = $_GET['transId'];
		$status = $_GET['status'];
		performQuery("update JioMoneyTxns set tidResponse = '$transId', status = '$status', updatedDate = GETDATE() where tid = '$tid'");
		if($status === "SUCCESS") {
			$invoice = performQuery("select top 1 Trans_Sl_No from JioMoneyTxns where tid = '$tid'")[0]['Trans_Sl_No'];
			performQuery("update Trans_Order_Head set isPaid = 'paid' where Trans_Sl_No = '$invoice'");
		}
		$res["success"] = true;
		$res["msg"] = "Payment status updated successfully";
		outputJSON($res);
		break;
		
	case "submit_customer_complaint":
		$feedback = $_GET['feedback'];
		$delivery = $_GET['delivery'];
		$damage = $_GET['damage'];
		$issue = $_GET['issue'];

		$mail = "1. Is the delivery of the material as per your order? - '$delivery' 2. Is there any issue you would like to report? – '$issue' 3. Is there any Material Damage / Expired? - '$damage' 4. Feedback - '$feedback'";
		
		$res["success"] = true;
		$res["msg"] = $mail;
		outputJSON($res);
		break;
		
	case "get_ad_images":
		$result = performQuery("select Serial, concat('https://rad.salesjump.in/server/rad/', url)url from Mas_AppAdvertisement where flag = '0'");
		if (count($result) > 0) {
			$res["success"] = true;
			$res["response"] = $result;
		} else {
			$res["success"] = false;
			$res["response"] = [];
		}
		outputJSON($res);
		break;
		
	case "get_states":
		$results = performQuery("select convert(varchar, State_Code) id, StateName title from Mas_State where State_Active_Flag = '0' order by StateName asc");
		$res["response"] = $results;
		$res["success"] = true;
		outputJSON($res);
		break;
}
?>