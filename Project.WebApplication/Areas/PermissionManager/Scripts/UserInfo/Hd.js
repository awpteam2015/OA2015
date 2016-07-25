﻿var pro = pro || {};
(function () {
    pro.UserInfo = pro.UserInfo || {};
    pro.UserInfo.HdPage = pro.UserInfo.HdPage || {};
    pro.UserInfo.HdPage = {
        init: function () {
            return {
                gridObj: new pro.GridBase("#datagrid", true),
                gridObj2: new pro.GridBase("#datagrid2", false),
                gridObj3: new pro.GridBase("#datagrid3", false)
            };
        },
        initPage: function () {
            var initObj = this.init();
            initObj.gridObj.grid({
                idField: "DepartmentCode",
                treeField: "DepartmentCode",
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: false,
                columns: [[
                {
                    field: 'DepartmentCode', title: '部门编码', width: 300, formatter: function (value, row) {
                        var checkHtml = row.Attr_IsCheck ? 'checked="checked"' : '';
                        return '<input  name="DepartmentCode" nameFz="DepartmentCode_' + row.ParentDepartmentCode + '" type="checkbox"  value="' + row.DepartmentCode + '" ' + checkHtml + '/>' + row.DepartmentCode;
                    }
                },
                { field: 'DepartmentName', title: '部门名称', width: 300 },
                { field: 'ParentDepartmentCode', title: '上级部门编码', width: 100 },
                  {
                      field: 'Attr_UserDepartmentPkId', hidden: true, title: 'Attr_UserDepartmentPkId', width: 200, formatter: function (value, row) {
                          return '<input   name="Attr_UserDepartmentPkId_' + row.DepartmentCode + '"  type="text" value="' + row.Attr_UserDepartmentPkId + '"  />';
                      }
                  }
                ]]
            }
             );
            initObj.gridObj.grid('loadData', JSON.parse($("#DepartmentList").val()));

            initObj.gridObj2.grid({
                nowrap: false,
                rownumbers: true, //行号
                fitColumns: false,
                singleSelect: false,
                columns: [
                    [
         {
             field: 'PkId', title: '角色ID', width: 100, formatter: function (value, row) {
                 var checkHtml = row.Attr_IsCheck ? 'checked="checked"' : "";

                 return '<input name="RoleId"  value="' + value + '"   type="checkbox"  value="' + row.PkId + '" ' + checkHtml + '/>' + value;
             }
         },
         { field: 'RoleName', title: '角色名称', width: 100 },
         { field: 'Remark', title: '备注', width: 100 },
                  {
                      field: 'Attr_UserRolePkId', hidden: true, title: 'Attr_UserRolePkId', width: 100, formatter: function (value, row) {
                          return '<input  name="Attr_UserRolePkId_' + row.PkId + '" type="text" value="' + value + '" />';
                      }
                  }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40], //可以设置每页记录条数的列表,
                onLoadSuccess: function () {

                }
            }
              );
            initObj.gridObj2.grid('loadData', JSON.parse($("#RoleList").val()));

            initObj.gridObj3.grid({
                nowrap: false,
                rownumbers: true, //行号
                fitColumns: false,
                singleSelect: false,
                columns: [
                    [
         {
             field: 'PkId', title: '河流ID', width: 100, formatter: function (value, row) {
                 var checkHtml = row.Attr_IsCheck ? 'checked="checked"' : "";

                 return '<input name="RiverId"  value="' + value + '"   type="checkbox"  value="' + row.PkId + '" ' + checkHtml + '/>' + value;
             }
         },
         { field: 'RiverName', title: '河流名称', width: 100 },
           {
               field: 'Attr_RiverOwerPkId', hidden: false, title: 'Attr_RiverOwerPkId', width: 100, formatter: function (value, row) {
                   return '<input  name="Attr_RiverOwerPkId_' + row.PkId + '" type="text" value="' + value + '" />';
               }
           }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40], //可以设置每页记录条数的列表,
                onLoadSuccess: function () {

                }
            }
           );
            initObj.gridObj3.grid('loadData', JSON.parse($("#RiverList").val()));


            $("#btnAdd").click(function () {
                pro.UserInfo.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.UserInfo.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.UserInfo.ListPage.closeTab("");
            });


            $('#Duty').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=Duty',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#Duty").combobox('setValue', bindEntity['Duty']);
                    }
                }
            });


            $('#Lever').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=Lever',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#Lever").combobox('setValue', bindEntity['Lever']);
                    }
                }
            });



            if ($("#BindEntity").val()) {
                pro.bindKit.config.excludeAreaIds = "div_datagrid,div_datagrid2,div_datagrid3";
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    console.log(filedname);
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }

            //$("input[nameFz^=DepartmentCode_]").on("click", function () {
            //    var result = !($(this).attr("checked") == "checked");
            //    if (result) {
            //        $(this).removeAttr("checked");
            //        $(this).attr("checked", true);
            //    } else {
            //        $(this).removeAttr("checked");
            //    }

            //    $("input[nameFz=DepartmentCode_" + $(this).val() + "]").each(function () {
            //        //alert(result);
            //        if (result) {
            //            $(this).removeAttr("checked");
            //            $(this).attr("checked", true);
            //        } else {
            //            $(this).removeAttr("checked");
            //        }

            //    });

            //});


        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            postData.RequestEntity.Password = $("#Password").val();

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            pro.submitKit.config.columnPkidName = "DepartmentCode";
            pro.submitKit.config.columns = ["Attr_UserDepartmentPkId"];
            pro.submitKit.config.iscolumnPkidChecked = true;
            pro.submitKit.config.isVerVal = false;
            postData.RequestEntity.UserDepartmentList = pro.submitKit.getRowJson();
            $.each(postData.RequestEntity.UserDepartmentList, function (index, row) {
                row.PkId = row.Attr_UserDepartmentPkId;
            }
            );

            pro.submitKit.config.columnPkidName = "RoleId";
            pro.submitKit.config.columns = ["Attr_UserRolePkId"];
            pro.submitKit.config.iscolumnPkidChecked = true;
            pro.submitKit.config.isVerVal = true;
            postData.RequestEntity.UserRoleList = pro.submitKit.getRowJson();
            $.each(postData.RequestEntity.UserRoleList, function (index, row) {
                row.PkId = row.Attr_UserRolePkId;
            }
          );


            pro.submitKit.config.columnPkidName = "RiverId";
            pro.submitKit.config.columns = ["Attr_RiverOwerPkId"];
            pro.submitKit.config.iscolumnPkidChecked = true;
            pro.submitKit.config.isVerVal = true;
            postData.RequestEntity.RiverOwerList = pro.submitKit.getRowJson();
            $.each(postData.RequestEntity.RiverOwerList, function (index, row) {
                row.PkId = row.Attr_RiverOwerPkId;
            }
          );


            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/PermissionManager/UserInfo/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.UserInfo.ListPage.closeTab();
                    }
                    parent.$.alertExtend.info("", afterSuccess());
                }
            ).fail(
             function (errordetails, errormessage) {
                 // $.alertExtend.error();
             }
            );

        },
        submitExtend: {
            addRule: function () {
                $("#form1").validate({
                    rules: {
                        // PkId: { required: true  },
                        UserCode: { required: true },
                        Password: { required: true },
                        UserName: { required: true }
                        //Email: { required: true  },
                        //Mobile: { required: true  },
                        //Tel: { required: true  },
                        //IsActive: { required: true  },
                        //CreatorUserCode: { required: true  },
                        //CreationTime: { required: true  },
                        //LastModifierUserCode: { required: true  },
                        //LastModificationTime: { required: true  },
                        //Remark: { required: true  },
                        //IsDeleted: { required: true  },
                    },
                    messages: {
                        PkId: "必填!",
                        UserCode: "员工号必填!",
                        Password: "密码必填!",
                        UserName: "用户名必填!",
                        Email: "电子邮件必填!",
                        Mobile: "手机号必填!",
                        Tel: "家庭电话必填!",
                        IsActive: "是否有效必填!",
                        CreatorUserCode: "创建人必填!",
                        CreationTime: "创建时间必填!",
                        LastModifierUserCode: "修改人必填!",
                        LastModificationTime: "修改时间必填!",
                        Remark: "备注必填!",
                        IsDeleted: "是否删除必填!",
                    },
                    errorPlacement: function (error, element) {
                        pro.commonKit.errorPlacementHd(error, element);
                    },
                    debug: false
                });
            },
            logicValidate: function () {
                return true;
            }
        },

        addTab: function (subtitle, url) {

        }

    };
})();



$(function () {
    pro.UserInfo.HdPage.initPage();
});


