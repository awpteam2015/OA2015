
var pro = pro || {};
(function () {
    pro.Department = pro.Department || {};
    pro.Department.ListPage = pro.Department.ListPage || {};
    pro.Department.ListPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", true)
            };
        },
        initPage: function () {
            var initObj = this.init();
            var tabObj = initObj.tabObj;
            var gridObj = initObj.gridObj;
            gridObj.grid({
                    url: '/PermissionManager/Department/GetList',
                    idField: "DepartmentCode",
                    treeField: "DepartmentCode",
                    fitColumns: false,
                    nowrap: false,
                    rownumbers: true, //行号
                    singleSelect: true,
                    columns: [
                        [
                            { field: 'DepartmentCode', title: '部门编码', width: 300 },
                            { field: 'DepartmentName', title: '部门名称', width: 200 },
                            { field: 'ParentDepartmentCode', title: '上级部门编码', width: 100 },
                            //{
                            //    field: 'DepartmentType', title: '类型', width: 100, formatter: function (value, row, index) {
                            //        var ret = "";
                            //        switch (value) {
                            //            case 0:
                            //                ret = '机构'
                            //                break;
                            //            case 1:
                            //                ret = '科室'
                            //                break;
                            //        }
                            //        return ret;               
                            //    }
                            //},
                            { field: 'Remark', title: '备注', width: 200 }
                        ]
                    ]
                }
            );

            $("#btnAdd").click(function () {
                tabObj.add("/PermissionManager/Department/Hd","新增");

            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.info("请选中要编辑的行");
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/PermissionManager/Department/Hd?PkId=" + PkId, "编辑" + PkId);
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
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.Department.ListPage.initPage();
});


