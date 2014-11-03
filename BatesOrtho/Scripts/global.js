$(document).ready(function(){
	var heroHeight = $("#hero-image").height();
	var headerHeight = $("#banner").height();
	var affixStart = $(".sub-page-title").height()+headerHeight
	
		
	if ($(window).width() > 768) {
		$( ".slides li.mobile-slide" ).remove();
	}
	$(window).resize(function() {
		if ($(window).width() > 768) {
			$( ".slides li.mobile-slide" ).remove();
		}
	});
	
	// Scroll Spy
	$('body').scrollspy({ 
		target: '#sidebar',
		offset: 160
	});

	
	$('[data-spy="scroll"]').each(function () {
	  var $spy = $(this).scrollspy('refresh')
	});

	$('#sidebar').affix({
		offset: {
		  top: affixStart + 50	    
		}
	});


	$('#banner').affix({
		offset: {
		  top: 80	    
		}
	});

	var pageTitle = $(".sub-page-title h1").text();	
	
	$('#main-nav a').filter(function() {
	    var text = $(this).text();
	    return text === pageTitle;
	}).parent().addClass('active');		
		//on link find h1 text equal to link and then add a class to this link

	$("#patient-appt").validate({	  
	  rules:{
	  	required: true, 
	  	prefApptDays: {
              required: true,
              minlength: 1
          }
	  }	  
	});	
	
	$("#patient-refer").validate({	  
	  rules:{
	  	required: true, 
	  	radiograph: {
              required: true,
              minlength: 1
          }
	  }	  
	});	
	

});

// Hide Header on on scroll down
var didScroll;
var lastScrollTop = 0;
var delta = 5;
var navbarHeight = $('#banner').outerHeight();

$(window).scroll(function(event){
    didScroll = true;
});

setInterval(function() {
    if (didScroll) {
        hasScrolled();
        didScroll = false;
    }
}, 250);

function hasScrolled() {
    var st = $(this).scrollTop();
    
    // Make sure they scroll more than delta
    if(Math.abs(lastScrollTop - st) <= delta)
        return;
    
    // If they scrolled down and are past the navbar, add class .nav-up.
    // This is necessary so you never see what is "behind" the navbar.
    if (st > lastScrollTop && st > navbarHeight){
        // Scroll Down
        $('#banner').removeClass('nav-down').addClass('nav-up');
        $('#sidebar').removeClass('nav-down');
    } else {
        // Scroll Up
        if(st + $(window).height() < $(document).height()) {
            $('#banner').removeClass('nav-up').addClass('nav-down');
            $('#sidebar').addClass('nav-down');
        }
    }
    
    lastScrollTop = st;
}

