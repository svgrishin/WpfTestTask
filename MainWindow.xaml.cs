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
using Newtonsoft.Json;
using System.IO;

namespace WpfTestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool sizeFlag = false;

        /// <summary>
        /// Объект калькулятора
        /// </summary>
        public Calculator calc = new Calculator();

        /// <summary>
        /// Массив с калькуляторами для хранения объектов с различными состояниями
        /// </summary>
        public Calculator[] calcs = new Calculator[0];

        /// <summary>
        /// Форма с историей вычислений
        /// </summary>
        private readonly HistoryForm hf;

        /// <summary>
        /// Форма отображения памяти калькулятора
        /// </summary>
        private readonly MRForm mf;

        public MainWindow()
        {
            InitializeComponent();
            mf = new MRForm(this);
            hf = new HistoryForm(this);
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            inputVal(((Button)sender).Content.ToString()[0]);
        }

        /// <summary>
        /// Ввод символа
        /// </summary>
        /// <param name="c">Вводимый символ</param>
        private void inputVal(char c)
        {
            label1.Content = calc.inputValues(c);
            setTextSize();
        }

        private void btn_Zero_Click(object sender, RoutedEventArgs e)
        {
            if (calc.arg == "") typeZeroComa();
            else label1.Content = calc.inputValues('0');
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            resetCalc();
            Array.Clear(calc.args, 0, 1);
            label1.Content = "0";
        }

        private void btn_bspace_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = calc.deleteSymbol();
            setTextSize();
        }

        private void btn_Negative_Click(object sender, RoutedEventArgs e)
        {
            calc.minus = !calc.minus;
            inputVal('-');
        }

        private void btn_Coma_Click(object sender, RoutedEventArgs e)
        {
            if (calc.arg.Contains(',') == false)
            {
                if (calc.arg == "")
                {
                    typeZeroComa();
                }
                else label1.Content = calc.inputValues(',');
            }
        }

        /// <summary>
        /// Ввод "0,"
        /// </summary>
        public void typeZeroComa()
        {
            label1.Content = calc.inputValues('0');
            label1.Content = calc.inputValues(',');
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

        /// <summary>
        /// Нажатие одной из функций
        /// </summary>
        /// <param name="f">Делегат функции выполнения</param>
        /// <param name="isExtraFunc">Флаг функции одного аргумента</param>
        private void btn_Func_Click(Calculator.funcDeleg f, bool isExtraFunc)
        {
            if (isExtraFunc == true)
            {
                calc.extraFunc(f);
                label1.Content = calc.disp;
                saveStatus();
                setTextSize();
            }
            else funcClick(f);
        }

        private void btn_plus_Click(object sender, RoutedEventArgs e)
        {
            btn_click("+", new Calculator.CalcFunction().Summ, false);
        }

        private void btn_multiply_Click(object sender, RoutedEventArgs e)
        {
            btn_click("×", new Calculator.CalcFunction().multiply, false);
        }

        private void btn_divide_Click(object sender, RoutedEventArgs e)
        {
            btn_click("÷", new Calculator.CalcFunction().divide, false);
        }

        private void btn_minus_Click(object sender, RoutedEventArgs e)
        {
            btn_click("-", new Calculator.CalcFunction().differens, false);
        }

        private void btn_SQRT_Click(object sender, RoutedEventArgs e)
        {
            calc.symbol = "√";
            calc.fDeleg = new Calculator.CalcFunction().sqrtOf;
            btn_Func_Click(calc.fDeleg, true);
        }

        private void btn_SQR_Click(object sender, RoutedEventArgs e)
        {
            calc.symbol = "^";
            calc.fDeleg = new Calculator.CalcFunction().sqrOf;
            btn_Func_Click(calc.fDeleg, true);
        }

        private void btn_Fraction_Click(object sender, RoutedEventArgs e)
        {
            calc.symbol = "1/";
            calc.fDeleg = new Calculator.CalcFunction().fraction;
            btn_Func_Click(calc.fDeleg, true);
        }

        /// <summary>
        /// Выполнение заданной функции
        /// </summary>
        /// <param name="f">Делегат заданной функции</param>
        private void funcClick(Calculator.funcDeleg f)
        {
            calc.resBtnFlag = false;

            switch (calc.index)
            {
                case false:
                    {
                        calc.fDeleg = f;
                        calc.tryToGetArg(calc.arg);
                        calc.funcFlag = true;
                        break;
                    }
                case true:
                    {
                        calc.tryToGetArg(calc.arg); ;
                        getResult(calc.fDeleg);
                        calc.funcFlag = true;
                        calc.fDeleg = f;
                        break;
                    }
            }

            label1.Content = calc.disp;
            calc.minus = false;
        }

        private void btn_click(string s, Calculator.funcDeleg f, bool isExtraFunc)
        {
            calc.symbol = s;

            if (calc.funcFlag == true)
            {
                calc.fDeleg = f;
                calc.resBtnFlag = false;
            }
            else
            {
                btn_Func_Click(f, isExtraFunc);
                setTextSize();
            }
        }

        private void btn_Result_Click(object sender, RoutedEventArgs e)
        {
            calc.tryToGetArg(calc.arg);

            calc.resBtnFlag = true;
            calc.funcFlag = true;

            calc.getResult(calc.fDeleg);

            label1.Content = calc.disp;

            saveStatus();

            setTextSize();
        }

        private void btn_MList_Click(object sender, RoutedEventArgs e)
        {
            mf.Left = Left + (Width - hf.Width) / 2;
            mf.Top = Top + (Height - hf.Height) / 2;
            mf.Show();
            IsEnabled = false;
        }

        public void setMR(int indexOf, int negative)
        {
            try
            {
                calc.mr[indexOf] += Convert.ToDouble(calc.arg) * negative;
            }
            catch
            {
                calc.mr[indexOf] = calc.args[0];
                calc.arg = calc.mr[indexOf].ToString();
            }

            setMrList(indexOf);

            calc.funcFlag = true;
            calc.resBtnFlag = true;

            btn_MList.IsEnabled = true;

            calc.mrFlag = true;
        }

        public void setMrList(int indexOf)
        {
            try
            {
                mf.listBox_MR.Items[calc.mr.Length - 1] = calc.mr[indexOf];
            }
            catch
            {
                mf.listBox_MR.Items.Add(calc.mr[indexOf]);
            }
        }

        public void switchMRButtons()
        {
            btn_MR.IsEnabled = false;
            btn_MC.IsEnabled = false;
        }

        /// <summary>
        /// Получение аргумента из памяти
        /// </summary>
        /// <param name="indexOf">индекс аргумента в массиве памяти</param>
        public void getFromMR(int indexOf)
        {
            calc.arg = calc.mr[indexOf - 1].ToString();
            if (calc.mr[indexOf - 1] < 0) calc.minus = true;
            calc.disp = calc.displayOut(calc.arg);

            label1.Content = calc.disp;
        }

        private void listBox_MR_DoubleClick(object sender, EventArgs e)
        {
            getFromMR(mf.listBox_MR.SelectedIndex + 1);
            calc.resBtnFlag = true;
        }

        /// <summary>
        /// Инициатор сохранения статуса калькулятора
        /// </summary>
        public void saveMe()
        {
            addCalc();
            try
            {
                saveCalc();
            }
            catch { this.Content = "Не удалось сохранить в файл"; }
        }

        /// <summary>
        /// Загрузка калькулятора
        /// </summary>
        /// <param name="i">Индекс загружаемого калькулятора из массива калькуляторов></param>
        public void loadMe(int i)
        {
            calc = new Calculator(calcs[i]);
            calc.resultString = "";

            label1.Content = calc.displayOut(calc.disp);

            resetCalc();

            calc.arg = calc.args[0].ToString();
        }

        /// <summary>
        /// Инициатор сброса калькулятора
        /// </summary>
        public void resetCalc()
        {
            calc.ResetCalc();
            label1.FontSize = 30;
        }

        /// <summary>
        /// Пополнение массива состояний новым состоянием калькулятора
        /// </summary>
        private void addCalc()
        {
            int i = calcs.Length;
            Array.Resize(ref calcs, i + 1);
            calcs[calcs.Length - 1] = new Calculator(calc);
        }

        /// <summary>
        /// Сохранение состояния калькулятора в файл
        /// </summary>
        private void saveCalc()
        {
            Calculator c = new Calculator(calc);

            c.fDeleg = null;

            string s = JsonConvert.SerializeObject(c);

            string location = Directory.GetCurrentDirectory() + "/calc.json";
            File.AppendAllText(location, s + "\n");
        }

        /// <summary>
        /// Добавление в список вычисления целиком
        /// </summary>
        /// <param name="c">Калькулятор</param>
        private void addToCalcList(Calculator c)
        {
            Combobox1.Items.Add(c.resultString);
        }

        /// <summary>
        /// Инициатор получения результата
        /// </summary>
        /// <param name="cf">Делегат функции</param>
        private void getResult(Calculator.funcDeleg cf)
        {
            calc.getResult(cf);
            saveStatus();
        }

        /// <summary>
        /// Фиксация этапа вычисления
        /// </summary>
        private void saveStatus()
        {
            saveMe();
            if (calc.resultString != "") addToCalcList(calc);
            calc.resultString = "";
        }

        private void Combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadMe(Combobox1.SelectedIndex);
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
            btn_Percent.FontSize = fSize;
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

        private void btn_M_Click(object sender, RoutedEventArgs e)
        {
            showForm(mf);
        }

        private void btn_History_Click(object sender, RoutedEventArgs e)
        {
            string location = Directory.GetCurrentDirectory() + "/calc.json";
            string[] s = File.ReadAllLines(location);
            int i = 0;
            foreach (string str in s)
            {
                Array.Resize(ref calcs, i + 1);
                calcs[i] = JsonConvert.DeserializeObject<Calculator>(str);
                hf.HistoryList.Items.Add(calcs[i].resultString);
                i++;
            }

            hf.Left = Left + (Width - hf.Width) / 2;
            hf.Top = Top + (Height - hf.Height) / 2;
            IsEnabled = false;
            //hf.Show();

            showForm(hf);
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

        private void btn_MPlus_Click_1(object sender, RoutedEventArgs e)
        {
            setMR(calc.mr.Length - 1, 1);

            calc.mrFlag = true;

            btn_MC.IsEnabled = true;
            btn_MR.IsEnabled = true;
        }

        private void btn_MC_Click_1(object sender, RoutedEventArgs e)
        {
            calc.mr = new double[1];
            btn_MList.IsEnabled = false;
            mf.listBox_MR.Items.Clear();
            switchMRButtons();
        }

        private void btn_MR_Click_1(object sender, RoutedEventArgs e)
        {
            getFromMR(calc.mr.Length - 1);
            label1.Content = calc.arg;

            calc.mrFlag = true;
        }

        private void btn_MMinus_Click_1(object sender, RoutedEventArgs e)
        {
            setMR(calc.mr.Length - 1, -1);
            calc.mrFlag = true;

            btn_MC.IsEnabled = true;
            btn_MR.IsEnabled = true;
        }

        private void btn_MS_Click_1(object sender, RoutedEventArgs e)
        {
            int l = calc.mr.Length - 1;

            if (calc.mr.Length > 0)
            {
                Array.Resize(ref calc.mr, l + 2);
                l++;
            }
            setMR(l, 1);

            calc.mrFlag = true;

            btn_MC.IsEnabled = true;
            btn_MR.IsEnabled = true;
        }

        private void btn_Percent_Click(object sender, RoutedEventArgs e)
        {
            calc.arg = "";
            calc.disp = "0";
            label1.Content = calc.disp;
            label1.FontSize = 50;
        }
    }
}
