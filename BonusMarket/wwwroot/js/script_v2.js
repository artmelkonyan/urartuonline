$('.add').click(function () {
  if ($(this).prev().val()) {
    $(this).prev().val(+$(this).prev().val() + 1);
  }
});
$('.sub').click(function () {
  if ($(this).next().val() > 1) {
    if ($(this).next().val() > 1) $(this).next().val(+$(this).next().val() - 1);
  }
});


$('.multiple-items').slick({
  infinite: true,
  slidesToShow: 5,
  slidesToScroll: 3
});

$('.slider-adv').slick({
  slidesToShow: 2,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 2000,
  arrows: false,
  dots: false,
  pauseOnHover: false,
  centerMode: true,
})


$('.adv-part-parent').slick({
  slidesToShow: 2,
  slidesToScroll: 1,
  autoplay: false,

  arrows: false,
  dots: false,

  centerMode: true,
  centerPadding: '40px',
  responsive: [
    {
      breakpoint: 1440,
 
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow:2,
       
      }
    },
   
    {
      breakpoint: 768,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    },
    
  ]
});

$('.gift-part-parent').slick({
  slidesToShow: 3,
  slidesToScroll: 1,
  autoplay: false,

  arrows: false,
  dots: false,

  centerMode: true,
  centerPadding: '40px',
  responsive: [
    {
      breakpoint: 1440,
 
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow:2,
       
      }
    },
   
    {
      breakpoint: 768,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    }
  ]
});

$('.customer-logos').slick({
  slidesToShow: 6,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 2000,
  arrows: false,
  dots: false,
  pauseOnHover: false,
  centerMode: true,
  centerPadding: '40px',
  responsive: [
    {
      breakpoint: 1800,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow:5,
      }
    },
    {
      breakpoint: 1600,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 4
      }
    },
    {
      breakpoint: 1200,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 3
      }
    },
    {
      breakpoint: 768,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 2
      }
    },
    {
      breakpoint: 480,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    }
  ]
});

// $('.slider').slick();
const rangeInput = document.querySelectorAll(".range-input input"),
priceInput = document.querySelectorAll(".price-input input"),
range = document.querySelector(".slider .progress");
let priceGap = 1000;

priceInput.forEach(input =>{
    input.addEventListener("input", e =>{
        let minPrice = parseInt(priceInput[0].value),
        maxPrice = parseInt(priceInput[1].value);
        
        if((maxPrice - minPrice >= priceGap) && maxPrice <= rangeInput[1].max){
            if(e.target.className === "input-min"){
                rangeInput[0].value = minPrice;
                range.style.left = ((minPrice / rangeInput[0].max) * 100) + "%";
            }else{
                rangeInput[1].value = maxPrice;
                range.style.right = 100 - (maxPrice / rangeInput[1].max) * 100 + "%";
            }
        }
    });
});

rangeInput.forEach(input =>{
    input.addEventListener("input", e =>{
        let minVal = parseInt(rangeInput[0].value),
        maxVal = parseInt(rangeInput[1].value);
        if((maxVal - minVal) < priceGap){
            if(e.target.className === "range-min"){
                rangeInput[0].value = maxVal - priceGap
            }else{
                rangeInput[1].value = minVal + priceGap;
            }
        }else{
            priceInput[0].value = minVal;
            priceInput[1].value = maxVal;
            range.style.left = ((minVal / rangeInput[0].max) * 100) + "%";
            range.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";
        }
    });
});

$('.general-slider .slider').slick({
  prevArrow: "<button type='button' class='slick-prev  center-left pull-left'><img src='./img/prev-arrow.png'></button>",
  nextArrow: "<button type='button' class='slick-next   center-right pull-right'><img src='./img/next-arrow.png'></button>"
});

$('.slider-nav').slick({
  slidesToShow: 3,
  slidesToScroll: 1,
  dots: true,
  autoplay: true,
  autoplaySpeed: 2000,
  focusOnSelect: true,
  infinite: true,
  autoplay: true,
  autoplaySpeed: 2000
});


$('.responsive').slick({
  dots: true,
  infinite: true,
  speed: 300,
  slidesToShow: 5,
  slidesToScroll: 1,
  prevArrow: "<button type='button' class='slick-prev pull-left'><img src='./img/prev-arrow-slider.png'></button>",
  nextArrow: "<button type='button' class='slick-next pull-right'><img src='./img/next-arrow-slider.png'></button>",
  responsive: [
    {
      breakpoint: 1420,
      settings: {
        slidesToShow: 4,
        slidesToScroll: 4,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 1100,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
        infinite: true,
        dots: true
      }
    },

    {
      breakpoint: 850,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    }
  ]
});


if ($('.product__slider-main').length) {
  var $slider = $('.product__slider-main')
    .on('init', function (slick) {
      $('.product__slider-main').fadeIn(1000);
    })
    .slick({
      slidesToShow: 1,
      slidesToScroll: 1,
      arrows: false,
      autoplay: false,
      lazyLoad: 'ondemand',
      autoplaySpeed: 3000,
      asNavFor: '.product__slider-thmb'
    });

  var $slider2 = $('.product__slider-thmb')
    .on('init', function (slick) {
      $('.product__slider-thmb').fadeIn(1000);
    })
    .slick({
      slidesToShow: 4,
      slidesToScroll: 1,
      lazyLoad: 'ondemand',
      asNavFor: '.product__slider-main',
      centerMode: false,
      focusOnSelect: true,
      arrows: true,
      prevArrow: "<button type='button' class='slick-prev  pull-left'><img src='./img/prev-arrow-slider.png'></button>",
      nextArrow: "<button type='button' class='slick-next   right pull-right'><img src='./img/next-arrow-slider.png'></button>",
    });

  //remove active class from all thumbnail slides
  $('.product__slider-thmb .slick-slide').removeClass('slick-active');

  //set active class to first thumbnail slides
  $('.product__slider-thmb .slick-slide').eq(0).addClass('slick-active');

  // On before slide change match active thumbnail to current slide
  $('.product__slider-main').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
    var mySlideNumber = nextSlide;
    $('.product__slider-thmb .slick-slide').removeClass('slick-active');
    $('.product__slider-thmb .slick-slide').eq(mySlideNumber).addClass('slick-active');
  });


  // init slider
  //require der tox mna

  (['js-sliderWithProgressbar'], function (slider) {

    $('.product__slider-main').each(function () {

      me.slider = new slider($(this), options, sliderOptions, previewSliderOptions);

      // stop slider
      //me.slider.stop();

      // start slider
      //me.slider.start(index);

      // get reference to slick slider
      //me.slider.getSlick();

    });
  });
  var options = {
    progressbarSelector: '.bJS_progressbar'
    , slideSelector: '.bJS_slider'
    , previewSlideSelector: '.bJS_previewSlider'
    , progressInterval: ''
    // add your own progressbar animation function to sync it i.e. with a video
    // function will be called if the current preview slider item (".b_previewItem") has the data-customprogressbar="true" property set
    , onCustomProgressbar: function ($slide, $progressbar) { }
  }

  // slick slider options
  // see: https://kenwheeler.github.io/slick/
  var sliderOptions = {
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    fade: true,
    autoplay: true
  }

  // slick slider options
  // see: https://kenwheeler.github.io/slick/
  var previewSliderOptions = {
    slidesToShow: 3,
    slidesToScroll: 1,
    dots: false,
    focusOnSelect: true,
    centerMode: true
  }
}

function countOfOrder(){
  var count=$(".count-of-order").text(($(".product-row").length));
}
countOfOrder()

function totalPrice(){
  var totalPrice = 0;
  $(".checkout-product-price").each(function () {
    var cenaEach = parseFloat($(this).text().split(" ").join(""));
    totalPrice += cenaEach;
});
$(".total-price").text(totalPrice + " Դ");
$(".order-sum-price").text(totalPrice + " Դ");
if(totalPrice==0){
  $('.free-shiping').text("")
}
else if(totalPrice<5000){
  $('.shipping-variant').css("display","block")
  $('.free-shiping').text("500 Դ")
}
}
totalPrice()

$(".close-checkout").on("click", function () {
 var x = $(this).parents('.product-row').remove();
 
  countOfOrder()
  totalPrice()
  // console.log($(".checkout-table").children().length)
  // if(.length==0){
  //   $(".checkout-table").remove()
  // }
  // console.log(x.length)
  
  // var totalPrice = 0;
  // $(".eachPrice").each(function () {
  //     var cenaEach = parseFloat($(this).text());
  //     totalPrice += cenaEach;
  // });
  // $("#total-price").text(totalPrice + "$");
  // $("#items-basket").text("(" + ($("#list-item").children().length) + ")");
});


function openAccountDetail(evt, cityName) {
  var i, tabcontent, tablinks;
  tabcontent = document.getElementsByClassName("tabcontent");
  for (i = 0; i < tabcontent.length; i++) {
    tabcontent[i].classList.remove("block");
  }
  tablinks = document.getElementsByClassName("tablinks");
  for (i = 0; i < tablinks.length; i++) {
    tablinks[i].className = tablinks[i].className.replace(" active", "");
  }
  document.getElementById(cityName).classList.add("block");
  evt.currentTarget.className += " active";
}

// Get the element with id="defaultOpen" and click on it
//document.getElementById("defaultOpen").click();


// const boxes = document.querySelectorAll('.address-detail');

// for (const box of boxes) {
//   box.addEventListener('click', function handleClick() {
//     box.classList.toggle('is-active');
//   });
// }




var modalBackdrop = document.querySelector(".modal-backdrop")

var editAddress = document.querySelectorAll('.edit-address');
var changeAddress = document.querySelector('.change-address');
for (var add of editAddress) {
  add.addEventListener('click', function () {
    changeAddress.classList.add("is-open");
    modalBackdrop.style.display = "block"
  });
}

var addressForm = document.querySelector('.address-form');
addressForm.addEventListener('click', function (e) {
  e.preventDefault()
});

var cancelAction = document.querySelector('.cancel-action');
cancelAction.addEventListener('click', function (e) {
  changeAddress.classList.remove("is-open")
  modalBackdrop.style.display = "none"
});



var addNewAddress = document.querySelector('.add-new-address');
var addAddress = document.querySelector('.add-address');
addNewAddress.addEventListener('click', function () {
  addAddress.classList.add("is-open")
  modalBackdrop.style.display = "block"
});

var addAddressForm = document.querySelector('.add-address-form');
addAddressForm.addEventListener('click', function (e) {
  e.preventDefault()
});

var cancelActionAddress = document.querySelector('.cancal-adding-address');
cancelActionAddress.addEventListener('click', function (e) {
  addAddress.classList.remove("is-open")
  modalBackdrop.style.display = "none"

});


var changeDataIcon = document.querySelector('.change-data-icon');
var changeData = document.querySelector('.change-data');
changeDataIcon.addEventListener('click', function () {
  changeData.classList.add("is-open")
  modalBackdrop.style.display = "block"
});

var changeDataForm = document.querySelector('.change-data-form');
changeDataForm.addEventListener('click', function (e) {
  e.preventDefault()
});

var cancelChangeData = document.querySelector('.cancal-change-data');
cancelChangeData.addEventListener('click', function (e) {
  changeData.classList.remove("is-open")
  modalBackdrop.style.display = "none"
});


var seeOrder = document.querySelectorAll('.see-order');
var orderData = document.querySelector('.order-data');
for (var order of seeOrder) {
  order.addEventListener('click', function () {
    orderData.classList.add("is-open")
    modalBackdrop.style.display = "block"
  });
}

var cancelChangeData = document.querySelector('.close-order-popup');
cancelChangeData.addEventListener('click', function (e) {
  orderData.classList.remove("is-open")
  modalBackdrop.style.display = "none"
});


// Accordian
var action="click";
var speed="0";

$(document).ready(function() {
    // Question handler
    $('div.q').on(action, function() {

        // Get next element
        $(this).next()
            .slideToggle(speed)
            
            
        // Select all other answers
                .siblings('div.a')
                    .slideUp();              
                        $("div.q").removeClass('rotate')
                        $(this).addClass("rotate")
    });
});
// function deleteRow(btn) {
//   if(!confirm("Are you sure you want to delete?")) return;

//   // var tbl = el.parentNode.parentNode.parentNode;
//   // var row = el.parentNode.parentNode.rowIndex;
//   // tbl.deleteRow(row);
//   var row = btn.parentNode.parentNode;
//   row.parentNode.removeChild(row);
// }


