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
        if (userData)
        {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        var str = {
            arc: data.arc,
            amc: data.amc
        };
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + JSON.stringify(str), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: {'Content-Type': 'application/x-www-form-urlencoded'}
        });
    }
    fmcgAPI.getDataList = function(method, url, data) {
        var tablistNSL = ["activity_Report_APP", "activity_doctor_report", "activity_Chemist_report", "activity_stockist_report", "activity_unlisteddoctor_Report", "activity_unlistedsample_Report", "activity_sample_report", "activity_input_report", "activity_chm_sample_report","speciality_master"];
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData)
        {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        var str = {
            tableName: data[0],
            coloumns: data[1],
            today: data[4],
            join: data[2],
            where: data[3],
            or: data[5],
            wt: data[6],
            sfCode: data[7],
        };
        if (tablistNSL.indexOf(data[0]) === -1)
        {
            str.orderBy = "[\"name asc\"]";
        }
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + JSON.stringify(str), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: {'Content-Type': 'application/x-www-form-urlencoded'}
        });
    };
    fmcgAPI.getPostData = function(method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData)
        {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }

        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + JSON.stringify(data), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: {'Content-Type': 'application/x-www-form-urlencoded'}
        });
    };
    pushKey = function(obj1, obj2) {
        obj1['f_key'] = obj2;
        return obj1;
    }
    addQuotes = function(data)
    {
        data = data ? data : "";
        return "\'" + data + "\'";
    }
    fmcgAPI.updateDCRData = function(method, url, data)
    {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData)
        {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        return $http({
            method: method,
            url: baseURL + url + appendDS,
            data: "data=" + JSON.stringify(data),
            headers: {'Content-Type': 'application/x-www-form-urlencoded'}
        });
    }
    fmcgAPI.addMAData = function(method, url, choice, data)
    {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData)
        {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        var resultData = [];
        switch (choice)
        {
            case "1":
                var chemists_master = {};
                chemists_master.town_code = addQuotes(data.cluster.selected.id);
                chemists_master.chemists_name = addQuotes(data.name);
                resultData.push({'chemists_master': chemists_master});
                break;
            case "2":
                var unlisted_doctor_master = {};
                unlisted_doctor_master.town_code = addQuotes(data.cluster.selected.id);
                unlisted_doctor_master.unlisted_doctor_name = addQuotes(data.name);
                resultData.push({'unlisted_doctor_master': unlisted_doctor_master});

        }
        $http.defaults.useXDomain = true;
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + JSON.stringify(resultData),
            headers: {'Content-Type': 'application/x-www-form-urlencoded'}
        });
    }
    fmcgAPI.saveDCRData = function(method, url, data, update)
    {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData)
        {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        var activity_report_APP = {};
        activity_report_APP['Worktype_code'] = addQuotes(data['worktype']['selected']['id']);

        try {
            activity_report_APP['Town_code'] = addQuotes(data['cluster']['selected']['id']);
        }
        catch (err)
        {

        }
        activity_report_APP['Daywise_Remarks'] = addQuotes(data['remarks']);
        activity_report_APP['rx'] = addQuotes(data['rx']);
        if (data['rx'])
            activity_report_APP['rx_t'] = addQuotes(data['rx_t']);
        else
            activity_report_APP['nrx_t'] = addQuotes(data['nrx_t']);
        //to be s
        var activity_sample_reports = [];
        var activity_input_reports = [];
        var resultData = [];
        resultData.push({'Activity_Report_APP': activity_report_APP});
        var endate = new Date(data['entryDate']);
        var moddate=new Date(data['modifiedDate']);
                
        var dateStr = endate.getFullYear() + "-" + endate.getMonth() + "-" + endate.getDate() + " " + endate.getHours() + ":" + endate.getMinutes() + ":" + endate.getSeconds();
        var mdateStr = moddate.getFullYear() + "-" + moddate.getMonth() + "-" + moddate.getDate() + " " + moddate.getHours() + ":" + moddate.getMinutes() + ":" + moddate.getSeconds();
        
        var selectedC = "";
        var jointWorkString = "";
        try {
            selectedC = data['customer']['selected']['id'];
        }
        catch (err)
        {

        }
        if ((data['worktype']['selected']['id']).toString() !== "field work")
        {
            selectedC = "10";
        }
        else
        {
            for (var t = 0, jwlen = data.jontWorkSelectedList.length; t < jwlen; t++)
            {
                if (t != 0)
                    jointWorkString += "$";
                jointWorkString += data.jontWorkSelectedList[t].jointwork;

            }
        }
        var tempD = parseInt(data['pob']);
        switch (selectedC)
        {
            case "1":
                var activity_doctor_rep = {};

                activity_doctor_rep['Doctor_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_doctor_rep['Worked_With'] = addQuotes(jointWorkString);
                activity_doctor_rep['Doc_Meet_Time'] = addQuotes(dateStr);
                activity_doctor_rep['modified_time']=addQuotes(mdateStr);
                activity_doctor_rep['location'] = addQuotes(data['location']);
                activity_doctor_rep['doctor_code'] = addQuotes(data['doctor']['selected']['id']);
                pushKey(activity_doctor_rep, {"Activity_Report_Code": "'Activity_Report_APP'"});
                resultData.push({'Activity_Doctor_Report': activity_doctor_rep});
                for (var i = 0, len = data['productSelectedList'].length; i < len; i++)
                {
                    var tempOb = {};

                    tempOb['Product_Code'] = addQuotes(data['productSelectedList'][i]['product']);
                    tempOb['Product_Rx_Qty'] = data['productSelectedList'][i]['rx_qty'];
                    tempOb['Product_Sample_Qty'] = data['productSelectedList'][i]['sample_qty'];
                    activity_sample_reports.push(pushKey(tempOb, {"Activity_MSL_Code": "Activity_Doctor_Report"}));
                }
                resultData.push({'Activity_Sample_Report': activity_sample_reports});
                for (var i = 0, len = data['giftSelectedList'].length; i < len; i++)
                {
                    var tempOb = {};
                    tempOb['Gift_Code'] = addQuotes(data['giftSelectedList'][i]['gift']);
                    tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                    activity_input_reports.push(pushKey(tempOb, {"Activity_MSL_Code": "Activity_Doctor_Report"}));
                }
                resultData.push({'Activity_Input_Report': activity_input_reports});
                break;
            case "2":
                var activity_chemist_rep = {};
                activity_chemist_rep['Chemist_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_chemist_rep['chemist_code'] = addQuotes(data['chemist']['selected']['id']);
                activity_chemist_rep['Worked_With'] = addQuotes(jointWorkString);
                activity_chemist_rep['location'] = addQuotes(data['location']);
                activity_chemist_rep['Chm_Meet_Time'] = addQuotes(dateStr);
                activity_chemist_rep['modified_time']=addQuotes(mdateStr); 
                pushKey(activity_chemist_rep, {"Activity_Report_Code": "'Activity_Report_APP'"});
                resultData.push({'Activity_Chemist_Report': activity_chemist_rep});
                for (var i = 0, len = data['giftSelectedList'].length; i < len; i++)
                {
                    var tempOb = {};
                    tempOb['Gift_Code'] = addQuotes(data['giftSelectedList'][i]['gift']);
                    tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                    activity_input_reports.push(pushKey(tempOb, {"activity_chemist_code": "Activity_Chemist_Report"}));
                }
                resultData.push({'Activity_Chm_Sample_Report': activity_input_reports});
                break;
            case "3":
                var activity_stockist_Rep = {};
                activity_stockist_Rep['Stockist_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_stockist_Rep['worked_with'] = addQuotes(jointWorkString);
                activity_stockist_Rep['location'] = addQuotes(data['location']);
                activity_stockist_Rep['stockist_code'] = addQuotes(data['stockist']['selected']['id']);
                activity_stockist_Rep['Stk_Meet_Time'] = addQuotes(dateStr);
                activity_stockist_Rep['modified_time']=addQuotes(mdateStr);
                pushKey(activity_stockist_Rep, {"Activity_Report_Code": "'Activity_Report_APP'"});
                resultData.push({'Activity_Stockist_Report': activity_stockist_Rep});
                break;
            case "4":
                var activity_ulDoctor_Rep = {};
                activity_ulDoctor_Rep['UnListed_Doctor_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_ulDoctor_Rep['Worked_With'] = addQuotes(jointWorkString);
                activity_ulDoctor_Rep['location'] = addQuotes(data['location']);
                activity_ulDoctor_Rep['UnListed_Doc_Meet_Time'] = addQuotes(dateStr);
                activity_ulDoctor_Rep['modified_time']=addQuotes(mdateStr);
                activity_ulDoctor_Rep['uldoctor_code'] = addQuotes(data['uldoctor']['selected']['id']);
                pushKey(activity_ulDoctor_Rep, {"Activity_Report_Code": "'Activity_Report_APP'"});
                resultData.push({'Activity_UnListedDoctor_Report': activity_ulDoctor_Rep});
                for (var i = 0, len = data['productSelectedList'].length; i < len; i++)
                {
                    var tempOb = {};
                    tempOb['product_code'] = addQuotes(data['productSelectedList'][i]['product']);
                    tempOb['product_rx_qty'] = data['productSelectedList'][i]['rx_qty'];
                    tempOb['product_sample_qty'] = data['productSelectedList'][i]['sample_qty'];
                    activity_sample_reports.push(pushKey(tempOb, {"activity_msl_code": "Activity_UnListedDoctor_Report"}));
                }
                resultData.push({'Activity_Unlistedsample_Report': activity_sample_reports});
                break;
            default :

                break;
        }
        $http.defaults.useXDomain = true;
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + JSON.stringify(resultData),
            headers: {'Content-Type': 'application/x-www-form-urlencoded'}
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
    fmcgLocalStorage.addData = function(key, value)
    {
        var predata = window.localStorage.getItem(key) || "[]";
        predata = JSON.parse(predata);
        predata.push(value);
        window.localStorage.setItem(key, JSON.stringify(predata));
        return true;
    }
    fmcgLocalStorage.createData = function(key, value)
    {

        window.localStorage.setItem(key, JSON.stringify(value));
        return true;
    }

    fmcgLocalStorage.getData = function(key) {

        return JSON.parse(window.localStorage.getItem(key));
    }
    fmcgLocalStorage.getItemCount = function(key) {
        var data = window.localStorage.getItem(key);
        if (data == null || data == undefined)
            return 0;
        var count = 0
        count = JSON.parse(data).length;
        return count;
    }
    fmcgLocalStorage.getEntryCount = function() {
        var data = window.localStorage.getItem("draft");
        var temp = JSON.parse(data);
        var result = {};
        var obj = {'doctor_count': 0,'chemist_count': 0, 'stockist_count': 0, 'uldoctor_count': 0};
        if (temp != null && temp.length > 0)
        {
            if ((temp[0]['worktype']['selected']['id']).toString() === "field work")
            {
                for (var i = 0, len = temp.length; i < len; i++)
                {
                    if(temp[i]['customer'])
                    switch (temp[i]['customer']['selected']['id'])
                    {
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
            }
            else
            {
                result['success'] = false;
                result['data'] = {};
                result['data']['daywise_remarks'] = temp[0]['remarks'];
                result['data']['worktype_code'] = temp[0]['worktype']['selected']['id'];
            }
        }
        else
        {
            result['ndata'] = true;
        }

        return result;
    }
    return fmcgLocalStorage;
});




