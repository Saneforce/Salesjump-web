<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="rpt_route_availvisi.aspx.cs" Inherits="MIS_Reports_rpt_route_availvisi" %>


    <html xmlns="http://www.w3.org/1999/xhtml">
<head >
   <link href="../css/style.css" rel="stylesheet" />    
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
      <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"> 


     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" type="text/css" media="screen" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/base/jquery-ui.css">   

    <title> Route Availability</title>
    <style type="text/css">
        td,th{
            padding:15px;
        }
       tfoot
        {
            padding: 3px 8px;
            background-color: #496a9a;
            color: #fff;
            border: 1px solid #bbb;
            font-weight: bold;
        } 
    </style>
      <script type="text/jscript">

          $(document).ready(function () {
              var route = 0;
              var prod = 0;
              var str = 0;
              var str1 = 0;
              $.ajax({
                  type: "POST",
                  contentType: "application/json;charset=utf-8",
                  async: false,
                  url: "rpt_route_availvisi.aspx/Routevisible",
                  datatype: "json",
                  success: function (data) {
                      route = data.d;
                  },
                  error: function (rs, exception) {
                      alert(JSON.stringify(rs));

                  }
              });
              $.ajax({
                  type: "POST",
                  contentType: "application/json;charset=utf-8",
                  async: false,
                  url: "rpt_route_availvisi.aspx/Productvisible",
                  datatype: "json",
                  success: function (data) {
                      prod = data.d;
                  },
                  error: function (rs, exception) {
                      alert(JSON.stringify(rs));

                  }
              });
              var str2 = 0;
              var tbl = $('#tblRoutevisible');
              $(tbl).find('thead tr').remove();
              $(tbl).find('tbody tr').remove();
              str = '<th rowspan=2>Sl No</th><th rowspan=2>Route Name</th>'
              //$('#tblRoutevisible').find('thead').append('<tr style="background-color: #39435C;color: #fff;font-weight: bold;">' + str + '</tr>');

              if (prod.length > 0) {

                  for (var i = 0; i < prod.length; i++) {
                      str += '<th colspan=2>' + prod[i].Product_Name + '</th>';
                      str1 += '<th colspan=1> Availability</th><th colspan=1 rowspan=1> Visibility</th>';

                  }
				    str += '<th colspan=2>Total</th>';
                  str1 += '<th colspan=1> Availability</th><th colspan=1 rowspan=1> Visibility</th>';
                  $('#tblRoutevisible').find('thead').append('<tr style="background-color: #39435C;color: #fff;font-weight: bold;">' + str + '</tr>');
                  $('#tblRoutevisible').find('thead').append('<tr style="background-color: #39435C;color: #fff;font-weight: bold;">' + str1 + '</tr>');
                  artu = "";
                  dRout = route.filter(function (a) {
                      if (("," + artu + ",").indexOf("," + a.Territory_Name + ",") < 0) {
                          artu += a.Territory_Name + ",";
                          return true
                      }
                  })
				  
				     var Avl = 0;
                  var Vis = 0;
                  var str3 = '<td colspan="2">Total</td>';
                 
                  for (var i = 0; i < prod.length; i++) {
                      for (var j = 0; j < dRout.length; j++) {

                          res = route.filter(function (a) {
                              return (a.Territory_Name == dRout[j].Territory_Name && a.Product_Code == prod[i].Product_Code)
                          });
                          if (res.length > 0) {

                              Avl += parseInt(res[0].Available);
                              Vis += parseInt(res[0].Visible);

                          }
                          else {
                              // str2 += '<td>0 </td><td>0 </td>';
                          }
                       
                      }
                      str3 += '<td>' + Avl + '</td><td>' + Vis + '</td>';
                      Avl = 0;
                      Vis = 0;
                  }   
				   var av = 0;
                  var vi = 0;
                  var avil = 0;
                  var visi = 0;
				   for (var j = 0; j < dRout.length; j++) {
                      str2 = '<td>' + (j + 1) + '</td><td>' + dRout[j].Territory_Name + '</td >';
                      for (var i = 0; i < prod.length; i++) {
                          res = route.filter(function (a) {
                              return (a.Territory_Name == dRout[j].Territory_Name && a.Product_Code == prod[i].Product_Code)
                          })
                          if (res.length > 0) {
                              str2 += '<td> ' + res[0].Available + '</td><td>' + res[0].Visible + '</td>';
                              av += parseInt(res[0].Available);
                              vi += parseInt(res[0].Visible);
                              avil += parseInt(res[0].Available);
                              visi += parseInt(res[0].Visible);
                          }
                          else {
                              str2 += '<td>0</td><td>0</td>';
                          }

                      }
   str2 += '<td> ' + av + '</td><td>' + vi+ '</td>';  
                      $('#tblRoutevisible').find('tbody').append('<tr>' + str2 + '</tr>');
                   // <td>' + Number(avail || 0) + '</td><td>' + Number(visi || 0) + '</td></tr>');
 av = 0; vi = 0;
                  }
				    str3 += '<td>' + avil + '</td><td>' + visi + '</td>';
                   $('#tblRoutevisible').find('tfoot').append('<tr>' + str3 + '</tr>');
                          
                

              }

              else {
                  $('#tblRoutevisible').find('tbody').append('<tr><td colspan="14" style="color:red;font-weight: bold;">No Records Found..!</td></tr>');
              }

          });
          </script>
    </head>
<body style="padding:10px">
      <form id="form1" runat="server">

      <div>
        <asp:HiddenField ID="hdnsf" runat="server" />
          <asp:HiddenField ID="HiddenField1" runat="server" />
          <asp:HiddenField ID="HiddenField2" runat="server" />
          <div style="padding: 11px;float: right;">
          <span id="lblsfname">Field Force : </span>
                <asp:Label   id="txtsfnane" runat="server"></asp:Label>
                <span id="lblmonth">Month Year : </span>
                  <asp:Label  id="txtmonyear" runat="server"></asp:Label>
          </div>
          <div style="padding: 10px;">ROUTEWISE AVAILABILITY DETAILS</div>
            <table id="tblRoutevisible" class="newStly">
               <thead><tr></tr></thead>
                <tbody></tbody>
                <tfoot></tfoot>
            </table>
            </div>
              </form>
</body>
</html>


