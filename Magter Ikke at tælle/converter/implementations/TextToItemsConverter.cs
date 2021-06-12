﻿using Magter_Ikke_at_tælle.converter.interfaces;
using System;
using System.Collections.Generic;

namespace Magter_Ikke_at_tælle.converter.implementations
{
    public class TextToItemsConverter : IConvertTextToItems
    {
        private readonly int[] idRange;
        private readonly int maxQty;
        private readonly IItemMapper itemMapper;
        private int orderLineCounter = 0;
        private bool foundId = false;
        private bool foundQty = false;
        private int tempItemId = 0;
        private int tempItemQty = 0;

        public TextToItemsConverter(int[] idRange, int maxQty)
        {
            this.idRange = idRange;
            this.maxQty = maxQty;
            itemMapper = new ItemMapper();
        }

        public List<IItem> ConvertTextToItems(string str)
        {
            string[] splStrings = str.Split();
            _ = str;
            for (int i = 0; i < splStrings.Length; i++)
            {
                string singleStr = splStrings[i];
                if (singleStr.Length == 5 && foundId == false)
                {
                    if (InputIsAnInt(singleStr))
                    {
                        int id = int.Parse(singleStr);
                        if (id > idRange[0] && id < idRange[1])
                        {
                            tempItemId = id;
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
                            tempItemQty = qty;
                            foundQty = true;
                        }
                    }
                }
                if (foundId && foundQty)
                {
                    IItem item = new Item(tempItemId, tempItemQty);
                    string name = TakeName(splStrings, i);
                    if (name.Length > 0)
                    {
                        item.Name = name;
                    }
                    AddItemAndResetTempItemInfo(item);
                }
            }
            List<IItem> items = itemMapper.GetItems();
            itemMapper.Clear();
            return items;
        }

        private void AddItemAndResetTempItemInfo(IItem item)
        {
            _ = itemMapper.AddItem(item);
            tempItemId = 0;
            tempItemQty = 0;
            foundId = false;
            foundQty = false;
            orderLineCounter++;
        }

        private string TakeName(string[] strs, int i)
        {
            string name;
            try
            {
                name = strs[i + 1];
            }
            catch (IndexOutOfRangeException e)
            {
                _ = e;
                name = "";
            }
            return name;
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
            ResetCount();
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
