
var pro = pro || {};
(function () {
    pro.IndexPage = pro.IndexPage || {};
    pro.IndexPage = {
        initPage: function () {
            $("[name='mainFrame']").attr("src", "/Account/Default");

            $('.menulist li').click(function () {
                var tabTitle = $(this).find("a").text();
                var url = $(this).find("a").attr("lang");
                pro.IndexPage.addTab(tabTitle, url);
            });

            $("#btnLogoff").click(
                function() {
                    abp.ajax({
                        url: "/Login/UserLogoff"
                    }).done(
              function (data, data2) {
                  window.location.href = "/Login/Index2";
              }
          );
                }
            );

        },
        addTab: function (subtitle, url) {
            if (!$('#tabsDefault').tabs('exists', subtitle)) {
                $('#tabsDefault').tabs('add', {
                    title: subtitle,
                    content: '<iframe scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>',
                    closable: true,
                    bodyCls: "tabOverflow"
                });
            } else {
                $('#tabsDefault').tabs('select', subtitle);
            }
        }
    };
})();



$(function () {
    pro.IndexPage.initPage();
});