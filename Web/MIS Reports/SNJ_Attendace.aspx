<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SNJ_Attendace.aspx.cs" Inherits="MIS_Reports_SNJ_Attendace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Attendance View</title>
    
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <style type="text/css">
         input[type='text'], select, label
        {
            line-height: 22px;
            padding: 0px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        let opwind;
        function NewWindow() {
            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
            if (subdiv == "--- Select ---") { alert("Select Subdivision."); $('#subdiv').focus(); return false; }
            var FO = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (FO == "--Select--") { alert("Select salesforce"); $('#salesforcelist').focus(); return false; }
            var sfcode = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var Fdt = $('#fdate').val();
            var Tdt = $('#tdate').val();
            if (Fdt == 0) { alert("Select From Date."); $('#ddlcntry').focus(); return false; }
            if (Tdt == 0) { alert("Select To Date."); $('#ddlcntry').focus(); return false; }
            var date1 = new Date(Fdt);
            var date2 = new Date(Tdt);
            var daysBetween = []; var days = [];
            var currentDate = date1; var timeDifference = date2 - date1;
            var numDays = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
            while (currentDate <= date2) {
                daysBetween.push(currentDate.toISOString().split('T')[0]);
                days.push(currentDate.getDate());
                currentDate.setDate(currentDate.getDate() + 1);
            }

            console.log(days);
            console.log(daysBetween);
            //console.log(`Number of days between ${dateStr1} and ${dateStr2}: ${numDays} days`);

            function hasDuplicates(array) {
                var valuesSoFar = Object.create(null);
                for (var i = 0; i < array.length; ++i) {
                    var value = array[i];
                    if (value in valuesSoFar) {
                        return true;
                    }
                    valuesSoFar[value] = true;
                }
                return false;
            }
            
            if (!hasDuplicates(days)) {
                window.open("rpt_SNJ_Attendace.aspx?&FDate=" + Fdt + "&TDate=" + Tdt + "&subdiv=" + subdiv + "&sfCode=" + sfcode + "&sfname=" + FO, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            }
            else {
                //opwind.Close();
                alert("Can't Select this DateRange.");
            }

            
        }
</script>
    </head>
<body>
     <form id="form1" runat="server">
         <div class="container" style="width:100%">
                <div class="row">
            <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                Division</label>
                  <div class="col-md-3 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" 
                             onselectedindexchanged="subdiv_SelectedIndexChanged" Width="200px"
                       AutoPostBack="true" >
                        </asp:DropDownList>
                    </div>
</div>
</div>
             <div class="row">
                            <label id="st" class="col-md-2  col-md-offset-3  control-label">
                                State</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectIndexchanged" AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
              <div class="row">
            <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                Field Force</label>
                  <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                   <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control"  AutoPostBack="true"
                       Width="360px" >   </asp:DropDownList>
               
                    </div>
</div>
</div>
              <div class="row">
            <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                From Date</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                    <input id="fdate" name="txtFrom" type="date" class="form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                tabindex="1" skinid="MandTxtBox" />
                   </div>
                    </div>
                    </div>
            <div class="row">
            <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                To Date</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                    <input id="tdate" name="txtFrom" type="date" class="form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                tabindex="1" skinid="MandTxtBox" />
                   </div>
                    </div>
                    </div>
             <div class="row">
            <div class="col-md-6  col-md-offset-5">

            <button id="btnGo"  runat="server" onclick="NewWindow().this" class="btn btn-primary btnview" style="width: 100px">
                <span>View</span></button>
<asp:Label ID="lblpath" runat="server" ForeColor="#f1f2f7"  ></asp:Label>
 </div>
                 </div>
             </div>
          </form>
</body>
</html>
</asp:Content>

