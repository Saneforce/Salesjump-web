<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="New_Claim_Master.aspx.cs" Inherits="MasterFiles_New_Claim_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">

    <style>
        .multiselect {
  width: 200px;
}

.selectBox {
  position: relative;
}

.selectBox select {
  width: 100%;
  font-weight: bold;
}

.overSelect {
  position: absolute;
  left: 0;
  right: 0;
  top: 0;
  bottom: 0;
}
.info-icon {
  position: relative;
  display: inline-block;
}

.tooltip {
  visibility: hidden;
  width: 250px;
  background-color: #333;
  color: #fff;
  text-align: center;
  border-radius: 6px;
  padding: 5px;
  position: absolute;
  z-index: 1;
   top: 5px;
    left: 475%;
  margin-left: -75px;
  opacity: 0;
  transition: opacity 0.3s;
}

        .info-icon:hover .tooltip {
            visibility: visible;
            opacity: 1;
        }

        .info-icon .fas.fa-info-circle {
  color: darkseagreen;
  font-size: 20px;
}
         .segment3 {
            display: inline-block;
            padding-left: 0;
            margin: -2px 22px;
            border-radius: 4px;
            font-size: 13px;
            font-family: "Poppins";
        }

            .segment3 > li {
                display: inline-block;
                background: #fafafa;
                color: #666;
                margin-left: -4px;
                padding: 5px 10px;
                min-width: 50px;
                border: 1px solid #ddd;
            }

                .segment3 > li:first-child {
                    border-radius: 4px 0px 0px 4px;
                }

                .segment3 > li:last-child {
                    border-radius: 0px 4px 4px 0px;
                }

                .segment3 > li.active {
                    color: #fff;
                    cursor: default;
                    background-color: #428bca;
                    border-color: #428bca;
                }

        .segment2 {
            display: inline-block;
            padding-left: 0;
            margin: -2px 22px;
            border-radius: 4px;
            font-size: 13px;
            font-family: "Poppins";
            /* border: 1px solid  #66a454   #d3252a  #f59303;*/
        }

            .segment2 > li {
                display: inline-block;
                background: #fafafa;
                color: #666;
                margin-left: -4px;
                padding: 5px 10px;
                min-width: 50px;
                border: 1px solid #ddd;
            }

                .segment2 > li:first-child {
                    border-radius: 4px 0px 0px 4px;
                }

                .segment2 > li:last-child {
                    border-radius: 0px 4px 4px 0px;
                }

                .segment2 > li.active {
                    color: #fff;
                    cursor: default;
                    background-color: #428bca;
                    border-color: #428bca;
                }
				                .aState1[data-val='Active']
                {color: #66a454 !important;}
                .aState1[data-val='Deactivate']
                {color: #d3252a;}

                .aState2[data-val='Active']
{color: #66a454 !important;}
.aState2[data-val='Deactivate']
{color: #d3252a;}

.aState3[data-val='Active']
{color: #66a454 !important;}
.aState3[data-val='Deactivate']
{color: #d3252a;}

    </style>
     <form id="frm" runat="server">
         <div class="container" style="max-width: 100%">
             <ul class="nav nav-tabs">
                 <li class="active"><a data-toggle="tab" href="#slab" id="slabtab">Sales Slab</a></li>
                    <li><a data-toggle="tab" href="#bill" id="billtab">Product Billed</a></li>
                    <%--<li><a data-toggle="tab" href="#distab" id="dista">In-Display</a></li>--%>
                    <li><a data-toggle="tab" href="#gift" id="claimtab">Claim/Gift Products</a></li>
                 </ul>
             <div class="tab-content">
             <div id="slab" class="tab-pane fade in active">
                    <button class="btn btn-primary" id="rbsbtn" type="button" data-toggle="modal" data-target="#Slab" style="margin-right: 10px; margin-top: -40px; float: right" onclick="clear()">Create</button>
             <div class="tab-content card" style="margin-top: 0px;">
                        <div class="card-body table-responsive">
                            <div style="white-space: nowrap">
                                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd1" autocomplete="off" style="width: 250px;" />
                                <label style="float: right">
                                    Show
                                        <select class="data-table-basic_length2" aria-controls="data-table-basic">
                                            <option value="10">10</option>
                                            <option value="25">25</option>
                                            <option value="50">50</option>
                                            <option value="100">100</option>
                                        </select>
                                    entries</label>
                                <div style="float: right">
                                    <ul class="segment">
                                        <li data-va='All'>ALL</li>
                                        <li data-va='0' class="active">Active</li>
                                    </ul>
                                </div>
                            </div>
                            <table class="table table-hover" id="retailbslab">
                                <thead class="text-warning">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Name</th>
                                        <th>Claim month/year</th>
                                        <th>Min Value</th>
                                        <th>Max Value</th>
                                        <th>Slab Amount Calculation</th>
                                        <th>Value</th>
                                        <th>Claim Form</th>
                                        <th>HQ Mapped</th>
                                        <th>Retailers Mapped</th>
                                        <th>Slab duration From and To</th>
                                        <th>Claim End date</th>
                                        <th>Edit</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="dataTables_info" id="retailbslab_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="dataTables_paginate paging_simple_numbers" id="example_paginate">
                                        <ul class="pagination">
                                            <li class="paginate_button previous disabled" id="example_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                            <li class="paginate_button next" id="example_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>

                  <div class="modal fade" id="HQModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 80% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="HQModalLabel"></h5>
                    </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <table id="hqdets" style="width: 100%; font-size: 12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">S.NO</th>
                                            <th style="text-align: left">HQ Code</th>
                                            <th style="text-align: left">HQ Name</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
                  <div class="modal fade" id="RModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 80% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="RModalLabel"></h5>
                    </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <table id="rdets" style="width: 100%; font-size: 12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">S.NO</th>
                                            <th style="text-align: left">Retailer Code</th>
                                            <th style="text-align: left">Retailer Name</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
                 <%-- <HeaderStyle HorizontalAlign="Left" />--%>
                 
                    <div id="Slab" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #00000000">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">SALES SLAB CREATE</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hbcode" />
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <label>Name  <span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8">
                                            <input type="text" id="txtbname" class="form-control" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Claim month/year (in Words) <span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8">
                                            <%--<input type="text" id="txtbdesc" class="form-control" autocomplete="off" maxlength="200" />--%>
                                            <input type="month" id="txtbdesc" class="form-control" name="bdaymonth">
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Min Value<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8">
                                            <input type="number" id="bminvalue" min="1" onkeyup="if(parseInt(this.value)<0){this.value='';return false}if(parseInt(this.value)>10000000000){ alert('Please Enter 10 digits'); this.value=Math.floor(this.value / 10); }" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Max Value<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8">
                                            <input type="number" id="bmaxvalue" onkeyup="if(parseInt(this.value)<0){this.value='';return false}if(parseInt(this.value)>10000000000){ alert('Please Enter 10 digits'); this.value=Math.floor(this.value / 10); }" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Claim Calculation</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <input type="radio" name="cc" id="fixed" value="0" checked="true">Fixed Amount<br>   
                                        </div>
                                         <div class="col-sm-4">
                                            <input type="radio" name="cc" id="percentage" value="1">Percentage<br> 
                                        </div>
                                         <div class="info-icon"><a><i class="fas fa-info-circle"></i></a><span class="tooltip">1) If the claim amount is a fixed value then select ‘Fixed Amount’ option<br>
                                                                                                                               2) If the claim amount is a percentage of invoice amount then select ‘percentage’ option
                                                                                                        </span></div>
                                    </div>
									<div class="row" style="margin-top: 10px;">
                                  <div class="col-sm-3"><label>Value<span hidden="hidden" id="val" style="color:forestgreen;font:bold;">  %</span><span  style="color:red;font:bold;">  *</span></label></div>
                                  <div class="col-sm-8">
                                      <input type="number" id="claimvalue" min="1" onkeyup="if(parseInt(this.value)<0){this.value='';return false}if(parseInt(this.value)>1000000){ alert('Please Enter 6 digits'); this.value=Math.floor(this.value / 10); }" class="form-control" />
                                    </div>
                                  </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Claim Form</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <input type="radio" name="cf" id="amount" value="0" checked="true">Amount<br>   
                                        </div>
                                         <div class="col-sm-4">
                                            <input type="radio" name="cf" id="product" value="1">Product<br> 
                                        </div>
                                        <div class="info-icon"><a><i class="fas fa-info-circle"></i></a><span class="tooltip">1) If the claim is to be given as amount then select ‘Amount’ option<br>
                                                                                                                              2) If the customer can avail products worth the claim amount then select ‘Products’ option
                                                                                                        </span></div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;display:none;" id="clgip">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">Claim/Gift Products<span  style="color:red;font:bold;">  *</span></label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlgift" multiple></select>
                                             </div>
                                        </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">HQ</label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlhq" multiple></select>
                                             </div>
                                        </div>
                                   <%-- <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">Retailer Category<span  style="color:red;font:bold;">  *</span></label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlretcat" multiple></select>
                                             </div>
                                        </div>--%>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">Retailer<span  style="color:red;font:bold;">  *</span></label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlret" multiple></select>
                                             </div>
                                        </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">Duration of Slab From<span  style="color:red;font:bold;">  *</span></label>
                                            &nbsp&nbsp&nbsp&nbsp
                                            <label>To<span  style="color:red;font:bold;">  *</span></label>&nbsp
                                        </div>
                                          <div class="col-sm-8">
                                            <input type="date" id="fdt" class="form-control" />
                                            &nbsp
                                          <input type="date" id="tdt" class="form-control" />
                                        </div>
                                        </div>
                                     <div class="row" style="margin-top: 10px;">
                                         <div class="col-sm-3">
                                              <label style="white-space: nowrap;">Claim EndDate<span  style="color:red;font:bold;">  *</span></label>
                                             </div>
                                         <div class="col-sm-8">
                                              <input type="date" id="ced" class="form-control" />
                                             </div>
                                         </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnsave" class="btn btn-success">Save</button>
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                 </div>
                 </div>

                  <%--Product Billed--%>
                 <div id="bill" class="tab-pane fade">
                    <button class="btn btn-primary" id="billprod" type="button" data-toggle="modal" data-target="#Billed" style="margin-right: 10px; margin-top: -40px; float: right">Create</button>
             <div class="tab-content card" style="margin-top: 0px;">
                        <div class="card-body table-responsive">
                            <div style="white-space: nowrap">
                                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd2" autocomplete="off" style="width: 250px;" />
                                <label style="float: right">
                                    Show
                                        <select class="data-table-basic_length1" aria-controls="data-table-basic">
                                            <option value="10">10</option>
                                            <option value="25">25</option>
                                            <option value="50">50</option>
                                            <option value="100">100</option>
                                        </select>
                                    entries</label>
                                <div style="float: right">
                                    <ul class="segment2">
                                        <li data-va='All'>ALL</li>
                                        <li data-va='0' class="active">Active</li>
                                    </ul>
                                </div>
                            </div>
                            <table class="table table-hover" id="billslab">
                                <thead class="text-warning">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Name</th>
                                        <th>Claim month/year</th>
                                        <th>Product Count</th>
                                        <th>Product Bill Type</th>
                                        <th>Claim Calculation</th>
                                        <th>Claim Value</th>
                                        <th>Claim Form</th>
                                        <th>HQ Mapped</th>
                                        <th>Retailers Mapped</th>
                                        <th>Slab duration From and To</th>
                                        <th>Claim End date</th>
                                        <th>Edit</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="dataTables_info" id="billslab_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="dataTables_paginate paging_simple_numbers" id="example_paginate1">
                                        <ul class="pagination">
                                            <li class="paginate_button previous disabled" id="example_previous1"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                            <li class="paginate_button next" id="example_next1"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    <div id="Billed" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #00000000">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">PRODUCT BILLED CLAIM</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="sbcode" />
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <label>Name  <span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8">
                                            <input type="text" id="txtbname1" class="form-control" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Claim month/year (in Words) <span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8">
                                            <input type="month" id="txtbdesc1" class="form-control" name="bdaymonth">
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Product Name<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlprodcatgry" multiple></select>
                                             </div>
                                        </div>
                                    <%--<div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Product Category<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8">
                                            <select class="form-control selectBox" id="ddlprodcatgry" multiple></select>
                                        </div>
                                    </div>--%>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Invoice Split Up Needed</label>
                                        </div>
                                          <div class="col-sm-3">
                                            <input type="radio" name="isun" id="yes" value="0" checked="true">Yes<br>   
                                        </div>
                                         <div class="col-sm-3">
                                            <input type="radio" name="isun" id="no" value="1">No<br> 
                                        </div>
                                    </div>
                                     <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Product Bill-Basis</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <input type="radio" name="bb" id="value" value="0" checked="true">Sale Value<br>   
                                        </div>
                                         <div class="col-sm-3" visible="false">
                                            <input type="radio" name="bb" id="qty" value="1">Sale Qty<br> 
                                        </div>
                                    </div>
                                    <div class="row hval" style="margin-top: 10px;">
                                  <div class="col-sm-3">
                                      <label>Sale Value<span  style="color:red;font:bold;">  *</span></label>
                                  </div>
                                  <div class="col-sm-8">
                                      <input type="number" id="salevalue" min="1" onkeyup="if(parseInt(this.value)<0){this.value='';return false}if(parseInt(this.value)>1000000){ alert('Please Enter 6 digits'); this.value=Math.floor(this.value / 10); }" class="form-control" />
                                    </div>
                                  </div>
                                    <div class="row hqty" style="margin-top: 10px;">
                                  <div class="col-sm-3">
                                      <label>Sale Qty<span  style="color:red;font:bold;">  *</span></label>
                                  </div>
                                  <div class="col-sm-8">
                                      <input type="number" id="saleqty" min="1" onkeyup="if(parseInt(this.value)<0){this.value='';return false}if(parseInt(this.value)>1000000){ alert('Please Enter 6 digits'); this.value=Math.floor(this.value / 10); }" class="form-control" />
                                    </div>
                                  </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Claim Calculation</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <input type="radio" name="cc1" id="fixed1" value="0" checked="true">Fixed Amount<br>   
                                        </div>
                                         <div class="col-sm-3">
                                            <input type="radio" name="cc1" id="percentage1" value="1">Percentage<br> 
                                        </div>
                                    </div>
									<div class="row" style="margin-top: 10px;">
                                  <div class="col-sm-3"><label>Value<span hidden="hidden" id="val1" style="color:forestgreen;font:bold;">  %</span><span  style="color:red;font:bold;">  *</span></label></div>
                                  <div class="col-sm-8">
                                      <input type="number" id="claimvalue1" min="1" onkeyup="if(parseInt(this.value)<0){this.value='';return false}if(parseInt(this.value)>1000000){ alert('Please Enter 6 digits'); this.value=Math.floor(this.value / 10); }" class="form-control" />
                                    </div>
                                  </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Claim Form</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <input type="radio" name="cf1" id="discount" value="0" checked="true">Discount<br>   
                                        </div>
                                         <div class="col-sm-4">
                                            <input type="radio" name="cf1" id="prod" value="1">Product<br> 
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;display:none;" id="clgip1">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">Claim/Gift Products<span  style="color:red;font:bold;">  *</span></label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlgift1" multiple></select>
                                             </div>
                                        </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">HQ</label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlhq1" multiple></select>
                                             </div>
                                        </div>
                                    <%--<div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">Retailer Category<span  style="color:red;font:bold;">  *</span></label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlretcat1" multiple></select>
                                             </div>
                                        </div>--%>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">Retailer<span  style="color:red;font:bold;">  *</span></label>
                                        </div>
                                         <div class="col-sm-8">
                                        <select class="form-control selectBox" id="ddlret1" multiple></select>
                                             </div>
                                        </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label style="white-space: nowrap;">Duration of Slab From<span  style="color:red;font:bold;">  *</span></label>
                                            &nbsp&nbsp&nbsp&nbsp
                                            <label>To<span  style="color:red;font:bold;">  *</span></label>&nbsp
                                        </div>
                                          <div class="col-sm-8">
                                            <input type="date" id="fdtt" class="form-control" />
                                            &nbsp
                                          <input type="date" id="tdtt" class="form-control" />
                                        </div>
                                        </div>
                                     <div class="row" style="margin-top: 10px;">
                                         <div class="col-sm-3">
                                              <label style="white-space: nowrap;">Claim EndDate<span  style="color:red;font:bold;">  *</span></label>
                                             </div>
                                         <div class="col-sm-8">
                                              <input type="date" id="ced1" class="form-control" />
                                             </div>
                                         </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnsave1" class="btn btn-success">Save</button>
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                 </div>
                 </div>

                 <%--Gift/Claim Products--%>
                 <div id="gift" class="tab-pane fade">
                      <button class="btn btn-primary" id="giftpbtn" type="button" data-toggle="modal" data-target="#giftpro" style="margin-right: 10px; margin-top: -40px; float: right">Create</button>
                    <div class="tab-content card" style="margin-top: 0px;">
                        <div class="card-body table-responsive">
                            <div style="white-space: nowrap">
                                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd3" autocomplete="off" style="width: 250px;" />
                                <label style="float: right">
                                    Show
                    <select class="data-table-basic_length3" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                                    entries</label>
                                <div style="float: right;">
                                    <ul class="segment3">
                                        <li data-va='All'>ALL</li>
                                        <li data-va='0' class="active">Active</li>
                                    </ul>
                                </div>
                            </div>
                            <table class="table table-hover" id="giftpList">
                                <thead class="text-warning">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Product Type</th>
                                        <th>Product Name</th>
                                        <th>Product Price</th>
                                        <th>Edit</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="dataTables_info" id="gporders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="dataTables_paginate paging_simple_numbers">
                                        <ul class="pagination">
                                            <li class="paginate_button previous disabled"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                            <li class="paginate_button next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="giftpro" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #00000000">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">Gift Products</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hbgpcode" />
                                     <div class="row">
                                         <div class="col-sm-3">
                                            <label>Product Type<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-4">
                                            <input type="radio" name="pt" id="sale" value="0" checked="true">Sale Product<br>   
                                        </div>
                                         <div class="col-sm-4">
                                            <input type="radio" name="pt" id="gft" value="1">Gift Product<br> 
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Product Name<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8" id="hid">
                                            <select id="ddlprod" class="form-control">
                                            </select>
                                        </div>
                                           <div class="col-sm-8" id="hid1">
                                            <input type="text" id="txtprod" class="form-control" autocomplete="off" />
                                        </div>
                                    </div>
                                     <div class="row" style="margin-top: 10px;">
                                          <div class="col-sm-3">
                                            <label>Product Price<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-8">
                                            <input type="number" id="prodpice" min="1" onkeyup="ValidatePrice(this.value)" class="form-control" />
                                        </div>
                                         </div>
                                     <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label>Duration &nbsp;&nbsp;&nbsp;From</label>
                                        </div>
                                        <div class="col-sm-8">
                                            <input type="date" id="gpfdt" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-3">
                                            <label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To</label>
                                        </div>
                                        <div class="col-sm-8">
                                            <input type="date" id="gptdt" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btngpsave" class="btn btn-success">Save</button>
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
             </div>
            </div>
         </form>
 <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script type="text/javascript" src="../js/plugins/timepicker/bootstrap-clockpicker.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>


    <script type="text/javascript">
        var pgNo = 1; PgRecords = 10; TotalPg = 0;
        var filtrkey1 = '0', filtrkey2 = '0', filtrkey3 ='0';
        var Orders = [], AllOrders = [], Orders2 = [], AllOrders2 = [], Orders3 = [], AllOrders3 = [];
        var searchKeys1 = "GiftName,GiftDesc,Claim_Type,GiftType,"; searchKeys2 = "GiftName,GiftDesc,bill_basis,Claim_Type,GiftType,"; searchKeys3 = "prod_type,PName,";
        
        

        //Slase Slab Creation---------------------------------------------------------------------------------------------
        $(".data-table-basic_length1").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable1();
            }
        );
        $("#tSearchOrd1").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys1).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = AllOrders;
            ReloadTable1();
        });
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey1 = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd1").val('');
            ReloadTable1();
        });
        $(document).on('click', ".ddlStatus>li>a", function () {
        //$(".ddlStatus>li>a").on("click", function () {
            cStus = $(this).closest("td").find(".aState1");
            let slbid = $(this).closest("tr").find(".sfedit").attr("id");
            stus = $(this).attr("v1");
            $indx = Orders.findIndex(x => x.GiftSlabID == slbid);
            cStusNm = $(this).text();
            if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                id = Orders[$indx].GiftSlabID;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_Claim_Master.aspx/SetNewStatus",
                    data: "{'ID':'" + id + "','stus':'" + stus + "'}",
                    dataType: "json",
                    success: function (data) {
                        Orders[$indx].Active_Flag = stus;
                        Orders[$indx].Status = cStusNm;
                        $(cStus).html(cStusNm);

                        ReloadTable1();
                        alert('Status Changed Successfully...');

                    },
                    error: function (result) {
                    }
                });
            }
            loadPgNos1();
        });
        function loadPgNos1() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
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
                pgNo = parseInt($(this).attr("data-dt-idx"));
                ReloadTable1();
            }
            );
        }
        function ReloadTable1() {
            $("#retailbslab TBODY").html("");
            if (filtrkey1 != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Active_Flag == filtrkey1;
                })
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr slbid='" + Orders[$i].GiftSlabID + "' slbnm='" + Orders[$i].GiftName + "'></tr>");
                    slno = $i + 1;
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].GiftName + '</td><td>' + Orders[$i].GiftDesc + '</td><td>' + Orders[$i].GiftMinVal +
                        '</td><td>' + Orders[$i].GiftMaxVal + '</td><td>' + Orders[$i].Claim_Type + '</td><td>' + Orders[$i].Claim_Val +
                        '</td><td>' + Orders[$i].GiftType + '</td><td class="hqcount"><a href="#">' + Orders[$i].Sf_HQ1 + '</a></td><td class="retcount"><a href="#">' + Orders[$i].Mapped_Retails1 +
                        '</a></td><td>' + Orders[$i].Duration + '</td><td>' + Orders[$i].Claim_deadline + '</td><td id=' + Orders[$i].GiftSlabID +
                        ' class="sfedit"><input type="hidden"  value="0"><a href="#">Edit</a></td>' +
                        '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span class="aState1" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + ((Orders[$i].Status == "Active") ? '<li><a href="#" v1="1">Deactivate</a></li>' : '<li><a href="#" v1="0">Active</a></li>') + '</ul></li></ul></td>');

                    $("#retailbslab TBODY").append(tr);
                }
            }
            $("#retailbslab_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos1();
        }

        function loadData1() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/getSalesSlab",
                data: "{'clmtyp':'S'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    ReloadTable1();
					$("#tSearchOrd1").val('');
                },
                error: function (result) {
                }
            });
        }
        $(document).on('click', '.sfedit', function () {
            x = this.id;
            $("#Slab").modal("show");
            fillSlabPop(x);
        });
        function fillSlabPop(scode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/getGiftUpdt",
                data: "{'scode':'" + scode + "'}",
                dataType: "json",
                success: function (data) {
                    var gbdts = JSON.parse(data.d) || [];
                    $('#hbcode').val(gbdts[0].GiftSlabID);
                    $('#txtbname').val(gbdts[0].GiftName);
                    $('#txtbdesc').val(gbdts[0].GiftDesc1);
                    $('#bminvalue').val(gbdts[0].GiftMinVal);
                    $('#bmaxvalue').val(gbdts[0].GiftMaxVal);
                    var rfd = (gbdts[0].From_Date).split('T');
                    var tfd = (gbdts[0].To_Date).split('T');
                    $('#fdt').val(rfd[0]);
                    $('#tdt').val(tfd[0]);
                    var hq1 = gbdts[0].Sf_HQ;
                    hq1 = hq1.split(',');
                    $('#ddlhq  > option').each(function () {
                        for (var i = 0; i < hq1.length; i++) {
                            if (hq1[i] == $(this).val()) { $(this).prop('selected', true); }
                        }
                    }); $('#ddlhq').multiselect('reload');
                    var ret1 = gbdts[0].Mapped_Retails;
                    ret1 = ret1.split(',');
                    $('#ddlret  > option').each(function () {
                        for (var i = 0; i < ret1.length; i++) {
                            if (ret1[i] == $(this).val()) { $(this).prop('selected', true); }
                        }
                    }); $('#ddlret').multiselect('reload');
                    $('.ms-options ul').css('column-count', '2');
                    if (gbdts[0].GiftType == '0') {
                        $("input[type='radio'][name='cf'][value='0']").prop("checked", true);
                        $("#clgip").hide();
                    }
                    else {
                        $("input[type='radio'][name='cf'][value='1']").prop("checked", true);
                        $("#clgip").show();
                        var gp = gbdts[0].Claim_Product;
                        gp = gp.split(',');
                        $('#ddlgift  > option').each(function () {
                            for (var i = 0; i < gp.length; i++) {
                                if (gp[i] == $(this).val()) { $(this).prop('selected', true); }
                            }
                        }); $('#ddlgift').multiselect('reload');
                    }
                    if (gbdts[0].Claim_Type == '0') {
                        $("input[type='radio'][name='cc'][value='0']").prop("checked", true);
                        $("#val").hide();
                    }
                    else {
                        $("input[type='radio'][name='cc'][value='1']").prop("checked", true);
                        $("#val").show();
                    }
                    $('#claimvalue').val(gbdts[0].Claim_Val);
                    var gpdf = (gbdts[0].From_Date).split('T');
                    var gpdt = (gbdts[0].To_Date).split('T');
                    $('#fdt').val(gpdf[0]);
                    $('#tdt').val(gpdt[0]);
                    var gpend = (gbdts[0].Claim_deadline).split('T');
                    $('#ced').val(gpend[0]);
                },
                error: function (data) {
                    //alert(JSON.stringify(data));
                }
            });
        }
        $('#rbsbtn').on('click', function () {
            clear();
        });
        function clear() {
            $('#hbcode').val('');
            $('#txtbname').val('');
            $('#txtbdesc').val('');
            $('#bminvalue').val('');
            $('#bmaxvalue').val('');
            $('#fdt').val('');
            $('#tdt').val('');
            $('#ced').val('');
            $('input[name="cc"][value="0"]').prop('checked', true);
            $('#claimvalue').val('');
            $('input[name="cf"][value="0"]').prop('checked', true);
            //$('#ddlgift').val([]);
            $('#ddlgift  > option:selected').each(function () {
                $(this).prop('selected', false);
            });
            $('#ddlgift').multiselect('reload');
            //$('#ddlhq').val([]);
            $('#ddlhq  > option:selected').each(function () {
                $(this).prop('selected', false);
            });
            $('#ddlhq').multiselect('reload');
            $('#ddlret  > option:selected').each(function () {
                $(this).prop('selected', false);
            });
            $('#ddlret').multiselect('reload');

            pgNo = 1; PgRecords = 10; TotalPg = 0;
            ReloadTable1();
        }
        $('#btnsave').on('click', function () {
            var slbid = $('#hbcode').val();
            var nam = $('#txtbname').val();
            if (nam == "") {
                alert("Please Enter Name");
                $('#txtbname').focus();
                return false;
            }
			 var validatename = [];
$.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    async: false,
    url: "New_Claim_Master.aspx/getSalesSlab",
    data: "{'clmtyp':'S'}",
    dataType: "json",
    success: function (data) {
        validatename = JSON.parse(data.d) || [];
        validatename = validatename.filter(function (a) {
            return a.GiftName.toLowerCase() == nam.toLowerCase();
        });
    },
    error: function (result) {
    }
});
if (validatename.length > 0&& slbid=='') {
    alert("Entered Slabname '" + nam + "' is Already Exist.");
    return false;
}
            var clmy = $('#txtbdesc').val();
            if (clmy == "") {
                alert("Please Select Month and Year");
                $('#txtbdesc').focus();
                return false;
            }
            var minval = $('#bminvalue').val();
            if (minval == "") {
                alert("Please Enter Minimum Value.");
                $('#bminvalue').focus();
                return false;
            }
            var maxval = $('#bmaxvalue').val();
            if (maxval == "") {
                alert("Please Enter Maximum Value.");
                $('#bmaxvalue').focus();
                return false;
            }
            if (minval != "" && maxval != "") {
                if (parseInt(minval) > parseInt(maxval)) {
                    alert("Your Minimum value is Greater...");
                    $('#bmaxvalue').focus();
                    return false;
                }
            }
            var clmtyp = $("input[type='radio'][name='cc']:checked").val();
            var clmval = $('#claimvalue').val();
			if (clmval == "" && clmtyp == '0') {
    alert("Enter Fixed Amount...");
    $('#claimvalue').focus();
    return false;
}
if (clmval == "" && clmtyp == '1') {
    alert("Enter Percentage of ClaimCalculation...");
    $('#claimvalue').focus();
    return false;
}
            var gifttyp = $("input[type='radio'][name='cf']:checked").val();
            var clmprod = '';
            $('#ddlgift  > option:selected').each(function () {
                clmprod += $(this).val() + ',';
            });
			if (clmprod == '' && gifttyp == '1') {
    alert("Mandatory select GiftProducts.");
    $('#ddlgift').focus();
    return false;
}
            var hq = "";
            $('#ddlhq  > option:selected').each(function () {
                hq += $(this).val() + ',';
            });
            var ret = '';
            $('#ddlret  > option:selected').each(function () {
                ret += $(this).val() + ',';
            });
			if (ret == '') {
    alert("Mandatory select Retailers.");
    $('#ddlret').focus();
    return false;
}
            var gfdt = $('#fdt').val();
			if (gfdt == '') {
    alert("Mandatory select Duration From date");
    $('#fdt').focus();
    return false;
}
            var gtdt = $('#tdt').val();
			if (gtdt == '') {
    alert("select Duration To date");
    $('#tdt').focus();
    return false;
}
            var clmenddt = $('#ced').val();
			if (clmenddt == '') {
    alert("End Date is Mandatory");
    $('#ced').focus();
    return false;
}

            data = { "slbid": slbid, "slabnm": nam, "clmdesc": clmy, "mnval": minval, "mxval": maxval, "clmtyp": clmtyp, "clmval": clmval, "Gtype": gifttyp, "FDT": gfdt, "TDT": gtdt, "hqdtl": hq, "retail": ret, "Gprod": clmprod, "ClmEndDt": clmenddt }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/Save_SalesSlab",
                data: "{'data':'" + JSON.stringify(data) + "'}",
                dataType: "json",
                success: function (data) {
                    alert(data.d);
                    $("#Slab").modal("hide");
                    loadData1();
                    //clear();
                },
                error: function (result) {
                }
            });

        });

        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //Product Billed Creation

        $(".data-table-basic_length2").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable2();
            });
        $("#tSearchOrd2").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders2 = AllOrders2.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys2).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders2 = AllOrders2;
            ReloadTable2();
        });
        $(".segment2>li").on("click", function () {
            $(".segment2>li").removeClass('active');
            $(this).addClass('active');
            filtrkey2 = $(this).attr('data-va');
            Orders2 = AllOrders2;
            $("#tSearchOrd2").val('');
            ReloadTable2();
        });
        function loadPgNos2() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders2.length / PgRecords) + ((Orders2.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
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
                pgNo = parseInt($(this).attr("data-dt-idx"));
                ReloadTable2();
            }
            );
        }
        $(document).on('click', ".ddlStatus1>li>a", function () {
            //$(".ddlStatus>li>a").on("click", function () {
            cStus = $(this).closest("td").find(".aState2");
            let slbid = $(this).closest("tr").find(".sfedit2").attr("id");
            stus = $(this).attr("v2");
            $indx = Orders2.findIndex(x => x.GiftSlabID == slbid);
            cStusNm = $(this).text();
            if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                id = Orders2[$indx].GiftSlabID;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_Claim_Master.aspx/SetNewStatus",
                    data: "{'ID':'" + id + "','stus':'" + stus + "'}",
                    dataType: "json",
                    success: function (data) {
                        Orders2[$indx].Active_Flag = stus;
                        Orders2[$indx].Status = cStusNm;
                        $(cStus).html(cStusNm);

                        ReloadTable2();
                        alert('Status Changed Successfully...');

                    },
                    error: function (result) {
                    }
                });
            }
            loadPgNos2();
        });
        function ReloadTable2() {
            $("#billslab TBODY").html("");
            if (filtrkey2 != "All") {
                Orders2 = Orders2.filter(function (a) {
                    return a.Active_Flag == filtrkey2;
                })
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders2.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    $(tr).html('<td>' + slno + '</td><td>' + Orders2[$i].GiftName + '</td><td>' + Orders2[$i].GiftDesc + '</td><td>' + Orders2[$i].billed_products1 +
                        '</td><td>' + Orders2[$i].bill_basis + '</td><td>' + Orders2[$i].Claim_Type + '</td><td>' + Orders2[$i].Claim_Val + '</td><td>' + Orders2[$i].GiftType +
                        '</td><td>' + Orders2[$i].Sf_HQ1 + '</td><td>' + Orders2[$i].Mapped_Retails1 + '</td><td>' + Orders2[$i].Duration + '</td><td>' + Orders2[$i].Claim_deadline + '</td><td id=' + Orders2[$i].GiftSlabID +
                        ' class="sfedit2"><a href="#">Edit</a></td>' +
                        '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span class="aState2" data-val="' + Orders2[$i].Status + '">' + Orders2[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus1" style="right:0;left:auto;">' + ((Orders2[$i].Status == "Active") ? '<li><a href="#" v2="1">Deactivate</a></li>' : '<li><a href="#" v2="0">Active</a></li>') + '</ul></li></ul></td>');

                    $("#billslab TBODY").append(tr);
                }
            }
            $("#billslab_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders2.length) ? (st + PgRecords) : Orders2.length) + " of " + Orders2.length + " entries")
            loadPgNos2();
        }
        function loadData2() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/getBilledClm",
                data: "{'clmtyp':'P'}",
                dataType: "json",
                success: function (data) {
                    AllOrders2 = JSON.parse(data.d) || [];
                    Orders2 = AllOrders2;
                    ReloadTable2();
					$("#tSearchOrd2").val('');
                },
                error: function (result) {
                }
            });
        }
        $(document).on('click', '.sfedit2', function () {
            x = this.id;
            $("#Billed").modal("show");
            fillBillPop(x);
        });
        function fillBillPop(bcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/getBillUpdt",
                data: "{'bcode':'" + bcode + "'}",
                dataType: "json",
                success: function (data) {
                    var gbdts = JSON.parse(data.d) || [];
                    $('#sbcode').val(gbdts[0].GiftSlabID);
                    $('#txtbname1').val(gbdts[0].GiftName);
                    $('#txtbdesc1').val(gbdts[0].GiftDesc1);
                    var rfd = (gbdts[0].From_Date).split('T');
                    var tfd = (gbdts[0].To_Date).split('T');
                    $('#fdtt').val(rfd[0]);
                    $('#tdtt').val(tfd[0]);
                    $('#ddlprodcatgry').selectpicker('val', gbdts[0].billed_products);
                    var splitsprd = gbdts[0].billed_products.split(',') || [];
                    if (splitsprd.length > 0) {
                        for (var i = 0; i < splitsprd.length; i++)
                            $('#ddlprodcatgry option[value=' + splitsprd[i] + ']').attr('selected', true);
                    }
                    $('#ddlprodcatgry').selectpicker('refresh');
                    var hq1 = gbdts[0].Sf_HQ;
                    hq1 = hq1.split(',');
                    $('#ddlhq1  > option').each(function () {
                        for (var i = 0; i < hq1.length; i++) {
                            if (hq1[i] == $(this).val()) { $(this).prop('selected', true); }
                        }
                    }); $('#ddlhq1').multiselect('reload');
                    var ret1 = gbdts[0].Mapped_Retails;
                    ret1 = ret1.split(',');
                    $('#ddlret1  > option').each(function () {
                        for (var i = 0; i < ret1.length; i++) {
                            if (ret1[i] == $(this).val()) { $(this).prop('selected', true); }
                        }
                    }); $('#ddlret1').multiselect('reload');
                    $('.ms-options ul').css('column-count', '2');
                    if (gbdts[0].bill_basis == '0') {
                        $("input[type='radio'][name='bb'][value='0']").prop("checked", true);
                        $('#salevalue').val(gbdts[0].sl_amt_qty);
                        $('.hqty').hide();
                    }
                    else {
                        $("input[type='radio'][name='bb'][value='1']").prop("checked", true);
                        $('#saleqty').val(gbdts[0].sl_amt_qty);
                        $('.hval').hide();
                    }
                    if (gbdts[0].GiftType == '0') {
                        $("input[type='radio'][name='cf1'][value='0']").prop("checked", true);
                        $("#clgip1").hide();
                    }
                    else {
                        $("input[type='radio'][name='cf1'][value='1']").prop("checked", true);
                        $("#clgip1").show();
                        var gp = gbdts[0].Claim_Product;
                        gp = gp.split(',');
                        $('#ddlgift1  > option').each(function () {
                            for (var i = 0; i < gp.length; i++) {
                                if (gp[i] == $(this).val()) { $(this).prop('selected', true); }
                            }
                        }); $('#ddlgift1').multiselect('reload');
                    }
                    if (gbdts[0].invoice_ned == '0') {
                        $("input[type='radio'][name='isun'][value='0']").prop("checked", true);
                    }
                    else {
                        $("input[type='radio'][name='isun'][value='1']").prop("checked", true);
                    }
                    if (gbdts[0].Claim_Type == '0') {
                        $("input[type='radio'][name='cc1'][value='0']").prop("checked", true);
                    }
                    else {
                        $("input[type='radio'][name='cc1'][value='1']").prop("checked", true);
                    }
                    $('#claimvalue1').val(gbdts[0].Claim_Val);
                    //var gpdf = (gbdts[0].From_Date).split('T');
                    //var gpdt = (gbdts[0].To_Date).split('T');
                    //$('#fdt').val(gpdf[0]);
                    //$('#tdt').val(gpdt[0]);
                    var gpend = (gbdts[0].Claim_deadline).split('T');
                    $('#ced1').val(gpend[0]);
                },
                error: function (data) {
                    //alert(JSON.stringify(data));
                }
            });
        }
        $('#billprod').on('click', function () {
            clear2();
        });
        function clear2() {
            $('#sbcode').val('');
            $('#txtbname1').val('');
            $('#txtbdesc1').val('');
            $('#fdtt').val('');
            $('#tdtt').val('');
            $('#ced1').val('');
            $('#ddlprodcatgry').selectpicker('val', '');
            $("input[type='radio'][name='isun'][value='0']").prop("checked", true);
            $('#claimvalue1').val('');
            $("input[type='radio'][name='cc1'][value='0']").prop("checked", true);
            $("input[type='radio'][name='cf1'][value='0']").prop("checked", true);
            $("input[type='radio'][name='bb'][value='0']").prop("checked", true);
            $('#salevalue').val('');
            $('.hqty').hide();
            $('#clgip1').hide();
            $('#ddlgift1  > option:selected').each(function () {
                $(this).prop('selected', false);
            });
            $('#ddlgift1').multiselect('reload');
            //$('#ddlhq').val([]);
            $('#ddlhq1  > option:selected').each(function () {
                $(this).prop('selected', false);
            });
            $('#ddlhq1').multiselect('reload');
            $('#ddlret1  > option:selected').each(function () {
                $(this).prop('selected', false);
            });
            $('#ddlret1').multiselect('reload');

            pgNo = 1; PgRecords = 10; TotalPg = 0;
            ReloadTable2();
        }
        $('#btnsave1').on('click', function () {
            var billid = $('#sbcode').val();
            var nam = $('#txtbname1').val();
            if (nam == "") {
                alert("Please Enter Name");
                $('#txtbname1').focus();
                return false;
            }
			var validatename = [];
$.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    async: false,
    url: "New_Claim_Master.aspx/getBilledClm",
    data: "{'clmtyp':'P'}",
    dataType: "json",
    success: function (data) {
        validatename = JSON.parse(data.d) || [];
        validatename = validatename.filter(function (a) {
            return a.GiftName.toLowerCase() == nam.toLowerCase();
        });
    },
    error: function (result) {
    }
});
if (validatename.length > 0 && billid == '') {
    alert("Entered Name '" + nam + "' is Already Exist.");
    return false;
}
            var clmy = $('#txtbdesc1').val();
            if (clmy == "") {
                alert("Please Select Month and Year");
                $('#txtbdesc1').focus();
                return false;
            }
            var prodcd = $('#ddlprodcatgry').val() || '';
            if (prodcd == '') {
                alert("select Products");
                $('#ddlprodcatgry').focus();
                return false;
            }
            var pcode = '', pname='';
            for (var y = 0; y < prodcd.length; y++) {
                pcode += prodcd[y] + ',';
                pname += $("#ddlprodcatgry option[value='" + prodcd[y] + "']").text() + ',';
            }
            var invnd = $("input[type='radio'][name='isun']:checked").val();
            var biltyp = $("input[type='radio'][name='bb']:checked").val();
            var billval = '';
            if (biltyp == '0') {
                billval = $('#salevalue').val();
            }
            if (biltyp == '1') {
                billval = $('#saleqty').val();
            }
            if (billval == '' && biltyp == '0') {
                alert("Please Enter SaleValue");
				$('#salevalue').focus();
                return false;
            }
            if (billval == '' && biltyp == '1') {
                alert("Please Enter SaleQuantity");
				$('#saleqty').focus();
                return false;
            }
            var clmcal = $("input[type='radio'][name='cc1']:checked").val();
            var clmval = $('#claimvalue1').val();
			if (clmval == '' && clmcal =='1') {
                alert("Enter Calculation percentage.");
				$('#claimvalue1').focus();
                return false;
            }
            if (clmval == '' && clmcal == '0') {
                alert("Enter Fixed Amount");
				$('#claimvalue1').focus();
                return false;
            }
            var clmfrm = $("input[type='radio'][name='cf1']:checked").val();
            var clmprod = '';
            $('#ddlgift1  > option:selected').each(function () {
                clmprod += $(this).val() + ',';
            });
			if (clmfrm == '1' && clmprod == '') {
                alert("Mandatory!. select ClaimProducts.");
	            $('#ddlgift1').focus();
                return false;
            }
            var hq = "";
            $('#ddlhq1  > option:selected').each(function () {
                hq += $(this).val() + ',';
            });
            var ret = '';
            $('#ddlret1  > option:selected').each(function () {
                ret += $(this).val() + ',';
            });
            if (ret == "") {
                alert("Please Select Retailers...");
				$('#ddlret1').focus();
                return false;
            }
            var bfdt = $('#fdtt').val();
            if (bfdt == '') {
                alert("Please Select Duration From...");
				$('#fdtt').focus();
                return false;
            }
            var btdt = $('#tdtt').val();
            if (btdt == '') {
                alert("Please Select Duration To...");
				$('#tdtt').focus();
                return false;
            }
            var clmenddt = $('#ced1').val();
            if (clmenddt == '') {
                alert("Please Select ClaimEnd Date..");
				$('#ced1').focus();
                return false;
            }

            data = {
                "billid": billid, "billnm": nam, "clmdesc": clmy, "invnd": invnd, "billval": billval, "clmtyp": clmcal, "biltyp": biltyp, "clmval": clmval, "Gtype": clmfrm, "FDT": bfdt, "TDT": btdt, "hqdtl": hq, "retail": ret, "Gprod": clmprod, "ClmEndDt": clmenddt, "pcode": pcode }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/Save_BilledProd",
                data: "{'data':'" + JSON.stringify(data) + "'}",
                dataType: "json",
                success: function (data) {
                    alert(data.d);
                    $("#Billed").modal("hide");
                    loadData2();
                    //clear2();
                },
                error: function (result) {
                }
            });

        });


        //----------------------------------------------------------------------------------------------------------------------------------------------

        //GiftProductCreation


        $(".data-table-basic_length3").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable3();
            }
        );
        $("#tSearchOrd3").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders3 = AllOrders3.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys3).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders3 = AllOrders3;
            ReloadTable3();
        });
        $(".segment3>li").on("click", function () {
            $(".segment3>li").removeClass('active');
            $(this).addClass('active');
            filtrkey3 = $(this).attr('data-va');
            Orders3 = AllOrders3;
            $("#tSearchOrd3").val('');
            ReloadTable3();
        });
        function loadPgNos3() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders3.length / PgRecords) + ((Orders3.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
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
                pgNo = parseInt($(this).attr("data-dt-idx"));
                ReloadTable3();
            }
            );
        }
        $(document).on('click', ".ddlStatus2>li>a", function () {
            //$(".ddlStatus>li>a").on("click", function () {
            cStus = $(this).closest("td").find(".aState3");
            let gid = $(this).closest("tr").find(".sfedit3").attr("id");
            stus = $(this).attr("v3");
            $indx = Orders3.findIndex(x => x.ID == gid);
            cStusNm = $(this).text();
            if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                id = Orders3[$indx].ID;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_Claim_Master.aspx/NewStatus",
                    data: "{'ID':'" + id + "','stus':'" + stus + "'}",
                    dataType: "json",
                    success: function (data) {
                        Orders3[$indx].Active_Flag = stus;
                        Orders3[$indx].Status = cStusNm;
                        $(cStus).html(cStusNm);

                        ReloadTable3();
                        alert('Status Changed Successfully...');

                    },
                    error: function (result) {
                    }
                });
            }
            loadPgNos3();
        });
        function ReloadTable3() {
            $("#giftpList TBODY").html("");
            if (filtrkey3 != "All") {
                Orders3 = Orders3.filter(function (a) {
                    return a.Active_Flag == filtrkey3;
                });
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders3.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1; 
                    $(tr).html('<td>' + slno + '</td><td>' + Orders3[$i].prod_type1 + '</td><td>' + Orders3[$i].PName + '</td><td>' + Orders3[$i].Product_Price + '</td><td id=' + Orders3[$i].ID +
                        ' class="sfedit3"><a href="#">Edit</a></td>' +
                        '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span class="aState3" data-val="' + Orders3[$i].Status + '">' + Orders3[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus2" style="right:0;left:auto;">' + ((Orders3[$i].Status == "Active") ? '<li><a href="#" v3="1">Deactivate</a></li>' : '<li><a href="#" v3="0">Active</a></li>') + '</ul></li></ul></td>');
                    $("#giftpList TBODY").append(tr);
                }
            }
            $("#gporders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders3.length) ? (st + PgRecords) : Orders3.length) + " of " + Orders3.length + " entries");
            loadPgNos3();
        }
        function loadData3() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/getAllGiftProducts",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    AllOrders3 = JSON.parse(data.d) || [];
                    Orders3 = AllOrders3;
                    ReloadTable3();
                },
                error: function (result) {
                }
            });
        }
        function clear1() {
		$('#hbgpcode').val('');
            $('input[name="pt"][value="0"]').prop('checked', true);
            loadproduct('<%=Session["div_code"]%>');
			$("#hid").show();
            $("#hid1").hide();
			$("#txtprod").val('');
            $('#prodpice').val('');
            $('#gpfdt').val('');
            $('#gptdt').val('');
        }
        $('#giftpbtn').on('click', function () {
            clear1();
        });
        function loadproduct(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/GetProductMaster",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    allprod = JSON.parse(data.d) || [];
                    $('#ddlprod').selectpicker('destroy');
                    var pdm = $('#ddlprod');
                    var prnm = $('#ddlprodcatgry');
                    pdm.empty().append($('<option value="0">--Select Product--</option>'));
                    //prnm.empty().append($('<optgroup value="' + allprod[0].Product_Cat_Code + '">' + allprod[0].Product_Cat_Name));
                    for (var i = 0; i < allprod.length; i++) {
                        pdm.append($('<option value="' + allprod[i].Product_Detail_Code + '">' + allprod[i].Product_Detail_Name + '</option>'));
                        //if (allprod[i].row_nm == "1") {
                        //    prnm.append($('<optgroup value="' + allprod[i].Product_Cat_Code + '" label="' + allprod[i].Product_Cat_Name +'" class="checkbox-group"></optgroup>'));
                        //}
                        prnm.append($('<option value="' + allprod[i].Product_Detail_Code + '">' + allprod[i].Product_Detail_Name + '</option>'));
                    }
                    //prnm.append($('</optgroup>'));
                },
                error: function (result) {
                }
            });
            $('#ddlprod').selectpicker({
                liveSearch: true
            });
            $('#ddlprodcatgry').selectpicker({
                liveSearch: true
            });
            //$('#ddlprodcatgry').multiselect({
            //    enableFiltering: false,
            //    enableClickableOptGroups: true,
            //    includeSelectAllOption: true,
            //});
        }
        $(document).on('click', '.sfedit3', function () {
            x = this.id;
            $("#giftpro").modal("show");
            fillGiftProdPop(x);
        });
        function fillGiftProdPop(gpcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/getGiftProdUpdt",
                data: "{'scode':'" + gpcode + "'}",
                dataType: "json",
                success: function (data) {
                    var gbdts = JSON.parse(data.d) || [];
                    $('#hbgpcode').val(gbdts[0].sl_no);
                    if (gbdts[0].prod_type == "0") {
                        $("input[type='radio'][name='pt'][value='0']").prop("checked", true);
                        $("#hid").show();
                        $("#hid1").hide();
                        $('#ddlprod').selectpicker('destroy');
                        $("#ddlprod").val(gbdts[0].Product_Code);
                        $('#ddlprod').selectpicker({
                            liveSearch: true
                        });
                    }
                    else {
                        $("input[type='radio'][name='pt'][value='1']").prop("checked", true);
                        $("#hid").hide();
                        $("#hid1").show();
                        $('#txtprod').val(gbdts[0].Product_Name);
                    }
                    $('#prodpice').val(gbdts[0].Product_Price);
                    var rfd = (gbdts[0].From_dt).split('T');
                    var tfd = (gbdts[0].To_dt).split('T');
                    $('#gpfdt').val(rfd[0]);
                    $('#gptdt').val(tfd[0]);
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }
        $('#btngpsave').on('click', function () {
            var gpscode = $('#hbgpcode').val();
            var prodtyp = $("input[type='radio'][name='pt']:checked").val();
            var productnm = "";
            if (prodtyp == "0") {
                productnm = $("#ddlprod option:selected").val();
            }
            if (prodtyp == "1") {
                productnm = $('#txtprod').val();
            }
			if((productnm=="" || productnm=="0") && prodtyp == "0")
			{
			alert("Select Product");
			$("#ddlprod").focus();
			return false;
			}
			if(productnm=="" && prodtyp == "1")
			{
			alert("Enter ProductName");
			$('#txtprod').focus();
			return false;
			}
			var validatename = [];
$.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    async: false,
    url: "New_Claim_Master.aspx/getAllGiftProducts",
    data: "{}",
    dataType: "json",
    success: function (data) {
        validatename = JSON.parse(data.d) || [];
        validatename = validatename.filter(function (a) {
            return (a.PName.toLowerCase() == productnm.toLowerCase() && a.prod_type == prodtyp && a.Active_Flag=='0');
        });
    },
    error: function (result) {
    }
});
if (validatename.length > 0 && gpscode == '') {
    alert("ProductName '" + productnm + "' is Already Exist for same Type.");
    return false;
}
            var prodprice = $('#prodpice').val();
			if(prodprice=='')
			{
			alert("Enter Product Price");
			$('#prodpice').focus();
			return false;
			}
            var frmdt = $('#gpfdt').val();
			if(frmdt=='')
			{
			alert("select Duration Fromdate");
			$('#gpfdt').focus();
			return false;
			}
            var todt = $('#gptdt').val();
			if(todt=='')
			{
			alert("select Duration Todate");
			$('#gptdt').focus();
			return false;
			}

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/Save_Gift",
                data: "{'prodnm':'" + productnm + "','prodprice':'" + prodprice + "','fdt':'" + frmdt + "','tdt':'" + todt + "','gpscode':'" + gpscode + "','prodtyp':'" + prodtyp + "'}",
                dataType: "json",
                success: function (data) {
                    alert(data.d);
                    $("#giftpro").modal("hide");
                    loadData3();
                },
                error: function (result) {
                }
            });

        });
        
      //----------------------------------------------------------------------------------------------------------------------------------  
        

        $(document).ready(function () {
            var divcode = Number(<%=Session["div_code"]%>);
            $("#hid").show();
            $("#hid1").hide();
            loadData1();
            loadData2();
            loadData3();
            loadproduct(divcode);
            loadretailers(divcode);
            loadHQ(divcode);
            //loadretailerCategory(divcode);
            loadclaimprod(divcode);
            fillGiftProductsID(divcode);

            $(document).on('click', '.hqcount', function () {
                $('#HQModal').modal('toggle');
                var slbid = $(this).closest('tr').attr('slbid');
                var slbnm = $(this).closest('tr').attr('slbnm');
                $('#HQModalLabel').text("HQ Mapped for " + slbnm);
                fillHQ(slbid);
            });

            $(document).on('click', '.retcount', function () {
                $('#RModal').modal('toggle');
                var slbid = $(this).closest('tr').attr('slbid');
                var slbnm = $(this).closest('tr').attr('slbnm');
                $('#RModalLabel').text("Retailer Mapped for " + slbnm);
                fillRetailers(slbid);
            });

            $('input[type=radio][name=cc]').change(function () {
                var rad = $('input[name="cc"]:checked').val();
                if (rad == '1') {
                    $("#val").show();
                }
                else {
                    $("#val").hide();
                }
            });
            $('input[type=radio][name=cc1]').change(function () {
                var rad = $('input[name="cc1"]:checked').val();
                if (rad == '1') {
                    $("#val1").show();
                }
                else {
                    $("#val1").hide();
                }
            });
            $('input[type=radio][name=cf]').change(function () {
                var rad = $('input[name="cf"]:checked').val();
                if (rad == '1') {
                    $("#clgip").show();
                }
                else {
                    $("#clgip").hide();
                }
            });

            $('input[type=radio][name=pt]').change(function () {
                var rad = $('input[name="pt"]:checked').val();
                if (rad == '0') {
                    $("#hid").show();//ddlpro
                    $("#hid1").hide();
                }
                else {
                    $("#hid").hide();
                    $("#hid1").show();
                }
            });

            $('input[type=radio][name=cf1]').change(function () {
                var rad = $('input[name="cf1"]:checked').val();
                if (rad == '1') {
                    $("#clgip1").show();
                }
                else {
                    $("#clgip1").hide();
                }
            });
            $('input[type=radio][name=bb]').change(function () {
                var rad = $('input[name="bb"]:checked').val();
                if (rad == '1') {
                    $(".hqty").show();
                    $(".hval").hide();
                }
                else {
                    $(".hqty").hide();
                    $(".hval").show();
                }
            });

            $('#ddlhq1').on('change', function () {
                var hq = "";
                $('#ddlhq1  > option:selected').each(function () {
                    hq += $(this).val() + ',';
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_Claim_Master.aspx/GetFilteredRet",
                    data: "{'hq':'" + hq + "'}",
                    dataType: "json",
                    success: function (data) {
                        alret = JSON.parse(data.d) || [];

                    },
                    error: function (result) {
                    }
                });

                var retmast1 = $('#ddlret1').empty();
                for (var i = 0; i < alret.length; i++) {
                    retmast1.append($('<option value="' + alret[i].listeddrcode + '">' + alret[i].ListedDr_Name + '</option>'));
                }
                $('#ddlret1').multiselect({
                    columns: 2,
                    placeholder: 'Select Retailer',
                    search: true,
                    searchOptions: {
                        'default': 'Search Retailer'
                    },
                    selectAll: true
                }).multiselect('reload');
                $('.ms-options ul').css('column-count', '2');
            });
            $('#ddlhq').on('change', function () {
                var hq = "";
                $('#ddlhq  > option:selected').each(function () {
                    hq += $(this).val() + ',';
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_Claim_Master.aspx/GetFilteredRet",
                    data: "{'hq':'" + hq + "'}",
                    dataType: "json",
                    success: function (data) {
                        alret = JSON.parse(data.d) || [];

                    },
                    error: function (result) {
                    }
                });
                var retmast = $('#ddlret').empty();
                for (var i = 0; i < alret.length; i++) {
                    retmast.append($('<option value="' + alret[i].listeddrcode + '">' + alret[i].ListedDr_Name + '</option>'));
                }
                $('#ddlret').multiselect({
                    columns: 2,
                    placeholder: 'Select Retailer',
                    search: true,
                    searchOptions: {
                        'default': 'Search Retailer'
                    },
                    selectAll: true
                }).multiselect('reload');
                $('.ms-options ul').css('column-count', '2');
            });
        });
        function fillRetailers(slbid) {
            $('#rdets tbody').html('');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/Retail_Count",
                data: "{'sdcode':'" + slbid + "'}",
                dataType: "json",
                success: function (data) {
                    var AllOrders2 = JSON.parse(data.d) || [];
                    $('#rdets TBODY').html("");
                    var slno = 0;
                    for ($i = 0; $i < AllOrders2.length; $i++) {
                        if (AllOrders2.length > 0) {
                            slno += 1;
                            tr = $("<tr></tr>");
                            $(tr).html("<td>" + slno + "</td><td>" + AllOrders2[$i].ListedDrCode + "</td><td>" + AllOrders2[$i].ListedDr_Name + "</td>");
                            $("#rdets TBODY").append(tr);
                        }
                    }
                }
            });
        }
        function fillHQ(slbid) {
            $('#hqdets tbody').html('');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/Hq_Count",
                data: "{'sdcode':'" + slbid + "'}",
                dataType: "json",
                success: function (data) {
                    var AllOrders2 = JSON.parse(data.d) || [];
                    $('#hqdets TBODY').html("");
                    var slno = 0;
                    for ($i = 0; $i < AllOrders2.length; $i++) {
                        if (AllOrders2.length > 0) {
                            slno += 1;
                            tr = $("<tr></tr>");
                            $(tr).html("<td>" + slno + "</td><td>" + AllOrders2[$i].HQ_ID + "</td><td>" + AllOrders2[$i].HQ_Name + "</td>");
                            $("#hqdets TBODY").append(tr);
                        }
                    }
                }
            });
        }
        function loadclaimprod(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/GetClaimProduct",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    allcp = JSON.parse(data.d) || [];
                    var clpmast = $('#ddlgift');
                    var clpmast1 = $('#ddlgift1');
                    for (var i = 0; i < allcp.length; i++) {
                        clpmast.append($('<option value="' + allcp[i].Product_Code + '">' + allcp[i].Product_Name + '</option>'));
                        clpmast1.append($('<option value="' + allcp[i].Product_Code + '">' + allcp[i].Product_Name + '</option>'));
                    }
                    $('#ddlgift').multiselect({
                        columns: 2,
                        placeholder: 'Select GiftProducts',
                        search: true,
                        searchOptions: {
                            'default': 'Search GiftProducts'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '2');
                    $('#ddlgift1').multiselect({
                        columns: 2,
                        placeholder: 'Select GiftProducts',
                        search: true,
                        searchOptions: {
                            'default': 'Search GiftProducts'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '2');
                },
                error: function (result) {
                }
            });
        }

        function loadHQ(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/GetHQMaster",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    allhq = JSON.parse(data.d) || [];
                    var hqmast = $('#ddlhq');
                    var hqmast1 = $('#ddlhq1');
                    for (var i = 0; i < allhq.length; i++) {
                        hqmast.append($('<option value="' + allhq[i].HQ_ID + '">' + allhq[i].HQ_Name + '</option>'));
                        hqmast1.append($('<option value="' + allhq[i].HQ_ID + '">' + allhq[i].HQ_Name + '</option>'));
                    }
                    $('#ddlhq').multiselect({
                        columns: 2,
                        placeholder: 'Select HQ',
                        search: true,
                        searchOptions: {
                            'default': 'Search HQ'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '2');
                    $('#ddlhq1').multiselect({
                        columns: 2,
                        placeholder: 'Select HQ',
                        search: true,
                        searchOptions: {
                            'default': 'Search HQ'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '2');
                },
                error: function (result) {
                }
            });
        }
        function fillGiftProductsID(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/getGiftProductsID",
                data: "{'divcode':" + divcode + "}",
                dataType: "json",
                success: function (data) {
                    var dets = JSON.parse(data.d) || [];
                    $('#hbgpcode').val(dets[0].ID);
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }

        function loadretailers(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/GetRetailerMaster",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    allret = JSON.parse(data.d) || [];
                    var retmast = $('#ddlret');
                    var retmast1 = $('#ddlret1');
                    for (var i = 0; i < allret.length; i++) {
                        retmast.append($('<option value="' + allret[i].listeddrcode + '">' + allret[i].listeddr_name + '</option>'));
                        retmast1.append($('<option value="' + allret[i].listeddrcode + '">' + allret[i].listeddr_name + '</option>'));
                    }
                    $('#ddlret').multiselect({
                        columns: 2,
                        placeholder: 'Select Retailer',
                        search: true,
                        searchOptions: {
                            'default': 'Search Retailer'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '2');
                    $('#ddlret1').multiselect({
                        columns: 2,
                        placeholder: 'Select Retailer',
                        search: true,
                        searchOptions: {
                            'default': 'Search Retailer'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '2');
                },
                error: function (result) {
                }
            });
        }

        function loadretailerCategory(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Claim_Master.aspx/GetRetCategoryMaster",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    allcat = JSON.parse(data.d) || [];
                    var retcat = $('#ddlretcat');
                    var retcat1 = $('#ddlretcat1');
                    retcat1.empty().append($('<option value="0">--Select Category--</option>'));
                    retcat.empty().append($('<option value="0">--Select Category--</option>'));
                    for (var i = 0; i < allcat.length; i++) {
                        retcat.append($('<option value="' + allcat[i].Doc_Cat_Code + '">' + allcat[i].Doc_Cat_Name + '</option>'));
                        retcat1.append($('<option value="' + allcat[i].Doc_Cat_Code + '">' + allcat[i].Doc_Cat_Name + '</option>'));
                    }
                },
                error: function (result) {
                }
            });
            $('#ddlretcat1').selectpicker({
                liveSearch: true
            });
            $('#ddlretcat').selectpicker({
                liveSearch: true
            });
        }

        

        function ValidatePrice(boxval) {
            if (parseInt(boxval) <= 0)
            { boxval = ''; return false }
            if (parseInt(boxval) > 100000000)
            { alert('Please Enter 8 digits'); boxval = Math.floor(this.value / 10); }
            return boxval;

            var number = 123456789; number.toString().length - 8; var num = 1; for (var i = 0; i < number.toString().length - 8; i++) num = num * 10; console.log(num)
        }

    </script>
</asp:Content>

