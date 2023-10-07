<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Retailer_Distributor_Wise.aspx.cs" Inherits="MIS_Reports_Retailer_Distributor_Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, subdiv, tmon, tyr) {
            popUpObj = window.open("rpt_Retailer_Distributor_Wise.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&subdivision=" + subdiv + "&TMonth=" + tmon + "&TYear=" + tyr,
                "ModalPopUp",
                "toolbar=no," +
                "scrollbars=yes," +
                "location=no," +
                "statusbar=no," +
                "menubar=no," +
                "addressbar=no," +
                "resizable=yes," +
                "width=900," +
                "height=600," +
                "left = 0," +
                "top=0"
            );
            popUpObj.focus();
            //LoadModalDiv();
        }



    </script>
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
        function NewWindow() {

            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
            if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
            var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
            if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
            var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
            if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }
            var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
            if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }

            var DLLMode = $('#<%=DLLMode.ClientID%>').val();
            if (DLLMode == 0) { alert("Select Mode"); $('#DLLMode').focus(); return false; }


            var selstate = $('#<%=ddlState.ClientID%>').val();
            var selstatev = $('#<%=ddlState.ClientID%> :selected').text();

            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').val();
            var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
            var fdate = $('#txtfrdate').val();
            if (fdate == '') {
                alert('Select the From Date');
                return false;
            }
            var Tdate = $('#ttxtodate').val();
            if (Tdate == '') {
                alert('Select the To Date');
                return false;
            }


            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();

            if (DLLMode == 1) {
                window.open("rpt_Retailer_Distributor_Wise.aspx?&Fdates=" + fdate + "&Tdates=" + Tdate + "&FMonth=" + FMonth + "&FYear=" + FYear + "&subdivision=" + subdiv + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Mode=" + DLLMode + "&state=" + selstate + "&vstate=" + selstatev, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            }
            else {
                window.open("new_retailer_distributorwise.aspx?&Fdates=" + fdate + "&Tdates=" + Tdate + "&FMonth=" + FMonth + "&FYear=" + FYear + "&subdivision=" + subdiv + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Mode=" + DLLMode + "&state=" + selstate + "&vstate=" + selstatev, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            }
        }
    </script>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="container" style="width: 100%">
                    <div class="form-group">

                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Label4" class="col-md-1 col-md-offset-4  control-label">
                                Division</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="subdiv" runat="server" OnSelectedIndexChanged="subdiv_SelectedIndexChanged" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150"  AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Label1" class="col-md-1 col-md-offset-4  control-label">
                                Mode</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="DLLMode" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                        <asp:ListItem Value="0" Text="--select--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Field Force"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Distributor"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label id="Label5" class="col-md-1 col-md-offset-4  control-label">
                                State</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                       <%--     <label id="lblFMonth" class="col-md-1 col-md-offset-4 control-label" >
                                From</label>--%>
                            <label id="Label6" class="col-md-1 col-md-offset-4  control-label" style="width:113px;">
                                    From Date
                                </label>
                            <div class="col-sm-5 inputGroupContainer" style="padding-left:1px;">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <input id="txtfrdate" name="txtFrom" type="date" class="TEXTAREA form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                        tabindex="1" skinid="MandTxtBox" style="font-size: medium" />                               
                                    
                                    <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" Visible="false" CssClass="form-control"
                                        Style="min-width: 100px" Width="100">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                                    </asp:DropDownList>
                                    <%-- <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Style="text-align: center"
                                            Width="60"></asp:Label>--%>
                                    <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="100" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                           <%-- <label id="Label3" class="col-md-1 col-md-offset-4  control-label">
                                To</label>--%>
                            <label id="Label7" class="col-md-1 col-md-offset-4  control-label">
                                    To Date
                                </label>
                            <div class="col-sm-5 inputGroupContainer" style="padding-left:15px;">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <input id="ttxtodate" name="txtFrom" type="date" class="TEXTAREA form-control" 
                                        onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"  
                                        onkeypress="AlphaNumeric_NoSpecialChars(event);" tabindex="1"   
                                        skinid="MandTxtBox" style="font-size: medium" />   
                                    <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="100" Visible="false">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                                    </asp:DropDownList>
                                    <%-- <asp:Label ID="lblTYear" runat="server" SkinID="lblMand" Text=" Year" Style="text-align: center"
                                Width="60"></asp:Label>--%>
                                    <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="100" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        
                        <div class="row">
                            <div class="col-md-6 col-md-offset-5">
                                <button id="btnGo" class="btn btn-primary" runat="server" onclick="NewWindow().this"
                                    style="width: 100px">
                                    <span>View</span></button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</asp:Content>
