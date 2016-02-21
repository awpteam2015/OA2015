
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
                url: '/ReportManager/HrReport/GetAttendanceReport2',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         //{ field: 'EmployeeCode', title: '工号', width: 100 },
         { field: 'DepartmentName', title: '部门', width: 100 },
         { field: 'EmployeeNum', title: '员工数', width: 100 },
         { field: 'WordkDays', title: '在岗天数', width: 100 },
         { field: 'NotWordkDays', title: '缺勤天数', width: 100 }
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
    pro.HrReport.AttendanceReportPage.initPage();
});


