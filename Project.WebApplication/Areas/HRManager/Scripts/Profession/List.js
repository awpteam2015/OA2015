
var pro = pro || {};
(function () {
    pro.Profession = pro.Profession || {};
    pro.Profession.ListPage = pro.Profession.ListPage || {};
    pro.Profession.ListPage = {
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
                url: '/HRManager/Profession/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'EmployeeID', title: '', width: 100 },
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'DepartmentCode', title: '部门编号', width: 100 },
         { field: 'Title', title: '名称', width: 100 },
         { field: 'TypeName', title: '类别', width: 100 },
         { field: 'RangeName', title: '范围', width: 100 },
         { field: 'GetDate', title: '取得时间', width: 100 },
         { field: 'CerNo', title: '职称证书编号', width: 100 },
         { field: 'CreatorUserCode', title: '操作人员', width: 100 },
         { field: 'CreatorUserName', title: '操作人员名称', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
         { field: 'LastModifierUserCode', title: '修改人', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
               tabObj.add("/HRManager/Profession/Hd","新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/Profession/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/Profession/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.Profession.ListPage.initPage();
});


