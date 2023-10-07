<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Notification_inpu.aspx.cs" Inherits="MIS_Reports_Notification_inpu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="description" content="">
        <script type="text/javascript"
            src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>

        <script type="text/javascript">
            $(function () {
                $('#headg').hide();
            });
</script>
        <script type="text/javascript">
            var userlist = []; var noti = '';
            $(document).ready(function () {



                var hdn = $('#<%=hdnslno.ClientID%>').val();
                if (($('#<%=hdnslno.ClientID%>').val()).length > 0) {
                    filldata(hdn);
                }
                else {
                }
            });
            function validate() {
                var comment = $('#<%=comment.ClientID%>').val();
                if (comment == "") {
                    alert("Enter Notification Message.");
                    $('#<%=comment.ClientID%>').focus(); return false;
                }
                var type = $("[id*=commenttype]").val();
                if (type == 0) {
                    alert("Enter Comment type."); return false;
                }
                var date = $('#date1').val();
                if (date == '') {
                    alert("Enter Effective from Date.");
                    return false;
                }
                
                var sf_type =<%=Session["sf_type"]%>;
                var div_code =<%=Session["div_code"]%>;
                var hdn = $('#<%=hdnslno.ClientID%>').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Notification_inpu.aspx/userlistitm",
                    dataType: "json",
                    data: "{'sf_type':'" + sf_type + "','div_code':'" + div_code + "'}",
                    success: function (data) {
                        userlist = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        console.log(result);
                    }
                });
                 const { host } = window.location
                var name = host;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Notification_inpu.aspx/sentnoti",
                    dataType: "json",
                    data: "{'div_code':'" + div_code + "','comment':'" + comment + "','type':'" + type + "','data':'" + JSON.stringify(userlist) + "','name':'"+name+"'}",
                    success: function (data) {
                        note = data.d;  
                       
                    },
                    error: function (result) {
                        console.log(result);
                    }
                });
                 $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Notification_inpu.aspx/savenotifi",
                    dataType: "json",
                    data: "{'div_code':'" + div_code + "','comment':'" + comment + "','type':'" + type + "','date':'" + date + "','sl_no':'" + hdn + "'}",
                    success: function (data) {
                        noti = data.d;//JSON.parse(data.d) || [];
                         if (noti == 'Saved') {
                            alert('Comment Saved Successfully!');
                        }
                        else if (noti == 'Updated') {
                            alert('Comment Updated Successfully!');
                        }
                    },
                    error: function (result) {
                        console.log(result);
                    }
                });
               

               
                window.location.href ='../MasterFiles/Noticeboard_List.aspx';
            }
          

            function filldata(hdn) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Notification_inpu.aspx/fillData",
                    dataType: "json",
                    data: "{'sl_no':'" + hdn + "'}",
                    success: function (data) {
                        Notify = JSON.parse(data.d) || [];
                        fillfield();
                    },
                    error: function (result) {
                        console.log(result);
                    }
                });
            }
            function fillfield() {
                $('#<%=comment.ClientID%>').text(Notify[0].Comment);
                $('#<%=commenttype.ClientID%>').children("option:selected").text(Notify[0].Comment_Type);
                $("#date1").val(Notify[0].Created_Date);
                $('#<%=hdnslno.ClientID%>').val(Notify[0].Sl_No);
            }
      </script>
        <style class="cp-pen-styles">
            @keyframes rotate {
                0% {
                    -ms-transform: rotate(0deg);
                    -webkit-transform: rotate(0deg);
                    transform: rotate(0deg);
                }

                100% {
                    -ms-transform: rotate(359deg);
                    -webkit-transform: rotate(359deg);
                    transform: rotate(359deg);
                }
            }

            @keyframes smiley {
                0% {
                    transform: rotateY(180deg);
                }

                80% {
                    transform: rotateY(180deg);
                }

                100% {
                    transform: rotateY(0deg);
                }
            }

            .loading {
                top: 30%;
                left: 50%;
            }

            .loading {
                background-color: #FFFFFF;
                height: 76px;
                width: 75px;
                margin-left: -12px;
                margin-top: -94px;
                border-top: 20px solid #cf2a2e;
                border-right: 20px solid #e8d77d;
                border-left: 20px solid #1a3f8e;
                border-bottom: 20px solid #11b02d;
                border-radius: 50%;
                animation-name: rotate;
                animation-duration: 0.3s;
                animation-timing-function: linear;
                animation-iteration-count: 10;
            }

            #load {
                width: 150px;
                height: 150px;
                backface-visibility: hidden;
            }

            .flip {
                transition: 0.6s;
                transform-style: preserve-3d;
                position: relative;
                animation-name: smiley;
                animation-duration: 4s;
                animation-iteration-count: 1;
                animation-timing-function: linear;
            }

            @keyframes showbutton {
                0% {
                    margin: -20px -125px 0;
                }

                100% {
                    margin: 20px -125px 0;
                }
            }

            @keyframes hidebutton {
                0% {
                    margin: 20px -125px 0;
                }

                100% {
                    margin: -20px -125px 0;
                }
            }

            #welcome {
                position: fixed;
                top: 60%;
                left: 50%;
                outline: none;
            }




            #search {
                display: none;
            }

            #welcome {
                width: 250px;
                height: 40px;
                margin: -70px -125px;
                font: 1.2em Helvetica;
                text-align: center;
            }
        </style>

        <!-- Bootstrap core CSS -->
        <link href="../assets/css/bootstrap.css" rel="stylesheet">
        <!--external css-->
        <link href="../assets/font-awesome/css/font-awesome.css" rel="stylesheet" />

        <!-- Custom styles for this template -->
        <link href="../assets/css/style.css" rel="stylesheet">
        <link href="../assets/css/style-responsive.css" rel="stylesheet">

        <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    </head>

    <body>

        <div class="container" style="padding-bottom: 990px;">
            <form class="form-login" runat="server" style="height: 688px; padding-bottom: 990px;">
                <h2 class="form-login-heading">Notification</h2>
                <div class="login-wrap" style="padding-bottom: 990px;">
                    <div class="loading" id="smile">
                        <div class="flip">

                            <svg id="load">
              <circle cx="19" cy="19" r="19" stroke-width="1" stroke="#86a9f3" fill="#d1f9ff"></circle>
            <circle cx="10" cy="15" r="4" stroke="none" fill="#86a9f3"></circle>    <circle cx="28" cy="15" r="4" stroke="none" fill="#86a9f3"></circle>
            <path fill="none" stroke="#86a9f3" stroke-width="5" d="M6,22 A20,37 0 0,0 32,22"></path>
        </svg>
                        </div>
                    </div>
                    </br>
                </br>
                </br>
                     
                    <asp:HiddenField ID="hdnslno" runat="server" />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Comments"></asp:Label></td>
                            <td>&nbsp&nbsp&nbsp&nbsp</td>
                            <td>
                                <asp:TextBox ID="comment" runat="server" TextMode="MultiLine" class="form-control" placeholder="Comments" Style="width: 300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp</td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Type"></asp:Label></td>
                            <td>&nbsp&nbsp&nbsp&nbsp</td>
                            <td>
                                <asp:DropDownList ID="commenttype" class="form-control" runat="server">
                                    <asp:ListItem Value="0">Select Type</asp:ListItem>
                                    <asp:ListItem>Wishes</asp:ListItem>
                                    <asp:ListItem>News</asp:ListItem>
                                    <asp:ListItem>Important</asp:ListItem>
                                    <asp:ListItem>Messages</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp&nbsp</td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp</td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label3" runat="server" Text="Effective From"></asp:Label></td>
                            <td>&nbsp&nbsp&nbsp&nbsp</td>
                            <td>
                                <input id="date1" name="tdate" type="date" class="form-control" /></td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp&nbsp</td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp&nbsp</td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp&nbsp</td>
                        </tr>
                        <tr align="center">
                            <td></td>
                            <td>
                                <asp:Button ID="Button1" class="btn btn-theme" runat="server" Text="Submit" Width="120px" OnClientClick="validate()" /><%--onclick="Button1_Click"--%>
                         </td>
                            <td>
                                <asp:Button ID="btnclose" runat="server" class="btn btn-theme" PostBackUrl="../MasterFiles/Noticeboard_List.aspx" Text="close" Width="120px" />
                            </td>
                        </tr>
                    </table>

                    <br>
                </div>
        </div>
        </form>	  	
	  
      </div>
	
        <script src="../assets/js/jquery.js"></script>
        <script src="../assets/js/bootstrap.min.js"></script>


    </body>
</asp:Content>

