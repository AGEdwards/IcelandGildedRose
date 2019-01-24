using System;
using System.Collections.Generic;
using System.Text;

namespace GildedRose
{
    public class Item
    {
        public virtual string Name { get { return "Normal Item"; } }
        public int Quality { get; set; }
        public int SellIn { get; set; }

        public void AddDay()
        {
            AddDayToSellIn();
            AddDayToQuality();
        }

        protected virtual void AddDayToSellIn()
        {
            SellIn--;
        }

        protected virtual void AddDayToQuality()
        {
            if (SellIn < 0)
            {
                Quality--;
            }
            Quality--;

            if (Quality > 50)
            {
                Quality = 50;
            }
        }

        public override string ToString()
        {
            return $"{Name} {SellIn} {Quality}";
        }
    }

    public class AgedBrie : Item
    {
        public override string Name { get { return "Aged Brie"; } }

        protected override void AddDayToQuality()
        {
            if (SellIn < 0)
            {
                Quality++;
            }
            Quality++;

            if (Quality > 50)
            {
                Quality = 50;
            }
        }
    }

    public class Sulfuras : Item
    {
        public override string Name { get { return "Sulfuras"; } }

        protected override void AddDayToSellIn()
        {
            // NOP
        }

        protected override void AddDayToQuality()
        {
            if (Quality > 50)
            {
                Quality = 50;
            }
        }
    }

    public class BackstagePasses : Item
    {
        public override string Name { get { return "Backstage passes"; } }

        protected override void AddDayToQuality()
        {
            if ( SellIn > 10)
            {
                Quality++;
            } else if ( SellIn > 5)
            {
                Quality = Quality + 2;
            } else if ( SellIn > -1)
            {
                Quality = Quality + 3;
            } else
            {
                Quality = 0;
            }
            
            if (Quality > 50)
            {
                Quality = 50;
            }
        }
    }

    public class ConjuredItem : Item
    {
        public override string Name { get { return "Conjured"; } }

        protected override void AddDayToQuality()
        {
            if (SellIn < 0)
            {
                Quality = Quality - 2;
            }
            Quality = Quality - 2;
            
            if (Quality > 50)
            {
                Quality = 50;
            }
        }
    }

    public class UnknownItem : Item
    {
        public override string Name { get { return "NO SUCH ITEM"; } }

        public override string ToString()
        {
            return Name;
        }
    }
}
