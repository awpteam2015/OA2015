
var pro = pro || {};
(function () {
    pro.HolidayDetail = pro.HolidayDetail || {};
    pro.HolidayDetail.ListPage = pro.HolidayDetail.ListPage || {};
    pro.HolidayDetail.ListPage = {
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
                url: '/SystemSetManager/HolidayDetail/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: 'PkId', hidden: true, width: 100 },
         { field: 'HolidayName', title: '节假日名称', width: 100 },
         {
             field: 'HolidayDateType', title: '类型', width: 100, formatter: function (val) {
                 var retStr = "";
                 switch (val) {
                     case 0:
                         retStr = "公休日";
                         break;
                     case 1:
                         retStr = "法定节假日";
                         break;
                 }
                 return retStr;
             }
         },
         { field: 'HolidayDate', title: '日期', width: 100 },
         { field: 'CreatorUserName', title: '操作人姓名', width: 100 },
         { field: 'CreateTime', title: '创建时间', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
         { field: 'Remark', title: '备注', width: 200 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/SystemSetManager/HolidayDetail/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/SystemSetManager/HolidayDetail/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/SystemSetManager/HolidayDetail/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.HolidayDetail.ListPage.initPage();
});


