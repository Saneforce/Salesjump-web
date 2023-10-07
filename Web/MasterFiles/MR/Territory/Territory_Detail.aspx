<%@ Page Title="Route Add" Language="C#" AutoEventWireup="true" CodeFile="Territory_Detail.aspx.cs" MasterPageFile="~/Master.master" 
    Inherits="MasterFiles_Territory_Detail" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Route Add</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <style type="text/css">
       #chkboxLocation label {
margin-bottom: 0px;
}
 table td, table th
{
margin-bottom: 0px;
padding: 0px;
}


    </style>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#back").on('click', function () {
                if ('<%=Session["div_code"]%>' == '32' || '<%=Session["div_code"]%>' == '43' || '<%=Session["div_code"]%>' == '48') 
				{
                    window.location.href = "Territory.aspx";
                }
                else {
                    window.location.href = "../../Route_Master.aspx";
                }
            })
            $('input:text:first').focus();
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
            $('#<%=btnSave.ClientID%>').click(function () {
                //if ($('#<%=txtRoutecode.ClientID%>').val() == "") { alert("Enter Route code."); $('#<%=txtRoutecode.ClientID%>').focus(); return false; }
                if ($('#<%=txtRouteName.ClientID%>').val() == "") { alert("Enter Route Name."); $('#<%=txtRouteName.ClientID%>').focus(); return false; }
                if ($('#<%=txt_Target.ClientID%>').val() == "") { alert("Enter Target."); $('#<%=txt_Target.ClientID%>').focus(); return false; }
                if ($('#<%=txtMinProd.ClientID%>').val() == "") { alert("Enter MinProd."); $('#<%=txtMinProd.ClientID%>').focus(); return false; }
                var type = $('#<%=ddlTerritoryName.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Territory."); $('#<%=ddlTerritoryName.ClientID%>').focus(); return false; }
				 var SName = $('#<%=DDL_aw_Type.ClientID%> :selected').text();
                if (SName == "---Select---") { alert("Please Select Allowance_Type."); $('#<%=DDL_aw_Type.ClientID%>').focus(); return false; }            
                if ($('#<%=chkboxLocation.ClientID%> input:checked').length > 0) { return true; } else { alert(' Distributor Name '); return false; }
                
            });
        });
		function CheckAll(thisObject) {
            var listBox = document.getElementById('<%= chkboxLocation.ClientID %>');
               var inputItems = listBox.getElementsByTagName("input");
               for (index = 0; index < inputItems.length; index++) {
                   if (inputItems[index].type == 'checkbox') {
                       inputItems[index].checked = thisObject.checked;

                   }
               }
           }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <button id="back" type="button" class="btn btn-primary" style="float:right">Back</button>
      <%-- <a class="btn btn-primary" href="../../Route_Master.aspx" style="margin-left: 90%; margin-top: -1%;">Back</a>--%>
        <div id="Divid" runat="server">
        </div>
        <br />
     <asp:Panel ID="pnlsf" runat="server" Visible="false" HorizontalAlign="Left" CssClass="marRight">
            <asp:Label ID="lblTerrritory" runat="server" Visible="false" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" CssClass="marRight">
            <asp:Label ID="Lab_DSM" runat="server" Visible="false" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <%--  <center>
        <table>
          <tr>
                <td>
                    <asp:Label ID="lblN" runat="server" Font-Size="Small" Font-Bold="true" Text="Name:" ></asp:Label>                   
                    <asp:Label ID="lblName" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="true" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                  &nbsp;&nbsp;</td>
             
           
                <td>
                <asp:Label ID="lblty" runat="server" Font-Size="Small" Font-Bold="true" Text="Type:" ></asp:Label>               
                    <asp:Label ID="lblType" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="true" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                </td>
               
            </tr>
          </table>
         </center>--%>
        <br />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblDocCatDtls">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblRoutecode" runat="server" SkinID="lblMand" Height="19px" Width="120px" Visible="false"><span style="Color:Red">*</span>Route Code</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtRoutecode" TabIndex="1" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" runat="server" MaxLength="10" onkeypress="AlphaNumeric_NoSpecialChars(event);" Visible="false">
                        </asp:TextBox>
                    </td></tr><tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblRouteName" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>Route Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtRouteName" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="stylespc" align="left">
                        <asp:Label ID="lblRoute_Target" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Target</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txt_Target" runat="server" SkinID="MandTxtBox" Width="144px" MaxLength="50"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            TabIndex="3" CssClass="TEXTAREA" onkeypress="CheckNumeric(event);" 
                            Height="16px" Text="0"></asp:TextBox>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMinProd" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>MinProd %</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txtMinProd" runat="server" SkinID="TxtBxNumOnly" MaxLength="15"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            Width="163px" TabIndex="4" CssClass="TEXTAREA" onkeypress="CheckNumeric(event);" Text="0"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                     <td class="stylespc" align="left">
                         <asp:Label ID="lblRoutePopulation" runat="server"  SkinID="lblMand"><span style="Color:red"></span>Route Population</asp:Label>
                      </td>
                     <td class="stylespc" align="left">
                         <asp:TextBox ID="txtRoutePopulation" runat="server" SkinID="TxtBxNumOnly" MaxLength="15" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            Width="163px" TabIndex="4" CssClass="TEXTAREA" onkeypress="CheckNumeric(event);" Text="0"></asp:TextBox>
                     </td>
                    <td class="stylespc" align="left">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Visible="false"><span style="Color:Red"></span>DSM Name</asp:Label>
                        <asp:Label ID="Label3" runat="server"  SkinID="lblMand"><span style="Color:red">*</span>Territory</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                       <asp:DropDownList ID="ddldsm" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlTerritoryName" runat="server" EnableViewState="true" CssClass="DropDownList"
                            Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            OnSelectedIndexChanged="ddlTerritoryName_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>                    
                </tr>
  				<tr>
                     <td class="space" align="left">
                        <asp:Label ID="Lbl_aw_type" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True"><span style="Color:Red">*</span>Allowance Type</asp:Label>
                    </td>
                   <td class="space" align="left">
                        <asp:DropDownList ID="DDL_aw_Type" runat="server" SkinID="ddlRequired" Visible="true" OnSelectedIndexChanged="DDL_aw_Type_SelectedIndexChanged" AutoPostBack="true">
                        <%--<asp:ListItem Value="0">---Select---</asp:ListItem>--%>
                        <asp:ListItem Value="1">HQ</asp:ListItem>
                        <asp:ListItem Value="2">EX</asp:ListItem>
                        <asp:ListItem Value="3">OS</asp:ListItem>
                        <asp:ListItem Value="4">OS-EX</asp:ListItem>
                        </asp:DropDownList>
                    </td> 
					<td class="space" align="left">
                        <asp:Label ID="lblTown" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True"><span style="Color:Red">*</span>Town</asp:Label>
                    </td>
                   <td class="space" align="left">
                        <asp:DropDownList ID="ddl_town" runat="server" SkinID="ddlRequired" Visible="true"  AutoPostBack="true">                        
                        </asp:DropDownList>
                    </td> 

                    <td class="space" align="left">
                        <asp:Label ID="lblstay" SkinID="lblMand" runat="server" Visible="false" Font-Bold="True"><span style="Color:Red">*</span>Stay Place</asp:Label>
                    </td>
                   <td class="space" align="left">
                        <asp:DropDownList ID="DDLStay" runat="server" SkinID="ddlRequired" Visible="false">                        
                        </asp:DropDownList>
                    </td> 


                    </tr>                  
            </table>
        </center>
        <br />

         <asp:Panel ID="Panel2" runat="server">
        
        <center>
        <table border="0" cellpadding="0" cellspacing="0" id="Table1" align="center" style="width: 70%;">
                <tr>
                    <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white; padding: 6px;">
                        &nbsp;Distributor&nbsp;
						<asp:CheckBox ID="chkBox" runat="server" align="right" Text="Select All" OnClick="CheckAll(this);" />
                    </td>
                </tr>
            </table>
            <table align="center" border="1" cellpadding="0" cellspacing="0" style="width: 70%;
                margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                <tr>
                    <td class="style71" align="left">
                        <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="subdivision_name"
                            CssClass="chkboxLocation" DataValueField="subdivision_code" Font-Names="Verdana"
                            Font-Bold="true" ForeColor="BlueViolet" Font-Size="X-Small" RepeatColumns="3"
                            RepeatDirection="Horizontal" Width="100%" TabIndex="29" >
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            </center>
            <br />

              <center>
        <table border="0" cellpadding="0" cellspacing="0" id="Table2" align="center" style="width: 70%;">
                <tr>
                    <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white; padding: 6px;">
                        &nbsp;Field Force&nbsp;
                    </td>
                </tr>
            </table>
            <table align="center" border="1" cellpadding="0" cellspacing="0" style="width: 70%;
                margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                <tr>
                    <td class="style71" align="left">
                        <asp:CheckBoxList ID="DDL_FO" runat="server" DataTextField="Sf_Name"
                            CssClass="chkboxLocation" DataValueField="Sf_Code" Font-Names="Verdana"
                            Font-Bold="true" ForeColor="BlueViolet" Font-Size="X-Small" RepeatColumns="3"
                            RepeatDirection="Horizontal" Width="100%" TabIndex="29" >
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            </center>
          </asp:Panel>
            <br />
        <center>
            <table cellpadding="5" cellspacing="10" width="8%">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                            Text="Save" OnClick="btnSave_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                            Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
</asp:Content>