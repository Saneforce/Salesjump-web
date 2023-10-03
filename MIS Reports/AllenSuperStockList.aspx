<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="AllenSuperStockList.aspx.cs" Inherits="MIS_Reports_AllenSuperStockList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form id="form1" runat="server">
    
        <div class="container" style="width: 100%">
           
            <div class="row">
                <center>
                    <table cellpadding="0" cellspacing="5">
                        <tr>
                           <td align="left" class="stylespc">
                               <asp:Label ID="lblDivision" Visible="false" runat="server" SkinID="lblMand" Text="Division"></asp:Label>
                           </td>
                           <td align="left">
                               <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="350" AutoPostBack="true">
                               </asp:DropDownList>
                           </td>
                        </tr>
                        <tr>
                            <td align="left" class="stylespc">
                                 <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Team"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                    <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false" 
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired"></asp:DropDownList>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" 
                                    OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" SkinID="ddlRequired"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false"></asp:DropDownList>
                                <input type="checkbox" id="chkom" class="chkom hide" onchange="CheboxChanged()" value="Only Managers" /> 
                                <asp:CheckBox ID="chkVacant" Text="Only Managers" AutoPostBack="true"  OnCheckedChanged="chkVacant_CheckedChanged" CssClass="hide" runat="server" />
                            </td>
                        </tr>
                        <tr class="divmr">
                            <td align="left" class="stylespc">
                                <asp:Label ID="lblMR" runat="server" Text="FieldForce" SkinID="lblMand" CssClass="lblMR"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" CssClass="ddlMR" onselectedindexchanged="ddlMR_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="stylespc">
                                <asp:Label ID="Lbl3_Form" runat="server" Text="From Date :" Visible="true"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Width="100" Visible="false"></asp:DropDownList>	
                                <input name="TextBox3" id="TextBox3" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="stylespc">
                                <asp:Label ID="Lbl4_To" runat="server" Text="To Date :" Visible="true" ></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" Width="100" Visible="false">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                </asp:DropDownList>
                                <input name="TextBox4" id="TextBox4" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                            </td>
                        </tr>
                    </table>
                </center>

                <div class="col-md-6 col-md-offset-5">
                    <a id="btnGo" class="btn btn-primary" onclick="NewWindow().this"
                        style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
                </div>                
            </div>           
           
        </div>
    </form>
    <script type="text/javascript">

        $(document).ready(function () {

            CheboxChanged();


        });
        function CheboxChanged() {
            //alert('hi');

            if ($('#chkom').is(":checked")) {
                //$('.ddlMR').hide(); $('.lblMR').hide();
            } else if ($('#chkom').not(":checked")) {
               // $('.ddlMR').show(); $('.lblMR').show();
            }           
        }
         

        function NewWindow() {

            var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (FieldForce == "---Select Clear---" || FieldForce == "---Select---") { alert("Select FieldForce Name."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
            <%--if ($('#<%=TextBox1.ClientID%>').val() == "") { alert("Please select From Date."); $('#<%=TextBox1.ClientID%>').focus(); return false; }
        if ($('#<%=TextBox2.ClientID%>').val() == "") { alert("Please select To Date."); $('#<%=TextBox2.ClientID%>').focus(); return false; }   --%>

            if ($('#TextBox3').val() == "") { alert("Please select From Date."); $('#TextBox1').focus(); return false; }
            if ($('#TextBox4').val() == "") { alert("Please select To Date."); $('#TextBox2').focus(); return false; }


            var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
            if (TMonth == "---Select---") { alert("Select Month."); $('#<%=ddlMonth.ClientID%>').focus(); return false; }
            <%-- if (ddlMRName != '') {

            var ddlMR = document.getElementById('#<%=ddlMR.ClientID%>').value;
        }--%>

            var ddlfo_Code = $('#<%=ddlFieldForce.ClientID%>').val();
            var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();

            //alert(ddlfo_Code); alert(ddlfo_Code);

            var ddlMR = $('#<%=ddlMR.ClientID%>').val();
            var ddlMR_Name = $('#<%=ddlMR.ClientID%> :selected').text();

            //alert(ddlMR); alert(ddlMR_Name);

            //var ddlMR = document.getElementById('#<=ddlMR.ClientID%>').value;
            //var ddlFieldForceValue = document.getElementById('#<=ddlFieldForce.ClientID%>').value;

            //var ddlMonth = document.getElementById('#<=ddlMonth.ClientID%>').value;

            //var ddlYear = document.getElementById('#<=ddlYear.ClientID%>').value;

            var FDate = $('#TextBox3').val();
            var TDate = $('#TextBox4').val();

            //alert(FDate); alert(TDate);

            var mode = "Super Stock";

            //if ($('#chkom').is(":checked")) {

            //    var sURL = "RptAllenSuperStockList.aspx?&sf_code=" + ddlfo_Code + "&Sf_Name=" + ddlfo_Name + "&FDate=" + FDate + "";
            //    sURL += "&TDate=" + TDate;
            //    console.log(sURL);

            //    window.open(sURL, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            //}
            //else {
            //    if (ddlMR != -1 && ddlMR != 0) {

            //        var sURL = "RptAllenSuperStockList.aspx?&sf_code=" + ddlMR + "&Sf_Name=" + ddlMR_Name + "&FDate=" + FDate + "";
            //        sURL += "&TDate=" + TDate;

            //        console.log(sURL);

            //        window.open(sURL, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            //    }
            //    else {

            //        var sURL = "RptAllenSuperStockList.aspx?&sf_code=" + ddlfo_Code + "&Sf_Name=" + ddlfo_Name + "&FDate=" + FDate + "";
            //        sURL += "&TDate=" + TDate;
            //        console.log(sURL);

            //        window.open(sURL, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            //    }
            //}

            if (ddlMR != -1 && ddlMR != 0&&ddlMR!="") {

                var sURL = "RptAllenSuperStockList.aspx?&sf_code=" + ddlMR + "&Sf_Name=" + ddlMR_Name + "&FDate=" + FDate + "";
                sURL += "&TDate=" + TDate;

                console.log(sURL);

                window.open(sURL, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            }
            else {

                var sURL = "RptAllenSuperStockList.aspx?&sf_code=" + ddlfo_Code + "&Sf_Name=" + ddlfo_Name + "&FDate=" + FDate + "";
                sURL += "&TDate=" + TDate;
                console.log(sURL);

                window.open(sURL, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            }

        }
    </script>
</asp:Content>

