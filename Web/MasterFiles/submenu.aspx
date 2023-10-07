
<%@ Page Title="Submenu" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="submenu.aspx.cs"
    Inherits="MasterFiles_submenu"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Submenu</title>
     <link type="text/css" rel="stylesheet" href="../css/style1.css" />
       
		 <style type="text/css">
			.grid-container {
  display: grid;

  align-content: center;
  grid-template-columns: auto auto auto;
  grid-gap: 10px;
  padding: 10px;
  font-weight: bold;
}

.grid-container > div {
 background-color: rgba(255, 255, 255, 0.8); 
  text-align: center;
  padding: 20px 0;
 font-size: 14px;
}
a, a:active, a:hover, a:visited {
    text-decoration: none;
}


            .md-tile {
                border-radius: 4px;
                box-sizing: border-box;
                color: rgb(var(--tile-title-color));
                cursor: pointer;
                height: var(--md-tile-size);
                margin-bottom: var(--md-tile-margin);
                opacity: 1;
                padding-top: var(--md-tile-padding-top);
                position: relative;
                transition-property: background, background-color, border-color, box-shadow, opacity, filter;
                width: var(--md-tile-size);
            }


           .grid-tile-container {
    position: absolute;
    transition: transform 300ms ease-in-out;
}
            .md-icon {
                width: 30px;
                    vertical-align: middle;
                background-color: var(--icon-background-color);
                
                height: var(--md-icon-size);
                justify-content: center;
                margin-bottom: var(--md-icon-margin-bottom);
               
            }

            .win * {
                font-family: 'Segoe UI', 'Roboto', arial, sans-serif;
            }
         
            
		    
        </style>
        
           
    </head>
    <body class="win">
        <form id="form1" runat="server">
           
<header>
    <h1 ></h1></header>
                   <div class="grid-container">
                         
                       <%--  <div>
  <a class="md-tile"  href="/MIS%20Reports/Attendance_view.aspx" draggable="false">
	<div class="md-tile-inner">
	<img class="md-icon" title="" alt="" src="">
	<div class="md-title" style="direction: ltr;
	"><span>Monthly Attendance</span></div></div>
</a></div>--%>
      </div>
        </form>
		 <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
     <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript">
          
	             $('.scrollmenu').hide();
	            $('.tl-row').hide();
            $(document).ready(
			
                function () {
				
                    var dataa = localStorage.getItem('d');
                    var url_string = window.location.href;
                    var url = new URL(url_string);
                    var c = url.searchParams.get("menuid");
                    toolbar(c, JSON.parse(dataa));
                    function toolbar(c, data) {

                        var tool = data.filter(function (a) {
                            return a.Parent_Menus == c && a.Tbar_ids == 0;
                        }); var str = "";
						var class1 = '';
                        for (var i = 0; i < tool.length; i++) {
							class1 = tool[i].Link_Urls == '/MasterFiles/Expense_Approval_Modified_New.aspx' ? 'class="Exp1"' : tool[i].Link_Urls == '/MasterFiles/Expense_Approval_Modified_New_Aachi.aspx'? 'class="Exp2"':tool[i].Link_Urls == '/MasterFiles/Expense_Approval_SingleDay.aspx'?'class="Exp3"':'';
                            str += "<div "+class1+" ><a class='md-tile' href='" + tool[i].Link_Urls + "?&menuid=" + tool[i].Parent_Menus + "&id=" + tool[i].Menu_IDs + "'><div class='md-tile-inner' draggable='false'><img class='md-icon' src=" + tool[i].Menu_Icons + "><div class='md-title' style='direction: ltr;font-size: 14px;'><span>" + tool[i].Menu_Names + "</span></div></div></a></div>";

                        }

                        $('.grid-container').show();
                        $('.grid-container').append(str);
                    }
					if (JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Web_Auto == 1 && JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Process_Type==0) {
                        $('.Exp2').hide();$('.Exp3').hide();
                    }
                    else if (JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Web_Auto == 2 && JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Process_Type==0) {
                        $('.Exp1').hide(); $('.Exp3').hide();
                    }
					else if (JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Process_Type==1) {
                        $('.Exp1').hide(); $('.Exp2').hide();
                    }
                });
                </script>
    </body>
    </html>
</asp:Content>

