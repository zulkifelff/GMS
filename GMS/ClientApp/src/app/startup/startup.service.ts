import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {PlatformLocation} from "@angular/common";
import {environment} from "../../environments/environment";
import {FormValueDTO, MembersDetailDataDTO} from "../shared/shared-dto";
import {Observable} from "rxjs";
import {ServiceCallService} from "../shared/service-call.service";
import { of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class StartupService {
  public API_URL: string;
  public localBaseURL: string;
  private HTTP_HEADERS: HttpHeaders;

  OPTIONS: { headers: HttpHeaders };

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, platformLocation: PlatformLocation, private SharedService: ServiceCallService) {

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
      'Authorization':this.SharedService.FetchLocalStorage('AuthToken')?this.SharedService.FetchLocalStorage('AuthToken'):"Basic emlrbzE6emlrbzE=",
      'Access-Control-Allow-Origin': '*',
    });

    this.OPTIONS = {
      headers: this.HTTP_HEADERS
    };

  }


  GetMembersComplete() {
    const url = `${this.API_URL}api/Member/GetMembersDetailData`;
    return this.http.get<any>(url, this.OPTIONS);
  }

  DeleteIndividualMember(MemberId: number) {
    let PassingObject = {
      "UpdatedBy": this.SharedService.FetchLocalStorage('UserId'),
      "MemberId": MemberId
    }
    const url = `${this.API_URL}/api/Member/DeleteMember`;
    return this.http.post<any>(url, JSON.stringify(PassingObject), this.OPTIONS);

  }

  UpdateMember(FormValueData: any) {

    const url = `${this.API_URL}/api/Member/DeleteMember`;
    return this.http.post<any>(url,JSON.stringify(FormValueData), this.OPTIONS);
  }

  GetPackages() {
    const url = `${this.API_URL}api/Package/GetPackages`;
    return this.http.get<any>(url, this.OPTIONS);
  }

  DeletePackage(PackageId: number) {
    let PassingObject = {
      "UpdatedBy": this.SharedService.FetchLocalStorage('UserId'),
      "PackageId": PackageId
    }
    const url = `${this.API_URL}/api/Package/DeletePackage`;
    return this.http.post<any>(url, JSON.stringify(PassingObject), this.OPTIONS);

  }

  GetPackageById(PackageId: number) {
    const url = `${this.API_URL}api/Package/GetPackageById?packageId=${PackageId}`;
    return this.http.get<any>(url, this.OPTIONS);
  }

  UpdatePackage(PackageForm: any) {
    const url = `${this.API_URL}/api/Package/UpdatePackage`;
    return this.http.post<any>(url,JSON.stringify(PackageForm), this.OPTIONS);
  }

  InsertPackage(PackageForm: any) {
    const url = `${this.API_URL}/api/Package/InsertPackage`;
    return this.http.post<any>(url,JSON.stringify(PackageForm), this.OPTIONS);
  }
}
