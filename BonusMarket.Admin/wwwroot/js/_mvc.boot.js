/**** BOOT PART ****/

$(document).ready(function() {
    // getting controller and action
    
    var currentControllerName = "service";//;getControllerNameFromUrl() || "Home";
    var currentActionName = getActionNameFromUrl();
    var currentId = getIdFromUrl();
    var localeName = getLocaleNameFromUrl();

    // load selectize no delete
    loadSelectizePlugins();

    // create jquery double click and single click fucntion
    jqueryDoubleClick();


    // methods
    function getControllerNameFromUrl() {
        var manJSCmon = location.pathname.split("/");

        var name = manJSCmon[1];
        if (name == "en" || name == "hy" || name == "ru")  return manJSCmon[2];
        //var local = location.protocol + '//' + location.host + "/" + manJSCmon[1] + "/" + manJSCmon[2];
        return manJSCmon[1] || null;
    }

    function getLocaleNameFromUrl() {
        var manJSCmon = location.pathname.split("/");

        var name = manJSCmon[1];
        if (name == "en" || name == "hy" || name == "ru") return name;
        else return null;
    }
    function getActionNameFromUrl() {
        var manJSCmon = location.pathname.split("/");
        if (manJSCmon[1] == "en" || manJSCmon[1] == "hy" || manJSCmon[1] == "ru") return manJSCmon[3];

            //var local = location.protocol + '//' + location.host + "/" + manJSCmon[1] + "/" + manJSCmon[2];
        return manJSCmon[2] || null;
    }
    function getIdFromUrl() {
        var manJSCmon = location.pathname.split("/");

        if (manJSCmon[1] == "en" || manJSCmon[1] == "hy" || manJSCmon[1] == "ru") return manJSCmon[4];

        //var local = location.protocol + '//' + location.host + "/" + manJSCmon[1] + "/" + manJSCmon[2];
        return manJSCmon[3] || null;
    }

    if (currentControllerName != null) {
        // default action name
        if (currentActionName == null) currentActionName = 'Index';

        //getting controller
        var currentControllerObject = _core.getController(currentControllerName);
        if (currentControllerObject != null) {
            var currentController = currentControllerObject.Controller;
            try {
                if (currentActionName == 'Index' || currentActionName == 'index') currentController.Index(); // calling Index
                else if (currentActionName == 'Add' || currentActionName == 'add') currentController.Add(currentId); // calling add
                else if (currentActionName == 'Edit' || currentActionName == 'edit') currentController.Edit(currentId); // calling edit
                else if (currentActionName == 'View' || currentActionName == 'view') currentController.View(currentId); // calling view
                else if (currentActionName == 'Items' || currentActionName == 'items') currentController.Items(currentId); // calling view
                else if (currentActionName == 'ItemsElems' || currentActionName == 'itemselems') currentController.ItemsElems(currentId); // calling view
                else if (currentActionName == 'ItemAdd' || currentActionName == 'itemadd') currentController.ItemAdd(currentId); // calling view
                else if (currentActionName == 'ItemEdit' || currentActionName == 'itemedit') currentController.ItemEdit(currentId); // calling view
                else if (currentActionName == 'ItemsEdit' || currentActionName == 'itemsedit') currentController.ItemsEdit(currentId); // calling view
                else if (currentActionName == 'ItemsAdd' || currentActionName == 'itemsadd') currentController.ItemsAdd(currentId); // calling view
                else if (currentActionName == 'ItemsElemsAdd' || currentActionName == 'itemselemsadd') currentController.ItemsElemsAdd(currentId); // calling view
                else if (currentActionName == 'ItemsElemsEdit' || currentActionName == 'itemselemsedit') currentController.ItemsElemsEdit(currentId); // calling view
                else if (currentActionName == 'ItemsRegulationsAdd' || currentActionName == 'itemsregulationsadd') currentController.ItemsRegulationsAdd(currentId); // calling view
                else if (currentActionName == 'ItemsRegulationsEdit' || currentActionName == 'itemsregulationsedit') currentController.ItemsRegulationsEdit(currentId); // calling view
                else currentController.Index();
            }
        catch (e) {
                console.warn(e);
            }
        }
        else {
            console.warn('no controller registered by name ', currentControllerName);
        }

        // changeTab(currentControllerName);
        bindHeaderEvents();
    }
    else {
        console.warn('could not get controller or action name from pathname');
        $(document).keypress(function (e) {
            if (e.which == 13) {
                $('#loginBtn').click();
            }
        });
    }

    function changeTab(controllerName) {

        var menuTab = $('.menutab');

        menuTab.removeClass('selected');

        menuTab.each(function () {
            if ($(this).attr('name') == controllerName) {
                $(this).addClass('selected');
            }
        });

    }

    function loadSelectizePlugins() {

        Selectize.define('no-delete', function (options) {
            this.deleteSelection = function () { };
        });
    }

    function jqueryDoubleClick() {
    
        jQuery.fn.single_double_click = function (single_click_callback, double_click_callback, timeout) {
            return this.each(function () {
                var clicks = 0, self = this;
                jQuery(this).click(function (event) {
                    clicks++;
                    if (clicks == 1) {
                        setTimeout(function () {
                            if (clicks == 1) {
                                single_click_callback.call(self, event);
                            } else {
                                double_click_callback.call(self, event);
                            }
                            clicks = 0;
                        }, timeout || 300);
                    }
                });
            });
        }
    }

    function bindHeaderEvents() {

        // bind lang change
        $(".langChange").unbind().bind("click", function(e) {
            e.preventDefault();

            var lg = $(this).data("lg");

            _core.getService("Post").Service.postJson({"lg": lg}, "/api/switchlang", function(res){

                // reload
                location.reload();
            });
        });
    }

});