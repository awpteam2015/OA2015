
var pro = pro || {};
(function () {
    pro.Function = pro.Function || {};
    pro.Function.ListPage = pro.Function.ListPage || {};
    pro.Function.ListPage = {
        initPage: function () {
           var gridObj = new pro.GridBase("#datagrid", false);
            gridObj.grid({
                url: '/PermissionManager/Function/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'FunctionnName', title: '模块名称', width: 100 },
         { field: 'ModuleId', title: '模块ID', width: 100 },
         { field: 'FunctionUrl', title: '模块路径', width: 100 },
         { field: 'Area', title: '', width: 100 },
         { field: 'Controller', title: '', width: 100 },
         { field: 'Action', title: '', width: 100 },
         { field: 'IsDisplayOnMenu', title: '是否在菜单上显示1是 0不是', width: 100 },
         { field: 'RankId', title: '顺序', width: 100 },
         { field: 'Remark', title: '备注', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                $.tabExtend.config.url = "/PermissionManager/Function/Hd";
                $.tabExtend.add();

            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    alert("请选中要编辑的行");
                    return;
                }
                $.tabExtend.config.url = "/PermissionManager/Function/Hd?PkId=" + gridObj.getSelectedRow().PkId;
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
                        url: "/PermissionManager/Function/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.Function.ListPage.initPage();
});


