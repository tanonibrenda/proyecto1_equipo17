//**************************************************************************************************
//*** GRUPO 17 ****///
//*** TP01 ****///
//*** PARTICIPANTES ****///
//VARAS MONTES, Maria Isabel///
//YOHENA TANONI, Brenda///
//ARAUJO, Facundo///
//************************************************************************************************** 

using System;
using System.Collections.Generic;
using System.Linq;
using static TP01.Program;
//using System.Data.SqlTypes;
//using System.Linq;
//using System.Net;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices.WindowsRuntime;
//using System.Text;
//using System.Threading.Tasks;

namespace TP01
{
    internal class Program
    {
        //**************************************************************************************************
        //*** ESTRUCUTRAS PRINCIPALES///
        //************************************************************************************************** 
        public static readonly string[] categoria =
        {
            "Infantiles",     //  < 13
            "Cadetes",        //  >=13 && < 16
            "Juveniles",      //  >=16 && < 18
            "Primera",        //  >=18 
            "Veteranos"       //  >=35 
        };

        public struct Equipo
        {
            public string nombreEquipo;
            public string nombreClub;
            public string categoria;
            public int cantMinima;

            /// <summary>
            /// Devuelve true si el equipo tiene al jugador, y false si no lo tiene 
            /// </summary>
            /// <param name="jugador"></param> recibe un jugador
            /// <returns></returns>
            public bool TieneJugador(Jugador jugador)
            {
                
                if (jugador.equipAsig != null) 
                {

                    foreach (Equipo equipo in jugador.equipAsig)
                    {
                        if (equipo.nombreEquipo == nombreEquipo)
                        {
                            return true;
                        }
                    }

                }
                return false;
            }
            public int CantidadJugadoresEquipo(List<Jugador> jugadores)
            {
                int cant = 0;
                foreach (Jugador jug in jugadores)
                {
                    if (jug.EstaEnEquipo(this))
                    {
                        cant++;
                    }
                }
                return cant;
            }

            public List<Jugador> ListadoDeJugadores(List<Jugador>jugadores)
            {
                List<Jugador> jugadoresEnEqupipo = new List<Jugador>();
                foreach(Jugador jug in jugadores)
                {
                    if(jug.EstaEnEquipo(this))
                    {
                        jugadoresEnEqupipo.Add(jug);
                    }
                }
                return jugadoresEnEqupipo;
            }

            // se utiliza por combatilibad entre genericos de Jugador y Equipo
            public void PrintSmall()
            {
                Console.WriteLine($"nombre del equipo : {nombreEquipo}");
                Console.WriteLine($"Club : {nombreClub}");
                Console.WriteLine($"Categoria : {categoria}");
                Console.WriteLine($"Cantidad minima de jugadores : {cantMinima}");
            }

            public void PrintFull(List<Jugador> jugadores)
            {
                PrintSmall();
                Console.WriteLine($"Actualmente hay {CantidadJugadoresEquipo(jugadores)} jugadores en este equipo");
            }

        }

        public struct Jugador
        {
            public string dni;
            public string nombre;
            public string apellido;
            public int edad;
            public bool seguro;
            public bool afiliado;
            public List<Equipo> equipAsig;


            /// <summary>
            /// Verif si un jugador esta en un equipo
            /// </summary>
            /// <param name="equipo"></param>
            /// <returns>true si esta, false si no esta</returns>
            public bool EstaEnEquipo(Equipo equipo)
            {
                if(equipAsig!= null)
                {
                    foreach(Equipo equip in equipAsig)
                    {
                        if(equip.nombreEquipo == equipo.nombreEquipo)
                            { return true; }
                    }
                }
                return false;
            }

            /// <summary>
            /// agrega un equipo a un jugador
            /// </summary>
            /// <param name="equipo"></param> recibe un Equipo
            public void AgregarAEquipo(Equipo equipo)
            {
                // verifico que no sea una lista vacia
                if (equipAsig == null)
                {
                    equipAsig = new List<Equipo>();
                }

                equipAsig.Add(equipo);

            }

            /// <summary>
            /// quita un equipo de un jugador
            /// </summary>
            /// <param name="equipo"></param>
            public void QuitarDeEquipo(Equipo equipo)
            {
                if (equipAsig != null)
                {
                    for (int i = 0; i < equipAsig.Count; i++)
                    {
                        if (equipAsig[i].nombreEquipo == equipo.nombreEquipo)
                        {
                            equipAsig.RemoveAt(i);
                            break;
                        }
                    }
                }
            }


            public void PrintSmall()
            {
                Console.WriteLine($"DNI : {dni}");
                Console.WriteLine($"Nombre : {nombre}");
                Console.WriteLine($"Apellido : {apellido}");
                Console.WriteLine($"Edad : {edad}");
            }
            public void PrintFull()
            {
                Console.WriteLine($"DNI : {dni}");
                Console.WriteLine($"Nombre : {nombre}");
                Console.WriteLine($"Apellido : {apellido}");
                Console.WriteLine($"Edad : {edad}");
                Console.WriteLine(seguro ? "Esta asegurado" : "No esta asegurado");
                Console.WriteLine(afiliado ? "Esta afiliado" : "No esta afiliado");


                if(equipAsig != null && equipAsig.Count > 0)
                {
                    Console.WriteLine("juega en los siguientes equipos:");
                    ImprimirListado(equipAsig);
                }
                else
                {
                    Console.WriteLine("Actualmente no participa en ningun equipo");
                }
            }
        }

        //**************************************************************************************************
        //*** ESTRUCUTRAS AUXILIARES///
        //************************************************************************************************** 


        // define el tipo de opcion que puedo tener dentro de un menu
        enum TipoOpcion
        {
            Accion, 
            Menu
        }

        enum AccionMenu
        {
            AltaEquipo,                 //alta de equipo 
            BajaEquipo,                 //baja de equipo
            ModificarDatosEquipo,       //Modif de equipo
            AgregarJugadorAEquipo,      //agrega un jugador disponible al equipo
            QuitarJugadorDeEquipo,      //quita un jugador del equipo
            AltaJugador,                //alta jugador
            BajaJugador,                //baja jugador
            ModificarDatosJugador,      //modif jugador
            AgregarEquipoAJugador,      //agrega un equipo al jugador (donde juega)
            QuitarEquipoDeJugador,      //quita un equipo del jugador (donde ya no va a jugar)
            JugadoresAsegurados,        //listado jugadores asegurados
            JugadoresXEdad,             //Listado de jugadores ordenados x edad
            JugadoresXCategoria,        //Listado de jugadores agrupados x categoria
            MasJovenMasViejo,           //Reporte de jugador mas joven y mas viejo
            CantidadXCategoria,         //Cantidad de jugadores x categoria
            PromedioEdad,               //promedio de edad de la liga
            Exit                        //salir del programa
        }

        //define en que consiste una opcion de menu
        struct OpcionMenu
        {
            public string nombreOpcion;
            public TipoOpcion tipoOpcion;
            public OpcionMenu[] newMenu;
            public AccionMenu accion;
        }

        // declaro los menues
        static OpcionMenu[] administrarEquipos;
        static OpcionMenu[] modificarEquipos;
        static OpcionMenu[] administrarJugadores;
        static OpcionMenu[] modificarJugadores;
        static OpcionMenu[] listados;
        static OpcionMenu[] reportes;
        static OpcionMenu[] menuPrincipal;

        //**************************************************************************************************
        //*** FUNCIONES PRINCIPALES///
        //************************************************************************************************** 


        // ABM DE EQUIPOS
        static void AltaEquipo(List<Equipo> equipos, List<Jugador> jugadores)
        {
            //inicializo variables, para guardar los parametros antes de crear el equioo
            string nombreClub;
            string nombreEquipo;
            string respCategoria;
            int categoriaIndex;
            List<Jugador> jugadoresDelClub = new List<Jugador>();
            int cantMinima = 9;

            //solicito el nombre del club 
            Console.WriteLine("Ingrese el nombre del Club");
            nombreClub = Console.ReadLine();

            //cuento cuantos equipos ya tiene ese club
            int cantEquiposDelClub = ContarEquiposPorClub(equipos, nombreClub);

            // creo el nombre del equipo, con el formato "Club" + {nombre del club} + Letras
            nombreEquipo = "Club " + nombreClub + " " + ObtenerEtiqueta(cantEquiposDelClub + 1);

            //genero un loop para obtener la categoria adecuada
            while(true)
            {
                Console.WriteLine("Ingrese la categoria");
                ImprimirListado(categoria);

                respCategoria = Console.ReadLine();

                if (ValidarOpcionElegida(categoria, respCategoria))
                {
                    categoriaIndex = int.Parse(respCategoria) - 1;   // le resto 1 para acomodarlo al indice del array
                    break;
                }
            }

            /// FALTA IMPLEMENTAR EL ALTA.. VER SI NO CONVIENE EMPEZAR X LOS LISTADOS Y X JUGADORES




        }

        static void BajaEquipo(List<Equipo> equipos, List<Jugador> jugadores)
        {
            int opcNumEq;
            string opc;
            List<Jugador> jugadoresAQuitar = new List<Jugador>();
            Console.WriteLine("BAJA DE EQUIPOS");
            Console.WriteLine("Este es el listado de Equipos: ");
            Console.WriteLine();
            //imprime el listado de equipos para seleccionar uno
            ImprimirListado(equipos);
            //verif la opcion elegida ajustada al index del list
            opcNumEq = ElegirOpcion(equipos, "equipo");
            //// si no eligio salir del menu
            if (opcNumEq != -1)
            {
                Console.WriteLine("Ha elegido a este equipo");
                equipos[opcNumEq].PrintFull(jugadores);
                Console.WriteLine();
                //recorro todos los jugadores de la liga
                foreach (Jugador jug in jugadores)
                {
                    //verifico si este jugador juega en este equipo
                    if (jug.EstaEnEquipo(equipos[opcNumEq]))
                    {
                        //lo agrego al listado de quitar
                        jugadoresAQuitar.Add(jug);
                    }
                }
                //si el equipo tiene jugadores asignados 
                if (jugadoresAQuitar.Count > 0)
                {
                    Console.WriteLine("Si borra el equipo, el mismo se removera de los siguientes jugadores: ");
                    foreach (Jugador jug in jugadoresAQuitar)
                    {
                        jug.PrintSmall();
                    }
                }
                //loop para forzar la eleccion valida
                while (true)
                {
                    Console.WriteLine("Desea borrarlo? (S/N)");
                    opc = Console.ReadLine();
                    //valida que elija s, S, n, N
                    if (ValidaBool(opc))
                    {
                        //presiono s o S
                        if (ValidaSoN(opc))
                        {
                            //si tiene jugadores a quitar
                            if(jugadoresAQuitar.Count > 0)
                            {
                                //recorro todo el listado de jugadores de la liga
                                for(int i = 0; i < jugadores.Count; i++)
                                {
                                    //copio el jugador a uno temporal 
                                    Jugador jug = jugadores[i];

                                    //si el jugador esta en el equipo a borrar 
                                    if (jug.EstaEnEquipo(equipos[opcNumEq]))
                                    {
                                        //recorro los equipos asignados del jugador
                                        for (int j = jug.equipAsig.Count - 1; j >= 0; j--)
                                        {
                                            //si es el equipo a borrar
                                            if (jug.equipAsig[j].nombreEquipo == equipos[opcNumEq].nombreEquipo)
                                            {
                                                //lo quito del listado de equipos del jugador
                                                jug.equipAsig.RemoveAt(j);
                                            }
                                        }
                                        //piso al jugador del listado con el temporal para actualizarlo
                                        jugadores[i] = jug;
                                    }                   
                                }
                            }

                          //borro al equipo
                          equipos.RemoveAt(opcNumEq);
                        }
                        else  // presiono n o N
                        {
                            Console.WriteLine("Se ha anulado la baja del equipo");
                        }
                        break;
                    }
                    Console.WriteLine("Ingreso no valido!!!");
                }
                Espera();
                LimpiarPantalla();
            }
        }

        static void ModificarDatosEquipo(List<Equipo> equipos, List<Jugador> jugadores)
        {
            // solo se permite cambiar la categoria del equipo, el nombre es creado automaticamente y el club, es parte del nombre.
            // la cant minima de jugadores depende de la categoria
            int opcNumEq;
            Console.WriteLine("MODIFICAR EQUIPO");
            Console.WriteLine("Solo se permite modificar la categoria del equipo, para otros cambios dar baja y nueva alta");
            Console.WriteLine("Este es el listado de Equipos: ");
            Console.WriteLine();
            // imprime listado de equipos
            ImprimirListado(equipos);
            // elegir equipo
            opcNumEq = ElegirOpcion(equipos, "equipo");
            // si no eligio salir
            if (opcNumEq != -1)
            {
                Console.WriteLine("Ha elegido a este equipo");
                equipos[opcNumEq].PrintFull(jugadores);
                Console.WriteLine();
                //fuerza respuesta corrcta
                while(true)
                {
                    Console.WriteLine("Desea Cambiar la Categoria? (S/N)");
                    string opc = Console.ReadLine();
                    //valida que elija s,S, n o N
                    if (ValidaBool(opc))
                    {
                        //si eleigio s, S
                        if (ValidaSoN(opc))
                        {
                            Console.WriteLine($"La categoria actual es {equipos[opcNumEq].categoria}");
                            String[] categoriasDisp = ObtenerCategoriasDisponibles(equipos[opcNumEq].categoria);
                            Console.WriteLine("Las categorias disponibles son:");
                            ImprimirListado(categoriasDisp);
                            Console.WriteLine();
                            int opcNumCat = ElegirOpcion(categoriasDisp.ToList<String>(), "categorias");
                            // si no eligio salir
                            if (opcNumCat != -1)
                            {
                                //creo un equipoTemporal
                                Equipo equipoTemp = equipos[opcNumEq];
                                int edadMaxOrig = ObtenerEdadMaxCategoria(equipoTemp.categoria);
                                int edadMaxNueva = ObtenerEdadMaxCategoria(categoriasDisp[opcNumCat]);
                                //filtrar jugadores x edad
                                List<Jugador> jugadoresAQuitar = new List<Jugador>();
                                List<Jugador> jugadoresQueQuedan = new List<Jugador>();
                                //si la edad maxima de la categoria original es mayor a la de la edad max de la nueva categoria y el equipo tiene jugadores
                                if (edadMaxOrig > edadMaxNueva && equipoTemp.CantidadJugadoresEquipo(jugadores) > 0)
                                {


                                    foreach(Jugador jug in equipoTemp.ListadoDeJugadores(jugadores))
                                    {
                                        if(jug.edad >  edadMaxNueva)
                                        {
                                            jugadoresAQuitar.Add(jug);
                                        }
                                        else
                                        {
                                            jugadoresQueQuedan.Add(jug);
                                        }
                                    }

                                    //si hay que quitar jugadores
                                    if(jugadoresAQuitar.Count > 0)
                                    {
                                        Console.WriteLine("El cambio de categoria forzaria a quitar a los siguentes jugadores:");
                                        ImprimirListado(jugadoresAQuitar);
                                        Console.WriteLine();
                                    }

                                    //si quedan jugadores
                                    if(jugadoresQueQuedan.Count > 0)
                                    {
                                        Console.WriteLine("Quedando solo los siguentes jugadores:");
                                        ImprimirListado(jugadoresQueQuedan);
                                        Console.WriteLine();
                                    }
                                    else //no quedan jugadores
                                    {
                                        Console.WriteLine("No quedando ningun jugador en el equipo");
                                    }


                                }
                                //fuerza la eleccion 
                                while(true)
                                {
                                    Console.WriteLine("Desea guardar los cambios? (S/N)");
                                    opc = Console.ReadLine();
                                    //valida que elija s,S, n o N
                                    if (ValidaBool(opc))
                                    {
                                        //si eleigio s, S
                                        if (ValidaSoN(opc))
                                        {
                                            //modifico el equipo asig en cada jugador a quitar
                                            foreach(Jugador jug in jugadoresAQuitar)
                                            {
                                                jug.QuitarDeEquipo(equipoTemp);
                                                //piso el estado de los jugadores a quitar en la lista de los jugadores de la liga
                                                for (int i = 0; i < jugadores.Count; i++)
                                                {
                                                    if(jug.dni == jugadores[i].dni)
                                                    {
                                                        jugadores[i] = jug;
                                                        break; 
                                                    }
                                                }
                                            }

                                            //actualizo en el equipoTemp la categoria
                                            equipoTemp.categoria = categoriasDisp[opcNumCat];

                                            //actualizo la cantMinima de jugadores si la nueva categoria es veteranos
                                            if(equipoTemp.categoria == "Veteranos")
                                            {
                                                equipoTemp.cantMinima = 10;
                                            }
                                            else
                                            {
                                                equipoTemp.cantMinima = 9;
                                            }

                                                //piso al equipo con los datos modificados
                                                equipos[opcNumEq] = equipoTemp;

                                            Console.Write("Se han guardado los cambios");
                                            Espera();
  
                                        }
                                        else // eliigio n, N
                                        {
                                            Console.WriteLine("Ud, ha anulado la modificacion del equipo");
                                            Espera();
                                        }
                                        //sale del while de grabacion de cambio
                                        break;
                                    }
                                    else // no ingreso s, S, n, N
                                    {
                                        Console.WriteLine("Ingreso no valido");
                                    }
                                }
                            }
                            LimpiarPantalla();
                        }
                        else // eliigio n, N
                        {
                            Console.WriteLine("Ud, ha anulado la modificacion del equipo");
                            Espera();
                        }
                        //sale del while de respuesta correcta
                        break;

                    }
                    else // no ingreso s, S, n, N
                    {
                        Console.WriteLine("Ingreso no valido");
                    }
                }
                LimpiarPantalla();
            }
        }

        static void AgregarJugadorAEquipo(List<Equipo> equipos, List<Jugador> jugadores)
        {
            int opcNumEq;
            int opcNumJug;
            List<Jugador> jugadoresCandidatos = new List<Jugador>();
            Console.WriteLine("AGREGAR JUGADOR");
            Console.WriteLine("Este es el listado de Equipos: ");
            Console.WriteLine();
            // imprime listado de equipos
            ImprimirListado(equipos);
            // elegir equipo
            opcNumEq = ElegirOpcion(equipos, "equipo");
            // si no eligio salir
            if (opcNumEq != -1)
            {
                Console.WriteLine("Ha elegido a este equipo");
                equipos[opcNumEq].PrintFull(jugadores);
                Console.WriteLine();

                // obtengo jugadores que pueden entrar en el equipo
                jugadoresCandidatos = ElegirJugadoresCandidatos(equipos[opcNumEq], jugadores);
                // si tiene jugadores canidatos
                if (jugadoresCandidatos.Count > 0)
                {
                    Console.WriteLine("Este es el listado de jugadores que pueden ingresar al equipo: ");
                    ImprimirListado(jugadoresCandidatos);
                    Console.WriteLine();
                    //elijo jugador a ingresar
                    opcNumJug = ElegirOpcion(jugadoresCandidatos, "jugador");
                    //si no elisio salir
                    if(opcNumJug != -1)
                    {
                        Console.WriteLine("Ha elegido a este jugador: ");
                        jugadoresCandidatos[opcNumJug].PrintSmall();
                        //fuerza eleccion correcta
                        while(true)
                        {
                            Console.WriteLine("Desea agregarlo? (S/N)");
                            string opc = Console.ReadLine();
                            //valida que elija s,S, n o N
                            if (ValidaBool(opc))
                            {
                                //si eleigio s, S
                                if (ValidaSoN(opc))
                                {
                                    for(int i = 0; i < jugadores.Count; i++)
                                    {
                                        if (jugadoresCandidatos[opcNumJug].dni == jugadores[i].dni)
                                        {
                                            Jugador jugadorTemp = jugadores[i];
                                            jugadorTemp.equipAsig.Add(equipos[opcNumEq]);
                                            // piso al jugador en la list de jugadores
                                            jugadores[i] = jugadorTemp;
                                        }
                                    }
                                    Console.WriteLine("Jugador agregado al equipo");
                                    Espera();
                         
                                }
                                else // eliigio n, N
                                {
                                    Console.WriteLine("Ud, ha anulado el ingreso del jugador al equipo");
                                    Espera();
                                }
                                //sale del while de respuesta correcta
                                break; 
                            }
                            else // no ingreso s, S, n, N
                            {
                                Console.WriteLine("Ingreso no valido");
                            }
          
                        }
                    }

                }
                else //no hay jugadores que pueden ingresar
                {
                    Console.WriteLine("Actualmente no hay ningun jugador que puede ingresar al equipo");
                }
                LimpiarPantalla();
            }
            
        }


        //***************** MODIFICAR PARA AGREGAR CONFIRMACION DE LA OPERACION COMO EN AGREGAR***********
        static void QuitarJugadorDeEquipo(List<Equipo> equipos, List<Jugador> jugadores)
        {
            int opcNumEq;
            int opcNumJug;
            List<Jugador> jugadoresDelEquipo = new List<Jugador>();
            Console.WriteLine("QUITAR JUGADOR");
            Console.WriteLine("Este es el listado de Equipos: ");
            Console.WriteLine();
            // imprime listado de equipos
            ImprimirListado(equipos);
            // elegir equipo
            opcNumEq = ElegirOpcion(equipos, "equipo");
            // si no eligio salir
            if (opcNumEq != -1)
            {
                Console.WriteLine("Ha elegido a este equipo");
                equipos[opcNumEq].PrintFull(jugadores);
                Console.WriteLine();

                // obtengo jugadores del equipo
                jugadoresDelEquipo = equipos[opcNumEq].ListadoDeJugadores(jugadores);
                // si tiene jugadores
                if (jugadoresDelEquipo.Count > 0)
                {
                    Console.WriteLine("El equipo tiene estos jugadores");
                    ImprimirListado(jugadoresDelEquipo);
                    Console.WriteLine();
                    // elegir jugador
                    opcNumJug =  ElegirOpcion(jugadoresDelEquipo, "jugador");
                    // si no eligio salir
                    if (opcNumJug != -1)
                    {
                        Console.WriteLine("Ha elegido a este jugador: ");
                        jugadoresDelEquipo[opcNumJug].PrintSmall();
                        //fuerza eleccion correcta
                        while(true)
                        {
                            Console.WriteLine("Desea quitarlo? (S/N)");
                            string opc = Console.ReadLine();
                            // si eligio s, S, n o N
                            if (ValidaBool(opc))
                            {
                                //si eleigio s, S
                                if (ValidaSoN(opc))
                                {
                                    // jugador elegido del listado
                                    Jugador jugElegido = jugadoresDelEquipo[opcNumJug];

                                    // buscar jugador real
                                    // en la lista general
                                    for (int j = 0; j < jugadores.Count; j++)
                                    {
                                        if (jugadores[j].dni == jugElegido.dni)
                                        {
                                            // copiar struct
                                            Jugador jugTemp = jugadores[j];

                                            // recorrer equipos asignados
                                            for (int i = jugTemp.equipAsig.Count - 1; i >= 0; i--)
                                            {
                                                // si es el equipo elegido
                                                if (jugTemp.equipAsig[i].nombreEquipo == equipos[opcNumEq].nombreEquipo)
                                                {
                                                    // quitar equipo
                                                    jugTemp.equipAsig.RemoveAt(i);
                                                    //para salir del for
                                                    break;
                                                }
                                            }

                                            // actualizar jugador
                                            jugadores[j] = jugTemp;

                                            Console.WriteLine("Jugador quitado del equipo");
                                            Espera();                          
                                        }
                                    }
                                }
                                else //eligio n, N
                                {
                                    Console.WriteLine("Ud, ha anulado la baja del jugador del equipo");
                                    Espera();
                                }
                                //para salir del while
                                break;
                            }
                            else //no eleigio ni s, S, n o N
                            {
                                Console.WriteLine("Ingreso no valido");
                            }
                        }
                        
                    }
                }
                else // equipo sin jugadores
                {
                    Console.WriteLine("El equipo no tiene jugadores para quitar");
                    Espera();
                }
                
                LimpiarPantalla();
            }
        }
        //ABM DE JUGADORES 
        /// <summary>
        /// Alta Jugador crea una nueva copia de struct Jugador
        /// </summary>
        /// <param name="jugadores"></param>
        /// <returns></returns>
        static void AltaJugador(List<Jugador> jugadores)
        {
            string dni;
            string nombre;
            string apellido;
            int edad;
            bool seguro = false;
            bool afiliado = false;
             List<Equipo> equipAsig = new List<Equipo>();
            Console.WriteLine("Alta de jugador:");
            Console.WriteLine();
            while(true)
            {
                Console.WriteLine("Ingrese el DNI: ");
                dni = Console.ReadLine();
                //verifico que sea valido
                if (VerificaDNIValido(dni))
                {
                    //verifico que no exista
                    if (!DNIExistente(dni, jugadores))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"El jugador con DNI {dni} ya esta cargado");
                    }
                }
                else
                {
                    Console.WriteLine("El formato ingresado no es valido, por favor ingrese solo numeros sin puntos ni espacios");
                }
                Espera();
                
            }
            Console.WriteLine("Ingrese el nombre: ");
            nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido: ");
            apellido = Console.ReadLine();


            //loop para forzar edad valida
            while(true)
            {
                Console.WriteLine("Ingrese la edad (1 a 99 años): ");
                string edadString = Console.ReadLine();
                if (VerificaIntRango(edadString, 1, 99))
                {
                    edad = int.Parse(edadString);
                    break;
                }
                else
                {
                    Console.WriteLine("Ingreso invalido");
                }

            }
            //loop para forzar seguro valido
            while(true)
            {
                Console.WriteLine("Está asegurado? (S/N)");
                string segString = Console.ReadLine();
                if(ValidaBool(segString))
                {
                    seguro = ValidaSoN(segString);
                    break;
                }
                Console.WriteLine("Ingreso no valido!!!");
            }

            //loop para forzar afiliacion valida
            while(true)
            {
                Console.WriteLine("Está afiliado? (S/N)");
                string afilString = Console.ReadLine();
                if (ValidaBool(afilString))
                {
                    afiliado = ValidaSoN(afilString);
                    break;
                }
                else
                {
                    Console.WriteLine("Ingreso no valido!!!");
                }
            }

            // creo al jugador
            Jugador newJugador = new Jugador();
            newJugador.dni = dni;
            newJugador.nombre = nombre;
            newJugador.apellido = apellido;
            newJugador.edad = edad;
            newJugador.seguro = seguro;
            newJugador.afiliado = afiliado;
            newJugador.equipAsig = equipAsig;

            jugadores.Add(newJugador);

            Console.WriteLine("nuevo jugador agregado!!!");
            Espera();
            LimpiarPantalla();

        }

        static void BajaJugador(List<Jugador> jugadores)
        {
            int opcNum = -2;
            string opc;
            Console.WriteLine("BAJA DE JUGADORES");
            Console.WriteLine("Este es el listado de jugadores existentes");
            Console.WriteLine();

            ImprimirListado(jugadores);

            opcNum = ElegirOpcion(jugadores, "jugador");
            

            //si no eligio salir del menu y eligio un jugador para borrar 
            if(opcNum != -1)
            {
                Console.WriteLine("Ha elegido a este jugador");
                jugadores[opcNum].PrintFull();

                //loop para forzar la eleccion valida
                while (true)
                {
                    Console.WriteLine("Desea borrarlo? (S/N)");
                    opc = Console.ReadLine();
                    //valida que elija s, S, n, N
                    if (ValidaBool(opc))
                    {
                        //presiono s o S
                        if (ValidaSoN(opc))
                        {
                            jugadores.RemoveAt(opcNum);
                            Console.WriteLine("El jugador se ha dado de baja");
                           
                        }
                        else  // presiono n o N
                        {
                            Console.WriteLine("Se ha anulado la baja del jugador");
                        }
                        break;
                    }
                    Console.WriteLine("Ingreso no valido!!!");
                }
                Espera();
                LimpiarPantalla();
            }

        }

        static void ModificarDatosJugador(List<Jugador> jugadores)
        {
            string opc;
            int opcNumJug = -1;
            Jugador jugadorTemp = new Jugador();

            Console.WriteLine("MODIFICAR DATOS DE JUGADOR");
            Console.WriteLine("Este es el listado de jugadores: ");
            //imprime el listado de jugadores para seleccionar uno
            ImprimirListado(jugadores);
            //verif la opcion elegida ajustada al index del list
            opcNumJug = ElegirOpcion(jugadores, "jugador");
            // si no eligio salir del menu
            if (opcNumJug != -1)
            {
                // copio los datos del jugador seleccionado a uno temporal
                jugadorTemp = jugadores[opcNumJug];
                Console.WriteLine();
                Console.WriteLine($"Ud ha elegido al jugador");
                Console.WriteLine();
                jugadores[opcNumJug].PrintSmall();
                Console.WriteLine();

                //edicion DNI
                while(true)
                {
                    Console.WriteLine("Desea modif el DNI (S/N)");
                    opc = Console.ReadLine();
                    //fuerzo ingreso de S, s, n o N
                    if (ValidaBool(opc))
                    {
                        //elije s, S
                        if(ValidaSoN(opc))
                        {
                            Console.WriteLine("ingrese nuevo dni");
                            opc = Console.ReadLine();
                            //valida que lo ingresado tenga formato dni
                            if(VerificaDNIValido(opc))
                            {
                                //verif que no exista el dni 
                                if(!DNIExistente(opc, jugadores))
                                {
                                    //modifico el dni 
                                    jugadorTemp.dni = opc;
                                    break;
                                }
                                else // el dni ya existe
                                {
                                    Console.WriteLine($"ya existe un jugador el dni {opc}");
                                }
                            }
                            else //dni ingresado no es valido
                            {
                                Console.WriteLine("El dni ingresado no tiene formato valido");
                            }
                        }
                        else //elije n, N
                        {
                            // sale del while de dni
                            break;
                        }
                    }

                }

                //edicion nombre
                while (true)
                {
                    Console.WriteLine("Desea modif el nombre (S/N)");
                    opc = Console.ReadLine();
                    //fuerzo ingreso de S, s, n o N
                    if (ValidaBool(opc))
                    {
                        //elije s, S
                        if (ValidaSoN(opc))
                        {
                            Console.WriteLine("ingrese nuevo nombre");
                            opc = Console.ReadLine();
                            jugadorTemp.nombre = opc;
                            break;                         
                        }
                        else //elije n, N
                        {
                            // sale del while de nombre
                            break;
                        }
                    }
                }

                //edicion apellido
                while (true)
                {
                    Console.WriteLine("Desea modif el apellido (S/N)");
                    opc = Console.ReadLine();
                    //fuerzo ingreso de S, s, n o N
                    if (ValidaBool(opc))
                    {
                        //elije s, S
                        if (ValidaSoN(opc))
                        {
                            Console.WriteLine("ingrese nuevo apellido");
                            opc = Console.ReadLine();
                            jugadorTemp.apellido = opc;
                            break;
                        }
                        else //elije n, N
                        {
                            // sale del while de apellido
                            break;
                        }
                    }
                }
                //edicion edad
                while(true)
                {
                    Console.WriteLine("Desea modif la edad ? (S/N)");
                    opc = Console.ReadLine();
                    //fuerzo ingreso de S, s, n o N
                    if (ValidaBool(opc))
                    {
                        //elije s, S a modificacion
                        if (ValidaSoN(opc))
                        {
                            // bucle fuerza ingreso edad valido
                            while(true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Ingrese la nueva edad: (1 a 99)");
                                opc = Console.ReadLine();

                                //verifica edad dentro de rango valido
                                if (VerificaIntRango(opc, 1, 99))
                                {
                                    //asigno la edad a una nueva variable
                                    int newEdad = int.Parse(opc);
                                    //creo 2 list de equipos para ver si modif los equipos asignados 
                                    List<Equipo> equiposASacar = new List<Equipo>();
                                    List<Equipo> equiposRestantes = new List<Equipo>();

                                    //recorro el listado de equipos asignados para ver si debe dejar alguno
                                    foreach(Equipo equip in jugadores[opcNumJug].equipAsig)
                                    {
                                        //si la nueva edad es <= a la cat del equipo asig
                                        if (newEdad <= ObtenerEdadMaxCategoria(equip.categoria))
                                        {
                                            equiposRestantes.Add(equip);
                                        }
                                        else
                                        {
                                            equiposASacar.Add(equip);
                                        }
                                    }
                                    //si con el cambio de edad se eliminarian equipos
                                    if(equiposASacar.Count > 0)
                                    {
                                        Console.WriteLine($"Con la nueva edad de {newEdad} el jugador no puede jugar mas en estos equipos:");
                                        foreach(Equipo equip in equiposASacar)
                                        {
                                            Console.WriteLine(equip.nombreEquipo);
                                        }
                                        //todavia tendria equipos restantes
                                        if(equiposRestantes.Count > 0)
                                        {
                                            Console.WriteLine("y sigue jugando en: ");
                                            foreach(Equipo equip in equiposRestantes)
                                            {
                                                Console.WriteLine(equip.nombreEquipo);
                                            }
                                        }
                                        else //no le quedarian equipos restantes
                                        {
                                            Console.WriteLine("Quedando sin estar asignado a ningun equipo");
                                        }

                                    }
                                    //guardo los cambios
                                    jugadorTemp.edad = newEdad;
                                    jugadorTemp.equipAsig = equiposRestantes;
                                    break;
                                }
                                else // ingreso no valido de edad
                                {
                                    Console.WriteLine("Ingreso invalido!!!");
                                }
                            }


                        }

                        //sale del break de edad 
                        break;
                    }
                    else //no ingreso s, S, n o N
                    {
                        Console.WriteLine("Ingreso no valido!!!");
                    }
                }



                //edicion seguro
                while (true)
                {
                    Console.WriteLine("Desea modif si esta asegurado ? (S/N)");
                    opc = Console.ReadLine();
                    //fuerzo ingreso de S, s, n o N
                    if (ValidaBool(opc))
                    {
                        //elije s, S a modificacion
                        if (ValidaSoN(opc))
                        {
                            // bucle para forzar ingreso de estado seguro
                            while(true)
                            {
                                Console.WriteLine("Esta asegurado (S/N)");
                                opc = Console.ReadLine();
                                //fuerzo ingreso de s, S, n o N
                                if (ValidaBool(opc))
                                {
                                    //si esta asgurado
                                    if(ValidaSoN(opc))
                                    {
                                        jugadorTemp.seguro = true;
                                    }
                                    else //no esta asegurado
                                    {
                                        jugadorTemp.seguro = false;
                                    }
                                    // salgo del bucle ingreso seguro
                                    break;
                                }                                   
                            }
                            ////sale del while seguro
                            //break;
                        }
                        //else //elije n, N a modificacion
                        //{
                        //    // sale del while de seguro
                        //    break;
                        //}
                        // sale del while de seguro
                        break;
                    }
                    else //no ingreso s, S, n o N
                    {
                        Console.WriteLine("Ingreso no valido!!!");
                    }
                }

                //edicion afiliado
                while (true)
                {
                    Console.WriteLine("Desea modif si esta afiliado (S/N)");
                    opc = Console.ReadLine();
                    //fuerzo ingreso de S, s, n o N
                    if (ValidaBool(opc))
                    {
                        //elije s, S a modificacion
                        if (ValidaSoN(opc))
                        {
                            // bucle para forzar ingreso de estado afiliado
                            while (true)
                            {
                                Console.WriteLine("Esta afiliado (S/N)");
                                opc = Console.ReadLine();
                                //fuerzo ingreso de s, S, n o N
                                if (ValidaBool(opc))
                                {
                                    //si esta afiliado
                                    if (ValidaSoN(opc))
                                    {
                                        jugadorTemp.seguro = true;
                                    }
                                    else //no esta afiliado
                                    {
                                        jugadorTemp.seguro = false;
                                    }
                                    // salgo del bucle ingreso afiliado
                                    break;
                                }
                            }
                            //sale del while afiliado
                            break;
                        }
                        else //elije n, N a modificacion
                        {
                            // sale del while de afiliado
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingreso no valido!!!");
                    }
                }

                Console.WriteLine("El jugador tenia estos datos:");
                jugadores[opcNumJug].PrintFull();
                Console.WriteLine();
                Console.WriteLine("Con las modificaciones tendra estos datos:");
                jugadorTemp.PrintFull();

                //bucle de confirmacion de cambios
                while(true)
                {
                    Console.WriteLine("Desea guardar los cambios? (S/N)");
                    opc = Console.ReadLine();
                    //fuerzo ingreso de S, s, n o N
                    if (ValidaBool(opc))
                    {
                        //elije s, S a modificacion
                        if (ValidaSoN(opc))
                        {

                            jugadores[opcNumJug] = jugadorTemp;
                            Console.WriteLine("Los datos del jugador han sido modificados");
                            Espera();
                            LimpiarPantalla();
                            //sale del bucle de confirmacion de modif
                            break;
                        }
                        else //elije n, N a modificacion
                        {
                            Console.WriteLine("Se ha cancelado la modif de datos");
                            Espera();
                            LimpiarPantalla();
                            //sale del bucle de confirmacion de modif
                            break;
                        }
                    }
                }
                
      
            }

            LimpiarPantalla();
            
        }
        
        /// <summary>
        /// agrega un equipo a un jugador
        /// </summary>
        /// <param name="jugadores"></param> Listado con todos los jugadores
        /// <param name="equipos"></param>  Listado con todos los equipos
        static void AgregarEquipoAJugador(List<Jugador> jugadores, List<Equipo> equipos)
        {
            string opc;
            int opcNumJug = -1;
            int opcNumEq = -1;
            List<Equipo> equiposDisponibles = new List<Equipo>();
            Jugador jugadorTemp = new Jugador();

            Console.WriteLine("AGREGAR EQUIPO A JUGADOR");
            Console.WriteLine("Este es el listado de jugadores: ");
            //imprime el listado de jugadores para seleccionar uno
            ImprimirListado(jugadores);
            //verif la opcion elegida ajustada al index del list
            opcNumJug = ElegirOpcion(jugadores, "jugador");
            // si no eligio salir del menu
            if (opcNumJug != -1)  
            {
                // copio los datos del jugador seleccionado a uno temporal
                jugadorTemp = jugadores[opcNumJug];
                Console.WriteLine();
                Console.WriteLine($"Ud ha elegido al jugador");
                Console.WriteLine();
                jugadores[opcNumJug].PrintSmall();
                Console.WriteLine();
                if (jugadores[opcNumJug].equipAsig.Count > 0)   // verifca que tenga equipos 
                {
                    Console.WriteLine("juega en los siguientes equipos:");

                    // imprimo el listdo de euipos en los que juega
                    ImprimirListado(jugadores[opcNumJug].equipAsig);
                    Console.WriteLine();
                }
                else //no tiene nignun equipo 
                {
                    Console.WriteLine("El jugador no esta jugando en ningun equipo");
                    Console.WriteLine();
                }

                //busco el listado de equipos disp para el jugador
                equiposDisponibles = BuscarEquiposDisponibles(jugadorTemp, equipos);

                //verifico que tenga equipos disponibles para agregar
                if(equiposDisponibles.Count >0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Este es el listado de equipos disponibles: ");
                    ImprimirListado(equiposDisponibles);
                    opcNumEq = ElegirOpcion(equiposDisponibles, "equipo");
                    // si la opcion no es salir 
                    if (opcNumEq != -1)
                    {
                        //fuerzo una respuesta valida x S o N
                        while (true)
                        {
                            Console.WriteLine($"Desea confirmar que va a agregar el jugador al equipo {equiposDisponibles[opcNumEq].nombreEquipo} (S/N)");
                            opc = Console.ReadLine();
                            if (ValidaBool(opc))
                            {
                                //si eleigio s, S
                                if (ValidaSoN(opc))
                                {
                                    // agrego el equipo al jugador
                                    jugadorTemp.AgregarAEquipo(equiposDisponibles[opcNumEq]);

                                    // piso al jugador en la list de jugadores
                                    jugadores[opcNumJug] = jugadorTemp;

                                    Console.WriteLine("Equipo agreado al jugador");
                                    Espera();
                                    LimpiarPantalla();
                                }
                                else // eliigio n, N
                                {
                                    Console.WriteLine("Ud, ha anulado agregar el equipo");
                                    Espera();
                                    LimpiarPantalla();
                                }
                                break;
                            }
                            else // no ingreso s, S, n, N
                            {
                                Console.WriteLine("Ingreso no valido");
                            }
                        }


                    }
                    else  //eligio salir 
                    {

                    }

                }
                else // no tiene equipos disp para agregar
                {
                    Console.WriteLine("Actualmente no hay nuevos equipos disponibles para este jugador");
                    Espera();
                    LimpiarPantalla();
                }

            }

        }
        /// <summary>
        /// funcion que quita un equipo de un jugador
        /// </summary>
        /// <param name="jugadores"></param> listado con todos los jugadores de la liga
        /// <param name="equipos"></param> listado con todos los equipos de la liga 
        static void QuitarEquipoDeJugador(List<Jugador> jugadores, List<Equipo> equipos)
        {
            string opc;
            int opcNumJug = -1;
            int opcNumEq = -1; 
            Jugador jugadorTemp = new Jugador();

            Console.WriteLine("QUITAR EQUIPO DE JUGADOR");
            Console.WriteLine("Este es el listado de jugadores: ");
            //imprime el listado de jugadores para seleccionar uno
            ImprimirListado(jugadores);
            //verif la opcion elegida ajustada al index del list
            opcNumJug = ElegirOpcion(jugadores, "jugador");

            if(opcNumJug != -1)  // si no eligio salir del menu
            {
                // copio los datos del jugador seleccionado a uno temporal
                jugadorTemp = jugadores[opcNumJug];
                Console.WriteLine();
                Console.WriteLine($"Ud ha elegido al jugador");
                Console.WriteLine();
                jugadores[opcNumJug].PrintSmall();
                Console.WriteLine();
                if (jugadores[opcNumJug].equipAsig.Count > 0)   // verifca que tenga equipos 
                {
                    Console.WriteLine("juega en los siguientes equipos:");

                    // imprimo el listdo de euipos en los que juega
                    ImprimirListado(jugadores[opcNumJug].equipAsig);
                    Console.WriteLine();
                    opcNumEq = ElegirOpcion(jugadores[opcNumJug].equipAsig, "equipo");

                    // si la opcion no es salir 
                    if(opcNumEq != -1)
                    {
                        //fuerzo una respuesta valida x S o N
                        while(true)
                        {
                            Console.WriteLine($"Desea confirmar que va a quitar al jugador del equipo {jugadorTemp.equipAsig[opcNumEq].nombreEquipo} (S/N)");
                            opc = Console.ReadLine();
                            if (ValidaBool(opc))
                            {
                                //si eleigio s, S
                                if(ValidaSoN(opc))
                                {
                                    // remuevo al equipo del jugador asignado
                                    jugadorTemp.QuitarDeEquipo(jugadorTemp.equipAsig[opcNumEq]);

                                    // piso al jugador en la list de jugadores
                                    jugadores[opcNumJug] = jugadorTemp;

                                    Console.WriteLine("Equipo quitado del jugador");
                                    Espera();
                                    LimpiarPantalla();
                                }
                                else // eliigio n, N
                                {
                                    Console.WriteLine("Ud, ha anulado quitar el equipo");
                                    Espera();
                                    LimpiarPantalla();
                                }
                                break;
                            }
                            else // no ingreso s, S, n, N
                            {
                                Console.WriteLine("Ingreso no valido");
                            }
                        }


                    }
                    else  //eligio salir 
                    {

                    }

                }
                else // eligio salir 
                {
                    Console.WriteLine("El jugador no esta jugando en ningun equipo");
                    Espera();
                    LimpiarPantalla();
                }



            }
            

            
        }


        // LISTADOS **************************************************************

        /// <summary>
        /// Imprime el listado de jugadores asegurados
        /// </summary>
        /// <param name="jugadores"></param>  recibe un List de elementos Jugador, con todos los jugadores inscriptos
        static void JugadoresAsegurados(List<Jugador> jugadores)
        {
            Console.WriteLine("Jugadores asegurados: ");
            Console.WriteLine();

            int cant = 0;
            foreach(Jugador jug in  jugadores)
            {
                if(jug.seguro)
                {
                   jug.PrintSmall();
                   cant ++;
                }
            }
            if(cant == 0)
            {
                Console.WriteLine("No hay jugadores asegurados");
            }
            Espera();
            LimpiarPantalla();
        }

        /// <summary>
        /// Imprime el listado de todos los jugadores ordenados por edad
        /// </summary>
        /// <param name="jugadores"></param> recibe un List de elementos Jugador, con todos los jugadores inscriptos
        static void JugadoresXEdad(List<Jugador> jugadores)
        {
            // creo una copia de la lista 
            List<Jugador> copia = new List<Jugador>(jugadores);

            Console.WriteLine("Listado de jugadores ordenados por edad:");

            //ordeno la nueva lista por edad ascendente
            copia.Sort((a, b) => a.edad.CompareTo(b.edad));

            //recorro el listado ordenado y lo imprimo
            foreach (var j in copia)
            {
                j.PrintSmall();
            }

            //si no hay jugadores lo informo
            if(copia.Count == 0)
            {
                Console.WriteLine("no hay jugadores en la liga");
            }
            Espera();
            LimpiarPantalla();
        }

        /// <summary>
        /// Imprime todos los jugadores x categoria 
        /// </summary>
        /// <param name="jugadores"></param> recibe un List de elementos Jugador, con todos los jugadores inscriptos
        static void JugadoresXCategoria(List<Jugador> jugadores)
        {

            Console.WriteLine("Jugadores agrupados por Categoria:");
            Console.WriteLine();
            for (int i = 0; i < categoria.Length; i++)
            {
                List<Jugador> jugadoresCategoria = new List<Jugador>();

                foreach (Jugador j in jugadores)
                {
                    if (ObtenerIndiceCategoria(j.edad) == i)
                    {
                        jugadoresCategoria.Add(j);
                    }
                }

                Console.WriteLine(categoria[i]);
                foreach(Jugador j in jugadoresCategoria)
                {
                    j.PrintSmall();
                }

                // agrego espacio
                Console.WriteLine();
            }
            Espera();
            LimpiarPantalla();
        }
   

        // REPORTES ******************************************************************

        /// <summary>
        /// Imprime el jugador mas joven y mas viejo del listado de jugadors
        /// </summary>
        /// <param name="jugadores"></param> recibe un List de elemntos Jugador, con todos los jugadores inscriptos
        static void MasJovenMasViejo(List<Jugador> jugadores)
        {
            //declaro variables locales para el mas joven y el mas viejo
            Jugador masJoven;
            Jugador masViejo;

            Console.WriteLine("Jugador mas Joven y mas Viejo:");
            Console.WriteLine();

            // verifico si la liga no tiene jugadores
            if (jugadores.Count == 0)
            {
                Console.WriteLine("Actualmente no hay ningun jugador en la liga");
                Espera();
                LimpiarPantalla();
            }
            else // si tiene jugadores
            {

                masJoven = jugadores[0];
                masViejo = jugadores[0];
                for(int i = 1; i < jugadores.Count; i++)
                {
                    if (jugadores[i].edad < masJoven.edad)
                    {
                        masJoven = jugadores[i];
                    }
                    else if (jugadores[i].edad > masViejo.edad)
                    {
                        masViejo = jugadores[i];
                    }
                }
                Console.WriteLine("el jugador mas joven es:");
                masJoven.PrintFull();
                Console.WriteLine("el jugador mas viejo es:");
                masViejo.PrintFull();
                Espera();
                LimpiarPantalla();
            }


        }

        /// <summary>
        /// Imprime la cantidad de jugadores que hay por categoria, segun la edad de los mismos, cada jugador puede estar en mas de un equipo de su edad para arriba
        /// </summary>
        /// <param name="jugadores"></param> recibe un List de elemntos Jugador, con todos los jugadores inscriptos
        static void CantidadXCategoria(List<Jugador> jugadores)
        {
            // creo un array de int local con la misma cantidad de elementos q de categorias
            int[] cantXCat = new int [categoria.Length];

            // recorro el list de jugadore y vos sumandola categoria
            foreach (Jugador j in jugadores)
            {
                int idx = ObtenerIndiceCategoria(j.edad);
                cantXCat[idx]++;
            }

            Console.WriteLine("Cantidad de Jugadores x Categoria:");
            Console.WriteLine();


            //imprimo el resultado
            for (int i = 0; i < categoria.Length; i++)
            {
                Console.WriteLine($"En la liga hay {cantXCat[i]} jugadores que son categoria {categoria[i]}");
            }
            Espera();
            LimpiarPantalla();
        }

        /// <summary>
        /// Imprime el promedio de edad de todos los jugadores inscriptos
        /// </summary>
        /// <param name="jugadores"></param>  recibe un List de elemntos Jugador, con todos los jugadores inscriptos
        static void PromedioEdad(List<Jugador> jugadores)
        {
            //inicializo variable local con la suma de las edades
            float sumaEdades = 0;

            // verifico que haya jugadores en la liga
            if(jugadores.Count > 0)
            {
                foreach (Jugador jugador in jugadores)
                {
                    sumaEdades += jugador.edad;
                }
                Console.WriteLine($"El promedio de edad de los jugadores de la liga es {sumaEdades / jugadores.Count}");

            }
            else // si no hay jugadores
            {
                Console.WriteLine("Actualmente la liga no tiene jugadores");
                
            }
            Espera();
            LimpiarPantalla();
        }



        //**************************************************************************************************
        //*** FUNCIONES AUXILIARES///
        //************************************************************************************************** 

        /// <summary>
        /// Limpia la pantalla de la consola
        /// </summary>
        static void LimpiarPantalla()
        {
            Console.Clear();
        }

        /// <summary>
        /// Espera un ingreso de teclado para continuar la ejecucion
        /// </summary>
        static void Espera()
        {
            Console.WriteLine("Presione cualquier tecla para continuar");
            Console.ReadLine();
        }

        /// <summary>
        /// Imprime un listado extraeido de un array de elementos OpcionMenu
        /// </summary>
        /// <param name="menu"></param> recibe un array de elementos OpcionMenu
        static void ImprimirListado(OpcionMenu[] menu)
        {
            int i = 1;
            foreach(OpcionMenu opc in menu)
            {
                Console.WriteLine($"{i} - {opc.nombreOpcion}");
                i++;
            }
        }

        /// <summary>
        /// Imprime un listado extraido de los elem de un array de strings
        /// </summary>
        /// <param name="listado"></param> Array de strings
        static void ImprimirListado(string[] listado)
        {
            int i = 1;
            foreach (var elem in listado)
            {
                Console.WriteLine($"{i} - {elem}");
                i++;
            }
        }

        /// <summary>
        /// Imprime un listado de los elem de una List de Equipo
        /// </summary>
        /// <param name="equipos"></param> Listado de Equipo
        static void ImprimirListado(List<Jugador> jugadores)
        {
            int i = 1;
            foreach (var jug in jugadores)
            {
                Console.WriteLine("******************");
                Console.WriteLine();
                Console.Write($"{i} - ");
                jug.PrintSmall();
                i++;
                Console.WriteLine();
            }
        }

        static void ImprimirListado(List<Equipo> equipos)
        {
            int i = 1;
            foreach (var equip in equipos)
            {
                Console.WriteLine($"{i} - {equip.nombreEquipo}");
                
                i++;
            }
        }


        /// <summary>
        /// Valida la opcion elegida
        /// </summary>
        /// <param name="menu"></param> El array de elementos OpcionMenu
        /// <param name="opc"></param>  El ingreso por teclado hecho por el usuario
        /// <returns></returns>
        static bool ValidarOpcionElegida(OpcionMenu[] menu, string opc)
        {
            // valida que no sea una cadena vacia
            if(opc.Length == 0)
            {
                return false;
            }
            // valida que cada char de la cadena sea un digito
            for (int i = 0; i < opc.Length; i++)
            {
                if (!char.IsDigit(opc[i]))
                {
                    return false;
                }
            }
            
            //sabiendo que es digito lo convierte a entero
            int opcNum = int.Parse(opc);

            //valida que la opcion este dentro de las opciones del menu
            if(opcNum <1 || opcNum > menu.Length)
                { 
                    return false; 
                }
    
            // si esta todo correcto
            return true;

        }

        /// <summary>
        /// Valida que la opcion elegida
        /// </summary>
        /// <param name="lista"></param> array de strings
        /// <param name="opc"></param> El ingreso por teclado hecho por el usuario
        /// <returns></returns>
        static bool ValidarOpcionElegida(string[] lista, string opc)
        {
            // valida que no sea una cadena vacia
            if (opc.Length == 0)
            {
                return false;
            }
            // valida que cada char de la cadena sea un digito
            for (int i = 0; i < opc.Length; i++)
            {
                if (!char.IsDigit(opc[i]))
                {
                    return false;
                }
            }

            //sabiendo que es digito lo convierte a entero
            int opcNum = int.Parse(opc);

            //valida que la opcion este dentro de las opciones del menu
            if (opcNum < 1 || opcNum > lista.Length)
            {
                return false;
            }

            // si esta todo correcto
            return true;

        }
        static bool ValidarOpcionElegida<T>(List<T> lista, string opc)
        {
            if (string.IsNullOrEmpty(opc))
                return false;

            foreach (char c in opc)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            int opcNum = int.Parse(opc);

            if (opcNum < 1 || opcNum > lista.Count)
                return false;

            return true;
        }

        //funcion que inicializa los menues
        static void InicializarMenues()
        {
            administrarEquipos = new OpcionMenu[]
            {
                new OpcionMenu{ nombreOpcion = "Alta de Equipo", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.AltaEquipo },
                new OpcionMenu{ nombreOpcion = "Baja de Equipo", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.BajaEquipo },
                new OpcionMenu{ nombreOpcion = "Modificacion de Equipo", tipoOpcion = TipoOpcion.Menu },
                new OpcionMenu{ nombreOpcion = "Volver atras", tipoOpcion = TipoOpcion.Menu }
            };

            modificarEquipos = new OpcionMenu[]
{
                new OpcionMenu{ nombreOpcion = "Modificar datos del Equipo", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.ModificarDatosEquipo },
                new OpcionMenu{ nombreOpcion = "Agregar jugador", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.AgregarJugadorAEquipo},
                new OpcionMenu{ nombreOpcion = "Quitar jugador", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.QuitarJugadorDeEquipo },
                new OpcionMenu{ nombreOpcion = "Volver atras", tipoOpcion = TipoOpcion.Menu }
};

            administrarJugadores = new OpcionMenu[]
            {
                new OpcionMenu{ nombreOpcion = "Alta de Jugador", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.AltaJugador },
                new OpcionMenu{ nombreOpcion = "Baja de Jugador", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.BajaJugador },
                new OpcionMenu{ nombreOpcion = "Modificacion de Jugador", tipoOpcion = TipoOpcion.Menu }, 
                new OpcionMenu{ nombreOpcion = "Volver atras", tipoOpcion = TipoOpcion.Menu }
            };

            modificarJugadores = new OpcionMenu[]
            {
                new OpcionMenu{ nombreOpcion = "Modificar datos del jugador", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.ModificarDatosJugador },
                new OpcionMenu{ nombreOpcion = "Agregar a equipo", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.AgregarEquipoAJugador},
                new OpcionMenu{ nombreOpcion = "Quitar de equipo", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.QuitarEquipoDeJugador },
                new OpcionMenu{ nombreOpcion = "Volver atras", tipoOpcion = TipoOpcion.Menu }
            };

            listados = new OpcionMenu[]
            {
                new OpcionMenu{ nombreOpcion = "Listar Jugadores asegurados", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.JugadoresAsegurados },
                new OpcionMenu{ nombreOpcion = "Listar Jugadores ordenados por edad", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.JugadoresXEdad },
                new OpcionMenu{ nombreOpcion = "Listar Jugadores agrupados por categoria", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.JugadoresXCategoria},
                new OpcionMenu{ nombreOpcion = "Volver atras", tipoOpcion = TipoOpcion.Menu }
            };

            reportes = new OpcionMenu[]
            {
                new OpcionMenu{ nombreOpcion = "Obtener jugador mas joven y mas viejo", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.MasJovenMasViejo },
                new OpcionMenu{ nombreOpcion = "Obtener cantidad de Jugadores por categoria", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.CantidadXCategoria },
                new OpcionMenu{ nombreOpcion = "Obtener promedio de edad General", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.PromedioEdad},
                new OpcionMenu{ nombreOpcion = "Volver atras", tipoOpcion = TipoOpcion.Menu }
            };

            menuPrincipal = new OpcionMenu[]
            {
                new OpcionMenu{ nombreOpcion = "Administrar Equipos", tipoOpcion = TipoOpcion.Menu },
                new OpcionMenu{ nombreOpcion = "Administrar Jugadores", tipoOpcion = TipoOpcion.Menu },
                new OpcionMenu{ nombreOpcion = "Listados", tipoOpcion = TipoOpcion.Menu },
                new OpcionMenu{ nombreOpcion = "Reportes", tipoOpcion = TipoOpcion.Menu },
                new OpcionMenu{ nombreOpcion = "Salir", tipoOpcion = TipoOpcion.Accion, accion = AccionMenu.Exit }
            };




            // lineko de relacion entre los menues para habilitar la navegacion 
            menuPrincipal[0].newMenu = administrarEquipos;
            menuPrincipal[1].newMenu = administrarJugadores;
            menuPrincipal[2].newMenu = listados;
            menuPrincipal[3].newMenu = reportes;

            administrarEquipos[2].newMenu = modificarEquipos;
            administrarEquipos[3].newMenu = menuPrincipal;

            modificarEquipos[3].newMenu = administrarEquipos;

            administrarJugadores[2].newMenu = modificarJugadores;
            administrarJugadores[3].newMenu = menuPrincipal;

            modificarJugadores[3].newMenu = administrarJugadores;

            listados[3].newMenu = menuPrincipal;
            reportes[3].newMenu = menuPrincipal;
        }

        //funcion que analiza cual es la accion y la ejecuta
        static void EjecutarAccion(AccionMenu accion, List<Equipo> equipos, List<Jugador> jugadores)
        {
            switch (accion)
            {
                case AccionMenu.AltaEquipo:
                    AltaEquipo(equipos, jugadores);
                    break;

                case AccionMenu.BajaEquipo:
                    BajaEquipo(equipos, jugadores);
                    break;

                case AccionMenu.ModificarDatosEquipo:
                    ModificarDatosEquipo(equipos, jugadores);
                    break;

                case AccionMenu.AgregarJugadorAEquipo:
                    AgregarJugadorAEquipo(equipos, jugadores);
                    break;

                case AccionMenu.QuitarJugadorDeEquipo:
                    QuitarJugadorDeEquipo(equipos, jugadores);
                    break;

                case AccionMenu.AltaJugador:
                    AltaJugador(jugadores);
                    break;

                case AccionMenu.BajaJugador:
                    BajaJugador(jugadores);
                    break;

                case AccionMenu.ModificarDatosJugador:
                    ModificarDatosJugador(jugadores);
                    break;

                case AccionMenu.AgregarEquipoAJugador:
                    AgregarEquipoAJugador(jugadores, equipos);
                    break;

                case AccionMenu.QuitarEquipoDeJugador:
                    QuitarEquipoDeJugador(jugadores, equipos);
                    break;

                case AccionMenu.JugadoresAsegurados:
                    JugadoresAsegurados(jugadores);
                    break;

                case AccionMenu.JugadoresXEdad:
                    JugadoresXEdad(jugadores);
                    break;

                case AccionMenu.JugadoresXCategoria:
                    JugadoresXCategoria(jugadores);
                    break;

                case AccionMenu.MasJovenMasViejo:
                    MasJovenMasViejo(jugadores);
                    break;

                case AccionMenu.CantidadXCategoria:
                    CantidadXCategoria(jugadores);
                    break;

                case AccionMenu.PromedioEdad:
                    PromedioEdad(jugadores);
                    break;

                case AccionMenu.Exit:
                    LimpiarPantalla();
                    Environment.Exit(0);
                    break;
            }
        }

        static int ContarEquiposPorClub(List<Equipo> equipos, string nombreClub)
        {
            int cantidad = 0;
            foreach (Equipo equip in equipos)
            {
                if (equip.nombreClub == nombreClub)
                {
                    cantidad++;
                }
            }
            return cantidad;
        }

        /// <summary>
        /// Obtiene letra para ir incrementando alfabeticamente los nombres de los equipos del mismo club
        /// </summary>
        /// <param name="numero"></param> recibe el numero de equipos del club + 1
        /// <returns>  devuelve un string A... Z, ... AA, AB... AZ</returns
        /// 
        static string ObtenerEtiqueta(int numero)  // REVISAR ESTA FUNCION
        {
            //INICIALIZA EL NUMERO 
            string resultado = "";

            while (numero > 0)
            {
                int resto = numero % 26;
                char letra = (char)('A' + resto);

                resultado = letra + resultado;
                numero /= 26;
            }

            return resultado;
        }
        /// <summary>
        /// obtiene el indice del enum categoria
        /// </summary>
        /// <param name="edad"></param>  recibe el int de la edad del jugador
        /// <returns></returns> el indice del enum que corresponde a la edad 
        static int ObtenerIndiceCategoria(int edad)
        {
            if (edad < 13) return 0;          // Infantiles
            else if (edad < 16) return 1;     // Cadetes
            else if (edad < 18) return 2;     // Juveniles
            else if (edad < 35) return 3;     // Primera
            else return 4;                    // Veteranos
        }
        /// <summary>
        /// convierte la categoria en la edad max permitida
        /// </summary>
        /// <param name="categoria"></param> string de cateogira 
        /// <returns>edad maxima permitida para la categoria</returns>
        static int ObtenerEdadMaxCategoria(String categoria)
        {
            int edadMax = -1;
            switch (categoria)
            {
                case "Infantiles":
                    edadMax = 12;
                    break;
                case "Cadetes":
                    edadMax = 15;
                    break;
                case "Juveniles":
                    edadMax = 17;
                    break;
                default:
                    edadMax = 99;
                    break;
            }
            return edadMax;
        }

        /// <summary>
        /// Devuelve un arreglo de categorias distintas a la que tiene 
        /// </summary>
        /// <param name="catActual"></param>
        /// <returns>un arreglo de categorias distintas a la que se pasa como parametro</returns>
        static String[] ObtenerCategoriasDisponibles(String catActual)
        {
            List<String> categoriasDisponibles = new List<String>();

            //recorro todas las categorias disponibles
            foreach(String cat in categoria)
            {
                //si es la que pase actual no la agrego
                if(cat == catActual)
                {
                    continue;
                }
                categoriasDisponibles.Add(cat);
            }

            return categoriasDisponibles.ToArray();
        }

        /// <summary>
        /// Verficia si el string es todo numerico, no nulo 
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        static bool VerificaDNIValido(string dni)
        {
            //verica si es nulo
            if (string.IsNullOrEmpty(dni))
                return false;

            //recorre cada caracter del string
            foreach (char c in dni)
            {
                //si no es un digito retorna falso
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Verifica si ya fue cargado antes un DNI en el listado de jugadores de la liga
        /// </summary>
        /// <param name="dni"></param>     string con el numero de dni a buscar
        /// <param name="jugadores"></param>  List de elem Jugador, con todos los jugadores de la liga
        /// <returns></returns>
        static bool DNIExistente (string dni, List<Jugador> jugadores)
        {
            foreach(Jugador jug in jugadores)
            {
                if(jug.dni == dni)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica que un valor string sea un int dentro de un rango
        /// </summary>
        /// <param name="valor"></param>  valor string
        /// <param name="valorMin"></param>  valor minimo
        /// <param name="valorMax"></param>  valor maximo
        /// <returns></returns>
        static bool VerificaIntRango(string valor, int valorMin, int valorMax)
        {
            //verica si es nulo
            if (string.IsNullOrEmpty(valor))
                return false;

            //recorre cada caracter del string
            foreach (char c in valor)
            {
                //si no es un digito retorna falso
                if (!char.IsDigit(c))
                    return false;
            }

            int valorNum = int.Parse(valor);
            if(valorNum < valorMin || valorNum > valorMax)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// recib en string y ve si es s, S, n o N para ser un bool
        /// </summary>
        /// <param name="valor"></param> recibe un string 
        /// <returns></returns>
        static bool ValidaBool(string valor)
        {
            switch(valor)
            {
                case "s":
                case "S":
                case "n":
                case "N":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// funcion que se usa luego de ValidaBool para asegurarse que reciba solo S, s, N o n
        /// </summary>
        /// <param name="valor"></param>  valor que recibe (S, s, N o n
        /// <returns> true si es S o s, y false si es n o N .. se pone default en false por sintaxis</returns>
        static bool ValidaSoN(string valor)
        {
            switch (valor)
            {
                case "s":
                case "S":
                    return true;
                    
                case "n":
                case "N":
                    return false;

                default:
                    return false;
            }

        }


        /// <summary>
        /// Recibe un listado y una opcion , y verifica si la opcion elegida es correcta 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listado"></param> listado generaico 
        /// <param name="tipo"></param>  puede ser jugador o equipo en principio
        /// <returns></returns>  el numero elegido dentro del listado, -1 si es que desea salir de la eleccion
        static int ElegirOpcion<T> (List<T> listado, string tipo)
        {
            string opc;
            int opcNum = -1;
            while (true)
            {

                Console.WriteLine($"Elija el numero de {tipo} que desea: (S p/salir)");

                opc = Console.ReadLine();

                //verifica si quiere salir de la baja
                if (opc == "s" || opc == "S")
                {
                    LimpiarPantalla();
                    return opcNum;   // devuelve -1 que es salir de pantalla 
                }
                if (ValidarOpcionElegida(listado, opc))
                {
                    opcNum = int.Parse(opc) - 1; // lo ajusto al index 
                    return opcNum;
                }
                else
                {
                    Console.WriteLine("Opcion Invalida!!!");
                }
            }

        }
        /// <summary>
        /// Devuelve un Listado de equipos a los cuales puedo agregar al jugador
        /// </summary>
        /// <param name="jugador"></param> jugador al que quiero agregar un nuevo equipo
        /// <param name="equipos"></param> el listado de todos los equipos de la liga
        /// <returns>el listado de equipos a los que lo puedo agregar</returns>
        static List<Equipo> BuscarEquiposDisponibles(Jugador jugador,  List<Equipo> equipos)
        {
            //creto una variable con el nombre de club si ya tiene equipos asig
            string nombreClub = "";
            //creo una list de equipos disponibles para devolver
            List<Equipo> equiposDisp = new List<Equipo>();
            // si ya tiene equipos asignados obtengo el club *** solo puede jugar en un club
            if(jugador.equipAsig.Count > 0)
            {
                nombreClub = jugador.equipAsig[0].nombreClub;
            }

            foreach(Equipo equip in equipos)
            {
                //si el jugador ya juega en un club y el club del equipo es otro
                if(nombreClub != "" && equip.nombreClub != nombreClub)
                {
                    continue;
                }

                //si la edad del jugador es menor o igual a la de la categoria del equipo 
                if(jugador.edad <= ObtenerEdadMaxCategoria(equip.categoria))
                {
                    bool yaEsta = false;
                    
                    foreach(Equipo asig in jugador.equipAsig)
                    {
                        //recorro los equipos que ya tiene asig el jugador para ver si equip ya se encuentra
                        if(asig.nombreEquipo == equip.nombreEquipo)
                        {
                            yaEsta = true;
                            break;
                        }
                    }

                    //si no se encontraba lo agrego al list
                    if(!yaEsta)
                    {
                        equiposDisp.Add(equip);
                    }
                    
                }
            }

            return equiposDisp;
        }  
        /// <summary>
        /// Funcion para elegir que jugadores pueden ingresar a un equipo
        /// </summary>
        /// <param name="equipo"></param>
        /// <param name="jugadores"></param>
        /// <returns>devuelve un list con los jugadores que pueden ingresar</returns>
        static List<Jugador> ElegirJugadoresCandidatos(Equipo equipo, List<Jugador> jugadores)
        {
            //creo una lista de jugadores que podrian ingresar al equipo
            List<Jugador> jugadoresCandidatos = new List<Jugador>();

            foreach (Jugador jug in jugadores)
            {
                // si ya pertenece al equipo, lo salto
                if (jug.EstaEnEquipo(equipo))
                {
                    continue;
                }

                // verifico edad maxima de categoria
                if (jug.edad > ObtenerEdadMaxCategoria(equipo.categoria))
                {
                    continue;
                }

                // si juega en otro club, lo salto
                if (jug.equipAsig.Count > 0 && jug.equipAsig[0].nombreClub != equipo.nombreClub)
                {
                    continue;
                }

                jugadoresCandidatos.Add(jug);
            }

            return jugadoresCandidatos;
        }
        


        //**************************************************************************************************
        //*** PROGRAMA PRINCIPAL
        //************************************************************************************************** 



        static void Main(string[] args)
        {
            //incializo los menues
            InicializarMenues();

            // creo lista de equipos
            List<Equipo> equipos = new List<Equipo>();

            // creo una lista de jugadores
            List<Jugador> jugadores = new List<Jugador>();

            //cargo datos para testeo .. BORRAR 
            //DatosTest.CargaTestCorta(jugadores, equipos);
            //DatosTest.CargaTest(jugadores, equipos);

            //pongo al menu activo apuntando al menu principal
            OpcionMenu[] currentMenu = menuPrincipal;

            //loop principal del programa
            while(true)
            {
                //inicializo la opcion de seleccion en null
                string opc = null;

                //Imprimo el menu actual               
                ImprimirListado(currentMenu);

                //capturo la eleccion del usuario
                opc = Console.ReadLine();

                // si la opcion es valida
                if (ValidarOpcionElegida(menuPrincipal, opc))
                {
                    int seleccion = int.Parse(opc) - 1; // le resto 1 para poner el indice del arreglo

                    // es una accion ?
                    if (currentMenu[seleccion].tipoOpcion == TipoOpcion.Accion)
                    {
                        // ejecuta la accion
                        EjecutarAccion(currentMenu[seleccion].accion, equipos, jugadores);
                    }
                    else  //si no es una accion es un menu
                    {
                        // apunto el menu activo al nuevo menu elegido
                        currentMenu = currentMenu[seleccion].newMenu;
                        LimpiarPantalla();
                    }
                }
            }
        }
    }
}
