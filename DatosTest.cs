using System;
using System.Collections.Generic;
using static TP01.Program;

namespace TP01
{
    static class DatosTest
    {
        static Random rnd = new Random();

        static string[] nombres =
        {
            "Juan", "Pedro", "Lucas", "Martin", "Mateo",
            "Joaquin", "Tomas", "Santiago", "Thiago", "Nicolas",
            "Franco", "Facundo", "Luciano", "Agustin", "Valentino",
            "Benjamin", "Bruno", "Federico", "Maximo", "Ramiro"
        };

        static string[] apellidos =
        {
            "Gomez", "Perez", "Lopez", "Fernandez", "Rodriguez",
            "Garcia", "Martinez", "Sanchez", "Romero", "Diaz",
            "Alvarez", "Torres", "Ruiz", "Acosta", "Benitez",
            "Suarez", "Herrera", "Medina", "Castro", "Vega"
        };

        static string[] clubes =
        {
            "River",
            "Boca",
            "Racing",
            "Independiente"
        };

        public static void CargaDatosMinima(List<Equipo> equipos)
        {
            string[] clubes =
            {
        "River Plate",
        "Boca Juniors",
        "San Lorenzo",
        "Racing Club",
        "Independiente"
    };

            string[] categorias =
            {
        "Infantil",
        "Juvenil",
        "Veteranos"
    };

            foreach (string club in clubes)
            {
                foreach (string categoria in categorias)
                {
                    Equipo nuevoEquipo = new Equipo();

                    nuevoEquipo.nombreClub = club;
                    nuevoEquipo.categoria = categoria;
                    nuevoEquipo.nombreEquipo = club + " " + categoria;

                    if (categoria == "Veteranos")
                    {
                        nuevoEquipo.cantMinima = 10;
                    }
                    else
                    {
                        nuevoEquipo.cantMinima = 9;
                    }

                    equipos.Add(nuevoEquipo);
                }
            }
        }
        public static void CargaTest(
            List<Program.Jugador> jugadores,
            List<Program.Equipo> equipos)
        {
            jugadores.Clear();
            equipos.Clear();

            // =====================================================
            // CREAR EQUIPOS
            // =====================================================
            //
            // Cada club tendrá:
            // 2 Infantiles
            // 2 Cadetes
            // 2 Juveniles
            // 2 Primera
            // 1 Veteranos
            //
            // Total por club: 9 equipos
            // Total general: 36 equipos
            //
            // =====================================================

            string[] categorias =
            {
                "Infantiles",
                "Infantiles",

                "Cadetes",
                "Cadetes",

                "Juveniles",
                "Juveniles",

                "Primera",
                "Primera",

                "Veteranos"
            };

            for (int c = 0; c < clubes.Length; c++)
            {
                for (int i = 0; i < categorias.Length; i++)
                {
                    Program.Equipo eq =
                        new Program.Equipo();

                    char letra =
                        (char)('A' + i);

                    eq.nombreClub =
                        clubes[c];

                    eq.nombreEquipo =
                        clubes[c] + " Equipo " + letra;

                    eq.categoria =
                        categorias[i];

                    eq.cantMinima = 8;

                    equipos.Add(eq);
                }
            }

            // =====================================================
            // CREAR JUGADORES
            // =====================================================

            for (int i = 0; i < 120; i++)
            {
                Program.Jugador jug =
                    new Program.Jugador();

                jug.dni =
                    rnd.Next(30000000, 47000000)
                    .ToString();

                jug.nombre =
                    nombres[rnd.Next(nombres.Length)];

                jug.apellido =
                    apellidos[rnd.Next(apellidos.Length)];

                jug.edad =
                    rnd.Next(10, 41);

                jug.seguro =
                    rnd.Next(0, 2) == 1;

                jug.afiliado =
                    rnd.Next(0, 2) == 1;

                jug.equipAsig =
                    new List<Program.Equipo>();

                jugadores.Add(jug);
            }

            // =====================================================
            // ASIGNAR JUGADORES A EQUIPOS
            // =====================================================
            //
            // Reglas:
            //
            // - Puede estar en varios equipos
            // - SOLO del mismo club
            // - Debe cumplir edad de categoría
            // - Algunos quedan sin equipo
            //
            // =====================================================

            for (int i = 0; i < jugadores.Count; i++)
            {
                Program.Jugador jug =
                    jugadores[i];

                // 20% sin equipo
                if (rnd.Next(0, 100) < 20)
                {
                    continue;
                }

                // elegir club para el jugador
                string clubJugador =
                    clubes[rnd.Next(clubes.Length)];

                // cantidad de equipos:
                // 1, 2 o 3
                int cantidadEquipos =
                    rnd.Next(1, 4);

                int intentos = 0;

                while (
                    jug.equipAsig.Count < cantidadEquipos
                    && intentos < 30)
                {
                    intentos++;

                    Program.Equipo eq =
                        equipos[rnd.Next(equipos.Count)];

                    // mismo club
                    if (eq.nombreClub != clubJugador)
                    {
                        continue;
                    }

                    // evitar repetidos
                    bool yaExiste = false;

                    for (int j = 0;
                        j < jug.equipAsig.Count;
                        j++)
                    {
                        if (
                            jug.equipAsig[j].nombreEquipo
                            == eq.nombreEquipo)
                        {
                            yaExiste = true;
                        }
                    }

                    if (yaExiste)
                    {
                        continue;
                    }

                    // validar edad
                    if (
                        CumpleEdad(
                            jug.edad,
                            eq.categoria))
                    {
                        jug.AgregarAEquipo(eq);
                    }
                }

                jugadores[i] = jug;
            }

            // =====================================================
            // DEJAR ALGUNOS EQUIPOS BAJO MINIMO
            // =====================================================

            DejarEquipoConPocosJugadores(
                jugadores,
                equipos[0],
                5);

            DejarEquipoConPocosJugadores(
                jugadores,
                equipos[10],
                4);
        }

        public static void CargaTestCorta(
    List<Program.Jugador> jugadores,
    List<Program.Equipo> equipos)
        {
            jugadores.Clear();
            equipos.Clear();

            // =====================================================
            // CREAR EQUIPOS
            // =====================================================

            string[] categorias =
            {
        "Infantiles",
        "Infantiles",

        "Cadetes",
        "Cadetes",

        "Juveniles",
        "Juveniles",

        "Primera",
        "Primera",

        "Veteranos"
    };

            for (int c = 0; c < clubes.Length; c++)
            {
                for (int i = 0; i < categorias.Length; i++)
                {
                    Program.Equipo eq =
                        new Program.Equipo();

                    char letra =
                        (char)('A' + i);

                    eq.nombreClub =
                        clubes[c];

                    eq.nombreEquipo =
                        clubes[c] + " Equipo " + letra;

                    eq.categoria =
                        categorias[i];

                    eq.cantMinima = 4;

                    equipos.Add(eq);
                }
            }

            // =====================================================
            // CREAR 25 JUGADORES
            // =====================================================

            for (int i = 0; i < 25; i++)
            {
                Program.Jugador jug =
                    new Program.Jugador();

                jug.dni =
                    rnd.Next(30000000, 47000000)
                    .ToString();

                jug.nombre =
                    nombres[rnd.Next(nombres.Length)];

                jug.apellido =
                    apellidos[rnd.Next(apellidos.Length)];

                jug.edad =
                    rnd.Next(10, 41);

                jug.seguro =
                    rnd.Next(0, 2) == 1;

                jug.afiliado =
                    rnd.Next(0, 2) == 1;

                jug.equipAsig =
                    new List<Program.Equipo>();

                jugadores.Add(jug);
            }

            // =====================================================
            // ASIGNAR EQUIPOS
            // =====================================================

            for (int i = 0; i < jugadores.Count; i++)
            {
                Program.Jugador jug =
                    jugadores[i];

                // algunos sin equipo
                if (rnd.Next(0, 100) < 25)
                {
                    continue;
                }

                string clubJugador =
                    clubes[rnd.Next(clubes.Length)];

                // entre 1 y 2 equipos
                int cantidadEquipos =
                    rnd.Next(1, 3);

                int intentos = 0;

                while (
                    jug.equipAsig.Count < cantidadEquipos
                    && intentos < 20)
                {
                    intentos++;

                    Program.Equipo eq =
                        equipos[rnd.Next(equipos.Count)];

                    // mismo club
                    if (eq.nombreClub != clubJugador)
                    {
                        continue;
                    }

                    // evitar repetidos
                    bool existe = false;

                    for (int j = 0;
                        j < jug.equipAsig.Count;
                        j++)
                    {
                        if (
                            jug.equipAsig[j].nombreEquipo
                            == eq.nombreEquipo)
                        {
                            existe = true;
                        }
                    }

                    if (existe)
                    {
                        continue;
                    }

                    // validar edad
                    if (
                        CumpleEdad(
                            jug.edad,
                            eq.categoria))
                    {
                        jug.AgregarAEquipo(eq);
                    }
                }

                jugadores[i] = jug;
            }

            // =====================================================
            // DEJAR ALGUNOS EQUIPOS VACIOS O BAJO MINIMO
            // =====================================================

            DejarEquipoConPocosJugadores(
                jugadores,
                equipos[0],
                2);

            DejarEquipoConPocosJugadores(
                jugadores,
                equipos[15],
                1);
        }




        // =====================================================
        // VALIDAR EDAD SEGUN CATEGORIA
        // =====================================================

        static bool CumpleEdad(
            int edad,
            string categoria)
        {
            switch (categoria)
            {
                case "Infantiles":
                    return edad <= 13;

                case "Cadetes":
                    return edad >= 13
                        && edad <= 16;

                case "Juveniles":
                    return edad >= 16
                        && edad <= 18;

                case "Primera":
                    return edad >= 18;

                case "Veteranos":
                    return edad >= 35;
            }

            return false;
        }

        // =====================================================
        // DEJAR EQUIPOS BAJO MINIMO
        // =====================================================

        static void DejarEquipoConPocosJugadores(
            List<Program.Jugador> jugadores,
            Program.Equipo equipo,
            int cantidadMaxima)
        {
            int contador = 0;

            for (int i = 0;
                i < jugadores.Count;
                i++)
            {
                Program.Jugador jug =
                    jugadores[i];

                if (jug.equipAsig == null)
                {
                    continue;
                }

                for (int j = jug.equipAsig.Count - 1;
                    j >= 0;
                    j--)
                {
                    if (
                        jug.equipAsig[j].nombreEquipo
                        == equipo.nombreEquipo)
                    {
                        contador++;

                        if (contador > cantidadMaxima)
                        {
                            jug.equipAsig.RemoveAt(j);
                        }
                    }
                }

                jugadores[i] = jug;
            }
        }
    }
}