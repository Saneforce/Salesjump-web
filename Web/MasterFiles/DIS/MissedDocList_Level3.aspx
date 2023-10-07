<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MissedDocList_Level3.aspx.cs" Inherits="MIS_Reports_MissedDocList_Level3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missed Customer List</title>
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
       <table width="100%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" GridLines="None" CssClass="mGrid">
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Right">
                                <ControlStyle Width="90%"></ControlStyle>                                                               
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ListedDr_Address1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="DOB/DOW" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDOB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ListedDr_DOB","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Right">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>        
                            <asp:TemplateField HeaderText="EMail" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("ListedDr_Email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    

                        </Columns>
                    </asp:GridView>
                </td> 
            </tr>
        </tbody>
    </table>
     </center>          
    
    
    </div>
    </form>
</body>
</html>
