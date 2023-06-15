using EspacioPersonaje;

internal class Program
{
    private static void Main(string[] args)
    {
        FabricaDePersonajes fabrica= new FabricaDePersonajes();

        Personaje pj1;

        pj1 = fabrica.crearPersonaje();
        pj1.mostrarPersonaje();
    }
}