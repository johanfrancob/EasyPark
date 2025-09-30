window.ticketPdf = async function (elementId, fileName) {
    const { jsPDF } = window.jspdf;

    const element = document.getElementById(elementId);
    if (!element) {
        alert("?? No se encontró el contenido en el DOM");
        return;
    }

    try {
        // Renderizar el div en un canvas con alta resolución
        const canvas = await html2canvas(element, {
            scale: 2,        // ?? aumenta la calidad (prueba con 3 si necesitas más)
            useCORS: true,   // permite cargar imágenes externas (logo, etc.)
            logging: false   // menos ruido en consola
        });

        // Convertir el canvas a imagen PNG con máxima calidad
        const imgData = canvas.toDataURL("image/png", 1.0);

        // Crear el PDF en tamaño A4
        const pdf = new jsPDF("p", "mm", "a4");

        // Ajustar la imagen al ancho del PDF manteniendo proporciones
        const pdfWidth = pdf.internal.pageSize.getWidth();
        const pdfHeight = (canvas.height * pdfWidth) / canvas.width;

        pdf.addImage(imgData, "PNG", 0, 0, pdfWidth, pdfHeight, "", "FAST");

        // Descargar con el nombre indicado o uno por defecto
        pdf.save(fileName || "documento.pdf");
    } catch (error) {
        console.error("? Error generando PDF:", error);
        alert("No se pudo generar el PDF. Revisa la consola.");
    }
};
