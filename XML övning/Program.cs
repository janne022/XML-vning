using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

namespace XML_övning
{
    class Program
    {
        static void Main(string[] args)
        {
            //declaring variables
            List<Pets> pets = new List<Pets>();

            //asking for inputs to add into class Pets
            Console.WriteLine("Petname");
            string petName = Console.ReadLine();
            Console.WriteLine("Pet description");
            string petDesc = Console.ReadLine();
            Console.WriteLine("pet speed");
            string petSpeed = Console.ReadLine();
            bool success = int.TryParse(petSpeed, out int petSpeedInt);
            for (int i = 0; i < 2; i++)
            {
                pets.Add(new Pets() { petName = petName, petDesc = petDesc, petSpeed = petSpeedInt });
            }


            //Serializer
            XmlSerializer serializer = new XmlSerializer(typeof(List<Pets>));
            FileStream myFile = File.Open("pets.xml", FileMode.OpenOrCreate);
            serializer.Serialize(myFile, pets);

            pets = (List<Pets>)serializer.Deserialize(myFile);


            myFile.Close();
        }
    }
}
