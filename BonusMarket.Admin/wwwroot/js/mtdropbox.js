var MTDropBox = function (dropzone) {
	var self = this;

	var count = 1;
	var imageType = /^image\//;

	var dzoneobj = document.getElementById(dropzone);

	dzoneobj.addEventListener("dragenter", dragenter, false);
	dzoneobj.addEventListener("dragleave", dragleave, false);
	dzoneobj.addEventListener("dragover", dragover, false);
	dzoneobj.addEventListener("drop", drop, false);

	function dragenter(e) {
		e.stopPropagation();
		e.preventDefault();
		dzoneobj.classList.remove("dragleave");
		dzoneobj.classList.add("dragenter");
	}

	function dragleave(e) {
		e.stopPropagation();
		e.preventDefault();
		dzoneobj.classList.remove("dragenter");
		dzoneobj.classList.add("dragleave");
	}

	function dragover(e) {
		e.stopPropagation();
		e.preventDefault();
	}

	var filesArr = [];
	var filesArrGeneral = [];

	function drop(e) {
		e.stopPropagation();
		e.preventDefault();
		dzoneobj.classList.remove("dragenter");
		dzoneobj.classList.add("dragleave");

		var dt = e.dataTransfer;
		var files = dt.files;

		//if (filesArr.length != 0) {
		//	for (var i = 0; i < filesArr.length; i++) {
		//		var filesArrName = filesArr[i].name;
		//		var filesArrLastModified = filesArr[i].lastModified;
		//		var filesArrSize = filesArr[i].size;
		//		for (var j = 0; j < files.length; j++) {
		//			var filesName = files[j].name;
		//			var filesLastModified = files[j].lastModified;
		//			var filesSize = files[j].size;
		//			if ((filesArrName !== filesName) && (filesArrLastModified !== filesLastModified) && (filesArrSize !== filesSize)) {
		//				filesArr.push(files[j]);
		//			}
		//		}
		//	}
		//}
		//else {
		//	filesArr = files;
		//}

		if (filesArrGeneral.length < count) {
			console.log("updating files array");
			for (var i = 0; i < count; i++) {
				if (typeof files[i] !== "undefined") {
					console.log(files[i]);
					if (!imageType.test(files[i].type)) {
						continue;
					}
					console.log("valid image file");
					filesArrGeneral.push(files[i]);
				}
				else {
					console.log("not a valid image file");
				}
			}
			handleFiles(filesArrGeneral);
		}
		else {
			console.log("files array is full");
		}
	}

	var preview = dzoneobj.querySelector(".coverImageIn");

	function handleFiles(files) {
		if (!files.length) {
			preview.innerHTML = "<span>No files selected!</span>";
		}
		else {
			preview.innerHTML = "";
			var list = document.createElement("ul");
			list.classList.add("MTDropBoxImgUl");
			preview.appendChild(list);
			for (var i = 0; i < files.length; i++) {

				var file = files[i];

				if (!imageType.test(file.type)) {
					continue;
				}

				var li = document.createElement("li");
				li.classList.add("MTDropBoxImgLi", ("index-" + i));
				var div = document.createElement("div");
				list.appendChild(li);

				var img = document.createElement("img");
				img.src = window.URL.createObjectURL(files[i]);
				img.height = 60;
				img.classList.add("MTDropBoxImg");
				img.onload = function () {
					window.URL.revokeObjectURL(this.src);
				};
				div.appendChild(img);
				// divControls
				var divControls = document.createElement("div");
				divControls.classList.add("coverImageInControls");
				// divControlsBtnGroup
				var divControlsBtnGroup = document.createElement("div");
				divControlsBtnGroup.classList.add("btn-group", "btn-group-sm", "nowrap");
				divControls.appendChild(divControlsBtnGroup);
				// divControlsBtn
				var divControlsDeleteBtn = document.createElement("button");
				divControlsBtnGroup.appendChild(divControlsDeleteBtn);
				divControlsDeleteBtn.classList.add("btn", "btn-flat", "btn-danger", "deleteImgBtn");
				var divControlsDeleteBtnIcon = document.createElement("i");
				divControlsDeleteBtnIcon.classList.add("fa", "fa-trash");
				divControlsDeleteBtn.appendChild(divControlsDeleteBtnIcon);
				div.appendChild(divControls);
				var info = document.createElement("span");
				info.innerHTML = files[i].name;
				div.appendChild(info);
				li.appendChild(div);
			}
		}
	}

	function findAncestor (el, cls) {
		while ((el = el.parentElement) && !el.classList.contains(cls));
		return el;
	}

	document.addEventListener('click', function (e) {
		var deleteBtn;
		if (e.target && e.target.classList.contains("deleteImgBtn")) {
			deleteBtn = e.target;
			console.log(deleteBtn);
			deleteImg(deleteBtn);
		}
		else if (e.target && e.target.parentElement.classList.contains("deleteImgBtn")) {
			deleteBtn = e.target.parentElement;
			console.log(deleteBtn);
			deleteImg(deleteBtn);
		}
	});

	function deleteImg(btn) {
		var btnParent = findAncestor(btn, "MTDropBoxImgLi");
		var btnParentClass = btnParent.classList[1].split("-");
		var btnParentIndex = btnParentClass[1];
		filesArrGeneral.splice(btnParentIndex, 1);
		handleFiles(filesArrGeneral);
	}

	function sendFiles() {
		var imgs = document.querySelectorAll(".MTDropBoxImg");

		for (var i = 0; i < imgs.length; i++) {
			new FileUpload(imgs[i], imgs[i].file);
		}
	}

	function init() {

	}


	init();
};