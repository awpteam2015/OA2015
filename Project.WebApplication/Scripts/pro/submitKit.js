var pro = pro || {};
(function () {
    pro.submitKit = pro.submitKit || {};
    pro.submitKit = {
        config: {
            iscolumnPkidChecked: false,
            isVerVal: true,//是否验证值大于0
            columnPkidName: "PkId",
            columns: new Array(),
            columnNamePreStr: '',//字段名前缀
            excludeAreaIds: ""
        },
        getHeadJson: function () {
            var inputObj = $("input");
            var selectObj = $("select");
            if (this.excludeAreaIds) {
                var excludeAreaId = excludeAreaIds.split(',');
                var inputnotexpr = "";
                var selectnotexpr = "";
                for (var i = 0, max = excludeAreaId.length; i < max; i++) {
                    inputnotexpr += "#" + excludeAreaId[i] + " input,";
                    selectnotexpr += "#" + excludeAreaId[i] + " select,";
                }
                inputObj = inputObj.not($(inputnotexpr.substring(0, inputnotexpr.length - 1)));
                selectObj = selectObj.not($(selectnotexpr.substring(0, selectnotexpr.length - 1)));
            }

            var json = "";
            inputObj.each(
                function () {
                    if ($(this).attr("name") != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + $.trim($(this).val()) + '",';
                    }
                });

            selectObj.each(
                function () {
                    if ($(this).attr("name") != undefined && $(this).val() != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + $.trim($(this).val()) + '",';
                    }
                }
            );

            $("textarea").each(
                function () {
                    if ($(this).attr("name") != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + $.trim(Base64.encode($(this).val())) + '",';
                    }
                }
            );
            return jQuery.parseJSON("{" + json.substring(0, json.length - 1) + "}");
        },
        getRowJson: function () {
            var columns = this.config.columns;
            var PkId = this.config.columnPkidName;
            var PreStr = this.config.columnNamePreStr;
            var verVal = this.config.isVerVal;
            var json = "";
            var obj = this.config.iscolumnPkidChecked ? $("[name=" + PkId + "]:checked") : $("[name=" + PkId + "]");

            obj.each(
                function () {
                    var rowJson = "";
                    if (!verVal || $(this).val() > 0) {
                        rowJson = '"' + PkId.replace(PreStr, '') + '":"' + $(this).val() + '",';
                    }


                    for (var i = 0, max = columns.length; i < max; i++) {
                        rowJson += '"' + columns[i] + '":"' + $("[name=" + PreStr + columns[i] + "_" + $(this).val() + "]").val() + '",';
                    }

                    json += "{" + rowJson.substring(0, rowJson.length - 1) + "},";
                }
            );
            //alert(json);
            return jQuery.parseJSON("[" + json.substring(0, json.length - 1) + "]");
        }
    };
})();