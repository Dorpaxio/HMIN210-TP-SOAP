using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetHotel
{
    public class Hotel
    {
        public string name { get; set; }
        private int _rating;
        // Adress
        private int streetNumber{ get; set; }
        private string street { get; set; }
        private string place { get; set; }
        private string city { get; set; }
        private string country { get; set; }
        private double latitude { get; set; }
        private double longitude { get; set; }

        public List<Room> rooms { get; }

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
        }

        public int rating
        {
            get => _rating;
            set => _rating = value < 0 ? 0 : value > 5 ? 5 : value;
        }

        public void addRoom(int price, int beds)
        {
            this.rooms.Add(new Room(price, beds));
        }
        

    }
}