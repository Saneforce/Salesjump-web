<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>
    <link href="http://fmcg.sanfmcg.com/images/logoTop.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="css_new/index.css" type="text/css" />
    <link rel="stylesheet" href="css_new/fontawesome.min.css" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>
    
</head>
<body style="min-height: 490px;">

	
     
    <div class="MainContainer">
        <div class="RightArea">
            <div class="mwlogo"></div>
            <div class="cntrlogo"></div>
        </div>
        <div class="login-group">
            <div class="container"><form id="Form1" runat="server" class="form-horizontal" enctype="multipart/form-data">
						<div style="display:block;width:100%;height:80px;text-align:center;padding-bottom:40px;"><img id="logoo" src="http://fmcg.sanfmcg.com/limg/demo_logo.png" style="max-height:100px;max-width:100%;vertical-align:middle"></div>
                        <%--<div class="wel-msg hidden" data-conn="<%=Globals.ConnString %>">
							<asp:HiddenField ID="HiddenField23" Value="" runat="server" />
							Welcome to Sales - Jump<asp:HiddenField ID="HiddenField1" Value="" runat="server" />

                        </div>--%>
				       <div class="wel-msg">
							<asp:HiddenField ID="HiddenField23" Value="" runat="server" />
							Welcome to Sales - Jump<asp:HiddenField ID="HiddenField1" Value="" runat="server" />

                        </div>
						<div class="form-cont">
							<div class="form-group">
								<i class='far fa-user'></i>
                                <asp:TextBox ID="txtUserName" runat="server" placeholder="User Name" class="form-input input" ></asp:TextBox>								
							</div>
							<div class="form-group">
								<i class="icpwd"></i> 
								<i class="far fa-lock"></i>
                                <asp:TextBox ID="txtPassWord" runat="server" class="form-input input" placeholder="Password"
                                MaxLength="15" TextMode="Password" ></asp:TextBox>
							</div>
							<div class="form-group" style="display:none">
								<div class="form-control" >
								<input ID="cRemb" type="checkbox" runat="server" />
								<label for="cRemb" class="clRemb">Remember me</label></div>
							</div>
							<div class="form-group-login">
                                <asp:Button ID="btnLogin" runat="server" OnClientClick="return validate()" Text="Login" CssClass="button-login" OnClick="btnLogin_Click" />
							</div>
						</div>
						<br>
						<br>
						<asp:Label ID="msg" runat="server" Width="100%" ForeColor="Red" Font-Bold="True"
                                       Font-Size="x-Small" Font-Names="Verdana"></asp:Label>
						<div style="display:block;width:100%;margin: 0px 0px 0px 0px;text-align:center;">
						<div style="font-size: 8px;font-family:Montserrat;margin-left: 15px;">Powered by</div>
						<div style="font-size: 13px;font-family: Montserrat;font-weight: 500;"><img src="img/clogo.png" style="height:30px;vertical-align: middle;margin: 0px 5px;font-size: 8px;font-family:Montserrat;"></div>
						</div>
					</form>    
            </div>
        </div>
    </div>
	
	 <script type="text/javascript">
        var hash='';

        function DoesParentExists()
        {
 	        hash = location.hash.split('#')[1];

	        if(hash==undefined){
		        console.log(location.href)
	 	        sploc=location.href.replace(/http:\/\//gi,'').replace(/https:\/\//gi,'').split('.');
		        hash =((sploc[0]=='www')?sploc[1]:sploc[0])+".";
	        console.log(hash+ '= '+hash.replace(".",''))
	        }
	        document.getElementById('HiddenField1').value = hash;
	        document.getElementById('logoo').src ='limg/'+hash.replace(".",'')+'_logo.png';

	        console.log('Logo: limg/'+hash+'_logo.png');
            genListOfPoster(hash.replace('.',''),hash);
        }
        
        $(document).ready(function () {
            localStorage.clear();	  
	     $('.input').keypress(function (e) {
                if (e.which === 32)
                    return false;
            });   
        });
        function genListOfPoster(shDiv,key)
        { 
	        $.ajax({
		        type: "POST",
		        contentType: "application/json;charset=utf-8",
		        url: "Index.aspx/getSlides",
		        data:JSON.stringify({shDiv}),
		        dataType: "json",
		        success: function (data) {
			        $owl=$('.owl-carousel');
			        if(data.d.length>0){
                        document.location.href="indexs.aspx#"+key;
			        }
		        },
		        error: function ajaxError(result) {
			        console.log(result.status + ' : ' + result.statusText);
		        }
	        });
        }
    </script><script type='text/javascript'>              DoesParentExists();</script>
</body>
</html>