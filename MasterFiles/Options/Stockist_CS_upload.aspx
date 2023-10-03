<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Stockist_CS_upload.aspx.cs" Inherits="MasterFiles_Stockist_CS_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <link href="/css/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <link href="/css/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery.multiselect.js" type="text/javascript"></script>--%>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        .content{
            background-color:#ffffff;
        }
        .list-group {
            max-height: 250px;
            min-height: 250px;
            width: 95%;
            margin-bottom: 10px;
            overflow: scroll;
            -webkit-overflow-scrolling: touch;
        }
        </style>
     <form id="frm1" runat="server">
         <h3>Distributor ClosingStock Upload</h3>
         <br />
         <br />
          <asp:Panel ID="pnlDoctor" Width="90%" runat="server">
          <center>
          <div class="row" style="margin-top:10px;">
                <asp:FileUpload ID="FlUploadcsv" runat="server" Style="padding-left: 20px;position:absolute" />
                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Excel Format" OnClick="lnkDownload_Click" />&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="upbt" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="upbt_Click" />
            </div>
              <br />
              <br />
              </center>
              </asp:Panel>

          <div id="dvStatus" style="display:block;width:80%;overflow:auto;height:120px">
                                
                                </div>
         </form>
</asp:Content>


