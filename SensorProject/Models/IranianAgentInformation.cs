using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    internal class IranianAgentInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string FavouriteWeapon { get; set; }
        public string ContactNumber { get; set; }
        public string SecretPhrase { get; set; }
        public string Affiliation { get; set; }
        public bool Exposed { get; set; }

        public IranianAgentInformation(string firstName, string lastName, string location,
            string favouriteWeapon, string contactNumber, string secretPhrase,
            string affiliation, bool exposed)
        {
            FirstName = firstName;
            LastName = lastName;
            Location = location;
            FavouriteWeapon = favouriteWeapon;
            ContactNumber = contactNumber;
            SecretPhrase = secretPhrase;
            Affiliation = affiliation;
            Exposed = exposed;
        }

        public string RevealAgent()
        {
            return $"Name - {FirstName} {LastName}\n" +
                $"Location - {Location}\n" +
                $"Favourite Weapon - {FavouriteWeapon}\n" +
                $"Contact Number - {ContactNumber}\n" +
                $"Secret Phrase - {SecretPhrase}\n" +
                $"Affiliation - {Affiliation}\n" +
                $"Exposed - {Exposed}";
        }
    }
}
