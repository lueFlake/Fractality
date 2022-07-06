using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalsLib {
    /// <summary>
    /// Класс фрактала: Кривая Коха.
    /// </summary>
    public class KohsCurve : BaseFractal {
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
        /// Градиент и цвет фрактала.
        /// </summary>
        public override Gradient Color { get; protected set; }

        /// <summary>
        /// Максимальная глубина рекурсии при генерации фрактала.
        /// </summary>
        public override int MaxDepth { get; protected set; }

        private (Point begin, Point end) _segment;
        private static KohsCurve? s_instance;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depth">Глубина рекурсии при генерации фрактала.</param>
        /// <param name="firstColor">Начальный цвет градиента.</param>
        /// <param name="lastColor">Конечный цвет градиента.</param>
        private KohsCurve(int depth, Color firstColor, Color lastColor) : base(depth) {
            Color = new Gradient(firstColor, lastColor, depth);
            MaxDepth = 6;
        }


        /// <summary>
        /// Генерация фрактала.
        /// </summary>
        /// <param name="depth">Глубина рекурсии.</param>
        /// <param name="start">Точка начала.</param>
        /// <param name="end">Точка конца.</param>
        private void GetShapes(int depth, Point start, Point end) {
            if (depth == 0) {
                DrawingArea.Children.Add(new Line() {
                    X1 = start.X,
                    Y1 = start.Y,
                    X2 = end.X,
                    Y2 = end.Y,
                    Stroke = new SolidColorBrush(Color.Colors.Last()),
                    StrokeThickness = 2
                });
                return;
            }
            var subsegmentVector = new Vector(
                (end.X - start.X) / 3.0,
                (end.Y - start.Y) / 3.0
            );
            GetShapes(depth - 1, start, start + subsegmentVector);
            var intermediate = start + subsegmentVector;
            GetShapes(depth - 1, intermediate, intermediate + subsegmentVector.Rotate(-Math.PI / 3.0));
            DrawingArea.Children.Add(new Line() {
                X1 = intermediate.X,
                Y1 = intermediate.Y,
                X2 = (start + subsegmentVector * 2).X,
                Y2 = (start + subsegmentVector * 2).Y,
                Stroke = new SolidColorBrush(Color.Colors[depth]),
                StrokeThickness = 2
            });
            intermediate += subsegmentVector.Rotate(-Math.PI / 3.0);
            GetShapes(depth - 1, intermediate, start + subsegmentVector * 2);
            GetShapes(depth - 1, start + subsegmentVector * 2, end);
        }

        /// <summary>
        /// Получить инстанцию данного класса.
        /// </summary>
        /// <returns>Этот класс.</returns>
        public static KohsCurve GetInstance() {
            return (s_instance == null ? new KohsCurve(8, Colors.LightGray, Colors.Black) : s_instance);
        }

        /// <summary>
        /// Вывод фрактала на холст.
        /// </summary>
        public override void Render() {
            GetShapes(Depth, 
                new(DrawingArea.ActualWidth / 5, DrawingArea.ActualHeight * 3 / 4), 
                new(DrawingArea.ActualWidth * 4 / 5, DrawingArea.ActualHeight * 3 / 4));
        }

        public override void ChangeColor(Color firstColor, Color secondColor) {
            Color = new Gradient(firstColor, secondColor, Depth + 1);
        }
    }
}