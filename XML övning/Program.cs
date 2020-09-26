using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Security;

namespace XML_övning
{
    class Program
    {
        static void Main(string[] args)
        {
            //declaring variables
            List<Pets> pets = new List<Pets>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Pets>));
            pets = LoadInstances(pets, serializer);
            //while loop that handles user input and let's the person choose what methods person wants to run
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Type one of following commands:\n!show pets\n!create pet\n!remove pet\n!quit");
                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "!show pets":
                        ShowInstances(pets);
                        Console.ReadLine();
                        break;
                    case "!create pet":
                        CreateInstance(pets);
                        break;
                    case "!remove pet":
                        Console.WriteLine("Please specify number of the pet you want to remove...");
                        string index = Console.ReadLine();
                        int.TryParse(index, out int indexInt);
                        RemoveInstance(pets, indexInt);
                        break;
                    case "!quit":
                        SaveInstances(pets, serializer);
                        return;
                }
            }
        }

        //Creates new instance of class Pets in list with inputs gotten from user
        static void CreateInstance(List<Pets> pets)
        {
            //asking for inputs to add into class Pets
            Console.WriteLine("Petname");
            string petName = Console.ReadLine();
            Console.WriteLine("Pet description");
            string petDesc = Console.ReadLine();
            Console.WriteLine("pet speed");
            string petSpeedInput = Console.ReadLine();
            //commonly had problems with this string, as it managed to escape the xml document. The securityelement fixes this by replacing invalid xml characters
            petSpeedInput = SecurityElement.Escape(petSpeedInput);
            int.TryParse(petSpeedInput, out int petSpeedInt);
            Random generator = new Random();
            pets.Add(new Pets() { petName = petName, petDesc = petDesc, petSpeed = petSpeedInt, petLifeSpan = new LifeSpan() { lifeSpan = generator.Next(60, 4321) } });
        }
        //Removes class instance from list, taking int as input to specify index in list
        static void RemoveInstance(List<Pets> pets, int index)
        {
            try
            {
                pets.RemoveAt(index);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("No item in this position, Press Enter to go back to menu...");
                Console.ReadLine();
                throw;
            }
        }
        //lists all features in class for every item in list
        static void ShowInstances(List<Pets> pets)
        {
            for (int i = 0; i < pets.Count; i++)
            {
                if (pets[i] != null)
                {
                    Console.WriteLine($"\nPet {i}:");
                }
                Console.WriteLine($"Name: {pets[i].petName}\nDescription: {pets[i].petDesc}\nPet Speed: {pets[i].petSpeed}\nPet lifespan: {pets[i].petLifeSpan.lifeSpan} minutes");
            }
        }
        //writes out the list to an xml document, in xml format
        static void SaveInstances(List<Pets> pets, XmlSerializer serializer)
        {
            //Serializer
            FileStream petFile = File.Open("pets.xml", FileMode.OpenOrCreate);
            serializer.Serialize(petFile, pets);
            petFile.Close();
        }
        //tries to load an xml file, if it manages to find the xml file, it deserializes it and stores the value in the list
        static List<Pets> LoadInstances(List<Pets> pets, XmlSerializer serializer)
        {
            try
            {
                FileStream petStream = File.OpenRead("pets.xml");
                pets = (List<Pets>)serializer.Deserialize(petStream);
                petStream.Dispose();
                return pets;
            }
            catch (Exception)
            {
                Console.WriteLine("Could not find any saved files.");
                return pets;
            }
        }
    }
}
