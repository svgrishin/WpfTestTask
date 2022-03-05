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
            //float s = TextRenderer.MeasureText(label1.Text, label1.Font).Width;
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
        }
    }
}
