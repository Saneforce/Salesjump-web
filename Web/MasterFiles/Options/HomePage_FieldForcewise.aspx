<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage_FieldForcewise.aspx.cs"
    Inherits="MasterFiles_Options_HomePage_FieldForcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page - FieldForcewise</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript">
    
    
    </script>
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
              $('#btnGo').click(function () {

                  var SName = $('#<%=ddlFilter.ClientID%> :selected').text();
                  if (SName == "---Select---") { alert("Select Filter by Manger."); $('#ddlFilter').focus(); return false; }

              });
          }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
    </div>
    <br />
    <asp:HiddenField ID="hidImage_Id" runat="server" />
    <center>
        <asp:Label ID="lblFilter" runat="server" SkinID="lblMand" Text="Filter By Manager"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="ddlFilter" SkinID="ddlRequired" runat="server">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" CssClass="BUTTON" />
    </center>
    <br />

    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblSelect" Text="Please Select the Filter By Manager" runat="server"
                    ForeColor="Red" Font-Size="Large"></asp:Label>
            </td>
        </tr>
    </table>
    <center>
        <table width="80%">
            <tr>
                <td>
                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" OnRowCommand="grdSalesForce_RowCommand" OnRowDeleting="grdSalesForce_RowDeleting"
                        AlternatingRowStyle-CssClass="alt" 
                       >
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Image_Id" HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left"/>
                            <asp:TemplateField HeaderText="Sf_Code" Visible="false" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Image_Id" HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left"/>
                            <asp:TemplateField HeaderText="FilePath" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:FileUpload ID="FilUpImage" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Uploaded File" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="bt_upload" runat="server" EnableViewState="False" Width="80px" Height="25px" Text="Upload" CssClass="BUTTON" CommandName="Upload" OnClick="bt_upload_OnClick" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="FileName" HeaderText="FileName"  ItemStyle-HorizontalAlign="left"/>
                      <asp:TemplateField HeaderText="Remove" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDel" Font-Size="11px" Font-Names="Verdana" runat="server" CommandArgument='<%# Eval("Image_Id") %>'
                                            CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Image');">Remove Image
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
      <%--  <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="BUTTON" Visible="false" />--%>
    </center>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
