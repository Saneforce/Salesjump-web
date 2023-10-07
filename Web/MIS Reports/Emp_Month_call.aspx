<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Emp_Month_call.aspx.cs" Inherits="MIS_Reports_Emp_Month_call" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
        thead th
        {
            border: 1px solid #ececec;
        }
        
        
        tbody td
        {
            text-align: right;
        }
        
        
        tbody td:first-child
        {
            text-align: left;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on('click', '.btnview', function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
                var Fyear = $("#<%=ddlFYear.ClientID%>").val();
                var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                var str = 'rptEmp_Month_call.aspx?&SFCode=' + sf_code + '&FYear=' + Fyear + '&FMonth=' + FMonth + '&SFName=' + SFName + '&SubDiv=' + SubDivCode;
                window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
            });

        });
    </script>
    <form id="form1" runat="server">
    <div class="container" style="width: 100%">
        <div class="row">
            <label id="Label2" class="col-md-2 col-md-offset-3  control-label">
                Division</label>
            <div class="col-sm-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Style="min-width: 150px"
                        Width="150" OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
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
                    <asp:DropDownList ID="ddlFieldForce" runat="server"  
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label for="txtMonth" class="col-md-2 col-md-offset-3  control-label">
                Month</label>
            <div class="col-md-2 inputGroupContainer">
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
            <label for="txtYear" class="col-md-2  col-md-offset-3  control-label">
                Year</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                        Style=" min-width: 100px" Width="100">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-md-offset-5">
                <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">
                    View</a>
            </div>
        </div>
        <br />
    </div>
    </form>
</asp:Content>
