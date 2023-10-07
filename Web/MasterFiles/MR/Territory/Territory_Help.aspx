<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_Help.aspx.cs" Inherits="MasterFiles_MR_Territory_Help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script runat="server">
[System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static AjaxControlToolkit.Slide[] GetSlides()
        {
            return new AjaxControlToolkit.Slide[] { 
            new AjaxControlToolkit.Slide("Slide/Scr1.png", "Click Add Button", ""),
            new AjaxControlToolkit.Slide("Slide/Scr2.png", "Enter Name", ""),
            new AjaxControlToolkit.Slide("Slide/Scr3.png", "Select Type", ""),
            new AjaxControlToolkit.Slide("Slide/Scr4.png", "Click Save", ""),
            
            };
        }
</script>
   <style type="text/css">    

.slideTitle
{
	font-weight:bold;
	font-size:small;
	font-style:italic;
}

.slideDescription
{
	font-size:small;
	font-weight:bold;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <table>
    <tr>
    <td>
    <asp:Label ID="lblLang" runat="server" SkinID="lblMand" Text="Select the Language">    
    </asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="ddlLang" runat="server" SkinID="ddlRequired" AutoPostBack="true" 
            onselectedindexchanged="ddlLang_SelectedIndexChanged" >
    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
    <asp:ListItem Text="Tamil" Value="0"></asp:ListItem>
    <asp:ListItem Text="English" Value="1"></asp:ListItem>
    <asp:ListItem Text="Hindi" Value="2"></asp:ListItem>
    <asp:ListItem Text="Telugu" Value="3"></asp:ListItem>
    <asp:ListItem Text="Malayalam" Value="4"></asp:ListItem>
    </asp:DropDownList>
    </td>
    </tr>
    </table>
    </center>
    <br />
    <br />
    <center>
    <asp:Panel ID="pnlenglish" runat="server" Visible="false">
      <object type="application/x-shockwave-flash" data="dewplayer.swf?mp3=mp3/English.mp3"

        width="240" height="20" id="dewplayer">

        <param name="wmode" value="transparent" />

        <param name="movie" value="dewplayer.swf?mp3=mp3/Voice_002.mp3" />

    </object>

    </asp:Panel>

<asp:Panel ID="pnlhindhi" runat="server" Visible="false">
       <object type="application/x-shockwave-flash" data="dewplayer.swf?mp3=mp3/hindhi.mp3"

        width="240" height="20" id="Object1">

        <param name="wmode" value="transparent" />

        <param name="movie" value="dewplayer.swf?mp3=mp3/hindhi.mp3" />

    </object>
    </asp:Panel>
    </center>
    <br />
     <div style="text-align:center"> 
          <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
          </asp:ToolkitScriptManager>
          <b>
            SlideShow Demonstration</b><br /><br /></div>
                  <div style="text-align:center">
            <asp:Label runat="Server" ID="imageTitle" CssClass="slideTitle"/><br />
            <asp:Image ID="Image1" runat="server" 
                Height="300"
                Style="border: 1px solid black;width:auto" 
                ImageUrl="~/MasterFiles/MR/Territory/Slide/Scr1.png"
                AlternateText="Createion Image" />
            <asp:Label runat="server" ID="imageDescription" CssClass="slideDescription"></asp:Label><br /><br />
            <asp:Button runat="Server" ID="prevButton" Text="Prev" Font-Size="Larger" />
            <asp:Button runat="Server" ID="playButton" Text="Play" Font-Size="Larger" />
            <asp:Button runat="Server" ID="nextButton" Text="Next" Font-Size="Larger" />
            <asp:SlideShowExtender ID="slideshowextend1" runat="server" 
                TargetControlID="Image1"
                SlideShowServiceMethod="GetSlides" 
                AutoPlay="true" 
                ImageTitleLabelID="imageTitle"
                ImageDescriptionLabelID="imageDescription"
                NextButtonID="nextButton" 
                PlayButtonText="Play" 
                StopButtonText="Stop"
                PreviousButtonID="prevButton" 
                PlayButtonID="playButton" 
                Loop="true" />
        </div>
  
    </form>
</body>
</html>
