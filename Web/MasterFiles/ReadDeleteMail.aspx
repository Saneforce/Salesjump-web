<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ReadDeleteMail.aspx.cs" Inherits="MasterFiles_ReadDeleteMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>AdminLTE 2 | Read Mail</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
        <style>
            .box-header > .box-tools {
                position: absolute;
                right: 10px;
                top: 5px;
            }
        </style>


    </head>
    <body class="hold-transition skin-blue sidebar-mini">
        <div class="wrapper">
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Trash Mail
                    </h1>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <div class="col-md-3">
                            <a href="compose_mail.aspx" class="btn btn-primary btn-block margin-bottom">Compose</a>

                            <div class="box box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Folders</h3>

                                    <div class="box-tools">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body no-padding">
                                    <ul class="nav nav-pills nav-stacked">
                                        <li><a href="MailBox.aspx"><i class="fa fa-inbox"></i>Inbox
                 
                                            <%--<span class="label label-primary pull-right">12</span>--%></a></li>
                                        <li><a href="SentMail.aspx"><i class="fa fa-envelope-o"></i>Sent</a></li>
                                        <%--<li><a href="#"><i class="fa fa-file-text-o"></i>Drafts</a></li>
                                        <li><a href="#"><i class="fa fa-filter"></i>Junk <span class="label label-warning pull-right">65</span></a>
                                        </li>--%>
                                        <li><a href="StarViewMail.aspx"><i class='fa fa-star-o text-black'></i>Starred</a></li>
                                        <li><a href="DeleteMail.aspx"><i class="fa fa-trash-o"></i>Trash</a></li>                                        
                                    </ul>
                                </div>
                                <!-- /.box-body -->
                            </div>
                            <!-- /. box -->
                            <div class="box box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Labels</h3>

                                    <div class="box-tools">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body no-padding">
                                    <ul class="nav nav-pills nav-stacked">
                                        <li><a href="ViewImportant.aspx"><i class="fa fa-circle-o text-red"></i>Important</a></li>
                                        <li><a href="PromotionMail.aspx"><i class="fa fa-circle-o text-yellow"></i>Promotions</a></li>
                                        <li><a href="SocialMail.aspx"><i class="fa fa-circle-o text-light-blue"></i>Social</a></li>
                                    </ul>
                                </div>
                                <!-- /.box-body -->
                            </div>
                            <!-- /.box -->
                        </div>
                        <!-- /.col -->
                        <div class="col-md-9" id="dvContents">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Read Mail</h3>

                                   <%-- <div class="box-tools pull-right">
                                        <a href="#" class="btn btn-box-tool" data-toggle="tooltip" title="Previous"><i class="fa fa-chevron-left"></i></a>
                                        <a href="#" class="btn btn-box-tool" data-toggle="tooltip" title="Next"><i class="fa fa-chevron-right"></i></a>
                                    </div>--%>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body" id="ReadMail">                                   

                                </div>                               
                            </div>

                            <!-- /. box -->
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                </section>
                <!-- /.content -->
            </div>
            <div class="control-sidebar-bg"></div>
        </div>

        <script type="text/javascript">
            var AllOrders = [];
            var sf;
            var sf1;
            var fdt = '';
            var tdt = '';
            //var filtrkey = 'All';
            var sortid = '';
            var asc = true;
            var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "SF,Mail_Subject,Mail_SF_From";
            var transid;
            var i = 0;
            var image;
            var bfarray = [];
            var imgspliarry = [];
            //var tr;
            var tr = [];
            var td;
            var img;
            //var img;

            $(function () {
                transid = getParameterValues('Trans_Sl_No');
                //$("[id$=Label1]").text(transid);
            });
            function getParameterValues(key) {
                var pageURL = window.location.search.substring(1);
                var urlQS = pageURL.split('&');
                for (var i = 0; i < urlQS.length; i++) {
                    var paramName = urlQS[i].split('=');
                    if (paramName[0] == key) {
                        //replace the special char like "+","&" etc from value
                        var value = paramName[1].replace('%20', ' ').replace('%26', '&').replace('+', ' ');
                        return value;
                    }
                }
            }

            function print() {
                var contents = $("#dvContents").html();
                var frame1 = $('<iframe />');
                frame1[0].name = "frame1";
                frame1.css({ "position": "absolute", "top": "-1000000px" });
                $("body").append(frame1);
                var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
                frameDoc.document.open();
                //Create a new HTML document.
                frameDoc.document.write('<html><head><title>DIV Contents</title>');
                frameDoc.document.write('</head><body>');
                //Append the external CSS file.
                frameDoc.document.write('<link href="style.css" rel="stylesheet" type="text/css" />');
                //Append the DIV contents.
                frameDoc.document.write(contents);
                frameDoc.document.write('</body></html>');
                frameDoc.document.close();
                setTimeout(function () {
                    window.frames["frame1"].focus();
                    window.frames["frame1"].print();
                    frame1.remove();
                }, 500);
            }

            function filebind($imgs) {
                var bfiles = $imgs
                bfarray = bfiles.split(',');
                var dd = [];
                dd = bfarray.length - 1;
                // imgspliarry = bfarray.length - 1;


                var previewImage = $("#showimage");
                //var previewImage = $("<div id='showimage'></div>");
                previewImage.html("");
                //var cimg = $("<div style='float: left;'></div>");
                //var tb = $("<table></table>");
                //var imgnametable = $("<table></table>");
                //var imgnamerow = $("<tr></tr>");
                //tr = $("<tr>");
                var pgrecord = 5;
                var pgNo = 1;
                fileId = i;
                //var img = $("<img/>");'
                st = PgRecords * (pgNo - 1); slno = 0;
                for ($i = st; $i < st + PgRecords ; $i++) {

                    if ($i < dd) {

                        var cimg = $("<div style='float: left;margin-bottom: 1rem;'></div>");
                        var tb = $("<table></table>");
                        var imgnametable = $("<table></table>");
                        var imgnamerow = $("<tr></tr>");
                        tr = $("<tr>");

                        img = $("<img/>");
                        if ((bfarray[$i].indexOf('.docx') > -1) || bfarray[$i].indexOf('.doc') > -1)
                            //img.attr("src", 'http://localhost:14802/FileImage/Word.jpg');
                            img.attr("src", 'http://fmcg.sanfmcg.com//FileImage/Word.jpg');
                        else if ((bfarray[$i].indexOf('.txt') > -1))
                            image = img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/txt.png');
                        else if ((bfarray[$i].indexOf('.pdf') > -1))
                            image = img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/pdf.jpg');
                        else if ((bfarray[$i].indexOf('.xlsx') > -1))
                            image = img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/Excel.png');
                        else if ((bfarray[$i].indexOf('.zip') > -1))
                            image = img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/Zipimg.png');
                        else
                            image = img.attr('src', 'http://fmcg.sanfmcg.com/uploads/' + bfarray[$i]);

                        //tr = $("<tr></tr>");
                        td = $("<td></td>");
                        img.attr("style", "height:150px;width: 150px;padding-left:10px");
                        td.append(img);
                        tr.append(td);
                        tb.append(tr);
                        cimg.append(tb);                        
                        td1 = $("<td style='width: 150px;padding-left:40px'><a href='http://localhost:14802/FileDownload.ashx?Mail_Attachement=uploads/" + bfarray[$i] + "'>" + bfarray[$i] + "</a></td>");
                        imgnamerow.append(td1);
                        imgnametable.append(imgnamerow);
                        cimg.append(imgnametable);
                        previewImage.append(cimg);
                    }

                }

                //i++;

                //previewImage.append(cimg);

            }

            $(document).ready(function () {
                var file = $(this);
                fileId = i;
                //var img = $("<img/>");

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ReadDeleteMail.aspx/GetReadDeleteData",
                    data: "{'divcode':'<%=Session["Division_Code"]%>','sfcode':'<%=Session["sf_code"]%>','trans':'" + transid + "'}",
                    dataType: "json",
                    success: function (data) {
                        Orders = JSON.parse(data.d) || [];
                        for (var i = 0; i < Orders.length; i++) {                          
                           
                            //$("#ReadMail").append("<div class='mailbox-read-info'><h3>" + Orders[i].Mail_Subject + "</h3><h5>From:" + Orders[i].Mail_SF_From + "<span class='mailbox-read-time pull-right'>"
                            //    + Orders[i].Mail_Sent_Time + "</span></h5></div><div class='mailbox-controls with-border text-center'> <div class='btn-group'> <button type='button' class='btn btn-default btn-sm' data-toggle='tooltip' data-container='body' title='Delete'><i class='fa fa-trash-o'></i> </button><button type='button' class='btn btn-default btn-sm' data-toggle='tooltip' data-container='body' title='Reply'><i class='fa fa-reply'></i></button> <button type='button' class='btn btn-default btn-sm' data-toggle='tooltip' data-container='body' title='Forward'><i class='fa fa-share'></i></button></div> <button type='button' class='btn btn-default btn-sm' data-toggle='tooltip' title='Print'> <i class='fa fa-print'></i></button></div><div class='mailbox-read-message'>"
                            //    + Orders[i].Mail_Content + "</div> <div id='showimage'></div> <br/><br/><br/><br/> <br/><br/><br/><br/> <br/><br/><br/><br/> <div class='box-footer'><button type='button' class='btn btn-default'><i class='fa fa-trash-o'></i>Delete</button><button type='button' class='btn btn-default' onclick='print()'><i class='fa fa-print'></i>Print</button></div>");
                            //filebind(Orders[i].Mail_Attachement)

                            $("#ReadMail").append("<div class='mailbox-read-info'><h3>" + Orders[i].Mail_Subject + "</h3><h5>From:" + Orders[i].Sf_Name + "<span class='mailbox-read-time pull-right'>"
                               + Orders[i].Mail_Sent_Time + "</span></h5></div><div class='mailbox-controls with-border text-center'> <div class='btn-group'> </div> <button type='button' class='btn btn-default btn-sm' data-toggle='tooltip' title='Print' onclick='print()'> <i class='fa fa-print'></i></button></div><div class='mailbox-read-message'>"
                               + Orders[i].Mail_Content + "</div> <table><tbody><tr><td style='width: 100% !important;'> <div id='showimage'></div></td></tr></tbody></table> <br/><br/><br/><br/> <br/><br/><br/><br/> <br/><br/><br/><br/> <div class='box-footer'><button type='button' class='btn btn-default' onclick='print()'><i class='fa fa-print'></i>Print</button></div>");
                            filebind(Orders[i].Mail_Attachement)
                            
                            
                        }

                    },
                    error: function (result) {
                        alert("Error");
                    }
                });


            });
           

        </script>
    </body>
    </html>
</asp:Content>

