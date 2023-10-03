var fmcg = angular.module('fmcg', ['ionic', 'fmcgServices', 'ngGrid', 'infinite-scroll', 'chart.js', 'flexcalendar', 'angular.filter', 'pascalprecht.translate', 'ion-affix', 'ion-sticky'])
    .config(function($stateProvider, $urlRouterProvider, $locationProvider) {
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
            .state('fmcgmenu.prvcall', {
                url: "/prvcall",
                views: {
                    'menuContent': {
                        templateUrl: "partials/CallsPreview.html",
                        controller: "callPreviewCtrl"
                    }
                }
            })
            .state('fmcgmenu.invEntry', {
                url: "/inventry",
                views: {
                    'menuContent': {
                        templateUrl: "partials/InvoiceEntry.html",
                        controller: "invEntryCtrl"
                    }
                }
            })
            .state('fmcgmenu.attView', {
                url: "/attView",
                views: {
                    'menuContent': {
                        templateUrl: "partials/AttendanceView.html",
                        controller: "attViewCtrl"
                    }
                }
            })
            .state('fmcgmenu.dySalSumm', {
                url: "/dySalSumm",
                views: {
                    'menuContent': {
                        templateUrl: "partials/DailySales.html",
                        controller: "dySalSumm"
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
            .state('fmcgmenu.TPEntry', {
                url: "/TourPlan",
                views: {
                    'menuContent': {
                        templateUrl: "partials/TourPlan.html",
                        controller: "TourPlanCtrl"
                    }
                }
            })
            .state('fmcgmenu.TourPlan', {
                url: "/TPMSelect",
                views: {
                    'menuContent': {
                        templateUrl: "partials/TPMSelect.html",
                        controller: "TPMonSelectCtrl"
                    }
                }
            })

        .state('fmcgmenu.DailyExpense', {
                url: "/DailyExpense",
                views: {
                    'menuContent': {
                        templateUrl: "partials/DailyExpense.html",
                        controller: "DailyExpenseCtrl"
                    }
                }
            })

         .state('fmcgmenu.EA', {
                url: "/EA",
                views: {
                    'menuContent': {
                        templateUrl: "partials/EA.html",
                        controller: "Eactrl"
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
            .state('fmcgmenu.BrndSummary', {
                url: "/BrndSummary",
                views: {
                    'menuContent': {
                        templateUrl: "partials/BrndSummary.html", //dcr.html
                        controller: "BrndSummary" //dcrData
                    }
                }
            })
            .state('fmcgmenu.DailyInv', {
                url: "/DailyInv",
                views: {
                    'menuContent': {
                        templateUrl: "partials/InventoryF1.html", //dcr.html
                        controller: "DailyInventory" //dcrData
                    }
                }
            })
            .state('fmcgmenu.OrderRet', {
                url: "/OrderRet",
                views: {
                    'menuContent': {
                        templateUrl: "partials/OrderReturn.html", //dcr.html
                        controller: "OrderReturn" //dcrData
                    }
                }
            })
            .state('fmcgmenu.brandsale', {
                url: "/brandsale",
                views: {
                    'menuContent': {
                        templateUrl: "partials/BrndSummaryValue.html", //dcr.html
                        controller: "BrndSummaryValue" //dcrData
                    }
                }
            })
            .state('fmcgmenu.brandlitter', {
                url: "/brandlitter",
                views: {
                    'menuContent': {
                        templateUrl: "partials/BrndSummaryLitter.html", //dcr.html
                        controller: "BrndSummaryLitter" //dcrData
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




 .state('fmcgmenu.DayCallReport', {
                url: "/DayCallReport",
                views: {
                    'menuContent': {
                        templateUrl: "partials/DayCallReport.html", //dcr.html
                        controller: "DayCallReportctrl" //dcrData
                    }
                }
            })








            .state('fmcgmenu.dayPlan', {
                url: "/dayPlan",
                views: {
                    'menuContent': {
                        templateUrl: "partials/dayPlan.html",
                        controller: "dayPlan" //dcrData1
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
            .state('fmcgmenu.ProductDisplayApprovals', {
                url: "/ProductDisplayApprovals",
                views: {
                    'menuContent': {
                        templateUrl: "partials/ProductDisplayApprovals.html",
                        controller: "ProductDisplayApprovalsctrl"
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
            .state('fmcgmenu.ViewExpense', {
                url: "/ViewExpense",
                views: {
                    'menuContent': {
                        templateUrl: "partials/ViewExpense.html",
                        controller: "ViewExpense"
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
            .state('fmcgmenu.reportingentry', {
                url: "/reportingentry",
                views: {
                    'menuContent': {
                        templateUrl: "partials/reportingentry.html",
                        controller: "reportingentryCtrl"
                    }
                }
            })
            .state('fmcgmenu.reports', {
                url: "/reports",
                views: {
                    'menuContent': {
                        templateUrl: "partials/reports.html",
                        controller: "reportsCtrl"
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

        .state('fmcgmenu.ProductdisplayLeaveapprovals', {
            url: "/ProductdisplayLeaveapprovals",
            views: {
                'menuContent': {
                    templateUrl: "partials/ProductdisplayLeaveapprovals.html",
                    controller: "ProductdisplayLeaveapprovalsctrl"
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
            .state('fmcgmenu.ExpenseApproval', {
                url: "/ExpenseApproval",
                views: {
                    'menuContent': {
                        templateUrl: "partials/ExpenseApproval.html",
                        controller: "ExpenseApproval"
                    }
                }
            })
            .state('fmcgmenu.EditSummary', {
                url: "/EditSummary",
                views: {
                    'menuContent': {
                        templateUrl: "partials/EditSummary.html",
                        controller: "EditSummaryCtrl"
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
            .state('fmcgmenu.addULDoctor', {
                url: "/adduldoctor",
                views: {
                    'menuContent': {
                        templateUrl: "partials/addData.html",
                        controller: "addULDocCtrl"
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
 .state('fmcgmenu.locationdfind', {
                url: "/locationdfind",
                views: {
                    'menuContent': {
                        templateUrl: "partials/LocationFinderList.html",
                        controller: "locationdfindctrl"
                    }
                }
            })
  .state('fmcgmenu.LocationFinder', {
                url: "/LocationFinder",
                views: {
                    'menuContent': {
                        templateUrl: "partials/LocationFinder.html",
                        controller: "LocationFinderctrl"
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

.state('fmcgmenu.SSstockUpdation', {
                url: "/SSstockUpdation",
                views: {
                    'menuContent': {
                        templateUrl: "partials/SSstockUpdation.html",
                        controller: "SSstockUpdationCtrl"
                    }
                }
            })
.state('fmcgmenu.ChangePassword', {
                url: "/ChangePassword",
                views: {
                    'menuContent': {
                        templateUrl: "partials/ChangePassword.html",
                        controller: "ChangePasswordCtrl"
                    }
                }
            })
            .state('fmcgmenu.stockUpdationPrimary', {
                url: "/stockUpdationPrimary",
                views: {
                    'menuContent': {
                        templateUrl: "partials/stockUpdationPrimary.html",
                        controller: "stockUpdationPrimaryCtrl"
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
            })
.state('fmcgmenu.transSSCurrentStocks', {
                url: "/transSSCurrentStocks",
                views: {
                    'menuContent': {
                        templateUrl: "partials/TransSSCurrentStocks.html",
                        controller: "transSSCurrentStocksCtrl"
                    }
                }
            })



        .state('fmcgmenu.EditRetail', {
            url: "/EditRetail",
            views: {
                'menuContent': {
                    templateUrl: "partials/RetailerEdited.html",
                    controller: "editRetailerCtrl"
                }
            }
        })
         .state('fmcgmenu.MissedDates', {
            url: "/MissedDates",
            views: {
                'menuContent': {
                    templateUrl: "partials/MissedDates.html",
                    controller: "MissedDatesCtrl"
                }
            }
        })
         .state('fmcgmenu.MissedOrderPage', {
            url: "/MissedOrderPage",
            views: {
                'menuContent': {
                    templateUrl: "partials/MissedOrderPage.html",
                    controller: "MissedOrderPageCtrl"
                }
            }
        })

        .state('fmcgmenu.tdStart', {
            url: "/tdStart",
            views: {
                'menuContent': {
                    templateUrl: "partials/todayStart.html",
                    controller: "tdStartCtrl"
                }
            }
        })

        .state('fmcgmenu.AddDoorToDoor', {
            url: "/AddDoorToDoor",
            views: {
                'menuContent': {
                    templateUrl: "partials/AddDoorToDoor.html",
                    controller: "AddDoorToDoorCtrl"
                }
            }
        })

  .state('fmcgmenu.GiftCard', {
            url: "/GiftCard",
            views: {
                'menuContent': {
                    templateUrl: "partials/GiftCard.html",
                    controller: "GiftCardCtrl"
                }
            }
        })



        .state('fmcgmenu.InshopActivity', {
            url: "/InshopActivity",
            views: {
                'menuContent': {
                    templateUrl: "partials/InshopActivity.html",
                    controller: "InshopActivityCtrl"
                }
            }
        })
 .state('fmcgmenu.OfferProduct', {
            url: "/OfferProduct",
            views: {
                'menuContent': {
                    templateUrl: "partials/OfferProduct.html",
                    controller: "OfferProductCtrl"
                }
            }
        })
         .state('fmcgmenu.SupplierMaster', {
            url: "/SupplierMaster",
            views: {
                'menuContent': {
                    templateUrl: "partials/SupplierMaster.html",
                    controller: "SupplierMasterCtrl"
                }
            }
        })

        .state('fmcgmenu.FieldDemoActivity', {
            url: "/FieldDemoActivity",
            views: {
                'menuContent': {
                    templateUrl: "partials/FieldDemo.html",
                    controller: "FieldDemoActivityCtrl"
                }
            }
        })


        .state('fmcgmenu.NewContact', {
            url: "/NewContact",
            views: {
                'menuContent': {
                    templateUrl: "partials/NewContact.html",
                    controller: "NewContactCtrl"
                }
            }
        })

.state('fmcgmenu.NewAI', {
            url: "/NewAI",
            views: {
                'menuContent': {
                    templateUrl: "partials/NewAI.html",
                    controller: "NewAICtrl"
                }
            }
        })

.state('fmcgmenu.PragnancyTest', {
            url: "/PragnancyTest",
            views: {
                'menuContent': {
                    templateUrl: "partials/PragnancyTest.html",
                    controller: "PragnancyTestCtrl"
                }
            }
        })


.state('fmcgmenu.DeliveryStatus', {
            url: "/DeliveryStatus",
            views: {
                'menuContent': {
                    templateUrl: "partials/DeliveryStatus.html",
                    controller: "DeliveryStatusCtrl"
                }
            }
        })
        .state('fmcgmenu.PaccsmeetingActivity', {
            url: "/PaccsmeetingActivity",
            views: {
                'menuContent': {
                    templateUrl: "partials/PaccsMeeting.html",
                    controller: "PaccsmeetingActivityCtrl"
                }
            }
        })




        .state('fmcgmenu.HomeActivityDisplay', {
            url: "/HomeActivityDisplay",
            views: {
                'menuContent': {
                    templateUrl: "partials/HomeActivityDisplay.html",
                    controller: "HomeActivityDisplayCtrl"
                }
            }
        })

        .state('fmcgmenu.InshopCheckIn', {
            url: "/InshopCheckIn",
            views: {
                'menuContent': {
                    templateUrl: "partials/InshopCheckin.html",
                    controller: "InshopCheckinCtrl"
                }
            }
        })



        .state('fmcgmenu.CollectionReport', {
            url: "/CollectionReport",
            views: {
                'menuContent': {
                    templateUrl: "partials/Todaycollectinreport.html",
                    controller: "CollectionReportctrl"
                }
            }
        })
.state('fmcgmenu.dailytotalcallsrport', {
            url: "/dailytotalcallsrport",
            views: {
                'menuContent': {
                    templateUrl: "partials/dailytotalcountstatus.html",
                    controller: "dailytotalcallsrportctrl"
                }
            }
        })








.state('fmcgmenu.TargetAndAchieve', {
            url: "/TargetAndAchieve",
            views: {
                'menuContent': {
                    templateUrl: "partials/TargetandAchieve.html",
                    controller: "TargetAndAchievectrl"
                }
            }
        })
        .state('fmcgmenu.settings', {
            url: "/settings",
            views: {
                'menuContent': {
                    templateUrl: "partials/AppSettings.html",
                    controller: "settingsCtrl"
                }
            }
        });
        $urlRouterProvider.otherwise("/sign-in");
    }).run(function($rootScope, $state, $ionicLoading, $ionicPopup) {
        $rootScope.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams) {
            var allow = ['/addNew', '/screen1', '/screen2', '/screen3', '/screen4', '/screen5']
            if ($rootScope.hasData) {
                if (allow.indexOf(toState.url) == -1) {

                    $rootScope.deleteRecord = true;
                    $rootScope.hasData = false;
                    $state.go(toState.name);

                }

            }

        });
        $rootScope.$on('$stateChangeSuccess', function(event, toState, toParams, fromState, fromParams) {
            $ionicLoading.hide();
        });
        $rootScope.dateFormat = function(format, dt) {
            if (dt == undefined)
                nDt = new Date();
            else
                nDt = new Date(dt);
            Yy = nDt.getFullYear();
            MM = nDt.getMonth() + 1;
            if (MM < 10) MM = "0" + MM;
            dd = nDt.getDate();
            if (dd < 10) dd = "0" + dd;
            hh = nDt.getHours();
            nn = "AM";
            if (format.indexOf("NN") > -1) {
                if (hh > 11) nn = "PM";
                if (hh > 12) hh = hh - 12;
            }
            if (hh < 10) hh = "0" + hh;
            mn = nDt.getMinutes();
            if (mn < 10) mn = "0" + mn;
            ss = nDt.getSeconds();
            if (ss < 10) ss = "0" + ss;

            return format.replace(/YYYY/ig, Yy).replace(/MM/ig, MM).replace(/dd/ig, dd).replace(/hh/ig, hh).replace(/mn/ig, mn).replace(/ss/ig, ss).replace(/nn/ig, nn);
        };
    })
    .controller('VacPgCtrl', ['$rootScope', '$scope', '$state', function($rootScope, $scope, $state) {
        $scope.GotoLogin = function() {
            $state.go('signin')
        }
    }])
    .controller('settingsCtrl', ['$rootScope', '$scope', '$state', 'fmcgLocalStorage', function($rootScope, $scope, $state, fmcgLocalStorage) {
        $scope.$parent.navTitle = "Settings";
        $scope.SaveSettings = function() {
            window.localStorage.setItem("LocalSettings", JSON.stringify($scope.lSettings));
            Toast("Settings Saved Successfully");
        }
        $scope.OpenDeviceList = function() {
            BTPrinter.list(function(data) {
                console.log(JSON.stringify(data));
                $scope.openModal(data, 600);
            }, function(err) {
                console.log("No Printers Found...");
                console.log(err);


            });

        }
    }])
    .controller('SignInCtrl', ['$rootScope', '$scope', '$state', '$ionicModal', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', function($rootScope, $scope, $state,$ionicModal,$location, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.appVar = "v 6.0.9"
        lpath=window.localStorage.getItem("logo");
        console.log("Logo Path : "+lpath);
        if(lpath!=null){
            document.getElementById("cLogo").src =lpath;
        }
        $scope.callback = function() {};

        $scope.process = false;
        $scope.login = true;

        $scope.error = "";
        var flag = false,
            temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        //fmcgLocalStorage.createData("events", []);
        if (userData && userData['user'] && userData['user']['valid']) {
            $scope.user = {};
            $scope.SFDispName = userData["sfName"] + ' ( ' + userData['user']['name'] + ' )';
            $scope.user['name'] = userData['user']['name'];
            $scope.user['valid'] = userData['user']['valid'];



        }

        temp = window.localStorage.getItem("LOGIN");
        if (temp != null) {

            var userDataa = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            if (userDataa == true) {

                $scope.process = true;
                $scope.login = false;

                var date2 = new Date();
                var date1 = new Date();

                date1.setTime(userData.lastLogin);
                date1.setUTCHours(0, 0, 0, 0);
                date2.setUTCHours(0, 0, 0, 0);

                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                if (flag == true && diffDays > 0) {
                    var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
                    var dat = new Date();
                    dat.setUTCHours(0, 0, 0, 0);
                    loginInfo.lastLogin = dat.getTime();
                    window.localStorage.setItem("loginInfo", JSON.stringify(loginInfo));
                    window.localStorage.removeItem("mypln");
                    window.localStorage.removeItem("OrderRetailerName");
                    $scope.Myplns=[];

                    window.localStorage.removeItem("MyDyRmks");
                    window.localStorage.removeItem("MyDyRmksQ");
                    window.localStorage.removeItem("BInvFlg");

                    $scope.process = false;
                    $scope.login = true;
                    //$state.go('signin');
                } else {
                    setTimeout(function() {
                        $state.go('fmcgmenu.home');
                    }, 1000);
                }
            }
        }

        $scope.signIn = function(user) {
            var loginInfo = {};
            $scope.process = true;
            $scope.login = false;
            $scope.error = "";
            if (__DevID != '') {
                user.DeviceRegId = localStorage.getItem('registrationId');
            }
            if (userData != null) {
                var date2 = new Date();
                var date1 = new Date();

                date1.setTime(userData.lastLogin);
                date1.setUTCHours(0, 0, 0, 0);
                date2.setUTCHours(0, 0, 0, 0);
                console.log(date2.getDate() + '<>' + date1.getDate())
                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                if (user.name === userData.user.name && user.password === userData.user.password) {
                    flag = true;
                    
                    window.localStorage.setItem("LOGIN", true);
                    $state.go('fmcgmenu.home');
                } else {
                    flag = false;
                }
            }
            if (!flag) {
                fmcgAPIservice.getPostData('POST', 'login&access=mobile', user).success(function(response) {
                    if (response.success) {
                        if (response.edit_sumry == 0) {
                            fmcgLocalStorage.createData("DCRToday", response.dcrtoday);
                        } else
                            fmcgLocalStorage.createData("DCRToday", 0);
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
                        loginData.sms = response.sms; //0= no need sms,. 1=send via whatsapp,. 2=send via sms
                        loginData.GEOTagNeed = response.GEOTagNeed;

                        loginData.DistBased = response.DistBased;
                        loginData.SupplyThrow = response.SupplyThrow;

                         loginData.RetailerNeeded=response.Needed;
                        loginData.RetailerMandatory=response.Mandatory;
                        // loginData.GEOTagNeed = 0;
                        loginData.DisRad = response.DisRad;
                        loginData.SFStat = response.SFStat;
                        loginData.CusOrder = response.CusOrder; //0=order by name. 1=walking order
                        loginData.SF_type = response.SF_type;
                        loginData.SFTPDate = response.SFTPDate;

                        loginData.AppTyp = response.AppTyp; // 0 = Pharma. 1 = FMCG
                        loginData.TBase = response.TBase; // 0 = Territory Base. 1 = Cluster Base

                        loginData.GeoChk = response.GeoChk; // 0 = Location Base. 1 = Non Location Base
                        if (loginData.GEOTagNeed == 1) loginData.GeoChk = 0;

                        loginData.prod = response.prod; //0= needed, 1=not needed
                        loginData.closing = response.closing;
                        loginData.CollectedAmountSetup = response.CollectedAmount;
                        loginData.JointworkSetup = response.jointwork;
                        loginData.JWNeed = response.JWNeed; // 0=needed,1=no need

                        loginData.NetweightVal = response.NetweightVal;
                        loginData.OrderVal = response.OrderVal;
                        loginData.recv = response.recv;

                        loginData.ChmNeed = response.ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
                        loginData.StkNeed = response.StkNeed; // 1 = Primary Order Needed. 0 = Not Needed
                        loginData.UNLNeed = response.UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed

                        loginData.DrCap = response.DrCap; // Listed Doctor Caption
                        loginData.ChmCap = response.ChmCap; // Chemist Caption
                        loginData.StkCap = response.StkCap;
                        loginData.StkRoute = response.StkRoute; //Route Caption

                        // Stockist Caption
                        loginData.NLCap = response.NLCap; // Unlisted DR. Caption

                        loginData.EDrCap = response.EDrCap; // Listed Doctor Caption
                        loginData.EChmCap = response.EChmCap; // Chemist Caption
                        loginData.EStkCap = response.EStkCap; // Stockist Caption
                        loginData.ENLCap = response.ENLCap; // Unlisted DR. Caption

                        loginData.ESHDrCap = response.ESHDrCap; // Listed Doctor Caption
                        loginData.ESHChmCap = response.ESHChmCap; // Chemist Caption
                        loginData.ESHStkCap = response.ESHStkCap; // Stockist Caption
                        loginData.ESHNLCap = response.ESHNLCap; // Unlisted DR. Caption

                        loginData.DrSmpQ = response.DrSmpQ; //Order value
                        loginData.DPNeed = response.DPNeed; // 0 = Doctor Product Neeed. 1 = Not Needed
                        loginData.DINeed = response.DINeed; // 0 = Doctor Input Neeed. 1 = Not Needed
                        loginData.CPNeed = response.CPNeed; // 0 = Chemist Product Neeed. 1 = Not Needed
                        loginData.CINeed = response.CINeed; // 0 = Chemist Input Neeed. 1 = Not Needed
                        loginData.SPNeed = response.SPNeed; // 0 = Stockist Product Neeed. 1 = Not Needed
                        loginData.SINeed = response.SINeed; // 0 = Stockist Input Neeed. 1 = Not Needed
                        loginData.NPNeed = response.NPNeed; // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
                        loginData.NINeed = response.NINeed; // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
                        loginData.HlfNeed = response.HlfNeed; // 0 = Halfday work Not Neeed. 1 >= Needed
                        loginData.template = response.template; // 0 = template Needed. 1 = Not Needed
                        loginData.VisitDist = response.VisitDist; // 0 = VisitDist Needed. 1 = Not Needed

                        loginData.DRxCap = response.DRxCap; // Listed Doctor Rx Qty Caption
                        loginData.DSmpCap = response.DSmpCap; // Listed Doctor Sample Qty Caption
                        loginData.CQCap = response.CQCap; // Chemist Qty Caption
                        loginData.SQCap = response.SQCap; // Stockist Qty Caption
                        loginData.NRxCap = response.NRxCap; // UnListed Dr. Rx Qty Caption
                        loginData.NSmpCap = response.NSmpCap; // UnListed Dr. Sample Qty Caption
                        loginData.State_Code = response.State_Code;
                        loginData.DTDNeed = response.DTDNeed;
                        loginData.InshopND = response.InshopND;
                        loginData.opbal = response.opbal;
                        loginData.clbal = response.clbal;
                        loginData.FormerCaption= response.FormerCaption;
                        loginData.Selfie= response.Selfie;
                        loginData.EventCapNd= response.EventCapNd;
                        loginData.RetailerPhotoNd=response.RetailerPhotoNd;
                        loginData.GeoTagPrimary_Nd=response.GeoTagPrimary_Nd;
                        loginData.DesigSname=response.Desig;
                        loginData.Templateremark_MD=response.Templateremark_MD;
                        loginData.changepassword=response.changepassword;
                        loginData.DeliveryStatus=response.DeliveryStatus;
                        loginData.Retailer_TP=response.Retailer_TP;
                        loginData.Price_category=response.Price_category;
                        loginData.Tp_NOf_Days=response.Tp_NOf_Days;
                        loginData.Order_Value=response.Order_Value;
                        loginData.Cl_MfgDtNeed=response.Cl_MfgDtNeed;
                        loginData.LOGIN = true;
                        user['valid'] = true;
                        loginData.user = user;
                        window.localStorage.setItem("LOGIN", JSON.stringify(loginData.LOGIN));
                        window.localStorage.setItem("loginInfo", JSON.stringify(loginData));
                        flag = true;
                        if (loginData.GeoChk == 0) {
                            window.schkGPS = loginData.GeoChk;
                            window.startGPS();
                        }
                        if (response.SFStat != undefined && response.SFStat.toString() != '0') {
                            $state.go('vacScr');
                        } else {

                           
                                $state.go('fmcgmenu.home');
                            
                           
                        }
                    } else {
                        $scope.process = false;
                        $scope.login = true;
                        $scope.error = response.msg;
                    }
                }).error(
                    function() {
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
                window.localStorage.removeItem("OrderRetailerName");
                window.localStorage.removeItem("MyDyRmks");
                window.localStorage.removeItem("MyDyRmksQ");
                window.localStorage.removeItem("BInvFlg");
            }
        };
    }])
    .controller('MainCtrl', ['$rootScope', '$scope', '$state', '$ionicModal', '$ionicScrollDelegate', '$ionicPopup', '$ionicPopover', '$location', 'fmcgLocalStorage', 'fmcgAPIservice', '$ionicSideMenuDelegate', 'geolocation', '$interval', 'notification', '$timeout', '$ionicLoading', '$filter', '$http', function($rootScope, $scope, $state, $ionicModal, $ionicScrollDelegate, $ionicPopup, $ionicPopover, $location, fmcgLocalStorage, fmcgAPIservice, $ionicSideMenuDelegate, geolocation, $interval, notification, $timeout, $ionicLoading, $filter, $http) {
        $scope.modaldataview = $ionicModal;
        $scope.modaldataview.fromTemplateUrl('partials/dataViewFinalModal.html', function(modal) {
            $scope.modaldataview = modal;
        }, {
            animation: 'slide-in-uopenModalp',
            focusFirstInput: true
        });
        _userLoginStatus = 1;
        $scope.ClikCnt = 0;

        fmcgAPIservice.getPostData('POST', 'save/ver&ver=6.0.9', []).success(function(response) {}).error(function() {});
        $scope.sendQData = function() {
            if ($scope.ClikCnt >= 3) {
                $scope.ClikCnt = 0;
                Toast("Sending Local Data To Support...");
                var draftData = window.localStorage.getItem("saveLater") || "";
                appendDS = "&sfCode=" + $scope.sfCode + "&msg=QueData";
                $http.defaults.useXDomain = true;
                return $http({
                    url: baseURL + 'error/data' + appendDS,
                    method: "POST",
                    data: "data=" + draftData,
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                }).then(function mySuccess(response) {
                    Toast("Data Sent");
                }, function myError(response) {
                    console.log("fail");
                });
            }
            $scope.ClikCnt++;
        }
        $scope.$on('uSendQData', function(evnt) {
            $scope.sendQData();
        });


        $scope.HomeLogout = function() {
            $scope.data = {};

            $scope.data.Lattitude = $scope.fmcgData.logoutLAT;
            $scope.data.Langitude = $scope.fmcgData.logoutLANG;


            $scope.attendanceView = window.localStorage.getItem("attendanceView");

            if ($scope.attendanceView != undefined && $scope.attendanceView == 1) {
                $scope.data.StartTime = 1;
            } else {
                $scope.data.StartTime = 0;
            }

            $ionicPopup.confirm({
                title: 'Today Activity',
                content: 'Are you sure you want to Logout Today?'
            }).then(function(res) {
                if (res) {

                    $ionicLoading.show({
                        template: 'Loading...'
                    });

                    fmcgAPIservice.getPostData('POST', 'get/logouttime',
                            $scope.data
                        )
                        .success(function(response) {
                            var loginData = {};
                            loginData.LOGIN = false;
                            window.localStorage.setItem("LOGIN", JSON.stringify(loginData.LOGIN));
                            $state.go("signin");

                        }).error(function() {
                            Toast("No Internet Connection! Try Again.");
                            $ionicLoading.hide();
                        });


                } else {
                    console.log('You are not sure');
                }
            });

        }

        /*$interval(checkInterval, 300000);   //1 minute 60000 millisec
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
        }*/
        if (__DevID != '') {
            app.push = PushNotification.init({
                "android": {
                    "senderID": "748265501325"
                },
            });
            app.push.on('notification', function(data) {
                if (!data.additionalData.url) {
                    $state.go('fmcgmenu.home');
                    popup(data.message);
                }

            });

        }
        $interval(checkNotification, 30000); //1 minute 60000 millisec
        function checkNotification() {
            if (__DevID != '') {
                app.push = PushNotification.init({
                    "android": {
                        "senderID": "748265501325"
                    },
                });
                app.push.on('notification', function(data) {
                    if (!data.additionalData.url) {
                        $state.go('fmcgmenu.home');
                        popup(data.message);
                    }

                });
            }
        }

        function popup(message) {
            $ionicPopup.show({
                title: 'Notification',
                content: message,
                scope: $scope,
                buttons: [{
                    text: 'Close',
                    type: 'button-assertive',
                    onTap: function(e) {
                        return true;
                    }
                }, ]
            }).then(function(res) {

            }, function(err) {

            }, function(popup) {
                // If you need to access the popup directly, do it in the notify method
                // This is also where you can programatically close the popup:
                popup.close();
            });
        }
        $scope.$on('finalSubmit', function(evnt) {
           
          
            $scope.submit();
        });

        $scope.submit = function() {

            if ($scope.JWNeed == 1 && $scope.Myplns[0] != undefined) {
                if ($scope.Myplns[0]['jontWorkSelectedList'] != undefined)
                    $scope.fmcgData.jontWorkSelectedList = $scope.Myplns[0]['jontWorkSelectedList'];
            }
            if ($scope.Myplns[0] != undefined) {
                if ($scope.Myplns[0]['dcrtype'] == "Route Wise" && $scope.fmcgData.worktype.selected.FWFlg == "F") {
                    products = 0;
                    for (i = 0; i < $scope.fmcgData.productSelectedList.length; i++) {
                        if ($scope.fmcgData.productSelectedList[i]['rx_qty'] != 0) {
                            products = 1;
                        }
                    }
                    if (products == 0 && $scope.fmcgData.customer.selected.id == "1") {
                        Toast('Select the Products....');
                        return false;
                    }
                }
            }
            whatsupval = "";
            prod = "";
            console.log($scope.fmcgData.productSelectedList);
            if ($scope.fmcgData.productSelectedList != undefined) {
                for (i = 0; i < $scope.fmcgData.productSelectedList.length; i++) {
                    prod = prod + $scope.fmcgData.productSelectedList[i]['product_Nm'] + "(" + $scope.fmcgData.productSelectedList[i]['rx_qty'] + ")" + "(" + $scope.fmcgData.productSelectedList[i]['sample_qty'] + ") ," + "\n"
                }
                whatsupval = prod + "\n Total Order Value: " + $scope.fmcgData.value

            }

           
 



            function sms() {
                if ($scope.fmcgData.doctor != undefined) {
                    mobileNumber = $scope.fmcgData.doctor.Mobile_Number;
                    if (mobileNumber != null && $scope.fmcgData.productSelectedList != undefined && $scope.sms == 2) {
                        fmcgAPIservice.sendSMS(mobileNumber, whatsupval, 'POST').success(function(response) {
                            // console.log(response);
                        });
                    }
                }
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
                /*if ($scope.reportTemplates.length > 0) {
                    if ($scope.fmcgData.rx_t == undefined && $scope.fmcgData.customer.selected.id == "1") {
                        Toast('Select Call Feedback....');
                        return false;
                    }
                }*/
                if ($scope.nonreportTemplates.length > 0) {
                    if (($scope.fmcgData.remarks == undefined || $scope.fmcgData.remarks == '') && $scope.fmcgData.customer.selected.id == "1") {
                        // Toast('Select the Template ( or ) Enter the Remarks....');
                        // return false;
                    }
                }
                var stockists = $scope.stockists;
                for (var key1 in stockists) {
                    if ($scope.fmcgData.stockist != undefined) {
                        if ($scope.fmcgData.stockist.selected != undefined && $scope.fmcgData.stockist.selected != '') {
                            if (stockists[key1]['id'] == $scope.fmcgData.stockist.selected.id) {
                                $scope.fmcgData.stockist.name = stockists[key1]['name'];
                                if ($scope.fmcgData.customer != undefined) {
                                    if (($scope.fmcgData.customer.selected.id == "3" || $scope.fmcgData.customer.selected.id == "8" ) && $scope.fmcgData.customer.selected != undefined) {
                                        $scope.fmcgData.cluster.selected.id = stockists[key1]['town_code'];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if($scope.fmcgData.customer!=undefined){
            if($scope.fmcgData.customer.selected.id == "8"){

                if($scope.subordinates!=undefined)
                     
                 $scope.hqname = $scope.subordinates.filter(function(a) {
                            return (a.id == $scope.fmcgData.subordinate.selected.id);
                        });
                 $scope.fmcgData.cluster.selected.id = $scope.hqname[0].name.replace(/\(.*\)/, '');
            //$scope.fmcgData.cluster.selected.id = $scope.hqname[0].name;
            }

            }


        /*
        if($scope.GeoChk==0 &&  ($scope.fmcgData.location==undefined ||  $scope.fmcgData.location==''))
        {
         Toast("Location not available. Please wait till get Location.");
        $scope.fmcgData.submitdis=false;
        return false;
        }
           */         /* if ($scope.photosList.length > 0)
             {
                 fmcgLocalStorage.addData("events", $scope.photosList);
             }*/
            /* if ($scope.fmcgData.location == undefined) {
                        Toast("Location not available. Please wait till get Location.");
                        return false;
                    }*/
            $scope.$emit('ClearEvents');
            if ($scope.fmcgData.arc) {
                var arc = $scope.fmcgData.arc;
                var amc = $scope.fmcgData.amc;


           


                $ionicLoading.show({
                    template: 'Call Submitting. Please wait...'
                });
                fmcgAPIservice.saveDCRData('POST', 'dcr/updateEntry&arc=' + arc + '&amc=' + amc, $scope.fmcgData, true)
                    .success(function(response) {
                        _clrData($scope);
                        Toast("Call updated SuccessFully");
                        $state.go('fmcgmenu.home');
                        $ionicLoading.hide();
                        //  if ($scope.sms == 2)
                        sms();
                        if ($scope.sms == 1 && $scope.fmcgData.productSelectedList != undefined)
                            window.plugins.socialsharing.shareViaWhatsApp(whatsupval, null, null, function() {}, function(errormsg) {
                                alert(errormsg)
                            })

                    }).error(function() {
                        _savLocal($scope, $state);
                        $state.go('fmcgmenu.home');
                        $ionicLoading.hide();
                    });

            } else {
                $ionicLoading.show({
                    template: 'Call Submitting. Please wait...'
                });


                var products = $scope.fmcgData.productSelectedList;
                var values = 0;
                for (var key in products) {
                    values = +products[key]['sample_qty'] + +values;




                }


                $scope.fmcgData.Pendinglisttt = [];
                //thiru
                //alert($scope.fmcgData.Pendinglist.length);
                if ($scope.fmcgData.Pendinglist != undefined) {


                    for (var i = 0; i < $scope.fmcgData.Pendinglist.length; i++) {
                        if (($scope.fmcgData.Pendinglist[i].billno != undefined && $scope.fmcgData.Pendinglist[i].billno > 0) || ($scope.fmcgData.Pendinglist[i].billamt != undefined && $scope.fmcgData.Pendinglist[i].billamt > 0) || ($scope.fmcgData.Pendinglist[i].BalanceAmout != undefined && $scope.fmcgData.Pendinglist[i].BalanceAmout > 0) || ($scope.fmcgData.Pendinglist[i].coll_amt != undefined && $scope.fmcgData.Pendinglist[i].coll_amt > 0)) {
                            $scope.fmcgData.Pendinglisttt.push($scope.fmcgData.Pendinglist[i]);


                        }
                    }
                }

 if($scope.fmcgData.doctor!=undefined){
                $scope.OrderRetailerNamelocal=[];
   $scope.OrderRetailerNamelocal  = fmcgLocalStorage.getData("OrderRetailerName") || [];

$scope.OrderRetailerNamelocal.push($scope.fmcgData.doctor.selected);
  fmcgLocalStorage.createData("OrderRetailerName", $scope.OrderRetailerNamelocal);


            }


                $scope.fmcgData.value = values;
                fmcgAPIservice.saveDCRData('POST', 'dcr/save', $scope.fmcgData, false).success(function(response) {
                        if (!response['success'] && response['type'] == 2) {
                            $state.go('fmcgmenu.home');
                            /*if(!$scope.$parent.fmcgData.MSDFlag==1){
                                $state.go(' fmcgmenu.MissedDates');
                                
                            }else{
                                  $state.go('fmcgmenu.home');
                            }*/
                            Toast(response['msg']);
                            _clrData($scope);

                            $ionicLoading.hide();
                            if ($scope.sms == 1 && $scope.fmcgData.productSelectedList != undefined)
                                window.plugins.socialsharing.shareViaWhatsApp(whatsupval, null, null, function() {}, function(errormsg) {
                                    alert(errormsg)
                                })

                        } else if (!response['success'] && response['type'] == 1) {
                            $scope.showConfirm = function() {
                                var confirmPopup = $ionicPopup.confirm({
                                    title: 'Warning !',
                                    template: response['msg']
                                });
                                confirmPopup.then(function(res) {
                                    if (res) {
                                        $ionicLoading.show({
                                            template: 'Call Submitting. Please wait...'
                                        });
                                        fmcgAPIservice.updateDCRData('POST', 'dcr/save&replace', response['data'], false).success(function(response) {
                                                if (!response['success'])
                                                    Toast(response['msg'], true);
                                                else {
                                                    if ($scope.edit_sumry == 0) {
                                                        fmcgLocalStorage.createData("DCRToday", 1);
                                                        $scope.DCRToday = 1;
                                                    } else {
                                                        fmcgLocalStorage.createData("DCRToday", 0);
                                                        $scope.DCRToday = 0;
                                                    }
                                                    Toast("call Updated Successfully");
                                                    if ($scope.sms == 1 && $scope.fmcgData.productSelectedList != undefined) {
                                                        window.plugins.socialsharing.shareViaWhatsApp(whatsupval, null, null, function() {}, function(errormsg) {
                                                            alert(errormsg)
                                                        })
                                                    }

                                                }
                                                if ($scope.fmcgData.customer != undefined)
                                                    cSelect = $scope.fmcgData.customer.selected.id;
                                                else
                                                    cSelect = 0
                                                flg = $scope.fmcgData.worktype.selected.FWFlg;
                                                _clrData($scope);
                                                if ($scope.Myplns[0]['dcrtype'] == "Route Wise" && flg == "F" && cSelect == "1") {
                                                    $state.go('fmcgmenu.EditSummary');
                                                } else
                                                    $state.go('fmcgmenu.home');
                                                $ionicLoading.hide();
                                            })
                                            .error(function() {
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
                        } else {
                            if (!response['success'])
                                Toast(response['msg'], true);
                            else {
                                if ($scope.edit_sumry == 0) {
                                    fmcgLocalStorage.createData("DCRToday", 1);
                                    $scope.DCRToday = 1;
                                } else {
                                    fmcgLocalStorage.createData("DCRToday", 0);
                                    $scope.DCRToday = 0;
                                }

                                $scope.clearIdividual(29, 1);
                                Toast("call Submited Successfully");
                                //sms();
                                $scope.SlTyp = $scope.fmcgData.customer.selected.id;
                                if ($scope.SlTyp != 7) {
                                   /* if ($scope.fmcgData.doctor != undefined && typeof($scope.fmcgData.doctor)!='object' ) {
                                  */  if ($scope.fmcgData.doctor != undefined && typeof($scope.fmcgData.doctor)!=Object) {
                                        if($scope.fmcgData.doctor.selected!=undefined){
                                        $scope.PrvData.id = $scope.fmcgData.doctor.selected.id;
                                        $scope.PrvData.name = $scope.fmcgData.doctor.name;
                                        $scope.PrvData.address = $scope.fmcgData.doctor.address;
                                        $scope.PrvData.Mobile_Number = $scope.fmcgData.doctor.Mobile_Number;
                                   
                                        }
                                     }
                                    if ($scope.fmcgData.stockist != undefined) {
                                        $scope.PrvData.stockist_code = $scope.fmcgData.stockist.selected.id;
                                        $scope.PrvData.stockist_name = $scope.fmcgData.stockist.name;
                                    }
                                    $scope.PrvData.Order_No = response['Order_No'];
                                    // $scope.PrvData.orderValue = $scope.fmcgData.ORvalues;
                                    $scope.PrvData.discount = $scope.fmcgData.dis;

                                    $scope.PrvData.Territory = $scope.fmcgData.cluster.name;
                                    $scope.PrvData.vtime = $scope.fmcgData.entryDate.toString().replace("GMT+0530 (India Standard Time)", "");
                                    $scope.PrvData.rateType = $scope.fmcgData.rateType;
                                   $scope.PrvData.Time_Taken=$scope.fmcgData.Checkoutdurations;
                                    if($scope.fmcgData.location!=undefined)
                                    var loc = $scope.fmcgData.location.split(':');
                                    if(loc!=undefined && loc!=""){
                                         $scope.PrvData.lat=loc[0];
                                      $scope.PrvData.long=loc[1];
                                    }
                                    
                                    $scope.PrvData.productSelectedList = [];
                                    if ($scope.fmcgData.eKey != undefined && $scope.fmcgData.eKey != "")
                                        $scope.EKEY = $scope.fmcgData.eKey;
                                    $scope.PrvData.productSelectedList = $scope.fmcgData.productSelectedList;
                                }
                                $scope.PrvData.InvFlag = 0;

                                NETAMOUNT = $scope.fmcgData.netamount;


                                $scope.PrvData.NA = $scope.fmcgData.netamount;
                                //scrTyp = $scope.fmcgData.customer.selected.id;
                                //$scope.dataview = {};
                                //$scope.dataview.draft = {};
                                //$scope.dataview.par = {};
                                //$scope.dataview.par.DPNeed = 0;
                                //$scope.dataview.par.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
                                //$scope.dataview.par.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
                                //$scope.dataview.par.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
                                //$scope.dataview.draft.customer = {};
                                //$scope.dataview.draft.customer.selected = {};
                                //$scope.dataview.draft.customer.selected.id = $scope.fmcgData.customer.selected.id;
                                //$scope.dataview.draft.doctor = $scope.fmcgData.doctor;
                                //$scope.dataview.par.DrCap = $scope.DrCap;
                                //$scope.dataview.draft.stockist = $scope.fmcgData.stockist;
                                //$scope.dataview.par.StkCap = $scope.StkCap;
                                //$scope.dataview.draft.chemist = $scope.fmcgData.chemist;
                                //   $scope.dataview.par.ChmCap = $scope.ChmCap;
                                //   $scope.dataview.draft.uldoctor = $scope.fmcgData.uldoctor;
                                //   $scope.dataview.par.NLCap = $scope.NLCap;
                                //   $scope.dataview.draft.productSelectedList = $scope.fmcgData.productSelectedList;
                                //   $scope.dataview.draft.jontWorkSelectedList = $scope.fmcgData.jontWorkSelectedList;
                                //   //$scope.modaldataview=$scope.dataview;
                                //   console.log($scope.modaldataview)
                                //   $scope.modaldataview = $ionicModal;
                                //   $scope.modaldataview.fromTemplateUrl('partials/dataViewFinalModal.html', function (modal) {
                                //       $scope.modaldataview = modal;
                                //   }, {
                                //       animation: 'slide-in-up',
                                //       focusFirstInput: true
                                //   });
                                //   $scope.modaldataview.show();
                                /*
                                    $scope.modaldataview.sendWhatsApp = function () {/*******
                                        xelem = $(event.target).closest('.scroll').find('#idOrdDetail');
                                        $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                                        $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                                        $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                                        $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                                        $('ion-nav-view').css("height", $(xelem).height() + 300);
                                        $('body').css("overflow", 'visible');
                                        $ionicScrollDelegate.scrollTop();

                                        /*var doc = new jsPDF();
                                        doc.fromHTML($(xelem).html(), 15, 15, {
                                            'width': 170
                                        });
                                        doc.save('sample-file.pdf');
                                        window.plugins.socialsharing.shareViaWhatsApp("Order Details", null, 'sample-file.pdf', function () { }, function (errormsg) { alert(errormsg) })
                                        *
                                        html2canvas($(xelem), {
                                            onrendered: function (canvas) {
                                                var canvasImg = canvas.toDataURL("image/jpg");
                                                getCanvas = canvas;
                                                $('.modal').css("height", "");
                                                $('.ion-content').css("height", "");
                                                $('.scroll').css("height", "");
                                                $('ion-nav-view').css("height", "");
                                                $('body').css("overflow", 'hidden');
                                                window.plugins.socialsharing.shareViaWhatsApp("Order Details", canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                                            }
                                        });
                                    }*/
                                if ($scope.sms == 1 && $scope.fmcgData.productSelectedList != undefined)
                                    window.plugins.socialsharing.shareViaWhatsApp(whatsupval, null, null, function() {}, function(errormsg) {
                                        alert(errormsg)
                                    })

                            }
                            if ($scope.fmcgData.customer != undefined)
                                cSelect = $scope.fmcgData.customer.selected.id;
                            else
                                cSelect = 0
                            flg = $scope.fmcgData.worktype.selected.FWFlg;
                            _clrData($scope);
                            if ($scope.Myplns[0]['dcrtype'] == "Route Wise" && flg == "F" && cSelect == "1")
                                $state.go('fmcgmenu.EditSummary');
                            else if ($scope.SlTyp != 7 && $scope.SetupCallPreview != 0 && NETAMOUNT != undefined && NETAMOUNT > 0)
                                $state.go('fmcgmenu.prvcall');
                            else
                                $state.go('fmcgmenu.home');
                            $ionicLoading.hide();
                        }
                    })
                    .error(function() {
                        if ($scope.edit_sumry == 0) {
                            fmcgLocalStorage.createData("DCRToday", 1);
                            $scope.DCRToday = 1;
                        } else {
                            fmcgLocalStorage.createData("DCRToday", 0);
                            $scope.DCRToday = 0;
                        }


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
        if (userData.desigCode.toLowerCase() == 'mr' || userData.SF_type==1){
               $scope.view_MR = 1;
        }
         
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
        $scope.JointworkSetup = userData.JointworkSetup;
        $scope.JWNeed = userData.JWNeed; //0=needed,1=no need

        $scope.DCRToday = fmcgLocalStorage.getData("DCRToday");
        $scope.desigCode = userData.desigCode;
        $scope.sms = userData.sms; //0= no need sms,. 1=send via whatsapp,. 2=send via sms
        $scope.DrSmpQ = userData.DrSmpQ;
        $scope.GEOTagNeed = userData.GEOTagNeed;

        $scope.DistBased = userData.DistBased;
        $scope.RetailerNeeded=userData.RetailerNeeded;
        $scope.RetailerMandatory=userData.RetailerMandatory;
        $scope.SupplyThrow = userData.SupplyThrow;
        $scope.DisRad = userData.DisRad;
        $scope.SFTPDate = userData.SFTPDate;
        $scope.AppTyp = userData.AppTyp; // 0 = Pharma. 1 = FMCG
        $scope.TBase = userData.TBase; // 0 = Territory Base. 1 = Cluster Base
        $scope.GeoChk = userData.GeoChk; // 0 = Location Base. 1 = Non Location Base
        $scope.edit_sumry = userData.edit_sumry; // 0 = Needed. 1 = No Need
        $scope.prod = userData.prod; //0= needed, 1=not needed
        //  $scope.GeoChk = 1;
        $scope.ChmNeed = userData.ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
        $scope.StkNeed = userData.StkNeed; // 0 = Stockist Needed. 1 = Not Needed
        $scope.UNLNeed = userData.UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed
        $scope.DrCap = userData.DrCap; // Listed Doctor Caption
        $scope.ChmCap = userData.ChmCap; // Chemist Caption
        $scope.StkCap = userData.StkCap; // Stockist Caption
        $scope.NLCap = userData.NLCap; // Unlisted DR. Caption

        $scope.EDrCap = userData.EDrCap; // Listed Doctor Caption
        $scope.EChmCap = userData.EChmCap; // Chemist Caption
        $scope.EStkCap = userData.EStkCap; // Stockist Caption
        $scope.ENLCap = userData.ENLCap; // Unlisted DR. Caption

        $scope.ESHDrCap = userData.ESHDrCap; // Listed Doctor Caption
        $scope.ESHChmCap = userData.ESHChmCap; // Chemist Caption
        $scope.ESHStkCap = userData.ESHStkCap; // Stockist Caption
        $scope.ESHNLCap = userData.ESHNLCap; // Unlisted DR. Caption

        $scope.closing = userData.closing;
        $scope.recv = userData.recv;
        $scope.template = userData.template; // 0 = template Needed. 1 = Not Needed
        $scope.VisitDist = userData.VisitDist; // 0 = VisitDist Needed. 1 = Not Needed
        $scope.NetweightVal = userData.NetweightVal;
        $scope.OrderVal = userData.OrderVal;
        $scope.DPNeed = userData.DPNeed; // 0 = Doctor Product Neeed. 1 = Not Needed
        $scope.DINeed = userData.DINeed; // 0 = Doctor Input Neeed. 1 = Not Needed
        $scope.CPNeed = userData.CPNeed; // 0 = Chemist Product Neeed. 1 = Not Needed
        $scope.CINeed = userData.CINeed; // 0 = Chemist Input Neeed. 1 = Not Needed
        $scope.SPNeed = userData.SPNeed; // 0 = Stockist Product Neeed. 1 = Not Needed
        $scope.SINeed = userData.SINeed; // 0 = Stockist Input Neeed. 1 = Not Needed
        $scope.NPNeed = userData.NPNeed; // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
        $scope.NINeed = userData.NINeed; // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
        $scope.HlfNeed = userData.HlfNeed; // 0 = Halfday work Not Neeed. 1 >= Needed

        $scope.DTDNeed = userData.DTDNeed;
        $scope.InshopND = userData.InshopND;
        $scope.DRxCap = userData.DRxCap; // Listed Doctor Rx Qty Caption
        $scope.DSmpCap = userData.DSmpCap; // Listed Doctor Sample Qty Caption
        $scope.CQCap = userData.CQCap; // Chemist Qty Caption
        $scope.SQCap = userData.SQCap; // Stockist Qty Caption
        $scope.NRxCap = userData.NRxCap; // UnListed Dr. Rx Qty Caption
        $scope.NSmpCap = userData.NSmpCap; // UnListed Dr. Sample Qty Caption
        $scope.SFStat = userData.SFStat;
        $scope.State_Code = userData.State_Code;
        $scope.SFStat = userData.SFStat;
        $scope.GEOTagNeed = userData.GEOTagNeed;
        $scope.DistBased = userData.DistBased;
        $scope.SupplyThrow = userData.SupplyThrow;
        $scope.RetailerNeeded=userData.RetailerNeeded;
         $scope.RetailerMandatory=userData.RetailerMandatory;
        $scope.SFTPDate = userData.SFTPDate;
        $scope.DisRad = userData.DisRad;
        $scope.TBase = userData.TBase; // 0 = Territory Base. 1 = Cluster Base
        $scope.GeoChk = userData.GeoChk; // 0 = Location Base. 1 = Non Location Base
        $scope.ChmNeed = userData.ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
        $scope.StkNeed = userData.StkNeed; // 0 = Stockist Needed. 1 = Not Needed
        $scope.UNLNeed = userData.UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed
        $scope.template = userData.template; // 0 = template Needed. 1 = Not Needed
        $scope.VisitDist = userData.VisitDist; // 0 = visitdist Needed. 1 = Not Needed
        $scope.DrCap = userData.DrCap; // Listed Doctor Caption
        $scope.ChmCap = userData.ChmCap; // Chemist Caption
        $scope.StkCap = userData.StkCap; // Stockist Caption
        $scope.NLCap = userData.NLCap; // Unlisted DR. Caption
        $scope.EDrCap = userData.EDrCap; // Listed Doctor Caption
        $scope.EChmCap = userData.EChmCap; // Chemist Caption
        $scope.EStkCap = userData.EStkCap; // Stockist Caption
        $scope.ENLCap = userData.ENLCap; // Unlisted DR. Caption
        $scope.ESHDrCap = userData.ESHDrCap; // Listed Doctor Caption
        $scope.ESHChmCap = userData.ESHChmCap; // Chemist Caption
        $scope.ESHStkCap = userData.ESHStkCap; // Stockist Caption
        $scope.ESHNLCap = userData.ESHNLCap; // Unlisted DR. Caption
        $scope.DrSmpQ = userData.DrSmpQ;
        $scope.CusOrder = userData.CusOrder; //0=order by name. 1=walking order
        $scope.SF_type = userData.SF_type;
        $scope.edit_sumry = userData.edit_sumry; // 0 = Needed. 1 = No Need
        $scope.prod = userData.prod; //0= needed, 1=not needed
        $scope.DPNeed = userData.DPNeed; // 0 = Doctor Product Neeed. 1 = Not Needed
        $scope.DINeed = userData.DINeed; // 0 = Doctor Input Neeed. 1 = Not Needed
        $scope.CPNeed = userData.CPNeed; // 0 = Chemist Product Neeed. 1 = Not Needed
        $scope.CINeed = userData.CINeed; // 0 = Chemist Input Neeed. 1 = Not Needed
        $scope.SPNeed = userData.SPNeed; // 0 = Stockist Product Neeed. 1 = Not Needed
        $scope.SINeed = userData.SINeed; // 0 = Stockist Input Neeed. 1 = Not Needed
        $scope.NPNeed = userData.NPNeed; // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
        $scope.NINeed = userData.NINeed; // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
        $scope.HlfNeed = userData.HlfNeed; // 0 = Halfday work Not Neeed. 1 >= Needed
        $scope.CollectedAmountSetup = userData.CollectedAmountSetup; //0 needed
        $scope.JointworkSetup = userData.JointworkSetup; //0 needed
        $scope.JWNeed = userData.JWNeed; //0=needed,1=no need
        $scope.DCRToday = fmcgLocalStorage.getData("DCRToday")
        $scope.closing = userData.closing;
        $scope.recv = userData.recv;
        $scope.NetweightVal = userData.NetweightVal;
        $scope.OrderVal = userData.OrderVal;
        $scope.CaseShCap = userData.CaseShCap;
        $scope.PiecShCap = userData.PiecShCap;

        $scope.DRxCap = userData.DRxCap; // Listed Doctor Rx Qty Caption
        $scope.DSmpCap = userData.DSmpCap; // Listed Doctor Sample Qty Caption
        $scope.CQCap = userData.CQCap; // Chemist Qty Caption
        $scope.SQCap = userData.SQCap; // Stockist Qty Caption
        $scope.NRxCap = userData.NRxCap; // UnListed Dr. Rx Qty Caption
        $scope.NSmpCap = userData.NSmpCap; // UnListed Dr. Sample Qty Caption
        $scope.State_Code = userData.State_Code;

        $scope.DyInvNeed = userData.DyInvNeed;
        $scope.OrdRetNeed = userData.OrdRetNeed;
        $scope.EdSubCalls = userData.EdSubCalls;
        $scope.RetCBNd = userData.RetCBNd;
        $scope.AgnBillClct = userData.AgnBillClct;
        $scope.OfferMode = userData.OfferMode;
        $scope.TDisc = userData.TDisc;
        $scope.RateEditable = userData.RateEditable;
        $scope.MfgDtNeed = userData.MfgDtNeed;
        $scope.InvCnvrtNd = userData.InvCnvrtNd;
        $scope.OrdPrnNd = userData.OrdPrnNd;
        $scope.CompanyName = userData.CompanyName;
        $scope.Addr1 = userData.Addr1;
        $scope.Addr2 = userData.Addr2;
        $scope.City = userData.City;
        $scope.Pincode = userData.Pincode;
        $scope.GSTN = userData.GSTN;
        $scope.PromoValND = userData.PromoValND;
        $scope.DemoGivenND = userData.DemoGivenND;
        $scope.NewContact=userData.NewContact;
        $scope.Supplier_Master=userData.Supplier_Master;
        $scope.RetailerAdditionapprovalNd=userData.RetailerAdditionapprovalNd;
        $scope.DTDNeed = userData.DTDNeed;
        $scope.InshopND = userData.InshopND;

         $scope.SrtNd = userData.SrtNd; // 0 need 1 not need For start Mainatenance

        //RateMode
         $scope.RateMode = userData.RateMode;
         $scope.Remarks=userData.Remarks;
         $scope.opbal = userData.opbal;
         $scope.clbal = userData.clbal;
         $scope.FormerCaption = userData.FormerCaption;


         $scope.MsdDate = userData.MsdDate;
         $scope.TP_Remainder_Date=userData.TP_Remainder_Date;
        $scope.TP_Mandatory_ND=userData.TP_Mandatory_ND;

       $scope.TP_ND=userData.TP_ND;

       $scope.Selfie=userData.Selfie;
       $scope.EventCapNd=userData.EventCapNd;
        $scope.RetailerPhotoNd=userData.RetailerPhotoNd;
       $scope.GeoTagPrimary_Nd=userData.GeoTagPrimary_Nd;
      $scope.Templateremark_MD=userData.Templateremark_MD;
     $scope.changepassword=userData.changepassword;
        $scope.DeliveryStatus=userData.DeliveryStatus;
        $scope.Retailer_TP=userData.Retailer_TP;
        $scope.Price_category=userData.Price_category;
        $scope.Order_Value=userData.Order_Value;
        $scope.Tp_NOf_Days=userData.Tp_NOf_Days;
        $scope.DesigSname=userData.DesigSname;
        $scope.Cl_MfgDtNeed=userData.Cl_MfgDtNeed;

        $scope.TodayTourPlan=0;
        if (userData.SFStat != undefined && userData.SFStat.toString() != '0') {
            $state.go('vacScr');
        }
        loadSetups = function() {

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
                 window.localStorage.removeItem("OrderRetailerName");
                 window.localStorage.removeItem("TodayTotalRoute");
                window.localStorage.removeItem("MyDyRmksQ");
                window.localStorage.setItem("attendanceView");
                if ($scope.view_STOCKIST == 0) {
                    $state.go('fmcgmenu.myPlan')
                    $scope.PlanChk = 1;
                }
            }


            fmcgAPIservice.getDataList('POST', 'get/setup', [])
                .success(function(response) {


                var url=LogoUrl+response[0].Logo_Name+'_logo.png';
                        console.log("downloading file: " + url);
        if (navigator.platform != "Win32") {
                var filePath = cordova.file.dataDirectory + 'img.png';          
                var fileTransfer = new FileTransfer();

                var uri = encodeURI(url);

                fileTransfer.download(
                    uri,
                    filePath,
                    function(entry) {
                        console.log("download complete: " + entry.toURL());
                        window.localStorage.setItem("logo", entry.toURL().toString());
                        //document.getElementById("cLogo").src = entry.toURL();
                        //$scope.imgLogo=entry.toURL();
                         // $scope.setting.logopath=entry.toURL();
                          //$scope.setting.logowurl=url;
                        console.log("download path: " + localStorage.getItem("logo"));
                         // window.localStorage.setItem("AppConfig", JSON.stringify($scope.setting));
                    },
                    function(error) {
                        console.log("download error source " + error.source);
                        console.log("download error target " + error.target);
                        console.log("upload error code" + error.code);
                    },
                    false,
                    {
                        headers: {
                        }
                    }
                );
     }

            if($scope.GeoChk==0){
                startGPS();
            }
                 
            
    
                    var loginData = JSON.parse(localStorage.getItem("loginInfo"));
                    loginData.SFStat = response[0].SFStat;

                    loginData.GEOTagNeed = response[0].GEOTagNeed;
                    loginData.DistBased = response[0].DistBased;
                    loginData.SupplyThrow = response[0].SupplyThrow;
                    loginData.RetailerNeeded= response[0].Needed;
                    loginData.RetailerMandatory= response[0].Mandatory;
                    loginData.DisRad = response[0].DisRad;
                    loginData.TBase = response[0].TBase; // 0 = Territory Base. 1 = Cluster Base
                    loginData.GeoChk = response[0].GeoChk; // 0 = Location Base. 1 = Non Location Base
                    if (loginData.GEOTagNeed == 1)
                        loginData.GeoChk = 0;
                    loginData.CusOrder = response[0].CusOrder; //0=order by name. 1=walking order
                    loginData.SF_type = response[0].SF_type;
                    loginData.SFTPDate = response[0].SFTPDate;
                    loginData.sms = response[0].sms; //0= no need sms,. 1=send via whatsapp,. 2=send via sms
                    loginData.DrSmpQ = response[0].DrSmpQ;
                    loginData.ChmNeed = response[0].ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
                    loginData.StkNeed = response[0].StkNeed; // 0 = Stockist Needed. 1 = Not Needed
                    loginData.UNLNeed = response[0].UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed
                    loginData.DrCap = response[0].DrCap; // Listed Doctor Caption
                    loginData.ChmCap = response[0].ChmCap; // Chemist Caption
                    loginData.StkCap = response[0].StkCap; // Stockist Caption
                    loginData.StkRoute = response[0].StkRoute;



                    loginData.NLCap = response[0].NLCap; // Unlisted DR. Caption

                    loginData.EDrCap = response[0].EDrCap; // Listed Doctor Caption
                    loginData.EChmCap = response[0].EChmCap; // Chemist Caption
                    loginData.EStkCap = response[0].EStkCap; // Stockist Caption
                    loginData.ENLCap = response[0].ENLCap; // Unlisted DR. Caption

                    loginData.ESHDrCap = response[0].ESHDrCap; // Listed Doctor Caption
                    loginData.ESHChmCap = response[0].ESHChmCap; // Chemist Caption
                    loginData.ESHStkCap = response[0].ESHStkCap; // Stockist Caption
                    loginData.ESHNLCap = response[0].ESHNLCap; // Unlisted DR. Caption

                    loginData.CollectedAmountSetup = response[0].CollectedAmount;
                    loginData.JointworkSetup = response[0].jointwork;
                    loginData.JWNeed = response[0].JWNeed; //0=needed,1=no need

                    loginData.closing = response[0].closing;
                    loginData.recv = response[0].recv;
                    loginData.template = response[0].template; // 0 = template Needed. 1 = Not Needed
                    loginData.VisitDist = response[0].VisitDist; // 0 = VISITDIST Needed. 1 = Not Needed
                    loginData.edit_sumry = response[0].edit_sumry; // 0 = Needed. 1 = No Need
                    loginData.prod = response[0].prod; //0= needed, 1=not needed
                    loginData.DPNeed = response[0].DPNeed; // 0 = Doctor Product Neeed. 1 = Not Needed
                    loginData.DINeed = response[0].DINeed; // 0 = Doctor Input Neeed. 1 = Not Needed
                    loginData.CPNeed = response[0].CPNeed; // 0 = Chemist Product Neeed. 1 = Not Needed
                    loginData.CINeed = response[0].CINeed; // 0 = Chemist Input Neeed. 1 = Not Needed
                    loginData.SPNeed = response[0].SPNeed; // 0 = Stockist Product Neeed. 1 = Not Needed
                    loginData.SINeed = response[0].SINeed; // 0 = Stockist Input Neeed. 1 = Not Needed
                    loginData.NPNeed = response[0].NPNeed; // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
                    loginData.NINeed = response[0].NINeed; // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
                    loginData.HlfNeed = (loginData.desigCode == 'MR') ? response[0].MRHlfDy : response[0].MGRHlfDy; // 0 = Halfday work Not Neeed. 1 >= Needed

                    loginData.DRxCap = response[0].DrRxQCap; // Listed Doctor Rx Qty Caption
                    loginData.DSmpCap = response[0].DrSmpQCap; // Listed Doctor Sample Qty Caption
                    loginData.CQCap = response[0].ChmQCap; // Chemist Qty Caption
                    loginData.SQCap = response[0].StkQCap; // Stockist Qty Caption
                    loginData.NRxCap = response[0].NLRxQCap; // UnListed Dr. Rx Qty Caption
                    loginData.NSmpCap = response[0].NLSmpQCap; // UnListed Dr. Sample Qty Caption
                    loginData.State_Code = response[0].State_Code;
                    loginData.NetweightVal = response[0].NetweightVal;
                    loginData.OrderVal = response[0].OrderVal;

                    loginData.CaseShCap = response[0].CaseShCap;
                    loginData.PiecShCap = response[0].PiecShCap;

                    loginData.DyInvNeed = response[0].DyInvNeed;
                    loginData.OrdRetNeed = response[0].OrdRetNeed;
                    loginData.EdSubCalls = response[0].EdSubCalls;
                    loginData.RetCBNd = response[0].RetCBNd;
                    loginData.AgnBillClct = response[0].AgnBillClct;
                    loginData.OfferMode = response[0].OfferMode;
                    loginData.TDisc = response[0].TDisc;
                    loginData.RateEditable = response[0].RateEditable;
                    loginData.MfgDtNeed = response[0].MfgDtNeed;
                    loginData.InvCnvrtNd = response[0].InvCnvrtNd;
                    loginData.OrdPrnNd = response[0].OrdPrnNd;
                    loginData.CompanyName = response[0].CompanyName;
                    loginData.Addr1 = response[0].Addr1;
                    loginData.Addr2 = response[0].Addr2;
                    loginData.City = response[0].City;
                    loginData.Pincode = response[0].Pincode;
                    loginData.GSTN = response[0].GSTN;
                    loginData.PromoValND = response[0].PromoValND;
                    loginData.DemoGivenND = response[0].DemoGivenND;
                    loginData.NewContact=response[0].NewContact;
                    loginData.Supplier_Master=response[0].Supplier_Master;
                    loginData.RetailerAdditionapprovalNd=response[0].RetailerAdditionapprovalNd;
                    loginData.DTDNeed = response[0].DTDNeed;
                    loginData.InshopND = response[0].InshopND;
                    loginData.SrtNd = response[0].SrtNd; // 0 need 1 not need For start Mainatenance
                    //RateMode
                    loginData.RateMode = response[0].RateMode;
                    loginData.BatteryStatus = response[0].BatteryStatus;
                    loginData.PhoneOrderND = response[0].PhoneOrderND;
                    loginData.SetupCallPreview = response[0].PreCall;
                    loginData.MinimumOQ = response[0].MOQ;
                    loginData.productdisplayND = response[0].productdisplayND;
                    loginData.Cl_Filter = response[0].Cl_Filter;
                    loginData.Remarks=response[0].Remarks;
                    loginData.opbal=response[0].opbal;
                    loginData.clbal=response[0].clbal;
                    loginData.FormerCaption=response[0].FormerCaption;
                    loginData.MsdDate=response[0].MsdDate;
                    loginData.TP_Remainder_Date=response[0].TP_Remainder_Date;
                    loginData.TP_Mandatory_ND=response[0].TP_Mandatory_ND;
                    loginData.TP_ND=response[0].TP_ND;
                    loginData.Selfie=response[0].Selfie;
                    loginData.EventCapNd=response[0].EventCapNd;
                    loginData.RetailerPhotoNd=response[0].RetailerPhotoNd;
                    loginData.GeoTagPrimary_Nd=response[0].GeoTagPrimary_Nd;
                    loginData.Templateremark_MD=response[0].Templateremark_MD;
                    loginData.changepassword=response[0].changepassword;
                    loginData.DeliveryStatus=response[0].DeliveryStatus;
                     loginData.Retailer_TP=response[0].Retailer_TP;
                     loginData.Price_category=response[0].Price_category;
                    loginData.Order_Value=response[0].Order_Value;
                     loginData.Tp_NOf_Days==response[0].Tp_NOf_Days;
                    loginData.DesigSname=response[0].Desig;
                    loginData.Cl_MfgDtNeed=response[0].Cl_MfgDtNeed;
                    window.localStorage.setItem("loginInfo", JSON.stringify(loginData));
                    $scope.SFStat = loginData.SFStat;
                    $scope.GEOTagNeed = loginData.GEOTagNeed;

                    $scope.DistBased = loginData.DistBased;
                    $scope.SupplyThrow = loginData.SupplyThrow;
                     $scope.RetailerNeeded= loginData.RetailerNeeded;
                    $scope.RetailerMandatory= loginData.RetailerMandatory;
                    $scope.SFTPDate = loginData.SFTPDate;
                    $scope.DisRad = loginData.DisRad;
                    $scope.TBase = loginData.TBase; // 0 = Territory Base. 1 = Cluster Base
                    $scope.GeoChk = loginData.GeoChk; // 0 = Location Base. 1 = Non Location Base
                    $scope.ChmNeed = loginData.ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
                    $scope.StkNeed = loginData.StkNeed; // 0 = Stockist Needed. 1 = Not Needed
                    $scope.UNLNeed = loginData.UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed
                    $scope.template = loginData.template; // 0 = template Needed. 1 = Not Needed
                    $scope.VisitDist = loginData.VisitDist; // 0 = visitdist Needed. 1 = Not Needed

                    $scope.DrCap = loginData.DrCap; // Listed Doctor Caption
                    $scope.ChmCap = loginData.ChmCap; // Chemist Caption
                    $scope.StkCap = loginData.StkCap;
                    $scope.StkRoute = loginData.StkRoute; //Route Caption



                    // Stockist Caption
                    $scope.NLCap = loginData.NLCap; // Unlisted DR. Caption

                    $scope.EDrCap = loginData.EDrCap; // Listed Doctor Caption
                    $scope.EChmCap = loginData.EChmCap; // Chemist Caption
                    $scope.EStkCap = loginData.EStkCap; // Stockist Caption
                    $scope.ENLCap = loginData.ENLCap; // Unlisted DR. Caption

                    $scope.ESHDrCap = loginData.ESHDrCap; // Listed Doctor Caption
                    $scope.ESHChmCap = loginData.ESHChmCap; // Chemist Caption
                    $scope.ESHStkCap = loginData.ESHStkCap; // Stockist Caption
                    $scope.ESHNLCap = loginData.ESHNLCap; // Unlisted DR. Caption

                    $scope.DrSmpQ = loginData.DrSmpQ;
                    $scope.CusOrder = loginData.CusOrder; //0=order by name. 1=walking order
                    $scope.SF_type = loginData.SF_type;
                    $scope.edit_sumry = loginData.edit_sumry; // 0 = Needed. 1 = No Need
                    $scope.prod = loginData.prod; //0= needed, 1=not needed
                    $scope.DPNeed = loginData.DPNeed; // 0 = Doctor Product Neeed. 1 = Not Needed
                    $scope.DINeed = loginData.DINeed; // 0 = Doctor Input Neeed. 1 = Not Needed
                    $scope.CPNeed = loginData.CPNeed; // 0 = Chemist Product Neeed. 1 = Not Needed
                    $scope.CINeed = loginData.CINeed; // 0 = Chemist Input Neeed. 1 = Not Needed
                    $scope.SPNeed = loginData.SPNeed; // 0 = Stockist Product Neeed. 1 = Not Needed
                    $scope.SINeed = loginData.SINeed; // 0 = Stockist Input Neeed. 1 = Not Needed
                    $scope.NPNeed = loginData.NPNeed; // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
                    $scope.NINeed = loginData.NINeed; // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
                    $scope.HlfNeed = loginData.HlfNeed; // 0 = Halfday work Not Neeed. 1 >= Needed
                    $scope.CollectedAmountSetup = loginData.CollectedAmountSetup; //0 needed
                    $scope.JointworkSetup = loginData.JointworkSetup; //0 needed
                    $scope.JWNeed = loginData.JWNeed; //0=needed,1=no need
                    $scope.DCRToday = fmcgLocalStorage.getData("DCRToday")
                    $scope.closing = loginData.closing;
                    $scope.recv = loginData.recv;
                    $scope.NetweightVal = loginData.NetweightVal;
                    $scope.OrderVal = loginData.OrderVal;
                    $scope.CaseShCap = loginData.CaseShCap;
                    $scope.PiecShCap = loginData.PiecShCap;

                    $scope.DRxCap = loginData.DRxCap; // Listed Doctor Rx Qty Caption
                    $scope.DSmpCap = loginData.DSmpCap; // Listed Doctor Sample Qty Caption
                    $scope.CQCap = loginData.CQCap; // Chemist Qty Caption
                    $scope.SQCap = loginData.SQCap; // Stockist Qty Caption
                    $scope.NRxCap = loginData.NRxCap; // UnListed Dr. Rx Qty Caption
                    $scope.NSmpCap = loginData.NSmpCap; // UnListed Dr. Sample Qty Caption
                    $scope.State_Code = loginData.State_Code;

                    $scope.DyInvNeed = loginData.DyInvNeed;
                    $scope.OrdRetNeed = loginData.OrdRetNeed;
                    $scope.EdSubCalls = loginData.EdSubCalls;
                    $scope.RetCBNd = loginData.RetCBNd;
                    $scope.AgnBillClct = loginData.AgnBillClct;
                    $scope.OfferMode = loginData.OfferMode;
                    $scope.TDisc = loginData.TDisc;
                    $scope.RateEditable = loginData.RateEditable;
                    $scope.MfgDtNeed = loginData.MfgDtNeed;
                    $scope.InvCnvrtNd = loginData.InvCnvrtNd;
                    $scope.OrdPrnNd = loginData.OrdPrnNd;
                    $scope.CompanyName = loginData.CompanyName;
                    $scope.Addr1 = loginData.Addr1;
                    $scope.Addr2 = loginData.Addr2;
                    $scope.City = loginData.City;
                    $scope.Pincode = loginData.Pincode;
                    $scope.GSTN = loginData.GSTN;
                    $scope.PromoValND = loginData.PromoValND;
                    $scope.DemoGivenND = loginData.DemoGivenND;
                    $scope.NewContact=loginData.NewContact;
                    $scope.Supplier_Master=loginData.Supplier_Master;

                    
                    $scope.DTDNeed = loginData.DTDNeed;
                    $scope.InshopND = loginData.InshopND;
                    $scope.BatteryStatus = loginData.BatteryStatus;
                    $scope.PhoneOrderND = loginData.PhoneOrderND;
                    $scope.MinimumOQ = loginData.MinimumOQ;
                    $scope.SetupCallPreview = loginData.SetupCallPreview;
                    $scope.productdisplayND = loginData.productdisplayND;
                    $scope.SrtNd = loginData.SrtNd; // 0 need 1 not need For start Mainatenance
                    $scope.Cl_Filter = loginData.Cl_Filter;
                    $scope.Remarks=loginData.Remarks;
                    //RateMode
                    $scope.RateMode = loginData.RateMode;
                    $scope.clbal= loginData.clbal;
                    $scope.FormerCaption= loginData.FormerCaption;

                    $scope.opbal= loginData.opbal;
                    $scope.MsdDate= loginData.MsdDate;
                    $scope.TP_Remainder_Date= loginData.TP_Remainder_Date;
                    $scope.TP_Mandatory_ND=loginData.TP_Mandatory_ND;
                    $scope.TP_ND=loginData.TP_ND;
                    $scope.Selfie=loginData.Selfie;
                    $scope.EventCapNd=loginData.EventCapNd;
                    $scope.RetailerPhotoNd=loginData.RetailerPhotoNd;
                    $scope.GeoTagPrimary_Nd=loginData.GeoTagPrimary_Nd;
                    $scope.Templateremark_MD=loginData.Templateremark_MD;
                     $scope.changepassword=loginData.changepassword;
                     $scope.DeliveryStatus=loginData.DeliveryStatus;
                     $scope.Retailer_TP=loginData.Retailer_TP;
                    $scope.Price_category=loginData.Price_category;
                    $scope.Tp_NOf_Days=loginData.Tp_NOf_Days;
                    $scope.Order_Value=loginData.Order_Value;
                    $scope.DesigSname=loginData.DesigSname;

                    $scope.Cl_MfgDtNeed=loginData.Cl_MfgDtNeed;





                    if (loginData.SFStat != undefined && loginData.SFStat.toString() != '0') {
                        $state.go('vacScr');
                    }
                })
            $timeout(function() {
                loadSetups();
            }, 1000 * (3600 * 2));
            $scope.$parent.fieldforceName = loginData.sfName;
            var eles = document.querySelectorAll(".fieldforce-name");
            for (var i = 0; i < eles.length; i++) {
                eles[i].innerHTML = $scope.$parent.fieldforceName;
            }
        }
        $timeout(function() {
            loadSetups();
        }, 100);

        $scope.ResetData = function() {
           if($scope.GeoChk==0){
            startGPS();
           }
            
             
            var temp = window.localStorage.getItem("loginInfo");
            var dtDtaDet = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            var dtDt = new Date();
            dtDt.setTime(dtDtaDet.lastLogin);
            if ($scope.cComputer) {
                var tDate = new Date();
                if (tDate.getDate() != dtDt.getDate()) {
                    dtDtaDet.lastLogin = tDate.getTime();
                    window.localStorage.setItem("loginInfo", JSON.stringify(dtDtaDet));
                    window.localStorage.removeItem("mypln");
                    window.localStorage.removeItem("OrderRetailerName");
                    window.localStorage.removeItem("MyDyRmks");
                    window.localStorage.removeItem("MyDyRmksQ");
                    window.localStorage.removeItem("BInvFlg");
                    window.localStorage.removeItem("AInvFlg");
                    window.localStorage.removeItem("EInvFlg");
                }
                $timeout(function() {
                    $scope.ResetData();
                }, 1000);
            } else {
                window.sangps.getDateTime(function(tDate) {
                    try {
                        if (tDate.getDate() != dtDt.getDate()) {
                            dtDtaDet.lastLogin = tDate.getTime();
                            window.localStorage.setItem("loginInfo", JSON.stringify(dtDtaDet));
                            window.localStorage.removeItem("mypln");
                            window.localStorage.removeItem("OrderRetailerName");
                            window.localStorage.removeItem("MyDyRmks");
                            window.localStorage.removeItem("MyDyRmksQ");
                            window.localStorage.removeItem("BInvFlg");
                            window.localStorage.removeItem("AInvFlg");
                            window.localStorage.removeItem("EInvFlg");
                        }
                        $timeout(function() {
                            $scope.ResetData();
                        }, 1000);
                    } catch (e) {}
                });
            }
        }

        $timeout(function() {
            $scope.ResetData();
        }, 1000);

        $ionicPopover.fromTemplateUrl('partials/AppNotifyList.html', {
            scope: $scope,
        }).then(function(popover) {
            $scope.popover = popover;
        });
  
       

           if($scope.GeoChk==0){
             startGPS();
           }
    


        // window.sangps.echoGPS(isComputer());
        $scope.demo = 'ios';
        $scope.setPlatform = function(p) {
                document.body.classList.remove('platform-ios');
                document.body.classList.remove('platform-android');
                document.body.classList.add('platform-' + p);
                $scope.demo = p;
            }
            //$scope.setPlatform($scope.demo);
        $scope.getNotifyList = function() {
            fmcgAPIservice.getDataList('POST', 'get/notify', [])
                .success(function(response) {
                    if (response.length == 0) {


                        /*$scope.ListofNotify = [{
                            "Subject": "Notofications is Not Available",
                            "CrDt": new Date(),
                            "NtfyMsg": "will send you soon",
                            "type": 2
                        }];*/
                          $scope.ListofNotify = response;
                        $scope.NotifyCount = $scope.ListofNotify.length;
                    } else {
                        $scope.ListofNotify = response;
                        $scope.NotifyCount = $scope.ListofNotify.length;
                    }




                    $timeout(function() {
                        $scope.getNotifyList();
                    }, 1000 * 1800);
                })
                .error(function(response) {
                    $timeout(function() {
                        $scope.getNotifyList();
                    }, 1000 * 1800);
                })
        }
        $timeout(function() {
            $scope.getNotifyList();
        }, 100);

        $scope.openPop = function(itm) {
            $scope.popover.hide();
            $ionicPopup.show({
                title: 'Notification',
                content: itm.NtfyMsg,
                scope: $scope,
                buttons: [{
                    text: 'Close',
                    type: 'button-assertive',
                    onTap: function(e) {
                        fmcgAPIservice.getDataList('POST', 'UPD/notify', [])
                            .success(function(response) {
                                $scope.getNotifyList();
                            })
                            .error(function(response) {})
                        return true;
                    }
                }, ]
            }).then(function(res) {

            }, function(err) {

            }, function(popup) {
                // If you need to access the popup directly, do it in the notify method
                // This is also where you can programatically close the popup:
                // popup.close();
            });
        }
        $scope.view_AddUMas = 0;
        if (userData.desigCode.toLowerCase() != 'mr') {
            $scope.view_AddUMas = 1;
            $scope.view_AddMas = 1;
        }

        $scope.loadr = new Array();
        $scope.ErrFnd = new Array();

        $scope.NotifyId='';
            setInterval(function() {
            if($scope.view_MR!=1 &&$scope.Retailer_TP==1)
            fmcgAPIservice.getDataList('POST', 'get/TPnotify', [])
                            .success(function(response) {
                                if(response.length>0 &&$scope.NotifyId!=response[0].NotifyID){

                                    $scope.NotifyId=response[0].NotifyID;
                            TPnotify(response[0].NtfyMsg);
                                }
                            })
                            .error(function(response) {})

                

        }, 50000);
    function TPnotify(MSg) {
        $ionicPopup.show({
                    title: 'Notification',
                    content:MSg,
                    scope: $scope,
                    buttons: [{
                        text: 'Close',
                        type: 'button-assertive',
                        onTap: function(e) {
                            return true;
                        }
                    }, ]
                }).then(function(res) {

                }, function(err) {

                }, function(popup) {
                    // If you need to access the popup directly, do it in the notify method
                    // This is also where you can programatically close the popup:
                    // popup.close();
                });
            }
        $scope.showFlash = function() {
            if (userData.callReport && userData.callReport.length > 0 && userData.Selfie!=1 )
                $ionicPopup.show({
                    title: 'Notification',
                    content: userData.callReport,
                    scope: $scope,
                    buttons: [{
                        text: 'Close',
                        type: 'button-assertive',
                        onTap: function(e) {
                            return true;
                        }
                    }, ]
                }).then(function(res) {

                }, function(err) {

                }, function(popup) {
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

        $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
        $scope.PlanCap = "My Day Plan";
        if ($scope.Myplns.length > 0) {
            $scope.PlanCap = "Switch Route";
        }
        $scope.LeaveTypes = fmcgLocalStorage.getData("LeaveTypes") || [];
        $scope.instrumenttypes = [{
            "id": 1,
            "name": "Cash"
        }, {
            "id": 2,
            "name": "Cheque"
        }, {
            "id": 3,
            "name": "RTGS"
        }]
        $scope.brand = fmcgLocalStorage.getData("brand") || [];
        $scope.DCRTypes = [{
            "id": "Route Wise",
            "name": "Route Wise"
        }, {
            "id": "Retail Wise",
            "name": "Retail Wise"
        }];
        $scope.Folders = fmcgLocalStorage.getData("folders") || [];
        $scope.SubFolders = fmcgLocalStorage.getData("subfolders") || [];

        $scope.mailsStaff = fmcgLocalStorage.getData("mailsSF") || [];

        $scope.myTpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
        $scope.clusters = fmcgLocalStorage.getData("town_master" + $scope.cDataID) || [];

        $scope.stockists = fmcgLocalStorage.getData("stockist_master" + $scope.cDataID) || [];
        $scope.chemists = fmcgLocalStorage.getData("chemist_master" + $scope.cDataID) || [];
        $scope.doctors = fmcgLocalStorage.getData("doctor_master" + $scope.cDataID) || [];
         $scope.SupplierMster = fmcgLocalStorage.getData("SupplierMster"+$scope.cDataID) || [];
        $scope.uldoctors = fmcgLocalStorage.getData("unlisted_doctor_master" + $scope.cDataID) || [];
        $scope.jointworks = fmcgLocalStorage.getData("salesforce_master" + $scope.cDataID) || [];

        $scope.categorys = fmcgLocalStorage.getData("Doctor_Category") || [];
        $scope.specialitys = fmcgLocalStorage.getData("Doctor_Specialty") || [];
        $scope.class_master = fmcgLocalStorage.getData("class_master") || [];
        $scope.FieldDemoCategory = fmcgLocalStorage.getData("FieldDemoCategory") || [];

        $scope.qulifications = fmcgLocalStorage.getData("Qualifications") || [];
        $scope.brands = fmcgLocalStorage.getData("product_master") || [];
        $scope.subordinates = fmcgLocalStorage.getData("subordinate_master") || [];
        $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
        $scope.ProdByCat = [];
        $scope.ProdCategory = fmcgLocalStorage.getData("category_master") || [];
        $scope.products = fmcgLocalStorage.getData("product_master") || [];
        $scope.Product_State_Rates = fmcgLocalStorage.getData("Product_State_Rates") || [];

        $scope.Product_Category_Rates_All = fmcgLocalStorage.getData("Product_Category_Rates") || [];


        $scope.gifts = fmcgLocalStorage.getData("gift_master") || [];
        $scope.allsubs = fmcgLocalStorage.getData("subordinate") || [];
        $scope.allstockists = fmcgLocalStorage.getData("stockist_master" + $scope.cDataID) || [];
        $scope.reportTemplates = fmcgLocalStorage.getData("report_template_master") || [];
        $scope.nonreportTemplates = fmcgLocalStorage.getData("nonreport_template_master") || [];
        $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
        $scope.Last_SSUpdation_Date = fmcgLocalStorage.getData("Last_SSUpdation_Date") || [];
        
        $scope.Tour_Plan = fmcgLocalStorage.getData("Tour_Plan") || [];




        $scope.lcnt = 0;

        $scope.loadDatas = function(cMod, CDId) {
            var ANR = window.localStorage.getItem("AddNewRetailer");
            if (!CDId)
                CDId = $scope.cDataID;
            $scope.myTpTwns = fmcgLocalStorage.getData("town_master" + CDId) || [];
            $scope.clusters = fmcgLocalStorage.getData("town_master" + CDId) || [];
            $scope.stockists = fmcgLocalStorage.getData("stockist_master" + CDId) || [];
            $scope.chemists = fmcgLocalStorage.getData("chemist_master" + CDId) || [];
            $scope.doctors = fmcgLocalStorage.getData("doctor_master" + CDId) || [];
            $scope.SupplierMster = fmcgLocalStorage.getData("SupplierMster"+CDId) || [];
            $scope.uldoctors = fmcgLocalStorage.getData("unlisted_doctor_master" + CDId) || [];
            $scope.jointworks = fmcgLocalStorage.getData("salesforce_master" + CDId) || [];

            $scope.categorys = fmcgLocalStorage.getData("Doctor_Category") || [];
            $scope.specialitys = fmcgLocalStorage.getData("Doctor_Specialty") || [];
            $scope.class_master = fmcgLocalStorage.getData("class_master") || [];
            $scope.FieldDemoCategory = fmcgLocalStorage.getData("FieldDemoCategory") || [];

            $scope.qulifications = fmcgLocalStorage.getData("Qualifications") || [];
            $scope.ProdCategory = fmcgLocalStorage.getData("category_master") || [];
            $scope.brands = fmcgLocalStorage.getData("product_master") || [];
            $scope.subordinates = fmcgLocalStorage.getData("subordinate_master") || [];
            $scope.allsubs = fmcgLocalStorage.getData("subordinate") || [];
            $scope.allstockists = fmcgLocalStorage.getData("stockist_master" + $scope.cDataID) || [];

            $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
            $scope.products = fmcgLocalStorage.getData("product_master") || [];
            $scope.Product_State_Rates = fmcgLocalStorage.getData("Product_State_Rates") || [];
            $scope.Product_Category_Rates_All = fmcgLocalStorage.getData("Product_Category_Rates") || [];


            $scope.gifts = fmcgLocalStorage.getData("gift_master") || [];
            $scope.reportTemplates = fmcgLocalStorage.getData("report_template_master") || [];
            $scope.nonreportTemplates = fmcgLocalStorage.getData("nonreport_template_master") || [];
            $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
            $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];

              $scope.Last_SSUpdation_Date = fmcgLocalStorage.getData("Last_SSUpdation_Date") || [];
            $scope.LeaveTypes = fmcgLocalStorage.getData("LeaveTypes") || [];
            $scope.instrumenttypes = [{
                "id": 1,
                "name": "Cash"
            }, {
                "id": 2,
                "name": "Cheque"
            }]
            $scope.brand = fmcgLocalStorage.getData("brand") || [];
            $scope.Folders = fmcgLocalStorage.getData("folders") || [];
            $scope.SubFolders = fmcgLocalStorage.getData("subfolders") || [];

            $scope.mailsStaff = fmcgLocalStorage.getData("mailsSF") || [];

            $scope.Tour_Plan = fmcgLocalStorage.getData("Tour_Plan") || [];
            $scope.PlanCap = "My Day Plan";
            if ($scope.view_STOCKIST == 0) {

                if ($scope.Myplns.length == 0) {
                    if (cMod == true) {
                        $state.go('fmcgmenu.myPlan')
                        $scope.PlanChk = 1;

                    }
                } else {
                    if (cMod == true)
                        $state.go('fmcgmenu.home');
                    $scope.PlanCap = "Switch Route";
                }
            } else
            /*
                        if(ANR!=undefined && ANR==true){
             $state.go('fmcgmenu.screen2');
                        }else{*/
                $state.go('fmcgmenu.home');
            $scope.lcnt = 0;
            $ionicLoading.hide();


        }

        $scope.clearIdividual = function(value, total, CDId, cMod) {
            if (!cMod)
                cMod = false;
            if (!CDId)
                CDId = $scope.cDataID;

            var temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            $scope.loadr[value] = 1;
            $scope.ErrFnd[value] = 0;

            ReldDta = function(aid) {
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
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwDoctor_Master_APP", '["doctor_code as id", "doctor_name as name","town_code","town_name","lat","long","addrs","ListedDr_Address1","ListedDr_Sl_No","Mobile_Number","Doc_cat_code"]', , '["isnull(Doctor_Active_flag,0)=0"]'], CDId.replace(/_/g, ''))
                        .success(function(response) {
                            window.localStorage.removeItem("doctor_master" + CDId);
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("doctor_master" + CDId, response);
                            ReldDta(0);
                        })
                        .error(function() {
                            $scope.ErrFnd[0] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(0);
                        });
                    break;
                case 1:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwChemists_Master_APP", '["chemists_code as id", "chemists_name as name","town_code","town_name"]', , '["isnull(Chemist_Status,0)=0"]'], CDId.replace(/_/g, ''))
                        .success(function(response) {
                            window.localStorage.removeItem("chemist_master" + CDId);
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("chemist_master" + CDId, response);
                            ReldDta(1);
                        })
                        .error(function() {
                            $scope.ErrFnd[1] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(1);
                        });
                    break;
                case 2:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwstockiest_Master_APP", '["distributor_code as id", "stockiest_name as name","town_code","town_name","Addr1","Addr2","City","Pincode","GSTN","lat","long","addrs","Tcode","Dis_Cat_Code"]', , '["isnull(Stockist_Status,0)=0"]'], CDId.replace(/_/g, ''))
                        .success(function(response) {
                            window.localStorage.removeItem("stockist_master" + CDId);
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("stockist_master" + CDId, response);
                            ReldDta(2);
                        })
                        .error(function() {
                            $scope.ErrFnd[2] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(2);
                        });
                    break;
                case 3:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwunlisted_doctor_master_APP", '["unlisted_doctor_code as id", "unlisted_doctor_name as name","town_code","town_name"]', , '["isnull(unlisted_activation_flag,0)=0"]'], CDId.replace(/_/g, ''))
                        .success(function(response) {
                            window.localStorage.removeItem("unlisted_doctor_master" + CDId);
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("unlisted_doctor_master" + CDId, response);
                            ReldDta(3);
                        })
                        .error(function() {
                            $scope.ErrFnd[3] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(3);
                        });
                    break;
                case 4:
                    if (cMod == true || total == 1) {
                        fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype", '["type_code as id", "Wtype as name"]', , '["isnull(Active_flag,0)=0"]', , , , 0]).success(function(response) {
                                window.localStorage.removeItem("mas_worktype");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("mas_worktype", response);
                                ReldDta(4);
                            })
                            .error(function() {
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
                        fmcgAPIservice.getDataList('POST', 'table/list', ["product_master", '["product_code as id", "product_name as name", "Product_Sl_No as pSlNo", "Product_Category cateid"]', , '["isnull(Product_DeActivation_Flag,0)=0"]', , , , 0])
                            .success(function(response) {
                                window.localStorage.removeItem("product_master");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("product_master", response);
                                ReldDta(5);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("gift_master");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("gift_master", response);
                                ReldDta(6);
                            })
                            .error(function() {
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
                        .success(function(response) {
                            window.localStorage.removeItem("salesforce_master" + CDId);
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("salesforce_master" + CDId, response);
                            ReldDta(7);
                        })
                        .error(function() {
                            $scope.ErrFnd[7] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(7);
                        });
                    break;
                case 8:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwTown_Master_APP", '["town_code as id", "town_name as name","target","min_prod","field_code","distributor_code as stockist_code"]', , '["isnull(Town_Activation_Flag,0)=0"]', , , , , , ], CDId.replace(/_/g, ''))
                        .success(function(response) {
                            window.localStorage.removeItem("town_master" + CDId);
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("town_master" + CDId, response);
                            ReldDta(8);
                        })
                        .error(function() {
                            $scope.ErrFnd[8] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(8);
                        });
                    break;
                case 9:
                    if (cMod == true || total == 1) {
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwFeedTemplate", '["id as id", "content as name"]', , '["isnull(ActFlag,0)=0"]', , , , 0])
                            .success(function(response) {
                                window.localStorage.removeItem("report_template_master");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("report_template_master", response);
                                ReldDta(9);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("nonreport_template_master");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("nonreport_template_master", response);
                                ReldDta(10);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("halfdayworks");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("halfdayworks", response);
                                ReldDta(11);
                            })
                            .error(function() {
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
                        fmcgAPIservice.getDataList('POST', 'table/list', ["vwMyDayPlan", '["worktype","FWFlg","sf_member_code as subordinateid","cluster as clusterid","ClstrName","remarks","stockist as stockistid","worked_with_code","worked_with_name","dcrtype","location","name","Sprstk"]', , ])
                            .success(function(response) {
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

                                if(response.length!=0){
                                if(response[0].clusterid!=undefined){
                                    $scope.Previous=[];
                                       $scope.Previous  = fmcgLocalStorage.getData("TodayTotalRoute") || [];
                                        $scope.Previous.push(response[0].clusterid);
                                     fmcgLocalStorage.createData("TodayTotalRoute",$scope.Previous);

                                }
                                       
                                }
                                ReldDta(12);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("Doctor_Category");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    window.localStorage.setItem("Doctor_Category", JSON.stringify(response));
                                ReldDta(13);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("Doctor_Specialty");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    window.localStorage.setItem("Doctor_Specialty", JSON.stringify(response));
                                ReldDta(14);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("subordinate_master");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("subordinate_master", response);
                                ReldDta(15);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("subordinate");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("subordinate", response);
                                ReldDta(16);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("category_master");
                                if (response.length && response.length > 0 && Array.isArray(response)) {
                                    fmcgLocalStorage.createData("category_master", response);
                                }
                                ReldDta(17);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("class_master");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("class_master", response);
                                ReldDta(18);
                            })
                            .error(function() {
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
                            .success(function(response) {
                                window.localStorage.removeItem("Qualifications");
                                if (response.length && response.length > 0 && Array.isArray(response))
                                    fmcgLocalStorage.createData("Qualifications", response);
                                ReldDta(19);
                            })
                            .error(function() {
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
                        .success(function(response) {
                            window.localStorage.removeItem("Product_State_Rates");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("Product_State_Rates", response);
                            ReldDta(20);
                        })
                        .error(function() {
                            $scope.ErrFnd[20] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(20);
                        });
                    break;
                case 21:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwLastUpdationDate", '["Last_Updation_Date"]'])
                        .success(function(response) {
                            window.localStorage.removeItem("Last_Updation_Date");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("Last_Updation_Date", response);
                            ReldDta(21);
                        })
                        .error(function() {
                            $scope.ErrFnd[21] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(21);
                        });
                    break;
                case 22:
                            var viewApp = "vwTourPlan";
                           var tDate = new Date();
                              dt = new Date(tDate);
                            $scope.entryDate = tDate;
                            $scope.CMonth = dt.getMonth() ;
                            $scope.CYear = dt.getFullYear();

                            dt = new Date(dt.setDate(1));
                            dt = new Date(dt.setDate(32));
                            $scope.NMonthh = dt.getMonth();
                            $scope.NYearr = dt.getFullYear();
                
                             if($scope.MTPEnty==undefined){
                                $scope.MTPEnty={};
                                $scope.MTPEnty.Month=$scope.NMonthh;
                                $scope.MTPEnty.Year= $scope.NYearr;
            
                                }
                            if($scope.TourPlanHq==5558){
                                CDId='_'+$scope.sfCode;
                            }
                            window.localStorage.removeItem("Tour_Plan");
                            fmcgAPIservice.getDataList('POST', 'table/list&CMonth='+ $scope.MTPEnty.Month+'&CYr='+$scope.MTPEnty.Year, [viewApp, '["date","remarks","worktype_code","worktype_name","RouteCode","RouteName","Worked_with_Code","Worked_with_Name","JointWork_Name","Retailer_Code","Retailer_Name"]'], CDId.replace(/_/g, ''))
                                .success(function(response) {
                                  
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("Tour_Plan", response);
                                        
                                    ReldDta(22);

                            if($scope.TourPlanHq==5558){
                                $scope.TourPlanHq=undefined;
                            
                                }else{
                                    $scope.$broadcast('Calldatefunction');
                                 $scope.$emit('Calldatefunction');
                                }
                                
                               
                                })
                        .error(function() {
                            $scope.ErrFnd[22] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(22);
                        });
                    break;
                case 23:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwLeaveType", '["id","name","Leave_Name"]'])
                        .success(function(response) {
                            window.localStorage.removeItem("LeaveTypes");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("LeaveTypes", response);
                            ReldDta(23);
                        })
                        .error(function() {
                            $scope.ErrFnd[23] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(23);
                        });
                    break;
                case 24:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwFolders", '["id", "name"]'])
                        .success(function(response) {
                            window.localStorage.removeItem("folders");
                            window.localStorage.removeItem("subfolders");
                            if (response.length && response.length > 0 && Array.isArray(response)) {
                                fmcgLocalStorage.createData("folders", response);
                                var subfolder = response.slice(3);
                                fmcgLocalStorage.createData("subfolders", subfolder);
                            }
                            ReldDta(24);
                        })
                        .error(function() {
                            $scope.ErrFnd[24] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(24);
                        });
                    break;
                case 25:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["GetMailSF", '[*]'])
                        .success(function(response) {
                            window.localStorage.removeItem("mailsSF");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("mailsSF", response);
                            ReldDta(25);
                        })
                        .error(function() {
                            $scope.ErrFnd[25] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(25);
                        });
                    break;

                case 26:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["Mas_Product_Brand", '[*]'])
                        .success(function(response) {
                            window.localStorage.removeItem("brand");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("brand", response);
                            ReldDta(26);
                        })
                        .error(function() {
                            $scope.ErrFnd[26] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(26);
                        });
                    break;

                case 27:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["TaxMaster", '[*]'])
                        .success(function(response) {
                            window.localStorage.removeItem("TaxDets");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("TaxDets", response);
                            ReldDta(27);
                        })
                        .error(function() {
                            $scope.ErrFnd[27] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(27);
                        });
                    break;

                case 28:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["ProdTaxDets", '[*]'])
                        .success(function(response) {
                            window.localStorage.removeItem("ProdTaxDets");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("ProdTaxDets", response);
                            ReldDta(28);
                        })
                        .error(function() {
                            $scope.ErrFnd[28] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(28);
                        });
                    break;


                case 29:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["PendingBils", '[*]'])
                        .success(function(response) {
                            window.localStorage.removeItem("viewpendingbills");
                            if (response.PendingList.length && response.PendingList.length > 0 && Array.isArray(response.PendingList))
                                fmcgLocalStorage.createData("viewpendingbills", response.PendingList);
                            ReldDta(29);


                        })
                        .error(function() {
                            $scope.ErrFnd[29] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(29);

                        });
                    break;


                case 30:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["Productcategoryrates", '[*]'])
                        .success(function(response) {
                            window.localStorage.removeItem("Product_Category_Rates");
                            if (response.ProductCategoryRates.length && response.ProductCategoryRates.length > 0 && Array.isArray(response.ProductCategoryRates))
                                fmcgLocalStorage.createData("Product_Category_Rates", response.ProductCategoryRates);
                            ReldDta(30);
                        })
                        .error(function() {
                            $scope.ErrFnd[30] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(30);

                        });
                    break;
                case 31:
                    fmcgAPIservice.getDataList('POST', 'get/Scheme', [])
                        .success(function(response) {
                            window.localStorage.removeItem("SchemeDetails");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("SchemeDetails", response);
                            ReldDta(31);
                        })
                        .error(function() {
                            $scope.ErrFnd[31] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(31);

                        });
                    break;
                     /*case 32:
                    fmcgAPIservice.getDataList('POST', 'get/Holyday&Statecode='+ $scope.MTPEnty.Month+'', [])
                        .success(function(response) {
                            window.localStorage.removeItem("Holyday");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("Holyday", response);
                            ReldDta(32);
                        })
                        .error(function() {
                            $scope.ErrFnd[32] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(32);

                        });
                    break;*/
                     case 32:
                
                        fmcgAPIservice.getDataList('POST', 'get/misseddates', []).success(function(response) {
                        
                              localStorage.removeItem("Servicemisseddate");
                            if (response.length && response.length > 0 && Array.isArray(response))
                              
                        fmcgLocalStorage.createData("Servicemisseddate", response);
                                ReldDta(32);
                                    
                          }).error(function() {
                            $scope.ErrFnd[32] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(32);
                       });
                        break;

                       case 33:
                

                          fmcgAPIservice.getDataList('POST', 'get/FieldDemoCategory', []).success(function(response) {
                        
                              localStorage.removeItem("FieldDemoCategory");
                            if (response.length && response.length > 0 && Array.isArray(response))
                              
                        fmcgLocalStorage.createData("FieldDemoCategory", response);
                                ReldDta(33);
                                    
                          }).error(function() {
                            $scope.ErrFnd[33] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(33);
                       });
                        break;

                       case 34:
                
                           fmcgAPIservice.getDataList('POST', 'get/BreedCategory', []).success(function(response) {
                        
                              localStorage.removeItem("BreedCategory");
                            if (response.length && response.length > 0 && Array.isArray(response))
                              
                            fmcgLocalStorage.createData("BreedCategory", response);
                                ReldDta(34);
                                    
                          }).error(function() {
                            $scope.ErrFnd[34] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(34);
                       });
                        break;

                         case 35:
                
                           fmcgAPIservice.getDataList('POST', 'get/SupplierMster&&SF_code='+CDId.replace(/_/g, ''), []).success(function(response) {
                        
                              localStorage.removeItem("SupplierMster"+ CDId);
                            if (response.length && response.length > 0 && Array.isArray(response))
                              
                            fmcgLocalStorage.createData("SupplierMster"+ CDId, response);
                                ReldDta(35);
                                    
                          }).error(function() {
                            $scope.ErrFnd[35] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(35);
                       });
                        break;

                        case 36:
                
                           fmcgAPIservice.getDataList('POST', 'get/GiftCard', []).success(function(response) {
                        
                            localStorage.removeItem("GiftCard");
                            if (response.length && response.length > 0 && Array.isArray(response))
                              
                            fmcgLocalStorage.createData("GiftCard", response);
                                ReldDta(36);
                                    
                          }).error(function() {
                            $scope.ErrFnd[36] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(36);
                       });
                        break;

                        case 37:
                
                           fmcgAPIservice.getDataList('POST', 'get/Mas_ClaimType', []).success(function(response) {
                        
                            localStorage.removeItem("Mas_ClaimType");
                            if (response.length && response.length > 0 && Array.isArray(response))
                              
                            fmcgLocalStorage.createData("Mas_ClaimType", response);
                                ReldDta(37);
                                    
                          }).error(function() {
                            $scope.ErrFnd[37] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(37);
                       });
                        break;
                        case 38:
                    fmcgAPIservice.getDataList('POST', 'table/list', ["SSvwlastupdationdate", '["Last_Updation_Date"]'])
                        .success(function(response) {
                            window.localStorage.removeItem("Last_SSUpdation_Date");
                            if (response.length && response.length > 0 && Array.isArray(response))
                                fmcgLocalStorage.createData("Last_SSUpdation_Date", response);
                            ReldDta(38);
                        })
                        .error(function() {
                            $scope.ErrFnd[38] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(38);
                        });
                    break;

            }

            return true;
        }
        $scope.clearAll = function(cMod, CID) {
            if (!CID)
                CID = $scope.cDataID;
            n = 39;
            $scope.lcnt = 0;
            $scope.DataLoaded = true;
            for (var i = 0; i < n; i++)
                $scope.clearIdividual(i, n, CID, cMod);
        };

        if ($scope.worktypes.length == 0) {

            $state.go('fmcgmenu.reloadMaster');
            $scope.clearAll(true);
        } else {
            if ($scope.Myplns.length == 0 && $scope.SFStat == '0') {
                $ionicLoading.show({
                    template: 'Validating Your Day Plan.<br>Please Wait...'
                });
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwMyDayPlan", '["worktype","FWFlg","sf_member_code as subordinateid","cluster as clusterid","stockist as stockistid","worked_with_code","worked_with_name","ClstrName","remarks","name"]', , ])
                    .success(function(response) {
                        if (response.length && response.length > 0 && Array.isArray(response)) {
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
                            window.localStorage.setItem("mypln", JSON.stringify(response));
                        }
                        $scope.loadDatas(true);
                        $ionicLoading.hide();
                    })
                    .error(function() {
                        $ionicLoading.hide();
                        Toast('No Internet Connection.');
                        $state.go('fmcgmenu.myPlan');
                        $scope.PlanChk = 1;
                    });
            }
        }

        $scope.onLine = isReachable();
        $scope.cComputer = isComputer();
        $scope.vibrate = function() {
            if (!$scope.cComputer)
                notification.vibrate("20");
        }

        function sendHourlyPositionToServer(coords) {}
        $scope.HOURLY_COUNTER = 0;
        /*prvLat=0;prvLng=0;
            var uploadLocaations=function() {
               // _TrackedLocation = fmcgLocalStorage.getData("TrackedLoction")||[];
                if (_TrackedLocation.length > 0) {
                    if (prvLat != _TrackedLocation[0].Latitude && prvLng != _TrackedLocation[0].Longitude) {
                         console.log("Data : " + _TrackedLocation.length + " : " + JSON.stringify(_TrackedLocation[0]));
                        data = {};
                        var temp = window.localStorage.getItem("loginInfo");
                        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
                        data.SF_code = userData.sfCode;
                        data.SF_Name =userData.sfName;
                        data.DtTm = _TrackedLocation[0].Time;
                        data.Lat = _TrackedLocation[0].Latitude;
                        data.Lon = _TrackedLocation[0].Longitude;
                        data.Auc = _TrackedLocation[0].Accuracy;
                        data.deg = _TrackedLocation[0].Bearing;
                        data.DvcID = __DevID;
                        prvLat = _TrackedLocation[0].Latitude;
                        prvLng = _TrackedLocation[0].Longitude;
                        fmcgAPIservice.addMAData('POST', 'dcr/save', '7', data)
                            .success(function (response) {
                                if (response['success']) {
                                   // _TrackedLocation=fmcgLocalStorage.getData("TrackedLoction")||[];
                                    _TrackedLocation.shift();
                                    fmcgLocalStorage.createData("TrackedLoction", _TrackedLocation);
                                }
                                $timeout(uploadLocaations, 2000);
                            })
                            .error(function (response) {
                                console.log("Error : " + JSON.stringify(response));
                                $timeout(uploadLocaations, 2000);
                            });
                    }
                    else {
                       // _TrackedLocation = fmcgLocalStorage.getData("TrackedLoction") || [];
                        _TrackedLocation.shift();
                        fmcgLocalStorage.createData("TrackedLoction", _TrackedLocation);
                        $timeout(uploadLocaations, 2000);
                    }
                }
                else
                {
                    $timeout(uploadLocaations, 20000);
                }
            }
            $timeout(uploadLocaations, 200);*/


        setInterval(function() {

            if ($scope.cComputer) {
                /*if ($scope.BatteryStatus != undefined && $scope.BatteryStatus == 1) {
                    navigator.getBattery().then(function(battery) {
                        var baterylevel = battery.level;
                        fmcgAPIservice.addMAData('POST', 'dcr/save', '38', baterylevel)
                            .success(function(response) {
                                if (response['success']) {

                                } else {

                                }

                            })
                            .error(function() {

                            });



                    });
                }*/
            } else {

                if ($scope.BatteryStatus != undefined && $scope.BatteryStatus == 1) {
                    navigator.getBattery().then(function(battery) {
                        var baterylevel = battery.level;
                        fmcgAPIservice.addMAData('POST', 'dcr/save', '38', baterylevel)
                            .success(function(response) {
                                if (response['success']) {

                                } else {

                                }

                            })
                            .error(function() {

                            });

                        console.log("EVERY 5 MNTS" + battery.level);

                    });
                }



            }
        }, 30000);
        $scope.HOURLY_COUNTER = 0;
        setInterval(function() {
            $scope.onLine = isReachable();
            $scope.cComputer = isComputer();
            $scope.HOURLY_COUNTER++;
            if ($scope.cComputer) {

                navigator.geolocation.getCurrentPosition(function(position) {
                    if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                        $scope.fmcgData.location = position.coords.latitude + ":" + position.coords.longitude;
                        $scope.fmcgData.logoutLAT = position.coords.latitude;
                        $scope.fmcgData.logoutLANG = position.coords.longitude;
                    }
                    $scope.fmcgData.hasGps = isGpsEnabled();
                });
            } else {

                ele = document.querySelector(".ion-location");
                if (ele) ele.classList.remove("available");
                $scope.fmcgData.hasGps = isGpsEnabled(true);
                if ($scope.GeoChk == 0) {
                    if (_currentLocation.Latitude != undefined) {
                        var dt = new Date();
                        var dt1 = new Date(_currentLocation.Time + ".000");
                        var difference = dt.getTime() - dt1.getTime(); // This will give difference in milliseconds
                        var resultInMinutes = Math.round(difference / 60000);
                        if (resultInMinutes > 10) {
                            _currentLocation = {};
                            return false;
                        }
                    } else {
                        return false;
                    }
                }
                if (_currentLocation.Latitude != undefined) {
                    ele = document.querySelector(".ion-location");
                    if (ele)
                        ele.classList.add("available");
                    $scope.fmcgData.hasGps = isGpsEnabled();
                    if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                        $scope.fmcgData.geoaddress = _currentLocation.address;
                        $scope.fmcgData.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;

                        $scope.fmcgData.logoutLAT = _currentLocation.Latitude;
                        $scope.fmcgData.logoutLANG = _currentLocation.Longitude;
                    }
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
        $scope.ProfilephotosList=[];
        $scope.RetailerphotosList=[];
        $ionicModal.fromTemplateUrl('partials/FindrouteMapView.html', {
            id: '6',
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function(modal) {
            $scope.FindRoutemodalMap = modal;
        });
        $ionicModal.fromTemplateUrl('partials/mapView.html', {
            id: '5',
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function(modal) {
            $scope.modalMap = modal;
        });
 
        $ionicModal.fromTemplateUrl('partials/openImgLst.html', {
            id: '4',
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function(modal) {
            $scope.modalPhotos = modal;
        });
        $ionicModal.fromTemplateUrl('partials/mnuOptions.html', {
            id: '3',
            scope: $scope,
            animation: 'slide-in-down'
        }).then(function(modal) {
            $scope.modalMnu = modal;
        });

        $scope.openOptMenu = function() {
            /*$scope.modalMnu.show();
            //$scope.pictureSource =;
            $scope.destinationType = Camera.DestinationType.FILE_URI;*/
            $scope.modalPhotos.show();
            if ($scope.photosList.length < 1) {
                $scope.openCamera()
            }
        }

        $scope.$on('ClearEvents', function() {
            $scope.photosList = [];
        })
        $scope.openCamera = function(source) {
            //$scope.modalPhotos.show();
            navigator.camera.getPicture(function(imgSrc) {
                imgLst = {};
                dt = new Date();
                imgLst.id = dt.getTime();
                imgLst.imgData = imgSrc;
                imgLst.uProgress = 0;
                imgLst.progressVisible = 1;
                imgLst.title = "";
                imgLst.remarks = "";
                $scope.photosList.push(imgLst);
                $timeout(function() {
                    $scope.uploadPhoto(imgLst.id, imgLst.imgData);
                }, 200);
            }, function(msg) {

            }, {
                sourceType: 1,
                quality: 60,
                destinationType: $scope.destinationType,
                sourceType: source
            });
        }


        $scope.openCameraProfile = function(source) {
            //$scope.modalPhotos.show();
            navigator.camera.getPicture(function(imgSrc) {
                imgLst = {};
                dt = new Date();
                imgLst.id = dt.getTime();
                imgLst.imgData = imgSrc;
                imgLst.uProgress = 0;
                imgLst.progressVisible = 1;
                imgLst.title = "";
                imgLst.remarks = "";
                $scope.ProfilephotosList.push(imgLst);
                $timeout(function() {
                    $scope.uploadPhotoprofile(imgLst.id, imgLst.imgData);
                }, 200);
            }, function(msg) {
                $scope.openCameraProfile();
            }, {
                sourceType: 1,
                quality: 60,
                destinationType: $scope.destinationType,
                sourceType: source
            });
        }


    $scope.openCameraRetailer = function(source) {
            //$scope.modalPhotos.show();
            if($scope.RetailerphotosList!=undefined && $scope.RetailerphotosList.length>0)
            $scope.RetailerphotosList=[];
            navigator.camera.getPicture(function(imgSrc) {
                imgLst = {};
                dt = new Date();
                imgLst.id = dt.getTime();
                imgLst.imgData = imgSrc;
                imgLst.uProgress = 0;
                imgLst.progressVisible = 1;
                imgLst.title = "";
                imgLst.remarks = "";
                $scope.RetailerphotosList.push(imgLst);
                $timeout(function() {
                    $scope.uploadPhotoretailer(imgLst.id, imgLst.imgData);
                }, 200);
            }, function(msg) {
               
            }, {
                sourceType: 1,
                quality: 60,
                destinationType: $scope.destinationType,
                sourceType: source
            });
        }

        $scope.updateProgress = function(indx, value) {
            $scope.photosList[indx]["uProgress"] = value.toFixed(0);
        }

        $scope.updateprofileProgress = function(indx, value) {
            $scope.ProfilephotosList[indx]["uProgress"] = value.toFixed(0);
        }

         $scope.updateretailerProgress = function(indx, value) {
            $scope.RetailerphotosList[indx]["uProgress"] = value.toFixed(0);
        }
        $scope.uploadPhoto = function(imgId, imgData) {
            var imgUrl = imgData;
            item = $scope.photosList.filter(function(a) {
                return (a.id == imgId);
            });
            indx = $scope.photosList.indexOf(item[0]);
            var options = new FileUploadOptions();
            options.fileKey = "imgfile";
            options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
            options.mimeType = "image/jpeg";
            options.chunkedMode = false;
            var uplUrl = baseURL + 'imgupload&sf_code=' + $scope.sfCode;
            var ft = new FileTransfer();
            ft.onprogress = function(progress) {
                $timeout(function() {
                    $scope.updateProgress(indx, ((progress.loaded / progress.total) * 100))
                }, 200);
            }
            ft.upload(imgUrl, uplUrl,
                function(response) {
                    $scope.photosList[indx]["progressVisible"] = 0;
                    console.log("Photo Uploaded....")
                },
                function() {}, options);
        }


            $scope.uploadPhotoprofile = function(imgId, imgData) {
            var imgUrl = imgData;
            item = $scope.ProfilephotosList.filter(function(a) {
                return (a.id == imgId);
            });
            indx = $scope.ProfilephotosList.indexOf(item[0]);
            var options = new FileUploadOptions();
            options.fileKey = "imgfile";
            options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
            options.mimeType = "image/jpeg";
            options.chunkedMode = false;
            var uplUrl = baseURL + 'imgupload&sf_code=' + $scope.sfCode;
            var ft = new FileTransfer();
            ft.onprogress = function(progress) {
                $timeout(function() {
                    $scope.updateprofileProgress(indx, ((progress.loaded / progress.total) * 100))
                }, 200);
            }
            ft.upload(imgUrl, uplUrl,
                function(response) {
                    $scope.ProfilephotosList[indx]["progressVisible"] = 0;
                    console.log("Photo Uploaded....")
                },
                function() {}, options);
        }


        $scope.uploadPhotoretailer = function(imgId, imgData) {

            var imgUrl = imgData;
            item = $scope.RetailerphotosList.filter(function(a) {
                return (a.id == imgId);
            });
            indx = $scope.RetailerphotosList.indexOf(item[0]);
            var options = new FileUploadOptions();
            options.fileKey = "imgfile";
            options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
            options.mimeType = "image/jpeg";
            options.chunkedMode = false;
            var uplUrl = baseURL + 'imgupload&sf_code=' + $scope.sfCode;
            var ft = new FileTransfer();
            ft.onprogress = function(progress) {
                $timeout(function() {
                    $scope.updateretailerProgress(indx, ((progress.loaded / progress.total) * 100))
                }, 200);
            }
            ft.upload(imgUrl, uplUrl,
                function(response) {
                    $scope.RetailerphotosList[indx]["progressVisible"] = 0;
                     $scope.fmcgData.rp=$scope.RetailerphotosList[0].imgData;
                    console.log("Photo Uploaded...."+$scope.RetailerphotosList.length);
                   
                },
                function() {}, options);
        }

        $scope.setEvntCap = function() {
            $scope.fmcgData.photosList = angular.copy($scope.photosList);
            $scope.RCPA.photosList = angular.copy($scope.photosList);
            $scope.modalMnu.hide();
        }
        $scope.deletePhoto = function(item) {
            $ionicPopup.confirm({
                title: 'Call Delete',
                content: 'Are you sure you want to Delete?'
            }).then(function(res) {
                if (res) {
                    $scope.photosList.splice($scope.photosList.indexOf(item), 1);
                    Toast("Deleted Successfully...");
                } else {
                    console.log('You are not sure');
                }
            });
        };
        $ionicModal.fromTemplateUrl('partials/selectModalProduct.html', {
            id: '1',
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function(modal) {
            $scope.modalProd = modal;
        });
        $ionicModal.fromTemplateUrl('partials/selectModalStaffs.html', {
            id: '1',
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function(modal) {
            $scope.modalStaffs = modal;
        });
        $ionicModal.fromTemplateUrl('partials/selectModal.html', {
            id: '2',
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function(modal) {
            $scope.modal = modal;
        });

        $scope.prdCloseMenu = function() {
            $('ion-side-menu-content').css(ionic.CSS.TRANSFORM, 'translate3d(0px, 0px, 0px)');
            if ($('body').hasClass('menu-open'))
                $('body').removeClass('menu-open');
        }
        $scope.filterProd = function(item) {
            $scope.ProdByCat = {};
            $scope.ShowMsg = true;
            if (item.id == -1) {
                $scope.ProdByCat = $scope.products.filter(function(a) {
                    return (a.Product_Type_Code == "P");
                });
            } else {
                $scope.ProdByCat = $scope.products.filter(function(a) {
                    return (a.cateid == item.id);
                });
            }
            for (di = 0; di < $scope.ProdByCat.length; di++) {
                itm = $scope.selectedProduct.filter(function(a) {
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

        $scope.roundNumber = function(number, precision) {
            precision = Math.abs(parseInt(precision)) || 0;
            var multiplier = Math.pow(10, precision);
            return (Math.round(number * multiplier) / multiplier);
        }
        $scope.cal_Distns = function(lat1, lon1, lat2, lon2, unit) {
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

        $scope.openMailStaffs = function(sdata, choice, repId) {
            $scope.sData = sdata;
            $scope.selectionType = choice;
            if (choice == 201)
                data = $scope.fmcgData.staffSelectedList || [];
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
                itm = data.filter(function(a) {
                    return (a.id == $scope.sData[di].id);
                });
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
        $scope.openMProduct = function(sdata, choice, repId) {
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


            }

            $scope.selectedProduct = data
            $scope.prdCloseMenu();
            for (di = 0; di < $scope.ProdByCat.length; di++) {
                itm = data.filter(function(a) {
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
        $scope.openModal = function(sdata, choice, repId) {
            $scope.prdCloseMenu();
            $scope.modal.show();
            $scope.FilteredData = {};
            tSdta = sdata;


            $scope.OrderRetailerName = fmcgLocalStorage.getData("OrderRetailerName") || [];

            if(choice==5555){
                $scope.msdremarksid=repId;
            }
            if(repId==5558){
                 $scope.TourPlanHq=repId;
            }
            if(choice==5556){
            $scope.msdremarksid=repId;
            }
            
            showmsg_t = false;
            if (typeof(sdata) == 'string') {
                if ((",town_master,stockist_master,chemist_master,doctor_master,unlisted_doctor_master,salesforce_master,MissedDates,SupplierMster,").indexOf(',' + sdata + ',') > -1)
                    sdata = sdata + '_' + repId; //((',102,103,'.indexOf(',' + choice + ',') > -1) ? $scope.cMTPDId : $scope.cDataID);

                data = fmcgLocalStorage.getData(sdata) || [];
                if (tSdta == 'doctor_master') {
                    $scope.OfflineRetailers = fmcgLocalStorage.getData("Offline_Retailer" + repId) || [];
                    for ($ili = 0; $ili < $scope.OfflineRetailers.length; $ili++) {
                        console.log($scope.OfflineRetailers);
                        data.push($scope.OfflineRetailers[$ili]);
                        console.log(data);
                    }
                }

                if(choice==90){
                    var data={};
                     var val = fmcgLocalStorage.getData('doctor_master_' +$scope.sfCode) || [];

                    if(repId.length>0){
                      for (var key in repId) {
                    
                     filterretailer= val.filter(function(a) {
                        return (a.town_code == repId[key].jointwork);


                    });
                    
                    if(key!=0){
                        data=data.concat(filterretailer);
                    }else{
                    data=filterretailer;
                    }
                }
            }

                }
                if (tSdta == 'town_master' && choice != 32 && choice != 22) {
                    $scope.fmcgData.doctor = {};

                    if ($scope.view_MR == 1 || $scope.view_MR == 0) {

                        if ((choice == 102 && $scope.DistBased == 1) || choice==506 || (choice == 89 && $scope.DistBased == 1)   ) {
                          //  console.log('(,,).indexOf(,' + $scope.Mypln.stockist.selected.id + ',)');

                            if(choice==506){

                            if($scope.DistBased==1){
                                data1 = data;            
                            

                                 data=data1;
                            }


                            }else{
                                data1 = data.filter(function(a) {
                                return ((',' + a.stockist_code + ',').indexOf(',' + $scope.Mypln.stockist.selected.id + ',') > -1);
                            });
                            data = data1;
                            }

                        } else {
                            if (($scope.SF_type !== 2 || $scope.SF_type == 1) && choice == 2 && $scope.DistBased == 1) {
                                //  $scope.fmcgData.cluster = {};
                                console.log('(,,).indexOf(,' + $scope.Mypln.stockist.selected.id + ',)');
                                data1 = data.filter(function(a) {
                                    console.log('(,' + a.stockist_code + ',).indexOf(,' + $scope.Mypln.stockist.selected.id + ',)');

                                    return (a.stockist_code == $scope.fmcgData.stockist.selected.id);
                                });
                                data = data1;
                            } else {
                                if (($scope.SF_type !== 2 || $scope.SF_type == 1) && $scope.BrndSumm.stockist != undefined && choice == 102 && $scope.DistBased == 1) {
                                    data1 = data.filter(function(a) {
                                        return (a.stockist_code == $scope.BrndSumm.stockist.selected.id);
                                    });
                                    data = data1;
                                }
                            }
                        }
                        console.log(data);
                    }
                    $scope['myTpTwns'] = data;
                }
            } else {
                data = sdata;
            }
            $scope.selectionType = choice;
            $scope.modal.searchText = '';
            $scope.SelMulti = 0;
            var res = [];
            if (",5,6,7,25,26,36,106,503,".indexOf("," + choice + ",") > -1) {
                $scope.OrderFields = ['name'];
                tCode = '';
                if (",6,36,".indexOf("," + choice + ",") > -1 && $scope.CusOrder == 1) {
                    $scope.OrderFields = ['ListedDr_Sl_No'];
                }

                if (",6,".indexOf("," + choice + ",") > -1 && $scope.GEOTagNeed == 1 && $scope.fmcgData.PhoneOrderTypes.selected.id!=1) {
                    if ($scope.fmcgData.location == undefined) {
                        Toast("Location not available. Please wait till get Location.");

                        if($scope.currentSelection.length>0)
                           $scope.currentSelection=[]; 
                        return false;
                    }
                    $scope.FilteredData = data.filter(function(a) {
                        var loc = $scope.fmcgData.location.split(':');
                        var crDis = $scope.cal_Distns(loc[0], loc[1], a.lat, a.long, 'K');
                        if (a.lat != '') {
                            a.dis = crDis;
                        }
                        return (crDis <= $scope.DisRad);
                    });

                for (var i = 0; i < $scope.FilteredData.length; i++) {
                         $scope.FilteredDatad = $scope.OrderRetailerName.filter(function(a) {
                                               
                          return (a.id ==  $scope.FilteredData[i].id);
                                            });
                         if($scope.FilteredDatad.length>0)

                         $scope.FilteredData[i].color=1;
                    };

                    data = [];
                } else {
                    if (",4,5,6,7,".indexOf("," + choice + ",") > -1)
                        tCode = $scope.fmcgData.cluster.selected.id;
                    if (",106,".indexOf("," + choice + ",") > -1) {
                        tCode = $scope.Mypln.cluster.selected.id;
                    }

                    if (",503,".indexOf("," + choice + ",") > -1)
                        tCode = $scope.Retailer.cluster.selected.id;

                    if (",25,26,".indexOf("," + choice + ",") > -1)
                        tCode = $scope.RCPA.cluster.selected.id;
                    if (",36,".indexOf("," + choice + ",") > -1)
                        tCode = $scope.precall.cluster.selected.id;

                    $scope.FilteredData = data.filter(function(a) {
                        return (a.town_code === tCode);
                    });

                    for (var i = 0; i < $scope.FilteredData.length; i++) {
                         $scope.FilteredDatad = $scope.OrderRetailerName.filter(function(a) {
                                               
                          return (a.id ==  $scope.FilteredData[i].id);
                                            });
                         if($scope.FilteredDatad.length>0)

                         $scope.FilteredData[i].color=1;
                    };


                    if (tCode = '') {
                        showmsg_t = true;
                        ErrMsg = "select the Cluster...";
                    }
                    if ($scope.FilteredData.length < 1 && tCode != '') {
                        showmsg_t = true;
                        ErrMsg = "No Data in this Cluster...";
                    }
                    data = []; //data.filter(function (a) { return (a.town_code != $scope.fmcgData.cluster.selected.id); });
                    $scope.SWTOWN = 0;
                    if (",4,".indexOf("," + choice + ",") > -1) {
                        $scope.FilteredData = data.filter(function(a) {
                            $scope.SWTOWN = 1;
                            return (a.town_code != tCode);
                        });
                    }


                }
            }
            // else if (",4,14,".indexOf("," + choice + ",") > -1) {
            else if (",14,".indexOf("," + choice + ",") > -1) {
                $scope.FilteredData = data.filter(function(a) {
                    return (a.town_code === $scope.fmcgData.cluster.selected.id);
                });
                data = data.filter(function(a) {
                    return (a.town_code != $scope.fmcgData.cluster.selected.id);
                });
            }

            else if(",4,".indexOf("," + choice + ",") > -1 && $scope.GEOTagNeed == 1  && $scope.GeoTagPrimary_Nd == 1 && $scope.fmcgData.PhoneOrderTypes.selected.id!=1 && $scope.fmcgData.customer.selected.id == 3){


                    if ($scope.fmcgData.location == undefined) {
                        Toast("Location not available. Please wait till get Location.");

                        if($scope.currentSelection.length>0)
                           $scope.currentSelection=[]; 
                        return false;
                    }
                    data= data.filter(function(a) {
                        var loc = $scope.fmcgData.location.split(':');
                        var crDis = $scope.cal_Distns(loc[0], loc[1], a.lat, a.long, 'K');
                        if (a.lat != '') {
                            a.dis = crDis;
                        }
                        return (crDis <= $scope.DisRad);
                    });
                     
}

             else if (",8,9,10,29,41,49,88,89,90,".indexOf("," + choice + ",") > -1) {
                $scope.SelMulti = 1;
                if (choice == 8)
                    slDta = $scope.fmcgData.jontWorkSelectedList;

                 if (choice == 88)
                    slDta = $scope.fmcgData.BreedCategory;

                if(choice==89)

                    slDta=$scope.fmcgData.TourPlanRoute;
                if(choice==90)

                    slDta=$scope.fmcgData.TourPlanRetailer;
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
                        if (",8,41,".indexOf("," + choice + ",") > -1){
                            data[di].name=data[di].name.concat('-'+data[di].desig.substring(0,  data[di].desig.lastIndexOf('(')).replace(" ",""))

                        }
                    for (var ii = 0; ii < slDta.length; ii++) {
                      if (",88,".indexOf("," + choice + ",") > -1){
                        if (slDtaid == data[di].id) {

                        }
                      }

                        if (",90,".indexOf("," + choice + ",") > -1){
                            var slDtaid = slDta[ii].id;
                        }

                        if (",8,41,89".indexOf("," + choice + ",") > -1)
        
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
            } else if (choice == 3) {
                $scope.OrderFields = ['id'];
            } else {
                $scope.OrderFields = ['name'];
                //data=data.sort(function(d1,d2){return  d1.name.toLowerCase()>d2.name.toLowerCase() ? 1 : (d1.name.toLowerCase()<d2.name.toLowerCase()) ? -1 : 0});                
            }

            $scope.currentSelection = data;
            $ionicScrollDelegate.$getByHandle("optsScroll").scrollTop();

            $timeout(function() {
                // $("#tSearch").focus();
            }, 800);
        };
        $scope.closeModal = function() {
            $scope.modal.hide();
        };
        $scope.setSelProduct = function(item) {



            if (item.checked == false) {
                $scope.selectedProductt = $scope.selectedProduct.filter(function(aa) {
                    return (aa.id == item.id)

                })
                if ($scope.selectedProductt[0].rx_qty > 0) {
                    var a = $scope.selectedProductt[0].sample_qty;
                    //var a=$scope.selectedProductt[0].Rate*$scope.selectedProductt[0].rx_qty;
                    $scope.fmcgData.value -= a;
                    //$scope.fmcgData.netamount = $scope.fmcgData.value - $scope.fmcgData.discounttt;

                    if (!($scope.fmcgData.discounttt == undefined)) {
                        var perc = (($scope.fmcgData.discounttt / 100) * $scope.fmcgData.value).toFixed(2);
                        $scope.fmcgData.netamount = Math.floor($scope.fmcgData.value - perc);


                        $scope.fmcgData.dis = perc;
                    }


                }

            }


            $scope.selectedProduct = $scope.selectedProduct.filter(function(a) {
                return (a.id != item.id)
            })
            if (item.checked == true) {
                $scope.selectedProduct.push(item);
            }
        }

        $scope.roundNumber = function(number, precision) {
            precision = Math.abs(parseInt(precision)) || 0;
            var multiplier = Math.pow(10, precision);
            return parseFloat(Math.round(number * multiplier) / multiplier).toFixed(2);
        }
        $scope.RateByCd = function(ProdCd, typ) {
            var productRate = $scope.Product_State_Rates;

            var productcatRate = $scope.Product_Category_Rates;


            /*  vRate = 0;
            for (var key in productcatRate) {
                if (productcatRate[key]['Product_Detail_Code'] == ProdCd)
                    vRate = (typ == 1) ? productcatRate[key]['RP_Base_Rate'] : productcatRate[key]['Discount'];
            }
            return vRate;*/


            var temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            $scope.RateMode = userData.RateMode;
            if ($scope.RateMode == 0 || typ > 1) {
                vRate = 0;
                for (var key in productRate) {
                    if (productRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productRate[key]['Retailor_Price'] : productRate[key]['DistCasePrice'];
                }
                return vRate;
            }

            if ($scope.RateMode == 1) {

                vRate = 0;
                for (var key in productcatRate) {
                    if (productcatRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productcatRate[key]['RP_Base_Rate'] : productcatRate[key]['Discount'];
                }
                return vRate;

            }

        };
        $scope.svSelect = function(CurrentData) {
            var SlTyp = $scope.selectionType;
            if (SlTyp == 8)
                $scope.fmcgData.jontWorkSelectedList = [];

             if (SlTyp == 88)
                $scope.fmcgData.BreedCategory = [];

            if(SlTyp==89)
                 $scope.fmcgData.TourPlanRoute=[];
              if(SlTyp==90)
                 $scope.fmcgData.TourPlanRetailer=[];
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
                $scope.fmcgData.staffSelectedList = [];
                $scope.fmcgData.staffNms = '';
                $scope.fmcgData.staffIds = "";
            }
            if (SlTyp == 202) {
                $scope.fmcgData.staffSelectedListcc = [];
                $scope.fmcgData.ccstaffNms = '';
                $scope.fmcgData.ccstaffIds = "";
            }
            if (SlTyp == 203) {
                $scope.fmcgData.staffSelectedListbcc = [];
                $scope.fmcgData.bccstaffNms = '';
                $scope.fmcgData.bccstaffIds = "";
            }
            if (SlTyp == 10)
                $scope.fmcgData.giftSelectedList = [];

            for (var i = 0; i < CurrentData.length; i++) {
                if (CurrentData[i].checked == true) {
                    if (",8,41,".indexOf("," + SlTyp + ",") > -1) {
                        var jontWorkData = {};
                        jontWorkData.jointwork = CurrentData[i].id;
                        jontWorkData.jointworkname=CurrentData[i].name;
                       // jontWorkData.jointworkname = CurrentData[i].name+"--"+CurrentData[i].desig.substring(0,  CurrentData[i].desig.lastIndexOf('(')).replace(" ","");
                        if (SlTyp == 8)
                            $scope.fmcgData.jontWorkSelectedList.push(jontWorkData);
                        if (SlTyp == 41) {
                            $scope.RMCL.JntWrkCds += CurrentData[i].id + ', ';
                            $scope.RMCL.JntWrkNms += CurrentData[i].name + ', ';
                            $scope.RMCL.jontWorkSelectedList.push(jontWorkData);
                        }
                    } 
                  else if (",88,".indexOf("," + SlTyp + ",") > -1) {
                       var BreedData = {};
                        BreedData.jointwork = CurrentData[i].id;
                        BreedData.name=CurrentData[i].name;
                    $scope.fmcgData.BreedCategory.push(BreedData);
 
                     }else if(",89,".indexOf("," + SlTyp + ",") > -1){
                       var MultiTuorPlan = {};
                        MultiTuorPlan.jointwork = CurrentData[i].id;
                        MultiTuorPlan.name=CurrentData[i].name;
                    $scope.fmcgData.TourPlanRoute.push(MultiTuorPlan);
                     }

                     else if(",90,".indexOf("," + SlTyp + ",") > -1){
                       var Multiretailer = {};
                        Multiretailer.id = CurrentData[i].id;
                        Multiretailer.name=CurrentData[i].name;
                    $scope.fmcgData.TourPlanRetailer.push(Multiretailer);
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
                            productData.discount = CurrentData[i].discount || 0;
                            productData.discount_price = CurrentData[i].discount_price || 0;
                            productData.free = CurrentData[i].free || 0;
                            productData.recv_qty = CurrentData[i].recv_qty || 0;

                            if (CurrentData[i].rate == undefined && $scope.fmcgData.customer != undefined) CurrentData[i].rate = $scope.roundNumber($scope.RateByCd(CurrentData[i].id, $scope.fmcgData.customer.selected.id), 2) || 0.00;

                            productData.Rate = CurrentData[i].rate || 0.00;
                            productData.product_netwt = CurrentData[i].product_netwt || 0;
                            productData.netweightvalue = CurrentData[i].netweightvalue || 0;
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
                    } else if (",201,202,203,".indexOf("," + SlTyp + ",") > -1) {
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
                    } else if (",10,".indexOf("," + SlTyp + ",") > -1) {
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
            } else if (",201,202,203,".indexOf("," + SlTyp + ",") > -1)
                $scope.modalStaffs.hide();
            else {
                $scope.modal.hide();
            }
        }
        $scope.selectData = function(item) {
            $rootScope.hasData = true;
            var key = ['worktype', 'cluster', 'customer', 'stockist', 'chemist', 'doctor', 'uldoctor', 'jointwork', 'dcrtypes'];
            switch ($scope.selectionType) {

                case 1:
                case 101:
                case 102:

                default:
                    if (typeof($scope.fmcgData.customer) != "undefined") {
                        if ($scope.selectionType == 3 && $scope.fmcgData.customer.selected.id.toString() != item.id.toString()) {
                            $rootScope.deleteRecord = true;
                            $scope.clearData();
                            $scope.data = {};
                            $scope.fmcgData.worktype = {};
                            $scope.fmcgData.worktype.selected = {};
                            var cus = item.id;
                            var CusTyp = (cus == 1) ? "D" : (cus == 2) ? "C" : (cus == 3) ? "S" : (cus == 4) ? "U" : "R";
                            var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function(a) {
                                return (a.id == $scope.prvRptId)
                            }) : $scope.worktypes.filter(function(a) {
                                return (a.FWFlg == "F" && a.ETabs.indexOf(CusTyp) > -1)
                            });
                            $scope.fmcgData.worktype.selected = Wtyps[0]; //{ "id": "field work", "name": "Field Work","FWFlg":"F"}        
                            if ($scope.view_MR == 1) {
                                $scope.fmcgData.subordinate = {};
                                $scope.fmcgData.subordinate.selected = {};
                                $scope.fmcgData.subordinate.selected.id = $scope.sfCode;
                            }
                        }

                    }

                    if($scope.selectionType == 1111){
                          $scope.MissedDatesselection=item.name;
                    }
                    if ($scope.selectionType == 144) {
                        $scope.stockUpdation.subordinate = {};
                        $scope.stockUpdation.subordinate.name = item.name;
                        $scope.stockUpdation.subordinate.selected = {};
                        $scope.stockUpdation.subordinate.selected.id = item.id;

                         var TwnDet = fmcgLocalStorage.getData("town_master_" + item.id) || [];
                        if ($scope.selectionType == 144) {
                            $scope.cMTPDId = '_' + item.id;
                            //$scope.myTpTwns =  fmcgLocalStorage.getData("town_master_" + item.id) || [];
                        } else {
                            $scope.cDataID = '_' + item.id;
                            //$scope.clusters = fmcgLocalStorage.getData("town_master_" + item.id) || [];
                        }


                        if (TwnDet.length > 0) {
                            $scope.loadDatas(false, '_' + item.id);
                            $scope.TourPlanHq=undefined;
                        } else if (TwnDet.length <= 0) {
                            $ionicLoading.show({
                                template: 'Please Wait. Data is Sync...'
                            })
                            $scope.clearAll(false, '_' + item.id);

                        }

                    } else if ($scope.selectionType >= 500) {
                        typ = $scope.selectionType;
                        if (typ == 500) {

                            var TwnDet = fmcgLocalStorage.getData("town_master_" + item.id) || [];

                            if (TwnDet.length > 0) {
                                $scope.loadDatas(false, '_' + item.id);
                            } else if (TwnDet.length <= 0) {
                                $ionicLoading.show({
                                    template: 'Please Wait. Data is Sync...'
                                })
                                $scope.clearAll(false, '_' + item.id);
                            }

                            $scope.Retailer.subordinate = {};
                            $scope.Retailer.subordinate.name = item.name;
                            $scope.fmcgData.inshopHQ = item.name;
                            $scope.Retailer.subordinate.selected = {};
                            $scope.Retailer.subordinate.selected.id = item.id;
                            $scope.Retailer.stockist = {};
                            $scope.Retailer.cluster = {};
                            $scope.Retailer.doctor = {};
                            $scope.$broadcast('event_ClearDataFields');
                        }
                        if (typ == 501) {
                            $scope.Retailer.stockist = {};
                            $scope.Retailer.stockist.name = item.name;
                            $scope.fmcgData.inshopDisti = item.name;
                            $scope.Retailer.stockist.selected = {};
                            $scope.Retailer.stockist.selected.id = item.id;
                            $scope.Retailer.cluster = {};
                            $scope.Retailer.doctor = {};
                            $scope.$broadcast('event_ClearDataFields');
                        }
                        if (typ == 502) {
                            $scope.Retailer.cluster = {};
                            $scope.Retailer.cluster.name = item.name;
                            $scope.fmcgData.inshopRoute = item.name;
                            $scope.Retailer.cluster.selected = {};
                            $scope.Retailer.cluster.selected.id = item.id;
                            $scope.Retailer.doctor = {};
                            $scope.$broadcast('event_ClearDataFields');
                        }
                        if (typ == 503) {
                            $scope.Retailer.doctor = {};
                            $scope.Retailer.doctor.name = item.name;
                            $scope.Retailer.doctor.selected = {};
                            $scope.Retailer.doctor.selected.id = item.id;
                            $scope.fmcgData.doctor = {};
                            $scope.fmcgData.doctor.name = item.name;
                            $scope.fmcgData.Retailername = {};
                            $scope.fmcgData.Retailername = item.name;
                            $scope.$broadcast('event_ClearDataFields');
                            $scope.$broadcast('event_getDatas');
                        }
                        if (typ == 504) {
                            $scope.Retailer.Data.class = {};
                            $scope.Retailer.Data.class.name = item.name;
                            $scope.Retailer.Data.class.selected = {};
                            $scope.Retailer.Data.class.selected.id = item.id;
                        }
                        if (typ == 505) {
                            $scope.Retailer.Data.speciality = {};
                            $scope.Retailer.Data.speciality.name = item.name;
                            $scope.Retailer.Data.speciality.selected = {};
                            $scope.Retailer.Data.speciality.selected.id = item.id;
                        }


                        if(typ==506){
                           $scope.Mypln.doctor = {};
                           $scope.precall.cluster = {};
                            $scope.precall.cluster.name = item.name;
                            $scope.fmcgData.inshopRoute = item.name;
                            $scope.precall.cluster.selected = {};
                            $scope.precall.cluster.selected.id = item.id;
                             $scope.$broadcast('getroutebasedretailer');

                        }

                        if(typ==507){
                           
                            $scope.Retailer.doctor = {};
                            $scope.Retailer.doctor.name = item.name;
                            $scope.Retailer.doctor.selected = {};
                            $scope.Retailer.doctor.selected.id = item.id;
                            $scope.fmcgData.doctor={};
                            $scope.fmcgData.doctor.name = item.name;
                            $scope.Mypln.SuperStokit={};
                            $scope.Mypln.SuperStokit.selected={};
                            $scope.Mypln.SuperStokit.selected.id=item.id;
                            $scope.$broadcast('getroutebasedretailer');
                            $scope.stockUpdation.stockist = {};
                            $scope.stockUpdation.stockist.name = item.name;
                            $scope.stockUpdation.stockist.selected = {};
                            $scope.stockUpdation.stockist.selected.id = item.id;
                            stat = 0;
                            value = fmcgLocalStorage.getData("Last_SSUpdation_Date") || [];
                            for (key in value) {
                                if (value[key]['stockistCode'] == item.id) {
                                    stat = 1;
                                    key1 = key;
                                }
                            }
                            if (stat == 1)
                                $scope.LastSSUpdationDate = value[key1]['Last_SSUpdation_Date'];
                            else
                                $scope.LastSSUpdationDate = '';
                            $scope.$broadcast('GetStocks');
                        


                        }
                        if(typ==508){
                           $scope.fmcgData.Super_Stck_code={};
                           $scope.fmcgData.Super_Stck_code.selected = {};
                            $scope.fmcgData.Super_Stck_code.name=item.name;
                            $scope.fmcgData.Super_Stck_code.selected.id = item.id;
                        }


                    }

                    if ($scope.selectionType == 56) {
                        $scope.SalRet.doctor = {};
                        $scope.SalRet.doctor.name = item.name;
                        $scope.SalRet.doctor.selected = {};
                        $scope.SalRet.doctor.selected.id = item.id;
                    }
                    if ($scope.selectionType == 54) {
                        $scope.SalRet.stockist = {};
                        $scope.SalRet.stockist.name = item.name;
                        $scope.SalRet.stockist.selected = {};
                        $scope.SalRet.stockist.selected.id = item.id;
                    }
                   /* if ($scope.selectionType == 154) {
                        $scope.SalRet.subordinate = {};
                        $scope.SalRet.subordinate.name = item.name;
                        $scope.SalRet.subordinate.selected = {};
                        $scope.SalRet.subordinate.selected.id = item.id;
                    }*/
                    if ($scope.selectionType == 164) {
                        $scope.DayInv.subordinate = {};
                        $scope.DayInv.subordinate.name = item.name;
                        $scope.DayInv.subordinate.selected = {};
                        $scope.DayInv.subordinate.selected.id = item.id;
                        //if ($scope.DayInv.EMode == 1) {
                        $scope.$broadcast('getDailyBeginInvs');
                        //}
                    }
                    if ($scope.selectionType == 165) {
                        $scope.DayInv.stockist = {};
                        $scope.DayInv.stockist.name = item.name;
                        $scope.DayInv.stockist.selected = {};
                        $scope.DayInv.stockist.selected.id = item.id;
                        //if ($scope.DayInv.EMode == 1) {
                        $scope.$broadcast('getDailyBeginInvs');
                        //}
                    } else if ($scope.selectionType >= 600) {
                        $scope.lSettings.MyPrinterIP = item.address;
                        $scope.lSettings.MyPrinterName = item.name;
                    } else if ($scope.selectionType > 100) {
                        $scope.Mypln[key[$scope.selectionType - 101]] = {};
                        $scope.Mypln[key[$scope.selectionType - 101]].name = item.name;
                        $scope.Mypln[key[$scope.selectionType - 101]].selected = {};
                        $scope.Mypln[key[$scope.selectionType - 101]].selected.id = item.id;

                        if ($scope.selectionType == 101)
                            $scope.Mypln[key[0]].selected.FWFlg = item.FWFlg;
                        if ($scope.selectionType == 104) {
                            $scope.$broadcast('distReport');
                            $scope.$broadcast('getBrandSalSumry');

                            if(typeof($scope.Mypln.cluster)!='object'){
                             $scope.Mypln.cluster.selected.id={};

                            }
                            
                                $scope.Mypln.cluster = {};
                                 $scope.Retailer.cluster={};
                        }

                         if ($scope.selectionType == 105) {
                            $scope.MsdStk={};
                            $scope.MsdStk.st = {};
                            $scope.MsdStk.st.name = item.name;
                            $scope.MsdStk.st.selected = {};
                            $scope.MsdStk.st.selected.id = item.id;
                            $scope.precall.cluster = {}; 
                            $scope.$broadcast('getDistributor');
                        }


                  if ($scope.selectionType == 107) {
                        $scope.Mypln.subordinate = {};
                       $scope.Mypln.subordinate.name = item.name;
                        $scope.Mypln.subordinate.selected = {};
                        $scope.Mypln.subordinate.selected.id = item.id;

                       var TwnDet = fmcgLocalStorage.getData("town_master_" + item.id) || [];

                            if (TwnDet.length > 0) {
                                $scope.loadDatas(false, '_' + item.id);
                            } else if (TwnDet.length <= 0) {
                                $ionicLoading.show({
                                    template: 'Please Wait. Data is Sync...'
                                })
                                $scope.clearAll(false, '_' + item.id);
                            }
                                $scope.$broadcast('HQCALL');

                    }


                        if ($scope.selectionType == 106) {
                            $scope.$broadcast('getLostProducts', {
                                DrId: item.id
                            });
                        }
                        if ($scope.selectionType == 102) {
                            $scope.Mypln.doctor = {};

                         $scope.precall.cluster = {};
                            $scope.precall.cluster.name = item.name;
                            $scope.fmcgData.inshopRoute = item.name;
                            $scope.precall.cluster.selected = {};
                            $scope.precall.cluster.selected.id = item.id;

                        }
                        if ($scope.selectionType == 206) {
                            $scope.Mypln.doctor = {};
                            $scope.Mypln.doctor.name = item.name;
                            $scope.Mypln.doctor.selected = {};
                            $scope.Mypln.doctor.selected.id = item.id;
                            $scope.Mypln.doctor.address = item.ListedDr_Address1;
                            $scope.Mypln.doctor.Mobile_Number = item.Mobile_Number;

                        }
                    } else if ($scope.selectionType == 200) {
                        $scope.folder = {};
                        $scope.folder.name = item.name;
                        $scope.folder.selected = {};
                        $scope.folder.selected.id = item.id;
                        $scope.$broadcast('getFolders', {
                            Details: item
                        });
                    } else if ($scope.selectionType == 201) {
                        $scope.subfolder = {};
                        $scope.subfolder.name = item.name;
                        $scope.subfolder.selected = {};
                        $scope.subfolder.selected.id = item.id;
                        $scope.$broadcast('moveFolders', {
                            Details: item
                        });
                    } else if ($scope.selectionType > 40) {
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

                    } else if ($scope.selectionType > 30) {
                        $scope.precall[key[$scope.selectionType - 31]] = {};
                        $scope.precall[key[$scope.selectionType - 31]].name = item.name;
                        $scope.precall[key[$scope.selectionType - 31]].selected = {};
                        $scope.precall[key[$scope.selectionType - 31]].selected.id = item.id;


                        if ($scope.selectionType == 32) {
                            $scope.precall.doctor = {};
                            $scope.precall.doctor = undefined;
                            $scope.$broadcast('clear', {
                                DrId: item.id
                            });
                        }
                        if ($scope.selectionType == 36)
                            $scope.$broadcast('getPreCall', {
                                DrId: item.id
                            });
                    } else if ($scope.selectionType > 20) {
                        if ($scope.selectionType == 22) {
                            if ($scope.RCPA.cluster == undefined || $scope.RCPA.cluster.selected.id != item.id) {
                                $scope.RCPA.chemist = undefined;
                                $scope.RCPA.doctor = undefined;
                            }
                        }
                        $scope.RCPA[key[$scope.selectionType - 21]] = {};
                        $scope.RCPA[key[$scope.selectionType - 21]].name = item.name;
                        $scope.RCPA[key[$scope.selectionType - 21]].selected = {};
                        $scope.RCPA[key[$scope.selectionType - 21]].selected.id = item.id;
                    } else {

                        $scope.fmcgData[key[$scope.selectionType - 1]] = {};
                        $scope.fmcgData[key[$scope.selectionType - 1]].name = item.name;
                        $scope.fmcgData[key[$scope.selectionType - 1]].selected = {};
                        $scope.fmcgData[key[$scope.selectionType - 1]].selected.id = item.id;
                        if ($scope.selectionType == 1)
                            $scope.fmcgData[key[0]].selected.FWFlg = item.FWFlg;
                        if ($scope.selectionType == 6) {
                            $scope.fmcgData[key[$scope.selectionType - 1]].address = item.ListedDr_Address1
                            $scope.fmcgData[key[$scope.selectionType - 1]].Mobile_Number = item.Mobile_Number;
                            $scope.$broadcast('getPreCall', {
                                DrId: item.id
                            });
                        }
                    }

                    var temp = [5, 6, 7, 503];
                    if (temp.indexOf($scope.selectionType) !== -1 && $scope.fmcgData.source === 0) {
                        $scope.fmcgData.cluster = {};
                        $scope.fmcgData.cluster.name = item.town_name;
                        $scope.fmcgData.cluster.selected = {};
                        $scope.fmcgData.cluster.selected.id = item.town_code;


                        $scope.fmcgData.Doc_cat_code = item.Doc_cat_code;


                        $scope.Product_Category_Rates = $scope.Product_Category_Rates_All.filter(function(a) {
                            return (a.Cate_Code == $scope.fmcgData.Doc_cat_code);
                        });

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


                    case 4:

                        $scope.fmcgData.stockist.selected.id=item.id;
                        if($scope.Price_category==1 && $scope.fmcgData.PhoneOrderTypes.selected.id==3){
                        $scope.Product_Category_Rates = $scope.Product_Category_Rates_All.filter(function(a) {
                            return (a.Cate_Code ==item.Dis_Cat_Code);
                        });
                        $scope.ProdCategoryyyy=$scope.ProdCategory;
                        if($scope.fmcgData.SLTItm!=undefined){
                        $scope.ProdCategoryyyy=$scope.fmcgData.SLTItm;
                        }

                         $scope.CatewsBillProdd = $scope.ProdCategoryyyy.filter(function(a) {
                                return (a.id != -1)
                                 });

                        for (var i = 0; i <  $scope.CatewsBillProdd.length; i++) {
                            if($scope.CatewsBillProdd[i].Products!=undefined)
                            for (var ii = 0; ii < $scope.CatewsBillProdd[i].Products.length; ii++) {
                                $scope.CatewsBillProdd[i].Products[ii].Rate=parseFloat(distfunction($scope.CatewsBillProdd[i].Products[ii].product,$scope.fmcgData.customer.selected.id));
                               // $scope.CatewsBillProdd[i].Products[ii].sample_qty=($scope.CatewsBillProdd[i].Products[ii].rx_qty * $scope.CatewsBillProdd[i].Products[ii].Rate);
                            $scope.CatewsBillProdd[i].Products[ii].sample_qty=0;
                            $scope.CatewsBillProdd[i].Products[ii].rx_qty=0;                                           }
                            $scope.CatewsBillProdd[i].SubTotal=0;
                            $scope.fmcgData.netamount=0;
                            $scope.fmcgData.value=0;
                            $scope.GTotal=0;

                        }


                function distfunction(ProdCd,typ){
                        if ($scope.RateMode == 0 || typ > 1) {
                            var productRate = $scope.Product_State_Rates;
                            vRate = 0;
                            for (var key in productRate) {
                                if (productRate[key]['Product_Detail_Code'] == ProdCd)
                                    vRate = (typ == 1) ? productRate[key]['Retailor_Price'] : productRate[key]['DistCasePrice'];
                            }
                            return vRate;
                        }

                        if ($scope.RateMode == 1) {
                            var productcatRate = $scope.Product_Category_Rates;
                            vRate = 0;
                            for (var key in productcatRate) {
                                if (productcatRate[key]['Product_Detail_Code'] == ProdCd)
                                    vRate = (typ == 1) ? productcatRate[key]['RP_Base_Rate'] : productcatRate[key]['Discount'];
                            }
                            return vRate;
                        }
                    }

                     }

                        
                        

                    break;

                  case 88:
                    var BreedData = {};
                    BreedData.jointwork = item.id;
                    BreedData.name = item.name;
                    $scope.fmcgData.BreedCategory = $scope.fmcgData.BreedCategory || [];
                    var len = $scope.fmcgData.BreedCategory.length;
                    var flag = true;
                    for (var i = 0; i < len; i++) {
                        if ($scope.fmcgData.BreedCategory[i]['jointwork'] === item.id) {
                            flag = false;
                        }
                    }
                    if (flag)
                        $scope.fmcgData.BreedCategory.push(BreedData);
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

                    case 5555:
                    $scope.fmcgData.MsdRetailerName[$scope.msdremarksid].remarks= item.name;
                    break;

                    case 5556:
                     $scope.fmcgData.MSD[$scope.msdremarksid].Wtype= item.name;
                     $scope.fmcgData.MSD[$scope.msdremarksid].Wtypeflag= item.FWFlg;

                    break;
                   /* case 5557:
                     $scope.fmcgData.MSD[$scope.msdremarksid].Remarks= item.name;*/
                    break;
                case 27:
                    $scope.fmcgData.instrumenttype = {};
                    $scope.fmcgData.instrumenttype.name = item.name;
                    $scope.fmcgData.instrumenttype.selected = {};
                    $scope.fmcgData.instrumenttype.selected.id = item.id;

                    break;


                case 99:
                    $scope.fmcgData.PhoneOrderTypes = {};
                    $scope.fmcgData.PhoneOrderTypes.name = item.name;
                    $scope.fmcgData.PhoneOrderTypes.selected = {};
                    $scope.fmcgData.PhoneOrderTypes.selected.id = item.id;
                    $scope.fmcgData.doctor={};
                    $scope.precall==1;
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

                    $scope.fmcgData.Catogoryformer = {};
                    $scope.fmcgData.Catogoryformer.name = item.name;
                    $scope.fmcgData.Catogoryformer.selected = {};
                    $scope.fmcgData.Catogoryformer.selected.id = item.id;

                    break;
                    case 115:

                   $scope.fmcgData.LEOS = {};
                    $scope.fmcgData.LEOS.name = item.name;
                    $scope.fmcgData.LEOS.selected = {};
                    $scope.fmcgData.LEOS.selected.id = item.id;
                    break;

               case 116:

                   $scope.fmcgData.MOC = {};
                    $scope.fmcgData.MOC.name = item.name;
                    $scope.fmcgData.MOC.selected = {};
                    $scope.fmcgData.MOC.selected.id = item.id;

                    if(item.id==11){
                         $scope.fmcgData.KM=0;
                           $scope.fmcgData.TotalKM=0;
                    }else{

                         $scope.fmcgData.Fare='';
                    }
                    break;


                     case 117:

                    $scope.fmcgData.FrequencyOfVisit = {};
                    $scope.fmcgData.FrequencyOfVisit.name = item.name;
                    $scope.fmcgData.FrequencyOfVisit.selected = {};
                    $scope.fmcgData.FrequencyOfVisit.selected.id = item.id;
                    break;
                    case 118:

                    $scope.fmcgData.CurrentlyUsing = {};
                    $scope.fmcgData.CurrentlyUsing.name = item.name;
                    $scope.fmcgData.CurrentlyUsing.selected = {};
                    $scope.fmcgData.CurrentlyUsing.selected.id = item.id;
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
                case 154:
                    var key = {
                        "c16": "speciality",
                        "c17": "category",
                        "c18": "subordinate"
                    };

                    if ($scope.selectionType == 50) {
                        $scope.tpview[key['c18']] = {};
                        $scope.tpview[key['c18']].name = item.name;
                        $scope.tpview[key['c18']].selected = {};
                        $scope.tpview[key['c18']].selected.id = item.id;
                        $scope.$broadcast('eGetTpEntry');

                    } else if ($scope.selectionType == 208) {
                        $scope.Reload[key['c18']] = {};
                        $scope.Reload[key['c18']].name = item.name;
                        $scope.Reload[key['c18']].selected = {};
                        $scope.Reload[key['c18']].selected.id = item.id;
                    } else if ($scope.selectionType == 58) {
                        $scope.geoTag[key['c18']] = {};
                        $scope.geoTag[key['c18']].name = item.name;
                        $scope.geoTag[key['c18']].selected = {};
                        $scope.geoTag[key['c18']].selected.id = item.id;
                        $scope.geoTag['cluster'] = {};
                    } else if ($scope.selectionType == 52) {
                        $scope.geoTag['cluster'] = {};
                        $scope.geoTag['cluster'].name = item.name;
                        $scope.geoTag['cluster'].selected = {};
                        $scope.geoTag['cluster'].selected.id = item.id;
                          $scope.geoTag['stockist_code'] = {};
                        $scope.geoTag['stockist_code'].name = item.name;
                        $scope.geoTag['stockist_code'].selected = {};
                        $scope.geoTag['stockist_code'].selected.id = item.stockist_code;
                        $scope.$broadcast('setGeoData');
                        $scope.$broadcast('setGeodistData');
                        
                    } else if ($scope.selectionType == 103) {
                        $scope.Mypln[key['c18']] = {};
                        $scope.Mypln[key['c18']].name = item.name;
                        $scope.Mypln[key['c18']].selected = {};
                        $scope.Mypln[key['c18']].selected.id = item.id;


                    }else if ($scope.selectionType == 154) {
                      /*  $scope.Mypln[key['c18']] = {};
                        $scope.Mypln[key['c18']].name = item.name;
                        $scope.Mypln[key['c18']].selected = {};
                        $scope.Mypln[key['c18']].selected.id = item.id;*/

                         $scope.SalRet[key['c18']] = {};
                        $scope.SalRet[key['c18']].name = item.name;
                        $scope.SalRet[key['c18']].selected = {};
                        $scope.SalRet[key['c18']].selected.id = item.id;
                    }
                     
                     else if ($scope.selectionType == 40) {
                        $scope.MnthSumm[key['c18']] = {};
                        $scope.MnthSumm[key['c18']].name = item.name;
                        $scope.MnthSumm[key['c18']].selected = {};
                        $scope.MnthSumm[key['c18']].selected.id = item.id;

                        $scope.$broadcast('getMonthReport');
                    } else if ($scope.selectionType == 42) {
                        $scope.BrndSumm[key['c18']] = {};
                        $scope.BrndSumm[key['c18']].name = item.name;
                        $scope.BrndSumm[key['c18']].selected = {};
                        $scope.BrndSumm[key['c18']].selected.id = item.id;

                        $scope.$broadcast('getBrandSalSumry');
                    } else if ($scope.selectionType == 38) {

                        $scope.precall[key['c18']] = {};
                        $scope.precall[key['c18']].name = item.name;
                        $scope.precall[key['c18']].selected = {};
                        $scope.precall[key['c18']].selected.id = item.id;
                    } else if ($scope.selectionType == 48) {
                        $scope.RMCL[key['c18']] = {};
                        $scope.RMCL[key['c18']].name = item.name;
                        $scope.RMCL[key['c18']].selected = {};
                        $scope.RMCL[key['c18']].selected.id = item.id;
                        $scope.RMCL.doctor = undefined;
                    } else if ($scope.selectionType == 28) {

                        $scope.RCPA[key['c18']] = {};
                        $scope.RCPA[key['c18']].name = item.name;
                        $scope.RCPA[key['c18']].selected = {};
                        $scope.RCPA[key['c18']].selected.id = item.id;

                        $scope.RCPA.chemist = undefined;
                        $scope.RCPA.doctor = undefined;
                    } else {
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
                    if (',18,28,38,48,104,103,154,58,'.indexOf(',' + $scope.selectionType + ',') > -1) {
                        var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
                        loginInfo.curSFCode = item.id;
                        $scope.fmcgData.jontWorkSelectedList = [];
                        window.localStorage.setItem("loginInfo", JSON.stringify(loginInfo));
                        var TwnDet = fmcgLocalStorage.getData("town_master_" + item.id) || [];
                        if ($scope.selectionType == 103 || 154) {
                            $scope.cMTPDId = '_' + item.id;
                            //$scope.myTpTwns =  fmcgLocalStorage.getData("town_master_" + item.id) || [];
                        } else {
                            $scope.cDataID = '_' + item.id;
                            //$scope.clusters = fmcgLocalStorage.getData("town_master_" + item.id) || [];
                        }



                        if (TwnDet.length > 0) {
                            $scope.loadDatas(false, '_' + item.id);
                            $scope.TourPlanHq=undefined;
                        } else if (TwnDet.length <= 0) {
                            $ionicLoading.show({
                                template: 'Please Wait. Data is Sync...'
                            })
                            $scope.clearAll(false, '_' + item.id);

                        }

                        $scope.$broadcast('DeliveryStatus', {
                                DrId: item.id
                            });

                    }

                    break;
            }
            $scope.modal.hide();
        };

        //Cleanup the modal when we're done with it!
        $scope.$on('$destroy', function() {
            $scope.modal.remove();
        });
        $scope.customers = [{
            'id': '1',
            'name': $scope.EDrCap,
            'url': 'manageDoctorResult'
        }];
        var al = 1;
        if ($scope.ChmNeed != 1) {
            $scope.customers.push({
                'id': '2',
                'name': $scope.EChmCap,
                'url': 'manageChemistResult'
            });
            $scope.cCI = al;
            al++;
        }
        if ($scope.StkNeed != 0) {
            $scope.customers.push({
                'id': '3',
                'name': $scope.EStkCap,
                'url': 'manageStockistResult'
            });
            $scope.sCI = al;
            al++;
        }
        if ($scope.UNLNeed != 1) {
            $scope.customers.push({
                'id': '4',
                'name': $scope.ENLCap,
                'url': 'manageStockistResult'
            });
            $scope.nCI = al;
            al++;
        }

        $scope.lSettings = fmcgLocalStorage.getData("LocalSettings") || {};
        $scope.DataLoaded = true;
        $scope.fmcgData = {};
        $scope.PrvData = {};

        //thirueditretailer
        $scope.Retailer = {};

        $scope.Reload = {};
        $scope.precall = {};
        $scope.RCPA = {};
        $scope.SalRet = {};
        $scope.DayInv = {};
        $scope.RMCL = {};
        $scope.Mypln = {};
        $scope.MnthSumm = {};
        $scope.BrndSumm = {};
        $scope.stockUpdation = {};
        $scope.tpview = {};
        $scope.geoTag = {};
        $scope.OfflineRetailers = {};
        $scope.MTPEnty = {};
        $scope.InvData = {};

        $scope.go = function(path) {
            $location.path(path);
        };
        $scope.Mypln.worktype = {};
        $scope.Mypln.worktype.selected = {};
        $scope.prvRptId = '';
        var Wtyps = $scope.worktypes.filter(function(a) {
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
        /* $scope.logout = function() {
             //window.localStorage.clear();
             $scope.toggleLeft();
             $state.go("signin");
         }*/

        $scope.logout = function() {
            $ionicPopup.confirm({
                title: 'Today Activity',
                content: 'Do you want to Stop Your Day  Work?'
            }).then(function(res) {
                if (res) {

                    $ionicLoading.show({
                        template: 'Loading...'
                    });
                    $scope.data = {};
                    $scope.data.Lattitude = $scope.fmcgData.logoutLAT;
                    $scope.data.Langitude = $scope.fmcgData.logoutLANG;
                    $scope.attendanceView = window.localStorage.getItem("attendanceView");
                    if ($scope.attendanceView != undefined && $scope.attendanceView == 1) {
                        $scope.data.StartTime = 1;
                    } else {
                        $scope.data.StartTime = 0;
                    }
                    fmcgAPIservice.getPostData('POST', 'get/logouttime', $scope.data)
                        .success(function(response) {
                            var loginData = {};
                            loginData.LOGIN = false;
                            window.localStorage.setItem("LOGIN", JSON.stringify(loginData.LOGIN));
                            $state.go("signin");


                        }).error(function() {
                            Toast("No Internet Connection! Try Again.");
                            $ionicLoading.hide();
                        });


                } else {
                    console.log('You are not sure');
                }
            });



        }



        $scope.toggleLeft = function() {
            //  notification.vibrate(15);
            //if ($('body').hasClass('menu-open')) {
            //}
            $ionicSideMenuDelegate.toggleLeft();
            $('body').removeClass('menu-open');
        };


 var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;

console.log(imageUrl);
     /*   $scope.dashboard_url = function () {


//var ref = cordova.InAppBrowser.open(imageUrl + 'MasterFiles/Dashboard_Menu.aspx?sfcode=' + $scope.sfCode + '&cMnth=' + 07 + '&cYr=' + 2019 + '&div_code=' + userData.divisionCode + '&sf_type=' + userData.SF_type , '_blank', 'location=yes');  

var ref=cordova.InAppBrowser.open("http://www.fmcg.sanfmcg.com/");
}*/
        $scope.goToAddNew = function() {
            if ($scope.cComputer == false) {
                if ($scope.GeoChk == 0) {
                    Toast("Checking for Location Services");
                    if (_currentLocation.Latitude != undefined) {
                        var dt = new Date();
                        var dt1 = new Date(_currentLocation.Time);
                        var difference = dt.getTime() - dt1.getTime(); // This will give difference in milliseconds
                        var resultInMinutes = Math.round(difference / 60000);
                        if (resultInMinutes > 10) {
                            _currentLocation = {};
                            return false;
                        }
                    } else {
                        // return false;
                    }
                }
                if (_currentLocation.Latitude != undefined) {
                    if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                        $scope.fmcgData.geoaddress = _currentLocation.address;
                        $scope.fmcgData.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;
                    }
                }
            }
            $state.go('fmcgmenu.addNew');
        };
        if ($scope.onLine) {

        }

        function svLocAddRetailer() {
            $scope.onLine = isReachable(); //console.log('Offline Checking');
            if ($scope.onLine && navigator.onLine) {
                var QuData = fmcgLocalStorage.getData("CustAddQ") || [];
                if (QuData.length > 0) {
                    var QCusData = QuData[0];
                    fmcgAPIservice.addMAData('POST', 'dcr/save', '2', QCusData)
                        .success(function(response) {
                            var dataCus = QuData.shift();
                            if (response['success']) {

                                $scope.OfflineRetailers = fmcgLocalStorage.getData("Offline_Retailer" + QCusData.cDataID) || [];
                                OffRets = $scope.OfflineRetailers.filter(function(a) {
                                    console.log(a.id + ' == ' + QCusData.DrKeyId);
                                    return (a.id == QCusData.DrKeyId)
                                });
                                $indxa = $scope.OfflineRetailers.indexOf(OffRets[0]);
                                $scope.OfflineRetailers = $scope.OfflineRetailers.slice($indxa, 0);
                                fmcgLocalStorage.createData("Offline_Retailer" + QCusData.cDataID, $scope.OfflineRetailers);
                                Toast('Local calls synced', true);

                            } else {
                                QuData.push(dataCus);
                            }
                            fmcgLocalStorage.createData("CustAddQ", QuData);
                            setTimeout(function() {
                                svLocAddRetailer();
                            }, 10000);
                        })
                        .error(function() {
                            setTimeout(function() {
                                svLocAddRetailer();
                            }, 10000);
                        });
                } else
                    setTimeout(function() {
                        svLocAddRetailer();
                    }, 10000);
            } else
                setTimeout(function() {
                    svLocAddRetailer();
                }, 5000);
        }

        setTimeout(function() {
            svLocAddRetailer();
        }, 500);
        $scope._savingData = false;
        $scope.savingData = {};
        $scope.savingData.RCPA = false;
        $scope.savingData.RMCL = false;
        $scope.savingData.MyPL = false;
        $scope.savingData.Rmks = false;
        $scope.savingData.EVNT = false;



function MissedLocal() {
      $scope.onLine = isReachable();

            if ($scope.onLine && navigator.onLine) {
             var draftData = fmcgLocalStorage.getData("saveLaterMissed") || [];

                if (draftData.length > 0 && $scope._savingData == false) {
            var data = draftData[0];
               $scope._savingData = true;

                       fmcgAPIservice.saveDCRData('POST', 'dcr/save', data, true)
                                .success(function(response) {
                                    if (!response['success'] && response['msg'] !== 'Call Already Exist') {
                                        draftData.shift();
                                    } else if (response['msg'] == 'Call Already Exist') {
                                        draftData.shift();

                                    } else if (response['success']) {
                                      
                        if(draftData[0].MSDFlag==undefined){
                          Toast('Local calls synced', true);
                         }
                                       
                                          draftData.shift();
                                    } else {
                                        var data = draftData.shift();
                                        draftData.push(data);
                                    }
                                    fmcgLocalStorage.createData("saveLaterMissed", draftData);
                                    setTimeout(function() {
                                        $scope._savingData = false;
                                    }, 1000);
                                })
                                .error(function() {
                                    setTimeout(function() {
                                        $scope._savingData = false;
                                    }, 1000);
                                });


                }
                   
            }

      }



        function svQDatas() {
            $scope.onLine = isReachable();

            if ($scope.onLine && navigator.onLine) {

                var draftData = fmcgLocalStorage.getData("saveLater") || [];
                if (draftData.length > 0 && $scope._savingData == false) {
                    var data = draftData[0];
                    if (data.subordinate == undefined || data.subordinate.selected == undefined || data.subordinate.selected.id == undefined)
                        $scope.OfflineRetailers = [];
                    else
                        $scope.OfflineRetailers = fmcgLocalStorage.getData("Offline_Retailer" + data.subordinate.selected.id) || [];
                    OffRets = $scope.OfflineRetailers.filter(function(a) {
                        return (a.id == data.doctor.selected.id)
                    });
                    if (OffRets.length > 0) {
                        console.log('Offline Calls Found...');
                    } else {
                        $scope._savingData = true;
                        if (data.arc) {
                            var arc = data.arc;
                            var amc = data.amc;
                            fmcgAPIservice.saveDCRData('POST', 'dcr/updateEntry&arc=' + arc + '&amc=' + amc, data, true).success(function(response) {
                                if (!response['success'] && response['msg'] !== 'Call Already Exist') {
                                    draftData.shift();
                                } else if (response['msg'] == 'Call Already Exist') {
                                    draftData.shift();
                                } else if (response['success']) {
                                    draftData.shift();
                                    // Toast('Local calls synced', true);
                                    Toast('Outbox call submitted successfully', true);
                                } else {
                                    var data = draftData.shift();
                                    draftData.push(data);
                                }
                                fmcgLocalStorage.createData("saveLater", draftData);
                                setTimeout(function() {
                                    $scope._savingData = false;
                                }, 1000);

                            }).error(function() {
                                setTimeout(function() {
                                    $scope._savingData = false;
                                }, 1000);
                            });
                        } else {
                            //     
                            fmcgAPIservice.saveDCRData('POST', 'dcr/save', data, true)
                                .success(function(response) {
                                    if (!response['success'] && response['msg'] !== 'Call Already Exist') {
                                        draftData.shift();
                                    } else if (response['msg'] == 'Call Already Exist') {
                                        draftData.shift();

                                    } else if (response['success']) {
                                      
                    /*if(draftData[0].MSDFlag==undefined){
                          Toast('Local calls synced', true);
                      }
                          */             
                                          draftData.shift();
                                    } else {
                                        var data = draftData.shift();
                                        draftData.push(data);
                                    }
                                    fmcgLocalStorage.createData("saveLater", draftData);
                                    setTimeout(function() {
                                        $scope._savingData = false;
                                    }, 1000);
                                })
                                .error(function() {
                                    setTimeout(function() {
                                        $scope._savingData = false;
                                    }, 1000);
                                });

                        }
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
                        function(response) {
                            QDataEVNT.shift();
                            fmcgLocalStorage.createData("events", QDataEVNT);
                            setTimeout(function() {
                                $scope.savingData.EVNT = false;
                            }, 1000);

                        },
                        function() {
                            setTimeout(function() {
                                $scope.savingData.EVNT = false;
                            }, 1000);
                        }, options);
                }

                var QDataRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
                if (QDataRCPA.length > 0 && $scope.savingData.RCPA == false) {
                    var dataRCPA = QDataRCPA[0];
                    $scope.savingData.RCPA = true;
                    fmcgAPIservice.addMAData('POST', 'dcr/save', '4', dataRCPA)
                        .success(function(response) {
                            if (response['success']) {
                                QDataRCPA.shift();
                                Toast('Local calls synced', true);
                            } else {
                                var dataRCPA = QDataRCPA.shift();
                                QDataRCPA.push(dataRCPA);
                            }
                            fmcgLocalStorage.createData("OutBx_RCPA", QDataRCPA);
                            setTimeout(function() {
                                $scope.savingData.RCPA = false;
                            }, 1000);
                        })
                        .error(function() {
                            setTimeout(function() {
                                $scope.savingData.RCPA = false;
                            }, 1000);
                        });
                }

                var QDataRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
                if (QDataRMCL.length > 0 && $scope.savingData.RMCL == false) {
                    var dataRMCL = QDataRMCL[0];
                    $scope.savingData.RMCL = true;
                    fmcgAPIservice.addMAData('POST', 'dcr/save', '5', dataRMCL)
                        .success(function(response) {
                            if (response['success']) {
                                QDataRMCL.shift();
                                Toast('Local calls synced', true);
                            } else {
                                var dataRMCL = QDataRMCL.shift();
                                QDataRMCL.push(dataRMCL);
                            }
                            fmcgLocalStorage.createData("OutBx_RMCL", QDataRMCL);
                            setTimeout(function() {
                                $scope.savingData.RMCL = false;
                            }, 1000);
                        })
                        .error(function() {
                            setTimeout(function() {
                                $scope.savingData.RMCL = false;
                            }, 1000);
                        });
                }

                var QDataMyPL = fmcgLocalStorage.getData("myplnQ") || [];
                if (QDataMyPL.length > 0 && $scope.savingData.MyPL == false) {
                    var dataMyPL = QDataMyPL[0];
                    $scope.savingData.MyPL = true;
                    fmcgAPIservice.addMAData('POST', 'dcr/save', '3', dataMyPL)
                        .success(function(response) {
                            if (response['success']) {
                                QDataMyPL.shift();
                                Toast('Local calls synced', true);
                            } else if (response.msg == "DCR Summary Already Updated") {
                                QDataMyPL.shift();
                                Toast(response.msg);
                                $ionicLoading.hide();
                            } else {
                                var dataMyPL = QDataMyPL.shift();
                                QDataMyPL.push(dataMyPL);
                            }
                            fmcgLocalStorage.createData("myplnQ", QDataMyPL);
                            setTimeout(function() {
                                $scope.savingData.MyPL = false;
                            }, 1000);
                        })
                        .error(function() {
                            setTimeout(function() {
                                $scope.savingData.MyPL = false;
                            }, 1000);
                        });
                }

                var QDataRmks = fmcgLocalStorage.getData("MyDyRmksQ") || [];
                if (QDataRmks.length > 0 && $scope.savingData.Rmks == false) {
                    var dataRmks = QDataRmks[0];
                    $scope.savingData.Rmks = true;
                    fmcgAPIservice.updateRemark('POST', 'dcr/updRem', dataRmks)
                        .success(function(response) {
                            if (response['success']) {
                                Toast('Local calls synced', true);
                            }
                            QDataRmks.shift();
                            fmcgLocalStorage.createData("MyDyRmksQ", QDataRmks);
                            setTimeout(function() {
                                $scope.savingData.Rmks = false;
                            }, 1000);
                        })
                        .error(function() {
                            setTimeout(function() {
                                $scope.savingData.Rmks = false;
                            }, 1000);
                        });
                }
            }
        }
        svQDataIntrvl = setInterval(function() {
            svQDatas();

        MissedLocal();

        }, 10000); //(1000*(60*5)));


svQDataIntrvl = setInterval(function() {

MissedLocal();

        }, 15000);
        setInterval(function() {
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
            var QueData = fmcgLocalStorage.getData("CustAddQ") || [];
            $scope.outboxCount += QueData.length;

            var QueData = fmcgLocalStorage.getData("draft") || [];
            $scope.draftCount = QueData.length;
        }, 500);

        $scope.fmcgDataCopy = angular.copy($scope.fmcgData);
        $scope.clearData = function() {
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
        $scope.saveToDraftO = function() {
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
            } else {
                $ionicPopup.confirm({
                        title: 'Call Conflict',
                        content: 'You have call for other worktype in draft do you want to replace...?'
                    })
                    .then(function(res) {
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
                    });
            }
        };

        $scope.setAllow = function() {
            $rootScope.hasData = true;
            if ($scope.fmcgData.amc || $scope.fmcgData.arc) {
                $rootScope.hasEditData = true;
            }

        }
    }])
    .controller('invEntryCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', '$ionicModal', '$ionicScrollDelegate', function($rootScope, $scope, $state, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal, $ionicScrollDelegate) {

        $scope.$parent.navTitle = "Invoice Entry";

        $scope.taxDets = fmcgLocalStorage.getData("TaxDets") || [];
        var TaxStru = fmcgLocalStorage.getData("ProdTaxDets") || [];
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        TBillAmt = 0;
        TtxAmt = 0;
        TDisc = 0;

       /* BTPrinter.ClosePrinter(function(data) {
            console.log("Printer closed...");
        }, function(err) {
            console.log("Print Err");

        });*/

        for (il = 0; il < $scope.InvData.productSelectedList.length; il++) {

            $scope.InvData.productSelectedList[il].sample_qty = $scope.InvData.productSelectedList[il].Rate * $scope.InvData.productSelectedList[il].rx_qty;
            FDta = TaxStru.filter(function(a) {
                return (a.Product_Code == $scope.InvData.productSelectedList[il].product);
            });
            $scope.InvData.productSelectedList[il].taxDet = [];
            txAmt = 0;
            $scope.InvData.productSelectedList[il].pQVal = $scope.InvData.productSelectedList[il].sample_qty;
            for (ij = 0; ij < FDta.length; ij++) {
                itm = {};
                itm.txId = FDta[ij].Tax_Id;
                iFDta = $scope.taxDets.filter(function(a) {
                    return (a.Tax_Id == itm.txId);
                });
                itm.txName = iFDta[0].name;
                itm.txCalVal = iFDta[0].Value;
                itm.txAmt = $scope.InvData.productSelectedList[il].sample_qty * (itm.txCalVal / 100);
                $scope.InvData.productSelectedList[il].taxDet.push(itm);
                txAmt += itm.txAmt;
            }
            $scope.InvData.productSelectedList[il].TaxAmt = txAmt;
            $scope.InvData.productSelectedList[il].Disc = (($scope.InvData.productSelectedList[il].sample_qty) * ($scope.InvData.productSelectedList[il].discount / 100)); //+ txAmt

            TBillAmt += $scope.InvData.productSelectedList[il].sample_qty;
            TtxAmt += $scope.InvData.productSelectedList[il].TaxAmt;
            TDisc += $scope.InvData.productSelectedList[il].Disc;
        }
        $scope.InvData.BillAmt = TBillAmt;
        $scope.InvData.TaxAmt = TtxAmt;
        $scope.InvData.Disc = TDisc;
        $scope.InvData.NetAmt = (TBillAmt + TtxAmt) - TDisc;
        if ($scope.cComputer) {
            tDate = new Date();
            $scope.InvData.BillDt = tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate() + " " + tDate.getHours() + ":" + tDate.getMinutes() + ":" + tDate.getSeconds();

        } else {
            window.sangps.getDateTime(function(tDate) {
                tDate = new Date(tDate);
                $scope.InvData.BillDt = tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate() + " " + tDate.getHours() + ":" + tDate.getMinutes() + ":" + tDate.getSeconds();
            });
        }
        $("#vwTaxDet").hide();
        $scope.calcInvTotal = function(item) {
            pVal = item.sample_qty;
            item.sample_qty = item.Rate * item.rx_qty;
            FDta = item.taxDet;
            txAmt = 0;
            PtxAmt = 0;
            for (ij = 0; ij < FDta.length; ij++) {
                PtxAmt += pVal * (FDta[ij].txCalVal / 100);
                item.taxDet[ij].txAmt = item.sample_qty * (FDta[ij].txCalVal / 100);
                txAmt += item.taxDet[ij].txAmt;
            }
            item.TaxAmt = txAmt;
            pDisc = item.Disc;
            item.Disc = ((item.sample_qty) * (item.discount / 100)); //+ txAmt

            $scope.InvData.BillAmt = ($scope.InvData.BillAmt - pVal) + item.sample_qty;
            $scope.InvData.TaxAmt = ($scope.InvData.TaxAmt - PtxAmt) + txAmt;
            $scope.InvData.Disc = ($scope.InvData.Disc - pDisc) + item.Disc;
            $scope.InvData.NetAmt = ($scope.InvData.BillAmt + $scope.InvData.TaxAmt) - $scope.InvData.Disc;
           if(item.InvFlag==undefined){
             item.InvFlag=0;
           }
            $scope.InvData.InvFlag = item.InvFlag;
        }

        $scope.openTaxSelect = function(item) {
            $scope.cItem = item;
            $("#vwTaxDet").show();
        }
        $scope.SetTaxDet = function(sItem) {
            $scope.cItem.ClaimType = sItem.id;
            $scope.cItem.ClaimTypeNm = sItem.name;
            $("#vwTaxDet").hide();
        }
        $scope.CloseDiv = function(x) {
            $("#vwTaxDet").hide();
        }
        $scope.PrintBill = function(PrnData) {
            billDet = {};
            console.log('Printing..');
            console.log(JSON.stringify($scope.InvData));

            var Stks = fmcgLocalStorage.getData("stockist_master_" + $scope.sfCode) || [];
            billDet.CmpName = $scope.CompanyName;
            aStk = Stks.filter(function(a) {
                    return (a.id == PrnData.stockist_code);
                })
                /*if (aStk.length > 0) {
                    billDet.Add1 = aStk[0].Addr1;
                    billDet.Add2 = aStk[0].Addr2;
                    billDet.City = aStk[0].City + "-" + aStk[0].Pincode;
                    billDet.GSTN = aStk[0].GSTN;
                }
                else {*/
            billDet.Add1 = $scope.Addr1;
            billDet.Add2 = $scope.Addr2;
            billDet.City = $scope.City + "-" + $scope.Pincode;
            billDet.Phone = $scope.Phone || "";
            billDet.GSTN = $scope.GSTN;
            // }

            billDet.BillNo = PrnData.BillNo || "";
            billDet.BillDt = PrnData.BillDt || "";

            billDet.name = PrnData.name;
            billDet.address = PrnData.address || "";
            billDet.CPhone = PrnData.Mobile_Number || "";
            billDet.TotVal = PrnData.BillAmt.toFixed(2);
            billDet.GTot = PrnData.NetAmt.toFixed(2);
            billDet.BillItems = PrnData.productSelectedList;
            for (il = 0; il < billDet.BillItems.length; il++) {
                billDet.BillItems[il].HSN = billDet.BillItems[il].HSN || "";
                billDet.BillItems[il].sample_qty = parseFloat(billDet.BillItems[il].sample_qty).toFixed(2);
                FDta = billDet.BillItems[il].taxDet;
                for (ij = 0; ij < FDta.length; ij++) {
                    FDta[ij].txAmt = parseFloat(FDta[ij].txAmt).toFixed(2);
                }
                billDet.BillItems[il].Rate = parseFloat(billDet.BillItems[il].Rate).toFixed(2);
            }
            billDet.TaxAmt = PrnData.TaxAmt.toFixed(2);
            billDet.Disc = PrnData.Disc.toFixed(2);
            console.log($scope.lSettings.MyPrinterName);
            console.log(JSON.stringify(billDet));
            BTPrinter.printBill(function(data) {
                console.log(data);
            }, function(err) {
                console.log("Print Err");

            }, billDet, $scope.lSettings.MyPrinterName);

        }
        $scope.SaveInvoice = function(flag) {
            if (!flag) flag = 0;
            data = {};
            $scope.InvData.vtime = $scope.dateFormat("YYYY-MM-dd hh:mn:ss", $scope.InvData.vtime);
            $scope.InvData.BillDt = $scope.dateFormat("YYYY-MM-dd hh:mn:ss", $scope.InvData.BillDt);
            fmcgAPIservice.addMAData('POST', 'dcr/save', "30", $scope.InvData).success(function(response) {
                if (response.success) {
                    Toast("Invoice Submited Successfully");
                    $scope.InvData.BillNo = response.InvNo;
                    $scope.InvData.InvFlag = 3;
                    if (flag == 1) $scope.PrintBill($scope.InvData);
                    $ionicLoading.hide();
                } else {
                    Toast(response.msg);
                    $ionicLoading.hide();
                }
                $state.go('fmcgmenu.home');

            }).error(function() {
                Toast("No Internet Connection! try again later");
                $ionicLoading.hide();
            });

        }
        $scope.PrintInvoice = function() {
            if ($scope.InvData.InvFlag <= 0)
                $scope.SaveInvoice(1);
            else
                $scope.PrintBill($scope.InvData);
        }
    }])
    .controller('callPreviewCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', '$ionicModal', '$ionicScrollDelegate', function($rootScope, $scope, $state, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal, $ionicScrollDelegate) {
        $scope.$parent.navTitle = "Call Preview";

lpath=window.localStorage.getItem("logo");
        console.log("Logo Path : "+lpath);
        if(lpath!=null){
            document.getElementById("cLogo").src =lpath;
        }

        if($scope.SlTyp==3){
           $scope.PrvData.name=$scope.PrvData.stockist_name;
        }

        console.log("EKEY" + $scope.EKEY);
 $.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyCyMzzgResJNYCYcShy46DlG26ZmQRvbGI&libraries=places');
      $scope.PrvData.Address="";     
 //var latlng = {lat: $scope.PrvData.lat, lng:$scope.PrvData.long};

 if($scope.PrvData.lat!=undefined && $scope.PrvData.lat.length>0 && $scope.PrvData.lat!=''){
    var latlng = {lat: parseFloat($scope.PrvData.lat), lng: parseFloat($scope.PrvData.long)};

        var geocoder = new google.maps.Geocoder();

         geocoder.geocode({'location': latlng}, function(results, status) {

        if (status == google.maps.GeocoderStatus.OK) {
             
                $scope.PrvData.Address=results[0].formatted_address;
            } 
        }); 


 }



        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;

        $scope.CnvtInvoice = function() {
            console.log("Invoice Entry");
            $scope.$parent.InvData = angular.copy($scope.PrvData);
            $state.go('fmcgmenu.invEntry');
        }


        console.log('Orientation is ' + screen.orientation.type);
        //alert( screen.orientation.type);
         $scope.PrvData.a=0;
        if(screen.orientation.type=="portrait-primary"){
             $scope.PrvData.a=2;
        /* alert(screen.orientation.type);
         alert($scope.PrvData.a);*/
        }else{
             $scope.PrvData.a=1;
        }
        screen.orientation.onchange = function(){
            console.log(screen.orientation.type);
           
            if(screen.orientation.type=="portrait-primary"){
                 $scope.PrvData.a=2;
                /* alert(screen.orientation.type);
         alert($scope.PrvData.a);*/
            }else{
              
                 $scope.PrvData.a=1;
                 /*alert(screen.orientation.type);
         alert($scope.PrvData.a);*/
            }
        };

/*if(window.innerHeight > window.innerWidth){
    alert("Please use Landscape!");
}*/



        fmcgAPIservice.addMAData('POST', 'dcr/save', "41", $scope.EKEY).success(function(response) {
            if (response.success) {


                delete $scope.EKEY;
if(response.Output!=undefined && response.Output.length>0 ){
$scope.PrvData.orderValue = response.Output[0].Order_Value;
 $scope.PrvData.Trans_Sl_No=response.Output[0].Trans_Sl_No;
}
                
               


            } else {
                delete $sc
                Toast(response.msg);

            }


        }).error(function() {
            Toast("No Internet Connection! try again later");
            $ionicLoading.hide();
        });
        $scope.PrintOrder = function() {

            var PrnData = $scope.PrvData;
            billDet = {};
            console.log(PrnData);
            var Stks = fmcgLocalStorage.getData("stockist_master_" + $scope.sfCode) || [];
            billDet.CmpName = $scope.CompanyName;
            aStk = Stks.filter(function(a) {
                    return (a.id == PrnData.stockist_code);
                })
                /*if (aStk.length > 0) {
                    billDet.Add1 = aStk[0].Addr1;
                    billDet.Add2 = aStk[0].Addr2;
                    billDet.City = aStk[0].City + "-" + aStk[0].Pincode;
                    billDet.GSTN = aStk[0].GSTN;
                }
                else {*/
            billDet.Add1 = $scope.Addr1;
            billDet.Add2 = $scope.Addr2;
            billDet.City = $scope.City + "-" + $scope.Pincode;
            billDet.GSTN = $scope.GSTN;
            billDet.Phone = $scope.Phone || "";
            // }

            billDet.BillNo = PrnData.Order_No || "";
            billDet.BillDt = PrnData.BillDt || "";

            billDet.name = PrnData.name;
            billDet.address = PrnData.address || "";
            billDet.CPhone = PrnData.Mobile_Number || "";
            billDet.TotVal = parseFloat(PrnData.BillAmt).toFixed(2);
            billDet.GTot = parseFloat(PrnData.NetAmt).toFixed(2);
            billDet.BillItems = PrnData.productSelectedList;
            for (il = 0; il < billDet.BillItems.length; il++) {
                billDet.BillItems[il].HSN = billDet.BillItems[il].HSN || "";
                billDet.BillItems[il].sample_qty = parseFloat(billDet.BillItems[il].sample_qty).toFixed(2);
                billDet.BillItems[il].Rate = parseFloat(billDet.BillItems[il].Rate).toFixed(2);
            }
            billDet.Disc = parseFloat(PrnData.Disc).toFixed(2);
            console.log("Print Start");
            console.log(JSON.stringify(billDet));


            BTPrinter.printOrder(function(data) {
                console.log(data);
            }, function(err) {
                console.log("Print Err");

            }, billDet, $scope.lSettings.MyPrinterName);
        }

        $scope.sendWhatsApp = function(x) {
            xelem = $(event.target).closest('.scroll').find('#vstpreview');
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $(xelem).height() + 300);
            $('body').css("overflow", 'visible');
            console.log(x);
            SendPDFtoSocialShare($(xelem), "Order Details",x.name+"-"+x.Territory+"-"+$scope.dateFormat("YYYY-MM-dd", new Date()), "w");

            /*html2canvas($(xelem), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    getCanvas = canvas;
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    //  console.log(canvasImg);
                    window.plugins.socialsharing.shareViaWhatsApp("", canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                }
            });*/
        }
        $scope.sendEmail = function() {
            xelem = $(event.target).closest('.scroll').find('#vstpreview');
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $(xelem).height() + 300);
            $('body').css("overflow", 'visible');

            SendPDFtoSocialShare($(xelem), "Order Details", "Order Details", "e");
            /*
            html2canvas($(xelem), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    window.plugins.socialsharing.share('', 'New Order', canvasImg, '');
                }
            });
            */
        }
    }])
    .controller('myTodyPlCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$filter', '$timeout', function($rootScope, $scope, $state,$ionicPopup, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $filter, $timeout) {
        $scope.$parent.navTitle = $scope.PlanCap;
        $scope.clearData();
        $scope.remarks = "";
        $scope.ShowItemCnt = 0;
        $scope.CheckSelfie=0;
        // $scope.fmcgData.DCRToday = 0;
        $scope.myRegex = /[a-zA-Z]{4}[0-9]{6,6}[a-zA-Z0-9]{3}/;
        $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
  
       $scope.setETime = function() {
            if ($scope.cComputer) {
                var tDate = new Date();
                $scope.eTime = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.eTime = tDate;
                });
            }
            $timeout(function() {
                $scope.setETime();
            }, 1000);
        }
        $scope.attendanceView = window.localStorage.getItem("attendanceView");
        if ($scope.attendanceView == undefined) {
            window.localStorage.setItem("attendanceView", 0);
            $scope.attendanceView = 0;
        }

        if ($scope.SrtNd == 0) {
            if ($scope.Myplns.length == 1 && $scope.attendanceView == 0) {
                $state.go('fmcgmenu.tdStart');
            } else {


            }
        }  

            var CurrentDate = new Date();

            if ($scope.TP_ND==1 ){

                fmcgAPIservice.getDataList('POST', 'table/list&CMonth='+ ((CurrentDate.getMonth() + 1)+1)+'&CYr='+CurrentDate.getFullYear(), ["vwTourPlan", '["date","remarks","worktype_code","worktype_name","RouteCode","RouteName","Worked_with_Code","Worked_with_Name","JointWork_Name","Retailer_Code","Retailer_Name"]'], $scope.sfCode)
                                .success(function(response) {
                                  
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("Tour_Plan_Next", response);
                                                            
                 })
                        .error(function() {
                          
                        });
        }
    
          var CurrentDate = new Date();
            
        $scope.data = {};
        $scope.fmcgData.value1 = 0;
        $scope.fmcgData.value2 = 0;
        $scope.BrndSumm.stockist = undefined;
        
                var $fl = 0;
             
        $scope.TodayTPRoute=[];

        if($scope.Mypl.length!=1)
        if ($scope.Mypl.length == 0 || $scope.Mypl == undefined || $scope.TodayTourPlan == 1 || $scope.Mypl.length>0) {
            var tr = fmcgLocalStorage.getData("Tour_Plan") || [];
            var tDate = new Date();

            var tourplanflag = 0;
            for (key in tr) {

        if (tr[key]['date'] == $scope.dateFormat("YYYY-MM-dd", tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate())) {

            $scope.CheckSelfie=1;

            $scope.Mypln.worktype = {};
            $scope.Mypln.worktype.selected = {};
            $scope.Mypln.worktype.selected.id = tr[key]["worktype_code"];

            $scope.Mypln.subordinate = {};
            $scope.Mypln.subordinate.selected = {};
            if ($scope.view_MR == 1) {
                $scope.Mypln.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Mypln.subordinate.selected.id = tr[key]["SF_Code"];
            }

            if(tr[key].RouteCode!=undefined){
                
                routecode=tr[key].RouteCode.split('$$')
                 for (keyy in routecode) {
                      var tourplanroute = {};
                      tourplanroute.tourplanroute=routecode[keyy]
                    $scope.TodayTPRoute.push(tourplanroute);
                     }
               
            }

            
            $scope.worktypess = fmcgLocalStorage.getData("mas_worktype") || [];

            var Wtyps = $scope.worktypess.filter(function(a) {
                return (a.id == tr[key]["worktype_code"])
            });
            if(Wtyps.length>0){
            $scope.Mypln.worktype.selected.FWFlg = Wtyps[0].FWFlg;
            $scope.Mypln.cluster = {};
            $scope.Mypln.cluster.selected = {};
            if(tr[key]["MultiRoute"]!=undefined){
             $scope.Mypln.cluster.selected.id =tr[key]["MultiRoute"][0].jointwork;
          
            }else{
            $scope.Mypln.cluster.selected.id=tr[key]["RouteCode"].replace('$$','').split('$$')[0];
       
             }
            $scope.Mypln.stockist = {};
            $scope.Mypln.stockist.selected = {};
            $scope.data.worktype = $scope.Mypln.worktype.selected.id;
            $scope.data.eTime = $scope.eTime;
            $scope.data.FWFlg = $scope.Mypln.worktype.selected.FWFlg;
            $scope.data.subordinateid = ($scope.view_MR == 1) ? userData.sfCode : tr[key]["SF_Code"];
            if ((',F,IH,DH,').indexOf(',' + Wtyps[0].FWFlg + ',') > -1) {
                $scope.Mypln.cluster.name = tr[key]["RouteRouteNameCode"];

                // $scope.data.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList;
                $scope.data.dcrtype = "";
                $scope.data.custid = "";
                $scope.data.custName = "";
                $scope.data.address = "";
                $scope.data.Confirmed = tr[key]["Confirmed"];

                $scope.data.stockistid = tr[key]["Worked_with_Code"];
                $scope.data.clusterid = $scope.Mypln.cluster.selected.id;
                if (tr[key]["JointWork_Name"] != null) {
                    var string = tr[key]["JointWork_Name"];
                    if (!string.contains("$$")) {
                        var jontWorkData = {};
                        jontWorkData.jointworkname = string;
                        $scope.data.jontWorkSelectedList = [];
                        $scope.data.jontWorkSelectedList.push(jontWorkData);
                    } else {
                        var splits = string.split('$$', 3);
                        $scope.data.jontWorkSelectedList = [];
                       
                        for (keyy in splits) {
                            var jontWorkData = {};
                            jontWorkData.jointworkname = splits[keyy];
                            $scope.data.jontWorkSelectedList.push(jontWorkData);
                        }
                    }
                }
            }
            

            $scope.data.remarks = tr[key]["remarks"];
            $scope.Myplns = [];
            $scope.Myplns.push($scope.data);

            //window.localStorage.removeItem("mypln");
            $scope.Mypl = $scope.Myplns;
            }


        }
    }
}



  $scope.omit_special_char=function(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
}



        $scope.setETime();
        if ($scope.Mypl.length > 0) {
            if ($scope.Mypl[0].worktype != undefined) {
                $scope.Mypln.worktype = {};
                $scope.Mypln.worktype.selected = {};
                $scope.Mypln.worktype.selected.id = $scope.Mypl[0].worktype;
                $scope.Mypln.worktype.selected.FWFlg = $scope.Mypl[0].FWFlg;
                $scope.Mypln.subordinate = {};
                $scope.Mypln.subordinate.selected = {};
                $scope.Mypln.dcrtypes = {};
                $scope.Mypln.dcrtypes.selected = {};
                $scope.Mypln.dcrtypes.name = $scope.Mypl[0].dcrtype;
                $scope.Mypln.dcrtypes.selected.id = $scope.Mypl[0].dcrtype;
              
                /*if($scope.Mypl[0].Confirmed!=undefined){
                 $scope.tourplanconfirm= $scope.Mypl[0].Confirmed;

                }*/
                if ($scope.view_MR == 1) {
                    $scope.Mypln.subordinate.selected.id = $scope.sfCode;
                } else {
                    $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
                }
                if ($scope.Mypl[0].FWFlg == 'F' || $scope.Mypl[0].FWFlg == 'DH' || $scope.Mypl[0].FWFlg == 'IH') {
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
                $scope.Mypln.cluster.name = $scope.Mypl[0].ClstrName;
                $scope.Mypln.stockist = {};
                $scope.Mypln.stockist.selected = {};
                $scope.Mypln.stockist.selected.id = $scope.Mypl[0].stockistid;
                $scope.Mypln.stockist.name = $scope.Mypl[0].stkName;
                $scope.Mypln.doctor = {};
                $scope.Mypln.doctor.selected = {};
                $scope.Mypln.doctor.selected.id = $scope.Mypl[0].custid;
                $scope.Mypln.doctor.name = $scope.Mypl[0].custName;
                $scope.Mypln.doctor.address = $scope.Mypl[0].address;
                $scope.Mypln.SuperStokit={};
                $scope.Mypln.SuperStokit.selected={};
                $scope.Mypln.SuperStokit.selected.id=$scope.Mypl[0].Sprstk;
                $scope.Mypln.remarks = $scope.Mypl[0].remarks;
                $scope.$parent.fmcgData['jontWorkSelectedList'] = [];
                if ($scope.Mypl[0].worked_with_code != undefined && $scope.Mypl[0].worked_with_code != '') {
                    var response2 = $scope.Mypl[0].worked_with_code.split("$$");
                    var jw = $scope.Mypl[0].worked_with_name.split(",");
                    for (var m = 0, leg = response2.length; m < leg; m++) {
                        $scope.$parent.fmcgData['jontWorkSelectedList'] = $scope.$parent.fmcgData['jontWorkSelectedList'] || [];
                        var pTemp = {};
                        pTemp.jointwork = response2[m].toString();
                        pTemp.jointworkname = jw[m].toString();
                        if (pTemp.jointwork.length !== 0)
                            $scope.$parent.fmcgData['jontWorkSelectedList'].push(pTemp);
                    }
                    $scope.Myplns[0].jontWorkSelectedList = $scope.$parent.fmcgData['jontWorkSelectedList'];
                } else
                    $scope.$parent.fmcgData['jontWorkSelectedList'] = $scope.Mypl[0].jontWorkSelectedList;

                $fl = 1;
            }

        } else {
            window.localStorage.removeItem("DCRToday");
            fmcgLocalStorage.createData("DCRToday", 0);
        }
        $scope.fmcgData.DCRToday = fmcgLocalStorage.getData("DCRToday")

        $scope.$parent.fmcgData.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList || [];
        $scope.addProduct = function(selected) {
            var jontWorkData = {};
            jontWorkData.jointwork = selected;
            $scope.$parent.fmcgData.jontWorkSelectedList.push(jontWorkData);
        };
        $scope.gridOptions = {
            data: 'fmcgData.jontWorkSelectedList',
            rowHeight: 50,
            enableRowSelection: false,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: false,
            columnDefs: [{
                field: 'jointworkname',
                displayName: 'Joint Work',
                enableCellEdit: false,
                cellTemplate: 'partials/jointworkCellTemplate.html'
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50
            }]
        };
        $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
        };
        if ($fl == 0) {

            $scope.Mypln.worktype = {};
            $scope.Mypln.worktype.selected = {};
            var Wtyps = $scope.worktypes.filter(function(a) {
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
        $scope.save = function() {
            var temp = window.localStorage.getItem("loginInfo");
            var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
            if($scope.GeoChk==0){
                startGPS();
            }
            
            $scope.data.worktype = $scope.Mypln.worktype.selected.id;
            $scope.data.eTime = $scope.eTime;
            $scope.data.FWFlg = $scope.Mypln.worktype.selected.FWFlg;
            $scope.data.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList;
            $scope.data.dcrtype = "";
            $scope.data.custid = "";
            $scope.data.custName = "";
            $scope.data.address = "";



            if ($scope.Mypln.worktype.selected.FWFlg == 'DH') {
                if ($scope.view_MR != 1 && ($scope.Mypln.subordinate == undefined || $scope.Mypln.subordinate.selected.id == '')) {
                    Toast('Select the field force...');
                    return false;
                }
                $scope.data.subordinateid = ($scope.view_MR == 1) ? userData.sfCode : $scope.Mypln.subordinate.selected.id;

            } else if ($scope.Mypln.worktype.selected.FWFlg == 'F' || $scope.Mypln.worktype.selected.FWFlg == 'IH') {
                if ($scope.Mypln.worktype.selected.FWFlg == 'IH') {
                    if ($scope.Mypln.doctor == undefined || $scope.Mypln.doctor.selected == undefined || $scope.Mypln.doctor.selected.id == '') {
                        Toast('Select the InHouse Retail / Place...');
                        return false;
                    }

                    $scope.data.custid = $scope.Mypln.doctor.selected.id;
                    $scope.data.custName = $scope.Mypln.doctor.name;
                    $scope.data.address = $scope.Mypln.doctor.address;

                }
                /*if($scope.view_MR != 1 && $scope.Supplier_Master==1 && $scope.Mypln.SuperStokit==undefined){
                Toast('Select the SuperStokit...');
                            return false;
                }
*/

                if ($scope.view_MR != 1 && ($scope.Mypln.subordinate == undefined || $scope.Mypln.subordinate.selected.id == '')) {
                    Toast('Select the field force...');
                    return false;
                }
                if ($scope.DistBased == 1) {
                    if ($scope.Mypln.stockist == undefined || $scope.Mypln.stockist.selected.id == '') {
                        Toast("Select " + $scope.StkCap);
                        return false;
                    }
                } else {
                    $scope.Mypln.stockist = {};
                    $scope.Mypln.stockist.selected = {};
                    $scope.Mypln.stockist.selected.id = '';
                }
                if ($scope.Mypln.cluster == undefined || $scope.Mypln.cluster.selected == undefined || $scope.Mypln.cluster.selected.id == undefined) {
                    Toast('Select the ' +$scope.StkRoute);
                    return false;
                }
                /*  if ($scope.edit_sumry == 0 && ($scope.Mypln.dcrtypes == undefined || $scope.Mypln.dcrtypes.selected == undefined || $scope.Mypln.dcrtypes.selected.id == '')) {
                      Toast('Select the Entry Mode...');
                      return false;
                  }
                  if ($scope.edit_sumry == 0)
                      $scope.data.dcrtype = $scope.Mypln.dcrtypes.selected.id;
                  else
                      $scope.data.dcrtype = '';



                      */



if($scope.CheckSelfie!=undefined && $scope.CheckSelfie==1){

        var Tproute =  $scope.TodayTPRoute.filter(function(a) {
                            return (a.tourplanroute == $scope.Mypln.cluster.selected.id)
                });

            if(Tproute.length==0){
            temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
      
            fmcgAPIservice.getDataList('POST', 'get/TpRouteNotify', [])
            .success(function(response) {
               
            }).error(function() {
                
            });



            }

        }


                $scope.data.subordinateid = ($scope.view_MR == 1) ? userData.sfCode : $scope.Mypln.subordinate.selected.id;
                $scope.data.clusterid = $scope.Mypln.cluster.selected.id;
                $scope.data.stockistid = $scope.Mypln.stockist.selected.id;



                if($scope.view_MR != 1 && $scope.Supplier_Master==1 &&$scope.Mypln.SuperStokit!=undefined){
                     $scope.data.Sprstk=$scope.Mypln.SuperStokit.selected.id;
                }
                if($scope.$parent.Myplns.length==0 && $scope.Selfie==1){
                        $scope.data.photosList=$scope.ProfilephotosList;
                }
                    if ($scope.DistBased == 1) {
                    console.log($scope.Mypln.stockist.selected.id);
                    console.log($scope.stockists);
                    console.log($scope.Mypln.stockist.name);
                    if ($scope.Mypln.stockist.name == undefined)
                        $scope.Mypln.stockist.name = $filter('getValueforID')($scope.Mypln.stockist.selected.id, $scope.stockists).name;
                    $scope.data.stkName = $scope.Mypln.stockist.name;
                }
                if ($scope.Mypln.cluster.name == undefined)
                    $scope.Mypln.cluster.name = $filter('getValueforID')($scope.Mypln.cluster.selected.id, $scope.myTpTwns).name;
                $scope.data.ClstrName = $scope.Mypln.cluster.name;
            } else {
                $scope.data.subordinateid = '';
                $scope.data.clusterid = '';
                $scope.data.ClstrName = '';
                $scope.data.stockistid = '';
                $scope.data.dcrtype = '';
            }
            $scope.data.remarks = $scope.Mypln.remarks;
            if (_currentLocation.Latitude != undefined)
                $scope.data.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;
            var tDate = new Date();
    var cdate=$scope.dateFormat("YYYY-MM-dd", tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate());

/*
if( (tDate.getMonth() + 1)== new Date($scope.TP_Remainder_Date).getMonth()+1){*/


        if($scope.data.location==undefined || $scope.data.location=='' ){
            $scope.data.location=$scope.fmcgData.location;
        }
        NoofDays=fmcgLocalStorage.getData("Tour_Plan_Next") || [];
            if($scope.TP_ND==1){

            if((cdate >= $scope.TP_Remainder_Date)){
            if((cdate >$scope.TP_Mandatory_ND) ||(cdate>$scope.TP_Mandatory_ND)){
                     $scope.MTPEnty.Month = (tDate.getMonth() + 1)+1;
                        $scope.MTPEnty.Year = tDate.getFullYear();
                      

                        if(new Date(tDate.getFullYear(), (tDate.getMonth() + 1)+1, 0).getDate()!=(NoofDays.length)){
                             $scope.MTPEnty.Month =  (tDate.getMonth() + 1)+1;
                             $scope.MTPEnty.Year = $scope.CYear;

                              $state.go('fmcgmenu.TPEntry');
                             //var daysmissing=(new Date(tDate.getFullYear(), (tDate.getMonth() + 1), 0).getDate()-(NoofDays.length))
                            Toast('Enter The Tour Plan First ');
                                return false;
                        }
                        
            }

                if ((cdate>=$scope.TP_Remainder_Date) || (cdate>= $scope.TP_Remainder_Date && $scope.TP_Mandatory_ND >= cdate)){
                   Toast('Remainder Enter the Tour plan');
                    
                }
            }

            }
               




            if($scope.Mypl.length>0 && $scope.Mypl!=undefined && $scope.Mypl[0].FWFlg=='L' ){
                Toast('Leave Already Posted');
                return false;
            }






/*if($scope.$parent.Myplns.length==0 && userData.Selfie==1 && $scope.CheckSelfie==0&& ($scope.photosList==undefined ||$scope.photosList.length==0 )){
        $scope.openCameraProfile();
        return false;
      }*/




 
            $ionicLoading.show({
                template: 'Saving...'
            });
            window.localStorage.removeItem("mypln");
            window.localStorage.removeItem("myplnQ");
            $scope.Myplns = [];
            $scope.Myplns.push($scope.data);
             $scope.$parent.TodayTourPlan=0;
               

            $scope.Previous=[];
            $scope.Previous  = fmcgLocalStorage.getData("TodayTotalRoute") || [];


       
           
            $scope.Previous.push($scope.Mypln.cluster.selected.id);
              fmcgLocalStorage.createData("TodayTotalRoute",$scope.Previous);



            fmcgAPIservice.getPostData('POST', 'save/ver&ver=6.0.9', []).success(function(response) {}).error(function() {});

            fmcgAPIservice.addMAData('POST', 'dcr/save', "3", $scope.data).success(function(response) {
                if (response.success) {
                     fmcgLocalStorage.createData("mypln", $scope.Myplns);
                    if ($scope.data.FWFlg != "F") {
                        if ($scope.edit_sumry == 0) {
                            $scope.Mypln.dcrtypes = {};
                            $scope.Mypln.dcrtypes.selected = {};
                            $scope.Mypln.dcrtypes.selected.id = 'Retail Wise';
                            $scope.Myplns[0]['dcrtype'] = 'Retail Wise';
                            fmcgLocalStorage.createData("DCRToday", 1);
                            $scope.DCRToday = 1;
                            $scope.PlanCap = "Switch Route";
                        } else
                            fmcgLocalStorage.createData("DCRToday", 0);
                        $scope.DCRToday = 0;
                    }
                     console.log("Before_Profile"+$scope.ProfilephotosList[0]+"MYPLAN"+$scope.$parent.Myplns.length);           

                
                    if($scope.Selfie==1){
                        $scope.ProfilephotosList=[];
                         console.log("myday plan zero"+$scope.$parent.Myplns.length);
                }
                   
               

                    Toast("My Today Plan Submited Successfully");
                    $ionicLoading.hide();
                } else {
                    Toast(response.msg);
                    $ionicLoading.hide();
                }
                $scope.$parent.Myplns = fmcgLocalStorage.getData("mypln") || [];
                $scope.PlanChk = 0;
                $scope.$parent.PlanChk = 0;
                // $state.go('fmcgmenu.home');

                if ($scope.SrtNd == 0) {
                    if ($scope.Mypl.length > 0) {
                        $state.go('fmcgmenu.home');
                    } else {
                        $state.go('fmcgmenu.tdStart');
                    }
                } else {
                    $state.go('fmcgmenu.home');

                }

            }).error(function() {
                if ($scope.data.FWFlg != "F") {
                    if ($scope.edit_sumry == 0) {
                        fmcgLocalStorage.createData("DCRToday", 1);
                        $scope.DCRToday = 1;
                    } else {
                        fmcgLocalStorage.createData("DCRToday", 0);
                        $scope.DCRToday = 0;
                    }
                }

                window.localStorage.setItem("myplnQ", JSON.stringify($scope.Myplns));
                Toast("No Internet Connection! My Day Plan Saved in Outbox");
                $ionicLoading.hide();
                $state.go('fmcgmenu.home');
            });
        };

    if(($scope.$parent.Myplns.length==0 || ($scope.$parent.Myplns.length>0 && $scope.CheckSelfie==1))&& userData.Selfie==1  && ($scope.photosList==undefined ||$scope.photosList.length==0 ) && (navigator.platform != "Win32")){
        $scope.openCameraProfile();
        return false;
      }

    }])
    .controller('EditSummaryCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading) {
        $scope.$parent.navTitle = "Edit Summary Details";
        $ionicLoading.show({
            template: 'Loading...'
        });

        $scope.data = {};
        $scope.fmcgData.value1 = 0;
        $scope.fmcgData.value2 = 0;
        fmcgAPIservice.getDataList('POST', 'DaySummaryDet', [])
            .success(function(response) {
                dcrSummary = response['dcrSummary'];
                $scope.data.pcalls = dcrSummary[0].productive_calls;
                $scope.data.upcalls = dcrSummary[0].unproductive_cals;
                $scope.data.tcalls = dcrSummary[0].total_cals;
                $scope.data.pob_value = dcrSummary[0].pob_value;
                if (dcrSummary[0].editFlag == 1) {
                    $scope.data.DCR_TLSD = dcrSummary[0].dcr_tlsd;
                    $scope.data.DCR_LPC = dcrSummary[0].dcr_lpc;
                    brandnames = dcrSummary[0]['brand_ec'];
                    brandids = dcrSummary[0]['brand_id'];
                    //  $scope.data.brandwise = dcrSummary[0]['brandwise'];
                    var names = brandnames.split("#");
                    var ids = brandids.split("#");

                    for (var m = 0, leg = names.length; m < leg; m++) {
                        if (names[m].length > 0) {
                            $scope.brandwise = $scope.brandwise || [];
                            var pTemp = {};
                            var id = ids[m].split('~');
                            var name = names[m].split('~');
                            pTemp.product_brd_code = id[0].toString().trim();
                            pTemp.product_brd_sname = name[0].toString();
                            pTemp.RetailCount = name[1];
                            $scope.brandwise.push(pTemp);
                        }
                    }
                    $scope.data.brandwise = $scope.brandwise;
                } else {
                    $scope.data.DCR_TLSD = response['DCR_TLSD'][0]['total_lines'];
                    $scope.data.DCR_LPC = response['DCR_LPC'].length;
                    $scope.data.brandwise = response['brandwise']
                }
                for (i = 0; i < $scope.brand.length; i++) {
                    val = 0;
                    for (j = 0; j < $scope.data.brandwise.length; j++) {
                        if ($scope.brand[i]['product_brd_code'] == $scope.data.brandwise[j]['product_brd_code'])
                            val = 1;
                    }
                    if (val == 0) {
                        var productData = {};
                        productData.product_brd_code = $scope.brand[i].product_brd_code;
                        productData.product_brd_sname = $scope.brand[i].product_brd_sname;
                        productData.RetailCount = 0;
                        $scope.data.brandwise.push(productData);

                    }
                }
                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });



        $scope.gridOptions = {
            data: 'data.brandwise',
            rowHeight: 50,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            enableRowSelection: false,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: [{
                field: 'product_brd_sname',
                displayName: 'Brand Name',
                enableCellEdit: false,
                cellTemplate: 'partials/productCellTemplate.html'
            }, {
                field: 'RetailCount',
                displayName: "EC",
                enableCellEdit: true,
                editableCellTemplate: "partials/cellEditTemplate.html",
                width: 60
            }]
        };
        $scope.prod = function() {

            if (this.data.pcalls == undefined)
                this.data.pcalls = 0;
            else if (this.data.pcalls == null)
                this.data.pcalls = "";
            else if (this.data.upcalls == undefined)
                this.data.upcalls = 0;
            if (this.data.tcalls < this.data.pcalls) {
                this.data.pcalls = "";
                Toast("Enter less than Total Order Value..");
                return false;

            } else if (this.data.tcalls - this.data.pcalls >= 0)
                $scope.data.upcalls = this.data.tcalls - this.data.pcalls;

        }
        $scope.tprod = function() {
            if (this.data.pcalls == undefined)
                this.data.pcalls = 0;
            else if (this.data.upcalls == undefined)
                this.data.upcalls = 0;
            if (this.data.tcalls - this.data.pcalls >= 0)
                $scope.data.upcalls = this.data.tcalls - this.data.pcalls;

        }
        $scope.total = function() {
            if (this.data.pcalls == undefined)
                $scope.data.tcalls = 0 + this.data.upcalls;
            else if (this.data.upcalls == undefined)
                $scope.data.tcalls = this.data.pcalls + 0;
            else
                $scope.data.tcalls = this.data.pcalls + this.data.upcalls;
        }
        $scope.update = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.addMAData('POST', 'dcr/save', "23", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("Summary Updated Successfully");
                    $state.go('fmcgmenu.dcr1');
                    $scope.data = {};
                    $ionicLoading.hide();

                }
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        };


    }])
    .controller('DyRmksCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
        $scope.$parent.navTitle = "Day Activity Remarks";
        $scope.data = {};
        $scope.dyRmks = {};

        $scope.MyDyRmks = fmcgLocalStorage.getData("MyDyRmks") || [];
        if ($scope.MyDyRmks.length == 0) {
            fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_report",
                '["remarks","Half_Day_FW as Halfday"]', , '["Activity_Date=convert(varchar,getDate(),101)"]'
            ]).success(function(response) {
                $scope.MyDyRmks = response;
                if (response.length && response.length > 0 && Array.isArray(response)) {
                    fmcgLocalStorage.createData("MyDyRmks", response);
                    $scope.dyRmks.remarks = response[0].remarks;
                    $scope.SlHlfDys = response[0].Halfday;
                }
            }).error(function() {
                Toast("No Internet Connection!.");
            });
        } else {
            $scope.dyRmks.remarks = $scope.MyDyRmks[0].remarks;
            $scope.SlHlfDys = $scope.MyDyRmks[0].Halfday;
        }

        $scope.HalfdayWorks = fmcgLocalStorage.getData("halfdayworks") || [];

        $scope.setHlfDy = function(item) {
            for (var di = 0; di < $scope.HalfdayWorks.length; di++) {
                $scope.HalfdayWorks[di].checked = false;
                if ($scope.SlHlfDys.indexOf($scope.HalfdayWorks[di].id) > -1)
                    $scope.HalfdayWorks[di].checked = true;
            }
        }
        if ($scope.HalfdayWorks.length == 0) {
            fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
                '["type_code as id", "Wtype as name"]', , '["isnull(Active_flag,0)=0","isnull(HalfDyNeed,0)=1"]', , , , 0
            ]).success(function(response) {
                $scope.HalfdayWorks = response;
                $scope.setHlfDy();
                if (response.length && response.length > 0 && Array.isArray(response))
                    fmcgLocalStorage.createData("halfdayworks", response);
            });
        } else {
            $scope.setHlfDy();
        }
        $scope.selHlfDy = function(item) {
            if (item.checked) {
                if ($scope.AppEnver != ".Net")
                    if ($scope.SlHlfDys.indexOf(item.id + ',') < 0)
                        $scope.SlHlfDys += item.id + ',';
                    else
                if ($scope.SlHlfDys.indexOf(item.name + ' ( ' + item.id + ' ),') < 0)
                    $scope.SlHlfDys += item.name + ' ( ' + item.id + ' ),';
            } else {
                $scope.SlHlfDys = $scope.SlHlfDys.replace((($scope.AppEnver != ".Net") ? item.id : item.name + ' ( ' + item.id + ' )') + ',', '');
            }
        }
        $scope.save = function() {
            if ($scope.dyRmks.remarks == undefined || $scope.dyRmks.remarks == '') {
                Toast('Enter Day Activity Remarks...')
            } else {
                $scope.data.remarks = $scope.dyRmks.remarks;
                $scope.data.Halfday = $scope.SlHlfDys;

                MyDyRmks = [{}];
                MyDyRmks[0]["remarks"] = $scope.dyRmks.remarks;
                MyDyRmks[0]["Halfday"] = $scope.SlHlfDys
                window.localStorage.removeItem("MyDyRmks");
                fmcgLocalStorage.createData("MyDyRmks", MyDyRmks);

                fmcgAPIservice.updateRemark('POST', 'dcr/updRem', $scope.data).success(function(response) {
                        if (response.success) {
                            Toast("Day Activity Remarks Updated Successfully");
                        } else
                            Toast(response['msg'], true);
                    })
                    .error(function() {
                        window.localStorage.setItem("MyDyRmksQ", JSON.stringify(MyDyRmks));
                        Toast("No Internet Connection! Saved in Outbox");
                        $state.go('fmcgmenu.home');
                    });
                $state.go('fmcgmenu.home');
            }
        }
    }])
    .controller('addULDocCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function($rootScope, $scope, $state, $ionicPopup,$location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
        $scope.$parent.navTitle = "Add " + $scope.NLCap;
        $scope.wlkg_View = $scope.doctors.length;
        $scope.clearData();
        $scope.data = {};
        $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
        if($scope.RetailerphotosList.length>0)
        $scope.RetailerphotosList.shift();
        if($scope.Mypl.length==0){
            Toast('My day plan is Needed');
         $state.go('fmcgmenu.myPlan');
        }
                var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
        $scope.divisionCode = loginInfo.divisionCode.replace(',', '');

        var tempp = window.localStorage.getItem("AddNewRetailer");
        var userDataRET = (tempp != null && tempp.length > 0) ? JSON.parse(tempp) : null;


        $scope.Mypln.subordinate = {};
        $scope.Mypln.subordinate.selected = {};
        if ($scope.view_MR == 1) {
            $scope.Mypln.subordinate.selected.id = $scope.sfCode;
        } else {
            if ($scope.Mypl.length != 0) $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
        }
        $scope.loadDatas(false, '_' + $scope.Mypln.subordinate.selected.id)
            //$scope.clearAll(false, '_' + sRSF);
        $scope.precall.doctor = undefined;
        $scope.Mypln.cluster = {};
        $scope.Mypln.stockist = {};
        $scope.NmCap = $scope.NLCap;
        $scope.CusOrder = $scope.CusOrder;

       $scope.precall.cluster = {}; 
       $scope.precall.cluster.selected = {};
       $scope.precall.cluster.selected.id = $scope.Mypl[0].clusterid;




        if ($scope.Mypl.length != 0) {
            console.log($scope.Mypl[0]);
            if ($scope.view_MR == 1) {
                $scope.Mypln.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
            }
            $scope.Mypln.cluster.selected = {};
            $scope.Mypln.cluster.selected.id = $scope.Mypl[0].clusterid;
            $scope.Mypln.stockist.selected = {}
            $scope.Mypln.stockist.selected.id = $scope.Mypl[0].stockistid;
            console.log(' hhi' + $scope.Mypln.cluster.selected.id);



        }

 $scope.TakePhoto=function(){
    $scope.openCameraRetailer();
 }
   

        $scope.save = function() {
            if ($scope.data.name == "" || $scope.data.name == undefined) {
                Toast('Enter the Name...');
                return false;
            }
            if ($scope.data.address == "" || $scope.data.address == undefined) {
                if ($scope.NdrAddrNd == 1) {
                    Toast('Enter the Address...');
                    return false;
                } else
                    $scope.data.address = "";

            }
           if (($scope.data.gstno == "" &&(','+$scope.RetailerMandatory+',').indexOf(',gs,')>-1 ) ||  ($scope.data.gstno == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',gs,')>-1 )) {
                Toast('Enter the Gstno...');
                return false;
            }
            if (($scope.data.address == "" &&(','+$scope.RetailerMandatory+',').indexOf(',ad,')>-1 ) ||  ($scope.data.address == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',ad,')>-1 )) {
                Toast('Enter the Address...');
                return false;
            }
            if (($scope.data.areaname == "" &&(','+$scope.RetailerMandatory+',').indexOf(',ar,')>-1 ) ||  ($scope.data.areaname == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',ar,')>-1 )) {
                Toast('Enter the Areaname...');
                return false;
            }
           if (($scope.data.cityname == "" &&(','+$scope.RetailerMandatory+',').indexOf(',ct,')>-1 ) ||  ($scope.data.cityname == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',ct,')>-1 )) {
                Toast('Enter the Cityname...');
                return false;
            }
           if (($scope.data.landmark == "" &&(','+$scope.RetailerMandatory+',').indexOf(',ln,')>-1 ) ||  ($scope.data.landmark == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',ln,')>-1 )) {
                Toast('Enter the Landmark...');
                return false;
            }
           if (($scope.data.pincode == "" &&(','+$scope.RetailerMandatory+',').indexOf(',pc,')>-1 ) ||  ($scope.data.pincode == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',pc,')>-1 )) {
                Toast('Enter the Pincode...');
                return false;
            }
           if (($scope.data.contactperson == "" &&(','+$scope.RetailerMandatory+',').indexOf(',cp,')>-1 ) ||  ($scope.data.contactperson == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',cp,')>-1 )) {
                Toast('Enter the Contact Person...');
                return false;
            }

            if($scope.RetailerPhotoNd!=undefined && $scope.RetailerPhotoNd==1 &&  ($scope.RetailerphotosList==undefined || $scope.RetailerphotosList.length==0)){
               Toast('Photo is Needed');

               console.log("PHOTOS"+$scope.RetailerphotosList);
               return false;
            }

            if (($scope.data.phone == "" &&(','+$scope.RetailerMandatory+',').indexOf(',ph,')>-1 ) ||  ($scope.data.phone == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',ph,')>-1 )) {
                Toast('Enter the Phone Number...');
                return false;
            }
            if (($scope.data.contactperson2 == "" &&(','+$scope.RetailerMandatory+',').indexOf(',cp2,')>-1 ) ||  ($scope.data.contactperson2 == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',cp2,')>-1 )) {
                Toast('Enter the Contact Person2...');
                return false;
            }
            if (($scope.data.phone2 == "" &&(','+$scope.RetailerMandatory+',').indexOf(',ph2,')>-1 ) ||  ($scope.data.phone2 == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',ph2,')>-1 )) {
                Toast('Enter the Phone Number2...');
                return false;
            }

           if (($scope.data.designation == "" &&(','+$scope.RetailerMandatory+',').indexOf(',dn,')>-1 ) ||  ($scope.data.designation == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',dn,')>-1 )) {
                Toast('Enter the Designation...');
                return false;
            }

           if (($scope.data.designation2 == "" &&(','+$scope.RetailerMandatory+',').indexOf(',dn2,')>-1 ) ||  ($scope.data.designation2 == undefined &&(','+$scope.RetailerMandatory+',').indexOf(',dn2,')>-1 )) {
                Toast('Enter the Designation2...');
                return false;
            }
        if($scope.data.phone!=undefined){
        if($scope.data.phone.length!=10){
                        Toast('Enter the Valid Phone Number...');
                        return false;
                    }
        }

            

/*if($scope.$parent.Myplns.length==0 && $scope.Selfie==1){
        $scope.data.photosList=$scope.ProfilephotosList;
        console.log("myday plan zero"+$scope.$parent.Myplns.length);
}
    */        $scope.data.cDataID = $scope.Mypln.subordinate.selected.id;
            $scope.data.cluster = {};

            $scope.data.wlkg_sequence = {};
            $scope.data.cluster.selected = {};
            $scope.data.wlkg_sequence.selected = {};

if($scope.RetailerPhotoNd!=undefined && $scope.RetailerPhotoNd==1){
    $scope.data.RetailerphotosList=$scope.RetailerphotosList;
}



            //                    if ($scope.precall.doctor == undefined || $scope.precall.doctor.selected == undefined)
            //                    {
            //                        Toast('Select the Existing Customer');
            //                        return false;
            //                    }
            $scope.data.cluster.selected.id = $scope.Mypln.cluster.selected.id;
            $scope.data.ClstrName = $scope.Mypln.cluster.name;
            if ($scope.CusOrder == 1 && $scope.wlkg_View != 0) {
                if ($scope.precall.doctor == undefined || $scope.precall.doctor.selected == undefined) {
                    Toast('Select the Existing Customer');
                    return false;
                }
                $scope.data.wlkg_sequence.selected.id = $scope.precall.doctor.selected.id;
            }
            $scope.data.qulification = {};
            $scope.data.qulification.selected = {};
            //if ($scope.fmcgData.qulification == undefined || $scope.fmcgData.qulification.selected == undefined) {
            //                        Toast('Select the Qulification...');
            //                        return false;
            // }
            //                    $scope.data.qulification.selected.id = $scope.fmcgData.qulification.selected.id;
            $scope.data.qulification.selected.id = 'samp';
            $scope.data.Ukey = $scope.Mypln.subordinate.selected.id + new Date();
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


        var loc = $scope.fmcgData.location.split(':');
                $scope.data.lat=loc[0];    
                 $scope.data.long=loc[1];
            $scope.data.speciality.selected.id = $scope.fmcgData.speciality.selected.id;

            $scope.data.category = {};
            $scope.data.category.selected = {};
            //$scope.fmcgData.category.selected.id = '';
            //if ($scope.fmcgData.category == undefined || $scope.fmcgData.category.selected == undefined)
            //{
            //    Toast('Select the Category...');
            //    return false;
            //}
            Dtky = new Date();
            $scope.data.category.selected.id = ''; //$scope.fmcgData.category.selected.id;
            $scope.data.DrKeyId = 'N-' + $scope.sfCode + '-' + Dtky.valueOf(); //$scope.fmcgData.category.selected.id;

            fmcgAPIservice.addMAData('POST', 'dcr/save', "2", $scope.data).success(function(response) {
                if (response.success)
                    Toast($scope.NLCap + " Added Successfully");
                $scope.RetailerphotosList=[];
                  $scope.fmcgData.rp=[];
                $scope.data = {};
                if ($scope.AppTyp == 1) {
                    var temp = window.localStorage.getItem("mypln");
                    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;

                    var sRSF = userData[0].subordinateid;
                    $scope.clearIdividual(0, 1, '_' + sRSF);
                } else {
                    $scope.clearIdividual(3, 1);
                }
                $scope.data = {};
                /*   if ($scope.fmcgData.cluster == undefined)
                       $scope.fmcgData.cluster = {};
                   if ($scope.fmcgData.speciality == undefined)
                       $scope.fmcgData.cluster = {};
                   if ($scope.fmcgData.category == undefined)
                       $scope.fmcgData.cluster = {};

                   $scope.fmcgData.cluster.selected = {};
                   $scope.fmcgData.speciality.selected = {};
                   $scope.fmcgData.category.selected = {};
                   */
            }).error(function() {
                var QuDataNew = fmcgLocalStorage.getData("CustAddQ") || [];
                QuDataNew.push($scope.data);
                fmcgLocalStorage.createData("CustAddQ", QuDataNew);
                $scope.OfflineRetailers = fmcgLocalStorage.getData("Offline_Retailer" + $scope.data.cDataID) || [];
                sdata = {
                    "id": $scope.data.DrKeyId,
                    "name": $scope.data.name,
                    "town_code": $scope.data.cluster.selected.id,
                    "town_name": $scope.Mypln.cluster.name,
                    "lat": "",
                    "long": "",
                    "addrs": "",
                    "ListedDr_Address1": $scope.data.address,
                    "ListedDr_Sl_No": "",
                    "Mobile_Number": $scope.data.phone
                };
                $scope.OfflineRetailers.push(sdata);
                fmcgLocalStorage.createData("Offline_Retailer" + $scope.data.cDataID, $scope.OfflineRetailers);
                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });
            $state.go('fmcgmenu.home');
        };
    }])



.controller('AddDoorToDoorCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
    $scope.$parent.navTitle = "Add Door To Door ";
    $scope.wlkg_View = $scope.doctors.length;
    $scope.fmcgData.DoortoDoor = [{
        "PromotorName": "",
        "Place": "",
        "Taken": "",
        "Issue": "",
        "Stime": "",
        "Etime": ""
    }, {
        "PromotorName": "",
        "Place": "",
        "Taken": "",
        "Issue": "",
        "Stime": "",
        "Etime": ""
    }, {
        "PromotorName": "",
        "Place": "",
        "Taken": "",
        "Issue": "",
        "Stime": "",
        "Etime": ""
    }, {
        "PromotorName": "",
        "Place": "",
        "Taken": "",
        "Issue": "",
        "Stime": "",
        "Etime": ""
    }];
    //$scope.clearData();
    $scope.data = {};
    $scope.customers = [{
        'id': '1',
        'name': $scope.EDrCap
    }];
function replacerr(match, p1, p2, p3, offset, string) {
  // p1 is nondigits, p2 digits, and p3 non-alphanumerics
  return [p1, p2, p3].join(' - ');
}
var newString = 'abc12345#$*%'.replace(/([^\d]*)(\d*)([^\w]*)/, replacerr);
console.log(newString);


    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
    if ($scope.Retailerr.length > 0) {
        if ($scope.Retailerr[0].worktype != undefined) {

            $scope.Retailer.subordinate = {};
            $scope.Retailer.subordinate.selected = {};
            //$scope.Retailer.subordinate.selected.id = $scope.Retailer[0].worktype;
            $scope.Retailer.subordinate.selected.FWFlg = $scope.Retailerr[0].FWFlg;

            console.log("THIRUs");
            if ($scope.view_MR == 1) {
                $scope.Retailer.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Retailer.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            }

            $scope.Retailer.cluster = {};
            $scope.Retailer.cluster.selected = {};
            $scope.Retailer.cluster.selected.id = $scope.Retailerr[0].clusterid;
            // $scope.$parent.fmcgData.cluster.selected.id =$scope.Retailerr[0].clusterid;
            $scope.Retailer.stockist = {};
            $scope.Retailer.stockist.selected = {};
            $scope.Retailer.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.Retailer.doctor = {};
            $scope.Retailer.doctor.selected = {};
            $scope.Retailer.doctor.selected.id = $scope.Retailerr[0].custid;
            $scope.fmcgData.doctor = {};
            $scope.fmcgData.doctor.selected = {};
            $scope.fmcgData.doctor.selected.id = $scope.Retailerr[0].custid;

            $scope.$parent.fmcgData.subordinate = {};
            $scope.$parent.fmcgData.subordinate.selected = {};
            $scope.$parent.fmcgData.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            $scope.$parent.fmcgData.stockist = {};
            $scope.$parent.fmcgData.stockist.selected = {};
            $scope.$parent.fmcgData.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.$parent.fmcgData.cluster = {};
            $scope.$parent.fmcgData.cluster.selected = {};

            $scope.$parent.fmcgData.cluster.selected.id = $scope.Retailerr[0].clusterid;
            $scope.$parent.fmcgData.cluster.name = $scope.Retailerr[0].ClstrName

            $scope.fmcgData.inshopRoute = $scope.Retailerr[0].ClstrName;
            $scope.fmcgData.inshopDisti = $scope.Retailerr[0].stkName;

        }

    } else {
        window.localStorage.removeItem("DCRToday");
        fmcgLocalStorage.createData("DCRToday", 0);
    }


    $scope.save = function() {

        

        $scope.fmcgData.FWFlg = $scope.Mypln.worktype.selected.FWFlg;

        $scope.fmcgData.Remarks = $scope.fmcgData.doortodoorremarks;

        $scope.fmcgData.DoortoDoorSEndToServer = [];
        // $scope.fmcgData.Retailername = $scope.Retailer.doctor.name;


        

        if ($scope.Retailer.cluster.selected == "" || $scope.Retailer.cluster == undefined || $scope.Retailer.cluster.selected == undefined) {
            Toast('Select The Route');
            return false;
        }
        if ($scope.fmcgData.Retailername == "" || $scope.fmcgData.Retailername == undefined) {
            Toast('Select The Retailer Name');
            return false;
        }
        if ($scope.fmcgData.Coupons == "" || $scope.fmcgData.Coupons == undefined) {
            Toast('Enter the Coupons...');
            return false;
        }

        if ($scope.fmcgData.DoortoDoor != undefined) {


            for (var i = 0; i < $scope.fmcgData.DoortoDoor.length; i++) {

                if (($scope.fmcgData.DoortoDoor[i].PromotorName != undefined && $scope.fmcgData.DoortoDoor[i].PromotorName != '')) {
                    $scope.fmcgData.DoortoDoorSEndToServer.push($scope.fmcgData.DoortoDoor[i]);
                }
            }
        }


        if ($scope.fmcgData.DoortoDoorSEndToServer.length < 1) {
            Toast('Enter the Promotor Name of Any One');
            return false;
        }


        fmcgAPIservice.addMAData('POST', 'dcr/save', "31", $scope.fmcgData).success(function(response) {
            if (response.success)
                Toast(" Added Successfully DoortoDoor");
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1)
            } else {
                $scope.clearIdividual(3, 1);
            }
            $scope.data = {};

        }).error(function() {
            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
        $state.go('fmcgmenu.home');
    };
}])



.controller('GiftCardCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
    $scope.$parent.navTitle = "Promotional";
    $scope.wlkg_View = $scope.doctors.length;
    $scope.fmcgData.GiftCard = fmcgLocalStorage.getData("GiftCard") || [];



    
  

    $scope.save = function() {

       


        $scope.fmcgData.GiftCardAryra = [];
        // $scope.fmcgData.Retailername = $scope.Retailer.doctor.name;


       
        if ($scope.fmcgData.GiftCard != undefined) {


            for (var i = 0; i < $scope.fmcgData.GiftCard.length; i++) {

                if (($scope.fmcgData.GiftCard[i].Qty != undefined && $scope.fmcgData.GiftCard[i].PromotorName != '')) {
                    $scope.fmcgData.GiftCardAryra.push($scope.fmcgData.GiftCard[i]);
                }
            }
        }


        if ($scope.fmcgData.GiftCardAryra.length < 1) {
            Toast('Enter the Qty  of Any One');
            return false;
        }


        fmcgAPIservice.addMAData('POST', 'dcr/save', "45", $scope.fmcgData).success(function(response) {
            if (response.success)
                Toast("Added Successfully");
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1);
            } else {
                $scope.clearIdividual(3, 1);
            }
            $scope.data = {};

        }).error(function() {
            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
        $state.go('fmcgmenu.home');
    };
}])
.controller('OfferProductCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
    $scope.$parent.navTitle = "Today Scheme";
    $scope.wlkg_View = $scope.doctors.length;
    $scope.fmcgData.SchemeDets = fmcgLocalStorage.getData("SchemeDetails") || [];
    


            $scope.goBack = function() {

                if($scope.fmcgData.OFP==1){
             $state.go('fmcgmenu.screen3');
                   
                }else{
                     $state.go('fmcgmenu.reports');
                }
                };
                
              
                for (di = 0; di < $scope.fmcgData.SchemeDets.length; di++) {
                        itm = $scope.products.filter(function(a) {
                            return (a.id == $scope.fmcgData.SchemeDets[di].PCode);
                        });
                        $scope.fmcgData.SchemeDets[di].PN=itm[0].name
                    }
   
}])



.controller('InshopActivityCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $filter) {
    $scope.$parent.navTitle = "InshopActivity";
    $scope.wlkg_View = $scope.doctors.length;
    $scope.fmcgData.InShopActivity = [{
        "PromotorName": "",
        "Value": "",
        "Stime": "",
        "Etime": ""
    }];
    $scope.customers = [{
        'id': '1',
        'name': "InShop Activity"
    }];
    //  $scope.clearData();
    $scope.data = {};
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];


    if ($scope.Retailerr.length > 0) {
        if ($scope.Retailerr[0].worktype != undefined) {

            $scope.Retailer.subordinate = {};
            $scope.Retailer.subordinate.selected = {};
            //$scope.Retailer.subordinate.selected.id = $scope.Retailer[0].worktype;
            $scope.Retailer.subordinate.selected.FWFlg = $scope.Retailerr[0].FWFlg;

            console.log("THIRUs");
            if ($scope.view_MR == 1) {
                $scope.Retailer.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Retailer.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            }

            $scope.Retailer.cluster = {};
            $scope.Retailer.cluster.selected = {};
            $scope.Retailer.cluster.selected.id = $scope.Retailerr[0].clusterid;
            // $scope.$parent.fmcgData.cluster.selected.id =$scope.Retailerr[0].clusterid;
            $scope.Retailer.stockist = {};
            $scope.Retailer.stockist.selected = {};
            $scope.Retailer.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.Retailer.doctor = {};
            $scope.Retailer.doctor.selected = {};
            $scope.Retailer.doctor.selected.id = $scope.Retailerr[0].custid;
            $scope.fmcgData.doctor = {};
            $scope.fmcgData.doctor.selected = {};
            $scope.fmcgData.doctor.selected.id = $scope.Retailerr[0].custid;
            $scope.Retailer.Remarks = $scope.Retailerr[0].remarks;

            $scope.$parent.fmcgData.subordinate = {};
            $scope.$parent.fmcgData.subordinate.selected = {};
            $scope.$parent.fmcgData.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
       
            $scope.$parent.fmcgData.stockist = {};
            $scope.$parent.fmcgData.stockist.selected = {};
            $scope.$parent.fmcgData.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.$parent.fmcgData.cluster = {};
            $scope.$parent.fmcgData.cluster.selected = {};
            $scope.$parent.fmcgData.cluster.selected.id = $scope.Retailerr[0].clusterid;
            $scope.$parent.fmcgData.cluster.name = $scope.Retailerr[0].ClstrName;



            $scope.fmcgData.inshopRoute = $scope.Retailerr[0].ClstrName;
            $scope.fmcgData.inshopDisti = $scope.Retailerr[0].stkName;




        }

    } else {
        window.localStorage.removeItem("DCRToday");
        fmcgLocalStorage.createData("DCRToday", 0);
    }

    $scope.save = function() {

        $scope.fmcgData.FWFlg = $scope.Retailer.subordinate.selected.FWFlg;

        $scope.fmcgData.Remarks = $scope.fmcgData.inshopactivityremarks;

        $scope.fmcgData.InshopActivitySEndToServer = [];

        if ($scope.fmcgData.InShopActivity != undefined) {


            for (var i = 0; i < $scope.fmcgData.InShopActivity.length; i++) {

                if (($scope.fmcgData.InShopActivity[i].PromotorName != undefined && $scope.fmcgData.InShopActivity[i].PromotorName != '')) {
                    $scope.fmcgData.InshopActivitySEndToServer.push($scope.fmcgData.InShopActivity[i]);


                }
            }
        }

        if ($scope.Retailer.cluster.selected == "" || $scope.Retailer.cluster == undefined || $scope.Retailer.cluster.selected == undefined) {
            Toast('Select The Route');
            return false;
        }

        if ($scope.fmcgData.Retailername == "" || $scope.fmcgData.Retailername == undefined) {
            Toast('Select The Retailer Name');
            return false;
        }

        if ($scope.fmcgData.InshopActivitySEndToServer.length < 1) {
            Toast('Enter the Promotor Name of Any One');
            return false;
        }

        fmcgAPIservice.addMAData('POST', 'dcr/save', "32", $scope.fmcgData).success(function(response) {
            if (response.success)
                Toast("Added Successfully");
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1)
            } else {
                $scope.clearIdividual(3, 1);
            }
            $scope.data = {};

        }).error(function() {

            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
        $state.go('fmcgmenu.home');
    };




    $scope.goNext = function() {
        $scope.fmcgData.FWFlg = $scope.Retailer.subordinate.selected.FWFlg;

        $scope.fmcgData.Remarks = $scope.Retailer.Remarks;

        $scope.fmcgData.InshopActivitySEndToServer = [];
        /*  $scope.fmcgData.Retailername = $scope.Retailer.doctor.name;
          $scope.fmcgData.inshopHQ= $scope.Retailer.subordinate.name;
           $scope.fmcgData.inshopDisti=$scope.Retailer.stockist.name;
          $scope.fmcgData.inshopRoute=$scope.Retailer.cluster.name;*/

        $scope.fmcgData.inshopactivityopen = true;


       
        if ($scope.Retailer.cluster.selected == "" || $scope.Retailer.cluster == undefined || $scope.Retailer.cluster.selected == undefined) {
            Toast('Select The Route');
            return false;
        }
         if ($scope.fmcgData.Retailername == "" || $scope.fmcgData.Retailername == undefined) {
            Toast('Select The Retailer Name');
            return false;
        }
        if ($scope.fmcgData.InShopActivity != undefined) {


            for (var i = 0; i < $scope.fmcgData.InShopActivity.length; i++) {

                if (($scope.fmcgData.InShopActivity[i].PromotorName != undefined && $scope.fmcgData.InShopActivity[i].PromotorName != '')) {
                    $scope.fmcgData.InshopActivitySEndToServer.push($scope.fmcgData.InShopActivity[i]);

                }
            }
        }


        if ($scope.fmcgData.InshopActivitySEndToServer.length < 1) {
            Toast('Enter the Promotor Name of Any One');
            return false;
        }

        $state.go('fmcgmenu.screen3');

    };

}])

.controller('SupplierMasterCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $filter) {
    $scope.$parent.navTitle = "Super Stockist";
    $scope.wlkg_View = $scope.doctors.length;
    
    
    //  $scope.clearData();
    $scope.data = {};
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];


    $scope.$parent.fmcgData.eKey = 'EK' + $scope.sfCode + '-' + (new Date()).valueOf();
    var tDate = new Date();
        if (!$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.isDraft) {
            if ($scope.cComputer) {
                $scope.$parent.fmcgData.entryDate = tDate;
                $scope.$parent.fmcgData.modifiedDate = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.$parent.fmcgData.entryDate = tDate;
                    $scope.$parent.fmcgData.modifiedDate = tDate;
                });
            }
        } else {
            if ($scope.cComputer) {
                $scope.$parent.fmcgData.modifiedDate = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.$parent.fmcgData.modifiedDate = tDate;
                });
            }
            var tDate = new Date($scope.$parent.fmcgData.entryDate);
            $scope.$parent.fmcgData.entryDate = tDate;
        }
    if ($scope.Retailerr.length > 0) {
        if ($scope.Retailerr[0].worktype != undefined) {

            $scope.Retailer.subordinate = {};
            $scope.Retailer.subordinate.selected = {};
            //$scope.Retailer.subordinate.selected.id = $scope.Retailer[0].worktype;
            $scope.Retailer.subordinate.selected.FWFlg = $scope.Retailerr[0].FWFlg;

            console.log("THIRUs");
            if ($scope.view_MR == 1) {
                $scope.Retailer.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Retailer.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            }
            $scope.Retailer.cluster = {};
            $scope.Retailer.cluster.selected = {};
            $scope.Retailer.cluster.selected.id = $scope.Retailerr[0].clusterid;
            // $scope.$parent.fmcgData.cluster.selected.id =$scope.Retailerr[0].clusterid;
            $scope.Retailer.stockist = {};
            $scope.Retailer.stockist.selected = {};
            $scope.Retailer.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.Retailer.doctor = {};
            $scope.Retailer.doctor.selected = {};
            $scope.Retailer.doctor.selected.id = $scope.Retailerr[0].custid;
            $scope.Retailer.Remarks = $scope.Retailerr[0].remarks;
            $scope.$parent.fmcgData.subordinate = {};
            $scope.$parent.fmcgData.subordinate.selected = {};
            $scope.$parent.fmcgData.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            /*  $scope.Retailer.subordinate.name= $filter('getValueforID')( $scope.$parent.fmcgData.subordinate.selected.id,$scope.$parent.fmcgData.subordinate).name;
 $scope.FilteredData = data.filter(function(a) {
            return (a.town_code === tCode);
        });
*/
         // $scope.Retailer.subordinate.name=$scope.Retailerr[0].stkName;
            $scope.$parent.fmcgData.stockist = {};
            $scope.$parent.fmcgData.stockist.selected = {};
            $scope.$parent.fmcgData.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.$parent.fmcgData.cluster = {};
            $scope.$parent.fmcgData.cluster.selected = {};
            $scope.$parent.fmcgData.cluster.selected.id = $scope.Retailerr[0].clusterid;
            $scope.$parent.fmcgData.cluster.name = $scope.Retailerr[0].ClstrName;
            $scope.fmcgData.SupplierRoute = $scope.Retailerr[0].clusterid;
           




        }

    } else {
        window.localStorage.removeItem("DCRToday");
        fmcgLocalStorage.createData("DCRToday", 0);
    }

    $scope.save = function() {

         $scope.customers = [{
        'id': '8',
        'name': "SupplierMaster"
    }];
    //  $scope.clearData();
    $scope.data = {};
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];




        $scope.fmcgData.FWFlg = $scope.Retailer.subordinate.selected.FWFlg;



        $scope.fmcgData.SupplierMaster = true;


        $scope.fmcgData.doctor = {};
            $scope.fmcgData.doctor.selected = {};
            $scope.fmcgData.doctor.name = $scope.Retailer.doctor.name;
            $scope.fmcgData.doctor.id= $scope.Retailer.doctor.selected.id ;

        if ($scope.fmcgData.doctor.name == "" || $scope.fmcgData.doctor.name == undefined) {
            Toast('Select The Superstockist');
            return false;
        }

    $scope.$emit('finalSubmit');
    };


    $scope.goNext = function() {

        $scope.customers = [{
        'id': '8',
        'name': "SupplierMaster"
    }];
    //  $scope.clearData();
    $scope.data = {};
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];

        $scope.fmcgData.FWFlg = $scope.Retailer.subordinate.selected.FWFlg;

        $scope.fmcgData.SupplierMaster = true;

        $scope.fmcgData.doctor = {};
            $scope.fmcgData.doctor.selected = {};
            $scope.fmcgData.doctor.name = $scope.Retailer.doctor.name;
            $scope.fmcgData.doctor.id= $scope.Retailer.doctor.selected.id ;

        if ($scope.fmcgData.doctor.name == "" || $scope.fmcgData.doctor.name == undefined) {
          Toast('Select The Superstockist');
            return false;
        }
       
        $state.go('fmcgmenu.screen3');

    };

}])

.controller('InshopCheckinCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $filter) {
    $scope.$parent.navTitle = "InshopCheckIn";
    $scope.wlkg_View = $scope.doctors.length;
    $scope.fmcgData.InShopActivity = [{
        "Stime": "",
        "Etime": ""
    }];
    $scope.customers = [{
        'id': '1',
        'name': "InShop Activity"
    }];
    //  $scope.clearData();
    $scope.data = {};
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];


    if ($scope.Retailerr.length > 0) {
        if ($scope.Retailerr[0].worktype != undefined) {

            $scope.Retailer.subordinate = {};
            $scope.Retailer.subordinate.selected = {};
            //$scope.Retailer.subordinate.selected.id = $scope.Retailer[0].worktype;
            $scope.Retailer.subordinate.selected.FWFlg = $scope.Retailerr[0].FWFlg;

           
            if ($scope.view_MR == 1) {
                 $scope.Retailer.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Retailer.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            }

            $scope.Retailer.cluster = {};
            $scope.Retailer.cluster.selected = {};
            $scope.Retailer.cluster.selected.id = $scope.Retailerr[0].clusterid;
            // $scope.$parent.fmcgData.cluster.selected.id =$scope.Retailerr[0].clusterid;
            $scope.Retailer.stockist = {};
            $scope.Retailer.stockist.selected = {};
            $scope.Retailer.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.Retailer.doctor = {};
            $scope.Retailer.doctor.selected = {};
            $scope.Retailer.doctor.selected.id = $scope.Retailerr[0].custid;
            $scope.fmcgData.doctor = {};
            $scope.fmcgData.doctor.selected = {};
            $scope.fmcgData.doctor.selected.id = $scope.Retailerr[0].custid;
            $scope.Retailer.Remarks = $scope.Retailerr[0].remarks;

            $scope.$parent.fmcgData.subordinate = {};
            $scope.$parent.fmcgData.subordinate.selected = {};
            $scope.$parent.fmcgData.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
           
            $scope.$parent.fmcgData.stockist = {};
            $scope.$parent.fmcgData.stockist.selected = {};
            $scope.$parent.fmcgData.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.$parent.fmcgData.cluster = {};
            $scope.$parent.fmcgData.cluster.selected = {};
            $scope.$parent.fmcgData.cluster.selected.id = $scope.Retailerr[0].clusterid;
            $scope.$parent.fmcgData.cluster.name = $scope.Retailerr[0].ClstrName;

            $scope.fmcgData.inshopRoute = $scope.Retailerr[0].ClstrName;
            $scope.fmcgData.inshopDisti = $scope.Retailerr[0].stkName;


        }

    } else {
        window.localStorage.removeItem("DCRToday");
        fmcgLocalStorage.createData("DCRToday", 0);
    }

    $scope.save = function() {

        $scope.fmcgData.FWFlg = $scope.Retailer.subordinate.selected.FWFlg;

        $scope.fmcgData.Remarks = $scope.fmcgData.inshopactivityremarks;

        $scope.fmcgData.InshopCheckIn = [];

        if ($scope.fmcgData.InShopActivity != undefined) {

            for (var i = 0; i < $scope.fmcgData.InShopActivity.length; i++) {

                if (($scope.fmcgData.InShopActivity[i].Stime != undefined && $scope.fmcgData.InShopActivity[i].Stime != '')) {
                    $scope.fmcgData.InshopCheckIn.push($scope.fmcgData.InShopActivity[i]);

                }
            }
        }


       
        if ($scope.Retailer.cluster.selected == "" || $scope.Retailer.cluster == undefined) {
            Toast('Select The Route');
            return false;
        }
        if ($scope.fmcgData.Retailername == "" || $scope.fmcgData.Retailername == undefined) {
            Toast('Select The Retailer Name');
            return false;
        }

        if ($scope.fmcgData.InshopCheckIn.length < 1) {
            Toast('Enter the StartTime');
            return false;
        }

        fmcgAPIservice.addMAData('POST', 'dcr/save', "40", $scope.fmcgData).success(function(response) {
            if (response.success)
                Toast("Added Successfully");
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1)
            } else {
                $scope.clearIdividual(3, 1);
            }
            $scope.data = {};

        }).error(function() {

            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
        $state.go('fmcgmenu.home');
    };




    $scope.goNext = function() {
        $scope.fmcgData.FWFlg = $scope.Retailer.subordinate.selected.FWFlg;

        $scope.fmcgData.Remarks = $scope.Retailer.Remarks;

        $scope.fmcgData.InshopCheckIn = [];
        /*  $scope.fmcgData.Retailername = $scope.Retailer.doctor.name;
          $scope.fmcgData.inshopHQ= $scope.Retailer.subordinate.name;
           $scope.fmcgData.inshopDisti=$scope.Retailer.stockist.name;
          $scope.fmcgData.inshopRoute=$scope.Retailer.cluster.name;*/

        $scope.fmcgData.inshopactivitycheckin = true;

       
        if ($scope.Retailer.cluster.selected == "" || $scope.Retailer.cluster == undefined || $scope.Retailer.cluster.selected == undefined) {
            Toast('Select The Route');
            return false;
        }
        if ($scope.fmcgData.Retailername == "" || $scope.fmcgData.Retailername == undefined) {
            Toast('Select The Retailer Name');
            return false;
        }

        if ($scope.fmcgData.InShopActivity != undefined) {


            for (var i = 0; i < $scope.fmcgData.InShopActivity.length; i++) {

                if (($scope.fmcgData.InShopActivity[i].Stime != undefined && $scope.fmcgData.InShopActivity[i].Stime != '')) {
                    $scope.fmcgData.InshopCheckIn.push($scope.fmcgData.InShopActivity[i]);

                }
            }
        }


        if ($scope.fmcgData.InshopCheckIn.length < 1) {
            Toast('Enter the Start Time');
            return false;
        }

        $state.go('fmcgmenu.screen3');

    };

}])




.controller('FieldDemoActivityCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $filter) {
    $scope.$parent.navTitle = "Field Demo";
    $scope.wlkg_View = $scope.doctors.length;

    //  $scope.clearData();
    $scope.data = {};
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
    $scope.$parent.fmcgData.subordinate = {};
    $scope.$parent.fmcgData.subordinate.selected = {};
    $scope.$parent.fmcgData.subordinate.selected.id = $scope.Myplns[0].subordinateid;
    $scope.$parent.fmcgData.stockist = {};
    $scope.$parent.fmcgData.stockist.selected = {};
    $scope.$parent.fmcgData.stockist.selected.id = $scope.Myplns[0].stockistid;
    $scope.$parent.fmcgData.cluster = {};
    $scope.$parent.fmcgData.cluster.selected = {};

    $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;
    $scope.goBack = function() {
        $state.go('fmcgmenu.home');
    };

    $scope.Save = function() {

        if ($scope.fmcgData.FormarName == "" || $scope.fmcgData.FormarName == undefined) {
            Toast('Enter  The Former Name');
            return false;
        }
        fmcgAPIservice.addMAData('POST', 'dcr/save', "37", $scope.fmcgData).success(function(response) {
            if (response.success)
                $state.go('fmcgmenu.home');
            Toast("Added Successfully");
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1);
            } else {
                $scope.clearIdividual(3, 1);
            }

        }).error(function() {

            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
    };



    $scope.goNext = function() {

        if ($scope.fmcgData.FormarName == "" || $scope.fmcgData.FormarName == undefined) {
            Toast('Enter  The Former Name');
            return false;
        }
        $scope.fmcgData.FormarName = $scope.fmcgData.FormarName;
        $scope.fmcgData.ContactNumber = $scope.fmcgData.ContactNumber;
        $scope.fmcgData.ContactNumber = $scope.fmcgData.ContactNumber;

        $scope.fmcgData.FLDEMO = 1;

        /*  $scope.fmcgData.Retailername = $scope.Retailer.doctor.name;
          $scope.fmcgData.inshopHQ= $scope.Retailer.subordinate.name;
           $scope.fmcgData.inshopDisti=$scope.Retailer.stockist.name;
          $scope.fmcgData.inshopRoute=$scope.Retailer.cluster.name;*/

        $state.go('fmcgmenu.screen3');

    };

}])

  .controller('NewContactCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $filter) {
    $scope.$parent.navTitle = "Field Demo";
    $scope.wlkg_View = $scope.doctors.length;
    $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
    //  $scope.clearData();
        $scope.Mypln.cluster = {};
        $scope.Mypln.cluster.selected = {};
          $scope.Mypln.cluster.selected = {};
                $scope.Mypln.cluster.selected.id = $scope.Mypl[0].clusterid;
                $scope.Mypln.cluster.name=$scope.Mypl[0].ClstrName;
                   $scope.Mypln.subordinate = {};
                 $scope.Mypln.subordinate.selected = {};
                 if ($scope.view_MR == 1) {
                $scope.Mypln.subordinate.selected.id = $scope.sfCode;
            } else {
                if ($scope.Mypl.length != 0) $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
            }
        $scope.Mypln.stockist={}
        $scope.Mypln.stockist.selected = {}
        $scope.Mypln.stockist.selected.id = $scope.Mypl[0].stockistid;
    $scope.data = {};
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
    $scope.$parent.fmcgData.subordinate = {};
    $scope.$parent.fmcgData.subordinate.selected = {};
    $scope.$parent.fmcgData.subordinate.selected.id = $scope.Myplns[0].subordinateid;
    $scope.$parent.fmcgData.stockist = {};
    $scope.$parent.fmcgData.stockist.selected = {};
    $scope.$parent.fmcgData.stockist.selected.id = $scope.Myplns[0].stockistid;
    $scope.$parent.fmcgData.cluster = {};
    $scope.$parent.fmcgData.cluster.selected = {};
    $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;
    $scope.$parent.fmcgData.worktype={};
    $scope.$parent.fmcgData.worktype.selected={};
    $scope.$parent.fmcgData.worktype.selected.FWFlg=$scope.Myplns[0].FWFlg;

    $scope.FrequencyOfVisit = [{
            "id": 10,
            "name": "FN"
        }, {
            "id": 11,
            "name": "MLY"
        },{
            "id":12,
            "name":"BMLY"

        }]
 
 $scope.CurrentlyUsing = [{
            "id": 10,
            "name": "ABS"
        }, {
            "id": 11,
            "name": "WWS"
        },{
            "id":12,
            "name":"VIKING"

        },{
            "id":13,
            "name":"BAIF"

        },{
            "id":14,
            "name":"NDS/SAG"

        },{
            "id":15,
            "name":"AMUL"

        },{
            "id":16,
            "name":"HISAR BOVINE"

        },{
            "id":17,
            "name":"OTHERS"

        }]
 

    $scope.goBack = function() {
        $state.go('fmcgmenu.home');
    };

    $scope.Save = function() {

        $scope.fmcgData.Route=$scope.Mypln.cluster.selected.id;
        $scope.fmcgData.Ukey='EK' + $scope.sfCode + '-' + (new Date()).valueOf();
        $scope.data.class = {};
        $scope.data.class.selected = {};
            if ($scope.fmcgData.class == undefined || $scope.fmcgData.class.selected == undefined) {
                Toast('Select the Class...');
                return false;
            }
            $scope.data.class.selected.id = $scope.fmcgData.class.selected.id;

            $scope.data.speciality = {};
            $scope.data.speciality.selected = {};
            if ($scope.fmcgData.speciality == undefined || $scope.fmcgData.speciality.selected == undefined) {
                Toast('Select the Category...');
                return false;
            }
            if($scope.fmcgData.CurrentlyUsing!=undefined){
                $scope.fmcgData.AITCU=$scope.fmcgData.CurrentlyUsing.name;
        
            }else{
                 $scope.fmcgData.AITCU='';
            }

          if ($scope.fmcgData.FormarName == "" || $scope.fmcgData.FormarName == undefined) {
                    Toast('Enter  the Customer Name');
                    return false;
                }
     /* if ($scope.fmcgData.Semen == undefined) {
                Toast('Enter The Seeman');
                return false;
            }


*/
            $scope.data.speciality.selected.id = $scope.fmcgData.speciality.selected.id;

            $scope.data.category = {};
            $scope.data.category.selected = {};


            fmcgAPIservice.addMAData('POST', 'dcr/save', "45", $scope.fmcgData).success(function(response) {
            if (response.success)
            
            $state.go('fmcgmenu.home');
            Toast("Added Successfully");

              $scope.clearIdividual(0, 1, '_' + $scope.Mypln.subordinate.selected.id);
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1);
            } else {
                $scope.clearIdividual(3, 1);
            }

        }).error(function() {

            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
    };


   $scope.$parent.fmcgData.BreedCategory=[];

     $scope.gridOptions = {
            data: 'fmcgData.BreedCategory',
            rowHeight: 50,
            enableRowSelection: false,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: false,
            columnDefs: [{
                field: 'name',
                displayName: 'Breed Name',
                enableCellEdit: false,
                cellTemplate: 'partials/jointworkCellTemplate.html'
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50
            }]
        };
   $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.BreedCategory.splice(index, 1);
        };
    $scope.goNext = function() {
        $scope.fmcgData.NewContact=1;


        $scope.fmcgData.Route=$scope.Mypln.cluster.selected.id;
     if ($scope.fmcgData.class == undefined || $scope.fmcgData.class.selected == undefined) {
                Toast('Select the Class...');
                return false;
            }
            
            if ($scope.fmcgData.speciality == undefined || $scope.fmcgData.speciality.selected == undefined) {
                Toast('Select the Category...');
                return false;
            }

        if ($scope.fmcgData.FormarName == "" || $scope.fmcgData.FormarName == undefined) {
                    Toast('Enter  the Customer Name');
                    return false;
                }
        $scope.fmcgData.FormarName = $scope.fmcgData.FormarName;
        $scope.fmcgData.ContactNumber = $scope.fmcgData.ContactNumber;
        $scope.fmcgData.Route=$scope.Mypln.cluster.selected.id;

        $scope.fmcgData.Ukey='EK' + $scope.sfCode + '-' + (new Date()).valueOf();

        /*  $scope.fmcgData.Retailername = $scope.Retailer.doctor.name;
          $scope.fmcgData.inshopHQ= $scope.Retailer.subordinate.name;
           $scope.fmcgData.inshopDisti=$scope.Retailer.stockist.name;
          $scope.fmcgData.inshopRoute=$scope.Retailer.cluster.name;*/

        $state.go('fmcgmenu.GiftCard');

    };

}])

.controller('NewAICtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $filter) {
    $scope.$parent.navTitle = "New AI";
    $scope.wlkg_View = $scope.doctors.length;
    $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
    //  $scope.clearData();
     $scope.$parent.fmcgData.eKey = 'EK' + $scope.sfCode + '-' + (new Date()).valueOf();
      
        $scope.Mypln.cluster = {};
        $scope.Mypln.cluster.selected = {};
          $scope.Mypln.cluster.selected = {};
                $scope.Mypln.cluster.selected.id = $scope.Mypl[0].clusterid;
                $scope.Mypln.cluster.name=$scope.Mypl[0].ClstrName;
                   $scope.Mypln.subordinate = {};
                 $scope.Mypln.subordinate.selected = {};
                 if ($scope.view_MR == 1) {
                $scope.Mypln.subordinate.selected.id = $scope.sfCode;
            } else {
                if ($scope.Mypl.length != 0) $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
            }
        $scope.Mypln.stockist={}
        $scope.Mypln.stockist.selected = {}
        $scope.Mypln.stockist.selected.id = $scope.Mypl[0].stockistid;
    $scope.data = {};
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.NewAi=1;
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
    $scope.$parent.fmcgData.subordinate = {};
    $scope.$parent.fmcgData.subordinate.selected = {};
    $scope.$parent.fmcgData.subordinate.selected.id = $scope.Myplns[0].subordinateid;
    $scope.$parent.fmcgData.stockist = {};
    $scope.$parent.fmcgData.stockist.selected = {};
    $scope.$parent.fmcgData.stockist.selected.id = $scope.Myplns[0].stockistid;
    $scope.$parent.fmcgData.cluster = {};
    $scope.$parent.fmcgData.cluster.selected = {};
    $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;
    $scope.$parent.fmcgData.cluster.name = $scope.Myplns[0].ClstrName;
    
    $scope.$parent.fmcgData.worktype={};
    $scope.$parent.fmcgData.worktype.selected={};
    $scope.$parent.fmcgData.worktype.selected.FWFlg=$scope.Myplns[0].FWFlg;
    $scope.fmcgData.PhoneOrderTypes = {};
        $scope.fmcgData.PhoneOrderTypes.selected = {};

        $scope.fmcgData.PhoneOrderTypes.selected.id = 3;
            var tDate = new Date();
        if (!$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.isDraft) {
            if ($scope.cComputer) {
                $scope.$parent.fmcgData.entryDate = tDate;
                $scope.$parent.fmcgData.modifiedDate = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.$parent.fmcgData.entryDate = tDate;
                    $scope.$parent.fmcgData.modifiedDate = tDate;
                });
            }
        } else {
            if ($scope.cComputer) {
                $scope.$parent.fmcgData.modifiedDate = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.$parent.fmcgData.modifiedDate = tDate;
                });
            }
            var tDate = new Date($scope.$parent.fmcgData.entryDate);
            $scope.$parent.fmcgData.entryDate = tDate;
        }
    $scope.FrequencyOfVisit = [{
            "id": 10,
            "name": "FN"
        }, {
            "id": 11,
            "name": "MLY"
        },{
            "id":12,
            "name":"BMLY"

        }]
 
 $scope.CurrentlyUsing = [{
            "id": 10,
            "name": "ABS"
        }, {
            "id": 11,
            "name": "WWS"
        },{
            "id":12,
            "name":"VIKING"

        },{
            "id":13,
            "name":"BAIF"

        },{
            "id":14,
            "name":"NDS/SAG"

        },{
            "id":15,
            "name":"AMUL"

        },{
            "id":16,
            "name":"HISAR BOVINE"

        },{
            "id":17,
            "name":"OTHERS"

        }]
 

    $scope.goBack = function() {
        $state.go('fmcgmenu.home');
    };

    $scope.Save = function() {

        $scope.fmcgData.Route=$scope.Mypln.cluster.selected.id;
        $scope.fmcgData.Ukey='EK' + $scope.sfCode + '-' + (new Date()).valueOf();
       $scope.fmcgData.class = {};
        $scope.fmcgData.class.selected = {};
            
            $scope.data.speciality = {};
            $scope.data.speciality.selected = {};
            if ($scope.fmcgData.speciality == undefined || $scope.fmcgData.speciality.selected == undefined) {
                Toast('Select the Category...');
                return false;
            }
            $scope.fmcgData.class.name="A";
            $scope.fmcgData.class.selected.id='249';
        if($scope.fmcgData.CurrentlyUsing!=undefined){
                $scope.fmcgData.AITCU=$scope.fmcgData.CurrentlyUsing.name;
        
            }else{
                 $scope.fmcgData.AITCU='';
            }
             if ($scope.fmcgData.FormarName == "" || $scope.fmcgData.FormarName == undefined) {
                    Toast('Enter  the Customer Name');
                    return false;
                }
     /* if ($scope.fmcgData.Semen == undefined) {
                Toast('Enter The Seeman');
                return false;
            }


*/
            $scope.data.speciality.selected.id = $scope.fmcgData.speciality.selected.id;

            $scope.data.category = {};
            $scope.data.category.selected = {};


            fmcgAPIservice.addMAData('POST', 'dcr/save', "45", $scope.fmcgData).success(function(response) {
            if (response.success)
            
          
            Toast("Added Successfully");

 $scope.getretailer();


             
        }).error(function() {

            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
    };

    $scope.getretailer=function(){
            fmcgAPIservice.getDataList('POST', 'get/AIretailer&UKEY='+$scope.fmcgData.Ukey, []).success(function(response) {
               
            $scope.fmcgData.doctor={};
            $scope.fmcgData.doctor.selected={};
            $scope.fmcgData.doctor.name=response.ListedDr_Name;
            $scope.fmcgData.doctor.selected.id=response.ListedDrCode;
            $scope.fmcgData.doctor.Mobile_Number=response.ListedDr_Phone;
            $scope.fmcgData.doctor.address=response.ListedDr_Address1;

            $state.go('fmcgmenu.screen3');
                 $scope.clearIdividual(0, 1, '_' + $scope.Mypln.subordinate.selected.id);
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1);
            } else {
                $scope.clearIdividual(3, 1);
            }

                                   
                                        
                }).error(function() {
                                        
                            
            });
}






   $scope.$parent.fmcgData.BreedCategory= [];

     $scope.gridOptions = {
            data: 'fmcgData.BreedCategory',
            rowHeight: 50,
            enableRowSelection: false,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: false,
            columnDefs: [{
                field: 'name',
                displayName: 'Breed Name',
                enableCellEdit: false,
                cellTemplate: 'partials/jointworkCellTemplate.html'
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50
            }]
        };
   $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.BreedCategory.splice(index, 1);
        };
    $scope.goNext = function() {
        $scope.fmcgData.NewContact=1;


        $scope.fmcgData.Route=$scope.Mypln.cluster.selected.id;
     if ($scope.fmcgData.class == undefined || $scope.fmcgData.class.selected == undefined) {
                Toast('Select the Class...');
                return false;
            }
            
            if ($scope.fmcgData.speciality == undefined || $scope.fmcgData.speciality.selected == undefined) {
                Toast('Select the Category...');
                return false;
            }

        if ($scope.fmcgData.FormarName == "" || $scope.fmcgData.FormarName == undefined) {
                    Toast('Enter  the Customer Name');
                    return false;
                }
        $scope.fmcgData.FormarName = $scope.fmcgData.FormarName;
        $scope.fmcgData.ContactNumber = $scope.fmcgData.ContactNumber;
        $scope.fmcgData.Route=$scope.Mypln.cluster.selected.id;

        $scope.fmcgData.Ukey='EK' + $scope.sfCode + '-' + (new Date()).valueOf();

        /*  $scope.fmcgData.Retailername = $scope.Retailer.doctor.name;
          $scope.fmcgData.inshopHQ= $scope.Retailer.subordinate.name;
           $scope.fmcgData.inshopDisti=$scope.Retailer.stockist.name;
          $scope.fmcgData.inshopRoute=$scope.Retailer.cluster.name;*/

        $state.go('fmcgmenu.GiftCard');

    };

}])


.controller('PaccsmeetingActivityCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $filter) {
    $scope.$parent.navTitle = "PACCS Meeting";
    $scope.wlkg_View = $scope.doctors.length;

    //  $scope.clearData();
    $scope.data = {};
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
    $scope.$parent.fmcgData.subordinate = {};
    $scope.$parent.fmcgData.subordinate.selected = {};
    $scope.$parent.fmcgData.subordinate.selected.id = $scope.Myplns[0].subordinateid;
    $scope.$parent.fmcgData.stockist = {};
    $scope.$parent.fmcgData.stockist.selected = {};
    $scope.$parent.fmcgData.stockist.selected.id = $scope.Myplns[0].stockistid;
    $scope.$parent.fmcgData.cluster = {};
    $scope.$parent.fmcgData.cluster.selected = {};
    $scope.$parent.fmcgData.doctor = {};
    $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;
    $scope.goBack = function() {
        $state.go('fmcgmenu.home');
    };

    $scope.Save = function() {

        if ($scope.fmcgData.doctor.name == "" || $scope.fmcgData.doctor.name == undefined) {
            Toast('Enter  The PACCS Name');
            return false;
        }

        if (($scope.fmcgData.remarks == undefined || $scope.fmcgData.remarks == '')) {
            Toast('Select the Template ( or ) Enter the Remarks....');

            return false;
        }
        fmcgAPIservice.addMAData('POST', 'dcr/save', "39", $scope.fmcgData).success(function(response) {
            if (response.success)
                $state.go('fmcgmenu.home');
            Toast("Added Successfully");
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1);
            } else {
                $scope.clearIdividual(3, 1);
            }

        }).error(function() {

            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
    };


}])


.controller('HomeActivityDisplayCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $filter) {
    $scope.$parent.navTitle = "Product Display";
    $scope.wlkg_View = $scope.doctors.length;
    $scope.fmcgData.HomeActivityDisplay = [{
        "SDate": "",
        "EDate": "",
        "CurrentVolume": "",
        "AddVolume": "",
        "DiscountAmount": ""
    }];
    $scope.customers = [{
        'id': '1',
        'name': "InShop Activity"
    }];
    //  $scope.clearData();
    $scope.data = {};
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];
    $scope.$parent.fmcgData.customer = {};
    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];


    if ($scope.Retailerr.length > 0) {
        if ($scope.Retailerr[0].worktype != undefined) {

            $scope.Retailer.subordinate = {};
            $scope.Retailer.subordinate.selected = {};
            //$scope.Retailer.subordinate.selected.id = $scope.Retailer[0].worktype;
            $scope.Retailer.subordinate.selected.FWFlg = $scope.Retailerr[0].FWFlg;

            console.log("THIRUs");
            if ($scope.view_MR == 1) {
                // $scope.Retailer.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Retailer.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            }

            $scope.Retailer.cluster = {};
            $scope.Retailer.cluster.selected = {};
            $scope.Retailer.cluster.selected.id = $scope.Retailerr[0].clusterid;
            // $scope.$parent.fmcgData.cluster.selected.id =$scope.Retailerr[0].clusterid;
            $scope.Retailer.stockist = {};
            $scope.Retailer.stockist.selected = {};
            $scope.Retailer.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.Retailer.doctor = {};
            $scope.Retailer.doctor.selected = {};
            $scope.Retailer.doctor.selected.id = $scope.Retailerr[0].custid;
            $scope.fmcgData.doctor = {};
            $scope.fmcgData.doctor.selected = {};
            $scope.fmcgData.doctor.selected.id = $scope.Retailerr[0].custid;
            $scope.Retailer.Remarks = $scope.Retailerr[0].remarks;

            $scope.$parent.fmcgData.subordinate = {};
            $scope.$parent.fmcgData.subordinate.selected = {};
            $scope.$parent.fmcgData.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            /*  $scope.Retailer.subordinate.name= $filter('getValueforID')( $scope.$parent.fmcgData.subordinate.selected.id,$scope.$parent.fmcgData.subordinate).name;


 $scope.FilteredData = data.filter(function(a) {
            return (a.town_code === tCode);
        });
*/

            // $scope.Retailer.subordinate.name=$scope.Retailerr[0].stkName;

            $scope.$parent.fmcgData.stockist = {};
            $scope.$parent.fmcgData.stockist.selected = {};
            $scope.$parent.fmcgData.stockist.selected.id = $scope.Retailerr[0].stockistid;
            $scope.$parent.fmcgData.cluster = {};
            $scope.$parent.fmcgData.cluster.selected = {};
            $scope.$parent.fmcgData.cluster.selected.id = $scope.Retailerr[0].clusterid;
            $scope.$parent.fmcgData.cluster.name = $scope.Retailerr[0].ClstrName;

            $scope.fmcgData.inshopRoute = $scope.Retailerr[0].ClstrName;
            $scope.fmcgData.inshopDisti = $scope.Retailerr[0].stkName;


        }

    } else {
        window.localStorage.removeItem("DCRToday");
        fmcgLocalStorage.createData("DCRToday", 0);
    }

    $scope.save = function() {

        $scope.fmcgData.FWFlg = $scope.Retailer.subordinate.selected.FWFlg;

        $scope.fmcgData.Remarks = $scope.fmcgData.productdisplayremarks;
        $scope.fmcgData.HQSfCode = $scope.Retailer.subordinate.selected.id;

        $scope.fmcgData.ProductDisplayActivitySendToServer = [];

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;

        $scope.fmcgData.FieldForceName = userData.sfName;

        if ($scope.fmcgData.HomeActivityDisplay != undefined) {


            for (var i = 0; i < $scope.fmcgData.HomeActivityDisplay.length; i++) {

                if (($scope.fmcgData.HomeActivityDisplay[i].SDate != undefined && $scope.fmcgData.HomeActivityDisplay[i].SDate != '')) {
                    $scope.fmcgData.ProductDisplayActivitySendToServer.push($scope.fmcgData.HomeActivityDisplay[i]);


                }
            }
        }

        if ($scope.fmcgData.Retailername == "" || $scope.fmcgData.Retailername == undefined) {
            Toast('Select The Retailer Name');
            return false;
        }

        if ($scope.fmcgData.ProductDisplayActivitySendToServer.length < 1) {
            Toast('Enter the Display Name of Any One');
            return false;
        }

        fmcgAPIservice.addMAData('POST', 'dcr/save', "34", $scope.fmcgData).success(function(response) {
            if (response.success)
                Toast("Added Successfully");
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1)
            } else {
                $scope.clearIdividual(3, 1);
            }
            $scope.data = {};

        }).error(function() {

            Toast("No Internet Connection! Try Again.");
            $ionicLoading.hide();
        });
        $state.go('fmcgmenu.home');
    };

}])


.controller('tdStartCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', function($rootScope, $scope, $state, $ionicPopup, $location, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
    $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
    $scope.attendanceView = window.localStorage.getItem("attendanceView");
    $scope.data = {};
    $scope.toStart = function(update) {

        $scope.data.lat = _currentLocation.Latitude;
        $scope.data.long = _currentLocation.Longitude;
        fmcgAPIservice.addMAData('POST', 'dcr/save&update=' + update, "33", $scope.data).success(function(response) {
                //console.log(response);
                if (response[0]['msg'] != "1") {
                    Toast(response[0]['msg']);
                } else {
                    if (update == 0) {
                        window.localStorage.setItem("attendanceView", 1);
                        $scope.attendanceView = 1;
                        $('#demo1').show();
                        $state.go('fmcgmenu.home');
                        Toast("Today Work Started Successfully");
                    } else {
                        window.localStorage.setItem("attendanceView", 0);
                        $scope.attendanceView = window.localStorage.getItem("attendanceView");
                        Toast("Today Work Completed Successfully");
                        //Toast(response['msg']);
                        $('#demo1').hide();
                        $('#demo2').hide();
                        $state.go('fmcgmenu.tdStart');
                    }
                }

            })
                .error(function() {
                    Toast('No Internet Connection.');
                });
        }
    }])

   .controller('MissedDatesCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', function($rootScope, $scope, $state, $ionicPopup, $location, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "Missed Dates";
        $ionicLoading.show({
            template: ''
        });
           fmcgAPIservice.getDataList('POST', 'get/misseddates', []).success(function(response) {
                            
                localStorage.removeItem("Servicemisseddate");
                    if (response.length && response.length > 0 && Array.isArray(response))
                                  
                    fmcgLocalStorage.createData("Servicemisseddate", response);
                                   
                                        
                }).error(function() {
                                        
                            
            });
        $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        if ($scope.Myplns.length > 0) {
            $scope.Retailer.subordinate = {};
            $scope.Retailer.subordinate.selected = {};
            if ($scope.Myplns[0].worktype != undefined) {

                $scope.Retailer.subordinate.selected.id = $scope.sfCode;
            }
        }
        $scope.Servicemisseddate = fmcgLocalStorage.getData("Servicemisseddate") || [];

 
            $scope.MSDLocal = fmcgLocalStorage.getData("MsdLocal") || [];

            for (var i = 0; $scope.Servicemisseddate.length > i; i++) {

                for (var ii = 0; ii < $scope.MSDLocal.length; ii++) {
                    $scope.FilteredData = $scope.MSDLocal.filter(function(a) {
                        return (a.entryDate == $scope.Servicemisseddate[i].Dcr_Missed_Date);
                    });

                    if ($scope.FilteredData.length > 0) {
                        $scope.FilteredDataa = $scope.FilteredData.filter(function(a) {
                            return (a.customer.selected.id == 1);
                        });
                        $scope.Servicemisseddate[i].status = 1;
                        $scope.Servicemisseddate[i].seccount = $scope.FilteredDataa.length;
                        $scope.Servicemisseddate[i].pricount = $scope.FilteredData.length - $scope.Servicemisseddate[i].seccount;
                    }
                };
                $scope.Servicemisseddate[i].id = i + 1;

            };
            $ionicLoading.hide();

                     $scope.WorkTypesavelocal = fmcgLocalStorage.getData("Worktypesave") || [];
    for (var i = 0; $scope.Servicemisseddate.length > i; i++) {

        for (var ii = 0; ii < $scope.WorkTypesavelocal.length; ii++) {
              $scope.FilteredDatasavewtp = $scope.WorkTypesavelocal.filter(function(a) {
                            return (a.date == $scope.Servicemisseddate[i].Dcr_Missed_Date);
                        });

                if ($scope.FilteredDatasavewtp.length > 0) {
                      $scope.Servicemisseddate[i].Wtype = $scope.FilteredDatasavewtp[0].wfname;
                       $scope.Servicemisseddate[i].Wtypeflag=  $scope.FilteredDatasavewtp[0].wflag;
                }
        }

    }


            $scope.fmcgData.MSD = $scope.Servicemisseddate;

        
        $scope.goBack = function() {
            $state.go('fmcgmenu.reportingentry');
        };
        $scope.Finalsubmit = function(x, id,AllFi) {


            if(AllFi.Wtype==undefined){


                Toast('Select The Worktype');
                return false;

            }

            // if(AllFi.Remarks==undefined || AllFi.Remarks==''){


            //     Toast('Enter The Remarks');
            //     return false;

            // }
            $scope.MissedDateUP(x,AllFi);

            $scope.MSDLocal = fmcgLocalStorage.getData("MsdLocal") || [];
            $scope.MSDLocall = fmcgLocalStorage.getData("MsdLocal") || [];
            var l = $scope.MSDLocal.length;

            $scope.LISTSENDTOSERVER = [];
            $scope.LISTSENDTOSERVER = $scope.MSDLocal.filter(function(a) {
                return (a.entryDate == x);
            });
            var saveposition = [];
            for (var ii = 0; ii < l; ii++) {
                if (x == $scope.MSDLocal[ii].entryDate) {
                    saveposition.push(ii);
                }

            }

            for (var i = saveposition.length - 1; i >= 0; i--) {
                $scope.MSDLocal.splice(saveposition[i], 1);
            }

            localStorage.removeItem("MsdLocal");
            fmcgLocalStorage.createData("MsdLocal", $scope.MSDLocal);
            $scope.MSDLocalll = fmcgLocalStorage.getData("MsdLocal") || [];

            $scope.FilteredData = $scope.MSDLocalll.filter(function(a) {
                return (a.entryDate == x);
            });
            if ($scope.FilteredData.length == 0) {

                Toast("Missed Date Sucessfully Submitted");
                $scope.finaldelete(id);
                $scope.submit();

            }

        };

        $scope.finaldelete = function(id) {
            //It Will Splice selected (date) When Click Final Submit Button. 
            $scope.fmcgData.MSD.splice(id, 1);

            localStorage.removeItem("Servicemisseddate");
            fmcgLocalStorage.createData("Servicemisseddate", $scope.fmcgData.MSD);

            //Delete All Primary And Secondary Order Local Database On Selected Date 
            TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];

            for (var i = 0; i < TpTwns.length; i++) {
                SecondaryOrder = fmcgLocalStorage.getData("Secondary Order" + $scope.$parent.MSDDATES + TpTwns[i].id) || [];
                PrimaryOrder = fmcgLocalStorage.getData("Primary Order" + $scope.$parent.MSDDATES + TpTwns[i].id) || [];
                if (SecondaryOrder.length > 0) {
                    localStorage.removeItem("Secondary Order" + $scope.$parent.MSDDATES + TpTwns[i].id);
                }
                if (PrimaryOrder.length > 0) {
                    localStorage.removeItem("Primary Order" + $scope.$parent.MSDDATES + TpTwns[i].id);
                }

            }

            localStorage.removeItem("SecondaryDate" + $scope.$parent.MSDDATES);
            localStorage.removeItem("PrimaryDate" + $scope.$parent.MSDDATES);

            $ionicLoading.hide();

        }

        function _savLocal(scope, stat, data) {
            fmcgLocalStorage.addData('saveLater', data);


        };

        $scope.submit = function() {

            var ii;
            for (ii = 0; ii < $scope.LISTSENDTOSERVER.length; ii++) {
                $scope.fmcgDataa = $scope.LISTSENDTOSERVER[ii];
                whatsupval = "";
                prod = "";
                console.log($scope.fmcgDataa.productSelectedList);
                if ($scope.fmcgDataa.productSelectedList != undefined) {
                    for (i = 0; i < $scope.fmcgDataa.productSelectedList.length; i++) {
                        prod = prod + $scope.fmcgDataa.productSelectedList[i]['product_Nm'] + "(" + $scope.fmcgDataa.productSelectedList[i]['rx_qty'] + ")" + "(" + $scope.fmcgDataa.productSelectedList[i]['sample_qty'] + ") ," + "\n"
                    }
                    whatsupval = prod + "\n Total Order Value: " + $scope.fmcgDataa.value

                }
                var stockists = $scope.stockists;
                for (var key1 in stockists) {
                    if ($scope.fmcgDataa.stockist != undefined) {
                        if ($scope.fmcgDataa.stockist.selected != undefined && $scope.fmcgDataa.stockist.selected != '') {
                            if (stockists[key1]['id'] == $scope.fmcgDataa.stockist.selected.id) {
                                $scope.fmcgDataa.stockist.name = stockists[key1]['name'];
                                if ($scope.fmcgData.customer != undefined) {
                                    if ($scope.fmcgDataa.customer.selected.id == "3" && $scope.fmcgDataa.customer.selected != undefined) {
                                        $scope.fmcgDataa.cluster.selected.id = stockists[key1]['town_name'];
                                    }
                                }
                            }
                        }
                    }
                }
                var products = $scope.fmcgDataa.productSelectedList;
                var values = 0;
                for (var key in products) {
                    values = +products[key]['sample_qty'] + +values;
                }
                _savLocal($scope, $state, $scope.fmcgDataa);

            }

        }
        $scope.MissedDateUP = function(date,AL) {

            fmcgAPIservice.addMAData('POST', 'dcr/save&REmarks='+AL.Remarks+'&Worktype='+AL.Wtype, "43", date).success(function(response) {

                if (response.success) {

                }

            }).error(function() {

                Toast("No Internet Connection! Try Again.");

            });
        }
        $scope.MissedOrder = function(code, date,AllFi) {
            $scope.$parent.fmcgData.customer = {};
            $scope.$parent.MSDDATES = date;
         if(AllFi.Wtype==undefined){


                Toast('Select The Worktype');
                return false;

            }

            if (code == 1) {

                $scope.customers = [{
                    'id': '1',
                    'name': "Secondary Order",
                    'wtypeflag':AllFi.Wtypeflag,
                    'wtypename':AllFi.Wtype


                }];
            } else {
                $scope.customers = [{
                    'id': '3',
                    'name': "Primary Order",
                    'wtypeflag':AllFi.Wtypeflag,
                    'wtypename':AllFi.Wtype

                }];
            }
            $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
            $state.go('fmcgmenu.MissedOrderPage');
        }

    }])

   .controller('MissedOrderPageCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', function($rootScope, $scope, $state, $ionicPopup, $location, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
    $scope.$parent.navTitle = "Missed Orders";

    var temp = window.localStorage.getItem("loginInfo");
    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
    $scope.Mypln.subordinate = {};
    $scope.Mypln.subordinate.selected = {};
    $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];

    $scope.allstockists = fmcgLocalStorage.getData("stockist_master_" + $scope.Myplns [0].subordinateid) || [];
 
    $scope.Mypln.stockist = {};
    $scope.Mypln.stockist.selected = {};
    $scope.Mypln.stockist.selected.id = $scope.Myplns[0].stockistid;
    $scope.Mypln.subordinate.selected.id = $scope.Myplns[0].subordinateid;
    $scope.GetDoctorOrStockit = 'doctor_master_';
    $scope.SecondaryDate_Primarydate = 'SecondaryDate';
    if ($scope.$parent.fmcgData.customer.selected.id == 3) {
        $scope.GetDoctorOrStockit = 'stockist_master_';
        $scope.SecondaryDate_Primarydate = 'PrimaryDate';
    }
    $scope.$on('getroutebasedretailer', function(evnt) {
        $scope.GetRouteBasedretailer();
    });
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
         $scope.$on('HQCALL', function(evnt) {
         $scope.Mypln.stockist.selected.id = $scope.Mypln.stockistid;
        $scope.Mypln.subordinate.selected.id =$scope.Mypln.subordinate.selected.id;
    });


      $scope.$parent.fmcgData.GetDoctorOrStockit= $scope.GetDoctorOrStockit;
      $scope.$parent.fmcgData.SecondaryDate_Primarydate=$scope.SecondaryDate_Primarydate

    var data = fmcgLocalStorage.getData($scope.GetDoctorOrStockit + $scope.Myplns[0].subordinateid) || [];

    $scope.$on('getDistributor', function(evnt) {
        $scope.Mypln.stockist.selected.id = $scope.MsdStk.st.selected.id;
    });

    if ($scope.precall.cluster == undefined || typeof($scope.precall.cluster)=='object') {
        $scope.precall.cluster = {};
        $scope.precall.cluster.selected = {};
        $scope.precall.cluster.selected.id = $scope.Myplns[0].clusterid;
    }
    data = data.filter(function(a) {
        return (a.town_code === $scope.precall.cluster.selected.id);
    });

    $scope.GetRouteBasedretailer = function() {

        var data = fmcgLocalStorage.getData($scope.GetDoctorOrStockit + $scope.Mypln.subordinate.selected.id) || [];

        if ($scope.GetDoctorOrStockit == 'doctor_master_') {
            data = data.filter(function(a) {
                return (a.town_code === $scope.precall.cluster.selected.id);
            });
        }

        var SecondarySelection = fmcgLocalStorage.getData($scope.SecondaryDate_Primarydate + $scope.MSDDATES + $scope.precall.cluster.selected.id) || [];

        if (SecondarySelection.length > 0) {

            for (var i = 0; i < data.length; i++) {
                $scope.SecFilteredDate = SecondarySelection.filter(function(a) {
                    return (a.id == data[i].id);
                });

                if ($scope.SecFilteredDate.length > 0) {
                    data[i].Avail = true;
                    if ($scope.SecFilteredDate[0].remarks !== undefined) {
                        data[i].remarks = $scope.SecFilteredDate[0].remarks;
                    }

                }
                $scope.Ordered = $scope.SecFilteredDate.filter(function(a) {
                    return (a.Ordered == 1);
                });
                if ($scope.Ordered.length > 0) {
                    data[i].Ordered = 1;
                }

            }
        }

        var Wtyps = $scope.worktypes.filter(function(a) {
            return (a.FWFlg == "F")
        });
        $scope.$parent.fmcgData.worktype = {};
        $scope.$parent.fmcgData.worktype.selected = {};
        $scope.$parent.fmcgData.worktype.selected = Wtyps[0];

        $scope.fmcgData.MsdRetailerName = data;

    }
    //AutoCheck Box Selected Based On Route 
    var data = fmcgLocalStorage.getData($scope.GetDoctorOrStockit + $scope.Myplns[0].subordinateid) || [];

    if ($scope.GetDoctorOrStockit == 'doctor_master_') {
        data = data.filter(function(a) {
            return (a.town_code === $scope.precall.cluster.selected.id);
        });
    }

    var SecondarySelection = fmcgLocalStorage.getData($scope.SecondaryDate_Primarydate + $scope.MSDDATES + $scope.precall.cluster.selected.id) || [];

    if (SecondarySelection.length > 0) {

        for (var i = 0; i < data.length; i++) {
            $scope.SecFilteredDate = SecondarySelection.filter(function(a) {
                return (a.id == data[i].id);
            });

            if ($scope.SecFilteredDate.length > 0) {
                data[i].Avail = true;
                if ($scope.SecFilteredDate[0].remarks !== undefined) {
                    data[i].remarks = $scope.SecFilteredDate[0].remarks;
                }

            }
            $scope.Ordered = $scope.SecFilteredDate.filter(function(a) {
                return (a.Ordered == 1);
            });
            if ($scope.Ordered.length > 0) {
                data[i].Ordered = 1;
            }

        }
    }

    $scope.fmcgData.MsdRetailerName = data;
    var Wtyps = $scope.worktypes.filter(function(a) {
        return (a.FWFlg == "F")
    });
    $scope.$parent.fmcgData.worktype = {};
    $scope.$parent.fmcgData.worktype.selected = {};
    $scope.$parent.fmcgData.worktype.selected = Wtyps[0];

    $scope.fmcgData.MsdRetailerName = data;

    $scope.MissedOrder = function(x, ii, orderornot) {

        $scope.SupplyThrow = 1;
        //Check On Order Page Whether This is Missed Date Order Or Direct Order
        $scope.$parent.fmcgData.MSDFlag = 1;
        $scope.$parent.fmcgData.entryDate = $scope.$parent.MSDDATES;
        $scope.$parent.fmcgData.modifiedDate = $scope.$parent.MSDDATES;
        $scope.$parent.fmcgData.subordinate = {};
        $scope.$parent.fmcgData.subordinate.selected = {};

        if ($scope.view_MR == 1) {
                $scope.$parent.fmcgData.subordinate.selected.id = userData.sfCode;

                    } else {
                    $scope.$parent.fmcgData.subordinate.selected.id =$scope.Mypln.subordinate.selected.id;

             }
        

        $scope.$parent.fmcgData.stockist = {};
        $scope.$parent.fmcgData.stockist.selected = {};
        if ($scope.$parent.fmcgData.customer.selected.id == 1) {
           /* $scope.$parent.fmcgData.stockist.selected.id =$scope.Mypln.stockist.selected.id;
           
        dist =$scope.allstockists.filter(function(a) {
            return (a.id == $scope.Mypln.stockist.selected.id);
          })

            $scope.$parent.fmcgData.stockist.name = dist[0].name;*/
            $scope.$parent.fmcgData.cluster = {};
            $scope.$parent.fmcgData.cluster.selected = {};
            $scope.$parent.fmcgData.cluster.selected.id = x.town_code;
            $scope.$parent.fmcgData.cluster.name = x.town_name;
            $scope.fmcgData.doctor = {};
            $scope.fmcgData.doctor.selected = {};
            $scope.fmcgData.doctor.selected.id = x.id;
            $scope.fmcgData.doctor.name = x.name;
            $scope.fmcgData.doctor.address = x.addrs;
            $scope.fmcgData.doctor.Mobile_Number = x.Mobile_Number;
        } else {
            $scope.$parent.fmcgData.stockist.selected.id = x.id;
            $scope.$parent.fmcgData.stockist.name = x.name;
        }

        $scope.fmcgData.OrderId = x.id;
        $scope.FilteredDate = $scope.fmcgData.MsdRetailerName.filter(function(a) {
            return (a.Avail == true);
        })

        window.localStorage.removeItem($scope.SecondaryDate_Primarydate + $scope.MSDDATES + $scope.precall.cluster.selected.id);
        fmcgLocalStorage.createData($scope.SecondaryDate_Primarydate + $scope.MSDDATES + $scope.precall.cluster.selected.id, $scope.FilteredDate);
        var secondarySelection = fmcgLocalStorage.getData($scope.$parent.fmcgData.customer.selected.name + $scope.MSDDATES + $scope.precall.cluster.selected.id) || [];
        if (secondarySelection.length > 0) {

         if ($scope.$parent.fmcgData.customer.selected.id == 1){
            $scope.FilteredDate = secondarySelection.filter(function(a) {
                return (a.doctor.selected.id == x.id);
            })
        }else{
             $scope.FilteredDate = secondarySelection.filter(function(a) {
                return (a.stockist.selected.id == x.id);
            })
        }
            



            //This is check Edit Order (Or) First Order
            if ($scope.FilteredDate.length > 0) {
                $scope.$parent.fmcgData.productSelectedList = $scope.FilteredDate[0].productSelectedList;
                $scope.$parent.fmcgData.value = $scope.FilteredDate[0].value;
                $scope.$parent.fmcgData.value1 = $scope.FilteredDate[0].value1;
                $scope.$parent.fmcgData.value2 = $scope.FilteredDate[0].value2;
                $scope.$parent.fmcgData.netamount = $scope.FilteredDate[0].netamount;
                if (orderornot == 0) {
                    //Edit Order Data Send To OrderScreen 
                    selecteditem = fmcgLocalStorage.getData("category_master") || [];
                    $scope.$parent.fmcgData.value = 0;
                    $scope.$parent.fmcgData.value1 = 0;
                    $scope.$parent.fmcgData.value2 = 0;
                    $scope.$parent.fmcgData.netamount = 0;
                } else {
                     $scope.$parent.fmcgData.stockist.selected.id= $scope.FilteredDate[0].stockist.selected.id;
                    selecteditem = $scope.FilteredDate[0].SelectedItem;
                }
                $scope.$parent.fmcgData.SelectedItem = selecteditem;
            } else {
                $scope.$parent.fmcgData.value = 0;
                $scope.$parent.fmcgData.value1 = 0;
                $scope.$parent.fmcgData.value2 = 0;
                $scope.$parent.fmcgData.netamount = 0;
                selecteditem = fmcgLocalStorage.getData("category_master") || [];

                $scope.$parent.fmcgData.SelectedItem = selecteditem;
            }

        } else {

            if (orderornot == 0) {
                $scope.$parent.fmcgData.value = 0;
                $scope.$parent.fmcgData.value1 = 0;
                $scope.$parent.fmcgData.value2 = 0;
                $scope.$parent.fmcgData.netamount = 0;
                selecteditem = fmcgLocalStorage.getData("category_master") || [];
            } else {
                selecteditem = $scope.FilteredDate[0].SelectedItem;

            }


        }

        selecteditem = selecteditem.filter(function(a) {
            return (a.id != -1)
        });
        $scope.$parent.fmcgData.SelectedItem = selecteditem;
        $state.go('fmcgmenu.screen3');


    }

    $scope.SaveOrder = function(x) {
        $scope.Onlyvisit = [];

         $scope.ONLYVISITRETAILERNAME = $scope.fmcgData.MsdRetailerName.filter(function(a) {
                    return (a.Avail == true && a.Ordered==undefined);
                })


         for (var i = 0; i <  $scope.ONLYVISITRETAILERNAME.length; i++) {

                itmP = {};
                itmP.entryDate = $scope.$parent.MSDDATES;
                itmP.modifiedDate = $scope.$parent.MSDDATES;
                itmP.subordinate = {};
                itmP.subordinate.selected = {};
                itmP.subordinate.selected.id = userData.sfCode;
                itmP.stockist = {};
                itmP.stockist.selected = {};
                itmP.worktype={};
                itmP.worktype.selected=$scope.fmcgData.worktype.selected;
                itmP.jontWorkSelectedList=[];
                itmP.productSelectedList=undefined;
                itmP.productSelectedList=$scope.$parent.fmcgData.productSelectedList;
                itmP.giftSelectedList=$scope.$parent.fmcgData.giftSelectedList;
                itmP.value1=0;
                itmP.value2=0;
                itmP.source=$scope.$parent.fmcgData.source;
                itmP.cluster = {};
                itmP.cluster.selected = {};
                itmP.cluster.selected.id =$scope.ONLYVISITRETAILERNAME[i].town_code;
                itmP.cluster.name = $scope.ONLYVISITRETAILERNAME[i].town_name;
                itmP.doctor = {};
                itmP.doctor.selected = {};
                itmP.doctor.selected.id = $scope.ONLYVISITRETAILERNAME[i].id;
                itmP.doctor.name = $scope.ONLYVISITRETAILERNAME[i].name;
                itmP.doctor.address = $scope.ONLYVISITRETAILERNAME[i].addrs;
                itmP.doctor.Mobile_Number =$scope.ONLYVISITRETAILERNAME[i].Mobile_Number;
                if ($scope.$parent.fmcgData.customer.selected.id == 1) {
                    itmP.customer={};
                    itmP.customer.selected = {};
                    itmP.customer.selected.id=$scope.$parent.fmcgData.customer.selected.id;
                /*  itmP.stockist.selected.id =$scope.Myplns [0].stockist.selected.id;
                   dist =$scope.allstockists.filter(function(a) {
                          return (a.id == $scope.Myplns [0].stockist.selected.id);
                   });
               itmP.stockist.name = dist[0].name;*/

                  itmP.eKey = 'EK' + $scope.sfCode + '-' + (new Date()).valueOf()+'-'+$scope.ONLYVISITRETAILERNAME[i].id;

                } else {
                    itmP.customer={};
                    itmP.customer.selected = {};
                    itmP.customer.selected.id=$scope.$parent.fmcgData.customer.selected.id;
                    itmP.eKey = 'EK' + $scope.sfCode + '-' + (new Date()).valueOf();
                   itmP.stockist.selected.id =$scope.ONLYVISITRETAILERNAME[i].id;
                   itmP.stockist.name = $scope.ONLYVISITRETAILERNAME[i].name;
                }
                   itmP.productSelectedList = undefined;
                   itmP.location=$scope.fmcgData.location;
                   itmP.rx=$scope.fmcgData.rx;
                  if ($scope.ONLYVISITRETAILERNAME[i].remarks !== undefined) {
                    itmP.remarks =$scope.ONLYVISITRETAILERNAME[i].remarks

                  }
                $scope.Onlyvisit.push(itmP);
            
        }
        //CheckBoX Selection Save
        var CheckboxSelectionValues = [];
        for (var i = 0; i < $scope.fmcgData.MsdRetailerName.length; i++) {

            if ($scope.fmcgData.MsdRetailerName[i].Avail != undefined && $scope.fmcgData.MsdRetailerName[i].Avail == true && $scope.fmcgData.MsdRetailerName[i].Ordered != undefined && $scope.fmcgData.MsdRetailerName[i].Ordered == 1) {
                CheckboxSelectionValues.push($scope.fmcgData.MsdRetailerName[i]);
            }
        }


if($scope.precall.cluster.selected!=undefined){
    $scope.thiru = fmcgLocalStorage.getData($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES + $scope.precall.cluster.selected.id) || [];

        $scope.MSDLocalll = [];

        if ($scope.$parent.fmcgData.customer.selected.id == 1) {
            for (var i = 0; i < CheckboxSelectionValues.length; i++) {



                $scope.FilteredDateddd = $scope.thiru.filter(function(a) {
                    return (a.doctor.selected.id == CheckboxSelectionValues[i].id);
                })

                $scope.MSDLocalll.push($scope.FilteredDateddd[0]);



            };
        } else {
            for (var i = 0; i < CheckboxSelectionValues.length; i++) {
                for (var ii = 0; ii < $scope.thiru.length; ii++) {
                    if (CheckboxSelectionValues[i].id == $scope.thiru[ii].OrderId) {

                        $scope.MSDLocalll.push($scope.thiru[ii]);

                    }
                };


            };
        }
        if ($scope.MSDLocalll !== undefined && $scope.MSDLocalll.length > 0) {
            localStorage.removeItem($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES + $scope.precall.cluster.selected.id);
            fmcgLocalStorage.createData($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES + $scope.precall.cluster.selected.id, $scope.MSDLocalll.concat($scope.Onlyvisit));
        } else {

            localStorage.removeItem($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES + $scope.precall.cluster.selected.id);
            fmcgLocalStorage.createData($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES + $scope.precall.cluster.selected.id, $scope.MSDLocalll.concat($scope.Onlyvisit));
        }

        //Save Check Box Perticular Route
        $scope.FilteredDate = $scope.fmcgData.MsdRetailerName.filter(function(a) {
            return (a.Avail == true);
        })
        //CheckBox Selected ROute  Values Save To Local if Secondary
        window.localStorage.removeItem($scope.SecondaryDate_Primarydate + $scope.MSDDATES + $scope.precall.cluster.selected.id);
        fmcgLocalStorage.createData($scope.SecondaryDate_Primarydate + $scope.MSDDATES + $scope.precall.cluster.selected.id, $scope.FilteredDate);

    }
        
        if (x == 0) {
            $scope.openModal('town_master', 506, $scope.Mypln.subordinate.selected.id);
        } else {
            $scope.finalsave();

        }

    }


    $scope.finalsave = function() {
        $scope.$parent.fmcgData.MSDFlag = 0;
        $scope.thiru = fmcgLocalStorage.getData("MsdLocal") || [];
        if ($scope.thiru !== undefined && $scope.thiru.length > 0) {
            var saveposition = [];
            for (var ii = 0; ii < $scope.thiru.length; ii++) {
                if ($scope.$parent.MSDDATES == $scope.thiru[ii].entryDate && $scope.thiru[ii].customer.selected.id == $scope.$parent.fmcgData.customer.selected.id) {
                    saveposition.push(ii);
                }

            }

            for (var i = saveposition.length - 1; i >= 0; i--) {
                $scope.thiru.splice(saveposition[i], 1);
            }
        }
        if ($scope.view_MR == 1)
            $scope.cDataID = '_' + $scope.sfCode;
        else
            $scope.cDataID = '_' + $scope.Myplns[0].subordinateid; //alert($scope.cDataID);

        TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cDataID) || [];

        $scope.MSDLocal = [];
        for (var i = 0; i < TpTwns.length; i++) {

            Orderroute = fmcgLocalStorage.getData($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES + TpTwns[i].id) || [];

            if (Orderroute.length > 0) {

                for (var ii = 0; ii < Orderroute.length; ii++) {
                    $scope.MSDLocal.push(Orderroute[ii]);
                };

            }
        }

        $scope.fmcgData.entryDate = $scope.$parent.MSDDATES;
        localStorage.removeItem("MsdLocal");
        fmcgLocalStorage.createData("MsdLocal", $scope.thiru.concat($scope.MSDLocal));
        $scope.worktypesave = fmcgLocalStorage.getData("Worktypesave") || [];
         saveworktype = {};
         saveworktype.date=$scope.$parent.MSDDATES;
         saveworktype.wflag=$scope.$parent.fmcgData.customer.selected.wtypeflag;
        saveworktype.wfname=$scope.$parent.fmcgData.customer.selected.wtypename;
        fmcgLocalStorage.createData("Worktypesave", $scope.worktypesave.concat(saveworktype));
        $state.go('fmcgmenu.MissedDates');



    }

    $scope.goBack = function() {

        $state.go('fmcgmenu.MissedDates');
    };

}])
.controller('editRetailerCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicScrollDelegate', '$timeout', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicScrollDelegate, $timeout) {
    $scope.$parent.navTitle = "Edit " + $scope.DrCap;
    $scope.clearData();

    $scope.$on('event_ClearDataFields', function(evnt) {
        $scope.ClearDataFields();
    });
    $scope.$on('event_getDatas', function(evnt) {
        $scope.getDatas();
    });
    $scope.setETime = function() {
        if ($scope.cComputer) {
            var tDate = new Date();
            $scope.eTime = tDate;
        } else {
            window.sangps.getDateTime(function(tDate) {
                $scope.eTime = tDate;
            });
        }
        $timeout(function() {
            $scope.setETime();
        }, 1000);
    }
    var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
    $scope.divisionCode = loginInfo.divisionCode.replace(',', '');
    $scope.setETime();
    $scope.Retailerr = fmcgLocalStorage.getData("mypln") || [];

    if ($scope.Retailerr.length > 0) {
        if ($scope.Retailerr[0].worktype != undefined) {

            $scope.Retailer.subordinate = {};
            $scope.Retailer.subordinate.selected = {};
            $scope.Retailer.subordinate.selected.id = $scope.Retailerr[0].worktype;
            $scope.Retailer.subordinate.selected.FWFlg = $scope.Retailerr[0].FWFlg;

            console.log("THIRUs");
            if ($scope.view_MR == 1) {
                 $scope.Retailer.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Retailer.subordinate.selected.id = $scope.Retailerr[0].subordinateid;
            }

            $scope.Retailer.cluster = {};
            $scope.Retailer.cluster.selected = {};
            $scope.Retailer.cluster.selected.id = $scope.Retailerr[0].clusterid;
            $scope.Retailer.stockist = {};
            $scope.Retailer.stockist.selected = {};
            $scope.Retailer.stockist.selected.id = $scope.Retailerr[0].stockistid;


        }

    } else {
        window.localStorage.removeItem("DCRToday");
        fmcgLocalStorage.createData("DCRToday", 0);
    }




    $scope.Retailer.data = {};
    $scope.ClearDataFields = function() {
        $scope.Retailer.Data = {};

    }
    $scope.getDatas = function() {
        $scope.Retailer.Data = {};
        $ionicLoading.show({
            template: 'Loading...'
        });

        fmcgAPIservice.getPostData('POST', 'get/retailer', $scope.Retailer.doctor.selected.id)
            .success(function(response) {
                $scope.Retailer.Data.name = response[0].RetailerName;
                $scope.Retailer.Data.address = response[0].Addr;
                $scope.Retailer.Data.phone = response[0].ListedDr_Mobile;
                $scope.Retailer.Data.class = {};
                $scope.Retailer.Data.class.selected = {};
                $scope.Retailer.Data.class.selected.id = response[0].Doc_ClsCode;
                $scope.Retailer.Data.class.name = response[0].Doc_Class_ShortName;
                $scope.Retailer.Data.speciality = {};
                $scope.Retailer.Data.speciality.selected = {};
                $scope.Retailer.Data.speciality.selected.id = response[0].doc_special_code;
                $scope.Retailer.Data.speciality.name = response[0].Doc_Spec_ShortName;
                $scope.Retailer.Data.cityname = response[0].cityname;
                $scope.Retailer.Data.areaname = response[0].areaname;
                $scope.Retailer.Data.contactperson = response[0].contactperson;
                $scope.Retailer.Data.designation = response[0].designation;
                $scope.Retailer.Data.gstno = response[0].gst;
                $scope.Retailer.Data.pincode = response[0].pin_code;
                $scope.Retailer.Data.phone2 = response[0].ListedDr_Phone2;
                $scope.Retailer.Data.contactperson2 = response[0].contactperson2;
                $scope.Retailer.Data.designation2 = response[0].designation2;
                $scope.Retailer.Data.Land_Mark = response[0].Land_Mark;

                $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });
    }
    $scope.SaveDatas = function() {
        if ($scope.Retailer.subordinate == undefined || $scope.Retailer.subordinate.selected == undefined) {
            Toast('Select the Headquarters');
            return false;
        }
        if ($scope.Retailer.stockist == undefined || $scope.Retailer.stockist.selected == undefined) {
            Toast("Select the " + StkCap);
            return false;
        }
        if ($scope.Retailer.cluster == undefined || $scope.Retailer.cluster.selected == undefined) {
            Toast("Select the Route");
            return false;
        }
        if ($scope.Retailer.doctor == undefined || $scope.Retailer.doctor.selected == undefined) {
            Toast('Select the Retailer Name...');
            return false;
        }

        if ($scope.Retailer.Data.name == "" || $scope.Retailer.Data.name == undefined) {
            Toast('Enter the Retailer Name...');
            return false;
        }



        $ionicLoading.show({
            template: 'Updating Retailer Details...'
        });

        data = {}
        data.name = $scope.Retailer.Data.name;
        data.Address = $scope.Retailer.Data.address;
        data.ClassCode = $scope.Retailer.Data.class.selected.id;
        data.SpecCode = $scope.Retailer.Data.speciality.selected.id;
        data.Phone = $scope.Retailer.Data.phone;
        data.SpecName = $scope.Retailer.Data.speciality.name;
        data.ClassName = $scope.Retailer.Data.class.name;
        data.GSTno = $scope.Retailer.Data.gstno;
        data.Cityname = $scope.Retailer.Data.cityname;
        data.Land_Mark = $scope.Retailer.Data.Land_Mark;
        data.AreaName = $scope.Retailer.Data.areaname;
        data.PINcode = $scope.Retailer.Data.pincode;
        data.ContactPerson = $scope.Retailer.Data.contactperson;
        data.Designation = $scope.Retailer.Data.designation;
        data.Phone2 = $scope.Retailer.Data.phone2;
        data.ContactPerson2 = $scope.Retailer.Data.contactperson2;
        data.Designation2 = $scope.Retailer.Data.designation2;

        data.id = $scope.Retailer.doctor.selected.id;

        fmcgAPIservice.getPostData('POST', 'upd/retailer', data)
            .success(function(response) {
                $scope.clearIdividual(0, 1)
                $scope.Retailer.Data = {};
                /* $scope.Retailer.subordinate = {};
                $scope.Retailer.stockist = {};
                $scope.Retailer.cluster = {};
                $scope.Retailer.doctor = {};
*/
                $ionicScrollDelegate.scrollTop();
                Toast("Retailer Updated Successfully.");
                $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });
    }

}])



     .controller('DailyExpenseCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {
        $scope.$parent.navTitle = "Daily Expense Entry ";
        $scope.DAExpense = [];
            $scope.LEOS = [{
                        "id": 12,
                        "name": "HQ"
                    }, {
                        "id": 11,
                        "name": "EX"
                    }, {
                        "id": 13,
                        "name": "OX"
                    }]

                $scope.MOC = [{
                        "id": 11,
                        "name": "Bus"
                    }, {
                        "id": 12,
                        "name": "Bike"
                    }]

            $scope.bikeamount=10;
         $scope.Calculatetotal=function(Km) {

         $scope.fmcgData.TotalKM=(Km*$scope.bikeamount);
        }
                fmcgAPIservice.getDataList('POST', 'get/DAExp', [])
                    .success(function(response) {
                        window.localStorage.removeItem("DAExp");
                        fmcgLocalStorage.createData("DAExp", response);
                        $scope.DAExpense = response || [];
                       


                             if($scope.DAExpense.length==0){
                                 $scope.ExpenseErrorMessage="Expense is not Available" ;
                             }
                          
                        
                    });
                $scope.SaveExp = function() {
        if ($scope.fmcgData.LEOS== undefined || $scope.fmcgData.LEOS.selected.id == undefined) {
                    Toast('Select the LEOS');
                    return false;
                }
        if ($scope.fmcgData.MOC== undefined || $scope.fmcgData.MOC.selected.id == undefined) {
                    Toast('Select the Mode OF Call');
                    return false;
                }
        if ($scope.fmcgData.selectVal == undefined) {
                    Toast('Select the Night Stay Type');
                    return false;
                }

                $scope.fmcgData.DAExpense=$scope.DAExpense;


            fmcgAPIservice.addMAData('POST', 'dcr/save', "28", $scope.fmcgData).success(function (response) {
                if (response.success) {
                    $ionicLoading.hide();
                    Toast("Daily Expense Successfully");
                    $state.go('fmcgmenu.home');
                }
            }).error(function () {
                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });

        }
        }])


    .controller('Eactrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {
        $scope.$parent.navTitle = "ExpenseEntry";
      //https://www.tutlane.com/tutorial/angularjs/angularjs-radio-buttons-binding-with-ng-repeat-validations-checked-example
             $scope.LEOS = [{
                        "id": 12,
                        "name": "HQ"
                    }, {
                        "id": 11,
                        "name": "EX"
                    }, {
                        "id": 13,
                        "name": "OX"
                    }]

                $scope.MOC = [{
                        "id": 12,
                        "name": "Bus"
                    }, {
                        "id": 11,
                        "name": "Bike"
                    }]

                   
             $scope.SaveExp = function() {

            if ($scope.fmcgData.LEOS== undefined || $scope.fmcgData.LEOS.selected.id == undefined) {
                        Toast('Select the LEOS');
                        return false;
                    }
            if ($scope.fmcgData.MOC== undefined || $scope.fmcgData.MOC.selected.id == undefined) {
                        Toast('Select the Mode OF Call');
                        return false;
                    }
            if ($scope.fmcgData.selectVal == undefined) {
                        Toast('Select the Night Stay Type');
                        return false;
                    }


                fmcgAPIservice.addMAData('POST', 'dcr/save', "44", $scope.fmcgData).success(function (response) {
                    if (response.success) {
                        $ionicLoading.hide();
                        Toast("Daily Expense Successfully");
                        $state.go('fmcgmenu.home');
                    }
                }).error(function () {
                    Toast("No Internet Connection! Try Again.");
                    $ionicLoading.hide();
                });

            }

            }])

    .controller('ExpenseEntryCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {


        $scope.update = 0;
        $scope.data = {};
        $scope.$parent.navTitle = "Expense Entry ";
        $scope.getTP = {};
        $scope.getExpense = {};
        $scope.extraDetails = [{
            "parameter": "",
            "type": true,
            "amount": ""

        }];
        view();

        function view() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'table/list', ["getTPDet"])
                .success(function(response) {
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
                .success(function(response) {
                    window.localStorage.removeItem("getExpense");

                    if (response.length == 0) {
                        console.log("no records");
                        $ionicLoading.hide();
                    } else {
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

                            } else {
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


        $scope.calculateTot = function() {
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
        $scope.total = function() {
            if (this.data.fare == undefined)
                $scope.data.tot = 0 + this.data.allowance;
            else if (this.data.allowance == undefined)
                $scope.data.tot = this.data.fare + 0;
            else
                $scope.data.tot = this.data.fare + this.data.allowance;
        }
        $scope.addFields = function() {
            $scope.extraDetails.push({
                parameter: '',
                type: true,
                amount: ''
            });
        }
        $scope.delete = function(index) {
                $scope.extraDetails.splice(index, 1);
            }
            //  $scope.data.inactive = true;

        //ng-disabled="data.inactive"
        $scope.getTP = fmcgLocalStorage.getData("getTP") || [];

        $scope.getTPlength = $scope.getTP.length;
        $scope.submit = function() {
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
            fmcgAPIservice.addMAData('POST', 'dcr/save&update=' + $scope.update, "10", $scope.data).success(function(response) {
                if (response.success) {
                    $ionicLoading.hide();
                    Toast("Expense Entered Successfully");
                    $state.go('fmcgmenu.home');
                }
            }).error(function() {
                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });
        }


    }])
    .controller('TPMonSelectCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {
        $scope.$parent.navTitle = "TP Entry - Month Selection";
        $scope.MonthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]
        var tDate = new Date();
        if ($scope.cComputer) {
            $scope.entryDate = tDate;
            dt = new Date(tDate);
            $scope.CMonth = dt.getMonth() + 1;
            $scope.CYear = dt.getFullYear();

            dt = new Date(dt.setDate(1));
            dt = new Date(dt.setDate(32));
            $scope.NMonth = dt.getMonth() + 1;
            $scope.NYear = dt.getFullYear();
        } else {
            window.sangps.getDateTime(function(tDate) {
                dt = new Date(tDate);
                $scope.entryDate = tDate;
                $scope.CMonth = dt.getMonth() + 1;
                $scope.CYear = dt.getFullYear();

                dt = new Date(dt.setDate(1));
                dt = new Date(dt.setDate(32));
                $scope.NMonth = dt.getMonth() + 1;
                $scope.NYear = dt.getFullYear();
            });
        }
        $scope.GotoCTP = function() {
            $scope.MTPEnty.Month = $scope.CMonth;
            $scope.MTPEnty.Year = $scope.CYear;
           
            $state.go('fmcgmenu.TPEntry');
        }
        $scope.GotoNTP = function(amn) {
            $scope.MTPEnty.Month = $scope.NMonth;
            $scope.MTPEnty.Year = $scope.NYear;
           
            $state.go('fmcgmenu.TPEntry');
        }
    }])
    .controller('TourPlanCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {
        $scope.$parent.navTitle = "Add TP Entry";
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id == "") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
        $scope.clearIdividual(22, 1, '_' + sRSF);
         /*$scope.$on('Calldatefunction', function() {
            $scope.datefn();

        });*/


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
            $scope.Mypln.subordinate.selected.id = undefined;
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


        $scope.month = $scope.MTPEnty.Month;
        $scope.year = $scope.MTPEnty.Year;

//Tour Plan


        $scope.$parent.fmcgData.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList || [];
        $scope.addProduct = function(selected) {
            var jontWorkData = {};
            jontWorkData.jointwork = selected;
            $scope.$parent.fmcgData.jontWorkSelectedList.push(jontWorkData);
        };
        $scope.gridOptions = {
            data: 'fmcgData.jontWorkSelectedList',
            rowHeight: 50,
            enableRowSelection: false,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: false,
            columnDefs: [{
                field: 'jointworkname',
                displayName: 'Work With',
                enableCellEdit: false,
                cellTemplate: 'partials/jointworkCellTemplate.html'
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50
            }]
        };

    $scope.$parent.fmcgData['TourPlanRoute'] = [];
    $scope.$parent.fmcgData['TourPlanRetailer'] = [];


    $scope.gridOptionsroute = {
            data: 'fmcgData.TourPlanRoute',
            rowHeight: 50,
            enableRowSelection: false,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: false,
            columnDefs: [{
                field: 'name',
                displayName: 'Route',
                enableCellEdit: false,
                cellTemplate: 'partials/TprouteCellTemplate.html'
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/RemoveTpButton.html',
                width: 50
            }]
        };




    $scope.gridOptionretailer = {
            data: 'fmcgData.TourPlanRetailer',
            rowHeight: 50,
            enableRowSelection: false,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: false,
            columnDefs: [{
                field: 'name',
                displayName: 'Retailer',
                enableCellEdit: false,
                cellTemplate: 'partials/TprouteCellTemplate.html'
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/RemoveRetailerTp.html',
                width: 50
            }]
        };

        $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
        };



        $scope.removeRowTP = function() {
            var index = this.row.rowIndex;
            $scope.gridOptionsroute.selectItem(index, false);
            $scope.$parent.fmcgData.TourPlanRoute.splice(index, 1);
        };
        $scope.RemoveRetailerTp = function() {
            var index = this.row.rowIndex;
            $scope.gridOptionretailer.selectItem(index, false);
            $scope.$parent.fmcgData.TourPlanRetailer.splice(index, 1);
        };
        /* var date = new Date();
         $scope.month = date.getMonth() + 2;
         $scope.year = date.getFullYear();
         if ($scope.month > 12) {
             $scope.month = 1;
             $scope.year = $scope.year + 1;
         }*/
        $scope.day = 1; //date.getDate();
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
        $scope.modal.fromTemplateUrl('partials/TourPlanModal.html', function(modal) {
            $scope.modal = modal;
        }, {
            animation: 'slide-in-up',
            focusFirstInput: true
        });
        var temp = window.localStorage.getItem("loginInfo");
        var $fl = 0;

        $scope.datefn = function() {
            $scope.options = {
                defaultDate: "'" + $scope.year + "-" + $scope.month + "-" + $scope.day + "'",
                minDate: "'" + $scope.year + "-" + $scope.month + "-" + 1 + "'",
                maxDate: "'" + $scope.year + "-" + $scope.month + "-" + $scope.noOfMonth + "'",
                disabledDates: [],
                dayNamesLength: 3, // 1 for "M", 2 for "Mo", 3 for "Mon"; 9 will show full day names. Default is 1.
                mondayIsFirstDay: true, //set monday as first day of week. Default is false
                eventClick: function(date) {
                    if (date.day < 10)
                        date.day = "0" + date.day;
                    date.month = date.month + 1;
                    if (date.month < 10)
                        date.month = "0" + date.month;
                    $scope.modal.update = function() {
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
                                $scope.Mypln.stockist.selected.id=tourPlan[key]['Worked_with_Code'];
                                $scope.Mypln.worktype.name = tourPlan[key]['worktype_name'];
                                if (tourPlan[key]['worktype_name'] == "Field Work" || tourPlan[key]['worktype_name'] == "Retailer Work") {
                                    $scope.Mypln.worktype.selected.FWFlg = 'F';
                                    if ($scope.SF_type == 6) {
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
                                    } else {
                                        $scope.BrndSumm.stockist.selected.id = Number(tourPlan[key]['Worked_with_Code']);
                                        $scope.BrndSumm.stockist.name = tourPlan[key]['Worked_with_Name'];
                                    }
                                     $scope.Mypln.cluster.selected={};
                                    $scope.Mypln.cluster.selected.id = Number(tourPlan[key]['RouteCode']);
                                    $scope.Mypln.cluster.name = tourPlan[key]['RouteName'];

                                } else {
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
                dateClick: function(date) {
                    $scope.show = 1;
                    $scope.Mypln.subordinate.selected.id = $scope.sfCode;

                    $scope.BrndSumm.stockist.selected.id = '';
                    $scope.Mypln.cluster.selected.id = '';
                    $scope.data.remarks = '';
                    //  $scope.data.market = '';
                    
                    if (date.day < 10) date.day = "0" + date.day;
                    date.month = date.month + 1;
                    if (date.month < 10) date.month = "0" + date.month;
                    $scope.data.date = date.year + "-" + date.month + "-" + date.day;
                    $scope.clickdate = date.day + "/" + date.month + "/" + date.year;
                },
                changeMonth: function(month, year) {

                },
            };
            $scope.Tour_Plan = fmcgLocalStorage.getData("Tour_Plan") || [];
            $scope.events = $scope.Tour_Plan;
        }
             $scope.datefn();
            $scope.$on('Calldatefunction', function() {
               $scope.Tour_Plan = fmcgLocalStorage.getData("Tour_Plan") || [];

          if ($scope.Tour_Plan.length == $scope.noOfMonthsubmit)
                    $scope.submitApproval = 1;
                $scope.show = 0;
                    $scope.events = $scope.Tour_Plan;
         
                });
     
                    
        /* var viewApp = ($scope.view_MR == 1) ? "vwTour_Plan_App" : "vwTourPlan";
        $scope.$on('Calldatefunction', function() {
                    $scope.datefn();

                });
                            fmcgAPIservice.getDataList('POST', 'table/list&CMonth='+ $scope.MTPEnty.Month+'&CYr='+$scope.MTPEnty.Year, [viewApp, '["date","remarks","worktype_code","worktype_name","RouteCode","RouteName","Worked_with_Code","Worked_with_Name"]'], sRSF.replace(/_/g, ''))
                                .success(function(response) {
                                    window.localStorage.removeItem("Tour_Plan");
                                    if (response.length && response.length > 0 && Array.isArray(response))
                                        fmcgLocalStorage.createData("Tour_Plan", response);
                                     var tourPlan = {};
                            tourPlan = fmcgLocalStorage.getData("Tour_Plan") || [];
                             $scope.Tour_Plan = fmcgLocalStorage.createData("Tour_Plan", tourPlan);
                                         $scope.datefn();
                                   
                                  //  $scope.$broadcast('Calldatefunction');
                                   //$rootScope.$broadcast('Calldatefunction');
                                })
                                .error(function() {
                                    $scope.ErrFnd[22] = 1;
                                    cMod = false;
                                    $scope.DataLoaded = false;
                                    ReldDta(22);
                                });*/

               
       
        $scope.submit = function() {
            fmcgAPIservice.addMAData('POST', 'dcr/save&month=' + $scope.month + '&year=' + $scope.year, "19", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("Submitted For Approval");
                    $state.go('fmcgmenu.home');
                }
            })
        }


        $scope.close = function() {
            $scope.show = 0;
        }

        $scope.GoBack = function(amn) {
            $state.go('fmcgmenu.TourPlan');
        }
        $scope.save = function() {
            if ($scope.Mypln.worktype.selected == undefined || $scope.Mypln.worktype.selected.id == "") {
                Toast('Select the Work Type...');
                return false;
            }
            var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
            $scope.data.sfName = loginInfo.sfName;
            $scope.data.SF_type = $scope.SF_type;
            $scope.data.worktype_code = $scope.Mypln.worktype.selected.id;
            $scope.data.worktype_name = $scope.Mypln.worktype.name;
            if ($scope.Mypln.worktype.selected.FWFlg == 'F') {

                //if ($scope.SF_type == 2) {
                if ($scope.Mypln.subordinate.selected == undefined || $scope.Mypln.subordinate.selected.id == "") {
                    Toast('Select the Headquarter...');
                    return false;
                }
                if($scope.DistBased==1){
                   if ($scope.Mypln.stockist.selected == undefined || $scope.Mypln.stockist.selected.id == "" || $scope.Mypln.stockist.selected.id==undefined) {
                    Toast('Select the Distributor...');
                    return false;
                } 
                }
                
                /*}
                else {
                    if ($scope.BrndSumm.stockist.selected == undefined || $scope.BrndSumm.stockist.selected.id == "") {
                        Toast('Select the Distributor...');
                        return false;
                    }
                }*/
                if ($scope.$parent.fmcgData.TourPlanRoute == undefined || $scope.$parent.fmcgData.TourPlanRoute.length==0) {
                    Toast('Select the ' + $scope.StkRoute + '...');
                    return false;
                }
                if($scope.Retailer_TP==1 && ($scope.$parent.fmcgData.TourPlanRetailer==undefined ||$scope.$parent.fmcgData.TourPlanRetailer.length==0 ))
                {
                     Toast('Select the Retailer...');
                    return false;
                }



                // if ($scope.SF_type == 2) {
                $scope.data.Worked_with_Code = $scope.Mypln.stockist.selected.id;
                $scope.data.Worked_with_Name = $scope.Mypln.stockist.name;
                $scope.data.HQ_Code = $scope.Mypln.subordinate.selected.id;
                $scope.data.HQ_Name = $scope.Mypln.subordinate.name;
                $scope.data.SF_Code=$scope.Mypln.subordinate.selected.id;
                $scope.data.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList;
                $scope.data.MultiRoute=$scope.$parent.fmcgData.TourPlanRoute;
                $scope.data.MultiRetailer=$scope.$parent.fmcgData.TourPlanRetailer;

                   jointWorkString = '';
                   if( $scope.data.jontWorkSelectedList!=undefined){
                for (var t = 0, jwlen =  $scope.data.jontWorkSelectedList.length; t < jwlen; t++) {
                    if (t != 0)
                        jointWorkString += "$$";
                    jointWorkString +=  $scope.data.jontWorkSelectedList[t].jointworkname;

                }
           }
                    Multiroutename = '',MultiRouteCode='';

                if($scope.data.MultiRoute!=undefined){
                            for (var t = 0, jwlen = $scope.data.MultiRoute.length; t < jwlen; t++) {
                                if (t != 0)
                                    Multiroutename += "&&";
                                MultiRouteCode+="$$";
                                Multiroutename += $scope.data.MultiRoute[t].name;
                                MultiRouteCode+=$scope.data.MultiRoute[t].jointwork;

                            }
                 }
            $scope.data.RouteCode=MultiRouteCode;
            $scope.data.RouteName=Multiroutename;
            $scope.data.JointWork_Name=jointWorkString;

                /*}
                else {
                    $scope.data.Worked_with_Code = $scope.BrndSumm.stockist.selected.id;
                    $scope.data.Worked_with_Name = $scope.BrndSumm.stockist.name;
                }*/
                /*$scope.data.RouteCode = $scope.Mypln.cluster.selected.id;
                $scope.data.RouteName = $scope.Mypln.cluster.name;*/

            } else {
                $scope.data.Worked_with_Code = '';
                $scope.data.Worked_with_Name = '';
                $scope.data.RouteCode = '';
                $scope.data.RouteName = '';
                $scope.data.SF_Code=$scope.Mypln.subordinate.selected.id;
            }

                    $ionicLoading.show({template: 'Saving...'
                    });

        if($scope.data.worktype_code==0 || $scope.data.worktype_code==""  || $scope.data.worktype_code==undefined){
                Toast("Try Again");

           $ionicLoading.hide();
            return false;
        }else{

            fmcgAPIservice.addMAData('POST', 'dcr/save', "9", $scope.data).success(function(response) {
                if (response.success) {
                        $scope.$parent.TodayTourPlan=0;

                    var tourPlan = {};
                    tourPlan = fmcgLocalStorage.getData("Tour_Plan") || [];
                    for (var key in tourPlan) {
                        if ($scope.data.date == (tourPlan[key]['date'])) {
                            $scope.$parent.TodayTourPlan=1;
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
                    $state.go('fmcgmenu.TPEntry');
                }
            }).error(function() {
                //window.localStorage.setItem("Tour_Plan", $scope.Tour_Plan);
                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
                //$state.go('fmcgmenu.TourPlan');
            });
        }
        }
    }])

    .controller('addChemistCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
        $scope.$parent.navTitle = "Add " + $scope.ChmCap;
        $scope.clearData();
        $scope.view_chemist = 1;
        $scope.NmCap = $scope.ChmCap;
        $scope.data = {};
        $scope.save = function() {
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
            fmcgAPIservice.addMAData('POST', 'dcr/save', "1", $scope.data).success(function(response) {
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
    .controller('dashbrdCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', '$ionicModal', '$ionicScrollDelegate', function($rootScope, $scope, $state, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal, $ionicScrollDelegate) {
        vwCalls = fmcgLocalStorage.getData("MnthSmry") || [];
        fmcgAPIservice.getDataList('POST', 'get/calls', [])
            .success(function(response) {
                window.localStorage.removeItem("MnthSmry");
                fmcgLocalStorage.createData("MnthSmry", response);
                explainVar(response);
            }).error(function() {
                Toast('No Internet Connection.');
            });
        var towns = $scope.myTpTwns;
        var tCode = $scope.Myplns[0]['clusterid'];
        var data = $scope.doctors;
        $scope.FilteredData = data.filter(function(a) {
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
        $scope.GetDate = function(dt) {
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

        $scope.sendWhatsApp = function() {
            xelem = $(event.target).closest('.scroll').find('#preview');
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $(xelem).height() + 300);
            $('body').css("overflow", 'visible');
            SendPDFtoSocialShare($(xelem), "Summary Dashboard", "Summary Dashboard", "w");
            /*html2canvas($(xelem), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    getCanvas = canvas;
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                  //  console.log(canvasImg);
                    window.plugins.socialsharing.shareViaWhatsApp("Summary Dashboard", canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                }
            });*/
        }
        $scope.sendEmail = function() {
            xelem = $(event.target).closest('.scroll').find('#preview');
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $(xelem).height() + 300);
            $('body').css("overflow", 'visible');
            SendPDFtoSocialShare($(xelem), "Summary Dashboard", "Summary Dashboard", "e");
            /*
            html2canvas($(xelem), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    window.plugins.socialsharing.share('', 'Summary Dashboard', canvasImg, '');
                }
            });
            */
        }
    }])
    .controller('homeCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', '$ionicModal', '$ionicScrollDelegate', function($rootScope, $scope, $state, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal, $ionicScrollDelegate) {
        $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
        $scope.$parent.RCPA.flag = 0;
        $scope.$parent.fmcgData.vansales=0;



        if ($scope.Myplns.length > 0) {
            if ($scope.Myplns[0].worktype != undefined) {
                $scope.Mypln.worktype = {};
                $scope.Mypln.worktype.selected = {};
                $scope.Mypln.worktype.selected.id = $scope.Myplns[0].worktype;
                $scope.Mypln.worktype.FWFlg = $scope.Myplns[0].FWFlg;

              //  $scope.clearIdividual(22, 1, '_' + $scope.Myplns[0].subordinateid);
                    }
                }
               
        if($scope.Myplns.length>0){
            $scope.MyplnsRoute=$scope.Myplns[0].ClstrName;
        }
             $scope.AI=0;

             if($scope.DesigSname!=undefined){
                if($scope.DesigSname.replace(/\(.*\)/, '')=="AI "){
                   $scope.AI=1; 
                }
                
             }

              if($scope.GeoChk==0 && navigator.platform != "Win32"){
                 window.sangps.CheckGPS({ "gps": schkGPS });
                    startGPS();                                
              }
        
        $scope.customers = [{
            'id': '1',
            'name': $scope.EDrCap,
            'url': 'manageDoctorResult'
        }];
        var al = 1;
        if ($scope.ChmNeed != 1) {
            $scope.customers.push({
                'id': '2',
                'name': $scope.EChmCap,
                'url': 'manageChemistResult'
            });
            $scope.cCI = al;
            al++;
        }
        if ($scope.StkNeed != 0) {
            $scope.customers.push({
                'id': '3',
                'name': $scope.EStkCap,
                'url': 'manageStockistResult'
            });
            $scope.sCI = al;
            al++;
        }
        if ($scope.UNLNeed != 1) {
            $scope.customers.push({
                'id': '4',
                'name': $scope.ENLCap,
                'url': 'manageStockistResult'
            });
            $scope.nCI = al;
            al++;
        }
        if ($scope.view_STOCKIST == 1) {
            $scope.customers.push({
                'id': '5',
                'name': "Stock Updation",
                'url': 'manageStockistResult'
            });
            $scope.suCI = al;
            al++;
        }
        if ($scope.view_STOCKIST == 1) {
            $scope.customers.push({
                'id': '6',
                'name': "Stock View",
                'url': 'manageStockistResult'
            });
            $scope.svCI = al;
            al++;
        }
        if ($scope.Mypln.worktype.FWFlg == 'DH') {
            $scope.customers.push({
                'id': '7',
                'name': $scope.StkCap + " Hunting",
                'url': 'manageStockistResult'
            });
            $scope.hCI = al;
            al++;
        }

      
           

 
        fmcgAPIservice.getDataList('POST', 'get/calls', [])
            .success(function(response) {
                $scope.modal.vwCalls = response.today;
                $scope.modal.monthvwCalls = response.month;
                $scope.modal.vwCalls.totalRetailor = $scope.FilteredData.length;

                $scope.modal.vwCalls.Totalretailer=response.today.RCCOUNT;
                $scope.modal.vwCalls.BalanceretailerCount=response.today.RCCOUNT-response.today.calls;
      
                $scope.labels = ["UnProductiveCalls", "Productive Calls"];
                $scope.data = [response.today.calls - response.today.order, response.today.order];
                $scope.monthdata = [response.month.calls - response.month.order, response.month.order];
                $scope.modal.monthvwCalls.productivity = parseFloat(($scope.modal.monthvwCalls.order / $scope.modal.vwCalls.totalRetailor) * 100).toFixed(2) + "%";
                $scope.modal.vwCalls.productivity = parseFloat(($scope.modal.vwCalls.order / $scope.modal.vwCalls.totalRetailor) * 100).toFixed(2) + "%";
                $scope.remaining = $scope.rootTarget - $scope.modal.vwCalls.orderVal;
                $scope.colors = ['#14bcc1', '#f8756e'];
            }).error(function() {
                Toast('No Internet Connection.');
            });

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
        } else if ($scope.SrtNd == 0) {
            $scope.attendanceVieww = window.localStorage.getItem("attendanceView");

            if ($scope.Myplns.length == 1 && $scope.attendanceVieww != undefined && $scope.attendanceVieww != 1) {
                $state.go('fmcgmenu.tdStart');
            }
            /*if ($scope.Myplns.length == 1 && $scope.attendanceView == 0) {
                $state.go('fmcgmenu.tdStart');
            }*/
        } else if ($scope.Myplns.length > 0) {

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
        $scope.viewDashboard = function() {
            $state.go('fmcgmenu.dashbrd');
        }


        $scope.doortodoor = function() {
            $state.go('fmcgmenu.AddDoorToDoor');
        }

        $scope.Inshop = function() {
            $scope.$parent.fmcgData.source = 0;
            $state.go('fmcgmenu.InshopActivity');
        }
         $scope.SupplierMaster = function() {
                $scope.$parent.fmcgData.superstockistid = {};
                $scope.$parent.fmcgData.superstockistid.selected = {};
                $scope.$parent.fmcgData.superstockistid.selected.id=$scope.Myplns[0].Sprstk;

                $scope.$parent.fmcgData.stockist = {};
                $scope.$parent.fmcgData.stockist.selected = {};
                $scope.$parent.fmcgData.stockist.selected.id = $scope.Myplns[0].stockistid;
             $scope.$parent.fmcgData.worktype={};

             var temp={};
             temp.FWFlg=$scope.Mypln.worktype.FWFlg;
             temp.id=$scope.Mypln.worktype.selected.id;
              $scope.$parent.fmcgData.worktype.selected =temp;
            $scope.$parent.fmcgData.source = 0;
            $state.go('fmcgmenu.SupplierMaster');
        }


        $scope.FieldDemo = function() {

            $state.go('fmcgmenu.FieldDemoActivity');

        }
        $scope.NewContactt = function() {
            $state.go('fmcgmenu.NewContact');
        }
        $scope.DeliveryStatuss = function() {
            $state.go('fmcgmenu.DeliveryStatus');
        }
        $scope.NewAI = function() {
            $state.go('fmcgmenu.NewAI');
        }
         $scope.pregnancy = function() {
            $state.go('fmcgmenu.PragnancyTest');
        }

        $scope.Paccsmeeting = function() {
            $state.go('fmcgmenu.PaccsmeetingActivity');
        }


        $scope.ProductDisplay = function() {
            $state.go('fmcgmenu.HomeActivityDisplay');
        }

        $scope.InshopCheckIn = function() {
            $state.go('fmcgmenu.InshopCheckIn');

        }


   $scope.$parent.fmcgData.vansales=0;

    $scope.VanSales = function() {
    $scope.$parent.fmcgData.vansales=1;
          $scope.goToCustomer(1);
        }
        $scope.BInvFlg = fmcgLocalStorage.getData("BInvFlg");
        if ($scope.BInvFlg == undefined) {
            if ($scope.cComputer) {
                tDate = new Date();
                fmcgAPIservice.getDataList('POST', 'get/InvFlags&edts=' + tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate() + " 00:00:00", [])
                    .success(function(response) {
                        window.localStorage.setItem("BInvFlg", response.BInvFlg);
                        window.localStorage.setItem("EInvFlg", response.EInvFlg);
                    }).error(function() {
                        Toast('No Internet Connection.');
                    });
            } else {
                window.sangps.getDateTime(function(tDate) {
                    fmcgAPIservice.getDataList('POST', 'get/InvFlags&edts=' + tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate() + " 00:00:00", [])
                        .success(function(response) {
                            window.localStorage.setItem("BInvFlg", response.BInvFlg);
                            window.localStorage.setItem("EInvFlg", response.EInvFlg);
                        }).error(function() {
                            Toast('No Internet Connection.');
                        });
                });
            }


        }
        $scope.gotoCust = function(cus) {

            $scope.$parent.fmcgData.customer = {};
            $scope.$parent.fmcgData.worktype = {};

            $scope.BInvFlg = fmcgLocalStorage.getData("BInvFlg") || 0;
            if ($scope.BInvFlg < 1 && cus == 1 && $scope.SF_type == 1 && $scope.DyInvNeed == 1) {
                Toast("Enter Today Inventory Details");
                $scope.gotoInv();
                return false;
            }
            var CusTyp = (cus == 1) ? "D" : (cus == 2) ? "C" : (cus == 3) ? "S" : (cus == 4) ? "U" : (cus == 7) ? "DH" : "R";

            if ($scope.Mypln.worktype.FWFlg == 'F' || $scope.Mypln.worktype.FWFlg == 'IH') {

                var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function(a) {
                    return (a.id == $scope.prvRptId)
                }) : $scope.worktypes.filter(function(a) {
                    return (a.FWFlg == "F" && a.ETabs.indexOf(CusTyp) > -1)
                });
            }
            if ($scope.Mypln.worktype.FWFlg == 'DH') {
                var Wtyps = $scope.worktypes.filter(function(a) {
                    return (a.FWFlg == "DH")
                });
            }
            if ($scope.Mypln.worktype.FWFlg == 'IH') {
                var Wtyps = $scope.worktypes.filter(function(a) {
                    return (a.FWFlg == "IH")
                });
            }
            if (Wtyps == undefined) {
                var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function(a) {
                    return (a.id == $scope.prvRptId)
                }) : $scope.worktypes.filter(function(a) {
                    return (a.FWFlg == "F" && a.ETabs.indexOf(CusTyp) > -1)
                });
            }
            if (Wtyps.length > 0 || $scope.Mypln.worktype.FWFlg == 'DH' || $scope.Mypln.worktype.FWFlg == 'IH') {
                $scope.$parent.fmcgData.worktype.selected = Wtyps[0]; //{ "id": "field work", "name": "Field Work","FWFlg":"F"}
                switch (cus) {
                    case 1:
                        $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
                        break;
                    case 2:
                        $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.cCI];
                        break;
                    case 3:
                        $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.sCI];
                        break;
                    case 4:
                        $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.nCI];
                        break;
                    case 7:
                        if ($scope.Mypln.worktype.FWFlg == 'DH')
                            $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.hCI];
                        break;
                    case 8:
                        $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
                        $scope.$parent.fmcgData.vansales=1;
                        break;
                }
                $scope.$parent.fmcgData.source = 0;
            }
            var pln = fmcgLocalStorage.getData("mypln") || [];

            if(pln.length>0){
            if(pln[0].eTime!=undefined){
                  dayplandate=new Date(pln[0].eTime.slice(0,10));
            }

            }


              todayy = new Date(); 
            if(pln==undefined ||pln.length==0){

                $scope.Myplns = [];
                  Toast("Your myday plan is missing");
                $state.go('fmcgmenu.myPlan');
            }


            /*else if(!((dayplandate.getFullYear()+"-"+(dayplandate.getMonth()+1)+"-"+dayplandate.getDate())==(todayy.getFullYear()+"-"+(todayy.getMonth()+1)+"-"+todayy.getDate()))){

                  Toast("Your myday plan is missing");
                        $state.go('fmcgmenu.myPlan');
            }*/else if(pln.length > 0) {
                            if ((pln[0]['dcrtype'] == "Route Wise" && cus == 1) || pln[0].FWFlg == 'IH') {
                                if ($scope.cComputer == false) {
                                    if ($scope.GeoChk == 0) {
                                        Toast("Checking for Location Services");
                                        if (_currentLocation.Latitude != undefined) {
                                            var dt = new Date();
                                            var dt1 = new Date(_currentLocation.Time);
                                            var difference = dt.getTime() - dt1.getTime(); // This will give difference in milliseconds
                                            var resultInMinutes = Math.round(difference / 60000);
                                            if (resultInMinutes > 10) {
                                                _currentLocation = {};
                                                return false;
                                            }
                                        } else {
                                            // return false;
                                        }
                                    }
                                    if (_currentLocation.Latitude != undefined) {
                                        if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                                            $scope.fmcgData.geoaddress = _currentLocation.address;
                                            $scope.fmcgData.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;
                                        }
                                    }
                                }

                                $state.go('fmcgmenu.screen3');
                            } else if (pln[0].FWFlg == 'F' || pln[0].FWFlg == 'DH') {
                                $state.go('fmcgmenu.screen2');
                            } else {
                                if(pln[0].FWFlg == 'H')
                                     Toast("You are in holiday Change the worktype");
                                 if(pln[0].FWFlg == 'N')
                            Toast("You are in meeting Change the worktype");
                             if(pln[0].FWFlg == 'W')
                               Toast("You are in week off Change the worktype");
                               // $state.go('fmcgmenu.addNew');
                               $state.go('fmcgmenu.myPlan');
                            }
                        }
                

                        
                    };
        $scope.gotoInv = function() {
            $scope.DayInv.EMode = 0;
            $state.go('fmcgmenu.DailyInv');
        }
        $scope.gotoRet = function() {
            $state.go('fmcgmenu.OrderRet');
        }



        $scope.goToCustomer = function(cus) {
            $scope.fmcgData.editableOrder = 0;
           if (cus == 5)
                $state.go('fmcgmenu.stockUpdation');
            else if (cus == 6)
                $state.go('fmcgmenu.transCurrentStocks');

            else {
                if ($scope.cComputer == false) {
                    if ($scope.GeoChk == 0) {
                        Toast("Checking for Location Services");
                        if (_currentLocation.Latitude != undefined) {
                            var dt = new Date();
                            var dt1 = new Date(_currentLocation.Time);
                            var difference = dt.getTime() - dt1.getTime(); // This will give difference in milliseconds
                            var resultInMinutes = Math.round(difference / 60000);
                            if (resultInMinutes > 10) {
                                _currentLocation = {};
                                return false;
                            }
                        } else {
                            // return false;
                        }
                    }
                    if (_currentLocation.Latitude != undefined) {
                        if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                            $scope.fmcgData.geoaddress = _currentLocation.address;
                            $scope.fmcgData.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;
                        }
                    }
                }
                $scope.gotoCust(cus);
            }
        }

        var ANR = window.localStorage.getItem("AddNewRetailer");
        if (ANR != undefined && ANR == 'true') {
            $scope.goToCustomer(1);
        } else {

        }
        if ($scope.Myplns.length > 0) {
            if ($scope.desigCode == 'MR') {
                var towns = $scope.myTpTwns;
                var tCode = $scope.Myplns[0]['clusterid'];
                var data = $scope.doctors;
                $scope.FilteredData = data.filter(function(a) {
                    return (a.town_code === tCode);
                });
                for (var key in towns) {
                    if (towns[key]['id'] == tCode)
                        $scope.rootTarget = towns[key]['target'];
                    //                        $scope.minProd = towns[key]['min_prod'] + "% -";
                }
                fmcgAPIservice.getDataList('POST', 'get/calls', [])
                    .success(function(response) {
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
                    }).error(function() {
                        Toast('No Internet Connection.');
                    });


            }
            $scope.modal = $ionicModal;
            $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function(modal) {
                $scope.modal = modal;
            }, {
                animation: 'slide-in-up',
                focusFirstInput: true
            });
            $scope.ViewDetail = function(Acd, SlTyp, Adt, type) {
                $scope.modal.type = type;
                $scope.modal.SlTyp = SlTyp;
                $scope.modal.InvCnvrtNd = $scope.InvCnvrtNd;
                $scope.modal.OrdPrnNd = $scope.OrdPrnNd;
                $scope.modal.OfferMode = $scope.OfferMode;
                $scope.modal.PromoValND = $scope.PromoValND;
                $scope.modal.DrSmpQ = $scope.DrSmpQ;
                if ($scope.modal.type == 0)
                    $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.EDrCap : (SlTyp == 2) ? $scope.EChmCap : (SlTyp == 3) ? $scope.EStkCap : (SlTyp == 4) ? $scope.ENLCap : '') + ' Visit Details For : ' + Adt;
                if ($scope.modal.type == 1)
                    $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.EDrCap : (SlTyp == 2) ? $scope.EChmCap : (SlTyp == 3) ? $scope.EStkCap : (SlTyp == 4) ? $scope.ENLCap : '') + ' Order Details For : ' + Adt;
                $scope.modal.show();

                $ionicLoading.show({
                    template: 'Loading...'
                });
                $scope.modal.vwVstlists = [];
                $scope.modal.sendWhatsApp = function() {
                    xelem = $(event.target).closest('.scroll').find('#vstpreview');
                    $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                    $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                    $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                    $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                    $('ion-nav-view').css("height", $(xelem).height() + 300);
                    $('body').css("overflow", 'visible');
                    $ionicScrollDelegate.scrollTop();
                    SendPDFtoSocialShare($(xelem), $scope.modal.rptTitle, $scope.modal.rptTitle, "w");
                    /*
                    html2canvas($(xelem), {
                        onrendered: function (canvas) {
                            $scope.modal.canvasVstImg = canvas.toDataURL("image/jpg");
                            getCanvas = canvas;
                            $('.modal').css("height", "");
                            $('.ion-content').css("height", "");
                            $('.scroll').css("height", "");
                            $('ion-nav-view').css("height", "");
                            $('body').css("overflow", 'hidden');
                            window.plugins.socialsharing.shareViaWhatsApp($scope.modal.rptTitle, $scope.modal.canvasVstImg, null, function () { }, function (errormsg) { alert(errormsg) })
                        }
                    });
                    */
                }
                $scope.modal.sendEmail = function() {
                    xelem = $(event.target).closest('.scroll').find('#vstpreview');
                    $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                    $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                    $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                    $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                    $('ion-nav-view').css("height", $(xelem).height() + 300);
                    $('body').css("overflow", 'visible');
                    $ionicScrollDelegate.scrollTop();
                    SendPDFtoSocialShare($(xelem), $scope.modal.rptTitle, $scope.modal.rptTitle, "e");
                    /*
                    html2canvas($(xelem), {
                        onrendered: function (canvas) {
                            $scope.modal.canvasVstImg = canvas.toDataURL("image/jpg");
                            $('.modal').css("height", "");
                            $('.ion-content').css("height","");
                            $('.scroll').css("height", "");
                            $('ion-nav-view').css("height", "");
                            $('body').css("overflow", 'hidden');
                            window.plugins.socialsharing.share('', $scope.modal.rptTitle, $scope.modal.canvasVstImg, '');
                        }
                    });
                    */
                }


                fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                    .success(function(response) {
                        if ($scope.modal.type == 1) {
                            for (var i = response.length - 1; i >= 0; i--) {
                                if (response[i]['orderValue'] == 0 || response[i]['orderValue'] == null)
                                    response.splice(i, 1);
                            }
                        }
                        $scope.modal.vwVstlists = response;

                        $ionicLoading.hide();
                    }).error(function() {
                        $ionicLoading.hide();
                        Toast('No Internet Connection.');
                    });
                $ionicScrollDelegate.scrollTop();
            }
        };
    }])
    .controller('manageCtrl', ['$rootScope', '$scope', '$state', '$ionicLoading', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', function($rootScope, $scope, $state, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.success = false;
        $scope.owsuccess = false;
        $scope.owTypeData = [];
        $scope.$parent.navTitle = "Submitted Calls";
        $scope.customers = [{
            'id': '1',
            'name': $scope.EDrCap,
            'url': 'manageDoctorResult'
        }];


        var al = 1;
        if ($scope.ChmNeed != 1) {
            $scope.customers.push({
                'id': '2',
                'name': $scope.EChmCap,
                'url': 'manageChemistResult'
            });
            $scope.cCI = al;
            al++;
        }
        if ($scope.StkNeed != 0) {
            $scope.customers.push({
                'id': '3',
                'name': $scope.EStkCap,
                'url': 'manageStockistResult'
            });
            $scope.sCI = al;
            al++;
        }
        if ($scope.UNLNeed != 1) {
            $scope.customers.push({
                'id': '4',
                'name': $scope.ENLCap,
                'url': 'manageStockistResult'
            });
            $scope.nCI = al;
            al++;
        }

        if($scope.Supplier_Master!=0){
             $scope.customers.push({
                        'id': '8',
                        'name':"Super Stockist",
                        'url': 'manageStockistResult'
                    });
                    $scope.nCI = al;
                    al++;
        }



        fmcgAPIservice.getDataList('POST', 'entry/count', []).success(function(response) {
            if (response['success']) {
                $scope.success = true;
                if (response['data'][0]['doctor_count'] == 0 && response['data'][2]['stockist_count'] == 0 &&  response['data'][3]['uldoctor_count'] == 0 && response['data'][7]['SuperStokit'] == 0  ) {

                    $scope.customerslength = 1;
                }

                $scope.customers[0].count = response['data'][0]['doctor_count'];
                if ($scope.ChmNeed != 1)
                    $scope.customers[$scope.cCI].count = response['data'][1]['chemist_count'];
                if ($scope.StkNeed != 0)
                    $scope.customers[$scope.sCI].count = response['data'][2]['stockist_count'];
                if ($scope.UNLNeed != 1)
                    $scope.customers[$scope.nCI].count = response['data'][3]['uldoctor_count'];
                if ($scope.Supplier_Master != 0)
                    $scope.customers[$scope.nCI].count = response['data'][7]['SuperStokit'];

                $scope.owTypeData.daywise_remarks = response['data'][4]['remarks'];
                $scope.owTypeData.HlfDayWrk = response['data'][5]['halfdaywrk'];
            } else {
                $scope.owsuccess = true;
                $scope.owTypeData = response['data'][0];
                if ($scope.owTypeData.FWFlg == 'DH')
                    $scope.owTypeData.count = response['count']['hunt_count'];
            }

            $ionicLoading.hide();
        }).error(function() {
            $ionicLoading.hide();
            Toast('No Internet Connection.');
        });
    }])
    .controller('screen1Ctrl', function($rootScope, $scope, $location, $ionicPopup, $stateParams, $state, fmcgAPIservice, fmcgLocalStorage, generateUID, notification) {
        $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
        if ($scope.Myplns.length == 0) {
            $state.go('fmcgmenu.myPlan');
        }
        $scope.attendanceView = window.localStorage.getItem("attendanceView");
        if ($scope.SrtNd == 0) {
            if ($scope.Myplns.length == 1 && $scope.attendanceView == 0) {
                $state.go('fmcgmenu.tdStart');
            }
        }
        $scope.$parent.fmcgData.currentLocation = "fmcgmenu.addNew";
        $scope.$parent.navTitle = "Call Entry";
        if ($scope.view_MR == 1) {
            $scope.$parent.fmcgData.subordinate = {};
            $scope.$parent.fmcgData.subordinate.selected = {};
            $scope.$parent.fmcgData.subordinate.selected.id = $scope.sfCode;
            $scope.loadDatas(false, '_' + $scope.sfCode)
        }
        if ($scope.$parent.fmcgData.worktype == undefined) {
            $scope.$parent.fmcgData.worktype = {};
            if ($scope.$parent.fmcgData.worktype.selected == undefined || $scope.$parent.fmcgData.worktype.selected.FWFlg == '' || $scope.$parent.fmcgData.worktype.selected.FWFlg == undefined) {
                $scope.$parent.fmcgData.worktype = {};
                $scope.$parent.fmcgData.worktype.selected = {};
            }
        }
        if ($scope.Myplns[0]['FWFlg'] == 'F' && ($scope.$parent.fmcgData.worktype.selected == undefined || $scope.$parent.fmcgData.worktype.selected.FWFlg == undefined || $scope.$parent.fmcgData.worktype.selected.FWFlg == '')) {
            var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function(a) {
                return (a.id == $scope.prvRptId)
            }) : $scope.worktypes.filter(function(a) {
                return (a.FWFlg == "F")
            });
            $scope.$parent.fmcgData.worktype.selected = Wtyps[0]; //{ "id": "field work", "name": "Field Work","FWFlg":"F"}        
            var id = $stateParams.customerId;
            if (id != null && id.length) {
                var Wtyps = $scope.worktypes.filter(function(a) {
                    return (a.FWFlg == "F")
                });
                $scope.worktype = {};
                $scope.worktype.selected = Wtyps[0];
            }
        } else if ($scope.Myplns[0]['FWFlg'] == 'DH' && ($scope.$parent.fmcgData.worktype.selected.FWFlg == '' || $scope.$parent.fmcgData.worktype.selected.FWFlg == undefined)) {
            var Wtyps = $scope.worktypes.filter(function(a) {
                return (a.FWFlg == "DH")
            });
            $scope.$parent.fmcgData.worktype.selected = Wtyps[0]; //{ "id": "field work", "name": "Field Work","FWFlg":"F"}        
            // var id = $stateParams.customerId;
            //if (id != null && id.length) {
            var Wtyps = $scope.worktypes.filter(function(a) {
                return (a.FWFlg == "DH")
            });
            $scope.worktype = {};
            $scope.worktype.selected = Wtyps[0];
            // }
        } else if ($scope.$parent.fmcgData.worktype == undefined) {
            if ($scope.$parent.fmcgData.worktype.selected.FWFlg == '' || $scope.$parent.fmcgData.worktype.selected.FWFlg == undefined) {
                $scope.$parent.fmcgData.worktype.selected.id = $scope.Myplns[0]['worktype'];
                $scope.$parent.fmcgData.worktype.selected.FWFlg = $scope.Myplns[0]['FWFlg'];
            }
        }
        $scope.goBack = function() {
            if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {
                $ionicPopup.confirm({
                    title: 'Confirm Navigation',
                    content: 'You have unsaved record do you want to save it in draft...?'
                }).then(function(res) {
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
                });
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

        };
        $scope.goNext = function() {
            if ($scope.$parent.fmcgData.worktype.selected.FWFlg != "F" || $scope.$parent.fmcgData.worktype.selected.FWFlg != "DH" || $scope.view_MR == 1) {
                $scope.fmcgData.subordinate = {}
                $scope.fmcgData.subordinate.selected = {}
                $scope.fmcgData.subordinate.selected.id = $scope.cDataID.replace(/_/g, '');
            }
            if ($scope.$parent.fmcgData.worktype.selected.FWFlg == "F" || $scope.$parent.fmcgData.worktype.selected.FWFlg == "DH") {
                var proceed = true;
                var msg = "";
                if (!$scope.fmcgData.subordinate && proceed && $scope.view_MR != 1) {
                    msg = "Please Select Headquarters      ";
                    proceed = false;
                }
                if (!$scope.fmcgData.stockist && proceed && $scope.$parent.fmcgData.worktype.selected.FWFlg != "DH" && $scope.DistBased == 1) {
                    msg = "Please Select Stockist       ";
                    proceed = false;
                }

                if (!$scope.fmcgData.cluster && proceed && $scope.$parent.fmcgData.worktype.selected.FWFlg != "DH") {
                    msg = "Please Select Route       ";
                    proceed = false;
                }
                if ($scope.Myplns[0]['dcrtype'] == 'Route Wise') {
                    $scope.fmcgData.customer = {};
                    $scope.fmcgData.customer.selected = {};
                    $scope.fmcgData.customer.selected.id = "1"
                }
                if (!$scope.fmcgData.customer && proceed && $scope.$parent.fmcgData.worktype.selected.FWFlg != "DH") {
                    msg = msg + "Please Select Visit To         "
                    proceed = false;
                }
                if ($scope.$parent.fmcgData.worktype.selected.FWFlg == "DH") {
                    $scope.fmcgData.customer = {};
                    $scope.fmcgData.customer.selected = {};
                    $scope.fmcgData.customer.selected.id = "7"

                }
                if (proceed) {
                    $scope.$parent.fmcgData.source = 1;
                    $state.go('fmcgmenu.screen2');
                } else {
                    Toast(msg, true);
                }
            } else
                $state.go('fmcgmenu.screen5');
        };
    })
    .controller('screen4Ctrl', function($rootScope, $scope, $state, $filter, fmcgAPIservice, fmcgLocalStorage, notification) {
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
            columnDefs: [{
                field: 'gift',
                displayName: 'Input Name',
                enableCellEdit: false,
                cellTemplate: 'partials/giftCellTemplate.html'
            }, {
                field: 'sample_qty',
                displayName: 'Input Qty',
                enableCellEdit: true,
                editableCellTemplate: "partials/cellEditTemplate1.html",
                width: 90
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50
            }]
        };
        $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.giftSelectedList.splice(index, 1);
        };
        $scope.goBack = function() {
            if (($scope.fmcgData.customer.selected.id == 1 && $scope.DPNeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CPNeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SPNeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NPNeed == 0))
                $state.go('fmcgmenu.screen3');
            else
                $state.go('fmcgmenu.screen2');
        };
        $scope.goNext = function() {
            $state.go('fmcgmenu.screen5');
        };
        $scope.save = function() {
            $scope.saveToDraftO();
        }
    })
    .controller('AppController', function($scope, $ionicSideMenuDelegate) {
        $scope.toggleLeft = function() {
            $ionicSideMenuDelegate.toggleLeft();
        };
    })
    .controller('screen3ssCtrl', function($rootScope, $ionicScrollDelegate, $scope, $filter, $state, $ionicModal, $ionicSideMenuDelegate, fmcgAPIservice, fmcgLocalStorage, notification) {
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
            itm = data.filter(function(a) {
                return (a.id == $scope.ProdByCat[di].id);
            });
            $scope.ProdByCat[di].checked = false;
            if (itm.length > 0) {
                if (itm[0].checked == true) {
                    $scope.ProdByCat[di].checked = true;
                }
            }
        }
        $scope.filterProd = function(item) {
            $scope.ProdByCat = {};
            $scope.ShowMsg = true;
            if (item.id == -1) {
                $scope.ProdByCat = $scope.products.filter(function(a) {
                    return (a.Product_Type_Code == "P");
                });
            } else {
                $scope.ProdByCat = $scope.products.filter(function(a) {
                    return (a.cateid == item.id);
                });
            }
            for (di = 0; di < $scope.ProdByCat.length; di++) {
                itm = $scope.selectedProduct.filter(function(a) {
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
    .controller('screen3sCtrl', function($rootScope, $ionicScrollDelegate, $scope, $filter, $state, $ionicModal, $ionicSideMenuDelegate, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen3";
        $scope.productsimg = [{
                "id": "1",
                "imagename": "a.jpg",
                "rate": 120
            }, {
                "id": "2",
                "imagename": "aa.jpg",
                "rate": 520
            }, {
                "id": "3",
                "imagename": "aaa.jpg",
                "rate": 480
            }, {
                "id": "4",
                "imagename": "aaaa.jpg",
                "rate": 230
            }, {
                "id": "5",
                "imagename": "aaaaa.jpg",
                "rate": 3000
            }, {
                "id": "6",
                "imagename": "b.jpg",
                "rate": 340
            }, {
                "id": "7",
                "imagename": "bb.jpg",
                "rate": 4560
            }, {
                "id": "8",
                "imagename": "bbb.jpg",
                "rate": 3240
            }, {
                "id": "9",
                "imagename": "bbbb.jpg",
                "rate": 800
            }, {
                "id": "10",
                "imagename": "bbbbb.jpg",
                "rate": 420
            },

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
        $scope.qtyChange = function(selected_images) {
            selected_images.value = selected_images.qty * selected_images.rate;
            calculate();
        }
        $scope.addProducts = function(selected_images) {
                if (selected_images.selected == undefined || selected_images.selected == false) {
                    selected_images.selected = true;
                    selectedList();
                } else {

                    selected_images.selected = false;
                    calculate();
                    selectedList();
                }
                selected_images.qty = '';
                selected_images.value = 0;

            }
            // console.log(productImgPath+$scope.productsimg[0].imagename)
        $scope.range = function(max, step) {
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

.controller('screen3Ctrl', function($rootScope, $scope, $filter, $state, $ionicModal, $ionicSideMenuDelegate, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen3";
    $scope.myplan = fmcgLocalStorage.getData("mypln") || [];
    $scope.fmcgData.OrdStk = {};
    $scope.fmcgData.OrdStk.selected = {};
    $scope.fmcgData.OrdStk.selected.id = $scope.myplan[0]['stockistid'];
    $scope.modal = $ionicModal;

    /*$scope.Savehide=0;*/
    $scope.fmcgData.submitdis=false;

    if($scope.fmcgData.netamount>0){
     $scope.GTotal=$scope.fmcgData.netamount;
    }
        $scope.slTyp = $scope.$parent.fmcgData.customer.selected.id;
        $scope.modal.fromTemplateUrl('partials/viewStockistModal.html', function(modal) {
            $scope.modal = modal;
        }, {
            animation: 'slide-in-up',
            focusFirstInput: true
        });

    /*$scope.criteriaMatch = function( item,criteria ) {
      return function( item ) {


        return item.name === criteria.name;


      };
    };*/
    //https://stackoverflow.com/questions/16474091/angularjs-custom-filter-function
    /* $filter('startsWithLetter', function () {
      return function (items, letter) {
        var filtered = [];
        var letterMatch = new RegExp(letter, 'i');
        for (var i = 0; i < items.length; i++) {
          var item = items[i];
          if (letterMatch.test(item.product_Nm.substring(0, 1))) {
            filtered.push(item);
          }
        }
        return filtered;
      };
    });*/
    $scope.filterCars = function(car) {
        // No filter, so return everything
        $scope.fmcgData.searchText = "";
        if (!$scope.fmcgData.searchText) return true;
        var matched = true;

        // Otherwise apply your matching logic
        $scope.fmcgData.Products = [];

        angular.forEach(car, function(value, key) {

            if (value.product_Nm.toLowerCase().indexOf($scope.fmcgData.searchText.toLowerCase()) != -1) {
                $scope.fmcgData.Products.push(car[key]);

            }
            console.log(key + ': ' + value.product_Nm);
        });

        return ($scope.fmcgData.Products);

    };


    $scope.OfferProduct = function() {
$scope.fmcgData.OFP=1;

            $state.go('fmcgmenu.OfferProduct');

        }
    $scope.viewStock = function() {
        var stockistCode = $scope.fmcgData.OrdStk.selected.id;
        fmcgAPIservice.getDataList('POST', 'table/list&stockistCode=' + stockistCode, ["viewStock"])
            .success(function(response) {
                $scope.modal.viewStock = [];
                products = [];
                for (di = 0; di < $scope.products.length; di++) {
                    prod = 0;
                    itm = response.filter(function(a) {
                        if (a.Product_Code == $scope.products[di].id) {
                            products.push({
                                "name": $scope.products[di].name,
                                "Cl_Qty": a.Cl_Qty,
                                "pieces": a.pieces
                            })
                            prod = 1
                        }

                    });
                    if (prod != 1)
                        products.push({
                            "name": $scope.products[di].name,
                            "Cl_Qty": 0,
                            "pieces": 0
                        })

                }
                $scope.modal.viewStock = products;
                $scope.modal.show();
            });

    }
    $scope.CatewsBillProd = $scope.ProdCategory || [];
    $scope.CatewsBillProd = $scope.CatewsBillProd.filter(function(a) {
        return (a.id != -1)
    });
    $scope.qtCol = 15;
    $scope.rtCol = 30;
    if ($scope.slTyp == 1 && $scope.OfferMode == 0) {
        $scope.rtCol = 45;
        $scope.qtCol = 30
    }
    $scope.fmcgData.Products = [];
    $scope.fmcgData.SLTItm = [];
    $scope.addprop1 = function(e) {
        if (e.target.checked) {
            $scope.fmcgData.fmcgcheckbox = true;
            $scope.fmcgData.productSelectedListt = [];

            for (il = 0; il < $scope.CatewsBillProd.length; il++) {
                if ($scope.CatewsBillProd[il].Products != undefined) {
                    var Prods = $scope.CatewsBillProd[il].Products.filter(function(a) {
                        return (a.cb_qty > 0);
                    })
                    if (Prods.length > 0) {
                        $scope.fmcgData.productSelectedListt = $scope.fmcgData.productSelectedListt.concat(Prods);
                        /*
                        $scope.fmcgData.SLTItm=$scope.fmcgData.SLTItm.filter(function(a) {
                                            return (a.id==Prods.cateid);
                                        });*/
                    }
                }
            }

            $scope.CatewsBillProd = $scope.fmcgData.SLTItm;

            for (il = 0; il < $scope.CatewsBillProd.length; il++) {

                $scope.filterProd($scope.CatewsBillProd[il]);
            }
            //$scope.fmcgData.SLTItm = [];       
        } else {
            $scope.fmcgData.fmcgcheckbox = false;
            $scope.CatewsBillProd = $scope.ProdCategory || [];
            $scope.CatewsBillProd = $scope.CatewsBillProd.filter(function(a) {
                return (a.id != -1)
            });
        }

    }
/* for (var i = 0; i < $scope.CatewsBillProd.length; i++) {
           
             $scope.filterProd($scope.CatewsBillProd[i]);
        }*/
    $scope.filterProd = function(itm) {

            if ((',1,').indexOf(',' + $scope.fmcgData.customer.selected.id + ',') > -1 && $scope.Price_category==1) {
                        if ($scope.fmcgData.productSelectedList.length > 0) {
                            console.log($scope.fmcgData.productSelectedList.length);
                            if ($scope.fmcgData.stockist == undefined || $scope.fmcgData.stockist.selected.id == "") {
                                Toast('Select the ' + $scope.StkCap + ' Name');
                                return false;
                            }
                        }
                    }


            if ($scope.fmcgData.SLTItm.length == 0) {
                $scope.fmcgData.SLTItm.push(itm);
            }




        var Prodsflttr = $scope.fmcgData.SLTItm.filter(function(a) {
            return (a.id == itm.id)
        });


        if (Prodsflttr.length == 0) {
            // a is NOT in array1
            $scope.fmcgData.SLTItm.push(itm);
        }



        if (itm.Products == undefined || itm.Products.length <= 0 || $scope.fmcgData.fmcgcheckbox == true) {
            itm.Products = [];


            if ($scope.fmcgData.fmcgcheckbox == true) {
                var Prodsfltr = $scope.fmcgData.productSelectedListt.filter(function(a) {
                    return (a.cateid == itm.id)
                });
            } else {

                var Prodsfltr = $scope.products.filter(function(a) {
                    return (a.cateid == itm.id)
                });

            }



                $scope.DailyBegin = fmcgLocalStorage.getData("DailyBegin") || [];


                if($scope.DailyBegin!=undefined && $scope.fmcgData.vansales!=undefined && $scope.fmcgData.vansales==1){

                    if(Prodsfltr.length>0){
                        for (var i = 0; i < Prodsfltr.length; i++) {
                            
                    Prodsfltr[i].dailyinvqty=0;
                if($scope.DailyBegin.InvProducts!=undefined)
                    if($scope.DailyBegin.InvProducts.length>0){
                         var dailyinvfilter= $scope.DailyBegin.InvProducts.filter(function(a) {
                                return (a.id == Prodsfltr[i].id)
                            });

                            if(dailyinvfilter.length>0){
                                Prodsfltr[i].dailyinvqty=dailyinvfilter[0].Qty;
                            }
                    }
                           
                        }
                    

                    }
                }


            for (il = 0; il < Prodsfltr.length; il++) {
                itmP = {};
                itmP.product = Prodsfltr[il].id;


                if ($scope.slTyp == 1) {

                    if ($scope.$parent.fmcgData.OpeningStock != undefined) {
                        var OP = $scope.$parent.fmcgData.OpeningStock.filter(function(a) {
                            return (a.Product_code == Prodsfltr[il].id)

                        });

                        if ($scope.$parent.fmcgData.MOQ != undefined) {

                            var MOQ = $scope.$parent.fmcgData.MOQ.filter(function(a) {
                                return (a.Product_code == Prodsfltr[il].id)

                            });

                        }
                        if (OP.length > 0) {
                            itmP.OP = OP[0].Rc + OP[0].cl;
                        } else {
                            itmP.OP = 0;
                        }
                        if (MOQ.length > 0) {
                            itmP.MOQ = MOQ[0].MMQ;

                        } else {
                            itmP.MOQ = 0;
                        }


                    }


                }

                itmP.product_Nm = Prodsfltr[il].name;
                itmP.OrdConv = Prodsfltr[il].OrdConv;
                itmP.HSN = Prodsfltr[il].HSN;
                if(Prodsfltr[il].dailyinvqty!=undefined)
                itmP.dailyinvqty= Prodsfltr[il].dailyinvqty;
                itmP.Rate = parseFloat($scope.roundNumber($scope.RateByCd(Prodsfltr[il].id, $scope.$parent.fmcgData.customer.selected.id), 2));

                itmP.CRate = parseFloat($scope.roundNumber($scope.RateByCd(Prodsfltr[il].id, $scope.$parent.fmcgData.customer.selected.id,1), 1));
                itmP.PRate = parseFloat($scope.roundNumber($scope.RateByCd(Prodsfltr[il].id, $scope.$parent.fmcgData.customer.selected.id,2), 2));

                itmP.PieseRate = parseFloat($scope.roundNumber($scope.RateBypice(Prodsfltr[il].id, $scope.$parent.fmcgData.customer.selected.id), 2));

                itmP.Product_Type_Code=Prodsfltr[il].Product_Type_Code;

                itmP.Schmval = Prodsfltr[il].Schmval || 0;
                itmP.rx_qty = Prodsfltr[il].rx_qty || 0;
                itmP.sample_qty = Prodsfltr[il].sample_qty || 0;
                itmP.discount = Prodsfltr[il].discount || 0;
                itmP.discount_price = Prodsfltr[il].discount_price || 0;
                itmP.free = Prodsfltr[il].free || 0;
                itmP.PromoVal = Prodsfltr[il].PromoVal || 0;
                itmP.cb_qty = Prodsfltr[il].cb_qty || 0;
                itmP.pieces = Prodsfltr[il].pieces || 0;
                itmP.recv_qty = Prodsfltr[il].recv_qty || 0;
                itmP.product_netwt = Prodsfltr[il].product_netwt || 0;
                itmP.netweightvalue = Prodsfltr[il].netweightvalue || 0;
                itmP.conversionQty = Number(Prodsfltr[il].conversionQty) || 0;
                itmP.cateid = Prodsfltr[il].cateid || 0;
                itmP.id = Prodsfltr[il].id;

                itmP.name = Prodsfltr[il].name;
                itm.Products.push(itmP);


                $scope.fmcgData.Products.push(itmP);
            }




       



        }


        arrw = $(".arrow")[this.$index]
        if (itm.ShowProd == true) {
            itm.ShowProd = false;
            $(arrw).removeClass("down");
            $(arrw).addClass("right");
        } else {
            itm.ShowProd = true;

            $(arrw).removeClass("right");
            $(arrw).addClass("down");
        }
    }
    $scope.resetfBx = function(x, ci) {
        if (x.rx_qty == '0' && ci == 0) x.rx_qty = '';
        if (x.Prx_qty == '0' && ci == 1) x.Prx_qty = '';
        if (x.discount == '0' && ci == 2) x.discount = '';
        if (x.cb_qty == '0' && ci == 3) x.cb_qty = '';
        if (x.Rate == '0' && ci == 4) x.Rate = '';
        if (x.free == '0' && ci == 5) x.free = '';
        if (x.PromoVal == '0' && ci == 6) x.PromoVal = '';
    }
    $scope.resetlBx = function(x, ci) {
        if (x.rx_qty == '' && ci == 0) x.rx_qty = 0;
        if (x.Prx_qty == '' && ci == 1) x.Prx_qty = 0;
        if (x.discount == '' && ci == 2) x.discount = 0;
        if (x.cb_qty == '' && ci == 3) x.cb_qty = 0;
        if (x.Rate == '' && ci == 4) x.Rate = 0;
        if (x.free == '' && ci == 5) x.free = 0;
        if (x.PromoVal == '' && ci == 6) x.PromoVal = 0;
    }


   
    $scope.pPRowCalc = function(ProdItem, CatItem, caseorpiese) {
        pval = parseFloat(ProdItem.sample_qty);
        pieseval = parseFloat(ProdItem.PieseRate);

        cTot = parseFloat(CatItem.SubTotal)
        cGTot = parseFloat($scope.GTotal);

        if (isNaN(cTot)) cTot = 0;
        if (isNaN(cGTot)) cGTot = 0;

        if (isNaN(pval)) pval = 0;


                if (caseorpiese == 'CQT') {
                    if (ProdItem.Prx_qty == undefined) ProdItem.Prx_qty = 0;

        if( $scope.$parent.fmcgData.customer.selected.id==8){
             ProdItem.sample_qty =(ProdItem.rx_qty * ProdItem.CRate) + ProdItem.Prx_qty * ProdItem.PRate;
        }else{
            ProdItem.sample_qty = ProdItem.rx_qty * ProdItem.Rate + ProdItem.Prx_qty * ProdItem.PieseRate;

        }



            if (ProdItem.sample_qty < 0) {
                ProdItem.sample_qty = 0;
                CatItem.SubTotal = (cTot - pval) + ProdItem.sample_qty;
                ProdItem.rx_qty = 0;
            } else {
                CatItem.SubTotal = (cTot - pval) + ProdItem.sample_qty;
            }
            //$scope.GTotal = (cGTot - pval) + ProdItem.sample_qty;
            //$scope.GTotal = (cGTot - cTot) + CatItem.SubTotal;
            $scope.GTotal = 0;

            console.log($scope.CatewsBillProd);

            for (var key in $scope.CatewsBillProd) {
                if ($scope.CatewsBillProd[key]['SubTotal'] != undefined && $scope.CatewsBillProd[key]['SubTotal'] > 0) {
                    $scope.GTotal += $scope.CatewsBillProd[key]['SubTotal'];
                }

            }
            $scope.fmcgData.value = $scope.GTotal;
            $scope.fmcgData.netamount = $scope.GTotal;
            console.log($scope.GTotal);
        } else {

            if (ProdItem.Prx_qty == undefined) ProdItem.Prx_qty = 0;
            if($scope.$parent.fmcgData.customer.selected.id==8){
             ProdItem.sample_qty =(ProdItem.rx_qty * ProdItem.CRate) + ProdItem.Prx_qty * ProdItem.PRate;
        }else{
         ProdItem.sample_qty = ProdItem.rx_qty * ProdItem.Rate + ProdItem.Prx_qty * ProdItem.PieseRate;
        }

       
            CatItem.SubTotal = (cTot - pval) + ProdItem.sample_qty;
            $scope.GTotal = (cGTot - pval) + ProdItem.sample_qty;

            $scope.fmcgData.value = $scope.GTotal;
            $scope.fmcgData.netamount = $scope.GTotal;
            /*  newval = ProdItem.Prx_qty * ProdItem.PieseRate;
               ProdItem.sample_qty = ProdItem.rx_qty * ProdItem.Rate;

               CatItem.SubTotal = $scope.GTotal +newval;
               $scope.GTotal = $scope.GTotal + newval;
              ProdItem.sample_qty=ProdItem.sample_qty+newval;
               $scope.fmcgData.value = $scope.GTotal;
               $scope.fmcgData.netamount = $scope.GTotal;*/

        }
    }

    $scope.pRowCalcc = function(ProdItem, CatItem) {
        pval = parseFloat(ProdItem.sample_qty);
        pNval = parseFloat(ProdItem.netweightvalue);
        pDis = parseFloat(ProdItem.discount_price);

        cTot = parseFloat(CatItem.SubTotal)
        cGTot = parseFloat($scope.GTotal);
        cGDisc = parseFloat($scope.GDisc);
        cGNW = parseFloat($scope.GNetW);

        if (isNaN(cTot)) cTot = 0;
        if (isNaN(cGTot)) cGTot = 0;
        if (isNaN(cGDisc)) cGDisc = 0;
        if (isNaN(cGNW)) cGNW = 0;

        if (isNaN(pval)) pval = 0;
        if (isNaN(pNval)) pNval = 0;
        if (isNaN(pDis)) pDis = 0;

        if (ProdItem.rx_qtyy==undefined) ProdItem.rx_qtyy=0;
        if (ProdItem.rx_qtyp==undefined) ProdItem.rx_qtyp=0;

        ProdItem.rx_qty = (ProdItem.rx_qtyy * ProdItem.OrdConv)+ProdItem.rx_qtyp;
        ProdItem.sample_qty = (((ProdItem.rx_qtyy * ProdItem.OrdConv)+ProdItem.rx_qtyp) * ProdItem.Rate);

        CatItem.SubTotal = (cTot - pval) + ProdItem.sample_qty;

        $scope.GTotal = (cGTot - pval) + ProdItem.sample_qty;
        $scope.GNetW = (cGNW - pNval) + ProdItem.netweightvalue;
        $scope.GDisc = (cGDisc - pDis) + ProdItem.discount_price;

        $scope.fmcgData.value = $scope.GTotal;
        $scope.fmcgData.netweightvaluetotal = $scope.GNetW;
        $scope.fmcgData.discount_price = $scope.GDisc;
        $scope.fmcgData.netamount = $scope.GTotal;



    $scope.SchemeDets = fmcgLocalStorage.getData("SchemeDetails") || [];
            Schemes = $scope.SchemeDets.filter(function(a) {
                return (a.PCode == ProdItem.product && a.Scheme <= ProdItem.rx_qty);
            });
        ProdItem.Schmval = '';
        ProdItem.free = 0;
        ProdItem.discount_price = 0;
        if (Schemes.length > 0) {
            if (Schemes[0].FQ != '' && Schemes[0].FQ>0) {
                if (Schemes[0].pkg == 'Y')
                    ProdItem.Schmval = parseInt(parseInt((ProdItem.rx_qty / parseInt(Schemes[0].Scheme))))
                else
                    ProdItem.Schmval = parseInt(parseFloat((ProdItem.rx_qty / parseInt(Schemes[0].Scheme))))

                ProdItem.free = ProdItem.Schmval || 0;

            } else {
                dis=0;
                if (Schemes[0].Disc != '')
                    dis = ProdItem.sample_qty * (Schemes[0].Disc / 100);
                ProdItem.Schmval = dis.toFixed(2);
                ProdItem.sample_qty = ProdItem.sample_qty - dis;
                ProdItem.discount_price = ProdItem.Schmval;

            }
    
        }


        }
        if($scope.$parent.fmcgData.MSDFlag==1){
        $scope.GTotal=$scope.$parent.fmcgData.value;
        $scope.CatewsBillProd=$scope.$parent.fmcgData.SelectedItem;
          parseFloat($scope.GTotal); 
    }
      
    $scope.pRowCalc = function(ProdItem, CatItem) {

        pval = parseFloat(ProdItem.sample_qty);
        pNval = parseFloat(ProdItem.netweightvalue);
        pDis = parseFloat(ProdItem.discount_price);

        cTot = parseFloat(CatItem.SubTotal)
        cGTot = parseFloat($scope.GTotal);
        cGDisc = parseFloat($scope.GDisc);
        cGNW = parseFloat($scope.GNetW);
        ProdItem.OFQ=0
        if (isNaN(cTot)) cTot = 0;
        if (isNaN(cGTot)) cGTot = 0;
        if (isNaN(cGDisc)) cGDisc = 0;
        if (isNaN(cGNW)) cGNW = 0;

        if (isNaN(pval)) pval = 0;
        if (isNaN(pNval)) pNval = 0;
        if (isNaN(pDis)) pDis = 0;

            if($scope.fmcgData.vansales==1){
            if(ProdItem.rx_qty>ProdItem.dailyinvqty){
                    ProdItem.rx_qty=0;
                    Toast("Qty limit Exceded");
                return false;
            }
            }
        if ($scope.PromoValND != undefined && $scope.PromoValND == 1) {
            ProdItem.sample_qty = ProdItem.rx_qty * (ProdItem.Rate * ProdItem.OrdConv);
            ProdItem.netweightvalue = ProdItem.product_netwt * (ProdItem.rx_qty * ProdItem.OrdConv);


        } else {
            ProdItem.sample_qty = ProdItem.rx_qty * ProdItem.Rate;
            ProdItem.netweightvalue = ProdItem.product_netwt * ProdItem.rx_qty;

        }



        $scope.SchemeDets = fmcgLocalStorage.getData("SchemeDetails") || [];
        Schemes = $scope.SchemeDets.filter(function(a) {
            return (a.PCode == ProdItem.product && a.Scheme <= ProdItem.rx_qty);
        });
       
        ProdItem.Schmval = '';
        ProdItem.free = 0;
        ProdItem.discount_price = 0;
        ProdItem.schemedis=0;
        if (Schemes.length > 0 ) {
            if (Schemes[0].FQ != '' &&Schemes[0].FQ>0 ) {
                if (Schemes[0].pkg == 'Y')
                    ProdItem.Schmval = parseInt(parseInt((ProdItem.rx_qty / parseInt(Schemes[0].Scheme))) * parseInt(Schemes[0].FQ))
                else
                    ProdItem.Schmval = parseInt(parseFloat((ProdItem.rx_qty / parseInt(Schemes[0].Scheme))) * parseInt(Schemes[0].FQ))


                ProdItem.free = ProdItem.Schmval || 0;
                if(Schemes[0].schType=="Y"){
                    ProdItem.Fname=Schemes[0].OffProdNm;
                    ProdItem.FreeP_Code=Schemes[0].OffProd;
                }else{
                    ProdItem.Fname='';
                    ProdItem.FreeP_Code='';

                }
            } else {
                if (Schemes[0].Disc != '' && Schemes[0].Disc>0){

                    dis = ProdItem.sample_qty * (Schemes[0].Disc / 100);
                    ProdItem.schemedis=Schemes[0].Disc;

                ProdItem.Schmval = dis.toFixed(2);
                ProdItem.sample_qty = ProdItem.sample_qty - dis;
                ProdItem.discount_price = ProdItem.Schmval;
                }
                
          //ProdItem.discount=Schemes[0].Disc;
            }
        }
        if ($scope.OfferMode == 1) {
            ProdItem.discount_price = ProdItem.sample_qty * (ProdItem.discount / 100);
            ProdItem.sample_qty = ProdItem.sample_qty - ProdItem.discount_price;
        }

/*if ($scope.OfferMode == 1) {
            ProdItem.discount_price = ProdItem.sample_qty * (ProdItem.schemedis / 100);
            //ProdItem.sample_qty = ProdItem.sample_qty - ProdItem.discount_price;
        }*/
        if (ProdItem.sample_qty < 0) {
            ProdItem.sample_qty = 0;
            CatItem.SubTotal = (cTot - pval) + ProdItem.sample_qty;
            ProdItem.discount = 0;
            ProdItem.discount_price=0;
        } else {
            CatItem.SubTotal = (cTot - pval) + ProdItem.sample_qty;
        }

        CatItem.SubTotal = (cTot - pval) + ProdItem.sample_qty;
        $scope.GTotal = (cGTot - pval) + ProdItem.sample_qty;
        $scope.GNetW = (cGNW - pNval) + ProdItem.netweightvalue;
        $scope.GDisc = (cGDisc - pDis) + ProdItem.discount_price;

        $scope.GTotal = 0;


        for (var key in $scope.CatewsBillProd) {
            if ($scope.CatewsBillProd[key]['SubTotal'] != undefined && $scope.CatewsBillProd[key]['SubTotal'] > 0) {
                $scope.GTotal += $scope.CatewsBillProd[key]['SubTotal'];
            }

        }

        $scope.fmcgData.value = $scope.GTotal;
        $scope.fmcgData.netweightvaluetotal = $scope.GNetW;
        $scope.fmcgData.discount_price = $scope.GDisc;
        $scope.fmcgData.netamount = $scope.GTotal;


    }



    $scope.roundNumber = function(number, precision) {
        precision = Math.abs(parseInt(precision)) || 0;
        var multiplier = Math.pow(10, precision);
        return parseFloat(Math.round(number * multiplier) / multiplier).toFixed(2);
    }
    $scope.RateByCd = function(ProdCd, typ,caseorp) {

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        $scope.RateMode = userData.RateMode;


        if ($scope.fmcgData.inshopactivityopen != undefined && $scope.fmcgData.inshopactivityopen == true) {

            if ($scope.RateMode == 0 || typ > 1) {
                // var productRate = $scope.Product_State_Rates;
                var productRate = $scope.Product_Category_Rates;
                vRate = 0;
                for (var key in productRate) {
                    if (productRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productRate[key]['MRP_Rate'] : productRate[key]['DistCasePrice'];
                }
                return vRate;
            }
            if ($scope.RateMode == 1) {
                //  var productcatRate = $scope.Product_Category_Rates;
                var productcatRate = $scope.Product_State_Rates;
                vRate = 0;
                for (var key in productcatRate) {
                    if (productcatRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productcatRate[key]['MRP_Price'] : productcatRate[key]['Discount'];
                }
                return vRate;
            }
        } else if($scope.fmcgData.SupplierMaster!= undefined && $scope.fmcgData.SupplierMaster==true){
              if ($scope.RateMode == 0 || typ > 1) {
              var productRate = $scope.Product_State_Rates;
              //  var productRate = $scope.Product_Category_Rates;
                vRate = 0;
               for (var key in productRate) {
                    if (productRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (caseorp == 1) ? productRate[key]['SS_Case_Rate'] : productRate[key]['SS_Base_Rate'];
                }
                return vRate;
            }
            if ($scope.RateMode == 1) {
                //  var productcatRate = $scope.Product_Category_Rates;
                var productcatRate = $scope.Product_State_Rates;
                vRate = 0;
               for (var key in productRate) {
                    if (productRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (caseorp == 1) ? productRate[key]['SS_Case_Rate'] : productRate[key]['SS_Base_Rate'];
                }
                return vRate;
            }
        }


        else {
            if ($scope.RateMode == 0 || typ > 1) {
                var productRate = $scope.Product_State_Rates;
                vRate = 0;
                for (var key in productRate) {
                    if (productRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productRate[key]['Retailor_Price'] : productRate[key]['DistCasePrice'];
                }
                return vRate;
            }

            if ($scope.RateMode == 1) {
                var productcatRate = $scope.Product_Category_Rates;
                vRate = 0;
                for (var key in productcatRate) {
                    if (productcatRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productcatRate[key]['RP_Base_Rate'] : productcatRate[key]['Discount'];
                }
                return vRate;
            }
        }



    };



    $scope.RateBypice = function(ProdCd, typ) {

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        $scope.RateMode = userData.RateMode;


        if ($scope.fmcgData.inshopactivityopen != undefined && $scope.fmcgData.inshopactivityopen == true) {

            if ($scope.RateMode == 0 || typ > 1) {
                var productRate = $scope.Product_State_Rates;
                vRate = 0;
                for (var key in productRate) {
                    if (productRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productRate[key]['MRP_Price'] : productRate[key]['Distributor_Price'];
                }
                return vRate;
            }
            if ($scope.RateMode == 1) {
                var productcatRate = $scope.Product_Category_Rates;
                vRate = 0;
                for (var key in productcatRate) {
                    if (productcatRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productcatRate[key]['MRP_Price'] : productcatRate[key]['Discount'];
                }
                return vRate;
            }
        } else {
            if ($scope.RateMode == 0 || typ > 1) {
                var productRate = $scope.Product_State_Rates;
                vRate = 0;
                for (var key in productRate) {
                    if (productRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productRate[key]['Retailor_Price'] : productRate[key]['Distributor_Price'];
                }
                return vRate;
            }

            if ($scope.RateMode == 1) {
                var productcatRate = $scope.Product_Category_Rates;
                vRate = 0;
                for (var key in productcatRate) {
                    if (productcatRate[key]['Product_Detail_Code'] == ProdCd)
                        vRate = (typ == 1) ? productcatRate[key]['RP_Base_Rate'] : productcatRate[key]['Discount'];
                }
                return vRate;
            }
        }


    };


    $scope.fmcgData.value1 = $scope.NetweightVal;
    $scope.fmcgData.value2 = $scope.OrderVal;
    if ($scope.$parent.fmcgData.customer.selected.id == "3") {
        $scope.fmcgData.value1 = 0;
        $scope.fmcgData.value2 = 0;
    }
    if ($scope.$parent.fmcgData.customer) {

        if ($scope.$parent.fmcgData.inshopactivityopen) {
            $scope.$parent.navTitle = "Inshop Activity";
        } else if ($scope.$parent.fmcgData.inshopactivitycheckin) {
            $scope.$parent.navTitle = "Inshop CheckIn";
        } else if($scope.$parent.fmcgData.SupplierMaster){
         $scope.$parent.navTitle = "Super Stockist";

        }

        else {
            $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;

        }
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.setAllow();
    if ($scope.prod == 0)
        $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
    else {
        if ($scope.fmcgData.editableOrder != 1)
            $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
        else {
            if ($scope.$parent.fmcgData.productSelectedList == undefined)
                $scope.$parent.fmcgData.productSelectedList = [];
            if ($scope.$parent.fmcgData.productSelectedList.length > 0) {
                productSelectedList = [];

                for (i = 0; i < $scope.products.length; i++) {
                    val = 0;

                    for (j = 0; j < $scope.$parent.fmcgData.productSelectedList.length; j++) {
                        if ($scope.products[i]['id'] == $scope.$parent.fmcgData.productSelectedList[j]['product']) {
                            $scope.$parent.fmcgData.productSelectedList[j].Rate = $scope.roundNumber($scope.RateByCd($scope.products[i].id, $scope.$parent.fmcgData.customer.selected.id), 2);
                            val = 1;
                        }
                    }
                    if (val == 0) {
                        var productData = {};
                        productData.product = $scope.products[i].id;
                        productData.product_Nm = $scope.products[i].name;
                        productData.HSN = $scope.products[i].HSN;
                        productData.Rate = $scope.roundNumber($scope.RateByCd($scope.products[i].id, $scope.$parent.fmcgData.customer.selected.id), 2);

                        productData.Schmval = CurrentData[i].Schmval || 0;
                        productData.rx_qty = $scope.products[i].rx_qty || 0;
                        productData.sample_qty = $scope.products[i].sample_qty || 0;
                        productData.discount = $scope.products[i].discount || 0;
                        productData.discount_price = $scope.products[i].discount_price || 0;
                        productData.free = $scope.products[i].free || 0;
                        productData.cb_qty = $scope.products[i].cb_qty || 0;
                        productData.pieces = $scope.products[i].pieces || 0;
                        productData.recv_qty = $scope.products[i].recv_qty || 0;
                        productData.product_netwt = $scope.products[i].product_netwt || 0;
                        productData.netweightvalue = $scope.products[i].netweightvalue || 0;
                        if ($scope.view_STOCKIST == 1) {
                            productData.conversionQty = Number($scope.products[i].conversionQty) || 0
                        }
                        $scope.$parent.fmcgData.productSelectedList.push(productData);
                    }

                }

            }
        }
        if ($scope.$parent.fmcgData.productSelectedList.length == 0) {
            CurrentData = $scope.products;
            for (i = 0; i < CurrentData.length; i++) {
                var productData = {};
                productData.product = CurrentData[i].id;
                productData.product_Nm = CurrentData[i].name;
                productData.HSN = CurrentData[i].HSN;
                productData.Rate = $scope.roundNumber($scope.RateByCd(CurrentData[i].id, $scope.$parent.fmcgData.customer.selected.id), 2);
                productData.Schmval = CurrentData[i].Schmval || 0;
                productData.rx_qty = CurrentData[i].rx_qty || 0;
                productData.sample_qty = CurrentData[i].sample_qty || 0;
                productData.discount = CurrentData[i].discount || 0;
                productData.discount_price = CurrentData[i].discount_price || 0;
                productData.free = CurrentData[i].free || 0;
                productData.cb_qty = CurrentData[i].cb_qty || 0;
                productData.pieces = CurrentData[i].pieces || 0;
                productData.recv_qty = CurrentData[i].recv_qty || 0;
                productData.product_netwt = CurrentData[i].product_netwt || 0;
                productData.netweightvalue = CurrentData[i].netweightvalue || 0;
                if ($scope.view_STOCKIST == 1) {
                    productData.conversionQty = Number(CurrentData[i].conversionQty) || 0
                }
                $scope.$parent.fmcgData.productSelectedList.push(productData);
            }
        }
    }
    $scope.addProduct = function(selected) {
            var productData = {};
            productData.product = selected;
            productData.product_Nm = (selected.toString() != "") ? $filter('getValueforID')(selected, $scope.products).name : "";
            productData.HSN = (selected.toString() != "") ? $filter('getValueforID')(selected, $scope.products).HSN : "";
            productData.Rate = $scope.roundNumber($scope.RateByCd(selected.id, $scope.$parent.fmcgData.customer.selected.id), 2);
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
        //  $scope.choice = 'Free';

    $scope.changeMode = function(ch) {
        //console.log('SampNeed1:' + $scope.DrSmpQ + " - " + $scope.SmplCap);
        if ($scope.DrSmpQ == 0)
            $scope.condValue = false;
        else
            $scope.condValue = true;
        if ($scope.prod == 0)
            $scope.prodShow = true;
        else
            $scope.prodShow = false;
        $scope.RetCB = ($scope.RetCBNd == 1) ? true : false;
        $scope.fmcgData.rateType = ch;
        OfrNd = false;
        SchmNd = false;
        if ($scope.OfferMode == 1 && $scope.TDisc == 1) OfrNd = true;
        if ($scope.OfferMode == 2) SchmNd = true; //$scope.TDisc==1

        if ($scope.fmcgData.rateType == "discount")
            rateModeName = "Disc";
        if ($scope.fmcgData.rateType == "free")
            rateModeName = "Free";
        edtable = false;
        if ($scope.RateEditable == 1) {
            edtable = true;
        }
        MfgNeed = false;
        if ($scope.MfgDtNeed == 1) {
            MfgNeed = true;
        }
        RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
        SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
        $scope.columnsSelected = [{
            field: 'product_Nm',
            subfield: 'Mfg_Date',
            showSubField: MfgNeed,
            displayName: 'Product',
            enableCellEdit: false,
            cellTemplate: 'partials/productCellTemplate.html',
            cellClass: 'grid-alignl'
        }, {
            field: 'Rate',
            displayName: 'Rate',
            enableCellEdit: edtable,
            editableCellTemplate: 'partials/cellEditTemplate.html',
            width: 70,
            cellClass: 'grid-alignr'
        }, {
            field: 'rx_qty',
            displayName: RxCap,
            enableCellEdit: true,
            editableCellTemplate: "partials/cellEditTemplate.html",
            width: 50,
            cellClass: 'grid-alignr'
        }, {
            field: $scope.fmcgData.rateType,
            displayName: rateModeName,
            enableCellEdit: true,
            editableCellTemplate: "partials/cellEditTemplate.html",
            width: 40,
            "visible": OfrNd,
            cellClass: 'grid-alignr'
        }, {
            field: 'Schmval',
            displayName: 'Sch',
            enableCellEdit: false,
            editableCellTemplate: "partials/cellEditTemplate.html",
            width: 40,
            "visible": SchmNd,
            cellClass: 'grid-alignr'
        }, {
            field: 'sample_qty',
            displayName: SmplCap,
            cellTemplate: "partials/cellEditTemplate1.html",
            width: 70,
            "visible": $scope.condValue,
            cellClass: 'grid-alignr'
        }, {
            field: 'cb_qty',
            displayName: 'Cl',
            enableCellEdit: true,
            editableCellTemplate: "partials/cellEditTemplate.html",
            width: 50,
            "visible": $scope.RetCB,
            cellClass: 'grid-alignr'
        }, {
            field: 'remove',
            displayName: '',
            enableCellEdit: false,
            cellTemplate: 'partials/removeButton.html',
            width: 50,
            "visible": $scope.prodShow
        }];
        var productRate = $scope.Product_State_Rates;
        var factor = "1" + Array(+(2 > 0 && 2 + 1)).join("0");
        var values = 0;
        for (i = 0; i < $scope.$parent.fmcgData.productSelectedList.length; i++) {
            var distributerPrice = 0;
            for (var key in productRate) {
                if (productRate[key]['Product_Detail_Code'] == $scope.$parent.fmcgData.productSelectedList[i]['product'])
                    distributerPrice = productRate[key]['Retailor_Price'];
            }
            distributerPrice = $scope.$parent.fmcgData.productSelectedList[i]['Rate'];
            val = $scope.$parent.fmcgData.productSelectedList[i]['rx_qty'] * distributerPrice;
            $scope.$parent.fmcgData.productSelectedList[i]['sample_qty'] = Math.round(val * factor) / factor;
            $scope.$parent.fmcgData.productSelectedList[i]['discount'] = 0;
            $scope.$parent.fmcgData.productSelectedList[i]['free'] = 0;
            $scope.$parent.fmcgData.productSelectedList[i]['discount_price'] = 0;
            values = +$scope.$parent.fmcgData.productSelectedList[i]['sample_qty'] + +values;
        }
        $scope.fmcgData.value = Math.round(values * factor) / factor;
        $scope.fmcgData.discount_price = 0;

        $scope.gridOptions = {
            data: 'fmcgData.productSelectedList',
            rowHeight: 50,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            enableRowSelection: false,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: 'columnsSelected'
        };
    }

    scrTyp = $scope.$parent.fmcgData.customer.selected.id;
    if (scrTyp == 1 || scrTyp == 4) {
        $scope.fmcgData.rateType = "free";
        $scope.changeMode($scope.fmcgData.rateType);
        /*console.log('SampNeed:'+$scope.DrSmpQ);
        if ($scope.DrSmpQ == 0)
            $scope.condValue = false;
        else
            $scope.condValue = true;
        if ($scope.prod == 0)
            $scope.prodShow = true;
        else
            $scope.prodShow = false;
        if ($scope.fmcgData.rateType == undefined) {
            rateTypeFlag = false;
            rateModeName = "";
        }
        else {
            if ($scope.fmcgData.rateType == "discount")
                rateModeName = "Disc";
            if ($scope.fmcgData.rateType == "free")
                rateModeName = "Free";
            rateTypeFlag = true;
        }
        RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
        SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
        $scope.columnsSelected = [
                 { field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html', cellClass: 'grid-alignl' },
                 { field: 'Rate', displayName: 'Rate', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html', width: 60, cellClass: 'grid-alignr' },
                 { field: 'rx_qty', displayName: RxCap, enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60, cellClass: 'grid-alignr' },
                 { field: $scope.fmcgData.rateType, displayName: rateModeName, enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60, cellClass: 'grid-alignr' },
                 { field: 'sample_qty', displayName: SmplCap, cellTemplate: "partials/cellEditTemplate1.html", width: 90, "visible": $scope.condValue, cellClass: 'grid-alignr' },
                 { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50, "visible": $scope.prodShow }
        ];

        $scope.gridOptions = {
            data: 'fmcgData.productSelectedList',
            rowHeight: 50,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            enableRowSelection: false,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: 'columnsSelected'
        };*/

    } else {
        if ($scope.prod == 0)
            $scope.prodShow = true;
        else
            $scope.prodShow = false;
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
            columnDefs: [{
                field: 'product_Nm',
                displayName: 'Product',
                enableCellEdit: false,
                cellTemplate: 'partials/productCellTemplate.html',
                cellClass: 'grid-alignl'
            }, {
                field: 'Rate',
                displayName: 'Rate',
                enableCellEdit: false,
                cellTemplate: 'partials/productCellTemplate.html',
                width: 70,
                cellClass: 'grid-alignr'
            }, {
                field: 'rx_qty',
                displayName: QCap,
                enableCellEdit: true,
                editableCellTemplate: "partials/cellEditTemplate5.html",
                width: 60,
                cellClass: 'grid-alignr'
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50,
                "visible": $scope.prodShow
            }]
        };
    }
    $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
    if ($scope.$parent.fmcgData.cluster != undefined)
        var id = $scope.$parent.fmcgData.cluster.selected.id;
    else {
        var id = $scope.Mypl[0]['clusterid'];
        $scope.fmcgData.doctor = {};
        $scope.fmcgData.doctor.selected = {};
        if ($scope.Myplns[0].FWFlg == 'IH') {
            if ($scope.Myplns[0].custid == undefined) {
                $scope.Myplns[0].custid = 'C-1';
            }
            $scope.fmcgData.doctor.selected.id = $scope.Myplns[0].custid;
            $scope.fmcgData.doctor.name = $scope.Myplns[0].custName;
            $scope.fmcgData.doctor.address = $scope.Myplns[0].address;
            $scope.fmcgData.doctor.Mobile_Number = '';
        } else {
            $scope.fmcgData.doctor.selected.id = "o_" + $scope.Myplns[0].clusterid;
            $scope.fmcgData.doctor.name = "Retailer Of " + $scope.Myplns[0].ClstrName;
            $scope.fmcgData.routeWise = 0;
        }
        if ($scope.view_MR == 1)
            $scope.cDataID = '_' + $scope.sfCode;
        else
            $scope.cDataID = '_' + $scope.Myplns[0].subordinateid; //alert($scope.cDataID);
        $scope.loadDatas(false, $scope.cDataID);

        $scope.$parent.fmcgData.subordinate = {};
        $scope.$parent.fmcgData.subordinate.selected = {};
        $scope.$parent.fmcgData.subordinate.selected.id = $scope.cDataID.replace(/_/g, '');

        $scope.$parent.fmcgData.stockist = {};
        $scope.$parent.fmcgData.stockist.selected = {};
        $scope.$parent.fmcgData.stockist.selected.id = $scope.Myplns[0].stockistid;
        $scope.$parent.fmcgData.stockist.name = $scope.Myplns[0].stkName;

        $scope.$parent.fmcgData.cluster = {};
        $scope.$parent.fmcgData.cluster.selected = {};
        $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;
        $scope.$parent.fmcgData.cluster.name = $scope.Myplns[0].ClstrName;
        var tDate = new Date();
        if (!$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.isDraft) {
            if ($scope.cComputer) {
                $scope.$parent.fmcgData.entryDate = tDate;
                $scope.$parent.fmcgData.modifiedDate = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.$parent.fmcgData.entryDate = tDate;
                    $scope.$parent.fmcgData.modifiedDate = tDate;
                });
            }
        } else {
            if ($scope.cComputer) {
                $scope.$parent.fmcgData.modifiedDate = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.$parent.fmcgData.modifiedDate = tDate;
                });
            }
            var tDate = new Date($scope.$parent.fmcgData.entryDate);
            $scope.$parent.fmcgData.entryDate = tDate;
        }
    }
    var towns = $scope.myTpTwns;
    for (var key in towns) {
        if (towns[key]['id'] == id)
            $scope.fmcgData.rootTarget = towns[key]['target'];
    }


    $scope.findDisRate = function() {
        var productRate = $scope.Product_State_Rates;
        var product = this.row.entity.product;

        var factor = "1" + Array(+(2 > 0 && 2 + 1)).join("0");
        var distributerPrice = 0;
        for (var key in productRate) {
            if (productRate[key]['Product_Detail_Code'] == product)
                distributerPrice = productRate[key]['DistCasePrice'];
        }
        val = (this.row.entity.rx_qty) * distributerPrice;
        this.row.entity.sample_qty = Math.round(val * factor) / factor;
        if ($scope.fmcgData.rateType == undefined) {
            this.row.entity.discount_price = 0;
        } else {
            if ($scope.fmcgData.rateType == "discount") {
                discount = this.row.entity.discount;
                this.row.entity.discount_price = this.row.entity.sample_qty * (discount / 100);
                this.row.entity.sample_qty = this.row.entity.sample_qty - this.row.entity.discount_price;
            }
        }

        var products = $scope.$parent.fmcgData.productSelectedList;

        var values = 0;
        var discount_price_all = 0;
        var netweightvaluetotal = 0;
        for (var key in products) {
            values = +products[key]['sample_qty'] + values;
            discount_price_all = +products[key]['discount_price'] + discount_price_all;
        }
        $scope.fmcgData.value2 = 2
        $scope.fmcgData.value = Math.round(values * factor) / factor;
        $scope.fmcgData.discount_price = discount_price_all;
    };

    $scope.findRate = function() {
        var productRate = $scope.Product_State_Rates;
        var product = this.row.entity.product;

        var factor = "1" + Array(+(2 > 0 && 2 + 1)).join("0");
        var distributerPrice = 0;
        var netPrice = this.row.entity.product_netwt;
        distributerPrice = this.row.entity.Rate;
        /*for (var key in productRate) {
            if (productRate[key]['Product_Detail_Code'] == product)
                distributerPrice = productRate[key]['Retailor_Price'];
        }*/
        val = (this.row.entity.rx_qty) * distributerPrice;
        val1 = (this.row.entity.rx_qty) * netPrice;
        this.row.entity.sample_qty = Math.round(val * factor) / factor;
        this.row.entity.netweightvalue = Math.round(val1 * factor) / factor;
        if ($scope.fmcgData.rateType == undefined) {
            this.row.entity.discount_price = 0;
        } else {
            if ($scope.fmcgData.rateType == "discount") {
                discount = this.row.entity.discount;
                this.row.entity.discount_price = this.row.entity.sample_qty * (discount / 100);
                this.row.entity.sample_qty = this.row.entity.sample_qty - this.row.entity.discount_price;
            }
        }
        var products = $scope.$parent.fmcgData.productSelectedList;
        //console.log(products)
        var values = 0;
        var netweightvaluetotal = 0;
        var discount_price_all = 0;
        for (var key in products) {
            values = +products[key]['sample_qty'] + +values;
            netweightvaluetotal = +products[key]['netweightvalue'] + +netweightvaluetotal;
            discount_price_all = +products[key]['discount_price'] + discount_price_all;
        }
        $scope.fmcgData.value = Math.round(values * factor) / factor;
        $scope.fmcgData.netweightvaluetotal = Math.round(netweightvaluetotal * factor) / factor;
        $scope.fmcgData.discount_price = discount_price_all;
        qt = this.row.entity.rx_qty
        $scope.SchemeDets = fmcgLocalStorage.getData("SchemeDetails") || [];
        Schemes = $scope.SchemeDets.filter(function(a) {
            return (a.PCode == product && a.Scheme <= qt);
        });
        this.row.entity.Schmval = '';
        if (Schemes.length > 0) {
            if (Schemes[0].FQ != '') {
                if (Schemes[0].pkg == 'Y')
                    this.row.entity.Schmval = parseInt(parseInt((this.row.entity.rx_qty / parseInt(Schemes[0].Scheme))) * parseInt(Schemes[0].FQ))
                else
                    this.row.entity.Schmval = parseInt(parseFloat((this.row.entity.rx_qty / parseInt(Schemes[0].Scheme))) * parseInt(Schemes[0].FQ))


                this.row.entity.free = this.row.entity.Schmval || 0;

            } else {
                if (Schemes[0].Disc != '')
                    dis = this.row.entity.sample_qty * (Schemes[0].Disc / 100)
                this.row.entity.Schmval = dis.toFixed(2);
                this.row.entity.sample_qty = this.row.entity.sample_qty - dis
                this.row.entity.discount_price = this.row.entity.Schmval

            }
        }

        console.log("$scope.fmcgData.netweightvaluetotal" + $scope.fmcgData.netweightvaluetotal);
        console.log("scope.fmcgData.value" + $scope.fmcgData.value);

        $scope.fmcgData.netamount = $scope.fmcgData.value;




        if (!($scope.fmcgData.discounttt == undefined)) {
            var perc = (($scope.fmcgData.discounttt / 100) * $scope.fmcgData.value).toFixed(2);
            $scope.fmcgData.netamount = Math.floor($scope.fmcgData.value - perc);
            $scope.fmcgData.dis = perc;
        }

    };

    $scope.reCalcTotol = function() {
        var products = $scope.fmcgData.productSelectedList;
        val = 0;
        for (il = 0; il < products.length; il++) {
            item = products[il];
            console.log(item);
            val += item.sample_qty;
        }
        $scope.fmcgData.value = val.toFixed(2);

        if (!($scope.fmcgData.discounttt == undefined)) {
            var perc = (($scope.fmcgData.discounttt / 100) * $scope.fmcgData.value).toFixed(2);
            $scope.fmcgData.netamount = Math.floor($scope.fmcgData.value - perc);
            $scope.fmcgData.dis = perc;
        }

    };

    $scope.netAmount = function() {
        $scope.fmcgData.netamount = $scope.fmcgData.value - $scope.fmcgData.discounttt;
        if (!($scope.fmcgData.discounttt == undefined)) {
            var perc = (($scope.fmcgData.discounttt / 100) * $scope.fmcgData.value).toFixed(2);
            $scope.fmcgData.netamount = ($scope.fmcgData.value - perc).toFixed(2);
            $scope.fmcgData.dis = perc;
        }



    }



    $scope.removeRow = function() {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.productSelectedList.splice(index, 1);
        var products = $scope.$parent.fmcgData.productSelectedList;
        var values = 0;
        netweightvaluetotal = 0;
        for (var key in products) {
            values = +products[key]['sample_qty'] + values;
            netweightvaluetotal = products[key]['netweightvalue'] + netweightvaluetotal;
        }
        $scope.fmcgData.value = values;
        $scope.fmcgData.netweightvaluetotal = netweightvaluetotal;

        $scope.fmcgData.value = values;
        $scope.fmcgData.netamount = $scope.fmcgData.value - $scope.fmcgData.discounttt;

        $scope.fmcgData.netweightvaluetotal = netweightvaluetotal;


    };
    $scope.save = function() {
        $scope.saveToDraftO();
        $state.go('fmcgmenu.home');
    }
    $scope.editOrder = function() {
        var details = {};
        details.Products = [];
        for (var i = 0, len = $scope.fmcgData.productSelectedList.length; i < len; i++) {
            if ($scope.fmcgData.productSelectedList[i]['rx_qty'] > 0) {
                details.Products.push($scope.fmcgData.productSelectedList[i])
            }
        }
        details.POB = $scope.fmcgData.pob;
        details.Value = $scope.fmcgData.value;
        details.Cust_Code = $scope.fmcgData.Cust_Code;
        details.DCR_Code = $scope.fmcgData.DCR_Code;
        details.Trans_Sl_No = $scope.fmcgData.Trans_Sl_No;
        details.Route = $scope.fmcgData.route;
        var id = details.Route;
        var towns = $scope.myTpTwns;
        for (var key in towns) {
            if (towns[key]['id'] == id)
                details.target = towns[key]['target'];
        }
        details.rateMode = $scope.fmcgData.rateType;
        details.discount_price = $scope.fmcgData.discount_price;
        details.Stockist = $scope.fmcgData.Stockist;
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.updateDCRData('POST', 'dcr/updateProducts', details).success(function(response) {
            if (response.success) {
                $ionicLoading.hide();
                Toast("call Updated Successfully");
                $state.go('fmcgmenu.home');
            }
        }).error(function() {
            $ionicLoading.hide();

            Toast("No Internet Connection!");
        });
    }
    $scope.submit = function() {

        //  $scope.fmcgData.netamount = $scope.GTotal;
        $scope.fmcgData.ORvalues = $scope.GTotal;


        if ((',1,').indexOf(',' + $scope.fmcgData.customer.selected.id + ',') > -1) {
            if ($scope.fmcgData.productSelectedList.length > 0) {
                console.log($scope.fmcgData.productSelectedList.length);
                if ($scope.fmcgData.stockist == undefined || $scope.fmcgData.stockist.selected.id == "") {
                    Toast('Select the ' + $scope.StkCap + ' Name');
                    return false;
                }
            }
        }
        if ((',3,').indexOf(',' + $scope.fmcgData.customer.selected.id + ',') > -1 && $scope.Supplier_Master==1 && $scope.view_MR!=1) {
                    if ($scope.fmcgData.productSelectedList.length > 0) {
                        console.log($scope.fmcgData.productSelectedList.length);
                        if ($scope.fmcgData.Super_Stck_code == undefined || $scope.fmcgData.Super_Stck_code.selected.id == "") {
                            Toast('Select the Supply through');
                            return false;
                        }
                    }
                }



        $scope.fmcgData.productSelectedList = [];
        for (il = 0; il < $scope.CatewsBillProd.length; il++) {
            if ($scope.CatewsBillProd[il].Products != undefined) {
                var Prods = $scope.CatewsBillProd[il].Products.filter(function(a) {
                    return (a.rx_qty > 0 || a.free > 0 || a.cb_qty > 0 || a.PromoVal > 0 || a.Prx_qty >0);
                })
                if (Prods.length > 0) {
                    $scope.fmcgData.productSelectedList = $scope.fmcgData.productSelectedList.concat(Prods);

                        if($scope.fmcgData.vansales==1){
                    $scope.DailyBegin = fmcgLocalStorage.getData("DailyBegin") || [];


                    if (Prods.length > 0) {
                        for (var i = 0; i < Prods.length; i++) {

                            if ($scope.DailyBegin.InvProducts.length > 0) {
                                var dailyinvfilter = $scope.DailyBegin.InvProducts.filter(function(a) {
                                    return (a.id == Prods[i].id)
                                });

                                if (dailyinvfilter.length > 0) {

                                    for (var ii = 0; ii < $scope.DailyBegin.InvProducts.length; ii++) {
                                        if($scope.DailyBegin.InvProducts[ii].id == Prods[i].id){
                                            if(Prods[i].rx_qty>$scope.DailyBegin.InvProducts[ii].Qty){
                                                Toast("product  limit Exceded"+Prods[i].product_Nm);
                                                 return false;
                                            }
                                           
                                        $scope.DailyBegin.InvProducts[ii].Qty = ($scope.DailyBegin.InvProducts[ii].Qty - Prods[i].rx_qty)
                                    }
                                }

                                }
                            }

                        }



                    }

                         localStorage.removeItem("DailyBegin");
                        fmcgLocalStorage.createData("DailyBegin", $scope.DailyBegin);
             

}



                }
            }
        }
        $scope.reCalcTotol();
        if ($scope.fmcgData.InshopActivitySEndToServer != undefined) {
            fmcgAPIservice.addMAData('POST', 'dcr/save', "32", $scope.fmcgData).success(function(response) {
                if (response.success)
                    $state.go('fmcgmenu.home');
                Toast("Added Successfully");
                if ($scope.AppTyp == 1) {
                    $scope.clearIdividual(0, 1);
                } else {
                    $scope.clearIdividual(3, 1);
                }
                $scope.data = {};

            }).error(function() {

                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });

        } else if ($scope.fmcgData.FLDEMO != undefined && $scope.fmcgData.FLDEMO == 1) {
            fmcgAPIservice.addMAData('POST', 'dcr/save', "37", $scope.fmcgData).success(function(response) {
                if (response.success)
                    $state.go('fmcgmenu.home');
                Toast("Added Successfully");
                if ($scope.AppTyp == 1) {
                    $scope.clearIdividual(0, 1);
                } else {
                    $scope.clearIdividual(3, 1);
                }
                $scope.data = {};

            }).error(function() {

                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });

        } 

       else if ($scope.fmcgData.NewContact != undefined && $scope.fmcgData.NewContact == 1) {
            fmcgAPIservice.addMAData('POST', 'dcr/save', "45", $scope.fmcgData).success(function(response) {
                if (response.success)
                    $state.go('fmcgmenu.home');
                Toast("Added Successfully");
                if ($scope.AppTyp == 1) {
                    $scope.clearIdividual(0, 1);
                } else {
                    $scope.clearIdividual(3, 1);
                }
                $scope.data = {};

            }).error(function() {

                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });

        } 
        else if ($scope.fmcgData.InshopCheckIn != undefined) {


            fmcgAPIservice.addMAData('POST', 'dcr/save', "40", $scope.fmcgData).success(function(response) {
                if (response.success)
                    $state.go('fmcgmenu.home');
                Toast("Added Successfully");
                if ($scope.AppTyp == 1) {
                    $scope.clearIdividual(0, 1)
                } else {
                    $scope.clearIdividual(3, 1);
                }
                $scope.data = {};

            }).error(function() {

                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });

        } 

/*else if($scope.fmcgData.SupplierMaster!=undefined && $scope.fmcgData.SupplierMaster!=false){
 fmcgAPIservice.addMAData('POST', 'dcr/save', "46", $scope.fmcgData).success(function(response) {
                if (response.success)
                    $state.go('fmcgmenu.home');
                 Toast("Order Submitted Successfully");

                if ($scope.AppTyp == 1) {
                    $scope.clearIdividual(0, 1)
                } else {
                    $scope.clearIdividual(3, 1);
                }
                $scope.data = {};

            }).error(function() {

                Toast("No Internet Connection! Try Again.");
                $ionicLoading.hide();
            });
}
  */      else {


            $scope.ClFilter = $scope.fmcgData.Products.filter(function(a) {
                 return (a.cb_qty >0);
            });

           if( $scope.$parent.fmcgData.MSDFlag!=1){

            if (($scope.fmcgData.netamount != undefined && $scope.fmcgData.netamount > 0  )  ||   $scope.ClFilter.length>0 || $scope.fmcgData.Incentive==1 ) {
              
               if($scope.EventCapNd==1 && ($scope.fmcgData.photosList==undefined ||$scope.fmcgData.photosList.length==0)){

                Toast("Take a photo");
                return false;
               }
            $scope.fmcgData.submitdis=true;
            $scope.date = new Date();
            if($scope.fmcgData.Checkin==undefined){
                $scope.fmcgData.Checkin= $scope.date.getHours()+":"+$scope.date.getMinutes()+":"+$scope.date.getSeconds();
            }
            $scope.fmcgData.Checkout= $scope.date.getHours()+":"+$scope.date.getMinutes()+":"+$scope.date.getSeconds();
            $scope.fmcgData.Checkoutdurations=Timediff($scope.fmcgData.Checkin,$scope.fmcgData.Checkout);
            $scope.$emit('finalSubmit');

                function Timediff(start, end) {
                    start = start.split(":");
                    end = end.split(":");
                    var startDate = new Date(0, 0, 0, start[1], start[2], 0);
                    var endDate = new Date(0, 0, 0, end[1], end[2], 0);
                    var diff = endDate.getTime() - startDate.getTime();
                    var hours = Math.floor(diff / 1000 / 60 / 60);
                    diff -= hours * 1000 * 60 * 60;
                    var minutes = Math.floor(diff / 1000 / 60);
                    
                    return (hours < 9 ? "0" : "") + hours +'Minutes'+ ":" + (minutes < 9 ? "0" : "") + minutes+'Seconds';
                }
            } else {
                Toast("Select The Product");
                 //$scope.$emit('finalSubmit');
            }
        }else{
             
        if($scope.fmcgData.netamount != undefined && $scope.fmcgData.netamount > 0 ||   $scope.ClFilter.length>0 || $scope.fmcgData.Incentive==1){
         
             $scope.MSDLocal = fmcgLocalStorage.getData($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES+$scope.precall.cluster.selected.id) || [];
             $scope.$parent.fmcgData.MSDFlag = 0;
             $scope.SupplyThrow=undefined;
            var saveposition = [];
            if ($scope.$parent.fmcgData.customer.selected.id == 1) {
                 var SecondarySelection = fmcgLocalStorage.getData("SecondaryDate" + $scope.MSDDATES+$scope.precall.cluster.selected.id) || [];

                for (var ii = 0; ii < $scope.MSDLocal.length; ii++) {
                    if ($scope.$parent.fmcgData.OrderId == $scope.MSDLocal[ii].doctor.selected.id) {
                        saveposition.push(ii);
                    }
                }
                    for (var ii = 0; ii < SecondarySelection.length; ii++) {
                        if ($scope.fmcgData.doctor.selected.id == SecondarySelection[ii].id) {
                            SecondarySelection[ii].Ordered=1;
                        }

                    }
                                fmcgLocalStorage.createData("SecondaryDate" + $scope.MSDDATES+$scope.precall.cluster.selected.id,SecondarySelection);
            } else {
                var SecondarySelection = fmcgLocalStorage.getData("PrimaryDate" + $scope.MSDDATES+$scope.precall.cluster.selected.id) || [];

                for (var ii = 0; ii < $scope.MSDLocal.length; ii++) {
                    if ($scope.$parent.fmcgData.OrderId == $scope.MSDLocal[ii].stockist.selected.id) {
                        saveposition.push(ii);
                    }
                }
                    for (var ii = 0; ii < SecondarySelection.length; ii++) {
                        if ($scope.fmcgData.stockist.selected.id == SecondarySelection[ii].id) {
                            SecondarySelection[ii].Ordered=1;
                        }

                    }
                     fmcgLocalStorage.createData("PrimaryDate" + $scope.MSDDATES+$scope.precall.cluster.selected.id,SecondarySelection);

            }

                for (var i = saveposition.length - 1; i >= 0; i--) {
                    $scope.MSDLocal.splice(saveposition[i], 1);
                }

            $scope.fmcgData.entryDate = $scope.$parent.MSDDATES;
            $scope.fmcgData.SelectedItem=$scope.CatewsBillProd;
            $scope.MSDLocal.push($scope.fmcgData);
            localStorage.removeItem($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES+$scope.precall.cluster.selected.id);
            fmcgLocalStorage.createData($scope.$parent.fmcgData.customer.selected.name + $scope.$parent.MSDDATES+$scope.precall.cluster.selected.id, $scope.MSDLocal);
            Toast("Order Submitted Successfully");
            $scope.fmcgData.netamount = 0;

            $scope.fmcgData.value = 0;
            $state.go('fmcgmenu.MissedOrderPage');
            }else{
                 Toast("Select The Product");
            }
                    }

                    }

                }

    $scope.goBack = function() {

        if ($scope.fmcgData.InshopActivitySEndToServer != undefined) {
            $state.go('fmcgmenu.InshopActivity');
        } else if ($scope.fmcgData.FLDEMO != undefined && $scope.fmcgData.FLDEMO == 1) {
            $state.go('fmcgmenu.FieldDemoActivity');
        } else if ($scope.fmcgData.InshopCheckIn != undefined) {
            $state.go('fmcgmenu.InshopCheckIn');
        }else if($scope.$parent.fmcgData.MSDFlag==1){
         $state.go('fmcgmenu.MissedOrderPage');
        }
        else if($scope.fmcgData.SupplierMaster != undefined){
        $state.go('fmcgmenu.SupplierMaster');
          }else if($scope.$parent.fmcgData.NewAi!=undefined){
            $state.go('fmcgmenu.home');
          }
        else{
            $state.go('fmcgmenu.screen2');
            $scope.fmcgData.value1 = 0;
            $scope.fmcgData.value2 = 0;

        }

    };
    $scope.goNext = function() {
        $scope.fmcgData.value1 = 0;
        $scope.fmcgData.value2 = 0;
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



.controller('screen2Ctrl', function($rootScope, $scope, $state, $timeout, $filter, $ionicModal,$location, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.fmcgData.value1 = 0;
        $scope.fmcgData.value2 = 0;
         $scope.fmcgData.submitdis=false;
        if ($scope.Myplns.length == 0) {
            $state.go('fmcgmenu.myPlan');
        }


        $scope.resetfBx = function(x, ci) {
            if (x.coll_amt == '0' && ci == 0) x.coll_amt = '';
        }
        $scope.resetlBx = function(x, ci) {
            if (x.coll_amt == '' && ci == 0) x.coll_amt = 0;
        }

        var loginData = {};
        loginData.LOGIN = false;
        window.localStorage.setItem("AddNewRetailer", JSON.stringify(loginData.LOGIN));

        $scope.date = new Date();
        $scope.fmcgData.Checkin=  $scope.date.getHours()+":"+$scope.date.getMinutes()+":"+$scope.date.getSeconds();

        $scope.difrentdate = function(x) {
            var tDate = new Date();

            var eDate = new Date(x.date);

            eedate = (eDate.getMonth() + 1) + "-" + eDate.getDate() + "-" + eDate.getFullYear();

            sdate = (tDate.getMonth() + 1) + "-" + tDate.getDate() + "-" + tDate.getFullYear();
            var date1 = new Date(eedate);
            var date2 = new Date(sdate);
            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

            x.days = diffDays;

            

            return diffDays;
            //alert(diffDays);

        }
        $scope.PhoneOrderType = [{
            "id": 0,
            "name": "Field Order"
        }, {
            "id": 1,
            "name": "Phone Order"
        }]
        $scope.fmcgData.PhoneOrderTypes = {};
        $scope.fmcgData.PhoneOrderTypes.selected = {};

        $scope.fmcgData.PhoneOrderTypes.selected.id = 0;

    if($scope.$parent.fmcgData.vansales!=undefined && $scope.$parent.fmcgData.vansales==1){
          $scope.fmcgData.PhoneOrderTypes.selected.id = 2;
    }

            if($scope.DesigSname!=undefined){
                if($scope.DesigSname.replace(/\(.*\)/, '')=="AI "){
                  $scope.fmcgData.PhoneOrderTypes.selected.id = 3;
                }
                
             }

        if ($scope.Myplns[0]['dcrtype'] == "Route Wise" && $scope.fmcgData.customer.selected.id == "1")
            $scope.routeWise = 0;
        else
            $scope.routeWise = 1;

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
                $scope.$parent.fmcgData.stockist = {};
                $scope.$parent.fmcgData.stockist.selected = {};
                $scope.$parent.fmcgData.stockist.selected.id = $scope.Myplns[0].stockistid;
                $scope.$parent.fmcgData.cluster = {};
                $scope.$parent.fmcgData.cluster.selected = {};

               $scope.$parent.fmcgData.superstockistid = {};
                $scope.$parent.fmcgData.superstockistid.selected = {};
                $scope.$parent.fmcgData.superstockistid.selected.id=$scope.Myplns[0].Sprstk;
                $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;
                $scope.$parent.fmcgData.cluster.name = $scope.Myplns[0].ClstrName
                if ($scope.Myplns[0]['dcrtype'] == "Route Wise" && $scope.fmcgData.customer.selected.id == "1") {
                    $scope.fmcgData.doctor = {};
                    $scope.fmcgData.doctor.selected = {};
                    $scope.fmcgData.doctor.selected.id = "o_" + $scope.Myplns[0].clusterid;
                    $scope.fmcgData.doctor.name = "Retailer Of " + $scope.Myplns[0].ClstrName;
                    $scope.fmcgData.routeWise = 0;
                }

            }

        } else {
            if ($scope.Myplns[0]['dcrtype'] == "Route Wise") {
                $scope.fmcgData.doctor = {};
                $scope.fmcgData.doctor.selected = {};
                if ($scope.$parent.fmcgData.cluster.name != undefined) {
                    $scope.fmcgData.doctor.selected.id = "o_" + $scope.$parent.fmcgData.cluster.selected.id;
                    $scope.fmcgData.doctor.name = "Retailer Of " + $scope.$parent.fmcgData.cluster.name;
                } else {
                    $scope.fmcgData.doctor.selected.id = "o_" + $scope.Myplns[0].clusterid;
                    $scope.fmcgData.doctor.name = "Retailer Of " + $scope.Myplns[0].ClstrName;
                }
                $scope.fmcgData.routeWise = 0;
            }
        }
        $scope.modal = $ionicModal;

        var tDate = new Date();
        if (!$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.isDraft) {
            if ($scope.cComputer) {
                $scope.$parent.fmcgData.entryDate = tDate;
                $scope.$parent.fmcgData.modifiedDate = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.$parent.fmcgData.entryDate = tDate;
                    $scope.$parent.fmcgData.modifiedDate = tDate;
                });
            }
        } else {
            if ($scope.cComputer) {
                $scope.$parent.fmcgData.modifiedDate = tDate;
            } else {
                window.sangps.getDateTime(function(tDate) {
                    $scope.$parent.fmcgData.modifiedDate = tDate;
                });
            }
            var tDate = new Date($scope.$parent.fmcgData.entryDate);
            $scope.$parent.fmcgData.entryDate = tDate;
        }

        $scope.$parent.fmcgData.eKey = 'EK' + $scope.sfCode + '-' + (new Date()).valueOf();
        if ($scope.$parent.fmcgData.customer) {
            if ($scope.$parent.fmcgData.customer.selected)
                if ($scope.$parent.fmcgData.customer.selected.id != "7") {
                    if ($filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name == "Secondary Order") {
                        //  alert("this is secondary");

                    }
                    $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
                } else {
                    $scope.$parent.navTitle = "Distributor Hunting"
                }
        } else {
            $scope.$parent.navTitle = "";
        }
        $scope.setAllow();
        $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen2";
        $scope.customers = [{
            'id': '1',
            'name': $scope.EDrCap
        }];
        var al = 1;
        if ($scope.ChmNeed != 1) {
            $scope.customers.push({
                'id': '2',
                'name': $scope.EChmCap
            });
            $scope.cCI = al;
            al++;
        }
        if ($scope.StkNeed != 0) {
            $scope.customers.push({
                'id': '3',
                'name': $scope.EStkCap
            });
            $scope.sCI = al;
            al++;
        }
        if ($scope.UNLNeed != 1) {
            $scope.customers.push({
                'id': '4',
                'name': $scope.ENLCap
            });
            $scope.nCI = al;
            al++;
        }
        if ($scope.view_STOCKIST == 1) {
            $scope.customers.push({
                'id': '5',
                'name': "Stock Updation",
                'url': 'manageStockistResult'
            });
            $scope.suCI = al;
            al++;
        }
        if ($scope.view_STOCKIST == 1) {
            $scope.customers.push({
                'id': '6',
                'name': "Stock View",
                'url': 'manageStockistResult'
            });
            $scope.svCI = al;
            al++;
        }
        if ($scope.Myplns[0].FWFlg == 'DH') {
            $scope.customers.push({
                'id': '7',
                'name': $scope.StkCap + " Hunting",
                'url': 'manageStockistResult'
            });
            $scope.hCI = al;
            al++;
        }
        $scope.precall = 1;
         var latandlang = {};
            var retaileraddress;
        $scope.$on('getPreCall', function(evnt, DrCd) {
            var id = $scope.$parent.fmcgData.subordinate.selected.id;
            var towns = $scope.myTpTwns;
            var data = $scope.doctors;
           
            $scope.FilteredData = data.filter(function(a) {
                return (a.town_code === id);
            });
            $scope.precall.TotalCalls = $scope.FilteredData.length;
            //                $scope.precall.productivity=
            var Pendinglistt = fmcgLocalStorage.getData("viewpendingbills") || [];
            Pendinglistt = Pendinglistt.filter(function(a) {
                return (a.custcode == DrCd.DrId);
            });
            if (Pendinglistt.length > 0) {
                $scope.fmcgData.Pendinglist = Pendinglistt;
                $scope.Pendinglistshow = true;
            } else {
                $scope.noo = true;
            }
            fmcgAPIservice.getPostData('POST', 'get/precall&Msl_No=' + DrCd.DrId, []).success(function(response) {
                var StockistDetails = response.StockistDetails;

                var data = fmcgLocalStorage.getData('doctor_master_' + (($scope.SF_type == 1) ? $scope.sfCode : $scope.fmcgData.subordinate.selected.id)) || [];

                var Mobilenumber = data.filter(function(a) {
                    return (a.id == DrCd.DrId)
                });
                $scope.MobileNumber = Mobilenumber[0].Mobile_Number;
              $.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyCyMzzgResJNYCYcShy46DlG26ZmQRvbGI&libraries=places');
          
        var geocoder = new google.maps.Geocoder();
         retaileraddress = response.Address;

        geocoder.geocode( { 'address': retaileraddress}, function(results, status) {

        if (status == google.maps.GeocoderStatus.OK) {
             
                latandlang.lat =Number(results[0].geometry.location.lat())
                latandlang.lng = Number(results[0].geometry.location.lng())
            } 
        }); 



                for (var key in StockistDetails) {
                    var stockiesCode = StockistDetails[key]['stockist_code'];
                    var stockists = $scope.stockists;
                    for (var key1 in stockists) {
                        if (stockists[key1]['id'] == stockiesCode)
                            StockistDetails[key]['stockist_code'] = stockists[key1]['name'];
                    }
                }
                $scope.vwPreCall = response;
                $scope.StockistDetails = StockistDetails;
                $scope.fmcgData.OpeningStock = response.OpeningStock;
                $scope.fmcgData.MOQ = response.MOQ;
                $scope.fmcgData.POt = response.POTENTIAL[0].Milk_Potential;
                $scope.fmcgData.MOV = response.MOV[0].MorderSum;
                $scope.precall = 0;
                // $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection!");
                // $ionicLoading.hide();
            });
        })

/*$scope.showMap = function () {
           var link = ""+"https://maps.google.com/?q?saddr="+13.030105+","+80.241425+" &daddr="+13.038063+","+80.159607;
             $location.path(link);
            //window.location = link;
        }*/

        $scope.Addnewretailer = function() {

            /*var link = ""+"http://maps.google.com/maps?saddr=kallakurichi thirumalaivasan";
                     // $location.path(link);
                     window.location = link;*/
            window.localStorage.setItem("AddNewRetailer", true);

            $state.go('fmcgmenu.addULDoctor');

        }
        $scope.IsChked = function($event, accept) {

            if ($event !== undefined) {

                var checkbox = $event.target;
                console.log($scope.IsAccepted);
                if (checkbox.checked) {
                    $scope.democheckIsVisible = true;
                } else {
                    $scope.democheckIsVisible = false;
                }

            }

        };
        $scope.submit = function() {
            var msg = "";
            var proceed = true;

            if($scope.GeoChk==0 && navigator.platform != "Win32"){
                 window.sangps.CheckGPS({ "gps": schkGPS });


                    startGPS();                                
              }

            if ($scope.fmcgData.customer.selected.id == "7") {}
            if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {} else {
                if ($scope.fmcgData.worktype.selected.FWFlg != "DH") {
                    proceed = false;
                    msg = "Please Select " + $filter('getValueforID')($scope.fmcgData.customer.selected.id, $scope.customers).name;
                }
            }
            if ($scope.fmcgData.worktype.selected.FWFlg == "DH") {
                if ($scope.view_MR != 1 && ($scope.fmcgData.subordinate == undefined || $scope.fmcgData.subordinate.selected.id == '')) {
                    Toast('Select the field force...');
                    return false;
                }
                if ($scope.fmcgData.dh_address == undefined || $scope.fmcgData.dh_Shop_Name == '') {
                    Toast('Select the Name of the Distributor...');
                    return false;
                }
                if ($scope.fmcgData.dh_address == undefined || $scope.fmcgData.dh_address == '') {
                    Toast('Select the Address of the Distributor...');
                    return false;
                }
                if ($scope.fmcgData.dh_area == undefined || $scope.fmcgData.dh_area == '') {
                    Toast('Select the Area of the Distributor...');
                    return false;
                }
                if ($scope.fmcgData.dh_Phone_Number == undefined || $scope.fmcgData.dh_Phone_Number == '') {
                    Toast('Select the Phone Number...');
                    return false;
                }


            }


            if ($scope.democheckIsVisible == true) {

                if ($scope.fmcgData.demogiven == undefined || $scope.fmcgData.demogiven == '') {
                    Toast('Enter the Demo Person Name');
                    return false;
                }


            }

         if($scope.EventCapNd==1 && ($scope.fmcgData.photosList==undefined ||$scope.fmcgData.photosList.length==0)){

                    Toast("Take a photo");
                    return false;
                 }

            if ($scope.fmcgData.worktype.selected.FWFlg == "F") {
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
                if (($scope.fmcgData.doctor == undefined || $scope.fmcgData.doctor.selected == undefined) && $scope.fmcgData.customer.selected.id == "1") {
                    Toast('Select  ' + $scope.DrCap + '....');
                    return false;
                }
            }
            var pobT = $scope.fmcgData.pob ? parseInt($scope.fmcgData.pob) : 0;
            if (isNaN(pobT) || pobT > 999999) {
                $scope.fmcgData.pob = "";
                proceed = false;
                msg = msg + "Please Check the input " + $scope.POBCap;
            }
            if (proceed) {
                 $scope.fmcgData.submitdis=true;


                $scope.$emit('finalSubmit');
            } else {
                Toast(msg, true);
            }

        }
        $scope.save = function() {
            if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {
                $scope.saveToDraftO();
            } else {
                Toast("No data to save in draft", true);
            }
        };
        /*if ($scope.fmcgData.doctor == undefined)
            $scope.$parent.fmcgData.jontWorkSelectedList = $scope.Myplns[0].jontWorkSelectedList || [];

        else
            $scope.$parent.fmcgData.jontWorkSelectedList = $scope.$parent.fmcgData['jontWorkSelectedList'] || [];
        $scope.addProduct = function(selected) {
            var jontWorkData = {};
            jontWorkData.jointwork = selected;
            $scope.$parent.fmcgData.jontWorkSelectedList.push(jontWorkData);
        };
        $scope.gridOptions = {
            data: 'fmcgData.jontWorkSelectedList',
            rowHeight: 50,
            enableRowSelection: false,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: [{
                field: 'jointworkname',
                displayName: 'Jointwork',
                enableCellEdit: false,
                cellTemplate: 'partials/jointworkCellTemplate.html'
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50
            }]
        };*/
        $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
        };
        $scope.goNext = function() {
            if($scope.GeoChk==0 && navigator.platform != "Win32"){
                 window.sangps.CheckGPS({ "gps": schkGPS });


                    startGPS();                                
              }
            var temp;
            var msg = "";
            var proceed = true;
            $scope.fmcgData.Incentive = 0;

            if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {} else {
                proceed = false;
                msg = "Please Select " + $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
            }

            if ($scope.fmcgData.worktype.selected.FWFlg == "F" && ($scope.fmcgData.doctor == undefined || $scope.fmcgData.doctor.selected == undefined) && $scope.$parent.fmcgData.customer.selected.id == "1") {
                //$scope.fmcgData.doctor = {};
                //if ( $scope.fmcgData.doctor.selected == undefined) {
                Toast('Select  ' + $scope.DrCap + '....');
                return false;
                // }
            }
            if ($scope.fmcgData.worktype.selected.FWFlg == "F" && ($scope.fmcgData.stockist == undefined || $scope.fmcgData.stockist.selected == undefined || $scope.fmcgData.stockist.selected.id == undefined || $scope.fmcgData.stockist.selected.id == "") && $scope.$parent.fmcgData.customer.selected.id == "3") {
                //$scope.fmcgData.doctor = {};
                //if ( $scope.fmcgData.doctor.selected == undefined) {
                Toast('Select  ' + $scope.StkCap + '....');
                return false;
                // }
            }

            if ($scope.democheckIsVisible == true) {

                if ($scope.fmcgData.demogiven == undefined || $scope.fmcgData.demogiven == '') {
                    Toast('Enter the Demo Person Name');
                    return false;
                }


            }
            $state.go('fmcgmenu.screen3');

        };

        $scope.goNextIncentive = function() {
            var temp;
            var msg = "";
            var proceed = true;

            $scope.fmcgData.Incentive = 1;
            if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor) {} else {
                proceed = false;
                msg = "Please Select " + $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
            }

            if ($scope.fmcgData.worktype.selected.FWFlg == "F" && ($scope.fmcgData.doctor == undefined || $scope.fmcgData.doctor.selected == undefined) && $scope.$parent.fmcgData.customer.selected.id == "1") {
                //$scope.fmcgData.doctor = {};
                //if ( $scope.fmcgData.doctor.selected == undefined) {
                Toast('Select  ' + $scope.DrCap + '....');
                return false;
                // }
            }
            if ($scope.fmcgData.worktype.selected.FWFlg == "F" && ($scope.fmcgData.stockist == undefined || $scope.fmcgData.stockist.selected == undefined || $scope.fmcgData.stockist.selected.id == undefined || $scope.fmcgData.stockist.selected.id == "") && $scope.$parent.fmcgData.customer.selected.id == "3") {
                //$scope.fmcgData.doctor = {};
                //if ( $scope.fmcgData.doctor.selected == undefined) {
                Toast('Select  ' + $scope.StkCap + '....');
                return false;
                // }
            }

            if ($scope.democheckIsVisible == true) {

                if ($scope.fmcgData.demogiven == undefined || $scope.fmcgData.demogiven == '') {
                    Toast('Enter the Demo Person Name');
                    return false;
                }


            }
            $state.go('fmcgmenu.screen3');

        };


        $scope.goBack = function() {
            //$state.go('fmcgmenu.addNew');
           // 04.02.2019
            $state.go('fmcgmenu.home');
        };


        $scope.ShowMapRoute=function()  {

        if(retaileraddress==null || retaileraddress=="" || retaileraddress==undefined){
            Toast("Address Not Found");

            return false;
        }
        $scope.FindRoutemodalMap.GoogleRoute = function() {
        window.sangps.openmap(function(tDate) {
                        alert("hi");
                    });
        sangps.openmap({ "Address": retaileraddress });
        }

            if (typeof google === 'undefined' || typeof google === undefined) {
                $.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyCyMzzgResJNYCYcShy46DlG26ZmQRvbGI&libraries=places');
            }



         
             var directionsDisplay;
             var directionsService = new google.maps.DirectionsService();
             var startlatandlang={};
                 startlatandlang.lat=$scope.fmcgData.location.split(':')[0];
                 startlatandlang.lng=$scope.fmcgData.location.split(':')[1];

            //AIzaSyBfMnEha5uy5UZgvjhk0y6IwFXkvbB_mak
            //    $.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyCOY8BqGMFqo7Ij2n1O8b4RcL443zjoI_o&libraries=places');
         

            $scope.FindRoutemodalMap.show();
            //$scope.GeoTagDr = item;
            $scope.FindRoutemodalMap.vTyp = 1;
        $scope.FindRoutemodalMap.rfinder = true;
        var map = new google.maps.Map(document.getElementById('map_canvas'), {
                center: { lat: -34.397, lng: 150.644},
                zoom: 14,
                panControl: true,
                zoomControl: true,
                mapTypeControl: false,
                scaleControl: true,
                streetViewControl: true,
                overviewMapControl: true,
                rotateControl: true,
                
                mapTypeId: google.maps.MapTypeId.ROADMAP

            });

            mapWin = $('.modal-backdrop .active');

            $("#map_canvas").closest('.scroll').addClass("fitHeight");
            $("#map_canvas").closest('.scroll').removeClass("scroll");
            $(".fitHeight").closest('.scroll-content').removeClass("ionic-scroll");
            $(".fitHeight").closest('.scroll-content').removeClass("scroll-content");
            mapWin.css("left", "0px");
            mapWin.css("top", "0px");
            mapWin.css("width", "100%");
            mapWin.css("height", "100%");
           directionsDisplay = new google.maps.DirectionsRenderer();

            

            function toggleBounce() {
                if (marker.getAnimation() !== null) {
                    marker.setAnimation(null);
                } else {
                    marker.setAnimation(google.maps.Animation.BOUNCE);
                }
            }

            var map;
            var marker;

              var start = new google.maps.LatLng($scope.fmcgData.location.split(':')[0], $scope.fmcgData.location.split(':')[1]);
            //var end = new google.maps.LatLng(38.334818, -181.884886);
            var end = new google.maps.LatLng(latandlang.lat,latandlang.lng);
            var request = {
              origin: start,
              destination: end,
              travelMode: google.maps.TravelMode.DRIVING
            };
            directionsService.route(request, function(response, status) {
              if (status == google.maps.DirectionsStatus.OK) {
                $scope.FindRoutemodalMap.km=response.routes[0].legs[0].distance.text;
                        $scope.FindRoutemodalMap.duration=response.routes[0].legs[0].duration.text;

                directionsDisplay.setDirections(response);
                directionsDisplay.setMap(map);
              } else {
               // alert("Directions Request from " + start.toUrlValue(6) + " to " + end.toUrlValue(6) + " failed: " + status);
               Toast("Address is Not Valid");
              }
            });
          

           // flightPath.setMap(map);
            marker.addListener('click', toggleBounce, function (map, marker) {
                    
                });
        }




    }).controller('screen4sCtrl', function($scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {

    }).controller('screen5Ctrl', function($rootScope, $scope, $state, $ionicPopup, $filter, geolocation, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        if ($scope.$parent.fmcgData.customer) {
            $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
        } else {
            $scope.$parent.navTitle = "";
        }
        $scope.setAllow();
        $scope.updateValue = function(value) {
            $scope.$parent.fmcgData.remarks = value.name;
        };
        $scope.saveD = function() {

            $scope.fmcgData.toBeSync = false;
            $scope.saveToDraftO();
        };
        $scope.save = function() {
            $scope.$emit('finalSubmit');

        };
        $scope.goBack = function() {
            if ($scope.fmcgData.worktype.selected.FWFlg.toString() !== "F") {
                $state.go('fmcgmenu.addNew')
            } else if (($scope.fmcgData.customer.selected.id == 1 && $scope.DINeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CINeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SINeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NINeed == 0))
                $state.go('fmcgmenu.screen4')
            else if (($scope.fmcgData.customer.selected.id == 1 && $scope.DPNeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CPNeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SPNeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NPNeed == 0))
                $state.go('fmcgmenu.screen3');
            else
                $state.go('fmcgmenu.screen2');
        };
    })
    .controller('RCPACtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage) {
        $scope.RCPA.doctor = {}
        $scope.fmcgData.customer = {};
        $scope.RCPA.flag = 1;
        $scope.CompDet = 0;
        $scope.fmcgData.value1 = 0;
        $scope.fmcgData.value2 = 0;
        $scope.$parent.navTitle = "Market Audit Entry";

        if ($scope.cComputer) {
            $scope.RCPA.eDate = new Date();
        } else {
            window.sangps.getDateTime(function(tDate) {
                $scope.RCPA.eDate = tDate;
            });
        }
        $scope.gridOptions = {
            data: 'RCPA.productSelectedList',
            rowHeight: 50,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            enableRowSelection: false,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: [{
                field: 'product_Nm',
                displayName: 'Product',
                enableCellEdit: false,
                cellTemplate: 'partials/productCellTemplate.html'
            }, {
                field: 'qty',
                displayName: 'Qty',
                enableCellEdit: true,
                editableCellTemplate: "partials/cellEditTemplate.html",
                width: 60
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50
            }]
        };

        $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.RCPA.productSelectedList.splice(index, 1);
        };
        $scope.clear = function() {
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
        if ($scope.prod == 1) {
            $scope.RCPA.productSelectedList = [];
            CurrentData = $scope.products;
            for (i = 0; i < CurrentData.length; i++) {
                var productData = {};
                productData.product = CurrentData[i].id;
                productData.product_Nm = CurrentData[i].name;

                productData.qty = CurrentData[i].rx_qty || 0;
                $scope.RCPA.productSelectedList.push(productData);
            }
        }
        $scope.save = function() {
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
            } else {
                $scope.RCPA.chemist = {};
                $scope.RCPA.chemist.selected = {};
                $scope.RCPA.chemist.selected.id == ""
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
                if (tmPrd[i].qty != 0) {
                    RCPAProdQty += tmPrd[i].product + " ( " + tmPrd[i].qty + " ), ";
                    RCPAProdNmQty += tmPrd[i].product_Nm + " ( " + tmPrd[i].qty + " ), ";
                }
            }
            $scope.RCPA.OurBrndCds = RCPAProdQty;
            $scope.RCPA.OurBrndNms = RCPAProdNmQty;

            $scope.data.RCPADt = $scope.RCPA.eDate;
            if ($scope.CompDet == 1) {
                if ($scope.RCPA.CpmptName == undefined || $scope.RCPA.CpmptName == "") {
                    Toast('Enter the Competitor Name');
                    return false;
                }
                if ($scope.RCPA.CpmptBrnd == undefined || $scope.RCPA.CpmptBrnd == "") {
                    Toast('Enter the Competitor Brand');
                    return false;
                }
            } else {
                $scope.RCPA.CpmptName = '';
                $scope.RCPA.CpmptBrnd = '';
            }
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
            $scope.data.photosList = $scope.RCPA.photosList;
            $scope.data.Remark = $scope.RCPA.Precrip;
            $scope.clear();

            fmcgAPIservice.addMAData('POST', 'dcr/save', "4", $scope.data).success(function(response) {
                if (response.success)
                    Toast("Market Audit Entry Submited Successfully");
            }).error(function() {
                $scope.OutRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
                $scope.OutRCPA.push($scope.data);
                localStorage.removeItem("OutBx_RCPA");
                fmcgLocalStorage.createData("OutBx_RCPA", $scope.OutRCPA);
                Toast("No Internet Connection! Market Audit Entry Saved in Outbox");
            });
            $state.go('fmcgmenu.home');
        };
    })
    .controller('LostProducts', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "Lost Products";

        $scope.Mypln.subordinate = {};
        $scope.Mypln.subordinate.selected = {};
        if ($scope.view_MR == 1) {
            $scope.Mypln.subordinate.selected.id = $scope.sfCode;
        }
        $scope.Mypln.cluster = {};
        $scope.Mypln.cluster.selected = {};

        $scope.$on('getLostProducts', function(evnt, DrCd) {
            custCode = DrCd.DrId;
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'spLostProducts&custCode=' + custCode + "&stockistCode=" + $scope.Mypln.stockist.selected.id, [])
                .success(function(response) {
                    ss = [];

                    for (di = 0; di < $scope.products.length; di++) {
                        response.filter(function(a) {
                            if (a.Product_Detail_Name == $scope.products[di].name) {
                                ss.push(a);
                            }
                        });

                    }
                    $scope.LostProducts = ss;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
        });
    })

.controller('reportsCtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "Reports";
        $scope.goTo = function(link) {
           
            if (link == 'attView')
                $state.go('fmcgmenu.attView');
            if (link == 'dcr1')
                $state.go('fmcgmenu.dcr1');
            if (link == 'dySalSum')
                $state.go('fmcgmenu.dySalSumm');
            if (link == 'dcr')
                $state.go('fmcgmenu.dcr');

             if (link == 'DayCallReport')
                $state.go('fmcgmenu.DayCallReport');

            if (link == 'dayPlan')
                $state.go('fmcgmenu.dayPlan');
            if (link == 'todayscheme'){
                $scope.fmcgData.OFP=0;
                $state.go('fmcgmenu.OfferProduct');
            }
            if (link == 'precall')
                $state.go('fmcgmenu.precall');
            if (link == 'BrndSummary')
                $state.go('fmcgmenu.BrndSummary');
            if (link == 'distDayReport')
                $state.go('fmcgmenu.distDayReport');
            if (link == 'myAudio')
                $state.go('fmcgmenu.myAudio');
            if (link == 'LostProducts')
                $state.go('fmcgmenu.LostProducts');
            if (link == 'transCurrentStocks')
                $state.go('fmcgmenu.transCurrentStocks');

            if (link == 'transSSCurrentStocks')
                $state.go('fmcgmenu.transSSCurrentStocks');
            
            if (link == 'tpview')
                $state.go('fmcgmenu.tpview');
            if (link == 'tpviewdt')
                $state.go('fmcgmenu.tpviewdt');
            if (link == 'collectionreport')
                $state.go('fmcgmenu.CollectionReport');
           
            if (link == 'targetandachieve')
                $state.go('fmcgmenu.TargetAndAchieve');
              if (link == 'dailytotalcallsrport')
                $state.go('fmcgmenu.dailytotalcallsrport');

        }
    })
    .controller('reportingentryCtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "Admin Form";
        $scope.goTo = function(link) {
            if (link == 'LeaveForm')
                $state.go('fmcgmenu.LeaveForm');
            if (link == 'RCPA')
                $state.go('fmcgmenu.RCPA');
            if (link == 'ExpenseEntry') {
                $state.go('fmcgmenu.DailyExpense');
                //$state.go('fmcgmenu.ExpenseEntry');
            }
           if (link == 'EA') {
                $state.go('fmcgmenu.EA');
                //$state.go('fmcgmenu.ExpenseEntry');
            }

             if (link == 'misseddate')
                $state.go('fmcgmenu.MissedDates');
            if (link == 'TourPlan')
                $state.go('fmcgmenu.TourPlan');
            if (link == 'dailyinv') {
                $scope.DayInv.EMode = 0; //($scope.SF_type == 2) ? 0 : 3; console.log("Inventor"+$scope.SF_type+"/"+$scope.DayInv.EMode);
                $state.go('fmcgmenu.DailyInv');
            }
            if (link == 'orderret') {
                $state.go('fmcgmenu.OrderRet');
            }
        }
    })
    .controller('ApprovalsCtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "Approvals";
        $scope.goToApproval = function(cus) {
            if (cus == 1)
                $state.go('fmcgmenu.ViewLeave');
            if (cus == 2)
                $state.go('fmcgmenu.viewTPApproval');
            if (cus == 3)
                $state.go('fmcgmenu.ViewExpense');
            if (cus == 4) {
                $scope.DayInv.EMode = 1;
                $state.go('fmcgmenu.DailyInv');
            }
            if (cus == 5) {

                $state.go('fmcgmenu.ProductDisplayApprovals');
            }


            //$state.go('fmcgmenu.ViewDCR');
        }
    })
    .controller('viewTPApproval', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "View TourPlan";
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'vwChkTransApproval', [])
            .success(function(response) {
                $scope.TP = response;
                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        $scope.approval = function(code, month, year) {
            $scope.$parent.TPCode = code;
            $scope.$parent.TPMonth = month;
            $scope.$parent.TPYear = year;
            $state.go('fmcgmenu.TPApproval');
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.Approvals');
        }
    })
    .controller('ExpenseApproval', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.data = {};
        $ionicLoading.show({
            template: 'Loading...'
        });

        $scope.rejection = 0;
        $scope.$parent.navTitle = "Expense Approval";
        fmcgAPIservice.getDataList('POST', 'vwChkExpenseApprovalOne&code=' + $scope.$parent.ExpenseCode + "&month=" + $scope.$parent.ExpenseMonth + "&year=" + $scope.$parent.ExpenseYear, [])
            .success(function(response) {
                $scope.head = response['head'];
                $scope.summary = response['summary'];

                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        $scope.approve = function() {
            fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.ExpenseCode + "&month=" + $scope.$parent.ExpenseMonth + "&year=" + $scope.$parent.ExpenseYear, "21", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("Expense Approval Submitted Successfully");
                    $state.go('fmcgmenu.ViewExpense');
                }
            });
        }
        $scope.reject = function() {
            $scope.rejection = 1;
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.ViewExpense');
        }
        $scope.rejSend = function() {
            if ($scope.data.reason == undefined || $scope.data.reason == '') {
                Toast('Enter the Reason...');
                return false;
            }
            fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.ExpenseCode + "&month=" + $scope.$parent.ExpenseMonth + "&year=" + $scope.$parent.ExpenseYear, "22", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("Expense Reject Successfully");
                    $state.go('fmcgmenu.ViewExpense');
                }
            });
        }

    })

.controller('TPApproval', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.data = {};
        $ionicLoading.show({
            template: 'Loading...'
        });

        $scope.rejection = 0;
        $scope.$parent.navTitle = "TP Approval";
        fmcgAPIservice.getDataList('POST', 'vwChkTransApprovalOne&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, [])
            .success(function(response) {
                $scope.TPSf = response;
                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        $scope.approve = function() {
            fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, "15", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("TP Approval Submitted Successfully");
                    $state.go('fmcgmenu.viewTPApproval');
                }
            });
        }
        $scope.reject = function() {
            $scope.rejection = 1;
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.viewTPApproval');
        }
        $scope.rejSend = function() {
            if ($scope.data.reason == undefined || $scope.data.reason == '') {
                Toast('Enter the Reason...');
                return false;
            }
            fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, "16", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("TP Reject Successfully");
                    $state.go('fmcgmenu.viewTPApproval');
                }
            });
        }

    })
    .controller('ViewLeave', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "View Leave";
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'vwLeave', [])
            .success(function(response) {
                $scope.leaves = response;
                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        $scope.approval = function(id) {
            leaves = $scope.leaves;
            for (key in leaves) {
                if (leaves[key]['Leave_Id'] == id)
                    $scope.$parent.LA = leaves[key];
            }

            $state.go('fmcgmenu.LeaveApproval');
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.Approvals');
        }
    })



.controller('ProductDisplayApprovalsctrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
    $scope.$parent.navTitle = "Product Display Approvals";
    $ionicLoading.show({
        template: 'Loading...'
    });
    fmcgAPIservice.getDataList('POST', 'Productdisplayapprovals', [])
        .success(function(response) {
            $scope.leaves = response;
            $ionicLoading.hide();
        }).error(function() {
            $ionicLoading.hide();
            Toast('No Internet Connection.');
        });
    $scope.approval = function(id) {
        leaves = $scope.leaves;
        for (key in leaves) {
            if (leaves[key]['sfCode'] == id)
                $scope.$parent.LA = leaves[key];
        }

        $state.go('fmcgmenu.ProductdisplayLeaveapprovals');
    }
    $scope.goBack = function() {
        $state.go('fmcgmenu.Approvals');
    }
})

.controller('ViewDCR', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "View DCR";
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'vwDcr', [])
            .success(function(response) {
                $scope.DCR = response;
                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        $scope.approval = function(sfCode, ActivityDate, FieldWork_Indicator, worktype, sfName, Trans_SlNo, PlanName) {
            $scope.$parent.code = sfCode;
            $scope.$parent.activity_Date = ActivityDate;
            $scope.$parent.FieldWork_Indicator = FieldWork_Indicator;
            $scope.$parent.worktype = worktype;
            $scope.$parent.sfName = sfName;
            $scope.$parent.Trans_SlNo = Trans_SlNo;
            $scope.$parent.PlanName = PlanName;
            $state.go('fmcgmenu.DCRApproval');
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.Approvals');
        }
    })
    .controller('DCRApproval', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.data = {};
        $scope.rejection = 0;
        $scope.$parent.navTitle = "DCR Approval";

        if ($scope.$parent.FieldWork_Indicator == 'F') {
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'vwDcrOne&Trans_SlNo=' + $scope.$parent.Trans_SlNo, [])
                .success(function(response) {
                    $scope.DCRSf = response;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
        }
        $scope.approve = function() {
            fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.code + "&date=" + $scope.$parent.activity_Date, "17", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("DCR Approval Submitted Successfully");
                    $state.go('fmcgmenu.ViewDCR');
                }
            });
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.ViewDCR');
        }
        $scope.reject = function() {
            $scope.rejection = 1;
        }
        $scope.rejSend = function() {
            if ($scope.data.reason == undefined || $scope.data.reason == '') {
                Toast('Enter the Reason...');
                return false;
            }
            fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.code + "&date=" + $scope.$parent.activity_Date, "18", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("DCR Reject Successfully");
                    $state.go('fmcgmenu.ViewDCR');
                }
            });
        }

    })
    .controller('LeaveApproval', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.data = {};

        $scope.rejection = 0;
        $scope.$parent.navTitle = "Leave Approval";
        $scope.approve = function() {
            $scope.data.LA = $scope.$parent.LA;
            fmcgAPIservice.addMAData('POST', 'dcr/save&leaveid=' + $scope.$parent.LA.Leave_Id, "13", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("Leave Approval Submitted Successfully");
                    $state.go('fmcgmenu.ViewLeave');
                }
            });
        }
        $scope.reject = function() {
            $scope.rejection = 1;
        }
        $scope.rejSend = function() {
            $scope.data.LA = $scope.$parent.LA;
            if ($scope.data.reason == undefined || $scope.data.reason == '') {
                Toast('Enter the Reason...');
                return false;
            }
            fmcgAPIservice.addMAData('POST', 'dcr/save&leaveid=' + $scope.$parent.LA.Leave_Id, "14", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("Leave Reject Successfully");
                    $state.go('fmcgmenu.ViewLeave');
                }
            });
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.ViewLeave');
        }
    })



.controller('ProductdisplayLeaveapprovalsctrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
    $scope.data = {};

    $scope.rejection = 0;
    $scope.$parent.navTitle = "Product Display Approvals";
    $scope.approve = function() {
        $scope.data.LA = $scope.$parent.LA;
        fmcgAPIservice.addMAData('POST', 'dcr/save', "35", $scope.data).success(function(response) {
            if (response.success) {
                Toast("Display Approval Submitted Successfully");
                $state.go('fmcgmenu.home');
            }
        });
    }
    $scope.reject = function() {
        $scope.rejection = 1;



    }
    $scope.rejSend = function() {
        $scope.data.LA = $scope.$parent.LA;
        if ($scope.data.reason == undefined || $scope.data.reason == '') {
            Toast('Enter the Reason...');
            return false;
        }
        fmcgAPIservice.addMAData('POST', 'dcr/save', "36", $scope.data).success(function(response) {
            if (response.success) {
                Toast("Display Reject Successfully");
                $state.go('fmcgmenu.home');
            }
        });
    }
    $scope.goBack = function() {
        $state.go('fmcgmenu.ProductDisplayApprovals');
    }
})



.controller('ViewExpense', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "View Expense";
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'vwExpenseApproval', [])
            .success(function(response) {
                $scope.Expense = response;
                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        $scope.approval = function(code, month, year) {
            $scope.$parent.ExpenseCode = code;
            $scope.$parent.ExpenseMonth = month;
            $scope.$parent.ExpenseYear = year;
            $state.go('fmcgmenu.ExpenseApproval');
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.Approvals');
        }
    })
    .controller('sendMailCtrl', function($rootScope, $scope, $ionicScrollDelegate, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal) {
        function failNative(e) {
            console.error('Houston, we have a big problem :(');
        }
        $scope.Attachment = function() {
            fileChooser.open(function(uri) {
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
                    function(response) {
                        $scope.finalPath = '';
                    },
                    function() {}, options);
            }
            // console.log($scope.myFile);
        data = {};


        $scope.goBack = function() {
            $state.go('fmcgmenu.mail');
        }
        $scope.sendMail = function() {
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
            fmcgAPIservice.addMAData('POST', 'dcr/save', "20", fd).success(function(response) {
                if (response.success) {
                    $state.go('fmcgmenu.mail');
                    Toast("Mail Successfully Submitted");
                    $ionicLoading.hide();

                }
            });
        }
    })

.directive('contenteditable', function() {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function(scope, element, attrs, ngModel) {
            if (!ngModel) return;
            ngModel.$render = function() {
                element.html(ngModel.$viewValue || '');
            };
            element.on('blur keyup change', function() {
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

.controller('mailCtrl', function($rootScope, $scope, $ionicScrollDelegate, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal) {
        $scope.$parent.navTitle = "Mail";
        $scope.folder = {};
        var date1 = new Date();
        //    date1 = new Date("2016-12-16")
        $scope.month = date1.getMonth() + 1;
        $scope.year = date1.getFullYear();
        years = [];
        for (i = 2014; i <= $scope.year; i++) {
            years.push({
                "val": i
            });
        }
        $scope.years = years;

        $scope.folder.selected = {};
        $scope.folder.selected.id = "0";

        $scope.compose = function() {
            $scope.fmcgData.staffSelectedList = [];
            $scope.fmcgData.staffNms = '';
            $scope.fmcgData.staffIds = "";
            $scope.fmcgData.staffSelectedListcc = [];
            $scope.fmcgData.ccstaffNms = '';
            $scope.fmcgData.ccstaffIds = "";
            $scope.fmcgData.staffSelectedListbcc = [];
            $scope.fmcgData.bccstaffNms = '';
            $scope.fmcgData.bccstaffIds = "";

            $scope.fmcgData.msg = '';
            $scope.fmcgData.Attach = '';
            $scope.fmcgData.subject = '';
            $scope.fmcgData.AttachName = '';
            $state.go('fmcgmenu.sendMail');
        }
        $scope.mailType = 'viewFolders';
        if ($scope.folder.selected.id != undefined)
            viewDetails($scope.folder.selected.id, $scope.month, $scope.year);
        $scope.viewMail = function(Staffid_Id, Mail_vc_ViewFlag, Mail_int_Det_No, date, Attach, from, to, cc, sub, msg) {
            if (Mail_vc_ViewFlag == "0" && $scope.folder.selected.id != "Sent") {
                fmcgAPIservice.addMAData('POST', 'mailView&id=' + $scope.fmcgData.Mail_int_Det_No).success(function(response) {
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
            } else {
                $scope.fmcgData.from = from;
                $scope.fmcgData.to = to;
            }
            $scope.fmcgData.cc = cc;
            $scope.fmcgData.sub = sub;
        }
        $scope.view = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });

            var fileTransfer = new FileTransfer();
            var uri = encodeURI($scope.fmcgData.Attach);
            var fileURL = cordova.file.externalCacheDirectory;
            fileTransfer.download(
                uri,
                fileURL + $scope.fmcgData.AttachName,
                function(entry) {
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
                        mimetype, {
                            error: function() {
                                //alert('Error status: ' + e.status + ' - Error message: ' + e.message);
                            },
                            success: function() {
                                // alert("Success");
                            }
                        }
                    );
                },
                function(error) {},
                false, {
                    headers: {
                        "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                    }
                }
            );
        }
        $scope.downloadFile = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var fileTransfer = new FileTransfer();
            var uri = encodeURI($scope.fmcgData.Attach);
            var fileURL = cordova.file.externalRootDirectory + "/Download/";
            fileTransfer.download(
                uri,
                fileURL + $scope.fmcgData.AttachName,

                function(entry) {
                    $ionicLoading.hide();
                    alert("Download Completed");
                    //  alert("download complete: " + entry.toURL());
                },
                function(error) {
                    $ionicLoading.hide();
                    // alert("download error source " + error.source);
                    // alert("download error target " + error.target);
                    // alert("download error code" + error.code);
                },
                false, {
                    headers: {
                        "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                    }
                }
            );
        }
        $scope.$on('moveFolders', function(evnt, det) {
            $scope.subfolder.selected.id = det.Details['id'];
            fmcgAPIservice.addMAData('POST', 'mailMove&id=' + $scope.fmcgData.Mail_int_Det_No + "&folder=" + $scope.subfolder.selected.id).success(function(response) {
                if (response.success) {
                    $scope.mailType = "viewFolders";
                    Toast("Mail Moved");
                }
            });
        });
        $scope.showSelectMonth = function(val) {
            $scope.month = val;
            viewDetails($scope.folder.selected.id, $scope.month, $scope.year);

        }
        $scope.showSelectYear = function(val) {
            $scope.year = val;
            viewDetails($scope.folder.selected.id, $scope.month, $scope.year);

        }
        $scope.$on('getFolders', function(evnt, det) {
            $scope.folder.selected.id = det.Details['id'];

            viewDetails(det.Details['id'], $scope.month, $scope.year);
        });

        $scope.reply = function() {
            $scope.fmcgData.staffIds = $scope.fmcgData.Staffid_Id;
            $scope.fmcgData.staffNms = $scope.fmcgData.from;
            $scope.$parent.mailsStaff.push({
                "id": $scope.fmcgData.Staffid_Id,
                "name": $scope.fmcgData.from
            });
            $scope.fmcgData.staffSelectedList = [];
            $scope.$parent.fmcgData.staffSelectedList.push({
                "id": $scope.fmcgData.Staffid_Id,
                "name": $scope.fmcgData.from,
                "checked": true
            });
            $scope.fmcgData.subject = 're:' + $scope.fmcgData.sub;
            $scope.fmcgData.message = '';
            $state.go('fmcgmenu.sendMail');
        }
        $scope.delete = function() {
            fmcgAPIservice.addMAData('POST', 'mailDel&id=' + $scope.fmcgData.Mail_int_Det_No + "&folder=" + $scope.folder.selected.id).success(function(response) {
                if (response.success) {
                    $scope.mailType = "viewFolders";

                }
            });
        }
        $scope.forward = function() {
            str = "<div>----- Forwarded Message ----</div><div>From : " + $scope.fmcgData.from + "</div><div>To :" + $scope.fmcgData.to + "</div><div>Sent : " + $scope.fmcgData.date + "</div><div><br></div><div><br></div>";
            $scope.fmcgData.message = str + $scope.fmcgData.msg;
            $scope.fmcgData.subject = "Fwd:" + $scope.fmcgData.sub;
            $state.go('fmcgmenu.sendMail');
        }
        $scope.goBack = function() {
            $scope.mailType = 'viewFolders';
        }

        function viewDetails(folder, month, year) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'getMailsApp&folder=' + folder + "&month=" + month + "&year=" + year, [])
                .success(function(response) {
                    $scope.Mails = response;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                })
        }


    })
    .controller('myAudioCtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $filter, $ionicLoading) {
        $scope.$parent.navTitle = "Product Detailing";
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'getProductDetailing', [])
            .success(function(response) {
                $scope.products = response;
                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            })
        $scope.view = function(product) {
            $scope.$parent.productDetail = product;
            $state.go('fmcgmenu.viewAudioDet');
        }
    })
    .controller('viewAudioDetCtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $filter, $ionicLoading) {
        $scope.$parent.navTitle = "View Product Detailing";
        $scope.play = function(fileName, filetype) {
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
                    mimetype, {
                        error: function(e) {
                            $ionicLoading.show({
                                template: 'Loading...'
                            });

                            var fileTransfer = new FileTransfer();
                            var uri = encodeURI(serverpath);
                            var fileURL = cordova.file.externalRootDirectory + "/Download/";
                            fileTransfer.download(
                                uri,
                                filepath,

                                function(entry) {
                                    $ionicLoading.hide();
                                    alert("Download Completed");
                                    //  alert("download complete: " + entry.toURL());
                                    cordova.plugins.fileOpener2.open(
                                        filepath,
                                        mimetype, {
                                            error: function() {
                                                $ionicLoading.hide();
                                                alert('Error status: ' + e.status + ' - Error message: ' + e.message);

                                            },
                                            success: function() {
                                                $ionicLoading.hide();
                                                // alert("Success");
                                            }
                                        }
                                    );
                                },
                                function(error) {
                                    $ionicLoading.hide();
                                    // alert("download error source " + error.source);
                                    // alert("download error target " + error.target);
                                    // alert("download error code" + error.code);
                                },
                                false, {
                                    headers: {
                                        "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                                    }
                                }
                            );
                        },
                        success: function() {
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
                    successCallback: function() {
                        alert("Player closed without error.");
                    },
                    errorCallback: function(errMsg) {
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
                            function(entry) {
                                $ionicLoading.hide();
                                alert("Download Completed");
                                if (filetype == "V")
                                    window.plugins.streamingMedia.playVideo(filepath, options);
                                else
                                    window.plugins.streamingMedia.playAudio(filepath, options);

                            },
                            function(error) {
                                $ionicLoading.hide();
                            },
                            false, {
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
    .controller('LeaveForm', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $filter, $ionicLoading) {
        $scope.$parent.navTitle = "Leave Form";
        $scope.LeaveTypes.type = {};
        $scope.LeaveTypes.type.selected = {};
        $scope.data = {};
        // __DevID
        $scope.campaign = {};
        $scope.campaign.dateRange = [];


             fmcgAPIservice.getDataList('POST', 'get/LeaveAvailabilityCheck&Year='+ new Date().getFullYear(),[]).success(function(response) {
                    $scope.LeaveValuesCheck = response;
                    if ($scope.LeaveValuesCheck == undefined || $scope.LeaveValuesCheck.length == 0) {
                        $scope.msg = true;
                    }

                    $ionicLoading.hide();
                }).error(function() {
                    Toast("No Internet Connection!");
                    $ionicLoading.hide();
                });

        // __DevID
        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.campaign.add_months = function(dt, n) {
            if (dt != undefined) {
                dateArray = dt.split("/");
                dt = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0])
                return new Date(dt.setMonth(dt.getMonth() + n));
            }
        }

        fmcgAPIservice.getDataList('POST', 'vwCheckLeave', [])
            .success(function(response) {
                $scope.campaign.dateRange = response;
                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });

        $scope.save = function() {
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


                fmcgAPIservice.addMAData('POST', 'dcr/save&sf_name=' + fieldforceName, "42", $scope.data).success(function(response) {
                if (response.success) {
                  if(response.Msg!=""){
                 $ionicLoading.hide();
                  Toast(response.Msg);
                  }else{
                     fmcgAPIservice.addMAData('POST', 'dcr/save&sf_name=' + fieldforceName, "12", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("Leave Form Submitted Successfully");
                    $ionicLoading.hide();
                    $state.go('fmcgmenu.home');
                }
            });
                  }
           
                }
            });



        }


    })
    .directive("startDateCalendar", [
        function() {
            return function(scope, element, attrs) {
                scope.$watch("campaign.dateRange", (function(newValue, oldValue) {
                    element.datepicker({
                        beforeShowDay: function(date) {
                            var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                            return [scope.campaign.dateRange.indexOf(dateString) == -1];
                        }
                    })
                }), true)
                return element.datepicker({
                    dateFormat: "dd/mm/yy",
                    numberOfMonths: 1,
                    //minDate: new Date(),
                    //maxDate: scope.campaign.end_at,
                    //beforeShowDay: $.datepicker.noWeekends,
                    beforeShowDay: function(date) {
                        var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                        return [scope.campaign.dateRange.indexOf(dateString) == -1];
                    },
                    onSelect: function(date) {
                        scope.campaign.start_at = date;
                        scope.data.No_of_Days = 0;

                        scope.$apply();
                        scope.campaign.endDat = scope.campaign.add_months(date, 1);

                    }
                });
            };
        }

    ]).directive("endDateCalendar", [
        function() {
            return function(scope, element, attrs) {
                scope.$watch("campaign.dateRange", (function(newValue, oldValue) {
                    element.datepicker({
                        beforeShowDay: function(date) {
                            var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                            return [scope.campaign.dateRange.indexOf(dateString) == -1];
                        }
                    })
                }), true)
                scope.$watch("campaign.start_at", (function(newValue, oldValue) {
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
                    //minDate: scope.campaign.start_at,
                    defaultDate: scope.campaign.end_at,
                    maxDate: scope.campaign.endDat,
                    beforeShowDay: function(date) {
                        var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                        return [scope.campaign.dateRange.indexOf(dateString) == -1];
                    },
                    onSelect: function(date) {
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

    ]).directive('myDirective', [ function() {
        function link(scope, elem, attrs, ngModel) {
            ngModel.$parsers.push(function(viewValue) {
              var reg = /^[^`~!@#$%\^&*()_+={}|[\]\\:';"<>?,./1-9]*$/;
              // if view values matches regexp, update model value
              if (viewValue.match(reg)) {
                return viewValue;
              }
              // keep the model value as it is
              var transformedValue = ngModel.$modelValue;
              ngModel.$setViewValue(transformedValue);
              ngModel.$render();
              return transformedValue;
            });
        }

        return {
            restrict: 'A',
            require: 'ngModel',
            link: link
        };      
    }]).directive("datepicker", [ function () {

        function link(scope, element, attrs) {
            // CALL THE "datepicker()" METHOD USING THE "element" OBJECT.
            element.datepicker({
                dateFormat: "dd/mm/yy"
            });
        }

        return {
            require: 'ngModel',
            link: link
        };
    }])
    .controller('RMCLCtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage) {
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
            columnDefs: [{
                field: 'product_Nm',
                displayName: 'Product',
                enableCellEdit: false,
                cellTemplate: 'partials/productCellTemplate.html'
            }, {
                field: 'qty',
                displayName: 'Qty',
                enableCellEdit: true,
                editableCellTemplate: "partials/cellEditTemplate.html",
                width: 60
            }, {
                field: 'remove',
                displayName: '',
                enableCellEdit: false,
                cellTemplate: 'partials/removeButton.html',
                width: 50
            }]
        };

        if ($scope.cComputer) {
            $scope.RMCL.eDate = new Date();
        } else {
            window.sangps.getDateTime(function(tDate) {
                $scope.RMCL.eDate = tDate;
            });
        }
        $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.RMCL.productSelectedList.splice(index, 1);
        };
        $scope.clear = function() {
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
        $scope.save = function() {
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

            $scope.data.RMCLDt = $scope.RMCL.eDate;
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

            fmcgAPIservice.addMAData('POST', 'dcr/save', "5", $scope.data).success(function(response) {
                if (response.success)
                    Toast("Reminder Calls Submited Successfully");
            }).error(function() {
                $scope.OutRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
                $scope.OutRMCL.push($scope.data);
                localStorage.removeItem("OutBx_RMCL");
                fmcgLocalStorage.createData("OutBx_RMCL", $scope.OutRMCL);
                Toast("No Internet Connection!.\n Reminder Calls Saved in Outbox");
            });
            $state.go('fmcgmenu.home');
        };
    })
    .controller('orderEditCtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, $ionicLoading) {

        $scope.edit = function(item) {
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
                    if (prod[1].indexOf('$') > 0) {
                        var prodQ = prod[1].split('$');

                        pTemp.sample_qty = Number(prodQ[0]);
                        var prodQ = prodQ[1].split('@');
                        pTemp.rx_qty = prodQ[0];
                        var prodQ = prodQ[1].split('+');
                        pTemp.product_netwt = prodQ[0];
                        var prodQ = prodQ[1].split('%');
                        pTemp.free = prodQ[0];
                        var prodQ = prodQ[1].split('-');
                        pTemp.discount = prodQ[0];
                        pTemp.discount_price = prodQ[1];
                        pTemp.netweightvalue = pTemp.product_netwt * pTemp.rx_qty;
                        if (pTemp.product.length !== 0)
                            $scope.tData1['productSelectedList'].push(pTemp);
                    } else {
                        pTemp.sample_qty = Number(prodQ[0]);
                    }
                }
            }
            $scope.$parent.fmcgData.productSelectedList = $scope.tData1['productSelectedList'];
            $scope.fmcgData.productSelectedList = $scope.tData1['productSelectedList'];
            $scope.fmcgData.pob = item.POB;
            $scope.fmcgData.value = item.POB_Value;
            $scope.fmcgData.DCR_Code = item.DCR_Code;
            $scope.fmcgData.Cust_Code = item.Cust_Code;
            $scope.fmcgData.Trans_Sl_No = item.Trans_Sl_No;
            $scope.fmcgData.netweightvaluetotal = item.net_weight_value;
            $scope.fmcgData.rateType = item.rateMode;
            $scope.fmcgData.discount_price = item.discount_price;
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
    .controller('precallCtrl', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, $ionicLoading) {
        $scope.$parent.navTitle = "Precall Analysis";
        if ($scope.view_MR == 1) {
            $scope.precall.subordinate = {};
            $scope.precall.subordinate.selected = {};
            $scope.precall.subordinate.selected.id = $scope.sfCode;
            $scope.loadDatas(false, '_' + $scope.sfCode)
        } else {
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
        $scope.$on('clear', function(evnt, DrCd) {
            $scope.vwPreCall = undefined;
        });
        $scope.precall.doctor = undefined
        $scope.$on('getPreCall', function(evnt, DrCd) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var id = $scope.precall.subordinate.selected.id;
            var towns = $scope.myTpTwns;
            var data = $scope.doctors;
            $scope.FilteredData = data.filter(function(a) {
                return (a.town_code === id);
            });
            $scope.precall.TotalCalls = $scope.FilteredData.length;
            //                $scope.precall.productivity=

            fmcgAPIservice.getPostData('POST', 'get/precall&Msl_No=' + DrCd.DrId, []).success(function(response) {
                var StockistDetails = response.StockistDetails;
                for (var key in StockistDetails) {
                    var stockiesCode = StockistDetails[key]['stockist_code'];
                    var stockists = $scope.stockists;
                    for (var key1 in stockists) {
                        if (stockists[key1]['id'] == stockiesCode)
                            StockistDetails[key]['stockist_code'] = stockists[key1]['name'];
                    }
                }
                $scope.vwPreCall = response;
                $scope.StockistDetails = StockistDetails;
                $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection!");
                $ionicLoading.hide();
            });
        });
    })
    .controller('draftCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
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
            $ionicPopup.confirm({
                    title: 'Call Delete',
                    content: 'Are you sure want to Delete?'
                })
                .then(function(res) {
                    if (res) {
                        localStorage.removeItem("draft");
                        $state.go('fmcgmenu.home');
                        Toast("draft deleted successfully");
                    } else {
                        console.log('You are not sure');
                    }
                });
        };
        $scope.editEntry = function() {
            var value = fmcgLocalStorage.getData("draft");
            for (key in value[0]) {
                if (key) {
                    $scope.$parent.fmcgData[key] = value[0][key];
                }
            }
            localStorage.removeItem("draft");
            $state.go('fmcgmenu.addNew');
        }
        $scope.saveEntry = function() {
            var temp = fmcgLocalStorage.getData("draft");
            fmcgAPIservice.saveDCRData('POST', 'dcr/save', temp[0], false).success(function(response) {
                if (!response['success'] && response['type'] == 1) {
                    $scope.showConfirm = function() {
                        var confirmPopup = $ionicPopup.confirm({
                            title: 'Warning !',
                            template: response['msg']
                        });
                        confirmPopup.then(function(res) {
                            if (res) {
                                fmcgAPIservice.updateDCRData('POST', 'dcr/save&replace', response['data'], false).success(function(response) {
                                    if (!response['success']) {
                                        Toast(response['msg'], true);
                                    } else {
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
                } else if (!response['success']) {
                    Toast(response['msg'], true);
                    localStorage.removeItem("draft");
                } else {
                    Toast("call Submited Successfully");
                    localStorage.removeItem("draft");
                }
            });

        }

        //$scope.$parent.navTitle = "Submitted Calls";
        $scope.customers = [{
            'id': '1',
            'name': $scope.EDrCap,
            'url': 'manageDoctorResult'
        }];
        var al = 1;
        if ($scope.ChmNeed != 1) {
            $scope.customers.push({
                'id': '2',
                'name': $scope.EChmCap,
                'url': 'manageChemistResult'
            });
            $scope.cCI = al;
            al++;
        }
        if ($scope.StkNeed != 0) {
            $scope.customers.push({
                'id': '3',
                'name': $scope.EStkCap,
                'url': 'manageStockistResult'
            });
            $scope.sCI = al;
            al++;
        }
        if ($scope.UNLNeed != 1) {
            $scope.customers.push({
                'id': '4',
                'name': $scope.ENLCap,
                'url': 'manageStockistResult'
            });
            $scope.nCI = al;
            al++;
        }
        $scope.getData = function() {
            response = fmcgLocalStorage.getEntryCount();
            if (response['success']) {
                $scope.success = true;
                $scope.customers[0].count = response['data']['doctor_count'];
                if ($scope.ChmNeed != 1)
                    $scope.customers[$scope.cCI].count = response['data']['chemist_count'];
                if ($scope.StkNeed != 0)
                    $scope.customers[$scope.sCI].count = response['data']['stockist_count'];
                if ($scope.UNLNeed != 1)
                    $scope.customers[$scope.nCI].count = response['data']['uldoctor_count'];
            } else if (response['ndata']) {
                $scope.nodata = true;
            } else {
                $scope.owsuccess = true;
                $scope.owTypeData = response['data'];
            }
            $ionicLoading.hide();
        };
        $scope.getData();
    })
    .controller('outboxCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.$parent.navTitle = "Outbox Calls";
        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.clearData();
        $scope.success = false;
        $scope.nodata = false;
        $scope.owsuccess = false;
        $scope.owTypeData = [];
        $scope.oxOffRet = fmcgLocalStorage.getData("CustAddQ") || [];

        $scope.oxRmks = fmcgLocalStorage.getData("MyDyRmksQ") || [];
        $scope.oxMyPln = fmcgLocalStorage.getData("myplnQ") || [];
        $scope.delOutbxMyPln = function(item) {
            $ionicPopup.confirm({
                title: 'Call Delete',
                content: 'Are you sure you want to Delete?'
            }).then(function(res) {
                if (res) {
                    $scope.oxMyPln.splice($scope.oxMyPln.indexOf(item), 1);
                    fmcgLocalStorage.createData('myplnQ', $scope.oxMyPln);
                    Toast("My day plan successfully deleted from Outbox");
                } else {
                    console.log('You are not sure');
                }
            });
        };
        $scope.oxRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
        $scope.deleteOutbox = function(item) {
            $ionicPopup.confirm({
                title: 'Call Delete',
                content: 'Are you sure you want to Delete?'
            }).then(function(res) {
                if (res) {
                    $scope.oxRCPA.splice($scope.oxRCPA.indexOf(item), 1);
                    fmcgLocalStorage.createData('OutBx_RCPA', $scope.oxRCPA);
                    Toast("Market Audit successfully deleted from Outbox");
                } else {
                    console.log('You are not sure');
                }
            });
        };
        $scope.oxRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
        $scope.delOutbxRMCL = function(item) {
            $ionicPopup.confirm({
                title: 'Call Delete',
                content: 'Are you sure you want to Delete?'
            }).then(function(res) {
                if (res) {
                    $scope.oxRMCL.splice($scope.oxRMCL.indexOf(item), 1);
                    fmcgLocalStorage.createData('OutBx_RMCL', $scope.oxRMCL);
                    Toast("Reminder call successfully deleted from Outbox");
                } else {
                    console.log('You are not sure');
                }
            });
        };



        $scope.deleteEntry = function(DelType) {
            $ionicPopup.confirm({
                title: 'Call Delete',
                content: 'Are you sure you want to Delete?'
            }).then(function(res) {

                if (res) {
                    localStorage.removeItem("saveLater");
                    $state.go('fmcgmenu.home');
                    Toast("draft deleted successfully");
                } else {
                    console.log('You are not sure');
                }
            });
        };
        $scope.customers = [{
            'id': '1',
            'name': $scope.EDrCap,
            'url': 'manageDoctorResult'
        }];
        var al = 1;
        if ($scope.ChmNeed != 1) {
            $scope.customers.push({
                'id': '2',
                'name': $scope.EChmCap,
                'url': 'manageChemistResult'
            });
            $scope.cCI = al;
            al++;
        }
        if ($scope.StkNeed != 0) {
            $scope.customers.push({
                'id': '3',
                'name': $scope.EStkCap,
                'url': 'manageStockistResult'
            });
            $scope.sCI = al;
            al++;
        }
        if ($scope.UNLNeed != 1) {
            $scope.customers.push({
                'id': '4',
                'name': $scope.ENLCap,
                'url': 'manageStockistResult'
            });
            $scope.nCI = al;
            al++;
        }
        $scope.getData = function() {
            response = fmcgLocalStorage.getOutboxCount();
            if (response['success']) {
                $scope.success = true;
                $scope.customers[0].count = response['data']['doctor_count'];
                if ($scope.ChmNeed != 1)
                    $scope.customers[$scope.cCI].count = response['data']['chemist_count'];
                if ($scope.StkNeed != 0)
                    $scope.customers[$scope.sCI].count = response['data']['stockist_count'];
                if ($scope.UNLNeed != 1)
                    $scope.customers[$scope.nCI].count = response['data']['uldoctor_count'];
            } else if (response['ndata']) {
                $scope.nodata = true;
            } else {
                $scope.owsuccess = true;
                $scope.owTypeData = response['data'];
            }
            $ionicLoading.hide();
        };
        $scope.getData();
        if ($scope.oxRCPA.length > 0 || $scope.oxRMCL.length > 0 || $scope.oxMyPln.length > 0 || $scope.oxRmks.length > 0 || $scope.oxOffRet.length > 0) {
            $scope.nodata = false;
        }
        $scope.nDtaRCPA = ($scope.oxRCPA.length > 0) ? false : true;
        $scope.nDtaRMCL = ($scope.oxRMCL.length > 0) ? false : true;
        $scope.nDtaMyPL = ($scope.oxMyPln.length > 0) ? false : true;
        $scope.nDtaRmks = ($scope.oxRmks.length > 0) ? false : true;
        $scope.nDtaOffRet = ($scope.oxOffRet.length > 0) ? false : true;

    })
    .controller('transCurrentStocksCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter,$ionicScrollDelegate) {
        $scope.$parent.navTitle = "Current Stocks View";
        $scope.stockUpdation.stockist = {};
        $scope.stockUpdation.stockist.selected = {};
        $scope.stockUpdation.ClDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
        $scope.$parent.fmcgData.currentLocation = "fmcgmenu.transCurrentStocks";
        $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
        if ($scope.view_STOCKIST == 1)
            $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];

        $scope.$on('GetStocks', function(evnt) {
            $scope.GetCurrentStock();
        });
        if ($scope.view_STOCKIST == 1)
            getStock();

           var temp = window.localStorage.getItem("mypln");
                    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;

           $scope.sendWhatsApp = function() {
                xelem = $(event.target).closest('.scroll').find('#vstpreview');
                $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                $('ion-nav-view').css("height", $(xelem).height() + 300);
                $('body').css("overflow", 'visible');
                $ionicScrollDelegate.scrollTop();
                SendPDFtoSocialShare($(xelem), 'Summary Dashboard', 'Summary Dashboard', "w");
                /*
                html2canvas($(xelem), {
                    onrendered: function (canvas) {
                        var canvasImg = canvas.toDataURL("image/jpg");
                        getCanvas = canvas;
                        $('.modal').css("height", "");
                        $('.ion-content').css("height", "");
                        $('.scroll').css("height","");
                        $('ion-nav-view').css("height", "");
                        $('body').css("overflow", 'hidden');
                       // console.log(canvasImg);
                        window.plugins.socialsharing.shareViaWhatsApp('Summary Dashboard', canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                    }
                });
                */
            }

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
            console.log($scope.stockUpdation.ClDate);
            fmcgAPIservice.getPostData('POST', 'get/currentStock&scode=' + $sCode + '&cldt=' + $scope.stockUpdation.ClDate, []).success(function(response) {
                $scope.currentStocks = response;
                $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection!");
                $ionicLoading.hide();
            });
        }
        $scope.GetCurrentStock = function() {
            getStock();
        }
        $scope.edit = function(item) {
            var value = $scope.currentStocks;
            $scope.$parent.fmcgData.editable = 1;
            $scope.$parent.fmcgData.productSelectedList = value;

            $scope.$parent.fmcgData.NavEdit = 1;
            $scope.fmcgData.NavEdit = 1;
            $state.go('fmcgmenu.stockUpdation');
        };
        $scope.editprimary = function(item) {
            var value = $scope.currentStocks;
            $scope.$parent.fmcgData.editable = 1;
            $scope.$parent.fmcgData.productSelectedList = value;
            $state.go('fmcgmenu.stockUpdation');
        };
    })



 .controller('transSSCurrentStocksCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
        $scope.$parent.navTitle = "Current SS Stocks View";
        $scope.stockUpdation.stockist = {};
        $scope.stockUpdation.stockist.selected = {};
        $scope.stockUpdation.ClDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
        $scope.$parent.fmcgData.currentLocation = "fmcgmenu.transCurrentStocks";
        $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_SSUpdation_Date") || [];
        if ($scope.view_STOCKIST == 1)
            $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_SSUpdation_Date'];

        $scope.$on('GetStocks', function(evnt) {
            $scope.GetCurrentStock();
        });
        if ($scope.view_STOCKIST == 1)
            getStock();

           var temp = window.localStorage.getItem("mypln");
                    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;

           
        function getStock() {
            $scope.tdate = $filter('date')(new Date(), 'dd/MM/yyyy');
            if ($scope.LastSSUpdationDate == $scope.tdate)
                $scope.editing = 0;
            else
                $scope.editing = 1;
            if ($scope.view_STOCKIST != 1 && $scope.stockUpdation.stockist.selected != undefined)
                $sCode = $scope.stockUpdation.stockist.selected.id
            else
                $sCode = "0";
            console.log($scope.stockUpdation.ClDate);
            fmcgAPIservice.getPostData('POST', 'get/currentSSStock&scode=' + $sCode + '&cldt=' + $scope.stockUpdation.ClDate, []).success(function(response) {
                $scope.currentStocks = response;
                $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection!");
                $ionicLoading.hide();
            });
        }



$scope.getDayReports = function() {
     $ionicLoading.show({
            template: 'Loading...'
        });
     $scope.tdate = $filter('date')(new Date(), 'dd/MM/yyyy');
            if ($scope.LastSSUpdationDate == $scope.tdate)
                $scope.editing = 0;
            else
                $scope.editing = 1;
            if ($scope.view_STOCKIST != 1 && $scope.stockUpdation.stockist.selected != undefined)
                $sCode = $scope.stockUpdation.stockist.selected.id
            else
                $sCode = "0";
            console.log($scope.stockUpdation.ClDate);
            fmcgAPIservice.getPostData('POST', 'get/currentSSStock&scode=' + $sCode + '&cldt=' + $scope.stockUpdation.ClDate, []).success(function(response) {
                $scope.currentStocks = response;
                $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection!");
                $ionicLoading.hide();
            });

}




        $scope.GetCurrentStock = function() {
            getStock();
        }
        $scope.edit = function(item) {
            var value = $scope.currentStocks;
            $scope.$parent.fmcgData.editable = 1;
            $scope.$parent.fmcgData.productSelectedList = value;

            $scope.$parent.fmcgData.NavEdit = 1;
            $scope.fmcgData.NavEdit = 1;
            $state.go('fmcgmenu.SSstockUpdation');
        };
        $scope.editprimary = function(item) {
            var value = $scope.currentStocks;
            $scope.$parent.fmcgData.editable = 1;
            $scope.$parent.fmcgData.productSelectedList = value;
            $state.go('fmcgmenu.SSstockUpdation');
        };
    })

.controller('CollectionReportctrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
    $scope.$parent.navTitle = "Collection Details";
    $scope.stockUpdation.stockist = {};
    $scope.stockUpdation.stockist.selected = {};

    $scope.stockUpdation.ClDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.CollectionReport";
    $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
    if ($scope.view_STOCKIST == 1)
        $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];
    $scope.data = {
        "Lattitude": 13.0300833,
        "Langitude": 80.2414837,
        "StartTime": 1
    };
    $scope.$on('GetStocks', function(evnt) {
        $scope.GetCurrentStock();
    });
    fmcgAPIservice.getPostData('POST', 'get/Todaycollectionreport', $scope.data).success(function(response) {
        $scope.Today_Collection_values = response;
        if ($scope.Today_Collection_values == undefined || $scope.Today_Collection_values.length == 0) {


            $scope.thiru = true;
        }


        $ionicLoading.hide();
    }).error(function() {
        Toast("No Internet Connection!");
        $ionicLoading.hide();
    });
    $scope.GetCustomername = function(x) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var data = fmcgLocalStorage.getData('doctor_master_' + userData.sfCode) || [];

        var types = data.filter(function(a) {
            return (a.id == x)
        });


        // getStock();
        return types[0].name;
    }
    $scope.goBack = function() {
        $state.go('fmcgmenu.reports');

    };

    $scope.getqtyTotal = function() {
        var total = 0;
        var Wtyps = $scope.Today_Collection_values;


        angular.forEach(Wtyps, function(el) {
            total = total + parseInt(el['CollAmt']);
        });
        return total;


    };
    $scope.date = new Date();



})


.controller('dailytotalcallsrportctrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
    $scope.$parent.navTitle = "Daily Status";
    $scope.stockUpdation.stockist = {};
    $scope.stockUpdation.stockist.selected = {};
    if ($scope.view_STOCKIST == 1)
        $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];
         fmcgAPIservice.getPostData('POST', 'get/retailercountfdc',[]).success(function(response) {
        
          if (response.TotalCalls>0)
           response.TotalValues = (response.TotalCalls / response.Productive * 100).toFixed(2)+'%';
           $scope.Dailystatuscal = response;
        if ($scope.Dailystatuscal == undefined || $scope.Dailystatuscal.length == 0) {


        }

        $ionicLoading.hide();
    }).error(function() {
        Toast("No Internet Connection!");
        $ionicLoading.hide();
    });
   

    $scope.goBack = function() {
        $state.go('fmcgmenu.reports');

    };

      $scope.showretailer = function() {
        $scope.msg=true;

    };

    $scope.date = new Date();



})



.controller('TargetAndAchievectrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
    $scope.$parent.navTitle = "Target And Achieve";
    $scope.stockUpdation.stockist = {};
    $scope.stockUpdation.stockist.selected = {};

    $scope.stockUpdation.ClDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.CollectionReport";
    $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
    if ($scope.view_STOCKIST == 1)
        $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];
    $scope.data = {
        "Lattitude": 13.0300833,
        "Langitude": 80.2414837,
        "StartTime": 1
    };
    $scope.$on('GetStocks', function(evnt) {
        $scope.GetCurrentStock();
    });
    fmcgAPIservice.getPostData('POST', 'get/Todaycollectionreport', $scope.data).success(function(response) {
        $scope.Today_Collection_values = response;
        if ($scope.Today_Collection_values == undefined || $scope.Today_Collection_values.length == 0) {


            $scope.thiru = true;
        }


        $ionicLoading.hide();
    }).error(function() {
        Toast("No Internet Connection!");
        $ionicLoading.hide();
    });
    $scope.GetCustomername = function(x) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var data = fmcgLocalStorage.getData('doctor_master_' + userData.sfCode) || [];

        var types = data.filter(function(a) {
            return (a.id == x)
        });


        // getStock();
        return types[0].name;
    }
    $scope.goBack = function() {
        $state.go('fmcgmenu.reports');

    };

    $scope.getqtyTotal = function() {
        var total = 0;
        var Wtyps = $scope.Today_Collection_values;


        angular.forEach(Wtyps, function(el) {
            total = total + parseInt(el['CollAmt']);
        });
        return total;


    };
    $scope.date = new Date();



})



.controller('stockUpdationPrimaryCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
    $scope.$parent.navTitle = "Primary Stock Entry";
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.stockUpdationPrimary";
    $scope.myplan = fmcgLocalStorage.getData("mypln") || [];
    if ($scope.$parent.fmcgData.editable != 1) {
        $scope.stockUpdation.stockist = {};
        $scope.stockUpdation.stockist.selected = {};
    }

    $scope.Last_Updation_Date = [];
    $scope.tdate = $filter('date')(new Date(), 'dd/MM/yyyy');
    $scope.fmcgData.value1 = 0;
    $scope.fmcgData.value2 = 0;
    $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
    $scope.setAllow();
    $scope.$on('GetStocks', function(evnt) {
        $scope.GetCurrentStock();
    });
    $scope.GetCurrentStock = function() {
        getStock();
    }
    if ($scope.view_STOCKIST == 1)
        getStock();

    function getStock() {
        if ($scope.view_STOCKIST == 1)
            $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];
        else {
            if ($scope.prod == 0)
                $scope.$parent.fmcgData.productSelectedList = [];
        }
        if ($scope.LastUpdationDate == $scope.tdate)
            $scope.editing = 0;
        else
            $scope.editing = 1;
        if ($scope.view_STOCKIST != 1 && $scope.stockUpdation.stockist.selected != undefined)
            $sCode = $scope.stockUpdation.stockist.selected.id
        else
            $sCode = "0";
        fmcgAPIservice.getPostData('POST', 'get/currentStock&scode=' + $sCode, []).success(function(response) {
            $scope.currentStocks = response;
            $ionicLoading.hide();
        }).error(function() {
            Toast("No Internet Connection!");
            $ionicLoading.hide();
        });
    }
    $scope.refresh = function() {
        $scope.stockUpdation.stockist = {};
        $scope.stockUpdation.stockist.selected = {};
        $scope.editing = 1;
        $scope.$parent.fmcgData.editable = 0;
    }
    $scope.edit = function(item) {
        var value = $scope.currentStocks;
        $scope.$parent.fmcgData.editable = 1;
        if ($scope.prod == 1) {
            $scope.$parent.fmcgData.productSelectedList = value;
            productList();
        } else
            $scope.$parent.fmcgData.productSelectedList = value;
        $state.go('fmcgmenu.stockUpdationPrimary');
    };
    if ($scope.$parent.fmcgData.editable == 1)
        productList();

    function productList() {
        $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
        CurrentData = $scope.products;
        for (i = 0; i < CurrentData.length; i++) {
            val = 0;
            for (j = 0; j < $scope.$parent.fmcgData.productSelectedList.length; j++) {
                if (CurrentData[i]['id'] == $scope.$parent.fmcgData.productSelectedList[j]['product']) {
                    val = 1;

                }
            }
            if (val == 0) {
                var productData = {};
                productData.product = CurrentData[i].id;
                productData.product_Nm = CurrentData[i].name;
                productData.recv_qty = CurrentData[i].recv_qty || 0;
                $scope.fmcgData.productSelectedList.push(productData);
            }
        }
    }
    if ($scope.prod == 1 && $scope.$parent.fmcgData.editable != 1) {
        $scope.fmcgData.productSelectedList = [];
        CurrentData = $scope.products;
        for (i = 0; i < CurrentData.length; i++) {
            var productData = {};
            productData.product = CurrentData[i].id;
            productData.product_Nm = CurrentData[i].name;
            productData.recv_qty = CurrentData[i].recv_qty || 0;
            $scope.fmcgData.productSelectedList.push(productData);
        }
    }

    $scope.addProduct = function(selected) {
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

        $scope.gridOptions = {
            data: 'fmcgData.productSelectedList',
            rowHeight: 50,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            enableRowSelection: false,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: [{
                field: 'product_Nm',
                displayName: 'Product',
                enableCellEdit: false,
                cellTemplate: 'partials/productCellTemplate.html'
            }, {
                field: 'recv_qty',
                displayName: "P.Stock",
                enableCellEdit: true,
                editableCellTemplate: "partials/cellEditTemplate.html",
                width: 70,
                "visible": true
            }]

        };

    }


    $scope.find = function() {
        if (this.row.entity.pieces >= this.row.entity.conversionQty) {
            // var str = "Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")";
            alert("Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")");
            this.row.entity.pieces = 0;
        }
    }
    $scope.fmcgData.value = 0;
    $scope.removeRow = function() {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.productSelectedList.splice(index, 1);
    };
    $scope.save = function() {
        //  var products = $scope.$parent.fmcgData.productSelectedList;
        products = [];
        product_all = $scope.$parent.fmcgData.productSelectedList;
        for (i = 0; i < product_all.length; i++) {
            if (product_all[i]['recv_qty'] > 0 || product_all[i]['cb_qty'] > 0 || product_all[i]['pieces'] > 0)
                products.push(product_all[i])
        }
        if ($scope.$parent.fmcgData.editable == 1)
            var save = 'dcr/save&editable=' + 1;
        else
            save = 'dcr/save&editable=' + 0;
        // if ($scope.SF_type == 1)
        $sCode = $scope.stockUpdation.stockist.selected.id
            /* else
                 $sCode = "0";*/
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
            .success(function(response) {
                if (response['success']) {
                    var date = new Date();
                    if ($scope.view_STOCKIST == 1) {
                        localStorage.removeItem("Last_Updation_Date");
                        var Last_Updation_Date = [{
                            "Last_Updation_Date": $filter('date')(new Date(), 'dd/MM/yyyy')
                        }]
                    } else {
                        var Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                        stat = 0;
                        for (key in Last_Updation_Date) {
                            if (Last_Updation_Date[key]['stockistCode'] == $sCode) {
                                stat = 1;
                                Last_Updation_Date[key]['Last_Updation_Date'] = $filter('date')(new Date(), 'dd/MM/yyyy');
                            }
                        }
                        if (stat == 0)
                            Last_Updation_Date.push({
                                "Last_Updation_Date": $filter('date')(new Date(), 'dd/MM/yyyy'),
                                "stockistCode": $sCode
                            });
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
            .error(function() {
                //  $scope.savingData.MyPL = false;
            });
        //                $state.go('fmcgmenu.home');
    }



})

.controller('stockUpdationCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter,$ionicScrollDelegate) {
        $scope.$parent.navTitle = "Closing Stock Entry";
        $scope.$parent.fmcgData.currentLocation = "fmcgmenu.stockUpdation";
        $scope.myplan = fmcgLocalStorage.getData("mypln") || [];
        if ($scope.$parent.fmcgData.editable != 1) {
            $scope.stockUpdation.stockist = {};
            $scope.stockUpdation.stockist.selected = {};
        }
        $scope.campaign = {};
        $scope.campaign.dateRange = [];
        $scope.Last_Updation_Date = [];
        $scope.tdate = $filter('date')(new Date(), 'dd/MM/yyyy');
        $scope.campaign.dateRange=$scope.tdate;
        $scope.fmcgData.value1 = 0;
        $scope.fmcgData.value2 = 0;
        $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
        $scope.setAllow();
        $scope.$on('GetStocks', function(evnt) {
            $scope.GetCurrentStock();
        });
        $scope.GetCurrentStock = function() {
            getStock();
        }
        if ($scope.view_STOCKIST == 1)
            getStock();

        function getStock() {
            if ($scope.view_STOCKIST == 1)
                $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];
            else {
                if ($scope.prod == 0)
                    $scope.$parent.fmcgData.productSelectedList = [];
            }
            if ($scope.LastUpdationDate == $scope.tdate)
                $scope.editing = 0;
            else
                $scope.editing = 1;
            if ($scope.view_STOCKIST != 1 && $scope.stockUpdation.stockist.selected != undefined)
                $sCode = $scope.stockUpdation.stockist.selected.id
            else
                $sCode = "0";
            fmcgAPIservice.getPostData('POST', 'get/currentStock&scode=' + $sCode, []).success(function(response) {
                $scope.currentStocks = response;
                $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection!");
                $ionicLoading.hide();
            });
        }






        
        $scope.refresh = function() {
            $scope.stockUpdation.stockist = {};
            $scope.stockUpdation.stockist.selected = {};
            $scope.editing = 1;
            $scope.$parent.fmcgData.editable = 0;
            $scope.$parent.fmcgData.NavEdit = 0;
            $scope.fmcgData.NavEdit = 0;
            $scope.$parent.fmcgData.productSelectedList = [];
            productList();
        }
        $scope.edit = function(item) {
            var value = $scope.currentStocks;
            $scope.$parent.fmcgData.editable = 1;
            if ($scope.prod == 1) {
                $scope.$parent.fmcgData.productSelectedList = value;
                productList();
            } else
                $scope.$parent.fmcgData.productSelectedList = value;
            $state.go('fmcgmenu.stockUpdation');
        };
        if ($scope.$parent.fmcgData.editable == 1)
            productList();

        function productList() {
            $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
            CurrentData = $scope.products;
            for (i = 0; i < CurrentData.length; i++) {
                val = 0;
                for (j = 0; j < $scope.$parent.fmcgData.productSelectedList.length; j++) {
                    if (CurrentData[i]['id'] == $scope.$parent.fmcgData.productSelectedList[j]['product'])
                        val = 1;
                }
                if (val == 0) {
                    var productData = {};
                    productData.product = CurrentData[i].id;
                    productData.product_Nm = CurrentData[i].name;
                    productData.recv_qty = CurrentData[i].recv_qty || 0;
                    productData.cb_qty = CurrentData[i].cb_qty || 0;
                    productData.pieces = CurrentData[i].pieces || 0;
                    $scope.fmcgData.productSelectedList.push(productData);
                }
            }
        }
        if ($scope.prod == 1 && $scope.$parent.fmcgData.editable != 1) {
            $scope.fmcgData.productSelectedList = [];
            CurrentData = $scope.products;
            for (i = 0; i < CurrentData.length; i++) {
                var productData = {};
                productData.product = CurrentData[i].id;
                productData.product_Nm = CurrentData[i].name;
                productData.recv_qty = CurrentData[i].recv_qty || 0;
                productData.cb_qty = CurrentData[i].cb_qty || 0;
                productData.pieces = CurrentData[i].pieces || 0;
                $scope.fmcgData.productSelectedList.push(productData);
            }
        }

        $scope.addProduct = function(selected) {
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

          $scope.CL_Mfg_Date=($scope.Cl_MfgDtNeed==1) ? true:false;
            
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
                columnDefs: [{
                    field: 'product_Nm',
                    displayName: 'Product',
                    enableCellEdit: false,
                    cellTemplate: 'partials/productCellTemplate.html'
                },

                 {
                    field: 'Mgf_date',
                    displayName: "MFG",
                    enableCellEdit: true,
                    editableCellTemplate: "partials/CelltemplateMFG.html",
                     width: 80,
                    "visible":$scope.CL_Mfg_Date
                   
                },
                 {
                    field: 'recv_qty',
                    displayName: "P.Stock",
                    enableCellEdit: true,
                    editableCellTemplate: "partials/cellEditTemplate.html",
                    width: 70,
                    "visible": $scope.condRecv
                }, {
                    field: 'cb_qty',
                    displayName: "Box",
                    enableCellEdit: true,
                    editableCellTemplate: "partials/cellEditTemplate2.html",
                    width: 40,
                    "visible": $scope.condClosing
                }
                
                , {
                    field: 'pieces',
                    displayName: "Units",
                    enableCellEdit: true,
                    editableCellTemplate: "partials/cellEditTemplate4.html",
                    width: 65,
                    "visible": $scope.condClosing
                }, {
                    field: 'remove',
                    displayName: '',
                    enableCellEdit: false,
                    cellTemplate: 'partials/removeButton.html',
                    width: 50,
                    "visible": false
                }]

            };

        }



        $scope.find = function() {
            if (this.row.entity.pieces >= this.row.entity.conversionQty) {
                // var str = "Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")";
                alert("Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")");
                this.row.entity.pieces = 0;
            }
        }
        $scope.fmcgData.value = 0;
        $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.productSelectedList.splice(index, 1);
        };
        $scope.save = function() {
            //  var products = $scope.$parent.fmcgData.productSelectedList;
            products = [];
            product_all = $scope.$parent.fmcgData.productSelectedList;
            for (i = 0; i < product_all.length; i++) {
                if (product_all[i]['recv_qty'] > 0 || product_all[i]['cb_qty'] > 0 || product_all[i]['pieces'] > 0)
                    products.push(product_all[i])
            }
            if ($scope.$parent.fmcgData.editable == 1)
                var save = 'dcr/save&editable=' + 1;
            else
                save = 'dcr/save&editable=' + 0;
            //if ($scope.SF_type == 1)
            $sCode = $scope.stockUpdation.stockist.selected.id
                /*else
                    $sCode = "0";*/
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
                .success(function(response) {
                    if (response['success']) {
                        var date = new Date();
                        if ($scope.view_STOCKIST == 1) {
                            localStorage.removeItem("Last_Updation_Date");
                            var Last_Updation_Date = [{
                                "Last_Updation_Date": $filter('date')(new Date(), 'dd/MM/yyyy')
                            }]
                        } else {
                            var Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                            stat = 0;
                            for (key in Last_Updation_Date) {
                                if (Last_Updation_Date[key]['stockistCode'] == $sCode) {
                                    stat = 1;
                                    Last_Updation_Date[key]['Last_Updation_Date'] = $filter('date')(new Date(), 'dd/MM/yyyy');
                                }
                            }
                            if (stat == 0)
                                Last_Updation_Date.push({
                                    "Last_Updation_Date": $filter('date')(new Date(), 'dd/MM/yyyy'),
                                    "stockistCode": $sCode
                                });
                        }
                        window.localStorage.setItem("Last_Updation_Date", JSON.stringify(Last_Updation_Date));
                        $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                        if ($scope.fmcgData.NavEdit == 1) {
                            $scope.refresh();
                            $state.go('fmcgmenu.transCurrentStocks');
                        } else {
                            $scope.refresh();
                        }
                        $ionicLoading.hide();
                        Toast('Stock Updated', true);
                    }
                    //                                        fmcgLocalStorage.createData("myplnQ", QDataMyPL);
                    //                                        $scope.savingData.MyPL = false;
                })
                .error(function() {
                    //  $scope.savingData.MyPL = false;
                });
            //                $state.go('fmcgmenu.home');
        }
  /* $scope.datepickerclosing= function() {

                 this.row.elm.datepicker({
                    dateFormat: "dd/mm/yy",
                    numberOfMonths: 1,
                    minDate: new Date(),
                     beforeShowDay: function(date) {
                        var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                        return [$scope.campaign.dateRange.indexOf(dateString) == -1];;
                    },
                    onSelect: function(date) {
                        
                    }
                });
}*/



    })


 
.controller('SSstockUpdationCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
        $scope.$parent.navTitle = "Closing SStock Entry";
        $scope.$parent.fmcgData.currentLocation = "fmcgmenu.SSstockUpdation";
        $scope.myplan = fmcgLocalStorage.getData("mypln") || [];
        if ($scope.$parent.fmcgData.editable != 1) {
            $scope.stockUpdation.stockist = {};
            $scope.stockUpdation.stockist.selected = {};
        }

        $scope.Last_SSUpdation_Date = [];
        $scope.tdate = $filter('date')(new Date(), 'dd/MM/yyyy');
        $scope.fmcgData.value1 = 0;
        $scope.fmcgData.value2 = 0;
        $scope.Last_SSUpdation_Date = fmcgLocalStorage.getData("Last_SSUpdation_Date") || [];
        $scope.setAllow();
        $scope.$on('GetStocks', function(evnt) {
            $scope.GetCurrentStock();
        });
        $scope.GetCurrentStock = function() {
            getStock();
        }
        if ($scope.view_STOCKIST == 1)
            getStock();

        function getStock() {
            if ($scope.view_STOCKIST == 1)
                $scope.LastSSUpdationDate = $scope.Last_SSUpdation_Date[0]['Last_SSUpdation_Date'];
            else {
                if ($scope.prod == 0)
                    $scope.$parent.fmcgData.productSelectedList = [];
            }
            if ($scope.LastSSUpdationDate == $scope.tdate)
                $scope.editing = 0;
            else
                $scope.editing = 1;
            if ($scope.view_STOCKIST != 1 && $scope.stockUpdation.stockist.selected != undefined)
                $sCode = $scope.stockUpdation.stockist.selected.id
            else
                $sCode = "0";
            fmcgAPIservice.getPostData('POST', 'get/currentSSStock&scode=' + $sCode, []).success(function(response) {
                $scope.currentStocks = response;
                $ionicLoading.hide();
            }).error(function() {
                Toast("No Internet Connection!");
                $ionicLoading.hide();
            });
        }
        $scope.refresh = function() {
            $scope.stockUpdation.stockist = {};
            $scope.stockUpdation.stockist.selected = {};
            $scope.Mypln.SuperStokit={};
            $scope.editing = 1;
            $scope.$parent.fmcgData.editable = 0;
            $scope.$parent.fmcgData.NavEdit = 0;
            $scope.fmcgData.NavEdit = 0;
            $scope.$parent.fmcgData.productSelectedList = [];
            productList();
        }
        $scope.edit = function(item) {
            var value = $scope.currentStocks;
            $scope.$parent.fmcgData.editable = 1;
            if ($scope.prod == 1) {
                $scope.$parent.fmcgData.productSelectedList = value;
                productList();
            } else
                $scope.$parent.fmcgData.productSelectedList = value;
            $state.go('fmcgmenu.SSstockUpdation');
        };
        if ($scope.$parent.fmcgData.editable == 1)
            productList();

        function productList() {
            $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
            CurrentData = $scope.products;
            for (i = 0; i < CurrentData.length; i++) {
                val = 0;
                for (j = 0; j < $scope.$parent.fmcgData.productSelectedList.length; j++) {
                    if (CurrentData[i]['id'] == $scope.$parent.fmcgData.productSelectedList[j]['product'])
                        val = 1;
                }
                if (val == 0) {
                    var productData = {};
                    productData.product = CurrentData[i].id;
                    productData.product_Nm = CurrentData[i].name;
                    productData.recv_qty = CurrentData[i].recv_qty || 0;
                    productData.cb_qty = CurrentData[i].cb_qty || 0;
                    productData.pieces = CurrentData[i].pieces || 0;
                    $scope.fmcgData.productSelectedList.push(productData);
                }
            }
        }
        if ($scope.prod == 1 && $scope.$parent.fmcgData.editable != 1) {
            $scope.fmcgData.productSelectedList = [];
            CurrentData = $scope.products;
            for (i = 0; i < CurrentData.length; i++) {
                var productData = {};
                productData.product = CurrentData[i].id;
                productData.product_Nm = CurrentData[i].name;
                productData.recv_qty = CurrentData[i].recv_qty || 0;
                productData.cb_qty = CurrentData[i].cb_qty || 0;
                productData.pieces = CurrentData[i].pieces || 0;
                $scope.fmcgData.productSelectedList.push(productData);
            }
        }

        $scope.addProduct = function(selected) {
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
                columnDefs: [{
                    field: 'product_Nm',
                    displayName: 'Product',
                    enableCellEdit: false,
                    cellTemplate: 'partials/productCellTemplate.html'
                }, {
                    field: 'recv_qty',
                    displayName: "P.Stock",
                    enableCellEdit: true,
                    editableCellTemplate: "partials/cellEditTemplate.html",
                    width: 70,
                    "visible": $scope.condRecv
                }, {
                    field: 'cb_qty',
                    displayName: "Box",
                    enableCellEdit: true,
                    editableCellTemplate: "partials/cellEditTemplate2.html",
                    width: 55,
                    "visible": $scope.condClosing
                }, {
                    field: 'pieces',
                    displayName: "Units",
                    enableCellEdit: true,
                    editableCellTemplate: "partials/cellEditTemplate4.html",
                    width: 90,
                    "visible": $scope.condClosing
                }, {
                    field: 'remove',
                    displayName: '',
                    enableCellEdit: false,
                    cellTemplate: 'partials/removeButton.html',
                    width: 50,
                    "visible": false
                }]

            };

        }


        $scope.find = function() {
            if (this.row.entity.pieces >= this.row.entity.conversionQty) {
                // var str = "Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")";
                alert("Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")");
                this.row.entity.pieces = 0;
            }
        }
        $scope.fmcgData.value = 0;
        $scope.removeRow = function() {
            var index = this.row.rowIndex;
            $scope.gridOptions.selectItem(index, false);
            $scope.$parent.fmcgData.productSelectedList.splice(index, 1);
        };
        $scope.save = function() {
            //  var products = $scope.$parent.fmcgData.productSelectedList;
            products = [];
            product_all = $scope.$parent.fmcgData.productSelectedList;
            for (i = 0; i < product_all.length; i++) {
                if (product_all[i]['recv_qty'] > 0 || product_all[i]['cb_qty'] > 0 || product_all[i]['pieces'] > 0)
                    products.push(product_all[i])
            }
            if ($scope.$parent.fmcgData.editable == 1)
                var save = 'dcr/save&editable=' + 1;
            else
                save = 'dcr/save&editable=' + 0;
            //if ($scope.SF_type == 1)
            $sCode = $scope.stockUpdation.stockist.selected.id
                /*else
                    $sCode = "0";*/
            save = save + '&sCode=' + $sCode;
            if ($scope.view_STOCKIST == 0) {
                if ($scope.Mypln.SuperStokit==undefined || $scope.Mypln.SuperStokit.selected.id == undefined) {
                    Toast("Please Select Superstockist");
                    return false;
                }
            }
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.addMAData('POST', save, '48', products)
                .success(function(response) {
                    if (response['success']) {
                        var date = new Date();
                        if ($scope.view_STOCKIST == 1) {
                            localStorage.removeItem("Last_SSUpdation_Date");
                            var Last_SSUpdation_Date = [{
                                "Last_SSUpdation_Date": $filter('date')(new Date(), 'dd/MM/yyyy')
                            }]
                        } else {
                            var Last_SSUpdation_Date = fmcgLocalStorage.getData("Last_SSUpdation_Date") || [];
                            stat = 0;
                            for (key in Last_SSUpdation_Date) {
                                if (Last_SSUpdation_Date[key]['stockistCode'] == $sCode) {
                                    stat = 1;
                                    Last_SSUpdation_Date[key]['Last_SSUpdation_Date'] = $filter('date')(new Date(), 'dd/MM/yyyy');
                                }
                            }
                            if (stat == 0)
                                Last_SSUpdation_Date.push({
                                    "Last_SSUpdation_Date": $filter('date')(new Date(), 'dd/MM/yyyy'),
                                    "stockistCode": $sCode
                                });
                        }
                        window.localStorage.setItem("Last_SSUpdation_Date", JSON.stringify(Last_SSUpdation_Date));
                        $scope.Last_SSUpdation_Date = fmcgLocalStorage.getData("Last_SSUpdation_Date") || [];
                        if ($scope.fmcgData.NavEdit == 1) {
                            $scope.refresh();
                            $state.go('fmcgmenu.transSSCurrentStocks');
                        } else {
                            $scope.refresh();
                        }

                        $ionicLoading.hide();
                        Toast('Stock Updated', true);
                    }
                    //                                        fmcgLocalStorage.createData("myplnQ", QDataMyPL);
                    //                                        $scope.savingData.MyPL = false;
                })
                .error(function() {
                    //  $scope.savingData.MyPL = false;
                });
            //                $state.go('fmcgmenu.home');
        }



    })


.controller('ChangePasswordCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
        $scope.$parent.navTitle = "Change Password";
              $scope.data = {};
        var temp =window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        $scope.save = function() {
        
                if($scope.data.oldpassword=='' || $scope.data.oldpassword==undefined){
                    Toast('Enter The Old Password');
                    return false;
                }

                if($scope.data.newpassword==undefined || $scope.data.newpassword==''){
                    Toast('Enter The New Password');
                    return false;
                }
                if($scope.data.confirmpassword==undefined || $scope.data.confirmpassword==''){
                    Toast('Enter The Confirm Password');
                    return false;
                }
                if($scope.data.confirmpassword!=$scope.data.newpassword){
                    Toast('Password And Confirm Password Must be Samel');
                    return false;
                }
                if($scope.data.oldpassword.toLocaleUpperCase()!=userData.user.password.toLocaleUpperCase()){
                    Toast('Enter The Correct Old Password');
                    return false;
                }

            if(!navigator.onLine){
                Toast("No Internet Connection!");
                return false;
            }

            fmcgAPIservice.addMAData('POST', 'dcr/save', '49', $scope.data)
                .success(function(response) {
                            if (response['success']) {
                    userData.user.password=$scope.data.newpassword;
                    window.localStorage.setItem("loginInfo", JSON.stringify(userData));

                           Toast("Password Changed Sucessfully");
                            var loginData = {};
                            loginData.LOGIN = false;
                            
                            window.localStorage.setItem("LOGIN", JSON.stringify(loginData.LOGIN));
                             $state.go("signin");
                        
                    }
                                                    
                })
                .error(function() {
                            Toast("No Internet Connection!");

                });
                           
        }
$scope.goBack = function() {

        $state.go('fmcgmenu.home');
    };


    })




    .controller('reloadCtrl', function($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.$parent.navTitle = "Master Sync";
        $scope.countInc = 0;

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        $scope.RefreshData = function() {
            var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id == "") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
            $scope.clearAll(false, '_' + sRSF);
        }
        $scope.clearItem = function(value) {
            var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id == "") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
            $scope.clearIdividual(value, 1, '_' + sRSF);
        };



            $scope.ClearDate= function() {
            $ionicPopup.confirm({
                title: 'Today Activity',
                content: 'Do you want to Clear App Data?'
            }).then(function(res) {
                if (res) {

                    $ionicLoading.show({
                        template: 'Loading...'
                    });
                     $scope.data = {};
                   
            $scope.attendanceView = window.localStorage.getItem("attendanceView");

            if ($scope.attendanceView != undefined && $scope.attendanceView == 1) {
                $scope.data.StartTime = 1;
            } else {
                $scope.data.StartTime = 0;
            }
                    fmcgAPIservice.getPostData('POST', 'get/logouttime', $scope.data)
                        .success(function(response) {
                            var loginData = {};
                            loginData.LOGIN = false;
                            
                            window.localStorage.setItem("LOGIN", JSON.stringify(loginData.LOGIN));
                            window.localStorage.clear();
    
                            $state.go("signin");


                        }).error(function() {
                            Toast("No Internet Connection! Try Again.");
                            $ionicLoading.hide();
                        });


                } else {
                    console.log('You are not sure');
                }
            });



        }


    })
    .controller('draftViewCtrl', function($rootScope, $scope, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification) {
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
        $scope.draftButtons = [{
            text: 'Edit',
            type: 'button-calm',
            onTap: function(item) {
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
        }];

        $scope.edit = function(item) {
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
        $scope.save = function(item) {

            fmcgAPIservice.saveDCRData('POST', 'dcr/save', item, false).success(function(response) {
                if (!response['success']) {
                    Toast(response['msg'], true);
                } else {
                    Toast("call Submited Successfully")
                }
                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                fmcgLocalStorage.createData('draft', $scope.drafts);
            });

        };
        $scope.preView = function(item) {
            scrTyp = item.customer.selected.id;
            $scope.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
            $scope.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
            $scope.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
            $scope.modal.par = $scope;
            $scope.modal.draft = item;
            $scope.modal.show();
        };
        $scope.deleteDrftList = function(item) {
            $ionicPopup.confirm({
                title: 'Call Delete',
                content: 'Are you sure you want to Delete?'
            }).then(function(res) {
                if (res) {
                    $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                    fmcgLocalStorage.createData('draft', $scope.drafts);
                    Toast("Call deleted successfully from draft");
                } else {
                    console.log('You are not sure');
                }
            });
        };
        $scope.drafts = [];
        $scope.drafts = fmcgLocalStorage.getData("draft");
    })
    .controller('outboxViewCtrl', function($rootScope, $scope, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.outboxs = fmcgLocalStorage.getData("saveLater");
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
        $scope.preView = function(item) {
            scrTyp = item.customer.selected.id;
            $scope.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
            $scope.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
            $scope.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
            $scope.modal.par = $scope;
            $scope.modal.draft = item;
            $scope.modal.show();
        };
        $scope.deleteOutbxList = function(item) {
            $ionicPopup.confirm({
                title: 'Call Delete',
                content: 'Are you sure you want to Delete?'
            }).then(function(res) {
                if (res) {
                    $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                    fmcgLocalStorage.createData('saveLater', $scope.drafts);
                    Toast("Call deleted successfully from Outbox");
                } else {
                    console.log('You are not sure');
                }
            });
        };
        $scope.drafts = [];
        $scope.drafts = fmcgLocalStorage.getData("saveLater");
    })
    .controller('distibutorHuntCtrl', function($rootScope, $scope, $ionicLoading, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
        $scope.editable = 0;
        $scope.distibutorHunt = true;
        fmcgAPIservice.getDataList('POST', 'table/list', ["DCRDetail_Distributors_Hunting", '["*"]', , , , , , , ]).success(function(response) {
            $scope.distributorHunt = response;
        })
        $scope.$parent.navTitle = "Submitted Distributor Hunt";

        $scope.Edit = function(distributor) {
            $scope.$parent.navTitle = "Edit Distributor Hunt"
            $scope.data = distributor;
            $scope.editable = 1;
        }
        $scope.Mypln.worktype = {};
        $scope.Mypln.worktype.selected = {};
        $scope.Mypln.worktype.selected.FWFlg = 'DH';

        var Wtyps = $scope.worktypes.filter(function(a) {
            return (a.FWFlg == 'DH')
        });
        $scope.Mypln.worktype.selected.id = Wtyps[0]['id'];
        $scope.hunt = function() {
            $scope.data.worktype = $scope.Mypln.worktype.selected.id;

            fmcgAPIservice.addMAData('POST', 'dcr/save', "21", $scope.data).success(function(response) {
                if (response.success) {
                    Toast("Updated Successfully");
                    $state.go('fmcgmenu.home');

                }
            });
        }
    })
    .controller('manageDataViewCtrl', function($rootScope, $scope, $ionicLoading, $stateParams, $state, $ionicModal, $ionicPopup, $ionicScrollDelegate, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
        if ($scope.Myplns[0] != undefined) {
            if ($scope.Myplns[0]['dcrtype'] == "Route Wise")
                $scope.routeWise = 0;
            else
                $scope.routeWise = 1;
        }
        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.drafts = [];
        var tm = [];
        $scope.myChoice = $stateParams.myChoice;
        var ch = $scope.myChoice;
        $scope.$parent.navTitle = "Submitted " + ((ch == 1) ? $scope.EDrCap : (ch == 2) ? $scope.EChmCap : (ch == 2) ? $scope.EStkCap : (ch == 2) ? $scope.ENLCap : 'calls');
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


        fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_report", '["*"]', , , 1, , 1, , "[\"activity_date asc\"]"]).success(function(response) {
            var tm = [];
            var pklist = [];
            if (response.length == 0)
                $ionicLoading.hide();
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
        }).error(function() {
            $ionicLoading.hide();
            Toast('No Internet Connection.');
        });
        $scope.loadEdDet = function(pklist, tempData) {
            switch ($scope.myChoice) {
                case "1":
                    $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_msl_details", '["*,cast(time as datetime) tm"]', , JSON.stringify(pklist), , 1, , , '["cast(time as datetime)"]']).success(function(response1) {

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
                            tData.stockist = {};
                            tData.stockist.selected = {};
                            tData.stockist.selected.id = response1[j]['stockist_code'];
                            tData.stockist.name = response1[j]['stockist_name'];
                            var id = response1[j]['SDP'];
                            var towns = $scope.myTpTwns;
                            for (var key in towns) {
                                if (towns[key]['id'] == id)
                                    $scope.fmcgData.rootTarget = towns[key]['target'];
                            }
                            tData.netweightvaluetotal = response1[j]["net_weight_value"];
                            tData.OrdStk.selected.id = response1[j]['Stockist_Code'];
                            tData.remarks = response1[j]["Activity_Remarks"];
                            tData.rateType = response1[j]["rateMode"];
                            tData.discount_price = response1[j]["discount_price"];
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
                                    /*  var pTemp = {};
                                      var prod = prods[m].split('~');
                                      var prodNm = prodNms[m].split('~');
                                      pTemp.product = prod[0].toString().trim();
                                      pTemp.product_Nm = prodNm[0].toString();
                                      var prodQ = prod[1].split('$');
                                      pTemp.sample_qty = Number(prodQ[0]);
                                      pTemp.rx_qty = prodQ[1];*/



                                    var pTemp = {};
                                    var prod = prods[m].split('~');
                                    var prodNm = prodNms[m].split('~');
                                    pTemp.product = prod[0].toString().trim();
                                    pTemp.product_Nm = prodNm[0].toString();


                                    if (prod[1] != undefined) {
                                        if (prod[1].indexOf('$') > 0) {
                                            var prodQ = prod[1].split('$');
                                            pTemp.sample_qty = Number(prodQ[0]);
                                            pTemp.rx_qty = prodQ[1];
                                            pTemp.sample_qty = Number(prodQ[0]);
                                            var prodQ = prodQ[1].split('@');
                                            pTemp.rx_qty = prodQ[0];
                                            var prodQ = prodQ[1].split('+');
                                            pTemp.product_netwt = prodQ[0];
                                            var prodQ = prodQ[1].split('%');
                                            pTemp.free = prodQ[0];
                                            var prodQ = prodQ[1].split('-');
                                            pTemp.discount = prodQ[0];
                                            pTemp.discount_price = prodQ[1];
                                            pTemp.netweightvalue = pTemp.product_netwt * pTemp.rx_qty;
                                        } else {
                                            pTemp.rx_qty = Number(prod[1]);
                                        }
                                    }
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
                    }).error(function() {
                        $ionicLoading.hide();
                        Toast('No Internet Connection.');
                    });

                    break;
                case "3":
                    $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_CSH_Detail", '["*"]', , JSON.stringify(pklist), , 3, , , '["stk_meet_time"]']).success(function(response1) {
                        for (var j = 0, le = response1.length; j < le; j++) {
                            tData = {};
                            tData = angular.copy(tempData);

                            tData['amc'] = response1[j]['Trans_Detail_Slno'];
                            tData['pob'] = parseInt(response1[j]['POB_Value']);
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
                                    /*  var pTemp = {};
                                      var prod = prods[m].split('~');
                                      var prodNm = prodNms[m].split('~');
                                      pTemp.product = prod[0].toString().trim();
                                      pTemp.product_Nm = prodNm[0].toString();
                                      var prodQ = prod[1].split('$');
                                      pTemp.sample_qty = Number(prodQ[0]);
                                      pTemp.rx_qty = prodQ[1];*/



                                    var pTemp = {};
                                    var prod = prods[m].split('~');
                                    var prodNm = prodNms[m].split('~');
                                    pTemp.product = prod[0].toString().trim();
                                    pTemp.product_Nm = prodNm[0].toString();


                                    if (prod[1] != undefined) {
                                        if (prod[1].indexOf('$') > 0) {
                                            var prodQ = prod[1].split('$');
                                            pTemp.sample_qty = Number(prodQ[0]);
                                            pTemp.rx_qty = prodQ[1];
                                            pTemp.sample_qty = Number(prodQ[0]);
                                            var prodQ = prodQ[1].split('@');
                                            pTemp.rx_qty = prodQ[0];
                                            var prodQ = prodQ[1].split('+');
                                            pTemp.product_netwt = prodQ[0];
                                            var prodQ = prodQ[1].split('%');
                                            pTemp.free = prodQ[0];
                                            var prodQ = prodQ[1].split('-');
                                            pTemp.discount = prodQ[0];
                                            pTemp.discount_price = prodQ[1];
                                            pTemp.netweightvalue = pTemp.product_netwt * pTemp.rx_qty;
                                        } else {
                                            pTemp.rx_qty = Number(prod[1]);
                                        }
                                    }
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

                            if (response1[j]['instrument_type'] != "") {
                                tData['instrumenttype'] = {};
                                tData['instrumenttype']['selected'] = {};
                                tData['instrumenttype']['name'] = response1[j]['instrument_type'];
                                instrumenttypes = $scope.instrumenttypes;
                                inst = instrumenttypes.filter(function(a) {
                                    return (a.name == response1[j]['instrument_type']);
                                });
                                tData['instrumenttype']['selected']['id'] = inst[0]['id'];
                            }
                            tData['dateofinst'] = response1[j]['date_of_instrument'];
                            tm.push(tData);
                        }
                        $scope.drafts = tm;
                        $ionicLoading.hide();
                    }).error(function() {
                        $ionicLoading.hide();
                        Toast('No Internet Connection.');
                    });
                    break;
 case "8":
                    $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_SuperCSH_Detail", '["*"]', , JSON.stringify(pklist), , 8, , , '["stk_meet_time"]']).success(function(response1) {
                        for (var j = 0, le = response1.length; j < le; j++) {
                            tData = {};
                            tData = angular.copy(tempData);

                            tData['amc'] = response1[j]['Trans_Detail_Slno'];
                            tData['pob'] = parseInt(response1[j]['POB_Value']);
                            tData['customer'] = {};
                            tData['customer']['selected'] = {}
                            tData['customer']['selected']['id'] = "8";
                            tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                            tData['Superstockist'] = {};
                            tData['Superstockist']['selected'] = {}
                            tData['Superstockist']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                            tData['Superstockist']['name'] = response1[j]['Trans_Detail_Name'];
                            /*$scope.Name =  $scope.SupplierMster.filter(function(a) {
                                            return (a.id == response1[j]['Trans_Detail_Info_Code']);
                                        });

                            if(response1[j]['Trans_Detail_Name']==''){
                                response1[j]['Trans_Detail_Name']=$scope.Name[0].name;
                            }*/
                             tData['Superstockist']['name'] = response1[j]['Trans_Detail_Name'];
 


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
                                    /*  var pTemp = {};
                                      var prod = prods[m].split('~');
                                      var prodNm = prodNms[m].split('~');
                                      pTemp.product = prod[0].toString().trim();
                                      pTemp.product_Nm = prodNm[0].toString();
                                      var prodQ = prod[1].split('$');
                                      pTemp.sample_qty = Number(prodQ[0]);
                                      pTemp.rx_qty = prodQ[1];*/



                                    var pTemp = {};
                                    var prod = prods[m].split('~');
                                    var prodNm = prodNms[m].split('~');
                                    pTemp.product = prod[0].toString().trim();
                                    pTemp.product_Nm = prodNm[0].toString();


                                    if (prod[1] != undefined) {
                                        if (prod[1].indexOf('$') > 0) {
                                            var prodQ = prod[1].split('$');
                                            pTemp.sample_qty = Number(prodQ[0]);
                                            pTemp.rx_qty = prodQ[1];
                                            pTemp.sample_qty = Number(prodQ[0]);
                                            var prodQ = prodQ[1].split('@');
                                            pTemp.rx_qty = prodQ[0];
                                            var prodQ = prodQ[1].split('+');
                                            pTemp.product_netwt = prodQ[0];
                                            var prodQ = prodQ[1].split('%');
                                            pTemp.free = prodQ[0];
                                            var prodQ = prodQ[1].split('-');
                                            pTemp.discount = prodQ[0];
                                            pTemp.discount_price = prodQ[1];
                                            pTemp.netweightvalue = pTemp.product_netwt * pTemp.rx_qty;
                                        } else {
                                            pTemp.rx_qty = Number(prod[1]);
                                        }
                                    }
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

                            if (response1[j]['instrument_type'] != "") {
                                tData['instrumenttype'] = {};
                                tData['instrumenttype']['selected'] = {};
                                tData['instrumenttype']['name'] = response1[j]['instrument_type'];
                                instrumenttypes = $scope.instrumenttypes;
                                inst = instrumenttypes.filter(function(a) {
                                    return (a.name == response1[j]['instrument_type']);
                                });
                                tData['instrumenttype']['selected']['id'] = inst[0]['id'];
                            }
                            tData['dateofinst'] = response1[j]['date_of_instrument'];
                            tm.push(tData);
                        }
                        $scope.drafts = tm;
                        $ionicLoading.hide();
                    }).error(function() {
                        $ionicLoading.hide();
                        Toast('No Internet Connection.');
                    });
                    break;




                case "2":
                    $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_CSH_Detail", '["*"]', , JSON.stringify(pklist), , 2, , , '["chm_meet_time"]']).success(function(response1) {
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
                    }).error(function() {
                        $ionicLoading.hide();
                        Toast('No Internet Connection.');
                    });
                    break;
                case "4":
                    $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_Unlst_Detail", '["*,cast(time as datetime) tm"]', , JSON.stringify(pklist), , 1, , , '["cast(time as datetime)"]']).success(function(response1) {
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
                            tData.stockist = {};
                            tData.stockist.selected.id = response1[j]['stockist_code'];
                            tData.stockist.name = response1[j]['stockist_name'];
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
                    }).error(function() {
                        $ionicLoading.hide();
                        Toast('No Internet Connection.');
                    });
                    break;
            }
        }

        $scope.edit = function(item) {
            var TwnDet = fmcgLocalStorage.getData("town_master_" + item.subordinate.selected.id) || [];
            if (TwnDet.length > 0) {
                $scope.loadDatas(false, '_' + item.subordinate.selected.id);
            } else if (TwnDet.length <= 0) {
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
        $scope.editOrder = function(item) {
            fmcgAPIservice.getDataList('POST', 'get/vwOrderDetails&DCR_Code=' + item.amc, [])
                .success(function(response) {

                    $scope.fmcgData.EOrders = response;
                    $state.go('fmcgmenu.orderEdit');
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
            $scope.fmcgData.editableOrder = 1;
        }
        $scope.preView = function(item) {
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
            $scope.modal.sendWhatsApp = function() { /*******/
                xelem = $(event.target).closest('.scroll').find('#idOrdDetail');
                $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                $('ion-nav-view').css("height", $(xelem).height() + 300);
                $('body').css("overflow", 'visible');
                $ionicScrollDelegate.scrollTop();

                /*var doc = new jsPDF();
                doc.fromHTML($(xelem).html(), 15, 15, {
                    'width': 170
                });
                doc.save('sample-file.pdf');
                window.plugins.socialsharing.shareViaWhatsApp("Order Details", null, 'sample-file.pdf', function () { }, function (errormsg) { alert(errormsg) })
                */
                SendPDFtoSocialShare($(xelem), "Order Details", "Order Details", "w");
                /*
                html2canvas($(xelem), {
                    onrendered: function (canvas) {
                        var canvasImg = canvas.toDataURL("image/jpg");
                        getCanvas = canvas;
                        $('.modal').css("height","");
                        $('.ion-content').css("height", "");
                        $('.scroll').css("height", "");
                        $('ion-nav-view').css("height", "");
                        $('body').css("overflow", 'hidden');
                        window.plugins.socialsharing.shareViaWhatsApp("Order Details", canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                    }
                });
                */
            }
        };
        $scope.deleteDraft = function(item) {
            $ionicPopup.confirm({
                title: 'Call Delete',
                content: 'Are you sure you want to Delete?'
            }).then(function(res) {
                if (res) {
                    $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                    fmcgAPIservice.deleteEntry('POST', 'deleteEntry', item).success(function(response) {
                        Toast("call deleted successfully");
                    });
                } else {
                    console.log('You are not sure');
                }
            });
        };
    }).controller('dcrData', function($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.$parent.navTitle = "Monthly Summary";
        $scope.clearData();
        $scope.dcrDataCur = {};
        fmcgAPIservice.getDataList('POST', 'dcr/callReport', []).success(function(response) {
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
        }).error(function() {
            Toast('No Internet Connection.');
        });

    })

.controller('tpviewCtrl', function($scope, $ionicLoading, fmcgAPIservice) {
        $scope.$parent.navTitle = "Tour plan view";
        $scope.tpvwlists = {};
        $scope.mnthDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

        $scope.subhdTop = (($scope.view_MR == 1) ? '44' : '88') + 'px';
        $scope.CntvwTop = (($scope.view_MR == 1) ? '88' : '130') + 'px';
        $scope.$on('eGetTpEntry', function(evnt) {
            $scope.GetTpEntry();
        });

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        $scope.tpview.subordinate = {};
        $scope.tpview.subordinate.name = userData.sfName;
        $scope.tpview.subordinate.selected = {};
        $scope.tpview.subordinate.selected.id = userData.sfCode;

        $scope.GetTpEntry = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var params = {};
            $scope.error = "";
            params.sfCode = ($scope.desigCode == 'MR') ? $scope.sfCode : $scope.tpview.subordinate.selected.id;
            params.mnthYr = $scope.mnthDt;
            fmcgAPIservice.getPostData('POST', 'tpview', params).success(
                function(response) {
                    $scope.tpvwlists = JSON.parse(JSON.stringify(response));
                    if ($scope.tpvwlists.length <= 0) {
                        $scope.error = "TP Entry Not Yet Updated...";
                    };
                    $ionicLoading.hide();
                }).error(function() {
                $ionicLoading.hide();
            });
        }
        $scope.GetTpEntry();
    })
    .controller('tpviewdtCtrl', function($scope, $ionicLoading, fmcgAPIservice) {
        $scope.$parent.navTitle = "Datewise Tour plan view";
        $scope.tpDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
        $scope.dtTPvwlists = {};

        $scope.$on('GetTpEntryDt', function(evnt) {
            $scope.GetTpEntryDtws();
        });

        $scope.GetTpEntryDtws = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var params = {};
            $scope.error = "";
            var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
            params.tpDate = $scope.tpDate;
            params.sfCode = $scope.sfCode;
            fmcgAPIservice.getPostData('POST', 'tpviewdt', params)
                .success(function(response) {
                    $scope.dtTPvwlists = JSON.parse(JSON.stringify(response));
                    if ($scope.dtTPvwlists.length <= 0) {
                        $scope.error = "TP Entry Not Yet Updated...";
                    };
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                });
        }
        $scope.GetTpEntryDtws();
    })
    .controller('BrndSummary', function($rootScope, $ionicLoading, $ionicScrollDelegate, $ionicModal, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {

        $scope.$parent.navTitle = "Brandwise Summary";
        $scope.goTobrandsummary = function(cus) {
            if (cus == 1)
                $state.go('fmcgmenu.brandsale');
            if (cus == 2)
                $state.go('fmcgmenu.brandlitter');

        }
    })
    .controller('BrndSummaryValue', function($rootScope, $ionicLoading, $ionicScrollDelegate, $ionicModal, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {

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
        $scope.roundNumber = function(number, precision) {
            precision = Math.abs(parseInt(precision)) || 0;
            var multiplier = Math.pow(10, precision);
            return parseFloat(Math.round(number * multiplier) / multiplier).toFixed(2);
        }
        $scope.$on('getBrandSalSumry', function(evnt) {
            $scope.getBrandSalSumm();
        });
        $scope.goBack = function() {
            $state.go('fmcgmenu.BrndSummary');
        }
        var getCanvas;
        $scope.BrndSumm.stockist = {};
        $scope.BrndSumm.stockist.selected = {};
        $scope.GetDate = function(dt) {
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
        $scope.sendWhatsApp = function() {
            xelem = $(event.target).closest('.scroll').find('#preview');
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $(xelem).height() + 300);
            $('body').css("overflow", 'visible');
            SendPDFtoSocialShare($(xelem), 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), "w");
            /*
            html2canvas($(xelem), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    getCanvas = canvas;
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    window.plugins.socialsharing.shareViaWhatsApp('Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                }
            });
            */
        }
        $scope.sendEmail = function() {
            xelem = $(event.target).closest('.scroll').find('#preview');
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $(xelem).height() + 300);
            $('body').css("overflow", 'visible');
            SendPDFtoSocialShare($(xelem), 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), "e");
            /*
            html2canvas($(xelem), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    window.plugins.socialsharing.share('', 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), canvasImg, '');
                }
            });
            */
        }

        $scope.subhdTop = (($scope.view_MR == 1 || $scope.view_STOCKIST == 1) ? '44' : '88') + 'px';
        $scope.CntvwTop = (($scope.view_MR == 1 || $scope.view_STOCKIST == 1) ? '75' : '130') + 'px';
        if ($scope.SF_type == 2) {
            $scope.subhdTop = '132px';
            $scope.CntvwTop = '174px';
        }
        $scope.getTVal = function() {
            arr = $scope.BrndSalSumm || [];
            oVal = 0;
            for (il = 0; il < arr.length; il++) {
                oVal += arr[il].OVal;
            }
            return parseFloat(oVal).toFixed(2);
        }
        $scope.getCVal = function() {
            arr = $scope.BrndSalSumm || [];
            oVal = 0;
            for (il = 0; il < arr.length; il++) {
                oVal += arr[il].TOVal;
            }
            return parseFloat(oVal).toFixed(2);
        }

        $scope.getBrandSalSumm = function() {
            $ionicLoading.show({
                template: 'Loading...'
            }); /**********/
            var stockist = $scope.SF_type == 2 ? $scope.Mypln.stockist.selected.id : $scope.BrndSumm.stockist.selected.id;
            fmcgAPIservice.getDataList('POST', 'get/BrndSumm&rptSF=' + $scope.BrndSumm.subordinate.selected.id + '&rptDt=' + $scope.BrndDt + '&stockistCode=' + stockist, [])
                .success(function(response) {
                    $scope.BrndSalSumm = response;

                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
        }
        $scope.getBrandSalSumm();
    })
    .controller('BrndSummaryLitter', function($rootScope, $ionicLoading, $ionicScrollDelegate, $ionicModal, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {

        $scope.$parent.navTitle = "Brandwise Litters Summary";
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


        $scope.roundNumber = function(number, precision) {
            precision = Math.abs(parseInt(precision)) || 0;
            var multiplier = Math.pow(10, precision);
            return parseFloat(Math.round(number * multiplier) / multiplier).toFixed(2);
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.BrndSummary');
        }
        $scope.$on('getBrandSalSumry', function(evnt) {
            $scope.getBrandSalSumm();
        });
        var getCanvas;
        $scope.BrndSumm.stockist = {};
        $scope.BrndSumm.stockist.selected = {};
        $scope.GetDate = function(dt) {
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
        $scope.sendWhatsApp = function() {
            xelem = $(event.target).closest('.scroll').find('#preview');
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $(xelem).height() + 300);
            $('body').css("overflow", 'visible');

            SendPDFtoSocialShare($(xelem), 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), "w");
            /*
            html2canvas($(xelem), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    getCanvas = canvas;
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    window.plugins.socialsharing.shareViaWhatsApp('Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                }
            });
            */
        }
        $scope.sendEmail = function() {
            xelem = $(event.target).closest('.scroll').find('#preview');
            $('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
            $('ion-nav-view').css("height", $(xelem).height() + 300);
            $('body').css("overflow", 'visible');
            SendPDFtoSocialShare($(xelem), 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), "e");
            /*
            html2canvas($(xelem), {
                onrendered: function (canvas) {
                    var canvasImg = canvas.toDataURL("image/jpg");
                    $('ion-nav-view').css("height", "");
                    $('body').css("overflow", 'hidden');
                    window.plugins.socialsharing.share('', 'Summary For : ' + userData.sfName + ' Date : ' + $scope.GetDate($scope.BrndDt), canvasImg, '');
                }
            });
            */
        }

        $scope.subhdTop = (($scope.view_MR == 1 || $scope.view_STOCKIST == 1) ? '44' : '88') + 'px';
        $scope.CntvwTop = (($scope.view_MR == 1 || $scope.view_STOCKIST == 1) ? '75' : '130') + 'px';
        if ($scope.SF_type == 2) {
            $scope.subhdTop = '132px';
            $scope.CntvwTop = '174px';
        }
        $scope.getTVal = function() {
            arr = $scope.BrndSalSumm || [];
            oVal = 0;
            for (il = 0; il < arr.length; il++) {
                oVal += arr[il].OVal;
            }
            return parseFloat(oVal).toFixed(2);
        }
        $scope.getCVal = function() {
            arr = $scope.BrndSalSumm || [];
            oVal = 0;
            for (il = 0; il < arr.length; il++) {
                oVal += arr[il].TOVal;
            }
            return parseFloat(oVal).toFixed(2);
        }

        $scope.getBrandSalSumm = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var stockist = $scope.SF_type == 2 ? $scope.Mypln.stockist.selected.id : $scope.BrndSumm.stockist.selected.id;
            fmcgAPIservice.getDataList('POST', 'get/BrndSummLitters&rptSF=' + $scope.BrndSumm.subordinate.selected.id + '&rptDt=' + $scope.BrndDt + '&stockistCode=' + stockist, [])
                .success(function(response) {
                    $scope.BrndSalSumm = response;

                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
        }
        $scope.getBrandSalSumm();
    })
    .controller('mnthSummary', function($rootScope, $ionicLoading, $ionicScrollDelegate, $ionicModal, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.modal = $ionicModal;
        $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function(modal) {
            $scope.modal = modal;
        }, {
            animation: 'slide-in-up',
            focusFirstInput: true
        });

        $scope.ViewDetail = function(Acd, SlTyp, Adt, type) {
            $scope.modal.type = type;
            $scope.modal.SlTyp = SlTyp;
            $scope.modal.InvCnvrtNd = $scope.InvCnvrtNd;
            $scope.modal.OrdPrnNd = $scope.OrdPrnNd;
            $scope.modal.OfferMode = $scope.OfferMode;
            $scope.modal.PromoValND = $scope.PromoValND;
            $scope.modal.DrSmpQ = $scope.DrSmpQ;
            $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.EDrCap : (SlTyp == 2) ? $scope.EChmCap : (SlTyp == 3) ? $scope.EStkCap : (SlTyp == 4) ? $scope.ENLCap : '') + ' Visit Details For : ' + Adt;
            if ($scope.modal.type == 0)
                $scope.modal.rptTitle = ($scope.DrCap) + ' Visit Details For : ' + Adt;
            if ($scope.modal.type == 1)
                $scope.modal.rptTitle = ($scope.DrCap) + ' Order Details For : ' + Adt;
            $scope.modal.show();

            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.modal.vwVstlists = [];

            $scope.modal.vwProductSummry = [];
            fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                .success(function(response) {
               for (var j = 0, le = response.length; j < le; j++) {
    var prods = (response[j]['Product_Code'] + ((response[j]['Product_Code'] == '') ? '' : '#') + response[j]['Additional_Prod_Code']).split("#");
    var prodNms = (response[j]['Product_Detail'] + ((response[j]['Product_Detail'] == '') ? '' : '#') + response[j]['Additional_Prod_Dtls']).split("#");
    for (var m = 0, leg = prods.length; m < leg; m++) {
        if (prods[m].length > 0) {
            response[j]['productSelectedList'] = response[j]['productSelectedList'] || [];
            var pTemp = {};
            var prod = prods[m].split('~');
            var prodNm = prodNms[m].split('~');
            pTemp.product = prod[0].toString().trim();
            pTemp.product_Nm = prodNm[0].toString();
            if (prod[1] != undefined) {
                if (prod[1].indexOf('$') > 0) {
                    var prodQ = prod[1].split('$');
                    pTemp.rx_qty = prodQ[1];
                    pTemp.sample_qty = Number(prodQ[0]);
                    var prodQ = prodQ[1].split('@');
                    pTemp.rx_qty = Number(prodQ[0]);
                    if (prodQ[1].indexOf('%') > 0) {
                        var prodDFD = prodQ[1].split('%');
                        var prodDF = prodDFD[0].split('+');
                        pTemp.free = Number(prodDF[1]);
                        var prodDF = prodDFD[1].split('-');
                        pTemp.discount = Number(prodDF[0]);
                        var prodDF = prodDF[1].split('*');
                        pTemp.discAmt = Number(prodDF[0]);
                        var prodDF = prodDF[1].split('!');
                        pTemp.Rate = Number(prodDF[0]);
                        pTemp.OrderNo = prodDF[1].replace('.', '-');
                    } else
                        pTemp.product_netwt = prodQ[1];
                    pTemp.netweightvalue = pTemp.product_netwt * pTemp.rx_qty;
                } else {
                    pTemp.rx_qty = Number(prod[1]);
                }

                idex = -1;
                for (i = 0; i < $scope.modal.vwProductSummry.length; i++) {
                    if ($scope.modal.vwProductSummry[i].PCd == prod[0].toString().trim()) {
                        idex = i;
                    }
                }
                qt = parseFloat(pTemp.rx_qty)
                if (isNaN(qt)) qt = 0;
                fre = parseFloat(pTemp.free)
                if (isNaN(fre)) fre = 0;
                if (idex > -1) {
                    Prw = $scope.modal.vwProductSummry[idex];
                    Prw.Qty = Prw.Qty + qt;
                    Prw.FQty = Prw.FQty + fre;
                    $scope.modal.vwProductSummry[idex] = Prw;
                } else {
                    pSumm = {};
                    pSumm.PCd = prod[0].toString().trim();
                    pSumm.PName = prodNm[0].toString();
                    pSumm.Qty = qt;
                    pSumm.FQty = fre;
                    $scope.modal.vwProductSummry.push(pSumm);
                }
                if (pTemp.product.length !== 0)
                    response[j]['productSelectedList'].push(pTemp);
            }
        }
    }
}
            if ($scope.modal.type == 1) {
                for (var i = response.length - 1; i >= 0; i--) {
                    if (response[i]['orderValue'] == 0 || response[i]['orderValue'] == null)
                        response.splice(i, 1);
                }
            }
                            $scope.modal.vwVstlists = response;

                         $ionicLoading.hide();
                            }).error(function() {
                                $ionicLoading.hide();
                                Toast('No Internet Connection.');
                            });
            $ionicScrollDelegate.scrollTop();
            $scope.modal.sendWhatsApp = function() {
                xelem = $(event.target).closest('.scroll').find('#vstpreview');
                $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                $('ion-nav-view').css("height", $(xelem).height() + 300);
                $('body').css("overflow", 'visible');
                $ionicScrollDelegate.scrollTop();
                SendPDFtoSocialShare($(xelem), 'Summary Dashboard', 'Summary Dashboard', "w");
                /*
                html2canvas($(xelem), {
                    onrendered: function (canvas) {
                        var canvasImg = canvas.toDataURL("image/jpg");
                        getCanvas = canvas;
                        $('.modal').css("height", "");
                        $('.ion-content').css("height", "");
                        $('.scroll').css("height","");
                        $('ion-nav-view').css("height", "");
                        $('body').css("overflow", 'hidden');
                       // console.log(canvasImg);
                        window.plugins.socialsharing.shareViaWhatsApp('Summary Dashboard', canvasImg, null, function () { }, function (errormsg) { alert(errormsg) })
                    }
                });
                */
            }
            $scope.modal.sendEmail = function() {
                xelem = $(event.target).closest('.scroll').find('#vstpreview');
                $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                $('ion-nav-view').css("height", $(xelem).height() + 300);
                $('body').css("overflow", 'visible');
                $ionicScrollDelegate.scrollTop();
                SendPDFtoSocialShare($(xelem), 'Summary Dashboard', 'Summary Dashboard', "e");
                /*
                html2canvas($(xelem), {
                    onrendered: function (canvas) {
                        var canvasImg = canvas.toDataURL("image/jpg");
                        $('.modal').css("height", "");
                        $('.ion-content').css("height", "");
                        $('.scroll').css("height", "");
                        $('ion-nav-view').css("height", "");
                        $('body').css("overflow", 'hidden');
                        window.plugins.socialsharing.share('', 'Summary Dashboard', canvasImg, '');
                    }
                });
                */
            }
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

        $scope.$on('getMonthReport', function(evnt) {
            $scope.getMonthReports();
        });

        $scope.subhdTop = (($scope.view_MR == 1) ? '44' : '88') + 'px';
        $scope.CntvwTop = (($scope.view_MR == 1) ? '75' : '130') + 'px';
        $scope.getMonthReports = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'get/MnthSumm&rptSF=' + $scope.MnthSumm.subordinate.selected.id + '&rptDt=' + $scope.mnthDt, [])
                .success(function(response) {
                    $scope.MnthDCRList = response;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
        }
        $scope.getMonthReports();
    })





      .controller('DayCallReportctrl', function($rootScope, $ionicLoading, $ionicScrollDelegate, $ionicModal, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.modal = $ionicModal;
        $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function(modal) {
            $scope.modal = modal;
        }, {
            animation: 'slide-in-up',
            focusFirstInput: true
        });

        $scope.$parent.navTitle = "DayCallReport";
        $scope.clearData();
        $scope.mnthDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var tempp = window.localStorage.getItem("mypln");
        var userDatamypln = (tempp != null && tempp.length > 0) ? JSON.parse(tempp) : null;
        $scope.MnthSumm.subordinate = {};
        $scope.MnthSumm.subordinate.name = userData.sfName;
        $scope.MnthSumm.subordinate.selected = {};
        $scope.MnthSumm.subordinate.selected.id = userData.sfCode;

        $scope.$on('getMonthReport', function(evnt) {
            $scope.getMonthReports();
        });

        $scope.subhdTop = (($scope.view_MR == 1) ? '44' : '88') + 'px';
        $scope.CntvwTop = (($scope.view_MR == 1) ? '75' : '130') + 'px';
        $scope.getMonthReports = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'get/daycallreport&HQ=' + $scope.MnthSumm.subordinate.selected.id, [])
                .success(function(response) {
                    if(response.length>0){
                          $scope.MnthDCRList = response;
                      }else{
                        Toast("Calls is Empty");
                      }
                  
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
        }
        $scope.getMonthReports();
    })

    .controller('DailyInventory', function($rootScope, $ionicLoading, $ionicModal, $ionicScrollDelegate, $scope, $filter, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.$parent.navTitle = " " + (($scope.SF_type == 2) ? (($scope.DayInv.EMode == 1) ? 'Begin Inventory Approval' : 'End Inventory') : 'Begin Inventory');

        $scope.DayInv.subordinate = undefined;
        $scope.DayInv.stockist = undefined;
        $scope.ProductsList = fmcgLocalStorage.getData("product_master") || [];
        $scope.BInvFlg = fmcgLocalStorage.getData("BInvFlg") || 0;
        $scope.EInvFlg = fmcgLocalStorage.getData("EInvFlg") || 0;
        $scope.AInvFlg = fmcgLocalStorage.getData("AInvFlg") || 0;
        $scope.CategoryList = fmcgLocalStorage.getData("category_master") || [];

        $scope.ProductsList = $filter('orderBy')($scope.ProductsList, 'cateid', false);
        $scope.OrderFields = ['cateid', 'name'];
        $scope.DayInv.AprlFlag = 0;
        if ($scope.DayInv.EMode == 0 && $scope.SF_type == 1)
            $scope.DayInv.EMode = ($scope.BInvFlg > 0) ? 3 : 0;
        if ($scope.DayInv.EMode == 1 && $scope.SF_type == 2)
            $scope.DayInv.EMode = ($scope.AInvFlg > 0) ? 3 : 1;


        //if ($scope.DayInv.EMode == 0 && $scope.SF_type == 2)
        //    $scope.DayInv.EMode = ($scope.EInvFlg > 0) ? 3 : 0;

        $scope.ErrMsg = '';
        $scope.$on('getDailyBeginInvs', function(evnt) {
            $scope.getDailyBeginInv();
        });

        $scope.getDailyBeginInv = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.ErrMsg = '';
            md = $scope.DayInv.EMode;
            if ($scope.DayInv.EMode == 0 && $scope.SF_type == 1) md = 3;
            if ($scope.SF_type == 2) {
                $scope.DayInv.stockist = undefined;
                $scope.ProductsList = fmcgLocalStorage.getData("product_master") || [];
            }
            fmcgAPIservice.getDataList('POST', 'get/DYBInv&rptSF=' + $scope.DayInv.subordinate.selected.id + '&Mod=' + md, [])
                .success(function(response) {
                    if (response.length > 0) {
                        window.localStorage.setItem("BInvFlg", 1);
                        $scope.BInvFlg = 1;
                        if ($scope.DayInv.EMode == 0 && $scope.SF_type == 1) $scope.DayInv.EMode = 3;
                        if (response[0].Msg != '') {
                            $scope.ErrMsg = response[0].Msg;
                        } else {
                            le = response.length;
                            for (var j = 0; j < le; j++) {
                                $scope.selItems = $scope.ProductsList.filter(function(a) {
                                    return (a.id == response[j].Prod_Code);
                                });
                                item = $scope.selItems[0];
                                if (j == 0) {
                                    $scope.DayInv.stockist = {};
                                    $scope.DayInv.stockist.selected = {};
                                    $scope.DayInv.stockist.selected.id = response[j].StkId;
                                    $scope.DayInv.stockist.name = response[j].StkNm;
                                }
                                indx = $scope.ProductsList.indexOf(item);
                                if ($scope.DayInv.EMode == 0 && $scope.SF_type == 2) {
                                    item.UCQty = response[j].UCQty;
                                    item.UPQty = response[j].UPQty;

                                    // item.SCQty = response[j].SCQty;
                                    item.SPQty = response[j].SPQty;
                                }
                                item.Qty = response[j].CaseQty;
                                item.PQty = response[j].PiceQty;

                                $scope.ProductsList.splice(indx, 1, item);
                            }
                            console.log($scope.ProductsList);
                        }
                    } else {
                        window.localStorage.setItem("BInvFlg", 0);
                        $scope.BInvFlg = 0;
                        if ($scope.SF_type == 1) $scope.DayInv.EMode = 0;
                    }
                    $ionicLoading.hide();
                })
                .error(function() {
                    Toast("No Internet Connection! Try Again.");
                    $ionicLoading.hide();
                });

        }
        if ($scope.SF_type != 2) {
            $scope.DayInv.subordinate = {};
            $scope.DayInv.subordinate.selected = {}
            $scope.DayInv.subordinate.selected.id = $scope.sfCode;
            $scope.getDailyBeginInv();
        } // else if ($scope.DayInv.EMode == 0 && $scope.SF_type == 1) { $scope.getDailyBeginInv(); }

        $scope.saveAprl = function() {
            $scope.DayInv.AprlFlag = 1;
            $scope.save();
        }
        $scope.save = function() {
            $scope.data = {};
            var svMode = "24";
            $scope.data.AprlFlag = 0;
            if ($scope.SF_type == 2) {
                if ($scope.DayInv.subordinate == undefined) {
                    Toast('Select the Headquarters ...');
                    return false;
                }
                $scope.data.SF = $scope.DayInv.subordinate.selected.id;
                svMode = "26";
                if ($scope.DayInv.AprlFlag == 1) {
                    svMode = "27";
                }
                $scope.data.AprlFlag = $scope.DayInv.AprlFlag;
            } else
                $scope.data.SF = $scope.sfCode;

            if ($scope.SF_type == 1) {
                if ($scope.DayInv.stockist == undefined) {
                    Toast('Select the From Stock Distributor ...');
                    return false;
                }
            }
            if( $scope.DayInv.stockist==undefined){
            Toast("Today inventry stack not received from HQ");
        }
            $scope.data.StkId = $scope.DayInv.stockist.selected.id;
            $scope.data.StkNm = $scope.DayInv.stockist.name;



            $scope.data.InvProducts = [];
            for (il = 0; il < $scope.ProductsList.length; il++) {
                if (($scope.ProductsList[il].Qty != undefined && $scope.ProductsList[il].Qty > 0) || ($scope.ProductsList[il].PQty != undefined && $scope.ProductsList[il].PQty > 0)) {
                    $scope.data.InvProducts.push($scope.ProductsList[il]);
                }
            }
            if ($scope.data.InvProducts.length < 1) {
                Toast('Enter Qty of Any One Product ...');
                return false;
            }
            $scope.data.svMode = svMode;
            fmcgAPIservice.addMAData('POST', 'dcr/save', svMode, $scope.data)
                .success(function(response) {
                    if (response.success) {
        
           fmcgLocalStorage.createData("DailyBegin", $scope.data);
                    

                        sM = "Begin - Inventory Saved ";
                        if (svMode == "24") window.localStorage.setItem("BInvFlg", 1);
                        if (svMode == "26") {
                            sM = "End - Inventory Saved ";
                            window.localStorage.setItem("EInvFlg", 1);
                        }
                        if (svMode == "27") {
                            sM = "Begin - Inventory Approved";

                            window.localStorage.setItem("AInvFlg", 1);
                        }
                        Toast(sM + " Successfully");
                    }
                    $scope.data = {};
                    $scope.ProductsList = [];
                })
                .error(function() {
                    Toast("No Internet Connection! Try Again.");
                    $ionicLoading.hide();
                });
            $state.go('fmcgmenu.home');
        };
    })
    .controller('OrderReturn', function($rootScope, $ionicLoading, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.$parent.navTitle = "Stock Return Entry";

        $scope.SalRet.ProductsList = fmcgLocalStorage.getData("product_master") || [];
        $scope.CategoryList = fmcgLocalStorage.getData("category_master") || [];




            if($scope.view_MR==1){
            $scope.SalRet.subordinate = {};        
            $scope.SalRet.subordinate.selected = {};
            $scope.SalRet.subordinate.selected.id = $scope.Myplns[0].subordinateid;
             }

        /*$scope.StockTypes = [{
            'id': 'G',
            'name': 'Good'
        }, {
            'id': 'D',
            'name': 'Damage'
        }];*/

         $scope.StockTypes = fmcgLocalStorage.getData("Mas_ClaimType") || [];

        $scope.ClaimTypes = [{
            'id': 'N',
            'name': 'Non - Claimable'
        }, {
            'id': 'Y',
            'name': 'Claimable'
        }];
        $scope.UpdateGrid = function(item) {
            $scope.selItems = $scope.SalRet.ProductsList.filter(function(a) {
                return (a.id == item.id);
            });
            indx = $scope.SalRet.ProductsList.indexOf($scope.selItems[0]);
            $scope.SalRet.ProductsList.splice(indx, 1, item);
        }
        $scope.ClearReturn = function() {
            $scope.SalRet.ProductsList = fmcgLocalStorage.getData("product_master") || [];

        }
        $scope.ClearReturn();
        $("#vwSType").hide();
        $("#vwClaimTyp").hide();


        $scope.openClaimSelect = function(item) {
            $scope.cItem = item;
            $("#vwClaimTyp").show();
        }
        $scope.SetClaimType = function(sItem) {
            $scope.cItem.ClaimType = sItem.id;
            $scope.cItem.ClaimTypeNm = sItem.name;
            $("#vwClaimTyp").hide();
        }
        $scope.CloseDiv = function(x) {
            $("#vwSType").hide();
            $("#vwClaimTyp").hide();
        }
        $scope.openSelect = function(item) {
            $scope.cItem = item;
            $("#vwSType").show();
        }
        $scope.SetStockType = function(sItem) {
             $scope.cItem.S_name=sItem.S_name;
            $scope.cItem.QType = sItem.id;
            $scope.cItem.QTypeNm = sItem.name;
            $("#vwSType").hide();
        }
        $scope.save = function() {
            $scope.data = {};
            $scope.data.RetProducts = [];
            if ($scope.SalRet.doctor == undefined) {
                Toast('Select the' + $scope.DrCap + ' ...');
                return false;
            }
            $scope.data.DrId = $scope.SalRet.doctor.selected.id;
            if ($scope.SalRet.stockist == undefined) {
                Toast('Select the ' + $scope.StkCap + ' ...');
                return false;
            }
            $scope.data.StkId = $scope.SalRet.stockist.selected.id;
            for (il = 0; il < $scope.SalRet.ProductsList.length; il++) {
                if (($scope.SalRet.ProductsList[il].damageqty!=undefined && $scope.SalRet.ProductsList[il].damageqty>0 )||($scope.SalRet.ProductsList[il].Qty != undefined && $scope.SalRet.ProductsList[il].Qty > 0) || ($scope.SalRet.ProductsList[il].PQty != undefined && $scope.SalRet.ProductsList[il].PQty > 0)) {
                    console.log($scope.SalRet.ProductsList[il]);
                    if ($scope.SalRet.ProductsList[il].QType == undefined) {
                        Toast("Select the Stock Type");
                        return false;
                    }

                   
                    if ($scope.SalRet.ProductsList[il].ClaimType == undefined) $scope.SalRet.ProductsList[il].ClaimType = 'N';
                    if ($scope.SalRet.ProductsList[il].ClaimTypeNm == undefined) $scope.SalRet.ProductsList[il].ClaimTypeNm = 'Non - Claimable';

                    $scope.data.RetProducts.push($scope.SalRet.ProductsList[il]);
                }
            }
            if ($scope.data.RetProducts.length < 1) {
                Toast('Enter the Qty of Any One Product ...');
                return false;
            }

            fmcgAPIservice.addMAData('POST', 'dcr/save', "25", $scope.data)
                .success(function(response) {
                    if (response.success) 
                        Toast("Return Entry Saved Successfully");
                    $scope.data = {};
                    $scope.SalRet = {};
                    $scope.clearData();
                    $scope.ClearReturn();
                })
                .error(function() {
                    /*
                        var QuDataNew = fmcgLocalStorage.getData("CustAddQ") || [];
                        QuDataNew.push($scope.data);
                        fmcgLocalStorage.createData("CustAddQ", QuDataNew);
                    */
                    Toast("No Internet Connection! Try Again.");
                    $ionicLoading.hide();
                });
            $state.go('fmcgmenu.home');
        }
    })
    .controller('dayPlan', function($rootScope, $ionicLoading, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.modal = $ionicModal;
        $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function(modal) {
            $scope.modal = modal;
        }, {
            animation: 'slide-in-up',
            focusFirstInput: true
        });

        $scope.$parent.navTitle = "Day Plan";
        $scope.clearData();
        $scope.mnthDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        $scope.MnthSumm.subordinate = {};
        $scope.MnthSumm.subordinate.name = userData.sfName;
        $scope.MnthSumm.subordinate.selected = {};
        $scope.MnthSumm.subordinate.selected.id = userData.sfCode;

        $scope.$on('getMonthReport', function(evnt) {
            $scope.getMonthReports();
        });

        $scope.subhdTop = (($scope.view_MR == 1) ? '44' : '88') + 'px';
        $scope.CntvwTop = (($scope.view_MR == 1) ? '75' : '130') + 'px';

        $scope.getMonthReports = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'get/dayplan&rptSF=' + $scope.MnthSumm.subordinate.selected.id + '&rptDt=' + $scope.mnthDt, [])
                .success(function(response) {
                    $scope.MnthDCRList = response;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
        }
        $scope.getMonthReports();
    })
    .controller('attViewCtrl', function($rootScope, $ionicLoading, $filter, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.$parent.navTitle = "Attendance Report";
        $scope.clearData();
        $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
        $scope.getAttView = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.dayDCRList = [];
            $scope.OrderFields = ["Desig_Code", "SF_Name", "Time"]
            fmcgAPIservice.getDataList('POST', 'get/Attendance&rptDt=' + $scope.dyRptDt, [])
                .success(function(response) {
                    $scope.dayDCRList = response || [];
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });

        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.reports');
        };
        $scope.getAttView();
    })

.controller('dySalSumm', function($rootScope, $ionicLoading, $filter, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {

        $scope.$parent.navTitle = "Daily Sales Summary";
        $scope.clearData();
        $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

        $scope.getDailySales = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'get/DailySales&rptDt=' + $scope.dyRptDt, [])
                .success(function(response) {
                    $scope.dayDCRList = response;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });

        }


        $scope.getDailySales();
    })
    .controller('dayReport', function($rootScope, $ionicLoading, $filter, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.modal = $ionicModal;
        $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function(modal) {
            $scope.modal = modal;
        }, {
            animation: 'slide-in-up',
            focusFirstInput: true
        });
        $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
        if ($scope.Mypl.length > 0) {
            if ($scope.Mypl[0]['dcrtype'] == "Route Wise")
                $scope.routewise = 0;
        }
            /*lpath=window.localStorage.getItem("logo");
        //console.log("Logo Path : "+lpath);
        if(lpath!=null && lpath!=undefined){
            //document.getElementById("cLogo").src =lpath;
           
        }
*/
         
           
        $scope.goBack = function() {
            $state.go('fmcgmenu.reports');
        };
        $scope.ViewDetailInshop = function(Acd, SlTyp, Adt, type) {
            $scope.modal.show();
            $ionicLoading.show({
                template: 'Loading...'
            });

            
            $scope.modal.type = type;
            if (type == 6) {
                $scope.modal.vwVstlistsdoor = $scope.DoorToDoorResponse;
            }
            if (type == 7) {
                $scope.modal.vwVstlistsinshop = $scope.InshopCount;
            }
            if (type == 8) {
                $scope.modal.vwVstlistsproduct = $scope.ProductCount;
            }

            $ionicLoading.hide();

        }
        $scope.ViewDetail = function(Acd, SlTyp, Adt, type) {
            $scope.modal.type = type;
            $scope.modal.SlTyp = SlTyp;
            $scope.modal.InvCnvrtNd = $scope.InvCnvrtNd;
            $scope.modal.OrdPrnNd = $scope.OrdPrnNd;
            $scope.modal.OfferMode = $scope.OfferMode;
            $scope.modal.PromoValND = $scope.PromoValND;
            $scope.modal.DrSmpQ = $scope.DrSmpQ;
            $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.EDrCap : (SlTyp == 2) ? $scope.EChmCap : (SlTyp == 3) ? $scope.EStkCap : (SlTyp == 4) ? $scope.ENLCap : '') + ' Visit Details For : ' + Adt;
            if ($scope.modal.type == 0)
                $scope.modal.rptTitle = ($scope.DrCap) + ' Visit Details For : ' + Adt;
            if ($scope.modal.type == 1)
                $scope.modal.rptTitle = ($scope.DrCap) + ' Order Details For : ' + Adt;
            $scope.modal.show();
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.modal.vwVstlists = [];
            $scope.modal.vwProductSummry = [];
            fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                .success(function(response) {
                    for (var j = 0, le = response.length; j < le; j++) {
                        var prods = (response[j]['Product_Code'] + ((response[j]['Product_Code'] == '') ? '' : '#') + response[j]['Additional_Prod_Code']).split("#");
                        var prodNms = (response[j]['Product_Detail'] + ((response[j]['Product_Detail'] == '') ? '' : '#') + response[j]['Additional_Prod_Dtls']).split("#");
                       

                      

                        for (var m = 0, leg = prods.length; m < leg; m++) {
                            if (prods[m].length > 0) {
                                response[j]['productSelectedList'] = response[j]['productSelectedList'] || [];
                                var pTemp = {};
                                var prod = prods[m].split('~');
              

                                var prodNm = prodNms[m].split('~');
                                pTemp.product = prod[0].toString().trim();
                                pTemp.product_Nm = prodNm[0].toString();
                              if(prods[m]!=undefined){
                              
                                  PQty=prods[m].split('%');

                                  if(PQty!=undefined){
                                      pTemp.PQty=PQty[2];
                                  }

                                }
                                if (prod[1] != undefined) {

                            

                                    if (prod[1].indexOf('$') > 0) {
                                        var prodQ = prod[1].split('$');
                                        pTemp.rx_qty = prodQ[1];
                                        pTemp.sample_qty = Number(prodQ[0]);
                                        var prodQ = prodQ[1].split('@');
                                        pTemp.rx_qty = Number(prodQ[0]);
                                        if (prodQ[1].indexOf('%') > 0) {
                                            var prodDFD = prodQ[1].split('%');
                                            var prodDF = prodDFD[0].split('+');
                                            pTemp.free = Number(prodDF[1]);
                                            var prodDF = prodDFD[1].split('-');
                                            pTemp.discount = Number(prodDF[0]);
                                            var prodDF = prodDF[1].split('*');
                                            pTemp.discAmt = Number(prodDF[0]);
                                            var prodDF = prodDF[1].split('!');
                                            pTemp.Rate = Number(prodDF[0]);
                                          
                                            pTemp.OrderNo = prodDF[1].replace('.', '-');
                                        } else
                                            pTemp.product_netwt = prodQ[1];
                                        pTemp.netweightvalue = pTemp.product_netwt * pTemp.rx_qty;
                                    } else {
                                        pTemp.rx_qty = Number(prod[1]);
                                    }

                                    idex = -1;
                                    for (i = 0; i < $scope.modal.vwProductSummry.length; i++) {
                                        if ($scope.modal.vwProductSummry[i].PCd == prod[0].toString().trim()) {
                                            idex = i;
                                        }
                                    }
                                    qt = parseFloat(pTemp.rx_qty)
                                    if (isNaN(qt)) qt = 0;
                                    fre = parseFloat(pTemp.free)
                                    if (isNaN(fre)) fre = 0;
                                    if (idex > -1) {
                                        Prw = $scope.modal.vwProductSummry[idex];
                                        Prw.Qty = Prw.Qty + qt;
                                        Prw.FQty = Prw.FQty + fre;
                                        $scope.modal.vwProductSummry[idex] = Prw;
                                    } else {
                                        pSumm = {};
                                        pSumm.PCd = prod[0].toString().trim();
                                        pSumm.PName = prodNm[0].toString();
                                        pSumm.Qty = qt;
                                        pSumm.FQty = fre;
                                        $scope.modal.vwProductSummry.push(pSumm);
                                    }
                                    if (pTemp.product.length !== 0)
                                        response[j]['productSelectedList'].push(pTemp);
                                }
                            }
                        }
                    }
                    if ($scope.modal.type == 1) {
                        for (var i = response.length - 1; i >= 0; i--) {
                            if (response[i]['orderValue'] == 0 || response[i]['orderValue'] == null)
                                response.splice(i, 1);
                        }
                    }
                    $scope.modal.vwVstlists = response;

                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
            $ionicScrollDelegate.scrollTop();
            $scope.modal.PrintInvoice = function(selitem) {
                $scope.$parent.InvData = angular.copy(selitem);
                $state.go('fmcgmenu.invEntry');
                $scope.modal.hide();

            }
            $scope.modal.sendWhatsApp = function() {
                xelem = $(event.target).closest('.scroll').find('#vstpreview');
                $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                $('ion-nav-view').css("height", $(xelem).height() + 300);
                $('body').css("overflow", 'visible');
                $ionicScrollDelegate.scrollTop();
                SendPDFtoSocialShare($(xelem), "Day Report", "Day Report", "w");
            }
            $scope.modal.sendEmail = function() {
                xelem = $(event.target).closest('.scroll').find('#vstpreview');
                $(event.target).closest('.modal').css("height", $(xelem).height() + 300);
                $(event.target).closest('.ion-content').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css("height", $(xelem).height() + 300);
                $(event.target).closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
                $('ion-nav-view').css("height", $(xelem).height() + 300);
                $('body').css("overflow", 'visible');
                $ionicScrollDelegate.scrollTop();
                SendPDFtoSocialShare($(xelem), "Day Report", "Day Report", "e");
            }
        };
        $scope.$parent.navTitle = "Day Report";
        $scope.clearData();
        $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
        $scope.Edit = function() {
            $state.go('fmcgmenu.EditSummary');
        }
        $scope.getDayReports = function() {
            currentDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
            if ($scope.dyRptDt == currentDate)
                $scope.summaryEdit = 0;
            else
                $scope.summaryEdit = 1;
            $ionicLoading.show({
                template: 'Loading...'
            });
            fmcgAPIservice.getDataList('POST', 'get/DayReport&rptDt=' + $scope.dyRptDt, [])
                .success(function(response) {
                    $scope.dayDCRList = response['dayrep'];
                    $scope.InshopCount = response['Inshop'];
                    $scope.ProductCount = response['ProductDiisplay'];
                    $scope.DoorToDoorResponse = response['DoorToDoor'];
                    $scope.SS=response['SS'];

                    var groupBy = function(xs, key) {
                        return xs.reduce(function(rv, x) {
                            (rv[x[key]] = rv[x[key]] || []).push(x);
                            return rv;
                        }, {});
                    };
                    brndwise = response['brndwise'];
                    $scope.brndwise = groupBy(brndwise, "sf_code");
                    DCR_TLSD = response['DCR_TLSD'];
                    DCR_TLSD = groupBy(DCR_TLSD, "sf_code");
                    for (var key in DCR_TLSD) {
                        total = DCR_TLSD[key][0]['total_lines'];
                        DCR_TLSD[key] = [];
                        DCR_TLSD[key]['total'] = total;
                    }
                    $scope.DCR_TLSD = DCR_TLSD;

                    DCR_LPC = response['DCR_LPC'];
                    DCR_LPC = groupBy(DCR_LPC, "sf_code");
                    for (var key in DCR_LPC) {
                        total = DCR_LPC[key].length;
                        DCR_LPC[key] = [];
                        DCR_LPC[key]['total'] = total;
                    }
                    $scope.DCR_LPC = DCR_LPC;
                    summary = response['summary'];
                    for (i = 0; i < summary.length; i++) {
                        for (var key in DCR_LPC) {
                            if (key == summary[i]['sf_code']) {
                                $scope.DCR_LPC[key]['total'] = summary[i]['dcr_lpc'];
                                $scope.DCR_TLSD[key]['total'] = summary[i]['dcr_tlsd'];
                                brandnames = summary[i]['brand_ec'];
                                brandids = summary[i]['brand_id'];
                                var names = brandnames.split("#");
                                var ids = brandids.split("#");
                                brandwise = [];
                                for (var m = 0, leg = names.length; m < leg; m++) {
                                    if (names[m].length > 0) {
                                        brandwise = brandwise || [];
                                        var pTemp = {};
                                        var id = ids[m].split('~');
                                        var name = names[m].split('~');
                                        pTemp.product_brd_code = id[0].toString().trim();
                                        pTemp.product_brd_sname = name[0].toString();
                                        pTemp.RetailCount = name[1];
                                        if (name[1] != 0)
                                            brandwise.push(pTemp);
                                    }
                                }
                                $scope.brndwise[key] = brandwise;
                            }
                        }
                    }
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });

        }


        $scope.getDayReports();
    })



        .controller('PragnancyTestCtrl', function($rootScope, $ionicLoading, $filter, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.$parent.navTitle = "Pregnancy Status";

        $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
        if ($scope.Mypl.length > 0) {
            if ($scope.Mypl[0]['dcrtype'] == "Route Wise")
                $scope.routewise = 0;
        }
        $scope.goBack = function() {
            $state.go('fmcgmenu.home');
        };
        $scope.todaycount = 0;
        $scope.Flag = 0;
        $scope.pendingcount = 0;
        $scope.Update = function(x, index) {


            if (x.pragnancyconfirm == undefined || x.pragnancyconfirm == '') {
                Toast("Enter the Confirmations")
                return false;
            }

            fmcgAPIservice.getDataList('POST', 'get/UpdateDayPragnancyReport&OrderID=' + x.OrderID + '&Confirm=' + x.pragnancyconfirm, [])
                .success(function(response) {
                    if (response.TodayPragnancy != null) {
                        Toast('Successfully Updated')
                        $scope.fmcgData.modal.vwVstlists.splice(index, 1);
                        if ($scope.Flag == 0) {
                            $scope.getDayReports();
                        } else {
                            $scope.Pending();
                        }
                        //$scope.ViewDetail($scope.dayDCRList.Trans_SlNo, 1, $scope.dyRptDt, 0);
                    }

                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });

        };

        $scope.Today = function() {
            $scope.getDayReports();
        };

        $scope.Pending = function() {
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.fmcgData.modal.vwVstlists = [];
            $scope.Flag = 1;
            fmcgAPIservice.getDataList('POST', 'get/vwVstPendingPragnancy', [])
                .success(function(response) {

                    $scope.pendingcount = response.length;
                    $scope.fmcgData.modal.vwVstlists = response;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
        };

        $scope.fmcgData.modal = {};
        $scope.ViewDetail = function(Acd, SlTyp, Adt, type) {
            $scope.fmcgData.modal.type = type;
            $scope.fmcgData.modal.SlTyp = SlTyp;
            $scope.fmcgData.modal.InvCnvrtNd = $scope.InvCnvrtNd;
            $scope.fmcgData.modal.OrdPrnNd = $scope.OrdPrnNd;
            $scope.fmcgData.modal.OfferMode = $scope.OfferMode;
            $scope.fmcgData.modal.PromoValND = $scope.PromoValND;
            $scope.fmcgData.modal.DrSmpQ = $scope.DrSmpQ;
            $scope.fmcgData.modal.rptTitle = ((SlTyp == 1) ? $scope.EDrCap : (SlTyp == 2) ? $scope.EChmCap : (SlTyp == 3) ? $scope.EStkCap : (SlTyp == 4) ? $scope.ENLCap : '') + ' Visit Details For : ' + Adt;
            if ($scope.fmcgData.modal.type == 0)
                $scope.fmcgData.modal.rptTitle = ($scope.DrCap) + ' Visit Details For : ' + Adt;
            if ($scope.fmcgData.modal.type == 1)
                $scope.fmcgData.modal.rptTitle = ($scope.DrCap) + ' Order Details For : ' + Adt;
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.fmcgData.modal.vwVstlists = [];
            $scope.fmcgData.modal.vwProductSummry = [];
            fmcgAPIservice.getDataList('POST', 'get/vwVstPragnancyDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                  //fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + 6, [])
        
                .success(function(response) {

                    $scope.todaycount = response.length;
                    $scope.fmcgData.modal.vwVstlists = response;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
            $ionicScrollDelegate.scrollTop();


        };
        $scope.clearData();
        $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
        $scope.Edit = function() {
            $state.go('fmcgmenu.EditSummary');
        }
        $scope.getDayReports = function() {
            $scope.Flag = 0;
            currentDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
            if ($scope.dyRptDt == currentDate)
                $scope.summaryEdit = 0;
            else
                $scope.summaryEdit = 1;
            $ionicLoading.show({
                template: 'Loading...'
            });

            $scope.fmcgData.modal.vwVstlists = [];
            fmcgAPIservice.getDataList('POST', 'get/DayPragnancyReport&rptDt=' + $scope.dyRptDt, [])
                .success(function(response) {

                    $scope.dayDCRList = response['TodayPragnancy'];
                    if ($scope.dayDCRList != null && $scope.dayDCRList != undefined)
                        $scope.ViewDetail($scope.dayDCRList.Trans_SlNo, 1, $scope.dyRptDt, 0);
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });

        }

        $scope.Pending();
        $scope.getDayReports();
    })
.controller('DeliveryStatusCtrl', function($rootScope, $ionicLoading, $filter, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.$parent.navTitle = "DeliveryStatus";
    $scope.modal = $ionicModal;
    $scope.Mysfcode={};

    $scope.modal.fromTemplateUrl('partials/ViewDailyStatus.html', function(modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });
    $scope.Mypln.worktype = {};
    $scope.Mypln.worktype.selected = {};
    $scope.worktypess = fmcgLocalStorage.getData("mas_worktype") || [];
    $scope.$parent.Myplns = fmcgLocalStorage.getData("mypln") || [];
    $scope.Mypln.worktype.selected.FWFlg = $scope.$parent.Myplns[0].FWFlg;
    $scope.Mypln.subordinate = {};
    $scope.Mypln.subordinate.selected = {};
    $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
    if ($scope.Mypl.length > 0) {
        if ($scope.Mypl[0]['dcrtype'] == "Route Wise")
            $scope.routewise = 0;
    }
    $scope.goBack = function() {
        $state.go('fmcgmenu.home');
    };
    if ($scope.view_MR == 1) {
        $scope.Mysfcode=$scope.sfCode;
        getDayReports($scope.sfCode,1);
    }



    $scope.$on('DeliveryStatus', function(evnt, DrCd) {
        $scope.Mysfcode=DrCd.DrId;
        getDayReports(DrCd.DrId,1);
    });
    $scope.fmcgData.modal = {};

    $scope.approve = function(delivery) {
        delivery.rejection = 1;
        delivery.reason=delivery.Remarks;
        fmcgAPIservice.getDataList('POST', 'get/DSApproveStatus&CSFcode=' + delivery.Sf_Code + '&SLNo=' + delivery.TSL + '&Type=' + delivery.ordertype+'&Reason='+delivery.reason+'&Flag='+delivery.rejection, [])
            .success(function(response) {
                $ionicLoading.hide();
                getDayReports(delivery.Sf_Code);
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
    }

    $scope.reject = function(delivery) {

        delivery.rejection = 2;
        if (delivery.reason == undefined || delivery.reason == '') {
            Toast('Enter The Reason');
            return false;
        }
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'get/DSApproveStatus&CSFcode=' + delivery.Sf_Code + '&SLNo=' + delivery.TSL + '&Type=' + delivery.ordertype+'&Reason='+delivery.reason+'&Flag='+delivery.rejection, [])
            .success(function(response) {
                $ionicLoading.hide();
                getDayReports(delivery.Sf_Code);
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
    }


  $scope.Secondary = function(sf_code,type){


        getDayReports(sf_code,type);
  }


  $scope.Primary = function(sf_code,type){


        getDayReports(sf_code,type);
  }



        if ($scope.view_MR == 1) {

        $scope.ViewDetail = function(Acd, tv, dtb, type) {

            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.modal.show();
            $scope.fmcgData.modal.vwVstlists = [];
            $scope.fmcgData.modal.vwProductSummry = [];
            fmcgAPIservice.getDataList('POST', 'get/vwDailyStatus&ACd=' + Acd + "&Type=" + type, [])
                .success(function(response) {
                    $scope.modal.vwVstlists = response;
                    $scope.modal.tv = tv;
                    $scope.modal.dtb = dtb;
                    $scope.modal.Type = type;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
            $ionicScrollDelegate.scrollTop();


        };
    }
    $scope.clearData();
    $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

    function getDayReports(SF_Code,type) {
        currentDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
        if ($scope.dyRptDt == currentDate)
            $scope.summaryEdit = 0;
        else
            $scope.summaryEdit = 1;
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'get/DeliveryStatus&CSFcode=' + SF_Code, [])
            .success(function(response) {

                if (response != null && response.length > 0) {
                    for (var i = 0; i < response.length; i++) {

                        var stk = $scope.stockists.filter(function(a) {
                            return (a.id == response[i].Stockist_Code);
                        });

                        if (response[i].Route != 0) {
                            var route = $scope.myTpTwns.filter(function(a) {
                                return (a.id == response[i].Route);
                            });
                            var doctor = $scope.doctors.filter(function(a) {
                                return (a.id == response[i].Cust_Code);
                            });

                            response[i].retailername = doctor[0].name;
                            response[i].routename = route[0].name;
                        }

                        if (response[i].Order_Flag == null)
                            response[i].Order_Flag = 0;

                        if (stk.length > 0) {

                            response[i].distibutorname = stk[0].name;
                        }
                    }
            response=response.filter(function(a)
                {
                    return  (a.ordertype==type);
                });


                    $scope.DLS = response;
                }


                $ionicLoading.hide();
            }).error(function() {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });

    }


})


    .controller('distDayReport', function($rootScope, $ionicLoading, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
        $scope.modal = $ionicModal;
        $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function(modal) {
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
        $scope.ViewDetail = function(Acd, SlTyp, Adt) {
            $scope.modal.SlTyp = SlTyp;
            $scope.modal.InvCnvrtNd = $scope.InvCnvrtNd;
            $scope.modal.OrdPrnNd = $scope.OrdPrnNd;
            $scope.modal.OfferMode = $scope.OfferMode;
            $scope.modal.PromoValND = $scope.PromoValND;
            $scope.modal.DrSmpQ = $scope.DrSmpQ;
            $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.EDrCap : (SlTyp == 2) ? $scope.EChmCap : (SlTyp == 3) ? $scope.EStkCap : (SlTyp == 4) ? $scope.ENLCap : '') + ' Visit Details For : ' + Adt;
            $scope.modal.show();

            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.modal.vwVstlists = [];
            fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
                .success(function(response) {
                    $scope.modal.vwVstlists = response;
                    $ionicLoading.hide();
                }).error(function() {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
            $ionicScrollDelegate.scrollTop();
        };
        $scope.$parent.navTitle = "Day Report";
        $scope.clearData();
        $scope.$on('distReport', function() {
            $scope.getDayReports();
        });
        $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
        $scope.getDayReports = function() {
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
                    .success(function(response) {
                        if (response != 'null') {
                            for (var key in response) {
                                var perDecrease = ((response[key]['orderVal'] / (response[key]['routeTarget'])) * 100);
                                response[key]['perAchieved'] = perDecrease + "%";
                            }
                            $scope.dayDCRList = response;
                        }
                        $ionicLoading.hide();
                    }).error(function() {
                        $ionicLoading.hide();
                        Toast('No Internet Connection.');
                    });
            }
        }
        $scope.getDayReports();




    })
    .controller('dcrData1', function($rootScope, $ionicLoading, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
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
            'name': $scope.EDrCap,
            'url': 'manageDoctorResult'
        }];
        var al = 1;
        if ($scope.ChmNeed != 1) {
            $scope.customers.push({
                'id': '2',
                'name': $scope.EChmCap,
                'url': 'manageChemistResult'
            });
            $scope.cCI = al;
            al++;
        }
        if ($scope.StkNeed != 0) {
            $scope.customers.push({
                'id': '3',
                'name': $scope.EStkCap,
                'url': 'manageStockistResult'
            });
            $scope.sCI = al;
            al++;
        }
        if ($scope.UNLNeed != 1) {
            $scope.customers.push({
                'id': '4',
                'name': $scope.ENLCap,
                'url': 'manageStockistResult'
            });
            $scope.nCI = al;
            al++;
        }
        fmcgAPIservice.getDataList('POST', 'entry/count', []).success(function(response) {
            if (response['success']) {
                $scope.success = true;
                $scope.customers[0].count = response['data'][0]['doctor_count'];
                if ($scope.ChmNeed != 1)
                    $scope.customers[$scope.cCI].count = response['data'][1]['chemist_count'];
                if ($scope.StkNeed != 0)
                    $scope.customers[$scope.sCI].count = response['data'][2]['stockist_count'];
                if ($scope.UNLNeed != 1)
                    $scope.customers[$scope.nCI].count = response['data'][3]['uldoctor_count'];
                $scope.owTypeData.daywise_remarks = response['data'][4]['remarks'];
                $scope.owTypeData.HlfDayWrk = response['data'][5]['halfdaywrk'];
            } else {
                $scope.owsuccess = true;
                $scope.owTypeData = response['data'][0];
            }
            $ionicLoading.hide();
        }).error(function() {
            $ionicLoading.hide();
            Toast('No Internet Connection.');
        });
    })
  .controller('geoTagCtrl', function($rootScope, $scope, fmcgLocalStorage, fmcgAPIservice, $compile, $ionicModal) {
        
$scope.$parent.navTitle = "Geo Tagging";
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
          









        $scope.disti=0;
        $scope.geoTag.cluster = {};
        $scope.GeoTagDr = {};
        $scope.geoCustomers = [];
        $scope.unGeoCustomers = [];
        $scope.geoDistributor=[];
        $scope.unGeoDistributor=[];

        $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
        if($scope.Myplns.length>0 &&  $scope.view_MR == 1) {

        $scope.geoTag['cluster'] = {};
        $scope.geoTag['cluster'].selected = {};
        $scope.geoTag.cluster.selected.id=$scope.Myplns[0].clusterid;
        $scope.geoTag['stockist_code'] = {};    
        $scope.geoTag['stockist_code'].selected = {};
        $scope.geoTag['stockist_code'].selected.id = $scope.Myplns[0].stockistid;
        $scope.$broadcast('setGeodistData');
        $scope.$broadcast('setGeoData');

          var data = fmcgLocalStorage.getData('doctor_master_' + $scope.geoTag.subordinate.selected.id) || [];

            $scope.geoCustomers = data.filter(function(a) {
                return (a.lat != '' && a.town_code == $scope.geoTag.cluster.selected.id);
            });
            $scope.unGeoCustomers = data.filter(function(a) {
                return (a.lat == '' && a.town_code == $scope.geoTag.cluster.selected.id);
            });
        }
       
        
        $scope.$on('setGeoData', function(e) {
            var data = fmcgLocalStorage.getData('doctor_master_' + $scope.geoTag.subordinate.selected.id) || [];

            $scope.geoCustomers = data.filter(function(a) {
                return (a.lat != '' && a.town_code == $scope.geoTag.cluster.selected.id);
            });
            $scope.unGeoCustomers = data.filter(function(a) {
                return (a.lat == '' && a.town_code == $scope.geoTag.cluster.selected.id);
            });
        });


        $scope.TagDistributor=function(){

        $scope.disti=1;
         $scope.$broadcast('setGeodistData');

        }

         $scope.$on('setGeodistData', function(e) {
                    var data = fmcgLocalStorage.getData('stockist_master_' + $scope.geoTag.subordinate.selected.id) || [];

        
                    $scope.geoDistributor = data.filter(function(a) {
                        return (a.lat != '' );


                    });
                    $scope.unGeoDistributor = data.filter(function(a) {
                        return (a.lat == '' );

                    });
                });

        $scope.TagRetailer=function(){
            $scope.disti=0;
             $scope.$broadcast('setGeoData');
        }
        var map;
        var myLatlng;
        setPosFlag = false;
        var marker;

        $scope.mapLocation = function(item, m_Flag,retailerordisti) {
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
                mymap.addListener('bounds_changed', function() {
                    searchBox.setBounds(mymap.getBounds());
                    setTimeout(function() {
                        plcs = $('.pac-container');
                        $('.pac-container').remove();
                        plcs.appendTo(".modal-backdrop .active");
                    }, 500);
                });

                $scope.map = mymap;

                searchBox.addListener('place_changed', function() {
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
                            (place.address_components[0] && place.address_components[0].short_name || ''), (place.address_components[1] && place.address_components[1].short_name || ''), (place.address_components[2] && place.address_components[2].short_name || '')
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
                    function(responses) {
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
                    if(retailerordisti!=undefined){
                        var infowindow = new google.maps.InfoWindow({
                                    content: "<br><b>" + $scope.StkCap + " :</b> " + $scope.GeoTagDr.name + "<br><b>Address :</b> <span id='plcAddr'></span><br><div style='display:" + ((m_Flag == 0) ? 'block' : 'none') + ";text-align:center;padding-top:15px;'><button class='button buttonClear  button-balanced pull-right' onclick='SvLocation()'>Tag This Place</button></div>",
                                });
                    }else{
                        
                        var infowindow = new google.maps.InfoWindow({
                                    content: "<br><b>" + $scope.DrCap + " :</b> " + $scope.GeoTagDr.name + "<br><b>Address :</b> <span id='plcAddr'></span><br><div style='display:" + ((m_Flag == 0) ? 'block' : 'none') + ";text-align:center;padding-top:15px;'><button class='button buttonClear  button-balanced pull-right' onclick='SvLocation()'>Tag This Place</button></div>",
                                }); 
                    }

           
            if (m_Flag == 0) {
                google.maps.event.addListener(marker, 'dragend', function() {
                    geocodePosition(marker.getPosition());
                });

                google.maps.event.addListener(marker, 'click', function() {
                    infowindow.open($scope.map, marker);
                    geocodePosition(marker.getPosition());
                });
            }


            marker.setAnimation(google.maps.Animation.BOUNCE);

            if (myLatlng != null) {
                infowindow.open($scope.map, marker);
                geocodePosition(myLatlng);
            }
            infowindow.addListener('domready', function() {
                SvLocation = function() {

            if(retailerordisti!=undefined){
                    fmcgAPIservice.addMAData('POST', 'dcr/save', "47", $scope.GeoTagDr).success(function(response) {
                        if (response.success)
                            Toast("GEO Tagged Successfully");
                    }).error(function() {
                        $scope.OutGEOTag = fmcgLocalStorage.getData("OutBxDisti_GEOTag") || [];
                        $scope.OutGEOTag.push($scope.GeoTagDr);
                        localStorage.removeItem("OutBxDisti_GEOTag");
                        fmcgLocalStorage.createData("OutBxDisti_GEOTag", $scope.OutGEOTag);
                        Toast("No Internet Connection! GEO Tagged in Outbox");

                    });
                    var data = fmcgLocalStorage.getData('stockist_master_' + $scope.geoTag.subordinate.selected.id) || [];
                    var drRw = data.filter(function(a) {
                        return (a.id == $scope.GeoTagDr.id);
                    });
                    var $indx = data.indexOf(drRw[0]);
                    data[$indx].lat = $scope.GeoTagDr.lat;
                    data[$indx].long = $scope.GeoTagDr.long;
                    data[$indx].addrs = $scope.GeoTagDr.addrs;
                    fmcgLocalStorage.createData('stockist_master_' + $scope.geoTag.subordinate.selected.id, data);
                    $scope.$broadcast('setGeodistData');

                    $scope.modalMap.hide();

                }else{
                        fmcgAPIservice.addMAData('POST', 'dcr/save', "6", $scope.GeoTagDr).success(function(response) {
                        if (response.success)
                            Toast("GEO Tagged Successfully");
                    }).error(function() {
                        $scope.OutGEOTag = fmcgLocalStorage.getData("OutBx_GEOTag") || [];
                        $scope.OutGEOTag.push($scope.GeoTagDr);
                        localStorage.removeItem("OutBx_GEOTag");
                        fmcgLocalStorage.createData("OutBx_GEOTag", $scope.OutGEOTag);
                        Toast("No Internet Connection! GEO Tagged in Outbox");

                    });
                    var data = fmcgLocalStorage.getData('doctor_master_' + $scope.geoTag.subordinate.selected.id) || [];
                    var drRw = data.filter(function(a) {
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



                }
            });
            marker.addListener('click', function() {

            });
        }

    })
        .controller('locationdfindctrl', function($rootScope, $scope, fmcgLocalStorage, fmcgAPIservice, $compile, $ionicModal,$timeout) {
              $scope.$parent.navTitle = "Locations Finder";
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

        var data = fmcgLocalStorage.getData('subordinate') || [];

        $scope.geoCustomers = data;



        var map;
        var myLatlng;
        setPosFlag = false;
        var marker;
        $scope.mapLocation = function(item, m_Flag) {


        var v = 1000;
        var flightPlanCoordinates = [];
        var tDate = new Date();
         
        var Passingdat=$scope.dateFormat("YYYY-MM-dd", tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate());
        setInterval(function() {
            if ($scope.cComputer) {
                fmcgAPIservice.getPostData('POST', 'get/track&SF_Code=' + item.id + '&Dt='+Passingdat, [])
                    .success(function(response) {

                        if (v==1000 && response.length > 0 && response !== undefined) {
                             flightPlanCoordinates = [];
                            for (var i = 0; i < response.length; i++) {
                                  if (response[i].lat != 0) {
                                    var latandlang = {};
                                    latandlang.lat = Number(response[i].lat);
                                    latandlang.lng = Number(response[i].lng);
                                    latandlang.retailername=response[i].Trans_Detail_Name;
                                    latandlang.OrderValues=response[i].POB_Value;
                                    latandlang.ChannelName=response[i].ChannelName;
                                     latandlang.ordfld=response[i].ordfld;

                                    flightPlanCoordinates.push(latandlang);
                                    if (i == response.length - 1) {
                                         Passingdat=response[i].DtTm.date;
                                    }
                                }
                            };


                        }
                        if(Passingdat!=$scope.dateFormat("YYYY-MM-dd", tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate()) && response.length>0 && v!=1000){
                         
                           for (var i = 0; i < response.length; i++) {
                                  if (response[i].lat != 0) {
                                    var latandlang = {};
                                    latandlang.lat = Number(response[i].lat);
                                    latandlang.lng = Number(response[i].lng);
                                    latandlang.retailername=response[i].Trans_Detail_Name;
                                    latandlang.OrderValues=response[i].POB_Value;
                                    latandlang.ChannelName=response[i].ChannelName;
                                    latandlang.ordfld=response[i].ordfld;
                                    flightPlanCoordinates.push(latandlang);
                                    if (i == response.length - 1) {
                                         Passingdat=response[i].DtTm.date;
                                    }
                                }
                            };

                           ShowMapRoute(0);
                        }
                        if (v == 1000) {
                            v = 5000;
                            ShowMapRoute(0);
                              
                        }
                      
                    }).error(function() {
                        Toast("No Internet Connection! Try Again.");
                        $ionicLoading.hide();
                    });
                   
            } else {


            fmcgAPIservice.getPostData('POST', 'get/track&SF_Code=' + item.id + '&Dt='+Passingdat, [])
                    .success(function(response) {

                        if (v==1000 && response.length > 0 && response !== undefined) {
                             flightPlanCoordinates = [];
                            for (var i = 0; i < response.length; i++) {
                                  if (response[i].lat != 0) {
                                    var latandlang = {};
                                    latandlang.lat = Number(response[i].lat);
                                    latandlang.lng = Number(response[i].lng);
                                    latandlang.retailername=response[i].Trans_Detail_Name;
                                    latandlang.OrderValues=response[i].POB_Value;
                                    latandlang.ChannelName=response[i].ChannelName;
                                    latandlang.ordfld=response[i].ordfld;
                                    flightPlanCoordinates.push(latandlang);
                                    if (i == response.length - 1) {
                                         Passingdat=response[i].DtTm.date;
                                    }
                                }
                            };


                        }
                        if(Passingdat!=$scope.dateFormat("YYYY-MM-dd", tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate()) && response.length>0 && v!=1000){
                         
                            for (var i = 0; i < response.length; i++) {
                                  if (response[i].lat != 0) {
                                    var latandlang = {};
                                    latandlang.lat = Number(response[i].lat);
                                    latandlang.lng = Number(response[i].lng);
                                    latandlang.retailername=response[i].Trans_Detail_Name;
                                    latandlang.OrderValues=response[i].POB_Value;
                                    latandlang.ChannelName=response[i].ChannelName;
                                             latandlang.ordfld=response[i].ordfld;
                                    flightPlanCoordinates.push(latandlang);
                                    if (i == response.length - 1) {
                                         Passingdat=response[i].DtTm.date;
                                    }
                                }
                            };
                        }
                        if (v == 1000) {
                            v = 5000;
                            ShowMapRoute(0);
                              
                        }
                      
                    }).error(function() {
                        Toast("No Internet Connection! Try Again.");
                        $ionicLoading.hide();
                    });


        }
        }, 1000*5);

         
        function moveMarker(){
             for (i = 0; i < flightPlanCoordinates.length; i++) {
                     var latandlangg = {};
                if (i == 0 || flightPlanCoordinates.length - 1 == i) {
                    latandlangg.lat = flightPlanCoordinates[i].lat;
                    latandlangg.lng = flightPlanCoordinates[i].lng;
                    
                        titlename = 'End Point updated'
                        iconmarker = 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png';
                    

                    var marker = new google.maps.Marker({
                        position: latandlangg,
                        map: map,
                        title: titlename,
                        icon: iconmarker,
                        animation: google.maps.Animation.DROP,
                    })
                   var latlng = new google.maps.LatLng(flightPlanCoordinates[i].lat, flightPlanCoordinates[i].lng);
            marker.setPosition(latlng);
                }

            }
           /* var latlng = new google.maps.LatLng(position[0], position[1]);
            marker.setTitle("Latitude:"+position[0]+" | Longitude:"+position[1]);
            marker.setPosition(latlng);
            if(i!=numDeltas){
                i++;
                setTimeout(moveMarker, delay);
            }*/
        }


        function ShowMapRoute(x) {
            if (typeof google === 'undefined' || typeof google === undefined) {
                $.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyCyMzzgResJNYCYcShy46DlG26ZmQRvbGI&libraries=places');
            }
            //AIzaSyBfMnEha5uy5UZgvjhk0y6IwFXkvbB_mak
            //    $.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyCOY8BqGMFqo7Ij2n1O8b4RcL443zjoI_o&libraries=places');

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


            var map = new google.maps.Map(document.getElementById('map_canvas'), {
                center: flightPlanCoordinates[0],
                zoom: 14,
                panControl: true,
                zoomControl: true,
                mapTypeControl: false,
                scaleControl: true,
                streetViewControl: true,
                overviewMapControl: true,
                rotateControl: true,
                mapTypeId: google.maps.MapTypeId.ROADMAP

            });

            function toggleBounce() {
                if (marker.getAnimation() !== null) {
                    marker.setAnimation(null);
                } else {
                    marker.setAnimation(google.maps.Animation.BOUNCE);
                }
            }

            var flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                geodesic: true,
                strokeColor: '#F4A460',
                strokeOpacity: 1.0,
                strokeWeight: 4
            });
        var marker;
        function moveMarkerr(map,marker){
           var numDeltas = 3;
        var delay = 100; //milliseconds
        var i = 0;

        for (i = 0; i < flightPlanCoordinates.length; i++) {
             var latandlangg = {};
            latandlangg.lat = flightPlanCoordinates[i].lat;
            latandlangg.lng = flightPlanCoordinates[i].lng;
              
            if(flightPlanCoordinates.length - 1 == i ) {
                 var latlng = new google.maps.LatLng(latandlangg.lat,  latandlangg.lng);
                marker.setTitle("Thiru");
                  marker.setPosition(latlng);
                    marker.seticon('https://t1.daumcdn.net/cfile/tistory/1618BE3D50EBA95437')
                    }

        }
        marker.setMap(map);

        }
        if(flightPlanCoordinates.length==0){
            Toast("Locations Not Finding");
        }

            for (i = 0; i < flightPlanCoordinates.length; i++) {
                var latandlangg = {};
                if (i == 0 || flightPlanCoordinates.length - 1 == i || flightPlanCoordinates[i].ordfld==0 ||  flightPlanCoordinates[i].ordfld==null ) {
                    latandlangg.lat = flightPlanCoordinates[i].lat;
                    latandlangg.lng = flightPlanCoordinates[i].lng;
                    if (i == 0) {
                        var titlename = 'Start Point';
                        var iconmarker = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|FE7569';
                    } else if(flightPlanCoordinates.length - 1 == i  ) {
                        titlename = 'End Point'
                        iconmarker = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR6zmidlQjze1s0Y7iuvFEcIoA6VC05ZTXElk1t9U4fG-y8sOU0';
                    }
                    

                 if((flightPlanCoordinates[i].ordfld==0 && i!=0 && flightPlanCoordinates.length - 1 != i )|| flightPlanCoordinates[i].ordfld==null) {
                  titlename =  flightPlanCoordinates[i].Trans_Detail_Name;
                        iconmarker = 'https://cdn0.iconfinder.com/data/icons/Buildings_Icons_by_shlyapnikova/32/shop.png';
                    }

                     marker = new google.maps.Marker({
                        position: latandlangg,
                        map: map,
                        title: titlename,
                        icon: iconmarker,
                       
                         label: 5
                    })

                    var ordervl=flightPlanCoordinates[i].OrderValues;

                  var infoWindowContent ='<div id="info-bubble" style="padding:20px;border-radius:5px;max-width:260px;box-shadow:none;"><p>Order Details: <br />Retailer Name' + flightPlanCoordinates[i].retailername + '<br /> Ordervalues:  <span>'+ flightPlanCoordinates[i].OrderValues+ '</span></p></div>';
        var infowindow = new google.maps.InfoWindow({
                content: infoWindowContent
            });
                }

           google.maps.event.addListener(marker,'click', (function(marker,infowindow){ 
                return function() {
                            if (infowindow) {
                             infowindow.close();
                              }
                       
                      infowindow.open(map,marker);
                        
                  
                };
            })(marker,infowindow)); 
                
            }

            flightPath.setMap(map);


            marker.addListener('click', toggleBounce, function (map, marker) {
                    
                });
        }
        }

        })


    .filter('selectMap', function() {

        return function(input, data) {
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
    }).filter('reverseGeoCode', function() {
        return function(input) {
            return input;
        };
    }).filter('getValueforID', function() {
        return function(id, collection) {
            var arrayToReturn = [];
            if (collection && id) {

                var len;
                try {
                    len = collection.length;
                } catch (m) {
                    len = 0;
                }
                for (var i = 0; i < len; i++) {
                    if (id == collection[i].id) {
                        return collection[i];
                        break;
                    }
                }
            }
            return null;
        };
    }).filter('searchF', function() {
        return function(id, collection) {
            var arrayToReturn = [];
            if (collection && id) {
                collection = collection;
                var len;
                try {
                    len = collection.length;
                } catch (m) {
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
        };
    }).filter('getValueforIDD', function() {
        return function(id, collection) {
            var arrayToReturn = [];
            if (collection && id) {
                collection = collection.collection;

                var len;
                try {
                    len = collection.length;
                } catch (m) {
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
        };
    }).directive('reverseGeocode', function() {
        return {
            restrict: 'E',
            template: '<div></div>',
            link: function(scope, element, attrs) {
                var geocoder = new google.maps.Geocoder();
                var results = attrs.latlng.split(":");
                if (results.length == 2) {
                    var latlng = new google.maps.LatLng(results[0], results[1]);
                    geocoder.geocode({
                        'latLng': latlng
                    }, function(results, status) {
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
var isReachable = function() {
    if (isComputer())
        return true;
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
    } else {
        return navigator.onLine;
    }
}
var isComputer = function() {

    if (navigator.platform === "Win32") {
        return true;
    }
    return false;
}
var isGpsEnabled = function(PositionError) {
    if (isComputer())
        return true;
    else if (PositionError) {
        return false;
    }
    return true;
}

var Toast = function(message, type) {
    if (window.plugins) {
        window.plugins.toast.showShortCenter(message, function(a) {
            console.log('toast success: ' + a)
        }, function(b) {
            alert('toast error: ' + b)
        });
    } else {
        var data = {};
        data.message = message;
        data.extraClasses = 'messenger-fixed messenger-on-left messenger-on-top';
        if (type)
            data.type = "error";
        Messenger().post(data);
    }
};
var cache_width, a4 = [595.28, 841.89]; // for a4 size paper width and height

function SendPDFtoSocialShare(form, Subject, FileName, Mode) {
    cCtrl = $(event.target);
    cCtrl.closest('.modal').css("height", $(form).height() + 300);
    cCtrl.closest('.ion-content').css("height", $(form).height() + 300);
    cCtrl.closest('.scroll').css("height", $(form).height() + 300);
    cCtrl.closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
    $('ion-nav-view').css("height", $(form).height() + 300);
    $('body').css("overflow", 'visible');
    cache_width = form.width();
    scrl = form.closest("body");
    cache_width = form.width();
    cache_width1 = scrl.width();
    scrl.width((a4[0] * 1.33333) - 80).css('max-width', 'none');
    form.width((a4[0] * 1.33333) - 80).css('max-width', 'none');
    html2canvas(form, {
        imageTimeout: 2000,
        removeContainer: true
    }).then(function(canvas) {
        var img = canvas.toDataURL("image/png") //,
        doc = new jsPDF({
            unit: 'mm',
            format: 'a4'
        }); 

        function footer() {
            doc.setDrawColor(0, 0, 0);
            doc.setFillColor(255, 255, 255);
            doc.rect(0, 0, 214, 20, "F");
            doc.setFillColor(255, 255, 255);
            doc.rect(0, 280, 214, 20, "F");
            var d = new Date();
            var n = d.toLocaleDateString();
            doc.setFontSize(6);
            doc.text(185, 295, n); //print number bottom right
            doc.page++;
        };
        var imgWidth = 170;
        var pageHeight = 255;
        var imgHeight = canvas.height * imgWidth / canvas.width;
        var heightLeft = imgHeight;
        var position = 20;

        doc.addImage(img, 'PNG', 20, position, imgWidth, imgHeight);
        footer();
        heightLeft -= pageHeight;
        while (heightLeft >= 0) {
            position = (heightLeft + 17) - imgHeight;
            console.log(position);
            doc.addPage();
            doc.addImage(img, 'PNG', 20, position, imgWidth, imgHeight);
            footer();
            heightLeft -= (pageHeight + 3);
        }

        if (isComputer())
            doc.save(FileName + '.pdf');
        else {
            sFile = cordova.file.externalRootDirectory + "FMCG/" + FileName + '.pdf';
            window.resolveLocalFileSystemURL(cordova.file.externalRootDirectory,
                function(fileSystem) {
                    fileSystem.getDirectory("FMCG", {
                            create: true,
                            exclusive: false
                        },
                        function(dir) {
                            dir.getFile(FileName + ".pdf", {
                                    create: true,
                                    exclusive: false
                                },
                                function(fileEntry) {
                                    fileEntry.createWriter(
                                        function(writer) {
                                            writer.write(doc.output("blob"));
                                            if (Mode == "w")
                                                window.plugins.socialsharing.shareViaWhatsApp(Subject, sFile, null, function() {}, function(errormsg) {
                                                    alert(errormsg)
                                                })
                                            else if (Mode == "e")
                                                window.plugins.socialsharing.share('', Subject, sFile, '');

                                        },
                                        function() {
                                            console.log("ERROR 1!");
                                        });
                                },
                                function() {
                                    console.log("ERROR 2!");
                                });
                        },
                        function() {
                            console.log("ERROR 3!");
                        });
                },
                function() {
                    console.log("ERROR 4!");
                });
        }
        cCtrl.closest('.modal').css("height", '');
        cCtrl.closest('.ion-content').css("height", '');
        cCtrl.closest('.scroll').css("height", '');
        cCtrl.closest('.scroll').css('transform', 'translate3d(0px, 0px, 0px) scale(1)');
        $('ion-nav-view').css("height", '');
        form.css('width', '');
        form.css('width', '');

    });
}


function enableLight() {
    if (window.watcher) {
        navigator.geolocation.clearWatch(window.watcher);
    }
    var ele = document.querySelector(".ion-location");
    window.watcher = navigator.geolocation.watchPosition(function(position) {
            ele && ele.classList.add("available");
        },
        function(error) {
            if (ele) {
                ele.classList.remove("available");
            } else {
                console.log("Notifier icon not found");
            }
        }, {
            maximumAge: 0,
            timeout: 5000,
            enableHighAccuracy: true
        })
}
document.addEventListener("deviceready", function() {
    enableLight();
});
document.addEventListener("resume", function() {
    //    alert("Resume"+window.watcher);
    enableLight();
    //    if (window.watch) {
    //        navigator.geolocation.clearWatch(window.watch);
    //    }
}, false);