﻿<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="Talukcreation.aspx.cs" Inherits="MasterFiles_Talukcreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<body>
    <form id="form1" runat="server">
        <div class="col-lg-12 sub-header" style="color: #ee3939;font-size: large;text-align: center;">Taluk Creation
                    </div><br />
        <div class="card">
        <div class="card-body table-responsive">
           
                 <input type="hidden" id="hoid"/>
        <div class="row">
                        <div class="col-sm-2">
                            <label>Taluk Name</label>
                        </div>
                        <div class="col-xs-6">
                            <input type="text" id="name" class="form-control"/>
                        </div>
                    </div><br />
      
     
            <div class="row" style="margin-top: 10px;">
                <div class="col-sm-2">
                            <label style="white-space:nowrap;"></label>
                        </div>
             <div class="col-xs-6" style="margin-left: -15px;">
                <button id="btnsub" class="btn btn-primary" style="width:100px"><span>Save</span></button>
           </div>
                </div>
            </div>
            </div>
    </form>
</body>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script src="http://code.jquery.com/jquery-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        var url_string = window.location.href;
        var newurl = new URL(url_string);
        var ho_id = newurl.searchParams.get("twncode");
        
         $("input[type='text']").on("keypress", function (e) {
                    var val = $(this).val();
                    var regex = /(<([^>]+)>)/ig;
                    var result = val.replace(regex, "");
                    $(this).val(result);
                });
        
        $(document).ready(function () {
           
            filltext();

        });
        function filltext() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                data: "{'hoid':'" + ho_id + "'}",
                url: "Talukcreation.aspx/filldata",
                dataType: "json",
                success: function (data) {
                    dtlz = JSON.parse(data.d);
                    for (var i = 0; i < dtlz.length; i++) {
                        $('#name').val(dtlz[i].TownName);
                      
                    }
                },

            });
        }
       
        $("#btnsub").click(function () {
            var name1 = $('#name').val();
            var name = name1.trim();
			if (name != "") {
var name2 = name.replace(/<(.|\n)*?>/g, '');
          $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
              data: "{'nme':'" + name2 + "','hoid':'" + ho_id + "',divcode:'<%=Session["div_code"]%>'}",
              url: "Talukcreation.aspx/saveData",
                dataType: "json",
                success: function (data) {
                     if (data.d == "Exist") {
                            alert("Taluk Already Exist...");
                        }
                        else {
                            alert("Saved successfully.");
                        }
                   // window.location.href = '../MasterFiles/ertlogin_dtls_view.aspx';

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
			}
			else {
                alert("Please enter Taluk Name...");
                document.getElementById("name").focus();
            }

        });
   
   
    </script>
</asp:Content>