<%@ Page Language="C#"  MasterPageFile="~/Master.master" AutoEventWireup="true"  CodeFile="rpt_PrimaryOrderSale.aspx.cs" Inherits="MIS_Reports_rpt_PrimaryOrderSale" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    
<head >
    <title>Primary Order Vs Sale</title>
     <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
    <script src="../JsFiles/serial.js" type="text/javascript"></script>
    <script src="../JsFiles/light.js" type="text/javascript"></script>
      <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript">
        function btnview() {
            var str = $('#<%=ddlmgr.ClientID%> :selected').text();
            if (str == "---Select Field Force---") {
                alert("Select Field Force."); $('#<%=ddlmgr.ClientID%>').focus(); return false;
            }
            var str = $('#<%=ddlmgr.ClientID%> :selected').val();
            var mgr_name = $('#<%=ddlmgr.ClientID%> :selected').text();
    

          var  st = $('#<%=ddlfieldforce.ClientID%> :selected').text();        

          var  st = $('#<%=ddlfieldforce.ClientID%> :selected').val();
          var  mr_name = $('#<%=ddlfieldforce.ClientID%> :selected').text();

         var   year = $('#<%=ddlyear.ClientID%> :selected').text();
        var    month = $('#<%=ddlmonth.ClientID%> :selected').val();

            var ddldiv = $('#<%=ddldiv.ClientID%> :selected').val();

            window.open("rpt_PrimaryOrderVsSale.aspx?SF_Code=" + st + "&year=" + year + "&month=" + month + "&Mgr_name=" + mgr_name + "&Mgr_code=" + str +  "&div=" + ddldiv, null,
                'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="width: 100%">
            <div class="row">
                <label id="Label1" class="col-md-2 col-md-offset-3 control-label">
            Division:</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
            <asp:DropDownList id="ddldiv" runat="server" OnSelectedIndexChanged="ddldiv_SelectIndexchanged" AutoPostBack="true"  CssClass="form-control" Width="120"></asp:DropDownList>
            </div>
                </div>
            </div>
                 <div class="row">
                <label id="lblFF" class="col-md-2 col-md-offset-3 control-label">
                Manager:</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group" id="kk" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList ID="ddlmgr" runat="server" OnSelectedIndexChanged="ddlmgr_SelectIndexchanged" AutoPostBack="true"   CssClass="form-control" Width="350"></asp:DropDownList> </div>
                </div>
            </div>
             <div class="row">
                <label id="lblMR" runat="server" class="col-md-2 col-md-offset-3 control-label">
                Field Force:</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList ID="ddlfieldforce" runat="server"  CssClass="form-control"
                            Width="350"></asp:DropDownList> </div>
                </div>
            </div>
            <div class="row">
                <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                <label id="lblFMonth" class="col-md-2 col-md-offset-3  control-label">
                Month:</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                <asp:DropDownList ID="ddlmonth" runat="server">
                <asp:ListItem Value="0" Text="---select--"></asp:ListItem>
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
                 <asp:DropDownList ID="ddlyear" runat="server"></asp:DropDownList>
           </div>
                </div>
            </div>

                <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnview" class="btn btn-primary"   onclick="btnview()"  style="vertical-align: middle; width: 100px" ><span>View</span></a>
                 </div>
            </div>
        </div>
    </form>
</body>
</html>
    </asp:Content>
