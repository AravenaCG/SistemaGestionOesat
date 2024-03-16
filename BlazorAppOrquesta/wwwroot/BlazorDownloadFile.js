export function BlazorDownloadFile(fileName, fileContent) {
    // Convertir el contenido del archivo a un blob
    const blob = new Blob([new Uint8Array(fileContent)], { type: 'text/csv' });

    // Crear un objeto URL para el blob
    const url = window.URL.createObjectURL(blob);

    // Crear un enlace de descarga
    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', fileName);

    // Agregar el enlace al documento y hacer clic en él para descargar el archivo
    document.body.appendChild(link);
    link.click();

    // Limpiar el enlace del documento
    document.body.removeChild(link);
    window.URL.revokeObjectURL(url);

};