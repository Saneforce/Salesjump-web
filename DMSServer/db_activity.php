<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
session_start();
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
//include "dbConn.php";
include "utils.php";

$data = json_decode($_POST['data'], true);

function getDynamicView(){
	global $data;	
	$slno = (string) $data['slno'];	
	$arrfield=array();
	 $query1 = "select * from Mas_Forms where Frm_ID='".$slno."' and Active_Flag='0'";
	 $rests=performQuery($query1);
	$query = "select *,IIF(Filter_Text<>'','true','false')Isfilterable from Mas_Forms_Fields where Frm_ID='".$slno."' and Active_Flag='0' order by Order_by";	
	$res=performQuery($query);
	if (count($res)>0) 
	{
		for ($il=0;$il<count($res);$il++)
		{
			$id=$res[$il]["Control_id"];
			if($id=="4" || $id=="6" || $id=="13"){
				$tblclm=$res[$il]["Fld_Src_Field"];
				$defnam=explode(",",$tblclm);
				$qu = "select ";
				$qucol="";
				for($k=0;$k<sizeof($defnam);$k++){
					if($qucol!="") $qucol = $qucol.",";
					$qucol = $qucol.$defnam[$k];
				}
				$qu = "select ".$qucol." from ".$res[$il]["Fld_Src_Name"]." ";	
				$res[$il]['inputss']=$qu;
				$res[$il]['header']=$rests[0]["Frm_Name"];
				$res[$il]['type']=$rests[0]["Targt_Frm"];
				$res[$il]['target']=$rests[0]["Frm_Type"];
				$res[$il]['input']=performQuery($qu);
				$res[$il]['code']=$defnam[0];
				$res[$il]['name']=$defnam[1];
				/*$tblclm=$res[$il]["Fld_Src_Field"];
				$defnam=explode(",",$tblclm);
				$qu = "select ".$defnam[0].",".$defnam[1]." from ".$res[$il]["Fld_Src_Name"]." ";	
				$res[$il]['inputss']=$qu;
				$res[$il]['header']=$rests[0]["Frm_Name"];
				$res[$il]['type']=$rests[0]["Targt_Frm"];
				$res[$il]['target']=$rests[0]["Frm_Type"];
				$res[$il]['input']=performQuery($qu);
				$res[$il]['code']=$defnam[0];
				$res[$il]['name']=$defnam[1];*/
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
				if($res[$il]["Fld_Src_Name"]=="vwFarmers"){
					$qu=$qu." where Center_Code=1 ";
				}
				
				$res[$il]['input']=performQuery($qu);
				$res[$il]['header']=$rests[0]["Frm_Name"];
				$res[$il]['type']=$rests[0]["Targt_Frm"];
				$res[$il]['target']=$rests[0]["Frm_Type"];
				
				//echo $qu;
			} 
			else if($id=="25"){
				$tblclm=$res[$il]["Fld_Src_Field"];
				$defnam=explode(",",$tblclm);
				$qu = "select ";
				$qucol="";
				for($k=0;$k<sizeof($defnam);$k++){
					if($qucol!="") $qucol = $qucol.",";
					$qucol = $qucol."isnull(".$defnam[$k].",0)".$defnam[$k];
				}
				$qu = "select ".$qucol." from ".$res[$il]["Fld_Src_Name"]." ";	
				$quer=performQuery($qu);
				$res[$il]['inputss']=$qu;
				$res[$il]['header']=$rests[0]["Frm_Name"];
				$res[$il]['type']=$rests[0]["Targt_Frm"];
				$res[$il]['target']=$rests[0]["Frm_Type"];
				$res[$il][$defnam[0]]=$quer[0][$defnam[0]];
				$res[$il]['value']=$defnam[0];
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
	 $divs = explode(",", $div . ",");
    $div = (string) $divs[0];
	$query = "select * from Mas_Forms where Division_Code='".$div."' and Active_Flag='0'  and isChild ='N' ";	
	$res=performQuery($query);
	return outputJSON($res);
}
function getDashBoardList(){
	global $data;
	$div = (string) $data['div'];	
	$divs = explode(",", $div . ",");
    $div = (string) $divs[0];
	$query = "exec getProcurementDashBoardList '".$div."' ";	
	$res=performQuery($query);
	return $res;
}
function UpdateSign(){
    global $URL_BASE;

$target_dir = "..\photos/";
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
}
else 
{
	  $message =  "Profile Has Been Updated";

	/* $UpdateMyAccount = "update Mas_Empolyee set Emp_Name='".$Username."' ,Emp_Email='".$Email."',Emp_Mobile='".$Mobile."',Emp_DOB='".$DOB."',Emp_ContactAdd_One='".$Address."'    where Emp_ID='" . $Emp_ID . "'";
    
    $Pass = performQuery($UpdateMyAccount);
	 */
 $success = true;
 
}
 
$response["success"] = $success;
$response["message"] = $message;
$response["url"] = $upload_url;
  return $response;
}

function UpdateProcPic(){
    global $URL_BASE;

$target_dir = "..\Proc_Photos/";
$target_file_name = $target_dir.basename($_FILES["file"]["name"]);
$target_file = basename($_FILES["file"]["name"]);
$response = array();


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
}
else 
{
	  $message =  "Profile Has Been Updated";

	/* $UpdateMyAccount = "update Mas_Empolyee set Emp_Name='".$Username."' ,Emp_Email='".$Email."',Emp_Mobile='".$Mobile."',Emp_DOB='".$DOB."',Emp_ContactAdd_One='".$Address."'    where Emp_ID='" . $Emp_ID . "'";
    
    $Pass = performQuery($UpdateMyAccount);
	 */
 $success = true;
 
}
 
$response["success"] = $success;
$response["message"] = $message;
$response["url"] = $upload_url;
  return $response;
}

function proc_dashboard(){
	global $data;
	$sf = (string) $data['SF'];	
	$result=array();
	$query="select * from vwFarmers where FM_Active_flag=0 and sf_Code='".$sf."' ";
	$res1=performQuery($query);
	$res1["tot"]=count($res1);
	$query="select * from Farmer_Survey_3  where  MONTH(Created_Date)=MONTH(GETDATE()) and Year(Created_Date)=Year(GETDATE())and sf_Code='".$sf."' ";
	$res5=performQuery($query);
	$res1["visit"]=count($res5);
	$result["farm"]=$res1;
	$query="select * from Mas_Centres";
	$res2=performQuery($query);
	$res2["tot"]=count($res2);
	$res2["visit"]="0";
	$result["center"]=$res2;
	$query="select * from Mas_Chill_Centre";
	$res2=performQuery($query);
	$res2["tot"]=count($res2);
	$res2["visit"]="0";
	$result["chill"]=$res2;
	return $result;
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
	   $query = "select Frm_Table,isChild from Mas_Forms where Frm_Name='".$key."'";
	   //echo $query;
	   $restt["tab"]=$query;
	   $res=performQuery($query);
	   //echo $res[0]["Frm_Table"];
	   $qu="(";
	   $vals=" select ";
	   //print_r($data[$k]);
	   foreach($val as $key => $val){
		   //echo $key . ': ' . $val;
		   if($key=="SF"){
			   $vals=$vals."'".$val."',"; 
		   }
		   if($key=="date"){
			   $vals=$vals."'".$val."',"; 
		   }
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
		   $qu=trim($qu,",");//substr($qu,0,strlen($qu)-7);
		   $vals=trim($vals,",");//substr($vals, 0, strlen($vals)-4);
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
$restt["success"]=$rest;
return outputJSON($restt);

	
}
function saveAllowance($Stat){
	global $data;
	$div=(string) $data['div'];
	$Datetime=date('Y-m-d H:i:s');
	$Dateonly=date('Y-m-d');
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
   $divisionCode = explode(",", $divCode);
   $sfCode=(string) $data['sf'];
   $km=(string) $data['km'];
   $url=(string) $data['url'];
 $rmk=(string) $data['rmk'];
 $mod=(string) $data['mod'];
 $sql;
 if($Stat==0){
		$sql = "SELECT max(Sl_No)+1 as Sl_No FROM Expense_Start_Activity";
		$topr = performQuery($sql);
		$pk =  (int)$topr[0]['Sl_No'];
		$sql = "insert into Expense_Start_Activity(Sf_code,Division_Code,Date_Time,Start_Km,Image_Url,Remarks,MOT,Approval_Flag,Sl_No) values( '".$sfCode."','".$Owndiv."','".$Datetime."','".$km."','".$url."' ,'".$rmk."','".$mod."','1','" . $pk . "')";
 }
 else{
		$sql = "update Expense_Start_Activity set Enddateand_time='".$Datetime."',End_Km='".$km."',End_Image_Url='".$url."',End_MOT='".$mod."',End_remarks='".$rmk."' where sf_Code='".$sfCode."' and convert(date,Date_Time)='".$Dateonly."'";
 }
 performQuery($sql);
 
 $resp["success"] = true;
 $resp["Query"] = $sql;
 return $resp;
}


function getTASummary(){
	global $data;
	$sfCode=(string) $data["sf"];
	$Ta_Date = (string) $data["Ta_Date"];
	//$sfCode=(string) $data['rSF'];
	//$sfCode=(string) $data['AMod'];
       
		
		$sql = "select  cast (convert(date,Date_Time) as varchar) Start_date,Date_Time Start_Time,Start_Km ,case when MOT=11 then 'BUS' when MOT=12 then 'Bike'  else 'No'  end Start_MOT,Enddateand_time End_Time,End_Km ,case when End_MOT=11 then 'BUS' when End_MOT=12 then 'Bike'  else 'No' end End_MOT,case when Approval_Flag=1 then 'Pending' when Approval_Flag=2 then 'Approved' else 'Reject' end TAStatus,case when Approval_Flag='1' then 'rgb(255,165,0) !important'  when Approval_Flag='2' then 'rgb(3,192,60)  !important' else 'rgb(255,0,0)  !important' end StusClr from Expense_Start_Activity where  sf_code='".$sfCode."' and convert(date,Date_Time)='".$Ta_Date."'";
		$TAStartandEnd = performQuery($sql);
		
		$sql = "select cast (convert(date,eDate) as varchar) Start_date, eDate, * from Trans_Daily_User_Expense where sf_code='".$sfCode."' and  convert(date,eDate)='".$Ta_Date."' ";
		$User_Expense =performQuery($sql);
		$expense['TAStartandEnd']=$TAStartandEnd;
        $expense['User_Expense']=$User_Expense;
        outputJSON($expense);
}

function getTAApproval(){
	global $data;
	$sfCode=(string) $data["sf"];
	 
        $sql = "select * from ViewTravelAllowance where Reporting_To_SF='".$sfCode."' ";
        $vwTAlowance = performQuery($sql);
		

		$sql = "select * from View_User_Expense where sf_code='".$sfCode."'";
		$User_Expense =performQuery($sql);
		$expense['TAStartandEnd']=$vwTAlowance;
        $expense['User_Expense']=$User_Expense;
        outputJSON($expense);
}
function getDailyAllow() {
	global $data;
    $div = (string) $data['div'];
	//echo $div;
	$date= (string) $data['Ta_Date'];
    //$divs = explode(",", $div . ",");
    //$Owndiv = (string) $divs[0];
	//$date=date('Y-m-d');
	$sfCode=(string) $data["sf"];
    $query = "select ID,Allowance_Name Name,Max_Allowance,Attachemnt from Mas_Allowance_Type where Type=1 and Division_Code='". $div ."' and user_enter=1 and active_flag=0";
	$ExpenseWeb=performQuery($query);
	$query = "select * from Expense_Start_Activity where Sf_code='".$sfCode."'  and convert(date,Date_Time)='".$date."'";
	$TodayExpense=performQuery($query);
	
	 $result['ExpenseWeb']=$ExpenseWeb;
        $result['TodayExpense']=$TodayExpense;
    return $result;
	
}
function getTravelMode(){
	global $data;
	$sfCode=(string) $data["sf"];
	
	$sql="select * from Expense_Start_Activity where convert(date, Date_Time)=convert(date,GetDate()) and Sf_code='".$sfCode."' ";
	$User_Expense =performQuery($sql);
        outputJSON($User_Expense);
}
 function saveDailyExp(){
	global $data;
	$dateTime=date('Y-m-d H:i:s');
	$sfCode=(string) $data["sf"];
	$Owndiv=(string) $data["div"];
	
	$exp=$data['dailyExpense'];
	$ea=$data['EA'];
	$cap=$data['ActivityCaptures'];
	$restt=array();
	
	 for ($i = 0; $i < count($data['dailyExpense']); $i++) 
    {
	
		 $sql = "insert into Trans_Daily_User_Expense(SF_Code,eDate,expCode,expName,Amt,Division_Code,LUpdtDate,Ukey,Image_Url) select '" . $sfCode . "','".$dateTime."','" . $exp[$i]["ID"] . "','" . $exp[$i]["Name"] . "','" . $exp[$i]["amt"] . "','" . $Owndiv . "',getDate(),'" . $ea[0]["Ukey"] . "' ,'" . $exp[$i]["imgData"] . "' " ;
                    performQuery($sql);
					echo $sql;
					$restt["first"]=$sql;
	}
	$sql1="insert into  Trans_Expense_Traveldetails(sf_code,Division_code,MOC,Remarks,Start_Image,FromDeparture,End_Image,ToArrival,Date_Time,Fare,FromPlace,ToPlace,Leos,Ukey,FromPlace_Name,ToPlace_Name,Bus_Support_Doc,DateofExpense)values('".$sfCode."','".$Owndiv ."','" . $ea[0]["MOT"] . "'
            ,'".$ea[0]["remarks"]."' ,'".$ea[0]["Start_image"]."','".$ea[0]["Start_Km"]."','".$ea[0]["Stop_image"]."','".$ea[0]["Stop_km"]."', '".$dateTime."' ,'".$ea[0]["BusFare"]."','".$ea[0]["Workplace"]."' ,'".$ea[0]["TodayworkRoute"]."' ,'" . $ea[0]["LEOS"] . "','" . $ea[0]["Ukey"] . "','".$ea[0]["Workplace_Name"]."' ,'".$ea[0]["TodayworkName"]."' ,'".$ea[0]["EventcaptureUrl"]."','".$ea[0]["DateOfExp"]."')";
        performQuery($sql1);
		$restt["second"]=$sql;
	
	for ($i = 0; $i < count($cap); $i++) 
    {
		$sql = "insert into Activity_Event_Captures(Activity_Report_Code,imgurl,title,remarks,Division_Code,Identification,Insert_Date_Time,DCRDetNo) values( '".$sfCode."','" . $cap[$i]["imgurl"] . "','" . $cap[$i]["title"] . "','" . $cap[$i]["remarks"] . "','".$Owndiv."','Expense','".$dateTime."','" . $ea[0]["Ukey"] . "')";
                    performQuery($sql); 
					$restt["third"]=$sql;
	} 
	$restt["success"]="true";
	return $restt;

} 

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
		outputJSON(UpdateSign());
		break;
		case "save/allowance":
		outputJSON(saveAllowance(0));
		break;
		case "update/allowance":
		outputJSON(saveAllowance(1));
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
		case "upload/procpic":
        outputJSON(UpdateProcPic());
        break;
		case "save/daexp":
        outputJSON(saveDailyExp());
        break;
		case "get/taapproval":
        outputJSON(getTAApproval());
        break;
		case "get/dashboard":
        outputJSON(proc_dashboard());
        break;
		case "get/dashboard_particulars":
        outputJSON(getDashBoardList());
		break;
		
}
?>