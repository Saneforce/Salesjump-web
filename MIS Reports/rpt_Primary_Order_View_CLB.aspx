<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Primary_Order_View_CLB.aspx.cs" Inherits="MIS_Reports_rpt_Primary_Order_View_CLB" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pirmary Order View</title>
 <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>

   <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

     <script language="Javascript">
         function RefreshParent() {
             // window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
<script type="text/javascript">
        function exportTabletoPdf() {

        };
    function closew() {
        $('#cphoto1').css("display", 'none');
    }
    $(document).ready(function () {
        dv = $('<div style="position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
        $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
        $("body").append(dv);
    });
    $(document).on('click', '.view_image', function () {
        var photo = $(this).attr("src");
        $('#photo1').attr("src", $(this).attr("src"));
        $('#cphoto1').css("display", 'block');
    });
      
    </script>

    <script src="../JsFiles/canvasjs.min.js"></script>
	
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        td{padding:2px 5px;}
		.subTot{Font-size:11pt;font-weight:bold;}
		.GrndTot{Font-size:13pt;font-weight:bold;}
        .remove  
  {
    text-decoration:none;
  }
  	.TopButton{
		border-color: Black;
	    border-width: 1px;
	    border-style: Solid;
	    font-family: Verdana;
	    font-size: 10px;
	    height: 25px;
	    width: 60px;
	}
    </style>
 <script type="text/javascript">
     function exportexcel() {
         $("#pnlContents").table2excel({

             filename: "Primary Order View",
             fileext: ".xls"
         });
     };
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
             
                    <td width="60%" align="center" >
                    <asp:Label ID="lblHead" Text="Primary Order View"   Font-Bold="true" STYLE="COLOR: #3F51B5;PADDING-RIGHT: 217PX;"  Font-Size="Large"  Font-Underline="true"
                runat="server" ></asp:Label>
                    </td>
                     <td width="40%" align="right">
                        <table>
                            <tr>
<td>
                                 <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
       <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf" Visible="false"   />
       <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                                        OnClick="btnExcel_Click" />
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                   </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
      <asp:Panel id="pnlContents"  EnableViewState="false" runat="server" Width="100%" style="margin-left:35px">
                <table border="0" id='1'style="margin:auto"   width="100%">                 
                            <tr><td>&nbsp;&nbsp;</td></tr>                         
          <tr align="left"><td align="left" style="font-size: small; font-weight: bold;font-family: Andalus;Padding-left:180px;">
                   <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" style="font-family: Andalus; color:Blue;" ></asp:Label></td></tr>
                         
        
      
                   
                    <tr> 
                        <td width="100%">
                            <asp:GridView ID="gdprimary" runat="server"      ShowHeader="false"
                                HorizontalAlign="Center"  Width="90%"  Font-Names="andalus" OnRowCreated = "OnRowCreated" OnDataBound="OnDataBound"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"   
                                AutoGenerateColumns="true"  class="newStly"
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>
                                
                                    <%--<asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer" ItemStyle-Width="70" HeaderStyle-BorderWidth="1" ItemStyle-Font-Bold="true"  
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="stockist" style="white-space:nowrap" runat="server" Font-Size="10pt" Text='<%#Eval("Listeddr_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Product" ItemStyle-Width="70" HeaderStyle-BorderWidth="1" ItemStyle-Font-Bold="true"  
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="prtname" style="white-space:nowrap" runat="server" Font-Size="10pt" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Stock" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1"  ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_name" runat="server" Font-Size="10pt" Text='<%#Eval("ClStock")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                   
                                      
                                   
                                   
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    </table>
 </asp:Panel>
     

  </form>
</body>
</html>