using Arena;
using ArenaVisualizer;
using NeuralNet;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PredatorPreyVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainArenaVisualizer Arena;
        private PredatorPreyEngine engine;

        public MainWindow(double xSize, double ySize, double timeStep, Perceptron perceptron)
        {
            engine = new PredatorPreyEngine(xSize, ySize, perceptron);
            Title = "Predator / Prey";
            var display = new ArenaVisualizerStandalone(engine);
            Arena = new MainArenaVisualizer(engine, display);
            InitializeComponent();
            Arena.TimeIncrement = timeStep;

            Arena.SlowDraw = true;

            ArenaSpot.Content = Arena.Content;

            ContentRendered += WireUpDisplay;
            ContentRendered += Arena.LinkManager;

            Arena.UpdatedTime += WhenUpdatedTime;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        private void WhenUpdatedTime(object sender, EventArgs e)
        {
            TimeText.Text = Math.Round(Arena.DisplayTime, 2).ToString() + " s";
        }

        private void WireUpDisplay(object sender, EventArgs e)
        {
            Arena.Visualizer = Arena.Display;
            InvalidateVisual();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Arena.IsRunning)
            {
                Start_Button.Content = "Resume";
                Arena.IsRunning = false;
            }
            else
            {
                Start_Button.Content = "Pause";
                Arena.IsRunning = true;

                Arena.StartAll();
            }
        }

        private void TimeIncrementSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            Arena.TimeIncrement = double.Parse(TimeIncrementSlider.Text);
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            bool needToRestart = false;
            if (Arena.IsRunning)
            {
                Arena.IsRunning = false;
                needToRestart = true;
            }

            WPFUtility.UtilityFunctions.SaveScreenshot((int)ActualWidth, (int)ActualHeight, this);

            if (needToRestart)
            {
                Arena.IsRunning = true;
            }
        }

        private void Clipboard_Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetImage(WPFUtility.UtilityFunctions.MakeScreenshot((int)ActualWidth,
                (int)ActualHeight, this));
        }
    }
}
