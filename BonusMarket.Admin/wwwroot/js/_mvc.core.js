/*************MVC CORE**********/

window.mvccore = function () {
    var self = this;

    self.cList = []; // Controllers
    self.mList = []; // Models
    self.wList = []; // Widgets
    self.modList = []; // Modules
    self.sList = []; // Services

    // add
    self.addController = function (obj) {
        removeObjectFromArray(obj, self.cList);
        addObjectToArray(obj, self.cList);
    };

    self.addModel = function (obj) {
        removeObjectFromArray(obj, self.mList);
        addObjectToArray(obj, self.mList);
    };

    self.addService = function (obj) {
        removeObjectFromArray(obj, self.sList);
        addObjectToArray(obj, self.sList);
    };

    self.addWidget = function (obj) {
        removeObjectFromArray(obj, self.wList);
        addObjectToArray(obj, self.wList);
    };

    // get
    self.getController = function (Name) {
        return getObjectFromArrayByName(Name, self.cList);
    };

    self.getModel = function (Name) {
        return getObjectFromArrayByName(Name, self.mList);
    };

    self.getService = function (Name) {
        return getObjectFromArrayByName(Name, self.sList);
    };

    self.getWidget = function (Name) {
        return getObjectFromArrayByName(Name, self.wList);
    };

    // Secondary functions
    function removeObjectFromArray(obj, array) {
        try {
            for (var i = array.length - 1; i >= 0; i--) {
                if (array[i].Name == obj.Name) {
                    array.splice(i, 1);
                }
            }
        }
        catch (e) {
            console.error(e);
        }
    }

    function addObjectToArray(obj, array) {
        try {
            array.push(obj)
        }
        catch (e) {
            console.error(e);
        }
    }

    function getObjectFromArrayByName(Name, array) {
        try {
            for (var i = array.length - 1; i >= 0; i--) {
                if (array[i].Name == Name) {
                    return array[i];
                }
            }
            return null;
        }
        catch (e) {
            console.error(e);
        }
    }
};

// main instance
var _core = new window.mvccore();