
$.tabExtend = $.tabExtend || {};
(function () {
    $.tabExtend = {
        config: {
            tabId: "tabs",
            title: "tab标签",
            url: ""
        },
        add: function () {
            if ($.tabExtend.config.url == "") {
                alert("config.url必填！");
                return false;
            }
            var defaultParamter = {
                content: "<iframe src='" + $.tabExtend.config.url + "' style='width: 100%; height:100%;' frameborder='0'/>",
                iconCls: "icon-save",
                closable: true,
                bodyCls: "tabOverflow"
            };
            var options = $.extend({}, defaultParamter, this.config);
            $("#" + options.tabId).tabs('add', options);
        },
        closeTab: function (tabId) {
            $("#" + this.config.tabId).tabs("close", $("#" + this.config.tabId).tabs("getSelected").panel('options').title);
        }
    };
})();


