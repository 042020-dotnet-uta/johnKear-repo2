import { TestBed } from '@angular/core/testing';

import { ApiService, Login, Template, Register, FormTemplate, Sheet } from './api.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Observable } from 'rxjs';
import { SocialUser } from 'angularx-social-login';

describe('ApiService', () => {
  let service: ApiService;
  let userLogin: Login;
  let socialUser: Partial<SocialUser>;
  let register: Register;
  let template: Partial<Template>;
  let formTemplate: Partial<FormTemplate>;
  let sheet: Partial<Sheet>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]
    }).compileComponents();
    service = TestBed.inject(ApiService);
    userLogin = { username: 'name', password: 'password' };
    socialUser = { name: 'name', email: 'email@domain.com' };
    register = { email: 'email@domain.com', password: 'password', username: 'name' };
    formTemplate = { height: 11 };
    template = { formTemplates: [<FormTemplate>formTemplate] };
    sheet = { sheetId: 'sheetId' }
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('userLogin should return Observable<any>', () => {
    let object: Observable<any> = service.userLogin(userLogin);
    expect(object).toBeTruthy();
  });

  it('googleLogin should return Observable<any>', () => {
    let object: Observable<any> = service.googleLogin(<SocialUser>socialUser);
    expect(object).toBeTruthy();
  });

  it('register should return Observable<any>', () => {
    let object: Observable<any> = service.register(register);
    expect(object).toBeTruthy();
  });

  it('getTemplate should return Observable<any>', () => {
    let object: Observable<any> = service.getTemplate('templateID');
    expect(object).toBeTruthy();
  });

  it('getTemplates should return Observable<any>', () => {
    let object: Observable<any> = service.getTemplates();
    expect(object).toBeTruthy();
  });

  it('getTemplatesByUser should return Observable<any>', () => {
    let object: Observable<any> = service.getTemplatesByUser();
    expect(object).toBeTruthy();
  });

  it('postTemplate should return Observable<any>', () => {
    let object: Observable<any> = service.postTemplate(<Template>template);
    expect(object).toBeTruthy();
  });

  it('postSheet should return Observable<any>', () => {
    let object: Observable<any> = service.postSheet(<Sheet>sheet);
    expect(object).toBeTruthy();
  });

  it('getSheet should return Observable<any>', () => {
    let object: Observable<any> = service.getSheet('12345');
    expect(object).toBeTruthy();
  });

  it('petSheet should return Observable<any>', () => {
    let object: Observable<any> = service.putSheet(<Sheet>sheet);
    expect(object).toBeTruthy();
  });

});
