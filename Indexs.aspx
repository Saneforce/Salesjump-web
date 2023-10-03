<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indexs.aspx.cs" Inherits="Indexs" %>

<!DOCTYPE html>
<html lang="en" dir="ltr">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link href="images/faviicon41.png" rel="shortcut icon">
    <title>Fast Moving Consumer Goods</title>
    <!-- Bootstrap stylesheet -->
    <link rel="stylesheet" href="lcss/bootstrap.css">
    <!-- stylesheet -->
    <link rel="stylesheet" href="lcss/style.css" title="style" />
    <link rel="stylesheet" href="lcss/responsive.css">
    <!-- font-awesome -->
    <link rel="stylesheet" href="lcss/font-awesome.min.css" type="text/css" />
    <!-- crousel css -->
    <link rel="stylesheet" href="lcss/owl.carousel.css" type="text/css" />
    <link rel="stylesheet" href="lcss/owl.theme.css" type="text/css" />
    <link rel="stylesheet" href="lcss/headerstyle.css" />
    <link href="http://fmcg.sanfmcg.com/images/logoTop.ico" rel="shortcut icon" type="image/x-icon" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <%--<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
var hash='';
function DoesParentExists()
{
 	hash = location.hash.split('#')[1];

	if(hash==undefined){
		console.log(location.href)
	 	sploc=location.href.replace(/http:\/\//gi,'').split('.');
		hash =((sploc[0]=='www')?sploc[1]:sploc[0])+".";
	console.log(hash+ '= '+hash.replace(".",''))
	}
	document.getElementById('HiddenField1').value = hash;
	document.getElementById('logoo').src ='limg/'+hash.replace(".",'')+'_logo.png';

	console.log('Logo: limg/'+hash+'_logo.png');
    genListOfPoster(hash.replace('.',''));
}

function genListOfPoster(shDiv)
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
				for(i=0;i<$('.owl-dot').length;i++)
				{
					$owl.trigger('remove.owl.carousel',0);
				}
				$owl.trigger('refresh.owl.carousel');
				$ii=0;
				$.each(data.d, function (key, value) {
	    			$owl.trigger('add.owl.carousel', [$('<div class="item"><img class="img-responsive" alt="banner" src="Posters/'+value.SlideName+'"/></div>'), $ii])
					$ii++;
				});
				$owl.trigger('refresh.owl.carousel');
			}


		},
		error: function ajaxError(result) {
			console.log(result.status + ' : ' + result.statusText);
		}
	});
}
    </script>
</head>
<body class="home-one">
    <section class="top-bar">
    <div class="container">
    <h2 class="hidden">topbar</h2>
        <div class="top-bar-left pull-left">

            <div class="social clearfix">
                <ul>
                    <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                    <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                    <li><a href="#"><i class="fa fa-skyatlas"></i></a></li>
                    <li><a href="#"><i class="fa fa-tumblr"></i></a></li>
                </ul>
            </div>            
        </div>
        <div class="top-bar-right pull-right">          
            <div class="top-info">
                <ul>
                    <li><a href="#">fmcgsupport@saneforce.com</a></li>
                </ul>
            </div>            
        </div>
    </div>
</section>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <header class="header clearfix">

    <div class="main-header stricky">
        <div class="container">
            <div class="logo pull-left">
                <a href="index.html">
                    <%--<img src="limg/logo.png" style="max-width: 150px;max-height: 76px;" alt="SANeForce.com">--%>
                <asp:Image ID="logoo" runat="server" height="82px"></asp:Image>
                </a>
            </div>

            <div class="nav-outer">
                <nav class="mainmenu-area clearfix">
                    <div class="navbar" role="navigation">
                        
                      
                       
                    </div>
                </nav>

                <div class="header-info">
                    <div class="icon-box">
                        <span class="icon-smartphone"></span>
                    </div>
                    <div class="content-box">
                        <p>Call us now - 24/7 Support<br /><span>(+91) 9962-111-203</span></p>
                    </div>
                </div>
            </div>

        </div>
    </div>
</header>
    <!-- form-set start here #e73e38-->
    <div class="form-set">
        <input id="hdshrtname" type="hidden" runat="server" value="" />
        <!-- slider starts here -->
        <div id="slideshow" class="owl-carousel owl-loaded owl-drag">
            <div class="item">
                <img class="img-responsive" alt="banner" src="Posters/slider_banner1.png" /></div>
            <div class="item">
                <img class="img-responsive" alt="banner" src="Posters/slider_banner2.png" />
            </div>
            <div class="item">
                <img class="img-responsive" alt="banner" src="Posters/slider_banner3.png" />
            </div>
            <div class="item">
                <img class="img-responsive" alt="banner" src="Posters/slider_banner4.png" />
            </div>
            <div class="item">
                <img class="img-responsive" alt="banner" src="Posters/slider_banner5.png" />
            </div>
        </div>
        <!-- slider end here -->
        <!-- slider_search start here -->
        <div class="container">
            <div class="row main-form">
                <div class="col-md-4 col-sm-6 slider_search">
                    <h4>
                        Login</h4>
                    <form runat="server" class="form-horizontal" enctype="multipart/form-data">
                    <asp:HiddenField ID="HiddenField1" runat="Server" />
                    <div class="col-sm-12 col-xs-12">
                        <div class="form-group">
                            <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="UserName"
                                required=""></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPassWord" runat="server" class="form-control" placeholder="Password"
                                MaxLength="15" TextMode="Password" required=""></asp:TextBox>
                        </div>
                    </div>
                    <asp:Button ID="btnLogin" runat="server" OnClientClick="return validate()" Text="Login"
                        CssClass="button" OnClick="btnLogin_Click" />
                    <br>
                    <br>
                    <asp:Label ID="msg" runat="server" Width="100%" ForeColor="Red" Font-Bold="True"
                        Font-Size="x-Small" Font-Names="Verdana"></asp:Label>
                    </form>
                </div>
            </div>
        </div>
        <!-- slider_search end here -->
    </div>
    <!-- form-set end here -->
    <!-- footer start here -->
    <footer>
    <span class="caret"></span>
    <div id="footer" class="container" style="padding:5px;width:100%">
        <div class="row">
            <div class="col-sm-4">
                <h5>About Us</h5>
                <p>
                    SANeFORCE.com divi of San Media Ltd is an Sales Force Automation company providing SaaS based 
                    Sales Force Automation and Customer Relation Management services to Life Sciences, 
                    FMCG and Healthcare enterprises across India and the world markets.
                </p>
            </div>
            <div class="col-sm-5">
                <div id="social_media">
                    <div class="links1">
                        <div class="address">
                            <h5>Contact Us</h5>
                            <ul class="list-unstyled">
                                <li>
                                    <i class="fa fa-map-marker"></i> No 4, Chamiers Road, Nandanam, Chennai - 600 035 INDIA<br/>
                                </li>
                                <li>
                                    <i class="fa fa-phone"></i> +91 9962-226-800<br/>
                                </li>
                                <li class="mail">
                                    <i class="fa fa-envelope"></i>sales@saneforce.com
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3 testimonial" style="text-align:right;padding-right:10px">
            	<img src="img/cLogo.png" style= "height: 50px;" />
            </div>
        </div>
        
    </div>
</footer>
    <!-- jquery -->
    <script src="ljs/jquery.2.1.1.min.js" type="text/javascript"></script>
    <!-- bootstrap js -->
    <script src="ljs/bootstrap.min.js" type="text/javascript"></script>
    <!-- crousel js -->
    <script src="ljs/owl.carousel.min.js" type="text/javascript"></script>
    <script src="ljs/slideshow.js" type="text/javascript"></script>
    <!-- footer end here -->
    <script type='text/javascript'>        DoesParentExists();</script>
</body>
</html>
