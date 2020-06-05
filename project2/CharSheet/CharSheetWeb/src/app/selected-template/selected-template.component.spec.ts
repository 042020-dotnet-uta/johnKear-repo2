import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedTemplateComponent } from './selected-template.component';
import { ApiService, Template, FormTemplate } from '../api.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';

describe('SelectedTemplateComponent', () => {
  let component: SelectedTemplateComponent;
  let fixture: ComponentFixture<SelectedTemplateComponent>;
  let apiMock: any;
  let routeMock: any;

  beforeEach(async(() => {
    apiMock = jasmine.createSpyObj('ApiService', ['postTemplate']);
    TestBed.configureTestingModule({
      declarations: [SelectedTemplateComponent],
      providers: [
        { provide: ApiService, useValue: apiMock },
        {provide: ActivatedRoute, useValue: routeMock},
      ],
      imports: [
        RouterTestingModule,
      ]
      
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectedTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
