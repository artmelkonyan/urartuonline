$(document).ready(function(){

    var menuOpenned = false;

    $(".burger-menu-icon").on("click", function(){
        if(!menuOpenned){
            $(".floating-nav").animate({height: "100%"}, 300, "linear");
            $(".burger-menu").slideDown(500);
            menuOpenned = true
        }else{
            $(".floating-nav").animate({height: "80px"}, 300, "linear");
            $(".burger-menu").slideUp(500);
            menuOpenned = false;
        }
    })

})