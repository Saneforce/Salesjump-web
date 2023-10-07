<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Entry.aspx.cs" Inherits="DCR_DCR_Entry" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title>Daily Work Entry</title>
    <link href="../css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../css/DCR_Entry.css" rel="stylesheet" type="text/css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../JsFiles/DCR_Entry.js" type="text/javascript"></script>

</head>

<body>
    <form id="form1" runat="server">
    <div class="pad HDBg"><a href="#" class="button mnu-bt" onclick="toggleMenu()"><i class="fa fa-bars"></i></a><a href="#" class="button home-bt">Home</a><div class='hCap'>Daily Work Entry For : <div runat="server" class="highlightor" id="DtInf"></div> - <div class="dtDisp">12/12/2016</div></div></div>
    <div id="mnuWrapr"  >
        <div class="aside active">
            <div class="sidebar" >
                <span class="mnuLblCap">Worktype</span>
                <asp:DropDownList ID="ddl_WorkType" class="ddlBox green" runat="server" style="display:block;width:100%;">
                    <asp:ListItem Text="Select the Work Type"></asp:ListItem>
                </asp:DropDownList>                
                 <ul id="mnuTab">
                     <li data-tag="D"><u class="fa fa-stethoscope"></u><u class="sKey">[ Ctrl + D ]</u>Listed Doctor</li>
                     <li data-tag="C"><u class="fa fa-flask"></u><u class="sKey">[ Ctrl + C ]</u>Chemist</li>
                     <li data-tag="S"><u class="fa fa-ambulance"></u><u class="sKey">[ Ctrl + S ]</u>Stockist</li>
                     <li data-tag="U"><u class="fa fa-stethoscope"></u><u class="sKey">[ Ctrl + U ]</u>Unlisted Doctor</li>
                     <li data-tag="R"><u class="fa fa-commenting"></u><u class="sKey">[ Ctrl + R ]</u>Remarks</li>
                     <li data-tag="P"><u class="fa fa-search"></u><u class="sKey">[ Ctrl + I ]</u>Preview</li>
                 </ul>
                 <a href="#" class="button button-green submit-bt">Final Submit</a>
            </div>
        </div>
        <div id="WorkArea" class="Work-Area active">
            <div id="planer">
                <div class="plnPlholder">
                    <span class="lblCap">Headquater</span>
                    <asp:DropDownList ID="ddl_HQ" class="ddlBox" runat="server">
                        <asp:ListItem Text="Select the Headquater"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="plnPlholder">
                    <span class="lblCap">SDP</span>
                    <asp:DropDownList ID="ddl_SDP" class="ddlBox" runat="server">
                        <asp:ListItem Text="Select the SDP"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <a href="#" class="button button-green submit-bt1">Final Submit</a>
            </div>
            <div id="working-Area">
                <asp:Panel ID="idWD" class="wTab" runat="server">
                    <table ID="tbD" class="fg-group" runat="server" border=1>
                        <tr><th>Session</th><th>Time</th><th>Listed Doctor Name</th><th>Joint Work</th><th>Product</th><th>Input</th><th>Go</th></tr>
                        <tr>
                            <td><div class="ddl-Box" style="width:80px;" data-value="" data-text="- Ses -" data-inwidth='120px' data-src="d_Ses"></div></td> 
                            <td><div class="ddl-Box" style="width:90px;" data-value="" data-text="- Time -" data-inwidth='120px' data-src="d_Tm"></div></td> 
                            <td><div class="ddl-Box search" style="width:250px" data-value="" data-text="- Select The Listed Doctor -" data-inwidth='300px' data-src="d_Doc"></div></td>
                            <td><div class="ddl-Box multi" style="width:90px" data-value="" data-text="- JW -" data-inwidth='300px' data-src="d_JW"></div></td> 
                            <td><div class="ddl-Box wind" style="width:90px" data-value="" data-text="- Product -" data-src="wProd"></div></td> 
                            <td><div class="ddl-Box wind" style="width:90px" data-value="" data-text="- Input -" data-src="wInput"></div></td> 
                            <td><a href="#" class="button go-bt">GO</a></td>
                        </tr>                        
                    </table><br />


                    <div>
                        <table ID="Table6" class="fg-grid" runat="server" border=1>                            
                            <tr><th style="width:80px;">Session</th><th style="width:90px;">Time</th><th style="width:250px">Listed Doctor Name</th><th style="width:250px">Joint Work</th><th style="width:50px">Del</th></tr>
                            <tr><td style="width:80px;">Session</td><td style="width:90px;">Time</td><td style="width:250px">Listed Doctor Name</td><td style="width:250px">Joint Work</td><td style="width:50px"><a href="#" class="button button-red go-bt">-</a></td></tr>
                            <tr><td style="width:80px;">Session</td><td style="width:90px;">Time</td><td style="width:250px">Listed Doctor Name</td><td style="width:250px">Joint Work</td><td style="width:50px"><a href="#" class="button button-red go-bt">-</a></td></tr>
                            <tr><td style="width:80px;">Session</td><td style="width:90px;">Time</td><td style="width:250px">Listed Doctor Name</td><td style="width:250px">Joint Work</td><td style="width:50px"><a href="#" class="button button-red go-bt">-</a></td></tr>
                            <tr><td style="width:80px;">Session</td><td style="width:90px;">Time</td><td style="width:250px">Listed Doctor Name</td><td style="width:250px">Joint Work</td><td style="width:50px"><a href="#" class="button button-red go-bt">-</a></td></tr>
                            <tr><td style="width:80px;">Session</td><td style="width:90px;">Time</td><td style="width:250px">Listed Doctor Name</td><td style="width:250px">Joint Work</td><td style="width:50px"><a href="#" class="button button-red go-bt">-</a></td></tr>
                            <tr><td style="width:80px;">Session</td><td style="width:90px;">Time</td><td style="width:250px">Listed Doctor Name</td><td style="width:250px">Joint Work</td><td style="width:50px"><a href="#" class="button button-red go-bt">-</a></td></tr>
                        </table>           
                    </div>
                </asp:Panel>
                <asp:Panel ID="idWC" class="wTab" runat="server">
                    <table ID="Table1" class="fg-group" runat="server" border=1>
                        <tr><th>Session</th><th>Time</th><th>Chemist Name</th><th>Joint Work</th><th>Product</th><th>Input</th><th>Go</th></tr>
                        <tr>
                            <td><div class="ddl-Box" style="width:80px;" data-value="" data-text="- Ses -" data-src="d_Ses"></div></td> 
                            <td><div class="ddl-Box" style="width:90px;" data-value="" data-text="- Time -" data-src="d_Tm"></div></td> 
                            <td><div class="ddl-Box search" style="width:250px" data-value="" data-text="- Select The Chemist -" data-src="d_Chm"></div></td> 
                            <td><div class="ddl-Box Multi" style="width:90px" data-value="" data-text="- JW -" data-src="d_JW"></div></td> 
                            <td><div class="ddl-Box wind" style="width:90px" data-value="" data-text="- Product -" data-src="wProd"></div></td> 
                            <td><div class="ddl-Box wind" style="width:90px" data-value="" data-text="- Input -" data-src="wInput"></div></td> 
                            <td><a href="#" class="button go-bt">GO</a></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="idWS" class="wTab" runat="server">
                    <table ID="Table4" class="fg-group" runat="server" border=1>
                        <tr><th>Session</th><th>Time</th><th>Stockist Name</th><th>Joint Work</th><th>Product</th><th>Input</th><th>Go</th></tr>
                        <tr>
                            <td><div class="ddl-Box" style="width:80px;" data-value="" data-text="- Ses -" data-src="d_Ses"></div></td> 
                            <td><div class="ddl-Box" style="width:90px;" data-value="" data-text="- Time -" data-src="d_Tm"></div></td> 
                            <td><div class="ddl-Box search" style="width:250px" data-value="" data-text="- Select The Stockist -" data-src="d_Chm"></div></td> 
                            <td><div class="ddl-Box Multi" style="width:90px" data-value="" data-text="- JW -" data-src="d_JW"></div></td> 
                            <td><div class="ddl-Box wind" style="width:90px" data-value="" data-text="- Product -" data-src="wProd"></div></td> 
                            <td><div class="ddl-Box wind" style="width:90px" data-value="" data-text="- Input -" data-src="wInput"></div></td> 
                            <td><a href="#" class="button go-bt">GO</a></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="idWU" class="wTab" runat="server">
                    <table ID="Table5" class="fg-group" runat="server" border=1>
                        <tr><th>Session</th><th>Time</th><th>Unlised Doctor Name</th><th>Joint Work</th><th>Product</th><th>Input</th><th>Go</th></tr>
                        <tr>
                            <td><div class="ddl-Box" style="width:80px;" data-value="" data-text="- Ses -" data-src="d_Ses"></div></td> 
                            <td><div class="ddl-Box" style="width:90px;" data-value="" data-text="- Time -" data-src="d_Tm"></div></td> 
                            <td><div class="ddl-Box search" style="width:250px" data-value="" data-text="- Select The Unlised Doctor -" data-src="d_Chm"></div></td> 
                            <td><div class="ddl-Box Multi" style="width:90px" data-value="" data-text="- JW -" data-src="d_JW"></div></td> 
                            <td><div class="ddl-Box wind" style="width:90px" data-value="" data-text="- Product -" data-src="wProd"></div></td> 
                            <td><div class="ddl-Box wind" style="width:90px" data-value="" data-text="- Input -" data-src="wInput"></div></td> 
                            <td><a href="#" class="button go-bt">GO</a></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="idWR" class="wTab" runat="server">
                </asp:Panel>
                <asp:Panel ID="idWP" class="wTab" runat="server">
                </asp:Panel>    
            </div>           
            <div class="alert-box notice"><span></span></div>
            <div id="wProd" class="wind-o">
                <table ID="Table2" class="fg-group" runat="server" border=1>
                    <tr><th>Product</th><th>Qty</th><th>Value</th><th>Add</th></tr>
                    <tr>
                        <td><div class="ddl-Box search" style="width:280px;" data-value="" data-text="- Select the Product -" data-src="d_Prod"></div></td>
                        <td><input type="number" style="width:80px;"></td>
                        <td><input type="number" style="width:80px;"></td>
                        <td><a href="#" class="button go-bt">+</a></td>
                    </tr>
                </table>
            </div>
            <div id="wInput" class="wind-o">
                <table ID="Table3" class="fg-group" runat="server" border=1>
                    <tr><th>Input</th><th>Qty</th><th>Value</th><th>Add</th></tr>
                    <tr>
                        <td><div class="ddl-Box search" style="width:280px;" data-value="" data-text="- Select the Input -" data-src="d_Input"></div></td>
                        <td><input type="number" style="width:80px;"></td>
                        <td><input type="number" style="width:80px;"></td>
                        <td><a href="#" class="button go-bt">+</a></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
