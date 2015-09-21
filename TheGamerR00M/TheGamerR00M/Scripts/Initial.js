var blnHasLoaded = false
var windowsize = $(window).width();
hasLoaded();

if (window.onload = true && blnHasLoaded == false) {
    blnHasLoaded = true
    //location.reload();
}
// Check initial screen size and show home if on mobile
if (windowsize < 769) {
    //  If user clicks on navbar in mobile close navbar
    $('#Home').show()
}
else {
    $('#Home').hide()
}
//  Set width of screen to variable to store
function checkWidth() {
    windowsize = $(window).width();
    if (windowsize < 769) {
        //  If user clicks on navbar in mobile close navbar
        $('#Home').show()
    }
    else {
        $('#Home').hide()
    }
}
//  close the sub menu if on mobile or when submenu appears
function closeSub() {
    if (windowsize < 769) {
        //  If user clicks on navbar in mobile close navbar
        $('.navbar-toggle').click()
    }
}

function hasLoaded() {
    //  Check Width of Screen
    $(window).resize(checkWidth)
    // Preload animation run
    $(window).load(function () {
        // Animate loader off screen
        $('body').addClass('loaded');
        console.log("Made it")        
    });
}