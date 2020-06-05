import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessCharSheetsComponent } from './access-char-sheets.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ApiService, Template, Sheet, FormTemplate } from '../api.service';

describe('AccessCharSheetsComponent', () => {
  let component: AccessCharSheetsComponent;
  let fixture: ComponentFixture<AccessCharSheetsComponent>;
  let templateStub: Partial<Template>;
  let sheetStub: Partial<Sheet>;
  let tempSpy: any;
  let formTemplateStub: Partial<FormTemplate>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AccessCharSheetsComponent,
      ],
      providers: [
        ApiService,
      ],
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ]
    })
      .compileComponents();
    formTemplateStub = { title: "formTemplate" }
    templateStub = { name: "testTemplate", templateId: "12345", formTemplates: [<FormTemplate>formTemplateStub]};
    sheetStub = { name: "testSheet" };
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessCharSheetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have default null property selectedTemplate', () => {
    expect(component.selectedTemplate).toBeNull();
  });

  it('should have default null property selectedSheet', () => {
    expect(component.selectedSheet).toBeNull();
  });

  it('should have default undefined templatesData property', () => {
    expect(component.templatesData).toBeUndefined();
  });

  it('should have default undefined sheetsData property', () => {
    expect(component.sheetsData).toBeUndefined();
  });

  it('getSelectedTemplate should set selectedTemplate', () => {
    tempSpy = spyOn(component, "getSelectedTemplate");
    component.getSelectedTemplate(<Template>templateStub);
    expect(tempSpy).toHaveBeenCalled();
    expect(component.selectedTemplate).toBeNull();
  });

  it('getSelectedSheet should set selectedSheet', () => {
    tempSpy = spyOn(component, "getSelectedSheet");
    component.getSelectedSheet(<Sheet>sheetStub);
    expect(tempSpy).toHaveBeenCalled();
    expect(component.selectedSheet).toBeNull();
  });


});
