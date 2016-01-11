
var pro = pro || {};
(function () {
    pro.LearningExperiences = pro.LearningExperiences || {};
    pro.LearningExperiences.ListPage = pro.LearningExperiences.ListPage || {};
    pro.LearningExperiences.ListPage = {
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
                url: '/HRManager/LearningExperiences/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'EmployeeCode', title: '', width: 100 },
         { field: 'DepartmentCode', title: '', width: 100 },
         { field: 'ProfessionCode', title: '数据字典维护', width: 100 },
         { field: 'School', title: '', width: 100 },
         { field: 'Degree', title: '手动输入', width: 100 },
         { field: 'Education', title: '手动输入', width: 100 },
         { field: 'VerifyPersone', title: '', width: 100 },
         { field: 'Reward', title: '', width: 100 },
         { field: 'Certificate', title: '', width: 100 },
         { field: 'Remark', title: '', width: 100 },
         { field: 'CreatorUserCode', title: '', width: 100 },
         { field: 'CreatorUserName', title: '', width: 100 },
         { field: 'CreateTime', title: '', width: 100 },
         { field: 'LastModificationTime', title: '', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/HRManager/LearningExperiences/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/LearningExperiences/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/LearningExperiences/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.LearningExperiences.ListPage.initPage();
});


