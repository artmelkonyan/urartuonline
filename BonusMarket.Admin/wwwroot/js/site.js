// $(document).ready(function () {
//
//     let bookCont = $('.book-cat-item-body');
//     let imgRowArr = [];
//     for (let i = 0; i < bookCont.length; i++) {
//         let height = $(bookCont[i]).find('.book-cont img')[0].height;
//         console.log(height)
//
//    
//         imgRowArr.push(height);
//
//         let maxHeight = Math.max(...imgRowArr);
//         $(bookCont[i]).find('.book-img-cont ').css('height', maxHeight + 'px');
//     }
//
//
// 	$("input:file").change(function () {
// 		var fileName = $(this).val();
// 		if (fileName.length > 0) {
// 			$(this).parent().children('span').html(fileName);
// 		}
// 		else {
// 			$(this).parent().children('span').html("Choose file");
//
// 		}
// 	});
// 	//file input preview
// 	function readURL(input) {
// 		if (input.files && input.files[0]) {
// 			var reader = new FileReader();
// 			reader.onload = function (e) {
// 				$('.logoContainer img').attr('src', e.target.result);
// 			}
// 			reader.readAsDataURL(input.files[0]);
// 		}
// 	}
// 	$("input:file").change(function () {
// 		readURL(this);
// 	});
//
//
// 	function bs_input_file() {
// 		$(".input-file").before(
// 			function () {
// 				if (!$(this).prev().hasClass('input-ghost')) {
// 					var element = $("<input type='file' class='input-ghost' style='visibility:hidden; height:0'>");
// 					element.attr("name", $(this).attr("name"));
// 					element.change(function () {
// 						element.next(element).find('input').val((element.val()).split('\\').pop());
// 					});
// 					$(this).find("button.btn-choose").click(function () {
// 						element.click();
// 					});
// 					$(this).find("button.btn-reset").click(function () {
// 						element.val(null);
// 						$(this).parents(".input-file").find('input').val('');
// 					});
// 					$(this).find('input').css("cursor", "pointer");
// 					$(this).find('input').mousedown(function () {
// 						$(this).parents('.input-file').prev().click();
// 						return false;
// 					});
// 					return element;
// 				}
// 			}
// 		);
// 	}
// 	$(function () {
// 		bs_input_file();
// 	});
// });
