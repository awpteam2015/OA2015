﻿
var pro = pro || {};
(function () {
    pro.Department = pro.Department || {};
    pro.Department.ListPage = pro.Department.ListPage || {};
    pro.Department.ListPage = {
        initPage: function () {
            var gridObj =
            $('#datagrid').datagrid({
                url: '/PermissionManager/Department/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'DepartmentCode', title: '部门编码', width: 100 },
         { field: 'DepartmentName', title: '部门名称', width: 100 },
         { field: 'ParentdepartmentCode', title: '公司代码', width: 100 },
         { field: 'Remark', title: '备注', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                $.tabExtend.config.url = "/PermissionManager/Department/Hd";
                $.tabExtend.add();

            });

            $("#btnEdit").click(function () {
                if (!$.datagridExtend.isSelected()) {
                    alert("请选中要编辑的行");
                    return;
                }
                $.tabExtend.config.url = "/PermissionManager/Department/Hd?PkId=" + $.datagridExtend.getFunObject("getSelected").PkId;
                $.tabExtend.add();
            });


            $("#btnSearch").click(function () {
                $.datagridExtend.getObject().search();
            });

            $("#btnDel").click(function () {
                if (!$.datagridExtend.isSelected()) {
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/PermissionManager/Department/Delete?PkId=" + $.datagridExtend.getFunObject("getSelected").PkId
                    }).done(
                    function (dataresult, data) {
                        $.alertExtend.info();
                    }
                    ).fail(
                    function (errordetails, errormessage) {
                        $.alertExtend.error();
                    }
                    );
                });
            });

            $("#btnRefresh").click(function () {
                $.datagridExtend.getObject().refresh();
            });
        }
    };
})();



$(function () {
    pro.Department.ListPage.initPage();
});

