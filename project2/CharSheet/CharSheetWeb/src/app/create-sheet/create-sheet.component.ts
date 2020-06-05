import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { ApiService, Template, Sheet, FormTemplate, FormGroup } from '../api.service';
import { FormElementArrays } from '../shared/form-types'
import { ActivatedRoute } from '@angular/router';
import { SavePdfService } from '../print-pdf.service';

@Component({
  selector: 'app-create-sheet',
  templateUrl: './create-sheet.component.html',
  styleUrls: ['./create-sheet.component.css']
})
export class CreateSheetComponent implements OnInit, AfterViewInit, FormElementArrays {
  @ViewChild('formBoundary') formBoundary: ElementRef;

  inputsElements = [];
  titleTextElements = [];
  textElements = [];
  titleElements = [];

  templateId: string;
  sheetId: string;
  nameInput: string;
  state: string;

  constructor(private apiService: ApiService, private activatedRoute: ActivatedRoute, private savedPdfService: SavePdfService) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(paramsId => {
      this.templateId = paramsId.p1;
      this.sheetId = paramsId.p2;
    });
  }

  ngAfterViewInit(): void {
    if (this.templateId != null)
      this.fetchTemplate();
    else if (this.sheetId != null)
      this.fetchSheet();
  }

  loadTemplate(template: Template): void {
    this.sheetId = null;
    this.templateId = template.templateId;
    this.nameInput = null;

    this.inputsElements.splice(0, this.inputsElements.length);
    this.textElements.splice(0, this.textElements.length);
    this.titleElements.splice(0, this.titleElements.length);
    this.titleTextElements.splice(0, this.titleTextElements.length);

    let formTemplates = template.formTemplates;

    formTemplates.forEach(formTemplate => {
      if (formTemplate.type === "inputs") {
        formTemplate.inputs = [];
        formTemplate.labels.forEach(label => {
          formTemplate.inputs.push({ label: label, input: null });
        });
      }
      this.pushForm(formTemplate);
    });
  }

  loadSheet(sheet: Sheet): void {
    this.templateId = null;
    this.sheetId = sheet.sheetId;
    this.nameInput = sheet.name;

    this.inputsElements.splice(0, this.inputsElements.length);
    this.textElements.splice(0, this.textElements.length);
    this.titleElements.splice(0, this.titleElements.length);
    this.titleTextElements.splice(0, this.titleTextElements.length);

    let formGroups = sheet.formGroups;

    formGroups.forEach(formGroup => {
      formGroup.formTemplate.formInputs = formGroup.formInputs;

      if (formGroup.formTemplate.type === "inputs") {
        formGroup.formTemplate.inputs = [];
        formGroup.formTemplate.labels.forEach((label, index) => {
          let input = formGroup.formInputs[index] ? formGroup.formInputs[index] : "";
          formGroup.formTemplate.inputs.push({ label: label, input: input });
        });
      }

      this.pushForm(formGroup.formTemplate);
    });
  }

  pushForm(formTemplate: FormTemplate) {
    switch (formTemplate.type) {
      case "inputs":
        this.inputsElements.push(formTemplate);
        break;
      case "title-text":
        this.titleTextElements.push(formTemplate);
        break;
      case "text":
        this.textElements.push(formTemplate);
        break;
      case "title":
        this.titleElements.push(formTemplate);
        break;
    }
  }

  fetchSheet(): void {
    this.apiService.getSheet(this.sheetId)
      .subscribe(response => {
        if (response.status == 200) {
          console.log(response.body);
          this.loadSheet(response.body as Sheet);
        } else {
          this.state = "Error Loading";
        }
      })
  }

  fetchTemplate(): void {

    this.apiService.getTemplate(this.templateId)
      .subscribe(response => {
        if (response.status == 200) {
          console.log(response);
          this.loadTemplate(response.body as Template);
        } else {
          this.state = "Error Loading";
        }
      });
  }

  toModel(): Sheet {
    let sheet = {} as Sheet;
    sheet.name = this.nameInput
    let forms = Array.from(this.formBoundary.nativeElement.children) as Array<HTMLElement>;
    sheet.formGroups = [];
    forms.forEach(form => {
      let formGroup = {} as FormGroup;
      formGroup.formTemplateId = form.id;
      formGroup.formInputs = [] as string[];

      let classes = form.className as string;
      if (classes.includes("inputs-form")) {
        let inputForms = Array.from(form.querySelector('.input-container').children);
        inputForms.forEach(inputForm => {
          formGroup.formInputs.push((inputForm.querySelector('input') as HTMLInputElement).value);
        });
      } else if (classes.includes("title-text-form")) {
        let input = ((form.firstChild as HTMLElement).querySelector(':nth-child(2)') as HTMLInputElement).value;
        formGroup.formInputs.push(input);
      } else if (classes.includes("text-form")) {
        let input = (form.firstChild as HTMLInputElement).value;
        formGroup.formInputs.push(input);
      }
      sheet.formGroups.push(formGroup);
    });
    return sheet;
  }

  saveSheet(): void {
    this.state = "Saving Sheet...";
    let sheet = this.toModel();
    if (this.sheetId == null) {
      this.apiService.postSheet(sheet)
        .subscribe(response => {
          console.log(response);
          if (response.status == 200 || response.status == 201) {
            this.sheetId = response.body.sheetId;
            this.nameInput = response.body.name;
            this.templateId = null;
          } else {
            this.state = "Error Saving"
          }
        }, error => this.state = "Error Saving");
    } else {
      sheet.sheetId = this.sheetId;
      this.apiService.putSheet(sheet)
        .subscribe(response => {
          console.log(response);
          if (response.status == 200 || response.status == 201) {
            this.state = null;
          } else {
            this.state = "Error Saving";
          }
        },
          error =>
            this.state = "Error Saving"
        );
    }
  }
  toPdf(): void {
    this.savedPdfService.captureScreen(this.formBoundary.nativeElement);
  }
}
