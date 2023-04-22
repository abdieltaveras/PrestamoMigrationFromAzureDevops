
//let  BalanceDocumentoCXC = 100000;
//let  MontoPagado = 10000;
//let  Capital = 5000;
//let  Interes = 2000;
//let  Moras = 1500;
//let  OtrosDebitos = 1500;
//let  CodigoCliente = "00000001";
//let  CuotasAtrasadas = 6;
//let  MontoAtrasado = 26000;
//let  Fecha = "25/02/2022";
//let  NombreDocumento = "Recibo Ingreso";
//let  NumeracionDocumento = "00007548";
//let  NumeracionGarantia = "lc6paga1526edfrt";
//let  TotalCuotasPorPagar = 15;
//let  NombreCompletoCliente = "Pedro Rodriguez Gutierrez";
//let  NombreDocumentoCxC = "Prestamo";
//let  InteresDespuesDeVencido = 0;
//let  NombreUsuario = "Ramon Rutin";
//let  LoginName = "RamonR";
//let  NombreNegocio = "Prestamos Rapido";
//let  ClienteTaxId = "112108236";

//function GenerarFichaDrCr(datos = null) {

//    console.log(datos);
//    document.write('<div id="divDrCr" >')
//    document.write("========================" + "<br/>")
//    document.write("**********PCPROG*********"+"<br />")
//    document.write("========================"+"<br />")
//    document.write("asdsad"+"<br />")
//    document.write("8095508455"+"<br />")
//    document.write("RNC110645041"+"<br />")
//    document.write("Recibo De Caja"+"<br />");
//    document.write("------------------"+"<br />");
//    document.write("Recibo No. 0000127"+"<br />");
//    document.write(`Prestamo:${NumeracionDocumento}`+"<br />");
//    document.write(`Cliente:${NombreCompletoCliente}`+"<br />");
//    document.write(`Garantia:${NumeracionGarantia}`+"<br />");
//    document.write(`Fecha:${Fecha}`+"<br />");
//    document.write("------------------"+"<br />");
//    document.write(`CPIC Cargados:${0.00}`+"<br />");
//    document.write("------------------"+"<br />");
//    document.write(`INFORME DEL PAGO`+"<br />");
//    document.write(`CPIC Pagados:${0.00}`+"<br />");
//    document.write(`BCES GENERALES AL MOMENTO`+"<br />");
//    document.write(`Bce:${BalanceDocumentoCXC}`+"<br />");
//    document.write(`Bce:${MontoPagado}`+"<br />");
//    document.write(`Bce:${BalanceDocumentoCXC - MontoPagado}`+"<br />");
//    document.write(`OTROS PRESTAMOS ACTIVOS`+"<br />");
//    document.write(`Bce:${BalanceDocumentoCXC - MontoAtrasado}`+"<br />");
//    document.write(`INFORMACION DE DEUDA ATRASADA`+"<br />");
//    document.write(`Pagares Atrasados:${CuotasAtrasadas}`+"<br />");
//    document.write(`Monto Atrasado:${MontoAtrasado}`+"<br />");
//    document.write(`Total Pagares/Pagar:${Math.round(MontoAtrasado / CuotasAtrasadas)}` + "<br />");
//    document.write('</div>');
//    printDiv("divDrCr");
//    // document.write('<button onclick="window.print()">Imprimir</button>')
//}



function GenerarReciboIng(datos = null) {
    if (datos != null) {

        var myWindow = window.open('', '', 'width=800,height=600')
        myWindow.document.write(`<html lang="en">`)
        myWindow.document.write(`<style> html *
            {
                font-size: 13px!important;
                color: #000!important;
                font-family: System !important;
            }</style>`)
        myWindow.document.write("================================" + "<br/>")
        myWindow.document.write("*************PCPROG**************" + "<br />")
        myWindow.document.write("================================" + "<br />")

        myWindow.document.write("La Romana" + "<br />")
        myWindow.document.write("8095508455" + "<br />")
        myWindow.document.write("RNC: 110645041" + "<br />")
        myWindow.document.write("Recibo De Ingreso" + "<br />");
        myWindow.document.write(`${datos.TipoComprobante}` + "<br />");
        myWindow.document.write("------------------------------------------------" + "<br />");
        myWindow.document.write("Recibo No. 0000127" + "<br />");
        myWindow.document.write(`NCF: ${datos.NCF}` + "<br />");
        myWindow.document.write(`Prestamo: ${datos.NumeracionDocumento}` + "<br />");
        myWindow.document.write(`Cliente: ${datos.NombreCompletoCliente}` + "<br />");
        myWindow.document.write(`Garantia: ${datos.NumeracionGarantia}` + "<br />");
        myWindow.document.write(`Fecha: ${datos.Fecha}` + "<br />");
        myWindow.document.write("------------------------------------------------" + "<br />");
        myWindow.document.write(`CPIC Cargados: ${0.00}` + "<br />");
        myWindow.document.write("------------------------------------------------" + "<br />");
        myWindow.document.write(`INFORME DEL PAGO` + "<br />");
        myWindow.document.write(`CPIC Pagados: ${0.00}` + "<br />");
        myWindow.document.write(`BCES GENERALES AL MOMENTO` + "<br />");
        myWindow.document.write(`Bce: ${datos.BalanceDocumentoCXC}` + "<br />");
        myWindow.document.write(`Bce: ${datos.MontoPagado}` + "<br />");
        myWindow.document.write(`Bce: ${datos.BalanceDocumentoCXC - datos.MontoPagado}` + "<br />");
        myWindow.document.write(`OTROS PRESTAMOS ACTIVOS` + "<br />");
        myWindow.document.write(`Bce: ${datos.BalanceDocumentoCXC - datos.MontoAtrasado}` + "<br />");
        myWindow.document.write(`INFORMACION DE DEUDA ATRASADA` + "<br />");
        myWindow.document.write(`Pagares Atrasados: ${datos.CuotasAtrasadas}` + "<br />");
        myWindow.document.write(`Monto Atrasado: ${datos.MontoAtrasado}` + "<br />");
        myWindow.document.write(`Total Pagares/Pagar: ${Math.round(datos.MontoAtrasado / datos.CuotasAtrasadas)}` + "<br />");
        myWindow.document.write(`</html>`)
        myWindow.print();
        myWindow.close();
    } else {
        alert("No hay datos para imprimir");
    }
    // document.write('<button onclick="window.print()">Imprimir</button>')
}

function GenerarFichaDrCr(datos = null) {
    if (datos != null) {


        var myWindow = window.open('', '', 'width=800,height=600')
        myWindow.document.write(`<html lang="en">`)
        myWindow.document.write(`<style> html *
            {
                font-size: 13px!important;
                color: #000!important;
                font-family: System !important;
            }</style>`)
        myWindow.document.write("================================" + "<br/>")
        myWindow.document.write("*************PCPROG*************" + "<br />")
        myWindow.document.write("================================" + "<br />")

        myWindow.document.write("asdsad" + "<br />")
        myWindow.document.write("8095508455" + "<br />")
        myWindow.document.write("RNC110645041" + "<br />")
        myWindow.document.write("Recibo De Caja" + "<br />");
        myWindow.document.write("------------------------------------------------" + "<br />");
        myWindow.document.write("Recibo No. 0000127" + "<br />");
        myWindow.document.write(`Prestamo:${datos.NumeracionDocumento}` + "<br />");
        myWindow.document.write(`Cliente:${datos.NombreCompletoCliente}` + "<br />");
        myWindow.document.write(`Garantia:${datos.NumeracionGarantia}` + "<br />");
        myWindow.document.write(`Fecha:${datos.Fecha}` + "<br />");
        myWindow.document.write("------------------------------------------------" + "<br />");
        myWindow.document.write(`CPIC Cargados:${0.00}` + "<br />");
        myWindow.document.write("------------------------------------------------" + "<br />");
        myWindow.document.write(`INFORME DEL PAGO` + "<br />");
        myWindow.document.write(`CPIC Pagados:${0.00}` + "<br />");
        myWindow.document.write(`BCES GENERALES AL MOMENTO` + "<br />");
        myWindow.document.write(`Bce:${datos.BalanceDocumentoCXC}` + "<br />");
        myWindow.document.write(`Bce:${datos.MontoPagado}` + "<br />");
        myWindow.document.write(`Bce:${datos.BalanceDocumentoCXC - datos.MontoPagado}` + "<br />");
        myWindow.document.write(`OTROS PRESTAMOS ACTIVOS` + "<br />");
        myWindow.document.write(`Bce:${datos.BalanceDocumentoCXC - datos.MontoAtrasado}` + "<br />");
        myWindow.document.write(`INFORMACION DE DEUDA ATRASADA` + "<br />");
        myWindow.document.write(`Pagares Atrasados:${datos.CuotasAtrasadas}` + "<br />");
        myWindow.document.write(`Monto Atrasado:${datos.MontoAtrasado}` + "<br />");
        myWindow.document.write(`Total Pagares/Pagar:${Math.round(datos.MontoAtrasado / datos.CuotasAtrasadas)}` + "<br />");
        myWindow.document.write(`</html>`)
        myWindow.print();
        myWindow.close();
    } else {
        alert("No hay datos para imprimir");
    }
    return true;
    // document.write('<button onclick="window.print()">Imprimir</button>')
}
function printDiv(divName) {
    var printContents = document.getElementById(divName).innerHTML;
    var originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents;

    window.print();

    document.body.innerHTML = originalContents;
}
