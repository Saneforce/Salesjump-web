<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="uplaodexmple.aspx.cs" Inherits="MasterFiles_uplaodexmple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
        </head>
        <body>
            <form id="form1" runat="server">
                <div>
                    <table>
                        <tr>
                            <td>Name:</td>
                            <td><input type="text" id="fileName" /></td>
                        </tr>
                        <tr>
                            <td>File:</td>
                            <td><input type="file" id="file" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><input type="button" id="btnUpload" value="Upload" /></td>
                        </tr>
                        <tr>
                            <td colspan="2"><progress id="fileProgress" style="display: none"></progress></td>
                        </tr>
                    </table>
                    <hr/>
                    <span id="lblMessage" style="color: Green"></span>
                </div>
                <script type="text/javascript">
                    $("body").on("click", "#btnUpload", function () {
                        var formData = new FormData();
                        formData.append("fileName", $("#fileName").val());
                        formData.append("file", $("#file")[0].files[0]);
                        $.ajax({
                            url: 'UploadService.asmx/UploadFiles',
                            type: 'POST',
                            data: formData,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (fileName) {
                                $("#fileProgress").hide();
                                $("#lblMessage").html("<b>" + fileName + "</b> has been uploaded.");
                            },
                            xhr: function () {
                                var fileXhr = $.ajaxSettings.xhr();
                                if (fileXhr.upload) {
                                    $("progress").show();
                                    fileXhr.upload.addEventListener("progress", function (e) {
                                        if (e.lengthComputable) {
                                            $("#fileProgress").attr({
                                                value: e.loaded,
                                                max: e.total
                                            });
                                        }
                                    }, false);
                                }
                                return fileXhr;
                            }
                        });
                    });
                </script>
            </form>
        </body>
    </html>
</asp:Content>


