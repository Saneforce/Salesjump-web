$(document).ready(function(){ 
	/*footer testimonial script code start here*/
	$('#carousel_foot').owlCarousel({
		items: 1,
		autoplay: 3000,
		nav: false,
		loop:true,
		pagination: true
	});	
	/*footer testimonial script code end here*/	
	
	/*slideshow script code start here*/
	$('#slideshow').owlCarousel({
		items: 1,
		autoplay: 3000,
		nav : true,
		loop:true,
		pagination : false,
		navText: ['<i class="fa fa-angle-left fa-4x" aria-hidden="true"></i>','<i class="fa fa-angle-right fa-4x" aria-hidden="true"></i>']
	});
	/*slideshow script code end here*/
	
	/*testimonial script code start here*/
	$('#carousel_test').owlCarousel({
		items: 1,
		autoplay: 3000,
		nav: true,
		loop:true,
		navText: ['<i class="fa fa-chevron-left fa-2x"></i>', '<i class="fa fa-chevron-right fa-2x"></i>'],
		pagination: true
	});
	/*testimonial script code end here*/

});