<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Retailing_summary.aspx.cs" Inherits="MIS_Reports_Retailing_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script>
	
	$(document).ready(function () {
            $('.datePicker').datepicker({ dateFormat: 'dd/mm/yy' });
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;
            $('#fromDate').val(today);
            $('#toDate').val(today);
        });
	
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
	
        function NewWindow() {
            var st = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (st == "---Select Manager---") { alert("Select Manager."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

            var st = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text().trim();
            var str;

            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
            if (subdiv == "--- Select ---") { alert("Select Subdivision."); $('#subdiv').focus(); return false; }

            if (sf_name == 'admin' || sf_name == 'admin - Admin -') {
                var str = $('#<%=ddlMR.ClientID%> :selected').val();
                MR_name = $('#<%=ddlMR.ClientID%> :selected').text();
                if (str == '0') {
                    str = st;
                    MR_name = sf_name;
                }
            }
            else {
                var str = $('#<%=ddlMR.ClientID%> :selected').val();
                var MR_name = $('#<%=ddlMR.ClientID%> :selected').text();
                if (str == '0') {
                    str = st;
                    MR_name = sf_name;
                }
            }

             <%--var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();--%>

           <%-- var Fyear = $("#<%=fdatee.ClientID%>").val();//$('#fdatee').val();
            if (Fyear == "") { alert("Select From Date."); $('#fdatee').focus(); return false; }
            var FMonth = $("#<%=tdatee.ClientID%>").val();//$('#tdatee').val();
            if (FMonth == "") { alert("Select To Date."); $('#tdatee').focus(); return false; }--%>

            var Fyear = $('#fromDate').val();

            if (Fyear.length <= 0) {
                alert('Enter From Date..!');
                $('#fromDate').focus();
                return false;
            }

            var FMonth = $('#toDate').val();

            if (FMonth.length <= 0) {
                alert('Enter To Date..!');
                $('#toDate').focus();
                return false;
            }


            window.open("rpt_Retailing_Summary.aspx?SF_Code=" + str + "&month=" + Fyear + "&year=" + FMonth + "&subdiv=" + subdiv + "&SF_Name=" + MR_name, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');


        }

    </script>
    <form id="form1" runat="server">
	
	<h3>Retail Summary Report</h3>

    <div class="row">
        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Style="text-align: right;
            padding: 8px 4px;" Text="Select Division" CssClass="col-md-3 control-label"></asp:Label>
        <div class="col-sm-4 inputGroupContainer">
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList ID="subdiv" runat="server"  CssClass="form-control"
                    Width="120" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>
    </div>

        <div class="row">
        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Manager" Style="text-align: right;
            padding: 8px 4px;" CssClass="col-md-3 control-label"></asp:Label>
        <div class="col-sm-4 inputGroupContainer">
            <div class="input-group" id="kk" runat="server">
                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="form-control"
                    Style="font-size: 14px;" Width="150" Visible="false">
                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired" CssClass="form-control"
                    Style="font-size: 14px;" Width="70">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                    SkinID="ddlRequired" CssClass="form-control" Style="font-size: 14px;" Width="120">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false"
                    CssClass="form-control" Style="font-size: 14px;" Width="400">
                </asp:DropDownList>
            </div>
        </div>
    </div>

        <div class="row">
        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"
            Style="text-align: right; padding: 8px 4px;font-weight:normal;" CssClass="col-md-3 control-label"></asp:Label>
        <div class="col-sm-4 inputGroupContainer">
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false"
                    CssClass="form-control" Style="font-size: 14px;" Width="120">
                </asp:DropDownList>
            </div>
        </div>
    </div>
	
    <div class="row">
                <label for="fromDate" class="col-md-3 control-label" Style="text-align: right; padding: 8px 4px;font-weight:normal;">From Date</label>
                <div class="col-sm-4 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input type="text" name="fromDate" id="fromDate" class="form-control datePicker" />
                    </div>
                </div>
            </div>

              <div class="row">
                                             <label for="fromDate" class="col-md-3 control-label" Style="text-align: right; padding: 8px 4px;font-weight:normal;">To Date</label>
                                        <div class="col-sm-4 inputGroupContainer">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                <input type="text" name="fromDate" id="toDate" class="form-control datePicker"/>
                                            </div>
                                        </div>                            
                            </div>
	
	<%--<div class="row">
            <label id="Label3" class="col-md-3 control-label" Style="text-align: right; padding: 8px 4px;font-weight:normal;">
                              From</label>
                 <div class="col-sm-4 inputGroupContainer">
                     <div class="input-group">
                         <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                         <input type="text" name="txtFromDate" runat="server" id="fdatee" class="form-control datetimepicker" Style="font-size: 14px;Width:120px;"
                        autocomplete="off" />

                      </div>
                  </div>

       </div>
       
                       <div class="row">
                           <label id="Label14" class="col-md-3 control-label" Style="text-align: right; padding: 8px 4px;font-weight:normal;">
                              To</label>
                                <div class="col-sm-4 inputGroupContainer">
                                  <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                      <input type="text" name="txtToDate" runat="server" id="tdatee" class="form-control datetimepicker"
                        autocomplete="off" Style="font-size: 14px;Width:120px;" />
                              </div>
                       </div>
                      </div>--%>


         <%--<div class="row">
             <asp:Label ID="Mnth" runat="server" Text="Month" SkinID="lblMand"
            Style="text-align: right; padding: 8px 4px;" CssClass="col-md-3 control-label"></asp:Label>
            <div class="col-sm-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                         Style="min-width: 100px" Width="100">
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
                </div>
            </div>
        </div>

         <div class="row">
             <asp:Label ID="Year" runat="server" Text="Year" SkinID="lblMand"
            Style="text-align: right; padding: 8px 4px;" CssClass="col-md-3 control-label"></asp:Label>
            <div class="col-sm-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                        Style=" min-width: 100px" Width="100">
                    </asp:DropDownList>
                </div>
            </div>
        </div>--%>

        <div class="row">
            <div class="col-sm-10" style="text-align: center">         
				<a id="btnView" class="btn btn-primary" onclick="NewWindow()"
                        style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
				<%--<button id="btnView" class="btn btn-primary" runat="server" onclick="NewWindow().this" style="vertical-align: middle">
                 <span>View</span></button>--%>
   </div>
</div>
        </form>
</asp:Content>

