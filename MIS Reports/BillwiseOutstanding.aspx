<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="BillwiseOutstanding.aspx.cs" Inherits="MIS_Reports_BillwiseOutstanding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            font-weight: normal;
            border-radius: 7px;
            width: 100%;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=btnView.ClientID%>').click(function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == 0) { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
            });

        });        
        function popUp(SF_Code, sf_name) {
            var sub_div_code = $('#<%=subdiv.ClientID%>').val();
            strOpen = "BillwiseOutstandingRetailer.aspx?SF_Code=" + SF_Code + "&SF_Name=" + sf_name + "&Sub_Div=" + sub_div_code
            window.open(strOpen, 'PendingBill', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
        }
    </script>
    <form id="form1" runat="server">
    <div class="container" style="width:100%">
        <div class="row">
            <label id="ddLdiv" class="col-md-2 col-md-offset-3 control-label">
                Division</label>
            <div class="col-sm-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control"
                       Width="120" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                Field Force</label>
            <div class="col-sm-6 inputGroupContainer">
                <div class="input-group" id="kk" runat="server">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlFieldForce" runat="server"  CssClass="form-control"
                         Width="500">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-md-offset-5">
                <asp:Button ID="btnView" class="btn btn-primary" runat="server" OnClick="btnGo_Click"
                    Style="vertical-align: middle; width: 100px;" Text="View"></asp:Button>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="GVPendingBill" runat="server" Width="80%" HorizontalAlign="Center"
                    AutoGenerateColumns="false" EmptyDataText="No Record Found" CssClass="newStly"
                    Style="margin: 0 auto;" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="20px" HeaderStyle-ForeColor="white"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Sf_Name" HeaderStyle-Width="280px" HeaderStyle-ForeColor="white"
                            HeaderText="Field Force" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblsfname" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="cnt" HeaderStyle-Width="150px" HeaderStyle-ForeColor="white"
                            HeaderText="Bill Count" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRetailer" runat="server" CausesValidation="False" Text='<%# Eval("cnt") %>'
                                    OnClientClick='<%# "return popUp(\"" + Eval("SF_CODE") + "\",\"" + Eval("Sf_Name") + "\");"%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="cnt" HeaderStyle-Width="150px" HeaderStyle-ForeColor="white"
                            HeaderText="Balance Amount" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblamount" runat="server" Text='<%# Bind("BalAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                    <FooterStyle BackColor="#496a9a" ForeColor="White" Font-Bold="true" />
                </asp:GridView>
            </div>
        </div>
    </div>
   
    </form>
</asp:Content>
