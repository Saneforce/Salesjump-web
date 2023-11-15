<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="CustomFieldFrom.aspx.cs" Inherits="MasterFiles_CustomFieldFrom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
             <link href="../css_new/1.12.1/HapNew.css" type="text/css" rel="Stylesheet" />
            <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
            <link type="text/css" rel="stylesheet" href="../css/style.css" />    

            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        </head>
        <body>
            <form id="Form2" runat="server">
                <div class="card" style="padding:10px !important; margin-top:0px !important; " id="AddModuleFields">
                    <input type="hidden" id="moduleId" class="moduleId" />
                    <input type="hidden" id="fieldId" class="fieldId" />
                    <div class="row" style="margin-bottom: 1rem!important;">
                        <div class="col-lg-12 sub-header">
                            Custom Forms & Fields
                           
                        </div>
                    </div>
                    <div class="row">
                        <div class="row" style="margin-bottom: 1rem!important;">
                            <div class="col-xs-12">
                                <div class="col-xs-6 col-center-block" style="float:left !important;margin-right:10%;">
                                    <div class="col-xs-3 col-sm-3" style="width: 24%!important;">
                                        <label id="modulName"><span style='Color:Red'>*</span>&nbsp;Module Name :</label>
                                    </div>
                                    <div class="col-xs-5 col-sm-5" style="padding-left:10px; ">
                                        <select id="ddlmodule" class="form-control ddlmodule">
                                            <option value="0" >--Select--</option>
                                           <%-- <option value="1" selected="selected" >Field Force</option>
                                            <option value="2" >Distributor</option>
                                            <option value="3" >Retailer</option>
                                            <option value="4" >Route</option>--%>
                                        </select>
                                    </div>
                                    <div class="col-xs-2 col-sm-2" style="width: 24%!important">
                                        <button type="button" style="margin: -7px !important; padding: 9px 6px !important; font-weight:bold !important; border-radius: 0 !important;font-size: 18px !important; background-color: #FFFFFF !important;" class="btn btn-demo btnadd" data-toggle="modal" data-target="#myModal3" id="btnAddModule" >+</button>
                                    </div>
                                </div>
                                <div class="col-xs-6" style="float:right !important;margin-top:-4% !important">
                                     <button type="button" class="btn btnadd" data-toggle="modal" data-target="#myModal2" style="float: right; background-color: #19a4c6; color: white;" id="btnAdd" >
                                Add Fields
                            </button>
                           <%-- <a href="CustomFieldFrom_Add.aspx" id="btnAdd" class="btn" data-toggle="tooltip" title="Some tooltip text!" style="float: right; background-color: #1a73e8; color: white;">Add</a>                            --%>
                                </div>
                            </div>
                            
                        </div>
                            <br />
                            <div class="card-body table-responsive" style="overflow-x: auto;">
                                <div style="white-space: nowrap">
                                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                                    <label style="float: right;padding-left:10px;">Show
                                        <select class="data-table-basic_length" aria-controls="data-table-basic" style="width:100px">
                                            <option value="10">10</option>
                                            <option value="25">25</option>
                                            <option value="50">50</option>
                                            <option value="100">100</option>
                                        </select>
                                        entries</label>
                                </div>                              
                                <br />
                                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                                    <thead class="text-warning">
                                        <tr style="white-space: nowrap;"> 
                                            <th style="text-align: left;">S.No</th>
                                            <th style="text-align: left;">Module Name</th>
                                            <th style="text-align: left;">Group Name</th>
                                            <th style="text-align: left;">Field Name</th>
                                            <th style="text-align: left;">Field Type</th>
                                            <th style="text-align: left;">Field Length</th>                                            
                                            <th style="text-align: left;">Mandatory</th>                                            
                                            <th style="text-align: left;">Deactivate</th>
                                            <th style="text-align: left;">Edit</th>                                                    
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                                
                                <div class="row" style="padding: 5px 0px">
                                    <div class="col-sm-5">
                                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                                    </div>
                                    <div class="col-sm-7">
                                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                            <ul class="pagination" style="float: right; margin: -11px 0px">
                                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>    
                    </div>                   
                </div>            
                
                <div class="container demo">
                    <!-- Modal -->
                    <div class="modal right fade" style="z-index:1031 !important;margin-top:-50px;" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="margin-top:50px !important;padding:16px !important">
                                <div class="modal-header">
                                    <button type="button" class="close" id="btnclose" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="myModalLabel2">Form Fields</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row" style="margin-bottom: 1rem !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-2">
                                                <label for="finame" style="padding-top: 5px !important;"><span style='Color:Red'>*</span>&nbsp;Field Name</label>
                                            </div>

                                            <div class="col-xs-12 col-sm-4">
                                                <input type="text" name="finame" id="finame" autocomplete="off" class="col-xs-12" />
                                            </div>

                                            <div class="col-xs-12 col-sm-2">
                                                <label for="finame" style="padding-top: 5px !important;"><span style='Color:Red'>*</span>&nbsp;Field Type  </label>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-4">
                                                <select id="ddltypes" class="form-control ddltypes">
                                                    <option value="0" selected="selected">--Select--</option>
                                                    <option value="L">Label</option>
                                                    <option value="TA">Text</option>
                                                    <option value="N">Number</option>
                                                    <option value="D">Date</option>
                                                    <option value="T">Time</option>
                                                    <option value="S">Selection</option>                                                  
                                                    <option value="R">Radio Button List</option>                                                  
                                                    <option value="C">Check Box List </option>
                                                    <option value="F">File</option>
                                                   <%-- <option value="LC">List Card</option>
                                                    <option value="LA">List Action</option>--%>
                                                </select>
                                            </div>
                                        </div>                                            
                                    </div>                                   
                                    <br />
                                    <div class="row" style="margin-bottom: 1rem !important">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-3">
                                                <label id="fGroupName"><span style='Color:Red'>*</span>&nbsp;Group Name</label>
                                            </div>

                                            <div class="col-xs-12 col-sm-4" style="padding-left:3px !important">
                                                <select id="ddlfgroup" class="form-control ddlfgroup col-xs-12">
                                                    <option value="0" >--Select--</option>
                                                   
                                                </select>                                                
                                            </div>

                                            <div class="col-xs-12 col-sm-2" style="width: 24%!important">
                                                <button type="button" style="margin: -7px !important; padding: 9px 6px !important; font-weight:bold !important; border-radius: 0 !important;font-size: 18px !important; background-color: #FFFFFF !important;" class="btn btn-demo btnadd" data-toggle="modal" data-target="#myModal4" id="btnAddFgModule" >+</button>
                                            </div>                                                                                        
                                        </div>
                                    </div>

                                    <div class="row" style="margin-bottom: 1rem!important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-3 daterow">
                                                <label style="padding-top: 5px;">Daterange</label>
                                            </div>

                                            <div class="col-xs-12 col-sm-3 daterow">
                                                <input class='tgl tgl-skewed' id="drange" type='checkbox' value="R" />
                                                <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="drange"></label>
                                            </div>

                                            <div class="col-xs-12 col-sm-3 timerow">
                                                 <label style="padding-top: 5px;">Timerange</label>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-3 timerow">
                                                <input class='tgl tgl-skewed' id="trange" type='checkbox' value="R" />
                                                 <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="trange"></label>
                                            </div>
                                        </div>                                                                                  
                                    </div>                                  
                                                                     
                                    <div class="row" style="margin-bottom: 1rem!important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-3 Mandrow">
                                                <label style="padding-top: 5px;">Mandatory Field</label>
                                            </div>

                                            <div class="col-xs-12 col-sm-3 Mandrow">
                                                <input class='tgl tgl-skewed' name="mand" id="mand" type="checkbox" />
                                                <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="mand"></label>
                                            </div>

                                            <div class="col-xs-12 col-sm-3 Maxlenrow">
                                                 <label for="maxleng" style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Max Length</label>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-3 Maxlenrow">
                                                <input type="number" name="maxleng" id="maxleng" class="col-xs-12" />
                                            </div>
                                        </div>                                                                                  
                                    </div>      

                                    <div class="row hiderow" id="phonenrow" style="margin-bottom: 1rem!important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-3">
                                                <label style="padding-top: 5px;">Number</label>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="col-xs-12 col-sm-4">
                                                    <input class='tgl tgl-skewed' name="numtype" id="phonen" type='radio' value="P" />
                                                    <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="phonen"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row hiderow" id="numberrow" style="margin-bottom: 1rem!important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-3">
                                                <label style="padding-top: 5px;">Currency</label>
                                            </div>
                                            <div class="col-xs-12 col-sm-9">
                                                <div class="col-xs-12 col-sm-3">
                                                    <input class='tgl tgl-skewed' id="cuurency" type='radio' name="numtype" value="C" />
                                                    <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="cuurency"></label>
                                                </div>
                                                
                                                <div class="col-xs-12 col-sm-6">
                                                    <select id="currlist">
                                                        <option value="">Select</option>
                                                        <option value="&#36;">&#36; - Australia Dollar</option>
                                                        <option value="&#8364;">&#8364; - Euro</option>
                                                        <option value="&#8377;">&#8377; - India Rupee</option>
                                                        <option value="&#165;">&#165; - Japan Yen</option>
                                                        <option value="&#8361;">&#8361; - Korea Won</option>
                                                        <option value="&#163;">&#163; - United Kingdom Pound</option>
                                                        <option value="&#36;">&#36; - United States Dollar</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="textrow" style="margin-bottom: 1rem!important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <label style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Text Type</label>
                                            </div>
                                            <div class="col-xs-12 col-sm-8">
                                                <div class="col-xs-12 col-sm-12">
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                                        <input id="stext" name="texttype" type='radio' value="S" />
                                                        <label style="padding-left: 7px; white-space: nowrap;" for="stext">Single Line</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                                        <input id="mtext" name="texttype" type='radio' value="M" />
                                                        <label style="padding-left: 7px; white-space: nowrap;" for="mtext">Multiple Line</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row hiderow" id="selectrow" style="margin-bottom: 1rem!important; display: none;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <label style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Selection</label>
                                            </div>
                                            <div class="col-xs-12 col-sm-8">
                                                <div class="col-xs-12 col-sm-12">
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                                        <input id="sslec" name="seltype" type='radio' value="S" />
                                                        <label style="padding-left: 7px; white-space: nowrap;" for="sslec">Single</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                                        <input id="mselc" name="seltype" type='radio' value="M" />
                                                        <label style="padding-left: 7px; white-space: nowrap;" for="mselc">Multiple</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="datarow" style="margin-bottom: 1rem!important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <label style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Data</label>
                                            </div>
                                            <div class="col-xs-12 col-sm-8">
                                                <div class="col-xs-12 col-sm-12">
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                                        <input id="master" name="dataf" type='radio' value="M" />
                                                        <label style="padding-left: 7px; white-space: nowrap;" for="master">Master</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                                        <input id="others" name="dataf" type='radio' value="O" />
                                                        <label style="padding-left: 7px; white-space: nowrap;" for="others">Others</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row Mtablerow" id="Mtablerow" style="margin-bottom: 1rem!important;padding-top:11px !important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <label style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Master Tables</label>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-8">
                                                <select id="mtables"><option value="0" >-- Select --</option></select>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row hiderow Fieldrow" id="Fieldrow" style="margin-bottom: 1rem!important;padding-top:11px !important; ">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-12">
                                                <label style="padding-top: 5px;">Fields</label>
                                            </div>
                                        </div>
                                    </div>
                                     
                                    <div class="row hiderow Fieldrow" id="ValueFRow" style="margin-bottom: 1rem!important;padding-top:11px !important; ">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="col-xs-12 col-sm-4">
                                                    <label for="fitype" style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Value</label>
                                                </div>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-8">
                                                <select id="ddlvaluef"></select>
                                            </div>
                                        </div>
                                    </div>
                                     
                                    <div class="row hiderow Fieldrow" id="TextFRow" style="margin-bottom: 1rem!important;padding-top:11px !important; ">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="col-xs-12 col-sm-4">
                                                    <label for="fitype" style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Text</label>
                                                </div>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-8">
                                                <select id="ddltextf"></select>
                                            </div>
                                        </div>
                                    </div>
                                     
                                    <div class="row hiderow" id="Templaterow" style="margin-bottom: 1rem!important; display: none;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <label for="fitype" style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Custom Templates</label>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-8 dropdown">
                                                <input list="custempl" class="autoc ui-autocomplete-input" style="width: 100%;" id="custt" />
                                                <datalist id="custempl"></datalist>
                                            </div>
                                        </div>
                                    </div>
                                     
                                    <div class="row hiderow" id="Customvrow" style="margin-bottom: 1rem!important; display: none;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <label for="fitype" style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;Custom Values</label>
                                            </div>
                                            <div class="col-xs-12 col-sm-8">
                                                <textarea id="custemps" class="col-xs-12" style="resize: vertical; height: 100px;"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row hiderow" id="filerow" style="margin-bottom: 1rem!important; display: none;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-2">
                                                <label style="padding-top: 5px;"><span style='Color:Red'>*</span>&nbsp;File</label>
                                            </div>
                                            <div class="col-xs-12 col-sm-8">
                                                <div class="col-xs-12 col-sm-12">
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                                        <input id="selupl" type='checkbox' name="flupl" value="S" />
                                                        <label style="padding-left: 7px; white-space: nowrap;" for="selupl">Selection</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                                        <input id="cameraupl" type='checkbox' name="flupl" value="C" />
                                                        <label style="padding-left: 7px; white-space: nowrap;" for="cameraupl">Camera</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     
                                    <div class="row hiderow" id="Filter" style="margin-bottom: 1rem!important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-4">
                                                <label for="ffilter" style="padding-top: 5px;">Filter</label>
                                            </div>
                                            <div class=" col-xs-12 col-sm-3">
                                                <select class="ddlFilter"></select>
                                            </div>
                                            <div class="col-xs-12 col-sm-3">
                                                <select class="ddleFilter"></select>
                                            </div>                                            
                                            <div class="col-xs-12 col-sm-2">
                                                <input type="button" onclick="addFilter()"  
                                                    style="float: right; background-color: #1a73e8;text-decoration-color:white;" 
                                                    class="btn btn-primary" value="+" />
                                            </div>
                                        </div>
                                    </div>

                                     <div class="row" style="margin-bottom: 1rem!important;padding-top:11px !important;">
                                        <div class="col-xs-12 col-sm-12">
                                            <div class="col-xs-12 col-sm-3 Accessrow">
                                                <label style="padding-top: 5px;">Access(App)</label>
                                            </div>

                                            <div class="col-xs-12 col-sm-3 Accessrow">
                                                <input class='tgl tgl-skewed' name="accwa" id="accwa" type="checkbox" />
                                                <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="accwa"></label>
                                            </div>

                                            <div class="col-xs-12 col-sm-3 Sortingrow hidden">
                                                 <label for="maxleng" style="padding-top: 5px;">Sorting Number</label>
                                            </div>
                                            
                                            <div class="col-xs-12 col-sm-3 Sortingrow hidden">
                                                <input type="number" name="sortingnum"  id="sortingnum" class="col-xs-12" />
                                            </div>
                                        </div>                                                                                  
                                    </div>      
                                    
                                </div>
                                
                                <div class="modal-footer">                                    
                                    <button type="button" style="background-color: #1a73e8;" class="btn btn-primary svfields" id="svfields">Save</button>
                                </div>
                            </div><!-- modal-content -->
                        </div><!-- modal-dialog -->
                    </div>
                    <!-- modal -->

                     <!-- Modal -->
                    <div class="modal right fade" style="z-index:1031 !important;margin-top:-50px;" id="myModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="margin-top:50px !important;padding:16px !important">
                                <div class="modal-header">
                                    <button type="button" class="close" id="btnclosem" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title mtitle" id="myModalLabel3">New Custom Form </h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <label for="finame">Module Name&nbsp;<span style='Color:Red'>*</span></label>
                                            </div>

                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtpagename" runat="server" CssClass="form-control" />
                                                <%--<input type="text" name="finamem" id="finamem" autocomplete="off" class="form-control" />--%>
                                            </div>                                            
                                        </div>   
                                    </div>
                                    <br />
                                    <div class="row hidden">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <label for="ftname">Module Type&nbsp;<span style='Color:Red'>*</span></label>
                                            </div>
                                            <div class="col-md-5">
                                                <select id="moduletype" class="form-control">                                                    
                                                    <option value="M" selected="selected">Master</option>
                                                    <option value="C" >Custom</option>
                                                </select>                                                 
                                            </div>
                                        </div>   
                                    </div>
                                    <br />
                                    <div class="row hidden">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <label for="ftname">Module Category&nbsp;<span style='Color:Red'>*</span></label>
                                            </div>
                                            <div class="col-md-5">
                                               <select id="moduleCat" class="form-control">                                                    
                                                    <option value="T" selected="selected">Transaction</option>
                                                    <option value="V" >View</option>
                                                </select>       
                                            </div>
                                        </div>   
                                    </div>                                

                                </div>
                                
                                <div class="modal-footer">                                    
                                    <button type="button" style="background-color: #1a73e8;" class="btn btn-primary svfields" id="svfieldsm">Save</button>
                                </div>
                            </div><!-- modal-content -->
                        </div><!-- modal-dialog -->
                    </div>
                    <!-- modal -->

                      <!-- Modal -->
                    <div class="modal right fade" style="z-index:1031 !important;margin-top:-50px;" id="myModal4" tabindex="-1" role="dialog" aria-labelledby="myModalLabel4">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="margin-top:50px !important;padding:16px !important">
                                <div class="modal-header">
                                    <button type="button" class="close" id="btnclosegm" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title mgtitle" id="myModalLabel4">Group</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label for="finame">Group Name&nbsp;<span style='Color:Red'>*</span></label>
                                            </div>

                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtFGroupName" MaxLength="120"  runat="server" CssClass="form-control" />
                                                <%--<input type="text" name="txtFGroupName" id="txtFGroupName" autocomplete="off" class="form-control" />--%>
                                            </div>                                            
                                        </div>   
                                    </div>                                                                               

                                </div>
                                
                                <div class="modal-footer">                                    
                                    <button type="button" style="background-color: #1a73e8;" class="btn btn-primary svfields" id="svfieldgsm">Save</button>
                                </div>
                            </div><!-- modal-content -->
                        </div><!-- modal-dialog -->
                    </div>
                    <!-- modal -->

                </div> 
                <!-- container -->
               

            </form>
            <script type="text/javascript">
                var Mtabs = [], custabs = [], MasFrms = [], FldArr = [], FtblCols = [];//change
                var FrmsandFlds = []; var CTList = []; var CTCList = [];
                var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "ModuleName,Field_Name,Field_Col,FieldType,Mandate,Status";
                var masflds = []; let hefild = '';
                masflds = [{ ControlID: 1, ControlName: 'TAS' }, { ControlID: 2, ControlName: 'TAM' }, { ControlID: 3, ControlName: 'N' }, { ControlID: 4, ControlName: 'SSM' }, { ControlID: 5, ControlName: 'SSO' },
                { ControlID: 6, ControlName: 'SMM' }, { ControlID: 7, ControlName: 'SMO' }, { ControlID: 8, ControlName: 'D' }, { ControlID: 9, ControlName: 'DR' }, { ControlID: 10, ControlName: 'RM' },
                { ControlID: 11, ControlName: 'T' }, { ControlID: 12, ControlName: 'TR' }, { ControlID: 13, ControlName: 'CM' }, { ControlID: 14, ControlName: 'NC' }, { ControlID: 15, ControlName: 'FS' },
                { ControlID: 16, ControlName: 'FC' }, { ControlID: 17, ControlName: 'FSC' }, { ControlID: 18, ControlName: 'NP' }, { ControlID: 19, ControlName: 'LCM' }, { ControlID: 20, ControlName: 'RO' },
                { ControlID: 21, ControlName: 'CO' }, { ControlID: 22, ControlName: 'L' }, { ControlID: 23, ControlName: 'LA' },]

                function loadPgNos() {
                    prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
                    Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
                    $(".pagination").html("");
                    TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
                    if (isNaN(prepg)) prepg = 0;
                    if (isNaN(Nxtpg)) Nxtpg = 2;
                    //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
                    selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
                    if ((Nxtpg) == pgNo) {
                        selpg = (parseInt(TotalPg)) - 7;
                        selpg = (selpg > 1) ? selpg : 1;
                    }
                    spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
                    for (il = selpg - 1; il < selpg + 7; il++) {
                        if (il < TotalPg)
                            spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
                    }
                    spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
                    $(".pagination").html(spg);

                    $(".paginate_button > a").on("click", function () {
                        pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
                    });
                }

                $(".data-table-basic_length").on("change", function () {
                    pgNo = 1;
                    PgRecords = $(this).val();
                    ReloadTable();

                });

                $("#tSearchOrd").on("keyup", function () {
                    if ($(this).val() != "") {
                        shText = $(this).val().toLowerCase();
                        Orders = MasFrms.filter(function (a) {
                            chk = false;
                            $.each(a, function (key, val) {
                                if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                                    chk = true;
                                }
                            });
                            return chk;
                        });
                    }
                    else
                        Orders = MasFrms;
                    ReloadTable();

                });
                
                $('.btnadd').on('click', function () {
                    var FieldId = $('.fieldId').val();
                    if (FieldId == "0") {
                        clearFields();
                        $('#btnAddFgModule').removeAttr('disabled');
                        $('#ddlfgroup').removeAttr('disabled');

                        $('#ddltypes').removeAttr('disabled');
                    }

                    $('#btnAddFgModule').removeAttr('disabled');
                    $('#ddlfgroup').removeAttr('disabled');
                    $('#ddltypes').removeAttr('disabled');
                    //var sel = document.getElementById("ddlfgroup");
                    //var text = sel.options[sel.selectedIndex].text;

                    loadFieldGroupList();
                });

                $('#btnAddFgModule').on('click', function () {                  
                    var ModuleName = $("#ddlmodule option:selected").text();

                    var Fromname = ModuleName + "  " + "Group";

                    $('.mgtitle').empty();
                    $('.mgtitle').append(Fromname);
                });

                $('#btnAddModule').on('click', function () {
                    var ModuleName = $("#ddlmodule option:selected").text();

                    var Fromname = ModuleName + "  " + "Form";

                    $('.mtitle').empty();
                    $('.mtitle').append(Fromname);
                });

                $('#btnclose').on('click', function () {
                    clearFields();

                    $('#btnAddFgModule').removeAttr('disabled');
                    $('#ddlfgroup').removeAttr('disabled');

                    $('.fieldId').val("0");
                    $('#ddltypes').val('0');
                    
                    FiledTypeOnChange();

                    
                });

                $('#svfieldgsm').on('click', function () {

                    var ModuleId = $("#ddlmodule").val();
                    if ((ModuleId == "" || ModuleId == "0" || ModuleId == null)) {
                        alert('Please Select Module Name !!');
                        $("#ddlmodule").focus();
                        return false;
                    }
                                        
                    var txtFGroupName = $('#<%=txtFGroupName.ClientID%>').val();
                    if (txtFGroupName == "") {
                        alert('Please Enter Group Name !!');
                        $('#<%=txtFGroupName.ClientID%>').focus();
                        return false;
                    }

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "CustomFieldFrom.aspx/AddCustomFormFieldGroup",
                        data: "{'ModuleId':'" + ModuleId +"' ,'txtFGroupName':'" + txtFGroupName + "'}",
                        dataType: "json",
                        success: function (msg) {
                            var frmresult = msg.d;
                            //alert(msg.d);
                            //var frmresult = JSON.parse(msg.d) || [];

                            if (msg.d == "Form Filed Group Already Exists") {
                                alert('Form Filed Group Already Exists !!');
                                FiledTypeOnChange(); clearFields();
                                //$('#myModal2').modal('hide');
                                window.location.href = "CustomFieldFrom.aspx";
                            }
                            else if (msg.d == 'New Form Field Group Created') {
                                alert('Group Created Successfully !!');
                                frmcr = false;
                                FiledTypeOnChange(); clearFields();
                                                                
                                $('#myModal2').show();

                                loadFieldGroupList();

                                //window.location.href = "CustomFieldFrom.aspx";

                                //$('#myModal2').hide();
                            }
                            else if (msg.d == 'Form Field Group Not Created') {
                                alert('Form Field Group Not Created !! ');
                                frmcr = false;
                                FiledTypeOnChange(); clearFields();
                                //$('#myModal2').hide();
                                window.location.href = "CustomFieldFrom.aspx";
                            }
                            else {
                                //console.log('Field Updated Successfully !!');
                                FiledTypeOnChange(); clearFields(); window.location.href = "CustomFieldFrom.aspx";
                            }
                        },
                        error: function (msg) {
                            alert(JSON.stringify(msg));
                            frmcr = false;
                        }

                    });

                });

                $('#svfieldsm').on('click', function () {

                    //var ModuleName = $("#finamem").val();
                    var ModuleName = $('#<%=txtpagename.ClientID%>').val();
                    if (ModuleName == "") {
                        alert('Please Enter Module Name !!');
                        $('#<%=txtpagename.ClientID%>').focus();
                        return false;
                    }

                    var ModuleType = $("#moduletype").val(); 
                    //if ((ModuleType == "" || ModuleType == "0" || ModuleType == null)) {
                    //    alert('Please Select Module Type !!');
                    //    $("#moduletype").focus();
                    //    return false;
                    //}

                    var ModuleCate = $("#moduleCat").val();
                    //if ((ModuleCate == "" || ModuleCate == "0" || ModuleCate == null)) {
                    //    alert('Please Select Module Category !!');
                    //    $("#moduleCat").focus();
                    //    return false;
                    //}

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "CustomFieldFrom.aspx/AddCustomForm",
                        data: "{'ModuleName':'" + ModuleName + "','ModuleType':'" + ModuleType + "','ModuleCate':'" + ModuleCate + "'}",
                        dataType: "json",
                        success: function (msg) {
                            var frmresult = msg.d;
                            //alert(msg.d);
                            //var frmresult = JSON.parse(msg.d) || [];
                            
                            if (msg.d == "Form Module Already Exists") {
                                alert('Form Module Already Exists !!');
                                FiledTypeOnChange(); clearFields();
                                //$('#myModal2').modal('hide');
                                window.location.href = "CustomFieldFrom.aspx";
                            }
                            else if (msg.d == 'New Form Module Created') {
                                alert('New Form Module Successfully !!');
                                frmcr = false;
                                FiledTypeOnChange(); clearFields();
                                window.location.href = "CustomFieldFrom.aspx";
                                //$('#myModal2').hide();
                            }
                            else if (msg.d == 'Form Module Not Created') {
                                alert('Form Module Not Created !! ');
                                frmcr = false;
                                FiledTypeOnChange(); clearFields();
                                //$('#myModal2').hide();
                                window.location.href = "CustomFieldFrom.aspx";
                            }                            
                            else {
                                //console.log('Field Updated Successfully !!');
                                FiledTypeOnChange(); clearFields(); window.location.href = "CustomFieldFrom.aspx";
                            }
                        },
                        error: function (msg) {
                            alert(JSON.stringify(msg));
                            frmcr = false;
                        }

                    });

                });
                
                $('#svfields').on('click', function () {
                    var FieldType = $('#ddltypes').val();
                    //alert(FieldType);
                    var sfldtyp = FieldType;
                    var MaxLen = 0;
                    var Fld_Src_Name = "";
                    var Fld_Src_Field = "";
                    var currtyp = '';
                    var currtyp = '';
                    var smtable = '';
                    var svalfld = '';
                    var stxtfld = '';
                    var custtxt = '';
                    var custempsval = '';
                    var listcardfields = '';
                    var lctarget = '0';
                    var cusxml = '<ROOT>';
                    var altqry = '';
                    var Filter_Text = '';//change
                    var Filter_Value = '';
                    var FGroupId = '';
                   

                    var ModuleName = $("#ddlmodule").val();
                    if (ModuleName == "0") {
                        alert('Please Select Module Name !!');
                        $("#ddlmodule").focus();
                        return false;
                    }


                    FGroupId = $("#ddlfgroup").val();
                    if (FGroupId == "0") {
                        alert('Please Select Group Name !!');
                        $("#FGroupId").focus();
                        return false;
                    }


                    var FieldName = $("#finame").val();
                    if (FieldName == "") {
                        alert('Please Enter Field Name !!');
                        $("#finame").focus();
                        return false;
                    }

                    if (FieldType == "" || FieldType == "0") {
                        alert('Please Select Field Type !!');
                        $("#ddltypes").focus();
                        return false;
                    }

                    switch (FieldType) {
                        case 'L':
                            sfldtyp += "";
                            MaxLen = "";
                            ModuleName = $('#ddlmodule').val();
                            break;
                        case 'TA':
                            if ($('input[type="radio"][name="texttype"]:checked').length < 1) {
                                alert('Select the Text Type');
                                sels = false;
                                return false;
                            }
                            sfldtyp += $('input[type="radio"][name="texttype"]:checked').val();
                            //MaxLen = ($('#maxleng').val() == '') ? $('#maxleng').val() : '0';
                            MaxLen = $('#maxleng').val();

                            if ((MaxLen == "" || MaxLen == null)) {
                                alert('Please Enter Max Length');
                                $('#maxleng').focus();
                                sels = false;
                                return false;
                            }
                            break;
                        case 'N':
                            if (($('input[type="radio"][name="numtype"]:checked').val() == 'C') && ($('#currlist').val() == '')) {
                                alert('Select the Currency');
                                sels = false;
                                $('#currlist').focus();
                                return false;
                            }
                            //if (($('input[type="radio"][name="numtype"]:checked').val() == 'P') && ($('#maxleng').val() == '')) {
                            //    alert('Select the Currency');
                            //    sels = false;
                            //    return false;
                            //}
                            sfldtyp += $('input[type="radio"][name="numtype"]:checked').val() || '';
                            currtyp = $('#currlist').val();
                            //maxlen = ($('#maxleng').val() != '') ? $('#maxleng').val() : 0;
                            //MaxLen = ($('#maxleng').val() == '') ? $('#maxleng').val() : '0';
                            MaxLen = $('#maxleng').val();

                            if ((MaxLen == "" || MaxLen == null)) {
                                alert('Please Enter Max Length');
                                $('#maxleng').focus();
                                sels = false;
                                return false;
                            }
                            break;
                        case 'D':
                            sfldtyp += ($('#drange').is(":checked") == true) ? ($('#drange').val()) : '';
                            break;
                        case 'T':
                            sfldtyp += ($('#trange').is(":checked") == true) ? ($('#trange').val()) : '';
                            break;
                        case 'S':
                            if ($('input[type="radio"][name="seltype"]:checked').length < 1) {
                                alert('Select the Selection Type');
                                sels = false;
                                return false;
                            }
                            sfldtyp += $('input[type="radio"][name="seltype"]:checked').val();
                            if ($('input[type="radio"][name="dataf"]:checked').length < 1) {
                                alert('Select the Data Source');
                                sels = false;
                                return false;
                            }

                            sfldtyp += $('input[type="radio"][name="dataf"]:checked').val();
                            smtable = $('#mtables').val();
                            Fld_Src_Name = smtable;
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (smtable == '' || smtable == '0')) {
                                alert('Select the Master Table');
                                $('#mtables').focus();
                                sels = false;
                                return false;
                            }
                            svalfld = $('#ddlvaluef').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (svalfld == "" || svalfld == "0")) {
                                alert('Select the Value Filed');
                                $('#ddlvaluef').focus();
                                sels = false;
                                return false;
                            }
                            stxtfld = $('#ddltextf').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (stxtfld == '' || stxtfld == '0')) {
                                alert('Select the Text Field');
                                $('#ddltextf').focus();
                                sels = false;
                                return false;
                            }
                            Fld_Src_Field = svalfld + ',' + stxtfld;
                            custtxt = $('#custt').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O' && custtxt == '') {
                                alert('Enter the Template Name');
                                $('#custt').focus();
                                sels = false;
                                return false;
                            }
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O') {
                                Fld_Src_Field = '';
                                Fld_Src_Name = custtxt;
                            }
                            custempsval = $('#custemps').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O' && custempsval == '') {
                                alert('Enter the Template Values');
                                $('#custemps').focus();
                                sels = false;
                                return false;
                            }
                            var splitvals = ($('#custemps').val()).split('\n');
                            if ($('#custemps').val() != '') {
                                for ($i = 0; $i < splitvals.length; $i++) {
                                    Fld_Src_Field += (splitvals[$i] + ',');
                                    cusxml += '<ASSD Cus_values="' + splitvals[$i] + '" />';
                                }
                            }

                            break;

                        case 'R':
                            if ($('input[type="radio"][name="dataf"]:checked').length < 1) {
                                alert('Select the Data Source');
                                sels = false;
                                return false;
                            }
                            sfldtyp += $('input[type="radio"][name="dataf"]:checked').val();
                            smtable = $('#mtables :selected').val();
                            //smtable = $('#mtables').val().trim();
                            alert(smtable);

                            Fld_Src_Name = smtable;
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (smtable == 0)) {
                                alert('Select the Master Table');
                                $('#mtables').focus();
                                sels = false;
                                return false;
                            }
                            svalfld = $('#ddlvaluef').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (svalfld == '' || svalfld == '0')) {
                                alert('Select the Value Field');
                                $('#ddlvaluef').focus();
                                sels = false;
                                return false;
                            }
                            stxtfld = $('#ddltextf').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (stxtfld == '' || stxtfld == '0')) {
                                alert('Select the Text Field');
                                $('#ddltextf').focus();
                                sels = false;
                                return false;
                            }
                            Fld_Src_Field = svalfld + ',' + stxtfld;
                            custtxt = $('#custt').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O' && custtxt == '') {
                                alert('Enter the Template Name');
                                $('#custt').focus();
                                sels = false;
                                return false;
                            }
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O') {
                                Fld_Src_Field = '';
                                Fld_Src_Name = custtxt;
                            }
                            custempsval = $('#custemps').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O' && custempsval == '') {
                                alert('Enter the Template Values');
                                $('#custemps').focus();
                                sels = false;
                                return false;
                            }
                            var splitvals = ($('#custemps').val()).split('\n');
                            for ($i = 0; $i < splitvals.length; $i++) {
                                Fld_Src_Field += (splitvals[$i] + ',');
                                cusxml += '<ASSD Cus_values="' + splitvals[$i] + '" />';
                            }
                            break;
                       
                        case 'C':
                            if ($('input[type="radio"][name="dataf"]:checked').length < 1) {
                                alert('Select the Data Source');
                                sels = false;
                                return false;
                            }
                            sfldtyp += $('input[type="radio"][name="dataf"]:checked').val();
                            smtable = $('#mtables :selected').val();
                            //smtable = $('#mtables').val().trim();
                            
                            Fld_Src_Name = smtable;
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (smtable == 0)) {
                                alert('Select the Master Table');
                                $('#mtables').focus();
                                sels = false;
                                return false;
                            }
                            svalfld = $('#ddlvaluef').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (svalfld == '' || svalfld == '0')) {
                                alert('Select the Value Field');
                                $('#ddlvaluef').focus();
                                sels = false;
                                return false;
                            }
                            stxtfld = $('#ddltextf').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'M' && (stxtfld == '' || stxtfld == '0')) {
                                alert('Select the Text Field');
                                $('#ddltextf').focus();
                                sels = false;
                                return false;
                            }
                            Fld_Src_Field = svalfld + ',' + stxtfld;
                            custtxt = $('#custt').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O' && custtxt == '') {
                                alert('Enter the Template Name');
                                $('#custt').focus();
                                sels = false;
                                return false;
                            }
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O') {
                                Fld_Src_Field = '';
                                Fld_Src_Name = custtxt;
                            }
                            custempsval = $('#custemps').val();
                            if ($('input[type="radio"][name="dataf"]:checked').val() == 'O' && custempsval == '') {
                                alert('Enter the Template Values');
                                $('#custemps').focus();
                                sels = false;
                                return false;
                            }
                            var splitvals = ($('#custemps').val()).split('\n');
                            for ($i = 0; $i < splitvals.length; $i++) {
                                Fld_Src_Field += (splitvals[$i] + ',');
                                cusxml += '<ASSD Cus_values="' + splitvals[$i] + '" />';
                            }
                            break;
                        case 'F':
                            if ($('input[type="checkbox"][name="flupl"]:checked').length < 1) {
                                alert('Select Atleast One File Upload Option');
                                sels = false;
                                return false;
                            }
                            $('input[type="checkbox"][name="flupl"]:checked').each(function () {
                                sfldtyp += $(this).val();
                            });
                            break;
                        default:
                            break;
                    }

                    if ($('#mand').prop('checked') == true) {
                        Fld_Mandatory = '1';
                    }
                    else {
                        Fld_Mandatory = '0';
                    }

                    //if ($('#accwa').prop('checked') == true) {
                    //    AccessPoint = '1';
                    //}
                    //else {
                    //    AccessPoint = '0';
                    //}

                    var AccessPoint = ($('#accwa').prop('checked')) ? 1 : 0;

                    SrtNo = $('#sortingnum').val();
                    //var Fld_Mandatory = ($('#mand').prop('checked')) ? 1 : 0;
                    //alert(Fld_Mandatory);
                    var Control_id = masflds.filter(function (a) {
                        return a.ControlName == sfldtyp;
                    });
                    var Fldtype = '';
                    if ((Control_id[0].ControlID == 3 || Control_id[0].ControlID == 18)) {
                        Fldtype = 'varchar(150)';
                    }
                    //else if ((Control_id[0].ControlID == 8 || Control_id[0].ControlID == 11 || Control_id[0].ControlID == 12)) {
                    //    Fldtype = 'datetime';
                   // }

                     else if ((Control_id[0].ControlID == 8 || Control_id[0].ControlID == 9 || Control_id[0].ControlID == 11 || Control_id[0].ControlID == 12)) {
                        Fldtype = 'varchar(150)';
                    }
                    else if ((Control_id[0].ControlID == 14)) {
                        Fldtype = 'varchar(300)';
                    }
                    else if ((Control_id[0].ControlID == 6 || Control_id[0].ControlID == 7)) {
                        Fldtype = 'varchar(3000)';
                    }
                    else if ((Control_id[0].ControlID == 2 || Control_id[0].ControlID == 5 || Control_id[0].ControlID == 13 || Control_id[0].ControlID == 15 || Control_id[0].ControlID == 16 || Control_id[0].ControlID == 17 || Control_id[0].ControlID == 21)) {
                        Fldtype = 'varchar(300)';
                    }
                    else if ((Control_id[0].ControlID == 4 || Control_id[0].ControlID == 20)) {
                        Fldtype = 'varchar(150)';
                    }
                    else if ((Control_id[0].ControlID == 1 || Control_id[0].ControlID == 10 || Control_id[0].ControlID == 22)) {
                        Fldtype = 'varchar(' + MaxLen + ')'
                    }
                    cusxml += '</ROOT>';

                  

                    var hefild = $('.fieldId').val();
                    var Formdata = {
                        'FldID': hefild,
                        'ModuleId': ModuleName,
                        'FldName': FieldName,
                        'FldTyp': sfldtyp,
                        'Fld_Src_Name': Fld_Src_Name,
                        'Fld_Src_Field': Fld_Src_Field,
                        'Fld_Symbol': currtyp,
                        'Fld_Length': MaxLen,
                        'Fld_Mandatory': Fld_Mandatory,
                        'Active_flag': 0,
                        'divcode': "<%=Session["div_code"]%>",
                        'Order_by': 1,
                        'AccessPoint': AccessPoint,
                        'SrtNo': SrtNo,
                        'Control_id': Control_id[0].ControlID,
                        'Fldtype': Fldtype,
                        'FGroupId': FGroupId
                    };

                    //console.log(Formdata);

                    if (Formdata.ModuleId != "") {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "CustomFieldFrom.aspx/AddCustomField",
                            data: "{'Formdata':'" + JSON.stringify(Formdata) + "','sf_Code':'<%=Session["Sf_Code"]%>','cusxml':'" + cusxml + "'}",
                            dataType: "json",
                            success: function (msg) {
                                var frmresult = msg.d;
                                alert(frmresult);
                                FiledTypeOnChange(); clearFields();
                                window.location.href = "CustomFieldFrom.aspx";
                                //alert(msg.d);
                                //var frmresult = JSON.parse(msg.d) || [];
                                //if (msg.d == 'New Field Created') {
                                //    alert('New Field Created Successfully !!');
                                //    frmcr = false;
                                //    FiledTypeOnChange(); clearFields();
                                //    window.location.href = "CustomFieldFrom.aspx";
                                //    //$('#myModal2').hide();
                                //}
                                //else if (msg.d == "UpdatedFields") {
                                //    alert('Updated Field Successfully !!');
                                //    FiledTypeOnChange(); clearFields();
                                //    //$('#myModal2').modal('hide');
                                //    window.location.href = "CustomFieldFrom.aspx";
                                //}
                                //else if (msg.d == 'Field Not Created') {
                                //    alert('New Field Not Created !! ');
                                //    frmcr = false;
                                //    FiledTypeOnChange(); clearFields();
                                //    //$('#myModal2').hide();
                                //    window.location.href = "CustomFieldFrom.aspx";
                                //}
                                //else if (msg.d == 'Not Updated') {
                                //    alert('Not Updated !! ');
                                //    frmcr = false;
                                //    FiledTypeOnChange(); clearFields();
                                //    //$('#myModal2').hide();
                                //    window.location.href = "CustomFieldFrom.aspx";
                                //}
                                //else {
                                //    //console.log('Field Updated Successfully !!');
                                //    FiledTypeOnChange(); clearFields(); window.location.href = "CustomFieldFrom.aspx";
                                //}
                            },
                            error: function (msg) {
                                alert(JSON.stringify(msg));
                                frmcr = false;
                            }

                        });
                    }
                });

                function addFilter() {//change
                    $('#dvContainer').show();
                    var dvContainer = $("#dvContainer");

                    var div = $("<div/>");
                    var ddlsample = $('<div class="col-xs-12 col-sm-4"></div>');
                    var ddlclass = $('<div class="col-xs-12 col-sm-3" style="margin-bottom: 1rem !important;"></div>');
                    var ddleclass = $('<div class="col-xs-12 col-sm-3" style="margin-bottom: 1rem !important;"></div>');
                    var ddlselect = $('<select class="ddlFilter"></select>');
                    var ddleselect = $('<select class="ddleFilter"></select>');

                    $(ddlselect).empty().append('<option value="">Select </option>');
                    for ($i = 0; $i < FrmsandFlds.length; $i++) {
                        $(ddlselect).append(('<option value="' + parseInt(FrmsandFlds[$i].Fld_ID) + '">' + FrmsandFlds[$i].Fld_Name + '</option>'));
                    }
                    $(ddleselect).empty().append('<option value="">Select </option>');
                    for ($i = 0; $i < FtblCols.length; $i++) {
                        $(ddleselect).append('<option value="' + FtblCols[$i].Cols + '">' + FtblCols[$i].Cols + '</option>');
                    }

                    ddlclass.append(ddlselect);
                    ddleclass.append(ddleselect);
                    div.append(ddlsample);
                    div.append(ddlclass);
                    div.append(ddleclass);
                    var btnRemove = $("<div class='col-xs-12 col-sm-2'><input type = 'button' value = '-' style='float:right;background-color: #1a73e8;' class='btn btn-primary' color= 'white'/></div>");
                    btnRemove.click(function () {
                        $(this).parent().remove();
                    });
                    div.append(btnRemove);
                    dvContainer.append(div);
                }//change

                $('#master').on('change', function () {
                    $('#ddlvaluef').empty();
                    $('#ddltextf').empty();
                    if ($(this).is(":checked")) {
                        masterch();
                    }
                });

                $('#others').on('change', function () {
                    if ($(this).is(":checked")) {
                        othersch();
                    }
                });

                function masterch() {
                    $('#mtables').val('');
                    $('#Customvrow').hide();
                    $('#Templaterow').hide();
                    $('#Mtablerow').show();
                    //$('#Fieldrow').show();
                    $('#Fieldrow').hide();
                    $('#Filter').hide();
                    if ($('#ddltypes').val() == 'S' || $('#ddltypes').val() == 'R' || $('#ddltypes').val() == 'C') {
                        //$('#ValueFRow').show();
                        //$('#TextFRow').show();

                        $('#ValueFRow').hide();
                        $('#TextFRow').hide();
                    }
                }

                function othersch() {
                    //$('#custt').val('');
                    //$('#custemps').val('');
                    $('#Templaterow').show();
                    $('#Customvrow').show();
                    $('#Mtablerow').hide();
                    $('#Fieldrow').hide();
                    $('#ValueFRow').hide();
                    $('#TextFRow').hide();
                    $('#Filter').hide();
                }                               

                $('#ddltypes').on('change', function () {
                    
                    FiledTypeOnChange();
                });

                $("#mtables").on('change', function () {
                    var TableName = $("#mtables").val();
                    loadTableColumns(TableName)
                });

                function FiledTypeOnChange() {
                    var selectyp = $('#ddltypes').val();
                    $('.hiderow').hide();
                    $('#Filter').hide();                    
                    switch (selectyp) {
                        case 'L':
                            $('.daterow').hide();
                            $('.timerow').hide();
                            $('#selectrow').hide();
                            $('#numberrow').hide();
                            $('#phonenrow').hide();
                            $('#currlist').hide();
                            $('#textrow').hide();
                            $('#Mtablerow').hide();
                            $('.Fieldrow').hide();
                            $('#Filter').hide();
                            $('.Maxlenrow').hide();
                            $('.Mandrow').hide();
                            $('#datarow').hide();
                            $('#Customvrow').hide();
                            $('#Templaterow').hide();
                            $('#filerow').hide();
                            break;
                        case 'TA':
                            $('.daterow').hide();
                            $('#datarow').hide();
                            $('#Mtablerow').hide();
                            $('.Fieldrow').hide();
                            $('#Filter').hide();
                            $('.timerow').hide();
                            $('.Maxlenrow').show();
                            $('.Mandrow').show();
                            $('#numberrow').hide();
                            $('#phonenrow').hide();
                            $('#currlist').hide();
                            $('#selectrow').hide();
                            $('#textrow').show();
                            $('#Customvrow').hide();
                            $('#Templaterow').hide();
                            $('#filerow').hide();
                            break;
                        case 'N':
                            $('.daterow').hide();
                            $('#datarow').hide();
                            $('#Mtablerow').hide();
                            $('.Fieldrow').hide();
                            $('#Filter').hide();
                            $('.timerow').hide();
                            $('#selectrow').hide();
                            $('#textrow').hide();
                            //$('.Maxlenrow').show();
                            $('.Maxlenrow').hide();
                            $('.Mandrow').show();
                            $('#numberrow').show();
                            $('#phonenrow').show();
                            $('#currlist').show();
                            $('#Customvrow').hide();
                            $('#Templaterow').hide(); $('#filerow').hide();
                            break;
                        case 'D':
                            $('#datarow').hide();
                            $('#Mtablerow').hide();
                            $('.Fieldrow').hide();
                            $('#Filter').hide();
                            $('.Maxlenrow').hide();
                            $('#selectrow').hide();
                            $('#numberrow').hide();
                            $('#phonenrow').hide();
                            $('#currlist').hide();
                            $('#textrow').hide();
                            $('.daterow').show();
                            $('.Mandrow').show();
                            $('.timerow').hide();
                            $('#filerow').hide();
                            $('#Customvrow').hide();
                            $('#Templaterow').hide();
                            break;
                        case 'T':
                            $('#datarow').hide();
                            $('#Mtablerow').hide();
                            $('#selectrow').hide();
                            $('#numberrow').hide();
                            $('#phonenrow').hide();
                            $('#currlist').hide();
                            $('#textrow').hide();
                            $('#filerow').hide();
                            $('.Fieldrow').hide();
                            $('#Filter').hide();
                            $('.Maxlenrow').hide();
                            $('.Mandrow').show();
                            $('.daterow').hide();
                            $('.timerow').show();
                            $('#Customvrow').hide();
                            $('#Templaterow').hide();
                            break;
                        case 'S':
                            $('#Filter').hide();
                            $('.Maxlenrow').hide();
                            $('.daterow').hide();
                            $('.timerow').hide();
                            $('#filerow').hide();
                            $('#datarow').show();
                            //$('#Mtablerow').hide();
                            $('#Mtablerow').show();
                            //$('.Fieldrow').show();
                            $('.Fieldrow').hide();
                            $('#selectrow').show();
                            $('#numberrow').hide();
                            $('#phonenrow').hide();
                            $('#currlist').hide();
                            $('#textrow').hide();
                            $('.Mandrow').show();

                            break;
                        
                        case 'R':
                            $('#Filter').hide();
                            $('.Maxlenrow').hide();
                            $('.daterow').hide();
                            $('.timerow').hide();
                            $('#filerow').hide();
                            $('#datarow').show();
                            $('#Mtablerow').show();
                            //$('#Mtablerow').hide();
                            //$('.Fieldrow').show();
                            $('.Fieldrow').hide();
                            $('#selectrow').hide();
                            $('#numberrow').hide();
                            $('#phonenrow').hide();
                            $('#currlist').hide();
                            $('#textrow').hide();
                            $('.Mandrow').show();
                            break;
                        
                        case 'C':
                            $('#Filter').hide(); $('.Maxlenrow').hide();
                            $('.daterow').hide(); $('.timerow').hide();
                            $('#filerow').hide(); $('#filerow').hide();
                            $('#textrow').hide(); $('#numberrow').hide();
                            $('#phonenrow').hide(); $('#currlist').hide();

                            $('#datarow').show();
                            $('#Mtablerow').show();
                            //$('#Mtablerow').hide();
                            //$('.Fieldrow').show();
                            $('.Fieldrow').hide();
                            $('#selectrow').hide();
                            $('.Mandrow').show();

                            break;
                        case 'F':
                            $('#Filter').hide(); $('.Maxlenrow').hide();
                            $('.daterow').hide(); $('.timerow').hide();
                            $('#numberrow').hide(); $('#phonenrow').hide();
                            $('#currlist').hide(); $('#filerow').hide();
                            $('#datarow').hide(); $('#Mtablerow').hide();
                            $('.Fieldrow').hide(); $('#selectrow').hide();
                            $('#textrow').hide(); $('.Mandrow').hide();
                            $('#filerow').show();
                            break;
                        case '0':
                            $('.daterow').hide();
                            $('#datarow').hide();
                            $('#Mtablerow').hide();
                            $('.Fieldrow').hide();
                            $('#Filter').hide();
                            $('.timerow').hide();
                            $('.Maxlenrow').hide();
                            $('.Mandrow').hide();
                            $('#selectrow').hide(); $('#textrow').hide();
                            $('#filerow').hide();
                            $('#numberrow').hide();
                            $('#phonenrow').hide(); $('#currlist').hide();
                            break;
                        default:
                            break;
                    }

                }

                function loadMasterTablelist() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "CustomFieldFrom.aspx/GetCustomMasterTableVal",
                        data: "{}",
                        dataType: "json",
                        success: function (data) {
                            CTList = JSON.parse(data.d) || [];
                            if (CTList.length > 0) {
                                var states = $("#mtables");
                                states.empty().append('<option selected="selected" value="0">Select Table</option>');
                                for (var i = 0; i < CTList.length; i++) {
                                    states.append($('<option value="' + CTList[i].TableActualName + '">' + CTList[i].TableName + '</option>'))
                                }
                            }
                        }
                    });                   
                }

                function loadTableColumns(TableName) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "CustomFieldFrom.aspx/CustomGetTableColumns",
                        data: "{'TableName':'" + TableName + "'}",
                        dataType: "json",
                        success: function (data) {
                            CTCList = JSON.parse(data.d) || [];


                            if (CTCList.length > 0) {
                                var valtbl = $("#ddlvaluef"); var texttbl = $('#ddltextf');
                                //valtbl.empty().append('<option selected="selected" value="0">Select Value</option>');
                                //texttbl.empty().append('<option selected="selected" value="0">Select Text</option>');
                                for (var i = 0; i < CTCList.length; i++) {
                                    valtbl.append($('<option value="' + CTCList[i].IdCol + '" selected="selected">' + CTCList[i].IdCol + '</option>'));
                                    texttbl.append($('<option value="' + CTCList[i].TextCol + '" selected="selected">' + CTCList[i].TextCol + '</option>'));
                                }
                            }
                        }
                    });
                    $("#ddlvaluef").se
                }

                function ReloadTable() {
                    $("#OrderList TBODY").html("");
                   
                    st = PgRecords * (pgNo - 1); slno = 0;
                    
                    for ($i = st; $i < st + PgRecords; $i++) {
                        if ($i < Orders.length) {
                            tr = $("<tr class='" + Orders[$i].Field_Id + "' id='" + Orders[$i].Field_Id + "'></tr>");
                            //var hq = filtered[$i].Sf_Name.split('-');
                            slno = $i + 1;

                            $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].ModuleName +
                                '</td><td> ' + Orders[$i].FGroupName + '</td><td> ' + Orders[$i].Field_Name + '</td><td>' + Orders[$i].FieldType +
                                '</td><td>' + Orders[$i].Fld_Length + '</td><td>' + Orders[$i].Mandate +
                                '</td><td class="frmdeactive"><a href="#">' + Orders[$i].Status +
                                '</a></td><td class="frmedit"><a href="#" data-toggle="modal" data-target="#myModal2"><span class="glyphicon glyphicon-edit"></span></a></td>');
                            $("#OrderList TBODY").append(tr);                           
                        }
                    }

                    $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                  
                    loadPgNos();
                }

                function loadModuleNames() {
                    var SFDivi = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "CustomFieldFrom.aspx/GetCustomModuleList",
                        data: "{'divcode':'<%=Session["div_code"]%>'}",
                        dataType: "json",
                        success: function (data) {
                            SFDivi = JSON.parse(data.d) || [];
                            if (SFDivi.length > 0) {
                                var divi = $("#ddlmodule");                               
                                divi.empty().append('<option selected="selected" value="0">Select Module</option>');
                                for (var i = 0; i < SFDivi.length; i++) {
                                    divi.append($('<option value="' + SFDivi[i].ModuleId + '">' + SFDivi[i].ModuleName + '</option>'));
                                }
                            }
                        }
                    });
                    
                }

                function loadFieldGroupList() {
                    var ModuleId = $("#ddlmodule option:selected").val();
                    if (ModuleId != "0") {
                        var SFDivi = [];
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "CustomFieldFrom.aspx/GetCustomFieldGroupList",
                            data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'" + ModuleId + "'}",
                            dataType: "json",
                            success: function (data) {
                                SFDivi = JSON.parse(data.d) || [];
                                if (SFDivi.length > 0) {
                                    var divi = $("#ddlfgroup");
                                    divi.empty().append('<option selected="selected" value="0">Select Group</option>');
                                    for (var i = 0; i < SFDivi.length; i++) {
                                        divi.append($('<option value="' + SFDivi[i].FieldGroupId + '">' + SFDivi[i].FGroupName + '</option>'));
                                    }
                                }
                            }
                        });
                    }
                }

                function loadCustomFields() {
                    var ModuleId = $("#ddlmodule option:selected").val();
                    
                    if ((ModuleId == "0" || ModuleId == "" || ModuleId == null)) {
                        $('#btnAdd').hide();
                    }
                    else {
                        $('#btnAdd').show();
                    }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "CustomFieldFrom.aspx/GetCustomFormsFieldsList",
                        data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'" + ModuleId + "'}",
                        dataType: "json",
                        success: function (data) {
                            //Orders = JSON.parse(data.d) || [];
                            MasFrms = JSON.parse(data.d) || [];
                            //console.log(Orders);
                            Orders = MasFrms; 
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                    ReloadTable();
                }

                $(document).ready(function () {
                    var FieldId = $('.fieldId').val();
                    if (FieldId == "") {
                        FieldId = "0";
                        $('.fieldId').val(FieldId);
                        $('#finame').prop('disabled', '');
                    }
                    

                    FiledTypeOnChange();
                    $('#Filter').hide();
                    loadMasterTablelist();
                    //var TableName = $("#mtables").val();
                    //alert(TableName);
                    //if((TableName!=""||TableName!="0"))
                    //{loadTableColumns(TableName);}
                    loadModuleNames();
                    loadCustomFields();
                    loadFieldGroupList();

                    var ModuleName = $("#ddlmodule").val();
                    
                    if (ModuleName == "0") {
                        $("#btnAdd").hide();
                    }
                    else {
                        $("#btnAdd").show();
                        
                    }

                    $('#mtables').selectpicker({
                        liveSearch: true
                    });

                    $('#ddlmodule').selectpicker({
                        liveSearch: true
                    });

                    $('#mtables').selectpicker({
                        liveSearch: true
                    });

                });

                $('#ddlmodule').on('change', function () {
                    var ModuleId = $("#ddlmodule option:selected").val();
                    if ((ModuleId == "0" || ModuleId == "" || ModuleId == null)) {
                        $('#btnAdd').hide();
                    }
                    else {
                        $('#btnAdd').show();
                    }
                    loadCustomFields();
                });

                function clearFields() {

                    $('input[type="text"]').each(function () {
                        $('.modal-body').find('input[type="text"]').val('');                        
                    });

                    $('input[type="number"]').each(function () {
                        $('.modal-body').find('input[type="number"]').val('');
                    });

                    $('input[type="checkbox"]').each(function () {
                        $('.modal-body').find('input[type="checkbox"]').prop('checked', false);
                    });

                    $('input[type="radio"]').each(function () {
                        $('.modal-body').find('input[type="radio"]').prop('checked', false);
                    });

                    $('input[type="textarea"]').each(function () {
                        $('.modal-body').find('input[type="textarea"]').val('');
                    });

                    $('#finame').prop('disabled', '');
                    $('#finame').val('');
                    $('#ddltypes').val('0');
                    $('#ddlfgroup').val('0');
                    $('.ddlFilter').val('');//change
                   
                    $('.ddleFilter').val('');
                    $('.hiderow').hide();
                    $('#Filter').hide();//change
                    $('.hiderow').hide();
                    $('#custt').val('');
                    //$('.modal-body').find('input[type="checkbox"]').prop('checked', false);
                    //$('.modal-body').find('input[type="radio"]').prop('checked', false);                  
                    $('#currlist').val('');
                    //$('.modal-body').find('textarea').val('');
                    $('#mtables').val('');
                    $('#ddlvaluef').empty();
                    $('#ddltextf').empty();
                   
                   
                }

                $('input[name="numtype"]').on('change', function () {
                    $('#currlist').val('0');
                    if ($('#cuurency').is(":checked")) {
                        $('#currlist').show();
                    }
                    if (!$('#cuurency').is(":checked")) {
                        $('#currlist').hide();
                    }
                    if ($('#phonen').is(":checked")) {
                        //$('#Maxlenrow').show();
                        $('.Maxlenrow').show();
                    }
                    if (!$('#phonen').is(":checked")) {
                        //$('#Maxlenrow').hide();
                        $('.Maxlenrow').hide();
                    }
                });

                $(document).on('click', '.frmedit', function () {
                    //clearFields();
                    var FldID = $(this).closest('tr').attr('id');

                    clearFields();

                    loadMasterTablelist(); loadFieldGroupList();

                    CustomloadFields(FldID);                    
                });

                $(document).on('click', '.frmdeactive', function () {
                    //clearFields();
                    var FldID = $(this).closest('tr').attr('id');                   

                    //alert(FldID);
                });

                function CustomloadFields(FldID) {
                    var FldArr = []; var FldSrcName = ''; var multiopt = ''; var Mand = '';
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "CustomFieldFrom.aspx/GetCustomFormsFieldsDataById",
                        data: "{'divcode':'<%=Session["div_code"]%>','FieldId':'" + FldID + "'}",
                        dataType: "json",

                        success: function (data) {
                            FldArr = JSON.parse(data.d) || [];
                            //console.log(FldArr);
                            //FldArr = JSON.parse(data.d);

                            $('#fieldId').val(FldArr[0].Field_Id);
                            $('#ddlfgroup').val(FldArr[0].FieldGroupId);
                            $('#btnAddFgModule').attr('disabled', 'disabled');
                            $('#ddlfgroup').attr('disabled', 'disabled');


                            $('.fieldId').val(FldArr[0].Field_Id);

                            $('#finame').val(FldArr[0].Field_Name);
                            $('#finame').prop('disabled', 'disabled');

                            $('#maxleng').val(FldArr[0].Fld_Length);

                            multiopt = (FldArr[0].Fld_Src_Field).split(',');

                            Mand = FldArr[0].Mandate1;
                            if (Mand == 'No') {
                                $('#mand').prop('checked', false);
                            }
                            else {
                                $('#mand').prop('checked', true);
                            }

                            FldSrcName = FldArr[0].Fld_Src_Name;
                            
                            if (FldArr[0].AccessPoint == "1") {
                                $('#accwa').prop('checked', true);
                            }

                            $('#sortingnum').val(FldArr[0].SrtNo);

                            if (FldArr[0].AccessPoint == '0') {
                                $('#accwa').prop('checked', false);
                            }
                            else {
                                $('#accwa').prop('checked', true);
                            }

                            switch (FldArr[0].Fld_Type) {
                                case 'L':
                                    $('#ddltypes').val('L'); //$('#ddltypes').selectpicker('val', 'L');
                                    FiledTypeOnChange();
                                    break;
                                case 'TAS':
                                    $('#ddltypes').val('TA'); 
                                    //$('#ddltypes').selectpicker('val', 'TA');
                                    $('#stext').prop('checked', true);                                  
                                    
                                    FiledTypeOnChange();

                                    break;
                                case 'TAM':
                                    $('#ddltypes').val('TA');
                                    //$('#ddltypes').selectpicker('val', 'TA');

                                    $('#mtext').prop('checked', true);
                                   
                                    FiledTypeOnChange();
                                    break;
                                case 'N':
                                    $('#ddltypes').val('N');
                                    //$('#ddltypes').selectpicker('val', 'N');
                                    if (Mand == 'No') {
                                        $('#mand').prop('checked', false);
                                    }
                                    else {
                                        $('#mand').prop('checked', true);
                                    }
                                    FiledTypeOnChange();
                                    break;
                                case 'NC':
                                    $('#ddltypes').val('N');
                                    //$('#ddltypes').selectpicker('val', 'N');
                                    $('#cuurency').prop('checked', true);
                                   
                                    $('#currlist').val(FldArr[0].Fld_Symbol);
                                    FiledTypeOnChange();
                                    break;
                                case 'NP':
                                    $('#ddltypes').val('N');
                                   
                                    //$('#ddltypes').selectpicker('val', 'N');
                                    $('#phonen').prop('checked', true);
                                    FiledTypeOnChange();
                                    break;
                                case 'SSM':
                                    $('#ddltypes').val('S');

                                    //$('#ddltypes').selectpicker('val', 'R');
                                    $('#datarow').show();

                                    $('#sslec').prop('checked', true);                                    
                                    $('#master').prop('checked', true);

                                    $('#mtables').val(FldArr[0].TableActualName);

                                    //console.log(FldArr[0].TableActualName);

                                    $('#mtables').selectpicker('refresh');

                                    //var element = $('#mtables');
                                    var velement = $('#ddlvaluef');
                                    var telement = $('#ddltextf');

                                    if ((FldSrcName != '' || FldSrcName != null)) {

                                        loadTableColumns(FldSrcName);
                                        //$(element).val(FldArr[0].Fld_Src_Name);
                                        $(velement).val(multiopt[0]);
                                        $(telement).val(multiopt[1]);

                                        //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                                        //$(velement).selectpicker('val', multiopt[0]);
                                        //$(telement).selectpicker('val', multiopt[1]);

                                    }
                                    $('#mtables').selectpicker('refresh');
                                    FiledTypeOnChange();
                                    masterch();                                    
                                    break;
                                case 'SSO':
                                    $('#ddltypes').val('S');
                                    //$('#ddltypes').selectpicker('val', 'S');
                                   
                                    $('#sslec').prop('checked', true);
                                    $('#others').prop('checked', true);

                                    //$('#ddltypes').selectpicker('val', 'R');
                                    $('#datarow').show();
                                    //$('#Templaterow').show();
                                    //$('#Customvrow').show();
                                    //$('#Mtablerow').hide();

                                    $('#others').prop('checked', true);

                                    $('#custt').val(FldArr[0].Fld_Src_Name);
                                    var newopt = '';
                                    for ($j = 0; $j < multiopt.length; $j++) {
                                        newopt += multiopt[$j] + '\n';
                                    }
                                    $('#custemps').val(newopt);
                                    console.log(newopt);
                                    FiledTypeOnChange();
                                    othersch();      



                                    //$('#custt').val(FldArr[0].Fld_Src_Name);
                                    //fillvalfields(FldArr[0].Fld_Src_Name);
                                    //var newopt = '';
                                    //for ($j = 0; $j < multiopt.length; $j++) {
                                    //    newopt += multiopt[$j] + '\n';
                                    //}
                                    //$('#custemps').val(newopt);
                                    //FiledTypeOnChange();
                                    //othersch();

                                    break;
                                case 'SMM':
                                    $('#ddltypes').val('S');
                                    //$('#ddltypes').selectpicker('val', 'S');
                                    $('#mselc').prop('checked', true);
                                    $('#master').prop('checked', true);

                                    $('#mtables').val(FldArr[0].TableActualName);

                                    //console.log(FldArr[0].TableActualName);

                                    $('#mtables').selectpicker('refresh');

                                    //var element = $('#mtables');
                                    var velement = $('#ddlvaluef');
                                    var telement = $('#ddltextf');

                                    if ((FldSrcName != '' || FldSrcName != null)) {

                                        loadTableColumns(FldSrcName);
                                        //$(element).val(FldArr[0].Fld_Src_Name);
                                        $(velement).val(multiopt[0]);
                                        $(telement).val(multiopt[1]);

                                        //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                                        //$(velement).selectpicker('val', multiopt[0]);
                                        //$(telement).selectpicker('val', multiopt[1]);

                                    }
                                    $('#mtables').selectpicker('refresh');
                                    FiledTypeOnChange();
                                    masterch();      

                                    //$('#mtables').val(FldArr[0].TableActualName);
                                    //$('#mtables').selectpicker('refresh');                               

                                  
                                    //var element = $('#mtables');
                                    //var velement = $('#ddlvaluef');
                                    //var telement = $('#ddltextf');

                                    //if ((FldSrcName != '' || FldSrcName != null)) {

                                    //    loadTableColumns(FldSrcName);
                                    //    //$(element).val(FldArr[0].Fld_Src_Name);
                                    //    $(velement).val(multiopt[0]);
                                    //    $(telement).val(multiopt[1]);

                                    //    //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                                    //    //$(velement).selectpicker('val', multiopt[0]);
                                    //    //$(telement).selectpicker('val', multiopt[1]);

                                    //}
                                    //FiledTypeOnChange();
                                    //masterch();
                                    break;
                                case 'SMO':
                                    $('#ddltypes').val('S'); //$('#ddltypes').selectpicker('val', 'S');
                                    
                                    $('#mselc').prop('checked', true);
                                    $('#others').prop('checked', true);


                                    

                                    //$('#ddltypes').selectpicker('val', 'R');
                                    $('#datarow').show();
                                    //$('#Templaterow').show();
                                    //$('#Customvrow').show();
                                    //$('#Mtablerow').hide();

                                    $('#others').prop('checked', true);

                                    $('#custt').val(FldArr[0].Fld_Src_Name);
                                    var newopt = '';
                                    for ($j = 0; $j < multiopt.length; $j++) {
                                        newopt += multiopt[$j] + '\n';
                                    }
                                    $('#custemps').val(newopt);
                                    console.log(newopt);
                                    FiledTypeOnChange();
                                    othersch();      
                                                                       
                                    //$('#custt').val(FldArr[0].Fld_Src_Name);
                                    //fillvalfields(FldArr[0].Fld_Src_Name);
                                    //var newopt = '';
                                    //for ($j = 0; $j < multiopt.length; $j++) {
                                    //    newopt += multiopt[$j] + '\n';
                                    //}
                                    //$('#custemps').val(newopt);
                                    //FiledTypeOnChange();
                                    //othersch();

                                    break;
                                case 'D':
                                    $('#ddltypes').val('D'); //$('#ddltypes').selectpicker('val', 'D');
                                    
                                    FiledTypeOnChange();
                                    break;
                                case 'DR':
                                    $('#ddltypes').val('D'); //$('#ddltypes').selectpicker('val', 'D');
                                    
                                    //$('#daterow').show();
                                    $('#drange').prop('checked', true);
                                    FiledTypeOnChange();
                                    break;
                                case 'T':
                                    $('#ddltypes').val('T'); //$('#ddltypes').selectpicker('val', 'T');
                                   
                                    //$('#timerow').show();
                                    FiledTypeOnChange();
                                    break;
                                case 'TR':
                                    $('#ddltypes').val('T'); //$('#ddltypes').selectpicker('val', 'T');
                                    
                                    //$('#timerow').show();
                                    $('#trange').prop('checked', true);
                                    FiledTypeOnChange();
                                    break;

                                case 'RM':
                                    $('#ddltypes').val('R');
                                   
                                    //$('#ddltypes').selectpicker('val', 'R');
                                    $('#datarow').show();
                                    $('#master').prop('checked', true);
                                    $('#mtables').val(FldArr[0].TableActualName);

                                    //console.log(FldArr[0].TableActualName);

                                    $('#mtables').selectpicker('refresh');

                                    //var element = $('#mtables');
                                    var velement = $('#ddlvaluef');
                                    var telement = $('#ddltextf');

                                    if ((FldSrcName != '' || FldSrcName != null)) {

                                        loadTableColumns(FldSrcName);
                                        //$(element).val(FldArr[0].Fld_Src_Name);
                                        $(velement).val(multiopt[0]);
                                        $(telement).val(multiopt[1]);

                                        //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                                        //$(velement).selectpicker('val', multiopt[0]);
                                        //$(telement).selectpicker('val', multiopt[1]);

                                    }
                                    $('#mtables').selectpicker('refresh');
                                    FiledTypeOnChange();
                                    masterch();
                                   
                                    break;
                                case 'RO':
                                    $('#ddltypes').val('R');
                                    
                                    //$('#ddltypes').selectpicker('val', 'R');
                                    $('#datarow').show();
                                    //$('#Templaterow').show();
                                    //$('#Customvrow').show();
                                    //$('#Mtablerow').hide();
                                    
                                    $('#others').prop('checked', true);

                                    $('#custt').val(FldArr[0].Fld_Src_Name);
                                    var newopt = '';
                                    for ($j = 0; $j < multiopt.length; $j++) {
                                        newopt += multiopt[$j] + '\n';
                                    }
                                    $('#custemps').val(newopt);
                                    console.log(newopt);    
                                    FiledTypeOnChange();
                                    othersch();        
                                  
                                    break;
                                case 'CM':
                                    $('#ddltypes').val('C');

                                    //$('#ddltypes').selectpicker('val', 'R');
                                    $('#datarow').show();
                                    $('#master').prop('checked', true);
                                    $('#mtables').val(FldArr[0].TableActualName);

                                    //console.log(FldArr[0].TableActualName);

                                    $('#mtables').selectpicker('refresh');

                                    //var element = $('#mtables');
                                    var velement = $('#ddlvaluef');
                                    var telement = $('#ddltextf');

                                    if ((FldSrcName != '' || FldSrcName != null)) {

                                        loadTableColumns(FldSrcName);
                                        //$(element).val(FldArr[0].Fld_Src_Name);
                                        $(velement).val(multiopt[0]);
                                        $(telement).val(multiopt[1]);

                                        //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                                        //$(velement).selectpicker('val', multiopt[0]);
                                        //$(telement).selectpicker('val', multiopt[1]);

                                    }
                                    $('#mtables').selectpicker('refresh');
                                    FiledTypeOnChange();
                                    masterch();

                                    break;
                                case 'CO':
                                    $('#ddltypes').val('C');
                                    
                                    //$('#ddltypes').selectpicker('val', 'C');
                                    $('#datarow').show();
                                    $('#others').prop('checked', true);
                                   
                                    $('#custt').val(FldArr[0].Fld_Src_Name);
                                    var newopt = '';
                                    for ($j = 0; $j < multiopt.length; $j++) {
                                        newopt += multiopt[$j] + '\n';
                                    }
                                    $('#custemps').val(newopt);
                                    FiledTypeOnChange();
                                    othersch();
                                    break;
                                case 'FS':
                                   
                                    $('#ddltypes').val('F');
                                    //$('#ddltypes').selectpicker('val', 'F');
                                    $('#selupl').prop('checked', true);
                                    FiledTypeOnChange();
                                    break;
                                case 'FC':
                                   
                                    $('#ddltypes').val('F');
                                   
                                    //$('#ddltypes').selectpicker('val', 'F');
                                    $('#cameraupl').prop('checked', true);
                                    FiledTypeOnChange();
                                    break;
                                case 'FSC':
                                    $('#ddltypes').val('F');
                                    
                                    //$('#ddltypes').selectpicker('val', 'F');
                                    $('#selupl').prop('checked', true);
                                    FiledTypeOnChange();
                                    break;
                                default:
                                    break;

                            }
                            //change
                            if (($('#ddltypes').val() == 'S' || $('#ddltypes').val() == 'R' || $('#ddltypes').val() == 'C' && ($('input[type="radio"][name="dataf"]:checked').val() == 'M'))) {
                                //$('#Filter').show();
                            }
                            if ($('#ddltypes').val() == 'LC') {
                                //$('#Filter').show();
                            }
                            if (($('#ddltypes').val() == 'S' || $('#ddltypes').val() == 'R' || $('#ddltypes').val() == 'C' || $('#ddltypes').val() == 'LC')) {
                                for ($i = 0; $i < FrmsandFlds.length; $i++) {
                                    if (FldArr[0].Fld_ID == parseInt(FrmsandFlds[$i].Fld_ID)) {
                                        $('.ddlFilter option[value=' + FrmsandFlds[$i].Fld_ID + ']').hide();
                                    }
                                    else {
                                        $('.ddlFilter option[value=' + FrmsandFlds[$i].Fld_ID + ']').show();
                                    }
                                }
                            }

                            $('#ddltypes').attr('disabled', 'disabled');
                            //change
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d))
                        }
                    })
                 }
                                 
            </script>

        </body>
    </html>
</asp:Content>


