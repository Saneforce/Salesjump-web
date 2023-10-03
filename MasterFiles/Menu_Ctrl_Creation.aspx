<%@ Page Title="Menu Control Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="Menu_Ctrl_Creation.aspx.cs" Inherits="MasterFiles_Menu_Ctrl_Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Company Creation</title>
        <style type="text/css">
            #tblDivisionDtls
            {
                margin-left: 300px;
            }
            #tblLocationDtls
            {
                margin-left: 300px;
            }
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
            .loading
            {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }
            .padding
            {
                padding: 3px;
            }
            td.stylespc
            {
                padding-bottom: 5px;
                padding-right: 5px;
            }
        </style>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
        </script>
        <script type="text/javascript">
            function OnTreeClick(evt) {
                var src = window.event != window.undefined ? window.event.srcElement : evt.target;
                var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
                if (isChkBoxClick) {
                    if (src.checked == true) {
                        var nodeText = getNextSibling(src).innerText || getNextSibling(src).innerHTML;

                        var nodeValue = GetNodeValue(getNextSibling(src));

                        document.getElementById("label").innerHTML += nodeText + ",";
                    }
                    else {
                        var nodeText = getNextSibling(src).innerText || getNextSibling(src).innerHTML;
                        var nodeValue = GetNodeValue(getNextSibling(src));
                        var val = document.getElementById("label").innerHTML;
                        document.getElementById("label").innerHTML = val.replace(nodeText + ",", "");
                    }
                    var parentTable = GetParentByTagName("table", src);
                    var nxtSibling = parentTable.nextSibling;
                    //check if nxt sibling is not null & is an element node
                    if (nxtSibling && nxtSibling.nodeType == 1) {
                        //if node has children    
                        if (nxtSibling.tagName.toLowerCase() == "div") {
                            //check or uncheck children at all levels
                            CheckUncheckChildren(parentTable.nextSibling, src.checked);
                        }

                    }
                    //check or uncheck parents at all levels
                    CheckUncheckParents(src, src.checked);
                }
            }

            function CheckUncheckChildren(childContainer, check) {
                var childChkBoxes = childContainer.getElementsByTagName("input");
                var childChkBoxCount = childChkBoxes.length;
                for (var i = 0; i < childChkBoxCount; i++) {
                    childChkBoxes[i].checked = check;
                }
            }

            function CheckUncheckParents(srcChild, check) {
                var parentDiv = GetParentByTagName("div", srcChild);
                var parentNodeTable = parentDiv.previousSibling;
                if (parentNodeTable) {
                    var checkUncheckSwitch;
                    //checkbox checked 
                    if (check) {
                        var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                        if (isAllSiblingsChecked)
                            checkUncheckSwitch = true;
                        else
                            return; //do not need to check parent if any(one or more) child not checked
                    }
                    else //checkbox unchecked
                    {
                        checkUncheckSwitch = false;
                    }

                    var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                    if (inpElemsInParentTable.length > 0) {
                        var parentNodeChkBox = inpElemsInParentTable[0];
                        parentNodeChkBox.checked = checkUncheckSwitch;
                        //do the same recursively
                        CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                    }
                }
            }

            function AreAllSiblingsChecked(chkBox) {
                var parentDiv = GetParentByTagName("div", chkBox);
                var childCount = parentDiv.childNodes.length;
                for (var i = 0; i < childCount; i++) {
                    if (parentDiv.childNodes[i].nodeType == 1) {
                        //check if the child node is an element node
                        if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                            var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                            //if any of sibling nodes are not checked, return false
                            if (!prevChkBox.checked) {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }

            //utility function to get the container of an element by tagname
            function GetParentByTagName(parentTagName, childElementObj) {
                var parent = childElementObj.parentNode;
                while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                    parent = parent.parentNode;
                }
                return parent;
            }

            function getNextSibling(element) {
                var n = element;
                do n = n.nextSibling;
                while (n && n.nodeType != 1);
                return n;
            }

            //returns NodeValue
            function GetNodeValue(node) {
                var nodeValue = "";
                var nodePath = node.href.substring(node.href.indexOf(",") + 2, node.href.length - 2);
                var nodeValues = nodePath.split("\\");
                if (nodeValues.length > 1)
                    nodeValue = nodeValues[nodeValues.length - 1];
                else
                    nodeValue = nodeValues[0].substr(1);
                return nodeValue;
            }  
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                //   $('input:text:first').focus();
                $('input:text').bind("keydown", function (e) {
                    var n = $("input:text").length;
                    if (e.which == 13) { //Enter key
                        e.preventDefault(); //to skip default behavior of the enter key
                        var curIndex = $('input:text').index(this);
                        if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                            $('input:text')[curIndex].focus();
                        }
                        else {
                            var nextIndex = $('input:text').index(this) + 1;

                            if (nextIndex < n) {
                                e.preventDefault();
                                $('input:text')[nextIndex].focus();
                            }
                            else {
                                $('input:text')[nextIndex - 1].blur();
                                $('#btnSubmit').focus();
                            }
                        }
                    }
                });
                $("input:text").on("keypress", function (e) {
                    if (e.which === 32 && !this.value.length)
                        e.preventDefault();
                });
                $('#<%=btnSubmit.ClientID%>').click(function () {
                    var SName = $('#<%=ddl_div.ClientID%> :selected').text();
                    if (SName == "--Select--") { alert("Please Select Division Name."); $('#<%=ddl_div.ClientID%>').focus(); return false; }
                    var SName1 = $('#<%=ddl_Desig.ClientID%> :selected').text();
                    if (SName1 == "--Select--") { alert("Please Select Designation Name."); $('#<%=ddl_Desig.ClientID%>').focus(); return false; }

                });
            });
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <center>
                <br />
                <table border="0" align="center">
                    <tr>
                        <td style="width: 100px" class="stylespc">
                            <asp:Label ID="lblDivName" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Division Name</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddl_div" runat="server" SkinID="ddlRequired" 
                                onselectedindexchanged="ddl_div_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 30px">
                        </td>
                    </tr>
                    <tr>
                        <td class="stylespc">
                            <asp:Label ID="lblDesig" runat="server" Width="110px" SkinID="lblMand"><span style="color:Red">*</span>Designation</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddl_Desig" runat="server" SkinID="ddlRequired" 
                                >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 92px">
                        </td>
                        <td colspan="3">
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td rowspan="" class="padding" align="center">
                            <%--<asp:Label ID="lblTitle_LocationDtls" runat="server"  Text="Select the State/Location" Visible="false" 
                            TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Underline="true" Font-Names="Verdana"
                            Font-Size="Small" ForeColor="#336277">
                        </asp:Label>--%>
                            <span runat="server" id="spMenu" style="font-weight: bold; text-decoration: underline;
                                border-style: none; font-family: Verdana; border-color: #E0E0E0; color: #336277">
                                Select the<asp:LinkButton ID="lnk" runat="server" Text=" " OnClick="lnk_Click"></asp:LinkButton>
                                &nbsp;Main Menu/Sub Menu</span>
                        </td>
                    </tr>
                    <tr style="height: 5px">
                        <td style="width: 92px; height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                    <td>
                    <table style="width: 745px">
                        <tr>
                            <td style="width: 196px; text-align: center">
                                <asp:LinkButton ID="MyWebSitesLink" runat="server">Home</asp:LinkButton>
                                 
                            </td>
                            <td style="width: 319px; text-align: center">
                                <asp:LinkButton ID="MyScheduleLink" runat="server">Master</asp:LinkButton>
                            </td>
                            <td style="width: 239px; text-align: center">
                                <asp:LinkButton ID="UploadPhotoLink" runat="server">Activities</asp:LinkButton>
                                
                            </td>
                        </tr>
                        <tr>
                          
                           <td style="vertical-align:top;height:320px;" >
                               <asp:Panel ID="pnlNew" runat="server" Visible="true">
                                    <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px;
                                        border-collapse: collapse">
                                        <tr>
                                            <td style="background-color: #98AFC7; color: White; font-weight: bold; font-family: Arial;"
                                                align="center">
                                                Home
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align:top;height:320px;">
                                                <asp:TreeView ID="tvTables_Menu" runat="server" ForeColor="Black" Font-Size="13pt"
                                                    RepeatColumns="3" ShowCheckBoxes="All" BackColor="#FEFCFF" CellPadding="2" CellSpacing="5"
                                                     Font-Names="Calibri" style="overflow:scroll;Width:300px;height:320px;"  OnClick="OnTreeClick(event)">
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                               
                   </td>
                   <td style="vertical-align:top;height:320px;">
                                <asp:Panel ID="Panel2" runat="server" Visible="true">
                                    <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px;
                                        border-collapse: collapse">
                                        <tr>
                                            <td style="background-color: #98AFC7; color: White; font-weight: bold; font-family: Arial;"
                                                align="center">
                                                Master
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align:top;height:320px;">
                                                <asp:TreeView ID="TreeView2" runat="server" ForeColor="Black" Font-Size="13pt" RepeatColumns="3"
                                                    ShowCheckBoxes="All" BackColor="#FEFCFF" CellPadding="2" CellSpacing="5"  
                                                    Font-Names="Calibri" OnClick="OnTreeClick(event)" style="overflow:scroll;Width:300px;height:320px;">
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                          </td>
                          <td style="vertical-align:top;height:320px;">
                                <asp:Panel ID="Panel3" runat="server" Visible="true">
                                    <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px;
                                        border-collapse: collapse">
                                        <tr>
                                            <td style="background-color: #98AFC7; color: White; font-weight: bold; font-family: Arial;"
                                                align="center">
                                                Activities
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align:top;height:320px;">
                                                <asp:TreeView ID="TreeView3" runat="server" ForeColor="Black" Font-Size="13pt" RepeatColumns="3"
                                                    ShowCheckBoxes="All" BackColor="#FEFCFF" CellPadding="2" CellSpacing="5" 
                                                    Font-Names="Calibri" OnClick="OnTreeClick(event)" style="overflow:scroll;Width:300px;height:320px;">
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                         </td>
                        </tr>
                        <tr>
                            <td style="width: 196px; text-align: center">
                                <asp:LinkButton ID="LinkButton1" runat="server">Activity Reports</asp:LinkButton>
                            </td>
                            <td style="width: 319px; text-align: center">
                                <asp:LinkButton ID="LinkButton2" runat="server">MIS Reports</asp:LinkButton>
                            </td>
                            <td style="width: 239px; text-align: center">
                                <asp:LinkButton ID="LinkButton3" runat="server">Options</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top;height:320px;">
                                <asp:Panel ID="Panel1" runat="server" Visible="true">
                                    <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px;
                                        border-collapse: collapse">
                                        <tr>
                                            <td style="background-color: #98AFC7; color: White; font-weight: bold; font-family: Arial;"
                                                align="center">
                                                Activity Reports
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align:top;height:320px;">
                                                <asp:TreeView ID="TreeView1" runat="server" ForeColor="Black" Font-Size="13pt" RepeatColumns="3"
                                                    ShowCheckBoxes="All" BackColor="#FEFCFF" CellPadding="2" CellSpacing="5"
                                                    Font-Names="Calibri" OnClick="OnTreeClick(event)" style="overflow:scroll;Width:300px;height:320px;">
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="vertical-align:top;height:320px;">
                                <asp:Panel ID="Panel4" runat="server" Visible="true">
                                    <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px;
                                        border-collapse: collapse">
                                        <tr>
                                            <td style="background-color: #98AFC7; color: White; font-weight: bold; font-family: Arial;"
                                                align="center">
                                               MIS Reports
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align:top;height:320px;">
                                                <asp:TreeView ID="TreeView4" runat="server" ForeColor="Black" Font-Size="13pt" RepeatColumns="3"
                                                    ShowCheckBoxes="All" BackColor="#FEFCFF" CellPadding="2" CellSpacing="5" 
                                                    Font-Names="Calibri" OnClick="OnTreeClick(event)" style="overflow:scroll;Width:300px;height:320px;">
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="vertical-align:top;height:320px;">
                                <asp:Panel ID="Panel5" runat="server" Visible="true">
                                    <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px;
                                        border-collapse: collapse">
                                        <tr>
                                            <td style="background-color: #98AFC7; color: White; font-weight: bold; font-family: Arial;"
                                                align="center">
                                                Options
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align:top;height:320px;">
                                                <asp:TreeView ID="TreeView5" runat="server" ForeColor="Black" Font-Size="13pt" RepeatColumns="3"
                                                    ShowCheckBoxes="All" BackColor="#FEFCFF" CellPadding="2" CellSpacing="5" 
                                                    Font-Names="Calibri" OnClick="OnTreeClick(event)" style="overflow:scroll;Width:300px;height:320px;">
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSubmit" runat="server" Width="60px" Height="25px" CssClass="btnnew"
                                Text="Save" OnClick="btnSubmit_Click" OnClientClick="return confirm('Do you want to Update in this Master');" />
                        </td>
                    </tr>
                </table>
            </center>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />
        </div>
        </form>
    </body>
    </html>
</asp:Content>
