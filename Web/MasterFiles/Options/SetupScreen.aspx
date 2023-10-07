<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetupScreen.aspx.cs" Inherits="MasterFiles_Options_SetupScreen" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Setup for Screen Access</title>
      <style type="text/css">
        #tblDocRpt
        {
            margin-left: 300px;
        }
       
    </style>
    
 
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
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

                  var Force = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                  if (Force == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }


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
            <table id="tblState" align="center">
                <tr>
                    <td>
                        <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                     </td>
                     <td>
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                            onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                         <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlFieldForce_SelectedIndexChanged" ></asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>

                    </td>
                </tr>
            </table>
            <br />   
       
            <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" OnClick="btnGo_Click" />
            <br />
            <br />
           <table width="100%" align="center">
            <tbody>               
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSalesForce" runat="server" Width="95%" HorizontalAlign="Center" 
                            AutoGenerateColumns="false" OnRowCreated="grdSalesForce_RowCreated" OnRowDataBound="grdSalesForce_RowDataBound"
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No Records Found" 
                            AlternatingRowStyle-CssClass="alt" ShowHeader="False">
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <RowStyle HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="BurlyWood"/>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>                
                                <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Left">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left" ></ItemStyle>                                
                                    <ItemTemplate>
                                        <asp:Label ID="lblsf_code" runat="server" Text='<%# Bind("sf_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="600px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>
                                        <asp:Label ID="lblsf_name"  runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="200px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>
                                        <asp:Label ID="lblDG"  runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="500px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>
                                        <asp:Label ID="lblsf_hq"  runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">                                
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDoctorAdd" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDoctorEdit" runat="server" />    
                                        </ItemTemplate>
                                </asp:TemplateField>                   
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDoctorDeAct" runat="server" />
                                        </ItemTemplate>
                                </asp:TemplateField>                   
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDoctorView" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>   
                                       <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDoctorReAct" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>    
                                
                                    <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDoctorName" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>    

                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkNewDoctorAdd" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkNewDoctorEdit" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkNewDoctorDeAct" runat="server" />
                                        </ItemTemplate>
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkNewDoctorView" runat="server" />
                                        </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkNewDoctorReAct" runat="server" />
                                        </ItemTemplate>
                                </asp:TemplateField>      
                                              
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkChemAdd" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkChemEdit" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkChemDeAct" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkChemView" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>   
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkChemReAct" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>   

                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTerrAdd" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTerrEdit" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTerrDeAct" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                               <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTerrView" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>    

                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkClassAdd" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkClassEdit" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkClassDeAct" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkClassView" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>     
                                 <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkClassReAct" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                </asp:TemplateField>  
                            </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td> 
                </tr> 
            </tbody>
        </table>


            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Visible="false" Text="Save" CssClass="BUTTON"  onclick="btnSubmit_Click" 
                    />
            &nbsp;
            <asp:Button ID="btnClear" runat="server" CssClass="BUTTON" Width="60px" Height="25px" Visible="false" Text="Clear" />
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
