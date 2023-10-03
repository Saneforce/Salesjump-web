$(document)["ready"](function () {
    $("#loading")["show"]();
    var e = $("#ddlwise");
    var mode = e.options[e.selectedIndex].value;
    if (mode == 0) {
        $('#ddlFrom').css('display','none');
        $('#ddlTo').css('display','none');
        $('#lbljoin').css('display','none');
        $('#lblSt').css('display','none');
        $('#ddlst').css('display','none');
        $('#lblDesig').css('display','none');
        $('#ddlDesig').css('display','none');
        $('#lblsub').css('display','none');
        $('#ddlsubdiv').css('display','none');
        // $('#btngo').css('display','none');
    }
    var TMonth = $("#ddlFrom").val();
    var TYear = $("#ddlTo").val();
    var St = $("#ddlst").val();
    var Sub = $("#ddlsubdiv").val();
    var Data = mode + "^" + TMonth + "^" + TYear + "^" + St + "^" + Sub
    $["ajax"]({
        type: "POST",
        url: "../webservice/Quiz_QuestionWS.asmx/BindUserList",
        data: '{objData:' + JSON.stringify(Data) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (variable_0) {
            if (variable_0["d"]["length"] > 0) {
                var variable_1 = "<thead><tr>";
                variable_1 += "<th style=\"width:34px\"><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" onchange=\"checkAll(this)\"/><div><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" style=\"border-color:white\" onchange=\"checkAll(this)\"/></div></th>";
                variable_1 += "<th style=\"width:33px\">S.No<div>S.No</div></th>";
                variable_1 += "<th style=\"width:270px\">Field Force Name<div>Field Force Name</div></th>";
                variable_1 += "<th style=\"width:130px\">HQ<div>HQ</div></th>";
                variable_1 += "<th style=\"width:10px\">Designation<div>Designation</div></th>";
                variable_1 += "<th style=\"width:167px\">State<div>State</div></th>";
                variable_1 += "<th style=\"display:none\">SF Code<div>SF Code</div></th>";
                variable_1 += "</tr></thead>";
                variable_1 += "<tbody>";
                for (var variable_2 = 0; variable_2 < variable_0["d"]["length"]; variable_2++) {
                    variable_1 += "<tr>";
                    if (variable_0["d"][variable_2]["ChkBox"] == "True") {
                        variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\"><input type=\"checkbox\" class = \"chcktbl\" disabled=\"disabled\" style=\"display:none;border-color:Red;\"  id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                        variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                        variable_1 += "<td id=\"SFName\" style=\"width:250px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                    } else {
                        variable_1 += "<td style=\"width:50px\"><input type=\"checkbox\" class = \"chcktbl\" id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                        variable_1 += "<td style=\"width:50px\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                        variable_1 += "<td id=\"SFName\" style=\"width:250px;\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                    };
                    variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["HQ"] + "</td>";
                    variable_1 += "<td style=\"width:100px\">" + variable_0["d"][variable_2]["Designation"] + "</td>";
                    variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["State"] + "</td>";
                    variable_1 += "<td style=\"display:none\">" + variable_0["d"][variable_2]["SF_Code"] + "</td>";
                    variable_1 += "</tr>"
                };
                variable_1 += "</tbody>";
                $("#tblUserList")["html"](variable_1);
                $("#loading")["hide"]()
            }
            else {
                $("#loading")["hide"]()
            }
        },
        error: function (variable_3) {
            $("#loading")["hide"]()
        }
    });
    $("#btngo").click(function () {
        var e = $("#ddlwise");
        var mode = e.options[e.selectedIndex].value;
        var TMonth = $("#ddlFrom").val();
        var TYear = $("#ddlTo").val();
        var St = $("#ddlst").val();
        var Desig = $("#ddlDesig option:selected").text();
        var Sub = $("#ddlsubdiv option:selected").val();
        var Data = mode + "^" + TMonth + "^" + TYear + "^" + St + "^" + Desig + "^" + Sub
        $("#loading")["show"]();
        $["ajax"]({
            type: "POST",
            url: "../webservice/Quiz_QuestionWS.asmx/BindUserList",
            data: '{objData:' + JSON.stringify(Data) + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (variable_0) {
                if (variable_0["d"]["length"] > 0) {
                    var variable_1 = "<thead><tr>";
                    variable_1 += "<th style=\"width:34px\"><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" onchange=\"checkAll(this)\"/><div><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" style=\"border-color:white\" onchange=\"checkAll(this)\"/></div></th>";
                    variable_1 += "<th style=\"width:33px\">S.No<div>S.No</div></th>";
                    variable_1 += "<th style=\"width:270px\">Field Force Name<div>Field Force Name</div></th>";
                    variable_1 += "<th style=\"width:130px\">HQ<div>HQ</div></th>";
                    variable_1 += "<th style=\"width:10px\">Designation<div>Designation</div></th>";
                    variable_1 += "<th style=\"width:167px\">State<div>State</div></th>";
                    variable_1 += "<th style=\"display:none\">SF Code<div>SF Code</div></th>";
                    variable_1 += "</tr></thead>";
                    variable_1 += "<tbody>";
                    for (var variable_2 = 0; variable_2 < variable_0["d"]["length"]; variable_2++) {
                        variable_1 += "<tr>";
                        if (variable_0["d"][variable_2]["ChkBox"] == "True") {
                            variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\"><input type=\"checkbox\" class = \"chcktbl\" disabled=\"disabled\" style=\"display:none;border-color:Red;\"  id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                            variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                            variable_1 += "<td id=\"SFName\" style=\"width:250px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                        } else {
                            variable_1 += "<td style=\"width:50px\"><input type=\"checkbox\" class = \"chcktbl\" id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                            variable_1 += "<td style=\"width:50px\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                            variable_1 += "<td id=\"SFName\" style=\"width:250px;\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                        };
                        variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["HQ"] + "</td>";
                        variable_1 += "<td style=\"width:100px\">" + variable_0["d"][variable_2]["Designation"] + "</td>";
                        variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["State"] + "</td>";
                        variable_1 += "<td style=\"display:none\">" + variable_0["d"][variable_2]["SF_Code"] + "</td>";
                        variable_1 += "</tr>"
                    };
                    variable_1 += "</tbody>";
                    $("#tblUserList")["html"](variable_1);
                    $("#loading")["hide"]()
                }
                else {

                   
                    var variable_1 = "<thead><tr>";
                    variable_1 += "<th style=\"width:34px\"><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" onchange=\"checkAll(this)\"/><div><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" style=\"border-color:white\" onchange=\"checkAll(this)\"/></div></th>";
                    variable_1 += "<th style=\"width:33px\">S.No<div>S.No</div></th>";
                    variable_1 += "<th style=\"width:270px\">Field Force Name<div>Field Force Name</div></th>";
                    variable_1 += "<th style=\"width:130px\">HQ<div>HQ</div></th>";
                    variable_1 += "<th style=\"width:10px\">Designation<div>Designation</div></th>";
                    variable_1 += "<th style=\"width:167px\">State<div>State</div></th>";
                    variable_1 += "<th style=\"display:none\">SF Code<div>SF Code</div></th>";
                    variable_1 += "</tr></thead>";
                    variable_1 += "<tbody>";
                    variable_1 += "<tr>";
                    variable_1 += "<td colspan=\"6\" style=\"font-size:15px;color:Red;width:250px;\">No Records Found</td>";
                    variable_1 += "</tr>"
                    variable_1 += "</tbody>";
                    $("#tblUserList")["html"](variable_1);
                    $("#loading")["hide"]()
                }
            },
            error: function (variable_3) {
                $("#loading")["hide"]()
            }
        });
    });
    $("#btnProcess")["click"](function () {
        if ($("#tblUserList input[type=checkbox]:checked")["length"] === 0) {
            event["preventDefault"]();
            var variable_4 = "Please Select Atleast one Checkbox";
            createCustomAlert(variable_4)
        } else {
            var variable_5 = $("#txtTime")["val"]();
            var variable_6 = $("#datepicker")["val"]();
            var variable_7 = new Date();
            var variable_8 = variable_7["getMonth"]() + 1;
            var variable_9 = variable_7["getFullYear"]();
            var variable_10 = variable_8 + "^" + variable_9;
            var variable_11 = $("#ddlType")["val"]();
            var variable_12 = $("#ddlNoOfAttempt")["val"]();
            selected = $("#tblUserList tr td input[type='checkbox']:checked");
            var variable_13 = $("#tblUserList");
            var variable_14 = $("#tblUserList tr td");
            var variable_15 = $("#datepickerFrom")["val"]();
            var variable_16 = $("#datepickerTo")["val"]();
            var variable_00 = [];
            $("input:checkbox:checked", variable_14)["each"](function (variable_2) {
                var variable_01 = $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(0)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(1)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(2)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(3)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(4)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(5)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(6)")["text"]() + "," + variable_5 + "," + variable_6 + "," + variable_11 + "," + variable_12 + "," + variable_8 + "," + variable_9 + "," + variable_15 + "," + variable_16;
                variable_00["push"](variable_01)
            })["get"]();
            $["ajax"]({
                type: "POST",
                url: "../webservice/Quiz_QuestionWS.asmx/AddProcessDetails",
                data: "{objUserData:" + JSON["stringify"](variable_00) + ",objMonth:" + JSON["stringify"](variable_10) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (variable_0) { },
                error: function (variable_3) { }
            })
        }
    });
    var variable_02 = document["getElementById"]("tblUserList")["rows"]["length"];
    var variable_03 = [];
    for (var variable_2 = 1; variable_2 < variable_02; variable_2++) {
        variable_03[variable_2] = document["getElementById"]("tblUserList")["rows"][variable_2];
        variable_03[variable_2]["onmouseover"] = function () {
            this["style"]["backgroundColor"] = "#f3f8aa"
        };
        variable_03[variable_2]["onmouseout"] = function () {
            this["style"]["backgroundColor"] = "#ffffff"
        }
    }
});

function checkAll(variable_05) {
    var variable_06 = document["getElementsByTagName"]("input");
    if (variable_05["checked"]) {
        for (var variable_2 = 0; variable_2 < variable_06["length"]; variable_2++) {
            if (variable_06[variable_2]["type"] == "checkbox" && variable_06[variable_2]["style"]["borderColor"] == "red") {
                variable_06[variable_2]["checked"] = false
            } else {
                variable_06[variable_2]["checked"] = true
            }
        }
    } else {
        for (var variable_2 = 0; variable_2 < variable_06["length"]; variable_2++) {
            if (variable_06[variable_2]["type"] == "checkbox") {
                variable_06[variable_2]["checked"] = false
            }
        }
    }
}