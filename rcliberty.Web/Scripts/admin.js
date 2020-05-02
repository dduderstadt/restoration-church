//***************************************
//============== admin nav ==============
//***************************************
$(function () {
    document.getElementById('rc-admin-nav').style.right = "-180px";
    $('#rc-admin').addClass('hidden');
});

var isAdminClosed = false;

function closeAdminNav() {
    document.getElementById('rc-admin-nav').style.right = "-180px";
    isAdminClosed = true;
    $('#rc-admin').addClass('hidden');
};

function openAdminNav() {
    document.getElementById('rc-admin-nav').style.right = "0px";
    isAdminClosed = false;
    $('#rc-admin').removeClass('hidden');
}
//***************************************
