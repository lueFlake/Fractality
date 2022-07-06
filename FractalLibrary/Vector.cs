using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace FractalsLib {
    /// <summary>
    /// Класс вектора.
    /// </summary>
    internal class Vector {
        /// <summary>
        /// X координата вектора.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y координата вектора.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Конструктор класса по координатам.
        /// </summary>
        /// <param name="x">X координата вектора.</param>
        /// <param name="y">Y координата вектора.</param>
        public Vector(double x, double y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Конструктор класса по 2-м точкам.
        /// </summary>
        /// <param name="x">1-я точка.</param>
        /// <param name="y">2-я точка..</param>
        public Vector(Point a, Point b) {
            X = b.X - a.X;
            Y = b.Y - a.Y;
        }

        /// <summary>
        /// Сложение соответствующих координат 2-х векторов.
        /// </summary>
        /// <param name="first">1-й вектор.</param>
        /// <param name="second">2-й вектор.</param>
        /// <returns>Сумму векторов.</returns>
        public static Vector operator +(Vector first, Vector second) => new(first.X + second.X, first.Y + second.Y);

        /// <summary>
        /// Сложение соответствующих координат вектора и точки.
        /// </summary>
        /// <param name="first">Точка.</param>
        /// <param name="second">Вектор.</param>
        /// <returns>Сумму вектора и точки.</returns>
        public static Point operator +(Point first, Vector second) => new(first.X + second.X, first.Y + second.Y);

        /// <summary>
        /// Вычитание соответствующих координат 2-х векторов.
        /// </summary>
        /// <param name="first">1-й вектор.</param>
        /// <param name="second">2-й вектор.</param>
        /// <returns>Разность векторов.</returns>
        public static Vector operator -(Vector first, Vector second) => new(first.X - second.X, first.Y - second.Y);

        /// <summary>
        /// Сложение соответствующих координат вектора и точки.
        /// </summary>
        /// <param name="first">Точка.</param>
        /// <param name="second">Вектор.</param>
        /// <returns>Разность точки и вектора.</returns>
        public static Point operator -(Point first, Vector second) => new(first.X - second.X, first.Y - second.Y);

        /// <summary>
        /// Умножение координат вектора на коэффицент.
        /// </summary>
        /// <param name="first">Вектор.</param>
        /// <param name="second">Коэффицент.</param>
        /// <returns>Произведение вектора и коэффицента.</returns>
        public static Vector operator *(Vector vector, double coef) => new(vector.X * coef, vector.Y * coef);

        /// <summary>
        /// Поворот вектора на угол.
        /// </summary>
        /// <param name="angle">Угол в радианах.</param>
        /// <returns>Повернутый вектор.</returns>
        public Vector Rotate(double angle) {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new(X * cos - Y * sin, X * sin + Y * cos);
        }

        /// <summary>
        /// Точка их координат конца вектора.
        /// </summary>
        /// <returns>Точка конца вектора.</returns>
        public Point ToPoint() {
            return new(X, Y);
        }
    }
}
