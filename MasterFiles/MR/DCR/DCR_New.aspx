<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_New.aspx.cs" Inherits="MasterFiles_MR_DCR_DCR_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DCR Entry</title>
    <link type="text/css" rel="stylesheet" href="../../../css/dcrstyle.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    
    <style type="text/css">
        .BUTTON : hover
        {
            background-color: #A6A6D2;
        }
        .Spc
        {
            padding: 1px 1px;
        }
        .hiddencol
        {
        display: none;
        }
        .clp
        {
            border-collapse:collapse;
        }
        td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    </style>
    
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .edit
        {
        }
        .delete
        {
        }
        .docrmk
        {
            position:absolute;
            top: 200px;
            left: 255px;
            width: 400px;
           
        }
        .newchem
        {    
            position:absolute;       
            top: 300px;
            left: 400px;
            background-color: #B6B6B4;
            width: 400px;        
        }
         .newundoc
        {
            position:absolute;
            top: 200px;
            left: 400px;
            background-color: #B6B6B4;
            width: 450px;
            
        }
         .lblddl
        {
            position:absolute;
            top: 205px;
            left: 405px;
            width: 70px;
        }    
        .mrddl
        {
            position:absolute;
            top: 205px;
            left: 500px;
            width: 70px;
            
        }
        .pnladd
        {
            position:absolute;
            top: 220px;
            left: 280px;
            background-color: #B6B6B4;
            width: 350px;
            
        }
        .customFooter
        {
            position: fixed;
            bottom: 0;
            right: 0;
            left: 0;
            background-color: #CCCCCC;
        }
         .lblinfo
        {
             border :0px;
             background-color:transparent;
            font-size : 32px;
            font-family :TimesNewRoman; 
            color : Red;
        }
         .Footer
        {
            position:fixed;
            bottom: 0;
            right: 0;
            left: 538px;
            background-color: #CCCCCC;
        }
        .DropDownListCssClass
        {
            color:#000000;
            font-family:Verdana;    
            font-size:xx-small;
            border-top-style :groove;
            border-right-style :groove;
            border-left-style :groove;
            border-bottom-style :groove;
            padding:1px 3px  0.2em;
            height:24px;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
         
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
       
            z-index: 999;
        }
         .completionList {
        border:solid 1px Gray;
        margin:0px;
        padding:3px;
        width:140px;
        overflow:auto;
        background-color: #FFFFFF;      
        } 
        .listItem {
        color: #191919;
        width:200px;
        max-height:100px;
        overflow:auto;
        } 

        .itemHighlighted {
        background-color: #ADD6FF;        
         width:200px;
        }
        #pnllst {
    position:relative;
    width:200px
}
#PnlCheList {
    position:relative;
    width:200px
}
#UnPnl {
    position:relative;
    width:200px
}

#txtPnlChe{
    width:200px;
}

#ImageButton5{
    position:absolute;
    right:2px;
    top:2px;
    height:18px;
}


#Untxt_Dr{
    width:200px;
}

#ImageButton6{
    position:absolute;
    right:2px;
    top:2px;
    height:18px;
}

#txtListDR{
    width:200px;
}

#imgListDR{
    position:absolute;
    right:2px;
    top:2px;
    height:18px;
}

  .mycheckbox label 
{  
    padding-left: 5px; 
}
.borderdr
{
    border-style:solid ;
    border-width:thin;
    
}
    </style>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/gridviewScroll.min.js"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

  <script type="text/javascript">

      var message = "Right Clicking not allowed!";
      function clickIE4() {
          if (event.button == 2) {
              alert(message);
              return false;
          }
        
      }
      function clickNS4(e) {
          if (document.layers || document.getElementById && !document.all) {
              if (e.which == 2 || e.which == 3) {
                  alert(message);
                  return false;
              }
             
          }
      }

      if (document.layers) {
          document.captureEvents(Event.MOUSEDOWN);
          document.onmousedown = clickNS4;
      }
      else if (document.all && !document.getElementById) {
          document.onmousedown = clickIE4;
      }
      document.oncontextmenu = new Function("return false");
    
</script>
   

</head>
<body >
    <form id="form1" runat="server">
    <script type="text/javascript">

        function confirm_Save() {

            if (confirm('Do you want to save DCR?')) {
                if (confirm('Are you sure?')) {
                    ShowProgress();
                    return true;

                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        function confirm_Clear() {
            if (confirm('Do you want to Clear DCR?')) {
                if (confirm('Are you sure?')) {
                    ShowProgress();
                    return true;

                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        function confirm_Submit_final() {
            var nodata = true;
            var hdnfwind = document.getElementById('<%=hdnfwind.ClientID%>');

            if (hdnfwind.value == "F") {
                var gvDCR = document.getElementById('<%=gvDCR.ClientID%>');
                if (gvDCR != null) {
                    if (gvDCR.rows.length > 0)
                        nodata = false;
                }
                if (nodata == true) {
                    var grdChem = document.getElementById('<%=grdChem.ClientID%>');
                    if (grdChem != null) {
                        if (grdChem.rows.length > 0)
                            nodata = false;
                    }
                }
                if (nodata == true) {
                    var GridStk = document.getElementById('<%=GridStk.ClientID%>');
                    if (GridStk != null) {
                        if (GridStk.rows.length > 0)
                            nodata = false;
                    }
                }
                if (nodata == true) {
                    var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
                    if (grdUnLstDR != null) {
                        if (grdUnLstDR.rows.length > 0)
                            nodata = false;
                    }
                }
                if (nodata == true) {
                    var GridHospital = document.getElementById('<%=GridHospital.ClientID%>');
                    if (GridHospital != null) {
                        if (GridHospital.rows.length > 0)
                            nodata = false;
                    }
                }
                var hdndocedit = document.getElementById("<%=hdndocedit.ClientID%>");
                var hdncheedit = document.getElementById("<%=hdncheedit.ClientID%>");
                var hdnstkedit = document.getElementById("<%=hdnstkedit.ClientID%>");
                var hdnundocedit = document.getElementById("<%=hdnundocedit.ClientID%>");
                var hdnhosedit = document.getElementById("<%=hdnhosedit.ClientID%>");
                var hdnnewpnlchem = document.getElementById("<%=hdnnewpnlchem.ClientID%>");
                var hdnNewUnDoc = document.getElementById("<%=hdnNewUnDoc.ClientID%>");

                if (hdndocedit.value != '') {
                    alert("Click Go Button");
                    return false;
                }
                else if (hdncheedit.value != '') {

                    alert("Click Go Button");
                    return false;
                }
                else if (hdnstkedit.value != '') {

                    alert("Click Go Button");
                    return false;
                }
                else if (hdnundocedit.value != '') {

                    alert("Click Go Button");
                    return false;
                }
                else if (hdnhosedit.value != '') {

                    alert("Click Go Button");
                    return false;
                }
                else if (hdnnewpnlchem.value != '') {

                    alert("Click Done Button");
                    return false;
                }
                else if (hdnNewUnDoc.value != '') {

                    alert("Click Done Button");
                    return false;
                }

            }
            else {
                var sval = document.getElementById("<%=ddlWorkType.ClientID%>").value;
                if (sval == "0") {
                    alert("Select WorkType");
                    return false;
                }
                else {
                    nodata = false;
                }
            }
            if (nodata == true) {
                alert("Enter Call Details");
                return false;
            }
            else {
                var butgreen = document.getElementById('pnlTab1');
                    butgreen.style.display = "none";
                    butgreen.style.visibility = "hidden";
                    return true;

//                if (confirm('Do you want to Submit DCR?')) {
//                    if (confirm('Are you sure?')) {

//                        var butgreen = document.getElementById('pnlTab1');
//                        butgreen.style.display = "none";
//                        butgreen.style.visibility = "hidden";
//                        return true;
//                    }
//                    else {
//                        return false;
//                    }
//                }
//                else {
//                    return false;
//                }
            }
        }
        function confirm_Submit() {
            var nodata = true;
            var hdnfwind = document.getElementById('<%=hdnfwind.ClientID%>');
           
        if(hdnfwind.value == "F")
        {
            var gvDCR = document.getElementById('<%=gvDCR.ClientID%>');
            if (gvDCR != null) {
                if (gvDCR.rows.length > 0)
                    nodata = false;
            }
            if (nodata == true) {
                var grdChem = document.getElementById('<%=grdChem.ClientID%>');
                if (grdChem != null) {
                    if (grdChem.rows.length > 0)
                        nodata = false;
                }
            }
            if (nodata == true) {
                var GridStk = document.getElementById('<%=GridStk.ClientID%>');
                if (GridStk != null) {
                if (GridStk.rows.length > 0)
                    nodata = false;
               }
            }
            if (nodata == true){
                var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
                if (grdUnLstDR != null) {
                if (grdUnLstDR.rows.length > 0)
                    nodata = false;
                 }
            }
            if (nodata == true) {
                var GridHospital = document.getElementById('<%=GridHospital.ClientID%>');
                if (GridHospital != null) {
                    if (GridHospital.rows.length > 0)
                        nodata = false;
                }
            }
            var hdndocedit = document.getElementById("<%=hdndocedit.ClientID%>");
            var hdncheedit = document.getElementById("<%=hdncheedit.ClientID%>");
            var hdnstkedit = document.getElementById("<%=hdnstkedit.ClientID%>");
            var hdnundocedit = document.getElementById("<%=hdnundocedit.ClientID%>");
            var hdnhosedit = document.getElementById("<%=hdnhosedit.ClientID%>");
            var hdnnewpnlchem = document.getElementById("<%=hdnnewpnlchem.ClientID%>");
            var hdnNewUnDoc = document.getElementById("<%=hdnNewUnDoc.ClientID%>");

            if (hdndocedit.value != '') {
                alert("Click Go Button");
                return false;
            }
            else if (hdncheedit.value != '') {
             
                alert("Click Go Button");
                return false;
            }
            else if (hdnstkedit.value != '') {
             
                alert("Click Go Button");
                return false;
            }
            else if (hdnundocedit.value != '') {
             
                alert("Click Go Button");
                return false;
            }
            else if (hdnhosedit.value != '') {
             
                alert("Click Go Button");
                return false;
            }
            else if (hdnnewpnlchem.value != '') {
             
                alert("Click Done Button");
                return false;
            }
            else if (hdnNewUnDoc.value != '') {
             
                alert("Click Done Button");
                return false;
            }

        }
        else
        {   
          nodata = false;
        }
        if (nodata == true) {
            alert("Enter Call Details");
            return false;
        }
        else {
            
            if (confirm('Do you want to Submit DCR?')) {
                if (confirm('Are you sure?')) {
                    ShowProgress();
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
     }

        function openleave(leavedate) {
            window.open("../LeaveForm.aspx?LeaveFrom=" + leavedate,
                 "ModalPopUp",
                "toolbar=no," +
                "scrollbars=yes," +
                "location=no," +
                "statusbar=no," +
                "menubar=no," +
                "addressbar=no," +
                "resizable=yes," +
                "width=700," +
                "height=500," +
                "left = 300," +
                "top = 400"
                );
         }
        function HideList() {
            var pnlsrc = document.getElementById('pnlList');
            pnlsrc.style.display = "none";
            pnlsrc.style.visibility = "hidden";
        }
        function ShowListDR() {
            var pnlList = document.getElementById('pnlList');
            if (pnlList.style.display == "block") {
                pnlList.style.display = "none";
                pnlList.style.visibility = "hidden";
                return false;
            }
            else {
                pnlList.style.display = "block";
                pnlList.style.visibility = "visible";
                return false;
            }
        }

        function CheHide() {
            var pnlsrc = document.getElementById('PChe');
            pnlsrc.style.display = "none";
            pnlsrc.style.visibility = "hidden";
        }

        function ShowChe() {
            var pnlList = document.getElementById('PChe');
            if (pnlList.style.display == "block") {
                pnlList.style.display = "none";
                pnlList.style.visibility = "hidden";
                return false;
            }
            else {
                pnlList.style.display = "block";
                pnlList.style.visibility = "visible";
                return false;
            }

        }

        function HideUnDrList() {         
           var pnlsrc = document.getElementById('UnListPnl');
           pnlsrc.style.display = "none";
           pnlsrc.style.visibility = "hidden";
        }

        function ShowUnDR() {
            var pnlList = document.getElementById('UnListPnl');
            if (pnlList.style.display == "block") {
                pnlList.style.display = "none";
                pnlList.style.visibility = "hidden";
                return false;
            }
            else {
                pnlList.style.display = "block";
                pnlList.style.visibility = "visible";
                return false;
            }
        }

        function HideRemarks() {
            var pnlRemarks = document.getElementById('pnlRemarks');
            var hdnRemarks = document.getElementById('hdnRemarks');
            pnlRemarks.style.display = "none";
            pnlRemarks.style.visibility = "hidden";

            return false;
        }

        function hideProduct() {
            var pnlProd = document.getElementById('pnlProduct');
            var hidProdClose = document.getElementById('hidProdClose');
            pnlProd.style.display = "none";
            pnlProd.style.visibility = "hidden";
            hidProdClose.value = "1";
            //return false;
        }
        function hideGift() {
            var pnlGift = document.getElementById('pnlGift');
            var hidGiftClose = document.getElementById('hidGiftClose');
            pnlGift.style.display = "none";
            pnlGift.style.visibility = "hidden";

            hidGiftClose.value = "1";
           // return false;
        }
        function hideUnlstProduct() {
          
            var pnlProduct_Unlst = document.getElementById('pnlProduct_Unlst');
            var hidUnlstProdClose = document.getElementById('hidUnlstProdClose');
            pnlProduct_Unlst.style.display = "none";
            pnlProduct_Unlst.style.visibility = "hidden";

            hidUnlstProdClose.value = "1";
           // return false;
        }
        function hideUnlstGift() {
           
            var pnlGiftUnlst = document.getElementById('pnlGiftUnlst');
            var hdnUnLstgift = document.getElementById('hdnUnLstgift');
            pnlGiftUnlst.style.display = "none";
            pnlGiftUnlst.style.visibility = "hidden";
            hdnUnLstgift.value = "1";
           // return false;
        }
        function HideChem() {
            enablepanel();
            var cheedit = document.getElementById("<%=hdncheedit.ClientID %>").value;
            if (cheedit != '') {
                var grdChem = document.getElementById('<%=grdChem.ClientID%>');
                if (grdChem != null) {
                    if (grdChem.rows.length > 0) {
                        for (i = 0; i < grdChem.rows.length; i++) {
                            grdChem.rows[i].style.backgroundColor = '#ffffff';
                        }
                    }
                }
            }
            var PnlChem = document.getElementById("<%=PnlChem.ClientID%>");
            PnlChem.style.display = "none";
            PnlChem.style.visibility = "hidden";
            PnlChem.className = "newchem";
           
            hdnChe.value = "1";
            return false;
     
       }
       function HideUnlst() {
           enablepanel();
           var undocedit = document.getElementById("<%=hdnundocedit.ClientID %>").value;
           if (undocedit != '') {
               var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
               if (grdUnLstDR != null) {
                   if (grdUnLstDR.rows.length > 0) {
                       for (i = 0; i < grdUnLstDR.rows.length; i++) {
                           grdUnLstDR.rows[i].style.backgroundColor = '#ffffff';
                       }
                   }
               }
           }
           var NPnlUnLst = document.getElementById("<%=NPnlUnLst.ClientID%>");
           NPnlUnLst.style.display = "none";
           NPnlUnLst.style.visibility = "hidden";
           NPnlUnLst.className = "newundoc";
           hdnUnlst.value = "1";
           return false;
        }

        function Check(row, event, sname) {
            if (event.button == 0) {

                var pnlRemarks = document.getElementById('pnlRemarks');
                pnlRemarks.style.display = "block";
                pnlRemarks.style.visibility = "visible";
                var lblDRRemarks = document.getElementById("<%=lblDR_Name_Remarks.ClientID%>");

                lblDRRemarks.value = sname;

                return false;
            }
        }

        function ShowHideList() {

            var pnlLstSF = document.getElementById('pnlLstSF');
            //alert('ok' + pnlLstSF);
            if (pnlLstSF.style.display == "block") {
                pnlLstSF.style.display = "none";
                return false;
            }
            else {
                pnlLstSF.style.display = "block";
                pnlLstSF.style.visibility = "visible";
                return false;
            }
        }

        function ShowHide_WorkWithHos() {
            var pnlLstSF = document.getElementById('Panel6');

            if (pnlLstSF.style.display == "block") {

                pnlLstSF.style.display = "none";
                return false;
            }
            else {

                pnlLstSF.style.display = "block";
                pnlLstSF.style.visibility = "visible";
                return false;
            }
        }
        function ShowHideListPanel(panelId, currow) {

            var cgrid = document.getElementById(panelId);
            //alert('ok' + pnlLstSF + ':' + panelId);
            //alert('2:' + currow);
            var ccol = cgrid.rows[currow].cells[4];
            alert('ccol :' + ccol);
            var FirstControl = ccol.childNodes[3];
            alert('FirstControl :' + FirstControl);

            alert('Final: ' + document.getElementById('gvDCR_ctl0' + currow + '_chkFieldForce_Edit'));

            //alert(pnlLstSF.style.display);
            //          var posx = 0;
            //          var posy = 0;
            //          var orgObject = windowc.event;
            //          if (orgObject.pageX || orgObject.pageY) {
            //              posx = orgObject.pageX;
            //              posy = orgObject.pageY;
            //          }
            //          else if (orgObject.clientX || orgObject.clientY) {
            //              posx = orgObject.clientX + document.body.scrollLeft;
            //              posy = orgObject.clientY + document.body.scrollTop;
            //          }

            //          //style="left:181px; top:89px; position:absolute; visibility:hidden;"

            //          pnlLstSF.style.top = posy;
            //          pnlLstSF.style.left = posx;
            //          pnlLstSF.style.position = "absolute";
            //           
            if (pnlLstSF.style.display == "block") {
                pnlLstSF.style.display = "none";
                return false;
            }
            else {
                pnlLstSF.style.display = "block";
                pnlLstSF.style.visibility = "visible";
                return false;
            }

            pnlLstSF.style.display = "block";
            pnlLstSF.style.visibility = "visible";

        }

        function ShowHide_WorkWith(pnl_ww) {

            var pnl = document.getElementById(pnl_ww);

            if (pnl.style.display == "block") {

                pnl.style.display = "none";
                return false;
            }
            else {

                pnl.style.display = "block";
                pnl.style.visibility = "visible";
                return false;
            }
        }

        function hidepanel(pnlcntrl) {
            var pnl = document.getElementById(pnlcntrl);
            pnl.style.display = "none";
            pnl.style.visibility = "hidden";
        }
        function hidepanel(txtcntrl, pnlcntrl) {

            var pnl = document.getElementById(pnlcntrl);
            var txtMGR = document.getElementById(txtcntrl);



            if (txtMGR.value != '') {
                pnl.style.display = 'none';
                return false;
            }
        }

        function LoadFieldForce() {

            var chkBox = document.getElementById("<%=chkFieldForce.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtFieldForce.ClientID%>");
            var txtSFCode = document.getElementById("<%=txtSFCode.ClientID%>");

            var counter = 0;
            var cur_workwith = "";

            objTextBox.value = "";
            txtSFCode.value = "";
            checkbox[0].checked = true; 
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {

                    counter = counter + 1;
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        txtSFCode.value = "SELF" + ", "; //chkBoxText[0].innerHTML;
                    }
                    else {
                        cur_workwith = chkBoxText[0].innerHTML;
                        if (cur_workwith.length > 10) {
                            cur_workwith = cur_workwith.substr(0, 9);
                        }
                        txtSFCode.value = txtSFCode.value + cur_workwith + ", ";

                        //txtSFCode.value = txtSFCode.value + ", " + chkBoxText[0].innerHTML;
                        //txtWWCode.value = txtWWCode.value + ", " + chkBoxText[1].innerHTML;
                    }
                    objTextBox.value = counter;
                }
            }
        }

        function LoadFieldForceNewChem() {
            var chkBox = document.getElementById("<%=ChkChem.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtChe.ClientID%>");
         
          
            var cur_workwith = "";

            objTextBox.value = "";
            checkbox[0].checked = true; 
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        objTextBox.value = "SELF" + ", "; //chkBoxText[0].innerHTML;
                    }
                    else {
                        cur_workwith = chkBoxText[0].innerHTML;
                        if (cur_workwith.length > 10) {
                            cur_workwith = cur_workwith.substr(0, 9);
                        }
                        objTextBox.value = objTextBox.value + cur_workwith + ", ";

                    }                 
                }
            }
            return false;
        }
        function LoadFieldForceNewUn() {
            var chkBox = document.getElementById("<%=ChkUn.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtUn.ClientID%>");

            var cur_workwith = "";
            checkbox[0].checked = true; 
            objTextBox.value = "";
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                   var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        objTextBox.value = "SELF" + ", "; //chkBoxText[0].innerHTML;
                    }
                    else {
                        cur_workwith = chkBoxText[0].innerHTML;
                        if (cur_workwith.length > 10) {
                            cur_workwith = cur_workwith.substr(0, 9);
                        }
                        objTextBox.value = objTextBox.value + cur_workwith + ", ";

                    }
                }
            }
            return false;
        }
        function LoadChemists() {
            var chkBox = document.getElementById("<%=chkChemWW.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtChemWW.ClientID%>");
            var txtSFCode = document.getElementById("<%=txtChemWWSF.ClientID%>");
           
            var hdnChemWW_Name= document.getElementById("<%=hdnChemWW_Name.ClientID%>");

            var counter = 0;
            var cur_workwith = "";
            objTextBox.value = "";
            txtSFCode.value = "";
            checkbox[0].checked = true; 
            hdnChemWW_Name.value = "";

            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter = counter + 1;
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');           
                    if (objTextBox.value == "") {
                        txtSFCode.value = "SELF" + ", "; //chkBoxText[0].innerHTML;
                        hdnChemWW_Name.value = "SELF" + ", ";
                    }
                    else {

                        cur_workwith = chkBoxText[0].innerHTML;

                        if (cur_workwith.length > 10) {
                            cur_workwith = cur_workwith.substr(0, 9);
                        }
                        txtSFCode.value = txtSFCode.value + cur_workwith + ", ";
                   
                        hdnChemWW_Name.value = hdnChemWW_Name.value + chkBoxText[0].innerHTML + ",";

                    }
                    objTextBox.value = counter;
                }
            }
           
        }
      
        function LoadUnListedDR() {
            var chkBox = document.getElementById("<%=chkUnLstDR.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtUnLstDR_SF.ClientID%>");
            var txtSFCode = document.getElementById("<%=txtUnLstDR_WW.ClientID%>");

            var counter = 0;
            var cur_workwith = "";
            objTextBox.value = "";
            txtSFCode.value = "";
            checkbox[0].checked = true; 
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter = counter + 1;
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        txtSFCode.value = "SELF" + ", "; //chkBoxText[0].innerHTML;
                    }
                    else {
                        cur_workwith = chkBoxText[0].innerHTML;
                        if (cur_workwith.length > 10) {
                            cur_workwith = cur_workwith.substr(0, 9);
                        }
                        txtSFCode.value = txtSFCode.value + cur_workwith + ", ";
                        //txtSFCode.value = txtSFCode.value + ", " + chkBoxText[0].innerHTML;
                        //txtWWCode.value = txtWWCode.value + ", " + chkBoxText[1].innerHTML;
                    }
                    objTextBox.value = counter;
                }
            }
        }


        function LoadStockiest() {
            var chkBox = document.getElementById("<%=StkChkBox.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtStk.ClientID%>");
            var txtSFCode = document.getElementById("<%=HdnStk.ClientID%>");
            checkbox[0].checked = true; 
            var counter = 0;
            objTextBox.value = "";
            txtSFCode.value = "";
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter = counter + 1;
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        txtSFCode.value = "SELF" + ", ";  //chkBoxText[0].innerHTML;
                    }
                    else {
                        cur_workwith = chkBoxText[0].innerHTML;
                        if (cur_workwith.length > 10) {
                            cur_workwith = cur_workwith.substr(0, 9);
                        }
                        txtSFCode.value = txtSFCode.value + cur_workwith + ", ";
                        //txtSFCode.value = txtSFCode.value + ", " + chkBoxText[0].innerHTML;
                        //txtWWCode.value = txtWWCode.value + ", " + chkBoxText[1].innerHTML;
                    }
                    objTextBox.value = counter;
                }
            }
        }

        //
        function LoadHospital() {
            var chkBox = document.getElementById("<%=ChkHos.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtHos.ClientID%>");
            var txtSFCode = document.getElementById("<%=HdnHos.ClientID%>");
            checkbox[0].checked = true; 
            var counter = 0;
            var cur_workwith = "";
            objTextBox.value = "";
            txtSFCode.value = "";
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter = counter + 1;
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        txtSFCode.value = "SELF" + ", "; // chkBoxText[0].innerHTML;
                    }
                    else {
                        cur_workwith = chkBoxText[0].innerHTML;
                        if (cur_workwith.length > 10) {
                            cur_workwith = cur_workwith.substr(0, 9);
                        }
                        txtSFCode.value = txtSFCode.value + cur_workwith + ", ";
                        //txtSFCode.value = txtSFCode.value + ", " + chkBoxText[0].innerHTML;
                        //txtWWCode.value = txtWWCode.value + ", " + chkBoxText[1].innerHTML;
                    }
                    objTextBox.value = counter;
                }
            }
        }

        function Load_WorkWith(chkFieldForce, txtFieldForce, txtSFCode) {

            var chkBox = document.getElementById(chkFieldForce);
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById(txtFieldForce);
            var txtSFCode = document.getElementById(txtSFCode);
            checkbox[0].checked = true; 
            var counter = 0;
            var cur_workwith = "";
            objTextBox.value = "";
            txtSFCode.value = "";
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter = counter + 1;
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        txtSFCode.value = "SELF" + ", "; // chkBoxText[0].innerHTML;
                    }
                    else {
                        cur_workwith = chkBoxText[0].innerHTML;
                        if (cur_workwith.length > 10) {
                            cur_workwith = cur_workwith.substr(0, 9);
                        }
                        txtSFCode.value = txtSFCode.value + cur_workwith + ", ";
                        //txtSFCode.value = txtSFCode.value + ", " + chkBoxText[0].innerHTML;
                    }
                    objTextBox.value = counter;
                }
            }
        }

        function PopulateInfo() {
            var lblcntrl = document.getElementById("<%=lblInfo.ClientID%>");
            lblcntrl.value = "Select SDP";
            return true;
        }

        function isValidate() {
            var cval = true;
            var inp = false;
            var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (j = 2; j < grid.rows.length + 1;j++) {
                        var cnt = 0;
                        var index = '';

                        if (j.toString().length == 1) {
                            index = cnt.toString() + j.toString();
                        }
                        else {
                            index = j.toString();
                        }
                        var sess_m_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_sess_m_dcr');
                        var time_m_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_time_m_dcr');
                        var prod_mand_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_prod_mand_dcr');
                        var prod_qty_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_prod_qty_dcr');
                        var max_doc_dcr_count = document.getElementById('GrdAdmStp_ctl' + index + '_max_doc_dcr_count');
                        var remarks_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_remarks_dcr');
                        
                        var docedit = document.getElementById("<%=hdndocedit.ClientID %>").value;
                        // Check max doc
                        if (docedit.length == 0) {
                            var gvDCR = document.getElementById('<%=gvDCR.ClientID%>');
                            if (gvDCR != null) {
                                if (gvDCR.rows.length > 0) {
                                    if (gvDCR.rows.length - 1 == max_doc_dcr_count.innerHTML) {
                                        alert('Cannot enter more than ' + max_doc_dcr_count.innerHTML + ' Listed Doctors');
                                        cval = false;
                                        break;
                                    }
                                }
                            }
                        }
                        if (sess_m_dcr.innerHTML == "1") {
                            var index_ses = document.getElementById("<%=ddlSes.ClientID%>").selectedIndex;
                            var sval_ses = document.getElementById("<%=ddlSes.ClientID%>").value;
                            if (sval_ses == "0") {
                                alert("Select Session");
                                cval = false;
                                document.getElementById("<%=ddlSes.ClientID%>").focus();
                                break;
                            }
                        }
                        if (time_m_dcr.innerHTML == "1") {
                            var index_time = document.getElementById("<%=ddlTime.ClientID%>").selectedIndex;
                            var sval_time = document.getElementById("<%=ddlTime.ClientID%>").value;
                            if (index_time == "0") {
                                alert("Select Time");
                                cval = false;
                                document.getElementById("<%=ddlTime.ClientID%>").focus();
                                break;
                            }
                        }
                        var txtListDR = document.getElementById("<%=txtListDR.ClientID%>").value;
                        var ListDR = document.getElementById("<%=txtListDR.ClientID%>");
                        var txtListDRCode = document.getElementById("<%=txtListDRCode.ClientID%>");
                        var hdnValue = document.getElementById("<%=hdnValue.ClientID%>");

                        if (txtListDR.length == 0) {
                            alert("Select Listed Doctor");
                            cval = false;
                            document.getElementById("<%=txtListDR.ClientID%>").focus();
                            break;
                        }
                        else {
                           
                            var dpt = document.getElementById("lstListDR");
                            if (dpt.selectedIndex >= 0) {
                                ListDR.value = dpt.options[dpt.selectedIndex].text;
                                txtListDRCode.value = dpt.options[dpt.selectedIndex].value;
                            }
                            else if (hdnValue.value != '') {
                                var lstdoc = document.getElementById("<%=lstListDR.ClientID%>");
                                for (k = 0; k < lstdoc.options.length; k++) {
                                    if (lstdoc.options[k].value == hdnValue.value) {
                                        ListDR.value = lstdoc.options[k].text;
                                        txtListDRCode.value = lstdoc.options[k].value;
                                        break;
                                    }
                                }  
                            }
                            else {
                              
                                var nodoc = true;
                                var docarray = txtListDR.split('-');

                                var lstdoc = document.getElementById("<%=lstListDR.ClientID%>");
                                if (lstChe.options.length > 0) {
                                    for (k = 0; k < lstdoc.options.length; k++) {
                                        var listdocarray = lstdoc.options[k].text.split('-');
                                        if (docarray[0].toUpperCase().trim() == listdocarray[0].toUpperCase().trim()) {
                                            ListDR.value = lstdoc.options[k].text;
                                            txtListDRCode.value = lstdoc.options[k].value;
                                            nodoc = false;
                                            break;
                                        }
                                    }
                                    if (nodoc == true) {
                                        alert('Listed Doctor does not exists!!');
                                        cval = false;
                                        break;
                                    }
                                }
                                else 
                                {
                                    alert('Listed Doctor does not exists!!');
                                    cval = false;
                                    break;
                                }
                            }
                           
                            var dupdoc = false;
                            var gvDCR = document.getElementById('<%=gvDCR.ClientID%>');
                            if (gvDCR != null) {
                                if (gvDCR.rows.length > 0) {
                                    for (i = 2; i < gvDCR.rows.length + 1; i++) {
                                        var cnt = 0;
                                        var index = '';

                                        if (i.toString().length == 1) {
                                            index = cnt.toString() + i.toString();
                                        }
                                        else {
                                            index = i.toString();
                                        }
                                        var drname = document.getElementById('gvDCR_ctl' + index + '_lblDR');
                                        
                                        var chkedit = index - 1;
                                        if (docedit.length == 0) {
                                            if (txtListDR.toUpperCase().trim() == drname.innerHTML.toUpperCase()) {
                                                dupdoc = true;
                                                break;
                                            }
                                        }
                                        else {

                                            if ((txtListDR.toUpperCase().trim() == drname.innerHTML.toUpperCase()) && (docedit != chkedit)) {
                                                dupdoc = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (dupdoc == true) {
                                        alert('Duplicate Listed Doctor!!');
                                        cval = false;
                                        break;
                                    }

                                }
                            }
                        }

                      
                        if (prod_mand_dcr.innerHTML > 0) {
                            if (prod_mand_dcr.innerHTML >= 1) {
                                var ddlProd1_index = document.getElementById("<%=ddlProd1.ClientID%>").selectedIndex;
                                var ddlProd1_val = document.getElementById("<%=ddlProd1.ClientID%>").value;
                                if (ddlProd1_val == "0") {
                                    alert("Select Product");
                                    cval = false;
                                    document.getElementById("<%=ddlProd1.ClientID%>").focus();
                                    break;
                                }
                                if (prod_qty_dcr.innerHTML == 0) {
                                    var txtProd1 = document.getElementById("<%=txtProd1.ClientID%>").value;
                                    if (txtProd1.length == 0) {
                                        alert("Enter Sample Qty");
                                        cval = false;
                                        document.getElementById("<%=txtProd1.ClientID%>").focus();
                                        break;
                                    }
                                }
                             }
                            if (prod_mand_dcr.innerHTML >= 2) {
                                var ddlProd2_index = document.getElementById("<%=ddlProd2.ClientID%>").selectedIndex;
                                var dddlProd2_val = document.getElementById("<%=ddlProd2.ClientID%>").value;
                                if (dddlProd2_val == "0") {
                                    alert("Select Product");
                                    cval = false;
                                    document.getElementById("<%=ddlProd2.ClientID%>").focus();
                                    break;
                                }

                                if (prod_qty_dcr.innerHTML == 0) {
                                    var txtProd2 = document.getElementById("<%=txtProd2.ClientID%>").value;
                                    if (txtProd2.length == 0) {
                                        alert("Enter Sample Qty");
                                        cval = false;
                                        document.getElementById("<%=txtProd2.ClientID%>").focus();
                                        break;
                                    }
                                }
                             }
                            if (prod_mand_dcr.innerHTML >= 3) {
                                var ddlProd3_index = document.getElementById("<%=ddlProd3.ClientID%>").selectedIndex;
                                var dddlProd3_val = document.getElementById("<%=ddlProd3.ClientID%>").value;
                                if (dddlProd3_val == "0") {
                                    alert("Select Product");
                                    cval = false;
                                    document.getElementById("<%=ddlProd3.ClientID%>").focus();
                                    break;
                                }

                                if (prod_qty_dcr.innerHTML == 0) {
                                    var txtProd3 = document.getElementById("<%=txtProd3.ClientID%>").value;
                                    if (txtProd3.length == 0) {
                                        alert("Enter Sample Qty");
                                        cval = false;
                                        document.getElementById("<%=txtProd3.ClientID%>").focus();
                                        break;
                                    }
                                }
                             }
                             //Dup check
                              var ddlProd1_val = document.getElementById("<%=ddlProd1.ClientID%>").value;
                              var dddlProd2_val = document.getElementById("<%=ddlProd2.ClientID%>").value;
                              var dddlProd3_val = document.getElementById("<%=ddlProd3.ClientID%>").value;
                              if(ddlProd1_val != 0 && dddlProd2_val != 0)
                              {
                                 if(ddlProd1_val == dddlProd2_val ){
                                        alert("Duplicate Product");
                                        cval = false;
                                        break;
                                        }
                              }
                              if(ddlProd1_val != 0 && dddlProd3_val != 0)
                              {
                                 if(ddlProd1_val == dddlProd3_val ){
                                        alert("Duplicate Product");
                                        cval = false;
                                        break;
                                        }
                               }
                               if (dddlProd2_val != 0 && dddlProd3_val != 0) {
                                   if (dddlProd2_val == dddlProd3_val) {
                                       alert("Duplicate Product");
                                       cval = false;
                                       break;
                                   }
                               }
                           } //end of  if (prod_mand_dcr > 0)
                           var ddlGift = document.getElementById("<%=ddlGift.ClientID%>").value;
                           if (ddlGift != 0) {
                               var txtGift = document.getElementById("<%=txtGift.ClientID%>").value;
                               if (txtGift.length == 0) {
                                   inp = true;
                                   cval = false;
                               }
                           }
                           if (cval == true) {
                               LoadFieldForce();
                               if (remarks_dcr.innerHTML == "1") {
                                   disablepanel();
                                   var pnlRemarks = document.getElementById("<%=pnlRemarks.ClientID%>");
                                   pnlRemarks.style.display = "block";
                                   pnlRemarks.style.visibility = "visible";
                                   pnlRemarks.className = "docrmk";
                                   document.getElementById("<%=lblDR_Name_Remarks.ClientID %>").value = document.getElementById("<%=txtListDR.ClientID%>").value;
                                   if (docedit.length == 0) {
                                       document.getElementById("<%=txtRemarks.ClientID %>").value = "";
                                   }
                                   cval = false;
                             }
                          }
                       } // end of for
                   } // end of if grid length
               } //end of grid null check    
               if (inp == true)
                   alert("Enter Input Qty");
               if (cval == true)
                   enablebut('btnListedDR', 'btnChemists', 'btnStockist', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
            return cval;
        }
        function isValidateProd() {
           
            var ddlProd1_val = document.getElementById("<%=ddlProd1.ClientID%>").value;
            var dddlProd2_val = document.getElementById("<%=ddlProd2.ClientID%>").value;
            var dddlProd3_val = document.getElementById("<%=ddlProd3.ClientID%>").value;
            if (ddlProd1_val != 0 && dddlProd2_val != 0 && dddlProd3_val != 0) {
                 return true;
            }
            else {
                alert("Select Products");
                return false;
            }
        }
        function isValidateGift() {
            var ddlGift = document.getElementById("<%=ddlGift.ClientID%>").value;

            if (ddlGift != 0) {
            
                return true;
            }
            else {
                alert("Select Input");
                return false;
            }
        }

        function isValidateUnProd() {

            var ddlProd1_val = document.getElementById("<%=ddlUnLstDR_Prod1.ClientID%>").value;
            var dddlProd2_val = document.getElementById("<%=ddlUnLstDR_Prod2.ClientID%>").value;
            var dddlProd3_val = document.getElementById("<%=ddlUnLstDR_Prod3.ClientID%>").value;
            if (ddlProd1_val != 0 && dddlProd2_val != 0 && dddlProd3_val != 0) {
                return true;
            }
            else {
                alert("Select Products");
                return false;
            }
        }
        function isValidateunGift() {
            var ddlGift = document.getElementById("<%=ddlUnLstDR_Gift.ClientID%>").value;

            if (ddlGift != 0) {

                return true;
            }
            else {
                alert("Select Input");
                return false;
            }
        }
        function GetSelectedRow_doc(row) {
                var rowData = row.parentNode.parentNode;
                var rowIndex = rowData.rowIndex;
                document.getElementById("<%=hdndocedit.ClientID %>").value = rowIndex;

                var docedit = rowIndex;
                var sess_dcr = '';
                var time_dcr = '';
                var prod_sel = '';
                var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
                if (grid != null) {
                     if (grid.rows.length > 0) {
                         for (k = 2; k < grid.rows.length + 1; k++) {
                             var cnt = 0;
                             var index = '';

                             if (k.toString().length == 1) {
                                 index = cnt.toString() + k.toString();
                             }
                             else {
                                 index = k.toString();
                             }
                             sess_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_sess_dcr');
                             time_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_time_dcr');
                             prod_sel = document.getElementById('GrdAdmStp_ctl' + index + '_prod_sel');
                         }
                     }
                 }
                var gvDCR = document.getElementById('<%=gvDCR.ClientID%>');
                gvDCR.rows[rowIndex].style.backgroundColor = "LimeGreen";
                if (gvDCR != null) {
                    if (gvDCR.rows.length > 0) {
                        for (k = 2; k < gvDCR.rows.length + 1; k++) {
                            var cnt = 0;
                            var index = '';

                            if (k.toString().length == 1) {
                                index = cnt.toString() + k.toString();
                            }
                            else {
                                index = k.toString();
                            }
                            var lblDR_Code = document.getElementById('gvDCR_ctl' + index + '_lblDR_Code');
                            var lblDR = document.getElementById('gvDCR_ctl' + index + '_lblDR');
                            var lblWorkWith = document.getElementById('gvDCR_ctl' + index + '_lblWorkWith');
                            var lblSess_Code = document.getElementById('gvDCR_ctl' + index + '_lblSess_Code');
                            var lblmin = document.getElementById('gvDCR_ctl' + index + '_lblmin');
                            var lblseconds = document.getElementById('gvDCR_ctl' + index + '_lblseconds');
                            var lblProd1_Code = document.getElementById('gvDCR_ctl' + index + '_lblProd1_Code');
                            var lblProd2_Code = document.getElementById('gvDCR_ctl' + index + '_lblProd2_Code');
                            var lblProd3_Code = document.getElementById('gvDCR_ctl' + index + '_lblProd3_Code');
                            var lblGift_Code = document.getElementById('gvDCR_ctl' + index + '_lblGift_Code');
                            var lblQty1 = document.getElementById('gvDCR_ctl' + index + '_lblQty1');
                            var lblQty2 = document.getElementById('gvDCR_ctl' + index + '_lblQty2');
                            var lblQty3 = document.getElementById('gvDCR_ctl' + index + '_lblQty3');
                            var lblGQty = document.getElementById('gvDCR_ctl' + index + '_lblGQty');
                            var lblremarks = document.getElementById('gvDCR_ctl' + index + '_lblremarks');
                       
                            var chkedit = index - 1;
                            if (docedit == chkedit) {
                             
                                    var txtListDR = document.getElementById("<%=txtListDR.ClientID%>");
                                    txtListDR.value = lblDR.innerHTML;

                                    var txtListDRCode = document.getElementById("<%=txtListDRCode.ClientID%>");
                                    txtListDRCode.value = lblDR_Code.innerHTML;

                                    var lstListDR = document.getElementById("<%=lstListDR.ClientID%>");

                                    for (var i = 0; i < lstListDR.options.length; i++) {
                                        if (lstListDR.options[i].value == lblDR_Code.innerHTML) {
                                            lstListDR.selectedIndex = i;
                                            break;
                                        }
                                    }
                                 
                                    if (sess_dcr.innerHTML == 0) {
                                        var ddlSes = document.getElementById("<%=ddlSes.ClientID%>");

                                        for (var i = 0; i < ddlSes.options.length; i++) {
                                            if (ddlSes.options[i].value == lblSess_Code.innerHTML) {
                                                ddlSes.selectedIndex = i;
                                                break;
                                            }
                                        }
                                    }
                                    if (time_dcr.innerHTML == 0) {
                                        var ddlTime = document.getElementById("<%=ddlTime.ClientID%>");

                                        for (var i = 0; i < ddlTime.options.length; i++) {
                                            if (ddlTime.options[i].value == lblmin.innerHTML) {
                                                ddlTime.selectedIndex = i;
                                                break;
                                            }
                                        }

                                        var ddlmin = document.getElementById("<%=ddlmin.ClientID%>");

                                        for (var i = 0; i < ddlmin.options.length; i++) {
                                            if (ddlmin.options[i].value == lblseconds.innerHTML) {
                                                ddlmin.selectedIndex = i;
                                                break;
                                            }
                                        }
                                    }
                                    if (prod_sel.innerHTML != 0) {
                                        if (prod_sel.innerHTML >= 1) {
                                            var ddlProd1 = document.getElementById("<%=ddlProd1.ClientID%>");

                                            for (var i = 0; i < ddlProd1.options.length; i++) {
                                                if (ddlProd1.options[i].value == lblProd1_Code.innerHTML) {
                                                    ddlProd1.selectedIndex = i;
                                                    break;
                                                }
                                            }
                                            var txtProd1 = document.getElementById("<%=txtProd1.ClientID%>");
                                            txtProd1.value = lblQty1.innerHTML;
                                        }
                                        if (prod_sel.innerHTML >= 2) {
                                            var ddlProd2 = document.getElementById("<%=ddlProd2.ClientID%>");

                                            for (var i = 0; i < ddlProd2.options.length; i++) {
                                                if (ddlProd2.options[i].value == lblProd2_Code.innerHTML) {
                                                    ddlProd2.selectedIndex = i;
                                                    break;
                                                }
                                            }

                                            var txtProd2 = document.getElementById("<%=txtProd2.ClientID%>");
                                            txtProd2.value = lblQty2.innerHTML;
                                        }
                                        if (prod_sel.innerHTML >= 3) {
                                            var ddlProd3 = document.getElementById("<%=ddlProd3.ClientID%>");

                                            for (var i = 0; i < ddlProd3.options.length; i++) {
                                                if (ddlProd3.options[i].value == lblProd3_Code.innerHTML) {
                                                    ddlProd3.selectedIndex = i;
                                                    break;
                                                }
                                            }
                                            var txtProd3 = document.getElementById("<%=txtProd3.ClientID%>");
                                            txtProd3.value = lblQty3.innerHTML;
                                        }
                                    }
                                    var ddlGift = document.getElementById("<%=ddlGift.ClientID%>");

                                    for (var i = 0; i < ddlGift.options.length; i++) {
                                        if (ddlGift.options[i].value == lblGift_Code.innerHTML) {
                                            ddlGift.selectedIndex = i;
                                            break;
                                        }
                                    }
                                    var txtGift = document.getElementById("<%=txtGift.ClientID%>");
                                    txtGift.value = lblGQty.innerHTML;
                                    
                                    var txtListDRCode = document.getElementById("<%=txtListDRCode.ClientID%>");
                                    txtListDRCode.value = lblDR_Code.innerHTML;
                                    
                                    var txtSFCode = document.getElementById("<%=txtSFCode.ClientID%>");
                                    txtSFCode.value = lblWorkWith.innerHTML;

                                    var txtRemarks = document.getElementById("<%=txtRemarks.ClientID%>");
                                    txtRemarks.value = lblremarks.innerHTML;

                                    var chkBox = document.getElementById("<%=chkFieldForce.ClientID%>")
                                    var checkbox = chkBox.getElementsByTagName("input");
                                    var objTextBox = document.getElementById("<%=txtFieldForce.ClientID%>")
                                    var counter = 0;
                                    objTextBox.value = "";
                                    for (var i = 0; i < checkbox.length; i++) {
                                        checkbox[i].checked = false;
                                    }
                                    var strarray = lblWorkWith.innerHTML.split(',');

                                    for (var j = 0; j < strarray.length; j++) {
                                        if (strarray[j].length > 0) {

                                            for (var i = 0; i < checkbox.length; i++) {
                                                var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                                                if (chkBoxText[0].innerHTML.substr(0, 9).trim() == strarray[j].trim()) {

                                                    checkbox[i].checked = true;
                                                    counter = counter + 1;
                                                    objTextBox.value = counter;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                               
                                break;
                            }
                        }
                    }
                }
                $(".edit").hide();
                $(".delete").hide();

                disablebut('btnListedDR', 'btnChemists', 'btnStockist', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
               
                return false;
        }
        function disablebut(high_but, blur_but1, blur_but2, blur_but3, blur_but4, blur_but5, blur_but6, blur_but7, blur_but8) {
              
            if (document.getElementById(high_but).style.display == "block")
                document.getElementById(high_but).disabled = false;
            if (document.getElementById(blur_but1).style.display == "block")
                document.getElementById(blur_but1).disabled = true;
          
            if (document.getElementById(blur_but2).style.display == "block")
                document.getElementById(blur_but2).disabled = true;
           
            if (document.getElementById(blur_but3).style.display == "block")
                document.getElementById(blur_but3).disabled = true;
                 
            if (document.getElementById(blur_but4).style.display == "block")
                document.getElementById(blur_but4).disabled = true;
            
            if (document.getElementById(blur_but5).style.display == "block")
                document.getElementById(blur_but5).disabled = true;
     
//            if (document.getElementById(blur_but6).style.display == "block")
//                document.getElementById(blur_but6).disabled = true;
           
            if (document.getElementById(blur_but7).style.display == "block")
                document.getElementById(blur_but7).disabled = true;
          
            if (document.getElementById(blur_but8).style.display == "block")
                document.getElementById(blur_but8).disabled = true;
           
               
        }
        function enablebut(high_but, blur_but1, blur_but2, blur_but3, blur_but4, blur_but5, blur_but6, blur_but7, blur_but8) {
            if (document.getElementById(high_but).style.display == "block")
                document.getElementById(high_but).disabled = false;
            if (document.getElementById(blur_but1).style.display == "block")
                document.getElementById(blur_but1).disabled = false;
            if (document.getElementById(blur_but2).style.display == "block")
                document.getElementById(blur_but2).disabled = false;
            if (document.getElementById(blur_but3).style.display == "block")
                document.getElementById(blur_but3).disabled = false;
            if (document.getElementById(blur_but4).style.display == "block")
                document.getElementById(blur_but4).disabled = false;
            if (document.getElementById(blur_but5).style.display == "block")
                document.getElementById(blur_but5).disabled = false;
//            if (document.getElementById(blur_but6).style.display == "block")
//                document.getElementById(blur_but6).disabled = false;
            if (document.getElementById(blur_but7).style.display == "block")
                document.getElementById(blur_but7).disabled = false;
            if (document.getElementById(blur_but8).style.display == "block")
                document.getElementById(blur_but8).disabled = false;
           
        }
        function GetSelectedRow_che(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            document.getElementById("<%=hdncheedit.ClientID %>").value = rowIndex;
            document.getElementById("<%=hdnnewpnlchem.ClientID %>").value = rowIndex;
            
            var cheedit = rowIndex;

            var grdChem = document.getElementById('<%=grdChem.ClientID%>');
            grdChem.rows[rowIndex].style.backgroundColor = "LimeGreen";
            if (grdChem != null) {
                if (grdChem.rows.length > 0) {
                    for (k = 2; k < grdChem.rows.length + 1; k++) {
                        var cnt = 0;
                        var index = '';

                        if (k.toString().length == 1) {
                            index = cnt.toString() + k.toString();
                        }
                        else {
                            index = k.toString();
                        }
                        var chename = document.getElementById('grdChem_ctl' + index + '_lblChemists');
                        var lblChem_Code = document.getElementById('grdChem_ctl' + index + '_lblChem_Code');
                        var lblPOBNo = document.getElementById('grdChem_ctl' + index + '_lblPOBNo');
                        var lblWW = document.getElementById('grdChem_ctl' + index + '_lblWW');
                        var lblTerr_Code = document.getElementById('grdChem_ctl' + index + '_lblTerr_Code');
                        var chename = document.getElementById('grdChem_ctl' + index + '_lblChemists');
                        var lblnew = document.getElementById('grdChem_ctl' + index + '_lblnew');
                        var lblsf_code = document.getElementById('grdChem_ctl' + index + '_lblsf_code');
                        var lblww_code = document.getElementById('grdChem_ctl' + index + '_lblww_code');
                        var chkedit = index - 1;
                        if (cheedit == chkedit) {
                            if (lblnew.innerHTML != "New") {
                               
                                var txtPnlChe = document.getElementById("<%=txtPnlChe.ClientID%>");
                                txtPnlChe.value = chename.innerHTML;

                                var txtPChe = document.getElementById("<%=txtPChe.ClientID%>");
                                txtPChe.value = lblChem_Code.innerHTML;

                                var lstChe = document.getElementById("<%=lstChe.ClientID%>");

                                for (var i = 0; i < lstChe.options.length; i++) {
                                    if (lstChe.options[i].innerHTML == lblChem_Code.innerHTML) {
                                        lstChe.selectedIndex = i;
                                        break;
                                    }
                                }
                                var txtPOBNo = document.getElementById("<%=txtPOBNo.ClientID%>");
                                txtPOBNo.value = lblPOBNo.innerHTML;
                                
                                var txtChemWWSF = document.getElementById("<%=txtChemWWSF.ClientID%>");
                                txtChemWWSF.value  = lblWW.innerHTML;

                                var chkBox = document.getElementById("<%=chkChemWW.ClientID%>")
                                var checkbox = chkBox.getElementsByTagName("input");
                                var objTextBox = document.getElementById("<%=txtChemWW.ClientID%>")
                                var counter = 0;
                                var cur_workwith = "";
                                objTextBox.value = "";
                                for (var i = 0; i < checkbox.length; i++){
                                    checkbox[i].checked = false;
                                }
                                var strarray = lblWW.innerHTML.split(',');

                                for (var j = 0; j < strarray.length; j++) {
                                    if (strarray[j].length > 0) {
                                      
                                        for (var i = 0; i < checkbox.length; i++) {
                                            var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                                            if (chkBoxText[0].innerHTML.substr(0, 9).trim() == strarray[j].trim()) {
                                               
                                                checkbox[i].checked = true;
                                                counter = counter + 1;
                                                objTextBox.value = counter;
                                                break;
                                            }
                                        }
                                    }
                                }
                                disablebut('btnChemists', 'btnListedDR', 'btnStockist', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
                            }
                            else 
                            {
                                disablepanel();

                                var hdnsftype = document.getElementById("<%=hdnsftype.ClientID%>");

                                if (hdnsftype.value == "2") {
                                    $('#lblcheMR').show();
                                    $('#ddlcheMR').show();

                                    var ddlcheMR = document.getElementById("<%=ddlcheMR.ClientID%>");

                                    for (var i = 0; i < ddlcheMR.options.length; i++) {
                                        if (ddlcheMR.options[i].value == lblsf_code.innerHTML) {
                                            ddlcheMR.selectedIndex = i;

                                            break;
                                        }
                                    }
                                    var ddlTerr = document.getElementById("<%=ddlTerr.ClientID%>");

                                    ddlTerr.options.length = 0;
                                    var ddlAllTerr = document.getElementById("<%=ddlAllTerr.ClientID%>");
                                    var ddlsfterr = document.getElementById("<%=ddlsfterr.ClientID%>");

                                    for (i = 0; i < ddlAllTerr.options.length; i++) {
                                        if ((ddlsfterr.options[i].text == lblsf_code.innerHTML) || (ddlsfterr.options[i].text == "ALL")) {
                                            var opt = document.createElement("option");
                                            opt.text = ddlAllTerr.options[i].text;
                                            opt.value = ddlAllTerr.options[i].value;
                                            ddlTerr.options.add(opt);
                                        }
                                    }
                                }
                                else {
                                    $('#lblcheMR').hide();
                                    $('#ddlcheMR').hide();
                                }

                                var NewtxtChem = document.getElementById("<%=NewtxtChem.ClientID%>");
                                NewtxtChem.value = chename.innerHTML;

                                var ddlTerr = document.getElementById("<%=ddlTerr.ClientID%>");

                                for (var i = 0; i < ddlTerr.options.length; i++) {
                                    if (ddlTerr.options[i].value == lblTerr_Code.innerHTML) {
                                        ddlTerr.selectedIndex = i;
                                        break;
                                    }
                                }
                              
                                var txtChe = document.getElementById("<%=txtChe.ClientID%>");
                                txtChe.value = lblWW.innerHTML;

                                var chkBox = document.getElementById("<%=ChkChem.ClientID%>")
                                var checkbox = chkBox.getElementsByTagName("input");

                                for (var i = 0; i < checkbox.length; i++) {
                                    checkbox[i].checked = false;
                                }
                                var strarray = lblWW.innerHTML.split(',');

                                for (var j = 0; j < strarray.length; j++) {
                                    if (strarray[j].length > 0) {
                                        for (var i = 0; i < checkbox.length; i++) {
                                            var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                                            if (chkBoxText[0].innerHTML.substr(0, 9).trim() == strarray[j].trim()) {
                                                checkbox[i].checked = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                                              
                                var txtChemNPOB = document.getElementById("<%=txtChemNPOB.ClientID%>");
                                txtChemNPOB.value = lblPOBNo.innerHTML;

                                var PnlChem = document.getElementById("<%=PnlChem.ClientID%>");
                                PnlChem.style.display = "block";
                                PnlChem.style.visibility = "visible";
                                PnlChem.className = "newchem";
                            }
                            
                            break;
                        }
                    }  
                }
            }
            $(".edit").hide();
            $(".delete").hide();
           
            return false;
        }

        function GetSelectedRow_undoc(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            document.getElementById("<%=hdnundocedit.ClientID %>").value = rowIndex;
            document.getElementById("<%=hdnNewUnDoc.ClientID %>").value = rowIndex;
            var undocedit = rowIndex;
            var sess_dcr = '';
            var time_dcr = '';
            var prod_sel = '';
            var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (k = 2; k < grid.rows.length + 1; k++) {
                        var cnt = 0;
                        var index = '';

                        if (k.toString().length == 1) {
                            index = cnt.toString() + k.toString();
                        }
                        else {
                            index = k.toString();
                        }
                        sess_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_sess_dcr');
                        time_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_time_dcr');
                        prod_sel = document.getElementById('GrdAdmStp_ctl' + index + '_prod_sel');
                    }
                }
            }
            var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
            grdUnLstDR.rows[rowIndex].style.backgroundColor = "LimeGreen";
            if (grdUnLstDR != null) {
                if (grdUnLstDR.rows.length > 0) {
                    for (k = 2; k < grdUnLstDR.rows.length + 1; k++) {
                        var cnt = 0;
                        var index = '';

                        if (k.toString().length == 1) {
                            index = cnt.toString() + k.toString();
                        }
                        else {
                            index = k.toString();
                        }
                        var lblUnLstDR_Code = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_Code');
                        var lblUnLstDR_DR = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_DR');
                        var lblUnLstDR_WorkWith = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_WorkWith');
                        var lblSess_Code = document.getElementById('grdUnLstDR_ctl' + index + '_lblSess_Code');
                        var lblmin = document.getElementById('grdUnLstDR_ctl' + index + '_lblmin');
                        var lblsec = document.getElementById('grdUnLstDR_ctl' + index + '_lblsec');
                        var lblProd1_Code = document.getElementById('grdUnLstDR_ctl' + index + '_lblProd1_Code');
                        var lblProd2_Code = document.getElementById('grdUnLstDR_ctl' + index + '_lblProd2_Code');
                        var lblProd3_Code = document.getElementById('grdUnLstDR_ctl' + index + '_lblProd3_Code');
                        var lblGift_Code = document.getElementById('grdUnLstDR_ctl' + index + '_lblGift_Code');
                        var lblUnLstDR_Qty1 = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_Qty1');
                        var lblUnLstDR_Qty2 = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_Qty2');
                        var lblUnLstDR_Qty3 = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_Qty3');
                        var lblUnLstDR_GQty = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_GQty');

                        var lblnew = document.getElementById('grdUnLstDR_ctl' + index + '_lblnew');
                        var lblterr = document.getElementById('grdUnLstDR_ctl' + index + '_lblterr');
                        var lblspe = document.getElementById('grdUnLstDR_ctl' + index + '_lblspe');
                        var lblcat = document.getElementById('grdUnLstDR_ctl' + index + '_lblcat');
                        var lblclass = document.getElementById('grdUnLstDR_ctl' + index + '_lblclass');
                        var lblqual = document.getElementById('grdUnLstDR_ctl' + index + '_lblqual');
                        var lbladd = document.getElementById('grdUnLstDR_ctl' + index + '_lbladd');
                        var lblsfcode = document.getElementById('grdUnLstDR_ctl' + index + '_lblsfcode');
                        
                        var chkedit = index - 1;
                        if (undocedit == chkedit) {
                            if (lblnew.innerHTML != "New") {
                                var Untxt_Dr = document.getElementById("<%=Untxt_Dr.ClientID%>");
                                Untxt_Dr.value = lblUnLstDR_DR.innerHTML;

                                var UnLstTxtDR = document.getElementById("<%=UnLstTxtDR.ClientID%>");
                                UnLstTxtDR.value = lblUnLstDR_Code.innerHTML;

                                var UnLstDr = document.getElementById("<%=UnLstDr.ClientID%>");

                                for (var i = 0; i < UnLstDr.options.length; i++) {
                                    if (UnLstDr.options[i].value == lblUnLstDR_Code.innerHTML) {
                                        UnLstDr.selectedIndex = i;
                                        break;
                                    }
                                }
                                if (sess_dcr.innerHTML == 0) {
                                    var ddlUnLstDR_Session = document.getElementById("<%=ddlUnLstDR_Session.ClientID%>");

                                    for (var i = 0; i < ddlUnLstDR_Session.options.length; i++) {
                                        if (ddlUnLstDR_Session.options[i].value == lblSess_Code.innerHTML) {
                                            ddlUnLstDR_Session.selectedIndex = i;
                                            break;
                                        }
                                    }
                                }
                                if (time_dcr.innerHTML == 0) {
                                    var ddlMinute = document.getElementById("<%=ddlMinute.ClientID%>");

                                    for (var i = 0; i < ddlMinute.options.length; i++) {
                                        if (ddlMinute.options[i].value == lblmin.innerHTML) {
                                            ddlMinute.selectedIndex = i;
                                            break;
                                        }
                                    }

                                    var ddlSec = document.getElementById("<%=ddlSec.ClientID%>");

                                    for (var i = 0; i < ddlSec.options.length; i++) {
                                        if (ddlSec.options[i].value == lblsec.innerHTML) {
                                            ddlSec.selectedIndex = i;
                                            break;
                                        }
                                    }
                                }
                                if (prod_sel.innerHTML != 0) {
                                    if (prod_sel.innerHTML >= 1) {
                                        var ddlUnLstDR_Prod1 = document.getElementById("<%=ddlUnLstDR_Prod1.ClientID%>");

                                        for (var i = 0; i < ddlUnLstDR_Prod1.options.length; i++) {
                                            if (ddlUnLstDR_Prod1.options[i].value == lblProd1_Code.innerHTML) {
                                                ddlUnLstDR_Prod1.selectedIndex = i;
                                                break;
                                            }
                                        }
                                        var txtUnLstDR_Prod_Qty1 = document.getElementById("<%=txtUnLstDR_Prod_Qty1.ClientID%>");
                                        txtUnLstDR_Prod_Qty1.value = lblUnLstDR_Qty1.innerHTML;
                                    }
                                    if (prod_sel.innerHTML >= 2) {
                                        var ddlUnLstDR_Prod2 = document.getElementById("<%=ddlUnLstDR_Prod2.ClientID%>");

                                        for (var i = 0; i < ddlUnLstDR_Prod2.options.length; i++) {
                                            if (ddlUnLstDR_Prod2.options[i].value == lblProd2_Code.innerHTML) {
                                                ddlUnLstDR_Prod2.selectedIndex = i;
                                                break;
                                            }
                                        }
                                        var txtUnLstDR_Prod_Qty2 = document.getElementById("<%=txtUnLstDR_Prod_Qty2.ClientID%>");
                                        txtUnLstDR_Prod_Qty2.value = lblUnLstDR_Qty2.innerHTML;
                                    }
                                    if (prod_sel.innerHTML >= 3) {
                                        var ddlUnLstDR_Prod3 = document.getElementById("<%=ddlUnLstDR_Prod3.ClientID%>");

                                        for (var i = 0; i < ddlUnLstDR_Prod3.options.length; i++) {
                                            if (ddlUnLstDR_Prod3.options[i].value == lblProd3_Code.innerHTML) {
                                                ddlUnLstDR_Prod3.selectedIndex = i;
                                                break;
                                            }
                                        }
                                        var txtUnLstDR_Prod_Qty3 = document.getElementById("<%=txtUnLstDR_Prod_Qty3.ClientID%>");
                                        txtUnLstDR_Prod_Qty3.value = lblUnLstDR_Qty3.innerHTML;
                                    }
                                }
                                var ddlUnLstDR_Gift = document.getElementById("<%=ddlUnLstDR_Gift.ClientID%>");

                                for (var i = 0; i < ddlUnLstDR_Gift.options.length; i++) {
                                    if (ddlUnLstDR_Gift.options[i].value == lblGift_Code.innerHTML) {
                                        ddlUnLstDR_Gift.selectedIndex = i;
                                        break;
                                    }
                                }
                                var txtUnLstDR_GQty = document.getElementById("<%=txtUnLstDR_GQty.ClientID%>");
                                txtUnLstDR_GQty.value = lblUnLstDR_GQty.innerHTML;
                               
                                var UnLstTxtDR = document.getElementById("<%=UnLstTxtDR.ClientID%>");
                                UnLstTxtDR.value = lblUnLstDR_Code.innerHTML;
                                
                                var txtUnLstDR_WW = document.getElementById("<%=txtUnLstDR_WW.ClientID%>");
                                txtUnLstDR_WW.value = lblUnLstDR_WorkWith.innerHTML;

                                var chkBox = document.getElementById("<%=chkUnLstDR.ClientID%>")
                                var checkbox = chkBox.getElementsByTagName("input");
                                var objTextBox = document.getElementById("<%=txtUnLstDR_SF.ClientID%>")
                                var counter = 0;
                                objTextBox.value = "";
                                for (var i = 0; i < checkbox.length; i++) {
                                    checkbox[i].checked = false;
                                }
                                var strarray = lblUnLstDR_WorkWith.innerHTML.split(',');

                                for (var j = 0; j < strarray.length; j++) {
                                    if (strarray[j].length > 0) {
                                        for (var i = 0; i < checkbox.length; i++) {
                                            var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                                            if (chkBoxText[0].innerHTML.substr(0, 9).trim() == strarray[j].trim()) {
                                                checkbox[i].checked = true;
                                                counter = counter + 1;
                                                objTextBox.value = counter;
                                                break;
                                            }
                                        }
                                    }
                                }
                                disablebut('btnNonListedDR', 'btnListedDR', 'btnChemists', 'btnStockist', 'btnHospital', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
                            }
                            else {
                                disablepanel();                          
                              
                                var hdnsftype = document.getElementById("<%=hdnsftype.ClientID%>");
                                if (hdnsftype.value == "2") {
                                    $('#lblUnMR').show();
                                    $('#ddlUnMR').show();
                                    var ddlUnMR = document.getElementById("<%=ddlUnMR.ClientID%>");

                                    for (var i = 0; i < ddlUnMR.options.length; i++) {
                                        if (ddlUnMR.options[i].value == lblsfcode.innerHTML) {
                                            ddlUnMR.selectedIndex = i;
                                            break;
                                        }
                                    }
                                    var ddlTerr_Un = document.getElementById("<%=ddlTerr_Un.ClientID%>");

                                    ddlTerr_Un.options.length = 0;
                                    var ddlAllTerr = document.getElementById("<%=ddlAllTerr.ClientID%>");
                                    var ddlsfterr = document.getElementById("<%=ddlsfterr.ClientID%>");

                                    for (i = 0; i < ddlAllTerr.options.length; i++) {
                                        if ((ddlsfterr.options[i].text == lblsfcode.innerHTML) || (ddlsfterr.options[i].text == "ALL")) {
                                            var opt = document.createElement("option");
                                            opt.text = ddlAllTerr.options[i].text;
                                            opt.value = ddlAllTerr.options[i].value;
                                            ddlTerr_Un.options.add(opt);
                                        }
                                    }
                                }
                                else {
                                    $('#lblUnMR').hide();
                                    $('#ddlUnMR').hide();
                                }
                                var ddlQual_Un = document.getElementById("<%=ddlQual_Un.ClientID%>");
                                for (var i = 0; i < ddlQual_Un.options.length; i++) {
                                    if (ddlQual_Un.options[i].value == lblqual.innerHTML) {
                                        ddlQual_Un.selectedIndex = i;
                                        break;
                                    }
                                }
                              
                                var ddlSpec_Un = document.getElementById("<%=ddlSpec_Un.ClientID%>");
                                for (var i = 0; i < ddlSpec_Un.options.length; i++) {
                                    if (ddlSpec_Un.options[i].value == lblspe.innerHTML) {
                                        ddlSpec_Un.selectedIndex = i;
                                        break;
                                    }
                                }
                                var ddlCate_Un = document.getElementById("<%=ddlCate_Un.ClientID%>");
                                for (var i = 0; i < ddlCate_Un.options.length; i++) {
                                    if (ddlCate_Un.options[i].value == lblcat.innerHTML) {
                                        ddlCate_Un.selectedIndex = i;
                                        break;
                                    }
                                }
                               
                                var ddlClass_Un = document.getElementById("<%=ddlClass_Un.ClientID%>");
                                for (var i = 0; i < ddlClass_Un.options.length; i++) {
                                    if (ddlClass_Un.options[i].value == lblclass.innerHTML) {
                                        ddlClass_Un.selectedIndex = i;
                                        break;
                                    }
                                }
                               
                                var ddlN_unsess = document.getElementById("<%=ddlN_unsess.ClientID%>");
                                for (var i = 0; i < ddlN_unsess.options.length; i++) {
                                    if (ddlN_unsess.options[i].value == lblSess_Code.innerHTML) {
                                        ddlN_unsess.selectedIndex = i;
                                        break;
                                    }
                                }
                             
                                var ddlN_untime = document.getElementById("<%=ddlN_untime.ClientID%>");
                                for (var i = 0; i < ddlN_untime.options.length; i++) {
                                    if (ddlN_untime.options[i].value == lblmin.innerHTML) {
                                        ddlN_untime.selectedIndex = i;
                                        break;
                                    }
                                }
                            
                                var ddlN_unmin = document.getElementById("<%=ddlN_unmin.ClientID%>");
                                for (var i = 0; i < ddlN_unmin.options.length; i++) {
                                    if (ddlN_unmin.options[i].value == lblsec.innerHTML) {
                                        ddlN_unmin.selectedIndex = i;
                                        break;
                                    }
                                }
                                var ddlN_unProd1 = document.getElementById("<%=ddlN_unProd1.ClientID%>");
                                for (var i = 0; i < ddlN_unProd1.options.length; i++) {
                                    if (ddlN_unProd1.options[i].value == lblProd1_Code.innerHTML) {
                                        ddlN_unProd1.selectedIndex = i;
                                        break;
                                    }
                                }
                               
                                var ddlTerr_Un = document.getElementById("<%=ddlTerr_Un.ClientID%>");
                                for (var i = 0; i < ddlTerr_Un.options.length; i++) {
                                    if (ddlTerr_Un.options[i].value == lblterr.innerHTML) {
                                        ddlTerr_Un.selectedIndex = i;
                                        break;
                                    }
                                }
                                var ddlN_unProd2 = document.getElementById("<%=ddlN_unProd2.ClientID%>");
                                for (var i = 0; i < ddlN_unProd2.options.length; i++) {
                                    if (ddlN_unProd2.options[i].value == lblProd2_Code.innerHTML) {
                                        ddlN_unProd2.selectedIndex = i;
                                        break;
                                    }
                                }
                                var ddlN_unProd3 = document.getElementById("<%=ddlN_unProd3.ClientID%>");
                                for (var i = 0; i < ddlN_unProd3.options.length; i++) {
                                    if (ddlN_unProd3.options[i].value == lblProd3_Code.innerHTML) {
                                        ddlN_unProd3.selectedIndex = i;
                                        break;
                                    }
                                }
                                var ddlN_ungift = document.getElementById("<%=ddlN_ungift.ClientID%>");
                                for (var i = 0; i < ddlN_ungift.options.length; i++) {
                                    if (ddlN_ungift.options[i].value == lblGift_Code.innerHTML) {
                                        ddlN_ungift.selectedIndex = i;
                                        break;
                                    }
                                }
                                     
                                var txtN_UQty1 = document.getElementById("<%=txtN_UQty1.ClientID%>");
                                txtN_UQty1.value = lblUnLstDR_Qty1.innerHTML;

                                var txtN_UQty2 = document.getElementById("<%=txtN_UQty2.ClientID%>");
                                txtN_UQty2.value = lblUnLstDR_Qty2.innerHTML;

                                var txtN_UQty3 = document.getElementById("<%=txtN_UQty3.ClientID%>");
                                txtN_UQty3.value = lblUnLstDR_Qty3.innerHTML;

                                var txtN_GQty = document.getElementById("<%=txtN_GQty.ClientID%>");
                                txtN_GQty.value = lblUnLstDR_GQty.innerHTML;

                                var txtUnDr = document.getElementById("<%=txtUnDr.ClientID%>");
                                txtUnDr.value = lblUnLstDR_DR.innerHTML;

                                var txtUnDrAddr = document.getElementById("<%=txtUnDrAddr.ClientID%>");
                                txtUnDrAddr.value = lbladd.innerHTML;

                                var txtUn = document.getElementById("<%=txtUn.ClientID%>");
                                txtUn.value = lblUnLstDR_WorkWith.innerHTML;
                                
                                var chkBox = document.getElementById("<%=ChkUn.ClientID%>")
                                var checkbox = chkBox.getElementsByTagName("input");

                                for (var i = 0; i < checkbox.length; i++) {
                                    checkbox[i].checked = false;
                                }
                                var strarray = lblUnLstDR_WorkWith.innerHTML.split(',');

                                for (var j = 0; j < strarray.length; j++) {
                                    if (strarray[j].length > 0) {
                                        for (var i = 0; i < checkbox.length; i++) {
                                            var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                                            if (chkBoxText[0].innerHTML.substr(0, 9).trim() == strarray[j].trim()) {
                                                checkbox[i].checked = true;
                                                break;
                                           }
                                        }
                                    }
                                }
                                var NPnlUnLst = document.getElementById("<%=NPnlUnLst.ClientID%>");
                                NPnlUnLst.style.display = "block";
                                NPnlUnLst.style.visibility = "visible";
                                NPnlUnLst.className = "newundoc";
                            }
                            break;
                        }
                    }
                }
            }
            $(".edit").hide();
            $(".delete").hide();
            
            return false;
        }
        function GetSelectedRow_stk(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            document.getElementById("<%=hdnstkedit.ClientID %>").value = rowIndex;      
 
            var stkedit = rowIndex;
            var GridStk = document.getElementById('<%=GridStk.ClientID%>');
            GridStk.rows[rowIndex].style.backgroundColor = "LimeGreen";
            if (GridStk != null) {
                if (GridStk.rows.length > 0) {
                    for (k = 2; k < GridStk.rows.length + 1; k++) {
                        var cnt = 0;
                        var index = '';

                        if (k.toString().length == 1) {
                            index = cnt.toString() + k.toString();
                        }
                        else {
                            index = k.toString();
                        }
                        var lblStockist = document.getElementById('GridStk_ctl' + index + '_lblStockist');
                        var lblStkWW = document.getElementById('GridStk_ctl' + index + '_lblStkWW');
                        var lblPOB = document.getElementById('GridStk_ctl' + index + '_lblPOB');
                        var lblStkVT = document.getElementById('GridStk_ctl' + index + '_lblStkVT');
                        var lblstk_Code = document.getElementById('GridStk_ctl' + index + '_lblstk_Code');
                        var lblvisit_Code = document.getElementById('GridStk_ctl' + index + '_lblvisit_Code');
                      
                        var chkedit = index - 1;
                        if (stkedit == chkedit) {
                            var StkDDL = document.getElementById("<%=StkDDL.ClientID%>");
                                for (var i = 0; i < StkDDL.options.length; i++) {
                                if (StkDDL.options[i].value == lblstk_Code.innerHTML) {
                                    StkDDL.selectedIndex = i;                                    
                                    break;
                                }
                            }
                           
                            var StkVisitType = document.getElementById("<%=StkVisitType.ClientID%>");
                            for (var i = 0; i < StkVisitType.options.length; i++) {
                                if (StkVisitType.options[i].value == lblvisit_Code.innerHTML) {
                                    StkVisitType.selectedIndex = i;   
                                    break;
                                }
                            }
                           
                            var StkPOB = document.getElementById("<%=StkPOB.ClientID%>");
                            StkPOB.value = lblPOB.innerHTML;

                            var HdnStk = document.getElementById("<%=HdnStk.ClientID%>");
                            HdnStk.value = lblStkWW.innerHTML;
                            
                            var chkBox = document.getElementById("<%=StkChkBox.ClientID%>")
                            var checkbox = chkBox.getElementsByTagName("input");
                            var objTextBox = document.getElementById("<%=txtStk.ClientID%>")
                            var counter = 0;
                            objTextBox.value = "";
                            for (var i = 0; i < checkbox.length; i++) {
                                checkbox[i].checked = false;
                            }
                            var strarray = lblStkWW.innerHTML.split(',');
                            for (var j = 0; j < strarray.length; j++) {
                                    if (strarray[j].length > 0) {                                        
                                        for (var i = 0; i < checkbox.length; i++) {
                                            var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                                            if (chkBoxText[0].innerHTML.substr(0, 9).trim() == strarray[j].trim()) {
                                             
                                                checkbox[i].checked = true;
                                                counter = counter + 1;
                                                objTextBox.value = counter;
                                            }
                                        }
                                    }
                                }
                            
                                break;                               
                        }
                    }
                }
           }
             
            $(".edit").hide();
            
            $(".delete").hide();
            disablebut('btnStockist', 'btnListedDR', 'btnChemists', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');

            return false;
        }
        function GetSelectedRow_hos(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            document.getElementById("<%=hdnhosedit.ClientID %>").value = rowIndex;

            var hosedit = rowIndex;
            var GridHospital = document.getElementById('<%=GridHospital.ClientID%>');
            GridHospital.rows[rowIndex].style.backgroundColor = "LimeGreen";
            if (GridHospital != null) {
                if (GridHospital.rows.length > 0) {
                    for (k = 2; k < GridHospital.rows.length + 1; k++) {
                        var cnt = 0;
                        var index = '';

                        if (k.toString().length == 1) {
                            index = cnt.toString() + k.toString();
                        }
                        else {
                            index = k.toString();
                        }
                        var lblHospital = document.getElementById('GridHospital_ctl' + index + '_lblHospital');
                        var lblHosWW = document.getElementById('GridHospital_ctl' + index + '_lblHosWW');
                        var lblPOB = document.getElementById('GridHospital_ctl' + index + '_lblPOB');
                        var lblHos_Code = document.getElementById('GridHospital_ctl' + index + '_lblHos_Code');                   

                        var chkedit = index - 1;
                        if (hosedit == chkedit) {
                            var HosDDL = document.getElementById("<%=HosDDL.ClientID%>");
                            for (var i = 0; i < HosDDL.options.length; i++) {
                                if (HosDDL.options[i].value == lblHos_Code.innerHTML) {
                                    HosDDL.selectedIndex = i;
                                    break;
                                }
                            }
                            var txtHospPOB = document.getElementById("<%=txtHospPOB.ClientID%>");
                            txtHospPOB.value = lblPOB.innerHTML;

                            var HdnHos = document.getElementById("<%=HdnHos.ClientID%>");
                            HdnHos.value = lblHosWW.innerHTML;

                            var chkBox = document.getElementById("<%=ChkHos.ClientID%>")
                            var checkbox = chkBox.getElementsByTagName("input");
                            var objTextBox = document.getElementById("<%=txtHos.ClientID%>")
                            var counter = 0;
                            objTextBox.value = "";
                            for (var i = 0; i < checkbox.length; i++) {
                                checkbox[i].checked = false;
                            }
                            var strarray = lblHosWW.innerHTML.split(',');
                            for (var j = 0; j < strarray.length; j++) {
                                if (strarray[j].length > 0) {
                                    for (var i = 0; i < checkbox.length; i++) {
                                        var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                                        if (chkBoxText[0].innerHTML.substr(0, 9).trim() == strarray[j].trim()) {

                                            checkbox[i].checked = true;
                                            counter = counter + 1;
                                            objTextBox.value = counter;
                                        }
                                    }
                                }
                            }

                            break;
                        }
                    }
                }
            }

            $(".edit").hide();
            $(".delete").hide();
            disablebut('btnHospital', 'btnListedDR', 'btnChemists', 'btnNonListedDR', 'btnStockist', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
            return false;
        }
        function isValidate_Chemists() {
            var cval = true;
            var cheedit = document.getElementById("<%=hdncheedit.ClientID %>").value;
          
            var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var max_chem_dcr_count = document.getElementById('GrdAdmStp_ctl' + index + '_max_chem_dcr_count');
                        if (cheedit.length == 0) {
                            var grdChem = document.getElementById('<%=grdChem.ClientID%>');
                            if (grdChem != null) {
                                if (grdChem.rows.length > 0) {
                                    if (grdChem.rows.length - 1 == max_chem_dcr_count.innerHTML) {
                                        alert('Cannot enter more than ' + max_chem_dcr_count.innerHTML + ' Chemists');
                                        cval = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cval == true) {
                var txtPnlChe = document.getElementById("<%=txtPnlChe.ClientID%>").value;
                var Chetext = document.getElementById("<%=txtPnlChe.ClientID%>");
                var txtPChe = document.getElementById("<%=txtPChe.ClientID%>");
                var chehdnValue = document.getElementById("<%=chehdnValue.ClientID%>");
                if (txtPnlChe.length == 0) {
                    alert("Select Chemists");
                    cval = false;
                    document.getElementById("<%=txtPnlChe.ClientID%>").focus();
                }
                else {
                    var dpt = document.getElementById("lstChe");
                    if (dpt.selectedIndex >= 0) {
                        Chetext.value = dpt.options[dpt.selectedIndex].text;
                        txtPChe.value = dpt.options[dpt.selectedIndex].value;
                    }
                    else if (chehdnValue.value != '') {
                        var lstChe = document.getElementById("<%=lstChe.ClientID%>");
                        for (k = 0; k < lstChe.options.length; k++) {
                            if (lstChe.options[k].value == chehdnValue.value) {
                                Chetext.value = lstChe.options[k].text;
                                txtPChe.value = lstChe.options[k].value;
                                break;
                            }
                        }
                    }
                    else {
                        var nodoc = true;
                        var lstChe = document.getElementById("<%=lstChe.ClientID%>");
                        if (lstChe.options.length > 0) {
                            for (i = 0; i < lstChe.options.length; i++) {
                                if (txtPnlChe.toUpperCase().trim() == lstChe.options[i].text.toUpperCase().trim()) {
                                    Chetext.value = lstChe.options[i].text;
                                    txtPChe.value = lstChe.options[i].value;
                                    nodoc = false;
                                    break;
                                }
                            }
                            if (nodoc == true) {
                                alert('Chemists does not exists!!');
                                cval = false;

                            }
                        }
                        else {
                            alert('Chemists does not exists!!');
                            cval = false;
                        }
                    }
                    if (cval == true) {
                        var dupdoc = false;

                        var grdChem = document.getElementById('<%=grdChem.ClientID%>');
                        if (grdChem != null) {
                            if (grdChem.rows.length > 0) {
                                for (i = 2; i < grdChem.rows.length + 1; i++) {
                                    var cnt = 0;
                                    var index = '';

                                    if (i.toString().length == 1) {
                                        index = cnt.toString() + i.toString();
                                    }
                                    else {
                                        index = i.toString();
                                    }
                                    var chename = document.getElementById('grdChem_ctl' + index + '_lblChemists');

                                    var chkedit = index - 1;
                                    if (cheedit.length == 0) {
                                        if (txtPnlChe.toUpperCase().trim() == chename.innerHTML.toUpperCase()) {
                                            dupdoc = true;
                                            break;
                                        }
                                    }
                                    else {

                                        if ((txtPnlChe.toUpperCase().trim() == chename.innerHTML.toUpperCase()) && (cheedit != chkedit)) {
                                            dupdoc = true;
                                            break;
                                        }
                                    }
                                }
                                if (dupdoc == true) {
                                    alert('Duplicate Chemist!!');
                                    cval = false;

                                }

                            }
                        }
                    }
                }
            }
            if (cval == true) {
                var txtPOBNo = document.getElementById("<%=txtPOBNo.ClientID%>");
                if (txtPOBNo.value.length > 0) {
                    if (isNaN(txtPOBNo.value)) {
                        alert("Only Numeric Values Allowed for POB");
                        return false;
                    }
                }
                LoadChemists();
                enablebut('btnChemists', 'btnListedDR', 'btnStockist', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
            }
          
            return cval;
        }

        function isValidate_Stockiest() {
            var cval = true;
            var index = document.getElementById("<%=StkDDL.ClientID%>").selectedIndex;
            var sval = document.getElementById("<%=StkDDL.ClientID%>").value;
            var svaltext = $('#<%=StkDDL.ClientID%> :selected').text();
            var stkedit = document.getElementById("<%=hdnstkedit.ClientID %>").value;

            var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var max_stk_dcr_count = document.getElementById('GrdAdmStp_ctl' + index + '_max_stk_dcr_count');
                        if (stkedit.length == 0) {
                            var GridStk = document.getElementById('<%=GridStk.ClientID%>');
                            if (GridStk != null) {
                                if (GridStk.rows.length > 0) {
                                    if (GridStk.rows.length - 1 == max_stk_dcr_count.innerHTML) {
                                        alert('Cannot enter more than ' + max_stk_dcr_count.innerHTML + ' Stockists');
                                        cval = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cval == true) {
                if (sval == "-1") {
                    alert("Select Stockist");
                    cval = false;
                    document.getElementById("<%=StkDDL.ClientID%>").focus();
                }
                if (cval == true) {
                    var dupstk = false;

                    var GridStk = document.getElementById('<%=GridStk.ClientID%>');
                    if (GridStk != null) {
                        if (GridStk.rows.length > 0) {
                            for (i = 2; i < GridStk.rows.length + 1; i++) {
                                var cnt = 0;
                                var index = '';

                                if (i.toString().length == 1) {
                                    index = cnt.toString() + i.toString();
                                }
                                else {
                                    index = i.toString();
                                }

                                var stkcode = document.getElementById('GridStk_ctl' + index + '_lblStockist');

                                var chkedit = index - 1;
                                if (stkedit.length == 0) {
                                    if (svaltext.trim() == stkcode.innerHTML) {
                                        dupstk = true;
                                        break;
                                    }
                                }
                                else {

                                    if ((svaltext.trim() == stkcode.innerHTML) && (stkedit != chkedit)) {
                                        dupstk = true;
                                        break;
                                    }
                                }
                            }
                            if (dupstk == true) {
                                alert('Duplicate Stockist!!');
                                cval = false;

                            }

                        }
                    }
                }
            }
            if (cval == true) {
                LoadStockiest();
                enablebut('btnStockist', 'btnListedDR', 'btnChemists', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
            }
            return cval
        }

        function isValidate_UnListedDr() {
            var cval = true;
         
            var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (k = 2; k < grid.rows.length + 1; k++) {
                        var cnt = 0;
                        var index = '';

                        if (k.toString().length == 1) {
                            index = cnt.toString() + k.toString();
                        }
                        else {
                            index = k.toString();
                        }
                        var sess_m_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_sess_m_dcr');
                        var time_m_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_time_m_dcr');
                        var prod_mand_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_prod_mand_dcr');
                        var prod_qty_dcr = document.getElementById('GrdAdmStp_ctl' + index + '_prod_qty_dcr');
                        var max_unlst_dcr_count = document.getElementById('GrdAdmStp_ctl' + index + '_max_unlst_dcr_count');
                        var undocedit = document.getElementById("<%=hdnundocedit.ClientID %>").value;
                        // Check max doc
                        if (undocedit.length == 0) {
                            var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
                            if (grdUnLstDR != null) {
                                if (grdUnLstDR.rows.length > 0) {
                                    if (grdUnLstDR.rows.length - 1 == max_unlst_dcr_count.innerHTML) {
                                        alert('Cannot enter more than ' + max_unlst_dcr_count.innerHTML + ' Un-Listed Doctors');
                                        cval = false;
                                        break;
                                    }
                                }
                            }
                        }
                        if (sess_m_dcr.innerHTML == "1") {
                            var index_ses = document.getElementById("<%=ddlUnLstDR_Session.ClientID%>").selectedIndex;
                            var sval_ses = document.getElementById("<%=ddlUnLstDR_Session.ClientID%>").value;
                            if (sval_ses == "0") {
                                alert("Select Session");
                                cval = false;
                                document.getElementById("<%=ddlUnLstDR_Session.ClientID%>").focus();
                                break;
                            }
                        }
                        if (time_m_dcr.innerHTML == "1") {
                            var index_time = document.getElementById("<%=ddlMinute.ClientID%>").selectedIndex;
                           
                            if (index_time == "0") {
                                alert("Select Time");
                                cval = false;
                                document.getElementById("<%=ddlMinute.ClientID%>").focus();
                                break;
                            }
                        }
                        var Untxt_Dr = document.getElementById("<%=Untxt_Dr.ClientID%>").value;
                        var undoct = document.getElementById("<%=Untxt_Dr.ClientID%>");
                        var UnLstTxtDR = document.getElementById("<%=UnLstTxtDR.ClientID%>");
                        var UnDrhdnValue = document.getElementById("<%=UnDrhdnValue.ClientID%>");

                        if (Untxt_Dr.length == 0) {
                            alert("Select Un-Listed Doctor");
                            cval = false;
                            document.getElementById("<%=Untxt_Dr.ClientID%>").focus();
                            break;
                        }
                        else {
                            var dpt = document.getElementById("UnLstDr");
                            if (dpt.selectedIndex >= 0) {
                                undoct.value = dpt.options[dpt.selectedIndex].text;
                                UnLstTxtDR.value = dpt.options[dpt.selectedIndex].value;
                            }
                            else if (UnDrhdnValue.value != '') {
                                var UnLstDr = document.getElementById("<%=UnLstDr.ClientID%>");
                                for (l = 0; l < UnLstDr.options.length; l++) {
                                    if (UnLstDr.options[l].value == UnDrhdnValue.value) {
                                        undoct.value = UnLstDr.options[l].text;
                                        UnLstTxtDR.value = UnLstDr.options[l].value;
                                        break;
                                    }
                                }
                            }
                            else {
                                var nodoc = true;

                                var UnLstDr = document.getElementById("<%=UnLstDr.ClientID%>");
                                if (UnLstDr.options.length > 0) {
                                    for (i = 0; i < UnLstDr.options.length; i++) {
                                        if (Untxt_Dr.toUpperCase().trim() == UnLstDr.options[i].text.toUpperCase().trim()) {
                                            undoct.value = UnLstDr.options[i].text;
                                            UnLstTxtDR.value = UnLstDr.options[i].value;
                                            nodoc = false;
                                            break;
                                        }
                                    }
                                    if (nodoc == true) {
                                        alert('Un-Listed Doctor does not exists!!');
                                        cval = false;
                                        break;
                                    }
                                }
                                else {
                                    alert('Un-Listed Doctor does not exists!!');
                                    cval = false;
                                    break;
                                }
                            }
                            if (cval == true) {
                                var dupdoc = false;

                                var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
                                if (grdUnLstDR != null) {
                                    if (grdUnLstDR.rows.length > 0) {
                                        for (i = 2; i < grdUnLstDR.rows.length + 1; i++) {
                                            var cnt = 0;
                                            var index = '';

                                            if (i.toString().length == 1) {
                                                index = cnt.toString() + i.toString();
                                            }
                                            else {
                                                index = i.toString();
                                            }
                                            var drname = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_DR');

                                            var chkedit = index - 1;
                                            if (undocedit.length == 0) {
                                                if (Untxt_Dr.toUpperCase().trim() == drname.innerHTML.toUpperCase()) {
                                                    dupdoc = true;
                                                    break;
                                                }
                                            }
                                            else {

                                                if ((Untxt_Dr.toUpperCase().trim() == drname.innerHTML.toUpperCase()) && (undocedit != chkedit)) {
                                                    dupdoc = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (dupdoc == true) {
                                            alert('Duplicate Un-Listed Doctor!!');
                                            cval = false;
                                            break;
                                        }

                                    }
                                }
                            }
                        }
                        if (prod_mand_dcr.innerHTML > 0) {
                            if (prod_mand_dcr.innerHTML >= 1) {
                                var ddlProd1_index = document.getElementById("<%=ddlUnLstDR_Prod1.ClientID%>").selectedIndex;
                                var ddlProd1_val = document.getElementById("<%=ddlUnLstDR_Prod1.ClientID%>").value;
                                if (ddlProd1_val == "0") {
                                    alert("Select Product");
                                    cval = false;
                                    document.getElementById("<%=ddlUnLstDR_Prod1.ClientID%>").focus();
                                    break;
                                }
                                if (prod_qty_dcr.innerHTML == 0) {
                                    var txtProd1 = document.getElementById("<%=txtUnLstDR_Prod_Qty1.ClientID%>").value;
                                    if (txtProd1.length == 0) {
                                        alert("Enter Sample Qty");
                                        cval = false;
                                        document.getElementById("<%=txtUnLstDR_Prod_Qty1.ClientID%>").focus();
                                        break;
                                    }
                                }
                            }
                            if (prod_mand_dcr.innerHTML >= 2) {
                                var ddlProd2_index = document.getElementById("<%=ddlUnLstDR_Prod2.ClientID%>").selectedIndex;
                                var dddlProd2_val = document.getElementById("<%=ddlUnLstDR_Prod2.ClientID%>").value;
                                if (dddlProd2_val == "0") {
                                    alert("Select Product");
                                    cval = false;
                                    document.getElementById("<%=ddlUnLstDR_Prod2.ClientID%>").focus();
                                    break;
                                }

                                if (prod_qty_dcr.innerHTML == 0) {
                                    var txtProd2 = document.getElementById("<%=txtUnLstDR_Prod_Qty2.ClientID%>").value;
                                    if (txtProd2.length == 0) {
                                        alert("Enter Sample Qty");
                                        cval = false;
                                        document.getElementById("<%=txtUnLstDR_Prod_Qty2.ClientID%>").focus();
                                        break;
                                    }
                                }
                            }
                            if (prod_mand_dcr.innerHTML >= 3) {
                                var ddlProd3_index = document.getElementById("<%=ddlUnLstDR_Prod3.ClientID%>").selectedIndex;
                                var dddlProd3_val = document.getElementById("<%=ddlUnLstDR_Prod3.ClientID%>").value;
                                if (dddlProd3_val == "0") {
                                    alert("Select Product");
                                    cval = false;
                                    document.getElementById("<%=ddlUnLstDR_Prod3.ClientID%>").focus();
                                    break;
                                }

                                if (prod_qty_dcr.innerHTML == 0) {
                                    var txtProd3 = document.getElementById("<%=txtUnLstDR_Prod_Qty3.ClientID%>").value;
                                    if (txtProd3.length == 0) {
                                        alert("Enter Sample Qty");
                                        cval = false;
                                        document.getElementById("<%=txtUnLstDR_Prod_Qty3.ClientID%>").focus();
                                        break;
                                    }
                                }
                            }
                            //Dup check
                            var ddlProd1_val = document.getElementById("<%=ddlUnLstDR_Prod1.ClientID%>").value;
                            var dddlProd2_val = document.getElementById("<%=ddlUnLstDR_Prod2.ClientID%>").value;
                            var dddlProd3_val = document.getElementById("<%=ddlUnLstDR_Prod3.ClientID%>").value;
                            if (ddlProd1_val != 0 && dddlProd2_val != 0) {
                                if (ddlProd1_val == dddlProd2_val) {
                                    alert("Duplicate Product");
                                    cval = false;
                                    break;
                                }
                            }
                            if (ddlProd1_val != 0 && dddlProd3_val != 0) {
                                if (ddlProd1_val == dddlProd3_val) {
                                    alert("Duplicate Product");
                                    cval = false;
                                    break;
                                }
                            }
                            if (dddlProd2_val != 0 && dddlProd3_val != 0) {
                                if (dddlProd2_val == dddlProd3_val) {
                                    alert("Duplicate Product");
                                    cval = false;
                                    break;
                                }
                            }
                        } //end of  if (prod_mand_dcr > 0)
                        var txtUnLstDR_GQty = document.getElementById("<%=txtUnLstDR_GQty.ClientID%>").value;
                        
                    } // end of for
                } // end of if grid length
            } //end of grid null check
            var ddlUnLstDR_Gift = document.getElementById("<%=ddlUnLstDR_Gift.ClientID%>").value;
            if (ddlUnLstDR_Gift != 0) {
                if (txtUnLstDR_GQty.length == 0) {
                alert("Enter Input Qty");
                cval = false;
              }
            }
            if (cval == true) {
                LoadUnListedDR();
                enablebut('btnNonListedDR', 'btnListedDR', 'btnChemists', 'btnStockist', 'btnHospital', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
            }
           
            return cval
        }

        function isValidate_Hospital() {
            var cval = true;
            var index = document.getElementById("<%=HosDDL.ClientID%>").selectedIndex;
            var sval = document.getElementById("<%=HosDDL.ClientID%>").value;
            var svaltext = $('#<%=HosDDL.ClientID%> :selected').text();
            var hosedit = document.getElementById("<%=hdnhosedit.ClientID %>").value;
            var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var max_hos_dcr_count = document.getElementById('GrdAdmStp_ctl' + index + '_max_hos_dcr_count');
                        if (hosedit.length == 0) {
                            var GridHospital = document.getElementById('<%=GridHospital.ClientID%>');
                            if (GridHospital != null) {
                                if (GridHospital.rows.length > 0) {
                                    if (GridHospital.rows.length - 1 == max_hos_dcr_count.innerHTML) {
                                        alert('Cannot enter more than ' + max_hos_dcr_count.innerHTML + ' Hospitals');
                                        cval = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cval == true) {
                if (sval == "-1") {
                    alert("Select Hospital");
                    cval = false;
                    document.getElementById("<%=HosDDL.ClientID%>").focus();
                }
                if (cval == true) {
                    var duphos = false;

                    var GridHospital = document.getElementById('<%=GridHospital.ClientID%>');
                    if (GridHospital != null) {
                        if (GridHospital.rows.length > 0) {
                            for (i = 2; i < GridHospital.rows.length + 1; i++) {
                                var cnt = 0;
                                var index = '';

                                if (i.toString().length == 1) {
                                    index = cnt.toString() + i.toString();
                                }
                                else {
                                    index = i.toString();
                                }

                                var hosname = document.getElementById('GridHospital_ctl' + index + '_lblHospital');

                                var chkedit = index - 1;
                                if (hosedit.length == 0) {
                                    if (svaltext.trim() == hosname.innerHTML) {
                                        duphos = true;
                                        break;
                                    }
                                }
                                else {

                                    if ((svaltext.trim() == hosname.innerHTML) && (hosedit != chkedit)) {
                                        duphos = true;
                                        break;
                                    }
                                }
                            }
                            if (duphos == true) {
                                alert('Duplicate Hospital!!');
                                cval = false;

                            }

                        }
                    }
                }
            }
            if (cval == true) {
                LoadHospital();
                enablebut('btnHospital', 'btnListedDR', 'btnChemists', 'btnNonListedDR', 'btnStockist', 'btnRemarks', 'btnPreview', 'btnNewChe', 'btnUnNew');
            }
            return cval;

        }


        function HideChePopup() {
            var mpu = $find('txtChe_PopupControlExtender')
            mpu.hidePopup();
            return false;
        }


        function HideUnPopup() {
            var mpu = $find('txtUn_PopupControlExtender1')
            mpu.hidePopup();
            return false;
        }
        function OnUnLdrSelected(sender, e) {
            var UnDrhdnValue = $get('<%= UnDrhdnValue.ClientID %>');
            UnDrhdnValue.value = e.get_value();
            document.getElementById("UnLstDr").selectedIndex = -1;
        }
        function OnLdrSelected(sender, e) {
            var hdnValue = $get('<%= hdnValue.ClientID %>');
            hdnValue.value = e.get_value();
            document.getElementById("lstListDR").selectedIndex = -1;
        }
        function OnCheSelected(sender, e) {
            var chehdnValue = $get('<%= chehdnValue.ClientID %>');
            chehdnValue.value = e.get_value();
            document.getElementById("lstChe").selectedIndex = -1;
        }

        function LdrDocLoad() {

            var dpt = document.getElementById("lstListDR");
            var ldrdocname = $get("txtListDR");
            ldrdocname.value = dpt.options[dpt.selectedIndex].text;
            var ldrdoccode = $get("txtListDRCode");
            ldrdoccode.value = dpt.options[dpt.selectedIndex].value;
            var pnlList = document.getElementById('pnlList');
            if (pnlList.style.display == "block") {
                pnlList.style.display = "none";
                pnlList.style.visibility = "hidden";
            }
            var hdnValue = document.getElementById('<%= hdnValue.ClientID %>');
            hdnValue.value = '';
        }

        function cheLoad() {

            var dpt = document.getElementById("lstChe");
            var chename = $get("txtPnlChe");
            chename.value = dpt.options[dpt.selectedIndex].text;
            var checode = $get("txtPChe");
            checode.value = dpt.options[dpt.selectedIndex].value;
            var pnlList = document.getElementById('PChe');
            if (pnlList.style.display == "block") {
                pnlList.style.display = "none";
                pnlList.style.visibility = "hidden";
            }
            var chehdnValue = document.getElementById('<%= chehdnValue.ClientID %>');
            chehdnValue.value = '';
        }

        function UnLdrDocLoad() {
            var dpt = document.getElementById("UnLstDr");
            var Unldrdocname = $get("Untxt_Dr");
            Unldrdocname.value = dpt.options[dpt.selectedIndex].text;
            var Unldrdoccode = $get("UnLstTxtDR");
            Unldrdoccode.value = dpt.options[dpt.selectedIndex].value;
            var pnlList = document.getElementById('UnListPnl');
            if (pnlList.style.display == "block") {
                pnlList.style.display = "none";
                pnlList.style.visibility = "hidden";
            }

            var UnDrhdnValue = document.getElementById('<%= UnDrhdnValue.ClientID %>');
            UnDrhdnValue.value = '';
        }

       
        function tabbut_Click(show_pnl, hide_pnl1, hide_pnl2, hide_pnl3, hide_pnl4, hide_pnl5, hide_pnl6, high_but, blur_but1, blur_but2, blur_but3, blur_but4, blur_but5, blur_but6, hdnbut) {
            var pb = show_pnl;
            var hdnbutname = document.getElementById("<%=hdnbutname.ClientID%>");
            hdnbutname.value = hdnbut;

            var show_pnl = document.getElementById(show_pnl);
            show_pnl.style.display = "block";
            show_pnl.style.visibility = "visible";

            var hide_pnl1 = document.getElementById(hide_pnl1);
            if (hide_pnl1.style.display == "block") {
                hide_pnl1.style.display = "none";
                hide_pnl1.style.visibility = "hidden";
            }

            var hide_pnl2 = document.getElementById(hide_pnl2);
            if (hide_pnl2.style.display == "block") {
                hide_pnl2.style.display = "none";
                hide_pnl2.style.visibility = "hidden";
            }

            var hide_pnl3 = document.getElementById(hide_pnl3);
            if (hide_pnl3.style.display == "block") {
                hide_pnl3.style.display = "none";
                hide_pnl3.style.visibility = "hidden";
            }

            var hide_pnl4 = document.getElementById(hide_pnl4);
            if (hide_pnl4.style.display == "block") {
                hide_pnl4.style.display = "none";
                hide_pnl4.style.visibility = "hidden";
            }

            var hide_pnl5 = document.getElementById(hide_pnl5);
            if (hide_pnl5.style.display == "block") {
                hide_pnl5.style.display = "none";
                hide_pnl5.style.visibility = "hidden";
            }

//            var hide_pnl6 = document.getElementById(hide_pnl6);
//            if (hide_pnl6.style.display == "block") {
//                hide_pnl6.style.display = "none";
//                hide_pnl6.style.visibility = "hidden";
//            }

            var butgreen = document.getElementById(high_but);
            if (butgreen.style.display == "block") {
                butgreen.style.backgroundColor = 'green';
                butgreen.style.color = 'white';
            }

            var blur_but1 = document.getElementById(blur_but1);
            if (blur_but1.style.display == "block") {
                blur_but1.style.backgroundColor = 'white';
                blur_but1.style.color = 'black';
            }

            var blur_but2 = document.getElementById(blur_but2);
            if (blur_but2.style.display == "block") {
                blur_but2.style.backgroundColor = 'white';
                blur_but2.style.color = 'black';
            }

            var blur_but3 = document.getElementById(blur_but3);
            if (blur_but3.style.display == "block") {
                blur_but3.style.backgroundColor = 'white';
                blur_but3.style.color = 'black';
            }

            var blur_but4 = document.getElementById(blur_but4);
            if (blur_but4.style.display == "block") {
                blur_but4.style.backgroundColor = 'white';
                blur_but4.style.color = 'black';
            }

            var blur_but5 = document.getElementById(blur_but5);
            if (blur_but5.style.display == "block") {
                blur_but5.style.backgroundColor = 'white';
                blur_but5.style.color = 'black';
            }

//            var blur_but6 = document.getElementById(blur_but6);
//            if (blur_but6.style.display == "block") {
//                blur_but6.style.backgroundColor = 'white';
//                blur_but6.style.color = 'black';
//            }

            if (hdnbutname.value == "5") {
                var txtMsg = document.getElementById("<%=txtRemarkDesc.ClientID%>");
                chars = txtMsg.value.length;
                var CharLength = 0;
                var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
                if (grid != null) {
                    if (grid.rows.length > 0) {
                        for (i = 2; i < grid.rows.length + 1; i++) {
                            var cnt = 0;
                            var index = '';

                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }
                            var remarks_len = document.getElementById('GrdAdmStp_ctl' + index + '_remarks_len');
                            CharLength = remarks_len.innerHTML;
                        }
                    }
                }
                document.getElementById('lblCount').innerHTML = CharLength - chars;
                if (chars > CharLength) {
                    txtMsg.value = txtMsg.value.substring(0, CharLength);
                }
            }
            return false;
        }
       
        function loadsetup() {
        var grid = document.getElementById('<%=grdWorkType.ClientID%>');
        if (grid != null) {
            if (grid.rows.length > 0) {
                var butgreen = document.getElementById('btnListedDR');
                butgreen.style.display = "none";
                butgreen.style.visibility = "hidden";

                var butgreen = document.getElementById('btnChemists');
                butgreen.style.display = "none";
                butgreen.style.visibility = "hidden";

                var butgreen = document.getElementById('btnNonListedDR');
                butgreen.style.display = "none";
                butgreen.style.visibility = "hidden";

                var butgreen = document.getElementById('btnHospital');
                butgreen.style.display = "none";
                butgreen.style.visibility = "hidden";

                var butgreen = document.getElementById('btnStockist');
                butgreen.style.display = "none";
                butgreen.style.visibility = "hidden";

                var butgreen = document.getElementById('btnPreview');
                butgreen.style.display = "none";
                butgreen.style.visibility = "hidden";

                for (i = 2; i < grid.rows.length + 1; i++) {
                    var cnt = 0;
                    var index = '';

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {
                        index = i.toString();
                    }

                    var code = document.getElementById('grdWorkType_ctl' + index + '_lblCode');
                    var butacc = document.getElementById('grdWorkType_ctl' + index + '_lblAccess');
                    var fwind = document.getElementById('grdWorkType_ctl' + index + '_lblFWInd');
                   
                    var index = document.getElementById("<%=ddlWorkType.ClientID%>").selectedIndex;
                    var sval = document.getElementById("<%=ddlWorkType.ClientID%>").value;
                    if (code.innerHTML == sval) {

                        labelText = $("#lblInfo").text();
                        $('#lblInfo').text("");
  
                        var txtRemarkDesc = document.getElementById("<%=txtRemarkDesc.ClientID%>");
                        var RevPreview = document.getElementById("<%=RevPreview.ClientID%>");
                        $('#txtRemarkDesc').val("");
                        $('#RevPreview').val("");

                        var show_pnl = document.getElementById('pnlMultiView');
                        show_pnl.style.display = "block";
                        show_pnl.style.visibility = "visible";

                        var show_pnltab = document.getElementById('pnlTab');
                        show_pnltab.style.display = "block";
                        show_pnltab.style.visibility = "visible";
                     

                        var show_pnltab = document.getElementById('pnlTab1');
                        show_pnltab.style.display = "block";
                        show_pnltab.style.visibility = "visible";

                        var butgreen = document.getElementById('btnRemarks');
                        butgreen.style.display = "block";
                        butgreen.style.visibility = "visible";

//                        var butgreen = document.getElementById('btnPreview');
//                        butgreen.style.display = "block";
//                        butgreen.style.visibility = "visible";

                        var butacc_val = butacc.innerHTML.split(',');
                        var isD = '0';
                        var isC = '0';
                        var isR = '0';
                        var isS = '0';
                        var isU = '0';
                        var isH = '0';
                        var isEntry = '0';
                        for (var i = 0; i < butacc_val.length; i++) {                                 
                            if (butacc_val[i] == "R") {
                                var butgreen = document.getElementById('btnRemarks');
                                butgreen.style.display = "block";
                                butgreen.style.visibility = "visible";
                                isR = '1';
                            }
                            else if (butacc_val[i] == "D") {
                                var butgreen = document.getElementById('btnListedDR');
                                butgreen.style.display = "block";
                                butgreen.style.visibility = "visible";
                                isD = '1';
                                isEntry = '1';
                            }
                            else if (butacc_val[i] == "C") {    
                                var butgreen = document.getElementById('btnChemists');
                                butgreen.style.display = "block";
                                butgreen.style.visibility = "visible";
                                isC = '1';
                                isEntry = '1';
                            }
                            else if (butacc_val[i] == "U") {
                                var butgreen = document.getElementById('btnNonListedDR');
                                butgreen.style.display = "block";
                                butgreen.style.visibility = "visible";
                                isU = '1';
                                isEntry = '1';
                            }
                            else if (butacc_val[i] == "H") {
                                var butgreen = document.getElementById('btnHospital');
                                butgreen.style.display = "block";
                                butgreen.style.visibility = "visible";
                                isH = '1';
                                isEntry = '1';
                            }
                            else if (butacc_val[i] == "S") {
                                var butgreen = document.getElementById('btnStockist');
                                butgreen.style.display = "block";
                                butgreen.style.visibility = "visible";
                                isS = '1';
                                isEntry = '1';
                            }
                            }
                        if (isEntry == "1") {
                                var hdnfwind = document.getElementById("<%=hdnfwind.ClientID%>");
                                hdnfwind.value = fwind.innerHTML;
                                var lblSDP = document.getElementById('lblSDP');
                                    lblSDP.style.display = "block";
                                    lblSDP.style.visibility = "visible";
                                var ddlSDP = document.getElementById("<%=ddlSDP.ClientID%>");
                                    ddlSDP.style.display = "block";
                                    ddlSDP.style.visibility = "visible";
                                    $('#ddlSDP').focus();
                                if (isD == "1")
                                    tabbut_Click('pnlTabLstDoc', 'pnlTabChem', 'PnlTabStk', 'PnlTabUnLst', 'PnlTabHos', 'PnlTabRem', 'PnlTabPrev', 'btnListedDR', 'btnChemists', 'btnStockist', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', '0');
                                else if (isC == "1")
                                    tabbut_Click('pnlTabChem', 'pnlTabLstDoc', 'PnlTabStk', 'PnlTabUnLst', 'PnlTabHos', 'PnlTabRem', 'PnlTabPrev', 'btnChemists', 'btnListedDR', 'btnStockist', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', '1');
                                else if (isS == "1")
                                    tabbut_Click('PnlTabStk', 'pnlTabChem', 'pnlTabLstDoc', 'PnlTabUnLst', 'PnlTabHos', 'PnlTabRem', 'PnlTabPrev', 'btnStockist', 'btnChemists', 'btnListedDR', 'btnNonListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', '2');
                                else if (isU == "1")
                                    tabbut_Click('PnlTabUnLst', 'pnlTabChem', 'PnlTabStk', 'pnlTabLstDoc', 'PnlTabHos', 'PnlTabRem', 'PnlTabPrev', 'btnNonListedDR', 'btnChemists', 'btnStockist', 'btnListedDR', 'btnHospital', 'btnRemarks', 'btnPreview', '3');
                                else if (isH == "1")
                                    tabbut_Click('PnlTabHos', 'pnlTabChem', 'PnlTabStk', 'PnlTabUnLst', 'pnlTabLstDoc', 'PnlTabRem', 'PnlTabPrev', 'btnHospital', 'btnChemists', 'btnStockist', 'btnNonListedDR', 'btnListedDR', 'btnRemarks', 'btnPreview', '4');
                                return false;
                            }
                            else if ((fwind.innerHTML == "N") || (fwind.innerHTML == "H") || (fwind.innerHTML == "W")) {
                                var hdnfwind = document.getElementById("<%=hdnfwind.ClientID%>");
                                hdnfwind.value = "N";            
                            tabbut_Click('PnlTabRem', 'pnlTabChem', 'PnlTabStk', 'PnlTabUnLst', 'PnlTabHos', 'pnlTabLstDoc', 'PnlTabPrev', 'btnRemarks', 'btnChemists', 'btnStockist', 'btnNonListedDR', 'btnListedDR', 'btnHospital', 'btnPreview', '5');

                            var lblSDP = document.getElementById('lblSDP');
                            if (lblSDP.style.display == "block") {
                                lblSDP.style.display = "none";
                                lblSDP.style.visibility = "hidden";
                            }

                            var ddlSDP = document.getElementById("<%=ddlSDP.ClientID%>");
                            if (ddlSDP.style.display == "block") {
                                ddlSDP.style.display = "none";
                                ddlSDP.style.visibility = "hidden";
                            }
                            return false;
                            }
                        else if (fwind.innerHTML == "L") {
                            var leavedate = $("#lblCurDate").text();
                            var show_pnl = document.getElementById('pnlMultiView');
                            show_pnl.style.display = "none";
                            show_pnl.style.visibility = "hidden";

                            var show_pnltab = document.getElementById('pnlTab');
                            show_pnltab.style.display = "none";
                            show_pnltab.style.visibility = "hidden";


                            var show_pnltab = document.getElementById('pnlTab1');
                            show_pnltab.style.display = "none";
                            show_pnltab.style.visibility = "hidden";
                        
                            var url = "../LeaveForm.aspx?LeaveFrom=" + leavedate
                            $(location).attr('href',url);
                            //openleave(leavedate);
                            return false;
                            }
                        }
                    }
                }
            }
    }
    function GiftAdd() {
        var txtGift = document.getElementById("<%=txtGift.ClientID%>");
        txtGift.value = "1";       
    }
    function UnGiftAdd() {
        var txtUnLstDR_GQty = document.getElementById("<%=txtUnLstDR_GQty.ClientID%>");
        txtUnLstDR_GQty.value = "1";
    }
    function newchemist() {
       
        var cval = true;
        var cheedit = document.getElementById("<%=hdncheedit.ClientID %>").value;
        var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
        if (grid != null) {
            if (grid.rows.length > 0) {
                for (i = 2; i < grid.rows.length + 1; i++) {
                    var cnt = 0;
                    var index = '';

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {
                        index = i.toString();
                    }
                    var max_chem_dcr_count = document.getElementById('GrdAdmStp_ctl' + index + '_max_chem_dcr_count');
                    if (cheedit.length == 0) {
                        var grdChem = document.getElementById('<%=grdChem.ClientID%>');
                        if (grdChem != null) {
                            if (grdChem.rows.length > 0) {
                                if (grdChem.rows.length - 1 == max_chem_dcr_count.innerHTML) {
                                    alert('Cannot enter more than ' + max_chem_dcr_count.innerHTML + ' Chemists');
                                    cval = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (cval == true) {

            disablepanel();
          
            var NewtxtChem = document.getElementById("<%=NewtxtChem.ClientID%>");         
            var txtChemNPOB = document.getElementById("<%=txtChemNPOB.ClientID%>");
            var txtChe = document.getElementById("<%=txtChe.ClientID%>");
            $('#NewtxtChem').val("");
            $('#txtChemNPOB').val("");
            var chkBox = document.getElementById("<%=ChkChem.ClientID%>")
            var checkbox = chkBox.getElementsByTagName("input");

            for (var i = 0; i < checkbox.length; i++) {
            var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
            if (chkBoxText[0].innerHTML == "SELF") {
                checkbox[i].checked = true;
            }
            else {
                checkbox[i].checked = false;
            }
            }
            $('#txtChe').val("SELF");
            $('#lblInfo').text("");
            $('#ddlTerr').prop('selectedIndex', 0);

            var PnlChem = document.getElementById("<%=PnlChem.ClientID%>");
            PnlChem.disabled = false;
            PnlChem.style.display = "block";
            PnlChem.style.visibility = "visible";
            PnlChem.className = "newchem";         
            var hdnsftype = document.getElementById("<%=hdnsftype.ClientID%>");
            if (hdnsftype.value == "2") {
               $('#lblcheMR').show();
               $('#ddlcheMR').show();

               $('#ddlcheMR').prop('selectedIndex', 0);
               document.getElementById("<%=ddlTerr.ClientID%>").disabled = true;
            }
            else {
                $('#lblcheMR').hide();
                $('#ddlcheMR').hide();
            }
        }
        return false;
    }
    function disablepanel() {     
         var pnlMultiView = document.getElementById("<%=pnlMultiView.ClientID%>").getElementsByTagName("input");  
         for (var i = 0; i < pnlMultiView.length; i++) {  
            pnlMultiView[i].disabled = true;
         }
         document.getElementById('<%= ddlSDP.ClientID %>').disabled = true;
         $(".edit").hide();
         $(".delete").hide();

         var pnlTab = document.getElementById("<%=pnlTab.ClientID%>").getElementsByTagName("input");
         for (var i = 0; i < pnlTab.length; i++) {
             pnlTab[i].disabled = true;  
         }
         var pnlTop = document.getElementById("<%=pnlTop.ClientID%>").getElementsByTagName("input");
         for (var i = 0; i < pnlTop.length; i++) {
             pnlTop[i].disabled = true;  
         }
         var pnlTab1 = document.getElementById("<%=pnlTab1.ClientID%>").getElementsByTagName("input");
         for (var i = 0; i < pnlTab1.length; i++) {
             pnlTab1[i].disabled = true;
         }
     }
     function enablepanel() {

         var pnlMultiView = document.getElementById("<%=pnlMultiView.ClientID%>").getElementsByTagName("input");
         for (var i = 0; i < pnlMultiView.length; i++) {
             pnlMultiView[i].disabled = false;
         }
         document.getElementById('<%= ddlSDP.ClientID %>').disabled = false;
        
         $(".edit").show();
        $(".delete").show();

         var pnlTab = document.getElementById("<%=pnlTab.ClientID%>").getElementsByTagName("input");
         for (var i = 0; i < pnlTab.length; i++) {
             pnlTab[i].disabled = false;
         }
         var pnlTop = document.getElementById("<%=pnlTop.ClientID%>").getElementsByTagName("input");
         for (var i = 0; i < pnlTop.length; i++) {
             pnlTop[i].disabled = false;
         }
         var pnlTab1 = document.getElementById("<%=pnlTab1.ClientID%>").getElementsByTagName("input");
         for (var i = 0; i < pnlTab1.length; i++) {
             pnlTab1[i].disabled = false;
         }
     }
     function CancelNewChem() {
         enablepanel();
         var cheedit = document.getElementById("<%=hdncheedit.ClientID %>").value;
         if (cheedit != '') {
             var grdChem = document.getElementById('<%=grdChem.ClientID%>');
             if (grdChem != null) {
                 if (grdChem.rows.length > 0) {
                     for (i = 0; i < grdChem.rows.length; i++) {
                         grdChem.rows[i].style.backgroundColor = '#ffffff';
                     }
                 }
             }
         }
         var PnlChem = document.getElementById("<%=PnlChem.ClientID%>");
         PnlChem.style.display = "none";
         PnlChem.style.visibility = "hidden";
         PnlChem.className = "newchem";
         return false;
     }

     function CancelNewUn() {
         enablepanel();
         var undocedit = document.getElementById("<%=hdnundocedit.ClientID %>").value;
         if (undocedit != '') {
             var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
             if (grdUnLstDR != null) {
                 if (grdUnLstDR.rows.length > 0) {
                     for (i = 0; i < grdUnLstDR.rows.length; i++) {
                         grdUnLstDR.rows[i].style.backgroundColor = '#ffffff';
                     }
                 }
             }
         }
         var NPnlUnLst = document.getElementById("<%=NPnlUnLst.ClientID%>");
         NPnlUnLst.style.display = "none";
         NPnlUnLst.style.visibility = "hidden";
         NPnlUnLst.className = "newundoc";
         return false;
     }
    function ValidateNewChem() {
        var cheedit = document.getElementById("<%=hdncheedit.ClientID %>").value;
        var type = $('#<%=ddlcheMR.ClientID%> :selected').text();
        if (type == "-Select-") { alert("Select Field Force"); $('#ddlcheMR').focus(); return false; }
        if ($("#NewtxtChem").val() == "") { alert("Enter Chemist Name"); $('#NewtxtChem').focus(); return false; }
        var sta = $('#<%=ddlTerr.ClientID%> :selected').text();
        if (sta == "---Select---") { alert("Select Territory"); $('#ddlTerr').focus(); return false; }
        else {
            var cval = true;
            var dupdoc = false;
            var lstChe = document.getElementById("<%=lstChe.ClientID%>");
            if (lstChe.options.length > 0) {
                for (i = 0; i < lstChe.options.length; i++) {
                    if ($("#NewtxtChem").val() == lstChe.options[i].text.trim()) {
                        dupdoc = true;
                        break;
                    }
                }
                if (dupdoc == true) {
                    alert('New Chemist already Exist');
                    cval = false;
                }
            }
            if (cval == true) {
                var grdChem = document.getElementById('<%=grdChem.ClientID%>');
                if (grdChem != null) {
                    if (grdChem.rows.length > 0) {
                        for (i = 2; i < grdChem.rows.length + 1; i++) {
                            var cnt = 0;
                            var index = '';

                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }
                            var chename = document.getElementById('grdChem_ctl' + index + '_lblChemists');

                            var chkedit = index - 1;
                            if (cheedit.length == 0) {
                                if ($("#NewtxtChem").val() == chename.innerHTML) {
                                    dupdoc = true;
                                    break;
                                }
                            }
                            else {

                                if (($("#NewtxtChem").val() == chename.innerHTML) && (cheedit != chkedit)) {
                                    dupdoc = true;
                                    break;
                                }
                            }
                        }
                        if (dupdoc == true) {
                            alert('Duplicate Chemist!!');
                            cval = false;

                        }

                    }
                }
            }
            if (cval == true) {
                var txtChemNPOB = document.getElementById("<%=txtChemNPOB.ClientID%>");
                if (txtChemNPOB.value.length > 0) {
                    if (isNaN(txtChemNPOB.value)) {
                        alert("Only Numeric Values Allowed for POB");
                        return false;
                    }
                }
                LoadFieldForceNewChem();
                document.getElementById("<%=hdncheedit.ClientID %>").value = '';
                enablepanel();
            }
            return cval;
        }
    }
    function btnback_click() {
       
        var hdnsftype = document.getElementById("<%=hdnsftype.ClientID%>");
        if (hdnsftype.value == "1") {
            var url = "../../../Default_MR.aspx" 
            $(location).attr('href', url);
        }
        else {
           
            var url = "../../MGR/DCR/DCRIndex.aspx"
            $(location).attr('href', url);
        }
        return false;
    }
    function valrem() {
     var txtRemarks = document.getElementById("<%=txtRemarks.ClientID%>");
     if (txtRemarks.value.length == 0) {
         alert("Enter Remarks");
         return false;
     }
     else {
         enablepanel();
         return true;
     }
    }
    function newunlisted() {

        var cval = true;
        var undocedit = document.getElementById("<%=hdnundocedit.ClientID %>").value;
        var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
        if (grid != null) {
            if (grid.rows.length > 0) {
                for (i = 2; i < grid.rows.length + 1; i++) {
                    var cnt = 0;
                    var index = '';

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {
                        index = i.toString();
                    }
                    var max_unlst_dcr_count = document.getElementById('GrdAdmStp_ctl' + index + '_max_unlst_dcr_count');
                    if (undocedit.length == 0) {
                        var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
                        if (grdUnLstDR != null) {
                            if (grdUnLstDR.rows.length > 0) {
                                if (grdUnLstDR.rows.length - 1 == max_unlst_dcr_count.innerHTML) {
                                    alert('Cannot enter more than ' + max_unlst_dcr_count.innerHTML + ' Un-Listed Doctors');
                                    cval = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (cval == true) {

            disablepanel();

            var txtUnDr = document.getElementById("<%=txtUnDr.ClientID%>");
            var txtUnDrAddr = document.getElementById("<%=txtUnDrAddr.ClientID%>");
            var txtN_UQty1 = document.getElementById("<%=txtN_UQty1.ClientID%>");
            var txtN_UQty2 = document.getElementById("<%=txtN_UQty2.ClientID%>");
            var txtN_UQty3 = document.getElementById("<%=txtN_UQty3.ClientID%>");
            var txtN_GQty = document.getElementById("<%=txtN_GQty.ClientID%>");
            var txtUn = document.getElementById("<%=txtUn.ClientID%>");

            $('#txtUnDr').val("");
            $('#txtUnDrAddr').val("");
            $('#txtN_UQty1').val("");
            $('#txtN_UQty2').val("");
            $('#txtN_UQty3').val("");
            $('#txtN_GQty').val("");
            var chkBox = document.getElementById("<%=chkUnLstDR.ClientID%>")
            var checkbox = chkBox.getElementsByTagName("input");

            for (var i = 0; i < checkbox.length; i++) {
                var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                if (chkBoxText[0].innerHTML == "SELF") {
                    checkbox[i].checked = true;
                }
                else {
                    checkbox[i].checked = false;
                }
            }
            $('#txtUn').val("SELF");
            $('#lblInfo').text("");
            $('#ddlQual_Un').prop('selectedIndex', 0);
            $('#ddlTerr_Un').prop('selectedIndex', 0);
            $('#ddlSpec_Un').prop('selectedIndex', 0);
            $('#ddlCate_Un').prop('selectedIndex', 0);
            $('#ddlClass_Un').prop('selectedIndex', 0);
            $('#ddlN_unsess').prop('selectedIndex', 0);
            $('#ddlN_untime').prop('selectedIndex', 00);
            $('#ddlN_unmin').prop('selectedIndex', 00);
            $('#ddlN_unProd1').prop('selectedIndex', 0);
            $('#ddlN_unProd2').prop('selectedIndex', 0);
            $('#ddlN_unProd3').prop('selectedIndex', 0);
            $('#ddlN_ungift').prop('selectedIndex', 0);
            var hdnsftype = document.getElementById("<%=hdnsftype.ClientID%>");
            var NPnlUnLst = document.getElementById("<%=NPnlUnLst.ClientID%>");
            NPnlUnLst.style.display = "block";
            NPnlUnLst.style.visibility = "visible";
            NPnlUnLst.className = "newundoc";

            if (hdnsftype.value == "2") {
                $('#lblUnMR').show();
                $('#ddlUnMR').show();

                $('#ddlUnMR').prop('selectedIndex', 0);
                document.getElementById("<%=ddlTerr_Un.ClientID%>").disabled = true;

            }
            else {
                $('#lblUnMR').hide();
                $('#ddlUnMR').hide();
            }
          
        }
     
        return false;
    }
    function ValidateNewUnDoc() {
        var undocedit = document.getElementById("<%=hdnundocedit.ClientID %>").value;
        var type = $('#<%=ddlUnMR.ClientID%> :selected').text();
        if (type == "-Select-") { alert("Select Field Force"); $('#ddlUnMR').focus(); return false; }
        if ($("#txtUnDr").val() == "") { alert("Enter Name"); $('#txtUnDr').focus(); return false; }
        if ($("#txtUnDrAddr").val() == "") { alert("Enter Address"); $('#txtUnDrAddr').focus(); return false; }
        var sta = $('#<%=ddlTerr_Un.ClientID%> :selected').text();
        if (sta == "---Select---") { alert("Select Territory"); $('#ddlTerr_Un').focus(); return false; }
        var sta = $('#<%=ddlQual_Un.ClientID%> :selected').text();
        if (sta == "---Select---") { alert("Select Qualification"); $('#ddlQual_Un').focus(); return false; }
        var sta = $('#<%=ddlSpec_Un.ClientID%> :selected').text();
        if (sta == "---Select---") { alert("Select Speciality"); $('#ddlSpec_Un').focus(); return false; }
        var sta = $('#<%=ddlCate_Un.ClientID%> :selected').text();
        if (sta == "---Select---") { alert("Select Category"); $('#ddlCate_Un').focus(); return false; }
        var sta = $('#<%=ddlClass_Un.ClientID%> :selected').text();
        if (sta == "---Select---") { alert("Select Class"); $('#ddlClass_Un').focus(); return false; }
        else 
        {
            var dupdoc = false;
            var cval = true;
            var UnLstDr = document.getElementById("<%=UnLstDr.ClientID%>");
            if (UnLstDr.options.length > 0) {
                    for (i = 0; i < UnLstDr.options.length; i++) {
                    if ($("#txtUnDr").val() == UnLstDr.options[i].text.trim()) {
                        dupdoc = true;
                        break;
                    }
                }
            }
            if (dupdoc == true) {
                alert('New Un-listed Doctor already Exist');
                cval = false;
            }

            if (cval == true) {
                var grdUnLstDR = document.getElementById('<%=grdUnLstDR.ClientID%>');
                if (grdUnLstDR != null) {
                    if (grdUnLstDR.rows.length > 0) {
                        for (i = 2; i < grdUnLstDR.rows.length + 1; i++) {
                            var cnt = 0;
                            var index = '';

                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }
                            var drname = document.getElementById('grdUnLstDR_ctl' + index + '_lblUnLstDR_DR');

                            var chkedit = index - 1;
                            if (undocedit.length == 0) {
                                if ($("#txtUnDr").val() == drname.innerHTML) {
                                    dupdoc = true;
                                    break;
                                }
                            }
                            else {
                                if (($("#txtUnDr").val() == drname.innerHTML) && (undocedit != chkedit)) {
                                    dupdoc = true;
                                    break;
                                }
                            }
                        }            
                    }
                }
                if (dupdoc == true) {
                    alert('Duplicate Un-Listed Doctor!!');
                    cval = false;
                }
            }
            if (cval == true) {
                LoadFieldForceNewUn();
                  document.getElementById("<%=hdnundocedit.ClientID %>").value = '';
                  enablepanel();
            }
            return cval;
        }  
    }

    </script>
     
    <script type="text/javascript">

        function LimtCharacters(txtMsg, indicator) {
            var txtmrmk = document.getElementById("<%=HdnMRmk.ClientID%>");
            txtmrmk = txtMsg.value;
            chars = txtMsg.value.length;
            var CharLength = 0;
            var grid = document.getElementById('<%=GrdAdmStp.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }
                        var remarks_len = document.getElementById('GrdAdmStp_ctl' + index + '_remarks_len');
                        CharLength = remarks_len.innerHTML; ;
                    }
                }
            }
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtMsg.value = txtMsg.value.substring(0, CharLength);
            }
        }
        function DisableRightClick(event) {

            if (event.button == 2) {

                alert("Right Clicking not allowed!");

            }

        }

        function DisableCtrlKey(e) {

            var code = (document.all) ? event.keyCode : e.which;

            var message = "Ctrl key functionality is disabled!";

            if (parseInt(code) == 17) {

                alert(message);

                window.event.returnValue = false;

            }

        }
//        $(document).ready(function () {
//            $('#<%=gvDCR.ClientID %>').Scrollable({
//                ScrollHeight: 400,
//                IsInUpdatePanel: true
//            });
//            $('#<%=grdChem.ClientID %>').Scrollable({
//                ScrollHeight: 400,
//                IsInUpdatePanel: true
//            });
//            $('#<%=GridStk.ClientID %>').Scrollable({
//                ScrollHeight: 400,
//                IsInUpdatePanel: true
//            });
//            $('#<%=grdUnLstDR.ClientID %>').Scrollable({
//                ScrollHeight: 400,
//                IsInUpdatePanel: true
//            });
//            $('#<%=GridHospital.ClientID %>').Scrollable({
//                ScrollHeight: 400,
//                IsInUpdatePanel: true
//            });
//        });
        $(document).ready(function () {
            $('body').bind('cut copy paste', function (e) {
                e.preventDefault();
            });
        });
        document.onkeydown = function (e) {
            var event = window.event || e;
            if (event.keyCode == 116) {
                event.keyCode = 0;
                alert("This action is not allowed");
                return false;
            }
        }

        $(document).bind('keypress', function (event) {
            if (event.which === 13 && event.shiftKey) {
                var bt = document.getElementById("<%=btnSubmit.ClientID%>");
                bt.click();
                return false;
            }
        });
    </script>
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            var hdnsftype = document.getElementById("<%=hdnsftype.ClientID%>");
            var hdnbutname = document.getElementById("<%=hdnbutname.ClientID%>");
            if (hdnsftype.value == "1") {
               //fixpos(hdnbutname.value);
                ddlSDP_onchange();
            }
            else if (hdnsftype.value == "2") {
                mgrcolor();
            }
        }
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
     </script>

     <script type="text/javascript">

         function ddlSDP_onchange() {
             HideList();
             CheHide();
             HideUnDrList();
             var SelectedText = $('#ddlSDP').find(":selected").text();
             var SelectedValue = $('#ddlSDP').val();
             var hdnbutname = document.getElementById("<%=hdnbutname.ClientID%>");
            
             if (hdnbutname.value == '0') {
                 
                    FillDoc_New(SelectedValue);
                    FillChem_New(SelectedValue);
                    FillUn_New(SelectedValue);
                 }
                 else if (hdnbutname.value == '1') {
                     FillChem_New(SelectedValue);
                     FillDoc_New(SelectedValue);
                     FillUn_New(SelectedValue);
                 }
                 else if (hdnbutname.value == '3') {
                     FillUn_New(SelectedValue);
                     FillDoc_New(SelectedValue);
                     FillChem_New(SelectedValue);           
                 }
                 else {              
                     FillDoc_New(SelectedValue);
                     FillChem_New(SelectedValue);
                     FillUn_New(SelectedValue);
                 }
             }

             function mgrcolor() {
                //listeddr color
                var ddl = document.getElementById("<%=mgrdoclist.ClientID%>");
                var lstdoc = document.getElementById("<%=lstListDR.ClientID%>");
                for (i = 0; i < ddl.options.length; i++) {
                    lstdoc.options[i].style.cssText = "background-color:" + ddl.options[i].text;
                }
                //chemist color
                var ddl = document.getElementById("<%=mgrchelist.ClientID%>");
                var lstChe = document.getElementById("<%=lstChe.ClientID%>");
                for (i = 0; i < ddl.options.length; i++) {
                    lstChe.options[i].style.cssText = "background-color:" + ddl.options[i].text;
                }
                
                //unlisteddr color
                var ddl = document.getElementById("<%=mgrundoclist.ClientID%>");
                var UnLstDr = document.getElementById("<%=UnLstDr.ClientID%>");
                for (i = 0; i < ddl.options.length; i++) {
                    UnLstDr.options[i].style.cssText = "background-color:" + ddl.options[i].text;
                }

            }
            function loadterrmr() {
               
                var SelectedText = $('#ddlcheMR').find(":selected").text();
                var SelectedValue = $('#ddlcheMR').val();
                var ddlTerr = document.getElementById("<%=ddlTerr.ClientID%>");
              
                ddlTerr.options.length = 0;
                var ddlAllTerr = document.getElementById("<%=ddlAllTerr.ClientID%>");
                var ddlsfterr = document.getElementById("<%=ddlsfterr.ClientID%>");

                for (i = 0; i < ddlAllTerr.options.length; i++) {
                    if ((ddlsfterr.options[i].text == SelectedValue) || (ddlsfterr.options[i].text == "ALL")) {
                        var opt = document.createElement("option");
                        opt.text = ddlAllTerr.options[i].text;
                        opt.value = ddlAllTerr.options[i].value;
                        ddlTerr.options.add(opt);
                    }
                }
                document.getElementById("<%=ddlTerr.ClientID%>").disabled = false;
            }
            function loadundocterrmr() {
               
                var SelectedText = $('#ddlUnMR').find(":selected").text();
                var SelectedValue = $('#ddlUnMR').val();
                var ddlTerr_Un = document.getElementById("<%=ddlTerr_Un.ClientID%>");

                ddlTerr_Un.options.length = 0;
                var ddlAllTerr = document.getElementById("<%=ddlAllTerr.ClientID%>");
                var ddlsfterr = document.getElementById("<%=ddlsfterr.ClientID%>");

                for (i = 0; i < ddlAllTerr.options.length; i++) {
                    if ((ddlsfterr.options[i].text == SelectedValue) || (ddlsfterr.options[i].text == "ALL")) {
                        var opt = document.createElement("option");
                        opt.text = ddlAllTerr.options[i].text;
                        opt.value = ddlAllTerr.options[i].value;
                        ddlTerr_Un.options.add(opt);
                    }
                }
                document.getElementById("<%=ddlTerr_Un.ClientID%>").disabled = false;
            }
            function loadterrmr() {
                var SelectedText = $('#ddlcheMR').find(":selected").text();
                var SelectedValue = $('#ddlcheMR').val();
                var ddlTerr = document.getElementById("<%=ddlTerr.ClientID%>");

                ddlTerr.options.length = 0;
                var ddlAllTerr = document.getElementById("<%=ddlAllTerr.ClientID%>");
                var ddlsfterr = document.getElementById("<%=ddlsfterr.ClientID%>");

                for (i = 0; i < ddlAllTerr.options.length; i++) {
                    if ((ddlsfterr.options[i].text == SelectedValue) || (ddlsfterr.options[i].text == "ALL")) {
                        var opt = document.createElement("option");
                        opt.text = ddlAllTerr.options[i].text;
                        opt.value = ddlAllTerr.options[i].value;
                        ddlTerr.options.add(opt);
                    }
                }
                document.getElementById("<%=ddlTerr.ClientID%>").disabled = false;
            }

            function FillDoc_New(val) {

                 var newdocval = new Array();
                 var newdocname = new Array();
                 var restdocval = new Array();
                 var restdocname = new Array();

                 var minpos = 0;
                 var maxpos = 0;
                 var cnt = 0;
                 var ismax = "1";

                 var ddl = document.getElementById("<%=ddl_terr_doc.ClientID%>");
                 for (i = 0; i < ddl.options.length; i++) {
                     if (val == ddl.options[i].text) {
                         cnt = cnt + 1;
                     }
                 }

                 var duplist = document.getElementById("<%=ddldoclist.ClientID%>");
                 // find the minposition
                 for (i = 0; i < ddl.options.length; i++) {
                     if (val == ddl.options[i].text) {
                         minpos = i
                         break;
                     }
                 }

                 //find the maxposition
                 for (i = minpos; i < ddl.options.length; i++) {
                     if (val != ddl.options[i].text) {
                         maxpos = i - 1;
                         break;
                     }
                     else if (i == (ddl.options.length - 1)) {
                         maxpos = i;
                         ismax = "2";
                         break;
                     }
                 }

                 var newpos = 0;
                 var restpos = 0;
                 if (maxpos != 0) {
                     for (i = minpos; i <= maxpos; i++) {
                         newdocval[newpos] = duplist.options[i].value;
                         newdocname[newpos] = duplist.options[i].text;
                         newpos = newpos + 1;
                     }

                     for (i = 0; i < minpos; i++) {
                         restdocname[restpos] = duplist.options[i].text;
                         restpos = restpos + 1;
                     }
                     if (ismax == "1") {
                         for (i = maxpos + 1; i < duplist.options.length; i++) {
                             restdocname[restpos] = duplist.options[i].text;
                             restpos = restpos + 1;
                         }
                     }
                     if (ismax == "2") {
                         for (i = maxpos; i < duplist.options.length; i++) {
                             restdocname[restpos] = duplist.options[i].text;
                             restpos = restpos + 1;
                         }
                     }
                     

                     // Sort the Remaining items
                     restdocname.sort();

                     for (i = 0; i < restpos; i++) {
                         for (j = 0; j < duplist.options.length; j++) {
                             if (restdocname[i] == duplist.options[j].text) {
                                 restdocval[i] = duplist.options[j].value;
                                 break;
                             }
                         }
                     }

                     var lstdoc = document.getElementById("<%=lstListDR.ClientID%>");
                     for (i = 0; i < cnt; i++) {
                         lstdoc.options[i].style.cssText = "background-color:" + '#ACFA58';
                         lstdoc.options[i].value = newdocval[i];
                         lstdoc.options[i].text = newdocname[i];
                     }
                     for (i = 0; i < restpos; i++) {
                         if ((i + cnt) < lstdoc.options.length) {
                             lstdoc.options[i + cnt].style.cssText = "background-color:" + '#FFFFFF';
                             lstdoc.options[i + cnt].value = restdocval[i];
                             lstdoc.options[i + cnt].text = restdocname[i];
                         } 
                     }

                 } 
             }


              function FillChem_New(val) {
                  var newcheval = new Array();
                  var newchename = new Array();
                  var restcheval = new Array();
                  var restchename = new Array();

                  var minpos = 0;
                  var maxpos = 0;
                  var cnt = 0;
                  var ismax = "1";

                  var ddl = document.getElementById("<%=ddl_terr_che.ClientID%>");
                  for (i = 0; i < ddl.options.length; i++) {
                      if (val == ddl.options[i].text) {
                          cnt = cnt + 1;
                      }
                  }

                  var duplist = document.getElementById("<%=ddlchelist.ClientID%>");
                  // find the minposition
                  for (i = 0; i < ddl.options.length; i++) {
                      if (val == ddl.options[i].text) {
                          minpos = i
                          break;
                      }
                  }

                  //find the maxposition
                  for (i = minpos; i < ddl.options.length; i++) {
                      if (val != ddl.options[i].text) {
                          maxpos = i - 1;
                          break;
                      }
                      else if (i == (ddl.options.length - 1)) {
                          maxpos = i;
                          ismax = "2";
                          break;
                      }
                  }


                  var newpos = 0;
                  var restpos = 0;
                  if (maxpos != 0) {
                      for (i = minpos; i <= maxpos; i++) {
                          newcheval[newpos] = duplist.options[i].value;
                          newchename[newpos] = duplist.options[i].text;
                          newpos = newpos + 1;
                      }

                      for (i = 0; i < minpos; i++) {
                          restchename[restpos] = duplist.options[i].text;
                          restpos = restpos + 1;
                      }
                      if (ismax == "1") {
                          for (i = maxpos + 1; i < duplist.options.length; i++) {
                              restchename[restpos] = duplist.options[i].text;
                              restpos = restpos + 1;
                          }
                      }
                      else if (ismax == "2") {
                          for (i = maxpos; i < duplist.options.length; i++) {
                              restchename[restpos] = duplist.options[i].text;
                              restpos = restpos + 1;
                          }
                      }
                      // Sort the Remaining items
                      restchename.sort();

                      for (i = 0; i < restpos; i++) {
                          for (j = 0; j < duplist.options.length; j++) {
                              if (restchename[i] == duplist.options[j].text) {
                                  restcheval[i] = duplist.options[j].value;
                                  break;
                              }
                          }
                      }

                      var lstChe = document.getElementById("<%=lstChe.ClientID%>");
                      for (i = 0; i < cnt; i++) {
                          lstChe.options[i].style.cssText = "background-color:" + '#ACFA58';
                          lstChe.options[i].value = newcheval[i];
                          lstChe.options[i].text = newchename[i];
                      }
                      for (i = 0; i < restpos; i++) {
                          if ((i + cnt) < lstChe.options.length) {
                              lstChe.options[i + cnt].style.cssText = "background-color:" + '#FFFFFF';
                              lstChe.options[i + cnt].value = restcheval[i];
                              lstChe.options[i + cnt].text = restchename[i];
                          }
                      }
                  }
              }


              function FillUn_New(val) {
                  var newundocval = new Array();
                  var newundocname = new Array();
                  var restundocval = new Array();
                  var restundocname = new Array();

                  var minpos = 0;
                  var maxpos = 0;
                  var cnt = 0;
                  var ismax = "1";
                  var ddl = document.getElementById("<%=ddl_terr_Undoc.ClientID%>");

                  for (i = 0; i < ddl.options.length; i++) {
                      if (val == ddl.options[i].text) {
                          cnt = cnt + 1;
                      }
                  }

                  var duplist = document.getElementById("<%=ddlundoclist.ClientID%>");
                  // find the minposition
                  for (i = 0; i < ddl.options.length; i++) {
                      if (val == ddl.options[i].text) {
                          minpos = i
                          break;
                      }
                  }

                  //find the maxposition
                  for (i = minpos; i < ddl.options.length; i++) {
                      if (val != ddl.options[i].text) {
                          maxpos = i - 1;
                          break;
                      }
                      else if (i == (ddl.options.length - 1)) {
                          maxpos = i;
                          ismax = "2";
                          break;
                      }
                  }


                  var newpos = 0;
                  var restpos = 0;
                  if (maxpos != 0) {
                      for (i = minpos; i <= maxpos; i++) {
                          newundocval[newpos] = duplist.options[i].value;
                          newundocname[newpos] = duplist.options[i].text;
                          newpos = newpos + 1;
                      }

                      for (i = 0; i < minpos; i++) {
                          restundocname[restpos] = duplist.options[i].text;
                          restpos = restpos + 1;
                      }
                      if (ismax == "1") {
                      for (i = maxpos + 1; i < duplist.options.length; i++) {
                          restundocname[restpos] = duplist.options[i].text;
                          restpos = restpos + 1;
                      }
                      }
                      else if (ismax == "2") {
                       for (i = maxpos; i < duplist.options.length; i++) {
                          restundocname[restpos] = duplist.options[i].text;
                          restpos = restpos + 1;
                      }
                      }

                      // Sort the Remaining items
                      restundocname.sort();

                      for (i = 0; i < restpos; i++) {
                          for (j = 0; j < duplist.options.length; j++) {
                              if (restundocname[i] == duplist.options[j].text) {
                                  restundocval[i] = duplist.options[j].value;
                                  break;
                              }
                          }
                      }

                      var UnLstDr = document.getElementById("<%=UnLstDr.ClientID%>");
                      for (i = 0; i < cnt; i++) {
                          UnLstDr.options[i].style.cssText = "background-color:" + '#ACFA58';
                          UnLstDr.options[i].value = newundocval[i];
                          UnLstDr.options[i].text = newundocname[i];
                      }
                      for (i = 0; i < restpos; i++) {
                          if ((i + cnt) < UnLstDr.options.length) {
                              UnLstDr.options[i + cnt].style.cssText = "background-color:" + '#FFFFFF';
                              UnLstDr.options[i + cnt].value = restundocval[i];
                              UnLstDr.options[i + cnt].text = restundocname[i];
                          }
                      }
                  }
              }      
   </script>
   
     <ajax:ToolkitScriptManager ID="ScriptManager1"  runat="server" />
    <asp:UpdatePanel ID="Uplworktype" runat="server">
    <ContentTemplate>
       <center>
        <asp:Panel ID="PnlInfo" runat="server" Style=" margin-top: 200px; margin-left: 550px;
            position: absolute;">
                 <asp:Label id="lblInfo" runat="server" style="border: 0px; background-color: transparent;
                font-size: 32px; font-family: TimesNewRoman; color: Red;  " Text="Select the WorkType..." />
        </asp:Panel>
        </center>

         <asp:Panel ID="PnlChem" runat="server"  CssClass = "newchem" >
            <asp:HiddenField ID="hdnChe" runat="server" />
           <table bgcolor="white" cellpadding="5" cellspacing="5" style="border-style:solid; border-width:thin; border-collapse:collapse ">
                <tr bgcolor="#999966" >
                    <td bgcolor="#999966" style="width: 400px" class="stylespc">
                        <asp:Label ID="lbl" runat="server" Text="&nbsp;New Chemist"   Font-Names="Verdana" Font-Size="10px" Font-Bold = "true" 
                            ForeColor="White"></asp:Label>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Close.gif" ImageAlign="Right"
                            OnClientClick="return HideChem();" />
                    </td>
                </tr>
                 <tr>
                    <td style="width: 300px;" class="stylespc">
                       <asp:Label ID="lblcheMR" runat="server" Text="&nbsp;FieldForce: "    Width = "70px" Font-Names="Verdana" Font-Size="10px" CssClass="pnl_label" ></asp:Label>
                       <asp:DropDownList ID="ddlcheMR" runat="server" Width="150px"   SkinID="ddlRequired"  onchange ="loadterrmr();"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px" class="stylespc">
                        <asp:Label ID="Label6" runat="server" Text="&nbsp;Name: " Width = "70px" Font-Names="Verdana" Font-Size="10px" CssClass="pnl_label" ></asp:Label>
                        <asp:TextBox ID="NewtxtChem" Font-Names="Verdana" Font-Size="10px" SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event)" Width = "250px" runat="server" MaxLength = "100" ></asp:TextBox>
                        <asp:hiddenfield id="hdnnewpnlchem"  runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px" class="stylespc">
                     <asp:Label ID="lblTerr" runat="server" Text="&nbsp;Territory: " Width = "70px" CssClass="pnl_label"></asp:Label>
                        <asp:DropDownList ID="ddlTerr" runat="server" Width="120px" SkinID="ddlRequired"  ></asp:DropDownList>
                     <asp:Label ID="lblWorkWithNewChe" runat="server" Text=" &nbsp; &nbsp; JFW: " Width = "50px" CssClass="pnl_label"></asp:Label>
                      <asp:TextBox ID="txtChe" runat="server" Text="SELF"  Width="120px" SkinID="MandTxtBox"  ></asp:TextBox>
                        <asp:HiddenField ID="ChehdnId" runat="server"></asp:HiddenField>
                        <ajax:PopupControlExtender ID="txtChe_PopupControlExtender" runat="server" Enabled="True" 
                            ExtenderControlID=""  TargetControlID="txtChe" PopupControlID="PnlNewChe" OffsetY="22">
                        </ajax:PopupControlExtender>
                        <asp:Panel ID="PnlNewChe" runat="server" Height="150px" Width="200px" BorderStyle="Solid"
                            BorderWidth="1px" CssClass="collp" Direction="LeftToRight" ScrollBars="Auto" BackColor="LemonChiffon" 
                            Style="display: none">
                            <div style="height:15px; position:relative; background-color: #999966; 
                                    text-transform: capitalize; width:100%; float: left" align="right">
                                    <asp:Button ID="btnNewCheClose" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color:Yellow; 
                                        color: Black; " Text="X" runat="server"  OnClientClick="return HideChePopup();" /> 
                            </div>
                            <asp:CheckBoxList ID="ChkChem" runat="server" Width="180px"  BorderStyle="None" CssClass="collp"
                                              DataTextField="sf_name" DataValueField="sf_code" OnClick="LoadFieldForceNewChem();">
                            </asp:CheckBoxList>
                         </asp:Panel>
                    </td>
                </tr>
                <tr>
                <td class="stylespc">
                   <asp:Label ID="lblNPOB" runat="server" Text="&nbsp;POB:"  Width = "70px" Font-Names="Verdana" Font-Size="10px" CssClass="pnl_label"></asp:Label>
                        <asp:TextBox ID="txtChemNPOB" Font-Names="Verdana" onkeypress="CheckNumeric(event);" MaxLength="6" Font-Size="10px" runat="server" SkinID="MandTxtBox" 
                        onKeyDown="return DisableCtrlKey(event)" onMouseDown="DisableRightClick(event)" ></asp:TextBox>
                </td>
                  
                </tr>
                <tr bgcolor="#999966">
                   <td style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px" >
                        <asp:Button ID="btnChe" runat="server"  Width="50px" Height="22"   Text="Done" Font-Names="Verdana" Font-Size="9px"
                            OnClientClick="return ValidateNewChem();" OnClick="btnChe_Click" />
                        <asp:Button ID="Button2" runat="server"  Text="Cancel" Font-Names="Verdana"  Width="50px" Height="22" Font-Size="9px"
                            OnClientClick="return CancelNewChem();"  />
                    
                    </td>
                </tr>
            </table>
                    
        </asp:Panel>
        
          <asp:Panel ID="NPnlUnLst" runat="server" CssClass = "newundoc" >
            <asp:HiddenField ID="hdnUnlst" runat="server" />
           <table bgcolor="white"  width ="450px" cellpadding="5" cellspacing="5" style="border-style:solid; border-width:thin; border-collapse:collapse ">
                <tr bgcolor="#999966">
                    <td bgcolor="#999966"  style="width: 450px" >
                        <asp:Label ID="Label5" runat="server" Text="&nbsp;New UnListed Doctor"   Font-Names="Verdana" Font-Size="10px" Font-Bold = "true" 
                            ForeColor="White"></asp:Label>
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Close.gif" ImageAlign="Right"
                            OnClientClick="return HideUnlst();"/>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 400px" class="stylespc">
                        <asp:Label ID="lblUnMR" runat="server" Text="&nbsp;FieldForce"   Width = "70px" Font-Names="Verdana" Font-Size="10px" CssClass="pnl_label" ></asp:Label>
                        <asp:DropDownList ID="ddlUnMR" runat="server" Width="150px"   SkinID="ddlRequired"  onchange = "loadundocterrmr();" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 400px" class="stylespc">
                        <asp:Label ID="Label7" runat="server" Text="&nbsp;Name " Width = "70px" Font-Names="Verdana" Font-Size="10px" CssClass="pnl_label"  ></asp:Label>
                        <asp:TextBox ID="txtUnDr" Font-Names="Verdana" Font-Size="10px" Width = "300px" runat="server" MaxLength = "150" SkinID = "TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event)"></asp:TextBox>
                        <asp:hiddenfield id="hdnNewUnDoc"  runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 400px" class="stylespc">
                        <asp:Label ID="Label11" runat="server" Text="&nbsp;Address " Width = "70px" Font-Names="Verdana" Font-Size="10px" CssClass="pnl_label" ></asp:Label>
                        <asp:TextBox ID="txtUnDrAddr" Font-Names="Verdana" onkeypress="AlphaNumeric(event);" Font-Size="10px" Width = "300px" runat="server" SkinID="MandTxtBox"></asp:TextBox>
                    </td>
                </tr>
                <tr >
                    <td style="width: 500px" class="stylespc">
                     <asp:Label ID="Label8" runat="server" Text="&nbsp;Territory " Width = "70px" CssClass="pnl_label"></asp:Label>
                        <asp:DropDownList ID="ddlTerr_Un" runat="server" Width="100px" SkinID="ddlRequired" > </asp:DropDownList>
                   
                    <asp:Label ID="Label12" runat="server" Text="&nbsp;Quali " Width = "50px" CssClass="pnl_label"></asp:Label>
                         <asp:DropDownList ID="ddlQual_Un" runat="server" Width="100px" SkinID="ddlRequired" >  </asp:DropDownList>
                    </td>
                 </tr>
                 <tr>
                   <td style="width: 500px" class="stylespc">
                     <asp:Label ID="Label13" runat="server" Text="&nbsp;Speciality " Width = "70px" CssClass="pnl_label"></asp:Label>
                        <asp:DropDownList ID="ddlSpec_Un" runat="server" Width="100px" SkinID="ddlRequired"  ></asp:DropDownList>
                    
                     <asp:Label ID="Label14" runat="server" Text="&nbsp;Category " Width = "50px" CssClass="pnl_label"></asp:Label>
                         <asp:DropDownList ID="ddlCate_Un" runat="server" Width="120px" SkinID="ddlRequired"  ></asp:DropDownList>
                    </td>
                    
                  </tr>
                  <tr>
                          <td style="width: 500px" class="stylespc">
                     <asp:Label ID="Label18" runat="server" Text="&nbsp;Class " Width = "70px" CssClass="pnl_label"></asp:Label>
                         <asp:DropDownList ID="ddlClass_Un" runat="server" Width="100px" SkinID="ddlRequired"> </asp:DropDownList>
                   
                      <asp:Label ID="Label19" runat="server" Text="&nbsp;Session " Width = "50px" CssClass="pnl_label"></asp:Label>
                            <asp:DropDownList ID="ddlN_unsess" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="M"></asp:ListItem>
                                <asp:ListItem Value="2" Text="E"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                  </tr>
                  <tr>
                    <td style="width: 500px" class="stylespc">
                        <asp:Label ID="Label20" runat="server" Text="&nbsp;Time " Width = "70px" CssClass="pnl_label"></asp:Label>
                                <asp:DropDownList ID="ddlN_untime" runat="server"  Width = "50px" SkinID="ddlRequired">
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                </asp:DropDownList>
                                
                                <asp:DropDownList ID="ddlN_unmin" runat="server" Width = "50px" SkinID="ddlRequired">
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                    <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                    <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                    <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                    <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                    <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                    <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                    <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                    <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                    <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                    <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                    <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                    <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                    <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                    <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                    <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                    <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                    <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                    <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                    <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                    <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                    <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                    <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                    <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                    <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                    <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                </asp:DropDownList>
                       
             
                        <asp:Label ID="Label9" runat="server" CssClass="pnl_label" 
                            Text="&nbsp;JFW: " Width="50px"></asp:Label>
                        <asp:TextBox ID="txtUn" runat="server" SkinID="MandTxtBox" Text="SELF" 
                            Width="150px"></asp:TextBox>
                        <asp:HiddenField ID="unhidDr" runat="server" />
                        <ajax:PopupControlExtender ID="txtUn_PopupControlExtender1" runat="server" 
                            Enabled="True" ExtenderControlID="" OffsetY="22" PopupControlID="PnlNewUn" 
                            TargetControlID="txtUn">
                        </ajax:PopupControlExtender>
                        <asp:Panel ID="PnlNewUn" runat="server" BackColor="LemonChiffon" 
                            BorderStyle="Solid" BorderWidth="1px" CssClass="collp" Direction="LeftToRight" 
                            Height="150px" ScrollBars="Auto" Style="display: none" Width="200px">
                            <div align="right" style="height:15px; position:relative; background-color: #999966; 
                                    text-transform: capitalize; width:100%; float: left">
                                <asp:Button ID="Button1" runat="server" OnClientClick="return HideUnPopup();" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color:Yellow; 
                                        color: Black; margin-top: -1px;" Text="X" />
                            </div>
                            <asp:CheckBoxList ID="ChkUn" runat="server" 
                                BorderStyle="None" CssClass="collp" DataTextField="sf_name" OnClick="LoadFieldForceNewUn();"
                                DataValueField="sf_code" 
                                Width="180px">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        </td> 
                        </tr> 
                    <tr>
                    <td style="width: 500px" class="stylespc">
                    
                        <asp:Label ID="Label15" runat="server" CssClass="pnl_label" 
                            Text="&nbsp;Prod(1)" Width="70px"></asp:Label>
                        <asp:DropDownList ID="ddlN_unProd1" runat="server" SkinID="ddlRequired" 
                            Width="100px">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtN_UQty1" runat="server" Font-Names="Verdana" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"
                            Font-Size="10px" Width="25px"></asp:TextBox>
                   
                                    <asp:Label ID="Label16" runat="server" CssClass="pnl_label" 
                                        Text="&nbsp;&nbsp;Prod(2)" Width="50px"></asp:Label>
                                    <asp:DropDownList ID="ddlN_unProd2" runat="server" SkinID="ddlRequired" 
                                        Width="100px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtN_UQty2" runat="server" Font-Names="Verdana" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"
                                        Font-Size="10px" Width="25px"></asp:TextBox>
                               </td> 
                               </tr>
                               <tr>
                                <td style="width: 500px" class="stylespc">
                                    <asp:Label ID="Label17" runat="server" CssClass="pnl_label" 
                                        Text="&nbsp;Prod(3) " Width="70px"></asp:Label>
                                    <asp:DropDownList ID="ddlN_unProd3" runat="server" SkinID="ddlRequired" 
                                        Width="100px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtN_UQty3" runat="server" Font-Names="Verdana" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"
                                        Font-Size="10px" Width="25px"></asp:TextBox>
                                    <asp:Label ID="Label10" runat="server" CssClass="pnl_label" 
                                        Text="&nbsp;&nbsp;Input " Width="50px"></asp:Label>
                                    <asp:DropDownList ID="ddlN_ungift" runat="server" SkinID="ddlRequired" 
                                        Width="100px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtN_GQty" runat="server" Font-Names="Verdana" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"
                                        Font-Size="10px"  Width="25px"></asp:TextBox>
                                </td>
                            </tr>
                <tr bgcolor="#999966">
          
                   <td style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px" class="stylespc">
                        <asp:Button ID="Button3" runat="server" BackColor="" Width="50px" Height="22"   Text="Done" Font-Names="Verdana" Font-Size="9px"
                            OnClientClick="return ValidateNewUnDoc();" OnClick="btnNun_Click" />
                        <asp:Button ID="Button4" runat="server" Text="Cancel" Font-Names="Verdana"  Width="50px" Height="22" Font-Size="9px"
                            OnClientClick="return CancelNewUn();"  />
                    
                    </td>
                </tr>
            </table>
                    
        </asp:Panel>        
        <asp:Panel ID="pnlRemarks" runat="server" BorderStyle="Solid" BorderWidth="1" CssClass = "docrmk" >
            <asp:HiddenField ID="hdnRemarks" runat="server" />
            <table bgcolor="white">
                <tr bgcolor="#999966">
                    <td bgcolor="#999966">
                        <asp:Label ID="lblPanel" runat="server" Text="&nbsp;Doctorwise Remarks"  Font-Names="Verdana" Font-Size="10px"
                            ForeColor="White"></asp:Label>
                      <%--  <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Images/Close.gif" ImageAlign="Right"
                            OnClientClick="HideRemarks();" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDR_Remarks" runat="server" Text="&nbsp;Doctor Name:"  Font-Names="Verdana" Font-Size="10px" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="lblDR_Name_Remarks" Font-Names="Verdana" Font-Size="10px" runat="server" BorderStyle="None" ForeColor="Green"
                            Font-Bold="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="390" MaxLength="250" BackColor="snow"
                       
                            BorderStyle="Solid" BorderWidth="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnRemarksok" runat="server" Text="OK"  OnClientClick="return valrem();" OnClick="btnRmkDone_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlProduct" runat="server"  CssClass = "pnladd">
            <asp:HiddenField ID="hidProdClose" runat="server" />
          <table width="100%" cellpadding="5" cellspacing="5" style="border-style:solid; border-width:thin; border-collapse:collapse " >       
                   <tr bgcolor="#999966">
                    <td bgcolor="#999966" style="width: 360px; border-color:Black; border-bottom-style:solid;border-bottom-width:thin">
                        <asp:Label ID="lblCapt" runat="server" Text="&nbsp;Product Sampled / Promoted"   Font-Names="Verdana" Font-Size="10px" Font-Bold = "true" 
                            ForeColor="White"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td >
                        <asp:GridView ID="grvProduct" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                            ForeColor="#333333" Width="100%" GridLines="None" OnRowDeleting="grvProduct_RowDeleting">
                            
                            <Columns>
                         
                                <asp:TemplateField HeaderText="Name"  HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="9px" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle CssClass="Spc" />
                                <ItemStyle CssClass="Spc" />
                                    <ItemTemplate>
                                    
                                        <asp:DropDownList ID="ddlProductAdd" Width="200px" runat="server" SkinID="ddlRequired" DataSource="<%# Populate_Products() %>"
                                            DataTextField="Product_Detail_Name" DataValueField="Product_Detail_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-Width="80px" HeaderStyle-Font-Size="9px" HeaderStyle-Font-Names="Verdana">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProdQty" runat="server" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2" Width="20px"></asp:TextBox>
                                    </ItemTemplate>                         
                                   
                                </asp:TemplateField>
                                 <asp:CommandField ShowDeleteButton="True"   DeleteImageUrl="~/Images/Delete.png"  ButtonType="Image" />
                                   <asp:TemplateField > 
                                <HeaderTemplate>  
                                         <asp:Button ID="ButtonAdd" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Add Product"  BackColor="LightBlue"  OnClick="ButtonAdd_Click"
                                            />
                                   </HeaderTemplate>  
                                      </asp:TemplateField> 
                                   <asp:TemplateField HeaderStyle-Width="50px">
                           <ItemTemplate></ItemTemplate>
                           </asp:TemplateField>
                           </Columns>
                          <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <RowStyle BackColor="White" />
                            <EditRowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#E0E0E0" Font-Bold="false" ForeColor="Black" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                      
                    </td>
                   
                </tr>
                <tr>
                 <tr bgcolor="#999966">
                    <td style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px">
                        <asp:Button ID="btnDone" runat="server" BackColor="" Width="50px" Height="22"   Text="Done" Font-Names="Verdana" Font-Size="9px"
                            OnClick="btnDone_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Names="Verdana"  Width="50px" Height="22" Font-Size="9px"
                            OnClientClick="return hideProduct();"  OnClick = "btnCancel_Click" />
                                  
                                  
                                
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <asp:Panel ID="pnlGift" runat="server" CssClass = "pnladd" >
            <asp:HiddenField ID="hidGiftClose" runat="server" />
            <table width="100%" cellpadding="5" cellspacing="5" style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px " >
              <tr bgcolor="#999966">
                    <td bgcolor="#999966" style="width: 360px; border-color:Black; border-bottom-style:solid;border-bottom-width:thin">
                        <asp:Label ID="Label21" runat="server" Text="&nbsp;Input"   Font-Names="Verdana" Font-Size="10px" Font-Bold = "true" 
                            ForeColor="White"></asp:Label>
                   
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdGift" runat="server" ShowFooter="True" AutoGenerateColumns="False" CssClass="clp" 
                            ForeColor="#333333" Width="355px" GridLines="None" OnRowDeleting="grdGift_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="9px" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle CssClass="Spc" />
                                <ItemStyle CssClass="Spc" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlGiftAdd" Width="200px" runat="server" SkinID="ddlRequired" DataSource="<%# Populate_Gift() %>"
                                            DataTextField="Gift_Name" DataValueField="Gift_Code" >
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-Width="80px" HeaderStyle-Font-Size="9px" HeaderStyle-Font-Names="Verdana">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGiftQty"   Text ="1" runat="server" Width="20px" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);" MaxLength="2"></asp:TextBox>
                                    </ItemTemplate>
                                   <%-- <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Button ID="ButtonAddGift" runat="server" Font-Names="Verdana" Font-Size="9px" Text="Add" BackColor="LightBlue" OnClick="ButtonAddGift_Click"/>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                               
                                <asp:CommandField ShowDeleteButton="True"   DeleteImageUrl="~/Images/Delete.png"  ButtonType="Image" />
                                 <asp:TemplateField >                                
                                <HeaderTemplate>     
                                                           
                                   <asp:Button ID="ButtonAddGift" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Add Input" BackColor="LightBlue" OnClick="ButtonAddGift_Click"/>
                                <%-- <asp:ImageButton ID="ButtonAddGift" runat="server" ImageUrl="~/Images/add.jpg" OnClick="ButtonAddGift_Click"/>--%>
                        </HeaderTemplate>
                                </asp:TemplateField> 
                                   <asp:TemplateField HeaderStyle-Width="50px">
                           <ItemTemplate></ItemTemplate>
                           </asp:TemplateField>
                            </Columns>
                         <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <RowStyle BackColor="White" />
                            <EditRowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#E0E0E0" Font-Bold="false" ForeColor="Black" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr bgcolor="#999966">
                    <td style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px">
                        <asp:Button ID="btnGiftDone" runat="server" Font-Names="Verdana"   Width="50px" Height="22" Font-Size="9px"
                            Text="Done" OnClick="btnGiftDone_Click" />
                            <%--    <asp:ImageButton ID="btnGiftDone" runat="server" ImageUrl="~/Images/ok1.jpg" Width="50px" Height="20px"
                            OnClick="btnGiftDone_Click" />--%>
                        <asp:Button ID="btnGiftCnl" runat="server" Font-Names="Verdana"  Width="50px" Height="22" Font-Size="9px"
                            Text="Cancel"  OnClick="btnGiftCnl_Click"/>
                           <%--  <asp:ImageButton ID="btnGiftCnl" runat="server" ImageUrl="~/Images/can.jpg" Height="20px"
                             OnClick="btnGiftCnl_Click" />--%>
                    </td>
                </tr>
            </table>
        </asp:Panel>
       
        <asp:Panel ID="pnlProduct_Unlst" runat="server"  CssClass = "pnladd">
            <asp:HiddenField ID="hidUnlstProdClose" runat="server" />
            <table width="100%" cellpadding="5" cellspacing="5" style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px " >
              <tr bgcolor="#999966">
                    <td bgcolor="#999966" style="width: 360px; border-color:Black; border-bottom-style:solid;border-bottom-width:thin">
                        <asp:Label ID="Label1" runat="server" Text="Product Sampled / Promoted" Font-Names="Verdana" Font-Size="10px" ></asp:Label>      
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grvProductUnlst" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                            ForeColor="#333333" Width="100%" GridLines="None" OnRowDeleting="grvProductUnlst_RowDeleting">
                            <Columns>
                             
                                <asp:TemplateField HeaderText="Product" HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="9px" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle CssClass="Spc" />
                                <ItemStyle CssClass="Spc" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlProductUnlstAdd" Width="200px" runat="server" SkinID="ddlRequired" DataSource="<%# Populate_Products() %>"
                                            DataTextField="Product_Detail_Name" DataValueField="Product_Detail_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-Width="80px" HeaderStyle-Font-Size="9px" HeaderStyle-Font-Names="Verdana">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProdUnlstQty" runat="server" Width="20" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"></asp:TextBox>
                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                 <asp:CommandField ShowDeleteButton="True"   DeleteImageUrl="~/Images/Delete.png"  ButtonType="Image" />
                                   <asp:TemplateField > 
                                <HeaderTemplate>     
                                                        
                               <asp:Button ID="ButtonAddUnlst" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Add New Row" BackColor="LightBlue" OnClick="ButtonAddUnlst_Click"/>
                               </HeaderTemplate> 
                              </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="50px">
                           <ItemTemplate></ItemTemplate>
                           </asp:TemplateField>
                            </Columns>
                             <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <RowStyle BackColor="White" />
                            <EditRowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#E0E0E0" Font-Bold="false" ForeColor="Black" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
               <tr bgcolor="#999966">
                    <td style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px">
                        <asp:Button ID="btnDoneUnlst" runat="server" Text="Done" OnClick="btnDoneUnlst_Click" Font-Names="Verdana"  Width="50px" Height="22" Font-Size="9px"/>
                        <asp:Button ID="btnCancelUnlst" runat="server" Text="Cancel" OnClick="btnCancelUnlst_Click" Font-Names="Verdana"  Width="50px" Height="22" Font-Size="9px"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
         
        <asp:Panel ID="pnlGiftUnlst" runat="server" CssClass = "pnladd">
            <asp:HiddenField ID="hdnUnLstgift" runat="server" />
           <table width="100%" cellpadding="5" cellspacing="5" style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px " >
              <tr bgcolor="#999966">
                    <td bgcolor="#999966" style="width: 360px; border-color:Black; border-bottom-style:solid;border-bottom-width:thin">
                        <asp:Label ID="Label2" runat="server" Text="Input" Font-Names="Verdana" Font-Size="10px" ></asp:Label>      
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdGiftUnlst" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                            ForeColor="#333333" Width="100%" GridLines="None" OnRowDeleting="grdGiftUnlst_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Input" HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="9px" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle CssClass="Spc" />
                                <ItemStyle CssClass="Spc" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlGiftUnlstAdd"  Width="200px" runat="server" SkinID="ddlRequired" DataSource="<%# Populate_Gift() %>"
                                            DataTextField="Gift_Name" DataValueField="Gift_Code" >
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-Width="80px" HeaderStyle-Font-Size="9px" HeaderStyle-Font-Names="Verdana">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGiftUnlstQty" runat="server" Width="20" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);" MaxLength="2"></asp:TextBox>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                               <asp:CommandField ShowDeleteButton="True"   DeleteImageUrl="~/Images/Delete.png"  ButtonType="Image" />
                               <asp:TemplateField>
                                <HeaderTemplate>     

                                        <asp:Button ID="ButtonAddGiftUnlst" runat="server" Font-Names="Verdana" Font-Size="10px" Text="Add New Row" BackColor="LightBlue" OnClick="ButtonAddGiftUnlst_Click"/>
                                </HeaderTemplate> 
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="50px">
                           <ItemTemplate></ItemTemplate>
                           </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <RowStyle BackColor="White" />
                            <EditRowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#E0E0E0" Font-Bold="false" ForeColor="Black" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                   <tr bgcolor="#999966">
                    <td style="border-style:solid; border-width:thin; border-collapse:collapse;padding:3px">
                        <asp:Button ID="btnGiftUnlstDone" runat="server" Text="Done" OnClick="btnGiftUnlstDone_Click" Font-Names="Verdana"  Width="50px" Height="22" Font-Size="9px"/>
                        <asp:Button ID="btnGiftUnlstCnl" runat="server" Text="Cancel" OnClick="btnGiftUnlstCnl_Click" Font-Names="Verdana"  Width="50px" Height="22" Font-Size="9px"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>  
        <asp:Panel ID="pnlTop" runat="server" BackColor="#E0E0E0">
            <asp:Label ID="lblCurDate" runat="server" Style= "display:none"></asp:Label>
            <table id="Table1" runat="server" width="100%" cellpadding="3" cellspacing="3">
                 <tr style="background-color:#E0E0E0;">
                    <td width="80%">                
                        &nbsp;
                       <asp:Label ID="lblText" runat="server" Font-Size="12px" Font-Names="TimesNewRoman"></asp:Label>
                        <asp:Label ID="lblHeader" runat="server" Font-Size="12px" Font-Names="Verdana"
                            BackColor="Yellow" ForeColor="Blue"></asp:Label>
                        <asp:Label ID="lblReject" runat="server" Font-Bold="true" ForeColor="Brown" Font-Names="TimesNewRoman"
                            Font-Size="14px"></asp:Label>
                    </td>
              
                 <td align="right">
                    <asp:Panel ID="Panel1" runat="server" Style="text-align: left;">
                    <asp:Label ID="lblReason" runat="server" Style="text-align: left" Font-Size="Small"
                        Font-Names="Verdana" Visible="false"></asp:Label>
                </asp:Panel>
                <ajax:BalloonPopupExtender ID="BalloonPopupExtender2" TargetControlID="lblNote" BalloonPopupControlID="Panel1"
                    runat="server" Position="TopLeft" DisplayOnMouseOver="true" BalloonSize="Small">
                </ajax:BalloonPopupExtender>
                        <asp:Label ID="lblNote" runat="server" Style="text-decoration: underline; " ForeColor="Red" 
                            Font-Size="Small" Font-Names="Verdana" Text="Note" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
            <table id="Table10" runat="server" width="100%" style="border-bottom:1px solid ">
                <tr style="background-color: #E0E0E0;">
                    <td style = "width : 270px">
                      &nbsp;&nbsp;
                       <asp:Label ID="lblWorkType" runat="server" Text="Work Type &nbsp;&nbsp;&nbsp;"
                            CssClass="pnl_label"></asp:Label>
                        <asp:DropDownList ID="ddlWorkType" runat="server" Width="190px" SkinID="ddldcr" onchange = "loadsetup();">
                        </asp:DropDownList>
                         <asp:hiddenfield id="hdnfwind"  runat="server" />
                    </td>
                  <td style = "width : 100px">
                        <asp:Label ID="lblSDP" runat="server" style= "display : none" Text="SDP Name &nbsp;:-" CssClass="pnl_label"></asp:Label>
                        </td>
                    <td style = "width : 250px">
                  
                        <asp:DropDownList ID="ddlSDP" runat="server"  Width="230px" style= "display : none" onchange = "ddlSDP_onchange();" SkinID = "ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAllTerr" runat ="server" style="display: none;" ></asp:DropDownList>
                        <asp:DropDownList ID="ddlsfterr" runat ="server" style="display: none;" ></asp:DropDownList>
                       <asp:hiddenfield id="hdnwtvalue"  runat="server" />
                    </td>
                    <td align="right">
                     <asp:hiddenfield id="hdnsftype"  runat="server" />
                      <asp:Button ID="btnnewfinal" Text="Final Submit" runat="server" BackColor="Yellow" Width="90px" Height="25px" OnClientClick="return confirm_Submit_final();"
                            OnClick="btnSubmit_Click"/>
                        <asp:Button ID="btnBack" Text="Back" runat="server" BackColor="Yellow" Width="70px" Height="25px" OnClientClick= "return btnback_click();"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <asp:Panel ID="pnlMultiView" runat="server" Style="position: absolute; visibility: hidden;">
        <asp:Panel ID="pnlTabLstDoc" runat="server" Style="position: absolute; visibility: hidden;">
        <%--<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" Visible="false"  >
            <asp:View ID="View1" runat="server"   >--%>
                <table id="tblDoc" runat="server" border="1" style="width: 100%;font-size: 10px; font-family:Verdana"  cellspacing="0" cellpadding="0">
                    <tr align="center" style="height: 20px; background-color: #B6B6B4; border-style: solid; border-color:Black">
                        <td style="width: 6px; border-style: solid; border-width: 1px">
                            Ses
                        </td>
                        <td style="border-style: solid; border-width: 1px" >
                            Time
                        </td>
                        <td id="tdDoc" runat="server" style="width: 100px; border-style: solid; border-width: 1px">
                            Listed Doctor Name
                        </td>
                        <td style="width: 10px; border-style: solid; border-width: 1px">
                            JFW
                        </td>
                        <td style="width: 355px; border-style: solid; border-width: 1px">
                            Product Sampled / Promoted
                        </td>
                        <td style="width: 120px; border-style: solid; border-width: 1px">
                            Input
                        </td>
                        <td style="width: 15px; border-style: solid; border-width: 1px">
                            Go
                        </td>
                        <td style="width: 100px;border-style: solid; border-width: 1px">
                        </td>
                        
                    </tr>
                    <tr style="border-style: none;">
                        <td style="border-style: solid; border-width: 1px">
                            <asp:DropDownList ID="ddlSes" Width="42px" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="M"></asp:ListItem>
                                <asp:ListItem Value="2" Text="E"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:100px; border-style: solid; border-width: 1px">
                        <asp:Panel ID="Panel10" runat="server" Width="100px">
                                <asp:DropDownList ID="ddlTime" runat="server" SkinID="ddlRequired">
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                </asp:DropDownList>
                                                             
                                <asp:DropDownList ID="ddlmin" runat="server" SkinID="ddlRequired">
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                    <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                    <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                    <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                    <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                    <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                    <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                    <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                    <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                    <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                    <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                    <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                    <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                    <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                    <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                    <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                    <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                    <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                    <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                    <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                    <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                    <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                    <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                    <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                    <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                    <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                </asp:DropDownList>
                               </asp:Panel>
                                <%--<ajax:TimeSelector ID="ddlTime" runat="server" DisplaySeconds="false"  SelectedTimeFormat="TwentyFour">
</ajax:TimeSelector>--%>
                         
                        </td>
                        <td style="width: 160px; border-style: solid; border-width: 1px" >
                            <asp:Panel ID="pnllst" runat="server" BorderStyle="Solid" BorderWidth="1">
                                <asp:TextBox ID="txtListDR" runat="server" Width="181px" Height="20px" BorderStyle="None"
                                    Font-Size="XX-Small" Font-Names="Verdana" onkeypress="HideList();" ></asp:TextBox>
                                <asp:ImageButton ID="imgListDR" runat="server" Style="left: 184px; top: 0px; position: absolute;" Height="20px" ImageUrl="~/Images/CmboButton.jpg"
                                    ImageAlign="Right" OnClientClick="return ShowListDR();" />
                            </asp:Panel>
                              <asp:Panel ID="pnlList" runat="server" Style=" position: absolute;
                                visibility: hidden;" CssClass="borderdr">
                                <asp:ListBox ID="lstListDR" onchange = "LdrDocLoad();" CssClass="borderdr"  runat="server" Width="250px" Font-Size="XX-Small" Font-Names="Verdana"
                                    Height="200">
                                </asp:ListBox>
                                <asp:TextBox ID="txtListDRCode" runat="server" style="display: none;" ></asp:TextBox>
                            </asp:Panel>
                           
                            <%--<asp:Panel runat="server" ID="myPanel" Style="left: 150px; height: 150px;
                                top: 170px;position:absolute; overflow:auto" BorderStyle="Solid" BorderWidth="1"
                                BackColor="White">
                            </asp:Panel>--%>
                            <asp:DropDownList ID="ddl_terr_doc" runat ="server" style="display: none;" ></asp:DropDownList>
                            <asp:DropDownList ID="ddldoclist" runat ="server" style="display: none;" ></asp:DropDownList>
                            <asp:DropDownList ID="mgrdoclist" runat ="server" style="display: none;" ></asp:DropDownList>
                            <asp:hiddenfield id="hdnValue"  runat="server" />
                            <div id="listPlacement" style="height:150px; width:150px; position:absolute; visibility: hidden; border-style:solid;  overflow :auto;"></div>
                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"  CompletionListCssClass="completionList"                          
                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                runat="server" TargetControlID="txtListDR" MinimumPrefixLength="1" EnableCaching="true"
                                CompletionSetCount="1" CompletionInterval="10" ServiceMethod="GetListedDR"  
                                CompletionListElementID="listPlacement" onclientitemselected="OnLdrSelected">
                            </ajax:AutoCompleteExtender>
                            
                            
                        </td>
                        <td style="border-style: solid; border-width: 1px; width:60px;">
                        <asp:Panel ID="Panel4" runat="server" Width="60px">
                            <asp:TextBox ID="txtFieldForce" Text="1" runat="server" Width="20px" SkinID="MandTxtBox" Enabled ="false"
                                BorderStyle="Groove" Font-Size="X-Small" Font-Names="Tahoma"></asp:TextBox>
                            <asp:Button ID="btnWW" runat="server" Width="20px" Height="24px" Text=">" BackColor="#8FBC8F" ForeColor="AntiqueWhite" ToolTip="Select 'Joint Work' with Whom"
                                Font-Size="11px" Font-Bold="true" Font-Names="Tahoma" OnClientClick="return ShowHideList();" />
                            <asp:Panel ID="pnlLstSF" runat="server" BackColor="White" Width="200px" Style="
                                position: absolute; visibility: hidden;">
                                <asp:CheckBoxList ID="chkFieldForce" runat="server" BorderStyle="Groove" BorderWidth="1px"
                                    Width="200px" OnClick="LoadFieldForce();" CssClass="mycheckbox">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <%--<asp:TextBox ID="txtSFCode" runat="server" Visible="false"></asp:TextBox>--%>
                            <asp:HiddenField ID="txtSFCode" runat="server" />
                            <asp:HiddenField ID="txtWWCode" runat="server" />
                            <asp:HiddenField ID="hdnsf_code" runat="server" Value='<%#Eval("sf_code") %>' />
                            </asp:Panel>
                        </td>
                        <td style="width: 500px; border-style: solid; border-width: 1px">
                            <asp:DropDownList ID="ddlProd1" runat="server" Width="120px" SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtProd1" runat="server" Width="20" BorderWidth="1" BorderColor="Black" onkeypress="CheckNumeric(event);" BorderStyle="Groove" Font-Size="X-Small" MaxLength="2" SkinID="MandTxtBox"></asp:TextBox>
                            <asp:TextBox ID="txtProdPOB1" Visible="false" runat="server" Width="20" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="6"></asp:TextBox>
                            <asp:DropDownList ID="ddlProd2" runat="server" Width="120px" SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtProd2" runat="server" Width="20"  BorderStyle="Solid" BorderWidth="1" BorderColor="Black" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"></asp:TextBox>
                            <asp:TextBox ID="txtProdPOB2" Visible="false" runat="server" Width="20" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);" MaxLength="6"></asp:TextBox>
                            <asp:DropDownList ID="ddlProd3" Width="120px" runat="server" SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtProd3" runat="server" Width="20"  BorderStyle="Solid" BorderWidth="1" BorderColor="Black" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"></asp:TextBox>
                            <asp:TextBox ID="txtProdPOB3" Visible="false" runat="server" Width="20" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);" MaxLength="6"></asp:TextBox>
                            <asp:Button ID="btnProdAdd" runat="server" Width="20px" Height="24px" Text="+" BackColor="#8FBC8F"
                                ForeColor="AntiqueWhite" Font-Size="11px" Font-Names="Tahoma" OnClientClick="return isValidateProd();" OnClick="btnProdAdd_Click"  ToolTip = "Click Here to Enter More Products" />
                        </td>
                        <td style="width: 150px; border-style: solid; border-width: 1px">
                <asp:Panel ID="Panel3" runat="server" Width="170px">
                            <asp:DropDownList ID="ddlGift" runat="server" Width="100px" SkinID="ddlRequired" onchange = "GiftAdd();">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtGift" runat="server" Width="20"  BorderStyle="Solid" BorderWidth="1" BorderColor="Black" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);" MaxLength="2"></asp:TextBox>
                            <asp:Button ID="btnGiftAdd" runat="server" Width="20px" Height="24px" Text="+" BackColor="#8FBC8F"
                                ForeColor="AntiqueWhite" Font-Size="11px" Font-Names="Tahoma" OnClientClick="return isValidateGift();" OnClick="btnGiftAdd_Click" ToolTip = "Click Here to Enter More Inputs"/>
                  </asp:Panel>
                        </td>
                        <td style="border-style: solid; border-width: 1px; border-right-color:Black">
                            <asp:Button ID="btnGo" runat="server" BackColor="Yellow" Text="Go" Width="35px" Height="25px" OnClientClick="return isValidate();" OnClick="btnGo_Click" />
                            <asp:HiddenField ID="hdndocedit" runat="server" />
                        </td>
                        

                          <td>
                         <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Uplworktype">
                         <ProgressTemplate>
                                    <img id="Img1" alt="" src="../../../Images/loading/loading19.gif" runat="server" /><span
                                        style="font-family: Verdana; color: Green; font-weight: bold">Please Wait....</span></ProgressTemplate>
                         </asp:UpdateProgress>
                         </td>
                    </tr>
                </table>
              
                <br />
              
            
             <asp:GridView ID="gvDCR" runat="server" Width="100%" HorizontalAlign="Center" AlternatingRowStyle-CssClass="alt"
                    GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false"
                    OnRowDeleting="gvDCR_RowDeleting" OnRowDataBound="gvDCR_RowDataBound" >
                    
                    <Columns>
                        <asp:TemplateField HeaderText="Ses">
                            <ItemTemplate>
                                <asp:Label ID="lblSession" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"session") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Time" ItemStyle-Width="90px">
                            <ItemTemplate>
                                <asp:Label ID="lblTime" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"time")%>'></asp:Label>                             
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Listed Doctor Name" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblDR" runat="server" Width="150px" Text='<%#DataBinder.Eval(Container. DataItem,"drcode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="JFW" HeaderStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkWith" runat="server" Width="100" Text='<%#DataBinder.Eval(Container. DataItem,"workwith") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Sampled / Promoted" HeaderStyle-Width="600px">
                            
                            <ItemTemplate>
                                <asp:Label ID="lblProduct1" runat="server"  style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"prod1") %>'></asp:Label>
                                <asp:Label ID="lblQty1" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"qty1") %>'></asp:Label>
                                <asp:Label ID="lblProd_POB1" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"prod_pob1") %>'></asp:Label>
                                
                                <asp:Label ID="lblProduct2" runat="server"  style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"prod2") %>'></asp:Label>
                                <asp:Label ID="lblQty2" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"qty2") %>'></asp:Label>
                                <asp:Label ID="lblProd_POB2" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"prod_pob2") %>'></asp:Label>
                                
                                <asp:Label ID="lblProduct3" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"prod3") %>'></asp:Label>
                                <asp:Label ID="lblQty3" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"qty3") %>'></asp:Label>
                                <asp:Label ID="lblProd_POB3" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"prod_pob3") %>'></asp:Label>
                                <asp:Label ID="lblLDProducts" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Input" HeaderStyle-Width="200">
                            <ItemTemplate>
                                <asp:Label ID="lblGift" runat="server" style="display: none;"   Text='<%#DataBinder.Eval(Container. DataItem,"gift") %>'></asp:Label>
                                <asp:Label ID="lblGQty" runat="server" style="display: none;"   Text='<%#DataBinder.Eval(Container. DataItem,"gqty") %>'></asp:Label>
                                 <asp:Label ID="lblLDGift" runat="server"></asp:Label>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Listed Doctor Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblDR_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"dr_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Session Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblSess_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"sess_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="minute" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblmin" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"minute") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="seconds" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblseconds" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"seconds") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Product1" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblProd1_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"prod1_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Product2" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblProd2_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"prod2_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Product3" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblProd3_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"prod3_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="AddProd" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblAddProd" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"AddProdCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="AddProdDtl" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblAddProdDtl" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"AddProd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="gift_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblGift_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"gift_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="AddInput" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblAddGift" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"AddGiftCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="AddInputDtl" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblAddGiftDtl" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"AddGift") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="remarks" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblremarks" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ww_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblww_code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"ww_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Edit" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server"  OnClientClick = "return GetSelectedRow_doc(this)"
                                    Text="Edit" CssClass  ="edit"> </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass  ="delete" OnClientClick="return confirm('Do you want to Delete')" CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderStyle-Width="10px" Visible="false">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1000px" FooterStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"
                            ItemStyle-BorderStyle="Solid"></asp:TemplateField>
                    </Columns>
                </asp:GridView>
             
           <%-- </asp:View>--%>
           </asp:Panel> 
           <asp:Panel ID="pnlTabChem" runat="server" Style="position: absolute; visibility: hidden;">           
                <table id="tblChemists" runat="server" border="1" style="width: 100%; font-family: Verdana;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr align="center" style="height: 20px; background-color: #B6B6B4; border-style: solid; border-color:Black">
                        <td style="width: 300px; border-style: solid; border-width: 1px">
                            Chemists Name
                        </td>
                        <td style="width: 60px; border-style: solid; border-width: 1px">
                            JFW
                        </td>
                        <td style="width: 50px; border-style: solid; border-width: 1px">
                            POB (Rs/-)
                        </td>
                        <td style="width: 15px; border-style: solid; border-width: 1px">
                            Go
                        </td>
                         <td style="visibility: hidden; width: 800px;">
                        </td>
                        <td style="width: 15px;">
                             <asp:Button ID="btnNewChe" runat="server" Text="New Chemist" Width="100px" Font-Size="11px"  Height="25px"  OnClientClick = "return newchemist();"
                             BackColor ="LightBlue" />
                             <asp:hiddenfield id="hdncheedit"  runat="server" />
                        </td>
                       
                    </tr>
                    <tr>
                      <%--  <td style="width: 150px; border-style: none;">
                            <asp:DropDownList ID="ddlChemists" runat="server" Width="300px" SkinID="ddlRequired">
                            </asp:DropDownList>
                        </td>--%>

                         <td style="width: 220px; border-style: solid; border-width: 1px" >
                            <asp:Panel ID="PnlCheList" runat="server" BorderStyle="Solid" BorderWidth="1" Width = "290px">
                                <asp:TextBox ID="txtPnlChe" runat="server" Width="273px"  Height="20px" BorderStyle="None" 
                                    Font-Size="XX-Small" Font-Names="Verdana" onkeypress="CheHide();" ></asp:TextBox>
                                <asp:ImageButton ID="ImageButton5" runat="server" Style="left: 274px; top: 0px; position: absolute;" Height="20px" ImageUrl="~/Images/CmboButton.jpg"
                                    ImageAlign="Right" OnClientClick="return ShowChe();" />
                            </asp:Panel>
                            <asp:Panel ID="PChe" runat="server" Style="position: absolute;
                                    visibility: hidden;" CssClass="borderdr">
                
                                    <asp:ListBox ID="lstChe" runat="server" onchange = "cheLoad();" CssClass="borderdr"  Width="290px" Font-Size="XX-Small" Font-Names="Verdana"
                                        Height="200">
                                    </asp:ListBox>
                                     <asp:DropDownList ID="ddl_terr_che" runat ="server" style="display: none;" ></asp:DropDownList>
                                     <asp:DropDownList ID="ddlchelist" runat ="server" style="display: none;" ></asp:DropDownList>
                                     <asp:DropDownList ID="mgrchelist" runat ="server" style="display: none;" ></asp:DropDownList>
                                     <asp:HiddenField ID="hdnchemcount" runat="server" />
                                    <asp:TextBox ID="txtPChe" runat="server" style="display: none;" ></asp:TextBox>
                            </asp:Panel>

                             <asp:hiddenfield id="chehdnValue"  runat="server" />
                             <div id="ChemDiv" style="height:150px; width:150px; position:absolute; visibility: hidden; overflow :auto;"></div>
                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" CompletionListCssClass="completionList"                       
                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                runat="server" TargetControlID="txtPnlChe" MinimumPrefixLength="1" EnableCaching="true"
                                CompletionSetCount="1" CompletionInterval="10" ServiceMethod="GetChemists"  
                                CompletionListElementID="ChemDiv" onclientitemselected="OnCheSelected" >
                            </ajax:AutoCompleteExtender>
                           
                        </td>
                        <td style="border-style: solid; border-width: 1px; width:60px;" >
                         
                            <asp:TextBox ID="txtChemWW" Text="1" runat="server" Width="20px" SkinID="MandTxtBox" BorderStyle="Groove" Enabled ="false"
                                Font-Size="X-Small" Font-Names="Tahoma"></asp:TextBox>
                            <asp:Button ID="btnCheWW" runat="server" Width="20px" Height="24px" Text=">" BackColor="#8FBC8F" ToolTip="Select 'Joint Work' with Whom" 
                                ForeColor="AntiqueWhite" Font-Size="11px" Font-Bold="true" Font-Names="Tahoma" OnClientClick="return ShowHide_WorkWith('pnlChemWW');" />
                            <asp:Panel ID="pnlChemWW" runat="server" BackColor="White" Width="200px" Style="position: absolute; visibility: hidden;">
                                <asp:CheckBoxList ID="chkChemWW" runat="server" BorderStyle="Groove" BorderWidth="1px" 
                                    Width="200px" OnClick="LoadChemists();" CssClass="mycheckbox">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:HiddenField ID="txtChemWWSF" runat="server" />
                            <asp:HiddenField ID="hdnChemWW_Code" runat="server" />
                            <asp:HiddenField ID="hdnChemWW_Name" runat="server" />
                            
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:TextBox ID="txtPOBNo" runat="server" Width="80px" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="6"
                             onKeyDown="return DisableCtrlKey(event)" onMouseDown="DisableRightClick(event)"></asp:TextBox>
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                       
                            <asp:Button ID="btnChemGo" runat="server" Text="Go" Width="35px" Height="25px" BackColor="Yellow" OnClientClick="return isValidate_Chemists();"
                                  OnClick="btnChemGo_Click"/>

                          </td>
                         <td colspan="2">
                         <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Uplworktype">
                         <ProgressTemplate>
                                    <img id="Img2" alt="" src="../../../Images/loading/loading19.gif" runat="server" /><span
                                        style="font-family: Verdana; color: Green; font-weight: bold">Please Wait....</span></ProgressTemplate>
                         </asp:UpdateProgress>
                         </td>
                     </tr>
                </table>
                 
                <br />
                
             
                <asp:GridView ID="grdChem" runat="server" Width="100%" HorizontalAlign="Center" AlternatingRowStyle-CssClass="alt"
                    GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false" 
                    OnRowDeleting="grdChem_RowDeleting" >
                    
                    <Columns>
                        <asp:TemplateField HeaderText="Chemists Name" ItemStyle-Width ="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblChemists" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"chemists") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="JFW" ItemStyle-Width = "200px">
                            <ItemTemplate>
                                <asp:Label ID="lblWW" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"chemww") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POB (Rs/-)" ItemStyle-Width = "80px"> 
                            <ItemTemplate>
                                <asp:Label ID="lblPOBNo" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"POBNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chemists Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblChem_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"chem_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Territory Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblTerr_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"terr_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="new" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblnew" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"new") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="sf_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblsf_code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"sf_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ww_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="ww_code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"ww_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width = "10px" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" OnClientClick = "return GetSelectedRow_che(this)"
                                   Text="Edit"   CssClass  ="edit"> </asp:LinkButton>       
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width ="10px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server"  CssClass  ="delete" OnClientClick="return confirm('Do you want to Delete')" CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="1000px" FooterStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"
                            ItemStyle-BorderStyle="Solid"></asp:TemplateField>
                    </Columns>
                </asp:GridView>
               
              <%--</asp:View>--%>
              </asp:Panel> 
             
              <asp:Panel ID="PnlTabStk" runat="server" Style="position: absolute; visibility: hidden;" >

            
                <table id="StkTbl" runat="server" border="1" style="width: 100%; font-family: Verdana;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr align="center" style="height: 20px; background-color:#B6B6B4; border-style: solid;">
                         
                        <td style="width: 300px; border-style: solid; border-width: 1px">
                            Stockist Name
                        </td>
                        <td style="width: 100px; border-style: solid; border-width: 1px">
                            JFW
                        </td>
                        <td style="width: 50px; border-style: solid; border-width: 1px">
                            POB (Rs/-) 
                        </td>
                        <td style="width: 100px; border-style: solid; border-width: 1px">
                            Visit Type
                        </td>
                        <td style="width: 15px; border-style: solid; border-width: 1px">
                            Go
                        </td>
                        <td style="visibility: hidden; width: 1000px;" >
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px; border-style: solid; border-width: 1px">
                            <asp:DropDownList ID="StkDDL" runat="server" Width="300px" SkinID="ddlRequired">
                            </asp:DropDownList>
                        </td>
                        <td style="border-style: solid; border-width: 1px;  width:60px;">
                     
                            <asp:TextBox ID="txtStk" Text="1" runat="server" Width="20px" SkinID="MandTxtBox" BorderStyle="Groove" Enabled ="false"
                                Font-Size="X-Small" Font-Names="Tahoma"></asp:TextBox>
                            <asp:Button ID="btnStk" runat="server" Width="20px" Height="24px" Text=">" BackColor="#8FBC8F" ToolTip="Select 'Joint Work' with Whom" 
                                ForeColor="AntiqueWhite" Font-Size="11px" Font-Bold="true" Font-Names="Tahoma" OnClientClick="return ShowHide_WorkWith('pnlStk');" />
                            <asp:Panel ID="pnlStk" runat="server" BackColor="White" Width="200px" Style="position: absolute; visibility: hidden;">
                                <asp:CheckBoxList ID="StkChkBox" runat="server" BorderStyle="Groove" BorderWidth="1px"
                                    Width="200px" OnClick="LoadStockiest();" CssClass="mycheckbox">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:HiddenField ID="HdnStk" runat="server" />
                             <asp:HiddenField ID="hdnStkWWCode" runat="server" />
                           
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:TextBox ID="StkPOB" runat="server" Width="80px"  onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="6"
                             onKeyDown="return DisableCtrlKey(event)" onMouseDown="DisableRightClick(event)"></asp:TextBox>
                        </td>
                        <td style="width: 100px; border-style: solid; border-width: 1px">
                            <asp:DropDownList ID="StkVisitType" runat="server" Width="100" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="POB Giving"></asp:ListItem>
                                <asp:ListItem Value="2" Text="POB Followup"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Statement Receipt"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Order Collection"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Closing Verification"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Payment Followup"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Others"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Button ID="btnStkGo" runat="server" Text="Go" Width="35px" Height="25px" BackColor="Yellow" OnClientClick="return isValidate_Stockiest();"
                                OnClick="btnStkGo_Click"  />
                                <asp:hiddenfield id="hdnstkedit"  runat="server" />
                        </td>
                          <td>
                         <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="Uplworktype">
                         <ProgressTemplate>
                                    <img id="Img3" alt="" src="../../../Images/loading/loading19.gif" runat="server" /><span
                                        style="font-family: Verdana; color: Green; font-weight: bold">Please Wait....</span></ProgressTemplate>
                         </asp:UpdateProgress>
                         </td>
                    </tr>
                </table>
                <br />
               
                <asp:GridView ID="GridStk" runat="server" Width="100%" HorizontalAlign="Center" AlternatingRowStyle-CssClass="alt"
                    GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false" 
                    OnRowDeleting="GridStk_RowDeleting" >
                    <Columns>
                        <asp:TemplateField HeaderText="Stockists" HeaderStyle-Width="150px">
                            
                            <ItemTemplate>
                                <asp:Label ID="lblStockist" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"stockiest") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="JFW" HeaderStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblStkWW" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"stockww") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POB (Rs/-)" HeaderStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPOB" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"pob") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Visit Type" HeaderStyle-Width="100px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblStkVT" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"visit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="stockist Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblstk_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"stockiest_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Visit Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblvisit_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"visit_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ww_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblww_code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"ww_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Edit" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" OnClientClick = "return GetSelectedRow_stk(this)"
                                   Text="Edit" CssClass = "edit" > </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server"   CssClass = "delete"  OnClientClick="return confirm('Do you want to Delete')" CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1000px" FooterStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"
                            ItemStyle-BorderStyle="Solid"></asp:TemplateField>
                    </Columns>
                </asp:GridView>
              
            </asp:Panel> 

            <!-- Unlisted Doctor -->
            <asp:Panel ID="PnlTabUnLst" runat="server"  Style=" position: absolute; visibility: hidden;">
            
                <table id="tblUnlstDr" runat="server" border="1" style="width: 100%; font-family: Verdana;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr align="center" style="height: 20px; background-color: #B6B6B4; border-style: solid;">
                        <td style="width: 35px; border-style: solid; border-width: 1px">
                            Ses
                        </td>
                        <td style=" border-style: solid; border-width: 1px">
                            Time
                        </td>
                        <td style="width: 130px; border-style: solid; border-width: 1px">
                            Un-Listed Doctor Name
                        </td>
                        <td style="width: 80px; border-style: solid; border-width: 1px">
                            JFW
                        </td>
                        <td style="width: 455px; border-style: solid; border-width: 1px">
                            Product Sampled / Promoted
                        </td>
                        <td style="width: 95px; border-style: solid; border-width: 1px">
                            Input
                        </td>
                        <td style="width: 20px; border-style: solid; border-width: 1px" >
                            Go
                        </td>
                        <td style="width: 200px; border-style: solid; border-width: 1px">
                        </td>
                        
                        <td style="width: 50px;">
                             <asp:Button ID="btnUnNew" runat="server" Text="New Unlisted Dr" Width="110px" Font-Size="11px"  Height="25px"  OnClientClick = "return newunlisted();" BackColor ="LightBlue" />
                        </td>
                    </tr>
                    <tr>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:DropDownList ID="ddlUnLstDR_Session" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="M"></asp:ListItem>
                                <asp:ListItem Value="2" Text="E"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                       <td style="width:100px; border-style: solid; border-width: 1px">
                       <asp:Panel ID="Panel2" runat="server" Width="100px">
                                <asp:DropDownList ID="ddlMinute" runat="server" SkinID="ddlRequired">
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                </asp:DropDownList>
                             
                                <asp:DropDownList ID="ddlSec" runat="server" SkinID="ddlRequired">
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                    <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                    <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                    <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                    <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                    <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                    <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                    <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                    <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                    <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                    <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                    <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                    <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                    <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                    <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                    <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                    <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                    <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                    <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                    <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                    <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                    <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                    <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                    <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                    <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                    <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                </asp:DropDownList>
                                </asp:Panel>
                       </td>
                         
                       <td style="width: 160px; border-style: solid; border-width: 1px" >
                            <asp:Panel ID="UnPnl" runat="server" BorderStyle="Solid" BorderWidth="1">
                                <asp:TextBox ID="Untxt_Dr" runat="server" Width="181px" Height="20px" BorderStyle="None"
                                    Font-Size="XX-Small" Font-Names="Verdana" onkeypress="HideUnDrList();" ></asp:TextBox>
                                <asp:ImageButton ID="ImageButton6" runat="server" Style="left: 184px; top: 0px; position: absolute;" Height="20px" ImageUrl="~/Images/CmboButton.jpg"
                                    ImageAlign="Right" OnClientClick="return ShowUnDR();" />
                            </asp:Panel>
                              <asp:Panel ID="UnListPnl" runat="server" Style=" position: absolute;
                                visibility: hidden;" CssClass="borderdr">
                                <asp:ListBox ID="UnLstDr" runat="server" onchange = "UnLdrDocLoad();" CssClass="borderdr"  Width="250px" Font-Size="XX-Small" Font-Names="Verdana"
                                    Height="200" >
                                </asp:ListBox>

                                <asp:TextBox ID="UnLstTxtDR" runat="server" style="display: none;" ></asp:TextBox>
                            </asp:Panel>
                            <asp:DropDownList ID="ddl_terr_Undoc" runat ="server" style="display: none;" ></asp:DropDownList>
                            <asp:DropDownList ID="ddlundoclist" runat ="server" style="display: none;" ></asp:DropDownList>
                            <asp:DropDownList ID="mgrundoclist" runat ="server" style="display: none;" ></asp:DropDownList>
                             <asp:hiddenfield id="UnDrhdnValue"  runat="server" />
                             <div id="UDrDiv" style="height:150px; width:150px; position:absolute;  visibility: hidden; overflow :auto;"></div>
                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender3" CompletionListCssClass="completionList"                       
                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                runat="server" TargetControlID="Untxt_Dr" MinimumPrefixLength="1" EnableCaching="true"
                                CompletionSetCount="1" CompletionInterval="10" ServiceMethod="GetUNListedDR"  
                                CompletionListElementID="UDrDiv" onclientitemselected="OnUnLdrSelected" >
                            </ajax:AutoCompleteExtender>
                        </td>
                     
                        <td style="width: 60px; border-style: solid; border-width: 1px" >
                        
                            <asp:TextBox ID="txtUnLstDR_SF" Text="1" runat="server" Width="20" SkinID="MandTxtBox" Enabled ="false"
                                BorderStyle="Groove" Font-Size="X-Small" Font-Names="Tahoma"></asp:TextBox>
                            <asp:Button ID="btnUnLstDR_SF" runat="server" Width="20px" Height="24px" Text=">" BackColor="#8FBC8F" ToolTip="Select 'Joint Work' with Whom" 
                                 ForeColor="AntiqueWhite" Font-Size="11px" Font-Names="Tahoma"
                                OnClientClick="return ShowHide_WorkWith('pnlUnLstDR_SF');" />

                            <asp:Panel ID="pnlUnLstDR_SF" runat="server" BackColor="White" Width="200px" Style= "position: absolute; visibility: hidden;">
                                <asp:CheckBoxList ID="chkUnLstDR" runat="server" BorderStyle="Groove" BorderWidth="1px"
                                    Width="200px" OnClick="LoadUnListedDR();" CssClass="mycheckbox">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <%--<asp:TextBox ID="txtSFCode" runat="server" Visible="false"></asp:TextBox>--%>
                            <asp:HiddenField ID="txtUnLstDR_WW" runat="server" />
                             <asp:HiddenField ID="hdnunww_code" runat="server" />
                        </td>
                        <td style="width: 670px; border-style: solid; border-width: 1px">
                            <asp:DropDownList ID="ddlUnLstDR_Prod1" runat="server"  SkinID="ddlRequired" Width="90">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtUnLstDR_Prod_Qty1" runat="server" Width="20" onkeypress="CheckNumeric(event);"  SkinID="MandTxtBox" MaxLength="2"></asp:TextBox>
                            <asp:TextBox ID="txtUnLstDR_Prod_POB1" Visible="false" runat="server" Width="20" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="6"></asp:TextBox>
                            <asp:DropDownList ID="ddlUnLstDR_Prod2" runat="server" SkinID="ddlRequired" Width="90">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtUnLstDR_Prod_Qty2" runat="server" Width="20" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"></asp:TextBox>
                            <asp:TextBox ID="txtUnLstDR_Prod_POB2" Visible="false" runat="server" Width="20" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="6"></asp:TextBox>
                            <asp:DropDownList ID="ddlUnLstDR_Prod3" runat="server" SkinID="ddlRequired" Width="90">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtUnLstDR_Prod_Qty3" runat="server" Width="20" onkeypress="CheckNumeric(event);"  SkinID="MandTxtBox" MaxLength="2"></asp:TextBox>
                            <asp:TextBox ID="txtUnLstDR_Prod_POB3" Visible="false" runat="server" Width="20" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="6"></asp:TextBox>
                
                            <asp:Button ID="BtnUnProdAdd" runat="server" Width="20px" Height="24px" Text="+"
                                BackColor="#8FBC8F" ForeColor="AntiqueWhite" Font-Bold="true" Font-Size="11px" Font-Names="Tahoma" OnClientClick="return isValidateUnProd();"
                                OnClick="btnUnProdAdd_Click" ToolTip = "Click Here to Enter More Products" />
                        </td>
                        <td style="width: 220px; border-style: solid; border-width: 1px;">
                            <asp:DropDownList ID="ddlUnLstDR_Gift" runat="server" SkinID="ddlRequired" Width="100"  onchange = "UnGiftAdd();">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtUnLstDR_GQty" runat="server" Width="20" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"></asp:TextBox>
                            <asp:Button ID="btnUnGiftAdd" runat="server" Width="20px" Height="24px" Text="+"
                                BackColor="#8FBC8F" ForeColor="AntiqueWhite" Font-Size="11px" Font-Names="Tahoma" OnClientClick="return isValidateunGift();"
                                OnClick="btnUnGiftAdd_Click" ToolTip = "Click Here to Enter More Inputs" />
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                            <asp:Button ID="btnUnLstDR_Go" runat="server" Text="Go" Width="35px" Height="25px" BackColor="Yellow" OnClientClick ="return isValidate_UnListedDr();" OnClick="btnUnLstDR_Go_Click" />
                            <asp:hiddenfield id="hdnundocedit"  runat="server" />
                        </td>
                          <td colspan="2">
                         <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="Uplworktype">
                         <ProgressTemplate>
                                    <img id="Img4" alt="" src="../../../Images/loading/loading19.gif" runat="server" /><span
                                        style="font-family: Verdana; color: Green; font-weight: bold">Please Wait....</span></ProgressTemplate>
                         </asp:UpdateProgress>
                         </td>
                    </tr>
                </table>
                
                <br />
             
                <asp:GridView ID="grdUnLstDR" runat="server" Width="100%" HorizontalAlign="Center"
                    AlternatingRowStyle-CssClass="alt" GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false"
                    OnRowDeleting="grdUnLstDR_RowDeleting"
                    OnRowDataBound="grdUnLstDR_RowDataBound">
                   
                    <Columns>
                        <asp:TemplateField HeaderText="Ses">
                            <ItemTemplate>
                                <asp:Label ID="lblUnLstDR_Session" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"session") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Time">
                            <ItemTemplate>
                                <asp:Label ID="lblUnLstDR_Time" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"time") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="UnListed Doctor Name"  HeaderStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblUnLstDR_DR" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"drcode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="JFW" HeaderStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblUnLstDR_WorkWith" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"workwith") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Product(s) Sampled / Promoted" HeaderStyle-Width="550">   
                            <ItemTemplate>
                                <asp:Label ID="lblUnLstDR_Product1" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"prod1") %>'></asp:Label>
                                <asp:Label ID="lblUnLstDR_Qty1" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"qty1") %>'></asp:Label>
                                <asp:Label ID="lblUnLstDR_POB1" runat="server" style="display: none;" Text='<%#DataBinder.Eval(Container. DataItem,"prod_pob1") %>'></asp:Label>
                                
                                <asp:Label ID="lblUnLstDR_Product2" runat="server" style="display: none;" Text='<%#DataBinder.Eval(Container. DataItem,"prod2") %>'></asp:Label>
                                <asp:Label ID="lblUnLstDR_Qty2" runat="server" style="display: none;" Text='<%#DataBinder.Eval(Container. DataItem,"qty2") %>'></asp:Label>
                                <asp:Label ID="lblUnLstDR_POB2" runat="server" style="display: none;" Text='<%#DataBinder.Eval(Container. DataItem,"prod_pob2") %>'></asp:Label>
                                
                                <asp:Label ID="lblUnLstDR_Product3" runat="server" style="display: none;"  Text='<%#DataBinder.Eval(Container. DataItem,"prod3") %>'></asp:Label>
                                <asp:Label ID="lblUnLstDR_Qty3" runat="server" style="display: none;" Text='<%#DataBinder.Eval(Container. DataItem,"qty3") %>'></asp:Label>
                                <asp:Label ID="lblUnLstDR_POB3" runat="server" style="display: none;" Text='<%#DataBinder.Eval(Container. DataItem,"prod_pob3") %>'></asp:Label>
                                <asp:Label ID="lblUnProd" runat="server"></asp:Label>
                                
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Input" HeaderStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="lblUnLstDR_Gift" runat="server" style="display: none;" Text='<%#DataBinder.Eval(Container. DataItem,"gift") %>'></asp:Label>
                                <asp:Label ID="lblUnLstDR_GQty" runat="server"  style="display: none;" Text='<%#DataBinder.Eval(Container. DataItem,"gqty") %>'></asp:Label>
                                <asp:Label ID="lblUngift" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Listed Doctor Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblUnLstDR_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"dr_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Session Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblSess_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"sess_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Time Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblTime_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"time_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Product1" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblProd1_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"prod1_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Product2" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblProd2_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"prod2_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Product3" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblProd3_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"prod3_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AddProd" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblAddProd" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"AddProdCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AddProdDtl" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblAddProdDtl" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"AddProd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="gift_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblGift_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"gift_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="AddInput" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblAddGift" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"AddGiftCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="AddInputDtl" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblAddGiftDtl" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"AddGift") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="min" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblmin" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"min") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="sec" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblsec" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"sec") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Territory Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblterr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"terr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="spec" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblspe" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"spec") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="cat" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblcat" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"cat") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="class" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblclass" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"class") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="qual" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblqual" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"qual") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="add" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lbladd" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"add") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="new" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblnew" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"new") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="sfcode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblsfcode" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"sfcode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ww_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblww_code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"ww_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkUnLstDrEdit_Click" runat="server" OnClientClick = "return GetSelectedRow_undoc(this)"
                                    Text="Edit" CssClass  ="edit"> </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server"  OnClientClick="return confirm('Do you want to Delete')"   CssClass  ="delete"  CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderStyle-Width="1000px" FooterStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"
                            ItemStyle-BorderStyle="Solid"></asp:TemplateField>
                    </Columns>
                </asp:GridView>
             
            </asp:Panel> 
            <!-- Hospital Details  -->
            <asp:Panel ID="PnlTabHos" runat="server"  Style=" position: absolute; visibility: hidden;">
                <table id="tblHospital" runat="server" border="1" style="width: 100%; font-family: Tahoma;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr align="center" style="height: 20px; background-color: #B6B6B4; border-style: solid;">
                        <td style="width: 300px; border-style: solid; border-width: 1px">
                            Hospital Name
                        </td>
                        <td style="width: 100px; border-style: solid; border-width: 1px">
                            JFW
                        </td>
                        <td style="width: 50px; border-style: solid; border-width: 1px">
                            POB (Rs/-)
                        </td>
                        <td style="width: 15px; border-style: solid; border-width: 1px">
                            Go
                        </td>
                        <td style="visibility: hidden; width: 1000px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px; border-style: none;">
                            <asp:DropDownList ID="HosDDL" runat="server" Width="300px" SkinID="ddlRequired">
                            </asp:DropDownList>
                        </td>
                        <td style="border-style: solid; border-width: 1px">
                       
                            <asp:TextBox ID="txtHos" Text="1" runat="server" Width="20" SkinID="MandTxtBox" Enabled ="false"
                                BorderStyle="Groove" Font-Size="X-Small" Font-Names="Tahoma"></asp:TextBox>
                            <asp:Button ID="btnHospWW" runat="server" Height="17px" Width="20px" Text=">" BackColor="ForestGreen" ToolTip="Select 'Joint Work' with Whom" 
                                ForeColor="AntiqueWhite" Font-Size="X-Small" Font-Names="Tahoma" OnClientClick="return ShowHide_WorkWith('Panel6');" />
                            
                            <asp:Panel ID="Panel6" runat="server" BackColor="White" Width="200px" Style="position: absolute; visibility: hidden;">           
                                <asp:CheckBoxList ID="ChkHos" runat="server" BorderStyle="Groove" BorderWidth="1px"
                                    Width="200px" OnClick="LoadHospital();" CssClass="mycheckbox">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:HiddenField ID="HdnHos" runat="server" />
                           <asp:HiddenField ID="hdnHosww_code" runat="server" />
                        </td>
                        <td style="border-style: solid; border-width: 1px;">
                            <asp:TextBox ID="txtHospPOB" runat="server" Width="80px" onkeypress="CheckNumeric(event);" SkinID="MandTxtBox" MaxLength="2"
                             onKeyDown="return DisableCtrlKey(event)" onMouseDown="DisableRightClick(event)"></asp:TextBox>
                        </td>
                        <td style="border-style: solid; border-width: 1px;">
                            <asp:Button ID="btnHosGo" runat="server" Text="Go" BackColor="Yellow" Width="25px" OnClientClick="return isValidate_Hospital();"
                                OnClick="btnHosGo_Click" />
                                 <asp:hiddenfield id="hdnhosedit"  runat="server" />
                        </td>
                            <td>
                         <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="Uplworktype">
                         <ProgressTemplate>
                                    <img id="Img5" alt="" src="../../../Images/loading/loading19.gif" runat="server" /><span
                                        style="font-family: Verdana; color: Green; font-weight: bold">Please Wait....</span></ProgressTemplate>
                         </asp:UpdateProgress>
                         </td>
                    </tr>
                </table>
                <br />
              
                <asp:GridView ID="GridHospital" runat="server" Width="100%" HorizontalAlign="Center"
                    AlternatingRowStyle-CssClass="alt" GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false"
                    OnRowDeleting="GridHospital_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Hospitals" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblHospital" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"hospital") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="JFW" HeaderStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblHosWW" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"hosww") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="POB (Rs/-)" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="lblPOB" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"pob") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Hospital Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblHos_Code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"hospital_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ww_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:Label ID="lblww_code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"ww_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" OnClientClick = "return GetSelectedRow_hos(this)"
                                   Text="Edit" CssClass  ="edit" > </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server"  CssClass  ="delete"  OnClientClick="return confirm('Do you want to Delete')" CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1000px" FooterStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"
                            ItemStyle-BorderStyle="Solid"></asp:TemplateField>
                    </Columns>
                </asp:GridView>
             
            </asp:Panel> 
            <!-- Remarks  -->
            <asp:Panel ID="PnlTabRem" runat="server"  Style=" position: absolute; visibility: hidden;">          
            
              <div id="div5" style="height: 450px; top: 138px;"> 
                <table id="tblRemarks" runat="server" border="1" style="width: 100%; font-family: Tahoma;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblDes" runat="server" Text="Description of Days work"
                                Font-Size="Small" Font-Bold="true" Font-Names="Verdana"></asp:Label>
                                  <span style="font-weight:bold; margin-left:300px;text-align:right; background-color: Yellow;"> Left:&nbsp;&nbsp;<asp:Label ID="lblCount" runat="server" ></asp:Label></span>
                        </td>        
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="txtRemarkDesc" runat="server" Width="1340" Height="470" BorderStyle="Solid" 
                                TextMode="MultiLine"  onkeypress="AlphaNumeric_enter(event);" 
                                  onpaste="return LimtCharacters(this,'lblCount')" onKeyDown="return LimtCharacters(this,'lblCount')" ></asp:TextBox>
                           <asp:HiddenField ID ="HdnMRmk" runat ="server" />
                        </td>
                    </tr>
                </table>
                  </div> 
            </asp:Panel> 
          
            <!-- Preview  -->
           
            <asp:Panel ID="PnlTabPrev" runat="server"  Style=" position: absolute; visibility: hidden;">
            <table id="tblPreview_LstDoc" runat="server" style="width: 100%; font-family: Tahoma;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label3" runat="server" Text="Daily Calls Report - Preview" Font-Size="Small"
                                Font-Names="Verdana" Font-Underline="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
          
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblld" runat="server" Text="Listed Doctor Details" Font-Size="Small"
                                Font-Names="Verdana" Font-Underline="true"></asp:Label>
                            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
                <table id="tblPreview_Chem" runat="server" style="width: 100%; font-family: Tahoma;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblch" runat="server" Text="Chemists Details" Font-Size="Small" Font-Names="Verdana"
                                Font-Underline="true"></asp:Label>                                
                            <asp:Table ID="tblChem" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
                <table id="tblPreview_Stock" runat="server" style="width: 100%; font-family: Tahoma;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblst" runat="server" Text="Stockiests Details" Font-Size="Small"
                                Font-Names="Verdana" Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblstk" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table id="tblPreview_UnLstDoc" runat="server" style="width: 100%; font-family: Tahoma;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblunls" runat="server" Text="UnListed Doctor Details" Font-Size="Small"
                                Font-Names="Verdana" Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblunlst" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table id="tblPreview_Hos" runat="server" style="width: 100%; font-family: Tahoma;
                    font-size: x-small;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblhos" runat="server" Text="Hospital" Font-Size="Small" Font-Names="Verdana"
                                Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblhos" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table id="tblPreview_Remarks" runat="server" style="width: 100%; font-family: Tahoma;
                    font-size: x-small;" cellspacing="0" cellpadding="0" visible ="false">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" Font-Size="Small" Font-Names="Verdana"
                                Font-Underline="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="RevPreview" runat="server" Width="1200"  Height="200"  Enabled ="false" BackColor ="White"  BorderStyle="Groove" TextMode ="MultiLine"  ></asp:TextBox>
                        </td>
                    </tr>
                </table>
              
            </asp:Panel> 
              
           
   </asp:Panel> 
    <div class="customFooter">
   <asp:Panel ID="pnlTab" runat="server"  Width = "538px" Style="display:none;">            
           <table>
                <tr>
                <asp:HiddenField ID="hdnbutname" runat="server" />
                    <td>
                        <asp:Button ID="btnListedDR" runat="server" Text="ListedDR" Font-Names="Verdana"
                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"  
                            OnClientClick="return tabbut_Click('pnlTabLstDoc','pnlTabChem','PnlTabStk','PnlTabUnLst','PnlTabHos','PnlTabRem','PnlTabPrev','btnListedDR','btnChemists','btnStockist','btnNonListedDR','btnHospital','btnRemarks','btnPreview','0');"
                            Height="24px" Width="74px" />
                    </td>
                    <td >
                        <asp:Button ID="btnChemists" runat="server" Text="Chemists" Font-Names="Verdana"
                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"  
                            OnClientClick="return tabbut_Click('pnlTabChem','pnlTabLstDoc','PnlTabStk','PnlTabUnLst','PnlTabHos','PnlTabRem','PnlTabPrev','btnChemists','btnListedDR','btnStockist','btnNonListedDR','btnHospital','btnRemarks','btnPreview','1');"
                            Height="24px" Width="74px" />
                    </td>
                    <td >
                        <asp:Button ID="btnStockist" runat="server" Text="Stockist" Font-Names="Verdana"
                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" 
                            OnClientClick="return tabbut_Click('PnlTabStk','pnlTabChem','pnlTabLstDoc','PnlTabUnLst','PnlTabHos','PnlTabRem','PnlTabPrev','btnStockist','btnChemists','btnListedDR','btnNonListedDR','btnHospital','btnRemarks','btnPreview','2');"
                            Height="24px" Width="74px" />
                    </td>
                    <td >
                        <asp:Button ID="btnNonListedDR" runat="server" Text="UnListedDR" Font-Names="Verdana"
                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                             OnClientClick="return tabbut_Click('PnlTabUnLst','pnlTabChem','PnlTabStk','pnlTabLstDoc','PnlTabHos','PnlTabRem','PnlTabPrev','btnNonListedDR','btnChemists','btnStockist','btnListedDR','btnHospital','btnRemarks','btnPreview','3');"
                            Height="24px" Width="74px" />
                    </td>
                    <td >
                        <asp:Button ID="btnHospital" runat="server" Text="Hospital" Font-Names="Verdana"
                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                             OnClientClick="return tabbut_Click('PnlTabHos','pnlTabChem','PnlTabStk','PnlTabUnLst','pnlTabLstDoc','PnlTabRem','PnlTabPrev','btnHospital','btnChemists','btnStockist','btnNonListedDR','btnListedDR','btnRemarks','btnPreview','4');"
                            Height="24px" Width="74px" />
                    </td>
                    <td >
                        <asp:Button ID="btnRemarks" runat="server" Text="Remarks" Font-Names="Verdana" Font-Size="10px"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                             OnClientClick="return tabbut_Click('PnlTabRem','pnlTabChem','PnlTabStk','PnlTabUnLst','PnlTabHos','pnlTabLstDoc','PnlTabPrev','btnRemarks','btnChemists','btnStockist','btnNonListedDR','btnListedDR','btnHospital','btnPreview','5');"
                            Height="24px" Width="74px" />
                    </td>
                    <td align ="left">
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" Font-Names="Verdana" Font-Size="10px"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" OnClick = "btnPreview_Click"
                            Height="24px" Width="74px" />
                         
                    </td>
                    
                   
                  </tr>
            </table>
            
        </asp:Panel>
          </div>
       </div>
          </ContentTemplate>
 </asp:UpdatePanel>
   <div class="Footer">
   <asp:Panel ID="pnlTab1" runat="server" Style=" display:none;">
            <table>
                <tr >
                  <td align="right" width="900px">
                        <asp:Button ID="btnClr" runat="server" Text="Clear" BackColor="Pink" Font-Names="Verdana"
                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" OnClientClick="return confirm_Clear();"
                            OnClick="btnClear_Click" Height="24px" Width="74px" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Draft Save" BackColor="Pink" Font-Names="Verdana" Visible="false" 
                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" OnClientClick="return confirm_Save();"
                            OnClick="btnSave_Click" Height="24px" Width="74px" />
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Final Submit" BackColor="Pink" Font-Names="Verdana"
                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" OnClientClick="return confirm_Submit();"
                            OnClick="btnSubmit_Click" Height="24px" Width="94px" />
                    </td>
                </tr>
            </table>
            
        </asp:Panel>
        </div> 
      <asp:GridView ID="grdWorkType" runat="server" Width="100%" HorizontalAlign="Center" 
                    AlternatingRowStyle-CssClass="alt" GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="WorkType_Code" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"WorkType_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="But_Access" HeaderStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblAccess" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"Button_Access") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="POB " HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="lblFWInd" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"FieldWork_Indicator") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns> 
                </asp:GridView>
                  <asp:GridView ID="GrdAdmStp" runat="server" Width="100%" HorizontalAlign="Center" 
                    AlternatingRowStyle-CssClass="alt" GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="sess_dcr" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="sess_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"DCRSess_Entry_Permission") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="time_dcr" HeaderStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="time_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"DCRTime_Entry_Permission") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="sess_m_dcr" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="sess_m_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"DCRSess_Mand") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="time_m_dcr" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="time_m_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"DCRTime_Mand") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="prod_mand_dcr" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="prod_mand_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"DCRProd_Mand") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="prod_qty_dcr" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="prod_qty_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"SampleProQtyDefaZero") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="max_doc_dcr_count" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="max_doc_dcr_count" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"No_Of_DCR_Drs_Count") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="max_chem_dcr_count" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="max_chem_dcr_count" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"No_Of_DCR_Chem_Count") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="max_unlst_dcr_count" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="max_unlst_dcr_count" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"No_Of_DCR_Ndr_Count") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="max_stk_dcr_count" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="max_stk_dcr_count" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"No_Of_DCR_Stockist_Count") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="max_hos_dcr_count" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="max_hos_dcr_count" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"No_of_dcr_hosp") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="remarks_dcr" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="remarks_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"DCRLDR_Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="remarkslen" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="remarks_len" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"Remarks_length_Allowed") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="sess_dcr" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="sess_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"DCRSess_Entry_Permission") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="time_dcr" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="time_dcr" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"DCRTime_Entry_Permission") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="prod_sel" HeaderStyle-Width="50px"> 
                            <ItemTemplate>
                                <asp:Label ID="prod_sel" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"No_Of_Product_selection_Allowed_in_Dcr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns> 
                </asp:GridView>
                
                           <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loading/loading6.gif" alt="" />
        </div>
     </form>
</body>     
</html>
