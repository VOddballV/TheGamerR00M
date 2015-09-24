﻿$(document).on('change', '.btn-file :file', function () {
    // Removes previous Image
    var myNode = document.getElementById("userpic");
    while (myNode.firstChild) {
        myNode.removeChild(myNode.firstChild);
    }
    // Gets image from user
    var input = $(this),
        numFiles = input.get(0).files ? input.get(0).files.length : 1,
        label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
    input.trigger('fileselect', [numFiles, label]);
    var file = input.get(0).files[0];
    var reader = new FileReader();
    var name = input.value;
    reader.onload = function (e) {
        $("#userpic").append("<img src='" + e.target.result + "' name='postImage'/>");
    };
    reader.readAsDataURL(file);
});

$(document).ready(function () {
    $('.btn-file :file').on('fileselect', function (event, numFiles, label) {

        var input = $(this).parents('.input-group').find(':text'),
            log = numFiles > 1 ? numFiles + ' files selected' : label;

        if (input.length) {
            input.val(log);
        } else {
            if (log) alert(log);
        }

    });
});