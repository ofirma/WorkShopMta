var globalOldSelectedLocationId = '';
var map;
var globalSelectedLocationId = '-999';
function initialize() {
    var mapOptions = {
        zoom: 15,
        center: new google.maps.LatLng(32.08018, 34.78056)
    };
    map = new google.maps.Map(document.getElementById('map-canvas'),
    mapOptions);
}

google.maps.event.addDomListener(window, 'load', initialize);
        
angular.module('initExample', [])
  .controller('ExampleController', ['$scope', function ($scope, $http) {
      $scope.days = createDays();
  }]);

function openDay(selectedDay) {
    var selectedDayClosedDivId = 'day' + selectedDay + 'Closed';
    var selectedDayOpenedDivId = 'day' + selectedDay + 'Opened';
    $('#' + selectedDayClosedDivId).hide();
    $('#' + selectedDayOpenedDivId).show('slide', { direction: 'up' }, 300);

    $('#locationsWrapper').show('slide', { direction: 'up' }, 500);
}
function closeDay(selectedDay) {
    var selectedDayClosedDivId = 'day' + selectedDay + 'Closed';
    var selectedDayOpenedDivId = 'day' + selectedDay + 'Opened';

    $('#locationsWrapper').css('display', 'none');
    $('#' + selectedDayOpenedDivId).hide();
    $('#' + selectedDayClosedDivId).show('slide', { direction: 'up' }, 300);

    closeLocation(true);
}
function goToLocationOnMap() {
    // todo: set another pic icon instead of simple marker
    latLangParam = { x: 32.08018, y: 34.78056 };
    var latLang = new google.maps.LatLng(latLangParam.x, latLangParam.y);

    var marker = new google.maps.Marker({
        position: latLang,
        map: map,
        title: ''
    });
    map.panTo(latLang);
    map.setZoom(17);
}

function onLocationClick(selectedLocationDataStr) {
    var selectedLocationData = jQuery.parseJSON(selectedLocationDataStr);
    var selectedLocation = selectedLocationData.id;

    globalOldSelectedLocationId = globalSelectedLocationId;
    globalSelectedLocationId = selectedLocation;

    var locationDivId = 'location' + selectedLocation;

    if (selectedLocation == globalOldSelectedLocationId) {
        closeLocation(true);
        globalSelectedLocationId = '-999';
        return;
    }
    else {
        closeLocation(false);
    }

    $('#' + locationDivId).attr('class', 'locationSelected');

    goToLocationOnMap();

    var locationData = $('#locationData');

    setTimeout(function () {
        $('#mapWrapper').prepend(locationData);
        $('#locationName').attr('value', selectedLocationData.name);
        $('#picsNum').html(selectedLocationData.pics.length);
               
        if (locationData.css('display') == 'none') {
            locationData.toggle('fold', 500);
        }
    }, 300);

    $('#storyNoLocationSelected').css('display', 'none'); // todo: story tt
    $('#storyLocationSelected').css('display', 'block');
            
    setLocationPics(selectedLocationData);
}

function setLocationPics(selectedLocationData) {
    $("#tempPics").append($("#locationPicsDiv").html());
    $("#locationPicsDiv").html('');

    var myPrefix = 'location' + selectedLocationData.id + 'img';
    $("[id^=" + myPrefix + "]").each(function (index) {
        var locationPicTemp = $('#' + $(this).attr('id')).detach();
        $('#locationPicsDiv').append(locationPicTemp);
    });
    var firstImgSrc = $('#location' + selectedLocationData.id + 'img0').attr('src');
    var firstImgCaption = selectedLocationData.pics[0].caption;
    $('#selectedImg').attr('src', firstImgSrc);
    $('#picCaption').attr('value', firstImgCaption);
}

function onImageClick(img) {
    $('#selectedImg').attr('src', img.src);
    $('#picCaption').attr('value', img.title);
}
function onImageMouseOut(img) {
    var imgId = img.id;
    $('#' + imgId).css('opacity', '1');
}
function onImageMouseOver(img) {
    var imgId = img.id;
    $('#' + imgId).css('opacity', '0.7');
}
function saveLocationChanges() {

}
function saveLocationChangesAndClose() {
    saveLocationChanges();
    closeLocation(true, true);
}
function closeLocation(closeLocationData, closerBtn) {
    if (closerBtn) {
        locationDivId = 'location' + globalSelectedLocationId;
        globalSelectedLocationId = -999;
    }
    else {
        locationDivId = 'location' + globalOldSelectedLocationId;
    }

    $('#' + locationDivId).attr('class', 'location');
    $('#storyLocationSelected').css('display', 'none'); // todo: story tt
    $('#storyNoLocationSelected').css('display', 'block');

    if (closeLocationData) {
        var locationData = $('#locationData');
        locationData.toggle('fold', 500);
    }
}
function closeStory() {
    $('#storyData').toggle('size', 300);
}
function onStoryClick() {
    var storyData = $('#storyData');
    $('#mapWrapper').prepend(storyData);
    $(storyData).toggle('fold', 300);
}
function saveStoryChanges() {

}
function saveDirections() {
    $('#my-tooltip').tooltipster('hide');
}
function changePreviewSound() {
    $('#moses').html('<audio controls style="width:210px"><source id = "soundController" src="sounds/1.mp3" type="audio/mpeg"></audio>');
}
$(function () {
    $("#locationData").draggable();
    $("#storyData").draggable();

    $('#my-tooltip').tooltipster({
        content: $('#nextLocationToolTip'),
        contentAsHtml: true,
        interactive: true,
        autoClose: true,
        theme: 'my-custom-theme'
    });


    var bbb = '<span>Please choose a location first</span>';
    $('#storyNoLocationSelected').tooltipster({
        content: $(bbb),
        contentAsHtml: true,
        interactive: true,
        autoClose: true,
        theme: 'my-custom-theme'
    });

    $('#soundsTooltip').tooltipster({
        content: $('#backgroundMusicTooltip'),
        contentAsHtml: true,
        interactive: true,
        autoClose: true,
        theme: 'my-custom-theme'
    });
});