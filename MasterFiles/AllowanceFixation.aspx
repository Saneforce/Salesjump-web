<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllowanceFixation.aspx.cs"
    Inherits="MasterFiles_AllowanceFixation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Allowance Fixation</title>
    <link type="text/css" rel="Stylesheet" href="../css/style.css" />
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
<script type="text/javascript">
    function Calculation() {
    alert('test')
        var grid = document.getElementById("<%= grdWTAllowance.ClientID%>");
        for (var i = 0; i < grid.rows.length - 1; i++) {
            var txtAmountReceive = $("input[id*=myTextBox13]")
            if (txtAmountReceive[i].value != '') {

                alert(txtAmountReceive[i].value);
                
            }
        }
    }  
</script> 
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
    </div>
    <div>
    <center>
        <table width="90%">
            <tr>
            <td style="width:4.6%"></td>
                <td align="left">
                    <asp:Label ID="lblRegionWise" width="110px" runat="server" SkinID="lblMand" Text="Field Force Name"></asp:Label>
                    <asp:DropDownList ID="ddlRegion" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
             
                <td align="right" >
                    <asp:Label ID="lblDate" runat="server" Text="Date"  SkinID="lblMand"></asp:Label>
              
                  <asp:TextBox ID="txtTPStartDate" TabIndex="1" style="font-family:Calibri" runat="server" Width="75px" 
                        ontextchanged="txtTPStartDate_TextChanged"  AutoPostBack="true" >
                        </asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                    TargetControlID="txtTPStartDate" />
                </td>
                 <td style="width:4.6%"></td>
            </tr>
            <tr>
                <td height="20px">
                </td>
            </tr>
          </table>
          </center>
          <center>
          <table width="90%">
            <tr>  
          
           
               <td  align="center">
                    <asp:GridView ID="grdWTAllowance" Width="90%" runat="server" AutoGenerateColumns="false"
                        BorderStyle="Solid" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" 
                        GridLines="None" onrowdatabound="grdWTAllowance_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" SkinID="lblMand" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblFieldForceName" Width="240px" SkinID="lblMand" Text='<%# Eval("sf_Name")%>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF_Code" HeaderStyle-Width="10%" Visible="false" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblsfcode" SkinID="lblMand" Text='<%# Eval("sf_code")%>' Visible="false"
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesignation" SkinID="lblMand" Text='<%# Eval("Designation_Short_Name")%>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblsf_hq" SkinID="lblMand" Width="120px" Text='<%# Eval("sf_hq")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHq" style="text-align:center;font-family:Calibri" Width="50px"  Text='<%# Eval("HQ_Allowance")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EX" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEXHQ" style="text-align:center;font-family:Calibri" Width="60px" Text='<%# Eval("EX_HQ_Allowance")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OS" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOS" style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("OS_Allowance")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hill" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHill" style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("Hill_Allowance")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fare/KM" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFareKm" style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("FareKm_Allowance")%>'  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRangeofFare1" style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("Range_of_Fare1")%>'  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRangeofFare2" style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("Range_of_Fare2")%>'  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField> 

                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox13" style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("Fixed_Column1")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox14" style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("Fixed_Column2")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox15" style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("Fixed_Column3")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            
                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox16"  style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("Fixed_Column4")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            
                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox17"  style="text-align:center;font-family:Calibri" Width="50px" Text='<%# Eval("Fixed_Column5")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>                         
                            
                            <asp:TemplateField HeaderText="Effective From" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEffective" style="text-align:center;font-family:Calibri" Width="80px" runat="server" Text='<%# Eval("Effective_Form")%>' ReadOnly="false" MaxLength="10"></asp:TextBox>
                                   <%-- <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffective"  runat="server" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>  

                            <%--<asp:TemplateField>                                                          
                               <ItemTemplate>
                                    <asp:Repeater ID="rptCont" runat="server">
                                        <ItemTemplate>
                                                   <asp:TextBox ID="txtRept" runat="server"></asp:TextBox> 
                                        </ItemTemplate>
                                    </asp:Repeater>
                              </ItemTemplate>
                            </asp:TemplateField>--%>
                           
                        </Columns>
                    </asp:GridView>
                   
                </td>
                
            </tr>
             
            <tr>
                <td height="20px">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnSave" CssClass="BUTTON" style="margin-left:80px" 
                        runat="server" Width="50px"
                     Text="Save"  OnClick="btnSave_Click" 
                          />
                </td>
            </tr>
        </table>
       </center>
        <br />
           <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
