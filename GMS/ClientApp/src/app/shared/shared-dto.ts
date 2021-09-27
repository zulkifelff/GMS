import {ServiceCallService} from "./service-call.service";

export class UploadMetaDataDTO {
  count?: number;
  size?: number;
  fileUpload?: boolean;
  filePath?: string;
}

export class FormValueDTO {
  EmployeeNumber?: string | number;
  Password?: string;
}

export class LoginHttpResponse {
  Id?: number;
  EmployeeNumber?: string;
  Password?: string;
  Token?: string;
}

export class PackageDetailDataDTO {
  PackageId?: number;
  Title?: string;
  Description?: string;
  Amount?: number;
  DurationDays?: number;
  CreatedOn?: Date;
  CreatedBy?: number;
  UpdatedOn?: Date;
  UpdatedBy?: number;
  IsActive?: boolean;
  Invoices?: any[];
  TotalCount?: string | number;

  constructor() {
    this.PackageId = null;
    this.Amount = null;
    this.CreatedBy = null;
    this.DurationDays = null;
    this.Title = null;
    this.Description = null;
  }
}


export class MembersDetailDataDTO {
  MemberId?: number;
  MembershipNumber?: string;
  Name?: string;
  DOB?: Date;
  CNIC?: string;
  Nationality?: string;
  Gender?: string;
  Mobile?: string;
  Address?: string;
  ProfilePicture?: string;
  Email?: string;
  Package?: string;
  RegistrationDate?: Date;
}

export class EmployeesDetailDataDTO {
  EmployeeId?: number;
  Name?: string;
  EmployeeCode?: string;
  FatherName?: string;
  Nationality?: string;
  Gender?: string;
  Mobile?: string;
  Phone?: string;
  Email?: string;
  DOB?: Date;
  DateOfEmployment?: Date;
  EmployeeNumber?: string;
  CardNumber?: string;
  Password?: null | string;
  UserType?: string;
  CNIC?: string;
  Address?: string;
  Department?: string;
  Designation?: string;
  ProfilePicture?: string;
}


export enum EntityStatus {
  View = 1,
  Edit = 2,
  Creation = 3


}
