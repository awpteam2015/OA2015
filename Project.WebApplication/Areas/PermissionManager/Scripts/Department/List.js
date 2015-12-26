
var pro = pro || {};
(function () {
    pro.Department = pro.Department || {};
    pro.Department.ListPage = pro.Department.ListPage || {};
    pro.Department.ListPage = {
        initPage: function () {
            var gridObj = new pro.GridBase("#datagrid", true);

            gridObj.grid({
                url: '/PermissionManager/Department/GetList',
                idField: "DepartmentCode",
                treeField: "DepartmentCode",
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'DepartmentCode', title: '部门编码', width: 300 },
         { field: 'DepartmentName', title: '部门名称', width: 100 },
         { field: 'ParentDepartmentCode', title: '上级部门编码', width: 100 },
         { field: 'Remark', title: '备注', width: 100 }
                ]]
            }
               );

            $("#btnAdd").click(function () {
                $.tabExtend.config.url = "/PermissionManager/Department/Hd";
                $.tabExtend.add();

            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.info("请选中要编辑的行");
                    return;
                }
                $.tabExtend.config.url = "/PermissionManager/Department/Hd?PkId=" + gridObj.getSelectedRow().PkId;
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
                        url: "/PermissionManager/Department/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
        }
    };
})();



$(function () {
    pro.Department.ListPage.initPage();
});


