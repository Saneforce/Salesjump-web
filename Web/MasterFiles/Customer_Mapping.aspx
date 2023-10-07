<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Customer_Mapping.aspx.cs" Inherits="MasterFiles_Customer_Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .supbody {
            -webkit-column-count: 2;
            -moz-column-count: 2;
            column-count: 4;
        }

        .disbody {
            display: flex;
            flex-wrap: wrap;
            justify-content: flex-start;
        }

       .card {
            border: 1px solid #ccc;
            padding: 10px;
            border-radius: 5px;
            width: calc(25% - 20px); 
            margin: 10px;
            display: flex;
            flex-direction: column;
        }

       .checkbox-label {
            display: flex;
            align-items: baseline;
        }

            .checkbox-label input {
                margin-right: 10px; 
            }

        .label {
            font-weight: 100;
            color: darkslategrey;
            text-align: left;
        }

        .search-box {
            float: right;
            margin-top: 14px;
            margin-right: 10px;
        }

        #searchInput {
            padding: 5px;
            width: 200px;
        }
    </style>
    <form runat="server" id="frm1">
        <div class="row">
            <div class="col-lg-12 sub-header">Customer Mapping<span style="float: right"></span></div>
        </div>
        <div class="container" style="margin-top: 20px; margin-left: 101px;">
            <div class="row col-lg-12">
                <div class="col-lg-3" style="width: 11%; padding-top: 5px;">
                    <label>Super Stockist</label>
                </div>
                <div class="col-xs-5">
                    <select class="form-control" id="sstddl">
                    </select>
                </div>
            </div>
        </div>
        <div class="search-box">
            <input type="text" id="searchInput" placeholder="Search...">
        </div>
        <div class="panel panel-default cus" style="margin-left: 26px; margin-top: 7px;">
            <div class="panel-heading" style="background-color: #19a4c6;">
                <div class="row">
                    <label style="color: white; margin-bottom: 0px; margin-left: 15px">Distributor</label>
                </div>
            </div>
            <!-- /.panel-heading -->

            <div class="panel-body disbody" style="margin-top: 7px">
            </div>
            <!-- /.panel-body -->
        </div>
        <center>
            <button class="btn btn-primary" type="button" id="svss">Save</button>
        </center>
    </form>
    <script type="text/javascript">
        var getsst = [];
        var ComDetails = [];
        var getd = [];
        var Selected_SCode = '';
        function handleCheckboxChange() {
            const checkbox = $(this);
            const card = checkbox.closest('.card');
            card.toggleClass('selected-card', checkbox.is(':checked'));
        }



        function fillss() {
            var divcode =<%=Session["div_code"]%>;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Customer_Mapping.aspx/GetSStockist",
                data: "{'divcode':" + divcode + "}",
                dataType: "json",
                success: function (data) {
                    getsst = JSON.parse(data.d) || [];
                    ComDetails = getsst;
                    if (getsst.length > 0) {
                        var ddlComName = $("#sstddl");
                        ddlComName.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                        for (var i = 0; i < getsst.length; i++) {
                            ddlComName.append($("<option></option>").val(getsst[i].S_No).html(getsst[i].S_Name));
                        };
                    }
                },
                error: function (reult) {

                }
            });
        }
        function getsuppliers() {
            var filsp = $('.supbody');
            filsp.empty();
            var td = '';
            var filt = getsst.filter(function (obj) {
                return obj.S_No != Selected_SCode;
            });
            if (filt.length > 0) {
                for (var i = 0; i < filt.length; i++) {
                    td += '<span style="white-space: nowrap;"><input type="checkbox" name="data" value="' + filt[i].S_No + '" id="' + filt[i].S_No + '" /><label style="font-weight: 100;">' + filt[i].S_Name + '</label></span><br/>';
                }
                filsp.append(td);
            }
        }
        function getdist() {
            var divcode =<%=Session["div_code"]%>;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Customer_Mapping.aspx/GetSSdist",
                data: "{'divcode':" + divcode + "}",
                dataType: "json",
                success: function (data) {
                    getd = JSON.parse(data.d) || [];
                    loaddist();
                },
                error: function (res) {

                }
            })
        }
        function createCard(id, name, erp, dis) {
            const cardContainer = $(".disbody");

            const card = document.createElement("div");
            card.classList.add("card");

            const checkboxLabel = document.createElement("div");
            checkboxLabel.classList.add("checkbox-label");

            const checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.name = "data";
            checkbox.class = "chk";
            checkbox.value = id;
            checkbox.id = id;

            const label1 = document.createElement("label");
            label1.classList.add("label");
            label1.textContent = name;

            const label2 = document.createElement("label");
            label2.classList.add("label");
            label2.textContent = "ERP Code : " + erp;

            const label3 = document.createElement("label");
            label3.classList.add("label");
            label3.textContent = "District : " + dis;

            checkboxLabel.appendChild(checkbox);
            checkboxLabel.appendChild(label1);

            card.appendChild(checkboxLabel);
            card.appendChild(label2);
            card.appendChild(label3);

            cardContainer.append(card);
        }
        function loaddist() {
            var td = '';
            var filld = $('.disbody');
            filld.empty();
            for (var i = 0; i < getd.length; i++) {
                td += '<span style="white-space: nowrap;"><input type="checkbox" name="data" value="' + getd[i].Stockist_Code + '" id="' + getd[i].Stockist_Code + '" /><label style="font-weight: 100;">' + getd[i].Stockist_Name + '</label></span><br/>';
            }
            getd.forEach(item => {
                createCard(item.Stockist_Code, item.Stockist_Name, item.ERP_Code, item.Dist_Name);
            });
            // filld.append(td);
        }
        var mapdi = [];
        var mpdis = [];
        var mfilt = [];
        $(document).on('change', '#sstddl', function () {
            Selected_SCode = $('#sstddl :selected').val()
            Selected_Sname = $('#sstddl :selected').text()
            if (Selected_SCode != "0") {
                loaddist();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Customer_Mapping.aspx/Getmapdist",
                    data:"{'divcode':<%=Session["div_code"]%>,'supcode':'" + Selected_SCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        mapdi = JSON.parse(data.d) || [];
                        //if(mapdi.length>0){
                        //    mpdis=(mapdi[0].Customer_code).split(',');
                        //    for(i=0;i<mpdis.length;i++){
                        //        $('#'+mpdis[i]).prop("checked", true);
                        //    }
                        //}
                        mapdi = JSON.parse(data.d) || [];
                        if (mapdi.length > 0) {
                            mpdis = (mapdi[0].Customer_code).split(',');

                            
                            for (i = 0; i < mpdis.length; i++) {
                                var customerId = mpdis[i];
                                $('#' + customerId).prop("checked", true);
                            }
                        }
                    },
                    error: function (res) {

                    }
                })
            }
            else {
                loaddist();
                alert("Please Select a Super Stockist");
                return false;
            }
        });
        $(document).on('click', '#svss', function () {
            if (Selected_SCode != "0") {
                var tt;
                $.each($('input[name="data"]:checked'), function () {
                    tt = tt + ',' + $(this).val();
                })
                if (tt != undefined) {
                    tt = tt.replace('undefined,', '').trim();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Customer_Mapping.aspx/savecus",
                        data: "{'data':'" + tt + "','supcode':'" + Selected_SCode + "','Supname':'" + Selected_Sname + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert('Success');
                            loaddist();
                        },
                        error: function (res) {

                        }
                    })
                }
                else {
                    alert("Please Select a Distributor");
                    return false;
                }
            }
            else {
                loaddist();
                alert("Please Select a Super Stockist");
            }
        })
        $(document).on('change', 'input[type="checkbox"]', handleCheckboxChange);
        $(document).ready(function () {
            fillss();
            getdist();
            $("#searchInput").on("input", function () {
                const searchQuery = $(this).val().toLowerCase();
                $(".card").each(function () {
                    const card = $(this);
                    const cardText = card.find(".label").text().toLowerCase();
                    if (cardText.includes(searchQuery)) {
                        card.show();
                    } else {
                        card.hide();
                    }
                });
            });
            $(".chk").on("change", function () {
                handleCheckboxChange();
            });

        });
    </script>
</asp:Content>

