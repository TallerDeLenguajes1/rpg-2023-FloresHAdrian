using EspacioPersonaje;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
internal class Program
{
    private static void Main(string[] args)
    {

        //Agregamos el el consumo de la api
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
                    if (strReader == null) return;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        var Cervezas = JsonSerializer.Deserialize<List<Cerveza>>(responseBody);
                        foreach (var cerveza in Cervezas)
                        {
                            Console.WriteLine("Nombre: " + cerveza.Nombre);
                        }

                    }
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
        }



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
            for (int i = 0; i <= 10; i++)
            {
                pj = fabrica.crearPersonaje();
                pj.mostrarPersonaje();
                listaPersonajes.Add(pj);
            }
            personajesJson.GuardarPersonajes(listaPersonajes, archivo);
        }




        // personajesJson.LeerPersonajes(archivo);

    }
}