import {ApplicationRef, Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {takeUntil} from "rxjs/operators";
import {EntityStatus, LoginHttpResponse, MembersDetailDataDTO} from "../../shared/shared-dto";
import {Observable, Subject, throwError} from "rxjs";
import {StartupService} from "../startup.service";
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';
import {ServiceCallService} from "../../shared/service-call.service";
import {MatDrawer} from "@angular/material/sidenav";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";


@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  passingHeading = 'Members';
  private ngUnsubscribe: Subject<any> = new Subject();
  public AllMembersData: MembersDetailDataDTO[];

  horizontalPosition: MatSnackBarHorizontalPosition = 'left';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  EntityStatusEnum: typeof EntityStatus = EntityStatus;

  // @ViewChild('EditUserForm', {static: true}) public EditUserForm: MatDrawer;
  @ViewChild('EditUserForm') ActivityIntent: TemplateRef<any>;

  public EditMemberForm: FormGroup;
  GenderList: any[] = [{'Key': 'Male', 'Value': 'Male'}, {'Key': 'Female', 'Value': 'Female'}, {
    'Key': 'Other',
    'Value': 'Other'
  }];
  public currentActionEvent: Number;
  public modalRef: BsModalRef;

  constructor(private modalService: BsModalService,private router:Router , private fb: FormBuilder, private applicationRef: ApplicationRef, private _StartupService: StartupService, private _Snackbar: MatSnackBar, private _SharedServiceCall: ServiceCallService) {

    this.GetMembersComplete();

  }

  ngOnInit(): void {

  }

  GetMembersComplete() {
    this._StartupService.GetMembersComplete()
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: MembersDetailDataDTO[]) => {
        if (incomingResponse) {
          this.AllMembersData = incomingResponse;
        }

      },
      (err) => {

        console.log('Error while getting Lease List', err);
        return throwError(
          'Something bad happened; please try again later.');
      }
    );

  }

  DeleteUser(ItemIterate: MembersDetailDataDTO) {

    this._StartupService.DeleteIndividualMember(ItemIterate.MemberId)
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: any) => {
        let Snackbar_Message: string = "User " + ItemIterate.Name + " has been successfully deleted.";

        this._Snackbar.open(Snackbar_Message, 'Dismiss', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          duration: 6000
        });

        this.GetMembersComplete();
      },
      (err) => {

        console.log('Error while getting Lease List', err);


      }
    );
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-lg'});
  }

  closeModal(template:TemplateRef<any>)
  {
    this.modalRef.hide();
  }


  SetupMemberForm(IndividualMemberDetail: MembersDetailDataDTO, ActionEvent: Number) {
    this.PrepareEditForm(IndividualMemberDetail);
    this.openModal(this.ActivityIntent);
    this.currentActionEvent=ActionEvent;
  }

  private PrepareEditForm(IndividualMemberDetail) {
    const EditFormGroup: FormGroup = this.fb.group([]);
    EditFormGroup.addControl('Name', new FormControl(IndividualMemberDetail.Name, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('Email', new FormControl(IndividualMemberDetail.Email, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('Address', new FormControl(IndividualMemberDetail.Address, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('CNIC', new FormControl(IndividualMemberDetail.CNIC, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('DOB', new FormControl(IndividualMemberDetail.DOB, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('Mobile', new FormControl(IndividualMemberDetail.Mobile, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('Gender', new FormControl(IndividualMemberDetail.Gender, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('MemberId', new FormControl(IndividualMemberDetail.MemberId, {}));

    EditFormGroup.addControl('UpdatedBy', new FormControl(this._SharedServiceCall.FetchLocalStorage('UserID'), {}));

    EditFormGroup.addControl('ProfilePicture', new FormControl(IndividualMemberDetail.ProfilePicture, {}));


    this.EditMemberForm = EditFormGroup;


  }

  /**
   *
   * @param $event
   * @constructor
   */
  UpdateMemberDetails($event: MouseEvent) {
    this.closeModal(this.ActivityIntent);
    this.UpdateMemberServiceCall(this.EditMemberForm);
    this.EditMemberForm.reset();
  }

  /**
   *
   * @param $event
   * @constructor
   */
  DiscardMemberForm($event: MouseEvent) {
    this.closeModal(this.ActivityIntent);
    this.EditMemberForm.reset();
  }

  /**
   *
   * @param EditMemberForm
   * @constructor
   * @private
   */
  private UpdateMemberServiceCall(EditMemberForm: FormGroup) {

    const FormValueData = EditMemberForm.getRawValue();
    this._StartupService.UpdateMember(FormValueData)
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: any) => {
        let Snackbar_Message: string = "User " + FormValueData.Name + " has been successfully updated.";

        this._Snackbar.open(Snackbar_Message, 'Dismiss', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          duration: 6000
        });

        this.GetMembersComplete();
      },
      (err) => {

        console.log('Error while getting Lease List', err);


      }
    );

  }



  UserActionEvent(ItemIterate: MembersDetailDataDTO, ActionType: Number) {
    switch (ActionType) {
      case this.EntityStatusEnum.Edit:
        this.SetupMemberForm(ItemIterate,this.EntityStatusEnum.Edit)
        break;

      case this.EntityStatusEnum.View:
        this.SetupMemberForm(ItemIterate,this.EntityStatusEnum.View)
        break;

    }

  }

  PrintList($event: MouseEvent) {
    debugger;
    const printContents = document.getElementById('content-table');
    const originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents.innerHTML;

    window.print();

    document.body.innerHTML = originalContents;
  }

  /**
   *
   * @param $event
   * @constructor
   */
  AddNewMemberList($event: MouseEvent) {
    this.router.navigate(['startup', 'membership', 'addition']).then(r => console.log(r));
  }
}
