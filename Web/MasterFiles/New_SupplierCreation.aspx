<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="New_SupplierCreation.aspx.cs" Inherits="MasterFiles_New_SupplierCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>
     <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title></title>
        <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
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
                font-size: 11px;
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
                width: 86%;
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
                height: 700px;
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

            /*.options-container {
                display: flex;
                align-items: center;
                justify-content: space-between;
            }*/
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
                /* box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);*/
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
                display: flex;
                !important align-items: center!important;
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
                left: 55%;
                top: 0;
                width: 45%;
                height: 100%;
                overflow: auto;
                background-color: rgb(0 0 0 / 66%);
                opacity: 1.8;
            }

            .modal-content {
                margin: auto;
                display: block;
                width: 100%;
                max-width: 100%;
            }

            .close {
                color: white;
                float: right;
                font-size: 28px;
                font-weight: bold;
                padding: 10px;
                cursor: pointer;
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

            .bootstrap-select .dropdown-menu {
                /* min-width: 100%; */
                overflow: unset;
            }
            /* Hide the increment and decrement buttons */
            input[type="number"]::-webkit-inner-spin-button,
            input[type="number"]::-webkit-outer-spin-button {
                -webkit-appearance: none;
                margin: 0;
            }

            /* Firefox specific styles */
            input[type="number"] {
                -moz-appearance: textfield;
            }
        </style>
    </head>
    <body>
       <div class="spinnner_div" style="display: none; width: 100%">
            <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
                <div class="rect1" style="background: #1a60d3;"></div>
                <div class="rect2" style="background: #DB4437;"></div>
                <div class="rect3" style="background: #F4B400;"></div>
                <div class="rect4" style="background: #0F9D58;"></div>
                <div class="rect5" style="background: orangered;"></div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 sub-header">
                SuperStockist Creation
                <button style="float: right;" type="button" id="btnsubmit" class="btn btn-primary">Submit</button>
                
            </div>
             <div class="col-lg-12 ">
                 <span id='status' style="float:right;margin-left:20px"></span>
                 </div>
        </div>
     <!-- Modal -->
<div id="rejectModal" class="modal" tabindex="-1" role="dialog">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Reject Confirmation</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <label for="remarks">Remarks:</label>
        <textarea id="remarks" class="form-control" rows="3"></textarea>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-danger" id="confirmReject">Confirm Reject</button>
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
      </div>
    </div>
  </div>
</div>
  <div id="form-container" style="z-index: 10000;  padding: 10px;">
    <span>Field Officer</span>
  <div class="sort-container" style="display:none;">
       <label for="sort-by">Sort By:</label>
  <select id="hyrw" style="margin-right:14px;">
     <option value="All">All </option>
    <option value="des">Designation</option>
    <option value="hie">hierarchy </option>
   
  </select >
  <label style="margin-left:14px;" id="hry2"  style="display:none;" for="sort-by">Sort By:</label>
      <div id="sel1" style="display:none;">
  <select id="hyr" >
    <option value="">Select </option>
   
  </select>
          </div>
        <div id="sel2" style="display:none;">
      <select id="hyrsecond">
    <option value="">Select </option>
   
  </select>
      </div>
  
</div>
      <div class="options-container">
      <div class="selected-options-container"></div>
         <%-- <input type="text" id="search">--%>
         <%-- <div class="form-group has-search" style="margin-bottom: 0px;width: 100%;padding:0;">
    <span class="fa fa-search form-control-feedback"></span>
    <input type="text" class="form-control" id="search" placeholder="Search">
  </div>--%>
           <div class="search-container">
      <i class="search-icon">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          viewBox="0 0 24 24"
        >
          <path
            fill="gray"
            d="M15.5 14h-.79l-.28-.27C15.41 12.59 16 11.11 16 9.5 16 5.91 13.09 3 9.5 3S3 5.91 3 9.5 5.91 16 9.5 16c1.61 0 3.09-.59 4.23-1.57l.27.28v.79l5 4.99L20.49 19l-4.99-5zm-6 0C7.01 14 5 11.99 5 9.5S7.01 5 9.5 5 14 7.01 14 9.5 11.99 14 9.5 14z"
          />
        </svg>
      </i>
      <input type="text" class="search-input" id="search" placeholder="Search..." />
    </div>
  <div class="checkbox-container">
    
    <label for="option-1"><input type="checkbox" id="option-1" value="Option 1">Option 1</label>

   
    <label for="option-2"> <input type="checkbox" id="option-2" value="Option 2">Option 2</label>

    <input type="checkbox" id="option-3" value="Option 3">
    <label for="option-3">Option 3</label>

    <input type="checkbox" id="option-4" value="Option 4">
    <label for="option-4">Option 4</label>

       <input type="checkbox" id="option-5" value="Option 1">
    <label for="option-5">Option 1</label>

    <input type="checkbox" id="option-6" value="Option 2">
    <label for="option-6">Option 2</label>

    <input type="checkbox" id="option-7" value="Option 3">
    <label for="option-7">Option 3</label>

    <input type="checkbox" id="option-8" value="Option 4">
    <label for="option-8">Option 4</label>
  </div>

  
</div>
      <input type="button" value="Close" id="cb" style="width: 99px;float: right;background:#f91111ad;margin-top:10px;font-size:16px;color:white">
      <input type="submit" value="Submit" id="sb" style="width: 99px;float: right;">
   
  </div>
        <form style="background: #ffffff; box-shadow: 0px 3px 12px rgba(0, 0, 0, 0.25); border-radius: 8px; margin-top: 5px;margin-bottom:10px;padding-bottom:20px;" runat="server">
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <div class="container">
                   </div>
            <!-- Modal -->
<div id="imageModal" class="modal">
  <span class="close">&times;</span>
  <img class="modal-content" id="zoomedImg">
</div>
                <div class="col-md-6">
                    <table id="upltbl" style="border-collapse: collapse; margin-top: 9px;">
                       
                        <tr>
                            <td align="left" ">
                                <img class="imgBlink" style="height:18px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAACWklEQVR4nMWXPWzTUBDHX9X4XgoVCAYWBhbExwILAoEYCpQRCSRAICQWBsSEBBtLOiCkjoXkLm5hqhLfncqHimCtYEJMrAiEBAsMDAxAF2iQm8axY7tJnLj8pbfE9v3uzpe7szGDCGvbzJxuNxuiRmPEop60xDWLsmzJm8iVN0a6E1DuAMpHS9JonxC4tFRwkC8B6U1b1t3Zaa7rQIXPWpLnFvlPFBgHW5Rn7d/5d4H0cF88eODtsSjTQPw1BkJ5B8iznWBA3Rt3ims9RLe4qUh8FVBeWeSVKIx/ADIVqvVDq5GRNxEDV7z9cSdZunItyVTHQytA8tp3xncqem8c3Ew1PwmnuljVo/2BUab9dKffmwz2iyvk+N2u0E6w6aIwGJBvW+TJ4LSzNpUr2Kaf/wMG0vMbm2rkSadSP9ATdLCIu7RM13Uc4iup3QyQSwG40RgZJOLokRfrdrNVj1rgLhOnx+JKORztZkB6LrhY4RO5gZE5YmwzPt4RtEqUe0NM9WI41WNu/UiCQXnb/CvIF6M6OpTiUh11SC5CVW+ljkpLfL1l0CG5nDVih/ig6UtlHQeUb2sGP5mZlzZzA0G+0BfbhqIGlPu5t8xApaUCoLxpG9BrJs8hEVaxvLALiL+vGfhrUW/kNhY7ZUlPN7fIILKHrYUgdREgeRqa6cvFinfMZJGteqcsys+QsfdQ5TNDXX3S5K8vgPw5OvL4QwzsLuxLKDDPDKSZ+S1A/Ci5elN2LuRfiV0qi/yqjVZ8ykK/XpcyA6jo6nEgnm++/5w/YRJV1nHj6tYsz/4Dvz4OkwSyap4AAAAASUVORK5CYII=">
                                <label style="width:61%">Supplier Name</label>
                                <span style="color: red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-9" id="txtStockist_Name" type="text" autocomplete="off" required />
                                <input class="col-xs-9" id="Txt_stock_Code" type="text" autocomplete="off" required style="display: none;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" >
                                <img class="imgBlink" style="height:18px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAACnElEQVR4nNVXzWsTURDfg5lJUHr1IjQK9aAnPw7+AQp+0oNiPdiDVL1IQdCrl161h2Ays6mHIpXszGPTVg8BRf0LPHoRC4KCoK1tQRH1osxbE6PdNpEmKw4MvI+Z37zM2/ebSRD8T4IcFZG07pWjYnaBSetA+hhYntg4u8CscxYUWJ8iyWyGgaMisjw3zZfjwSBLAZaqaU/AcuQOALszQamBPQtcaqBhGnY6UCinkOQrsLxHlvud8JD1kmlHO9IHHtOwK+5EGtAtYH2YIzlnRsGkKwSblUlXMKxc6EaA5BGS3FxjY6lAklVk/Q4sS0D6CjgaXmNYdtuApQwkb00LU/UdaTHNN8GQJcNElpUc1faln7A0M4BTbmcwPZ0H0hvI8hlZG8DxUAuQ9C6SvsCKXLAMIeknILn6K2A8ZD7maxiGhRTvMuy/fDY6hyxfMJTxZE1W2u8qX9HzwPra74Uy7m29Tw8YDUN3NE9uNPnF8sbm7XvA8s4fgtxo+15PBUgXgaKTrTlHw5aFoN+CJLPIGrXNNRO+zpVre/09khxGdkdsDOz29D1wkHzxC0hy0dTGttbXmEjusn+/LC+33qltN7WxrXXDYl0L3I52A8k1GyO7MWT5iKRXfuPyUgNtze+xG/N+rNfNt/tIpZkB/9iN6lgm/P2R3vNgRnmsy8jyLF112Wz8IUlqyXuWiYQ24/UJZEtYO/gHZS4ARcea+wWSQ5bqjdRsWtliOf7zW/jgKZNkNVeV/esXidCNdFMk/DV0SmezSLCeXbdIGCn40kW6iKTzGwImqWfTTnZWYpOyqN9Sy2KrESA5HTgHPW8Eqilp/uetT7dipbPZ7GXdV88n7a311Vm2t6T1Vl/NGmcWOF+OB5O/Lxpn3ldvVn4AVGCnEP02+K0AAAAASUVORK5CYII=">
                                <label>ERP Code</label>
                                <span style="color: red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-9" id="Txt_ERP_Code" type="text" autocomplete="off" required />
                                <input class="col-xs-9" id="Txt_stockCode" type="text" autocomplete="off" value="0" required style="display: none;" />
                            </td>
                        </tr>
                         <tr>
                            <td align="left">
                                <img class="imgBlink" style="height:18px;padding-right: 8px" style="height:25px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAADU0lEQVR4nOVXT6iUVRT//POdM/NeYhqpaH+kZS7MlZim4EIlRDCocFFkSZAtRHIhLhxIJF1ImDPnzOd7LhKc75zTs+KZ4KKykNoUirYqjVDRkrAWlv3R18i5M743T755M2+cNnrgwJ1zf/f87v3On3snijqVJImD/t/SU7RZwLYJWD8H0l+Q5D9XHwcb2ybHdI8xGexB1h1I+geS/AOsx4BtO5JscPWx23wuYFjejvZY/q448/sPP4Is3yLLdWTZFfXZ9KbgPpuOpLuR9C8k/SbPNqcj0l46PANIfwKS83HRnmp3XUyVBcB6wdc+sNceHh+r2SQgOQEsP+cSe2y8m84VBx4PsSf90n21vRBJX/XEyVH6TKZjlsVA+p5rrpQ+nYkp2bKQgGyvtMdaO+1FJNGs6Zj1BWQdQpIfgrIOAcnzmQdgHfBQtXXqXFmWIms1V7ZFWfPA8iOSfBAVChNda871XKYvlsXBV2JLWhIjyzsen+D4TjGbhCw3kOyNBvxGJP0381SFwkRguYIsO1sTk6g3hGbzQPI1sp70pHNFllPA+lVzvB5H1rQlMbB8MRYwZpkPJJf9E7r62G1ND8Kaus+WxEg6iKRHxwS9+9GDUE6fA7a1UWJTx/TH8gmyHmlJDCyEJGeaAvo/nhJaZb2ckO01tzUn1u+AtNxeDbMOZbU8IFuDrL8hyc3GckKSq0Dp6jvx+X2V2aEfsLzckjiiQ9OQ5W8geWv0hmx5IGQ9gpzOHTlROrdmkxveNEZtlHWL26fs+/Ch1sS1THwfSH9tXOC1CyyfZZZNaDr6qdf0sC2xqe4DSPrbInXxexVZfg/ZXa1OCMaDB3sjM2i6yOeSwZ4wrlYn+EaR5dq4b6m4bC/W2yEPk7cjtSazNzwSPOs7ESzJ+kDOeixXSh9tiaeBJ+qPgptI+mZHpLcFy7YKWC/VE67ff48qH48ly7PAcqD+QrmEJCujrkjiiWJb/YK/3bE8fkjy50gH03NAum04zt0WYHsyR/aSlxuwbo5Z18XFyrzonpC4WJmHZK93ov5lOiYG0tJITMenvrarX+H+kHxSWYik3/t7q0HPYildEQD+mGc97RjHdo24t68yM/yNYdnVoDv9/RwAheOTvYYd49h2nN4C3rALlRQaigIAAAAASUVORK5CYII=">
                                <label>UserName</label>
                                <%--<span style="color: red;">*</span>--%>
                            </td>
                            <td>
                                <div class="row" style="height:0px;">
                                    <div class="col-lg-6 col-md-5 col-sm-12">
                                        <div class="input-group input-group-sm mb-5" style="display: flex; width: 185PX;">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text" style="padding: 5px 2px 5px 5px; background: #868383; color: white; border-radius: 4px 0px 0px 4px; width: 69PX; height: 30px;" id="fixUsrn">SWDF</div>
                                            </div>
                                            <input type="text" class="form-control" id="sfusrname" style="border-radius: 0px 4px 4px 0px !important;" aria-label="Small" aria-describedby="inputGroup-sizing-sm">
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-5 col-sm-12" style="padding-left: 55px" id="user">
                                        <input style="height:15px;padding-right: 8px" type="checkbox" id="usrauto" name="userauto" value="Yes" style="width: auto; margin: 8px 0px 8px 8px;" />
                                        <span style="font-size: 13px;">Auto-generate</span>
                                    </div>
                                </div>


                            </td>
                        </tr>
                         <tr>
                            <td align="left" width="140px">
                                <img class="imgBlink" style="height:18px;padding-right: 8px;" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABkAAAAZCAYAAADE6YVjAAAACXBIWXMAAAsTAAALEwEAmpwYAAADQUlEQVR4nK1VXYhUdRS/6+49504t2YKRqG994YOlRhj59dpL+BEK9mEaIpW+ZIL1EFNK4IOrjDtzzowii7U795xDudCDDz4kgglRKmT1lKVFtLSmKEQPCRPnP87sjjt3V3f3wHCZ3//c/++c8zv3nCi6R3ugaHOxJFuBdA+ybXnwaPXRaCYNWD4A0hFkGUSW/f70/044MwRk7yPpt7m+6ryxeI5tPrJ8B6y7p0XgJQGWaw2CuCJLoWy7YqouaRD5eXfBHpkyiWvgpQkEVF0CLH8iyUEgHY6Ltjj4sKYJ6Rv3fTn0pU8Cye/IWnMNAkbyHpL0houdqGy76iSuUfC7AaV04b1nQPY2sByLWV5F1qFmJqTDyHooZMLyzB3CEzHZK1jWt5D1ApAeRpJvQqOQfhkVPnuoLYlfiCwXXRMkuRXRYE/Ai7YYWN9tEEQVm42sN0M712odQPqhnyel9AXXCUmqHnBrBizvIOl5ZL0EpGcbNUeWT9pmzPoRkmhmRbzlSXeMRu+dQzqcI3k+rtii7srgnOBYSR9D1uu5I18sGPdxkvwN/PkT7QgcB9Jfc5XqsjrSazlk+SEp6WsZEe0Hlk9bL9GjyHqgrT9Jb9CP5OMon59Vf4H0MJAOZKUdFa0bWC5DOV0f/MvykkeZJaqXOinLqibg5QHW36JDQw9nkkRRlLAsB5a/PH2PMqF0ZZavd2aL4AnZ68D6B7CtG+fd35+4cI26IsteZPkPWfdNFFA9UznXAnaV5Dkg/Tlm3TQWR7ad/qF5zwfArDNh2ezPiUii/OkuYP0aWQotuJcNSX5sweqlGZlQrwyLyZ71ueaEo6hZJ5L+G5lBNAOGJCfargJvYyB5eboESclWA8kvrum4w4Y2QLJhygz5/CzfMzHrxkwfJH3TW3CqHDHrptBZtVpHppO3tA+2TIdarePuLTnWgPWrSUuORXvcl1NWukA6gCT/hHHO6Zq7Iw4jiKQ/msziUvVpILnqve4zC8m2A9lTXgok+d7HCZRkbZjY/ivbi+FF71CWggcwKUkwGuxJKrYCSbYB6XFguYIktx1r+vgOKafrkfUnID3jWgDJqcb+mZoVTmJb3KzTy+sbsjl1x9j/qpyni3sNIacAAAAASUVORK5CYII=">
                                <label>State</label>
                                <span style="color: red;">*</span>
                            </td>

                            <td align="left" >
                                <select id="ddlstate" style="width: 62% !important;display:none!important">
                                </select>

                            </td>
                        </tr>
                         <tr>
                            <td align="left" >
                                <img class="imgBlink" style="height:18px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAB60lEQVR4nN1VS2sUQRDuy1ZvQoyoEUwMePCBR8FTDrlo8CoejeYf5O4efP0BwbCbqkwCAZE4VeXEHBRBvPggOS35Fa6XIMaLDyQq3VNzSDaJO5sExIKCZqbm+6q+r6fbuf8++pKFAU9y3xM3PfJ6TOImIN87VH92bE/gQHotB5Tf2ybyOlB6tXtwko0cjF/4mfSyS/RwSI885kleGtFGaZK+KT3uSb4EAEC+vUsTd62Bz6XkyjWP3T2HJDsPqLU2cJRbUE/PFZMEsjIEq7nGeskjPw3rCvGN4n1YmwdicoUpmp0TmLFu6nE/IH8M6576kyEgWQbk9z2zi8Mm34foiRneMQGgrEUCXDgCJK1IQHoygAPJu00Ec3o0X8taCQJesa7GPInuIhH76fRK7oEsl5ngjn00F4wMhrbXaA0oOwsk83/bbW3hG3rGI//yxN+DNDvVRamQf8TaJD3dMUEkIVkyGR7sWIP80LZz5spGBfWiTfF1u+6CPB7lW/iTKw29UJoggqA8Mi9etRPIa3s377qN3mRxMBwD+S6S68Xz6rTcLI6I3oae6JoggqFOFGDVRnbKjP0USVHH3X6EJ8lyOfgNoLw1Y9Xt56UDdmSY7q09XzZbozqjI3HPE/+sYjrqDiI86WTIAwH/Z+MPln9KDUDNdz8AAAAASUVORK5CYII=">
                                <label>Gst No</label>
                                <%--<span style="color: red;">*</span>--%>
                            </td>

                            <td align="left" >
                                <input class="col-xs-9" id="txtGst" type="text" autocomplete="off" required />
                            </td>
                        </tr>
                   
                       <tr>
                            <td align="left" >
                                <img class="imgBlink" style="height:18px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAB60lEQVR4nN1VS2sUQRDuy1ZvQoyoEUwMePCBR8FTDrlo8CoejeYf5O4efP0BwbCbqkwCAZE4VeXEHBRBvPggOS35Fa6XIMaLDyQq3VNzSDaJO5sExIKCZqbm+6q+r6fbuf8++pKFAU9y3xM3PfJ6TOImIN87VH92bE/gQHotB5Tf2ybyOlB6tXtwko0cjF/4mfSyS/RwSI885kleGtFGaZK+KT3uSb4EAEC+vUsTd62Bz6XkyjWP3T2HJDsPqLU2cJRbUE/PFZMEsjIEq7nGeskjPw3rCvGN4n1YmwdicoUpmp0TmLFu6nE/IH8M6576kyEgWQbk9z2zi8Mm34foiRneMQGgrEUCXDgCJK1IQHoygAPJu00Ec3o0X8taCQJesa7GPInuIhH76fRK7oEsl5ngjn00F4wMhrbXaA0oOwsk83/bbW3hG3rGI//yxN+DNDvVRamQf8TaJD3dMUEkIVkyGR7sWIP80LZz5spGBfWiTfF1u+6CPB7lW/iTKw29UJoggqA8Mi9etRPIa3s377qN3mRxMBwD+S6S68Xz6rTcLI6I3oae6JoggqFOFGDVRnbKjP0USVHH3X6EJ8lyOfgNoLw1Y9Xt56UDdmSY7q09XzZbozqjI3HPE/+sYjrqDiI86WTIAwH/Z+MPln9KDUDNdz8AAAAASUVORK5CYII=">
                                <label>Type</label>
                                <%--<span style="color: red;">*</span>--%>
                            </td>

                            <td align="left" >
                                <select id="ddltype" style="width: 62% !important;display:none!important">
								<option value="-1">Select Type</option>
								<option value='Stoctkist'>Stoctkist</option>
								<option value='Warehouse'>Warehouse</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-6">
                    <table>
                         <tr>
                            <td align="left" >
                                <img class="imgBlink" style="height:18px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAAC7ElEQVR4nL2XSWgUURBA22WqZpJcFHHPwRU8qCfRg+hBRFBcCB4UcxD04IIL4sGDGr15EqMx8VdNMuCCoZ2qyQQVgwsIMScPHhTFFbezYgwegkaqM9HM0mSSdFJQ8Lt+db/+v35XVXveCCVBsirh2lZ64yKNt6eA0yNI2oosvYGStgLLYZsbEyZweiOyfkfWvpJK8g0pvSFSKHJ2IbL0hEL/azc2p+dHBgbWy2VAcyvX+sjASPKibDDr88jAwPIBSJpjTnbEXGZplWuf5tXfRVMbx/jWshjpTvMBlveRgT3fByBdjE4O2MORpB1IO037x9KMrPuhKb3IfCPjImu2vPjKH3uR8QdzoNnIwIkmfw6S3gDSR0DSiCx748n0GlMbmy2YY72eSLbNjgyMlNntNfhVQzo2+FWBb2Rglh5b3VB+cadrzTcyMLDeB5YnHvtTQ53YnwokXUDSERm4krMzgPVznLU2zAdJDgLrx8pGmR4Z2ARJzwPrPS9EgPQxsp7zohak7Dwk+WnZq3DObFYg4i2Z6sH2yU5WxJ2uHjUcWFLA4krYHZC25Blde4WFJ0gqrDcTJHNHA3ah4AI7sJ7OTyzSgyxnvFQqPjxqX98EYH2ILBcKp8xmMfbq6ibadYWTWUFtLpHZgPQdJGVTWUyLk31SYYXebEjyYyBd5opGaFoF1i/htFQqblkIWZ4OLgJAurXQFVhrcnPHYs2yHFl/D1FMDpVkAsl2IPla+kbpBdaLyLLeFEguBTaSN/3lUx4UxZZ0HyYz64DkWvCCrEuK40h6ZRiVaPD21QBnNhfZk3K8YFGfYiS78mPJWjsiKGmn51wMWV8Wb2tmzz+oky22A5YJC7e4a4SrPVFqtTl9jU4XWJuEJK+Q5GxRbHMnc9hgJL1jDwbSkyXbYNJf1jACy1vvakdliUOldQPJYNiazGwLwtWSqe7/08iDdwPJ0YqUPzP8M4pIgu6E9Fl/KOTUmAPzxPcn2YEKWuEC+Qs+8PlSFvx6VwAAAABJRU5ErkJggg==">
                                <label style="width:61%">Contact Person</label>
                                <span style="color: red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-9" id="txtStockist_ContactPerson" type="text" autocomplete="off" required />
                            </td>
                        </tr>
                         <tr>
                            <td align="left" width="140px">
                                <img class="imgBlink" style="height: 18px; padding-right:13px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAACOUlEQVR4nMWXT2sUQRDF28BWrRrJQT+FIl4FPQriQc8KegiIX0Jh/fMR3KmaubiyYPZ1H0S8x4MX8e4poCAejKKCWd1ssupKd0YlszvZSezRgjr/qFevXs8YTt0iC/qsdlzaYtcaai+ZmMWCrztC8ybFp/8CZrXjQ3cfHo4GJsGbSmDB9/2CkzHBzyuCB01xV+KB1QkrRhXg6yy4FQ3M4q6x2tl7FoxZ4aKBG4oTLPhR0WAvooHNeLyPBB8rnZTgfTyw8XtGd/aeMSS196KCWXCWxZaDxa6Tuiem02lGBZtWa47ErpZOmmA5PjQvSt0NP1nByZuU2Ke1QUNlboHVfink85AVt03d1Uh6N720BalH/uTqJXe7B0nxltX+uWvBNxas1Cu38dntLrAWHY4hCx55E9YLT22P1W6TnBQbpGjXCjZtN0+CV0Hm7ee1GeBlk3c6TVJ3nxTvfLyyWOfN2UzsZf+kzmcPjsxkN9pLx1hdv5jjfvIge3HnHppgeYo5/ZoG/j3/9SXTEFzcEd7M3Gl/y9vMtmW4IYtd+e32UmjZZ5T9PHNykt65AJ98wUZ5vt/xIVMVynnPBIfJFaeC7FMeEtIg48ZuoFwVHCZXd5QULydidY9tdu32FEtboVLYe63gvCjF+ZBw3mT/Ehwqe3yAUtdiRX8v8pu/rswtkLrrJFj15qOJuK0LbPJqteZYcIYEHRJ8COcnGNCULxsSrMUDF6qRueMsuEpJr01qn5H0XoffJd+pW/wJnltIyTudX+UAAAAASUVORK5CYII=">
                                <label>Mobile</label>
                                <span style="color: red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfMobile" type="number" autocomplete="off" required />
                            </td>
                        </tr>
                          <tr>
                            <td align="left" width="140px">
                                <img class="imgBlink" style="height:18px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAAC0UlEQVR4nO1WX4hMYRS/tPecGbuplfBAPFj/H7Akbx6UF6HwIPGOhJJ9nFUeRBty55wdW0bLzj3fZ1bZRXnQFk9EWWS3kEQ2nrywyp/RuXfW+jMzd2b38sKpU9/M+d3z++655zvfz3H+WyVL9dUlWHYimR5k8zhwMj0Jsjs05vwJA843IcsjZPMF2NxEkpPqQHJL/0OSB4qJlTSR9mcByxCSDLiUW/Zr3M3IcmQZVEzyTPfM2IiRzFUgeVFP3dPKYeo7ctOB5CWy6Y2FFNguQjYFl822KKzLsl2xkMkviIN4H5IZdrLZRCS4zSaR5SO2y95xEyPLMWB5VgN+EEmOxEBsTmhTVYvX5pvk2Rl/nTg2w3+DOJWaCGTnu2SbkSQHZJ7rulaH0/48zVUdaYedouNPz2NM3q85I3mBzUE9t5CWTUiydjwe5jDDmjOSWA8/snxClt1AsnU8rjmCXGT2RJe6s7MeWa7EUmaSz8HsbrPJ6r5zUHIZQpajDnU1jtlrIRwxIHmNJIedsZq1ACSbkU0rkGmpa8+tqOo5ZPMQWLyxcLqeXarzvUT5rX7KKOJeINNXLh40ENstJe9lljcVvvvFCOKwI8sNfN1YqUtf+yIkMPeB/PUNma6pgYIh2Y8k7zRWsez6gALLlRtJLqmX2NAd1WaOZxsSXn62vj2w3NBYMAlVs5FpqfjWwHIoFHf+xt9j+Sb07NwSxP3I5pquk5ncqkCRsLwNRid1NSLJeyBJVSRWMLJc1ukTSNgqDFg6AzIyaefU+cmJdrsa2Z8DaX8hstwNY3ZDdKZsNgFkLoQ7N9eRZJ0ele9xawHZX+OSLNGfdWlZWRwcBWT5gCS3Q/0tX4vNNVCTBtcODmRN2DjDQOYpsDzRdbGc50awSHbXKPmoA8kr18strpr0R1NpEwpAOY4kbcDmgDaNUyhM+Blnm4HNWSRzryj6W6u6pf60fQMupUVzzxNbowAAAABJRU5ErkJggg==">
                                <label>Password</label>
                                <%--<span style="color: red;">*</span>--%>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfpassword" type="password" autocomplete="off" required />
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="left" width="140px">
                                <img class="imgBlink" style="height:18px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAAC0klEQVR4nO1VO2gUURSdxOx9s/k2/lDQToigTQp/4K/UgIUfTKeiln6idpJNpTYpQjL37ph0wZ17nwmEJK1GK0VTiL1WKqiFiQpGLFbu2924m51kXcwGhFy4MPPmvHPmnTnvjeet1VotUQblggn4vLfaBcShds1XB8gnS4SRB7RLX8aeUuyKCSdIzhjknwmSroXB0La5/oPpUgwgn14xYa0E8lmDckev/bTdZ0h6tZPIe3XMEN9VjFerMk6Q5w3KhCGZ1Gsg2+PVsny3Up4vrHKpsX8uQ3LJpVdDFNq2vL2TMbhJfaYYFzriUOeugjBPAXKqRBj5YnVqfTYJA9EOLzXdsPiR2qm2qr2FMZ/4gKY5GWb2lHGlphscV59NLqsJJNddcEiyQPwJiI+VY2xPHqPBmspvodtluMAeB5TPyuUCiHwtVtQP7CGD8gMw6vSGx1uA5KYhmWsaymyKX7n0qr1xK20ctJsN8ldI227HhVGncvtpPlgmbIjvGeSHJW+N/FZPoxLryO5MoO0obh0r/jQ6B4jflPLLqO71cpvR3jIkL7xsts4N5MI0Zyg67J6nbbdB+Z6zLq75W8FOg/aoQZ71+kdaHVc2W2dQXqqLZcJNOLYRiD/qwQDINwzxDJA886xd5/5ExF/ceR0XFA2kO6d51pA955xBee7ElAtlQrmb++0GL678wdHtgHwfUKbd8Tg83pJbAb8GlCuxk4pdI3vVoLxyN/0jrWqtcun28kO7zau2DPGv2O2yqBSj2L8mTgSZ3YDytLCVFlq/KcqYXmuIKvKg7cjPGyvPA88D8ZNEaHfl0LnT5gOQDOnBUJxWE/ARIHlcrTAQP9JQFnP5QbQfkIeB5L37nQJFJwD5nZdK1cfaR3ZrtcLJgcyWWEAqVa/CqqnRv6wJXo6wWuHluXhGNb3m8MF6CKL2SoReONFYSVgxlV4QgqhdNStyrdV/X78B2xHFDPHzUzoAAAAASUVORK5CYII=">
                                <label>Field Officer</label>
                                <span style="color: red;">*</span>
                            </td>
                            <td>
                                <%--<select id="ddlField" style="width: 62% !important;" multiple>
                                </select>--%>
                                 <input class="col-xs-8" id="tt" type="text" disabled autocomplete="off" required />
                                <button type="button" id="toggleform" style="height: 30px;width: 69px;" class="btn btn-primary"> Edit</button>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="140px">
                                <img class="imgBlink" style="height:18px;padding-right: 8px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAADVUlEQVR4nO2WS2gUQRCGR81UbRLfEZ8nBfUgouhBEBG8KIIKgjdRxBeKT1CUgLgKPqKXqEmqMgkYUZOtbpJoFHM06EFRBBVEQSUqgkGQ+MD4iCGR3pnZzM7OxmTNSSwolumdnq/773q0Zf03z4ASy61s5jh2YXXdBOtsM1qDavGWPGC5YGk9zB8qcBomIctJZPUUSbqRVY9xIGkFUueA66cHP2E7Mg9JnfqrdQCp3Ujqqw+LdFKdSOpEarHOtQK7vG5WzlAkORMFApbX0QuQxqBSuUEr1bYMIMkbJL0dSB2MsaxHlocRu89d4uFO7Tgk+RTYSQeSHAPWe4DlvTf205wvktoBLG2Bd7tylhlYigO7rMl36hYAy0VkeRDhTTFHL0KW0wbqzanK+Gg8PrQfYHXXW303sDqPJMuApRxYnCjHClmS/CX54sVAW/B7Nun5SOpGBghZb0DWO3uf5WMomA4gy7esUV0pu8JjVrUemwZxHDtzi6VXRo8oayxyJWnJi4jiIPixOWsTwX2BoSwx44/Shi2ct+lgvSEZWCzFQOpVNnAhNYy3BmrA6nZWsA8hUUDqVhQYSN4OGGrM5GroQ4eQ1EYkvTUps5uvV6FSVpoxJNk8OLlcUxMDUi/7LJPZvd2XGav0VFNowrW8T3NTQDoGBCXpAkqsMPOB9MxUrJD6bpfrudEkqh1j9fQMCQ4B69V+UeiPm4aSmkuyNz1O9OEMJpDEvReuZ/zHeo2J4j/stNuAgvNiFYmFwRaKFYmlUeA77gvyI6q7mItBVtldRbZEiWgqnulwQHpVpMqxSlmMrJpNjqZNrNJTTT0GVjdjpNchq8+h6O10u5g0mfdMzbYGw5DkaLAl2ixrewNGutw06m2NQOqyPzcZTI4e1S9QQbmemJI6Hh9qYMDyAkjeefBWZL0JWX4ByT4kuecfEbKUmFhJLZrVEXMF6hfYtD6TRt5ul2VUKBdSYrPMMeeaft7JxtJuVLD+xtAAWT6YSx6wqkaWJ95CniX7MKlH3vNzZDmejAVXmfs5Q0eUNRZ58j1xy6Tab84vOp2kye/Hfh+3HT0795slp9IlJHWU652hsdKcwIXUMB6pfppxc3nPL6ub7D9HuXX20sjgcz7rKTmBrX/VfgMe8GJCC0s1WwAAAABJRU5ErkJggg==">
                                <label>Address</label>
                                <%--<span style="color: red;">*</span>--%>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfAddress" type="text" autocomplete="off" required />
                            </td>
                        </tr>
                       
                        
                    </table>
                </div>
              <%-- <div class="col-md-9">
                    <h3 align="center" style="font-weight: normal; font-family: 'Open Sans','arial';"><img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAAEjklEQVR4nLWXbUxbVRjH79x6byktbRmwoUYXE18XN6NLnF/URN18idGYbGYzvoQZ5owzftA4Ew3zq1EJjt7Xlm0S2ttzX9q6AWODrVlIkGzLlA0Z4lsW94ENlBYoxSHH/C8WobBWsXuSf/L0PL/n/5x72hwuDLPUqGvhLF33oHTZ3EEcT3ZB8zYCptDBieE3OFHdbX3Yv99u48kDUGYzqIEp+GCm5sQKhpDlSItE8s5NDbFxCLlVRw1MQeIzUmSJYRiPZLztlLQxh0iqXZLe/eHJXyiEHGuogcnuW1Kwovosx6sfc7y6uSIQHd936grdED6e2HG0d7K5/yo9fOEqRb5BPZ744tRlCgYsetD7vx+8mDcqPLIxJJ75jXYMUHqo7w9a03nREnKsoQYGLFOQqKm5wV6v3+pVzL7a7kEaPDdGVwWio+X+CCnzRzTkoZ5x+nn3IAUDFj1LHyh95WDqidOjGJ+U+6NjmyNdo+0DlD5hdiWdEtmZwZwS2bXJ7EqitinSNQoWPei1PP5r2AX1FU4gb61UIvK78R+s44TubGwbtknk3gxn8wXX3dXYNpypg0UPeuGx5Af3inq1Vzau3NgQG2/tn6IvtXyT8kiGbF0WlC7zyLrycuu3KdTAgEUPU6golc3OvZ0XacuFKboxHE96FXPQKxuXkWMNNTCFmUbIcqfUVIbjLFPMpNY7QY9+P20dqdmbtoQca/r5CQoGLHoyl82SghVDL7B8+H2YlSrmSFXb+T9dkj65Ltg+gl8ytL6pfQRrqIEBix70FuThi6Tgg17ZaGf3he5wyXrovfiPFEKONdTAMIWOYlwMtREPcptIXnRJegqyCeGtFlAb8RTu8pgTGMDx6uuzC9jE3xtBoDa7iesZLE/2QAU1dUhGJcerj+cSy6t+KB8Hr381lBNCa1ghfInjVY0TwuSa4tUeS7kZDV7W/Z0vWCH0HMuHDxbqqOEFz7yDbfXBtSyvbsnHcULoUSj/YHULPPMO5kTyJMeTaoYQNucGebLdJoS35TSrObGC48NVnBh6LCdnqyf3lfnNxP2hjmSRoO29JieEt952sPl3KNfpOHjtI3iVKWYC3otCdh95xClp6Y0kPrIn/tO0XQhPl+Lvala4BX2HQyRT21rOpiDkbkmryuZKZfNTeMALni5Rm7CL6sMLBntl49LOY330IRJPfHDyZ/pU9OvU6oZYkhXIPbNQXWOJWzZGXztyjm5vPpuGXj3SQ92ynmT8Udc/JxdcW9kQSz4d7U7BC57Vx76jXsX4dd7QYtHY7RDI1O1fto57FXNyzYHDE6sC0XR5IJJ2SXpr5mXdJWm8WzYmbzlwaKIyEEtDyLHmkvT6mc21cCWyHkMvPOAFT3hjhlMw3pyZSgiLI/adHrJe2LK1PtieYH3q83jyykAsIZwZXsBgbXUglmR9obtZH3kGPYt5+U4PUadI0owk2WaOWTI7vIo5WuGPJLJVImuDRbJxM96T3ZIxUK5EktkM1jyK3o//MMCWSPrgYl6YsVIx2uZ/yXWNJQzf5F2g7DfGxRhobqBnMWbO7+AvD25t8xEbfaoAAAAASUVORK5CYII=">Division</h3>
                </div>--%>
                <fieldset style="width: 93%;margin:auto;border-radius: 15px;padding-bottom:10px;border: 1px solid #d8d8d8 !important;">
                <legend style=" width: 130px;padding-left: 14px;margin-left: 28px;border:0">Sub Division</legend>
                <div class="col-md-11" id="division_div" >
                </div></fieldset>
         
        </form>
    </body>
    <script type="text/javascript">
        $(document).ready(function () {
            loadstate();
            loaddiv();
            getDivisname(0);
            loadfoname();
             $('#ddltype').selectpicker({
                liveSearch: true
            });




            var passwordInput = document.getElementById('sfpassword');

            passwordInput.addEventListener('input', function (event) {
                var input = event.target;
                var value = input.value;

                var sanitizedValue = value.replace(/\s/g, '');
                input.value = sanitizedValue;
            });


            // Function to handle checkbox change event
            //$(".checkbox-container  input[type='checkbox']").change(function () {
            $(document).on('change', '.sfcheck', function () {
                var option = $(this).val();

                var selectedOptionsContainer = $(".selected-options-container");

                if ($(this).is(":checked")) {
                    var nm = $(this).closest('label').text();
                    var option1 = nm.split("-")[0].trim();
                    // Append selected option to the selected options container
                    //selectedOptionsContainer.append(
                    //    '<div class="selected-option">' +
                    //    '<span>' + option + '</span>' +
                    //    '<span class="remove-option">Remove</span>' +
                    //    '</div>'
                    //);
                    selectedOptionsContainer.append(
                        '<div class="selected-option btn btn-primary" style="padding-top: 0px;padding-bottom: 2px;padding-left: 6px;font-size:12px;">' +
                        '<span id=' + option1 + '>' + option + '</span>' +
                        '<span class="remove-option">&times;</span>' +
                        '<span style="display:none;">' + option1 + '</span>' +
                        '</div>'
                    );

                } else {
                    // Remove the selected option from the selected options container
                    $(".selected-option").each(function () {
                        if ($(this).find("span:first").text() === option) {
                            $(this).remove();
                        }
                    });
                }
            });
            $(".selected-options-container").on("click", ".remove-option", function () {
                var option = $(this).parent().find("span:first").text();
                var checkbox = $(".checkbox-container input[value='" + option + "']");

                // Uncheck the checkbox
                checkbox.prop("checked", false);

                // Remove the selected option from the selected options container
                $(this).parent().remove();
            });
            $("#toggleform").click(function () {
                $("#form-container").slideToggle();
            });
            $("tr").on("click", "input", function () {
                $("img").removeClass("imgBlinks");
                $(this).closest("tr").find("img").addClass("imgBlinks");
                $(".lik").removeClass("imgBlinks");
            });
            $("tr").on("focus", "input", function () {
                $("img").removeClass("imgBlinks");
                $(this).closest("tr").find("img").addClass("imgBlinks");
                $(".lik").removeClass("imgBlinks");
            });

            $("tr").on("blur", "input", function () {
                $(this).closest("tr").find("img").removeClass("imgBlinks");
                $(".lik").removeClass("imgBlinks");
            });
            $('#hyrw').selectpicker({
                liveSearch: true
            });
            $('#ddlBank').selectpicker({
                liveSearch: true
            });
            $('#ddlAgreement').selectpicker({
                liveSearch: true
            });
            $('#ddlPayment').selectpicker({
                liveSearch: true
            });


       <%-- if (localStorage.getItem('new_Distributor_Master.aspx') === null) {
            var item = { div: '<%=Session["div_code"]%>', sfcode: '<%=SF_Code%>', state: '<%=STATE%>', hq: '<%=HQ%>', hqnm: '<%=HQNm%>' };

            namesArr = [];
            namesArr.push(item);
            window.localStorage.setItem('new_Distributor_Master.aspx', JSON.stringify(namesArr));
        }--%>
            var sfcode = '<%=stockist_code%>';
            var sfdept = '<%=sf_dept%>';
            if (sfcode != '') {
                loaddistval(sfcode);

            }
            $("#ddlstate").change(function () {
                var st = $('#ddlstate').val();
                // loadfoname(st);
            })
            $("#ddlregion").change(function () {
                var st = $('#ddlstate').val();
                if (st != '' && st != '0') {
                    var reg = $('#ddlregion').val();
                    loadoffice(reg);
                }
                else {
                    alert('select State ');
                }
            })
            $("#ddlsloffice").change(function () {
                var st = $('#ddlstate').val();
                var reg = $('#ddlregion').val();
                if (st != '' && st != '0') {
                    if (reg != '' && reg != '0') {
                        var off = $('#ddlsloffice').val();
                        loadroute(off);
                    }
                    else {
                        alert('select Region ');
                    }

                }
                else {
                    alert('select State ');
                }
            })

            $("#ddlTerritory").change(function () {
                var ter = $('#ddlTerritory').val();
                /// loadfoname(ter);
            })

            $('#ddlType').selectpicker({
                liveSearch: true
            });
            $("#sfAddress ,#Txt_ERP_Code ,#txtStockist_Name ,#txtStockist_ContactPerson ,#txtemail ,#sfDesignation ,#sfpassword").keypress(function (event) {
                if (event.which === 39) { // 39 is the code for a single quote
                    event.preventDefault();
                }
            });
            $("#hyr").on('change', function () {
                loadSEChyr($(this).val());
            });
            $("#hyrsecond").on('change', function () {
                loadsecondhyr($(this).val());
            });
            $("#hyrw").on('change', function () {
                if ($(this).val() == 'des') {
                    loadhyr();
                }
                else if ($(this).val() == 'hie') {
                    loadhyrwise()
                } else {
                    $("#sel1").hide();
                    $("#sel2").hide();
                    $("#hry2").hide();
                }
            });

            $('#usrauto').on('change', function () {
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#sfusrname').prop('readonly', true)
                    usratuogenerate = true; getDivisname(1);
                }
                else {
                    $('#sfusrname').prop('readonly', false)
                    usratuogenerate = false;
                }
            })
            const search = document.querySelector("#search");
            const checkboxContainer = document.querySelector(".checkbox-container");

            search.addEventListener("input", function () {
                const searchTerm = this.value.toLowerCase();
                const checkboxes = checkboxContainer.querySelectorAll("label");
                let matchFound = false;
                checkboxes.forEach(function (checkbox) {
                    const checkboxText = checkbox.textContent.toLowerCase();
                    if (checkboxText.includes(searchTerm)) {
                        checkbox.style.display = "block";
                        matchFound = true;
                    } else {
                        checkbox.style.display = "none";
                    }
                });
                if (!matchFound) {
                    $('.checkbox-container').append('<label for="" id="" class="empty" >No Result Found</label>')
                }
                else {
                    $('.empty').remove();
                }
            });
        });
        //var cc = document.querySelector('#sfNorm');

        //cc.addEventListener('keydown', function (e) {

        //    var charCode = (e.which) ? e.which : e.keyCode;
        //    if ((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 105) || charCode == 8 || charCode == 190 || charCode == 110 || charCode == 46) {
        //        return true;
        //    }
        //    e.preventDefault();
        //    return false;
        //}, true);
        function openModal() {
            $('#imageModal').css('display', 'block');
        }

        function closeModal() {
            $('#imageModal').css('display', 'none');
        }
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



        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }
        function getdistnum() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getdistnum",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').hide();
                    SFDivnum = JSON.parse(data.d);
                    $("#Txt_stock_Code").val(SFDivnum[0].Num);
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
        }
        function loaddiv() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_SupplierCreation.aspx/getdivision",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').show();
                    SFDivisname = JSON.parse(data.d);
                    var str = '';
                    for (var i = 0; i < SFDivisname.length; i++) {
                        str += '<div class="col-xs-3"><input type="checkbox" name="sub_div" id="' + SFDivisname[i].subdivision_code + '"style="width: 15px;"/><label style="padding-left: 5px;">' + SFDivisname[i].subdivision_name + '</label></div>'
                    }
                    $("#division_div").html(str);
                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
        }
        <%-- new function --%>
        function loadstate() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_SupplierCreation.aspx/getstate",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').show();
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlstate").html('');
                        var tert = $("#ddlstate");
                        tert.empty().append('<option selected="selected" value="0">Select State </option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].State_Code + '">' + SFTer[i].statename + '</option>'))
                        }
                    }
                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
            $('#ddlstate').selectpicker({
                liveSearch: true
            });
        }
         
        function loadfoname() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_SupplierCreation.aspx/getFOName",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').show();
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $(".checkbox-container").html('');
                        var str = '';
                        var tert = $("#ddlField");

                        tert.empty().append('<option  value="0">Select Field Officer</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            // tert.append($('<option value="' + SFTer[i].Sf_Code + '">' + SFTer[i].Sf_Name + '</option>'))
                            str += ' <label for="' + SFTer[i].Sf_Code + '" id = "l' + SFTer[i].Sf_Code + '"><input type = "checkbox" class="sfcheck" style="width: inherit;" id = "' + SFTer[i].Sf_Code + '" value = ' + SFTer[i].Sf_Code + ' >' + SFTer[i].Sf_Name + '</label>';
                            //$(".checkbox-container").append(' <label for="' + SFTer[i].Sf_Code + '"><input type = "checkbox" class="sfcheck" style="width: inherit;" id = "' + SFTer[i].Sf_Code + '" value = ' + SFTer[i].Sf_Code + ' >' + SFTer[i].Sf_Name + '</label>');
                            //$(".checkbox-container").append('<input type = "checkbox" class="sfcheck" style="width: inherit;" id = "' + SFTer[i].Sf_Code + '" value = ' + SFTer[i].Sf_Code + ' >');
                            // $(".checkbox-container").append(' <label for="'+SFTer[i].Sf_Code+'">"' + SFTer[i].Sf_Name + '"</label>');

                        }
                        $(".checkbox-container").append(str);
                    }
                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
            $('#ddlField').selectpicker({
                liveSearch: true
            });
            $('#ddlField').selectpicker('refresh');
        }

        function loadDist() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getdist",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').show();
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlDistrict").html('');
                        var tert = $("#ddlDistrict");
                        tert.empty().append('<option selected="selected" value="0">Select District</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Dist_code + '">' + SFTer[i].Dist_name + '</option>'))
                        }
                    }
                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
            $('#ddlDistrict').selectpicker({
                liveSearch: true
            });
        }

        function loadRate() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getRatecard",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').show();
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlRate").html('');
                        var tert = $("#ddlRate");
                        tert.empty().append('<option selected="selected" value="0">Select Rate</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Price_list_Sl_No + '">' + SFTer[i].Price_list_Name + '</option>'))
                        }
                    }
                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
            $('#ddlRate').selectpicker({
                liveSearch: true
            });
        }

        function loadhyr() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/gethyr",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $("#sel2").hide();
                    $("#hry2").show();
                    $('.spinnner_div').show();
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#hyr").html('');
                        // Destroy the existing selectpicker instance
                        $('#hyr').selectpicker('destroy');

                        // Remove any existing options
                        $('#hyr').empty();
                        var tert = $("#hyr");
                        tert.empty().append('<option selected="selected" value="ALL">All</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].soname + '">' + SFTer[i].soname + '</option>'))
                        }
                        $('#hyr').selectpicker({
                            liveSearch: true
                        });
                        $("#sel1").show();
                    }
                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });

        }
        function loadhyrwise() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/gethyrwise",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $("#sel1").hide();
                    $("#hry2").show();
                    $('.spinnner_div').show();
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#hyrsecond").html('');
                        var tert = $("#hyrsecond");
                        // Destroy the existing selectpicker instance
                        $('#hyrsecond').selectpicker('destroy');

                        // Remove any existing options
                        $('#hyrsecond').empty();
                        tert.empty().append('<option selected="selected" value="ALL">All</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Sf_Code + '">' + SFTer[i].Sf_Name + '</option>'))
                        }

                        $('#hyrsecond').selectpicker({
                            liveSearch: true
                        });
                        $("#sel2").show();
                    }
                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });


        }
        function loadsecondhyr(SEC) {
            $('.spinnner_div').show();
            if (SEC == 'ALL') {
                $(".sfcheck").parent().show();
            }
            else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_Distributor_creation.aspx/getsecondHYR",
                    data: "{'Div_Code':'" + SEC + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('.spinnner_div').show();
                        var SFTer = JSON.parse(data.d) || [];
                        const checkboxContainer = document.querySelector(".checkbox-container");
                        for (var i = 0; i < SFTer.length; i++) {
                            // const searchTerm = this.value.toLowerCase();
                            const checkboxes = checkboxContainer.querySelectorAll("label");
                            checkboxes.forEach(function (checkbox) {
                                const checkboxText = checkbox.htmlFor;
                                if (checkboxText.includes(SFTer[i].Sf_Code)) {
                                    checkbox.style.display = "block";
                                } else {
                                    checkbox.style.display = "none";
                                }
                            });

                        };

                        $(".sfcheck").parent().hide();
                        for (var i = 0; i < SFTer.length; i++) {
                            $("#l" + SFTer[i].Sf_Code).css('display', 'block');

                        }
                        $('.spinnner_div').hide();


                    },
                    error: function (rs) {
                        alert(rs);
                        $('.spinnner_div').hide();
                    }
                });
            }
            //$('#hyr').selectpicker({
            //    liveSearch: true
            //});
        }
        function loadSEChyr(SEC) {
            $('.spinnner_div').show();
            if (SEC == 'ALL') {
                $(".sfcheck").parent().show();
            }
            else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_Distributor_creation.aspx/getSECHYR",
                    data: "{'Div_Code':'" + SEC + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('.spinnner_div').show();
                        var SFTer = JSON.parse(data.d) || [];
                        const checkboxContainer = document.querySelector(".checkbox-container");
                        //for (var i = 0; i < SFTer.length; i++) {
                        //   // const searchTerm = this.value.toLowerCase();
                        //    const checkboxes = checkboxContainer.querySelectorAll("label");
                        //    checkboxes.forEach(function (checkbox) {
                        //        const checkboxText = checkbox.htmlFor;
                        //        if (checkboxText.includes(SFTer[i].Sf_Code)) {
                        //            checkbox.style.display = "block";
                        //        } else {
                        //            checkbox.style.display = "none";
                        //        }
                        //    });

                        //};

                        $(".sfcheck").parent().hide();
                        for (var i = 0; i < SFTer.length; i++) {
                            $("#l" + SFTer[i].Sf_Code).css('display', 'block');

                        }
                        $('.spinnner_div').hide();


                    },
                    error: function (rs) {
                        alert(rs);
                        $('.spinnner_div').hide();
                    }
                });
            }
            //$('#hyr').selectpicker({
            //    liveSearch: true
            //});
        }
        function getDivisname(fl) {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_SupplierCreation.aspx/getusername",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').show();
                    SFDivisname = JSON.parse(data.d);
                    $('#fixUsrn').text(SFDivisname[0].Division_SName + "-");
                    if (fl == 1) $('#sfusrname').val(SFDivisname[0].uname);
                    $('.spinnner_div').hide();
                }
            });
            $('.spinnner_div').hide();
        }
        var result = '';
        var result1 = '';
        var ary = '';
        $('#sb').on('click', function () {
            let optionElements = document.querySelectorAll('.selected-option span:first-child');
            let optionTexts = Array.from(optionElements).map(elem => elem.textContent);
            result = optionTexts.join(',');
            var ret = result.split(',');
            let optionElements1 = document.querySelectorAll('.selected-option span:last-child');
            let optionTexts1 = Array.from(optionElements1).map(elem => elem.textContent);
            result1 = optionTexts1.join(',');
            $("#form-container").slideToggle();
            if (ret.length > 0) {
                ary = result.split(',').map(function (val) {
                    return { sfcode: val };
                });
				if(result!="")
                  $('#tt').val(ret.length + ' member mapped');
				else
                $('#tt').val('nothing selected');
            }
            else
                $('#tt').val('nothing selected');
            // alert(result1);
        });
        $('#cb').on('click', function () {
            $("#form-container").slideToggle();
        });
        $('#Finsubmit').on('click', function (e) {
            $('.spinnner_div').show();
            var sfcode = '<%=stockist_code%>';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/sfapprovereject",
                data: "{'Div_Code':'<%=Session["div_code"]%>','stk_code':'" + sfcode + "','type':'approve','sftype':'finance','remarks':''}",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        alert("Approved Succefully");
                        $('.spinnner_div').hide();
                        window.location = 'new_Distributor_Master.aspx';
                    }

                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
        });
        $('#Finalappr').on('click', function (e) {
            $('.spinnner_div').show();
            var sfcode = '<%=stockist_code%>';
            var sfcrdlimit = $('#sfcrdlimit').val();
            var sfcrdday = $('#sfcrdday').val();
            var sfreconac = $('#sfreconac').val();
            var sfrecontxt = $('#sfrecontxt').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/sfapprovereject",
                data: "{'Div_Code':'<%=Session["div_code"]%>','stk_code':'" + sfcode + "','type':'approve','sftype':'master','remarks':'','sfcrdlimit':'" + sfcrdlimit + "','sfcrdday':'" + sfcrdday + "','sfreconac':'" + sfreconac + "','sfrecontxt':'" + sfrecontxt + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        alert("Approved Succefully");
                        $('.spinnner_div').hide();
                        window.location = 'new_Distributor_Master.aspx';
                    }

                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
        });
        $('#logsubmit').on('click', function (e) {
            var dis_route = $('#ddlroute').val().trim();
            var dis_routename = $('#ddlroute :selected').text();
            var sfcode = '<%=stockist_code%>';
            if (dis_route == '0') {
                alert('SELECT ROUTE ');
                return false;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/logsubmit",
                data: "{'Div_Code':'<%=Session["div_code"]%>','stk_code':'" + sfcode + "','route_code':'" + dis_route + "','route_name':'" + dis_routename + "'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').show();
                    alert(data.d)
                    $('.spinnner_div').hide();
                    window.location = 'new_Distributor_Master.aspx';
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
        });
        $('#btnsubmit').on('click', function (e) {


            var stockist_name = $("#txtStockist_Name").val() || '';
            var erp_code = $("#Txt_ERP_Code").val() || '';
            var dis_state = $('#ddlstate').val() || '';
            var dis_type = $('#ddltype').val() || '';
            var dis_statename = $('#ddlstate :selected').text();
            var dis_address = $('#sfAddress').val() || '';
            var Gst = $('#txtGst').val() || '';
            var ContactPerson = $('#txtStockist_ContactPerson').val() || '';
            var dis_mobile = $('#sfMobile').val() || '';
            var sfusrname = $('#sfusrname').val() || '';
            var sfusrnames = $('#sfusrname').val() || '';
            var fixUsrn = $('#fixUsrn').text() || '';
            if(sfusrname!='')
              sfusrname = fixUsrn + sfusrnames;
            var dis_pass = $('#sfpassword').val() || '';
            var ddlFieldname = result1;
            var ddlField = result;
            var sfcode = '<%=stockist_code%>';
            var sub_div = '';
            $('input[name="sub_div"]:checked').each(function () {
                sub_div += $(this).attr('id') + ',';
            });
            if (stockist_name == '') {
                alert('Enter Supplier Name');
                return false;
            }
            if (erp_code == '') {
                alert('Enter ERP Code');
                return false;
            }
            if (dis_state == '' || dis_state == '0') {
                alert('Select State');
                return false;
            }
			if (dis_type == '' || dis_type == '-1') {
                alert('Select type');
                return false;
            }
            //if (sfusrname == '') {
            //    alert('Enter Username ');
            //    return false;
            //}
            if (ContactPerson == '') {
                alert('Enter Contact Person');
                return false;
            }
            if (dis_mobile == '') {
                alert('Enter Mobile Number');
                return false;
            }
            //if (dis_pass == '') {
            //    alert('Enter Password ');
            //    return false;
            //}
            if (ddlField == '') {
                alert('Choose Field Officer ');
                return false;
            }
            if (sub_div == '') {
                alert('Select Sub division ');
                return false;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_SupplierCreation.aspx/save_supplier",
                data: "{'Div_Code':'<%=Session["div_code"]%>','stockist_code':'" + sfcode + "','stockist_name':'" + stockist_name + "','contant_person':'" + ContactPerson + "','mobile':'" + dis_mobile + "','erp_code':'" + erp_code + "','username':'" + sfusrname + "','pass':'" + dis_pass + "','field_name':'" + ddlFieldname + "'\
                    , 'field_code': '" + ddlField + "', 'state_code': '" + dis_state + "', 'state_name': '" + dis_statename + "', 'Gst': '" + Gst + "'\
                    , 'address': '" + dis_address + "','sub_div':'" + sub_div + "','dis_type':'" + dis_type + "'}",
                dataType: "json",
                success: function (data) {


                    $('.spinnner_div').hide();
                    alert("Created Successfully");
                    window.location = 'SupplierList.aspx';

                },
                error: function (rs) {
                    alert(rs);
                }
            });
        });



        $('#btnsubmit1').on('click', function () {


            $('.spinnner_div').show();
            var erp_code = $('#Txt_ERP_Code').val().trim();
            var userentry = $('#Txt_stock_Code').val().trim();
            var stockist_code = getParameterByName('stockist_code', '')
            var ContactPerson = $('#txtStockist_ContactPerson').val().trim();
            var Stockist_Name = $('#txtStockist_Name').val().trim();
            if (Stockist_Name == "") {
                alert('Enter the Distributor Name');
                $('#txtStockist_Name').focus();
                return false;
                $('.spinnner_div').hide();
            }
            var sfusrname = $('#sfusrname').val();
            if (stockist_code != "" && stockist_code != null) {
                if (sfusrname == '') {
                    sfusrname = "";
                }
                else {
                    sfusrname = $('#fixUsrn').text() + sfusrname;
                }
            }
            var sfterr = $('#ddlTerritory').val().trim();
            var sfterrname = $('#ddlTerritory :selected').text();
            if (sfterr == '' || sfterr == "0") {
                alert('select Territory');
                $('#ddlTerritory').focus();
                return false;
                $('.spinnner_div').hide();
            }
            var sfDistrict = $('#ddlDistrict').val();
            var sfDistrictname = $('#ddlDistrict :selected').text();
            var stockcode = $('#Txt_stockCode').val();
            //if (sfDistrict == '' || sfDistrict == "0") {
            //    alert('select District');
            //    $('#ddlDistrict').focus();
            //    return false;
            //    $('.spinnner_div').hide();
            //}
            //var sfddlTaluk = $('#ddlTaluk').val().trim();
            var sfType = $('#ddlType').val().trim();
            if (sfType == '' || sfType == "-1") {
                alert('select Type ');
                $('#ddlType').focus();
                return false;
                $('.spinnner_div').hide();
            }
            var sfRate = $('#ddlRate').val() == undefined ? '' : $("#ddlRate").val().trim();
            if (sfRate == '' || sfRate == "0") {
                // alert('select Rate');
                // $('#ddlRate').focus();
                // return false;
            }
            var sfAddress = $('#sfAddress').val().trim();
            if (sfAddress == '' || sfAddress == "0") {
                alert('Enter Address');
                $('#sfAddress').focus();
                return false;
                $('.spinnner_div').hide();
            }
            var sfDesignation = $('#sfDesignation').val().trim();
            var sfMobile = $('#sfMobile').val().trim();
            var sfNorm = $('#sfNorm').val() == undefined ? 0 : $('#sfNorm').val() == '' ? 0 : $('#sfNorm').val();
            var sfemail = $('#txtemail').val().trim();
            var sfpwd = $('#sfpassword').val();
            if (sfpwd == '') {
                //alert('Enter the Password');
                // $('#sfpassword').focus();
                // return false;
            }

            //var ddlFieldcode = $('#ddlField').val() || '';
            //if (ddlFieldcode == '') {
            //    alert("select Field Officer");
            //    $('#ddlField').focus();
            //    return false;
            //    $('.spinnner_div').hide();
            //}
            var ddlFieldname = result1;
            var ddlField = result;
            //for (var y = 0; y < ddlFieldcode.length; y++) {
            //    ddlField += ddlFieldcode[y] + ',';
            //    ddlFieldname += $("#ddlField option[value='" + ddlFieldcode[y] + "']").text() + ',';
            //}
            if (ddlField == '') {
                alert("select  Field Officer");
                $('#ddlField').focus();
                $('.spinnner_div').hide();
                return false;
            }
            var sfHead = $('#sfHead').val().trim();
            var sfCategory = $('#ddlCategory').val() == undefined ? '' : $("#ddlCategory").val().trim();
            if (sfCategory == '') {
                //alert("select the Category");
                //$('#ddlCategory').focus();
                // return false;
            }
            var sfGSTN = $('#sfGSTN').val();
            var stock = $('input[name="stock"]:checked').val();
            var sub_div = '';
            $('input[name="sub_div"]:checked').each(function () {
                sub_div += $(this).attr('id') + ',';
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/submit_distributor",
                data: "{'Div_Code':'<%=Session["div_code"]%>','Stock_code':'" + stockcode + "','userentry':'" + userentry + "','stockist_code':'" + stockist_code + "','erp_code':'" + erp_code + "','ContactPerson':'" + ContactPerson + "','Stockist_Name':'" + Stockist_Name + "','sfusrname':'" + sfusrname + "'\
                    , 'sfterr': '" + sfterr + "', 'sfterrname': '" + sfterrname + "', 'sfDistrict': '" + sfDistrict + "', 'sfDistrictname': '" + sfDistrictname + "', 'sfType': '" + sfType + "'\
                    , 'sfRate': '" + sfRate + "', 'sfAddress': '" + sfAddress + "', 'sfemail': '" + sfemail + "', 'sfDesignation': '" + sfDesignation + "', 'sfMobile': '" + sfMobile + "', 'sfNorm': '" + sfNorm + "', 'sfpwd': '" + sfpwd + "', 'ddlField': '" + ddlField + "'\
                     , 'ddlFieldname': '" + ddlFieldname + "', 'sfHead': '" + sfHead + "', 'sfCategory': '" + sfCategory + "', 'sfGSTN': '" + sfGSTN + "', 'stock': '" + stock + "', 'sub_div': '" + sub_div + "', 'ary': '" + JSON.stringify(ary) + "' }",
                dataType: "json",
                success: function (data) {

                //    var divcode = '<%=Session["div_code"]%>';
                    if (data.d.length > 0) {
                        if (data.d == "Created_Successfully") {
                            $('.spinnner_div').hide();
                            alert('Created_Successfully');
                            location.reload();
                        }
                        else if (data.d == "Updated_Successfully") {
                            if (divcode == "32" || divcode == "43" || divcode == "48") {
                                $('.spinnner_div').hide();
                                alert("Updated Successfully");
                                window.location = 'new_Distributor_Creation.aspx';
                            }
                            else {
                                $('.spinnner_div').hide();
                                alert("Updated Successfully");
                                window.location = 'new_Distributor_Master.aspx';
                            }
                        }
                        else {
                            alert(data.d);
                            return false;
                        }
                    }


                },
                error: function (rs) {
                    alert(rs);
                }
            });
        });
        function zoomImage(imgUrl) {
            // Create a new element to display the zoomed image
            var zoomedImage = document.createElement('div');
            zoomedImage.classList.add('zoomed-image');

            // Create an image element inside the zoomed image container
            var zoomedImgElement = document.createElement('img');
            zoomedImgElement.src = imgUrl;

            // Append the image element to the zoomed image container
            zoomedImage.appendChild(zoomedImgElement);

            // Add the zoomed image container to the document body
            document.body.appendChild(zoomedImage);

            // Add event listener to close the zoomed image on click
            zoomedImage.addEventListener('click', function () {
                document.body.removeChild(zoomedImage);
            });
        }
        function getFileExtension(filename) {
            return filename.slice((filename.lastIndexOf('.') - 1 >>> 0) + 2);
        }
        function loaddistval(sfcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_SupplierCreation.aspx/getdistdetail",
                data: "{'Div_Code':'<%=Session["div_code"]%>','stockist_code':'" + sfcode + "'}",
                dataType: "json",
                success: function (data) {
                    SfDetails = JSON.parse(data.d);
                    loadfoname();
                    Itype = '1';
                    //$('#txtemail').val(SfDetails[0].email);
                    if (!SfDetails[0].UsrDfd_UserName || SfDetails[0].UsrDfd_UserName == "") {
                        $('#sfusrname').val(SfDetails[0].UsrDfd_UserName)
                    }
                    else if (SfDetails[0].UsrDfd_UserName.indexOf("-") > -1) {
                        var sfausrname = SfDetails[0].UsrDfd_UserName.split("-");
                        $('#fixUsrn').text(sfausrname[0] + "-");
                        $('#sfusrname').val(sfausrname[1]);
                        $("#user").hide();
                    }
                    else {
                        $('#fixUsrn').text("");
                        $('#sfusrname').val(SfDetails[0].UsrDfd_UserName);
                        $("#user").hide();
                    }
                    $('#sfpassword').val(SfDetails[0].sf_password);
                    $('#Txt_ERP_Code').val(SfDetails[0].ERP_Code);
                    $('#txtStockist_Name').val(SfDetails[0].S_Name);
                    $('#txtStockist_ContactPerson').val(SfDetails[0].Contact_Person);
                    $('#sfAddress').val(SfDetails[0].shop_address);
                    $('#sfMobile').val(SfDetails[0].Mobile);
                    $('#txtGst').val(SfDetails[0].GST_NO);
                    $('#sfAddress').val(SfDetails[0].Addr);
                    $('#ddlstate').selectpicker('val', SfDetails[0].State_Code);
                    $('#ddltype').selectpicker('val', SfDetails[0].Type);
                    var sub = (SfDetails[0].subdivision_code).split(',');
                    for (var k = 0; k < sub.length; k++) {
                        $('input[id="' + sub[k] + '"]').prop('checked', true);
                    }
                    var splitsdiv = SfDetails[0].Field_Code.split(',') || [];
                    var splitsdivsfnm = SfDetails[0].Field_Code.split(',') || [];
                    //var splitsdiv = stp.split(',') || [];
                    if (splitsdiv.length > 0) {
                        var ctchk = 0;
                        result = "";
                        result1 = "";
                        for (var i = 0; i < splitsdiv.length; i++) {
                            var splitsdivsfnm = splitsdiv[i].split('-') || [];
                            if ($("#" + splitsdivsfnm[0]).val() == splitsdivsfnm[0]) {
                                $("#" + splitsdivsfnm[0]).prop("checked", true);
                                var nm = $("#" + splitsdivsfnm[0]).closest('label').text();
                                var option1 = nm.split("-")[0].trim();
                                var selectedOptionsContainer = $(".selected-options-container");
                                selectedOptionsContainer.append(
                                    '<div class="selected-option btn btn-primary" style="padding-top: 0px;padding-bottom: 2px;padding-left: 6px;font-size:12px;">' +
                                    '<span  id=' + splitsdivsfnm[0] + '>' + splitsdivsfnm[0] + '</span>' +
                                    '<span class="remove-option">&times;</span>' +
                                    '<span style="display:none;">' + option1 + '</span>' +
                                    '</div>'
                                );
                                ctchk++;
                            }
                            result += splitsdivsfnm[0] + ",";
                            result1 += option1 + ",";
                        }
                        $('#tt').val(ctchk + ' member mapped');
                        // $('.checkbox-container input[type="checkbox"] option[value="MGR4921"]').attr('selected', true);
                    }
                    else
                        $('#tt').val('nothing selected');
                    $('#ddlField').selectpicker('refresh');
                    // $("#user").hide();
                    $('.spinnner_div').hide();
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').hide();
                }
            });
        }
    </script>
         </html>
</asp:Content>

