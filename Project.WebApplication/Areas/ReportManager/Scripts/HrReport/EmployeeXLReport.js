
var pro = pro || {};
(function () {
    pro.HrReport = pro.HrReport || {};
    pro.HrReport.EmployeeDYReport = pro.HrReport.EmployeeDYReport || {};
    pro.HrReport.EmployeeDYReport = {
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
                url: '/ReportManager/HrReport/GetEmployeXLReport',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'EmployeeName', title: '员工名称', width: 100 },
         { field: 'CertNo', title: '身份证', width: 130 },
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
         { field: 'WorkStateName', title: '在职状态', width: 100 },
         { field: 'EducationName', title: '学历', width: 120 },
         { field: 'JoinCommy', title: '入党时间', width: 120 }

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

            //$('#btnExport').click(function () {
            //    var urlParam = pro.commonKit.parseParam(gridObj.searchForm());
            //    location.href = "/ReportManager/HrReport/ExportReport1?" + urlParam;
            //});

        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.HrReport.EmployeeDYReport.initPage();
});


