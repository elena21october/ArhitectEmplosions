function DrawEmotion(emot) {
    if (emot.Points.length == 3) {
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
    else if (emot.Points.length == 4) {
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
    else if (emot.Points.length == 5) {
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
    else if (emot.Points.length == 6) {
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
    else if (emot.Points.length == 7) {
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
    else if (emot.Points.length == 8) {
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