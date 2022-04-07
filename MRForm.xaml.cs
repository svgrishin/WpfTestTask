using System;
using System.Collections.Generic;

using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfTestTask
{
    /// <summary>
    /// Логика взаимодействия для MRForm.xaml
    /// </summary>
    public partial class MRForm : Window
    {
        MainWindow mainForm;

        public MRForm(MainWindow f)
        {
            mainForm = f;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            mainForm.IsEnabled = true;
            e.Cancel = true;
        }

        private void listBox_MR_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mainForm.getFromMR(listBox_MR.SelectedIndex + 1);
            Hide();
            mainForm.IsEnabled = true;
        }
    }
}
