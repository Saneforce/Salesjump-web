<%@ Page Title="" Language="C#" MasterPageFile="~/Billing.master" AutoEventWireup="true" CodeFile="Company_Approval_form.aspx.cs" Inherits="MasterFiles_Company_Approval_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                border:0px none;
                background-color: initial;
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
            <div class="col-lg-12 header" style="font-size:25px;">
                Company Approval
           
            </div>
        </div>
        <form style=" margin-top: 5px;">
            <div class="container"style="overflow: hidden";>
                <div class="container">
                <div  class="col-md-5"  style="right: -25px; margin-top:16px;">

                    <div class="mb-3">
                        <label for="cmpny" style="font-size: small" class="form-label">Company Name :</label>
                        <input style="width:300px;float:right;margin-top:-5px;height:30px; font-size: small;border:0px none;background-color: initial;" id="cmpny" type="text" readonly  /><br>
                        
                        
                    </div>
                    <div class="mb-3">                     
                        <label for="code" style="font-size: small;margin-top:5px" class="form-label">Code</label>
                            <input  style="height: 30px;width:150px;margin-left:145px;margin-top:-20px;height:30px;border:0px none;background-color: initial; font-size: small" id="code" type="text"  readonly />
                     
                        
                    </div>
                    <div class="mb-3">
                        <label for="addr" style="font-size: small" class="form-label">Address</label>
                        <textarea name="Text1" style="font-size: small;margin-left:90px;width:235px;background-color: initial;" cols="40" id="addr" rows="4" readonly></textarea><br>
                        <br>    
                           
                    </div>
                    <div class="mb-3"style=" margin-top:-15px;">
                        <label for="cntry" style="font-size: small" class="form-label">Country</label>
                        <input  name="country" id="cntry" style="width:300px;float:right;margin-top:-5px;height:30px;background-color: initial; font-size: small" readonly ></input><br>
                        
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: 10px">
                        <label for="city" style="font-size: small" class="form-label">City</label>
                        <input  style="width:300px;float:right;margin-top:-5px;height:30px; background-color: initial;font-size: small" id="city"   readonly /><br>
                        
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: 10px">
                        <label for="gstno" style="font-size: small" class="form-label">GSTN.No</label>
                        <input style="width:300px;float:right;margin-top:-5px;height:30px; background-color: initial;font-size: small" id="gstno"  readonly  /><br>
                        
                    </div>



                    
                </div>




                <div class="col-md-8" style="margin-top:-313px;float:right;left:131px;">
                    <div style="position:absolute;margin-top:80px">

                     <div class="mb-3" style="position:static">
                         <label for="Logo" style="font-size: small" class="form-label">Company Logo</label>
                            <img id="upimg" style="width: 100px; margin-left: 100px;margin-top:25px; /* height: 0%; */" src="https://seeklogo.com/images/B/business-company-logo-C561B48365-seeklogo.com.png" />
                     <%--   </td>--%>
                    </div></div>
                     <div style="margin-top:185px;position:static">                  
                        
                   
                    <div class="mb-3"style="margin-top:25px;">
                        <label for="url" style="font-size: small" class="form-label">Preferred URL</label>
                        <input  style="width:250px;margin-left:34px;height:30px;background-color: initial; font-size: small" id="url"  readonly /><br>
                       
                    </div>
                    <div class="mb-3"style="margin-top:0px;">
                        <label for="ste" style="font-size: small" class="form-label">State</label>                      
                        <input  name="state" id="ste" style="width:250px;margin-left:91px;height:30px;background-color: initial;font-size: small" readonly /></input>
                        
                        
                    </div>
                    <div class="mb-3"style="margin-top:0px;">
                        <label for="pin" style="font-size: small" class="form-label">Pincode</label>
                        <input style="width:250px;margin-left:72px;height:30px;background-color: initial; font-size: small" id="pin"  readonly />                        
                    </div>
                    <div class="mb-3"style="margin-top:0px;">
                        <label for="atp" style="font-size: small;" class="form-label">Attach Proposal</label>
                        <input style="width:250px;margin-left:25px;height:30px;background-color: initial; font-size: small" id="myFile" name="filename" readonly />
                        <button  class="btn" style="background-color:lavender;margin-left:25px;height:30px" onclick="DownloadFile()" autopostback="false" ><i  style="height:20px;" class="fa fa-download"></i> </button><br>
                        <br>
                        <br>
                    </div>  </div>                
                     </div>
                </div>
                
                <div>
                    
                <div class="mb-3" style="top: 100px; margin-top: -35px">
                        <label for="bil" style="font-size: 20px" class="form-label">Billing Details</label><hr  style="width:100%;overflow: visible; /* For IE */
                            padding: 0;
                            border: none;
                            border-top: medium double #333;
                            color: #0c499f; margin-top: 0px;
                            text-align: center; "/<br>
                    </div>
                    <div class="col-md-5" style="right: -42px; margin-top:-28px;">
                    <div class="mb-3" style="top: 100px; margin-top: 15px">
                        <label for="bilnme" style="font-size: small" class="form-label">Billing Name</label>
                        <input style="width:300px;float:right;margin-top:-5px;height:30px; font-size: small" id="bilnme"  readonly/><br>
                        <br>
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: -8px">
                        <label for="bilmod" style="font-size: small" class="form-label">Billing Mode</label>
                        <input name="Mode" style="width:300px;float:right;margin-top:-5px;height:30px; font-size: small" id="bilmod" readonly>                                                    
                        </input><br>
                        <br>
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: -8px">
                        <label for="mnt" style="font-size: small" class="form-label">Start Month</label>
                        <input style="width:300px;float:right;margin-top:-5px;height:30px; font-size: small" id="mnt"  readonly/><br>
                        <br>
                    </div>
                    <div class="mb-3" style="top: 100px; margin-top: -8px">
                        <label for="amt" style="font-size: small" class="form-label">Amount</label>
                        <input style="width:300px;float:right;margin-top:-5px;height:30px; font-size: small" id="amt" readonly /><br>
                        <br>
                        <br>
                    </div>
                        </div>
                    <div class="col-md-8" style="margin-top:-187px;float:right;left:147px;"">
                    <div class="mb-3">
                        <label for="usr" style="font-size: small; margin-top: 38px;" class="form-label">No of Users</label>
                        <input style="width:225px;margin-left:65px;height:30px;font-size: small" id="usr"  readonly /><br>
                       
                    </div>
                    <div class="mb-3" style="margin-top:0px">
                        <label for="usr" style="font-size: small; margin-top: 0px;" class="form-label">Type</label>
                        <input style="width:225px;margin-left:111px;height:30px;font-size: small" id="type" style="font-size: small;"  readonly> </input>                    
                        
                    </div>
                    <div class="mb-3" style="margin-top:0px">
                        <label for="ran" style="font-size: small;margin-top:5px;" class="form-label">Range</label>
                        <input style="width:225px;margin-left:101px;height:30px;font-size: small" id="ran"  readonly oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');" /><br>
                        
                    </div>
                    <div class="mb-3" style="margin-top:0px">
                        <label for="acu" style="font-size: small" class="form-label">Addition Cost/User</label>
                        <input style="width:225px;margin-left:24px;height:30px;font-size: small" id="acu"   readonly /><br>
                        
                    </div>
                        </div>
                      </div>


                <div style="margin-top:125px;float:none">
                <div  style="top: 100px; margin-top: 15px;width: 100%;">
                        <label for="bil" style="font-size:20px;margin-left:-475px" class="form-label">Contacts For Billing</label>
                    <hr style="width:100%;overflow: visible;padding: 0;border: none;border-top: medium double #333;margin-top:05px;color: #0c499f; text-align: center; "/>
                   
                    </div>
                  <div class="mb-3" style="margin-left:50px; margin-top:-1px;">
                      <table id="cfb">
                          <tbody>
                      <tr>
                        <label for="name" style="font-size: small;margin-left:05px" class="form-label">Name</label>
                          <label for="no" style="font-size: small;margin-left:285px" class="form-label">Mobile</label>
                          <label for="no" style="font-size: small;margin-left:285px" class="form-label">Email</label>
                        

                      </tr>
                      <tr>
                      <td>
                          <div class="confrbil" >                              
                             </div> </td>  
                    </tr></tbody></table>
                      </div>               
                    </div>
                <div style="margin-top: -15px;float: left; width:100%">                   
                <div  style="top: 100px;; margin-top: -15px">
                        <label for="bil" style="font-size: 20px;margin-left:0px" class="form-label">Contacts For Data Upload</label><hr 
                            style="width:100%;
                            overflow: visible;
                            padding: 0;
                            border: none;
                            margin-top: 5px;
                            border-top: medium double #333;
                            color: #0c499f;
                            text-align: center; "/>
                    </div>
                        
                  <div class="mb-3" style="margin-left:50px; margin-top:-1px;">
                      <table id="cfd"><tbody>
                      <tr>
                        <label for="name" style="font-size: small;margin-left:05px" class="form-label">Name</label>
                          <label for="no" style="font-size: small;margin-left:285px" class="form-label">Mobile</label>
                          <label for="no" style="font-size: small;margin-left:285px" class="form-label">Email</label>
                       

                      </tr>
                         
                      <tr>
                      <td><div class="confrdat">                       
                      
                        </div></td>  
                    </tr>
                      </tbody></table>
                      </div>
                    </div><br />
                <div style="margin-top: 100px;">
    
                    <input type="checkbox" id="payCheck" name="check" style="width:20px;margin-left:100px" value="checked"><label for="payment" style="font-size:15px"> Payment Received</label> 
                    <button style="margin-left: 500px;margin-top: 25px;height:32px;" type="button" id="btnsubmit"  class="btn btn-success">Approve</button><br>
                    <input type="checkbox" id="datacheck" name="dcheck" style="display:none; width:20px;margin-left:100px" value="checked"><label for="data" style="font-size:15px;display:none"> Data Received</label><br>
                </div>
                <div><label for="data" style="font-size:15px">Status</label>
                    <div id="idStatus">

                                                                                    </div> </div>
                <footer style="height:50px">
                    hai
                </footer>
            </div>

            


            
        </form>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            var propic_name = ' ';
            var AddFields = [];
            var ComDets = [];
            var Comp_id = '<%=Comp_id%>';
            $sSiteUrl = "";
            function getParameterByName(Comp_id, url) {
                if (!url) url = window.location.href;
                Comp_id = Comp_id.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + Comp_id + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }



            $(document).ready(function () {
                getParameterByName(Comp_id, url);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Company_Approval_form.aspx/getCert",
                    data: "",
                    dataType: "json",
                    success: function (data) {
                        console.log( data.d);
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Company_Approval_form.aspx/getcompdets",
                    data: "{'Comp_id':'" + Comp_id + "'}",
                    dataType: "json",
                    success: function (data) {
                        ComDets = data.d;
                        $('#cmpny').val(ComDets[0].cmpnyname);
                        $('#code').val(ComDets[0].code);
                        $('#addr').val(ComDets[0].address);
                        if (ComDets[0].logo_Img != null && ComDets[0].logo_Img != '' && ComDets[0].logo_Img != ' ') {
                            $('#upimg').attr('src', 'http://fmcg.sanfmcg.com//limg/' + ComDets[0].logo_Img);
                            propic_name = ComDets[0].logo_Img;
                        }
                        else {
                            $('#upimg').attr('src', 'https://seeklogo.com/images/B/business-company-logo-C561B48365-seeklogo.com.png')
                            propic_name = null;
                        }

                        $sSiteUrl = ComDets[0].purl;
			            $sCmpName = ComDets[0].cmpnyname;
			            $sSHNM = ComDets[0].code;
			            $sStates = ComDets[0].statecd;
			            $sCountryCd = ComDets[0].cntryId;
                        $iUserCnt = ComDets[0].nousr;
                        $iCnt = ComDets[0].nousr;
			            $iRate = ComDets[0].amount;
			
                        $('#cntry').val(ComDets[0].country);
                        $('#city').val(ComDets[0].city);
                        $('#gstno').val(ComDets[0].gstno);
                        $('#url').val(ComDets[0].purl);
                        $('#ste').val(ComDets[0].state);
                        $('#pin').val(ComDets[0].pincode);
                        $('#myFile').val(ComDets[0].prop);
                        $('#bilnme').val(ComDets[0].billname);
                        $('#bilmod').val(ComDets[0].billmod);
                        $('#mnt').val(ComDets[0].month);
                        $('#amt').val(ComDets[0].amount);
                        $('#usr').val(ComDets[0].nousr);
                        $('#type').val(ComDets[0].biltype);
                        $('#ran').val(ComDets[0].range);
                        $('#acu').val(ComDets[0].acost);

                        for (var i = 1; i < ComDets.length; i++) {
                            var bname = "bname" + i;
                            var bmobile = "bmobile" + i;
                            var bgmail = "bgmail" + i;
                            if (ComDets[i].bname != null) {
                                var newTr = '<div class="row" style="margin-top:-42px"><input class="col-xs-6" name="name" style="height: 30px; width:250px; font-size: small;margin-left:-10px" readonly id=' + bname + ' value="' + ComDets[i].bname + '" /><input class="col-xs-6" name="mobile" style="height: 30px; width:250px; font-size: small;margin-left:70px" readonly id=' + bmobile + ' value="' + ComDets[i].bmobile + '" /><input class="col-xs-6" name="gmail" style="height: 30px; width:250px; font-size: small;margin-left:75px" readonly id=' + bgmail + ' value="' + ComDets[i].bgmail + '" /></div>';
                                $('.confrbil').append(newTr);
                            }
                        }
                        for (var i = 1; i < ComDets.length; i++) {
                            var dname = "dname" + i;
                            var dmobile = "dmobile" + i;
                            var dgmail = "dgmail" + i;
                            if (ComDets[i].dname != null) {
                                var newTr = '<div class="row" style="margin-top:-42px"><input class="col-xs-6" name="names" style="height: 30px; width:250px; font-size: small;margin-left:-10px" readonly id=' + dname + ' value="' + ComDets[i].dname + '" /><input class="col-xs-6" name="mobileno" style="height: 30px; width:250px; font-size: small;margin-left:70px" readonly id=' + dmobile + ' value="' + ComDets[i].dmobile + '" /><input class="col-xs-6" name="mail" style="height: 30px; width:250px; font-size: small;margin-left:75px" readonly id=' + dgmail + ' value="' + ComDets[i].dgmail + '" /></div>';
                                $('.confrdat').append(newTr);
                            }
                        }

                    }
                });


                $('#btnsubmit').on('click', function () {
                    CreateSite();
                });


            });

            function DownloadFile() {
                fileName = ComDets[0].prop;
                Comp_id = ComDets[0].comp_id;
                $.ajax({
                    type: "POST",
                    url: "Company_Approval_form.aspx/DownloadFile",
                    data: '{fileName: "' + fileName + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        //Convert Base64 string to Byte Array.
                        var bytes = Base64ToBytes(r.d);

                        //Convert Byte Array to BLOB.
                        var blob = new Blob([bytes], { type: "application/octetstream" });

                        //Check the Browser type and download the File.
                        var isIE = false || !!document.documentMode;
                        if (isIE) {
                            window.navigator.msSaveBlob(blob, fileName);
                        } else {
                            var url = window.URL || window.webkitURL;
                            link = url.createObjectURL(blob);
                            var a = $("<a />");
                            a.attr("download", fileName);
                            a.attr("href", link);
                            $("body").append(a);
                            a[0].click();
                            $("body").remove(a);
                        }
                    }
                });
                event.preventDefault();    // to prevent the page reload
            };
            function Base64ToBytes(base64) {
                var s = window.atob(base64);
                var bytes = new Uint8Array(s.length);
                for (var i = 0; i < s.length; i++) {
                    bytes[i] = s.charCodeAt(i);
                }
                return bytes;
            };
            
	    $Webip="13.232.59.11";//129.154.40.185
	    $Appip="13.232.59.11";
            function CreateSite() {
                CreateDataBase();
                //CreateWebSite()
            }
            function CreateDataBase()
            {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "Company_Approval_form.aspx/CreateDB",
                    data: "{'IP':'" + $Webip + "','SiteName':'"+ $sSiteUrl +"','CmpName':'"+ $sCmpName +"','SHNM':'"+ $sSHNM +"','States':'"+ $sStates +"','CountryCd':'"+ $sCountryCd +"','UserCnt':'"+ $iUserCnt +"','Cnt':'"+ $iCnt +"','Rate':'"+ $iRate +"'}",
                    dataType: "json",
                    success: function (data) {
                        console.log("print:" + data);
                        $("#idStatus").html("Database Created...");
                        CreateWebSite();
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }
            function CreateWebSite()
            {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "Company_Approval_form.aspx/CreateWebSite",
                    data: "{'SiteName':'"+ $sSiteUrl +"', 'IP':'" + $Webip + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log("print:" + data);
                        Data = JSON.parse(data.d);
                        $("#idStatus").html($("#idStatus").html()+"<br>"+Data.Msg);
                        CreateAppSite()
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }
            function CreateAppSite()
            {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "Company_Approval_form.aspx/CreateWebSite",
                    data: "{'SiteName':'apps"+ $sSiteUrl +"', 'IP':'" + $Appip + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log("print:" + data);
                        Data = JSON.parse(data.d);
                        $("#idStatus").html($("#idStatus").html()+"<br>"+Data.Msg);
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }
            </script>

         </body>
    </html>
</asp:Content>

