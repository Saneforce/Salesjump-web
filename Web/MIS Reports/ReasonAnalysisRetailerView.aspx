<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReasonAnalysisRetailerView.aspx.cs"
    Inherits="MIS_Reports_ReasonAnalysisRetailerView" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reason Analysis</title>
<link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
 <link href="../css/style.css" rel="stylesheet" />


    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    
 <style type="text/css">
        #GVRetailer td {
            white-space:nowrap;
        }

    </style>
<script type="text/javascript">
        $(document).ready(function () {

           
            $(document).on('click', "#btnClose", function () {
                window.close();
            });

		 
		
			$(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;           
            });

			   $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#content').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'ReasonAnalysis.xls';
                a.click();
                e.preventDefault();
            });
        });
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="container" id="content" style="max-width: 100%; width: 95%; margin: 0px auto;margin-top: 10px;">
        <div class="row" style="max-width: 100%; width: 98%;margin: 0px;">
            <div class="col-md-8">
                <asp:Label ID="lblhead" runat="server"  Style="font-weight: bold;
                    font-size: x-large"></asp:Label>
            </div>
            <div class="col-md-4" style="text-align: right">
            <a name="btnPrint" id="btnPrint" type="button" style="padding: 0px 20px;" href="#" class="btn btnPrint"></a>
            <a name="btnPdf" id="btnPdf" type="button"   style="padding: 0px 20px;display:none" href="#" class="btn btnPdf"></a>
            <a name="btnExcel" id="btnExcel" type="button" style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
			<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
        </div>

        <span style=" color:#1000ff; font-weight:bold" >Field Force : </span> <asp:Label ID="Label2" runat="server" Text="Label" ></asp:Label>
		<br/>
        <span style=" color:#1000ff; font-weight:bold" >Reason : </span><asp:Label ID="Label1" runat="server" Text="Label" ></asp:Label>

        <asp:GridView ID="GVRetailer" runat="server" width="100%"  class="newStly">
            <Columns>
                <asp:TemplateField HeaderText="Sl.No." HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle CssClass="table_04" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle CssClass="table_02" HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
    </form>
</body>
</html>
