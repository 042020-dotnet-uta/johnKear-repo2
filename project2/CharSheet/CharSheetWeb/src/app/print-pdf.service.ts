import { Injectable } from '@angular/core';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';


@Injectable({
  providedIn: 'root'
})
export class SavePdfService {

  constructor() { }

  // Method to capture form and convert to PDF
  public captureScreen(data: HTMLElement) {
    let div = document.getElementById('form-boundary');
    html2canvas(div, { scrollX: -window.scrollX, scrollY: -window.scrollY }).then(canvas => {
      var pdf = new jspdf('p', 'cm', 'a4');
      var imgData = canvas.toDataURL("image/png");
      pdf.addImage(imgData, 'PNG', 0, 0, 21.0, 29.7);
      pdf.save('pdfSheet.pdf');
    });
  }
}
