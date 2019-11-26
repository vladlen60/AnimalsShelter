using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace AnimalShelter.BizLogic
{
    public class AnimalRepository
    {
        /// <summary>
        /// Retrieve one animal data.
        /// </summary>
        /// /// <returns></returns>
        public Animal Retrieve(string animalId, string filePath)
        {
            // Call a Rerieve Stored Procedure
            List<Animal> records = null;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    using (CsvReader csvReader = new CsvReader(reader))
                    {
                        records = csvReader.GetRecords<Animal>().ToList();
                    }
                }
                while (true)
                {
                    // This part handles Duplicated records. This needs to be handle more properly later.
                    var tempList = records.FindAll(x => x.AnimalId.ToLowerInvariant() == animalId.ToLowerInvariant());
                    if (tempList.Count > 1)
                    {
                        Console.WriteLine("+++ A duplicated records found. Please delete those manually");
                    }


                    var index = records.FindIndex(x => x.AnimalId.ToLowerInvariant() == animalId.ToLowerInvariant());
                    if (index < 0)
                    {
                        Console.WriteLine($"Sorry, the ID: {animalId} does not exists. Please enter the correct ID (or Esc to exit): ");
                        animalId = Console.ReadLine();
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Escape)
                        {

                            throw new Exception($"Sorry, the ID: {animalId} does not exists. Bye.");
                        }
                    } else
                    {
                        return records[index];
                    }                    
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("The Data file does not exist.  Please press any key to exit.");
                Console.ReadKey();
                throw;
            }
        }

        /// <summary>
        /// Saves the animal data.
        /// </summary>
        /// <returns></returns>
        public bool Add(Animal animal, string filePath)
        {
            var success = true;

            if (animal.IsValid)
            {
                // Call an Insert Stored Procedure
                List<Animal> records = null;
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        using (CsvReader csvReader = new CsvReader(reader))
                        {
                            records = csvReader.GetRecords<Animal>().ToList();
                        }
                    }

                    records.Add(animal);

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        using (CsvWriter csvWriter = new CsvWriter(writer))
                        {
                            csvWriter.WriteRecords(records);
                        }
                    }
                }
                else
                {
                    using (StreamWriter writer = File.CreateText(filePath))
                    using (var csvWriter = new CsvWriter(writer))
                    {
                        csvWriter.WriteHeader<Animal>();
                        csvWriter.NextRecord();
                        csvWriter.WriteRecord(animal);
                    }
                }
            }
            else
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Update one animal.
        /// </summary>
        /// /// <returns></returns>
        public bool Update(Animal animal, string animalId, string filePath)
        {
            var success = true;
            // Call a Rerieve Stored Procedure
            List<Animal> records = null;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    using (CsvReader csvReader = new CsvReader(reader))
                    {
                        records = csvReader.GetRecords<Animal>().ToList();
                    }
                }

                var index = records.FindIndex(x => x.AnimalId == animalId);
                if (index >= 0)
                {
                    records[index] = animal;
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        using (CsvWriter csvWriter = new CsvWriter(writer))
                        {
                            csvWriter.WriteRecords(records);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                success = false;
                Console.WriteLine(ex);
                throw;
            }

            return success;
        }

        /// <summary>
        /// Archive one animal (all records are saved, but Status changes).
        /// </summary>
        /// /// <returns></returns>
        public bool Archive(string animalId, string filePath)
        {
            var success = true;
            // Call a Rerieve Stored Procedure
            List<Animal> records = null;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    using (CsvReader csvReader = new CsvReader(reader))
                    {
                        records = csvReader.GetRecords<Animal>().ToList();
                    }
                }

                var index = records.FindIndex(x => x.AnimalId == animalId);
                if (index >= 0)
                {
                    records[index].InShelterState = AnimalStateOption.Returned;
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        using (CsvWriter csvWriter = new CsvWriter(writer))
                        {
                            csvWriter.WriteRecords(records);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                success = false;
                Console.WriteLine(ex);
                throw;
            }

            return success;
        }
    }
}
