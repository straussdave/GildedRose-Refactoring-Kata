using System.Collections.Generic;
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
        Assert.AreEqual(0, items[0].SellIn);
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

}