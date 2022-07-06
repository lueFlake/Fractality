using System;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Media;

namespace FractalsLib {
    /// <summary>
    /// Базовый класс фракталов.
    /// </summary>
    public abstract class BaseFractal {
        /// <summary>
        /// Глубина рекурсии при генерации фрактала.
        /// </summary>
        public abstract int Depth { get; set; }

        /// <summary>
        /// Максимальная глубина рекурсии при генерации фрактала.
        /// </summary>
        public abstract int MaxDepth { get; protected set; }

        /// <summary>
        /// Градиент и цвет фрактала.
        /// </summary>
        public abstract Gradient Color { get; protected set; }

        /// <summary>
        /// Холст для рисования.
        /// </summary>
        public static Canvas? DrawingArea { get; set; }

        /// <summary>
        /// Вывод фрактала на холст.
        /// </summary>
        public abstract void Render();

        public abstract void ChangeColor(Color firstColor, Color secondColor);

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="depth">Глубина рекурсии.</param>
        protected BaseFractal(int depth) {
            Depth = depth;
        }
    }
}