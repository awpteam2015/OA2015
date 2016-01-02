var pro = pro || {};
(function() {
    pro.bindKit = pro.bindKit || {};
    pro.bindKit = {
        config: {
            columnPkidName: "",
            columns: new Array(),
            excludeAreaIds: ""
        },
        getHeadJson: function() {
            var inputObj = $("input");
            var selectObj = $("select");
    
            if (this.config.excludeAreaIds) {
                var excludeAreaId = this.config.excludeAreaIds.split(',');
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
                function() {
                    if ($(this).attr("name") != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + $(this).attr("name") + '",';
                    }
                });

            selectObj.each(
                function() {
                    if ($(this).attr("name") != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + $(this).attr("name") + '",';
                    }
                }
            );

            $("textarea").each(
                function() {
                    if ($(this).attr("name") != undefined) {
                        json += '"' + $(this).attr("name") + '":"' + $(this).attr("name") + '",';
                    }
                }
            );
            return jQuery.parseJSON("{" + json.substring(0, json.length - 1) + "}");
        },
        getRowJson: function() {
            //var columns = this.config.columns;
            //var json = "";
            //$("[name=" + this.config.columnPkidName + "]").each(
            //    function() {
            //        var rowJson = "";
            //        for (var i = 0, max = columns.length; i < max; i++) {
            //            rowJson += '"' + columns[i] + '":"' + $("[name=" + columns[i] + "_" + $(this).val() + "]").val() + '",';
            //        }
            //        json += "{" + rowJson.substring(0, rowJson.length - 1) + "},";
            //    }
            //);
            //return jQuery.parseJSON("[" + json.substring(0, json.length - 1) + "]");
        }
    };
})();