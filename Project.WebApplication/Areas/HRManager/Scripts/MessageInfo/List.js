﻿
var pro = pro || {};
(function () {
    pro.MessageInfo = pro.MessageInfo || {};
    pro.MessageInfo.ListPage = pro.MessageInfo.ListPage || {};
    pro.MessageInfo.ListPage = {
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
                url: '/HRManager/MessageInfo/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', hidden: true, width: 100 },
         { field: 'MesTitle', title: '标题', width: 200 },
         { field: 'MesContent', title: '内容', width: 280 },
         //{ field: 'ReceiveUserCode', title: '接收人', width: 100 },
         //{ field: 'IsAll', title: '是否所有人', width: 100 },
         //{ field: 'CreatorUserCode', title: '发送人', width: 100 },
         {
             field: 'InfoType', title: '类型', width: 100, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case 1:
                         ret = '生日提醒';
                         break;
                     case 2:
                         ret = '退休提醒';
                         break;

                     case 3:
                         ret = '合同过期提醒';
                         break;

                     case 4:
                         ret = '合同到期提醒';
                         break;
                     case 5:
                         ret = '其它提醒';
                         break;
                 }
                 return ret;
             }
         },

         { field: 'CreatorUserName', title: '发送人', width: 100 },
         {
             field: 'IsRead', title: '是否已读', width: 100, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case true:
                         ret = '已读'
                         break;
                     case false:
                         ret = '未读'
                         break;
                 }
                 return ret;
             }
         },
         //{ field: 'CreationTime', title: '', width: 100 },
         //{ field: 'LastModificationTime', title: '', width: 100 },
         //{ field: 'LastModifierUserCode', title: '', width: 100 },
         //{ field: 'DeleterUserCode', title: '', width: 100 },
         //{ field: 'DeletionTime', title: '', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/MessageInfo/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/MessageInfo/Hd?PkId=" + PkId, "编辑" + PkId);
            });
            $("#btnView").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/MessageInfo/Hd?PkId=" + PkId + "&View=true", "提醒详情");
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
                        url: "/HRManager/MessageInfo/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.MessageInfo.ListPage.initPage();
});


