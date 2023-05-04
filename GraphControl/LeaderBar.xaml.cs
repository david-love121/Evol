using DongUtility;
using GraphData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GraphControl
{
    /// <summary>
    /// Interaction logic for LeaderBar.xaml
    /// </summary>
    public partial class LeaderBar : UserControl, IUpdating
    {
        public Color Color
        {
            get
            {
                return MyBrush.Color;
            }
            set
            {
                MyBrush.Color = value;
                Title.Foreground = new SolidColorBrush(WPFUtility.UtilityFunctions.HighContrast(value));
            }
        }
        public double BarLength
        {
            get
            {
                return Bar.Width / (ActualWidth * .8);
            }
            set
            {
                Bar.Width = value > 0 ? value * ActualWidth * .8 : 0;
            }
        }// From 0 to 1
        public string NameOfBar { get { return Title.Text; } set { Title.Text = value; } }
        private double number = 0;
        public double NumberOnRight { get { return number; } set { number = value; SetText(); } }
        public string AlertText;

        private int nUpdates = 0;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            double heightBased = ActualHeight * .75;
            Title.FontSize = ActualHeight * .75;

            double width = WPFUtility.UtilityFunctions.MeasureString(Title.Text, Title).Width;
            double widthBased = (ActualWidth * .8 / width) * .8 * Title.FontSize;

            Title.FontSize = Math.Min(heightBased, widthBased);
            //Number.FontSize = Title.FontSize;

            base.OnRenderSizeChanged(sizeInfo);
        }

        public double SortValue 
        {
            get
            {
                if (string.IsNullOrEmpty(AlertText))
                {
                    return NumberOnRight;
                }
                else
                {
                    var resultString = Regex.Match(AlertText, @"\d+").Value;
                    return double.Parse(resultString) - 1000; // Kind of lame subtraction to keep this smaller than everything else
                }
            }
        }

        private void SetText()
        {
            if (string.IsNullOrEmpty(AlertText))
            {
                Number.Text = number.ToString();
            }
            else
            {
                Number.Text = AlertText;
            }
        }

        public void Update(GraphDataPacket data)
        {
            double value = data.GetData();
            if (TextFunction != null)
                AlertText = TextFunction(value);
            NumberOnRight = value;
        }

        public delegate string AlertTextSetter(double val);
        public AlertTextSetter TextFunction { get; set; } = null;

        public LeaderBar(AlertTextSetter textFunction = null)
        {
            TextFunction = textFunction;
            InitializeComponent();
        }
    }
}
