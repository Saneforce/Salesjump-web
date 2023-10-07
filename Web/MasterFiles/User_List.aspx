<%@ Page Title="User List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="User_List.aspx.cs" Inherits="MasterFiles_User_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>User List</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
         .marright
        {
            margin-left:85%;
        }
    </style>
      <script type="text/javascript">
          function PrintGridData() {

              var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');

              prtGrid.border = 1;
              var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
              prtwin.document.write(prtGrid.outerHTML);
              prtwin.document.close();
              prtwin.focus();
              prtwin.print();
              prtwin.close();
          }

</script>
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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

                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Company Name."); $('#ddlDivision').focus(); return false; }
                var Field = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var Field = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field == "") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var State = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (State == "---Select State---") { alert("Select State Name."); $('#ddlFieldForce').focus(); return false; }


            });
        }); 
    </script>
    <script type="text/javascript">

          function OpenNewWindow() {

              //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

              window.open('UserList_NewWindow.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no,pnllnk.style.visibility = "hidden";');

              return false;
              
          }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>

            <br />
        <center>
            <table cellpadding="1" cellspacing="1" >
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivision" runat="server" Text="Company Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFilter" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired"
                            OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>&nbsp;
                        <asp:Button ID="btnGo" runat="server" Text="Go" Width="35px" Height="25px" OnClick="btnGo_Click"
                            CssClass="btnnew" /> &nbsp;
   <asp:ImageButton ID="imgNew" runat="server" BorderStyle="Solid" BorderColor="Red" Width="18px" ImageUrl="~/Images/new window.png" OnClientClick="return OpenNewWindow() ;" />
                    </td>
                    
                </tr>             
                   
                   
            </table>
            <center>
             <asp:CheckBox ID="chkVacant" Text=" Without - Vacant" Font-Size="Medium" Font-Names="Calibri"
                            ForeColor="Red" Checked="true" runat="server" /> &nbsp;
               
                        
                               </center>
        
                         <asp:CheckBox ID="chkdoctor" Text=" Without - Customer Count" Font-Size="Medium" Font-Names="Calibri"
                            ForeColor="Red" Checked="true" runat="server" /> &nbsp;  
                           
                             <asp:Button ID="btnmgrgo" runat="server" Text="Go" Width="35px" Height="25px" OnClick="btnmgrgo_Click"
                            CssClass="btnnew" />
                            </center>
            <br />
                 <asp:Panel ID="pnlprint" runat="server" CssClass="marright" Visible="false"  > 
 <%--  <input type="button" id="btnPrint" value="Print" style="width:60px;height:25px; background-color:LightBlue; "    />--%>
    <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData()" >
     <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Print.GIF" ToolTip="Print"  Width="20px" style="border-width: 0px;" />
   </asp:LinkButton>
   <asp:LinkButton ID="lnkpdf" ToolTip="Pdf" runat="server" OnClick="btnExcel_Click">
     <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/excel_Img.png" ToolTip="Excel"  Width="20px" style="border-width: 0px;" />
   </asp:LinkButton>
   <asp:LinkButton ID="imgpdf" ToolTip="Pdf" runat="server"  OnClick="btnPDF_Click">
     <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Pdf_Img.jpg" ToolTip="Pdf"  Width="20px" style="border-width: 0px;" />
   </asp:LinkButton>
  </asp:Panel>
   <center>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="95%" HorizontalAlign="Center"
                                EmptyDataText="No Records Found" AutoGenerateColumns="false" OnRowDataBound="grdSalesForce_RowDataBound"
                                BorderStyle="Solid" BorderWidth="1" GridLines="Both"  CssClass="newStly" AlternatingRowStyle-CssClass="alt" HeaderStyle-ForeColor="Black">
                                <HeaderStyle Font-Bold="True" BackColor="#496a9a" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="User Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("Sf_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="RTs Count" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrsCnt" runat="server" Font-Size="12px" Font-Bold="true" Width="10%" Font-Names="sans-serif" Forecolor="Red" Text='<%# Bind("Lst_drCount") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            

                                    <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="200px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlace" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State Name" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("StateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="SF Code" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblKey" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Employee ID" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmp_id" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Employee_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldForce" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									
                                    <asp:TemplateField HeaderText="Joining Date" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblJD" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Sf_Joining_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerritory" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Territory") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Reporting" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTPReporting" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									
                                    <asp:TemplateField HeaderText="Mobile" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMN" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("SF_Mobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

										<asp:TemplateField HeaderText="Division" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Division") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="User Name" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" Width="120px" runat="server" Font-Size="8pt" Font-Names="Verdana" Forecolor="#000000" Style="font-weight:700" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
 								

                                    <asp:TemplateField HeaderText="Password" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPassword" runat="server" Font-Size="8pt" Font-Names="Verdana" Forecolor="#000000" Style="font-weight:700" Text='<%# Bind("sf_password") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblS" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("sf_Tp_Active_flag") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="Verdana" Forecolor="#000000" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFType" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#000000" Text='<%# Bind("sf_type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
        </center>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
           <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>
