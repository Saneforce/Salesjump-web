<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_PrimaryOrderSFdetail.aspx.cs" Inherits="MIS_Reports_Rpt_PrimaryOrderSFdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Order VS Sale</title>
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
       <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript">
        $(document).on('click', '#divexcel', function (e) {
            var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divExcel').html());
            var a = document.createElement('a');
            a.href = data_type;
            a.download = 'PrimaryOrderReport xls';
            a.click();
            e.preventDefault();

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <div class="col-md-4" style="text-align: right">              
                 
                  <a name="btnExcel" id="btnExcel" type="button"   style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
                  <a name="btnClose" id="btnClose"  type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                  </div>
            <div id="divexcel">
            <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="false" class="newStly"  >
                <Columns>
                     <asp:TemplateField HeaderText="Sl No">
                        <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Name" >
                        <ItemTemplate>
  <%#Eval("Product_Name")%>
                            </ItemTemplate>
                    </asp:TemplateField>              
                         <asp:TemplateField HeaderText="Distributer">
                             <ItemTemplate>
                                 <%#Eval("Stockist_Name")%>
                             </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Super Stockist">
                        <ItemTemplate>

                            <%#Eval("S_Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
  <asp:TemplateField HeaderText=" Order No" >
                        <ItemTemplate>
                            <%#Eval("Trans_Sl_No") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Case Quantity">
                        <ItemTemplate>
                            <%#Eval("CaseCnfQty") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Piece Quantity">
                        <ItemTemplate>
                            <%#Eval("PcsCnfQty") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Rate">
                        <ItemTemplate>
                            <%#Eval("Rate") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Order Value">
                        <ItemTemplate>
                            <%#Eval("value") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>              
            </asp:GridView>
                </div>
        </div>
    </form>
</body>
</html>
