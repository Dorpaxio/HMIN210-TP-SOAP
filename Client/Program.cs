using System;
using System.Collections.Generic;
using Client.AccorHotel;
using Client.BBHotel;
using Newtonsoft.Json;
using System.Globalization;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            AccorHotelService accorService = new AccorHotelService();
            accorService.Init();

            BBHotelService BBService = new BBHotelService();
            BBService.Init();

            Console.WriteLine("Bienvenue sur la sélection d'hotel.");
            bool demande = true;
            while (demande)
            {
                Console.WriteLine("- Sélectionnez un hotêl dans lequel vous souhaitez faire une réservation");
                Console.WriteLine("-- 1 : AccordHotel");
                Console.WriteLine("-- 2 : BBHotel");
                string hotel = Console.ReadLine();

                if (hotel == "1")
                {
                    Console.WriteLine("Vous avez choisi AccorHotel.");
                    Console.WriteLine("Connectez-vous à l'aide de vos identifiants et mot de passe d'agence partenaire.");

                    string token = null;
                    while (token == null)
                    {
                        Console.WriteLine("Agence : ");
                        string partnerName = Console.ReadLine();
                        Console.WriteLine("Mot de passe : ");
                        string password = Console.ReadLine();
                        token = accorService.Auth(partnerName, password);
                        if (token == null) Console.WriteLine("Mauvaise agence ou mot de passe, veuillez réssayer.");
                    }

                    Console.WriteLine("Vous êtes maintenant authentifié comme une agence partenaire.");
                    Console.WriteLine("- Recherche un voyage");
                    Console.WriteLine("-- Entrez le nombre de voyageurs");
                    int voyageurs = int.Parse(Console.ReadLine());
                    Console.WriteLine("-- Entrez la date d'arrivée ! FORMAT : 24/01/2012");
                    DateTime dateDebut = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Console.WriteLine("-- Entrez la date de départ ! FORMAT : 24/01/2012");
                    DateTime dateFin = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    Console.WriteLine("Récapitulatif de votre recherche : ");
                    Console.WriteLine("- Voyageurs : " + voyageurs + " | Arrivée : " + dateDebut.ToString("dd/MM/yyyy") + " | Départ : " + dateFin.ToString("dd/MM/yyyy"));
                    List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(accorService.GetRooms(token, voyageurs, dateDebut, dateFin));
                    Console.WriteLine("Il y a " + rooms.Count + " chambres de disponibles");
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        Console.WriteLine("######## Chambre n°" + (i+1) + " ########");
                        Console.WriteLine("-- Lits disponibles : " + rooms[i].beds);
                        Console.WriteLine("-- Prix : " + rooms[i].price + " euros");
                        Console.WriteLine("-- Image : " + rooms[i].imgUrl);
                        Console.WriteLine("#############################\n");
                    }

                    Console.WriteLine("Quelle chambre voulez-vous réserver ? (Indiquez le numéro de la chambre qui vous intéresse, 0 sinon");

                    int selection = int.Parse(Console.ReadLine());
                    if (selection != 0 && selection <= rooms.Count)
                    {
                        Console.WriteLine("Commençons la réservation !");
                        Console.WriteLine("Entrez votre nom : ");
                        string lastname = Console.ReadLine();
                        Console.WriteLine("Entrez votre prénom : ");
                        string firstname = Console.ReadLine();
                        Console.WriteLine("Entrez vos informations banquaires : ");
                        string creditCard = Console.ReadLine();

                        BookingInfos bookingInfos = new BookingInfos(rooms[selection-1].id, creditCard, dateDebut, dateFin, voyageurs, firstname, lastname);
                        if(!accorService.BookRoom(token, JsonConvert.SerializeObject(bookingInfos)))
                            Console.WriteLine("Un problème est survenu, votre réservation a été annulée.");
                        else
                            Console.WriteLine("Vous avez bien réservé votre chambre !");
                    }

                    bool repValide = false;
                    while (!repValide)
                    {
                        Console.WriteLine("Voulez-vous réserver une autre chambre ? (oui/non)");
                        string res = Console.ReadLine();
                        if (res.Equals("oui")) repValide = true;
                        else if (res.Equals("non"))
                        {
                            demande = false;
                            repValide = true;
                        }
                        else Console.WriteLine("Nous n'avons pas compris votre réponse !");
                    }
                }

                else if (hotel == "2")
                {
                    Console.WriteLine("Vous avez choisi BBHotel.");
                    Console.WriteLine("Connectez-vous à l'aide de vos identifiants et mot de passe d'agence partenaire.");

                    string token = null;
                    while (token == null)
                    {
                        Console.WriteLine("Agence : ");
                        string partnerName = Console.ReadLine();
                        Console.WriteLine("Mot de passe : ");
                        string password = Console.ReadLine();
                        token = BBService.Auth(partnerName, password);
                        if (token == null) Console.WriteLine("Mauvaise agence ou mot de passe, veuillez réssayer.");
                    }

                    Console.WriteLine("Vous êtes maintenant authentifié comme une agence partenaire.");
                    Console.WriteLine("- Recherche un voyage");
                    Console.WriteLine("-- Entrez le nombre de voyageurs");
                    int voyageurs = int.Parse(Console.ReadLine());
                    Console.WriteLine("-- Entrez la date d'arrivée ! FORMAT : 24/01/2012");
                    DateTime dateDebut = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Console.WriteLine("-- Entrez la date de départ ! FORMAT : 24/01/2012");
                    DateTime dateFin = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    Console.WriteLine("Récapitulatif de votre recherche : ");
                    Console.WriteLine("- Voyageurs : " + voyageurs + " | Arrivée : " + dateDebut.ToString("dd/MM/yyyy") + " | Départ : " + dateFin.ToString("dd/MM/yyyy"));
                    List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(BBService.GetRooms(token, voyageurs, dateDebut, dateFin));
                    Console.WriteLine("Il y a " + rooms.Count + " chambres de disponibles");
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        Console.WriteLine("######## Chambre n°" + (i + 1) + " ########");
                        Console.WriteLine("-- Lits disponibles : " + rooms[i].beds);
                        Console.WriteLine("-- Prix : " + rooms[i].price + " euros");
                        Console.WriteLine("#############################\n");
                    }

                    Console.WriteLine("Quelle chambre voulez-vous réserver ? (Indiquez le numéro de la chambre qui vous intéresse, 0 sinon");

                    int selection = int.Parse(Console.ReadLine());
                    if (selection != 0 && selection <= rooms.Count)
                    {
                        Console.WriteLine("Commençons la réservation !");
                        Console.WriteLine("Entrez votre nom : ");
                        string lastname = Console.ReadLine();
                        Console.WriteLine("Entrez votre prénom : ");
                        string firstname = Console.ReadLine();
                        Console.WriteLine("Entrez vos informations banquaires : ");
                        string creditCard = Console.ReadLine();

                        BookingInfos bookingInfos = new BookingInfos(rooms[selection - 1].id, creditCard, dateDebut, dateFin, voyageurs, firstname, lastname);
                        if (!BBService.BookRoom(token, JsonConvert.SerializeObject(bookingInfos)))
                            Console.WriteLine("Un problème est survenu, votre réservation a été annulée.");
                        else
                            Console.WriteLine("Vous avez bien réservé votre chambre !");
                    }

                    bool repValide = false;
                    while (!repValide)
                    {
                        Console.WriteLine("Voulez-vous réserver une autre chambre ? (oui/non)");
                        string res = Console.ReadLine();
                        if (res.Equals("oui")) repValide = true;
                        else if (res.Equals("non"))
                        {
                            demande = false;
                            repValide = true;
                        }
                        else Console.WriteLine("Nous n'avons pas compris votre réponse !");
                    }
                }

            }
        }
    }
}