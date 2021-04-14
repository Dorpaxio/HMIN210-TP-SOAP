using System.Web.Services;
using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ProjetHotel.WebServices
{
    /// <summary>
    /// Description résumée de BBHotelService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class BBHotelService : WebService
    {

        private static Hotel hotel;
        private static Dictionary<string, Partner> auths;
        private static Random random = new Random();

        [WebMethod]
        public void Init()
        {
            hotel = new Hotel("BZ Bihun Hotel", 4, 69, "Viale del perno della gioia", "Venise", "Italie", 13.478456, 3.874314);
            auths = new Dictionary<string, Partner>();
            hotel.addRoom("SuiteA", 412, 3, "https://media.gettyimages.com/photos/luxury-hotel-bedroom-interior-with-a-bed-picture-id907621754?s=2048x2048");
            hotel.addRoom("SuiteB", 245, 2, "https://media.gettyimages.com/photos/chinese-style-bedroom-interior-picture-id1129076301?s=2048x2048");
            hotel.addRoom("SuiteC", 1341, 5, "https://www.gettyimages.fr/detail/photo/bedroom-image-libre-de-droits/185260469");
            hotel.addRoom("SuiteD", 135, 1, "https://media.gettyimages.com/photos/chinese-style-bedroom-interior-picture-id1129076301?s=2048x2048");
            hotel.addRoom("SuiteE", 578, 2, "https://media.gettyimages.com/photos/stylish-tropical-bedroom-at-night-picture-id1081883830?s=2048x2048");

            hotel.addPartner("Agence Desgenetez", "ok", 0.69f);
            hotel.addPartner("Tho-Mas Cook", "admin", 0.35f);
            hotel.addPartner("Max Hymne", "max", 0.13f);
            hotel.addPartner("Sebien", "imou", 0.01f);
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