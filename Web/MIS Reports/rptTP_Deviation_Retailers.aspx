<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTP_Deviation_Retailers.aspx.cs"
    Inherits="MIS_Reports_rptTP_Deviation_Retailers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP - Deviation</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div  width="90%" style="margin: 0px auto; padding-left:5%; padding-right:5%">
    <br />
    
        <asp:Label ID="Label1" runat="server" style="font-size:x-large" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
        <br />
        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="false" ShowFooter="true" width="100%" class="newStly" style="border-collapse: collapse;margin: 0px auto;">
            <Columns>
                <asp:TemplateField HeaderText="S No" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Route Name" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Territory_Name") %>'>  </asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Distributor Name" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("stockist_name") %>'>  </asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Retailer Name" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ListedDr_Name") %>'>  </asp:Label>
                    </ItemTemplate>


                   
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Address" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ListedDr_Address1") %>'>  </asp:Label>
                    </ItemTemplate>


                   
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Special Name" >
                    <ItemTemplate>
                        
                        <asp:Label ID="Label4" runat="server"  Text='<%# Eval("Doc_Special_Name") %>'>  </asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>



                <asp:TemplateField HeaderText="Order Value" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Order_Value") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Remarks" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Activity_Remarks") %>'>  </asp:Label>
                    </ItemTemplate>


                   
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true" width="100%" class="newStly" style="border-collapse: collapse;margin: 0px auto;">
            <Columns>
                <asp:TemplateField HeaderText="S No" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Route Name" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("SDP_Name") %>'>  </asp:Label>
                    </ItemTemplate>                   
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Distributor Name" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Stockist_Name") %>'>  </asp:Label>
                    </ItemTemplate>                   
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Address" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Stockist_Address") %>'>  </asp:Label>
                    </ItemTemplate>                   
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Value" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Order_Value") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Activity_Remarks") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
