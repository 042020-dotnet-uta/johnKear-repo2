import { async, ComponentFixture, TestBed, inject, getTestBed } from '@angular/core/testing';
import { AuthService, GoogleLoginProvider } from "angularx-social-login";
import { LoginComponent } from './login.component';
import { SocialUser } from "angularx-social-login";
import { Observable } from 'rxjs';
import { DebugElement } from '@angular/core';
import { ApiService } from '../api.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let de: DebugElement;
  let injector: TestBed;
  let googleStub: Partial<GoogleLoginProvider>;
  //let googleStub: GoogleLoginProvider;
  let authMock: any;
  let userStub: Observable<SocialUser>;
  let states: Observable<string[]>;
  let apiStub: any;

  beforeEach(async(() => {
    apiStub = { apiService: 'anyservice' }
    authMock = jasmine.createSpyObj('AuthService', ['authState'])
    TestBed.configureTestingModule({
      declarations: [LoginComponent],
      providers: [
        ApiService,
        { provide: GoogleLoginProvider, useValue: googleStub },
        { provide: SocialUser, useValue: userStub },
        { provide: AuthService, useValue: authMock },
      ],
      imports: [
        HttpClientTestingModule,
      ]
      
    })
      .compileComponents();
    injector = getTestBed();
    //authStub = injector.get(AuthService);
    //googleStub = injector.get(GoogleLoginProvider);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    de = fixture.debugElement;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy()
  });

  xit('should have default logged in of false', () => {
    let spy: any = spyOn(component, 'signOut');
    component.signOut();
    expect(spy).toHaveBeenCalled();
  });

 
});
