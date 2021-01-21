//***************************************
//========== live notification ==========
//***************************************
//function stopBlink() {
//    $('#live-notification').removeClass('blink');
//}

//function startBlink() {
//    $('#live-notification').addClass('blink');
//}
//***************************************

//***************************************
//============== side nav ===============
//***************************************
var isClosed = true;

function closeSideNav() {
    document.getElementById('side-nav').style.left = "-80px";
    isClosed = true;
    $('#social').removeClass('hidden');
};

function openSideNav() {
    document.getElementById('side-nav').style.left = "0px";
    isClosed = false;
    $('#social').addClass('hidden');
};
//***************************************