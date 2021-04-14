using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client
{
    public class Client
    {
        private string _firstName;
        private string _lastName;
        private List<Booking> _bookings;

        public Client(string firstName, string lastName)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._bookings = new List<Booking>();
        }

        public string firstName => _firstName;
        public string lastName => _firstName;

        public List<Booking> bookings => _bookings;

        public void createBooking(Room room, string creditCardInfos, DateTime arrival, DateTime departure)
        {
            this.bookings.Add(new Booking(room, creditCardInfos, arrival, departure));
        }
    }
}