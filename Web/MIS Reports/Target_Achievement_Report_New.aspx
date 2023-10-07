<%@ Page Title="Target Achievement Report" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Target_Achievement_Report_New.aspx.cs" Inherits="MIS_Reports_Target_Achievement_Report_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
        .button
        {
            display: inline-block;
            border-radius: 4px;
            background-color: #6495ED;
            border: none;
            color: #FFFFFF;
            text-align: center;
            font-bold: true;
            width: 75px;
            height: 29px;
            transition: all 0.5s;
            cursor: pointer;
            margin: 5px;
        }
        
        .button span
        {
            cursor: pointer;
            display: inline-block;
            position: relative;
            transition: 0.5s;
        }
        
        .button span:after
        {
            content: '»';
            position: absolute;
            opacity: 0;
            top: 0;
            right: -20px;
            transition: 0.5s;
        }
        
        .button:hover span
        {
            padding-right: 25px;
        }
        
        .button:hover span:after
        {
            opacity: 1;
            right: 0;
        }
        
        
        .gvHeader th
        {
            padding: 3px 8px;
            background-color: #496a9a;
            color: #fff;
            border: 1px solid #bbb;
        }
        .gvRow td
        {
            padding: 3px 8px;
            background-color: #ffffff;
            border: 1px solid #bbb;
            text-align: right;
        }
        .gvAltRow td
        {
            padding: 3px 8px;
            background-color: #f1f1f1;
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
		.col-sm-6
        {
                padding: 0px 3px 6px 4px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });

        $(document).ready(function () {
            $(document).on('click', 'a', function () {
                if (Number($(this).text()) <= 0) {
                    return false;
                }
            });
        });
        function popUp(Stockist_Code, year, sf_name) {
            var sub_div_code = $('#<%=subdiv.ClientID%>').val();
            strOpen = "rpt_target_order.aspx?SF_Code=" + Stockist_Code + "&Year=" + year + "&SF_Name=" + sf_name + "&Sub_Div=" + sub_div_code
            window.open(strOpen, 'popmgr', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
        }

    </script>
    <form id="form1" runat="server">
      <div class="container" style="width:100%">      
                 
                  <div class="row">

                  <label id="Label2" class="col-md-1  col-md-offset-4  control-label">
                Division</label>
               <%-- <asp:Label ID="Label2" runat="server"  Text="Division" CssClass="col-sm-1 col-sm-offset-4 control-label"></asp:Label>--%>
                <div class="col-sm-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server"  CssClass="form-control"
                            Width="150" >
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
                 <div class="row" >                 
                             <%-- <asp:Label ID="Label1" runat="server"  Text="Year " CssClass="col-sm-1 col-sm-offset-4 control-label"></asp:Label>--%>
                               <label id="Label1" class="col-md-1  col-md-offset-4  control-label">
                Year</label>

                <div class="col-sm-6 inputGroupContainer">
                    <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                     <asp:DropDownList ID="ddlFYear" runat="server"  CssClass="form-control" 
                            Width="100">
                        </asp:DropDownList>
                        </div>
                </div>
                
                </div>          
                <div class="row">
                
                <div class="col-sm-11" style="text-align:center">
                            <button id="btnGo" class="btn btn-primary" runat="server" onserverclick="btnGo_Click" style="vertical-align: middle;">
                                     <span>View</span></button></div>              
                </div>
                
              
      
        </div>
    <br />
    <br />
    <div style="height: 100px;">
        <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="false" GridLines="None"
            HeaderStyle-CssClass="gvHeader" CssClass="gvRow">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
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
                            TOTAL
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
                        <td style="text-align: left;">                          
                            <asp:LinkButton ID="lblSubDiv_count" runat="server" CausesValidation="False" Text='<%# Eval("Name") %>'
                                OnClientClick='<%# "return popUp(\"" + Eval("SF_CODE") + "\",\"" + Eval("Year") + "\",\"" + Eval("Name") + "\");"%>'>
                            </asp:LinkButton>
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
    </form>
</asp:Content>
