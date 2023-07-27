using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
namespace EspacioPersonaje
{
    // public enum @string { humano, ogro, elfo, goblin, orco, enano }


    public class Personaje
    {

        //Caracteristicas
        private string tipo;
        private string? nombre;
        private string? apodo;
        private DateTime fechaNac;
        private int edad;

        private string cervezaFavorita;
        private string embriaguez;

        private int velocidad;
        private int destreza;//1a 5
        private int fuerza; // 1 a 5
        private int nivel; //1 a 10
        private int armadura;// 1 a 10
        private int salud; //100
        private int saludMaxima;

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
        public string Tipo { get => tipo; set => tipo = value; }
        public string CervezaFavorita { get => cervezaFavorita; set => cervezaFavorita = value; }
        public int SaludMaxima { get => saludMaxima; set => saludMaxima = value; }
        public string Embriaguez { get => embriaguez; set => embriaguez = value; }

        public void mostrarPersonajeResumido()
        {
            System.Console.WriteLine("      Nombre: " + nombre + " \"" + apodo + "\",Tipo: " + tipo + ", Edad: " + edad);
            System.Console.WriteLine($" Nivel: {Nivel}, Fuerza: {Fuerza}, Destreza: {Destreza}, Armadura: {Armadura}");
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
        public static string[] EstadoEmbriaguez = { "Sobrio", "Ebriedad Leve", "Ebriedad Moderada", "Ebriedad Severa" };
    }

    public class FabricaDePersonajes
    {
        public Personaje crearPersonaje()
        {
            Personaje personaje = new Personaje();
            Random rdm = new Random();

            personaje.Nombre = Constantes.Nombres2[rdm.Next(Constantes.Nombres2.Length)];
            personaje.Apodo = Constantes.Apodos[rdm.Next(Constantes.Apodos.Length)];
            // personaje.Tipo = (Clase)rdm.Next(0, (Enum.GetValues(typeof(Clase)).Cast<Clase>().ToArray()).Length);
            personaje.Tipo = Constantes.Tipos[rdm.Next(Constantes.Tipos.Length)];
            personaje.FechaNac = generarFechaNacimiento();
            personaje.Edad = calcularEdad(personaje.FechaNac);
            personaje.CervezaFavorita = elegirCerveza();
            personaje.Embriaguez = Constantes.EstadoEmbriaguez[rdm.Next(Constantes.EstadoEmbriaguez.Length)];
            //Caracteristicas
            personaje.Velocidad = rdm.Next(1, 11);
            personaje.Destreza = rdm.Next(1, 6);
            personaje.Fuerza = rdm.Next(1, 11);
            personaje.Nivel = rdm.Next(1, 11);
            personaje.Armadura = rdm.Next(1, 11);
            personaje.Salud = 100;
            personaje.SaludMaxima = 100;

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
        //Metodo que devuelve el daño echo por el atacante
        public int calcularDanio(Personaje atacante, Personaje defensor)
        {
            var rand = new Random();
            int ataque = atacante.Destreza * atacante.Fuerza * atacante.Nivel;
            int efectividad = rand.Next(0, 101);
            int defensa = defensor.Armadura * defensor.Velocidad;
            int ajuste = 300;//Ajuste menor al pedido, porque sino demora 20 rondas cada combate
            int ajusteAlcoholico = 0;
            if (atacante.Embriaguez != "Sobrio")
            {
                
                switch (atacante.Embriaguez)
                {
                    case "Ebriedad Leve":
                        ajusteAlcoholico = 20;
                        break;
                    case "Ebriedad Moderada":
                        ajusteAlcoholico = 50;
                        break;
                    case "Ebriedad Severa":
                        ajusteAlcoholico = 100;
                        break;
                }
                if (atacante.Tipo == "Enano")
                    ajusteAlcoholico *= -1;
            }
            var danio = ((ataque * efectividad) - defensa) / (ajuste+ajusteAlcoholico);

            return danio;
        }

        public Personaje ganadorCombate(Personaje pj1, Personaje pj2)
        {
            // int band = 0;
            // var rand = new Random();
            var cont = caraCruz();
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
            while (true)
            {
                danio = calcularDanio(atacante, defensor);
                System.Console.WriteLine($"{atacante.Nombre}, {atacante.Apodo}(Salud: {atacante.Salud});ataca a {defensor.Nombre}, {defensor.Apodo}(Salud:{defensor.Salud}) con un daño de {danio}");
                Thread.Sleep(100);
                defensor.Salud -= danio;

                if (defensor.Salud <= 0)
                    break; //Es necesario una bandera? es mejor usar un break?
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
            int opc, opc2;
            var rand = new Random();
            Personaje ganador;
            //Bucle para la eleccion del personaje a seguir
            do
            {
                System.Console.WriteLine("\n\nElejir un personaje a seguir:");
            } while (!int.TryParse(Console.ReadLine(), out opc) || opc < 0 || opc > personajes.Count - 1);
            var jugador1 = personajes[opc];
            System.Console.WriteLine($"El personaje elegido es");
            jugador1.mostrarPersonajeResumido();
            Thread.Sleep(1000);
            personajes.Remove(jugador1);
            var jugador2 = personajes[rand.Next(0, personajes.Count)];//Asigno aleatorimente el contrincante
            personajes.Remove(jugador2);

            //Combate de toda la lista de jugadores o hasta que decida salir
            do
            {
                ganador = ganadorCombate(jugador1, jugador2);
                do
                {
                    System.Console.WriteLine("  ¿Desea continuar el combate?\n1. Si\n2. No");
                } while (!int.TryParse(Console.ReadLine(), out opc2) || opc2 < 1 || opc2 > 2);
                if (opc2 == 2) break;

                if (ganador.Equals(jugador1))
                {
                    mejorarGanador(ganador);
                    jugador2 = personajes[rand.Next(0, personajes.Count)];//Asigno aleatorimente el contrincante
                    personajes.Remove(jugador2);
                }
                else
                {
                    System.Console.WriteLine("\n\n Mala suerte, tu personaje no lo logro");
                    break;
                }

            } while (personajes.Count >= 0);

            if (ganador.Equals(jugador1))
            {
                if (personajes.Count > 0)
                    System.Console.WriteLine("\nLastima no ganaste, pero sigues con vida y mas fuerte felicidades\n");
                else
                    System.Console.WriteLine($"\nFELICIDADES {ganador.Nombre}, \"{ganador.Apodo}\" eres el CAMPEON que derroto a todos\n\n");
            }


        }

        public void mejorarGanador(Personaje ganador)
        {
            var rand = new Random();
            int op = rand.Next(1, 6);
            double recuperacionSalud = ganador.SaludMaxima * 0.5;
            ganador.Salud = ganador.Salud + (int)recuperacionSalud;
            if (ganador.Salud > ganador.SaludMaxima)
                ganador.Salud = ganador.SaludMaxima;

            System.Console.WriteLine($"{ganador.Nombre} Va recibir una mejora aleatoria...");
            switch (op)
            {
                case 1:
                    System.Console.WriteLine("Se mejora la velocidad en 5");
                    ganador.Velocidad += 5;
                    break;
                case 2:
                    System.Console.WriteLine("Se mejora la destreza en 2");
                    ganador.Destreza += 2;
                    break;
                case 3:
                    System.Console.WriteLine("Se mejora la Fuerza en 5");
                    ganador.Fuerza += 2;
                    break;
                case 4:
                    System.Console.WriteLine("Se mejora la Armadura en 5");
                    ganador.Armadura += 5;
                    break;
                case 5:
                    System.Console.WriteLine("Se mejora la Salud en 10");
                    ganador.Salud += 10;
                    ganador.SaludMaxima += 10;
                    break;
                default:
                    break;
            }
            // return ganador;
        }

        public void mostrarPersonajes(List<Personaje> personajes)
        {
            int cont = 0;
            System.Console.WriteLine("***********Listado de Personajes***********");
            foreach (var item in personajes)
            {
                System.Console.WriteLine("════════════");
                System.Console.WriteLine("Indice =" + cont);
                item.mostrarPersonajeResumido();
                cont++;
            }
        }

        public int caraCruz()
        {
            int op;
            string cara = "Cara";
            string cruz = "Cruz";
            System.Console.WriteLine("Decidamos quien da el primer golpe, elija:");
            do
            {
                System.Console.WriteLine($"1. {cara}\n2. {cruz}");
            } while (!int.TryParse(Console.ReadLine(), out op) || op < 1 || op > 2);
            var rand = new Random();
            var cont = rand.Next(1, 3);

            if (op == cont)
            {
                System.Console.WriteLine("Ganaste, ahora tienes el primer golpe");
                return 1;
            }
            else
            {
                System.Console.WriteLine("Gano el contrincante el tiene el primer golpe");
                return 0;
            }
        }

    }
}
