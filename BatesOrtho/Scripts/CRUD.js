/// <reference path="jquery-1.10.2.intellisense.js" />
/// <reference path="jquery-1.10.2.js" />

//this file will hold all the CRUD AJAX calls and client side validation

$(document).ready(function (){

    // Validations
    $("#patient-appt").validate({     
          rules:{
            required: true, 
            txtFirstName: "required",
            txtLastName: "required",
            txtDOB: "required",
            txtRespPartyFirstName: "required",
            txtRespPartyLastName: "required",
            ddlNewPatient: "required",
            txtPhone: "required",
            txtEmail:{
                required: true,
                email: true
            },
            contactType: "required",            
            prefApptDays: {
                  required: true,
                  minlength: 1
            }

          }   
        });
        $("#patient-refer").validate({    
          rules:{
            required: true,             
            txtDoctorFirstName: "required",
            txtDoctorLastName: "required",
            txtPracticeName: "required",
            txtEmail:{
                required: true,
                email: true
            },
            txtPatientFirstName: "required",
            txtPatientLastName: "required",
            txtPatientPhone: "required",
            txtPatientEmail:{
                required: true,
                email: true
            },
            radiograph: {
                  required: true,
                  minlength: 1
              }
          }   
        });     

    $("#btnSaveAppointmentRequest").click(function(){   
        $('#patient-app').validate();   
        if($('#patient-app').valid()){ //checks if it's valid
            return true;
        }else{
           return false;
        };
    });




    $('#btnSaveSponsorshipRequest').click(function (event) {
        event.preventDefault();


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
            success: function (data) {
                window.location.href = data.url;
            },
            error: function (data) {
                alert(data);
            }
        });
    });


    $('#btnSaveDoctorReferral').click(function (event) {
        event.preventDefault();

        $('#patient-refer').validate();
        if($('#patient-refer').valid()){ 
            return true;
        }else{
            var validator = $( "#patient-refer" ).validate();
            validator.showErrors();
           return false;

        };  

        var firstName = $('#txtDoctorFirstName').val();
        var lastName = $('#txtDoctorLastName').val();
        var practiceName = $('#txtPracticeName').val();
        var email = $('#txtEmail').val();
        var patientFirstName = $('#txtPatientFirstName').val();
        var patientLastName = $('#txtPatientLastName').val();
        var patientPhone = $('#txtPatientPhone').val();
        var patientEmail = $('#txtPatientEmail').val();
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
            success: function (data) {
                window.location.href = data.url;
            },
            error: function (data) {
                alert(data);
            }
        });
    });


    $('#btnSaveAppointmentRequest').click(function (event) {
        event.preventDefault();
        var firstName = $('#txtFirstName').val();
        var lastName = $('#txtLastName').val();
        var dob = $('#txtDOB').val();
        var respPartyFirstName = $('#txtRespPartyFirstName').val();
        var respPartyLastName = $('#txtRespPartyLastName').val();
        var newPatient = $('#ddlNewPatient').val();
        var phone = $('#txtPhone').val();
        var email = $('#txtEmail').val();
        var modeOfContact = $('#contactType').val();
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
                window.location.href = data.url;
            },
            error: function (data) {
                //alert(data);
                console.log(data);
            }
        });

    });
});


