using System;
using System.Collections.Generic;

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
            "Independiente",
            "San Lorenzo"
        };

        public static void CargaTest(
            List<Program.Jugador> jugadores,
            List<Program.Equipo> equipos)
        {
            // ==========================================
            // CREAR EQUIPOS
            // ==========================================

            int indiceCategoria = 0;

            for (int club = 0; club < clubes.Length; club++)
            {
                for (int letra = 0; letra < 2; letra++)
                {
                    Program.Equipo eq = new Program.Equipo();

                    char sufijo = (char)('A' + letra);

                    eq.nombreClub = clubes[club];

                    eq.nombreEquipo =
                        clubes[club] + " Equipo " + sufijo;

                    eq.categoria =
                        Program.categoria[indiceCategoria];

                    eq.cantMinima = 8;

                    equipos.Add(eq);

                    indiceCategoria++;

                    if (indiceCategoria >= Program.categoria.Length)
                    {
                        indiceCategoria = 0;
                    }
                }
            }

            // ==========================================
            // CREAR 100 JUGADORES
            // ==========================================

            for (int i = 0; i < 100; i++)
            {
                Program.Jugador jug = new Program.Jugador();

                // DNI verosímil
                jug.dni = rnd.Next(30000000, 47000000).ToString();

                jug.nombre =
                    nombres[rnd.Next(nombres.Length)];

                jug.apellido =
                    apellidos[rnd.Next(apellidos.Length)];

                jug.edad = rnd.Next(10, 40);

                jug.seguro = rnd.Next(0, 2) == 1;

                jug.afiliado = rnd.Next(0, 2) == 1;

                jug.equipAsig =
                    new List<Program.Equipo>();

                jugadores.Add(jug);
            }

            // ==========================================
            // ASIGNAR 90 JUGADORES
            // ==========================================

            // equipos 8 y 9 quedarán vacíos

            for (int i = 0; i < 90; i++)
            {
                int indiceEquipo = rnd.Next(0, 8);

                Program.Jugador jug = jugadores[i];

                jug.AgregarAEquipo(equipos[indiceEquipo]);

                jugadores[i] = jug;
            }

            // ==========================================
            // DEJAR 2 EQUIPOS BAJO MINIMO
            // ==========================================

            int contador7 = 0;
            int contador8 = 0;

            for (int i = 0; i < jugadores.Count; i++)
            {
                Program.Jugador jug = jugadores[i];

                if (jug.equipAsig != null &&
                    jug.equipAsig.Count > 0)
                {
                    string nombreEquipo =
                        jug.equipAsig[0].nombreEquipo;

                    if (nombreEquipo == equipos[6].nombreEquipo)
                    {
                        contador7++;

                        if (contador7 > 5)
                        {
                            jug.equipAsig.Clear();
                        }
                    }

                    if (nombreEquipo == equipos[7].nombreEquipo)
                    {
                        contador8++;

                        if (contador8 > 4)
                        {
                            jug.equipAsig.Clear();
                        }
                    }

                    jugadores[i] = jug;
                }
            }
        }

        public static void CargaTestCorta(
    List<Program.Jugador> jugadores,
    List<Program.Equipo> equipos)
        {
            // ==========================================
            // CREAR 3 EQUIPOS
            // ==========================================

            for (int i = 0; i < 3; i++)
            {
                Program.Equipo eq = new Program.Equipo();

                char sufijo = (char)('A' + i);

                eq.nombreClub = clubes[i];

                eq.nombreEquipo =
                    clubes[i] + " Equipo " + sufijo;

                eq.categoria =
                    Program.categoria[i];

                eq.cantMinima = 4;

                equipos.Add(eq);
            }

            // ==========================================
            // CREAR 10 JUGADORES
            // ==========================================

            for (int i = 0; i < 10; i++)
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

                jug.edad = rnd.Next(10, 40);

                jug.seguro =
                    rnd.Next(0, 2) == 1;

                jug.afiliado =
                    rnd.Next(0, 2) == 1;

                jug.equipAsig =
                    new List<Program.Equipo>();

                jugadores.Add(jug);
            }

            // ==========================================
            // ASIGNAR SOLO 7 JUGADORES
            // ==========================================

            // quedan 3 sin equipo
            // equipo 3 queda vacío

            for (int i = 0; i < 7; i++)
            {
                int indiceEquipo =
                    rnd.Next(0, 2);

                Program.Jugador jug =
                    jugadores[i];

                jug.AgregarAEquipo(
                    equipos[indiceEquipo]);

                jugadores[i] = jug;
            }
        }
    }
}