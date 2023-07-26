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
            FabricaDePersonajes fabrica = new FabricaDePersonajes();
            Personaje pj = new Personaje();
            for (int i = 0; i < 10; i++)
            {
                pj = fabrica.crearPersonaje();
                // pj.mostrarPersonajeTodo();
                listaPersonajes.Add(pj);
            }
            personajesJson.GuardarPersonajes(listaPersonajes, archivo);
        }

        var Combate = new Combate();
        Combate.eleccionCombatientes(listaPersonajes);

    }
}