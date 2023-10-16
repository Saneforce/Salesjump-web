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
     <div style="float: right;padding-top: 3px;"
             <ul class="segment">
                 <li data-va='All'>ALL</li>
                 <li data-va='0' class="active">Active</li>
             </ul>
        </div> 
         <div style="float: right;padding-top: 3px;">
             <button class="btn btn-primary" id="newfield" data-toggle="modal" data-target="#addcustomfild">Add Fields</button>
     
     </div>
 </div>
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

    
</div>
                            <div class="modal-footer">                                    
                                 <button type="button" style="background-color: #1a73e8;" class="btn btn-primary svfields" id="svfieldgsm">Save</button>
                           </div>
                        </div>
                    </div>
                </div>


                </form>
             <script type="text/javascript">
                 var masflds = []; let hefild = '', filtrkey = '0';
                 var Orders = [];MasFrms=[], pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "ModuleName,Field_Name,Field_Col,FieldType,Mandate,Status";
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

                 function ReloadTable() {
                     $("#OrderList TBODY").html("");
                     if (filtrkey != "All") {
                         Orders = Orders.filter(function (a) {
                             return a.flag == filtrkey;
                         })
                     }
                     st = PgRecords * (pgNo - 1); slno = 0;

                     for ($i = st; $i < st + PgRecords; $i++) {
                         if ($i < Orders.length) {
                             tr = $("<tr class='" + Orders[$i].Field_Id + "' id='" + Orders[$i].Field_Id + "'></tr>");
                             //var hq = filtered[$i].Sf_Name.split('-');
                             slno = $i + 1;

                             $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].ModuleName +
                                 '</td><td> ' + Orders[$i].Field_Name + '</td ><td>' + Orders[$i].FieldType +
                                 '</td><td>' + Orders[$i].Fld_Length + '</td><td>' + Orders[$i].Mandate +
                                 '</td><td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                                 + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                                 '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + ((Orders[$i].Status == "Active") ? '<li><a href="#" v="1">Deactivate</a></li>' : '<li><a href="#" v="0">Active</a></li>') + '</ul></li></ul></td>'
                                 + '<td class= "frmedit" id="' + Orders[$i].Field_Id + '"> <a href="#" data-toggle="modal" data-target="#addcustomfild"><span class="glyphicon glyphicon-edit"></span></a></td > ');
                             $("#OrderList TBODY").append(tr);
                         }
                     }

                     $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

                     loadPgNos();
                 }
                 $(".segment>li").on("click", function () {
                     $(".segment>li").removeClass('active');
                     $(this).addClass('active');
                     filtrkey = $(this).attr('data-va');
                     Orders = MasFrms;
                     $("#tSearchOrd").val('');
                     ReloadTable();
                 });
                 $(document).on('click', '.frmedit', function () {
                     var FldID = $(this).closest('tr').attr('id');

                     CustomloadFields(FldID);

                 });
                 $(document).on('click', ".ddlStatus>li>a", function () {
                     //$(".ddlStatus>li>a").on("click", function () {
                     cStus = $(this).closest("td").find(".aState");
                     let fieldid = $(this).closest("tr").find(".frmedit").attr("id");
                     stus = $(this).attr("v");
                     $indx = Orders.findIndex(x => x.Field_Id == fieldid);
                     cStusNm = $(this).text();
                     if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                         id = Orders[$indx].Field_Id;
                         $.ajax({
                             type: "POST",
                             contentType: "application/json; charset=utf-8",
                             async: false,
                             url: "Asset_Custom_Fields.aspx/SetNewStatus",
                             data: "{'ID':'" + id + "','stus':'" + stus + "'}",
                             dataType: "json",
                             success: function (data) {
                                 Orders[$indx].flag = stus;
                                 Orders[$indx].Status = cStusNm;
                                 $(cStus).html(cStusNm);

                                 ReloadTable();
                                 alert('Status Changed Successfully...');

                             },
                             error: function (result) {
                             }
                         });
                     }
                     loadPgNos();
                 });

                 $(".data-table-basic_length").on("change", function () {
                     pgNo = 1;
                     PgRecords = $(this).val();
                     ReloadTable();

                 });
                 function CustomloadFields(FldID) {
                     var FldArr = []; var FldSrcName = ''; var multiopt = ''; var Mand = '';
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         async: false,
                         url: "Asset_Custom_Fields.aspx/GetCustomFormsFieldsDataById",
                         data: "{'divcode':'<%=Session["div_code"]%>','FieldId':'" + FldID + "'}",
        dataType: "json",

        success: function (data) {
            FldArr = JSON.parse(data.d) || [];
            //console.log(FldArr);
            //FldArr = JSON.parse(data.d);

            $('#fieldId').val(FldArr[0].Field_Id);
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
                    //$('#ddltypes').selectpicker('val', 'S');

                    $('#sslec').prop('checked', true);
                    $('#master').prop('checked', true);

                    masterch();

                    var element = $('#mtables');
                    var velement = $('#ddlvaluef');
                    var telement = $('#ddltextf');

                    if ((FldSrcName != '' || FldSrcName != null)) {

                        loadTableColumns(FldSrcName);
                        $(element).val(FldSrcName);
                        $(velement).val(multiopt[0]);
                        $(telement).val(multiopt[1]);

                        //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                        //$(velement).selectpicker('val', multiopt[0]);
                        //$(telement).selectpicker('val', multiopt[1]);

                    }

                    FiledTypeOnChange();
                    break;
                case 'SSO':
                    $('#ddltypes').val('S');
                    //$('#ddltypes').selectpicker('val', 'S');

                    $('#sslec').prop('checked', true);
                    $('#others').prop('checked', true);
                    othersch();
                    $('#custt').val(FldArr[0].Fld_Src_Name);
                    fillvalfields(FldArr[0].Fld_Src_Name);
                    var newopt = '';
                    for ($j = 0; $j < multiopt.length; $j++) {
                        newopt += multiopt[$j] + '\n';
                    }
                    $('#custemps').val(newopt);
                    FiledTypeOnChange();

                    break;
                case 'SMM':
                    $('#ddltypes').val('S');
                    //$('#ddltypes').selectpicker('val', 'S');

                    $('#mselc').prop('checked', true);
                    $('#master').prop('checked', true);

                    masterch();
                    var element = $('#mtables');
                    var velement = $('#ddlvaluef');
                    var telement = $('#ddltextf');

                    if ((FldSrcName != '' || FldSrcName != null)) {

                        loadTableColumns(FldSrcName);
                        $(element).val(FldArr[0].Fld_Src_Name);
                        $(velement).val(multiopt[0]);
                        $(telement).val(multiopt[1]);

                        //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                        //$(velement).selectpicker('val', multiopt[0]);
                        //$(telement).selectpicker('val', multiopt[1]);

                    }
                    FiledTypeOnChange();
                    break;
                case 'SMO':
                    $('#ddltypes').val('S'); //$('#ddltypes').selectpicker('val', 'S');

                    $('#mselc').prop('checked', true);
                    $('#others').prop('checked', true);
                    othersch();
                    $('#custt').val(FldArr[0].Fld_Src_Name);
                    fillvalfields(FldArr[0].Fld_Src_Name);
                    var newopt = '';
                    for ($j = 0; $j < multiopt.length; $j++) {
                        newopt += multiopt[$j] + '\n';
                    }
                    $('#custemps').val(newopt);
                    FiledTypeOnChange();
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

                    var element = $('#mtables');
                    var velement = $('#ddlvaluef');
                    var telement = $('#ddltextf');

                    if ((FldSrcName != '' || FldSrcName != null)) {

                        loadTableColumns(FldSrcName);
                        $(element).val(FldArr[0].Fld_Src_Name);
                        $(velement).val(multiopt[0]);
                        $(telement).val(multiopt[1]);

                        //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                        //$(velement).selectpicker('val', multiopt[0]);
                        //$(telement).selectpicker('val', multiopt[1]);

                    }
                    masterch();
                    FiledTypeOnChange();
                    break;
                case 'RO':
                    $('#ddltypes').val('R');

                    //$('#ddltypes').selectpicker('val', 'R');
                    $('#datarow').show();
                    $('#others').prop('checked', true);
                    othersch();
                    $('#custt').val(FldArr[0].Fld_Src_Name);
                    var newopt = '';
                    for ($j = 0; $j < multiopt.length; $j++) {
                        newopt += multiopt[$j] + '\n';
                    }
                    $('#custemps').val(newopt);
                    FiledTypeOnChange();
                    break;
                case 'CM':
                    $('#ddltypes').val('C');

                    //$('#ddltypes').selectpicker('val', 'C');
                    $('#datarow').show();
                    $('#master').prop('checked', true);

                    masterch();
                    var element = $('#mtables');
                    var velement = $('#ddlvaluef');
                    var telement = $('#ddltextf');

                    if ((FldSrcName != '' || FldSrcName != null)) {

                        loadTableColumns(FldSrcName);
                        $(element).val(FldSrcName);
                        $(velement).val(multiopt[0]);
                        $(telement).val(multiopt[1]);

                        //$(element).selectpicker('val', FldArr[0].Fld_Src_Name);
                        //$(velement).selectpicker('val', multiopt[0]);
                        //$(telement).selectpicker('val', multiopt[1]);

                    }
                    FiledTypeOnChange();
                    break;
                case 'CO':
                    $('#ddltypes').val('C');

                    //$('#ddltypes').selectpicker('val', 'C');
                    $('#datarow').show();
                    $('#others').prop('checked', true);
                    othersch();
                    $('#custt').val(FldArr[0].Fld_Src_Name);
                    var newopt = '';
                    for ($j = 0; $j < multiopt.length; $j++) {
                        newopt += multiopt[$j] + '\n';
                    }
                    $('#custemps').val(newopt);
                    FiledTypeOnChange();
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
            //change
        },
        error: function (data) {
            alert(JSON.stringify(data.d))
        }
    })
                 }

                 function loadCustomFields() {
                     var ModuleId = $("#ddlmodule option:selected").val();

                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         async: false,
                         url: "Asset_Custom_Fields.aspx/GetCustomFormsFieldsList",
                         data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'" + ModuleId + "'}",
                        dataType: "json",
                        success: function (data) {
                            MasFrms = JSON.parse(data.d) || [];
                            Orders = MasFrms;
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                     ReloadTable();
                 }

                 $(document).ready(function () {
                     FiledTypeOnChange();
                 });
                 $('#newfield').on('click', function () {
                     if ($("#ddlmodule option:selected").val() == '0') {
                         alert("Please select Moduel.");
                         $("#ddlmodule").focus();
                         return false;
                     }
                 });
                 $('#ddlmodule').on('change', function () {
                     loadCustomFields();
                 });
                 //function loadCustomFields() {
                 //    var ModuleId = $("#ddlmodule option:selected").val();
                 //}
                 $('#ddltypes').on('change', function () {

                     FiledTypeOnChange();
                 });

                 $('input[name="numtype"]').on('change', function () {
                     $('#currlist').val('0');
                     if ($('#cuurency').is(":checked")) {
                         $('#currlist').show();
                     }
                     if (!$('#cuurency').is(":checked")) {
                         $('#currlist').hide();
                     }
                     if ($('#phonen').is(":checked")) {
                         $('.Maxlenrow').show();
                     }
                     if (!$('#phonen').is(":checked")) {
                         $('.Maxlenrow').hide();
                     }
                 });

                 $('#svfieldgsm').on('click', function () {
                     var FieldType = $('#ddltypes').val();
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
                     //var FGroupId = '';


                     var ModuleName = $("#ddlmodule").val();
                     if (ModuleName == "0") {
                         alert('Please Select Module Name !!');
                         $("#ddlmodule").focus();
                         return false;
                     }


                     //FGroupId = $("#ddlfgroup").val();
                     //if (FGroupId == "0") {
                     //    alert('Please Select Group Name !!');
                     //    $("#FGroupId").focus();
                     //    return false;
                     //}


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
                             sfldtyp += $('input[type="radio"][name="numtype"]:checked').val() || '';
                             currtyp = $('#currlist').val();
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
                         //case 'F':
                         //    if ($('input[type="checkbox"][name="flupl"]:checked').length < 1) {
                         //        alert('Select Atleast One File Upload Option');
                         //        sels = false;
                         //        return false;
                         //    }
                         //    $('input[type="checkbox"][name="flupl"]:checked').each(function () {
                         //        sfldtyp += $(this).val();
                         //    });
                         //    break;
                         default:
                             break;
                     }

                     

                     if ($('#mand').prop('checked') == true) {
                         Fld_Mandatory = '1';
                     }
                     else {
                         Fld_Mandatory = '0';
                     }


                     var AccessPoint = ($('#accwa').prop('checked')) ? 1 : 0;

                     SrtNo = $('#sortingnum').val();
                     var Control_id = masflds.filter(function (a) {
                         return a.ControlName == sfldtyp;
                     });
                     var Fldtype = '';
                     if ((Control_id[0].ControlID == 3 || Control_id[0].ControlID == 18)) {
                         Fldtype = 'varchar(150)';
                     }

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
                         //'FGroupId': FGroupId
                     };


                     if (Formdata.ModuleId != "") {
                         $.ajax({
                             type: "POST",
                             contentType: "application/json; charset=utf-8",
                             async: false,
                             url: "Asset_Custom_Fields.aspx/AddCustomField",
                             data: "{'Formdata':'" + JSON.stringify(Formdata) + "','sf_Code':'<%=Session["Sf_Code"]%>','cusxml':'" + cusxml + "'}",
                            dataType: "json",
                            success: function (msg) {
                                var frmresult = msg.d;
                                alert(frmresult);
                                FiledTypeOnChange();
                                clearFields();
                            },
                            error: function (msg) {
                                alert(JSON.stringify(msg));
                                frmcr = false;
                            }

                         });
                     }
                 });
                 $('#others').on('change', function () {
                     if ($(this).is(":checked")) {
                         othersch();
                     }
                 });
                 function othersch() {
                     $('#custt').val('');
                     $('#custemps').val('');
                     
                 }  
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
                     $('#currlist').val('');
                     $('#mtables').val('');
                     $('#ddlvaluef').empty();
                     $('#ddltextf').empty();


                 }

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
                             $('.Mandrow').show(); $('#numberrow').show(); $('#phonenrow').show();
                             $('#phonen').prop('checked','true');$('.Maxlenrow').show();
                             $('#currlist').hide();$('#Customvrow').hide();
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
                             $('#Mtablerow').hide();$('.Fieldrow').hide();$('#selectrow').show();
                             $('#numberrow').hide();$('#phonenrow').hide();$('#currlist').hide();
                             $('#textrow').hide(); $('.Mandrow').show();
                             $('#Templaterow').show();
                             $('#Customvrow').show();
                             $('#Fieldrow').hide();
                             $('#ValueFRow').hide();
                             $('#TextFRow').hide();
                             $('#Filter').hide();

                             break;

                         case 'R':
                             $('#Filter').hide();$('.Maxlenrow').hide();$('.daterow').hide();
                             $('.timerow').hide();$('#filerow').hide();$('#datarow').show();
                             $('#Mtablerow').show();$('.Fieldrow').hide(); $('#selectrow').hide();
                             $('#numberrow').hide();$('#phonenrow').hide();$('#currlist').hide();
                             $('#textrow').hide(); $('.Mandrow').show();
                             $('#Templaterow').show();
                             $('#Customvrow').show();
                             $('#Fieldrow').hide();
                             $('#ValueFRow').hide();
                             $('#TextFRow').hide();
                             $('#Filter').hide();
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
                             $('#Templaterow').show();
                             $('#Customvrow').show();
                             $('#Fieldrow').hide();
                             $('#ValueFRow').hide();
                             $('#TextFRow').hide();
                             $('#Filter').hide();

                             break;
                         //case 'F':
                         //    $('#Filter').hide(); $('.Maxlenrow').hide();
                         //    $('.daterow').hide(); $('.timerow').hide();
                         //    $('#numberrow').hide(); $('#phonenrow').hide();
                         //    $('#currlist').hide(); $('#filerow').hide();
                         //    $('#datarow').hide(); $('#Mtablerow').hide();
                         //    $('.Fieldrow').hide(); $('#selectrow').hide();
                         //    $('#textrow').hide(); $('.Mandrow').hide();
                         //    $('#filerow').show();
                         //    break;
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

