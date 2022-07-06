using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalsLib {
    /// <summary>
    /// Класс фрактала: Треугольник Серпинского.
    /// </summary>
    public class SerpinskyTriangle : BaseFractal {

        private int _depth;

        /// <summary>
        /// Глубина рекурсии при генерации фрактала.
        /// </summary>
        public override int Depth {
            get => _depth;
            set {
                _depth = value;
                if (Color != null) {
                    Color = new Gradient(Color.Colors.First(), Color.Colors.Last(), _depth + 1);
                }
            }
        }

        /// <summary>
        /// Максимальная глубина рекурсии при генерации фрактала.
        /// </summary>
        public override int MaxDepth { get; protected set; }

        /// <summary>
        /// Градиент и цвет фрактала.
        /// </summary>
        public override Gradient Color { get; protected set; }


        private static SerpinskyTriangle? s_instance;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depth">Глубина рекурсии при генерации фрактала.</param>
        /// <param name="firstColor">Начальный цвет градиента.</param>
        /// <param name="lastColor">Конечный цвет градиента.</param>
        private SerpinskyTriangle(int depth, Color firstColor, Color lastColor) : base(depth) {
            Color = new Gradient(firstColor, lastColor, depth);
            MaxDepth = 10;
        }

        /// <summary>
        /// Центр между двумя точками.
        /// </summary>
        /// <param name="point1">1-я точка.</param>
        /// <param name="point2">2-я точка.</param>
        /// <returns>Центральная точка.</returns>
        private Point GetCenter(Point point1, Point point2) {
            return new Point((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
        }

        /// <summary>
        /// Генерация фрактала.
        /// </summary>
        /// <param name="depth">Глубина рекурсии.</param>
        /// <param name="points">Точки треугольника на текущей итерации.</param>
        private void GetShapes(int depth, PointCollection points) {
            if (depth != 0) {
                GetShapes(depth - 1, new() {
                    points[0],
                    GetCenter(points[0], points[1]),
                    GetCenter(points[0], points[2])
                });
                GetShapes(depth - 1, new() {
                    GetCenter(points[0], points[1]),
                    points[1],
                    GetCenter(points[1], points[2])
                });
                GetShapes(depth - 1, new() {
                    GetCenter(points[0], points[2]),
                    GetCenter(points[1], points[2]),
                    points[2]
                });
            }

            points.Add(points[0]);
            var poly = new Polyline();
            poly.Points = points;
            poly.Stroke = new SolidColorBrush(Color.Colors[depth]);
            poly.StrokeThickness = 2;
            DrawingArea.Children.Add(poly);
        }

        /// <summary>
        /// Получить инстанцию данного класса.
        /// </summary>
        /// <returns>Этот класс.</returns>
        public static SerpinskyTriangle GetInstance() {
            return (s_instance == null ? new SerpinskyTriangle(5, Colors.LightGray, Colors.Black) : s_instance);
        }

        /// <summary>
        /// Вывод фрактала на холст.
        /// </summary>
        public override void Render() {
            double side = Math.Min(DrawingArea.ActualWidth, DrawingArea.ActualHeight);
            var points = new PointCollection();
            points.Add(new(side / 4, side * 3 / 4));
            points.Add(new(DrawingArea.ActualWidth - side / 4, side * 3 / 4));
            var vector = new Vector(points[0], points[1]);
            points.Add(points[0] + vector.Rotate(-Math.PI / 3));
            GetShapes(Depth, points);
        }

        public override void ChangeColor(Color firstColor, Color secondColor) {
            Color = new Gradient(firstColor, secondColor, Depth);
        }
    }
}