$(document).ready(function(){
	var heroHeight = $("#hero-image").height();
	var headerHeight = $("#banner").height();
	var affixStart = $(".sub-page-title").height()+headerHeight
	
		
	if ($(window).width() > 768) {
		$( ".slides li:nth-child(1)" ).remove();
	}
	$(window).resize(function() {
		if ($(window).width() > 768) {
			$( ".slides li:nth-child(1)" ).remove();
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

