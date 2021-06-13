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
        private readonly IConvertTextToItems CTI;
        private static readonly int[] idRange = { 60000, 90000 }; //burde ikke være readonly; er det kun indtil funktionaliteten findes.
        private const int MaxQty = 200;
        private string input;
        public MainWindow()
        {
            CTI = new TextToItemsConverter(idRange, MaxQty);
            InitializeComponent();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            CTI.ResetCount();
            input = InputText.Text;
            if (SeeStrings.IsChecked == true)
            {
                SetOutputTextToReformatedString(input);
            }

            if (input.Length > 10 && SeeStrings.IsChecked == false && DoesNotKnow.IsSelected)
            {
                SetOutputTextToItemsAndCurrentCount(input);
            }

            if (input.Length > 10 && SeeStrings.IsChecked == false && TabSeperator.IsSelected)
            {
                string indexStrs = Indexes.Text.Trim();
                string[] coordsStrs = indexStrs.Split(',');
                int[] coords = { 0, 0, 0 };
                for (int i = 0; i < coordsStrs.Length; i++)
                {
                    coords[i] = int.Parse(coordsStrs[i]);
                }
                SetOutputTextToNewItemsAndCurrentCount(input, coords);
            }

            if (KeepInput.IsChecked == false)
            {
                SetInputTextToEmpty();
            }
        }

        private async void SetOutputTextToNewItemsAndCurrentCount(string input, int[] coords)
        {
            DisableInput();
            TotalCount.Text = "Loading";
            OutputText.Text = "Loading";
            List<IItem> items = new List<IItem>();
            Task task = Task.Run(() =>
            {
                items = CTI.ConvertTextToItemsFromTabbedStringLines(input, coords);
            });
            await task;
            if (QuantityBox.IsSelected)
            {
                OutputText.Text = StrMaker.ConvertItemsToString(SortItems(items, SortType.Quantity));
            }
            else if (IdBox.IsSelected)
            {

                OutputText.Text = StrMaker.ConvertItemsToString(SortItems(items, SortType.Id));
            }
            StrMaker.Clear();
            TotalCount.Text = CTI.CountOfOrderLines().ToString();
            EnableInput();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearEverything();
        }

        /*
         * Helperfunctions:
         */

        private void SetOutputTextToReformatedString(string input)
        {
            OutputText.Text = StrMaker.ReformatStringsToLines(input);
            StrMaker.Clear();
        }

        private void SetInputTextToEmpty()
        {
            InputText.Text = "";
        }



        private async void SetOutputTextToItemsAndCurrentCount(string input)
        {
            DisableInput();
            TotalCount.Text = "Loading";
            OutputText.Text = "Loading";
            List<IItem> items = new List<IItem>();
            Task task = Task.Run(() =>
            {
                items = CTI.ConvertTextToItems(input);
            });
            await task;
            if (QuantityBox.IsSelected)
            {
                OutputText.Text = StrMaker.ConvertItemsToString(SortItems(items, SortType.Quantity));
            }
            else if (IdBox.IsSelected)
            {

                OutputText.Text = StrMaker.ConvertItemsToString(SortItems(items, SortType.Id));
            }
            StrMaker.Clear();
            TotalCount.Text = CTI.CountOfOrderLines().ToString();
            EnableInput();
        }

        private List<IItem> SortItems(List<IItem> items, SortType sortType)
        {
            if (items == null)
            {
                return null;
            }
            switch (sortType)
            {
                case SortType.Id:
                    return SortItemsById(items);
                case SortType.ItemType:
                    return SortItemsByItemType(items);
                case SortType.Quantity:
                    return SortItemsByQuantity(items);
                default:
                    return SortItemsByQuantity(items);
            }
        }

        private void DisableInput()
        {
            mainButton.IsEnabled = false;
            SortBox.IsEnabled = false;
            KeepInput.IsEnabled = false;
            SeeStrings.IsEnabled = false;
        }

        private void EnableInput()
        {
            mainButton.IsEnabled = true;
            SortBox.IsEnabled = true;
            KeepInput.IsEnabled = true;
            SeeStrings.IsEnabled = true;
        }

        private List<IItem> SortItemsById(List<IItem> items)
        {
            items.Sort((x, y) => x.Id.CompareTo(y.Id));
            return items;
        }

        private List<IItem> SortItemsByQuantity(List<IItem> items)
        {
            items.Sort((x, y) => y.Quantity.CompareTo(x.Quantity));
            return items;
        }

        private List<IItem> SortItemsByItemType(List<IItem> items)
        {
            items.Sort((x, y) => x.ItemType.CompareTo(y.ItemType));
            return items;
        }

        private void ClearEverything()
        {
            InputText.Text = "";
            OutputText.Text = "";
            TotalCount.Text = "0";
            input = "";
            CTI.ClearItems();
            CTI.ResetCount();
        }
    }
    public enum SortType : int
    {
        Id,
        Quantity,
        ItemType
    }
}
