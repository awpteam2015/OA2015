
var pro = pro || {};
(function () {
    pro.GroupEmployee = pro.GroupEmployee || {};
    pro.GroupEmployee.ListPage = pro.GroupEmployee.ListPage || {};
    pro.GroupEmployee.ListPage = {      
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false),
                gridObjdetail: new pro.GridBase("#datagriddetail", false),
                VarsObj: {
                    selectedGrioupCode: ''
                },
            };
        },
        initPage: function () {
            var initObj = this.init();
            var tabObj = initObj.tabObj;
            var gridObj = initObj.gridObj;
            var gridObjdetail = initObj.gridObjdetail;
            var varsObj = initObj.VarsObj;
            initObj.gridObj.grid({
                url: '/HRManager/Group/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'GroupCode', title: '组编号', width: 100 },
         { field: 'GroupName', title: '组名称', width: 100 },
         { field: 'Remark', title: '备注', width: 100 }
                ]],
                onClickRow: function (index, row) {
                    VarsObj.selectedGrioupCode = row.GroupCode;
                    gridObjdetail.reload({ GroupCode: row.GroupCode });
                },
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
              );
            gridObjdetail.grid({
                url: '/HRManager/GroupEmployee/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'GroupCode', title: '组编号', width: 100 },
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'CreatorUserName', title: '操作员', width: 100 },
         { field: 'CreateTime', title: '创建时间', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/GroupEmployee/Hd", "新增");
            });

            $("#btnDel").click(function () {
                if (!gridObjdetail.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/HRManager/GroupEmployee/Delete?PkId=" + gridObj.getSelectedRow().PkId
                    }).done(
                    function (dataresult, data) {
                        $.alertExtend.info();
                        gridObjdetail.search();
                    }
                    ).fail(
                    function (errordetails, errormessage) {
                        $.alertExtend.error();
                    }
                    );
                });
            });

            $("#btnSearchGroup").click(function () {
                gridObj.search();
            });

            $("#btnRefresh").click(function () {
                gridObjdetail.refresh();
            });
            $("#btnReload").click(function () { gridObj.refresh(); })
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.GroupEmployee.ListPage.initPage();
});


