var $body = $("body"),
    mainNavigation = $('.menu-panel');
$(document).ready(function () {
    $(window).resize(function () {

        $('.left-side').css('height', $(window).height() - 63);
        $('.Work-Area').css('height', $(window).height() - 63);
        $('#mnuWrapr').css('height', $(window).height() - 90);
        $('.working-Area').css('height', $(window).height() - 120);

        if ($(window).width() <= 800) hideMenu(); else if (!$('.left-side').hasClass('active')) showMenu();
        $('.ContArea').css('width',  $(window).width()-(($(window).width() <= 800)?0:240));
        $('.ContAreaData').css('width',  $(window).width()-(($(window).width() <= 800)?0:240));
        
    });

    $(document).click(function (e) {
        if ($('.dropdown.messages-menu.open').has(e.target).length < 1) {
            $('.dropdown.messages-menu.open').removeClass('open');
        }
        if ($('.dropdown.warning-menu.open').has(e.target).length < 1) {
            $('.dropdown.warning-menu.open').removeClass('open');
        }
        if ($('.dropdown.tasks-menu.open').has(e.target).length < 1) {
            $('.dropdown.tasks-menu.open').removeClass('open');
        }
        if ($('.notifier').has(e.target).length < 1) {
            $('#mnuProf').removeClass('open');
        }
        if ($(window).width() <= 580) {
            if ($('.left-side.active').has(e.target).length < 1 && $('.Mnu-Bt').has(e.target).length < 1) {
                hideMenu();
            }
        }
    }

    );

    $('.prof-DropDn').on('click', function () {
        if ($('#mnuProf').hasClass('open')) {
            $('#mnuProf').removeClass('open');
        }
        else {
            $('#mnuProf').addClass('open');
        }
    });


    /*$('.nav>li>a').on('click', function () {
    if ($(this).parent().hasClass('open')) {
    $(this).parent().removeClass('open');
    }
    else {
    $(this).parent().addClass('open');
    }
    });*/
    $('.left-side').css('height', $(window).height() - 62);
    $('.Work-Area').css('height', $(window).height() - 63);
    $('#mnuWrapr').css('height', $(window).height() - 90);
    $('.working-Area').css('height', $(window).height() - 120);

    $('.tab-hd > ul > li').click(function () {
        $('.tab-hd > ul > li.select').removeClass('select');
        $(this).addClass('select');
        var $ox = '#' + $(this).attr("data-target")
        $('.tb-wrk-Win.active').removeClass('active');
        $($ox).addClass('active');
    });
    /*$('ul > li > a').click(function () {
    $('li').removeClass('active');
    $(this).closest('li').addClass('active');
    var checkElement = $(this).next();
    if ((checkElement.is('ul')) && (checkElement.is(':visible'))) {
    $(this).closest('li').removeClass('active');
    checkElement.slideUp('normal');
    }
    if ((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
    $('ul ul:visible').slideUp('normal');
    checkElement.slideDown('normal');
    }
    if ($(this).closest('li').find('ul').children().length == 0) {
    return true;
    } else {
    return false;
    }
    });*/
    runNavigationMenu();

    if ($(window).width() <= 800)
        hideMenu();
    $('.fa-bars').click(
        function () {
            if ($('.left-side').hasClass('active')) hideMenu(); else showMenu();
        }
    );
});
showMenu = function () { 
    $('.left-side').addClass('active');
    $('.Work-Area').addClass('active'); //.css('margin-left', '220px');
}

hideMenu = function () {
    $('.left-side').removeClass('active');
    $('.Work-Area').removeClass('active'); //.css('margin-left', '0px');
}










    var runNavigationMenu = function () {
        if ($("body").hasClass("single-page") == false) {
            $('.main-menu > li.active').addClass('open');
            $('.main-menu > li a').on('click', function () {

                if ($(this).parent().children('ul').hasClass('sub-menu') && ((!$body.hasClass('navigation-small') || $windowWidth < 767) || !$(this).parent().parent().hasClass('main-menu'))) {
                    
                    if (!$(this).parent().hasClass('open')) {
                        $(this).parent().addClass('open');
                        $(this).parent().parent().children('li.open').not($(this).parent()).not($('.main-menu > li.active')).removeClass('open').children('ul').slideUp(200);
                        $(this).parent().children('ul').slideDown(200, function () {
                           /* if (mainNavigation.height() > $(".main-menu").outerHeight()) {
                                mainNavigation.scrollTo($(this).parent("li"), 300, {
                                    
                                    onAfter: function () {
                                        if ($body.hasClass("isMobile") == false) {
                                            mainNavigation.perfectScrollbar('update');
                                        }
                                    }
                                });
                            } else {

                                mainNavigation.scrollTo($(this).parent("li"), 300, {
                                    onAfter: function () {
                                        if ($body.hasClass("isMobile") == false) {
                                            mainNavigation.perfectScrollbar('update');
                                        }
                                    }
                                });
                            }*/
                        });
                    } else {
                        if (!$(this).parent().hasClass('active')) {
                            $(this).parent().parent().children('li.open').not($('.main-menu > li.active')).removeClass('open').children('ul').slideUp(200, function () {
                               /* if (mainNavigation.height() > $(".main-menu").outerHeight()) {
                                    mainNavigation.scrollTo(0, 300, {
                                        onAfter: function () {
                                            if ($body.hasClass("isMobile") == false) {
                                                mainNavigation.perfectScrollbar('update');
                                            }
                                        }
                                    });
                                } else {
                                    mainNavigation.scrollTo($(this).parent("li").closest("ul").children("li:eq(0)"), 300, {
                                        onAfter: function () {
                                            if ($body.hasClass("isMobile") == false) {
                                                mainNavigation.perfectScrollbar('update');
                                            }
                                        }
                                    });
                                }*/
                            });
                        } else {
                            $(this).parent().parent().children('li.open').removeClass('open').children('ul').slideUp(200, function () {
                               /* if (mainNavigation.height() > $(".main-menu").outerHeight()) {
                                    mainNavigation.scrollTo(0, 300, {
                                        onAfter: function () {
                                            if ($body.hasClass("isMobile") == false) {
                                                mainNavigation.perfectScrollbar('update');
                                            }
                                        }
                                    });
                                } else {
                                    mainNavigation.scrollTo($(this).parent("li"), 300, {
                                        onAfter: function () {
                                            if ($body.hasClass("isMobile") == false) {
                                                mainNavigation.perfectScrollbar('update');
                                            }
                                        }
                                    });
                                }*/
                            });
                        }
                    }
                } else {
                    $(this).parent().addClass('active');
                }
            });
        } else {
            var url, ajaxContainer = $("#ajax-content");
            var start = $('.main-menu li.start');
            if (start.length) {
                start.addClass("active");
                if (start.closest('ul').hasClass('sub-menu')) {
                    start.closest('ul').parent('li').addClass('active open');
                }
                url = start.children("a").attr("href");
                ajaxLoader(url, ajaxContainer);
            }
            $('.main-menu > li.active').addClass('open');
            $('.main-menu > li a').on('click', function (e) {
                e.preventDefault();
                var $this = $(this);

                if ($this.parent().children('ul').hasClass('sub-menu') && (!$('body').hasClass('navigation-small') || !$this.parent().parent().hasClass('main-menu'))) 
                {
                    if (!$this.parent().hasClass('open')) 
                    {
                        $this.parent().addClass('open');
                        $this.parent().parent().children('li.open').not($this.parent()).not($('.main-menu > li.active')).removeClass('open').children('ul').slideUp(200);
                        $this.parent().children('ul').slideDown(200, function () {
                            runContainerHeight();
                        });
                    } else {
                        if (!$this.parent().hasClass('active')) {
                            $this.parent().parent().children('li.open').not($('.main-menu > li.active')).removeClass('open').children('ul').slideUp(200, function () {
                                runContainerHeight();
                            });
                        } else {
                            $this.parent().parent().children('li.open').removeClass('open').children('ul').slideUp(200, function () {
                                runContainerHeight();
                            });
                        }
                    }
                } else {

                    $('.main-menu ul.sub-menu li').removeClass('active');
                    $this.parent().addClass('active');
                    var closestUl = $this.parent('li').closest('ul');
                    if (closestUl.hasClass('main-menu')) {
                        $('.main-menu > li.active').removeClass('active').removeClass('open').children('ul').slideUp(200);
                        $this.parents('li').addClass('active');
                    } else if (!closestUl.parent('li').hasClass('active') && !closestUl.parent('li').closest('ul').hasClass('sub-menu')) {
                        $('.main-menu > li.active').removeClass('active').removeClass('open').children('ul').slideUp(200);
                        $this.parent('li').closest('ul').parent('li').addClass('active');
                    } else {

                        if (closestUl.parent('li').closest('ul').hasClass('sub-menu')) {
                            if (!closestUl.parents('li.open').hasClass('active')) {
                                $('.main-menu > li.active').removeClass('active').removeClass('open').children('ul').slideUp(200);
                                closestUl.parents('li.open').addClass('active');
                            }
                        }

                    }
                    url = $(this).attr("href");
                    ajaxLoader(url, ajaxContainer);
                };
            });
        }
    };