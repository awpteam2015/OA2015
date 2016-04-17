
var pro = pro || {};
(function () {
    pro.ContinEducation = pro.ContinEducation || {};
    pro.ContinEducation.ListPage = pro.ContinEducation.ListPage || {};
    pro.ContinEducation.ListPage = {
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
                url: '/HRManager/ContinEducation/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'EmployeeID', title: '用户ID', width: 100 },
         { field: 'EmployeeCode', title: '用户编号', width: 100 },
         { field: 'DepartmentCode', title: '部门编号', width: 100 },
         { field: 'CreditType', title: '学分类型', width: 100 },
         { field: 'CreditTypeName', title: '学分类型名称', width: 100 },
         { field: 'Score', title: '分数', width: 100 },
         { field: 'GetTime', title: '时间', width: 100 },
         { field: 'CreatorUserCode', title: '', width: 100 },
         { field: 'CreattorUserName', title: '', width: 100 },
         { field: 'CreationTime', title: '', width: 100 },
         { field: 'LastModificationTime', title: '', width: 100 },
         { field: 'LastModifierUserCode', title: '', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/HRManager/ContinEducation/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/ContinEducation/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/ContinEducation/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.ContinEducation.ListPage.initPage();
});


