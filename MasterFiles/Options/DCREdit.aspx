<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCREdit.aspx.cs" Inherits="MasterFiles_Options_DCREdit" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Edit</title>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />
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

                  var SMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                  if (SMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }

              });
          }); 
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
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
                <td align="left" class="stylespc" width="120px">
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>     
                    </td>
                    <td align="left" class="stylespc">  
                    
                      <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
            ToolTip="Enter Text Here"></asp:TextBox>          
                    <asp:DropDownList ID="ddlFieldForce" runat="server" Width="300px" SkinID="ddlRequired">
                    <asp:ListItem Selected="True" Value="-1" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
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
                    <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>                    
                 </td>
                 <td align="left" class="stylespc"> 
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
          
          </table>
          <br />
          <center>
                    <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" Width="30px" Height="25px" Text="Go" onclick="btnGo_Click"  />
                </center>
        <br />
       <table width="85%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdTP" runat="server" Width="50%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Records Found"   
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="Trans_SlNo" ItemStyle-Width="100" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrans_SlNo" runat="server" Text='<%#Eval("Trans_SlNo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DCR Date" ItemStyle-Width="300" >
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDate" runat="server" />
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Activity_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="300">
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkType" runat="server" Text='<%# Eval("worktype_name_b") %>'></asp:Label>
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
    <table>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Update" CssClass="BUTTON" Visible="false"
                    OnClientClick="return confirm('Do you want to allow DCR Edit for the selected date(s)');" 
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
