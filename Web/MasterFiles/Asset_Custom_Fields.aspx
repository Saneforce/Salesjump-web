<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Asset_Custom_Fields.aspx.cs" Inherits="MasterFiles_Asset_Custom_Fields" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
     <style type="text/css">
    th {
        white-space: nowrap;
        cursor: pointer;
    }
    label {
    font-size: 13px;
    font-family: 'Open Sans','arial';
    font-weight: 700;
    margin: 0px;
    transition: 0.2s ease all;
    -moz-transition: 0.2s ease all;
    -webkit-transition: 0.2s ease all;
    z-index: 1;
}
    #tblselect li:hover {
    cursor: pointer;
    color: #fff !important;
    text-decoration: none;
    background-color: #08c;
}

#tblselected li:hover {
    cursor: pointer;
    color: #fff !important;
    text-decoration: none;
    background-color: #08c;
}

.hero-unit .ms-container ul.ms-list {
    height: 250px;
}
.ms-container .ms-list .ms-focus {
    border-color: rgba(82, 168, 236, 0.8);
    -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6);
    -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6);
    box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6);
    outline: 0;
    outline: thin dotted \9;
}

.ms-container .ms-list {
    -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
    -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
    box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
    -webkit-transition: border linear 0.2s, box-shadow linear 0.2s;
    -moz-transition: border linear 0.2s, box-shadow linear 0.2s;
    -ms-transition: border linear 0.2s, box-shadow linear 0.2s;
    -o-transition: border linear 0.2s, box-shadow linear 0.2s;
    transition: border linear 0.2s, box-shadow linear 0.2s;
    border: 1px solid #ccc;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border-radius: 3px;
    position: relative;
    height: 200px;
    padding: 0;
    overflow-y: auto;
}
.ms-container ul {
    margin: 0;
    list-style-type: none;
    padding: 0;
}

.ms-container .ms-selectable li.ms-hover, .ms-container .ms-selection li.ms-hover {
    cursor: pointer;
    color: #fff !important;
    text-decoration: none;
    background-color: #08c;
}

.ms-container .ms-selectable li.ms-elem-selectable, .ms-container .ms-selection li.ms-elem-selection {
    border-bottom: 1px #eee solid;
    padding: 2px 10px;
    color: #6e6767;
    font-size: 14px;
}

.ms-elem-selectable {
    line-height: 30px !important;
}

.hero-unit li {
    line-height: 30px;
}



select {
    /*width: 100%;
    border: 1px solid #d8d8d8 !important;
    padding: 6px 6px 7px !important;
    border-radius: 3px !important;*/
    box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.1);
    transition: all 0.3s ease;
}

select:focus {
    outline: none;
    box-shadow: 0 0 0 2px #1a73e8;
}

input[type="text"], input[type="number"], input[list] {
    border: 1px solid #d8d8d8 !important;
    padding: 6px 6px 7px !important;
    border-radius: 3px !important;
    line-height: inherit !important;
    box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.1);
    transition: all 0.3s ease;
    height: 30px;
    font-size: 13px;
    /*border: 1px solid #00809D;*/
}

textarea {
    border: 1px solid #d8d8d8 !important;
    padding: 6px 6px 7px !important;
    border-radius: 3px !important;
    line-height: inherit !important;
    box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.1);
    transition: all 0.3s ease;
    font-size: 13px;
}

textarea:focus {
    outline: none;
    box-shadow: 0 0 0 2px #1a73e8;
}

input[type="text"]:focus, input[type="number"]:focus {
    outline: none;
    box-shadow: 0 0 0 2px #1a73e8;
}

input[list]:focus {
    outline: none;
    box-shadow: 0 0 0 2px #1a73e8;
}

.container {
    width: auto;
}

.tg-list-item {
    margin: 0 2em;
}

.tgl {
    display: none;
}

.tgl, .tgl:after, .tgl:before, .tgl *, 
.tgl *:after, .tgl *:before, 
.tgl + .tgl-btn {
    box-sizing: border-box;
}

.tgl::-moz-selection, .tgl:after::-moz-selection, .tgl:before::-moz-selection, 
.tgl *::-moz-selection, .tgl *:after::-moz-selection, .tgl *:before::-moz-selection, 
.tgl + .tgl-btn::-moz-selection {
    background: none;
}

.tgl::selection, .tgl:after::selection, .tgl:before::selection, 
.tgl *::selection, .tgl *:after::selection, 
.tgl *:before::selection, .tgl + .tgl-btn::selection {
    background: none;
}

.tgl + .tgl-btn {
    outline: 0;
    display: block;
    width: 4em;
    height: 2em;
    position: relative;
    cursor: pointer;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
}

.tgl-skewed + .tgl-btn {
    overflow: hidden;
    -webkit-transform: skew(-10deg);
    transform: skew(-10deg);
    -webkit-backface-visibility: hidden;
    backface-visibility: hidden;
    -webkit-transition: all .2s ease;
    transition: all .2s ease;
    font-family: sans-serif;
    background: #888;
}

.tgl-skewed + .tgl-btn:after, 
.tgl-skewed + .tgl-btn:before {
    -webkit-transform: skew(10deg);
    transform: skew(10deg);
    display: inline-block;
    -webkit-transition: all .2s ease;
    transition: all .2s ease;
    width: 100%;
    text-align: center;
    position: absolute;
    line-height: 2em;
    font-weight: bold;
    color: #fff;
    text-shadow: 0 1px 0 rgba(0, 0, 0, 0.4);
}

.tgl-skewed + .tgl-btn:after {
    left: 100%;
    content: attr(data-tg-on);
}

.tgl-skewed + .tgl-btn:before {
    left: 0;
    content: attr(data-tg-off);
}

.tgl-skewed + .tgl-btn:active {
    background: #888;
}

.tgl-skewed + .tgl-btn:active:before {
    left: -10%;
}
.dropbtn {
    padding: 16px;
    font-size: 16px;
    border: none;
    cursor: pointer;
}

#myInput {
    box-sizing: border-box;
    background-position: 14px 12px;
    background-repeat: no-repeat;
    font-size: 16px;
    border: none;
    border-bottom: 1px solid #ddd;
}

#myInput:focus {
    outline: 3px solid #ddd;
}

.dropdown {
    position: relative;
    display: inline-block;
}

.dropdown-content {
    display: none;
    position: absolute;
    background-color: #f6f6f6;
    min-width: 230px;
    overflow: auto;
    border: 1px solid #ddd;
    z-index: 1;
}

.dropdown-content a {
    color: black;
    padding: 12px 16px;
    text-decoration: none;
    display: block;
}

.dropdown a:hover {
    background-color: #ddd;
}

.show {
    display: block;
}

.tgl-skewed:checked + .tgl-btn {
    background: #86d993;
}

.tgl-skewed:checked + .tgl-btn:before {
    left: -100%;
}

.tgl-skewed:checked + .tgl-btn:after {
    left: 0;
}

.tgl-skewed:checked + .tgl-btn:active:after {
    left: 10%;
}
.modal {
    background-color: #0000006e !important;
}
.modal.left .modal-dialog,
.modal.right .modal-dialog {
    position: fixed;
    margin: auto;
    width: 50%;
    height: 100%;
    -webkit-transform: translate3d(0%, 0, 0);
    -ms-transform: translate3d(0%, 0, 0);
    -o-transform: translate3d(0%, 0, 0);
    transform: translate3d(0%, 0, 0);
    z-index: 1031;
}

.modal.left .modal-content,
.modal.right .modal-content {
    height: 100%;
    overflow-y: auto;
}

.modal.left .modal-body,
.modal.right .modal-body {
    padding: 15px 15px 80px;
}
.modal-content {
    border-radius: 0;
    border: none;
}

.modal-header {
    border-bottom-color: #EEEEEE;
    background-color: #FAFAFA;
}
.demo {
    padding-top: 60px;
    padding-bottom: 110px;
}

.btn-demo {
    margin: 15px;
    padding: 10px 15px;
    border-radius: 0;
    font-size: 16px;
    background-color: #FFFFFF;
}

.btn-demo:focus {
    outline: 0;
}

.demo-footer {
    position: fixed;
    bottom: 0;
    width: 100%;
    padding: 15px;
    background-color: #212121;
    text-align: center;
}

.demo-footer > a {
    text-decoration: none;
    font-weight: bold;
    font-size: 16px;
    color: #fff;
}

</style>
            <title></title>
             <%--<link href="../css_new/1.12.1/HapNew.css" type="text/css" rel="Stylesheet" />--%>
              <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        </head>
           
        <body>

            <form runat="server">

                 <div>
     <%-- <div class="col-lg-12 sub-header">Asset Custom Fields --%>
          <br />
     <div class="row">
         <label style="float: left !important;padding-top: 3px;font-weight:bold;font-size:15px;"><span style='color:Red'>*</span>&nbsp;Module Name :</label>
         <div style="float: left !important;padding-top: 3px;" > 
   
    <div style="padding-left:10px; ">
        <select id="ddlmodule" class="form-control">
            <option value="0" >--Select--</option>
            <option value="1" >Add Asset</option>
            <option value="2" >Category</option>
            <option value="3" >Model</option>
            <option value="4" >Location</option>
            <option value="5" >Vendor</option>
        </select>
    </div>
</div>
     <div style="float: right;padding-top: 3px;">
             <ul class="segment">
                 <li data-va='All'>ALL</li>
                 <li data-va='0' class="active">Active</li>
             </ul>
        </div> 
         <div style="float: right;padding-top: 3px;">
             <button class="btn btn-primary" id="newfield" data-toggle="modal" data-target="#addcustomfild">Add Fields</button>
     
     </div>
 </div>
     <%--</div>--%>
 </div>
     <div class="card" id="AddModuleFields">
     <div class="row">
             <br />
             <div class="card-body table-responsive" style="overflow-x: auto;">
                  <div style="white-space: nowrap">
     Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />&nbsp;&nbsp;&nbsp;&nbsp;
      <label style="float: right">
         Show
     <select class="data-table-basic_length" aria-controls="data-table-basic">
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
                             <th style="text-align: left;">Field Name</th>
                             <th style="text-align: left;">Field Type</th>
                             <th style="text-align: left;">Field Length</th>                                            
                             <th style="text-align: left;">Mandatory</th>  
                             <th style="text-align: left;">Edit</th> 
                             <th style="text-align: left;">Status</th>             
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
                 <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                 <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
             </ul>
         </div>
     </div>
 </div>
             </div>    
     </div>                   
 </div>   

                          <div id="addcustomfild" class="modal fade" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
                    <div class="modal-dialog" role="document" style="width: 80% !important">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">Add Fields</h4>
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
                <label for="finame" style="padding-top: 5px !important;"><span style='Color:Red'>*</span>&nbsp;Field Type</label>
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
                </select>
            </div>
        </div>                                            
    </div>                                   
    <br />

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

    <%--<div class="row" id="datarow" style="margin-bottom: 1rem!important;padding-top:11px !important;">
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
    </div>--%>
    
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

    <%-- <div class="row" style="margin-bottom: 1rem!important;padding-top:11px !important;">
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
    </div>      --%>
    
</div>
                            <div class="modal-footer">                                    
                                 <button type="button" style="background-color: #1a73e8;" class="btn btn-primary svfields" id="svfieldgsm">Save</button>
                           </div>
                           <%-- <div class="modal-footer" style="margin-top:-40px !important;">
                                <button type="button" id="btnsave" class="btn btn-success">Save</button>
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>--%>
                        </div>
                    </div>
                </div>


                </form>
             <script type="text/javascript">
                 $(document).ready(function () {
                     FiledTypeOnChange();
                 });
                 $('#newfield').on('click', function () {
                     //if ($("#ddlmodule option:selected").val() == '0') {
                     //    alert("Please select Moduel.");
                     //    $("#ddlmodule").focus();
                     //    return false;
                     //}
                 });
                 $('#ddlmodule').on('change', function () {
                     loadCustomFields();
                 });
                 function loadCustomFields() {
                     var ModuleId = $("#ddlmodule option:selected").val();
                 }
                 $('#ddltypes').on('change', function () {

                     FiledTypeOnChange();
                 });
                 function FiledTypeOnChange() {
                     var selectyp = $('#ddltypes').val();
                     $('.hiderow').hide();
                     $('#Filter').hide();
                     switch (selectyp) {
                         case 'L':
                             $('.daterow').hide();$('.timerow').hide();$('#selectrow').hide();
                             $('#numberrow').hide();$('#phonenrow').hide();$('#currlist').hide();
                             $('#textrow').hide();$('#Mtablerow').hide();$('.Fieldrow').hide();
                             $('#Filter').hide();$('.Maxlenrow').hide();$('.Mandrow').hide();
                             $('#datarow').hide();$('#Customvrow').hide();$('#Templaterow').hide();
                             $('#filerow').hide();
                             break;
                         case 'TA':
                             $('.daterow').hide();$('#datarow').hide();$('#Mtablerow').hide();
                             $('.Fieldrow').hide();$('#Filter').hide();$('.timerow').hide();
                             $('.Maxlenrow').show();$('.Mandrow').show();$('#numberrow').hide();
                             $('#phonenrow').hide();$('#currlist').hide();$('#selectrow').hide();
                             $('#textrow').show();$('#Customvrow').hide();$('#Templaterow').hide();
                             $('#filerow').hide();
                             break;
                         case 'N':
                             $('.daterow').hide();$('#datarow').hide();$('#Mtablerow').hide();
                             $('.Fieldrow').hide();$('#Filter').hide();$('.timerow').hide();
                             $('#selectrow').hide();$('#textrow').hide();$('.Maxlenrow').hide();
                             $('.Mandrow').show();$('#numberrow').show();$('#phonenrow').show();
                             $('#currlist').show();$('#Customvrow').hide();
                             $('#Templaterow').hide(); $('#filerow').hide();
                             break;
                         case 'D':
                             $('#datarow').hide();$('#Mtablerow').hide();$('.Fieldrow').hide();
                             $('#Filter').hide();$('.Maxlenrow').hide();$('#selectrow').hide();
                             $('#numberrow').hide();$('#phonenrow').hide();$('#currlist').hide();
                             $('#textrow').hide();$('.daterow').show();$('.Mandrow').show();
                             $('.timerow').hide();$('#filerow').hide();$('#Customvrow').hide();
                             $('#Templaterow').hide();
                             break;
                         case 'T':
                             $('#datarow').hide();$('#Mtablerow').hide();$('#selectrow').hide();
                             $('#numberrow').hide();$('#phonenrow').hide();$('#currlist').hide();
                             $('#textrow').hide();$('#filerow').hide();$('.Fieldrow').hide();
                             $('#Filter').hide();$('.Maxlenrow').hide();$('.Mandrow').show();
                             $('.daterow').hide();$('.timerow').show();$('#Customvrow').hide();
                             $('#Templaterow').hide();
                             break;
                         case 'S':
                             $('#Filter').hide();$('.Maxlenrow').hide();$('.daterow').hide();
                             $('.timerow').hide();$('#filerow').hide();$('#datarow').show();
                             $('#Mtablerow').show();$('.Fieldrow').hide();$('#selectrow').show();
                             $('#numberrow').hide();$('#phonenrow').hide();$('#currlist').hide();
                             $('#textrow').hide();$('.Mandrow').show();

                             break;

                         case 'R':
                             $('#Filter').hide();$('.Maxlenrow').hide();$('.daterow').hide();
                             $('.timerow').hide();$('#filerow').hide();$('#datarow').show();
                             $('#Mtablerow').show();$('.Fieldrow').hide(); $('#selectrow').hide();
                             $('#numberrow').hide();$('#phonenrow').hide();$('#currlist').hide();
                             $('#textrow').hide();$('.Mandrow').show();
                             break;

                         case 'C':
                             $('#Filter').hide(); $('.Maxlenrow').hide();
                             $('.daterow').hide(); $('.timerow').hide();
                             $('#filerow').hide(); $('#filerow').hide();
                             $('#textrow').hide(); $('#numberrow').hide();
                             $('#phonenrow').hide(); $('#currlist').hide();

                             $('#datarow').show();$('#Mtablerow').show();
                             $('.Fieldrow').hide();$('#selectrow').hide();
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
                             $('.daterow').hide();$('#datarow').hide();$('#Mtablerow').hide();
                             $('.Fieldrow').hide(); $('#Filter').hide();$('.timerow').hide();
                             $('.Maxlenrow').hide();$('.Mandrow').hide();
                             $('#selectrow').hide(); $('#textrow').hide();
                             $('#filerow').hide();$('#numberrow').hide();
                             $('#phonenrow').hide(); $('#currlist').hide();
                             break;
                         default:
                             break;
                     }

                 }
             </script>
                </body>
</html>
</asp:Content>

