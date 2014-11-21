/// <reference path="jquery-1.10.2.intellisense.js" />
/// <reference path="jquery-1.10.2.js" />

//this file will hold all the CRUD AJAX calls and client side validation

$(document).ready(function (){


    /* Validations
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
        });*/

        /*$("#patient-refer").validate({    
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
        });  */   

        /*$("#sponsorshipRequest").validate({    
          rules:{
            required: true,
            txtFirstName: "required",
            txtLastName: "required",
            txtPhone: "required"                       
          }   
        });     */

        /*$("#contact-us").validate({    
          rules:{
            required: true, 
            firstName: "required",                       
            lastName: "required",
            txtEmail:{
                required: true,
                email: true
            },
            message: "required"
          }   
        }); */    




    //$('#btnSaveContactRequest').click(function (event) {
      //  event.preventDefault();
      
        
        //alert('test')
        var firstNameC = $('#firstName').val();
        var lastNameC = $('#lastName').val();
        var emailC = $('#txtEmail').val();
        var messageC = $('#message').val();
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
        submitHandler: function(form) {
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
                    //console.log(data.url);
                    window.location.href = data.url;
                    //alert(data.url);
                    //console.log(data.url);
                    //alert('test');
                    //window.location.href = 'Home/ThankYou';
                    //alert('test');
                   // alert(data);
                   // $('#divThankYou').text(data);
                },
                error: function (jqXHR, status, err) {
                    console.log(data);
                },
                complete: function (data) {
                    //alert('test complete');
                    //alert(data.url);
                    //window.location.href = 'Home/ThankYou';
                }
            });
        }
    });



    //$('#btnSaveSponsorshipRequest').click(function (event) {
        //event.preventDefault();
      

        var checkedValuesS = $('input[name="typeOfAd"]:checked').map(function () {
            return this.value;
        }).get();
        
        var dateS = $('#dateSponsorshipRequest').val();
        var firstNameS = $('#txtFirstName').val();
        var lastNameS = $('#txtLastName').val();
        var phoneS = $('#txtPhone').val();
        var addressS = $('#txtAddress').val();
        var addressLine2S = $('#txtAddressLine2').val();
        var cityS = $('#txtCity').val();
        var stateS = $('#ddlState').val();
        var zipS = $('#txtZip').val();
        var statusS = $('#txtPatientTreatmentStatus').val();
        var organizationS = $('#txtOrganization').val();
        var checkpayabletoS = $('#txtCheckPayableTo').val();
        var commentsS = $('#txtComments').val();
        
    $("#sponsorshipRequest").validate({    
      rules:{
        required: true,
        txtFirstName: "required",
        txtLastName: "required",
        txtPhone: "required"                       
      },
        submitHandler: function(form) {
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
                        'AdTypes': checkedValuesS
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


    //$('#btnSaveDoctorReferral').click(function (event) {
       // event.preventDefault();

        var firstNameR = $('#txtDoctorFirstName').val();
        var lastNameR = $('#txtDoctorLastName').val();
        var practiceNameR = $('#txtPracticeName').val();
        var emailR = $('#txtEmail').val();
        var patientFirstNameR = $('#txtPatientFirstName').val();
        var patientLastNameR = $('#txtPatientLastName').val();
        var patientPhoneR = $('#txtPatientPhone').val();
        var patientEmailR = $('#txtPatientEmail').val();
        var radiographsSentR = $("input:radio[name ='radiograph']:checked").val();
        var commentsR = $('#txtComments').val();

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
              }, 
           

            submitHandler: function(form) {
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


    /* $('#btnSaveAppointmentRequest').click(function() {
        
            event.preventDefault();  */
            
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
            var checkedValuesA = $('input[name="prefApptDays"]:checked').map(function (){
                return this.value;
            }).get();
        

            //var jsonarray =  JSON.stringify(checkedValues);
            //console.log(jsonarray);
            //console.log(checkedValues);
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
                    
                submitHandler: function(form) {      
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
                return false;
            }
        });
    // });

});


