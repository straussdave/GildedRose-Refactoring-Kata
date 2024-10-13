using System.Collections.Generic;

namespace GildedRoseKata
{
    public enum ItemType
    {
        Normal,
        AgedBrie,
        BackstagePass,
        Sulfuras
    }

    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }
        public ItemType Type { get; set; }
    }

    public class GildedRose
    {
        private IList<Item> items;

        public GildedRose(IList<Item> items)
        {
            this.items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in items)
            {
                switch (item.Type)
                {
                    case ItemType.Normal:
                        UpdateNormalItem(item);
                        break;

                    case ItemType.AgedBrie:
                        UpdateAgedBrieItem(item);
                        break;

                    case ItemType.BackstagePass:
                        UpdateBackstagePassItem(item);
                        break;

                    case ItemType.Sulfuras:
                        break; // Sulfuras, Hand of Ragnaros does not change in quality

                }
            }
        }

        private static void UpdateNormalItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.SellIn < 0
                 ? item.Quality - 2
                  : item.Quality - 1;
            }
        }

        private static void UpdateAgedBrieItem(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;
            }
        }

        private static void UpdateBackstagePassItem(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;

                if (item.SellIn < 11)
                {
                    item.Quality < 50 && item.Quality++;
                }

                if (item.SellIn < 6)
                {
                    item.Quality < 50 && item.Quality++;
                }
            }

            item.Quality = item.SellIn < 0 ? 0 : item.Quality;
        }
    }
}