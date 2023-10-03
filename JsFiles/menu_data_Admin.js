
_menuCloseDelay=500           // The time delay for menus to remain visible on mouse out
_menuOpenDelay=150            // The time delay before menus open on mouse over
_followSpeed=5                // Follow scrolling speed
_followRate=40                // Follow scrolling Rate
_subOffsetTop=10              // Sub menu top offset
_subOffsetLeft=-10            // Sub menu left offset
_scrollAmount=3               // Only needed for Netscape 4.x
_scrollDelay=20               // Only needed for Netcsape 4.x


with(menuStyle=new mm_style()){
onbgcolor="#4F8EB6";
oncolor="#ffffff";
offbgcolor="#DCE9F0";
offcolor="#6600cc";
bordercolor="#296488";
borderstyle="solid";
borderwidth=1;
separatorcolor="#2D729D";
separatorsize="1";
padding=2;
fontsize="12";
fontstyle="bold";
fontfamily="Times New Roman";
//fontfamily="Microsoft Sans Serif,Tahoma,Helvetica";
pagecolor="black";
pagebgcolor="#82B6D7";
headercolor="#000000";
headerbgcolor="#ffffff";
subimage="../../Images/arrow.gif";
subimagepadding="2";
overfilter="Fade(duration=0.2);Alpha(opacity=90);Shadow(color='#777777', Direction=135, Strength=5)";
outfilter="randomdissolve(duration=0.3)";
}
	function openWindow()
	{
		//window.open('E:/Orbit_15_12_2005/SFM/Screens/Transaction/How to Clear the Cookies.htm','','left=50,top=20,width=900,height=600,scrollbars=1,menubar=1')
		window.open('http://www.support.pmcsoftindia.com','','left=50,top=20,width=900,height=600,scrollbars=1,Address bar=1,menubar=1')
        //onclick='if(top.location!=self.location){top.location=self.location;}top.location.href="http://www.support.pmcsoftindia.com"' target="_blank">
	}
with(milonic=new menuname("Main Menu")){
style=menuStyle;
//top=70;  // menu position
//left=10;
left=0;
alwaysvisible=1;
orientation="horizontal";
aI("text=;url=../Master/Index_Admin.asp;status=;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Masters&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=Masters;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Activity  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=Activity;status=Activity;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Activity Reports &nbsp;&nbsp;showmenu=Activity Report;status=Activity Reports;");
aI("text=&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MIS Reports &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=MIS Report;status=MIS Report;");
aI("text=&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Options&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=Options;status=Options;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Home &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../../Division_Selection.Asp;status=Back To Home Page;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;LogOut &nbsp;&nbsp;&nbsp;url=../../../Logout.asp;status=Log Out;");
}
with(milonic=new menuname("Masters")){
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Product&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=Product;status=Product;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HQ&nbsp;&nbsp;showmenu=HQ;status=HQ;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Divisional&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/master/Divisional_List.asp;status=Divisional;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Territory Category&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/master/Territory_category_list.asp;status=Territory Category;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Territory&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/master/Town_list.asp;status=Town;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Qualification&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/master/Qualification_List.asp;status=Qualification;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doctor&nbsp;&nbsp;showmenu=Doctor;status=Doctor;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Product Reminder&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/master/Gift_List.asp;status=Gift;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MR Entries &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=MR Entries;status=MR Entries;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sales Force&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/master/SalesForce_List.asp;status=Sales Force;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Statewise - Holiday Fixation&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/Holiday_fixation.asp;status=Statewise - Holiday Fixation;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Distance Fixation&nbsp;&nbsp;showmenu=Distance Fixation;status=Distance Fixation;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Allowance Fixation&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/Mas_Allowence_Fixation.asp;status=Allowence Fixation;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DM - ZM Mapp&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/Map_DM_ZM.asp;status=DM - ZM Mapp;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Confirm Tour Plan&nbsp;&nbsp;url=../../ScreenMgr/master/ConformTourPlan.asp;status=Confirm Tour Plan;")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=Personal Details;status=Personal Details;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Managers HQ Area Mapp&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/frmMgr_MR_HqMap.asp;status=Managers HQ Area Mapp;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PMT - Regional Map&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/Map_PM_RM.asp;status=Managers HQ Area Mapp;");
}
with(milonic=new menuname("HQ"))
{
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;Sales&nbsp;url=../../screens/master/SSM_List.asp;status=SZ;");
aI("text=&nbsp;Divisional&nbsp;url=../../screens/master/Senior_Zonal_List.asp;status=SZ;");
aI("text=&nbsp;Zonal&nbsp;&nbsp;url=../../screens/master/Zonal_List.asp;status=Zonal;");
aI("text=&nbsp;Region&nbsp;&nbsp;url=../../screens/master/Region_List.asp;status=Region;");
aI("text=&nbsp;Area&nbsp;&nbsp;url=../../screens/master/Area_list.asp;status=Area;")
aI("text=&nbsp;Territory&nbsp;url=../../screens/master/Territory_list.asp;status=Territory;");
}
with(milonic=new menuname("Personal Details")){
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;Entry&nbsp;url=../../Screens/Reports/Employee_Details.asp;status=Entry;");
aI("text=&nbsp;View&nbsp;url=../../Screens/Reports/Employee_Details_View.asp;status=View;");

}

with(milonic=new menuname("Product")){
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;&nbsp;&nbsp;Category&nbsp;&nbsp;&nbsp;url=../../screens/master/category_list.asp;")
aI("text=&nbsp;&nbsp;&nbsp;Group&nbsp;&nbsp;&nbsp;url=../../screens/master/Type_list.asp;")
aI("text=&nbsp;&nbsp;&nbsp;Detail&nbsp;&nbsp;&nbsp;url=../../screens/master/product_master.asp;status=Detail;")
aI("text=&nbsp;&nbsp;&nbsp;Statewise - Rate Fixation&nbsp;&nbsp;&nbsp;url=../../screens/master/Rate.asp;status=Rate;")
}


with(milonic=new menuname("MR Entries")){
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;&nbsp;&nbsp;Territory &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../ScreenActivities/Master/Town_List.asp;status=SO's - Territory Creation;")
aI("text=&nbsp;&nbsp;&nbsp;Listed Doctor &nbsp;&nbsp;&nbsp;status=Detail;url=../../ScreenActivities/Reports/Doctor_List.asp;")
aI("text=&nbsp;&nbsp;&nbsp;Chemists &nbsp;&nbsp;&nbsp;&nbsp;url=../../ScreenActivities/Reports/Chemist_List.asp;status=Chemists Ctration;")
aI("text=&nbsp;&nbsp;&nbsp;Stockist &nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/master/frmStockist_Creation.asp;status=Stokist Ctration;")
}


with(milonic=new menuname("Doctor"))
{
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;&nbsp;&nbsp;Speciality&nbsp;&nbsp;&nbsp;status=Speciality;url=../../screens/master/doctorSpecialty_list.asp;")
aI("text=&nbsp;&nbsp;&nbsp;Category&nbsp;&nbsp;&nbsp;status=Speciality;url=../../screens/master/doctorCategory_list.asp;")
aI("text=&nbsp;&nbsp;&nbsp;Class&nbsp;&nbsp;&nbsp;status=Class;url=../../screens/master/DoctorClass_list.asp;")
aI("text=&nbsp;&nbsp;&nbsp;Campaign&nbsp;&nbsp;&nbsp;status=Sub-Category;url=../../screens/master/doctorSubCategory_list.asp;")
//aI("text=&nbsp;&nbsp;&nbsp;Detail&nbsp;&nbsp;&nbsp;status=Detail;url=../../screens/master/Doctor_List.asp;")
}

with(milonic=new menuname("Distance Fixation"))
{
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Territory / Territory HQ wise&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/Mas_Distance_Fixaction.asp;status=Territory wise;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;View&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Transaction/frmDistanceface_allowance.asp;status=View;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;STP Fare Chart&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Reports/RptStpFareChart.Asp;status=STP Fare Chart;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Territory HQ / Area HQ wise&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/mas_distance_fix_HQwise.asp;status=Territory HQ / Area HQ wise;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Area HQ / Regional HQ wise&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/mas_distance_fix_RegionHQwise.asp;status=Area HQ / Region HQ wise;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Regional HQ / Zonal HQ wise&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Master/mas_distance_fix_ZonalHQwise.asp;status=Region HQ / Zonal HQ wise;");
}

with(milonic=new menuname("Activity")){
style=menuStyle;
overflow="scroll";

//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sale Entry;showmenu=Sale Entry;status=Sale Entry;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sampling Pattern&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Transaction/SamplingPattern.asp;status=Sampling Pattern;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Gift / Sample Despatch;showmenu=Gift / Sample Issue;status=Gift / Sample Issue;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Target Fixation;showmenu=Target Fixation;status=Target Fixation;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Listed Doctor Approval&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=Listed Doctor Approval;status=Listed Doctor Approval;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doctor Visit Map;showmenu=Doctor Visit Map;status=Doctor Visit Map;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Leave Approvals&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Master/FrmLeaveAppView.asp;status=Leave Approvals;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Listed Doctor Approval View&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;showmenu=Listed Doctor Approval View;status=Listed Doctor Approval View;");

}
with(milonic=new menuname("Listed Doctor Approval")){
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;Addition&nbsp;url=../../ScreenMgr/Master/frmdocadd.asp;status=Addition;");
//aI("text=&nbsp;Updation&nbsp;url=../../ScreenMgr/Master/frmdocEdit.asp;status=Updation;");
aI("text=&nbsp;DeActivation&nbsp;url=../../ScreenMgr/Master/frmdocdeact.asp;status=Deletion;");
}
with(milonic=new menuname("Listed Doctor Approval View")){
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;Addition&nbsp;url=../../ScreenMgr/Master/frmdocaddvw.asp;status=Addition;");
//aI("text=&nbsp;Updation&nbsp;url=../../ScreenMgr/Master/*.asp;status=Updation;");
aI("text=&nbsp;DeActivation&nbsp;url=../../ScreenMgr/Master/frmdocdeactvw.asp;status=Deletion;");
}




with(milonic=new menuname("Doctor Visit Map")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Specialty/Categorywise &nbsp;url=../../Screens/Transaction/Category_Specialty_MapForVisits.asp;status=Doctor / Visit Mapping;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Missed Listed Doctor(Visit Freq)&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Reports/frmrptMissedListDoctTer.asp;status=Missed Listed Doctor;");

}

with(milonic=new menuname("Target Fixation")){
style=menuStyle;
overflow="scroll";	
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;General-Product Categorywise &nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Transaction/TargetFixation.asp;status=General;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;General-Product Detailwise &nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Transaction/TargetProductDetail.asp;status=General;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;General-Product Territorywise&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Transaction/Target_Territory.asp;status=General;");

}

with(milonic=new menuname("Gift / Sample Issue")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sample Product - Despatch (From HQ);url=../../Screens/Transaction/RepProductIssue.asp;status=Rep.Product Issue;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sample Despatch - Status Report&nbsp;url=../../Screens/Reports/frmRptSampleIssue_SF.asp;status=Sample Issued - Status Report;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sample Allocation Report - Regionwise &nbsp;url=../../Screens/Reports/RptSample_Allocation.asp;status=Sample Allocation Report - Regionwise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Gift Despatch (From HQ);url=../../Screens/Transaction/RepGiftIssue.asp;status=Rep.Gift Issue;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Gift Despatch - Status Report;url=../../Screens/Transaction/RptRepGiftIssue.asp;status=Rep.Gift Issue;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Gift Allocation Report - Regionwise &nbsp;url=../../Screens/Reports/RptGift_Allocation.asp;status=Gift Allocation Report - Regionwise;");
}


with(milonic=new menuname("Deactivation")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SF Member&nbsp;url=../../Screens/Transaction/deactivation.asp;status=SF Member Deactivation;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doctor&nbsp;url=../../Screens/Transaction/Doctor_DeActivate.asp;status=Doctor Deactivation;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Chemist&nbsp;url=../../Screens/Transaction/Chemist_DeActivate_test.asp;status=Chemist Deactivation;");
}


with(milonic=new menuname("Sale Entry")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Territory Wise;url=../../Screens/Transaction/AWPW_Admin.asp;status=Territory Wise;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Listed Doctor Wise;url=../../ScreenActivities/Transaction/Doctorwise_Sale_Entry.asp;status=Listed Doctor Wise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Chemist Wise;url=../../Screens/Transaction/Chemistwise_Sale_Entry_Admin.asp;status=Chemist Wise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stockist Wise;url=../../Screens/Transaction/Stockistwise_Sale_Entry_Admin.asp;status=Stockist Wise;");
}

with(milonic=new menuname("Activity Report")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tour Plan&nbsp;&nbsp;&nbsp;showmenu=Tour Plan;status=Tour Plan;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Std.Daywise Plan&nbsp;&nbsp;&nbsp;showmenu=SDP;status=SDP;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Daily Work Reports&nbsp;&nbsp;&nbsp;&nbsp;showmenu=Activity Reports;status=Daily Calls Reports;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Expense Statement&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Reports/rptAutoExpense.asp;status=Expense Statement;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Missed Call Visit Reason-MRwise&nbsp;&nbsp;&nbsp;url=../../Screens/Reports/frmrptmissedmr.asp;status=Missed Call Visit Reason MRwise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Approved Expense Summary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Reports/RptExpApp.asp;status=Expense Approved;");
}


with(milonic=new menuname("SDP")){
style=menuStyle;
overflow="scroll";	
//aI("text=Detailed;url=../../Screens/Reports/rptCall_Plan.asp;status=Detailed;");
aI("text=Listed Doctorwise;url=../../Screens/Reports/FrmStdDaywisedr.Asp;status=Listed Doctorwise;");
}

with(milonic=new menuname("Tour Plan")){
style=menuStyle;
overflow="scroll";	
aI("text=View;url=../../Screens/Master/RptTPVw.asp;status=Representative;");
}
with(milonic=new menuname("Std.Daywise Plan")){
style=menuStyle;
overflow="scroll";	
//aI("text=&nbsp;&nbsp;New&nbsp;&nbsp;status=New;");
//aI("text=&nbsp;&nbsp;Edit&nbsp;&nbsp;status=Edit;");
aI("text=&nbsp;&nbsp;View&nbsp;&nbsp;url=../../screens/Reports/rptCall_Plan.asp;status=View;");
}
with(milonic=new menuname("activity Reports")){
style=menuStyle;
overflow="scroll";
//aI("text=&nbsp;&nbsp;Entry&nbsp;&nbsp;status=Entry;");	
//aI("text=MSR/MGR;url=../../Screens/Reports/frmrptDaily_activity.asp;status=REP/MGR;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;View&nbsp;&nbsp;&nbsp;&nbsp; ;url=../../Screens/Reports/frmrpt_DCR.asp;status=View ;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Approval&nbsp;&nbsp;&nbsp;&nbsp; ;url=../../Screens/Reports/ConformDCR.asp;status=View ;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Review&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../ScreenMgr/Reports/frmrptmgrDCR.asp;status=Review;");
}
with(milonic=new menuname("MIS Report")){
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Analysis&nbsp;&nbsp;showmenu=Analysis;status=Analysis;");	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Missed Calls&nbsp;&nbsp;showmenu=Missed Calls;status=Missed Calls;");	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Visit Details&nbsp;&nbsp;showmenu=Visit Details;status=Visit Details;");	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sample Details;showmenu=Sample Details;status=Sample Details;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Gift Details;showmenu=Gift Details;status=Gift Details;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Exception&nbsp;&nbsp;showmenu=Exception;status=Exception;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doctor Details&nbsp;&nbsp;showmenu=Doctor Details;status=Doctor Details;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Others;showmenu=Others;status=Others;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Status;showmenu=Status;status=Status;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Quick Mgr Analysis;showmenu=Mgr Quick Analysis;status=Quick Analysis;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sample Issued (From HQ);showmenu=Sample Issued (From HQ);status=Sample Issued (From HQ);");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doctor Service History;url=../../screens/Reports/frmrptDoctor_Service.asp;status=Doctor Service History;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Target View;showmenu=Target Fixed;status=Target View;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DWR Delayed;url=../../screens/reports/FrmDelayedReports.asp;status=DCR Delayed;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Comparison Rep Vs Manager;url=../../screens/Reports/frmRptRepManagerCalls.asp;status=Comparison Rep Vs Manager;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Product Exposure;showmenu=Product Exposure;status=Product Exposure;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sales Analysis;showmenu=Sales Analysis;status=Sales Analysis;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sales Graph;showmenu=Graph;status=Graph;");
}


with(milonic=new menuname("Others")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Listed Doctorwise Mapping View&nbsp;&nbsp;url=../Reports/Rptdoc_Doc_Detail_MR_wise_view.asp;status=SubCategorywise Listed Drs View;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Listed Doctors - CampaignWise&nbsp;url=../Reports/frmRptListed_Doctor_Categorywise.asp;status=Listed Doctors - CategoryWise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Campaignwise Listed Drs- View;url=../Reports/RptSubCatLstDrSmpGtVst.asp;status=Sub Categorywise Listed Drs- View;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fieldforce Status - View&nbsp;&nbsp;url=../Transaction/RptlblPrint.asp;status=Fieldforce Status - View;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Productwise Listed Doctor View (As per Mapping)&nbsp;&nbsp;url=../MIS_Reports/RptMapped_Product_Listed_Drs.asp;status=Productwise Listed Doctor View;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Productwise Listed Doctor View (As per DCR Entry)&nbsp;&nbsp;url=../Reports/RptProdList.asp;status=Productwise Listed Doctor View;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Listed Doctor Detail - View&nbsp;&nbsp;url=../MIS_Reports/RptDoctor_Detail_Report.asp;status=Listed Doctor Detail - View;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Listed Doctor Status - View&nbsp;&nbsp;url=../MIS_Reports/RptDoctor_Status_Report.asp;status=Listed Doctor Status - View;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Product - Specialitywise - Listed Doctor View&nbsp;&nbsp;url=../MIS_Reports/RptDoctor_Detail_product_specialtywise.asp;status=Product-Specialitywise-Listed Doctor View;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Expense Statement Entry & Print Status&nbsp;&nbsp;url=../Reports/Monthwise_ExpenseProcess.asp;status=Expense Statement Entry & Print Status");
}


with(milonic=new menuname("Graph")){
style=menuStyle;
overflow="scroll";	
//aI("text=&nbsp;Product Exposure&nbsp;url=../../screens/Reports/frmRptProdExposureGraph.asp;status=Product Exposure;");
aI("text=&nbsp;MR - Monthwise Sales&nbsp;url=../../screens/Reports/frmRptmr_monthsale.asp;status=MR - Monthwise Sales;");
aI("text=&nbsp;Area - Monthwise Sales&nbsp;url=../../screens/Reports/frmRptArea_Monthsale.asp;status=Area - Monthwise Sales;");
aI("text=&nbsp;Month - Areawise Sales&nbsp;url=../../screens/Reports/frmRptmonth_Areasale.asp;status=Month - Areawise Sales;");
aI("text=&nbsp;Month/Area-MR wise Sales&nbsp;url=../../screens/Reports/frmRptmonth_Area_mrsale.asp;status=Month/Area-MR wise Sales;");
aI("text=&nbsp;MR - Month/Product wise Sales&nbsp;url=../../screens/Reports/frmRptmr_month_Productsale.asp;status=MR - Month/Product wise Sales;");
aI("text=&nbsp;Area - Month/Product wise Sales&nbsp;url=../../screens/Reports/frmRptarea_month_Productsale.asp;status=Area - Month/Product wise Sales;");
//aI("text=&nbsp;MR- Productwise Sales&nbsp;url=../../screens/Reports/frmSalesProdMrMonthwise_Chart.asp;status=MR- Productwise Sales;");
}


with(milonic=new menuname("Analysis")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;DWR&nbsp;url=../../screens/Reports/frmrptwork_analysis.asp;status=DCR;");
aI("text=&nbsp;DWR(Individual)&nbsp;url=../../screens/Reports/frmrptwork_Analysis_Ind.asp;status=DCR;");


aI("text=&nbsp;Manager&nbsp;&nbsp;showmenu=Manager;status=Manager;");	
aI("text=&nbsp;MR&nbsp;&nbsp;showmenu=MR;status=MR;");
}


with(milonic=new menuname("Manager")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Work Analysis-Managerwise&nbsp;url=../../screens/Reports/frmMgrWrkAnalysis.asp;status=MGR;");
aI("text=&nbsp;Manager - Analysis - All&nbsp;url=../../screens/Reports/rptMgrAnalysis.asp;status=MGR;");
aI("text=&nbsp;Territory HQwise&nbsp;url=../../screens/Reports/frmTerrwiseVstMonitor.asp;status=MGR;");
aI("text=&nbsp; Manager Joint Work Summery&nbsp;url=../../screens/Reports/frmrptMgr_Analysis.asp;status= Manager Joint Work Summery;");
}
with(milonic=new menuname("MR")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Joint Work&nbsp;url=../../screens/Reports/frmrptMGR_Joint_Work.asp;status=Mgr- Joint Work;");
aI("text=&nbsp;Speciality & CategoryWise&nbsp;url=../../screens/Reports/frmSpecCatDr_Visit.asp;status=Speciality & CategoryWise;");
}


with(milonic=new menuname("Mgr Quick Analysis")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Field Work&nbsp;url=../../screens/Reports/frmrptfieldwork_Analysis.asp;status=Field Work;");
aI("text=&nbsp;Work Type&nbsp;url=../../screens/Transaction/FrmWorktypeStatus.asp;status=Work Type;");
aI("text=&nbsp;Call Average&nbsp;url=../../screens/Transaction/FrmCallAverage.asp;status=Call Average;");

//aI("text=&nbsp;Territorywise&nbsp;url=../../screens/Reports/frmrptTerritory_Analysis.asp;status=Territory;");
}

with(milonic=new menuname("Missed Calls")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Territorywise&nbsp;url=../../screens/Reports/frmrptMissedCallTer.asp;status=DCR;");
aI("text=&nbsp;Listed Doctorwise&nbsp;url=../../screens/Reports/frmrptMissedCall.asp;status=MGR;");
}



with(milonic=new menuname("Visit Details")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;TerritoryWise&nbsp;url=../../screens/Reports/frmrptMkt_Coverage.asp;status=TerritoryWise;");
aI("text=&nbsp;ChemistWise&nbsp;url=../../screens/Reports/frmrptChem_Coverage.asp;status=ChemistWise;");
aI("text=&nbsp;Listed Doctors - CategoryWise&nbsp;url=../../screens/Reports/frmrptadminDoctor_coverage.asp;status=Listed Doctors - CategoryWise;");
aI("text=&nbsp;Listed Doctors - ClassWise&nbsp;url=../../screens/Reports/frmrptadminDoctor_Class_Coverage.asp;status=Listed Doctors - ClassWise;");
aI("text=&nbsp;Listed Doctors - SpecialityWise&nbsp;url=../../screens/Reports/frmrptadminDocspecwise_coverage.asp;status=Listed Doctors - SpecialityWise;");

}


with(milonic=new menuname("Visit Analysis")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Listed DoctorWise&nbsp;url=../../screens/Reports/frmrptDoc_Visit.asp;status=TerritoryWise;");
aI("text=&nbsp;TerritoryWise&nbsp;url=../../screens/Reports/frmrptTer_Visit.asp;status=ChemistWise;");
}

with(milonic=new menuname("Visit Summary")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Doctor/Categorywise&nbsp;url=../../screens/Reports/frmrptadminDoctorSummary_coverage.asp;status=Doctor/Category;");
//aI("text=&nbsp;TerritoryWise&nbsp;url=../../screens/Reports/frmrptTer_Visit.asp;status=ChemistWise;");
}

with(milonic=new menuname("Others")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Drs-SubCategory-MRwise View&nbsp;&nbsp;url=../../screens/Reports/Rptdoc_Doc_Vs_SubCat_view.asp;status=Drs-SubCategory-MR wise View;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Chemist-MRwise&nbsp;url=../../screens/Reports/;status=;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FieldForce Visit Deviation&nbsp;url=../../screens/Reports/frmRptVisit_deviation.asp;status=FieldForce Visit Deviation;");
}

with(milonic=new menuname("Sample Issued (From HQ)")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Area wise&nbsp;url=../../screens/Reports/frmRptSampleIssue_Reg_Qtr.asp;status=Area wise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sampling Pattern&nbsp;url=../../screens/Reports/frmRptSamplingPattern_Qtr.asp;status=Sampling Pattern;");
}

with(milonic=new menuname("Graph")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Product Exposure&nbsp;url=../../screens/Reports/frmRptProdExposureGraph.asp;status=Product Exposure;");
aI("text=&nbsp;MR - Monthwise Sales&nbsp;url=../../screens/Reports/frmRptmr_monthsale.asp;status=MR - Monthwise Sales;");
aI("text=&nbsp;Area - Monthwise Sales&nbsp;url=../../screens/Reports/frmRptArea_Monthsale.asp;status=Area - Monthwise Sales;");
aI("text=&nbsp;Month - Areawise Sales&nbsp;url=../../screens/Reports/frmRptmonth_Areasale.asp;status=Month - Areawise Sales;");
aI("text=&nbsp;Month/Area-MR wise Sales&nbsp;url=../../screens/Reports/frmRptmonth_Area_mrsale.asp;status=Month/Area-MR wise Sales;");
aI("text=&nbsp;MR - Month/Product wise Sales&nbsp;url=../../screens/Reports/frmRptmr_month_Productsale.asp;status=MR - Month/Product wise Sales;");
aI("text=&nbsp;Area - Month/Product wise Sales&nbsp;url=../../screens/Reports/frmRptarea_month_Productsale.asp;status=Area - Month/Product wise Sales;");
aI("text=&nbsp;MR- Productwise Sales&nbsp;url=../../screens/Reports/frmSalesProdMrMonthwise_Chart.asp;status=MR- Productwise Sales;");
}

with(milonic=new menuname("Product Exposure")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Detail&nbsp;url=../../Screens/reports/frmRptProdExposure.asp;status=Detailed Product Exposure;");
aI("text=&nbsp;Speciality/Category Wise&nbsp;url=../../Screens/reports/frmSpecCatProdExposure.asp;status=Speciality/Category Wise;");
}

with(milonic=new menuname("Work Analysis")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;View&nbsp;&nbsp;&nbsp;url=../../screens/Reports/frmrptwork_analysis.asp;status=View;");
}
with(milonic=new menuname("Market Coverage")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;REP/MGR&nbsp;url=../../screens/Reports/frmrptMkt_Coverage.asp;status=REP/MGR;");
}

with(milonic=new menuname("Exception")){
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;Tour Plan - MR&nbsp;url=../Reports/frmTourplanDiv.asp;status=Tour Plan;");
aI("text=&nbsp;Tour Plan - For Managers&nbsp;url=../Reports/Rpt_Mgr_Tp_Deviation.asp;status=Tour Plan;");

}

with(milonic=new menuname("Doctor Wise")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Doctor/Category Wise&nbsp;url=../../screens/Reports/frmrptadminDoctor_coverage.asp;status=Doctor/Category Wise;");
aI("text=&nbsp;Doctor/Speciality Wise&nbsp;url=../../screens/Reports/frmrptadminDocspecwise_coverage.asp;status=Doctor/Speciality Wise;");
}
with(milonic=new menuname("Visit Details(Cons)")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Doctor/Category Wise&nbsp;url=../../screens/Reports/frmrptadminDoctor_coverageCon.asp;status=Doctor/Category Wise;");
aI("text=&nbsp;Doctor/Speciality Wise&nbsp;url=../../screens/Reports/frmrptadminDocspecwise_coverageCon.asp;status=Doctor/Speciality Wise;");
}
with(milonic=new menuname("PCR")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;Doctor Response&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Reports/Doctor_Response.asp;status=Doctor Response;");
aI("text=&nbsp;&nbsp;&nbsp;PCR Vs Target Analysis;url=../../screens/Reports/frmrptPCR_TargetAnaly.asp;status=PCR Vs Target Analysis;");
//aI("text=&nbsp;&nbsp;&nbsp;PCR Vs ROI;url=../../screens/Reports/frmRptROI_PCR.asp;status=PCR Vs ROI;");
}

with(milonic=new menuname("Sales Analysis")){
style=menuStyle;
overflow="scroll";	
//aI("text=&nbsp;Sample Vs Sales;url=../../screens/Reports/frmSamples_Sales.asp;status=Sample Vs Sales;");
//aI("text=&nbsp;Target Vs Sales;url=../../screens/Reports/frmRptTargetFixationsales.asp;status=Target Vs Sales;");
aI("text=&nbsp;Stockistwise Sales;url=../../screens/Reports/frmRptStockistsalesDet.asp;status=Stockist Vs Sales;");
aI("text=&nbsp;Sales & Stock Statement;url=../../screens/Reports/frmrptStock_Statement.Asp;=Status=Sales & Stock Statement;")
//aI("text=&nbsp;Doctorwise;url=../../screens/Reports/frmRptDoctorwise_POB.asp;status=Doctorwise;");
//aI("text=&nbsp;Weekly Sales View;url=../../Screens/Reports/Rptwklysalrpt.asp;status=Weekly Sales View;");
//aI("text=&nbsp;Chemistwise Sales;url=../../screens/Reports/frmRptChemistssales.asp;status=Chemist Vs Sales;");
//aI("text=&nbsp;Territorywise Sales;url=../../screens/Reports/TerritorywiseSalesdet.asp;status=Territorywise Sales;");
//aI("text=&nbsp;Territory - Productwise Sales;url=../../screens/Reports/TerritorywiseProductwiseSales.asp;status=Territory - Productwise Sale;");
//aI("text=&nbsp;Sale Listed Drs - MRwise&nbsp;url=../../Screens/reports/frmrptsaledrs.asp;status=Sale Listed Drs - MRwise;");

}
with(milonic=new menuname("Sample Details")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Sample Issued-FieldForce Wise;url=../../Screens/Reports/frmSamplePro_issued.asp;status=Sample Issued-Rep Wise;");
aI("text=&nbsp;Sample Issued-Doctor Wise;url=../../Screens/Reports/frmRptSampleIssue_DR.asp;status=Sample Issued-Doctor Wise;");
aI("text=&nbsp;Sample Issued-Speciality / Category Wise;url=../../Screens/Reports/frmSamplePro_issued_Doc_Spe_Cat_Wise.asp;status=Sample Issued-Speciality / Category Wise;");
aI("text=&nbsp;Sample Issued-Region Wise;url=../../Screens/Reports/frmSamplePro_issued_Region_Wise.asp;status=Sample Issued-Region Wise;");
aI("text=&nbsp;Sample Issued-Territory Wise;url=../../Screens/Reports/frmSamplePro_issued_Territory_Wise.asp;status=Sample Issued-Territory Wise;");
}

with(milonic=new menuname("Gift Details")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Gift Issued-FieldForce wise;url=../../Screens/Reports/frmGiftPro_issued.asp;status=Gift Issued-Rep wise;");
aI("text=&nbsp;Gift Issued-Doctor Wise;url=../../Screens/Reports/frmGiftPro_issued_doc_wise.asp;status=Gift Issued-Doctor Wise;");
aI("text=&nbsp;Gift Issued-Speciality / Category Wise;url=../../Screens/Reports/frmGiftPro_issued_Doc_Spe_Cat_Wise.asp;status=Gift Issued-Speciality / Category Wise;");
aI("text=&nbsp;Gift Issued-Region Wise;url=../../Screens/Reports/frmGiftPro_issued_Region_Wise.asp;status=Gift Issued-Region Wise;");
aI("text=&nbsp;Gift Issued-Territory Wise;url=../../Screens/Reports/frmGiftPro_issued_Territory_Wise.asp;status=Gift Issued-Territory Wise;");
//aI("text=&nbsp;Gift Issued-Month Wise;status=Gift Issued-Month Wise;");
//aI("text=&nbsp;Gift Issued-Rep Wise;status=Gift Issued-Rep Wise;");
//aI("text=&nbsp;Gift Issued-Speciality Wise;url=../../Screens/Reports/frmGiftPro_issued_Doc_Spe_Cat_Wise.asp;status=Gift Issued-Speciality Wise;");
}

with(milonic=new menuname("Status")){
style=menuStyle;
overflow="scroll";	
//aI("text=&nbsp;DCR Status&nbsp;url=../../screens/Reports/frmrptDaily_statusWithRegion.asp;status=DCR Status;");
aI("text=&nbsp;DWR Status&nbsp;url=../../screens/Reports/frmrptDaily_statusSAMAIL.asp;status=DCR Status;");
aI("text=&nbsp;TourPlan Status&nbsp;url=../../screens/Reports/frmrptTourPlanStatus.asp;status=TourPlan Status;");
//aI("text=&nbsp;MGR - Joint Work&nbsp;url=../../screens/Reports/frmrptMGR_Joint_Work.asp;status=MGR - Joint Work;");
//aI("text=&nbsp;DCR Summary&nbsp;url=../../screens/Reports/frmrptDaily_statusSumma.asp;status=DCR Summary;");

}
with(milonic=new menuname("Daily Status")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;REP/MGR&nbsp;url=../../screens/Reports/frmrptDaily_statusShort.asp;status=REP/MGR;");
}
with(milonic=new menuname("Exposure Analysis")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Product Wise&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Reports/frmRptProdExp_Analysis.asp;status=Product Wise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Gift Wise&nbsp;&nbsp;&nbsp;&nbsp;url=../../screens/Reports/frmRptGiftExp_Analysis.asp;status=Gift Wise;");
}
with(milonic=new menuname("Doctor Vs SampleIssued")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sample Issued - Speciality Wise;url=../../screens/Reports/frmrptDoctor_product.asp;status=Sample Issued - Speciality Wise;");
}
with(milonic=new menuname("Doctor Details"))
{
style=menuStyle;
overflow="scroll";	
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Region wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Reports/frmRptDoctor_Regionwise.asp;status=Region wise;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Area wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Reports/frmRptDoctor_Areawise.asp;status=Area wise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MR wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Reports/frmRptDoctor_Repwise.asp;status=Rep wise;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TerritoryWise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Reports/frmrptDoctor_Territorywise.asp;status=TerritoryWise;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doctor DataBase&nbsp;&nbsp;url=../../screens/Reports/frmrptDoctor_database.asp;status=Doctor DataBase;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DD Summary - Product&nbsp;&nbsp;url=../../screens/Reports/frmrptDoctor_AMwise.asp;status=DD Summary - Product;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DD Summary - Doctor&nbsp;&nbsp;url=../../screens/Reports/frmrptDoctor_MRwise.asp;status=DD Summary - Doctor;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Exceded the Max No of Listed Dr Status&nbsp;&nbsp;url=../../screens/Reports/RptExstngdocchg.asp;status=Exceded the Max No of Listed Dr Status;");
}
with(milonic=new menuname("Tourplan Status")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;REP/MGR;url=../../screens/Reports/frmrptTourPlanStatus.asp;status=REP/MGR;");
}

with(milonic=new menuname("Target Fixed")){
style=menuStyle;
overflow="scroll";	
//aI("text=&nbsp;Product  Category Wise;url=../../screens/Reports/frmRptTargetFixation.asp;status=Product  Category Wise;");
aI("text=&nbsp; Product Detail Wise;url=../../screens/Reports/frmRptStarProdTargetFixation.asp;status= Product Detail Wise;");
}
with(milonic=new menuname("Target Achieved")){
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;Incentive Calculation&nbsp;url=../../screens/Transaction/IncentCalculation.asp;status=Incentive Calculation;");
aI("text=&nbsp;General;url=../../screens/Reports/frmRptTargetAchieved.asp;status=General;");
aI("text=&nbsp;StarProd&Levelwise;url=../../screens/Reports/frmRptRepTargetAchieved.asp;status=StarProd&Levelwise;");
}
with(milonic=new menuname("Options"))
{
style=menuStyle;
overflow="scroll";	
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Change PassWord&nbsp;&nbsp;url=../../Screens/Master/adminpwd.asp;=Status=Change PassWord;")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Deactivation&nbsp;&nbsp;&nbsp;url=../../Screens/Master/Deactivation.asp;=Status=Deactivation;")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Vacant MR Login - Access&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Master/MgrRepAccess.asp;=Status=Vacant MR Login - Access;")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Update/Delete;showmenu=Update/Delete;status=Update/Delete;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Setup;showmenu=Setup;status=Setup;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Mail Box&nbsp;&nbsp;&nbsp;&nbsp;url=../../Mail/Mail.asp;status=Mail Box;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Forum&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../Mail/Forum.asp;status=Forum;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Notice Board&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Master/Notice_Board.asp;status=Notice Board;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Flash News&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../Mail/Flash_News_Updated.asp;status=Flash News;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;DWR Delayed release&nbsp;&nbsp;url=../Transaction/DrcDelayRelease.asp;status=DCR Delayed release;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Leave Status&nbsp;&nbsp;url=../../Screens/Reports/RptLeaveAppView.asp;=Status=Leave Status;")
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Activities;showmenu=OptionActivities;status=Activities;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;MR - MR Transfer (Listed Drs(Sponser/Business)/Chemists)&nbsp;&nbsp;&nbsp;url=../../Screens/Master/Drs_Chm_Stk_Trans_MrtoMr.asp;=Status=MR - MR Transfer (Listed Drs/Chemists/Stockists);")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;TP Edit&nbsp;&nbsp;&nbsp;url=../../Screens/Master/frmTPEdit.asp;=Status=MR - MR Transfer (Listed Drs/Chemists/Stockists);")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;MR - MR Stockist Sales Transfer &nbsp;&nbsp;&nbsp;url=../../Screens/Transaction/frmStockist_Sale_Transfer.asp;=Status=MR - MR Stockist Sales Transfer;")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Doctor Service/Business Transfer&nbsp;&nbsp;&nbsp;url=../../Screens/Transaction/frmSASDrTransfer.asp;=Status=MR - MR Transfer (Listed Drs/Chemists/Stockists);")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Upload Drs Excel&nbsp;&nbsp;&nbsp;url=../../Screens/Master/frmUPLDocMaster.asp;=Status=Upload;")
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Upload Chm Excel&nbsp;&nbsp;&nbsp;url=../../Screens/Master/frmUPLChmMaster.asp;=Status=Upload;")

}
with(milonic=new menuname("Setup"))
{
style=menuStyle;
overflow="scroll";	
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Mail Parameters&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Master/Mas_Mail_Parameters.asp;status=Mail Box;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Expense Parameters&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Master/Mas_Expense_Parameters.asp;status=Expense Parameter;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Permission For Mgrs - To Access Vacant MR&nbsp;&nbsp;url=../../screens/Master/PermissionForVacMR.asp;status=Permission For Mgrs - To Access Vacant MR;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;TP / DWR &nbsp;&nbsp;url=../../screens/Transaction/SetupTPDCR.asp;status=TP / DCR Setup;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;MSR&nbsp;&nbsp;url=../../screens/Transaction/RepresentativeSetup.asp;status=Representative;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Managers&nbsp;&nbsp;url=../../screens/Transaction/SetupDCR_MGR.asp;status=Manager;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Missed Drs Visited Reason&nbsp;&nbsp;url=../../screens/Master/Mas_Listed_Doctor_Visit_Reason.asp;status=Missed Reason;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Service Requisition SlNo Setting&nbsp;&nbsp;url=../../screens/Master/SlNo_Settings.asp;status=SlNo_Settings;");
}
with(milonic=new menuname("Update/Delete"))
{
style=menuStyle;
overflow="scroll";
aI("text=&nbsp;&nbsp;&nbsp; Managers- Core Drs Allocation &nbsp;&nbsp;url=../../Screens/Master/Drs_Permission.asp;status=Manager Drs Allocation;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;User Name Edit&nbsp;&nbsp;url=../Transaction/Usernameedit.asp");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Bulk DWR Starting Date - Change;url=../../Screens/Transaction/BulkStartingDtUpdation.asp;status=DCRDelete;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;TP&nbsp;&nbsp;url=../../Screens/Master/TpEditDelete.asp;status=TP Deletion;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Mail Deletion&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Transaction/Mail_Deletion.asp;status=Mail Deletion;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;Mail Deletion With Backup&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/Transaction/Mail_Deletion_Backup.asp;statue=Mail Deletion;");
aI("text=&nbsp;&nbsp;&nbsp;&nbsp;DWR Edit&nbsp;&nbsp;url=../../Screens/Master/frmDCREdit.asp;status=DCR Edit;");
//aI("text=&nbsp;&nbsp;&nbsp;&nbsp;DWR Deletion;url=../../Screens/Transaction/DCR_Deletion.asp;status=DCRDelete;");
}
with(milonic=new menuname("OptionActivities")){
style=menuStyle;
overflow="scroll";	

//aI("text=&nbsp;Service Requisition Report;url=../../Screens/Reports/frmService_Report_Form_CRL.asp;status=Service Requisition Report;");
//aI("text=&nbsp;Service Requisition Sanction Report;url=../../Screens/Reports/frmService_Sanction_Report_Form_CRL.asp;status=Service Requisition Sanction Report;");
//aI("text=&nbsp;Service Requisition Sanction Doctorwise Report;url=../../Screens/Reports/frmService_Sanction_Doctor_Report_Form.asp;status=Service Requisition Sanction Doctorwise;");
//aI("text=&nbsp;Service Requisition Rejection Report;url=../../Screens/Reports/frmService_Rejection_Report_Form_CRL.asp;status=Service Requisition Rejection Report;");
aI("text=&nbsp;Joining Report View&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url=../../Screens/reports/Frm_Joining_ReportAM.asp;status=Joining Report;");
}


drawMenus();