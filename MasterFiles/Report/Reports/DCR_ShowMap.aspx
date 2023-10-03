<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_ShowMap.aspx.cs" Inherits="MasterFiles_DCR_ShowMap" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShowMap</title>

    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&callback=initialize"></script>
    <script type="text/javascript">
        window.onload = function () {
            var table = document.getElementById("<%= GVMap.ClientID %>");
            if (table != null) {
                var rowCount = table.rows.length;
                var prodVal = '';
                var prodName = '';
                var cnt = 0;
                for (var i = 1; i < rowCount; i++) {
                    var prodVal = "";
                    var row = table.rows[i];
                    var txt = row.getElementsByTagName('input');


                    if (prodVal == "") {

                        prodVal = txt[1].value + '|' + txt[3].value;

                        if (txt[1].value != "") {
                            var mapOptions = {
                                center: new google.maps.LatLng(txt[1].value, txt[3].value),
                                zoom: 14,
                                mapTypeId: google.maps.MapTypeId.ROADMAP
                            };
                            var infoWindow = new google.maps.InfoWindow();
                            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
                            for (var i = 1; i < rowCount; i++) {
                                var row = table.rows[i];
                                var txt = row.getElementsByTagName('input');
                                var data = rowCount

                                var myLatlng = new google.maps.LatLng(txt[1].value, txt[3].value);
                                var marker = new google.maps.Marker({ position: myLatlng, map: map, title: "Address  : " + txt[2].value + "\r\n" + "Doctor Name : " + txt[5].value + " - " + txt[4].value });

                                (function (marker, data) {
                                    google.maps.event.addListener(marker, "click", function (e) {
                                        //                                    infoWindow.setContent(txt[2].value);
                                        //                                    infoWindow.open(map, marker);
                                    });
                                })(marker, data);

                            }
                        }
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">   
    <div id="dvMap" style="width: 100%; height: 800px">
    </div>
    
    <div style="display:none">
    <asp:GridView ID="GVMap" runat="server" Width="0px" Height="0px" HorizontalAlign="Center" BorderWidth="1" 
                    CellPadding="2" OnRowDataBound="GVMap_DataBound" EmptyDataText="No Data found for View"  AutoGenerateColumns="false" 
                    >
                    <HeaderStyle Font-Bold="False" />
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <HeaderStyle BorderWidth="1" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No"  HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date"  HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" Font-Size="9pt" Text='<%#Eval("Trans_Detail_Name")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date"  HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="lati" runat="server" Font-Size="9pt" Text='<%#Eval("lati")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="GeoAddrs" runat="server" Font-Size="9pt" Text='<%#Eval("GeoAddrs")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date"  HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="long" runat="server" Font-Size="9pt" Text='<%#Eval("long")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date"  HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="Activity_Date" runat="server" Font-Size="9pt" Text='<%#Eval("Activity_Date", "{0:dd/MM/yyyy}")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date"  HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="Trans_Detail_Name" runat="server" Font-Size="9pt" Text='<%#Eval("Trans_Detail_Name")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                         VerticalAlign="Middle" />
                </asp:GridView>
    </div>
    </form>
</body>
</html>
