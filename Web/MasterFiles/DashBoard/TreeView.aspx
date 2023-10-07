<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreeView.aspx.cs" Inherits="MasterFiles_DashBoard_TreeView" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>

 

    <style type="text/css">
        .accordionContent
        {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 20%;
        }
        
        .accordionHeaderSelected
        {
            background-color: #5078B3;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
        }
        
        .accordionHeader
        {
            background-color: #2E4D7B;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
        }
        
        .href
        {
            color: White;
            font-weight: bold;
            text-decoration: none;
        }
        
        .gridtable
        {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }
        .gridtable th
        {
            border-width: 1px;
            border-style: solid;
            border-color: #666666;
            background-color: #A6A6D2;
        }
        .gridtable td
        {
            border-color: #666666;
            background-color: #ffffff;
        }
        
        .fancy-green .ajax__tab_header
        {
            background: url(green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
            background: url(green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
            background: url(green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
            height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
            height: 46px;
            margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
            margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
            color: #fff;
        }
        .fancy .ajax__tab_body
        {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
        .button
        {
            font-family: Helvetica, Arial, sans-serif;
            font-size: 18px;
            font-weight: bold;
            color: #FFFFFF;
            padding: 10px 45px;
            margin: 0 20px;
            text-decoration: none;
        }
        .shape-1
        {
            -webkit-border-radius: 5px 50px 5px 50px;
            border-radius: 5px 50px 5px 50px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 50px;
            -moz-border-radius-bottomleft: 50px;
            -moz-border-radius-bottomright: 5px;
        }
        
        
        .shape-2
        {
            -webkit-border-radius: 50px 5px 50px 5px;
            border-radius: 50px 5px 50px 5px;
            -moz-border-radius-topleft: 50px;
            -moz-border-radius-topright: 5px;
            -moz-border-radius-bottomleft: 5px;
            -moz-border-radius-bottomright: 50px;
        }
        
        .effect-4
        {
            transition: border-radius 1s;
            -webkit-transition: border-radius 1s;
            -moz-transition: border-radius 1s;
            -o-transition: border-radius 1s;
            -ms-transition: border-radius 1s;
        }
        
        
        
        .effect-4:hover
        {
            border-radius: 50px 5px 50px 5px;
            -webkit-border-radius: 50px 5px 50px 5px;
            -moz-border-radius-topleft: 50px;
            -moz-border-radius-topright: 5px;
            -moz-border-radius-bottomleft: 5px;
            -moz-border-radius-bottomright: 50px;
        }
        
        .effect-5
        {
            transition: border-radius 1s;
            -webkit-transition: border-radius 1s;
            -moz-transition: border-radius 1s;
            -o-transition: border-radius 1s;
            -ms-transition: border-radius 1s;
        }
        
        
        
        .effect-5:hover
        {
            border-radius: 5px 50px 5px 50px;
            -webkit-border-radius: 5px 50px 5px 50px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 50px;
            -moz-border-radius-bottomleft: 50px;
            -moz-border-radius-bottomright: 5px;
        }
        .green
        {
            border: solid 1px #3b7200;
            background-color: #88c72a;
            background: -moz-linear-gradient(top, #88c72a 0%, #709e0e 100%);
            background: -webkit-linear-gradient(top, #88c72a 0%, #709e0e 100%);
            background: -o-linear-gradient(top, #88c72a 0%, #709e0e 100%);
            background: -ms-linear-gradient(top, #88c72a 0% ,#709e0e 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#709e0e', endColorstr='#709e0e',GradientType=0 );
            background: linear-gradient(top, #88c72a 0% ,#709e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #66FF00, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #66FF00, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #66FF00, inset 0px 0px 1px #FFFFFF;
        }
        
        
        
        .red
        {
            border: solid 1px #720000;
            background-color: #c72a2a;
            background: -moz-linear-gradient(top, #c72a2a 0%, #9e0e0e 100%);
            background: -webkit-linear-gradient(top, #c72a2a 0%, #9e0e0e 100%);
            background: -o-linear-gradient(top, #c72a2a 0%, #9e0e0e 100%);
            background: -ms-linear-gradient(top, #c72a2a 0% ,#9e0e0e 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e0e0e', endColorstr='#9e0e0e',GradientType=0 );
            background: linear-gradient(top, #c72a2a 0% ,#9e0e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
        }
        .orange
        {
            border: solid 1px #720000;
            background-color: #FF9900;
            background: -moz-linear-gradient(top, #FF9900 0%, #FF9900 100%);
            background: -webkit-linear-gradient(top, #FF9900 0%, #FF9900 100%);
            background: -o-linear-gradient(top, #FF9900 0%, #FF9900 100%);
            background: -ms-linear-gradient(top, #FF9900 0% ,#FF9900 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e0e0e', endColorstr='#9e0e0e',GradientType=0 );
            background: linear-gradient(top, #FF9900 0% ,#9e0e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
        }
        
        .blue
        {
            border: solid 1px #720000;
            background-color: #9900FF;
            background: -moz-linear-gradient(top, #9900FF 0%, #9900FF 100%);
            background: -webkit-linear-gradient(top, #9900FF 0%, #9900FF 100%);
            background: -o-linear-gradient(top, #9900FF 0%, #9900FF 100%);
            background: -ms-linear-gradient(top, #9900FF 0% ,#9900FF 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e0e0e', endColorstr='#9e0e0e',GradientType=0 );
            background: linear-gradient(top, #9900FF 0% ,#9e0e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
        }
        
        .pink
        {
            border: solid 1px Black;
            background-color: #CC3399;
            background: -moz-linear-gradient(top, #CC3399 0%, #CC3399 100%);
            background: -webkit-linear-gradient(top, #CC3399 0%, #CC3399 100%);
            background: -o-linear-gradient(top, #CC3399 0%, #CC3399 100%);
            background: -ms-linear-gradient(top, #CC3399 0% ,#CC3399 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#9e0e0e', endColorstr='#9e0e0e',GradientType=0 );
            background: linear-gradient(top, #CC3399 0% ,#9e0e0e 100%);
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
        }
        .myListBox
        {
            border-style: none;
            border-width: 0px;
            border: none;
            font-size: 12px;
            font-family: Verdana;
            height: 300px;
            width: 300px;
        }
    </style>
</head>
<body>
  <script type="text/javascript">
      google.load("visualization", "1", { packages: ["corechart"] });

      google.setOnLoadCallback(drawChart);
      function drawChart(r) {

          var options = {
              
          };
          //    var chartType = $("#rblChartType input:checked").val();


          //      var tb = $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(0);
      var sid = 'Pie';
          if (r == "2") {
              options.is3D = true;
              sid = 'Pie3D';
          }

          //Doughnut Chart
          if (r == "3") {
              options.pieHole = 0.5;
              sid = 'Doughnut'
          }

          $.ajax({
              type: 'POST',
              dataType: 'json',
              contentType: 'application/json',
              url: 'TreeView.aspx/GetData',
              data: '{}',
              success:
                   function (response) {
                       drawVisualization(response, options);

                   }

          });
          function drawVisualization(dataValues, opt) {
              var data = new google.visualization.DataTable();
              data.addColumn('string', 'Column Name');
              data.addColumn('number', 'Column Value');

              for (var i = 0; i < dataValues.d.length; i++) {
                  data.addRow([dataValues.d[i].ColumnName, dataValues.d[i].Value]);
              }

              new google.visualization.PieChart(document.getElementById(sid)).
                draw(data, opt);
          }
      }
      $(document).on('click', function (e) {      
          if (!e) e = event;
          elm = (e.target) ? e.target : e.srcElement;
          elm1 = $('.ajax__tab_active');
          elm2 = $(elm1).find('.ajax__tab_tab');

          if (elm2[0].id == elm.id || elm2[0].id == elm.parentNode.id) {
              drawChart(elm2[0].id.replace(/__tab_TabContainer1_TabPanel/g, ''));
          }
      });
    </script>
    <form id="form1" runat="server">
    <div>
        <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxtoolkit:ToolkitScriptManager>
        <center>
            <img src="../../Images/dash.jpg" />
        </center>
        <asp:Panel ID="pnlback" runat="server" HorizontalAlign="Right" Width="97%">
            <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" PostBackUrl="~/BasicMaster.aspx"
                runat="server" />
        </asp:Panel>
        <br />
        <center>
            <%--<a href="#" class="button shape-1 green effect-4">SFE KPI</a>  --%>
            <asp:Button ID="btnmaster" CssClass="button shape-2 red effect-5" Text="Masters KPI"
                runat="server" OnClick="btnmaster_Click" />
            <asp:Button ID="btnsfe" CssClass="button shape-2 green effect-5" Text="SFE KPI" runat="server"
                OnClick="btnsfe_OnClick" />
            <asp:Button ID="btnmar" class="button shape-2 orange effect-5" Text="Marketing KPI"
                runat="server" />
            <asp:Button ID="btnsale" CssClass="button shape-2 blue effect-5" Text="Sales KPI"
                runat="server" />
            <asp:Button ID="btntrain" class="button shape-2 pink effect-5" Text="Training KPI"
                runat="server" />
        </center>
        <br />
        <center>
            <asp:Panel ID="pnldash" runat="server" Width="100%" Height="500px" BorderWidth="1">
                <center>
                    <asp:Label ID="lblSelect" runat="server" Text="Please Select KPI" Visible="false"></asp:Label>
                </center>
                <asp:Panel ID="pnltree" runat="server" Width="100%" Height="500px" BorderWidth="1"
                    BackColor="White">
                    <table id="gridtable" border="1" class="gridtable" cellspacing="0" cellpadding="8">
                        <tr>
                            <th style="color: White">
                                Monthly Analysis
                            </th>
                            <th style="color: White">
                                Mode
                            </th>
                            <th style="color: White">
                                Graph
                            </th>
                        </tr>
                        <tr>
                            <td valign="top" style="borderwidth: 1">
                                <asp:Panel ID="pnltreeview" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="overflow: auto; width: 300px; height: 350px;">
                                                    <asp:TreeView ID="trvuser" BorderStyle="None" Font-Bold="false" ImageSet="Contacts"
                                                        Font-Names="Verdana" runat="server" OnSelectedNodeChanged="trvuser_SelectedNodeChanged">
                                                    </asp:TreeView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="vertical-align: top; width: 30%">
                                <asp:Panel ID="pnldoc" runat="server">
                                    <table id="tbldoc">
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lstmas" runat="server" CssClass="myListBox" AutoPostBack="true"
                                                    OnSelectedIndexChanged="lstmas_OnSelectedIndexChanged">
                                                    <asp:ListItem Value="1">- Customers Category</asp:ListItem>
                                                    <asp:ListItem Value="2">- Customers Speciality</asp:ListItem>
                                                    <asp:ListItem Value="3">- Customers Class</asp:ListItem>
                                                    <asp:ListItem Value="4">- Customers Qualification</asp:ListItem>
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnldoc1" runat="server" Visible="false">
                                    <table id="Table1">
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lstkpi" runat="server">
                                                    <asp:ListItem Value="1">- Customer Coverage</asp:ListItem>
                                                    <asp:ListItem Value="2">- Call Average</asp:ListItem>
                                                    <asp:ListItem Value="3">- Manager Days in Field</asp:ListItem>
                                                    <asp:ListItem Value="4">- Manager MSL Coverage</asp:ListItem>
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td width="100%" style="border-style: solid; border-width: 1px">
                                <table width="90%" align="center" style="border-width: 1px;">
                                    <tr>
                                        <td width="90%">
                   
                                            <asp:Panel ID="pnlgraph" runat="server">
                                                <div style="width: 40%">
                                                    <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" TabIndex="0" Width="700px"
                                                        CssClass="fancy fancy-green">
                                                        <ajaxtoolkit:TabPanel HeaderText="Pie Chart" runat="server" ID="TabPanel1" TabIndex="1">
                                                            <ContentTemplate>
                                                                <asp:Label ID="Lblsf_name" runat="server"></asp:Label>
                                                                <div id="Pie" style="width: 600px; height: 350px;">
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxtoolkit:TabPanel>
                                                        <ajaxtoolkit:TabPanel HeaderText="Pie Chart - 3D" runat="server" ID="TabPanel2" TabIndex="2">
                                                            <ContentTemplate>
                                                                     <div id="Pie3D" style="width: 600px; height: 350px;"></div>
                                                            </ContentTemplate>
                                                        </ajaxtoolkit:TabPanel>
                                                        <ajaxtoolkit:TabPanel HeaderText="Doughnut" runat="server" ID="TabPanel3" TabIndex="3">
                                                            <ContentTemplate>
                                                
                                                                     <div id="Doughnut" style="width: 600px; height: 350px;"></div>
                                                            </ContentTemplate>
                                                        </ajaxtoolkit:TabPanel>
                                                        <ajaxtoolkit:TabPanel HeaderText="Bar Chart" runat="server" ID="TabPanel4">
                                                            <ContentTemplate>
                                                              
                                                            </ContentTemplate>
                                                        </ajaxtoolkit:TabPanel>
                                                        <ajaxtoolkit:TabPanel HeaderText="Stacked Chart" runat="server" ID="TabPanel5">
                                                            <ContentTemplate>
                                                                <div>
                                                                    <asp:Literal ID="lt" runat="server"></asp:Literal>
                                                                </div>
                                                                <div id="chart_div">
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxtoolkit:TabPanel>
                                                        <ajaxtoolkit:TabPanel HeaderText="Line Chart" runat="server" ID="TabPanel6">
                                                            <ContentTemplate>
                                                                Line Chart
                                                            </ContentTemplate>
                                                        </ajaxtoolkit:TabPanel>
                                                   
                                                     
                                                    </ajaxtoolkit:TabContainer>
                                                </div>
                                            </asp:Panel>
                                            <center>
                                             <%--   <asp:Label ID="lblSelect1" Text="Please Select KPI" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="Medium" ForeColor="Red" runat="server"></asp:Label>--%>
                                            </center>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
