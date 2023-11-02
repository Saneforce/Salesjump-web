

function CheckNumeric(e) {

    if (window.event) // IE 
    {
        if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
            e.returnValue = false;
            return false;
        }
    }
    else { // Fire Fox
        if ((e.which < 48 || e.which > 57) & e.which != 8 & e.which != 13 & e.keyCode != 9) {
            e.preventDefault();
            return false;
        }
    }
}

function AlphaNumeric(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 32 || e.keyCode == 44 || e.keyCode == 95 || e.keyCode == 46 || e.keyCode == 45 || e.keyCode == 35 || e.keyCode == 36 || e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 8 || e.keyCode == 9)) {
            e.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 32 || e.which == 44 || e.which == 95 || e.which == 46 || e.which == 45 || e.which == 35 || e.which == 36 || e.which == 37 || e.which == 38 || e.which == 8 || e.keyCode == 9)) {
            e.preventDefault();
            return false;

        }
    }
}


function CharactersOnly(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.which == 8 || e.keyCode == 9 || e.keyCode == 32 || e.keyCode == 95)) {
            e.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!((e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 8 || e.keyCode == 9 || e.which == 32 || e.which == 95)) {
            e.preventDefault();
            return false;
        }
    }
}

//Email

function ValidateEmail(mail)   
{  
 if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(myForm.emailAddr.value))  
  {  
    return (true)  
  }  
    alert("You have entered an invalid email address!")  
    return (false)  
}
function AlphaNumeric_enter(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 32 || e.keyCode == 44 || e.keyCode == 95 || e.keyCode == 46 || e.keyCode == 45 || e.keyCode == 35 || e.keyCode == 36 || e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 13)) {
            e.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 32 || e.which == 44 || e.which == 95 || e.which == 46 || e.which == 45 || e.which == 35 || e.which == 36 || e.which == 37 || e.which == 38 || e.which == 8 || e.keyCode == 9 || e.keyCode == 13)) {
            e.preventDefault();
            return false;

        }
    }
}
function Calendar_enter(e) {

    if (window.event) // IE 
    {
        if (!( e.keyCode == 8 || e.keyCode == 9 )) {
            e.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!(e.which == 8 || e.keyCode == 9 )) {
            e.preventDefault();
            return false;

        }
    }
}
function AlphaNumeric_NoSpecialChars(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 32 || e.keyCode == 8 || e.keyCode == 9)) {
            e.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 32 || e.which == 8 || e.keyCode == 9)) {
            e.preventDefault();
            return false;

        }
    }
}

function AlphaNumeric_NoSpecialChars_New(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 47 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 32 || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 42 || e.keyCode != 43)) {
            e.returnValue = false;
            return false;

        }
        if (e.keyCode == 39 || e.keyCode == 0) {
            e.preventDefault();
            return false;
        }
    }
    else { // Fire Fox
        if (!((e.which >= 47 && e.which <= 57) || (e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 32 || e.which == 8 || e.keyCode == 9 || e.keyCode == 42 || e.keyCode != 43)) {
            e.preventDefault();
            return false;

        }
        if (e.which == 39 || e.which == 0) {
            e.preventDefault();
            return false;
        }
    }
}


function CheckNumeric_dot(e) {

    if (window.event) // IE 
    {
        if ((e.keyCode < 48 || e.keyCode > 57) && e.keyCode != 8 && e.keyCode != 13 && e.keyCode != 9 && e.keyCode!=49) {
            e.returnValue = false;
            return false;
        }
    }
    else { // Fire Fox
        if ((e.which < 48 || e.which > 57) && e.which != 8 && e.which != 13 && e.keyCode != 9 && e.keyCode!=49) {
            e.preventDefault();
            return false;
        }
    }
}