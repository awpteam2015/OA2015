﻿
var pro = pro || {};
(function () {
    pro.UserInfo = pro.UserInfo || {};
    pro.UserInfo.ListPage = pro.UserInfo.ListPage || {};
    pro.UserInfo.ListPage = {
        initPage: function () {

            $('#datagrid').datagrid({
                url: '/PermissionManager/UserInfo/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
                    { field: 'UserCode', title: 'UserCode', width: 100 },
                    { field: 'UserName', title: 'UserName', width: 100 },
                    { field: 'Email', title: 'Email', width: 100 },
                    { field: 'Mobile', title: 'Mobile', width: 100 },
                    { field: 'Tel', title: 'Tel', width: 100 },
                    { field: 'Mobile', title: 'Mobile', width: 100 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                $.tabExtend.config.url = "/PermissionManager/UserInfo/Hd";
                $.tabExtend.add();

            });

            $("#btnEdit").click(function () {
                if (!$.datagridExtend.isSelected()) {
                    alert("请选中要编辑的行");
                    return;
                }
                $.tabExtend.config.url = "/PermissionManager/UserInfo/Hd?PkId=" + $.datagridExtend.getObject("getSelected").PkId;
                $.tabExtend.add();
            });


        }
    };
})();



$(function () {
    pro.UserInfo.ListPage.initPage();
});