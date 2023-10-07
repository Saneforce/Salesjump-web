<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Fieldforce_Upload.aspx.cs" Inherits="MasterFiles_Options_Fieldforce_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/jscript">
        $(document).ready(function () {
            $('#<%=upbt.ClientID%>').on('click', function () {
                var stcode = $('#<%=ddlst.ClientID%> :selected').val();
            });
        });
    </script>
    <form id="frm1" runat="server">
        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="row">
                    <asp:Label ID="lbldiv" runat="server" Text="Division" CssClass="col-md-2  col-md-offset-3  control-label" style="padding-left:85px;padding-top:7px;" ></asp:Label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                            <asp:DropDownList ID="ddldiv" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddldiv_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top:15px">
                    <asp:Label ID="Label1" runat="server" Text="State" CssClass="col-md-2  col-md-offset-3  control-label" style="padding-left:85px;padding-top:7px;" ></asp:Label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                            <asp:DropDownList ID="ddlst" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
                <center>
                    <div class="row" style="margin-top: 10px;">
                        <asp:FileUpload ID="FlUploadcsv" runat="server" Style="padding-left: 20px; position: absolute" />
                        <asp:Button ID="Upldbt" CssClass="btn btn-primary" runat="server" Text="Excel File" OnClick="lnkDownload_Click" />
                        <asp:Button ID="upbt" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="upbt_Click" />
                    </div>
                </center>
        <div class="row col-md-5">
            <textarea readonly class="form-control" id="dvStatus" style="width: 485px;height: 97px;margin-top: 20px;resize:none;background-color: #fff;">Important Note: 
                1.Enter the Employee Type in Emp_Type Column in Excel.
                2.If Employee is Baselevel Emp_Type=1 else Emp_Type=2.
            </textarea>
        </div>
    </form>
</asp:Content>

