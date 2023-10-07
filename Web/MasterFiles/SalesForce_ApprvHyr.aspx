<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SalesForce_ApprvHyr.aspx.cs" Inherits="MasterFiles_SalesForce_ApprvHyr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title></title>
        <a href="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.js.map"></a>
        <link href="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.css" rel="stylesheet" />
        <script src="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.js" type="text/javascript""></script>
        <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
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
                width: 100%;
                border: 1px solid #D5D5D5 !important;
                padding: 6px 6px 7px !important;
            }

                select:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }

            .nav-tabs {
                padding-left: 5px;
                border-bottom: 1px solid white;
            }

            .active > a {
                border-bottom: 2px solid #86d993 !important;
            }

            .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
                border: 1px solid #f0f3f4 !important;
                color: #555;
                cursor: default;
                background-color: #f0f3f4 !important;
                border-bottom: 2px solid #86d993 !important;
                /* border-bottom-color: transparent; */
            }

            /*li.different {
                border: none;
                position: relative;
            }

                li.different:hover {
                    border: none;
                }



            .different::after {
                content: '';
                position: absolute;
                width: 0px;
                height: 5px;
                left: 50%;
                bottom: 0;
                background-color: #86d993;
                transition: all ease-in-out .2s;
            }

            .different:hover::after {
                width: 100%;
                left: 0;
            }

            nav {
                width: 100%;
                height: 50px;
                position: fixed;
                top: 0;
                left: 0;
                background-color: #333333;
            }*/

            li {
                margin-bottom: 10px;
            }

            .cool-link {
                display: inline-block;
                color: #86d993;
                text-decoration: none;
            }

                .cool-link::after {
                    content: '';
                    display: block;
                    width: 0;
                    height: 2px;
                    background: #86d993;
                    transition: width .3s;
                }

                .cool-link:hover::after {
                    width: 100%;
                }

            /*#wrapper {
                width: 100%;
                height: 100%;
                overflow: hidden;
                display: block;
                float: left;
            }

                #wrapper ul {
                    diplay: block;
                    width: 50%;
                    margin: 0 auto;
                    height: 100%;
                    text-align: center;
                }

                #wrapper li {
                    display: block;
                    float: left;
                    text-align: center;
                    width: 25%;
                    height: 100%;
                    transition: all ease-in-out .2s;
                }
				.dropdown-menu {
    position: absolute;
    top: 100%;
    left: 0;
    z-index: 1000;
    display: none;
    float: left;
    min-width: 160px;
    padding: 5px 0;
    margin: 2px 0 0;
    font-size: 14px;
    list-style: none;
    background-color: #fff;
    border: 1px solid #ccc;
    border: 1px solid rgba(0,0,0,0.15);
    border-radius: 4px;
    -webkit-box-shadow: 0 6px 12px rgba(0,0,0,0.175);
    box-shadow: 0 6px 12px rgba(0,0,0,0.175);
    background-clip: padding-box;
    height: 100px;
}
                #wrapper a {
                    display: block;
                    float: left;
                    color: #86d993;
                    width: 100%;
                    padding: 0 .5em;
                    line-height: 50px;
                    text-decoration: none;
                    font-weight: bold;
                }*/
        </style>
    </head>
    <body>
        <div class="row">
            <div class="col-lg-12 sub-header">
                Employee Master
                <div class="row">
                    <div class="col-lg-11" id="btnback">
                        <i class="fa fa-arrow-left btn btn-circle" style="float: right; color: #3f7b96; /* padding: 10px; */box-shadow: 1px 1px 6px 2px grey;"></i>
                    </div>
                    <div class="col-lg-1">
                        <button style="/* float: right; *//* padding: 10px; */" type="button" id="btnsubmit" class="btn btn-primary">Submit</button>
                    </div>
                </div>
            </div>
            <div id="re_direct" style="display:none;">
                <label style="line-height: 30px;position: absolute;right: 195px;/* font-size: x-small; */">Redirect Pages :</label>
                <a href="Allowance_Entry_New.aspx" style="line-height: 30px;position: absolute;right: 118px;/* font-size: x-small; */">Allowance</a>
                <a href="TravelMod.aspx" style="line-height: 30px;position: absolute;right: 26px;/* font-size: x-small; */">Travel Mode</a>
            </div>
        </div>
        <ul class="nav nav-tabs">
            <li class="active cool-link"><a data-toggle="tab" id="tab_1">Employee Details</a></li>
            <li class="cool-link"><a data-toggle="tab" id="tab_2">Approval Hierarchy</a></li>
        </ul>
        <form action="#" style="background: #ffffff; box-shadow: 0px 3px 12px rgba(0, 0, 0, 0.25); border-radius: 8px; margin-top: 25px;">
            <div class="container" id="Emp_Det">
                <div class="col-md-4">
                    <table id="upltbl" style="border-collapse: collapse;">
                        <tr>
                            <td colspan="2" align="center" style="padding-top: 57px; padding-right: 16px; /* padding-bottom: 57px; */padding-left: 16px;">
                                <img alt="upimg" id="upimg" style="width: 80%; /* height: 0%; */" src="https://www.w3schools.com/w3css/img_avatar3.png" />
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
                        <tr>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Status</label>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="spstat" class="label label-success">Active</span>
                                <a id="ffstatus" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <select id="fstat" style="width: 62% !important; display: none;">
                                    <option value="0">Active</option>
                                    <option value="2">Deactive</option>
                                    <option value="1">Vacant</option>
                                    <option value="3">Block</option>
                                </select>
                            </td>
                        </tr>
                        <tr id="resroew" style="display: none;">
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Resignation Date</label></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="rffdttxt">Jan 01 2020</span>
                                <a id="rdate" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <input type="date" id="redate" style="display: none;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Type</label></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="fftyp">Non-Field</span>
                                <a id="fftype" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <select id="ddlftyp" style="width: 62% !important; display: none">
                                    <option value="FF">Field</option>
                                    <option value="VS">Van Sales</option>
                                    <option value="NF">Non-Field</option>
                                    <option value="I">Inseminator</option>
                                </select>
                            </td>
                        </tr>
                        <tr id="sftyptr">
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Level</label></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="sfftyp">Base Level</span>
                                <a id="sfftype" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <select id="ddlsftyp" style="width: 62% !important; display: none">
                                    <option value="1">Base Level</option>
                                    <option value="2">Manager</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Joining Date</label><span class="ffJdate" style="color: red;">*</span></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="ffdttxt">Jan 01 2020</span>
                                <a id="ffJdate" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <input type="date" id="fjdate" style="display: none;" min="1900-01-01" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Reporting To</label></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="ffrpttxt">Admin</span>
                                <a id="ffrpt" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc; display: none;">
                                <select id="repto" data-dropup-auto="false"></select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-8">
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Account Setting</h3>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>Email</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfemail" type="email" required="required" />
    			        <small id="EmailHelp" style="padding-left: 7px; display: none;" class="text-danger">Please provide a valid email address</small>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>UserName</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="input-group input-group-sm mb-3" style="display: flex">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text" style="padding: 5px 2px 5px 5px; background: #868383; color: white; border-radius: 4px 0px 0px 4px;" id="fixUsrn"></div>
                                            </div>
                                            <input type="text" class="form-control" id="sfusrname" onkeypress="return AvoidSpace(event)" style="border-radius: 0px 4px 4px 0px !important;" aria-label="Small" aria-describedby="inputGroup-sizing-sm" />
                                        </div>
                                    </div>
                                    <div class="col-sm-6" style="padding-left: 30px">
                                        <input type="checkbox" id="usrauto" name="userauto" value="Yes" style="width: auto; margin: 8px 0px 8px 8px;" />
                                        <span style="font-size: 13px;">Auto-generate</span>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">

                                <label>Password</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <div class='image'>
                                    <span class='ab'><i class="fa fa-eye" onclick="myFunction()"></i></span>
                                    <input class="col-xs-5" id="sfpwd" type="password" required="required" oninput="validate(this)" />
                                    <small id="sfpwdHelp" style="padding-left: 7px; display: none;" class="text-danger">Password must contain: Min 6 and Max 12 Characters 1 UpperCase(A-Z),1 LowerCase(a-z) , 1 Number(0-9),1 Special Character(!@#$%^&*_=+-) Exp:PaSs@1</small>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>ConfirmPassword</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-5" id="sfcpwd" type="password" required="required" />
                                <small id="passwordHelp" style="padding-left: 7px; display: none;" class="text-danger">Password doesn't Match</small>
                            </td>
                        </tr>
                    </table>
                    <div class="row">
                        <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Profile Setting &nbsp
                            <button type="button" class="btn btn-warning btn-circle btn-lg hidden" data-toggle="modal" data-target="#exampleModal">+</button></h3>
                    </div>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>First Name</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-9" type="text" id="sffirstname" oninput="validate(this)" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Last Name</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sflastname" type="text" oninput="validate(this)" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Employee ID</label><span class="sfempid"  style="color: red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-5" type="text" id="sfempid" oninput="validate(this)" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Date of Birth</label>
                            </td>
                            <td>
                                <input class="col-xs-5" id="sfdob" type="date" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Designation</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <select id="desg" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Division</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <select id="division" data-dropup-auto="false" multiple>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Department</label>
                            </td>
                            <td>
                                <select id="depts" class="selectpicker" data-live-search="true" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>State</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <select id="states" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>HeadQuarters</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <select id="hqs" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Territory</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <select id="routes" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>District</label>
                            </td>
                            <td>
                                <select id="ddldistrict" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                    </table>
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Contact Setting &nbsp
                            <button type="button" class="btn btn-warning btn-circle btn-lg hidden" data-toggle="modal" data-target="#modalcontact">+</button></h3>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>Mobile Phone</label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfmobil" autocomplete="off" type="number" oninput="validate(this)" maxlength="25" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Door No. & Street</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfaddr" type="text" oninput="validate(this)" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Area & Locality</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfarea" type="text" oninput="validate(this)" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>City</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfcity" type="text" oninput="validate(this)" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Pincode</label>
                            </td>
                            <td>
                                <input class="col-xs-5" id="sfpincode" type="text" oninput="validate(this)" maxlength="6" autocomplete="off" />
                            </td>
                        </tr>
                        <tr id="depo">
                            <td align="right">
                                <label>Depot</label>
                            </td>
                            <td>
                                <input class="col-xs-5" id="sfdepot" type="text" oninput="validate(this)" autocomplete="off" />
                            </td>
                        </tr>
                    </table>

                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial';" class="hidden">Additional Fields Setting &nbsp</h3>
                    <table id="additionalField" class="hidden">
                        <tbody>
                        </tbody>
                    </table>

                </div>
            </div>
            <div style="display: none;" id="tab_3">
                <ul class="nav nav-tabs">
                    <li class="active cool-link"><a data-toggle="tab" value="1" id="tab_Exp">Expense</a></li>
					
                    <li class="cool-link"><a data-toggle="tab" value="2" id="tab_Tp">Tour Plan</a></li>
                </ul>
                <div style="margin: 25px;">
                    <div class="row">
                        <div class="col-sm-4">
                            <select id="selmgr" style="width: 50%;" data-dropupauto="true"></select>
                        </div>
                        <div class="col-sm-2">
                            <button type="button" style="background-color: #1a73e8; text-decoration-color: white;" id="addLvl" class="btn btn-primary">+</button>
                        </div>
                        <%--  <div class="col-sm-6" style="text-align: end;">
                            <button id="btnsv" class="btn btn-primary">SAVE</button>
                            <button id="btnsvTp" style="display: none;" class="btn btn-primary">SAVE</button>
                        </div>--%>
                    </div>
                    <div class="row" style="padding: 10px;">
                        <div id="Exp_det" class="col-lg-8">
                            <div class="col-lg-offset-4">
                                <ul id="draggablePanelList" class="list-unstyled">
                                </ul>
                            </div>
                        </div>
                        <div id="Tp" class="col-lg-8">
                            <div class="col-lg-offset-4">
                                <ul id="draggablePanelList1" class="list-unstyled">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="modal fade" id="exampleModal" tabindex="-1" style="z-index: 1000000; background-color: transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <%--<button type="button" id="btnpls" class="btn btn-info btn-circle btn-lg hidden" style="float: right">+</button>--%>
                            <h5 class="modal-title" id="exampleModalLabel">Profile Fields</h5>
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
                                    <div class="scrldiv" style="height: 120px;">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <input name="field" type="text" autocomplete="off" />
                                            </div>
                                            <div class="col-sm-6">
                                                <input name="value" type="text" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.panel-body -->
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="modalcontact" tabindex="-1" style="z-index: 1000000; background-color: transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="bttnpls" class="btn btn-info btn-circle btn-lg hidden" style="float: right">+</button>
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
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">Name</div>
                                        <div class="col-sm-6">Value</div>
                                    </div>
                                    <div class="scrldiv2" style="height: 120px;">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <input name="field" type="text" autocomplete="off" />
                                            </div>
                                            <div class="col-sm-6">
                                                <input name="value" type="text" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.panel-body -->
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div>
            </div>
        </form>
        <script type="text/javascript">
            var propic_name = ' ';
            var AddFields = [], pndng = [];
            var sfcode = '', ArrLen = 1, TpArrLen = 1;
            var Itype = '1', typ = '';
            var SfDetails = [], appSfHyr = [], appSfHyrTP = [], appSfHyrExp = [];
            var usratuogenerate = false; var SFDesg = [];
            var SFStates = []; var SFDivi = [], sfApprDets = [];
            var autogenusrname = ''; var SFDivisname = ''; var fstat1 = 0;
            var today = new Date(); var filtmgr = [];
	    var sf ='';
            function getworkedwith($sffc) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getSFWorkedWith",
                    data: "{'sfcode':'" + $sffc + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#selmgr').empty();
                        //$('#selmgr').selectpicker('refresh');
                        filtmgr = JSON.parse(data.d);
						$('#selmgr').append('<option value="0" >Nothing Selected</option>');
                        for ($i = 0; $i < filtmgr.length; $i++) {
							FilArr = appSfHyrExp.filter(function (a) {
                                return (filtmgr[$i].SF_Code.indexOf(a.SF) > -1)
                            });
							if(FilArr.length==0)
								$('#selmgr').append('<option value="' + filtmgr[$i].SF_Code + '">' + filtmgr[$i].SF_Name + '</option>');
							
                        }
						if ('<%=sf_code%>' != '') {
                             $('#selmgr  option[value=' + '<%=sf_code%>' + ']').remove();
                        }
                        $('#selmgr').selectpicker({
                            liveSearch: true
                        });
                        $('#selmgr').selectpicker('refresh');
                    }
                });
            }
            function getmgrExpensePnding(sfcode) {
                <%--$.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getpnding",
                    data: "{'sf':'" + sfcode + "'}",
                    dataType: "json",
                    success: function (data) {
                        pndng = JSON.parse(data.d) || [];
                    },
                    error: function (res) {
                        alert(res);
                    }
                });--%>
            }

            function getApprSFDets($appTyp) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getApprSFDets",
                    data: "{'SF':'" + sfcode + "','div':'<%=Session["div_code"]%>','apprTyp':'" + $appTyp + "'}",
                    dataType: "json",
                    success: function (data) {
                        sfApprDets = JSON.parse(data.d);
                        //if (sfApprDets.length == 0) {
                        //    alert('This user doesn\'t have hierarchy list!!!');                             
                        //}
                        typ = ($appTyp == 2) ? '1' : '';
                        //$('#draggablePanelList').empty();                        
                        $('#draggablePanelList' + typ).empty();
                        if (sfApprDets.length > 0) {
                            for ($i = 0; $i < sfApprDets.length; $i++) {
                                $('#draggablePanelList' + typ).append(`<li class='panel panel-info' style='margin-bottom: 1rem;'><div class='panel-heading' tsf='${sfApprDets[$i].SF}'><span class="apsf">${sfApprDets[$i].SFNM}</span><i class="fa fa-trash-o" style="float: right;line-height: 20px;margin-left: 2rem;cursor:pointer !important"></i>&nbsp;<span class="aplvl" style='float:right;'>${sfApprDets[$i].Lvl}</span></div></li>`);
                                //removeLowersf(sfApprDets[$i].SF);
                                $('#selmgr > option').each(function () {
                                    if ($(this).val() == sfApprDets[$i].SF) {
                                        $(this).remove();
                                        $('#selmgr').selectpicker('refresh');
                                    }
                                });
                            }
                            ArrLen++;
                            drag_arr();
                        }
                    }
                });
            }
            function Refreshddl(appSfHyr1) {
                var sf = '', appArr = [];
                $('#selmgr > option').each(function () {
                    sf = $(this).val();
                    if ($(this).val() != '') {
                        appArr = appSfHyr1.filter(function (element) {
                            return (element.SF == sf && element.Typ == typ);
                        });
                    }
                    if (appArr.length > 0) {
                        $(this).remove();
                        $('#selmgr').selectpicker('refresh');
                    }
                });
            }
            function drag_arr() {
                if ($('#draggablePanelList' + typ).length > 0) {
					appSfHyrExp=[];
                    $('#draggablePanelList' + typ).find('li').each(function (key, val) {
                        appHyr = {};
                        appHyr.SF = $(this).find('div').attr('tsf');
                        appHyr.Typ = typ;
                        appHyr.SFN = $(this).find('.apsf').text();
                        appHyr.LvlTxt = $(this).find('.aplvl').text();
                        appHyr.Lvl = parseInt($(this).find('.aplvl').text().slice($(this).find(".aplvl").text().length - 1));
                        if (appSfHyrExp.length > 0) {
                            FilArr = appSfHyrExp.filter(function (a) {
                                return (appHyr.SF.indexOf(a.SF) > -1)
                            });
                            if (FilArr.length == 0) {
                                typ == '' ? appSfHyrExp.push(appHyr) : appSfHyrTP.push(appHyr);
                            }
                            //for (var i = 0; i < appSfHyrExp.length; i++) {
                            //    if (appSfHyrExp[i].SF != appHyr.SF)
                            //        typ == '' ? appSfHyrExp.push(appHyr) : appSfHyrTP.push(appHyr);
                            //    else {
                            //        typ == '' ? appSfHyrExp.push(appHyr) : appSfHyrTP.push(appHyr);
                            //    }
                            //}
                        }
                        else {
                            typ == '' ? appSfHyrExp.push(appHyr) : appSfHyrTP.push(appHyr);
                        }

                    });
                    //Refreshddl(appSfHyrExp);
                }
            }
            function removeIndx(sf, appSfHyr_) {
                appSfHyr_ = $.grep(appSfHyr_, function (element, index) {
                    return element.SF !== sf;
                });
                typ == '' ? appSfHyrExp = appSfHyr_ : appSfHyrTP = appSfHyr_;
            }
            function loadAdditionalFields() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getEmployeeFieldSettings",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        AddFields = JSON.parse(data.d) || [];
                        if (AddFields.length > 0) {
                            $('.scrldiv').empty();
                            for (var i = 0; i < AddFields.length; i++) {
                                if (AddFields[i].Field_Type == 'Text') {
                                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" mandatory="' + AddFields[i].Mandatory + '" value="' + AddFields[i].Field_name + '" autocomplete="off" /></div><div class="col-sm-6 cls"><input type="' + AddFields[i].Field_Type + '" name="value" id="' + AddFields[i].Field_Code + '" autocomplete="off" /></div></div>';
                                }
                                $('.scrldiv').append(newTr);
                            }
                        }
                    }
                });
            }
            function loaddivision() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getDivision",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFDivi = JSON.parse(data.d) || [];
                        if (SFDivi.length > 0) {
                            var divi = $("#division");
                            divi.empty();
                            for (var i = 0; i < SFDivi.length; i++) {
                                divi.append($('<option value="' + SFDivi[i].subdivision_code + '">' + SFDivi[i].subdivision_name + '</option>'));
                            }
                        }
                    }
                });
                $('#division').selectpicker({
                    liveSearch: true
                });
            }
            function getDivisname(fl) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getDivisionsname",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFDivisname = JSON.parse(data.d);

                        $('#fixUsrn').text(SFDivisname[0].Division_SName + "-");
                        if (fl == 1) $('#sfusrname').val(SFDivisname[0].uname);
                    }
                });
            }

            getDivisname(0);
            function loaddesignation() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getDesignation",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFDesg = JSON.parse(data.d) || [];
                        var sf_des = SFDesg.filter(function (a) {
                            return a.dtype == 1;
                        });
                        if (sf_des.length > 0) {
                            var desg = $("#desg");
                            desg.empty().append('<option selected="selected" value="0">Select Designation</option>');
                            for (var i = 0; i < sf_des.length; i++) {
                                desg.append($('<option value="' + sf_des[i].dcode + '">' + sf_des[i].dname + '</option>'));
                            }
                        }
                    }
                });
                $('#desg').selectpicker({
                    liveSearch: true
                });
            }
            function loadhq() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/GetHQDetails",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var SFHQs = JSON.parse(data.d) || [];
                        if (SFHQs.length > 0) {
                            var sfhq = $("#hqs");
                            sfhq.empty().append('<option selected="selected" value="0">Select HQ</option>');
                            for (var i = 0; i < SFHQs.length; i++) {
                                sfhq.append($('<option value="' + SFHQs[i].HQ_ID + '">' + SFHQs[i].HQ_Name + '</option>'));
                            }
                        }
                    }
                });
                $('#hqs').selectpicker({
                    liveSearch: true
                });
            }
            function loaddepts(ssdiv) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getDepts",
                    data: '{"divcode":"<%=Session["div_code"]%>","subdiv":"' + ssdiv + '"}',
                    dataType: "json",
                    success: function (data) {
                        var SFDepts = JSON.parse(data.d) || [];
                        if (SFDepts.length > 0) {
                            var dept = $("#depts");
                            dept.empty().append('<option selected="selected" value="0">Select Department</option>');
                            for (var i = 0; i < SFDepts.length; i++) {
                                dept.append($('<option value="' + SFDepts[i].DeptID + '">' + SFDepts[i].DeptName + '</option>'))
                            }
                        }
                    }
                });
                $('#depts').selectpicker('refresh');
            }
            function loadstates() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getStates",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFStates = JSON.parse(data.d) || [];
                        if (SFStates.length > 0) {
                            var states = $("#states");
                            states.empty().append('<option selected="selected" value="0">Select State</option>');
                            for (var i = 0; i < SFStates.length; i++) {
                                states.append($('<option value="' + SFStates[i].scode + '">' + SFStates[i].sname + '</option>'))
                            }
                        }
                    }
                });
                $('#states').selectpicker({
                    liveSearch: true
                });
            }
            function loadDistrict() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getDistrict",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        let sfdistrict = JSON.parse(data.d) || [];
                        if (sfdistrict.length > 0) {
                            let sdistrict = $("#ddldistrict");
                            sdistrict.empty().append('<option selected="selected" value="0">Select District</option>');
                            for (var i = 0; i < sfdistrict.length; i++) {
                                sdistrict.append($('<option value="' + sfdistrict[i].Dist_code + '">' + sfdistrict[i].Dist_name + '</option>'))
                            }
                        }
                    }
                });
                $('#ddldistrict').selectpicker({
                    liveSearch: true
                });
            }
            function loadReporting() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/GetReportingDetails",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var SFRep = JSON.parse(data.d) || [];
                        if (SFRep.length > 0) {
                            var sfrepto = $("#repto");
                            sfrepto.empty();
                            for (var i = 0; i < SFRep.length; i++) {
                                if (sfcode != SFRep[i].Sf_Code) {
                                    sfrepto.append($('<option value="' + SFRep[i].Sf_Code + '">' + SFRep[i].sf_name + '</option>'))
                                }
                            }
                        }
                    }
                });
                $('#repto').selectpicker({
                    liveSearch: true
                });
                //$('#repto').closest('td').find('button').hide();
            }
            function loadTerritory() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getTerritory",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var SFTer = JSON.parse(data.d) || [];
                        if (SFTer.length > 0) {
                            var tert = $("#routes");
                            tert.empty().append('<option selected="selected" value="0">Select Territory</option>');
                            for (var i = 0; i < SFTer.length; i++) {
                                tert.append($('<option value="' + SFTer[i].tcode + '">' + SFTer[i].tname + '</option>'))
                            }
                        }
                    }
                });
                $('#routes').selectpicker({
                    liveSearch: true
                });
            }
            function filtDesg($de) {
                $desgfilt = SFDesg.filter(function (a) {
                    return a.dtype == $de;
                });
                if ($desgfilt.length > 0) {
                    var desg = $("#desg");
                    desg.empty().append('<option selected="selected" value="0">Select Designation</option>');
                    for (var i = 0; i < $desgfilt.length; i++) {
                        desg.append($('<option value="' + $desgfilt[i].dcode + '">' + $desgfilt[i].dname + '</option>'));
                    }
                }
                $('#desg').selectpicker('refresh');
            }
            function clearfields() {
                $(document).find('input[name="field"]').val('');
                $(document).find('input[name="value"]').val('');
                $(document).find('.cls').closest(".row").remove();
                $(document).find('.cls').closest(".row").remove();
                $('#sfemail').val('');
                $('#sfusrname').val('');
                $('#sfpwd').val('');
                $('#sfcpwd').val('');
                $('#sffirstname').val('');
                $('#sflastname').val('');
                $('#sfempid').val('');
                $('#sfdob').val('');
                $('#desg').val();
                $('#depts').val();
                $('#states').val();
                $('#hqs').val();
                $('#routes').val();
                $('#sfmobil').val('');
                $('#sfaddr').val('');
                $('#sfcity').val('');
                $('#sfarea').val('');
                $('#sfpincode').val('');
                $('#fstat').val();
                $('#ddlftyp').val();
                $('#fjdate').val('');
                $('#repto').val();
                $('#sfdepot').val('');
                $('#redate').val('');
            }
            function myFunction() {
                var x = document.getElementById("sfpwd");
                if (x.type === "password") {
                    x.type = "text";
                } else {
                    x.type = "password";
                }
            }
            function Save(appFSFC, sXMl, appHyrTyp, appHyrNm) {
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    data: "{'sf':'" + appFSFC + "','Div':'<%=Session["div_code"].ToString().TrimEnd(',')%>','Appr_Type':'" + appHyrTyp + "','Appr_Name':'" + appHyrNm + "','cusxml':'" + sXMl + "'}",
                    url: "SalesForce_ApprvHyr.aspx/svApprHierarchy",
                    dataType: "json",
                    success: function (data) {
                        var rmsg = data.d;
                        if (rmsg == 'Error Occured')
                            alert('Error occured in approval hierarchy');
                        //window.location.href = 'SalesForce_List.aspx';
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function GetCustomFormsFields() {
                var MasFrms = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/GetCustomFormsFieldsList",
                    data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'1'}",
                    dataType: "json",
                    success: function (data) {

                        MasFrms = JSON.parse(data.d) || [];
                        console.log(MasFrms);
                        var str = "";
                        for (var i = 0; i < MasFrms.length; i++) {
                            var FldType = MasFrms[i].Fld_Type;
                            var Mandate = MasFrms[i].Mandate;
                            //alert(FldType);
                            if ((FldType == "T" || FldType == "TAS")) {
                                if (Mandate == "Yes")
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "<span style='color: red;'>*</span></td>";
                                else
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "</td>";

                                str += "<td><input type='text' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].Field_Col + "  class='col-xs-9' maxLength=" + MasFrms[i].Fld_Length + " /></td></tr>";
                            }
                            else if (FldType == "TAM") {
                                if (Mandate == "Yes")
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "<span style='color: red;'>*</span></td>";
                                else
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "</td>";
                                str += "<td><textarea type='text' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].Field_Col + "  class='col-xs-9' maxLength=" + MasFrms[i].Fld_Length + "></textarea></td></tr>";
                            }
                            else if (FldType == "NC") {
                                if (Mandate == "Yes")
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "<span style='color: red;'>*</span></td>";
                                else
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "</td>";
                                str += "<td>";
                                str += "<div class='row'>";
                                str += "<div class='col-sm-6'>";
                                str += "<div class='input-group input-group-sm mb-3' style='display: flex'>";
                                str += "<div class='input-group-prepend'>";
                                str += "<div class='input-group-text' style='width:50px; padding: 5px 2px 5px 5px; background: #868383; color: white; border-radius: 4px 0px 0px 4px;' id='NCS'>" + MasFrms[i].Fld_Symbol + "</div>";
                                str += "</div>";
                                str += "<input type='number' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].Field_Col + "  class='col-xs-9' maxLength=" + MasFrms[i].Fld_Length + "/>";
                                str += "</div>";
                                str += "</div>";
                                str += "</div>";
                                str += "</td></tr>";

                            }
                            else if (FldType == "NP") {
                                if (Mandate == "Yes")
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "<span style='color: red;'>*</span></td>";
                                else
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "</td>";
                                str += "<td><input type='number' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].Field_Col + "  class='col-xs-9' maxLength=" + MasFrms[i].Fld_Length + " /></td></tr>";
                            }
                            else if (FldType == "N") {
                                if (Mandate == "Yes")
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "<span style='color: red;'>*</span></td>";
                                else
                                    str += "<tr><td align='right'>" + MasFrms[i].Field_Name + "</td>";
                                str += "<td><input type='number' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].Field_Col + "  class='col-xs-9' maxLength=" + MasFrms[i].Fld_Length + "/></td></tr>";
                            }
                        }

                        //var tr = $(tr).html(str);
                        //$(tr).html(str);
                        $("#additionalField tbody").append(str);
                        //$("#additionalField tbody").append('<tr class="alcode">' + str + '</tr>');

                    },
                    error: function (data) {
                        alert(JSON.stringify(data.d));
                    }
                });

            }

            function getSFCODE(sftype) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_ApprvHyr.aspx/getNewSfCode",
                    data: "{'typ':'" + sftype + "'}",
                    dataType: "json",
                    success: function (data) {
                        sfcode = data.d;
                        console.log(sfcode);
                        Itype = '0';
                        $('#ffstatus').closest('td').css('display', 'none');
                        $('#fftype').closest('td').css('display', 'none');
                        $('#sfftype').closest('td').css('display', 'none');
                        $('#ffJdate').closest('td').css('display', 'none');
                        $('#rdate').closest('td').css('display', 'none');
                        $('#ffrpt').closest('td').css('display', 'none');

                        $('#sftyptr').show();
                        $('#fstat').show();
                        $('#ddlftyp').show();
                        $('#ddlsftyp').show();
                        $('#fjdate').show();
                        //$('#fjdate').attr('min', today);
                        $('#redate').show();
                        $('#repto').closest('td').show();
                    },
                    error: function (rs) {
                        alert(rs);
                    }
                });
            }
            function removeLowersf(hrsf) {
                var PanelArr = filtmgr.filter(function (a) {
                    return a.SF_Code == hrsf;
                });
				if(PanelArr.length>0){
					FilArr = filtmgr.filter(function (a) {
						return a.steps < PanelArr[0].steps
					});
					for (var i = 0; i < FilArr.length; i++) {
						$('#selmgr option[value=' + FilArr[i].SF_Code + ']').remove();
						$('#selmgr').selectpicker('refresh');
					}
				}
            }
			
			 

            $(document).ready(function () {
                $('#btnsubmit').text('Next');
                GetCustomFormsFields();


                if ('<%=Session["div_code"]%>' == '107') {
                    $('#depo').show();
                }
                else {
                    $('#depo').hide();
                }
                sfcode = '<%=sf_code%>';
                loadAdditionalFields();
                loaddivision();
                loadhq();
                loadstates();
                loadDistrict();
                loadTerritory();
                loaddesignation();
                loadReporting();


                if (sfcode == '') {

                    var dd = today.getDate();
                    var mm = today.getMonth() + 1;
                    var yyyy = today.getFullYear();
                    if (dd < 10) {
                        dd = '0' + dd
                    }
                    if (mm < 10) {
                        mm = '0' + mm
                    }
                    today = yyyy + '-' + mm + '-' + dd;
                    $("#tab_2").hide();
                    fstat1 = 0;
                    getSFCODE($('#ddlsftyp').val());
                }
                else {
                    getworkedwith(sfcode);
                    getApprSFDets('1');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "SalesForce_ApprvHyr.aspx/getSfDets",
                        data: "{'sfcode':'" + sfcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            SfDetails = data.d;
                            Itype = '1';
                            $('#sfemail').val(SfDetails[0].SfEmail);
                            if (SfDetails[0].sfusrname.indexOf("-") > -1) {
                                var sfausrname = SfDetails[0].sfusrname.split("-");
                                $('#fixUsrn').text(sfausrname[0] + "-");
                                $('#sfusrname').val(sfausrname[1]);
                            } else {
                                $('#fixUsrn').text("");
                                $('#sfusrname').val(SfDetails[0].sfusrname)
                            }
                            $('#sfpwd').val(SfDetails[0].sfpwd);
                            $('#sfcpwd').val(SfDetails[0].sfpwd); var splitsdiv = SfDetails[0].sfselsdiv.split(',') || [];
                            var csdiv = '';
                            if (splitsdiv.length > 0) {
                                for (var i = 0; i < splitsdiv.length; i++) {
                                    $('#division option[value=' + splitsdiv[i] + ']').attr('selected', true);
                                    csdiv += '\'' + splitsdiv[i] + '\',';
                                }
                                loaddepts(csdiv);
                            }
                            $('#division').selectpicker('refresh');
                            $('#sffirstname').val(SfDetails[0].SfName);
                            $('#sflastname').val('');
                            if (SfDetails[0].sfpimg != null && SfDetails[0].sfpimg != '' && SfDetails[0].sfpimg != ' ') {
                                $('#upimg').attr('src', 'http://fmcg.sanfmcg.com//SalesForce_Profile_Img/' + SfDetails[0].sfpimg);
                                propic_name = SfDetails[0].sfpimg;
                            }
                            else {
                                $('#upimg').attr('src', 'https://www.w3schools.com/howto/img_avatar.png')
                                propic_name = null;
                            }
                            $('#sfempid').val(SfDetails[0].sfempid);
                            $('#ddldistrict').val(SfDetails[0].sfdistrict);
                            $('#ddldistrict').selectpicker('refresh');
                            var sdob = (SfDetails[0].sfdob).split(' ');
                            sdob = sdob[0].split('/');
                            sdob = sdob[2] + '-' + sdob[1] + '-' + sdob[0];
                            $('#sfdob').val(sdob);
                            $('#depts').selectpicker('val', SfDetails[0].sfdept);
                            $('#states').selectpicker('val', SfDetails[0].sfstate);
                            $('#hqs').selectpicker('val', SfDetails[0].sfhq);
                            $('#routes').selectpicker('val', SfDetails[0].sfterr);
                            $('#sfmobil').val(SfDetails[0].sfmobile);
                            $('#sfaddr').val(SfDetails[0].sfaddr);
                            $('#usrauto').closest('td').find('span').hide();
                            $('#usrauto').hide();
                            var city = (SfDetails[0].sfcity).split('-');
                            $('#sfcity').val(city[0]);
                            $('#sfpincode').val(city[1]);
                            $('#sfarea').val(SfDetails[0].sfarea);
                            $('#fstat').val(SfDetails[0].sfstatus);

                            fstat1 = SfDetails[0].sfstatus;

                            //alert(fstat1);

                            $('#spstat').text($('#fstat :selected').text());
                            $('#ddlftyp').val(SfDetails[0].sftype);
                            $('#ddlsftyp').val(SfDetails[0].sflvl);
                            // $('#ddlsftyp').prop('disabled',true);
                            $('#sfftyp').text($('#ddlsftyp :selected').text());
                            // $('#sftyptr').hide();
                            filtDesg(SfDetails[0].sflvl);
                            $('#fftyp').text($('#ddlftyp :selected').text());
                            var sjdate = (SfDetails[0].sfjdate).split(' ');
                            sjdate = sjdate[0].split('/');
                            sjdate = sjdate[2] + '-' + sjdate[1] + '-' + sjdate[0];
                            var mdt = moment(sjdate);
                            var txtdt = mdt.format("MMM Do YYYY");
                            $('#ffdttxt').text(txtdt);
                            $('#fjdate').val(sjdate);
                            //$("#fjdate").attr('min', sjdate);
                            var redate = (SfDetails[0].resndate).split(' ');
                            redate = redate[0].split('/');
                            redate = redate[2] + '-' + redate[1] + '-' + redate[0];
                            var rmdt = moment(redate);
                            var rtxtdt = rmdt.format("MMM Do YYYY");
                            $('#rffdttxt').text(rtxtdt);
                            $('#redate').val(redate);
                            $('#repto').selectpicker('val', SfDetails[0].sfreport);
                            $('#ffrpttxt').text($('#repto :selected').text());
                            $('#sfdepot').val(SfDetails[0].sfdepot);
                            $('#desg').selectpicker('val', SfDetails[0].sfdesg);
                            //$(document).find('.scrldiv .row').closest(".row").remove();
                            $(document).find('.scrldiv2 .row').empty();
                            //$(document).find('input[name="field"]').remove();
                            //$(document).find('input[name="value"]').remove();
                            for (var i = 1; i < SfDetails.length; i++) {
                                var txtfield = "txtfield" + i;
                                var txtval = "txtval" + i;
                                if (SfDetails[i].pname != null) {
                                    $('#' + SfDetails[i].pname).val(SfDetails[i].pval);
                                }
                            }
                            for (var i = 1; i < SfDetails.length; i++) {
                                var txtfield = "txtfield" + i;
                                var txtval = "txtval" + i;
                                if (SfDetails[i].cname != null) {
                                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" autocomplete="off" id=' + txtfield + ' value="' + SfDetails[i].cname + '" /></div><div class="col-sm-6 cls"><input type="text" name="value" autocomplete="off" id=' + txtval + ' value="' + SfDetails[i].cval + '" /></div></div>';
                                    $('.scrldiv2').append(newTr);
                                }
                            }
                        },
                        error: function (rs) {
                            alert(rs);
                        }
                    });
                }
                console.log(sfcode);
                $("#tab_Exp").on('click', function () {
                    typ = '';
                    //getworkedwith(sfcode);
                    if (ArrLen == 1) {
                        getApprSFDets($(this).attr('value'));
                        ArrLen++;
                    }
                    $('#draggablePanelList').show();
                    $('#draggablePanelList' + $(this).attr('value')).hide();
                    Refreshddl(appSfHyrExp);
                    $("#btnsvTp").hide(); $("#btnsv").show();
                });
                $("#tab_Tp").on('click', function () {
                    typ = '1';
                    $("#btnsvTp").show();
                    $("#btnsv").hide();
                    //getworkedwith(sfcode);
                    if (TpArrLen == 1) {
                        getApprSFDets($(this).attr('value'));
                        TpArrLen++;
                    }
                    $('#draggablePanelList1').show();
                    $('#draggablePanelList').hide();
                    //var sf = filtmgr.length == 0 ? $('#repto').val() : sfcode;
                    getworkedwith(sf);
                    Refreshddl(appSfHyrTP);
                });
                $("#tab_1").on('click', function () {
                    $("#btnsubmit").text('Next')
                })
                $("#tab_2").on('click', function () {
                    //$("#btnsubmit").prop('disabled', true);
                    $("#Emp_Det").hide(); $("#tab_3").show();
                    //$("#tab_Exp").click();
                    $("#btnsubmit").text('Submit');	
					var sf = $('#repto').val();
					if(sf == 'admin')
						getworkedwith(sf); 					
                });
                $("#tab_1").on('click', function () {
                    $("#Emp_Det").show(); $("#tab_3").hide();

                });
                $(document).on('click', '.panel-heading>i', function () {
                    var addrsf = $(this).closest('.panel-heading').attr('tsf');
                    var addrsfname = $(this).closest('.panel-heading').find('.apsf').text();
                    getmgrExpensePnding(addrsf);
                    if (pndng.length > 0) {
                        alert('Expense is pending with this Manager Once it\'s cleared \nThen only You will Remove from the list!!!');
                        return false;
                    }
                    if (filtmgr.length > 0) {
                        $('#selmgr').empty();
                        //$('#selmgr').selectpicker('refresh');
                        for ($i = 0; $i < filtmgr.length; $i++) {
                            $('#selmgr').append('<option value="' + filtmgr[$i].SF_Code + '">' + filtmgr[$i].SF_Name + '</option>');
                        }
						if ('<%=sf_code%>' != '') {
							 $('#selmgr  option[value=' + '<%=sf_code%>' + ']').remove();
						}
                        $('#selmgr').selectpicker('refresh');
                    }

                    Refreshddl(appSfHyrExp); Refreshddl(appSfHyrTP);
                    $('#selmgr').append('<option value="' + addrsf + '">' + addrsfname + '</option>');
                    $('#selmgr').selectpicker('refresh');
                    $(this).parent().parent().remove();
                    var panelList = $('#draggablePanelList' + typ);
                    removeIndx(addrsf, typ == '' ? appSfHyrExp : appSfHyrTP);
					appSfHyrExp=[];					
                    $('.panel', panelList).each(function (index, elem) {

                        var $listItem = $(elem),
                            newIndex = $listItem.index();
                        $(elem).find('div>.aplvl').html("Level - " + ($listItem.index() + 1))
                        // Persist the new indices.
                        if ($(this).find('div').attr('tsf') == 'admin') {
                            //$('#selmgr').empty();
                            //$('#selmgr').selectpicker('refresh');
                        }
                        else {
                            //removeLowersf($(this).find('div').attr('tsf'));
                        }drag_arr();
                    });
                });
                $('#addLvl').on('click', function () {
                    var hrsf = $('#selmgr').val();
                    var hrsfname = $('#selmgr :selected').text();
					appArr = appSfHyrExp.filter(function (element) {
                            return (element.SF == 'admin');
                        });
                    if ((hrsf == '')) {
                        alert('Select a Manager');
                        return false;
                    }
					//else if($('#selmgr').val()==null || $('#selmgr').val()=="0" || appArr.length==0){
					//	return false;
					//}
					
					else if(($('#selmgr').val() == null || $('#selmgr').val()=="0")){
						return false;
					}
					
                    $('#draggablePanelList' + typ).append(`<li class='panel panel-info' style='margin-bottom: 1rem;'><div class='panel-heading' tsf='${hrsf}'><span class="apsf">${hrsfname}</span><i class="fa fa-trash-o" style="float: right;line-height: 20px;margin-left: 2rem;cursor:pointer !important"></i>&nbsp;<span class="aplvl" style='float:right;'>Level - ${($('#draggablePanelList' + typ + '>li').length + 1)}</span></div></li>`);
                    if (hrsf == 'admin') {
                        //$('#selmgr').empty();
                        //$('#selmgr').selectpicker('refresh');
                    }
                    else {
                        //removeLowersf(hrsf);
                    }
                    drag_arr();
                    $('#selmgr :selected').remove();
                    $('#selmgr').selectpicker('refresh');
                });
                var ctr = 1;
                $('#btnpls').on('click', function () {
                    ctr++;
                    var txtfield = "txtfield" + ctr;
                    var txtval = "txtval" + ctr;
                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" autocomplete="off" id=' + txtfield + ' /></div><div class="col-sm-6 cls"><input type="text" name="value" autocomplete="off" id=' + txtval + ' /></div></div>';
                    $('.scrldiv').append(newTr);
                });
                var ctr2 = 1;

                $('#bttnpls').on('click', function () {
                    ctr++;
                    var txtfield = "txtfield" + ctr2;
                    var txtval = "txtval" + ctr2;
                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" autocomplete="off" id=' + txtfield + ' /></div><div class="col-sm-6 cls"><input type="text" name="value" autocomplete="off" id=' + txtval + ' /></div></div>';
                    $('.scrldiv2').append(newTr);
                });

                $("#mobil").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $("#pin").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $('#uplfile').on('change', function (e) {
                    var img = URL.createObjectURL(e.target.files[0]);
                    $('#upimg').attr('src', img);
                    $.ajax({
                        url: 'SalesForce_Handler.ashx',
                        type: 'POST',
                        data: new FormData($('form')[0]),
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (file) {
                            propic_name = file.name;
                            alert(file.name + "has been uploaded.");
                        }
                    });
                });

                $('#ffstatus').on('click', function () {
                    $('#fstat').show();
                    $('#ffstatus').closest('td').css('display', 'none')
                });

                $('#fstat').on('change', function () {
                    $('#spstat').text($('#fstat :selected').text());
                    $('#fstat').hide();
                    $('#ffstatus').closest('td').css('display', 'table-cell');
                    var actval = $('#fstat').val();
                    if (actval == 2) {
                        $('#resroew').show();
                        //$('.sfempid').hide();
                        //$('.ffJdate').hide();
                    }
                    else if (actval == 1) {
                        $('#resroew').show();
                        //$('.sfempid').show();
                        //$('.ffJdate').show();
                    }
                    else {
                        $('#resroew').hide();
                        //$('.sfempid').hide();
                        //$('.ffJdate').hide();
                    }
                });

                var actval = $('#fstat').val();
                if (actval == 2) {
                    $('#resroew').show();
                    //$('.sfempid').hide();
                    //$('.ffJdate').hide();
                }
                else if (actval == 1) {
                    $('#resroew').show();
                    //$('.sfempid').show();
                    //$('.ffJdate').show();
                }
                else {
                    $('#resroew').hide();
                    //$('.sfempid').hide();
                    //$('.ffJdate').hide();
                }

                $('#fftype').on('click', function () {
                    $('#ddlftyp').show();
                    $('#fftype').closest('td').css('display', 'none')
                });

                $('#division').on('change', function () {
                    var sdiv = $('#division').val() || [];
                    var ssdiv = '';
                    if (sdiv.length > 0) {
                        for (var i = 0; i < sdiv.length; i++) {
                            ssdiv += '\'' + sdiv[i] + '\',';
                        }
                        loaddepts(ssdiv);
                    }
                });

                $('#usrauto').on('change', function () {
                    var $this = $(this);
                    if ($this.is(":checked")) {
                        $('#sfusrname').prop('readonly', true)
                        usratuogenerate = true; getDivisname(1);
                    }
                    else {
                        $('#sfusrname').prop('readonly', false)
                        usratuogenerate = false;
                    }
                })

                $('#states').on('change', function () {
                    if (usratuogenerate == true) {
                        var sfilt = SFStates.filter(function (a) {
                            return a.scode == $('#states').val();
                        });

                        $('#fixUsrn').text(SFDivisname[0].Division_SName + "-");
                        if (autogenusrname == '') {
                            autogenusrname = sfilt[0].sshname + SFDivisname[0].uname;//SFDivisname[0].Division_SName +
                        }
                        else {
                            var des = ($('#sfusrname').attr('des') == undefined) ? '' : $('#sfusrname').attr('des')
                            autogenusrname = sfilt[0].sshname + des + SFDivisname[0].uname;//SFDivisname[0].Division_SName +
                        }
                        $('#sfusrname').attr('stt', sfilt[0].sshname);
                        $('#sfusrname').val(autogenusrname)
                    }
                });

                $('#desg').on('change', function () {
                    if (usratuogenerate == true) {
                        var dfilt = SFDesg.filter(function (a) {
                            return a.dcode == $('#desg').val();
                        });
                        $('#fixUsrn').val(SFDivisname[0].Division_SName + "-");
                        if (autogenusrname == '') {
                            autogenusrname = dfilt[0].dshname + SFDivisname[0].uname; //SFDivisname[0].Division_SName +
                        }
                        else {
                            var stt = ($('#sfusrname').attr('stt') == undefined) ? '' : $('#sfusrname').attr('stt')
                            autogenusrname = stt + dfilt[0].dshname + SFDivisname[0].uname;//SFDivisname[0].Division_SName +
                        }
                        $('#sfusrname').attr('des', dfilt[0].dshname);
                        $('#sfusrname').val(autogenusrname)
                    }
                });

                $('#ddlftyp').on('change', function () {
                    /*if ($('#ddlftyp').val() != 'FF') {
                        $('#sftyptr').hide();
                    }
                    else {
                        $('#sftyptr').show();
                    }*/
                    $('#fftyp').text($('#ddlftyp :selected').text());
                    $('#ddlftyp').hide();
                    $('#fftype').closest('td').css('display', 'table-cell')
                });

                $('#sfftype').on('click', function () {
                    $('#ddlsftyp').show();
                    $('#sfftype').closest('td').css('display', 'none')
                });

                $('#ddlsftyp').on('change', function () {
                    $('#sfftyp').text($('#ddlsftyp :selected').text());
                    $('#ddlsftyp').hide();
                    $('#sfftype').closest('td').css('display', 'table-cell');
                    $x = $('#ddlsftyp').val();
                    filtDesg($x);
                    getSFCODE($('#ddlsftyp').val());
                });

                $('#ffJdate').on('click', function () {
                    $('#fjdate').show();
                    $('#ffJdate').closest('td').css('display', 'none');
                });

                $('#fjdate').on('change', function () {
                    var mdt = moment($('#fjdate').val());
                    var txtdt = mdt.format("MMM Do YYYY");
                    $('#ffdttxt').text(txtdt);
                    //$('#fjdate').hide();
                    //$('#ffJdate').closest('td').css('display', 'table-cell');
                });

                $('#rdate').on('click', function () {
                    $('#redate').show();
                    $('#rdate').closest('td').css('display', 'none');
                });

                $('#redate').on('change', function () {
                    var rdt = moment($('#redate').val());
                    var txtrdt = rdt.format("MMM Do YYYY");
                    $('#rffdttxt').text(txtrdt);
                    $('#redate').hide();
                    $('#rdate').closest('td').css('display', 'table-cell');
                });

                $('#ffrpt').on('click', function () {
                    $('#repto').closest('td').show();
                    $('#ffrpt').closest('td').css('display', 'none');
                });

                $('#repto').on('changed.bs.select', function () {
                    $('#ffrpttxt').text($('#repto :selected').text());
                    $('#repto').closest('td').css('display', 'none');
                    $('#ffrpttxt').closest('td').css('display', 'table-cell');
					getworkedwith($(this).val());
                });
				
			 $("#sfemail").keyup(function () {
                        var email = $("#sfemail").val();
                        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                        if (!filter.test(email)) {
                            //alert('Please provide a valid email address');                       
                            //$("#EmailHelp").val(msg);
                            $('#sfemail').addClass('is-invalid');
                            $('#EmailHelp').css('display', 'inline')
                            email.focus;
                            return false;
                        }
                        else {
                            $('#sfemail').removeClass('is-invalid');
                            $('#EmailHelp').css('display', 'none')
                            return true;
                        }
                    });
                    
                     $("#sfpwd").keyup(function () {
                    var sfcpassword = $("#sfpwd").val();
                    //let regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,12}$/;

                    var filter = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,12}$/;
                    if (!filter.test(sfcpassword)) {
                       
                        $('#sfpwd').addClass('is-invalid');
                        $('#sfpwdHelp').css('display', 'inline')
                        sfcpassword.focus;
                        return false;
                    }
                    else {
                        $('#sfpwd').removeClass('is-invalid');
                        $('#sfpwdHelp').css('display', 'none')
                        return true;
                    }
                });



                  
                $('#sfcpwd').keyup(function () {
                    if ($('#sfcpwd').val() != $('#sfpwd').val()) {
                        $('#sfcpwd').addClass('is-invalid');
                        $('#passwordHelp').css('display', 'inline')
                        return false;
                    }
                    else {
                        $('#sfcpwd').removeClass('is-invalid');
                        $('#passwordHelp').css('display', 'none')
                        return true;
                    }
                });

                //$('#btnsvTp').on('click', function () {

                //});

                //$('#btnsv').on('click', function () {

                //});

                $('#btnback').on('click', function () {
                    window.location.href = 'SalesForce_List.aspx';
                });

                $('#btnsubmit').on('click', function () {
                    if ($('#btnsubmit').text() == 'Submit') {
                       // if ($('#draggablePanelList1').find('li').length < 1) {
                       //     alert('User Must Have Atleast One Approval Manager');
                       //     return false;
                       // }
                       if(appSfHyrTP.length>0){
                        var sXMl = '<ROOT>';
                        for ($i = 0; $i < appSfHyrTP.length; $i++) {
                            sXMl += "<ASSD ALvl=\"" + appSfHyrTP[$i].Lvl + "\" ASF=\"" + appSfHyrTP[$i].SF + "\" ALvlTxt=\"" + appSfHyrTP[$i].LvlTxt + "\" ASFN=\"" + appSfHyrTP[$i].SFN + "\" />"
                        }
                        sXMl += "</ROOT>";
                        Save(sfcode, sXMl, 2, 'Tp');
                       }
                        if ($('#draggablePanelList').find('li').length < 1 && '<%=DBase_EReport.Global.ExpenseType%>'=='2') {
                            toastr.error('User Must Have Atleast One Approval Manager', 'Alert!!!');
                            //alert('User Must Have Atleast One Approval Manager');
                            return false;
                        }
                        var sXMl = '<ROOT>';
                        for ($i = 0; $i < appSfHyrExp.length; $i++) {
                            sXMl += "<ASSD ALvl=\"" + appSfHyrExp[$i].Lvl + "\" ASF=\"" + appSfHyrExp[$i].SF + "\" ALvlTxt=\"" + appSfHyrExp[$i].LvlTxt + "\" ASFN=\"" + appSfHyrExp[$i].SFN + "\" />"
                        }
                        sXMl += "</ROOT>";
                        Save(sfcode, sXMl, 1, 'Expense');
                        var sfemail = $('#sfemail').val().trim();
                        //if (sfemail == "") {
                        //     alert('Enter the Email');
                        //      $('#sfemail').focus();
                        //      return false;
                        //  }
                        var sfusrname = $('#sfusrname').val();
                        if (sfusrname == '') {
                            alert('Enter the Username');
                            $('#sfusrname').focus();
                            return false;
                        }
                        /*if((window.location.origin).indexOf('fmcg.sanfmcg')>-1){
                            sfusrname=sfusrname;
                        }
                        else{*/
                        sfusrname = $('#fixUsrn').text() + sfusrname;
                        //}
                        var sfpwd = $('#sfpwd').val();
                        if (sfpwd == '') {
                            alert('Enter the Password');
                            $('#sfpwd').focus();
                            return false;
                        }
                        var sfcpwd = $('#sfcpwd').val();
                        if (sfcpwd != sfpwd) {
                            alert("Password doesn't Match");
                            $('#sfcpwd').focus();
                            return false;
                        }
                        var sffirstname = $('#sffirstname').val().trim();
                        if (sffirstname == '') {
                            alert("Enter the First Name");
                            $('#sffirstname').focus();
                            return false;
                        }
                        var sflastname = $('#sflastname').val().trim();
                        var sfempid = $('#sfempid').val().trim();
                        //if (sfempid == '') {
                        //    alert("Enter the Employee ID");
                        //    $('#sfempid').focus();
                        //    return false;
                        //}
                        var sfdob = $('#sfdob').val();
                        var sfdesg = $('#desg').val().trim();
                        if (sfdesg == 0) {
                            alert("Select the Designation");
                            $('#desg').focus();
                            return false;
                        }
                        var sfdept = $('#depts').val();
                        // var sfdept = $('#depts').val().trim();
                        if (sfdept == 0) {
                            alert("Select the Department");
                            $('#depts').focus();
                            return false;
                        }
                        var sfstate = $('#states').val().trim();
                        if (sfstate == 0) {
                            alert("Select the State");
                            $('#states').focus();
                            return false;
                        }
                        var sfhq = $('#hqs').val();
                        var sfhqname = $('#hqs :selected').text();
                        if (sfhq == 0 || sfhqname == '') {
                            alert("Select the HQ");
                            return false;
                        }
                        var sdiv = [];
                        sdiv = $('#division').val() || [];
                        //alert(sdiv);
                        if (sdiv.length == 0) {
                            alert('Select a Division');
                            return false;
                        }
                        var seldiv = '';
                        for (var i = 0; i < sdiv.length; i++) {
                            seldiv += sdiv[i] + ',';
                        }
                        var sfdept = $('#depts').val();
                        //var sfdept = $('#depts').val().trim();
                        //if (sfdept == 0) {
                        //    alert("Select the Department");
                        //    return false;
                        //}
                        var sftype = $('#ddlftyp').val();
                        var sflevel = '2';
                        if (sftype == 'FF') {
                            sflevel = $('#ddlsftyp').val();
                        }
                        sflevel = SFDesg.filter(function (a) {
                            return a.dcode == sfdesg;
                        }).map(function (a) { return a.dtype }).toString();
                        var sfterr = $('#routes').val().trim();
                        if (sflevel == '1' && sfterr == "0") {
                            alert("Select the Territory");
                            $('#routes').focus();
                            return false;
                        }
                        var sfmobile = $('#sfmobil').val().trim();
                        if (sfmobile == '') {
                            alert("Enter the Mobile Number");
                            $('#sfmobil').focus();
                            return false;
                        }
                        var sfaddr = $('#sfaddr').val().trim();
                        var sfcity = $('#sfcity').val().trim();
                        var sfarea = $('#sfarea').val().trim();
                        var sfpincode = $('#sfpincode').val();
                        var sfstatus = $('#fstat').val();
                        var sfjdate = $('#fjdate').val();
                        var sfresdate = $('#redate').val();
                        var sfrepto = $('#repto').val();
                        var sfdepot = $('#sfdepot').val();
                        if (sfrepto == 0) {
                            alert("Select Reporting to");
                            $('#repto').focus();
                            return false;
                        }

                        if (fstat1 == 0) {
                            if (sfstatus == 0 && sfempid == '') {

                                alert("Please enter the employee id");
                                $('#sfempid').focus();
                                return false;
                            }

                            if (sfstatus == 0 && sfjdate == '') {

                                alert("Please select the joining date");
                                $('#fjdate').focus();
                                return false;
                            }
                        }
                        else {

                            if ((fstat1 == 1 && sfstatus == 0 && sfempid == '')) {

                                alert("Please enter the employee id");
                                $('#sfempid').focus();
                                return false;
                            }

                            if ((fstat1 == 1 && sfstatus == 0 && sfjdate == '')) {

                                alert("Please select the joining date");
                                $('#fjdate').focus();
                                return false;
                            }
                        }

                        var psarr = [];
                        var a = true;
                        $('.scrldiv .row').each(function () {
                            if ($(this).find('input[name="field"]').attr('mandatory') == 'Mandatory') {
                                if ($(this).find('input[name="value"]').val() == '' || $(this).find('input[name="value"]').val() == undefined) {
                                    alert('Enter the Value for ' + $(this).find('input[name="field"]').val());
                                    $('#exampleModal').modal('toggle');
                                    psarr = [];
                                    $(this).find('input[name="value"]').focus();
                                    a = false;
                                }
                                else {
                                    psarr.push({
                                        pfield: $(this).find('input[name="value"]').attr('id'),
                                        pval: $(this).find('input[name="value"]').val()
                                    }); a = true;
                                }
                            }
                            else {
                                psarr.push({
                                    pfield: $(this).find('input[name="value"]').attr('id'),
                                    pval: $(this).find('input[name="value"]').val()
                                }); a = true;
                            }
                            if (a == false) {
                                return false;
                            }
                        });
                        if (a == false) {
                            return false;
                        }
                        var csarr = [];
                        $('.scrldiv2 .row').each(function () {
                            csarr.push({
                                cfield: $(this).find('input[name="field"]').val(),
                                cval: $(this).find('input[name="value"]').val()
                            });
                        });
                        if (propic_name == null) {
                            propic_name = ' ';
                        }
                        let sfdist = $('#ddldistrict').val();
                        //data = { "DivCode": "<%=Session["div_code"]%>", "SfDiv": seldiv, "SFHQN":sfhqname, "SfCode": sfcode, "SF_Name": (sffirstname + sflastname), "SfEmail": sfemail, "SfUsername": sfusrname, "SfPwd": sfpwd, "SfEmpId": sfempid, "SfDOB": sfdob, "SfDesig": sfdesg, "SfDept": sfdept, "SfHQ": sfhq, "SfState": sfstate, "SfTerr": sfterr, "SfMobile": sfmobile, "SfAddr": sfaddr, "SfArea": sfarea, "SfCity": (sfcity + '-' + sfpincode), "SfPincode": sfpincode, "SfStatus": sfstatus,"Sfrdate": sfresdate, "SfTyp": sftype, "SfJDate": sfjdate, "SfReport": sfrepto, "SfPImg": propic_name, "PArr": psarr, "CArr": csarr, "SFlevel": sflevel, "SFDepot": sfdepot, "district": sfdist }

                        if (sflastname == "" || sflastname == null) {

                            data = { "DivCode": "<%=Session["div_code"]%>", "SfDiv": seldiv, "SFHQN": sfhqname, "SfCode": sfcode, "SF_Name": sffirstname, "SfEmail": sfemail, "SfUsername": sfusrname, "SfPwd": sfpwd, "SfEmpId": sfempid, "SfDOB": sfdob, "SfDesig": sfdesg, "SfDept": sfdept, "SfHQ": sfhq, "SfState": sfstate, "SfTerr": sfterr, "SfMobile": sfmobile, "SfAddr": sfaddr, "SfArea": sfarea, "SfCity": (sfcity + '-' + sfpincode), "SfPincode": sfpincode, "SfStatus": sfstatus, "Sfrdate": sfresdate, "SfTyp": sftype, "SfJDate": sfjdate, "SfReport": sfrepto, "SfPImg": propic_name, "PArr": psarr, "CArr": csarr, "SFlevel": sflevel, "SFDepot": sfdepot, "district": sfdist }

                        }
                        else {
                            data = { "DivCode": "<%=Session["div_code"]%>", "SfDiv": seldiv, "SFHQN": sfhqname, "SfCode": sfcode, "SF_Name": sffirstname + ' ' + sflastname, "SfEmail": sfemail, "SfUsername": sfusrname, "SfPwd": sfpwd, "SfEmpId": sfempid, "SfDOB": sfdob, "SfDesig": sfdesg, "SfDept": sfdept, "SfHQ": sfhq, "SfState": sfstate, "SfTerr": sfterr, "SfMobile": sfmobile, "SfAddr": sfaddr, "SfArea": sfarea, "SfCity": (sfcity + '-' + sfpincode), "SfPincode": sfpincode, "SfStatus": sfstatus, "Sfrdate": sfresdate, "SfTyp": sftype, "SfJDate": sfjdate, "SfReport": sfrepto, "SfPImg": propic_name, "PArr": psarr, "CArr": csarr, "SFlevel": sflevel, "SFDepot": sfdepot, "district": sfdist }
                        }

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "SalesForce_ApprvHyr.aspx/saveFieldForce",
                            data: "{'data':'" + JSON.stringify(data) + "','Itype':'" + Itype + "'}",
                            dataType: "json",
                            success: function (data) {
                                var successtext = data.d;
                                if (successtext == "Dup") {
                                    toastr.error('UserName Already Exists', 'Alert!!!');
                                    //alert('UserName Already Exists');
                                }
                                else if (successtext == "SalesForce Created") {
                                    toastr.success('SalesForce Created', 'Success!!!');
                                    //alert('SalesForce Created');
                                    //$("#tab_1").hide();                                    
                                    //window.location.href = 'SalesForce_List.aspx';
                                    $("#re_direct").show();
                                    clearfields();
                                }
                                else if (successtext == "Update Success") {
                                    
                                    toastr.options = {
                                        "positionClass": "toast-top-right",
                                        timeOut: "5000",
                                        extendedTimeOut: "1000",
                                        tapToDismiss: false,
                                        progressBar: true,
                                        "onHidden": function () {
                                            clearfields();
                                            window.location.href = 'SalesForce_List.aspx';
                                        }
                                    }
                                    toastr.success(successtext, 'Success!!!');
                                    //alert(successtext);
                                    //window.location.href = 'SalesForce_List.aspx';
                                    //clearfields();
                                }
                                else if (successtext == "Employee ID Already Exist") {
                                    toastr.error(successtext, 'Alert!!!');
                                    //alert(successtext);
                                    $('#sfempid').focus();
                                }
                                else {
                                    toastr.success(successtext, 'Success!!!');
                                   // alert(successtext);
                                    sfcode = successtext.slice(19);
                                    $("#re_direct").show();
                                    //window.location.href = 'SalesForce_List.aspx';
                                    //$("#tab_2").show();
                                    //$("#tab_2").click();
                                    //getworkedwith(sfcode);
                                    //getApprSFDets('1');
                                    clearfields();
                                }
                            },
                            error: function (rs) {
                                alert(rs);
                            }
                        });
                    }
                    else if ($('#btnsubmit').text() == 'Next') {
 	 		var sfusrname = $('#sfusrname').val();
                        if (sfusrname == '') {
                            toastr.error('Enter the Username', 'Alert!!!');
                            //alert('Enter the Username');
                            $('#sfusrname').focus();
                            return false;
                        }
                        /*if((window.location.origin).indexOf('fmcg.sanfmcg')>-1){
                            sfusrname=sfusrname;
                        }
                        else{*/
                        sfusrname = $('#fixUsrn').text() + sfusrname;
                        //}
                        var sfpwd = $('#sfpwd').val();
                        if (sfpwd == '') {
                            toastr.error('Please Enter the Password', 'Alert!!!');
                            //alert('Enter the Password');
                            $('#sfpwd').focus();
                            return false;
                        }
                        var sfcpwd = $('#sfcpwd').val();
                        if (sfcpwd != sfpwd) {
                            toastr.error("Password doesn't Match", 'Alert!!!');
                            //alert("Password doesn't Match");
                            $('#sfcpwd').focus();
                            return false;
                        }
                        var sffirstname = $('#sffirstname').val();
                        if (sffirstname == '') {
                            toastr.error("Please Enter the First Name", 'Alert!!!');
                            //alert("Enter the First Name");
                            $('#sffirstname').focus();
                            return false;
                        }
                        var sflastname = $('#sflastname').val();
                        var sfempid = $('#sfempid').val();
                        if (sfempid == '') {
                            toastr.error("Enter the Employee Id", 'Alert!!!');
                            //alert("Enter the Employee ID");
                            $('#sfempid').focus();
                            return false;
                        }
                        var sfdob = $('#sfdob').val();
                        var sfdesg = $('#desg').val();
                        if (sfdesg == 0) {
                            toastr.error("Select the Designation", 'Alert!!!');
                            //alert("Select the Designation");
                            $('#desg').focus();
                            return false;
                        }
                        var sfdept = $('#depts').val();
                        // var sfdept = $('#depts').val().trim();
                        if (sfdept == 0) {
                            toastr.error("Select the Department", 'Alert!!!');
                            //alert("Select the Department");
                            $('#depts').focus();
                            return false;
                        }
                        var sfstate = $('#states').val();
                        if (sfstate == 0) {
                            toastr.error("Select the State", 'Alert!!!');
                            //alert("Select the State");
                            $('#states').focus();
                            return false;
                        }
                        var sfhq = $('#hqs').val();
                        var sfhqname = $('#hqs :selected').text();
                        if (sfhq == 0 || sfhqname == '') {
                            toastr.error("Select the HQ", 'Alert!!!');
                            //alert("Select the HQ");
                            return false;
                        }
						
			var div=$('#division').val(); 
			if((div == 0 || div == null))
			{
 			   toastr.error("Please Select  the division", 'Alert!!!');
                           //alert("Select the HQ");
         		   $('#division').focus();
                            return false;
			}
						
                        var sdiv = [];
                        sdiv = $('#division').val() || [];
                        //alert(sdiv);
                        if (sdiv.length == 0) {
                            toastr.error('Select a Division', 'Alert!!!');
                            //alert('Select a Division');
                            return false;
                        }
                        var seldiv = '';
                        for (var i = 0; i < sdiv.length; i++) {
                            seldiv += sdiv[i] + ',';
                        }
                        var sfdept = $('#depts').val();
                        //var sfdept = $('#depts').val().trim();
                        //if (sfdept == 0) {
                        //    alert("Select the Department");
                        //    return false;
                        //}
                        var sftype = $('#ddlftyp').val();
                        var sflevel = '2';
                        if (sftype == 'FF') {
                            sflevel = $('#ddlsftyp').val();
                        }
                        sflevel = SFDesg.filter(function (a) {
                            return a.dcode == sfdesg;
                        }).map(function (a) { return a.dtype }).toString();
                        var sfterr = $('#routes').val().trim();
                        if (sflevel == '1' && sfterr == "0") {
                            toastr.error("Select the Territory", 'Alert!!!');
                            //alert("Select the Territory");
                            $('#routes').focus();
                            return false;
                        }
                        var sfmobile = $('#sfmobil').val();
                        if (sfmobile == '') {
                            toastr.error("Enter the Mobile Number", 'Alert!!!');
                            //alert("Enter the Mobile Number");
                            $('#sfmobil').focus();
                            return false;
                        }
                        var sfaddr = $('#sfaddr').val();
                        var sfcity = $('#sfcity').val();
                        var sfarea = $('#sfarea').val();
                        var sfpincode = $('#sfpincode').val();
                        var sfstatus = $('#fstat').val();
                        var sfjdate = $('#fjdate').val();
                        var sfresdate = $('#redate').val();
                        var sfrepto = $('#repto').val();
                        var sfdepot = $('#sfdepot').val();
                        if (sfrepto == 0) {                            
                            toastr.error("Select Reporting to", 'Alert!!!');
                            //alert("Select Reporting to");
                            $('#repto').focus();
                            return false;
                        }

                        if (fstat1 == 0) {
                            if (sfstatus == 0 && sfempid == '') {
                                toastr.error("Please enter the employee id", 'Alert!!!');
                                //alert("Please enter the employee id");
                                $('#sfempid').focus();
                                return false;
                            }

                            if (sfstatus == 0 && sfjdate == '') {
                                toastr.error("Please select the joining date", 'Alert!!!');
                                //alert("Please select the joining date");
                                $('#fjdate').focus();
                                return false;
                            }
                        }
                        else {

                            if ((fstat1 == 1 && sfstatus == 0 && sfempid == '')) {
                                toastr.error("Please enter the employee id", 'Alert!!!');
                                //alert("Please enter the employee id");
                                $('#sfempid').focus();
                                return false;
                            }

                            if ((fstat1 == 1 && sfstatus == 0 && sfjdate == '')) {
                                toastr.error("Please select the joining date", 'Alert!!!');
                               // alert("Please select the joining date");
                                $('#fjdate').focus();
                                return false;
                            }
                        }                       
                     sf = $('#repto').val();
                        if (sf == '') {
                            alert("Select Reporting Manager!!!");
                            $("#tab_2").hide();
                            $("#tab_1").show();
                            $('#repto').focus();
                            return false;
                        }
                        getworkedwith(sf); 
						$("#tab_2").show();
                        $("#tab_2").click(); $('#btnsubmit').text('Submit');
                        //getApprSFDets('1');
                    }
                });
            });

            function validate(input) {
                    if (/^\s/.test(input.value))
                        input.value = '';
                }

                function AvoidSpace(event) {
                    var k = event ? event.which : window.event.keyCode;
                    if (k == 32) return false;
                }
        </script>
    </body>
    </html>
</asp:Content>

