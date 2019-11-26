using System;
using System.ComponentModel;


namespace AnimalShelter.BizLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"C:\src\QA Transition - Projects\AnimalsShelter\AS_DataStoreOut.csv";

            Console.WriteLine("Hello and Welcome to the Animal Shelter.");
            Console.WriteLine("      hit 'Esc' key to exit.");
            Console.WriteLine();
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                var animal = new Animal();
                var animalRepository = new AnimalRepository();

                Console.WriteLine("Is it your first time here? (Y/N) ");
                string answer = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(answer) && answer.ToLower().Trim() == "y") // Actions for New Commers
                {
                    animal = Utilities.EnterAnimalValues(animal);
                    animal.AnimalId = Utilities.RandomString(6);
                    if (animalRepository.Add(animal, filePath))
                    {
                        animal.InShelterState = AnimalStateOption.CheckedIn;
                        Console.WriteLine($"Your pet {animal.AnimalName.ToUpperInvariant()} is checked-in now.");
                        Console.WriteLine("      pls hit Enter to continue.");
                    }                    
                }
                else if (!string.IsNullOrWhiteSpace(answer) && answer.ToLower().Trim() == "n") // Action for Existing customer
                {
                    Console.WriteLine("Do you want to update pet's info? ");
                    answer = Console.ReadLine();
                    Console.WriteLine("Please enter its ID: ");
                    var petsID = Console.ReadLine();
                    var retrieve = animalRepository.Retrieve(petsID, filePath);
                    Console.WriteLine("- Here is the data we have on file: ");
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(retrieve)) // Printout exising data on specific pet
                    {
                        string name = descriptor.Name;
                        object value = descriptor.GetValue(retrieve);
                        Console.WriteLine("{0}={1}", name, value);                        
                    }
                    if (!string.IsNullOrWhiteSpace(answer) && answer.ToLower() == "y") // Actions to Update existing pet info
                    {                        
                        Console.WriteLine("- Please fill in the updated info: ");
                        animal = Utilities.EnterAnimalValues(animal);
                        animal.AnimalId = retrieve.AnimalId;
                        animalRepository.Update(animal, petsID, filePath);
                        Console.WriteLine($"The info for {animal.AnimalType.ToUpperInvariant()} : {animal.AnimalName.ToUpperInvariant()} has been updated.");
                        Console.WriteLine("      pls hit Enter to continue. OR  \n      pres 'Esc' key to exit.");
                    }
                    else if (!string.IsNullOrWhiteSpace(answer) && answer.ToLower() == "n") // Actions to Update existing pet info to take it back home
                    {
                        Console.WriteLine("Do you want to take your pet home?: ");
                        answer = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(answer) && answer.ToLower() == "y")
                        {
                            animalRepository.Archive(petsID, filePath);
                            Console.WriteLine("Your pet's status is now Returned.");
                            Console.WriteLine("      pls hit Enter to continue. OR  \n      pres 'Esc' key to exit.");
                        }
                    }
                    else // No Updates on axisting pet
                    {
                        Console.WriteLine("      pls hit Enter to continue. OR  \n      pres 'Esc' key to exit.");
                    }
                } 
                else
                {
                    animalRepository.Add(animal, filePath);
                }

                answer = null;
            }
        }
    }
}
