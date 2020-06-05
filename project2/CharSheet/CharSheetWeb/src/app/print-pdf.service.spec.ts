import { TestBed } from '@angular/core/testing';

import { SavePdfService } from './print-pdf.service';

describe('PrintPdfService', () => {
  let service: SavePdfService;
  let pdfSpy: any;
  let htmlEl: Partial<HTMLElement>;
  let parentEl: Partial<HTMLElement>;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SavePdfService);
    parentEl = { id: 'form-boundary' };
    htmlEl = { parentElement: <HTMLElement>parentEl };
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call captureScreen', () => {
    pdfSpy = spyOn(service, 'captureScreen');
    service.captureScreen(<HTMLElement>htmlEl);
    expect(pdfSpy).toHaveBeenCalled();
  });

});
