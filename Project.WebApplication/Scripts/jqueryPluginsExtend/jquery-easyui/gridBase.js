
var pro = pro || {};

(function () {
    pro.GridBase = pro.GridBase || {};

    pro.GridBase = function (id, isTree) {
        if (!id) {
            id = "#datagrid";
        }
        if (!isTree) {
            isTree = false;
        }
        this.grdidId = id;
        this.isTree = isTree;
        this.RowIndex = 0;
        this.PkId = -1;
    };

    pro.GridBase.prototype = {
        grid: function (obj, data) {
            if (this.isTree) {
                return data == undefined ? $(this.grdidId).treegrid(obj) : $(this.grdidId).treegrid(obj, data);
            } else {
                return data == undefined ? $(this.grdidId).datagrid(obj) : $(this.grdidId).datagrid(obj, data);
            }
        },
        isSelected: function () {
            if (this.grid("getSelected")) return true;
            return false;
        },
        selectRowLast: function () {
            $(this.grdidId).datagrid("selectRow", this.getRowsCount() - 1);
        },
        getSelectedIndex: function () {
            var selectedIndex = $(this.grdidId).datagrid("getRowIndex", $(this.grdidId).datagrid("getSelected"));
            return selectedIndex;
        },
        getSelectedRow: function () {
            var getSelected = $(this.grdidId).datagrid("getSelected");
            return getSelected;
        },
        getRowsCount: function () {
            return $(this.grdidId).datagrid("getRows").length;
        },
        searchForm: function () {
            ///<summary>获取json格式搜索参数</summary>
            var strJson = "{";
            $("#divSearch input").each(function () {
                /* if ($(this).attr("class")) {
                     if ($(this).attr("class").indexOf("combotree-f") > 0) {
                         if ($(this).combo("options").multiple)
                             strJson += "\"" + $(this).attr("comboname") + "\":\"" + $.trim($(this).combotree("getValues")) + "\",";
                         else
                             strJson += "\"" + $(this).attr("comboname") + "\":\"" + $.trim($(this).combotree("getValue")) + "\",";
                     }
                     else if ($(this).attr("class").indexOf("combobox-f ") > 0)
                         strJson += "\"" + $(this).attr("comboname") + "\":\"" + $.trim($(this).combotree("getValue")) + "\",";
 
                     else
                         strJson += "\"" + $(this).attr("id") + "\":\"" + $.trim($(this).val()) + "\",";
 
                 }
                 else*/
                strJson += "\"" + $(this).attr("name") + "\":\"" + $.trim($(this).val()) + "\",";
            });

            $("#divSearch select").each(function () {
                /*if ($(this).attr("class")) {
                    if ($(this).attr("class").indexOf("combobox-f ") > 0)
                        strJson += "\"" + $(this).attr("comboname") + "\":\"" + $.trim($(this).combotree("getValue")) + "\",";
                    else
                        strJson += "\"" + $(this).attr("name") + "\":\"" + $.trim($(this).val()) + "\",";

                }
                else*/
                strJson += "\"" + $(this).attr("name") + "\":\"" + $.trim($(this).val()) + "\",";
            });
            if (strJson.length > 1) strJson = strJson.substr(0, strJson.length - 1);
            strJson += "}";
            var jsonSearch = $.parseJSON(strJson);
            return jsonSearch;
        },
        resetFormSearch: function () {
            ///<summary>重置搜索表单</summary>
            // $('#form1')[0].reset();
        },
        reload: function (formSearch) {
            ///<summary>刷新列表，不带搜索参数，当前页数重置1</summary>
            if (!formSearch) formSearch = {};
            this.grid("options").queryParams = formSearch;
            this.grid("load");
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
        },
        insertRow: function (rowJson) {
            var obj = this;
            var row = $(this.grdidId).datagrid('getSelected');
            if (row && row != undefined) {
                obj.RowIndex = $(this.grdidId).datagrid('getRowIndex', row);
            }

            $(this.grdidId).datagrid('insertRow', {
                index: obj.RowIndex,
                row: rowJson
            });

            obj.PkId--;
            //alert(this.grdidId);
            //alert(this.grdidId);
            //$(this.grdidId).datagrid('selectRow', obj.RowIndex);

        },
        delRow: function () {
            if ($(this.grdidId).datagrid("getRows") == "") {
                return false;
            }
            var selectrow = $(this.grdidId).datagrid("getSelected");
            if (!selectrow)
                return false;
            var nowIndex = $(this.grdidId).datagrid("getRowIndex", selectrow);
            $(this.grdidId).datagrid('deleteRow', nowIndex);
            if (nowIndex > 0) {
                $(this.grdidId).datagrid('selectRow', nowIndex - 1);
            }
            //else {
            //    $(this.grdidId).datagrid('selectRow', 0);
            //}
            return true;
        }
    };

})();


