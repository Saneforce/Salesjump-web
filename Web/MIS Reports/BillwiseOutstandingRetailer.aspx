<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillwiseOutstandingRetailer.aspx.cs"
    Inherits="MIS_Reports_BillwiseOutstandingRetailer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <title>Retailerwise Outstanding</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

           
            $(document).on('click', "#btnClose", function () {
                window.close();
            });

			 $(document).on('click', "#btnExcel", function (e) {
				 var dt = new Date();
	       		 var day = dt.getDate();
	       		 var month = dt.getMonth() + 1;
	       		 var year = dt.getFullYear();
				 var postfix = day + "_" + month + "_" + year;	
		        //creating a temporary HTML link element (they support setting file names)
		        var a = document.createElement('a');
		        //getting data from our div that contains the HTML table
		        var data_type = 'data:application/vnd.ms-excel';
		        var table_div = document.getElementById('content');
		        var table_html = table_div.outerHTML.replace(/ /g, '%20');
		        a.href = data_type + ', ' + table_html;
		        //setting the file name
		        a.download = 'Retailerwise_Outstanding_' + postfix + '.xls';
		        //triggering the function
		        a.click();
		        //just in case, prevent default behaviour
		        e.preventDefault();
	  		  });

			 
        });
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto; width: 90%">
        <br />
        <div class="row" style="width: 100%">
            <div class="col-sm-8">
                <asp:Label ID="Label2" runat="server" Text="Retailerwise Outstanding" Style="font-weight: bold;
                    font-size: x-large"></asp:Label>
            </div>
            <div class="col-sm-4" style="text-align: right">
                <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn" >
                    <i class="fa fa-file-excel-o" style="font-size: 36px; color: green"></i></a>
                <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();"
                    class="btn" ><i class="fa fa-close" style="font-size: 36px;
                        color: red"></i></a>
            </div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <div id="content" style="padding: 10px 0px;">
            <asp:GridView ID="GVRetailer" runat="server" Width="100%" HorizontalAlign="Center"
                AutoGenerateColumns="false" EmptyDataText="No Records Found" CssClass="newStly"
                ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="50px" 
                        ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ListedDr_Name" HeaderStyle-Width="250px" 
                        HeaderText="Retailer Name" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
  <asp:TemplateField SortExpression="Territory_Name" HeaderStyle-Width="250px" 
                        HeaderText="Route" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblterritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField SortExpression="BillNo" HeaderStyle-Width="150px" 
                        HeaderText="Bill No." ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblbilno" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Date" HeaderStyle-Width="150px" 
                        HeaderText="Date" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="BillAmt" HeaderStyle-Width="150px" 
                        HeaderText="Bill Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblbiamount" runat="server" Text='<%# Bind("BillAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Coll_Amt" HeaderStyle-Width="150px" 
                        HeaderText="Paid Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblcamount" runat="server" Text='<%# Bind("Coll_Amt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="bal_Amt" HeaderStyle-Width="150px" 
                        HeaderText="Balance Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblbamount" runat="server" Text='<%# Bind("bal_Amt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#496a9a" ForeColor="White" Font-Bold="true" />                
            </asp:GridView>
        </div>
        
    </div>
    </form>
</body>
</html>
