using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;

    private readonly IList<string> _legendaryItems = new List<string> { "Sulfuras, Hand of Ragnaros" };

    private const string _backStageString = "Backstage passes to a TAFKAL80ETC concert";
    private const string _brieString = "Aged Brie";

    private readonly int _maxQuality = 50;
    private readonly int _minQuality = 0;

    public GildedRose(IList<Item> items)
    {
        _items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            if (_legendaryItems.Contains(item.Name))
            {
                continue;
            }
            
            ReduceItemSellIn(item);

            ChangeQuality(item);
        }
    }

    private void ChangeQuality(Item item)
    {
        var itemName = item.Name;
        switch (itemName)
        {
            case _brieString:
                UpdateBrie(item);
                break;
            case _backStageString:
                UpdateBackstagePass(item);
                break;
            default:
                UpdateDefaultItem(item);
                break;
        }
    }

    private void UpdateDefaultItem(Item item)
    {
        var qualityChange = -1;
        if(item.SellIn < 0)
        {
            qualityChange *= 2;
        }
        if (item.Name.Contains("Conjured"))
        {
            qualityChange *= 2;
        }
        AddQuality(item, qualityChange);
    }

    private void UpdateBackstagePass(Item item)
    {
        var qualityChange = 0;
        var sellIn = item.SellIn;
        if (sellIn < 0)
        {
            item.Quality = 0;
            return;
        }
        if (sellIn >= 0)
        {
            qualityChange++;
        }
        if(sellIn <= 10)
        {
            qualityChange++;
        }
        if (sellIn <= 5)
        {
            qualityChange++;
        }
        if(qualityChange != 0)
        {
            AddQuality(item, qualityChange);
        }
    }

    private void UpdateBrie(Item item)
    {
        var qualityChange = 1;
        if(item.SellIn < 0) 
        { 
            qualityChange++; 
        }
        AddQuality(item, qualityChange);
    }

    private void ReduceItemSellIn(Item item)
    {
        item.SellIn--;
    }

    private void AddQuality(Item item, int qualityChange)
    {
        var newQuality = item.Quality + qualityChange;
        if(newQuality > _maxQuality)
        {
            newQuality = _maxQuality;
        }
        if(newQuality < _minQuality)
        {
            newQuality = _minQuality;
        }
        item.Quality = newQuality;
    }
}