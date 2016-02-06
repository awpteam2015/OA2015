
var pro = pro || {};
(function () {
    pro.AttendanceUploadRecord = pro.AttendanceUploadRecord || {};
    pro.AttendanceUploadRecord.ListPage = pro.AttendanceUploadRecord.ListPage || {};
    pro.AttendanceUploadRecord.ListPage = {
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
                url: '/HRManager/AttendanceUploadRecord/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'DepartmentName', title: '部门', width: 100 },
         { field: 'Att_Date', title: '考勤月份', width: 100 },
         //{ field: 'CreatorUserCode', title: '创建人编码', width: 100 },
         //{ field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'FileName', title: '文件', width: 500 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );
            pro.DepartmentControl.init();

            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/AttendanceUploadRecord/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/AttendanceUploadRecord/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/AttendanceUploadRecord/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.AttendanceUploadRecord.ListPage.initPage();
});


