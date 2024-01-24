
//classes for quest
class Point {
    X;
    Y;
}
class Emotion {
    Color;
    Points;
    constructor() {
        this.Points = [];
    }
}
class Diffetentiation {
    Gender;
    Place;
    Visiting;
    Age;
}
class Questionnaire {
    Id;
    Differentiation;
    PositiveComm;
    NegativeComm;
    NeutralComm;
    Emotions;
    HazarNabegId;
    HazarNabeg;
    hazarUserId;
    DateTime;
    constructor() {
        this.Emotions = [];
    }
}
var questionnaires = document.getElementById("quests");
var listQuest = [];
var quest = new Questionnaire();
var Hazar = "";
ymaps.ready(initAdd);

function initAdd() {

    const reuqestUrl = "/HazarNabeg/GetQuestionnaires";
    const xhr = new XMLHttpRequest();
    xhr.open("Get", reuqestUrl);
    xhr.responseType = "json";
    xhr.onload = () => {
        Hazar = JSON.parse(xhr.response);
        var myMap = new ymaps.Map("map",
            {
                center: [Hazar.X, Hazar.Y],
                zoom: 15

            });

        document.getElementById("createPolygon").addEventListener("click", createPolygon);
        function createPolygon() {
            var emotion = new Emotion();
            var radiobuttons = document.getElementsByName("emot");
            for (var i = 0; i < radiobuttons.length; i++) {
                if (radiobuttons[i].checked == true) {
                    emotion.Color = radiobuttons[i].value;
                    break;
                }
            }
            polygon = new ymaps.Polygon([], {},
                {
                    editorDrawingCursor: "crosshair",
                    editorMaxPoints: 5,
                    fillColor: emotion.Color,
                    strokeColor: emotion.Color,
                    strokeWidth: 5
                });
            myMap.geoObjects.add(polygon);
            polygon.editor.startDrawing();

            $('input').attr('disabled', false);

            $('#stopEditPolyline').click(function () {
                $('#stopEditPolyline').attr('disabled', true);
                polygon.editor.stopEditing();
                var coord = polygon.geometry.getCoordinates();

                var point1 = new Point();
                point1.X = coord[0][0][0];
                point1.Y = coord[0][0][1];

                var point2 = new Point();
                point2.X = coord[0][1][0];
                point2.Y = coord[0][1][1];

                var point3 = new Point();
                point3.X = coord[0][2][0];
                point3.Y = coord[0][2][1];

                var point4 = new Point();
                point4.X = coord[0][3][0];
                point4.Y = coord[0][3][1];
                if (emotion.Points.length != 4) {
                    emotion.Points.push(point1);
                    emotion.Points.push(point2);
                    emotion.Points.push(point3);
                    emotion.Points.push(point4);
                    quest.Emotions.push(emotion);
                }
                $('stopEditPolyline').attr('disabled', false);
            });
        }
        function diffVal(list) {
            for (var i = 0; i < list.length; i++) {
                if (list[i].checked) {
                    return list[i].value;
                }
            }
            return "";
        }
        document.getElementById("createQuest").addEventListener("click", function () {
            var genVal = diffVal(document.getElementsByName("Gender"));
            var placeVal = diffVal(document.getElementsByName("Place"));
            var visitorVal = diffVal(document.getElementsByName("Visiting"));
            var ageVal = diffVal(document.getElementsByName("Age"));
            var diff = new Diffetentiation();
            diff.Gender = genVal;
            diff.Place = placeVal;
            diff.Visiting = visitorVal;
            diff.Age = ageVal;
            quest.Differentiation = diff;
            listQuest.push(quest);
            quest = new Questionnaire();
            myMap.geoObjects.removeAll();
        });
    }
    xhr.send();
}

ymaps.ready(initLook)
function initLook() {
    var lookMap = new ymaps.Map("mapLook",
        {
            center: [30, 30],
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
            div.setAttribute("id", i);
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
            var polygon = new ymaps.Polygon(
                [[
                    [n.Points[0].X, n.Points[0].Y],
                    [n.Points[1].X, n.Points[1].Y],
                    [n.Points[2].X, n.Points[2].Y],
                    [n.Points[3].X, n.Points[3].Y],
                ]], {},
                {
                    editorMaxPoints: 5,
                    // Цвет заливки.
                    fillColor: n.Color,
                    // Цвет обводки.
                    strokeColor: n.Color,
                    // Ширина обводки.
                    strokeWidth: 5

                });
            lookMap.geoObjects.add(polygon);
        }

    }
    function delet(ii) {
        var rm = document.getElementById(ii);
        rm.remove();
    }
    document.getElementById("sendBtn").addEventListener("click", send);
    async function send() {
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
            console.log("excellent");
        }
        else {
            const error = await response.json();
            console.log(error.message);
        }
    }
}