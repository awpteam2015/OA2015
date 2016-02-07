var pro = pro || {};
(function () {
    pro.EmployeeInfo = pro.EmployeeInfo || {};
    pro.EmployeeInfo.HdPage = pro.EmployeeInfo.HdPage || {};
    pro.EmployeeInfo.HdPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObjWork: new pro.GridBase("#datagridwork", false),
                gridObjStudy: new pro.GridBase("#datagridstudy", false),
                gridObjTechnical: new pro.GridBase("#datagridTechnical", false),
                gridObjProfession: new pro.GridBase("#datagridProfession", false),
                xzOptionHtml: '',//学制<option></option>
                zcOptionHtml: ''//职称<option></option>
            };
        },
        initPage: function () {
            var initObj = this.init();
            var gridObjWork = initObj.gridObjWork;
            var gridObjStudy = initObj.gridObjStudy;
            var gridObjTechnical = initObj.gridObjTechnical;
            var gridObjProfession = initObj.gridObjProfession;
            $("#btnAdd").click(function () {
                pro.EmployeeInfo.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.EmployeeInfo.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.EmployeeInfo.ListPage.closeTab("");
            });

            $('#DepartmentCode').combotree({
                required: true,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            });

            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=ShoochYears"
                //,data: JSON.stringify(postData)
            }).done(
               function (dataresult, data) {
                   initObj.xzOptionHtml = "";

                   $.each(dataresult, function (i, item) {
                       initObj.xzOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
                   });

               }
           ).fail(
            function (errordetails, errormessage) {
                //  $.alertExtend.error();
            }
           );

            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=JSZC"
                //,data: JSON.stringify(postData)
            }).done(
              function (dataresult, data) {
                  initObj.zcOptionHtml = "";

                  $.each(dataresult, function (i, item) {
                      initObj.zcOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
                  });

              }
          ).fail(
           function (errordetails, errormessage) {
               //  $.alertExtend.error();
           }
          );

            $('#TechnicalTitle').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=JSZC'
            });
            $('#Duties').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=DWZW'
            });
            $('#WorkState').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZZZT'
            });

            $('#EmployeeType').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=YGLY'
            });
            $('#State').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZT'
            });

            gridObjWork.grid({
                url: '/HRManager/WorkExperience/GetAllList?EmployeeID=' + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', title: '', hidden: true, width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("PkId", value);
                            }
                        },
                        {
                            field: 'WorkCompany',
                            title: '工作单位',
                            width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("WorkCompany_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'Duties',
                            title: '职务',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("Duties_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'BeginDate',
                            title: '开始日期',
                            width: 150,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputDateHtml("BeginDate_" + row.PkId, value, 145);
                            }
                        },
                        {
                            field: 'EndDate',
                            title: '结束日期',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputDateHtml("EndDate_" + row.PkId, value, 145);
                            }
                        },
                        {
                            field: 'WorkContent',
                            title: '工作内容',
                            width: 160,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getTextAreaHtml("WorkContent_" + row.PkId, value, 140, 50);
                            }
                        }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
            );

            gridObjStudy.grid({
                url: '/HRManager/LearningExperiences/GetAllList?EmployeeID=' + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', title: '', hidden: true, width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("S_PkId", row.PkId);
                            }
                        },
                        {
                            field: 'School',
                            title: '毕业院校',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("S_School_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'ProfessionCode',
                            title: '专业',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("S_ProfessionCode_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'Degree',
                            title: '学位',
                            width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("S_Degree_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'Education',
                            title: '学历',
                            width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("S_Education_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'SchoolYear',
                            title: '学制',
                            width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("S_SchoolYear_" + row.PkId, value, initObj.xzOptionHtml);
                            }
                        },
                        {
                            field: 'CertNumber',
                            title: '证书编号',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("S_CertNumber_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'BeginDate',
                            title: '入学日期',
                            width: 110,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputDateHtml("S_BeginDate_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'EndDate',
                            title: '毕业日期',
                            width: 110,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputDateHtml("S_EndDate_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'Remark',
                            title: '备注',
                            width: 160,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getTextAreaHtml("S_Remark_" + row.PkId, value, 170, 50);
                            }
                        }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
            );
            gridObjTechnical.grid({
                url: '/HRManager/Technical/GetAllList?EmployeeID=' + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', title: '', hidden: true, width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("T_PkId", row.PkId);
                            }
                        },
                        {
                            field: 'Title',
                            title: '名称',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("T_Title_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'LevNum',
                            title: '等级',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("T_LevNum_" + row.PkId, value, initObj.zcOptionHtml, 110);
                            }
                        },
                        {
                            field: 'GetDate',
                            title: '取得时间',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputDateHtml("T_GetDate_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'CerNo',
                            title: '职称证书编号',
                            width: 130,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("T_CerNo_" + row.PkId, value);
                            }
                        }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
            );
            gridObjProfession.grid({
                url: '/HRManager/Profession/GetAllList?EmployeeID=' + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', title: '', hidden: true, width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("P_PkId", row.PkId);
                            }
                        },
                        {
                            field: 'Title',
                            title: '名称',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("P_Title_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'TypeName',
                            title: '执业类别',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("P_TypeName_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'RangeName',
                            title: '执业范围',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("P_RangeName_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'GetDate',
                            title: '取得时间',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputDateHtml("P_GetDate_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'CerNo',
                            title: '职称证书编号',
                            width: 130,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("P_CerNo_" + row.PkId, value);
                            }
                        }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
            );

            $("#btnAddWork_ToolBar").click(function () {
                gridObjWork.insertRow({
                    PkId: gridObjWork.PkId,
                    WorkCompany: ""
                });

                //console.log(JSON.stringify($("#datagrid").datagrid('getRows')));
                //console.log(gridObj.PkId + 1);

                $("#datagridwork").datagrid('selectRecord', gridObjWork.PkId + 1);
            });
            $("#btnAddStudy_ToolBar").click(function () {
                gridObjStudy.insertRow({
                    PkId: gridObjStudy.PkId,
                    School: ""
                });

                $("#datagridstudy").datagrid('selectRecord', gridObjStudy.S_PkId + 1);
            });
            $("#btnAddTechnical_ToolBar").click(function () {
                gridObjTechnical.insertRow({
                    PkId: gridObjTechnical.PkId,
                    Title: ""
                });

                $("#datagridTechnical").datagrid('selectRecord', gridObjTechnical.T_PkId + 1);
            });
            $("#btnAddProfession_ToolBar").click(function () {
                gridObjProfession.insertRow({
                    PkId: gridObjProfession.PkId
                });

                $("#datagridProfession").datagrid('selectRecord', gridObjProfession.P_PkId + 1);
            });

            $("#btnDelWork_ToolBar").click(function () {
                gridObjWork.delRow();
            });
            $("#btnDelStudy_ToolBar").click(function () {
                gridObjStudy.delRow();
            });
            $("#btnDelTechnical_ToolBar").click(function () {
                gridObjTechnical.delRow();
            });
            $("#btnDelProfession_ToolBar").click(function () {
                gridObjProfession.delRow();
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();

                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }

                if (bindEntity["FileName"] != undefined && bindEntity["FileName"] != "") {
                    var fullPath = bindEntity["FileUrl"] + "\\" + bindEntity["FileName"];
                    $('#div_filename').html("<span ><img name=\"listP\" style=\"height:50px;width:160px;\" src=\"" + fullPath + "\">" + "</img> <a href=\"javascript:void(0)\" onclick=\"delImg(this);\">删除</a></span>");//+ json.extension.orgfileName

                }
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }

        },
        submit: function (command) {
            var postData = {};

            postData.RequestEntity = pro.submitKit.getHeadJson();
            //postData.RequestEntity.TechnicalTitleName = $('#TechnicalTitle').combobox('getText');
            postData.RequestEntity.DutiesName = $('#Duties').combobox('getText');
            postData.RequestEntity.WorkStateName = $('#WorkState').combobox('getText');
            postData.RequestEntity.EmployeeTypeName = $('#EmployeeType').combobox('getText');

            pro.submitKit.config.columnPkidName = "PkId";
            pro.submitKit.config.columnNamePreStr = "";
            pro.submitKit.config.columns = ["WorkCompany", "Duties", "BeginDate", "EndDate", "WorkContent"];
            postData.RequestEntity.WorkList = pro.submitKit.getRowJson();

            pro.submitKit.config.columnPkidName = "S_PkId";
            pro.submitKit.config.columnNamePreStr = "S_";
            pro.submitKit.config.columns = ["School", "ProfessionCode", "Degree", "Education", "SchoolYear", "CertNumber", "BeginDate", "EndDate", "Remark"];
            postData.RequestEntity.LearningList = pro.submitKit.getRowJson();

            pro.submitKit.config.columnPkidName = "T_PkId";
            pro.submitKit.config.columnNamePreStr = "T_";
            pro.submitKit.config.columns = ["Title", "LevNum", "GetDate", "CerNo"];
            postData.RequestEntity.TechnicalList = pro.submitKit.getRowJson();

            pro.submitKit.config.columnPkidName = "P_PkId";
            pro.submitKit.config.columnNamePreStr = "P_";
            pro.submitKit.config.columns = ["Title", "TypeName", "RangeName", "GetDate", "CerNo"];
            postData.RequestEntity.ProfessionList = pro.submitKit.getRowJson();

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
           
            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/EmployeeInfo/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.EmployeeInfo.ListPage.closeTab();
                    }
                    parent.$.alertExtend.info("", afterSuccess());
                }
            ).fail(
             function (errordetails, errormessage) {
                 //  $.alertExtend.error();
             }
            );

        },
        submitExtend: {
            addRule: function () {
                $("#form1").validate({
                    rules: {

                        EmployeeCode: { required: true },
                        EmployeeName: { required: true },
                        //DepartmentCode: { required: true  },
                        //JobName: { required: true  },
                        //PayCode: { required: true  },
                        //Sex: { required: true  },
                        CertNo: { isIdCardNo: '输入正确身份证号' },

                        //Birthday: { required: true  },
                        //TechnicalTitle: { required: true  },
                        //Duties: { required: true  },
                        //WorkState: { required: true  },
                        //EmployeeType: { required: true  },
                        //HomeAddress: { required: true  },
                        MobileNO: { isMobile: '输入正确手机号' },
                        //ImageUrl: { required: true  },
                        //Sort: { required: true  },
                        //State: { required: true  },
                        //Remark: { required: true  },
                        //CreatorUserCode: { required: true  },
                        //CreatorUserName: { required: true  },
                        //CreateTime: { required: true  },
                        //LastModificationTime: { required: true  },
                    },
                    messages: {
                        PkId: "必填!",
                        EmployeeCode: "员工编号必填!",
                        EmployeeName: "员工名称必填!",
                        DepartmentCode: "所属部门必填!",
                        JobName: "工号必填!",
                        PayCode: "中文简拼必填!",
                        Sex: "姓别必填!",
                        CertNo: "输入正确身份证号!",
                        Birthday: "生日必填!",
                        TechnicalTitle: "技术职称必填!",
                        Duties: "单位职务必填!",
                        WorkState: "在职状态必填!",
                        EmployeeType: "员工类型必填!",
                        HomeAddress: "家庭地址必填!",
                        MobileNO: "输入正确手机号!",
                        ImageUrl: "图片地址必填!",
                        Sort: "排序必填!",
                        State: "状态必填!",
                        Remark: "备注必填!",
                        CreatorUserCode: "操作员必填!",
                        CreatorUserName: "操作员名称必填!",
                        CreateTime: "创建时间必填!",
                        LastModificationTime: "修改时间必填!",
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
    pro.EmployeeInfo.HdPage.initPage();
});


