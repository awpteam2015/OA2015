
var pro = pro || {};
(function () {
    pro.EmployeeYearDetail = pro.EmployeeYearDetail || {};
    pro.EmployeeYearDetail.ListPage = pro.EmployeeYearDetail.ListPage || {};
    pro.EmployeeYearDetail.ListPage = {
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
                url: '/HRManager/EmployeeYearDetail/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', hidden: true, width: 100 },
          {
              field: 'DepartmentCode', title: '部门', width: 100, formatter: function (value, row) {
                  return row.DepartmentEntity.DepartmentName;
              }
          },
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'EmployeeName', title: '员工', width: 100 },
         {
             field: 'UseType', title: '类型', width: 100,
             formatter: function (val) {
                 var retStr = "";
                 switch (val) {
                     case 0:
                         retStr = "年假添加";
                         break;
                     case 1:
                         retStr = "使用登记";
                         break;
                 }
                 return retStr;
             }
         },
         { field: 'BeginDate', title: '开始日期', width: 100 },
         { field: 'EndDate', title: '结束日期', width: 100 },
         { field: 'BeforeUseCount', title: '使用前天数', width: 100 },
         { field: 'UseCount', title: '使用天数', width: 100 },
         { field: 'LeftCount', title: '年休余数', width: 100 }
         //,
         //{ field: 'Remark', title: '', width: 100 },
         //{ field: 'CreatorUserCode', title: '', width: 100 },
         //{ field: 'CreatorUserName', title: '', width: 100 },
         //{ field: 'CreationTime', title: '', width: 100 },
         //{ field: 'LastModificationTime', title: '', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/EmployeeYearDetail/Hd", "新增");
            });

            $('#DepartmentCode').combotree({
                required: true,
                editable: false,
                multiple: true,//支持多选
                cascadeCheck: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            }).combotree({
                onChange: function (newValue, oldValue) {
                    $('#EmployeeCode').combobox({
                        required: true,
                        editable: false,
                        valueField: 'EmployeeCode',
                        textField: 'EmployeeName',
                        url: '/HRManager/EmployeeInfo/GetAllList?DepartmentCode=' + $('#DepartmentCode').combotree("getValues")
                    });
                }
            });
            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/EmployeeYearDetail/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/EmployeeYearDetail/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.EmployeeYearDetail.ListPage.initPage();
});


