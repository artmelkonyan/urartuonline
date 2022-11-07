function RegisterValidate(elem) {
    debugger;
    
    $('#register_bock').find('.validatereg').each(function () {
        $(this).trigger('onblur');
        
    });
    let counter = 0;
    $('#register_bock').find('input').each(function () {
        if ($(this).next().is('span')) {
            ++counter;
        }
    })
    if (counter == 0) {
        return true;
    }
    return false;
}

function isValid(element, minlength, maxlength) {
    debugger
    let required = 'Դաշտը պարտադիր է';
    let mintext = 'Դաշտի մինիմում սիմվոլների քանակը պետք է գերազանցի ' + minlength + ' ը';
    let passText = 'Գաղտնաբառերը չեն համընկնում';
    if (($(element).val() == "" || $(element).val() == null) && !$(element).hasClass('reqval')) {
        $(element).addClass('reqval');
        $(element).after('<span class="validate">' + required + '</span>');
    } else if ($(element).val().length > 0) {
        if ($(element).hasClass('reqval')) {
            $(element).removeClass('reqval');
        }
        $(element).next().remove();
    }
    if (minlength) {
        if ($(element).val().length < minlength && !$(element).hasClass('reqval')) {
            $(element).addClass('reqval');
            $(element).after('<span class="validate">' + mintext + '</span>');
        } else if ($(element).val().length > minlength)
        {
            $(element).removeClass('reqval');
            $(element).next().remove();
        }
    }
    var pass = $('.passval');
    if ($(pass[0]).val() != $(pass[1]).val()) {
        $(pass[1]).next().remove();
        if ($(pass[1]).hasClass('reqval')) {
            $($('.passval')[1]).removeClass('reqval');
        }
        $(pass[1]).after('<span class="validate">' + passText + '</span>')
    } else if (!$(pass[1]).hasClass('reqval') && $($('.passval')[1]).next().is('span')) {
        $($('.passval')[1]).next().remove();
    }
}


