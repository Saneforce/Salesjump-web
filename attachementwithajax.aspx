<%@ Page Title="" Language="C#" MasterPageFile="~/mail.master" AutoEventWireup="true" CodeFile="attachementwithajax.aspx.cs" Inherits="attachementwithajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="css/Styles_MasterPage.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
 <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<%--<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>--%>
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>
   <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>
 <script type="text/javascript">
     $('select').each(function () {
         var $this = $(this), numberOfOptions = $(this).children('option').length;

         $this.addClass('select-hidden');
         $this.wrap('<div class="select"></div>');
         $this.after('<div class="select-styled"></div>');

         var $styledSelect = $this.next('div.select-styled');
         $styledSelect.text($this.children('option').eq(0).text());

         var $list = $('<ul />', {
             'class': 'select-options'
         }).insertAfter($styledSelect);

         for (var i = 0; i < numberOfOptions; i++) {
             $('<li />', {
                 text: $this.children('option').eq(i).text(),
                 rel: $this.children('option').eq(i).val()
             }).appendTo($list);
         }

         var $listItems = $list.children('li');

         $styledSelect.click(function (e) {
             e.stopPropagation();
             $('div.select-styled.active').not(this).each(function () {
                 $(this).removeClass('active').next('ul.select-options').hide();
             });
             $(this).toggleClass('active').next('ul.select-options').toggle();
         });

         $listItems.click(function (e) {
             e.stopPropagation();
             $styledSelect.text($(this).text()).removeClass('active');
             $this.val($(this).attr('rel'));
             $list.hide();
             //console.log($this.val());
         });

         $(document).click(function () {
             $styledSelect.removeClass('active');
             $list.hide();
         });

     });
 </script>
 <style type="text/css">
 .notification-label.notification-label-red {
  background: #00c0ef;
}
.notification-label.notification-label-blue {
  background: #4285f4;
}
  
.btn:hover {
  -webkit-transform: scale(1.05);
          transform: scale(1.05);
}
.btn .notification-label {
  position: absolute;
  top: -17px;
    right: -85px;

}

.notification-label {
  border-radius: 50%;
  width: 37px;
    height: 34px;
    line-height: 15px;
  color: #fff;
  font-size: 1.5rem;
  -webkit-animation: bounceIn 700ms;
          animation: bounceIn 700ms;
}


@-webkit-keyframes appear {
  0% {
    -webkit-transform: scale(0.7);
            transform: scale(0.7);
    opacity: 0;
  }
  100% {
    -webkit-transform: scale(1);
            transform: scale(1);
    opacity: 1;
  }
}

@keyframes appear {
  0% {
    -webkit-transform: scale(0.7);
            transform: scale(0.7);
    opacity: 0;
  }
  100% {
    -webkit-transform: scale(1);
            transform: scale(1);
    opacity: 1;
  }
}
@-webkit-keyframes bounceIn {
  0%,
  20%,
  40%,
  60%,
  80%,
  100% {
    -webkit-transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
            transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
  }
  0% {
    -webkit-transform: scale3d(0.3, 0.3, 0.3);
            transform: scale3d(0.3, 0.3, 0.3);
  }
  20% {
    -webkit-transform: scale3d(1.1, 1.1, 1.1);
            transform: scale3d(1.1, 1.1, 1.1);
  }
  40% {
    -webkit-transform: scale3d(0.9, 0.9, 0.9);
            transform: scale3d(0.9, 0.9, 0.9);
  }
  60% {
    -webkit-transform: scale3d(1.03, 1.03, 1.03);
            transform: scale3d(1.03, 1.03, 1.03);
  }
  80% {
    -webkit-transform: scale3d(0.97, 0.97, 0.97);
            transform: scale3d(0.97, 0.97, 0.97);
  }
  100% {
    -webkit-transform: scale3d(1, 1, 1);
            transform: scale3d(1, 1, 1);
  }
}
@keyframes bounceIn {
  0%,
  20%,
  40%,
  60%,
  80%,
  100% {
    -webkit-transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
            transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
  }
  0% {
    -webkit-transform: scale3d(0.3, 0.3, 0.3);
            transform: scale3d(0.3, 0.3, 0.3);
  }
  20% {
    -webkit-transform: scale3d(1.1, 1.1, 1.1);
            transform: scale3d(1.1, 1.1, 1.1);
  }
  40% {
    -webkit-transform: scale3d(0.9, 0.9, 0.9);
            transform: scale3d(0.9, 0.9, 0.9);
  }
  60% {
    -webkit-transform: scale3d(1.03, 1.03, 1.03);
            transform: scale3d(1.03, 1.03, 1.03);
  }
  80% {
    -webkit-transform: scale3d(0.97, 0.97, 0.97);
            transform: scale3d(0.97, 0.97, 0.97);
  }
  100% {
    -webkit-transform: scale3d(1, 1, 1);
            transform: scale3d(1, 1, 1);
  }
}

 </style>
 <script type="text/javascript">
   $(function () {
     var context = document.getElementsByTagName('canvas')[0].getContext('2d');

     var hue = 0;

     function bgcolor() {
         hue = hue + Math.random() * 3;
         context.fillStyle = 'hsl(' + hue + ', 100%, 50%)';
         context.fillRect(0, 0, context.canvas.width, context.canvas.height);
     }

     setInterval(bgcolor, 20);
 });
 </script>
 <script type="text/javascript">
     function enter(flag) {
//     $('.delrow').click(function () {
//        $(this).parent().remove(); //Deleting the Row (tr) Element 
         //         });
         //         var $this = $(this);
         //         document.getElementById("#fdata").deleteRow(0);
      
         $(this).parent('tr').remove();
//      var rowe = $(this).closest("tr");
        // $(this).closest('tr').remove();
         //         alert(rowe);
//                 rowe.remove();
         //      
        
     };
</script>
<style type="text/css">
    #page { height:50px; width:250px;border-radius: 8px;}
div.gradient{ position: absolute; width:250px; height:50px ;border-radius: 8px;border-style: groove;border-color: #f5f5f5;
  
    background: 
    radial-gradient(80% 10%, circle, rgba(191, 62, 35, .5), transparent),
    radial-gradient(80% 50%, circle, rgba(237, 192, 97, .4), transparent),
    radial-gradient(20% 80%, 40em 40em, rgba(108, 70, 34 , .5), transparent),
    radial-gradient(10% 10%, circle, rgba(196, 137, 118, .7), transparent);  
  
  background: 
    -webkit-radial-gradient(80% 10%, circle, rgba(191, 62, 35, .5), transparent),
    -webkit-radial-gradient(80% 50%, circle, rgba(237, 192, 97, .4), transparent),
    -webkit-radial-gradient(20% 80%, 40em 40em, rgba(108, 70, 34 , .5), transparent),
    -webkit-radial-gradient(10% 10%, circle, rgba(196, 137, 118, .7), transparent);

    background: 
    -moz-radial-gradient(80% 10%, circle, rgba(191, 62, 35, .5), transparent),
    -moz-radial-gradient(80% 50%, circle, rgba(237, 192, 97, .4), transparent),
    -moz-radial-gradient(20% 80%, 40em 40em, rgba(108, 70, 34 , .5), transparent),
    -moz-radial-gradient(10% 10%, circle, rgba(196, 137, 118, .7), transparent);
  

        }
p a {
  text-transform: uppercase;
  text-decoration: none;
  display: inline-block;
  color: #fff;
  padding: 5px 10px;
  margin: 0 5px;
  background-color: #b83729;
  -moz-transition: all 0.2s ease-in;
  -o-transition: all 0.2s ease-in;
  -webkit-transition: all 0.2s ease-in;
  transition: all 0.2s ease-in;
}
p a:hover {
  background-color: #ab3326;
}

.select-hidden {
  display: none;
  visibility: hidden;
  padding-right: 10px;
}

.select {
  cursor: pointer;
  display: inline-block;
  position: relative;
  font-size: 16px;
  font-family:Pristina;
  background: #fff;
  padding: 0px 4px;
  width:100px;
  height:20px;
 margin-left: 10px;
    margin-top: -2px;
}

.select-styled {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background-color: #c0392b;
  padding: 8px 15px;
  -moz-transition: all 0.2s ease-in;
  -o-transition: all 0.2s ease-in;
  -webkit-transition: all 0.2s ease-in;
  transition: all 0.2s ease-in;
}
.select-styled:after {
  content: "";
  width: 0;
  height: 0;
  border: 7px solid transparent;
  border-color: #fff transparent transparent transparent;
  position: absolute;
  top: 16px;
  right: 10px;
}
.select-styled:hover {
  background-color: #b83729;
}
.select-styled:active, .select-styled.active {
  background-color: #ab3326;
}
.select-styled:active:after, .select-styled.active:after {
  top: 9px;
  border-color: transparent transparent #fff transparent;
}

.select-options {
  display: none;
  position: absolute;
  top: 100%;
  right: 0;
  left: 0;
  z-index: 999;
  margin: 0;
  padding: 0;
  list-style: none;
  background-color: #ab3326;
}
.select-options li {
  margin: 0;
  padding: 12px 0;
  text-indent: 15px;
  border-top: 1px solid #962d22;
  -moz-transition: all 0.15s ease-in;
  -o-transition: all 0.15s ease-in;
  -webkit-transition: all 0.15s ease-in;
  transition: all 0.15s ease-in;
}
.select-options li:hover {
  color: #c0392b;
  background: #fff;
}
.select-options li[rel="hide"] {
  display: none;
}
</style>
<%--<script type="text/javascript">
    $(function () {
        //        $("[id*=ctl00_ContentPlaceHolder1_GridView1] td").click(function () {
        $(document).on('click', "#ctl00_ContentPlaceHolder1_GridViewfrd", function () {
            alert('ff');
            $(this).closest('tr').remove();
            var gv = document.getElementById("<%=imgBtnDell.ClientID%>");
            alert(gv);
        });

        //    $('#ff').on('click', function () {
        //        $(this).closest('tr').remove();
    });
</script>--%>
<script type="text/javascript">

     function ConfirmDelete() {
         var gv = document.getElementById("<%=GridView1.ClientID%>");
         var chk = gv.getElementsByTagName("input");

//          for(var i=0;i<chk.length;i++)
//       {
//         
//         if (chk[i].checked) {

             $('table tr td :checkbox:checked').map(function () {
                    var $this = $(this);
                     
                   $this.closest('tr').each(function () {
                    var hdn = $this.closest('tr').find('input[type="hidden"]').val();
                                    

//             alert("ggg");
//              var row = $(chk[i]).closest("tr");
////               var $this = $(this);
////                $this.closest('tr').each(function () {
//                    tran = row.find("input[type=hidden]").val();
////                    });
             
//               v=JSON.stringify({translno});
//              alert(v);
//               row.remove();
//               i=0;
               
             selectValue=hdn;
            
              $.ajax({

                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "attachementwithajax.aspx/filldelete",
                    data:JSON.stringify({selectValue}),
                    dataType: "json",
                    success: function (r) {
                    if (r.d) {
                     
                     for(var i=0;i<chk.length;i++)
       {
         
         if (chk[i].checked) {

          

//             alert("ggg");
              var rowe= $(chk[i]).closest("tr");
               rowe.remove();
                alert("Customer record has been deleted sucessfully.");
               i=0;}} 
                                 
                        if ($("[id*=GridView1] td").length == 0) {
                            $("[id*=GridView1] tbody").append("<tr><td colspan = '4' align = 'center'>No records found.</td></tr>")
                        }
                                              
                    }
                }
                    ,
                    error: function ajaxError(result) {
                        alert("failure");
                    }
                });

});
});
//       }
//               }
         return false;
        
     }
</script>
	<script src="http://code.jquery.com/jquery-latest.js"></script>
	
    <script type="text/javascript">
        function Bccaddress() {

            var lnktext = document.getElementById('<%= LinkButton6.ClientID %>').innerText;
            if (lnktext == 'Add Bcc') {
                $(document).ready(function () {

                    $('#DivFree').show();
                    document.getElementById('<%= LinkButton6.ClientID %>').innerText = 'Remove Bcc';
                });
            }
            else {
                $(document).ready(function () {

                    $('#DivFree').hide();
                    document.getElementById('<%= LinkButton6.ClientID %>').innerText = 'Add Bcc';
                });

            }


            return false
        }
        function ccaddress() {

            var lnktext1 = document.getElementById('<%= LinkButton5.ClientID %>').innerText;
            if (lnktext1 == 'Add Cc') {
                $(document).ready(function () {

                    $('#DivPaid').show();
                    document.getElementById('<%= LinkButton5.ClientID %>').innerText = 'Remove Cc';
                });
            }
            else {
                $(document).ready(function () {

                    $('#DivPaid').hide();
                    document.getElementById('<%= LinkButton5.ClientID %>').innerText = 'Add Cc';
                });
            }


            return false
        }
</script>


<script type="text/javascript">
    function Bccaddressfrd() {

        var lnktext = document.getElementById('<%= LinkButton8.ClientID %>').innerText;
        if (lnktext == 'Add Bcc') {
            $(document).ready(function () {

                $('#divbcc').show();
                document.getElementById('<%= LinkButton8.ClientID %>').innerText = 'Remove Bcc';
            });
        }
        else {
            $(document).ready(function () {

                $('#divbcc').hide();
                document.getElementById('<%= LinkButton8.ClientID %>').innerText = 'Add Bcc';
            });

        }


        return false
    }
    function ccaddressfrd() {

        var lnktext1 = document.getElementById('<%= LinkButton7.ClientID %>').innerText;
        if (lnktext1 == 'Add Cc') {
            $(document).ready(function () {

                $('#divcc').show();
                document.getElementById('<%= LinkButton7.ClientID %>').innerText = 'Remove Cc';
            });
        }
        else {
            $(document).ready(function () {

                $('#divcc').hide();
                document.getElementById('<%= LinkButton7.ClientID %>').innerText = 'Add Cc';
            });
        }


        return false
    }
</script>
<script type="text/javascript">
    $(function () {
        $("[id*=ctl00_ContentPlaceHolder1_composesend]").click(function () {

            var at='';
           
            var cfieldforce = $.trim($("#ctl00_ContentPlaceHolder1_txtAddr").val());
            var combcc = $.trim($("#bcctextbox").val());
            var comcc = $.trim($("#cctextbox").val());
            var comsub = $.trim($("#ctl00_ContentPlaceHolder1_subject").val());
            var commsg = $.trim($("#ctl00_ContentPlaceHolder1_message1").val());
            //            var cfieldforce = document.getElementById('#ctl00_ContentPlaceHolder1_txtAddr').innerHTML;


            var fatch = document.getElementById("dvFileName").innerHTML;
            var file = $get('ctl00_ContentPlaceHolder1_gvNewFiles').getElementsByTagName('a');
            for (var t = 0; t < file.length; t++) {
                //                at += file[t].innerHTML + "'";
              

                at += file[t].innerHTML + ',';
                alert(at);
                //              if (file[t].innerHTML == selectedFile[selectedFile.length - 1]) {
                //                  return file[t].innerHTML;
                //              }
            }

            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                url: "attachementwithajax.aspx/composedetails",
                data: JSON.stringify({ cfeildfrce: cfieldforce, cbcc: combcc, ccc: comcc, csubj: comsub, cmessage: commsg, cattach: at }),
                dataType: "json",
                success: function (data) {

                },
                error: function ajaxError(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });


            alert("Customer record has been sent sucessfully.");


        });
    });
</script>

<script type="text/javascript">
    $(document).on("click", "#grdcc", function (e) {
        var slvals = '';
        //    $("[id*=grdadd] td").click(function () {
        $('#grdcc  tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                v = $(this).text();
                //                alert(v);
                slvals += v + ",";
                //                            var string_length = v.length;

                //                            alert(v);


            });



        });

        //        alert(checkedVals.join(","));

        //        alert(slvals);
        document.getElementById('cctextbox').value = slvals;

        //            });
    });
</script>
<script type="text/javascript">
    $(document).on("click", "#frdcc", function (e) {
        var slvalsfrd = '';
        //    $("[id*=grdadd] td").click(function () {
        $('#frdcc  tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                vfrd = $(this).text();
                //                alert(v);
                slvalsfrd += vfrd + ",";
                //                            var string_length = v.length;

                //                            alert(v);


            });



        });

        //        alert(checkedVals.join(","));

        //        alert(slvals);
        document.getElementById('cctextboxfrd').value = slvalsfrd;

        //            });
    });
</script>
<script type="text/javascript">
    $(document).on("click", "#grdbcc", function (e) {
        var slvals = '';
        //    $("[id*=grdadd] td").click(function () {
        $('#grdbcc  tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                v = $(this).text();
                //                alert(v);
                slvals += v + ",";
                //                            var string_length = v.length;

                //                            alert(v);


            });



        });

        //        alert(checkedVals.join(","));

        //        alert(slvals);
        document.getElementById('bcctextbox').value = slvals;

        //            });
    });
</script>
<script type="text/javascript">
    $(document).on("click", "#frdbcc", function (e) {
        var slvalsbfrd = '';
        //    $("[id*=grdadd] td").click(function () {
        $('#frdbcc  tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                bv = $(this).text();
                //                alert(v);
                slvalsbfrd += bv + ",";
                //                            var string_length = v.length;

                //                            alert(v);


            });



        });

        //        alert(checkedVals.join(","));

        //        alert(slvals);
        document.getElementById('bcctextboxfrd').value = slvalsbfrd;

        //            });
    });
</script>
<script type="text/javascript">
    $(document).on("click", "#grdadd", function (e) {
        var slvals = '';
        //    $("[id*=grdadd] td").click(function () {
        $('#grdadd  tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                v = $(this).text();
                //                alert(v);
                slvals += v + ",";
                //                            var string_length = v.length;

                //                            alert(v);


            });



        });

        //        alert(checkedVals.join(","));

        //        alert(slvals);
        document.getElementById('ctl00_ContentPlaceHolder1_txtAddr').value = slvals;

        //            });
    });
</script>
<script type="text/javascript">
    $(document).on("click", "#frdadd", function (e) {
        var slvals = '';
        //    $("[id*=grdadd] td").click(function () {
        $('#frdadd  tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                v = $(this).text();
                //                alert(v);
                slvals += v + ",";
                //                            var string_length = v.length;

                //                            alert(v);


            });



        });

        //        alert(checkedVals.join(","));

        //        alert(slvals);
        document.getElementById('ctl00_ContentPlaceHolder1_TextBox3').value = slvals;

        //            });
    });
</script>
<%--<script type="text/javascript">

    var selected = [];
    $('input[type="checkbox"]').change(function () {
        var id = $(this).attr('id');
        alert(id);
//        selected.push($(this).attr('name'));
    });
</script>--%>
<script type="text/javascript">
    //    $(document).ready(function () {
    // $(function(){
    //    $('div checkbox:checked').map(function () {
    $(document).on("change", "#res", function () {
        //  $("#res").change(function () {

        if ($(this).prop('checked')) {
            $("#grdadd tr td ." + this.value + ":checkbox").attr('checked', 'checked');
        }
        else {
            $("#grdadd tr td ." + this.value + ":checkbox").removeAttr('checked');
        }
        var slvals = '';
        var bcc = '';
        var cc = '';
        $('#grdadd tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                v = $(this).text();
                //                alert(v);
                slvals += v + ",";
                //                            var string_length = v.length;

                //                            alert(v);
                document.getElementById('ctl00_ContentPlaceHolder1_txtAddr').value = slvals;

            });



        });
    });
        //    $("[id*=grdadd] td").click(function () {
    $(document).on("change", "#resbcc", function () {
        //  $("#res").change(function () {

        if ($(this).prop('checked')) {
            $("#grdbcc tr td ." + this.value + ":checkbox").attr('checked', 'checked');
        }
        else {
            $("#grdbcc tr td ." + this.value + ":checkbox").removeAttr('checked');
        }
        var slvals = '';
        var bcc = '';
        var cc = '';
        $('#grdbcc tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                vv1 = $(this).text();
                //                alert(v);
                bcc += vv1 + ",";
                //                            var string_length = v.length;

                //                            alert(v);


            });

            document.getElementById('bcctextbox').value = bcc;

        });
    });
    $(document).on("change", "#rescc", function () {
        //  $("#res").change(function () {

        if ($(this).prop('checked')) {
            $("#grdcc tr td ." + this.value + ":checkbox").attr('checked', 'checked');
        }
        else {
            $("#grdcc tr td ." + this.value + ":checkbox").removeAttr('checked');
        }
        var slvals = '';
        var bcc = '';
        var cc = '';

        $('#grdcc tr td :checkbox:checked').map(function () {
            var $this = $(this);
            $this.closest('tr').each(function () {
                vv = $(this).text();
                //                alert(v);
                cc += vv + ",";
                //                            var string_length = v.length;

                //                            alert(v);


            });

            document.getElementById('cctextbox').value = cc;

        });
    });

        //        alert(checkedVals.join(","));

        //        alert(slvals);


        //            if (this.value == 'ASM') {
        //                $(':checkbox').map(function () {

        //

        //        .prop('checked', true);
        //            return this.value;
        //                })
        //            }
        //            else if (this.value == 'ASO') {
        //                alert("Transfer Thai Gayo");
        //                $(':checkbox').map(function () {
        //                    $("table tr td .ASM:checkbox").removeAttr('checked');
        //                    $("table tr td .SM:checkbox").removeAttr('checked');
        //                    $("table tr td .ASO:checkbox").attr('checked', 'checked')

        //                    //        .prop('checked', true);
        //                    //            return this.value;
        //                })
        //            }
        //            else if (this.value == 'SM') {
        //                alert("Transfer Thai Gayo");
        //                $(':checkbox').map(function () {
        //                    //                    $("table tr td .ASM:checkbox").removeAttr('checked');
        //                    //                    $("table tr td .ASO:checkbox").removeAttr('checked');
        //                    $("table tr td .SM:checkbox").attr('checked', 'checked')

        //                    //        .prop('checked', true);
        //                    //            return this.value;
        //                })
        //            }



        //        });
   
    //       
    
    </script>
    <script type="text/javascript">
        //    $(document).ready(function () {
        // $(function(){
        //    $('div checkbox:checked').map(function () {
        $(document).on("change", "#resfrd", function () {
            //  $("#res").change(function () {

            if ($(this).prop('checked')) {
                $("#frdadd tr td ." + this.value + ":checkbox").attr('checked', 'checked');
            }
            else {
                $("#frdadd tr td ." + this.value + ":checkbox").removeAttr('checked');
            }
            var slvals = '';
            var bcc = '';
            var cc = '';
            $('#frdadd tr td :checkbox:checked').map(function () {
                var $this = $(this);
                $this.closest('tr').each(function () {
                    v = $(this).text();
                    //                alert(v);
                    slvals += v + ",";
                    //                            var string_length = v.length;

                    //                            alert(v);
                    document.getElementById('ctl00_ContentPlaceHolder1_TextBox3').value = slvals;

                });



            });
        });
        //    $("[id*=grdadd] td").click(function () {
        $(document).on("change", "#resbccfrd", function () {
            //  $("#res").change(function () {

            if ($(this).prop('checked')) {
                $("#frdbcc tr td ." + this.value + ":checkbox").attr('checked', 'checked');
            }
            else {
                $("#frdbcc tr td ." + this.value + ":checkbox").removeAttr('checked');
            }
            var slvals = '';
            var bcc = '';
            var cc = '';
            $('#frdbcc tr td :checkbox:checked').map(function () {
                var $this = $(this);
                $this.closest('tr').each(function () {
                    vv1 = $(this).text();
                    //                alert(v);
                    bcc += vv1 + ",";
                    //                            var string_length = v.length;

                    //                            alert(v);


                });

                document.getElementById('bcctextboxfrd').value = bcc;

            });
        });
        $(document).on("change", "#resccfrd", function () {
            //  $("#res").change(function () {

            if ($(this).prop('checked')) {
                $("#frdcc tr td ." + this.value + ":checkbox").attr('checked', 'checked');
            }
            else {
                $("#frdcc tr td ." + this.value + ":checkbox").removeAttr('checked');
            }
            var slvals = '';
            var bcc = '';
            var cc = '';

            $('#frdcc tr td :checkbox:checked').map(function () {
                var $this = $(this);
                $this.closest('tr').each(function () {
                    vv = $(this).text();
                    //                alert(v);
                    cc += vv + ",";
                    //                            var string_length = v.length;

                    //                            alert(v);


                });

                document.getElementById('cctextboxfrd').value = cc;

            });
        });

        //        alert(checkedVals.join(","));

        //        alert(slvals);


        //            if (this.value == 'ASM') {
        //                $(':checkbox').map(function () {

        //

        //        .prop('checked', true);
        //            return this.value;
        //                })
        //            }
        //            else if (this.value == 'ASO') {
        //                alert("Transfer Thai Gayo");
        //                $(':checkbox').map(function () {
        //                    $("table tr td .ASM:checkbox").removeAttr('checked');
        //                    $("table tr td .SM:checkbox").removeAttr('checked');
        //                    $("table tr td .ASO:checkbox").attr('checked', 'checked')

        //                    //        .prop('checked', true);
        //                    //            return this.value;
        //                })
        //            }
        //            else if (this.value == 'SM') {
        //                alert("Transfer Thai Gayo");
        //                $(':checkbox').map(function () {
        //                    //                    $("table tr td .ASM:checkbox").removeAttr('checked');
        //                    //                    $("table tr td .ASO:checkbox").removeAttr('checked');
        //                    $("table tr td .SM:checkbox").attr('checked', 'checked')

        //                    //        .prop('checked', true);
        //                    //            return this.value;
        //                })
        //            }



        //        });

        //       
    
    </script>
<script type="text/javascript">
   
            function Loadaddress(flag)
         {
         
           
          ad = document.getElementById("addbook");
          ad.style = "width:502px;height:450px;"
          //  dv.attributes('style', 'width:500px;height:300px;');
           
          ad.className = 'popup';
                 var adcancel = document.createElement('div');




          adcancel.id = "canlad";
          adcancel.className = 'cancel'; adcancel.innerHTML = 'Close';
          adcancel.onclick = function (e) { $("#addbook").hide(); ;cad = document.getElementById("canlad"); ad.className = ''; cad.parentNode.removeChild(cad);};
          ad.appendChild(adcancel);
//            var discode = document.getElementById("<%=Searchtxt.ClientID %>");
               var gridid ='grdadd';

                var selectV = 'admin';
                   var a = [];
                     var result = [], map = {}, item="";
                     var di;
   
       
              $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "attachementwithajax.aspx/Getaddress",
                     data:JSON.stringify({selectV}),
                    dataType: "json",
                    success: function (data) {
                      if (data.d.length > 0) {
//                        $(document).on("click", "#ctl00_ContentPlaceHolder1_Button6", function () {
//                 $("#ctl00_ContentPlaceHolder1_Button6").click(function(){
if(flag=='ctl00_ContentPlaceHolder1_Button6')
{
                    $('#grdadd').show();
                          $('#designationbcc').hide();
             $('#designationcc').hide(); 
             $('#designation').show();
                  
                    var rw=$("#grdadd").find("tr")[0];
                    $("#grdadd").empty();                  
                         $("#grdadd").append(rw);
                           for (var i = 0; i < data.d.length; i++) {
                         var chk="<input name='chkadd' type='checkbox' class='"+data.d[i].sfshrtname+"' id='chk_"+i+"'/>";
                         $("#grdadd").append("<tr><td>"+ chk + "</td>< td>" + 
                            data.d[i].sfid + "</td> <td>" + 
                            data.d[i].sfname + "</td> <td>" + 
                            data.d[i].sfshrtname + "</td> <td>" + 
                            data.d[i].sfhq + "</td></tr>");
//                             a += data.d[i].sfshrtname +",";
 a[i]=data.d[i].sfshrtname;
   $('#grdcc').hide();
    $('#grdbcc').hide();
     } 

   $('#designation').empty();
      
      if(item=="")
  {
    	
    for (var k = 0; k < a.length; k++) {
        item = a[k].trim();
      
        if (!map[item]) {
            result.push(item);
            map[item] = true;
        }

}
//   alert(result);
 for (var j = 0; j < result.length; j++) { //loop through the array and add each rate to the dropdown
		var input = document.createElement('input');//input 
		var label = document.createElement('label');//label
		var ptag = document.createElement('p');// create ptag


		input.setAttribute('type', 'checkbox');// add type radio
		input.setAttribute('value', result[j]);
        input.setAttribute('id', 'res');
        	input.setAttribute('text', result[j]);// add rate value
		input.setAttribute('name', 'result');// add rate name
//        input.setAttribute("oncheck", runCommand());
//       	 di = document.createElement('div');

//		
		label.setAttribute('for',result[j]);// set for attribute for label
		label.setAttribute('id', result[j]);// set id for label
//		
	label.appendChild(document.createTextNode(result[j]));// append text for label
  
     desig = document.getElementById("designation");

       desig.appendChild(input);
       desig.appendChild(label);

          addd = document.getElementById("addbklist");
        aad = document.getElementById("desigfilter");
//        ad.insertBefore(input, aad.childNodes[0]);
          ad.prepend(aad);
          ad.appendChild(desig);
          ad.appendChild(addd);
//  input.insertBefore($("#grdadd"), input.firstChild);
//           label.insertBefore($('#grdadd'), label.firstChild);
//		rateslist.parentNode.insertBefore(label, rateslist.nextSibling);
//		rateslist.parentNode.insertBefore(input, rateslist.nextSibling); 





      }  
      }
     } 
//      }); 
//                            var counter=1;
//    var chk="<input type='checkbox' id='chk_'"+counter+"'/>";

//    $("table#t tr td").each(function(){
//        $(this).append(chk);
//        counter++;
//    });
 
              
                        
//$("#ctl00_ContentPlaceHolder1_Button7").click(function(){
if(flag=='ctl00_ContentPlaceHolder1_Button7')
{
   $('#designationcc').hide();
         $('#designation').hide();
           $('#designationbcc').show();
    $('#grdbcc').show();
     $('#grdadd').hide();
  $('#grdcc').hide();
                    var rw=$("#grdbcc").find("tr")[0];
                    $("#grdbcc").empty();
                   
                         $("#grdbcc").append(rw);
        for (var i = 0; i < data.d.length; i++) {
                         var chkbc="<input name='chkaddbcc' type='checkbox' class='"+data.d[i].sfshrtname+"' id='chkbcc_"+i+"'/>";
  $("#grdbcc").append("<tr><td>"+ chkbc + "</td>< td>" + 
                            data.d[i].sfid + "</td> <td>" + 
                            data.d[i].sfname + "</td> <td>" + 
                            data.d[i].sfshrtname + "</td> <td>" + 
                            data.d[i].sfhq + "</td></tr>");
//                             a += data.d[i].sfshrtname +",";
 a[i]=data.d[i].sfshrtname;
 
  
    }
     $('#designationbcc').empty();
    
     if(item=="")
  {
    	
    for (var k = 0; k < a.length; k++) {
        item = a[k].trim();
      
        if (!map[item]) {
            result.push(item);
            map[item] = true;
        }

}
//   alert(result);
 for (var j = 0; j < result.length; j++) { //loop through the array and add each rate to the dropdown
		var input = document.createElement('input');//input 
		var label = document.createElement('label');//label
		var ptag = document.createElement('p');// create ptag


		input.setAttribute('type', 'checkbox');// add type radio
		input.setAttribute('value', result[j]);
        input.setAttribute('id', 'resbcc');
        	input.setAttribute('text', result[j]);// add rate value
		input.setAttribute('name', 'result');// add rate name
//        input.setAttribute("oncheck", runCommand());
//       	 di = document.createElement('div');

//		
		label.setAttribute('for',result[j]);// set for attribute for label
		label.setAttribute('id', result[j]);// set id for label
//		
	label.appendChild(document.createTextNode(result[j]));
     desigbcc = document.getElementById("designationbcc");

       desigbcc.appendChild(input);
       desigbcc.appendChild(label);// append text for label
      
          addd = document.getElementById("addbklist");
        aad = document.getElementById("desigfilter");
//        ad.insertBefore(input, aad.childNodes[0]);
          ad.prepend(aad);
             ad.appendChild(desigbcc);
          ad.appendChild(addd);
//  input.insertBefore($("#grdadd"), input.firstChild);
//           label.insertBefore($('#grdadd'), label.firstChild);
//		rateslist.parentNode.insertBefore(label, rateslist.nextSibling);
//		rateslist.parentNode.insertBefore(input, rateslist.nextSibling);





      }  
      }
     } 
//       $("#ctl00_ContentPlaceHolder1_Button8").click(function(){
if(flag=='ctl00_ContentPlaceHolder1_Button8')
{
        $('#grdcc').show(); 
         $('#grdadd').hide();
           $('#designationcc').show();
  $('#grdbcc').hide();
     var rw=$("#grdcc").find("tr")[0];
                    $("#grdcc").empty();
                
                         $("#grdcc").append(rw);
        for (var i = 0; i < data.d.length; i++) {
                         var chkd="<input name='chkadcc' type='checkbox' class='"+data.d[i].sfshrtname+"' id='chkcc_"+i+"'/>";
       $("#grdcc").append("<tr><td>"+ chkd + "</td>< td>" + 
                            data.d[i].sfid + "</td> <td>" + 
                            data.d[i].sfname + "</td> <td>" + 
                            data.d[i].sfshrtname + "</td> <td>" + 
                            data.d[i].sfhq + "</td></tr>");
//                             a += data.d[i].sfshrtname +",";
 a[i]=data.d[i].sfshrtname;


      } 
       $('#designationcc').empty();   
          $('#designationbcc').hide();
             $('#designation').hide();         
        if(item=="")
  {
    	
    for (var k = 0; k < a.length; k++) {
        item = a[k].trim();
      
        if (!map[item]) {
            result.push(item);
            map[item] = true;
        }

}
//   alert(result);
 for (var j = 0; j < result.length; j++) { //loop through the array and add each rate to the dropdown
		var input = document.createElement('input');//input 
		var label = document.createElement('label');//label
		var ptag = document.createElement('p');// create ptag


		input.setAttribute('type', 'checkbox');// add type radio
		input.setAttribute('value', result[j]);
        input.setAttribute('id', 'rescc');
        	input.setAttribute('text', result[j]);// add rate value
		input.setAttribute('name', 'result');// add rate name
//        input.setAttribute("oncheck", runCommand());
//       	 di = document.createElement('div');

//		
		label.setAttribute('for',result[j]);// set for attribute for label
		label.setAttribute('id', result[j]);// set id for label
//		
	label.appendChild(document.createTextNode(result[j]));// append text for label
      desigcc = document.getElementById("designationcc");

       desigcc.appendChild(input);
       desigcc.appendChild(label);// append text for label
      
          addd = document.getElementById("addbklist");
        aad = document.getElementById("desigfilter");
//        ad.insertBefore(input, aad.childNodes[0]);
          ad.prepend(aad);
             ad.appendChild(desigcc);
          ad.appendChild(addd);
//  input.insertBefore($("#grdadd"), input.firstChild);
//           label.insertBefore($('#grdadd'), label.firstChild);
//		rateslist.parentNode.insertBefore(label, rateslist.nextSibling);
//		rateslist.parentNode.insertBefore(input, rateslist.nextSibling);





      }  
      }                
 }



//                        var   x = document.createElement("INPUT");
//                     x.setAttribute("type", "checkbox");
//x.setAttribute("id", i);
//                      td.appendChild(chk);
////    tr.appendChild(td);
//    $("#ctl00_ContentPlaceHolder1_GridView1").append(tr);

                          

//              a=['ASO','ASO', 'ASM','SM','SM'];
  
 
//    alert(result);
   	



                        }
//                     alert( $.unique(a));        
//                 var unique = a.filter(function(itm, ii, a) {
//    return ii == a.indexOf(itm);
//});
//alert(unique);
                   
                    },
                    error: function ajaxError(result) {
                        alert(result.status + ' : ' + result.statusText);
                    }
                });
//                 di.appendchild(ad);   
//     

               
return false

     };

//   
  

</script>


<script type="text/javascript">
   
            function Loadaddressfrd(flag)
         {
     
           
          fd = document.getElementById("addbookfrd");
          fd.style = "width:502px;height:450px;"
          //  dv.attributes('style', 'width:500px;height:300px;');
           
          fd.className = 'popup';
                 var fdcancel = document.createElement('div');




          fdcancel.id = "canlfd";
          fdcancel.className = 'cancel'; fdcancel.innerHTML = 'Close';
          fdcancel.onclick = function (e) { $("#addbookfrd").hide(); ;fad = document.getElementById("canlfd"); fd.className = ''; fad.parentNode.removeChild(fad);};
          fd.appendChild(fdcancel);
//            var discode = document.getElementById("<%=Searchtxt.ClientID %>");
               var gridid ='grdaddfrd';

                var selectV = 'admin';
                   var a = [];
                     var result = [], map = {}, item="";
                     var di;
   
       
              $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "attachementwithajax.aspx/Getaddress",
                     data:JSON.stringify({selectV}),
                    dataType: "json",
                    success: function (data) {
                      if (data.d.length > 0) {
//                        $(document).on("click", "#ctl00_ContentPlaceHolder1_Button6", function () {
//                 $("#ctl00_ContentPlaceHolder1_Button6").click(function(){
if(flag=='ctl00_ContentPlaceHolder1_Button11')
{
                    $('#frdadd').show();
                          $('#designationbcc').hide();
             $('#designationccfrd').hide(); 
             $('#designationfrd').show();
                  
                    var rw=$("#frdadd").find("tr")[0];
                    $("#frdadd").empty();                  
                         $("#frdadd").append(rw);
                           for (var i = 0; i < data.d.length; i++) {
                         var chk="<input name='chkadd' type='checkbox' class='"+data.d[i].sfshrtname+"' id='chk_"+i+"'/>";
                         $("#frdadd").append("<tr><td>"+ chk + "</td>< td>" + 
                            data.d[i].sfid + "</td> <td>" + 
                            data.d[i].sfname + "</td> <td>" + 
                            data.d[i].sfshrtname + "</td> <td>" + 
                            data.d[i].sfhq + "</td></tr>");
//                             a += data.d[i].sfshrtname +",";
 a[i]=data.d[i].sfshrtname;
   $('#frdcc').hide();
    $('#frdbcc').hide();
     } 

   $('#designationfrd').empty();
      
      if(item=="")
  {
    	
    for (var k = 0; k < a.length; k++) {
        item = a[k].trim();
      
        if (!map[item]) {
            result.push(item);
            map[item] = true;
        }

}
//   alert(result);
 for (var j = 0; j < result.length; j++) { //loop through the array and add each rate to the dropdown
		var input = document.createElement('input');//input 
		var label = document.createElement('label');//label
		var ptag = document.createElement('p');// create ptag


		input.setAttribute('type', 'checkbox');// add type radio
		input.setAttribute('value', result[j]);
        input.setAttribute('id', 'resfrd');
        	input.setAttribute('text', result[j]);// add rate value
		input.setAttribute('name', 'result');// add rate name
//        input.setAttribute("oncheck", runCommand());
//       	 di = document.createElement('div');

//		
		label.setAttribute('for',result[j]);// set for attribute for label
		label.setAttribute('id', result[j]);// set id for label
//		
	label.appendChild(document.createTextNode(result[j]));// append text for label
  
     desig = document.getElementById("designationfrd");

       desig.appendChild(input);
       desig.appendChild(label);

          addd = document.getElementById("addbklistfrd");
        aad = document.getElementById("desigfilterfrd");
//        ad.insertBefore(input, aad.childNodes[0]);
          fd.prepend(aad);
          fd.appendChild(desig);
          fd.appendChild(addd);
//  input.insertBefore($("#grdadd"), input.firstChild);
//           label.insertBefore($('#grdadd'), label.firstChild);
//		rateslist.parentNode.insertBefore(label, rateslist.nextSibling);
//		rateslist.parentNode.insertBefore(input, rateslist.nextSibling); 





      }  
      }
     } 
//      }); 
//                            var counter=1;
//    var chk="<input type='checkbox' id='chk_'"+counter+"'/>";

//    $("table#t tr td").each(function(){
//        $(this).append(chk);
//        counter++;
//    });
 
              
                        
//$("#ctl00_ContentPlaceHolder1_Button7").click(function(){
if(flag=='ctl00_ContentPlaceHolder1_Button9')
{
   $('#designationccfrd').hide();
         $('#designationfrd').hide();
           $('#designationbccfrd').show();
    $('#frdbcc').show();
     $('#frdadd').hide();
  $('#frdcc').hide();
                    var rw=$("#frdbcc").find("tr")[0];
                    $("#frdbcc").empty();
                   
                         $("#frdbcc").append(rw);
        for (var i = 0; i < data.d.length; i++) {
                         var chkbc="<input name='chkaddbcc' type='checkbox' class='"+data.d[i].sfshrtname+"' id='chkbcc_"+i+"'/>";
  $("#frdbcc").append("<tr><td>"+ chkbc + "</td>< td>" + 
                            data.d[i].sfid + "</td> <td>" + 
                            data.d[i].sfname + "</td> <td>" + 
                            data.d[i].sfshrtname + "</td> <td>" + 
                            data.d[i].sfhq + "</td></tr>");
//                             a += data.d[i].sfshrtname +",";
 a[i]=data.d[i].sfshrtname;
 
  
    }
     $('#designationbccfrd').empty();
    
     if(item=="")
  {
    	
    for (var k = 0; k < a.length; k++) {
        item = a[k].trim();
      
        if (!map[item]) {
            result.push(item);
            map[item] = true;
        }

}
//   alert(result);
 for (var j = 0; j < result.length; j++) { //loop through the array and add each rate to the dropdown
		var input = document.createElement('input');//input 
		var label = document.createElement('label');//label
		var ptag = document.createElement('p');// create ptag


		input.setAttribute('type', 'checkbox');// add type radio
		input.setAttribute('value', result[j]);
        input.setAttribute('id', 'resbccfrd');
        	input.setAttribute('text', result[j]);// add rate value
		input.setAttribute('name', 'result');// add rate name
//        input.setAttribute("oncheck", runCommand());
//       	 di = document.createElement('div');

//		
		label.setAttribute('for',result[j]);// set for attribute for label
		label.setAttribute('id', result[j]);// set id for label
//		
	label.appendChild(document.createTextNode(result[j]));
     desigbccfrd = document.getElementById("designationbccfrd");

       desigbccfrd.appendChild(input);
       desigbccfrd.appendChild(label);// append text for label
      
          adddfrd = document.getElementById("addbklistfrd");
        aadfrd = document.getElementById("desigfilterfrd");
//        ad.insertBefore(input, aad.childNodes[0]);
          fd.prepend(aadfrd);
             fd.appendChild(desigbccfrd);
          fd.appendChild(adddfrd);
//  input.insertBefore($("#grdadd"), input.firstChild);
//           label.insertBefore($('#grdadd'), label.firstChild);
//		rateslist.parentNode.insertBefore(label, rateslist.nextSibling);
//		rateslist.parentNode.insertBefore(input, rateslist.nextSibling);





      }  
      }
     } 
//       $("#ctl00_ContentPlaceHolder1_Button8").click(function(){
if(flag=='ctl00_ContentPlaceHolder1_Button10')
{
        $('#frdcc').show(); 
         $('#frdadd').hide();
           $('#designationccfrd').show();
  $('#frdbcc').hide();
     var rw=$("#frdcc").find("tr")[0];
                    $("#frdcc").empty();
                
                         $("#frdcc").append(rw);
        for (var i = 0; i < data.d.length; i++) {
                         var chkd="<input name='chkadcc' type='checkbox' class='"+data.d[i].sfshrtname+"' id='chkcc_"+i+"'/>";
       $("#frdcc").append("<tr><td>"+ chkd + "</td>< td>" + 
                            data.d[i].sfid + "</td> <td>" + 
                            data.d[i].sfname + "</td> <td>" + 
                            data.d[i].sfshrtname + "</td> <td>" + 
                            data.d[i].sfhq + "</td></tr>");
//                             a += data.d[i].sfshrtname +",";
 a[i]=data.d[i].sfshrtname;


      } 
       $('#designationccfrd').empty();   
          $('#designationbccfrd').hide();
             $('#designationfrd').hide();         
        if(item=="")
  {
    	
    for (var k = 0; k < a.length; k++) {
        item = a[k].trim();
      
        if (!map[item]) {
            result.push(item);
            map[item] = true;
        }

}
//   alert(result);
 for (var j = 0; j < result.length; j++) { //loop through the array and add each rate to the dropdown
		var input = document.createElement('input');//input 
		var label = document.createElement('label');//label
		var ptag = document.createElement('p');// create ptag


		input.setAttribute('type', 'checkbox');// add type radio
		input.setAttribute('value', result[j]);
        input.setAttribute('id', 'resccfrd');
        	input.setAttribute('text', result[j]);// add rate value
		input.setAttribute('name', 'result');// add rate name
//        input.setAttribute("oncheck", runCommand());
//       	 di = document.createElement('div');

//		
		label.setAttribute('for',result[j]);// set for attribute for label
		label.setAttribute('id', result[j]);// set id for label
//		
	label.appendChild(document.createTextNode(result[j]));// append text for label
      desigcc = document.getElementById("designationccfrd");

       desigcc.appendChild(input);
       desigcc.appendChild(label);// append text for label
      
          addd = document.getElementById("addbklistfrd");
        aad = document.getElementById("desigfilterfrd");
//        ad.insertBefore(input, aad.childNodes[0]);
          fd.prepend(aad);
             fd.appendChild(desigcc);
          fd.appendChild(addd);
//  input.insertBefore($("#grdadd"), input.firstChild);
//           label.insertBefore($('#grdadd'), label.firstChild);
//		rateslist.parentNode.insertBefore(label, rateslist.nextSibling);
//		rateslist.parentNode.insertBefore(input, rateslist.nextSibling);





      }  
      }                
 }



//                        var   x = document.createElement("INPUT");
//                     x.setAttribute("type", "checkbox");
//x.setAttribute("id", i);
//                      td.appendChild(chk);
////    tr.appendChild(td);
//    $("#ctl00_ContentPlaceHolder1_GridView1").append(tr);

                          

//              a=['ASO','ASO', 'ASM','SM','SM'];
  
 
//    alert(result);
   	



                        }
//                     alert( $.unique(a));        
//                 var unique = a.filter(function(itm, ii, a) {
//    return ii == a.indexOf(itm);
//});
//alert(unique);
                 
                    },
                    error: function ajaxError(result) {
                        alert(result.status + ' : ' + result.statusText);
                    }
                });
//                 di.appendchild(ad);   
//     

               
return false

     };
 
//   
  

</script>


<script  type="text/javascript">
    $(function () {
        $("[id*=ctl00_ContentPlaceHolder1_Button1]").click(function () {
            cmp = document.getElementById("cmpflp");
            cmpd = document.getElementById("flp");
            cmp.appendChild(cmpd);
            document.getElementById("dvIcon").innerHTML = 'Please select a file to upload';
            document.getElementById("tblMessage").className = "Information";
            document.getElementById("dvFileName").innerHTML = "";
            document.getElementById("dvDownload").innerHTML = "";
            document.getElementById("dvProgressPrcent").innerHTML = "";
            document.getElementById("dvProgress").style.backgroundImage = "none";
            $('#ctl00_ContentPlaceHolder1_gvNewFiles').empty();
//            $.ajax({

//                type: "POST",
//                contentType: "application/json;charset=utf-8",
//                url: "attachementwithajax.aspx/composeclk",

//                data: {},
//                dataType: "json",
//                success: function (r) {


//                },


//                error: function ajaxError(result) {
//                    alert("failure");
//                }
//            });

        });
    });

</script>







    <script type="text/javascript">
        var attachidden;
        var translno;
        var chkboxx;

        $(function () {
            $("[id*=ctl00_ContentPlaceHolder1_GridView1] td").click(function () {


                var gv = document.getElementById("<%=GridView1.ClientID%>");
                var chk = gv.getElementsByTagName("input");
                //                for (var i = 0; i < chk.length; i++) {

                //                    if (chk[i].checked) {

                //                    } 
                //                }
                //                $('#ctl00_ContentPlaceHolder1_GridViewfrd').empty();
                var t = $(this).closest("tr").text();
                document.getElementById("dvIcon").innerHTML = 'Please select a file to upload';
                document.getElementById("tblMessage").className = "Information";
                document.getElementById("dvFileName").innerHTML = "";
                document.getElementById("dvDownload").innerHTML = "";
                document.getElementById("dvProgressPrcent").innerHTML = "";
                document.getElementById("dvProgress").style.backgroundImage = "none";
                //                $('#dvFileName').innerHTML = "";
                //                $('#dvDownload').innerHTML = "";
                //                $('#dvProgressPrcent').innerHTML = "";
                //                $('#dvProgressContainer').innerHTML = "";

               
                //                setInterval(function () {
                //        $('#dvIcon').load('#dvIcon'));
                //    }, 3000);
                //                document.getElementById("dvIcon").innerHTML.reload;
                $('#atdivv').remove();
                $('#atdivvp').remove();
                $('#ctl00_ContentPlaceHolder1_gvNewFiles').empty();

                //                selecteow();
                //                v = $(this).find("td:last").text();
                //                alert($(this).closest('tr').find('td:last').text());
                //                var trs = $('table tr');
                //                //alert(trs);
                //                var values = trs.first().find('td');
                //                alert(v);
                //                var v = "null";
                //                $('table tr td :checkbox:checked').map(function () {
                //                    var $this = $(this);
                //                    $this.closest('tr').each(function () {
                //                        v = $(this).find("td:last").text();

                //                        var string_length = v.length;
                //                        //                        alert(v);
                //                    });
                //                });
                if (chkboxx != "checked") {
                    dv = document.getElementById("popupdiv");
                    dv.style = "width:572px;height:450px;"
                    //  dv.attributes('style', 'width:500px;height:300px;');


                    dv.className = 'popup';
                    var cancel = document.createElement('div');



                    cancel.id = "canl";
                    cancel.className = 'cancel'; cancel.innerHTML = 'Close';
                    cancel.onclick = function (e) { cdv = document.getElementById("canl"); dv.className = ''; cdv.parentNode.removeChild(cdv); rdv = document.getElementById("repp"); dv.className = ''; rdv.parentNode.removeChild(rdv); fdv = document.getElementById("forwd"); dv.className = ''; fdv.parentNode.removeChild(fdv); $("#popupdiv").hide(); $("#replydiv").hide(); };
                    dv.appendChild(cancel);
                    var reply = document.createElement('div');
                    reply.id = "repp";
                    reply.className = 'cancel'; reply.innerHTML = 'Reply';
                    reply.onclick = function (e) { cdv = document.getElementById("canl"); dv.className = ''; cdv.parentNode.removeChild(cdv); dv.className = ''; rdv = document.getElementById("repp"); dv.className = ''; rdv.parentNode.removeChild(rdv); fdv = document.getElementById("forwd"); dv.className = ''; fdv.parentNode.removeChild(fdv); replymessage(); };
                    dv.appendChild(reply);
                    var forward = document.createElement('div');
                    forward.id = "forwd";
                    forward.className = 'cancel'; forward.innerHTML = 'Forward';
                    forward.onclick = function (e) { cdv = document.getElementById("canl"); dv.className = ''; cdv.parentNode.removeChild(cdv); rdv = document.getElementById("repp"); dv.className = ''; rdv.parentNode.removeChild(rdv); fdv = document.getElementById("forwd"); dv.className = ''; fdv.parentNode.removeChild(fdv); forwardmessage(); };
                    dv.appendChild(forward);

                    DisplayDetails($(this).closest("tr"));
                    return false;
                }
                else {


                  


                }




            });



        });
        function calculateTotal(ele) {

            //ele is the element which triggered this event.

            if (ele.checked) {
                chkboxx = 'checked';
              
                ele.checked = true;


            }
            else {

            }
            //do other stuff.

        }
      
        function DisplayDetails(row) {
            var message = "";

            document.getElementById("ctl00_ContentPlaceHolder1_subj").innerHTML = $("td", row).eq(1).text();
            var sub = document.getElementById("ctl00_ContentPlaceHolder1_subj").innerHTML;
            document.getElementById("ctl00_ContentPlaceHolder1_tosubj").innerHTML = $("td", row).eq(2).text();
            document.getElementById("ctl00_ContentPlaceHolder1_from").innerHTML = $("td", row).eq(2).text();
            document.getElementById("ctl00_ContentPlaceHolder1_tofeildforce").innerHTML = $("td", row).eq(1).text();
            document.getElementById("ctl00_ContentPlaceHolder1_message").innerHTML = $("td", row).eq(3).text();
            var msg = document.getElementById("ctl00_ContentPlaceHolder1_message").innerHTML;
            document.getElementById("ctl00_ContentPlaceHolder1_Datetime").innerHTML = $("td", row).eq(4).text();
            document.getElementById("ctl00_ContentPlaceHolder1_attachmentfile").innerHTML = $("td", row).eq(6).find(':input').val();
            document.getElementById("ctl00_ContentPlaceHolder1_subfrd").innerHTML = sub;
            document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").innerHTML = msg;


           
            






          

            attachidden = $("td", row).eq(6).find(':input').val();
            translno = $("td", row).eq(5).find(':input').val();
              $.ajax({

                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "attachementwithajax.aspx/updatereadstatus",
                 
                      data: JSON.stringify({  selectValue :translno}),
                    dataType: "json",
                    success: function (r) {
                  
                
                     },
                     
                   
                    error: function ajaxError(result) {
                        alert("failure");
                    }
                });


            //            alert(td.html(td.find("input[type=hidden]").val()));
            //            alert($("input[type=hidden]", row).eq(5).val());
            //            var attach = $("td", row).eq(4).text();

            var strv = $("td", row).eq(6).find(':input').val();
            var strarrayv = strv.split(',');
            for (var i = 0; i < strarrayv.length; i++) {
               
            }



            //                                     for (var k = 0; k < s.length; k++) {
            //                    item = s[k].trim();
            //                  
            //                    if (!map[item]) {
            //                        attachresult.push(item);
            //                        map[item] = true;
            //                    }


            //}
            //var atdiv  = document.getElementById('attachmentdiv');//input 
            for (var l = 0; l < strarrayv.length; l++) { //loop through the array and add each rate to the dropdown0

                $('#atdivv').empty();
                $('#atdivvp').remove();
                var atdivv = document.createElement('div');

                var elem1v = document.createElement("div");
                elem1v.className = 'glyphicon glyphicon-picture';
              
              
                elem1v.onclick = function (e) { alert('img1click'); };
                var elem2v = document.createElement("a");
              
                elem2v.setAttribute('href', "DownloadHandler.ashx?ImageName='" + strarrayv[l] + "'");
                elem2v.innerHTML = strarrayv[l];
                //    elem2.innerHTML = attachresult[l];
                //    elem2.setAttribute("value",attachresult[l]);
                //   elem2.innerHTML='Attach';
                elem2v.className = 'attach';
                //    elem2.className='form-control';
                //  		elem2.setAttribute('type', 'text');// 
                //elem2.setAttribute("height", "18");
                //elem2.setAttribute("width", "50");
                //elem2.setAttribute("color", "#ccc");

                //elem2.setAttribute("font-size", "smaller");

                // elem2.onclick = function (e) { alert('imgclick');};

                var elemv = document.createElement("div");
                elemv.className = 'glyphicon glyphicon-save';
                //elem.setAttribute("height", "20");
                //elem.setAttribute("width", "50");

                elemv.onclick = function (e) { alert('imgclick'); };
                //		var ptag = document.createElement('p');// create ptag
                                atdivv.id = 'atdivvp';
                atdivv.appendChild(elem1v);
                atdivv.appendChild(elem2v);
                //atdiv.className='attach';
                atdivv.appendChild(elemv);



                dv.appendChild(atdivv);
              



            }
        }
        function replymessage() {

            $("#popupdiv").hide();
            vdv = document.getElementById("replydiv");
            vdv.style = "width:572px;height:450px;"
            vdv.className = 'popup';

            var repcancel = document.createElement('div');
            repcancel.id = "reep";
            repcancel.className = 'cancel'; repcancel.innerHTML = 'Close';
            repcancel.onclick = function (e) { $("#replydiv").hide(); fcdv = document.getElementById("reep"); vdv.className = ''; fcdv.parentNode.removeChild(fcdv); tfdv = document.getElementById("rsend"); vdv.className = ''; tfdv.parentNode.removeChild(tfdv);rdv = document.getElementById("repp"); dv.className = ''; rdv.parentNode.removeChild(rdv); cdv = document.getElementById("canl"); dv.className = ''; cdv.parentNode.removeChild(cdv); };

            vdv.appendChild(repcancel);
            var rpsave = document.createElement('div');
            rpsave.id = "rsend";
            rpsave.className = 'cancel'; rpsave.innerHTML = 'Send';
            rpsave.onclick = function (e) {  $("#replydiv").hide(); fcdv = document.getElementById("reep"); fcdv.parentNode.removeChild(fcdv); vdv.className = ''; tfdv = document.getElementById("rsend"); vdv.className = ''; tfdv.parentNode.removeChild(tfdv); rdv = document.getElementById("repp"); dv.className = ''; rdv.parentNode.removeChild(rdv);sendreplydetail(); };
            vdv.appendChild(rpsave);
//            $('#fileupld').show();

            var strr = attachidden;
            var strarrayr = strr.split(',');


            for (var i = 0; i < strarrayr.length; i++) {
                
            }



            //                                     for (var k = 0; k < s.length; k++) {
            //                    item = s[k].trim();
            //                  
            //                    if (!map[item]) {
            //                        attachresult.push(item);
            //                        map[item] = true;
            //                    }


            //}
            //var atdiv  = document.getElementById('attachmentdiv');//input
            for (var l = 0; l < strarrayr.length; l++) { //loop through the array and add each rate to the dropdown0

                $('#atdivvr').empty();

                var atdivr = document.createElement('div');

                var elem1r = document.createElement("div");
                elem1r.className = 'glyphicon glyphicon-picture';


                elem1r.onclick = function (e) { alert('img1click'); };
                var elem2r = document.createElement("a");

                elem2r.setAttribute('href', "DownloadHandler.ashx?ImageName='" + strarrayr[l] + "'");
                elem2r.innerHTML = strarrayr[l];
                //    elem2.innerHTML = attachresult[l];
                //    elem2.setAttribute("value",attachresult[l]);
                //   elem2.innerHTML='Attach';
                elem2r.className = 'attach';
                //    elem2.className='form-control';
                //  		elem2.setAttribute('type', 'text');// 
                //elem2.setAttribute("height", "18");
                //elem2.setAttribute("width", "50");
                //elem2.setAttribute("color", "#ccc");

                //elem2.setAttribute("font-size", "smaller");

                // elem2.onclick = function (e) { alert('imgclick');};

                var elemr = document.createElement("div");
                elemr.className = 'glyphicon glyphicon-save';
                //elem.setAttribute("height", "20");
                //elem.setAttribute("width", "50");

                elemr.onclick = function (e) { alert('imgclick'); };
                //		var ptag = document.createElement('p');// create ptag
                atdivr.id = 'atdivvr';
                atdivr.appendChild(elem1r);
                atdivr.appendChild(elem2r);
                //atdiv.className='attach';
                atdivr.appendChild(elemr);



                vdv.appendChild(atdivr);
            }
//            var gg = document.getElementById('#fileupld');
           
        }
//        function forwardmessage() {

//            $("#popupdiv").hide();
//            $("#replydiv").hide();
//            wdv = document.getElementById("frddiv");
//            wdv.style = "width:572px;height:530px;"
//            wdv.className = 'popup';
//            var dt = new Date();
//            document.getElementById("ctl00_ContentPlaceHolder1_datetimefrd").innerHTML = dt.toLocaleTimeString();
//            //            var dt = new Date();
//            //            document.getElementById("lblTime").innerHTML = dt.toLocaleTimeString();
//            //            window.setTimeout("ShowCurrentTime()", 1000);
//            //            element.text = 
//            var frdcancel = document.createElement('div');
//            frdcancel.id = "frdcancl";
//            frdcancel.className = 'cancel'; frdcancel.innerHTML = 'Close';
//            frdcancel.onclick = function (e) { $("#frddiv").hide(); kcdv = document.getElementById("frdcancl"); wdv.className = ''; kcdv.parentNode.removeChild(kcdv); rdv = document.getElementById("repp"); dv.className = ''; rdv.parentNode.removeChild(rdv); cdv = document.getElementById("canl"); dv.className = ''; cdv.parentNode.removeChild(cdv); };

//            wdv.appendChild(frdcancel);


//        }
//  
        function forwardmessage() {

            $("#popupdiv").hide();
            $("#replydiv").hide();
            wdv = document.getElementById("frddiv");
            wdv.style = "width:572px;height:530px;"
            wdv.className = 'popup';
            var dt = new Date();
            document.getElementById("ctl00_ContentPlaceHolder1_datetimefrd").innerHTML = dt.toLocaleTimeString();
            //            var dt = new Date();
            //            document.getElementById("lblTime").innerHTML = dt.toLocaleTimeString();
            //            window.setTimeout("ShowCurrentTime()", 1000);
            //            element.text = 
            fwdv = document.getElementById("flp");
            wdv.appendChild(fwdv);
            var frdcancel = document.createElement('div');
            frdcancel.id = "frdcancl";
            frdcancel.className = 'cancel'; frdcancel.innerHTML = 'Close';
            frdcancel.onclick = function (e) { kcdv = document.getElementById("frdcancl"); wdv.className = ''; kcdv.parentNode.removeChild(kcdv); rdv = document.getElementById("repp"); dv.className = ''; $("#frddiv").hide(); };

            wdv.appendChild(frdcancel);
            var fdsave = document.createElement('div');
            fdsave.id = "fsend";
            fdsave.className = 'cancel'; fdsave.innerHTML = 'Send';
            fdsave.onclick = function (e) { $("#frddiv").hide(); kcdv = document.getElementById("frdcancl"); wdv.className = ''; kcdv.parentNode.removeChild(kcdv); sendforwarddetail(); gv = document.getElementById("fsend"); wdv.className = ''; gv.parentNode.removeChild(gv); rdv = document.getElementById("repp"); dv.className = ''; rdv.parentNode.removeChild(rdv); };
            wdv.appendChild(fdsave);
            var s = [];
            var attachresult = [], map = {}, item;
        
            //             $.ajax({
            //                    type: "POST",
            //                    contentType: "application/json;charset=utf-8",
            //                    url: "genaeratingattach.aspx/Getattach",
            //                     data:JSON.stringify({selectValue}),
            //                    dataType: "json",
            //                    success: function (data) {



            //                    if (data.d.length > 0) {

            //                        for (var i = 0; i < data.d.length; i++) {
            //                         s[i]=data.d[i].Mail_Attachement;
            //                         
            //                        }
           
            var str = attachidden;
        var strarray = str.split(',');
        for (var i = 0; i < strarray.length; i++) {
          
        }



//                                     for (var k = 0; k < s.length; k++) {
//                    item = s[k].trim();
//                  
//                    if (!map[item]) {
//                        attachresult.push(item);
//                        map[item] = true;
//                    }


            //}
        //var atdiv  = document.getElementById('attachmentdiv');//input 
        for (var l = 0; l < strarray.length; l++) { //loop through the array and add each rate to the dropdown0

            $('#atdivv').empty();

                var atdiv = document.createElement('div');
               
                var elem1 = document.createElement("div");
                elem1.className = 'glyphicon glyphicon-picture';
                //elem.setAttribute("height", "20");
                //elem.setAttribute("width", "50");
                var h = 'Wildlife.wmv';
                elem1.onclick = function (e) { alert('img1click'); };
                var elem2 = document.createElement("a");
                //   var aTag = document.createElement('a');
                elem2.setAttribute('href', "DownloadHandler.ashx?ImageName='" + strarray[l] + "'");
                elem2.innerHTML = strarray[l];
                //    elem2.innerHTML = attachresult[l];
                //    elem2.setAttribute("value",attachresult[l]);
                //   elem2.innerHTML='Attach';
                elem2.className = 'attach';
                //    elem2.className='form-control';
                //  		elem2.setAttribute('type', 'text');// 
                //elem2.setAttribute("height", "18");
                //elem2.setAttribute("width", "50");
                //elem2.setAttribute("color", "#ccc");

                //elem2.setAttribute("font-size", "smaller");

                // elem2.onclick = function (e) { alert('imgclick');};

                var elem = document.createElement("div");
                elem.className = 'glyphicon glyphicon-save';
                //elem.setAttribute("height", "20");
                //elem.setAttribute("width", "50");

                elem.onclick = function (e) { alert('imgclick'); };
                //		var ptag = document.createElement('p');// create ptag
                atdiv.id = 'atdivv';
                atdiv.appendChild(elem1);
                atdiv.appendChild(elem2);
                //atdiv.className='attach';
                atdiv.appendChild(elem);

                //		atdiv.setAttribute('type', 'Label');// add type radio
                //		atdiv.setAttribute('value', attachresult[l]);
                //        atdiv.setAttribute('id', 'res');

                //        	atdiv.setAttribute('text',attachresult[l]);// add rate value
                //		atdiv.setAttribute('name', 'attfile');// add rate name
                //        input.setAttribute("oncheck", runCommand());
                //       	 di = document.createElement('div');

                //		

                //		

                wdv.appendChild(atdiv);

            }




            //                    },
            //                    error: function ajaxError(result) {
            //                        alert(result.status + ' : ' + result.statusText);
            //                    }
            //                });





            //            return false;

        }
  
  
</script>
   
  <script type="text/javascript">

      function sendforwarddetail() {
          var at;
          var fatch = document.getElementById("dvFileName").innerHTML;
          var file = $get('ctl00_ContentPlaceHolder1_gvNewFiles').getElementsByTagName('a');
          for (var t = 0; t < file.length; t++) {
              at += file[t].innerHTML + "'";
              alert(at);
//              if (file[t].innerHTML == selectedFile[selectedFile.length - 1]) {
//                  return file[t].innerHTML;
//              }
          }
      var fld = document.getElementById("ctl00_ContentPlaceHolder1_TextBox3").value;
      var bccval = document.getElementById("bcctextboxfrd").value;
      var ccval = document.getElementById("cctextboxfrd").value;
         var msgg= document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").innerHTML;

          var sub= document.getElementById("ctl00_ContentPlaceHolder1_subfrd").innerHTML;
        $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "attachementwithajax.aspx/sendforwarddetails",
                    data:JSON.stringify({feildfrce:fld,bcc :bccval, cc:ccval, subj:sub, message:msgg, attach:attachidden}),
                    dataType: "json",
                    success: function (data) {
                       
                    },
                    error: function ajaxError(result) {
                        alert(result.status + ' : ' + result.statusText);
                    }
                });

      }
  </script>

   <script type="text/javascript">

       function sendreplydetail() {

           var rfld = document.getElementById("ctl00_ContentPlaceHolder1_TextBox3").value;
           var rbccval = document.getElementById("bcctextboxfrd").innerHTML;
           var rccval = document.getElementById("cctextboxfrd").innerHTML;
           var rmsgg = document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").innerHTML;

           var rsub = document.getElementById("ctl00_ContentPlaceHolder1_subfrd").innerHTML;
           $.ajax({
               type: "POST",
               contentType: "application/json;charset=utf-8",
               url: "attachementwithajax.aspx/sendreplydetails",
               data: JSON.stringify({ feildfrce: rfld, bcc: rbccval, cc: rccval, subj: rsub, message: rmsgg, attach: attachidden }),
               dataType: "json",
               success: function (data) {

               },
               error: function ajaxError(result) {
                   alert(result.status + ' : ' + result.statusText);
               }
           });

       }
  </script>
 
 
 
<script type="text/javascript">
   
        
        function tSpeedValue(button)
         {

            var discode = document.getElementById("<%=Searchtxt.ClientID %>");
               var gridid ='ctl00_ContentPlaceHolder1_GridView1';

                var selectValue = discode.value;

        alert(selectValue);
              $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "attachementwithajax.aspx/GetData",
                     data:JSON.stringify({selectValue}),
                    dataType: "json",
                    success: function (data) {

                   
                    var rw=$("#ctl00_ContentPlaceHolder1_GridView1").find("tr")[0];
                    $("#ctl00_ContentPlaceHolder1_GridView1").empty();
                    if (data.d.length > 0) {
                         $("#ctl00_ContentPlaceHolder1_GridView1").append(rw);
//                            var counter=1;
//    var chk="<input type='checkbox' id='chk_'"+counter+"'/>";

//    $("table#t tr td").each(function(){
//        $(this).append(chk);
//        counter++;
//    });
                       
                        for (var i = 0; i < data.d.length; i++) {
                         var chk="<input type='checkbox' id='chk_'"+i+"'/>";
//                        var   x = document.createElement("INPUT");
//                     x.setAttribute("type", "checkbox");
//x.setAttribute("id", i);
//                      td.appendChild(chk);
////    tr.appendChild(td);
//    $("#ctl00_ContentPlaceHolder1_GridView1").append(tr);

                            $("#ctl00_ContentPlaceHolder1_GridView1").append("<tr><td>"+ chk + "</td><td>" + 
                            data.d[i].Mail_Subject + "</td> <td>" + 
                            data.d[i].Mail_SF_From + "</td> <td>" + 
                            data.d[i].Mail_Content + "</td> <td>" + 
                            data.d[i].Mail_Sent_Time +  "</td> <td style = 'display:none'>" + 
                            data.d[i].Trans_Sl_No + "</td> <td style = 'display:none'>" + 
                            data.d[i].Mail_Attachement + "</td></tr>");
                        }
                    }
                    },
                    error: function ajaxError(result) {
                        alert(result.status + ' : ' + result.statusText);
                    }
                });

     };

   
  

</script>
<script type="text/javascript">
    $(function () { /* DOM ready */
        $('#<%=DropDownList1.ClientID%>').change(function () {
            var discode = document.getElementById("<%=DropDownList1.ClientID %>");
            var selectValue = discode.options[discode.selectedIndex].innerHTML;
            alert(selectValue);
            alert('Are to sure to move to folder');
            var gv = document.getElementById("<%=GridView1.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            //         alert(chk.length);

            //           for(var i=0;i<chk.length;i++)
            //       {
            //       if(chk[i].type == "checkbox")
            //        {
            //         
            //         if (chk[i].checked) {
            $('table tr td :checkbox:checked').map(function () {
                var $this = $(this);

                $this.closest('tr').each(function () {
                    var hdn = $this.closest('tr').find('input[type="hidden"]').val();
                    alert(hdn);
                    //                      var roww = $(chk[i]).closest("tr");
                    //                      roww.remove();
                    $.ajax({

                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        async: false,
                        url: "attachementwithajax.aspx/Movetofolder",
                        data: JSON.stringify({ trno: hdn, movefolder: selectValue }),
                        //                    data:JSON.stringify({hdn},{selectValue}),
                        dataType: "json",
                        success: function (r) {
                            if (r.d) {

                                for (var i = 0; i < chk.length; i++) {

                                    if (chk[i].checked) {



                                        //             alert("ggg");
                                        var rowe = $(chk[i]).closest("tr");
                                        rowe.remove();
                                        i = 0;
                                    } 
                                }

                                if ($("[id*=GridView1] td").length == 0) {
                                    $("[id*=GridView1] tbody").append("<tr><td colspan = '4' align = 'center'>No records found.</td></tr>")
                                }
                                //                        alert("Customer record has been deleted.");
                            }
                        }
                ,
                        error: function ajaxError(result) {
                            alert("failure");
                        }
                    });

                    //                        cv = $(this).find("td:last").text();
                    //                 bv = $(this).find("td").text().find("input:hidden");
                    //                        cv = $(this).find("td").text();
                    //                         alert(cv); 
                    //                        var string_length = cv.length;

                });

            });


            alert("Moved sucessfully");

            var trs = $('table tr');

            //alert(trs); 
            var values = trs.first().find('td');
            var cv = "null";

            //              alert(values);
            //             alert("ggg");

            //               row.remove();
            //             alert(roww);
            //               i=0;

            selectValue = '3';


            //                }
            //       }
            //               }
            return false;





        });
    });
//    function rowb() {
//    }
//    $(function () {
//        $(".table1").bind("mouseover", function () {
//            $(this).css("background-color", "#d6e9c6");
//        });
//        $(".table1").bind("mouseout", function () {
//            $(this).css("background-color", "transparent");

//        });
//    });
    </script>
   
   <script type="text/javascript">
       //function downgg() {
       //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").innerHTML = '~/attach/88061c62e935ddfd7dea9e75c5d027cf.jpg';

       //}

       function getvalue() {
           var att = document.getElementById("ctl00_ContentPlaceHolder1_attachmentfile").innerHTML;
           alert(att);
           document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = att;
       }
   </script>
 
    <meta charset="utf-8">

  
        <meta name="viewport" content="width=device-width, initial-scale=1">
   <%-- <link href="http://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet">--%>
    <style type="text/css">
        body{ margin-top:50px;}
.nav-tabs .glyphicon:not(.no-margin) { margin-right:10px; }
.tab-pane .list-group-item:first-child {border-top-right-radius: 0px;border-top-left-radius: 0px;}
.tab-pane .list-group-item:last-child {border-bottom-right-radius: 0px;border-bottom-left-radius: 0px;}
.tab-pane .list-group .checkbox { display: inline-block;margin: 0px; }
.tab-pane .list-group input[type="checkbox"]{ margin-top: 2px; }
.tab-pane .list-group .glyphicon { margin-right:5px; }
.tab-pane .list-group .glyphicon:hover { color:#FFBC00; }
a.list-group-item.read { color: #222;background-color: #F3F3F3; }
hr { margin-top: 5px;margin-bottom: 10px; }
.nav-pills>li>a {padding: 5px 10px;}

.ad { padding: 5px;background: #F5F5F5;color: #222;font-size: 80%;border: 1px solid #E5E5E5; }
.ad a.title {color: #15C;text-decoration: none;font-weight: bold;font-size: 110%;}
.ad a.url {color: #093;text-decoration: none;}
    </style>
    <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="http://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>

<body>
<form id="Form1" runat="server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release">
    </asp:ToolkitScriptManager>
<div class="container">
    <div class="row">
        <div class="col-sm-3 col-md-2">
            <div class="btn-group">
               <a href=".http://trial.sanfmcg.com/E-Report_DotNet/Default2.aspx" class="btn btn-primary dropdown-toggle">Home<span class="glyphicon glyphicon-home"></span></a>
                <ul class="dropdown-menu" role="menu">
                    <li><a href="#">Mail</a></li>
                    <li><a href="#">Contacts</a></li>
                    <li><a href="#">Tasks</a></li>
                </ul>
            </div>
        </div>
     
        <div class="col-sm-9 col-md-10">
            <!-- Split button -->
            <div class="btn-group">
               <%-- <button type="button" class="btn btn-default">
                    <div class="checkbox" style="margin: 0;">
                        <label>
                            <input type="checkbox">
                        </label>
                    &nbsp;&nbsp;&nbsp;</div>
                </button>--%>



               <%-- <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                    <span class="caret"></span><span class="sr-only">Toggle Dropdown</span>
                </button>--%>
                <ul class="dropdown-menu" role="menu" id="folderdrop">
                    <li><a href="#">All</a></li>
                    <li><a href="#">None</a></li>
                    <li><a href="#">Read</a></li>
                    <li><a href="#">Unread</a></li>
                    <li><a href="#">Starred</a></li>
                    <li><a href="#">Unstarred</a></li>
                </ul>
            </div>
            <button type="button" class="btn btn-default" data-toggle="tooltip" title="Refresh">
                &nbsp;&nbsp;&nbsp;<span class="glyphicon glyphicon-refresh"></span>&nbsp;&nbsp;&nbsp;</button>
                &nbsp&nbsp&nbsp&nbsp&nbsp
            <!-- Single button -->
           <%-- <asp:LinkButton ID="LinkButton5" runat="server" OnClientClick="movetofolder();">LinkButton</asp:LinkButton>--%>
            <div class="btn-group">
<%--                <asp:Button ID="Button5" class="btn btn-default dropdown-toggle"  runat="server" onclientclick="return movetofolder();" Text="MoveTO" /><span class="glyphicon glyphicon-folder-open"></span></button>--%>
            <%--    <button type="button" id ="move" class="btn btn-default dropdown-toggle"  runat="server"   >
                    MoveTO 
                </button>--%>
               <%-- <button class="btn btn-primary dropdown-toggle"  id="menu1" type="button" data-toggle="dropdown"  >--%>
    <button class="btn btn-primary"   type="button"   >
    <asp:DropDownList ID="DropDownList1" 
         runat="server"   style="color:Black;background-color: #f5f5f5;font-family:-webkit-pictograph;border:solid;border-color:bisque;width: 99px; padding-bottom: 0px; padding-top: 2px;border-style: groove; font-size:small" >
           
          
          </asp:DropDownList> <span class="glyphicon glyphicon-folder-open"></span> 

            <%--<asp:DropDownList ID="ddd"   runat="server">
           
          
          </asp:DropDownList> <span class="glyphicon glyphicon-folder-open"></span> --%>
         
      </button>
              &nbsp&nbsp&nbsp&nbsp&nbsp
     <%-- </button>--%>
     
                <ul class="dropdown-menu" role="menu">
                    <li><a href="#">Mark all as read</a></li>
                    <li class="divider"></li>
                    <li class="text-center"><small class="text-muted">Select messages to see more actions</small></li>
                </ul>
                 <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/remove-icon-png-26.png"  style="padding-top:8px;" 
                          OnClientClick = "return ConfirmDelete();" Height                  
        ="32px" Width="28px"/>
            </div>
        
          
            <div class="pull-right">
             <asp:Label ID="Search" runat="server" Text="Search" Font-Bold="True" 
                    Font-Names="-webkit-pictograph" ForeColor="#136798"></asp:Label><asp:Image ID="Image1" width="28px" Height="28px" runat="server" ImageUrl="~/Images/placeholder.png" /><asp:TextBox ID="Searchtxt" onchange="tSpeedValue(this)"
                    runat="server" BorderStyle="Solid" BorderWidth="1px"  
                    BorderColor="#3399FF"  placeholder="Search Message by Subject" 
                    Width="250px" 
                    Height="30px" Font-Names="Pristina"></asp:TextBox><i class="glyphicon glyphicon-search"></i>
               <%-- <span class="text-muted"><b>1</b>–<b>50</b> of <b>277</b></span>--%>
                <div class="btn-group btn-group-sm">
                    <button type="button" class="btn btn-default">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                    </button>
                    <button type="button" class="btn btn-default">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                    </button>
                </div>
            </div>
        </div>
        
    </div>
    <hr />
    <div class="row">
        <div class="col-sm-3 col-md-2">
        <asp:Button ID="Button1" class="btn btn-danger btn-sm btn-block" runat="server" Text="COMPOSE" />
       
            <hr />
            <ul class="nav nav-pills nav-stacked">
                <li class="active"><a onServerClick="Inbox_Click"  runat="server" class="btn" style="
    padding-right: 101px;">&nbsp<i class="glyphicon glyphicon-inbox"><span class="notification-label notification-label-red"><asp:Label ID="mailcount"  runat="server"  class="button tiny"></asp:Label><br>
<span class="glyphicon glyphicon-envelope" style=" width: 16px;padding-top: 1px;"></span></span></i> Inbox </a>
                </li>
              
          <%--      <li><a href="http://www.jquery2dotnet.com">Starred</a></li>--%>
              <%--  <li><a href="http://www.jquery2dotnet.com">Important</a></li>--%>
                <li ><a onServerClick="btnSentItem_Click"  runat="server"><span class="glyphicon glyphicon-envelope"></span>&nbsp;&nbsp;&nbsp;&nbsp;Sent Mail</a></li>
                                <li><a href="http://www.jquery2dotnet.com"><span class="fa fa-eye"></span>&nbsp;&nbsp;&nbsp;&nbsp;Viewed Mail</a></li>
               <%-- <li><a href="http://www.jquery2dotnet.com"><span class="badge pull-right">3</span>Drafts</a></li>--%>
              <li><a href="http://www.jquery2dotnet.com"><span class="glyphicon glyphicon-folder-open">3</span>My Folders</a></li>  
                 <%--<div class="MenuBar">--%>
                 <table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>  <asp:Menu ID="menuBar" runat="server" Orientation="vertical" Width="100%"   OnMenuItemClick="OnMenuItemClick" >
		<DynamicHoverStyle    />
		<DynamicMenuItemStyle VerticalPadding="10" HorizontalPadding="20"/>
		<DynamicSelectedStyle  />
       

		<StaticHoverStyle   BackColor="#AFD8D8" />
         
		<StaticMenuItemStyle ItemSpacing="3px"  VerticalPadding="40" HorizontalPadding="40" BorderStyle="NotSet" BackColor="CadetBlue" ForeColor="White" Height="25px" Width="80px" />
		<StaticSelectedStyle  />
	</asp:Menu><%--<span class="glyphicon glyphicon-folder-open"></span>--%></td></tr></table>
	  

            </ul>
        </div>
        <div class="col-sm-9 col-md-10">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#home" data-toggle="tab" style="padding-bottom: 19px;"
><span class="glyphicon glyphicon-inbox">
                </span>Mails</a></li>
               <table><tr><td width="450px"></td><td> <div class="gradient" id="tdDate" style="">
               </br>
               <%--<select id="ddlMon"  Width="120px" Height="10px" class="select" >
    <option value="hide">-- Month --</option>
    <option value="january" rel="icon-temperature">January</option>
    <option value="february">February</option>
    <option value="march">March</option>
    <option value="april">April</option>
    <option value="may">May</option>
    <option value="june">June</option>
    <option value="july">July</option>
    <option value="august">August</option>
    <option value="september">September</option>
    <option value="october">October</option>
    <option value="november">November</option>
    <option value="december">December</option>
</select> --%>
                                                                                    <asp:DropDownList ID="ddlMon" 
                                                                                        AutoPostBack="true"  CssClass="select" runat="server">
                                                                                        <asp:ListItem Text="January" Value="1">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="February" Value="2">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="March" Value="3">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="April" Value="4">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="May" Value="5">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="June" Value="6">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="July" Value="7">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="August" Value="8">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="September" Value="9">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="October" Value="10">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="November" Value="11">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="December" Value="12">
                                                                                        </asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <asp:DropDownList ID="ddlYr" AutoPostBack="true" 
                                                                                        CssClass="select"  runat="server">
                                                                                    </asp:DropDownList>
                                                                                </div><canvas id="page">Your Browser does not support canvas!</canvas></td></tr></table>
             
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
                <div class="tab-pane fade in active" id="home">
                 
                       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"   EnableViewState="false" GridLines="Horizontal"  HeaderStyle-BackColor="#14B1BB" HeaderStyle-Font-Names=" -webkit-pictograph;" HeaderStyle-BorderColor="#00c0ef" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="Solid"
             Width="100%" EmptyDataText="No results found" EmptyDataRowStyle-Font-Size="Large" AllowPaging="true" style="border-style: solid;border-color: #ccc;width:100%;border-collapse:collapse;"
                          CssClass="table table-hover"        BorderStyle="None" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center">
                          <Columns>

                          <asp:TemplateField>
        <%--<HeaderTemplate>
            <asp:CheckBox ID="chkAll" runat="server"
              />
        </HeaderTemplate>--%>
        <ItemTemplate>
            <asp:CheckBox ID="chk" runat="server" onclick="javascript: calculateTotal(this);" />
        </ItemTemplate>
    </asp:TemplateField>


                          <%-- <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>--%>
                        <%--   <asp:TemplateField ItemStyle-Width="60" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox2" runat="server" CssClass="mycheckBig"/>
            </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" Width="60px" />
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Subject" ItemStyle-Height="30px">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server"   ForeColor="Black"><%# Eval("Mail_Subject")%></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="From" ItemStyle-Height="30px">
        <ItemTemplate>
          <asp:LinkButton ID="LinkButton2" runat="server"    ForeColor="Black"><%# Eval("Mail_SF_Name")%></asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Message" ItemStyle-Height="30px">
        <ItemTemplate>
         <asp:LinkButton ID="LinkButton3" runat="server"    ForeColor="Black"><%# Eval("Mail_Content")%></asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>
<%--        <asp:BoundField DataField="Status" ItemStyle-Height="30px" HeaderStyle-Height="30px" >
                              <HeaderStyle Height="30px" />
                              <ItemStyle Height="30px" />
                              </asp:BoundField>--%>
        <%-- <asp:BoundField DataField="MessageId" ItemStyle-Height="30px" HeaderStyle-Height="30px">
       
                              <HeaderStyle Height="30px" />
                              <ItemStyle Height="30px" />
                              </asp:BoundField>--%>
       <asp:TemplateField HeaderText="Date" ItemStyle-Height="30px">
     <ItemTemplate>
       <asp:LinkButton ID="LinkButton4" runat="server"   ForeColor="Black"><%# Eval("Mail_Sent_Time")%></asp:LinkButton>
     </ItemTemplate>
       </asp:TemplateField>
            <asp:TemplateField  ItemStyle-Height="30px">
     <ItemTemplate>                 
          <asp:HiddenField runat="server" ID="testLabel" Value='<%# Bind("Trans_Sl_No") %>' /> 
          </ItemTemplate>
       </asp:TemplateField> 
          <asp:TemplateField   ItemStyle-Height="30px">
     <ItemTemplate>
      <asp:HiddenField runat="server" ID="att" Value='<%# Bind("Mail_Attachement") %>' /> 
    
    <%--   <asp:LinkButton ID="att" runat="server"    ForeColor="Black"><%# Eval("Mail_Attachement")%></asp:LinkButton>--%>
     </ItemTemplate>
       </asp:TemplateField> 
      
           <%-- <asp:BoundField DataField="Mail_Attachement"  ItemStyle-Height="30px" HeaderStyle-Height="30px">
               
                              <HeaderStyle  />
                              <ItemStyle Height="30px" />
                              </asp:BoundField>
             <%--  <asp:TemplateField HeaderText="DateTime" ItemStyle-Height="30px">
     <ItemTemplate>
      <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup"  />
     </ItemTemplate>
       </asp:TemplateField>--%>
         <%--<asp:TemplateField ItemStyle-Width="30">
            <ItemTemplate>
                           
                <asp:Image ID="Image1" runat="server"  ImageUrl="../Images/paperclip.png"  />
            </ItemTemplate>
             <ItemStyle Width="30px" />
        </asp:TemplateField>--%>
                          </Columns>
                           <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" HorizontalAlign="Center" />
                         <%--  <HeaderStyle BackColor="#14B1BB" Font-Size="Small"  ForeColor="White" Height="30px" Font-Names="-webkit-pictograph" />--%>
                           
                          </asp:GridView>  
                          


                               <asp:GridView ID="grdDemo"    CssClass="Grid"  GridLines="Horizontal"   runat="server">
                              <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" HorizontalAlign="Center" />

                           <HeaderStyle BackColor="#14B1BB" Font-Size="Small"  ForeColor="White" Height="30px" Font-Names="-webkit-pictograph" />
    </asp:GridView>
                             <asp:modalpopupextender ID="mpe" runat="server" BehaviorID="pnlPopupp" PopupControlID="pnlPopupp" TargetControlID="Button1" 
     CancelControlID="btnClose"   
        BackgroundCssClass="modalBackground" >
</asp:modalpopupextender>



<%--
<asp:modalpopupextender ID="modal" runat="server" BehaviorID="panelmsg" PopupControlID="panelmsg" TargetControlID="btnShow" 
     CancelControlID="btnClose"   
        BackgroundCssClass="modalBackground" >
</asp:modalpopupextender>--%>

<asp:Panel ID="pnlPopupp" runat="server"   CssClass="modalPopup" Style="display: none;width:650px; height:500px; overflow:scroll;   "  >

   <div class="col-md-9">
              <div class="box box-primary">
                <div class="box-header with-border">
                  <h4 class="text-center" style="font:bold;">Compose Your Mail!!</h4>
                  <div class="box-tools pull-right">
                   
                  </div><!-- /.box-tools -->
                </div><!-- /.box-header -->
                <div class="box-body no-padding">
                  
                  <div class="table-responsive mailbox-messages">
                   <%-- <table class="table table-hover table-striped">--%>
                         <div class="box-body">
                  <div class="form-group">
                  <asp:UpdatePanel ID="UpdatePanel1"  runat ="server">
                  <ContentTemplate>
                  <table><tr><td>                    <asp:Label ID="fldforcelbl" runat="server" Text="Select Fieldforce"  ForeColor="#00C0EF"></asp:Label></td></td></tr><tr><td>  <asp:TextBox ID="txtAddr" runat="server"  Width="525px" class="form-control"
                                                        ></asp:TextBox><td>
                                                            <asp:Button ID="Button6" runat="server" Text="Search" CausesValidation="false"  class="btn btn-primary" OnClientClick="return Loadaddress(this.id)"/></td><%--<td>  
                                                        <asp:ImageButton ID="imgAddressBook" runat="server" ImageUrl="~/Images/search1.png"  OnClientClick="Loadaddress()" Width="45px" Height="25px"/>
                                                                 </td>--%></tr></table>
                                                                 </ContentTemplate></asp:UpdatePanel>
                     <table><tr><td> <asp:LinkButton ID="LinkButton5" runat="server" OnClientClick="return ccaddress()" CausesValidation="false">Add Cc</asp:LinkButton></td><td><asp:LinkButton
                          ID="LinkButton6" runat="server" OnClientClick="return Bccaddress()" CausesValidation="false">Add Bcc</asp:LinkButton></td></tr></table>

                 <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Fieldforce" ForeColor="#00C0EF" ControlToValidate="to"></asp:RequiredFieldValidator>
                      <asp:TextBox ID="to" runat="server" class="form-control"   Width="800px"></asp:TextBox>--%>
                      
                     
                                                          
                                                           <%-- <asp:Button ID="imgAddressBook" runat="server" Height="20px" Width="24px"  UseSubmitBehavior="false" style="background-image: url(../../images/Address_Book_Icon.gif);border:0" OnClick="imgAddressBook_Click" />--%>
                                                      
                  </div>
                  <div id="DivFree" class="form-group" style="display:none;">
                  <table><tr><td><input type="text" id="bcctextbox" class="form-control"  name="txtFree"  style="width:500px; height:12px;" /></td><td><asp:Button ID="Button7" runat="server" Text="Add Bcc" CausesValidation="false"  class="btn btn-primary" OnClientClick="return Loadaddress(this.id)"/></td></tr></table>
				
			</div>
			<div id="DivPaid" class="form-group" style="display:none;">
            <table><tr><td>	<input type="text" name="txtPaid" id="cctextbox"  class="form-control"  style="width:500px;height:12px;" /></td><td>			<asp:Button ID="Button8" runat="server" Text="Add Cc" CausesValidation="false"  class="btn btn-primary" OnClientClick="return Loadaddress(this.id)"/></td></tr></table>

			</div>
                  <div class="form-group">

                     <asp:Label ID="sbjlbl" runat="server" Text="Subject"  ForeColor="#00C0EF"></asp:Label>
                 <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="subject" ForeColor="#00C0EF" ControlToValidate="subject"></asp:RequiredFieldValidator>--%>
                      <asp:TextBox ID="subject" runat="server" class="form-control" Width="800px"></asp:TextBox>
                  </div>
                  <div class="form-group">
                      <asp:Label ID="msglbl" runat="server" Text="Message"  ForeColor="#00C0EF"></asp:Label>
<%--                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Message" ControlToValidate="message1"></asp:RequiredFieldValidator>--%>
                      <asp:TextBox ID="message1" runat="server" class="form-control"  
                          TextMode="MultiLine" Width="800px" Height="200px" 
                         ></asp:TextBox>
                    
                  </div>
                  
                </div>  
                <div id="cmpflp">
              <table width="620px" cellpadding="5" cellspacing="5" border="0" id="flp"  >
        <tr>
            <td>
                <table class="ContainerWrapper" border="0" cellpadding="2" cellspacing="0" width="100%">
                    <tr class="ContainerHeader">
                        <td>
                            File upload control
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerMargin">
                            <table class="Container" cellpadding="0" cellspacing="4" width="100%" border="0">
                                <tr>
                                    <td>
                                        <div id="dvUploader">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 50%">
                                                        <iframe id="uploadFrame" frameborder="0" height="25" width="200" scrolling="no" src="UploadEngine.aspx">
                                                        </iframe>
                                                    </td>
                                                    <td>
                                                        <input id="upload" type="button" value="Upload"  />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                               <tr><td>&nbsp</td></tr>
                                <tr>
                                    <td>
                                        <table id="tblMessage" cellpadding="4" cellspacing="4" class="Information" border="0">
                                            <tr>
                                                <td style="text-align: left" colspan="2">
                                                    <div id="dvIcon" class="Information">
                                                        Please select a file to upload
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="2" width="100%" border="0">
                                            <tr>
                                                <td style="width: 100px; text-align: left">
                                                    Progress
                                                </td>
                                                <td style="width: auto">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <div id="dvProgressContainer">
                                                                    <div id="dvProgress">
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div id="dvProgressPrcent">
                                                                    0%
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    Download Bytes
                                                </td>
                                                <td align="right">
                                                    <div id="dvDownload">
                                                        Bytes
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    File Name
                                                </td>
                                                <td align="right">
                                                    <div id="dvFileName">
                                                        FileName
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="ContainerWrapper" border="0" cellpadding="2" cellspacing="0" width="100%">
                    <tr class="ContainerHeader">
                        <td>
                            List of uploaded files
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerMargin">
                            <asp:UpdatePanel runat="server" ID="upFiles" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdRefereshGrid" runat="server" OnValueChanged="hdRefereshGrid_ValueChanged" />
                                    <table class="Container" cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr class="GridHeader">
                                            <td class="Separator" style="width: 5%;" align="right">
                                            </td>
                                            <td class="Separator" style="width: 69%">
                                                File
                                            </td>
                                            <td class="Separator" style="width: 18%" align="right">
                                                Size
                                            </td>
                                            <td style="width: 4%">
                                            </td>
                                            <td style="width: 4%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <div style="height: 140px; overflow: auto;">
                                                    <asp:GridView DataKeyNames="Name" ID="gvNewFiles" AllowPaging="false" runat="server"
                                                        PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false" Width="100%"
                                                        CellPadding="0" BorderWidth="0" GridLines="None" ShowHeader="false" OnRowCommand="gvNewFiles_RowCommand"
                                                        OnRowDataBound="gvNewFiles_RowDataBound">
                                                        <AlternatingRowStyle CssClass="GridAlternate" />
                                                        <RowStyle CssClass="GridNormalRow" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                        <tr>
                                                                            <td class="GridNumberRow" style="width: 5%;" align="right">
                                                                                <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                                            </td>
                                                                            <td style="width: 63%; padding-left: 2px;" align="left">
                                                                                <asp:LinkButton ToolTip='<%# String.Format("Download {0}",Eval("Name")) %>' runat="server"
                                                                                    ID="lbtnFiles" Text='<%#Eval("Name") %>' CommandArgument='<%#Eval("Name") %>'
                                                                                    CommandName="downloadFile"></asp:LinkButton>
                                                                            </td>
                                                                            <td style="width: 22%" align="right">
                                                                                <%#Eval("ConvertedSize")%>
                                                                            </td>
                                                                            <td colspan="2" style="width: 5%" align="center">
                                                                                <asp:ImageButton Width="10" runat="server" ImageUrl="~/Images/Grid_ActionDelete.gif"
                                                                                    ID="imgBtnDel"  CommandName="deleteFile" CommandArgument='<%#Eval("Name") %>'
                                                                                    AlternateText="Delete" ToolTip="Delete File" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="GridEmptyRow" />
                                                        <EmptyDataTemplate>
                                                            <span>No file uploaded</span>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="GridFooter">
                                            <td colspan="5">
                                                <div style="float: left">
                                                    Total Files:
                                                    <%= gvNewFiles.Rows.Count  %>
                                                </div>
                                                <div style="float: right">
                                                    Total Size:
                                                    <asp:Label runat="server" ID="lblTotalSize" Text="0 K"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="gvNewFiles" EventName="RowCommand" />
                                    <%--<asp:PostBackTrigger ControlID="gvNewFiles" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div> 
                 
                </div>
                <br />
   <div align="center">
      <asp:Button ID="composesend" runat="server" Text="SEND" class="btn btn-primary"    />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <%--  </div>--%><asp:Button ID="btnClose" runat="server" Text="CANCEL" class="btn btn-primary"  />
                     
                          
          </div>        
                
              </div>
            
    
        
   </br>
   </div>
 </div>
</asp:Panel>




            <div id="popupdiv"  style="display:none;background-color:#FFFFFF; visibility:hidden; overflow:scroll"  > 
        
                      
                           <table  class="login-wrap" style="border: 1px solid #999999; background-color: rgba(66, 139, 202, 0.58);;width:552px; height: 20px;" ><tr><td><i class="fa fa-tachometer" aria-hidden="true"></i></td>    
                               <td><asp:Label ID="sub" runat="server" Text="Subject:-" Font-Bold="True" ForeColor="#0073b7" BorderStyle="Solid" BorderWidth="1px" BorderColor="#CCCCCC"></asp:Label></td><td><asp:Label ID="subj" runat="server"  style="font-family:-webkit-pictograph; align:center; font-size: large;"  ></asp:Label></td><td align="right">
                               <asp:Label ID="Datetime" runat="server"  
                                   style="background-color: rgba(250, 250, 250, 0.57);font-family: -webkit-pictograph;align:center;font-size: small;border-style: solid;border-bottom-width: 1px;border-color: #ddd;"></asp:Label></td></tr></table>
                                                  
                          </br>
                           <div class="box-body">
                               <div class="form-group">
                               <table><tr><td><i class="fa fa-user" aria-hidden="true"></i></td><td>                <asp:Label ID="Label2" runat="server" Text="From:"></asp:Label>&nbsp&nbsp&nbsp</td><td><asp:Label ID="from" runat="server"  style="width:300px;font-family: -webkit-pictograph;"></asp:Label></td>
                                 </tr></table>
 
                                                                          
                                       
                                     
                               </div>
                           
                               <div class="form-group">
                               <table><td><i class="fa fa-envelope" aria-hidden="true"></i></td><td> <asp:Label ID="mg" runat="server" Text="Message" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td></table>
                                  
                                   <asp:Label ID="message" runat="server" class="form-control" style="display:inline-block;background-color: rgba(51, 204, 204, 0.16);height: 208px;width: 530px;"></asp:Label>
                               </div>
                               <%--<asp:Panel ID="Panel2" runat="server" ImageUrl="~/Images/areo.png"   >
                            
                                  <%-- <asp:Button ID="Button3" runat="server" Text="Reply"  OnClientClick="return replymessage();" />--%>
                               <%--        <p class="help-block">
                                       </p>
                                 
                               </asp:Panel>--%>
                            <div id="vattach"></div>  
                      </div>   
                        
                                  
                   </div>
               
                   <div id="replydiv" style="display:none;" >
                   <table align="center" width="452px;"><tr  style="border: 1px solid #999999; background-color: #33CCCC;width:852px;height:20px;" ><td>&nbsp</td><td>  <asp:Label ID="Toid" runat="server" Text="To :" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td><td><i class="fa fa-reply" aria-hidden="true"></i></td><td> <asp:Label ID="tofeildforce"
                           runat="server"></asp:Label></td></tr></table>
                         <table>  <tr>&nbsp</tr>
                           <tr><td><i class="fa fa-tachometer" aria-hidden="true"></i></td><td><asp:Label ID="Label4" runat="server" Text="Subject :" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td><td>&nbsp</td><td> <asp:Label ID="tosubj"
                           runat="server" ></asp:Label></td></tr>
                             <tr>&nbsp</tr>
                           </table>
                 
                      </br>
                      <tr><td></td><i class="fa fa-envelope" aria-hidden="true"></i><td><asp:Label ID="mgg" runat="server" Text="Message" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td></tr>
                   

                       <asp:TextBox ID="TextBox1" runat="server"  class="form-control" placeholder="Type Your Message here!!" TextMode="MultiLine" style="display:inline-block;background-color:rgba(255, 255, 255, 0.15);height: 180px;width: 530px;" BorderStyle="Solid" BorderWidth="1px" BorderColor="#3399FF" Font-Italic="True"></asp:TextBox>
                     <div>
                     </br>
                        
                    
                
                   
                       <tr><td><i class="fa fa-paperclip" aria-hidden="true"></i></td><td> <asp:Label ID="Label13" runat="server" Text="Attachements" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td></tr>
                      
                  </div>
                </div>   
             <div id="frddiv" style="display:none;" >

                   <table align="center" width="452px;"><tr  style="border: 1px solid #999999; background-color: rgba(51, 204, 204, 0.26);width:852px;height:20px;" ><td width="69px">&nbsp</td><td>  <i class="fa fa-forward" aria-hidden="true"></i><i class="fa fa-forward" aria-hidden="true"></i><asp:Label ID="Label3" runat="server" Text="Forward Message" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td><td>&nbsp</td><td> <asp:Label ID="Label5"
                           runat="server"></asp:Label></td></tr></table>
                         <table>  <tr>&nbsp</tr>
                           <tr><td><i class="fa fa-tachometer" aria-hidden="true"></i></td><td><asp:Label ID="Label7" runat="server" Text="Subject :" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td><td>&nbsp</td><td> <asp:Label ID="subfrd"
                           runat="server" ></asp:Label></td><td width="100px">&nbsp;&nbsp;</td><td><i class="fa fa-calendar" aria-hidden="true"></i></td><td>
                                 <asp:Label ID="datetimefrdlabel" runat="server" Text="Date&Time:"  ForeColor="#3399FF" Font-Bold="True"></asp:Label></td><td>
                                     <asp:Label ID="datetimefrd" runat="server" ></asp:Label></td></tr>
                              <table><tr><td>                    <asp:Label ID="Label8" runat="server" Text="To Fieldforce"  ForeColor="#00C0EF"></asp:Label></td></td></tr><tr><td>  <asp:TextBox ID="TextBox3" runat="server"  Width="400px" class="form-control"
                                                        ></asp:TextBox><td>
                                                            <asp:Button ID="Button11" runat="server" Text="Search" CausesValidation="false"  class="btn btn-primary" OnClientClick="return Loadaddressfrd(this.id)"/>
                                                           <%-- <asp:LinkButton ID="Button11" runat="server" OnClientClick="return Loadaddressfrd(this.id)">Add</asp:LinkButton>--%>
                                                            </td><%--<td>  
                                                        <asp:ImageButton ID="imgAddressBook" runat="server" ImageUrl="~/Images/search1.png"  OnClientClick="Loadaddress()" Width="45px" Height="25px"/>
                                                                 </td>--%></tr></table>
                                                                
                             <%--<tr></tr>--%>
                            <%-- <tr><td>
                                 <asp:Label ID="Tofrd" runat="server" Text="Label"></asp:Label></td><td>
                                     <asp:DropDownList ID="DropDownList2" runat="server">
                                     </asp:DropDownList>
                                 </td></tr>--%>
                           </table>
                 
                       <table><tr><td> <asp:LinkButton ID="LinkButton7" runat="server" OnClientClick="return ccaddressfrd()" CausesValidation="false">Add Cc</asp:LinkButton></td><td><asp:LinkButton
                          ID="LinkButton8" runat="server" OnClientClick="return Bccaddressfrd()" CausesValidation="false">Add Bcc</asp:LinkButton></td></tr></table>

                 <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Fieldforce" ForeColor="#00C0EF" ControlToValidate="to"></asp:RequiredFieldValidator>
                      <asp:TextBox ID="to" runat="server" class="form-control"   Width="800px"></asp:TextBox>--%>
                      
                     
                                                          
                                                           <%-- <asp:Button ID="imgAddressBook" runat="server" Height="20px" Width="24px"  UseSubmitBehavior="false" style="background-image: url(../../images/Address_Book_Icon.gif);border:0" OnClick="imgAddressBook_Click" />--%>
                                                      
                 
                  <div id="divbcc" class="form-group" style="display:none;">
                  <table><tr><td><input type="text" id="bcctextboxfrd" class="form-control"  name="txtFree"  style="width:400px; height:12px;" /></td><td><asp:Button ID="Button9" runat="server" Text="Add Bcc" CausesValidation="false"  class="btn btn-primary" OnClientClick="return Loadaddressfrd(this.id);"/></td></tr></table>
				
			</div>
			<div id="divcc" class="form-group" style="display:none;">
            <table><tr><td>	<input type="text" name="txtPaid" id="cctextboxfrd"  class="form-control"  style="width:400px;height:12px;" /></td><td>			<asp:Button ID="Button10" runat="server" Text="Add Cc" CausesValidation="false"  class="btn btn-primary" OnClientClick="return Loadaddressfrd(this.id);"/></td></tr></table>

			</div>
          <table>  <tr><td  colspan="2"><i class="fa fa-envelope" aria-hidden="true"></i><asp:Label ID="Label9" runat="server" Text="Message" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td></tr>
                   
                   <tr><td>  <asp:TextBox ID="TextBox2" runat="server"  class="form-control" placeholder="Type Your Message here!!" TextMode="MultiLine" style="display:inline-block;background-color:rgba(255, 255, 255, 0.15);height: 180px;width: 530px;" BorderStyle="Solid" BorderWidth="1px" BorderColor="#3399FF" Font-Italic="True"></asp:TextBox></td></tr>
                    
                    
                     
                                                                           
                                 
                                   </table>                   
                      </br>
                        <table>
                       
   
 
                        
                       <tr><td><i class="fa fa-paperclip" aria-hidden="true"></i></td><td> <asp:Label ID="Label12" runat="server" Text="Attachements" ForeColor="#3399FF" Font-Bold="True"></asp:Label></td></tr>
                        </table>
                    
                
                   
                

                      
                  </div>
              
                </div>
                <div id="addbook" style="display:none;">
                    <%--<asp:GridView ID="grdadd" runat="server">
                    </asp:GridView>--%>
                    <div id="desigfilter"><center> <asp:Label ID="Label1" runat="server" Text="Select Fieldforce Name" Font-Bold="True" ForeColor="#009999"></asp:Label></center>    </div>
               <div id="designation"></div>
               <div id="designationcc"></div>
               <div id="designationbcc"></div>
                   <%-- <input type="radio" name="bedStatus" id="allot"  value="ASM">ASM
                    <input type="radio" name="bedStatus" id="transfer" value="ASO">ASO
                      <input type="radio" name="bedStatus" id="Radio1" value="SM">SM--%>
                      <div id="addbklist" style=" display:block;height:90%; overflow:auto;">
                  
                <table id="grdadd"></table>
                    <table id="grdbcc"></table>
                        <table id="grdcc"></table>
                
                </div>
                </div>





                <div id="addbookfrd" style="display:none;">
                    <%--<asp:GridView ID="grdadd" runat="server">
                    </asp:GridView>--%>
                    <div id="desigfilterfrd"><center> <asp:Label ID="Label11" runat="server" Text="Select Fieldforce Name" Font-Bold="True" ForeColor="#009999"></asp:Label></center>    </div>
               <div id="designationfrd"></div>
               <div id="designationccfrd"></div>
               <div id="designationbccfrd"></div>
                   <%-- <input type="radio" name="bedStatus" id="allot"  value="ASM">ASM
                    <input type="radio" name="bedStatus" id="transfer" value="ASO">ASO
                      <input type="radio" name="bedStatus" id="Radio1" value="SM">SM--%>
                      <div id="addbklistfrd" style=" display:block;height:90%; overflow:auto;">
                  
                <table id="frdadd"></table>
                    <table id="frdbcc"></table>
                        <table id="frdcc"></table>
                
                </div>
                </div>






                   </div>
                   
                   
                   
                   
                   
                </div>


             
 



 
  <asp:HiddenField id="HiddenField1"   runat="Server" />
  <asp:Label ID="attachmentfile" ForeColor="#f1f2f7" runat="server"></asp:Label>
                                          
              
            </div>
          
            
        </div>
    </div>


</form>
<style type="text/css">
   .popup{
    position:absolute;
    top:-111px;
    left:207px;
    margin:138px auto;
    width:572px;
    height:450px;
    font-family:verdana;
    font-size:13px;
    padding:10px;
    background-color:#f4f4f4;
    border:2px solid #14B1BB;
    z-index:100000000000000000;
    overflow-y:scroll;
    }
    .cancel{
    display:relative;
   cursor: pointer;
    margin: 0;
    float: right;
    height: 15px;
    width: 44px;
    padding: 4px 11px 14px 1px;
    background-color: #428bca;
    text-align: center;
    font-weight: bold;
    font-size: 8px;
    color: white;
    border-radius: 3px;
    z-index: 100000000000000000;
    border-style: solid;
    }

.cancel:hover{
    background:rgb(255,50,50);
    }
    
.regular-checkbox {
background-color: #FF0000;
}
    .ddl
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
                     
        background-image:url('css/download%20(2).png');
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
         .ddl1
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;  
         
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
    .modalBackground
    {
       
           width:200px;
           height:500px;
      
    }
    .modalPopup
    {
        background-color: #FFFFFF;
       width:200px;
        height:500px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0;
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
            width: 40px;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
    
        .Grid        {

            background-color: #fff;

            margin: 5px 0 10px 0;

            border: solid 1px #525252;

            border-collapse: collapse;

            font-family: Calibri;

            color: #474747;

        }

        .Grid td{

            padding: 2px;

            border: solid 1px #c1c1c1;

        }

        .Grid th{

            padding: 4px 2px;

            color: #fff;

            background: #14B1BB;

            border-left: solid 1px #525252;

            font-size: 0.9em;
             Font-Names:-webkit-pictograph;
              

        }

    </style>
</body>




<script type="text/javascript">
    //Enumeration for messages status
    MessageStatus = {
        Success: 1,
        Information: 2,
        Warning: 3,
        Error: 4
    }

    //Enumeration for messages status class
    MessageCSS = {
        Success: "Success",
        Information: "Information",
        Warning: "Warning",
        Error: "Error"
    }

    //Global variables
    var intervalID = 0;
    var subintervalID = 0;
    var fileUpload;
    var form;
    var previousClass = '';

    //Attach to the upload click event and grab a reference to the progress bar
    function pageLoad() {
        $addHandler($get('upload'), 'click', onUploadClick);
    }

    //Register the form
    function register(form, fileUpload) {
        this.form = form;
        this.fileUpload = fileUpload;
    }

    //Start upload process
    function onUploadClick() {
        if (fileUpload.value.length > 0) {
            var filename = fileExists();

            if (filename == '') {
                //Update the message
                    document.getElementById("dvProgress").style.backgroundImage="url(../Images/Progressbar_Content.gif)"; 
                updateMessage(MessageStatus.Information, 'Initializing upload ...', '', '0 of 0 Bytes');
                //Submit the form containing the fileupload control
                form.submit();
                //Set transparancy 20% to the frame and upload button
                Sys.UI.DomElement.addCssClass($get('dvUploader'), 'StartUpload');
                //Initialize progressbar
                setProgress(0);
                //Start polling to check on the progress ...
                startProgress();
                intervalID = window.setInterval(function () {
                    PageMethods.GetUploadStatus(function (result) {
                        if (result) {
                            setProgress(result.percentComplete);
                            //Upadte the message every 500 milisecond
                            updateMessage(MessageStatus.Information, result.message, result.fileName, result.downloadBytes);
                            if (result == 100) {
                                //clear the interval
                                window.clearInterval(intervalID);
                                clearTimeout(subintervalID);
                            }
                        }
                    });
                }, 500);
            }
            else
                onComplete(MessageStatus.Error, "File name '<b>" + filename + "'</b> already exists in the list.", '', '0 of 0 Bytes');
        }
        else
            onComplete(MessageStatus.Warning, 'You need to select a file.', '', '0 of 0 Bytes');
    }

    //Stop progrss when file was successfully uploaded
    function onComplete(type, msg, filename, downloadBytes) {
        window.clearInterval(intervalID);
        clearTimeout(subintervalID);
        updateMessage(type, msg, filename, downloadBytes);
        if (type == MessageStatus.Success) setProgress(100);
        //Set transparancy 100% to the frame and upload button
        Sys.UI.DomElement.removeCssClass($get('dvUploader'), 'StartUpload');
        //Refresh uploaded files list.
        refreshFileList('<%=hdRefereshGrid.ClientID %>');
    }

    //Update message based on status
    function updateMessage(type, message, filename, downloadBytes) {
//        var slvals = '';
//        $('table tr td :checkbox:checked').map(function () {
//            var $this = $(this);
//            $this.closest('tr').each(function () {
//                v = $(this).text();
//                slvals += v + ",";
//            });
//        });

//        document.getElementById('ctl00_ContentPlaceHolder1_txtAddr').value = slvals;
        var _className = MessageCSS.Error;
        var _messageTemplate = $get('tblMessage');
        var _icon = $get('dvIcon');
        _icon.innerHTML = message;
        $get('dvDownload').innerHTML = downloadBytes;
        $get('dvFileName').innerHTML = filename;
        switch (type) {
            case MessageStatus.Success:
                _className = MessageCSS.Success;
                break;
            case MessageStatus.Information:
                _className = MessageCSS.Information;
                break;
            case MessageStatus.Warning:
                _className = MessageCSS.Warning;
                break;
            default:
                _className = MessageCSS.Error;
                break;
        }
        _icon.className = '';
        _messageTemplate.className = '';
        Sys.UI.DomElement.addCssClass(_icon, _className);
        Sys.UI.DomElement.addCssClass(_messageTemplate, _className);
    }

    //Refresh uploaded file list when new file was uploaded successfully
    function refreshFileList(hiddenFieldID) {
        var hiddenField = $get(hiddenFieldID);
        if (hiddenField) {
            hiddenField.value = (new Date()).getTime();
            __doPostBack(hiddenFieldID, '');
        }
    }

    //Set progressbar based on completion value
    function setProgress(completed) {
        $get('dvProgressPrcent').innerHTML = completed + '%';
        $get('dvProgress').style.width = completed + '%';


    }

    //Display mouse over and out effect of file upload list
    function eventMouseOver(_this) {
        previousClass = _this.className;
        _this.className = 'GridHoverRow';
    }
    function eventMouseOut(_this) {
        _this.className = previousClass;
    }

    //This will call every 200 milisecnd and update the progress based on value
    function startProgress() {
        var increase = $get('dvProgressPrcent').innerHTML.replace('%', '');
        increase = Number(increase) + 1;
        if (increase <= 100) {
            setProgress(increase);
            subintervalID = setTimeout("startProgress()", 200);
        }
        else {
            window.clearInterval(subintervalID);
            clearTimeout(subintervalID);
        }
    }

    //This will check whether will was already exist on the server, 
    //if file was already exists it will return file name else empty string.
    function fileExists() {

        var selectedFile = fileUpload.value.split('\\');
      
        var file = $get('ctl00_ContentPlaceHolder1_gvNewFiles').getElementsByTagName('a');
        for (var f = 0; f < file.length; f++) {
            if (file[f].innerHTML == selectedFile[selectedFile.length - 1]) {
                return file[f].innerHTML;
            }
        }
        return '';
    }
</script>
         <link href="CSS/ThemeBlue.css" rel="Stylesheet" type="text/css" />
          </form>
    
</asp:Content>
   
