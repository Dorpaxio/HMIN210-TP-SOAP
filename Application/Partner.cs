using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetHotel
{
    public class Partner
    {
        private float _percentage;

        public float percentage
        {
            get => _percentage;
            set => _percentage = value < 0 ? 0 : value > 1 ? 1 : value;
        }

        // Constructors
        public Partner(float percentage)
        {
            this.percentage = percentage;
        }
    }
}