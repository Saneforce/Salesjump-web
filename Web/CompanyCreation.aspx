<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="CompanyCreation.aspx.cs" Inherits="CompanyCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <input type="button" onclick="CreateSite()" value="Create Site" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        function CreateSite() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "CompanyCreation.aspx/CreateWebSite",
                data: "{'SiteName':'abcnew', 'IP':'37.61.220.198'}",
                dataType: "json",
                success: function (data) {
                    console.log("print:"+data);
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
    </script>
</asp:Content>

