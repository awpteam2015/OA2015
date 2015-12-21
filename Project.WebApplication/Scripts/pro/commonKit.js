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
        }
    };
})();