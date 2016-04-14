
var pro = pro || {};
(function () {
    pro.Function = pro.Function || {};
    pro.Function.ListPage = pro.Function.ListPage || {};
    pro.Function.ListPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false)
            };
        },
        initPage: function () {
            var initObj = this.init();
            var tabObj = initObj.tabObj;
            var gridObj = initObj.gridObj;
            gridObj.grid({
                url: '/PermissionManager/Function/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                frozenColumns: [[
                     { field: 'PkId', title: '', width: 50 },
         { field: 'FunctionnName', title: '模块名称', width: 200 },
         {
             field: 'ModuleId', title: '模块ID', width: 100, formatter: function (value, row) {
                 return row.ModuleEntity.ModuleName;
             }
         }
                ]],
                columns: [[

         { field: 'FunctionUrl', title: '模块路径', width: 400 },
         //{ field: 'Area', title: '', width: 100 },
         //{ field: 'Controller', title: '', width: 100 },
         //{ field: 'Action', title: '', width: 100 },
         {
             field: 'IsDisplayOnMenu', title: '是否显示', width: 100, formatter: function (value, row) {
                 switch (value) {
                     case 0:
                         return '否';
                     case 1:
                         return '是';
                     default:
                         return '否';
                 }
             }
         },
         { field: 'RankId', title: '顺序', width: 100 },
         { field: 'Remark', title: '备注', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/PermissionManager/Function/Hd?ModuleId=" + $('#ModuleId').combobox("getValue"), "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var functionnName = gridObj.getSelectedRow().FunctionnName;
                tabObj.add("/PermissionManager/Function/Hd?PkId=" + PkId, "编辑" + functionnName);
            });


            $("#btnSearch").click(function () {
                gridObj.search();
            });

            $("#btnDel").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/PermissionManager/Function/Delete?PkId=" + gridObj.getSelectedRow().PkId
                    }).done(
                    function (dataresult, data) {
                        $.alertExtend.info();
                        gridObj.search();
                    }
                    ).fail(
                    function (errordetails, errormessage) {
                        $.alertExtend.error();
                    }
                    );
                });
            });

            $("#btnRefresh").click(function () {
                gridObj.refresh();
            });



            $('#ModuleId').combobox({
                valueField: 'PkId',
                textField: 'ModuleName',
                url: '/PermissionManager/Module/GetListAll_ForCombobox'
            });

        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.Function.ListPage.initPage();
});


