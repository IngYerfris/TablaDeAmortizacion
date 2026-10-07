using Spectre.Console;


Console.Write("Ingrese el monto del préstamo: ");
decimal monto = decimal.Parse(Console.ReadLine()!);

Console.Write("Ingrese la tasa de interés anual (%): ");
decimal tasaAnual = decimal.Parse(Console.ReadLine()!);

Console.Write("Ingrese el plazo del préstamo (meses): ");
int plazo = int.Parse(Console.ReadLine()!);


decimal tasaMensual = (tasaAnual / 100) / 12;


decimal cuota;

if (tasaMensual == 0)
{
    
    cuota = monto / plazo;
}
else
{
    

    double potencia = Math.Pow(
        (double)(1 + tasaMensual),
        plazo
    );

    cuota = monto *
            (tasaMensual * (decimal)potencia) /
            ((decimal)potencia - 1);
}


cuota = Math.Round(cuota, 2);




Console.WriteLine($"Monto: RD$ {monto:N2}");
Console.WriteLine($"Tasa anual: {tasaAnual:N2}%");
Console.WriteLine($"Tasa mensual: {(tasaMensual * 100):N4}%");
Console.WriteLine($"Plazo: {plazo} meses");
Console.WriteLine($"Cuota mensual: RD$ {cuota:N2}");

Console.WriteLine();



var tabla = new Table();

tabla.Border(TableBorder.Rounded);

tabla.AddColumn("No. de cuota");
tabla.AddColumn("Pago de cuota");
tabla.AddColumn("Interés a pagar");
tabla.AddColumn("Abono a capital");
tabla.AddColumn("Saldo");



decimal saldo = monto;

for (int mes = 1; mes <= plazo; mes++)
{
    // Calcular interés del período
    decimal interes = saldo * tasaMensual;

    // Calcular abono al capital
    decimal abonoCapital = cuota - interes;

    // Actualizar el saldo
    saldo = saldo - abonoCapital;

    
    if (mes == plazo)
    {
        saldo = 0;
    }

    interes = Math.Round(interes, 2);
    abonoCapital = Math.Round(abonoCapital, 2);
    saldo = Math.Round(saldo, 2);

    // Agregar fila a la tabla
    tabla.AddRow(
        mes.ToString(),
        $"RD$ {cuota:N2}",
        $"RD$ {interes:N2}",
        $"RD$ {abonoCapital:N2}",
        $"RD$ {saldo:N2}"
    );
}



AnsiConsole.Write(
    new Panel(tabla)
        .Header("[yellow]Tabla de Amortización[/]")
        .Border(BoxBorder.Double)
);