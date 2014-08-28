function onContinueToVideoEditorClick() {
    //$.ajax({
    //    type: 'GET',
    //    url: '/www/get'
    //}).done(function (data) {
    //    var videoId = data;
    //    localStorage.setItem("videoId", videoId);
    //    window.location = 'editor.html';
    //});
    var videoId = 56;
    localStorage.setItem("videoId", videoId);
    window.location = 'editor.html';
}