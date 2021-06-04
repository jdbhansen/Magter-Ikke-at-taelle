using Magter_Ikke_at_tælle.converter.interfaces;
using System;
using System.Collections.Generic;

namespace Magter_Ikke_at_tælle.converter.implementations
{
    public class TextToItemConverter : IConvertTextToItems
    {
        private readonly int[] idRange;
        private readonly int maxQty;
        private readonly IItemMapper itemMapper;
        private int orderLineCounter = 0;

        public TextToItemConverter(int[] idRange, int maxQty)
        {
            this.idRange = idRange;
            this.maxQty = maxQty;
            itemMapper = new ItemMapper();
        }

        public List<IItem> ConvertText(string str)
        {
            bool foundId = false;
            bool foundQty = false;
            int itemId = 0;
            int itemQty = 0;
            string[] splStrings = str.Split();
            for (int k = 0; k < splStrings.Length; k++)
            {
                string singleStr = splStrings[k];
                if (singleStr.Length == 5 && foundId == false)
                {
                    if (InputIsAnInt(singleStr))
                    {
                        int id = int.Parse(singleStr);
                        if (id > idRange[0] && id < idRange[1])
                        {
                            itemId = id;
                            foundId = true;
                        }
                    }
                }
                if (foundId && foundQty == false && DoesStringContainDotOrComma(singleStr))
                {
                    string curStr = RemoveDotOrComma(singleStr);
                    if (InputIsAnInt(curStr))
                    {
                        int qty = int.Parse(curStr);
                        if (qty < maxQty)
                        {
                            itemQty = qty;
                            foundQty = true;
                        }
                    }
                }
                if (foundId && foundQty)
                {
                    IItem item = new Item(itemId, itemQty);
                    _ = itemMapper.AddItem(item);
                    itemId = 0;
                    itemQty = 0;
                    foundId = false;
                    foundQty = false;
                    orderLineCounter++;
                }
            }
            List<IItem> items = itemMapper.GetItems();
            itemMapper.Clear();
            return items;
        }



        private string RemoveDotOrComma(string str)
        {
            if (DoesStringContainDot(str))
            {
                return str.Substring(0, str.IndexOf("."));
            }
            if (DoesStringContainComma(str))
            {
                return str.Substring(0, str.IndexOf(","));
            }
            return "does not have dot or comma";
        }

        private bool DoesStringContainComma(string str)
        {
            string comma = ",";
            return str.Contains(comma);
        }

        private bool DoesStringContainDot(string str)
        {
            string dot = ".";
            return str.Contains(dot);
        }

        private bool DoesStringContainDotOrComma(string str)
        {
            return DoesStringContainDot(str) || DoesStringContainComma(str);
        }

        private bool InputIsAnInt(string str)
        {
            if (str.Length == 0)
            {
                return false;
            }

            try
            {
                _ = int.Parse(str);
                return true;
            }
            catch (FormatException e)
            {
                _ = e;
                return false;
            }
        }

        public void ClearItems()
        {
            orderLineCounter = 0;
            itemMapper.Clear();
        }

        public int CountOfOrderLines()
        {
            return orderLineCounter;
        }

        public void ResetCount()
        {
            orderLineCounter = 0;
        }

    }
}
