﻿using System;
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
        private readonly MainWindow MF;
        public HistoryForm(MainWindow f)
        {
            MF= f;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MF.closingForm(this, e);
        }

    }
}