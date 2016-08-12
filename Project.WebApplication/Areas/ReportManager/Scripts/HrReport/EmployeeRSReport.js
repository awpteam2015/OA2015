
var pro = pro || {};
(function () {
    pro.HrReport = pro.HrReport || {};
    pro.HrReport.EmployeeRSReportPage = pro.HrReport.EmployeeRSReportPage || {};
    pro.HrReport.EmployeeRSReportPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false)
            };
        },
        initPage: function () {
            var initObj = this.init();
            var gridObj = initObj.gridObj;
            gridObj.grid({
                url: '/ReportManager/HrReport/GetEmployeeRsReport',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
                 { field: 'DepartmentName', title: '单位名称', width: 200, rowspan: 2 },
                 { title: '专业技术人员', width: 100, colspan: 11 },
                 { field: 'Glrysl', title: '管理人员', width: 100, rowspan: 2 },
                  { title: '工勤人员', width: 100, colspan: 11 },
                  { field: 'Ryzs', title: '实有在编人数', width: 100, rowspan: 2 }
                ], [
                    { field: 'Zyry3J', title: '三级', width: 100, rowspan: 1 },
                    { field: 'Zyry4J', title: '四级', width: 100, rowspan: 1 },
                    { field: 'Zyry5J', title: '五级', width: 100 },
                    { field: 'Zyry6J', title: '六级', width: 100 },
                    { field: 'Zyry7J', title: '七级', width: 100 },
                    { field: 'Zyry8J', title: '八级', width: 100 },
                    { field: 'Zyry9J', title: '九级', width: 100 },
                    { field: 'Zyry10J', title: '十级', width: 100 },
                    { field: 'Zyry11J', title: '十一级', width: 100 },
                    { field: 'Zyry12J', title: '十二级', width: 100 },
                    { field: 'Zyry13J', title: '十三级', width: 100 },
                    { field: 'Gqry3J', title: '三级', width: 100, rowspan: 1 },
                    { field: 'Gqry4J', title: '四级', width: 100, rowspan: 1 },
                    { field: 'Gqry5J', title: '五级', width: 100 },
                    { field: 'Gqry6J', title: '六级', width: 100 },
                    { field: 'Gqry7J', title: '七级', width: 100 },
                    { field: 'Gqry8J', title: '八级', width: 100 },
                    { field: 'Gqry9J', title: '九级', width: 100 },
                    { field: 'Gqry10J', title: '十级', width: 100 },
                    { field: 'Gqry11J', title: '十一级', width: 100 },
                    { field: 'Gqry12J', title: '十二级', width: 100 },
                    { field: 'Gqry13J', title: '十三级', width: 100 },
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
                        url: "/ReportManager/HrReport/ExportRsEmployeeExcel?ReportType=" + $("#ReportType").val() + "&" + urlParam
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
    pro.HrReport.EmployeeRSReportPage.initPage();
});


