using EspacioPersonaje;

internal class Program
{
    private static void Main(string[] args)
    {
        FabricaDePersonajes fabrica= new FabricaDePersonajes();
        PersonajesJson personajesJson = new PersonajesJson();
        string archivo = "Personajes.json";


        List<Personaje> listaPersonajes = new List<Personaje>();

        for (int i = 0; i <= 10; i++)
        {
            listaPersonajes.Add(fabrica.crearPersonaje());
        }

        personajesJson.GuardarPersonajes(listaPersonajes, archivo);
        personajesJson.LeerPersonajes(archivo);
                
    }
}