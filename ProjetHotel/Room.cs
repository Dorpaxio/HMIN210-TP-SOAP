using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetHotel
{
    public class Room
    {
        public int price { get; }
        public int beds { get; }
        public Room(int price, int beds)
        {
            this.price = price;
            this.beds = beds;
        }
    }
}