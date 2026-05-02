using System;

namespace ClinicaSalud.Models
{
   
    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Species { get; set; }

        // Constructor para obligar a poner nombre, edad y especie al crear el objeto
        public Animal(string name, int age, string species)
        {
            Name = name;
            Age = age;
            Species = species;
        }

        
        public virtual void MakeSound()
        {
            Console.WriteLine("Making an animal sound...");
        }
    }
}