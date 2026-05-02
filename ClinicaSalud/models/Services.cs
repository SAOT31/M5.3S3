using System;

namespace ClinicaSalud.Models
{
   
    public interface IRegisterable
    {
        void Register();
    }

 
    public abstract class VeterinaryService
    {
      
        public abstract void Attend();
    }

   
    public class GeneralConsultation : VeterinaryService
    {
        public override void Attend() { Console.WriteLine("Attending general consultation."); }
    }

    
    public class Vaccination : VeterinaryService
    {
        public override void Attend() { Console.WriteLine("Applying vaccination."); }
    }
}