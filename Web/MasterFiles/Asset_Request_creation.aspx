<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Asset_Request_creation.aspx.cs" Inherits="MasterFiles_Asset_Request_creation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
     <title>Asset Request</title>
             <style type="text/css">
            table {
                border-collapse: separate;
                border-spacing: 9px;
                width: 100%;
            }

            .is-invalid {
                outline: none;
                box-shadow: 0 0 0 3px rgba(255, 0, 0, 0.4);
            }

            .scrldiv, .scrldiv2 {
                width: 545px;
                height: 116px;
            }

                .scrldiv input {
                    margin-top: 10px;
                }

                .scrldiv2 input {
                    margin-top: 10px;
                }

            label {
                /*font-size: 11px;*/
                font-family: 'Open Sans','arial';
                font-weight: 700;
                margin: 0px;
            }

            .panel-default {
                border-color: #ddd !important;
                border: 1px solid transparent;
            }

            input {
                width: 100%;
                border: 1px solid #d8d8d8 !important;
                padding: 6px 6px 7px !important;
                border-radius: 3px !important;
                line-height: inherit !important;
                background: #fff;
                /*box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.1);*/
                transition: all 0.3s ease;
                /*border: 1px solid #00809D;*/
            }

                input:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }

            select {
                width: 100%;
                border: 1px solid #D5D5D5 !important;
                padding: 6px 6px 7px !important;
            }

                select:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }

            .bootstrap-select:not([class*=col-]):not([class*=form-control]):not(.input-group-btn) {
                width: 294px !important;
            }

            .bootstrap-select .dropdown-menu {
                /* min-width: 100%; */
                overflow: scroll;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                box-sizing: border-box;
                max-width: 450px;
            }

            .sticky-header.scrolled {
                background-color: #eee;
            }

            .spinner {
                margin: 100px auto;
                width: 50px;
                height: 40px;
                text-align: center;
                font-size: 10px;
            }

                .spinner > div {
                    background-color: #333;
                    height: 100%;
                    width: 6px;
                    display: inline-block;
                    -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                    animation: sk-stretchdelay 1.2s infinite ease-in-out;
                }

                .spinner .rect2 {
                    -webkit-animation-delay: -1.1s;
                    animation-delay: -1.1s;
                }

                .spinner .rect3 {
                    -webkit-animation-delay: -1.0s;
                    animation-delay: -1.0s;
                }

                .spinner .rect4 {
                    -webkit-animation-delay: -0.9s;
                    animation-delay: -0.9s;
                }

                .spinner .rect5 {
                    -webkit-animation-delay: -0.8s;
                    animation-delay: -0.8s;
                }

            @-webkit-keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    -webkit-transform: scaleY(0.4)
                }

                20% {
                    -webkit-transform: scaleY(1.0)
                }
            }

            @keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    transform: scaleY(0.4);
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    transform: scaleY(1.0);
                    -webkit-transform: scaleY(1.0);
                }
            }

            .spinnner_div {
                width: 1200px;
                height: 1000px;
                background: rgba(255, 255, 255, 0.1);
                backdrop-filter: blur(2px);
                position: absolute;
                z-index: 100;
                overflow-y: hidden;
            }



            .col-xs-9 {
                width: 76%;
            }

            .imgBlinks {
                animation: blink 0.5s linear infinite;
            }

            @keyframes blink {
                0% {
                    opacity: 1;
                }

                50% {
                    opacity: 0;
                }

                100% {
                    opacity: 1;
                }
            }

            #form-container {
                display: none;
                width: 800px;
                height: 600px;
                background-color: #ffffff;
                border: none;
                border: 1px solid #19a4c6;
                border-radius: 5px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                position: fixed;
                right: 0;
                top: 50%;
                transform: translateY(-50%);
            }

                #form-container input[type="text"], textarea {
                    width: 100%;
                    padding: 10px;
                    margin-top: 10px;
                    font-size: 16px;
                    box-sizing: border-box;
                }

                #form-container input[type="submit"] {
                    width: 100%;
                    padding: 10px;
                    margin-top: 10px;
                    font-size: 16px;
                    background-color: #4CAF50;
                    color: white;
                    cursor: pointer;
                }

            .sort-container {
                display: flex;
                align-items: center;
                justify-content: end;
            }

            label {
                font-weight: bold;
                width: 48%;
                white-space: nowrap !important;
            }

            select {
                padding: 10px;
                font-size: 16px;
                background-color: #f2f2f2;
                border: none;
                border-radius: 5px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                width: 150px;
            }

            .options-container {
                display: flex;
                align-items: center;
                justify-content: space-between;
                align-content: center;
                flex-wrap: wrap;
                flex-direction: row;
                margin-top: 10px;
            }

            .checkbox-container {
                border: 1px solid darkgrey;
                border-radius: 5px;
                display: flex;
                width: 100%;
                padding: 5px;
                flex-wrap: wrap;
                flex-direction: row;
                align-items: flex-start;
                max-height: 350px;
                overflow: scroll;
            }

            input[type="checkbox"] {
                margin-right: 10px;
            }

            .selected-options-container {
                border: 1px solid darkgrey;
                margin-bottom: 9px;
                width: 100%;
                background-color: #f2f2f2;
                padding: 8px;
                border-radius: 5px;
                display: flex;
                flex-direction: row;
                flex-wrap: wrap;
                max-height: 127px;
                overflow: scroll;
            }

            .selected-option {
                display: flex;
                align-items: center;
                margin-bottom: 10px;
            }

                .selected-option span {
                    margin-right: 10px;
                }

            .remove-option {
                cursor: pointer;
                color: red;
            }

            .selected-option {
                display: inline-block;
                margin-right: 5px;
                margin-bottom: 5px;
            }

            .remove-option {
                font-size: 18px;
                padding-left: 10px;
                cursor: pointer;
            }


            .search-container {
                display: flex !important ;
                align-items: center!important;
                width: 100% !important
            }

            .search-icon {
                padding-top: 28px !important;
                padding-right: 0px;
                cursor: pointer !important;
                margin-right: 0;
                border-bottom: 1px solid grey;
                margin-bottom: 5px
            }

            .search-input {
                width: 250px !important;
                transition: width 0.4s ease-in-out !important;
                padding: 10px !important;
                margin-left: 10px !important;
                border: none !important;
                outline: none !important;
                background-color: transparent !important;
                font-size: 16px !important;
                color: gray !important;
                margin-bottom: 5px;
                border-bottom: 1px solid grey !important;
                margin-left: 0PX !important;
            }

                .search-input:focus {
                    box-shadow: 0 0 0 0px rgb(21 156 228 / 40%) !important;
                }

            .search-container:hover .search-input {
                width: 100% !important;
                border-bottom: 1px solid grey !important;
                margin-left: 0PX !important;
            }

            input[type="checkbox"] {
                margin-right: 0;
                width: 24px !important;
            }

            .upload-area {
                border: 2px dashed #ccc;
                padding: 20px;
                text-align: center;
                position: relative;
            }

            .drag-text {
                margin-top: 10px;
            }

            .image-preview {
                display: none;
                justify-content: center;
                align-items: center;
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background-color: rgba(0, 0, 0, 0.5);
            }

            .preview-text {
                display: flex;
                flex-direction: column;
                justify-content: center;
                align-items: center;
                color: #fff;
            }

                .preview-text i {
                    font-size: 40px;
                    margin-bottom: 10px;
                }

                .preview-text span {
                    font-size: 18px;
                }

            #previewImage {
                max-width: 100%;
                max-height: 100%;
                object-fit: contain;
            }

            .upload-area.dragging {
                background-color: #f7f7f7;
            }

                .upload-area.dragging .drag-text {
                    color: #999;
                }

                .upload-area.dragging i {
                    color: #999;
                }

                .upload-area.dragging h3 {
                    color: #999;
                }

            img#profile-pic-label img {
                max-width: 100%;
                height: auto;
                transition: opacity 0.3s ease-in-out;
            }

                img#profile-pic-label img.hidden {
                    opacity: 0;
                }

            .bootstrap-select:not([class*=col-]):not([class*=form-control]):not(.input-group-btn) {
                width: 334px !important;
            }

            .imgsml {
                padding: 4px;
                background: aliceblue;
                width: 36px;
                height: 28px;
                border: 1px solid #d8d8d8;
                border-radius: 3px;
                margin-left: 5px;
            }

            .imglbl {
                width: 25px !important;
                height: 20px !important;
            }

            .uplimg {
                max-width: 25px !important;
                max-height: 20px !important;
            }
            /* CSS */
            .modal {
                display: none;
                position: fixed;
                z-index: 9999;
                padding-top: 20px;
                right: 0;
                left: 0%;
                top: 0;
                width: 100%;
                height: 100%;
                overflow: auto;
                background-color: rgb(0 0 0 / 66%);
                opacity: 1.8;
            }

            .modal-content {
                margin: auto;
                display: block;
                width: 30%;
                max-width: 85%;
            }

            .close {
                color: red;
                float: right;
                font-size: 28px;
                font-weight: bold;
                padding: 10px;
                cursor: pointer;
				opacity:1;
            }

                .close:hover,
                .close:focus {
                    color: #bbb;
                    text-decoration: none;
                    cursor: pointer;
                }

            .bootstrap-select > .dropdown-toggle {
                position: relative;
                width: 86%;
            }

            .drpshort .bootstrap-select > .dropdown-toggle {
                position: relative;
                width: 58%;
            }
			 .switch-container {
            display: flex;
			display:inline-flex;
            align-items: center;
        }

        .switch-label {
            margin-right: 10px;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

        .switch input {
            opacity: 0;
            width: 0;
            height: 0;
        }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            transition: 0.4s;
        }

        .slider:before {
            position: absolute;
            content: "";
            height: 16px;
            width: 16px;
            left: 2px;
            bottom: 2px;
            background-color: white;
            transition: 0.4s;
        }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            transform: translateX(20px);
        }

        .slider.round {
            border-radius: 20px;
        }

        .slider.round:before {
            border-radius: 50%;
        }
         .custom-selectpicker {
          }
           .dropdown-menu {
            display: none;
        }
           .image-upload {
    /*height: 70px;
    width: 70px;*/
    border-radius: 50%;
    box-shadow: rgb(0 0 0 / 16%) 0px 3px 6px, rgb(0 0 0 / 23%) 0px 3px 6px;
    margin: 100px auto 0px auto;
    overflow-y: hidden;
}
.image-upload > input
{
    display: none;
}

.image-upload img
{    width: 23px;
    cursor: pointer;
    position: absolute;
    top: 130px;
    left: 606px;
    color: var(--white);
                     height: 12px;
                 }
input[type="radio"]{
    width: 50px !important;
}

        </style>
           <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
	<script src="https://cdnjs.cloudflare.com/ajax/libs/openlayers/6.5.0/ol.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/openlayers/6.5.0/ol.css" />
          <script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
          </script>
          
          <script type="text/javascript">
              var profilePicInput = document.getElementById("profile-pic-input");
              profilePicInput.addEventListener("change", function (event) {
                  handleProfilePicUpload(event, 'profile-label');
              });
              function handleProfilePicUpload(event, parameter) {
                  const selectedFile = event.target.files[0];
                  const reader = new FileReader();
                  reader.readAsDataURL(selectedFile);
                  reader.onload = function () {
                      const newProfilePic = document.createElement("img");
                      newProfilePic.src = reader.result;
                      newProfilePic.alt = "New Profile Picture";
                      newProfilePic.classList.add("uplimg");
                      newProfilePic.addEventListener("load", function () {
                          // Code to animate the transition from default to new profile picture
                      });
                      const profilePicLabel = document.getElementById(parameter);
                      profilePicLabel.innerHTML = "";
                      profilePicLabel.appendChild(newProfilePic);
                  };
              }
              $(document).ready(function () {
                  $('#uplfile').on('change', function (e) {
                      var img = URL.createObjectURL(e.target.files[0]);
                      $('#upimg').attr('src', img);
                      $.ajax({
                          url: 'SalesForce_Handler.ashx',
                          type: 'POST',
                          data: new FormData($('form')[0]),
                          cache: false,
                          contentType: false,
                          processData: false,
                          success: function (file) {
                              propic_name = file.name;
                              alert(file.name + "has been uploaded.");
                          }
                      });
                  });
              });
          </script>
               </head >
      <body>
          <div class="row">
                            <div class="col-lg-12 sub-header">Asset Request
<button style="float: right;margin-right:20px;" type="button" id="btnsubmit" class="btn btn-primary">Save</button>
        </div>
          </div>
              
    <form style="background: #ffffff; box-shadow: 0px 3px 12px rgba(0, 0, 0, 0.25); border-radius: 8px; margin-top: 5px;" runat="server">
               <div class="container">
                   <div class="col-md-6">
                       <h4>Basic Information</h4>
                       <br />
                        <table id="basicinfo" style="border-collapse: collapse; margin-top: 9px;">
                            <tr>
                                <td align="left" style="width: 150px;">
                                    Asset Image
                                </td>
                                <td>
                                    <div colspan="2" align="center" style="border-radius: 50%;box-shadow: rgb(0 0 0 / 16%) 0px 3px 6px, rgb(0 0 0 / 23%) 0px 3px 6px;">
                                         <img alt="upimg" id="upimg" style=" min-width: 75px; min-height: 75px;" src="../Images/Pdf_Img.jpg" />
                                           <label for="uplfile" class="input-group-btn">
                                               <span><i class="fa fa-cloud-upload append-icon"></i></span>
                   
                                           <input accept=".jpg,.jpeg,.png" id="uplfile" class="sr-only" name="uplfile" type="file" /></label>
                                    </div>
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="left" style="width: 150px;">
                                    Asset Image
                                </td>
                                <td>
                                    <label for="profile-pic-input" id="profile-label" style="border-radius: 50%;box-shadow: rgb(0 0 0 / 16%) 0px 3px 6px, rgb(0 0 0 / 23%) 0px 3px 6px;">
                                         <img src="../Images/Pdf_Img.jpg" id="custimg" alt="Default Profile Picture" class="lik" style=" min-width: 75px; min-height: 75px;" />
                                        Select Image
                                        <input type="file" accept=".jpg,.jpeg,.png" id="profile-pic-input" style="display:none;" />
                                    </label>
                                </td>
                            </tr>--%>
                             <tr>
                             <td align="left">
                                 <label>Asset Name</label><span style="color: red;margin-left: 20px;">*</span>
                             </td>
                             <td>
                                 <input class="col-xs-9" id="Txt_Asset_nam" type="text" autocomplete="off" required />
                             </td>
                         </tr>
                            <tr>
                                <td align="left" >
                                    <label>Asset Code</label>
                                    <span style="color: red;margin-left: 20px;">*</span>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="txtAsset_code" type="text" autocomplete="off" maxlength="30" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Category</label>
                                    <span style="color: red;margin-left: 20px;">*</span>
                                </td>
                                <td>
                                    <select id="ddlcat" class="col-xs-9">
                                     </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Location</label>
                                    <span style="color: red;margin-left: 20px;">*</span>
                                </td>
                                <td>
                                    <select id="ddlloc" class="col-xs-9">
                                     </select>
                                </td>
                            </tr>
                             <tr>
                                 <td align="left" >
                                     <label>Asset Status</label>
                                     <span style="color: red;margin-left: 20px;">*</span>
                                 </td>
                                 <td>
                                     <select id="ddlstatus" class="col-xs-9">
                                      </select>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="left" >
                                     <label>Brand Name</label>
                                 </td>
                                 <td>
                                     <input class="col-xs-9" id="txtbrand_nm" type="text" autocomplete="off" maxlength="30" />
                                 </td>
                             </tr>
                            <tr>
                                <td align="left" >
                                    <label>Model</label>
                                </td>
                                <td>
                                    <select id="ddlmodel" class="col-xs-9">
                                     </select>
                                </td>
                            </tr>
                             <tr>
                                 <td align="left" >
                                     <label>Serial Number</label>
                                 </td>
                                 <td>
                                     <input class="col-xs-9" id="txtser_nm" type="text" autocomplete="off" maxlength="30" />
                                 </td>
                             </tr>
                            <tr>
                                <td align="left" >
                                    <label>Condition</label>
                                    <span style="color: red;margin-left: 20px;">*</span>
                                </td>
                                <td>
                                    <select id="ddlcond" class="col-xs-9">
                                     </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Description</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="txtdescrpt" type="text" autocomplete="off" maxlength="30" />
                                </td>
                            </tr>
                        </table>
                    </div>
                   <div class="col-md-6">
                       <h4>Purchase Information</h4>
                       <br />
                        <table id="purchseinfo" style="border-collapse: collapse; margin-top: 9px;">
                            <tr>
                                <td align="left" style="width: 150px;">
                                    <label>Vendor Name</label>
                                    <span style="color: red;margin-left: 30px;">*</span>
                                </td>
                                <td>
                                    <select id="ddlvend" class="col-xs-9">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Purchase Date</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="purdt" type="date" autocomplete="off" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Invoice Number</label>
                                    <span style="color: red;margin-left: 40px;">*</span>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="invnum" type="text" autocomplete="off" maxlength="30" required />
                                </td>
                            </tr>
                             <tr>
                                 <td align="left" >
                                     <label>Invoice Date</label>
                                     <span style="color: red;margin-left: 30px;">*</span>
                                 </td>
                                 <td>
                                     <input class="col-xs-9" id="invdt" type="date" autocomplete="off" />
                                 </td>
                             </tr>
                            <tr>
                                <td align="left" >
                                    <label>Warranty StartDate</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="warstdt" type="date" autocomplete="off" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Warranty EndDate</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="warendt" type="date" autocomplete="off" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Purchase Price</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="purval" type="number" autocomplete="off" maxlength="30" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label>Purchase Type</label>
                                </td>
                                <td>
                                    <select id="ddlpurtyp" class="col-xs-9">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label>Invoice Upload</label>
                                </td>
                                <td>
                                     <asp:FileUpload ID="FlUploadcsv" runat="server" style="padding-left:20px;width:210px"  />
                                </td>
                            </tr>
                        </table>
                       <br />
                       <h4>Financial Information</h4>
                       <br />
                       <table id="finaninfo" style="border-collapse: collapse; margin-top: 9px;">
                            <tr>
                                <td align="left" style="width: 150px;">
                                    <label>Capitalization Price</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="capval" type="number" autocomplete="off" maxlength="30" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Capitalization Date</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="capdate" type="date" autocomplete="off" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>Value Depreciates?</label>
                                </td>
                                <td>
                                    <input type="radio" name="vd" id="fixed" value="0" checked="true" />Yes
                                    <input type="radio" name="vd" id="" value="0" />No
                                </td>
                            </tr>
                            <tr class="hid">
                                <td align="left" >
                                    <label>Annual Depreciation(%)</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="deprate" type="number" autocomplete="off" maxlength="30" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <label>End Of Life</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="endat" type="date" autocomplete="off" />
                                </td>
                            </tr>
                       </table>
                   </div>
                   <br />
               </div>            
         <br />
        </form>
          </body>
     </html>
</asp:Content>

