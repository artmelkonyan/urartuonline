// ContactTheme controller

/*****Main Function******/
var ContactThemeControllerClass = function () {
    var _self = this;

    function init() {
        console.log('ContactTheme controller init');

        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.DateService.BindDatePicker();
    }
};
/************************/


// creating class instance
var ContactThemeController = new ContactThemeControllerClass();

// creating object
var ContactThemeControllerObject = {
    // important
    Name: "contacttheme",

    Controller: ContactThemeController
}

// registering controller object
_core.addController(ContactThemeControllerObject);

// Hall controller

/*****Main Function******/
var HallControllerClass = function () {
    var _self = this;

    function init() {
        console.log('Hall controller init');


        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditHall = _core.getService('ItemEdit').Service;
        _self.VideoHall = _core.getService('Video').Service;
        _self.SelectizeService = _core.getService('Selectize').Service;
        _self.TinyMCEHall = _core.getService('TinyMCE').Service;
        _self.DateHall = _core.getService('Date').Service;
        _self.TableHall = _core.getService('Table').Service;
        _self.PostService = _core.getService('Post').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableHall.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEHall.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditHall.Bind();

        _self.SelectizeService.Bind();

        _self.VideoHall.Bind();

        _self.DateHall.BindDatePicker();

        _self.SelectizeService.BindChangeEvent('#locations', function(e) {
            _self.PostService.postJson({value:e}, '/location/getitems', function(res) {

                if (res.status) {
                    $itemSelectize = $('#locationitems')[0].selectize;

                    $itemSelectize.clearOptions();
                    if (typeof res.items !== 'undefined') {
                        for(let i in res.items) {
                            let curItem = res.items[i];
                            $itemSelectize.addOption({value: curItem.id, text: curItem.name});
                        }
                    }
                    $itemSelectize.refreshOptions();
                }
            })
        });
    }
};
/************************/


// creating class instance
var HallController = new HallControllerClass();

// creating object
var HallControllerObject = {
    // important
    Name: "hall",

    Controller: HallController
}

// registering controller object
_core.addController(HallControllerObject);

// Home controller

/*****Main Function******/
var HomeControllerClass = function () {
    var _self = this;

    function init() {
        console.log('Home controller init');

        _self.imageHolder = _core.getService("Image").Service;
        _self.imageService = new _self.imageHolder();

        _self.imageTranslateService = new _self.imageHolder();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.imageTranslateService.Bind('image_translates');

        _self.DateService.BindDatePicker();
    }
};
/************************/


// creating class instance
var HomeController = new HomeControllerClass();

// creating object
var HomeControllerObject = {
    // important
    Name: "home",

    Controller: HomeController
}

// registering controller object
_core.addController(HomeControllerObject);

// Location controller

/*****Main Function******/
var LocationControllerClass = function () {
    var _self = this;

    function init() {
        console.log('Location controller init');

        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.DateService.BindDatePicker();
    }
};
/************************/


// creating class instance
var LocationController = new LocationControllerClass();

// creating object
var LocationControllerObject = {
    // important
    Name: "location",

    Controller: LocationController
}

// registering controller object
_core.addController(LocationControllerObject);

// LocationItem controller

/*****Main Function******/
var LocationItemControllerClass = function () {
    var _self = this;

    function init() {
        console.log('LocationItem controller init');

        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();

        bindComponentEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.DateService.BindDatePicker();
    }
};
/************************/


// creating class instance
var LocationItemController = new LocationItemControllerClass();

// creating object
var LocationItemControllerObject = {
    // important
    Name: "locationitem",

    Controller: LocationItemController
}

// registering controller object
_core.addController(LocationItemControllerObject);

// News controller

/*****Main Function******/
var NewsControllerClass = function () {
    var _self = this;

    _self.ViewHolder = null;
    _self.WidgetHolder = null;
    _self.deleteURL = "/news/delete";


    _self.mode = "Add"; // initial mode

    _self.init = function () {
        console.log('News controller inited with action');

        _self.ViewHolder = $('#viewholder');
        _self.WidgetHolder = $('#widgetHolder');

    };

    _self.Add = function(){
        bindEditEvents()
    };

    _self.Edit = function(){

        bindEditEvents();
    };

    // index action
    _self.Index = function () {
        console.log('index called');

        _self.init();

        // bind buttons
        bindButtons();
    };

    // js bindings
    function bindButtons() {

        var deleteElems = $(".deleteElems");
        var addBtn = $("#addBtn");
        var delBtn = $("#delBtn");
        var editElem = $(".editElem");
        var deleteElem = $(".deleteElem");
        var elemLangs = $("#elemLangs");

        deleteElems.unbind().change(function(e){
            e.preventDefault();
            var checked = false;
            if ($(this).is(":checked")) {
                checked = true;
            }

            $('.delItems').each(function(){
                $(this).prop("checked", checked);
            });
        });

        addBtn.unbind().click(function(e){
            e.preventDefault();

            location.href = $(this).data("href");
        });

        delBtn.unbind().click(function(e){
            e.preventDefault();
            var list = [];

            $('.delItems').each(function(){
                if ($(this).is(":checked")) {
                    list.push($(this).data('id'));
                }
            });

            removeItems(list);
        });

        editElem.unbind().click(function(e){
            e.preventDefault();

            location.href = $(this).data("href");
        });

        deleteElem.unbind().click(function(e){
            e.preventDefault();

            var id = $(this).data('id');
            var list = [];
            list.push(id);

            removeItems(list);
        });

        elemLangs.unbind().change(function(e){
            e.preventDefault();

            location.href = $(this).val();
        });

        tinymce.init({            relative_urls : false,            remove_script_host : false,
            selector: 'textarea',
            plugins: [
                'advlist autolink lists link image charmap print preview hr anchor pagebreak',
                'searchreplace wordcount visualblocks visualchars code fullscreen',
                'insertdatetime media nonbreaking save table contextmenu directionality',
                'emoticons template paste textcolor colorpicker textpattern imagetools codesample toc'
            ],
            toolbar1: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            toolbar2: 'print preview media | forecolor backcolor emoticons | codesample',
            image_advtab: true,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ],
            content_css: [
                '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
                '//www.tinymce.com/css/codepen.min.css'
            ]
        });

    }

    // bind add and edit events
    function bindEditEvents() {

        var lgElem = $('.lgElem');
        lgElem.unbind().bind('change', function() {

            var link = location.href;
            var linkArray = link.split("?");
            location.href = linkArray[0] + '?lg='+$(this).val();
        });

        tinymce.init({            relative_urls : false,            remove_script_host : false,
            selector: 'textarea',
            plugins: [
                'advlist autolink lists link image charmap print preview hr anchor pagebreak',
                'searchreplace wordcount visualblocks visualchars code fullscreen',
                'insertdatetime media nonbreaking save table contextmenu directionality',
                'emoticons template paste textcolor colorpicker textpattern imagetools codesample toc'
            ],
            toolbar1: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            toolbar2: 'print preview media | forecolor backcolor emoticons | codesample',
            image_advtab: true,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ],
            content_css: [
                '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
                '//www.tinymce.com/css/codepen.min.css'
            ]
        });


        $(".Dates").daterangepicker({
            locale: {
                format: 'YYYY-MM-DD'
            },
            singleDatePicker: true,
            showDropdowns: true,
            "buttonClasses": "btn btn-sm btn-flat",
            "applyClass": "color-success-bg color-success-hover-bg color-text-white",
            "cancelClass": "btn-default"
        });

    }

    function removeItems(list) {

        _core.getService("Post").Service.postJson({'ids' : list}, _self.deleteURL, function(res) {

            // location reload
            location.reload();
        })
    }
};
/************************/


// creating class instance
var NewsController = new NewsControllerClass();

// creating object
var NewsControllerObject = {
    // important
    Name: "news",

    Controller: NewsController
}

// registering controller object
_core.addController(NewsControllerObject);

// Page controller

/*****Main Function******/
var PageControllerClass = function () {
    var _self = this;

    function init() {
        console.log('Page controller init');

        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
        _self.SelectizeService = _core.getService('Selectize').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.DateService.BindDatePicker();

        // _self.SelectizeService.BindWithCreate('/page/addTheme', '/page/removeTheme');
    }
};
/************************/


// creating class instance
var PageController = new PageControllerClass();

// creating object
var PageControllerObject = {
    // important
    Name: "page",

    Controller: PageController
}

// registering controller object
_core.addController(PageControllerObject);

// SendEmail controller

/*****Main Function******/
var SendEmailControllerClass = function () {
    var _self = this;

    function init() {
        console.log('SendEmail controller init');


        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditSendEmail = _core.getService('ItemEdit').Service;
        _self.VideoSendEmail = _core.getService('Video').Service;
        _self.SelectizeSendEmail = _core.getService('Selectize').Service;
        _self.TinyMCESendEmail = _core.getService('TinyMCE').Service;
        _self.DateSendEmail = _core.getService('Date').Service;
        _self.TableSendEmail = _core.getService('Table').Service;
        _self.PostService = _core.getService('Post').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableSendEmail.Bind();

        $('.viewSendEmail').unbind().bind('click', function(e) {
            e.preventDefault();

            let id = $(this).data('id');

            if (id === null || typeof id === 'undefined')
                return null;

            _self.PostService.postPartial({id: id}, '/sendemail/viewmodal', function(res) {
                if (res === null)
                    return null;

                $('.viewSendEmailModalHolder').html(res);

                $('#viewSendEmailModel').modal('show');

                bindSendEmailBtnEvents();
            });
        });
    }

    function bindSendEmailBtnEvents() {
        $('.sendEmailBtn').unbind().bind('click', function(e) {
            e.preventDefault();

            let id = $(this).data('id');


            if (id === null || typeof id === 'undefined')
                return null;

            _self.PostService.postPartial({id: id}, '/sendemail/send', function(res) {
                if (res === null)
                    return null;

                $('#viewSendEmailModel').modal('hide');
            });
        });
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCESendEmail.BindSendMail();

        _self.imageService.Bind('images');

        _self.ItemEditSendEmail.Bind();

        _self.SelectizeSendEmail.Bind();

        _self.VideoSendEmail.Bind();

        _self.DateSendEmail.BindDatePicker();
    }
};
/************************/


// creating class instance
var SendEmailController = new SendEmailControllerClass();

// creating object
var SendEmailControllerObject = {
    // important
    Name: "sendemail",

    Controller: SendEmailController
}

// registering controller object
_core.addController(SendEmailControllerObject);

// Service controller

/*****Main Function******/
var ServiceControllerClass = function () {
    var _self = this;

    function init() {
        console.log('Service controller init');

        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.VideoService = _core.getService('Video').Service;
        _self.SelectizeService = _core.getService('Selectize').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
        _self.PostService = _core.getService('Post').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.SelectizeService.Bind();

        _self.VideoService.Bind();

        _self.DateService.BindDatePicker();
    }
};
/************************/


// creating class instance
var ServiceController = new ServiceControllerClass();

// creating object
var ServiceControllerObject = {
    // important
    Name: "service",

    Controller: ServiceController
}

// registering controller object
_core.addController(ServiceControllerObject);

// Service controller

/*****Main Function******/
var ServiceControllerClass = function () {
    var _self = this;

    function init() {
        console.log('Service controller init');

        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.VideoService = _core.getService('Video').Service;
        _self.SelectizeService = _core.getService('Selectize').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
        _self.PostService = _core.getService('Post').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.SelectizeService.Bind();

        _self.VideoService.Bind();

        _self.DateService.BindDatePicker();

        _self.SelectizeService.BindChangeEvent('#locations', function(e) {
            _self.PostService.postJson({value:e}, '/location/getitems', function(res) {

                if (res.status) {
                    $itemSelectize = $('#locationitems')[0].selectize;

                    $itemSelectize.clearOptions();
                    if (typeof res.items !== 'undefined') {
                        for(let i in res.items) {
                            let curItem = res.items[i];
                            $itemSelectize.addOption({value: curItem.id, text: curItem.name});
                        }
                    }
                    $itemSelectize.refreshOptions();
                }
            })
        });
    }
};
/************************/


// creating class instance
var ServicesController = new ServiceControllerClass();

// creating object
var ServiceControllerObject = {
    // important
    Name: "shows",

    Controller: ServicesController
}

// registering controller object
_core.addController(ServiceControllerObject);

// Sub controller

/*****Main Function******/
var SubControllerClass = function () {
    var _self = this;

    function init() {
        console.log('Sub controller init');


        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditSub = _core.getService('ItemEdit').Service;
        _self.VideoSub = _core.getService('Video').Service;
        _self.SelectizeSub = _core.getService('Selectize').Service;
        _self.TinyMCESub = _core.getService('TinyMCE').Service;
        _self.DateSub = _core.getService('Date').Service;
        _self.TableSub = _core.getService('Table').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableSub.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCESub.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditSub.Bind();

        _self.SelectizeSub.Bind();

        _self.VideoSub.Bind();

        _self.DateSub.BindDatePicker();
    }
};
/************************/


// creating class instance
var SubController = new SubControllerClass();

// creating object
var SubControllerObject = {
    // important
    Name: "sub",

    Controller: SubController
}

// registering controller object
_core.addController(SubControllerObject);

// Tag controller

/*****Main Function******/
var TagControllerClass = function () {
    var _self = this;

    function init() {
        console.log('Tag controller init');

        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.DateService.BindDatePicker();
    }
};
/************************/


// creating class instance
var TagController = new TagControllerClass();

// creating object
var TagControllerObject = {
    // important
    Name: "tag",

    Controller: TagController
}

// registering controller object
_core.addController(TagControllerObject);

// TagItem controller

/*****Main Function******/
var TagItemControllerClass = function () {
    var _self = this;

    function init() {
        console.log('TagItem controller init');

        _self.imageService = _core.getService("Image").Service;
        _self.imageService = new _self.imageService();
        _self.ItemEditService = _core.getService('ItemEdit').Service;
        _self.TinyMCEService = _core.getService('TinyMCE').Service;
        _self.DateService = _core.getService('Date').Service;
        _self.TableService = _core.getService('Table').Service;
    }

    _self.Add = function(){

        init();

        bindComponentEvents();
    };

    _self.Edit = function(){

        init();

        bindComponentEvents();
    };

    _self.Index = function () {

        init();

        bindMainEvents();

        bindComponentEvents();
    };

    function bindMainEvents() {

        _self.TableService.Bind();
    }

    // bind add and edit events
    function bindComponentEvents() {

        _self.TinyMCEService.Bind();

        _self.imageService.Bind('images');

        _self.ItemEditService.Bind();

        _self.DateService.BindDatePicker();
    }
};
/************************/


// creating class instance
var TagItemController = new TagItemControllerClass();

// creating object
var TagItemControllerObject = {
    // important
    Name: "tagitem",

    Controller: TagItemController
}

// registering controller object
_core.addController(TagItemControllerObject);

// Date service

/*****Main Function******/
var DateServiceClass = function () {
    var _self = this;

    function init() {

    }

    _self.Bind = function() {
        init();
    }

    _self.BindDatePicker = function() {

        $(".Dates").daterangepicker({
            locale: {
                format: 'YYYY-MM-DD'
            },
            singleDatePicker: true,
            showDropdowns: true,
            "buttonClasses": "btn btn-sm btn-flat",
            "applyClass": "color-success-bg color-success-hover-bg color-text-white",
            "cancelClass": "btn-default"
        });
    }
}
/************************/


// creating class instance
var DateService = new DateServiceClass();

// creating object
var DateServiceObject = {
    // important
    Name: 'Date',

    Service: DateService
}

// registering controller object
_core.addService(DateServiceObject);

// Image service

/*****Main Function******/
var ImageServiceClass = function () {
    var _self = this;
    _self.name = 'images';
    _self.selectedFileIds = [];
    _self.loaderService = null;



    function init(name) {
        _self.name = name;

        _self.coversContentLoaderClass = 'productsGalleryWrapBodyCur'+_self.name;
        _self.coverImageModalLoaderClass = 'coverImageModalLoader'+_self.name;
        _self.imgItemWrapper = $('#imgItemWrapper'+_self.name);
        _self.coverTabHolder = $('#coverTabHolder'+_self.name);
        _self.modalTabSelectFolder = $('#modalTabSelectFolder'+_self.name);
        _self.plUploaderElem = $("#plupload-imageUploader"+_self.name);
        _self.coverImgApproveBtn = $('#coverImgApproveBtn'+_self.name);
        _self.productsGalleryWrapBodyCurRow = $('#productsGalleryWrapBodyRow'+_self.name);
        _self.coverImageModal = $('#coverImageModal'+_self.name);
        _self.selectAllCoverImages = $('#selectAllCoverImages'+_self.name);

        _self.loaderService = _core.getService("Loading").Service;

        bindModalEvents();

        bindCoversForContent();
    }

    _self.Bind = function(name) {
        init(name);
    };


    function bindModalEvents() {
        $('#openAddCoverModal'+_self.name).unbind().bind('click', function (e) {
            _self.coverImageModal.modal('show');

            bindAddModalEvents();
        });
        $('#openDeleteCoverModal'+_self.name).unbind().bind('click', function (e) {
            $('#coverImageDeleteModal'+_self.name).modal('show');

            bindDeleteModalEvents();
        });

        _self.selectAllCoverImages.unbind().bind('change', function(e) {
            var checked = $(this).is(':checked');

            $('.coversContentItemDeleteCheckbox'+_self.name).each(function() {
                $(this).prop('checked', checked);
            })
        })
    }

    function bindAddModalEvents() {

        _self.selectedFileIds = [];

        _self.coverTabHolder.html('');
        _self.imgItemWrapper.html('');

        bindCoverTabEvents();

        bindGeneralEvents();
    }

    function bindDeleteModalEvents() {

        $('#coverImageDeleteModalApprove'+_self.name).unbind().bind('click', function () {

            var list = [];

            $('.coversContentItemDeleteCheckbox'+_self.name).each(function(e) {
                if ($(this).is(':checked')) {
                    list.push($(this).data('id'));
                }
            });

            removeCoversContentItem(list);

            _self.selectAllCoverImages.prop('checked', false);
        })
    }
    function bindGeneralEvents() {
        _self.coverImgApproveBtn.unbind().bind('click', function(e) {

            var coverContentIdList = filterCoverIdsByContent();

            getCoversForContent(coverContentIdList);

            _self.coverImageModal.modal('hide');
        });
    }

    function bindCoverTabEvents() {
        $('#covertab'+_self.name).unbind().bind('click', function(e) {
            _self.coverTabHolder.html('');

            _self.getCoverImages();
        });


        $('#pluploadtab'+_self.name).unbind().bind('click', function(e) {

            bindPluploaderEvents();
        });

        $('#coverselecttab'+_self.name).unbind().bind('click', function(e) {
            _self.imgItemWrapper.html('');

            bindCoverSelectEvents();
        });
    }

    function bindPluploaderEvents() {
        _self.plUploaderElem.pluploadQueue({
            // General settings
            runtimes : 'html5,flash,silverlight,html4',
            url : "/image/imgUpload",

            chunk_size : '1mb',
            rename : true,
            dragdrop: true,

            filters : {
                // Maximum file size
                max_file_size : '10mb',
                // Specify what files to browse for
                mime_types: [
                    {title : "Image files", extensions : "jpg,gif,png"},
                    {title : "Zip files", extensions : "zip"}
                ]
            },

            // Resize images on clientside if we can
            // resize: {
            //     width : 200,
            //     height : 200,
            //     quality : 90,
            //     crop: false // crop to exact dimensions
            // },


            // Post init events, bound after the internal events
            init : {
                FileUploaded: function(up, file, info) {
                    // Called when file has finished uploading
                    var id = JSON.parse(info.response).id;
                    addImage(id);
                },
                UploadComplete: function(up, files) {
                    // _self.getSelectedFiles();
                    up.destroy();
                }
            },

            // Flash settings
            flash_swf_url : '/plupload/js/Moxie.swf',

            // Silverlight settings
            silverlight_xap_url : '/plupload/js/Moxie.xap'
        });
    }

    // get selected images
    _self.getCoverImages = function() {

        _self.loaderService.enableByClass(_self.coverImageModalLoaderClass);
        _core.getService("Post").Service.postPartial({'ids':_self.selectedFileIds, 'name': _self.name}, "/image/getCoverImages", function (res) {
            // loader
            _self.loaderService.disableByClass(_self.coverImageModalLoaderClass);

            _self.coverTabHolder.html(res);
            bindCoverImageEvents();
        })
    };


    function bindCoverImageEvents() {
        $('.coverImageItem'+_self.name).unbind().bind('click', function(e) {
            e.preventDefault();
            
            var id = $(this).data('id');

            remImage(id);

            _self.getCoverImages();
        })
    }

    function bindCoverSelectEvents() {
        _self.loaderService.enableByClass(_self.coverImageModalLoaderClass);
        _core.getService("Post").Service.postPartial({'ids':_self.selectedFileIds, 'name':_self.name}, "/image/getCoverImageDates", function (res) {
            // loader
            _self.loaderService.disableByClass(_self.coverImageModalLoaderClass);

            _self.modalTabSelectFolder.html(res);
            bindCoverDatesEvents();
        })
    }

    function bindCoverDatesEvents() {
        _self.modalTabSelectFolder.unbind().bind('change', function(e) {
            var val = $(this).val();

            if (val !== '') {
                getCoversByDate(val);
            }
        });
    }

    function getCoversByDate(date) {
        _self.loaderService.enableByClass(_self.coverImageModalLoaderClass);
        _core.getService("Post").Service.postPartial({'date':date, 'name':_self.name}, "/image/getCoversByDate", function (res) {
            // loader
            _self.loaderService.disableByClass(_self.coverImageModalLoaderClass);

            _self.imgItemWrapper.html(res);
            bindCoversByDateEvents();
        })
    }

    function getCoversForContent(coverContentIdList) {

        _self.loaderService.enableByClass(_self.coversContentLoaderClass);
        _core.getService("Post").Service.postPartial({'ids':coverContentIdList, 'name':_self.name}, "/image/getCoversForContent", function (res) {
            // loader
            _self.loaderService.disableByClass(_self.coversContentLoaderClass);

            appendCoversContent(res);
        })
    }

    function bindCoversForContent() {
        $('.deleteCoversContentItem'+_self.name).unbind().bind('click', function(e) {

            e.preventDefault();

            var image_id = $(this).data('id');

            var list = [];
            list.push(image_id);

            removeCoversContentItem(list);
        })


        $( ".sortableImages" ).sortable();
    }

    function removeCoversContentItem(list) {

        $('.productsGalleryItemCur'+_self.name).each(function(e) {

            var curId = $(this).data('id');
            if ($.inArray(curId, list) !== -1) {
                $(this).remove();
            };
        });
    }

    function bindCoversByDateEvents() {
        $('.selectedImageSelect'+_self.name).unbind().bind('click', function (e) {
            var image_id = $(this).data('id');

            $(this).addClass('disabled');

            addImage(image_id);
        })
    }

    function addImage(id) {

        remImage(id);

        _self.selectedFileIds.push(id);

        checkSaveBtn();
    }

    function remImage(id) {
        _self.selectedFileIds = $.grep(_self.selectedFileIds, function(e){
            return e !== id;
        });
        checkSaveBtn();
    }

    function checkSaveBtn() {
        var empty = _self.selectedFileIds.length === 0;

        if (empty && !_self.coverImgApproveBtn.hasClass('disabled')) {
            _self.coverImgApproveBtn.addClass('disabled');
        } else {
            _self.coverImgApproveBtn.removeClass('disabled');
        }
    }

    function appendCoversContent(res) {

        _self.productsGalleryWrapBodyCurRow.append(res);

        bindCoversForContent();
    }

    function filterCoverIdsByContent() {

        var list = _self.selectedFileIds.slice();

        $('.productsGalleryItemCur'+_self.name).each(function(e) {
            var image_id = $(this).data('id');

            list = $.grep(list, function(e){
                return e !== image_id;
            });
        });

        return list;
    }

};
/************************/


// creating class instance
var ImageService = new ImageServiceClass();

// creating object
var ImageServiceObject = {
    // important
    Name: 'Image',

    Service: ImageServiceClass
}

// registering controller object
_core.addService(ImageServiceObject);

// ItemEdit service

/*****Main Function******/
var ItemEditServiceClass = function () {
    var _self = this;

    _self.itemEditParams = $('#itemEditParams');
    _self.itemEditForm = $('#itemEditForm');
    _self.loaderService = null;
    _self.postService = null;

    function init() {

        _self.loaderService = _core.getService("Loading").Service;
        _self.postService = _core.getService("Post").Service;

        bindButtonEvents();
    }

    _self.Bind = function() {
        init();

    };

    function bindButtonEvents() {
        $('#itemEditSave').unbind().bind('click', function (e) {

            e.preventDefault();

            saveContent();
        });

    }

    function saveContent() {
        var url = _self.itemEditParams.data('url');

        if (validateData()) {
            var data = collectSendData();

            _self.loaderService.enableByClass('loaderHolder');

            _self.postService.postJson(data, url, function(res) {

                _self.loaderService.disableByClass('loaderHolder');

                location.href=res.redirect_url;
            })
        }

    }

    function collectSendData() {

        var data = {};

        data['id'] = _self.itemEditParams.data('id');
        data['lg'] = _self.itemEditParams.data('lg');
        data['vc'] = _self.itemEditParams.data('vc');

        $('.collectItemEdit').each(function(e) {

            var name = $(this).attr('name');

            if ($(this).is('input')) {
                data[name] = $(this).val();
            } else if ($(this).is('textarea')) {
                data[name] = tinyMCE.get($(this).attr('id')).getContent();
            } else if ($(this).is('select')) {
                data[name] = $(this)[0].selectize.getValue();
            }
        });

        $('.productsGalleryItem').each(function(e) {

            var name = $(this).data('name');
            var id = $(this).data('id');

            if (typeof data[name] === 'undefined')
                data[name] = [];

            data[name].push(id);
        });

        return data;
    }

    function validateData() {
        _self.itemEditForm.validate({
            rules: generateValidateRule()
        });

        var validForm = _self.itemEditForm.valid();

        var valid = true;

        $('.collectItemEdit').each(function(e) {

            var validate = $(this).data('valid');

            if (validate !== 1)
                return;

            if ($(this).is('textarea')) {
                if (tinyMCE.activeEditor.getContent() === '')
                    valid = false;
            } else if ($(this).is('select')) {
                var val  =$(this)[0].selectize.getValue();
                if (val === '' || val.length === 0)
                    valid = false;
            }
        });

        return (valid && validForm);
    }

    function generateValidateRule() {
        var rule = {};

        $('.collectItemEdit').each(function(e) {
            var validate = $(this).data('valid');
            var name = $(this).attr('name');

            if ($(this).is('input') && validate === 1) {
                rule[name] = {
                    required: true
                }
            }
        });

        return rule;
    }

};
/************************/


// creating class instance
var ItemEditService = new ItemEditServiceClass();

// creating object
var ItemEditServiceObject = {
    // important
    Name: 'ItemEdit',

    Service: ItemEditService
};

// registering controller object
_core.addService(ItemEditServiceObject);

// loading service

/*****Main Function******/
var LoadingServiceClass = function () {
    var self = this;

    // enable loader
    self.enableByClass = function (loaderDivClass) {
        var elem = $('.' + loaderDivClass);

        elem.addClass('loading blur');
    };

    // disable laoder
    self.disableByClass = function(loaderDivClass) {
        var elem = $('.' + loaderDivClass);
        elem.removeClass('loading blur');
    }
};
/************************/


// creating class instance
var LoadingService = new LoadingServiceClass();

// creating object
var LoadingServiceObject = {
    // important
    Name: 'Loading',

    Service: LoadingService
}

// registering controller object
_core.addService(LoadingServiceObject);

// Merge service

/*****Main Function******/
var MergeServiceClass = function () {
    var _self = this;
    _self.modalUrl = "/_Popup_/Get_MergeApprove";
    _self.approveUrl = '/Merge/ApproveData'
    _self.ModalName = "merge-approve-modal";

    function init() {

    }

    _self.bindPrisonerMerges = function (callback) {
        _self.callback = callback;
        bindEvents();
    }

    function bindEvents() {
        var mergeApproveRow = $('.mergeApproveRow');

        mergeApproveRow.unbind().bind('click', function () {
            var currentID = $(this).data('id');

            _core.getService('Loading').Service.enableBlur('loaderClass');

            loadView(currentID);
        })
    }

    function loadView(currentID) {

        var data = {};

        data.ID = currentID;

        _core.getService("Post").Service.postPartial(data, _self.modalUrl, function (res) {

            _self.content = res;

            // content
            $('#widgetHolder').html(_self.content);

            // bind merge approve events
            bindMergeApproveEvents();

            _core.getService('Loading').Service.disableBlur('loaderClass');

            // showing modal
            $('.' + _self.ModalName).modal('show');
        });
    }

    function bindMergeApproveEvents() {
        var approveModalBtn = $('#approveModalBtn');
        var declineModalBtn = $('#declineModalBtn');

        approveModalBtn.unbind().bind('click', function () {
            var id = $(this).data('id');
            approveAnswer(id, true);
        });

        declineModalBtn.unbind().bind('click', function () {
            var id = $(this).data('id');
            approveAnswer(id, false);
        })
    }

    // approve or decline
    function approveAnswer(id, status) {
        var url = _self.approveUrl;
        var data = {};
        data['ID'] = id;
        data['State'] = status;
        _core.getService('Post').Service.postJson(data, url, function (res) {

            //hiding modal
            $('.' + _self.ModalName).modal('hide');

            _self.callback({});
        });
    }

}
/************************/


// creating class instance
var MergeService = new MergeServiceClass();

// creating object
var MergeServiceObject = {
    // important
    Name: 'Merge',

    Service: MergeService
}

// registering controller object
_core.addService(MergeServiceObject);

// post service

/*****Main Function******/
var PostServiceClass = function () {
    var self = this;
    self.loaderWidget = null;
    self.divClass = 'loaderClass';
    self.partialXHRList = [];
    
    // Json Post With return Datatype json
    self.postJsonReturn = function (data, url, callback) {
        if (typeof url != 'undefined') {
            try {
                // enable loader
                self.loaderWidget = _core.getService('Loading').Service;
                self.loaderWidget.enableBlur(self.divClass);

                $.ajax({
                    type: "POST",
                    url: url,
                    dataType: 'text',
                    beforeSend: function (xhr) { // for ASP.NET auto deserialization
                        xhr.setRequestHeader("Content-type", "application/json");
                    },
                    data: data,
                    statusCode: {
                        401: function () {
                            location.reload();
                        }
                    },
                    success: function (res, textStatus, xhr) {
                        // disable loader
                        self.loaderWidget = _core.getService('Loading').Service;

                        callback(JSON.parse(res));
                    },
                    error: function (xhr, textStatus, err) {
                        // disable loader
                        self.loaderWidget = _core.getService('Loading').Service;

                        callback(null);
                    }
                });
            } catch (err) {
                //console.log(err);
                callback(null);
            }
        }
        else {
            console.log('error', 'undefined parameter (plugins.post)');
            callback(null);
        }
    };

    // Json Post
    self.post = function (data, url, callback) {
        if (typeof url != 'undefined') {
            try {
                $.ajax({
                    type: "POST",
                    url: url,
                    dataType: 'text',
                    data: data,
                    statusCode: {
                        401: function () {
                            location.reload();
                        }
                    },
                    success: function (res, textStatus, xhr) {
                        callback(res);
                    },
                    error: function (xhr, textStatus, err) {
                        callback(null);
                    }
                });
            } catch (err) {
                //console.log(err);
                callback(null);
            }
        }
        else {
            console.log('error', 'undefined parameter (plugins.post)');
            callback(null);
        }
    }

    // Json Post
    self.postJson = function (data, url, callback) {
        if (typeof data != 'undefined' && typeof url != 'undefined') {
            try {
                // enable loader
                //self.loaderWidget = _core.getService('Loading').Service;
                //self.loaderWidget.enableBlur(self.divClass);

                $.ajax({
                    type: "POST",
                    url: url,
                    dataType: 'text',
                    beforeSend: function (xhr) { // for ASP.NET auto deserialization
                        xhr.setRequestHeader("Content-type", "application/json");
                    },
                    data: JSON.stringify(data),
                    statusCode: {
                        401: function () {
                            location.reload();
                        }
                    },
                    success: function (res, textStatus, xhr) {
                        // disable loader
                        //self.loaderWidget = _core.getService('Loading').Service;

                        callback(JSON.parse(res));
                    },
                    error: function (xhr, textStatus, err) {
                        // disable loader
                        self.loaderWidget = _core.getService('Loading').Service;

                        callback(null);
                    }
                });
            } catch (err) {
                //console.log(err);
                callback(null);
            }
        }
        else {
            console.log('error', 'undefined parameter (plugins.post)');
            callback(null);
        }
    }

    // Json Post
    self.postJsonAsync = function (data, url, callback) {

        return new Promise(function (resolve, reject) {
            if (typeof data != 'undefined' && typeof url != 'undefined') {
                try {
                    // enable loader
                    //self.loaderWidget = _core.getService('Loading').Service;
                    //self.loaderWidget.enableBlur(self.divClass);

                    $.ajax({
                        type: "POST",
                        url: url,
                        dataType: 'text',
                        beforeSend: function (xhr) { // for ASP.NET auto deserialization
                            xhr.setRequestHeader("Content-type", "application/json");
                        },
                        data: JSON.stringify(data),
                        statusCode: {
                            401: function () {
                                location.reload();
                            }
                        },
                        success: function (res, textStatus, xhr) {
                            // disable loader
                            //self.loaderWidget = _core.getService('Loading').Service;
                            resolve(JSON.parse(res));
                        },
                        error: function (xhr, textStatus, err) {
                            // disable loader
                            self.loaderWidget = _core.getService('Loading').Service;

                            reject(JSON.parse(err));
                        }
                    });
                } catch (err) {
                    //console.log(err);
                    callback(null);
                }
            } else {
                console.log('error', 'undefined parameter (plugins.post)');
                callback(null);
            }
        });
    };

    // post Partial
    self.postPartial = function (data, url, callback) {
        if (typeof data != 'undefined' && typeof url != 'undefined') {
            try {
                var xhr = $.ajax({
                    type: "POST",
                    url: url,
                    dataType: 'text',
                    beforeSend: function (xhr) { // for ASP.NET auto deserialization
                        xhr.setRequestHeader("Content-type", "application/json");
                    },
                    data: JSON.stringify(data),
                    statusCode: {
                        401: function () {
                            location.reload();
                        }
                    },
                    success: function (res, textStatus, xhr) {
                        // clean list
                        cleanPartialList();

                        // callback
                        callback(res);
                    },
                    error: function (xhr, textStatus, err) {
                        // clean list
                        cleanPartialList();

                        // callback
                        callback(null);
                    }
                });
                
                // clean list and push in new xhr
                cleanPartialList();
                self.partialXHRList.push(xhr);
            } catch (err) {
                //console.log(err);
                callback(null);
            }
        }
        else {
            console.log('error', 'undefined parameter (plugins.post)');
            callback(null);
        }
    }

    // post Partial async
    self.postPartialAsync = function (data, url, callback) {
        if (typeof data != 'undefined' && typeof url != 'undefined') {
            try {
                var xhr = $.ajax({
                    type: "POST",
                    url: url,
                    dataType: 'text',
                    beforeSend: function (xhr) { // for ASP.NET auto deserialization
                        xhr.setRequestHeader("Content-type", "application/json");
                    },
                    data: JSON.stringify(data),
                    statusCode: {
                        401: function () {
                            location.reload();
                        }
                    },
                    success: function (res, textStatus, xhr) {
                        // clean list
                        //cleanPartialList();

                        // callback
                        callback(res);
                    },
                    error: function (xhr, textStatus, err) {
                        // clean list
                        //cleanPartialList();

                        // callback
                        callback(null);
                    }
                });

                // clean list and push in new xhr
                //cleanPartialList();
                //self.partialXHRList.push(xhr);
            } catch (err) {
                //console.log(err);
                callback(null);
            }
        }
        else {
            console.log('error', 'undefined parameter (plugins.post)');
            callback(null);
        }
    }

    // FormData Post
    self.postFormData = function (data, url, callback) {
        if (typeof data != 'undefined' && typeof url != 'undefined') {
            try {
                // enable loader
                //self.loaderWidget = _core.getService('Loading').Service;
                //self.loaderWidget.enableBlur(self.divClass);

                $.ajax({
                    type: "POST",
                    url: url,
                    //processData: true,
                    processData: false,
                    contentType: false,
                    data: data,
                    //beforeSend: function (xhr) { // for ASP.NET auto deserialization
                    //    xhr.setRequestHeader("Content-type", "multipart/form-data");
                    //},
                    statusCode: {
                        401: function () {
                            location.reload();
                        }
                    },
                    success: function (res, textStatus, xhr) {
                        callback(res);
                    },
                    error: function (xhr, textStatus, err) {
                        console.log('error', err.responseText);
                        callback(null);
                    }
                });
            } catch (err) {
                //console.log(err);
                callback(null);
            }
        }
        else {
            console.log('error', 'undefined parameter (plugins.post)');
            callback(null);
        }
    }
    

    // clean partial list
    function cleanPartialList() {
        for (var i in self.partialXHRList) {
            self.partialXHRList[i].abort();
        }

        partialXHRList = [];
    }
}
/************************/


// creating class instance
var PostService = new PostServiceClass();

// creating object
var PostServiceObject = {
    // important
    Name: 'Post',

    Service: PostService
}

// registering controller object
_core.addService(PostServiceObject);

// Selectize service

/*****Main Function******/
var SelectizeServiceClass = function () {
    var _self = this;
    _self.addUrl = null;
    _self.removeUrl = null;
    _self.postService = _core.getService("Post").Service;

    function init() {

        bindMainEvents();
    }

    _self.Bind = function() {
        init();

    };

    async function saveContent(input) {
        let result = await _self.postService.postJsonAsync({value: input}, _self.addUrl);
        return result;
    }

    _self.onCreate = function(input) {
        console.log(_self.addUrl, _self.removeUrl);
        var res = saveContent(_self.postService);
        console.log(res);
        return {
            text: input,
            value: input
        };
    };

    _self.BindWithCreate = function (addUrl, removeUrl) {
        _self.addUrl = addUrl;
        _self.removeUrl = removeUrl;
        $('.selectize').each(function(e) {
            $(this).selectize( {
                delimiter: ',',
                persist: false,
                create: _self.onCreate,
            });
        });
    };


    _self.BindChangeEvent = function(elem, func) {

        var selectizeControl = $(elem)[0].selectize;
        selectizeControl.on('change', func);
    };

    function bindMainEvents() {

        $('.selectize').each(function(e) {
            $(this).selectize();
        });
    }
};
/************************/


// creating class instance
var SelectizeService = new SelectizeServiceClass();

// creating object
var SelectizeServiceObject = {
    // important
    Name: 'Selectize',

    Service: SelectizeService
};

// registering controller object
_core.addService(SelectizeServiceObject);

// loading service

/*****Main Function******/
var SelectMenuServiceClass = function () {
    var _self = this;

    _self.valueList = {};
    _self.nameList = {};
    _self.LoadContentURL = "/_Popup_/Get_SentencingDataArticles";

    _self.Init = function () {
        _self.valueList = {};
        _self.nameList = {};
    }

    function bindEvents(name) {

        // call init
        //_self.Init();

        // bind change
        $('.customSelectInput'+name).unbind().bind('change', function () {

            //_self.valueList = {};
            //_self.nameList = {};

            var curName = $(this).attr("name");
            _self.valueList[curName] = [];
            _self.nameList[curName] = [];

            $(".customSelectInput" + name).each(function () {
                if ($(this).is(":checked")) {
                    var name = $(this).attr("name");
                    var newName = $(this).data("newname");
                    var value = $(this).val();
                    var dataName = $(this).data('name');

                    if (name == "SentencingDataArticles") {
                        dataName = '';
                        var arr = newName.split(';');
                        arr.splice(0, 1);
                        for (var i in arr) {
                            var newarr = arr[i];
                            var newarr = newarr.split('.');
                            var curValue = newarr[0];
                            dataName = dataName + curValue + ' ';
                        }
                    }

                    if (name != '' && name != null) {
                        if (typeof _self.valueList[name] == 'undefined') {
                            _self.valueList[name] = [];
                        }
                        if (typeof _self.nameList[name] == 'undefined') {
                            _self.nameList[name] = [];
                        }
                        _self.valueList[name].push(value);
                        _self.nameList[name].push(dataName);
                    }
                    else {
                        alert('serious error on custom select');
                    }
                }
            });

            // after generate name to its span
            var free = true;
            var currentName = '';
            if (typeof _self.nameList[curName] != 'undefined') {
                if (_self.nameList[curName].length == 0) {
                    var free = true
                    currentName = '<a class="mainNode selectItems" href="javascript:;" >Ընտրել</a>';
                }
                else {
                    for (var i in _self.nameList[curName]) {
                        free = false;
                        //if (i > 0 && _self.nameList[curName].length != i) {
                        //    currentName += ', ';
                        //}
                        currentName += '<a data-name="' + curName + '" data-value="' + _self.valueList[curName][i] + '" class="item selectItem" href="javascript:;" >' + _self.nameList[curName][i] + '</a>';
                    }
                }
            }
            if (free) currentName = '<a class="mainNode selectItems" href="javascript:;" >Ընտրել</a>';
            $('.customSelectNames' + curName).html(currentName);

            // bind delete event()
            bindDeleteEvent()
        });

        var openClass = $(".open");

        var customSelect1 = $("#customSelect" + name);
        var customSelect1Drop = $("#customSelect" + name + "Drop");

        customSelect1Drop.unbind();

        customSelect1Drop.on('click', function (e) {
            if (!$(this).hasClass('item'))
                customSelect1.parents(".customSelectWrap").toggleClass("open");
        });

        $(".custom-select-dropdown-in-body").find("button[data-toggle='collapse']").click(function () {
            $(this).parents(".custom-select-dropdown-in-body").animate({
                scrollTop: $(this).offset().bottom
            }, 300);
        });

        $("body").on("click", function (e) {
            if (!customSelect1.parents(".customSelectWrap").is(e.target) && customSelect1.parents(".customSelectWrap").has(e.target).length === 0 && openClass.has(e.target).length === 0) {
                customSelect1.parents(".customSelectWrap").removeClass("open");
            }
        });
    }

    function bindDeleteEvent() {
        $('.selectItem').unbind().bind('keyup', function (e) {
            if (e.keyCode == 8 || e.keyCode == 46) {
                var id = $(this).data('value');
                var curName = $(this).data('name');
                console.log($('#customSelectInput' + curName + id).val());
                $('#customSelectInput' + curName + id).prop('checked', false);
                $('#customSelectInput' + curName + id).trigger('change');
            }
        })
    }

    // bind events and draws selected names at first run
    _self.bindSelectEvents = function (name) {
        bindEvents(name);
        $('.customSelectInput'+name).trigger('change');
    }

    // load content
    _self.loadContent = function (ParentID, type, curData, callback) {

        var data = {};
        if (type == "prevData") {
            data["prevData"] = curData;
        }
        else if (type == "sentenceData") {
            data["sentenceData"] = curData;
        }
        else if (type == "arrestData") {
            data["arrestData"] = curData;
        }
        data["ParentID"] = ParentID;

        _core.getService('Post').Service.postPartial(data, _self.LoadContentURL, function (res) {
            var response = {};
            if (res != null) {
                $('.articlesHolder').empty();
                //var change = $('.articlesHolder').append(res);


                var change = $(res).appendTo(".articlesHolder");
                response.status = true;
            }
            else {
                response.status = false;
            }

            callback(response);
        })
    }

    _self.collectData = function (name) {
        if (name != null)
            collectDataWorker(name);
        if (typeof _self.valueList[name] != 'undefined') {
            return filterValues(_self.valueList[name]);
        }
    }

    _self.clearData = function (name) {
        if (name != null) {
            if (typeof _self.valueList[name] != 'undefined' && typeof _self.nameList[name] != 'undefined') {
                _self.valueList = [];
                _self.nameList = [];
                $('.customSelectNames' + name).html('');
            }
        }
        return true;
    }

    _self.collectFullData = function (name) {
        var list = [];
        if (name != null)
            collectDataWorker(name);
        if (typeof _self.valueList[name] != 'undefined' && typeof _self.nameList[name] != 'undefined') {
            var valueList = filterValues(_self.valueList[name]);
            var nameList = filterValues(_self.nameList[name]);
            
            if (valueList.length == nameList.length) {
                for (var i in valueList) {
                    var data = {};
                    data['ID'] = valueList[i];
                    data['Name'] = nameList[i];
                    list.push(data);
                }
            }
        }
        return list;
    }

    function filterValues(list) {
        var newList = [];
        for (var i in list) {
            var found = false;
            var curValue = list[i];

            for (var j in newList) {
                if (newList[j] == curValue) found = true;
            }
            if (!found) newList.push(curValue);
        }

        return newList;
    }

    function collectDataWorker(name) {
        $(".customSelectInput" + name).each(function () {
            if ($(this).is(":checked")) {
                var name = $(this).attr("name");
                var value = $(this).val();
                var dataName = $(this).data('name');

                if (name != '' && name != null) {
                    if (typeof _self.valueList[name] == 'undefined') {
                        _self.valueList[name] = [];
                    }
                    if (typeof _self.nameList[name] == 'undefined') {
                        _self.nameList[name] = [];
                    }
                    _self.valueList[name].push(value);
                    _self.nameList[name].push(dataName);
                }
                else {
                    alert('serious error on custom select');
                }
            }
        });
    }
}
/************************/


// creating class instance
var SelectMenuService = new SelectMenuServiceClass();

// creating object
var SelectMenuServiceObject = {
    // important
    Name: 'SelectMenu',

    Service: SelectMenuService
}

// registering controller object
_core.addService(SelectMenuServiceObject);

// Table service

/*****Main Function******/
var TableServiceClass = function () {
    var _self = this;
    _self.delList = [];
    _self.delUrl = null;

    function init() {

        _self.postService = _core.getService("Post").Service;
        _self.loaderService = _core.getService("Loading").Service;

        bindMainEvents();
    }

    _self.Bind = function() {
        init();
    };

    function bindDeleteModalEvents() {

        $('#indexItemDeleteModalApprove').unbind().bind('click', function (e) {

            removeItems();
        })

    }

    function bindMainEvents() {
        $("#elemLangs").unbind().change(function(e){
            e.preventDefault();

            location.href = $(this).val();
        });

        $('.selectElems').unbind().change(function(e){
            e.preventDefault();
            var checked = false;
            if ($(this).is(":checked")) {
                checked = true;
            }

            $('.selItems').each(function(){
                $(this).prop("checked", checked);
            });
        });

        $('#deleteAllBtn').unbind().bind('click', function (e) {
            $('#indexItemDeleteModal').modal('show');

            _self.delList = [];

            $('.selItems').each(function(){
                if ($(this).is(":checked")) {
                    _self.delList.push($(this).data('id'));
                }
            });

            _self.delUrl = $(this).data('url');

            bindDeleteModalEvents();
        });

        $('.deleteElem').unbind().click(function(e){
            e.preventDefault();
            $('#indexItemDeleteModal').modal('show');

            var id = $(this).data('id');
            _self.delUrl = $(this).data('url');

            _self.delList.push(id);

            bindDeleteModalEvents();
        });

        $('.deleteAllBtn').unbind().click(function(e){
            e.preventDefault();
        });
    }

    function removeItems() {

        _self.loaderService.enableByClass('loaderHolder');

        _self.postService.postJson({'ids': _self.delList}, _self.delUrl, function(status,res) {

            _self.loaderService.disableByClass('loaderHolder');

            location.reload();
        })

    }

};
/************************/


// creating class instance
var TableService = new TableServiceClass();

// creating object
var TableServiceObject = {
    // important
    Name: 'Table',

    Service: TableService
}

// registering controller object
_core.addService(TableServiceObject);

// TinyMCE service

/*****Main Function******/
var TinyMCEServiceClass = function () {
    var _self = this;

    function init() {

        bindMainEvents();
    }

    _self.Bind = function() {
        init();

    };
    _self.BindSendMail = function() {
        initSendMail();

    };

    function initSendMail() {

        tinymce.init({
            orced_root_block : "",
            relative_urls : false,            remove_script_host : false,
            selector: 'textarea',
            toolbar: "...| removeformat | ...",
            plugins: [
                'link pagebreak'
            ],
            toolbar1: 'undo redo | link',
            toolbar2: 'print preview | codesample',
            image_advtab: true,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ],
            content_css: [
                '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
                '//www.tinymce.com/css/codepen.min.css'
            ]
        });
    };

    function bindMainEvents() {

        tinymce.init({            relative_urls : false,            remove_script_host : false,
            selector: 'textarea',
            plugins: [
                'advlist autolink lists link image charmap print preview hr anchor pagebreak',
                'searchreplace wordcount visualblocks visualchars code fullscreen',
                'insertdatetime media nonbreaking save table contextmenu directionality',
                'emoticons template paste textcolor colorpicker textpattern imagetools codesample toc'
            ],
            toolbar1: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            toolbar2: 'print preview media | forecolor backcolor emoticons | codesample',
            image_advtab: true,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ],
            content_css: [
                '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
                '//www.tinymce.com/css/codepen.min.css'
            ]
        });
    }
};
/************************/


// creating class instance
var TinyMCEService = new TinyMCEServiceClass();

// creating object
var TinyMCEServiceObject = {
    // important
    Name: 'TinyMCE',

    Service: TinyMCEService
};

// registering controller object
_core.addService(TinyMCEServiceObject);

// Validation service

/*****Main Function******/
var ValidationServiceClass = function () {
    var self = this;

    self.validate = function(action) {
        if (action == null || action) {
            alert('Խնդրում ենք լրացնել անհրաժաշտ բոլոր դաշտերը');
        }
    }
}
/************************/


// creating class instance
var ValidationService = new ValidationServiceClass();

// creating object
var ValidationServiceObject = {
    // important
    Name: 'Validation',

    Service: ValidationService
}

// registering controller object
_core.addService(ValidationServiceObject);

// Video service

/*****Main Function******/
var VideoServiceClass = function () {
    var _self = this;
    _self.selectedFileIds = [];
    _self.videoUrlIFrame = $('#videoUrlIFrame');
    _self.iFrameHolderBlock = $('.iFrameHolderBlock');

    function init() {

        _self.loaderService = _core.getService("Loading").Service;

        bindMainEvents();
    }

    _self.Bind = function() {
        init();

        checkSrcExists();
    };

    function bindMainEvents() {
        $('.videoUrlBlock').unbind().bind('change keyup', function (e) {
            var url = $(this).val();

            $('#videoUrlIFrame').attr('src',url);

            checkSrcExists();
        })
    }

    function checkSrcExists() {
        if (_self.videoUrlIFrame.attr('src') === '') {
            _self.iFrameHolderBlock.addClass('hidden');
        } else {
            _self.iFrameHolderBlock.removeClass('hidden');
        }
    }
};
/************************/


// creating class instance
var VideoService = new VideoServiceClass();

// creating object
var VideoServiceObject = {
    // important
    Name: 'Video',

    Service: VideoService
};

// registering controller object
_core.addService(VideoServiceObject);

// AddFile widget

/*****Main Function******/
var AddFileWidgetClass = function () {
    var _self = this;
    _self.callback = null;
    _self.content = null;
    _self.PrisonerID = null;
    _self.type = null;
    _self.id = null;
    _self.mode = null;
    _self.modalUrl = "/_Popup_/Get_AddFile";
    _self.viewUrl = "/_Popup_Views_/Get_AddFile";
    _self.finalFormData = new FormData();
    _self.removeFileURL = '/_Data_Main_/RemoveFile';
    _self.addFileURL = '/_Data_Main_/AddFile';
    _self.editFileURL = '/_Data_Main_/EditFile';
    
    // action list
    _self.actionList = {
    };

    _self.init = function () {
        console.log('File Widget Inited');

        _self.finalFormData = new FormData();
        _self.callback = null;
        _self.content = null;
        _self.mode = null;
        _self.id = null;
        _self.PrisonerID = null;
        _self.type = null;
    };

    // ask action
    _self.Add = function (data, callback) {

        // initilize
        _self.init();

        console.log('File Add called');

        // save callback
        _self.callback = callback;

        _self.mode = 'Add';

        _self.type = data.TypeID;

        _self.PrisonerID = data.PrisonerID;

        // loading view
        loadView(_self.modalUrl);
    };

    // ask action
    _self.Edit = function (data, callback) {

        // initilize
        _self.init();

        console.log('File Edit called');

        // save callback
        _self.callback = callback;

        _self.mode = 'Edit';

        _self.type = data.TypeID;

        _self.id = data.ID;

        // loading view
        loadView(_self.modalUrl);
    };

    // ask action
    _self.View = function (data, callback)
    {

        // initilize
        _self.init();

        console.log('File Edit called');

        // save callback
        _self.callback = callback;

        _self.mode = 'View';

        _self.type = data.TypeID;

        _self.id = data.ID;

        // loading view
        loadView(_self.viewUrl);
    };

    // remove file Action
    _self.Remove = function (data, callback) {

        // initilize
        _self.init();

        _self.type = data.TypeID;


        _core.getService('Loading').Service.enableBlur('loaderClass');

        // yes or no widget
        _core.getWidget('YesOrNo').Widget.Ask(1, function (res) {

            _core.getService('Loading').Service.disableBlur('loaderClass');

            if (res) {

                var url = _self.removeFileURL;
                
                _core.getService('Post').Service.postJson(data, url, function (res) {
                    if (res != null) {
                        callback(res);
                    }
                });
            }
        });
    }

    // loading modal from Views
    function loadView(url) {
            if (_self.id != null) url = url + '/' + _self.id;

            _core.getService('Loading').Service.enableBlur('loaderClass');

            $.get(url, function (res) {

                _core.getService('Loading').Service.disableBlur('loaderClass');

                // save content for later usage
                _self.content = res;

                // content
                $('#widgetHolder').html(_self.content);

                // bind events for yes and no
                bindEvents();

                // showing modal
                $('.add-file-modal').modal('show');
            }, 'html');
    }

    // binding buttons for Modal
    function bindEvents() {

        // buttons and fields
        var addFileToggle = $('#add-file-toggle');
        var acceptBtn = $('#acceptBtn');
        var checkItems = $('.checkValidateAddFile');
        var fileName = $('#filename');
        _self.getItems = $('.getItemsFile');

        // unbind
        addFileToggle.unbind();
        acceptBtn.unbind();

        // bind file change
        addFileToggle.bind('change', function () {
            _self.finalFormData.delete('fileImage');
            _self.finalFormData.append('fileImage', $(this)[0].files[0], $(this).val());

            // change filename fields and trigger change
            fileName.val($(this).val());
            fileName.html($(this).val());
        });

        // bind accept
        acceptBtn.bind('click', function () {

            var data = collectData();

            // adding file
            if (_self.id == null) {

                // default add file
                var url = _self.addFileURL;

                // append prisonerid and type
                data.append('PrisonerID', _self.PrisonerID);
                data.append('TypeID', _self.type);
            }
            else {

                // default edit file
                url = _self.editFileURL;

                // append id
                data.append('ID', _self.id);
            }

            //// log it
            //for (var pair of data.entries()) {
            //    console.log(pair[0] + ', ' + pair[1]);
            //}

            var curClass = '';
            if (typeof _self.type == 'undefined') curClass = 'fileLoader';
            else {
                if (parseInt(_self.type) == 1) curClass = 'imageLoader';
                else if (parseInt(_self.type) == 8) curClass = 'fileSentenceLoader';
                else curClass = 'fileLoader';
            }

            _core.getService('Loading').Service.enableBlur(curClass);

            // post service
            var postService = _core.getService('Post').Service;

            postService.postFormData(data, url, function (res) {

                _core.getService('Loading').Service.disableBlur(curClass);

                if (res != null) {
                    _self.callback(res);
                }
                else alert('serious eror on adding file');

            })

            //hiding modal
            $('.add-file-modal').modal('hide');
        });

        // check validate and toggle accept button
        checkItems.bind('change', function () {
            var action = false;

            checkItems.each(function () {
                if ($(this).val() == '' || $(this).val() == null) {
                    var curName = $(this).attr('name');
                    if (curName == 'Description' || curName == 'Note' || curName == 'Notes') $(this).val(' ');
                    else action = true;
                }
            });

            acceptBtn.attr("disabled", action);
        });

        // selectize
        $(".selectizeAddFile").each(function () {
            $(this).selectize({
                create: false
            });
        });
    }

    // collect data
    function collectData() {

        // collect data
        _self.getItems.each(function () {

            // name of Field (same as in Entity)
            var name = $(this).attr('name');

            _self.finalFormData.delete(name);
            _self.finalFormData.append(name, $(this).val());

        });

        return _self.finalFormData;
    }

}
/************************/


// creating class instance
var AddFileWidget = new AddFileWidgetClass();

// creating object
var AddFileWidgetObject = {
    // important
    Name: 'AddFile',

    Widget: AddFileWidget
}

// registering widget object
_core.addWidget(AddFileWidgetObject);

// find person widget

/*****Main Function******/
var FindPersonWidgetClass = function () {
    var _self = this;
    _self.callback = null;
    _self.content = null;
    _self.modal = $('.searchUserModal');
    _self.modalUrl = "/_Popup_/Get_FindPerson";
    _self.searchByDocUrl = "/Structure/CheckEmployeeByDocNumber";
    _self.searchByNameUrl = "/Structure/CheckEmployee";
    _self.idDocTableRowDrawURL = "/Structure/Draw_DocIdentity_Table_Row";
    _self.currentPerson = null;
    _self.searchCallback = null;
    _self.response = {};
    _self.response.status = false;
    _self.response.data = null;
    _self.DocIdentities = [];
    _self.menualSearch = false;
    _self.isBind = false;


    _self.init = function () {
        console.log('FindPerson Widget Inited');

        _self.menualSearch = $("#persondata").data("iscustom") == "True";;
        _self.DocIdentities = [];
        _self.response = {};
        _self.response.status = false;
        _self.response.data = null;
        _self.searchCallback = null;
        _self.isBind = false;

        bindIdDocTableRows();
    };

    // ask action
    _self.Show = function (callback) {

        // initilize
        _self.init();

        console.log('FindPerson Ask called');

        // save callback
        _self.callback = callback;

        // loading view
        loadView();
    };

    // bind person search
    _self.BindPersonSearch = function (callback) {

        // call init
        _self.init();

        _self.isBind = true;

        _self.searchCallback = callback;

        var findPersonBrn = $('#findperson');

        bindEvents();

        bindCollapseTable();
    }

    // loading modal from Views
    function loadView() {
        var data = {};

        if (_self.id != null && (_self.mode == "Edit" || _self.mode == "View")) {
            data.ID = _self.id;
        }

        var url = _self.modalUrl;

        _core.getService("Post").Service.postPartial(data, url, function (res) {

            _core.getService("Loading").Service.disableBlur("loaderDiv");
            _core.getService('Loading').Service.disableBlur('loaderClass');

            _self.content = res;

            // content
            $('#widgetHolder').html(_self.content);

            // bind events for yes and no
            bindEvents();

            // showing modal
            $('.searchUserModal').modal('show');
        });
    }

    // binding buttons for Modal
    function bindEvents() {

        // buttons
        _self.getItemsPerson = $('.getItemsPerson');
        _self.getItemsDocs = $('.getItemsDocuments');
        _self.checkItemsPerson = $('.checkValidatePerson');
        var checkItemsDocs = $('.checkValidateDocuments');
        var docDate = $('.Dates');

        var findPersonBrn = $('#findperson');
        var findPersonAgainBtn = $('#findpersonAgain');
        var addDocBtn = $('#addDocidentityButton');
        var nextStepBtn = $('#nextStepButton');
        var prevStepBtn = $('#prevStepButton');
        var saveBtn = $('#userformsave');
        var menualBtn = $('#menualAddBtn');
        
        // searching for person
        findPersonBrn.unbind();
        findPersonBrn.bind('click', function () {
            
            _self.menualSearch = false;
            $('.partdetails').removeClass('hidden');
            $('.partcustom').addClass('hidden');
            $('.partcustomdetails').addClass('hidden');
            $('.partcustom').addClass('hidden');
            $('.partnationality').addClass('hidden');
            $('.partsex').addClass('hidden');
            saveBtn.attr('disabled', true);

            // find person function
            getPerson();
        });

        // save button
        saveBtn.unbind();
        saveBtn.bind('click', function () {
            if (_self.callback != null) {

                // call callback
                //_self.response.data = 
                //_self.response.menual = _self.menualSearch;
                //_self.response.status = true;

                _self.callback(_self.getPersonData());

                // making null
                _self.callback = null;
                _self.currentPerson = null;

                // hide modal
                $('.searchUserModal').modal('hide');
            }
            else {
                console.warn('currentPerson: ', _self.currentPerson);
                console.warn('callback: ', _self.callback);
            }
        });

        // validation of form
        _self.checkItemsPerson.unbind().bind('change keyup', function () {

            var action = !_self.isValidData();

            if (!action && _self.isBind) {
                _self.searchCallback(true);
            }
            else {

                saveBtn.attr("disabled", action);
            }
        });

        // validation of doc identity add
        checkItemsDocs.unbind().bind('change keyup', function () {

            var action = false;

            checkItemsDocs.each(function () {
                if (($(this).val() == '' || $(this).val() == null) && _self.menualSearch) {
                    action = true;
                }
            });

            if (_self.menualSearch) {
                addDocBtn.attr('disabled', action);
            }
        });

        // add doc identity
        addDocBtn.unbind().click(function () {

            // add docidentity
            addDocIdentity();

            // trigger change
            _self.checkItemsPerson.trigger('change');
        });

        // menual add person
        menualBtn.unbind().click(function () {

            _self.menualSearch = true;

            $('.partnationality').removeClass('hidden');
            $('.partsex').removeClass('hidden');
            $('.partcustom').removeClass('hidden');
            $('.partdetails').addClass('hidden');
            $('.partcustomdetails').removeClass('hidden');

            var status = false;

            $('#modal-person-name-1').attr('disabled', status);
            $('#modal-person-name-2').attr('disabled', status);
            $('#modal-person-name-3').attr('disabled', status);
            $('#modal-person-bd').attr('disabled', status);
            $('#modal-person-social-id').attr('disabled', status);
            $('#modal-person-passport').attr('disabled', status);

            $('#modal-person-name-1').trigger('change');
        })

        // find again person
        findPersonAgainBtn.unbind().click(function () {

            // clean search
            cleanSearch();

            if (_self.isBind)
            _self.searchCallback(false);

            // disable button
            saveBtn.attr('disabled', true);
        });

        // daterangepicker
        docDate.daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            "locale": {
                "format": "DD/MM/YYYY",
                "separator": " - ",
                "applyLabel": "Հաստատել",
                "cancelLabel": "Մաքրել",
                "fromLabel": "From",
                "toLabel": "To",
                "customRangeLabel": "Custom",
                "daysOfWeek": [
                    "Երկ",
                    "Երք",
                    "Չրք",
                    "Հնգ",
                    "Ուր",
                    "Շբթ",
                    "Կրկ"
                ],
                "monthNames": [
                    "Հունվար",
                    "Փետրվար",
                    "Մարտ",
                    "Ապրիլ",
                    "Մայիս",
                    "Հունիս",
                    "Հուլիս",
                    "Օգոստոս",
                    "Սեպտեմբեր",
                    "Հոկտեմբեր",
                    "Նոյեմբեր",
                    "Դեկտեմբեր"
                ],
                "firstDay": 0
            },
            "buttonClasses": "btn btn-sm btn-flat",
            "applyClass": "color-success-bg color-success-hover-bg color-text-white",
            "cancelClass": "btn-default"
        });
        docDate.on("apply.daterangepicker", function (ev, picker) {
            $(this).val(picker.startDate.format("DD/MM/YYYY"));
        });
        docDate.on("cancel.daterangepicker", function (ev, picker) {
            $(this).val("");
        });

        // selectize
        $(".selectizePerson").each(function () {
            $(this).selectize({
                create: false
            });
        });
    }

    // clean search
    function cleanSearch() {

        // hide main row for loader show (it is new user search)
        $('#foundusertable').addClass('hidden');
        $('#foundusererrorp').addClass('hidden');

        // disableing or enabling fields
        $('#modal-person-name-1').val('');
        $('#modal-person-name-2').val('');
        $('#modal-person-name-3').val('');
        $('#modal-person-bd').val('');
        $('#modal-person-social-id').val('');
        $('#modal-person-passport').val('');

        $('#modal-person-name-1').attr('disabled', false);
        $('#modal-person-name-2').attr('disabled', false);
        $('#modal-person-name-3').attr('disabled', false);
        $('#modal-person-bd').attr('disabled', false);
        $('#modal-person-social-id').attr('disabled', false);
        $('#modal-person-passport').attr('disabled', false);

        _self.currentPerson = null;

        _self.menualSearch = false;

        $('.partcustom').addClass('hidden');
        $('.partdetails').addClass('hidden');
        $('.partcustomdetails').addClass('hidden');
        $('.partnationality').addClass('hidden');
        $('.partsex').addClass('hidden');

        //_self.currentPersonalID = null;
    }

    // get person from police database
    function getPerson() {

        // data to send to server
        var data = {};

        // in search progress part
        searchProcessEvents();

        // loader
        var loadingService = _core.getService('Loading').Service;

        loadingService.enable('searchuserloader', 'foundusertable');

        // getting fields
        var firstname = $('#modal-person-name-1').val();
        var lastname = $('#modal-person-name-2').val();
        var bday = $('#modal-person-bd').val();
        var passport = $('#modal-person-passport').val();
        var socId = $('#modal-person-social-id').val();

        // first search by socid
        if (socId != '' && socId != null) {
            data['DocNumber'] = socId;
            data['Type'] = false;

            // search by doc
            searchPerson('doc', data);
        }
            // else search by pasport
        else if (passport != '' && passport != null) {
            data['DocNumber'] = passport;
            data['Type'] = true;

            // search by doc
            searchPerson('doc', data);
        }
            // then search by firstname lastname bday
        else if (firstname != '' && lastname != '' && bday != '') {
            data['FirstName'] = firstname;
            data['LastName'] = lastname;
            data['BirthDate'] = bday;

            // search by doc
            searchPerson('name', data);
        }
        else {
            // disable loader
            var loadingService = _core.getService('Loading').Service;
            loadingService.disable('searchuserloader', 'foundusertable', false);
        }
    }

    // check validate for binds from other modal
    _self.isValidData = function () {
        var action = true;

        _self.checkItemsPerson.each(function () {
            if (_self.menualSearch) {
                if (($(this).val() == '' || $(this).val() == null)) {
                    action = false;
                }
            }
            else {
                if (_self.currentPerson == null) action = false;
            }
        });

        return action;

    }

    // get person data for bindings from other modal
    _self.getPersonData = function () {
        var data = {};
        data['Person'] = collectData();
        data['menualSearch'] = data['Person']['isCustom'];
        //data['PhotoLink'] = data['Person']['PhotoLink'];
        return data;
    }

    // collect data
    function collectData() {

        var data = {};
        var hasID = $("#persondata").data("id") != "";

        // if data is from police
        if (!_self.menualSearch && !hasID) {
            data = _self.currentPerson;
        }
        // menul data
        else {
            _self.getItemsPerson.each(function () {

                // name of Field (same as in Entity)
                var name = $(this).attr('name');

                // appending to Data
                //if (name == "SexLibItem_ID") {
                //    debugger
                //    data["SexLibItem_ID"] = $(this).data('id');
                //    data["SexLibItem_ID"] = $(this).val();
                //}
                //else

                data[name] = $(this).val();
            });

            data.IdentificationDocuments = CollectDocIdentityData();
        }

        data['isCustom'] = _self.menualSearch;
        
        data.ID = $("#persondata").data("id");

        //if (_self.menualSearch) {
            var livingData = {};
            var livingStr = '';
            $('.getItemsLivAddress').each(function () {
                livingData[$(this).attr('name')] = $(this).val();
                livingStr = livingStr + $(this).val() + ' ';
            });

            var regData = {};
            var regStr = '';
            $('.getItemsRegAddress').each(function () {
                regData[$(this).attr('name')] = $(this).val();
                regStr = regStr + $(this).val() + ' ';
            });

            data['Registration_address'] = regStr;
            data['Living_place'] = livingStr;
            data['Registration_Entity'] = regData;
            data['Living_Entity'] = livingData;
        //}
        delete data['status'];
        data['Status'] = null;

        console.log(data);

        return data;
    }

    // add menual doc identity
    function addDocIdentity() {

        var citizenship = $('#family-partner-doc-citizenship')[0].selectize;
        var docidentityselect = $('#family-partner-doc-type')[0].selectize;

        var data = {};
        var url = _self.idDocTableRowDrawURL;

        _self.getItemsDocs.each(function () {

            // name of Field (same as in Entity)
            var name = $(this).attr('name');

            // appending to Data
            data[name] = $(this).val();
        });

        data.CitizenshipLibItem_Name = citizenship.getItem(citizenship.getValue())[0].innerHTML;
        data.TypeLibItem_Name = docidentityselect.getItem(docidentityselect.getValue())[0].innerHTML;

        data.ID = Math.floor((Math.random() * 100) + 1);
        data.Type = false;

        drawDocIdntityTableRow(data);
    }

    // draw docidentity table row
    function drawDocIdntityTableRow(data) {
        var curHtml = '<tr class="getManualDocIdentity" data-TypeLibItem_Name="' + data.TypeLibItem_Name + '" data-CitizenshipLibItem_Name="' + data.CitizenshipLibItem_Name + '" data-ID="" data-FromWhom="' + data.FromWhom + '" data-Date="' + data.Date + '" data-Number="' + data.Number + '" data-CitizenshipLibItem_ID="' + data.CitizenshipLibItem_ID + '" data-TypeLibItem_ID="' + data.TypeLibItem_ID + '">';
        curHtml += '<td>'+data.CitizenshipLibItem_Name+'</td>';
        curHtml += '<td>'+data.TypeLibItem_Name+'</td>';
            curHtml += '<td>'+data.Number+'</td>';
            curHtml += '<td>'+data.Date+'</td>';
            curHtml += '<td>'+data.FromWhom+'</td>';
            curHtml += '<td>';
            curHtml += '<button class="btn btn-sm btn-default btn-flat removeManualDocIdentity"><i class="fa fa-trash-o" title="Հեռացնել"></i></button>';
            curHtml += '</td>';
            curHtml += '</tr>';

            $('#menualDocIdentityTableBody').append(curHtml);

        bindIdDocTableRows();
    }

    // collect doc identity data
    function CollectDocIdentityData() {
        var list = [];

        $('.getManualDocIdentity').each(function () {
            var data = {};
            
            data.ID = $(this).data('id');
            data.FromWhom = $(this).data('fromwhom');
            data.Date = $(this).data('date');
            data.Number = $(this).data('number');
            data.CitizenshipLibItem_ID = $(this).data('citizenshiplibitem_id');
            data.CitizenshipLibItem_Name = $(this).data('citizenshiplibitem_name');
            data.TypeLibItem_ID = $(this).data('typelibitem_id');
            data.TypeLibItem_Name = $(this).data('typelibitem_name');

            list.push(data);
        });

        return list;
    }

    // events while searching for person
    function searchProcessEvents() {

        // hide main row for loader show (it is new user search)
        $('#foundusertable').addClass('hidden');
        $('#foundusererrorp').addClass('hidden');
    }

    // event after search
    function searchProcessAfterEvents(status) {
        if (status) {

            // toggle message table and <p>
            $('#foundusertable').removeClass('hidden');
            $('#foundusererrorp').addClass('hidden');

            //$('#menualAddBtn').attr('disabled', true);

        } 
        else {

            // current person is null
            _self.currentPerson = null;

            // hide table and show error <p>
            $('#foundusertable').addClass('hidden');
            $('#foundusererrorp').removeClass('hidden');
            $('#foundusererrorp').html('Որոնման արդյունքում ոչ մի անձ չի գտնվել');

            //$('#menualAddBtn').attr('disabled', false);

        }

        // disableing or enabling fields
        $('#modal-person-name-1').attr('disabled', status);
        $('#modal-person-name-2').attr('disabled', status);
        $('#modal-person-name-3').attr('disabled', status);
        $('#modal-person-bd').attr('disabled', status);
        $('#modal-person-social-id').attr('disabled', status);
        $('#modal-person-passport').attr('disabled', status);
    }

    // match data to table
    function matchPersonDataToTable(data) {

        // if user found
        if (typeof data != 'undefined' && data != null) {

            if (!data.Status) {

                // find again button toggle
                //$('#findpersonAgain').attr('disabled', false);

                // aftet search events
                searchProcessAfterEvents(false);
                if (_self.isBind) _self.searchCallback(false);
            }
            else if (data.Status) {

                // find again button toggle
                //$('#findpersonAgain').attr('disabled', true);

                // after search events
                searchProcessAfterEvents(true);

                // current person
                _self.currentPerson = data;

                // bind values to table
                drawUserList(data);

                $('.partcustom').removeClass("hidden");

                // draw fields
                drawUserFields(data, false);

                // bind collapse table info
                bindCollapseTable();
                if (_self.isBind) _self.searchCallback(true);
            }

            // disable loader
            var loadingService = _core.getService('Loading').Service;
            loadingService.disable('searchuserloader', 'foundusertable', false);
        }
        else {
            console.error('server returned null from server while searching for user')
        }
    }

    // draw user table row
    function drawUserList(user) {
        //console.log(user);
        var tbody = $('#searchuserinfoshort');

        // empty
        tbody.empty();

        curHtml = '';
        curHtml += '<tr><td data-title="#">';
        curHtml += '0</td>';
        curHtml += '<td data-title="Անուն Ազգանուն Հայրանուն">' + user.FirstName + ' ' + user.MiddleName + ' ' + user.LastName + '</td>';
        curHtml += '<td data-title="Ծննդ. ամսաթիվ">' + user.Birthday + '</td>';
        curHtml += '<td data-title="Սոց. քարտ">' + user.Personal_ID + '</td>';
        curHtml += '<td data-title="Անձնագիր">' + ((user.IdentificationDocuments.length != 0 ) ? user.IdentificationDocuments[0].Number : "") + '</td></tr>';

        var reg = user.Registration_Entity;
        var registerationData = reg.Registration_Region + ', ' + reg.Registration_Community + ' ' + (reg.Registration_Apartment != null ? reg.Registration_Apartment : "") + ', ' + reg.Registration_Street + ' ' + reg.Registration_Building_Type + ' ' + reg.Registration_Building;
        reg = user.Living_Entity;
        var livingData = reg.Registration_Region + ', ' + reg.Registration_Community + ' ' + (reg.Registration_Apartment != null ? reg.Registration_Apartment : "") + ', ' + reg.Registration_Street + ' ' + reg.Registration_Building_Type + ' ' + reg.Registration_Building;
        
        if (user.SexLibItem_ID == 153) user.GenderName = 'Իգական';
        else if (user.SexLibItem_ID == 154) user.GenderName = 'Արական';
        else user.GenderName = '';

        curHtml += ' <tr class="collapse"><td colspan="5"> <div class="row"> <div class="col-xs-12 col-md-4"><img class="img-responsive" src="' + user.PhotoLink + '" alt="" /></div> <div class="col-xs-12 col-md-8">';
        curHtml += ' <div class="form-group"> <label>Անձնագիր՝</label> <p>' + ((user.IdentificationDocuments.length != 0) ? user.IdentificationDocuments[0].Number : "") + ', ' + ((user.IdentificationDocuments.length != 0) ? user.IdentificationDocuments[0].Date : "") + ', ' + ((user.IdentificationDocuments.length != 0) ? user.IdentificationDocuments[0].FromWhom : "") + '</p> </div> <div class="form-group"> <label>Ազգություն՝</label> <p>' + user.NationalityLabel + '</p> </div> <div class="form-group"> <label>Քաղաքացիությւն՝</label> <p>' + user.Citizenship + '</p> </div> <div class="form-group"> <label>Սեռ՝</label> <p>' + user.GenderName + '</p> ';
        curHtml += '</div> <div class="form-group"> <label>Գրանցման հասցե՝</label> <p>' + registerationData + '</p> </div> <div class="form-group"> <label>Բնակության հասցե՝</label> <p>' + livingData + '</p> </div> </div> </div> </td> </tr>';

        tbody.append(curHtml);
    }


    // search person
    function searchPerson(type, data) {

        var url = null;
        if (type == 'doc') url = _self.searchByDocUrl;
        else if (type == 'name') url = _self.searchByNameUrl;

        // post service
        var postService = _core.getService('Post').Service;

        // sending to server and reciving answer
        postService.postJson(data, url, function (res) {
            matchPersonDataToTable(res);
        })
    }
    // draw user fields
    function drawUserFields(user, disabled) {

        // fiving values
        $('#modal-person-name-1').val(user.FirstName);
        $('#modal-person-name-2').val(user.LastName);
        $('#modal-person-name-3').val(user.MiddleName);
        $('#modal-person-bd').val(typeof user.Birthday != 'undefined' ? user.Birthday : user.BirthDay);
        $('#modal-person-social-id').val(typeof user.Personal_ID != 'undefined' ? user.Personal_ID : user.PSN);
        $('#modal-person-passport').val((user.IdentificationDocuments.length != 0) ? user.IdentificationDocuments[0].Number : "");

        $('#family-partner-sex')[0].selectize.setValue(user.SexLibItem_ID);
        $('#family-partner-nationality')[0].selectize.setValue(user.NationalityID);
        if (user.IdentificationDocuments.length != 0) {
            drawDocIdntityTableRow(user.IdentificationDocuments[0]);
        }

        $('#registrationRegionLiv').val((typeof user.Living_Entity != "undefined" && typeof user.Living_Entity.Registration_Region != " undefined") ? user.Living_Entity.Registration_Region : "");
        $('#registrationComLiv').val((typeof user.Living_Entity != "undefined" && typeof user.Living_Entity.Registration_Community != " undefined") ? user.Living_Entity.Registration_Community : "");
        $('#registrationStreetLiv').val((typeof user.Living_Entity != "undefined" && typeof user.Living_Entity.Registration_Street != " undefined") ? user.Living_Entity.Registration_Street : "");
        $('#registrationBuildingTypeLiv').val((typeof user.Living_Entity != "undefined" && typeof user.Living_Entity.Registration_Building != " undefined") ? user.Living_Entity.Registration_Building : "");
        $('#registrationBuildingLiv').val((typeof user.Living_Entity != "undefined" && typeof user.Living_Entity.Registration_Building_Type != " undefined") ? user.Living_Entity.Registration_Building_Type : "");
        $('#registrationAptLiv').val((typeof user.Living_Entity != "undefined" && typeof user.Living_Entity.Registration_Apartment != " undefined") ? user.Living_Entity.Registration_Apartment : "");

        $('#registrationRegionReg').val((typeof user.Registration_Entity != "undefined" && typeof user.Registration_Entity.Registration_Region != " undefined") ? user.Registration_Entity.Registration_Region : "");
        $('#registrationComReg').val((typeof user.Registration_Entity != "undefined" && typeof user.Registration_Entity.Registration_Community != " undefined") ? user.Registration_Entity.Registration_Community : "");
        $('#registrationStreetReg').val((typeof user.Registration_Entity != "undefined" && typeof user.Registration_Entity.Registration_Street != " undefined") ? user.Registration_Entity.Registration_Street : "");
        $('#registrationBuildingTypeReg').val((typeof user.Registration_Entity != "undefined" && typeof user.Registration_Entity.Registration_Building != " undefined") ? user.Registration_Entity.Registration_Building : "");
        $('#registrationBuildingReg').val((typeof user.Registration_Entity != "undefined" && typeof user.Registration_Entity.Registration_Building_Type != " undefined") ? user.Registration_Entity.Registration_Building_Type : "");
        $('#registrationAptReg').val((typeof user.Registration_Entity != "undefined" && typeof user.Registration_Entity.Registration_Apartment != " undefined") ? user.Registration_Entity.Registration_Apartment : "");

        // trigger change
        $('.checkValidatePerson').trigger('change');
    }

    // bind table collapse
    function bindCollapseTable() {

        // collapse table
        var collapseTable = $(".collapseTable");
        var collapseTableTr = collapseTable.find("tbody tr:not(.collapse)");

        collapseTableTr.click(function () {
            if ($(this).next("tr.collapse").hasClass("in")) {
                $(this).parents(".collapseTable").find("tr.collapse.in").removeClass("in");
            }
            else {
                $(this).parents(".collapseTable").find("tr.collapse.in").removeClass("in");
                $(this).parents(".collapseTable").find("input[name='modal-person-radio']").prop("checked", false);
                $(this).next("tr.collapse").addClass("in");
                $(this).find("input[name='modal-person-radio']").prop("checked", true);
            }
        });

        if (_self.searchCallback == null) {
            collapseTableTr.trigger('click');
        }
    }

    function bindIdDocTableRows() {

        var tableRow = $('.removeManualDocIdentity');

        tableRow.unbind().click(function () {

            $(this).parent().parent().remove();
        })
    }
}
/************************/


// creating class instance
var FindPersonWidget = new FindPersonWidgetClass();

// creating object
var FindPersonWidgetObject = {
    // important
    Name: 'FindPerson',

    Widget: FindPersonWidget
}

// registering widget object
_core.addWidget(FindPersonWidgetObject);

// Add LibItem widget

/*****Main Function******/
var LibItemWidgetClass = function () {
    var _self = this;
    _self.callback = null;
    _self.content = null;
    _self.getItems = null
    _self.id = null;
    _self.mode = null;

    _self.modalUrl = "/_Popup_/Get_LibItem";
    _self.addUrl = '/Lists/AddLib';
    _self.editUrl = '/Lists/EditLib';
    _self.removeUrl = '/Lists/DelLib';
    _self.ModalName = 'libitem-modal';
    _self.finalData = {};
    
    _self.init = function () {
        console.log('Widget Inited');

        _self.finalData = {};
        _self.callback = null;
        _self.content = null;
        _self.getItems = null;
        _self.id = null;
        _self.mode = null;
    };

    // add action
    _self.Add = function (data, callback) {

        // initilize
        _self.init();

        console.log('Add called');

        _self.pathid = data.PathID;
        _self.libid = data.LibID;

        _self.mode = 'Add';

        // save callback
        _self.callback = callback;

        // loading view
        loadView(_self.modalUrl);
    }

    // edit action
    _self.Edit = function (data, callback) {
        // initilize
        _self.init();

        console.log('Edit called');

        _self.id = data.ID;
        _self.pathid = data.PathID;
        _self.libid = data.LibID;

        _self.mode = 'Edit';

        // save callback
        _self.callback = callback;

        // loading view
        loadView(_self.modalUrl);
    };

    // edit action
    _self.Remove = function (data, callback) {
        // initilize
        _self.init();

        console.log('Remove called');

        _self.id = data.ID;

        // save callback
        _self.callback = callback;

        // remove
        removeData();
    };

    // loading modal from Views
    function loadView(url) {

        var data = {};

        if (_self.id != null && _self.mode == "Edit") {
            data.ID = _self.id;
        }

        _core.getService("Post").Service.postPartial(data, url, function (res) {

            _self.content = res;

            _core.getService('Loading').Service.disableBlur('loaderDiv');

            // content
            $('#widgetHolder').html(_self.content);

            // bind events for yes and no
            bindEvents();

            // showing modal
            $('.'+_self.ModalName).modal('show');
        });
    }

    // binding buttons for Modal
    function bindEvents() {

        // buttons and fields
        var acceptBtn = $('#acceptBtnModal');
        var checkItems = $('.checkValidateModal');
        var dates = $('.DatesModal');
        _self.getItems = $('.getItemsModal');
        var propertyselect = $('#propertyselect');
        var addPropertyButton = $("#addPropertyButton");

        // unbind
        acceptBtn.unbind();
        checkItems.unbind();

        // date range picker
        dates.daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            "locale": {
                "format": "DD/MM/YYYY",
                "separator": " - ",
                "applyLabel": "Հաստատել",
                "cancelLabel": "Մաքրել",
                "fromLabel": "From",
                "toLabel": "To",
                "customRangeLabel": "Custom",
                "daysOfWeek": [
                    "Երկ",
                    "Երք",
                    "Չրք",
                    "Հնգ",
                    "Ուր",
                    "Շբթ",
                    "Կրկ"
                ],
                "monthNames": [
                    "Հունվար",
                    "Փետրվար",
                    "Մարտ",
                    "Ապրիլ",
                    "Մայիս",
                    "Հունիս",
                    "Հուլիս",
                    "Օգոստոս",
                    "Սեպտեմբեր",
                    "Հոկտեմբեր",
                    "Նոյեմբեր",
                    "Դեկտեմբեր"
                ],
                "firstDay": 0
            },
            "buttonClasses": "btn btn-sm btn-flat",
            "applyClass": "color-success-bg color-success-hover-bg color-text-white",
            "cancelClass": "btn-default"
        });
        dates.on("apply.daterangepicker", function (ev, picker) {
            $(this).val(picker.startDate.format("DD/MM/YYYY"));
            dates.trigger('change');
        });
        dates.on("cancel.daterangepicker", function (ev, picker) {
            $(this).val("");
            dates.trigger('change');
        });

        // bind hide show release data
        propertyselect.unbind().bind('change', function () {

            var value = propertyselect[0].selectize.getValue();
            
            if (value != "" && typeof value != "undefined" && value != null) {
                $(".propertyselectvalue").addClass("hidden");
                $("#propertyselectvaluediv" + value).removeClass("hidden");
            }
        });

        // bind accept
        acceptBtn.bind('click', function () {

            var data = collectData();

            if (_self.mode == 'Add') {

                data.LibPathID = _self.pathid;
                data.LibToLibID = _self.libid;

                // add
                addData(data);
            }
            else if (_self.mode == 'Edit') {

                data.LibID = _self.id;

                // edit
                editData(data);
            };

            //hiding modal
            $('.'+_self.ModalName).modal('hide');
        });

        // bind add property
        addPropertyButton.unbind().bind("click", function (e) {
            e.preventDefault();
            var data = {};
            var propertyselectSelectize = $("#propertyselect")[0].selectize;
            data.PropertyID = propertyselectSelectize.getValue();
            data.PropertyName = propertyselectSelectize.getItem(propertyselectSelectize.getValue())[0].innerHTML;

            var dataCustom = $('#propertyselectvalue' + data.PropertyID).data("custom");

            if (dataCustom != null && typeof dataCustom != "undefined" && dataCustom != "") {
                data.ValueName = $('#propertyselectvalue' + data.PropertyID).val();
                data.ValueID = $('#propertyselectvalue' + data.PropertyID).data("id");
                data.Custom = true;
            }
            else {
                var tempselect = $('#propertyselectvalue' + data.PropertyID)[0].selectize;
                data.ValueID = tempselect.getValue();
                data.ValueName = tempselect.getItem(tempselect.getValue())[0].innerHTML;
                data.Custom = false;
            }

            data.ValueID = (typeof data.ValueID == 'undefined' ? '' : data.ValueID);

            drawPropertyList(data);
        });

        // check validate and toggle accept button
        checkItems.bind('change keyup', function () {
            var action = false;

            checkItems.each(function () {
                if ($(this).val() == '' || $(this).val() == null) {
                    var curName = $(this).attr('name');
                    if (curName == 'Description' || curName == 'ReleaseBasisLibItemID' || curName == 'ReleaseDate' || curName == 'ReleaseNote' || curName == 'Note' || curName == 'Notes') $(this).val(' ');
                    else action = true;
                }
            });

            acceptBtn.attr("disabled", action);
        });

        // selectize
        $(".selectizeModal").each(function () {
            $(this).selectize({
                create: false
            });
        });

        // bind property list events
        bindPropertyListEvents();
    }

    // draw property list
    function drawPropertyList(data) {

        var curHtml = "<tr data-newitem='true' class='getPropertyListRow' data-custom='" + data.Custom + "' data-propertyid='" + data.PropertyID + "' data-propertyname='" + data.PropertyName + "' data-valueid='" + data.ValueID + "' data-valuename='" + data.ValueName + "' data-id=''>";
        curHtml += "<td>" + data.PropertyName + "</td>";
        curHtml += "<td>" + data.ValueName + "</td>";
        curHtml += '<td><button data-id="" class="btn btn-sm btn-default btn-flat removePropertyListRow" title="Հեռացնել"><i class="fa fa-trash-o" ></i></button></td>';
        curHtml += "</tr>";

        $('#propertyselectdraw').append(curHtml);

        // bind draw ContentList events
        bindPropertyListEvents();
    }
    
    // remove property from selected list
    function bindPropertyListEvents() {
        $('.removePropertyListRow').unbind().click(function (e) {
            e.preventDefault();

            $(this).parent().parent().remove();
        })
    }

    // add
    function addData(data) {

        // post service
        var postService = _core.getService('Post').Service;

        _core.getService("Loading").Service.enableBlur("loaderDiv");

        postService.postJson(data, _self.addUrl, function (res) {

            _core.getService("Loading").Service.disableBlur("loaderDiv");

            if (res != null) {
                _self.callback(res);
            }
            else alert('serious eror on adding');
        })
    }

    // edit
    function editData(data) {
        // post service
        var postService = _core.getService('Post').Service;

        _core.getService("Loading").Service.enableBlur("loaderDiv");

        postService.postJson(data, _self.editUrl, function (res) {

            _core.getService("Loading").Service.disableBlur("loaderDiv");

            if (res != null) {
                _self.callback(res);
            }
            else alert('serious eror on editing');
        })
    }

    // remove
    function removeData() {

        var data = {};
        data.LibID = _self.id;

        // post service
        var postService = _core.getService('Post').Service;

        _core.getService("Loading").Service.enableBlur("loaderDiv");

        postService.postJson(data, _self.removeUrl, function (res) {

            _core.getService("Loading").Service.enableBlur("loaderDiv");

            if (res != null) {
                _self.callback(res);
            }
            else alert('serious eror on removing');
        })
    }

    // collect data
    function collectData() {

        // array of violation list
        //_self.finalData["ViolationList"] = [];

        _self.getItems.each(function () {

            // name of Field (same as in Entity)
            var name = $(this).attr('name');

            if (name == "ViolationList") {
                //var tempValue = $(this).val();
                //for (var i in tempValue) {
                //    var temp = {};
                //    temp["LibItemID"] = parseInt(tempValue[i]);
                //    _self.finalData[name].push(temp);
                //}
            }
            else {
                // appending to Data
                _self.finalData[name] = $(this).val();
            }

            _self.finalData["propList"] = collectPropertyList();

        });

        return _self.finalData;
    }

    // collect property list
    function collectPropertyList() {
        var list = [];
        $(".getPropertyListRow").each(function () {

            var data = {};
            var PropsEntity = {};
            var PropValuesEntity = {};

            PropsEntity.ID = $(this).data("propertyid");
            PropsEntity.Fixed = !$(this).data("custom");
            PropsEntity.NewItem = $(this).data("newitem");
            PropValuesEntity.ID = $(this).data("valueid");
            PropValuesEntity.Value = $(this).data("valuename");

            data.property = PropsEntity;
            data.value = PropValuesEntity;

            list.push(data);
        });
        return list;
    }
}
/************************/


// creating class instance
var LibItemWidget = new LibItemWidgetClass();

// creating object
var LibItemWidgetObject = {
    // important
    Name: 'LibItem',

    Widget: LibItemWidget
}

// registering widget object
_core.addWidget(LibItemWidgetObject);

// Order widget

/*****Main Function******/
var OrderWidgetClass = function () {
    var _self = this;
    _self.callback = null;
    _self.content = null;
    _self.PrisonerID = null;
    _self.type = null;
    _self.id = null;
    _self.URL = null;
    _self.EmployeeID = null;
    _self.mode = null;
    _self.modalUrl = "/_Popup_/Get_Order";
    _self.ModalName = "add-order-modal";
    _self.viewUrl = "/_Popup_Views_/Get_Order";
    _self.finalFormData = new FormData();
    _self.removeFileURL = '/Structure/RemoveFile';
    _self.OrderURL = '/_Data_Main_/Order';
    _self.editFileURL = '/_Data_Main_/EditFile';
    
    // action list
    _self.actionList = {
    };

    _self.init = function () {
        console.log('File Widget Inited');

        _self.finalFormData = new FormData();
        _self.callback = null;
        _self.URL = null;
        _self.EmployeeID = null;
        _self.content = null;
        _self.mode = null;
        _self.id = null;
        _self.PrisonerID = null;
        _self.type = null;
    };

    // ask action
    _self.Add = function (data, callback) {

        // initilize
        _self.init();

        console.log('File Add called');

        // save callback
        _self.callback = callback;

        _self.mode = 'Add';

        _self.URL = data.URL;

        _self.EmployeeID = data.EmployeeID;

        // loading view
        loadView(_self.modalUrl);
    };

    // ask action
    _self.Edit = function (data, callback) {

        // initilize
        _self.init();

        console.log('File Edit called');

        // save callback
        _self.callback = callback;

        _self.mode = 'Edit';

        _self.type = data.TypeID;

        _self.id = data.ID;

        // loading view
        loadView(_self.modalUrl);
    };

    // ask action
    _self.View = function (data, callback)
    {

        // initilize
        _self.init();

        console.log('File Edit called');

        // save callback
        _self.callback = callback;

        _self.mode = 'View';

        _self.type = data.TypeID;

        _self.id = data.ID;

        // loading view
        loadView(_self.viewUrl);
    };

    // remove file Action
    _self.Remove = function (data, callback) {

        // initilize
        _self.init();

        _self.type = data.TypeID;


        _core.getService('Loading').Service.enableBlur('loaderClass');

        // yes or no widget
        _core.getWidget('YesOrNo').Widget.Ask(1, function (res) {

            _core.getService('Loading').Service.disableBlur('loaderClass');

            if (res) {

                var url = _self.removeFileURL;
                
                _core.getService('Post').Service.postJson(data, url, function (res) {
                    if (res != null) {
                        callback(res);
                    }
                });
            }
        });
    }

    // loading modal from Views
    function loadView(url) {
            var data = {};

            if (_self.id != null && _self.mode == "Edit") {
                data.ID = _self.id;
            }

            _core.getService("Post").Service.postPartial(data, url, function (res) {

                _self.content = res;

                _core.getService('Loading').Service.disableBlur('loaderDiv');

                // content
                $('#widgetHolder').html(_self.content);

                // bind events for yes and no
                bindEvents();

                // showing modal
                $('.' + _self.ModalName).modal('show');
            });
    }

    // binding buttons for Modal
    function bindEvents() {

        // buttons and fields
        var acceptBtn = $('#acceptBtnModal');
        var checkItems = $('.checkValidateModal');
        var dates = $('.DatesModal');
        _self.getItems = $('.getItemsModal');
        var propertyselect = $('#propertyselect');
        var addPropertyButton = $("#addPropertyButton");

        // unbind
        acceptBtn.unbind();
        checkItems.unbind();

        // date range picker
        dates.daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            "locale": {
                "format": "DD/MM/YYYY",
                "separator": " - ",
                "applyLabel": "Հաստատել",
                "cancelLabel": "Մաքրել",
                "fromLabel": "From",
                "toLabel": "To",
                "customRangeLabel": "Custom",
                "daysOfWeek": [
                    "Երկ",
                    "Երք",
                    "Չրք",
                    "Հնգ",
                    "Ուր",
                    "Շբթ",
                    "Կրկ"
                ],
                "monthNames": [
                    "Հունվար",
                    "Փետրվար",
                    "Մարտ",
                    "Ապրիլ",
                    "Մայիս",
                    "Հունիս",
                    "Հուլիս",
                    "Օգոստոս",
                    "Սեպտեմբեր",
                    "Հոկտեմբեր",
                    "Նոյեմբեր",
                    "Դեկտեմբեր"
                ],
                "firstDay": 0
            },
            "buttonClasses": "btn btn-sm btn-flat",
            "applyClass": "color-success-bg color-success-hover-bg color-text-white",
            "cancelClass": "btn-default"
        });
        dates.on("apply.daterangepicker", function (ev, picker) {
            $(this).val(picker.startDate.format("DD/MM/YYYY"));
            dates.trigger('change');
        });
        dates.on("cancel.daterangepicker", function (ev, picker) {
            $(this).val("");
            dates.trigger('change');
        });

        // bind hide show release data
        propertyselect.unbind().bind('change', function () {

            var value = propertyselect[0].selectize.getValue();

            if (value != "" && typeof value != "undefined" && value != null) {
                $(".propertyselectvalue").addClass("hidden");
                $("#propertyselectvaluediv" + value).removeClass("hidden");
            }
        });

        // bind accept
        acceptBtn.bind('click', function () {

            var data = collectData();

            if (_self.mode == 'Add') {

                data.append("EmployeeID", _self.EmployeeID);

                // add
                addData(data);
            }
            else if (_self.mode == 'Edit') {

                data.LibID = _self.id;

                // edit
                editData(data);
            };

            //hiding modal
            $('.' + _self.ModalName).modal('hide');
        });

        // bind add property
        addPropertyButton.unbind().bind("click", function (e) {
            e.preventDefault();
            var data = {};
            var propertyselectSelectize = $("#propertyselect")[0].selectize;
            data.PropertyID = propertyselectSelectize.getValue();
            data.PropertyName = propertyselectSelectize.getItem(propertyselectSelectize.getValue())[0].innerHTML;

            var dataCustom = $('#propertyselectvalue' + data.PropertyID).data("custom");

            if (dataCustom != null && typeof dataCustom != "undefined" && dataCustom != "") {
                data.ValueName = $('#propertyselectvalue' + data.PropertyID).val();
                data.ValueID = $('#propertyselectvalue' + data.PropertyID).data("id");
                data.Custom = true;
            }
            else {
                var tempselect = $('#propertyselectvalue' + data.PropertyID)[0].selectize;
                data.ValueID = tempselect.getValue();
                data.ValueName = tempselect.getItem(tempselect.getValue())[0].innerHTML;
                data.Custom = false;
            }

            data.ValueID = (typeof data.ValueID == 'undefined' ? '' : data.ValueID);

            drawPropertyList(data);
        });

        // check validate and toggle accept button
        checkItems.bind('change keyup', function () {
            var action = false;

            checkItems.each(function () {
                if ($(this).val() == '' || $(this).val() == null) {
                    var curName = $(this).attr('name');
                    if (curName == 'Description' || curName == 'ReleaseBasisLibItemID' || curName == 'ReleaseDate' || curName == 'ReleaseNote' || curName == 'Note' || curName == 'Notes') $(this).val(' ');
                    else action = true;
                }
            });

            acceptBtn.attr("disabled", action);
        });

        // selectize
        $(".selectizeModal").each(function () {
            $(this).selectize({
                create: false
            });
        });
    }

    // collect data
    function collectData() {

        // collect data
        _self.getItems.each(function () {

            // name of Field (same as in Entity)
            var name = $(this).attr('name');
            if (name == "File") {
                _self.finalFormData.append(name, $(this)[0].files[0], $(this).val());
            }
            else {
                _self.finalFormData.append(name, $(this).val());
            }
        });

        return _self.finalFormData;
    }

    // add Data
    function addData(data) {

        var postService = _core.getService('Post').Service;

        postService.postFormData(data, _self.URL, function (res) {

            if (res != null) {
                _self.callback(res);
            }
            else alert('serious eror on adding Order');
        })
    }

}
/************************/


// creating class instance
var OrderWidget = new OrderWidgetClass();

// creating object
var OrderWidgetObject = {
    // important
    Name: 'Order',

    Widget: OrderWidget
}

// registering widget object
_core.addWidget(OrderWidgetObject);

// yes or no widget

/*****Main Function******/
var YesOrNoWidgetClass = function () {
    var _self = this;
    _self.callback = null;
    _self.content = null;
    _self.modal = $('.confirmDeleteModal');
    _self.modalUrl = "/_Popup_/Get_YesOrNO";
    _self.type = null;
    _self.ModalName = 'confirmDeleteModal';

    // action list
    _self.actionList = {
    };

    _self.init = function () {
        console.log('YesOrNo Widget Inited');
        _self.type = null;
    };

    // ask action
    _self.Ask = function (type, callback) {
        // initilize
        _self.init();
        _self.type = type;

        console.log('YesOrNo Ask called');

        // save callback
        _self.callback = callback;

        // loading view
        loadView();
    };

    // loading modal from Views
    function loadView() {

        var data = {};
        var url = _self.modalUrl;
        
        data.type = _self.type;

        _core.getService("Post").Service.postPartial(data, url, function (res) {

            _core.getService("Loading").Service.disableBlur("loaderDiv");
            _core.getService('Loading').Service.disableBlur('loaderClass');

            _self.content = res;

            // content
            $('#widgetHolder').html(_self.content);

            // bind events for yes and no
            bindEvents();

            // showing modal
            $('.' + _self.ModalName).modal('show');
        });
    }

    // binding buttons for Modal
    function bindEvents() {
        var yesBtn = $('#acceptremove');
        var noBtn = $('#declineremove');

        // unbind
        yesBtn.unbind();
        noBtn.unbind();

        // bind
        yesBtn.bind('click', function () {
            // calling callback
            if (_self.callback != null)
                _self.callback(true);

            // remove callback
            _self.callback = null;

            //hiding modal
            $('.confirmDeleteModal').modal('hide');
        });

        noBtn.bind('click', function () {
            //hiding modal
            $('.confirmDeleteModal').modal('hide');

            // call callback
            _self.callback(false);

            // remove callback
            _self.callback = null;
        });
    }
}
/************************/


// creating class instance
var YesOrNoWidget = new YesOrNoWidgetClass();

// creating object
var YesOrNoWidgetObject = {
    // important
    Name: 'YesOrNo',

    Widget: YesOrNoWidget
}

// registering widget object
_core.addWidget(YesOrNoWidgetObject);