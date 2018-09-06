﻿$.blockUI.defaults.css.border = 'none';
$.blockUI.defaults.css.backgroundColor = 'none';

//
// Create (update) a scope object based on the series data.
//
function scopeFromSeries(jsonData, $filter) {
    var chartData = jsonData;

    chartData.$total = 0;

    chartData.voteData.forEach(function (item) {
        item.value = parseInt(item.value);
        chartData.$total += item.value;
    });

    chartData.voteData.forEach(function (item, index) {
        item.percent = Math.round(((item.value / chartData.$total) * 100));
        item.pct = item.percent + "%";
        item.value = $filter('number')(item.value);
        item.isLast = index == chartData.voteData.length - 1;
    });

    return chartData;
}