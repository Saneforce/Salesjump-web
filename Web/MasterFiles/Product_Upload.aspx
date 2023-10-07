<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Product_Upload.aspx.cs" Inherits="MasterFiles_Product_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <div class="card">
            <div class="col-lg-12" style="margin: 15px;">
                <div class="col-lg-6">
                    <h4 id="lblExcel" class="" style="position: relative; color: #336277; font-family: sans-serif; font-weight: bold; text-transform: capitalize; font-size: 18px;">Excel file</h4>
                    <br>
                    <asp:FileUpload ID="FlUploadcsv" runat="server" Style="padding-left: 20px;" />
                </div>
                <%-- <div class="col-lg-6" style="padding: 30px;">
                    <lable id="Lab_div">Select Division :</lable>
                    <select name="dd1division" onchange="javascript:setTimeout('__doPostBack(\'dd1division\',\'\')', 0)" id="dd1division" style="font-size: xx-small; color: #000000; padding: 1px 3px  0.2em; height: 24px; border-top-style: groove; font-family: Verdana; border-right-style: groove; border-left-style: groove; border-bottom-style: groove;">
                    </select>
                </div>--%>
                <div class="col-lg-6" style="text-align: center;">
                    <div>
                        <label>Excel Format File</label>
                    </div>
                     <asp:ImageButton ID="lnkDownload" runat="server" ImageUrl="~/Images/button_download-here.png" OnClick="lnkDownload_Click" />
                </div>
            </div>

            <div class="col-lg-12" style="margin-bottom: 20px;">
                <div style="text-align: center;">
                    <asp:Button name="UploadButton" runat="server" ID="UploadButton" Text="Upload" Style="height: 30px; width: 160px; box-shadow: 1px 1px 10px 1px grey; border: 0px; background-color: #479bdd; border-radius: 15px;" type="button" OnClick="btnUpload_Click" />
                </div>
            </div>

        </div>
    </form>
    <script type="text/javascript">
        var str = "";
        $(document).ready(function () {
            //$.ajax({
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    async: false,
            //    url: "Product_Upload.aspx/dd1divisionChanged",
            //    dataType: "json",
            //    success: function (data) {
            //        SFDets = JSON.parse(data.d) || [];
            //        if (SFDets.length > 0) {
            //            $("#dd1division").empty();
            //            for (var i = 0; i < SFDets.length; i++) {
            //                str += "<option value='" + SFDets[i].subdivision_code + "'>" + SFDets[i].subdivision_name + "</option>"
            //            }
            //            $("#dd1division").append(str);
            //        }
            //    },
            //    error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            //});
        })
    </script>
</asp:Content>

