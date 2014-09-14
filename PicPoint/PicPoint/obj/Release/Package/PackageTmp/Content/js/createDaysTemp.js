var DRIVING = "Driving";
var TRANSIT = "Transit";
var WALKING = "Walking";
// http://dev.virtualearth.net/services/v1/geocodeservice/geocodeservice.asmx/ReverseGeocode?latitude=32.08018&longitude=34.78056&key=[YOU_KEY]&culture=%22en-us%22&format=json
function createDays() {
    return [];
}
/*
function createDays() {
    var innerDays = {
        "musicOptions": [
                            {
                                "id": 0,
                                "name": "Sunny Beach",
                                "path": "resources/sounds/1.mp3"
                            },
                            {
                                "id": 1,
                                "name": "Happiness",
                                "path": "resources/sounds/happiness.mp3"
                            },
                            {
                                "id": 2,
                                "name": "Acoustic Air",
                                "path": "resources/sounds/acoustic.mp3"
                            },
                            {
                                "id": 3,
                                "name": "Funky",
                                "path": "resources/sounds/funky.mp3"
                            },
                            {
                                "id": 4,
                                "name": "Clear Day",
                                "path": "resources/sounds/clearDay.mp3"
                            }
                        ],
        "backgroundMusic": {
            "id": 0,
            "name": "Sunny Beach",
            "path": "resources/sounds/1.mp3"
        },
        "days": [
            {
                "day": 1,
                "daySummary": "<br/>Day 1 - Tel Aviv<br/><br/>Rabin Square<br/>Dizengoff Square<br/>Taylet Tel Aviv",
                "picsNum": 10,
                "locations": [
                    {
                        "id": 0,
                        "seqId": 0,
                        "latLang": { "x": 32.08018, "y": 34.78056 },
                        "name": "Rabin Square",
                        "pics": [
                            { "name": "rabin1.jpg", "caption": "Rabin Square" },
                            { "name": "rabin2.jpg", "caption": "Rabin Square" },
                            { "name": "rabin3.jpg", "caption": "Rabin Square" },
                            { "name": "rabin1.jpg", "caption": "Rabin Square" },
                            { "name": "rabin2.jpg", "caption": "Rabin Square" },
                            { "name": "rabin3.jpg", "caption": "Rabin Square" },
                            { "name": "rabin1.jpg", "caption": "Rabin Square" },
                            { "name": "rabin2.jpg", "caption": "Rabin Square" },
                            { "name": "rabin3.jpg", "caption": "Rabin Square" }
                        ],
                        "showStreetView": true,
                        "story": "hello this is mystory blablab",
                        "travelModeToNextLocation": "Walking"
                    },
                    {
                        "id": 1,
                        "seqId": 1,
                        "latLang": { "x": 32.077825, "y": 34.773898 },
                        "name": "Dizengof Square",
                        "pics": [
                            { "name": "dizi1.jpg", "caption": "Rabin Square" },
                            { "name": "dizi2.jpg", "caption": "Rabin Square" },
                            { "name": "dizi3.jpg", "caption": "Rabin Square" }
                        ],
                        "story": "story number 2",
                        "showStreetView": true,
                        "travelModeToNextLocation": "Transit"
                    },
                    {
                        "id": "2",
                        "seqId": "2",
                        "latLang": { "x": 32.079636, "y": 34.766977 },
                        "name": "Tayelet Tel Aviv",
                        "pics": [
                            { "name": "Tayelet1.jpg", "caption": "Rabin Square" },
                            { "name": "Tayelet2.jpg", "caption": "Rabin Square" },
                            { "name": "Tayelet3.jpg", "caption": "Rabin Square" }
                        ],
                        "story": "story number 2",
                        "showStreetView": true
                    }
                ]
            },
            {
                "day": 2,
                "daySummary": "<br/>Day 1 - Tel Aviv<br/><br/>Rabin Square<br/>Dizengoff Square<br/>Taylet Tel Aviv",
                "picsNum": 4,
                "locations": [
                    {
                        "id": 0,
                        "seqId": 3,
                        "latLang": { "x": 32.08018, "y": 34.78056 },
                        "name": "222Rabin",
                        "pics": [
                            { "name": "rabin1.jpg", "caption": "Rabin Square" },
                            { "name": "rabin2.jpg", "caption": "Rabin Square" },
                            { "name": "rabin3.jpg", "caption": "Rabin Square" },
                            { "name": "rabin1.jpg", "caption": "Rabin Square" },
                            { "name": "rabin2.jpg", "caption": "Rabin Square" },
                            { "name": "rabin3.jpg", "caption": "Rabin Square" },
                            { "name": "rabin1.jpg", "caption": "Rabin Square" },
                            { "name": "rabin2.jpg", "caption": "Rabin Square" },
                            { "name": "rabin3.jpg", "caption": "Rabin Square" },
                            { "name": "rabin1.jpg", "caption": "Rabin Square" }
                        ],
                        "showStreetView": true,
                        "story": "tory number 2",
                        "travelModeToNextLocation": "Walking"
                    },
                    {
                        "id": 1,
                        "seqId": 4,
                        "latLang": { "x": 32.077825, "y": 34.773898 },
                        "name": "Dizengof Square",
                        "pics": [
                            { "name": "dizi1.jpg", "caption": "Blablabla" },
                            { "name": "dizi2.jpg", "caption": "Blablabla" },
                            { "name": "dizi3.jpg", "caption": "Blablabla" }
                        ],
                        "showStreetView": false,
                        "story": "story number 2",
                        "travelModeToNextLocation": "Transit"
                    }
                ]
            }
        ]
    };

    return innerDays;
}
*/