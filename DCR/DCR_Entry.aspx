<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Entry.aspx.cs" Inherits="DCR_DCR_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title>Daily Work Entry</title>
    <link href="../css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../css/DCR_Entry.css" rel="stylesheet" type="text/css" /> 
    <script src="../JsFiles/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../JsFiles/DCR_Entry.js" type="text/javascript"></script>   
    <link rel="stylesheet" type="text/css" href="ui/css/jquery.timeentry.css"/> 
    <script type="text/javascript" src="ui/js/jquery.mousewheel.js"></script> 
    <script type="text/javascript" src="ui/js/jquery.plugin.js"></script> 
    <script type="text/javascript" src="ui/js/jquery.timeentry.js"></script>
</head>

<body class="loading">
    <form id="frm" runat="server">
    <div class="modal"></div>
    <div class="Process modl">
        <div class="container">
            <b>DCR Saving</b>
            <progress></progress>
        </div>
	</div>

    <div class="pad HDBg"><a href="#" class="button mnu-bt" onclick="toggleMenu()"><i class="fa fa-bars"></i></a><a runat="server" id="aHome" class="button home-bt">Home</a><div class='hCap'>Daily Calls Report For : <div runat="server" class="highlightor" id="SFInf"></div> - <div id="DtInf" runat="server" class="dtDisp"></div></div></div>
    <div id="mnuWrapr">
        <div class="aside active">
            <div class="sidebar" >
                <div class="WTplcH"><span class="mnuLblCap">Work Type</span>
                    <asp:DropDownList ID="ddl_WorkType" onchange="ChangeWT()" class="ddlBox green" runat="server" style="display:block;width:100%;"><asp:ListItem Text="Select the Work Type"></asp:ListItem></asp:DropDownList>
                </div>
                <ul id="mnuTab"></ul>                
                <a href="#" class="button button-green submit-bt" onclick="DCR.validDCR()">Final Submit</a>
            </div>
        </div>
        <div id="WorkArea" class="Work-Area active">
            <div id="planer">
                <div runat="server" class="plnPlholder" id="Plchold_HQ">
                    <span class="lblCap">Headquarter</span>
                    <asp:DropDownList ID="ddl_HQ" onchange="ChangeHQ();" class="ddlBox" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="plnPlholder">
                    <span runat="server" id="lbSDP" class="lblCap lblSDP">SDP</span>
                    <asp:DropDownList ID="ddl_SDP" onchange="srtCusTwbs(this);" class="ddlBox" runat="server">
                    </asp:DropDownList>
                </div>
                 <a href="#" class="button button-green submit-bt1" onclick="DCR.validDCR()">Final Submit</a>
            </div>
            <div id="working-Area">
            </div>           
            <div class="alert-box notice" style="display:none"><span></span></div>
            <div id="wProd" class="wind-o">
                <table class="fg-group TBH">
                   <tr><th w="280">Product</th><th w="80">Qty</th><th w="80" style="display:none">Value</th><th w="40"><a href="#" class="button go-bt" onclick="addRow('tProd')">+</a></th></tr>
                </table>
                <div class="wScroll">
                <table id='tProd' class="fg-group TBD">
                    <tr>
                        <td><div class="ddl-Box search" data-value="" data-text="- Select the Product -" data-src="d_Prod" data-vf='id' data-tf='name'></div></td>
                        <td><input type="number" class="tPQ" name="tPQ" min="0" maxlength="3"></td>
                        <td style="display:none"><input type="number" class="tPV" value="0" name="tPV" min="0" maxlength="7"></td>
                        <td><a href="#" class="button button-red go-bt" onclick="delRow(this)">-</a></td>
                    </tr>
                </table>
                </div>
                <div style="height: 22px;padding:2px;margin:5px 0px;"><a href='#' class='button button-green go-bt' onclick="svWinVal(event)" style="margin:0px 5px;float:left">Save</a><a href='#' class='button button-red go-bt' style="margin:0px 5px;float:left" onclick="ddlWinClose()">Cancel</a></div>
            </div>
            <div id="wInput" class="wind-o">
                <table class="fg-group TBH">
                    <tr><th w="280">Input</th><th w="80">Qty</th><th w="40"><a href="#" class="button go-bt" onclick="addRow('tInput')">+</a></th></tr>
                </table>
                <div class="wScroll">
                    <table id="tInput" class="fg-group TBD">
                        <tr>
                            <td><div class="ddl-Box search" data-value="" data-text="- Select the Input -" data-src="d_Input" data-vf='id' data-tf='name'></div></td>
                            <td><input type="number" class="tGQ" name="tGQ" min="0" maxlength="3"></td>
                            <td><a href="#" class="button button-red go-bt" onclick="delRow(this)">-</a></td>
                        </tr>
                    </table> 
                </div>
                <div style="height: 22px;padding:2px;margin:5px 0px;"><a href='#' class='button button-green go-bt' onclick="svWinVal(event)" style="margin:0px 5px;float:left">Save</a><a href='#' class='button button-red go-bt' style="margin:0px 5px;float:left" onclick="ddlWinClose()">Cancel</a></div>
            </div>
            <div id="wRem" class="wind-o" style="padding:5px 1px">
                <table class="fg-group">
                    <tr><th>Call Feedback</th></tr>
                    <tr><td><div id="ddl_fedBk" class="ddl-Box" style="width:100%;" data-value="" data-text="- Select the Call Feedback -" data-src="d_fedbk" data-vf='id' data-tf='name'></div></td></tr>
                    <tr><th>Remarks</th></tr>
                    <tr><td><textarea id="tCusRem" style="display:block;width:380px;height:200px"></textarea></td></tr>
                </table>
                <a href='#' class='button button-green go-bt' onclick="svWinVal(event)" style="margin:0px 5px;">Save</a>
            </div> 
            
            
            <div id="wNCus" class="wind-n"  style="padding:0px">
                <div class="cntnr">
                    <div class="pad HDBg" ><b class="wCap"></b><a class="button button-red mnu-bt" onclick="CloseWin()">X</a></div>
                    <table class="fg-group">
                        <tr><td>Name</td><td colspan="3"><input type="text" value="" id="tNwCus"/> </td></tr>
                        <tr><td>Address</td><td colspan="3"><input type="text" value="" id="tNwAdd"/> </td></tr>
                        <tr><td class="lblSDP">SDP</td><td colspan="3"><div id="ddl_Ntwn" class="ddl-Box search" data-value="" data-text="- Select -" data-src="d_twns" data-vf='id' data-tf='name'></div></td></tr>
                        <tr class="drE"><td>Category</td><td><div id="ddl_NCat" class="ddl-Box search" data-value="" data-text="- Select -" data-src="d_Cat" data-vf='id' data-tf='name'></div></td>
                            <td>Specialty</td><td><div id="ddl_NSpc" class="ddl-Box search" data-value="" data-text="- Select -" data-src="d_Spec" data-vf='id' data-tf='name'></div></td></tr>
                        <tr class="drE"><td>Class</td><td><div id="ddl_NCla" class="ddl-Box search" data-value="" data-text="- Select -" data-src="d_Cla" data-vf='id' data-tf='name'></div></td>
                            <td>Qual.</td><td><div id="ddl_NQua" class="ddl-Box search" data-value="" data-text="- Select -" data-src="d_Qua" data-vf='id' data-tf='name'></div></td></tr>
                     </table>
                    <a href='#' style="margin:7px;float:left" class='button button-green go-bt' onclick="AddtoListCus()">Add To List</a><a href='#' class='button button-red go-bt' style="margin:7px;float:left" onclick="CloseWin()">Cancel</a>
                </div>
            </div>           
        </div>
    </div>

    <asp:HiddenField ID="hSF_Code" runat="server" />
    <asp:HiddenField ID="hDiv" runat="server" />
    <asp:HiddenField ID="hSFTyp" runat="server" />
    <asp:HiddenField ID="hDCRDt" runat="server" />
    <asp:HiddenField ID="hSTime" runat="server" />
    <asp:HiddenField ID="hCurrDt" runat="server" />
    <asp:HiddenField ID="hDtTyp" runat="server" />

    <asp:ScriptManager ID="ScriptService" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        _C = {
            ses: { id: 'ses', cap: 'Session', w: '80', src: 'd_Ses', df: '- Ses -', val: '', ty: 'ddl-Box', iw: '120', gw: '80' },
            tm: { id: 'tm', cap: 'Time', w: '90', ty: 'txt_Tm' },
            cus: { id: 'cus', cap: 'Customer', w: '250', src: 'd_cus', df: '-- Customer --', val: '', ty: 'ddl-Box search', iw: '300', adf:'TCd,TNm,sf' },
            pob: { id: 'pob', cap: 'POB', w: '90', val: '', df: '4,2', ty: 'txt_Dc',mxl: 7},
            jw: { id: 'jw', cap: 'Joint Work', w: '200', src: 'd_JW', df: '', val: '', ty: 'ddl-Box multi', iw: '300', gw: '300' },
            prd: { id: 'prd', cap: 'Product', w: '90', src: 'wProd', vf: 'id', tf: 'name', df: '- Product -', val: '', ty: 'ddl-Box wind', iw: '300' },
            inp: { id: 'inp', cap: 'Input', w: '90', src: 'wInput', vf: 'id', tf: 'name', df: '- Input -', val: '', ty: 'ddl-Box wind', iw: '300' },
            rem: { id: 'rem', cap: 'Remark', w: '70', src: 'wRem', df: '', val: '', ty: 'ddl-Box wind', iw: "300", mxl:350},
            go: { id: 'go', cap: 'Go', w: '90', gw: '60', src: 'ins', df: 'Go', val: '', ty: 'button green' }
        };
        __Menu = {
            D: { name: 'Listed Doctor', key: 'D', ic: 'stethoscope', eSrc: 'drs' },
            C: { name: 'Chemist', key: 'C', ic: 'flask', eSrc: 'chm' },
            S: { name: 'Stockist', key: 'S', ic: 'ambulance', eSrc: 'stk' },
            H: { name: 'Hospital', key: 'H', ic: 'hospital-o', eSrc: 'hos' },
            U: { name: 'Unlisted Doctor', key: 'U', ic: 'stethoscope', eSrc: 'udr' },
            R: { name: 'Remarks', key: 'R', ic: 'commenting', imp: 1 },
            P: { name: 'Preview', key: 'P', ic: 'search', imp: 1 }
        };
        document.oncontextmenu = function () { return false; };
    </script>
    </form>
</body>
</html>
