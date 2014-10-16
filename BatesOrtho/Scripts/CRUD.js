/// <reference path="jquery-1.10.2.intellisense.js" />
/// <reference path="jquery-1.10.2.js" />

//this file will hold all the CRUD AJAX calls and client side validation

$(document).ready(function (){

    $('#btnSaveAppointmentRequest').click(function () {
        var firstName = $('#txtFirstName').val();
        //alert(firstName);

        var lastName = $('#txtLastName').val();
        var dob = $('#txtDOB').val();
        var respPartyFirstName = $('#txtRespPartyFirstName').val();
        var respPartyLastName = $('#txtRespPartyLastName').val();
        var newPatient = $('#ddlNewPatient').val();
        var phone = $('#txtPhone').val();
        var email = $('#txtEmail').val();
        var modeOfContact = $('#ddlModeOfContact').val();
        var convenientTimes = $('#txtConvenientTimes').val();
        var howDidYouHear = $('#txtHowDidYouHear').val();
        var dentistName = $('#txtGeneralDentistName').val();
        var comments = $('#txtAdditionalComments').val();

        $.ajax({
            url: "/Home/CreateAppointmentRequest",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                apptRequest: {
                    'FirstName': firstName,
                    'LastName': lastName,
                    'DOB': dob,
                    'ResponsiblePartyFirstName': respPartyFirstName,
                    'ResponsiblePartyLastName': respPartyLastName,
                    'IsNewPatient': newPatient,
                    'Phone': phone,
                    'Email': email,
                    'PreferredModeOfContact': modeOfContact,
                    'ConvenientTimes': convenientTimes,
                    'HowDidYouHear': howDidYouHear,
                    'GeneralDentistName': dentistName,
                    'AdditionalComments': comments
                }
            }),
            success: function (data) {
                alert(data);
            },
            error: function (data) {
                alert(data);
            }
        });
    });
});


