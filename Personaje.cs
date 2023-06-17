using System;
using System.Collections.Generic;
using System.Text.Json;
namespace EspacioPersonaje
{
    enum Clase{humano, ogro,elfo,goblin, orco,enano}

    enum Nombres{Gimli, Aragorn, Luthor}
    // enum Apodo{"The Magnificent,}

    public class Personaje
    {

        //Caracteristicas
        private Clase tipo;
        private string? nombre;
        private string? apodo;
        private DateTime fechaNac;
        private int edad;
        
        private int velocidad;
        private int destreza;//1a 5
        private int fuerza; // 1 a 5
        private int nivel; //1 a 10
        private int armadura;// 1 a 10
        private int salud; //100

        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Apodo { get => apodo; set => apodo = value; }
        public DateTime FechaNac { get => fechaNac; set => fechaNac = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Armadura { get => armadura; set => armadura = value; }
        public int Salud { get => salud; set => salud = value; }
        internal Clase Tipo { get => tipo; set => tipo = value; }

        public void mostrarPersonaje(){
            System.Console.WriteLine(nombre);
            System.Console.WriteLine(apodo);
            System.Console.WriteLine(fechaNac);
            System.Console.WriteLine(edad);
            System.Console.WriteLine(tipo);
            System.Console.WriteLine(velocidad);
        }
    }

    public class FabricaDePersonajes{

        string[] Nombres2 = { "Urhan" ,"Ejamar","Qrutrix","Oruxeor","Ushan","Ugovras","Thalan","Gaelin"};
        string[] Apodos = {"The Magnifecient", "The Dire One","BoneBane","The Wild","Conrad del Rio", "SnakeEyes"};

        public Personaje crearPersonaje(){
            Personaje personaje= new Personaje();
            Random rdm = new Random();

            //Datos
            // personaje.Nombre = (Nombres)rdm.Next(0, (Enum.GetValues(typeof(Nombres)).Cast<Nombres>().ToArray()).Length);
            personaje.Nombre = Nombres2[rdm.Next(Nombres2.Length)];
            personaje.Apodo = Apodos[rdm.Next(Apodos.Length)];

            var enumV = Enum.GetValues(typeof(Clase)).Cast<Clase>().ToArray();
            personaje.Tipo = enumV[rdm.Next(enumV.Length)];

            personaje.Tipo = (Clase)rdm.Next(0, (Enum.GetValues(typeof(Clase)).Cast<Clase>().ToArray()).Length);
            personaje.FechaNac = generarFechaNacimiento();
            personaje.Edad = calcularEdad(personaje.FechaNac);

            //Caracteristicas
            personaje.Velocidad = rdm.Next(1,11);
            personaje.Destreza = rdm.Next(1,11);
            personaje.Fuerza = rdm.Next(1,11);
            personaje.Nivel = rdm.Next(1,11);
            personaje.Armadura = rdm.Next(1,11);
            personaje.Salud = 100;

            

            return personaje;
        }

        public DateTime generarFechaNacimiento(){
            DateTime fechaActual = DateTime.Now;
            Random rand = new Random();
            int maxAnios = 300;
            DateTime fechaMinima = fechaActual.AddYears(-maxAnios);

            int diasTotales = (fechaActual-fechaMinima).Days;
            int randDias = rand.Next(diasTotales);
            DateTime fechaAleatoria = fechaMinima.AddDays(randDias);

            return fechaAleatoria;
        }

        public int calcularEdad(DateTime fechaNacimiento){
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if(fechaNacimiento.Month > DateTime.Now.Month){
                edad--;
            }
            return edad;
        }
    }

    public class PersonajesJson{

        public void GuardarPersonajes(List<Personaje> personajes, string nombArchivo) {
            
            string json = JsonSerializer.Serialize(personajes);
            File.WriteAllText(nombArchivo,json);
        }

        public List<Personaje> LeerPersonajes(string nombArchivo){
            string jsonString = File.ReadAllText(nombArchivo);
            List<Personaje> lista = JsonSerializer.Deserialize<List<Personaje>>(jsonString);
            return lista;
        }


        // public bool Existe( string nombArchivo){
            
        // }



    }
}