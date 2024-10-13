using System;
using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        internal IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            this.Items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateSellIn(item);
                UpdateQuality(item);
            }
        }

        private void UpdateSellIn(Item item)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn--;
            }
        }

        private void UpdateQuality(Item item)
        {
            if (item.Quality == 0 || item.Name == "Sulfuras, Hand of Ragnaros") return;

            if (item.Name == "Aged Brie")
            {
                item.Quality = Math.Min(item.Quality + 1, 50);
            }
            else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                HandleBackstageItemsQuality(item);
            }
            else
            {
                item.Quality = Math.Max(item.Quality - 1, 0);
            }

            if (item.SellIn < 0)
            {
                if (item.Name == "Aged Brie")
                {
                    item.Quality = Math.Min(item.Quality + 1, 50);
                }
                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    item.Quality = 0;
                }
                else
                {
                    item.Quality = Math.Max(item.Quality - 1, 0);
                }
            }
        }

        private void HandleBackstageItemsQuality(Item item)
        {
            item.Quality++;

            if (item.SellIn < 11)
            {
                item.Quality++;
            }

            if (item.SellIn < 6)
            {
                item.Quality++;
            }

            item.Quality = Math.Min(item.Quality, 50);
        }
    }
}