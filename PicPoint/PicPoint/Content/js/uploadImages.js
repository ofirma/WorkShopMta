function onContinueToVideoEditorClick() {
    $.ajax({
        type: 'GET',
        url: '/createtrip/get'
    }).done(function (data) {
        var videoId = data;
        localStorage.setItem("videoId", videoId);
        window.location = 'editor.html';
    });
    //var videoId = 56;
    //localStorage.setItem("videoId", videoId);
    //window.location = 'editor.html';
}

function onSearchClick() {
    window.location = 'main.html?=' + $('#searchInput').val();
}

function onSearchEnter(e) {
    if (e.keyCode == 13) {
        onSearchClick();
    }
}

function logout() {
    $.ajax({
        type: 'GET',
        url: '/SignOut/Get'
    }).done(function (data) {
        window.location = 'main.html';
    });
}

$(function () {
    $('#userOptions').tooltipster({
        offsetY: -15,
        arrow: false,
        content: $('#userOptionsToolTip'),
        contentAsHtml: true,
        interactive: true,
        autoClose: true,//
        theme: 'my-custom-themeNoBorder',
        minWidth: 130
    });
});