using System;
using System.Collections.Generic;

namespace ClinicaSalud.Models
{
   
    public class Patient : IRegisterable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

      
        private string _phoneNumber;
        
       
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

       
        public List<Pet> Pets { get; set; }

        public Patient(int id, string name, int age, string address, string phoneNumber)
        {
            Id = id;
            Name = name;
            Age = age;
            Address = address;
            PhoneNumber = phoneNumber; 
            // Inicializo la lista para que no de error "null" al intentar agregar mascotas
            Pets = new List<Pet>(); 
        }

        // Muestro los datos personales del dueño
        public void ShowInfo()
        {
            Console.WriteLine($"OWNER: {Name} (ID: {Id}), Address: {Address}, Phone: {PhoneNumber}");
        }

        public void Register()
        {
            Console.WriteLine($"Owner {Name} registered successfully.");
        }
    }
}