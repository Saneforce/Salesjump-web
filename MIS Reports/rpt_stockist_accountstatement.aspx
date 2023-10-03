<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_stockist_accountstatement.aspx.cs" Inherits="MIS_Reports_rpt_stockist_accountstatement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<title>Stock Account Statement</title>
<head  >
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
   <script type="text/javascript" 
   src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>


  
    <script type="text/javascript">
        $(document).ready(function () {
            var bal = parseInt(document.getElementById('fdatebal').innerText);
            $('[id*=GridView1]').find('tr:has(td)').each(function () {

                var sf_name = $(this).find("td:eq(1)").text();
                var sf_code = $(this).find("td:eq(0) :input").val();
                var credit = parseInt($(this).find("td :eq(3)").text());
                var debit = parseInt($(this).find("td:eq(4)").text());




                bal += credit;
                bal -= debit;

                $(this).find("td:eq(5)").text(bal);


            });
            var sum = 0;
            var debitsum = 0;
            var balncesum = 0;
            var table = document.getElementById('<%=GridView1.ClientID %>');
            var ths = table.getElementsByTagName('th');
            var tds = table.getElementsByClassName('credit');
            for (var i = 0; i < tds.length; i++) {
                sum += isNaN(tds[i].innerText) ? 0 : parseInt(tds[i].innerText);
            }
            var tds1 = table.getElementsByClassName('debit');
            for (var i = 0; i < tds1.length; i++) {
                debitsum += isNaN(tds1[i].innerText) ? 0 : parseInt(tds1[i].innerText);
            }
            balncesum = sum - debitsum;

            var row = table.insertRow(table.rows.length);
            var cell3 = row.insertCell(0);
            cell3.style.textAlign = 'Center'
            cell3.style.fontWeight = 'bold'
            cell3.style.backgroundColor = "#cddc3970"
            var balsum = document.createTextNode(balncesum);


            cell3.appendChild(balsum);
            var cell2 = row.insertCell(0);
            cell2.style.textAlign = 'Center'
            cell2.style.fontWeight = 'bold'
            cell2.style.backgroundColor = "#cddc3970"
            var debitsum = document.createTextNode(debitsum);


            cell2.appendChild(debitsum);
            var cell = row.insertCell(0);

            cell.style.textAlign = 'Center'
            cell.style.fontWeight = 'bold'
            cell.style.backgroundColor = "#cddc3970"

            var creditsum = document.createTextNode(sum);

            cell.appendChild(creditsum);
            var cell1 = row.insertCell(0);

            var totalBalance = document.createTextNode(' Total Balance');

            cell1.colSpan = 3;
            cell1.style.textAlign = 'Center'
            cell1.style.backgroundColor = "#D3D3D3"


            cell1.style.fontWeight = 'bold'

            cell1.appendChild(totalBalance);

            if (balncesum > 0) {
                lbloutputdisplay.innerText = "Current Stock:";
                balvalue.innerText = balncesum;
            }
            else {

                balncesum = Math.abs(balncesum);
                balvalue.innerText = balncesum;
                lbloutputdisplay.innerText = "Current Stock :";
            }
        });
</script>
 
    <!-- Bootstrap core CSS -->
    <%--<link href="../assets/css/bootstrap.css" rel="stylesheet">--%>
    <!--external css-->
  <%--  <link href="../assets/font-awesome/css/font-awesome.css" rel="stylesheet" />--%>
        
    <!-- Custom styles for this template -->
  <%--  <link href="../assets/css/style.css" rel="stylesheet">--%>
<%--    <link href="../assets/css/style-responsive.css" rel="stylesheet">--%>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
  </head>

  <body>
 
  <div class="container" style="padding-bottom:990px;">
      		      <form id="Form1" class="form-login"  runat="server" style="height:688px; padding-bottom:990px;">
		       <h3 align="center"  style="text-decoration: underline">Stock Account Statement  for the period of 
                    &nbsp;<asp:Label ID="fdatee" runat="server" Text="Label" ForeColor="Red"></asp:Label>&nbsp;to &nbsp;<asp:Label ID="todatee"
                        runat="server" Text="Label" ForeColor="Red"></asp:Label></h3>
                </br>
                <table align="center"><tr><td> <asp:Label ID="lblretailer" runat="server" Text="Distributor: "  Font-Bold="true" Font-Names="Andalus"></asp:Label><asp:Label ID="lblretailerval" Font-Names="Andalus"
                        runat="server" Text="Label">  </asp:Label></td><td><asp:Label ID="lblretailerr" runat="server" Text="Product_name: "  Font-Bold="true" Font-Names="Andalus"></asp:Label></td><td>  </td><td> <asp:Label ID="lblrouteval" Font-Names="Andalus"
                        runat="server" Text="Label"></asp:Label></td><td></td></tr></table>
                
		          <h3 align="Center" style="color:#2196F3;text-decoration: underline " >Stock as on   &nbsp;
                      <asp:Label ID="fdate" runat="server" ForeColor="Red"   ></asp:Label>&nbsp;: &nbsp;
                                            <asp:Label ID="fdatebal" runat="server"  BorderColor="Aqua"  Text="0" class="form-control"  style="width:70px;height:20px;" ></asp:Label></h3>
                    <asp:GridView ID="GridView1" runat="server" Width="70%" GridLines="None" AutoGenerateColumns="false"  RowStyle-HorizontalAlign="Center"  ForeColor="Black"  HeaderStyle-VerticalAlign="Middle" BackColor="#cffabd " CellPadding="2" HorizontalAlign="Center"  HeaderStyle-BackColor="#D3D3D3" 
                               HeaderStyle-HorizontalAlign="Center"  BorderStyle="Solid" AlternatingRowStyle-BackColor="#b9f6ca" HeaderStyle-Height="40px">
                    <Columns>
                    

                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"  HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-Height="34" ItemStyle-Width="70"   HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="stockist" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Ledg_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction" ItemStyle-Width="270"   HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_name" runat="server" Font-Size="9pt" Text='<%#Eval("Entryby")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In" ItemStyle-Width="270"  HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
    
 <asp:Label ID="credit" runat="server" Font-Size="9pt" class="credit" Text='<%#Eval("Debit")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Out" ItemStyle-Width="270"  HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:Label ID="debit" runat="server" class="debit" Font-Size="9pt" Text='<%#Eval("Credit")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance" ItemStyle-Width="270"  HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                               <asp:Label ID="debit" runat="server" Font-Size="9pt"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                     
                    </Columns>


                    </asp:GridView>
		            
		            </br>
                    </br>
                    <center>
                    <table><tr style="background-color:#DCDCDC;"><td > <asp:Label ID="lbloutputdisplay" runat="server"  Font-Bold="true" Font-Size="Large"></asp:Label></td><td>  <asp:Label ID="balvalue"  style="border-color:Aqua;  border-width:2px; background-color:beige; color:#2196F3;" runat="server"  Font-Bold="true" Font-Size="Large"></asp:Label></td></tr></table></center>
		           
		
		        </div>
             

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="../assets/js/jquery.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>

  </form></body>



</html>
