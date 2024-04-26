$(document).ready(function () {
    $('#Cover').on('change', function () {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('.cover-preview').attr('src', e.target.result).removeClass('d-none');
        }
        reader.readAsDataURL(this.files[0]);
    })
});