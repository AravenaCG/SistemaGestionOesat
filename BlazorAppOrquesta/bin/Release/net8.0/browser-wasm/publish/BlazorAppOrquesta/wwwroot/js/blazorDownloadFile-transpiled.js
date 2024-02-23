

"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.BlazorDownloadFile = BlazorDownloadFile;
function BlazorDownloadFile(fileName, fileContent) {
  // Convertir el contenido del archivo a un blob
  var blob = new Blob([new Uint8Array(fileContent)], {
    type: 'text/csv'
  });

  // Crear un objeto URL para el blob
  var url = window.URL.createObjectURL(blob);

  // Crear un enlace de descarga
  var link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', fileName);

  // Agregar el enlace al documento y hacer clic en él para descargar el archivo
  document.body.appendChild(link);
  link.click();

  // Limpiar el enlace del documento
  document.body.removeChild(link);
  window.URL.revokeObjectURL(url);
}
;
