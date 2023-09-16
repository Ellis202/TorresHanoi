using System;
using System.Collections.Generic;

// Clase que representa una torre o pila de discos
class Torre
{
    // Atributo que almacena los discos como una lista de enteros
    private List<int> discos;

    // Constructor que recibe el número de discos iniciales
    public Torre(int n)
    {
        // Inicializa la lista de discos
        discos = new List<int>();
        // Agrega los discos de mayor a menor
        for (int i = n; i > 0; i--)
        {
            discos.Add(i);
        }
    }

    // Constructor vacío que crea una torre sin discos
    public Torre()
    {
        // Inicializa la lista de discos vacía
        discos = new List<int>();
    }

    // Método que devuelve el número de discos en la torre
    public int Contar()
    {
        return discos.Count;
    }

    // Método que devuelve el disco superior de la torre, o 0 si está vacía
    public int Ver()
    {
        if (discos.Count > 0)
        {
            return discos[discos.Count - 1];
        }
        else
        {
            return 0;
        }
    }

    // Método que quita y devuelve el disco superior de la torre, o 0 si está vacía
    public int Sacar()
    {
        if (discos.Count > 0)
        {
            int disco = discos[discos.Count - 1];
            discos.RemoveAt(discos.Count - 1);
            return disco;
        }
        else
        {
            return 0;
        }
    }

    // Método que agrega un disco a la torre, si es menor que el anterior
    public bool Agregar(int disco)
    {
        if (disco > 0 && (discos.Count == 0 || disco <= Ver()))
        {
            discos.Add(disco);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Método que imprime la torre como una cadena de caracteres
    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < discos.Count; i++)
        {
            s += discos[i] + " ";
        }
        return s;
    }
}

// Clase que representa el juego de las torres de Hanoi
class Juego
{
    // Atributos que representan las tres torres del juego
    private Torre origen;
    private Torre destino;
    private Torre auxiliar;

    // Atributo que almacena el número de movimientos realizados
    private int movimientos;

    // Constructor que recibe el número de discos iniciales
    public Juego(int n)
    {
        // Crea las tres torres, la primera con los discos iniciales y las otras vacías
        origen = new Torre(n);
        destino = new Torre();
        auxiliar = new Torre();

        // Inicializa el contador de movimientos a cero
        movimientos = 0;

        // Imprime el estado inicial del juego
        Imprimir();

        // Indica al jugador cómo jugar
        Console.WriteLine("Para mover un disco, ingresa dos letras que representen las torres origen y destino.");
        Console.WriteLine("Por ejemplo, OD significa mover un disco de la torre origen a la torre destino.");
        Console.WriteLine("Las letras válidas son O (origen), D (destino) y A (auxiliar).");
        Console.WriteLine("Para rendirte y ver la solución, ingresa R.");

    }
    // Método que ejecuta el juego
    public void Jugar()
    {
        // Mientras el juego no esté resuelto
        while (!Resuelto())
        {
            // Pide al jugador que ingrese un movimiento
            Console.Write("Ingresa tu movimiento: ");
            string movimiento = Console.ReadLine().ToUpper();

            // Si el movimiento es válido, lo realiza y actualiza el contador de movimientos
            if (Valido(movimiento))
            {
                Mover(movimiento);
                movimientos++;
            }
            // Si el jugador se rinde, llama al método Resolver y termina el juego
            else if (movimiento == "R")
            {
                Console.WriteLine("Te has rendido. Aquí está la solución:");
                // Cambia el orden de los parámetros para que coincida con el método Resolver
                Resolver(destino.Contar(), destino, origen, auxiliar);
                break;
            }
            // Si el movimiento es inválido, le indica al jugador que lo intente de nuevo
            else
            {
                Console.WriteLine("Movimiento inválido. Inténtalo de nuevo.");
            }

            // Imprime el estado actual del juego
            Imprimir();
        }

        // Si el juego está resuelto, felicita al jugador y le muestra el número de movimientos realizados
        if (Resuelto())
        {
            Console.WriteLine("¡Felicidades! Has resuelto el juego.");
            Console.WriteLine("Has realizado {0} movimientos.", movimientos);
        }
    }

    // Método que verifica si el juego está resuelto, es decir, si todos los discos están en la torre destino
    public bool Resuelto()
    {
        return origen.Contar() == 0 && auxiliar.Contar() == 0;
    }

    // Método que verifica si un movimiento es válido, es decir, si tiene dos letras distintas entre O, D y A y si el disco de la torre origen es menor que el de la torre destino
    public bool Valido(string movimiento)
    {
        // Obtiene las torres correspondientes a las letras del movimiento
        Torre t1 = ObtenerTorre(movimiento[0]);
        Torre t2 = ObtenerTorre(movimiento[1]);

        // Verifica si el movimiento tiene dos letras distintas entre O, D y A
        bool letrasValidas = movimiento.Length == 2 && movimiento[0] != movimiento[1] &&
               (movimiento[0] == 'O' || movimiento[0] == 'D' || movimiento[0] == 'A') &&
               (movimiento[1] == 'O' || movimiento[1] == 'D' || movimiento[1] == 'A');

        // Verifica si el disco de la torre origen es menor que el de la torre destino, o si la torre destino está vacía
        bool discosValidos = t1.Ver() > 0 && (t2.Ver() == 0 || t1.Ver() < t2.Ver());

        // Devuelve verdadero si se cumplen ambas condiciones
        return letrasValidas && discosValidos;
    }


    // Método que realiza un movimiento entre dos torres, quitando el disco superior de la primera y agregándolo a la segunda
    public void Mover(string movimiento)
    {
        // Obtiene las torres correspondientes a las letras del movimiento
        Torre t1 = ObtenerTorre(movimiento[0]);
        Torre t2 = ObtenerTorre(movimiento[1]);

        // Quita el disco superior de la primera torre y lo agrega a la segunda
        int disco = t1.Sacar();
        t2.Agregar(disco);

        // Imprime el movimiento realizado
        Console.WriteLine("Has movido el disco {0} de la torre {1} a la torre {2}.", disco, movimiento[0], movimiento[1]);
    }


    // Método que devuelve la torre correspondiente a una letra, O para origen, D para destino y A para auxiliar
    public Torre ObtenerTorre(char letra)
    {
        switch (letra)
        {
            case 'O':
                return origen;
            case 'D':
                return destino;
            case 'A':
                return auxiliar;
            default:
                return null;
        }
    }

    // Método que imprime el estado actual de las tres torres
    public void Imprimir()
    {
        Console.WriteLine("Origen: " + origen);
        Console.WriteLine("Destino: " + destino);
        Console.WriteLine("Auxiliar: " + auxiliar);
        Console.WriteLine();
    }

    // Método que resuelve el juego de forma recursiva, moviendo n discos de la torre origen a la torre destino usando la torre auxiliar
    public void Resolver(int n, Torre origen, Torre destino, Torre auxiliar)
    {
        // Caso base: si no hay discos que mover, termina la recursión
        if (n == 0)
        {
            return;
        }

        // Caso recursivo: 
        // 1. Mueve n-1 discos de la torre origen a la torre auxiliar usando la torre destino
        Resolver(n - 1, origen, auxiliar, destino);

        // 2. Mueve el disco restante de la torre origen a la torre destino
        destino.Agregar(origen.Sacar());

        // 3. Imprime el estado actual del juego
        Imprimir();

        // 4. Mueve n-1 discos de la torre auxiliar a la torre destino usando la torre origen
        Resolver(n - 1, auxiliar, destino, origen);
    }
}

// Programa principal que crea y ejecuta el juego
class Program
{
    static void Main(string[] args)
    {
        // Pide al jugador que elija el nivel de dificultad
        Console.WriteLine("Elige el nivel de dificultad:");
        Console.WriteLine("F - Fácil (3 discos)");
        Console.WriteLine("N - Normal (5 discos)");
        Console.WriteLine("D - Difícil (7 discos)");
        Console.Write("Ingresa tu elección: ");
        string nivel = Console.ReadLine().ToUpper();

        // Crea el juego con el número de discos correspondiente al nivel elegido
        Juego juego;
        switch (nivel)
        {
            case "F":
                juego = new Juego(3);
                break;
            case "N":
                juego = new Juego(5);
                break;
            case "D":
                juego = new Juego(7);
                break;
            default:
                juego = new Juego(3);
                break;
        }

        // Ejecuta el juego
        juego.Jugar();
    }
}