<%@ Page Title="Expense Statement" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MR.master" CodeFile="RptAutoExpense.aspx.cs" Inherits="MasterFiles_MR_RptAutoExpense" %>
<%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Expense Statement</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
     <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
   
     <style type="text/css">
             table {
    border-collapse: collapse;
}
.mainDiv
{
}
.removeMainDiv
{
    background-color:White;
}sf
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
        function TerritoryView(sfCode, divCode) {
            window.open("Territory_View.aspx?sfCode=" + sfCode + "&divCode=" + divCode, 'TerritoryView', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
        }
        function sfcView(sfCode, divCode) {
           // alert("sss");
            window.open("/../masterfiles/Reports/Distance_fixation_view.aspx?sfCode=" + sfCode + "&divCode=" + divCode, 'sfcView', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
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
    //alert($OthExpTotValEle);
    //alert($grndTot);
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
    var $Fr = $R.cells[0].children[0].value.split("##")[1];
    //alert($Fr);
    if ($Fr == '') {
        $limit = 0;

    }
    else {
        $limit = $Fr;
    }
    return $limit;
}

function OthExpCalc($OthExpVal) {
    $OthExpTotValEle = document.getElementById("Othtotal");
    var othExpVal = parseFloat($OthExpTotValEle.value);
    //alert(othExpVal);

    var $maxLimit = parseFloat(getMaxMisLmtVal($OthExpVal));
    var amt = parseFloat($OthExpVal.parentNode.parentNode.cells[1].children[0].value);
   // alert($maxLimit+" ff "+amt);
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

function totalcalc(newvalue, oldvalue,$tot) {
    //$tot = document.getElementById("TotalVal");
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
    <div id="menuId" runat="server">
    <%--<ucl:Menu ID="menu1" runat="server" />--%>
    <center>
    <table align="center">
    <tr><td><u><font color="#800000" size=4>Expense Statement</font></u><br/><br/></td></tr>
     
        <tr><td><asp:dropdownlist ID="monthId" runat="server"></asp:dropdownlist></td> 
   <td><asp:DropDownList ID="yearID" runat="server"></asp:DropDownList></td>
        <td><asp:Button ID="btnSubmit" runat ="server" Text ="Go" CssClass="BUTTON"
            onclick="btnSubmit_Click" /></td></tr>
  </table></center></div>
  <br />
  <div id="heading" runat="server" visible="false">
  <asp:HiddenField ID="sfCode" runat="server" Value="" /><asp:HiddenField ID="divCode" runat="server" Value="" />
   <table align="center" width="100%">
<tr>
<td align="left" width="25%"><asp:label Font-Bold="true" ForeColor="red" runat="server" Visible="false" id="messageId" Text=""></asp:label></td>
<td align="center" style="font-weight:bold" width="50%" ><font size="3"  face="Verdana, Arial, Helvetica, sans-serif" color="maroon"><b><u>Expense Statement For The month of <span style="background-color:yellow;color:red" id="mnthtxtId" runat="server"></span>-<span style="background-color:yellow;color:red" id="yrtxtId" runat="server"></span></u></b></font></td>
<td align="right" width="25%">&nbsp;
<font class="print" style="color:blue;cursor:pointer;font-weight:bold"><a onclick="TerritoryView(sfCode.value,divCode.value)"><b>Territory View</b></a></font>&nbsp;&nbsp;&nbsp;
<font class="print" style="color:blue;cursor:pointer;font-weight:bold"><a onclick="sfcView(sfCode.value,divCode.value)"><b>SFC View</b></a></font>&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Height="25px" Width="60px" Text="Back" onClick="btnBack_Click" /></td>
</tr>
</table>
<br/>
 <table align="center" width="100%">
<tr>
<td align="left"  style="font-weight:bold;font-size:small;" id="fieldforceId" runat="server"></td>
<td align="center" style="font-weight:bold;font-size:small;" id="hqId" runat="server"></td>
<td align="right" style="font-weight:bold;font-size:small;" id="empId" runat="server"></td>
</tr>
</table></div>
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
                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                         <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ADate" runat="server" Text='<%# Bind("Adate") %>'></asp:Label>
                                            <asp:HiddenField ID="adateHidden" runat="server" Value='<%# Bind("Adate1") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDayName" runat="server" Text='<%# Bind("theDayName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkType" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Place of Work" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" Width="200px" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerrName" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Allowance Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblCat" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label><asp:DropDownList  onchange="onAllowTypeChange(this,'false','')" class="from" ID="AllowType" Visible="false" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                                                <asp:ListItem Selected="false" Text="---Select---" Value=""></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="HQ" Value="HQ"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="EX" Value="EX"></asp:ListItem>
                                                <asp:ListItem Selected="false" Text="OS" Value="OS"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="allowTypeHidden" runat="server" Value='<%# Bind("Territory_Cat") %>' />
                                            <asp:HiddenField ID="allowTypeHidden1" runat="server" Value='<%# Bind("catTemp") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Type"  Visible="false" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Allowance (in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblAllw" runat="server" Text='<%# Bind("Allowance") %>'></asp:Label><asp:TextBox ID="txtAllow" width="50" visible="false" runat="server" Text='<%# Bind("Allowance") %>' onkeyup="onAllowChange(this)"></asp:TextBox><asp:HiddenField ID="allowHidden" runat="server" Value='<%# Bind("Allowance") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" >
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                     <table>
                                            <tr>
                                           <td width="100px" align="left"><asp:Label ID="lblFrom" runat="server" Text='<%# Bind("From_place") %>'> </asp:Label>
                                             <asp:DropDownList  onchange="_CalDist(this,'sss','rrr');" class="from" ID="fromPlace" Visible="false" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                                                <asp:ListItem Selected="false" Text="---Select---" Value=""></asp:ListItem>
                                            </asp:DropDownList><asp:HiddenField ID="fromHidden" runat="server" Value='<%# Bind("From_place") %>' /></td>
                                           <td width="200px" align="right"><asp:Label ID="lblTo" runat="server" Text='<%# Bind("To_place") %>'></asp:Label>
                                            <asp:DropDownList onchange="_CalDist(this,'sss','rrr');" class="to" ID="toPlace" Visible="false" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                                                <asp:ListItem Selected="false" Text="---Select---" Value=""></asp:ListItem>
                                            </asp:DropDownList><asp:HiddenField ID="toHidden" runat="server" Value='<%# Bind("To_place") %>' /></td>
                                            <td><asp:HiddenField ID="distPrev" runat="server" Value="0" /></td>
                                            </tr>
                                     </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Distance Travelled</br>(in Kms)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblDistance" runat="server" Text='<%# Bind("Distance") %>'></asp:Label><asp:HiddenField ID="distHidden" runat="server" Value='<%# Bind("Distance") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Fare</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:TextBox ID="txtFare" onkeyup="onFareChange(this);" visible="false" width="30" runat="server" Text='<%# Bind("Fare") %>'></asp:TextBox><asp:Label ID="lblFare" runat="server" Text='<%# Bind("Fare") %>'></asp:Label><asp:HiddenField ID="fareHidden" runat="server" Value='<%# Bind("Fare") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total Amt</br>(in Rs/-)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate><asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label><asp:HiddenField ID="totHidden" runat="server" Value='<%# Bind("Total") %>' /></ItemTemplate>
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
                                           
                                            <asp:Label ID="lblSexpName" runat="server" Text='<%# Bind("Expense_Parameter_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnSexpName" runat="server" Value='<%#Eval("Expense_Parameter_Code")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle ></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSexpAmnt" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
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
<br />
<br />
<table align="right" border="0">

<tr>
<th class="tblHead" colspan="4" align="center" style="padding-top:10px;font-weight:bold;"><b>Miscellaneous Expenses</b></th></tr>
<tr>
<td class="b" align="right" style="width: 50px;padding:0px;" id="td1">


 <table border="0" id="tableId" runat="server" clientidmode="Static">
<tr style="font-weight:bold">
<td class="tblHead" align="center"><b>Type</b></td>
<td class="tblHead" align="center"><b>Amount</b></td>
<td class="tblHead" align="center"><b>Remarks</b></td>
<td class="tblHead" colspan="2" align="center"><b>Add/Del</b></td></tr>
<tr>
 <td>
  <asp:DropDownList  class="otherExp" ID="otherExp" runat="server" AutoPostBack="false" SkinID="ddlRequired">
  <asp:ListItem Selected="false" Text="--Select--" Value="0"></asp:ListItem>
 </asp:DropDownList>
 </td>
<td><input type='text' class='textbox' name="OthExpVal" id="OthExpVal"  value="" size='6' maxlength=4 onkeyup="OthExpCalc(this)"/></td>
<td><input type='text' class='textbox' name="OthExpRmk" id="OthExpRmk" value="" size='50'/></td>
 <td>
<input type="button" id="btnadd" value=" + " class='btnSave' onclick="_AdRowByCurrElem(this)" />&nbsp
</td>
 <td>
<input type="button" value=" - " class='btnSave' onclick="DRForOthExp(this,this.parentNode.parentNode,1)"/>
</td>
 </tr>
            </table></td>
            
            </tr>
            </table>
<table width="100%"  border="0">
 <tr><td  style="padding-top:10px;text-align:right;color:red; font-family:Times New Roman;font-weight:bold;font-size:30px">Grand Total : </td><td style="padding-top:10px;color:red;text-align:right;font-family:Times New Roman;font-weight:bold;font-size:30px" runat="server" id="grandTotalName">0</td></tr>
</table> <asp:HiddenField ID="otherExpValues" runat="server" Value="" /><asp:HiddenField ID="Othtotal" runat="server" Value="0" />
<asp:HiddenField ID="distString" runat="server" Value="" /><asp:HiddenField ID="allowString" runat="server" Value="" />
<br />
<br />
               <center>
        <asp:Button ID="btnDrftSave" runat="server" CssClass="BUTTON" Width="80px" Height="25px"
            Text="Draft Save" Visible="true" OnClientClick="saveOtherExp()" OnClick="btnSaveDraft_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" Width="80px" Height="25px"
            Text="Send To HO" Visible="true" OnClientClick="saveOtherExp()" OnClick="btnSave_Click" />
    </center>
    </div>
    </div>

    </form>

</body>
<script>
    function saveOtherExp() {
        //alert(document.getElementById("otherExp"));
        var otherExpRmk = document.getElementsByName("OthExpRmk");
        //alert(otherExpRmk.length);
        var otherExpVal = document.getElementsByName("OthExpVal");
        var otherExp = document.getElementsByClassName("otherExp");
        var exp=0, val=0;
        var remarks="";
        for (var i = 0; i < otherExpRmk.length; i++) {
            var value = otherExp[i].options[otherExp[i].selectedIndex].value;
            var text = otherExp[i].options[otherExp[i].selectedIndex].text;
           
             //alert(otherExpRmk[i].value + "::" + otherExpVal[i].value + "::" + otherExp[i].value);
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
        alert(remarks + "~" + val + "~" + exp);
        document.getElementById("otherExpValues").value = remarks + "~" + val + "~" + exp;

    }
function _CalDist($x,TotDist,fare)
{
    var sVal = document.getElementById("allowString").value;
    var allowArr = sVal.split('@');
    fare = allowArr[3];
    //alert("fare :: " + fare);
    var $Dis=0;
    var $sDist=0;
        $distEle = document.getElementById("distString");

        SDis = $distEle.value;
    //alert(SDis);
        var selValue = $x.parentNode.children[0].selectedOptions[0].innerHTML;
        $x.parentNode.children[1].value=selValue;
        var $R = $x.parentNode.parentNode;
    var $Fr = $R.cells[0].childNodes[1].value.split("~~");
    var $To = $R.cells[1].childNodes[1].value.split("~~");
    //alert($Fr[0]+" :: "+$To[0]);
    var $R1 = $x.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
    var type = $R1.cells[4].children[2].value;
    if ("AE-FE" == type) {
        if ($To[0] != '') {
            $R1.cells[4].children[0].innerHTML = $To[1];
            $R1.cells[4].children[1].value = $To[1];
        }
        else {
            $R1.cells[4].children[0].innerHTML = "";
            $R1.cells[4].children[1].value = "";

        }
        return;
    }
    if ($To[0] != '') {
        if ("AS-FA" != type && "AS-FE" != type) {
            $R1.cells[4].children[0].innerHTML = $To[1];
            $R1.cells[4].children[1].value = $To[1];
        }
    }   
    else {
        if ("AS-FA" != type && "AS-FE" != type) {
            $R1.cells[4].children[0].innerHTML = "";
            $R1.cells[4].children[1].value = "";
        }
        $To[1] = '';
    }
    //alert("type :: "+ type);
    if ("NA-FA" != type && "AS-FA" != type && "AS-FE" != type && "AE-FA" != type) {
        
        onAllowTypeChange($R1, 'true', $To[1]);
    }
    else {
        if ("AS-FA" != type && "AS-FE" != type) {
            $R1.cells[4].children[0].innerHTML = "";
            $R1.cells[4].children[1].value = "";
        }
    }
    if ("AE-FE" == type || "AA-FE" == type || "AA-NF" == type || "AS-FE" == type) {
        return;
    }
    if ($Fr[0]=='' || $To[0]=='') 
    {

        $Dis = 0;
        $To[1] = "";
    }
    else
    {

        var $st = SDis.indexOf($Fr[0] + "#" + $To[0] + "#");
        if($st>-1)
        {
            $st = $st + ($Fr[0] + "#" + $To[0] + "#").length;
            var $et=SDis.indexOf("$",$st);
            $Dis=SDis.substring($st,$et);
	            $sDist=$Dis;

            if($R1.cells[4].children.length>0)
            {
                if ($R1.cells[4].children[0].value == 'AS-FA'  || $R1.cells[4].children[0].value == 'AE-FA'  || $R1.cells[4].children[0].value == 'AA-FA' || $R1.cells[4].children[0].value == 'AA-FE'
            || $R1.cells[4].children[0].value == 'AA-NF' || $R1.cells[4].children[0].value == 'NA-FA' || $R1.cells[4].children[0].value == 'AE-FE')
	            {
	            $Dis=$Dis*2;		
	            }
            }
        }
}
$Dis = $Dis * 2;
    //alert($Dis);
    var pValue = $R1.cells[7].children[1].value;
    pValue=parseFloat(pValue.replace(/,/g,''));
    //alert($R1.cells[4].children[0].value);
    if($R1.cells[4].children.length>0)
    {
        if($R1.cells[4].children.length>0 && $R1.cells[4].children[0].value=='  ' || $R1.cells[4].children[0].value=='Act  ' || $R1.cells[4].children[0].value=='OS  ' || $R1.cells[4].children[0].value=='EX  ' || $R1.cells[4].children[0].value=='HQ  '
        || $R1.cells[4].children[0].value=='FAct' || $R1.cells[4].children[0].value=='EAAct')
	        {
	        pValue=pValue*2;		
	        }
    }
    //alert(pValue);
    if (isNaN($Dis)) $Dis=0;
    if (isNaN(pValue)) pValue=0;
    $R1.cells[7].children[1].value = $Dis;
    $R1.cells[7].children[0].innerHTML = $Dis;
    //if (isNaN(TotDist.value)) TotDist.value=0;
    //var totDist=parseFloat($R1.cells[7].children[0].innerHTML.replace(/,/g, ''));
    //var tDis = ((totDist + parseFloat($Dis)) - parseFloat(pValue));
    //$R1.cells[9].children[0].innerHTML=((totDist+parseFloat($Dis))-parseFloat(pValue));
    //alert("dist "+tDis);
    var oldFare = parseFloat($R1.cells[8].children[1].innerHTML.replace(/,/g, ''));

    if ($Dis > 120)
    {
        $R1.cells[8].children[0].innerHTML = parseFloat(parseFloat($Dis) * parseFloat(fare));
        $R1.cells[8].children[1].value = parseFloat(parseFloat($Dis) * parseFloat(fare));
    }
    else
    {
        $R1.cells[8].children[0].innerHTML = parseFloat(parseFloat($Dis) * parseFloat(fare));
        $R1.cells[8].children[1].value = parseFloat(parseFloat($Dis) * parseFloat(fare));
    }
    var newFare = parseFloat($R1.cells[8].children[0].innerHTML.replace(/,/g, ''));
    //alert(oldFare + " f::f " + newFare);
    var allw=0;
    if(type=="AE-FA")
    {
        allw = parseFloat($R1.cells[5].children[0].value.replace(/,/g, ''));
        $R1.cells[4].children[0].innerHTML = $To[1];
        $R1.cells[4].children[1].value = $To[1];
    }
    else
    {
        allw = parseFloat($R1.cells[5].children[0].innerHTML.replace(/,/g, ''));
    }
    //alert("ddd"+$R1.cells[4].children[0].children.length);
    if ($R1.cells[4].children[0].children.length == 4) {
        //allw = 0;
    }

    var oldTot = parseFloat($R1.cells[9].childNodes[0].innerHTML.replace(/,/g, ''));
    if(isNaN(allw))allw=0;
    if(isNaN(newFare))newFare=0;
    if(isNaN(oldTot))oldTot=0;
    var newTot = parseFloat(newFare) + parseFloat(allw);
    $R1.cells[9].childNodes[0].innerHTML = parseFloat(newTot);
    $R1.cells[9].childNodes[1].value = parseFloat(newTot);
    //alert($R1.parentNode.children.length - 1);
    $tot=$R1.parentNode.children[$R1.parentNode.children.length - 1].cells[9].children[0];
    $totF = $R1.parentNode.children[$R1.parentNode.children.length - 1].cells[8].children[0];
    //$tot=document.getElementById("TotalVal");
    $grndTot=document.getElementById("grandTotalName");

    var totFareVal = parseFloat($totF.innerHTML.replace(/,/g, ''));
    var totval=parseFloat($tot.innerHTML.replace(/,/g,''));
    var grndVal=parseFloat($grndTot.innerHTML.replace(/,/g,''));
    if (isNaN(totval)) totval=0;
    if (isNaN(grndVal)) grndVal = 0;
    if (isNaN(oldFare)) oldFare = 0;
    //alert(totval + " :: " + grndVal + " :: " + totFareVal);
    var newtotval = grndVal - oldTot + newTot;
    $tot.innerHTML = totval - oldTot + newTot;
    //totalcalc(newTot,oldTot,$tot);
    grandtotalcalc(newtotval, grndVal);
    $totF.innerHTML = totFareVal - oldFare + newFare;
    //totalFareCalc(newFare,oldFare);
}
function onAllowTypeChange($e, flag,type) {
 var $R;
 var allType = $e.value;
 //alert(flag+" :: "+$e);
 if (flag == 'false') {
     $R = $e.parentNode.parentNode;
 }
 else {
     $R = $e;
     allType = type;
 }
 $R.cells[4].children[1].value = allType;
       var oldAllow = parseFloat($R.cells[5].children[0].innerHTML.replace(/,/g, ''));
       var oldRowTot = parseFloat($R.cells[9].children[0].innerHTML.replace(/,/g, ''));
       var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[9].children[0];
       var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
       var $grndAllowTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
       var oldGrndAllowTot = parseFloat($grndAllowTot.innerHTML.replace(/,/g, ''));
       // alert(oldRowTot +" :: "+oldTot);
    var sVal = document.getElementById("allowString").value;
    var allowArr = sVal.split('@');
    var ex = 0, os = 0, hq;
    ex = allowArr[0];
    os = allowArr[1];
    hq = allowArr[2];
    if (allType == "EX") {
        $R.cells[5].children[0].innerHTML = ex;
        $R.cells[5].children[1].value = ex;

    }
    else if (allType == "OS" || allType == "OS-EX") {
        $R.cells[5].children[0].innerHTML = os;
        $R.cells[5].children[1].value = os;
    }
    else if (allType == "HQ") {
        $R.cells[5].children[0].innerHTML = hq;
        $R.cells[5].children[1].value = hq;
    }
    else {
        $R.cells[5].children[0].innerHTML = "0";
        $R.cells[5].children[1].value = "0";
    }
    var newAllow = parseFloat($R.cells[5].children[0].innerHTML.replace(/,/g, ''));
    //alert(oldAllow + " :: " + newAllow);
    if (isNaN(oldAllow)) oldAllow = 0;
    if (isNaN(newAllow)) newAllow = 0;
    if (isNaN(oldRowTot)) oldRowTot = 0;

    $R.cells[9].children[0].innerHTML = oldRowTot - oldAllow + newAllow;
    $R.cells[9].children[1].value = oldRowTot - oldAllow + newAllow;
       $tot.innerHTML = oldTot - oldAllow + newAllow;
       $grndAllowTot.innerHTML = oldGrndAllowTot - oldAllow + newAllow;

       $grndTot = document.getElementById("grandTotalName");
       var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
       if (isNaN(grndVal)) grndVal = 0;
       var newtotval = grndVal - oldAllow + newAllow;
       grandtotalcalc(newtotval, grndVal);
   }
   function onAllowChange($e) {
    $R = $e.parentNode.parentNode;
    var oldAllow = parseFloat($R.cells[5].children[1].value.replace(/,/g, ''));
    var oldRowTot = parseFloat($R.cells[9].children[0].innerHTML.replace(/,/g, ''));
    //alert($R.parentNode.children.length - 1);
    var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[9].children[0];
    var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
    var $grndAllowTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[5].children[0];
    var oldGrndAllowTot = parseFloat($grndAllowTot.innerHTML.replace(/,/g, ''));
    //alert(oldRowTot + " :: " + oldTot);
    var newAllow = parseFloat($R.cells[5].children[0].value.replace(/,/g, ''));
    if (isNaN(oldAllow)) oldAllow = 0;
    if (isNaN(newAllow)) newAllow = 0;
    $R.cells[5].children[1].value = newAllow;
    //alert(oldAllow + " :: " + newAllow);
    $R.cells[9].children[0].innerHTML = oldRowTot - oldAllow + newAllow;
    $R.cells[9].children[1].value = oldRowTot - oldAllow + newAllow;
    $tot.innerHTML = oldTot - oldAllow + newAllow;
    $grndAllowTot.innerHTML = oldGrndAllowTot - oldAllow + newAllow;
    $grndTot = document.getElementById("grandTotalName");
    var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
    if (isNaN(grndVal)) grndVal = 0;
    var newtotval = grndVal - oldAllow + newAllow;
    grandtotalcalc(newtotval, grndVal);
}

function onFareChange($e) {
    $R = $e.parentNode.parentNode;
    var oldFare = parseFloat($R.cells[8].children[1].value.replace(/,/g, ''));
    var oldRowTot = parseFloat($R.cells[9].children[0].innerHTML.replace(/,/g, ''));
    var $tot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[9].children[0];
    var oldTot = parseFloat($tot.innerHTML.replace(/,/g, ''));
    var $grndFareTot = $R.parentNode.children[$R.parentNode.children.length - 1].cells[8].children[0];
    var oldGrndFareTot = parseFloat($grndFareTot.innerHTML.replace(/,/g, ''));
    // alert(oldRowTot + " :: " + oldTot);
    var newFare = parseFloat($R.cells[8].children[0].value.replace(/,/g, ''));
    if (isNaN(oldFare)) oldFare = 0;
    if (isNaN(newFare)) newFare = 0;
    //alert(newFare + " :: " + oldFare);

    $R.cells[8].children[1].value = newFare;
    $R.cells[9].children[0].innerHTML = oldRowTot - oldFare + newFare;
    $R.cells[9].children[1].value = oldRowTot - oldFare + newFare;
    $tot.innerHTML = oldTot - oldFare + newFare;
    $grndFareTot.innerHTML = oldGrndFareTot - oldFare + newFare;

    $grndTot = document.getElementById("grandTotalName");
    var grndVal = parseFloat($grndTot.innerHTML.replace(/,/g, ''));
    if (isNaN(grndVal)) grndVal = 0;
    var newtotval = grndVal - oldFare + newFare;
    grandtotalcalc(newtotval, grndVal);

}
</script>
</html>
</asp:Content>