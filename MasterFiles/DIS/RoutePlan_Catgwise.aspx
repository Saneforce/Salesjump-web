<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoutePlan_Catgwise.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_RoutePlanView" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Route Plan</title> 
 <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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
          $('form').live("go", function () {
            ShowProgress();
        });
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             $('input:text:first').focus();
             $('input:text').bind("keydown", function (e) {
                 var n = $("input:text").length;
                 if (e.which == 13) { //Enter key
                     e.preventDefault(); //to skip default behavior of the enter key
                     var curIndex = $('input:text').index(this);

                     if ($('input:text')[curIndex].value == '') {
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

                 var cat = $('#<%=ddlTerritory.ClientID%> :selected').text();
                 if (cat == "---Select---") { alert("Please Select Route Plan."); $('#ddlTerritory').focus(); return false; }

             });
         });

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <center>
        <br />
        <table align="center">          

            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblAllocate" runat="server" Text="Route Plan " BackColor="#FFFF66" SkinID="lblMand"></asp:Label>
                </td>
            </tr>

            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="Label1" runat="server" Text="&nbsp;"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblSDP" runat="server" Text="Route Plan "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTerritory" runat="server" SkinID="ddlRequired"></asp:DropDownList>                    
                </td>
                <td>
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" onclick="btnGo_Click" />
                </td>
            </tr>
           
           </table>    
        <br />
      
        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width = "25%">
        </asp:Table>
        <br />
         <table width ="90%">
          <tr>
                <td align ="left">
                    <asp:Label ID="lblNote1" runat="server" Text="Note : Highlighted in green represents Missed Customers" BackColor ="LightGreen" Visible ="false"  ></asp:Label>
                </td>
               
               
                <td  align = "right">
                    <asp:Label ID="lblNote2" runat="server" Text="Note : Highlighted in PapayaWhip represents Customers mapped in other plans" BackColor ="PapayaWhip"  Visible ="false"></asp:Label>
                </td>
            </tr>
         
        </table>
        <br />

        <table>
            <tr>
                <td align="center" width="30%">
                    <table width="80%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCatg1" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdDoctor1" runat="server" Height="100%" HorizontalAlign="Center" 
                                    AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                    onrowdatabound = "grdDoctor1_RowDataBound">
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood"/>
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>                
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="400" HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="100" HeaderText="Customer Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblSpecialName" runat="server" Text='<%#Eval("Doc_Type")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center" width="30%">
                    <table width="80%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCatg2" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdDoctor2" runat="server" Height="100%" HorizontalAlign="Center" 
                                    AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                    onrowdatabound = "grdDoctor2_RowDataBound">
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood"/>
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>                      
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="400" HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="100" HeaderText="Customer Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblSpecialName" runat="server" Text='<%#Eval("Doc_Type")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>                            
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center" width="30%">
                    <table width="80%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCatg3" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdDoctor3" runat="server" Height="100%" HorizontalAlign="Center" 
                                    AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                    onrowdatabound = "grdDoctor3_RowDataBound">
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood"/>
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>                
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="400" HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="100" HeaderText="Customer Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblSpecialName" runat="server" Text='<%#Eval("Doc_Type")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                      </Columns>
                                </asp:GridView>                            
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td align="center" width="30%">
                    <table width="80%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCatg4" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdDoctor4" runat="server" Height="100%" HorizontalAlign="Center" 
                                    AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                    onrowdatabound = "grdDoctor4_RowDataBound">
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood"/>
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>                
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="400" HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="100" HeaderText="Customer Type"  ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblSpecialName" runat="server" Text='<%#Eval("Doc_Type")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                    </Columns>
                                </asp:GridView>                            
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center" width="30%">
                    <table width="80%">
                        <tr>
                            <td>
                                <asp:Label ID="lblCatg5" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdDoctor5" runat="server" Height="100%" HorizontalAlign="Center" 
                                    AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                    onrowdatabound = "grdDoctor5_RowDataBound">
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood"/>
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>                
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="300" HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="100" HeaderText="Customer Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblSpecialName" runat="server" Text='<%#Eval("Doc_Type")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       
                                    </Columns>
                                </asp:GridView>                            
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center" width="30%">
                    <table width="80%">
                        <tr>
                            <td>
                                <asp:Label ID="lblCatg6" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdDoctor6" runat="server" Height="100%" HorizontalAlign="Center" 
                                    AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                    onrowdatabound = "grdDoctor6_RowDataBound">
                                    <HeaderStyle Font-Bold="False" />
                                    <SelectedRowStyle BackColor="BurlyWood"/>
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>                
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="300" HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="100" HeaderText="Customer Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:Label ID="lblSpecialName" runat="server" Text='<%#Eval("Doc_Type")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>                            
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <br />
        <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px" Text="Update" Visible="false" onclick="btnSubmit_Click"/>
        </center>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
         
    </div>
    </form>
</body>
</html>
