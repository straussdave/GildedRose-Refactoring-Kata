using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata
{
    public class GildedRose
    {
        private readonly IList<Item> _items;

        public GildedRose(IList<Item> items)
        {
            _items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                UpdateQualityBasedOnName(item);
                UpdateSellIn(item);
                UpdateQualityAfterSellIn(item);
            }
        }

        private void UpdateQualityBasedOnName(Item item)
        {
            if (item.IsNormalItem())
            {
                DecreaseQuality(item);
            }
            else if (item.IsAgedBrieOrBackstagePass())
            {
                IncreaseQuality(item, item.IsBackstagePass());
            }
        }

        private void UpdateSellIn(Item item)
        {
            if (!item.IsLegendaryItem())
            {
                item.SellIn--;
            }
        }

        private void UpdateQualityAfterSellIn(Item item)
        {
            if (item.SellIn < 0)
            {
                if (item.IsAgedBrieOrBackstagePass())
                {
                    IncreaseQuality(item, item.IsBackstagePass());
                }
                else if (item.IsNormalItem())
                {
                    DecreaseQuality(item);
                }
            }
        }

        private void IncreaseQuality(Item item, bool isBackstagePass = false)
        {
            item.Quality = Clamp(item.Quality + 1, 0, 50);

            if (isBackstagePass && item.SellIn < 11)
            {
                item.Quality = Clamp(item.Quality + 1, 0, 50);
            }

            if (isBackstagePass && item.SellIn < 6)
            {
                item.Quality = Clamp(item.Quality + 1, 0, 50);
            }
        }

        private void DecreaseQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = Clamp(item.Quality - 1, 0, 50);
            }
        }

        private int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }
    }

    public static class ItemExtensions
    {
        public static bool IsNormalItem(this Item item)
        {
            return !(item.IsAgedBrieOrBackstagePass() || item.IsLegendaryItem());
        }

        public static bool IsAgedBrieOrBackstagePass(this Item item)
        {
            return item.Name == "Aged Brie" || item.Name == "Backstage passes to a TAFKAL80ETC concert";
        }

        public static bool IsBackstagePass(this Item item)
        {
            return item.Name == "Backstage passes to a TAFKAL80ETC concert";
        }

        public static bool IsLegendaryItem(this Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros";
        }
    }
}