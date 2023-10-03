<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mgr_DistributorDashbord.aspx.cs" Inherits="MasterFiles_mgr_DistributorDashbord" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Distributor List</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
 function popup(stcode, stname) {
            url = "rptDis_Terr.aspx?Stockist_Code=" + stcode + "&Stockist_Name=" + stname + ""
            window.open(url, "_blank", "toolbar=1, scrollbars=1, resizable=1, width=1000 height=500");

        }
        $(document).ready(function () {
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
                a.download = 'Distributor_' + postfix + '.xls';
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
    <div class="container" style="max-width: 100%; width: 98%">
        <div class="col-md-8" style="margin: 0px;">
            <asp:Label ID="Label2" runat="server" Text="DISTRIBUTORS LIST" Style="font-weight: bold;
                font-size: x-large"></asp:Label>
        </div>
        <div class="col-md-4" style="text-align: right">
    <%--        <a name="btnExcel" id="btnExcel" type="button" style="padding: 0px 20px;" class="btn btnExcel">

            </a>--%>
            
             <asp:LinkButton ID="LinkButton1" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                            OnClick="btnExcel_Click" />
            
            
            
            
            <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                class="btn btnClose"></a>
        </div>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div class="container" id="excelDiv" style="max-width: 100%; width: 98%">
            <br />
            <br />
            <br />
            <asp:GridView ID="DistributorGrd" runat="server" class="newStly" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Slno">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Distributor Name">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Stockist_Name") %>'>  </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Stockist_Mobile") %>'>  </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Field Force ">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Field_Name") %>'>  </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Territory ">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("Territory") %>'>  </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Route Count">
                        <ItemTemplate>
  <a href="javascript:popup(<%#Eval("Stockist_Code")%>,'<%# Eval("Stockist_Name")%>')">
                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("Sub_Couns") %>'>  </asp:Label>
                                                       </a>
 </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <br />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
