<%@  Language="C#"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="rptorderwithsale.aspx.cs" Inherits="MIS_Reports_rptorderwithsale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Order With Sale Report</title>
  <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />

         <script language="Javascript">
         function RefreshParent() {
             // window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
</head>
     <script  type="text/javascript" src="../JsFiles/canvasjs.min.js"></script>
    
	<script type="text/javascript">

	    function genChart(arrDta) {

	        var chart = new CanvasJS.Chart("chartContainer", {
	            theme: "theme2", //theme1
	            title: {
	            //text: ""
	        },
	        animationEnabled: true,   // change to true
	        data: [{
	            type: "pie",
	            indexLabelPlacement: "inside",    // Change type to "bar", "area", "spline", "pie",etc.
	            dataPoints: arrDta
	        }]
	    });
	    chart.render();
	}



	function genChart1(arrDta,arrdta1) {

	    var chart = new CanvasJS.Chart("barchart", {
	        theme: "theme2", //theme1
	        title: {
	        //text: ""
	    },
//	    axisX: {

//	        interval: 50,
//            labelFontSize: 8,
//            labelFontColor: "black",
////           labelAutoFit: true
//         },
	    axisX: {
	       
	        labelAngle: 30, 
            labelFontSize: 8,
        labelFontColor: "black",
	    },
//        truncateLabel: function(label) {
//    var xSubstring = label.substring(0, this.labelLength);
//    if(xSubstring.length < this.labelLength) {
//        return xSubstring;  
//    } else {
//        // Check if a word is cut off
//        if ( ' ' != label.charAt( (this.labelLength-1) ) && ' ' != label.charAt( this.labelLength ) ) {
//            // If so, cut the label off at the last space instead of mid-word
//            var last_space_pos = label.lastIndexOf(" ", this.labelLength);
//            last_space_pos = (0 > last_space_pos)? this.labelLength: last_space_pos;
//            xSubstring = label.substring(0, last_space_pos);
//        }
//        return xSubstring+"...";
//    }
//},
//        options:{
//    scales: {
//        xAxes: [{
//            ticks: {
//                callback: function(value) {
//                    return value.substr(0, 10);//truncate
//                },
//            }
//        }],
//        yAxes: [{}]
//    },
//	    axisX: {
//	        labelFontSize: 8,
//	        labelFontColor: "black",
//            scaleFontSize: 10
//	    },
	    animationEnabled: true,   // change to true
//	    data: [{
//	        type: "bar",
//	        indexLabelPlacement: "inside",    // Change type to "bar", "area", "spline", "pie",etc.
//	        dataPoints: arrDta

//	    }]
        data: [{
            type: "column",
//            indexLabelPlacement: "inside",
//            indexLabelOrientation: "vertical",
//            indexLabel: "{x}",
//            indexLabelOrientation: "vertical",
            // Change type to "bar", "area", "spline", "pie",etc.
                dataPoints: arrDta
            },{
                type: "column", 
//                indexLabelOrientation: "vertical",
//                indexLabelPlacement: "inside",
//                indexLabelOrientation: "vertical",
//                indexLabel: "{x}",       // Change type to "bar", "area", "spline", "pie",etc.
                dataPoints: arrdta1
            }]
	});
	chart.render();
}


    
    </script>

<body>
    <form id="form1" runat="server">
   <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
            <td width="20%"></td><td></td>
                <td width="80%" align="center" >
                <asp:Label ID="lblProd" runat="server" Text="Order With Sale Report" SkinID="lblMand" ></asp:Label>
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPrint_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnExcel_Click" />
                            </td>
<td> <asp:Button ID="btnExport" runat="server" Text="PDF"  Font-Names="Verdana" Font-Size="10px"  BorderWidth="1" Height="25px" Width="60px"
                                        BorderColor="Black" BorderStyle="Solid" onclick="btnExport_Click" /></td>

                           
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    
  <br> </br>

    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table width="100%" align="center">
            <tbody>
            <tr>
          
              <td align="center" >
               <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="FieldForce Name:" Font-Bold="true"  Font-Underline="true" ></asp:Label>
               <asp:Label ID="lblname" runat="server" SkinID="lblMand"></asp:Label>
              </td>
              
            </tr>
               
                                                                                                                    
                         
            </tbody>
        </table>
     
      <table width="100%">
      <tr><td width="75%" style="padding-left:200px; padding-right:90px;"><div id="barchart" style="height:300px; width:100%;"></div></td><td  width="55%" ><div id="chartContainer" style="height: 350px; width:45%;"></div>
          </td></tr>
      </table>
      <center >
           
          
        <table width="100%" align="center">
                <tbody>
                    <tr>

                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="70%" >
                            </asp:Table>
                        </td>
                    </tr>
                </tbody>
            </table>  
       
        </center>
  </asp:Panel>
    </form>
</body>
</html>



