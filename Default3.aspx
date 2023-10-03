<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Master_MGR.master" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
    
     <script src="fusioncharts/fusioncharts.js" type="text/javascript"></script>
   <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.11.2/css/bootstrap-select.min.css">
<link rel="stylesheet" href="css/dashboard.css">
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>

 <%-- <script type="text/javascript">
      $(document).ready(function () {
          $('#calendar').fullCalendar({
              events: "/Home/CalendarData"
          });
      }); 
   </script> --%>
        <script type="text/javascript">
            function gench(Obj, value, title) {
                var chart4 = new CanvasJS.Chart(Obj, {
                    title: {
                        text: title
                    },
                    animationEnabled: true,
                    axisX: {
                        interval: 1,
                        labelFontSize: 10,
                        lineThickness: 0
                    },
                    axisY2: {
                        valueFormatString: "0",
                        lineThickness: 0
                    },
                    toolTip: {
                        shared: true
                    },
                    legend: {
                        verticalAlign: "top",
                        horizontalAlign: "center"
                    },

                    data: [
		{
		    type: "stackedBar",
		    showInLegend: true,
		    name: "Butter (500gms)",
		    axisYType: "secondary",
		    color: "#7E8F74",
		    dataPoints: value
		},
        {
            type: "stackedBar",
            showInLegend: true,
            name: "Butter (100gms)",
            axisYType: "secondary",
            color: "#cccccc",
            dataPoints: value
        }
		]
                });
                chart4.render();
            }
</script>
<script type="text/javascript">
    function showLoader(loaderType) {
        if (loaderType == "Search1") {
            document.getElementById("loaderSearchddlSFCode").style.display = '';
        }
    }

</script>
      
    <div class="row" style="margin-bottom: 5px;">
    <div class="col-lg-8">
        <div class="col-md-4">
          <a>  <button id="Button1" type="button" runat="server" onserverclick="ExportToExcel" class="sm-st clearfix" style="width:210px;height:93px;border-bottom-width:0px;border-right-width:0px;border-color:White;">
            
   

               <a class="sm-st-icon st-red"><i class="fa fa-user"></i></a>
                <div class="sm-st-info">
             
                    <span><asp:Label ID="retailer" runat="server" ></asp:Label></span><asp:Label ID="Label2" runat="server" Font-Size="Small" ></asp:Label></div>
                    </button>
                  </a>
          
               
                     
           
        </div>
      <div class="col-md-4">
         <a>  <button id="Button2" type="button" runat="server" onserverclick="ExportToExcel2" class="sm-st clearfix" style="width:210px;height:93px;border-bottom-width:0px;border-right-width:0px;border-color:White;">
            
   

               <a class="sm-st-icon st-violet"><i class="fa fa-user"></i></a>
                <div class="sm-st-info">
             
                    <span><asp:Label ID="Dist_cou" runat="server" ></asp:Label></span><asp:Label ID="Label3" runat="server" Font-Size="Small" ></asp:Label></div>
                    </button>
                  </a>

        
        </div>
        <div class="col-md-4">
     
            <a>  <button id="Btn_order" type="button" runat="server" onserverclick="Submit_Click" class="sm-st clearfix" style="width:210px;height:93px;border-bottom-width:0px;border-right-width:0px;border-color:White;">
            
   

               <a class="sm-st-icon st-blue"><i class="glyphicon glyphicon-briefcase"></i></a>
                <div class="sm-st-info">
             
                    <span><asp:Label ID="ordercount" runat="server" ></asp:Label></span><asp:Label ID="Order_val" runat="server" Font-Size="Small" ></asp:Label></div>
                    </button>
                  </a>
                   
            </div>
         <div class="row" style="padding-bottom:100px;margin-left:-15px" >
        <div class="col-md-12" style="background: white;margin-left:-5px;"">
          <div class="row" style="margin-left:-5px;" >
 <header class="panel-heading" style="background-color:#19a4c6;;color:White; font-size:10px; font-family:Sans-Serif; padding-left:400px; font-weight: bolder;" >CHARTS</header>
   <br />
    <div class="row">
  <div class="col-md-12">
    <div id="ChrtPrimSec" class="Chartdown">
                    </div>
  </div>
  </div>
       <br />

    <div class="col-md-4" style="width:360px;height:200px;padding-left:1px;">
    <div style="text-align:center">
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>           
    </div>
    
    </div>
   
    <div>

    </div>
 <div class="col-md-4" style="width:20px;height:20px;">
  <div style="text-align:center">
        <asp:Literal ID="Literal2" runat="server"></asp:Literal>           
    </div>
 
                   
    </div>
  </div>
 <div class="col-md-4" style="padding-left:0px;">
   <div id="chartContainer" style="height: 20%; width: 320%;">
  
     <div id="calendar">
   </div>
   </div>
        <%--<asp:Literal ID="Literal3" runat="server"></asp:Literal>--%>
    
  </div>
 
     </ContentTemplate>
              </asp:updatepanel>
  
        </div>
        </div>
               </div>
        <div class="col-lg-4">
        <div class="col-md-12">
        <div class="sm-st clearfix">
  <asp:Label ID="feildlabl" runat="server" Text="FieldForce:" 
                Font-Names="Andalus" Font-Size="Smaller"></asp:Label> &nbsp
            <asp:DropDownList ID="ddlFieldForce" runat="server" onchange="showLoader('Search1')" class="btn btn-info dropdown-toggle"  onselectedindexchanged="ddlFieldForce_SelectedIndexChanged" AutoPostBack="false"  style="padding: 2px; font-size: 73%; color:Black; height:17px; width:217px; background-color:#DCE775;" >
            </asp:DropDownList>
        <div class="dropdown"  >
            <asp:Label ID="distname" runat="server" Text="Distributor:" 
                Font-Names="Andalus" Font-Size="Smaller"></asp:Label>
          <asp:DropDownList ID="Distributor"  class="btn btn-info dropdown-toggle" onselectedindexchanged="Distributor_SelectedIndexChanged" AutoPostBack="false" style="padding: 2px; font-size: 73%; height: 20px; width: 90px;" 


                runat="server" onchange="showLoader('Search1')" Height="15px" Width="55px"  Font-Size="Smaller" >
            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
            </asp:DropDownList> 
			 <img src="Images/ajax_loader_2.gif" style="display: none;" id="loaderSearchddlSFCode1" />
			<asp:Label ID="routename" runat="server" Text="Route:"  Font-Names="Andalus" Font-Size="Smaller"></asp:Label>
          
            <asp:DropDownList ID="route"  class="btn btn-info dropdown-toggle" style="padding: 2px; font-size: 73%; height: 20px; width: 90px;" 
                runat="server" Height="15px" Width="50px"  Font-Size="Smaller">
           
            </asp:DropDownList>
			 <img src="Images/ajax_loader_2.gif" style="display: none;" id="loaderSearchddlSFCode" />
</br>
  <asp:Label ID="Total" runat="server" Text="Total :"   Font-Names="Andalus" Font-Size="Smaller"  ForeColor="#0073b7"></asp:Label>    
            <asp:Label ID="tot_cont" runat="server"   Font-Names="Andalus" 
                Font-Size="Smaller" Width="30px"></asp:Label><asp:Label ID="Productive" runat="server" Text="Productive :"   ForeColor="#0073b7"  Font-Names="Andalus" Font-Size="Smaller"></asp:Label><asp:Label ID="productive_value" runat="server" Width="30px"   Font-Names="Andalus" Font-Size="Smaller"></asp:Label><asp:Label ID="nonprod" runat="server" Text="Non-productive:"  ForeColor="#0073b7"  Font-Names="Andalus" Font-Size="Smaller"></asp:Label><asp:Label ID="Non_prodt_val" runat="server" Width="30px"  Font-Names="Andalus" Font-Size="Smaller"></asp:Label>

 
  </div>
   
 </br>
  <div id="prodt" class="Chartpie">
<div class="loading" align="center">
           
            <br />
            <img src="Images/Loading.gif" alt="" />
 			Loading. Please wait.<br />
        </div>
                    </div>
</div>
  <div></div>
   <asp:HiddenField ID="caption" runat="server" />
    <asp:HiddenField ID="subcaption" runat="server" />
    <asp:HiddenField ID="numberprefix" runat="server" />
    <asp:HiddenField ID="showvalues" runat="server" />
    <asp:HiddenField ID="bgcolor" runat="server" />
    <asp:HiddenField ID="xaxisname" runat="server" />
    <asp:HiddenField ID="plotgradientcolor" runat="server" />
     <asp:HiddenField ID="showalternatehgridcolor" runat="server" />
     <asp:HiddenField ID="showplotborder" runat="server" />
       <asp:HiddenField ID="divlinecolor" runat="server" />
     <asp:HiddenField ID="canvasborderalpha" runat="server" />
    </div></div>
       
            <section class="panel">
                             
    <div>
<script type="text/javascript">

            $('#<%=ddlFieldForce.ClientID%>').change(function () {
                var discode = document.getElementById("<%=ddlFieldForce.ClientID %>");
                var selectValue = discode.value;
 var obj = "prodt";
                        var title = "";
                        var da="";
                   gen(obj ,da, title);

                $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "Default2.aspx/filldistr",
                    data:JSON.stringify({selectValue}),
                    dataType: "json",
                    success: function (data) {
                        $('#<%=Distributor.ClientID%>').empty();
                        $('#<%=Distributor.ClientID%>').append("<option value='0'>---Select----</option>");
                        $.each(data.d, function (key, value) {
                            $('#<%=Distributor.ClientID%>').append($("<option></option>").val(value.stockistCode).html(value.stockistname));
                        });
                    },
                    error: function ajaxError(result) {
                        alert(result.status + ' : ' + result.statusText);
                    }
                });
            });
    </script>
        <script type="text/javascript">

            $('#<%=Distributor.ClientID%>').change(function () {
                var discode = document.getElementById("<%=Distributor.ClientID %>");
                var selectValue = discode.value;
//                alert(selectValue);
                $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "Default2.aspx/fillrou",
                    data:JSON.stringify({selectValue}),
                    dataType: "json",
                    success: function (data) {
                        $('#<%=route.ClientID%>').empty();
                        $('#<%=route.ClientID%>').append("<option value='0'>---Select----</option>");
                        $.each(data.d, function (key, value) {
                            $('#<%=route.ClientID%>').append($("<option></option>").val(value.TerritoryCode).html(value.TerritoryName));
                        });
                    },
                    error: function ajaxError(result) {
                        alert(result.status + ' : ' + result.statusText);
                    }
                });
            });
    </script>
       
        <script type="text/javascript">
//            $('#ddlDivisionName').change(function () {

            $('#<%=route.ClientID%>').change(function () {
                //                var is = $('#Distributor :selected').text();
               
                var distcode = document.getElementById("<%=Distributor.ClientID %>");
                var selectedValue = distcode.value;
                 var routecode = document.getElementById("<%=route.ClientID %>");
                var routevalue = routecode.value;
//                alert(selectedValue);
//                alert(routevalue);
                $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "Default2.aspx/populatedropdownlist",                
                    dataType: "json",
                    data: JSON.stringify({selectedValue,routevalue}),
                    success: function (data) {

                        var obj = "prodt";
                        var title = "";
                        document.getElementById("ctl00_ContentPlaceHolder1_tot_cont").innerText=data.d[1];
                          document.getElementById("ctl00_ContentPlaceHolder1_productive_value").innerText=data.d[2];
                            document.getElementById("ctl00_ContentPlaceHolder1_Non_prodt_val").innerText=data.d[3];
                      
                            sData = JSON.parse(data.d[0]); console.log(sData);
                            gen(obj, sData, title);


                        
                    },
                    error: function ajaxError(data) {
                        alert(data.status + ' : ' + data.statusText);
                    }
                });
            });
//        });
//        });
        </script>
       
        <script type="text/javascript">
            function gen1(Obj, value, title) {

                var charts1 = new CanvasJS.Chart(Obj, {

                    theme: "theme3",
                    axisX: {
                        labelFormatter: function (e) {
                            return " ";
                        },
                        tickLength: 0
					, lineThickness: 0
                        //,margin:-6
                    },
                    axisY: {

                        valueFormatString: " ",
                        tickLength: 0,
                        lineThickness: 0
                    },
                    title: {
                        text: title
                    },

                    animationEnabled: true,
                    data: [{
                        //                        color: "LightSeaGreen","RoyalBlue",
                        indexLabelPlacement: "inside",
                        showInLegend: true,
                        type: "bar",       // Change type to "bar", "area", "spline", "pie",etc.
                        dataPoints: value

                    }]
                });
                charts1.render();



            }

            function gen(Obj, value, title) {

                var charts = new CanvasJS.Chart(Obj, {

                    theme: "theme3",
                    axisX: {
                        labelFormatter: function (e) {
                            return " ";
                        },
                        tickLength: 0
					, lineThickness: 0
                        //,margin:-6
                    },
                    axisY: {

                        valueFormatString: " ",
                        tickLength: 0,
                        lineThickness: 0
                    },
                    title: {
                        text: title
                    },

                    animationEnabled: true,
                    data: [{
                        //                        color: "LightSeaGreen","RoyalBlue",
                        indexLabelPlacement: "inside",
                        showInLegend: true,
                        type: "pie",       // Change type to "bar", "area", "spline", "pie",etc.
                        dataPoints: value

                    }]
                });
                charts.render();



            }
            function genChart(Obj, arrDta, title) {
                var chart = new CanvasJS.Chart(Obj, {
                    theme: "theme3",
                    axisX: {
                        labelFormatter: function (e) {
                            return " ";
                        },
                        tickLength: 0
					, lineThickness: 0
                        //,margin:-6
                    },
                    axisY: {

                        valueFormatString: " ",
                        tickLength: 0,
                        lineThickness: 0
                    },
                    title: {
                        text: title
                    },
                    animationEnabled: true,
                    data: [{
                        type: "column",
                        toolTipContent: "<a href = {name}> {label}</a><hr/>Views: {y}",          // Change type to "bar", "area", "spline", "pie",etc.
                        dataPoints: arrDta
                    }]
                });
                chart.render();



            }
            function genChart1(Obj, arrDta, arr1, title) {
                console.log(arrDta);
                var chart1 = new CanvasJS.Chart(Obj, {
                    theme: "theme2", //theme1
                    title: {
                        text: title
                    },
                    animationEnabled: true,   // change to true
                    data: [{
                        type: "splineArea",       // Change type to "bar", "area", "spline", "pie",etc.
                        dataPoints: arrDta,
                        yValueFormatString: "#,##,##,###.00",
                        color: "rgba(38, 185, 154, 0.38)"
                    }, {
                        type: "splineArea",       // Change type to "bar", "area", "spline", "pie",etc.
                        dataPoints: arr1,
                        yValueFormatString: "#,##,##,###.00",
                        color: "rgba(3, 88, 106, 0.38)"
                    }]
                });
                chart1.render();

            }

        </script>
        
        <script src="js/canvasjs.min.js" type="text/javascript"></script>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>

     </section>
         <div>
   
     <div class="col-md-4">
      <header class="panel-heading" style="background-color:#ff865c;color:White; font-size:14px; font-family:Sans-Serif" >
                              <center>Annual Channal Summary Overview</center>    
                                </header>
    
   
            <div class="sm-st clearfix" style="overflow:scroll; height:200px;">
        
 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
            GridLines="None" Width="10%"
            AllowSorting="True" DataKeyNames="Doc_Special_Name" 
            ShowHeaderWhenEmpty="True" >
            <Columns>
                <asp:BoundField DataField="Doc_Special_Name" HeaderText="ChannalName" ItemStyle-Width="10px" ItemStyle-Font-Size="X-Small"/>
              
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="container">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="progress">
                                        <div   id="div1" class="progress-bar" role="progressbar" aria-valuenow="<%# Eval("Spec_Count")%>" aria-valuemin="0"
                                            aria-valuemax="100" style="width:<%# Convert.ToInt16((Convert.ToDouble(Eval("TVst")) / Convert.ToDouble(Eval("Spec_Count")))*100)%>%;" >
                                        </div>
                                        <span class="progress-type" style="display:block;height:100%;top:-1px;padding:0px 0px 23px 0px;width:<%# Convert.ToInt16((Convert.ToDouble(Eval("PVst")) / Convert.ToDouble(Eval("Spec_Count")))*100)%>%;overflow:hidden"></span> 
                                        <span class="Status"><%# Eval("PVst")%> (<%# Convert.ToInt16((Convert.ToDouble(Eval("PVst")) / Convert.ToDouble(Eval("Spec_Count")))*100)%>% )  ( Rs.<%# Eval("Pob")%>)   | <font color="green"><%# Eval("TVst")%>  ( <%# Convert.ToInt16((Convert.ToDouble(Eval("TVst")) / Convert.ToDouble(Eval("Spec_Count")))*100)%>% ) </font></span> 
                                        

                                    </div>
  									<span style="font-size:xx-small;padding-left:200px;font-weight:bold;"><%# Eval("Spec_Count")%> </span>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
              
            </Columns>
        </asp:GridView>
         </div>
      </div>
          
               
    </div>  
      
        <div class="col-lg-4">
            <div class="col-lg-4">
            <div class="slimScrollBar" >
            <section class="panel">
                                <header class="panel-heading" style="background-color:#ff865c;color:White; font-size:14px; font-family:Sans-Serif" >
                                    Notice Board
                                </header>
                               <div class="panel-body" id="noti-box">
                               
                           

                                   <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" CssClass="table"    OnItemDataBound="Item_Bound" >
        <ItemTemplate>
        <div class="desc">
        <asp:Label ID="lblInput" runat="server" Width="255px" style="padding-top:10px;padding-left:5px;"  Text='<%# Eval("comment") %>'></asp:Label><br/>
                      	<div class="thumb" style="height:40px;padding-top:15px;padding-left:117px;">
                      		<span class="badge bg-theme" style="background-color:transparent;"></span>
<asp:Label ID="daytime" runat="server" Text='<%# Eval("timee") %>' style="font-style:bold;font-size:10px;color:#a8b0b3;" ></asp:Label><i class="fa fa-clock-o" style="color:#a6aeb1;"></i>
                          
                      	</div>
                      
                      </div>
                      <asp:Label ID="cmttype" runat="server" Text='<%# Eval("Comment_Type") %>' Visible="false"></asp:Label>
        </ItemTemplate>
        </asp:DataList>
          </div>
                   

                                
                            
        </div>

</ContentTemplate>
   </asp:UpdatePanel>
   </form>
   
     
    <script type="text/javascript">
            $(function () {
                "use strict";
                //BAR CHART
                var data = {
                    labels: ["January", "February", "March", "April", "May", "June", "July"],
                    datasets: [
                        {
                            label: "My First dataset",
                            fillColor: "rgba(220,220,220,0.2)",
               {
                            label: "My First dataset",
                            fillColor: "rgba(220,220,220,0.2)",
                            strokeColor: "rgba(220,220,220,1)",
                            pointColor: "rgba(220,220,220,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(220,220,220,1)",
                            data: [65, 59, 80, 81, 56, 55, 40]
                        },
                        {
                            label: "My Second dataset",
                            fillColor: "rgba(151,187,205,0.2)",
                            strokeColor: "rgba(151,187,205,1)",
                            pointColor: "rgba(151,187,205,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(151,187,205,1)",
                            data: [28, 48, 40, 19, 86, 27, 90]
                        }
                    ]
                };
                new Chart(document.getElementById("splineAreachart").getContext("2d")).Spline(data, {
                    responsive: true,
                    maintainAspectRatio: false,
                });

            });
        
    </script>
  
</asp:Content>
