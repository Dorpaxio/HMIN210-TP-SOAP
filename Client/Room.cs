using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Client
{
    [Serializable]
    public class Room
    {
        public float price { get; }
        public int beds { get; }

        public string id { get; }

        public string imgUrl { get; }

        public Room()
        {
            this.price = 0;
            this.beds = 0;
        }

        [JsonConstructor]
        public Room(string id, float price, int beds, string imgUrl)
        {
            this.id = id;
            this.price = price;
            this.beds = beds;
            this.imgUrl = imgUrl;
        }
    }
}