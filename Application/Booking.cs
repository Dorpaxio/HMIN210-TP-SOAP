using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetHotel
{
    public class Booking
    {
        private Room room { get; }
        private string creditCardInfos { get; }
        private DateTime arrival { get; }
        private DateTime departure { get; }

        public Booking(Room room, string creditCardInfos, DateTime arrival, DateTime departure) {
            this.room = room;
            this.creditCardInfos = creditCardInfos;
            this.arrival = arrival;
            this.departure = departure;
        }
    }
}