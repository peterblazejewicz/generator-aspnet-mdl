if ($.validator && $.validator.unobtrusive) {
    $.validator.unobtrusive.options = {
        errorClass: "is-invalid"
    };
    $.validator.setDefaults({
        highlight: function(element, errorClass, validClass) {
            $(element).closest('.mdl-js-textfield').addClass(errorClass);
        },
        unhighlight: function(element, errorClass, validClass) {
            $(element).closest('.mdl-js-textfield').removeClass(errorClass);

        }
    });
}
