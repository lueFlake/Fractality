using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FractalsLib {
    /// <summary>
    /// Класс для градиента и цвета фракталов.
    /// </summary>
    public class Gradient {

        /// <summary>
        /// Цвета линейного градиента.
        /// </summary>
        public List<Color> Colors { get; private set; }
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="begin">Начальный цвет градиента.</param>
        /// <param name="end">Конечный цвет градиента.</param>
        /// <param name="length">Длина градиента.</param>
        public Gradient(Color begin, Color end, int length = 255) {
            Colors = new List<Color>();

            var beginRed = (int)begin.R;
            var beginGreen = (int)begin.G;
            var beginBlue = (int)begin.B;

            var endRed = (int)end.R;
            var endGreen = (int)end.G;
            var endBlue = (int)end.B;

            int differenceRed = (endRed - beginRed) / (length + 1);
            int differenceGreen = (endGreen - beginGreen) / (length + 1);
            int differenceBlue = (endBlue - beginBlue) / (length + 1);


            for (int i = 0; i < length + 1; i++) {
                Colors.Add(Color.FromRgb((byte)(beginRed + differenceRed * i), (byte)(beginGreen + differenceGreen * i), (byte)(beginBlue + differenceBlue * i)));
            }

            Colors.RemoveAt(Colors.Count - 1);
            Colors.Add(end);
        }
    }
}
