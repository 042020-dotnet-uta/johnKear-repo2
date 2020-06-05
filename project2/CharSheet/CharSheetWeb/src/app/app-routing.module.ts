import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateFormComponent } from './create-form/create-form.component';
import { AccessCharSheetsComponent } from './access-char-sheets/access-char-sheets.component';
import { SelectedTemplateComponent } from './selected-template/selected-template.component';
import { SelectedSheetComponent } from './selected-sheet/selected-sheet.component';


const routes: Routes = [
  { path: 'template/:p1', component: SelectedTemplateComponent },
  { path: 'sheet/:p2', component: SelectedSheetComponent },
  { path: 'createForm', component: CreateFormComponent },
  { path: 'accessForm', component: AccessCharSheetsComponent },
  { path: '', redirectTo: 'accessForm', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
