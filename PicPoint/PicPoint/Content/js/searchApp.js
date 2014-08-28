var app = angular.module('app', ['autocomplete']);

$(function () {
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
});

// the service that retrieves some movie title from an url
app.factory('MovieRetriever', function ($http, $q, $timeout) {
    var MovieRetriever = new Object();

    MovieRetriever.getmovies = function (i) {
        var moviedata = $q.defer();
        var movies;

        $http({
            url: '/GetVideosByFilter/GetVideosOfUser/?filter=' + i,
            method: 'GET'
        }).success(function (data, status, headers, config) {
            movies = parseVideosDataToNamesOnly(data);
        });



        $timeout(function () {
            moviedata.resolve(movies);
        }, 1000);

        return moviedata.promise
    }

    return MovieRetriever;
});

function parseVideosDataToNamesOnly(data) {
    var names = [];
    for (var i = 0; i < data.length; i++) {
        names.push(data[i].name);
    }
    return names;
}

app.controller('MyCtrl', function ($scope, MovieRetriever) {

    $scope.movies = MovieRetriever.getmovies("");
    $scope.movies.then(function (data) {
        $scope.movies = data;
    });

    $scope.getmovies = function () {
        return $scope.movies;
    }

    $scope.doSomething = function (typedthings) {
        $scope.newmovies = MovieRetriever.getmovies(typedthings);
        $scope.newmovies.then(function (data) {
            $scope.movies = data;
        });
    }

    $scope.doSomethingElse = function (suggestion) {
    }

});

function changeVideos(suggestion) {
    clickOnVideosToWatchOptionNativJs();
    var videosToSearch = suggestion;
    if (videosToSearch == "") {
        videosToSearch = $('#videoSearchInput').val();
    }

    //if (videosToSearch == "") {
    //    return;
    //}

    $('#helperForFoucs2').focus();

    $('#videosResultsTitle').text('Showing results for: ' + videosToSearch);
    angular.element(document.querySelector('[ng-controller="videosController"]')).scope().videosLoading = true;
    angular.element(document.querySelector('[ng-controller="videosController"]')).scope().videos = [];



    $.ajax({
        type: 'GET',
        url: '/GetVideosByFilter/GetVideosOfUser/?filter=' + videosToSearch,
    }).done(function (data) {
        angular.element(document.querySelector('[ng-controller="videosController"]')).scope().videosLoading = false;
        angular.element(document.querySelector('[ng-controller="videosController"]')).scope().videos = data;
        $('#helperForFoucs').focus();
    });

}

app.controller('myUser', function ($scope, $http) {
    $scope.userLoggedIn = false;

    $scope.checkUserLoggedInBool = function () {
        return $scope.userLoggedIn;
    }

    $scope.checkIfUserIsLoggedIn = function () {
        var result;
        $.ajax({
            type: 'GET',
            url: '/CheckIfUserIsLoggedIn/Get'
        }).done(function (data) {
            if (data.isLoggedIn) {
                result = true;
                $scope.userLoggedIn = true;
                $scope.username = data.username;
            }
            else {
                $scope.username = '';
                result = false;
            }
            return result;
        });
    }

    $scope.checkIfUserIsLoggedIn();

    $scope.logOut = function () {
        $.ajax({
            type: 'GET',
            url: '/SignOut/Get'
        }).done(function (data) {
            $scope.checkIfUserIsLoggedIn();
            location.reload();
        });
    }

    $scope.getUsername = function () {
        return $scope.username;
    }

    $scope.signIn = function () {
        var username = $('#userid').val();
        var password = $('#passwordinput').val();
        if (username == "" || password == "") {
            return;
        }

        $.ajax({
            type: 'GET',
            url: '/login/get/?username=' + username + '&password=' + password
        }).done(function (data) {
            if (data == 'True') {
                $('#signnedInMsg').css('display', 'block');

                $('#signInForm').hide();
                $('#regsiterOption').hide();
                $scope.userLoggedIn = true;
                location.reload();
            }
            else {
                alert('Username or password are incorrect.\n');
            }
        });
    }

});

function clickOnTripVideosOptionNativeJs() {
    angular.element(document.querySelector('[ng-controller="videosController"]')).scope().clickOnTripVideosOption();
    angular.element(document.querySelector('[ng-controller="videosController"]')).scope().$apply();
    $('#userOptions').tooltipster('hide');
}

function deleteVideoConfirm(videoId) {
    var x;
    if (confirm("Clicking OK will delete the video permenantly") == true) {
        deleteVideo(videoId);
    }
}

function deleteVideo(videoId) {
    $.ajax({
        type: 'GET',
        url: '/deletevideo/deletevideo/?id=' + videoId,
    }).done(function (data) {
        var videoIndex = getVideoIndexInArrayByVideoId(videoId);
        angular.element(document.querySelector('[ng-controller="videosController"]')).scope().userVideos.splice(videoIndex, 1);
        angular.element(document.querySelector('[ng-controller="videosController"]')).scope().$apply();
    });
}

function getVideoIndexInArrayByVideoId(videoId) {
    var videosArr = angular.element(document.querySelector('[ng-controller="videosController"]')).scope().userVideos;
    for (var i = 0; i < videosArr.length; i++) {
        if (videosArr[i].id == videoId) {
            return i;
        }
    }
}

function onCreateNewVideoClick() {
    window.location = '/content/uploadImages.html';
}

function onVideoEditClick(videoId) {
    localStorage.setItem("videoId", videoId);
    window.location = '/content/editor.html';
}

function clickOnTripVideosOptionNativeJs() {
    angular.element(document.querySelector('[ng-controller="videosController"]')).scope().clickOnTripVideosOption();
    angular.element(document.querySelector('[ng-controller="videosController"]')).scope().$apply();
}

function clickOnVideosToWatchOptionNativJs() {
    angular.element(document.querySelector('[ng-controller="videosController"]')).scope().clickOnVideosToWatchOption();
    angular.element(document.querySelector('[ng-controller="videosController"]')).scope().$apply();
}

app.controller('videosController', function ($scope, $http) {
    $scope.videosLoading = true;
    $scope.videos = [];
    $scope.userVideos = [];
    $scope.videosViewMode = true;

    $scope.checkVideosViewMode = function () {
        return $scope.videosViewMode;
    }

    $scope.clickOnTripVideosOption = function () {
        var tempLoggedInBool = angular.element(document.querySelector('[ng-controller="myUser"]')).scope().userLoggedIn;
        if (!tempLoggedInBool) {
            alert('In order to create your own trip videos you must be registered/logged in');
            return;
        }
        $scope.videosViewMode = false;

        $('#myTripVideosOptionDiv').removeClass('menuOption')
        $('#myTripVideosOptionDiv').addClass('menuOptionSelected');

        $('#videosToWatchOptionDiv').removeClass('menuOptionSelected');
        $('#videosToWatchOptionDiv').addClass('menuOption');
    }

    $scope.clickOnVideosToWatchOption = function () {
        $scope.videosViewMode = true;

        $('#videosToWatchOptionDiv').removeClass('menuOption')
        $('#videosToWatchOptionDiv').addClass('menuOptionSelected');
        
        $('#myTripVideosOptionDiv').removeClass('menuOptionSelected');
        $('#myTripVideosOptionDiv').addClass('menuOption');
    }

    $scope.getRecVideos = function () {
        $http({
            url: '/GetVideosByFilter/GetVideosOfUser/?filter=',
            method: 'GET'
        }).success(function (data, status, headers, config) {
            $scope.videosLoading = false;
            $scope.videos = data;
        });
    }

    $scope.getUserVideos = function () {
        $scope.userVideos =[];
        //$http({
        //    url: '/getvideosofuser/getvideosofuser',
        //    method: 'GET'
        //}).success(function (data, status, headers, config) {
        //    console.log(data);
        //    $scope.userVideos = data;
        //});

        $.ajax({
            type: 'GET',
            url: '/getvideosofuser/getvideosofuser'
        }).done(function (data) {
            console.log(data);
            $scope.userVideos = data;
        });

    }

    setTimeout(function () {
        $scope.getRecVideos();
    }, 300);
    

    setTimeout(function () {
        $scope.getUserVideos();
    }, 300);
    

    //$scope.videos = [
    //    {
    //        img: "resources/videosPics/1.jpg",
    //        name: "Amsterdam - 8 days",
    //        uploader: "DaveBroox145",
    //        views: 123
    //    },
    //    {
    //        img: "resources/videosPics/2.jpg",
    //        name: "Brazil (Florianapolis) - 23 days",
    //        uploader: "DaveBroox145",
    //        views: 123
    //    },
    //    {
    //        img: "resources/videosPics/3.jpg",
    //        name: "Israel - 20 days ",
    //        uploader: "DaveBroox145",
    //        views: 123
    //    },
    //    {
    //        img: "resources/videosPics/4.jpg",
    //        name: "Tennessi (Texas, USA) - 10 days",
    //        uploader: "DaveBroox145",
    //        views: 123
    //    },
    //    {
    //        img: "resources/videosPics/5.jpg",
    //        name: "Egnland - 3 days",
    //        uploader: "DaveBroox145",
    //        views: 123
    //    },
    //    {
    //        img: "resources/videosPics/6.jpg",
    //        name: "South America - 126 days",
    //        uploader: "DaveBroox145",
    //        views: 123
    //    },
    //    {
    //        img: "resources/videosPics/7.jpg",
    //        name: "Spain - 5 days",
    //        uploader: "DaveBroox145",
    //        views: 123
    //    },
    //    {
    //        img: "resources/videosPics/8.jpg",
    //        name: "Italy - 9 days ",
    //        uploader: "DaveBroox145",
    //        views: 123
    //    }
    //]

    $scope.isGood = function (i) {
        if (i == 3) {
            return true;
        }
        else {
            return false;
        }
    }
});




function signUp() {

    var username = $('#registerUsername').val();
    var email = $('#registerEmail').val();
    var password = $('#registerPassword').val();
    var passwordConfirm = $('#registerPasswordConfirm').val();
    if (username == "" || password == "" || email == "") {
        return;
    }

    $.ajax({
        type: 'GET',
        url: '/createuser/get/?username=' + username + '&password=' + password + '&email='+ email
    }).done(function (data) {
        if (data) {
            $('#signedUpMsg').css('display', 'block');

            $('#signUpForm').hide();
            angular.element(document.querySelector('[ng-controller="myUser"]')).scope().userLoggedIn = true;

            $('#signInOption').hide();
            location.reload();
        }
        else {
            alert('Username is already taken, please choose another one');
        }
    });
}

function gotoVideo(videoId) {
    incVideoViews(videoId);
    window.location = 'movie.html?id=' + videoId // todo: go to real url
}

function incVideoViews(videoId) {
    $.ajax({
        type: 'POST',
        url: 'http://jsonstub.com/api/incVideoViews', // todo: real stub to check it
        data: { 'videoId': videoId },
        beforeSend: function (request) {
            request.setRequestHeader('JsonStub-User-Key', '589902c6-c52d-4a38-b0e9-7ae438abe8ce');
            request.setRequestHeader('JsonStub-Project-Key', 'd1134f6f-6ada-46c1-9855-f35ae2cbcfe8');
        }
    }).done(function (data) {

    });
}