using Magter_Ikke_at_tælle.converter.implementations;
using Magter_Ikke_at_tælle.converter.interfaces;
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

namespace Magter_Ikke_at_tælle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly IStringMaker StrMaker = new StringMaker();
        private readonly IConvertTextToItems cvi;
        private static readonly int[] idRange = { 60000, 90000 };
        private const int MaxQty = 200;
        private string input;
        public MainWindow()
        {
            cvi = new TextToItemsConverter(idRange, MaxQty);
            InitializeComponent();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            cvi.ResetCount();
            input = InputText.Text;

            if (SeeStrings.IsChecked == true)
            {
                OutputText.Text = StrMaker.ReformatStringsToLines(input);
                StrMaker.Clear();
            }

            if (input.Length > 10 && SeeStrings.IsChecked == false)
            {
                OutputText.Text = StrMaker.ConvertItemsToString(cvi.ConvertText(input));
                StrMaker.Clear();
                TotalCount.Text = cvi.CountOfOrderLines().ToString();
            }


            if (KeepInput.IsChecked == false)
            {
                InputText.Text = "";
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearEverything();
        }

        private void ClearEverything()
        {
            InputText.Text = "";
            OutputText.Text = "";
            TotalCount.Text = "0";
            input = "";
            cvi.ClearItems();
            cvi.ResetCount();
        }
    }
}
