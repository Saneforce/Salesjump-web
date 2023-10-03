/// <reference path="services.js" />
var fmcgServices = angular.module('fmcgServices', []);
fmcgServices.factory('phonegapReady', function($rootScope) {
    return function(fn) {
        var queue = [];
        var impl = function() {
            queue.push(Array.prototype.slice.call(arguments));
        };
        document.addEventListener('deviceready', function() {
            queue.forEach(function(args) {
                fn.apply(this, args);
            });
            impl = fn;
        }, false);
        return function() {
            return impl.apply(this, arguments);
        };
    };
});
fmcgServices.factory('navSvc', function($navigate) {
    return {
        slidePage: function(path, type) {
            $navigate.go(path, type);
        },
        back: function() {
            $navigate.back();
        }
    }
});
fmcgServices.factory('geolocation', function($rootScope, phonegapReady) {
    return {
        getCurrentPosition: phonegapReady(function(onSuccess, onError, options) {
            navigator.geolocation.getCurrentPosition(function() {
                    var that = this,
                        args = arguments;
                    if (onSuccess) {
                        $rootScope.$apply(function() {
                            onSuccess.apply(that, args);
                        });
                    }
                }, function() {
                    var that = this,
                        args = arguments;
                    if (onError) {
                        $rootScope.$apply(function() {
                            onError.apply(that, args);
                        });
                    }
                },
                options);
        })
    };
});
fmcgServices.factory('notification', function($rootScope, phonegapReady) {
    return {
        alert: phonegapReady(function(message, alertCallback, title, buttonName) {
            navigator.notification.alert(message, function() {
                var that = this,
                    args = arguments;
                $rootScope.$apply(function() {
                    alertCallback.apply(that, args);
                });
            }, title, buttonName);
        }),
        confirm: phonegapReady(function(message, confirmCallback, title, buttonLabels) {
            navigator.notification.confirm(message, function() {
                var that = this,
                    args = arguments;
                $rootScope.$apply(function() {
                    confirmCallback.apply(that, args);
                });
            }, title, buttonLabels);
        }),
        beep: function(times) {
            navigator.notification.beep(times);
        },
        vibrate: function(milliseconds) {
            navigator.notification.vibrate(milliseconds);
        }
    };
});


fmcgServices.factory('fmcgAPIservice', function($http, fmcgLocalStorage) {
    var fmcgAPI = {};
    $http.defaults.useXDomain = true;
    $http.defaults.headers.common['X-Requested-With'];
    fmcgAPI.deleteEntry = function(method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        if (data.doctor != undefined)
            var custId = data.doctor.selected.id;
        var str = {
            arc: data.arc,
            amc: data.amc,
            custId: custId
        };

        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + encodeURIComponent(JSON.stringify(str).replace(/&/g, '')), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    }


    fmcgAPI.deleteNotify = function(method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        if (data.doctor != undefined)
            var custId = data.doctor.selected.id;
        var str = {
            NotifyID: data.NotifyID
           
        };

        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + encodeURIComponent(JSON.stringify(str).replace(/&/g, '')), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    }

    fmcgAPI.getDataList = function(method, url, data, sSF) {
        var tablistNSL = ["vwProductStateRates", "vwactivity_report", "vwactivity_msl_details", "vwActivity_CSH_Detail","vwActivity_SuperCSH_Detail", "activity_stockist_report", "vwActivity_Unlst_Detail", "activity_doctor_report", "activity_Chemist_report", "activity_stockist_report", "activity_unlisteddoctor_Report", "vwactivity_Report_APP", "vwactivity_doctor_report_App", "vwactivity_Chemist_report_App", "vwactivity_stockist_report_App", "vwactivity_unlisteddoctor_Report_App", "activity_unlistedsample_Report", "activity_sample_report", "activity_input_report", "activity_chm_sample_report", "speciality_master", "vwActivity_Report", "Activity_POB_Report", "vwMyDayPlan"];
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
       
        if (!sSF)
            sSF = ((userData.desigCode.toLowerCase() == 'mr' || (tablistNSL.indexOf(data[0]) > -1) || data.length == 0) ? userData.sfCode : userData.curSFCode)
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + sSF + "&rSF=" + userData.sfCode + "&State_Code=" + userData.State_Code;
        }
      if(userData.desigCode.toLowerCase()=='stockist')
      userData.desigCode='stockist';
     if(userData.desigCode.toLowerCase()=='superstock')
      userData.desigCode='superstock';

        var str = {
            tableName: data[0],
            coloumns: data[1],
            today: data[4],
            join: data[2],
            where: data[3],
            or: data[5],
            wt: data[6],
            sfCode: data[7],
            orderBy: data[8],
            desig: userData.desigCode.toLowerCase(),
        };
        if (tablistNSL.indexOf(data[0]) === -1) {
            str.orderBy = "[\"name asc\"]";
        }
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + encodeURIComponent(JSON.stringify(str).replace(/&/g, '')), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    };
    fmcgAPI.getPostData = function(method, url, data) {


        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + encodeURIComponent(JSON.stringify(data).replace(/&/g, '')), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    };


 fmcgAPI.GooglePlusRequest = function(method, url, data) {

        var appendDS = "";
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + encodeURIComponent(JSON.stringify(data).replace(/&/g, '')), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    };

    pushKey = function(obj1, obj2) {
        obj1['f_key'] = obj2;
        return obj1;
    }
    addQuotes = function(data) {
        data = data ? data : "";
        return "\'" + data.toString().replace(/\'/g, "\'\'") + "\'";
    }
    fmcgAPI.updateRemark = function(method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        resultData = [];
        if (userData) {
            var UpdRemark = {};
            UpdRemark.SF_code = addQuotes(userData.sfCode);
            UpdRemark.remarks = addQuotes(data.remarks);
            UpdRemark.HalfDay_FW_Type = addQuotes(data.Halfday);
            resultData.push({
                'Activity_Report': UpdRemark
            });
            $http.defaults.useXDomain = true;
            return $http({
                method: method,
                url: baseURL + url,
                data: "data=" + JSON.stringify(resultData).replace(/&/g, ''),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
        } else {
            alert('Kindly Login Again !...');
        }
    }
    fmcgAPI.sendData = function(method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        $http.defaults.useXDomain = true;
        return $http({
            method: method,
            url: baseURL + url + appendDS,
            data: "data=" + JSON.stringify(data).replace(/&/g, ''),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    }
    fmcgAPI.updateDCRData = function(method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        $http.defaults.useXDomain = true;
        return $http({
            method: method,
            url: baseURL + url + appendDS,
            data: "data=" + JSON.stringify(data).replace(/&/g, ''),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    }
    fmcgAPI.sendSMS = function(mobileNumber, text, method) {
        return $http({
            url: "http://www.smswave.in/panel/sendsms.php?user=SANEFO&password=123123&sender=SANEFO&PhoneNumber=" + mobileNumber + "&Text=" + text + "&Message=test",
            method: method,
            //  headers: { 'Access-Control-Allow-Origin': '*' }
        });
    }
    fmcgAPI.addMAData = function(method, url, choice, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode + "&State_Code=" + userData.State_Code + "&desig=" + userData.desigCode;;
        }
        var Activity_Event_Captures = [];
        var resultData = [];
        switch (choice) {
            case "1":
                var chemists_master = {};
                chemists_master.town_code = addQuotes(data.cluster.selected.id);
                chemists_master.chemists_name = addQuotes(data.name);
                chemists_master.Chemists_Address1 = addQuotes(data.address);
                chemists_master.Chemists_Phone = addQuotes(data.phone);
                resultData.push({
                    'chemists_master': chemists_master
                });
                break;


            case "2":
                var unlisted_doctor_master = {};
                unlisted_doctor_master.town_code = addQuotes(data.cluster.selected.id);
                if (data.wlkg_sequence.selected.id == undefined)
                    data.wlkg_sequence.selected.id = "null";
                else
                data.wlkg_sequence.selected.id=addQuotes(data.wlkg_sequence.selected.id);
                unlisted_doctor_master.wlkg_sequence =data.wlkg_sequence.selected.id;
                unlisted_doctor_master.unlisted_doctor_name = addQuotes(data.name);
                unlisted_doctor_master.unlisted_doctor_address = addQuotes(data.address);
                unlisted_doctor_master.unlisted_doctor_phone = addQuotes(data.phone);
                unlisted_doctor_master.unlisted_doctor_cityname = addQuotes(data.cityname);
                unlisted_doctor_master.unlisted_doctor_landmark = addQuotes(data.landmark);
                if (data.RetailerphotosList != undefined && data.RetailerphotosList.length>0) { 
                    var tempOb = {};
                    var imgUrl = data['RetailerphotosList'][0]['imgData'];
                    tempOb['imgurl'] = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                   // Activity_Event_Captures.push(pushKey(tempOb));
                    unlisted_doctor_master.Retailerphoto=tempOb;
                }
                unlisted_doctor_master.lat=addQuotes(data.lat);
                unlisted_doctor_master.long=addQuotes(data.long);
                unlisted_doctor_master.unlisted_doctor_areaname = addQuotes(data.areaname);
                unlisted_doctor_master.unlisted_doctor_contactperson = addQuotes(data.contactperson);
                unlisted_doctor_master.unlisted_doctor_designation = addQuotes(data.designation);
                unlisted_doctor_master.unlisted_doctor_gst = addQuotes(data.gstno);
                unlisted_doctor_master.unlisted_doctor_pincode = addQuotes(data.pincode);
                unlisted_doctor_master.unlisted_doctor_phone2 = addQuotes(data.phone2);
                unlisted_doctor_master.unlisted_doctor_phone3 = addQuotes(data.phone3);
                unlisted_doctor_master.unlisted_doctor_contactperson2 = addQuotes(data.contactperson2);
                unlisted_doctor_master.unlisted_doctor_contactperson3 = addQuotes(data.contactperson3);
                unlisted_doctor_master.unlisted_doctor_designation2= addQuotes(data.designation2);
                unlisted_doctor_master.unlisted_cat_code = "null";
                unlisted_doctor_master.unlisted_specialty_code = data.speciality.selected.id;
                unlisted_doctor_master.unlisted_qulifi = addQuotes(data.qulification.selected.id);
                unlisted_doctor_master.unlisted_class = data.class.selected.id;
                unlisted_doctor_master.DrKeyId = addQuotes(data.DrKeyId);
                resultData.push({'unlisted_doctor_master': unlisted_doctor_master});

                console.log('Mydayplan_Request'+JSON.stringify(resultData));
                break;
            case "3":

        
                var tbMyDayPlan = {};
                var endate = new Date(data['eTime']);
                tbMyDayPlan.wtype = addQuotes(data.worktype);
                tbMyDayPlan.sf_member_code = addQuotes(data.subordinateid);
                tbMyDayPlan.stockist = addQuotes(data.stockistid);
                tbMyDayPlan.stkName = data.stkName;
                tbMyDayPlan.dcrtype = data.dcrtype;
                tbMyDayPlan.cluster = addQuotes(data.clusterid);
                tbMyDayPlan.custid = data.custid;
                tbMyDayPlan.custName = data.custName;
                tbMyDayPlan.address = data.address;
                tbMyDayPlan.remarks = addQuotes(data.remarks);
                tbMyDayPlan.OtherWors = data.OtherWors;
                tbMyDayPlan.FWFlg = addQuotes(data.FWFlg);
                tbMyDayPlan.ClstrName = addQuotes(data.ClstrName);
                tbMyDayPlan.AppVersion=data.AppVersion;
              if(data.Sprstk!=undefined)
                tbMyDayPlan.superstockistid=data.Sprstk;
                jointWorkString = '';
                tbMyDayPlan.location = data.location;
                var dateStr = endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + " " + endate.getHours() + ":" + endate.getMinutes() + ":" + endate.getSeconds();
                tbMyDayPlan.dcr_activity_date = addQuotes(dateStr);
               
                for (var t = 0, jwlen = data.jontWorkSelectedList.length; t < jwlen; t++) {
                    if (t != 0)
                        jointWorkString += "$$";
                    jointWorkString += data.jontWorkSelectedList[t].jointwork;

                }
               if (data.photosList != undefined && data.photosList.length>0) { 
                    var tempOb = {};
                    var imgUrl = data['photosList'][0]['imgData'];
                    tempOb['imgurl'] = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                    tbMyDayPlan.profilepic=tempOb;
                }

                tbMyDayPlan.worked_with = addQuotes(jointWorkString);
                resultData.push({
                    'tbMyDayPlan': tbMyDayPlan
                });
  

                break;
            case "4":
                var endate = new Date(data['RCPADt']);
                var dateStr = endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + " " + endate.getHours() + ":" + endate.getMinutes() + ":" + endate.getSeconds();
                var tbRCPADetails = {};
                tbRCPADetails.ChmId = addQuotes(data.ChmId);
                tbRCPADetails.ChmName = addQuotes(data.ChmName);
                tbRCPADetails.DrId = addQuotes(data.DrId);
                tbRCPADetails.DrName = addQuotes(data.DrName);
                tbRCPADetails.RCPADt = addQuotes(dateStr);
                tbRCPADetails.ourBrnd = addQuotes(data.ourBrnd);
                tbRCPADetails.ourBrndNm = addQuotes(data.ourBrndNm);
                tbRCPADetails.CmptrName = addQuotes(data.CmptrName);
                tbRCPADetails.CmptrBrnd = addQuotes(data.CmptrBrnd);
                tbRCPADetails.CmptrQty = addQuotes(data.CmptrQty);
                tbRCPADetails.CmptrPOB = addQuotes(data.CmptrPOB);
                tbRCPADetails.CmptrPriz = addQuotes(data.CmptrPriz);
                tbRCPADetails.Remark = addQuotes(data.Remark);
                tbRCPADetails.photosList = data.photosList;
                resultData.push({
                    'tbRCPADetails': tbRCPADetails
                });
                break;
            case "5":
                var tbRemdrCall = {};
                tbRemdrCall.Doctor_ID = addQuotes(data.DrId);
                tbRemdrCall.WWith = addQuotes(data.JntWrkCds);
                tbRemdrCall.WWithNm = addQuotes(data.JntWrkNms);
                tbRemdrCall.Prods = addQuotes(data.ProdCds);
                tbRemdrCall.ProdsNm = addQuotes(data.ProdNms);
                tbRemdrCall.Remarks = addQuotes(data.Remark);
                tbRemdrCall.location = addQuotes(data.location);
                resultData.push({
                    'tbRemdrCall': tbRemdrCall
                });
                break;
            case "6":
                var Map_GEO_Customers = {};
                Map_GEO_Customers.Cust_Code = addQuotes(data.id);
                Map_GEO_Customers.lat = addQuotes(data.lat);
                Map_GEO_Customers.long = addQuotes(data.long);
                Map_GEO_Customers.StatFlag = '0';
                resultData.push({
                    'Map_GEO_Customers': Map_GEO_Customers
                });
                break;
            case "7":
                var endate = new Date(data['DtTm']);
                var tbTrackLoction = {};
                var dateStr = endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + " " + endate.getHours() + ":" + endate.getMinutes() + ":" + endate.getSeconds();
                tbTrackLoction.SF_code = addQuotes(data.SF_code);
                tbTrackLoction.DtTm = addQuotes(dateStr);
                tbTrackLoction.Lat = addQuotes(data.Lat);
                tbTrackLoction.Lon = addQuotes(data.Lon);
                tbTrackLoction.Auc = addQuotes(data.Auc);
                tbTrackLoction.deg = addQuotes(data.deg);
                tbTrackLoction.DvcID = addQuotes(data.DvcID);

                resultData.push({
                    'tbTrackLoction': tbTrackLoction
                });
                break;
            case "8":
                resultData.push({
                    'stockUpdation': data
                });


if (data.photosList != undefined) {
                     for (var i = 0, len = data['photosList'].length; i < len; i++) {
                       var tempOb = {};
                       var imgUrl = data['photosList'][i]['imgData'];
                        tempOb['imgurl'] = addQuotes(imgUrl.substr(imgUrl.lastIndexOf('/') + 1));
                        tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                        tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                        Activity_Event_Captures.push(pushKey(tempOb));
                   }
           
              }

                                resultData.push({
            'Activity_Event_Captures': Activity_Event_Captures
        });


                break;
            case "9":
                var Tour_Plan = {};
                Tour_Plan.worktype_code = addQuotes(data.worktype_code);
                Tour_Plan.worktype_name = addQuotes(data.worktype_name);

                if(data.Worked_with_Code==undefined || data.Worked_with_Code=='undefined'){
                   data.Worked_with_Code=""; 
                }
                Tour_Plan.Worked_with_Code = addQuotes(data.Worked_with_Code);
                Tour_Plan.Worked_with_Name = addQuotes(data.Worked_with_Name);
               
                Tour_Plan.sfName = addQuotes(data.sfName);
                Tour_Plan.objective = addQuotes(data.remarks);
                Tour_Plan.Tour_Date = addQuotes(data.date);
                jointWorkString = '';
              if(data.jontWorkSelectedList!=undefined){
                for (var t = 0, jwlen = data.jontWorkSelectedList.length; t < jwlen; t++) {
                    if (t != 0)
                        jointWorkString += "$$";
                    jointWorkString += data.jontWorkSelectedList[t].jointworkname;

                }
             }
             Multiroutename = '',MultiRouteCode='';
                 if(data.MultiRoute!=undefined){
                for (var t = 0, jwlen = data.MultiRoute.length; t < jwlen; t++) {
                    if (t != 0)
                        Multiroutename += "$$";
                    MultiRouteCode+="$$";
                    Multiroutename += data.MultiRoute[t].name;
                    MultiRouteCode+=data.MultiRoute[t].jointwork;

                }
             }
               Multiretailername = '',MultiretailerCode='';
                 if(data.MultiRetailer!=undefined){
                for (var t = 0, jwlen = data.MultiRetailer.length; t < jwlen; t++) {
                    if (t != 0)
                    Multiretailername += ",";
                    MultiretailerCode+=",";
                    Multiretailername += data.MultiRetailer[t].name;
                    MultiretailerCode+=data.MultiRetailer[t].id;

                }
             }
                Tour_Plan.RouteName = addQuotes(Multiroutename);
                Tour_Plan.RouteCode =addQuotes(MultiRouteCode);
                Tour_Plan.Multiretailername = addQuotes(Multiretailername);
                Tour_Plan.MultiretailerCode =addQuotes(MultiretailerCode);
                Tour_Plan.worked_with = addQuotes(jointWorkString);
                if (data.SF_type == 2) {
                    Tour_Plan.HQ_Code = addQuotes(data.HQ_Code);
                    Tour_Plan.HQ_Name = addQuotes(data.HQ_Name);
                } else {
                    Tour_Plan.HQ_Code = addQuotes();
                    Tour_Plan.HQ_Name = addQuotes();
                }
                //  Tour_Plan.Market = addQuotes(data.market);
                resultData.push({
                    'Tour_Plan': Tour_Plan
                });
                break;
            case "10":
                resultData.push({
                    'expense': data
                });
            case "11":
                resultData.push({
                    'locationDetails': data
                });
            case "12":
                var LeaveForm = {};
                LeaveForm.Leave_Type = addQuotes(data.Leave_Type);
                LeaveForm.From_Date = data.From_Date;
                LeaveForm.To_Date = addQuotes(data.To_Date);
                LeaveForm.Reason = addQuotes(data.Reason);
                LeaveForm.address = addQuotes(data.address);
                LeaveForm.No_of_Days = data.No_of_Days;

                resultData.push({
                    'LeaveForm': LeaveForm
                });
                break;
            case "13":
                var LeaveApproval = {};
                LeaveApproval.From_Date = data.LA.From_Date;
                LeaveApproval.To_Date = addQuotes(data.LA.To_Date);
                LeaveApproval.No_of_Days = data.LA.LeaveDays;
                LeaveApproval.Sf_Code = data.LA.Sf_Code;
                resultData.push({
                    'LeaveApproval': LeaveApproval
                });
                break;
            case "14":
                var LeaveReject = {};
                LeaveReject.From_Date = data.LA.From_Date;
                LeaveReject.To_Date = addQuotes(data.LA.To_Date);
                LeaveReject.No_of_Days = data.LA.LeaveDays;
                LeaveReject.reason = addQuotes(data.reason);
                LeaveReject.Sf_Code = data.LA.Sf_Code;
                resultData.push({
                    'LeaveReject': LeaveReject
                });
                break;
            case "15":
                var TPApproval = {};
                resultData.push({
                    'TPApproval': TPApproval
                });
                break;
            case "16":
                var TPReject = {};
                TPReject.reason = addQuotes(data.reason);
                resultData.push({
                    'TPReject': TPReject
                });
                break;
            case "17":
                var DCRApproval = {};
                resultData.push({
                    'DCRApproval': DCRApproval
                });
                break;
            case "18":
                var DCRReject = {};
                DCRReject.reason = addQuotes(data.reason);
                resultData.push({
                    'DCRReject': DCRReject
                });
                break;
            case "19":
                var TourPlanSubmit = {};
                resultData.push({
                    'TourPlanSubmit': TourPlanSubmit
                });
                break;
            case "21":
                var ExpenseApproval = {};
                resultData.push({
                    'ExpenseApproval': ExpenseApproval
                });
                break;
            case "22":
                var ExpenseReject = {};
                ExpenseReject.reason = addQuotes(data.reason);
                resultData.push({
                    'ExpenseReject': ExpenseReject
                });
                break;
            case "23":
                var DCRSummary = {};
                if (data.pcalls == '' || data.pcalls == null || data.pcalls == undefined || data.pcalls == "")
                    DCRSummary.pcalls = 0;
                else
                    DCRSummary.pcalls = data.pcalls;
                if (data.upcalls == '' || data.upcalls == null || data.upcalls == undefined || data.upcalls == "")
                    DCRSummary.upcalls = 0;
                else
                    DCRSummary.upcalls = data.upcalls;
                if (data.tcalls == '' || data.tcalls == null || data.tcalls == undefined || data.tcalls == "")
                    DCRSummary.tcalls = 0;
                else
                    DCRSummary.tcalls = data.tcalls;
                if (data.DCR_TLSD == '' || data.DCR_TLSD == null || data.DCR_TLSD == undefined || data.DCR_TLSD == "")
                    DCRSummary.DCR_TLSD = 0;
                else
                    DCRSummary.DCR_TLSD = data.DCR_TLSD;
                if (data.DCR_LPC == '' || data.DCR_LPC == null || data.DCR_LPC == undefined || data.DCR_LPC == "")
                    DCRSummary.DCR_LPC = 0;
                else
                    DCRSummary.DCR_LPC = data.DCR_LPC;
                DCRSummary.brandwise = data.brandwise;
                resultData.push({
                    'DCRSummary': DCRSummary
                });
                break;
            case "24":
            case "26":
            case "27":
                resultData.push({
                    'DailyInventory': data
                });
                break;
            case "25":
                resultData.push({
                    'OrderReturn': data
                });
                break;
            case "28":

                var EA = {};
                /*EA.Fare = data.Fare;
                EA.LEOS = data.LEOS.name;
                EA.FromPlace=data.FromPlace;
                EA.FromDeparture=data.FromDeparture;
                EA.ToPlace=data.ToPlace;
                EA.ToArrival=data.ToArrival;
                EA.TotalKMofbike=data.TotalKM;
                EA.MOC = data.MOC.name;
                EA.KM=data.KA;
                EA.selectVal = data.selectVal;
                EA.remarks = data.remarks;
                EA.Amount = data.remarks;*/

                if (data.photosList != undefined) {
                for (var i = 0, len = data['photosList'].length; i < len; i++) {
                    var tempOb = {};
                    var imgUrl = data['photosList'][i]['imgData'];
                    var moddate = new Date(data['modifiedDate']);
                    var mdateStr = moddate.getFullYear() + "-" + (moddate.getMonth() + 1) + "-" + moddate.getDate() + " " + moddate.getHours() + ":" + moddate.getMinutes() + ":" + moddate.getSeconds();
                    tempOb['imgurl'] = addQuotes(imgUrl.substr(imgUrl.lastIndexOf('/') + 1));
                    tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                    tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                    Activity_Event_Captures.push(pushKey(tempOb));
                 }
                }


                 EA.MOT="";
                if(data.MOT!=undefined)
                EA.MOT = data.MOT;
                EA.BusFare=data.Busfare;
                EA.Start_image=data.Start_Expense.Start_Photo;
                EA.Start_Km=data.Start_Expense.KM;
                console.log('OUTPUT'+JSON.stringify(data));
                console.log('PHOTO'+data.StopWorkPhoto);
                if(data.StopWorkPhoto!=undefined){
                    EA.Stop_image=data.StopWorkPhoto.imgurl;
                                EA.Stop_km=data.Stop_Expense.EndKm;
                }
                
                EA.Remarks=data.remarks;
                resultData.push({
                    'dailyExpense': data.DAExpense
                });
                resultData.push({
                    'EA': EA
                });
                resultData.push({"ActivityCaptures": Activity_Event_Captures});
                break;
            case "30":
                resultData.push({
                    'InvoiceEntry': data
                });
                break;

             case "31":

                   if (data.photosList != undefined) {
                     for (var i = 0, len = data['photosList'].length; i < len; i++) {
                    var tempOb = {};
                    var imgUrl = data['photosList'][i]['imgData'];
                    tempOb['imgurl'] = addQuotes(imgUrl.substr(imgUrl.lastIndexOf('/') + 1));
                    tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                    tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                    Activity_Event_Captures.push(pushKey(tempOb));
                 }
                }

                      var DoortoDoorserver = [];

            if(data.DoortoDoorSEndToServer!=undefined){
                for (var i = 0, len = data['DoortoDoorSEndToServer'].length; i < len; i++) {
                      var tempObb= {};
                    tempObb['PromotorName']=addQuotes(data['DoortoDoorSEndToServer'][i]['PromotorName']);
                    tempObb['Place']=addQuotes(data['DoortoDoorSEndToServer'][i]['Place']);
                    tempObb['Taken']=addQuotes(data['DoortoDoorSEndToServer'][i]['Taken']);
                    tempObb['Issue']=addQuotes(data['DoortoDoorSEndToServer'][i]['Issue']);
                    tempObb['Stime']=addQuotes(data['DoortoDoorSEndToServer'][i]['Stime']);
                    tempObb['Etime']=addQuotes(data['DoortoDoorSEndToServer'][i]['Etime']);
                    DoortoDoorserver.push(pushKey(tempObb));                      
                     }
                }
                    resultData.push({'DOOR_TO_DOOR':DoortoDoorserver });
                    resultData.push({"Remarks":data['Remarks']});
                    resultData.push({"FWFlg":data['FWFlg']});
                    resultData.push({"No0fcoupen":data['Coupons']});
                    resultData.push({"Retailer":data['Retailername']});
                    resultData.push({'Distributors': data['inshopDisti']});
                    resultData.push({'Route': data['inshopRoute']});
                    resultData.push({"ActivityCaptures": Activity_Event_Captures});
                break;

             case "32":
                 var DoortoDoorserver = [];
                 var activity_sample_reports = [];

                  if (data.photosList != undefined) {
                     for (var i = 0, len = data['photosList'].length; i < len; i++) {
                       var tempOb = {};
                       var imgUrl = data['photosList'][i]['imgData'];
                        tempOb['imgurl'] = addQuotes(imgUrl.substr(imgUrl.lastIndexOf('/') + 1));
                        tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                        tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                        Activity_Event_Captures.push(pushKey(tempOb));
                   }
           
              }


            if(data.InshopActivitySEndToServer!=undefined){
            for (var i = 0, len = data['InshopActivitySEndToServer'].length; i < len; i++) {
                var tempObb= {};
                    tempObb['PromotorName']=addQuotes(data['InshopActivitySEndToServer'][i]['PromotorName']);
                    tempObb['Value']=addQuotes(data['InshopActivitySEndToServer'][i]['Value']);
                    tempObb['Stime']=addQuotes(data['InshopActivitySEndToServer'][i]['Stime']);
                    tempObb['Etime']=addQuotes(data['InshopActivitySEndToServer'][i]['Etime']);
                     DoortoDoorserver.push(pushKey(tempObb));                        
                    }
                  }
           if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        // if (data['productSelectedList'][i]['rx_qty'] > 0) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty'];
                        tempOb['Product_Rx_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['sample_qty'];
                        tempOb['Product_Sample_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['product_netwt'];
                        tempOb['net_weight'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['free'];
                        tempOb['free'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount'];
                        tempOb['discount'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount_price'];
                        tempOb['discount_price'] = (Qty == null) ? 0 : Qty;
                        Rate = data['productSelectedList'][i]['Rate'];
                        tempOb['Rate'] = (Rate == null) ? 0 : Rate;
                        Mfg_Date = data['productSelectedList'][i]['Mfg_Date'];
                        tempOb['Mfg_Date'] = (Mfg_Date == null) ? '' : Mfg_Date;
                        cb_qty = data['productSelectedList'][i]['cb_qty'];
                        tempOb['cb_qty'] = (cb_qty == null) ? '' : cb_qty;
                        PromoVal = data['productSelectedList'][i]['PromoVal'];
                        tempOb['PromoVal'] = (PromoVal == null) ? '' : PromoVal;

                        activity_sample_reports.push(pushKey(tempOb, {
                            "Activity_MSL_Code": "Activity_Doctor_Report"
                        }));
                        //}
                    }
                }
                       resultData.push({'Inshop_Activity':DoortoDoorserver });
                       resultData.push({"Remarks":data['Remarks']});
                       resultData.push({"FWFlg":data['FWFlg']});
                       resultData.push({'ActivityCaptures': Activity_Event_Captures});
                       resultData.push({'Inshop_Activity_Orders': activity_sample_reports});
                       resultData.push({'RetailerName': data['Retailername']});
                       resultData.push({'Distributors': data['inshopDisti']});
                       resultData.push({'Route': data['inshopRoute']});
                break;

    case "46":
                
                 var activity_sample_reports = [];

           if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        // if (data['productSelectedList'][i]['rx_qty'] > 0) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty'];
                        tempOb['Product_Rx_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['sample_qty'];
                        tempOb['Product_Sample_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['product_netwt'];
                        tempOb['net_weight'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['free'];
                        tempOb['free'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount'];
                        tempOb['discount'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount_price'];
                        tempOb['discount_price'] = (Qty == null) ? 0 : Qty;
                        Rate = data['productSelectedList'][i]['Rate'];
                        tempOb['Rate'] = (Rate == null) ? 0 : Rate;
                        Mfg_Date = data['productSelectedList'][i]['Mfg_Date'];
                        tempOb['Mfg_Date'] = (Mfg_Date == null) ? '' : Mfg_Date;
                        cb_qty = data['productSelectedList'][i]['cb_qty'];
                        tempOb['cb_qty'] = (cb_qty == null) ? '' : cb_qty;
                        PromoVal = data['productSelectedList'][i]['PromoVal'];
                        tempOb['PromoVal'] = (PromoVal == null) ? '' : PromoVal;
                        activity_sample_reports.push(pushKey(tempOb, {
                            "Activity_MSL_Code": "Activity_Doctor_Report"
                        }));
                        //}
                    }
                }
                       resultData.push({'Supplier_Master':data['Remarks'] });
                       resultData.push({"Remarks":data['Remarks']});
                       resultData.push({'Supplier_Master_Orders': activity_sample_reports});
                       resultData.push({'SupplierName': data['doctor']['name']});
                       resultData.push({'Route': data['SupplierRoute']});
                break;
            case "33":      
                var TP_Attendance = {}
                TP_Attendance.lat = addQuotes(data.lat);
                TP_Attendance.long = addQuotes(data.long);
                resultData.push({ 'TP_Attendance': TP_Attendance });

                break;

             case "34":
       var DisplayActivity = [];
         var activity_sample_reports = [];

    if (data.photosList != undefined) {
            for (var i = 0, len = data['photosList'].length; i < len; i++) {
                var tempOb = {};
                var imgUrl = data['photosList'][i]['imgData'];
                tempOb['imgurl'] = addQuotes(imgUrl.substr(imgUrl.lastIndexOf('/') + 1));
                tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                Activity_Event_Captures.push(pushKey(tempOb));
            }
            //resultData.push({'Activity_Event_Captures': Activity_Event_Captures});
        }

             if(data.ProductDisplayActivitySendToServer!=undefined){
             for (var i = 0, len = data['ProductDisplayActivitySendToServer'].length; i < len; i++) {
                       var tempObb= {};
                       tempObb['SDate']=addQuotes(data['ProductDisplayActivitySendToServer'][i]['SDate']);
                       tempObb['EDate']=addQuotes(data['ProductDisplayActivitySendToServer'][i]['EDate']);
                       tempObb['CurrentVolume']=addQuotes(data['ProductDisplayActivitySendToServer'][i]['CurrentVolume']);
                       tempObb['AddVolume']=addQuotes(data['ProductDisplayActivitySendToServer'][i]['AddVolume']);
                       tempObb['DiscountAmount']=addQuotes(data['ProductDisplayActivitySendToServer'][i]['DiscountAmount']);
                      DisplayActivity.push(pushKey(tempObb));                        
                   } 
            }
                       resultData.push({'Product_Display_Activity':DisplayActivity });
                       resultData.push({"Remarks":data['Remarks']});
                       resultData.push({"FWFlg":data['FWFlg']});
                       resultData.push({'ActivityCaptures': Activity_Event_Captures});
                       resultData.push({'Retailer': data['doctor']['id']});
                       resultData.push({'Distributors': data['stockist']['selected']['id']});
                       resultData.push({'Route': data['cluster']['selected']['id']});
                       resultData.push({'FieldForceName': data['FieldForceName']});
                       resultData.push({'HQSfCode': data['HQSfCode']});


                break;

                  case "35":
                var productdisplayapprovals = {};
                productdisplayapprovals.Sf_Code = data.LA.sfCode;
                productdisplayapprovals.Flag=1;
                resultData.push({
                    'ProductDisplayApprovals': productdisplayapprovals
                });
                break;
                case "36":
                var productdisplayReject = {};
                productdisplayReject.Sf_Code = data.LA.sfCode;
                productdisplayReject.Reson = data.reason;
                productdisplayReject.Flag = 2;
                resultData.push({
                    'ProductDisplayReject': productdisplayReject
                });
                break;


        case "37":
           
                 var activity_sample_reports = [];
           if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        // if (data['productSelectedList'][i]['rx_qty'] > 0) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty'];
                        tempOb['Product_Rx_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['sample_qty'];
                        tempOb['Product_Sample_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['product_netwt'];
                        tempOb['net_weight'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['free'];
                        tempOb['free'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount'];
                        tempOb['discount'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount_price'];
                        tempOb['discount_price'] = (Qty == null) ? 0 : Qty;
                        Rate = data['productSelectedList'][i]['Rate'];
                        tempOb['Rate'] = (Rate == null) ? 0 : Rate;
                        Mfg_Date = data['productSelectedList'][i]['Mfg_Date'];
                        tempOb['Mfg_Date'] = (Mfg_Date == null) ? '' : Mfg_Date;
                        cb_qty = data['productSelectedList'][i]['cb_qty'];
                        tempOb['cb_qty'] = (cb_qty == null) ? '' : cb_qty;
                        PromoVal = data['productSelectedList'][i]['PromoVal'];
                        tempOb['PromoVal'] = (PromoVal == null) ? '' : PromoVal;
                        area = data['productSelectedList'][i]['area'];
                        tempOb['area'] = (area == null) ? '' : area;
                        activity_sample_reports.push(pushKey(tempOb, {
                            "Activity_MSL_Code": "Activity_Doctor_Report"
                        }));
                        //}
                    }
                }

                resultData.push({'FieldDemo_Activity_Orders': activity_sample_reports});
                 resultData.push({'FormarName': data['FormarName']});
                resultData.push({"ContactNumber":data['ContactNumber']});
               
                resultData.push({"Remarks":data['remarks']});
                if(data['doctor']!=undefined ){
                resultData.push({"PACCS":data['doctor']['name']});
                }
           
                  resultData.push({"Crop":data['Crop']});
                      
                break;


             
             case "38":
           if (data != undefined) {

           resultData.push({'BatteryStatus': data});

                                }


                   break

            case "39":
           
               

                resultData.push({'PaccsMeeting':"PACCS Meeting"});
                 
               resultData.push({"PACCS":data['doctor']['name']});
                resultData.push({"Remarks":data['remarks']});
                 
                      
                break;

            case "40":
                 var InshopCheckinserver = [];
                 var activity_sample_reports = [];

                  if (data.photosList != undefined) {
                     for (var i = 0, len = data['photosList'].length; i < len; i++) {
                       var tempOb = {};
                       var imgUrl = data['photosList'][i]['imgData'];
                        tempOb['imgurl'] = addQuotes(imgUrl.substr(imgUrl.lastIndexOf('/') + 1));
                        tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                        tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                        Activity_Event_Captures.push(pushKey(tempOb));
                   }
           
              }


            if(data.InshopCheckIn!=undefined){
            for (var i = 0, len = data['InshopCheckIn'].length; i < len; i++) {
                var tempObb= {};
                   
                    tempObb['Stime']=addQuotes(data['InshopCheckIn'][i]['Stime']);
                    tempObb['Etime']=addQuotes(data['InshopCheckIn'][i]['Etime']);
                     InshopCheckinserver.push(pushKey(tempObb));                        
                    }
                  }
              if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        // if (data['productSelectedList'][i]['rx_qty'] > 0) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty'];
                        tempOb['Product_Rx_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['sample_qty'];
                        tempOb['Product_Sample_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['product_netwt'];
                        tempOb['net_weight'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['free'];
                        tempOb['free'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount'];
                        tempOb['discount'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount_price'];
                        tempOb['discount_price'] = (Qty == null) ? 0 : Qty;
                        Rate = data['productSelectedList'][i]['Rate'];
                        tempOb['Rate'] = (Rate == null) ? 0 : Rate;
                        Mfg_Date = data['productSelectedList'][i]['Mfg_Date'];
                        tempOb['Mfg_Date'] = (Mfg_Date == null) ? '' : Mfg_Date;
                        cb_qty = data['productSelectedList'][i]['cb_qty'];
                        tempOb['cb_qty'] = (cb_qty == null) ? '' : cb_qty;
                        PromoVal = data['productSelectedList'][i]['PromoVal'];
                        tempOb['PromoVal'] = (PromoVal == null) ? '' : PromoVal;
                        activity_sample_reports.push(pushKey(tempOb, {
                            "Activity_MSL_Code": "Activity_Doctor_Report"
                        }));
                        //}
                    }
                }
                       resultData.push({'Inshop_Activity_CheckIn':InshopCheckinserver });
                       resultData.push({"Remarks":data['Remarks']});
                       resultData.push({"FWFlg":data['FWFlg']});
                       resultData.push({'ActivityCaptures': Activity_Event_Captures});
                       resultData.push({'Inshop_Activity_Orders': activity_sample_reports});
                       resultData.push({'RetailerName': data['Retailername']});
                       resultData.push({'Distributors': data['inshopDisti']});
                       resultData.push({'Route': data['inshopRoute']});
                break;


        case "41":
        
                 
               resultData.push({"CallPreviewEkey":data});

 
 

        break;


       case "43":
                 var MSD = {};
                MSD.misseddate = data;

                resultData.push({
                    'DCRMissedDates': MSD
                });
                break;
       case "42":
                var LeaveForm = {};
                LeaveForm.Leave_Type = addQuotes(data.Leave_Type);
                LeaveForm.From_Date = data.From_Date;
                LeaveForm.To_Date = addQuotes(data.To_Date);
             

                resultData.push({
                    'LeaveFormValidate': LeaveForm
                });

                break;

                case "44":
                var EA = {};
                EA.LEOS = data.LEOS.name;
                EA.MOC = data.MOC.name;
                EA.selectVal = data.selectVal;

                EA.remarks = data.remarks;

                resultData.push({
                    'EA': EA
                });
                
                break;

                case "45":
           
                var GiftCardAryra = [];
                var tempObb=1;
            if(data.GiftCardAryra!=undefined){
                 var tempObb="";
                for (var i = 0, len = data['GiftCardAryra'].length; i < len; i++) {
                     
                     if (i != 0)
                    tempObb +="#"
                    tempObb +=data['GiftCardAryra'][i]['name']+'~'+data['GiftCardAryra'][i]['Qty'];
                   
                              
                            }
                }
                   
                jointWorkString="";
                if(data.BreedCategory!=undefined)
             for (var t = 0, jwlen = data.BreedCategory.length; t < jwlen; t++) {
                    if (t != 0)
                        jointWorkString += ",";
                    jointWorkString += data.BreedCategory[t].name;

                }
               
                activity_sample_reports={};
                var FieldDemo = {};
                var endate = new Date(data['eTime']);
                FieldDemo.FormarName =data['FormarName'];
                FieldDemo.ContactNumber = data['ContactNumber'];
                FieldDemo.Remarks = data['remarks'];
                FieldDemo.AreaName= data['areaname'];
                FieldDemo.CityName= data['cityname'];
                FieldDemo.Landmark= data['landmark'];


                //FieldDemo.Seeman =data['Semen'];
                FieldDemo.Address = data['Address'];
                FieldDemo.Catogoryformer = data.speciality.selected.id;
                if(data.FrequencyOfVisit!=undefined){
                FieldDemo.FrequencyOfVisit = data.FrequencyOfVisit.selected.id;
                }

              if(data['doctor']!=undefined ){
                 FieldDemo.PACCS = data['doctor']['name'];
                }

                if(data.CurrentlyUsing!=undefined){
                 FieldDemo.CUFSD=data.CurrentlyUsing.selected.id; 
                }

                FieldDemo.Crop = data['DFCrop'];
                FieldDemo.Route = data['Route'];
                FieldDemo.Breed = jointWorkString;
                FieldDemo.Class = addQuotes(data.class.selected.id);
                FieldDemo.Ukey = addQuotes(data.Ukey);
                FieldDemo.unlisted_cat_code = addQuotes(data.ClstrName);
                FieldDemo.DFDairyMP=data.DFDairyMP;
                FieldDemo.DFFSD=data.DFFSD;
                FieldDemo.DFPFSDM=data.DFPFSDM;
                FieldDemo.DFFreq=data.DFFreq;
                FieldDemo.MonthlyAI=data.AITMAI;
                FieldDemo.AITCU=data.AITCU;
                FieldDemo.AITMP=data.AITMP;
                FieldDemo.MCCNFPM=data.MCCNFPM;
                FieldDemo.MCCMilkColDaily=data.MCCMilkColDaily;
                FieldDemo.VetsMP=data.VetsMP;
                FieldDemo.Date=endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + "00:00:00";
                resultData.push({'NewContact': activity_sample_reports});
                resultData.push({'NewContactOrder': FieldDemo});

                resultData.push({'GIFTCARD':tempObb });

                break;


                 case "47":
                                var Map_GEO_Customers = {};
                                Map_GEO_Customers.Cust_Code = addQuotes(data.id);
                                Map_GEO_Customers.lat = addQuotes(data.lat);
                                Map_GEO_Customers.long = addQuotes(data.long);
                                Map_GEO_Customers.StatFlag = '0';
                                resultData.push({
                                    'Map_GEO_Distributor': Map_GEO_Customers
                                });

                 break;



                case "48":
                                resultData.push({
                                    'SSstockUpdation': data
                                });
                                break;
                case "49":
                                resultData.push({
                                    'ChangePassword': data
                                });
                                break;

                 case "50":
                                resultData.push({
                                    'ProductAvailablity': data
                                });
                                break;
                 case "51":
                                resultData.push({
                                    'PrimaryClosing': data
                              });
                                break;
                 case "53":

                var unlisted_doctor_master = {};
                unlisted_doctor_master.Km = addQuotes(data.Km);
                unlisted_doctor_master.Remarks = addQuotes(data.Remarks);
                unlisted_doctor_master.MOT=addQuotes(data.MOT);
                 if (data.RetailerphotosList != undefined && data.RetailerphotosList.length>0) { 
                                var tempOb = {};
                                var imgUrl = data['RetailerphotosList'][0]['imgData'];
                                tempOb['imgurl'] = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                               // Activity_Event_Captures.push(pushKey(tempOb));
                                unlisted_doctor_master.Retailerphoto=tempOb;
                            }
                        resultData.push({'unlisted_Expense_start': unlisted_doctor_master});


                        console.log("SQL ERROR"+JSON.stringify(resultData))  ;
                 break;


                 case "54":
                                resultData.push({
                                    'primaryOrderReturn': data
                                });

                break

             case "55":
                resultData.push({
                'UpdateSlab': data
                 });
               break;
             case "56":
                resultData.push({ 'Quiz_Results': data });
                break;

                 case "57":
                     resultData.push({ 'quiz': data });
                break;


                 case "58":
                    resultData.push({
                    'UpdateSlabRetailer': data
                     });
                break;


                case "59":
                    resultData.push({
                    'Gift_Claim': data
                     });
                if (data.photosList != undefined) {
                     for (var i = 0, len = data['photosList'].length; i < len; i++) {
                       var tempOb = {};
                       var imgUrl = data['photosList'][i]['imgData'];
                        tempOb['imgurl'] = addQuotes(imgUrl.substr(imgUrl.lastIndexOf('/') + 1));
                        tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                        tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                        Activity_Event_Captures.push(pushKey(tempOb));
                   }
           
              }

                                resultData.push({
            'Activity_Event_Captures': Activity_Event_Captures
        });

                break;

case "60":
                var GiftApproval = {};
                GiftApproval.Aproval_Flag = data.Aproval_Flag;
                GiftApproval.reason = data.reason ;
                GiftApproval.FSf_Code =data.FSf_Code;
                resultData.push({
                    'GiftApproval': GiftApproval
                });
                break;



        }
        
        $http.defaults.useXDomain = true;
        if (choice == "20") {
            return $http({
                url: baseURL + 'createMail' + appendDS,
                method: method,
                data: data.replace(/&/g, ''),
                transformRequest: angular.identity,
                headers: {
                    'Content-Type': undefined
                }

            });
        } else {
            return $http({
                url: baseURL + url + appendDS,
                method: method,
                data: "data=" + encodeURIComponent(JSON.stringify(resultData).replace(/&/g, '')),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
        }
    }
    fmcgAPI.saveDCRData = function(method, url, data, update) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        if (data['worktype'] == undefined) {

            $http.defaults.useXDomain = true;
            return $http({
                url: baseURL,
                method: method,
                data: "data=",
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
        }
        var activity_report_APP = {};
        activity_report_APP['Worktype_code'] = addQuotes(data['worktype']['selected']['id']);

        try {
            activity_report_APP['Town_code'] = addQuotes(data['cluster']['selected']['id']);
        } catch (err) {

        }
        var endate = new Date(data['entryDate']);
        var moddate = new Date(data['modifiedDate']);
        var eDate = endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + " 00:00:00";
        var dateStr = endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + " " + endate.getHours() + ":" + endate.getMinutes() + ":" + endate.getSeconds();
        var mdateStr = moddate.getFullYear() + "-" + (moddate.getMonth() + 1) + "-" + moddate.getDate() + " " + moddate.getHours() + ":" + moddate.getMinutes() + ":" + moddate.getSeconds();

        activity_report_APP['dcr_activity_date'] = addQuotes(eDate);
        activity_report_APP['Daywise_Remarks'] = addQuotes(data['remarks']);
        activity_report_APP['eKey'] = data['eKey'];
        activity_report_APP['rx'] = addQuotes(data['rx']);
        if (data['rx'])
            activity_report_APP['rx_t'] = addQuotes(data['rx_t']);
        else
            activity_report_APP['nrx_t'] = addQuotes(data['nrx_t']);
        activity_report_APP['DataSF'] = addQuotes(data['subordinate']['selected']['id']);
        //to be s
        var activity_sample_reports = [];
        var activity_input_reports = [];
        var activity_pob_reports = [];
        var Activity_Event_Captures = [];
        var resultData = [];
        resultData.push({
            'Activity_Report_APP': activity_report_APP
        });

        var selectedC = "";
        var jointWorkString = "";
        try {
            selectedC = data['customer']['selected']['id'];
        } catch (err) {

        }
        if ((data['worktype']['selected']['FWFlg']).toString() !== "F" && (data['worktype']['selected']['FWFlg']).toString() !== "DH" && (data['worktype']['selected']['FWFlg']).toString() !== "IH") {
            selectedC = "10";
        } else {
            for (var t = 0, jwlen = data.jontWorkSelectedList.length; t < jwlen; t++) {
                if (t != 0)
                    jointWorkString += "$$";
                jointWorkString += data.jontWorkSelectedList[t].jointwork;

            }
        }

        var sOrdStk = '',
            sOrdNo = '0';
        if ((',1,4,').indexOf(',' + selectedC + ',') > -1 && userData.AppTyp == 1) {
            if (data.OrdStk != undefined) {
                if (data.OrdStk.selected != undefined) {
                    sOrdStk = data['OrdStk']['selected']['id'];
                    sOrdNo = data['OrderNo'];
                    if (sOrdNo == '')
                        sOrdNo = 0;
                }
            }
        }
        var tempD = parseFloat(data['pob']).toFixed(2);
        switch (selectedC) {
            case "1":
                var activity_doctor_rep = {};
                activity_doctor_rep['Doctor_POB'] = isNaN(tempD) ? 0 : tempD;
                if (data.instrumenttype != undefined) {
                    if (data.instrumenttype.selected != undefined)
                    activity_doctor_rep['PayType'] = data['instrumenttype']['selected']['id'];
                    activity_doctor_rep['PayTypeNm'] = data['instrumenttype']['name'];
                }
                activity_doctor_rep['PayDt'] = data['dateofinst'];
                activity_doctor_rep['PayRefNo'] = data['PayRefNo'];
                activity_doctor_rep['Worked_With'] = addQuotes(jointWorkString);
                activity_doctor_rep['Doc_Meet_Time'] = addQuotes(dateStr);
                activity_doctor_rep['modified_time'] = addQuotes(mdateStr);
                activity_doctor_rep['net_weight_value'] = (data['netweightvaluetotal'] == null) ? 0 : data['netweightvaluetotal'];
                if (data['stockist'] != undefined) {
                    activity_doctor_rep['stockist_code'] = addQuotes(data['stockist']['selected']['id']);
                    activity_doctor_rep['stockist_name'] = addQuotes(data['stockist']['name']);
                }
                if(data['superstockistid']!=undefined){
                     activity_doctor_rep['superstockistid'] = addQuotes(data['superstockistid']['selected']['id']);

                }
                discount = data['discounttt'];
                activity_doctor_rep['Discountpercent'] =(discount == null || discount==undefined ) ? 0 : discount;
                activity_doctor_rep['CheckinTime'] = data['Checkin'];
                activity_doctor_rep['CheckoutTime'] = data['Checkout'];
                activity_doctor_rep['routeWise'] = data['routeWise'];
                activity_doctor_rep['location'] = addQuotes(data['location']);
                activity_doctor_rep['geoaddress'] = ''; ///['geoaddress'].replace(/\//g,'\\\/');
                if(data['PhoneOrderTypes']!=undefined)
                activity_doctor_rep["PhoneOrderTypes"]=data['PhoneOrderTypes']['selected']['id'];
                if(data['SlabTypes']!=undefined){
                    activity_doctor_rep["SlabTypes_Id"]=data['SlabTypes']['selected']['id'];
                     activity_doctor_rep["SlabNames"]=data['SlabTypes']['name'];

                }
                if (userData.AppTyp == 1) {
                activity_doctor_rep['Order_Stk'] = addQuotes(sOrdStk);
                activity_doctor_rep['Order_No'] = addQuotes(sOrdNo);
                }
                if (data['routeWise'] == 0)
                activity_doctor_rep['doctor_name'] = addQuotes(data['doctor']['name']);
                activity_doctor_rep['rootTarget'] = data['rootTarget'];
                activity_doctor_rep['orderValue'] = data['netamount'];
                activity_doctor_rep['demo_given_name'] = data['demogiven'];
                if (data['rateType'] == undefined)
                activity_doctor_rep['rateMode'] = 'Nil';
                else
                activity_doctor_rep['rateMode'] = data['rateType'];
                discountprice = data['dis'];
                discount=(discountprice == null || discountprice==undefined) ? 0 : discountprice;
                activity_doctor_rep['discount_price'] = discount;
                activity_doctor_rep['doctor_code'] = addQuotes(data['doctor']['selected']['id']);
                activity_doctor_rep['doctor_name'] = addQuotes(data['doctor']['name']);
                pushKey(activity_doctor_rep, {
                    "Activity_Report_Code": "'Activity_Report_APP'"
                });
                resultData.push({
                    'Activity_Doctor_Report': activity_doctor_rep
                });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        // if (data['productSelectedList'][i]['rx_qty'] > 0) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty'];
                        tempOb['Product_Rx_Qty'] = (Qty == null) ? 0 : Qty;
                        UnitId = data['productSelectedList'][i]['UnitId'];
                        tempOb['UnitId'] = (UnitId == null) ? 0 : UnitId;
                        rx_Conqty = data['productSelectedList'][i]['rx_Conqty'];
                        tempOb['rx_Conqty'] = (rx_Conqty == null) ? 0 : rx_Conqty;
                        
                        Qty = data['productSelectedList'][i]['rx_nqty'];
                        tempOb['Product_Rx_NQty'] = (Qty == null) ? 0 : Qty;

                        Qty = data['productSelectedList'][i]['sample_qty'];
                        tempOb['Product_Sample_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['product_netwt'];
                        tempOb['net_weight'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['free'];
                        tempOb['free'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['FQname'];
                        tempOb['FreePQty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['FreeP_Code'];
                        tempOb['FreeP_Code'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['Fname'];
                        tempOb['Fname'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount'];
                        tempOb['discount'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['discount_price'];
                        tempOb['discount_price'] = (Qty == null) ? 0 : Qty;
                        Rate = data['productSelectedList'][i]['Rate'];
                        tempOb['Rate'] = (Rate == null) ? 0 : Rate;
                        Mfg_Date = data['productSelectedList'][i]['Mfg_Date'];
                        tempOb['Mfg_Date'] = (Mfg_Date == null) ? '' : Mfg_Date;
                        cb_qty = data['productSelectedList'][i]['cb_qty'];
                        tempOb['cb_qty'] = (cb_qty == null) ? '' : cb_qty;
                        RcpaId = data['productSelectedList'][i]['RcpaId'];
                        tempOb['RcpaId'] = (RcpaId == null) ? '' : RcpaId;
                         Ccb_qty = data['productSelectedList'][i]['Ccb_qty'];
                        tempOb['Ccb_qty'] = (Ccb_qty == null) ? '' : Ccb_qty;
                        PromoVal = data['productSelectedList'][i]['PromoVal'];
                        tempOb['PromoVal'] = (PromoVal == null) ? '' : PromoVal;
                        rx_remarks = data['productSelectedList'][i]['rx_remarks'];
                        tempOb['rx_remarks'] = (rx_remarks == null) ? 0 : rx_remarks;
                         rx_remarks_Id = data['productSelectedList'][i]['rx_remarks_Id'];
                        tempOb['rx_remarks_Id'] = (rx_remarks_Id == null) ? 0 : rx_remarks_Id;
                        activity_sample_reports.push(pushKey(tempOb, {
                            "Activity_MSL_Code": "Activity_Doctor_Report"
                        }));
                        //}
                    }
                }
                resultData.push({
                    'Activity_Sample_Report': activity_sample_reports
                });
        
                data['giftSelectedList'] = data['giftSelectedList'] || [];
                for (var i = 0, len = data['giftSelectedList'].length; i < len; i++) {
                    var tempOb = {};
                    var gfcd = data['giftSelectedList'][i]['gift'];
                    tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                    if (Envrmnt == ".Net")
                        tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                    tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                    activity_input_reports.push(pushKey(tempOb, {
                        "Activity_MSL_Code": "Activity_Doctor_Report"
                    }));
                }
               
                resultData.push({
                    'Trans_Order_Details': activity_input_reports
                });

                 resultData.push({
                    'Activity_Input_Report': activity_input_reports
                });
                 
                break;
            case "2":
                var activity_chemist_rep = {};
                activity_chemist_rep['Chemist_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_chemist_rep['chemist_code'] = addQuotes(data['chemist']['selected']['id']);
                activity_chemist_rep['Worked_With'] = addQuotes(jointWorkString);
                activity_chemist_rep['location'] = addQuotes(data['location']);
                activity_chemist_rep['geoaddress'] = ''; //data['geoaddress'].replace(/\//g, '\\\/');
                activity_chemist_rep['Chm_Meet_Time'] = addQuotes(dateStr);
                activity_chemist_rep['modified_time'] = addQuotes(mdateStr);
                pushKey(activity_chemist_rep, {
                    "Activity_Report_Code": "'Activity_Report_APP'"
                });
                resultData.push({
                    'Activity_Chemist_Report': activity_chemist_rep
                });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net")
                            tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty'];
                        tempOb['Qty'] = (Qty == null) ? 0 : Qty;
                        activity_pob_reports.push(pushKey(tempOb, {
                            "activity_chemist_code": "Activity_Chemist_Report"
                        }));
                    }
                }
                resultData.push({
                    'Activity_POB_Report': activity_pob_reports
                });
                if (data.giftSelectedList != undefined) {
                    for (var i = 0, len = data.giftSelectedList.length; i < len; i++) {
                        var tempOb = {};
                        var gfcd = data['giftSelectedList'][i]['gift'];
                        tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                        if (Envrmnt == ".Net")
                            tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                        tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                        activity_input_reports.push(pushKey(tempOb, {
                            "activity_chemist_code": "Activity_Chemist_Report"
                        }));
                    }
                }
                resultData.push({
                    'Activity_Chm_Sample_Report': activity_input_reports
                });
                break;

            case "3":
                var activity_stockist_Rep = {};
                activity_stockist_Rep['Stockist_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_stockist_Rep['Worked_With'] = addQuotes(jointWorkString);
                activity_stockist_Rep['location'] = addQuotes(data['location']);
                activity_stockist_Rep['geoaddress'] = ''; //data['geoaddress'].replace(/\//g, '\\\/');
                activity_stockist_Rep['stockist_code'] = addQuotes(data['stockist']['selected']['id']);
                if(data['superstockistid']!=undefined){
                    activity_stockist_Rep['superstockistid'] = addQuotes(data['superstockistid']['selected']['id']);           
                }

               if(data['Super_Stck_code']!=undefined){
                 activity_stockist_Rep['Super_Stck_code'] = addQuotes(data['Super_Stck_code']['selected']['id']);   
               }
                activity_stockist_Rep['Stk_Meet_Time'] = addQuotes(dateStr);
                activity_stockist_Rep['modified_time'] = addQuotes(mdateStr);
                activity_stockist_Rep['date_of_intrument'] = data['dateofinst'];
                if (data['instrumenttype'] != undefined)
                activity_stockist_Rep['intrumenttype'] = data['instrumenttype']['name'];
                tempD = parseFloat(data['value'])
                activity_stockist_Rep['orderValue'] = isNaN(tempD) ? 0 : tempD;
                AOB=(data['Aob']==undefined?0:data['Aob']);
                activity_stockist_Rep['Aob'] = AOB;
                activity_stockist_Rep['CheckinTime'] = data['Checkin'];
                activity_stockist_Rep['CheckoutTime'] = data['Checkout'];
                //activity_stockist_Rep['orderValue'] = 0;
                pushKey(activity_stockist_Rep, {
                    "Activity_Report_Code": "'Activity_Report_APP'"
                });

              if(data['PhoneOrderTypes']!=undefined)
                activity_stockist_Rep["PhoneOrderTypes"]=data['PhoneOrderTypes']['selected']['id'];
                
                resultData.push({
                    'Activity_Stockist_Report': activity_stockist_Rep
                });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        if (data['productSelectedList'][i]['rx_qty'] > 0 || data['productSelectedList'][i]['Prx_qty'] > 0 || data['productSelectedList'][i]['cb_qty'] > 0) {
                            var tempOb = {};
                            var spcd = data['productSelectedList'][i]['product'];
                            tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                            if (Envrmnt == ".Net")
                                tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                             Qty = data['productSelectedList'][i]['rx_qty'];
                             PQty = data['productSelectedList'][i]['Prx_qty'];
                             tempOb['Qty'] = (Qty == null) ? 0 : Qty;
                             tempOb['PQty'] = (PQty == null) ? 0 : PQty;
                             cb_qty = data['productSelectedList'][i]['cb_qty'];
                             tempOb['cb_qty'] = (cb_qty == null) ? 0 : cb_qty;
                             Qty = data['productSelectedList'][i]['free'];
                             tempOb['free'] = (Qty == null) ? 0 : Qty;
                             Qty = data['productSelectedList'][i]['discount'];
                             tempOb['discount'] = (Qty == null) ? 0 : Qty;
                             Qty = data['productSelectedList'][i]['FreeP_Code'];
                             tempOb['FreeP_Code'] = (Qty == null) ? 0 : Qty;
                             Qty = data['productSelectedList'][i]['Fname'];
                             tempOb['Fname'] = (Qty == null) ? 0 : Qty;
                             Qty = data['productSelectedList'][i]['discount_price'];
                             tempOb['discount_price'] = (Qty == null) ? 0 : Qty;
                            activity_pob_reports.push(pushKey(tempOb, {
                                "activity_stockist_code": "Activity_Stockist_Report"
                            }));
                        }
                    }
                }
                resultData.push({
                    'Activity_Stk_POB_Report': activity_pob_reports
                });
                if (data.giftSelectedList != undefined) {
                    for (var i = 0, len = data.giftSelectedList.length; i < len; i++) {
                        var tempOb = {};
                        var gfcd = data['giftSelectedList'][i]['gift'];
                        tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                        if (Envrmnt == ".Net")
                            tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                        tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                        activity_input_reports.push(pushKey(tempOb, {
                            "activity_stockist_code": "Activity_Stockist_Report"
                        }));
                    }
                }
                resultData.push({
                    'Activity_Stk_Sample_Report': activity_input_reports
                });
                break;

        case "8":
               var activity_stockist_Rep = {};
                activity_stockist_Rep['Stockist_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_stockist_Rep['Worked_With'] = addQuotes(jointWorkString);
                activity_stockist_Rep['location'] = addQuotes(data['location']);
                activity_stockist_Rep['geoaddress'] = ''; //data['geoaddress'].replace(/\//g, '\\\/');
                activity_stockist_Rep['stockist_code'] = addQuotes(data['doctor']['id']);
                activity_stockist_Rep['Stk_Meet_Time'] = addQuotes(dateStr);
                activity_stockist_Rep['modified_time'] = addQuotes(mdateStr);
                activity_stockist_Rep['version'] =8;
                activity_stockist_Rep['doctor_id'] = addQuotes(data['doctor']['id']);
                if(data['superstockistid']!=undefined){
                activity_stockist_Rep['superstockistid'] = addQuotes(data['superstockistid']['selected']['id']);           
                }
                activity_stockist_Rep['date_of_intrument'] = data['dateofinst'];
                if (data['instrumenttype'] != undefined)
                activity_stockist_Rep['intrumenttype'] = data['instrumenttype']['name'];
                tempD = parseFloat(data['value'])
                activity_stockist_Rep['orderValue'] = isNaN(tempD) ? 0 : tempD;
                //activity_stockist_Rep['orderValue'] = 0;
                pushKey(activity_stockist_Rep, {
                    "Activity_Report_Code": "'Activity_Report_APP'"
                });

               if(data['PhoneOrderTypes']!=undefined)
                activity_stockist_Rep["PhoneOrderTypes"]=data['PhoneOrderTypes']['selected']['id'];
                
                resultData.push({
                    'Activity_Stockist_Report': activity_stockist_Rep
                });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        if (data['productSelectedList'][i]['rx_qty'] > 0 || data['productSelectedList'][i]['Prx_qty'] > 0) {
                            var tempOb = {};
                            var spcd = data['productSelectedList'][i]['product'];
                            tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                            if (Envrmnt == ".Net")
                                tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                            Qty = data['productSelectedList'][i]['rx_qty'];
                              PQty = data['productSelectedList'][i]['Prx_qty'];
                            tempOb['Qty'] = (Qty == null) ? 0 : Qty;
                             tempOb['PQty'] = (PQty == null) ? 0 : PQty;

                            activity_pob_reports.push(pushKey(tempOb, {
                                "activity_stockist_code": "Activity_Stockist_Report"
                            }));
                        }
                    }
                }
                resultData.push({
                    'Activity_Stk_POB_Report': activity_pob_reports
                });
                if (data.giftSelectedList != undefined) {
                    for (var i = 0, len = data.giftSelectedList.length; i < len; i++) {
                        var tempOb = {};
                        var gfcd = data['giftSelectedList'][i]['gift'];
                        tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                        if (Envrmnt == ".Net")
                            tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                        tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                        activity_input_reports.push(pushKey(tempOb, {
                            "activity_stockist_code": "Activity_Stockist_Report"
                        }));
                    }
                }
                resultData.push({
                    'Activity_Stk_Sample_Report': activity_input_reports
                });
                break;

            case "4":
                var activity_ulDoctor_Rep = {};
                activity_ulDoctor_Rep['UnListed_Doctor_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_ulDoctor_Rep['Worked_With'] = addQuotes(jointWorkString);
                activity_ulDoctor_Rep['location'] = addQuotes(data['location']);
                activity_ulDoctor_Rep['geoaddress'] = ''; //data['geoaddress'].replace(/\//g, '\\\/');
                activity_ulDoctor_Rep['UnListed_Doc_Meet_Time'] = addQuotes(dateStr);
                activity_ulDoctor_Rep['modified_time'] = addQuotes(mdateStr);
                if (userData.AppTyp == 1) {
                activity_ulDoctor_Rep['Order_Stk'] = sOrdStk;
                activity_ulDoctor_Rep['Order_No'] = sOrdNo;
                }
                activity_ulDoctor_Rep['uldoctor_code'] = addQuotes(data['uldoctor']['selected']['id']);
                pushKey(activity_ulDoctor_Rep, {
                    "Activity_Report_Code": "'Activity_Report_APP'"
                });
                resultData.push({
                    'Activity_UnListedDoctor_Report': activity_ulDoctor_Rep
                });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net")
                            tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        tempOb['Product_Rx_Qty'] = data['productSelectedList'][i]['rx_qty'];
                        tempOb['Product_Sample_Qty'] = data['productSelectedList'][i]['sample_qty'];
                        activity_sample_reports.push(pushKey(tempOb, {
                            "activity_msl_code": "Activity_UnListedDoctor_Report"
                        }));
                    }
                }
                resultData.push({
                    'Activity_Unlistedsample_Report': activity_sample_reports
                });
                data['giftSelectedList'] = data['giftSelectedList'] || [];
                for (var i = 0, len = data['giftSelectedList'].length; i < len; i++) {
                    var tempOb = {};
                    var gfcd = data['giftSelectedList'][i]['gift'];
                    tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                    if (Envrmnt == ".Net")
                        tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                    tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                    activity_input_reports.push(pushKey(tempOb, {
                        "activity_msl_code": "Activity_UnListedDoctor_Report"
                    }));
                }
                resultData.push({
                    'activity_unlistedGift_Report': activity_input_reports
                });
                break;
            case "7":
                var DCRDetail_Distributors_Hunting = {};
                pushKey(DCRDetail_Distributors_Hunting, {
                    "Activity_Report_Code": "'Activity_Report_APP'"
                });
                DCRDetail_Distributors_Hunting.name = addQuotes(data.dh_Shop_Name);
                DCRDetail_Distributors_Hunting.address = addQuotes(data.dh_address);
                DCRDetail_Distributors_Hunting.contact = addQuotes(data.dh_Contact_Person);
                DCRDetail_Distributors_Hunting.phone = addQuotes(data.dh_Phone_Number);
                DCRDetail_Distributors_Hunting.area = addQuotes(data.dh_area);
                DCRDetail_Distributors_Hunting['Worked_With'] = addQuotes(jointWorkString);
                resultData.push({
                    'DCRDetail_Distributors_Hunting': DCRDetail_Distributors_Hunting
                });
                break;
            default:

                break;
        }

        if (data.photosList != undefined) {
            for (var i = 0, len = data['photosList'].length; i < len; i++) {
                var tempOb = {};
                var imgUrl = data['photosList'][i]['imgData'];
                tempOb['imgurl'] = addQuotes(imgUrl.substr(imgUrl.lastIndexOf('/') + 1));
                tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                Activity_Event_Captures.push(pushKey(tempOb, {
                    "Activity_Report_Code": "Activity_Report_APP"
                }));
            }
            
        }

          var activity_pending_pills = [];
            if (data.Pendinglisttt != undefined) {
                for (var i = 0, len = data['Pendinglisttt'].length; i < len; i++) {
                    var tempObb = {};
                    tempObb['Bill_No'] = addQuotes(data['Pendinglisttt'][i]['billno']);
                    tempObb['Bill_Amount'] = addQuotes(data['Pendinglisttt'][i]['billamt']);
                    tempObb['Balance_Amount'] = addQuotes(data['Pendinglisttt'][i]['BalanceAmout']);
                    tempObb['Collected_Amount'] = addQuotes(data['Pendinglisttt'][i]['coll_amt']);
                    activity_pending_pills.push(pushKey(tempObb));

                }
            }

             var Compititor=[]

             if (data.CurrentCompProduct != undefined) {
                    for (var i = 0, len = data['CurrentCompProduct'].length; i < len; i++) {
                      if (data['CurrentCompProduct'][i]['Qty'] > 0) {
                        var tempOb = {}; 
                        CCType = data['CurrentCompType'];
                        tempOb['CCType'] = (CCType == null) ? 0 : CCType;
                        pid = data['CurrentCompProduct'][i]['id'];
                        tempOb['Pid'] = (pid == null) ? 0 : pid;
                        bid = data['CurrentCompProduct'][i]['bid'];
                        tempOb['bid'] = (bid == null) ? 0 : bid;
                        Name = data['CurrentCompProduct'][i]['Name'];
                        tempOb['Name'] = (Name == null) ? 0 : Name;
                        Cat_Id = data['CurrentCompProduct'][i]['Cpid'];
                        tempOb['Cat_Id'] = (Cat_Id == null) ? '' : Cat_Id;
                        rate = data['CurrentCompProduct'][i]['Rate'];
                        tempOb['Rate'] = (rate == null) ? '' : rate;
                        qty = data['CurrentCompProduct'][i]['Qty'];
                        tempOb['Qty'] = (qty == null) ? '' : qty;
                        Compititor.push(tempOb);
                      }
                    }
                }

        resultData.push({
            'Activity_Event_Captures': Activity_Event_Captures
        });
        resultData.push({
            'PENDING_Bills': activity_pending_pills
        });
         resultData.push({
                    'Compititor_Product': Compititor
                });
   console.log('Request_DCR'+JSON.stringify(resultData));
        $http.defaults.useXDomain = true;
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + JSON.stringify(resultData).replace(/&/g, ''),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    }


    return fmcgAPI;
});
fmcgServices.factory('generateUID', function($http) {
    var gUID = {};
    gUID.generate = function(separator) {
        var delim = separator || "-";

        function S4() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }
        return (S4() + S4() + delim + S4() + delim + S4() + delim + S4() + delim + S4() + S4() + S4());
    };
    return gUID;
});
fmcgServices.factory('fmcgLocalStorage', function() {
    var fmcgLocalStorage = {};
    fmcgLocalStorage.addData = function(key, value) {
        var predata = window.localStorage.getItem(key) || "[]";
        predata = JSON.parse(predata);
        predata.push(value);
        window.localStorage.setItem(key, JSON.stringify(predata));
        return true;
    }
    fmcgLocalStorage.createData = function(key, value) {
        window.localStorage.setItem(key, JSON.stringify(value));
        return true;
    }

    fmcgLocalStorage.getData = function(key) {
        var temp = window.localStorage.getItem(key);
        var ugData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        return ugData;
    }
    fmcgLocalStorage.getItemCount = function(key) {
        var data = window.localStorage.getItem(key);
        if (data == null || data == undefined)
            return 0;
        var count = 0;
        count = JSON.parse(data).length;
        return count;
    }
    fmcgLocalStorage.getEntryCount = function() {
        var data = window.localStorage.getItem("draft");
        var temp = JSON.parse(data);
        var result = {};
        var obj = {
            'doctor_count': 0,
            'chemist_count': 0,
            'stockist_count': 0,
            'uldoctor_count': 0
        };
        if (temp != null && temp.length > 0) {
            if ((temp[0]['worktype']['selected']['FWFlg']).toString() === "F") {
                for (var i = 0, len = temp.length; i < len; i++) {
                    if (temp[i]['customer'])
                        switch (temp[i]['customer']['selected']['id']) {
                            case "1":
                                obj['doctor_count']++;
                                break;
                            case "2":
                                obj['chemist_count']++;
                                break;
                            case "3":
                                obj['stockist_count']++;
                                break;
                            case "4":
                                obj['uldoctor_count']++;
                                break;
                        }
                }
                result['data'] = obj;
                result['success'] = true;
            } else {
                result['success'] = false;
                result['data'] = {};
                result['data']['daywise_remarks'] = temp[0]['remarks'];
                result['data']['worktype_code'] = temp[0]['worktype']['selected']['id'];
            }
        } else {
            result['ndata'] = true;
        }

        return result;
    }

    fmcgLocalStorage.getOutboxCount = function() {
        var data = window.localStorage.getItem("saveLater");
        var temp = JSON.parse(data);
        var result = {};
        var obj = {
            'doctor_count': 0,
            'chemist_count': 0,
            'stockist_count': 0,
            'uldoctor_count': 0
        };
        if (temp != null && temp.length > 0) {
            if ((temp[0]['worktype']['selected']['FWFlg']).toString() === "F") {
                for (var i = 0, len = temp.length; i < len; i++) {
                    if (temp[i]['customer'])
                        switch (temp[i]['customer']['selected']['id']) {
                            case "1":
                                obj['doctor_count']++;
                                break;
                            case "2":
                                obj['chemist_count']++;
                                break;
                            case "3":
                                obj['stockist_count']++;
                                break;
                            case "4":
                                obj['uldoctor_count']++;
                                break;
                        }
                }
                result['data'] = obj;
                result['success'] = true;
            } else {
                result['success'] = false;
                result['data'] = {};
                result['data']['daywise_remarks'] = temp[0]['remarks'];
                result['data']['worktype_code'] = temp[0]['worktype']['selected']['id'];
            }
        } else {
            result['ndata'] = true;
        }

        return result;
    }
    return fmcgLocalStorage;
});