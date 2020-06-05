import { TestBed } from '@angular/core/testing';

import { GenerateFormService } from './generate-form.service';
import { FormTemplate } from './api.service';

describe('GenerateFormService', () => {
  let service: GenerateFormService;
  let form: Partial<FormTemplate>;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GenerateFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('getNewFormTemplate should return a FormTemplate', () => {
    form = service.getNewFormTemplate();
    expect(form).toBeTruthy();
  });

  it('getNewFormTemplate should return a FormTemplate with no title', () => {
    form = service.getNewFormTemplate();
    expect(form.title).toBe('');
  });
});
