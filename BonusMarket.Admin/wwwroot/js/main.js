$(document).ready(function() {
	// $(".selectize").each(function () {
	// 	$(this).selectize();
	// });

	$(".selectize-tags").each(function () {
		$(this).selectize({
			plugins: ["remove_button", "drag_drop"],
			delimiter: ",",
			persist: false,
			create: function (input) {
				return {
					value: input,
					text: input
				}
			}
		});
	});
	
	$("[data-type='sortable']").each(function() {
		var self = $(this);
		self.sortable({
			nested: true,
			vertical: true,
			handle: ".handler"
		});
	});
	
	var body = $("body");
	var sideMenuToggle = $(".sideMenuToggle");

	if (localStorage.getItem('mtsidebartoggle') === "true") {
        body.addClass("mtSidebarOpen");
    } else {
        body.removeClass("mtSidebarOpen");
    }

	sideMenuToggle.click(function (e) {
		e.preventDefault();
		body.toggleClass("mtSidebarOpen");
		
		saveSidebarToggle();
	});

	function saveSidebarToggle() {
        let open = body.hasClass('mtSidebarOpen');

        localStorage.setItem('mtsidebartoggle', open);
	}
	
	var notificationsToggle = $(".notificationsToggle");
	
	notificationsToggle.click(function (e) {
		e.preventDefault();
		body.toggleClass("mtNotificationsOpen");
	});

	var sidebarHeadingCollapseLink = $(".mtSidebar .panel-heading a[data-toggle='collapse']");

	sidebarHeadingCollapseLink.click(function () {
		if (!$("body").hasClass("mtSidebarOpen")) {
			return false;
		}
	});

	var contentWrapper = ".contentWrapper";
	var headerHeight = $("header").height();
	var footerHeight = $("footer").height();

	function contentWrapperHeight() {
		var calcHeight = $(window).height() - headerHeight - footerHeight;
		var sidebarHeight = $(".mtSidebar > .sidebar-menu").height() - footerHeight;
		if (calcHeight <= sidebarHeight) {
			$(contentWrapper).css("min-height", sidebarHeight);
		}
		else {
			$(contentWrapper).css("min-height", calcHeight);
		}
	}

	contentWrapperHeight();
	$("footer").removeClass("invisibile");

	$(window).resize(function() {
		contentWrapperHeight();
	});
});