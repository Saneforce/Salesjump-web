<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetailersDetailsSFwise.aspx.cs"
    Inherits="MasterFiles_RetailersDetailsSFwise" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retailers</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        function popUp(Stockist_Code, sftype,sf_name) {
     if (sftype == 2) {
                strOpen = "RetailersDetailsSFwise.aspx?SFCode=" + Stockist_Code + "&SFName=" + sf_name
                window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
            }
            else {
                strOpen = "RetailersDetailsSTKwise.aspx?SFCode=" + Stockist_Code + "&SFName=" + sf_name
                window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
}       
        }

        function RefreshParent() {
            window.close();
        }
        $(document).ready(function () {
            $('#<%=RetailerGrd.ClientID%> tr').each(function () {
                tds = $(this).find("td");
                console.log($.trim($(tds[0]).text()).toString() + ":" + $.trim($('#<%=hdsfName.ClientID%>').val()).toString());
                if ($.trim($(tds[0]).text()).toString() == $.trim($('#<%=hdsfName.ClientID%>').val()).toString()) {
                    console.log($.trim($(tds[0]).text()).toString());
                    if ($.trim($(tds[0]).text()).toString() == "admin") {
                        $(this).hide();
                    }
                    else {
                        
                        $(tds[0]).html($(tds[0]).text());
                    }

                }
            });
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
        <asp:HiddenField ID="hd_sfcode" runat="server" />
        <asp:HiddenField ID="hdsfName" runat="server" />
        <asp:HiddenField ID="hdSub_Div" runat="server" />
    <div class="container"  style="max-width: 100%; width: 98%">
    <div class="col-md-8" style="margin: 0px;">
            <asp:Label ID="Label2" runat="server" Text="Master Details" Style="font-weight: bold; font-size: x-large"  ></asp:Label>
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
            <%--    
                <asp:TemplateField HeaderText="Slno">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
      
                <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label ID="RCode" runat="server" Text='<%# Eval("ListedDrCode")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="RCode" runat="server" Text='<%# Eval("ListedDr_Name")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="RCode" runat="server" Text='<%# Eval("ListedDr_Created_Date")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Field Force">
                    <ItemTemplate>
                        <asp:Label ID="RCode" runat="server" Text='<%# Eval("SFName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Distributor">
                    <ItemTemplate>
                        <asp:Label ID="RCode" runat="server" Text='<%# Eval("DistName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Territory">
                    <ItemTemplate>
                        <asp:Label ID="RCode" runat="server" Text='<%# Eval("hQ")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Route">
                    <ItemTemplate>
                        <asp:Label ID="RCode" runat="server" Text='<%# Eval("tRoute")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> --%>
					<asp:TemplateField HeaderText="Names">
                        <ItemTemplate>
                            <a href="javascript:popUp('<%# Eval("sfCode") %>',<%# Eval("sftype") %>, '<%# Eval("sfName") %>')">
                                <%# Eval("sfName") %>
                        </ItemTemplate>
                    </asp:TemplateField>                   

                    <asp:TemplateField HeaderText="Retailer">
                        <ItemTemplate>
                            <%# Eval("RteCount")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Distributor">
                        <ItemTemplate>
                            <%# Eval("DisCount")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Route">
                        <ItemTemplate>
                            <%# Eval("RutCount")%>
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
