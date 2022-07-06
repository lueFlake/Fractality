using System;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows.Controls;
using System.Threading;

namespace FractalsLib {
    /// <summary>
    /// Класс фрактала: Ковёр Серпинского.
    /// </summary>
    public class SerpinskyCarpet : BaseFractal {
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

        private double _squareSize;
        private double _left;
        private double _top;
        private static SerpinskyCarpet? s_instance;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depth">Глубина рекурсии при генерации фрактала.</param>
        /// <param name="firstColor">Начальный цвет градиента.</param>
        /// <param name="lastColor">Конечный цвет градиента.</param>
        private SerpinskyCarpet(int depth, Color firstColor, Color lastColor) : base(depth) {
            Color = new Gradient(firstColor, lastColor, depth);
            MaxDepth = 6;
        }


        /// <summary>
        /// Генерация фрактала.
        /// </summary>
        /// <param name="depth">Глубина рекурсии.</param>
        /// <param name="left">Отступ слева.</param>
        /// <param name="top">Отступ справа.</param>
        /// <param name="squareSize">Площадь занимаемая квадратом.</param>
        private void GetShapes(int depth, double left, double top, double squareSize) {
            if (depth == 0) {
                return;
            }

            double subsquareSize = squareSize / 3.0;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (i != 1 || j != 1) {
                        GetShapes(depth - 1, left + subsquareSize * i, top + subsquareSize * j, subsquareSize);
                    }
                    else {
                        var polygon = new Polygon() {
                            Fill = new SolidColorBrush(Color.Colors[depth]),
                        };
                        polygon.Points = new PointCollection() {
                            new System.Windows.Point(left + subsquareSize, top + subsquareSize),
                            new System.Windows.Point(left + subsquareSize * 2, top + subsquareSize),
                            new System.Windows.Point(left + subsquareSize * 2, top + subsquareSize * 2),
                            new System.Windows.Point(left + subsquareSize, top + subsquareSize * 2)
                        };
                        DrawingArea?.Children.Add(polygon);
                    }
                }
            }

            return; 
        }


        /// <summary>
        /// Вывод фрактала на холст.
        /// </summary>
        public override void Render() {
            _left = DrawingArea.ActualWidth / 2 - DrawingArea.ActualWidth / 4;
            _top = DrawingArea.ActualHeight / 2 - DrawingArea.ActualHeight / 4;
            _squareSize = Math.Min(DrawingArea.ActualHeight, DrawingArea.ActualWidth) / 2;

            var polygon = new Polygon() {
                Fill = new SolidColorBrush(Color.Colors.First())
            };
            polygon.Points = new PointCollection() {
                    new System.Windows.Point(_left, _top),
                    new System.Windows.Point(_left + _squareSize, _top),
                    new System.Windows.Point(_left + _squareSize, _top + _squareSize),
                    new System.Windows.Point(_left, _top + _squareSize)
                };
            DrawingArea?.Children.Add(polygon);
            GetShapes(Depth, _left, _top, _squareSize);
        }

        /// <summary>
        /// Получить инстанцию данного класса.
        /// </summary>
        /// <returns>Этот класс.</returns>
        public static SerpinskyCarpet GetInstance() {
            return (s_instance == null ? new SerpinskyCarpet(5, Colors.LightGray, Colors.Black) : s_instance);
        }

        public override void ChangeColor(Color firstColor, Color secondColor) {
            Color = new Gradient(firstColor, secondColor, Depth);
        }
    }
}