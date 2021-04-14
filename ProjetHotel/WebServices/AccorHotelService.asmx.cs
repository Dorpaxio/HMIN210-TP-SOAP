using System.Web.Services;
using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ProjetHotel.WebServices
{
    /// <summary>
    /// Description résumée de AccorHotelService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class AccorHotelService : WebService
    {

        private static Hotel hotel;
        private static Dictionary<string, Partner> auths;
        private static Random random = new Random();

        [WebMethod]
        public void Init()
        {
            hotel = new Hotel("AccorHotel", 2, 13, "Rue du Parmezan", "Montpellier", "France", 1.454456, 2.454564);
            auths = new Dictionary<string, Partner>();
            hotel.addRoom("A12", 80, 2, "https://media.gettyimages.com/photos/hotel-room-in-the-new-hotel-complex-in-moscow-picture-id871617622?s=2048x2048");
            hotel.addRoom("A14", 45, 1, "https://media.gettyimages.com/photos/tourist-woman-staying-in-luxury-hotel-picture-id975732182?k=6&m=975732182&s=612x612&w=0&h=G7iM5yp4rew7G-DWIW_1VYAJtZ0OyWUXDm4T23SZg98=");
            hotel.addRoom("A22", 135, 4, "https://media.gettyimages.com/photos/modern-bedroom-interior-with-sea-view-picture-id1146021471?k=6&m=1146021471&s=612x612&w=0&h=lSB7UcQxW4wQP86ks-ksbopVr27Qfcyz43uWsQt0B4Q=");
            hotel.addRoom("B12", 150, 6, "https://media.gettyimages.com/photos/studioshotel-photography-picture-id983627784?s=2048x2048");
            hotel.addRoom("B02", 90, 3, "https://media.gettyimages.com/photos/luxury-hotel-room-picture-id184609057?s=2048x2048");
            hotel.addRoom("C11", 24, 1, "https://media.gettyimages.com/photos/beautiful-modern-hotel-room-picture-id1006187120?s=2048x2048");
            hotel.addRoom("C02", 135, 2, "https://media.gettyimages.com/photos/senior-couple-having-fun-in-hotel-room-picture-id1043782452?s=2048x2048");
            hotel.addRoom("C03", 268, 2, "https://www.gettyimages.fr/detail/photo/deluxe-in-5-star-hotel-in-moscow-image-libre-de-droits/1007087224");

            hotel.addPartner("Agence Desgenetez", "1234", 0.35f);
            hotel.addPartner("Tho-Mas Cook", "bg", 0.13f);
            hotel.addPartner("Max Hymne", "jtm", 0.18f);
            hotel.addPartner("Sebien", "music", 0.09f);
            hotel.addPartner("a", "a", 0.1f);
        }

        public Partner checkToken(string token)
        {
            return auths.ContainsKey(token) ? auths[token] : null;
        }

        [WebMethod]
        public string Auth(string partnerName, string password)
        {
            Partner partner = hotel.partners.Find(p => p.name == partnerName && p.password == password);
            if (partner == null) return null;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string key = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            auths.Add(key, partner);
            return key;
        }

        [WebMethod]
        public string GetRooms(string token, int nbVoyageurs, DateTime debut, DateTime fin)
        {
            Partner partner = checkToken(token);
            if (partner == null) return null;
            return JsonConvert.SerializeObject(hotel.rooms
                .FindAll(room => room.beds >= nbVoyageurs && !hotel.isRoomReserved(room, debut, fin))
                .Select(room => new Room(room.id, room.price - room.price * partner.percentage, room.beds, room.imgUrl)));
        }

        [WebMethod]
        public bool BookRoom(string token, string infos)
        {
            Partner partner = checkToken(token);
            if (partner == null) return false;

            BookingInfos bookingInfos = JsonConvert.DeserializeObject<BookingInfos>(infos);

            Client.Client client = hotel.clients.Find(c => c.firstName.Equals(bookingInfos.firstName) && c.lastName.Equals(bookingInfos.lastName));
            if (client == null) client = hotel.addClient(bookingInfos.firstName, bookingInfos.lastName);

            Room room = hotel.rooms.Find(r => r.id.Equals(bookingInfos.roomId) && !hotel.isRoomReserved(r, bookingInfos.arrival, bookingInfos.departure));
            if (room == null) return false;

            client.createBooking(room, bookingInfos.creditCardInfos, bookingInfos.arrival, bookingInfos.departure);
            return true;
        }

    }
}
