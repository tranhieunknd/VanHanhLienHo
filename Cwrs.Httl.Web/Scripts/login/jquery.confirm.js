(function($){
	
	$.confirm = function(params){
		
		if($('#confirmOverlay').length){
			// A confirm is already shown on the page:
			return false;
		}
		
		var buttonHTML = '';
		//var arrListName = params.name.split(",");
		var i = 0;
		$.each(params.buttons,function(name,obj){
			
			// Generating the markup for the buttons:
			
		    buttonHTML += '<a href="#" class="button ' + obj['class'] + '">' + name + '<span></span></a>';
		    i = i + 1;
			if(!obj.action){
				obj.action = function(){};
			}
		});
		
		var markup = [
			'<div id="confirmOverlay">',
			'<div id="confirmBox">',
            '<i class="icon-', params.type, '"></i>',
			'<h1>',params.title,'</h1>',
			'<p>',params.message,'</p>',
			'<div id="confirmButtons">',
			buttonHTML,
			'</div></div></div>'
		].join('');
		
		$(markup).hide().appendTo('body').fadeIn();
		
		var buttons = $('#confirmBox .button'),
			i = 0;

		$.each(params.buttons,function(name,obj){
			buttons.eq(i++).click(function(){
				
				// Calling the action attribute when a
				// click occurs, and hiding the confirm.
				
				obj.action();
				$.confirm.hide();
				return false;
			});
		});
	}

	$.confirm.hide = function(){
		$('#confirmOverlay').fadeOut(function(){
			$(this).remove();
		});
	}

	alertInfor = function (type, title, message) {	    
	    $.confirm({
	        //'name': listbtnname,
	        'type': type,
	        'title': title,
	        'message': message,
	        'buttons': {
	            'Đồng ý': {
	                'class': 'blue',
	                'action': function () {
	                }
	            }
	        }
	    });
	};

	alertCongTrinhInfor = function (message, itemfoscus) {
	    $.confirm({	 

	        'type': 'warning',
	        'title': 'Thông báo',
	        'message': message,
	        'buttons': {
	            'Đồng ý': {
	                'class': 'blue',
	                'action': function () {
	                    if (itemfoscus != '') {
	                        $("#" + itemfoscus).focus();
	                        $("#" + itemfoscus).select();
	                    }
	                }
	            }
	        }
	    });
	};

})(jQuery);