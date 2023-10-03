<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="RetailerAdd.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_RetailerAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="https://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title></title>
        <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
        <style type="text/css">
            table {
                border-collapse: separate;
                border-spacing: 9px;
                width: 100%;
            }

            .is-invalid {
                outline: none;
                box-shadow: 0 0 0 3px rgba(255, 0, 0, 0.4);
            }

            .scrldiv, .scrldiv2 {
                width: 545px;
                height: 116px;
            }

                .scrldiv input {
                    margin-top: 10px;
                }

                .scrldiv2 input {
                    margin-top: 10px;
                }

            label {
                font-size: 13px;
                font-family: 'Open Sans','arial';
                font-weight: 700;
                margin: 0px;
            }

            .panel-default {
                border-color: #ddd !important;
                border: 1px solid transparent;
            }

            input {
                width: 100%;
                border: 1px solid #d8d8d8 !important;
                padding: 6px 6px 7px !important;
                border-radius: 3px !important;
                line-height: inherit !important;
                background: #fff;
                box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.1);
                transition: all 0.3s ease;
                /*border: 1px solid #00809D;*/
            }

                input:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }

            select {
                width: 83%;
                border: 1px solid #D5D5D5 !important;
                padding: 6px 6px 7px !important;
            }

                select:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }

            .chosen-container {
                width: 100% !important;
            }
        </style>
    </head>
    <body>
        <div class="row">
            <div class="col-lg-12 sub-header">
                Retailer Creation 
            <button style="float: right;" type="button" id="btnBack1" class="btn btn-primary">Back</button>
                <button style="float: right; margin-right: 20px;" type="button" id="btnsubmit" class="btn btn-primary">Submit</button>
            </div>
        </div>
        <form style="background: #ffffff; box-shadow: 0px 3px 12px rgba(0, 0, 0, 0.25); border-radius: 8px; margin-top: 5px;" runat="server">
            <asp:HiddenField ID="HiddenFieldRetCode" runat="server" />
            <div class="container" style="width: 1000px;">
                <div class="col-md-6">
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial'; margin-top: 40px;">Profile Setting</h3>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>Customer/ Retailer Code</label>
                            </td>
                            <td>

                                <input class="col-xs-10" type="text" id="Txt_id" autocomplete="off" disabled />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Customer / Retailer Name</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtName" autocomplete="off" required />
                            </td>
                        </tr>
					<%--	  <tr>
                            <td align="right">
                                <label>Group</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <input type="radio" name="Grp_type" id="Yes" value="Yes"/>
								<label for="dewey">Yes</label><br>
								<input type="radio" name="Grp_type" id="No" value="No"/>
								<label for="dewey">No</label>
                            </td>
                        </tr>--%>
						<tr>
                            <td align="right">
                                <label>Group</label>
                            </td>
                            <td>
                                <select id="ddl_group" data-dropup-auto="false" required>
                                </select>
                            </td>
                        </tr>
						
						<tr>
                            <td align="right">
                                <label>State</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="ddl_state" data-dropup-auto="false" required>
                                </select>
                            </td>
                        </tr>
						
					<tr>
                            <td align="right">
                                <label>Location</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="ddl_location" data-dropup-auto="false" required>
                                </select>
                            </td>
                        </tr>
						
                        <tr>
                            <td align="right">
                                <label>Facility</label>
                            </td>
                            <td> 
                                <input list="retsubName" name="subName" class="form-control autoc ui-autocomplete-input" id="txtsubName" style="width: 260px;" />
                                <datalist id="retsubName">
                                </datalist>
                             </td>
                        </tr>                        
                        <tr>
                            <td align="right">
                                <label>ERP Code</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtERBCode" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Category</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="DDL_category" data-dropup-auto="false" required>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Sub Category</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="DDL_Subcategory" data-dropup-auto="false" required>
								<option value="0">---Select---</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Class</label>
                                <span style="color: Red"></span>
                            </td>
                            <td>
                                <select id="ddlClass" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="right">
                                <label>Channel</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="ddlchannel" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td align="right">
                                <label>Purchase Slab</label>
                            </td>
                            <td>
                                <select id="ddlslb" data-dropup-auto="false" required>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Route</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="ddlTerritory" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
						<tr>
                            <td align="right">
                                <label>Distributor</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="ddl_dis" data-dropup-auto="false" required>
                                </select>
                            </td>
                        </tr>						
						
                    </table>
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Contact Setting</h3>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>Mobile No</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <input class="col-xs-10" id="txtMobile" type="text" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="130px">
                                <label>Secondary Mobile No</label>                                
                            </td>
                            <td>
                                <input class="col-xs-10" id="txtSecMobile" type="text" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Email Id</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtEmail" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Contact Person <span style="color: Red">*</span> Name</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtDOW" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Billing Address</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtAddress" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Shipping Address</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtStreet" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Latitude</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtlat" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Longitude</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtlong" autocomplete="off" />
                            </td>
                        </tr>
                    </table>
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Price Setting</h3>
                    <button style="float: right;" id="newsorder" type="button" class="btn btn-warning btn-circle btn-lg" data-toggle="modal" data-target="#exampleModal">+</button>
                </div>
                <div class="col-md-6">
                    <button style="float: right;" type="button" class="btn btn-warning btn-circle btn-lg" data-toggle="modal" data-target="#modalcontact">+</button>
                    <button style="float: right; margin-left: 20px; display: none;" type="button" id="btnclear" class="btn btn-primary">Clear</button>
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial'; margin-top: 40px;">Account Setting</h3>
                    <table>
                        <tr>
                            <td align="right">
                                <label>Aadhar Number</label>
                            </td>
                            <td>
                                 <input class="col-xs-10" type="text" id="txtAadhar" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Retailer Type</label>
                            </td>
                            <td>
                                <select id="ddlretyp" data-dropup-auto="false" required>
                                    <option selected="selected" value="">---Select---</option>
                                    <option value="0">GST Register</option>
                                    <option value="1">GST UnRegister</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="130px">
                                <label>GST No</label>
                                <span id="gstspan" style="color: Red; display: none">*</span>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="salestaxno" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>FSSAI No</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="FssiNo" autocomplete="off" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="right">
                                <label>TIN No</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="TinNO" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Tax-group</label>
                            </td>
                            <td>
                                <select id="ddltax" data-dropup-auto="false" required>
                                    <option selected="selected" value="">---Select---</option>
                                    <option value="0">Intra</option>
                                    <option value="1">Inter</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>PAN No</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txt_pan_no" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>TCS Applicable</label>
                            </td>
                            <td>
                                <select id="ddlTcs" data-dropup-auto="false" required>
                                    <option selected="selected" value="0">---Select---</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>TDS Applicable</label>
                            </td>
                            <td>
                                <select id="ddlTds" data-dropup-auto="false" required>
                                    <option selected="selected" value="0">---Select---</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Credit Days</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="creditdays" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Outstanding</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtoutstanding" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Credit Limit</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txtcreditlimit" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Advance Amount</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="Txt_advanceamt" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Potential</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="Txt_Mil_Pot" autocomplete="off" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td align="right">
                                <label>Price List</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="ddl_Price" data-dropup-auto="false" required>
                                </select>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left">
                                <label>Type</label>
                            </td>
                            <td>
                                <select id="DDL_Re_Type" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left">
                                <label>UOM</label>
                            </td>
                            <td>
                                <select id="ddl_uom" data-dropup-auto="false" required>
                                </select>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left">
                                <label>CustomerWise Alter</label>
                            </td>
                            <td>
                                <input class="RblAlt" type="radio" value="1" autocomplete="off" checked />
                                <label>ON</label>
                            </td>
                            <td>
                                <input class="RblAlt" type="radio" value="0" autocomplete="off" />
                                <label>OFF</label>
                            </td>
                            <td align="right">
                                <label>(Note : More Then 3 Bill Show Alter)</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Outlet Type</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="ddlOutletType" data-dropup-auto="false" required>
                                    <option selected="selected" value="">---Select---</option>
                                    <option value="0">Universe</option>
                                    <option value="1">Service</option>
                                    <option value="2">Closed</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Invoice Type</label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <select id="ddlDeliveryMode" data-dropup-auto="false" required>
                                    <option selected="selected" value="">---Select---</option>
                                    <option value="0">Invoice</option>
                                    <option value="1">Dc</option>
                                </select>
                            </td>
                        </tr>


                    </table>
                </div>
                <div class="col-md-6">
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Freezer Setting</h3>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>Freezer Type</label>
                            </td>
                            <td>
                                <select id="ddl_freez_typ" data-dropup-auto="false" required>
                                    <option selected="selected" value="0">---Select---</option>
                                    <option value="Glass Top">Glass Top</option>
                                    <option value="Hard Top">Hard Top</option>
                                    <option value="Visi Cooler">Visi Cooler</option>
                                </select>
                            </td>
                        </tr>
                        <td align="right">
                            <label>Freezer Status</label>
                        </td>
                        <td>
                            <select id="ddl_freezer_status" data-dropup-auto="false" required>
                                <option selected="selected" value="0">---Select---</option>
                                <option value="company">Company</option>
                                <option value="Own">Own</option>
                            </select>
                        </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Freezer Tag No</label>
                            </td>
                            <td>
                                <input class="col-xs-10" type="text" id="txt_freezer_tag_no" autocomplete="off" minlength="13" maxlength="13" onkeypress="return /[0-9a-zA-Z]/i.test(event.key)" disabled />

                            </td>
                            <td>
                                <button style="float: left; margin-right: 20px;" type="button" class="btn btn-warning btn-circle btn-lg" data-toggle="modal" data-target="#modalfreeztyp">+</button>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Freezer DOD</label>
                            </td>
                            <td>
                                <input type="date" class="form-control" id="frezdate" style="width: 65%" />
                            </td>
                        </tr>
                    </table>
                </div>


                <div class="col-md-2" style="display: none">
                    <table id="upltbl" style="border-collapse: collapse;">
                        <tr>
                            <td colspan="2" align="center" style="padding-top: 57px; padding-right: 16px; /* padding-bottom: 57px; */padding-left: 16px;">
                                <img id="upimg" style="width: 80%; /* height: 0%; */" src="https://www.w3schools.com/w3css/img_avatar3.png" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="/* padding-top: 57px; */padding-right: 57px; padding-bottom: 16px; padding-left: 57px;">
                                <label for="uplfile" class="input-group-btn">
                                    <span class="btn btn-primary"><i class="fa fa-cloud-upload append-icon"></i>&nbsp Upload</span>
                                </label>
                                <!--<button for="uplfile" type="button" class="btn btn-primary">Upload</button>-->
                                <input accept=".jpg,.jpeg,.png" id="uplfile" class="sr-only" name="uplfile" type="file" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="modal fade" id="modalfreeztyp" tabindex="-1" style="z-index: 1000000; background-color: transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="bttnpls1" class="btn btn-info btn-circle btn-lg" style="float: right">+</button>
                            <h5 class="modal-title" id="modalcontactLabel1">FreezType</h5>
                        </div>
                        <div class="modal-body">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-6">Additional Fields</div>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                </div>
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">Name</div>
                                        <div class="col-sm-6">Value</div>
                                    </div>
                                    <div class="scrldiv1" style="height: 120px;">
                                    </div>
                                </div>
                                <!-- /.panel-body -->
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="btnClose" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="modalcontact" tabindex="-1" style="z-index: 1000000; background-color: transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="bttnpls" class="btn btn-info btn-circle btn-lg" style="float: right">+</button>
                            <h5 class="modal-title" id="modalcontactLabel">Contact Fields</h5>
                        </div>
                        <div class="modal-body">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-6">Additional Fields</div>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">Name</div>
                                        <div class="col-sm-6">Value</div>
                                    </div>
                                    <div class="scrldiv2" style="height: 120px;">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="exampleModal" style="z-index: 10000000; background-color: transparent; overflow-y: auto;display:none;" tabindex="0" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog" role="document" style="width: 1200px !important">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="exampleModalLabel">Retailer Wise Price</h4>
                                 <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="control-label " for="focusedInput">
                                            Effective From</label>
                                        <span style="color: Red">*</span>
                                        <input type="date" id="eff_dt" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12" style="padding: 15px">
                                    <div class="tableBodyScroll">
                                        <table class="table table-hover" id="OrderList" style="background-color: #f1f1f1;">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th style="text-align: left">S.No</th>
                                                    <th style="text-align: left">Product</th>
                                                    <th style="text-align: left;">Unit</th>
                                                    <th style="text-align: center;">MRP Price</th>
                                                    <th style="text-align: center">ON Invoice Price</th>
                                                    <th style="text-align: center">OFF Invoice Dis</th>
                                                    <th style="text-align: center">Net Price</th>
                                                    <th style="text-align: center">Status</th>
                                                    <th style="text-align: right">Remark View</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                            </div>
                            <div class="row" style="margin-left: 0%;">
                                <div>
                                    <div style="text-align: left" colspan="9">
                                        <button type="button" class="btn btn-success" onclick="AddRow()" style="font-size: 12px">+ Add Product </button>
                                        <button type="button" class="btn btn-danger" onclick="DelRow()" style="font-size: 12px">- Remove Product</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" id="btnOrderListOk" data-dismiss="modal">Ok</button>
                            <button type="button" class="btn btn-secondary" id="btnOrderListclose" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div>
            </div>
        </form>

        <script src="/js/jquery.min.js" type="text/javascript"></script>

        <script type="text/javascript">
            var dchannel = []; var dTerritory = []; var dType = []; var dCategory = [];
            var dSlab = []; var dUom = []; var retlrDetails = [];
            var Retcode = ''; var ftparr = []; var dGroup = []; var dPrice = [];
            var freezer_tag_no_pre = ''; var All_Product_Details = []; var Product_Details = []; var NewOrd = []; var Price_card_Details = [];
            var Product_array = []; var Prds = ""; var units1 = ""; var All_unit = []; var scrollhightchnage = ''; var units = ""; var BodyData = []; var Hierarchy_Detail = []; var MRP_details = []; var Dis_Detail=[];
            var Prds1 = "";

            $(document).on('click', '#newsorder', function () {

                arr = [];
                var retailercode = $('#Txt_id').val();
				
				var dist_Code = $('#ddl_dis option:selected').val();
				
				if(dist_Code !="0"){

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/Get_price_card_details",
                    data: "{'retailerCode':'" + retailercode + "'}",
                    dataType: "json",
                    success: function (data) {
                        Price_card_Details = JSON.parse(data.d);
                        if (Price_card_Details.length > 0) {
                            ReloadTable(Price_card_Details);
                        }
                        else {
                            $('#exampleModal').modal('toggle');
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
				}
				else{					
					alert('Please Select Distributor');
					$('#exampleModal').css('display','none');			
					return false;			
							
				}
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RetailerAdd.aspx/GetProducts",
                dataType: "json",
                success: function (data) {
                    Product_array = JSON.parse(data.d) || [];
                    Prds1 += "<option  selected='selected' value='0'>Select Product</option>";
                    for ($k = 0; $k < Product_array.length; $k++) {
                        Prds1 += "<option value='" + Product_array[$k].Product_Detail_Code + "'>" + Product_array[$k].Product_Detail_Name + "</option>";
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RetailerAdd.aspx/Get_hierarchy_detail",
                dataType: "json",
                success: function (data) {
                    Hierarchy_Detail = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
			
			 $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RetailerAdd.aspx/Get_Dis_detail",
                dataType: "json",
                success: function (data) {
                    Dis_Detail = JSON.parse(data.d) || [];
                    $("#ddl_dis").empty().append('<option selected="selected" value="0">---Select---</option>');
                    if (Dis_Detail.length > 0) {
                        for (var i = 0; i < Dis_Detail.length; i++) {
                            if ('<%=Session["sf_type"]%>' == '4') {
                                $("#ddl_dis").append($('<option  value="' + Dis_Detail[i].Stockist_Code + '" selected>' + Dis_Detail[i].Stockist_Name + '</option>'))
                            }else
                            $("#ddl_dis").append($('<option value="' + Dis_Detail[i].Stockist_Code + '">' + Dis_Detail[i].Stockist_Name + '</option>'))
                        }
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
			
			

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RetailerAdd.aspx/Get_Product_unit",
                dataType: "json",
                success: function (data) {
                    All_unit = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });
			
			$.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RetailerAdd.aspx/Get_MRPRate_detail",
                dataType: "json",
                success: function (data) {
                    MRP_details = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

			function get_unit(Pcode, UnitCode) {

                units1 = '';

                filter_unit = All_unit.filter(function (w) {
                    return (Pcode == w.Product_Detail_Code);
                });

                if (filter_unit.length > 0) {
                    for (var z = 0; z < filter_unit.length; z++) {
                        units1 += "<option " + ((filter_unit[z].Move_MailFolder_Id == UnitCode) ? 'selected' : '') + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                    }
                }
            }

            function ReloadTable(Orders) {

                $("#OrderList tbody").html('');
				$('#eff_dt').val(Orders[0].Effective_From_Date);
                if (NewOrd.length <= 0) {

                    for ($k = 0; $k < Orders.length; $k++) {

                        NewOrd.push({
                            retailer: Orders[$k].Retailer_code,
                            pcode: Orders[$k].Product_Code,
                            PName: Orders[$k].Product_Name,
                            Unit: Orders[$k].Unit,
                            Unit_Code: Orders[$k].UnitCode,
                            Product_Sale_Unit: Orders[$k].Product_Sale_Unit,
                            product_unit: Orders[$k].product_unit,
                            Base_Unit_code: Orders[$k].Base_Unit_code,
                            Sample_Erp_Code: Orders[$k].Sample_Erp_Code,
                            UOM_Weight: Orders[$k].UOM_Weight,
                            MRP_Price: Orders[$k].MRP_Price,
                            Rate: Orders[$k].Rate,
                            OffInvPrice: Orders[$k].OffInvPrice,
                            Net_Price: Orders[$k].Net_Price,
                            Default_umo: Orders[$k].Default_UOM,
                            con_fac: Orders[$k].con_fac,
                            Flag: Orders[$k].detail_flag,
                            sub_division: Orders[$k].sub_div_code
                        });
                    }
                }
				
				

                for (var st = 0; st < NewOrd.length; st++) {

                    Prds = '';

                    for (var k = 0; k < Product_array.length; k++) {

                        if (Product_array[k].Product_Detail_Code == NewOrd[st].pcode) {
                            Prds += "<option selected value='" + Product_array[k].Product_Detail_Code + "'>" + Product_array[k].Product_Detail_Name + "</option>";
                        }
                        else {
                            Prds += "<option value='" + Product_array[k].Product_Detail_Code + "'>" + Product_array[k].Product_Detail_Name + "</option>";
                        }
                    }
                    get_unit(NewOrd[st].pcode, NewOrd[st].Unit_Code);


                    tr = $("<tr class=" + NewOrd[st].pcode + "></tr>");
                    slno = st + 1;

                    $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm' style='width: 15px;'><span class='rwsl'>" + slno + "</span></label></td>" +
                        "<td class= 'pro_td' style = 'width:23%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:35px;'><option value=''>--select--</option>" + Prds + "</select></td> " +
                        "<td class='uni_td'><select class='unit' style='margin-top:-3px;height:35px;'><option value='0'>Select</option>" + units1 + "</select></td>" +
                        "<td style = 'display:none;' ><input type='text' class='Product_Sale_Unit' style='text-align:center' value=" + NewOrd[st].Product_Sale_Unit + "></td>" +
                        "<td style = 'display:none;' ><input type='text' class='Product_Unit' style='text-align:center' id='Product_Unit' value=" + NewOrd[st].product_unit + "></td>" +
                        "<td style = 'display:none;' > <input type='text' style='text-align:center' class='Product_Sale_Unit_code' value=" + NewOrd[st].Base_Unit_code + "></td>" +
                        "<td style='display:none;'><input type='text' style='text-align:center' class='Product_Unit_code' value=" + NewOrd[st].Unit_Code + "></td>" +
                        "<td style='display:none;'><input type='text' style='text-align:center' class='Product_sample_erp_code' value=" + NewOrd[st].Sample_Erp_Code + "></td>" +
                        "<td style='display:none;'><input type='text' id='UomWeight' class='UomWeight' style='text-align:center' value=" + NewOrd[st].UOM_Weight + "></td>" +
                        "<td style='text-align:center'><input type='text' autocomplete='off'  id='MRP_Price' class='MRP_Price' style='text-align:right;width: 70px;' value=" + NewOrd[st].MRP_Price + "></td>" +
                        //"<td style='text-align:center'><input type='text' autocomplete='off' id='rate' class='rate' style='text-align:right' value=" + NewOrd[st].Rate + "></td>" +
                        "<td style='text-align:center'><input type='text' autocomplete='off' id='On_Inv_price' class='On_Inv_price' style='text-align:right;width: 70px;' value=" + NewOrd[st].Rate + "></td>" +
                        "<td style='text-align:center'><input type='text' autocomplete='off' id='Off_Inv_price' class='Off_Inv_price' style='text-align:right;width: 70px;' value=" + NewOrd[st].OffInvPrice + "></td>" +
                        "<td style='text-align:center'><input type='text' autocomplete='off' id='Net_Price' class='Net_Price' style='text-align:right;width: 70px;' value=" + NewOrd[st].Net_Price + "></td>" +
                        "<td style='text-align:center'><label id='Price_Status' class='rate' value=" + NewOrd[st].Flag + " style='text-align:right' >" + ((NewOrd[st].Flag == 1) ? 'Pending' : 'Price Approved') + "</label></td>" +
                        "<td style='text-align:right;'><a id='myButton1' href='#' onClick='Rem(\"" + NewOrd[st].pcode + "\")'><span class='glyphicon glyphicon-eye-open'></span></a></td>" +
                        "<td style ='display:none'><input type='hidden' id='Default_umo_code' class='Default_umo_code' value=" + NewOrd[st].Default_umo + "></td>" +
                        "<td style ='display:none'><input type='hidden' id='Default_con_fac' class='Default_con_fac' value=" + NewOrd[st].con_fac + "></td>" +
                        "<td style ='display:none'><input type='hidden' id='Active_flg' class='Active_flg' value=" + NewOrd[st].Flag + "></td>" +
                        "<td style ='display:none'><input type='hidden' id='sub_div' class='sub_div' value=" + NewOrd[st].sub_division + "></td>" +
                        "<td style='display:none;'><input type='hidden' id='hidden_MRP_Price' class='hidden_MRP_Price' style='text-align:right' value=" + NewOrd[st].MRP_Price + "></td>" +
                        "<td style='display:none;'><input type='hidden' id='hidden_rate' class='hidden_rate' style='text-align:right' value=" + NewOrd[st].Rate + "></td>" +
                        "<td style='display:none;'><input type='hidden' id='hidden_On_Inv_price' class='hidden_On_Inv_price' style='text-align:right' value=" + NewOrd[st].Rate + "></td>" +
                        "<td style='display:none;'><input type='hidden' id='hidden_On_Off_Inv_price' class='hidden_On_Off_Inv_price' style='text-align:right' value=" + NewOrd[st].OffInvPrice + "></td>" +
                        "<td style='display:none;'><input type='hidden' id='hidden_Net_Price' class='hidden_Net_Price' style='text-align:right' value=" + NewOrd[st].Net_Price + "></td>"
                    );
                    $("#OrderList > tbody").append(tr);

                    if (NewOrd[st].Flag == '0') {
                        $("#OrderList > tbody tr").eq(st).find('#Price_Status').addClass('green');
                    }
                    else if (NewOrd[st].Flag == '1') {
                        $("#OrderList > tbody tr").eq(st).find('#Price_Status').addClass('red');
                    }
                }
                $('.ddlProd').trigger('chosen:updated').css("width", "100%");
                $('.unit').trigger('chosen:updated').css("width", "100%");
                $('.unit').prop('disabled', true).trigger("chosen:updated");
            }

            function AddRow() {

                itm = {}
                itm.pcode = ''; itm.PName = ''; itm.Unit = ''; itm.Unit_Code = '';
                NewOrd.push(itm);
                currentheight = $('.modal-content').height();

                tr = $("<tr class='subRow'></tr>");
                $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm' style='width: 15px;'> <span class='rwsl'>" + ($("#OrderList > tbody > tr").length + 1) + "</span></label></td><td class='pro_td' style='width:27%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:25px;'>" + Prds1 + "</select></td>" +
                    "<input type='hidden' class='prodCode'></td>" +
                    "<td class='uni_td'><select class='unit'><option value='0'>Select</option></select></td><input type='hidden' class='Product_Sale_Unit' />" +
                    "<input type='hidden' class='Product_Unit' style='text-align:center' id='Product_Unit'></td>" +
                    "<input type='hidden' class='Product_Sale_Unit_code'></td>" +
                    "<input type='hidden' class='Product_Unit_code'></td>" +
                    "<input type='hidden' class='Product_sample_erp_code'></td>" +
                    "<input type='hidden' class='UomWeight' ></td>" +
                    "<td style='text-align:center'><input type='text' autocomplete='off' disabled class='MRP_Price' style='text-align:right;width: 70px;'></td>" +
                    //"<td style='text-align:center'><input autocomplete='off' type='text' class='rate' style='text-align:right'></td>" +
                    "<td style='text-align:center'><input autocomplete='off' type='text' class='On_Inv_price' style='text-align:right;width: 70px;'></td>" +
                    "<td style='text-align:center'><input autocomplete='off' type='text' class='Off_Inv_price' style='text-align:right;width: 70px;'></td>" +
                    "<td style='text-align:center'><input autocomplete='off' disabled type='text' class='Net_Price' style='text-align:right;width: 70px;'></td>" +
                    "<td></td><td></td>" +
                    "<td style='display:none'><input type='hidden' class='Default_umo_code'></td>" +
                    "<td style='display:none'><input type='hidden' class='Default_con_fac'></td>" +
                    "<td style='display:none'><input type='hidden' id='Active_flg' class='Active_flg'></td>" +
                    "<td style='display:none'><input type='hidden' id='sub_div' class='sub_div'></td>" +
                    "<td style='display:none;'><input type='hidden' id='hidden_MRP_Price' class='hidden_MRP_Price' style='text-align:right'></td>" +
                    //"<td style='display:none;'><input type='hidden' id='hidden_rate' class='hidden_rate' style='text-align:right'></td>");
                    "<td style='display:none;'><input type='hidden' id='hidden_On_Inv_price' class='hidden_On_Inv_price' style='text-align:right'></td>" +
                    "<td style='display:none;'><input type='hidden' id='hidden_On_Off_Inv_price' class='hidden_On_Off_Inv_price' style='text-align:right'></td>" +
                    "<td style='display:none;'><input type='hidden' id='hidden_Net_Price' class='hidden_Net_Price' style='text-align:right'></td>");

                $("#OrderList > tbody").append(tr);
                $('.ddlProd').chosen();
                $('.unit').chosen();
            }
			
            function DelRow() {

                $(".slitm:checked").each(function () {

                    itr = $(this).closest('tr');
                    idx = $(itr).index();
                    $(this).closest('tr').remove();
                    NewOrd.splice(idx, 1);
                }); resetSL();
            }

            resetSL = function () {
                $(".rwsl").each(function () {
                    $(this).text($(this).closest('tr').index() + 1);
                });
            }

            $(document).ready(function () {
			
             $(document).on("keyup", ".On_Inv_price", function () {

                itr = $(this).closest('tr');
                idx = $(itr).index();
                var Prod_mrp_price = itr.find('.MRP_Price').val();
                if (Prod_mrp_price == '' || Prod_mrp_price == 0 || Prod_mrp_price == undefined) { Prod_mrp_price = 0; }
                var Given_price = $(this).val();


                if (parseFloat(Given_price) > parseFloat(Prod_mrp_price)) {
                    alert('Price is Greater than Mrp..Please Check the Price');
                    itr.find('.On_Inv_price').val('');
                    return false;
                }

                var tis_val = $(this).val();
                if (tis_val == "") { itr.find('.Off_Inv_price').val(''); itr.find('.Net_Price').val(''); }
                var off_val = itr.find('.Off_Inv_price').val();
                var cal_net = tis_val - off_val;
                itr.find('.Net_Price').val(cal_net)

            });

				$(document).on("keyup", ".Off_Inv_price", function () {

                itr = $(this).closest('tr'); var Net_prc = 0;
                idx = $(itr).index();

                var Prod_mrp_price = itr.find('.MRP_Price').val();
                if (Prod_mrp_price == '' || Prod_mrp_price == 0 || Prod_mrp_price == undefined) { Prod_mrp_price = 0; }

                var Given_on_inv_price = itr.find('.On_Inv_price').val();
                var Given_off_inv_price = $(this).val();
                if (Given_off_inv_price == '' || Given_off_inv_price == undefined || isNaN(Given_off_inv_price)) { Given_off_inv_price = 0; }

                if (Given_on_inv_price == '' || Given_on_inv_price == 0 || Given_on_inv_price == undefined) {
                    alert('Please Enter On Invoice Price');
                    itr.find('.Off_Inv_price').val('');
                    itr.find('.On_Inv_price').focus();
                    return false;
                }

                Net_prc = parseFloat(Given_on_inv_price) - parseFloat(Given_off_inv_price);
                itr.find('.Net_Price').val(Net_prc);

            });

				
                $(document).on("change", ".ddlProd", function () {

                    itr = $(this).closest('tr');
                    idx = $(itr).index();

                    sPCd = $(this).val();
                    $(itr).find('.Active_flg').val(1);
                    $(this).closest("tr").attr('class', $(this).val());
                    var P_Name = itr.find('.ddlProd').find('option:selected').text();

                    pro_filter = NewOrd.filter(function (s, key) {
                        return (s.pcode == sPCd && key != idx);
                    });

                    if (pro_filter.length <= 0) {

                        Prod = Product_array.filter(function (a) {
                            return (a.Product_Detail_Code == sPCd);
                        });

                        MRP_details_filter = MRP_details.filter(function (a) {
                            return (a.Prod_Code == sPCd);
                        });
						
						if(Prod[0].Default_UOM == '15'){
						
						var mrpprice = MRP_details_filter[0].MRP;
						
						}
						else{
						
						var mrpprice1 = Prod[0].Default_UOMQty * Prod[0].Sample_Erp_Code;					
						var mrpprice=  MRP_details_filter[0].MRP / mrpprice1 ;
												
						}

                       // if (Prod[0].Default_UOM == '14') {
                       //     var mrpprice = MRP_details_filter[0].MRP;
                       // }
                       // else {
                       //     var mrpprice = MRP_details_filter[0].MRP / 12;
                       // };

                        get_unit(sPCd, Prod[0].Move_MailFolder_Id);
                        $(itr).find('.MRP_Price').val(parseFloat(mrpprice).toFixed(2));
                        $(itr).find('.uni_td').html("<select class='unit' style='margin-top:-3px;height:25px;'><option value='0'>Select</option>" + units1 + "</select>")
                        $(itr).find('.prodCode').val(sPCd);
                        $(itr).find('.prodName').val(P_Name);
                        $(itr).find('.Product_Sale_Unit').val(Prod[0].Product_Sale_Unit);
                        $(itr).find('.Product_Sale_Unit_code').val(Prod[0].Base_Unit_code);
                        $(itr).find('.Product_Unit').val(Prod[0].product_unit);
                        $(itr).find('.Product_Unit_code').val(Prod[0].Unit_code);;
                        $(itr).find('.Product_sample_erp_code').val(Prod[0].Sample_Erp_Code);
                        $(itr).find('.UomWeight').val(Prod[0].UOM_Weight);
                        NewOrd[idx].pcode = sPCd;
                        NewOrd[idx].PName = P_Name;
                    }
                    else {

                        $(itr).find('.ddlProd').val('');
                        $(itr).find('.ddlProd ').chosen("destroy");
                        $(itr).find('.ddlProd').chosen();
                        $(itr).find('.unit').val('');
                        $(itr).find('.unit ').chosen("destroy");
                        $(itr).find('.unit').chosen();
                        $(itr).find('.MRP_Price').val('');
                        $(itr).find('.rate').val('');
                        NewOrd[idx].pcode = '';
                        NewOrd[idx].PName = '';
                        NewOrd[idx].MRP_Price = '';
                        NewOrd[idx].Unit = '';
                        NewOrd[idx].Unit_Code = '';
                        NewOrd[idx].Rate = '';
                        alert('Product Units Already Selected');
                        set = 1;
                        return false;
                    }

                    $('.unit').chosen();
                    $('.unit').prop('disabled', true).trigger("chosen:updated");

                    new_height = $('.modal-content').height();
                    scrollhightchnage = scrollhightchnage + ((new_height - currentheight) * 2);
                    scrollhightchnageDel = scrollhightchnage;
                    $('#exampleModal').scrollTop(scrollhightchnage);

                });
				
				$(document).on("change", "#DDL_category", function (e) {
                    var selVal = $(this).val();
                    loadSubCategory(selVal);
                });

				$(document).on("change", ".unit", function (e) {

                    var row = $(this).closest("tr");
                    var indx = $(row).index();
                    var Selected_Unit = row.find('.unit option:selected').text();
                    var Selected_Unit_Code = row.find('.unit option:selected').val();
                    var selected_pro_code = row.find('.prodCode').val();
                    NewOrd[idx].Unit = Selected_Unit;
                    NewOrd[idx].Unit_Code = Selected_Unit_Code;
                    row.find('.Default_umo_code').val(Selected_Unit_Code);
                    var arr = [];

                    arr = Product_array.filter(function (r) {
                        return (r.Product_Detail_Code == selected_pro_code);
                    });

                    if (arr[0].Base_Unit_code == Selected_Unit_Code) {
                        $(itr).find('.Default_con_fac').val(1);
                    }
                    else if (arr[0].Unit_code == Selected_Unit_Code) {
                        $(itr).find('.Default_con_fac').val(arr[0].Sample_Erp_Code);
                    }
                    else {
                        $(itr).find('.Default_con_fac').val(Prod[0].Default_UOMQty);
                    }
                });

                $(document).on('keyup', '.MRP_Price', function () {
                    var row = $(this).closest("tr");
                    var indx = $(row).index();
                    NewOrd[indx].MRP_Price = $(this).val();
                });

                $(document).on('keyup', '.rate', function () {
                    var row = $(this).closest("tr");
                    var indx = $(row).index();
                    NewOrd[indx].Rate = $(this).val();
                });

            });

            function loadRetcode() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillRetNumber",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        Retcode = JSON.parse(data.d) || '';
                        $("#Txt_id").val(Retcode);
                    }
                });
            }


            function loadState() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillState",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dstate = JSON.parse(data.d) || [];
                        $("#ddl_state").empty().append('<option selected="selected" value="0">---Select---</option>');
                        if (dstate.length > 0) {
                            for (var i = 0; i < dstate.length; i++) {
                                if ('<%=Session["sf_type"]%>' == '4') {
                                     $("#ddl_state").append($('<option value="' + dstate[i].State_Code + '" selected>' + dstate[i].StateName + '</option>'))                                    
                                 }else
                                $("#ddl_state").append($('<option value="' + dstate[i].State_Code + '">' + dstate[i].StateName + '</option>'))
                            }
                        }
                    }
                });
                //$('#ddl_state').selectpicker({
                //    liveSearch: true
                //});
            }

            function loadChannel() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillChannel",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dchannel = JSON.parse(data.d) || [];
                        if (dchannel.length > 0) {
                            for (var i = 0; i < dchannel.length; i++) {
                                $("#ddlchannel").append($('<option value="' + dchannel[i].Doc_Special_Code + '">' + dchannel[i].Doc_Special_Name + '</option>'))
                            }
                        }
                    }
                });

            }

            function loadTerritory() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillTerritory",
                    data: "{'divcode':'<%=Session["div_code"]%>','sf_type':'<%=Session["sf_type"]%>','sf_code':'<%=Session["sf_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dTerritory = JSON.parse(data.d) || [];
                        if (dTerritory.length > 0) {
                            for (var i = 0; i < dTerritory.length; i++) {
                                $("#ddlTerritory").append($('<option value="' + dTerritory[i].Territory_Code + '">' + dTerritory[i].Territory_Name + '</option>'))
                            }
                        }
                    }
                });
                //$('#ddlTerritory').selectpicker({
                //    liveSearch: true
                //});
            }

            function loadClass() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillClass",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dClass = JSON.parse(data.d) || [];
                        if (dClass.length > 0) {
                            for (var i = 0; i < dClass.length; i++) {
                                $("#ddlClass").append($('<option value="' + dClass[i].Doc_ClsCode + '">' + dClass[i].Doc_ClsName + '</option>'))
                            }
                        }
                    }
                });
                //$('#ddlClass').selectpicker({
                //    liveSearch: true
                //});
            }

            function loadType() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillType",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dType = JSON.parse(data.d) || [];
                        $("#DDL_Re_Type").empty().append('<option selected="selected" value="0">---Select---</option>');
                        if (dType.length > 0) {
                            for (var i = 0; i < dType.length; i++) {
                                $("#DDL_Re_Type").append($('<option value="' + dType[i].Type_Id + '">' + dType[i].Name + '</option>'))
                            }
                        }
                    }
                });
                //$('#DDL_Re_Type').selectpicker({
                //    liveSearch: true
                //});
            }


            function loadCategory() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillCatagry",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dCategory = JSON.parse(data.d) || [];
                        if (dCategory.length > 0) {
                            for (var i = 0; i < dCategory.length; i++) {
                                $("#DDL_category").append($('<option value="' + dCategory[i].Doc_Cat_Code + '">' + dCategory[i].Doc_Cat_Name + '</option>'))
                            }
                        }
                    }
                });
                //$('#DDL_category').selectpicker({
                //    liveSearch: true
                //});
            }
            function loadSubCategory(catCode) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillSubCatagry",
                    //data: "{'divcode':'<%=Session["div_code"]%>'}",
					data: "{'divcode':'<%=Session["div_code"]%>','cat_code':'" + catCode+"'}",
                    dataType: "json",
                    success: function (data) {
                        dSubCategory = JSON.parse(data.d) || [];
                        if (dSubCategory.length > 0) {
						    $("#DDL_Subcategory").html('');
                            for (var i = 0; i < dSubCategory.length; i++) {
                                $("#DDL_Subcategory").append($('<option value="' + dSubCategory[i].Doc_SubCatCode + '">' + dSubCategory[i].Doc_SubCatName + '</option>'))
                            }
                        }
                    }
                });
                //$('#DDL_category').selectpicker({
                //    liveSearch: true
                //});
            }

            function loadSlab() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/Filldrslab",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dSlab = JSON.parse(data.d) || [];
                        $("#ddlslb").empty().append('<option selected="selected" value="0">---Select---</option>');
                        if (dSlab.length > 0) {
                            for (var i = 0; i < dSlab.length; i++) {
                                $("#ddlslb").append($('<option value="' + dSlab[i].Code + '">' + dSlab[i].Name + '</option>'))
                            }
                        }
                    }
                });
                //$('#ddlslb').selectpicker({
                //    liveSearch: true
                //});
            }

            function loadUOM() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillUOM",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dUom = JSON.parse(data.d) || [];
                        $("#ddl_uom").empty().append('<option selected="selected" value="0">---Select---</option>');
                        if (dUom.length > 0) {
                            for (var i = 0; i < dUom.length; i++) {
                                $("#ddl_uom").append($('<option value="' + dUom[i].Move_MailFolder_Id + '">' + dUom[i].Move_MailFolder_Name + '</option>'))
                            }
                        }
                    }
                });

            }

            function loadPrice() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillPrice",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dPrice = JSON.parse(data.d) || [];
                        $("#ddl_Price").empty().append('<option selected="selected" value="0">---Select---</option>');
                        if (dPrice.length > 0) {
                            for (var i = 0; i < dPrice.length; i++) {
                                $("#ddl_Price").append($('<option value="' + dPrice[i].Price_list_Sl_No + '">' + dPrice[i].Price_list_Name + '</option>'))
                            }
                        }
                    }
                });
            }

            function loadGroup() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillGroup",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        dGroup = JSON.parse(data.d) || [];
                        $("#ddl_group").empty().append('<option selected="selected" value="0">---Select---</option>');
                        if (dGroup.length > 0) {
                            for (var i = 0; i < dGroup.length; i++) {
                                $("#ddl_group").append($('<option value="' + dGroup[i].Doc_QuaCode + '">' + dGroup[i].Doc_QuaName + '</option>'))
                            }
                        }
                    }
                });
            }
            function loadSubName() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/FillSubName",
                   <%-- data: "{'divcode':'<%=Session["div_code"]%>','sf_type':'<%=Session["sf_type"]%>','sf_code':'<%=Session["sf_code"]%>'}",--%>
                    data: "{}",
                    dataType: "json",
                    success: function (data) {
                        dSubName = JSON.parse(data.d) || [];
                        $('#retsubName').empty('');
                        if (dSubName.length > 0) {
                            for (var i = 0; i < dSubName.length; i++) {
                                if (dSubName[i].ListedDr_SubName != '') {
                                    $('#retsubName').append($('<option data-xyz=' + dSubName[i].ListedDr_SubName + ' value="' + dSubName[i].ListedDr_SubName + '">'));
                                }
                            }
                        }
                    }
                });
            }



            function clearfields() {

                $('#ddlchannel').val("0");
                $("#ddlTerritory").val("0");
                $("#ddlClass").val("0");
                $("#DDL_Re_Type").val("0");
                $("#DDL_category").val("0");
                $("#DDL_Subcategory").val("0");
                $("#ddl_Price").val("0");
                $("#ddl_group").val("0");
                $("#ddlslb").val("0");
                $("#ddl_uom").val("0");
                $("#ddlTcs").val("0");
                $("#ddlTds").val("0");
                $("#ddl_freezer_status").val("0");
                $("#ddl_freez_typ").val("0");
                $("#ddl_state").val("0");
                $("#ddlretyp").val("");
                $("#ddltax").val("");
                $("#Txt_id").val('');
                $("#txtName").val('');
                $("#txtMobile").val('');
                $("#txtERBCode").val('');
                $("#TinNO").val('');
                $("#FssiNo").val('');
                $("#creditdays").val('');
                $("#Txt_advanceamt").val('');
                $("#txtStreet").val('');
                $("#Txt_Mil_Pot").val('');
                $("#txtlat").val('');
                $("#txtlong").val('');
                $('#txtoutstanding').val('');
                $('#txtDOW').val('');
                $('#salestaxno').val('');
                $('#txtcreditlimit').val('');
                $('#txtEmail').val('');
                $('#txt_freezer_tag_no').val('');
                $('#txt_pan_no').val('');
                $('#txtAddress').val('');
                $('#frezdate').val('');
                $('#txtSecMobile').val('');
                $('#txtAadhar').val('');
                $('#txtsubName').val('');               
            }

            $(document).ready(function () {
			
                loadChannel();
                loadTerritory();
                loadClass();
                loadType();
                loadCategory();
                //loadSubCategory();
                loadSlab();
                loadUOM();
                loadRetcode();
                loadState();
                loadPrice();
                loadGroup();
                loadSubName();
				
				var dtToday = new Date();
                var month = dtToday.getMonth() + 1;
                var day = dtToday.getDate();
                var year = dtToday.getFullYear();
                if (month < 10)
                    month = '0' + month.toString();
                if (day < 10)
                    day = '0' + day.toString();

                var maxDate = year + '-' + month + '-' + day;
                $('#eff_dt').attr('min', maxDate);

                $(document).on('keypress', '#txtMobile', function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
				
				
                $('#btnclear').on('click', function () {
                    clearfields();
                });

                $('#btnBack1').on('click', function () {
                    if ('<%=Session["sf_type"]%>' == '4') {
                        window.location.href = '/Stockist/Retailer_List.aspx';
                    }
                    else {
                        window.location.href = '../Retailer_Details.aspx';
                    }

                });

                var ctr2 = 1;
                $('#bttnpls').on('click', function () {
                    ctr2++;
                    var txtfield = "txtfield" + ctr2;
                    var txtval = "txtval" + ctr2;
                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" autocomplete="off" id=' + txtfield + ' /></div><div class="col-sm-6 cls"><input type="text" name="value" autocomplete="off" id=' + txtval + ' /></div></div>';
                    $('.scrldiv2').append(newTr);
                });

                var ctr1 = 1;
                $('#bttnpls1').on('click', function () {
                    ctr1++;
                    var txtfield = "txtfield" + ctr1;
                    var txtval = "txtval" + ctr1;
                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field1"  value="Freezer Tag No" autocomplete="off" id=' + txtfield + ' /></div><div class="col-sm-6 cls"><input type="text" name="value1" maxlength="13" value= ' + freezer_tag_no_pre + ' onkeypress="return /[0-9a-zA-Z]/i.test(event.key)" autocomplete="off" id=' + txtval + ' /></div></div>';
                    $('.scrldiv1').append(newTr);
                });

                $('#ddl_freez_typ').on('change', function () {
                    var frztyp = $("#ddl_freez_typ").val();
                    if (frztyp == 'Glass Top')
                        $("#txt_freezer_tag_no").val('GT');
                    else if (frztyp == 'Hard Top')
                        $("#txt_freezer_tag_no").val('HT');
                    else if (frztyp == 'Visi Cooler')
                        $("#txt_freezer_tag_no").val('VC');
                    $("#txt_freezer_tag_no").prop("disabled", false);
                    freezer_tag_no_pre = $("#txt_freezer_tag_no").val();
                });

                $('#ddlTcs').on('change', function () {
                    if ($('#ddlTcs option:selected').text() == 'Yes' && $('#ddlTds option:selected').text() == 'Yes') {
                        alert("Already TDS yes is selected. please choose Any one as Yes");
                        $('#ddlTcs').focus();
                    }
                });

                $('#ddlTds').on('change', function () {
                    if ($('#ddlTcs option:selected').text() == 'Yes' && $('#ddlTds option:selected').text() == 'Yes') {
                        alert("Already TCS yes is selected. please choose Any one as Yes");
                        $('#ddlTds').focus();
                    }
                });
                $('#ddlretyp').on('change', function () {
                    if ($('#ddlretyp option:selected').text() == 'Register') {
                        $('#gstspan').show();
                    } else
                        $('#gstspan').hide();
                });
                $('#ddlTerritory').on('change', function () {
                   /*var Routetxt = $("#ddlTerritory option:selected").text();
                   var Routeval = $("#ddlTerritory option:selected").val();
                   distFiltr = Dis_Detail.filter(function (a) {
                        return (a.Territory_Code == Routeval);
                    });
                    $("#ddl_dis").empty().append('<option selected="selected" value="0">---Select---</option>');
                    if (distFiltr.length > 0) {
                        for (i = 0; i < distFiltr.length; i++) {
                             $("#ddl_dis").append($('<option value="' + distFiltr[i].Stockist_Code + '">' + distFiltr[i].Stockist_Name + '</option>'))
                        }
                    }  */                 
                   
                });
                


                var retcode = $('#<%=HiddenFieldRetCode.ClientID%>').val();
                if (retcode == '') { }
                else {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerAdd.aspx/getRetDets",
                        data: "{'retcode':'" + retcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            retlrDetails = data.d;
                            $('#txtName').val(retlrDetails[0].DrName);
                            $('#Txt_id').val(retlrDetails[0].DrCode);
                            $('#txtMobile').val(retlrDetails[0].MobileNo);
                            $('#txtERBCode').val(retlrDetails[0].ErbCode);
                            $('#txtDOW').val(retlrDetails[0].ContactPerson);
                            $('#TinNO').val(retlrDetails[0].Tinno);
                            $('#FssiNo').val(retlrDetails[0].FssiNo);
                            $('#salestaxno').val(retlrDetails[0].SalesTax);
                            $('#creditdays').val(retlrDetails[0].CreditDays);
                            $('#Txt_advanceamt').val(retlrDetails[0].Ad);
                            $('#txtAddress').val(retlrDetails[0].DrAddress1);
                            $('#txtStreet').val(retlrDetails[0].DrAddress2);
                            $('#Txt_Mil_Pot').val(retlrDetails[0].MilkPon);
                            $('#txtoutstanding').val(retlrDetails[0].Outstandng);
                            $('#txtcreditlimit').val(retlrDetails[0].Creditlmt);
                            $('#txtlat').val(retlrDetails[0].Latitude);
                            $('#txtlong').val(retlrDetails[0].Longitude);
                            $('#txtEmail').val(retlrDetails[0].Email);
                            $('#txt_pan_no').val(retlrDetails[0].PanNo);
                            $('#txt_freezer_tag_no').val(retlrDetails[0].FreezerTagNo);
                            $('#frezdate').val(retlrDetails[0].Frezdate);
                            $('#txtsubName').val(retlrDetails[0].Sub_Name);
                            $('#txtSecMobile').val(retlrDetails[0].SecMobile_No);
                            $('#txtAadhar').val(retlrDetails[0].Aadhar_No);
                            $('#ddlretyp option[value="' + retlrDetails[0].RetType + '"]').attr("selected", "selected");
                            $('#ddltax option[value="' + retlrDetails[0].Taxgrp + '"]').attr("selected", "selected");
                            $('#ddlchannel option[value="' + retlrDetails[0].DrSpec + '"]').attr("selected", "selected");
                            //$('#DDL_category option[value="' + retlrDetails[0].DrCategory + '"]').attr("selected", "selected");
                             $('#DDL_category option[value="' + retlrDetails[0].DrClass + '"]').attr("selected", "selected");
							loadSubCategory(retlrDetails[0].DrClass);
                            //$('#DDL_Subcategory option[value="' + retlrDetails[0].DrSubcategory + '"]').attr("selected", "selected");
                            $('#DDL_Subcategory option[value="' + retlrDetails[0].DrSpec + '"]').attr("selected", "selected");
                            $('#ddlTerritory option[value="' + retlrDetails[0].DrTerr + '"]').attr("selected", "selected");
                            $('#ddlClass option[value="' + retlrDetails[0].DrClass + '"]').attr("selected", "selected");
                            $('#ddl_uom option[value="' + retlrDetails[0].Uom + '"]').attr("selected", "selected");
                            $('#DDL_Re_Type option[value="' + retlrDetails[0].DdlReType + '"]').attr("selected", "selected");
                            $('#ddlslb option[value="' + retlrDetails[0].SlbVal + '"]').attr("selected", "selected");
                            $('#ddl_freezer_status option[value="' + retlrDetails[0].FreezerStatus + '"]').attr("selected", "selected");
                            $('#ddl_freez_typ option[value="' + retlrDetails[0].Freezer_Type + '"]').attr("selected", "selected");
                            $('#ddlTcs option[value="' + retlrDetails[0].TcsApp + '"]').attr("selected", "selected");
                            $('#ddlTds option[value="' + retlrDetails[0].TdsApp + '"]').attr("selected", "selected");
                            $('#ddl_state option[value="' + retlrDetails[0].StateCode + '"]').attr("selected", "selected");
                            $('#ddl_Price option[value="' + retlrDetails[0].PriceListName + '"]').attr("selected", "selected");
                            $('#ddl_group option[value="' + retlrDetails[0].DrQuaCode + '"]').attr("selected", "selected");
                            $('#ddlOutletType option[value="' + retlrDetails[0].Outlet_Type + '"]').attr("selected", "selected");
                            $('#ddlDeliveryMode option[value="' + retlrDetails[0].Delivery_Mode + '"]').attr("selected", "selected");                            
                            $('#ddl_dis option[value="' + retlrDetails[0].Dist_Code + '"]').attr("selected", "selected");                            
                            $("input[type='radio'].RblAlt:checked").val(retlrDetails[0].CusAlter);
                            $(document).find('.scrldiv2 .row').empty();
                            for (var i = 1; i < retlrDetails.length; i++) {
                                var txtfield = "txtfield" + i;
                                var txtval = "txtval" + i;
                                if (retlrDetails[i].cname != null) {
                                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" autocomplete="off" id=' + txtfield + ' value="' + retlrDetails[i].cname + '" /></div><div class="col-sm-6 cls"><input type="text" name="value" autocomplete="off" id=' + txtval + ' value="' + retlrDetails[i].cval + '" /></div></div>';
                                    $('.scrldiv2').append(newTr);
                                }
                            }
                            $(document).find('.scrldiv1 .row').empty();
                            for (var i = 1; i < retlrDetails.length; i++) {
                                var txtfield = "txtfield" + i;
                                var txtval = "txtval" + i;
                                if (retlrDetails[i].ftypename != null && retlrDetails[0].FreezerTagNo != retlrDetails[i].ftypeval) {
                                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field1" autocomplete="off" id=' + txtfield + ' value="' + retlrDetails[i].ftypename + '" /></div><div class="col-sm-6 cls"><input type="text" name="value1" maxlength="13" onkeypress="return /[0-9a-zA-Z]/i.test(event.key)" autocomplete="off" id=' + txtval + ' value="' + retlrDetails[i].ftypeval + '" /></div></div>';
                                    $('.scrldiv1').append(newTr);
                                }
                            }
                        },
                        error: function (rs) {
                            alert(rs);
                        }
                    });
                }
            });

            $('#btnClose').on('click', function () {
                var count = 0;
                if ($("#txt_freezer_tag_no").val() != '') {
                    ftparr.push({
                        ffield: 'Freezer Tag No',
                        fval: $("#txt_freezer_tag_no").val()
                    });
                }
                $('.scrldiv1 .row').each(function () {
                    if ($(this).find('input[name="value1"]').val().length < 13) {
                        count = 1;
                    }
                    else {
                        ftparr.push({
                            ffield: $(this).find('input[name="field1"]').val(),
                            fval: $(this).find('input[name="value1"]').val()
                        });
                    }
                });
                if (count == 1) {
                    alert("Freezer Tag No Must be 11digit");
                    return false;
                }
            });
            $('#btnOrderListclose').on('click', function () {
                $('#exampleModal').modal('hide');
            });

            $('#btnOrderListOk').on('click', function () {

                if ($('#OrderList > TBODY > tr').length > 0) {
				
					if ($('#eff_dt').val() == '') { alert('Please Select Effective Date'); return false; };

                    buttoncount = 0; var buttonQty = 0; var MRP_Price = ''; var rate = '';                    
                    $(document).find("#OrderList tbody tr").each(function () {

                        row1 = $(this).closest("tr");
						var indx1 = $(row1).index();
						
                        var pc = row1.find('.ddlProd').val();
                        var pcName = row1.find('.ddlProd :selected').text();                        
                        MRP_Price = $(row1).find('.MRP_Price').val();
                        rate = $(row1).find('.On_Inv_price').val();

                        if (pc == "0" || MRP_Price == '' || rate == '') {
                            buttoncount += 1;
                        }
                    });

                    if (buttoncount > 0) {

                        $('#exampleModal').modal('show');
                        alert('Please fill the data or Remove empty product');
                        return false;
                    }
                    else {
                        $('#exampleModal').modal('hide');
                    }

                }
                else {
                    $('#exampleModal').modal('hide');
                }
            });

            $(document).on('keypress', '.MRP_Price, .rate', function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && e.which != 46) {
                    return false;
                }
            });


            $('#btnsubmit').on('click', function () {

                if ($('#txtName').val() == "") { alert("Enter Retailer Name."); $('#txtName').focus(); return false; }

                var Category = $('#DDL_category option:selected').text();
                if (Category == "---Select---") { alert("Select Category."); $('#DDL_category').focus(); return false; }

                var SubCategory = $('#DDL_Subcategory option:selected').text();
                if (SubCategory == "---Select---") { alert("Select Subcategory."); $('#DDL_Subcategory').focus(); return false; }

                /*
				var clas = $('#ddlClass option:selected').text();
                if (clas == "---Select---") { alert("Select Class."); $('#ddlClass').focus(); return false; }
				*/

                var Rou = $("#ddlTerritory option:selected").text();
                if (Rou == "---Select---") { alert("Select Route."); $('#ddlTerritory').focus(); return false; }

                var dist = $("#ddl_dis option:selected").text();
                if (dist == "---Select---") { alert("Select Distributor."); $('#ddl_dis').focus(); return false; }

                var state = $('#ddl_state option:selected').text();
                if (state == "---Select---") { alert("Select State."); $('#ddl_state').focus(); return false; }

                var retype = $('#ddlretyp option:selected').text();
                if (retype == 'Register') {
                    if ($('#salestaxno').val() == "") {
                        alert("Enter GST No");
                        $('#salestaxno').focus();
                        return false;
                    }
                }

                if ($('#txtMobile').val() == "") {
                    alert("Enter Mobile No");
                    $('#txtMobile').focus();
                    return false;
                }

                var email = $('#txtEmail').val();
                if (email != null && email != "") {
                    if (IsEmail(email) == false) {
                        alert('Invalid Email ID');
                        return false;
                    }
                }

                function IsEmail(email) {
                    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (!regex.test(email)) {
                        return false;
                    } else {
                        return true;
                    }
                }
                if ($('#txtDOW').val() == "") {
                    alert("Enter Contact Person Name");
                    $('#txtDOW').focus();
                    return false;
                }

                if ($('#txtAddress').val() == "") { alert("Enter Address."); $('#txtAddress').focus(); return false; }

                //var PriceList = $('#ddl_Price option:selected').text();
                //if (PriceList == "---Select---") { alert("Select Price List"); $('#ddl_Price').focus(); return false; }

                if ($('#ddlOutletType option:selected').text() == "---Select---") { alert("Select Outlet Type."); $('#ddlOutletType').focus(); return false; }
                if ($('#ddlDeliveryMode option:selected').text() == "---Select---") { alert("Select Outlet Type."); $('#ddlDeliveryMode').focus(); return false; }
               
                if ($('#ddl_freez_typ option:selected').val() != '0') {
                    var frees = $("#txt_freezer_tag_no").val().length;
                    if ($("#txt_freezer_tag_no").val() == '') {
                        alert("Enter Freezer Tag No"); $('#txt_freezer_tag_no').focus(); return false;
                    }
                    else if (frees > 13 || frees < 13) {
                        alert("Freezer Tag No Must be 11digit"); $('#txt_freezer_tag_no').focus(); return false;
                    }
                }

                var csarr = [];
                $('.scrldiv2 .row').each(function () {
                    csarr.push({
                        cfield: $(this).find('input[name="field"]').val(),
                        cval: $(this).find('input[name="value"]').val()
                    });
                });

                $('#Txt_Mil_Pot').keypress(function (event) {
                    return isNumber(event, this)
                });
                function isNumber(evt, element) {
                    var charCode = (evt.which) ? evt.which : event.keyCode
                    if ((charCode != 46 || $(element).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))   // �.� CHECK DOT, AND ONLY ONE.
                        return false;
                    else
                        return true;
                }
                var credit_days = $('#creditdays').val();
				var slabresult = '';
                var slbval = '';
                if (credit_days == "") {
                    slbval = '';
                }
                else {
                    credit_daysInt = parseInt(credit_days);                    
                    // dSlab[i].Code + '">' + dSlab[i].Name
                    for (i = 0; i < dSlab.length; i++) {
                        var slab_arr = dSlab[i].Name.split("-");
                        if (slab_arr[0].includes('above') == true) {
                            slab_arrval = slab_arr[0].split('&');
                            if (credit_daysInt > parseInt(slab_arrval[0])) {
                                slabresult = dSlab[i].Name;
                                slbval = dSlab[i].Code;
                            }
                        }
                        else {
                            if ((credit_daysInt >= parseInt(slab_arr[0]) && credit_daysInt <= parseInt(slab_arr[1])) == true) {
                                slabresult = dSlab[i].Name;
                                slbval = dSlab[i].Code;
                            }
                        }
                    }
                }               

                var DR_Name = $('#txtName').val();                
                var Mobile_No = $('#txtMobile').val();                
                var retail_code = $('#Txt_id').val();
                var erbCode = '';
                if ($('#txtERBCode').val() == '') { erbCode = retail_code; } else { erbCode = $('#txtERBCode').val(); }
                var contact_person = $('#txtDOW').val();
                var DR_Spec = $('#ddlchannel option:selected').val();
                var dr_spec_name = $('#ddlchannel option:selected').text();
                

                //var DR_Spec = "14";
                //var dr_spec_name = "PAN SHOP";

                if (DR_Spec == 0) {
                    DR_Spec = "";
                    dr_spec_name = "";
                }

                var sub_Name = $('#txtsubName').val();
				if (sub_Name == '' || sub_Name == undefined) sub_Name = '';
                var SecMobile_No = $('#txtSecMobile').val();
                var Aadhar_No = $('#txtAadhar').val();
                var retype = $('#ddlretyp option:selected').val();
                var Taxgrp = $('#ddltax option:selected').val();                
                var sales_Tax = $('#salestaxno').val();
                var Tinno = $('#TinNO').val();
                var FssiNo = $('#FssiNo').val();
                var DR_Terr = $('#ddlTerritory option:selected').val();
                var dist_code = $('#ddl_dis option:selected').val();
                //var credit_days = $('#creditdays').val();

                var DR_Class = $('#ddlClass option:selected').val();
                var dr_class_name = $('#ddlClass option:selected').text();
                var drcategory = $('#DDL_category option:selected').val();
                var dscategoryName = $('#DDL_category option:selected').text();
                var drSubcategory = $('#DDL_Subcategory option:selected').val();
                var outstandng = $('#txtoutstanding').val();
                var creditlmt = $('#txtcreditlimit').val();
                var ad = $('#Txt_advanceamt').val();
                var DDL_Re_Type = ($('#DDL_Re_Type option:selected').val() != '0') ? $('#DDL_Re_Type option:selected').val() : '';
                var Milk_pon = $('#Txt_Mil_Pot').val();
                //var slbval = $('#ddlslb option:selected').val();
                var email = $('#txtEmail').val();
                var UOM = $('#ddl_uom option:selected').val();
                var UOM_Name = $('#ddl_uom option:selected').text();
                var Cus_Alter = $("input[type='radio'].RblAlt:checked").val();
                var latitude = $('#txtlat').val();
                var longitude = $('#txtlong').val();
                var Freezer_tag_no = $('#txt_freezer_tag_no').val();
                var Freezer_status = $('#ddl_freezer_status option:selected').val();
                var FreezerType = $('#ddl_freez_typ option:selected').val();
                var pan_no = $('#txt_pan_no').val();
                var Tcs_App = $('#ddlTcs option:selected').val();
                var Tds_App = $('#ddlTds option:selected').val();
                var DR_Address1 = $('#txtAddress').val();
                var DR_Address2 = $('#txtStreet').val();
                var State = $('#ddl_state option:selected').val();
                var ListedDrCode = $('#<%=HiddenFieldRetCode.ClientID%>').val();
                var frezdate = $('#frezdate').val();
                //var PriceList = $('#ddl_Price option:selected').val();
                var QuaVal = $('#ddl_group option:selected').val();
                var QuaName = $('#ddl_group option:selected').text();
                var outletType = $('#ddlOutletType option:selected').val();
                var Delivery_Type = $('#ddlDeliveryMode option:selected').val();

                var retailer_code = $('#lbl_ret_code').text();
                var subDiv = $('#lbl_sub_div').text();
                var filtered_sf = [];
				var Ef_Dt = $('#eff_dt').val();

                $(document).find("#OrderList tbody tr").each(function () {

                    var product_detail_code = $(this).closest("tr").find(".ddlProd option:Selected").val();
                    var product_detail_name = $(this).closest("tr").find(".ddlProd option:Selected").text();
                    var selected_unit_code = $(this).closest("tr").find(".unit option:Selected").val();
                    var selected_unit_Name = $(this).closest("tr").find(".unit option:Selected").text();

                    var mrp_price = $(this).closest("tr").find(".MRP_Price").val();
                    var Given_ret_rate = $(this).closest("tr").find(".On_Inv_price").val();
                    var given_ofInvRate = $(this).closest("tr").find(".Off_Inv_price").val();
                    var given_Net_Price = $(this).closest("tr").find(".Net_Price").val();

                    var hid_Given_ret_rate = $(this).closest("tr").find(".hidden_rate").val();

                    if (Given_ret_rate == "" || Given_ret_rate == undefined) { Given_ret_rate = 0; }
                    if (hid_Given_ret_rate == "" || hid_Given_ret_rate == undefined) { hid_Given_ret_rate = 0; }

                    var sale_unt_code = $(this).closest("tr").find(".Product_Sale_Unit_code").val();
                    var Prd_unit_code = $(this).closest("tr").find(".Product_Unit_code").val();
                    var Prd_sale_erp_code = $(this).closest("tr").find(".Product_sample_erp_code").val();
                    var con_fac = $(this).closest("tr").find(".Default_con_fac").val();
                    var umoweg = $(this).closest("tr").find(".UomWeight").val();

                    if (Given_ret_rate == hid_Given_ret_rate) {
                        var fg = $(this).closest("tr").find(".Active_flg").val();
                    }
                    else {
                        var fg = 1;
                    }

                    if (sale_unt_code == selected_unit_code) {
                        var ret_con_rate = Given_ret_rate;
                    }
                    else if (Prd_unit_code == selected_unit_code) {
                        var ret_con_rate = Prd_sale_erp_code * Given_ret_rate;
                    }
                    else {
                        var Unit_conversion = (umoweg / con_fac);
                        //var ret_con_rate = umoweg / Unit_conversion * Given_ret_rate;
                        var ret_con_rate = umoweg / 1000 * Given_ret_rate;

                    }

                    if (mrp_price == 0 || mrp_price == "" || mrp_price == null || mrp_price == undefined) { mrp_price = 0; }
                    if (Given_ret_rate == 0 || Given_ret_rate == "" || Given_ret_rate == null || Given_ret_rate == undefined) { Given_ret_rate = 0; }

                    var get_differnece = mrp_price - Given_ret_rate;

                    if (get_differnece <= 3) {
                        var diff = 3;
                    }
                    else if (get_differnece <= 5) {
                        var diff = 5;
                    }
                    else {
                        var diff = 6;
                    }

                    filtered_sf = Hierarchy_Detail.filter(function (r) {
                        return (r.Price_slap == diff);
                    })

                    if (filtered_sf.length > 0) { var Fil_Sf = filtered_sf[0].Sf_Code; } else { var Fil_Sf = 0; }

                    if (Given_ret_rate != 0 || mrp_price != 0) {
                        itm1 = {};
                        itm1.product_detail_code = product_detail_code;
                        itm1.product_detail_name = product_detail_name;
                        itm1.MRP_Price = $(this).closest("tr").find('.MRP_Price').val();
                        itm1.Ret_Rate = Given_ret_rate;
                        itm1.OffInvPrice = given_ofInvRate;
                        itm1.NetPrice = given_Net_Price;
                        itm1.Unit = selected_unit_Name;
                        itm1.UnitCode = selected_unit_code;
                        itm1.Ret_Rate_in_piece = ret_con_rate;
                        itm1.Dis_Rate = 0;
                        itm1.Dis_Rate_in_piece = 0;
                        itm1.Retailer_Code = 0;
                        itm1.flag = fg;
                        itm1.Approvel_To = Fil_Sf;
                        BodyData.push(itm1);
                    }
                });

                data = {};
                /*var sub_Name = $('#txtsubName').val();
                var SecMobile_No = $('#txtMobile').val();
                var Aadhar_No = $('#txtAadhar').val();*/
                
                data = { "DrCode": ListedDrCode, "DivCode": "<%=Session["div_code"]%>", "SfCode": "<%=Session["sf_code"]%>", "SfType": "<%=Session["sf_type"]%>", "DrName": DR_Name, "RetailCode": retail_code, "MobileNo": Mobile_No, "ErbCode": erbCode, "ContactPerson": contact_person, "DrSpec": DR_Spec, "DrSpecName": dr_spec_name, "SalesTax": sales_Tax, "RetType": retype, "Taxgrp": Taxgrp, "Tinno": Tinno, "FssiNo": FssiNo, "DrTerr": DR_Terr, "CreditDays": credit_days, "DrClass": DR_Class, "DrClassName": dr_class_name, "DrCategory": drcategory, "DrCategoryName": dscategoryName, "Outstandng": outstandng, "Creditlmt": creditlmt, "Ad": ad, "DdlReType": DDL_Re_Type, "MilkPon": Milk_pon, "SlbVal": slbval, "Email": email, "Uom": UOM, "UomName": UOM_Name, "CusAlter": Cus_Alter, "Latitude": latitude, "Longitude": longitude, "FreezerTagNo": Freezer_tag_no, "FreezerStatus": Freezer_status, "FreezerType": FreezerType, "PanNo": pan_no, "TcsApp": Tcs_App, "TdsApp": Tds_App, "DrAddress1": DR_Address1, "DrAddress2": DR_Address2, "CArr": csarr, "StateCode": State, "Frezdate": frezdate, "FrzArr": ftparr, "DrSubcategory": drSubcategory, "DrQuaCode": QuaVal, "DrQuaName": QuaName, "BodyBulkData": BodyData, "outletType": outletType, "Delivery_Mode": Delivery_Type, "Dist_Code" : dist_code,"Sub_Name" :sub_Name,"SecMobile_No" :SecMobile_No,"Aadhar_No":Aadhar_No };

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerAdd.aspx/saveRetailer",
                    data: "{'data':'" + JSON.stringify(data) + "','Rate_Effective_Date':'" + Ef_Dt + "'}",
                    dataType: "json",
                    success: function (data) {
                        var iReturn = data.d;
                        if (iReturn == "Created Successfully" || iReturn == "Updated Successfully") {
                            //save_price();
                            alert(iReturn);
                            if ('<%=Session["sf_type"]%>' == '4') {
                                window.location.href = '/Stockist/Retailer_List.aspx';
                            }
                            else {
                                window.location.href = '../Retailer_Details.aspx';
                            }
                            clearfields();
                        }
                        else
                            alert(iReturn);
                    },
                    error: function (rs) {
                        alert(rs);
                    }
                });
            });

        </script>
    </body>
    </html>
</asp:Content>
