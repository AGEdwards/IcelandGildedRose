using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GildedRose
{
    public class InventoryManager
    {
        private readonly Func<List<string>> GetInput;

        public InventoryManager()
        {
            GetInput = GetInputFromConsole;
        }

        // Currently unused, but could be used later to use file input etc.
        public InventoryManager(Func<List<string>> GetInput)
        {
            this.GetInput = GetInput;
        }

        private List<string> GetInputFromConsole()
        {
            List<string> lines = new List<string>();

            while (true)
            {
                string input = Console.ReadLine();
                // Stop input if empty or only whitespace is entered
                if( string.IsNullOrWhiteSpace(input) )
                {
                    break;
                }
                lines.Add(input);
            }
            return lines;
        }

        public List<Item> ParseLinesToItems(List<string> lines)
        {
            List<Item> items = new List<Item>();

            foreach (string line in lines)
            {
                // Use regular expressions to extract the data
                Match itemNameMatch = Regex.Match(line, @"([a-zA-Z ])+");
                if (!itemNameMatch.Success)
                {
                    throw new InvalidOperationException();
                }

                Item item;
                // Need to remove trailing space
                switch (itemNameMatch.Value.Trim())
                {
                    case "Normal Item":
                        item = new Item();
                        break;
                    case "Aged Brie":
                        item = new AgedBrie();
                        break;
                    case "Sulfuras":
                        item = new Sulfuras();
                        break;
                    case "Backstage passes":
                        item = new BackstagePasses();
                        break;
                    case "Conjured":
                        item = new ConjuredItem();
                        break;
                    default:
                        item = new UnknownItem();
                        break;
                }

                // Match positive or negative digits
                Match sellInAndQualityMatch = Regex.Match(line, @"(-?\d+)");
                if (!itemNameMatch.Success)
                {
                    throw new InvalidOperationException();
                }

                // SellIn value comes first
                item.SellIn = Int32.Parse(sellInAndQualityMatch.Value);

                sellInAndQualityMatch = sellInAndQualityMatch.NextMatch();
                item.Quality = Int32.Parse(sellInAndQualityMatch.Value);

                items.Add(item);
            }

            return items;
        }

        public void AddDayToItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                item.AddDay();
            }
        }

        static void Main(string[] args)
        {            
            InventoryManager manager = new InventoryManager();

            List<string> lines = manager.GetInput();
            List<Item> items = manager.ParseLinesToItems(lines);
            manager.AddDayToItems(items);

            foreach(Item item in items)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
