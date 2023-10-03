<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_emp_order_valueSTKwise.aspx.cs" Inherits="MIS_Reports_rpt_emp_order_valueSTKwise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Report</title>
<link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        function popUp(sf_code, Stockist_Code, Year, Sf_name, Stockist_Name) {
            var sub_div_code = $('#<%=hdSub_Div.ClientID%>').val();
         console.log(sub_div_code);
           
            strOpen = "rpt_emp_order_valueDAYwise.aspx?SF_Code=" + sf_code + "&Year=" + Year + "&Stockist_Code=" + Stockist_Code + "&Sf_name=" + Sf_name + "&Stockist_Name=" + Stockist_Name
         window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

     }
 function popUp123(Sf_Code, Year, name, CDate, sf_type, tmonth) {
            var sURL = "rpt_Total_Order_View.aspx?sf_code=" + Sf_Code + "&cur_month=" + tmonth + "&cur_year=" + Year +
                "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + name + "&Date=" + CDate + "&Type=" + sf_type;
            window.open(sURL, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
        }
        function popUp1(Sf_Code, Year, name, sub) {
           
                strOpen = "rpt_dis_order_value.aspx?SF_Code=" + Sf_Code + "&Year=" + Year + "&SF_Name=" + name
                window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
            }
       
        $(document).ready(function () {
            $('#<%=GVData.ClientID%> tr').each(function () {
                tds = $(this).find("td");
             //   console.log($.trim($(tds[1]).text()).toString() + ":" + $.trim($('#<%=hd_sfcode.ClientID%>').val()).toString());
                //if ($.trim($(tds[1]).text()).toString() == $.trim($('#<%=hd_sfcode.ClientID%>').val()).toString()) {
                if ($.trim($(tds[2]).text()).toString() == "Total") {
                    $(this).addClass("background-color");
                    }                 
                //}
            });
            $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divExcel').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'SecondaryOrderReport xls';
                a.click();
                e.preventDefault();

            });
        });
    </script>
    <style type="text/css">
        .background-color
        {
            padding: 3px 8px;
            background-color: #496a9a;
            color: #fff;
            border: 1px solid #bbb;
            text-align:center;
        }
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
                <b><asp:Label ID="lblhead" runat="server"></asp:Label>            
            </div>           
                <div style="text-align: left; font-size: large">
                    Taken By : <b>
                        <asp:Label ID="lblsf" runat="server"></asp:Label>
<a href="javascript:popUp1('<%=sf_code%>','<%=Year%>','<%=SF_Name%>',2	)" >
                            Distributer list</a>
  <a href="javascript:popUp123('<%=sf_code%>','<%=Year%>','<%=SF_Name%>','<%=CDate%>','<%=sf_type%>','<%=tmonth%>')" >
                            Day Wise Secondary Order</a>
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
                                    Salesforce_Code
                                </th>
                                 <th  rowspan="2" style="min-width: 200px;">
                                    Salesforce_Name
                                </th>
                                <th style="display: none">
                                    Stockist_Code
                                </th>
     <th style="display: none">
                                    Stockist_Code1
                                </th>
                                <th rowspan="2" style="min-width: 200px;">
                                     Distributer Name
                                </th>
                                  <th style="display: none">
                                    Sfcode for cnt
                                </th>
                                <th rowspan="2"  >
                                  No of Outlet  
                                </th>
                                <th colspan="4">
                                    JAN
                                </th>
                                <th colspan="4">
                                    FEB
                                </th>
                                <th colspan="4">
                                    MAR
                                </th>
                                <th colspan="4">
                                    APR
                                </th>
                                <th colspan="4">
                                    MAY
                                </th>
                                <th colspan="4">
                                    JUN
                                </th>
                                <th colspan="4">
                                    JUL
                                </th>
                                <th colspan="4">
                                    AUG
                                </th>
                                <th colspan="4">
                                    SEP
                                </th>
                                <th colspan="4">
                                    OCT
                                </th>
                                <th colspan="4">
                                    NOV
                                </th>
                                <th colspan="4">
                                    DEC
                                </th>
                                <th colspan="4">
                                    TOTAL
                                </th>
                                <tr class="gvHeader">
                                   
                                    <th></th>
                                    <th>
                                          No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                    <th>
                                          No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                   
                                    <th>
                                         No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                    
                                    <th>
                                          No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                    
                                    <th>
                                         No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                   
                                    <th>
                                        No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                    
                                    <th>
                                         No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                   
                                    <th>
                                          No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                    
                                    <th>
                                          No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                    
                                    <th>
                                         No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                   
                                    <th>
                                         No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                    
                                    <th>
                                          No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                    <th>
                                          No of Attempt
                                    </th>
                                    <th>
                                        Order Val
                                    </th>
                                    <th>
                                        Tax Val
                                    </th>
                                    <th>
                                        Net Val
                                    </th>
                                   
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                   <td style="text-align: left; display: none">
                                    <%# Eval("sf_code")%>
                                </td>
                                   <td style="text-align: left;">
                                    <%# Eval("Sf_Name")%>
                                </td>
                                <td style="text-align: left; display: none">
                                    <%# Eval("Stockist_Code")%>
                                </td>
 								<td style="text-align: left; display: none">
                                    <%# Eval("Stockist_Code1")%>
                                </td>
                                <td style="text-align: left;">
                                    <a href="javascript:popUp('<%# Eval("sf_code")%>','<%# Eval("Stockist_Code") %>','<%=Year%>','<%# Eval("Sf_Name")%>','<%# Eval("Stockist_Name") %>')">
                                        <%# Eval("Stockist_Name") %></a>
                                </td>
                                <td style="text-align: left; display: none">
                                    <%# Eval("Sf_Code1")%>
                                </td>

                                 <td style="text-align: left;" >
                                    <%# Eval("cnt")%>
                                </td>
                                <td>
                                    <%# Eval("JAN_TV")%>
                                </td>
                                <td>
                                    <%# Eval("JAN_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("JAN_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("JAN_OV", "{0:0.00}")%>
                                </td>
                                
                                <td>
                                    <%# Eval("FEB_TV")%>
                                </td>
                                <td>
                                    <%# Eval("FEB_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("FEB_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("FEB_OV", "{0:0.00}")%>
                                </td>
                               
                                <td>
                                    <%# Eval("MAR_TV")%>
                                </td>
                                <td>
                                    <%# Eval("MAR_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("MAR_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("MAR_OV", "{0:0.00}")%>
                                </td>
                                
                                <td>
                                    <%# Eval("APR_TV")%>
                                </td>
                                <td>
                                    <%# Eval("APR_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("APR_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("APR_OV", "{0:0.00}")%>
                                </td>
                               
                                <td>
                                    <%# Eval("MAY_TV")%>
                                </td>
                                <td>
                                    <%# Eval("MAY_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("MAY_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("MAY_OV", "{0:0.00}")%>
                                </td>
                               
                                <td>
                                    <%# Eval("JUN_TV")%>
                                </td>
                                <td>
                                    <%# Eval("JUN_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("JUN_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("JUN_OV", "{0:0.00}")%>
                                </td>
                                
                                <td>
                                    <%# Eval("JUL_TV")%>
                                </td>
                                <td>
                                    <%# Eval("JUL_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("JUL_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("JUL_OV", "{0:0.00}")%>
                                </td>
                               
                                <td>
                                    <%# Eval("AUG_TV")%>
                                </td>
                                <td>
                                    <%# Eval("AUG_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("AUG_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("AUG_OV", "{0:0.00}")%>
                                </td>
                               
                                <td>
                                    <%# Eval("SEP_TV")%>
                                </td>
                                <td>
                                    <%# Eval("SEP_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("SEP_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("SEP_OV", "{0:0.00}")%>
                                </td>
                                
                                <td>
                                    <%# Eval("OCT_TV")%>
                                </td>
                                <td>
                                    <%# Eval("OCT_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("OCT_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("OCT_OV", "{0:0.00}")%>
                                </td>
                                
                                <td>
                                    <%# Eval("NOV_TV")%>
                                </td>
                                <td>
                                    <%# Eval("NOV_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("NOV_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("NOV_OV", "{0:0.00}")%>
                                </td>
                               
                                <td>
                                    <%# Eval("DEC_TV")%>
                                </td>
                                <td>
                                    <%# Eval("DEC_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("DEC_TOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("DEC_OV", "{0:0.00}")%>
                                </td>
                               
                                <td>
                                    <%# Eval("TOT_TV")%>
                                </td>
                                <td>
                                    <%# Eval("TOT_wTOV", "{0:0.00}")%>
                                </td>
                                <td>
                                    <%# Eval("TOT_TOV", "{0:0.00}")%>
                                </td>
                                
                                <td>
                                    <%# Eval("TOT_OV", "{0:0.00}")%>
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
