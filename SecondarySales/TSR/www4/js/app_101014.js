var fmcg = angular.module('fmcg', ['ionic', 'fmcgServices', 'ngGrid']).config(function($stateProvider, $urlRouterProvider, $locationProvider) {
    $stateProvider
            .state('signin', {
                url: "/sign-in",
                templateUrl: "partials/sign-in.html",
                controller: 'SignInCtrl'
            })
            .state('fmcgmenu', {
                url: "/fmcg",
                abstract: true,
                templateUrl: "partials/sidemenu.html",
                controller: "MainCtrl"
            })
            .state('fmcgmenu.home', {
                url: "/home",
                views: {
                    'menuContent': {
                        templateUrl: "partials/home.html",
                        controller: "homeCtrl"
                    }
                }
            })
            .state('fmcgmenu.reloadMaster', {
                url: "/reload",
                views: {
                    'menuContent': {
                        templateUrl: "partials/reload.html",
                        controller: "reloadCtrl"
                    }
                }
            })
            .state('fmcgmenu.draft', {
                url: "/draft",
                views: {
                    'menuContent': {
                        templateUrl: "partials/draft.html",
                        controller: "draftCtrl"
                    }
                }
            }).state('fmcgmenu.addNew', {
        url: "/addNew",
        views: {
            'menuContent': {
                templateUrl: "partials/screen-1.html",
                controller: "screen1Ctrl"
            }
        }
    })
            .state('fmcgmenu.screen2', {
                url: "/screen2",
                views: {
                    'menuContent': {
                        templateUrl: "partials/screen-2.html",
                        controller: "screen2Ctrl"
                    }
                }
            })
            .state('fmcgmenu.draftView', {
                url: "/draftView?myChoice",
                views: {
                    'menuContent': {
                        templateUrl: "partials/draftView.html",
                        controller: "draftViewCtrl"
                    }
                }
            })
            .state('fmcgmenu.screen3', {
                url: "/screen3",
                views: {
                    'menuContent': {
                        templateUrl: "partials/screen-3.html",
                        controller: "screen3Ctrl"
                    }
                }
            })
            .state('fmcgmenu.screen4', {
                url: "/screen4",
                views: {
                    'menuContent': {
                        templateUrl: "partials/screen-4.html",
                        controller: "screen4Ctrl"
                    }
                }
            })
            .state('fmcgmenu.screen4s', {
                url: "/screen4s",
                views: {
                    'menuContent': {
                        templateUrl: "partials/screen-4s.html",
                        controller: "screen4sCtrl"
                    }
                }
            })
            .state('fmcgmenu.manageDataView', {
                url: "/manageDataView?myChoice",
                views: {
                    'menuContent': {
                        templateUrl: "partials/manageDataView.html",
                        controller: "manageDataViewCtrl"
                    }
                }
            })
            .state('fmcgmenu.manageDataTabView', {
                url: "/manageDataTabView?myChoice",
                views: {
                    'menuContent': {
                        templateUrl: "partials/manageDataTabView.html",
                        controller: "manageDataViewCtrl"
                    }
                }
            })
            .state('fmcgmenu.manageData', {
                url: "/manageData",
                views: {
                    'menuContent': {
                        templateUrl: "partials/manageData.html",
                        controller: "manageCtrl"
                    }
                }
            })
            .state('fmcgmenu.screen5', {
                url: "/screen5",
                views: {
                    'menuContent': {
                        templateUrl: "partials/screen-5.html",
                        controller: "screen5Ctrl"
                    }
                }
            })
            .state('fmcgmenu.dcr', {
                url: "/dcrdata",
                views: {
                    'menuContent': {
                        templateUrl: "partials/dcr.html",
                        controller: "dcrData"
                    }
                }
            })
            .state('fmcgmenu.dcr1', {
                url: "/dcrdata1",
                views: {
                    'menuContent': {
                        templateUrl: "partials/dcrListType.html",
                        controller: "dcrData1"
                    }
                }
            })
            .state('fmcgmenu.addULDoctor', {
                url: "/adduldoctor",
                views: {
                    'menuContent': {
                        templateUrl: "partials/addData.html",
                        controller: "addULDocCtrl"
                    }
                }
            }).state('fmcgmenu.addChemist', {
        url: "/addchemist",
        views: {
            'menuContent': {
                templateUrl: "partials/addData.html",
                controller: "addChemistCtrl"
            }
        }
    });
    $urlRouterProvider.otherwise("/sign-in");
}).run(function($rootScope, $state, $ionicLoading, $ionicPopup) {
//    $rootScope.isViewLoading = false;
    $rootScope.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams) {
        var allow = ['/addNew', '/screen1', '/screen2', '/screen3', '/screen4', '/screen5']
        if ($rootScope.hasData)
        {
            if (allow.indexOf(toState.url) == -1)
            {

                $rootScope.deleteRecord = true;
                $rootScope.hasData = false;
                $state.go(toState.name);

            }

        }

    });
    $rootScope
            .$on('$stateChangeSuccess',
                    function(event, toState, toParams, fromState, fromParams) {
//                        $rootScope.isViewLoading = false;
                        $ionicLoading.hide();
                    });
}).controller('SignInCtrl', ['$rootScope', '$scope', '$state', 'fmcgAPIservice', 'fmcgLocalStorage','notification', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage,notification) {
        $scope.callback = function() {
        };
        $scope.process = false;
        
        $scope.login = true;
        $scope.error = "";
        var flag = false, temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;


        if (userData && userData['user'] && userData['user']['valid'])
        {
            $scope.user = {};
            $scope.user['name'] = userData['user']['name'];
            $scope.user['valid'] = userData['user']['valid'];
        }
        $scope.signIn = function(user) {
            var loginInfo = {};
            $scope.process = true;
            $scope.login = false;
            $scope.error = "";

            if (userData != null)
            {
                var date2 = new Date();
                var date1 = new Date();
                date1.setTime(userData.lastLogin);
                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                if (diffDays < parseInt(userData.activeDays) && user.name === userData.user.name && user.password === userData.user.password)
                {
                    flag = true;
                    $state.go('fmcgmenu.home');
                }
                else
                {
                    flag = false;
                }

            }
            if (!flag)
            {

                //window.localStorage.setItem()
                fmcgAPIservice.getPostData('POST', 'login&access=mobile', user).success(function(response) {
                    if (response.success)
                    {

                        var loginData = {};
                        var dat = new Date();
                        loginData.lastLogin = dat.getTime();
                        loginData.activeDays = response.days;
                        loginData.sfCode = response.sfCode;
                        loginData.divisionCode = response.divisionCode;
                        loginData.callReport = response.call_report;
                        user['valid'] = true;
                        loginData.user = user;
                        window.localStorage.setItem("loginInfo", JSON.stringify(loginData));
                        $state.go('fmcgmenu.home');
                    }
                    else
                    {
                        $scope.process = false;
                        $scope.login = true;
                        $scope.error = response.msg;
                    }
                });
            }
        }
        ;
    }
]).controller('MainCtrl', ['$rootScope', '$scope', '$state', '$ionicModal', '$ionicScrollDelegate', '$ionicPopup', '$location', 'fmcgLocalStorage', 'fmcgAPIservice', '$ionicSideMenuDelegate', 'geolocation','$interval','notification', function($rootScope, $scope, $state, $ionicModal, $ionicScrollDelegate, $ionicPopup, $location, fmcgLocalStorage, fmcgAPIservice, $ionicSideMenuDelegate, geolocation,$interval,notification) {
        $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
         var  temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var lastMsgShown=window.localStorage.getItem("msg_shown")||undefined;
        $scope.showFlash=function(){
            if(userData.callReport&&userData.callReport.length>0)
                   $ionicPopup.show({
                title: 'Notification',
                content: userData.callReport,
                scope: $scope,
                buttons: [
                    {text: 'Close', type: 'button-assertive', onTap: function(e) {
                            return true;
                        }},
                ]
            }).then(function(res) {
                
            }, function(err) {
             
            }, function(popup) {
                // If you need to access the popup directly, do it in the notify method
                // This is also where you can programatically close the popup:
                // popup.close();
            });
        }
        if(!lastMsgShown)
        {
             $scope.showFlash();
            var obj={};
            var d=new Date();
            obj.date=d.getTime();
            window.localStorage.setItem("msg_shown",JSON.stringify(obj));
            
        }else{
            var data=JSON.parse(lastMsgShown);
                var date2 = new Date();
                var date1 = new Date();
                date1.setTime(data.date);
                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                if (diffDays > parseInt(1))
                {
             $scope.showFlash();
                }
        }
        
        
        $scope.clusters = fmcgLocalStorage.getData("town_master") || [];
        $scope.onLine = isReachable();
        $scope.cComputer = isComputer();  
         $scope.vibrate=function(){
             if(!$scope.cComputer)
                    notification.vibrate("20");
        }
        $interval(function(){
             $scope.onLine = isReachable();
                 $scope.cComputer = isComputer();
                 if ($scope.cComputer)
        {
            navigator.geolocation.getCurrentPosition(function(position) {
                $scope.fmcgData.location = position.coords.latitude + ":" + position.coords.longitude;
             
                $scope.fmcgData.hasGps = isGpsEnabled();
            });
        }
        else
        {
            $scope.fmcgData.location = geolocation.getCurrentPosition(function(position) {
                $scope.fmcgData.location = position.coords.latitude + ":" + position.coords.longitude;                  
                $scope.fmcgData.hasGps = isGpsEnabled();
            }, function(error) {
                $scope.fmcgData.hasGps = isGpsEnabled(true);             
            }, {maximumAge: 3000, timeout: 5000, enableHighAccuracy: true});
        }
        },100);
        $scope.currentSelection = {};
        $scope.selectionType = 0;

        $ionicModal.fromTemplateUrl('partials/selectModal.html', {
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function(modal) {
            $scope.modal = modal;
        });
        $scope.openModal = function(data, choice) {

            $scope.selectionType = choice;
            $scope.modal.searchText = '';
            var res = [];
            if (choice == 6||choice == 5)
            {
                
//                data=data.sort(function(a, b) {       
//                   if (a.town_code === $scope.fmcgData.cluster.selected.id)
//                    {           
//                        a.town_name=$scope.fmcgData.cluster.name;
//                        return 1;
//                    }
//                    if (b.town_code === $scope.fmcgData.cluster.selected.id)
//                    {                      
//                        return -1;
//                    }
//                    // a must be equal to b
//                    return 0;
//                });      
//                data=data.reverse();
                //console.log(data);
                $scope.currentSelection = data;
            }
            else
            {
                $scope.currentSelection = data;
            }

            $scope.currentSelection = data;
            $scope.modal.show();
            $ionicScrollDelegate.scrollTop();
        };
        $scope.closeModal = function() {
            $scope.modal.hide();
        };
        $scope.selectData = function(item) {
            var key = ['worktype', 'cluster', 'customer', 'stockist', 'chemist', 'doctor', 'uldoctor', 'jointwork'];
            switch ($scope.selectionType)
            {
                case 1:
                default:
                    $scope.fmcgData[key[$scope.selectionType - 1]] = {};
                    $scope.fmcgData[key[$scope.selectionType - 1]].name = item.name;
                    $scope.fmcgData[key[$scope.selectionType - 1]].selected = {};
                    $scope.fmcgData[key[$scope.selectionType - 1]].selected.id = item.id;
                    var temp = [4, 5, 6, 7];

                    if (temp.indexOf($scope.selectionType) !== -1 && $scope.fmcgData.source === 0)
                    {
                        $scope.fmcgData.cluster = {};
                        $scope.fmcgData.cluster.selected = {};
                        $scope.fmcgData.cluster.selected.id = item.town_code;
                    }

                    break;
                case 8:
                    var jontWorkData = {};
                    jontWorkData.jointwork = item.id;
                    $scope.fmcgData.jontWorkSelectedList = $scope.fmcgData.jontWorkSelectedList || [];
                    var len = $scope.fmcgData.jontWorkSelectedList.length;
                    var flag = true;
                    for (var i = 0; i < len; i++)
                    {
                        if ($scope.fmcgData.jontWorkSelectedList[i]['jointwork'] === item.id)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                        $scope.fmcgData.jontWorkSelectedList.push(jontWorkData);
                    else
                        Toast("JointWork Already Added", true);
                    break;
                case 9:
                    var productData = {};
                    $scope.fmcgData.productSelectedList = $scope.fmcgData.productSelectedList || [];
                    productData.product = item.id;
                    productData.rx_qty = 0;
                    productData.sample_qty = 0;
                    var len = $scope.fmcgData.productSelectedList.length;
                    var flag = true;
                    for (var i = 0; i < len; i++)
                    {
                        if ($scope.fmcgData.productSelectedList[i]['product'] === item.id)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                        $scope.fmcgData.productSelectedList.push(productData);
                    else
                        Toast("Product Already Added", true);
                    break;
                case 10:
                    var giftData = {};
                    $scope.fmcgData.giftSelectedList = $scope.fmcgData.giftSelectedList || [];
                    giftData.gift = item.id;
                    giftData.sample_qty = 1;
                    var len = $scope.fmcgData.giftSelectedList.length;
                    var flag = true;
                    for (var i = 0; i < len; i++)
                    {
                        if ($scope.fmcgData.giftSelectedList[i]['gift'] === item.id)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                        $scope.fmcgData.giftSelectedList.push(giftData);
                    else
                        Toast("Input Already Added", true);
                    break;
                case 12:
                    $scope.fmcgData.rx_t = item.id;
                    break;
                case 13:
                    $scope.fmcgData.nrx_t = item.id;
                case 15:
                    $scope.fmcgData.remarks = item.name;
            }
            $scope.modal.hide();
        };
        //Cleanup the modal when we're done with it!
        $scope.$on('$destroy', function() {
            $scope.modal.remove();
        });
        $scope.customers = [{
                'id': '1',
                'name': 'Listed Dr',
                'url': 'manageDoctorResult'
            }, {
                'id': '2',
                'name': 'Chemist',
                'url': 'manageChemistResult'
            }, {
                'id': '3',
                'name': 'Stockist',
                'url': 'manageStockistResult'
            }, {
                'id': '4',
                'name': 'Unlisted Dr',
                'url': 'manageStockistResult'
            }];
        if ($scope.clusters.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'table/list', ["town_master",
                '["town_code as id", "town_name as name"]'
                        , ,"isnull(Town_Activation_Flag,'0')='0'", , , , , , ]).success(function(response) {
                $scope.clusters = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("town_master", response);
            });
        }

        if ($scope.worktypes.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
                '["type_code as id", "Wtype as name"]'
                        , ,"isnull(Active_flag,'0')='0'", , , , 0]).success(function(response) {
                $scope.worktypes = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("mas_worktype", response);
            });
        }
        $scope.products = fmcgLocalStorage.getData("product_master") || [];
        if ($scope.products.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'table/list', ["product_master",
                '["product_code as id", "product_name as name"]'
                        , ,"isnull(Product_DeActivation_Flag,'0')='0'", , , , 0]).success(function(response) {
                $scope.products = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("product_master", response);
            });
        }
        $scope.gifts = fmcgLocalStorage.getData("gift_master") || [];
        if ($scope.gifts.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'table/list', ["gift_master",
                '["gift_code as id", "gift_name as name"]'
                        , ,"isnull(Gift_DeActivate_Flag,'0')='0'", , , , 0]).success(function(response) {
                $scope.gifts = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("gift_master", response);
            });
        }
        $scope.jointworks = fmcgLocalStorage.getData("salesforce_master") || [];
        if ($scope.jointworks.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'get/jointwork', ["salesforce_master",
                '["sf_code as id", "sf_name as name"]'
            ]).success(function(response) {
                $scope.jointworks = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("salesforce_master", response);
            });
        }
        $scope.fmcgData = {
        };
        $scope.go = function(path) {
            $location.path(path);
        };
        $scope.showCounter = 0;
        $scope.draftCount = fmcgLocalStorage.getItemCount("draft");
        $scope.fmcgData.productSelectedList = [];
        $scope.fmcgData.giftSelectedList = [];
        $scope.fmcgData.jontWorkSelectedList = [];
        $scope.fmcgData.worktype = {};       
        $scope.fmcgData.worktype.selected = {};
        $scope.fmcgData.worktype.selected.id = "field work";
        $scope.fmcgData.source = 1;
        $rootScope.hasData = false;
        $scope.logout = function() {
            window.localStorage.clear();
            $scope.toggleLeft();
            $state.go("signin");
        }
        $scope.toggleLeft = function() {
            //  notification.vibrate(15);
            $ionicSideMenuDelegate.toggleLeft();
        };
        if ($scope.onLine)
        {
            var temp = [];
            var draftData = fmcgLocalStorage.getData("saveLater") || [];
            while (draftData.length != 0)
            {
                var data = draftData.shift();
                fmcgAPIservice.saveDCRData('POST', 'dcr/save', data, false).success(function(response) {
                    if (!response['success'])
                    {
                        Toast(response['msg'], true);

                    }
                    else
                    {
                        fmcgLocalStorage.createData("saveLater", draftData);
                    }
                });

            }
        }
        $scope.fmcgDataCopy = angular.copy($scope.fmcgData);
        $scope.clearData = function() {
            if ($scope.fmcgData)
            {
                if ($rootScope.deleteRecord)
                {
                    if ($rootScope.saveToDraft)
                    {
                        fmcgLocalStorage.addData('draft', $scope.fmcgData);

                    }
                    var value = $scope.fmcgData;
                    for (key in value)
                    {
                        if (key)
                        {
                            $scope.fmcgData[key] = undefined;

                        }
                    }
                    $rootScope.hasData = false;
                    $rootScope.hasEditData = false;
                    $rootScope.deleteRecord = false;
                    $rootScope.saveToDraft = false;

                }
            }

        };
        $scope.saveToDraftO = function()
        {
            var temp = fmcgLocalStorage.getData("draft");
            if (temp === null || temp.length == 0 || ($scope.fmcgData.worktype.selected.id).toString() === (temp[0]['worktype']['selected']['id']).toString())
            {
                $scope.fmcgData.isDraft = true;
                fmcgLocalStorage.addData('draft', $scope.fmcgData);
                var value = $scope.fmcgData;
                for (key in value)
                {
                    if (key)
                    {
                        $scope.fmcgData[key] = undefined;
                    }
                }
                $state.go('fmcgmenu.home');

                Toast("Call saved to draft");
            }
            else
            {
                $ionicPopup.confirm(
                        {
                            title: 'Call Conflict',
                            content: 'You have call for other worktype in draft do you want to replace...?'
                        }).then(function(res) {
                    if (res) {
                        window.localStorage.removeItem("draft");
                        fmcgLocalStorage.addData('draft', $scope.fmcgData);
                        var value = $scope.$parent.fmcgData;
                        for (key in value)
                        {
                            if (key)
                            {
                                $scope.$parent.fmcgData[key] = undefined;
                            }
                        }
                        $state.go('fmcgmenu.home');
                        Toast("Data added to draft Successfully");
                    } else {
                        console.log('You are not sure');
                    }
                }
                );
            }
        };
        $scope.setAllow = function() {
            $rootScope.hasData = true;
            if ($scope.fmcgData.amc || $scope.fmcgData.arc)
            {
                $rootScope.hasEditData = true;
            }
        }
    }]).controller('addULDocCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate','notification', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate,notification) {
        $scope.$parent.navTitle = "Add Unlisted Doctor";
        $scope.clusters = fmcgLocalStorage.getData("town_master") || [];
        $scope.clearData();
        $scope.data = {};
        if ($scope.clusters.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'table/list', ["town_master",
                '["town_code as id", "town_name as name"]',,"isnull(Town_Activation_Flag,'0')='0'"
            ]).success(function(response) {
                $scope.clusters = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("town_master", response);
            });
        }
        $scope.save = function() {
            $scope.data.cluster = {};
            $scope.data.cluster.selected = {};
            $scope.data.cluster.selected.id = $scope.fmcgData.cluster.selected.id;
            fmcgAPIservice.addMAData('POST', 'dcr/save', "2", $scope.data).success(function(response) {
                if (response.success)
                    Toast("Unlisted Doctor Added Successfully");
                $scope.data = {};
                window.localStorage.removeItem("unlisted_doctor_master")
                $scope.data = {};
            });
            $state.go('fmcgmenu.home');
        };
    }]).controller('addChemistCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate','notification', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate,notification) {
        $scope.$parent.navTitle = "Add Chemist";
        $scope.clearData();
        $scope.clusters = fmcgLocalStorage.getData("town_master") || [];

        $scope.data = {};
        if ($scope.clusters.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'table/list', ["town_master",
                '["town_code as id", "town_name as name"]',,"isnull(Town_Activation_Flag,'0')='0'"
            ]).success(function(response) {

                $scope.clusters = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("town_master", response);
            });
        }
        $scope.save = function() {
            $scope.data.cluster = {};
            $scope.data.cluster.selected = {};
            $scope.data.cluster.selected.id = $scope.fmcgData.cluster.selected.id;
            fmcgAPIservice.addMAData('POST', 'dcr/save', "1", $scope.data).success(function(response) {
                if (response.success)
                    Toast("Chemist Added Successfully");
                //write as service
                window.localStorage.removeItem("chemist_master")
                $scope.data = {};
            });
            $state.go('fmcgmenu.home');
        };
    }]).controller('homeCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', 'fmcgAPIservice','notification', function($rootScope, $scope, $state, $ionicPopup, fmcgAPIservice,notification) {

        $scope.customers = [{
                'id': '1',
                'name': 'Listed Dr',
                'url': 'manageDoctorResult'
            }, {
                'id': '2',
                'name': 'Chemist',
                'url': 'manageChemistResult'
            }, {
                'id': '3',
                'name': 'Stockist',
                'url': 'manageStockistResult'
            }, {
                'id': '4',
                'name': 'Unlisted Dr',
                'url': 'manageStockistResult'
            }];
        $scope.clearData();
        //network.isReachable("fmcg1.azurewebsites.net");
        $scope.$parent.navTitle = "";
        if ($scope.$parent.showCounter == 0)
        {
//            var temp = window.localStorage.getItem("loginInfo");
//            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
//            $ionicPopup.show({
//                title: 'Notification',
//                content: userData.callReport,
//                scope: $scope,
//                buttons: [
//                    {text: 'Close', type: 'button-assertive', onTap: function(e) {
//                            return true;
//                        }},
//                ]
//            }).then(function(res) {
//                console.log('Tapped!', res);
//            }, function(err) {
//                console.log('Err:', err);
//            }, function(popup) {
//                // If you need to access the popup directly, do it in the notify method
//                // This is also where you can programatically close the popup:
//                // popup.close();
//            });
        }
        $scope.$parent.showCounter++;
        $scope.goToCustomer = function(cus) {
            $scope.$parent.fmcgData.customer = {};
            $scope.$parent.fmcgData.worktype = {};
            $scope.$parent.fmcgData.worktype.selected = {"id": "field work", "name": "Field Work"}
            switch (cus)
            {
                case 1:
                    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
                    break;
                case 2:
                    $scope.$parent.fmcgData.customer.selected = $scope.customers[1];
                    break;
                case 3:
                    $scope.$parent.fmcgData.customer.selected = $scope.customers[2];
                    break;
                case 4:
                    $scope.$parent.fmcgData.customer.selected = $scope.customers[3];
                    break;
            }
            $scope.$parent.fmcgData.source = 0;

            $state.go('fmcgmenu.screen2');
        };
    }]).controller('manageCtrl', ['$rootScope', '$scope', '$state', '$ionicLoading', 'fmcgAPIservice', 'fmcgLocalStorage','notification', function($rootScope, $scope, $state, $ionicLoading, fmcgAPIservice, fmcgLocalStorage,notification) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.success = false;
        $scope.owsuccess = false;
        $scope.owTypeData = [];
        $scope.$parent.navTitle = "Submitted Calls";
        $scope.customers = [{
                'id': '1',
                'name': 'Listed Dr',
                'url': 'manageDoctorResult'
            }, {
                'id': '2',
                'name': 'Chemist',
                'url': 'manageChemistResult'
            }, {
                'id': '3',
                'name': 'Stockist',
                'url': 'manageStockistResult'
            }, {
                'id': '4',
                'name': 'Unlisted Dr',
                'url': 'manageStockistResult'
            }];
        $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
        if ($scope.worktypes.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
                '["type_code as id", "wtype as name"]'
                        , ,"isnull(Active_flag,'0')='0'", , , , 0]).success(function(response) {
                $scope.worktypes = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("mas_worktype", response);
            });
        }
        fmcgAPIservice.getDataList('POST', 'entry/count', []).success(function(response) {
            if (response['success'])
            {
                $scope.success = true;
                $scope.customers[0].count = response['data'][0]['doctor_count'];
                $scope.customers[1].count = response['data'][1]['chemist_count'];
                $scope.customers[2].count = response['data'][2]['stockist_count'];
                $scope.customers[3].count = response['data'][3]['uldoctor_count'];
            }
            else
            {
                $scope.owsuccess = true;
                $scope.owTypeData = response['data'][0];
            }
            $ionicLoading.hide();
        });
    }]).controller('screen1Ctrl', function($rootScope, $scope, $ionicPopup, $stateParams, $state, fmcgAPIservice, fmcgLocalStorage, generateUID,notification) {
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.addNew";
    $scope.$parent.navTitle = "";
    if (!$scope.$parent.fmcgData.worktype)
    {
        $scope.$parent.fmcgData.worktype = {};
        $scope.$parent.fmcgData.worktype.selected = {};
        $scope.$parent.fmcgData.worktype.selected.id = "field work";
    }
    $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
    $scope.clusters = fmcgLocalStorage.getData("town_master") || [];
    if ($scope.clusters.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["town_master",
            '["town_code as id", "town_name as name"]',,"isnull(Town_Activation_Flag,'0')='0'"
        ]).success(function(response) {
            $scope.clusters = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("town_master", response);
        });
    }
    if ($scope.worktypes.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
            '["type_code as id", "wtype as name"]'
                    , ,"isnull(Active_flag,'0')='0'", , , , 0]).success(function(response) {
            $scope.worktypes = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("mas_worktype", response);
        });
    }
    var id = $stateParams.customerId;
    if (id != null && id.length)
    {
        $scope.worktype = {};
        $scope.worktype.selected = $filter('getValueforID')("field work", $scope.worktypes);
    }
    $scope.goBack = function() {
        if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor)
        {
            $ionicPopup.confirm(
                    {
                        title: 'Confirm Navigation',
                        content: 'You have unsaved record do you want to save it in draft...?'
                    }).then(function(res) {
                if (res) {
                    fmcgLocalStorage.addData('draft', $scope.fmcgData);
                    var value = $scope.$parent.fmcgData;
                    for (key in value)
                    {
                        if (key)
                        {
                            $scope.$parent.fmcgData[key] = undefined;

                        }
                    }
                    $state.go('fmcgmenu.home');

                } else {
                    $state.go('fmcgmenu.home');
                    var value = $scope.$parent.fmcgData;
                    for (key in value)
                    {
                        if (key)
                        {
                            $scope.$parent.fmcgData[key] = undefined;

                        }
                    }
                }
            }
            );
        }
        else
        {
            $state.go('fmcgmenu.home');
            var value = $scope.$parent.fmcgData;
            for (key in value)
            {
                if (key)
                {
                    $scope.$parent.fmcgData[key] = undefined;

                }
            }
        }

    };
    $scope.goNext = function() {
        if ($scope.$parent.fmcgData.worktype.selected.id == "field work")
        {
            var proceed = true;
            var msg = "";
            if (!$scope.fmcgData.cluster)
            {
                msg = "Please Select Cluster       ";
                proceed = false;
            }
            if (!$scope.fmcgData.customer)
            {
                msg = msg + "Please Select Customer Type         "
                proceed = false;
            }
            if (proceed)
            {
                $scope.$parent.fmcgData.source = 1;
                $state.go('fmcgmenu.screen2');
            }
            else
            {
                Toast(msg, true);
            }
        }
        else
            $state.go('fmcgmenu.screen5');
    };
}).controller('screen4Ctrl', function($rootScope, $scope, $state, $filter, fmcgAPIservice, fmcgLocalStorage,notification) {
    $scope.gifts = fmcgLocalStorage.getData("gift_master") || [];
    if ($scope.gifts.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["gift_master", '["gift_code as id", "gift_name as name"]', ,"isnull(Gift_DeActivate_Flag,'0')='0'", , , , 0]).success(function(response) {
            $scope.gifts = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("gift_master", response);
        });
    }
    $scope.setAllow();
    if ($scope.$parent.fmcgData.customer) {
        $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen4";
    $scope.$parent.fmcgData.giftSelectedList = $scope.$parent.fmcgData.giftSelectedList || [];
    $scope.addProduct = function(selected) {
        var giftData = {};
        giftData.gift = selected;
        giftData.sample_qty = 1;
        $scope.$parent.fmcgData.giftSelectedList.push(giftData);
        // $scope.updateLayout();
    }
//    var layoutPlugin = new ngGridLayoutPlugin();
//    $scope.updateLayout = function() {
//        layoutPlugin.updateGridLayout();
//    };
    $scope.gridOptions = {
        data: 'fmcgData.giftSelectedList',
        rowHeight: 50,
        enableRowSelection: false,
        rowTemplate: 'rowTemplate.html',
        enableCellSelection: true,
        enableColumnResize: true,
        plugins: [new ngGridFlexibleHeightPlugin()],
        showFooter: true,
        columnDefs: [{field: 'gift', displayName: 'Input Name', enableCellEdit: false, cellTemplate: 'partials/giftCellTemplate.html'},
            {field: 'sample_qty', displayName: 'Input Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate1.html", width: 90},
            {field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50}]
    };
    $scope.removeRow = function() {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.giftSelectedList.splice(index, 1);
    };
    $scope.goBack = function() {
        if ($scope.fmcgData.customer.selected.id == 2)
            $state.go('fmcgmenu.screen2');
        else
            $state.go('fmcgmenu.screen3');
    };
    $scope.goNext = function() {
        $state.go('fmcgmenu.screen5');
    };
    $scope.save = function() {
        $scope.saveToDraftO();
    }

}).controller('screen3Ctrl', function($rootScope, $scope, $filter, $state, fmcgAPIservice, fmcgLocalStorage,notification) {
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen3";
    if ($scope.$parent.fmcgData.customer) {
        $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.setAllow();
    $scope.products = fmcgLocalStorage.getData("product_master") || [];
    if ($scope.products.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["product_master",
            '["product_code as id", "product_name as name"]'
                    , ,"isnull(Product_DeActivation_Flag,'0')='0'", , , , 0]).success(function(response) {
            $scope.products = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("product_master", response);
        });
    }

    $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
    $scope.addProduct = function(selected) {
        var productData = {};
        productData.product = selected;
        productData.rx_qty = 0;
        productData.sample_qty = 0;
        var len = $scope.$parent.fmcgData.productSelectedList.length;
        var flag = true;
        for (var i = 0; i < len; i++)
        {
            if ($scope.$parent.fmcgData.productSelectedList[i]['product'] === selected)
            {
                flag = false;
            }
        }
        if (flag)
            $scope.$parent.fmcgData.productSelectedList.push(productData);
        // $scope.updateLayout();
    }
//    var layoutPlugin = new ngGridLayoutPlugin();
//    $scope.updateLayout = function() {
//        layoutPlugin.updateGridLayout();
//    };
    $scope.gridOptions = {
        data: 'fmcgData.productSelectedList',
        rowHeight: 50,
        rowTemplate: 'rowTemplate.html',
        enableCellSelection: true,
        enableColumnResize: true,
        enableRowSelection: false,
        plugins: [new ngGridFlexibleHeightPlugin()],
        showFooter: true,
        columnDefs: [{field: 'product', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html'},
            {field: 'rx_qty', displayName: 'Rx Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60},
            {field: 'sample_qty', displayName: 'Sample Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate1.html", width: 90},
            {field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50}]
    };
    $scope.removeRow = function() {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.productSelectedList.splice(index, 1)
    };
    $scope.save = function() {
        $scope.saveToDraftO();
        $state.go('fmcgmenu.home');
    }
    $scope.goBack = function() {

        $state.go('fmcgmenu.screen2');
    };
    $scope.goNext = function() {
        if ($scope.fmcgData.customer.selected.id == 4)
            $state.go('fmcgmenu.screen5');
        else
            $state.go('fmcgmenu.screen4');
    };
}).controller('screen2Ctrl', function($rootScope, $scope, $state, $timeout, $filter, $ionicModal, fmcgAPIservice, fmcgLocalStorage,notification) {
    $scope.modal = $ionicModal;
    var tDate = new Date();
    if (!$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.arc && !$scope.isDraft)
    {
        $scope.$parent.fmcgData.entryDate = tDate.setMonth(tDate.getMonth() + 1);
        $scope.$parent.fmcgData.modifiedDate = tDate;
    }
    else
    {
        $scope.$parent.fmcgData.modifiedDate = tDate.setMonth(tDate.getMonth() + 1);
	    var tDate = new Date($scope.$parent.fmcgData.entryDate);
		$scope.$parent.fmcgData.entryDate= tDate.setMonth(tDate.getMonth() + 1);

    }

    if ($scope.$parent.fmcgData.customer) {
        $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.setAllow();
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen2";
    $scope.modal.fromTemplateUrl('partials/summaryModal.html', function(modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });
    $scope.showModal = function() {
        $scope.modal.show();
        $scope.modal.sm_msg;
        $scope.modal.summaryData;
        $scope.modal.customerLs;
        if ($scope.onLine)
        {
            if ($scope.fmcgData.customer)
            {
                switch ($scope.fmcgData.customer.selected.id)
                {
                    case "1":
                        $scope.modal.customerLs = fmcgLocalStorage.getData("doctor_master") || [];

                        if ($scope.modal.customerLs.length == 0)
                        {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["doctor_master",
                                '["doctor_code as id", "doctor_name as name","town_code"]',,"isnull(Doctor_Active_flag,'0')='0'"
                            ]).success(function(response) {
                                $scope.modal.customerLs = response;
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("doctor_master", response);
                            });
                        }/* May be change */
                        /*$scope.modal.doctorSpec = fmcgLocalStorage.getData("speciality_master") || [];
                        if ($scope.modal.doctorSpec.length == 0)
                        {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["speciality_master", '["*"]']).success(function(response) {
                                $scope.modal.doctorSpec = response;
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("speciality_master", response);
                            });
                        }*/
                        fmcgAPIservice.getDataList('POST', 'get/doctorCount', []).success(function(response) {
                            $scope.modal.summaryData = response;
                        });
                        break;
                    case "2":
                        $scope.modal.customerLs = fmcgLocalStorage.getData("chemist_master") || [];
                        fmcgLocalStorage.getData("chemist_master") || [];
                        if ($scope.modal.customerLs.length == 0)
                        {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["chemists_master",
                                '["chemists_code as id", "chemists_name as name","town_code"]',,"isnull(Chemist_Status,'0')='0'"
                            ]).success(function(response) {
                                $scope.modal.customerLs = response;
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("chemist_master", response);
                            });
                        }
                        fmcgAPIservice.getDataList('POST', 'get/chemistCount', []).success(function(response) {
                            $scope.modal.summaryData = response;
                        });
                        break;
                    case "3":
                        $scope.modal.customerLs = fmcgLocalStorage.getData("stockist_master") || [];
                        if ($scope.modal.customerLs.length == 0)
                        {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["stockiest_master",
                                '["stockiest_code as id", "stockiest_name as name","town_code"]',,"isnull(Stockist_Status,'0')='0'"
                            ]).success(function(response) {
                                $scope.modal.customerLs = response;
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("stockist_master", response);
                            });
                        }
                        fmcgAPIservice.getDataList('POST', 'get/stockistCount', []).success(function(response) {
                            $scope.modal.summaryData = response;
                        });
                        break;
                    case "4":
                        $scope.modal.customerLs = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
                        if ($scope.modal.customerLs.length == 0)
                        {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["unlisted_doctor_master",
                                '["unlisted_doctor_code as id", "unlisted_doctor_name as name","town_code"]',,"isnull(unlisted_activation_flag,'0')='0'"
                            ]).success(function(response) {
                                $scope.modal.customerLs = response;
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("unlisted_doctor_master", response);
                            });
                        }
                        fmcgAPIservice.getDataList('POST', 'get/uldoctorCount', []).success(function(response) {
                            $scope.modal.summaryData = response;
                        });
                        break;
                }
            }
            else
            {
                $scope.modal.sm_msg = "Please Select One Customer";
            }
        }
        else
        {
            $scope.modal.sm_msg = "No Internet Connection";
        }

    }
    $scope.customers = [{
            'id': '1',
            'name': 'Listed Dr'
        }, {
            'id': '2',
            'name': 'Chemist'
        }, {
            'id': '3',
            'name': 'Stockist'
        }, {
            'id': '4',
            'name': 'Unlisted Dr'
        }];
    $scope.stockists = fmcgLocalStorage.getData("stockist_master") || [];
    $scope.chemists = fmcgLocalStorage.getData("chemist_master") || [];
    $scope.doctors = fmcgLocalStorage.getData("doctor_master") || [];
      $scope.clusters = fmcgLocalStorage.getData("town_master") || [];
    $scope.uldoctors = fmcgLocalStorage.getData("unlisted_doctor_master") || [];

    if ($scope.doctors.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["doctor_master",
            '["doctor_code as id", "doctor_name as name","town_code"]',,"isnull(Doctor_Active_flag,'0')='0'"
        ]).success(function(response) {
            $scope.doctors = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("doctor_master", response);
        });
    }/*May bChange*/
    /*$scope.modal.doctorSpec = fmcgLocalStorage.getData("speciality_master") || [];
    if ($scope.modal.doctorSpec.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["speciality_master", '["*"]']).success(function(response) {
            $scope.modal.doctorSpec = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("speciality_master", response);
        });
    }*/
       if ($scope.clusters.length == 0)
        {
            fmcgAPIservice.getDataList('POST', 'table/list', ["town_master",
                '["town_code as id", "town_name as name"]'
                        , ,"isnull(Town_Activation_Flag,'0')='0'", , , , , , ]).success(function(response) {
                $scope.clusters = response;
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("town_master", response);
            });
        }           
    if ($scope.stockists.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["stockiest_master",
            '["stockiest_code as id", "stockiest_name as name","town_code"]',,"isnull(Stockist_Status,'0')='0'"
        ]).success(function(response) {
            $scope.stockists = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("stockist_master", response);
        });
    }

    if ($scope.chemists.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["chemists_master",
            '["chemists_code as id", "chemists_name as name","town_code"]',,"isnull(Chemist_Status,'0')='0'"
        ]).success(function(response) {
            $scope.chemists = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("chemist_master", response);
        });
    }
    $scope.sortData=function(data)
    {
          data.sort(function(a, b) {       
             
              a.town_name=$filter('getValueforID')(a.town_code, $scope.clusters).name;
              b.town_name=$filter('getValueforID')(b.town_code, $scope.clusters).name;
                   try{    
                   if(a.town_code === $scope.fmcgData.cluster.selected.id)
                    {           
                       
                        return 1;
                    }
                    if (b.town_code === $scope.fmcgData.cluster.selected.id)
                    {          
                        
                        return -1;
                    }}
                    catch (msg)
                    {
                       
                    }
                    // a must be equal to b
                    return 0;
                });      
                if($scope.fmcgData.cluster)
                data=data.reverse();
                return data;
    }
    if ($scope.uldoctors.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["unlisted_doctor_master",
            '["unlisted_doctor_code as id", "unlisted_doctor_name as name","town_code"]',,"isnull(unlisted_activation_flag,'0')='0'"
        ]).success(function(response) {
            $scope.uldoctors = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("unlisted_doctor_master", response);
        });
    }
    if ($scope.$parent.fmcgData.cluster && $scope.$parent.fmcgData.customer)
    {
      

                $timeout(function() {
                
                $scope.doctors=$scope.sortData($scope.doctors);
                }, 1000);
              
                $timeout(function() {
                     $scope.chemists=$scope.sortData($scope.chemists);
                    //$scope.chemists = $filter('searchF')($scope.$parent.fmcgData.cluster.selected.id, $scope.chemists);

                }, 1000);
//                break;
//            case "3":
//                $timeout(function() {
//                    $scope.stockists = $filter('searchF')($scope.$parent.fmcgData.cluster.selected.id, $scope.stockists);
//
//                }, 1000);
//                break;
//            case "4":
//                $timeout(function() {
//                    $scope.uldoctors = $filter('searchF')($scope.$parent.fmcgData.cluster.selected.id, $scope.uldoctors);
//                }, 1000);
//                break;
//        
    }else if($scope.$parent.fmcgData.customer){
         $timeout(function() {
                
                $scope.doctors=$scope.sortData($scope.doctors);
                }, 1000);
              
                $timeout(function() {
                     $scope.chemists=$scope.sortData($scope.chemists);
                    //$scope.chemists = $filter('searchF')($scope.$parent.fmcgData.cluster.selected.id, $scope.chemists);

                }, 1000);
    }
    $scope.jointworks = fmcgLocalStorage.getData("salesforce_master") || [];
    if ($scope.jointworks.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'get/jointwork', ["salesforce_master",
            '["sf_code as id", "sf_name as name"]'
        ]).success(function(response) {
            $scope.jointworks = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("salesforce_master", response);
        });
    }
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen4s";
    $scope.save = function() {
        if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor)
        {
            $scope.saveToDraftO();

        }
        else
        {
            Toast("You have to select in customer to save in draft", true);
        }
    };
    $scope.$parent.fmcgData.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList || [];
    $scope.addProduct = function(selected)
    {
        var jontWorkData = {};
        jontWorkData.jointwork = selected;
        $scope.$parent.fmcgData.jontWorkSelectedList.push(jontWorkData);
    }
    ;
    $scope.gridOptions = {
        data: 'fmcgData.jontWorkSelectedList',
        rowHeight: 50,
        enableRowSelection: false,
        rowTemplate: 'rowTemplate.html',
        enableCellSelection: true,
        enableColumnResize: true,
        plugins: [new ngGridFlexibleHeightPlugin()],
        showFooter: true,
        columnDefs: [{field: 'jointwork', displayName: 'Jointwork', enableCellEdit: false, cellTemplate: 'partials/jointworkCellTemplate.html'},
            {field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50}]
    };
    $scope.removeRow = function() {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
    };
    $scope.goNext = function() {
        var temp;
        var msg = "";
        var proceed = true;
        if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor)
        {
        }
        else
        {
            proceed = false;
            msg = "Please Select Customer</br>";
        }
        var pobT = $scope.fmcgData.pob ? parseInt($scope.fmcgData.pob) : 0;
        if (isNaN(pobT) || pobT > 999999)
        {
            $scope.fmcgData.pob = "";
            proceed = false;
            msg = msg + "Please Check the input pob";
        }
        if (proceed)
        {
            if ($scope.fmcgData.customer.selected.id == 1 || $scope.fmcgData.customer.selected.id == 4)
                $state.go('fmcgmenu.screen3');
            else if ($scope.fmcgData.customer.selected.id == 2)
                $state.go('fmcgmenu.screen4')
            else
                $state.go('fmcgmenu.screen5');
        }
        else
        {
            Toast(msg, true);
        }
    };
    $scope.goBack = function() {
        $state.go('fmcgmenu.addNew');
    };

}).controller('screen4sCtrl', function($scope, $state, fmcgAPIservice, fmcgLocalStorage,notification) {

}).controller('screen5Ctrl', function($rootScope, $scope, $state, $ionicPopup, $filter, geolocation, fmcgAPIservice, fmcgLocalStorage,notification) {

    if ($scope.$parent.fmcgData.customer) {
        $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.setAllow();
    $scope.reportTemplates = fmcgLocalStorage.getData("report_template_master") || [];
    if ($scope.reportTemplates.length == 0)
    {

        fmcgAPIservice.getDataList('POST', 'table/list', ["report_template_master",
            '["id as id", "content as name"]'
                    , , , , , , 0]).success(function(response) {
            $scope.reportTemplates = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("report_template_master", response);
        });
    }
    $scope.nonreportTemplates = fmcgLocalStorage.getData("nonreport_template_master") || [];
    if ($scope.nonreportTemplates.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["nonreport_template_master",
            '["id as id", "content as name"]'
                    , , , , , , 0]).success(function(response) {
            $scope.nonreportTemplates = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("nonreport_template_master", response);
        });
    }
    $scope.updateValue = function(value) {
        $scope.$parent.fmcgData.remarks = value.name;
    };
    if ($scope.cComputer)
    {
        navigator.geolocation.getCurrentPosition(function(position) {
            $scope.fmcgData.location = position.coords.latitude + ":" + position.coords.longitude;

        });
    }
    else
    {
        $scope.fmcgData.location = geolocation.getCurrentPosition(function(position) {
            $scope.fmcgData.location = position.coords.latitude + ":" + position.coords.longitude;
        }, function() {
        }, {maximumAge: 3000, timeout: 5000, enableHighAccuracy: true});
    }

//    $scope.fmcgData.location = getLocation(geolocation);    
    $scope.saveD = function() {

        $scope.fmcgData.toBeSync = false;
        $scope.saveToDraftO();
    };
    $scope.save = function() {
        $rootScope.hasData = false;
        $rootScope.hasEditData = false;
        if (!$scope.onLine)
        {

            $scope.fmcgData.toBeSync = true;
            fmcgLocalStorage.addData('saveLater', $scope.fmcgData);
            var value = $scope.$parent.fmcgData;
            for (key in value)
            {
                if (key)
                {
                    $scope.$parent.fmcgData[key] = undefined;
                }
            }
            $state.go('fmcgmenu.home');

            Toast("No Connection! Call Saved Locally", true);

        }
        else
        if ($scope.fmcgData.arc)
        {
            var arc = $scope.fmcgData.arc;
            var amc = $scope.fmcgData.amc;
            fmcgAPIservice.saveDCRData('POST', 'dcr/updateEntry&arc=' + arc + '&amc=' + amc, $scope.fmcgData, true).success(function(response) {
                console.log(response);
            });
            var value = $scope.$parent.fmcgData;
            for (key in value)
            {
                if (key)
                {
                    $scope.$parent.fmcgData[key] = undefined;
                }
            }
            Toast("Call updated SuccessFully");
        }
        else
        {
            fmcgAPIservice.saveDCRData('POST', 'dcr/save', $scope.fmcgData, false).success(function(response) {
                if (!response['success'] && response['type'] == 1)
                {
                    $scope.showConfirm = function() {
                        var confirmPopup = $ionicPopup.confirm({
                            title: 'Warning !',
                            template: response['msg']
                        });
                        confirmPopup.then(function(res) {
                            if (res) {
                                fmcgAPIservice.updateDCRData('POST', 'dcr/save&replace', response['data'], false)
                            } else {

                            }
                        });
                    };
                    $scope.showConfirm();
                }
                else if (!response['success'])
                {
                    Toast(response['msg'], true);

                }
                else
                {
                    Toast("call Submited Successfully")
                }
            });
            var value = $scope.$parent.fmcgData;
            for (key in value)
            {
                if (key)
                {
                    $scope.$parent.fmcgData[key] = undefined;
                }
            }

        }

        $state.go('fmcgmenu.home');
    }
    $scope.goBack = function() {
        if ($scope.fmcgData.worktype.selected.id.toString() !== "field work")
        {
            $state.go('fmcgmenu.addNew')
        }
        else if ($scope.fmcgData.customer.selected.id.toString() === "4")
        {
            $state.go('fmcgmenu.screen3')
        }
        else if ($scope.fmcgData.customer.selected.id.toString() === "1" || $scope.fmcgData.customer.selected.id.toString() === "2")
        {
            $state.go('fmcgmenu.screen4')
        }
        else
        {
            $state.go('fmcgmenu.screen2')
        }
    };
}).controller('draftCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage,notification) {
    $scope.$parent.navTitle = "Draft Calls";
    $ionicLoading.show({
        template: 'Loading...'
    });
    $scope.clearData();
    $scope.success = false;
    $scope.nodata = false;
    $scope.owsuccess = false;
    $scope.owTypeData = [];
    $scope.deleteEntry = function() {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function(res) {
            if (res) {
                localStorage.removeItem("draft");
                $state.go('fmcgmenu.home');
            } else {
                console.log('You are not sure');
            }
        }
        );
    };
    $scope.editEntry = function() {
        var value = fmcgLocalStorage.getData("draft");

        for (key in value[0])
        {
            if (key)
            {
                $scope.$parent.fmcgData[key] = value[0][key];
            }
        }
        localStorage.removeItem("draft");
        $state.go('fmcgmenu.addNew');
    }
    $scope.saveEntry = function() {
        var temp = fmcgLocalStorage.getData("draft");
        fmcgAPIservice.saveDCRData('POST', 'dcr/save', temp[0], false).success(function(response) {
            if (!response['success'] && response['type'] == 1)
            {
                $scope.showConfirm = function() {
                    var confirmPopup = $ionicPopup.confirm({
                        title: 'Warning !',
                        template: response['msg']
                    });
                    confirmPopup.then(function(res) {
                        if (res) {
                            fmcgAPIservice.updateDCRData('POST', 'dcr/save&replace', response['data'], false)
                            localStorage.removeItem("draft");
                        } else {
                            localStorage.removeItem("draft");
                        }
                    });
                };
                $scope.showConfirm();
            }
            else if (!response['success'])
            {
                Toast(response['msg'], true);
                localStorage.removeItem("draft");
            }
            else
            {
                Toast("call Submited Successfully");
                localStorage.removeItem("draft");
            }
        });

    }

    //$scope.$parent.navTitle = "Submitted Calls";
    $scope.customers = [{
            'id': '1',
            'name': 'Listed Dr',
            'url': 'manageDoctorResult'
        }, {
            'id': '2',
            'name': 'Chemist',
            'url': 'manageChemistResult'
        }, {
            'id': '3',
            'name': 'Stockist',
            'url': 'manageStockistResult'
        }, {
            'id': '4',
            'name': 'Unlisted Dr',
            'url': 'manageStockistResult'
        }];
    $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
    if ($scope.worktypes.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
            '["type_code as id", "wtype as name"]'
                    , ,"isnull(Active_flag,'0')='0'", , , , 0]).success(function(response) {
            $scope.worktypes = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("mas_worktype", response);
        });
    }

    $scope.getData = function() {
        response = fmcgLocalStorage.getEntryCount();
        if (response['success'])
        {
            $scope.success = true;
            $scope.customers[0].count = response['data']['doctor_count'];
            $scope.customers[1].count = response['data']['chemist_count'];
            $scope.customers[2].count = response['data']['stockist_count'];
            $scope.customers[3].count = response['data']['uldoctor_count'];
        }
        else if (response['ndata'])
        {
            $scope.nodata = true;
        }
        else
        {
            $scope.owsuccess = true;
            console.log(response['data']);
            $scope.owTypeData = response['data'];
        }
        $ionicLoading.hide();
    };
    $scope.getData();
}).controller('reloadCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage,notification) {
    $scope.$parent.navTitle = "Reload Master";
    $scope.countInc = 0;
    $scope.clearIdividual = function(value, total) {

        switch (value) {
            case 0:
                window.localStorage.removeItem("doctor_master");
                fmcgAPIservice.getDataList('POST', 'table/list', ["doctor_master",
                    '["doctor_code as id", "doctor_name as name","town_code"]',,"isnull(Doctor_Active_flag,'0')='0'"
                ]).success(function(response) {
                    $scope.countInc++;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("doctor_master", response);
                });
                break;
            case 1:
                window.localStorage.removeItem("doctor_master");
                fmcgAPIservice.getDataList('POST', 'table/list', ["chemists_master",
                    '["chemists_code as id", "chemists_name as name","town_code"]',,"isnull(Chemist_Status,'0')='0'"
                ]).success(function(response) {
                    $scope.countInc++;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("chemist_master", response);
                });
                break;
            case 2:
                window.localStorage.removeItem("doctor_master");
                fmcgAPIservice.getDataList('POST', 'table/list', ["stockiest_master",
                    '["stockiest_code as id", "stockiest_name as name","town_code"]',,"isnull(Stockist_Status,'0')='0'"
                ]).success(function(response) {
                    $scope.countInc++;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("stockist_master", response);
                });

                break;
            case 3:
                window.localStorage.removeItem("doctor_master");
                fmcgAPIservice.getDataList('POST', 'table/list', ["unlisted_doctor_master",
                    '["unlisted_doctor_code as id", "unlisted_doctor_name as name","town_code"]',,"isnull(unlisted_activation_flag,'0')='0'"
                ]).success(function(response) {
                    $scope.countInc++;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("unlisted_doctor_master", response);
                });
                break;
            case 4:
                window.localStorage.removeItem("doctor_master");
                fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
                    '["type_code as id", "Wtype as name"]'
                            , ,"isnull(Active_flag,'0')='0'", , , , 0]).success(function(response) {
                    $scope.countInc++;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("mas_worktype", response);
                });
                break;
            case 5:
                window.localStorage.removeItem("doctor_master");
                fmcgAPIservice.getDataList('POST', 'table/list', ["product_master",
                    '["product_code as id", "product_name as name"]'
                            , ,"isnull(Product_DeActivation_Flag,'0')='0'", , , , 0]).success(function(response) {
                    $scope.countInc++;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("product_master", response);
                });
                break;
            case 6:
                window.localStorage.removeItem("doctor_master");
                fmcgAPIservice.getDataList('POST', 'table/list', ["gift_master",
                    '["gift_code as id", "gift_name as name"]'
                            , ,"isnull(Gift_DeActivate_Flag,'0')='0'", , , , 0]).success(function(response) {
                    $scope.countInc++;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("gift_master", response);
                });
                break;
            case 7:
                window.localStorage.removeItem("doctor_master");
                fmcgAPIservice.getDataList('POST', 'get/jointwork', ["salesforce_master",
                    '["sf_code as id", "sf_name as name"]'
                ]).success(function(response) {
                    $scope.countInc++;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("salesforce_master", response);
                });
                break;
        }


    }
    $scope.clearItem = function(value) {

        $scope.clearIdividual(value);

        Toast("Data Reloaded");
    };
    $scope.clearAll = function() {
        $ionicLoading.show();
        for (var i = 0; i < 8; i++)
            $scope.clearIdividual(i);
        Toast("Data Reloaded");

    };
}).controller('draftViewCtrl', function($rootScope, $scope, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage,notification) {


    $scope.modal = $ionicModal;
    $scope.modal.fromTemplateUrl('partials/manageDataViewModal.html', function(modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });
    $scope.data = {
        showDelete: false
    };
    $scope.customerDatas;
    $scope.myChoice = $stateParams.myChoice;
    switch ($scope.myChoice)
    {
        case "1":
            $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
            if ($scope.customerDatas.length == 0)
            {
                fmcgAPIservice.getDataList('POST', 'table/list', ["doctor_master",
                    '["doctor_code as id", "doctor_name as name","town_code"]',,"isnull(Doctor_Active_flag,'0')='0'"
                ]).success(function(response) {
                    $scope.customerDatas = response;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("doctor_master", response);
                });
            }
            break;
        case "3":
            $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
            if ($scope.customerDatas.length == 0)
            {
                fmcgAPIservice.getDataList('POST', 'table/list', ["stockiest_master",
                    '["stockiest_code as id", "stockiest_name as name","town_code"]',,"isnull(Stockist_Status,'0')='0'"
                ]).success(function(response) {
                    $scope.customerDatas = response;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("stockist_master", response);
                });
            }
            break;
        case "2":

            $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
            if ($scope.customerDataslength == 0)
            {
                fmcgAPIservice.getDataList('POST', 'table/list', ["chemists_master",
                    '["chemists_code as id", "chemists_name as name","town_code"]',,"isnull(Chemist_Status,'0')='0'"
                ]).success(function(response) {
                    $scope.customerDatas = response;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("chemist_master", response);
                });
            }
            break;
        case "4":
            $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
            if ($scope.customerDatas.length == 0)
            {
                fmcgAPIservice.getDataList('POST', 'table/list', ["unlisted_doctor_master",
                    '["unlisted_doctor_code as id", "unlisted_doctor_name as name","town_code"]',,"isnull(unlisted_activation_flag,'0')='0'"
                ]).success(function(response) {
                    $scope.customerDatas = response;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("unlisted_doctor_master", response);
                });
            }
            break;
    }
    $scope.draftButtons = [
        {
            text: 'Edit',
            type: 'button-calm',
            onTap: function(item) {
                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                var value = item;

                for (key in value)
                {
                    if (key)
                    {
                        $scope.$parent.fmcgData[key] = item[key];
                    }
                }
                fmcgLocalStorage.createData('draft', $scope.drafts);
                $state.go('fmcgmenu.addNew');
            }
        }

    ];
    $scope.edit = function(item) {
        $scope.drafts.splice($scope.drafts.indexOf(item), 1);
        var value = item;

        for (key in value)
        {
            if (key)
            {
                $scope.$parent.fmcgData[key] = item[key];
            }
        }
        fmcgLocalStorage.createData('draft', $scope.drafts);
        $state.go('fmcgmenu.screen2');
    };
    $scope.save = function(item) {

        fmcgAPIservice.saveDCRData('POST', 'dcr/save', item, false).success(function(response) {
            if (!response['success'])
            {
                Toast(response['msg'], true);

            }
            $scope.drafts.splice($scope.drafts.indexOf(item), 1);
            fmcgLocalStorage.createData('draft', $scope.drafts);
        });

    };
    $scope.preView = function(item) {
        $scope.modal.par = $scope;
        $scope.modal.draft = item;
        $scope.modal.show();
    };
    $scope.delete = function(item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function(res) {
            if (res) {
                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                fmcgLocalStorage.createData('draft', $scope.drafts);
            } else {
                console.log('You are not sure');
            }
        }
        );
    };
    $scope.drafts = [];
    $scope.drafts = fmcgLocalStorage.getData("draft");
}).controller('manageDataViewCtrl', function($rootScope, $scope, $ionicLoading, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage,notification) {
    $ionicLoading.show({
        template: 'Loading...'
    });
    $scope.drafts = [];
    var tm = [];
    $scope.reportTemplates = fmcgLocalStorage.getData("report_template_master") || [];
    if ($scope.reportTemplates.length == 0)
    {

        fmcgAPIservice.getDataList('POST', 'table/list', ["report_template_master",
            '["id as id", "content as name"]'
                    , , , , , , 0]).success(function(response) {
            $scope.reportTemplates = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("report_template_master", response);
        });
    }
    $scope.nonreportTemplates = fmcgLocalStorage.getData("nonreport_template_master") || [];
    if ($scope.nonreportTemplates.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["nonreport_template_master",
            '["id as id", "content as name"]'
                    , , , , , , 0]).success(function(response) {
            $scope.nonreportTemplates = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("nonreport_template_master", response);
        });
    }
    $scope.$parent.navTitle = "Submitted Calls";
    $scope.clearData();
    $scope.modal = $ionicModal;
    $scope.modal.fromTemplateUrl('partials/dataViewModal.html', function(modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });
    $scope.data = {
        showDelete: false
    };
    $scope.customerDatas;
    $scope.myChoice = $stateParams.myChoice;
    switch ($scope.myChoice)
    {
        case "1":
            $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
            if ($scope.customerDatas.length == 0)
            {
                fmcgAPIservice.getDataList('POST', 'table/list', ["doctor_master",
                    '["doctor_code as id", "doctor_name as name","town_code"]',,"isnull(Doctor_Active_flag,'0')='0'"
                ]).success(function(response) {
                    $scope.customerDatas = response;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("doctor_master", response);
                });
            }
            fmcgAPIservice.getDataList('POST', 'table/list', ["activity_Report_APP", '["*"]', , , 1, , 1]).success(function(response) {
                var tm = [];
                var pklist = [];
                for (var i = 0, len = response.length; i < len; i++)
                {
                    var tempData = {};
                    tempData['worktype'] = {};
                    tempData['worktype']['selected'] = {};
                    tempData['cluster'] = {};
                    tempData['cluster']['selected'] = {};
                    tempData['remarks'] = "";
                    tempData['arc'] = "";
                    tempData.worktype.selected.id = response[i]['worktype_code'];
                    tempData.cluster.selected.id = response[i]['town_code'];
                    tempData.remarks = response[i]["daywise_remarks"];
                    tempData.arc = response[i]["activity_report_code"];
                    tempData.rx = response[i]['rx'];
                    if (tempData.rx)
                        tempData.rx_t = response[i]['rx_t'];
                    else
                        tempData.nrx_t = response[i]['nrx_t'];
                    pklist.push("activity_report_code=\'" + tempData.arc + "\'");
                    tm.push(tempData);
                }
                fmcgAPIservice.getDataList('POST', 'table/list', ["activity_doctor_report", '["*"]', , JSON.stringify(pklist), , 1]).success(function(response1) {
                    var mslColl = [];
                    for (var j = 0, le = response1.length; j < le; j++)
                    {
                        mslColl.push("activity_msl_code=\'" + response1[j]['activity_msl_code'] + "\'");
                        for (var k = 0, leng = tm.length; k < leng; k++)
                        {
                            if (tm[k]['arc'] === response1[j]['activity_report_code'])
                            {
                                tm[k]['amc'] = response1[j]['activity_msl_code'];
                                tm[k]['pob'] = parseInt(response1[j]['doctor_pob']);
                                tm[k]['customer'] = {};
                                tm[k]['customer']['selected'] = {}
                                tm[k]['customer']['selected']['id'] = "1";
                                tm[k]['doctor'] = {};
                                tm[k]['location'] = response1[j]['location'];
                                tm[k]['doctor']['selected'] = {}
                                tm[k]['doctor']['selected']['id'] = response1[j]['doctor_code'];
                                // tm[k]['jointwork'] = {};
                                var response2 = response1[j]['worked_with'].split("$");
                                for (var m = 0, leg = response2.length; m < leg; m++)
                                {

                                    tm[k]['jontWorkSelectedList'] = tm[k]['jontWorkSelectedList'] || [];
                                    var pTemp = {};
                                    pTemp.jointwork = response2[m].toString();
                                    if (pTemp.jointwork.length !== 0)
                                        tm[k]['jontWorkSelectedList'].push(pTemp);
                                }

// tm[k]['jointwork'].selected = $filter('getValueforID')(response1[j]['worked_with'], $scope.jointworks);

                                tm[k]['entryDate'] = new Date(response1[j]['doc_meet_time']['date']);
                                tm[k]['modifiedDate'] = new Date(response1[j]['modified_time']['date']);
                                //tm[k]['entryDate'].setMonth(tm[k]['entryDate'].getMonth());

                            }
                        }
                    }
                    if (response1.length > 0)
                    {

                        $scope.drafts = tm;
                        fmcgAPIservice.getDataList('POST', 'table/list', ["activity_sample_report", '["*"]', , JSON.stringify(mslColl), , 1]).success(function(response2) {
                            for (var m = 0, leg = response2.length; m < leg; m++)
                            {
                                for (var n = 0, lengt = tm.length; n < lengt; n++)
                                {
                                    if (tm[n]['amc'] === response2[m]['Activity_MSL_Code'])
                                    {
                                        tm[n]['productSelectedList'] = tm[n]['productSelectedList'] || [];
                                        var pTemp = {};
                                        pTemp.product = response2[m].Product_Code;
                                        pTemp.sample_qty = response2[m].Product_Sample_Qty;
                                        pTemp.rx_qty = response2[m].Product_Rx_Qty;
                                        tm[n]['productSelectedList'].push(pTemp);
                                    }
                                    $scope.drafts = tm;
                                }
                            }
                            $ionicLoading.hide();
                        });
                        fmcgAPIservice.getDataList('POST', 'table/list', ["activity_input_report", '["*"]', , JSON.stringify(mslColl), , 1]).success(function(response3) {

                            $scope.drafts = tm;
                            for (var m = 0, leg = response3.length; m < leg; m++)
                            {
                                for (var n = 0, lengt = tm.length; n < lengt; n++)
                                {
                                    if (tm[n]['amc'] === response3[m]['Activity_MSL_Code'])
                                    {
                                        tm[n]['giftSelectedList'] = tm[n]['giftSelectedList'] || [];
                                        var giftTemp = {};
                                        giftTemp.gift = response3[m].Gift_Code;
                                        giftTemp.sample_qty = response3[m].Gift_Qty;
                                        tm[n]['giftSelectedList'].push(giftTemp);
                                    }

                                }
                            }
                            $ionicLoading.hide();
                            $scope.drafts = tm;

                        });
                    }
                    else
                    {
                        $ionicLoading.hide();
                    }
                });
            });

            break;
        case "3":
            $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
            if ($scope.customerDatas.length == 0)
            {
                fmcgAPIservice.getDataList('POST', 'table/list', ["stockiest_master",
                    '["stockiest_code as id", "stockiest_name as name","town_code"]',,"isnull(Stockist_Status,'0')='0'"
                ]).success(function(response) {
                    $scope.customerDatas = response;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("stockist_master", response);
                });
            }
            fmcgAPIservice.getDataList('POST', 'table/list', ["activity_Report_APP", '["*"]', , , 1, , 1]).success(function(response) {
                var tm = [];
                var pklist = [];
                for (var i = 0, len = response.length; i < len; i++)
                {


                    var tempData = {};
                    tempData['worktype'] = {};
                    tempData['worktype']['selected'] = {};
                    tempData['cluster'] = {};
                    tempData['cluster']['selected'] = {};
                    tempData['remarks'] = "";
                    tempData['arc'] = "";
                    tempData.worktype.selected.id = response[i]['worktype_code'];
                    tempData.cluster.selected.id = response[i]['town_code'];
                    tempData.remarks = response[i]["daywise_remarks"];
                    tempData.rx = response[i]['rx'];
                    if (tempData.rx)
                        tempData.rx_t = response[i]['rx_t'];
                    else
                        tempData.nrx_t = response[i]['nrx_t'];
                    tempData.arc = response[i]["activity_report_code"];
                    pklist.push("activity_report_code=\'" + tempData.arc + "\'");
                    tm.push(tempData);
                }
                fmcgAPIservice.getDataList('POST', 'table/list', ["activity_stockist_report", '["*"]', , JSON.stringify(pklist), , 1]).success(function(response1) {
                    var mslColl = [];
                    for (var j = 0, le = response1.length; j < le; j++)
                    {
                        mslColl.push("activity_stockist_code=\'" + response1[j]['activity_stockist_code'] + "\'")
                        for (var k = 0, leng = tm.length; k < leng; k++)
                        {
                            if (tm[k]['arc'] === response1[j]['activity_report_code'])
                            {
                                tm[k]['amc'] = response1[j]['activity_stockist_code'];
                                tm[k]['pob'] = parseInt(response1[j]['stockist_POB']);
                                tm[k]['customer'] = {};
                                tm[k]['customer']['selected'] = {}
                                tm[k]['customer']['selected']['id'] = "3";
                                tm[k]['location'] = response1[j]['location'];
                                tm[k]['stockist'] = {};
                                tm[k]['stockist']['selected'] = {}
                                tm[k]['stockist']['selected']['id'] = response1[j]['stockist_code'];
                                var response2 = response1[j]['worked_with'].split("$");
                                for (var m = 0, leg = response2.length; m < leg; m++)
                                {

                                    tm[k]['jontWorkSelectedList'] = tm[k]['jontWorkSelectedList'] || [];
                                    var pTemp = {};
                                    pTemp.jointwork = response2[m].toString();
                                    if (pTemp.jointwork.length !== 0)
                                        tm[k]['jontWorkSelectedList'].push(pTemp);
                                }

                                tm[k]['entryDate'] = new Date(response1[j]['stk_meet_time']['date']);
                                tm[k]['modifiedDate'] = new Date(response1[j]['modified_time']['date']);
                                //tm[k]['entryDate'].setMonth(tm[k]['entryDate'].getMonth());
                            }
                        }

                    }
                    $scope.drafts = tm;
                    $ionicLoading.hide();
                });
            });
            break;
        case "2":

            $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
            if ($scope.customerDataslength == 0)
            {
                fmcgAPIservice.getDataList('POST', 'table/list', ["chemists_master",
                    '["chemists_code as id", "chemists_name as name","town_code"]',,"isnull(Chemist_Status,'0')='0'"
                ]).success(function(response) {
                    $scope.customerDatas = response;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("chemist_master", response);
                });
            }
            fmcgAPIservice.getDataList('POST', 'table/list', ["activity_Report_APP", '["*"]', , , 1, , 1]).success(function(response) {
                var tm = [];
                var pklist = [];
                for (var i = 0, len = response.length; i < len; i++)
                {
                    var tempData = {};
                    tempData['worktype'] = {};
                    tempData['worktype']['selected'] = {};
                    tempData['cluster'] = {};
                    tempData['cluster']['selected'] = {};
                    tempData['remarks'] = "";
                    tempData['arc'] = "";
                    tempData.worktype.selected.id = response[i]['worktype_code'];
                    tempData.cluster.selected.id = response[i]['town_code'];
                    tempData.remarks = response[i]["daywise_remarks"];
                    tempData.arc = response[i]["activity_report_code"];
                    tempData.rx = response[i]['rx'];
                    if (tempData.rx)
                        tempData.rx_t = response[i]['rx_t'];
                    else
                        tempData.nrx_t = response[i]['nrx_t'];
                    pklist.push("activity_report_code=\'" + tempData.arc + "\'");
                    tm.push(tempData);
                }
                fmcgAPIservice.getDataList('POST', 'table/list', ["activity_Chemist_report", '["*"]', , JSON.stringify(pklist), , 1]).success(function(response1) {
                    var mslColl = [];
                    for (var j = 0, le = response1.length; j < le; j++)
                    {
                        mslColl.push("activity_chemist_code=\'" + response1[j]['activity_chemist_code'] + "\'")
                        for (var k = 0, leng = tm.length; k < leng; k++)
                        {
                            if (tm[k]['arc'] === response1[j]['activity_report_code'])
                            {
                                tm[k]['amc'] = response1[j]['activity_chemist_code'];
                                tm[k]['pob'] = parseInt(response1[j]['chemist_pob']);
                                tm[k]['customer'] = {};
                                tm[k]['location'] = response1[j]['location'];
                                tm[k]['customer']['selected'] = {}
                                tm[k]['customer']['selected']['id'] = "2";
                                tm[k]['chemist'] = {};
                                tm[k]['chemist']['selected'] = {}
                                tm[k]['chemist']['selected']['id'] = response1[j]['chemist_code'];
                                var response2 = response1[j]['worked_with'].split("$");
                                for (var m = 0, leg = response2.length; m < leg; m++)
                                {

                                    tm[k]['jontWorkSelectedList'] = tm[k]['jontWorkSelectedList'] || [];
                                    var pTemp = {};
                                    pTemp.jointwork = response2[m].toString();
                                    if (pTemp.jointwork.length !== 0)
                                        tm[k]['jontWorkSelectedList'].push(pTemp);
                                }

                                tm[k]['entryDate'] = new Date(response1[j]['chm_meet_time']['date']);
                                tm[k]['modifiedDate'] = new Date(response1[j]['modified_time']['date']);
                                //tm[k]['entryDate'].setMonth(tm[k]['entryDate'].getMonth());
                            }
                        }

                    }
                    $scope.drafts = tm;
                    if (response1.length > 0)
                    {


                        fmcgAPIservice.getDataList('POST', 'table/list', ["activity_chm_sample_report", '["*"]', , JSON.stringify(mslColl), , 1]).success(function(response3) {
                            $scope.drafts = tm;
                            for (var m = 0, leg = response3.length; m < leg; m++)
                            {
                                for (var n = 0, lengt = tm.length; n < lengt; n++)
                                {
                                    if (tm[n]['amc'] === response3[m]['activity_chemist_code'])
                                    {
                                        tm[n]['giftSelectedList'] = tm[n]['giftSelectedList'] || [];
                                        var giftTemp = {};
                                        giftTemp.gift = response3[m].gift_code;
                                        giftTemp.sample_qty = response3[m].gift_qty;
                                        tm[n]['giftSelectedList'].push(giftTemp);
                                    }

                                }
                            }
                            $scope.drafts = tm;
                            $ionicLoading.hide();
                        });
                    }
                    else
                    {
                        $ionicLoading.hide();
                    }
                });
            });
            break;
        case "4":
            $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
            if ($scope.customerDatas.length == 0)
            {
                fmcgAPIservice.getDataList('POST', 'table/list', ["unlisted_doctor_master",
                    '["unlisted_doctor_code as id", "unlisted_doctor_name as name","town_code"]',,"isnull(unlisted_activation_flag,'0')='0'"
                ]).success(function(response) {
                    $scope.customerDatas = response;
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("unlisted_doctor_master", response);
                });
            }
            fmcgAPIservice.getDataList('POST', 'table/list', ["activity_Report_APP", '["*"]', , , 1, , 1]).success(function(response) {
                var tm = [];
                var pklist = [];
                for (var i = 0, len = response.length; i < len; i++)
                {


                    var tempData = {};
                    tempData['worktype'] = {};
                    tempData['worktype']['selected'] = {};
                    tempData['cluster'] = {};
                    tempData['cluster']['selected'] = {};
                    tempData['remarks'] = "";
                    tempData['arc'] = "";
                    tempData.worktype.selected.id = response[i]['worktype_code'];
                    tempData.cluster.selected.id = response[i]['town_code'];
                    tempData.remarks = response[i]["daywise_remarks"];
                    tempData.rx = response[i]['rx'];
                    if (tempData.rx)
                        tempData.rx_t = response[i]['rx_t'];
                    else
                        tempData.nrx_t = response[i]['nrx_t'];
                    tempData.arc = response[i]["activity_report_code"];
                    pklist.push("activity_report_code=\'" + tempData.arc + "\'");
                    tm.push(tempData);
                }
                fmcgAPIservice.getDataList('POST', 'table/list', ["activity_unlisteddoctor_Report", '["*"]', , JSON.stringify(pklist), , 1]).success(function(response1) {
                    var mslColl = [];
                    for (var j = 0, le = response1.length; j < le; j++)
                    {
                        mslColl.push("activity_msl_code=\'" + response1[j]['activity_msl_unlistedcode'] + "\'")
                        for (var k = 0, leng = tm.length; k < leng; k++)
                        {
                            if (tm[k]['arc'] === response1[j]['activity_report_code'])
                            {
                                tm[k]['amc'] = response1[j]['activity_msl_code'];
                                tm[k]['pob'] = parseInt(response1[j]['unlisted_doctor_pob']);
                                tm[k]['customer'] = {};
                                tm[k]['location'] = response1[j]['location'];
                                tm[k]['customer']['selected'] = {}
                                tm[k]['customer']['selected']['id'] = "4";
                                tm[k]['uldoctor'] = {};
                                tm[k]['uldoctor']['selected'] = {}
                                tm[k]['uldoctor']['selected']['id'] = response1[j]['uldoctor_code'];
                                // tm[k]['jointwork'] = {};
                                var response2 = response1[j]['worked_with'].split("$");
                                for (var m = 0, leg = response2.length; m < leg; m++)
                                {

                                    tm[k]['jontWorkSelectedList'] = tm[k]['jontWorkSelectedList'] || [];
                                    var pTemp = {};
                                    pTemp.jointwork = response2[m].toString();
                                    if (pTemp.jointwork.length !== 0)
                                        tm[k]['jontWorkSelectedList'].push(pTemp);
                                }

// tm[k]['jointwork'].selected = $filter('getValueforID')(response1[j]['worked_with'], $scope.jointworks);
                                tm[k]['entryDate'] = new Date(response1[j]['unlisted_doc_meet_time']['date']);
                                tm[k]['modifiedDate'] = new Date(response1[j]['modified_time']['date']);
                                //tm[k]['entryDate'].setMonth(tm[k]['entryDate'].getMonth());
                            }
                        }
                    }
                    if (response1.length > 0)
                    {

                        $scope.drafts = tm;
                        fmcgAPIservice.getDataList('POST', 'table/list', ["activity_unlistedsample_Report", '["*"]', , JSON.stringify(mslColl), , 1]).success(function(response2) {
                            for (var m = 0, leg = response2.length; m < leg; m++)
                            {
                                for (var n = 0, lengt = tm.length; n < lengt; n++)
                                {
                                    if (tm[n]['amc'] === response2[m]['Activity_MSL_Code'])
                                    {
                                        tm[n]['productSelectedList'] = tm[n]['productSelectedList'] || [];
                                        var pTemp = {};
                                        pTemp.product = response2[m].product_code;
                                        pTemp.sample_qty = response2[m].product_sample_qty;
                                        pTemp.rx_qty = response2[m].product_rx_qty;
                                        tm[n]['productSelectedList'].push(pTemp);
                                    }
                                    $ionicLoading.hide();
                                    $scope.drafts = tm;
                                }
                            }
                            $ionicLoading.hide();
                        });
                    }
                    else
                    {
                        $ionicLoading.hide();
                    }
                });
            });
            break;
    }

    $scope.edit = function(item) {
        $scope.drafts.splice($scope.drafts.indexOf(item), 1);
        var value = item;
        for (key in value)
        {
            if (key)
            {
                $scope.$parent.fmcgData[key] = item[key];
            }
        }
        fmcgLocalStorage.createData('draft', $scope.drafts);
        $state.go('fmcgmenu.screen2');
    };
    $scope.preView = function(item) {
        $scope.modal.par = $scope;
        $scope.modal.draft = item;
        $scope.modal.show();
    };
    $scope.delete = function(item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function(res) {
            if (res) {
                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                fmcgAPIservice.deleteEntry('POST', 'deleteEntry', item).success(function(response) {
                });
            } else {
                console.log('You are not sure');
            }
        }
        );
    };
}).controller('dcrData', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage,notification) {
    $scope.$parent.navTitle = "DCR";
    $scope.clearData();
    $scope.dcrDataCur = {};
    fmcgAPIservice.getDataList('POST', 'dcr/callReport', []).success(function(response) {
        var data = response;
        var temp = [];
        for (var i = 0; i < data.length; i++)
        {
            var flag = true;
            for (var j = 0; j < temp.length; j++)
            {
                if (data[i]['day_dcr'] == temp[j]['day_dcr'])
                {
                    temp[j]['town_code'] = temp[j]['town_code'] + ',' + data[i]['town_code'];
                    temp[j]['chm_count'] = parseInt(temp[j]['chm_count']) + parseInt(data[i]['chm_count']);
                    temp[j]['stk_count'] = parseInt(temp[j]['stk_count']) + parseInt(data[i]['stk_count']);
                    temp[j]['doc_count'] = parseInt(temp[j]['doc_count']) + parseInt(data[i]['doc_count']);
                    flag = false;
                }

            }

            if (flag)
            {
                temp.push(data[i]);
            }
        }
        $scope.dcrDataCur = temp;
    });

}).controller('dcrData1', function($rootScope, $ionicLoading, $scope, $state, fmcgAPIservice, fmcgLocalStorage,notification) {
    $scope.$parent.navTitle = "DCR";
    $scope.clearData();
    $ionicLoading.show({
        template: 'Loading...'
    });
    $scope.success = false;
    $scope.owsuccess = false;
    $scope.owTypeData = [];
    $scope.$parent.navTitle = "Submitted Calls";
    $scope.customers = [{
            'id': '1',
            'name': 'Listed Dr',
            'url': 'manageDoctorResult'
        }, {
            'id': '2',
            'name': 'Chemist',
            'url': 'manageChemistResult'
        }, {
            'id': '3',
            'name': 'Stockist',
            'url': 'manageStockistResult'
        }, {
            'id': '4',
            'name': 'Unlisted Dr',
            'url': 'manageStockistResult'
        }];
    $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
    if ($scope.worktypes.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
            '["type_code as id", "wtype as name"]'
                    , ,"isnull(Active_flag,'0')='0'", , , , 0]).success(function(response) {
            $scope.worktypes = response;
            if (response.length && response.length > 0 && Array.isArray(response))
                fmcgLocalStorage.createData("mas_worktype", response);
        });
    }
    fmcgAPIservice.getDataList('POST', 'entry/count', []).success(function(response) {
        if (response['success'])
        {
            $scope.success = true;
            $scope.customers[0].count = response['data'][0]['doctor_count'];
            $scope.customers[1].count = response['data'][1]['chemist_count'];
            $scope.customers[2].count = response['data'][2]['stockist_count'];
            $scope.customers[3].count = response['data'][3]['uldoctor_count'];
        }
        else
        {
            $scope.owsuccess = true;
            $scope.owTypeData = response['data'][0];
        }
        $ionicLoading.hide();
    });
}).filter('selectMap', function() {

    return function(input, data) {
        if (data)
        {
            for (var i = 0, len = data['collection'].length; i < len; i++)
            {
                if (parseInt(data['collection'][i].id) === parseInt(input))
                {
                    input = data['collection'][i].name;
                    break;
                }
            }
        }
        return input;
    };
}).filter('reverseGeoCode', function() {

    return function(input) {
        return input;
    };
}).filter('getValueforID', function() {
    return function(id, collection) {
        var arrayToReturn = [];
        if (collection && id)
        {

            var len;
            try {
                len = collection.length;
            }
            catch (m)
            {
                len = 0;
            }
            for (var i = 0; i < len; i++) {

                if (id === collection[i].id) {
                    return collection[i];
                    break;
                }
            }
        }
        return null;
    }
    ;
}).filter('searchF', function() {
    return function(id, collection) {
        var arrayToReturn = [];
        if (collection && id)
        {
            collection = collection;
            var len;
            try {
                len = collection.length;
            }
            catch (m)
            {
                len = 0;
            }
            for (var i = 0; i < len; i++) {

                if (id.toString() === collection[i].town_code.toString()) {
                    arrayToReturn.push(collection[i]);

                }
            }
            return arrayToReturn;
        }
        return [];
    }
    ;
}).filter('getValueforIDD', function() {
    return function(id, collection) {
        var arrayToReturn = [];
        if (collection && id)
        {
            collection = collection.collection;
            var len;
            try {
                len = collection.length;
            }
            catch (m)
            {
                len = 0;
            }
            for (var i = 0; i < len; i++) {

                if (id.toString() === collection[i].id.toString()) {
                    return collection[i];
                    break;
                }
            }
        }
        return null;
    }
    ;
}).directive('reverseGeocode', function() {
    return {
        restrict: 'E',
        template: '<div></div>',
        link: function(scope, element, attrs) {
            var geocoder = new google.maps.Geocoder();
            var results = attrs.latlng.split(":");
            if (results.length == 2)
            {
                var latlng = new google.maps.LatLng(results[0], results[1]);
                geocoder.geocode({'latLng': latlng}, function(results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[1]) {
                            element.text(results[1].formatted_address);
                        } else {
                            element.text('Location not found');
                        }
                    } else {
                        element.text('Geocoder failed due to: ' + status);
                    }
                });
            }
        },
        replace: true
    }
});
var isReachable = function()
{
    if (navigator.connection)
    {
        var networkState = navigator.connection ? navigator.connection.type : Connection.NONE;
        var states = {};
        states[Connection.UNKNOWN] = 'Unknown connection';
        states[Connection.ETHERNET] = 'Ethernet connection';
        states[Connection.WIFI] = 'WiFi connection';
        states[Connection.CELL_2G] = 'Cell 2G connection';
        states[Connection.CELL_3G] = 'Cell 3G connection';
        states[Connection.CELL_4G] = 'Cell 4G connection';
        states[Connection.NONE] = 'No network connection';
        if (Connection.NONE == networkState)
            return false;
        else
            return true;
    }
    else
    {
        return navigator.onLine;
    }
}
var isComputer = function()
{

    if (navigator.platform === "Win32")
    {
        return true;
    }
    return false;
}
var isGpsEnabled = function(PositionError)
{
    if (isComputer())
        return true;
    else if (PositionError)
    {
        return false;
    }
    return true;
}
var Toast = function(message, type)
{
    if (window.plugins)
    {
        window.plugins.toast.showShortCenter(message, function(a) {
            console.log('toast success: ' + a)
        }, function(b) {
            alert('toast error: ' + b)
        });
    }
    else
    {
        var data = {};
        data.message = message;
        data.extraClasses = 'messenger-fixed messenger-on-left messenger-on-top';
        if (type)
            data.type = "error";
        Messenger().post(data);
    }
};