
$.datagridExtend = $.datagridExtend || {};
(function () {
    $.datagridExtend = {
        config: {
            grdidId: "#datagrid",
            url: ""
        },
        getObject: function () {
            return $(this.config.grdidId);
        },
        getFunObject: function (obj) {
            return $(this.config.grdidId).datagrid(obj);
        },
        isSelected: function () {

            if (this.getFunObject("getSelected")) return true;
            return false;
        },
        closeTab: function (tabId) {

        },
        selectRowLast: function () {
            $(this.config.grdidId).datagetFunObject("selectRow", this.getRowsCount() - 1);
        },
        getSelectedIndex: function () {
            var selectedIndex = $(this.config.grdidId).datagetFunObject("getRowIndex", $(this.config.grdidId).datagetFunObject("getSelected"));
            return selectedIndex;
        },
        getRowsCount: function () {
            return $(this.config.grdidId).datagetFunObject("getRows").length;
        },
        searchForm: function () {
            ///<summary>获取json格式搜索参数</summary>
            var strJson = "{";
            $("#divSearch input").each(function () {
                strJson += "\"" + $(this).attr("name") + "\":\"" + $.trim($(this).val()) + "\",";
            });

            $("#divSearch select").each(function () {
                strJson += "\"" + $(this).attr("name") + "\":\"" + $.trim($(this).val()) + "\",";
            });
            if (strJson.length > 1) strJson = strJson.substr(0, strJson.length - 1);
            strJson += "}";

            var jsonSearch = $.parseJSON(strJson);
            return jsonSearch;
        },
        reload: function (formSearch) {
            ///<summary>刷新列表，不带搜索参数，当前页数重置1</summary>
            if (!formSearch) formSearch = {};
            this.getFunObject("options").queryParams = formSearch;
            this.getFunObject("load");
        },
        refresh: function () {
            ///<summary>重置搜索表单；刷新列表，不带搜索参数，当前页数重置1</summary>
            this.resetFormSearch();
            this.reload();
        },
        search: function () {
            ///<summary>搜索</summary>
            var formSearch = this.searchForm();
            this.reload(formSearch);
        }
    };
})();


