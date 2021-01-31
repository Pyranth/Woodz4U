using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Woodz4U.Models
{
    public class ItemModel
    {
        public List<Item> Item { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Material { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
