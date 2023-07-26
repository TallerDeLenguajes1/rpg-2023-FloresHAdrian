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

        public void mostrarPersonajeResumido()
        {
            System.Console.WriteLine("      Nombre:" + nombre + "\"" + apodo + "\", Edad: " + edad);
        }

        public void mostrarPersonajeTodo()
        {
            System.Console.WriteLine(nombre);
            System.Console.WriteLine(apodo);
            System.Console.WriteLine(fechaNac);
            System.Console.WriteLine(edad);
            System.Console.WriteLine(tipo);
            System.Console.WriteLine(velocidad);
            System.Console.WriteLine(cervezaFavorita);
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
            personaje.CervezaFavorita = elegirCerveza();

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
                            var rand = new Random();
                            Cerveza cer = Cervezas[rand.Next(0, Cervezas.Count - 1)];
                            return cer.Nombre;
                        }

                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API");
            }
            return "Cerveza Desconocida";
        }
    }

    public class Combate
    {

        public int calcularDanio(Personaje atacante, Personaje defensor)
        {
            var rand = new Random();
            int ataque = atacante.Destreza * atacante.Fuerza * atacante.Nivel;
            int efectividad = rand.Next(0, 101);
            int defensa = defensor.Armadura * defensor.Velocidad;
            int ajuste = 100;
            var danio = ((ataque * efectividad) - defensa) / ajuste;
            return danio;
        }

        public Personaje ganadorCombate(Personaje pj1, Personaje pj2)
        {
            int band = 0;
            var rand = new Random();
            var cont = rand.Next(1, 3);
            int danio = 0;
            Personaje atacante, defensor, aux;

            if (cont == 1)
            {
                atacante = pj1;
                defensor = pj2;
            }
            else
            {
                atacante = pj2;
                defensor = pj1;
            }
            while (band != 1)
            {
                //Aqui va mensaje de quien ataca y quien defiende
                danio = calcularDanio(atacante, defensor);
                defensor.Salud -= danio;
                System.Console.WriteLine($"{atacante.Nombre}, {atacante.Apodo}(Salud: {atacante.Salud});ataca a {defensor.Nombre}, {defensor.Apodo}(Salud:{defensor.Salud}) ");

                //Aqui va mensaje de daño echo
                if (defensor.Salud <= 0)
                     band = 1;
                else
                {
                    aux = atacante;
                    atacante = defensor;
                    defensor = aux;
                }

            }
            System.Console.WriteLine($"{atacante.Nombre} Es el ganador del combate");
            return atacante;
        }

        public void eleccionCombatientes(List<Personaje> personajes)
        {
            mostrarPersonajes(personajes);
            int opc = 0;
            int band = 1;
            var rand = new Random();
            Personaje ganador;
            //Bucle para la eleccion del personaje a seguir
            do
            {
                System.Console.WriteLine("Elejir un personaje a seguir:");
            } while (!int.TryParse(Console.ReadLine(), out opc) || opc < 0 || opc > personajes.Count - 1);
            var jugador1 = personajes[opc];
            personajes.Remove(jugador1);
            var jugador2 = personajes[rand.Next(0, personajes.Count)];//Asigno aleatorimente el contrincante
            personajes.Remove(jugador2);

            //Combate de toda la lista de jugadores o hasta que salga(implementar)
            do
            {
                ganador = ganadorCombate(jugador1, jugador2);
                //Agregar bonificacion de atributo
                //Mensaje desea continuar combate
                if (ganador.Equals(jugador1))
                {
                    jugador2 = personajes[rand.Next(0, personajes.Count)];//Asigno aleatorimente el contrincante
                    personajes.Remove(jugador2);
                }
                else{
                    System.Console.WriteLine("\n Mala suerte, tu personaje no lo logro");
                    band=1;
                }

            } while (personajes.Count > 0 && band == 1);


            System.Console.WriteLine($"{jugador1.Nombre} ES EL GRAN GANADOR DEL TORNEO");

        }

        public void mostrarPersonajes(List<Personaje> personajes)
        {
            int cont = 0;
            System.Console.WriteLine("***********Listado de Personajes***********");
            foreach (var item in personajes)
            {
                System.Console.WriteLine("══════════════════════════════════════════════════════");
                System.Console.WriteLine("Indice =" + cont);
                item.mostrarPersonajeResumido();
                cont++;
            }
        }
    }
}
