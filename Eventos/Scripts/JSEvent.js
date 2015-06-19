
$('.collapsible').collapsible();

$('.button-collapse').sideNav({
    edge: 'left', 
    closeOnClick: true 
});

$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('.slider').slider({ full_width: true });
    $('.collapsible').collapsible({
        accordion: false // A setting that changes the collapsible behavior to expandable instead of the default accordion style
    });
});
