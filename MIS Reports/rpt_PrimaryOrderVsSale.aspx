<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_PrimaryOrderVsSale.aspx.cs" Inherits="MIS_Reports_rpt_PrimaryOrderVsSale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Order Vs sale</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
       <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript">
        function popUp(Sf_Name, Stockist_Name, Sf_Code, Trans_Sl_No, order_value, div) {
            var url = "Rpt_PrimaryOrderSFdetail.aspx?Sf_Code=" + Sf_Code + "&Sf_Name=" + Sf_Name + "&Stockist_Name=" + Stockist_Name + "&Trans_Sl_No=" + Trans_Sl_No + "&order_value=" + order_value + "&div=" + div
            window.open(url, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
        }

        $(document).ready(function () {
            $("#btnExcel").on('click', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divexcel').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'PrimaryOrderVSsale xls';
                a.click();
                e.preventDefault();

            });
        });
           </script>
  
        
</head>
<body>
    <form id="form1" runat="server">
        <div >
              <div class="col-md-4" style="text-align: right">              
                 
                  <a name="btnExcel" id="btnExcel" type="button"   style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
                  <a name="btnClose" id="btnClose"  type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                  </div>
            <div id="divexcel">
            <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="false" class="newStly"  ShowFooter="true">
                <Columns>
                     <asp:TemplateField HeaderText="Sl No">
                        <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Date" >
                        <ItemTemplate>
  <%#Eval("Order_Date")%>
                            </ItemTemplate>
                    </asp:TemplateField>              
                         <asp:TemplateField HeaderText=" Field Force">
                             <ItemTemplate>                                 
                                 <%#Eval("Sf_Name")%>
                             </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Distributer">
                        <ItemTemplate>
                            <%#Eval("Stockist_Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="sfcode">
                        <ItemTemplate>
                            <%#Eval("Sf_Code") %>
                        </ItemTemplate>
                    </asp:TemplateField>
  <asp:TemplateField HeaderText=" Order No" >
                        <ItemTemplate>
                            <a href="javascript:popUp('<%# Eval("Sf_Name") %>','<%# Eval("Stockist_Name")%>','<%#Eval("Sf_Code") %>', '<%#Eval("Trans_Sl_No") %>','<%#Eval("order_value") %>','<%=div%>')">
                            <%#Eval("Trans_Sl_No") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Order Value">
                        <ItemTemplate>
                            <%#Eval("order_value") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="OrderVsSale">
                        <ItemTemplate>
                            <%#Eval("order_flag") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>              
            </asp:GridView>
                </div>
        </div>
    </form>
</body>
</html>
