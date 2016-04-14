
var pro = pro || {};
(function () {
    pro.FunctionDetail = pro.FunctionDetail || {};
    pro.FunctionDetail.ListPage = pro.FunctionDetail.ListPage || {};
    pro.FunctionDetail.ListPage = {
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
                url: '/PermissionManager/FunctionDetail/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'FunctionDetailName', title: '功能名称', width: 100 },
         { field: 'FunctionDetailCode', title: '功能代号对应页面需要控制的按钮Id', width: 100 },
         { field: 'FunctionId', title: '模块ID', width: 100 },
         { field: 'Area', title: '', width: 100 },
         { field: 'Controller', title: '', width: 100 },
         { field: 'Action', title: '', width: 100 },
         { field: 'CreatorUserCode', title: '创建人', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'LastModifierUserCode', title: '修改人', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/PermissionManager/FunctionDetail/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var functionDetailName = gridObj.getSelectedRow().FunctionDetailName;
                alert(functionDetailName);
                tabObj.add("/PermissionManager/FunctionDetail/Hd?PkId=" + PkId, "编辑" + functionDetailName);
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
                        url: "/PermissionManager/FunctionDetail/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.FunctionDetail.ListPage.initPage();
});


