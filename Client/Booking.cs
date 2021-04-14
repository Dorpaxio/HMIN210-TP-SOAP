using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client
{
    public class Booking
    {
        public Room room { get; }
        public string creditCardInfos { get; }
        public DateTime arrival { get; }
        public DateTime departure { get; }

        public Booking(Room room, string creditCardInfos, DateTime arrival, DateTime departure) {
            this.room = room;
            this.creditCardInfos = creditCardInfos;
            this.arrival = arrival;
            this.departure = departure;
        }
    }
}