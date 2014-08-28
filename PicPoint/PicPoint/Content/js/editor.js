var globalOldSelectedLocationId = '';
var map;
var globalSelectedLocationId = '-999';
var globalOldSelectedLocationIdRawForJson;
var globalSelectedImgId = 0;
var globalSelectedDayId = -999;

var myVideoId = localStorage.getItem("videoId");

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
      var allData = createDays();
      $scope.days = allData.days;

      $scope.userLoggedIn = false;
      $.ajax({
          type: 'GET',
          url: 'http://www.json-generator.com/api/json/get/cjVGeQsaKW?indent=2'
      }).done(function (data) {
          if (data.loggedIn) {
              $scope.username = data.username;
              $('#helperForFoucs2').focus();
              $('#helperForFoucs').focus();
              $scope.userLoggedIn = true;
          }
      });

      $scope.getRealDaysFromServer = function () {
          $.ajax({
              type: 'GET',
              url: '/video/getdays/?id=' + myVideoId
          }).done(function (data) {
              $scope.days = data.days;
              $scope.musicOptions = data.musicOptions;
              $scope.backgroundMusic = data.backgroundMusic;
              $scope.$apply();
              runAfterDataFetchFromServer();
          });
      }

      $scope.getRealDaysFromServer();
  }]);

function openDay(selectedDay) {
    if (globalSelectedDayId != -999) {
        closeDay(globalSelectedDayId);
    }

    globalSelectedDayId = selectedDay;
    var selectedDayClosedDivId = 'day' + selectedDay + 'Closed';
    var selectedDayOpenedDivId = 'day' + selectedDay + 'Opened';
    $('#' + selectedDayClosedDivId).hide();
    $('#' + selectedDayOpenedDivId).show('slide', { direction: 'up' }, 300);

    $('#locationsWrapperForDay' + selectedDay).show('slide', { direction: 'up' }, 500);
}

function closeDay(selectedDay) {
    var selectedDayClosedDivId = 'day' + selectedDay + 'Closed';
    var selectedDayOpenedDivId = 'day' + selectedDay + 'Opened';

    $('#locationsWrapperForDay' + selectedDay).css('display', 'none');
    $('#' + selectedDayOpenedDivId).hide();
    $('#' + selectedDayClosedDivId).show('slide', { direction: 'up' }, 300);

    if (globalSelectedLocationId != -999) {
        changeLocationButtonToClosed(globalSelectedLocationId);
        closeLocation(true);
    }
    
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
function changeLocationButtonToSelected(locationId) {
    $('#location' + locationId).attr('class', 'locationSelected');
}
function closeLocationFromXButton() {
    closeLocation(globalSelectedLocationId);
    globalSelectedLocationId = '-999';
}
function onLocationClick(selectedLocationDataStr) {
    var selectedLocationData = jQuery.parseJSON(selectedLocationDataStr);
    var selectedLocation = selectedLocationData.id;

    globalOldSelectedLocationId = globalSelectedLocationId;
    globalSelectedLocationId = selectedLocation;
    globalOldSelectedLocationIdRawForJson = selectedLocationData.id;
    var locationDivId = 'location' + selectedLocation;

    if (selectedLocation == globalOldSelectedLocationId) {
        closeLocation(selectedLocation);
        
        globalSelectedLocationId = '-999';
        return;
    }
    else {
        if (globalOldSelectedLocationId != -999) {
            changeLocationButtonToClosed(globalOldSelectedLocationId);
            $('#storyData').hide('fold', 300);
        }
    }

    changeLocationButtonToSelected(selectedLocation);


    goToLocationOnMap();

    var locationData = $('#locationData');

    setTimeout(function () {
        $('#mapWrapper').prepend(locationData);

        var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

        $('#locationName').val(angular.element(document.querySelector('[ng-controller="ExampleController"]'))
            .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].name);
        $('#picsNum').html(selectedLocationData.pics.length);

        document.getElementById('showStreetViewCheckBox').checked =angular.element(document.querySelector('[ng-controller="ExampleController"]'))
            .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].showStreetView;
               
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

    var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

    var firstImgCaption = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                                .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].pics[0].caption;
    $('#selectedImg').attr('src', firstImgSrc);
    document.getElementById('picCaption').value = firstImgCaption;
    $('#picCaptionView').text(firstImgCaption);
}

function onImageClick(img) {
    globalSelectedImgId = img.id.split('img')[1];

    $('#selectedImg').attr('src', img.src);
    //$('#picCaption').attr('value', img.title);

    var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

    document.getElementById('picCaption').value = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                                                    .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].pics[globalSelectedImgId].caption;
    $('#picCaptionView').text(angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                                                    .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].pics[globalSelectedImgId].caption);

    
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
    var v = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
            .scope().days;
    $.ajax({
        type: 'POST',
        url: 'http://jsonstub.com/saveLocationChanges',
        data: { "videoId": myVideoId, "days": v },
        beforeSend: function (request) {
            request.setRequestHeader('JsonStub-User-Key', '589902c6-c52d-4a38-b0e9-7ae438abe8ce');
            request.setRequestHeader('JsonStub-Project-Key', 'd1134f6f-6ada-46c1-9855-f35ae2cbcfe8');
        }
    }).done(function (data) {
        //done
    });
}
function saveLocationChangesAndClose() {
    saveLocationChanges();
    closeLocationFromXButton();
}
function changeLocationButtonToClosed(locationId) {
    locationDivId = 'location' + locationId;
    $('#' + locationDivId).attr('class', 'location');
}
function closeLocation(locationId) {
    $('#storyData').hide('fold', 300);
    changeLocationButtonToClosed(locationId);
    var locationData = $('#locationData');
    locationData.hide('fold', 500);
    globalSelectedLocationId = -999;
    globalOldSelectedLocationId = -999;
}
function closeStory() {
    $('#storyData').toggle('size', 300);
}
function onStoryClick() {

    var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

    document.getElementById('storyTextArea').value = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                                                    .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].story;
    var storyData = $('#storyData');
    $('#mapWrapper').prepend(storyData);
    $(storyData).toggle('fold', 300);
}
function saveStoryChanges() {
    var myStory = document.getElementById('storyTextArea').value;
    $.ajax({
        type: 'POST',
        data: { "videoId": myVideoId, "day": globalSelectedDayId, "location": globalOldSelectedLocationIdRawForJson, "story": myStory },
        url: 'http://jsonstub.com/api/saveStoryChanges',
        beforeSend: function (request) {
            request.setRequestHeader('JsonStub-User-Key', '589902c6-c52d-4a38-b0e9-7ae438abe8ce');
            request.setRequestHeader('JsonStub-Project-Key', 'd1134f6f-6ada-46c1-9855-f35ae2cbcfe8');
        }
    }).done(function (data) {

        var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

        angular.element(document.querySelector('[ng-controller="ExampleController"]'))
            .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].story = document.getElementById('storyTextArea').value;
    });
    closeStory();
}
function saveBackgroundMusic(selectedIndex) {
    var selectedMusicOption = document.getElementById('backgroundMusicDropDown').options[selectedIndex].value;

    $.ajax({
        type: 'POST',
        data: { "videoId": myVideoId, "musicOption": selectedMusicOption },
        url: 'http://jsonstub.com/api/saveBackgroundMusic',
        beforeSend: function (request) {
            request.setRequestHeader('JsonStub-User-Key', '589902c6-c52d-4a38-b0e9-7ae438abe8ce');
            request.setRequestHeader('JsonStub-Project-Key', 'd1134f6f-6ada-46c1-9855-f35ae2cbcfe8');
        }
    }).done(function (data) {
        
    });

    
    setTimeout(function () {
        $('#soundsTooltip').tooltipster('content', $('#backgroundMusicTooltip'));
    },500);
    
}
function changePreviewSound(selectedIndex) {
    selectedIndex = selectedIndex - 1;
    var musicPath = "";
    if (selectedIndex == -1) {
        musicPath = "";
    }
    else {
        musicPath = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                   .scope().musicOptions[selectedIndex].path;
    }

    $('#mySoundController').html('<audio controls style="width:210px"><source id = "soundController" src="' + musicPath + '" type="audio/mpeg"></audio>');

    var myTempTP = $('#backgroundMusicTooltip');
    $('#soundsTooltip').tooltipster('content', myTempTP);
}

function runAfterDataFetchFromServer () {
    $("#locationData").draggable();
    $("#storyData").draggable();

    // link tooltips of next directions
    var myDays = angular.element(document.querySelector('[ng-controller="ExampleController"]')).scope().days;
    for (var dayIndex = 0; dayIndex < myDays.length; dayIndex++) {
        var currentDay = myDays[dayIndex];
        for (var locationIndex = 0; locationIndex < currentDay.locations.length; locationIndex++) {

            var realLocationId = currentDay.locations[locationIndex].id;

            var toolTipPlaceHolder = "day" + dayIndex + "location" + realLocationId + "directionsTooltipIcon";

            var toolTip = "day" + dayIndex + "location" + realLocationId + "directionsTooltip";
            $('#' + toolTipPlaceHolder).tooltipster({
                content: $('#' + toolTip),
                contentAsHtml: true,
                contentCloning: false,
                interactive: true,
                autoClose: true,
                theme: 'my-custom-theme'
            });

        }
    }

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
        contentCloning: false,
        interactive: true,
        autoClose: true,
        theme: 'my-custom-theme'
    });

    $('#showStreetViewToolTipHolder').tooltipster({
        content: $('#showStreetViewToolTip'),
        contentAsHtml: true,
        interactive: true,
        autoClose: true,
        theme: 'my-custom-theme'
    });

    $('#shareVideoToolTipHolder').tooltipster({
        content: $('#shareVideoToolTip'),
        contentAsHtml: true,
        interactive: true,
        autoClose: true,
        theme: 'my-custom-theme'
    });

    $('#locationStoryToolTipHolder').tooltipster({
        content: $('#locationStoryToolTip'),
        contentAsHtml: true,
        interactive: true,
        autoClose: true,
        theme: 'my-custom-theme'
    });

    $('#userOptions').tooltipster({
        offsetY: -15,
        arrow: false,
        content: $('#userOptionsToolTip'),
        contentAsHtml: true,
        interactive: true,
        autoClose: true,
        theme: 'my-custom-themeNoBorder',
        minWidth: 130,
        trigger: 'click'
    });

    musicBackgroundFromJson = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                   .scope().backgroundMusic;

    $('#backgroundMusicDropDown')[0].children[musicBackgroundFromJson.id + 1].selected = true;

    musicPath = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
               .scope().backgroundMusic.path;


    $('#mySoundController').html('<audio controls style="width:210px"><source id = "soundController" src="' + musicPath + '" type="audio/mpeg"></audio>');


    $('#soundsTooltip').tooltipster('content', $('#backgroundMusicTooltip'));
};

function saveNextDirections(day,location,transportType) {
    $.ajax({
        type: 'POST',
        data: { "videoId": myVideoId, "day": day, "location": location, "transportType": transportType },
        url: 'http://jsonstub.com/api/saveNextLocationTransportType',
        beforeSend: function (request) {
            request.setRequestHeader('JsonStub-User-Key', '589902c6-c52d-4a38-b0e9-7ae438abe8ce');
            request.setRequestHeader('JsonStub-Project-Key', 'd1134f6f-6ada-46c1-9855-f35ae2cbcfe8');
            
        }
    }).done(function (data) {
        var toolTipPlaceHolder = "day" + day + "location" + location + "directionsTooltipIcon";
        var toolTip = "day" + day + "location" + location + "directionsTooltip";
            setTimeout(function () {
                $('#' + toolTipPlaceHolder).tooltipster('content', $('#' + toolTip));
            }, 500);
    });
}

function picCatpionManualChange() {

    var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

    $('#picCaptionView').text($('#picCaption').val());

    angular.element(document.querySelector('[ng-controller="ExampleController"]'))
        .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].pics[globalSelectedImgId].caption = $('#picCaption').val();

}

function locationNameChanged() {

    var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

    angular.element(document.querySelector('[ng-controller="ExampleController"]'))
        .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].name = $('#locationName').val();
}

function changeShowStreetView() {

    var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

    angular.element(document.querySelector('[ng-controller="ExampleController"]'))
            .scope().days[globalSelectedDayId].locations[tempRealLocationIndex].showStreetView = document.getElementById('showStreetViewCheckBox').checked;
     
}

function getLocationIndexInArrayByLocationId(dayItemIndex, locationId) {
    var tempArr = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                .scope().days[dayItemIndex].locations;
    for (var i = 0; i < tempArr.length; i++) {
        if (tempArr[i].id == locationId) {
            return i;
        }
    }
}

function onPlayVideoClick() {
    window.open('movie.html?videoId='+myVideoId);
}