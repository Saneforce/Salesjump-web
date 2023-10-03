<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Company_Creation.aspx.cs" Inherits="MasterFiles_Company_Creation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title></title>
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
        </style>
    </head>
    <body>
        <div class="row">
            <div class="col-lg-12 sub-header">
                COMPANY CREATION 
           
            </div>
        </div>
        <form style="background: #ffffff; box-shadow: 0px 3px 12px rgba(0, 0, 0, 0.25); border-radius: 8px; margin-top: 5px;">
            <div class="container"style="overflow: hidden";>
                <div class="container">
                <div class="col-md-4" style="right: -25px; top: 25px;">

                    <div class="mb-3">
                        <label for="cmpny" style="font-size: medium" class="form-label">Company Name</label><br>
                        <input class="col-xs-15" style="height: 35px; font-size: medium" id="cmpny" type="text"  /><br>
                        <br>
                        
                    </div>
                    <div class="mb-3">
                        <label for="code" style="font-size: medium" class="form-label">Code</label><br>
                        <input class="col-xs-6" style="height: 35px; font-size: medium" id="code" type="text"  /><br>
                        <br>
                        <br>
                    </div>
                    <div class="mb-3">
                        <label for="addr" style="font-size: medium" class="form-label">Address</label><br>
                      <%--  <input class="col-xs-15" style="height: 80px; font-size:small" id="addr" rows="5" type="text" />--%>
                        <textarea name="Text1" style="font-size: medium" cols="40" id="addr" rows="4"></textarea><br>
                        <br>    
                    </div>
                    <div class="mb-3">
                        <label for="cntry" style="font-size: medium" class="form-label">Country</label>
                        <i class="fa fa-plus-circle" style="font-size:25px;margin-left:130px;color:green" id="addcountry" onclick="addRo()"></i><br>
                        <select  name="country" id="cntry" style="width:350px" onchange="loadstates()"></select><br>
                        <br>
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: 5px">
                        <label for="city" style="font-size: medium" class="form-label">City</label><br>
                        <input class="col-xs-15" style="height: 35px; font-size: medium" id="city" type="text" /><br>
                        <br>
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: 5px">
                        <label for="gstno" style="font-size: medium" class="form-label">GSTN.No</label><br>
                        <input class="col-xs-15" style="height: 35px; font-size: medium" id="gstno" type="text" /><br>
                        <br>
                    </div>



                    
                </div>




                <div class="col-md-8" style="left: 225px;">
                    <div style="position:absolute">
                     <div class="mb-3" style="position:static">
                        <%--<td colspan="2" align="center" style="padding-top: 45px;height:130px; padding-right: 10px; /* padding-bottom: 57px; */padding-left: 16px;">--%>
                            <img id="upimg" style="width: 40%; margin-left: 100px;margin-top:25px; /* height: 0%; */" src="https://seeklogo.com/images/B/business-company-logo-C561B48365-seeklogo.com.png" />
                     <%--   </td>--%>
                    </div></div>
                     <div style="margin-top:191px;position:static">
                   <div class="mb-3"  style="margin-top:60px; position:static">
                       <%-- <td colspan="2" align="center" style="/* padding-top: 57px; */padding-right: 10px; padding-bottom: 16px; padding-left: 60px;">--%>
                            <label for="uplfile" class="input-group-btn" style="width:150px">
                                <span class="btn btn-primary" style="margin-left: 156px"><i class="fa fa-cloud-upload append-icon"></i>&nbsp Uplaod</span>
                            </label>
                            <!--<button for="uplfile" type="button" class="btn btn-primary">Upload</button>-->
                            <input accept=".jpg,.jpeg,.png" id="uplfile" class="sr-only" name="uplfile" type="file" />
                        <%--</td>--%>
                    </div>
                        
                   
                    <div class="mb-3"style="margin-top:25px;">
                        <label for="url" style="font-size: medium" class="form-label">Preferred URL</label><br>
                        <div class="row" style="border: 1px solid #d8d8d8 !important;padding: 6px 6px 7px !important;border-radius: 6px !important;line-height: inherit !important;background: #fff;box-shadow: 0px 3px 5px rgb(0 0 0 / 10%);transition: all 0.3s ease;width: 300px;margin-left: 0px;">
                            <div class="col-sm-8" style="padding: 0px;"><input class="col-xs-8" style="height: 26px;font-size: medium;padding: 0px !important;width: 100%;border: 0px solid #d8d8d8 !important;border-radius: 0px !important;line-height: inherit !important;background: #fff;box-shadow: 0px 0px 0px rgb(0 0 0 / 10%);" id="url" type="text"></div>
                            <div class="col-sm-4" style="padding: 0px;line-height: 26px;font-size: 16px;">.salesjump.in</div></div>
                        <input class="col-xs-8" style="height: 35px; font-size: medium" id="url" type="text"  /><br>
                        <br>
                        <br>
                    </div>
                    <div class="mb-3">
                        <label for="ste" style="font-size: medium" class="form-label">State</label>
                         <i class="fa fa-plus-circle" style="font-size:25px;margin-left:180px;color:green" id="addstat" onclick="addsRo()"></i><br>
                        <select name="state" id="ste" style="width:300px"  /></select>
                        <br>
                        
                    </div>
                    <div class="mb-3"style="margin-top:30px;">
                        <label for="pin" style="font-size: medium" class="form-label">Pincode</label><br>
                        <input class="col-xs-5" style="height: 35px; font-size: medium" id="pin" type="text" /><br>
                        <br>
                    </div>
                    <div class="mb-3"style="margin-top:25px;">
                        <label for="atp" style="font-size: medium;" class="form-label">Attach Proposal</label><br>
                        <input class="col-xs-5" style="height: 35px; font-size: medium"<%--accept=".xlsx,.xlx,.csv"--%>  type="file" id="myFile" name="filename" /><br>
                        <br>
                        <br>
                    </div>  </div>                
                     </div>
                </div>
                
                <div>
                    
                <div class="mb-3" style="top: 100px; margin-top: 5px">
                        <label for="bil" style="font-size: 25px" class="form-label">Billing Details</label><br><hr  style="width:100%;overflow: visible; /* For IE */
                            padding: 0;
                            border: none;
                            border-top: medium double #333;
                            color: #0c499f;
                            text-align: center; "/<br>
                    </div>
                    <div class="col-md-4" style="right: -25px; top: 0px;">
                    <div class="mb-3" style="top: 100px; margin-top: 5px">
                        <label for="bilnme" style="font-size: medium" class="form-label">Billing Name</label><br>
                        <input class="col-xs-15" style="height: 35px; font-size: medium" id="bilnme" type="text" /><br>
                        <br>
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: 5px">
                        <label for="bilmod" style="font-size: medium" class="form-label">Billing Mode</label><br>
                        <select name="Mode" id="bilmod">
                            <option value="0">--select--</option>                            
                            <option value="Yearly">Yearly</option>                            
                            <option value="Half Yearly">Half Yearly</option>                            
                            <option value="Quaterly">Quaterly</option>                            
                            <option value="Monthly">Monthly</option>                            
                        </select><br>
                        <br>
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: 5px">
                        <label for="mnt" style="font-size: medium" class="form-label">Start Month</label><br>
                        <input class="col-xs-15" style="height: 35px; font-size: medium" id="mnt" type="text"/><br>
                        <br>
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: 5px">
                        <label for="amt" style="font-size: medium" class="form-label">Amount</label><br>
                        <input class="col-xs-15" style="height: 35px; font-size: medium" id="amt" type="text" /><br>
                        <br>
                        <br>
                    </div>
                        </div>
                    <div class="col-md-8" style="left: 240px;margin-top:-30px;">
                    <div class="mb-3">
                        <label for="usr" style="font-size: medium; margin-top: 38px;" class="form-label">No of Users</label><br>
                        <input class="col-xs-5" style="height: 35px; font-size: medium" id="usr" type="text"  /><br>
                        <br>
                        <br>
                    </div>
                    <div class="mb-3">
                        <label for="usr" style="font-size: medium; margin-top: 0px;" class="form-label">Type</label><br>
                        <select name="type" id="type" style="width:42%">
                            <option value="0">--select--</option>                            
                            <option value="Range-wise">Range-wise</option>                            
                            <option value="Regular">Regular</option>                            
                        </select>
                     
                        <br>
                        <br>
                    </div>
                    <div class="mb-3">
                        <label for="ran" style="font-size: medium;margin-top:5px;" class="form-label">Range</label><br>
                        <input class="col-xs-5" style="height: 35px; font-size: medium" id="ran"  type="text" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');" /><br>
                        <br>
                        <br>
                    </div>
                    <div class="mb-3">
                        <label for="acu" style="font-size: medium" class="form-label">Addition Cost/User</label><br>
                        <input class="col-xs-5" style="height: 35px; font-size: medium" id="acu"  type="text"  /><br>
                        <br>
                        <br>
                        <br>
                    </div>
                        </div>
                      </div>


                <div >
                <div  style="top: 100px;; margin-top: 15px">
                        <label for="bil" style="font-size:20px;margin-left:20px" class="form-label">Contacts For Billing</label>
                   
                    </div><br/><br/>
                  <div class="mb-3" style="right: -35px; top: 15px;">
                      <table id="cfb">
                          <tbody>
                      <tr>
                        <label for="name" style="font-size: medium;margin-left:05px" class="form-label">Name</label>
                          <label for="no" style="font-size: medium;margin-left:285px" class="form-label">Mobile</label>
                          <label for="no" style="font-size: medium;margin-left:285px" class="form-label">Email</label>
                        <%-- <button type="button" id="copybtn"  style="margin-left:240px"onclick="addRoww(this)">--%>
                             <i class="fa fa-plus-circle" style="font-size:30px;color:green;margin-left:250px" id="copybtn" onclick="addRoww(this)"></i>
                        <%-- </button><br>--%>

                      </tr>
                      <tr>
                      <td>
                          <div class="confrbil">                              
                              <input class="col-xs-6" name="name" style="height:35px;width:250px;margin-left:-10px;font-size: medium;" type="text"  />                        
                       <input class="col-xs-6"  name="mobile" style="height: 35px; width:250px; font-size: medium;margin-left:75px"  type="text" onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
                          <input class="col-xs-6" name="gmail" style="height: 35px; width:250px; font-size: medium;margin-left:75px"  type="text"  />
                      <%--<button type="button" style="margin-left:50px;margin-top:05px"id="rembtn"class="btnDelete">Remove</button>--%>
                       </div> </td>  
                    </tr></tbody></table>
                      </div>               
                    </div>
                <div >                   
                <div  style="top: 100px;; margin-top: 15px">
                        <label for="bil" style="font-size: 25px;margin-left:25px" class="form-label">Contacts For Data Upload</label><hr 
                            style="width:100%;overflow: visible;
                            padding: 0;
                            border: none;
                            border-top: medium double #333;
                            color: #0c499f;
                            text-align: center; "/>
                    </div>
                        
                  <div class="mb-3" style="right: -35px; top: 15px;">
                      <table id="cfd"><tbody>
                      <tr>
                        <label for="name" style="font-size: medium;margin-left:05px" class="form-label">Name</label>
                          <label for="no" style="font-size: medium;margin-left:285px" class="form-label">Mobile</label>
                          <label for="no" style="font-size: medium;margin-left:285px" class="form-label">Email</label>
                        <%-- <button type="button" >--%>
                          <i class="fa fa-plus-circle" style="font-size:30px;margin-left:250px;color:green" id="copybtn1" onclick="addRow(this)"></i>
                          <%--</button>--%><br>

                      </tr>
                         
                      <tr>
                      <td><div class="confrdat">
                          <input class="col-xs-6 " name="names" style="height:35px;width:250px;margin-left:-10px;font-size: medium;"  type="text"  />                        
                       <input class="col-xs-6" name="mobileno" style="height: 35px; width:250px; font-size: medium;margin-left:75px"  type="text" onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
                          <input class="col-xs-6" name="mail" style="height: 35px; width:250px; font-size: medium;margin-left:75px"  type="text"  />
                      <%--<button type="button" style="margin-left:50px;margin-top:05px" id="remobtn" onclick="delrow()" class="btnDelet">Remove</button>
                        <br><br><br>--%></div></td>  
                    </tr>
                      </tbody></table>
                      </div>
                    </div><br /><br /><br />
                <button style="margin-left: 500px;margin-top: 10px;" type="button" id="btnsubmit" class="btn btn-primary">Submit</button><br>
                <footer style="height:50px"></footer>
            </div>

            <div class="modal fade" id="myModal" role="dialog" style="display:none; z-index: 10000000; overflow-y: auto; background-color: rgb(255 255 255 / 15%);">
            <div class="modal-dialog" role="document">

                <!-- Modal content-->
                <div class="modal-content" style="width:125%;">
                    
                    <div class="modal-body" style="height:100px">
                        <table id="addcn"><tbody>
                      <tr>
                        <label for="name" style="font-size: medium;margin-left:05px" class="form-label">Country Name</label>
                          <label for="no" style="font-size: medium;margin-left:130px" class="form-label">Country Code</label>
                          <label for="no" style="font-size: medium;margin-left:130px" class="form-label">Short Name</label>                     
                            <br>
                      </tr>
                         
                      <tr>
                      <td><div class="addcn">
                          <input class="col-xs-5 " id="cname"  style="height:35px;width:200px;margin-left:-10px;font-size: medium;"  type="text"  />                        
                       <input class="col-xs-3" id="ccode" style="height: 35px; width:200px; font-size: medium;margin-left:35px"  type="text" onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
                          <input class="col-xs-4"  id="csname" style="height: 35px; width:200px; font-size: medium;margin-left:35px"  type="text"  />
                     </div></td>  
                    </tr>
                      </tbody></table>

                    </div>
                    <div class="modal-footer" style="height:50px">
                        <button type="button" style="margin-top:-10px" class="btn btn-default" onclick="Savecountry()">Save</button>
                        <button type="button" style="margin-top:-10px" class="btn btn-default" id="Closebtn" data-dismiss="modal">Close</button>
                        
                       
                    </div>
                </div>
            </div>
        </div>


            <div class="modal fade" id="addstate" role="dialog" style="display:none; z-index: 10000000; overflow-y: auto; background-color: rgb(255 255 255 / 15%);">
            <div class="modal-dialog" role="document">

                <!-- Modal content-->
                <div class="modal-content" style="width:125%;">
                    
                    <div class="modal-body" style="height:100px">
                        <table id="addcn"><tbody>
                      <tr>
                        <label for="name" style="font-size: medium;margin-left:05px" class="form-label">State Name</label>
                          <label for="no" style="font-size: medium;margin-left:150px" class="form-label"> State Short Name</label>  
                          <label for="no" style="font-size: medium;margin-left:100px" class="form-label">Country Code</label>
                                             
                            <br>
                      </tr>
                         
                      <tr>
                      <td><div class="addcn">
                          <input class="col-xs-5 " id="sname"  style="height:35px;width:200px;margin-left:-10px;font-size: medium;"  type="text"  /> 
                           <input class="col-xs-4"  id="sshname" style="height: 35px; width:200px; font-size: medium;margin-left:35px"  type="text"  />
                       <input class="col-xs-3" id="cscode" style="height: 35px; width:200px; font-size: medium;margin-left:35px"  type="text" onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
                         
                     </div></td>  
                    </tr>
                      </tbody></table>

                    </div>
                    <div class="modal-footer" style="height:50px">
                        <button type="button" style="margin-top:-10px" class="btn btn-default" onclick="Savestate()">Save</button>
                        <button type="button" style="margin-top:-10px" class="btn btn-default" id="Closebtn" data-dismiss="modal">Close</button>
                        
                       
                    </div>
                </div>
            </div>
        </div>
        </form>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.0/jquery.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
<script src="./js/country-states.js"></script>
 <script type="text/javascript">
     var Country = [];
     var state = [];
     $('#uplfile').on('change', function (e) {
         var img = URL.createObjectURL(e.target.files[0]);
         $('#upimg').attr('src', img);
         $.ajax({
             url: 'Company_LogoHandler.ashx',
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

     $('#myFile').on('change', function (e) {
         var afile = URL.createObjectURL(e.target.files[0]);
         $('#myFile').attr('src', afile);
         $.ajax({
             url: 'proposal_Handler.ashx',
             type: 'POST',
             data: new FormData($('form')[0]),
             cache: false,
             contentType: false,
             processData: false,
             success: function (file) {
                 prop_name = file.name;
                 alert(file.name + "has been uploaded.");
             }
         });
     });

     $('#Closebtn').on("click", function () {
         $('#cname').val('');
         $('#ccode').val('');
         $('#csname').val('');
     });
     function addRo() {
         $('#myModal').modal('toggle');
     }
     function Savecountry() {
         var addconarr = [];

         var cname = document.getElementById("cname").value;
         if (cname == '') {
             alert('Enter the New Country Name');
             return false;
         }
         var ccode = document.getElementById("ccode").value;
         if (ccode == '') {
             alert('Enter the country code');
             return false;
         }
         var sname = document.getElementById("csname").value;
         if (sname == '') {
             alert('Enter the Country short name');
             return false;
         }
         addconarr.push({
             cname: cname,
             ccode: ccode,
             sname: sname
         })
         let sxml = "<ROOT>";
         for (let i = 0; i < addconarr.length; i++) {
             sxml += `<TP CNAME="${addconarr[i]["cname"]}"  CCODE="${addconarr[i]["ccode"]}" CSNAME="${addconarr[i]["sname"]}" />`;
         }
         sxml += "</ROOT>";

         var arr = Country.filter(function (a) {
             return a.ccode == addconarr[0].ccode;
         });
         if (arr.length > 0) {
             alert('Already Exist');
         }
         else {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "Company_Creation.aspx/SaveCountry",
                 data: "{'sXML':'" + sxml + "'}",
                 dataType: "json",
                 success: function (data) {

                     Country.push
                         ({
                             ccode: ccode,
                             sname: sname,
                             cname: cname
                         })
                     loadcountry();
                     $('#myModal').modal('hide');
                     $('#cname').val('');
                     $('#ccode').val('');
                     $('#csname').val('');

                 },
                 error: function (result) {
                 }
             });
         }


     }
     
     $('#Closebtn').on("click", function () {
         $('#sname').val('');
         $('#ccode').val('');
         $('#sshname').val('');
     });

     function addsRo() {
         var country = $('#cntry').val();
         if (country == 0) {
             alert('select the Country');
             $('#cntry').focus();
             return false;
         }
         else
         {
             $('#addstate').modal('toggle');
         }
         
     }
     function Savestate() {
         var addstaarr = [];

         var sname = document.getElementById("sname").value;
         if (sname == '') {
             alert('Enter the New State Name');
             return false;
         }
         var sshname = document.getElementById("sshname").value;
         if (sshname == '') {
             alert('Enter the State short name');
             return false;
         }
         var cscode = document.getElementById("cscode").value;
         if (cscode == '') {
             alert('Enter the country code');
             return false;
         }
         
         addstaarr.push({
             sname: sname,
             cscode: cscode,
             sshname: sshname
         })
          var DT = new Date();
         DT = moment(DT).format("YYYY-MM-DD HH:mm:ss");

         let sxml = "<ROOT>";
         for (let i = 0; i < addstaarr.length; i++) {
             addstaarr[0].DT = DT
             sxml += `<TP SNAME="${addstaarr[i]["sname"]}" SSHNAME="${addstaarr[i]["sshname"]}" SDT="${addstaarr[i]["DT"]}" CCODE="${addstaarr[i]["cscode"]}"  />`;
         }
         sxml += "</ROOT>";

         var arr = state.filter(function (b) {
             return b.sname == addstaarr[0].sname;
         });
         if (arr.length > 0) {
             alert('Already Exist');
         }
         else {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "Company_Creation.aspx/SaveState",
                 data: "{'sXML':'" + sxml + "'}",
                 dataType: "json",
                 success: function (data) {

                     state.push
                         ({
                             scode: cscode,
                             sshname: sshname,
                             sname: sname
                         })
                     loadstates();
                     $('#addstate').modal('hide');
                     $('#sname').val('');
                     $('#cscode').val('');
                     $('#sshname').val('');

                 },
                 error: function (result) {
                 }
             });
         }


     }


     //http://jsfiddle.net/cse_tushar/9EKRB/1
     var txtboxx = '<tr><td><div class="confrbil"><input type="text" class="col-xs-6 name" name="name" style="height:35px;width:250px;margin-left:-10px;font-size: medium;" /><input type="text" name="mobile" class="col-xs-6" style="height: 35px; width:250px; font-size: medium;margin-left:75px" onkeypress="return event.charCode >= 48 && event.charCode <= 57" /><input type="text" name="gmail" class="col-xs-6" style="height:35px;width:250px;margin-left:75px;font-size: medium;"/><input type="image" style="margin-left:55px;margin-top:0px;width:3%;" id="remobtn"  src="https://img.icons8.com/fluency/344/delete-sign.png" class="btnDelet"/></div></td></td>';

     function addRoww(btn) {
         $('#cfb').append(txtboxx);
     }
     $("#cfb").on('click', '.btnDelet', function () {
         $(this).closest('tr').remove();
     });
     var txtbox = '<tr><td><div class="confrdat"><input type="text" class="col-xs-6 name" name="names" style="height:35px;width:250px;margin-left:-10px;font-size: medium;" /><input type="text" name="mobileno" class="col-xs-6" style="height: 35px; width:250px; font-size: medium;margin-left:75px" onkeypress="return event.charCode >= 48 && event.charCode <= 57" /><input type="text" name="mail" class="col-xs-6" style="height:35px;width:250px;margin-left:75px;font-size: medium;" /><input type="image" style="margin-left:55px;margin-top:0px;width:3%;" id="remobtn"  src="https://img.icons8.com/fluency/344/delete-sign.png" class="btnDelet"/></div></td></td>';

     function addRow(btn) {
         $('#cfd').append(txtbox);
     }
     $("#cfd").on('click', '.btnDelet', function () {
         $(this).closest('tr').remove();
     });

     function loadcountry() {
         $.ajax({
             type: "POST",
             contentType: "application/json; charset=utf-8",
             async: false,
             url: "Company_Creation.aspx/getcountry",
             data: "{'divcode':'<%=Session["div_code"]%>'}",
             dataType: "json",
             success: function (data) {
                 Country = JSON.parse(data.d) || [];
                 if (Country.length > 0) {
                     $("#cntry").empty();
                     $("#cntry").selectpicker("destroy");
                     var Countries = $("#cntry");
                     Countries.empty().append('<option selected="selected" value="0" style="width:350px">Select Country</option>');
                     for (var i = 0; i < Country.length; i++) {
                         Countries.append($('<option value="' + Country[i].ccode + '">' + Country[i].cname + '</option>'))
                     }
                 }
             }
         });
         $('#cntry').selectpicker({
             liveSearch: true
         });

     }

     function loadstates() {
         var ccode = $('#cntry option:selected').val();
         $.ajax({
             type: "POST",
             contentType: "application/json; charset=utf-8",
             async: false,
             url: "Company_Creation.aspx/getstates",
             data: "{'ccode':'" + ccode + "'}",
             dataType: "json",
             success: function (data) {
                 state = JSON.parse(data.d) || [];
                 if (state.length > 0) {
                     $("#ste").empty();
                     $("#ste").selectpicker("destroy");
                     var states = $("#ste");
                     states.empty().append('<option selected="selected" value="0" style="width:350px">Select state</option>');
                     for (var i = 0; i < state.length; i++) {
                         states.append($('<option value="' + state[i].scode + '">' + state[i].sname + '</option>'))
                     }
                 }
                 else
                 {
                    $("#ste").empty();
                     $("#ste").selectpicker("destroy");
                     var states = $("#ste");
                     states.empty().append('<option selected="selected" value="0" style="width:350px">Add state</option>');
                 }
             }
         });
         $('#ste').selectpicker({
             liveSearch: true
         });

     }




     $(document).ready(function () {
         loadcountry();
         function clearfields() {
             $(document).find('input[name="name"]').val('');
             $(document).find('input[name="mobile"]').val('');
             $(document).find('input[name="gmail"]').val('');
             $(document).find('.name').closest(".confrbil").remove();
             $(document).find('input[name="names"]').val('');
             $(document).find('input[name="mobileno"]').val('');
             $(document).find('input[name="mail"]').val('');
             $(document).find('.name').closest(".confrdat").remove();
             $('#cmpny').val('');
             $('#code').val('');
             $('#addr').val('');
             
             $('#city').val('');
             $('#gstno').val('');
             $('#url').val('');
             
             $('#pin').val('');
             $('#myFile').val('');
             $('#bilnme').val('');
             $('#bilmod').val('');
             $('#mnt').val('');
             $('#amt').val('');
             $('#usr').val('');
             $('#type').val('');
             $('#ran').val('');
             $('#acu').val('');
             $('#uplfile').attr('src', 'https://seeklogo.com/images/B/business-company-logo-C561B48365-seeklogo.com.png');
             loadcountry();
             loadstates();
         }
         $('#btnsubmit').on('click', function () {

             var cmpnyname = $('#cmpny').val();
             if (cmpnyname == '') {
                 alert('Enter the Company Name');
                 $('#cmpny').focus();
                 return false;
             }
             var code = $('#code').val();
             if (code == '') {
                 alert('Enter the Company Code');
                 $('#code').focus();
                 return false;
             }
             var logo_Img = $('#uplfile').val();

             if ($('#uplfile').val() == '') {
                 alert('Attach The logo');
                 $('#uplfile').focus();
                 return false;
             }
             logo_Img = document.getElementById("uplfile").files[0].name;


             var address = $('#addr').val();
             if (address == '') {
                 alert('Enter the Address');
                 $('#addr').focus();
                 return false;
             }

             var country = $("#cntry :selected").text();
             if (country == 0) {
                 alert('select the Country');
                 $('#cntry').focus();
                 return false;
             }
             var city = $('#city').val();
             if (city == '') {
                 alert('Enter the City');
                 $('#city').focus();
                 return false;
             }
             var gstno = $('#gstno').val();
             if (gstno == '') {
                 alert('Enter the GSTN.No');
                 $('#gstno').focus();
                 return false;
             }

             var purl = $('#url').val();
             if (purl == '') {
                 alert('Enter the Url');
                 $('#url').focus();
                 return false;
             }
             var state = $("#ste :selected").text();
             if (state == 0) {
                 alert('Select  the State');
                 $('#ste').focus();
                 return false;
             }
             var pincode = $('#pin').val();
             if (pincode == '') {
                 alert('Enter the Pincode');
                 $('#pin').focus();
                 return false;
             }
             var prop = $('#myFile').val();

             if ($('#myFile').val() == '') {
                 alert('Attach The Proposal');
                 $('#myFile').focus();
                 return false;
             }
             prop = document.getElementById("myFile").files[0].name;

             var billname = $('#bilnme').val();
             if (billname == '') {
                 alert('Enter the Billng Name');
                 $('#bilnme').focus();
                 return false;
             }
             var nousr = $('#usr').val();
             if (nousr == '') {
                 alert('Enter the No.Of.User');
                 $('#usr').focus();
                 return false;
             }
             var billmod = $('#bilmod').val();
             if (billmod == 0) {
                 alert('select the Billing of Mode');
                 $('#bilmod').focus();
                 return false;
             }

             var biltype = $('#type').val();
             if (biltype == 0) {
                 alert('select the Billing Type');
                 $('#type').focus();
                 return false;
             }
             var month = $('#mnt').val();
             if (month == '') {
                 alert('Enter the Starting month');
                 $('#mnt').focus();
                 return false;
             }
             var amount = $('#amt').val();
             if (amount == '') {
                 alert('Enter the Amount');
                 $('#amt').focus();
                 return false;
             }
             var range = $('#ran').val();
             if (range == '') {
                 alert('Enter the Range');
                 $('#ran').focus();
                 return false;
             }
             var acost = $('#acu').val();
             if (acost == '') {
                 alert('Enter the Addition Cost per User');
                 $('#acu').focus();
                 return false;
             }
             var cfbarr = [];
             $('.confrbil').each(function () {
                 var bname = $(this).find('input[name="name"]').val();
                 if (bname == '') {
                     alert('Enter the contact Name for Billing');
                     return false;
                 }
                 var bmobile = $(this).find('input[name="mobile"]').val();
                 if (bmobile == '') {
                     alert('Enter the contact mobile no for Billing');
                     return false;
                 }
                 var bgmail = $(this).find('input[name="gmail"]').val();
                 if (bgmail == '') {
                     alert('Enter the contact mobile no for Billing');
                     return false;
                 }
                 cfbarr.push({
                     bname: bname,
                     bmobile: bmobile,
                     bmail: bgmail
                 })

             });

             var cfdarr = [];
             $('.confrdat').each(function () {
                 var dname = $(this).find('input[name="names"]').val();
                 if (dname == '') {
                     alert('Enter the contact Name for data upload');
                     return false;
                 }
                 var dmobile = $(this).find('input[name="mobileno"]').val();
                 if (dmobile == '') {
                     alert('Enter the contact mobile no for data upload');
                     return false;
                 }
                 var dgmail = $(this).find('input[name="mail"]').val();
                 if (dgmail == '') {
                     alert('Enter the contact mobile no for data upload');
                     return false;
                 }
                 cfdarr.push({
                     dname: dname,
                     dmobile: dmobile,
                     dmail: dgmail
                 })
             });

             var usrID ="<%=Session["sf_code"]%>";
             var usrName = "<%=Session["sf_name"]%>";

             var DT = new Date();
             DT = moment(DT).format("YYYY-MM-DD HH:mm:ss");


             data = { "COMPANY_NAME": cmpnyname, "CODE": code, "logo_Img": logo_Img, "URL": purl, "ADDRESS": address, "COUNTRY": country, "State": state, "CITY": city, "Pincode": pincode, "ID": usrID, "USR_Name": usrName, "Req_DATE": DT, "Bill_Name": billname, "NoOfUser": nousr, "BillMode": billmod, "Type": biltype, "StartMn": month, "RngVal": range, "Amount": amount, "AddUserCost": acost, "GSTNO": gstno, "Proposalpath": prop, "CBArr": cfbarr, "CDArr": cfdarr }
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "Company_Creation.aspx/savecompanycreate",
                 data: "{'data':'" + JSON.stringify(data) + "'}",
                 dataType: "json",
                 success: function (data) {
                     var successtext = data.d;
                     if (successtext == "Company Profile Created") {
                         alert('Company Profile Created');
                         clearfields();
                         window.location.reload();
                     }
                     else {
                         alert(successtext);
                     }
                 }
             });


         });
     });



 </script>
    </body>
    </html>



</asp:Content>

