using System;

namespace ClinicaSalud.Models
{
   
    public class Pet : Animal, IRegisterable
    {
        public string Breed { get; set; } 
        public Patient Owner { get; set; } // Referencia al dueño para saber de quién es la mascota
        public string Symptom { get; set; }

      
        public Pet(string name, int age, string species, string breed, string symptom)
            : base(name, age, species)
        {
            Breed = breed;
            Symptom = symptom;
        }

       
        public override void MakeSound()
        {
            if (Species.ToLower() == "dog") Console.WriteLine("Woof!");
            else if (Species.ToLower() == "cat") Console.WriteLine("Meow!");
            else Console.WriteLine("Animal sound...");
        }

        // Imprime los datos actuales de la mascota en consola
        public void ShowInfo()
        {
            Console.WriteLine($" - Pet: {Name} ({Species}), Breed: {Breed}, Age: {Age}, Symptom: {Symptom}");
        }

        
        public void Register()
        {
            Console.WriteLine($"Pet {Name} registered successfully.");
        }
    }
}