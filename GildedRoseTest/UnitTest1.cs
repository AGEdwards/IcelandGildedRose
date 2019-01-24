using System;
using Xunit;
using GildedRose;
using System.Collections.Generic;

namespace GildedRoseTest
{
    public class UnitTest1
    {
        [Fact]
        public void NormalItemAfterDay()
        {
            Item item = new Item
            {
                SellIn = 10,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(9, item.SellIn);
            Assert.Equal(9, item.Quality);
        }

        [Fact]
        public void NormalItemAfterDayAfterSellIn()
        {
            Item item = new Item
            {
                SellIn = 0,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(-1, item.SellIn);
            Assert.Equal(8, item.Quality);
        }

        [Fact]
        public void AgedBrieAfterDay()
        {
            AgedBrie item = new AgedBrie
            {
                SellIn = 10,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(9, item.SellIn);
            Assert.Equal(11, item.Quality);
        }

        [Fact]
        public void AgedBrieAfterDayAfterSellIn()
        {
            AgedBrie item = new AgedBrie
            {
                SellIn = 0,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(-1, item.SellIn);
            Assert.Equal(12, item.Quality);
        }

        [Fact]
        public void AgedBrieAfterDayMaxQuality()
        {
            AgedBrie item = new AgedBrie
            {
                SellIn = 10,
                Quality = 50
            };

            item.AddDay();

            Assert.Equal(9, item.SellIn);
            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void SulfurasAfterDay()
        {
            Sulfuras item = new Sulfuras
            {
                SellIn = 10,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(10, item.SellIn);
            Assert.Equal(10, item.Quality);
        }

        [Fact]
        public void BackstagePassesAfterDayOverTenDaysAway()
        {
            BackstagePasses item = new BackstagePasses
            {
                SellIn = 12,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(11, item.SellIn);
            Assert.Equal(11, item.Quality);
        }

        [Fact]
        public void BackstagePassesAfterDayOverFiveDaysAway()
        {
            BackstagePasses item = new BackstagePasses
            {
                SellIn = 7,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(6, item.SellIn);
            Assert.Equal(12, item.Quality);
        }

        [Fact]
        public void BackstagePassesAfterDayLessThanSixDaysAway()
        {
            BackstagePasses item = new BackstagePasses
            {
                SellIn = 5,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(4, item.SellIn);
            Assert.Equal(13, item.Quality);
        }

        [Fact]
        public void BackstagePassesAfterDayAfterSellIn()
        {
            BackstagePasses item = new BackstagePasses
            {
                SellIn = 0,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(-1, item.SellIn);
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void ConjuredAfterDay()
        {
            ConjuredItem item = new ConjuredItem
            {
                SellIn = 10,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(9, item.SellIn);
            Assert.Equal(8, item.Quality);
        }

        [Fact]
        public void ConjuredAfterDayAfterSellIn()
        {
            ConjuredItem item = new ConjuredItem
            {
                SellIn = 0,
                Quality = 10
            };

            item.AddDay();

            Assert.Equal(-1, item.SellIn);
            Assert.Equal(6, item.Quality);
        }

        [Fact]
        public void ParseExampleDataWithoutInvalidItem()
        {
            InventoryManager manager = new InventoryManager();
            List<string> lines = new List<string>
            {
                "Aged Brie 1 1",
                "Backstage passes -1 2",
                "Backstage passes 9 2",
                "Sulfuras 2 2",
                "Normal Item -1 55",
                "Normal Item 2 2",
                "Conjured 2 2",
                "Conjured -1 5"
            };

            List<Item> items = manager.ParseLinesToItems(lines);

            for (int i = 0; i < lines.Count; i++)
            {
                Assert.Equal(lines[i], items[i].ToString());
            }
        }

        [Fact]
        public void ParseExampleInvalidItem()
        {
            InventoryManager manager = new InventoryManager();
            List<string> lines = new List<string>
            {
                "INVALID ITEM 2 2"
            };

            List<Item> items = manager.ParseLinesToItems(lines);

            Assert.Equal("NO SUCH ITEM", items[0].ToString());
        }

        [Fact]
        public void ExampleTest()
        {
            InventoryManager manager = new InventoryManager();
            List<string> lines = new List<string>
            {
                "Aged Brie 1 1",
                "Backstage passes -1 2",
                "Backstage passes 9 2",
                "Sulfuras 2 2",
                "Normal Item -1 55",
                "Normal Item 2 2",
                "INVALID ITEM 2 2",
                "Conjured 2 2",
                "Conjured -1 5"
            };

            List<Item> items = manager.ParseLinesToItems(lines);
            manager.AddDayToItems(items);

            Assert.Equal("Aged Brie 0 2", items[0].ToString());
            Assert.Equal("Backstage passes -2 0", items[1].ToString());
            Assert.Equal("Backstage passes 8 4", items[2].ToString());
            Assert.Equal("Sulfuras 2 2", items[3].ToString());
            Assert.Equal("Normal Item -2 50", items[4].ToString());
            Assert.Equal("Normal Item 1 1", items[5].ToString());
            Assert.Equal("NO SUCH ITEM", items[6].ToString());
            Assert.Equal("Conjured 1 0", items[7].ToString());
            Assert.Equal("Conjured -2 1", items[8].ToString());
        }
    }
}
