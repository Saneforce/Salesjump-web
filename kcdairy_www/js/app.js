
var fmcg = angular.module('fmcg', ['ionic', 'fmcgServices', 'ngGrid', 'infinite-scroll', 'chart.js', 'flexcalendar', 'pascalprecht.translate'])
        .config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
            $stateProvider
                    .state('signin', {
                        url: "/sign-in",
                        templateUrl: "partials/sign-in.html",
                        controller: 'SignInCtrl'
                    })
                    .state('vacScr', {
                        url: "/vacScr",
                        templateUrl: "partials/VacPg.html",
                        controller: 'VacPgCtrl'
                    })
                    .state('fmcgmenu.mail', {
                        url: "/mail",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/mail.html",
                                controller: "mailCtrl"
                            }
                        }
                    })
                     .state('fmcgmenu.sendMail', {
                         url: "/sendMail",
                         views: {
                             'menuContent': {
                                 templateUrl: "partials/sendMail.html",
                                 controller: "sendMailCtrl"
                             }
                         }
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
                    .state('fmcgmenu.RCPA', {
                        url: "/RCPA",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/RCPA.html",
                                controller: "RCPACtrl"
                            }
                        }
                    })
                    .state('fmcgmenu.RMCL', {
                        url: "/RMCL",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/CallReminder.html",
                                controller: "RMCLCtrl"
                            }
                        }
                    })
                    .state('fmcgmenu.precall', {
                        url: "/precall",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/PrecallAnalysis.html",
                                controller: "precallCtrl"
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
                    })
                 .state('fmcgmenu.dashbrd', {
                     url: "/dashboard",
                     views: {
                         'menuContent': {
                             templateUrl: "partials/DashboardSummary.html",
                             controller: "dashbrdCtrl"
                         }
                     }
                 })
                    .state('fmcgmenu.outbox', {
                        url: "/outbox",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/outbox.html",
                                controller: "outboxCtrl"
                            }
                        }
                    })

                    .state('fmcgmenu.outboxView', {
                        url: "/outboxView?myChoice",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/outboxView.html",
                                controller: "outboxViewCtrl"
                            }
                        }
                    })
                    .state('fmcgmenu.addNew', {
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
                    .state('fmcgmenu.orderEdit', {
                        url: "/orderEdit",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/orderEdit.html",
                                controller: "orderEditCtrl"
                            }
                        }
                    })
                     .state('fmcgmenu.TourPlan', {
                         url: "/TourPlan",
                         views: {
                             'menuContent': {
                                 templateUrl: "partials/TourPlan.html",
                                 controller: "TourPlanCtrl"
                             }
                         }
                     })
                 .state('fmcgmenu.ExpenseEntry', {
                     url: "/ExpenseEntry",
                     views: {
                         'menuContent': {
                             templateUrl: "partials/ExpenseEntry.html",
                             controller: "ExpenseEntryCtrl"
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
                  .state('fmcgmenu.screen3s', {
                      url: "/screen3s",
                      views: {
                          'menuContent': {
                              templateUrl: "partials/screen-3s.html",
                              controller: "screen3sCtrl"
                          }
                      }
                  })
                   .state('fmcgmenu.screen3ss', {
                       url: "/screen3ss",
                       views: {
                           'menuContent': {
                               templateUrl: "partials/screen3ss.html",
                               controller: "screen3ssCtrl"
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
                   .state('fmcgmenu.brandsale', {
                       url: "/brandsale",
                       views: {
                           'menuContent': {
                               templateUrl: "partials/BrndSummary.html", //dcr.html
                               controller: "BrndSummary" //dcrData
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
                 .state('fmcgmenu.distibutorHunt', {
                     url: "/distibutorHunt",
                     views: {
                         'menuContent': {
                             templateUrl: "partials/distibutorHunt.html",
                             controller: "distibutorHuntCtrl"
                         }
                     }
                 })
                 .state('fmcgmenu.doortodoorView', {
                     url: "/doortodoorView",
                     views: {
                         'menuContent': {
                             templateUrl: "partials/distibutorHunt.html",
                             controller: "doortodoorViewCtrl"
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
                                templateUrl: "partials/MnSummary.html", //dcr.html
                                controller: "mnthSummary" //dcrData
                            }
                        }
                    })
                    .state('fmcgmenu.dcr1', {
                        url: "/dayReport",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/dayReport.html",
                                controller: "dayReport" //dcrData1
                            }
                        }
                    })
                    .state('fmcgmenu.distDayReport', {
                        url: "/distDayReport",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/distDayReport.html",
                                controller: "distDayReport" //dcrData1
                            }
                        }
                    })
                    .state('fmcgmenu.myPlan', {
                        url: "/myPlan",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/myTodayPlan.html",
                                controller: "myTodyPlCtrl"
                            }
                        }
                    })
                .state('fmcgmenu.addDoorToDoor', {
                    url: "/addDoorToDoor",
                    views: {
                        'menuContent': {
                            templateUrl: "partials/addDoorToDoor.html",
                            controller: "addDoorToDoorCtrl"
                        }
                    }
                })
                    .state('fmcgmenu.tpview', {
                        url: "/tpview",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/tpview.html",
                                controller: 'tpviewCtrl'
                            }
                        }
                    })
                    .state('fmcgmenu.tpviewdt', {
                        url: "/tpviewdt",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/TpViewDt.html",
                                controller: 'tpviewdtCtrl'
                            }
                        }
                    })
                .state('fmcgmenu.LostProducts', {
                    url: "/LostProducts",
                    views: {
                        'menuContent': {
                            templateUrl: "partials/LostProducts.html",
                            controller: "LostProducts"
                        }
                    }
                })
                   .state('fmcgmenu.LeaveForm', {
                       url: "/LeaveForm",
                       views: {
                           'menuContent': {
                               templateUrl: "partials/LeaveForm.html",
                               controller: "LeaveForm"
                           }
                       }
                   })
         .state('fmcgmenu.ViewLeave', {
             url: "/ViewLeave",
             views: {
                 'menuContent': {
                     templateUrl: "partials/ViewLeave.html",
                     controller: "ViewLeave"
                 }
             }
         })
         .state('fmcgmenu.ViewDCR', {
             url: "/ViewDCR",
             views: {
                 'menuContent': {
                     templateUrl: "partials/ViewDCR.html",
                     controller: "ViewDCR"
                 }
             }
         })
          .state('fmcgmenu.viewTPApproval', {
              url: "/viewTPApproval",
              views: {
                  'menuContent': {
                      templateUrl: "partials/viewTPApproval.html",
                      controller: "viewTPApproval"
                  }
              }
          })
      .state('fmcgmenu.Approvals', {
          url: "/Approvals",
          views: {
              'menuContent': {
                  templateUrl: "partials/Approvals.html",
                  controller: "ApprovalsCtrl"
              }
          }
      })
      .state('fmcgmenu.LeaveApproval', {
          url: "/LeaveApproval",
          views: {
              'menuContent': {
                  templateUrl: "partials/LeaveApproval.html",
                  controller: "LeaveApproval"
              }
          }
      })
         .state('fmcgmenu.DCRApproval', {
             url: "/DCRApproval",
             views: {
                 'menuContent': {
                     templateUrl: "partials/DCRApproval.html",
                     controller: "DCRApproval"
                 }
             }
         })
      .state('fmcgmenu.TPApproval', {
          url: "/TPApproval",
          views: {
              'menuContent': {
                  templateUrl: "partials/TPApproval.html",
                  controller: "TPApproval"
              }
          }
      })

                    .state('fmcgmenu.DayRemarks', {
                        url: "/DayRemarks",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/DayActRemks.html",
                                controller: "DyRmksCtrl"
                            }
                        }
                    })
                 .state('fmcgmenu.addDistributor', {
                     url: "/addDistributor",
                     views: {
                         'menuContent': {
                             templateUrl: "partials/addDistributor.html",
                             controller: "addDistributorCtrl"
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
                    })
                   .state('fmcgmenu.addSurvey', {
                       url: "/addSurvey",
                       views: {
                           'menuContent': {
                               templateUrl: "partials/addSurvey.html",
                               controller: "addSurveyCtrl"
                           }
                       }
                   })
                 .state('fmcgmenu.addRoute', {
                     url: "/addRoute",
                     views: {
                         'menuContent': {
                             templateUrl: "partials/addRoute.html",
                             controller: "addRouteCtrl"
                         }
                     }
                 })
                    .state('fmcgmenu.addChemist', {
                        url: "/addchemist",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/addData.html",
                                controller: "addChemistCtrl"
                            }
                        }
                    })
                    .state('fmcgmenu.geoTag', {
                        url: "/geoTag",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/geoTagList.html",
                                controller: "geoTagCtrl"
                            }
                        }
                    })
                    .state('fmcgmenu.stockUpdation', {
                        url: "/stockUpdation",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/stockUpdation.html",
                                controller: "stockUpdationCtrl"
                            }
                        }
                    })
                 .state('fmcgmenu.myAudio', {
                     url: "/myAudio",
                     views: {
                         'menuContent': {
                             templateUrl: "partials/myAudio.html",
                             controller: "myAudioCtrl"
                         }
                     }
                 })
                .state('fmcgmenu.viewAudioDet', {
                    url: "/viewAudioDet",
                    views: {
                        'menuContent': {
                            templateUrl: "partials/viewAudioDet.html",
                            controller: "viewAudioDetCtrl"
                        }
                    }
                })
                    .state('fmcgmenu.transCurrentStocks', {
                        url: "/transCurrentStocks",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/transCurrentStocks.html",
                                controller: "transCurrentStocksCtrl"
                            }
                        }
                    });
            $urlRouterProvider.otherwise("/sign-in");
        }).run(function ($rootScope, $state, $ionicLoading, $ionicPopup) {
            $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
                var allow = ['/addNew', '/screen1', '/screen2', '/screen3', '/screen4', '/screen5']
                if ($rootScope.hasData) {
                    if (allow.indexOf(toState.url) == -1) {

                        $rootScope.deleteRecord = true;
                        $rootScope.hasData = false;
                        $state.go(toState.name);

                    }

                }

            });
            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                $ionicLoading.hide();
            });
        })
        .controller('VacPgCtrl', ['$rootScope', '$scope', '$state', function ($rootScope, $scope, $state) {
            $scope.GotoLogin = function () {
                $state.go('signin')
            }
        }])
        .controller('SignInCtrl', ['$rootScope', '$scope', '$state', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.appVar = "v 4.15.715"

            $scope.callback = function () {
            };
            $scope.process = false;
            $scope.login = true;
            $scope.error = "";
            var flag = false, temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            //fmcgLocalStorage.createData("events", []);
            if (userData && userData['user'] && userData['user']['valid']) {
                $scope.user = {};
                $scope.SFDispName = userData["sfName"] + ' ( ' + userData['user']['name'] + ' )';
                $scope.user['name'] = userData['user']['name'];
                $scope.user['valid'] = userData['user']['valid'];
            }
            $scope.signIn = function (user) {
                var loginInfo = {};
                $scope.process = true;
                $scope.login = false;
                $scope.error = "";

                if (userData != null) {
                    var date2 = new Date();
                    var date1 = new Date();

                    date1.setTime(userData.lastLogin);
                    date1.setUTCHours(0, 0, 0, 0);
                    date2.setUTCHours(0, 0, 0, 0);

                    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                    if (user.name === userData.user.name && user.password === userData.user.password) {
                        flag = true;
                        $state.go('fmcgmenu.home');
                    }
                    else {
                        flag = false;
                    }
                }
                if (!flag) {
                    fmcgAPIservice.getPostData('POST', 'login&access=mobile', user).success(function (response) {
                        if (response.success) {
                            var loginData = {};
                            var dat = new Date();
                            dat.setUTCHours(0, 0, 0, 0);
                            loginData.lastLogin = dat.getTime();
                            loginData.activeDays = response.days;
                            loginData.sfName = response.sfName;
                            loginData.sfCode = response.sfCode;
                            loginData.curSFCode = response.sfCode;
                            loginData.desigCode = response.desigCode;
                            loginData.divisionCode = response.divisionCode;
                            loginData.callReport = response.call_report;
                            loginData.sms = response.sms;  //0= no need sms,. 1=send via whatsapp,. 2=send via sms
                            loginData.GEOTagNeed = response.GEOTagNeed;
                            loginData.DisRad = response.DisRad;
                            loginData.SFStat = response.SFStat;
                            loginData.CusOrder = response.CusOrder; //0=order by name. 1=walking order
                            loginData.SF_type = response.SF_type;
                            loginData.SFTPDate = response.SFTPDate;
                            loginData.AppTyp = response.AppTyp; // 0 = Pharma. 1 = FMCG
                            loginData.TBase = response.TBase; 	// 0 = Territory Base. 1 = Cluster Base
                            loginData.GeoChk = response.GeoChk;  // 0 = Location Base. 1 = Non Location Base
                            if (loginData.GEOTagNeed == 1)
                                loginData.GeoChk = 0;
                            loginData.closing = response.closing;
                            loginData.CollectedAmountSetup = response.CollectedAmount;
                            loginData.JointworkSetup = response.jointwork;

                            loginData.recv = response.recv;
                            loginData.ChmNeed = response.ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
                            loginData.StkNeed = response.StkNeed;   // 0 = Stockist Needed. 1 = Not Needed
                            loginData.UNLNeed = response.UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed
                            loginData.DrCap = response.DrCap; 	// Listed Doctor Caption
                            loginData.ChmCap = response.ChmCap;   // Chemist Caption
                            loginData.StkCap = response.StkCap;   // Stockist Caption
                            loginData.NLCap = response.NLCap;     // Unlisted DR. Caption
                            loginData.DrSmpQ = response.DrSmpQ; //Order value
                            loginData.DPNeed = response.DPNeed; 	// 0 = Doctor Product Neeed. 1 = Not Needed
                            loginData.DINeed = response.DINeed;       // 0 = Doctor Input Neeed. 1 = Not Needed
                            loginData.CPNeed = response.CPNeed; 	// 0 = Chemist Product Neeed. 1 = Not Needed
                            loginData.CINeed = response.CINeed;       // 0 = Chemist Input Neeed. 1 = Not Needed
                            loginData.SPNeed = response.SPNeed; 	// 0 = Stockist Product Neeed. 1 = Not Needed
                            loginData.SINeed = response.SINeed;       // 0 = Stockist Input Neeed. 1 = Not Needed
                            loginData.NPNeed = response.NPNeed; 	// 0 = UnListed Dr. Product Neeed. 1 = Not Needed
                            loginData.NINeed = response.NINeed;       // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
                            loginData.HlfNeed = response.HlfNeed;   // 0 = Halfday work Not Neeed. 1 >= Needed

                            loginData.DRxCap = response.DRxCap;     // Listed Doctor Rx Qty Caption
                            loginData.DSmpCap = response.DSmpCap;   // Listed Doctor Sample Qty Caption
                            loginData.CQCap = response.CQCap;       // Chemist Qty Caption
                            loginData.SQCap = response.SQCap;       // Stockist Qty Caption
                            loginData.NRxCap = response.NRxCap;     // UnListed Dr. Rx Qty Caption
                            loginData.NSmpCap = response.NSmpCap;   // UnListed Dr. Sample Qty Caption
                            loginData.State_Code = response.State_Code;
                            user['valid'] = true;
                            loginData.user = user;
                            window.localStorage.setItem("loginInfo", JSON.stringify(loginData));
                            flag = true;
                            if (loginData.GeoChk == 0)
                                window.startGPS();
                            if (response.SFStat != undefined && response.SFStat.toString() != '0') {
                                $state.go('vacScr');
                            } else {
                                $state.go('fmcgmenu.home');
                            }
                        }
                        else {
                            $scope.process = false;
                            $scope.login = true;
                            $scope.error = response.msg;
                        }
                    }).error(
                            function () {
                                Toast("connection failure...");
                                $scope.process = false;
                                $scope.login = true;
                            }
                    );
                }
                if (flag == true && diffDays > 0) {
                    var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
                    var dat = new Date();
                    dat.setUTCHours(0, 0, 0, 0);
                    loginInfo.lastLogin = dat.getTime();
                    window.localStorage.setItem("loginInfo", JSON.stringify(loginInfo));
                    window.localStorage.removeItem("mypln");

                    window.localStorage.removeItem("MyDyRmks");
                    window.localStorage.removeItem("MyDyRmksQ");
                }
            };
        }])
        .controller('MainCtrl', ['$rootScope', '$scope', '$state', '$ionicModal', '$ionicScrollDelegate', '$ionicPopup', '$location', 'fmcgLocalStorage', 'fmcgAPIservice', '$ionicSideMenuDelegate', 'geolocation', '$interval', 'notification', '$timeout', '$ionicLoading', '$filter', function ($rootScope, $scope, $state, $ionicModal, $ionicScrollDelegate, $ionicPopup, $location, fmcgLocalStorage, fmcgAPIservice, $ionicSideMenuDelegate, geolocation, $interval, notification, $timeout, $ionicLoading, $filter) {
            $interval(checkInterval, 300000);   //1 minute 60000 millisec
            function checkInterval() {
                var date = new Date();
                $scope.currentdate = $filter('date')(new Date(), 'yyyy-M-dd H:mm:ss');
                $scope.locDetails = fmcgLocalStorage.getData("LocationDetails") || [];
                // _currentLocation.Latitude = '13.0460838';
                // _currentLocation.Longitude = '80.22289099999999';
                if (_currentLocation.Latitude) {
                    $scope.locDetails.push({
                        "lattitude": _currentLocation.Latitude, "date": $scope.currentdate, "longitude": _currentLocation.Longitude
                    });
                }
                fmcgLocalStorage.createData("LocationDetails", $scope.locDetails);
                $scope.onLine = isReachable();
                // if ($scope.onLine && navigator.onLine)
                if ($scope.onLine) {
                    $scope.locDetails = fmcgLocalStorage.getData("LocationDetails") || [];
                    if ($scope.locDetails.length != 0) {
                        fmcgAPIservice.addMAData('POST', 'dcr/save', "11", $scope.locDetails).success(function (response) {
                            if (response.success) {
                                $scope.locDetails = [];
                                localStorage.removeItem("LocationDetails");
                                fmcgLocalStorage.createData("LocationDetails", $scope.locDetails);
                            }
                        }).error(function () {
                            Toast("No Internet Connection!.");
                        });
                    }

                }
            }
            $scope.$on('finalSubmit', function (evnt) {
                $scope.submit();
            });
            $scope.submit = function () {


                whatsupval = "";
                prod = "";
                if ($scope.fmcgData.productSelectedList != undefined) {
                    for (i = 0; i < $scope.fmcgData.productSelectedList.length; i++) {
                        prod = prod + $scope.fmcgData.productSelectedList[i]['product_Nm'] + "(" + $scope.fmcgData.productSelectedList[i]['rx_qty'] + ")" + "(" + $scope.fmcgData.productSelectedList[i]['sample_qty'] + ") ," + "\n"
                    }
                    whatsupval = prod + "\n Total Order Value: " + $scope.fmcgData.value

                }
                function sms() {
                    mobileNumber = $scope.fmcgData.doctor.Mobile_Number;
                    if (mobileNumber != null && $scope.fmcgData.productSelectedList != undefined && $scope.sms == 2) {
                        fmcgAPIservice.sendSMS(mobileNumber, whatsupval, 'POST').success(function (response) {
                            // console.log(response);
                        });
                    }
                }
                if ($scope.fmcgData.customer.selected.id == "3" && $scope.fmcgData.customer.selected != undefined) {
                    $scope.fmcgData.cluster.selected.id = $scope.fmcgData.cluster.name;
                }
                function _clrData(scope) {
                    var value = scope.fmcgData;
                    for (key in value) {
                        if (key) {
                            if (key != 'jontWorkSelectedList')
                                scope.fmcgData[key] = undefined;
                        }
                    }
                }
                function _savLocal(scope, stat) {
                    scope.fmcgData.toBeSync = true;
                    fmcgLocalStorage.addData('saveLater', scope.fmcgData);
                    _clrData(scope);
                    stat.go('fmcgmenu.home');
                    Toast("No Connection! Call Saved Locally", true);
                };
                

                $rootScope.hasData = false;
                $rootScope.hasEditData = false;
                if ($scope.fmcgData.worktype.selected.FWFlg == "F") {
                    if ($scope.reportTemplates.length > 0) {
                        if ($scope.fmcgData.rx_t == undefined && $scope.fmcgData.customer.selected.id == "1") {
                            Toast('Select Call Feedback....');
                            return false;
                        }
                    }
                    if ($scope.nonreportTemplates.length > 0) {
                        if (($scope.fmcgData.remarks == undefined || $scope.fmcgData.remarks == '') && $scope.fmcgData.customer.selected.id == "1") {
                            // Toast('Select the Template ( or ) Enter the Remarks....');
                            // return false;
                        }
                    }
                }
                if ($scope.photosList.length > 0) {
                    for (i = 0; i < $scope.photosList.length; i++) {
                        var dataEVNT = $scope.photosList[i];
                        var imgUrl = dataEVNT.imgData;
                        $scope.savingData.EVNT = true;
                        var options = new FileUploadOptions();
                        options.fileKey = "imgfile";
                        options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                        options.mimeType = "image/jpeg";
                        options.chunkedMode = false;
                        var uplUrl = baseURL + 'imgupload&sf_code=' + $scope.sfCode;
                        var ft = new FileTransfer();
                        ft.upload(imgUrl, uplUrl,
                                function (response) {
                                    $scope.savingData.EVNT = false;
                                },
                                function () {
                                    $scope.savingData.EVNT = false;
                                    fmcgLocalStorage.addData("events", dataEVNT);
                                }, options);
                        //fmcgLocalStorage.addData("events", $scope.photosList);
                    }
                }
                $scope.$emit('ClearEvents');
                if ($scope.fmcgData.arc) {
                    var arc = $scope.fmcgData.arc;
                    var amc = $scope.fmcgData.amc;

                    $ionicLoading.show({ template: 'Call Submitting. Please wait...' });
                    fmcgAPIservice.saveDCRData('POST', 'dcr/updateEntry&arc=' + arc + '&amc=' + amc, $scope.fmcgData, true)
                            .success(function (response) {
                                _clrData($scope);
                                Toast("Call updated SuccessFully");
                                $state.go('fmcgmenu.home');
                                $ionicLoading.hide();
                                //  if ($scope.sms == 2)
                                sms();
                                if ($scope.sms == 1 && $scope.fmcgData.productSelectedList != undefined)
                                    window.plugins.socialsharing.shareViaWhatsApp(whatsupval, null, null, function () { }, function (errormsg) { alert(errormsg) })

                            }).error(function () {
                                _savLocal($scope, $state);
                                $state.go('fmcgmenu.home');
                                $ionicLoading.hide();
                            });

                } else {
                    $ionicLoading.show({ template: 'Call Submitting. Please wait...' });


                    var products = $scope.fmcgData.productSelectedList;
                    var values = 0;
                    for (var key in products) {
                        values = +products[key]['sample_qty'] + +values;
                    }
                    $scope.fmcgData.value = values;
                   
                    fmcgAPIservice.saveDCRData('POST', 'dcr/save', $scope.fmcgData, false).success(function (response) {
                        if (!response['success'] && response['type'] == 2) {
                            $state.go('fmcgmenu.home');
                            Toast(response['msg']);
                            $ionicLoading.hide();
                            if ($scope.sms == 1 && $scope.fmcgData.productSelectedList != undefined)
                                window.plugins.socialsharing.shareViaWhatsApp(whatsupval, null, null, function () { }, function (errormsg) { alert(errormsg) })

                        }

                        else if (!response['success'] && response['type'] == 1) {
                            $scope.showConfirm = function () {
                                var confirmPopup = $ionicPopup.confirm({
                                    title: 'Warning !',
                                    template: response['msg']
                                });
                                confirmPopup.then(function (res) {
                                    if (res) {
                                        $ionicLoading.show({ template: 'Call Submitting. Please wait...' });
                                        fmcgAPIservice.updateDCRData('POST', 'dcr/save&replace', response['data'], false).success(function (response) {
                                            if (!response['success'])
                                                Toast(response['msg'], true);
                                            else {
                                                Toast("call Updated Successfully");
                                                if ($scope.sms == 1 && $scope.fmcgData.productSelectedList != undefined)
                                                    window.plugins.socialsharing.shareViaWhatsApp(whatsupval, null, null, function () { }, function (errormsg) { alert(errormsg) })

                                            }
                                            _clrData($scope);
                                            $state.go('fmcgmenu.home');
                                            $ionicLoading.hide();
                                        })
                                                .error(function () {
                                                    Toast("call Updation Failed...");
                                                    _clrData($scope);
                                                    $state.go('fmcgmenu.home');
                                                    $ionicLoading.hide();
                                                });
                                    } else {
                                        _clrData($scope);
                                        $state.go('fmcgmenu.home');
                                        $ionicLoading.hide();
                                    }
                                });
                            };
                            $ionicLoading.hide();
                            $scope.showConfirm();
                            $scope.fmcgData.value = 0;
                        }
                        else {
                            if (!response['success'])
                                Toast(response['msg'], true);
                            else {
                                Toast("call Submited Successfully");
                                sms();

                                if ($scope.sms == 1 && $scope.fmcgData.productSelectedList != undefined)
                                    window.plugins.socialsharing.shareViaWhatsApp(whatsupval, null, null, function () { }, function (errormsg) { alert(errormsg) })

                            }
                            _clrData($scope);
                            $state.go('fmcgmenu.home');
                            $ionicLoading.hide();
                        }
                    })
                            .error(function () {
                                _savLocal($scope, $state);
                                $state.go('fmcgmenu.home');
                                $ionicLoading.hide();
                            });
                }

            };
            Exitflag = false;
            var temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            var lastMsgShown = window.localStorage.getItem("msg_shown") || undefined;
            $scope.view_MR = 0;
            $scope.view_STOCKIST = 0;
            if (userData.desigCode.toLowerCase() == 'mr')
                $scope.view_MR = 1;
            if (userData.desigCode.toLowerCase() == 'stockist')
                $scope.view_STOCKIST = 1;
            //Setup variables Value Assign
            $scope.cDataID = '_' + userData.curSFCode;
            $scope.cMTPDId = '_' + userData.curSFCode;
            $scope.PlanChk = 0;
            $scope.AppEnver = ".Net";
            $scope.CusOrder = userData.CusOrder; //0=order by name. 1=walking order
            $scope.SF_type = userData.SF_type;
            $scope.sfCode = userData.sfCode;
            $scope.CollectedAmountSetup = userData.CollectedAmountSetup;
            $scope.CollectedAmountSetup = 1;
            $scope.JointworkSetup = userData.JointworkSetup;
            $scope.JointworkSetup = 1
            $scope.desigCode = userData.desigCode;
            $scope.sms = userData.sms;  //0= no need sms,. 1=send via whatsapp,. 2=send via sms
            $scope.DrSmpQ = userData.DrSmpQ;
            $scope.GEOTagNeed = userData.GEOTagNeed;
            $scope.DisRad = userData.DisRad;
            $scope.SFTPDate = userData.SFTPDate;
            $scope.AppTyp = userData.AppTyp;    // 0 = Pharma. 1 = FMCG
            $scope.TBase = userData.TBase;      // 0 = Territory Base. 1 = Cluster Base
            $scope.GeoChk = userData.GeoChk;    // 0 = Location Base. 1 = Non Location Base
            // $scope.GeoChk = 1;
            $scope.ChmNeed = userData.ChmNeed;  // 0 = Chemist Needed. 1 = Not Needed
            $scope.StkNeed = userData.StkNeed;  // 0 = Stockist Needed. 1 = Not Needed
            $scope.UNLNeed = userData.UNLNeed;  // 0 = Unlisted Needed. 1 = Not Needed
            $scope.DrCap = userData.DrCap;      // Listed Doctor Caption
            $scope.ChmCap = userData.ChmCap;    // Chemist Caption
            $scope.StkCap = userData.StkCap;    // Stockist Caption
            $scope.NLCap = userData.NLCap;      // Unlisted DR. Caption

            $scope.closing = userData.closing;
            $scope.recv = userData.recv;

            $scope.DPNeed = userData.DPNeed;    // 0 = Doctor Product Neeed. 1 = Not Needed
            $scope.DINeed = userData.DINeed;    // 0 = Doctor Input Neeed. 1 = Not Needed
            $scope.CPNeed = userData.CPNeed;    // 0 = Chemist Product Neeed. 1 = Not Needed
            $scope.CINeed = userData.CINeed;    // 0 = Chemist Input Neeed. 1 = Not Needed
            $scope.SPNeed = userData.SPNeed;    // 0 = Stockist Product Neeed. 1 = Not Needed
            $scope.SINeed = userData.SINeed;    // 0 = Stockist Input Neeed. 1 = Not Needed
            $scope.NPNeed = userData.NPNeed;    // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
            $scope.NINeed = userData.NINeed;    // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
            $scope.HlfNeed = userData.HlfNeed;  // 0 = Halfday work Not Neeed. 1 >= Needed

            $scope.DRxCap = userData.DRxCap;    // Listed Doctor Rx Qty Caption
            $scope.DSmpCap = userData.DSmpCap;  // Listed Doctor Sample Qty Caption
            $scope.CQCap = userData.CQCap;      // Chemist Qty Caption
            $scope.SQCap = userData.SQCap;      // Stockist Qty Caption
            $scope.NRxCap = userData.NRxCap;    // UnListed Dr. Rx Qty Caption
            $scope.NSmpCap = userData.NSmpCap;  // UnListed Dr. Sample Qty Caption
            $scope.SFStat = userData.SFStat;
            $scope.State_Code = userData.State_Code;
            if (userData.SFStat != undefined && userData.SFStat.toString() != '0') {
                $state.go('vacScr');
            }
            loadSetups = function () {

                var loginData = JSON.parse(localStorage.getItem("loginInfo"));

                var date2 = new Date();
                var date1 = new Date();

                date1.setTime(loginData.lastLogin);
                date1.setUTCHours(0, 0, 0, 0);
                date2.setUTCHours(0, 0, 0, 0);

                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                if (diffDays > 0) {
                    var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
                    var dat = new Date();
                    dat.setUTCHours(0, 0, 0, 0);
                    loginInfo.lastLogin = dat.getTime();
                    window.localStorage.setItem("loginInfo", JSON.stringify(loginInfo));
                    window.localStorage.removeItem("mypln");
                    window.localStorage.removeItem("MyDyRmks");
                    window.localStorage.removeItem("MyDyRmksQ");
                    if ($scope.view_STOCKIST == 0) {
                        $state.go('fmcgmenu.myPlan')
                        $scope.PlanChk = 1;
                    }
                }


                fmcgAPIservice.getDataList('POST', 'get/setup', [])
                        .success(function (response) {
                            var loginData = JSON.parse(localStorage.getItem("loginInfo"));
                            loginData.SFStat = response[0].SFStat;

                            loginData.GEOTagNeed = response[0].GEOTagNeed;
                            loginData.DisRad = response[0].DisRad;
                            loginData.TBase = response[0].TBase; 	 // 0 = Territory Base. 1 = Cluster Base
                            loginData.GeoChk = response[0].GeoChk;   // 0 = Location Base. 1 = Non Location Base
                            if (loginData.GEOTagNeed == 1)
                                loginData.GeoChk = 0;
                            loginData.CusOrder = response[0].CusOrder; //0=order by name. 1=walking order
                            loginData.SF_type = response[0].SF_type;
                            loginData.SFTPDate = response[0].SFTPDate;
                            loginData.sms = response[0].sms;  //0= no need sms,. 1=send via whatsapp,. 2=send via sms
                            loginData.DrSmpQ = response[0].DrSmpQ;
                            loginData.ChmNeed = response[0].ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
                            loginData.StkNeed = response[0].StkNeed; // 0 = Stockist Needed. 1 = Not Needed
                            loginData.UNLNeed = response[0].UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed
                            loginData.DrCap = response[0].DrCap; 	 // Listed Doctor Caption
                            loginData.ChmCap = response[0].ChmCap;   // Chemist Caption
                            loginData.StkCap = response[0].StkCap;   // Stockist Caption
                            loginData.NLCap = response[0].NLCap;     // Unlisted DR. Caption
                            loginData.CollectedAmountSetup = response[0].CollectedAmount;
                            loginData.JointworkSetup = response[0].jointwork;
                            loginData.closing = response[0].closing;
                            loginData.recv = response[0].recv;

                            loginData.DPNeed = response[0].DPNeed; 	 // 0 = Doctor Product Neeed. 1 = Not Needed
                            loginData.DINeed = response[0].DINeed;   // 0 = Doctor Input Neeed. 1 = Not Needed
                            loginData.CPNeed = response[0].CPNeed; 	 // 0 = Chemist Product Neeed. 1 = Not Needed
                            loginData.CINeed = response[0].CINeed;   // 0 = Chemist Input Neeed. 1 = Not Needed
                            loginData.SPNeed = response[0].SPNeed; 	 // 0 = Stockist Product Neeed. 1 = Not Needed
                            loginData.SINeed = response[0].SINeed;   // 0 = Stockist Input Neeed. 1 = Not Needed
                            loginData.NPNeed = response[0].NPNeed; 	 // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
                            loginData.NINeed = response[0].NINeed;   // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
                            loginData.HlfNeed = (loginData.desigCode == 'MR') ? response[0].MRHlfDy : response[0].MGRHlfDy;   // 0 = Halfday work Not Neeed. 1 >= Needed

                            loginData.DRxCap = response[0].DrRxQCap;     // Listed Doctor Rx Qty Caption
                            loginData.DSmpCap = response[0].DrSmpQCap;   // Listed Doctor Sample Qty Caption
                            loginData.CQCap = response[0].ChmQCap;       // Chemist Qty Caption
                            loginData.SQCap = response[0].StkQCap;       // Stockist Qty Caption
                            loginData.NRxCap = response[0].NLRxQCap;     // UnListed Dr. Rx Qty Caption
                            loginData.NSmpCap = response[0].NLSmpQCap;   // UnListed Dr. Sample Qty Caption
                            loginData.State_Code = response[0].State_Code;
                            window.localStorage.setItem("loginInfo", JSON.stringify(loginData));

                            $scope.SFStat = loginData.SFStat;

                            $scope.GEOTagNeed = loginData.GEOTagNeed;
                            $scope.SFTPDate = loginData.SFTPDate;
                            $scope.DisRad = loginData.DisRad;
                            $scope.TBase = loginData.TBase;      // 0 = Territory Base. 1 = Cluster Base
                            $scope.GeoChk = loginData.GeoChk;    // 0 = Location Base. 1 = Non Location Base
                            $scope.ChmNeed = loginData.ChmNeed;  // 0 = Chemist Needed. 1 = Not Needed
                            // $scope.GeoChk = 1;
                            $scope.StkNeed = loginData.StkNeed;  // 0 = Stockist Needed. 1 = Not Needed
                            $scope.UNLNeed = loginData.UNLNeed;  // 0 = Unlisted Needed. 1 = Not Needed
                            $scope.DrCap = loginData.DrCap;      // Listed Doctor Caption
                            $scope.ChmCap = loginData.ChmCap;    // Chemist Caption
                            $scope.StkCap = loginData.StkCap;    // Stockist Caption
                            $scope.NLCap = loginData.NLCap;      // Unlisted DR. Caption
                            $scope.DrSmpQ = loginData.DrSmpQ;
                            $scope.CusOrder = loginData.CusOrder;   //0=order by name. 1=walking order
                            $scope.SF_type = loginData.SF_type;
                            $scope.DPNeed = loginData.DPNeed;    // 0 = Doctor Product Neeed. 1 = Not Needed
                            $scope.DINeed = loginData.DINeed;    // 0 = Doctor Input Neeed. 1 = Not Needed
                            $scope.CPNeed = loginData.CPNeed;    // 0 = Chemist Product Neeed. 1 = Not Needed
                            $scope.CINeed = loginData.CINeed;    // 0 = Chemist Input Neeed. 1 = Not Needed
                            $scope.SPNeed = loginData.SPNeed;    // 0 = Stockist Product Neeed. 1 = Not Needed
                            $scope.SINeed = loginData.SINeed;    // 0 = Stockist Input Neeed. 1 = Not Needed
                            $scope.NPNeed = loginData.NPNeed;    // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
                            $scope.NINeed = loginData.NINeed;    // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
                            $scope.HlfNeed = loginData.HlfNeed;  // 0 = Halfday work Not Neeed. 1 >= Needed
                            $scope.CollectedAmountSetup = loginData.CollectedAmountSetup;  //0 needed
                            $scope.CollectedAmountSetup = 1;
                            $scope.JointworkSetup = loginData.JointworkSetup; //0 needed
                            $scope.JointworkSetup = 1;
                            $scope.closing = loginData.closing;
                            $scope.recv = loginData.recv;


                            $scope.DRxCap = loginData.DRxCap;    // Listed Doctor Rx Qty Caption
                            $scope.DSmpCap = loginData.DSmpCap;  // Listed Doctor Sample Qty Caption
                            $scope.CQCap = loginData.CQCap;      // Chemist Qty Caption
                            $scope.SQCap = loginData.SQCap;      // Stockist Qty Caption
                            $scope.NRxCap = loginData.NRxCap;    // UnListed Dr. Rx Qty Caption
                            $scope.NSmpCap = loginData.NSmpCap;  // UnListed Dr. Sample Qty Caption
                            $scope.State_Code = loginData.State_Code;
                            if (loginData.SFStat != undefined && loginData.SFStat.toString() != '0') {
                                $state.go('vacScr');
                            }
                        })
                $timeout(function () {
                    loadSetups();
                }, 1000 * (3600 * 2));
                $scope.$parent.fieldforceName = loginData.sfName;
                var eles = document.querySelectorAll(".fieldforce-name");
                for (var i = 0; i < eles.length; i++) {
                    eles[i].innerHTML = $scope.$parent.fieldforceName;
                }
            }
            $timeout(function () {
                loadSetups();
            }, 100);
            $scope.view_AddUMas = 0;
            if (userData.desigCode.toLowerCase() != 'mr') {
                $scope.view_AddUMas = 1;
                $scope.view_AddMas = 1;
            }
            $scope.loadr = new Array();
            $scope.ErrFnd = new Array();

            $scope.showFlash = function () {
                if (userData.callReport && userData.callReport.length > 0)
                    $ionicPopup.show({
                        title: 'Notification',
                        content: userData.callReport,
                        scope: $scope,
                        buttons: [{
                            text: 'Close', type: 'button-assertive', onTap: function (e) {
                                return true;
                            }
                        }, ]
                    }).then(function (res) {

                    }, function (err) {

                    }, function (popup) {
                        // If you need to access the popup directly, do it in the notify method
                        // This is also where you can programatically close the popup:
                        // popup.close();
                    });
            }
            if (!lastMsgShown) {
                $scope.showFlash();
                var obj = {};
                var d = new Date();
                obj.date = d.getTime();
                window.localStorage.setItem("msg_shown", JSON.stringify(obj));

            } else {
                var data = JSON.parse(lastMsgShown);
                var date2 = new Date();
                var date1 = new Date();
                date1.setTime(data.date);
                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                if (diffDays > parseInt(1)) {
                    $scope.showFlash();
                }
            }
            $scope.allcompetitors = fmcgLocalStorage.getData("competitor") || [];
            $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
            $scope.LeaveTypes = fmcgLocalStorage.getData("LeaveTypes") || [];
            $scope.Folders = fmcgLocalStorage.getData("folders") || [];
            $scope.SubFolders = fmcgLocalStorage.getData("subfolders") || [];
            $scope.mas_dsm = fmcgLocalStorage.getData("mas_dsm") || [];
            $scope.mailsStaff = fmcgLocalStorage.getData("mailsSF") || [];

            $scope.myTpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
            $scope.clusters = fmcgLocalStorage.getData("town_master" + $scope.cDataID) || [];

            $scope.stockists = fmcgLocalStorage.getData("stockist_master" + $scope.cDataID) || [];
            $scope.chemists = fmcgLocalStorage.getData("chemist_master" + $scope.cDataID) || [];
            $scope.doctors = fmcgLocalStorage.getData("doctor_master" + $scope.cDataID) || [];
            $scope.uldoctors = fmcgLocalStorage.getData("unlisted_doctor_master" + $scope.cDataID) || [];
            $scope.jointworks = fmcgLocalStorage.getData("salesforce_master" + $scope.cDataID) || [];

            $scope.categorys = fmcgLocalStorage.getData("Doctor_Category") || [];
            $scope.specialitys = fmcgLocalStorage.getData("Doctor_Specialty") || [];
            $scope.class_master = fmcgLocalStorage.getData("class_master") || [];
            $scope.qulifications = fmcgLocalStorage.getData("Qualifications") || [];
            $scope.brands = fmcgLocalStorage.getData("product_master") || [];
            $scope.subordinates = fmcgLocalStorage.getData("subordinate_master") || [];
            $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
            $scope.ProdByCat = [];
            $scope.ProdCategory = fmcgLocalStorage.getData("category_master") || [];
            $scope.products = fmcgLocalStorage.getData("product_master") || [];
            $scope.Product_State_Rates = fmcgLocalStorage.getData("Product_State_Rates") || [];
            $scope.districts = fmcgLocalStorage.getData("districts") || [];
            $scope.gifts = fmcgLocalStorage.getData("gift_master") || [];
            $scope.allsubs = fmcgLocalStorage.getData("subordinate") || [];
            $scope.allstockists = fmcgLocalStorage.getData("stockist_master" + $scope.cDataID) || [];
            $scope.reportTemplates = fmcgLocalStorage.getData("report_template_master") || [];
            $scope.nonreportTemplates = fmcgLocalStorage.getData("nonreport_template_master") || [];
            $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
            $scope.Tour_Plan = fmcgLocalStorage.getData("Tour_Plan") || [];

            $scope.lcnt = 0;

            $scope.loadDatas = function (cMod, CDId) {
                if (!CDId)
                    CDId = $scope.cDataID;
                $scope.allcompetitors = fmcgLocalStorage.getData("competitor") || [];
                $scope.myTpTwns = fmcgLocalStorage.getData("town_master" + CDId) || [];
                $scope.clusters = fmcgLocalStorage.getData("town_master" + CDId) || [];
                $scope.stockists = fmcgLocalStorage.getData("stockist_master" + CDId) || [];
                $scope.chemists = fmcgLocalStorage.getData("chemist_master" + CDId) || [];
                $scope.doctors = fmcgLocalStorage.getData("doctor_master" + CDId) || [];
                $scope.uldoctors = fmcgLocalStorage.getData("unlisted_doctor_master" + CDId) || [];
                $scope.jointworks = fmcgLocalStorage.getData("salesforce_master" + CDId) || [];
                $scope.mas_dsm = fmcgLocalStorage.getData("mas_dsm") || [];
                $scope.categorys = fmcgLocalStorage.getData("Doctor_Category") || [];
                $scope.specialitys = fmcgLocalStorage.getData("Doctor_Specialty") || [];
                $scope.class_master = fmcgLocalStorage.getData("class_master") || [];
                $scope.qulifications = fmcgLocalStorage.getData("Qualifications") || [];
                $scope.ProdCategory = fmcgLocalStorage.getData("category_master") || [];
                $scope.brands = fmcgLocalStorage.getData("product_master") || [];
                $scope.subordinates = fmcgLocalStorage.getData("subordinate_master") || [];
                $scope.allsubs = fmcgLocalStorage.getData("subordinate") || [];
                $scope.allstockists = fmcgLocalStorage.getData("stockist_master" + $scope.cDataID) || [];

                $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
                $scope.products = fmcgLocalStorage.getData("product_master") || [];
                $scope.Product_State_Rates = fmcgLocalStorage.getData("Product_State_Rates") || [];
                $scope.districts = fmcgLocalStorage.getData("districts") || [];
                $scope.gifts = fmcgLocalStorage.getData("gift_master") || [];
                $scope.reportTemplates = fmcgLocalStorage.getData("report_template_master") || [];
                $scope.nonreportTemplates = fmcgLocalStorage.getData("nonreport_template_master") || [];
                $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
                $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                $scope.LeaveTypes = fmcgLocalStorage.getData("LeaveTypes") || [];
                $scope.Folders = fmcgLocalStorage.getData("folders") || [];
                $scope.SubFolders = fmcgLocalStorage.getData("subfolders") || [];

                $scope.mailsStaff = fmcgLocalStorage.getData("mailsSF") || [];

                $scope.Tour_Plan = fmcgLocalStorage.getData("Tour_Plan") || [];
                if ($scope.view_STOCKIST == 0) {

                    if ($scope.Myplns.length == 0) {
                        if (cMod == true) {
                            $state.go('fmcgmenu.myPlan')
                            $scope.PlanChk = 1;
                        }
                    } else {
                        if (cMod == true)
                            $state.go('fmcgmenu.home');
                    }
                }
                else
                    $state.go('fmcgmenu.home');
                $scope.lcnt = 0;
                $ionicLoading.hide();
            }

            $scope.clearIdividual = function (value, total, CDId, cMod) {
                if (!cMod)
                    cMod = false;
                if (!CDId)
                    CDId = $scope.cDataID;

                var temp = window.localStorage.getItem("loginInfo");
                var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
                $scope.loadr[value] = 1;
                $scope.ErrFnd[value] = 0;

                ReldDta = function (aid) {
                    $scope.lcnt++;
                    $scope.loadr[aid] = 0;
                    if ($scope.lcnt == total) {
                        if ($scope.DataLoaded == false) {
                            Toast('No Internet Connection');
                            $scope.DataLoaded = true;
                        }
                        $scope.loadDatas(cMod, CDId);
                    }
                }
                switch (value) {
                    case 0:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwDoctor_Master_APP", '["doctor_code as id", "doctor_name as name","town_code","town_name","lat","long","addrs","ListedDr_Address1","ListedDr_Sl_No","Mobile_Number","horeca"]', , '["isnull(Doctor_Active_flag,0)=0"]'], CDId.replace(/_/g, ''))
                                .success(function (response) {
                                    window.localStorage.removeItem("doctor_master" + CDId);
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("doctor_master" + CDId, response);
                                    ReldDta(0);
                                })
                                .error(function () {
                                    $scope.ErrFnd[0] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(0);
                                });
                        break;
                    case 1:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwChemists_Master_APP", '["chemists_code as id", "chemists_name as name","town_code","town_name"]', , '["isnull(Chemist_Status,0)=0"]'], CDId.replace(/_/g, ''))
                                .success(function (response) {
                                    window.localStorage.removeItem("chemist_master" + CDId);
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("chemist_master" + CDId, response);
                                    ReldDta(1);
                                })
                                .error(function () {
                                    $scope.ErrFnd[1] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(1);
                                });
                        break;
                    case 2:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwstockiest_Master_APP", '["distributor_code as id", "stockiest_name as name","town_code","town_name"]', , '["isnull(Stockist_Status,0)=0"]'], CDId.replace(/_/g, ''))
                                .success(function (response) {
                                    window.localStorage.removeItem("stockist_master" + CDId);
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("stockist_master" + CDId, response);
                                    ReldDta(2);
                                })
                                .error(function () {
                                    $scope.ErrFnd[2] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(2);
                                });
                        break;
                    case 3:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwunlisted_doctor_master_APP", '["unlisted_doctor_code as id", "unlisted_doctor_name as name","town_code","town_name"]', , '["isnull(unlisted_activation_flag,0)=0"]'], CDId.replace(/_/g, ''))
                                .success(function (response) {
                                    window.localStorage.removeItem("unlisted_doctor_master" + CDId);
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("unlisted_doctor_master" + CDId, response);
                                    ReldDta(3);
                                })
                                .error(function () {
                                    $scope.ErrFnd[3] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(3);
                                });
                        break;
                    case 4:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype", '["type_code as id", "Wtype as name"]', , '["isnull(Active_flag,0)=0"]', , , , 0]).success(function (response) {
                                window.localStorage.removeItem("mas_worktype");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("mas_worktype", response);
                                ReldDta(4);
                            })
                                    .error(function () {
                                        $scope.ErrFnd[4] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(4);
                                    });
                        } else {
                            ReldDta(4);
                        }
                        break;
                    case 5:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["product_master", '["product_code as id", "product_name as name", "Product_Sl_No as pSlNo", "Product_Category cateid","product_netwt"]', , '["isnull(Product_DeActivation_Flag,0)=0"]', , , , 0])
                                    .success(function (response) {
                                        window.localStorage.removeItem("product_master");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("product_master", response);
                                        ReldDta(5);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[5] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(5);
                                    });
                        } else {
                            ReldDta(5);
                        }
                        break;
                    case 6:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["gift_master", '["gift_code as id", "gift_name as name"]', , '["isnull(Gift_DeActivate_Flag,0)=0"]', , , , 0])
                                    .success(function (response) {
                                        window.localStorage.removeItem("gift_master");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("gift_master", response);
                                        ReldDta(6);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[6] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(6);
                                    });
                        } else {
                            ReldDta(6);
                        }
                        break;
                    case 7:
                        fmcgAPIservice.getDataList('POST', 'get/jointwork', ["salesforce_master", '["sf_code as id", "sf_name as name"]'], CDId.replace(/_/g, ''))
                                .success(function (response) {
                                    window.localStorage.removeItem("salesforce_master" + CDId);
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("salesforce_master" + CDId, response);
                                    ReldDta(7);
                                })
                                .error(function () {
                                    $scope.ErrFnd[7] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(7);
                                });
                        break;
                    case 8:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwTown_Master_APP", '["town_code as id", "town_name as name","target","min_prod","field_code","distributor_code as stockist_code"]', , '["isnull(Town_Activation_Flag,0)=0"]', , , , , , ], CDId.replace(/_/g, ''))
                                .success(function (response) {
                                    window.localStorage.removeItem("town_master" + CDId);
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("town_master" + CDId, response);
                                    ReldDta(8);
                                })
                                .error(function () {
                                    $scope.ErrFnd[8] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(8);
                                });
                        break;
                    case 9:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["vwFeedTemplate", '["id as id", "content as name"]', , '["isnull(ActFlag,0)=0"]', , , , 0])
                                    .success(function (response) {
                                        window.localStorage.removeItem("report_template_master");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("report_template_master", response);
                                        ReldDta(9);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[9] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(9);
                                    });
                        } else {
                            ReldDta(9);
                        }
                        break;
                    case 10:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["vwRmksTemplate", '["id as id", "content as name"]', , '["isnull(ActFlag,0)=0"]', , , , 0])
                                    .success(function (response) {
                                        window.localStorage.removeItem("nonreport_template_master");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("nonreport_template_master", response);
                                        ReldDta(10);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[10] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(10);
                                    });
                        } else {
                            ReldDta(10);
                        }
                        break;
                    case 11:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype", '["type_code as id", "Wtype as name"]', , '["isnull(Active_flag,0)=0","isnull(HalfDyNeed,0)=1"]', , , , 0])
                                    .success(function (response) {
                                        window.localStorage.removeItem("halfdayworks");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("halfdayworks", response);
                                        ReldDta(11);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[11] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(11);
                                    });
                        } else {
                            ReldDta(11);
                        }
                        break;
                    case 12:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["vwMyDayPlan", '["worktype","FWFlg","sf_member_code as subordinateid","cluster as clusterid","ClstrName","remarks","stockist as stockistid","worked_with_code","worked_with_name"]', , ])
                                    .success(function (response) {
                                        window.localStorage.removeItem("mypln");

                                        $scope.fmcgData['jontWorkSelectedList'] = [];
                                        if (response.length != 0) {
                                            if (response[0].worked_with_code != "" && response[0].worked_with_code != undefined) {
                                                var response2 = response[0].worked_with_code.split("$$");
                                                var jw = response[0].worked_with_name.split(",");
                                                for (var m = 0, leg = response2.length; m < leg; m++) {
                                                    $scope.fmcgData['jontWorkSelectedList'] = $scope.fmcgData['jontWorkSelectedList'] || [];
                                                    var pTemp = {};
                                                    pTemp.jointwork = response2[m].toString();
                                                    pTemp.jointworkname = jw[m].toString();
                                                    if (pTemp.jointwork.length !== 0)
                                                        $scope.fmcgData['jontWorkSelectedList'].push(pTemp);
                                                }
                                            }
                                            response[0].jontWorkSelectedList = $scope.fmcgData['jontWorkSelectedList'];
                                        }

                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            window.localStorage.setItem("mypln", JSON.stringify(response));
                                        ReldDta(12);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[12] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(12);
                                    });
                        } else {
                            ReldDta(12);
                        }
                        break;
                    case 13:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["Doctor_Category", '["Cat_Code as id", "Cat_Name as name"]', , '["isnull(Cat_Flag,0)=0"]', , , , 0])
                                    .success(function (response) {
                                        window.localStorage.removeItem("Doctor_Category");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            window.localStorage.setItem("Doctor_Category", JSON.stringify(response));
                                        ReldDta(13);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[13] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(13);
                                    });
                        } else {
                            ReldDta(13);
                        }
                        break;
                    case 14:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["Doctor_Specialty", '["Specialty_Code as id", "Specialty_Name as name"]', , '["isnull(Deactivate_flag,0)=0"]', , , , 0])
                                    .success(function (response) {
                                        window.localStorage.removeItem("Doctor_Specialty");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            window.localStorage.setItem("Doctor_Specialty", JSON.stringify(response));
                                        ReldDta(14);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[14] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(14)
                                    });
                        } else {
                            ReldDta(14);
                        }
                        break;
                    case 15:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'get/subordinate', ["subordinate_master", '["sf_code as id", "sf_name as name"]'])
                                    .success(function (response) {
                                        window.localStorage.removeItem("subordinate_master");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("subordinate_master", response);
                                        ReldDta(15);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[15] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(15)
                                    });
                        } else {
                            ReldDta(15);
                        }
                        break;
                    case 16:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'get/submgr', ["subordinate", '["sf_code as id", "sf_name as name"]'])
                                    .success(function (response) {
                                        window.localStorage.removeItem("subordinate");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("subordinate", response);
                                        ReldDta(16);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[16] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(16)
                                    });
                        } else {
                            ReldDta(16);
                        }
                        break;
                    case 17:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["category_master", '["Category_Code as id", "Category_Name as name"]', , , , , , 0])
                                    .success(function (response) {
                                        window.localStorage.removeItem("category_master");
                                        if (response.length && response.length > 0 && Array.isArray(response)) {
                                            fmcgLocalStorage.createData("category_master", response);
                                        }
                                        ReldDta(17);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[17] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(17);
                                    });
                        } else {
                            ReldDta(17);
                        }
                        break;
                    case 18:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["Mas_Doc_Class", '["Doc_ClsCode as id", "Doc_ClsSName as name"]'])
                                    .success(function (response) {
                                        window.localStorage.removeItem("class_master");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("class_master", response);
                                        ReldDta(18);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[18] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(18)
                                    });
                        } else {
                            ReldDta(18);
                        }
                        break;
                    case 19:
                        if (cMod == true || total == 1) {
                            fmcgAPIservice.getDataList('POST', 'table/list', ["Mas_Doc_Qualification", '["sf_code as id", "sf_name as name"]'])
                                    .success(function (response) {
                                        window.localStorage.removeItem("Qualifications");
                                        if (response.length && response.length > 0 && Array.isArray(response))
                                            fmcgLocalStorage.createData("Qualifications", response);
                                        ReldDta(19);
                                    })
                                    .error(function () {
                                        $scope.ErrFnd[19] = 1;
                                        cMod = false;
                                        $scope.DataLoaded = false;
                                        ReldDta(19)
                                    });
                        } else {
                            ReldDta(19);
                        }
                        break;

                    case 20:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwProductStateRates", '["State_Code","Division_Code","Distributor_Price","Product_Detail_Code"]', , '["isnull(State_Code,0)=' + $scope.State_Code + '"]'], CDId.replace(/_/g, ''))
                                .success(function (response) {
                                    window.localStorage.removeItem("Product_State_Rates");
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("Product_State_Rates", response);
                                    ReldDta(20);
                                })
                                .error(function () {
                                    $scope.ErrFnd[20] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(20);
                                });
                        break;
                    case 21:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwLastUpdationDate", '["Last_Updation_Date"]'])
                                .success(function (response) {
                                    window.localStorage.removeItem("Last_Updation_Date");
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("Last_Updation_Date", response);
                                    ReldDta(21);
                                })
                                .error(function () {
                                    $scope.ErrFnd[21] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(21);
                                });
                        break;
                    case 22:
                        var viewApp = ($scope.view_MR == 1) ? "vwTour_Plan_App" : "vwTourPlan";

                        fmcgAPIservice.getDataList('POST', 'table/list', [viewApp, '["date","remarks","worktype_code","worktype_name","RouteCode","RouteName","Worked_with_Code","Worked_with_Name"]'], CDId.replace(/_/g, ''))
                                .success(function (response) {
                                    window.localStorage.removeItem("Tour_Plan");
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("Tour_Plan", response);
                                    ReldDta(22);
                                })
                                .error(function () {
                                    $scope.ErrFnd[22] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(22);
                                });
                        break;
                    case 23:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwLeaveType", '["id","name","Leave_Name"]'])
                       .success(function (response) {
                           window.localStorage.removeItem("LeaveTypes");
                           if (response.length && response.length > 0 && Array.isArray(response))
                               fmcgLocalStorage.createData("LeaveTypes", response);
                           ReldDta(23);
                       })
                       .error(function () { $scope.ErrFnd[23] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(23); });
                        break;
                    case 24:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwFolders", '["id", "name"]'])
                        .success(function (response) {
                            window.localStorage.removeItem("folders");
                            window.localStorage.removeItem("subfolders");
                            if (response.length && response.length > 0 && Array.isArray(response)) {
                                fmcgLocalStorage.createData("folders", response);
                                var subfolder = response.slice(3);
                                fmcgLocalStorage.createData("subfolders", subfolder);
                            }
                            ReldDta(24);
                        })
                        .error(function () { $scope.ErrFnd[24] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(24); });
                        break;
                    case 25:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["GetMailSF", '[*]'])
                        .success(function (response) {
                            window.localStorage.removeItem("mailsSF");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("mailsSF", response);
                            ReldDta(25);
                        })
                        .error(function () { $scope.ErrFnd[25] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(25); });
                        break;
                    case 26:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwMasDSM", '["id", "name","town"]'])
                        .success(function (response) {
                           
                            window.localStorage.removeItem("mas_dsm");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("mas_dsm", response);
                            ReldDta(26);
                        })
                        .error(function () { $scope.ErrFnd[26] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(26); });
                        break;

                    case 27:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["competitor_details", '["id", "name"]', , , , , , , , ], CDId.replace(/_/g, ''))
                     .success(function (response) {
                         window.localStorage.removeItem("competitor");
                         if (response.length && response.length > 0 && Array.isArray(response))
                             fmcgLocalStorage.createData("competitor", response);
                         ReldDta(27);
                     })
                       .error(function () { });
                        break;
                 
               
                    case 28:
                        fmcgAPIservice.getDataList('POST', 'table/list', ["mas_district", '["dist_code as id", "dist_name as name","state_code state_code"]', , , , , , 0])

                     .success(function (response) {
                         window.localStorage.removeItem("districts");
                         if (response.length && response.length > 0 && Array.isArray(response))
                             fmcgLocalStorage.createData("districts", response);
                         ReldDta(28);
                     })
                       .error(function () { });
                        break;

                }

                return true;
            }
            $scope.clearAll = function (cMod, CID) {
                if (!CID)
                    CID = $scope.cDataID;
                n = 29;
                $scope.lcnt = 0;
                $scope.DataLoaded = true;
                for (var i = 0; i < n; i++)
                    $scope.clearIdividual(i, n, CID, cMod);
            };

            if ($scope.worktypes.length == 0) {

                $state.go('fmcgmenu.reloadMaster');
                $scope.clearAll(true);
            }
            else {
                if ($scope.Myplns.length == 0 && $scope.SFStat == '0') {
                    $ionicLoading.show({
                        template: 'Validating Your Day Plan.<br>Please Wait...'
                    });
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwMyDayPlan", '["worktype","FWFlg","sf_member_code as subordinateid","cluster as clusterid","ClstrName","remarks","stockist as stockistid","worked_with_code","worked_with_name"]', , ])
                            .success(function (response) {
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    window.localStorage.setItem("mypln", JSON.stringify(response));
                                $scope.loadDatas(true);
                                $ionicLoading.hide();
                            })
                            .error(function () {
                                $ionicLoading.hide();
                                Toast('No Internet Connection.');
                                $state.go('fmcgmenu.myPlan');
                                $scope.PlanChk = 1;
                            });
                }
            }

            $scope.onLine = isReachable();
            $scope.cComputer = isComputer();
            $scope.vibrate = function () {
                //if (!$scope.cComputer)
                //    notification.vibrate("20");
            }
            function sendHourlyPositionToServer(coords) {
            }
            $scope.HOURLY_COUNTER = 0;
            setInterval(function () {
                $scope.onLine = isReachable();
                $scope.cComputer = isComputer();
                $scope.HOURLY_COUNTER++;
                if ($scope.cComputer) {
                    navigator.geolocation.getCurrentPosition(function (position) {
                        if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                            $scope.fmcgData.location = position.coords.latitude + ":" + position.coords.longitude;
                        }
                        $scope.fmcgData.hasGps = isGpsEnabled();
                    });
                }
                else {
                    if (_currentLocation.Latitude != undefined) {
                        var dt = new Date();
                        var difference = dt.getTime() - _currentLocation.Time.getTime(); // This will give difference in milliseconds
                        var resultInMinutes = Math.round(difference / 60000);
                        if (resultInMinutes > 5) {
                            _currentLocation = {}; //startGPS();
                            ele = document.querySelector(".ion-location");
                            if (ele)
                                ele.classList.remove("available");
                            $scope.fmcgData.hasGps = isGpsEnabled(true);
                        }
                        else {
                            if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                                $scope.fmcgData.geoaddress = _currentLocation.address;
                                $scope.fmcgData.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;
                            }
                            ele = document.querySelector(".ion-location");
                            if (ele)
                                ele.classList.add("available");
                            $scope.fmcgData.hasGps = isGpsEnabled();
                        }
                    } else {
                        navigator.geolocation.getCurrentPosition(function (position) {
                            if ($scope.HOURLY_COUNTER == 36) {
                                $scope.HOURLY_COUNTER = 0;
                                sendHourlyPositionToServer(position.coords);
                            }
                            if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                                $scope.fmcgData.location = position.coords.latitude + ":" + position.coords.longitude;
                            }
                            ele = document.querySelector(".ion-location");
                            if (ele)
                                ele.classList.add("available");
                            $scope.fmcgData.hasGps = isGpsEnabled();
                        }, function (error) {
                            ele = document.querySelector(".ion-location");
                            if (ele)
                                ele.classList.remove("available");
                            $scope.fmcgData.hasGps = isGpsEnabled(true);
                        }, { maximumAge: 0, timeout: 5000, enableHighAccuracy: true });
                    }
                }
            }, 1000);
            $scope.currentSelection = {};
            $scope.FilteredData = {};
            $scope.selectionType = 0;
            $scope.SelMulti = 0;
            $scope.SlHlfDys = '';
            $scope.ShowMsg = true;
            $scope.selCate = '';

            $scope.selectedProduct = [];
            $scope.photosList = [];

            $ionicModal.fromTemplateUrl('partials/mapView.html', {
                id: '5',
                scope: $scope,
                animation: 'slide-in-up'
            }).then(function (modal) {
                $scope.modalMap = modal;
            });

            $ionicModal.fromTemplateUrl('partials/openImgLst.html', {
                id: '4',
                scope: $scope,
                animation: 'slide-in-up'
            }).then(function (modal) {
                $scope.modalPhotos = modal;
            });
            $ionicModal.fromTemplateUrl('partials/mnuOptions.html', {
                id: '3',
                scope: $scope,
                animation: 'slide-in-down'
            }).then(function (modal) {
                $scope.modalMnu = modal;
            });
            $scope.openOptMenu = function () {
                $scope.modalMnu.show();
                //$scope.pictureSource =;
                $scope.destinationType = Camera.DestinationType.FILE_URI;
            }

            $scope.$on('ClearEvents', function () {
                $scope.photosList = [];
            })
            $scope.openCamera = function (source) {
                $scope.modalPhotos.show();
                navigator.camera.getPicture(function (imgSrc) {
                    imgLst = {};
                    imgLst.imgData = imgSrc;
                    imgLst.title = "";
                    imgLst.remarks = "";
                    $scope.photosList.push(imgLst);
                }, function (msg) {
                }, { sourceType: 1, quality: 60, destinationType: $scope.destinationType, sourceType: source });
            }
            $scope.setEvntCap = function () {
                $scope.fmcgData.photosList = angular.copy($scope.photosList);
                $scope.modalMnu.hide();
            }
            $scope.deletePhoto = function (item) {
                $ionicPopup.confirm(
                        {
                            title: 'Call Delete',
                            content: 'Are you sure you want to Delete?'
                        }).then(function (res) {
                            if (res) {
                                $scope.photosList.splice($scope.photosList.indexOf(item), 1);
                                Toast("Deleted Successfully...");
                            } else {
                                console.log('You are not sure');
                            }
                        }
                );
            };
            $ionicModal.fromTemplateUrl('partials/selectModalProduct.html', {
                id: '1',
                scope: $scope,
                animation: 'slide-in-up'
            }).then(function (modal) {
                $scope.modalProd = modal;
            });
            $ionicModal.fromTemplateUrl('partials/selectModalStaffs.html', {
                id: '1',
                scope: $scope,
                animation: 'slide-in-up'
            }).then(function (modal) {
                $scope.modalStaffs = modal;
            });
            $ionicModal.fromTemplateUrl('partials/selectModal.html', {
                id: '2',
                scope: $scope,
                animation: 'slide-in-up'
            }).then(function (modal) {
                $scope.modal = modal;
            });

            $scope.prdCloseMenu = function () {
                $('ion-side-menu-content').css(ionic.CSS.TRANSFORM, 'translate3d(0px, 0px, 0px)');
                if ($('body').hasClass('menu-open'))
                    $('body').removeClass('menu-open');
            }
            $scope.filterProd = function (item) {
                $scope.ProdByCat = {};
                $scope.ShowMsg = true;
                if (item.id == -1) {
                    $scope.ProdByCat = $scope.products.filter(function (a) {
                        return (a.Product_Type_Code == "P");
                    });
                }
                else {
                    $scope.ProdByCat = $scope.products.filter(function (a) {
                        return (a.cateid == item.id);
                    });
                }
                for (di = 0; di < $scope.ProdByCat.length; di++) {
                    itm = $scope.selectedProduct.filter(function (a) {
                        return (a.id == $scope.ProdByCat[di].id);
                    });
                    $scope.ProdByCat[di].checked = false;
                    if (itm.length > 0) {
                        $scope.ProdByCat[di].checked = true;
                    }
                }
                $scope.selCate = item.name;
                if ($scope.ProdByCat.length < 1) {
                    $scope.prodmsg = 'No Products Found';
                } else {
                    $scope.ShowMsg = false;
                }
                $ionicScrollDelegate.scrollTop();
            }

            $scope.roundNumber = function (number, precision) {
                precision = Math.abs(parseInt(precision)) || 0;
                var multiplier = Math.pow(10, precision);
                return (Math.round(number * multiplier) / multiplier);
            }
            $scope.cal_Distns = function (lat1, lon1, lat2, lon2, unit) {
                var radlat1 = Math.PI * lat1 / 180;
                var radlat2 = Math.PI * lat2 / 180;
                var radlon1 = Math.PI * lon1 / 180;
                var radlon2 = Math.PI * lon2 / 180;
                var theta = lon1 - lon2;
                var radtheta = Math.PI * theta / 180;
                var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
                dist = Math.acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;
                if (unit == "K") {
                    dist = dist * 1.609344
                }
                if (unit == "N") {
                    dist = dist * 0.8684
                }
                return $scope.roundNumber(dist, 3);
            }

            $scope.openMailStaffs = function (sdata, choice, repId) {
                $scope.sData = sdata;
                $scope.selectionType = choice;
                if (choice == 201)
                    data = $scope.fmcgData.staffSelectedList || [];
                //  console.log($scope.fmcgData.staffSelectedList)
                if (choice == 202)
                    data = $scope.fmcgData.staffSelectedListcc || [];
                if (choice == 203)
                    data = $scope.fmcgData.staffSelectedListbcc || [];
                for (var di = 0; di < data.length; di++) {
                    data[di].checked = true;
                    data[di].id = data[di].id
                    data[di].name = data[di].name
                }
                $scope.selectedProduct = data
                $scope.prdCloseMenu();
                for (di = 0; di < $scope.sData.length; di++) {
                    itm = data.filter(function (a) { return (a.id == $scope.sData[di].id); });
                    $scope.sData[di].checked = false;
                    if (itm.length > 0) {
                        if (itm[0].checked == true) {
                            $scope.sData[di].checked = true;
                        }
                    }
                }
                $scope.modalStaffs.show();
                $ionicScrollDelegate.$getByHandle("ProdScroll").scrollTop();

            }
            $scope.openMProduct = function (sdata, choice, repId) {
                if ($scope.selCate == '') {
                    //$scope.selCate = 'Select the Category';
                    item = $scope.ProdCategory[0];
                    $scope.filterProd(item);

                }
                $scope.selectionType = choice;
                if (choice == 9) {
                    $scope.modal.searchText = '';
                    data = $scope.fmcgData.productSelectedList;
                }
                if (choice == 29)
                    data = $scope.RCPA.productSelectedList;
                if (choice == 49)
                    data = $scope.RMCL.productSelectedList;

                for (var di = 0; di < data.length; di++) {
                    data[di].checked = true;

                    data[di].id = data[di].product
                    data[di].name = data[di].product_Nm
                    data[di].product_netwt = data[di].product_netwt
                }

                $scope.selectedProduct = data
                $scope.prdCloseMenu();
                for (di = 0; di < $scope.ProdByCat.length; di++) {
                    itm = data.filter(function (a) {
                        return (a.id == $scope.ProdByCat[di].id);
                    });
                    $scope.ProdByCat[di].checked = false;
                    if (itm.length > 0) {
                        if (itm[0].checked == true) {
                            $scope.ProdByCat[di].checked = true;
                        }
                    }
                }
                $scope.modalProd.show();
                $ionicScrollDelegate.$getByHandle("ProdScroll").scrollTop();
            }
            $scope.openModal = function (sdata, choice, repId) {
                $scope.prdCloseMenu();
                $scope.modal.show();
                $scope.FilteredData = {};
                tSdta = sdata;

                showmsg_t = false;
                data1 = {};
                if (typeof (sdata) == 'string') {
                    if ((",town_master,stockist_master,chemist_master,doctor_master,unlisted_doctor_master,salesforce_master,").indexOf(',' + sdata + ',') > -1)
                        sdata = sdata + '_' + repId; //((',102,103,'.indexOf(',' + choice + ',') > -1) ? $scope.cMTPDId : $scope.cDataID);
                    data = fmcgLocalStorage.getData(sdata) || [];
                    if (tSdta == 'town_master' && choice != 32 && choice != 22) {
                        $scope.fmcgData.doctor = {};
                        $scope.fmcgData.doctor = undefined;
                        if ($scope.view_MR != 1) {
                            if (($scope.SF_type == 2 || $scope.SF_type == 1) && $scope.Mypln.stockist !== undefined) {
                               
                                data1 = data.filter(function (a) {
                                    return (a.stockist_code == $scope.Mypln.stockist.selected.id);
                                });
                            }
                            if (($scope.SF_type !== 2 || $scope.SF_type == 1) && $scope.BrndSumm.stockist != undefined) {
                              
                                data1 = data.filter(function (a) {
                                    return (a.stockist_code == $scope.BrndSumm.stockist.selected.id);
                                });

                            }
                            if ($scope.BrndSumm.stockist != undefined && $scope.Mypln.stockist !== undefined)
                            data = data1;
                        }
                        $scope['myTpTwns'] = data;
                    }
                }
                else {
                    data = sdata;
                }
                $scope.selectionType = choice;
                $scope.modal.searchText = '';
                $scope.SelMulti = 0;
                var res = [];
                if (",5,6,7,25,26,36,106,109,".indexOf("," + choice + ",") > -1) {
                    $scope.OrderFields = ['name'];
                    tCode = '';
                    if (",6,36,109,".indexOf("," + choice + ",") > -1 && $scope.CusOrder == 1) {
                        $scope.OrderFields = ['ListedDr_Sl_No'];
                    }

                    if (",6,".indexOf("," + choice + ",") > -1 && $scope.GEOTagNeed == 1) {
                        if ($scope.fmcgData.location == undefined) {
                            Toast("Location not available. Please wait till get Location.")
                            return false;
                        }
                        $scope.FilteredData = data.filter(function (a) {
                            var loc = $scope.fmcgData.location.split(':');
                            var crDis = $scope.cal_Distns(loc[0], loc[1], a.lat, a.long, 'K');
                            if (a.lat != '') {
                                a.dis = crDis;
                            }
                            return (crDis <= $scope.DisRad);
                        });
                        data = [];
                    } else {
                        if (",4,5,6,7,".indexOf("," + choice + ",") > -1)
                            tCode = $scope.fmcgData.cluster.selected.id;
                        if (",106,".indexOf("," + choice + ",") > -1) {
                            tCode = $scope.Mypln.cluster.selected.id;

                        }
                        if (",25,26,".indexOf("," + choice + ",") > -1)
                            tCode = $scope.RCPA.cluster.selected.id;
                        if (",36,".indexOf("," + choice + ",") > -1)
                            tCode = $scope.precall.cluster.selected.id;
                        $scope.FilteredData = data.filter(function (a) {
                            return (a.town_code === tCode);
                        });
                        if (",109,".indexOf("," + choice + ",") > -1)
                            tCode = $scope.Mypln.cluster.selected.id;
                            $scope.FilteredData = data.filter(function (a) {
                                return (a.town_code === tCode);
                            });
                     //      $scope.Mypl = fmcgLocalStorage.getData("mypln") || []; $scope.Mypl[0].FWFlg;
                            if (choice == 6 && $scope.fmcgData.worktype.selected.FWFlg == "HG") {
                            $scope.FilteredData = data.filter(function (a) {
                                return (a.town_code === tCode && a.horeca==0);
                            });
                        }
                        if (tCode = '') {
                            showmsg_t = true;
                            ErrMsg = "select the Cluster...";
                        }
                        if ($scope.FilteredData.length < 1 && tCode != '') {
                            showmsg_t = true;
                            ErrMsg = "No Data in this Cluster...";
                        }
                        data = [];//data.filter(function (a) { return (a.town_code != $scope.fmcgData.cluster.selected.id); });
                        $scope.SWTOWN = 0;
                        if (",4,".indexOf("," + choice + ",") > -1) {
                            $scope.FilteredData = data.filter(function (a) {
                                $scope.SWTOWN = 1;
                                return (a.town_code != tCode);
                            });
                        }
                    }
                }
                else if (",4,14,".indexOf("," + choice + ",") > -1) {
                    $scope.FilteredData = data.filter(function (a) {
                        return (a.town_code === $scope.fmcgData.cluster.selected.id);
                    });
                    data = data.filter(function (a) {
                        return (a.town_code != $scope.fmcgData.cluster.selected.id);
                    });
                }
                else if (",8,9,10,29,41,49,".indexOf("," + choice + ",") > -1) {
                    $scope.SelMulti = 1;
                    if (choice == 8)
                        slDta = $scope.fmcgData.jontWorkSelectedList;
                    if (choice == 10)
                        slDta = $scope.fmcgData.giftSelectedList;


                    if (choice == 41)
                        slDta = $scope.RMCL.jontWorkSelectedList;
                    if (choice == 9) {
                        slDta = $scope.fmcgData.productSelectedList;
                    }
                    if (choice == 29)
                        slDta = $scope.RCPA.productSelectedList;
                    if (choice == 49)
                        slDta = $scope.RMCL.productSelectedList;

                    for (var di = 0; di < data.length; di++) {
                        data[di].checked = false;
                        for (var ii = 0; ii < slDta.length; ii++) {
                            if (",8,41,".indexOf("," + choice + ",") > -1)
                                var slDtaid = slDta[ii].jointwork;
                            if (",9,29,49,".indexOf("," + choice + ",") > -1)
                                var slDtaid = slDta[ii].product;
                            if (choice == 10)
                                var slDtaid = slDta[ii].gift;
                            if (slDtaid == data[di].id) {
                                data[di].checked = true;
                                if (choice == 9)
                                    data[di].rx_qty = slDta[ii].rx_qty || 0;
                                if (",29,49,".indexOf("," + choice + ",") > -1) {
                                    data[di].qty = slDta[ii].qty || 0;
                                }
                                if (choice == 9 || choice == 10)
                                    data[di].sample_qty = slDta[ii].sample_qty || 0;
                            }
                        }
                    }
                    if (",9,29,".indexOf("," + choice + ",") > -1)
                        $scope.OrderFields = ['pSlNo', 'name'];
                    else
                        $scope.OrderFields = ['name'];
                }
                else if (choice == 3) {
                    $scope.OrderFields = ['id'];
                }

                else {
                    $scope.OrderFields = ['name'];
                    //data=data.sort(function(d1,d2){return  d1.name.toLowerCase()>d2.name.toLowerCase() ? 1 : (d1.name.toLowerCase()<d2.name.toLowerCase()) ? -1 : 0});                
                }
                var fl = window.localStorage.getItem("addRetailorflg");
                if (choice == 16 && fl==1) {
                   data = data.filter(function (a) {
                        return (a.horeca == 0);
                    });
                }
                $scope.currentSelection = data;
                $ionicScrollDelegate.$getByHandle("optsScroll").scrollTop();

                $timeout(function () {
                    $("#tSearch").focus();
                }, 800);
            };
            $scope.closeModal = function () {
                $scope.modal.hide();
            };
            $scope.setSelProduct = function (item) {
                $scope.selectedProduct = $scope.selectedProduct.filter(function (a) {
                    return (a.id != item.id)
                })
                if (item.checked == true) {
                    $scope.selectedProduct.push(item);
                }
            }

            $scope.svSelect = function (CurrentData) {
                var SlTyp = $scope.selectionType;
                if (SlTyp == 8)
                    $scope.fmcgData.jontWorkSelectedList = [];
                if (SlTyp == 9)
                    $scope.fmcgData.productSelectedList = [];
                if (SlTyp == 29) {
                    $scope.RCPA.productSelectedList = [];
                    $scope.RCPA.OurBrndNms = '';
                    $scope.RCPA.OurBrndCds = "";
                }
                if (SlTyp == 49) {
                    $scope.RMCL.productSelectedList = [];
                    $scope.RMCL.ProdNms = '';
                    $scope.RMCL.ProdCds = "";
                }
                if (SlTyp == 41) {
                    $scope.RMCL.jontWorkSelectedList = [];
                    $scope.RMCL.JntWrkNms = '';
                    $scope.RMCL.JntWrkCds = "";
                }
                if (SlTyp == 201) {
                    $scope.fmcgData.staffSelectedList = []; $scope.fmcgData.staffNms = ''; $scope.fmcgData.staffIds = "";
                }
                if (SlTyp == 202) {
                    $scope.fmcgData.staffSelectedListcc = []; $scope.fmcgData.ccstaffNms = ''; $scope.fmcgData.ccstaffIds = "";
                }
                if (SlTyp == 203) {
                    $scope.fmcgData.staffSelectedListbcc = []; $scope.fmcgData.bccstaffNms = ''; $scope.fmcgData.bccstaffIds = "";
                }
                if (SlTyp == 10)
                    $scope.fmcgData.giftSelectedList = [];

                for (var i = 0; i < CurrentData.length; i++) {
                    if (CurrentData[i].checked == true) {
                        if (",8,41,".indexOf("," + SlTyp + ",") > -1) {
                            var jontWorkData = {};
                            jontWorkData.jointwork = CurrentData[i].id;
                            jontWorkData.jointworkname = CurrentData[i].name;
                            if (SlTyp == 8)
                                $scope.fmcgData.jontWorkSelectedList.push(jontWorkData);
                            if (SlTyp == 41) {
                                $scope.RMCL.JntWrkCds += CurrentData[i].id + ', ';
                                $scope.RMCL.JntWrkNms += CurrentData[i].name + ', ';
                                $scope.RMCL.jontWorkSelectedList.push(jontWorkData);
                            }
                        }
                        else if (",9,29,49,".indexOf("," + SlTyp + ",") > -1) {
                            var productData = {};
                            productData.product = CurrentData[i].id;
                            productData.product_Nm = CurrentData[i].name;
                            if (SlTyp == 9) {
                                productData.rx_qty = CurrentData[i].rx_qty || 0;
                                productData.sample_qty = CurrentData[i].sample_qty || 0;
                                productData.cb_qty = CurrentData[i].cb_qty || 0;
                                productData.pieces = CurrentData[i].pieces || 0;
                                productData.netweightvalue = CurrentData[i].netweightvalue || 0;

                                productData.product_netwt = CurrentData[i].product_netwt || 0;
                                productData.recv_qty = CurrentData[i].recv_qty || 0;
                                if ($scope.view_STOCKIST == 1) {
                                    productData.conversionQty = Number(CurrentData[i].conversionQty) || 0
                                }
                                $scope.fmcgData.productSelectedList.push(productData);
                            }
                            if (SlTyp == 29) {
                                $scope.RCPA.OurBrndCds += CurrentData[i].id + ', ';
                                $scope.RCPA.OurBrndNms += CurrentData[i].name + ', ';
                                productData.qty = CurrentData[i].qty || 0;
                                $scope.RCPA.productSelectedList.push(productData);
                            }
                            if (SlTyp == 49) {
                                $scope.RMCL.ProdCds += CurrentData[i].id + ', ';
                                $scope.RMCL.ProdNms += CurrentData[i].name + ', ';
                                productData.qty = CurrentData[i].qty || 0;
                                $scope.RMCL.productSelectedList.push(productData);
                            }
                        }
                        else if (",201,202,203,".indexOf("," + SlTyp + ",") > -1) {
                            var staffData = {};
                            staffData.id = CurrentData[i].id;
                            staffData.name = CurrentData[i].name;
                            if (SlTyp == 201) {
                                $scope.fmcgData.staffIds += CurrentData[i].id + ', ';
                                $scope.fmcgData.staffNms += CurrentData[i].name + ', ';
                                $scope.fmcgData.staffSelectedList.push(staffData);

                            }
                            if (SlTyp == 202) {
                                $scope.fmcgData.ccstaffIds += CurrentData[i].id + ', ';
                                $scope.fmcgData.ccstaffNms += CurrentData[i].name + ', ';
                                $scope.fmcgData.staffSelectedListcc.push(staffData);

                            }
                            if (SlTyp == 203) {
                                $scope.fmcgData.bccstaffIds += CurrentData[i].id + ', ';
                                $scope.fmcgData.bccstaffNms += CurrentData[i].name + ', ';
                                $scope.fmcgData.staffSelectedListbcc.push(staffData);

                            }
                        }
                        else if (",10,".indexOf("," + SlTyp + ",") > -1) {
                            var giftData = {};
                            giftData.gift = CurrentData[i].id;
                            giftData.gift_Nm = CurrentData[i].name;
                            giftData.sample_qty = CurrentData[i].sample_qty || 1;
                            $scope.fmcgData.giftSelectedList.push(giftData);
                        }
                    }
                }
                if (",9,29,49,".indexOf("," + SlTyp + ",") > -1) {
                    $scope.modalProd.hide();
                }
                else if (",201,202,203,".indexOf("," + SlTyp + ",") > -1)
                    $scope.modalStaffs.hide();
                else {
                    $scope.modal.hide();
                }
            }
            $scope.selectData = function (item) {
                $rootScope.hasData = true;
                var key = ['worktype', 'cluster', 'customer', 'stockist', 'chemist', 'doctor', 'uldoctor', 'jointwork', 'handover'];
                switch ($scope.selectionType) {
                    case 1:
                    case 101:
                    case 102:
                    default:
                        if (typeof ($scope.fmcgData.customer) != "undefined") {
                            if ($scope.selectionType == 3 && $scope.fmcgData.customer.selected.id.toString() != item.id.toString()) {
                                $rootScope.deleteRecord = true;
                                $scope.clearData();
                                $scope.data = {};
                                $scope.fmcgData.worktype = {};
                                $scope.fmcgData.worktype.selected = {};
                                var cus = item.id;
                                $scope.Mypl = fmcgLocalStorage.getData("mypln") || []; $scope.Mypl[0].FWFlg;

                                var CusTyp = (cus == 1) ? "D" : (cus == 2) ? "C" : (cus == 3) ? "S" : (cus == 4) ? "U" : "R";
                                var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function (a) {
                                    return (a.id == $scope.prvRptId)
                                }) : $scope.worktypes.filter(function (a) {
                                    return (a.FWFlg == $scope.Mypl[0].FWFlg && a.ETabs.indexOf(CusTyp) > -1)
                                });
                               
                                $scope.fmcgData.worktype.selected = Wtyps[0];   //{ "id": "field work", "name": "Field Work","FWFlg":"F"}        
                                if ($scope.view_MR == 1) {
                                    $scope.fmcgData.subordinate = {};
                                    $scope.fmcgData.subordinate.selected = {};
                                    $scope.fmcgData.subordinate.selected.id = $scope.sfCode;
                                }
                            }

                        }
                        if ($scope.selectionType > 100) {
                            $scope.Mypln[key[$scope.selectionType - 101]] = {};
                            $scope.Mypln[key[$scope.selectionType - 101]].name = item.name;
                            $scope.Mypln[key[$scope.selectionType - 101]].selected = {};
                            $scope.Mypln[key[$scope.selectionType - 101]].selected.id = item.id;
                            if ($scope.selectionType == 101)
                                $scope.Mypln[key[0]].selected.FWFlg = item.FWFlg;
                            if ($scope.selectionType == 104) {
                                $scope.$broadcast('distReport');
                                $scope.$broadcast('getBrandSalSumry');
                                if ($scope.view_MR != 1)
                                    $scope.Mypln.cluster = {};
                                $scope.fmcgData.RouteAdd = 1;
                            }
                            if ($scope.selectionType == 106) {
                                $scope.$broadcast('getLostProducts', { DrId: item.id });
                            }
                            if ($scope.selectionType == 102) {
                                $scope.Mypln.doctor = {};

                            }
                        }
                        if ($scope.selectionType == 200) {
                            $scope.folder = {};
                            $scope.folder.name = item.name;
                            $scope.folder.selected = {};
                            $scope.folder.selected.id = item.id;
                            $scope.$broadcast('getFolders', { Details: item });
                        }
                        if ($scope.selectionType == 201) {
                            $scope.subfolder = {};
                            $scope.subfolder.name = item.name;
                            $scope.subfolder.selected = {};
                            $scope.subfolder.selected.id = item.id;
                            $scope.$broadcast('moveFolders', { Details: item });
                        }
                        else if ($scope.selectionType > 40) {
                            $scope.RMCL[key[$scope.selectionType - 41]] = {};
                            $scope.RMCL[key[$scope.selectionType - 41]].name = item.name;
                            $scope.RMCL[key[$scope.selectionType - 41]].selected = {};
                            $scope.RMCL[key[$scope.selectionType - 41]].selected.id = item.id;

                            $scope.BrndSumm[key[$scope.selectionType - 40]] = {};
                            $scope.BrndSumm[key[$scope.selectionType - 40]].name = item.name;
                            $scope.BrndSumm[key[$scope.selectionType - 40]].selected = {};
                            $scope.BrndSumm[key[$scope.selectionType - 40]].selected.id = item.id;

                            $scope.stockUpdation[key[$scope.selectionType - 41]] = {};
                            $scope.stockUpdation[key[$scope.selectionType - 41]].name = item.name;
                            $scope.stockUpdation[key[$scope.selectionType - 41]].selected = {};
                            $scope.stockUpdation[key[$scope.selectionType - 41]].selected.id = item.id;
                            if ($scope.selectionType == 45) {
                                $scope.LeaveTypes.type = {};
                                $scope.LeaveTypes.type.name = item.name;
                                $scope.LeaveTypes.type.selected = {};
                                $scope.LeaveTypes.type.selected.id = item.id;
                            }
                            if ($scope.selectionType == 43)
                                $scope.$broadcast('getBrandSalSumry');
                            if ($scope.selectionType == 44) {
                                stat = 0;
                                value = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                                for (key in value) {
                                    if (value[key]['stockistCode'] == $scope.stockUpdation.stockist.selected.id) {
                                        stat = 1;
                                        key1 = key;
                                    }
                                }
                                if (stat == 1)
                                    $scope.LastUpdationDate = value[key1]['Last_Updation_Date'];
                                else
                                    $scope.LastUpdationDate = '';
                                $scope.$broadcast('GetStocks');
                            }

                            //   $scope.$broadcast('getBrandSalSumry');

                        }

                        else if ($scope.selectionType > 30) {
                            $scope.precall[key[$scope.selectionType - 31]] = {};
                            $scope.precall[key[$scope.selectionType - 31]].name = item.name;
                            $scope.precall[key[$scope.selectionType - 31]].selected = {};
                            $scope.precall[key[$scope.selectionType - 31]].selected.id = item.id;
                            if ($scope.selectionType == 32) {
                                $scope.precall.doctor = {};
                                $scope.precall.doctor = undefined;
                                $scope.$broadcast('clear', { DrId: item.id });
                            }
                            if ($scope.selectionType == 36)
                                $scope.$broadcast('getPreCall', { DrId: item.id });
                        }
                        else if ($scope.selectionType > 20) {
                            if ($scope.selectionType == 22) {
                                if ($scope.RCPA.cluster == undefined || $scope.RCPA.cluster.selected.id != item.id) {
                                    $scope.RCPA.chemist = undefined;
                                    $scope.RCPA.doctor = undefined;
                                }
                            }
                            if ($scope.selectionType == 24) {

                                $scope.RCPA.CpmptName = item.name;
                                $scope.RCPA.CpmptId = item.id;
                                // $scope.$broadcast('getCompetitorDetails', { id: item.id, i: i });
                            }
                            $scope.RCPA[key[$scope.selectionType - 21]] = {};
                            $scope.RCPA[key[$scope.selectionType - 21]].name = item.name;
                            $scope.RCPA[key[$scope.selectionType - 21]].selected = {};
                            $scope.RCPA[key[$scope.selectionType - 21]].selected.id = item.id;
                        }
                        else {

                            $scope.fmcgData[key[$scope.selectionType - 1]] = {};
                            $scope.fmcgData[key[$scope.selectionType - 1]].name = item.name;
                            $scope.fmcgData[key[$scope.selectionType - 1]].selected = {};
                            $scope.fmcgData[key[$scope.selectionType - 1]].selected.id = item.id;
                            if ($scope.selectionType == 1)
                                $scope.fmcgData[key[0]].selected.FWFlg = item.FWFlg;
                            if ($scope.selectionType == 6)
                                $scope.fmcgData[key[$scope.selectionType - 1]].Mobile_Number = item.Mobile_Number;
                            
                            $scope.$broadcast('checkWorktype', { flg: item.FWFlg });

                        }

                        var temp = [4, 5, 6, 7];
                        if (temp.indexOf($scope.selectionType) !== -1 && $scope.fmcgData.source === 0) {
                            $scope.fmcgData.cluster = {};
                            $scope.fmcgData.cluster.name = item.town_name;
                            $scope.fmcgData.cluster.selected = {};
                            $scope.fmcgData.cluster.selected.id = item.town_code;
                        }
                        break;
                    case 8:
                        var jontWorkData = {};
                        jontWorkData.jointwork = item.id;
                        jontWorkData.jointworkname = item.name;
                        $scope.fmcgData.jontWorkSelectedList = $scope.fmcgData.jontWorkSelectedList || [];
                        var len = $scope.fmcgData.jontWorkSelectedList.length;
                        var flag = true;
                        for (var i = 0; i < len; i++) {
                            if ($scope.fmcgData.jontWorkSelectedList[i]['jointwork'] === item.id) {
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
                        productData.product_Nm = item.name;
                        productData.rx_qty = 0;
                        productData.sample_qty = 0;
                        var len = $scope.fmcgData.productSelectedList.length;
                        var flag = true;
                        for (var i = 0; i < len; i++) {
                            if ($scope.fmcgData.productSelectedList[i]['product'] === item.id) {
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
                        giftData.gift_Nm = item.name;
                        giftData.sample_qty = 1;
                        var len = $scope.fmcgData.giftSelectedList.length;
                        var flag = true;
                        for (var i = 0; i < len; i++) {
                            if ($scope.fmcgData.giftSelectedList[i]['gift'] === item.id) {
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
                    case 11:
                        $scope.fmcgData.district = {};
                        $scope.fmcgData.district.selected = {};
                        $scope.fmcgData.district.selected.id = item.id;
                        $scope.fmcgData.district.name = item.name;
                        break;
                    case 13:
                        $scope.fmcgData.nrx_t = item.id;
                        break;
                    case 14:
                        $scope.fmcgData.OrdStk = {};
                        $scope.fmcgData.OrdStk.name = item.name;
                        $scope.fmcgData.OrdStk.selected = {};
                        $scope.fmcgData.OrdStk.selected.id = item.id;
                        break;
                    case 15:
                        $scope.fmcgData.remarks = item.name;
                        break;
                    case 112:
                        $scope.fmcgData.qulification = {};
                        $scope.fmcgData.qulification.name = item.name;
                        $scope.fmcgData.qulification.selected = {};
                        $scope.fmcgData.qulification.selected.id = item.id;
                        break;
                    case 113:
                        $scope.fmcgData.class = {};
                        $scope.fmcgData.class.name = item.name;
                        $scope.fmcgData.class.selected = {};
                        $scope.fmcgData.class.selected.id = item.id;
                        break;
                    case 114:
                        $scope.fmcgData.dsm = {};
                        $scope.fmcgData.dsm.name = item.name;
                        $scope.fmcgData.dsm.town = item.town;
                        $scope.fmcgData.dsm.selected = {};
                        $scope.fmcgData.dsm.selected.id = item.id;
                        break;
                    case 16:
                    case 17:
                    case 18:
                    case 28:
                    case 38:
                    case 40:
                    case 50:
                    case 52:
                    case 58:
                    case 48:
                    case 103:
                    case 208:
                        var key = { "c16": "speciality", "c17": "category", "c18": "subordinate" };

                        if ($scope.selectionType == 50) {
                            $scope.tpview[key['c18']] = {};
                            $scope.tpview[key['c18']].name = item.name;
                            $scope.tpview[key['c18']].selected = {};
                            $scope.tpview[key['c18']].selected.id = item.id;
                            $scope.$broadcast('eGetTpEntry');

                        }
                        else if ($scope.selectionType == 208) {
                            $scope.Reload[key['c18']] = {};
                            $scope.Reload[key['c18']].name = item.name;
                            $scope.Reload[key['c18']].selected = {};
                            $scope.Reload[key['c18']].selected.id = item.id;
                        }
                        else if ($scope.selectionType == 58) {
                            $scope.geoTag[key['c18']] = {};
                            $scope.geoTag[key['c18']].name = item.name;
                            $scope.geoTag[key['c18']].selected = {};
                            $scope.geoTag[key['c18']].selected.id = item.id;
                            $scope.geoTag['cluster'] = {};
                        }
                        else if ($scope.selectionType == 52) {
                            $scope.geoTag['cluster'] = {};
                            $scope.geoTag['cluster'].name = item.name;
                            $scope.geoTag['cluster'].selected = {};
                            $scope.geoTag['cluster'].selected.id = item.id;
                            $scope.$broadcast('setGeoData');
                        }
                        else if ($scope.selectionType == 103) {
                            $scope.Mypln[key['c18']] = {};
                            $scope.Mypln[key['c18']].name = item.name;
                            $scope.Mypln[key['c18']].selected = {};
                            $scope.Mypln[key['c18']].selected.id = item.id;


                        }

                        else if ($scope.selectionType == 40) {
                            $scope.MnthSumm[key['c18']] = {};
                            $scope.MnthSumm[key['c18']].name = item.name;
                            $scope.MnthSumm[key['c18']].selected = {};
                            $scope.MnthSumm[key['c18']].selected.id = item.id;

                            $scope.$broadcast('getMonthReport');
                        }
                        else if ($scope.selectionType == 42) {
                            $scope.BrndSumm[key['c18']] = {};
                            $scope.BrndSumm[key['c18']].name = item.name;
                            $scope.BrndSumm[key['c18']].selected = {};
                            $scope.BrndSumm[key['c18']].selected.id = item.id;

                            $scope.$broadcast('getBrandSalSumry');
                        }

                        else if ($scope.selectionType == 38) {

                            $scope.precall[key['c18']] = {};
                            $scope.precall[key['c18']].name = item.name;
                            $scope.precall[key['c18']].selected = {};
                            $scope.precall[key['c18']].selected.id = item.id;
                        }
                        else if ($scope.selectionType == 48) {
                            $scope.RMCL[key['c18']] = {};
                            $scope.RMCL[key['c18']].name = item.name;
                            $scope.RMCL[key['c18']].selected = {};
                            $scope.RMCL[key['c18']].selected.id = item.id;
                            $scope.RMCL.doctor = undefined;
                        }
                        else if ($scope.selectionType == 28) {

                            $scope.RCPA[key['c18']] = {};
                            $scope.RCPA[key['c18']].name = item.name;
                            $scope.RCPA[key['c18']].selected = {};
                            $scope.RCPA[key['c18']].selected.id = item.id;

                            $scope.RCPA.chemist = undefined;
                            $scope.RCPA.doctor = undefined;
                        }
                        else {
                            $scope.fmcgData[key['c' + $scope.selectionType]] = {};
                            $scope.fmcgData[key['c' + $scope.selectionType]].name = item.name;
                            $scope.fmcgData[key['c' + $scope.selectionType]].selected = {};
                            $scope.fmcgData[key['c' + $scope.selectionType]].selected.id = item.id;
                            if ($scope.selectionType == 18) {
                                $scope.fmcgData.stockist = undefined;
                                $scope.fmcgData.chemist = undefined;
                                $scope.fmcgData.doctor = undefined;
                                $scope.fmcgData.uldoctor = undefined;

                                $scope.fmcgData.cluster = undefined;
                            }
                        }
                        if (',18,28,38,48,104,103,'.indexOf(',' + $scope.selectionType + ',') > -1) {
                            var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
                            loginInfo.curSFCode = item.id;
                            $scope.fmcgData.jontWorkSelectedList = [];
                            window.localStorage.setItem("loginInfo", JSON.stringify(loginInfo));
                            var TwnDet = fmcgLocalStorage.getData("town_master_" + item.id) || [];
                            if ($scope.selectionType == 103) {
                                $scope.cMTPDId = '_' + item.id;
                                //$scope.myTpTwns =  fmcgLocalStorage.getData("town_master_" + item.id) || [];
                            } else {
                                $scope.cDataID = '_' + item.id;
                                //$scope.clusters = fmcgLocalStorage.getData("town_master_" + item.id) || [];
                            }
                            if (TwnDet.length > 0) {
                                $scope.loadDatas(false, '_' + item.id);
                            }
                            else if (TwnDet.length <= 0) {
                                $ionicLoading.show({
                                    template: 'Please Wait. Data is Sync...'
                                })
                                $scope.clearAll(false, '_' + item.id);
                            }
                        }
                        break;
                }
                $scope.modal.hide();
            };

            //Cleanup the modal when we're done with it!
            $scope.$on('$destroy', function () {
                $scope.modal.remove();
            });
            $scope.customers = [{
                'id': '1',
                'name': $scope.DrCap,
                'url': 'manageDoctorResult'
            }];
            var al = 1;
            if ($scope.ChmNeed != 1) {
                $scope.customers.push({
                    'id': '2',
                    'name': $scope.ChmCap,
                    'url': 'manageChemistResult'
                });
                $scope.cCI = al;
                al++;
            }
            if ($scope.StkNeed != 1) {
                $scope.customers.push({
                    'id': '3',
                    'name': $scope.StkCap,
                    'url': 'manageStockistResult'
                });
                $scope.sCI = al;
                al++;
            }
            if ($scope.UNLNeed != 1) {
                $scope.customers.push({
                    'id': '4',
                    'name': $scope.NLCap,
                    'url': 'manageStockistResult'
                });
                $scope.nCI = al;
                al++;
            }



            $scope.DataLoaded = true;
            $scope.fmcgData = {};
            $scope.Reload = {};
            $scope.precall = {};
            $scope.RCPA = {};
            $scope.RMCL = {};
            $scope.Mypln = {};
            $scope.MnthSumm = {};
            $scope.BrndSumm = {};
            $scope.stockUpdation = {};
            $scope.tpview = {};
            $scope.geoTag = {};

            $scope.go = function (path) {
                $location.path(path);
            };
            $scope.Mypln.worktype = {};
            $scope.Mypln.worktype.selected = {};
            $scope.prvRptId = '';
            var Wtyps = $scope.worktypes.filter(function (a) {
                return (a.FWFlg == "F")
            });
            $scope.Mypln.worktype.selected = Wtyps[0];

            $scope.showCounter = 0;
            $scope.draftCount = fmcgLocalStorage.getItemCount("draft");
            $scope.fmcgData.productSelectedList = [];
            $scope.fmcgData.giftSelectedList = [];
            $scope.fmcgData.jontWorkSelectedList = [];
            $scope.RCPA.productSelectedList = [];
            $scope.RMCL.jontWorkSelectedList = [];
            $scope.RMCL.productSelectedList = [];

            $scope.fmcgData.worktype = {};
            $scope.fmcgData.worktype.selected = {};
            $scope.fmcgData.worktype.selected = Wtyps[0];

            $rootScope.hasData = false;
            $scope.logout = function () {
                //window.localStorage.clear();
                $scope.toggleLeft();
                $state.go("signin");
            }
            $scope.toggleLeft = function () {
                //  notification.vibrate(15);
                //if ($('body').hasClass('menu-open')) {
                //}
                $ionicSideMenuDelegate.toggleLeft();
                $('body').removeClass('menu-open');
            };
            $scope.goToAddNew = function () {
                if ($scope.GeoChk == 0) {
                    Toast("Checking for Location Services");
                    navigator.geolocation.getCurrentPosition(function (position) {
                        $state.go('fmcgmenu.addNew');
                    }, function (error) {
                        alert("Please Enable Location Services.");
                    }, { maximumAge: 0, timeout: 5000, enableHighAccuracy: true });
                } else {
                    $state.go('fmcgmenu.addNew');
                }
            };
            if ($scope.onLine) {

            }

            $scope._savingData = false;
            $scope.savingData = {};
            $scope.savingData.RCPA = false;
            $scope.savingData.RMCL = false;
            $scope.savingData.MyPL = false;
            $scope.savingData.Rmks = false;
            $scope.savingData.EVNT = false;
            function svQDatas() {
                $scope.onLine = isReachable();

                if ($scope.onLine && navigator.onLine) {

                    var draftData = fmcgLocalStorage.getData("saveLater") || [];

                    //console.log(draftData.length + ' : ' + $scope._savingData);
                    if (draftData.length > 0 && $scope._savingData == false) {
                        var data = draftData[0];
                        $scope._savingData = true;
                        if (data.arc) {
                            var arc = data.arc;
                            var amc = data.amc;
                            fmcgAPIservice.saveDCRData('POST', 'dcr/updateEntry&arc=' + arc + '&amc=' + amc, data, true).success(function (response) {
                                if (!response['success'] && response['msg'] !== 'Call Already Exist') {
                                    draftData.shift();
                                } else if (response['msg'] == 'Call Already Exist') {
                                    draftData.shift();
                                }
                                else if (response['success']) {
                                    draftData.shift();
                                    Toast('Local calls synced', true);
                                } else {
                                    var data = draftData.shift();
                                    draftData.push(data);
                                }
                                fmcgLocalStorage.createData("saveLater", draftData);
                                $scope._savingData = false;
                            }).error(function () {
                                $scope._savingData = false;
                            });
                        } else {
                            //                                console.log(data)
                            fmcgAPIservice.saveDCRData('POST', 'dcr/save', data, true)
                                    .success(function (response) {
                                        if (!response['success'] && response['msg'] !== 'Call Already Exist') {
                                            draftData.shift();
                                        } else if (response['msg'] == 'Call Already Exist') {
                                            draftData.shift();
                                        }
                                        else if (response['success']) {
                                            draftData.shift();
                                            Toast('Local calls synced', true);
                                        } else {
                                            var data = draftData.shift();
                                            draftData.push(data);
                                        }
                                        fmcgLocalStorage.createData("saveLater", draftData);
                                        $scope._savingData = false;
                                    })
                                    .error(function () {
                                        $scope._savingData = false;
                                    });
                        }

                    }

                    var QDataEVNT = fmcgLocalStorage.getData("events") || [];
                    //console.log(JSON.stringify(QDataEVNT));
                    if (QDataEVNT.length > 0 && $scope.savingData.EVNT == false) {
                        var dataEVNT = QDataEVNT[0];
                        var imgUrl = dataEVNT.imgData;
                        $scope.savingData.EVNT = true;
                        var options = new FileUploadOptions();
                        options.fileKey = "imgfile";
                        options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                        options.mimeType = "image/jpeg";
                        options.chunkedMode = false;
                        var uplUrl = baseURL + 'imgupload&sf_code=' + $scope.sfCode;
                        var ft = new FileTransfer();
                        ft.upload(imgUrl, uplUrl,
                                function (response) {
                                    QDataEVNT.shift();
                                    alert("success");
                                    fmcgLocalStorage.createData("events", QDataEVNT);
                                    $scope.savingData.EVNT = false;
                                },
                                function () {
                                    $scope.savingData.EVNT = false;
                                }, options);
                    }

                    var QDataRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
                    if (QDataRCPA.length > 0 && $scope.savingData.RCPA == false) {
                        var dataRCPA = QDataRCPA[0];
                        $scope.savingData.RCPA = true;
                        fmcgAPIservice.addMAData('POST', 'dcr/save', '4', dataRCPA)
                                .success(function (response) {
                                    if (response['success']) {
                                        QDataRCPA.shift();
                                        Toast('Local calls synced', true);
                                    } else {
                                        var dataRCPA = QDataRCPA.shift();
                                        QDataRCPA.push(dataRCPA);
                                    }
                                    fmcgLocalStorage.createData("OutBx_RCPA", QDataRCPA);
                                    $scope.savingData.RCPA = false;
                                })
                                .error(function () {
                                    $scope.savingData.RCPA = false;
                                });
                    }

                    var QDataRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
                    if (QDataRMCL.length > 0 && $scope.savingData.RMCL == false) {
                        var dataRMCL = QDataRMCL[0];
                        $scope.savingData.RMCL = true;
                        fmcgAPIservice.addMAData('POST', 'dcr/save', '5', dataRMCL)
                                .success(function (response) {
                                    if (response['success']) {
                                        QDataRMCL.shift();
                                        Toast('Local calls synced', true);
                                    } else {
                                        var dataRMCL = QDataRMCL.shift();
                                        QDataRMCL.push(dataRMCL);
                                    }
                                    fmcgLocalStorage.createData("OutBx_RMCL", QDataRMCL);
                                    $scope.savingData.RMCL = false;
                                })
                                .error(function () {
                                    $scope.savingData.RMCL = false;
                                });
                    }

                    var QDataMyPL = fmcgLocalStorage.getData("myplnQ") || [];
                    if (QDataMyPL.length > 0 && $scope.savingData.MyPL == false) {
                        var dataMyPL = QDataMyPL[0];
                        $scope.savingData.MyPL = true;
                        fmcgAPIservice.addMAData('POST', 'dcr/save', '3', dataMyPL)
                                .success(function (response) {
                                    if (response['success']) {
                                        QDataMyPL.shift();
                                        Toast('Local calls synced', true);
                                    } else {
                                        var dataMyPL = QDataMyPL.shift();
                                        QDataMyPL.push(dataMyPL);
                                    }
                                    fmcgLocalStorage.createData("myplnQ", QDataMyPL);
                                    $scope.savingData.MyPL = false;
                                })
                                .error(function () {
                                    $scope.savingData.MyPL = false;
                                });
                    }

                    var QDataRmks = fmcgLocalStorage.getData("MyDyRmksQ") || [];
                    if (QDataRmks.length > 0 && $scope.savingData.Rmks == false) {
                        var dataRmks = QDataRmks[0];
                        $scope.savingData.Rmks = true;
                        fmcgAPIservice.updateRemark('POST', 'dcr/updRem', dataRmks)
                                .success(function (response) {
                                    if (response['success']) {
                                        Toast('Local calls synced', true);
                                    }
                                    QDataRmks.shift();
                                    fmcgLocalStorage.createData("MyDyRmksQ", QDataRmks);
                                    $scope.savingData.Rmks = false;
                                })
                                .error(function () {
                                    $scope.savingData.Rmks = false;
                                });
                    }
                }
            }
            svQDataIntrvl = setInterval(function () {
                svQDatas();
            }, 500); //(1000*(60*5)));
            setInterval(function () {
                var QueData = fmcgLocalStorage.getData("saveLater") || [];
                $scope.outboxCount = QueData.length;
                var QueData = fmcgLocalStorage.getData("OutBx_RCPA") || [];
                $scope.outboxCount += QueData.length;
                var QueData = fmcgLocalStorage.getData("OutBx_RMCL") || [];
                $scope.outboxCount += QueData.length;
                var QueData = fmcgLocalStorage.getData("myplnQ") || [];
                $scope.outboxCount += QueData.length;
                var QueData = fmcgLocalStorage.getData("MyDyRmksQ") || [];
                $scope.outboxCount += QueData.length;

                var QueData = fmcgLocalStorage.getData("draft") || [];
                $scope.draftCount = QueData.length;
            }, 500);

            $scope.fmcgDataCopy = angular.copy($scope.fmcgData);
            $scope.clearData = function () {
                if ($scope.fmcgData) {
                    if ($rootScope.deleteRecord) {
                        if ($rootScope.saveToDraft || $scope.fmcgData.isDraft == true) {
                            fmcgLocalStorage.addData('draft', $scope.fmcgData);
                        }
                        var value = $scope.fmcgData;
                        for (key in value) {
                            if (key) {
                                if (key != 'jontWorkSelectedList')
                                    $scope.fmcgData[key] = undefined;
                            }
                        }
                        $rootScope.hasData = false;
                        $rootScope.hasEditData = false;
                        $rootScope.deleteRecord = false;
                        $rootScope.saveToDraft = false;
                        $scope.fmcgData.isDraft = false;
                    }
                }
            };
            $scope.saveToDraftO = function () {
                var temp = fmcgLocalStorage.getData("draft");
                if (temp === null || temp.length == 0 || ($scope.fmcgData.worktype.selected.id).toString() === (temp[0]['worktype']['selected']['id']).toString()) {
                    $scope.fmcgData.isDraft = true;
                    fmcgLocalStorage.addData('draft', $scope.fmcgData);
                    var value = $scope.fmcgData;
                    for (key in value) {
                        if (key) {
                            if (key != 'jontWorkSelectedList')
                                $scope.fmcgData[key] = undefined;
                        }
                    }
                    $state.go('fmcgmenu.home');
                    Toast("Call saved to draft");
                }
                else {
                    $ionicPopup.confirm({
                        title: 'Call Conflict',
                        content: 'You have call for other worktype in draft do you want to replace...?'
                    })
                            .then(function (res) {
                                if (res) {
                                    window.localStorage.removeItem("draft");
                                    fmcgLocalStorage.addData('draft', $scope.fmcgData);
                                    var value = $scope.$parent.fmcgData;
                                    for (key in value) {
                                        if (key) {
                                            if (key != 'jontWorkSelectedList')
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

            $scope.setAllow = function () {
                $rootScope.hasData = true;
                if ($scope.fmcgData.amc || $scope.fmcgData.arc) {
                    $rootScope.hasEditData = true;
                }

            }
        }])

      .controller('addDoorToDoorCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$filter', '$ionicPopup', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $filter, $ionicPopup) {
          $scope.$parent.navTitle = "Door to Door";
          $scope.clearData();
          $scope.remarks = "";
          $scope.data = {};
          $scope.fmcgData.customer = {};
          $scope.fmcgData.customer.selected = {};
          $scope.fmcgData.customer.selected.id = 1;
          $scope.fmcgData.value1 = 0;
          var temp = window.localStorage.getItem("loginInfo");
          var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
          var $fl = 0;
          $scope.goBack = function () {
              $state.go('fmcgmenu.addNew');
          }
          $scope.addRetailor = function () {
              localStorage.removeItem("addRetailorflg");
              var temp = window.localStorage.getItem("addRetailorflg");
              fmcgLocalStorage.createData("addRetailorflg", 0);
              $state.go('fmcgmenu.addULDoctor');
              
          }
          $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
          if ($scope.Mypl.length > 0) {
              if ($scope.Mypl[0].worktype != undefined) {
                  $scope.Mypln.worktype = {};
                  $scope.Mypln.worktype.selected = {};
                  $scope.Mypln.worktype.selected.FWFlg = 'DD';
                  var Wtyps = $scope.worktypes.filter(function (a) {
                      return (a.FWFlg == 'DD')
                  });
                  $scope.Mypln.worktype.selected.id = Wtyps[0]['id'];
                  $scope.Mypln.subordinate = {};
                  $scope.Mypln.subordinate.selected = {};
                  if ($scope.view_MR == 1) {
                      $scope.Mypln.subordinate.selected.id = $scope.sfCode;
                  } else {
                      $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
                  }
                      $scope.cMTPDId = '_' + $scope.Mypl[0].subordinateid;
                      TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
                      if (TpTwns.length < 1) {
                          $ionicLoading.show({
                              template: 'Please Wait. Data is Sync...'
                          });
                          $scope.clearAll(false, $scope.cMTPDId);
                      } else {
                          $scope.loadDatas(false, $scope.cMTPDId);
                      }
                  $scope.Mypln.cluster = {};
                  $scope.Mypln.cluster.selected = {};
                  $scope.Mypln.cluster.selected.id = $scope.Mypl[0].clusterid;
                  $scope.Mypln.stockist = {};
                  $scope.Mypln.stockist.selected = {};
                  $scope.Mypln.stockist.selected.id = $scope.Myplns[0].stockistid;
               
                  $scope.$parent.fmcgData['jontWorkSelectedList'] = [];
                  if ($scope.Myplns[0].worked_with_code != undefined && $scope.Myplns[0].worked_with_code != "") {
                      var response2 = $scope.Myplns[0].worked_with_code.split("$$");
                      var jw = $scope.Myplns[0].worked_with_name.split(",");
                      for (var m = 0, leg = response2.length; m < leg; m++) {
                          $scope.$parent.fmcgData['jontWorkSelectedList'] = $scope.$parent.fmcgData['jontWorkSelectedList'] || [];
                          var pTemp = {};
                          pTemp.jointwork = response2[m].toString();
                          pTemp.jointworkname = jw[m].toString();
                          if (pTemp.jointwork.length !== 0)
                              $scope.$parent.fmcgData['jontWorkSelectedList'].push(pTemp);
                      }
                      $scope.Myplns[0].jontWorkSelectedList = $scope.$parent.fmcgData['jontWorkSelectedList'];
                  }
                  else
                      $scope.$parent.fmcgData['jontWorkSelectedList'] = $scope.Myplns[0].jontWorkSelectedList;

                  $fl = 1;
              }

          }

          $scope.$parent.fmcgData.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList || [];
         
          $scope.gridOptions = {
              data: 'fmcgData.jontWorkSelectedList',
              rowHeight: 50,
              enableRowSelection: false,
              rowTemplate: 'rowTemplate.html',
              enableCellSelection: true,
              enableColumnResize: true,
              plugins: [new ngGridFlexibleHeightPlugin()],
              showFooter: true,
              columnDefs: [{ field: 'jointworkname', displayName: 'Jointwork', enableCellEdit: false, cellTemplate: 'partials/jointworkCellTemplate.html' },
                  { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
          };
          $scope.removeRow = function () {
              var index = this.row.rowIndex;
              $scope.gridOptions.selectItem(index, false);
              $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
          };
      
          $scope.save = function () {
              $scope.data.worktype = $scope.Mypln.worktype.selected.id;
              $scope.data.FWFlg = $scope.Mypln.worktype.selected.FWFlg;
              $scope.data.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList;
                  if ($scope.view_MR != 1 && ($scope.Mypln.subordinate == undefined || $scope.Mypln.subordinate.selected.id == '')) {
                      Toast('Select the field force...');
                      return false;
                  }
                  if ($scope.Mypln.cluster == undefined || $scope.Mypln.cluster.selected.id == '') {
                      Toast('Select the Cluster...');
                      return false;
                  }
                  if ($scope.Mypln.stockist == undefined || $scope.Mypln.stockist.selected.id == '') {
                      Toast("Select " + $scope.StkCap);
                      return false;
                  }
                  if ($scope.Mypln.doctor == undefined || $scope.Mypln.doctor.selected.id == '') {
                      Toast("Select " + DrCap);
                      return false;
                  }
                  $scope.data.subordinateid = ($scope.view_MR == 1) ? userData.sfCode : $scope.Mypln.subordinate.selected.id;
                  $scope.data.clusterid = $scope.Mypln.cluster.selected.id;
                  $scope.data.stockistid = $scope.Mypln.stockist.selected.id;
                  $scope.data.stockistname = $scope.Mypln.stockist.name;
                 
                  $scope.data.retailorcode = $scope.Mypln.doctor.selected.id;
                  $scope.data.retailorname= $scope.Mypln.doctor.name;
                  if ($scope.Mypln.cluster.name == undefined)
                      $scope.Mypln.cluster.name = $filter('getValueforID')($scope.Mypln.cluster.selected.id, $scope.myTpTwns).name;
                  $scope.data.ClstrName = $scope.Mypln.cluster.name;
            
                  $scope.data.remarks = $scope.Mypln.remarks;
                  imagString = '';

                
                  $ionicLoading.show({ template: 'Saving...' });
                  if ($scope.photosList.length > 0) {
                      for (i = 0; i < $scope.photosList.length; i++) {
                          
                          if (i!= 0)
                              imagString += ", ";
                        
                         
                          var dataEVNT = $scope.photosList[i];
                          var imgUrl = dataEVNT.imgData;
                          $scope.savingData.EVNT = true;
                          var options = new FileUploadOptions();
                          options.fileKey = "imgfile";
                          options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                          imagString +=$scope.sfCode+"_"+options.fileName;
                          options.mimeType = "image/jpeg";
                          options.chunkedMode = false;
                          var uplUrl = baseURL + 'imgupload&sf_code=' + $scope.sfCode;
                          var ft = new FileTransfer();
                          ft.upload(imgUrl, uplUrl,
                                  function (response) {
                                      $scope.savingData.EVNT = false;
                                  },
                                  function () {
                                      $scope.savingData.EVNT = false;
                                      fmcgLocalStorage.addData("events", dataEVNT);
                                  }, options);
                          //fmcgLocalStorage.addData("events", $scope.photosList);
                      }
                  }
                  $scope.data.imagString = imagString;
          
                  fmcgAPIservice.addMAData('POST', 'dcr/save', "24", $scope.data).success(function (response) {

                      if (!response['success'] && response['type'] == 2) {
                          $state.go('fmcgmenu.home');
                          Toast(response['msg']);
                          $ionicLoading.hide();
                      }
                      else if (!response['success'] && response['type'] == 1) {
                          $scope.showConfirm = function () {

                              var confirmPopup = $ionicPopup.confirm({
                                  title: 'Warning !',
                                  template: response['msg']
                              });
                              confirmPopup.then(function (res) {
                                  if (res) {
                                      $ionicLoading.show({ template: 'Call Submitting. Please wait...' });
                                      fmcgAPIservice.addMAData('POST', 'dcr/save&replace', "24", $scope.data).success(function (response) {
                                          if (!response['success'])
                                              Toast(response['msg'], true);
                                          else {
                                              Toast("call Updated Successfully");

                                          }
                                          $state.go('fmcgmenu.home');
                                          $ionicLoading.hide();
                                      })

                                  }
                              });
                          };
                          $scope.showConfirm();
                          $ionicLoading.hide();

                      }
                      else {
                          if (!response['success'])
                              Toast(response['msg'], true);
                          else {
                              Toast("Call Submitted Successfully");
                              $scope.Mypln.doctor.selected.id = '';
                              $scope.Mypln.remarks='';
                              //  $scope.$parent.Myplns = fmcgLocalStorage.getData("mypln") || [];
                              $scope.PlanChk = 0;
                              $scope.$parent.PlanChk = 0;
                              $state.go('fmcgmenu.home');
                              $ionicLoading.hide();
                          }
                      }

                      });
                  }
         

      }])
        .controller('myTodyPlCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$filter', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $filter) {
            $scope.$parent.navTitle = "My Day Plan";
            $scope.clearData();
            $scope.remarks = "";
            $scope.data = {};
            $scope.fmcgData.value1 = 0;
            var temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            var $fl = 0;
            $scope.addStockist = function () {
                $state.go('fmcgmenu.addDistributor');
            }
            $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
            if ($scope.Mypl.length > 0) {
                if ($scope.Mypl[0].worktype != undefined) {
                    $scope.Mypln.worktype = {};
                    $scope.Mypln.worktype.selected = {};
                    $scope.Mypln.worktype.selected.id = $scope.Mypl[0].worktype;
                    $scope.Mypln.worktype.selected.FWFlg = $scope.Mypl[0].FWFlg;
                    $scope.Mypln.subordinate = {};
                    $scope.Mypln.subordinate.selected = {};
                    if ($scope.view_MR == 1) {
                        $scope.Mypln.subordinate.selected.id = $scope.sfCode;
                    } else {
                        $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
                    }
                    if ($scope.Mypl[0].FWFlg == 'F' || $scope.Mypl[0].FWFlg == 'DD' || $scope.Mypl[0].FWFlg == 'DS' || $scope.Mypl[0].FWFlg == 'HG') {
                        $scope.cMTPDId = '_' + $scope.Mypl[0].subordinateid;
                        TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
                        if (TpTwns.length < 1) {
                            $ionicLoading.show({
                                template: 'Please Wait. Data is Sync...'
                            });
                            $scope.clearAll(false, $scope.cMTPDId);
                        } else {
                            $scope.loadDatas(false, $scope.cMTPDId);
                        }
                    }
                    $scope.Mypln.cluster = {};
                    $scope.Mypln.cluster.selected = {};
                    $scope.Mypln.cluster.selected.id = $scope.Mypl[0].clusterid;
                    $scope.Mypln.stockist = {};
                    $scope.Mypln.stockist.selected = {};
                    $scope.Mypln.stockist.selected.id = $scope.Myplns[0].stockistid;
                    $scope.Mypln.remarks = $scope.Mypl[0].remarks;
                    $scope.$parent.fmcgData['jontWorkSelectedList'] = [];
                    if ($scope.Myplns[0].worked_with_code != undefined && $scope.Myplns[0].worked_with_code != "") {
                        var response2 = $scope.Myplns[0].worked_with_code.split("$$");
                        var jw = $scope.Myplns[0].worked_with_name.split(",");
                        for (var m = 0, leg = response2.length; m < leg; m++) {
                            $scope.$parent.fmcgData['jontWorkSelectedList'] = $scope.$parent.fmcgData['jontWorkSelectedList'] || [];
                            var pTemp = {};
                            pTemp.jointwork = response2[m].toString();
                            pTemp.jointworkname = jw[m].toString();
                            if (pTemp.jointwork.length !== 0)
                                $scope.$parent.fmcgData['jontWorkSelectedList'].push(pTemp);
                        }
                        $scope.Myplns[0].jontWorkSelectedList = $scope.$parent.fmcgData['jontWorkSelectedList'];
                    }
                    else
                        $scope.$parent.fmcgData['jontWorkSelectedList'] = $scope.Myplns[0].jontWorkSelectedList;

                    $fl = 1;
                }

            }

            $scope.$parent.fmcgData.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList || [];
            $scope.addProduct = function (selected) {
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
                columnDefs: [{ field: 'jointworkname', displayName: 'Jointwork', enableCellEdit: false, cellTemplate: 'partials/jointworkCellTemplate.html' },
                    { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
            };
            $scope.removeRow = function () {
                var index = this.row.rowIndex;
                $scope.gridOptions.selectItem(index, false);
                $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
            };
            if ($fl == 0) {

                $scope.Mypln.worktype = {};
                $scope.Mypln.worktype.selected = {};
                var Wtyps = $scope.worktypes.filter(function (a) {
                    return (a.FWFlg == "F")
                });
                $scope.Mypln.worktype.selected = Wtyps[0];
                $scope.Mypln.subordinate = {};
                $scope.Mypln.subordinate.selected = {};
                if ($scope.view_MR == 1) {
                    $scope.Mypln.subordinate.selected.id = $scope.sfCode;
                }
                $scope.Mypln.cluster = {};
                $scope.Mypln.cluster.selected = {};
                $scope.Mypln.remarks = "";

            }
            $scope.save = function () {
                $scope.data.worktype = $scope.Mypln.worktype.selected.id;
                $scope.data.FWFlg = $scope.Mypln.worktype.selected.FWFlg;
                $scope.data.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList;
                if ($scope.Mypln.worktype.selected.FWFlg == 'F' || $scope.Mypln.worktype.selected.FWFlg == 'HG' || $scope.Mypln.worktype.selected.FWFlg == 'DD' || $scope.Mypln.worktype.selected.FWFlg == 'DS') {

                    if ($scope.view_MR != 1 && ($scope.Mypln.subordinate == undefined || $scope.Mypln.subordinate.selected.id == '')) {
                        Toast('Select the field force...');
                        return false;
                    }
                    if ($scope.Mypln.cluster == undefined || $scope.Mypln.cluster.selected.id == '') {
                        Toast('Select the Cluster...');
                        return false;
                    }
                    if ($scope.Mypln.stockist == undefined || $scope.Mypln.stockist.selected.id == '') {
                        Toast("Select " + $scope.StkCap);
                        return false;
                    }
                    $scope.data.subordinateid = ($scope.view_MR == 1) ? userData.sfCode : $scope.Mypln.subordinate.selected.id;
                    $scope.data.clusterid = $scope.Mypln.cluster.selected.id;
                    $scope.data.stockistid = $scope.Mypln.stockist.selected.id;

                    if ($scope.Mypln.cluster.name == undefined)
                        $scope.Mypln.cluster.name = $filter('getValueforID')($scope.Mypln.cluster.selected.id, $scope.myTpTwns).name;
                    $scope.data.ClstrName = $scope.Mypln.cluster.name;
                } else {
                    $scope.data.subordinateid = '';
                    $scope.data.clusterid = '';
                    $scope.data.ClstrName = '';
                    $scope.data.stockistid = '';

                }
                $scope.data.remarks = $scope.Mypln.remarks;
                $ionicLoading.show({ template: 'Saving...' });
                window.localStorage.removeItem("mypln");
                window.localStorage.removeItem("myplnQ");
                $scope.Myplns = [];
                $scope.Myplns.push($scope.data);
                fmcgLocalStorage.createData("mypln", $scope.Myplns);
                fmcgAPIservice.addMAData('POST', 'dcr/save', "3", $scope.data).success(function (response) {
                    if (response.success) {
                        Toast("My Today Plan Submited Successfully");
                        $ionicLoading.hide();
                    }
                    else {
                        Toast("Leave Post Already Updated");
                        $ionicLoading.hide();
                    }
                    $scope.$parent.Myplns = fmcgLocalStorage.getData("mypln") || [];
                    $scope.PlanChk = 0;
                    $scope.$parent.PlanChk = 0;
                    $state.go('fmcgmenu.home');


                }).error(function () {
                    window.localStorage.setItem("myplnQ", JSON.stringify($scope.Myplns));
                    Toast("No Internet Connection! My Day Plan Saved in Outbox");
                    $ionicLoading.hide();
                    $state.go('fmcgmenu.home');
                });
            };

        }])
        .controller('DyRmksCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
            $scope.$parent.navTitle = "Day Activity Remarks";
            $scope.data = {};
            $scope.dyRmks = {};

            $scope.MyDyRmks = fmcgLocalStorage.getData("MyDyRmks") || [];
            if ($scope.MyDyRmks.length == 0) {
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_report",
                    '["remarks","Half_Day_FW as Halfday"]', , '["Activity_Date=convert(varchar,getDate(),101)"]'
                ]).success(function (response) {
                    $scope.MyDyRmks = response;
                    if (response.length && response.length > 0 && Array.isArray(response)) {
                        fmcgLocalStorage.createData("MyDyRmks", response);
                        $scope.dyRmks.remarks = response[0].remarks;
                        $scope.SlHlfDys = response[0].Halfday;
                    }
                }).error(function () {
                    Toast("No Internet Connection!.");
                });
            }
            else {
                $scope.dyRmks.remarks = $scope.MyDyRmks[0].remarks;
                $scope.SlHlfDys = $scope.MyDyRmks[0].Halfday;
            }

            $scope.HalfdayWorks = fmcgLocalStorage.getData("halfdayworks") || [];

            $scope.setHlfDy = function (item) {
                for (var di = 0; di < $scope.HalfdayWorks.length; di++) {
                    $scope.HalfdayWorks[di].checked = false;
                    if ($scope.SlHlfDys.indexOf($scope.HalfdayWorks[di].id) > -1)
                        $scope.HalfdayWorks[di].checked = true;
                }
            }
            if ($scope.HalfdayWorks.length == 0) {
                fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
                    '["type_code as id", "Wtype as name"]'
                            , , '["isnull(Active_flag,0)=0","isnull(HalfDyNeed,0)=1"]', , , , 0]).success(function (response) {
                                $scope.HalfdayWorks = response;
                                $scope.setHlfDy();
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("halfdayworks", response);
                            });
            } else {
                $scope.setHlfDy();
            }
            $scope.selHlfDy = function (item) {
                if (item.checked) {
                    if ($scope.AppEnver != ".Net")
                        if ($scope.SlHlfDys.indexOf(item.id + ',') < 0)
                            $scope.SlHlfDys += item.id + ',';
                        else
                            if ($scope.SlHlfDys.indexOf(item.name + ' ( ' + item.id + ' ),') < 0)
                                $scope.SlHlfDys += item.name + ' ( ' + item.id + ' ),';
                }
                else {
                    $scope.SlHlfDys = $scope.SlHlfDys.replace((($scope.AppEnver != ".Net") ? item.id : item.name + ' ( ' + item.id + ' )') + ',', '');
                }
            }
            $scope.save = function () {
                if ($scope.dyRmks.remarks == undefined || $scope.dyRmks.remarks == '') {
                    Toast('Enter Day Activity Remarks...')
                }
                else {
                    $scope.data.remarks = $scope.dyRmks.remarks;
                    $scope.data.Halfday = $scope.SlHlfDys;

                    MyDyRmks = [{}];
                    MyDyRmks[0]["remarks"] = $scope.dyRmks.remarks;
                    MyDyRmks[0]["Halfday"] = $scope.SlHlfDys
                    window.localStorage.removeItem("MyDyRmks");
                    fmcgLocalStorage.createData("MyDyRmks", MyDyRmks);

                    fmcgAPIservice.updateRemark('POST', 'dcr/updRem', $scope.data).success(function (response) {
                        if (response.success) {
                            Toast("Day Activity Remarks Updated Successfully");
                        }
                        else
                            Toast(response['msg'], true);
                    })
                            .error(function () {
                                window.localStorage.setItem("MyDyRmksQ", JSON.stringify(MyDyRmks));
                                Toast("No Internet Connection! Saved in Outbox");
                                $state.go('fmcgmenu.home');
                            })
                    ;
                    $state.go('fmcgmenu.home');
                }
            }
        }])

      .controller('addRouteCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
          $scope.$parent.navTitle = "Add Route";
          $scope.data = {};
          if ($scope.view_MR != 1) {
              $scope.fmcgData.subordinate = {};
              $scope.fmcgData.subordinate.selected = {};
              $scope.fmcgData.subordinate.selected.id = $scope.sfCode;
              $scope.loadDatas(false, '_' + $scope.sfCode)
          }
          else {
              $scope.fmcgData.subordinate = {};
              $scope.fmcgData.subordinate.selected = {};
              $scope.fmcgData.subordinate.selected.id = $scope.sfCode;
          }
          if ($scope.fmcgData.subordinate !== undefined) {
              $scope.cMTPDId = '_' + $scope.fmcgData.subordinate.selected.id;
          }

          $scope.save = function () {

              if ($scope.data.route == undefined || $scope.data.route == '') {
                  Toast("Select the Route Code");
                  return false;
              }
              if ($scope.data.routeName == undefined || $scope.data.routeName == '') {
                  Toast("Select the Route Name");
                  return false;
              }
              if ($scope.fmcgData.dsm == undefined || $scope.fmcgData.dsm.selected.id == '') {
                  Toast("Select the DSM Name");
                  return false;
              }
              $scope.data.Dist_Name = $scope.Mypln.stockist.selected.id;
              $scope.data.SF_Code = $scope.fmcgData.dsm.selected.id;
              $scope.data.town_code = $scope.fmcgData.dsm.town;
              fmcgAPIservice.addMAData('POST', 'dcr/save', "22", $scope.data).success(function (response) {
                  if (response.success) {
                      Toast("Route Added Successfully");
                      if ($scope.fmcgData.RouteSel == 0)
                          $state.go('fmcgmenu.addSurvey');
                      else
                          $state.go('fmcgmenu.addULDoctor');
                      $scope.clearIdividual(8, 1, '_' + $scope.fmcgData.subordinate.selected.id);
                  }
              });
          }
          $scope.goBack = function () {
              if($scope.fmcgData.RouteSel==0)
                  $state.go('fmcgmenu.addSurvey');
              else
              $state.go('fmcgmenu.addULDoctor');
          }
      }])
            .controller('addSurveyCtrl', ['$rootScope', '$scope', '$state', '$ionicModal', '$ionicScrollDelegate', '$ionicPopup', '$location', 'fmcgLocalStorage', 'fmcgAPIservice', '$ionicSideMenuDelegate', 'geolocation', '$interval', 'notification', '$timeout', '$ionicLoading', '$filter', function ($rootScope, $scope, $state, $ionicModal, $ionicScrollDelegate, $ionicPopup, $location, fmcgLocalStorage, fmcgAPIservice, $ionicSideMenuDelegate, geolocation, $interval, notification, $timeout, $ionicLoading, $filter) {

        $scope.$parent.navTitle = "Survey";
        $scope.wlkg_View = $scope.doctors.length;
        $scope.brandShow = 0;
        $scope.clearData();
        $scope.data = {};
        $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
        if ($scope.Mypl.length > 0) {
            if ($scope.Mypl[0].worktype != undefined) {
                $scope.Mypln.worktype = {};
                $scope.Mypln.worktype.selected = {};
                $scope.Mypln.worktype.selected.FWFlg = 'DS';
                var Wtyps = $scope.worktypes.filter(function (a) {
                    return (a.FWFlg == 'DS')
                });
                $scope.Mypln.worktype.selected.id = Wtyps[0]['id'];

                $scope.Mypln.subordinate = {};
                $scope.Mypln.subordinate.selected = {};
                if ($scope.view_MR == 1) {
                    $scope.Mypln.subordinate.selected.id = $scope.sfCode;
                } else {
                    $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
                }
                $scope.Mypln.stockist = {};
                $scope.Mypln.stockist.selected = {};
                $scope.Mypln.stockist.selected.id = $scope.Mypl[0].stockistid
                
                $scope.Mypln.cluster = {};
                $scope.Mypln.cluster.selected = {};
                $scope.Mypln.cluster.selected.id = $scope.Mypl[0].clusterid;
                $scope.Mypln.cluster.name = $scope.Mypl[0].ClstrName;
                $scope.$parent.fmcgData.value1 = 0;

            }

        }
        $rootScope.extraDetails = $rootScope.extraDetails || [];

        if ($rootScope.extraDetails.length != 0)
            $scope.brandShow = 1;
        else
            $scope.brandShow = 0;
        $scope.done = function () {

            if ($scope.RCPA.CpmptId == '' || $scope.RCPA.CpmptId == undefined) {
                Toast('Select the Competitor Name');
                return false;
            }
            if ($scope.RCPA.CpmptQty == '' || $scope.RCPA.CpmptQty == undefined) {
                Toast('Select the Competitor Qty');
                return false;
            }
            $ionicScrollDelegate.scrollTop();
            $scope.brandShow = 1;
            $rootScope.extraDetails.push({
         CpmptId: $scope.RCPA.CpmptId, CpmptName: $scope.RCPA.CpmptName, CpmptQty: $scope.RCPA.CpmptQty});
        }
        $scope.addFields = function () {
            $scope.brandShow = 0;
            $scope.RCPA.CpmptId = '';
            $scope.RCPA.CpmptName = '';
            $scope.RCPA.CpmptQty = '';
        }
        $scope.delete = function (index) {
            $ionicScrollDelegate.scrollTop();
            $rootScope.extraDetails.splice(index, 1);
        }

        $scope.goBack = function () {
            $state.go('fmcgmenu.addNew');
        }
        $scope.$parent.fmcgData.jontWorkSelectedList = $scope.Mypl[0].jontWorkSelectedList || [];
        $scope.addProduct = function (selected) {
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
            columnDefs: [{ field: 'jointworkname', displayName: 'Jointwork', enableCellEdit: false, cellTemplate: 'partials/jointworkCellTemplate.html' },
                { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
        };
        $scope.removeRow = function () {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
        };
        $scope.gridOptions1 = {
            data: 'extraDetails',
            rowHeight: 50,
            enableRowSelection: false,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: [{ field: 'CpmptName', displayName: 'Comp Name', enableCellEdit: false, cellTemplate: 'partials/jointworkCellTemplate.html' },
                { field: 'CpmptQty', displayName: 'Comp Qty', enableCellEdit: false, cellTemplate: 'partials/jointworkCellTemplate.html' },
                { field: 'remove1', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton1.html', width: 50 }]
        };
        $scope.removeRow1 = function () {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.competitors.splice(index, 1);
        };
        $scope.loadDatas(false, '_' + $scope.sfCode)
        $scope.precall.doctor = undefined;
        $scope.NmCap = $scope.NLCap;
        $scope.CusOrder = $scope.CusOrder;
        if ($scope.view_MR != 1) {
            $scope.Mypln.subordinate = {};
            $scope.Mypln.subordinate.selected = {};
            $scope.Mypln.subordinate.selected.id = $scope.sfCode;
            $scope.loadDatas(false, '_' + $scope.sfCode)
        }
        else {
            $scope.Mypln.subordinate = {};
            $scope.Mypln.subordinate.selected = {};
            $scope.Mypln.subordinate.selected.id = $scope.sfCode;
        }
        if ($scope.Mypln.subordinate !== undefined) {
            $scope.cMTPDId = '_' + $scope.Mypln.subordinate.selected.id;
        }
   
        $scope.fmcgData.RouteAdd = 0;
        $scope.addRoute = function (stat) {
            $scope.fmcgData.RouteSel = 0;
            $state.go('fmcgmenu.addRoute');
        }

        if ($scope.Mypln.cluster != undefined) {
            $scope.fmcgData.RouteAdd = 1;
        }
        $scope.save = function () {
            if ($scope.data.name == "" || $scope.data.name == undefined) {
                Toast('Enter the Name...');
                return false;
            }
            if ($scope.data.address == "" || $scope.data.address == undefined) {
                Toast('Enter the Address...');
                return false;
            }

            $scope.data.wlkg_sequence = {};
            $scope.data.wlkg_sequence.selected = {};
            $scope.data.townid = $scope.Mypln.cluster.selected.id;
            $scope.data.townname = $scope.Mypln.cluster.name;
            $scope.data.subordinate = $scope.Mypln.subordinate.selected.id;
            if ($scope.CusOrder == 1 && $scope.wlkg_View != 0) {
                if ($scope.Mypln.handover == undefined || $scope.Mypln.handover.selected == undefined) {
                    Toast('Select the Existing Customer');
                    return false;
                }
                $scope.data.wlkg_sequence.selected.id = $scope.Mypln.handover.selected.id;
            }
            $scope.data.qulification = {};
            $scope.data.qulification.selected = {};
     
            $scope.data.qulification.selected.id = 'samp';

            $scope.data.class = {};
            $scope.data.class.selected = {};
            if ($scope.fmcgData.class == undefined || $scope.fmcgData.class.selected == undefined) {
                Toast('Select the Category...');
                return false;
            }
            $scope.data.class.selected.id = $scope.fmcgData.class.selected.id;

            $scope.data.speciality = {};
            $scope.data.speciality.selected = {};
            if ($scope.fmcgData.speciality == undefined || $scope.fmcgData.speciality.selected == undefined) {
                Toast('Select the Speciality...');
                return false;
            }
            $scope.data.speciality.selected.id = $scope.fmcgData.speciality.selected.id;

            $scope.data.category = {};
            $scope.data.category.selected = {};
              $scope.data.worktype=$scope.Mypln.worktype.selected.id;
          $scope.data.category.selected.id = '';//$scope.fmcgData.category.selected.id;
            $scope.data.jontWorkSelectedList = $scope.fmcgData.jontWorkSelectedList;
            $scope.data.location = $scope.fmcgData.location;
            var tDate = new Date();
            $scope.data.entryDate = tDate;
            $scope.data.modifiedDate = tDate;
            $scope.data.RCPAdetails = $rootScope.extraDetails;
            $ionicLoading.show({
                template: 'Please Wait. Data is Sync...'
            })

            fmcgAPIservice.addMAData('POST', 'dcr/save', "23", $scope.data).success(function (response) {
                if (!response['success'] && response['type'] == 2) {
                    $state.go('fmcgmenu.home');
                    Toast(response['msg']);
                    $ionicLoading.hide();
                }
                else if (!response['success'] && response['type'] == 1) {
                    $scope.showConfirm = function () {

                        var confirmPopup = $ionicPopup.confirm({
                            title: 'Warning !',
                            template: response['msg']
                        });
                        confirmPopup.then(function (res) {
                            if (res) {
                                $ionicLoading.show({ template: 'Call Submitting. Please wait...' });
                                fmcgAPIservice.addMAData('POST', 'dcr/save&replace', "23", $scope.data).success(function (response) {
                                    if (!response['success'])
                                        Toast(response['msg'], true);
                                    else {
                                        Toast("call Updated Successfully");

                                    }
                                    $state.go('fmcgmenu.home');
                                    $ionicLoading.hide();
                                })

                            }
                        });
                    };
                    $scope.showConfirm();
                    $ionicLoading.hide();

                }
                else {
                    if (!response['success'])
                        Toast(response['msg'], true);
                    else {
                            Toast("Call Submitted Successfully");
                        $scope.data = {};
                        if ($scope.AppTyp == 1) {
                            $scope.clearIdividual(0, 1)
                        }
                        else {
                            $scope.clearIdividual(3, 1);
                        }
                        $scope.data = {};
                        if ($scope.fmcgData.cluster == undefined) {
                            $scope.fmcgData.cluster = {};
                            $scope.fmcgData.cluster.selected = {};

                        }
                        if ($scope.fmcgData.speciality == undefined) {
                            $scope.fmcgData.speciality = {};
                            $scope.fmcgData.speciality.selected = {};

                        }
                        if ($scope.fmcgData.category == undefined) {
                            $scope.fmcgData.category = {};

                            $scope.fmcgData.category.selected = {};
                        }
                    }
                
                    $state.go('fmcgmenu.home');
                    $ionicLoading.hide();
                }
                

            });
        };
    }])
        .controller('addULDocCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
            $scope.$parent.navTitle = "Add " + $scope.NLCap;
            $scope.wlkg_View = $scope.doctors.length;

            $scope.clearData();
            $scope.data = {};
            $scope.goBack = function () {
                var fl = window.localStorage.getItem("addRetailorflg");

                if (fl == 0)
                    $state.go('fmcgmenu.addDoorToDoor');
                else if(fl==1)
                    $state.go('fmcgmenu.screen2');
              else
                    $state.go('fmcgmenu.home');
            }
            $scope.loadDatas(false, '_' + $scope.sfCode)
            $scope.precall.doctor = undefined;
            // $scope.Mypln.cluster = undefined;
            $scope.NmCap = $scope.NLCap;
            $scope.CusOrder = $scope.CusOrder;
            if ($scope.view_MR != 1) {
                $scope.Mypln.subordinate = {};
                $scope.Mypln.subordinate.selected = {};
                $scope.Mypln.subordinate.selected.id = $scope.sfCode;
                $scope.loadDatas(false, '_' + $scope.sfCode)
            }
            else {
                $scope.Mypln.subordinate = {};
                $scope.Mypln.subordinate.selected = {};
                $scope.Mypln.subordinate.selected.id = $scope.sfCode;
                // $scope.Mypln.subordinate = undefined
            }
            if ($scope.Mypln.subordinate !== undefined) {
                $scope.cMTPDId = '_' + $scope.Mypln.subordinate.selected.id;
            }
            //  alert($scope.view_MR);
            // alert(  $scope.Mypln.subordinate.selected.id)
            $scope.fmcgData.RouteAdd = 0;
            $scope.addRoute = function (stat) {
                $scope.fmcgData.RouteSel = 1;
                $state.go('fmcgmenu.addRoute');
            }

            if ($scope.Mypln.cluster != undefined) {
                $scope.fmcgData.RouteAdd = 1;
            }
            $scope.save = function () {
                if ($scope.data.name == "" || $scope.data.name == undefined) {
                    Toast('Enter the Name...');
                    return false;
                }
                if ($scope.data.address == "" || $scope.data.address == undefined) {
                    Toast('Enter the Address...');
                    return false;
                }
                $scope.data.cluster = {};

                $scope.data.wlkg_sequence = {};
                $scope.data.cluster.selected = {};
                $scope.data.wlkg_sequence.selected = {};
                //                    if ($scope.precall.doctor == undefined || $scope.precall.doctor.selected == undefined)
                //                    {
                //                        Toast('Select the Existing Customer');
                //                        return false;
                //                    }
                $scope.data.cluster.selected.id = $scope.Mypln.cluster.selected.id;
                if ($scope.CusOrder == 1 && $scope.wlkg_View != 0) {
                    if ($scope.Mypln.handover == undefined || $scope.Mypln.handover.selected == undefined) {
                        Toast('Select the Existing Customer');
                        return false;
                    }
                    $scope.data.wlkg_sequence.selected.id = $scope.Mypln.handover.selected.id;
                }
                $scope.data.qulification = {};
                $scope.data.qulification.selected = {};
                //if ($scope.fmcgData.qulification == undefined || $scope.fmcgData.qulification.selected == undefined) {
                //                        Toast('Select the Qulification...');
                //                        return false;
                // }
                //                    $scope.data.qulification.selected.id = $scope.fmcgData.qulification.selected.id;
                $scope.data.qulification.selected.id = 'samp';

                $scope.data.class = {};
                $scope.data.class.selected = {};
                if ($scope.fmcgData.class == undefined || $scope.fmcgData.class.selected == undefined) {
                    Toast('Select the Category...');
                    return false;
                }
                $scope.data.class.selected.id = $scope.fmcgData.class.selected.id;

                $scope.data.speciality = {};
                $scope.data.speciality.selected = {};
                if ($scope.fmcgData.speciality == undefined || $scope.fmcgData.speciality.selected == undefined) {
                    Toast('Select the Speciality...');
                    return false;
                }
                $scope.data.speciality.selected.id = $scope.fmcgData.speciality.selected.id;

                $scope.data.category = {};
                $scope.data.category.selected = {};
                //$scope.fmcgData.category.selected.id = '';
                //if ($scope.fmcgData.category == undefined || $scope.fmcgData.category.selected == undefined)
                //{
                //    Toast('Select the Category...');
                //    return false;
                //}
                $scope.data.category.selected.id = '';//$scope.fmcgData.category.selected.id;

                fmcgAPIservice.addMAData('POST', 'dcr/save', "2", $scope.data).success(function (response) {
                    if (response.success)
                        Toast($scope.NLCap + " Added Successfully");
                    $scope.data = {};
                    if ($scope.AppTyp == 1) {
                        $scope.clearIdividual(0, 1)
                    }
                    else {
                        $scope.clearIdividual(3, 1);
                    }
                    $scope.data = {};
                    if ($scope.fmcgData.cluster == undefined) {
                        $scope.fmcgData.cluster = {};
                        $scope.fmcgData.cluster.selected = {};

                    }
                    if ($scope.fmcgData.speciality == undefined) {
                        $scope.fmcgData.speciality = {};
                        $scope.fmcgData.speciality.selected = {};

                    }
                    if ($scope.fmcgData.category == undefined) {
                        $scope.fmcgData.category = {};

                        $scope.fmcgData.category.selected = {};
                    }

                });
                var fl = window.localStorage.getItem("addRetailorflg");

                if (fl == 0)
                    $state.go('fmcgmenu.addDoorToDoor');
                else if (fl == 1)
                    $state.go('fmcgmenu.screen2');
                else
                    $state.go('fmcgmenu.home');
            };
        }])
    .controller('addDistributorCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
        $scope.$parent.navTitle = "Add Distributor";
        $scope.fmcgData.district = {};
        $scope.fmcgData.district.selected = {};
        $scope.data = {};
        if ($scope.view_MR != 1) {
            $scope.fmcgData.subordinate = {};
            $scope.fmcgData.subordinate.selected = {};
            $scope.fmcgData.subordinate.selected.id = $scope.sfCode;
            $scope.loadDatas(false, '_' + $scope.sfCode)
        }
        else {
            $scope.fmcgData.subordinate = {};
            $scope.fmcgData.subordinate.selected = {};
            $scope.fmcgData.subordinate.selected.id = $scope.sfCode;
        }
        if ($scope.fmcgData.subordinate !== undefined) {
            $scope.cMTPDId = '_' + $scope.fmcgData.subordinate.selected.id;
        }
        $scope.save = function () {
            if ($scope.data.name == "" || $scope.data.name == undefined) {
                Toast('Enter the Name...');
                return false;
            }
            if ($scope.data.address == "" || $scope.data.address == undefined) {
                Toast('Enter the Address...');
                return false;
            }
            if ($scope.fmcgData.district.selected.id == "" || $scope.fmcgData.district.selected.id == undefined) {
                Toast('Select the District...');
                return false;
            }
            $scope.data.dist_code = $scope.fmcgData.district.selected.id;
            $scope.data.dist_name = $scope.fmcgData.district.name;
            fmcgAPIservice.addMAData('POST', 'dcr/save', "25", $scope.data).success(function (response) {
                if (response.success)
                    Toast( "Distributor Added Successfully");
                $scope.data = {};
                $state.go('fmcgmenu.myPlan');
                $scope.clearIdividual(2, 1);
        $scope.clearIdividual(8, 1, '_' + $scope.fmcgData.subordinate.selected.id);
               
                $scope.data = {};
            });
           
        };
    }])
         .controller('ExpenseEntryCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {


             $scope.update = 0;
             $scope.data = {};
             $scope.$parent.navTitle = "Expense Entry ";
             $scope.getTP = {};
             $scope.getExpense = {};
             $scope.extraDetails = [{
                 "parameter": "", "type": true, "amount": ""

             }];
             view();
             function view() {
                 $ionicLoading.show({
                     template: 'Loading...'
                 });
                 fmcgAPIservice.getDataList('POST', 'table/list', ["getTPDet"])
                                       .success(function (response) {
                                           window.localStorage.removeItem("getTP");
                                           fmcgLocalStorage.createData("getTP", response);
                                           if (response.length == 0)
                                               console.log("no record");
                                           else {
                                               $scope.getTPlength = response.length;
                                               if ($scope.getTPlength != 0) {
                                                   $scope.worktype = response[0].WorkType_Name;
                                                   $scope.place = response[0].place;
                                               }
                                           }
                                       });

                 fmcgAPIservice.getDataList('POST', 'table/list', ["getExpenseDet"])
                                          .success(function (response) {
                                              window.localStorage.removeItem("getExpense");

                                              if (response.length == 0) {
                                                  console.log("no records");
                                                  $ionicLoading.hide();
                                              }
                                              else {
                                                  $scope.update = 1;
                                                  expenseDet = response.extraDetails;
                                                  head = response.head;
                                                  $scope.data.tot = head.Expense_Total;
                                                  $scope.data.allowance = head.Expense_Allowance;
                                                  $scope.data.fare = head.Expense_Fare;
                                                  $scope.data.distance = head.Expense_Distance;
                                                  $scope.amttot = 0;
                                                  for (var key in expenseDet) {
                                                      if (expenseDet[key]['type'] == 0) {
                                                          expenseDet[key]['type'] = true;
                                                          amt = expenseDet[key]['amount'];

                                                      }
                                                      else {
                                                          amt = -expenseDet[key]['amount'];
                                                          expenseDet[key]['type'] = false;
                                                      }
                                                      $scope.amttot = amt + $scope.amttot;
                                                  }
                                                  if (expenseDet.length != 0)
                                                      $scope.extraDetails = expenseDet;
                                                  $ionicLoading.hide();
                                              }
                                          });
             }

             var date = new Date();
             $scope.dat = $filter('date')(new Date(), 'dd/MM/yyyy');

             $scope.date = $scope.dat;


             $scope.calculateTot = function () {
                 var extraDetails = $scope.extraDetails;
                 var amtTot = 0;
                 for (var key in extraDetails) {
                     if (extraDetails[key]['type'] == true)
                         amtTot = extraDetails[key]['amount'] + amtTot;
                     if (extraDetails[key]['type'] == false)
                         amtTot = -extraDetails[key]['amount'] + amtTot;
                 }
                 $scope.amttot = amtTot;
             }
             $scope.total = function () {
                 if (this.data.fare == undefined)
                     $scope.data.tot = 0 + this.data.allowance;
                 else if (this.data.allowance == undefined)
                     $scope.data.tot = this.data.fare + 0;
                 else
                     $scope.data.tot = this.data.fare + this.data.allowance;
             }
             $scope.addFields = function () {
                 $scope.extraDetails.push({ parameter: '', type: true, amount: '' });
             }
             $scope.delete = function (index) {
                 $scope.extraDetails.splice(index, 1);
             }
             //  $scope.data.inactive = true;

             //ng-disabled="data.inactive"
             $scope.getTP = fmcgLocalStorage.getData("getTP") || [];

             $scope.getTPlength = $scope.getTP.length;
             $scope.submit = function () {
                 $scope.getTP = fmcgLocalStorage.getData("getTP") || [];

                 $scope.getTPlength = $scope.getTP.length;
                 $scope.data.worktype = addQuotes($scope.getTP[0].Work_Type);
                 if ($scope.getTP[0].Work_Type == undefined) {
                     Toast('Not update Dcr Entry...');
                     return false;
                 }
                 $ionicLoading.show({
                     template: 'Loading...'
                 });
                 $scope.data.worktype_name = addQuotes($scope.getTP[0].WorkType_Name);
                 $scope.data.placeno = addQuotes($scope.getTP[0].placeNo);
                 $scope.data.place = addQuotes($scope.getTP[0].place);
                 $scope.data.sfCode = addQuotes($scope.getTP[0].Sf_Code);
                 $scope.data.sfName = addQuotes($scope.getTP[0].Sf_Name);
                 $scope.data.date = addQuotes($scope.date);
                 $scope.data.expense_total = $scope.tot;
                 $scope.data.extraDetails = $scope.extraDetails;
                 $scope.data.additionalTot = $scope.amttot;
                 $scope.data.allowance = $scope.data.allowance;
                 $scope.data.fare = $scope.data.fare;
                 $scope.data.distance = $scope.data.distance;
                 fmcgAPIservice.addMAData('POST', 'dcr/save&update=' + $scope.update, "10", $scope.data).success(function (response) {
                     if (response.success) {
                         $ionicLoading.hide();
                         Toast("Expense Entered Successfully");
                         $state.go('fmcgmenu.home');
                     }
                 }).error(function () {
                     Toast("No Internet Connection! Try Again.");
                     $ionicLoading.hide();
                 });
             }


         }])
     .controller('TourPlanCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {
         $scope.$parent.navTitle = "Add TP Entry";
         var temp = window.localStorage.getItem("loginInfo");
         var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
         var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id == "") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
         $scope.clearIdividual(22, 1, '_' + sRSF);
         $scope.update = 0;
         $scope.Mypln.worktype = {};
         $scope.Mypln.worktype.selected = {};
         $scope.Mypln.stockist = {};
         $scope.Mypln.stockist.selected = {};
         $scope.bac = "red";
         //  var Wtyps = $scope.worktypes.filter(function (a) {
         //   return (a.FWFlg == "F")
         // });
         // $scope.Mypln.worktype.selected = Wtyps[0];
         $scope.Mypln.subordinate = {};
         $scope.Mypln.subordinate.selected = {};
         if ($scope.view_MR == 1) {
             $scope.Mypln.subordinate.selected.id = $scope.sfCode;
         } else {
             $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
             if ($scope.Mypl.length != 0)
                 $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
         }

         if ($scope.Mypln.worktype.selected.FWFlg == 'F' && $scope.Mypln.subordinate.selected.id !== undefined) {
             $scope.cMTPDId = '_' + $scope.Mypln.subordinate.selected.id;
             TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
             if (TpTwns.length < 1) {
                 $ionicLoading.show({
                     template: 'Please Wait. Data is Sync...'
                 });
                 $scope.clearAll(false, $scope.cMTPDId);
             } else {
                 $scope.loadDatas(false, $scope.cMTPDId);
             }
         }
         $scope.BrndSumm.stockist = {};
         $scope.BrndSumm.stockist.selected = {};
         $scope.Mypln.cluster = {};
         $scope.Mypln.cluster.selected = {};
         var date = new Date();
         $scope.month = date.getMonth() + 2;
         $scope.year = date.getFullYear();
         $scope.day = date.getDate();
         var date1 = new Date($scope.SFTPDate.date);
         //    date1 = new Date("2016-12-16")
         $scope.disableMonth = date1.getMonth() + 1;
         $scope.disableYear = date1.getFullYear();
         $scope.disableDay = date1.getDate();
         var disableDates = [];
         if ($scope.disableYear == $scope.year) {
             if ($scope.disableMonth == $scope.month) {
                 for ($i = 01; $i < $scope.disableDay; $i++) {
                     disableDates.push($scope.year + '-' + $scope.month + '-' + $i);
                 }
             }
             if ($scope.disableMonth > $scope.month) {
                 for ($i = 01; $i <= $scope.noOfMonth; $i++) {
                     disableDates.push($scope.year + '-' + $scope.month + '-' + $i);
                 }
             }
         }

         if ($scope.disableYear > $scope.year) {
             for ($i = 01; $i <= $scope.noOfMonth; $i++) {
                 disableDates.push($scope.year + '-' + $scope.month + '-' + $i);
             }
         }
         $scope.noOfMonth = new Date($scope.year, $scope.month, 0).getDate();

         $scope.noOfMonthsubmit = $scope.noOfMonth - disableDates.length;
         var tourPlan = fmcgLocalStorage.getData("Tour_Plan") || [];
         $scope.submitApproval = 0;
         if (tourPlan.length == $scope.noOfMonthsubmit)
             $scope.submitApproval = 1;
         $scope.show = 0;
         $scope.clearData();
         if (tourPlan.length > 0)
             $scope.Confirmed = tourPlan[0].Confirmed;
         if ($scope.Confirmed == null)
             $scope.Confirmed = 0;

         $scope.data = {};
         $scope.modal = $ionicModal;
         $scope.modal.fromTemplateUrl('partials/TourPlanModal.html', function (modal) {
             $scope.modal = modal;
         }, {
             animation: 'slide-in-up',
             focusFirstInput: true
         });
         var temp = window.localStorage.getItem("loginInfo");
         var $fl = 0;
         $scope.datefn = function () {
             $scope.options = {
                 defaultDate: "'" + $scope.year + "-" + $scope.month + "-" + $scope.day + "'",
                 minDate: "'" + $scope.year + "-" + $scope.month + "-" + 1 + "'",
                 maxDate: "'" + $scope.year + "-" + $scope.month + "-" + $scope.noOfMonth + "'",
                 disabledDates: [],
                 dayNamesLength: 3, // 1 for "M", 2 for "Mo", 3 for "Mon"; 9 will show full day names. Default is 1.
                 mondayIsFirstDay: true, //set monday as first day of week. Default is false
                 eventClick: function (date) {
                     if (date.day < 10)
                         date.day = "0" + date.day;
                     date.month = date.month + 1;
                     if (date.month < 10)
                         date.month = "0" + date.month;
                     $scope.modal.update = function () {
                         $scope.show = 1;
                         updateData();
                     }
                     function updateData() {

                         $scope.data.date = date.year + "-" + date.month + "-" + date.day;
                         $scope.modal.date = date.day + "/" + date.month + "/" + date.year;
                         $scope.clickdate = date.day + "/" + date.month + "/" + date.year;
                         var tourPlan = fmcgLocalStorage.getData("Tour_Plan");

                         for (var key in tourPlan) {
                             if ($scope.data.date === (tourPlan[key]['date'])) {
                                 $scope.modal.SF_type = $scope.SF_type;
                                 $scope.modal.tourPlan = tourPlan[key];
                                 $scope.Mypln.subordinate.selected.id = $scope.sfCode;
                                 //  $scope.data.market = tourPlan[key]['market'];
                                 $scope.data.remarks = tourPlan[key]['remarks'];
                                 $scope.Mypln.worktype.selected.id = tourPlan[key]['worktype_code'];
                                 $scope.Mypln.worktype.name = tourPlan[key]['worktype_name'];

                                 if (tourPlan[key]['worktype_code'] == 52) {
                                     $scope.Mypln.worktype.selected.FWFlg = 'F';
                                     if ($scope.SF_type == 2) {
                                         $scope.Mypln.subordinate.selected.id = tourPlan[key]['HQ_Code'];
                                         $scope.cMTPDId = '_' + $scope.Mypln.subordinate.selected.id;
                                         TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
                                         if (TpTwns.length < 1) {
                                             $ionicLoading.show({
                                                 template: 'Please Wait. Data is Sync...'
                                             });
                                             $scope.clearAll(false, $scope.cMTPDId);
                                         } else {
                                             $scope.loadDatas(false, $scope.cMTPDId);
                                         }
                                         $scope.Mypln.subordinate.name = tourPlan[key]['HQ_Name'];
                                         $scope.Mypln.stockist.selected.id = Number(tourPlan[key]['Worked_with_Code']);
                                         $scope.Mypln.stockist.name = tourPlan[key]['Worked_with_Name'];
                                     }
                                     else {
                                         $scope.BrndSumm.stockist.selected.id = Number(tourPlan[key]['Worked_with_Code']);
                                         $scope.BrndSumm.stockist.name = tourPlan[key]['Worked_with_Name'];
                                     }
                                     $scope.Mypln.cluster.selected.id = Number(tourPlan[key]['RouteCode']);
                                     $scope.Mypln.cluster.name = tourPlan[key]['RouteName'];

                                 }
                                 else {
                                     $scope.Mypln.worktype.selected.FWFlg = 'A';
                                     $scope.BrndSumm.stockist.selected.id = '';
                                     $scope.Mypln.cluster.selected.id = '';
                                     $scope.BrndSumm.stockist.name = '';
                                     $scope.Mypln.cluster.name = '';
                                 }
                             }
                         }
                     }

                     updateData();
                     $scope.modal.show();

                 },
                 dateClick: function (date) {
                     $scope.Mypln.subordinate.selected.id = $scope.sfCode;

                     $scope.BrndSumm.stockist.selected.id = '';
                     $scope.Mypln.cluster.selected.id = '';
                     $scope.data.remarks = '';
                     //  $scope.data.market = '';
                     $scope.show = 1;
                     if (date.day < 10) date.day = "0" + date.day;
                     date.month = date.month + 1;
                     if (date.month < 10) date.month = "0" + date.month;
                     $scope.data.date = date.year + "-" + date.month + "-" + date.day;
                     $scope.clickdate = date.day + "/" + date.month + "/" + date.year;
                 },
                 changeMonth: function (month, year) {

                 },
             };
             $scope.Tour_Plan = fmcgLocalStorage.getData("Tour_Plan") || [];
             $scope.events = $scope.Tour_Plan;
         }
         $scope.datefn();

         $scope.submit = function () {
             fmcgAPIservice.addMAData('POST', 'dcr/save&month=' + $scope.month + '&year=' + $scope.year, "19", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("Submitted For Approval");
                     $state.go('fmcgmenu.home');
                 }
             })
         }


         $scope.close = function () {
             $scope.show = 0;
         }

         $scope.save = function () {
             if ($scope.Mypln.worktype.selected == undefined || $scope.Mypln.worktype.selected.id == "") {
                 Toast('Select the Work Type...');
                 return false;
             }
             var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
             $scope.data.sfName = loginInfo.sfName;
             $scope.data.SF_type = $scope.SF_type;
             $scope.data.worktype_code = $scope.Mypln.worktype.selected.id;
             $scope.data.worktype_name = $scope.Mypln.worktype.name;
             if ($scope.data.worktype_code == 52) {

                 if ($scope.SF_type == 2) {
                     if ($scope.Mypln.subordinate.selected == undefined || $scope.Mypln.subordinate.selected.id == "") {
                         Toast('Select the Headquarter...');
                         return false;
                     }
                     if ($scope.Mypln.stockist.selected == undefined || $scope.Mypln.stockist.selected.id == "") {
                         Toast('Select the Distributor...');
                         return false;
                     }
                 }
                 else {
                     if ($scope.BrndSumm.stockist.selected == undefined || $scope.BrndSumm.stockist.selected.id == "") {
                         Toast('Select the Distributor...');
                         return false;
                     }
                 }
                 if ($scope.Mypln.cluster.selected == undefined || $scope.Mypln.cluster.selected.id == "") {
                     Toast('Select the ' + $scope.SDPCap + '...');
                     return false;
                 }

                 if ($scope.SF_type == 2) {
                     $scope.data.Worked_with_Code = $scope.Mypln.stockist.selected.id;
                     $scope.data.Worked_with_Name = $scope.Mypln.stockist.name;
                     $scope.data.HQ_Code = $scope.Mypln.subordinate.selected.id;
                     $scope.data.HQ_Name = $scope.Mypln.subordinate.name;
                 }
                 else {
                     $scope.data.Worked_with_Code = $scope.BrndSumm.stockist.selected.id;
                     $scope.data.Worked_with_Name = $scope.BrndSumm.stockist.name;
                 }
                 $scope.data.RouteCode = $scope.Mypln.cluster.selected.id;
                 $scope.data.RouteName = $scope.Mypln.cluster.name;

             } else {
                 $scope.data.Worked_with_Code = '';
                 $scope.data.Worked_with_Name = '';
                 $scope.data.RouteCode = '';
                 $scope.data.RouteName = '';
             }
             $ionicLoading.show({
                 template: 'Saving...'
             });
             fmcgAPIservice.addMAData('POST', 'dcr/save', "9", $scope.data).success(function (response) {
                 if (response.success) {
                     var tourPlan = {};
                     tourPlan = fmcgLocalStorage.getData("Tour_Plan") || [];
                     for (var key in tourPlan) {
                         if ($scope.data.date == (tourPlan[key]['date'])) {
                             tourPlan.splice(key, 1);
                         }
                     }
                     $scope.data.Confirmed = 0;
                     tourPlan.push($scope.data);
                     $scope.show = 0;
                     $scope.Tour_Plan = fmcgLocalStorage.createData("Tour_Plan", tourPlan);
                     if (tourPlan.length == $scope.noOfMonthsubmit)
                         $scope.submitApproval = 1;
                     $scope.data = {};
                     $scope.datefn();
                     Toast("Plan Submited Successfully");
                     $ionicLoading.hide();
                     $state.go('fmcgmenu.TourPlan');
                 }
             }).error(function () {
                 //window.localStorage.setItem("Tour_Plan", $scope.Tour_Plan);
                 Toast("No Internet Connection! Try Again.");
                 $ionicLoading.hide();
                 //$state.go('fmcgmenu.TourPlan');
             });
         }
     }])

        .controller('addChemistCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
            $scope.$parent.navTitle = "Add " + $scope.ChmCap;
            $scope.clearData();
            $scope.view_chemist = 1;
            $scope.NmCap = $scope.ChmCap;
            $scope.data = {};
            $scope.save = function () {
                $scope.data.cluster = {};
                $scope.data.cluster.selected = {};
                if ($scope.data.name == "" || $scope.data.name == undefined) {
                    Toast('Enter the Name...');
                    return false;
                }
                if ($scope.fmcgData.cluster.selected.id == undefined) {
                    Toast('Select the Cluster...');
                    return false;
                }
                if ($scope.data.address == "" || $scope.data.address == undefined) {
                    Toast('Enter the Address...');
                    return false;
                }
                if ($scope.data.phone == "" || $scope.data.phone == undefined) {
                    Toast('Enter the Phone...');
                    return false;
                }
                $scope.data.cluster.selected.id = $scope.fmcgData.cluster.selected.id;
                fmcgAPIservice.addMAData('POST', 'dcr/save', "1", $scope.data).success(function (response) {
                    if (response.success)
                        Toast($scope.ChmCap + " Added Successfully");
                    //write as service
                    $scope.clearIdividual(1, 1);
                    $scope.data = {};
                    $scope.fmcgData.cluster.selected.id = undefined;
                });
                $state.go('fmcgmenu.home');
            };
        }])
    .controller('dashbrdCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', '$ionicModal', '$ionicScrollDelegate', function ($rootScope, $scope, $state, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal, $ionicScrollDelegate) {
        vwCalls = fmcgLocalStorage.getData("MnthSmry") || [];
        fmcgAPIservice.getDataList('POST', 'get/calls', [])
                               .success(function (response) {
                                   window.localStorage.removeItem("MnthSmry");
                                   fmcgLocalStorage.createData("MnthSmry", response);
                                   explainVar(response);
                               }).error(function () {
                                   Toast('No Internet Connection.');
                               });
        var towns = $scope.myTpTwns;
        var tCode = $scope.Myplns[0]['clusterid'];
        var data = $scope.doctors;
        $scope.FilteredData = data.filter(function (a) {
            return (a.town_code === tCode);
        });
        for (var key in towns) {
            if (towns[key]['id'] == tCode)
                $scope.rootTarget = towns[key]['target'];
            //                        $scope.minProd = towns[key]['min_prod'] + "% -";
        }
        $scope.modal = {};
        $scope.modal.vwCalls = {};
        function explainVar(response) {
            $scope.modal.vwCalls = response.today;
            $scope.modal.monthvwCalls = response.month;
            $scope.modal.vwCalls.totalRetailor = $scope.FilteredData.length;
            $scope.labels = ["UnProductiveCalls", "Productive Calls"];
            $scope.data = [response.today.calls - response.today.order, response.today.order];
            $scope.monthdata = [response.month.calls - response.month.order, response.month.order];
            $scope.modal.monthvwCalls.productivity = parseFloat(($scope.modal.monthvwCalls.order / $scope.modal.vwCalls.totalRetailor) * 100).toFixed(2) + "%";
            $scope.modal.vwCalls.productivity = parseFloat(($scope.modal.vwCalls.order / $scope.modal.vwCalls.totalRetailor) * 100).toFixed(2) + "%";
            $scope.remaining = $scope.rootTarget - $scope.modal.vwCalls.orderVal;
        }
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        $scope.GetDate = function (dt) {
            var today = new Date(dt);
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            return dd + '/' + mm + '/' + yyyy;
        }

        $scope.sendWhatsApp = function () {
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $('#preview').height() + 300);
            $('body').css("overflow", 'visible');
            html2canvas($('#preview'), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    getCanvas = canvas;
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    console.log(canvasImg);
                    window.plugins.socialsharing.shareViaWhatsApp('Summary Dashboard', canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                }
            });
        }
        $scope.sendEmail = function () {
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $('#preview').height() + 300);
            $('body').css("overflow", 'visible');
            html2canvas($('#preview'), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    window.plugins.socialsharing.share('', 'Summary Dashboard', canvasImg, '');
                }
            });
        }
    }])
        .controller('homeCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', '$ionicModal', '$ionicScrollDelegate', function ($rootScope, $scope, $state, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal, $ionicScrollDelegate) {
            $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
            if ($scope.Mypl.length > 0) {
                if ($scope.Mypl[0].worktype != undefined) {
                    $scope.Mypln.worktype = {};
                    $scope.Mypln.worktype.selected = {};
                    $scope.Mypln.worktype.selected.id = $scope.Mypl[0].worktype;
                    $scope.Mypln.worktype.FWFlg = $scope.Mypl[0].FWFlg;
                }
            }
            $scope.customers = [{
                'id': '1',
                'name': $scope.DrCap,
                'url': 'manageDoctorResult'
            }];
            var al = 1;
            if ($scope.ChmNeed != 1) {
                $scope.customers.push({
                    'id': '2',
                    'name': $scope.ChmCap,
                    'url': 'manageChemistResult'
                });
                $scope.cCI = al;
                al++;
            }
            if ($scope.StkNeed != 1) {
                $scope.customers.push({
                    'id': '3',
                    'name': $scope.StkCap,
                    'url': 'manageStockistResult'
                });
                $scope.sCI = al;
                al++;
            }
            if ($scope.UNLNeed != 1) {
                $scope.customers.push({
                    'id': '4',
                    'name': $scope.NLCap,
                    'url': 'manageStockistResult'
                });
                $scope.nCI = al;
                al++;
            }
            $scope.clearData();
            $scope.$parent.navTitle = "";

            var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
            $scope.$parent.fieldforceName = loginInfo.sfName;
            var eles = document.querySelectorAll(".fieldforce-name");
            for (var i = 0; i < eles.length; i++) {
                eles[i].innerHTML = $scope.$parent.fieldforceName;
            }
            if ($scope.Myplns.length == 0 && $scope.PlanChk == 1) {
                $state.go('fmcgmenu.myPlan');
            }
            else if ($scope.Myplns.length > 0) {

                if ($scope.view_MR == 1)
                    $scope.Myplns[0].subordinateid = $scope.sfCode;
                if ($scope.Myplns[0].subordinateid != "") {
                    $scope.cDataID = '_' + $scope.Myplns[0].subordinateid;
                    TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cDataID) || [];
                    if (TpTwns.length < 1) {
                        $ionicLoading.show({
                            template: 'Please Wait. Day Plan Details is Sync...'
                        });
                        $scope.clearAll(false, $scope.cDataID);
                    } else {
                        $scope.loadDatas(false, $scope.cDataID);
                    }
                }
            }
            $scope.viewDashboard = function () {

                $state.go('fmcgmenu.dashbrd');
            }
            $scope.gotoCust = function (cus) {
                
                $scope.$parent.fmcgData.customer = {};
                $scope.$parent.fmcgData.worktype = {};
                $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
               if($scope.Mypl.length > 0)
                flg= $scope.Mypl[0].FWFlg;
            else
                flg="F";
                var CusTyp = (cus == 1) ? "D" : (cus == 2) ? "C" : (cus == 3) ? "S" : (cus == 4) ? "U" : "R";
                var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function (a) {
                    return (a.id == $scope.prvRptId)
                }) : $scope.worktypes.filter(function (a) {
                    return (a.FWFlg ==flg&& a.ETabs.indexOf(CusTyp) > -1)
                });

                if (Wtyps.length > 0) {
                    $scope.$parent.fmcgData.worktype.selected = Wtyps[0]; //{ "id": "field work", "name": "Field Work","FWFlg":"F"}
                    switch (cus) {
                        case 1:
                            $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
                            break;
                        case 2:
                            if ($scope.ChmNeed != 1)
                                $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.cCI];
                            break;
                        case 3:
                            if ($scope.StkNeed != 1)
                                $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.sCI];
                            break;
                        case 4:
                            if ($scope.UNLNeed != 1)
                                $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.nCI];
                            break;
                    }
                    $scope.$parent.fmcgData.source = 0;
                }

                var pln = fmcgLocalStorage.getData("mypln") || [];
                if (pln.length > 0) {
                    if (pln[0].FWFlg == 'F' ||  pln[0].FWFlg == 'HG') {
                        $state.go('fmcgmenu.screen2');
                    }
                    else if (pln[0].FWFlg == 'DD') {
                        $state.go('fmcgmenu.addDoorToDoor');
                    }
                    else if (pln[0].FWFlg == 'DS') {
                        $state.go('fmcgmenu.addSurvey');
                    }
                    else {
                        $state.go('fmcgmenu.addNew');
                    }
                }
            };

            $scope.goToCustomer = function (cus) {
                $scope.fmcgData.editableOrder = 0;

                if (cus == 5)
                    $state.go('fmcgmenu.stockUpdation');
                if (cus == 6)
                    $state.go('fmcgmenu.transCurrentStocks');
                if (cus == 7) {
                    $scope.$parent.fmcgData.customer = {};
                    $scope.$parent.fmcgData.customer.selected = cus;
                    $state.go('fmcgmenu.addNew');
                }
                else {
                    if ($scope.GeoChk == 0) {
                        Toast("Checking for Location Services");
                        if (_currentLocation.Time != undefined) {
                            var dt = new Date();
                            var difference = dt.getTime() - _currentLocation.Time.getTime(); // This will give difference in milliseconds
                            var resultInMinutes = Math.round(difference / 60000);
                            if (resultInMinutes > 5) {
                                _currentLocation = {};
                                // startGPS();
                            }
                            else {
                                $scope.gotoCust(cus);
                            }
                        } else {
                            // startGPS();
                            if (window.watchScreen) {
                                navigator.geolocation.clearWatch(window.watchScreen);
                            }
                            window.watchScreen = navigator.geolocation.watchPosition(function (position) {
                                navigator.geolocation.clearWatch(window.watchScreen);
                                $scope.gotoCust(cus);
                            }, function (error) {

                                alert(error.code + ":" + error.message + ". Please Enable Location Services.");

                            }, { maximumAge: 5000, timeout: 15000, enableHighAccuracy: false });
                        }
                    }
                    else {
                        $scope.gotoCust(cus);
                    }
                }
            }
            if ($scope.Myplns.length > 0) {
                if ($scope.desigCode == 'MR') {
                    var towns = $scope.myTpTwns;


                    var tCode = $scope.Myplns[0]['clusterid'];
                    var data = $scope.doctors;
                    $scope.FilteredData = data.filter(function (a) {
                        return (a.town_code === tCode);
                    });
                    for (var key in towns) {
                        if (towns[key]['id'] == tCode)
                            $scope.rootTarget = towns[key]['target'];
                        //                        $scope.minProd = towns[key]['min_prod'] + "% -";
                    }
                    fmcgAPIservice.getDataList('POST', 'get/calls', [])
                            .success(function (response) {
                                $scope.modal.vwCalls = response.today;
                                $scope.modal.monthvwCalls = response.month;
                                $scope.modal.vwCalls.totalRetailor = $scope.FilteredData.length;
                                $scope.labels = ["UnProductiveCalls", "Productive Calls"];
                                $scope.data = [response.today.calls - response.today.order, response.today.order];
                                $scope.monthdata = [response.month.calls - response.month.order, response.month.order];
                                $scope.modal.monthvwCalls.productivity = parseFloat(($scope.modal.monthvwCalls.order / $scope.modal.vwCalls.totalRetailor) * 100).toFixed(2) + "%";
                                $scope.modal.vwCalls.productivity = parseFloat(($scope.modal.vwCalls.order / $scope.modal.vwCalls.totalRetailor) * 100).toFixed(2) + "%";
                                $scope.remaining = $scope.rootTarget - $scope.modal.vwCalls.orderVal;
                                //                                $scope.data = [$scope.modal.vwCalls.totalRetailor, response.order];
                                //                                $scope.options = {
                                //                                    maintainAspectRatio: true,
                                //                                    responsive: false
                                //                                };
                                $scope.colors = ['#14bcc1', '#f8756e'];

                                //                                $scope.colors = ['#f8f8f8', '#f8756e'];
                            }).error(function () {
                                Toast('No Internet Connection.');
                            });


                }
                $scope.modal = $ionicModal;
                $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function (modal) {
                    $scope.modal = modal;
                }, {
                    animation: 'slide-in-up',
                    focusFirstInput: true
                });
                $scope.ViewDetail = function (Acd, SlTyp, Adt, type) {
                    $scope.modal.type = type;
                    if ($scope.modal.type == 0)
                        $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.DrCap : (SlTyp == 2) ? $scope.ChmCap : (SlTyp == 3) ? $scope.StkCap : (SlTyp == 4) ? $scope.NLCap : '') + ' Visit Details For : ' + Adt;
                    if ($scope.modal.type == 1)
                        $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.DrCap : (SlTyp == 2) ? $scope.ChmCap : (SlTyp == 3) ? $scope.StkCap : (SlTyp == 4) ? $scope.NLCap : '') + ' Order Details For : ' + Adt;
                    $scope.modal.show();

                    $ionicLoading.show({
                        template: 'Loading...'
                    });
                    $scope.modal.vwVstlists = [];
                    $scope.modal.sendWhatsApp = function () {
                        $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                        $('ion-nav-view').css("height", $('#vstpreview').height() + 300);
                        $('body').css("overflow", 'visible');
                        html2canvas($('#vstpreview'), {
                            onrendered: function (canvas) {
                                $scope.modal.canvasVstImg = canvas.toDataURL("image/jpg");
                                getCanvas = canvas;
                                $('ion-nav-view').css("height", "");
                                $('body').css("overflow", 'hidden');

                                window.plugins.socialsharing.shareViaWhatsApp($scope.modal.rptTitle, $scope.modal.canvasVstImg, null, function () { }, function (errormsg) { alert(errormsg) })
                            }
                        });
                    }
                    $scope.modal.sendEmail = function () {
                        $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                        $('ion-nav-view').css("height", $('#vstpreview').height() + 300);
                        $('body').css("overflow", 'visible');
                        html2canvas($('#vstpreview'), {
                            onrendered: function (canvas) {
                                $scope.modal.canvasVstImg = canvas.toDataURL("image/jpg");
                                $('ion-nav-view').css("height", "");
                                $('body').css("overflow", 'hidden');
                                window.plugins.socialsharing.share('', $scope.modal.rptTitle, $scope.modal.canvasVstImg, '');
                            }
                        });
                    }


                    fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                            .success(function (response) {
                                if ($scope.modal.type == 1) {
                                    for (var i = response.length - 1; i >= 0; i--) {
                                        if (response[i]['orderValue'] == 0 || response[i]['orderValue'] == null)
                                            response.splice(i, 1);
                                    }
                                }
                                $scope.modal.vwVstlists = response;

                                $ionicLoading.hide();
                            }).error(function () {
                                $ionicLoading.hide();
                                Toast('No Internet Connection.');
                            });
                    $ionicScrollDelegate.scrollTop();
                }
            }
            ;
        }])
        .controller('manageCtrl', ['$rootScope', '$scope', '$state', '$ionicLoading', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', function ($rootScope, $scope, $state, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.success = false;
            $scope.owsuccess = false;
            $scope.owTypeData = [];
            $scope.$parent.navTitle = "Submitted Calls";
            $scope.customers = [{
                'id': '1',
                'name': $scope.DrCap,
                'url': 'manageDoctorResult'
            }];
         
            var al = 1;
            if ($scope.ChmNeed != 1) {
                $scope.customers.push({
                    'id': '2',
                    'name': $scope.ChmCap,
                    'url': 'manageChemistResult'
                });
                $scope.cCI = al;
                al++;
            }
            if ($scope.StkNeed != 1) {
                $scope.customers.push({
                    'id': '3',
                    'name': $scope.StkCap,
                    'url': 'manageStockistResult'
                });
                $scope.sCI = al;
                al++;
            }
            if ($scope.UNLNeed != 1) {
                $scope.customers.push({
                    'id': '4',
                    'name': $scope.NLCap,
                    'url': 'manageStockistResult'
                });
                $scope.nCI = al;
                al++;
            }
            fmcgAPIservice.getDataList('POST', 'entry/count', []).success(function (response) {
                if (response['success']) {
                    $scope.success = true;
                    $scope.customers[0].count = response['data'][0]['doctor_count'];
                    if ($scope.ChmNeed != 1)
                        $scope.customers[$scope.cCI].count = response['data'][1]['chemist_count'];
                    if ($scope.StkNeed != 1)
                        $scope.customers[$scope.sCI].count = response['data'][2]['stockist_count'];
                    if ($scope.UNLNeed != 1)
                        $scope.customers[$scope.nCI].count = response['data'][3]['uldoctor_count'];
                    $scope.owTypeData.daywise_remarks = response['data'][4]['remarks'];
                    $scope.owTypeData.HlfDayWrk = response['data'][5]['halfdaywrk'];
                    
                    $scope.door = response['data'][7]['door_count'];
                }
                else {
                    $scope.owsuccess = true;
                    $scope.owTypeData = response['data'][0];
                    if ($scope.owTypeData.FWFlg == 'DH')
                        $scope.owTypeData.count = response['count']['hunt_count'];
                }
                $ionicLoading.hide();
            }).error(function () {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        }])
        .controller('screen1Ctrl', function ($rootScope, $scope, $location, $ionicPopup, $stateParams, $state, fmcgAPIservice, fmcgLocalStorage, generateUID, notification, $ionicLoading) {
            $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
            if ($scope.Myplns.length == 0) {
                $state.go('fmcgmenu.myPlan');
            }
            $scope.fmcgData.value1 = 0;

            $scope.$on('checkWorktype', function (e,flgs) {
                if(flgs.flg=="DD")
                    $state.go('fmcgmenu.addDoorToDoor');
               else if (flgs.flg == "DH")
                   $state.go('fmcgmenu.distributorHunt');
               else if (flgs.flg == "DS")
                   $state.go('fmcgmenu.addSurvey');
            });
            if ($scope.Myplns.length > 0) {
                if ($scope.Myplns[0].worktype != undefined) {
                    $scope.fmcgData.worktype = {};
                    $scope.fmcgData.worktype.selected = {};
                    $scope.fmcgData.worktype.selected.id = $scope.Myplns[0].worktype;
                    $scope.fmcgData.worktype.selected.FWFlg = $scope.Myplns[0].FWFlg;
                    $scope.fmcgData.stockist = {};
                    $scope.fmcgData.stockist.selected = {};
                    $scope.fmcgData.stockist.selected.id = $scope.Myplns[0].stockistid;
                }
            }
            else
                $scope.Myplns[0].FWFlg = 'F';
            if ($scope.fmcgData.worktype.selected.FWFlg == 'DD') {
                $state.go('fmcgmenu.addDoorToDoor');
            }
            if ($scope.fmcgData.worktype.selected.FWFlg == 'DS') {
                $state.go('fmcgmenu.addSurvey');
            }
            $scope.$parent.fmcgData.currentLocation = "fmcgmenu.addNew";
            $scope.$parent.navTitle = "Call Report";

            if ($scope.fmcgData.worktype.selected.FWFlg != 'DH') {
                if ($scope.view_MR == 1) {
                    $scope.$parent.fmcgData.subordinate = {};
                    $scope.$parent.fmcgData.subordinate.selected = {};
                    $scope.$parent.fmcgData.subordinate.selected.id = $scope.sfCode;
                    $scope.loadDatas(false, '_' + $scope.sfCode)
                }

                if ($scope.Myplns[0].FWFlg == 'F' || $scope.Myplns[0].FWFlg == 'DD' || $scope.Myplns[0].FWFlg == 'DS' || $scope.Myplns[0].FWFlg == 'HG') {
                    $scope.$parent.fmcgData.worktype = {};
                    $scope.$parent.fmcgData.worktype.selected = {};
                    var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function (a) {
                        return (a.id == $scope.prvRptId)
                    }) : $scope.worktypes.filter(function (a) {
                        return (a.FWFlg == $scope.Myplns[0].FWFlg)
                    });
                    $scope.$parent.fmcgData.worktype.selected = Wtyps[0];//{ "id": "field work", "name": "Field Work","FWFlg":"F"}        
                    var id = $stateParams.customerId;
                    if (id != null && id.length) {
                        var Wtyps = $scope.worktypes.filter(function (a) {
                            return (a.FWFlg == $scope.Myplns[0].FWFlg)
                        });
                        $scope.worktype = {};
                        $scope.worktype.selected = Wtyps[0];
                    }
                }
            }
            $scope.data = {}
            $scope.hunt = function () {
                if ($scope.data.Shop_Name == undefined || $scope.data.Shop_Name == '') {
                    Toast('Enter Shop Name');
                    return false;
                }

                if ($scope.data.Contact_Person == undefined || $scope.data.Contact_Person == '') {
                    Toast('Enter Contact Person Name');
                    return false;
                }
                if ($scope.data.Phone_Number == undefined || $scope.data.Phone_Number == '') {
                    Toast('Enter Phone Number');
                    return false;
                }
                if ($scope.data.area == undefined || $scope.data.area == '') {
                    Toast('Enter Area');
                    return false;
                }
                $ionicLoading.show({
                    template: 'Loading...'
                });
                $scope.Mypln.worktype = {};
                $scope.Mypln.worktype.selected = {};
                $scope.Mypln.worktype.selected.FWFlg = 'DH';
                var Wtyps = $scope.worktypes.filter(function (a) {
                    return (a.FWFlg == 'DH')
                });
                $scope.Mypln.worktype.selected.id = Wtyps[0]['id'];
                    $scope.data.worktype = $scope.Mypln.worktype.selected.id;
                    fmcgAPIservice.addMAData('POST', 'dcr/save', "21", $scope.data).success(function (response) {

                        if (!response['success'] && response['type'] == 2) {
                            $state.go('fmcgmenu.home');
                            Toast(response['msg']);
                            $ionicLoading.hide();
                        }
                        else if (!response['success'] && response['type'] == 1) {
                            alert()
                            $scope.showConfirm = function () {
                                var confirmPopup = $ionicPopup.confirm({
                                    title: 'Warning !',
                                    template: response['msg']
                                });
                                confirmPopup.then(function (res) {
                                    if (res) {
                                        $ionicLoading.show({ template: 'Call Submitting. Please wait...' });
                                        fmcgAPIservice.addMAData('POST', 'dcr/save&replace', "21", $scope.data).success(function (response) {
                                            if (!response['success'])
                                                Toast(response['msg'], true);
                                            else {
                                                Toast("call Updated Successfully");

                                            }
                                            $state.go('fmcgmenu.home');
                                            $ionicLoading.hide();
                                        })

                                    }
                                });
                            };
                            $scope.showConfirm();
                            $ionicLoading.hide();

                        }
                        else {
                            if (!response['success'])
                                Toast(response['msg'], true);
                            else {
                                Toast("Call Submitted Successfully");
                                $scope.Mypln.doctor.selected.id = '';
                                $scope.Mypln.remarks = '';
                                //  $scope.$parent.Myplns = fmcgLocalStorage.getData("mypln") || [];
                                $scope.PlanChk = 0;
                                $scope.$parent.PlanChk = 0;
                                $state.go('fmcgmenu.home');
                                $ionicLoading.hide();
                            }
                        }

                    });

           
            }
            $scope.goBackDH = function () {
                if ($scope.Myplns.length > 0) {
                    if ($scope.Myplns[0].worktype != undefined) {
                        $scope.fmcgData.worktype = {};
                        $scope.fmcgData.worktype.selected = {};
                        $scope.fmcgData.worktype.selected.id = $scope.Myplns[0].worktype;
                        $scope.fmcgData.worktype.selected.FWFlg = $scope.Myplns[0].FWFlg;
                    }
                }
            }
            $scope.goBack = function () {
                if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {
                    $ionicPopup.confirm(
                            {
                                title: 'Confirm Navigation',
                                content: 'You have unsaved record do you want to save it in draft...?'
                            }).then(function (res) {
                                if (res) {
                                    fmcgLocalStorage.addData('draft', $scope.fmcgData);
                                    var value = $scope.$parent.fmcgData;
                                    for (key in value) {
                                        if (key) {
                                            if (key != 'jontWorkSelectedList')
                                                $scope.$parent.fmcgData[key] = undefined;

                                        }
                                    }
                                    $state.go('fmcgmenu.home');

                                } else {
                                    $state.go('fmcgmenu.home');
                                    var value = $scope.$parent.fmcgData;
                                    for (key in value) {
                                        if (key) {
                                            if (key != 'jontWorkSelectedList')
                                                $scope.$parent.fmcgData[key] = undefined;

                                        }
                                    }
                                }
                            }
                    );
                }
                else {
                    $state.go('fmcgmenu.home');
                    var value = $scope.$parent.fmcgData;
                    for (key in value) {
                        if (key) {
                            if (key != 'jontWorkSelectedList')
                                $scope.$parent.fmcgData[key] = undefined;

                        }
                    }
                }

            };
            $scope.goNext = function () {
                if ($scope.$parent.fmcgData.worktype.selected.FWFlg != "F" ||  $scope.$parent.fmcgData.worktype.selected.FWFlg != "HG" || $scope.view_MR == 1) {
                    $scope.fmcgData.subordinate = {}
                    $scope.fmcgData.subordinate.selected = {}
                    $scope.fmcgData.subordinate.selected.id = $scope.cDataID.replace(/_/g, '');
                }
                if ($scope.$parent.fmcgData.worktype.selected.FWFlg == "F" || $scope.$parent.fmcgData.worktype.selected.FWFlg == "HG") {
                    var proceed = true;
                    var msg = "";
                    if (!$scope.fmcgData.subordinate && proceed && $scope.view_MR != 1) {
                        msg = "Please Select Headquarters      ";
                        proceed = false;
                    }
                    if (!$scope.fmcgData.cluster && proceed) {
                        msg = "Please Select Cluster       ";
                        proceed = false;
                    }
                    if (!$scope.fmcgData.customer && proceed) {
                        msg = msg + "Please Select Visit To         "
                        proceed = false;
                    }
                    if (proceed) {
                        $scope.$parent.fmcgData.source = 1;
                        $state.go('fmcgmenu.screen2');
                    }
                    else {
                        Toast(msg, true);
                    }
                }
                else
                    $state.go('fmcgmenu.screen5');
            };
        })
        .controller('screen4Ctrl', function ($rootScope, $scope, $state, $filter, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.setAllow();
            if ($scope.$parent.fmcgData.customer) {
                $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
            } else {
                $scope.$parent.navTitle = "";
            }
            $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen4";
            $scope.$parent.fmcgData.giftSelectedList = $scope.$parent.fmcgData.giftSelectedList || [];
            $scope.addProduct = function (selected) {
                var giftData = {};
                giftData.gift = selected;
                giftData.sample_qty = 1;
                $scope.$parent.fmcgData.giftSelectedList.push(giftData);
            }

            $scope.gridOptions = {
                data: 'fmcgData.giftSelectedList',
                rowHeight: 50,
                enableRowSelection: false,
                rowTemplate: 'rowTemplate.html',
                enableCellSelection: true,
                enableColumnResize: true,
                plugins: [new ngGridFlexibleHeightPlugin()],
                showFooter: true,
                columnDefs: [{ field: 'gift', displayName: 'Input Name', enableCellEdit: false, cellTemplate: 'partials/giftCellTemplate.html' },
                    { field: 'sample_qty', displayName: 'Input Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate1.html", width: 90 },
                    { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
            };
            $scope.removeRow = function () {
                var index = this.row.rowIndex;
                $scope.gridOptions.selectItem(index, false);
                $scope.$parent.fmcgData.giftSelectedList.splice(index, 1);
            };
            $scope.goBack = function () {
                if (($scope.fmcgData.customer.selected.id == 1 && $scope.DPNeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CPNeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SPNeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NPNeed == 0))
                    $state.go('fmcgmenu.screen3');
                else
                    $state.go('fmcgmenu.screen2');
            };
            $scope.goNext = function () {
                $state.go('fmcgmenu.screen5');
            };
            $scope.save = function () {
                $scope.saveToDraftO();
            }
        })
        .controller('AppController', function ($scope, $ionicSideMenuDelegate) {
            $scope.toggleLeft = function () {
                $ionicSideMenuDelegate.toggleLeft();
            };
        })
        .controller('screen3ssCtrl', function ($rootScope, $ionicScrollDelegate, $scope, $filter, $state, $ionicModal, $ionicSideMenuDelegate, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen3ss";
            $ionicScrollDelegate.scrollTop();

            $ionicScrollDelegate.$getByHandle("catScroll").scrollTop();
            $ionicScrollDelegate.$getByHandle("ProdScroll").scrollTop();
            if ($scope.selCate == '') {
                //$scope.selCate = 'Select the Category';
                item = $scope.ProdCategory[0];
                $scope.filterProd(item);

            }

            data = $scope.fmcgData.productSelectedList;


            for (var di = 0; di < data.length; di++) {
                data[di].checked = true;

                data[di].id = data[di].product
                data[di].name = data[di].product_Nm

            }

            $scope.selectedProduct = data
            $scope.prdCloseMenu();
            for (di = 0; di < $scope.ProdByCat.length; di++) {
                itm = data.filter(function (a) {
                    return (a.id == $scope.ProdByCat[di].id);
                });
                $scope.ProdByCat[di].checked = false;
                if (itm.length > 0) {
                    if (itm[0].checked == true) {
                        $scope.ProdByCat[di].checked = true;
                    }
                }
            }
            $scope.filterProd = function (item) {
                //    console.log(item)
                $scope.ProdByCat = {};
                $scope.ShowMsg = true;
                if (item.id == -1) {
                    $scope.ProdByCat = $scope.products.filter(function (a) {
                        return (a.Product_Type_Code == "P");
                    });
                }
                else {
                    $scope.ProdByCat = $scope.products.filter(function (a) {
                        return (a.cateid == item.id);
                    });
                }
                for (di = 0; di < $scope.ProdByCat.length; di++) {
                    itm = $scope.selectedProduct.filter(function (a) {
                        return (a.id == $scope.ProdByCat[di].id);
                    });
                    $scope.ProdByCat[di].checked = false;
                    if (itm.length > 0) {
                        $scope.ProdByCat[di].checked = true;
                    }
                }
                $scope.selCate = item.name;
                if ($scope.ProdByCat.length < 1) {
                    $scope.prodmsg = 'No Products Found';
                } else {
                    $scope.ShowMsg = false;
                }
            }

        })
       .controller('screen3sCtrl', function ($rootScope, $ionicScrollDelegate, $scope, $filter, $state, $ionicModal, $ionicSideMenuDelegate, fmcgAPIservice, fmcgLocalStorage, notification) {
           $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen3";
           $scope.productsimg = [
               { "id": "1", "imagename": "a.jpg", "rate": 120 },
                { "id": "2", "imagename": "aa.jpg", "rate": 520 },
                 { "id": "3", "imagename": "aaa.jpg", "rate": 480 },
                  { "id": "4", "imagename": "aaaa.jpg", "rate": 230 },
                   { "id": "5", "imagename": "aaaaa.jpg", "rate": 3000 },
                    { "id": "6", "imagename": "b.jpg", "rate": 340 },
                { "id": "7", "imagename": "bb.jpg", "rate": 4560 },
                 { "id": "8", "imagename": "bbb.jpg", "rate": 3240 },
                  { "id": "9", "imagename": "bbbb.jpg", "rate": 800 },
                   { "id": "10", "imagename": "bbbbb.jpg", "rate": 420 },

           ];
           //  $scope.ur = "http://www.trial.sanfmcg.com/test_img/a.jpg";
           $scope.selected_images = {};
           $scope.productSelectedLists = [];
           $scope.totalOrderValue = 0;
           $scope.orders = $scope.productSelectedLists.length;
           function selectedList() {
               $scope.productSelectedLists = [];
               for (i = 0; i < $scope.productsimg.length; i++) {
                   if ($scope.productsimg[i].selected == true)
                       $scope.productSelectedLists.push($scope.productsimg[i]);
               }
               $scope.orders = $scope.productSelectedLists.length;

           }
           function calculate() {
               $scope.totalOrderValue = 0;

               for (i = 0; i < $scope.productsimg.length; i++) {
                   if ($scope.productsimg[i].selected == true)
                       $scope.totalOrderValue = $scope.totalOrderValue + $scope.productsimg[i].value;
               }
           }
           $scope.qtyChange = function (selected_images) {
               selected_images.value = selected_images.qty * selected_images.rate;
               calculate();
           }
           $scope.addProducts = function (selected_images) {
               if (selected_images.selected == undefined || selected_images.selected == false) {
                   selected_images.selected = true;
                   selectedList();
               }
               else {

                   selected_images.selected = false;
                   calculate();
                   selectedList();
               }
               selected_images.qty = '';
               selected_images.value = 0;

           }
           // console.log(productImgPath+$scope.productsimg[0].imagename)
           $scope.range = function (max, step) {
               step = step || 1;
               var input = [];
               for (var i = 0; i <= max; i += step) {
                   input.push(i);
               }
               return input;
           }
           $scope.productImgPath = productImgPath;
           $ionicScrollDelegate.$getByHandle("ProdScroll").scrollTop();



       })

        .controller('screen3Ctrl', function ($rootScope, $scope, $filter, $state, $ionicModal, $ionicSideMenuDelegate, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen3";
            $scope.myplan = fmcgLocalStorage.getData("mypln") || [];
            $scope.fmcgData.OrdStk = {};
            $scope.fmcgData.OrdStk.selected = {};
            $scope.fmcgData.OrdStk.selected.id = $scope.myplan[0]['stockistid'];
            $scope.modal = $ionicModal;
            $scope.modal.fromTemplateUrl('partials/viewStockistModal.html', function (modal) {
                $scope.modal = modal;
            }, {
                animation: 'slide-in-up',
                focusFirstInput: true
            });

            $scope.viewStock = function () {
                var stockistCode = $scope.fmcgData.OrdStk.selected.id;
                fmcgAPIservice.getDataList('POST', 'table/list&stockistCode=' + stockistCode, ["viewStock"])
                                              .success(function (response) {
                                                  $scope.modal.viewStock = [];
                                                  products = [];
                                                  for (di = 0; di < $scope.products.length; di++) {
                                                      prod = 0;
                                                      itm = response.filter(function (a) {
                                                          if (a.Product_Code == $scope.products[di].id) {
                                                              products.push({ "name": $scope.products[di].name, "Cl_Qty": a.Cl_Qty, "pieces": a.pieces })
                                                              prod = 1
                                                          }

                                                      });
                                                      if (prod != 1)
                                                          products.push({ "name": $scope.products[di].name, "Cl_Qty": 0, "pieces": 0 })

                                                  }
                                                  $scope.modal.viewStock = products;
                                                  $scope.modal.show();
                                              });

                // else
                //   Toast("Please Select Doctor");
            }
            $scope.fmcgData.value1 = 1;
            if ($scope.$parent.fmcgData.customer) {
                $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
            } else {
                $scope.$parent.navTitle = "";
            }
            $scope.setAllow();

            $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
            $scope.addProduct = function (selected) {
                var productData = {};
                productData.product = selected;
                productData.product_Nm = (selected.toString() != "") ? $filter('getValueforID')(selected, $scope.products).name : "";
                productData.rx_qty = 0;
                productData.sample_qty = 0;
                var len = $scope.$parent.fmcgData.productSelectedList.length;
                var flag = true;
                for (var i = 0; i < len; i++) {
                    if ($scope.$parent.fmcgData.productSelectedList[i]['product'] === selected) {
                        flag = false;
                    }
                }
                if (flag)
                    $scope.$parent.fmcgData.productSelectedList.push(productData);
            }


            scrTyp = $scope.$parent.fmcgData.customer.selected.id
            if (scrTyp == 1 || scrTyp == 4) {
                if ($scope.DrSmpQ == 0)
                    $scope.condValue = false;
                else
                    $scope.condValue = true;

                RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
                SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
                $scope.gridOptions = {
                    data: 'fmcgData.productSelectedList',
                    rowHeight: 50,
                    rowTemplate: 'rowTemplate.html',
                    enableCellSelection: true,
                    enableColumnResize: true,
                    enableRowSelection: false,
                    plugins: [new ngGridFlexibleHeightPlugin()],
                    showFooter: true,
                    columnDefs: [{ field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html' },
                        { field: 'rx_qty', displayName: RxCap, enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60 },
                        { field: 'sample_qty', displayName: SmplCap, cellTemplate: "partials/cellEditTemplate1.html", width: 90, "visible": $scope.condValue },
                        { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
                };

            }
            else {
                QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
                $scope.gridOptions = {
                    data: 'fmcgData.productSelectedList',
                    rowHeight: 50,
                    rowTemplate: 'rowTemplate.html',
                    enableCellSelection: true,
                    enableColumnResize: true,
                    enableRowSelection: false,
                    plugins: [new ngGridFlexibleHeightPlugin()],
                    showFooter: true,
                    columnDefs: [{ field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html' },
                        { field: 'rx_qty', displayName: QCap, enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60 },
                        { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
                };
            }
            $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
            var id = $scope.$parent.fmcgData.cluster.selected.id;
            var towns = $scope.myTpTwns;
            for (var key in towns) {
                if (towns[key]['id'] == id)
                    $scope.fmcgData.rootTarget = towns[key]['target'];
            }
            $scope.findRate = function () {
                var productRate = $scope.Product_State_Rates;
                var product = this.row.entity.product;

                var factor = "1" + Array(+(2 > 0 && 2 + 1)).join("0");
                var distributerPrice = 0;
                var netPrice = this.row.entity.product_netwt;
                for (var key in productRate) {
                    if (productRate[key]['Product_Detail_Code'] == product)
                        distributerPrice = productRate[key]['Retailor_Price'];
                }
                val = (this.row.entity.rx_qty) * distributerPrice;
                val1 = (this.row.entity.rx_qty) * netPrice;
                this.row.entity.sample_qty = Math.round(val * factor) / factor;
                this.row.entity.netweightvalue = Math.round(val1 * factor) / factor;
                var products = $scope.$parent.fmcgData.productSelectedList;
                //console.log(products)
                var values = 0;
                var netweightvaluetotal = 0;
                for (var key in products) {
                    values = +products[key]['sample_qty'] + +values;
                    netweightvaluetotal = +products[key]['netweightvalue'] + +netweightvaluetotal;
                }
                $scope.fmcgData.value = Math.round(values * factor) / factor;
                $scope.fmcgData.netweightvaluetotal = Math.round(netweightvaluetotal * factor) / factor;
            };
            $scope.removeRow = function () {
                var index = this.row.rowIndex;
                $scope.gridOptions.selectItem(index, false);
                $scope.$parent.fmcgData.productSelectedList.splice(index, 1);
                var products = $scope.$parent.fmcgData.productSelectedList;
                var values = 0;
                netweightvaluetotal = 0;
                for (var key in products) {
                    values = +products[key]['sample_qty'] + +values;
                    netweightvaluetotal = +products[key]['netweightvalue'] + +netweightvaluetotal;
                }
                $scope.fmcgData.value = values;
                $scope.fmcgData.netweightvaluetotal = netweightvaluetotal;

            };
            $scope.save = function () {
                $scope.saveToDraftO();
                $state.go('fmcgmenu.home');
            }
            $scope.editOrder = function () {
                var details = {};
                details.POB = $scope.fmcgData.pob;
                details.Value = $scope.fmcgData.value;
                details.Cust_Code = $scope.fmcgData.Cust_Code;
                details.Products = $scope.fmcgData.productSelectedList;
                details.DCR_Code = $scope.fmcgData.DCR_Code;
                details.Trans_Sl_No = $scope.fmcgData.Trans_Sl_No;
                details.Route = $scope.fmcgData.route;
                var id = details.Route;
                var towns = $scope.myTpTwns;
                for (var key in towns) {
                    if (towns[key]['id'] == id)
                        details.target = towns[key]['target'];
                }
                details.Stockist = $scope.fmcgData.Stockist;
                fmcgAPIservice.updateDCRData('POST', 'dcr/updateProducts', details).success(function (response) {
                    if (response.success)
                        Toast("call Updated Successfully");
                    $state.go('fmcgmenu.home');
                }).error(function () {
                    Toast("No Internet Connection!");
                });
            }
            $scope.submit = function () {
                $scope.$emit('finalSubmit');
            }
            $scope.goBack = function () {
                $state.go('fmcgmenu.screen2');
                $scope.fmcgData.value1 = 0;
            };
            $scope.goNext = function () {
                $scope.fmcgData.value1 = 0;

                if ((',1,4,').indexOf(',' + $scope.fmcgData.customer.selected.id + ',') > -1 && $scope.AppTyp == 1) {
                    if ($scope.fmcgData.productSelectedList.length > 0) {
                        if ($scope.fmcgData.OrdStk == undefined || $scope.fmcgData.OrdStk.selected.id == "") {
                            Toast('Select the ' + $scope.StkCap + ' Name');
                            return false;
                        }
                    }
                }
                if (($scope.fmcgData.customer.selected.id == 1 && $scope.DINeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CINeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SINeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NINeed == 0))
                    $state.go('fmcgmenu.screen4');
                else
                    $state.go('fmcgmenu.screen5');
            };
        })
        .controller('screen2Ctrl', function ($rootScope, $scope, $state, $timeout, $filter, $ionicModal, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.fmcgData.value1 = 0;
            if ($scope.Myplns.length == 0) {
                $state.go('fmcgmenu.myPlan');
            }
            var fl = window.localStorage.getItem("addRetailorflg");
            if (fl == 1) {
                $scope.fmcgData.customer = {};
                $scope.fmcgData.customer.selected = {};
                $scope.fmcgData.customer.selected.id= "1";
            }
         
                $state.go('fmcgmenu.screen2');
            $scope.addRetailor = function (sd) {
                localStorage.removeItem("addRetailorflg");
                fmcgLocalStorage.createData("addRetailorflg", 1);
                $state.go('fmcgmenu.addULDoctor');
            }
            if (!($scope.$parent.fmcgData.cluster)) {
                if ($scope.Myplns[0].subordinateid == "" && $scope.view_MR != 1) {
                    $state.go('fmcgmenu.addNew');
                } else {
                    if ($scope.view_MR == 1)
                        $scope.cDataID = '_' + $scope.sfCode;
                    else
                        $scope.cDataID = '_' + $scope.Myplns[0].subordinateid; //alert($scope.cDataID);
                    $scope.loadDatas(false, $scope.cDataID);

                    $scope.$parent.fmcgData.subordinate = {};
                    $scope.$parent.fmcgData.subordinate.selected = {};
                    $scope.$parent.fmcgData.subordinate.selected.id = $scope.cDataID.replace(/_/g, '');

                    $scope.$parent.fmcgData.cluster = {};
                    $scope.$parent.fmcgData.cluster.selected = {};
                    $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;

                }
            }
            $scope.modal = $ionicModal;
            var tDate = new Date();
            if (!$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.isDraft) {
                $scope.$parent.fmcgData.entryDate = tDate;
                $scope.$parent.fmcgData.modifiedDate = tDate;
            }
            else {
                $scope.$parent.fmcgData.modifiedDate = tDate;
                var tDate = new Date($scope.$parent.fmcgData.entryDate);
                $scope.$parent.fmcgData.entryDate = tDate;

            }

            if ($scope.$parent.fmcgData.customer) {
                $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
            } else {
                $scope.$parent.navTitle = "";
            }
            $scope.setAllow();
            $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen2";
            $scope.customers = [{
                'id': '1',
                'name': $scope.DrCap
            }];
            var al = 1;
            if ($scope.ChmNeed != 1) {
                $scope.customers.push({
                    'id': '2',
                    'name': $scope.ChmCap
                });
                $scope.cCI = al;
                al++;
            }
            if ($scope.StkNeed != 1) {
                $scope.customers.push({
                    'id': '3',
                    'name': $scope.StkCap
                });
                $scope.sCI = al;
                al++;
            }
            if ($scope.UNLNeed != 1) {
                $scope.customers.push({
                    'id': '4',
                    'name': $scope.NLCap
                });
                $scope.nCI = al;
                al++;
            }

            $scope.submit = function () {
                var msg = "";
                var proceed = true;
                if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {
                }
                else {
                    proceed = false;
                    msg = "Please Select " + $filter('getValueforID')($scope.fmcgData.customer.selected.id, $scope.customers).name;
                }
                if ($scope.fmcgData.worktype.selected.id == "field work") {
                    if ($scope.reportTemplates.length > 0) {
                        if ($scope.fmcgData.rx_t == undefined && $scope.fmcgData.customer.selected.id == "1") {
                            Toast('Select Call Feedback....');
                            return false;
                        }
                    }
                    if ($scope.nonreportTemplates.length > 0) {
                        if (($scope.fmcgData.remarks == undefined || $scope.fmcgData.remarks == '') && $scope.fmcgData.customer.selected.id == "1") {
                            Toast('Select the Template ( or ) Enter the Remarks....');
                            return false;
                        }
                    }
                }
                var pobT = $scope.fmcgData.pob ? parseInt($scope.fmcgData.pob) : 0;
                if (isNaN(pobT) || pobT > 999999) {
                    $scope.fmcgData.pob = "";
                    proceed = false;
                    msg = msg + "Please Check the input " + $scope.POBCap;
                }
                if (proceed) {
                    $scope.$emit('finalSubmit');
                }
                else {
                    Toast(msg, true);
                }

            }
            $scope.save = function () {
                if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {
                    $scope.saveToDraftO();
                }
                else {
                    Toast("No data to save in draft", true);
                }
            };
            if ($scope.fmcgData.doctor == undefined)
                $scope.$parent.fmcgData.jontWorkSelectedList = $scope.Myplns[0].jontWorkSelectedList || [];

            else
                $scope.$parent.fmcgData.jontWorkSelectedList = $scope.$parent.fmcgData['jontWorkSelectedList'] || [];
            $scope.addProduct = function (selected) {
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
                columnDefs: [{ field: 'jointworkname', displayName: 'Jointwork', enableCellEdit: false, cellTemplate: 'partials/jointworkCellTemplate.html' },
                    { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
            };
            $scope.removeRow = function () {
                var index = this.row.rowIndex;
                $scope.gridOptions.selectItem(index, false);
                $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
            };
            $scope.goNext = function () {
                var temp;
                var msg = "";
                var proceed = true;
                if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {
                }
                else {
                    proceed = false;
                    msg = "Please Select " + $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
                }
                var pobT = $scope.fmcgData.pob ? parseInt($scope.fmcgData.pob) : 0;
                if (isNaN(pobT) || pobT > 999999) {
                    $scope.fmcgData.pob = "";
                    proceed = false;
                    msg = msg + "Please Check the input pob";
                }
                if (proceed) {
                    if (($scope.fmcgData.customer.selected.id == 1 && $scope.DPNeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CPNeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SPNeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NPNeed == 0))
                        $state.go('fmcgmenu.screen3');
                    else if (($scope.fmcgData.customer.selected.id == 1 && $scope.DINeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CINeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SINeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NINeed == 0))
                        $state.go('fmcgmenu.screen4')
                    else
                        $state.go('fmcgmenu.screen5');
                }
                else {
                    Toast(msg, true);
                }
            };
            $scope.goBack = function () {
                $state.go('fmcgmenu.addNew');
            };

        }).controller('screen4sCtrl', function ($scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {

        }).controller('screen5Ctrl', function ($rootScope, $scope, $state, $ionicPopup, $filter, geolocation, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
            if ($scope.$parent.fmcgData.customer) {
                $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
            } else {
                $scope.$parent.navTitle = "";
            }
            $scope.setAllow();
            $scope.updateValue = function (value) {
                $scope.$parent.fmcgData.remarks = value.name;
            };
            $scope.saveD = function () {

                $scope.fmcgData.toBeSync = false;
                $scope.saveToDraftO();
            };
            $scope.save = function () {
                $scope.$emit('finalSubmit');

            };
            $scope.goBack = function () {
                if ($scope.fmcgData.worktype.selected.FWFlg.toString() !== "F") {
                    $state.go('fmcgmenu.addNew')
                }
                else if (($scope.fmcgData.customer.selected.id == 1 && $scope.DINeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CINeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SINeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NINeed == 0))
                    $state.go('fmcgmenu.screen4')
                else if (($scope.fmcgData.customer.selected.id == 1 && $scope.DPNeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CPNeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SPNeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NPNeed == 0))
                    $state.go('fmcgmenu.screen3');
                else
                    $state.go('fmcgmenu.screen2');
            };
        })
        .controller('RCPACtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage) {
            $scope.fmcgData.value1 = 0;
            $scope.$parent.navTitle = "RCPA Entry";
            $scope.gridOptions = {
                data: 'RCPA.productSelectedList',
                rowHeight: 50,
                rowTemplate: 'rowTemplate.html',
                enableCellSelection: true,
                enableColumnResize: true,
                enableRowSelection: false,
                plugins: [new ngGridFlexibleHeightPlugin()],
                showFooter: true,
                columnDefs: [{ field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html' },
                    { field: 'qty', displayName: 'Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60 },
                    { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
            };

            $scope.removeRow = function () {
                var index = this.row.rowIndex;
                $scope.gridOptions.selectItem(index, false);
                $scope.$parent.RCPA.productSelectedList.splice(index, 1);
            };
            $scope.clear = function () {
                if ($scope.view_MR == 1) {
                    $scope.RCPA.subordinate = {};
                    $scope.RCPA.subordinate.selected = {};
                    $scope.RCPA.subordinate.selected.id = $scope.sfCode;
                    $scope.loadDatas(false, '_' + $scope.sfCode)
                } else {
                    $scope.RCPA.subordinate = undefined;
                }
                $scope.RCPA.cluster = undefined;
                $scope.RCPA.chemist = undefined;
                $scope.RCPA.doctor = undefined;
                $scope.RCPA.OurBrndCds = "";
                $scope.RCPA.OurBrndNms = "";
                $scope.RCPA.productSelectedList = [];

                $scope.RCPA.CpmptName = "";
                $scope.RCPA.CpmptBrnd = "";
                $scope.RCPA.CpmptQty = "";
                $scope.RCPA.CpmptPOB = "";
                $scope.RCPA.CpmptRate = "";
                $scope.RCPA.Precrip = "";
            }
            $scope.clear();
            $scope.save = function () {
                $scope.data = {};
                if ($scope.view_MR != 1) {
                    if ($scope.RCPA.subordinate == undefined || $scope.RCPA.subordinate.selected.id == "") {
                        Toast('Select the Headquarters');
                        return false;
                    }
                }

                if ($scope.AppTyp != 1) {
                    if ($scope.RCPA.chemist == undefined || $scope.RCPA.chemist.selected.id == "") {
                        Toast('Select the ' + $scope.ChmCap + ' Name');
                        return false;
                    }
                    else
                        $scope.RCPA.chemist.selected.id = ' ';
                }
                if ($scope.RCPA.doctor == undefined || $scope.RCPA.doctor.selected.id == "") {
                    Toast('Select the ' + $scope.DrCap + ' Name');
                    return false;
                }
                tmPrd = $scope.RCPA.productSelectedList;
                if (tmPrd.length < 1) {
                    Toast('Select the Our Brand');
                    return false;
                }
                RCPAProdQty = "";
                RCPAProdNmQty = "";
                for (i = 0; i < tmPrd.length; i++) {
                    RCPAProdQty += tmPrd[i].product + " ( " + tmPrd[i].qty + " ), ";
                    RCPAProdNmQty += tmPrd[i].product_Nm + " ( " + tmPrd[i].qty + " ), ";
                }
                $scope.RCPA.OurBrndCds = RCPAProdQty;
                $scope.RCPA.OurBrndNms = RCPAProdNmQty;

                if ($scope.RCPA.CpmptName == undefined || $scope.RCPA.CpmptName == "") {
                    Toast('Enter the Competitor Name');
                    return false;
                }
                if ($scope.RCPA.CpmptBrnd == undefined || $scope.RCPA.CpmptBrnd == "") {
                    Toast('Enter the Competitor Brand');
                    return false;
                }

                $scope.data.RCPADt = new Date();
                if ($scope.AppTyp != 1) {
                    $scope.data.ChmId = $scope.RCPA.chemist.selected.id;
                    $scope.data.ChmName = $scope.RCPA.chemist.name;
                } else {
                    $scope.data.ChmId = '';
                    $scope.data.ChmName = '';
                }
                $scope.data.DrId = $scope.RCPA.doctor.selected.id;
                $scope.data.DrName = $scope.RCPA.doctor.name;
                $scope.data.ourBrnd = $scope.RCPA.OurBrndCds;
                $scope.data.ourBrndNm = $scope.RCPA.OurBrndNms;

                $scope.data.CmptrName = $scope.RCPA.CpmptName;
                $scope.data.CmptrBrnd = $scope.RCPA.CpmptBrnd;
                $scope.data.CmptrQty = $scope.RCPA.CpmptQty;
                $scope.data.CmptrPOB = $scope.RCPA.CpmptPOB;
                $scope.data.CmptrPriz = $scope.RCPA.CpmptRate;
                $scope.data.Remark = $scope.RCPA.Precrip;
                $scope.clear();

                fmcgAPIservice.addMAData('POST', 'dcr/save', "4", $scope.data).success(function (response) {
                    if (response.success)
                        Toast("RCPA Entry Submited Successfully");
                }).error(function () {
                    $scope.OutRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
                    $scope.OutRCPA.push($scope.data);
                    localStorage.removeItem("OutBx_RCPA");
                    fmcgLocalStorage.createData("OutBx_RCPA", $scope.OutRCPA);
                    Toast("No Internet Connection! RCPA Entry Saved in Outbox");
                });
                $state.go('fmcgmenu.home');
            };
        })
     .controller('LostProducts', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
         $scope.$parent.navTitle = "Lost Products";

         $scope.Mypln.subordinate = {};
         $scope.Mypln.subordinate.selected = {};
         if ($scope.view_MR == 1) {
             $scope.Mypln.subordinate.selected.id = $scope.sfCode;
         }
         $scope.Mypln.cluster = {};
         $scope.Mypln.cluster.selected = {};

         $scope.$on('getLostProducts', function (evnt, DrCd) {
             custCode = DrCd.DrId;
             $ionicLoading.show({
                 template: 'Loading...'
             });
             fmcgAPIservice.getDataList('POST', 'spLostProducts&custCode=' + custCode + "&stockistCode=" + $scope.Mypln.stockist.selected.id + "&code=" + $scope.Mypln.subordinate.selected.id, [])
                  .success(function (response) {
                      ss = [];

                      for (di = 0; di < $scope.products.length; di++) {
                          response.filter(function (a) {
                              if (a.Product_Detail_Name == $scope.products[di].name) {
                                  ss.push(a);
                              }
                          });

                      }
                      $scope.LostProducts = ss;
                      $ionicLoading.hide();
                  }).error(function () {
                      $ionicLoading.hide();
                      Toast('No Internet Connection.');
                  });
         });
     })
       .controller('ApprovalsCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
           $scope.$parent.navTitle = "Approvals";
           $scope.goToApproval = function (cus) {
               if (cus == 1)
                   $state.go('fmcgmenu.ViewLeave');
               if (cus == 2)
                   $state.go('fmcgmenu.viewTPApproval');
               if (cus == 3)
                   $state.go('fmcgmenu.ViewDCR');
           }
       })
       .controller('viewTPApproval', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
           $scope.$parent.navTitle = "View TourPlan";
           $ionicLoading.show({
               template: 'Loading...'
           });
           fmcgAPIservice.getDataList('POST', 'vwChkTransApproval', [])
                .success(function (response) {
                    $scope.TP = response;
                    $ionicLoading.hide();
                }).error(function () {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
           $scope.approval = function (code, month, year) {
               $scope.$parent.TPCode = code;
               $scope.$parent.TPMonth = month;
               $scope.$parent.TPYear = year;
               $state.go('fmcgmenu.TPApproval');
           }
           $scope.goBack = function () {
               $state.go('fmcgmenu.Approvals');
           }
       })
      .controller('TPApproval', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
          $scope.data = {};
          $ionicLoading.show({
              template: 'Loading...'
          });

          $scope.rejection = 0;
          $scope.$parent.navTitle = "TP Approval";
          fmcgAPIservice.getDataList('POST', 'vwChkTransApprovalOne&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, [])
              .success(function (response) {
                  $scope.TPSf = response;
                  $ionicLoading.hide();
              }).error(function () {
                  $ionicLoading.hide();
                  Toast('No Internet Connection.');
              });
          $scope.approve = function () {
              fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, "15", $scope.data).success(function (response) {
                  if (response.success) {
                      Toast("TP Approval Submitted Successfully");
                      $state.go('fmcgmenu.viewTPApproval');
                  }
              });
          }
          $scope.reject = function () {
              $scope.rejection = 1;
          }
          $scope.goBack = function () {
              $state.go('fmcgmenu.viewTPApproval');
          }
          $scope.rejSend = function () {
              if ($scope.data.reason == undefined || $scope.data.reason == '') {
                  Toast('Enter the Reason...');
                  return false;
              }
              fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, "16", $scope.data).success(function (response) {
                  if (response.success) {
                      Toast("TP Reject Successfully");
                      $state.go('fmcgmenu.viewTPApproval');
                  }
              });
          }

      })
     .controller('ViewLeave', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
         $scope.$parent.navTitle = "View Leave";
         $ionicLoading.show({
             template: 'Loading...'
         });
         fmcgAPIservice.getDataList('POST', 'vwLeave', [])
              .success(function (response) {
                  $scope.leaves = response;
                  $ionicLoading.hide();
              }).error(function () {
                  $ionicLoading.hide();
                  Toast('No Internet Connection.');
              });
         $scope.approval = function (id) {
             leaves = $scope.leaves;
             for (key in leaves) {
                 if (leaves[key]['Leave_Id'] == id)
                     $scope.$parent.LA = leaves[key];
             }

             $state.go('fmcgmenu.LeaveApproval');
         }
         $scope.goBack = function () {
             $state.go('fmcgmenu.Approvals');
         }
     })
    .controller('ViewDCR', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "View DCR";
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'vwDcr', [])
             .success(function (response) {
                 $scope.DCR = response;
                 $ionicLoading.hide();
             }).error(function () {
                 $ionicLoading.hide();
                 Toast('No Internet Connection.');
             });
        $scope.approval = function (sfCode, ActivityDate, FieldWork_Indicator, worktype, sfName, Trans_SlNo, PlanName) {
            $scope.$parent.code = sfCode;
            $scope.$parent.activity_Date = ActivityDate;
            $scope.$parent.FieldWork_Indicator = FieldWork_Indicator;
            $scope.$parent.worktype = worktype;
            $scope.$parent.sfName = sfName;
            $scope.$parent.Trans_SlNo = Trans_SlNo;
            $scope.$parent.PlanName = PlanName;
            $state.go('fmcgmenu.DCRApproval');
        }
        $scope.goBack = function () {
            $state.go('fmcgmenu.Approvals');
        }
    })
     .controller('DCRApproval', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
         $scope.data = {};
         $scope.rejection = 0;
         $scope.$parent.navTitle = "DCR Approval";

         if ($scope.$parent.FieldWork_Indicator == 'F') {
             $ionicLoading.show({
                 template: 'Loading...'
             });
             fmcgAPIservice.getDataList('POST', 'vwDcrOne&Trans_SlNo=' + $scope.$parent.Trans_SlNo, [])
                  .success(function (response) {
                      $scope.DCRSf = response;
                      $ionicLoading.hide();
                  }).error(function () {
                      $ionicLoading.hide();
                      Toast('No Internet Connection.');
                  });
         }
         $scope.approve = function () {
             fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.code + "&date=" + $scope.$parent.activity_Date, "17", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("DCR Approval Submitted Successfully");
                     $state.go('fmcgmenu.ViewDCR');
                 }
             });
         }
         $scope.goBack = function () {
             $state.go('fmcgmenu.ViewDCR');
         }
         $scope.reject = function () {
             $scope.rejection = 1;
         }
         $scope.rejSend = function () {
             if ($scope.data.reason == undefined || $scope.data.reason == '') {
                 Toast('Enter the Reason...');
                 return false;
             }
             fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.code + "&date=" + $scope.$parent.activity_Date, "18", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("DCR Reject Successfully");
                     $state.go('fmcgmenu.ViewDCR');
                 }
             });
         }

     })
     .controller('LeaveApproval', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
         $scope.data = {};

         $scope.rejection = 0;
         $scope.$parent.navTitle = "Leave Approval";
         $scope.approve = function () {
             $scope.data.LA = $scope.$parent.LA;
             fmcgAPIservice.addMAData('POST', 'dcr/save&leaveid=' + $scope.$parent.LA.Leave_Id, "13", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("Leave Approval Submitted Successfully");
                     $state.go('fmcgmenu.ViewLeave');
                 }
             });
         }
         $scope.reject = function () {
             $scope.rejection = 1;
         }
         $scope.rejSend = function () {
             $scope.data.LA = $scope.$parent.LA;
             if ($scope.data.reason == undefined || $scope.data.reason == '') {
                 Toast('Enter the Reason...');
                 return false;
             }
             fmcgAPIservice.addMAData('POST', 'dcr/save&leaveid=' + $scope.$parent.LA.Leave_Id, "14", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("Leave Reject Successfully");
                     $state.go('fmcgmenu.ViewLeave');
                 }
             });
         }
         $scope.goBack = function () {
             $state.go('fmcgmenu.ViewLeave');
         }
     })
         .controller('sendMailCtrl', function ($rootScope, $scope, $ionicScrollDelegate, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal) {
             function failNative(e) {
                 console.error('Houston, we have a big problem :(');
             }
             $scope.Attachment = function () {
                 fileChooser.open(function (uri) {
                     var uripath = uri;
                     $ionicLoading.show({
                         template: 'Loading...'
                     });
                     window.FilePath.resolveNativePath(uripath, successNative, failNative);
                 });
             }
             function successNative(finalPath) {
                 // var path = 'file://' + finalPath;
                 $scope.finalPath = finalPath;
                 $scope.NameOfFile = finalPath.substr(finalPath.lastIndexOf('/') + 1);
                 $ionicLoading.hide();

             }
             $scope.fileName = '';
             $scope.finalPath = '';
             function uploadFile() {
                 var imgUrl = $scope.finalPath;
                 var options = new FileUploadOptions();
                 options.fileKey = "imgfile";
                 options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                 options.chunkedMode = false;
                 options.mimeType = "image/jpeg";
                 var uplUrl = baseURL + 'fileAttachment&sf_code=' + $scope.sfCode;
                 var ft = new FileTransfer();
                 ft.upload(imgUrl, uplUrl,
                    function (response) {
                        $scope.finalPath = '';
                    },
                    function () {
                    }, options);
             }
             // console.log($scope.myFile);
             data = {};


             $scope.goBack = function () {
                 $state.go('fmcgmenu.mail');
             }
             $scope.sendMail = function () {
                 $scope.data = {};
                 if ($scope.finalPath != "") {
                     uploadFile();
                     var imgUrl = $scope.finalPath;
                     $scope.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                 }
                 var fd = new FormData();
                 fd.append('fileName', $scope.fileName);
                 fd.append('toId', addQuotes($scope.fmcgData.staffIds));
                 fd.append('ccId', addQuotes($scope.fmcgData.ccstaffIds));
                 fd.append('bccId', addQuotes($scope.fmcgData.bccstaffIds));
                 fd.append('to', $scope.fmcgData.staffNms);
                 fd.append('cc', addQuotes($scope.fmcgData.ccstaffNms));
                 fd.append('bcc', addQuotes($scope.fmcgData.bccstaffNms));
                 msg = $("[name=subject]").html();
                 msg1 = msg.replace(/</g, '&lt;');
                 msg2 = msg1.replace(/>/g, '&gt;');
                 fd.append('subject', encodeURIComponent(msg2));
                 msg = $("[name=content]").html();
                 if ($scope.fmcgData.ccstaffIds == undefined)
                     $scope.fmcgData.ccstaffIds = "";
                 if ($scope.fmcgData.bccstaffIds == undefined)
                     $scope.fmcgData.bccstaffIds = "";
                 ToCcBcc = $scope.fmcgData.staffIds + $scope.fmcgData.ccstaffIds + $scope.fmcgData.bccstaffIds;
                 msg1 = msg.replace(/</g, '&lt;');
                 msg2 = msg1.replace(/>/g, '&gt;');
                 fd.append('message', encodeURIComponent(msg2));
                 fd.append('ToCcBcc', ToCcBcc);
                 var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
                 fd.append('from', loginInfo.sfName);
                 if ($scope.fmcgData.staffNms == '' || $scope.fmcgData.staffNms == undefined) {
                     Toast("Select To");
                     return false;
                 }
                 $ionicLoading.show({
                     template: 'Loading...'
                 });
                 fmcgAPIservice.addMAData('POST', 'dcr/save', "20", fd).success(function (response) {
                     if (response.success) {
                         $state.go('fmcgmenu.mail');
                         Toast("Mail Successfully Submitted");
                         $ionicLoading.hide();

                     }
                 });
             }
         })


    .directive('contenteditable', function () {
        return {
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                if (!ngModel) return;
                ngModel.$render = function () {
                    element.html(ngModel.$viewValue || '');
                };
                element.on('blur keyup change', function () {
                    scope.$apply(read);
                });
                read();
                function read() {
                    var html = element.html();
                    if (attrs.stripBr && html == '<br>') {
                        html = '';
                    }
                    ngModel.$setViewValue(html);
                }
            }
        };
    })

      .controller('mailCtrl', function ($rootScope, $scope, $ionicScrollDelegate, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal) {
          $scope.$parent.navTitle = "Mail";
          $scope.folder = {};
          var date1 = new Date();
          //    date1 = new Date("2016-12-16")
          $scope.month = date1.getMonth() + 1;
          $scope.year = date1.getFullYear();
          years = [];
          for (i = 2014; i <= $scope.year; i++) {
              years.push({ "val": i });
          }
          $scope.years = years;

          $scope.folder.selected = {};
          $scope.folder.selected.id = "0";

          $scope.compose = function () {
              $scope.fmcgData.staffSelectedList = []; $scope.fmcgData.staffNms = ''; $scope.fmcgData.staffIds = "";
              $scope.fmcgData.staffSelectedListcc = []; $scope.fmcgData.ccstaffNms = ''; $scope.fmcgData.ccstaffIds = "";
              $scope.fmcgData.staffSelectedListbcc = []; $scope.fmcgData.bccstaffNms = ''; $scope.fmcgData.bccstaffIds = "";

              $scope.fmcgData.msg = '';
              $scope.fmcgData.Attach = '';
              $scope.fmcgData.subject = '';
              $scope.fmcgData.AttachName = '';
              $state.go('fmcgmenu.sendMail');
          }
          $scope.mailType = 'viewFolders';
          if ($scope.folder.selected.id != undefined)
              viewDetails($scope.folder.selected.id, $scope.month, $scope.year);
          $scope.viewMail = function (Staffid_Id, Mail_vc_ViewFlag, Mail_int_Det_No, date, Attach, from, to, cc, sub, msg) {
              if (Mail_vc_ViewFlag == "0" && $scope.folder.selected.id != "Sent") {
                  fmcgAPIservice.addMAData('POST', 'mailView&id=' + $scope.fmcgData.Mail_int_Det_No).success(function (response) {
                      if (response.success) {
                          //$scope.mailType = "viewFolders";
                      }
                  });
              }
              $scope.fmcgData.msg = "";
              $ionicScrollDelegate.scrollTop();
              $scope.mailType = 'View';
              msg1 = msg.replace(/&lt;/g, '<');
              msg2 = msg1.replace(/&gt;/g, '>');
              $scope.fmcgData.Mail_int_Det_No = Mail_int_Det_No;
              $scope.fmcgData.msg = msg2;
              $scope.fmcgData.date = date;
              $scope.fmcgData.Attach = imageUrl + "MasterFiles/Mails/Attachment/" + Attach;
              $scope.fmcgData.AttachName = Attach;
              $scope.fmcgData.Staffid_Id = Staffid_Id;
              if ($scope.folder.selected.id == "Sent") {
                  $scope.fmcgData.to = from;
                  $scope.fmcgData.from = to;
              }
              else {
                  $scope.fmcgData.from = from;
                  $scope.fmcgData.to = to;
              }
              $scope.fmcgData.cc = cc;
              $scope.fmcgData.sub = sub;
          }
          $scope.view = function () {
              $ionicLoading.show({
                  template: 'Loading...'
              });

              var fileTransfer = new FileTransfer();
              var uri = encodeURI($scope.fmcgData.Attach);
              var fileURL = cordova.file.externalCacheDirectory;
              fileTransfer.download(
                  uri,
                   fileURL + $scope.fmcgData.AttachName,
                  function (entry) {
                      $ionicLoading.hide();
                      filePath = fileURL + $scope.fmcgData.AttachName;
                      fileName = $scope.fmcgData.AttachName;
                      var extn = fileName.split(".").pop();
                      if (extn == "png" || extn == "jpg")
                          mimetype = 'image/png';
                      else
                          mimetype = 'application/' + extn;
                      alert(mimetype)
                      cordova.plugins.fileOpener2.open(

                     filePath,
                    mimetype,
                    {
                        error: function () {
                            //alert('Error status: ' + e.status + ' - Error message: ' + e.message);
                        },
                        success: function () {
                            // alert("Success");
                        }
                    }
                    );
                  },
                  function (error) {
                  },
                  false,
                  {
                      headers: {
                          "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                      }
                  }
              );
          }
          $scope.downloadFile = function () {
              $ionicLoading.show({
                  template: 'Loading...'
              });
              var fileTransfer = new FileTransfer();
              var uri = encodeURI($scope.fmcgData.Attach);
              var fileURL = cordova.file.externalRootDirectory + "/Download/";
              fileTransfer.download(
                  uri,
                   fileURL + $scope.fmcgData.AttachName,

                  function (entry) {
                      $ionicLoading.hide();
                      alert("Download Completed");
                      //  alert("download complete: " + entry.toURL());
                  },
                  function (error) {
                      $ionicLoading.hide();
                      // alert("download error source " + error.source);
                      // alert("download error target " + error.target);
                      // alert("download error code" + error.code);
                  },
                  false,
                  {
                      headers: {
                          "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                      }
                  }
              );
          }
          $scope.$on('moveFolders', function (evnt, det) {
              $scope.subfolder.selected.id = det.Details['id'];
              fmcgAPIservice.addMAData('POST', 'mailMove&id=' + $scope.fmcgData.Mail_int_Det_No + "&folder=" + $scope.subfolder.selected.id).success(function (response) {
                  if (response.success) {
                      $scope.mailType = "viewFolders";
                      Toast("Mail Moved");
                  }
              });
          });
          $scope.showSelectMonth = function (val) {
              $scope.month = val;
              viewDetails($scope.folder.selected.id, $scope.month, $scope.year);

          }
          $scope.showSelectYear = function (val) {
              $scope.year = val;
              viewDetails($scope.folder.selected.id, $scope.month, $scope.year);

          }
          $scope.$on('getFolders', function (evnt, det) {
              $scope.folder.selected.id = det.Details['id'];

              viewDetails(det.Details['id'], $scope.month, $scope.year);
          });

          $scope.reply = function () {
              $scope.fmcgData.staffIds = $scope.fmcgData.Staffid_Id;
              $scope.fmcgData.staffNms = $scope.fmcgData.from;
              $scope.$parent.mailsStaff.push({ "id": $scope.fmcgData.Staffid_Id, "name": $scope.fmcgData.from });
              $scope.fmcgData.staffSelectedList = [];
              $scope.$parent.fmcgData.staffSelectedList.push({ "id": $scope.fmcgData.Staffid_Id, "name": $scope.fmcgData.from, "checked": true });
              $scope.fmcgData.subject = 're:' + $scope.fmcgData.sub;
              $scope.fmcgData.message = '';
              $state.go('fmcgmenu.sendMail');
          }
          $scope.delete = function () {
              fmcgAPIservice.addMAData('POST', 'mailDel&id=' + $scope.fmcgData.Mail_int_Det_No + "&folder=" + $scope.folder.selected.id).success(function (response) {
                  if (response.success) {
                      $scope.mailType = "viewFolders";

                  }
              });
          }
          $scope.forward = function () {
              str = "<div>----- Forwarded Message ----</div><div>From : " + $scope.fmcgData.from + "</div><div>To :" + $scope.fmcgData.to + "</div><div>Sent : " + $scope.fmcgData.date + "</div><div><br></div><div><br></div>";
              $scope.fmcgData.message = str + $scope.fmcgData.msg;
              $scope.fmcgData.subject = "Fwd:" + $scope.fmcgData.sub;
              $state.go('fmcgmenu.sendMail');
          }
          $scope.goBack = function () {
              $scope.mailType = 'viewFolders';
          }
          function viewDetails(folder, month, year) {
              $ionicLoading.show({
                  template: 'Loading...'
              });
              fmcgAPIservice.getDataList('POST', 'getMailsApp&folder=' + folder + "&month=" + month + "&year=" + year, [])
               .success(function (response) {
                   $scope.Mails = response;
                   $ionicLoading.hide();
               }).error(function () {
                   $ionicLoading.hide();
                   Toast('No Internet Connection.');
               })
          }


      })
           .controller('myAudioCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $filter, $ionicLoading) {
               $scope.$parent.navTitle = "Product Detailing";
               $ionicLoading.show({
                   template: 'Loading...'
               });
               $scope.products = [];
               fmcgAPIservice.getDataList('POST', 'getProductDetailing', [])
                .success(function (response) {
                    $scope.products = response;
                    $ionicLoading.hide();
                }).error(function () {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                })
               $scope.view = function (product) {
                   $scope.$parent.productDetail = product;
                   $state.go('fmcgmenu.viewAudioDet');
               }
           })
     .controller('viewAudioDetCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $filter, $ionicLoading) {
         $scope.$parent.navTitle = "View Product Detailing";
         $scope.play = function (fileName, filetype) {
             var serverpath;
             if (filetype == "I")
                 serverpath = imgPath;
             if (filetype == "A")
                 serverpath = mp3Path;
             if (filetype == "V")
                 serverpath = mp4Path;
             //  var fileName = "test.mp3";
             serverpath = serverpath + fileName;
             // alert(serverpath);
             var filepath = cordova.file.externalApplicationStorageDirectory + "/FMCG/" + fileName;

             if (filetype == "I") {


                 var extn = fileName.split(".").pop();
                 if (extn == "png" || extn == "jpg")
                     mimetype = 'image/png';
                 else
                     mimetype = 'application/' + extn;
                 cordova.plugins.fileOpener2.open(
                      filepath,
                      mimetype,
                     {
                         error: function (e) {
                             $ionicLoading.show({
                                 template: 'Loading...'
                             });

                             var fileTransfer = new FileTransfer();
                             var uri = encodeURI(serverpath);
                             var fileURL = cordova.file.externalRootDirectory + "/Download/";
                             fileTransfer.download(
                                 uri,
                                 filepath,

                                 function (entry) {
                                     $ionicLoading.hide();
                                     alert("Download Completed");
                                     //  alert("download complete: " + entry.toURL());
                                     cordova.plugins.fileOpener2.open(
                                   filepath,
                                   mimetype,
                                  {
                                      error: function () {
                                          $ionicLoading.hide();
                                          alert('Error status: ' + e.status + ' - Error message: ' + e.message);

                                      },
                                      success: function () {
                                          $ionicLoading.hide();
                                          // alert("Success");
                                      }
                                  }
                                  );
                                 },
                                 function (error) {
                                     $ionicLoading.hide();
                                     // alert("download error source " + error.source);
                                     // alert("download error target " + error.target);
                                     // alert("download error code" + error.code);
                                 },
                                 false,
                                 {
                                     headers: {
                                         "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                                     }
                                 }
                             );
                         },
                         success: function () {
                             $ionicLoading.hide();
                             // alert("Success");
                         }
                     }
                     );
             } else {

                 var options = {
                     bgColor: "#FFFFFF",
                     bgImage: "image",
                     bgImageScale: "fit", // other valid values: "stretch"
                     initFullscreen: false, // true(default)/false iOS only
                     successCallback: function () {
                         alert("Player closed without error.");
                     },
                     errorCallback: function (errMsg) {
                         alert("Error! " + errMsg);
                         $ionicLoading.show({
                             template: 'Downloading...'
                         });
                         var fileTransfer = new FileTransfer();
                         var url = serverpath;
                         var uri = encodeURI(url);
                         fileTransfer.download(
                                uri,
                                filepath,
                                function (entry) {
                                    $ionicLoading.hide();
                                    alert("Download Completed");
                                    if (filetype == "V")
                                        window.plugins.streamingMedia.playVideo(filepath, options);
                                    else
                                        window.plugins.streamingMedia.playAudio(filepath, options);

                                },
                                function (error) {
                                    $ionicLoading.hide();
                                },
                                false,
                                {
                                    headers: {
                                        "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                                    }
                                }
                            );
                     }
                 }
             };
             if (filetype == "V")
                 window.plugins.streamingMedia.playVideo(filepath, options);
             else
                 window.plugins.streamingMedia.playAudio(filepath, options);


         }
     })
 .controller('LeaveForm', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $filter, $ionicLoading) {
     $scope.$parent.navTitle = "Leave Form";
     $scope.LeaveTypes.type = {};
     $scope.LeaveTypes.type.selected = {};
     $scope.data = {};
     // __DevID
     $scope.campaign = {
     };
     $scope.campaign.dateRange = [];

     // __DevID
     $ionicLoading.show({
         template: 'Loading...'
     });
     $scope.campaign.add_months = function (dt, n) {
         if (dt != undefined) {
             dateArray = dt.split("/");
             dt = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0])
             return new Date(dt.setMonth(dt.getMonth() + n));
         }
     }

     fmcgAPIservice.getDataList('POST', 'vwCheckLeave', [])
             .success(function (response) {
                 $scope.campaign.dateRange = response;
                 $ionicLoading.hide();
             }).error(function () {
                 $ionicLoading.hide();
                 Toast('No Internet Connection.');
             });

     $scope.save = function () {
         $scope.data.Leave_Type = $scope.LeaveTypes.type.selected.id;
         if ($scope.data.Leave_Type == undefined) {
             Toast('Select the Leave Type...');
             return false;
         }
         $scope.data.From_Date = $scope.campaign.start_at;
         if ($scope.data.From_Date == undefined) {
             Toast('Enter the From Date...');
             return false;
         }
         dateArray = ($scope.data.From_Date).split("/");
         $scope.data.From_Date = dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0];
         $scope.data.To_Date = $scope.campaign.end_at || 0;
         if ($scope.data.To_Date == undefined || $scope.data.To_Date == 0) {
             Toast('Enter the To Date...');
             return false;
         }
         dateArray = ($scope.data.To_Date).split("/");
         $scope.data.To_Date = dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0];
         $ionicLoading.show({
             template: 'Loading...'
         });

         var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
         var fieldforceName = loginInfo.sfName;
         fmcgAPIservice.addMAData('POST', 'dcr/save&sf_name=' + fieldforceName, "12", $scope.data).success(function (response) {
             if (response.success) {
                 Toast("Leave Form Submitted Successfully");
                 $ionicLoading.hide();
                 $state.go('fmcgmenu.home');
             }
         });
     }


 })
  .directive("startDateCalendar", [
  function () {
      return function (scope, element, attrs) {
          scope.$watch("campaign.dateRange", (function (newValue, oldValue) {
              element.datepicker({
                  beforeShowDay: function (date) {
                      var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                      return [scope.campaign.dateRange.indexOf(dateString) == -1];
                  }
              })
          }), true)
          return element.datepicker({
              dateFormat: "dd/mm/yy",
              numberOfMonths: 1,
              minDate: new Date(),
              //maxDate: scope.campaign.end_at,
              //beforeShowDay: $.datepicker.noWeekends,
              beforeShowDay: function (date) {
                  var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                  return [scope.campaign.dateRange.indexOf(dateString) == -1];
              },
              onSelect: function (date) {
                  scope.campaign.start_at = date;
                  scope.data.No_of_Days = 0;

                  scope.$apply();
                  scope.campaign.endDat = scope.campaign.add_months(date, 1);

              }
          });
      };
  }

  ]).directive("endDateCalendar", [
  function () {
      return function (scope, element, attrs) {
          scope.$watch("campaign.dateRange", (function (newValue, oldValue) {
              element.datepicker({
                  beforeShowDay: function (date) {
                      var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                      return [scope.campaign.dateRange.indexOf(dateString) == -1];
                  }
              })
          }), true)
          scope.$watch("campaign.start_at", (function (newValue, oldValue) {
              k = 0;
              scope.campaign.end_at = '';
              scope.campaign.endDat = scope.campaign.add_months(newValue, 1);
              for (i = 0; i <= scope.campaign.dateRange.length; i++) {
                  dat = scope.campaign.dateRange[i];

                  if (dat != undefined && newValue != undefined) {
                      dateArray = newValue.split("/");
                      dt1 = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0]);
                      dateArray = dat.split("/");
                      dt2 = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0])
                      if (dt1 < dt2) {
                          element.datepicker("option", "maxDate", scope.campaign.dateRange[i]);
                          k = 1;
                          i = scope.campaign.dateRange.length;
                      }
                  }
              }

              element.datepicker("option", "minDate", newValue);;
              if (k == 0)
                  element.datepicker("option", "maxDate", scope.campaign.endDat);
          }), true);

          return element.datepicker({
              dateFormat: "dd/mm/yy",
              numberOfMonths: 1,
              minDate: scope.campaign.start_at,
              defaultDate: scope.campaign.end_at,
              maxDate: scope.campaign.endDat,
              beforeShowDay: function (date) {
                  var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                  return [scope.campaign.dateRange.indexOf(dateString) == -1];
              }
,
              onSelect: function (date) {
                  var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
                  dateArray = scope.campaign.start_at.split("/");
                  var firstDate = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0]);
                  dateArray = date.split("/");
                  var secondDate = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0]);
                  var diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
                  scope.data.No_of_Days = diffDays + 1;
                  scope.campaign.end_at = date;
                  return scope.$apply();
              }
          });
      };
  }

  ])
        .controller('RMCLCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage) {
            $scope.$parent.navTitle = "Reminder Calls";
            $scope.gridOptions = {
                data: 'RMCL.productSelectedList',
                rowHeight: 50,
                rowTemplate: 'rowTemplate.html',
                enableCellSelection: true,
                enableColumnResize: true,
                enableRowSelection: false,
                plugins: [new ngGridFlexibleHeightPlugin()],
                showFooter: true,
                columnDefs: [{ field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html' },
                    { field: 'qty', displayName: 'Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60 },
                    { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
            };

            $scope.removeRow = function () {
                var index = this.row.rowIndex;
                $scope.gridOptions.selectItem(index, false);
                $scope.$parent.RMCL.productSelectedList.splice(index, 1);
            };
            $scope.clear = function () {
                if ($scope.view_MR == 1) {
                    $scope.RMCL.subordinate = {};
                    $scope.RMCL.subordinate.selected = {};
                    $scope.RMCL.subordinate.selected.id = $scope.sfCode;
                    $scope.loadDatas(false, '_' + $scope.sfCode)
                } else {
                    $scope.RMCL.subordinate = undefined;
                }
                $scope.RMCL.doctor = undefined;
                $scope.RMCL.JntWrkCds = "";
                $scope.RMCL.JntWrkNms = "";
                $scope.RMCL.jontWorkSelectedList = [];
                $scope.RMCL.ProdCds = "";
                $scope.RMCL.ProdNms = "";
                $scope.RMCL.productSelectedList = [];
                $scope.RMCL.remarks = "";
            }
            $scope.clear();
            $scope.save = function () {
                $scope.data = {};
                if ($scope.view_MR != 1) {
                    if ($scope.RMCL.subordinate == undefined || $scope.RMCL.subordinate.selected.id == "") {
                        Toast('Select the Headquarters');
                        return false;
                    }
                }
                if ($scope.RMCL.doctor == undefined || $scope.RMCL.doctor.selected.id == "") {
                    Toast('Select the ' + $scope.DrCap + ' Name');
                    return false;
                }
                if ($scope.RMCL.JntWrkCds == undefined)
                    $scope.RMCL.JntWrkCds = '';
                /*if ($scope.RMCL.JntWrkCds == undefined || $scope.RMCL.JntWrkCds == "") {
                 Toast('Select the Joint Work');
                 return false;
                 }*/
                if ($scope.RMCL.ProdCds == undefined || $scope.RMCL.ProdCds == "") {
                    Toast('Select the Product');
                    return false;
                }

                $scope.data.RMCLDt = new Date();
                $scope.data.DrId = $scope.RMCL.doctor.selected.id;
                $scope.data.DrName = $scope.RMCL.doctor.name;
                $scope.data.JntWrkCds = $scope.RMCL.JntWrkCds;
                $scope.data.JntWrkNms = $scope.RMCL.JntWrkNms;
                tmPrd = $scope.RMCL.productSelectedList;
                RMCLProdQty = "";
                RMCLProdNmQty = "";
                for (i = 0; i < tmPrd.length; i++) {
                    RMCLProdQty += tmPrd[i].product + " ( " + tmPrd[i].qty + " ), ";
                    RMCLProdNmQty += tmPrd[i].product_Nm + " ( " + tmPrd[i].qty + " ), ";
                }
                if (RMCLProdQty == '') {
                    Toast('Select the Product');
                    return false;
                }
                $scope.data.ProdCds = RMCLProdQty;
                $scope.data.ProdNms = RMCLProdNmQty;
                $scope.data.Remark = $scope.RMCL.remarks;
                $scope.clear();

                fmcgAPIservice.addMAData('POST', 'dcr/save', "5", $scope.data).success(function (response) {
                    if (response.success)
                        Toast("Reminder Calls Submited Successfully");
                }).error(function () {
                    $scope.OutRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
                    $scope.OutRMCL.push($scope.data);
                    localStorage.removeItem("OutBx_RMCL");
                    fmcgLocalStorage.createData("OutBx_RMCL", $scope.OutRMCL);
                    Toast("No Internet Connection!.\n Reminder Calls Saved in Outbox");
                });
                $state.go('fmcgmenu.home');
            };
        })
        .controller('orderEditCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, $ionicLoading) {

            $scope.edit = function (item) {
                $scope.tData1 = {};
                var prods = item['Additional_Prod_Code'].split("#");
                var prodNms = item['Additional_Prod_Dtls'].split("#");

                for (var m = 0, leg = prods.length; m < leg; m++) {
                    if (prods[m].length > 0) {
                        $scope.tData1['productSelectedList'] = $scope.tData1['productSelectedList'] || [];
                        var pTemp = {};
                        var prod = prods[m].split('~');
                        var prodNm = prodNms[m].split('~');
                        pTemp.checked = true;
                        pTemp.id = prod[0].toString().trim();
                        pTemp.name = prodNm[0].toString();
                        pTemp.product = prod[0].toString().trim();
                        pTemp.product_Nm = prodNm[0].toString();
                        var prodQ = prod[1].split('$');

                        pTemp.sample_qty = Number(prodQ[0]);
                        var prodQ = prodQ[1].split('@');
                        pTemp.rx_qty = prodQ[0];
                        pTemp.product_netwt = prodQ[1];
                        pTemp.netweightvalue = pTemp.product_netwt * pTemp.rx_qty;
                        if (pTemp.product.length !== 0)
                            $scope.tData1['productSelectedList'].push(pTemp);
                    }
                }
                $scope.$parent.fmcgData.productSelectedList = $scope.tData1['productSelectedList'];
                $scope.fmcgData.productSelectedList = $scope.tData1['productSelectedList'];
                $scope.fmcgData.pob = item.POB;
                $scope.fmcgData.value = item.POB_Value;
                $scope.fmcgData.netweightvaluetotal = item.net_weight_value;
                $scope.fmcgData.DCR_Code = item.DCR_Code;
                $scope.fmcgData.Cust_Code = item.Cust_Code;
                $scope.fmcgData.Trans_Sl_No = item.Trans_Sl_No;
                $scope.$parent.fmcgData.customer = {};
                $scope.$parent.fmcgData.customer.selected = {};
                $scope.$parent.fmcgData.customer.selected.id = "1";
                $scope.fmcgData.editableOrder = 1;
                $scope.fmcgData.route = item.Route;
                $scope.fmcgData.Stockist = item.Stockist_Code;
                $scope.$parent.fmcgData.OrdStk = {};
                $scope.$parent.fmcgData.OrdStk.selected = {};
                $scope.$parent.fmcgData.OrdStk.selected.id = item.Stockist_Code;
                $scope.myplan = fmcgLocalStorage.getData("mypln") || [];
                $scope.fmcgData.cluster = {};
                $scope.fmcgData.cluster.selected = {};
                $scope.fmcgData.cluster.selected.id = $scope.myplan[0]['clusterid'];
                $state.go('fmcgmenu.screen3');
            }

        })
        .controller('precallCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, $ionicLoading) {
            $scope.$parent.navTitle = "Precall Analysis";
            if ($scope.view_MR == 1) {
                $scope.precall.subordinate = {};
                $scope.precall.subordinate.selected = {};
                $scope.precall.subordinate.selected.id = $scope.sfCode;
                $scope.loadDatas(false, '_' + $scope.sfCode)
            }
            else {
                $scope.precall.subordinate = undefined
            }
            if ($scope.precall.subordinate !== undefined) {
                $scope.cMTPDId = '_' + $scope.precall.subordinate.selected.id;
                TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
                if (TpTwns.length < 1) {
                    $ionicLoading.show({
                        template: 'Please Wait. Data is Sync...'
                    });
                    $scope.clearAll(false, $scope.cMTPDId);
                } else {
                    $scope.loadDatas(false, $scope.cMTPDId);
                }
            }
            $scope.$on('clear', function (evnt, DrCd) {
                $scope.vwPreCall = undefined;
            });
            $scope.precall.doctor = undefined
            $scope.$on('getPreCall', function (evnt, DrCd) {
                $ionicLoading.show({
                    template: 'Loading...'
                });
                var id = $scope.precall.subordinate.selected.id;
                var towns = $scope.myTpTwns;
                var data = $scope.doctors;
                $scope.FilteredData = data.filter(function (a) {
                    return (a.town_code === id);
                });
                $scope.precall.TotalCalls = $scope.FilteredData.length;
                //                $scope.precall.productivity=

                fmcgAPIservice.getPostData('POST', 'get/precall&Msl_No=' + DrCd.DrId, []).success(function (response) {
                    var StockistDetails = response.StockistDetails;
                    for (var key in StockistDetails) {
                        var stockiesCode = StockistDetails[key]['stockist_code'];
                        var stockists = $scope.stockists;
                        for (var key1 in stockists) {
                            if (stockists[key1]['id'] == stockiesCode)
                                StockistDetails[key]['stockist_code'] = stockists[key]['name'];
                        }
                    }
                    $scope.vwPreCall = response;
                    $scope.StockistDetails = StockistDetails;
                    $ionicLoading.hide();
                }).error(function () {
                    Toast("No Internet Connection!");
                    $ionicLoading.hide();
                });
            });
        })
        .controller('draftCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.$parent.navTitle = "Draft Calls";
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.clearData();
            $scope.success = false;
            $scope.nodata = false;
            $scope.owsuccess = false;
            $scope.owTypeData = [];
            $scope.deleteEntry = function () {
                $ionicPopup.confirm({
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                })
                        .then(function (res) {
                            if (res) {
                                localStorage.removeItem("draft");
                                $state.go('fmcgmenu.home');
                                Toast("draft deleted successfully");
                            } else {
                                console.log('You are not sure');
                            }
                        });
            };
            $scope.editEntry = function () {
                var value = fmcgLocalStorage.getData("draft");
                for (key in value[0]) {
                    if (key) {
                        $scope.$parent.fmcgData[key] = value[0][key];
                    }
                }
                localStorage.removeItem("draft");
                $state.go('fmcgmenu.addNew');
            }
            $scope.saveEntry = function () {
                var temp = fmcgLocalStorage.getData("draft");
                fmcgAPIservice.saveDCRData('POST', 'dcr/save', temp[0], false).success(function (response) {
                    if (!response['success'] && response['type'] == 1) {
                        $scope.showConfirm = function () {
                            var confirmPopup = $ionicPopup.confirm({
                                title: 'Warning !',
                                template: response['msg']
                            });
                            confirmPopup.then(function (res) {
                                if (res) {
                                    fmcgAPIservice.updateDCRData('POST', 'dcr/save&replace', response['data'], false).success(function (response) {
                                        if (!response['success']) {
                                            Toast(response['msg'], true);
                                        }
                                        else {
                                            Toast("call Updated Successfully");
                                        }
                                    });
                                    localStorage.removeItem("draft");
                                } else {
                                    localStorage.removeItem("draft");
                                }
                            });
                        };
                        $scope.showConfirm();
                    }
                    else if (!response['success']) {
                        Toast(response['msg'], true);
                        localStorage.removeItem("draft");
                    }
                    else {
                        Toast("call Submited Successfully");
                        localStorage.removeItem("draft");
                    }
                });

            }

            //$scope.$parent.navTitle = "Submitted Calls";
            $scope.customers = [{
                'id': '1',
                'name': $scope.DrCap,
                'url': 'manageDoctorResult'
            }];
            var al = 1;
            if ($scope.ChmNeed != 1) {
                $scope.customers.push({
                    'id': '2',
                    'name': $scope.ChmCap,
                    'url': 'manageChemistResult'
                });
                $scope.cCI = al;
                al++;
            }
            if ($scope.StkNeed != 1) {
                $scope.customers.push({
                    'id': '3',
                    'name': $scope.StkCap,
                    'url': 'manageStockistResult'
                });
                $scope.sCI = al;
                al++;
            }
            if ($scope.UNLNeed != 1) {
                $scope.customers.push({
                    'id': '4',
                    'name': $scope.NLCap,
                    'url': 'manageStockistResult'
                });
                $scope.nCI = al;
                al++;
            }
            $scope.getData = function () {
                response = fmcgLocalStorage.getEntryCount();
                if (response['success']) {
                    $scope.success = true;
                    $scope.customers[0].count = response['data']['doctor_count'];
                    if ($scope.ChmNeed != 1)
                        $scope.customers[$scope.cCI].count = response['data']['chemist_count'];
                    if ($scope.StkNeed != 1)
                        $scope.customers[$scope.sCI].count = response['data']['stockist_count'];
                    if ($scope.UNLNeed != 1)
                        $scope.customers[$scope.nCI].count = response['data']['uldoctor_count'];
                }
                else if (response['ndata']) {
                    $scope.nodata = true;
                }
                else {
                    $scope.owsuccess = true;
                    $scope.owTypeData = response['data'];
                }
                $ionicLoading.hide();
            };
            $scope.getData();
        })
        .controller('outboxCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.$parent.navTitle = "Outbox Calls";
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.clearData();
            $scope.success = false;
            $scope.nodata = false;
            $scope.owsuccess = false;
            $scope.owTypeData = [];

            $scope.oxRmks = fmcgLocalStorage.getData("MyDyRmksQ") || [];
            $scope.oxMyPln = fmcgLocalStorage.getData("myplnQ") || [];
            $scope.delOutbxMyPln = function (item) {
                $ionicPopup.confirm(
                        {
                            title: 'Call Delete',
                            content: 'Are you sure you want to Delete?'
                        }).then(function (res) {
                            if (res) {
                                $scope.oxMyPln.splice($scope.oxMyPln.indexOf(item), 1);
                                fmcgLocalStorage.createData('myplnQ', $scope.oxMyPln);
                                Toast("My day plan successfully deleted from Outbox");
                            } else {
                                console.log('You are not sure');
                            }
                        }
                );
            };
            $scope.oxRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
            $scope.deleteOutbox = function (item) {
                $ionicPopup.confirm(
                        {
                            title: 'Call Delete',
                            content: 'Are you sure you want to Delete?'
                        }).then(function (res) {
                            if (res) {
                                $scope.oxRCPA.splice($scope.oxRCPA.indexOf(item), 1);
                                fmcgLocalStorage.createData('OutBx_RCPA', $scope.oxRCPA);
                                Toast("RCPA successfully deleted from Outbox");
                            } else {
                                console.log('You are not sure');
                            }
                        }
                );
            };
            $scope.oxRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
            $scope.delOutbxRMCL = function (item) {
                $ionicPopup.confirm(
                        {
                            title: 'Call Delete',
                            content: 'Are you sure you want to Delete?'
                        }).then(function (res) {
                            if (res) {
                                $scope.oxRMCL.splice($scope.oxRMCL.indexOf(item), 1);
                                fmcgLocalStorage.createData('OutBx_RMCL', $scope.oxRMCL);
                                Toast("Reminder call successfully deleted from Outbox");
                            } else {
                                console.log('You are not sure');
                            }
                        }
                );
            };



            $scope.deleteEntry = function (DelType) {
                $ionicPopup.confirm(
                        {
                            title: 'Call Delete',
                            content: 'Are you sure you want to Delete?'
                        }).then(function (res) {

                            if (res) {
                                localStorage.removeItem("saveLater");
                                $state.go('fmcgmenu.home');
                                Toast("draft deleted successfully");
                            } else {
                                console.log('You are not sure');
                            }
                        }
                );
            };
            $scope.customers = [{
                'id': '1',
                'name': $scope.DrCap,
                'url': 'manageDoctorResult'
            }];
            var al = 1;
            if ($scope.ChmNeed != 1) {
                $scope.customers.push({
                    'id': '2',
                    'name': $scope.ChmCap,
                    'url': 'manageChemistResult'
                });
                $scope.cCI = al;
                al++;
            }
            if ($scope.StkNeed != 1) {
                $scope.customers.push({
                    'id': '3',
                    'name': $scope.StkCap,
                    'url': 'manageStockistResult'
                });
                $scope.sCI = al;
                al++;
            }
            if ($scope.UNLNeed != 1) {
                $scope.customers.push({
                    'id': '4',
                    'name': $scope.NLCap,
                    'url': 'manageStockistResult'
                });
                $scope.nCI = al;
                al++;
            }
            $scope.getData = function () {
                response = fmcgLocalStorage.getOutboxCount();
                if (response['success']) {
                    $scope.success = true;
                    $scope.customers[0].count = response['data']['doctor_count'];
                    if ($scope.ChmNeed != 1)
                        $scope.customers[$scope.cCI].count = response['data']['chemist_count'];
                    if ($scope.StkNeed != 1)
                        $scope.customers[$scope.sCI].count = response['data']['stockist_count'];
                    if ($scope.UNLNeed != 1)
                        $scope.customers[$scope.nCI].count = response['data']['uldoctor_count'];
                }
                else if (response['ndata']) {
                    $scope.nodata = true;
                }
                else {
                    $scope.owsuccess = true;
                    $scope.owTypeData = response['data'];
                }
                $ionicLoading.hide();
            };
            $scope.getData();
            if ($scope.oxRCPA.length > 0 || $scope.oxRMCL.length > 0 || $scope.oxMyPln.length > 0 || $scope.oxRmks.length > 0) {
                $scope.nodata = false;
            }
            $scope.nDtaRCPA = ($scope.oxRCPA.length > 0) ? false : true;
            $scope.nDtaRMCL = ($scope.oxRMCL.length > 0) ? false : true;
            $scope.nDtaMyPL = ($scope.oxMyPln.length > 0) ? false : true;
            $scope.nDtaRmks = ($scope.oxRmks.length > 0) ? false : true;

        })
        .controller('transCurrentStocksCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
            $scope.$parent.navTitle = "Current Stocks";
            $scope.stockUpdation.stockist = {};
            $scope.stockUpdation.stockist.selected = {};
            $scope.$parent.fmcgData.currentLocation = "fmcgmenu.transCurrentStocks";
            $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
            if ($scope.view_STOCKIST == 1)
                $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];

            $scope.$on('GetStocks', function (evnt) {
                $scope.GetCurrentStock();
            });
            if ($scope.view_STOCKIST == 1)
                getStock();
            function getStock() {
                $scope.tdate = $filter('date')(new Date(), 'dd/MM/yyyy');
                if ($scope.LastUpdationDate == $scope.tdate)
                    $scope.editing = 0;
                else
                    $scope.editing = 1;
                if ($scope.view_STOCKIST != 1 && $scope.stockUpdation.stockist.selected != undefined)
                    $sCode = $scope.stockUpdation.stockist.selected.id
                else
                    $sCode = "0";
                fmcgAPIservice.getPostData('POST', 'get/currentStock&scode=' + $sCode, []).success(function (response) {
                    $scope.currentStocks = response;
                    $ionicLoading.hide();
                }).error(function () {
                    Toast("No Internet Connection!");
                    $ionicLoading.hide();
                });
            }
            $scope.GetCurrentStock = function () {
                getStock();
            }
            $scope.edit = function (item) {
                var value = $scope.currentStocks;
                $scope.$parent.fmcgData.editable = 1;
                $scope.$parent.fmcgData.productSelectedList = value;
                $state.go('fmcgmenu.stockUpdation');
            };

        })
        .controller('stockUpdationCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
            $scope.$parent.navTitle = "Stock Updation";
            $scope.$parent.fmcgData.currentLocation = "fmcgmenu.stockUpdation";
            $scope.myplan = fmcgLocalStorage.getData("mypln") || [];
            if ($scope.$parent.fmcgData.editable != 1) {
                $scope.stockUpdation.stockist = {};
                $scope.stockUpdation.stockist.selected = {};
            }

            $scope.Last_Updation_Date = [];
            $scope.tdate = $filter('date')(new Date(), 'dd/MM/yyyy');
            $scope.fmcgData.value1 = 0;
            $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
            $scope.setAllow();
            $scope.$on('GetStocks', function (evnt) {
                $scope.GetCurrentStock();
            });
            $scope.GetCurrentStock = function () {
                getStock();
            }
            if ($scope.view_STOCKIST == 1)
                getStock();
            function getStock() {
                if ($scope.view_STOCKIST == 1)
                    $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];
                else
                    $scope.$parent.fmcgData.productSelectedList = [];

                if ($scope.LastUpdationDate == $scope.tdate)
                    $scope.editing = 0;
                else
                    $scope.editing = 1;
                if ($scope.view_STOCKIST != 1 && $scope.stockUpdation.stockist.selected != undefined)
                    $sCode = $scope.stockUpdation.stockist.selected.id
                else
                    $sCode = "0";
                fmcgAPIservice.getPostData('POST', 'get/currentStock&scode=' + $sCode, []).success(function (response) {
                    $scope.currentStocks = response;
                    $ionicLoading.hide();
                }).error(function () {
                    Toast("No Internet Connection!");
                    $ionicLoading.hide();
                });
            }
            $scope.edit = function (item) {
                var value = $scope.currentStocks;
                $scope.$parent.fmcgData.editable = 1;
                $scope.$parent.fmcgData.productSelectedList = value;
                $state.go('fmcgmenu.stockUpdation');
            };
            $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
            $scope.addProduct = function (selected) {
                var productData = {};
                $scope.desigCode = $scope.desigCode;
                productData.product = selected;
                productData.product_Nm = (selected.toString() != "") ? $filter('getValueforID')(selected, $scope.products).name : "";
                productData.rx_qty = 0;
                productData.sample_qty = 0;
                var len = $scope.$parent.fmcgData.productSelectedList.length;
                var flag = true;
                for (var i = 0; i < len; i++) {
                    if ($scope.$parent.fmcgData.productSelectedList[i]['product'] === selected) {
                        flag = false;
                    }
                }
                if (flag)
                    $scope.$parent.fmcgData.productSelectedList.push(productData);
            }
            scrTyp = 1
            if (scrTyp == 1 || scrTyp == 4) {

                RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
                SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
                if ($scope.closing == 0)
                    $scope.condClosing = false;
                else
                    $scope.condClosing = true;
                if ($scope.recv == 0)
                    $scope.condRecv = false;
                else
                    $scope.condRecv = true;
                $scope.gridOptions = {
                    data: 'fmcgData.productSelectedList',
                    rowHeight: 50,
                    rowTemplate: 'rowTemplate.html',
                    enableCellSelection: true,
                    enableColumnResize: true,
                    enableRowSelection: false,
                    plugins: [new ngGridFlexibleHeightPlugin()],
                    showFooter: true,
                    columnDefs: [{ field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html' },
                        { field: 'recv_qty', displayName: "P.Stock", enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 70, "visible": $scope.condRecv },
                        { field: 'cb_qty', displayName: "UOM", enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate2.html", width: 55, "visible": $scope.condClosing },
                        { field: 'pieces', displayName: "Base UOM", enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate4.html", width: 90, "visible": $scope.condClosing },
                        { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]

                };

            }


            $scope.find = function () {
                if (this.row.entity.pieces >= this.row.entity.conversionQty) {
                    // var str = "Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")";
                    alert("Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")");
                    this.row.entity.pieces = 0;
                }
            }
            $scope.fmcgData.value = 0;
            $scope.removeRow = function () {
                var index = this.row.rowIndex;
                $scope.gridOptions.selectItem(index, false);
                $scope.$parent.fmcgData.productSelectedList.splice(index, 1);
            };
            $scope.save = function () {

                var products = $scope.$parent.fmcgData.productSelectedList;
                if ($scope.$parent.fmcgData.editable == 1)
                    var save = 'dcr/save&editable=' + 1;
                else
                    save = 'dcr/save&editable=' + 0;
                if ($scope.SF_type == 1)
                    $sCode = $scope.stockUpdation.stockist.selected.id
                else
                    $sCode = "0";
                save = save + '&sCode=' + $sCode;
                if ($scope.view_STOCKIST == 0) {
                    if ($scope.stockUpdation.stockist.selected.id == undefined) {
                        Toast("Please Select Distributors");
                        return false;
                    }
                }
                $ionicLoading.show({
                    template: 'Loading...'
                });
                fmcgAPIservice.addMAData('POST', save, '8', products)
                        .success(function (response) {
                            if (response['success']) {
                                var date = new Date();
                                if ($scope.view_STOCKIST == 1) {
                                    localStorage.removeItem("Last_Updation_Date");
                                    var Last_Updation_Date = [{ "Last_Updation_Date": $filter('date')(new Date(), 'dd/MM/yyyy') }]
                                }
                                else {
                                    var Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                                    stat = 0;
                                    for (key in Last_Updation_Date) {
                                        if (Last_Updation_Date[key]['stockistCode'] == $sCode) {
                                            stat = 1;
                                            Last_Updation_Date[key]['Last_Updation_Date'] = $filter('date')(new Date(), 'dd/MM/yyyy');
                                        }
                                    }
                                    if (stat == 0)
                                        Last_Updation_Date.push({ "Last_Updation_Date": $filter('date')(new Date(), 'dd/MM/yyyy'), "stockistCode": $sCode });
                                }
                                window.localStorage.setItem("Last_Updation_Date", JSON.stringify(Last_Updation_Date));
                                $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                                $state.go('fmcgmenu.transCurrentStocks');
                                $ionicLoading.hide();
                                Toast('Stock Updated', true);
                            }
                            //                                        fmcgLocalStorage.createData("myplnQ", QDataMyPL);
                            //                                        $scope.savingData.MyPL = false;
                        })
                        .error(function () {
                            //  $scope.savingData.MyPL = false;
                        });
                //                $state.go('fmcgmenu.home');
            }



        })
        .controller('reloadCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.$parent.navTitle = "Master Sync";
            $scope.countInc = 0;

            var temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            $scope.RefreshData = function () {
                var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id == "") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
                $scope.clearAll(false, '_' + sRSF);
            }
            $scope.clearItem = function (value) {
                var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id == "") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
                $scope.clearIdividual(value, 1, '_' + sRSF);
            };

        })
        .controller('draftViewCtrl', function ($rootScope, $scope, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.modal = $ionicModal;
            $scope.modal.fromTemplateUrl('partials/manageDataViewModal.html', function (modal) {
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
            switch ($scope.myChoice) {
                case "1":
                    $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
                    break;
                case "3":
                    $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
                    break;
                case "2":
                    $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
                    break;
                case "4":
                    $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
                    break;
            }
            $scope.draftButtons = [
                {
                    text: 'Edit',
                    type: 'button-calm',
                    onTap: function (item) {
                        $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                        var value = item;

                        for (key in value) {
                            if (key) {
                                $scope.$parent.fmcgData[key] = item[key];
                            }
                        }
                        fmcgLocalStorage.createData('draft', $scope.drafts);
                        $state.go('fmcgmenu.addNew');
                    }
                }
            ];

            $scope.edit = function (item) {
                $scope.loadDatas(false, '_' + item.subordinate.selected.id);
                $scope.fmcgData.isDraft = true;
                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                var value = item;

                for (key in value) {
                    if (key) {
                        $scope.$parent.fmcgData[key] = item[key];
                    }
                }
                fmcgLocalStorage.createData('draft', $scope.drafts);
                $state.go('fmcgmenu.screen2');
            };
            $scope.save = function (item) {

                fmcgAPIservice.saveDCRData('POST', 'dcr/save', item, false).success(function (response) {
                    if (!response['success']) {
                        Toast(response['msg'], true);
                    }
                    else {
                        Toast("call Submited Successfully")
                    }
                    $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                    fmcgLocalStorage.createData('draft', $scope.drafts);
                });

            };
            $scope.preView = function (item) {
                scrTyp = item.customer.selected.id;
                $scope.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
                $scope.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
                $scope.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
                $scope.modal.par = $scope;
                $scope.modal.draft = item;
                $scope.modal.show();
            };
            $scope.deleteDrftList = function (item) {
                $ionicPopup.confirm(
                        {
                            title: 'Call Delete',
                            content: 'Are you sure you want to Delete?'
                        }).then(function (res) {
                            if (res) {
                                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                                fmcgLocalStorage.createData('draft', $scope.drafts);
                                Toast("Call deleted successfully from draft");
                            } else {
                                console.log('You are not sure');
                            }
                        }
                );
            };
            $scope.drafts = [];
            $scope.drafts = fmcgLocalStorage.getData("draft");
        })
        .controller('outboxViewCtrl', function ($rootScope, $scope, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.outboxs = fmcgLocalStorage.getData("saveLater");
            $scope.modal = $ionicModal;
            $scope.modal.fromTemplateUrl('partials/manageDataViewModal.html', function (modal) {
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
            switch ($scope.myChoice) {
                case "1":
                    $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
                    break;
                case "3":
                    $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
                    break;
                case "2":
                    $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
                    break;
                case "4":
                    $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
                    break;
            }
            $scope.preView = function (item) {
                scrTyp = item.customer.selected.id;
                $scope.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
                $scope.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
                $scope.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
                $scope.modal.par = $scope;
                $scope.modal.draft = item;
                $scope.modal.show();
            };
            $scope.deleteOutbxList = function (item) {
                $ionicPopup.confirm(
                        {
                            title: 'Call Delete',
                            content: 'Are you sure you want to Delete?'
                        }).then(function (res) {
                            if (res) {
                                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                                fmcgLocalStorage.createData('saveLater', $scope.drafts);
                                Toast("Call deleted successfully from Outbox");
                            } else {
                                console.log('You are not sure');
                            }
                        }
                );
            };
            $scope.drafts = [];
            $scope.drafts = fmcgLocalStorage.getData("saveLater");
        })
    .controller('doortodoorViewCtrl', function ($rootScope, $scope, $ionicLoading, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
        $scope.editable = 0;
        $scope.doortodoor = true;
        fmcgAPIservice.getDataList('POST', 'table/list', ["doorToDoor", '["*"]', , , , , , , ]).success(function (response) {
            $scope.doors = response;
        })
        $scope.$parent.navTitle = "Submitted Door To Door";

        $scope.Edit = function (distributor) {
            $scope.$parent.navTitle = "Edit Door To Door"
            $scope.data = distributor;
            $scope.editable = 1;
        }
        $scope.hunt = function () {
            fmcgAPIservice.addMAData('POST', 'dcr/save', "21", $scope.data).success(function (response) {
                if (response.success) {
                    Toast("Updated Successfully");
                    $state.go('fmcgmenu.home');

                }
            });
        }
    })
    .controller('distibutorHuntCtrl', function ($rootScope, $scope, $ionicLoading, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
        $scope.editable = 0;
        $scope.distibutorHunt = true;
        fmcgAPIservice.getDataList('POST', 'table/list', ["DCRDetail_Distributors_Hunting", '["*"]', , , , , , , ]).success(function (response) {
            $scope.distributorHunt = response;
        })
        $scope.$parent.navTitle = "Submitted Distributor Hunt";

        $scope.Edit = function (distributor) {
            $scope.$parent.navTitle = "Edit Distributor Hunt"
            $scope.data = distributor;
            $scope.editable = 1;
        }
        $scope.Mypln.worktype = {};
        $scope.Mypln.worktype.selected = {};
        $scope.Mypln.worktype.selected.FWFlg = 'DH';
        var Wtyps = $scope.worktypes.filter(function (a) {
            return (a.FWFlg == 'DS')
        });
        $scope.Mypln.worktype.selected.id = Wtyps[0]['id'];
        $scope.hunt = function () {
            $scope.data.worktype = $scope.Mypln.worktype.selected.id;

            fmcgAPIservice.addMAData('POST', 'dcr/save', "21", $scope.data).success(function (response) {
                if (response.success) {
                    Toast("Updated Successfully");
                    $state.go('fmcgmenu.home');

                }
            });
        }
    })
        .controller('manageDataViewCtrl', function ($rootScope, $scope, $ionicLoading, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
            $ionicLoading.show({
                template: 'Loading...'
            });

            $scope.drafts = [];
            var tm = [];

            $scope.myChoice = $stateParams.myChoice;
            var ch = $scope.myChoice;
            $scope.$parent.navTitle = "Submitted " + ((ch == 1) ? $scope.DrCap : (ch == 2) ? $scope.ChmCap : (ch == 2) ? $scope.StkCap : (ch == 2) ? $scope.NLCap : 'calls');
            $scope.clearData();
            $scope.modal = $ionicModal;
            $scope.modal.fromTemplateUrl('partials/dataViewModal.html', function (modal) {
                $scope.modal = modal;
            }, {
                animation: 'slide-in-up',
                focusFirstInput: true
            });
            $scope.data = {
                showDelete: false
            };
            $scope.customerDatas;
            fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_report", '["*"]', , , 1, , 1, , "[\"activity_date asc\"]"]).success(function (response) {
                var tm = [];
                var pklist = [];
                for (var i = 0, len = response.length; i < len; i++) {
                    var tempData = {};
                    tempData['worktype'] = {};
                    tempData['worktype']['selected'] = {};
                    tempData['cluster'] = {};
                    tempData['cluster']['selected'] = {};
                    tempData['remarks'] = "";
                    tempData['arc'] = "";
                    tempData.worktype.selected.id = response[i]['Work_Type'];
                    tempData.worktype.selected.FWFlg = response[i]['FWFlg'];
                    tempData.arc = response[i]["Trans_SlNo"];

                    pklist.push("Trans_SlNo=\'" + tempData.arc + "\'");
                    $scope.loadEdDet(pklist, tempData);
                }
            }).error(function () {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
            $scope.loadEdDet = function (pklist, tempData) {
                switch ($scope.myChoice) {
                    case "1":
                        $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_msl_details", '["*,cast(time as datetime) tm"]', , JSON.stringify(pklist), , 1, , , '["cast(time as datetime)"]']).success(function (response1) {

                            for (var j = 0, le = response1.length; j < le; j++) {
                                tData = {};
                                tData = angular.copy(tempData);
                                tData['amc'] = response1[j]['Trans_Detail_Slno'];
                                tData['pob'] = parseInt(response1[j]['POB']);
                                tData['customer'] = {};
                                tData['customer']['selected'] = {}
                                tData['customer']['selected']['id'] = "1";
                                tData['doctor'] = {};
                                tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                                tData['doctor']['selected'] = {}
                                tData['doctor']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                                tData['doctor']['name'] = response1[j]['Trans_Detail_Name'];

                                tData.cluster.selected.id = response1[j]['SDP'];
                                tData.cluster.name = response1[j]['SDP_Name'];
                                tData['OrdStk'] = {};
                                tData['OrdStk']['selected'] = {};
                                //                                var stockists = $scope.stockists;
                                //                                var stockistCode = response1[j]['Stockist_Code'];
                                //                                for (var key1 in stockists)
                                //                                {
                                //                                    if (stockists[key1]['id'] == stockistCode)
                                //                                        tData.stockist.name = stockists[key1]['name'];
                                //                                }
                                var id = response1[j]['SDP'];
                                var towns = $scope.myTpTwns;
                                for (var key in towns) {
                                    if (towns[key]['id'] == id)
                                        $scope.fmcgData.rootTarget = towns[key]['target'];
                                }
                                tData.netweightvaluetotal = response1[j]["net_weight_value"];
                                tData.OrdStk.selected.id = response1[j]['Stockist_Code'];
                                tData.remarks = response1[j]["Activity_Remarks"];
                                tData.rx = response1[j]['Rx'];
                                tData['rx_t'] = response1[j]['Rx'];

                                /****
                                 if (tData.rx)
                                 tData.rx_t = response1[j]['rx_t'];
                                 else
                                 tData.nrx_t = response1[j]['nrx_t'];
                                 ****/

                                tData['subordinate'] = {};
                                tData['subordinate']['selected'] = {};
                                tData.subordinate.selected.id = response1[j]['DataSF'];

                                /**** 
                                 
                                 tData['OrdStk'] = {};
                                 tData['OrdStk']['selected'] = {}
                                 tData['OrdStk']['selected']['id'] = response1[j]['Order_Stk'];
                                 tData['OrderNo'] = parseInt(response1[j]['Order_No']);
                                 
                                 ****/

                                var response2 = response1[j]['Worked_with_Code'].split("$$");
                                var jw = response1[j]['Worked_with_Name'].split(",");
                                for (var m = 0, leg = response2.length; m < leg; m++) {
                                    tData['jontWorkSelectedList'] = tData['jontWorkSelectedList'] || [];
                                    var pTemp = {};
                                    pTemp.jointwork = response2[m].toString();
                                    pTemp.jointworkname = jw[m].toString();
                                    if (pTemp.jointwork.length !== 0)
                                        tData['jontWorkSelectedList'].push(pTemp);
                                }

                                var prods = (response1[j]['Product_Code'] + ((response1[j]['Product_Code'] == '') ? '' : '#') + response1[j]['Additional_Prod_Code']).split("#");
                                var prodNms = (response1[j]['Product_Detail'] + ((response1[j]['Product_Detail'] == '') ? '' : '#') + response1[j]['Additional_Prod_Dtls']).split("#");
                                for (var m = 0, leg = prods.length; m < leg; m++) {
                                    if (prods[m].length > 0) {
                                        tData['productSelectedList'] = tData['productSelectedList'] || [];
                                        var pTemp = {};
                                        var prod = prods[m].split('~');
                                        var prodNm = prodNms[m].split('~');
                                        pTemp.product = prod[0].toString().trim();
                                        pTemp.product_Nm = prodNm[0].toString();
                                        var prodQ = prod[1].split('$');
                                        pTemp.sample_qty = Number(prodQ[0]);
                                        pTemp.rx_qty = prodQ[1];
                                        pTemp.sample_qty = Number(prodQ[0]);
                                        var prodQ = prodQ[1].split('@');
                                        pTemp.rx_qty = prodQ[0];
                                        pTemp.product_netwt = prodQ[1];
                                        pTemp.netweightvalue = pTemp.product_netwt * pTemp.rx_qty;
                                        if (pTemp.product.length !== 0)
                                            tData['productSelectedList'].push(pTemp);
                                    }
                                }


                                var gfts = (response1[j]['Gift_Code'] + ((response1[j]['Gift_Code'] == '') ? '' : '~' + response1[j]['Gift_Qty'] + '#') + response1[j]['Additional_Gift_Code'].replace(/$/g, '')).split("#");
                                var gftNms = (response1[j]['Gift_Name'] + ((response1[j]['Gift_Name'] == '') ? '' : '~' + response1[j]['Gift_Qty'] + '#') + response1[j]['Additional_Gift_Dtl'].replace(/$/g, '')).split("#");
                                for (var m = 0, leg = gfts.length; m < leg; m++) {
                                    if (gfts[m].length > 0) {
                                        tData['giftSelectedList'] = tData['giftSelectedList'] || [];
                                        var giftTemp = {};
                                        var gft = gfts[m].split('~');
                                        var gftNm = gftNms[m].split('~');
                                        giftTemp.gift = gft[0];
                                        giftTemp.gift_Nm = gftNm[0];
                                        giftTemp.sample_qty = gft[1];
                                        if (giftTemp.gift.length !== 0)
                                            tData['giftSelectedList'].push(giftTemp);
                                    }
                                }

                                tData['entryDate'] = new Date(response1[j]['tm']['date']);
                                tData['modifiedDate'] = new Date(response1[j]['ModTime']['date']);

                                tm.push(tData);
                            }
                            $scope.drafts = tm;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                            Toast('No Internet Connection.');
                        });

                        break;
                    case "3":
                        $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_CSH_Detail", '["*"]', , JSON.stringify(pklist), , 3, , , '["stk_meet_time"]']).success(function (response1) {
                            for (var j = 0, le = response1.length; j < le; j++) {
                                tData = {};
                                tData = angular.copy(tempData);

                                tData['amc'] = response1[j]['Trans_Detail_Slno'];
                                tData['pob'] = parseInt(response1[j]['POB']);
                                tData['customer'] = {};
                                tData['customer']['selected'] = {}
                                tData['customer']['selected']['id'] = "3";
                                tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                                tData['stockist'] = {};
                                tData['stockist']['selected'] = {}
                                tData['stockist']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                                tData['stockist']['name'] = response1[j]['Trans_Detail_Name'];

                                tData.cluster.selected.id = response1[j]['SDP'];
                                tData.cluster.name = response1[j]['SDP_Name'];
                                tData.remarks = response1[j]["Activity_Remarks"];
                                tData.rx = response1[j]['Rx'];
                                tData['rx_t'] = response1[j]['Rx'];

                                /****
                                 if (tData.rx)
                                 tData.rx_t = response1[j]['rx_t'];
                                 else
                                 tData.nrx_t = response1[j]['nrx_t'];
                                 ****/

                                tData['subordinate'] = {};
                                tData['subordinate']['selected'] = {};
                                tData.subordinate.selected.id = response1[j]['DataSF'];

                                var response2 = response1[j]['Worked_with_Code'].split("$$");
                                var jw = response1[j]['Worked_with_Name'].split(",");
                                for (var m = 0, leg = response2.length; m < leg; m++) {
                                    tData['jontWorkSelectedList'] = tData['jontWorkSelectedList'] || [];
                                    var pTemp = {};
                                    pTemp.jointwork = response2[m].toString();
                                    pTemp.jointworkname = jw[m].toString();
                                    if (pTemp.jointwork.length !== 0)
                                        tData['jontWorkSelectedList'].push(pTemp);
                                }


                                var prods = response1[j]['Additional_Prod_Code'].split("#");
                                var prodNms = response1[j]['Additional_Prod_Dtls'].split("#");
                                for (var m = 0, leg = prods.length; m < leg; m++) {
                                    if (prods[m].length > 0) {
                                        tData['productSelectedList'] = tData['productSelectedList'] || [];
                                        var pTemp = {};
                                        var prod = prods[m].split('~');
                                        var prodNm = prodNms[m].split('~');
                                        pTemp.product = prod[0].toString();
                                        pTemp.product_Nm = prodNm[0].toString();
                                        pTemp.rx_qty = prod[1];
                                        if (pTemp.product.length !== 0)
                                            tData['productSelectedList'].push(pTemp);
                                    }
                                }


                                var gfts = response1[j]['Additional_Gift_Code'].split("#");
                                var gftNms = response1[j]['Additional_Gift_Dtl'].split("#");
                                for (var m = 0, leg = gfts.length; m < leg; m++) {
                                    if (gfts[m].length > 0) {
                                        tData['giftSelectedList'] = tData['giftSelectedList'] || [];
                                        var giftTemp = {};
                                        var gft = gfts[m].split('~');
                                        var gftNm = gftNms[m].split('~');
                                        giftTemp.gift = gft[0];
                                        giftTemp.gift_Nm = gftNm[0];
                                        giftTemp.sample_qty = gft[1];
                                        if (giftTemp.gift.length !== 0)
                                            tData['giftSelectedList'].push(giftTemp);
                                    }
                                }

                                tData['entryDate'] = new Date(response1[j]['vstTime']['date']);
                                tData['modifiedDate'] = new Date(response1[j]['ModTime']['date']);

                                tm.push(tData);
                            }
                            $scope.drafts = tm;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                            Toast('No Internet Connection.');
                        });
                        break;
                    case "2":
                        $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_CSH_Detail", '["*"]', , JSON.stringify(pklist), , 2, , , '["chm_meet_time"]']).success(function (response1) {
                            for (var j = 0, le = response1.length; j < le; j++) {
                                tData = {};
                                tData = angular.copy(tempData);

                                tData['amc'] = response1[j]['Trans_Detail_Slno'];
                                tData['pob'] = parseInt(response1[j]['POB']);
                                tData['customer'] = {};
                                tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                                tData['customer']['selected'] = {}
                                tData['customer']['selected']['id'] = "2";
                                tData['chemist'] = {};
                                tData['chemist']['selected'] = {}
                                tData['chemist']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                                tData['chemist']['name'] = response1[j]['Trans_Detail_Name'];

                                tData.cluster.selected.id = response1[j]['SDP'];
                                tData.cluster.name = response1[j]['SDP_Name'];
                                tData.remarks = response1[j]["Activity_Remarks"];
                                tData.rx = response1[j]['Rx'];
                                tData['rx_t'] = response1[j]['Rx'];

                                /****
                                 if (tData.rx)
                                 tData.rx_t = response1[j]['rx_t'];
                                 else
                                 tData.nrx_t = response1[j]['nrx_t'];
                                 ****/

                                tData['subordinate'] = {};
                                tData['subordinate']['selected'] = {};
                                tData.subordinate.selected.id = response1[j]['DataSF'];


                                var response2 = response1[j]['Worked_with_Code'].split("$$");
                                var jw = response1[j]['Worked_with_Name'].split(",");
                                for (var m = 0, leg = response2.length; m < leg; m++) {
                                    tData['jontWorkSelectedList'] = tData['jontWorkSelectedList'] || [];
                                    var pTemp = {};
                                    pTemp.jointwork = response2[m].toString();
                                    pTemp.jointworkname = jw[m].toString();
                                    if (pTemp.jointwork.length !== 0)
                                        tData['jontWorkSelectedList'].push(pTemp);
                                }


                                var prods = response1[j]['Additional_Prod_Code'].split("#");
                                var prodNms = response1[j]['Additional_Prod_Dtls'].split("#");
                                for (var m = 0, leg = prods.length; m < leg; m++) {
                                    if (prods[m].length > 0) {
                                        tData['productSelectedList'] = tData['productSelectedList'] || [];
                                        var pTemp = {};
                                        var prod = prods[m].split('~');
                                        var prodNm = prodNms[m].split('~');
                                        pTemp.product = prod[0].toString();
                                        pTemp.product_Nm = prodNm[0].toString();
                                        pTemp.rx_qty = prod[1];
                                        if (pTemp.product.length !== 0)
                                            tData['productSelectedList'].push(pTemp);
                                    }
                                }


                                var gfts = response1[j]['Additional_Gift_Code'].split("#");
                                var gftNms = response1[j]['Additional_Gift_Dtl'].split("#");
                                for (var m = 0, leg = gfts.length; m < leg; m++) {
                                    if (gfts[m].length > 0) {
                                        tData['giftSelectedList'] = tData['giftSelectedList'] || [];
                                        var giftTemp = {};
                                        var gft = gfts[m].split('~');
                                        var gftNm = gftNms[m].split('~');
                                        giftTemp.gift = gft[0];
                                        giftTemp.gift_Nm = gftNm[0];
                                        giftTemp.sample_qty = gft[1];
                                        if (giftTemp.gift.length !== 0)
                                            tData['giftSelectedList'].push(giftTemp);
                                    }
                                }

                                tData['entryDate'] = new Date(response1[j]['vstTime']['date']);
                                tData['modifiedDate'] = new Date(response1[j]['ModTime']['date']);

                                tm.push(tData);
                            }
                            $scope.drafts = tm;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                            Toast('No Internet Connection.');
                        });
                        break;
                    case "4":
                        $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_Unlst_Detail", '["*,cast(time as datetime) tm"]', , JSON.stringify(pklist), , 1, , , '["cast(time as datetime)"]']).success(function (response1) {
                            for (var j = 0, le = response1.length; j < le; j++) {

                                tData = {};
                                tData = angular.copy(tempData);

                                tData['amc'] = response1[j]['Trans_Detail_Slno'];
                                tData['pob'] = parseInt(response1[j]['POB']);
                                tData['customer'] = {};
                                tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                                tData['customer']['selected'] = {}
                                tData['customer']['selected']['id'] = "4";
                                tData['uldoctor'] = {};
                                tData['uldoctor']['selected'] = {}
                                tData['uldoctor']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                                tData['uldoctor']['name'] = response1[j]['Trans_Detail_Name'];

                                tData.cluster.selected.id = response1[j]['SDP'];
                                tData.cluster.name = response1[j]['SDP_Name'];
                                tData.remarks = response1[j]["Activity_Remarks"];
                                tData.rx = response1[j]['Rx'];
                                tData['rx_t'] = response1[j]['Rx'];

                                /****
                                 if (tData.rx)
                                 tData.rx_t = response1[j]['rx_t'];
                                 else
                                 tData.nrx_t = response1[j]['nrx_t'];
                                 ****/

                                tData['subordinate'] = {};
                                tData['subordinate']['selected'] = {};
                                tData.subordinate.selected.id = response1[j]['DataSF'];

                                var response2 = response1[j]['Worked_with_Code'].split("$$");
                                var jw = response1[j]['Worked_with_Name'].split(",");
                                for (var m = 0, leg = response2.length; m < leg; m++) {
                                    tData['jontWorkSelectedList'] = tData['jontWorkSelectedList'] || [];
                                    var pTemp = {};
                                    pTemp.jointwork = response2[m].toString();
                                    pTemp.jointworkname = jw[m].toString();
                                    if (pTemp.jointwork.length !== 0)
                                        tData['jontWorkSelectedList'].push(pTemp);
                                }

                                var prods = (response1[j]['Product_Code'] + ((response1[j]['Product_Code'] == '') ? '' : '#') + response1[j]['Additional_Prod_Code']).split("#");
                                var prodNms = (response1[j]['Product_Detail'] + ((response1[j]['Product_Detail'] == '') ? '' : '#') + response1[j]['Additional_Prod_Dtls']).split("#");
                                for (var m = 0, leg = prods.length; m < leg; m++) {
                                    if (prods[m].length > 0) {
                                        tData['productSelectedList'] = tData['productSelectedList'] || [];
                                        var pTemp = {};
                                        var prod = prods[m].split('~');
                                        var prodNm = prodNms[m].split('~');
                                        pTemp.product = prod[0].toString();
                                        pTemp.product_Nm = prodNm[0].toString();
                                        var prodQ = prod[1].split('$');
                                        pTemp.sample_qty = prodQ[0];
                                        pTemp.rx_qty = prodQ[1];

                                        if (pTemp.product.length !== 0)
                                            tData['productSelectedList'].push(pTemp);
                                    }
                                }


                                var gfts = (response1[j]['Gift_Code'] + ((response1[j]['Gift_Code'] == '') ? '' : '~' + response1[j]['Gift_Qty'] + '#') + response1[j]['Additional_Gift_Code'].replace(/$/g, '')).split("#");
                                var gftNms = (response1[j]['Gift_Name'] + ((response1[j]['Gift_Name'] == '') ? '' : '~' + response1[j]['Gift_Qty'] + '#') + response1[j]['Additional_Gift_Dtl'].replace(/$/g, '')).split("#");
                                for (var m = 0, leg = gfts.length; m < leg; m++) {
                                    if (gfts[m].length > 0) {
                                        tData['giftSelectedList'] = tData['giftSelectedList'] || [];
                                        var giftTemp = {};
                                        var gft = gfts[m].split('~');
                                        var gftNm = gftNms[m].split('~');
                                        giftTemp.gift = gft[0];
                                        giftTemp.gift_Nm = gftNm[0];
                                        giftTemp.sample_qty = gft[1];
                                        if (giftTemp.gift.length !== 0)
                                            tData['giftSelectedList'].push(giftTemp);
                                    }
                                }

                                tData['entryDate'] = new Date(response1[j]['tm']['date']);
                                tData['modifiedDate'] = new Date(response1[j]['ModTime']['date']);

                                tm.push(tData);
                            }
                            $scope.drafts = tm;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                            Toast('No Internet Connection.');
                        });
                        break;
                }
            }

            $scope.edit = function (item) {
                var TwnDet = fmcgLocalStorage.getData("town_master_" + item.subordinate.selected.id) || [];
                if (TwnDet.length > 0) {
                    $scope.loadDatas(false, '_' + item.subordinate.selected.id);
                }
                else if (TwnDet.length <= 0) {
                    $scope.clearAll(false, '_' + item.subordinate.selected.id);
                }

                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                var value = item;

                for (key in value) {
                    if (key) {
                        $scope.$parent.fmcgData[key] = item[key];

                    }
                }
                if (item.productSelectedList != undefined) {
                    var values = 0;
                    var products = item.productSelectedList;
                    for (var key in products) {
                        values = +products[key]['sample_qty'] + +values;
                    }
                    $scope.fmcgData.value = Math.floor(values);
                }
                $scope.fmcgData.editableOrder = 1;
                //fmcgLocalStorage.createData('draft', $scope.drafts);
                $state.go('fmcgmenu.screen2');
            };
            $scope.editOrder = function (item) {
                fmcgAPIservice.getDataList('POST', 'get/vwOrderDetails&DCR_Code=' + item.amc, [])
                        .success(function (response) {

                            $scope.fmcgData.EOrders = response;
                            $state.go('fmcgmenu.orderEdit');
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                            Toast('No Internet Connection.');
                        });
                $scope.fmcgData.editableOrder = 1;
            }
            $scope.preView = function (item) {
                scrTyp = item.customer.selected.id
                $scope.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
                $scope.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
                $scope.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
                $scope.modal.par = $scope;
                if (item.productSelectedList != undefined) {
                    var values = 0;
                    var products = item.productSelectedList;
                    for (var key in products) {
                        values = +products[key]['sample_qty'] + +values;
                    }
                    var orderValue = Math.floor(values);
                }
                $scope.modal.draft = item;
                $scope.modal.draft.orderValue = orderValue;
                $scope.modal.show();
            };
            $scope.deleteDraft = function (item) {
                $ionicPopup.confirm(
                        {
                            title: 'Call Delete',
                            content: 'Are you sure you want to Delete?'
                        }).then(function (res) {
                            if (res) {
                                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                                fmcgAPIservice.deleteEntry('POST', 'deleteEntry', item).success(function (response) {
                                    Toast("call deleted successfully");
                                });
                            } else {
                                console.log('You are not sure');
                            }
                        });
            };
        }).controller('dcrData', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.$parent.navTitle = "Monthly Summary";
            $scope.clearData();
            $scope.dcrDataCur = {};
            fmcgAPIservice.getDataList('POST', 'dcr/callReport', []).success(function (response) {
                var data = response;
                var temp = [];
                for (var i = 0; i < data.length; i++) {
                    var flag = true;
                    for (var j = 0; j < temp.length; j++) {
                        if (data[i]['day_dcr'] == temp[j]['day_dcr']) {
                            temp[j]['town_code'] = temp[j]['town_code'] + ',\n' + data[i]['town_code'];
                            temp[j]['chm_count'] = parseInt(temp[j]['chm_count']) + parseInt(data[i]['chm_count']);
                            temp[j]['stk_count'] = parseInt(temp[j]['stk_count']) + parseInt(data[i]['stk_count']);
                            temp[j]['doc_count'] = parseInt(temp[j]['doc_count']) + parseInt(data[i]['doc_count']);
                            temp[j]['uldoc_count'] = parseInt(temp[j]['uldoc_count']) + parseInt(data[i]['uldoc_count']);

                            flag = false;
                        }

                    }

                    if (flag) {
                        temp.push(data[i]);
                    }
                }
                $scope.dcrDataCur = temp;
            }).error(function () {
                Toast('No Internet Connection.');
            });

        })

        .controller('tpviewCtrl', function ($scope, $ionicLoading, fmcgAPIservice) {
            $scope.$parent.navTitle = "Tour plan view";
            $scope.tpvwlists = {};
            $scope.mnthDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

            $scope.subhdTop = (($scope.view_MR == 1) ? '44' : '88') + 'px';
            $scope.CntvwTop = (($scope.view_MR == 1) ? '88' : '130') + 'px';
            $scope.$on('eGetTpEntry', function (evnt) {
                $scope.GetTpEntry();
            });

            var temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            $scope.tpview.subordinate = {};
            $scope.tpview.subordinate.name = userData.sfName;
            $scope.tpview.subordinate.selected = {};
            $scope.tpview.subordinate.selected.id = userData.sfCode;

            $scope.GetTpEntry = function () {
                $ionicLoading.show({
                    template: 'Loading...'
                });
                var params = {};
                $scope.error = "";
                params.sfCode = ($scope.desigCode == 'MR') ? $scope.sfCode : $scope.tpview.subordinate.selected.id;
                params.mnthYr = $scope.mnthDt;
                fmcgAPIservice.getPostData('POST', 'tpview', params).success(
                        function (response) {
                            $scope.tpvwlists = JSON.parse(JSON.stringify(response));
                            if ($scope.tpvwlists.length <= 0) {
                                $scope.error = "TP Entry Not Yet Updated...";
                            }
                            ;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                        });
            }
            $scope.GetTpEntry();
        })
        .controller('tpviewdtCtrl', function ($scope, $ionicLoading, fmcgAPIservice) {
            $scope.$parent.navTitle = "Datewise Tour plan view";
            $scope.tpDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
            $scope.dtTPvwlists = {};

            $scope.$on('GetTpEntryDt', function (evnt) {
                $scope.GetTpEntryDtws();
            });

            $scope.GetTpEntryDtws = function () {
                $ionicLoading.show({
                    template: 'Loading...'
                });
                var params = {};
                $scope.error = "";
                var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
                params.tpDate = $scope.tpDate;
                params.sfCode = $scope.sfCode;
                fmcgAPIservice.getPostData('POST', 'tpviewdt', params)
                        .success(function (response) {
                            $scope.dtTPvwlists = JSON.parse(JSON.stringify(response));
                            if ($scope.dtTPvwlists.length <= 0) {
                                $scope.error = "TP Entry Not Yet Updated...";
                            }
                            ;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                        });
            }
            $scope.GetTpEntryDtws();
        })
     .controller('BrndSummary', function ($rootScope, $ionicLoading, $ionicScrollDelegate, $ionicModal, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {

         $scope.$parent.navTitle = "Brandwise Sale Summary";
         $scope.clearData();
         $scope.BrndDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
         var temp = window.localStorage.getItem("loginInfo");
         var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
         $scope.BrndSumm.subordinate = {};
         $scope.BrndSumm.subordinate.name = userData.sfName;
         $scope.BrndSumm.subordinate.selected = {};
         $scope.BrndSumm.subordinate.selected.id = userData.sfCode;
         $scope.Mypln.stockist = {};
         $scope.Mypln.stockist.selected = {};
         $scope.roundNumber = function (number, precision) {
             precision = Math.abs(parseInt(precision)) || 0;
             var multiplier = Math.pow(10, precision);
             return parseFloat(Math.round(number * multiplier) / multiplier).toFixed(2);
         }
         $scope.$on('getBrandSalSumry', function (evnt) {
             $scope.getBrandSalSumm();
         });
         var getCanvas;
         $scope.BrndSumm.stockist = {};
         $scope.BrndSumm.stockist.selected = {};
         $scope.sendWhatsApp = function () {
             $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
             $('ion-nav-view').css("height", $('#preview').height() + 300);
             $('body').css("overflow", 'visible');
             html2canvas($('#preview'), {
                 onrendered: function (canvas) {
                     var canvasImg = canvas.toDataURL("image/jpg");
                     getCanvas = canvas;
                     $('ion-nav-view').css("height", "");
                     $('body').css("overflow", 'hidden');
                     window.plugins.socialsharing.shareViaWhatsApp(null, canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                 }
             });
         }
         $scope.sendEmail = function () {
             $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
             $('ion-nav-view').css("height", $('#preview').height() + 300);
             $('body').css("overflow", 'visible');
             html2canvas($('#preview'), {
                 onrendered: function (canvas) {
                     var canvasImg = canvas.toDataURL("image/jpg");
                     $('ion-nav-view').css("height", "");
                     $('body').css("overflow", 'hidden');
                     window.plugins.socialsharing.share('', 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.vwCalls.Adate), canvasImg, '');
                 }
             });
         }

         $scope.subhdTop = (($scope.view_MR == 1 || $scope.view_STOCKIST == 1) ? '44' : '88') + 'px';
         $scope.CntvwTop = (($scope.view_MR == 1 || $scope.view_STOCKIST == 1) ? '75' : '130') + 'px';
         if ($scope.SF_type == 2) {
             $scope.subhdTop = '132px';
             $scope.CntvwTop = '174px';
         }
         $scope.getTVal = function () {
             arr = $scope.BrndSalSumm || [];
             oVal = 0;
             for (il = 0; il < arr.length; il++) {
                 oVal += arr[il].OVal;
             }
             return parseFloat(oVal).toFixed(2);
         }
         $scope.getCVal = function () {
             arr = $scope.BrndSalSumm || [];
             oVal = 0;
             for (il = 0; il < arr.length; il++) {
                 oVal += arr[il].TOVal;
             }
             return parseFloat(oVal).toFixed(2);
         }

         $scope.getBrandSalSumm = function () {
             $ionicLoading.show({
                 template: 'Loading...'
             });
             var stockist = $scope.SF_type == 2 ? $scope.Mypln.stockist.selected.id : $scope.BrndSumm.stockist.selected.id;
             fmcgAPIservice.getDataList('POST', 'get/BrndSumm&rptSF=' + $scope.BrndSumm.subordinate.selected.id + '&rptDt=' + $scope.BrndDt + '&stockistCode=' + stockist, [])
                     .success(function (response) {
                         $scope.BrndSalSumm = response;

                         $ionicLoading.hide();
                     }).error(function () {
                         $ionicLoading.hide();
                         Toast('No Internet Connection.');
                     });
         }
         $scope.getBrandSalSumm();
     })
        .controller('mnthSummary', function ($rootScope, $ionicLoading, $ionicScrollDelegate, $ionicModal, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.modal = $ionicModal;
            $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function (modal) {
                $scope.modal = modal;
            }, {
                animation: 'slide-in-up',
                focusFirstInput: true
            });
       
            $scope.ViewDetail = function (Acd, SlTyp, Adt, type) {
                $scope.modal.type = type;
                $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.DrCap : (SlTyp == 2) ? $scope.ChmCap : (SlTyp == 3) ? $scope.StkCap : (SlTyp == 4) ? $scope.NLCap : '') + ' Visit Details For : ' + Adt;
                if ($scope.modal.type == 0)
                    $scope.modal.rptTitle = ($scope.DrCap) + ' Visit Details For : ' + Adt;
                if ($scope.modal.type == 1)
                    $scope.modal.rptTitle = ($scope.DrCap) + ' Order Details For : ' + Adt;
                if (SlTyp == 4)
                    $scope.modal.rptTitle = 'New Distributor Hunting Details For : ' + Adt;
                if (SlTyp == 6)
                    $scope.modal.rptTitle = 'Door To Door Details For : ' + Adt;
                $scope.modal.show();
                $scope.modal.sendWhatsApp = function () {
                    $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                    $('ion-nav-view').css("height", $('#vstpreview').height() + 300);
                    $('body').css("overflow", 'visible');
                    html2canvas($('#vstpreview'), {
                        onrendered: function (canvas) {
                            var canvasImg = canvas.toDataURL("image/jpg");
                            getCanvas = canvas;
                            $('ion-nav-view').css("height", "");
                            $('body').css("overflow", 'hidden');
                            console.log(canvasImg);
                            window.plugins.socialsharing.shareViaWhatsApp('Summary Dashboard', canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                        }
                    });
                }
                $scope.modal.sendEmail = function () {
                    $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                    $('ion-nav-view').css("height", $('#vstpreview').height() + 300);
                    $('body').css("overflow", 'visible');
                    html2canvas($('#vstpreview'), {
                        onrendered: function (canvas) {
                            var canvasImg = canvas.toDataURL("image/jpg");
                            $('ion-nav-view').css("height", "");
                            $('body').css("overflow", 'hidden');
                            window.plugins.socialsharing.share('', 'Summary Dashboard', canvasImg, '');
                        }
                    });
                }

                $ionicLoading.show({
                    template: 'Loading...'
                });
                $scope.modal.vwVstlists = [];
                if (SlTyp == 4) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["DCRDetail_Distributors_Hunting", '["*"]', , , , , , , ]).success(function (response) {
                        $scope.modal.vwVstlists = response;
                        $scope.modal.hunt = 0;
                        $ionicLoading.hide();
                       
                    })
                }

              else if (SlTyp == 6) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["doorToDoor", '["*"]', , , , , , , ]).success(function (response) {
                        $scope.modal.vwVstlists = response;
                        $scope.modal.door = 0;
                        $ionicLoading.hide();
                    })
                }
                else {
                    fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                            .success(function (response) {
                                $scope.modal.hunt = 1;
                                if ($scope.modal.type == 1) {
                                    for (var i = response.length - 1; i >= 0; i--) {
                                        if (response[i]['orderValue'] == 0 || response[i]['orderValue'] == null)
                                            response.splice(i, 1);
                                    }
                                }
                                $scope.modal.vwVstlists = response;
                                $ionicLoading.hide();
                            }).error(function () {
                                $ionicLoading.hide();
                                Toast('No Internet Connection.');
                            });
                }
                $ionicScrollDelegate.scrollTop();
            };
            $scope.$parent.navTitle = "Monthly Summary";
            $scope.clearData();
            $scope.mnthDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

            var temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            $scope.MnthSumm.subordinate = {};
            $scope.MnthSumm.subordinate.name = userData.sfName;
            $scope.MnthSumm.subordinate.selected = {};
            $scope.MnthSumm.subordinate.selected.id = userData.sfCode;

            $scope.$on('getMonthReport', function (evnt) {
                $scope.getMonthReports();
            });

            $scope.subhdTop = (($scope.view_MR == 1) ? '44' : '88') + 'px';
            $scope.CntvwTop = (($scope.view_MR == 1) ? '75' : '130') + 'px';
            $scope.getMonthReports = function () {
                $ionicLoading.show({
                    template: 'Loading...'
                });
                fmcgAPIservice.getDataList('POST', 'get/MnthSumm&rptSF=' + $scope.MnthSumm.subordinate.selected.id + '&rptDt=' + $scope.mnthDt, [])
                        .success(function (response) {
                            $scope.MnthDCRList = response;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                            Toast('No Internet Connection.');
                        });
            }
            $scope.getMonthReports();
        })
        .controller('dayReport', function ($rootScope, $ionicLoading, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.modal = $ionicModal;
            $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function (modal) {
                $scope.modal = modal;
            }, {
                animation: 'slide-in-up',
                focusFirstInput: true
            });

            $scope.ViewDetail = function (Acd, SlTyp, Adt, type) {
                $scope.modal.type = type;
                $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.DrCap : (SlTyp == 2) ? $scope.ChmCap : (SlTyp == 3) ? $scope.StkCap : (SlTyp == 4) ? $scope.NLCap : '') + ' Visit Details For : ' + Adt;
                if ($scope.modal.type == 0)
                    $scope.modal.rptTitle = ($scope.DrCap) + ' Visit Details For : ' + Adt;
                if ($scope.modal.type == 1)
                    $scope.modal.rptTitle = ($scope.DrCap) + ' Order Details For : ' + Adt;
                if (SlTyp == 6)
                    $scope.modal.rptTitle = 'Door To Door Details For : ' + Adt;
               
                $scope.modal.show();
                $scope.modal.sendWhatsApp = function () {
                    $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                    $('ion-nav-view').css("height", $('#vstpreview').height() + 300);
                    $('body').css("overflow", 'visible');
                    html2canvas($('#vstpreview'), {
                        onrendered: function (canvas) {
                            var canvasImg = canvas.toDataURL("image/jpg");
                            getCanvas = canvas;
                            $('ion-nav-view').css("height", "");
                            $('body').css("overflow", 'hidden');
                            console.log(canvasImg);
                            window.plugins.socialsharing.shareViaWhatsApp('Summary Dashboard', canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                        }
                    });
                }
                $scope.modal.sendEmail = function () {
                    $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                    $('ion-nav-view').css("height", $('#vstpreview').height() + 300);
                    $('body').css("overflow", 'visible');
                    html2canvas($('#vstpreview'), {
                        onrendered: function (canvas) {
                            var canvasImg = canvas.toDataURL("image/jpg");
                            $('ion-nav-view').css("height", "");
                            $('body').css("overflow", 'hidden');
                            window.plugins.socialsharing.share('', 'Summary Dashboard', canvasImg, '');
                        }
                    });
                }
                $ionicLoading.show({
                    template: 'Loading...'
                });
                $scope.modal.vwVstlists = [];
                if (SlTyp == 4) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["DCRDetail_Distributors_Hunting", '["*"]', , , , , , , ]).success(function (response) {
                        $scope.modal.vwVstlists = response;
                        $scope.modal.hunt = 0;
                        $ionicLoading.hide();

                    })
                }
                else if (SlTyp == 6) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["doorToDoor", '["*"]', , , , , , , ]).success(function (response) {
                        $scope.modal.vwVstlists = response;
                        $scope.modal.door = 0;
                        $ionicLoading.hide();
                    })
                }
                else {
                    fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                            .success(function (response) {
                                if ($scope.modal.type == 1) {
                                    for (var i = response.length - 1; i >= 0; i--) {
                                        if (response[i]['orderValue'] == 0 || response[i]['orderValue'] == null)
                                            response.splice(i, 1);
                                    }
                                }
                                $scope.modal.vwVstlists = response;
                                $ionicLoading.hide();
                            }).error(function () {
                                $ionicLoading.hide();
                                Toast('No Internet Connection.');
                            });
                }
                $ionicScrollDelegate.scrollTop();
            };
            $scope.$parent.navTitle = "Day Report";
            $scope.clearData();
            $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
            $scope.getDayReports = function () {
                $ionicLoading.show({
                    template: 'Loading...'
                });
                fmcgAPIservice.getDataList('POST', 'get/DayRpt&rptDt=' + $scope.dyRptDt, [])
                        .success(function (response) {
                            $scope.dayDCRList = response;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                            Toast('No Internet Connection.');
                        });
            }
            $scope.getDayReports();
        })
        .controller('distDayReport', function ($rootScope, $ionicLoading, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.modal = $ionicModal;
            $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function (modal) {
                $scope.modal = modal;
            }, {
                animation: 'slide-in-up',
                focusFirstInput: true
            });
            $scope.Mypln.subordinate = {};
            $scope.Mypln.subordinate.selected = {};
            $scope.Mypln.stockist = {};
            $scope.Mypln.stockist.selected = {};
            if ($scope.desigCode == 'MR')
                $scope.Mypln.subordinate.selected.id = $scope.sfCode;
            $scope.ViewDetail = function (Acd, SlTyp, Adt) {
                $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.DrCap : (SlTyp == 2) ? $scope.ChmCap : (SlTyp == 3) ? $scope.StkCap : (SlTyp == 4) ? $scope.NLCap : '') + ' Visit Details For : ' + Adt;
                $scope.modal.show();

                $ionicLoading.show({
                    template: 'Loading...'
                });
                $scope.modal.vwVstlists = [];
                fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                        .success(function (response) {
                            $scope.modal.vwVstlists = response;
                            $ionicLoading.hide();
                        }).error(function () {
                            $ionicLoading.hide();
                            Toast('No Internet Connection.');
                        });
                $ionicScrollDelegate.scrollTop();
            };
            $scope.$parent.navTitle = "Day Report";
            $scope.clearData();
            $scope.$on('distReport', function () {
                $scope.getDayReports();
            });
            $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
            $scope.getDayReports = function () {
                var stockist = $scope.Mypln.stockist.selected.id;
                var baseSfCode = $scope.Mypln.subordinate.selected.id;
                if ($scope.desigCode == 'MGR') {
                    var filter = {
                        stockist: stockist,
                        sfCode: baseSfCode,
                        checkStock: "0",
                        date: $scope.dyRptDt
                    }
                }
                if ($scope.desigCode == 'MR') {
                    filter = {
                        stockist: stockist,
                        sfCode: $scope.sfCode,
                        checkStock: "0",
                        date: $scope.dyRptDt
                    }
                }
                if ($scope.desigCode == 'STOCKIST') {
                    filter = {
                        stockist: $scope.sfCode,
                        sfCode: $scope.sfCode,
                        checkStock: "1",
                        date: $scope.dyRptDt
                    }
                }

                if (filter.stockist != undefined && filter.sfCode != undefined) {
                    $ionicLoading.show({
                        template: 'Loading...'
                    });
                    fmcgAPIservice.getDataList('POST', 'getDistReport&filter=' + JSON.stringify(filter), [])
                            .success(function (response) {
                                if (response != 'null') {
                                    for (var key in response) {
                                        //if (response[key]['routeTarget'] > response[key]['orderVal'])
                                        //{
                                        //                           var Decrease = response[key]['routeTarget']- response[key]['orderVal'];
                                        //                           var perDecrease = (Decrease / (response[key]['routeTarget']))* 100;
                                        var perDecrease = ((response[key]['orderVal'] / (response[key]['routeTarget'])) * 100);
                                        response[key]['perAchieved'] = perDecrease + "%";// Decrease";
                                        //}
                                        //if (response[key]['routeTarget'] < response[key]['orderVal'])
                                        //{
                                        //                            var Increase = response[key]['orderVal']- response[key]['routeTarget'];
                                        //                           var perIncrease = (Increase / (response[key]['orderVal']))* 100;
                                        //     var perIncrease = ((response[key]['routeTarget'] / (response[key]['orderVal'])) * 100);

                                        //     response[key]['perAchieved'] = perIncrease + "% Increase"
                                        // }    
                                    }
                                    $scope.dayDCRList = response;
                                }
                                $ionicLoading.hide();
                            }).error(function () {
                                $ionicLoading.hide();
                                Toast('No Internet Connection.');
                            });
                }
            }
            $scope.getDayReports();
        })
        .controller('dcrData1', function ($rootScope, $ionicLoading, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
            $scope.$parent.navTitle = "Day Report";
            $scope.clearData();
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.success = false;
            $scope.owsuccess = false;
            $scope.owTypeData = [];
            //$scope.$parent.navTitle = "Submitted Calls";

            $scope.customers = [{
                'id': '1',
                'name': $scope.DrCap,
                'url': 'manageDoctorResult'
            }];
            var al = 1;
            if ($scope.ChmNeed != 1) {
                $scope.customers.push({
                    'id': '2',
                    'name': $scope.ChmCap,
                    'url': 'manageChemistResult'
                });
                $scope.cCI = al;
                al++;
            }
            if ($scope.StkNeed != 1) {
                $scope.customers.push({
                    'id': '3',
                    'name': $scope.StkCap,
                    'url': 'manageStockistResult'
                });
                $scope.sCI = al;
                al++;
            }
            if ($scope.UNLNeed != 1) {
                $scope.customers.push({
                    'id': '4',
                    'name': $scope.NLCap,
                    'url': 'manageStockistResult'
                });
                $scope.nCI = al;
                al++;
            }
            fmcgAPIservice.getDataList('POST', 'entry/count', []).success(function (response) {
                if (response['success']) {
                    $scope.success = true;
                    $scope.customers[0].count = response['data'][0]['doctor_count'];
                    if ($scope.ChmNeed != 1)
                        $scope.customers[$scope.cCI].count = response['data'][1]['chemist_count'];
                    if ($scope.StkNeed != 1)
                        $scope.customers[$scope.sCI].count = response['data'][2]['stockist_count'];
                    if ($scope.UNLNeed != 1)
                        $scope.customers[$scope.nCI].count = response['data'][3]['uldoctor_count'];
                    $scope.owTypeData.daywise_remarks = response['data'][4]['remarks'];
                    $scope.owTypeData.HlfDayWrk = response['data'][5]['halfdaywrk'];
                }
                else {
                    $scope.owsuccess = true;
                    $scope.owTypeData = response['data'][0];
                }
                $ionicLoading.hide();
            }).error(function () {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        })
        .controller('geoTagCtrl', function ($rootScope, $scope, fmcgLocalStorage, fmcgAPIservice, $compile, $ionicModal) {
            if ($scope.view_MR == 1) {
                $scope.geoTag.subordinate = {};
                $scope.geoTag.subordinate.selected = {};
                $scope.geoTag.subordinate.selected.id = $scope.sfCode;
                $scope.loadDatas(false, '_' + $scope.sfCode);

                $scope.subhdTop = '44px';
                $scope.CntvwTop = '88px';
            } else {
                $scope.geoTag.subordinate = undefined;
                $scope.subhdTop = '88px';
                $scope.CntvwTop = '132px';
            }
            $scope.geoTag.cluster = {};
            $scope.GeoTagDr = {};
            $scope.geoCustomers = [];
            $scope.unGeoCustomers = [];
            $scope.$on('setGeoData', function (e) {
                var data = fmcgLocalStorage.getData('doctor_master_' + $scope.geoTag.subordinate.selected.id) || [];

                $scope.geoCustomers = data.filter(function (a) {
                    return (a.lat != '' && a.town_code == $scope.geoTag.cluster.selected.id);
                });
                $scope.unGeoCustomers = data.filter(function (a) {
                    return (a.lat == '' && a.town_code == $scope.geoTag.cluster.selected.id);
                });
            });
            var map;
            var myLatlng;
            setPosFlag = false;
            var marker;

            $scope.mapLocation = function (item, m_Flag) {
                if (typeof google === 'undefined' || typeof google === undefined) {
                    $.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyCOY8BqGMFqo7Ij2n1O8b4RcL443zjoI_o&libraries=places');
                }

                $scope.modalMap.show();
                $scope.GeoTagDr = item;
                $scope.modalMap.vTyp = m_Flag;

                mapWin = $('.modal-backdrop .active');

                $("#map_canvas").closest('.scroll').addClass("fitHeight");
                $("#map_canvas").closest('.scroll').removeClass("scroll");
                $(".fitHeight").closest('.scroll-content').removeClass("ionic-scroll");
                $(".fitHeight").closest('.scroll-content').removeClass("scroll-content");
                mapWin.css("left", "0px");
                mapWin.css("top", "0px");
                mapWin.css("width", "100%");
                mapWin.css("height", "100%");

                myLatlng = null;
                tLatLng = new google.maps.LatLng(13.0460985, 80.2228998)
                if (m_Flag == 1) {
                    myLatlng = new google.maps.LatLng(item.lat, item.long);
                    setPosFlag = true;
                } else {
                    if (_currentLocation.Latitude != undefined) {
                        myLatlng = new google.maps.LatLng(_currentLocation.Latitude, _currentLocation.Longitude);
                        setPosFlag = true;
                    }
                }

                var mapOptions = {
                    center: ((myLatlng == null) ? tLatLng : myLatlng),
                    zoom: ((myLatlng == null) ? 5 : (17 - ((m_Flag == 1) ? $scope.roundNumber($scope.DisRad, 0) : 0))),
                    panControl: true,
                    zoomControl: true,
                    mapTypeControl: false,
                    scaleControl: true,
                    streetViewControl: true,
                    overviewMapControl: true,
                    rotateControl: true,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                if ($scope.mymap == undefined) {
                    var myDiv = document.getElementById("map_canvas");
                    var mymap = new google.maps.Map(myDiv, mapOptions);

                    // Create the search box and link it to the UI element.
                    var input = document.getElementById('pac-input');
                    input.value = "";
                    var searchBox = new google.maps.places.Autocomplete(input);
                    searchBox.setTypes(['geocode']);
                    mymap.addListener('bounds_changed', function () {
                        searchBox.setBounds(mymap.getBounds());
                        setTimeout(function () {
                            plcs = $('.pac-container');
                            $('.pac-container').remove();
                            plcs.appendTo(".modal-backdrop .active");
                        }, 500);
                    });

                    $scope.map = mymap;

                    searchBox.addListener('place_changed', function () {
                        var place = searchBox.getPlace();
                        if (place.geometry.viewport) {
                            mymap.fitBounds(place.geometry.viewport);
                        } else {
                            mymap.setCenter(place.geometry.location);
                            mymap.setZoom(17);
                        }
                        marker.setPosition(place.geometry.location);
                        var address = '';
                        if (place.address_components) {
                            address = [
                                (place.address_components[0] && place.address_components[0].short_name || ''),
                                (place.address_components[1] && place.address_components[1].short_name || ''),
                                (place.address_components[2] && place.address_components[2].short_name || '')
                            ].join(' ');
                        }

                        plcID = document.getElementById('plcAddr');
                        if (plcID != null)
                            plcID.innerHTML = address;
                        infowindow.open(mymap, marker);
                    });
                }

                if (marker != undefined)
                    marker.setMap(null);

                if (m_Flag == 1) {
                    marker = new google.maps.Circle({
                        center: myLatlng,
                        map: $scope.map,
                        strokeColor: '#cccccc',
                        strokeWeight: 2,
                        strokeOpacity: 0.5,
                        fillColor: '#0000df',
                        fillOpacity: 0.5,
                        radius: $scope.DisRad * 1000
                    });
                }
                marker = new google.maps.Marker({
                    map: $scope.map,
                    draggable: (m_Flag == 1) ? false : true,
                    animation: google.maps.Animation.DROP,
                    position: myLatlng
                });

                var geocoder = new google.maps.Geocoder();

                function geocodePosition(pos) {
                    $scope.GeoTagDr.lat = pos.lat();
                    $scope.GeoTagDr.long = pos.lng();
                    geocoder.geocode({
                        latLng: pos
                    },
                    function (responses) {
                        plcID = document.getElementById('plcAddr')
                        if (responses && responses.length > 0) {
                            if (plcID != null)
                                plcID.innerHTML = responses[0].formatted_address;
                            $scope.GeoTagDr.addrs = responses[0].formatted_address;
                        } else {
                            if (plcID != null)
                                plcID.innerHTML = 'Cannot determine address at this location.';
                            $scope.GeoTagDr.addrs = '';
                        }
                    });
                }
                var infowindow = new google.maps.InfoWindow({
                    content: "<br><b>Doctor :</b> " + $scope.GeoTagDr.name + "<br><b>Address :</b> <span id='plcAddr'></span><br><div style='display:" + ((m_Flag == 0) ? 'block' : 'none') + ";text-align:center;padding-top:15px;'><button class='button buttonClear  button-balanced pull-right' onclick='SvLocation()'>Tag This Place</button></div>",
                });
                if (m_Flag == 0) {
                    google.maps.event.addListener(marker, 'dragend', function () {
                        geocodePosition(marker.getPosition());
                    });

                    google.maps.event.addListener(marker, 'click', function () {
                        infowindow.open($scope.map, marker);
                        geocodePosition(marker.getPosition());
                    });
                }


                marker.setAnimation(google.maps.Animation.BOUNCE);

                if (myLatlng != null) {
                    infowindow.open($scope.map, marker);
                    geocodePosition(myLatlng);
                }
                infowindow.addListener('domready', function () {
                    SvLocation = function () {
                        fmcgAPIservice.addMAData('POST', 'dcr/save', "6", $scope.GeoTagDr).success(function (response) {
                            if (response.success)
                                Toast("GEO Tagged Successfully");
                        }).error(function () {
                            $scope.OutGEOTag = fmcgLocalStorage.getData("OutBx_GEOTag") || [];
                            $scope.OutGEOTag.push($scope.GeoTagDr);
                            localStorage.removeItem("OutBx_GEOTag");
                            fmcgLocalStorage.createData("OutBx_GEOTag", $scope.OutGEOTag);
                            Toast("No Internet Connection! GEO Tagged in Outbox");

                        });
                        var data = fmcgLocalStorage.getData('doctor_master_' + $scope.geoTag.subordinate.selected.id) || [];
                        var drRw = data.filter(function (a) {
                            return (a.id == $scope.GeoTagDr.id);
                        });
                        var $indx = data.indexOf(drRw[0]);
                        data[$indx].lat = $scope.GeoTagDr.lat;
                        data[$indx].long = $scope.GeoTagDr.long;
                        data[$indx].addrs = $scope.GeoTagDr.addrs;
                        fmcgLocalStorage.createData('doctor_master_' + $scope.geoTag.subordinate.selected.id, data);
                        $scope.$broadcast('setGeoData');

                        $scope.modalMap.hide();
                    }
                });
                marker.addListener('click', function () {

                });
            }

        })
        .filter('selectMap', function () {

            return function (input, data) {
                if (data) {
                    for (var i = 0, len = data['collection'].length; i < len; i++) {
                        if (parseInt(data['collection'][i].id) === parseInt(input)) {
                            input = data['collection'][i].name;
                            break;
                        }
                    }
                }
                return input;
            };
        }).filter('reverseGeoCode', function () {
            return function (input) {
                return input;
            };
        }).filter('getValueforID', function () {
            return function (id, collection) {
                var arrayToReturn = [];
                if (collection && id) {

                    var len;
                    try {
                        len = collection.length;
                    }
                    catch (m) {
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
        }).filter('searchF', function () {
            return function (id, collection) {
                var arrayToReturn = [];
                if (collection && id) {
                    collection = collection;
                    var len;
                    try {
                        len = collection.length;
                    }
                    catch (m) {
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
        }).filter('getValueforIDD', function () {
            return function (id, collection) {
                var arrayToReturn = [];
                if (collection && id) {
                    collection = collection.collection;

                    var len;
                    try {
                        len = collection.length;
                    }
                    catch (m) {
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
        }).directive('reverseGeocode', function () {
            return {
                restrict: 'E',
                template: '<div></div>',
                link: function (scope, element, attrs) {
                    var geocoder = new google.maps.Geocoder();
                    var results = attrs.latlng.split(":");
                    if (results.length == 2) {
                        var latlng = new google.maps.LatLng(results[0], results[1]);
                        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
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
var isReachable = function () {
    if (navigator.connection) {
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
    else {
        return navigator.onLine;
    }
}
var isComputer = function () {

    if (navigator.platform === "Win32") {
        return true;
    }
    return false;
}
var isGpsEnabled = function (PositionError) {
    if (isComputer())
        return true;
    else if (PositionError) {
        return false;
    }
    return true;
}
var Toast = function (message, type) {
    if (window.plugins) {
        window.plugins.toast.showShortCenter(message, function (a) {
            console.log('toast success: ' + a)
        }, function (b) {
            alert('toast error: ' + b)
        });
    }
    else {
        var data = {};
        data.message = message;
        data.extraClasses = 'messenger-fixed messenger-on-left messenger-on-top';
        if (type)
            data.type = "error";
        Messenger().post(data);
    }
};


function enableLight() {
    if (window.watcher) {
        navigator.geolocation.clearWatch(window.watcher);
    }
    var ele = document.querySelector(".ion-location");
    window.watcher = navigator.geolocation.watchPosition(function (position) {
        ele && ele.classList.add("available");
    },
            function (error) {
                if (ele) {
                    ele.classList.remove("available");
                } else {
                    console.log("Notifier icon not found");
                }
            }, { maximumAge: 0, timeout: 5000, enableHighAccuracy: true })
}
document.addEventListener("deviceready", function () {
    enableLight();
});
document.addEventListener("resume", function () {
    //    alert("Resume"+window.watcher);
    enableLight();
    //    if (window.watch) {
    //        navigator.geolocation.clearWatch(window.watch);
    //    }
}, false);
