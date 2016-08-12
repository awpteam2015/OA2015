
var pro = pro || {};
(function () {
    pro.HrReport = pro.HrReport || {};
    pro.HrReport.EmployeeInOutReportPage = pro.HrReport.EmployeeInOutReportPage || {};
    pro.HrReport.EmployeeInOutReportPage = {
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
                url: '/ReportManager/HrReport/GetEmployeeInOutReport',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'ParnetDepartmentName', title: '单位名称', width: 200 },
         { field: 'Zjxj', title: '增加小计', width: 100 },
         { field: 'Bxdl', title: '本系统调入', width: 100 },
         { field: 'Ly', title: '新录用', width: 100 },
         { field: 'Dl', title: '调入', width: 100 },
         { field: 'Jsxj', title: '减少小计', width: 100 },
         { field: 'Tx', title: '退休', width: 100 },
         { field: 'Cz', title: '调出或辞职', width: 100 },
         { field: 'Bxttc', title: '(本系统)调出', width: 100 }
        /* { field: 'EmployeeName', title: '员工名称', width: 100 },
         { field: 'CertNo', title: '身份证', width: 100 },
         {
             field: 'Birthday', title: '生日', width: 100
         },
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
         { field: 'EmployeeTypeName', title: '员工类型', width: 100 },

         { field: 'DepartmentName', title: '部门名称', width: 100 },
         { field: 'InDepartmentName', title: '部门名称（修改后）', width: 100 },
         { field: 'WorkStateName', title: '在职状态', width: 100 },
         { field: 'InWorkStateName', title: '在职状态（修改后）', width: 100 },
         {
             field: 'IsDeleted', title: '删除状态', width: 100, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case 0:
                         ret = '正常'
                         break;
                     case 1:
                         ret = '删除'
                         break;
                 }
                 return ret;
             }
         },
         {
             field: 'InOrOut', title: '进出类型', width: 100, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case 0:
                         ret = '进'
                         break;
                     case 1:
                         ret = '出'
                         break;
                 }
                 return ret;
             }
         },*/

                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageAttendanceReport: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            pro.DepartmentControl.init();

            $("#btnSearch").click(function () {
                gridObj.search();
            });
            $('#btnExport').click(function () {
                var urlParam = pro.commonKit.parseParam(gridObj.searchForm());
                // location.href = "/ReportManager/HrReport/ExportEmployeeExcel?" + urlParam;
                $.messager.confirm("确认操作", "是否确认导出", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/ReportManager/HrReport/ExportInOrOutEmployeeExcel?ReportType=" + $("#ReportType").val() + "&" + urlParam
                    }).done(
                    function (dataresult, data) {
                        //
                        if (data && data.success) {
                            location.href = data.targeturl;
                        }
                        //$.alertExtend.info();
                        //gridObj.search();
                    }
                    ).fail(
                    function (errordetails, errormessage) {
                        $.alertExtend.error();
                    }
                    );
                });
            });
        
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.HrReport.EmployeeInOutReportPage.initPage();
});


