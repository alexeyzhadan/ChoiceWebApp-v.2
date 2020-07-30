$(document).ready(() => {

    $(function() {
        if ($('.check-student').is(':checked')) {
            $('.student-info').css('display', 'none');
        }
    });

    $('.check-student').change(() => {
        $('.student-info').toggle();
    });

});