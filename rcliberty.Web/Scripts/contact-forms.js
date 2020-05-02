//========== confirm connect ==========
setTimeout(function () {
    $("#rc-connect-success").fadeOut(1000);
}, 3000);

//========== email error ==========
setTimeout(function () {
    $("#rc-connect-error").fadeOut(1000);
}, 3000);

//= plan your visit (first time guests) =
function getViewportWidth() {
    let vpW = $(document).width();

    if (vpW > 991) {
        /***** CONNECT *****/
        //handle name
        $('#connectName').addClass("form-group");
        $('#connectName').addClass("row");
        $('#connectFName').removeClass("form-group");
        $('#connectFName').removeClass("row");
        $('#connectLName').removeClass("form-group");
        $('#connectLName').removeClass("row");
        $('#connectFName').children("div").removeClass("col-md-12");
        $('#connectFName').children("div").addClass("col-md-6");
        $('#connectLName').children("div").removeClass("col-md-12");
        $('#connectLName').children("div").addClass("col-md-6");

        /***** CONNECT *****/
        //handle detail
        $('#connectDetail').addClass("form-group");
        $('#connectDetail').addClass("row");
        $('#connectSubject').removeClass("form-group");
        $('#connectSubject').removeClass("row");
        $('#connectEmail').removeClass("form-group");
        $('#connectEmail').removeClass("row");
        $('#connectSubject').children("div").removeClass("col-md-12");
        $('#connectSubject').children("div").addClass("col-md-6");
        $('#connectEmail').children("div").removeClass("col-md-12");
        $('#connectEmail').children("div").addClass("col-md-6");


        /***** GUEST VISIT *****/
        //handle name
        $('#guestVisitName').addClass("form-group");
        $('#guestVisitName').addClass("row");
        $('#guestVisitFName').removeClass("form-group");
        $('#guestVisitFName').removeClass("row");
        $('#guestVisitLName').removeClass("form-group");
        $('#guestVisitLName').removeClass("row");
        $('#guestVisitFName').children("div").removeClass("col-md-12");
        $('#guestVisitFName').children("div").addClass("col-md-6");
        $('#guestVisitLName').children("div").removeClass("col-md-12");
        $('#guestVisitLName').children("div").addClass("col-md-6");

        //handle contact
        $('#guestVisitContact').addClass("form-group");
        $('#guestVisitContact').addClass("row");
        $('#guestVisitEmail').removeClass("form-group");
        $('#guestVisitEmail').removeClass("row");
        $('#guestVisitPhone').removeClass("form-group");
        $('#guestVisitPhone').removeClass("row");
        $('#guestVisitEmail').children("div").removeClass("col-md-12");
        $('#guestVisitEmail').children("div").addClass("col-md-6");
        $('#guestVisitPhone').children("div").removeClass("col-md-12");
        $('#guestVisitPhone').children("div").addClass("col-md-6");


    } else if (vpW <= 991) {
        /***** CONNECT *****/
        //handle name
        $('#connectName').removeClass("form-group");
        $('#connectName').removeClass("row");
        $('#connectFName').addClass("form-group");
        $('#connectFName').addClass("row");
        $('#connectLName').addClass("form-group");
        $('#connectLName').addClass("row");
        $('#connectFName').children("div").addClass("col-md-12");
        $('#connectFName').children("div").removeClass("col-md-6");
        $('#connectLName').children("div").addClass("col-md-12");
        $('#connectLName').children("div").removeClass("col-md-6");

        /***** CONNECT *****/
        //handle detail
        $('#connectDetail').removeClass("form-group");
        $('#connectDetail').removeClass("row");
        $('#connectSubject').addClass("form-group");
        $('#connectSubject').addClass("row");
        $('#connectEmail').addClass("form-group");
        $('#connectEmail').addClass("row");
        $('#connectSubject').children("div").addClass("col-md-12");
        $('#connectSubject').children("div").removeClass("col-md-6");
        $('#connectEmail').children("div").addClass("col-md-12");
        $('#connectEmail').children("div").removeClass("col-md-6");


        /***** GUEST VISIT *****/
        //handle name
        $('#guestVisitName').removeClass("form-group");
        $('#guestVisitName').removeClass("row");
        $('#guestVisitFName').addClass("form-group");
        $('#guestVisitFName').addClass("row");
        $('#guestVisitLName').addClass("form-group");
        $('#guestVisitLName').addClass("row");
        $('#guestVisitFName').children("div").addClass("col-md-12");
        $('#guestVisitFName').children("div").removeClass("col-md-6");
        $('#guestVisitLName').children("div").addClass("col-md-12");
        $('#guestVisitLName').children("div").removeClass("col-md-6");

        //handle contact
        $('#guestVisitContact').removeClass("form-group");
        $('#guestVisitContact').removeClass("row");
        $('#guestVisitEmail').addClass("form-group");
        $('#guestVisitEmail').addClass("row");
        $('#guestVisitPhone').addClass("form-group");
        $('#guestVisitPhone').addClass("row");
        $('#guestVisitEmail').children("div").addClass("col-md-12");
        $('#guestVisitEmail').children("div").removeClass("col-md-6");
        $('#guestVisitPhone').children("div").addClass("col-md-12");
        $('#guestVisitPhone').children("div").removeClass("col-md-6");
    }
}

function verifyContact(contact) {
    if (contact.value === "phone") {
        $('#phoneNbr').attr("required", "required");
        $('#email').removeAttr("required", "required");

    } else if (contact.value === "email") {
        $('#email').attr("required", "required");
        $('#phoneNbr').removeAttr("required", "required");
    }
}

$('#frmGuestVisit').on('submit', function () {
    let prefContact = $('#PreferredContact').find(":selected").text();

    if (prefContact === "--Select an option--") {
        alert("Please choose a valid contact method");
        return false;
    }
})

function isBringingKidsCheck(that) {
    let attributes =
    {
        required: "required",
        min: 1,
        max: 255
    };

    if (that.value === "true") {
        $('#kidsDetail').removeClass("hidden");
        $('#totalNbrOfKids').attr(attributes);

        let nbrOfKids = $('#totalNbrOfKids').val();

    } else {
        //do nothing
        $('#kidsDetail').addClass("hidden");
        $('#totalNbrOfKids').removeAttr("required");
        $('#totalNbrOfKids').removeAttr("min");
        $('#totalNbrOfKids').removeAttr("max");
    }
}

function PopulateGuestVisitFieldsOnError(firstName, lastName, email, phoneNbr, preferredContact, isBringingKids, totalNbrOfKids, addtlQuestions) {
    $('#firstName').val(firstName);
    $('#lastName').val(lastName);
    $('#email').val(email);
    $('#phoneNbr').val(phoneNbr);
    $('#preferredContact').val(preferredContact);
    if (isBringingKids === "True") {
        $('#isBringingKids').val("true");
    } else {
        $('#isBringingKids').val("false");
    }
    $('#totalNbrOfKids').val(totalNbrOfKids);
    $('#addtlQuestions').val(addtlQuestions);
}

    //function generateRows(totalNbrOfKids) {
    //    alert("GENERATE ROWS " + totalNbrOfKids.value);
    //    $('#kidsNames').removeClass("hidden");

    //    let htmlString = "";

    //    for (let i = 0; i < totalNbrOfKids.value; i++) {
    //        htmlString += "<div class='form-group row'><div class='col-md-6'><input type='text' class='form-control' id='kid" + i + "Name' name='kid" + i + "Name' placeholder='First Name' onchange='addChild(this)'/></div><div class='col-md-6'><button type='button' class='btn btn-success' style='width: 100%;' disabled='disabled'>Add</button></div></div>"
    //    }

    //    $('#kidsNames').html(htmlString);

    //}