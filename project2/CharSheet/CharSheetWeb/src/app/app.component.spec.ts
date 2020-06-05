import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { DebugElement } from '@angular/core';
import { AuthService, GoogleLoginProvider } from "angularx-social-login";
import { By } from '@angular/platform-browser';
import { applySourceSpanToExpressionIfNeeded } from '@angular/compiler/src/output/output_ast';
import { ApiService } from './api.service';

describe('AppComponet', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let de: DebugElement;
  let googleStub: Partial<GoogleLoginProvider>;
  let authStub: any;
  let signInSpy: any;
  let signOutSpy: any;
  let apiStub: any;

  beforeEach(async(() => {
    authStub = { AuthService: 'anyAuth'};
    googleStub = { };
    apiStub = {apiService: 'anyservice'}
    TestBed.configureTestingModule({
      declarations: [AppComponent],
      providers: [{ provide: GoogleLoginProvider, useValue: googleStub },
        { provide: AuthService, useValue: authStub },
        { provide: ApiService, useValue: apiStub }],
      
    }).compileComponents(); //compiles html template and css

  }));

  //  setup of variables used for testing
  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    de = fixture.debugElement;
    signOutSpy = spyOn(component, 'signOut');
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // check title of app button
  it('should have title: Character Sheet Creator', () => {
    expect(component.title).toContain('Character Sheet Creator');
  });

  it('signIn should be called', () => {
    signInSpy = spyOn(component, 'singIn');
    component.singIn('google');
    expect(signInSpy).toHaveBeenCalled();
  });

  it('should have undefined "user" property', () => {
    expect(component.user).toBeUndefined();
  });

  it('signOut should return void', () => {
    let returnValue: any = component.signOut();
    expect(returnValue).toBeFalsy();
  });

  it('signOut should reset properties', () => {
    component.signOut();
    expect(component.user).toBeFalsy();
    expect(component.isLoggedIn).toBeFalsy();
  });

  it('should have button', () => {
    const linkDes = de.queryAll(
      By.css('button')
    );
    expect(linkDes.length>=1).toBeTruthy();
  });

  it('should have create form link', () => {
    const linkDes = de.queryAll(
      By.css('li')
    );
    const createForm: HTMLDListElement = linkDes[0].nativeElement;
    expect(createForm.textContent).toContain('Create Form');
  });

  it('should have access form link', () => {
    const linkDes = de.queryAll(
      By.css('li')
    );
    const createForm: HTMLDListElement = linkDes[1].nativeElement;
    expect(createForm.textContent).toContain('Access Form');
  });

  it('should have Sign in with Google link', () => {
    const linkDes = de.queryAll(
      By.css('li')
    );
    const createForm: HTMLLinkElement = linkDes[2].nativeElement;
    expect(createForm.textContent).toContain('Sign in with Google');
  });

  it('signIn should be called when "Sign in with Google" is clicked', () => {
    const linkDes = de.queryAll(
      By.css('a')
    );
    let signInLink: HTMLLinkElement = linkDes[3].nativeElement;
    signInLink.click();
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(signInSpy).toHaveBeenCalled();
    });
  });

  it('should contain one header', () => {
    const linkDes = de.queryAll(
      By.css('header')
    );
    expect(linkDes.length==1).toBeTruthy();
  });

});

