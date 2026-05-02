using System;
using System.Collections.Generic;
using System.Linq; 
using ClinicaSalud.Models; 

class PatientService
{
    // Método principal para registrar
    public static void RegisterPatient(List<Patient> list)
    {
        Console.WriteLine("\n--- Registration ---");
        Console.Write("Owner ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        // Busco si el dueño ya existe para no pedir sus datos otra vez
        Patient owner = list.FirstOrDefault(p => p.Id == id);
        
        if (owner == null)
        {
            // Si no existe, pido sus datos y lo registro
            Console.WriteLine("New owner! Enter info:");
            Console.Write("Owner Name: "); string n = Console.ReadLine() ?? "";
            Console.Write("Age: "); int a = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Address: "); string addr = Console.ReadLine() ?? "";
            Console.Write("Phone: "); string ph = Console.ReadLine() ?? "";
            
            owner = new Patient(id, n, a, addr, ph);
            list.Add(owner); 
            owner.Register(); 
        }
        else
        {
            
            Console.WriteLine($"Owner found: {owner.Name}. Adding a new pet to their profile...");
        }

       
       
        Console.WriteLine("\n Pet Data ");
        Console.Write("Pet Name: "); string pName = Console.ReadLine() ?? "";
        Console.Write("Species (dog/cat): "); string spec = Console.ReadLine() ?? "";
        Console.Write("Breed: "); string br = Console.ReadLine() ?? "";
        Console.Write("Age: "); int pAge = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Symptom: "); string symp = Console.ReadLine() ?? "";

        // Creo el objeto de la mascota
        Pet pet = new Pet(pName, pAge, spec, br, symp);
        
        
        pet.Owner = owner;
        owner.Pets.Add(pet); // Aquí es donde la mascota queda guardada dentro del dueño
        
       
        pet.Register(); 
        pet.MakeSound(); 
    }

    // Muestra todos los dueños y cada uno, sus mascotas
    public static void ListAll(List<Patient> list)
    {
        if (list.Count == 0) { Console.WriteLine("The list is empty."); return; }
        foreach (var o in list)
        {
            o.ShowInfo(); // Datos del dueño
            foreach (var p in o.Pets) 
            {
                p.ShowInfo(); // Datos de cada mascota que tiene ese dueño
            }
        }
    }

    // Busca una mascota en todas las listas de todos los dueños
    public static void SearchPet(List<Patient> list)
    {
        Console.Write("Enter pet name to search: ");
        string name = Console.ReadLine()?.ToLower() ?? "";
        bool found = false;
        foreach (var o in list)
        {
            foreach (var p in o.Pets)
            {
                if (p.Name.ToLower() == name)
                {
                    Console.WriteLine($"Found! {p.Name} belongs to Owner: {o.Name}");
                    found = true;
                }
            }
        }
        if (!found) Console.WriteLine("Pet not found.");
    }

    // Práctica de diccionarios usando el ID del dueño como llave
    public static void PracticeDictionary(List<Patient> list)
    {
        if (list.Count == 0) { Console.WriteLine("No owners to add to dictionary."); return; }
        var dict = list.ToDictionary(p => p.Id, p => p);
        Console.WriteLine("Dictionary updated with current owners.");
    }

    // Filtra solo los perros usando LINQ
    public static void ShowLinqFilters(List<Patient> list)
    {
        var dogs = list.SelectMany(o => o.Pets).Where(p => p.Species.ToLower() == "dog");
        if (!dogs.Any()) { Console.WriteLine("No dogs found."); return; }
        foreach (var d in dogs) Console.WriteLine($"Dog: {d.Name}, Age: {d.Age}");
    }

    // Muestra cuántos dueños y mascotas hay en total
    public static void ShowStats(List<Patient> list)
    {
        int totalPets = list.Sum(o => o.Pets.Count);
        Console.WriteLine($"Total owners: {list.Count}");
        Console.WriteLine($"Total pets in clinic: {totalPets}");
    }

    // Busca la mascota y cambia su síntoma
    public static void UpdateSymptom(List<Patient> list)
    {
        Console.Write("Pet name: ");
        string name = Console.ReadLine()?.ToLower() ?? "";
        foreach (var o in list)
        {
            foreach (var p in o.Pets)
            {
                if (p.Name.ToLower() == name) 
                { 
                    Console.Write("New Symptom: "); 
                    p.Symptom = Console.ReadLine() ?? ""; 
                    Console.WriteLine("Updated successfully.");
                    return;
                }
            }
        }
        Console.WriteLine("Pet not found.");
    }

    // Elimina una mascota de la lista interna del dueño
    public static void RemovePet(List<Patient> list)
    {
        Console.Write("Name of pet to remove: ");
        string name = Console.ReadLine()?.ToLower() ?? "";
        foreach (var o in list)
        {
            var p = o.Pets.FirstOrDefault(x => x.Name.ToLower() == name);
            if (p != null) 
            { 
                o.Pets.Remove(p); 
                Console.WriteLine("Pet removed."); 
                return; 
            }
        }
        Console.WriteLine("Pet not found.");
    }

   
    public static void AssignService(List<Patient> list)
    {
        Console.Write("Pet name: ");
        string name = Console.ReadLine()?.ToLower() ?? "";
      
        var pet = list.SelectMany(o => o.Pets).FirstOrDefault(p => p.Name.ToLower() == name);
        if (pet != null)
        {
            Console.WriteLine("1. Consultation | 2. Vaccination");
            string opt = Console.ReadLine() ?? "";
            
            // Uso la clase abstracta para manejar cualquier servicio
            VeterinaryService s = opt == "1" ? new GeneralConsultation() : new Vaccination();
            s.Attend(); 
        }
        else Console.WriteLine("Pet not found.");
    }
}

class Program
{
    // Menú principal organizado verticalmente
    static void Main()
    {
        List<Patient> owners = new List<Patient>(); 
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== CLINIC MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Register Pet & Owner");
            Console.WriteLine("2. List All Records");
            Console.WriteLine("3. Search Pet by Name");
            Console.WriteLine("4. Dictionary Practice");
            Console.WriteLine("5. LINQ Filters (Dogs)");
            Console.WriteLine("6. Statistics & Reports");
            Console.WriteLine("7. Update Pet Symptom");
            Console.WriteLine("8. Remove Pet from System");
            Console.WriteLine("9. Assign Veterinary Service");
            Console.WriteLine("10. Exit System");
            Console.Write("Option: "); 

            string c = Console.ReadLine() ?? "";
            
            switch (c)
            {
                case "1": PatientService.RegisterPatient(owners); break;
                case "2": PatientService.ListAll(owners); break;
                case "3": PatientService.SearchPet(owners); break;
                case "4": PatientService.PracticeDictionary(owners); break;
                case "5": PatientService.ShowLinqFilters(owners); break;
                case "6": PatientService.ShowStats(owners); break; 
                case "7": PatientService.UpdateSymptom(owners); break;
                case "8": PatientService.RemovePet(owners); break;
                case "9": PatientService.AssignService(owners); break;
                case "10": 
                    Console.WriteLine("Closing system. Goodbye!");
                    running = false; 
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}