<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewpromotion_image.aspx.cs" Inherits="MasterFiles_Reports_viewpromotion_image" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
     #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 50%;
        }

    th {
        position: sticky;
        top: 0;
        background: #6c7ae0;
        text-align: center;
        font-weight: normal;
        font-size: 15px;
        color: white;
    }

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
            text-align: center;
        }
</style>
</head>
    
<body>
    <form id="form1" runat="server">
        <div>
             <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="Activity Event Captures" Style=" margin-left: 10px; font-size: x-large "></asp:Label>
           </div>
                 </div>
                  <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
      
             <div>
                 <div id="txt"></div>
            <table class="auto-index" id="grid" style="display:none;">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Image</th>
                        <th>Title</th>
						<th>Date And Time</th>
                        <th>Remarks</th>
                     </tr>
                 </thead>
                <tbody></tbody>
              
            </table>
        </div>
        </div>
    </form>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  <script type="text/javascript">
      var prodsi = [];
      $(document).ready(function (){
          promotionimg();
         
      })
      function closew() {
          $('#cphoto1').css("display", 'none');
      }
      $(document).ready(function () {
          dv = $('<div style="position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
          $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
          $("body").append(dv);
      });
      $(document).on('click', '.view_image', function () {
          var photo = $(this).attr("src");
          $('#photo1').attr("src", $(this).attr("src"));
          $('#cphoto1').css("display", 'block');
      });
      function promotionimg() {

     
          $.ajax({
              type: "Post",
              contentType: "application/json; charset=utf-8",
              url: "viewpromotion_image.aspx/promotionimg",
              dataType: "json",
              success: function (data) {
                  $('#grid').show();
                  $('#grid tbody').html('');
                  prodsi = JSON.parse(data.d);
                  str = '';
                  strs = '';
                  if (prodsi.length > 0) {
                      for (var i = 0; i < prodsi.length; i++) {


                          str += "<tr><td>" + (i + 1) + "</td><td><img class='view_image' src='http://sanfmcg.com/photos/" + prodsi[i].SF + '_' + prodsi[i].imgurl + "' style='height: 100px;width: 100px;'></td><td>" + prodsi[i].title + "</td><td>" + prodsi[i].Dateandtime + "</td><td>" + prodsi[i].remarks + "</td></tr>";
                      }
                  }
                  else {
                      $('#grid').hide();
                      strs = "<h1>No Image</h1>";
                  }
                  $('#grid tbody').append(str);
                  $('#txt').append(strs);
              }
          });
      };
         </script>
</body>
</html>
