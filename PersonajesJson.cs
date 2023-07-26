using System.Text.Json;
namespace EspacioPersonaje
{
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