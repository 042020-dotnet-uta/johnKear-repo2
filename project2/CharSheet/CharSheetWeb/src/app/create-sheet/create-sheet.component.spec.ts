import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';

import { CreateSheetComponent } from './create-sheet.component';
import { ApiService, Template, Sheet, FormGroup, FormTemplate } from '../api.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { SavePdfService } from '../print-pdf.service';

describe('CreateSheetComponent', () => {
  let component: CreateSheetComponent;
  let fixture: ComponentFixture<CreateSheetComponent>;
  let fetchTemplateSpy: any;
  let loadTemplateSpy: any;
  let fetchSheetSpy: any;
  let templateStub: Partial<Template>;
  let formTemplateStub: Partial<FormTemplate>;
  let sheetStub: Sheet;
  let tempSpy: any;
  let formGroupStub: Partial<FormGroup>
  
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateSheetComponent],
      providers: [
        ApiService,
        SavePdfService
      ],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule
      ]
    })
      .compileComponents();
    formTemplateStub = { formInputs: ["123456"] , type: "inputs", labels: ["testLabel"]}
    templateStub = { name: 'testTemplate' , templateId: "12345", formTemplates: [<FormTemplate>formTemplateStub]};
    formGroupStub = { formTemplateId: "12345", formInputs: ["12345"], formTemplate: <FormTemplate>formTemplateStub };
    sheetStub = {name: 'testSheet', sheetId: '12345', formGroups: [<FormGroup>formGroupStub]};
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSheetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have textElements property', () => {
    expect(component.textElements).toBeTruthy();
  });

  it('should have titleElements property', () => {
    expect(component.titleElements).toBeTruthy();
  });

  it('should have undefined templateId property', () => {
    expect(component.templateId).toBeUndefined();
  });

  it('should have undefined sheetId property', () => {
    expect(component.sheetId).toBeUndefined();
  });

  it('should call fetchTemplate', () => {
    fetchTemplateSpy = spyOn(component, 'fetchTemplate');
    component.templateId = "12345";
    component.fetchTemplate();
    expect(fetchTemplateSpy).toHaveBeenCalled();
  });

  it('should call loadTemplate', () => {
    loadTemplateSpy = spyOn(component, 'loadTemplate');
    component.loadTemplate(<Template>templateStub);
    expect(loadTemplateSpy).toHaveBeenCalled();
  });

  it('loadTemplate should set properties', () => {
    component.loadTemplate(<Template>templateStub);
    expect(component.templateId).toBe("12345");
    expect(component.sheetId).toBeNull();
    expect(component.nameInput).toBeNull();
  });

  it('should call fetchSheet', () => {
    fetchSheetSpy = spyOn(component, 'fetchSheet');
    component.sheetId = "12345";
    component.fetchSheet();
    expect(fetchSheetSpy).toHaveBeenCalled();
  });

  it('should call loadSheet', () => {
    tempSpy = spyOn(component, 'loadSheet');
    component.loadSheet(<Sheet>sheetStub);
    expect(tempSpy).toHaveBeenCalled();
  });  

  it('loadSheet should set templateId to null', () => {
    component.loadSheet(<Sheet>sheetStub);
    expect(component.templateId).toBeNull();
    expect(component.sheetId).toBe("12345");
    expect(component.nameInput).toBe("testSheet");
  });

  it('toModel should return type Sheet', () => {
    tempSpy = spyOn(component, 'toModel');
    component.nameInput = "testNameInput";
    let result: any = component.toModel();
    expect(tempSpy).toHaveBeenCalled();
  });

  it('should call saveSheet', () => {
    tempSpy = spyOn(component, 'saveSheet');
    component.saveSheet();
    expect(tempSpy).toHaveBeenCalled();
  });

  it('saveSheet should', () => {
    tempSpy = spyOn(component, 'saveSheet');
    component.sheetId = '12345';
    component.saveSheet();
    expect(tempSpy).toHaveBeenCalled();
    component.sheetId = null;
    component.saveSheet();
    expect(tempSpy).toHaveBeenCalledTimes(2);
  });

  it('should call toPdf', () => {
    tempSpy = spyOn(component, 'toPdf');
    component.toPdf();
    expect(tempSpy).toHaveBeenCalled();
  });

  it('should call fetchTemplate if templateId is not null after view init', () => {
    component.templateId = "1234";
    tempSpy = spyOn(component, "fetchTemplate");
    component.ngAfterViewInit();
    expect(tempSpy).toHaveBeenCalled();
  });

  it('should call fetchSheet if templateId is null and sheetId is not null after view init', () => {
    component.sheetId = "1234";
    tempSpy = spyOn(component, "fetchSheet");
    component.ngAfterViewInit();
    expect(tempSpy).toHaveBeenCalled();
  });

  it('pushForm should be called', () => {
    tempSpy = spyOn(component, 'pushForm');
    component.pushForm(<FormTemplate>formTemplateStub);
    expect(tempSpy).toHaveBeenCalled();
  });

  it('pushForm inputsElements should push when formTemplate.type is "inputs"', () => {
    formTemplateStub.type = "inputs";
    component.pushForm(<FormTemplate>formTemplateStub);
    expect(component.inputsElements[0]).toEqual(<FormTemplate>formTemplateStub);
  });

  it('pushForm titleTextElements should push when formTemplate.type is "title-text"', () => {
    formTemplateStub.type = "title-text";
    component.pushForm(<FormTemplate>formTemplateStub);
    expect(component.titleTextElements[0]).toEqual(<FormTemplate>formTemplateStub);
  });

  it('pushForm textElements should push when formTemplate.type is "text"', () => {
    formTemplateStub.type = "text";
    component.pushForm(<FormTemplate>formTemplateStub);
    expect(component.textElements[0]).toEqual(<FormTemplate>formTemplateStub);
  });

  it('pushForm textElements should push when formTemplate.type is "text"', () => {
    formTemplateStub.type = "text";
    component.pushForm(<FormTemplate>formTemplateStub);
    expect(component.textElements[0]).toEqual(<FormTemplate>formTemplateStub);
  });

  it('pushForm titleElements should push when formTemplate.type is "title"', () => {
    formTemplateStub.type = "title";
    component.pushForm(<FormTemplate>formTemplateStub);
    expect(component.titleElements[0]).toEqual(<FormTemplate>formTemplateStub);
  });

});
