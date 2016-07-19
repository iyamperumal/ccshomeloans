	var $googlemap_latitude 	= -37.812344,
		$googlemap_longitude	= 144.968900,
		$googlemap_zoom			= 13;



/**	@Login Drop Down ccsHL1
*************************************************** **/
	// window click, no action - stop here
	jQuery("#loginDD").bind("click", function() {
		
	});

	// window click, no action - stop here
	jQuery("#userDD").bind("click", function () {

	});

	// window click, no action - stop here
	jQuery("#registerDD").bind("click", function () {

	});

	// login click
	jQuery("#loginBtn").bind("click", function() {
		if(!jQuery("#loginDD").is(":visible")) {
			_loginShow();
		}
	});

	// user click
	jQuery("#userBtn").bind("click", function () {
	    if (!jQuery("#userDD").is(":visible")) {
	        _userShow();
	    }
	});

	// register click
	jQuery("#registerBtn").bind("click", function () {
	    if (!jQuery("#registerDD").is(":visible")) {
	        _registerShow();
	    }
	});

	// login show
	function _loginShow() {
		jQuery("#loginDD").fadeIn(600);
		jQuery('#loginBtn').append('<span id="globalOverlay" class="overlay global fixed"></span>');
		jQuery('#loginBtn').removeClass('pointer');
		_overlayClick();
	}

	// login show
	function _userShow() {
	    jQuery("#userDD").fadeIn(600);
	    jQuery('#userBtn').append('<span id="globalOverlay" class="overlay global fixed"></span>');
	    jQuery('#userBtn').removeClass('pointer');
	    _overlayClick();
	}

	// Register show
	function _registerShow() {
	    jQuery("#registerDD").fadeIn(600);
	    jQuery('#registerBtn').append('<span id="globalOverlay" class="overlay global fixed"></span>');
	    jQuery('#registerBtn').removeClass('pointer');
	    _overlayClick();
	}

	// login close
	function _loginClose() {
	    jQuery("#loginDD").fadeOut(300);
		//jQuery("#userDD").fadeOut(300);
	    jQuery('#globalOverlay').fadeOut(300).remove();
	    jQuery('#loginBtn').addClass('pointer');
		//jQuery('#userBtn').addClass('pointer');
	}

	// login close
	function _userClose() {
	    jQuery("#loginDD").fadeOut(300);
	    jQuery('#globalOverlay').fadeOut(300).remove();
	    jQuery('#loginBtn').addClass('pointer');
	}

	// login close
	function _registerClose() {
	    jQuery("#registerDD").fadeOut(300);
	    jQuery('#globalOverlay').fadeOut(300).remove();
	    jQuery('#registerBtn').addClass('pointer');
	}

	/**
		@OVERLAY CLICK
	**/
	function _overlayClick() {
		jQuery("#globalOverlay").bind("click", function() {
		    _loginClose();
		    _registerClose();
		    _userClose();
		});
	}
	/**
		@ESC KEY PRESS
	**/
	jQuery(document).keydown(function(e){
		var code = e.keyCode ? e.keyCode : e.which;

		if (code == 27){
		    _loginClose();
		    _registerClose();
		}
	});


/**	@MENU ON TOP
*************************************************** **/
	 /*
		window.isOnTop = avoid bad actions on each scroll
		Benefits: no unseen jquery actions, faster rendering
	 */ window.isOnTop 		= true;
	jQuery(window).scroll(function() {
		if(jQuery(document).scrollTop() > 52) {
			if(window.isOnTop === true) {
				if(jQuery(window).width() > 768) {
					jQuery('#header .logo').addClass('scrolled');
					jQuery('#header img.logo_pc').hide();
					jQuery('#header img.logo_mobile').show();
				}

				jQuery('#header').stop().animate({"top":"-52px"}, 300);
				window.isOnTop = false;
			}
		} else {
			if(window.isOnTop === false) {
				if(jQuery(window).width() > 768) {
					jQuery('#header .logo').removeClass('scrolled');
					jQuery('#header img.logo_pc').show();
					jQuery('#header img.logo_mobile').hide();
				}

				jQuery('#header').stop().animate({"top":"0"}, 300);
				window.isOnTop = true;
			}
		}
	});
			


/**	@Scroll Top
*************************************************** **/
	jQuery("a.toTop").bind("click", function(e) {
		e.preventDefault();
		jQuery('html,body').animate({scrollTop: 0}, 1000, 'easeInOutExpo');
	});


/**	@Circles
*************************************************** **/
		var colors = [
			['#D3B6C6', '#4B253A'], ['#FCE6A4', '#EFB917'], ['#BEE3F7', '#45AEEA'], ['#F8F9B6', '#D2D558'], ['#F4BCBF', '#D43A43']
		];

		jQuery("#canvas .circle").each(function() {
			var percentage 	= jQuery(this).attr('data-percentage'),
				color_bg 	= jQuery(this).attr('data-colorbg'),
				color_line 	= jQuery(this).attr('data-colorline');

			Circles.create({
				id:         jQuery(this).attr('id'),
				percentage: percentage,
				radius:     100,
				width:      8,
				number:   	percentage,
				text:       '',
				colors:     [color_bg, color_line]
			});

		});



/**	@CountTo (number animate)
*************************************************** **/
		if (jQuery().appear) {
		    if (jQuery().countTo) {
		        jQuery('.countTo').appear(function () {
		            jQuery(this).each(function () {
		                var $to = jQuery(this).html();
		                jQuery(this).countTo({
		                    from: 0,
		                    to: $to,
		                    speed: 4000,
		                    refreshInterval: 40
		                });
		            });
		        });
		    }
		};



/**	@Mobile Menu
*************************************************** **/
jQuery("#mobileMenu").bind("click", function() {
	if(jQuery("header nav").is(':visible')) {
		jQuery("header nav").hide();
	} else {
		jQuery("header nav").show();
	}
	return false;
});

// bootstrap bug on resize back to full
jQuery(window).resize(function() {
	if(!jQuery("header nav").is(':visible') && jQuery(window).width() > 960) {
		jQuery("header nav").show();
	}
});



	
/**	@FITVIDS
*************************************************** **/
	if (jQuery().fitVids) {
		$("body").fitVids();
	}



	
/**	@PRETTYPHOTO
*************************************************** **/
	if (jQuery().prettyPhoto) {
		jQuery('a[data-photo^="prettyPhoto"]').prettyPhoto({
			deeplinking:				false,
			slideshow: 					5000,
			autoplay_slideshow: 		false,
			animationSpeed: 			'fast', 			/* fast/slow/normal */
			padding: 					40, 				/* padding for each side of the picture */
			opacity: 					0.75, 				/* Value betwee 0 and 1 */
			showTitle: 					true, 				/* true/false */
			allowresize: 				true, 				/* true/false */
			counter_separator_label: 	'/', 				/* The separator for the gallery counter 1 "of" 2 */
			// theme: 						'default', 			/* default / facebook / light_rounded / dark_rounded / light_square / dark_square */
			hideflash: 					false, 				/* Hides all the flash object on a page, set to TRUE if flash appears over prettyPhoto */
			modal: 						false, 				/* If set to true, only the close button will close the window */
			changepicturecallback: 		function(){}, 		/* Called everytime an item is shown/changed */
			callback: 					function(){} 		/* Called when prettyPhoto is closed */
		});
	}





/**	@ELEMENTS ANIMATION
*************************************************** **/
	jQuery('.animate_from_top').each(function () {
		jQuery(this).appear(function() {
			jQuery(this).delay(150).animate({opacity:1,top:"0px"},1000);
		});	
	});

	jQuery('.animate_from_bottom').each(function () {
		jQuery(this).appear(function() {
			jQuery(this).delay(150).animate({opacity:1,bottom:"0px"},1000);
		});	
	});


	jQuery('.animate_from_left').each(function () {
		jQuery(this).appear(function() {
			jQuery(this).delay(150).animate({opacity:1,left:"0px"},1000);
		});	
	});


	jQuery('.animate_from_right').each(function () {
		jQuery(this).appear(function() {
			jQuery(this).delay(150).animate({opacity:1,right:"0px"},1000);
		});	
	});

	jQuery('.animate_fade_in').each(function () {
		jQuery(this).appear(function() {
			jQuery(this).delay(350).animate({opacity:1,right:"0px"},1000);
		});	
	});




/**	@Cookies
*************************************************** **/
	function addCookie(name,value,days) {
		if (days) {
			var date = new Date();
			date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
			var expires = "; expires=" + date.toGMTString();
		}
		else var expires = "";
		document.cookie = name + "="+value + expires + "; path=/";
	}

	function readCookie(name) {
		var nameEQ = name + "=";
		var ca = document.cookie.split(';');
		for(var i=0;i < ca.length;i++) {
			var c = ca[i];
			while (c.charAt(0)==' ') c = c.substring(1,c.length);
			if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length,c.length);
		}
		return null;
	}

	function delCookie(name) {
		addCookie(name,"",-1);
	}


	/**	@Google Map
	*************************************************** **/
	function contactMap() {
		var latLang = new google.maps.LatLng($googlemap_latitude,$googlemap_longitude);

		var mapOptions = {
			zoom:$googlemap_zoom,
			center: latLang,
			disableDefaultUI: false,
			navigationControl: false,
			mapTypeControl: false,
			scrollwheel: false,
			mapTypeId: google.maps.MapTypeId.ROADMAP
		};

		var map = new google.maps.Map(document.getElementById('gmap'), mapOptions);
		google.maps.event.trigger(map, 'resize');
		map.setZoom( map.getZoom() );

		var marker = new google.maps.Marker({
			icon: 'data:image/png;base64,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',
			position: latLang,
			map: map,
			title: ""
		});

		marker.setMap(map);
		google.maps.event.addListener(marker, "click", function() {
			// Add optionally an action for when the marker is clicked
		});

		// kepp googlemap responsive - center on resize
		google.maps.event.addDomListener(window, 'resize', function() {
			map.setCenter(latLang);
		});

	}

	
	function showMap(initWhat) {
		var script 		= document.createElement('script');
		script.type 	= 'text/javascript';
		script.src 		= 'https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=true&callback='+initWhat;
		document.body.appendChild(script);
	}

	
	// INIT CONTACT, NLY IF #contactMap EXISRS
	if(jQuery("#gmap").length > 0) {
		showMap('contactMap');
	}


/**	@Checkbox Style
*************************************************** **/
	function setupLabel() {

		if (jQuery('.label_check input').length) {

			jQuery('.label_check').each(function(){ 
				jQuery(this).removeClass('c_on');
			});

			jQuery('.label_check input:checked').each(function(){ 
				jQuery(this).parent('label').addClass('c_on');
			});

		}

		if (jQuery('.label_radio input').length) {

			jQuery('.label_radio').each(function(){ 
				jQuery(this).removeClass('r_on');
			});

			jQuery('.label_radio input:checked').each(function(){ 
				jQuery(this).parent('label').addClass('r_on');
			});

		}

	}

	jQuery(document).ready(function(){
		jQuery('body').addClass('has-js');
		jQuery('.label_check, .label_radio').click(function(){
		setupLabel();
		}); setupLabel(); 
	});



/**	@TOP SLIDER
*************************************************** **/
	// Else, video will not work - width problems (because of display:table-cell;)
	if (jQuery("#slider .video").length > 0) {

		function _slider_width() {
			_slWidth = jQuery("#slider").width();
			jQuery("#slider .image-caption .inner").width(_slWidth);
		}	_slider_width(); // on load

		// on resize
		jQuery(window).resize(function() {
			_slider_width();
		});

	}

	function fullSlider(sliderContainer) {

		if(jQuery(sliderContainer + " .fullSlider").length) {
			jQuery(sliderContainer + " .fullSlider").maximage({
				cycleOptions: {
					fx: 		'fade',
					speed: 		1000,
					timeout: 	4000,
					prev: 		sliderContainer + ' .sliderPrev',
					next: 		sliderContainer + ' .sliderNext',
					pause: 		1,

					before: function(last,current){
						jQuery('.image-caption').fadeOut().animate({top:'100px'},{queue:false, easing: 'easeOutQuad', duration: 550});
						jQuery('.image-caption').fadeOut().animate({top:'-100px'});
					},

					after: function(last,current){
						jQuery('.image-caption').fadeIn().animate({top:'0'},{queue:false, easing: 'easeOutQuad', duration: 450});
					}	
				},

				onFirstImageLoaded: function(){
					jQuery(sliderContainer + ' .imgLoader').delay(800).hide();
					jQuery('.fullSlider').delay(800).fadeIn('slow');
					jQuery('.image-caption').fadeIn().animate({top:'0'});		
				}
			});

			// Fill and Center HTML5 Videos
			jQuery('video,object').maximage('maxcover');
		}

		// no click
		jQuery(sliderContainer + ' .sliderPrev , ' + sliderContainer + ' .sliderNext').bind("click", function(e) {
			e.preventDefault();
		});

	}	fullSlider("#slider");



/**	@Facebook
*************************************************** **/
	/*
		https://developers.facebook.com/docs/plugins/like-button/

		ADD TO YOUR CODE (just change data-href, that's all):

		<div class="fb-like" data-href="https://developers.facebook.com/docs/plugins/" data-layout="button_count" data-action="like" data-show-faces="false" data-share="false"></div>
	*/
	if(jQuery("div.fb-like").length > 0) {

		jQuery('body').append('<div id="fb-root"></div>');

		(function(d, s, id) {
			var js, fjs = d.getElementsByTagName(s)[0];
			if (d.getElementById(id)) { return; }
			js = d.createElement(s); js.id = id;
			js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
			fjs.parentNode.insertBefore(js, fjs);
		}(document, 'script', 'facebook-jssdk'));

	}

/**	@Google Plus
*************************************************** **/
	/*
		https://developers.google.com/+/web/+1button/

		<!-- Place this tag where you want the +1 button to render. -->
		<div class="g-plusone" data-size="medium" data-annotation="inline" data-width="300"></div>
	*/
	if (jQuery("div.g-plusone").length > 0) {

		(function() {
			var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
			po.src = 'https://apis.google.com/js/platform.js';
			var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
		})();

	}

/**	@Twitter
*************************************************** **/
	/*
		https://dev.twitter.com/docs/tweet-button

		<!-- Place this tag where you want the twitter button to render. -->
		<a href="https://twitter.com/share" class="twitter-share-button" data-lang="en">Tweet</a>
	*/
	if (jQuery("a.twitter-share-button").length > 0) {

		!function(d,s,id){
			var js,fjs=d.getElementsByTagName(s)[0];
			if (!d.getElementById(id)){js = d.createElement(s);
			js.id = id;js.src="https://platform.twitter.com/widgets.js";
			fjs.parentNode.insertBefore(js,fjs);}
		}(document,"script","twitter-wjs");

	}