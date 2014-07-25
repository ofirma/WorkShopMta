var DRIVING = "Driving";
var TRANSIT = "Transit";
var WALKING = "Walking";
function createDays() {
    var innerDays = [
        {
            day: 1,
            daySummary: "<br/>Day 1 - Tel Aviv<br/><br/>Rabin Square<br/>Dizengoff Square<br/>Taylet Tel Aviv",
            picsNum: 10,
            locations: [
                {
                    id: 0,
                    latLang: { x: 32.08018, y: 34.78056 },
                    name: "Rabin Square",
                    pics: [
                        { name: 'rabin1.jpg', caption: 'Rabin Square' },
                        { name: 'rabin2.jpg', caption: 'אני בכיכר' },
                        { name: 'rabin3.jpg', caption: 'בלה בלה בלה' },
                        { name: 'rabin1.jpg', caption: 'כיכר רבין' },
                        { name: 'rabin2.jpg', caption: 'אני בכיכר' },
                        { name: 'rabin3.jpg', caption: 'בלה בלה בלה' },
                        { name: 'rabin1.jpg', caption: 'כיכר רבין' },
                        { name: 'rabin2.jpg', caption: 'אני בכיכר' },
                        { name: 'rabin3.jpg', caption: 'בלה בלה בלה' },
                        { name: 'rabin1.jpg', caption: 'כיכר רבין' },
                        { name: 'rabin2.jpg', caption: 'אני בכיכר' },
                        { name: 'rabin3.jpg', caption: 'בלה בלה בלה' }
                    ],
                    travelModeToNextLocation: WALKING
                },
                {
                    id: 1,
                    latLang: { x: 32.077825, y: 34.773898 },
                    name: "Dizengof Square",
                    pics: [
                        { name: 'dizi1.jpg', caption: 'כיכר דיזנגוף 1' },
                        { name: 'dizi2.jpg', caption: 'כיכר דיזנגוף 2' },
                        { name: 'dizi3.jpg', caption: 'כיכר דיזנגוף 3' }
                    ],
                    travelModeToNextLocation: TRANSIT
                },
                {
                    id: 2,
                    latLang: { x: 32.079636, y: 34.766977 },
                    name: "Tayelet Tel Aviv",
                    pics: [
                        { name: 'Tayelet1.jpg', caption: 'טיילת תל אביב 1' },
                        { name: 'Tayelet2.jpg', caption: 'טיילת תל אביב 2' },
                        { name: 'Tayelet3.jpg', caption: 'טיילת תל אביב 3' }
                    ]
                }
            ]
        }
    ];

    return innerDays;
}