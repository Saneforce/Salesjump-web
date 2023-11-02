<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Compose_Mail.aspx.cs" Inherits="MasterFiles_Compose_Mail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <meta charset="utf-8">
            <meta http-equiv="X-UA-Compatible" content="IE=edge">
            <title>AdminLTE 2 | Compose Message</title>
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
            <link rel="stylesheet" href="../css/bootstrap3-wysihtml5.min.css" />
            <link href="/css/jquery.multiselect.css" rel="stylesheet" type="text/css" />
            <!-- The main CSS file -->
            <%--<link href="../assets/css/style.css" rel="stylesheet" />
                <link href="css/style.css" rel="stylesheet" />--%>
            <style type="text/css">
                .box-header > .box-tools {
                    position: absolute;
                    right: 10px;
                    top: 5px;
                }

                #compul {
                    list-style-type: none;
                }

                ul.wysihtml5-toolbar > li {
                    float: left;
                    display: list-item;
                    list-style: none;
                    margin: 0 5px 10px 0;
                }

                ul.wysihtml5-toolbar {
                    margin: 0;
                    padding: 0;
                    display: block;
                }

                .box.box-primary {
                    border-top-color: #3c8dbc;
                }

                .box-header.with-border {
                    border-bottom: 1px solid #f4f4f4;
                }

                .box .nav-stacked > li {
                    border-bottom: 1px solid #f4f4f4;
                    margin: 0;
                }

                .nav-stacked > li.active > a, .nav-stacked > li.active > a:hover {
                    background: transparent;
                    color: #444;
                    border-top: 0;
                    border-left-color: #3c8dbc;
                }

                .margin-bottom {
                    margin-bottom: 20px;
                }

                .space {
                    padding: 3px 3px;
                }
            </style>
        </head>
        <body class="hold-transition skin-blue sidebar-mini">
            <div class="wrapper">
                <!-- Content Wrapper. Contains page content -->
                <div class="content-wrapper">
                    <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Compose Mail</h1>
                    </section>
                    
                    <!-- Main content -->
                    <section class="content">
                        <div class="row">
                            <div class="col-md-3">
                                <a href="mailbox.aspx" class="btn btn-primary btn-block margin-bottom">Back to Inbox</a>
                                <div class="box box-solid">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Folders</h3>
                                        <div class="box-tools">
                                            <button type="button" style="background: transparent;" class="btn btn-box-tool" data-widget="collapse">
                                                <i class="fa fa-minus"></i>
                                            </button>
                                        </div>
                                    </div>
                                    
                                    <div class="box-body no-padding" style="">
                                        <ul class="nav nav-pills nav-stacked" style="margin: 0;">
                                            <li class="active"><a href="MailBox.aspx"><i class="fa fa-inbox"></i>Inbox
                                                <%-- <span class="label label-primary pull-right">12</span>--%></a></li>
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
                                    <!-- /.box-header -->
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
                            <div class="col-md-9">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Compose New Message</h3>
                                    </div>
                                    <%--<div>
                                        <label id="FrwdLabel"></label>
                                        </div>--%>
                                    <!-- /.box-header -->
                                    <div class="box-body">
                                        <div class="form-group">
                                            <%-- <input class="form-control" list="products" type="text" id="txtSearch" placeholder="To:" />--%>
                                            <select class="form-control" id="txtSearch" multiple="multiple"></select>
                                        </div>
                                        <div class="form-group ssss">
                                            <input class="form-control" type="text" id="txtSubj" placeholder="Subject:" />
                                        </div>
                                        <%-- <div class="form-group ffff">
                                            <input class="form-control" id="txtFrwdSubj" placeholder="FrwdSubject:">
                                            </div>
                                            <div class="form-group rrrr">
                                            <input class="form-control" id="txtreply" placeholder="ReplySubject:">
                                            </div>--%>
                                        <div>
                                            <label id="FrwdLabel"></label>
                                        </div>
                                        <div class="form-group">
                                            <textarea id="compose-textarea" class="form-control" style="height: 300px">
                                                <%--  <h1><u>Heading Of Message</u></h1>
                                                    <h4>Subheading</h4>
                                                    <p>But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain
                                                    was born and I will give you a complete account of the system, and expound the actual teachings
                                                    of the great explorer of the truth, the master-builder of human happiness. No one rejects,
                                                    dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know
                                                    how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again
                                                    is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain,
                                                    but because occasionally circumstances occur in which toil and pain can procure him some great
                                                    pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise,
                                                    except to obtain some advantage from it? But who has any right to find fault with a man who
                                                    chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that
                                                    produces no resultant pleasure? On the other hand, we denounce with righteous indignation and
                                                    dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so
                                                    blinded by desire, that they cannot foresee</p>
                                                    <ul>
                                                    <li>List item one</li>
                                                    <li>List item two</li>
                                                    <li>List item three</li>
                                                    <li>List item four</li>
                                                    </ul>
                                                    <p>Thank you,</p>
                                                    <p>John Doe</p>--%>
                                            </textarea>
                                        </div>
                                        <div class="form-group">
                                            <div class="btn btn-default btn-file">
                                                <i class="fa fa-paperclip"></i>Attachment                 
                                                <input type="file" id="fileUploader" name="attachment" multiple="multiple" />
                                            </div>
                                            <br />
                                            <br />
                                            <table>
                                                <tr>
                                                    <td style="width: 100% !important;">
                                                        <!--Thumbnail image -->
                                                        <div id="showimage">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%-- <div id="divFiles" style="width:100px" class="files">
                                                            </div>--%>
                                                        <ul id="ulList"></ul>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p class="help-block">Max. 3MB</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <div class="pull-right">
                                            <%--<button type="button" class="btn btn-default"><i class="fa fa-pencil"></i>Draft</button>--%>
                                            <%--<button type="submit" id="btnUpload" class="btn btn-primary"><i class="fa fa-envelope-o"></i>Send</button>--%>
                                            <button type="button" id="btnUpload" class="btn btn-primary"><i class="fa fa-envelope-o"></i>Send</button>
                                        </div>
                                        <%-- <button type="reset" class="btn btn-default"><i class="fa fa-times"></i>Discard</button>--%>
                                    </div>
                                    <!-- /.box-footer -->
                                </div>
                                <!-- /. box -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </section>
                    <!-- /.content -->
                </div>
            </div>
            <script src="../js/jquery.min.js" type="text/javascript"></script>
            <!-- jQuery UI 1.10.3 -->
            <script src="../js/jquery-ui-1.10.3.min.js" type="text/javascript"></script>
            <script type="text/javascript">
                $(function () {
                    //Add text editor
                    $("#compose-textarea").wysihtml5();
                });
                //$("#txtFrwdSubj").hide();
            </script>
            <script type="text/javascript">
                var transid = 0;
                var names = 0;
                $(function () {
                    transid = getParameterValues('Trans_Sl_No');
                    names = getParameterValues('From');
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

                var Getfilename = [];
                var filelist = "";
                var selfile = '';

                function uploadFiles() {
                    var file = document.getElementById("fileUploader")//All files
                    for (var i = 0; i < file.files.length; i++) {
                        uploadSingleFile(file.files[i], i);
                    }
                }

                function uploadSingleFile(file, i) {
                    var fileId = i;
                    var mimeid1 = i;
                    var ajax = new XMLHttpRequest();
                    //Progress Listener
                    ajax.upload.addEventListener("progress", function (e) {
                        var percent = (e.loaded / e.total) * 100;
                        $("#status_" + fileId).text(Math.round(percent) + "% uploaded, please wait...");
                        $('#progressbar_' + fileId).css("width", percent + "%")
                        $("#notify_" + fileId).text("Uploaded " + (e.loaded / 1048576).toFixed(2) + " MB of " + (e.total / 1048576).toFixed(2) + " MB ");
                    }, false);
                    //Load Listener
                    ajax.addEventListener("load", function (e) {
                        $("#status_" + fileId).text(e.target.responseText);
                        $('#progressbar_' + fileId).css("width", "100%");

                        //Hide cancel button
                        var _cancel = $('#cancel_' + fileId);
                        _cancel.hide();
                    }, false);
                    //Error Listener
                    ajax.addEventListener("error", function (e) {
                        $("#status_" + fileId).text("Upload Failed");
                    }, false);
                    //Abort Listener
                    ajax.addEventListener("abort", function (e) {
                        $("#status_" + fileId).text("Upload Aborted");
                    }, false);

                    ajax.open("POST", "UploadHandler.ashx"); // Your API .net, php

                    var uploaderForm = new FormData(); // Create new FormData
                    uploaderForm.append("file", file); // append the next file for upload
                    ajax.send(uploaderForm);

                    //Cancel button
                    var _cancel = $('#cancel_' + fileId);
                    _cancel.show();

                    _cancel.on('click', function () {
                        ajax.abort();
                    })
                }
                             

                function alpha(e) {
                    var k;
                    //document.all ? k = e.keyCode : k = e.which;
                    e.keyCode ? k = e.keyCode : k = e.which;
                    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
                }


                $("input[type='text']").on("keypress", function (e) {
                    var k;
                    //document.all ? k = e.keyCode : k = e.which;
                    e.keyCode ? k = e.keyCode : k = e.which;
                    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
                });

                $(document).ready(function () {
                    sf = '<%=Session["Sf_Code"]%>';
                    //SearchText();
                    radiochange();
                    forwardmsg();
                    if (transid != 0 && transid != undefined) {
                        $('#txtSearch  > option').each(function () {
                            //if ('admin' == $(this).val()) { $(this).prop('selected', true); }
                            var name1 = (names).split(',');
                            for ($i = 0; $i < name1.length; $i++) {
                                if (name1[$i] == $(this).val()) { $(this).prop('selected', true); }
                            }
                        });
                    }
                    $('#txtSearch').multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                    $('#btnUpload').on('click', function () {
                        uploadFiles();
                        //  if (transid == 0) {                                     
                        var TransNo = 0;
                        var From = '';
                        //var content = '';
                        var cc = '';
                        var bcc = '';
                        var ip = '';
                        var MailSentTime = '';
                        //var ToSfName = '';
                        var CCsfname = '';
                        var BccSfName = '';
                        var MailSfName = '';
                        var star = '';
                        var imp = '';
                        var soc = '';
                        var promo = '';
                        var MailRead = '';


                        var To = [];
                        To = $('#txtSearch').val() || [];
                        if (To.length == 0) {
                            alert('Select a FieldForce');
                            return false;
                        }
                        var seldiv = '';
                        for (var i = 0; i < To.length; i++) {
                            seldiv += To[i] + ',';
                        }

                        var sfname = '';
                        $('#txtSearch option:selected').each(function () {
                            sfname += $(this).text() + ',';
                        });

                        var sub = $('#txtSubj').val();
                        if (sub == '') {
                            alert('Enter the subject');
                            $('#txtSubj').focus();
                            return false;
                        }
                        var message = $('#compose-textarea').val();

                        transid = (typeof transid === "undefined") ? 0 : transid;


                        data = { "DivCode": "<%=Session["div_code"]%>", "Trans_Sl_No": TransNo, "Mail_SF_From": "<%=Session["sf_code"]%>", "Mail_SF_To": seldiv, "Mail_Subject": sub, "Mail_Content": message, "Mail_Attachement": filelist, "Mail_CC": cc, "Mail_BCC": bcc, "Division_Code": "<%=Session["div_code"]%>", "System_Ip": ip, "Mail_Sent_Time": MailSentTime, "To_SFName": sfname, "CC_SfName": CCsfname, "Bcc_SfName": BccSfName, "Mail_SF_Name": MailSfName, "Frwd_Mail_Id": transid, "@StarredMessage": star, "@ImportantMessage": imp, "SocialMessage": soc, "Promotions": promo, "MailRead": MailRead }
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Compose_Mail.aspx/ForwardMsgSaveData",
                            //data: "{'data':'" + JSON.stringify(data) + "','Itype':'" + Itype + "'}",
                            data: "{'data':'" + JSON.stringify(data) + "'}",
                            dataType: "json",
                            success: function (data) {
                                var successtext = data.d;
                                if (successtext == "SalesForce inserted") {
                                    alert('SalesForce inserted');
                                    //clearfields();
                                    //window.location.href = 'MailBox.aspx';
                                }
                                else {
                                    alert(successtext);
                                    window.location.href = 'SentMail.aspx';
                                }
                            },
                            error: function (rs) {
                                alert(rs);
                            }
                        });

                    });

                    $("#fileUploader").change(function () {

                        var id = $(this).attr("id");

                        var ext = $('#' + id + '').val().split('.').pop().toLowerCase();
                        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'mp3', 'mp4', 'txt', 'xls', 'xlsx', 'doc', 'docx', 'pdf', 'zip']) == -1) {
                            alert('invalid extension!');
                        }
                        else {

                            var fileType;
                            var extension;
                            var previewImage = $("#showimage");
                            previewImage.html("");
                            $("#ulList").empty();
                            var i = 0;
                            $($(this)[0].files).each(function () {
                                var file = $(this);
                                //var reader = new FileReader();
                                var ul;
                                // reader.onload = function (e) {

                                var cimg = $("<div style='float: left;'></div>");

                                var tb = $("<table></table>");
                                var tr = $("<tr></tr>");
                                var td = $("<td></td>");
                                fileId = i;
                                var img = $("<img/>");
                                switch (file[0].type) {
                                    case 'application/vnd.openxmlformats-officedocument.wordprocessingml.document': case 'application/msword':
                                        //img.attr("src", 'http://localhost:14802/FileImage/Word.jpg');
                                        img.attr("src", 'http://fmcg.sanfmcg.com//FileImage/Word.jpg');
                                        break;
                                    case 'application/pdf':
                                        img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/pdf.jpg');
                                        break;
                                    case 'text/plain':
                                        img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/txt.png');
                                        break;
                                    case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet': case 'application/vnd.ms-excel':
                                        img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/Excel.png');
                                        break;
                                    case 'application/x-sql':
                                        img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/sqlimg.png');
                                        break;
                                    case 'application/zip':
                                        img.attr("src", 'http://fmcg.sanfmcg.com/FileImage/Zipimg.png');
                                        break;
                                    default:
                                        img.attr('src', 'http://fmcg.sanfmcg.com/uploads/' + file[0].name);
                                }
                                img.attr("style", "height:150px;width: 150px;padding-left:10px");
                                td.append(img);
                                tr.append(td);
                                tb.append(tr);

                                var fragment = "";
                                var fileName = file[0].name; // get file[0] name
                                var fileSize = file[0].size; // get file[0] size 
                                if (fileSize < 3000000) {
                                    fileType = file[0].type; // get file type   
                                    console.log(fileType);
                                    fragment += "<li>" + fileName + " " + fileSize + " bytes. Type :" + fileType + "</li>";
                                    ul = $("#ulList").append(fragment);
                                    Getfilename = file[0].name;
                                    filelist += Getfilename + ",";
                                    var tr2 = $("<tr></tr>");
                                    var td2 = $("<td></td>");
                                    $(td2).append('<div><ul id="ulList_' + ul + '></ul></div>');
                                    tr2.append(td2);
                                    tb.append(tr2);

                                    var tr1 = $("<tr></tr>");
                                    var td1 = $("<td></td>");

                                    //if (fileSize < 2000000) {

                                    $(td1).append('<div style="width:170px">' + '<div class="col-md-12">' +
                                        '<div class="progress"><div class="progress-bar" role="progressbar" id="progressbar_' + fileId + '" aria-valuemin="0" aria-valuemax="100" style="width:0%"></div></div>' +
                                        '</div>' +
                                        '<div class="col-md-12">' +
                                        '<div class="col-md-6">' +
                                        '<input type="button" class="btn btn-danger" style="display:none;line-height:6px;height:25px" id="cancel_' + fileId + '" value="cancel">' +
                                        '</div>' +
                                        '<div class="col-md-6">' +
                                        '<p class="progress-status" style="text-align: right;margin-right:-15px;font-weight:bold;color:saddlebrown" id="status_' + fileId + '"></p>' +
                                        '</div>' +
                                        '</div>' +
                                        '<div class="col-md-12">' +
                                        '<p id="notify_' + fileId + '" style="text-align: right;"></p>' +
                                        '</div>' + '</div>');

                                    tr1.append(td1);
                                    tb.append(tr1);
                                    cimg.append(tb);
                                    i++;
                                    previewImage.append(cimg);
                                }
                                else {
                                    alert('Maximum 3 Mb');
                                }

                                //previewImage.append(cimg);
                            });
                        }
                    });

                });


                function radiochange() {
                    //$('#dropdistributor').html("");
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Compose_Mail.aspx/GetAutoCompleteData",
                        data: "{'divcode':'<%=Session["div_code"]%>'}",
                        dataType: "json",
                        success: function (data) {
                            SFStates = JSON.parse(data.d) || [];
                            //var ms = $('#txtSearch');
                            //ms.empty();
                            if (SFStates.length > 0) {
                                var states = $("#txtSearch");
                                states.empty()
                                for (var i = 0; i < SFStates.length; i++) {
                                    //states.append($('<option value="' + SFStates[i].Sf_Code + '">' + SFStates[i].Sf_Name + '</option>'))                               
                                    states.append($('<option value="' + SFStates[i].SF_Code + '">' + SFStates[i].SF_Name + '</option>'))

                                }
                            }
                            //$('#txtSearch').multiselect();
                            $('#txtSearch').multiselect({
                                columns: 3,
                                placeholder: 'Select FieldForce',
                                search: true,
                                searchOptions: {
                                    'default': 'Search FieldForce'
                                },
                                selectAll: true
                            }).multiselect('reload');
                            $('.ms-options ul').css('column-count', '3');
                        }
                    });

                }


                function forwardmsg() {
                    //var transid;
                    var Orders = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Compose_Mail.aspx/GetReadData",
                        data: "{'divcode':'<%=Session["Division_Code"]%>','sfcode':'<%=Session["sf_code"]%>','trans':'" + transid + "'}",
                        dataType: "json",
                        success: function (data) {
                            Orders = JSON.parse(data.d) || [];
                            for (var i = 0; i < Orders.length; i++) {
                                $("#FrwdLabel").append(Orders[i].Mail_Content);
                            }

                        },
                        error: function (result) {
                            //alert("Error");
                        }
                    });
                }

                function insertforwardmsg() {
                    //uploadFiles();
                    var TransNo = 0;
                    var From = '';
                    var content = '';
                    var cc = '';
                    var bcc = '';
                    var ip = '';
                    var MailSentTime = '';
                    var ToSfName = '';
                    var CCsfname = '';
                    var BccSfName = '';
                    var MailSfName = '';

                    var To = [];
                    To = $('#txtSearch').val() || [];
                    if (To.length == 0) {
                        alert('Select a FieldForce');
                        return false;
                    }
                    var seldiv = '';
                    for (var i = 0; i < To.length; i++) {
                        seldiv += To[i] + ',';
                    }
                    var sfname = '';
                    $('#txtSearch option:selected').each(function () {
                        sfname += $(this).text() + ',';
                    });
                    var sub = $('#txtSubj').val();
                    if (sub == '') {
                        alert('Enter the subject');
                        $('#txtSubj').focus();
                        return false;
                    }
                    var message = $('#compose-textarea').val();
                    if (message == '') {
                        alert('Enter the message');
                        $('#compose-textarea').focus();
                        return false;
                    }
                    data = { "DivCode": "<%=Session["div_code"]%>", "Trans_Sl_No": TransNo, "Mail_SF_From": "<%=Session["sf_code"]%>", "Mail_SF_To": seldiv, "Mail_Subject": sub, "Mail_Content": content, "Mail_Attachement": filelist, "Mail_CC": cc, "Mail_BCC": bcc, "Division_Code": "<%=Session["div_code"]%>", "System_Ip": ip, "Mail_Sent_Time": MailSentTime, "To_SFName": sfname, "CC_SfName": CCsfname, "Bcc_SfName": BccSfName, "Mail_SF_Name": MailSfName, "Frwd_Mail_Id": transid }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Compose_Mail.aspx/ForwardMsgSaveData",
                        //data: "{'data':'" + JSON.stringify(data) + "','Itype':'" + Itype + "'}",
                        data: "{'data':'" + JSON.stringify(data) + "'}",
                        dataType: "json",
                        success: function (data) {
                            var successtext = data.d;
                            if (successtext == "forwardmsg inserted") {
                                alert('forwardmsg inserted');
                                //clearfields();
                                //window.location.href = 'SalesForce_List.aspx';
                            }
                            else {
                                alert(successtext);
                            }
                        },
                        error: function (rs) {
                            alert(rs);
                        }
                    });

                }
            </script>
        </body>
    </html>
</asp:Content>

