
var pro = pro || {};
(function () {
    pro.FunctionDetail = pro.FunctionDetail || {};
    pro.FunctionDetail.ListPage = pro.FunctionDetail.ListPage || {};
    pro.FunctionDetail.ListPage = {
        initPage: function () {
           var gridObj = new pro.GridBase("#datagrid", false);
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
                $.tabExtend.config.url = "/PermissionManager/FunctionDetail/Hd";
                $.tabExtend.add();

            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    alert("请选中要编辑的行");
                    return;
                }
                $.tabExtend.config.url = "/PermissionManager/FunctionDetail/Hd?PkId=" + gridObj.getSelectedRow().PkId;
                $.tabExtend.add();
            });


            $("#btnSearch").click(function () {
                gridObj.search();
            });

            $("#btnDel").click(function () {
                if (!gridObj.isSelected()) {
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/PermissionManager/FunctionDetail/Delete?PkId=" + gridObj.getSelectedRow().PkId
                    }).done(
                    function (dataresult, data) {
                        $.alertExtend.info();
                        gridObj.getObject().search();
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
        }
    };
})();



$(function () {
    pro.FunctionDetail.ListPage.initPage();
});


