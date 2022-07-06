using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;

namespace FractalsLib {
    /// <summary>
    /// Класс фрактала: Множество Кантора.
    /// </summary>
    public class KantorsSet : BaseFractal {
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

        /// <summary>
        /// Относительный размер следующей линии.
        /// </summary>
        public double Resolution { get => _resolution; set => _resolution = value * 5; }

        private static KantorsSet? s_instance;

        private double _resolution;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="res">Относительный размер следующей линии.</param>
        /// <param name="depth">Глубина рекурсии при генерации фрактала.</param>
        /// <param name="firstColor">Начальный цвет градиента.</param>
        /// <param name="lastColor">Конечный цвет градиента.</param>
        private KantorsSet(double res, int depth, Color firstColor, Color lastColor) : base(depth) {
            _resolution = res * 5;
            Color = new Gradient(firstColor, lastColor, depth);
            MaxDepth = 10;
        }


        /// <summary>
        /// Генерация фрактала.
        /// </summary>
        /// <param name="depth">Глубина рекурсии.</param>
        /// <param name="begin">Точка начала.</param>
        /// <param name="end">Точка конца.</param>
        public void GetShapes(int depth, Point begin, Point end) {
            var line = new Line();
            line.X1 = begin.X;
            line.Y1 = begin.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;
            line.Stroke = new SolidColorBrush(Color.Colors[depth]);
            line.StrokeThickness = 2;

            if (depth == 0) {
                DrawingArea.Children.Add(line);
                return;
            }
            
            GetShapes(
                depth - 1,
                new Point(begin.X, begin.Y + _resolution),
                new Point(begin.X + (end.X - begin.X) / 3.0, begin.Y + _resolution)
            );
            GetShapes(
                depth - 1,
                new Point(end.X - (end.X - begin.X) / 3.0, end.Y + _resolution),
                new Point(end.X, end.Y + _resolution)
             );

            DrawingArea.Children.Add(line);
        }

        /// <summary>
        /// Получить инстанцию данного класса.
        /// </summary>
        /// <returns>Этот класс.</returns>
        public static KantorsSet GetInstance() {
            return (s_instance == null ? new KantorsSet(
                10.5,
                5,
                Colors.LightGray,
                Colors.Black
            ) : s_instance);
        }

        /// <summary>
        /// Вывод фрактала на холст.
        /// </summary>
        public override void Render() {
            GetShapes(
                Depth, 
                new(DrawingArea.ActualWidth / 5, DrawingArea.ActualHeight / 5), 
                new(DrawingArea.ActualWidth * 4 / 5, DrawingArea.ActualHeight / 5)
            );
        }

        public override void ChangeColor(Color firstColor, Color secondColor) {
            Color = new Gradient(firstColor, secondColor, Depth);
        }
    }
}