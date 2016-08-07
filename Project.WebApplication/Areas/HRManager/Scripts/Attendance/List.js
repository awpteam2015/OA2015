﻿
var pro = pro || {};
(function () {
    pro.Attendance = pro.Attendance || {};
    pro.Attendance.ListPage = pro.Attendance.ListPage || {};
    pro.Attendance.ListPage = {
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
                url: '/HRManager/Attendance/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         //{ field: 'PkId', title: '序号', width: 100 },
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'EmployeeName', title: '员工姓名', width: 100 },
         { field: 'DepartmentName', title: '部门', width: 100 },
         { field: 'State', title: '状态', width: 100 },
         { field: 'Date', title: '考勤日期', width: 100 },
         { field: 'CreatorUserCode', title: '创建人', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'LastModifierUserCode', title: '修改人', width: 100 },
         { field: 'LastModificationTime', title: '最后修改时间', width: 100 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            pro.DepartmentControl.init();

            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=KQZT"
                //,data: JSON.stringify(postData)
            }).done(
             function (dataresult, data) {
                 var html = '<option value="">全部</option>';

                 $.each(dataresult, function (i, item) {
                     html += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
                 });
                 $("#State").html(html);
             }
         ).fail(
          function (errordetails, errormessage) {
              //  $.alertExtend.error();
          }
         );


            $('#btnExport').click(function () {

                if ($("#Attr_ExportDate").val() == "" || $('#DepartmentCode').combotree("getValue") == "") {
                    $.alertExtend.infoOp("请选择导出部门及导出月份！");
                    return false;
                }

                if ($('#DepartmentCode').combotree("getValue").length != 5) {
                    $.alertExtend.infoOp("请选择科室（部门最后一级）！");
                    return false;
                }


                var urlParam = pro.commonKit.parseParam(gridObj.searchForm());
                location.href = "/HRManager/Attendance/ExportReport?" + urlParam;
            });


            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/Attendance/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/Attendance/Hd?PkId=" + PkId, "编辑" + PkId);
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
                        url: "/HRManager/Attendance/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.Attendance.ListPage.initPage();
});


