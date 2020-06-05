import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
//imports the DragDropModule that allows for "easier" drag and drop funcionality
// for divs.
//import { DragDropModule } from '@angular/cdk/drag-drop';
// imports teh resizing module that allows for "more simple" resizing of divs
//import { ResizeEvent } from 'angular-resizable-element';
import { ActivatedRoute} from '@angular/router';
import { ApiService, Template, FormTemplate } from '../api.service';
import { FormElementArrays, FormElement } from "../shared/form-types";

//ties component info to correlating temlate and css files.
var document;

@Component({
  selector: 'app-create-form',
  templateUrl: './create-form.component.html',
  styleUrls: ['./create-form.component.css']
})

//Angular class for declaring methods, kinda like controller from what I have seen, with the methods inside being like
//controller actions. 
export class CreateFormComponent implements OnInit, FormElementArrays {
  @ViewChild('formBoundary') formBoundary: ElementRef;

  width = 500;
  height = 100;
  type: string;
  nameInput: string;
  count: number;
  state:string;

  formTypes = [
    {
      value: "text-form", name: "Text Box",
    },
    {
      value: "title-form", name: "Title Box"
    },
    {
      value: "title-text-form", name: "Title and Text"
    },
    {
      value: "inputs-form", name: "Input Fields"
    }];

  inputsElements = [];
  titleTextElements = [];
  textElements = [];
  titleElements = [];

  constructor(private route: ActivatedRoute, private apiService: ApiService
  ) { }
  // create a method to be used in the HTML to  push a new dev of the predefined type
  // to the dom and to the page dynamically 
  createItem(): void {
    let newElement = { width: this.width, height: this.height } as any;
    switch (this.type) {
      case "inputs-form":
        newElement.inputs = [] as Array<FormElement>;
        newElement.inputs.length = this.count;
        this.inputsElements.push(newElement as FormElement);
        break;
      case "title-text-form":
        this.titleTextElements.push(newElement as FormElement);
        break;
      case "text-form":
        this.textElements.push(newElement as FormElement);
        break;
      case "title-form":
        this.titleElements.push(newElement as FormElement);
        break;
    }
  }

  /*
  createDragItem(): void {
    this.formElements.push({ height: 300, width: 200 } as FormElement);
  }

  createTitleItem(): void {
    this.titleElements.push({ width: this.width, height: this.height } as FormElement);
  }
  */

  name;
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.name = params['name'];
    });
  }

  convertToModel(): Template {
    let forms = Array.from(this.formBoundary.nativeElement.children) as Array<HTMLElement>;
    let template = {} as Template;
    template.name = this.nameInput;
    template.formTemplates = [];
    forms.forEach(form => {
      let formTemplate = {} as FormTemplate;

      let formArea = (form as HTMLElement).firstChild as HTMLElement;
      let classes = form.className as string;
      let style = window.getComputedStyle(form);

      // x and y position from transform.
      let matrix = new WebKitCSSMatrix(style.webkitTransform);
      formTemplate.x = matrix.m41;
      formTemplate.y = matrix.m42;

      formTemplate.width = parseInt(style.width, 10);
      formTemplate.height = parseInt(style.height, 10);

      formTemplate.labels = [];

      if (classes.includes("title-text-form")) {
        formTemplate.title = (formArea.firstChild as HTMLInputElement).value;
        formTemplate.type = "title-text"
      } else if (classes.includes("inputs-form")) {
        formTemplate.title = "inputs";
        formTemplate.type = "inputs";
        let inputContainer = form.querySelector(".input-container");
        (Array.from(inputContainer.children)).forEach(input => {
          let value = (input.firstChild.firstChild as HTMLInputElement).value;
          formTemplate.labels.push(value ? value : "");
        });
        formTemplate.title = (formArea.firstChild as HTMLInputElement).value;
      } else if (classes.includes("title-form")) {
        formTemplate.title = (formArea as HTMLInputElement).value;
        formTemplate.type = "title";
      } else if (classes.includes("text-form")) {
        formTemplate.title = "";
        formTemplate.type = "text";
      }
      template.formTemplates.push(formTemplate);
    });
    return template;
  }

  saveTemplate(): void {
    this.state = "Saving...";
    let template = this.convertToModel();
    this.apiService.postTemplate(template)
      .subscribe(response => {
        this.state = "Saved Template";
      }, error => {
        this.state = "Error Saving Template";
      });
  }
}
