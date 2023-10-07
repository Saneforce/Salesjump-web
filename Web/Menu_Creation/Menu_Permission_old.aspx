<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Menu_Permission.aspx.cs" Inherits="Menu_Creation_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

           
            bindComdrop();
            var ss = [];
            var menuids = [];
            var menurights = '';   
            var str = '';
            $('label[for="inputDesgname"]').hide();
			$('#btn').attr('disabled', true);
            $('#btn').click(function () {
                debugger;
                var comname = $('#ddlcomname :selected').text();
                var comname1 = $('#ddlcomname :selected').val();
                //var desgname = $('#chkrole :checked').length;
                //var desgname = $('#chkrole :checked').val();
                //var desgname1 = $('#d1 :checked').length;

                if (comname == "--Select Company Name--") {
                    alert("Please Select Company Name.");
                    return false;
                }
                //if (desgname == undefined) {
                //    alert("Please select Designation Name");
                //    return false;
                //}
                //if (desgname1 == 0) {
                //    alert("Please select Menu List");
                //    return false;
                //}

                var valuesArray2 = $('input[name=check2]:checked').map(function () {
                    return $(this).attr('id');
                }).get().join(',');

                var valuesArray3 = $('input[name=check3]:checked').map(function () {
                    return $(this).attr('id');
                }).get().join();

                var result = valuesArray2.concat(",");
                var f = result.concat(valuesArray3);

                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    data: "{'companyid':'" + comname1 + "','companyname':'" + comname + "','arr':'" + f + "'}",
                    url: "Menu_Permission.aspx/savedata",
                    dataType: "json",
                    success: function () {           
                        alert("Menu List has been Added successfully");                                                            
                        $('input[name=check1]:checked').removeAttr('checked');
                        $('input[name=check2]:checked').removeAttr('checked');
                        $('input[name=check3]:checked').removeAttr('checked');
                        $("#ddlcomname")[0].selectedIndex = 0;
                        $('.sss').removeAttr('checked');
                        $('#chkrole').hide();
                        $('label[for="inputDesgname"]').hide();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

            });

    
            $('#chkrole,.ddd').click(function () {

                //var desgname = $(this)('input[name=check1]:checked').val();
              //  var checkedValue = $('.ddd:checked').val();
               // $("input[name='sport']:checked")
                var c = $(':checked', this).val();
               // var card_type = $(this).filter(':checked').val();

                    //var c = $(this).attr('checked');
                    //var desid = $(this).find(".ddd").val();            
                    var d = $('#ddlcomname :selected').val();



                    //if ($("input[type='radio'].ddd").is(':checked')) {

                    //}

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        data: "{'Cid':'" + d + "','Did':'" + c + "'}",
                        url: "Menu_Permission.aspx/GetByDesginID",
                        dataType: "json",
                        success: function (data) {

                            if (data.d.length > 0) {

                                var v = ss.d.length;

                                for (var i = 0; i < ss.d.length; i++) {

                                    for (var j = 0; j < data.d.length; j++) {

                                        if (ss.d[i].Menu_ID == data.d[j].Menu_ID) {

                                            $('input[type="checkbox"]input[id="' + data.d[j].Menu_ID + '"]').prop("checked", "checked");

                                        }
                                    }
                                }
                            }
                            else {

                                $('input[name=check2]:checked').removeAttr('checked');
                                $('input[name=check3]:checked').removeAttr('checked');
                                
                            }

                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }

                    });
                
            });

           



            //$('#ddlcomname').change(function () {

            //    $('label[for="inputDesgname"]').show();
            //    $('#chkrole').show();
            //    var desid = $(this).children("option:selected").val();
                
            //    $.ajax({
            //        type: "Post",
            //        contentType: "application/json; charset=utf-8",
            //        data: "{'degnid':'" + desid + "'}",
            //        url: "Menu_Permission.aspx/binddegndrop",
            //        dataType: "json",
            //        success: function (data) {

            //            $('#chkrole').empty();
                      
            //            var str = "<div class='row'>";
            //            for (var i = 0; i < data.d.length; i++) {
            //                if ((i + 1) % 2) {
            //                    str += "</div><div class='row'>";
            //                }
            //                str += "<div class='col-sm-6' style='text-align: left;overflow: hidden;'><label class='checkbox-inline'>";
            //                str += "<input type='radio' class='ddd' name='check1' value=" + data.d[i].desigcode + " id=" + data.d[i].desigcode + " />" + data.d[i].designame + "";
            //               // str += "<input type='checkbox' name='check1' value=" + data.d[i].desigcode + " id=" + data.d[i].desigcode + " />" + data.d[i].designame + "";
            //                str += "</label></div>";
            //            }
            //            str += "</div>";
            //            $('#chkrole').append(str);
            //        },
            //        error: function (result) {
            //            alert(JSON.stringify(result));
            //        }
            //    });
            //});
            $('#ddlcomname').change(function () {

                var comname1 = $('#ddlcomname :selected').val();
               
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    data: "{'comname':'" + comname1 + "'}",
                    url: "Menu_Permission.aspx/findmenuids",
                    dataType: "json",
                    success: function (data) {
                        menuids = JSON.parse(data.d);
                        menurights = '';
                        for (var j = 0; j < menuids.length; j++) {
                            menurights=menuids[j].Menu_IDs;
                        }
                        
                        getdata();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });

            function bindComdrop() {

                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Menu_Permission.aspx/bindcomdrop",
                    dataType: "json",
                    success: function (data) {
                        $.each(data.d, function (data, value) {

                            $('#ddlcomname').append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            };

            function getdata() {
           
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Menu_Permission.aspx/Display",
                    dataType: "json",
                    success: function (data) {
                        $('#d1').html('');
                        var MainMnu = data.d.filter(function (a) {
                            return a.Menu_Type == '0';
                        });           
                        ss = data;
                        var str = "<table style='border-collapse: separate;border-spacing: 16px; width:100%;'><tr>";
                        for (var i = 0; i < MainMnu.length; i++) {
                            var mchecked = ((menurights.indexOf(',' + MainMnu[i].Menu_ID + ',')) > -1 ? 'checked' : '');
                            str += "<td class='tdclass' id=" + MainMnu[i].Menu_ID + " style='background: #FFFFFF;border: 1px solid #e6e6e6; width:30%;padding: 0px;box-shadow: 0px 2px 1px #0000001f;text-align: left;' valign='top'><div class='divclass1' id=" + MainMnu[i].Menu_ID + " style='background: #19a4c6; padding: 5px; box-shadow: 0px 3px 9px -2px #0000007a;color:#fff;'><label><input type='checkbox'  class='sss' name='check2' value=" + MainMnu[i].Menu_ID + " id=" + MainMnu[i].Menu_ID + " " + mchecked + " /> " + MainMnu[i].Menu_Name + "</label></div><div name='mas'  value=" + MainMnu[i].Menu_ID + " class='" + MainMnu[i].Menu_ID + "' id=" + MainMnu[i].Menu_ID + " style='padding:10px 0px;'>";
                            var s = data.d.filter(function (a) {
                                return (a.Parent_Menu == MainMnu[i].Menu_ID)
                            });
                            for (var j = 0; j < s.length; j++) {
                                var schecked = ((menurights.indexOf(',' + s[j].Menu_ID + ',')) > -1 ? 'checked' : '');

                                if (s[j].Menu_Type == 1) {
                                    str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (s[j].lvl - 1))) + "px'><label><input type='checkbox' id= " + s[j].Menu_ID + "  value=" + s[j].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check2' title=" + MainMnu[i].Menu_ID + " " + schecked + " /> " + s[j].Menu_Name + "</label></div>";

                                    var h = data.d.filter(function (a) {
                                        return (a.Parent_Menu == s[j].Menu_ID)
                                        // return (a.Parent_Menu == MainMnu[i].Menu_ID)
                                    });

                                    for (var r = 0; r < h.length; r++) {
                                        var hchecked = ((menurights.indexOf(',' + h[r].Menu_ID + ',')) > -1 ? 'checked' : '');

                                        str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (h[r].lvl - 1))) + "px'><label  style='font-weight:normal;'><input type='checkbox' id= " + h[r].Menu_ID + "  value=" + h[r].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check3' title=" + MainMnu[i].Menu_ID + " " + hchecked + " /> " + h[r].Menu_Name + "</label></div>";

                                    }
                                }
                                else if (s[j].Menu_Type == 2) {
                                    str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (s[j].lvl - 1))) + "px'><label><input type='checkbox' id= " + s[j].Menu_ID + "  value=" + s[j].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check2' title=" + MainMnu[i].Menu_ID + " " + schecked + " /> " + s[j].Menu_Name + "</label></div>";

                                    var n = data.d.filter(function (a) {
                                        return (a.Parent_Menu == s[j].Menu_ID)
                                        // return (a.Parent_Menu == MainMnu[i].Menu_ID)
                                    });

                                    for (var r = 0; r < n.length; r++) {
                                        var hchecked = ((menurights.indexOf(',' + n[r].Menu_ID + ',')) > -1 ? 'checked' : '');

                                        str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (h[r].lvl - 1))) + "px'><label  style='font-weight:normal;'><input type='checkbox' id= " + n[r].Menu_ID + "  value=" + n[r].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check3' title=" + MainMnu[i].Menu_ID + " " + hchecked + " /> " + n[r].Menu_Name + "</label></div>";

                                    }
                                }


                                //str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (s[j].lvl - 1))) + "px'><input type='checkbox' id= " + s[j].Menu_ID + "  value=" + s[j].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check2' title=" + MainMnu[i].Menu_ID + "  />" + s[j].Menu_Name + "</div>";
                            }
                            str += "</div></td>";
                            if (((i + 1) % 3) == 0) {
                                str += "</tr></tr>";
                            }
                        }
                        $('#d1').append(str);

                        $('.sss').click(function () {
                            
                            var b = $(this).attr('value');
                            var c = $(this).attr('checked') ? true : false;
                            $('.cchk' + b).each(function () {
                                $(this).prop("checked", c);
                            });
                        });                         

                        $('input[name=check2]').click(function () {
                            
                            var m = $(this).attr('id');
                            var u = $(this).attr('title');
                         
                            var c = $(this).attr('checked') ? true : false;                                                                               
                                $('input[value="' + m + '"][name=check3]').each(function () {
                                    $(this).prop("checked", c);
                                });
                            $('input[value="' + u + '"].sss').prop("checked", "checked");
                        });

                        $('input[name=check3]').click(function () {

                            var m = $(this).attr('value');
                            var c = $(this).attr('checked') ? true : false; 
                            var u = $(this).attr('title');
                            $('input[value="' + u + '"].sss').prop("checked", "checked");
                            $('input[id="' + m + '"][name=check2]').each(function () {
                                $(this).prop("checked", "checked");
                            });
                        });
						$('#btn').attr('disabled', false);						
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            };
        });


    </script>

    <form id="formid" class="form-horizontal" runat="server">
        <center>
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <br />
                <br />
                <div style="margin-right: 175px; margin-left: 265px;" class="form-group row ">
                    <label for="inputcomname" class="col-sm-2 col-form-label">Company Name</label>

                    <div class="col-sm-10">
                        <select id="ddlcomname">
                            <option value="0">--Select Company Name--</option>
                        </select>
                    </div>
                </div>
                <div style="margin-right: 170px; margin-left: 268px; margin-top: -20px;display:none" class="form-group row" >
                    <label for="inputDesgname" class="col-sm-2 col-form-label">Designation Name</label>
                    <div class="col-sm-10">
                        <div id="chkrole" style="width: 553px;padding-left:50px;">
                        </div>
                    </div>
                </div>

                <div id="d1" style="text-align: left; padding-left: 100px;">
                </div>
                <br />
              

                <input type="button" class="btn btn-primary" value="Submit" id="btn" />

              
            </div>
        </center>
    </form>
</asp:Content>

