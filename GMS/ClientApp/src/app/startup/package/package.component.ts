import {ApplicationRef, ChangeDetectorRef, Component, ElementRef, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {takeUntil} from "rxjs/operators";
import {EntityStatus, MembersDetailDataDTO, PackageDetailDataDTO} from "../../shared/shared-dto";
import {Subject, throwError} from "rxjs";
import {Router} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {StartupService} from "../startup.service";
import {MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition} from "@angular/material/snack-bar";
import {ServiceCallService} from "../../shared/service-call.service";
import {MatDrawer} from "@angular/material/sidenav";
import {MediaMatcher} from "@angular/cdk/layout";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";

@Component({
  selector: 'app-package',
  templateUrl: './package.component.html',
  styleUrls: ['./package.component.scss']
})
export class PackageComponent implements OnInit {
  passingHeading: string = "Packages";
  private ngUnsubscribe: Subject<any> = new Subject();
  public AllPackageData: PackageDetailDataDTO[];
  EntityStatusEnum: typeof EntityStatus = EntityStatus;
  horizontalPosition: MatSnackBarHorizontalPosition = 'left';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  @ViewChild('ActivityIntentForm') ActivityIntent: TemplateRef<any>;
  public currentActionEvent: number;
  public SelectedPackageData: PackageDetailDataDTO;
  public ActivityIntentFormGroup: FormGroup;
  reason='hello';

  modalRef?: BsModalRef;

  constructor(private modalService: BsModalService,private changeDetectorRef: ChangeDetectorRef,private media: MediaMatcher,private router: Router, private fb: FormBuilder, private applicationRef: ApplicationRef, private _StartupService: StartupService, private _Snackbar: MatSnackBar, private _SharedServiceCall: ServiceCallService) {
    this.GetAllPackagesComplete();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-lg'});
  }

  closeModal(template:TemplateRef<any>)
  {
    this.modalRef.hide();
  }


  ngOnInit(): void {
  }

  private GetAllPackagesComplete() {
    this._StartupService.GetPackages()
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: PackageDetailDataDTO[]) => {
        if (incomingResponse) {
          this.AllPackageData = incomingResponse;
        }

      },
      (err) => {

        console.log('Error while getting Lease List', err);
        return throwError(
          'Something bad happened; please try again later.');
      }
    );
  }

  PackagesEvent(ItemPackage?: PackageDetailDataDTO, EntityStatus?: number) {
debugger;
    if(EntityStatus==this.EntityStatusEnum.Creation)
    {
      let NewPackageData:PackageDetailDataDTO=new PackageDetailDataDTO();
      this.triggerActivityIntentFormCreation(NewPackageData);
      this.openModal(this.ActivityIntent);
      this.currentActionEvent = EntityStatus;
    }
    else
    {
      this.PrepareActivityIntentForm(ItemPackage);
      this.currentActionEvent = EntityStatus;
    }


  }

  DeletePackage(ItemPackage: PackageDetailDataDTO) {
    this._StartupService.DeletePackage(ItemPackage.PackageId)
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: any) => {
        let Snackbar_Message: string = "Package " + ItemPackage.Title + " has been successfully deleted.";

        this._Snackbar.open(Snackbar_Message, 'Dismiss', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          duration: 6000
        });

        this.GetAllPackagesComplete();
      },
      (err) => {

        console.log('Error while getting Lease List', err);


      }
    );
  }

  private PrepareActivityIntentForm(ItemPackage: PackageDetailDataDTO) {
    this.GetIndividualPackageDetail(ItemPackage.PackageId);
  }

  private GetIndividualPackageDetail(PackageId: number): void {

    this._StartupService.GetPackageById(PackageId)
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: PackageDetailDataDTO) => {
        if (incomingResponse) {
          this.SelectedPackageData = incomingResponse;
          this.triggerActivityIntentFormCreation(this.SelectedPackageData);
          this.openModal(this.ActivityIntent);
        }

      },
      (err) => {

        console.log('Error while getting Lease List', err);
        return throwError(
          'Something bad happened; please try again later.');
      }
    );

  }


  private triggerActivityIntentFormCreation(SelectedPackageData: PackageDetailDataDTO) {
    const EditFormGroup: FormGroup = this.fb.group([]);
    EditFormGroup.addControl('PackageId', new FormControl(SelectedPackageData.PackageId, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('Amount', new FormControl(SelectedPackageData.Amount, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('Title', new FormControl(SelectedPackageData.Title, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('DurationDays', new FormControl(SelectedPackageData.DurationDays, {
      validators: [Validators.required],
      updateOn: 'blur'
    }));

    EditFormGroup.addControl('DurationDays', new FormControl(SelectedPackageData.DurationDays));


    EditFormGroup.addControl('Description', new FormControl(SelectedPackageData.Description, {}));

    if (this.currentActionEvent == this.EntityStatusEnum.Edit) {
      EditFormGroup.addControl('UpdatedBy', new FormControl(this._SharedServiceCall.FetchLocalStorage('UserID'), {}));
    }
    if (this.currentActionEvent == this.EntityStatusEnum.Creation) {
      EditFormGroup.addControl('CreatedBy', new FormControl(this._SharedServiceCall.FetchLocalStorage('UserID'), {}));
    }
    debugger;
    this.ActivityIntentFormGroup = EditFormGroup;
  }

  UpdatePackageDetails($event: MouseEvent) {
    let PackageForm = this.ActivityIntentFormGroup.getRawValue();
    this.closeModal(this.ActivityIntent);

    this._StartupService.UpdatePackage(PackageForm)
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: any) => {
        if (incomingResponse) {
          let Snackbar_Message: string = incomingResponse.Message;

          this._Snackbar.open(Snackbar_Message, 'Dismiss', {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
            duration: 4000
          });

          this.ActivityIntentFormGroup.reset();
          location.reload(true);
        }

      },
      (err) => {

        console.log('Error while getting Lease List', err);
        return throwError(
          'Something bad happened; please try again later.');
      }
    );

  }

  DiscardPackageForm($event: MouseEvent) {
    this.closeModal(this.ActivityIntent);
    this.ActivityIntentFormGroup.reset();
  }

  CreatePackageDetails($event: MouseEvent) {
    let PackageForm = this.ActivityIntentFormGroup.getRawValue();
    this.closeModal(this.ActivityIntent);

    this._StartupService.InsertPackage(PackageForm)
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: any) => {
        if (incomingResponse) {
          let Snackbar_Message: string = incomingResponse.Message;

          this._Snackbar.open(Snackbar_Message, 'Dismiss', {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
            duration: 4000
          });

          this.ActivityIntentFormGroup.reset();
          location.reload(true);
        }

      },
      (err) => {

        console.log('Error while getting Lease List', err);
        return throwError(
          'Something bad happened; please try again later.');
      }
    );
  }
}
