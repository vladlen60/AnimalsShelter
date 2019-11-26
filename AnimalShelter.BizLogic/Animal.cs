using System;


namespace AnimalShelter.BizLogic
{
    public class Animal : AnimalBase
    {
        private string _ownerPhoneNumber;

        public Animal()
        {

        }

        public Animal(string animalId)
        {
            AnimalId = animalId;
        }

        public string AnimalId { get; set; } // Setter should be Private when a DB part is finished (the unique-Id should be set by DB)
        public string AnimalType { get; set; }
        public string AnimalName { get; set; }
        public float? AnimalAge { get; set; }
        public string AnimalColor { get; set; }
        public bool? FoodRestriction { get; set; }
        public string AnimalDescription { get; set; }
        public string OwnerPhoneNumber
        {
            get { return _ownerPhoneNumber; }
            set => _ownerPhoneNumber = "=\"" + Utilities.RemoveSpecialCharacters(value).Trim() + "\"";
        }

        /// <summary>
        /// Validates the animal data
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            var isValid = true;

            if (Enum.IsDefined(typeof(AcceptedAnimalOption), AnimalType.ToLower()))
            {
                if (string.IsNullOrWhiteSpace(value: AnimalType)) isValid = false;
                if (string.IsNullOrWhiteSpace(value: AnimalName)) isValid = false;
                //if (AnimalAge == null) isValid = false;
            }
            else
            {
                isValid = false;
                Console.WriteLine(string.Format($"Sorry, but the animal type of {AnimalType.ToUpperInvariant()} is not accepted in our Shelter."));
                Console.WriteLine($"We support only: ");
                foreach (var myEn in Enum.GetNames(typeof(AcceptedAnimalOption)))
                {
                    Console.Write($"{myEn.ToUpperInvariant()} ");
                }
                Console.WriteLine();
                Console.WriteLine("Please re-enter or hit Esc key.");
                //throw new Exception("An error occured.");
            }

            return isValid;
        }
    }
}
