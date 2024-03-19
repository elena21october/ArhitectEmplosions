class Point {
    X;
    Y;
    constructor(x, y) {
        this.X = x;
        this.Y = y;
    }
}
class Emotion {
    Color;
    Points;
    constructor(coord, color) {
        //console.log(coord);
        this.Points = [];
        this.Color = color;
        for (var i = 0; i < coord[0].length; i++) {
            this.Points.push(new Point(coord[0][i][0], coord[0][i][1]));
        }
    }
}
class Diffetentiation {
    Gender;
    Place;
    Visiting;
    Age;
    constructor(gender, place, visit, age) {
        this.Gender = gender;
        this.Place = place;
        this.Visiting = visit;
        this.Age = age;
    }
}
class Questionnaire {
    Emotions;
    Diff;
    Comm;
    constructor(emotions, diff, comm) {
        this.Emotions = emotions;
        this.Diff = diff;
        this.Comm = comm;
    }
}
async function GetData() {
    const response = await fetch("/HazarNabeg/GetQuestionnaires",
        {
            method: "GET",
            headers: { "Accept": "application/json" }
        });
    if (response.ok === true) {
        let data = await response.json();
        return JSON.parse(data);
    }
}
var questionnaires = document.getElementById("quests");
var listQuest = [];
var colorChose = "green";
ymaps.ready(initAdd);

async function initAdd() {
    let hazar = await GetData();
    var myMap = new ymaps.Map("map",
        {
            center: [Hazar.X, Hazar.Y],
            zoom: 15

        });
    SetBorder();
}
document.getElementById("createPolygon").addEventListener("click", createPolygon);
function createPolygon() {
    var radiobuttons = document.getElementsByName("emot");
    for (var i = 0; i < radiobuttons.length; i++) {
        if (radiobuttons[i].checked == true) {
            colorChose = radiobuttons[i].value;
            break;
        }
    }
    polygon = new ymaps.Polygon([], {},
        {
            editorDrawingCursor: "crosshair",
            editorMaxPoints: 9,
            fillColor: colorChose,
            strokeColor: colorChose,
            strokeWidth: 5,
        });
    myMap.geoObjects.add(polygon);
    polygon.editor.startDrawing();
}
document.getElementById("createQuest").addEventListener("click", function () {
    var genVal = diffVal(document.getElementsByName("Gender"));
    var placeVal = diffVal(document.getElementsByName("Place"));
    var visitorVal = diffVal(document.getElementsByName("Visiting"));
    var ageVal = diffVal(document.getElementsByName("Age"));
    var diff = new Diffetentiation(genVal, placeVal, visitorVal, ageVal);
    var emotionList = [];
    myMap.geoObjects.each(function (obj) {
        emotionList.push(new Emotion(obj.geometry.getCoordinates(), obj.geometry.options._parent._options.fillColor));
    });
    var quest = new Questionnaire(JSON.stringify(emotionList), JSON.stringify(diff), null);
    listQuest.push(quest);
    myMap.geoObjects.removeAll();
});

ymaps.ready(initLook)
function initLook() {
    var lookMap = new ymaps.Map("mapLook",
        {
            center: [1, 1],
            zoom: 15
        });
    document.getElementById("addDiv").addEventListener("click", function () {
        var add = document.getElementById("addVar");
        var push = document.getElementById("pushVar");
        push.setAttribute("style", "display:none");
        add.setAttribute("style", "");
    });

    document.getElementById("lookDiv").addEventListener("click", function () {
        var add = document.getElementById("addVar");
        var push = document.getElementById("pushVar");
        add.setAttribute("style", "display:none");
        push.setAttribute("style", "");
        lookMap.setCenter([Hazar.X, Hazar.Y], 15);
        for (var i = 0; i < listQuest.length; i++) {
            var del = document.getElementById("look" + i);
            if (del != null) {
                del.remove();
            }
        }
        for (var i = 0; i < listQuest.length; i++) {
            let div = document.createElement('div');
            div.className = "alert";
            div.innerHTML = "Анкета № " + (i + 1);
            div.setAttribute("id", "look" + i);
            let btn = document.createElement('button');
            btn.setAttribute("id", "del" + i);
            btn.innerText = "Удалить";
            div.appendChild(btn);
            questionnaires.appendChild(div);
            document.getElementById("look" + i).addEventListener("click", () => func(div.id));
            document.getElementById("del" + i).addEventListener("click", () => delet(div.id));
        }
    });
    function func(pp) {
        var item = listQuest[pp.slice(4)];
        lookMap.geoObjects.removeAll();
        for (n of item.Emotions) {
            var polygon = DrawEmotion(n);
            lookMap.geoObjects.add(polygon);
        }

    }
    function delet(ii) {
        var rm = document.getElementById(ii);
        rm.remove();
    }
    document.getElementById("sendBtn").addEventListener("click", send);
    async function send() {
        console.log(listQuest);
        const response = await fetch(`/HazarNabeg/GiveQuestionnaire`, {
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
            },
            body: JSON.stringify({
                Quastionnaires: listQuest,
            })
        });
        if (response.ok === true) {
            alert("Excellent")
        }
        else {
            const error = await response.json();
            console.log(error.message);
        }
    }
}
document.getElementById("teach").addEventListener("click", teacher);
function teacher() {
    let teachDiv = document.getElementById("parent_popup");
    teachDiv.setAttribute("style", "");
}
function SetBorder() {
    var Border1 = new ymaps.Polyline(
        [[54.81381547028136, 56.058855168250936], [54.81978506189913, 56.057144323137514], [54.82061663545558, 56.05780123918561], [54.82004358269541, 56.059759251766465],
        [54.81972762578878, 56.05949103086499], [54.81807788083479, 56.05995757290714], [54.818353206147655, 56.06327814766757], [54.81694680996119, 56.0635892839133], [54.81402263845585, 56.06174321584752],
        [54.81381547028136, 56.058855168250936]], {},
        {
            strokeWidth: 5,
            strokeColor: '#ff0000'
        });
    var Border2 = new ymaps.Polyline(
        [[54.818822209793694, 56.067258947081115], [54.81952228359511, 56.067307226843404], [54.81986302307071, 56.06764518517927], [54.820287394561795, 56.06857322949845], [54.81956874823908, 56.06930815476852],
        [54.819389084651455, 56.069002382940816], [54.8192249086022, 56.06884145039992], [54.81899568054412, 56.06886290807203], [54.818822209793694, 56.067258947081115]], {},
        {
            strokeWidth: 5,
            strokeColor: '#ff0000'
        });
    var Border3 = new ymaps.Polyline(
        [[54.72267068870143, 56.01271888931717], [54.723341385095786, 56.01157090385876], [54.724095905214504, 56.01257941444838], [54.7239592853337, 56.01280472000565], [54.72437535352996, 56.0133733483168],
        [54.724946664063026, 56.01359328945603], [54.72558316898897, 56.01462325771771], [54.72506154618249, 56.016243311962704], [54.72267068870143, 56.01271888931717]], {},
        {
            strokeWidth: 5,
            strokeColor: '#ff0000'
        });
    var Border4 = new ymaps.Polyline(
        [[54.725387894949755, 55.969689626240275], [54.72625104586402, 55.97008659317448], [54.726033707445126, 55.971556443714626], [54.7251332929521, 55.97113265469029], [54.725387894949755, 55.969689626240275]], {},
        {
            strokeWidth: 5,
            strokeColor: '#ff0000'
        });
    var Border5 = new ymaps.Polyline(
        [[54.71693155925201, 55.99654339621368], [54.7171784491901, 55.99713616440598], [54.71701075052875, 55.99740974972548], [54.71734925266595, 55.99825196335618], [54.717041805888975, 55.99876158306899],
        [54.71672038175441, 55.99810175965134], [54.71647504180358, 55.99825464556517], [54.71617704766842, 55.99751781341211], [54.71693155925201, 55.99654339621368]
        ], {},
        {
            strokeWidth: 5,
            strokeColor: '#ff0000'
        });
    var Border6 = new ymaps.Polyline(
        [[54.77159315450484, 56.01339989740415], [54.772228923378734, 56.014671264477215], [54.77198392095236, 56.015105782337606], [54.771320236007384, 56.01399534780548], [54.77159315450484, 56.01339989740415]], {},
        {
            strokeWidth: 5,
            strokeColor: '#ff0000'
        });
    myMap.geoObjects.add(Border1);
    myMap.geoObjects.add(Border2);
    myMap.geoObjects.add(Border3);
    myMap.geoObjects.add(Border4);
    myMap.geoObjects.add(Border5);
    myMap.geoObjects.add(Border6);
}
function diffVal(list) {
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked) {
            return list[i].value;
        }
    }
    return "";
}
function DrawEmotion(emotion) {
    console.log(emotion);
    if (emot.length == 3) {
        return new ymaps.Polygon(
            [[
                [emot.Points[0].X, emot.Points[0].Y],
                [emot.Points[1].X, emot.Points[1].Y],
                [emot.Points[2].X, emot.Points[2].Y],
            ]], {},
            {
                // Цвет заливки.
                fillColor: emot.Color,
                // Цвет обводки.
                strokeColor: emot.Color,
                // Ширина обводки.
                strokeWidth: 5
            });
    }
    else if (emot.length == 4) {
        return new ymaps.Polygon(
            [[
                [emot.Points[0].X, emot.Points[0].Y],
                [emot.Points[1].X, emot.Points[1].Y],
                [emot.Points[2].X, emot.Points[2].Y],
                [emot.Points[3].X, emot.Points[3].Y],
            ]], {},
            {
                // Цвет заливки.
                fillColor: emot.Color,
                // Цвет обводки.
                strokeColor: emot.Color,
                // Ширина обводки.
                strokeWidth: 5
            });
    }
    else if (emot.length == 5) {
        return new ymaps.Polygon(
            [[
                [emot.Points[0].X, emot.Points[0].Y],
                [emot.Points[1].X, emot.Points[1].Y],
                [emot.Points[2].X, emot.Points[2].Y],
                [emot.Points[3].X, emot.Points[3].Y],
                [emot.Points[4].X, emot.Points[4].Y],
            ]], {},
            {
                // Цвет заливки.
                fillColor: emot.Color,
                // Цвет обводки.
                strokeColor: emot.Color,
                // Ширина обводки.
                strokeWidth: 5
            });
    }
    else if (emot.length == 6) {
        return new ymaps.Polygon(
            [[
                [emot.Points[0].X, emot.Points[0].Y],
                [emot.Points[1].X, emot.Points[1].Y],
                [emot.Points[2].X, emot.Points[2].Y],
                [emot.Points[3].X, emot.Points[3].Y],
                [emot.Points[4].X, emot.Points[4].Y],
                [emot.Points[5].X, emot.Points[5].Y],
            ]], {},
            {
                // Цвет заливки.
                fillColor: emot.Color,
                // Цвет обводки.
                strokeColor: emot.Color,
                // Ширина обводки.
                strokeWidth: 5
            });
    }
    else if (emot.length == 7) {
        return new ymaps.Polygon(
            [[
                [emot.Points[0].X, emot.Points[0].Y],
                [emot.Points[1].X, emot.Points[1].Y],
                [emot.Points[2].X, emot.Points[2].Y],
                [emot.Points[3].X, emot.Points[3].Y],
                [emot.Points[4].X, emot.Points[4].Y],
                [emot.Points[5].X, emot.Points[5].Y],
                [emot.Points[6].X, emot.Points[6].Y],
            ]], {},
            {
                // Цвет заливки.
                fillColor: emot.Color,
                // Цвет обводки.
                strokeColor: emot.Color,
                // Ширина обводки.
                strokeWidth: 5
            });
    }
    else if (emot.length == 8) {
        return new ymaps.Polygon(
            [[
                [emot.Points[0].X, emot.Points[0].Y],
                [emot.Points[1].X, emot.Points[1].Y],
                [emot.Points[2].X, emot.Points[2].Y],
                [emot.Points[3].X, emot.Points[3].Y],
                [emot.Points[4].X, emot.Points[4].Y],
                [emot.Points[5].X, emot.Points[5].Y],
                [emot.Points[6].X, emot.Points[6].Y],
            ]], {},
            {
                // Цвет заливки.
                fillColor: emot.Color,
                // Цвет обводки.
                strokeColor: emot.Color,
                // Ширина обводки.
                strokeWidth: 5
            });
    }
}