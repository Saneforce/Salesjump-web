<%@ Page Title="TP Delete" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="TPDelete.aspx.cs" Inherits="MasterFiles_Options_TPDelete" %>
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TP Delete</title>
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
              $('#<%=btnGo.ClientID%>').click(function () {

                  var SMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                  if (SMonth == "---Select---") { alert("Select Month."); $('#<%=ddlMonth.ClientID%>').focus(); return false; }
                
              });
          }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <br /> 
        <center>
        <table align="center">
           <tr style="height:25px;" >
                <td align="left" class="stylespc" width="120px">
                    <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>                  
                 </td>
                 <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height:25px;" >
                <td align="left" class="stylespc"> 
                    <asp:Label ID="lblMonth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>                    
                  </td>
                  <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
         
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                   </td>
                   <td class="stylespc">
                    <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>

                </td>
            </tr>
          </table>
          <br />
          <center>
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" onclick="btnGo_Click"  />
                    </center>
        <br />
       <table width="85%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdTP" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"   onrowdatabound="grdTP_RowDataBound"
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="Select" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSNo" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Field Force" ItemStyle-Width="300" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblReporting" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TP Entered" Visible="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkTP" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TP Entered for future month(s)" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblFutureTP" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="TP Delete" CssClass="BUTTON" 
                    OnClientClick="return confirm('Do you want to delete the Tour Plan');" 
                    onclick="btnSubmit_Click" Visible="false"/>
            </td>
        </tr>
    </table>
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
</asp:Content>