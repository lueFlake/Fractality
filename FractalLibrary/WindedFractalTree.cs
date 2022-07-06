using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalsLib {
    /// <summary>
    /// Класс фрактала: Обдуваемое Фрактальное дерево.
    /// </summary>
    public class WindedFractalTree : BaseFractal {

        /// <summary>
        /// Глубина рекурсии при генерации фрактала.
        /// </summary>
        public override int Depth { get; set; }

        /// <summary>
        /// Градиент и цвет фрактала.
        /// </summary>
        public override Gradient Color { get; protected set; }

        /// <summary>
        /// Максимальная глубина рекурсии при генерации фрактала.
        /// </summary>
        public override int MaxDepth { get; protected set; }

        /// <summary>
        /// Угол поворота левой ветви.
        /// </summary>
        public double Angle1 { get; set; }

        /// <summary>
        /// Угол поворота правой ветви.
        /// </summary>
        public double Angle2 { get; set; }

        /// <summary>
        /// Относительный размер следующей линии.
        /// </summary>
        public double Resolution { get; set; }
      
        private Point _point1;
        private Point _point2;
        private static WindedFractalTree? s_instance;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="point1">Точка начала первого отрезка.</param>
        /// <param name="point2">Точка конца первого отрезка.</param>
        /// <param name="angle1">Угол поворота левой ветви.</param>
        /// <param name="angle2">Угол поворота правой ветви.</param>
        /// <param name="res">Относительный размер следующей линии.</param>
        /// <param name="depth">Глубина рекурсии при генерации фрактала.</param>
        /// <param name="firstColor">Начальный цвет градиента.</param>
        /// <param name="lastColor">Конечный цвет градиента.</param>
        public WindedFractalTree(Point point1, Point point2, double angle1, double angle2, double res, int depth, Color firstColor, Color lastColor) : base(depth) {
            Angle1 = angle1;
            Angle2 = angle2;

            Resolution = res;

            Color = new Gradient(firstColor, lastColor, depth);
            MaxDepth = 12;
        }

        /// <summary>
        /// Генерация фрактала.
        /// </summary>
        /// <param name="depth">Глубина рекурсии.</param>
        /// <param name="point1">Точка начала.</param>
        /// <param name="point2">Точка конца.</param>
        public void GetShapes(int depth, Point point1, Point point2) {
            var line = new Line();
            line.X1 = point1.X;
            line.Y1 = point1.Y;
            line.X2 = point2.X;
            line.Y2 = point2.Y;
            line.Stroke = new SolidColorBrush(Color.Colors[depth]);
            line.StrokeThickness = 2;

            if (depth == 0) {
                DrawingArea.Children.Add(line);
                return;
            }

            var vector = new Vector(
                point2, 
                new Point((point2.X - point1.X) / Resolution + point2.X, (point2.Y - point1.Y) / Resolution + point2.Y)
            );

            GetShapes(
                depth - 1,
                point2,
                point2 + vector.Rotate(Angle1)
            );

            GetShapes(
                depth - 1,
                point2,
                point2 + vector.Rotate(Angle2)
            );

            DrawingArea.Children.Add(line);
        }

        /// <summary>
        /// Вывод фрактала на холст.
        /// </summary>
        public override void Render() {
            GetShapes(
                Depth, 
                new(DrawingArea.ActualWidth / 2, DrawingArea.ActualHeight * 5 / 6),
                new(DrawingArea.ActualWidth / 2, DrawingArea.ActualHeight * 3 / 6)
            );
        }

        /// <summary>
        /// Получить инстанцию данного класса.
        /// </summary>
        /// <returns>Этот класс.</returns>
        public static WindedFractalTree GetInstance() {
            return s_instance == null ? new WindedFractalTree(
                new Point(250, 0),
                new Point(250, 150),
                Math.PI / 4.0,
                -Math.PI / 6.0,
                1.5,
                14,
                Colors.LightGray,
                Colors.Black
            ) : s_instance;
        }

        public override void ChangeColor(Color firstColor, Color secondColor) {
            Color = new Gradient(firstColor, secondColor, Depth + 1);
        }
    }
}