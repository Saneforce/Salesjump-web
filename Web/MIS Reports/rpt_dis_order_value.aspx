<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_dis_order_value.aspx.cs" Inherits="MIS_Reports_rpt_dis_order_value" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
    <title>Secondary Order</title>
<link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function popUp(Sf_Code, Stockist_Code, Year, Sf_name, Stockist_Name) {
            
           
            strOpen = "rpt_dis_orderRUT_value.aspx?SF_Code=" + Sf_Code + "&Year=" + Year + "&Stockist_Code=" + Stockist_Code + "&Sf_name=" + Sf_name + "&Stockist_Name=" + Stockist_Name
         window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

     }
       
        $(document).ready(function () {
            $('#<%=GVData.ClientID%> tr').each(function () {
                tds = $(this).find("td");
             //   console.log($.trim($(tds[1]).text()).toString() + ":" + $.trim($('#<%=hd_sfcode.ClientID%>').val()).toString());
                if ($.trim($(tds[1]).text()).toString() == $.trim($('#<%=hd_sfcode.ClientID%>').val()).toString()) {
                    if ($.trim($(tds[1]).text()).toString() == "admin") {
                        $(this).hide();
                    }
                    else {
                        $(tds[2]).html($(tds[2]).text());
                    }

                }
            });
            $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divExcel').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'TargetAchievementReport xls';
                a.click();
                e.preventDefault();

            });
        });
    </script>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 10px 10px;
        }
        #GVData tr:last-child
        {
            padding: 3px 8px;
            background-color: #496a9a;
            color: #fff;
            border: 1px solid #bbb;
            font-weight: bold;
        }
        .gvHeader th
        {
            padding: 3px 8px;
            background-color: #496a9a;
            color: #fff;
            border: 1px solid #bbb;
            text-align:center;
        }
        .gvRow td
        {
            padding: 3px 8px;
            border: 1px solid #bbb;
            text-align: right;
        }
        .gvAltRow td
        {
            padding: 3px 8px;
            border: 1px solid #bbb;
            text-align: right;
        }
        
      .gvHeader th:first-child
        {
            display: none;
        }
        .gvRow td:first-child
        {
            display: none;
        }
        .gvAltRow td:first-child
        {
            display: none;
        }
        .gvRow tr:last-child a
        {
            color:#fff;          
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
    <div style="margin: 0 auto; width: 90%">
		<br />
                 <div class="row" style="width: 100%">
 	<div class="col-sm-8">        
                                    <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold;
                    font-size: x-large"></asp:Label>                               
                            </div>
            <div class="col-sm-4" style="text-align: right">	
       <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;display:none" class="btn btnPrint" OnClick="btnPrint_Click"/>
       <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;display:none" class="btn btnPdf"  OnClick="btnExport_Click" />
       <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" />
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                            
                      </div>
        </div>
         
    </div>
    <center>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div id="divExcel">
            <div style="text-align: center; font-size:medium; display: none">
               SECONDARY ORDER  <asp:Label ID="lblhead" runat="server"></asp:Label>            
            </div>           
                <div style="text-align: center; font-size: medium">
                     
                        <asp:Label ID="lblsf" runat="server"></asp:Label>
                     
                        <asp:HiddenField ID="hd_sfcode" runat="server" />
                        <asp:HiddenField ID="hdSub_Div" runat="server" />
                    </b>
                </div>
                <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="false" 
                    HeaderStyle-CssClass="gvHeader" CssClass="gvRow" AlternatingRowStyle-CssClass="gvAltRow">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <th style="display: none">
                                    Stockist Code
                                </th>
                                <th style="min-width: 200px;">
                                   Stockist Name
                                </th>
                                <th style="min-width: 200px;">
                                   Head Quarters
                                </th>
                                <th >
                                    JAN
                                </th>
                                <th >
                                    FEB
                                </th>
                                <th>
                                    MAR
                                </th>
                                <th >
                                    APR
                                </th>
                                <th >
                                    MAY
                                </th>
                                <th >
                                    JUN
                                </th>
                                <th >
                                    JUL
                                </th>
                                <th >
                                    AUG
                                </th>
                                <th >
                                    SEP
                                </th>
                                <th>
                                    OCT
                                </th>
                                <th>
                                    NOV
                                </th>
                                <th >
                                    DEC
                                </th>
                                <th >
                                     TOTAL
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                               
                                <td style="text-align: left;">
                                    <a href="javascript:popUp('<%=sf_code %>',<%# Eval("Stockist_Code") %>,'<%=Year%>','<%=SF_Name%>','<%# Eval("Stockist_Name") %>')">
                                        <%# Eval("Stockist_Name") %>
                                    </a>
                                </td>
                                <td style="text-align: left;">
                                  
                                        <%# Eval("Territory") %>
                                    
                                </td>
                               
                               
                                <td>
                                    <%# Eval("JAN_OV")%>
                                </td>                              
                                
                                <td>
                                    <%# Eval("FEB_OV")%>
                                </td>
                               
                                <td>
                                    <%# Eval("MAR_OV")%>
                                </td>
                                
                                <td>
                                    <%# Eval("APR_OV")%>
                                </td>
                               
                                <td>
                                    <%# Eval("MAY_OV")%>
                                </td>
                               
                                <td>
                                    <%# Eval("JUN_OV")%>
                                </td>
                               
                                <td>
                                    <%# Eval("JUL_OV")%>
                                </td>
                               
                                <td>
                                    <%# Eval("AUG_OV")%>
                                </td>
                              
                                <td>
                                    <%# Eval("SEP_OV")%>
                                </td>
                                
                                <td>
                                    <%# Eval("OCT_OV")%>
                                </td>
                               
                                <td>
                                    <%# Eval("NOV_OV")%>
                                </td>
                                
                                <td>
                                    <%# Eval("DEC_OV")%>
                                </td>

                                
                                <td>
                                    <%# Eval("TOT_OV")%>
                                </td>
                              
                              
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </center>
    <br />
    </form>
</body>
</html>
