using Magter_Ikke_at_tælle.converter.implementations;
using Magter_Ikke_at_tælle.converter.interfaces;
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

namespace Magter_Ikke_at_tælle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly StringBuilder SB = new StringBuilder();
        private string input;
        private readonly IConvertTextToItems cvi;
        private static readonly int[] idRange = { 60000, 90000 };
        private const int MaxQty = 200;
        private List<IItem> items;
        public MainWindow()
        {
            cvi = new TextToItemsConverter(idRange, MaxQty);
            InitializeComponent();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            cvi.ResetCount();
            input = InputText.Text;
            if (input.Length > 10)
            {
                items = cvi.ConvertText(input);
            }
            OutputText.Text = ConvertItemsToString();
            TotalCount.Text = cvi.CountOfOrderLines().ToString();
            if (KeepInput.IsChecked == false)
            {
                InputText.Text = "";
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearEverything();
        }

        private string ConvertItemsToString()
        {
            if (SB.Length != 0)
            {
                _ = SB.Clear();
            }
            if (items != null && items.Count != 0)
            {
                string str = "id\tqty\n";
                _ = SB.Append(str);
                foreach (IItem item in items)
                {
                    _ = SB.Append(item.ToString());
                }
            }
            return SB.ToString();
        }

        private void ClearEverything()
        {
            InputText.Text = "";
            OutputText.Text = "";
            TotalCount.Text = "0";
            input = "";
            cvi.ClearItems();
            cvi.ResetCount();
            if (items != null)
            {
                items.Clear();
            }
        }
    }
}
