using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public void UpdateQuality()
        {
            // Update non-unique rarity items
            UpdateNonUniqueItemsQuality();

            // Update unique non-rare items
            UpdateNonUniqueNonRareItemsQuality();

            // Update rare items based on their sell-in timer
            UpdateRareItemsQuality();

            // Decrease sell-in for all items
            Items.ForEach(item =>
            {
                if (item.IsNonRare)
                {
                    item.SellIn = Math.Max(item.SellIn - 1, 0);
                }
            });
        }

        private void UpdateNonUniqueItemsQuality()
        {
            var uniqueRarities = new HashSet<string> { "Aged Brie", "Backstage passes to a TAFKAL80ETC concert" };
            foreach (var item in Items)
            {
                if (!uniqueRarities.Contains(item.Name))
                {
                    if (item.Quality > 0)
                    {
                        item.Quality -= 1;
                        if (item.Quality < 1)
                        {
                            item.Quality = 0;
                        }
                    }
                }
            }
        }

        private void UpdateNonUniqueNonRareItemsQuality()
        {
            foreach (var item in Items)
            {
                if (item.Quality < 50 && !item.IsUniqueRare)
                {
                    item.Quality += 1;

                    if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (item.SellIn < 11)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }
                        }

                        if (item.SellIn < 6)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }
                        }
                    }
                }
            }
        }

        private void UpdateRareItemsQuality()
        {
            foreach (var item in Items)
            {
                if (!item.IsUniqueRare)
                {
                    if (item.SellIn < 0 && item.Quality > 0)
                    {
                        if (item.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            item.Quality -= item.Quality;

                            if (item.Quality < 1)
                            {
                                item.Quality = 0;
                            }
                        }
                    }
                    if (item.Name == "Aged Brie")
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality += 1;
                        }
                    }
                }
            }
        }

        private bool IsNonRare(Item item)
        {
            return item.Name != "Sulfuras, Hand of Ragnaros" && item.Quality > 0;
        }
    }
}