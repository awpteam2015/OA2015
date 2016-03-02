
var pro = pro || {};
(function () {
    pro.WorkExperience = pro.WorkExperience || {};
    pro.WorkExperience.ListPage = pro.WorkExperience.ListPage || {};
    pro.WorkExperience.ListPage = {
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
                url: '/HRManager/WorkExperience/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'DepartmentCode', title: '所属部门', width: 100 },
         { field: 'WorkCompany', title: '工作单位', width: 100 },
         { field: 'Duties', title: '职务', width: 100 },
         { field: 'BeginDate', title: '开始日期', width: 100 },
         { field: 'EndDate', title: '结束日期', width: 100 },
         { field: 'WorkContent', title: '工作内容', width: 100 },
         { field: 'LeaveReason', title: '离职原因', width: 100 },
         { field: 'Remark', title: '备注', width: 100 },
         { field: 'CreatorUserCode', title: '操作人', width: 100 },
         { field: 'CreatorUserName', title: '操作人名称', width: 100 },
         { field: 'CreateTime', title: '创建时间', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/HRManager/WorkExperience/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/WorkExperience/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/WorkExperience/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.WorkExperience.ListPage.initPage();
});


