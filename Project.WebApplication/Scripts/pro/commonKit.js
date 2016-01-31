var pro = pro || {};
(function () {
    pro.commonKit = pro.commonKit || {};
    pro.commonKit = {
        getUrlParam: function (paramName) {
            var paramValue = "";
            var isFound = false;
            if (window.location.search.indexOf("?") == 0 && window.location.search.indexOf("=") > 1) {
                var arrSource = unescape(window.location.search).substring(1, window.location.search.length).split("&");
                var i = 0;
                while (i < arrSource.length && !isFound) {
                    if (arrSource[i].indexOf("=") > 0) {
                        if (arrSource[i].split("=")[0].toLowerCase() == paramName.toLowerCase()) {
                            paramValue = arrSource[i].split("=")[1];
                            isFound = true;
                        }
                    }
                    i++;
                }
            }
            return paramValue;
        },
        parseParam: function (param, key) {
            var paramStr = "";
            if (param instanceof String || param instanceof Number || param instanceof Boolean) {
                paramStr += "&" + key + "=" + encodeURIComponent(param);
            } else {
                $.each(param, function (i) {
                    var k = key == null ? i : key + (param instanceof Array ? "[" + i + "]" : "." + i);
                    paramStr += '&' + pro.commonKit.parseParam(this, k);
                });
            }
            return paramStr.substr(1);
        },
        errorPlacementHd: function (error, element) {
            if (element.get(0).type.indexOf("select") == -1) {
                element.attr('title', error.html());
                element.poshytip({
                    className: 'tip-yellowsimple',
                    showOn: 'focus',
                    alignTo: 'target',
                    alignX: 'inner-left',
                    offsetX: 0,
                    offsetY: 5,
                    showTimeout: 100
                });
            }
        }
    };
})();