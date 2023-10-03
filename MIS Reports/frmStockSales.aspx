<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="frmStockSales.aspx.cs" Inherits="MIS_Reports_frmStockSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="header"><b>Distributor's Stock & Sales Analysis</b></div><form runat="server">
<div class="row" style="margin-left:100px;margin-top:30px">
<table>
<tr><td>Fieldforce</td><td>:</td><td><asp:DropDownList ID="ddlFF" style="width:250px" runat="server" onchange="getDistributor()"></asp:DropDownList></td></tr>
<tr><td>Distributor Name</td><td>:</td><td><asp:DropDownList ID="ddlDist" style="width:250px" runat="server"></asp:DropDownList></td></tr>
<tr><td>Month</td><td>:</td><td><asp:DropDownList ID="ddlMnth" style="width:150px" runat="server"></asp:DropDownList></td></tr>
<tr><td>Year</td><td>:</td><td><asp:DropDownList ID="ddlYr" style="width:100px" runat="server"></asp:DropDownList></td></tr>
<tr><td colspan="3" align="center"><button type="button" class="btn btn-primary btn-vwReport">View</button></td></tr>
</table></div></form>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script>
$x=$('#<%=ddlDist.ClientID%>');$($x).empty();vs="<option value=''>--select Distributor--</option>";$($x).append(vs);
mons=["January","February","March","April","May","June","July","August","September","October","November","December"]
$x=$('#<%=ddlMnth.ClientID%>');$($x).empty();vs="<option value=''>--Month--</option>";for(il=0;il<12;il++){vs+="<option value='"+(il+1)+"'>"+mons[il]+"</option>";}$($x).append(vs);$x=$('#<%=ddlYr.ClientID%>');$($x).empty();
vs="<option value=''>--Year--</option>";yr=<%=DateTime.Parse(DateTime.Now.ToString()).Year%>;for(il=yr-1;il<=yr;il++){vs+="<option value='"+il+"'>"+il+"</option>";}$($x).append(vs);


function getDistributor(){
    $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "frmStockSales.aspx/getDistributor",
                data: "{'div_code':'" + <%=divCd %> + "','SFCode':'"+$('#<%=ddlFF.ClientID%>').val()+"'}",
                dataType: "json",
                success: function (data) {
                    //[{"Field_Code":"MR2798,","Division_Code":"52","Stockist_code":"9505","Stockist_Name":"C J T STORES","Distributor_Code":9505,"subdivision_code":"70,"},{"Field_Code":"MR2798,","Division_Code":"52","Stockist_code":"9480","Stockist_Name":"DIVYA AGENCIES","Distributor_Code":9480,"subdivision_code":"70,"},{"Field_Code":"MR2798,","Division_Code":"52","Stockist_code":"9485","Stockist_Name":"JAYAN TRADERS","Distributor_Code":9485,"subdivision_code":"70,"},{"Field_Code":"MR2798,","Division_Code":"52","Stockist_code":"9486","Stockist_Name":"LEKSHMI AGENCIES","Distributor_Code":9486,"subdivision_code":"70,"},{"Field_Code":"MR2798,","Division_Code":"52","Stockist_code":"9504","Stockist_Name":"P K T TRADERS","Distributor_Code":9504,"subdivision_code":"70,"},{"Field_Code":"MR2798","Division_Code":"52","Stockist_code":"10751","Stockist_Name":"PATHANAMTHITTA- OTW","Distributor_Code":10751,"subdivision_code":""},{"Field_Code":"MR2798,","Division_Code":"52","Stockist_code":"9503","Stockist_Name":"SUDHAS TRADERS","Distributor_Code":9503,"subdivision_code":"70,"},{"Field_Code":"MR2798,","Division_Code":"52","Stockist_code":"9502","Stockist_Name":"THOMSON AGENCIES","Distributor_Code":9502,"subdivision_code":"70,"}]
                    $x=$('#<%=ddlDist.ClientID%>');
                    jsv=JSON.parse(data.d);$($x).empty();
                    vs="<option value=''>--select Distributor--</option>";
                    for(il=0;il<jsv.length;il++){
                    vs+="<option value='"+jsv[il].Stockist_code+"'>"+jsv[il].Stockist_Name+"</option>"
                    }
                    $($x).append(vs);
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            })
}
$(".btn-vwReport").click(function()
{
    document.location.href="vwRptStockSales.aspx?SF="+$('#<%=ddlFF.ClientID%>').val()+"&Dist="+$('#<%=ddlFF.ClientID%>').val()+"&Mnth="+$('#<%=ddlFF.ClientID%>').val()+"&Yr="+$('#<%=ddlYr.ClientID%>').val();
});
</script>
</asp:Content>

