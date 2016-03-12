
var pro = pro || {};
(function () {
    pro.GoAbroad = pro.GoAbroad || {};
    pro.GoAbroad.ListPage = pro.GoAbroad.ListPage || {};
    pro.GoAbroad.ListPage = {
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
                url: '/HRManager/GoAbroad/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', hidden: true, width: 100 },
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'EmployeeName', title: '员工姓名', width: 100 },
         { field: 'DepartmentName', title: '员工部门', width: 100 },
         { field: 'Country', title: '出访国家', width: 100 },
         { field: 'BeginDate', title: '出国日期', width: 120 },
         { field: 'EndDate', title: '回国日期', width: 120 },
         { field: 'DaySum', title: '出国天数', width: 100 },
         //{
         //    field: 'Reason', title: '事由', width: 100, formatter: function (val) {
         //        alert();
         //        Base64.decode(val);
         //    }
         //},
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );
            //$('#DepartmentCode').combotree({
            //    required: true,
            //    editable: false,
            //    multiple: true,//支持多选
            //    cascadeCheck: false,
            //    valueField: 'DepartmentCode',
            //    textField: 'DepartmentName',
            //    url: '/PermissionManager/Department/GetList_Combotree',
            //    onLoadSuccess: function (node, data) {
            //        $("#DepartmentCode").combotree('setValue', "0");
            //    }
            //})
            pro.DepartmentControl.init();
            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/GoAbroad/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/GoAbroad/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/GoAbroad/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.GoAbroad.ListPage.initPage();
});


