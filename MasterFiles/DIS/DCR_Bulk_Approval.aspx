<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Bulk_Approval.aspx.cs" Inherits="MasterFiles_MGR_DCR_Bulk_Approval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DCR - Approval</title>
 <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />
        <link type="text/css" rel="stylesheet" href="../../css/style.css" />
       <script type="text/javascript">
           function checkAll() {
           var grid = document.getElementById('<%= grdDCR.ClientID %>');
           if (grid != null) {
               var inputList = grid.getElementsByTagName("input");               
               var cnt = 0;
               var index = '';
               var chkall = document.getElementById('grdDCR_ctl01_chkAll');
               var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
               for (i = 2; i < inputList.length; i++) {                  
                       if (i.toString().length == 1) {
                           index = cnt.toString() + i.toString();
                       }
                       else {
                           index = i.toString();
                       }
                       var chkapp = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');
                       
                       var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');                     
                       if (chkall.checked) {
                           chkapp.checked = true;
                           chkrej.checked = false;
                           chkrejall.checked = false;
                       }

                       else {
                           chkapp.checked = false;
                       }
                   }
               }
           }

           function checkAllRej() {
               var grid = document.getElementById('<%= grdDCR.ClientID %>');
               if (grid != null) {
                   var inputList = grid.getElementsByTagName("input");
                   var cnt = 0;
                   var index = '';
                   var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                   var chkall = document.getElementById('grdDCR_ctl01_chkAll');
                   for (i = 2; i < inputList.length; i++) {
                       if (i.toString().length == 1) {
                           index = cnt.toString() + i.toString();
                       }
                       else {
                           index = i.toString();
                       }
                       var chkapp = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');
                       var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                       if (chkrejall.checked) {
                           chkrej.checked = true;
                           chkapp.checked = false;
                           chkall.checked = false;
                       }
                       else {

                           chkrej.checked = false;
                       }
                   }
               }
           }
           function checkapp() {
               var grid = document.getElementById('<%= grdDCR.ClientID %>');

               if (grid != null) {

                   var inputList = grid.getElementsByTagName("input");

                   var cnt = 0;
                   var index = '';

                   for (i = 2; i < inputList.length; i++) {

                       if (i.toString().length == 1) {
                           index = cnt.toString() + i.toString();
                       }
                       else {

                           index = i.toString();
                       }

                       var chkapp = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');
                       var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                       
                       if (chkapp.checked) {
                           chkrej.checked = false;
                        
                       }
                   }
               }
           }
            function checkrej() {
               var grid = document.getElementById('<%= grdDCR.ClientID %>');

               if (grid != null) {


                   var inputList = grid.getElementsByTagName("input");

                   var cnt = 0;
                   var index = '';
                   
                   for (i = 2;  i <inputList.length; i++) {

                       if (i.toString().length == 1) {
                           index = cnt.toString() + i.toString();
                       }
                       else {

                           index = i.toString();
                       }

                       var chkapp = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');
                       var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                       if (chkrej.checked) {
                           chkapp.checked = false;
                    }
               }
             }
           }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <br />
    <div style="margin-left:90%">
    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" /> 

     </div> 
     <br />
    <div>
 
   
        <table id="tblPreview_LstDoc" runat="server" style="width: 100%;  font-family:Bookman Old Style; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
             
                    <tr>
                        <td align="center">
                           <asp:Label ID="lblText" runat="server" Font-Size="Small" Font-Names="Bookman Old Style"   Font-Underline ="true"  Font-Bold = "true" 
                                text="Daily Calls Entry For  "></asp:Label>
                           
                        </td>
                    </tr>                        
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
             
         </table>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDCR" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt" 
                            OnRowDataBound="grdDCR_RowDataBound">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Approve" ItemStyle-HorizontalAlign="Center">
                                     <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" Text="  Approve All" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:CheckBox ID="chkAppDCR" runat="server" onclick="checkapp(this); "/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                                 <HeaderTemplate>
                                        <asp:CheckBox ID="chkRejAll" runat="server" Text="  Reject All" onclick="checkAllRej(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRjtDCR" runat="server" onclick="checkrej(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="trans_slno" visible = "false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltrans_slno" runat="server" Text='<%#Eval("trans_slno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           
                               
                                <asp:HyperLinkField HeaderText="Activity Date"  DataTextField="Activity_Date" 
                                    DataNavigateUrlFormatString="~/MasterFiles/MR/DCR/MR_DCR_Approval.aspx?sfcode={0}&amp;trans_slno={1}&amp;Activity_Date={2}"
                                    DataNavigateUrlFields="SF_Code,trans_slno,Activity_Date" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>  
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                
                                   
                                <asp:TemplateField HeaderText="Plan" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerr" runat="server" Text='<%#Eval("Plan_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Type" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblWork" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doc Met">
                                    <ItemTemplate> 
                                        <asp:Label ID="lblDocMet" runat="server" Text='<%#Eval("doc_cnt")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Chem Met">
                                    <ItemTemplate> 
                                        <asp:Label ID="lblChemMet" runat="server" Text='<%#Eval("che_cnt")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Stockist Met">
                                    <ItemTemplate> 
                                        <asp:Label ID="lblStockMet" runat="server" Text='<%#Eval("stk_cnt")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hospitals Met">
                                    <ItemTemplate> 
                                        <asp:Label ID="lblhosMet" runat="server" Text='<%#Eval("hos_cnt")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unlisted Doc Met">
                                    <ItemTemplate> 
                                        <asp:Label ID="lblNewDocMet" runat="server" Text='<%#Eval("unlst_cnt")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate> 
                                        <asp:TextBox ID="lblRemarks" runat="server" Width="250px" Text='<%#Eval("Remarks")%>' TextMode ="MultiLine" ReadOnly = "true" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason for Rejection">
                                    <ItemTemplate> 
                                        <asp:TextBox ID="txtReason" runat="server" Width="200" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                           </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>

  </div>
  <br />
  <center>
        <asp:Button ID="btnApprove" runat="server" BackColor="LightBlue" Text="Approve" Width="100px" Height="25px" 
            onclick="btnApprove_Click" />&nbsp;&nbsp;
       <asp:Button ID="btnReject" runat="server" BackColor="LightBlue" Text="Reject" Width="90px" Height="25px" 
            onclick="btnReject_Click" />
        </center>
    <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>

</html>
