<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales_Top10_Exception.aspx.cs" Inherits="MIS_Reports_Sales_Top10_Exception" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales Top 10 Exception</title>
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />
    <style type="text/css">
   
.regular-checkbox {
background-color: #FF0000;
}
    .ddl
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
             font-family:Andalus;         
          background-image:url('css/download%20(2).png');
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
         .ddl1
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
                     
        
  
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }         
  
  </style>
    <script type = "text/javascript">
        var popUpObj;
        function showModalPopUp(FYear, viewby, viewtop) {

            popUpObj = window.open("rptSales_Top10_Exception.aspx?FYear=" + FYear + "&viewby=" + viewby + "&viewtop=" + viewtop,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=0," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
        }

</script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
      
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
    <style type="text/css">
       
.button {
  display: inline-block;
  border-radius: 4px;
  background-color: #6495ED;
  border: none;
  color: #FFFFFF;
  text-align: center;  
  font-bold:true;
  width: 75px;
  height:29px;
  transition: all 0.5s;
  cursor: pointer;
  margin: 5px;
}

.button span {
  cursor: pointer;
  display: inline-block;
  position: relative;
  transition: 0.5s;
}

.button span:after {
  content: '»';
  position: absolute;
  opacity: 0;
  top: 0;
  right: -20px;
  transition: 0.5s;
}

.button:hover span {
  padding-right: 25px;
}

.button:hover span:after {
  opacity: 1;
  right: 0;
}


       
</style>
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <div id="Divid" runat="server">
        </div>
        <center>
            <br />
            
            <table>
                
                <tr>
                    <td align="center" class="style1" Width="90px">
                        <asp:Label ID="Top" runat="server" Text="Select Top"  
                            Font-Names="Andalus"  ></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:DropDownList ID="topdrop" runat="server"  SkinID="ddlRequired"  CssClass="ddl"
                            >
                              <asp:ListItem Value="0" Text="--- Select ---"></asp:ListItem>
                        <asp:ListItem Value="1" >5</asp:ListItem>
                          <asp:ListItem Value="2" >10</asp:ListItem>
                            <asp:ListItem Value="3" >15</asp:ListItem>
                              <asp:ListItem Value="4" >20</asp:ListItem>
                                <asp:ListItem Value="5" >25</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td   width="90px" align="center">
                        <asp:Label ID="Label1" runat="server" Text="View By" Font-Names="Andalus" 
                            ></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="viewdrop" runat="server" AutoPostBack="true" SkinID="ddlRequired"   CssClass="ddl">
                             <asp:ListItem Value="0" Text="--- Select ---" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="DistributorWise"></asp:ListItem>
                            <asp:ListItem Value="2" Text="BrandWise" ></asp:ListItem>
                              <asp:ListItem Value="3" Text="CategoryWise" ></asp:ListItem>
                                <asp:ListItem Value="4" Text="ProductWise" ></asp:ListItem>

                        </asp:DropDownList>
               
                    </td>
                </tr>
                <tr>
                   
                    <td  class="stylespc" align="center">
                        <asp:Label ID="lblFYear" Width="50px" runat="server" Text="Year" 
                            Font-Names="Andalus" Font-Bold="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" AutoPostBack="false" CssClass="ddl">
                        </asp:DropDownList>
                    </td>
                </tr>
                
             
            </table>
            <br />
            <br />
            <button  ID="btnGo" class="button" runat="server"   OnServerClick="btnGo_Click"  style="vertical-align:middle"><span>View</span></button>
            <br />
            <br />
            <br />
            <br />
            
         
    </form>
</body>
</html>
