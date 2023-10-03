<%@ Page Language="C#" AutoEventWireup="true" CodeFile="State_Count.aspx.cs" Inherits="User_Count" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        $('form').live("submit", function () {
            ShowProgress();
        });
       
       
       
    </script>
   <link type="text/css" rel="stylesheet" href="../css/style.css" />
 <style type="text/css">
         .txt:focus {
                border-color: #56aff1;
                background-color: #fff4d8;
            }
   </style>
</head>
<body style="background-color:LightBlue">
    <form id="form1" runat="server">
    <div>
    <br />
    <center>
    <h3 style="text-decoration:underline">State Master View</h3>
    </center>
    <br />
 
     <center>
                
            </center>
            <br />
              <center>

    <table align="center" style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="Grid_View" runat="server" AutoGenerateColumns="false">
                         <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (Grid_View.PageIndex * Grid_View.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_State" runat="server" Text='<%#Eval("statename")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Area" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Area" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Zone" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Zone" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Territory" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Territory" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Distributor" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Distributor" runat="server" Text='<%#Eval("")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                        </asp:GridView>   
                    </td>
                </tr>
            </tbody>
        </table>
                 
  
     <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Visible="false" Height="20px" BorderColor="Black"  BorderStyle="Solid" BorderWidth="2" Font-Bold="True" >No Records Found</asp:Label>
    </center>
     <br />  
    </div>
    </form>
</body>
</html>
