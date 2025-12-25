(function ($) {
 "use strict";
 
 
				var initialSpark = 60;
			var sparkTooltip = function(event, ui) {
				var curSpark = ui.value  || initialSpark
				var sparktip = '<span class="slider-tip">' + curSpark + '</span>';
				$(this).find('.ui-slider-handle').html(sparktip);
			}			
			
			$("#slider9").slider({
				orientation: "vertical",
				range: "min",
				min: 1,
				max: 100,
				step: 1,
				value: initialSpark,
				create: sparkTooltip,
				slide: sparkTooltip				
			});
			
				
				
				$("#slider6").slider({
					orientation: "vertical",
					range: "min",
					min: 0,
					max: 100,
					value: 60,
					slide: function(event, ui) {
						$("#volume").val(ui.value);
					}
				});
				
				$("#volume").val( 
					$("#slider6").slider("value") 
				);
				
				 $("#slider7").slider({
					orientation: "vertical",
					range: true,
					values: [27, 67],
					slide: function(event, ui) {
						$("#sales").val("$" + ui.values[0] + " - $" + ui.values[1]);
					}
				});
				$("#sales").val( "$" + $("#slider7").slider("values", 0) + " - $" + $("#slider7").slider("values", 1));
 
				$("#eq > .sliderv-wrapper").each(function() {
					var value = parseInt($(this).text(), 10);
						$(this).empty().slider({
						value: value,
						range: "min",
						animate: true,
						orientation: "vertical"
					});
				});	
				
				$("#eq2 > .sliderv-wrapper").each(function() {
					var value = parseInt($(this).text(), 10);
						$(this).empty().slider({
						value: value,
						range: "min",
						animate: true,
						orientation: "vertical"
					});
				});		
				
				var initialYear = 1980;
				var yearTooltip = function(event, ui) {
					var curYear = ui.value || initialYear
					var yeartip = '<span class="slider-tip">' + curYear + '</span>';
					$(this).find('.ui-slider-handle').html(yeartip);
				}
				
				$("#slider10").slider({
					value: initialYear,
					range: "min",
					min: 1950,
					max: 2020,
					step: 1,
					create: yearTooltip,
					slide: yearTooltip
				});	
				
				
 
				$('#slider8').slider({
					range: true,
					values: [500, 1500],
					min: 10,
					max: 2000,
					step: 10,
					slide: function(event, ui) { 
						$("#budget").val("$" + ui.values[0] + " - $" + ui.values[1]);
					}			
				});
				$("#budget").val("$" + $("#slider8").slider("values", 0) + " - $" + $("#slider8").slider("values", 1));
				
				
		
				$("#slider").slider({
					range: "min",
					min: 10,
					max: 100,
					value: 80			
				});
				$("#slider1").slider({
					range: true,
					values: [17, 83]
				});	
				
				
				$("#slider3").slider({
					range: "max",
					min: 1,
					max: 10,
					value: 2,
					slide: function(event, ui) {
						$("#bedrooms").val(ui.value);
					}
				});
				
				$("#bedrooms").val( 
					$("#slider3").slider("value") 
				);
				
				

    var a = document.getElementById('OffsetAlarm').value;
    $("#slider_offset1").slider({
        range: "min",
        value: a*10,
        min: 5,
        max: 20,
        slide: function (event, ui) {
            $("#OffsetAlarm").val(ui.value / 10);
        }
    });
    $("#OffsetAlarm").val(
        $("#slider_offset1").slider("value") / 10
    );

    var b = document.getElementById('OffsetControl').value;
    $("#slider_offset2").slider({
        range: "min",
        value: b*10,
        min: 16,
        max: 30,
        slide: function (event, ui) {
            $("#OffsetControl").val(ui.value / 10);
        }
    });
    $("#OffsetControl").val(
        $("#slider_offset2").slider("value") / 10
    );

    var c = document.getElementById('StrainAlarm').value;
    $("#slider_strain1").slider({
        range: "min",
        value: c*10,
        min: 290,
        max: 760,
        slide: function (event, ui) {
            $("#StrainAlarm").val(ui.value / 10);
        }
    });
    $("#StrainAlarm").val(
        $("#slider_strain1").slider("value") / 10
    );

    var d = document.getElementById('StrainControl').value;
    $("#slider_strain2").slider({
        range: "min",
        value: d*10,
        min: 500,
        max: 1000,
        slide: function (event, ui) {
            $("#StrainControl").val(ui.value / 10);
        }
    });
    $("#StrainControl").val(
        $("#slider_strain2").slider("value") / 10
    );

    var e = document.getElementById('VibrationAlarm_YJ').value;
    $("#slider_vibration1").slider({
        range: "min",
        value: e,
        min: 1,
        max: 15,
        slide: function (event, ui) {
            $("#VibrationAlarm_YJ").val(ui.value);
        }
    });
    $("#VibrationAlarm_YJ").val(
        $("#slider_vibration1").slider("value")
    );

    var f = document.getElementById('VibrationAlarm_ZD').value;
    $("#slider_vibration2").slider({
        range: "min",
        value: f,
        min: 25,
        max: 45,
        slide: function (event, ui) {
            $("#VibrationAlarm_ZD").val(ui.value);
        }
    });
    $("#VibrationAlarm_ZD").val(
        $("#slider_vibration2").slider("value")
    );

    var g = document.getElementById('VibrationAlarm_LJ').value;
    $("#slider_vibration3").slider({
        range: "min",
        value: g,
        min: 100,
        max: 200,
        slide: function (event, ui) {
            $("#VibrationAlarm_LJ").val(ui.value);
        }
    });
    $("#VibrationAlarm_LJ").val(
        $("#slider_vibration3").slider("value")
    );

    var h = document.getElementById('StressAlarm').value;
    $("#slider_stress").slider({
        range: "min",
        value: h,
        min: 60,
        max: 90,
        slide: function (event, ui) {
            $("#StressAlarm").val(ui.value);
        }
    });
    $("#StressAlarm").val(
        $("#slider_stress").slider("value")
    );
 
})(jQuery); 