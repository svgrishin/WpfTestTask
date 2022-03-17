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
using System.Windows.Shapes;

namespace WpfTestTask
{
    /// <summary>
    /// Логика взаимодействия для HistoryForm.xaml
    /// </summary>
    public partial class HistoryForm : Window
    {
        MainWindow calcForm;
        
        public HistoryForm(MainWindow f)
        {
            InitializeComponent();
            calcForm = f;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HistoryList.Items.Clear();
            Hide();
            calcForm.IsEnabled = true;
            e.Cancel = true;
        }

        private void HistoryList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            calcForm.loadMe(HistoryList.SelectedIndex);
            calcForm.Combobox1.Items.Add(HistoryList.Items[HistoryList.SelectedIndex]);
            
            Close();
        }
    }
}