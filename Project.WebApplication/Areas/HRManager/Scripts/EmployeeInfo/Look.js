var pro = pro || {};
(function () {
    pro.EmployeeInfo = pro.EmployeeInfo || {};
    pro.EmployeeInfo.LookPage = pro.EmployeeInfo.LookPage || {};
    pro.EmployeeInfo.LookPage = {
        init: function () {
            //$('#tabs').tabs({
            //    width: $("#tabs").parent().width(),
            //    height: $(window).height() - 550
            //});
            return {
                tabObj: new pro.TabBase(),
                gridObjWork: new pro.GridBase("#datagridwork", false),
                gridObjStudy: new pro.GridBase("#datagridstudy", false),
                gridObjTechnical: new pro.GridBase("#datagridTechnical", false),
                gridObjContinEducation: new pro.GridBase("#datagridContinEducation", false),
                gridObjProfession: new pro.GridBase("#datagridProfession", false),
                gridObjYear: new pro.GridBase("#datagridYear", false),
                gridObjWage: new pro.GridBase("#datagridWage", false),
                gridObjFile: new pro.GridBase("#datagridfile", false),
                xwOptionHtml: '',//学位<option></option>
                xlOptionHtml: '',//学历<option></option>
                xzOptionHtml: '',//学制<option></option>
                zcOptionHtml: '',//职称<option></option>
                zYLXOptionHtml: '',//专业类型<option></option>
                jxjyOptionHtml: '',//继续教育学分类型<option></option>
                gwgzOptionHtml: '',//岗位工资<option></option>
                xzgzOptionHtml: '',//薪资工资<option></option>
                ndkhOptionHtml: ''//年度考核评价<option></option>
            };
        },
        initPage: function () {
            var initObj = this.init();
            var gridObjWork = initObj.gridObjWork;
            var gridObjStudy = initObj.gridObjStudy;
            var gridObjContinEducation = initObj.gridObjContinEducation;
            var gridObjTechnical = initObj.gridObjTechnical;
            var gridObjProfession = initObj.gridObjProfession;
            var gridObjYear = initObj.gridObjYear;
            var gridObjWage = initObj.gridObjWage;
            var gridObjFile = initObj.gridObjFile;

            ////隐藏编辑按钮
            //if (pro.commonKit.getUrlParam("View")) {
            //    $('#btnEdit').css("display", "none");
            //}
            //else {
            //    $("#btnEdit").click(function () {
            //        pro.EmployeeInfo.LookPage.submit("Edit");
            //    });
            //}
            //$("#btnAdd").click(function () {
            //    pro.EmployeeInfo.LookPage.submit("Add");
            //});



            $("#btnClose").click(function () {
                parent.pro.EmployeeInfo.ListPage.closeTab("");
            });
            $("#btnExport").click(function () {
                $.messager.confirm("确认操作", "是否确认导出", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/HRManager/EmployeeInfo/ExportWord?employeeId=" + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0)
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
            $('#DepartmentCode').combotree({
                required: true,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            });
            $("#CertNo").blur(function () {

                var UUserCard = $("#CertNo").val();

                //获取出生日期
                if (UUserCard && UUserCard.length >= 15)
                    $('#Birthday').val(UUserCard.substring(6, 10) + "-" + UUserCard.substring(10, 12) + "-" + UUserCard.substring(12, 14));
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
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=JXJY"
            }).done(
             function (dataresult, data) {
                 initObj.jxjyOptionHtml = "";

                 $.each(dataresult, function (i, item) {
                     initObj.jxjyOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
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
            //专业类型
            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZYLX"
                //,data: JSON.stringify(postData)
            }).done(
            function (dataresult, data) {
                initObj.zYLXOptionHtml = "";

                $.each(dataresult, function (i, item) {
                    initObj.zYLXOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
                });

            }
        ).fail(
         function (errordetails, errormessage) {
             //  $.alertExtend.error();
         }
        );


            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=Degree"
                //,data: JSON.stringify(postData)
            }).done(
           function (dataresult, data) {
               initObj.xwOptionHtml = "";

               $.each(dataresult, function (i, item) {
                   initObj.xwOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
               });

           }
           ).fail(
            function (errordetails, errormessage) {
                //  $.alertExtend.error();
            }
       );

            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=Education"
                //,data: JSON.stringify(postData)
            }).done(
          function (dataresult, data) {
              initObj.xlOptionHtml = "";

              $.each(dataresult, function (i, item) {
                  initObj.xlOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
              });

          }
          ).fail(
           function (errordetails, errormessage) {
               //  $.alertExtend.error();
           }
      );
            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=NDKH_PJ"
                //,data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    initObj.ndkhOptionHtml = "";

                    $.each(dataresult, function (i, item) {
                        initObj.ndkhOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
                    });
                }).fail(
                    function (errordetails, errormessage) {
                        //  $.alertExtend.error();
                    });

            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=RY_GWGZ"
                //,data: JSON.stringify(postData)
            }).done(

          function (dataresult, data) {
              initObj.gwgzOptionHtml = "";

              $.each(dataresult, function (i, item) {
                  initObj.gwgzOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
              });
          }).fail(
                 function (errordetails, errormessage) {
                     //  $.alertExtend.error();
                 });
            abp.ajax({
                url: "/HRManager/Dictionary/GetListByCode?ParentKeyCode=RY_YZGZ"
                //,data: JSON.stringify(postData)
            }).done(
             function (dataresult, data) {
                 initObj.xzgzOptionHtml = "";

                 $.each(dataresult, function (i, item) {
                     initObj.xzgzOptionHtml += "<option value='" + item.KeyValue + "'>" + item.KeyName + "</option>";
                 });
             }).fail(
                 function (errordetails, errormessage) {
                     //  $.alertExtend.error();
                 });

            var bindEntity = JSON.parse($("#BindEntity").val());
            $('#TechnicalTitle').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=JSZC',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#TechnicalTitle").combobox('setValue', bindEntity['TechnicalTitle']);
                    }
                }
            });
            $('#Duties').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=DWZW',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#Duties").combobox('setValue', bindEntity['Duties']);
                    }
                }
            });
            $('#WorkState').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZZZT',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#WorkState").combobox('setValue', bindEntity['WorkState']);
                    }
                }
            });

            $('#PoliticsName').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZZMM',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#PoliticsName").combobox('setValue', bindEntity['PoliticsName']);
                    }
                }
            });

            $('#EmployeeType').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=YGLY',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#EmployeeType").combobox('setValue', bindEntity['EmployeeType']);
                    }
                }
            });
            $('#State').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZT',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#State").combobox('setValue', bindEntity['State']);
                    }
                }
            });
            $('#PostLevel').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=GWDJ',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#PostLevel").combobox('setValue', bindEntity['PostLevel']);
                    }
                }
            });
            $('#PostProperty').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=GWXZ',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#PostProperty").combobox('setValue', bindEntity['PostProperty']);
                    }
                }
            });

            $('#EngageInPost').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=EngageInPost',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#EngageInPost").combobox('setValue', bindEntity['EngageInPost']);
                    }
                }
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
                            field: 'PkId', title: '', hidden: true, width: 100
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("PkId", value);
                            //}
                        },
                        {
                            field: 'WorkCompany',
                            title: '工作单位',
                            width: 150
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("WorkCompany_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'Duties',
                            title: '职务',
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("Duties_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'BeginDate',
                            title: '开始日期',
                            width: 150
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputDateHtml("BeginDate_" + row.PkId, value, 145);
                            //}
                        },
                        {
                            field: 'EndDate',
                            title: '结束日期',
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputDateHtml("EndDate_" + row.PkId, value, 145);
                            //}
                        },
                        {
                            field: 'WorkContent',
                            title: '工作内容',
                            width: 160
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getTextAreaHtml("WorkContent_" + row.PkId, value, 140, 50);
                            //}
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
                            width: 200
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("S_School_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'ProfessionCode',
                            title: '专业',
                            width: 150
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("S_ProfessionCode_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'Degree',
                            title: '学位',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("S_Degree_" + row.PkId, value, initObj.xwOptionHtml, 110, true);
                            }
                            //formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("S_Degree_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'Education',
                            title: '学历',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("S_Education_" + row.PkId, value, initObj.xlOptionHtml, 110, true);
                            }
                        },
                        {
                            field: 'SchoolYear',
                            title: '学制',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("S_SchoolYear_" + row.PkId, value, initObj.xzOptionHtml, 110, true);
                            }
                        },
                        {
                            field: 'CertNumber',
                            title: '证书编号',
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("S_CertNumber_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'BeginDate',
                            title: '入学日期',
                            width: 110
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputDateHtml("S_BeginDate_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'EndDate',
                            title: '毕业日期',
                            width: 110
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputDateHtml("S_EndDate_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'Remark',
                            title: '备注',
                            width: 160
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getTextAreaHtml("S_Remark_" + row.PkId, value, 170, 50);
                            //}
                        }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
            );

            gridObjContinEducation.grid({
                url: '/HRManager/ContinEducation/GetAllList?EmployeeID=' + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0),
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
                                return pro.controlKit.getInputHtml("C_PkId", row.PkId);
                            }
                        },
                        {
                            field: 'CreditType',
                            title: '学分类型',
                            width: 120
                            , formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("C_CreditType_" + row.PkId, value, initObj.jxjyOptionHtml, 110, true);
                            }
                        },
                        {
                            field: 'Score',
                            title: '分数',
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("C_Score_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'GetTime',
                            title: '时间',
                            width: 110
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputDateHtml("C_GetTime_" + row.PkId, value);
                            //}
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
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("T_Title_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'LevNum',
                            title: '等级',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("T_LevNum_" + row.PkId, value, initObj.zcOptionHtml, 110, true);
                            }
                        },
                        {
                            field: 'GetDate',
                            title: '取得时间',
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputDateHtml("T_GetDate_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'CerNo',
                            title: '职称证书编号',
                            width: 130
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("T_CerNo_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'EmployDate',
                            title: '聘用时间',
                            width: 130
                        },
                        {
                            field: 'EmployEndDate',
                            title: '聘用结束时间',
                            width: 130
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
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("P_Title_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'TypeName',
                            title: '执业类别',
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("P_TypeName_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'RangeName',
                            title: '执业范围',
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("P_RangeName_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'GetDate',
                            title: '取得时间',
                            width: 120
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputDateHtml("P_GetDate_" + row.PkId, value);
                            //}
                        },
                        {
                            field: 'ZYLX',
                            title: '专业类型',
                            width: 120,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("T_ZYLX_" + row.PkId, value, initObj.zYLXOptionHtml, 110, true);
                            }
                        },
                        {
                            field: 'CerNo',
                            title: '职称证书编号',
                            width: 130
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputHtml("P_CerNo_" + row.PkId, value);
                            //}
                        }
                        //,
                        //{
                        //    field: 'EmployDate',
                        //    title: '聘用时间',
                        //    width: 130
                        //    //,formatter: function (value, row, index) {
                        //    //    return pro.controlKit.getInputDateHtml("P_EmployDate_" + row.PkId, value);
                        //    //}
                        //},
                        //{
                        //    field: 'EmployEndDate',
                        //    title: '聘用结束时间',
                        //    width: 130
                        //    //,formatter: function (value, row, index) {
                        //    //    return pro.controlKit.getInputDateHtml("P_EmployEndDate_" + row.PkId, value);
                        //    //}
                        //}
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
            );
            gridObjYear.grid({
                url: '/HRManager/YearAssessment/GetAllList?EmployeeID=' + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0),
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
                                return pro.controlKit.getInputHtml("Y_PkId", row.PkId);
                            }
                        },
                        {
                            field: 'KHYear',
                            title: '考核年度',
                            width: 180
                            //,formatter: function (value, row, index) {
                            //    return pro.controlKit.getInputDateHtml("Y_KHYear_" + row.PkId, value, 150, 'yyyy');
                            //}
                        },
                        {
                            field: 'KHComment',
                            title: '评价',
                            width: 200
                            , formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("Y_KHComment_" + row.PkId, value, initObj.ndkhOptionHtml, 190, true);
                            }
                        }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
          );


            gridObjWage.grid({
                url: '/HRManager/YGWage/GetAllList?EmployeeID=' + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0),
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
                                return pro.controlKit.getInputHtml("W_PkId", row.PkId);
                            }
                        },
                        {
                            field: 'GWGZ',
                            title: '岗位工资',
                            width: 180,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("W_GWGZ_" + row.PkId, value, initObj.gwgzOptionHtml, 150, true);
                            }
                        },
                        {
                            field: 'XZGZ',
                            title: '薪级工资',
                            width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getSelectHtml("W_XZGZ_" + row.PkId, value, initObj.xzgzOptionHtml, 190, true);
                                // return pro.controlKit.getInputHtml("Y_KHComment_" + row.PkId, value, 180);
                            }
                        }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
            );
            gridObjFile.grid({
                url: '/HRManager/EmployeeFile/GetAllList?EmployeeID=' + (pro.commonKit.getUrlParam("PkId") ? pro.commonKit.getUrlParam("PkId") : 0),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'FOrgName',
                            title: '名称',
                            width: 350
                        },
                        {
                            field: 'FileUrl',
                            title: '下载',
                            width: 120,
                            formatter: function (value, row, index) {
                                return "<a href='" + row.FileUrl + "/" + row.FName + "'>下载</a>";
                            }
                        },
                        {
                            field: 'PkId',
                            title: '操作',
                            width: 120,
                            formatter: function (value, row, index) {
                                return "<a href='javascript:pro.EmployeeInfo.HdPage.deletedFile(" + row.PkId + ")'>删除</a>";
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
            $("#btnAddYear_ToolBar").click(function () {
                gridObjYear.insertRow({
                    PkId: gridObjYear.PkId
                });

                $("#datagridYear").datagrid('selectRecord', gridObjYear.P_PkId + 1);
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
            $("#btnDelYear_ToolBar").click(function () {
                gridObjYear.delRow();
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();

                //var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                    if (filedname == "Sex")
                        $("#Sex").combobox('setValue', bindEntity[filedname]);
                }

                if (bindEntity["FileName"] != undefined && bindEntity["FileName"] != "") {
                    var fullPath = bindEntity["FileUrl"] + "\\" + bindEntity["FileName"];
                    $('#div_filename').html("<span ><img name=\"listP\" style=\"height:206px;width:148px;\" src=\"" + fullPath + "\">" + "</img> </span>");//+ json.extension.orgfileName
                }
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }
            $('#WorkingYears').numberbox({
                min: 0
            });

        },
        submit: function (command) {
            var postData = {};

            postData.RequestEntity = pro.submitKit.getHeadJson();
            //postData.RequestEntity.TechnicalTitleName = $('#TechnicalTitle').combobox('getText');

            postData.RequestEntity.DutiesName = $('#Duties').combobox('getText');
            postData.RequestEntity.WorkStateName = $('#WorkState').combobox('getText');
            postData.RequestEntity.EmployeeTypeName = $('#EmployeeType').combobox('getText');
            postData.RequestEntity.DepartmentName = $('#DepartmentCode').combotree('getText');

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

            pro.submitKit.config.columnPkidName = "Y_PkId";
            pro.submitKit.config.columnNamePreStr = "Y_";
            pro.submitKit.config.columns = ["KHYear", "KHComment"];
            postData.RequestEntity.YearAssessmentList = pro.submitKit.getRowJson();

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
    pro.EmployeeInfo.LookPage.initPage();
});


