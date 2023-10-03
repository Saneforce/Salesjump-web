<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Details.aspx.cs" Inherits="MasterFiles_DashBoard_Doctor_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer Channel/Category/Class</title>
       <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({         
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'Doctor_Details.aspx/GetData',
                data: '{}',
                success:
                    function (response) {
                        drawVisualization(response.d);
                        
                    }

            });
        })

        function drawVisualization(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].ColumnName, dataValues[i].Value]);
            }

            new google.visualization.PieChart(document.getElementById('visualization')).
                draw(data, { title: "" });
        } 
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table >
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="lblSpace">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                            SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="lblSpace">
                        <asp:Label ID="lblType" runat="server" Text="Type" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="rdoType" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="-1" Text="--Select--" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Category"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Channel"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Class"></asp:ListItem>
                            <%--<asp:ListItem Value="3" Text="Qualification"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </center>
        <center>
            <asp:Button ID="btnGo" runat="server" CssClass="btnnew" Text="Go" Width="35px" Height="25px" OnClick="btnGo_Click" />
        </center>
        <br />
        <center>
           <asp:Panel ID="pnlchart" runat="server" BorderStyle="Solid" Width="80%" BackColor="White" BorderWidth="1" Visible="true">
        

            <table width="60%">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCatg" runat="server" Font-Bold="True" Font-Size="Small" Font-Underline="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td>
                <br />
                </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="100%"
                            align="center">
                        </asp:Table>
                    </td>
                </tr>
                <tr>
                <td>
                
         <div id="visualization" style="width: 750px; height: 550px;">
       
    </div>
                </td>
                </tr>
            </table>
              </asp:Panel>
        </center>
     <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
    </div>
    </form>
</body>
</html>
