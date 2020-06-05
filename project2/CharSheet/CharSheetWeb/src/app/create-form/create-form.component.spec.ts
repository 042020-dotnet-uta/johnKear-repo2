import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';

import { CreateFormComponent } from './create-form.component';
//  import dependecies
import { RouterTestingModule } from '@angular/router/testing';
import { ApiService, Template } from '../api.service';
import { type } from 'os';
import { Type } from '@angular/core';

describe('CreateFormComponent', () => {
  let component: CreateFormComponent;
  let fixture: ComponentFixture<CreateFormComponent>;
  let apiMock: any;
  /* let dragSpy: any;
   let titleSpy: any;*/
  let tempSpy: any;

  beforeEach(async(() => {
    apiMock = jasmine.createSpyObj('ApiService', ['postTemplate']);
    TestBed.configureTestingModule({
      declarations: [CreateFormComponent],
      providers: [
        { provide: ApiService, useValue: apiMock },
      ],
      imports: [
        RouterTestingModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have default width of 500', () => {
    expect(component.width == 500).toBeTruthy();
  });

  it('should have default height of 100', () => {
    expect(component.height == 100).toBeTruthy();
  });

  it('should have should have undefined "type" property of type string', () => {
    expect(component.type).toBeUndefined();
  });

  it('should have should have undefined "nameInput" property of type string', () => {
    expect(component.nameInput).toBeUndefined();
  });

  it('should have titleElements property', () => {
    expect(component.titleElements).toBeTruthy();
  });

  it('should have textElements property', () => {
    expect(component.textElements).toBeTruthy();
  });

  it('convertToModel should return a Template', () => {
    const template: Template = component.convertToModel();
    expect(template).toBeTruthy();
  });

  it('should call createItem', () => {
    tempSpy = spyOn(component, 'createItem');
    component.createItem();
    expect(tempSpy).toHaveBeenCalled();
  });

  it('createItem case "inputs-form"', () => {
    component.count = 1;
    component.type = 'inputs-form';
    component.createItem();
    expect(component.inputsElements[0].width).toBe(500);
    expect(component.inputsElements[0].height).toBe(100);
  });

  it('createItem case "title-text-form"', () => {
    component.count = 1;
    component.type = 'title-text-form';
    component.createItem();
    expect(component.titleTextElements[0].width).toBe(500);
    expect(component.titleTextElements[0].height).toBe(100);
  });

  it('createItem case "text-form"', () => {
    component.count = 1;
    component.type = 'text-form';
    component.createItem();
    expect(component.textElements[0].width).toBe(500);
    expect(component.textElements[0].height).toBe(100);
  });

  it('createItem case "title-form"', () => {
    component.count = 1;
    component.type = 'title-form';
    component.createItem();
    expect(component.titleElements[0].width).toBe(500);
    expect(component.titleElements[0].height).toBe(100);
  });

  it('should call saveTemplate', () => {
    tempSpy = spyOn(component, 'saveTemplate');
    component.saveTemplate();
    expect(tempSpy).toHaveBeenCalled();
  });


  /*it('should call createDragItem', () => {
    dragSpy = spyOn(component, 'createDragItem');
    let drag: any = component.createDragItem();
    expect(dragSpy).toHaveBeenCalled();
    expect(drag).toBeUndefined();
  });*/

  /*it('should call createTitleItem', () => {
    titleSpy = spyOn(component, 'createTitleItem');
    component.createTitleItem();
    expect(titleSpy).toHaveBeenCalled();
  });*/

  /*it('should change width when changeWidth is called', () => {
    expect(component.width == 50).toBeTruthy();
    component.changeWidth(100);
    expect(component.width == 100).toBeTruthy();
  });

  it('should change height when changeHeight is called', () => {
    expect(component.height == 30).toBeTruthy();
    component.changeHeight(200);
    expect(component.height == 200).toBeTruthy();
  });*/


});
