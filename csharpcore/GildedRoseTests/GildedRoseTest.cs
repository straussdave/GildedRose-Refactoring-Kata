﻿using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using GildedRoseKata;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    //All `items` have a `SellIn` value which denotes the number of days we have to sell the `items`
    //All `items` have a `Quality` value which denotes how valuable the item is
    [Test]
    public void GetSetBaseValues()
    {
        var items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        Assert.AreEqual("foo", items[0].Name);
        Assert.AreEqual(0, items[0].SellIn);
        Assert.AreEqual(0, items[0].Quality);
    }

    //At the end of each day our system lowers both values for every item
    [Test]
    public void LowerValues()
    {
        var items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 1 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].SellIn);
        Assert.AreEqual(0, items[0].Quality);
    }

    //Once the sell by date has passed, `Quality` degrades twice as fast
    [Test]
    public void DegradeQualityTwiceAsFast()
    {
        var items = new List<Item> { new Item { Name = "foo", SellIn = -1, Quality = 3 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(1, items[0].Quality);
    }

    //The `Quality` of an item is never negative
    [Test]
    public void QualityNeverNegative()
    {
        var items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].Quality);
    }

    //__"Aged Brie"__ actually increases in `Quality` the older it gets
    [Test]
    public void AgedBrieRule()
    {
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 0 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(1, items[0].Quality);
    }

    //The `Quality` of an item is never more than `50`
    [Test]
    public void QualityNeverOverFifty()
    {
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 50 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(50, items[0].Quality);
    }

    //__"Sulfuras"__, being a legendary item, never has to be sold or decreases in `Quality`
    [Test]
    public void SulfurasRule()
    {
        var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(80, items[0].Quality);
        Assert.AreEqual(1, items[0].SellIn);
    }

    //__"Backstage passes"__, like aged brie, increases in `Quality` as its `SellIn` value approaches;
    [Test]
    public void BackstagePassIncreaseQualityRule()
    {
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 50, Quality = 10 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(11, items[0].Quality);
    }

    //`Quality` increases by `2` when there are `10` days or less and by `3` when there are `5` days or less
    [Test]
    public void BackstagePassIncreaseQualityFasterRule()
    {
        var items = new List<Item> {
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 10 }
        };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(12, items[0].Quality);
        Assert.AreEqual(13, items[1].Quality);
    }

    //`Quality` drops to `0` after the concert
    [Test]
    public void BackstagePassQualityToZeroRule()
    {
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 10 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].Quality);
    }

    //__"Conjured"__ items degrade in `Quality` twice as fast as normal items
    [Test]
    public void ConjuredItemsRule()
    {
        var items = new List<Item> { 
            new Item { Name = "Conjured Item", SellIn = 10, Quality = 10 },
            new Item { Name = "Conjured Item", SellIn = -1, Quality = 10 }};
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(8, items[0].Quality);
        Assert.AreEqual(6, items[1].Quality);
    }
}