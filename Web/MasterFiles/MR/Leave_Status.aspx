<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Leave_Status.aspx.cs" Inherits="MasterFiles_MR_Leave_Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl1" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Leave Status</title>
       <link type="text/css" rel="stylesheet" href="../../css/style.css" />
        <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnView').click(function () {            
                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "--Select--") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "Select") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "--Select--") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "Select") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }                

            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div id="Divid" runat="server"></div>
        <br />
        <center>
            <table >
              <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                 
                </td>
             <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="true"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>

                </td>
            </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="From Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                        <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="From Year"></asp:Label>
                        <asp:DropDownList ID="ddlFYear" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            Width="60">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblTMonth" runat="server" SkinID="lblMand" Text="To Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                        <asp:Label ID="lblTYear" runat="server" SkinID="lblMand" Text="To Year" Width="60"></asp:Label>
                        <asp:DropDownList ID="ddlTYear" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            Width="60">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
        <asp:Button ID="btnView" runat="server" Text="View" CssClass="BUTTON"  Width="70px" Height="25px" 
                onclick="btnView_Click" />
        </center>
        <br />

           <center>
       <table width="80%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdLeave" runat="server" Width="100%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Data found for Approval's"   
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Code" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Designaion" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                     
                              <asp:TemplateField HeaderText="Emp.Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblemp" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                  <asp:TemplateField HeaderText="From Date" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server"  Text='<%#Eval("From_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbltodate" runat="server"  Text='<%#Eval("To_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Leave_Active_Flag")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved BY" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Reporting_To_SF")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Reason")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
    </center>
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>

    </form>
</body>
</html>
</asp:Content>