<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Outlet_target_Upload.aspx.cs" Inherits="MasterFiles_Options_Outlet_target_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
    <HTML>
   <HEAD>
      <title>WebForm1</title>
       <style>
        .container {
            max-width: 800px;
            max-height: 500px;
            margin: 0 auto;
            padding: 20px;
            /*background-color: #f2f2f2;*/
            border: 1px solid #ccc;
            border-radius: 4px;
        }
		.msgbx{
		display:block;
		width:80%;
		overflow:auto;
		height:120px;
		}
        /*.form-group {
            margin-bottom: 10px;
        }*/
          .button {
  display: inline-block;
  font-size: 4px;
  cursor: pointer;
  text-align: center;
  text-decoration: none;
  outline: none;
  color: #fff;
  background-color: #4CAF50;
  border: none;
  border-radius:30px;
  
}

    </style>
      <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
      <meta name="CODE_LANGUAGE" Content="C#">
      <meta name="vs_defaultClientScript" content="JavaScript">
      <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
   </HEAD>
   <body>
       <div class="card container" style="width:100%">
        <h3 align="center">Outlet Placement Upload</h3>

        <form method="post" enctype="multipart/form-data" runat="server">
            <div class="row">
                <label for="file">Select a file:</label>
                <asp:FileUpload ID="FlUploadcsv" runat="server" style="padding-left:20px;"  />

                <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="Upload_Click" CssClass="button" Style="float:right;" Width="100px" Height="30px" BackColor="#19a4c6"
                    ForeColor="#ffffff" Font-Size="Medium"/>
            </div>
            
            <div class="row">
                <asp:Label ID="lblExc" runat="server" Text="Excel Format File" Style="position: sticky;
                    color: #336277; font-family: Verdana; font-weight: bold; text-transform: capitalize;
                    font-size: 12px;"></asp:Label>
            <asp:ImageButton ID="lnkDownload" runat="server" ImageUrl="~/Images/button_download-here.png" Style="position: sticky;"
                    OnClick="lnkDownload_Click" />
            </div>

        </form>

            <div>
               <br />
               <br />
               <br />
              <asp:Label ID="Label4" runat="server" Text="Note:" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="Label6" Font-Size="11px" Font-Names="Verdana" runat="server" Text="1) Month must be in MM format.Example, for January Month must Enter 01 or 1 as Month."></asp:Label>
                                <br />
           </div>
           <br />
           <br />

        <span style="color:red">Errors: </span><p style="display:block;width:100%;overflow:auto;height:120px"><asp:Literal ID="messageLiteral" runat="server" /></asp:Literal></p>
 
	</div>
   </body>
</HTML>
</asp:Content>

