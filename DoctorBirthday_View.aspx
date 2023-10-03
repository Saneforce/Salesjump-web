<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorBirthday_View.aspx.cs" Inherits="DoctorBirthday_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> </title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
         <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
       td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    
</style>
 
    <script type="text/javascript" src="JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="JsFiles/jquery-1.10.1.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
   
    <div>
    <br />
     <center>
    <div style="text-align:center; color:Blue; font-size:medium; text-decoration:underline" >
    Listed Customer(s) Date of Birth and Wedding Anniversary Details 
    </div>
    <br />
        <table >
                <tr>
                   <td align="left">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                 <td align="left">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
    <br />
    </center>
    <center>
    <table width="30%">
    <tr>
    <td >
    <asp:Label ID="lblDate" runat="server" Width="60px" Text="Find Date"   SkinID="lblMand"></asp:Label>
    </td>
    <td style="width:10%">
    <asp:TextBox ID="txtDate" runat="server" Width="30px" SkinID="MandTxtBox" ></asp:TextBox>
    </td>
    <td>
    
    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
    </td>
   
    <td>
    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="BUTTON" Width="30px" Height="25px"
            onclick="btnGo_Click" />
    </td>
        <td>
    <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear" CssClass="BUTTON" />
    </td>
    </tr>
    </table>
    <br />
     <table width="100%"  cellpadding="0" cellspacing="4" align="center">
        <tr>
            <td class="style1">
                <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                    font-size: 12px; text-align: left; margin-top:0px" ForeColor="Blue" Font-Bold="True" Font-Names="Verdana" >
                </asp:Label>
            </td>
        </tr>
    </table>
   
    <br />

       <table width="100%" align="center">
            <tbody>
            <th>Date of Birth View</th>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false"   OnRowDataBound="grdDoctor_RowDataBound"                           
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblSF" runat="server" Text='<%#Eval("sf_name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" ForeColor="Blue" runat="server" Text='<%#Eval("ListedDr_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>                                 
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="ListedDR_Address1" onkeypress="AlphaNumeric(event);" runat="server" SkinID="lblMand" MaxLength="250"
                                            Text='<%#Eval("ListedDR_Address1")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>                    
                              
                                <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="DOB" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB"   ForeColor="Blue" Width="70px" runat="server" Text='<%# Bind("ListedDr_DOB") %>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>     
                                                               
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Phone/Mobile" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmob" runat="server" Text='<%# Bind("ListedDr_Mobile") %>' SkinID="lblMand"></asp:Label>
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

     <table width="100%" align="center">
            <tbody>
            <th>Date of Wedding View</th>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor_Dow" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false"   OnRowDataBound="grdDoctor_Dow_RowDataBound"                             
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblSF" runat="server" Text='<%#Eval("sf_name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblDG" runat="server" Text='<%#Eval("Sf_Designation_Short_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" ForeColor="Blue" runat="server" Text='<%#Eval("ListedDr_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>                                 
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="ListedDR_Address1" onkeypress="AlphaNumeric(event);" runat="server" SkinID="lblMand" MaxLength="250"
                                            Text='<%#Eval("ListedDR_Address1")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>                    
                              
                                <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr2" runat="server" Text='<%# Bind("territory_Name") %>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="DOW" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOW"  ForeColor="BlueViolet" Width="70px" runat="server" Text='<%# Bind("ListedDr_DOW") %>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>     
                                                               
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Phone/Mobile" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmob" runat="server" Text='<%# Bind("ListedDr_Mobile") %>' SkinID="lblMand"></asp:Label>
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
     <table width="100%" align="center">
            <tbody>
            <th >Date of Birth/Date of Wedding View</th>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDobDow" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false"    OnRowDataBound="grdDobDow_RowDataBound"                           
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblSF" runat="server" Text='<%#Eval("sf_name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>                                 
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="ListedDR_Address1" onkeypress="AlphaNumeric(event);" runat="server" SkinID="lblMand" MaxLength="250"
                                            Text='<%#Eval("ListedDR_Address1")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>                    
                              
                                <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr1" runat="server" Text='<%# Bind("territory_Name") %>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>                                                                    
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="DOB" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB1" runat="server" Width="70px" Text='<%# Bind("ListedDr_DOB") %>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>     
                                                               
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="DOW" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOW1" runat="server" Width="70px" Text='<%# Bind("ListedDr_DOW") %>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>     
                                                               
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Phone/Mobile" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("ListedDr_Mobile") %>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>      
                                                               
                                </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"  />
                                </asp:GridView>
    </td>
    </tr>
    </tbody>
    </table>

    </center>
     <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="Images/loader.gif" alt="" />
</div> 
    </div>
    </form>
</body>
</html>
