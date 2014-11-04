/// <reference path="jquery-1.10.2.intellisense.js" />
/// <reference path="jquery-1.10.2.js" />

//this file will hold all the CRUD AJAX calls and client side validation

$(document).ready(function (){

    $('#btnSaveContactRequest').click(function () {
        
        var firstName = $('#firstName').val();
        var lastName = $('#lastName').val();
        var email = $('#txtEmail').val();
        var message = $('#message').val();
    
        $.ajax({
            url: "/Home/CreateContactRequest",
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                request: {
                    'FirstName': firstName,
                    'LastName': lastName,
                    'Email': email,
                    'Message': message
                }
            }),
            success: function () {
                $('#firstName').val('');
                $('#lastName').val('');
                $('#txtEmail').val('');
                $('#message').val('');
            },
            error: function (data) {
                alert(data);
            }
        })
    });

    $('#btnSaveSponsorshipRequest').click(function () {
        
        var checkedValues = $('input[name="typeOfAd"]:checked').map(function () {
            return this.value;
        }).get();
        
        var date = $('#dateSponsorshipRequest').val();
        var firstName = $('#txtFirstName').val();
        var lastName = $('#txtLastName').val();
        var phone = $('#txtPhone').val();
        var address = $('#txtAddress').val();
        var addressLine2 = $('#txtAddressLine2').val();
        var city = $('#txtCity').val();
        var state = $('#ddlState').val();
        var zip = $('#txtZip').val();
        var status = $('#txtPatientTreatmentStatus').val();
        var organization = $('#txtOrganization').val();
        var checkpayableto = $('#txtCheckPayableTo').val();
        var comments = $('#txtComments').val();

        $.ajax({
            url: "/Home/CreateSponsorshipRequest",
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                request: {
                    'Date': date,
                    'FirstName': firstName,
                    'LastName': lastName,
                    'PhoneNumber': phone,
                    'Address': address,
                    'AddressLine2': addressLine2,
                    'City': city,
                    'State': state,
                    'Zip': zip,
                    'PatientTreatmentStatus': status,
                    'Organization': organization,
                    'CheckPayableTo': checkpayableto,
                    'Address': address,
                    'AddressLine2': addressLine2,
                    'City': city,
                    'State': state,
                    'Zip': zip,
                    'Comments': comments,
                    'AdTypes': checkedValues
                } 
            }),
            success: function () {
                $('#dateSponsorshipRequest').val('');
                $('#txtFirstName').val('');
                $('#txtLastName').val('');
                $('#txtPhone').val('');
                $('#txtAddress').val('');
                $('#txtAddressLine2').val('');
                $('#txtCity').val('');
                $('#ddlState').val('');
                $('#txtZip').val('');
                $('#txtPatientTreatmentStatus').val('');
                $('#txtOrganization').val('');
                $('#txtCheckPayableTo').val('');
                $('#txtComments').val('');
            },
            error: function (data) {
                alert(data);
            }
        });
    });


    $('#btnSaveDoctorReferral').click(function () {
        var firstName = $('#txtDoctorFirstName').val();
        var lastName = $('#txtDoctorLastName').val();
        var practiceName = $('#txtPracticeName').val();
        var email = $('#txtEmail').val();
        var patientFirstName = $('#txtPatientFirstName').val();
        var patientLastName = $('#txtPatientLastName').val();
        var patientPhone = $('#txtPatientPhone').val();
        var patientEmail = $('#txtEmail').val();
        var radiographsSent = $("input:radio[name ='radiograph']:checked").val();
        var comments = $('#txtComments').val();

        $.ajax({
            url: "/Home/CreateDoctorReferral",
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                referral: {
                    'DoctorFirstName': firstName,
                    'DoctorLastName': lastName,
                    'PracticeName': practiceName,
                    'DoctorEmail': email,
                    'PatientFirstName': patientFirstName,
                    'PatientLastName': patientLastName,
                    'PatientPhoneNumber': patientPhone,
                    'PatientEmailAddress': patientEmail,
                    'RadiographsSent': radiographsSent,
                    'Comments': comments
                }
            }),
            success: function () {
                $('#txtDoctorFirstName').val('');
                $('#txtDoctorLastName').val('');
                $('#txtPracticeName').val('');
                $('#txtEmail').val('');
                $('#txtPatientFirstName').val('');
                $('#txtPatientLastName').val('');
                $('#txtPatientPhone').val('');
                $('#txtEmail').val('');
                $("input:radio[name ='radiographs']").val('');
                $('#txtComments').val('');
            },
            error: function (data) {
                alert(data);
            }
        });
    });


    $('#btnSaveAppointmentRequest').click(function () {
        var firstName = $('#txtFirstName').val();
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

        //for the checkboxes
        var checkedValues = $('input[name="prefApptDays"]:checked').map(function () {
            return this.value;
        }).get();

        //var jsonarray =  JSON.stringify(checkedValues);
        //console.log(jsonarray);
        //console.log(checkedValues);
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
                    'AdditionalComments': comments,
                    'PreferredDays': checkedValues
                }
                //preferredDays: { 'AppointmentRequestDay': checkedValues }

            }),
            success: function (data) {
                //alert(data);
                $('#txtFirstName').val('');
                $('#txtLastName').val('');
                $('#txtDOB').val('');
                $('#txtRespPartyFirstName').val('');
                $('#txtRespPartyLastName').val('');
                $('#txtPhone').val('');
                $('#txtEmail').val('');
                $('#txtConvenientTimes').val('');
                $('#txtHowDidYouHear').val('');
                $('#txtGeneralDentistName').val('');
                $('#txtGeneralDentistName').val('');
                $('#txtAdditionalComments').val('');
                //$(this).closest('form').find("input[type=text], textarea").val("");
            },
            error: function (data) {
                //alert(data);
                console.log(data);
            }
        });

        //now insert the preferred times into the PreferredAppointmentDay table
        //$.ajax({
        //    url: "/Home/CreatePreferredAppointmentDay",
        //    type: "POST",
        //    contentType: 'application/json; charset=utf-8',
        //    data: JSON.stringify(checkedValues),
        //    success: function (data) {
        //        alert('success');
        //    },
        //    error: function (data) {
        //        alert(data);
        //    }
        //});

    });
});


