$(document).ready(function () {

    $(document).delegate('.dropdown', 'click', function (event) {

        $('.dropdown-menu').removeClass('hide');
        $('.dropdown-menu').addClass('show');

    });

    $(document).on("click", function (event) {

        var $trigger = $(".dropdown");
        if ($trigger !== event.target && !$trigger.has(event.target).length) {
            
            $('.dropdown-menu').removeClass('show');
            $('.dropdown-menu').addClass('hide');
        }
    });

    //$("*").click(function (event) {
    //        if (!$(event.target).is(".dropdown-toggle")) {
    //             $('.dropdown-menu').toggleClass('show');
    //            return false;
    //        } else {
    //            alert("Pencere içine tıklandı.");
    //            return false;
    //        }
    //    });

})