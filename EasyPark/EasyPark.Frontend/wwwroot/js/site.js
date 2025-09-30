window.printTicket = () => {
    let ticket = document.getElementById("ticket");
    if (!ticket) return;

    let w = window.open("", "_blank", "width=400,height=600");
    w.document.write("<html><head><title>Ticket</title></head><body>");
    w.document.write(ticket.innerHTML);
    w.document.write("</body></html>");
    w.document.close();
    w.print();
};
window.printFactura = () => {
    let factura = document.getElementById("facturaParaImprimir");
    if (!factura) {
        console.error("No se encontró el elemento de la factura para imprimir.");
        return;
    }

    let w = window.open("", "_blank", "width=500,height=700");
    w.document.write('<html><head><title>Factura EasyPark</title></head><body>');
    w.document.write(factura.innerHTML);
    w.document.write('</body></html>');
    w.document.close();
    w.print();
};
