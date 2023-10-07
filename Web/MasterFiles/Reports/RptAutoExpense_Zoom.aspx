<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptAutoExpense_Zoom.aspx.cs" Inherits="MasterFiles_MR_RptAutoExpense" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Statement</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
     <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
   
     <style type="text/css">
             table {
    border-collapse: collapse;
}
.mainDiv
{
    background-color:White;
}
.removeMainDiv
{
    background-color:White;
}
.tdHead
{
background: #FFEFD5; /* Old browsers */


}
.tblHead
{
background: #FFEFD5;
}
.mainGrid
{
background: #FFEFD5; /* Old browsers */

}
</style>
    <script type="" language="javascript">
        function _fNvALIDeNTRY(_tYPE, _MaxL) { var _cTRL = event.srcElement; var _v = _cTRL.value; if (_tYPE == 'N' || _tYPE == 'n' || _tYPE == 'D' || _tYPE == 'd') { if ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 46) { var _sTi = _v.indexOf('.'); if ((_tYPE == 'D' || _tYPE == 'd') && _sTi <= -1) { if (_v.length < _MaxL - 2 || event.keyCode == 46) { event.returnValue = true; return true; } } else { if ((_v.substring(_sTi + 1, _v.length).length != 2 || _tYPE == 'N' || _tYPE == 'n') && (event.keyCode != 46)) { if (_v.length < _MaxL || _sTi > -1) { event.returnValue = true; return true; } } } } } else if (_tYPE == 'C' || _tYPE == 'c') { if (_v.length < _MaxL) { event.returnValue = true; return true; } } else if (_tYPE.substring(0, 3) == '-O-' || _tYPE.substring(0, 3) == '-o-') { _tYPE = _tYPE.replace(/-o-/, ''); if (_v.length < _MaxL) { var _C = String.fromCharCode(event.keyCode); if (_C == '"') _tYPE = _tYPE.replace("!!", '"'); if (_C == "'") _tYPE = _tYPE.replace("~~", "'"); if (_tYPE.indexOf(_C) == -1) { event.returnValue = true; return true; } } } event.returnValue = false; }

        function DRForAdmin($x, $r, rCnt) {
            var tb = $r.parentNode;
            var Ttb = tb.parentNode
            if (Ttb.rows.length > rCnt + 1) {
                tb.removeChild($r);
            }
            else
                clrNRw($r);
            adminAdjustCalc(tb, 1);
        }
        function adminAdjustCalc($x, isDelete) {
            $grandTotalEle = document.getElementById("grandTotalName");
            var grandTotalVal = parseFloat($grandTotalEle.innerHTML.replace(/,/g, ''));
            var $R;
            if (isDelete == 1) {
                //alert('ddd');
                $R = $x;
            }
            else {
                $R = $x.parentNode.parentNode.parentNode;
            }
            var $tot = 0;

            $totEle = document.getElementById("hidtamtval");
            var $tot = parseFloat($totEle.value.replace(/,/g, ''));
            
            var $plus = 0;
            var $minus = 0;
            var $temp = 0;
            for (var $rl = 1; $rl < $R.children.length; $rl++) {
                var $type = parseFloat($R.children[$rl].cells[1].children[0].value.replace(/,/g, ''));
                var $amount = parseFloat($R.children[$rl].cells[2].children[0].value.replace(/,/g, ''));
                if (isNaN($amount)) $amount = 0;

                if ($type == 1) {
                    $plus = $plus + $amount;
                }
                if ($type == 0) {
                    $minus = $minus + $amount;
                }

            }
            $temp = $plus - $minus;
            //alert($temp+"::"+$tot);
            if ($tot < 0) {
                //$tot=-$tot;
            }
            //alert($tot);
            //$tot = $temp + $tot;
            grandtotalcalc($temp,$tot);
            $totEle.value = $temp;

        }
        function grandtotalcalc1(addVal) {
            $grndTot = document.getElementById("grandTotalName");
            var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
            alert(grndVal + " kkk " + addVal);
            $grndTot.innerHTML = grndVal + addVal;
        }
        function _AdRowByCurrElem($x) {
           
            _tR = $x.parentNode.parentNode;
            _nTR = _tR.cloneNode(true);
            _tR.parentNode.appendChild(_nTR);
            //newRow.appendChild(_nTR);
            //_tR.parentNode.appendChild(newRow);
            clrNRw(_nTR)
        }
function clrNRw($r)
{
    for (var $rl = 0; $rl < $r.childNodes.length; $rl++) {
        $c = $r.childNodes[$rl];
        for (var $i = 0; $i < $c.childNodes.length; $i++) {
            $o = $c.childNodes[$i];

            if ($o.id != '' && $o.id != null) {
                $s = $o.id.split('_');
                $o.id = $s[0] + '_' + $r.rowIndex
            }
            if ($o.type == "checkbox") {
                $o.checked = false;
            }
            else if ($o.tagName == 'SELECT') {
                $o.selectedIndex = 0;
            }
            else if ($o.tagName == 'SPAN') {
                $o.innerText = "";
            }
            else if ($o.value != null && $o.type != "button" && $o.type != "hidden") {
                $o.value = "";

            }
            if ($o.pv != null) $o.pv = '';
            if ($o.Pval != null) $o.Pval = '';
        } 
    }
}
function DRForOthExp($x, $r, rCnt) {
    var $temp = $r.cells[1].childNodes[0].value.replace(/,/g, '');
    if (isNaN($temp) || $temp == '') $temp = 0;

    var tb = $r.parentNode;
    var Ttb = tb.parentNode
   
    if (Ttb.rows.length-1 > rCnt) {
        tb.removeChild($r);
    }
    else
        clrNRw($r);


    $OthExpTotValEle = document.getElementById("Othtotal");
    $grndTot = document.getElementById("grandTotalName");
    alert($OthExpTotValEle);
    alert($grndTot);
    var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
    var othExpVal = parseFloat($OthExpTotValEle.value);

    $grndTot.innerHTML = parseFloat(grndVal) - parseFloat($temp);
    $OthExpTotValEle.value = parseFloat(othExpVal) - parseFloat($temp);

}
function getMaxMisLmtVal($x) {
    var $limit = 0;
    var limit = "50";
    //alert(limit);
    var $R = $x.parentNode.parentNode;
    var $Fr = $R.cells[0].children[0].value;
    //alert($Fr);
    if ($Fr == '') {
        $limit = 0;

    }
    else {
        var $st = limit.indexOf($Fr + "#");
        if ($st > -1) {
            $st = $st + ($Fr + "#").length;
            var $et = limit.indexOf("$", $st);
            $limit = limit.substring($st, $et);
        }
    }
    return $limit;
}

function OthExpCalc($OthExpVal) {
    $OthExpTotValEle = document.getElementById("Othtotal");
    var othExpVal = parseFloat($OthExpTotValEle.value);
    //alert(othExpVal);

    var $maxLimit = parseFloat(getMaxMisLmtVal($OthExpVal));
    var amt = parseFloat($OthExpVal.parentNode.parentNode.cells[1].children[0].value);
    //alert($maxLimit+" ff "+amt);
    //alert(amt>=$maxLimit);
    if ($maxLimit > 0 && amt > $maxLimit) {
        alert("Amount should be less than equal to " + $maxLimit);
        $OthExpVal.parentNode.parentNode.cells[1].children[0].value = 0;
        //othExpVal=0;
    }

    var $R = $OthExpVal.parentNode.parentNode.parentNode;
    var $Tot = 0;
    var $temp = 0;
    for (var $rl = 1; $rl < $R.children.length; $rl++) {
        $temp = $R.children[$rl].cells[1].childNodes[0].value.replace(/,/g, '');
        if (isNaN($temp) || $temp == '') $temp = 0;
        $Tot = parseFloat($Tot) + parseFloat($temp);

    }
    $OthExpTotValEle.value = $Tot;
    //alert($Tot);
    //alert(othExpVal);
    grandtotalcalc($Tot, othExpVal);

}
function totalAllowCalc(newvalue, oldvalue) {
    var $tot = document.getElementById("AllowTotal");
    var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
    $tot.innerHTML = totval - oldvalue + newvalue;
}

function totalDistCalc(newvalue, oldvalue) {
    var $tot = document.getElementById("DistTotal");
    var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
    $tot.innerHTML = totval - oldvalue + newvalue;
}

function totalFareCalc(newvalue, oldvalue) {
    var $tot = document.getElementById("FareTotal");
    var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
    $tot.innerHTML = totval - oldvalue + newvalue;
}

function totalcalc(newvalue, oldvalue) {
    $tot = document.getElementById("TotalVal");
    var totval = parseFloat($tot.innerHTML.replace(/,/g, ''));
    $tot.innerHTML = totval - oldvalue + newvalue;
}
function grandtotalcalc(newvalue, oldvalue) {
    $grndTot = document.getElementById("grandTotalName");
    var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
    $grndTot.innerHTML = grndVal - oldvalue + newvalue;
}

    </script>
</head>
<body class="bodycss">
    <form id="form1" runat="server">
    <div class="mainDiv" id="mainDiv" runat="server" >
     <center><font size="3" face="Verdana, Arial, Helvetica, sans-serif" color="maroon"><b><u>Expense Statement For The month of <span id="mnthtxtId" runat="server"></span>-<span id="yrtxtId" runat="server"></span></u></b></font></center>
 <br />
     <table  width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="right">
                            <asp:GridView ID="grdExpMain" runat="server" Width="100%" Font-Size="8pt" 
                            HeaderStyle-BackColor="#FFEFD5" HeaderStyle-CssClass="mainGrid" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="black"  HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" cellpadding="10" cellspacing="5">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdExpMain.PageIndex * grdExpMain.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                         <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ADate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDayName" runat="server" Text='<%# Bind("theDayName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Work Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkType" runat="server" Text='<%#Eval("Expense_wtype_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Place of Work" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerrName" runat="server" Text='<%# Bind("Place_of_Work") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Allowance Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Allowance (in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllw" runat="server" Text='<%# Bind("Expense_Allowance") %>'>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <%--<asp:TemplateField HeaderText="From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" >
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                     <table>
                                            <tr>
                                           <td width="100px" align="left"><asp:Label ID="lblFrom" runat="server" Text='<%# Bind("From_place") %>'></asp:Label></td>
                                           <td width="200px" align="right"><asp:Label ID="lblTo" runat="server" Text='<%# Bind("To_place") %>'>'></asp:Label></td>
                                            </tr>
                                     </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Distance Travelled</br>(in Kms)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistance" runat="server" Text='<%# Bind("Expense_Distance") %>'>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Fare</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFare" runat="server" Text='<%# Bind("Expense_Fare") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total Amt</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Expense_Total") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="100%" align="right">
            <tr><td colspan="10">&nbsp;</td></tr>

<tr><td colspan="8" align="right">

                                <asp:GridView ID="otherExpGrid" runat="server" Width="40%" Font-Name="Verdana"  Font-Size="8pt" 
            HeaderStyle-BackColor="#FFEFD5"
            HeaderStyle-ForeColor="black" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Exepense Name" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblSexpName" runat="server" Text='<%# Bind("Parameter_Name") %>'></asp:Label>
                                      <%--      <asp:HiddenField ID="hdnSexpName" runat="server" Value='<%#Eval("Expense_Parameter_Code")%>' />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSexpAmnt" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
</td></tr>

<tr><td  align="right">

                                <asp:GridView ID="expGrid" runat="server"  Width="40%" Font-Name="Verdana"  Font-Size="8pt" 
            HeaderStyle-BackColor="#FFEFD5"
            HeaderStyle-ForeColor="black" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("Paritulars") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
</td></tr>
<tr><td  align="right">

                                <asp:GridView ID="adminExpGrid" runat="server"  Width="40%" Font-Name="Verdana"  Font-Size="8pt" 
            HeaderStyle-BackColor="#FFEFD5"
            HeaderStyle-ForeColor="black" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("Paritulars") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("typ") %>'></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                               </Columns>
                                
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
</td></tr>
</table>
<div id="misExp" visible="false" runat="server">
<br />
<table width="100%"  border="0">
 <tr><td  style="padding-top:10px;text-align:right;color:red; font-family:Times New Roman;font-weight:bold;font-size:30px">Grand Total : </td><td style="padding-top:10px;color:red;text-align:right;font-family:Times New Roman;font-weight:bold;font-size:30px" runat="server" id="grandTotalName">0</td></tr>
</table> <asp:HiddenField ID="otherExpValues" runat="server" Value="" /><asp:HiddenField ID="hidtamtval" runat="server" Value="0" />
<br />
<br />
<br />
    </div>
    </div>

    </form>

</body>
<script>
    function saveOtherExp() {
        //alert(document.getElementById("otherExp"));
        var otherExpRmk = document.getElementsByName("tP");
        //alert(otherExpRmk.length);
        var otherExpVal = document.getElementsByName("tAmt");
        var otherExp = document.getElementsByClassName("Combovalue");
        var exp=0, val=0,gt=0;
        var remarks="";
        for (var i = 0; i < otherExpRmk.length; i++) {
            var value = otherExp[i].options[otherExp[i].selectedIndex].value;
            var text = otherExp[i].options[otherExp[i].selectedIndex].text;
           
             alert(otherExpRmk[i].value + "::" + otherExpVal[i].value + "::" + otherExp[i].value);
            if (i == 0) {
                remarks =  otherExpRmk[i].value;
                val = otherExpVal[i].value;
                exp = value+"="+text;
            }
            else {
                remarks = remarks + "," + otherExpRmk[i].value;
                val = val + "," + otherExpVal[i].value;
                exp = exp+","+value + "=" + text;

            }

        }
        $grandTotalEle = document.getElementById("grandTotalName");
        var gt = parseFloat($grandTotalEle.innerHTML.replace(/,/g, ''));
        alert(remarks + "~" + val + "~" + exp);
        document.getElementById("otherExpValues").value = remarks + "~" + val + "~" + exp+"~"+gt;
        
    }
</script>
</html>
