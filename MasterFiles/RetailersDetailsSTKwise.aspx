<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetailersDetailsSTKwise.aspx.cs" Inherits="MasterFiles_RetailersDetailsSTKwise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

        <title>Distributor</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function popUp(Stockist_Code, sf_Name,sf_code, Stockist_Name) {             

            strOpen = "RetailersDetailsRUTwise.aspx?StockistCode=" + Stockist_Code + "&SFcode=" + sf_code + "&StockistName=" + Stockist_Name
            window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
        }

        
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
            var table_div = document.getElementById('excelDiv');
            var table_html = table_div.outerHTML.replace(/ /g, '%20');
            a.href = data_type + ', ' + table_html;
            //setting the file name
            a.download = 'Retailers_' + postfix + '.xls';
            //triggering the function
            a.click();
            //just in case, prevent default behaviour
            e.preventDefault();
        });
 </script>
</head>
<body>
  
    <form id="form1" runat="server">
<asp:Label ID="lblsf" runat="server"></asp:Label>
     
    <div class="container"  style="max-width: 100%; width: 98%">
    <div class="col-md-8" style="margin: 0px;">
            <asp:Label ID="Label2" runat="server" Text="Distributor Wise Outlet Details" Style="font-weight: bold; font-size: x-large"  ></asp:Label>
        </div>
           <div class="col-md-8" style="margin: 0px; top:60px">
            Field Name : <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div class="col-md-4" style="text-align: right">
            <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                href="#" class="btn btnPrint"></a><a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px;
                    display: none" href="#" class="btn btnPdf"></a><a name="btnExcel" id="btnExcel" type="button"
                        style="padding: 0px 20px;" href="#" class="btn btnExcel"></a><a name="btnClose" id="btnClose"
                            type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                            class="btn btnClose"></a>
        </div>
    </div>
    <div class="container" id="excelDiv" style="max-width: 100%; width: 98%">
        <br />
        <br />
        <br />
        
        <asp:GridView ID="RetailerGrd" runat="server" AutoGenerateColumns="false" class="newStly">
            <Columns>
<asp:TemplateField HeaderText="Field Force">
                        <ItemTemplate>                           
                                <%# Eval("Field_Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>  
                <asp:TemplateField HeaderText="Distributor">
                        <ItemTemplate>                          
                                <%# Eval("Stockist_Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>                   

                    <asp:TemplateField HeaderText="Outlet Count">
                        <ItemTemplate>
                             <a href="javascript:popUp('<%# Eval("Dist_Name") %>','<%# Eval("Field_Name") %>','<%# Eval("Field_Code") %>','<%# Eval("Stockist_Name") %>')">
                            <%# Eval("Cnt")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
            </Columns>
        </asp:GridView>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
