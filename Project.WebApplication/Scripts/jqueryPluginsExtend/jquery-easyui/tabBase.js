
var pro = pro || {};

(function() {
    pro.TabBase = pro.TabBase || {};

    pro.TabBase = function (addconfig) {
        var defaultParamter = {
            tabId: "tabs",
            title: "tab标签",
            //url: "",
            //content: "<iframe src='" + $.tabExtend.config.url + "' style='width: 100%; height:100%;' frameborder='0'/>",
            iconCls: "icon-save",
            closable: true,
            bodyCls: "tabOverflow"
        };
        this.config = $.extend({}, defaultParamter, addconfig);
    };

    pro.TabBase.prototype = {
        add: function(obj) {
            

        }
    }
})();