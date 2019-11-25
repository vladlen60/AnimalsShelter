using System;
using System.IO;
using AS.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AS.BLTest
{
    [TestClass]
    public class AnimalTest
    {
        [TestMethod]
        public void AnimalEntryValidTest()
        {
            //-- Arrange
            Animal animal = new Animal()
            {
                AnimalType = "Cat",
                AnimalName = "Pussy",
                AnimalAge = 5
            };
            var expected = true;

            //-- Act
            var actual = animal.Validate();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void AnimalInvalidTypeTest()
        {
            //-- Arrange
            Animal animal = new Animal()
            {
                AnimalType = "Bear",
                AnimalName = "Grissly",
                AnimalAge = 5
            };
            var expected = false;

            //-- Act
            var actual = animal.Validate();

            //-- Assert
            Assert.AreEqual(expected, actual);
            //Assert.ThrowsException<Exception>(() => animal.Validate());
        }

        [TestMethod]
        public void AnimalEntryAddTest()
        {
            //-- Arrange
            AnimalRepository animalRepos = new AnimalRepository();
            Animal animal = new Animal()
            {
                AnimalType = "Cat",
                AnimalName = "MyKitty",
                AnimalAge = 5,
                AnimalId = "TESTID"
            };
            var expected = true;

            //-- Act
            animalRepos.Add(animal, "test.csv");
            var animalRetrieved = animalRepos.Retrieve(animal.AnimalId, "test.csv");
            var actual = true;
            Type type = typeof(Animal);
            if (animalRetrieved != null)
            {
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties())
                {
                    var exptValue = type.GetProperty(pi.Name).GetValue(animal);
                    var actValue = type.GetProperty(pi.Name).GetValue(animalRetrieved);
                    if (exptValue != null && !exptValue.Equals(actValue))
                    {
                        actual = false;
                        break;
                    }
                    else if (exptValue == null && actValue != null && !actValue.Equals("") && !actValue.Equals("=\"\""))
                    {
                        actual = false;
                        break;
                    }
                }
            }
            File.Delete("test.csv");            

            //-- Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void AnimalEntryUpdateTest()
        {
            //-- Arrange
            AnimalRepository animalRepos = new AnimalRepository();
            Animal animal = new Animal()
            {
                AnimalType = "Cat",
                AnimalName = "MyKitty",
                AnimalAge = 5,
                AnimalId = "TESTID"
            };
            Animal animalUpdated = new Animal()
            {
                AnimalType = "Cat",
                AnimalName = "MyKitty",
                AnimalAge = float.Parse("5.6"),
                AnimalId = "TESTID"
            };            
            var expected = true;

            //-- Act
            animalRepos.Add(animal, "test.csv");
            animalRepos.Update(animalUpdated, animal.AnimalId, "test.csv");
            var animalRetrieved = animalRepos.Retrieve(animal.AnimalId, "test.csv");
            var actual = true;
            Type type = typeof(Animal);
            if (animalRetrieved != null)
            {
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties())
                {
                    var exptValue = type.GetProperty(pi.Name).GetValue(animalUpdated);
                    var actValue = type.GetProperty(pi.Name).GetValue(animalRetrieved);
                    if (exptValue != null && !exptValue.Equals(actValue))
                    {
                        actual = false;
                        break;
                    }
                    else if (exptValue == null && actValue != null && !actValue.Equals("") && !actValue.Equals("=\"\""))
                    {
                        actual = false;
                        break;
                    }
                }
            }
            File.Delete("test.csv");

            //-- Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void AnimalEntryArchiveTest()
        {
            //-- Arrange
            AnimalRepository animalRepos = new AnimalRepository();
            Animal animal = new Animal()
            {
                AnimalType = "Cat",
                AnimalName = "MyKitty",
                AnimalAge = 5,
                AnimalId = "TESTID"
            };
            var expected = true;

            //-- Act
            animalRepos.Add(animal, "test.csv");
            animalRepos.Archive(animal.AnimalId, "test.csv");
            var actual = true;
            var animalRetrieved = animalRepos.Retrieve(animal.AnimalId, "test.csv");
            if (animalRetrieved.InShelterState != AnimalStateOption.Returned)
            {
                actual = false;
            }            
            File.Delete("test.csv");

            //-- Assert
            Assert.AreEqual(expected, actual);
        }       
    }
}
