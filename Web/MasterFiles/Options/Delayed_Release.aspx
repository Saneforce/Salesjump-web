<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delayed_Release.aspx.cs" Inherits="MasterFiles_Options_Delayed_Release" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delayed Release</title>
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
              
          }); 
    </script>

    <script type="text/javascript" language="javascript" >
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= grdRelease.ClientID %>');
            var validator = document.getElementById('RequiredFieldValidator1');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            return true;
                        }
                    }
                }
            }
            alert("Please Select at least one record.");

            return false;
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
          
              <tr style="height:25px;">
                <td align="left" class="stylespc">
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
            <tr style="height:25px">
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>     
                    </td>
                    <td align="left" class="stylespc">            
                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                   
                    </asp:DropDownList>
                </td>
            </tr>
          </table>
          </center>
          <br />
          <center>
                    <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" Width="30px" Height="25px" Text="Go" onclick="btnGo_Click"  />
                </center>
        <br />
           <table width="100%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdRelease" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Records Found"   
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdSalesForce_RowDataBound">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>   
   <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    
                         <asp:TemplateField HeaderText="Release" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White">
                            <ItemTemplate>                            
                                <asp:CheckBox ID="chkRelease" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>         
                         
                           <asp:TemplateField HeaderText="Sf Code" Visible="false">
                                <ItemTemplate>
                                   
                                    <asp:Label ID="lblsf_code" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="180px" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                   
                                    <asp:Label ID="lblSfName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" HeaderStyle-Width="180px" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Eval("Sf_HQ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="180px" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesi" runat="server" Text='<%# Eval("sf_Designation_Short_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Delayed Date" HeaderStyle-Width="180px" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Delayed_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode" HeaderStyle-Width="180px" Visible="false" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblMode" runat="server" Text='<%# Eval("Mode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                           <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"  />
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
    <br />
    <center>
    <table>
        <tr>
            <td>
               <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Release" OnClientClick="return validateCheckBoxes()" CssClass="BUTTON" Visible="false"
                     
                    onclick="btnSubmit_Click"/>
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
