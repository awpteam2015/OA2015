var pro = pro || {};
(function () {
    pro.submitKit = pro.submitKit || {};
    pro.submitKit = {
        config: {
            columnPkidName: "PkId",
            columns: new Array(),
            excludeAreaIds: ""
        },
        getHeadJson: function () {
            var inputObj = $("input[type=text]");
            var selectObj = $("select");
            if (this.excludeAreaIds) {
                var excludeAreaId = excludeAreaIds.split(',');
                var inputnotexpr = "";
                var selectnotexpr = "";
                for (var i = 0, max = excludeAreaId.length; i < max; i++) {
                    inputnotexpr += "#" + excludeAreaId[i] + " input[type='text'],";
                    selectnotexpr += "#" + excludeAreaId[i] + " select,";
                }
                inputObj = inputObj.not($(inputnotexpr.substring(0, inputnotexpr.length - 1)));
                selectObj = selectObj.not($(selectnotexpr.substring(0, selectnotexpr.length - 1)));
            }

            var json = "";
            inputObj.each(
                function () {
                    if ($(this).attr("name") != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + $(this).val().trim() + '",';
                    }
                });

            selectObj.each(
                function () {
                    if ($(this).attr("name") != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + $(this).val().trim() + '",';
                    }
                }
            );

            $("textarea").each(
                function () {
                    if ($(this).attr("name") != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + Base64.encode($(this).val().trim()) + '",';
                    }
                }
            );
            return jQuery.parseJSON("{" + json.substring(0, json.length - 1) + "}");
        },
        getRowJson: function () {
            var columns = this.config.columns;
            var PkId = this.config.columnPkidName;
            var json = "";
            $("[name=" + PkId + "]").each(
                function () {
                    var rowJson = '"' + PkId + '":"' + $(this).val() + '",';
                    for (var i = 0, max = columns.length; i < max; i++) {

                        rowJson += '"' + columns[i] + '":"' + $("[name=" + columns[i] + "_" + $(this).val() + "]").val() + '",';
                    }

                    json += "{" + rowJson.substring(0, rowJson.length - 1) + "},";
                }
            );
            alert(json);
            return jQuery.parseJSON("[" + json.substring(0, json.length - 1) + "]");
        }
    };
})();