<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
//session_start();
date_default_timezone_set("Asia/Kolkata");

//include "dbConn_new.php";
include "dbConn.php";
include "utils.php";
 
// $query = "insert into reqDetails select 'DBnAct','".$_GET["axn"]."',''"; 
// performQuery($query);
$data = json_decode($_POST['data'], true);

function getDynamicView(){
	global $data;	
	$slno = (string) $data['slno'];	
	$arrfield=array();
	 $query1 = "select * from Mas_Forms where Frm_ID='".$slno."' and Active_Flag='0'";
	 $rests=performQuery($query1);
	$query = "select * from Mas_Forms_Fields where Frm_ID='".$slno."' and Active_Flag='0' ";	
	$res=performQuery($query);
	if (count($res)>0) 
	{
		for ($il=0;$il<count($res);$il++)
		{
			$id=$res[$il]["Control_id"];
			if($id=="4" || $id=="6" || $id=="13"){
				$tblclm=$res[$il]["Fld_Src_Field"];
				$defnam=explode(",",$tblclm);
				$qu = "select ".$defnam[0].",".$defnam[1]." from ".$res[$il]["Fld_Src_Name"]." ";	
				$res[$il]['inputss']=$qu;
				$res[$il]['header']=$rests[0]["Frm_Name"];
				$res[$il]['type']=$rests[0]["Targt_Frm"];
				$res[$il]['target']=$rests[0]["Frm_Type"];
				$res[$il]['input']=performQuery($qu);
				$res[$il]['code']=$defnam[0];
				$res[$il]['name']=$defnam[1];
			}
			else if($id=="5" || $id=="7" || $id=="20"){
				$tblclm=$res[$il]["Fld_ID"];
				$qu = "select Id,Template_Text from Mas_Custom_Temp where Fld_Id='".$tblclm."'";
				$res[$il]['inputss']=$qu;
				$res[$il]['input']=performQuery($qu);
				$res[$il]['code']="Id";
				$res[$il]['name']="Template_Text";
				$res[$il]['header']=$rests[0]["Frm_Name"];
				$res[$il]['type']=$rests[0]["Targt_Frm"];
				$res[$il]['target']=$rests[0]["Frm_Type"];
			}
			else if($id=="19"){
				$def=explode(",",$res[$il]["Fld_Src_Field"]);
				
				for($k=0;$k<sizeof($def);$k++){
   
				$pos=stripos($def[$k], ';');
				$arrfield[]=array(substr($def[$k],0,$pos));
				}
				$qu="select ";
				for($k=0;$k<sizeof($arrfield);$k++){
					
					if($k==sizeof($arrfield)-1)
						$qu=$qu.$arrfield[$k][0];
					else
					$qu=$qu.$arrfield[$k][0].",";
				}
				$qu=$qu." from ".$res[$il]["Fld_Src_Name"]." ";
				$res[$il]['inputss']=$qu;
				$res[$il]['input']=performQuery($qu);
				$res[$il]['header']=$rests[0]["Frm_Name"];
				$res[$il]['type']=$rests[0]["Targt_Frm"];
				$res[$il]['target']=$rests[0]["Frm_Type"];
				
				//echo $qu;
			} 
			else{
				$res[$il]['input']=array();
				$res[$il]['header']=$rests[0]["Frm_Name"];
				$res[$il]['type']=$rests[0]["Targt_Frm"];
				$res[$il]['target']=$rests[0]["Frm_Type"];
			}
		}
		
	}
	
	return outputJSON($res);
}
function getMenu(){
	global $data;
	$div = (string) $data['div'];	
	$query = "select * from Mas_Forms WITH (NOLOCK) where Division_Code='".$div."' and Active_Flag='0' and isChild ='N' ";	
	$res=performQuery($query);
	return outputJSON($res);
}
function saveEditedMot(){
	global $data;
	$sl_no=$data["sl_no"];
	$mot=$data["mot"];
	$motnm=$data["motnm"];
	$sfCode=$data["sfCode"];
	$drvAllw=$data["driverAllow"];
	$startKm=$data["startKm"];
	$endKm=$data["endKm"];
	$personalKm=$data["personalKm"];
	$sql="update Expense_Start_Activity set LastUpdt_Dt=getDate(),MOT='".$mot."',MOT_Name='".$motnm."',Start_Km='".$startKm."',End_Km='".$endKm."',Personal_Km='".$personalKm."',driverAllowance='".$drvAllw."' where Sl_No=$sl_no and Sf_code='".$sfCode."'";
	performQuery($sql);
	$resp["success"] = true;
	$resp["Query"] =$sql;
	return $resp;
}
function Upload_Image($Stat){
	global $data;
    global $URL_BASE;
if($Stat==0){
	$target_dir = "..\TAphotos/";
}else if ($Stat==1){
	$target_dir = "..\BikePhotos/";
}else{
	$Head_Travel=(string)$_GET['HeadTravel'];
	$Mode=(string)$_GET['Mode'];
	$Date=(string) $_GET['Date'];
	$SF_Code=(string) $_GET['sfCode'];
	$From=(string) $_GET['From'];
	$To=(string) $_GET['To'];
	$U_key=(string) $_GET['U_key'];
	$Img_U_key=(string) $_GET['Img_U_key'];
	$target_dir = "..\TAphotos/";
	$target_file_name = $target_dir .basename($_FILES["file"]["name"]);
	$target_file = basename($_FILES["file"]["name"]);
	/*$ressql="select Activity_Report_Code from Activity_Event_Captures where Activity_Report_Code='".$SF_Code."' and title='".$Head_Travel."' and Identification='".$Mode."' and convert(date,Insert_Date_Time)='".$Date."'";
	$resAlready=performQuery($ressql);
	if(count($resAlready)>0){
		$sql="delete from Activity_Event_Captures where Activity_Report_Code='".$SF_Code."' and title='".$Head_Travel."' and Identification='".$Mode."' and convert(date,Insert_Date_Time)='".$Date."'";
		performQuery($sql);
	}*/
	
	$sql="insert into Activity_Event_Captures (Activity_Report_Code,title,Identification,imgurl,remarks,DCRDetNo,Insert_Date_Time,lat,lon) values('".$SF_Code."','".$Head_Travel."','".$Mode."','".$target_file."','".$From."','".$To."','".$Date."','".$U_key."','".$Img_U_key."')";
	performQuery($sql);

}
	
$target_file_name = $target_dir .basename($_FILES["file"]["name"]);
$target_file = basename($_FILES["file"]["name"]);
$response = array();
//echo "file are here".$target_file_name;

/* $data = json_decode($_POST['data'], true);
$json = json_decode($data, true); */
 $upload_url;

if (isset($_FILES["file"])) 
{
	//echo $_FILES["file"]["tmp_name"];
 if (move_uploaded_file($_FILES["file"]["tmp_name"], $target_file_name)) 
 {
  $success = true;
  
  $upload_url = $target_file;
  
 // $UpdateMyAccount = "update Mas_Empolyee set File_Path='". $upload_url."',Emp_Name='".$Username."' ,Emp_Email='".$Email."',Emp_Mobile='".$Mobile."',Emp_DOB='".$DOB."',Emp_ContactAdd_One='".$Address."'    where Emp_ID='" . $Emp_ID . "'";
    
   // $Pass = performQuery($UpdateMyAccount);
  
  $message = "Profile Has Been Updated" ;
  
  
 
 }
 else 
 {
  $success = false;
  $message = "Error while uploading";
 }
 unlink($_FILES["file"]["tmp_name"]);
}
else 
{
	  $message =  "Profile Has Been Updated";

	/* $UpdateMyAccount = "update Mas_Empolyee set Emp_Name='".$Username."' ,Emp_Email='".$Email."',Emp_Mobile='".$Mobile."',Emp_DOB='".$DOB."',Emp_ContactAdd_One='".$Address."'    where Emp_ID='" . $Emp_ID . "'";
    
    $Pass = performQuery($UpdateMyAccount);
	 */
 $success = true;
 
}
 
CloseConn();
$response["success"] = $success;
$response["message"] = $message;
$response["url"] = $upload_url;
  return $response;
}
function Get_TAImage(){
	$Head_Travel=(string)$_GET['HeadTravel'];
	$Mode=(string)$_GET['Mode'];
	$Date=(string) $_GET['Date'];
	$SF_Code=(string) $_GET['sfCode'];
	$U_key=(string) $_GET['U_key'];
	$query = "select *,isnull(('https://checkin.hap.in/TAphotos/'+imgurl),'') Imageurl,lon Img_U_key  from Activity_Event_Captures WITH (NOLOCK) where title='".$Head_Travel."'  and Identification ='".$Mode."'  and Insert_Date_Time='".$Date."' and lat='".$U_key."' and Activity_Report_Code='".$SF_Code."'";	
	return performQuery($query);
}
function insertExpenseLodgeException(){	
	global $data;
	$sfCode=(string) $data["sfCode"];
	$Div= explode(",", $data["division_code"]);
	$Owndiv=(string) $Div[0];
	$DA_Type=(string) $data['da_type'];
	$DA_Date=(string) $data['da_date'];
	$DA_Actual_Time=(string) $data['da_actua_time'];
	$DA_Early_Time=(string) $data['da_early_time'];
	$DA_Amt=(string) $data['da_amt'];
	$FMOT=(string) $data['FMOT'];
	$TMOT=(string) $data['TMOT'];
	$FMOTNm=(string) $data['FMOTName'];
	$TMOTNm=(string) $data['TMOTName'];
	$UKey="";
	$sql = "select * from Expense_Lodge_Exception WITH (NOLOCK) where Sf_Code='$sfCode' and convert(date,DA_Date)=cast('$DA_Date' as date) and DA_Type='$DA_Type' and Approval_Flag<>2";
	$Expex=performQuery($sql);
	if(count($Expex)<1){
	$sql = "{call sv_Exp_Lodge_Exception(?,?,?,?,?,?,?,?,?,?,?,?)}";
	$params1 = array(
					array($sfCode, SQLSRV_PARAM_IN),
					array($Owndiv, SQLSRV_PARAM_IN),
					array($DA_Type, SQLSRV_PARAM_IN),
					array($DA_Date, SQLSRV_PARAM_IN),
					array($DA_Actual_Time, SQLSRV_PARAM_IN),
					array($DA_Early_Time, SQLSRV_PARAM_IN),
					array($DA_Amt, SQLSRV_PARAM_IN),
					array($UKey, SQLSRV_PARAM_IN),
					array($FMOT,SQLSRV_PARAM_IN),
					array($TMOT,SQLSRV_PARAM_IN),
					array($FMOTNm,SQLSRV_PARAM_IN),
					array($TMOTNm,SQLSRV_PARAM_IN)
				);
	
	$res["success"]=performQueryWP($sql, $params1);
	}
	else{
	$res["success"]="Expense Exception Already Submitted for this Date";	
	}
	return $res;
}
function Get_WorktypeFields($worktypecode,$sfCode){
	
	$sql="select * from mas_worktype_fields WITH (NOLOCK)  where charindex(cast(Fld_ID as varchar),','+'".$worktypecode."'+',')>0  and Active_flag='0'";
	
	$res=performQuery($sql);
	 if (count($res)>0) 
	{
		for ($il=0;$il<count($res);$il++)
		{
			$tblclm=$res[$il]["Fld_Src_Name"];
			$id=$res[$il]["Control_id"];
			//echo $id;
				$tblclmname=$res[$il]["Fld_Src_Field"];
				$defnam=explode(",",$tblclmname);
				$qucol="";
				for($k=0;$k<sizeof($defnam);$k++){
					if($qucol!="") $qucol = $qucol.",";
					$qucol = $qucol.$defnam[$k];
				}
		$res[$il]['inputs']=[];
		  if($id=='10' ||  $id=='17' ||  $id=='7'){
			if($res[$il]["View_master"]=="1"){
				if($res[$il]["Fld_Symbol"]=="R"){
					$query = "select   ".$qucol.",$tblclmname from ".$tblclm."  WHERE charindex('$sfCode',','+Field_Code+',')>0";
				}else{
					$query = "select   ".$qucol.",$tblclmname from ".$tblclm."  WHERE  SF_Code='$sfCode'";
				}  
			  }else{
			 $query = "select   ".$qucol.",$tblclmname from ".$tblclm." ";	
			  }
			
			$res[$il]['inputs']=(performQuery($query)==null?[]:performQuery($query));
			} 
			
		}
	} 
	return $res;
}
function Delete_TAImage(){
	$ImageName=(string)$_GET['Img_U_key'];
	$Ukey=(string)$_GET['U_key'];
	$Date=(string) $_GET['Date'];
	$sfcode=(string)$_GET['sfCode'];
	$ssql="exec DeleteTABillImage '".$sfcode."','".$Date."','".$Ukey."'";
	performQuery($ssql);
	$sql="delete from Activity_Event_Captures  where convert(date,Insert_Date_Time)='".$Date."' and lon='".$ImageName."'";
	performQuery($sql);
	$response["success"] = true;
	$response["Qry"] = $ssql;
	return $response;
}
function UpdateProcPic(){
    global $URL_BASE;

$target_dir = "..\Proc_Photos/";
$target_file_name = $target_dir .basename($_FILES["file"]["name"]);
$target_file = basename($_FILES["file"]["name"]);
$response = array();

 $upload_url;

if (isset($_FILES["file"])) 
{
	//echo $_FILES["file"]["tmp_name"];
 if (move_uploaded_file($_FILES["file"]["tmp_name"], $target_file_name)) 
 {
  $success = true;
  
  $upload_url = $target_file;
  
 // $UpdateMyAccount = "update Mas_Empolyee set File_Path='". $upload_url."',Emp_Name='".$Username."' ,Emp_Email='".$Email."',Emp_Mobile='".$Mobile."',Emp_DOB='".$DOB."',Emp_ContactAdd_One='".$Address."'    where Emp_ID='" . $Emp_ID . "'";
    
   // $Pass = performQuery($UpdateMyAccount);
  
  $message = "Profile Has Been Updated" ;
 }
 else 
 {
  $success = false;
  $message = "Error while uploading";
 }
 unlink($_FILES["file"]["tmp_name"]);
}
else 
{
	  $message =  "Profile Has Been Updated";

	/* $UpdateMyAccount = "update Mas_Empolyee set Emp_Name='".$Username."' ,Emp_Email='".$Email."',Emp_Mobile='".$Mobile."',Emp_DOB='".$DOB."',Emp_ContactAdd_One='".$Address."'    where Emp_ID='" . $Emp_ID . "'";
    
    $Pass = performQuery($UpdateMyAccount); 
	 */
 $success = true;
 
}
 CloseConn();
$response["success"] = $success;
$response["message"] = $message;
$response["url"] = $upload_url;
  return $response;
}
function SaveCheckinImage(){
    global $URL_BASE;

	$SF=(string)$_GET['sfCode'];
	$fileName=(string) $_GET['FileName'];
	$mode=(string) $_GET['Mode'];
	$target_file = $fileName;
	if($fileName==""){		
		$response["Qry"] = "";
		$response["success"] = true;
		$response["message"] = "";
		$response["url"] = "";
		return $response;
		die;
	}
	if(stripos($mode,"ExpClaim1")>-1){
		$target_dir = "../TAphotos1/";
	}
	if(stripos($mode,"ExpClaim")>-1){
		$target_dir = "../TAphotos/";
	}
	else if ($mode=="Travel")
		$target_dir = "..\BikePhotos/";
	else if($mode=="PROF")
		$target_dir = "..\SalesForce_Profile_Img/";
	else if(strtolower($mode)=="outlet")
		$target_dir = "..\Outlet_Images/";
	else if(strtolower($mode)=="outlet_close")
		$target_dir = "..\Closed_Outlets/";
	else if(strtolower($mode)=="qps")
		$target_dir = "..\QPS_Images/";
	else if(strtolower($mode)=="pop")
		$target_dir = "..\POP_Images/";
	else if(strtolower($mode)=="ob")
		$target_dir = "..\OtherBrandImg/";
	else if(strtolower($mode)=="salesreturnimg")
		$target_dir = "..\StockReturnImages/";	
	else if(strtolower($mode)=="freezer")
		$target_dir = "..\FreezerImages/";
	else if(strtolower($mode)=="cooler")
		$target_dir = "..\CoolerInfo/";
	else{
		$target_dir = "..\Photos/";
		$target_file = $SF."_".$fileName;
		
	}
$target_file_name = $target_dir . $target_file;  //.basename($_FILES["file"]["name"]);
$response = array();
$Qry="";
if (isset($_FILES["file"])) 
{
	//echo $_FILES["file"]["tmp_name"];
 if (move_uploaded_file($_FILES["file"]["tmp_name"], $target_file_name)) 
 {
  $success = true;
  
  if(stripos($mode,"ExpClaim")>-1)
  {
	$sval=explode(";",$mode);
	
	$Qry="insert into Activity_Event_Captures (Activity_Report_Code,title,Identification,imgurl,remarks,DCRDetNo,Insert_Date_Time,lat,lon) 
	values('".$SF."','".$sval[1]."','".$sval[4]."','".$target_file."','','','".$sval[2]."','".$sval[3]."','".$sval[5]."')";
	performQuery($Qry);
	
/*			$myfile = fopen("../server/elog/errlog_".date('Y_m_d_H_i_s').".txt", "a+");
			$sqlsp=$Qry;
			fwrite($myfile, $sqlsp);
			fclose($myfile);*/
  }
  if($mode=="PROF"){
	  $Qry="update Mas_salesforce set Profile_Pic='".$target_file ."' where Sf_Code='".$SF."'";
	  performQuery($Qry);
  }
  $message = "Profile Has Been Updated" ;
 }
 else 
 {
  $success = false;
  $message = "Error while uploading";
 }
 unlink($_FILES["file"]["tmp_name"]);
}
else 
{
	$message =  "Profile Has Been Updated";
	$success = true;
}
 CloseConn();
$response["Qry"] = $Qry;
$response["success"] = $success;
$response["message"] = $message;
$response["url"] = $upload_url;
  return $response;
}
function saveView(){
	global $data;
	//echo count($data);
	$restt=array();
	$restt["val"]=$data;
	$tot=count($data);
	$restt["tot"]=$tot;
	$fk="";
	$pk="";
	for($k=0; $k<count($data) ; $k++){
	foreach($data[$k] as $key => $val) {
      // echo $key;
	   $query = "select Frm_Table from Mas_Forms where Frm_Name='".$key."'";
	   //echo $query;
	   $restt["tab"]=$query;
	   $res=performQuery($query);
	   //echo $res[0]["Frm_Table"];
	   $qu="(";
	   $vals=" select ";
	   //print_r($data[$k]);
	   foreach($val as $key => $val){
		   //echo $key . ': ' . $val;
		   if($key=="ctrl"){
			  
			  for($kk=0; $kk<count($val) ; $kk++){
				 // echo " ***** ".$val[$kk]["col"];
				  $qu=$qu.$val[$kk]["col"].",";
				$vals=$vals."'".$val[$kk]["value"]."',";
			  }
		   }
		   else{
		   $qu=$qu.$key.",";
		   
		   if($key=="PK_ID"){
			   $pk=$val;
		   }
		   if($k!=0 &&($key=="FK_ID")){
			   $vals=$vals."'".$fk."',";
			   $fk=$pk;
		   }
		   else{
			   if($k==0){
				   $fk=$pk;
			   }
			  $vals=$vals."'".$val."',"; 
		   }
		   
		   }
		   
	   }
	   if($k==0){
		   $qu=substr($qu,0,strlen($qu)-7);
		   $vals=substr($vals, 0, strlen($vals)-4);
	   }
	   else{
	   $qu=substr($qu, 0, -1);
	   $vals=substr($vals, 0, -1);
	   }
	   $query = "insert into " .$res[0]["Frm_Table"].$qu.")".$vals;
		$rest=performQuery($query);
	   $restt["hello"]="echo";
	   //array_push($restt,$resttt);
	  
	
   }
  
}
$restt["success"]="true";
return outputJSON($restt);

	
}
function saveAllowance($Stat){
	global $data;
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
	$sql;
	if($Stat==0){
		$sql = "SELECT max(Sl_No)+1 as Sl_No FROM Expense_Start_Activity";
		$topr = performQuery($sql);
		$pk =  (int)$topr[0]['Sl_No'];
		$sqldub ="select Sf_code from Expense_Start_Activity WITH (NOLOCK) where Sf_code='".$sfCode."' and convert(date,Date_Time)='".$Datetime ."' and Start_Km='".$km."'";
		$resAlready=performQuery($sqldub);
	/*if(count($resAlready)>0){
		$sql="delete from Expense_Start_Activity where  Sf_code='".$sfCode."' and convert(date,Date_Time)='".$Datetime ."'";
		performQuery($sql);
	}*/
		
		$sql = "insert into Expense_Start_Activity(Sf_code,Division_Code,Date_Time,Start_Km,Image_Url,Remarks,MOT,Approval_Flag,Sl_No,From_Place,To_Place,Fare,StEndNeed,dailyAllowance,driverAllowance,MOT_Name,To_Place_Id,Identification,ExpenseDate) values( '".$sfCode."','".$Owndiv."','".$Datetime."','".$km."','".$url."' ,'".$rmk."','".$mod."','1','" . $pk . "','".$from."','".$to."','".$fare."','".$StEndNeed."','".$dailyAllowance."','".$driverAllowance."' ,'".$MOT_Name."','".$to_code."',0,cast('".$Datetime."' as date))";
	}
	else if($Stat==3){
		
		$sql = "SELECT max(Sl_No)+1 as Sl_No FROM Expense_Start_Activity";
		$topr = performQuery($sql);
		$pk =  (int)$topr[0]['Sl_No'];
		$sql = "insert into Expense_Start_Activity(Sf_code,Division_Code,Date_Time,Start_Km,Image_Url,Remarks,MOT,Approval_Flag,Sl_No,From_Place,To_Place,Fare,StEndNeed,dailyAllowance,driverAllowance,MOT_Name,To_Place_Id,Identification,ExpenseDate) values( '".$sfCode."','".$Owndiv."','".$Datetime."','".$km."','".$url."' ,'".$rmk."','".$mod."','1','" . $pk . "','".$from."','".$to."','".$fare."','".$StEndNeed."','".$dailyAllowance."','".$driverAllowance."' ,'".$MOT_Name."','".$to_code."',1,cast('".$Datetime."' as date))";
	}
	else if($Stat==2){
		$sql = "update Expense_Start_Activity set Enddateand_time='".$Datetime."',End_Km='".$km."',Personal_Km='".$pkm."',End_Image_Url='".$url."',End_MOT='".$mod."',End_remarks='".str_replace("'","''",$rmk)."',To_Place='".$to."',To_Place_Id='".$to_code."' where sf_Code='".$sfCode."' and convert(date,Date_Time)=convert(date,'".$ActDate."') and End_Km is null";
	}
	else{
		$sql = "update Expense_Start_Activity set Enddateand_time='".$Datetime."',End_Km='".$km."',Personal_Km='".$pkm."',End_Image_Url='".$url."',End_MOT='".$mod."',End_remarks='".str_replace("'","''",$rmk)."',To_Place='".$to."',To_Place_Id='".$to_code."' where sf_Code='".$sfCode."' and convert(date,Date_Time)='".$Dateonly."' and End_Km is null";
	}
 performQuery($sql);

 /* $myfile = fopen("../server/newSAveExpense.txt", "a+");
        $txt = "Vasan\n";
        fwrite($myfile, $sql);
        fclose($myfile);
  */
 $resp["success"] = true;
 $resp["Query"] =$sql;
 return $resp;
}
function getTASummary(){
	global $data;
	$sfCode=(string) $data["sf"];
	$Ta_Date = (string) $data["Ta_Date"];
	//$sfCode=(string) $data['rSF'];
	//$sfCode=(string) $data['AMod'];
       
		$sql = "select  cast (convert(date,Date_Time) as varchar) Start_date,Date_Time Start_Time,Start_Km ,case when MOT=11 then 'BUS' when MOT=12 then 'Bike'  else 'No'  end Start_MOT,Enddateand_time End_Time,End_Km ,case when End_MOT=11 then 'BUS' when End_MOT=12 then 'Bike'  else 'No' end End_MOT,case when Approval_Flag=1 then 'Pending' when Approval_Flag=2 then 'Approved' else 'Reject' end TAStatus,case when Approval_Flag='1' then 'rgb(255,165,0) !important'  when Approval_Flag='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr,case when MOT_Name='Two Wheeler' then 300 else 800 end Maxkm from Expense_Start_Activity WITH (NOLOCK) where  sf_code='".$sfCode."' and convert(date,Date_Time)='".$Ta_Date."'";
		$TAStartandEnd = performQuery($sql);
		
		$sql = "select cast (convert(date,eDate) as varchar) Start_date, eDate, * from Trans_Daily_User_Expense WITH (NOLOCK) where sf_code='".$sfCode."' and  convert(date,eDate)='".$Ta_Date."' ";
		$User_Expense =performQuery($sql);
		$expense['TAStartandEnd']=$TAStartandEnd;
        $expense['User_Expense']=$User_Expense;
		$sql = "select * from Trans_Daily_Allowance_Additional WITH (NOLOCK) where sf_code='".$sfCode."' and  convert(date,Exp_date)='".$Ta_Date."' ";
		$AD =performQuery($sql);
		$expense['AditionalAllowance']=$AD;
        outputJSON($expense);
}
function getTAApproval(){
	global $data;
	$sfCode=(string) $data["sf"];
	 
        $sql = "select * from ViewTravelAllowance WITH (NOLOCK) where Reporting_To_SF='".$sfCode."' ";
        $vwTAlowance = performQuery($sql);
		

		$sql = "select * from View_User_Expense WITH (NOLOCK) where sf_code='".$sfCode."'";
		$User_Expense =performQuery($sql);
		$expense['TAStartandEnd']=$vwTAlowance;
        $expense['User_Expense']=$User_Expense;
        outputJSON($expense);
}
function getDailyAllow() {
	global $data;
    $div = (string) $data['div'];
	$date= (string) $data['Ta_Date'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
	$sfCode=(string) $data["sf"];
    $query = "select ID,Allowance_Name Name,Max_Allowance,Attachemnt,user_enter,isnull(Reference,0)Reference,isnull(Multi_Fields,0) Multi_Fields,isnull(Exp_For,0)Exp_For,Short_Name  from Mas_Allowance_Type WITH (NOLOCK) where Type=1 and Division_Code='". $Owndiv ."'  and active_flag=0";
	$result['Expensequery']=$query;
	$result['ExpenseWeb']=performQuery($query);
	for($k=0;$k<count($result['ExpenseWeb']);$k++){
		$query = "select Ad_Fld_ID,Ad_Fld_Name from Allowance_additional WITH (NOLOCK) where Allowance_Type_Code='".$result['ExpenseWeb'][$k]["ID"]."' and Division_Code='". $Owndiv ."' and Active_Flag=0 ";
		$result['ExpenseWeb'][$k]["value"]=performQuery($query);
	}
	
	$query = "select *,case when MOT_Name='Two Wheeler' then 300 else 800 end Maxkm from Expense_Start_Activity WITH (NOLOCK) where Sf_code='".$sfCode."'  and convert(date,Date_Time)='".$date."'";
	$TodayExpense=performQuery($query);
	$result['TodayExpense']=$TodayExpense;
	
	
	
    return $result;
	
}
function StartKmD() {
global $data;
    $sf = (string) $data['sf'];
	 
	 $query = "select SF_HQ from Mas_Salesforce where SF_Code='".$sf."' ";
	 $Sf_Hqname=performQuery($query);
	 $Sf_Hq=$Sf_Hqname[0]['SF_HQ'];
	$date= (string) $data['Activity_Date'];
	$query = "select top 1 cast(cast(Start_Km as float) as int) Start_Km,MOT_Name,dailyAllowance,case when MOT_Name='Two Wheeler' then 300 else 1000 end Maxkm,isnull(('http://hap.sanfmcg.com/BikePhotos/'+Image_Url),'')start_Photo,case when  dailyAllowance='HQ' then (select HQ_Name from Mas_HQuarters  where HQ_ID=$Sf_Hq) else To_Place end To_Place,To_Place_Id from Expense_Start_Activity WITH (NOLOCK)  where  sf_code='".$sf."' and convert(date,Date_Time)='".$date."'  order by Date_Time desc";
	$result['StartDetails']=performQuery($query);
	return $result;
}
function expsendtoapproval() {
saveNewDailyExp();
global $data;
$sfCode=(string) $data["SF_Code"];
$date =(string) $data['exp_date'];
$query = "update Expense_Start_Activity set Approval_Flag='2',LastUpdt_Dt=getDate()  where  sf_code='".$sfCode."' and convert(date,Date_Time)='".$date."'";
performQuery($query);
$query = "update Trans_Expense_Head2 set Flag='2'  where  sf_code='".$sfCode."' and Expense_Date='".$date."'";
performQuery($query);
$res["success"]=true;
return $res;

}
function getTravelMode(){
	global $data;
	$sfCode=(string) $data["sf"];
	$sql="select *,case when MOT_Name='Two Wheeler' then 300 else 800 end Maxkm from Expense_Start_Activity WITH (NOLOCK) where convert(date, Date_Time)=convert(date,GetDate()) and Sf_code='".$sfCode."' ";
	$User_Expense =performQuery($sql);
    outputJSON($User_Expense);
}
function saveApprove(){
	global $data;
	$sfCode=(string) $data["sfCode"];
	$Sl_No =(string) $data['Sl_No'];
	$AAmount = (string) $data['AAmount'];
	$Flag = (string) $data['Flag'];
	$Reason = (string) $data['Reason'];
	$Reason = str_replace("'","''",$Reason);
	if($Flag=="1"){
		$sql = "update Expense_Start_Activity set Approval_Flag=3,Approval_Amount='" . $AAmount . "',Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $sfCode . "' where Sl_No='".$Sl_No."'";
	}else{
		$sql = "update Expense_Start_Activity set Approval_Flag=4,Approved_Date='" .date('Y-m-d H:i:s'). "',Approved_by='" . $sfCode . "',Reject_reason='" . $Reason . "' where Sl_No='".$Sl_No."'";   
	}
	performQuery($sql);
	$res["success"]="true";
	
	return $res;
}
function saveDAExceptionApprove(){
	global $data;
	$sfCode=(string) $data["Sf_Code"];
	$asfCode=(string) $data["Approve_by"];
	$Sl_No =(string) $data['ID'];
	$DADT =(string) $data['DaDT'];
	$DAType =(string) $data['DA_Type'];
	$Flag = (string) $data['Flag'];
	$Reason = (string) $data['Reject_Reason'];
	if($Flag=="0"){
		$sql = "update Expense_Lodge_Exception set Approval_Flag=0,Appr_Date='" .date('Y-m-d H:i:s'). "',Appr_By='" . $asfCode . "' where Sf_Code='".$sfCode."' and cast(DA_Date as date)='".$DADT."' and DA_Type='".$DAType."' and SlNo=".$Sl_No."";
	}
	else{
		$sql = "update Expense_Lodge_Exception set Approval_Flag=2,Appr_Date='" .date('Y-m-d H:i:s'). "',Appr_By='" . $asfCode . "',Reject_Reason='".$Reason."' where Sf_Code='".$sfCode."' and cast(DA_Date as date)='".$DADT."' and DA_Type='".$DAType."' and SlNo=".$Sl_No."";
	}
	performQuery($sql);
	$res["success"]="true";
	return $res;
}
function getApproval(){
	global $data;
	$sfCode=(string) $data["sf"];
	$response=array();
	$sql="select * from ViewTravelAllowance WITH (NOLOCK) where Reporting_To_SF ='".$sfCode."'";
	$res=performQuery($sql);	
	
	for($k=0; $k<count($res) ; $k++){
		$jw1=array();
		$jw1["sf_code"]=$res[$k]["sf_code"];
		$jw1["Start_date"]=$res[$k]["Start_date"];
		$jw1["sl_no"]=$res[$k]["Sl_No"];
		$jw1["name"]=$res[$k]["FieldForceName"];
		$jw1["code"]=$res[$k]["EmpCode"];
		$jw1["hq"]=$res[$k]["HQ"];
		$jw1["Designation"]=$res[$k]["Designation"];
		$jw1["mob"]=$res[$k]["SF_Mobile"];
		$jw1["smod"]=$res[$k]["Start_MOT"];
		$jw1["emod"]=$res[$k]["End_MOT"];
		$jw1["skm"]=$res[$k]["Start_Km"];
		$jw1["ekm"]=$res[$k]["End_Km"];
		
		$sql="select * from View_User_Expense WITH (NOLOCK) where sf_code ='".$res[$k]["sf_code"]."' and Start_date='".$res[$k]["Start_date"]."' ";
		$ress=performQuery($sql);
		$arrVal=array();
		for($kk=0; $kk<count($ress) ; $kk++){
			$res_arr=array();
			$res_arr["name"]=$ress[$kk]["expName"];
			$res_arr["amt"]=$ress[$kk]["Amt"];
			array_push($arrVal,$res_arr);
		}
		$jw1["value"]=$arrVal;
		array_push($response,$jw1);
	}
	return $response;
}
 function saveDailyExp(){
	global $data;
	$dateTime=date('Y-m-d H:i:s');
	$date=(string) $data["date"];
	$sfCode=(string) $data["sf"];
	$Owndiv=(string) $data["div"];
	$divs = explode(",", $Owndiv . ",");
    $Owndiv = (string) $divs[0];
	$exp=$data['dailyExpense'];
	$ea=$data['EA'];
	$cap=$data['ActivityCaptures'];
	$restt=array();
	$Aditionalall=$data['AditionalAlowanceArray'];
	print_r($date);
	die;
	
	$ea[0]["DateOfExp"]=str_replace("\"","",$ea[0]["DateOfExp"]);
	
	for ($ad = 0; $ad < count($Aditionalall); $ad++) 
    {
		$sql="insert into Trans_AddAllowance(Sf_Code,Division_Code,MODE,FromPlace,to_Place,FARE,Serverdate,Ekey) select '" . $sfCode . "','" . $Owndiv . "','".$Aditionalall[$ad]["MODE"]."','".$Aditionalall[$ad]["FROM"]."','".$Aditionalall[$ad]["TO"]."','".$Aditionalall[$ad]["FARE"]."',getDate(),'" . $ea[0]["Ukey"] . "'";
		performQuery($sql);
	}
	
	 for ($i = 0; $i < count($data['dailyExpense']); $i++) 
    {
	$additional=$exp[$i]["value"];
	for ($j = 0; $j < count($exp[$i]["value"]); $j++){
		//Trans_Daily_Allowance_Additional
		$sql="insert into Trans_Daily_Allowance_Additional(Sf_Code,Exp_date,Alw_Code,ALw_Add_Fld,ALw_Add_Val,Ekey) select '" . $sfCode . "','".$date."','".$additional[$j]["mainid"]."','".$additional[$j]["id"]."','".$additional[$j]["val"]."','" . $ea[0]["Ukey"] . "'";
		performQuery($sql);
	}
	$sql = "insert into Trans_Daily_User_Expense(SF_Code,eDate,expCode,expName,Amt,Division_Code,LUpdtDate,Ukey,Image_Url) select '" . $sfCode . "','".$date."','" . $exp[$i]["ID"] . "','" . $exp[$i]["Name"] . "','" . $exp[$i]["amt"] . "','" . $Owndiv . "',getDate(),'" . $ea[0]["Ukey"] . "' ,'" . $exp[$i]["imgData"] . "' " ;
     performQuery($sql);
			

 /* $myfile = fopen("../server/newSAveExpense.txt", "a+");
        $txt = "Vasan\n";
        fwrite($myfile, $sql);
        fclose($myfile);
			 */
	$restt["first"]=$sql;
}
	$sql1="insert into  Trans_Expense_Traveldetails(sf_code,Division_code,MOC,Remarks,Start_Image,FromDeparture,End_Image,ToArrival,Date_Time,Fare,FromPlace,ToPlace,Leos,Ukey,FromPlace_Name,ToPlace_Name,Bus_Support_Doc,DateofExpense)values('".$sfCode."','".$Owndiv ."','" . $ea[0]["MOT"] . "'
            ,'".$ea[0]["remarks"]."' ,'".$ea[0]["Start_image"]."','".$ea[0]["Start_Km"]."','".$ea[0]["Stop_image"]."','".$ea[0]["Stop_km"]."', '".$date."' ,'".$ea[0]["BusFare"]."','".$ea[0]["Workplace"]."' ,'".$ea[0]["TodayworkRoute"]."' ,'" . $ea[0]["LEOS"] . "','" . $ea[0]["Ukey"] . "','".$ea[0]["Workplace_Name"]."' ,'".$ea[0]["TodayworkName"]."' ,'".$ea[0]["EventcaptureUrl"]."','".$ea[0]["DateOfExp"]."')";
        performQuery($sql1);
	$restt["second"]=$sql;
	
	for ($i = 0; $i < count($cap); $i++) 
    {
		$sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,DCRDetNo) values( '".$sfCode."','" . $cap[$i]["imgurl"] . "','" . $cap[$i]["title"] . "','" . $cap[$i]["remarks"] . "','".$Owndiv."','Expense','".$dateTime."','" . $ea[0]["Ukey"] . "')";
                    performQuery($sql); 
					$restt["third"]=$sql;
	} 
	 
	$updatetable="update Expense_Start_Activity set Approval_Flag=2 where Sf_code='".$sfCode."'  and convert(date,Date_Time)=convert(date,'".$ea[0]["DateOfExp"]."')";
	performQuery($updatetable); 
	
	 
	$restt["success"]="true";
	return $restt;

}
function getExpenseApproval(){
		global $data;
		$sfCode=(string) $data["sfCode"];
		$Ta_Date = (string) $data["Ta_Date"];
		$query = "select st.Sf_code,st.Division_Code,Date_Time,isnull(Start_Km,0)Start_Km,	isnull(Remarks,'')Remarks,st.MOT,isnull(Enddateand_time,'')Enddateand_time,
isnull(End_Km,0)End_Km,isnull(End_remarks,'')End_remarks,isnull(End_MOT,'')End_MOT,Approval_Flag,isnull(summary_Flag,0)summary_Flag,st.Sl_No,	isnull(Approval_Amount,0)Approval_Amount,isnull(Approved_Date,0)Approved_Date,	isnull(Approved_by,'')Approved_by,isnull(Reject_reason,'')Reject_reason,From_Place,	To_Place,	Fare,isnull(Personal_Km,0)Personal_Km,	st.StEndNeed,	dailyAllowance,	driverAllowance,	MOT_Name,	To_Place_Id  ,isnull(FuelAmt,0) FuelAmt,isnull(('http://hap.sanfmcg.com/BikePhotos/'+st.Image_Url),'')start_Photo,
isnull(('http://hap.sanfmcg.com/BikePhotos/'+st.End_Image_Url),'')End_photo,isnull(Alw_Eligibilty,0) Alw_Eligibilty,
isnull((select isnull(Allowance_Value,0) from Mas_Allowance_Entry where Allowance_Code=st.dailyAllowance and Sf_code=st.Sf_code),1)Allowance_Value ,case when MOT_Name='Two Wheeler' then 300 else 800 end Maxkm
from Expense_Start_Activity st WITH (NOLOCK) inner join   Mas_Modeof_Travel mot WITH (NOLOCK) on  mot.Sl_No=CAST(st.MOT AS int) 
where  Sf_code='".$sfCode."' and convert(date,Date_Time)='".$Ta_Date."'";
		$TSD = performQuery($query);
		$expense['TodayStart_Details']=$TSD;
		$ExpH= "select * from Trans_Expense_Head2 WITH (NOLOCK) where  sf_code='".$sfCode."' and convert(date,Expense_Date)='".$Ta_Date."'";
		$Expense_Head = performQuery($ExpH);
		$expense['Expense_Head']=$Expense_Head;
		$TAAddsql = "select    SlNo,Exp_Date,Exp_Code,Exp_Amt,isnull(Attachments,0)Attachments,Fk_Sl_No,Sf_Code,Expense_Type,isnull(Ukey,'')Ukey  from Trans_Additional_Expense WITH (NOLOCK) where Fk_Sl_No='".$Expense_Head[0]["Sl_No"]."'";
		$TAAditional_Exp =performQuery($TAAddsql);
		
		if(count($TAAditional_Exp)<1){
			$expense['Additional_Expense']=[];
		}else{
			$expense['Additional_Expense']=$TAAditional_Exp;
		}
		
		
	    $TARefsql = "select  * from Trans_Ref_Details WITH (NOLOCK) where FK_SlNo='".$Expense_Head[0]["Sl_No"]."'";
	    $TARefDetails =performQuery($TARefsql);
		if(count($TARefDetails)<1){
			$expense['Additional_Expense']=[];
		}else{
			$expense['Additional_Expense']=$TARefDetails;
		}
		
		$expense['Ref_Details']=$TARefDetails;
	    $TAtravelsql = "select  * from Trans_Travelled_Details WITH (NOLOCK) where FK_Exp_SlNo='".$Expense_Head[0]["Sl_No"]."'";
	    $Travelled =performQuery($TAtravelsql);
		
		if(count($Travelled)<1){
			$expense['Travelled_Details']=[];
		}else{
			$expense['Travelled_Details']=$Travelled;
		}
		
		
		$TAtravelLocsql = "select   SlNo,FK_Trv_SlNo,From_P,To_P,isnull(Attachments,0)Attachments,Sf_Code,Mode,Fare ,isnull(Ukey,'') Ukey from Trans_Travelled_Loc WITH (NOLOCK) where FK_Trv_SlNo='".$Expense_Head[0]["Sl_No"]."'";
	    $Travelled_Loc =performQuery($TAtravelLocsql);
		if(count($Travelled_Loc)<1){
			$expense['Travelled_Loc']=[];
		}else{
			$expense['Travelled_Loc']=$Travelled_Loc;
		}
		
       return $expense;
		
}
function saveTpEntry(){
	global $data;
	$dateTime=date('Y-m-d H:i:s');
	$divCode = $_GET['divisionCode'];

    $divisionCode = explode(",", $divCode);
	$sfCode = $_GET['sfCode'];
	$tourDate = $data[0]['Tour_Plan']['Tour_Date'];
	$Ekey = $data[0]['Tour_Plan']['Ekey'];
	$Tp_JsonObject=$data[0]['Tp_DynamicValues'];
	$worktype_code = $data[0]['Tour_Plan']['worktype_code'];
    $worktype_name = $data[0]['Tour_Plan']['worktype_name'];
	$worked_with_code = ($data[0]['Tour_Plan']['Worked_with_Code']==null?"":$data[0]['Tour_Plan']['Worked_with_Code']);
    $worked_with_name = ($data[0]['Tour_Plan']['Worked_with_Name']==null?"":$data[0]['Tour_Plan']['Worked_with_Name']);
	$RouteCode = ($data[0]['Tour_Plan']['RouteCode']==null?"":$data[0]['Tour_Plan']['RouteCode']);
	$RouteName = ($data[0]['Tour_Plan']['RouteName']==null?"":$data[0]['Tour_Plan']['RouteName']);
   $MOT=$data[0]['Tour_Plan']['MOT'];
   $Flag	=$data[0]['Tour_Plan']['Flag'];
   $DA_Type=$data[0]['Tour_Plan']['DA_Type'];
   $Driver_Allow=$data[0]['Tour_Plan']['Driver_Allow'];
   $From_Place=$data[0]['Tour_Plan']['From_Place'];
   $To_Place=$data[0]['Tour_Plan']['To_Place'];		
   $MOT_ID=$data[0]['Tour_Plan']['MOT_ID'];
   $objective = $data[0]['Tour_Plan']['objective'];
   $jointworkname=$data[0]['Tour_Plan']['worked_with'];
   $jointWorkCode=$data[0]['Tour_Plan']['jointWorkCode'];
   $To_Place_ID=$data[0]['Tour_Plan']['To_Place_ID'];
	$sql ="insert into Tp_Data select '".$sfCode."',getdate(),".$tourDate.",'".str_replace("'","",$_POST['data'])."',''";
	performQuery($sql);
   if($data[0]['Tour_Plan']['Button_Access']==null)
    $Button_Access="";
    else
   $Button_Access=$data[0]['Tour_Plan']['Button_Access'];
			
			$Mode_Travel_Id=$data[0]['Tour_Plan']['Mode_Travel_ID'];
	$sql = "delete from Trans_TP_One WHERE SF_Code ='" . $sfCode . "' and Tour_Date=cast($tourDate as datetime)";
            performQuery($sql);
			$sql = "delete from Tp_DynamicTable WHERE Sf_Code ='" . $sfCode . "' and Tour_Date=cast($tourDate as datetime)";
            performQuery($sql);
			
    $toursql = "insert into Trans_TP_One(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Confirmed,Change_Status,JointWork_Name,submit_status,JointworkCode,Worktype_Flag,MOT,DA_Type,Driver_Allow,From_Place,To_Place,Mot_ID,To_Place_ID,Mode_Travel_Id,Button_Access,Territory_Code2,Division_Code,Objective,Worked_With_SF_Code,Worked_With_SF_Name,Tour_Schedule1,Territory_Code1) 
			select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),getdate(),$tourDate,$worktype_code,$worktype_name,1,1,$jointworkname,'2',$jointWorkCode,$Flag,$MOT,$DA_Type,$Driver_Allow,$From_Place,$To_Place,$MOT_ID,$To_Place_ID,$Mode_Travel_Id,'$Button_Access','" . $Ekey . "',   " . $divisionCode[0] . ",'". str_replace("'","","$objective") ."','$worked_with_code','$worked_with_name','$RouteCode','$RouteName'";
            performQuery($toursql);
		     /*$myfile = fopen("../server/newSAveExpense.txt", "a+");
        $txt = "Vasan\n";
        fwrite($myfile, $toursql);
        fclose($myfile); */
		
		 //print_r($data);
	for ($i = 0; $i < count($Tp_JsonObject); $i++) 
    {
		$sql = "SELECT isnull(max(Sl_No)+1,1) as SlNo FROM Tp_DynamicTable";
		$topr = performQuery($sql);
		$pk =  (int)$topr[0]['SlNo'];
		$sql="insert into Tp_DynamicTable (Sl_No,Ukey,Sf_Code,Fld_ID,Fld_Name,Fld_Type,Fld_Src_Name,Fld_Src_Field,Fld_Length,Fld_Symbol,Fld_Mandatory,Active_flag,
		Control_id,Target_Form,Filter_Text,Filter_Value,Field_Col,Tour_Date)
		select '$pk','".$Ekey."','".$sfCode."','".$Tp_JsonObject[$i]["Fld_ID"]."','".$Tp_JsonObject[$i]["Fld_Name"]."','".$Tp_JsonObject[$i]["Fld_Type"]."',
		'".$Tp_JsonObject[$i]["Fld_Src_Name"]."','".$Tp_JsonObject[$i]["Fld_Src_Field"]."','".$Tp_JsonObject[$i]["Fld_Length"]."','".$Tp_JsonObject[$i]["Fld_Symbol"]."','".$Tp_JsonObject[$i]["Fld_Mandatory"]."'
		,'0','".$Tp_JsonObject[$i]["Control_id"]."' ,'".$Tp_JsonObject[$i]["Target_Form"]."',
		'".$Tp_JsonObject[$i]["Filter_Text"]."','".$Tp_JsonObject[$i]["Filter_Value"]."','".$Tp_JsonObject[$i]["Field_Col"]."',$tourDate";
			performQuery($sql);
	/* 	$myfile = fopen("../server/newSAveExpense.txt", "a+");
        $txt = "Vasan\n";
        fwrite($myfile, $sql);
        fclose($myfile); */
		
	}
	  $resp["success"] = true;
           // $resp["REs"]=$sql;
      echo json_encode($resp);
     die;
}
function savemydayplan(){
	global $data;
	
	$dateTime=date('Y-m-d H:i:s');
	$divCode = $_GET['divisionCode'];
    $divisionCode = explode(",", $divCode);
	$sfCode = $_GET['sfCode'];
	$today = $data[0]['Tp_Dayplan']['dcr_activity_date'];
	$Ekey = $data[0]['Tp_Dayplan']['Ekey'];
	$Tp_JsonObject=$data[0]['Tp_DynamicValues'];
	$worktype_code = $data[0]['Tp_Dayplan']['worktype_code'];
    $worktype_name = $data[0]['Tp_Dayplan']['worktype_name'];
	
	
	
	$worked_with_code = ($data[0]['Tp_Dayplan']['Worked_with_Code']==null?"":$data[0]['Tp_Dayplan']['Worked_with_Code']);
    $worked_with_name = ($data[0]['Tp_Dayplan']['Worked_with_Name']==null?"":$data[0]['Tp_Dayplan']['Worked_with_Name']);
	$RouteCode = ($data[0]['Tp_Dayplan']['RouteCode']==null?"":$data[0]['Tp_Dayplan']['RouteCode']);
	$RouteName = ($data[0]['Tp_Dayplan']['RouteName']==null?"":$data[0]['Tp_Dayplan']['RouteName']);
   $MOT=$data[0]['Tp_Dayplan']['MOT'];
   $Tp_JsonObject=$data[0]['Tp_DynamicValues'];
   $Flag	=$data[0]['Tp_Dayplan']['Flag'];
   $DA_Type=$data[0]['Tp_Dayplan']['DA_Type'];
   $Driver_Allow=$data[0]['Tp_Dayplan']['Driver_Allow'];
   $From_Place=$data[0]['Tp_Dayplan']['From_Place'];
   $To_Place=$data[0]['Tp_Dayplan']['To_Place'];		
   $MOT_ID=$data[0]['Tp_Dayplan']['MOT_ID'];
   $objective = $data[0]['Tp_Dayplan']['objective'];
   $jointworkname=$data[0]['Tp_Dayplan']['worked_with'];
   $jointWorkCode=$data[0]['Tp_Dayplan']['jointWorkCode'];
   $To_Place_ID=$data[0]['Tp_Dayplan']['To_Place_ID'];
   if($data[0]['Tp_Dayplan']['Button_Access']==null)
    $Button_Access="";
    else
   $Button_Access=$data[0]['Tp_Dayplan']['Button_Access'];		
	$Mode_Travel_Id=$data[0]['Tp_Dayplan']['Mode_Travel_ID'];

$sql = "insert into tbMyDayPlan(sf_code,Pln_Date,remarks,Division_Code,wtype,FWFlg,dcrtype,location,Button_Access,Ekey,stockist,StkName,cluster,ClstrName) 
select '" . $sfCode . "',".$today."," . $objective . ",'" . $divisionCode[0] . "'," . $worktype_code . "," . $Flag . ",'App','$location','$Button_Access','".$Ekey."','$worked_with_code','$worked_with_name','$RouteCode','$RouteName'";  
performQuery($sql);



for ($i = 0; $i < count($Tp_JsonObject); $i++) 
    {
		$sql = "SELECT isnull(max(Sl_No)+1,1) as SlNo FROM Mydayplan_Dynamic";
		$topr = performQuery($sql);
		$pk =  (int)$topr[0]['SlNo'];
		$sql="insert into Mydayplan_Dynamic (Sl_No,Ukey,Sf_Code,Fld_ID,Fld_Name,Fld_Type,Fld_Src_Name,Fld_Src_Field,Fld_Length,Fld_Symbol,Fld_Mandatory,Active_flag,
		Control_id,Target_Form,Filter_Text,Filter_Value,Field_Col,Plan_Date)
		select '$pk','".$Ekey."','".$sfCode."','".$Tp_JsonObject[$i]["Fld_ID"]."','".$Tp_JsonObject[$i]["Fld_Name"]."','".$Tp_JsonObject[$i]["Fld_Type"]."',
		'".$Tp_JsonObject[$i]["Fld_Src_Name"]."','".$Tp_JsonObject[$i]["Fld_Src_Field"]."','".$Tp_JsonObject[$i]["Fld_Length"]."','".$Tp_JsonObject[$i]["Fld_Symbol"]."','".$Tp_JsonObject[$i]["Fld_Mandatory"]."'
		,'0','".$Tp_JsonObject[$i]["Control_id"]."' ,'".$Tp_JsonObject[$i]["Target_Form"]."',
		'".$Tp_JsonObject[$i]["Filter_Text"]."','".$Tp_JsonObject[$i]["Filter_Value"]."','".$Tp_JsonObject[$i]["Field_Col"]."',$today";
			performQuery($sql);
	/* $myfile = fopen("../server/newSAveExpense.txt", "a+");
        $txt = "Vasan\n";
        fwrite($myfile, $sql);
        fclose($myfile); */
		
	}





//$missedquery="exec svPostMissAndAuto  '$sfCode',".$today.",'" . $divisionCode[0]. "' ";

//performQuery($missedquery);

$resp["success"] = true;
           // $resp["REs"]=$sql;
      echo json_encode($resp);
     die;

	
}
function saveNewDailyExp(){
	global $data;
	
	$Result=[];
	$dateTime=date('Y-m-d H:i:s');
	$sfCode=(string) $data["SF_Code"];
	$eremarks=(string) $data["edt_remark"];
	$date=(string) $data["exp_date"];
	$da_mode=(string) $data["da_mode"];
	$al_type=(string) $data["al_type"];
	$from_place=(string) $data["from_place"];
	$to_place=(string) $data["to_place"];
	$al_amount=(string) $data["al_amount"];
	$da_amount=(string) $data["da_amount"];
	$lc_amount=(string) $data["lc_amount"];
	$oe_amount=(string) $data["oe_amount"];
	$total_ldg_amt=(string) $data["total_ldg_amt"];
	$ta_total_amount=(string) $data["ta_total_amount"];
	$drvBrdAmt=(string) $data["drvBrdAmt"];
	$gr_total=(string) $data["gr_total"];
	$trv_lc_amt=(string) $data["trv_lc_amt"];
	$divs = explode(",", $Owndiv . ",");
    $Owndiv = (string) $divs[0];
	$Add_Exp=$data['Add_Exp'];
	$Trv_details=$data['Trv_details'];
	$ExpAttch=$data['TAAttach'];
	$Other_Exp=$data['Other_Exp'];
	$Lodg_details=$data['Lodg_details'];
	$Da_Claim=$data['Da_Claim'];
	$restt=array();
	
	
	
	$sql ="insert into Expense_Data select '".$sfCode."',getdate(),'".$date ."','".str_replace("'","",$_POST['data'])."'";
	performQuery($sql);
	$sql ="select Sl_No from Trans_Expense_Head2 WITH (NOLOCK) where sf_Code='".$sfCode."' and convert(date,Expense_Date)='".$date ."'";
	$resAlready=performQuery($sql);
	if(count($resAlready)>0){
		for ($i = 0; $i < count($resAlready); $i++) 
		{
			$sql="delete from Trans_Expense_Head2  where sf_code='".$sfCode."' and Sl_No='".$resAlready[$i]['Sl_No']."'";
			performQuery($sql);	
			$sql="delete from Trans_Ref_Details  where sf_code='".$sfCode."' and FK_SlNo='".$resAlready[$i]['Sl_No']."'";
			performQuery($sql);	
			$sql="delete from Trans_Additional_Expense  where sf_code='".$sfCode."' and Fk_Sl_No='".$resAlready[$i]['Sl_No']."'";
			performQuery($sql);	
			$sql="delete from Trans_Travelled_Loc  where sf_code='".$sfCode."' and FK_Trv_SlNo='".$resAlready[$i]['Sl_No']."'";
			performQuery($sql);		
			$sql="delete from Trans_Travelled_Details  where Sf_Code='".$sfCode."' and FK_Exp_SlNo='".$resAlready[$i]['Sl_No']."'";
			performQuery($sql);
			$sql="delete from Trans_Lodging_Details  where Sf_Code='".$sfCode."' and FK_SlNo='".$resAlready[$i]['Sl_No']."'";
			performQuery($sql);
			$sql="delete from Trans_Lod_Join_Details  where FK_SlNo='".$resAlready[$i]['Sl_No']."'";
			performQuery($sql);
			$sql="delete from Da_Claim  where FK_Trv_SlNo='".$resAlready[$i]['Sl_No']."'";
			performQuery($sql);
		}
	}
	$sql = "SELECT isnull(max(Sl_No)+1,1) as Sl_No FROM Trans_Expense_Head2";
	$topr = performQuery($sql);
	$pk =  (int)$topr[0]['Sl_No'];
	$sql1="insert into  Trans_Expense_Head2(Sl_No,Sf_Code,Expense_Date,Flag,Total_Amount,DA_Mode,Alw_Type,From_Loc,To_Loc,Alw_Amount,Boarding_Amt,Lc_totalAmt,Oe_totalAmt,Ldg_totalAmt,Ta_totalAmt,drvBrdAmt,trv_lc_amt,reason)values( '".$pk."','".$sfCode."','".$date ."','0'
            ,'".$gr_total."','".$da_mode."','".$al_type."','".$from_place."','".$to_place."', '".$al_amount."','".$da_amount."','".$lc_amount."','".$oe_amount."','".$total_ldg_amt."','".$ta_total_amount."','".$drvBrdAmt."','".$trv_lc_amt."','".str_replace("'","",$eremarks)."')";
	performQuery($sql1);
	
	for ($i = 0; $i < count($ExpAttch); $i++)
	{
		$attItem=$ExpAttch[$i];
		$Qry="insert into Activity_Event_Captures (Activity_Report_Code,title,Identification,imgurl,remarks,DCRDetNo,Insert_Date_Time,lat,lon) 
		values('".$sfCode."','".$attItem["Mode"]."','".$attItem["Type"]."','".$attItem["FName"]."','','','".$attItem["DtTm"]."','".$attItem["UKey"]."','".$attItem["IKey"]."')";
		performQuery($Qry);
	}
	
	for ($i = 0; $i < count($Add_Exp); $i++) 
    {
		$sql = "SELECT isnull(max(SlNo)+1,1) as SlNo FROM Trans_Additional_Expense";
		$topr = performQuery($sql);
		
		$TASlNo =  (int)$topr[0]['SlNo'];
		$GSTAmt =  $Add_Exp[$i]["lc_gstAmt"];
		$GSTBNo =  $Add_Exp[$i]["lc_gstBillNo"];
		if($GSTAmt=="") $GSTAmt="0";
		$expsql="insert into Trans_Additional_Expense(SlNo,Fk_Sl_No,Exp_Date,Exp_Code,Exp_Amt,Attachments,Sf_Code,Expense_Type,Ukey,GSTNo,GSTAmt,GSTBNo)values('".$TASlNo."','".$pk."','".$date."','".$Add_Exp[$i]["type"] ."','".$Add_Exp[$i]["total_amount"]."','".$Add_Exp[$i]["attach_count"]."','".$sfCode."','".$Add_Exp[$i]["exp_type"]."','".$Add_Exp[$i]["u_key"]."','".$Add_Exp[$i]["lc_gstNo"]."',".$GSTAmt.",'".$GSTBNo."')";
		performQuery($expsql);
		$ad_exp=$Add_Exp[$i]["ad_exp"];
		
		for ($j = 0; $j < count($Add_Exp[$i]["ad_exp"]); $j++){
			$sql = "SELECT isnull(max(Max_Sl_no)+1,1) as Sl_No FROM Trans_Ref_Details";
			$TRD = performQuery($sql);	
			$TRDsl =  (int)$TRD[0]['Sl_No'];
			$sql="exec insertTAReferenceDetails '" . $TRDsl . "','" . $TASlNo . "','".$pk."','".$sfCode."','".$ad_exp[$j]["KEY"]."','".str_replace("'","",$ad_exp[$j]["VALUE"])."'";
			//$sql="insert into Trans_Ref_Details(Max_Sl_no,SlNo,FK_SlNo,Ref_Code,Ref_Value,Sf_Code) select '" . $TRDsl . "','" . $TASlNo . "','".$pk."','".$ad_exp[$j]["KEY"]."','".$ad_exp[$j]["VALUE"]."','".$sfCode."'";
			performQuery($sql);
		}
		
	}
	
    for ($i = 0; $i < count($Other_Exp); $i++) {
    
		$sql = "SELECT isnull(max(SlNo)+1,1) as Fk_Sl_No FROM Trans_Additional_Expense";
		$topr = performQuery($sql);
		$Fk_Sl_No = (int)$topr[0]['Fk_Sl_No'];
		$GSTAmt =  $Other_Exp[$i]["otherExp_gstAmt"];
		$GSTBNo =  $Other_Exp[$i]["otherExp_gstBillNo"];
		if($GSTAmt=="") $GSTAmt="0";
		$expsql="insert into Trans_Additional_Expense(SlNo,Fk_Sl_No,Exp_Date,Exp_Code,Exp_Amt,Attachments,Sf_Code,Expense_Type,Ukey,GSTNo,GSTAmt,GSTBNo)values('".$Fk_Sl_No."','".$pk."','".$date."','".$Other_Exp[$i]["type"] ."','".$Other_Exp[$i]["total_amount"]."','".$Other_Exp[$i]["attach_count"]."','".$sfCode."','".$Other_Exp[$i]["exp_type"]."','".$Other_Exp[$i]["u_key"]."','".$Other_Exp[$i]["otherExp_gstNo"]."',".$GSTAmt.",'".$GSTBNo."')";
		performQuery($expsql);
		
		$ad_exp=$Other_Exp[$i]["ad_exp"];
		
		for ($j = 0; $j < count($Other_Exp[$i]["ad_exp"]); $j++){
			$sql = "SELECT isnull(max(Max_Sl_no)+1,1) as Sl_No FROM Trans_Ref_Details";
			$TRD = performQuery($sql);	
			$TRDsl =  (int)$TRD[0]['Sl_No'];
			$sql="exec insertTAReferenceDetails '" . $TRDsl . "','" . $Fk_Sl_No . "','".$pk."','".$sfCode."','".$ad_exp[$j]["KEY"]."','".str_replace("'","",$ad_exp[$j]["VALUE"])."'";
			//$sql="insert into Trans_Ref_Details(Max_Sl_no,SlNo,FK_SlNo,Ref_Code,Ref_Value,Sf_Code) select '" . $TRDsl . "','" . $Fk_Sl_No . "','".$pk."','".$ad_exp[$j]["KEY"]."','".$ad_exp[$j]["VALUE"]."','".$sfCode."'";
			performQuery($sql);
		}
	}
	
	$sql = "SELECT isnull(max(SlNo)+1,1) as Sl_No FROM Trans_Travelled_Details";
	$TTD = performQuery($sql);	
	$TTDsl =  (int)$TTD[0]['Sl_No'];
	$trvSql="insert into Trans_Travelled_Details(SlNo,FK_Exp_SlNo,Mode_Of_Travel,Start_KM,End_KM,Travelled_KM,Personal_KM,Claimed_KM,Fare,FS_Attach,ED_Attach,Sf_Code,Amt,ta_total_amount)values('".$TTDsl."','".$pk."','".$Trv_details["MOT"]."','".$Trv_details["Start_Km"]."','".$Trv_details["End_Km"]."'
            ,'".$Trv_details["Tr_km"]."','".$Trv_details["Pr_km"]."','".$Trv_details["total_claim"]."','".$Trv_details["fuel_cha"]."','','','".$sfCode."','".$Trv_details["fuel_amt"]."','".$Trv_details["ta_total_amount"]."')";
	performQuery($trvSql);
	$Traveled_Loc=$Trv_details["trv_loca"];
	
    for ($k = 0; $k < count($Trv_details["trv_loca"]); $k++){
		$sql = "SELECT isnull(max(SlNo)+1,1) as Sl_No FROM Trans_Travelled_Loc";
		$trv = performQuery($sql);	
		$trvsl =  (int)$trv[0]['Sl_No'];
		$GSTAmt =  $Traveled_Loc[$k]["tv_gstAmt"];
		$GSTBNo =  $Traveled_Loc[$k]["tv_gstBillNo"];
		if($GSTAmt=="") $GSTAmt="0";
		$sql="insert into Trans_Travelled_Loc(SlNo,FK_Trv_SlNo,From_P,To_P,Sf_Code,Mode,Fare,Ukey,GSTNo,GSTAmt,GSTBNo) select '" . $trvsl . "','".$pk."','".str_replace("'","''",$Traveled_Loc[$k]["from"])."','".str_replace("'","''",$Traveled_Loc[$k]["to"])."','".$sfCode."','".$Traveled_Loc[$k]["mode"]."','".$Traveled_Loc[$k]["fare"]."','".$Traveled_Loc[$k]["u_key"]."','".$Traveled_Loc[$k]["tv_gstNo"]."',".$GSTAmt.",'".$GSTBNo."'";
		performQuery($sql);

	}
	
	if($Lodg_details["ldg_type"]!=""){
		
		$sql = "SELECT isnull(max(SlNo)+1,1) as SlNo FROM Trans_Lodging_Details";
		$topr = performQuery($sql);
		
		$Lgd =  (int)$topr[0]['SlNo'];
		if(stripos($Lodg_details["toout_dte"],"/")>-1){
			$dts = explode("/",$Lodg_details["toout_dte"]);
			$Lodg_details["toout_dte"]=$dts[2]."-".$dts[1]."-".$dts[0];
		}
		if (preg_match("/^(0[1-9]|[1-2][0-9]|3[0-1])-(0[1-9]|1[0-2])-[0-9]{4}$/",str_replace(" ".$Lodg_details["to_dte"],"", $Lodg_details["toout_dte"])))
		{
			$dts = explode("-",$Lodg_details["toout_dte"]);
			$Lodg_details["toout_dte"]=$dts[2]."-".$dts[1]."-".$dts[0];
		}
		
		$GSTAmt =  $Lodg_details["ldg_GstAmt"];
		$GSTBNo =  $Lodg_details["ldg_GstBillNo"];
		if($GSTAmt=="") $GSTAmt="0";
		
		$Ldgsql="insert into Trans_Lodging_Details(SlNo,FK_SlNo,Lodging_Type,Eligible,Bill_Amt,WOB_Amt,Add_SF_Emp_ID,Ldg_Stay_Loc,Stay_Date,Driver_Ldg_Amount,Joining_Ldg_Amount,Total_Ldg_Amount,Attachment,Sf_Code,To_Date,NO_Of_Days,Ukey,Continuous_Stay,Early_Checkin,Late_Checkout,Erly_Check_in,Erly_Check_out,Ear_bill_amt,lat_chec_in,lat_check_out,lat_bill_amt,DrvLdgNeed,drvLocId, drvLocNm,GSTNo,GSTAmt,GSTBNo) values(
				'".$Lgd."','".$pk."','".str_replace("'","''", $Lodg_details["ldg_type"])."','".$Lodg_details["elgble"]."','".$Lodg_details["bil_amt"]."','".$Lodg_details["wob_amt"]."','".$Lodg_details["LocID"]."','".$Lodg_details["ldg_type_sty"]."','".$Lodg_details["sty_dte"]."','".str_replace("Rs.","",$Lodg_details["drv_ldg_amt"])."','".$Lodg_details["jnt_ldg_amt"]."','".$Lodg_details["total_ldg_amt"]."','','".$sfCode."',
				'".$date." ".$Lodg_details["to_dte"]."','".$Lodg_details["noOfDays"]."','".$Lodg_details["u_key"]."',
				".$Lodg_details["con_sty"].",".$Lodg_details["Erly_sty"].",".$Lodg_details["lte_sty"].",'".$date." ".$Lodg_details["Ear_chec_in"]."','".$date." ".$Lodg_details["Ear_check_out"]."','".$Lodg_details["Ear_bill_amt"]."','".$date." ".$Lodg_details["lat_chec_in"]."',
				'".str_replace(" ".$Lodg_details["to_dte"],"", $Lodg_details["toout_dte"])." ".$Lodg_details["to_dte"]."','".$Lodg_details["lat_bill_amt"]."','".$Lodg_details["ldg_drv_need"]."','".$Lodg_details["ldg_drv_Styid"]."','".$Lodg_details["ldg_drv_StyNm"]."','".$Lodg_details["ldg_GstNo"]."',".$GSTAmt.",'".$GSTBNo."')"; ///lat_check_out
		performQuery($Ldgsql); 
		
		$ad_expDetails=$Lodg_details["Loding_Emp"];
		
		for ($j = 0; $j < count($ad_expDetails); $j++){
			$sql = "SELECT isnull(max(Sl_No)+1,1) as Sl_No FROM Trans_Lod_Join_Details";
			$TRD = performQuery($sql);	
			$TRDsl =  (int)$TRD[0]['Sl_No'];
			$sql="insert into Trans_Lod_Join_Details (Sl_No,Fl_Ldg_Sl_No,Emp_Code,Sf_Name,Desig,Dept,Sf_Hq,Sf_Mobile,Ldg_Amount,FK_SlNo)  select '" . $TRDsl . "','" . $Lgd . "','".$ad_expDetails[$j]["emp_cde"]."','".$ad_expDetails[$j]["emp_Name"]."','".$ad_expDetails[$j]["emp_Desig"]."','".$ad_expDetails[$j]["emp_Dept"]."','".$ad_expDetails[$j]["emp_HQ"]."','".$ad_expDetails[$j]["emp_Mob"]."','".str_replace("Rs.","",$ad_expDetails[$j]["emp_ldg_amt"])."','".$pk."'";
			performQuery($sql);
		}
	}
	/*else{
		
		$myfile = fopen("../server/tessq9.txt", "a+");
		$txt = "-" .$Lodg_details["ldg_type"] . "-";
        fwrite($myfile, $txt);
        fclose($myfile); 
		
	}*/
	$sql = "SELECT isnull(max(SL_No)+1,1) as SlNo FROM Da_Claim";
	$topr = performQuery($sql);
		
	$DaclaimId =  (int)$topr[0]['SlNo'];
	$DaclaimSql="insert into Da_Claim(SL_No,FK_Trv_SlNo,Sf_Code,all_name,brd_amt,drvBrdAmt,da_amount) values(
'".$DaclaimId."','".$pk."','".$sfCode."','".$Da_Claim["all_name"]."','".$Da_Claim["brd_amt"]."','".$Da_Claim["drvBrdAmt"]."','".$Da_Claim["da_amt"]."')";
	performQuery($DaclaimSql);
	
	$Result["Msg"]="Saved Successfully..";
	$Result["success"]=true;
	return $Result;

}
function getHeadQuaters(){
	global $data;
	$div =(string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$SF_code = (string) $data['sfCode'];
		$Owndiv = (string) $divs[0];
		$query = "select Sf_HQ from mas_salesforce where   sf_code='".$SF_code."'";
		$Sf_HQ=performQuery($query);

		$query = "select HQ_ID id,HQ_Name name,AvlShifts ODFlag from Mas_HQuarters WITH (NOLOCK) where Division_code='" . $Owndiv . "' and HQ_Active_Flag=0 order by HQ_Name";
		return performQuery($query);
} 
function getExpensedate($Stat){
	global $data;
	$div =(string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$SF_code = (string) $data['sfCode'];
		$Owndiv = (string) $divs[0];
		if($Stat==0){
		$query = "exec getExpDates '".$SF_code."'";
		return performQuery($query);
		}else{
		$Selectdate = (string) $data['Selectdate'];
		$query = "select st.*,isnull(FuelAmt,0) FuelAmt,isnull(('http://hap.sanfmcg.com/BikePhotos/'+st.Image_Url),'')start_Photo,isnull(('http://hap.sanfmcg.com/BikePhotos/'+st.End_Image_Url),'')End_photo,isnull(Alw_Eligibilty,0) Alw_Eligibilty,isnull((select isnull(Allowance_Value,0) from Mas_Allowance_Entry where Allowance_Code=st.dailyAllowance and Sf_code=st.Sf_code),1)Allowance_Value,case when MOT_Name='Two Wheeler' then 300 else 800 end Maxkm  from Expense_Start_Activity st WITH (NOLOCK) inner join   Mas_Modeof_Travel mot WITH (NOLOCK) on  mot.Sl_No=CAST(st.MOT AS int) where   Sf_code='".$SF_code."' and convert(date,Date_Time)='".$Selectdate."'";
		
		return performQuery($query);
		}
		
	
	   
} 
function getTAApprovalDets(){
	global $data;
	//$div =(string) $data['divisionCode'];
	//$divs = explode(",", $div . ",");
	$SF_code = $_GET['SF_Code'];
	$query = "exec [getApprRajDets] '".$SF_code."'";
	return performQuery($query);
}
function getExpSubmitDtsStatus(){
	global $data;
	//$div =(string) $data['divisionCode'];
	//$divs = explode(",", $div . ",");
	$SF_code = $_GET['SF_Code'];
	$query = "exec getExpSubDts '".$SF_code."'";
	return performQuery($query);
}
function getExpenseList(){
	global $data;
	$div =(string) $data['divisionCode'];
	$divs = explode(",", $div . ",");
	$SF_code = (string) $data['sfCode'];
	$Owndiv = (string) $divs[0];
	$sfCode = $_GET['SF_Code'];
	$query = "select * from vwexpenseapprovelist WITH (NOLOCK) where Reporting_To_SF='".$sfCode."'";
	return performQuery($query);
		
}
function getExpenseSFList(){
	global $data;
	$div =(string) $data['divisionCode'];
	$divs = explode(",", $div . ",");
	$SF_code = (string) $data['sfCode'];
	$Selectdate = (string) $data['Selectdate'];
	$query = "select Sl_No,Sf_Code,isnull(Total_Amount,0)Total_Amount,isnull(DA_Mode,'')DA_Mode
,isnull(Alw_Type,'')Alw_Type,isnull(From_Loc,'')From_Loc,isnull(To_Loc,'')To_Loc,isnull(Alw_Amount,0)Alw_Amount,isnull(Boarding_Amt,0)Boarding_Amt,
 isnull( Lc_totalAmt,0)Lc_totalAmt,isnull(Oe_totalAmt,0)Oe_totalAmt,isnull(Ldg_totalAmt,0)Ldg_totalAmt,isnull(Ta_totalAmt,0)Ta_totalAmt,isnull(trv_lc_amt,0)trv_lc_amt from Trans_Expense_Head2 WITH (NOLOCK) where Sf_Code='".$SF_code."' and convert(date,Expense_Date)='".$Selectdate."'";
	return performQuery($query);
}
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
		//$query="select convert(varchar,isnull((select top 1 convert(varchar,isnull(Stay_Date,getdate()),23) from Trans_Lodging_Details where Sf_Code='".$SF_code."' and convert(date,Stay_Date)=DATEADD(day,-1,cast('".$Selectdate."' as date)) and Continuous_Stay=1),cast('".$Selectdate."' as date)),23) Stay_Date_time";
		$query="select CONVERT(varchar,isnull(st,'".$Selectdate."'),23) Stay_Date_time,CONVERT(varchar,isnull(st,'".$Selectdate."'),103) CInDate,left(CONVERT(varchar,isnull(st,'".$Selectdate."'),114),5) CInTime,isnull(Continuous_Stay,0) ContStay,Add_SF_Emp_ID LocId,Ldg_Stay_Loc StayLoc from (select top 1 case when Continuous_Stay=1 then Stay_Date else '".$Selectdate."' end st,Continuous_Stay,Add_SF_Emp_ID,Ldg_Stay_Loc from Trans_Lodging_Details WITH (NOLOCK) where Sf_Code='".$SF_code."' and To_Date=dateadd(day,-1,'".$Selectdate."') order by To_Date desc) t";
		$STDT = performQuery($query);
		$expense['Stay_Qry']=$query;
		$expense['Stay_Date_time']=$STDT;	
		$queryn = "exec getLodingContinuous '".$SF_code."','".$Selectdate."'";
		$expense['Stay_Q']=$queryn;
		$expense['LodDtlist'] = performQuery($queryn);
		$ExpH= "select case when DA_Mode='HQ' then HQNAME else  To_Locc end To_Loc,*   from (select     Sl_No,th.Sf_Code,convert(varchar,Expense_Date,23)Expense_Date,Flag,isnull(Reason,'')Reason,isnull(th.Approved_By,'')Approved_By,isnull(convert(varchar,Approved_Date,23),'')Approved_Date,isnull(Total_Amount,0)Total_Amount,isnull(DA_Mode,'')DA_Mode,isnull(Alw_Type,'')Alw_Type,isnull(From_Loc,'')From_Loc,isnull(To_Loc,'')To_Locc,isnull(Alw_Amount,0)Alw_Amount,isnull(Boarding_Amt,0)Boarding_Amt,isnull( Lc_totalAmt,0)Lc_totalAmt,isnull(Oe_totalAmt,0)Oe_totalAmt,isnull(Ldg_totalAmt,0)Ldg_totalAmt,isnull(th.drvBrdAmt,0)drvBrdAmt,isnull(Ta_totalAmt,0)Ta_totalAmt,isnull(trv_lc_amt,0)trv_lc_amt,(select HQ_Name from Mas_HQuarters  where HQ_ID=ms.Sf_HQ)HQNAME from Trans_Expense_Head2 th WITH (NOLOCK)  inner join Mas_Salesforce ms WITH (NOLOCK) on th.Sf_Code=ms.Sf_Code  where  th.sf_code='".$SF_code."' and convert(date,Expense_Date)='".$Selectdate."') A order by Expense_Date desc";
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
			$TAAddsql = "select   SlNo,Exp_Date,Exp_Code,Exp_Amt,isnull(Attachments,0)Attachments,Fk_Sl_No,Sf_Code,Expense_Type,isnull(Ukey,'')Ukey  from Trans_Additional_Expense WITH (NOLOCK) where Fk_Sl_No='".$Expense_Head[0]["Sl_No"]."' and Expense_Type='LC'   ";
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
			$Lod="select  SlNo,FK_SlNo,Lodging_Type,Eligible,Bill_Amt,WOB_Amt, isnull(Ukey,'')Ukey, Add_SF_Emp_ID LocId,Ldg_Stay_Loc,CONVERT(varchar, Stay_Date, 8) Stay_Date,isnull(Driver_Ldg_Amount,0)Driver_Ldg_Amount,Joining_Ldg_Amount,Total_Ldg_Amount,Attachment,Sf_Code,CONVERT(varchar, To_Date, 8) To_Date ,NO_Of_Days ,convert(varchar, Stay_Date, 23) Tadate,isnull(Continuous_Stay,0)Continuous_Stay,isnull(Early_Checkin,0)Early_Checkin,isnull(Late_Checkout,0)Late_Checkout,convert(varchar,Erly_Check_in,8)Erly_Check_in,convert(varchar,Erly_Check_out,8)Erly_Check_out,isnull(Ear_bill_amt,0)Ear_bill_amt,convert(varchar,lat_chec_in,8)lat_chec_in,convert(varchar,lat_check_out,8)lat_check_out,isnull(lat_bill_amt,0)lat_bill_amt from Trans_Lodging_Details WITH (NOLOCK) where FK_SlNo='".$Expense_Head[0]["Sl_No"]."' and Sf_Code='".$SF_code."' and Lodging_Type<>'' ";
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
			$TAAddsql = "select  SlNo,Exp_Date,Exp_Code,Exp_Amt,isnull(Attachments,0)Attachments,Fk_Sl_No,Sf_Code,Expense_Type,isnull(Ukey,'')Ukey from Trans_Additional_Expense WITH (NOLOCK) where Fk_Sl_No='".$Expense_Head[0]["Sl_No"]."' and Expense_Type='OE'   ";
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
			
			$TAtravelLocsql = "select   SlNo,FK_Trv_SlNo,From_P,To_P,isnull(Attachments,0)Attachments,Sf_Code,Mode,Fare,isnull(Ukey,'') Ukey,ISNULL(Alw_Eligibilty,'0')Alw_Eligibilty from Trans_Travelled_Loc lc WITH (NOLOCK) 
								left join Mas_Modeof_Travel mt WITH (NOLOCK)  on mt.MOT=lc.Mode and mt.Active_Flag=0 and mt.Division_Code=3 
								where FK_Trv_SlNo='".$Expense_Head[0]["Sl_No"]."' and iif(Mode='','0',Mode)<>'0' and Sf_Code='".$SF_code."'";
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
/*function getExpensedatenew($Stat){
	global $data;
	$div =(string) $data['divisionCode'];
		$divs = explode(",", $div . ",");
		$SF_code = (string) $data['sfCode'];
		$Owndiv = (string) $divs[0];
		if($Stat==0){
		$query = "select convert(varchar, Date_Time, 23) id,convert(varchar, Date_Time, 23)+'  '+FORMAT(convert(datetime,Date_Time), 'dddd') Datewithname,case when MOT_Name='Two Wheeler' then 200 else 500 end Maxkm from Expense_Start_Activity where   Sf_code='".$SF_code."'   and isnull(Approval_Flag,0)=1";
		return performQuery($query);
		}else{
		$Selectdate = (string) $data['Selectdate'];
		/*$query = "select st.*,isnull(FuelAmt,0) FuelAmt,isnull(('http://hap.sanfmcg.com/BikePhotos/'+st.Image_Url),'')start_Photo,isnull(('http://hap.sanfmcg.com/BikePhotos/'+st.End_Image_Url),'')End_photo,isnull(Alw_Eligibilty,0) Alw_Eligibilty,isnull((select isnull(Allowance_Value,0) from Mas_SF_AllowanceDets where Allowance_Code=(case(st.dailyAllowance) when 'HQ' then 'HQ1' when 'EXQ' then 'EX1' end) and Sf_code=st.Sf_code),0)Allowance_Value,(
	select e.SF_code SF,To_Place_Id,al.HQ_Type,Allowance_Value LdgAmt
	from Expense_Start_Activity e inner join Mas_HQuarters ihq on To_Place_Id=ihq.HQ_ID
	inner join Mas_SF_AllowanceDets al on al.Sf_code=e.Sf_code and Allowance_Code='22' and
	al.HQ_type=(Case(ihq.HQ_Type) when 'Metro' then 'MT' when 'Major' then 'MJ' when 'Others' then 'OT' else '' End)
	where cast(convert(varchar,Date_Time,101) as datetime)=@Dt)  from Expense_Start_Activity st inner join   Mas_Modeof_Travel mot on  mot.Sl_No=CAST(st.MOT AS int) where   Sf_code='".$SF_code."' and convert(date,Date_Time)='".$Selectdate."'";
		*
		$query = "exec GetDailyAllowanceDetails '".$SF_code."','".$Selectdate."'";
		$TSD = performQuery($query);
		$expense['TodayStart_Details']=$TSD;	
		$query = "exec GetDailyFuelAllowanceDetails '".$SF_code."','".$Selectdate."'";
		$FTSD = performQuery($query);
		$expense['FuelAllowance']=$FTSD;
		$query="select convert(varchar,isnull((select top 1 convert(varchar,isnull(Stay_Date,getdate()),23) from Trans_Lodging_Details where Sf_Code='".$SF_code."' and convert(date,Stay_Date)=DATEADD(day,-1,cast('".$Selectdate."' as date)) and Continuous_Stay=1),cast('".$Selectdate."' as date)),23) Stay_Date_time";
		$STDT = performQuery($query);
		$expense['Stay_Date_time']=$STDT;	
		$ExpH= "select case when DA_Mode='HQ' then HQNAME else  To_Locc end To_Loc,*   from (select     Sl_No,th.Sf_Code,convert(varchar,Expense_Date,23)Expense_Date,Flag,isnull(Reason,'')Reason,isnull(th.Approved_By,'')Approved_By,isnull(convert(varchar,Approved_Date,23),'')Approved_Date,isnull(Total_Amount,0)Total_Amount,isnull(DA_Mode,'')DA_Mode,isnull(Alw_Type,'')Alw_Type,isnull(From_Loc,'')From_Loc,isnull(To_Loc,'')To_Locc,isnull(Alw_Amount,0)Alw_Amount,isnull(Boarding_Amt,0)Boarding_Amt,isnull( Lc_totalAmt,0)Lc_totalAmt,isnull(Oe_totalAmt,0)Oe_totalAmt,isnull(Ldg_totalAmt,0)Ldg_totalAmt,isnull(Ta_totalAmt,0)Ta_totalAmt,isnull(trv_lc_amt,0)trv_lc_amt,(select HQ_Name from Mas_HQuarters  where HQ_ID=ms.Sf_HQ)HQNAME from Trans_Expense_Head2 th  inner join Mas_Salesforce ms on th.Sf_Code=ms.Sf_Code  where  th.sf_code='".$SF_code."' and convert(date,Expense_Date)='".$Selectdate."') A";
		$Expense_Head = performQuery($ExpH);
		$expense['Expense_Head']=$Expense_Head;	
		$queryn = "exec getLodingContinuous '".$SF_code."','".$Selectdate."'";
		$expense['LodDtlist'] = performQuery($queryn);
		$TAAddsql = "select   SlNo,Exp_Date,Exp_Code,Exp_Amt,isnull(Attachments,0)Attachments,Fk_Sl_No,Sf_Code,Expense_Type,isnull(Ukey,'')Ukey  from Trans_Additional_Expense where Fk_Sl_No='".$Expense_Head[0]["Sl_No"]."' and Expense_Type='LC'   ";
		$TAAditional_Exp =performQuery($TAAddsql);
		if(count($TAAditional_Exp)<1){
			$expense['Additional_ExpenseLC']=[];
		}else{
		$expense['Additional_ExpenseLC']=$TAAditional_Exp;
		for ($k = 0; $k < count($expense['Additional_ExpenseLC']); $k++){
			$TARefsql = "select  * from Trans_Ref_Details where SlNo='".$expense['Additional_ExpenseLC'][$k]['SlNo']."'";
			$TSDPerfomance = performQuery($TARefsql);
			if(count($TSDPerfomance)<1){
			$expense['Additional_ExpenseLC'][$k]['Additional']=[];
			}else{
			$expense['Additional_ExpenseLC'][$k]['Additional']=$TSDPerfomance;
			}
			}
		}
		
		
		//$Lod = "select  SlNo,FK_SlNo,Lodging_Type,Eligible,Bill_Amt,WOB_Amt, isnull(Ukey,'')Ukey, Add_SF_Emp_ID,Ldg_Stay_Loc,CONVERT(varchar, Stay_Date, 8) Stay_Date,CONVERT(varchar, Stay_Date, 103)+' '+ CONVERT(varchar, Stay_Date, 108),Driver_Ldg_Amount,Joining_Ldg_Amount,Total_Ldg_Amount,Attachment,Sf_Code,CONVERT(varchar, To_Date, 8) To_Date ,NO_Of_Days ,convert(varchar, Stay_Date, 23) Tadate,isnull(Continuous_Stay,0)Continuous_Stay,isnull(Early_Checkin,0)Early_Checkin,isnull(Late_Checkout,0)Late_Checkout,convert(varchar,Erly_Check_in,8)Erly_Check_in,convert(varchar,Erly_Check_out,8)Erly_Check_out,isnull(Ear_bill_amt,0)Ear_bill_amt,convert(varchar,lat_chec_in,8)lat_chec_in,convert(varchar,lat_check_out,8)lat_check_out,isnull(lat_bill_amt,0)lat_bill_amt from Trans_Lodging_Details where FK_SlNo='".$Expense_Head[0]["Sl_No"]."' and Sf_Code='".$SF_code."' and Lodging_Type<>''  ";
		$Lod="select  SlNo,FK_SlNo,Lodging_Type,Eligible,Bill_Amt,WOB_Amt, isnull(Ukey,'')Ukey, Add_SF_Emp_ID,Ldg_Stay_Loc,CONVERT(varchar, Stay_Date, 8) Stay_Date,Driver_Ldg_Amount,Joining_Ldg_Amount,Total_Ldg_Amount,Attachment,Sf_Code,CONVERT(varchar, To_Date, 8) To_Date ,NO_Of_Days ,convert(varchar, Stay_Date, 23) Tadate,isnull(Continuous_Stay,0)Continuous_Stay,isnull(Early_Checkin,0)Early_Checkin,isnull(Late_Checkout,0)Late_Checkout,convert(varchar,Erly_Check_in,8)Erly_Check_in,convert(varchar,Erly_Check_out,8)Erly_Check_out,isnull(Ear_bill_amt,0)Ear_bill_amt,convert(varchar,lat_chec_in,8)lat_chec_in,convert(varchar,lat_check_out,8)lat_check_out,isnull(lat_bill_amt,0)lat_bill_amt from Trans_Lodging_Details where FK_SlNo='".$Expense_Head[0]["Sl_No"]."' and Sf_Code='".$SF_code."' and Lodging_Type<>'' ";
		$Lod_Head =performQuery($Lod);
		if(count($Lod_Head)<1){
			$expense['Lodging_Head']=[];
		}else{
		$expense['Lodging_Head']=$Lod_Head;
		for ($k = 0; $k < count($expense['Lodging_Head']); $k++){
			$Lodad = "select  * from Trans_Lod_Join_Details where Fl_Ldg_Sl_No='".$expense['Lodging_Head'][$k]['SlNo']."'";
			$Lodad = performQuery($Lodad);
			if(count($Lodad)<1){
			$expense['Lodging_Head'][$k]['Additional']=[];
			}else{
			$expense['Lodging_Head'][$k]['Additional']=$Lodad;
			}
			}
		}
		$TAAddsql = "select  SlNo,Exp_Date,Exp_Code,Exp_Amt,isnull(Attachments,0)Attachments,Fk_Sl_No,Sf_Code,Expense_Type,isnull(Ukey,'')Ukey from Trans_Additional_Expense where Fk_Sl_No='".$Expense_Head[0]["Sl_No"]."' and Expense_Type='OE'   ";
		$TAAditional_ExpOE =performQuery($TAAddsql);
		if(count($TAAditional_ExpOE)<1){
			$expense['Additional_ExpenseOE']=[];
		}else{
			$expense['Additional_ExpenseOE']=$TAAditional_ExpOE;
			for ($k = 0; $k < count($expense['Additional_ExpenseOE']); $k++){
			$TARefsql = "select  * from Trans_Ref_Details where SlNo='".$expense['Additional_ExpenseOE'][$k]['SlNo']."'";
			$TSDPerfomance = performQuery($TARefsql);
			if(count($TSDPerfomance)<1){
			$expense['Additional_ExpenseOE'][$k]['Additional']=[];
			}else{
			$expense['Additional_ExpenseOE'][$k]['Additional']=$TSDPerfomance;
			}
			}
			
		}
	    $TAtravelsql = "select  * from Trans_Travelled_Details where FK_Exp_SlNo='".$Expense_Head[0]["Sl_No"]."' and Sf_Code='".$SF_code."'";
	    $Travelled =performQuery($TAtravelsql);
		if(count($Travelled)<1){
			$expense['Travelled_Details']=[];
		}else{
			$expense['Travelled_Details']=$Travelled;
		}
		$TAtravelLocsql = "select   SlNo,FK_Trv_SlNo,From_P,To_P,isnull(Attachments,0)Attachments,Sf_Code,Mode,Fare  , isnull(Ukey,'') Ukey from Trans_Travelled_Loc where FK_Trv_SlNo='".$Expense_Head[0]["Sl_No"]."' and Sf_Code='".$SF_code."'";
	    $Travelled_Loc =performQuery($TAtravelLocsql);
		if(count($Travelled_Loc)<1){
		$person = array("From_P" =>$TSD[0]["From_Place"],"To_P" =>$TSD[0]["To_Place"],"Fare" =>'0',"Mode"=>$TSD[0]["MOT_Name"],"Attachemnt"=>'0',"SlNo"=>"","FK_Trv_SlNo"=>'',"Ukey"=>''); 
		$expense['Travelled_Loc']=array($person);
		}else{
			$expense['Travelled_Loc']=$Travelled_Loc;
		}
		
		$TAclaimsql = "select  * from Da_Claim where FK_Trv_SlNo='".$Expense_Head[0]["Sl_No"]."'";
	    $TAclaimRes =performQuery($TAclaimsql);
		if(count($TAclaimRes)<1){
			$expense['Da_Claim']=[];
		}else{
			$expense['Da_Claim']=$TAclaimRes;
		}
		
		return $expense;
		}
		
		
	   
} */


$axn = $_GET['axn'];

$value = explode(":", $axn);


switch (strtolower($value[0])) {
	case "get/view":
        getDynamicView();
        break;
		case "get/menu":
        getMenu();
        break;
		case "save/view":
        saveView();
        break;
		case "upload/img":
		outputJSON(Upload_Image(0));
		break;
		case "upload/start":
		outputJSON(Upload_Image(1));
		break;
		case "upload/taimg":
		outputJSON(Upload_Image(2));
		break;
		case "save/allowance":
		outputJSON(saveAllowance(0));
		break;
		case "save/recallallowance":
		outputJSON(saveAllowance(3));
		break;
		case "update/allowance":
		outputJSON(saveAllowance(1));
		break;
		case "update/predayallowance":
		outputJSON(saveAllowance(2));
		break;
		case "get/allowance":
		getTASummary();
		break;
		case "get/travelmode":
		getTravelMode();
		break;
		case "get/daexp":
        outputJSON(getDailyAllow());
        break;
		case "get/startkmdetails":
        outputJSON(StartKmD());
        break;
		case "save/expsendtoapproval":
        outputJSON(expsendtoapproval());
        break;
		case "upload/checkinimage":
        outputJSON(SaveCheckinImage());
        break;

		case "upload/procpic":
        outputJSON(UpdateProcPic());
        break;
		case "save/daexp":
        outputJSON(saveNewDailyExp());
        break;
		
		case "save/dynamictp":
        outputJSON(saveTpEntry());
        break;
		case "get/taffdetails":
        outputJSON(getExpenseApproval());
        break;
		case "get/taapproval":
        outputJSON(getApproval());
        break;
		case "save/taapprove":
        outputJSON(saveApprove());
        break;
		case "get/fieldforce_hq":
		outputJSON(getHeadQuaters());
		break;	
		case "get/expensedate":
		outputJSON(getExpensedate(0));
		break;
		case "get/expensedatedetails":
		outputJSON(getExpensedate(1));
		break;
		
		case "get/expensedatedetailsnew":
		outputJSON(getExpensedatenew(1));
		break;
		case "get/expenseapprovallist":
		outputJSON(getExpenseList());
		break;
		case "get/expensesflist":
		outputJSON(getExpenseSFList());
		break;
		case "get/ta_image":
		outputJSON(Get_TAImage());
		break;
		case "get/worktypefields":
		$Worktype_Code = $_GET['Worktype_Code'];
		$sfcode = $_GET['sfCode'];
		outputJSON(Get_WorktypeFields($Worktype_Code,$sfcode));
		break;
		case "delete/ta_image":
		outputJSON(Delete_TAImage());
		break;
		
		case "get/taapprovehistory":
		outputJSON(getTAApprovalDets());
		break;
		case "get/expensesubdatestatus":
		outputJSON(getExpSubmitDtsStatus());
		break;
		case "save/dayplandynamic":
        outputJSON(savemydayplan());
        break;
		case "save/explodgeexception":
        outputJSON(insertExpenseLodgeException());
        break;
		case "save/editstartactivity":
		outputJSON(saveEditedMot());
		break;
		case "save/taexecptionapprove":
        outputJSON(saveDAExceptionApprove());
        break;
		case "get/vwexceptionstatus":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwTAExpecptionEntry WITH (NOLOCK) where Reporting_To_SF='$sfCode'";
        $expentry = performQuery($sql);
        outputJSON($expentry);
        break;
		case "get/execptionstatus":
        $sfCode = $_GET['sfCode'];
        $rSF = $_GET['rSF'];
        $AMod = $_GET['AMod'];
		if($AMod==0){
			$sql = "select ms.Sf_Code,ms.Sf_Name+' - '+sf_emp_id Name,convert(varchar,ex.Created_Date,103)Applied_Date,ex.DA_Type,cast(Approval_Flag as varchar)Approval_Flag,
				convert(varchar,ex.DA_Date,103)DA_Date,ex.DA_Amt,convert(varchar,ex.DA_Actual_Time,108)Actual_Time,convert(varchar,ex.DA_Early_Time,108)Early_Late_Time,FMOTName,TMOTName,
				case when Approval_Flag=1 then 'Pending' when Approval_Flag=2 then 'Reject' when Approval_Flag=0 then 'Approve' end Status,isnull(Reject_Reason,'')RejectReason,convert(varchar,Appr_Date,103)Approve_Date
				from Expense_Lodge_Exception ex WITH (NOLOCK) inner join Mas_Salesforce ms WITH (NOLOCK) on ms.Sf_Code=ex.Sf_Code
				where ex.Sf_Code='$sfCode' order by ex.Created_Date desc";
        }
		else
		{
			$sql = "select ms.Sf_Code,ms.Sf_Name+' - '+sf_emp_id Name,convert(varchar,ex.Created_Date,103)Applied_Date,ex.DA_Type,cast(Approval_Flag as varchar)Approval_Flag,
					convert(varchar,ex.DA_Date,103)DA_Date,ex.DA_Amt,convert(varchar,ex.DA_Actual_Time,108)Actual_Time,convert(varchar,ex.DA_Early_Time,108)Early_Late_Time,FMOTName,TMOTName,
					case when Approval_Flag=1 then 'Pending' when Approval_Flag=2 then 'Reject' when Approval_Flag=0 then 'Approve' end Status,isnull(Reject_Reason,'')RejectReason,convert(varchar,Appr_Date,103)Approve_Date
					from Expense_Lodge_Exception ex WITH (NOLOCK) inner join Mas_Salesforce ms WITH (NOLOCK) on ms.Sf_Code=ex.Sf_Code
					where ms.Reporting_To_SF='$rSF'	order by ex.Created_Date desc";
        }
		$exceptiondt = performQuery($sql);
        outputJSON($exceptiondt);
}
?>