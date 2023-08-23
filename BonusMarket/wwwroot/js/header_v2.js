  $(function(){
  $('.dropdown-menu a.dropdown-toggle').on('click', function(e) {
    if (!$(this).next().hasClass('show')) {
      $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
    }
    var $subMenu = $(this).next(".dropdown-menu");
    $subMenu.toggleClass('show'); 			// appliqué au ul
    $(this).parent().toggleClass('show'); 	// appliqué au li parent

    $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function(e) {
      $('.dropdown-submenu .show').removeClass('show'); 	// appliqué au ul
      $('.dropdown-submenu.show').removeClass('show'); 		// appliqué au li parent
    });
    return false;
  });
});

$(document).on("click", function (event) {
   
  // If the target is not the container or a child of the container, then process
  // the click event for outside of the container.
  if ($(event.target).closest(".navbar-toggler").length === 0) {
    if($('.navbar-collapse').hasClass('show')){
    
    $('.navbar-collapse').removeClass('show')
    $('.mob-menu-close').removeClass('show-close-btn')
}
  }
});

$( ".navbar-toggler" ).click(function() {
    $('.mob-menu-close').addClass('show-close-btn')
});
