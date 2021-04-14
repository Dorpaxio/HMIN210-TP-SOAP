using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client
{
    public class Partner
    {
        public string name { get; }
        public string password { get; }
        private float _percentage;

        public float percentage
        {
            get => _percentage;
            set => _percentage = value < 0 ? 0 : value > 1 ? 1 : value;
        }

        // Constructors
        public Partner(string name, string password, float percentage)
        {
            this.name = name;
            this.password = password;
            this.percentage = percentage;
        }
    }
}