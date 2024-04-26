$.validator.addMethod('fileSize', function (value, element, params) {
    var s = this.optional(element) || element.files[0].size <= param;
    return s;
});