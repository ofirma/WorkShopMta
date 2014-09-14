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
        
function logout(){
    $.ajax({
        type: 'GET',
        url: '/SignOut/Get'
    }).done(function (data) {
        window.location = 'main.html';
    });
}

angular.module('initExample', [])
  .controller('ExampleController', ['$scope', function ($scope, $http) {
      var allData = createDays();
      $scope.days = allData.days;

      $scope.userLoggedIn = false;
      $.ajax({
          type: 'GET',
          url: '/CheckIfUserIsLoggedIn/Get'
      }).done(function (data) {
          if (data.isLoggedIn) {
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
              console.log(data.days);
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
function goToLocationOnMap(x, y) {
    // todo: set another pic icon instead of simple marker
    latLangParam = { x: x, y: y };
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
    goToLocationOnMap(selectedLocationData.latitude, selectedLocationData.longitude);

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
    var tempRealLocationIndex = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalSelectedLocationId);

    var myLocation = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
            .scope().days[globalSelectedDayId].locations[tempRealLocationIndex];
    $.ajax({
        type: 'POST',
        url: '/api/updatelocation',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(myLocation)
    }).done(function (data) {
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
    var realLocationId = getLocationIndexInArrayByLocationId(globalSelectedDayId, globalOldSelectedLocationIdRawForJson);

    angular.element(document.querySelector('[ng-controller="ExampleController"]'))
        .scope().days[globalSelectedDayId].locations[realLocationId].story = document.getElementById('storyTextArea').value;


    var locationData = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                                                    .scope().days[globalSelectedDayId].locations[realLocationId];

    $.ajax({
        type: 'POST',
        url: '/api/updatelocationstory',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(locationData)
    }).done(function (data) {
        
    });
    closeStory();
}
function saveBackgroundMusic(selectedIndex) {
    var selectedMusicOption = document.getElementById('backgroundMusicDropDown').options[selectedIndex].value;
    var dataToSend = { "id": myVideoId, "soundId": selectedMusicOption };
    $.ajax({
        type: 'POST',
        url: '/api/updatemusicforvideo',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(dataToSend)
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

            var dropDownId = 'transportType_day' + dayIndex + 'location' + realLocationId;
            var transportTypeFromDb = myDays[dayIndex].locations[locationIndex].travelModeToNextLocation;

            transportTypeFromDb = getTransportIndexByName(transportTypeFromDb);

            $('#' + dropDownId)[0].children[transportTypeFromDb].selected = true;
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

    $('#shareVideoLinkDiv').val('/movie.html?videoId=' + myVideoId);

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

    setTimeout(function () {
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
    },3000);


    musicBackgroundFromJson = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
                   .scope().backgroundMusic;

    $('#backgroundMusicDropDown')[0].children[musicBackgroundFromJson.id + 1].selected = true;

    musicPath = angular.element(document.querySelector('[ng-controller="ExampleController"]'))
               .scope().backgroundMusic.path;


    $('#mySoundController').html('<audio controls style="width:210px"><source id = "soundController" src="' + musicPath + '" type="audio/mpeg"></audio>');


    $('#soundsTooltip').tooltipster('content', $('#backgroundMusicTooltip'));

    
};

function getTransportTypeNameByIndex(transportTypeIndex) {
    if (transportTypeIndex == 0) {
        return "WALKING";
    }
    else if (transportTypeIndex == 1) {
        return "PUBLIC TRANSPORT";
    }
    else {
        return "CAR";
    }
}

function getTransportIndexByName(transportName) {
    if (transportName == "WALKING") {
        return 0;
    }
    else if (transportName == "PUBLIC TRANSPORT") {
        return 1;
    }
    else {
        return 2;
    }
}

function saveNextDirections(day, location) {
    var dropDownId = 'transportType_day' + day + 'location' + location;   
    var transportTypeIndex = document.getElementById(dropDownId).selectedIndex;
    var transportType = getTransportTypeNameByIndex(transportTypeIndex);
    var dataToSend = { "locationId": location, "transportType": transportType };
    $.ajax({
        type: 'POST',
        url: '/api/UpdateNextLocationTransport',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(dataToSend)
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