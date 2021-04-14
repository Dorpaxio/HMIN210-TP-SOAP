using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Client
{
    public class Hotel
    {
        public string name { get; set; }
        private int _rating;
        // Adress
        public int streetNumber{ get; set; }
        public string street { get; set; }
        public string place { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public List<Room> rooms { get; }
        public List<Partner> partners { get; }
        public List<Client> clients { get;   }

        public Hotel(string name, int rating, int streetNumber, string street, string city, string country, double latitude, double longitude) :
            this(name, rating, streetNumber, street, null, city, country, latitude, longitude)
        {}

        public Hotel(string name, int rating, int streetNumber, string street, string place, string city, string country, double latitude, double longitude)
        {
            this.name = name;
            this.rating = rating;
            this.streetNumber = streetNumber;
            this.street = street;
            this.place = place;
            this.city = city;
            this.country = country;
            this.latitude = latitude;
            this.longitude = longitude;
            this.rooms = new List<Room>();
            this.partners = new List<Partner>();
            this.clients = new List<Client>();
        }

        [JsonConstructor]
        public Hotel(string name, int rating, int streetNumber, string street, string place, string city, string country, double latitude, double longitude, List<Room> rooms, List<Partner> partners, List<Client> clients)
        {
            this.name = name;
            this.rating = rating;
            this.streetNumber = streetNumber;
            this.street = street;
            this.place = place;
            this.city = city;
            this.country = country;
            this.latitude = latitude;
            this.longitude = longitude;
            this.rooms = rooms;
            this.partners = partners;
            this.clients = clients;
        }

        public int rating
        {
            get => _rating;
            set => _rating = value < 0 ? 0 : value > 5 ? 5 : value;
        }

        public void addRoom(string id, float price, int beds, string imgUrl)
        {
            this.rooms.Add(new Room(id, price, beds, imgUrl)); ;
        }
        
        public void addPartner(string name, string password, float percentage)
        {
            this.partners.Add(new Partner(name, password, percentage));
        }

        public Client addClient(string firstName, string lastName)
        {
            Client client = new Client(firstName, lastName);
            this.clients.Add(client);
            return client;
        }

        public bool isRoomReserved(Room room, DateTime debut, DateTime fin)
        {
            return this.clients.Find(r => r.bookings.Find(b => b.room == room && ((b.arrival <= debut && b.departure >= debut) || (b.arrival <= fin && b.departure >= fin))) != null) != null;
        }

    }
}