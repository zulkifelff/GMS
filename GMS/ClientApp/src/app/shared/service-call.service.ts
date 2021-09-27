import {Inject, Injectable} from '@angular/core';
import {Observable, Subject} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {PlatformLocation} from '@angular/common';
import {FormValueDTO} from "./shared-dto";
import {environment} from "../../environments/environment";



@Injectable({
  providedIn: 'root'
})
export class ServiceCallService {
  public API_URL: string;
  public localBaseURL: string;
  private HTTP_HEADERS: HttpHeaders;

  OPTIONS: { headers: HttpHeaders };

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, platformLocation: PlatformLocation) {

    console.log((platformLocation as any).location);
    console.log((platformLocation as any).location.href);
    console.log((platformLocation as any).location.origin);

    this.localBaseURL = baseUrl;
    console.info(this.localBaseURL);
    this.API_URL = environment.API_URL;
    this.HTTP_HEADERS = new HttpHeaders({
      'Content-Type': 'application/json',
      'X-Requested-With': 'XMLHttpRequest',
      'Accept': '*/*',
      'Access-Control-Allow-Origin' :'*',
    });

    this.OPTIONS = {
      headers: this.HTTP_HEADERS
    };

  }

  private toggleNavigationSubject = new Subject<any>();

  sendToggleNavigationMessage(incomingValue: any) {
    this.toggleNavigationSubject.next(incomingValue);
  }

  clearToggleNavigationMessages() {
    this.toggleNavigationSubject.next();
  }

  getToggleNavigationMessage(): Observable<any> {
    return this.toggleNavigationSubject.asObservable();
  }

  postFile(fileToUpload: File) {
    const files: FormData = new FormData();
    files.append('files', fileToUpload);
    const destinationURL = this.localBaseURL + 'api/' + 'ContentUpload';
    return this.http.post(destinationURL, files);
  }


  GetLoginToken(LoginModel: FormValueDTO) {
    debugger;
    const url = `${this.API_URL}api/Account/Login`;
    return this.http.post<any>(url, JSON.stringify(LoginModel),this.OPTIONS);
  }

  SetupLocalStorage(CookieString: string, CookieValue: string) {
    sessionStorage.setItem(CookieString, CookieValue);
  }

  RemoveLocalStorage(CookieString:string)
  {
    sessionStorage.removeItem(CookieString);
  }

  FetchLocalStorage(CookieString:string)
  {
    return sessionStorage.getItem(CookieString);
  }

  ClearLocalStorage()
  {
    sessionStorage.clear();
  }
}
