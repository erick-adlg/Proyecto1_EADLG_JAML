using System;

class Program
{
    static void Main(string[] args)
    {
        // ============================================
        // SISTEMA DE PARQUEO INTELIGENTE - SMARTPARK
        // Proyecto de laboratorio No. 1
        // ============================================
        // Variables para registro
        string nombreOperador = "";
        string codigoTurno = "";
        int capacidadParqueo = 0;

        // Variables generales del sistema
        int ticketsCreados = 0;
        int ticketsCerrados = 0;
        double totalRecaudado = 0;
        int tiempoSimuladoMinutos = 0;
        bool ticketActivo = false;

        // Variables del ticket activo
        string placaVehiculo = "";
        int tipoVehiculo = 0; // 1 = Moto, 2 = Auto, 3 = Pickup/SUV
        string nombreCliente = "";
        int minutoEntrada = 0;

        // Otras variables
        int opcionMenu = 0;
        bool entradaValida = false;

        // Registro inicial del sistema
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("""
            =======================
            Bienvenido a SmartPark!
            =======================
            """);
        Console.ResetColor();

        // Solicitar nombre del operador
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ingrese el nombre del operador: ");
            Console.ResetColor();
            nombreOperador = Console.ReadLine();

            if (nombreOperador == null)
            {
                nombreOperador = "";
            }
            nombreOperador = nombreOperador.Trim();
            if (nombreOperador != "")
            {
                entradaValida = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: el nombre del operador no puede estar vacío.");
                Console.ResetColor();
                entradaValida = false;
            }

        } while (!entradaValida);

        // Solicitar código de turno
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ingrese el código de turno (exactamente 4 caracteres): ");
            Console.ResetColor();
            codigoTurno = Console.ReadLine();
            if (codigoTurno == null)
            {
                codigoTurno = "";
            }
            codigoTurno = codigoTurno.Trim();
            if (codigoTurno.Length == 4)
            {
                entradaValida = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: el código de turno debe tener exactamente 4 caracteres.");
                Console.ResetColor();
                entradaValida = false;
            }

        } while (!entradaValida);
        // Solicitar capacidad del parqueo
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ingrese la capacidad del parqueo (mínimo 10): ");
            Console.ResetColor();
            string textoCapacidad = Console.ReadLine();

            if (int.TryParse(textoCapacidad, out capacidadParqueo))
            {
                if (capacidadParqueo >= 10)
                {
                    entradaValida = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: la capacidad debe ser un número entero mayor o igual a 10.");
                    Console.ResetColor();
                    entradaValida = false;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: debe ingresar un número entero válido.");
                Console.ResetColor();
                entradaValida = false;
            }

        } while (!entradaValida);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nSistema inicializado correctamente.");
        Console.ResetColor();

        // MENÚ PRINCIPAL
        do
        {
            int espaciosOcupados = 0;
            int espaciosDisponibles = 0;

            if (ticketActivo)
            {
                espaciosOcupados = 1;
            }
            else
            {
                espaciosOcupados = 0;
            }

            espaciosDisponibles = capacidadParqueo - espaciosOcupados;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("""
                ===============
                Menu Principal
                ===============
                """);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Operador: " + nombreOperador + " | Turno: " + codigoTurno);
            Console.WriteLine("Tiempo simulado actual: " + tiempoSimuladoMinutos + " minutos");
            Console.WriteLine("Espacios ocupados: " + espaciosOcupados + " | Espacios disponibles: " + espaciosDisponibles);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("1. Crear ticket de entrada");
            Console.WriteLine("2. Registrar salida y calcular cobro");
            Console.WriteLine("3. Ver estado del parqueo");
            Console.WriteLine("4. Simular paso del tiempo");
            Console.WriteLine("5. Salir");
            Console.WriteLine("----------------------------------------------");
            Console.Write("Seleccione una opción: ");
            Console.ResetColor();

            string textoOpcion = Console.ReadLine();

            if (!int.TryParse(textoOpcion, out opcionMenu))
            {
                opcionMenu = 0;
            }

            // Opcion 1, crear ticket de entrada
            if (opcionMenu == 1)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------ Crear Ticket de Entrada ------");
                Console.ResetColor();

                if (ticketActivo)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: ya existe un ticket activo. No se puede crear otro.");
                    Console.ResetColor();
                }
                else if (espaciosDisponibles <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: el parqueo está lleno.");
                    Console.ResetColor();
                }
                else
                {
                    // Validar placa
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Ingrese la placa del vehículo (6 a 8 caracteres, sin espacios): ");
                        Console.ResetColor();

                        placaVehiculo = Console.ReadLine();

                        if (placaVehiculo == null)
                        {
                            placaVehiculo = "";
                        }

                        placaVehiculo = placaVehiculo.Trim();

                        if (placaVehiculo.Length >= 6 && placaVehiculo.Length <= 8 && !placaVehiculo.Contains(" "))
                        {
                            entradaValida = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: la placa debe tener entre 6 y 8 caracteres y no debe contener espacios.");
                            Console.ResetColor();
                            entradaValida = false;
                        }

                    } while (!entradaValida);

                    // Validar tipo de vehículo
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Ingrese tipo de vehículo (1 = Moto, 2 = Auto, 3 = Pickup/SUV): ");
                        Console.ResetColor();

                        string textoTipoVehiculo = Console.ReadLine();

                        if (int.TryParse(textoTipoVehiculo, out tipoVehiculo))
                        {
                            if (tipoVehiculo >= 1 && tipoVehiculo <= 3)
                            {
                                entradaValida = true;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error: el tipo de vehículo debe ser 1, 2 o 3.");
                                Console.ResetColor();
                                entradaValida = false;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: debe ingresar un número válido.");
                            Console.ResetColor();
                            entradaValida = false;
                        }

                    } while (!entradaValida);

                    // Validar nombre del cliente
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Ingrese el nombre del cliente: ");
                        Console.ResetColor();

                        nombreCliente = Console.ReadLine();

                        if (nombreCliente == null)
                        {
                            nombreCliente = "";
                        }

                        nombreCliente = nombreCliente.Trim();

                        if (nombreCliente != "")
                        {
                            entradaValida = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: el nombre del cliente no puede estar vacío.");
                            Console.ResetColor();
                            entradaValida = false;
                        }

                    } while (!entradaValida);

                    // Guardar minuto de entrada y activar ticket
                    minutoEntrada = tiempoSimuladoMinutos;
                    ticketActivo = true;
                    ticketsCreados++;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ticket creado correctamente.");
                    Console.WriteLine("Minuto de entrada registrado: " + minutoEntrada);
                    Console.ResetColor();
                }
            }

            // Opcion 2, registrar salida y calcular cobro
            else if (opcionMenu == 2)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------ Registrar salida y Calcular cobro ------");
                Console.ResetColor();

                if (!ticketActivo)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: no existe un ticket activo para registrar salida.");
                    Console.ResetColor();
                }
                else
                {
                    int minutosEstacionado = tiempoSimuladoMinutos - minutoEntrada;
                    int horasCobradas = 0;
                    double tarifaPorHora = 0;
                    double montoBase = 0;
                    double multa = 0;
                    double descuentoVip = 0;
                    double recargoPermanenciaExtrema = 0;
                    double montoFinal = 0;
                    string respuestaVip = "";

                    // Asignar tarifa según tipo de vehículo
                    if (tipoVehiculo == 1)
                    {
                        tarifaPorHora = 5;
                    }
                    else if (tipoVehiculo == 2)
                    {
                        tarifaPorHora = 10;
                    }
                    else
                    {
                        tarifaPorHora = 15;
                    }

                    // Calcular horas cobradas por fracción de hora
                    if (minutosEstacionado <= 15)
                    {
                        horasCobradas = 0;
                        montoBase = 0;
                    }
                    else
                    {
                        horasCobradas = minutosEstacionado / 60;

                        if (minutosEstacionado % 60 != 0)
                        {
                            horasCobradas = horasCobradas + 1;
                        }

                        montoBase = horasCobradas * tarifaPorHora;
                    }

                    // Aplicar multa si supera 6 horas
                    if (minutosEstacionado > 360)
                    {
                        multa = 25;
                    }
                    else
                    {
                        multa = 0;
                    }

                    // Preguntar si es VIP
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("¿El cliente es VIP? (si/no): ");
                        Console.ResetColor();

                        respuestaVip = Console.ReadLine();

                        if (respuestaVip == null)
                        {
                            respuestaVip = "";
                        }

                        respuestaVip = respuestaVip.Trim().ToLower();

                        if (respuestaVip == "si" || respuestaVip == "no")
                        {
                            entradaValida = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: responda únicamente con 'si' o 'no'.");
                            Console.ResetColor();
                            entradaValida = false;
                        }

                    } while (!entradaValida);

                    // Subtotal antes del descuento VIP
                    montoFinal = montoBase + multa;

                    // Aplicar descuento VIP del 10%
                    if (respuestaVip == "si")
                    {
                        descuentoVip = montoFinal * 0.10;
                    }
                    else
                    {
                        descuentoVip = 0;
                    }

                    montoFinal = montoFinal - descuentoVip;

                    // Aplicar recargo por permanencia extrema si supera 12 horas
                    if (minutosEstacionado > 720)
                    {
                        recargoPermanenciaExtrema = montoFinal * 0.20;
                        montoFinal = montoFinal + recargoPermanenciaExtrema;
                    }
                    else
                    {
                        recargoPermanenciaExtrema = 0;
                    }

                    // Mostrar detalle del cobro
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n========== Detalle de cobro ==========");
                    Console.ResetColor();

                    Console.WriteLine("Cliente: " + nombreCliente);
                    Console.WriteLine("Placa: " + placaVehiculo);

                    if (tipoVehiculo == 1)
                    {
                        Console.WriteLine("Tipo de vehículo: Moto");
                    }
                    else if (tipoVehiculo == 2)
                    {
                        Console.WriteLine("Tipo de vehículo: Auto");
                    }
                    else
                    {
                        Console.WriteLine("Tipo de vehículo: Pickup/SUV");
                    }

                    Console.WriteLine("Minuto de entrada: " + minutoEntrada);
                    Console.WriteLine("Minuto de salida: " + tiempoSimuladoMinutos);
                    Console.WriteLine("Minutos estacionado: " + minutosEstacionado);
                    Console.WriteLine("Horas cobradas: " + horasCobradas);
                    Console.WriteLine("Tarifa por hora: Q" + tarifaPorHora.ToString("0.00"));
                    Console.WriteLine("Monto base: Q" + montoBase.ToString("0.00"));
                    Console.WriteLine("Multa por más de 6 horas: Q" + multa.ToString("0.00"));
                    Console.WriteLine("Descuento VIP: Q" + descuentoVip.ToString("0.00"));
                    Console.WriteLine("Recargo permanencia extrema: Q" + recargoPermanenciaExtrema.ToString("0.00"));
                    Console.WriteLine("Monto final a pagar: Q" + montoFinal.ToString("0.00"));

                    if (minutosEstacionado <= 15)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Aplicó gratuidad por estancia de 15 minutos o menos.");
                        Console.ResetColor();
                    }

                    if (recargoPermanenciaExtrema > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Se aplicó recargo por permanencia prolongada mayor a 12 horas.");
                        Console.ResetColor();
                    }

                    // Actualizar acumulados y liberar ticket
                    totalRecaudado = totalRecaudado + montoFinal;
                    ticketsCerrados++;
                    ticketActivo = false;

                    // Limpiar datos del ticket
                    placaVehiculo = "";
                    tipoVehiculo = 0;
                    nombreCliente = "";
                    minutoEntrada = 0;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Salida registrada correctamente. Ticket cerrado.");
                    Console.ResetColor();
                }
            }

            // Opcion 3, ver estado del parqueo
            else if (opcionMenu == 3)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------ Estado del Parqueo ------");
                Console.ResetColor();

                Console.WriteLine("Capacidad total: " + capacidadParqueo);

                if (ticketActivo)
                {
                    espaciosOcupados = 1;
                }
                else
                {
                    espaciosOcupados = 0;
                }

                espaciosDisponibles = capacidadParqueo - espaciosOcupados;

                Console.WriteLine("Espacios ocupados: " + espaciosOcupados);
                Console.WriteLine("Espacios disponibles: " + espaciosDisponibles);
                Console.WriteLine("Tiempo simulado acumulado: " + tiempoSimuladoMinutos + " minutos");
                Console.WriteLine("Total recaudado: Q" + totalRecaudado.ToString("0.00"));
                Console.WriteLine("Tickets creados: " + ticketsCreados);
                Console.WriteLine("Tickets cerrados: " + ticketsCerrados);

                if (ticketActivo)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nHay un ticket activo actualmente:");
                    Console.ResetColor();

                    Console.WriteLine("Placa: " + placaVehiculo);

                    if (tipoVehiculo == 1)
                    {
                        Console.WriteLine("Tipo: Moto");
                    }
                    else if (tipoVehiculo == 2)
                    {
                        Console.WriteLine("Tipo: Auto");
                    }
                    else
                    {
                        Console.WriteLine("Tipo: Pickup/SUV");
                    }

                    Console.WriteLine("Cliente: " + nombreCliente);
                    Console.WriteLine("Minuto de entrada: " + minutoEntrada);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("No hay ticket activo en este momento.");
                    Console.ResetColor();
                }
            }

            // Opcion 4, simular paso del tiempo
            else if (opcionMenu == 4)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------ Simular paso del tiempo ------");
                Console.ResetColor();

                int minutosAgregar = 0;

                do
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Ingrese la cantidad de minutos a simular (1 a 1440): ");
                    Console.ResetColor();

                    string textoMinutosAgregar = Console.ReadLine();

                    if (int.TryParse(textoMinutosAgregar, out minutosAgregar))
                    {
                        if (minutosAgregar >= 1 && minutosAgregar <= 1440)
                        {
                            entradaValida = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: debe ingresar un valor entre 1 y 1440.");
                            Console.ResetColor();
                            entradaValida = false;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: debe ingresar un número entero válido.");
                        Console.ResetColor();
                        entradaValida = false;
                    }

                } while (!entradaValida);

                tiempoSimuladoMinutos = tiempoSimuladoMinutos + minutosAgregar;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tiempo actualizado correctamente.");
                Console.WriteLine("Tiempo simulado acumulado: " + tiempoSimuladoMinutos + " minutos.");
                Console.ResetColor();

                // Advertencias si hay ticket activo
                if (ticketActivo)
                {
                    int minutosEstacionado = tiempoSimuladoMinutos - minutoEntrada;

                    if (minutosEstacionado > 720)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Advertencia: el vehículo ya superó las 12 horas. Se aplicará recargo por permanencia extrema al salir.");
                        Console.ResetColor();
                    }
                    else if (minutosEstacionado > 360)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Advertencia: el vehículo ya superó las 6 horas. Se aplicará multa al salir.");
                        Console.ResetColor();
                    }
                }
            }

            // Opcion 5, Salir
            else if (opcionMenu == 5)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("============= Resumen General =============");
                Console.ResetColor();

                int espaciosOcupadosFinal = 0;
                int espaciosDisponiblesFinal = 0;

                if (ticketActivo)
                {
                    espaciosOcupadosFinal = 1;
                }
                else
                {
                    espaciosOcupadosFinal = 0;
                }

                espaciosDisponiblesFinal = capacidadParqueo - espaciosOcupadosFinal;

                Console.WriteLine("Operador: " + nombreOperador);
                Console.WriteLine("Código de turno: " + codigoTurno);
                Console.WriteLine("Capacidad del parqueo: " + capacidadParqueo);
                Console.WriteLine("Espacios ocupados al cierre: " + espaciosOcupadosFinal);
                Console.WriteLine("Espacios disponibles al cierre: " + espaciosDisponiblesFinal);
                Console.WriteLine("Tickets creados: " + ticketsCreados);
                Console.WriteLine("Tickets cerrados: " + ticketsCerrados);
                Console.WriteLine("Tiempo simulado total: " + tiempoSimuladoMinutos + " minutos");
                Console.WriteLine("Total recaudado: Q" + totalRecaudado.ToString("0.00"));

                if (ticketActivo)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Atención: queda un ticket activo sin cerrar.");
                    Console.WriteLine("Placa pendiente: " + placaVehiculo);
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nGracias por utilizar SmartPark.");
                Console.ResetColor();
            }

            // Opcion invalida
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: opción no válida. Intente nuevamente.");
                Console.ResetColor();
            }

        } while (opcionMenu != 5);
        Console.Clear();
    }
}