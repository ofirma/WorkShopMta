function partial(func /*, 0..n args */) {
    var args = Array.prototype.slice.call(arguments, 1);
    return function () {
        var allArguments = args.concat(Array.prototype.slice.call(arguments));
        return func.apply(this, allArguments);
    };
}

var days = [
    {
        day: 1,
        locations: [
            { latLang: '32.08018,34.78056' },
            { latLang: '33.08018,34.78056' },
            { latLang: '35.08018,36.78056' }
        ]
    },
    {
        day: 2,
        locations: [
            { latLang: '32.08018,34.78056' },
            { latLang: '33.08018,34.78056' },
            { latLang: '35.08018,36.78056' }
        ]
    }
];

function getActions(dayNum) {

    var dayData = days[dayNum];

    var actions = [];

    var f0 = partial(daySummary, dayData.day);
    actions.push(f0);

    for (var i = 0; i < dayData.locations.length; i++) {

        var f1 = partial(showStreetView, dayData.locations[i].latLang);
        var f2 = partial(showPics, dayData.locations[i].latLang);

        actions.push(f1, f2);

        if ((i + 1) != dayData.locations.length) {
            var f3 = partial(showRouteForNextLocation, dayData.locations[i + 1].latLang);
            actions.push(f3);
        }

    }

    return actions;
}

var daySummary = function (x, callback) {
    setTimeout(function () {
        $('#div1').html('day summary: ' + x);
        callback();
    }, 1000);
};

var showDaySummaryInner = function (text) {

    setElementToScreenCenter($("#msg"));
    $("#msg").toggle('drop', { direction: "left" }, 500);

    var dest = document.getElementById("msg");
    dest.innerHTML = text;
   // setTimeout("startDay1()", 2000);
};


function setPanoToCenterScreen() {

    var winH = window.innerHeight;
    var winW = window.innerWidth;
    var left = (winW - 400) / 2;
    var top = (winH - 400) / 2;

    $('#pano').css('top', top);
    $('#pano').css('left', left);

}

function showStreetViewInnner() {
    var panoramaOptions = {
        position: myLatlng,
        pov: {
            heading: 1,
            pitch: 10
        }
    };
    panorama = new google.maps.StreetViewPanorama(document.getElementById('pano'), panoramaOptions);
    map.setStreetView(panorama);

    setPanoToCenterScreen();

    $('#pano').css('visibility', 'visible');
    $('#pano').css('display', 'none');
    $("#pano").toggle('puff', { direction: "left" }, 1000);

    var i = 0;

    var panoInterval = setInterval(function () {
        i += 1;
        var pov2 = {
            heading: i,
            pitch: 10
        };
        panorama.setPov(pov2);
        if (i == 360) {
            clearInterval(panoInterval);
            setTimeout(function () {
                $("#pano").toggle('puff', { direction: "left" }, 1000);
            }, 2000);
        }
    }, 50);
}

var showStreetView = function (x, callback) {
    setTimeout(function () {
        showStreetViewInnner();
        $('#div1').html('showing street view for location' + x);
        callback();
    }, 1000);
};

var showPics = function (x, callback) {
    setTimeout(function () {
        //showStreetView
        $('#div1').html('showing pics for location' + x);
        callback();
    }, 1000);
};

var showRouteForNextLocation = function (x, callback) {
    setTimeout(function () {
        //showStreetView
        $('#div1').html('showing route for next location' + x);
        callback();
    }, 1000);
};

var currentDay = 0;

async.whilst(
    function () { return currentDay < days.length; },
    function (callback) {
        async.series(getActions(currentDay),
            function (err, results) {
                callback();
            });
        currentDay++;
    },
    function (err) {
        $('#div1').html('done');
    }
);