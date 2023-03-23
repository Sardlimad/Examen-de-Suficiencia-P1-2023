/*
Examen de Suficiencia P1 - 2023
Estudiante: David Sardinias Lima.
Brigada: 4105
Lenguaje: C#
*/

//-----------------------------------------------------------

//Matriz de 5x5 para almacenar 5 transferencias de 5 dias.
double[,] Trans_Day = new double[5, 5];

//Ciclo Infinito para siempre regresar al menu de opciones
while (true)
{
    Console.Clear(); //Para limpiar la pantalla cada vez que se selecciona una opcion

    //Menu de Opciones
    Console.WriteLine("*********OPCIONES*********");
    Console.WriteLine("(A) Entrar Transferencias");
    Console.WriteLine("(B) Ver Reporte");
    Console.WriteLine("(C) Ver Lista");
    Console.WriteLine("(D) Editar");
    Console.WriteLine("(E) Eliminar");
    Console.WriteLine("(X) Salir");
    Console.WriteLine("**************************");

    //Realizar una accion segun desee el usuario.
    switch (Console.ReadKey(true).Key)
    {

        case ConsoleKey.A: NewReg(); break;

        case ConsoleKey.B: Reporte(); break;

        case ConsoleKey.C: Listar(Trans_Day); break;

        case ConsoleKey.D: Corregir(); break;

        case ConsoleKey.E: Eliminar(); break;

        case ConsoleKey.X: Salir(); break;

        default: break;
    }

    Console.WriteLine("****Presiones una tecla para volver al MENU****");
    Console.ReadKey();
}

//Crear nuevo registro de transacciones
void NewReg()
{
    Console.Clear(); //Se limpia la pantalla para borrar el menu.

    Console.WriteLine("*********REGISTRAR OPERACIONES*********");

    int[] cant_trans = new int[5];
    for (int i = 0; i < Trans_Day.GetLength(1); i++)
    {
        Console.WriteLine("Cantidad de Transacciones en el Dia " + (i + 1));
        cant_trans[i] = int.Parse(Console.ReadLine());
    }
    Console.WriteLine("**************************************");

    RegTrans(cant_trans);
}

void RegTrans(int[] cant_trans)
{
    //Iterar los 5 dias de la semana
    for (int i = 0; i < Trans_Day.GetLength(0); i++)
    {
        Console.WriteLine("******DIA " + (i + 1) + " ******");

        //Iterar las 5 transacciones del DIA 'i'
        for (int j = 0; j < cant_trans[i]; j++)
        {
            Console.Write("Monto Transaccion " + (j + 1) + ":");

            //Guardo el monto de la transaccion 'j' del DIA 'i'

            string entrada = Console.ReadLine();
            //verificar si la entrada no es nula
            if (entrada != null) { Trans_Day[i, j] = Convert.ToDouble(entrada); } else { Trans_Day[i, j] = 0; }
            
        }
    }
}
//Verificar si la transaccion es valida, o sea diferente de 0.
bool IsTrans(double monto)
{
    if (monto != 0) return true;

    return false;
}
/*
Clasificar Transacciones
1 Ordinarias, 2 Regulares, 3 Complejas
*/
int Clasificar(double monto)
{
    if (monto <= 1000d && monto > 0d) { return 1; }
    if (monto > 1000d && monto <= 3000d) { return 2; }
    if (monto > 3000d && monto <= 10000d) { return 3; }

    return 0;
}

//Hace una llamada a los metodos de las operaciones que requiere un reporte.
void Reporte()
{
    Console.Clear();

    Console.WriteLine("*****************REPORTE*****************");

    Contador(); //Monstrar total de transacciones por dia.

    Console.WriteLine("Monto Total de Transacciones Complejas: "); Console.WriteLine(MontoTotal(3));  //Monto total de las transacciones COMPLEJAS(3).

    Console.WriteLine("Monto Total de Recaudacion por intereses: "); Console.WriteLine(Intereses()); //Total de Recaudaciones por intereses.

}

//Determinar el monto total de las transacciones x clasificacion.
double MontoTotal(int type)
{
    //Monto Total de Transacciones Complejas
    double total = 0;

    //Iterar los 5 dias de la semana
    for (int i = 0; i < Trans_Day.GetLength(0); i++)
    {
        //Iterar las 5 transacciones del DIA 'i'
        for (int j = 0; j < Trans_Day.GetLength(1); j++)
        {
            if (Clasificar(Trans_Day[i, j]) == type)
            {
                total += Trans_Day[i, j];
            }
        }
    }

    return total;
}

//LLeva el conteo de la cantidad de transacciones x dia.
void Contador()
{
    Console.WriteLine("****Cantidad de Transacciones por Dia****");

    //Contador
    int count = 0;

    //Iterar los 5 dias de la semana
    for (int i = 0; i < Trans_Day.GetLength(0); i++)
    {
        //Iterar las 5 transacciones del DIA 'i'
        for (int j = 0; j < Trans_Day.GetLength(1); j++)
        {
            if (IsTrans(Trans_Day[i, j])) { count++; } //Si el monto de transaccion es diferente de 0 significa que se hizo transaccion.
        }

        Console.WriteLine("DIA " + +(i + 1) + ":" + count); //Despues de contar la cantidad de transacciones del dia imprimo el valor en consola.
        count = 0;   //Reinicio el contador a 0.
    }

}

//Total de Recaudaciones por tasa de interes
double Intereses()
{

    double total = 0d;

    //Iterar los 5 dias de la semana
    for (int i = 0; i < Trans_Day.GetLength(0); i++)
    {
        //Iterar las 5 transacciones del DIA 'i'
        for (int j = 0; j < Trans_Day.GetLength(1); j++)
        {
            switch (Clasificar(Trans_Day[i, j]))
            {
                case 1: total += IntOrdinaria(Trans_Day[i, j]); break;
                case 2: total += IntRegulares(Trans_Day[i, j]); break;
                case 3: total += IntComplejas(Trans_Day[i, j]); break;
            }
        }
    }
    return total;
}

//Calcular Interes de Operaciones Ordinarias
double IntOrdinaria(double monto) { return monto * 0.027d; }

//Calcular Interes de Operaciones Regulares
double IntRegulares(double monto)
{
    //Uso una variable auxiliar para guardar cuantas veces se puede desglozar el monto en 1000.
    double aux = monto / 1000d;

    return monto * (0.027d + (0.003d * aux));
}

//Calcular Interes de Operaciones Complejas
double IntComplejas(double monto) { return monto * 0.15d; }


//Asigna la letra segun el numero de dia: 
char Dia(int i)
{
    switch (i)
    {
        case 0: return 'L';
        case 1: return 'M';
        case 2: return 'W';
        case 3: return 'J';
        case 4: return 'V';
    }
    return 'Z';
}


//Listar Transacciones
void Listar(double[,] Trans_Day)
{
    Console.Clear();
    Console.WriteLine("*****LISTADO DE TRANSACCIONES*****");
    //Iterar los 5 dias de la semana
    for (int i = 0; i < Trans_Day.GetLength(0); i++)
    {
        //Iterar las 5 transacciones del DIA 'i'
        for (int j = 0; j < Trans_Day.GetLength(1); j++)
        {
            if (IsTrans(Trans_Day[i, j]))
            {
                //Imprimiendo en Consola la Codificacion.
                Console.WriteLine(Dia(i) + "_" + (j+1) + "_" + Clasificar(Trans_Day[i, j]) + "_" + Trans_Day[i, j]);
            }
        }

    }
}

void Corregir()
{
    Console.WriteLine("*******EDITAR TRANSACCION*******");
    //Se le pide al usuario que introduzca el dia y numero de la transferencia que desea corregir.
    Console.Write("Dia de la Transferencia:");
    int i = int.Parse(Console.ReadLine());
    Console.Write("No. de la Transferencia:");
    int j = int.Parse(Console.ReadLine());

    //Se le muestra el monto actual de la transferencia. 
    Console.WriteLine("Monto Actual:" + Trans_Day[i - 1, j - 1]);
    Console.Write("Nuevo Monto:");
    Trans_Day[i - 1, j - 1] = double.Parse(Console.ReadLine());
}

void Eliminar()
{
    Console.Clear();
    Console.WriteLine("******ELIMINAR TRANSACCION******");
    
    //Se le pide al usuario que introduzca el dia y numero de la transferencia que desea eliminar.
    Console.Write("Dia de la Transferencia:");
    int i = int.Parse(Console.ReadLine());
    Console.Write("No. de la Transferencia:");
    int j = int.Parse(Console.ReadLine());

    //Se le muestra el monto actual de la transferencia. 
    Console.WriteLine("Monto:" + Trans_Day[i - 1, j - 1]);
    Console.WriteLine("********************************");

    //Se pide confirmacion del usuario
    Console.WriteLine("Desea eliminar la transferencia? (S/N) Default N.");

    if(Console.ReadKey(true).Key == ConsoleKey.S){Trans_Day[i - 1, j - 1] = 0;}

    
    // Console.WriteLine("");
    // char x = char.Parse(Console.ReadLine());

    
    // if (x == 's' || x == 'S') { Trans_Day[i - 1, j - 1] = 0; }

}

void Salir()
{
    Console.Clear();

    Console.WriteLine("Desea SALIR?(S/N)");
    
    if(Console.ReadKey(true).Key == ConsoleKey.S){Environment.Exit(0);}
}