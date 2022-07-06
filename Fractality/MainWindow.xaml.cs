using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FractalsLib;
using ColorPickerWPF;

namespace Fractality {
    /// <summary>
    /// Класс логики взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private BaseFractal _mainFractal;
        private Color _color1 = Colors.LightGray;
        private Color _color2 = Colors.Black;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            BaseFractal.DrawingArea = DrawingArea;
        }

        /// <summary>
        /// Событие нажатия кнопки.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Аргументы события.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void FractalButton_Click(object sender, RoutedEventArgs e) {
            SubmitButton.Visibility = Visibility.Visible;
            var button = (Button)sender;
            _mainFractal = button.Name switch {
                "WindedFractalTreeButton" => WindedFractalTree.GetInstance(),
                "KochsCurveButton" => KohsCurve.GetInstance(),
                "SerpinskyCarpetButton" => SerpinskyCarpet.GetInstance(),
                "SerpinskyTriangleButton" => SerpinskyTriangle.GetInstance(),
                "KantorsSetButton" => KantorsSet.GetInstance(),
                _ => throw new NotImplementedException("Выбран несуществующий фрактал.")
            };
            DepthSlider.Maximum = _mainFractal.MaxDepth;
            DrawFractal();
        }

        /// <summary>
        /// Вызов вывода соответствующего фрактала.
        /// </summary>
        private void DrawFractal() {
            DrawingArea.Children.Clear();
            GC.Collect();

            if (_mainFractal != null) {
                _mainFractal.Depth = (int)DepthSlider.Value;
            }
            if (_mainFractal is KantorsSet) {
                ((KantorsSet)_mainFractal).Resolution = IntervalSlider.Value;
            }
            if (_mainFractal is WindedFractalTree) {
                ((WindedFractalTree)_mainFractal).Resolution = 2 - LengthCoefficientSlider.Value + 1.5;
                ((WindedFractalTree)_mainFractal).Angle1 = LeftAngleSlider.Value;
                ((WindedFractalTree)_mainFractal).Angle2 = RightAngleSlider.Value;
            }
            _mainFractal?.ChangeColor(_color1, _color2);
            _mainFractal?.Render();
        }

        /// <summary>
        /// Событие кнопки рисования фрактала.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Аргументы события.</param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e) {
            DrawFractal();
        }

        /// <summary>
        /// Событие изменения значения слайдера.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Аргументы события.</param>
        private void IntSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            ((Slider)sender).Value = Math.Floor(((Slider)sender).Value);
        }

        /// <summary>
        /// Событие изменения размеров окна.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Аргументы события.</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            DrawFractal();
        }

        /// <summary>
        /// Событие изменения начального цвета.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Аргументы события.</param>
        private void FirstColorButton_Click(object sender, RoutedEventArgs e) {
            bool ok = ColorPickerWindow.ShowDialog(out _color1);
            DrawFractal();
        }

        /// <summary>
        /// Событие изменения  конечного цвета.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Аргументы события.</param>
        private void LastColorButton_Click(object sender, RoutedEventArgs e) {
            bool ok = ColorPickerWindow.ShowDialog(out _color2);
            DrawFractal();
        }
    }
}
