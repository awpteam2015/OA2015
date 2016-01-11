
var pro = pro || {};
(function () {
    pro.EmployeeInfo = pro.EmployeeInfo || {};
    pro.EmployeeInfo.ListPage = pro.EmployeeInfo.ListPage || {};
    pro.EmployeeInfo.ListPage = {
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
                url: '/HRManager/EmployeeInfo/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', width: 100 },
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'EmployeeName', title: '员工名称', width: 100 },
         { field: 'DepartmentCode', title: '所属部门', width: 100 },
         { field: 'JobName', title: '工号', width: 100 },
         { field: 'PayCode', title: '中文简拼', width: 100 },
         {
             field: 'Sex', title: '姓别', width: 100, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case 0:
                         ret = '女'
                         break;
                     case 1:
                         ret = '男'
                         break;
                     default:
                         ret = '未知'
                         break;
                 }
                 return ret;
             }
         },
         { field: 'CertNo', title: '身份证', width: 100 },
         { field: 'Birthday', title: '生日', width: 100 },
         { field: 'TechnicalTitleName', title: '技术职称', width: 100 },
         { field: 'DutiesName', title: '单位职务', width: 100 },
         { field: 'WorkStateName', title: '在职状态', width: 100 },
         { field: 'EmployeeTypeName', title: '员工类型', width: 100 },
         //{ field: 'HomeAddress', title: '家庭地址', width: 100 },
         //{ field: 'MobileNO', title: '手机号', width: 100 },
         //{ field: 'ImageUrl', title: '图片地址', width: 100 },
         { field: 'Sort', title: '排序', width: 100 },
         {
             field: 'State', title: '状态', width: 100, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case 0:
                         ret = '停用'
                         break;
                     case 1:
                         ret = '启用'
                         break;
                     default:
                         ret = '停用'
                         break;
                 }
                 return ret;
             }
         },
         //{ field: 'Remark', title: '备注', width: 100 },
         //{ field: 'CreatorUserCode', title: '操作员', width: 100 },
         { field: 'CreatorUserName', title: '操作员名称', width: 100 },
         { field: 'CreateTime', title: '创建时间', width: 100 },
         { field: 'LastModificationTime', title: '修改时间', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/EmployeeInfo/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/EmployeeInfo/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/EmployeeInfo/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.EmployeeInfo.ListPage.initPage();
});


