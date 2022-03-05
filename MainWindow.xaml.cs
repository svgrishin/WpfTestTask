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

namespace WpfTestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #pragma warning disable IDE0044 // Добавить модификатор только для чтения
        private readonly MRForm mrForm;
        private readonly HistoryForm HF;
        bool sizeFlag = false;
        public MainWindow()
        {
            InitializeComponent();
            mrForm = new MRForm(this);
            HF = new HistoryForm(this);
        }

        public void setTextSize()
        {
            double x = 0.6;
            
            double fSize = label1.FontSize;
            string str = label1.Content.ToString();
            double s = fSize * str.Length*x;
            double w = Width;
            if (s > w)
            {
                while (w < s)
                {
                    try
                    {
                        fSize--;
                        label1.FontSize = fSize;
                        s = fSize * str.Length * x;
                    }
                    catch { }

                }
            }
            else

                while (w > s + fSize && label1.FontSize < 50)
                {
                    fSize++;
                    label1.FontSize=fSize;
                    s = fSize * str.Length * x;
                }
        }

        private void setFonSize(float fSize)
        {
            btn_MR.FontSize = fSize;
            btn_MS.FontSize = fSize;
            btn_MC.FontSize = fSize;
            btn_MR.FontSize = fSize;
            btn_MPlus.FontSize = fSize;
            btn_MMinus.FontSize = fSize;
            //button1.FontSize = fSize;
            btn_History.FontSize = fSize;
            //button2.FontSize = fSize;
            btn_Result.FontSize = fSize;
            btn_Plus.FontSize = fSize;
            btn_Minus.FontSize = fSize;
            btn_Negative.FontSize = fSize;
            btn_Coma.FontSize = fSize;
            btn_1.FontSize = fSize;
            btn_0.FontSize = fSize;
            btn_2.FontSize = fSize;
            btn_7.FontSize = fSize;
            btn_3.FontSize = fSize;
            btn_8.FontSize = fSize;
            btn_6.FontSize = fSize;
            btn_4.FontSize = fSize;
            btn_5.FontSize = fSize;
            btn_Divide.FontSize = fSize;
            btn_Multiply.FontSize = fSize;
            btn_Backspase.FontSize = fSize;
            btn_SQR.FontSize = fSize;
            btn_SQRT.FontSize = fSize;
            btn_C.FontSize = fSize;
            btn_9.FontSize = fSize;
            btn_CE.FontSize = fSize;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

        }

        private void btn_M_Click(object sender, RoutedEventArgs e)
        {
            showForm(mrForm);
        }

        private void btn_History_Click(object sender, RoutedEventArgs e)
        {
            showForm(HF);
        }

        private void showForm(Window f)
        {
            f.Left = Left + ((Width - f.Width) / 2);
            f.Top = Top + ((Height - f.Height) / 2);
            f.Show();

            this.IsEnabled = false;
        }

        public void closingForm(Window f, System.ComponentModel.CancelEventArgs e)
        {
            IsEnabled = true;
            f.Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            setTextSize();

            bool flag = sizeFlag;

            if (Height >= 400) setTextSize();

            if (Height > 650 && Width > 350) sizeFlag = true;
            else sizeFlag = false;

            if (flag != sizeFlag)
            {
                switch (sizeFlag)
                {
                    case true: setFonSize(20); break;
                    case false: setFonSize(12); break;
                }
            }
        }
    }
}
