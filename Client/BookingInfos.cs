using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class BookingInfos
    {
        public string roomId { get; }
        public string creditCardInfos { get; }
        public DateTime arrival { get; }
        public DateTime departure { get; }
        public int nbVoyageurs { get; }
        public string firstName { get; }
        public string lastName { get; }

        public BookingInfos(string roomId, string creditCardInfos, DateTime arrival, DateTime departure, int nbVoyageurs, string firstName, string lastName)
        {
            this.roomId = roomId;
            this.creditCardInfos = creditCardInfos;
            this.arrival = arrival;
            this.departure = departure;
            this.nbVoyageurs = nbVoyageurs;
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}
