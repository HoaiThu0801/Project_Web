window.onload = function () {
    $.ajax({
        type: "get",
        url: "/Chart_Statistic/LoadQuantityImportProduct",
        success: function (response) {
			var dataPoints = response;
			alert(dataPoints)
			var chart = new CanvasJS.Chart("chartContainer", {
				animationEnabled: true,
				title: {
					text: "Số lượng nhập hàng của mỗi cửa hàng"
				},
				subtitles: [{
					text: "from 1903 to 2015"
				}],
				axisY: {
					title: "Số lượng nhập hàng của mỗi cửa hàng",
					gridThickness: 0
				},
				data: [{
					type: "bar",
					indexLabel: "{y}",
					dataPoints: dataPoints
				}]
			});
			chart.render();	    	
        }
    })
};
