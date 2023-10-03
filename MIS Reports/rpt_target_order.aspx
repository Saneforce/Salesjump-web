﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_target_order.aspx.cs"
    EnableEventValidation="false" Inherits="MIS_Reports_rpt_target_order" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Target Achievement Report</title>
<link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        function popUp(Stockist_Code, year, sf_type, sf_name) {
            var sub_div_code = $('#<%=hdSub_Div.ClientID%>').val();
            console.log(sub_div_code);
            if (sf_type == 2) {
                strOpen = "rpt_target_order.aspx?SF_Code=" + Stockist_Code + "&Year=" + year + "&SF_Name=" + sf_name + "&Sub_Div=" + sub_div_code
                window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
            }

        }
        function RefreshParent() {
            //   window.opener.document.getElementById('form1').click();
            window.close();
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
            <div style="text-align: center; font-size: large; display: none">
                Target Achievement Report for Year of<b><asp:Label ID="lblhead" runat="server"></asp:Label>            
            </div>           
                <div style="text-align: left; font-size: large">
                    Taken By : <b>
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
                                    Sf_code
                                </th>
                                <th rowspan="2" style="min-width: 200px;">
                                    Name
                                </th>
                                <th colspan="3">
                                    JAN
                                </th>
                                <th colspan="3">
                                    FEB
                                </th>
                                <th colspan="3">
                                    MAR
                                </th>
                                <th colspan="3">
                                    APR
                                </th>
                                <th colspan="3">
                                    MAY
                                </th>
                                <th colspan="3">
                                    JUN
                                </th>
                                <th colspan="3">
                                    JUL
                                </th>
                                <th colspan="3">
                                    AUG
                                </th>
                                <th colspan="3">
                                    SEP
                                </th>
                                <th colspan="3">
                                    OCT
                                </th>
                                <th colspan="3">
                                    NOV
                                </th>
                                <th colspan="3">
                                    DEC
                                </th>
                                <th colspan="3">
                                    2018 - TOTAL
                                </th>
                                <tr class="gvHeader">
                                    <th>
                                    </th>                                    
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                    <th>
                                        Target Val
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Achieve
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td style="text-align: left; display: none">
                                    <%# Eval("SF_CODE")%>
                                </td>
                                <td style="text-align: left;">
                                    <a href="javascript:popUp('<%# Eval("SF_CODE") %>','<%# Eval("Year") %>',<%# Eval("sf_type") %>,'<%# Eval("Name") %>')">
                                        <%# Eval("Name") %></a>
                                </td>
                                <td>
                                    <%# Eval("JAN_TV")%>
                                </td>
                                <td>
                                    <%# Eval("JAN_OV")%>
                                </td>
                                <td>
                                    <%# Eval("JAN_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("FEB_TV")%>
                                </td>
                                <td>
                                    <%# Eval("FEB_OV")%>
                                </td>
                                <td>
                                    <%# Eval("FEB_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("MAR_TV")%>
                                </td>
                                <td>
                                    <%# Eval("MAR_OV")%>
                                </td>
                                <td>
                                    <%# Eval("MAR_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("APR_TV")%>
                                </td>
                                <td>
                                    <%# Eval("APR_OV")%>
                                </td>
                                <td>
                                    <%# Eval("APR_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("MAY_TV")%>
                                </td>
                                <td>
                                    <%# Eval("MAY_OV")%>
                                </td>
                                <td>
                                    <%# Eval("MAY_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("JUN_TV")%>
                                </td>
                                <td>
                                    <%# Eval("JUN_OV")%>
                                </td>
                                <td>
                                    <%# Eval("JUN_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("JUL_TV")%>
                                </td>
                                <td>
                                    <%# Eval("JUL_OV")%>
                                </td>
                                <td>
                                    <%# Eval("JUL_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("AUG_TV")%>
                                </td>
                                <td>
                                    <%# Eval("AUG_OV")%>
                                </td>
                                <td>
                                    <%# Eval("AUG_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("SEP_TV")%>
                                </td>
                                <td>
                                    <%# Eval("SEP_OV")%>
                                </td>
                                <td>
                                    <%# Eval("SEP_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("OCT_TV")%>
                                </td>
                                <td>
                                    <%# Eval("OCT_OV")%>
                                </td>
                                <td>
                                    <%# Eval("OCT_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("NOV_TV")%>
                                </td>
                                <td>
                                    <%# Eval("NOV_OV")%>
                                </td>
                                <td>
                                    <%# Eval("NOV_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("DEC_TV")%>
                                </td>
                                <td>
                                    <%# Eval("DEC_OV")%>
                                </td>
                                <td>
                                    <%# Eval("DEC_ACH")%>
                                </td>
                                <td>
                                    <%# Eval("TOT_TV")%>
                                </td>
                                <td>
                                    <%# Eval("TOT_OV")%>
                                </td>
                                <td>
                                    <%# Eval("TOT_ACH")%>
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
