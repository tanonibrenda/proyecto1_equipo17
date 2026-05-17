
#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;

/* ====================================================================
 * Proyecto 1, TEMA 1: Liga deportiva C#
 * Grupo 17
 *
 * Sistema de gestión para una liga deportiva
 * ==================================================================== */

namespace LigaDeportiva
{
    // ==================== 1. ENUMERADOS ====================
    enum Categoria { Infantil, Cadetes, Juveniles, Primera, Veteranos }

    // ==================== 2. ESTRUCTURAS ====================

    /// <summary>
    /// Representa un jugador de la liga
    /// </summary>
    struct Jugador
    {
        public int DNI;
        public string Nombre;
        public string Apellido;
        public int Edad;
        public List<string> EquiposAsignados;
        public bool Seguro;
        public bool Afiliacion;
        public Categoria Categoria;
    }

    /// <summary>
    /// Representa un equipo de la liga
    /// </summary>
    struct Equipo
    {
        public string Nombre;
        public string Club;
        public Categoria Cat;
        public List<int> DNIsJugadores;
    }

    // ==================== 3. PROGRAMA PRINCIPAL ====================
    class Program
    {
        static void Main()
        {
            List<Jugador> jugadores = [];
            List<Equipo> equipos = [];

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== LIGA DEPORTIVA ===");
                Console.WriteLine("1. ABM Jugadores");
                Console.WriteLine("2. ABM Equipos");
                Console.WriteLine("3. Reportes");
                Console.WriteLine("0. Salir");
                Console.Write("Opcion: ");

                string op = Console.ReadLine();
                if (op == "0") break;

                switch (op)
                {
                    case "1": MenuJugadores(jugadores, equipos); break;
                    case "2": MenuEquipos(equipos, jugadores); break;
                    case "3": MenuReportes(jugadores, equipos); break;
                    default: Console.WriteLine("Opción no válida"); Console.ReadKey(); break;
                }
            }
        }

        // ==================== 4. ABM JUGADORES ====================

        static void MenuJugadores(List<Jugador> jugadores, List<Equipo> equipos)
        {
            Console.Clear();
            Console.WriteLine("=== ABM JUGADORES ===");
            Console.WriteLine("1. Alta");
            Console.WriteLine("2. Baja");
            Console.WriteLine("3. Modificar");
            Console.WriteLine("4. Listar");
            Console.Write("Opcion: ");
            string op = Console.ReadLine();

            switch (op)
            {
                case "1": AltaJugador(jugadores); break;
                case "2": BajaJugador(jugadores, equipos); break;
                case "3": ModificarJugador(jugadores); break;
                case "4": ListarJugadores(jugadores); break;
                default: Console.WriteLine("Opción no válida"); Console.ReadKey(); break;
            }
        }

        /// <summary>
        /// Alta de un nuevo jugador con validaciones de DNI único y edad-categoría
        /// </summary>
        static void AltaJugador(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== ALTA JUGADOR ===");

            Console.Write("DNI: ");
            if (!int.TryParse(Console.ReadLine(), out int dni))
            {
                Console.WriteLine("DNI inválido.");
                Console.ReadKey();
                return;
            }

            if (jugadores.Exists(j => j.DNI == dni))
            {
                Console.WriteLine("Error: Ya existe un jugador con ese DNI.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Edad: ");
            if (!int.TryParse(Console.ReadLine(), out int edad))
            {
                Console.WriteLine("Edad inválida.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Categorias: 0-Infantil 1-Cadetes 2-Juveniles 3-Primera 4-Veteranos");
            Console.Write("Seleccione categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int catInt) || catInt < 0 || catInt > 4)
            {
                Console.WriteLine("Categoría inválida.");
                Console.ReadKey();
                return;
            }
            Categoria cat = (Categoria)catInt;

            if (!ValidarEdadCategoria(edad, cat))
            {
                Console.WriteLine("Error: La edad no corresponde a la categoria seleccionada.");
                Console.ReadKey();
                return;
            }

            Console.Write("Tiene seguro? s/n: ");
            bool seguro = string.Equals(Console.ReadLine(), "s", StringComparison.OrdinalIgnoreCase);

            Console.Write("Esta afiliado? s/n: ");
            bool afiliacion = string.Equals(Console.ReadLine(), "s", StringComparison.OrdinalIgnoreCase);

            Jugador j = new()
            {
                DNI = dni,
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Categoria = cat,
                Seguro = seguro,
                Afiliacion = afiliacion,
                EquiposAsignados = []
            };

            jugadores.Add(j);
            Console.WriteLine("\nJugador agregado correctamente.");
            Console.ReadKey();
        }

        /// <summary>
        /// Valida que la edad corresponda a la categoría según el reglamento
        /// </summary>
        static bool ValidarEdadCategoria(int edad, Categoria cat)
        {
            return cat switch
            {
                Categoria.Infantil => edad < 13,
                Categoria.Cadetes => edad >= 13 && edad <= 15,
                Categoria.Juveniles => edad >= 16 && edad <= 17,
                Categoria.Primera => edad >= 18 && edad <= 34,
                Categoria.Veteranos => edad >= 35,
                _ => false
            };
        }

        static void BajaJugador(List<Jugador> jugadores, List<Equipo> equipos)
        {
            Console.Clear();
            Console.WriteLine("=== BAJA JUGADOR ===");
            Console.Write("Ingrese DNI del jugador a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int dni))
            {
                Console.WriteLine("DNI inválido.");
                Console.ReadKey();
                return;
            }

            int indice = jugadores.FindIndex(j => j.DNI == dni);
            if (indice == -1)
            {
                Console.WriteLine("No existe jugador con ese DNI.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < equipos.Count; i++)
            {
                Equipo equipo = equipos[i];
                equipo.DNIsJugadores.Remove(dni);
                equipos[i] = equipo;
            }

            jugadores.RemoveAt(indice);
            Console.WriteLine("\nJugador eliminado correctamente.");
            Console.ReadKey();
        }

        static void ModificarJugador(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== MODIFICAR JUGADOR ===");
            Console.Write("Ingrese DNI del jugador a modificar: ");
            if (!int.TryParse(Console.ReadLine(), out int dni))
            {
                Console.WriteLine("DNI inválido.");
                Console.ReadKey();
                return;
            }

            int indice = jugadores.FindIndex(j => j.DNI == dni);
            if (indice == -1)
            {
                Console.WriteLine("No existe jugador con ese DNI.");
                Console.ReadKey();
                return;
            }

            Jugador j = jugadores[indice];

            Console.WriteLine($"\n--- Datos actuales ---");
            Console.WriteLine($"Nombre: {j.Nombre} | Apellido: {j.Apellido} | Edad: {j.Edad}");
            Console.WriteLine($"Categoria: {j.Categoria} | Seguro: {(j.Seguro ? "SI" : "NO")} | Afiliacion: {(j.Afiliacion ? "SI" : "NO")}");

            Console.WriteLine("\n--- Nuevos datos (Enter para mantener) ---");

            Console.Write($"Nuevo nombre [{j.Nombre}]: ");
            string nombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nombre)) j.Nombre = nombre;

            Console.Write($"Nuevo apellido [{j.Apellido}]: ");
            string apellido = Console.ReadLine();
            if (!string.IsNullOrEmpty(apellido)) j.Apellido = apellido;

            Console.Write($"Nueva edad [{j.Edad}]: ");
            string edadStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(edadStr) && int.TryParse(edadStr, out int nuevaEdad))
            {
                if (!ValidarEdadCategoria(nuevaEdad, j.Categoria))
                {
                    Console.WriteLine("Error: La nueva edad no es compatible con la categoría actual.");
                    Console.ReadKey();
                    return;
                }
                j.Edad = nuevaEdad;
            }

            Console.Write($"Nuevo seguro (s/n) [{(j.Seguro ? "s" : "n")}]: ");
            string seguroStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(seguroStr))
                j.Seguro = string.Equals(seguroStr, "s", StringComparison.OrdinalIgnoreCase);

            Console.Write($"Nueva afiliacion (s/n) [{(j.Afiliacion ? "s" : "n")}]: ");
            string afiliacionStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(afiliacionStr))
                j.Afiliacion = string.Equals(afiliacionStr, "s", StringComparison.OrdinalIgnoreCase);

            jugadores[indice] = j;
            Console.WriteLine("\nJugador modificado correctamente.");
            Console.ReadKey();
        }

        static void ListarJugadores(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE JUGADORES ===");
            if (jugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores cargados.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("DNI\tNombre\tApellido\tEdad\tCategoria\tSeguro\tAfiliacion");
            Console.WriteLine("------------------------------------------------------------------");

            foreach (var j in jugadores)
            {
                Console.WriteLine($"{j.DNI}\t{j.Nombre}\t{j.Apellido}\t{j.Edad}\t{j.Categoria}\t{(j.Seguro ? "SI" : "NO")}\t{(j.Afiliacion ? "SI" : "NO")}");
            }
            Console.ReadKey();
        }

        // ==================== 5. ABM EQUIPOS ====================

        static void MenuEquipos(List<Equipo> equipos, List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== ABM EQUIPOS ===");
            Console.WriteLine("1. Alta");
            Console.WriteLine("2. Baja");
            Console.WriteLine("3. Modificar");
            Console.WriteLine("4. Listar");
            Console.WriteLine("5. Asignar Jugador a Equipo");
            Console.Write("Opcion: ");
            string op = Console.ReadLine();

            switch (op)
            {
                case "1": AltaEquipo(equipos); break;
                case "2": BajaEquipo(equipos); break;
                case "3": ModificarEquipo(equipos); break;
                case "4": ListarEquipos(equipos); break;
                case "5": AsignarJugadorAEquipo(equipos, jugadores); break;
                default: Console.WriteLine("Opción no válida"); Console.ReadKey(); break;
            }
        }

        /// <summary>
        /// Alta de equipo con nombre automático: Club + LetraCategoría + Número
        /// Ejemplo: River P1, River P2
        /// </summary>
        static void AltaEquipo(List<Equipo> equipos)
        {
            Console.Clear();
            Console.WriteLine("=== ALTA EQUIPO ===");

            Console.Write("Ingrese el nombre del Club: ");
            string club = Console.ReadLine();

            Console.WriteLine("Categorias: 0-Infantil 1-Cadetes 2-Juveniles 3-Primera 4-Veteranos");
            Console.Write("Seleccione categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int catInt) || catInt < 0 || catInt > 4)
            {
                Console.WriteLine("Categoría inválida.");
                Console.ReadKey();
                return;
            }
            Categoria cat = (Categoria)catInt;

            int contador = 1;
            string nombreBase = club;
            string nombreEquipo = $"{nombreBase} {ObtenerLetraCategoria(cat)}{contador}";

            while (equipos.Exists(e => string.Equals(e.Nombre, nombreEquipo, StringComparison.OrdinalIgnoreCase)))
            {
                contador++;
                nombreEquipo = $"{nombreBase} {ObtenerLetraCategoria(cat)}{contador}";
            }

            equipos.Add(new Equipo
            {
                Nombre = nombreEquipo,
                Club = club,
                Cat = cat,
                DNIsJugadores = []
            });

            Console.WriteLine($"\nEquipo '{nombreEquipo}' agregado correctamente.");
            Console.ReadKey();
        }

        static string ObtenerLetraCategoria(Categoria cat)
        {
            return cat switch
            {
                Categoria.Infantil => "I",
                Categoria.Cadetes => "C",
                Categoria.Juveniles => "J",
                Categoria.Primera => "P",
                Categoria.Veteranos => "V",
                _ => ""
            };
        }

        static void BajaEquipo(List<Equipo> equipos)
        {
            Console.Clear();
            Console.WriteLine("=== BAJA EQUIPO ===");
            ListarEquipos(equipos);

            Console.Write("\nIngrese el nombre del equipo a eliminar: ");
            string nombre = Console.ReadLine();

            int indice = equipos.FindIndex(e => string.Equals(e.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
            if (indice == -1)
            {
                Console.WriteLine("No existe equipo con ese nombre.");
                Console.ReadKey();
                return;
            }

            if (equipos[indice].DNIsJugadores.Count > 0)
            {
                Console.WriteLine("No se puede eliminar el equipo porque tiene jugadores asignados.");
                Console.ReadKey();
                return;
            }

            equipos.RemoveAt(indice);
            Console.WriteLine("Equipo eliminado correctamente.");
            Console.ReadKey();
        }

        static void ModificarEquipo(List<Equipo> equipos)
        {
            Console.Clear();
            Console.WriteLine("=== MODIFICAR EQUIPO ===");
            ListarEquipos(equipos);

            Console.Write("\nIngrese el nombre del equipo a modificar: ");
            string nombre = Console.ReadLine();

            int indice = equipos.FindIndex(e => string.Equals(e.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
            if (indice == -1)
            {
                Console.WriteLine("No existe equipo con ese nombre.");
                Console.ReadKey();
                return;
            }

            Equipo e = equipos[indice];
            Console.WriteLine($"\n--- Datos actuales ---");
            Console.WriteLine($"Club: {e.Club} | Categoria: {e.Cat}");

            Console.WriteLine("\n--- Nuevos datos (Enter para mantener) ---");

            Console.Write($"Nuevo club [{e.Club}]: ");
            string club = Console.ReadLine();
            if (!string.IsNullOrEmpty(club)) e.Club = club;

            Console.Write($"Nueva categoria (0-4) [{(int)e.Cat}]: ");
            string catStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(catStr) && int.TryParse(catStr, out int catInt) && catInt >= 0 && catInt <= 4)
            {
                e.Cat = (Categoria)catInt;
            }

            equipos[indice] = e;
            Console.WriteLine("Equipo modificado correctamente.");
            Console.ReadKey();
        }

        static void ListarEquipos(List<Equipo> equipos)
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE EQUIPOS ===");
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos cargados.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Nombre\tClub\tCategoria\tJugadores");
            Console.WriteLine("--------------------------------------------------------");

            foreach (var e in equipos)
            {
                Console.WriteLine($"{e.Nombre}\t\t{e.Club}\t\t{e.Cat}\t\t{e.DNIsJugadores.Count}");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Asigna un jugador a un equipo validando categoría y evitando duplicados
        /// Muestra advertencia si no se cumple el mínimo de jugadores
        /// </summary>
        static void AsignarJugadorAEquipo(List<Equipo> equipos, List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== ASIGNAR JUGADOR A EQUIPO ===");

            if (jugadores.Count == 0 || equipos.Count == 0)
            {
                Console.WriteLine("Debe haber jugadores y equipos cargados.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n--- JUGADORES DISPONIBLES ---");
            foreach (var j in jugadores)
            {
                Console.WriteLine($"DNI: {j.DNI} - {j.Nombre} {j.Apellido} (Edad: {j.Edad}, Categoria: {j.Categoria})");
            }

            Console.Write("\nIngrese DNI del jugador: ");
            if (!int.TryParse(Console.ReadLine(), out int dni))
            {
                Console.WriteLine("DNI inválido.");
                Console.ReadKey();
                return;
            }

            int indiceJugador = jugadores.FindIndex(j => j.DNI == dni);
            if (indiceJugador == -1)
            {
                Console.WriteLine("No existe jugador con ese DNI.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n--- EQUIPOS DISPONIBLES ---");
            foreach (var e in equipos)
            {
                Console.WriteLine($"Nombre: {e.Nombre} - Categoria: {e.Cat} - Jugadores: {e.DNIsJugadores.Count}");
            }

            Console.Write("\nIngrese nombre del equipo: ");
            string nombreEquipo = Console.ReadLine();

            int indiceEquipo = equipos.FindIndex(e => string.Equals(e.Nombre, nombreEquipo, StringComparison.OrdinalIgnoreCase));
            if (indiceEquipo == -1)
            {
                Console.WriteLine("No existe equipo con ese nombre.");
                Console.ReadKey();
                return;
            }

            Equipo equipo = equipos[indiceEquipo];
            Jugador jugador = jugadores[indiceJugador];

            if (!ValidarEdadCategoria(jugador.Edad, equipo.Cat))
            {
                Console.WriteLine($"Error: El jugador de {jugador.Edad} años no puede jugar en categoría {equipo.Cat}.");
                Console.ReadKey();
                return;
            }

            if (equipo.DNIsJugadores.Contains(dni))
            {
                Console.WriteLine("El jugador ya está asignado a este equipo.");
                Console.ReadKey();
                return;
            }

            equipo.DNIsJugadores.Add(dni);
            equipos[indiceEquipo] = equipo;

            jugador.EquiposAsignados.Add(equipo.Nombre);
            jugadores[indiceJugador] = jugador;

            Console.WriteLine($"\nJugador {jugador.Nombre} {jugador.Apellido} asignado a {equipo.Nombre} correctamente.");

            int minimoRequerido = (equipo.Cat == Categoria.Veteranos) ? 10 : 9;
            if (equipo.DNIsJugadores.Count < minimoRequerido)
            {
                Console.WriteLine($"\n⚠️ ADVERTENCIA: El equipo {equipo.Nombre} tiene {equipo.DNIsJugadores.Count} jugadores. El mínimo es {minimoRequerido}.");
            }

            Console.ReadKey();
        }

        // ==================== 6. REPORTES ====================

        static void MenuReportes(List<Jugador> jugadores, List<Equipo> equipos)
        {
            Console.Clear();
            Console.WriteLine("=== REPORTES ===");
            Console.WriteLine("1. Listar jugadores asegurados");
            Console.WriteLine("2. Listar jugadores ordenados por edad");
            Console.WriteLine("3. Listar jugadores agrupados por categoria");
            Console.WriteLine("4. Jugador mas joven y mas viejo");
            Console.WriteLine("5. Cantidad de jugadores por categoria");
            Console.WriteLine("6. Promedio de edad general");
            Console.WriteLine("7. Equipos que no cumplen minimo de jugadores");
            Console.WriteLine("0. Volver");
            Console.Write("Opcion: ");

            string op = Console.ReadLine();

            switch (op)
            {
                case "1": ListarJugadoresAsegurados(jugadores); break;
                case "2": ListarJugadoresOrdenadosPorEdad(jugadores); break;
                case "3": ListarJugadoresAgrupadosPorCategoria(jugadores); break;
                case "4": MostrarJugadorJovenViejo(jugadores); break;
                case "5": CantidadJugadoresPorCategoria(jugadores); break;
                case "6": PromedioEdadGeneral(jugadores); break;
                case "7": EquiposSinMinimoJugadores(equipos); break;
                case "0": return;
                default: Console.WriteLine("Opción no válida"); Console.ReadKey(); break;
            }
        }

        static void ListarJugadoresAsegurados(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== JUGADORES ASEGURADOS ===");
            var asegurados = jugadores.Where(j => j.Seguro).ToList();

            if (asegurados.Count == 0)
            {
                Console.WriteLine("No hay jugadores asegurados.");
                Console.ReadKey();
                return;
            }

            foreach (var j in asegurados)
                Console.WriteLine($"{j.DNI} - {j.Nombre} {j.Apellido}");

            Console.ReadKey();
        }

        static void ListarJugadoresOrdenadosPorEdad(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== JUGADORES ORDENADOS POR EDAD ===");
            var ordenados = jugadores.OrderBy(j => j.Edad).ToList();

            if (ordenados.Count == 0)
            {
                Console.WriteLine("No hay jugadores cargados.");
                Console.ReadKey();
                return;
            }

            foreach (var j in ordenados)
                Console.WriteLine($"{j.Edad} años - {j.Nombre} {j.Apellido}");

            Console.ReadKey();
        }

        static void ListarJugadoresAgrupadosPorCategoria(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== JUGADORES AGRUPADOS POR CATEGORIA ===");

            foreach (Categoria cat in Enum.GetValues<Categoria>())
            {
                var grupo = jugadores.Where(j => j.Categoria == cat).ToList();
                Console.WriteLine($"\n--- {cat} ({grupo.Count}) ---");
                foreach (var j in grupo)
                    Console.WriteLine($"{j.Nombre} {j.Apellido} - {j.Edad} años");
            }

            Console.ReadKey();
        }

        static void MostrarJugadorJovenViejo(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== JUGADOR MAS JOVEN Y MAS VIEJO ===");

            if (jugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores cargados.");
                Console.ReadKey();
                return;
            }

            var joven = jugadores.OrderBy(j => j.Edad).First();
            var viejo = jugadores.OrderByDescending(j => j.Edad).First();

            Console.WriteLine($"Más joven: {joven.Nombre} {joven.Apellido} - {joven.Edad} años");
            Console.WriteLine($"Más viejo: {viejo.Nombre} {viejo.Apellido} - {viejo.Edad} años");
            Console.ReadKey();
        }

        static void CantidadJugadoresPorCategoria(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== CANTIDAD DE JUGADORES POR CATEGORIA ===");

            foreach (Categoria cat in Enum.GetValues<Categoria>())
            {
                int cant = jugadores.Count(j => j.Categoria == cat);
                Console.WriteLine($"{cat}: {cant} jugadores");
            }

            Console.ReadKey();
        }

        static void PromedioEdadGeneral(List<Jugador> jugadores)
        {
            Console.Clear();
            Console.WriteLine("=== PROMEDIO DE EDAD GENERAL ===");

            if (jugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores cargados.");
                Console.ReadKey();
                return;
            }

            double promedio = jugadores.Average(j => j.Edad);
            Console.WriteLine($"Promedio de edad: {promedio:F2} años");
            Console.ReadKey();
        }

        /// <summary>
        /// Lista equipos que no cumplen con el mínimo: 9 jugadores o 10 si son Veteranos
        /// </summary>
        static void EquiposSinMinimoJugadores(List<Equipo> equipos)
        {
            Console.Clear();
            Console.WriteLine("=== EQUIPOS QUE NO CUMPLEN MINIMO DE JUGADORES ===");

            bool hayEquipos = false;
            foreach (var e in equipos)
            {
                int minimo = (e.Cat == Categoria.Veteranos) ? 10 : 9;
                if (e.DNIsJugadores.Count < minimo)
                {
                    hayEquipos = true;
                    Console.WriteLine($"{e.Nombre} - Categoria: {e.Cat} - Jugadores: {e.DNIsJugadores.Count}/{minimo}");
                }
            }

            if (!hayEquipos)
                Console.WriteLine("Todos los equipos cumplen con el mínimo de jugadores.");

            Console.ReadKey();
        }
    }
}
