
//eg var tabObj = new pro.TabBase({ tabId: "tabs", title: "hh" });

var pro = pro || {};

(function () {
    pro.TabBase = pro.TabBase || {};

    pro.TabBase = function (addconfig) {
        var defaultParamter = {
            tabId: "tabs",
            title: "tab标签",
            //url: "",
            //content: "<iframe src='" + $.tabExtend.config.url + "' style='width: 100%; height:100%;' frameborder='0'/>",
           // iconCls: "icon-save",
            closable: true,
            bodyCls: "tabOverflow"
        };
        this.config = $.extend({}, defaultParamter, addconfig);
    };
    pro.TabBase.prototype = {
        add: function (url,title) {
            var content = {
                title: title,
                content: "<iframe src='" + url + "' style='width: 100%; height:100%;' frameborder='0'/>"
            };
            var options = $.extend({}, this.config, content);
            $("#" + options.tabId).tabs('add', options);
        },
        closeTab: function () {
            $("#" + this.config.tabId).tabs("close", $("#" + this.config.tabId).tabs("getSelected").panel('options').title);
        }
    }
})();