using System;
using System.Linq;
using System.Text;


namespace AS.BL
{
    public static class Utilities
    {
        public static Animal EnterAnimalValues(Animal animal)
        {
            Console.Write("What is you phone number?: ");
            while (true)
            {
                var input = Console.ReadLine();
                if (input != null && input != "")
                {
                    animal.OwnerPhoneNumber = input;
                    break;
                }
                Console.WriteLine($"Sorry, Phone Number is Required. Please re-enter: ");
            }
            Console.Write("What is you pet type?: ");
            while (true)
            {
                var input = Console.ReadLine();
                if (input != null && input != "")
                {
                    animal.AnimalType = input;
                    break;
                }
                Console.WriteLine($"Sorry, Animal Type cannot be empty. Please re-enter: ");
            }
            Console.Write("What is you pet Name?: ");
            animal.AnimalName = (Console.ReadLine());
            Console.Write("What is you pet age (in years)?: ");
            while (true)
            {
                float result;
                var input = Console.ReadLine();
                if (input == null || input == "")
                {
                    animal.AnimalAge = null;
                    break;
                }
                if (float.TryParse(input, out result))
                {
                    animal.AnimalAge = result;
                    break;
                }
                Console.WriteLine($"Sorry, '{input}' is not correct age value. Please re-enter: ");
            }
            Console.Write("What is the color of your pet?: ");
            animal.AnimalColor = (Console.ReadLine());
            Console.Write("Does your pet have any food restrictions (Y/N)?: ");
            var read = Console.ReadLine();
            if (read.ToLowerInvariant().Trim() == "y")
            {
                animal.FoodRestriction = true;
            } else if (read.ToLowerInvariant().Trim() == "n")
            {
                animal.FoodRestriction = false;
            }
            else
            {
                animal.FoodRestriction = null;
            }
            Console.Write("Any other details on your pet?: ");
            animal.AnimalDescription = (Console.ReadLine());

            return animal;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RemoveSpecialCharacters(string str, bool allowLetters = false)
        {
            StringBuilder sb = new StringBuilder();
            if (!allowLetters)
            {
                foreach (char c in str)
                {
                    if ((c >= '0' && c <= '9'))
                    {
                        sb.Append(c);
                    }
                }
            }
            else
            {
                foreach (char c in str)
                {
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    {
                        sb.Append(c);
                    }
                }
            }
            return sb.ToString();
        }
    }
}
