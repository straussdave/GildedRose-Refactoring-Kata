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
}