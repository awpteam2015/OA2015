
var pro = pro || {};
(function () {
    pro.HrReport = pro.HrReport || {};
    pro.HrReport.AttendanceReportPage = pro.HrReport.AttendanceReportPage || {};
    pro.HrReport.AttendanceReportPage = {
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
                url: '/ReportManager/HrReport/GetAttendanceReport1',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'EmployeeCode', title: '工号', width: 100 },
         { field: 'DepartmentName', title: '部门', width: 100 },
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


            $("#btnRefresh").click(function () {
                gridObj.refresh();
            });

            $('#btnExport').click(function () {
                var departmentCode = $('#DepartmentCode').combotree("getValue");
                location.href = "/ReportManager/HrReport/ExportReport1?Date=" + $("#Date").val() + "&DepartmentCode=" + departmentCode;
            });
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.HrReport.AttendanceReportPage.initPage();
});


