<%@ Page Title="Product " Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="locationtest.aspx.cs" Inherits="locationtest" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
    </head>
    <body>
        <button type="button" id="audio" onclick="getvoicedecode()">Audio</button>
        <button type="button" id="audiostop" onclick="pausefile()">Stop</button>

        <audio id="bgm" src = "MasterFiles/Reports/AudFiles/bgm.mp3"  controls = "controls" ></audio><br />
         <audio  id="voice" src = "MasterFiles/Reports/AudFiles/MR4126_1696056541410.ogg" controls = "controls" ></audio>

        


    </body>
    </html>


</asp:Content>


