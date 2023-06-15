namespace EspacioPersonaje
{
    enum Clase{humano, ogro,elfo,goblin, orco,enano}

    enum Nombre{Gimli, Aragorn, Luthor}

    enum Apellido{Flores, Gonzalez, Torres}    
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
        private int salud=100; //100

        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Apodo { get => apodo; set => apodo = value; }
        public DateTime FechaNac { get => fechaNac; set => fechaNac = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        
        public int Salud { get => salud; set => salud = value; }
        public int Armadura { get => armadura; set => armadura = value; }
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


        public Personaje crearPersonaje(){
            Personaje personaje= new Personaje();
            Random rdm = new Random();

            //Datos
            personaje.Nombre = "Lothar";
            personaje.Apodo = "JJJJJJ";

            var enumV = Enum.GetValues(typeof(Clase)).Cast<Clase>().ToArray();
            personaje.Tipo = enumV[rdm.Next(enumV.Length)];

            // personaje.Tipo = Clase.elfo;
            personaje.FechaNac = DateTime.Now;
            personaje.Edad = rdm.Next(0,301);

            //Caracteristicas
            personaje.Velocidad = rdm.Next(1,11);
            personaje.Destreza = rdm.Next(1,11);
            personaje.Fuerza = rdm.Next(1,11);
            personaje.Nivel = rdm.Next(1,11);
            personaje.Armadura = rdm.Next(1,11);
            personaje.Salud = 100;

            

            return personaje;
        }
    }
}