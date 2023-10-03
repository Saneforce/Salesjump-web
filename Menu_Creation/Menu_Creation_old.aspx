<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Menu_Creation.aspx.cs" Inherits="Menu_Creation_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <style type="text/css">
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }
                th{
                    color: #0d2031;
                    text-align:left;
                }
               
                .loading
            {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }

            #txtfilter_chzn {
                width: 328px !important;
                position: absolute;
            }

            li {
                font-weight: 100;
            }
    .sub-header {
    
    margin-top: 54px;
}
        </style>
        <title></title>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <link type="text/css" rel="stylesheet" href="../css/style.css" />

        
    </head>
    <body>
        <form id="form1" runat="server" method="post">
            <center class="col-lg-4">
            <h3>Create Menu</h3>
        <div>
            <table>
                <tr><td><input type="hidden" id="menuid"  onblur="this.style.backgroundColor='White'" onfocus="this.style.backgroundColor='#E0EE9D'"/></td>
</tr>
                <tr>
                    <td>
                        <label>Menu Name</label>
                     
                    </td>
                    <td>
                        <input type="text" id="txtmenuname"  onblur="this.style.backgroundColor='White'" onfocus="this.style.backgroundColor='#E0EE9D'"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Menu Icon</label>
                    </td>
                    <td>
                     <input type="text" id="txticon"  onblur="this.style.backgroundColor='White'" onfocus="this.style.backgroundColor='#E0EE9D'"/>
                    </td>
                </tr>

                 <tr>
                    <td>
                        <label>Menu Type</label>
                    </td>
                    <td>                      
                            <select id="ddlmenutype">
                                <option value="-1">--Select--</option>
                              <option value="0">Header</option>
                              <option value="1">Sub Header</option>
                              <option value="2">Link</option>
                            </select>
                       
                    </td>
                </tr>
            
            
                    <tr>
                        <td>
                            <label id="lbparentmenu" style="display:none;">Parent Menu</label>
                        </td>
                        <td>
                      <select  id="ddlparentmenu" style='display:none'>
                          
                           </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <label id="lblinkurl" style="display:none;">Link Url</label>
                        </td>
                        <td>
                            <input type="text" id="txtlinkurl" style='display:none' onblur="this.style.backgroundColor='White'" onfocus="this.style.backgroundColor='#E0EE9D'"/>
                        </td>

                    </tr>
					<tr>
                    <td>
                        <label id="Tbar" style="display:none;">T Bar</label>
                    </td>
                    <td>
                        
                        <input type="radio" id="rd_1" value="1" style="display:none;" name="rdBtn" />
						<label id="Tbarlbl1" style="display:none;">Needed</label>
                        
                        <input type="radio" id="rd_2" style="display:none;" name="rdBtn" />
						<label id="Tbarlbl2" value="0" style="display:none;">No Needed</label>
                    </td>
                </tr>
                    <tr>                
                        <td rowspan="2">
                             <input type="button" class="btn btn-success btn-md" onclick="saveData();" id="btnSave" value="Save" title="Save" />
                        </td>
                         <td rowspan="2">
                             <input type="button" class="btn btn-success btn-md" onclick="clearData();" id="clr" value="Clear" title="Clear" />
                        </td>
                    </tr>
                </table>
                <div class="loading" align="center">
                 Loading. Please wait.<br />
                 <br />
                <img src="../Images/loader.gif" alt="" />
        </div>
             
        </div></center>
            
                <div class="col-lg-8">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text"  id="tSearchOrd" style="width: 250px;"  autocomplete="off"/>
                   <%-- <label style="white-space: nowrap; margin-left: 57px;">Filter By&nbsp;&nbsp;</label><select id="txtfilter" name="ddfilter" style="width: 250px;"></select>--%>
                    <%--<label style="float: right">Show--%>
                        <%--<select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>--%>
                        <%--entries</label>--%>
                </div>
                                   <div class="scroll" style="overflow: scroll;overflow: auto; height:550px; overflow-y: scroll;">

                <table class="table table-hover" id="OrderList" style="font-size: 12px;">
                    <thead class="text-warning">
                        <tr style="white-space: nowrap;">
						    <th>S.NO</th>
                            <th>Menu_ID</th>
                            <th>Menu_Names</th>
                            <th>Menu_Type</th>
                            <th>Link_Url</th>
                            <th>Menu_Icon</th>
                            <th>Parent_Menu</th>
                            <th>Edit</th>
                        </tr>
                    </thead>

                    <tbody id="myTable">
                        <tr></tr>
                    </tbody>
                </table>
                    </div>
                
            </div>
        </div>   
        </form>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script type="text/javascript">
    var data;
    var dataaa = [];
    $(document).ready(
        function () {
            var str = "";
            pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Menu_IDs,Menu_Names,Menu_Types,Parent_Menus";

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "../menucre.asmx/getMenuCreationData",
                dataType: "json",
                async: false,
                success: function (data) {
                    AllData = data.d;
                    localStorage.clear();
                    localStorage.setItem('d', JSON.stringify(AllData));
                    dataaa = JSON.parse(localStorage.getItem('d'));

                },

                error: function (result) {
                    console.log(result);
                }
            });

            $("#tSearchOrd").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#myTable tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });


            $('#ddlmenutype').on('change', function () {

                 if (this.value == '1') {
                            $("#ddlparentmenu").show();
                            $("#lbparentmenu").show();
                            $("#txtlinkurl").hide();
                            $("#lblinkurl").hide();
                            $("#Tbar").show();
                            $("#Tbarlbl1").show();
                            $("#Tbarlbl2").show();
                            $("#rd_1").show();
                            $("#rd_2").show();
                        }
                        else if (this.value == '2') {
                            $("#ddlparentmenu").show();
                            $("#lbparentmenu").show();
                            $("#txtlinkurl").show();
                            $("#lblinkurl").show();
                            $("#Tbar").show();
                            $("#Tbarlbl1").show();
                            $("#Tbarlbl2").show();
                            $("#rd_1").show();
                            $("#rd_2").show();
                        }
                        if (this.value == '0') {
                            $("#ddlparentmenu").hide();
                            $("#lbparentmenu").hide();
                            $("#txtlinkurl").hide();
                            $("#lblinkurl").hide();
                            $("#Tbarlbl1").hide();
                            $("#Tbarlbl2").hide();
                            $("#Tbar").hide();
                            $("#rd_1").hide();
                            $("#rd_2").hide();
                        }
            });
            function search(dataaa) {
                var MainM = dataaa.filter(function (a) {
                    return a.Menu_Types == '0' || a.Menu_Types == '1';
                });
                var sf = $("[id*=ddlparentmenu]");
                sf.empty().append('<option selected="selected" value="0">Select Filter</option>');
                for (var i = 0; i < MainM.length; i++) {
                    sf.append($('<option value="' + MainM[i].Menu_IDs + '">' + MainM[i].Menu_Names + '</option>')).trigger('chosen:updated').css("width", "100%"); ; ;
                };
                $('[id*=ddlparentmenu]').chosen();
            }
            function view(dataaa) {

                for (var i = 0; i < dataaa.length; i++) {
                    str += ("<tr><td>" + (i + 1) + "</td><td>" + dataaa[i].Menu_IDs + "</td><td>" + dataaa[i].Menu_Names + "</td><td>" + dataaa[i].Menu_Types + "</td><td>" + dataaa[i].Link_Urls + "</td><td><img class='md-icon' style='width:30%;' src=" + dataaa[i].Menu_Icons + "></td><td>" + dataaa[i].Parent_Menus + "</td><td><a id='add' href='#?type=1' onclick=\"update('" + dataaa[i].Tbar_ids + "','" + dataaa[i].Menu_IDs + "','" + dataaa[i].Menu_Names + "','" + dataaa[i].Menu_Icons + "','" + dataaa[i].Menu_Types + "','" + dataaa[i].Parent_Menus + "','" + dataaa[i].Link_Urls + "',1)\">Edit</a></tr>");
                }
                $('.table-hover').show();
                $('.table-hover').append(str);
            }
            view(dataaa);
            search(dataaa);

        });
    $('#clr').click(function () {
         $("#txtmenuname").val('');
        $("#txticon").val('');
        $("#ddlmenutype").val('');
        $("#ddlparentmenu").val('');
        $("#txtlinkurl").val('');
        $("#menuid").val('');
        $("#ddlparentmenu").hide();
        $("#lbparentmenu").hide();
        $("#txtlinkurl").hide();
        $("#lblinkurl").hide();
    });
    $('#btnSave').click(function () {

        var menuname = $("#txtmenuname").val();
        var icon = $("#txticon").val();
        var type = $("#ddlmenutype").val();
        var parentid = $("#ddlparentmenu").val();
        var url = $("#txtlinkurl").val();
        var id = $("#menuid").val();
		var tbar = (type != 0) ? $("input[name=rdBtn]:checked").val() : 0;

		arr = AllData.filter(function (a) {
                    return (a.Menu_Name == menuname && a.Link_Urls == url && a.Menu_IDs == id);
                });
        if (arr.length == 0) {
			$.ajax({
				type: 'POST',
				url: 'Menu_Creation.aspx/saveData',
				async: false,
				data: "{'menu_name':'" + menuname + "','menu_icon':'" + icon + "','menutype':'" + type + "','parentmenu':'" + parentid + "','menuurl':'" + url + "','menu_id':'" + id + "'\
                          ,'Active_flag':'"+0+"','Dfault_Screen':'"+id+"','order_id':'"+0+"','Tbar_id':'"+tbar+"'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (data) {					
					if(data.d=='Success'){
						alert("Saved successfully.");					
						$('#txtmenuname').val('');
						$("#txticon").val('');
						$("#ddlmenutype").val('');
						$("#ddlparentmenu").val('');
						$("#txtlinkurl").val('');
						window.location.reload();
					}
					else{
						alert(data.d);
					}
				}
			});
		}
        else {
          alert("Menu Name Already Exists");
        }
    });
    function update(tbar,menu_id, menu_name, menu_icon, menutype, parentmenu, menuurl) {
        $(document).find('#menuid').val(menu_id);
        $(document).find('#txtmenuname').val(menu_name);
        $(document).find("#txticon").val(menu_icon);
        $(document).find("#ddlmenutype").val(menutype);
        tbar == 1 ? $("#rd_1").prop('checked', 'checked') : $("#rd_2").prop('checked', 'checked');
        if (menutype = '2') {
            $("#ddlparentmenu").show();
            $("#lbparentmenu").show();
            $("#txtlinkurl").show();
            $("#lblinkurl").show();
            $("#Tbar").show();
            $("#Tbarlbl1").show();
            $("#Tbarlbl2").show();
            $("#rd_1").show();
            $("#rd_2").show();
        }
       
        $(document).find("#ddlparentmenu").val(parentmenu);
        $(document).find("#txtlinkurl").val(menuurl);
       
     

    }
         </script>
        
    </body>
    </html>
</asp:Content>

