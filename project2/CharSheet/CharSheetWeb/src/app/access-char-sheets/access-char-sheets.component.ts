import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute} from '@angular/router';
import { ApiService, Template, Sheet } from '../api.service';




@Component({
  selector: 'app-access-char-sheets',
  templateUrl: './access-char-sheets.component.html',
  styleUrls: ['./access-char-sheets.component.css']
})
export class AccessCharSheetsComponent implements OnInit {
  constructor(private http: HttpClient, private route: ActivatedRoute,
    private apiService: ApiService, private router: Router) { }
  selectedTemplate = null;
  selectedSheet = null;

  getSelectedTemplate(template: Template) {
    this.selectedTemplate = template;
  }

  getSelectedSheet(sheet: Sheet) {
    this.selectedSheet = sheet;
  }

  templatesData: Template[];
  sheetsData: Sheet[];
  ngOnInit(): void {
    this.apiService.getTemplates().subscribe(response => {
      console.log(response);
      this.templatesData = response.body as Template[];
    });
    this.apiService.getSheets().subscribe(response => {
      console.log(response);
      this.sheetsData = response.body as Sheet[];
    });
  }

  
}





