using EspacioPersonaje;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
internal class Program
{
    private static void Main(string[] args)
    {
        PersonajesJson personajesJson = new PersonajesJson();
        string archivo = "Personajes.json";
        List<Personaje> listaPersonajes = new List<Personaje>();

        if (personajesJson.Existe(archivo))
        {//Si el archivo existe trabajo con el json
            listaPersonajes = personajesJson.LeerPersonajes(archivo);
        }
        else
        {//Si no existe creo 10 intancias random y las agrego
            listaPersonajes = crearListaPersonajes(archivo,personajesJson);
        }


        System.Console.WriteLine("\n\n****************GAME START****************");
        int op;
        var Combate = new Combate();
        do
        {
            System.Console.WriteLine("\n\n************ Menu Principal ************");
            System.Console.WriteLine("              1.Iniciar Juego             ");
            System.Console.WriteLine("              2.Mostrar lista de personajes             ");
            System.Console.WriteLine("              3.Crear nueva lista de personajes            ");
            System.Console.WriteLine("              4.Salir             ");

            int.TryParse(Console.ReadLine(), out op);
            switch (op)
            {
                case 1:
                    Combate.eleccionCombatientes(listaPersonajes);
                    break;
                case 2:
                    Combate.mostrarPersonajes(listaPersonajes);
                    break;
                case 3:
                    listaPersonajes = crearListaPersonajes(archivo,personajesJson);
                    break;
                case 4:
                    System.Console.WriteLine("GAME OVER");
                    break;
                default:
                    System.Console.WriteLine("Opcion incorrecta");
                    break;
            }
        } while (op!=4);
    }


    public static List<Personaje> crearListaPersonajes(string archivo, PersonajesJson personajesJson)
    {
        List<Personaje> listaPersonajes = new List<Personaje>();//Variable donde guardo los personajes
        FabricaDePersonajes fabrica = new FabricaDePersonajes();//Variable que uso para crear personajes
        Personaje pj = new Personaje();
        for (int i = 0; i < 10; i++)
        {
            pj = fabrica.crearPersonaje();
            listaPersonajes.Add(pj);
        }
        personajesJson.GuardarPersonajes(listaPersonajes, archivo);//Los guardo en el json
        return listaPersonajes;
    }
}