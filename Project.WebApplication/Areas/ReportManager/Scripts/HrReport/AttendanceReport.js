
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
                { field: 'EmployeeCode', title: '员工编号', width: 100 },
                { field: 'EmployeeName', title: '员工姓名', width: 100 },
                { field: 'DepartmentName', title: '部门', width: 100 },
                { field: 'RiDays', title: '日班天数', width: 100 },
                { field: 'YeDays', title: '夜班天数', width: 100 },
                { field: 'GongDays', title: '公休天数', width: 100 },
                { field: 'ZhiDays', title: '值班天数', width: 100 },
                { field: 'QueDays', title: '缺勤天数', width: 100 }
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
                if ($("#Date").val() == "" || $('#DepartmentCode').combotree("getValue") == "") {
                    $.alertExtend.infoOp("请选择导出部门及导出月份！");
                    return false;
                }

                if ($('#DepartmentCode').combotree("getValue").length != 5) {
                    $.alertExtend.infoOp("请选择科室（部门最后一级）！");
                    return false;
                }

                var urlParam = pro.commonKit.parseParam(gridObj.searchForm());
                location.href = "/ReportManager/HrReport/ExportReport1?" + urlParam;
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


