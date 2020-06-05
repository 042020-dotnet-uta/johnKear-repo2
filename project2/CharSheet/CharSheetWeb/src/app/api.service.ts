import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SocialUser } from 'angularx-social-login';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})

export class ApiService {
  constructor(private httpClient: HttpClient, private cookieService: CookieService) { }

  private mainUrl = 'https://revatureprojectapi.azurewebsites.net/api/';
  // private mainUrl = 'http://localhost:5000/';

  public userLogin(login: Login): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.mainUrl + 'account/login', login, { headers, responseType: 'text', observe: 'response' });
  }

  public googleLogin(user: SocialUser): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.mainUrl + 'account/googlelogin', { username: user.name, email: user.email }, { headers, responseType: 'text', observe: 'response' });
  }

  public register(register: Register): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.mainUrl + 'account/register', register, { headers, observe: 'response' });
  }

  public getTemplate(templateId: string): Observable<any> {
    return this.httpClient.get(this.mainUrl + `templates/${templateId}`, { observe: 'response' })
  }

  public getTemplates(): Observable<any> {
    return this.httpClient.get(this.mainUrl + 'templates', { observe: 'response' });
  }

  public getTemplatesByUser(): Observable<any> {
      return this.httpClient.get(this.mainUrl + 'templates?user=true', { observe: 'response' });
  }

  public getSheet(sheetId: string): Observable<any> {
    return this.httpClient.get(this.mainUrl + `sheets/${sheetId}`, { observe: "response" });
  }

  public getSheets(): Observable<any> {
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.cookieService.get('access_token')}`)
      .set('Authenticationtoken', `Bearer ${this.cookieService.get('access_token')}`);
    return this.httpClient.get(this.mainUrl + 'sheets', { headers, observe: "response" });
  }

  public postTemplate(template: Template): Observable<any> {
    const headers = this.setRequestHeaders();
    return this.httpClient.post(this.mainUrl + 'templates', template, { headers, observe: 'response' })
  }

  public postSheet(sheet: Sheet): Observable<any> {
    const headers = this.setRequestHeaders();
    return this.httpClient.post(this.mainUrl + 'sheets', sheet, { headers, observe: 'response' });
  }

  public putSheet(sheet: Sheet): Observable<any> {
    const headers = this.setRequestHeaders();
    return this.httpClient.put(this.mainUrl + `sheets/${sheet.sheetId}`, sheet, { headers, observe: 'response' });
  }

  private setRequestHeaders(): HttpHeaders {
    return new HttpHeaders()
      .set('Content-type', 'application/json')
      .set('Authorization', `Bearer ${this.cookieService.get('access_token')}`)
      .set('Authenticationtoken', `Bearer ${this.cookieService.get('access_token')}`)
      .set('X-Requested-With', 'XMLHttpRequest');
  }
}

export interface Login {
  username: string;
  password: string;
}

export interface Register extends Login {
  email: string;
}

export interface Template {
  templateId: string;
  name: string;
  formTemplates: FormTemplate[];
}

export interface FormTemplate {
  formTemplateId: string;
  type: string;
  title: string;
  x: number;
  y: number;
  height: number;
  width: number;
  labels: string[];
  formInputs: string[] | null;
  inputs: Input[] | null;
}

interface Input {
  label: string;
  input: string;
}

export interface Sheet {
  sheetId: string;
  name: string;
  formGroups: FormGroup[];
}
export interface FormGroup {
  formTemplateId: string;
  formTemplate: FormTemplate | null;
  formInputs: string[];
}
