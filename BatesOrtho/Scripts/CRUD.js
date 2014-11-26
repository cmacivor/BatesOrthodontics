/// <reference path="jquery-1.10.2.intellisense.js" />
/// <reference path="jquery-1.10.2.js" />
/// <reference path="bootstrap-datepicker.js" />

//this file will hold all the CRUD AJAX calls and client side validation

$(document).ready(function (){

        $("#contact-us").validate({    
          rules:{
            required: true, 
            firstName: "required",                       
            lastName: "required",
            txtEmail:{
                required: true,
                email: true
            },
            message: "required"
          },
          submitHandler: function (form) {
              var firstNameC = $('#firstName').val();
              var lastNameC = $('#lastName').val();
              var emailC = $('#txtEmail').val();
              var messageC = $('#message').val();
            $.ajax({
                url: "/Home/CreateContactRequest",
                type: 'POST',
                dataType: 'JSON',
                
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    request: {
                        'FirstName': firstNameC,
                        'LastName': lastNameC,
                        'Email': emailC,
                        'Message': messageC
                    }
                }),
                success: function (data, status, jqXHR) {
                    window.location.href = data.url;
                },
                error: function (jqXHR, status, err) {
                    console.log(data);
                },
                complete: function (data) {
                }
            });
        }
    });
     
    $('#dateSponsorshipRequest').datepicker({
        autoclose: true,

    }).on('changeDate', function (evt) {
        //alert('woo');
        if ($('#dateSponsorshipRequest').valid()) {
            $('#dateSponsorshipRequest').removeClass('invalid').addClass('success');
        }
    });

    $('#txtDOB').datepicker({
        autoclose: true,

    }).on('changeDate', function (evt) {
        //alert('woo');
        if ($('#txtDOB').valid()) {
            $('#txtDOB').removeClass('invalid').addClass('success');
        }
    });

    $("#sponsorshipRequest").validate({    
      rules:{
        required: true,
        txtFirstName: "required",
        txtLastName: "required",
        txtPhone: "required"                       
      },
      submitHandler: function (form) {

          var checkedValuesS = $('input[name="typeOfAd"]:checked').map(function () {
              return this.value;
          }).get();

          var dateS = $('#dateSponsorshipRequest').val();
          var firstNameS = $('#txtSponsorFirstName').val();
          var lastNameS = $('#txtSponsorLastName').val();
          var phoneS = $('#txtSponsorPhone').val();
          var addressS = $('#txtAddress').val();
          var addressLine2S = $('#txtAddressLine2').val();
          var cityS = $('#txtCity').val();
          var stateS = $('#ddlState').val();
          var zipS = $('#txtZip').val();
          var statusS = $('#txtPatientTreatmentStatus').val();
          var organizationS = $('#txtOrganization').val();
          var checkpayabletoS = $('#txtCheckPayableTo').val();
          var commentsS = $('#txtSponsorComments').val();
          var adSize = $('#txtSizeOfAd').val();
          var adCost = $('#txtCost').val();
          var dueDate = $('#txtDueDate').val();
          var artworkEmail = $('#txtArtworkEmail').val();
          var sendCheckTo = $('#txtSendCheckTo').val();
          var sendCheckTo2 = $('#txtSendCheckAddressLine2').val();
          var sendCheckToCity = $('#txtSendCheckCity').val();
          var sendCheckToState = $('#ddlSendCheckState').val();
          var sendCheckToZip = $('#txtSendCheckZip').val();

            $.ajax({
                url: "/Home/CreateSponsorshipRequest",
                type: 'POST',
                dataType: 'JSON',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    request: {
                        'Date': dateS,
                        'FirstName': firstNameS,
                        'LastName': lastNameS,
                        'PhoneNumber': phoneS,
                        'Address': addressS,
                        'AddressLine2': addressLine2S,
                        'City': cityS,
                        'State': stateS,
                        'Zip': zipS,
                        'PatientTreatmentStatus': statusS,
                        'Organization': organizationS,
                        'CheckPayableTo': checkpayabletoS,
                        'Address': addressS,
                        'AddressLine2': addressLine2S,
                        'City': cityS,
                        'State': stateS,
                        'Zip': zipS,
                        'Comments': commentsS,
                        'AdTypes': checkedValuesS,
                        'AdSize': adSize,
                        'AdCost': adCost,
                        'DueDate': dueDate,
                        'ArtworkEmailedTo': artworkEmail,
                        'SendCheckToAddress': sendCheckTo,
                        'SendCheckToAddress2': sendCheckTo2,
                        'SendCheckToCity': sendCheckToCity,
                        'SendCheckToState': sendCheckToState,
                        'SendCheckToZip': sendCheckToZip
                    } 
                }),
                success: function (data) {
                    window.location.href = data.url;
                },
                error: function (data) {
                    alert(data);
                }
            });
        }
    });


       $("#patient-refer").validate({    
              rules:{
                required: true,             
                txtDoctorFirstName: "required",
                txtDoctorLastName: "required",
                txtPracticeName: "required",
                txtDoctorEmail: {
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
              }, 
           

              submitHandler: function (form) {

                  var firstNameR = $('#txtDoctorFirstName').val();
                  var lastNameR = $('#txtDoctorLastName').val();
                  var practiceNameR = $('#txtPracticeName').val();
                  var emailR = $('#txtDoctorEmail').val();
                  var patientFirstNameR = $('#txtPatientFirstName').val();
                  var patientLastNameR = $('#txtPatientLastName').val();
                  var patientPhoneR = $('#txtPatientPhone').val();
                  var patientEmailR = $('#txtPatientEmail').val();
                  var radiographsSentR = $("input:radio[name ='radiograph']:checked").val();
                  var commentsR = $('#txtComments').val();

                  //alert(emailR);
                $.ajax({
                    url: "/Home/CreateDoctorReferral",
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        referral: {
                            'DoctorFirstName': firstNameR,
                            'DoctorLastName': lastNameR,
                            'PracticeName': practiceNameR,
                            'DoctorEmail': emailR,
                            'PatientFirstName': patientFirstNameR,
                            'PatientLastName': patientLastNameR,
                            'PatientPhoneNumber': patientPhoneR,
                            'PatientEmailAddress': patientEmailR,
                            'RadiographsSent': radiographsSentR,
                            'Comments': commentsR
                        }
                    }),
                    success: function (data) {
                        window.location.href = data.url;
                    },
                    error: function (data) {
                        alert(data);
                    }
                });
            }
        });


    
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
                },  
                    
                  submitHandler: function (form) {

                      var firstNameA = $('#txtFirstName').val();
                      var lastNameA = $('#txtLastName').val();
                      var dobA = $('#txtDOB').val();
                      var respPartyFirstNameA = $('#txtRespPartyFirstName').val();
                      var respPartyLastNameA = $('#txtRespPartyLastName').val();
                      var newPatientA = $('#ddlNewPatient').val();
                      var phoneA = $('#txtPhone').val();
                      var emailA = $('#txtEmail').val();
                      var modeOfContactA = $('#contactType').val();
                      var convenientTimesA = $('#txtConvenientTimes').val();
                      var howDidYouHearA = $('#txtHowDidYouHear').val();
                      var dentistNameA = $('#txtGeneralDentistName').val();
                      var commentsA = $('#txtAdditionalComments').val();

                      //for the checkboxes
                      var checkedValuesA = $('input[name="prefApptDays"]:checked').map(function () {
                          return this.value;
                      }).get();

                $.ajax({
                    url: "/Home/CreateAppointmentRequest",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        apptRequest: {
                            'FirstName': firstNameA,
                            'LastName': lastNameA,
                            'DOB': dobA,
                            'ResponsiblePartyFirstName': respPartyFirstNameA,
                            'ResponsiblePartyLastName': respPartyLastNameA,
                            'IsNewPatient': newPatientA,
                            'Phone': phoneA,
                            'Email': emailA,
                            'PreferredModeOfContact': modeOfContactA,
                            'ConvenientTimes': convenientTimesA,
                            'HowDidYouHear': howDidYouHearA,
                            'GeneralDentistName': dentistNameA,
                            'AdditionalComments': commentsA,
                            'PreferredDays': checkedValuesA
                        }
                    }),
                    success: function (data) {
                            window.location.href = data.url;
                    },
                    error: function (data) {
                        //alert(data);
                        console.log(data);
                    }
                });
                return false;
            }
        });
  
});


