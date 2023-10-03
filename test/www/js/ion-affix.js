angular.module('ion-affix', ['ionic'])
    .directive('ionAffix', ['$ionicPosition', '$compile', function ($ionicPosition, $compile) {
        function getParentWithClass(elementSelector, parentClass) {
            return angular.element(ionic.DomUtil.getParentWithClass(elementSelector[0], parentClass));
        }
        function throttle(theFunction) {
            return ionic.Utils.throttle(theFunction);
        }
        function requestAnimationFrame(callback) {
            return ionic.requestAnimationFrame(callback);
        }
        function offset(elementSelector) {
            return $ionicPosition.offset(elementSelector);
        }
        function position(elementSelector) {
            return $ionicPosition.position(elementSelector);
        }

        function applyTransform(element, transformString) {
            if (element.style[ionic.CSS.TRANSFORM] == transformString) {
            }
            else {
                element.style[ionic.CSS.TRANSFORM] = transformString;
            }
        }

        function translateUp(element, dy, executeImmediately) {
            var translateDyPixelsUp = dy == 0 ? 'translate3d(0px, 0px, 0px)' : 'translate3d(0px, -' + dy + 'px, 0px)';
            if (executeImmediately) {
                applyTransform(element, translateDyPixelsUp);
            }
            else {
                requestAnimationFrame(function () {
                    applyTransform(element, translateDyPixelsUp);
                });
            }
        }

        var CALCULATION_THROTTLE_MS = 500;

        return {
            restrict: 'A',
            require: '^$ionicScroll',
            link: function ($scope, $element, $attr, $ionicScroll) {
                var $container;
                if ($attr.affixWithinParentWithClass) {
                    $container = getParentWithClass($element, $attr.affixWithinParentWithClass);
                    if (!$container) {
                        $container = $element.parent();
                    }
                }
                else {
                    $container = $element.parent();
                }

                var scrollMin = 0;
                var scrollMax = 0;
                var scrollTransition = 0;
                var calculateScrollLimits = function (scrollTop) {
                    var containerPosition = position($container);
                    var elementOffset = offset($element);

                    var containerTop = containerPosition.top;
                    var containerHeight = containerPosition.height;

                    var affixHeight = elementOffset.height;

                    scrollMin = scrollTop + containerTop;
                    scrollMax = scrollMin + containerHeight;
                    scrollTransition = scrollMax - affixHeight;
                };
                var throttledCalculateScrollLimits = throttle(
                    calculateScrollLimits,
                    CALCULATION_THROTTLE_MS,
                    {trailing: false}
                );

                var affixClone = null;

                var createAffixClone = function () {
                    var clone = $element.clone().css({
                        position: 'absolute',
                        top: 0,
                        left: 0,
                        right: 0
                    });

                    if ($attr.affixClass) {
                        clone.addClass($attr.affixClass);
                    }

                    clone.removeAttr('ion-affix').removeAttr('data-ion-affix').removeAttr('x-ion-affix');

                    angular.element($ionicScroll.element).append(clone);

                    $compile(clone)($scope);

                    return clone;
                };

                var removeAffixClone = function () {
                    if (affixClone)
                        affixClone.remove();
                    affixClone = null;
                };

                $scope.$on("$destroy", function () {
                    removeAffixClone();
                    angular.element($ionicScroll.element).off('scroll');
                });


                angular.element($ionicScroll.element).on('scroll', function (event) {
                    var scrollTop = (event.detail || event.originalEvent && event.originalEvent.detail).scrollTop;
                    if (scrollTop == 0) {
                        calculateScrollLimits(scrollTop);
                    }
                    else {
                        throttledCalculateScrollLimits(scrollTop);
                    }

                    if (scrollTop >= scrollMin && scrollTop <= scrollMax) {
                        var cloneCreatedJustNow = false;
                        if (!affixClone) {
                            affixClone = createAffixClone();
                            cloneCreatedJustNow = true;
                        }

                        if (scrollTop > scrollTransition) {
                            translateUp(affixClone[0], Math.floor(scrollTop - scrollTransition), cloneCreatedJustNow);
                        } else {
                            translateUp(affixClone[0], 0, cloneCreatedJustNow);
                        }
                    } else {
                        removeAffixClone();
                    }
                });
            }
        }
    }]);