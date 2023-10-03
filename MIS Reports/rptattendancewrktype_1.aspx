<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptattendancewrktype_1.aspx.cs" Inherits="MIS_Reports_rptattendancewrktype_1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attendance View</title>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
     <script language="Javascript">
         function RefreshParent() {
             // window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var nStr = '';
            var nStr1 = '';
            var rshq = [];
            var dRSF = [];
            var dDist = [];
            var dDist_no = [];
            var Dtls = [];
            var CatVal = [];
            var catH = [];
            var lvl = 0;

            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            var SubDivCode = $("#<%=SubDivCode.ClientID%>").val();
            var str = '';
            function genReport() {
                if (rshq.length) {
                    var balncesum = 'added';

                    nStr1 = '<td>' + balncesum  + '</td>';
                    //            $('#gvtotalorder tr td:first').eq(2).hide();
                    //            $('#gvtotalorder tr td:first').eq(2).append(nStr);
                    $('#gvtotalorder th:first').before(str);
                    $('[id*=gvtotalorder]').find('tr').each(function (i) {

                        var sf_name = $(this).find("td:eq(1)").text();
                        //                var sf_code = $(this).find("td:eq(0) :input").val();
                        var rpsf = $(this).find("td:eq(5)").text();
                        if (rpsf == 'admin') {
                            for (var u = 0; u < lvl; u++) {
                                nStr = nStr + '<td></td>';
                            }
                        }
                        vp = rshq.filter(function (a) { return (a.sfCode == rpsf); });

                        if (vp.length > 0) {
                            var ssF = vp[0].sfCode;
                            var llV = Number(vp[0].level || 0);
                            var kkk = llV;
                            var ssRf = vp[0].RSFCode;
                            var nStr = '';
                            while (Number(llV) != Number(0)) {
                                nStr = '<td>' + vp[0].sfName + '</td>' + nStr;
                                vp = rshq.filter(function (a) { return (a.sfCode == ssRf); });

                                if (vp.length > 0) {
                                    ssF = vp[0].sfCode;
                                    llV = vp[0].level || 0;
                                    ssRf = vp[0].RSFCode;
                                }
                                else {
                                    llV = 0;
                                }

                            }
                            for (var u = kkk; u < lvl; u++) {
                                nStr = nStr + '<td></td>';
                            }


                        }
                        $(this).prepend(nStr);

                        //                        var balncesum = 'added';

                        //                        nStr = '<td>' + balncesum + '</td>';


                    });
                }
            };
            var ReasonArray = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptNew_Outlet_Sales_RepView.aspx/GetDataD",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "'}",
                dataType: "json",
                success: function (data) {
                     console.log(data.d);
                    dRSF = data.d;
                    //                    strh += "<th>Reporting Manager</th><th>SR Erp Id</th><th> User</th><th>user Rank</th><th>SR HQ/Area</th><th>Beats </th><th> Sf_Name</th> <th> ListedDr_Name </th> <th>ListedDr_Mobile</th><th>ListedDr_Email</th><th>GST</th><th>Address</th><th>cityname</th><th>PIN_Code</th><th>StateName</th><th>Remarks</th><th>Order_Value</th>";
                    //                    $('#ProductTable >thead').append(' <tr>' + strh + ' </tr>');

                },
                error: function (jqXHR, exception) {
                    alert(JSON.stringify(result));
                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                }
            });

            var leng = 0;
            if (dRSF.length > 0) {
                leng = dRSF.length;

                Rf = dRSF.filter(function (a) { return (a.RSF_Code == 'admin'); });
                if (Rf.length > 0) {
                    var Rf1;
                    var strk = '';
                    leng = leng - Rf.length;

                    for (var l = 0; l < Rf.length; l++) {
                        rshq.push({ sfCode: Rf[l].Sf_Code, RSFCode: Rf[l].RSF_Code, Desig: Rf[l].Designation, sfName: Rf[l].Sf_Name, level: '1' });
                        lvl = lvl > 1 ? lvl : 1;
                        Rf1 = dRSF.filter(function (a) { return (a.RSF_Code == Rf[l].Sf_Code); });
                        if (Rf1.length > 0) {
                            for (var k = 0; k < Rf1.length; k++) {
                                rshq.push({ sfCode: Rf1[k].Sf_Code, RSFCode: Rf1[k].RSF_Code, Desig: Rf1[k].Designation, sfName: Rf1[k].Sf_Name, level: '2' });
                                lvl = lvl > 2 ? lvl : 2;
                                Rf2 = dRSF.filter(function (a) { return (a.RSF_Code == Rf1[k].Sf_Code); });

                                if (Rf2.length > 0) {
                                    for (var c = 0; c < Rf2.length; c++) {
                                        rshq.push({ sfCode: Rf2[c].Sf_Code, RSFCode: Rf2[c].RSF_Code, Desig: Rf2[c].Designation, sfName: Rf2[c].Sf_Name, level: '3' });
                                        lvl = lvl > 3 ? lvl : 3;
                                        Rf3 = dRSF.filter(function (a) { return (a.RSF_Code == Rf2[c].Sf_Code); });

                                        if (Rf3.length > 0) {
                                            for (var m = 0; m < Rf3.length; m++) {
                                                rshq.push({ sfCode: Rf3[m].Sf_Code, RSFCode: Rf3[m].RSF_Code, Desig: Rf3[m].Designation, sfName: Rf3[m].Sf_Name, level: '4' });
                                                lvl = lvl > 4 ? lvl : 4;

                                                Rf4 = dRSF.filter(function (a) { return (a.RSF_Code == Rf3[m].Sf_Code); });
                                                if (Rf4.length > 0) {
                                                    for (var n = 0; n < Rf4.length; n++) {
                                                        rshq.push({ sfCode: Rf4[n].Sf_Code, RSFCode: Rf4[n].RSF_Code, Desig: Rf4[n].Designation, sfName: Rf4[n].Sf_Name, level: '5' });
                                                        lvl = lvl > 5 ? lvl : 5;
                                                        //Rf4 = dRSF.filter(function (a) { return (a.RSF_Code == Rf3[c].Sf_Code); });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                console.log(rshq);
            }
            for (jk = 0; jk < lvl; jk++) {

                if (jk > 0) {
                    str += '<th style="min-width:250px;">Level-' + jk + '</th>';
                }
                else {
                    str += '<th style="min-width:250px;">Top Level</th>';
                }
            }
            genReport();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //            genReport = function () {

				
            $('#btnExcel').on('click', function () {
                var htmls = "";
                var uri = 'data:application/vnd.ms-excel;base64,';
                var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
                var base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                };
                var format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) {
                        return c[p];
                    })
                };
                htmls = document.getElementById("gvtotalorder").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = "Attendance Worktypewise <%=strFMonthName%> - <%=FYear%>"+".xls";

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
                event.preventDefault();
            })

        });
    </script>
    
    <style type="text/css">
       .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }

        .ttable tr:nth-child(odd) {
            background-color: #dbe2f9;
        }

        .ttable td {
            padding: 5px 2px;
            width: 14px;
            text-align: justify;
            border: solid 1px black;
        }

        .ttable th {
            padding: 4px 2px;
            color: #fff;
            background: #819DFB url(Images/grid-header.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 0.9em;
        }


        .ttable table {
            overflow: hidden;
        }

        .ttable tr:hover {
            background-color: #ffa;
        }

        .ttable td, th {
            position: relative;
        }

            .ttable td:hover::after,
            .ttable th:hover::after {
                content: "";
                background-color: #ffa;
                left: 0;
                top: -5000px;
                height: 10000px;
                width: 100%;
                z-index: -1;
            }

        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
        }

            #grid th {
                position: sticky;
                top: 0;
                background: #6c7ae0;
                text-align: center;
                font-weight: normal;
                font-size: 15px;
                color: white;
            }


            #grid td, #grid th {
                padding: 5px;
                border: 1px solid #ddd;
            }
    </style>
</head>
    <body>
    <form id="form1" runat="server">
     <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="ddlFYear" runat="server" />
    <asp:HiddenField ID="ddlFMonth" runat="server" />
    <asp:HiddenField ID="SubDivCode" runat="server" />
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
<tr>&nbsp</tr>
                <tr>
               <td width="100%" align="center" >
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <button id="btnExcel" type="button" style="border-color:black;border-style:solid;border-width:1px;height:25px;width:60px;font-size:10px;font-family:Verdana;" >Excel</button>
                                </td>
                         
                               
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
 
       
        
       
        <div>
                <table width="100%" align="center">
                <tr>
                <td colspan="4" >
                <asp:Label ID="lblHead"  SkinID="lblMand" style="font-weight:bold;FONT-SIZE: 16pt;COLOR: black;FONT-FAMILY:Times New Roman;float: left;padding: 5px;" 
                runat="server"></asp:Label>
                    </td></tr>
                    <tr>
                                    
                       
                        <td align="left">
                            <asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true"  Font-Underline="true" ForeColor="#476eec" runat="server" ></asp:Label>
                            <asp:Label ID="lblsf_name" runat="server"   Font-Underline="true" SkinID="lblMand"></asp:Label>
                        </td>
                       
                        <td align="right">
                 
                <asp:Image ID="logoo" runat="server" style="width: 28%;border-width:0px;height: 39px;"></asp:Image>
         
            </td>
                    </tr>
                </table>
          
         
        <div align="center">
       
                        <asp:GridView ID="gvtotalorder" runat="server" Width="98%"    Font-Size=9pt OnPreRender="gridView_PreRender"  CssClass="ttable"
                                HorizontalAlign="Center"  HeaderStyle-BackColor="#819dfb"
                                BorderWidth="1px" CellPadding="2" CellSpacing="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true"  OnRowDataBound="GridView1_RowDataBound"
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>
                            
                                   
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
							 <div>
                        <br />
                        <br />
                        <h3 style="color: #d02c64;">Work Type Name</h3><br />
                        <table class="Numbers" id="grid" cellpadding="0" cellspacing="0">
                            <tr></tr>
                        </table>
                    </div>
                            </div>
                       
            </div>      
    </asp:Panel>
    </form>
   <script type="text/javascript">
        $(document).ready(function () {
            gethint();
        });
        function gethint() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptattendancewrktype_1.aspx/gethints",
                data: '{"divcode":"<%=Session["div_code"]%>"}',
              dataType: "json",
              success: function (data) {
                  hint = JSON.parse(data.d) || [];
                  str = '<tr>';

                  for (var i = 0; i < hint.length; i++) {
                      str += "<td style='color:#162dd3;font-weight: 900;'>" + hint[i].WType_SName + "</td><td  style='color:#cb16a4;font-weight: 700;'>" + hint[i].Wtype + "</td>";
                      if (((i + 1) % 3) == 0) {
                          str += "</tr>"
                          $(".Numbers").append(str); str = '<tr>';
                      } else {
                          continue;
                      }



                  }

              }
          });

        }
    </script>
</body>
</html>
