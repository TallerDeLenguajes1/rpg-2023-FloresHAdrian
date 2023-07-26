using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
namespace EspacioPersonaje
{
    public enum Clase { humano, ogro, elfo, goblin, orco, enano }


    public class Personaje
    {

        //Caracteristicas
        private Clase tipo;
        private string? nombre;
        private string? apodo;
        private DateTime fechaNac;
        private int edad;

        private string cervezaFavorita;

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
        public Clase Tipo { get => tipo; set => tipo = value; }
        public string CervezaFavorita { get => cervezaFavorita; set => cervezaFavorita = value; }

        public void mostrarPersonaje()
        {
            System.Console.WriteLine(nombre);
            System.Console.WriteLine(apodo);
            System.Console.WriteLine(fechaNac);
            System.Console.WriteLine(edad);
            System.Console.WriteLine(tipo);
            System.Console.WriteLine(velocidad);
        }
    }


    public static class Constantes
    {
        public static string[] Tipos = { "Humano", "Orco", "Elfo", "Enano", "Gnomo" };
        public static string[] Nombres2 = { "Urhan", "Ejamar", "Qrutrix", "Oruxeor", "Ushan", "Ugovras", "Thalan", "Gaelin" };
        public static string[] Apodos = { "The Magnifecient", "The Dire One", "BoneBane", "The Wild", "Conrad del Rio", "SnakeEyes" };
    }

    public class FabricaDePersonajes
    {
        public Personaje crearPersonaje()
        {
            Personaje personaje = new Personaje();
            Random rdm = new Random();

            //Datos
            // personaje.Nombre = (Nombres)rdm.Next(0, (Enum.GetValues(typeof(Nombres)).Cast<Nombres>().ToArray()).Length);

            // var enumV = Enum.GetValues(typeof(Clase)).Cast<Clase>().ToArray();
            // personaje.Tipo = enumV[rdm.Next(enumV.Length)];
            personaje.Nombre = Constantes.Nombres2[rdm.Next(Constantes.Nombres2.Length)];
            personaje.Apodo = Constantes.Apodos[rdm.Next(Constantes.Apodos.Length)];
            personaje.Tipo = (Clase)rdm.Next(0, (Enum.GetValues(typeof(Clase)).Cast<Clase>().ToArray()).Length);
            personaje.FechaNac = generarFechaNacimiento();
            personaje.Edad = calcularEdad(personaje.FechaNac);

            //Caracteristicas
            personaje.Velocidad = rdm.Next(1, 11);
            personaje.Destreza = rdm.Next(1, 11);
            personaje.Fuerza = rdm.Next(1, 11);
            personaje.Nivel = rdm.Next(1, 11);
            personaje.Armadura = rdm.Next(1, 11);
            personaje.Salud = 100;



            return personaje;
        }

        public DateTime generarFechaNacimiento()
        {
            DateTime fechaActual = DateTime.Now;
            Random rand = new Random();
            // int maxAnios = 300;
            // DateTime fechaMinima = fechaActual.AddYears(-maxAnios);

            // int diasTotales = (fechaActual-fechaMinima).Days;
            // int randDias = rand.Next(diasTotales);
            DateTime fechaAleatoria = new DateTime(rand.Next(fechaActual.Year - 300, fechaActual.Year), rand.Next(1, 13), rand.Next(1, 30));
            return fechaAleatoria;
        }

        public int calcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Month > DateTime.Now.Month)
            {
                edad--;
            }
            return edad;
        }

        public string elegirCerveza()
        {
            var url = $"https://api.punkapi.com/v2/beers";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "Desconocido";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            var Cervezas = JsonSerializer.Deserialize<List<Cerveza>>(responseBody);
                            // foreach (var cerveza in Cervezas)
                            // {
                            //     Console.WriteLine("Nombre: " + cerveza.Nombre);
                            // }
                        //AQUI TENGO QUE AGREGAR QUE DEVUELVA UNA CERVEZA RANDOM

                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API");
            }
            return "Cerveza";
        }
    }

    public class PersonajesJson
    {

        public void GuardarPersonajes(List<Personaje> personajes, string nombArchivo)
        {
            string json = JsonSerializer.Serialize(personajes);
            File.WriteAllText(nombArchivo, json);
        }

        public List<Personaje> LeerPersonajes(string nombArchivo)
        {
            string jsonString = File.ReadAllText(nombArchivo);
            List<Personaje> lista = JsonSerializer.Deserialize<List<Personaje>>(jsonString);
            return lista;
        }


        public bool Existe(string nombArchivo)
        {
            return File.Exists(nombArchivo) && new FileInfo(nombArchivo).Length > 0;
        }

    }
}