<%@ Page Title="Missed Date Approve" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Missed_Date_Approve.aspx.cs" Inherits="MasterFiles_Missed_Date_Approve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <style type="text/css">
        input[type='text'], select, label {
            line-height: 22px;
            padding: 2px 2px;
            font-size: medium;
            border-radius: 7px;
            font-weight: normal;
        }

        fieldset.group {
            margin: 0;
            padding: 0;
            margin-bottom: 1.25em;
            padding: .125em;
        }

            fieldset.group legend {
                margin: 0;
                padding: 0;
                font-weight: bold;
                margin-left: 20px;
                font-size: 100%;
                color: black;
            }


        ul.checkbox {
            margin: 0;
            padding: 0;
            margin-left: 20px;
            list-style: none;
        }

            ul.checkbox li input {
                margin-right: .25em;
            }

            ul.checkbox li {
                border: 1px transparent solid;
                display: inline-block;
                width: 12em;
            }

                ul.checkbox li label {
                    margin-left:;
                }

                    ul.checkbox li label:hover,
                    ul.checkbox li label.focus {
                        background-color: lightyellow;
                        border: 1px gray solid;
                        width: 12em;
                    }
					ul.checkbox1 {
            margin: 0;
            padding: 0;
            /*margin-left: 20px;*/
            list-style: none;
        }
            ul.checkbox1 li input {
                margin-right: .25em;
            }
         ul.checkbox1 li {
              border: 1px transparent solid;
              display: inline-block;
              width: 12em;
                        }
           ul.checkbox1 li label {
               margin-left:;
               }
             ul.checkbox1 li label:hover,
             ul.checkbox1 li label.focus {
                    background-color: lightyellow;
                   border: 1px gray solid;
               }

        /*.funkyradio div {
            clear: both;
            overflow: hidden;
        }

        .funkyradio label {
            width: 100%;
            border-radius: 3px;
            border: 1px solid #D1D3D4;
            font-weight: normal;
        }

        .funkyradio input[type="radio"]:empty,
        .funkyradio input[type="checkbox"]:empty {
            display: none;
        }

            .funkyradio input[type="radio"]:empty ~ label,
            .funkyradio input[type="checkbox"]:empty ~ label {
                position: relative;
                line-height: 2.5em;
                text-indent: 3.25em;
                margin-top: 2em;
                cursor: pointer;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
            }

                .funkyradio input[type="radio"]:empty ~ label:before,
                .funkyradio input[type="checkbox"]:empty ~ label:before {
                    position: absolute;
                    display: block;
                    top: 0;
                    bottom: 0;
                    left: 0;
                    content: '';
                    width: 2.5em;
                    background: #D1D3D4;
                    border-radius: 3px 0 0 3px;
                }

        .funkyradio input[type="radio"]:hover:not(:checked) ~ label,
        .funkyradio input[type="checkbox"]:hover:not(:checked) ~ label {
            color: #888;
        }

            .funkyradio input[type="radio"]:hover:not(:checked) ~ label:before,
            .funkyradio input[type="checkbox"]:hover:not(:checked) ~ label:before {
                content: '\2714';
                text-indent: .9em;
                color: #C2C2C2;
            }

        .funkyradio input[type="radio"]:checked ~ label,
        .funkyradio input[type="checkbox"]:checked ~ label {
            color: #777;
        }

            .funkyradio input[type="radio"]:checked ~ label:before,
            .funkyradio input[type="checkbox"]:checked ~ label:before {
                content: '\2714';
                text-indent: .9em;
                color: #333;
                background-color: #ccc;
            }

        .funkyradio input[type="radio"]:focus ~ label:before,
        .funkyradio input[type="checkbox"]:focus ~ label:before {
            box-shadow: 0 0 0 3px #999;
        }

        .funkyradio-default input[type="radio"]:checked ~ label:before,
        .funkyradio-default input[type="checkbox"]:checked ~ label:before {
            color: #333;
            background-color: #ccc;
        }

        .funkyradio-primary input[type="radio"]:checked ~ label:before,
        .funkyradio-primary input[type="checkbox"]:checked ~ label:before {
            color: #fff;
            background-color: #337ab7;
        }

        .funkyradio-success input[type="radio"]:checked ~ label:before,
        .funkyradio-success input[type="checkbox"]:checked ~ label:before {
            color: #fff;
            background-color: #5cb85c;
        }

        .funkyradio-danger input[type="radio"]:checked ~ label:before,
        .funkyradio-danger input[type="checkbox"]:checked ~ label:before {
            color: #fff;
            background-color: #d9534f;
        }

        .funkyradio-warning input[type="radio"]:checked ~ label:before,
        .funkyradio-warning input[type="checkbox"]:checked ~ label:before {
            color: #fff;
            background-color: #f0ad4e;
        }

        .funkyradio-info input[type="radio"]:checked ~ label:before,
        .funkyradio-info input[type="checkbox"]:checked ~ label:before {
            color: #fff;
            background-color: #5bc0de;
        }*/



        /**, *::before, *::after {
  box-sizing: border-box;
}

html {
  min-height: 100%;
}

body {
  color: #435757;
  background: radial-gradient(#fff, #dac4cd);
  font: 1.4em/1 'Noto Sans', sans-serif;
}

.container {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);  
}

input {
  position: absolute;
  --left: -9999px;
}

label {
  display: block;
  position: relative;
  margin: 20px;
  padding: 15px 30px 15px 62px;
  border: 3px solid #fff;
  border-radius: 100px;
  color: #fff;
  background-color: #6a8494;
  box-shadow: 0 0 20px rgba(0, 0, 0, .2);
  white-space: nowrap;
  cursor: pointer;
  user-select: none;
  transition: background-color .2s, box-shadow .2s;
}

label::before {
  content: '';
  display: block;
  position: absolute;
  top: 10px;
  bottom: 10px;
  left: 10px;
  width: 32px;
  border: 3px solid #fff;
  border-radius: 100px;
  transition: background-color .2s;
}

label:first-of-type {
  transform: translateX(-40px);
}

label:last-of-type {
  transform: translateX(40px);
}

label:hover, input:focus + label {
  box-shadow: 0 0 20px rgba(0, 0, 0, .6);
}

input:checked + label {
  background-color: #ab576c;
}

input:checked + label::before {
  background-color: #fff;
}*/
    </style>
     
  
  
  
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
		$(document).on('change', 'input[type="checkbox"]', function (e) {
		var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                var st_code = $('#<%=ddlstate.ClientID%>').val();
                var date = $('.checkbox input:checked').val();
				var dat1 = date.split("/");
                var dat = dat1[0];
                var Fyear = $("#<%=ddlFYear.ClientID%>").val();
                var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
				$.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Missed_Date_Approve.aspx/LeavedDate",
                    data: "{'sf_Code':'" + sf_code + "','Dates':'" + date + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == "Leave") {
                            //alert("User Leave on '" + date + "'");
                            $('#cbl').attr('checked', false);
                                if (confirm("Leave on '" + date + "'.Do You want to release?")) {
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        async: false,
                                        url: "Missed_Date_Approve.aspx/leavedel_rel",
                                        data: "{'sf_Code':'" + sf_code + "','Dates':'" + date + "'}",
                                        dataType: "json",
                                        success: function (data) {
                                            alert(data.d);
                                            getdata();
                                        },
                                        error: function (rs) {
                                            console.log(rs);
                                        }
                                    });
                                }
                                else {
                                    alert("Canceled...");
                                    $('#cbl').attr('checked', false);
                                }
                        }
                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });
				$.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Missed_Date_Approve.aspx/FieldWDate",
                    data: "{'sf_Code':'" + sf_code + "','Dates':'" + dat + "','stcode':'" + st_code + "','year':'" + Fyear + "','month':'" + FMonth+"'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == "FW") {
                            if (confirm("Already on Field Work. Do You want to release?")) {
                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: false,
                                    url: "Missed_Date_Approve.aspx/ReleasMissedDate",
                                    data: "{'sf_Code':'" + sf_code + "','Dates':'" + date + "'}",
                                    dataType: "json",
                                    success: function (data) {
                                        alert(data.d);
                                        getdata();
                                    },
                                    error: function (rs) {
                                        console.log(rs);
                                    }
                                });
                            }
                            else {
                                alert("cancelled...");
                            }
                        }
                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });
		});
            $(document).on('click', '.btnview', function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
                getdata();
            });
            $('.btnMiss_Date').click(function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
               
                stk = '';
                var items = $(".checkbox input:checked");
                items.each(function (idx, item) {
                    stk += $(item).attr('name') + ',';
                });
				var items1 = $(".checkbox1 input:checked");
                items1.each(function (idx, item) {
                    stk += $(item).attr('name') + ',';
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Missed_Date_Approve.aspx/ReleasMissedDate",
                    data: "{'sf_Code':'" + sf_code + "','Dates':'" + stk + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        getdata();
                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });
            });
			$("#<%=ddlFieldForce.ClientID%>").change(function () {
                while (document.getElementById('clr').firstChild) {
                    document.getElementById('clr').removeChild(document.getElementById('clr').firstChild);
                }
            });
            $("#<%=ddlFMonth.ClientID%>").change(function () {
                while (document.getElementById('clr').firstChild) {
                    document.getElementById('clr').removeChild(document.getElementById('clr').firstChild);
                }
            });
            function getdata() {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                var Fyear = $("#<%=ddlFYear.ClientID%>").val();
                var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
                var SubDivCode = $("#<%=subdiv.ClientID%>").val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Missed_Date_Approve.aspx/GetMissedData",
                    data: "{'sf_Code':'" + sf_code + "','FYear':'" + Fyear + "','FMonth':'" + FMonth + "','SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        var dtls = data.d;
                        var chl = $('.checkbox');
                        $(chl).find('li').remove();
                        if (dtls.length > 0) {
                            for (var i = 0; i < dtls.length; i++) {
                                if (dtls[i].sun_val == 'SR' || dtls[i].sun_val == 'R') {
								var parts = dtls[i].MDate.split('-');
                                    var mdate=parts[2] + "/" + parts[1] + "/" + parts[0];
								
                                    str = '<li><input type="checkbox" class="custom-control-input" id="cbl" value="' + dtls[i].MDate + '" name="' + dtls[i].MDate + '" disabled /><label class="custom-control-label" for="cbl' + dtls[i].MDate + '" style="' + (dtls[i].sun_val == 'SR' ? 'color: red; ' : 'color: none;') + '">' + mdate + '</label></li>';
                                }
                                else {
								var parts = dtls[i].MDate.split('-');
                                    var mdate=parts[2] + "/" + parts[1] + "/" + parts[0];
                                    str = '<li><input type="checkbox" class="custom-control-input" id="cbl" value="' + dtls[i].MDate + '" name="' + dtls[i].MDate + '"  /><label class="custom-control-label" for="cbl' + dtls[i].MDate + '" style="' + (dtls[i].sun_val == 'S' ? 'color: red; ' : 'color: none;') + '">' + mdate + '</label></li>';
                                }
                                $(chl).append(str);
                            }
                        }
                        else {
                            str = '<li><label>No Records Found..!</label></li>';
                            $(chl).append(str);

                        }

                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });
            }
        });
    </script>
    <form id="form1" runat="server">
        <div class="container" style="width: 100%">
            <div class="row">
                <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                    Division</label>
                <div class="col-sm-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:dropdownlist id="subdiv" runat="server" skinid="ddlRequired" cssclass="form-control"
                            style="min-width: 100px;" onselectedindexchanged="subdiv_SelectedIndexChanged"
                            autopostback="true">
                        </asp:dropdownlist>
                    </div>
                </div>
            </div>
			  <div class="row">
                            <label id="st" class="col-md-2  col-md-offset-3  control-label">
                                State</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectIndexchanged" AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
            <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Field Force</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:dropdownlist id="ddlFieldForce" runat="server"
                            cssclass="form-control" width="350">
                        </asp:dropdownlist>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtMonth" class="col-md-2  col-md-offset-3  control-label">
                    Month</label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:dropdownlist id="ddlFMonth" runat="server" skinid="ddlRequired" cssclass="form-control"
                            style="min-width: 100px">
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:dropdownlist>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtYear" class="col-md-2 col-md-offset-3  control-label">
                    Year</label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:dropdownlist id="ddlFYear" runat="server" skinid="ddlRequired" cssclass="form-control"
                            style="min-width: 100px">
                        </asp:dropdownlist>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6  col-md-offset-5">
                    <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">View</a>
                </div>
            </div>
        </div>


        <div class="container" style="width: 60%">
            <fieldset class="group">
                <legend>Select Missed Release Dates</legend>
                <ul class="checkbox" id="clr">
                </ul>
            </fieldset>
        </div>
        <div class="row">
            <div class="col-md-6  col-md-offset-5">
                <a name="btnMiss_Date" type="button" class="btn btn-primary btnMiss_Date" style="width: 23%">Missed Release</a>
            </div>
        </div>

    </form>
</asp:Content>

