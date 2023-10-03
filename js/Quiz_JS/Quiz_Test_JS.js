$(document)['ready'](function () {
    $('#btnHomepage')['hide']();
    var _0x5a9ax1;
    var _0x5a9ax2 = 0;
    var _0x5a9ax3 = [];
    var _0x5a9ax4 = $('#quiz');
    var _0x5a9ax5 = [];
    var _0x5a9ax6;
    var _0x5a9ax7;
    _0x5a9ax2b();
    $('#next')['on']('click', function (_0x5a9ax8) {
        _0x5a9ax8['preventDefault']();
        if (_0x5a9ax4['is'](':animated')) {
            return false
        };
        _0x5a9ax14();
        if (isNaN(_0x5a9ax3[_0x5a9ax2])) {
            alert('Please make a selection!')
        } else {
            _0x5a9ax2++;
            _0x5a9ax15()
        }
    });
    $('#prev')['on']('click', function (_0x5a9ax8) {
        _0x5a9ax8['preventDefault']();
        if (_0x5a9ax4['is'](':animated')) {
            return false
        };
        _0x5a9ax14();
        _0x5a9ax2--;
        _0x5a9ax15();
        $('#next')['html']('Next')
    });
    $('.button')['on']('mouseenter', function () {
        $(this)['addClass']('active')
    });
    $('.button')['on']('mouseleave', function () {
        $(this)['removeClass']('active')
    });

    function _0x5a9ax9(_0x5a9axa) {
        var _0x5a9axb = $('<div>', {
            id: 'question'
        });
        var _0x5a9axc = $('<h2 style="color:#f740cf">Question ' + (_0x5a9axa + 1) + ':</h2>');
        _0x5a9axb['append'](_0x5a9axc);
        var _0x5a9axd = $('<p>')['append'](_0x5a9ax1[_0x5a9axa]['question']);
        _0x5a9axb['append'](_0x5a9axd);
        var _0x5a9axe = _0x5a9axf(_0x5a9axa);
        _0x5a9axb['append'](_0x5a9axe);
        return _0x5a9axb
    }

    function _0x5a9axf(_0x5a9axa) {
        var _0x5a9ax10 = $('<ul>');
        var _0x5a9ax11;
        var _0x5a9ax12 = '';
        for (var _0x5a9ax13 = 0; _0x5a9ax13 < _0x5a9ax1[_0x5a9axa]['choices']['length']; _0x5a9ax13++) {
            _0x5a9ax11 = $('<li>');
            _0x5a9ax12 = '<input type="radio" name="answer" value=' + _0x5a9ax13 + ' />';
            _0x5a9ax12 += _0x5a9ax1[_0x5a9axa]['choices'][_0x5a9ax13];
            _0x5a9ax11['append'](_0x5a9ax12);
            _0x5a9ax10['append'](_0x5a9ax11)
        };
        return _0x5a9ax10
    }

    function _0x5a9ax14() {
        _0x5a9ax3[_0x5a9ax2] = +$('input[name="answer"]:checked')['val']()
    }

    function _0x5a9ax15() {
        _0x5a9ax4['fadeOut'](function () {
            $('#question')['remove']();
            if (_0x5a9ax2 < _0x5a9ax1['length']) {
                var _0x5a9ax16 = _0x5a9ax9(_0x5a9ax2);
                _0x5a9ax4['append'](_0x5a9ax16)['fadeIn']();
                if (!(isNaN(_0x5a9ax3[_0x5a9ax2]))) {
                    $('input[value=' + _0x5a9ax3[_0x5a9ax2] + ']')['prop']('checked', true)
                };
                if (_0x5a9ax2 === 1) {
                    $('#prev')['show']()
                } else {
                    if (_0x5a9ax2 === 0) {
                        $('#prev')['hide']();
                        $('#next')['show']()
                    } else {
                        if (_0x5a9ax2 == _0x5a9ax1['length'] - 1) {
                            $('#next')['html']('Submit')
                        }
                    }
                }
            } else {
                var _0x5a9ax17 = new Date();
                _0x5a9ax7 = (_0x5a9ax17['getMonth']() + 1) + '-' + _0x5a9ax17['getDate']() + '-' + _0x5a9ax17['getFullYear']() + ' ' + _0x5a9ax17['getHours']() + ':' + _0x5a9ax17['getMinutes']() + ':' + _0x5a9ax17['getSeconds']();
                var _0x5a9ax18 = _0x5a9ax19();
                _0x5a9ax4['append'](_0x5a9ax18)['fadeIn']();
                $('#next')['hide']();
                $('#prev')['hide']();
                _0x5a9ax28()
            }
        })
    }

    function _0x5a9ax19() {
        var _0x5a9ax1a = $('<p>', {
            id: 'question'
        });
        var _0x5a9ax1b = 0;
        for (var _0x5a9ax13 = 0; _0x5a9ax13 < _0x5a9ax3['length']; _0x5a9ax13++) {
            var _0x5a9ax1c = _0x5a9ax3[_0x5a9ax13];
            var _0x5a9ax1d = _0x5a9ax1[_0x5a9ax13]['ChoiceID'][_0x5a9ax1c];
            var _0x5a9ax1e = _0x5a9ax1[_0x5a9ax13]['Question_Id'];
            var _0x5a9ax1f = _0x5a9ax1[_0x5a9ax13]['correctAnswer'];
            var _0x5a9ax20 = _0x5a9ax1e + '^' + _0x5a9ax1d;
            _0x5a9ax5['push'](_0x5a9ax20);
            if (_0x5a9ax1c == _0x5a9ax1f) {
                _0x5a9ax1b++
            }
        };
        //var _0x5a9ax21 = _0x5a9ax1b;
        //var _0x5a9ax22 = _0x5a9ax1['length'];
        //var _0x5a9ax23 = parseFloat((parseFloat(_0x5a9ax21) / parseFloat(_0x5a9ax22)) * 100);
        //var _0x5a9ax24 = Math['round'](_0x5a9ax23);
        //var _0x5a9ax25 = '';
        //if (_0x5a9ax24 < 50) {
        //    _0x5a9ax25 = '<img src="../../Images/below_50.jpg" alt="" />'
        //} else {
        //    if (_0x5a9ax24 >= 50 && _0x5a9ax24 < 70) {
        //        _0x5a9ax25 = '<img src="../../Images/50_70.jpg" alt="" />'
        //    } else {
        //        if (_0x5a9ax24 >= 70 && _0x5a9ax24 < 100) {
        //            _0x5a9ax25 = '<img src="../../Images/72_Percen.jpg" alt="" />'
        //        } else {
        //            if (_0x5a9ax24 == 100) {
        //                _0x5a9ax25 = '<img src="../../Images/100_Percent.gif" alt="" />'
        //            }
        //        }
        //    }
        //};
        var _0x5a9ax26 = '<center>';
        _0x5a9ax26 += '<div>';
       
        _0x5a9ax26 += '<h2 class=\'Service\'>Thank you for Attending the Online Quiz. Check your Marks under MIS Reports --> Quiz Result !!! ';
        _0x5a9ax26 += '<img src="../../Images/100_Percent.gif" alt="" />'
        _0x5a9ax1a['append'](_0x5a9ax26);
        $('#btnHomepage')['show']();
        $('#demo')['hide']();
        return _0x5a9ax1a
    }

    function _0x5a9ax27() {
        window['location'] = ('http://sanffs.info/')
    }

    function _0x5a9ax28() {
        var _0x5a9ax29 = _0x5a9ax5 + '#' + _0x5a9ax6 + '#' + _0x5a9ax7;
        $['ajax']({
            type: 'POST',
            url: '../webservice/Quiz_QuestionWS.asmx/Add_QuizResult',
            data: '{Result:' + JSON['stringify'](_0x5a9ax29) + '}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (_0x5a9ax20) {
                if (_0x5a9ax20['d']['length'] > 0) { }
            },
            error: function (_0x5a9ax2a) { }
        })
    }

    function _0x5a9ax2b() {
        $['ajax']({
            type: 'POST',
            url: '../webservice/Quiz_QuestionWS.asmx/Get_QuizTimeLimit',
            data: '{}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (_0x5a9ax20) {
                var _0x5a9ax2c = _0x5a9ax20['d'];
                var _0x5a9ax2d = [];
                _0x5a9ax2d = _0x5a9ax2c['split']('^');
                var _0x5a9ax2e = new Countdown({
                    wrapId: 'demo',
                    seconds: _0x5a9ax2d[0],
                    callback: _0x5a9ax27,
                    ratio: 2
                });
                _0x5a9ax2f(_0x5a9ax2d[1])
            },
            error: function (_0x5a9ax2a) { }
        })
    }

    function _0x5a9ax2f(_0x5a9ax30) {
        $['ajax']({
            type: 'POST',
            url: '../webservice/Quiz_QuestionWS.asmx/GetQuestion',
            data: '{}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (_0x5a9ax20) {
                if (_0x5a9ax20['d']['length'] > 0) {
                    _0x5a9ax1 = JSON['parse'](_0x5a9ax20['d']);
                    if (_0x5a9ax30 == 'Suffle') {
                        _0x5a9ax1 = shuffle(_0x5a9ax1);
                        console['log'](_0x5a9ax1)
                    } else {
                        _0x5a9ax1 = JSON['parse'](_0x5a9ax20['d'])
                    };
                    _0x5a9ax15();
                    var _0x5a9ax17 = new Date();
                    _0x5a9ax6 = (_0x5a9ax17['getMonth']() + 1) + '-' + _0x5a9ax17['getDate']() + '-' + _0x5a9ax17['getFullYear']() + ' ' + _0x5a9ax17['getHours']() + ':' + _0x5a9ax17['getMinutes']() + ':' + _0x5a9ax17['getSeconds']()
                }
            },
            error: function (_0x5a9ax2a) { }
        })
    }
})();

function shuffle(_0x5a9ax32) {
    var _0x5a9ax33 = _0x5a9ax32['length'],
        _0x5a9ax34, _0x5a9ax35;
    while (0 !== _0x5a9ax33) {
        _0x5a9ax35 = Math['floor'](Math['random']() * _0x5a9ax33);
        _0x5a9ax33 -= 1;
        _0x5a9ax34 = _0x5a9ax32[_0x5a9ax33];
        _0x5a9ax32[_0x5a9ax33] = _0x5a9ax32[_0x5a9ax35];
        _0x5a9ax32[_0x5a9ax35] = _0x5a9ax34
    };
    return _0x5a9ax32
}