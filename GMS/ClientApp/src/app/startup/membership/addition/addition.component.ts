import {Component, OnInit, Sanitizer, SecurityContext} from '@angular/core';
import {ServiceCallService} from "../../../shared/service-call.service";
import {UploadMetaDataDTO} from "../../../shared/shared-dto";
import {DomSanitizer, SafeUrl} from '@angular/platform-browser';

@Component({
  selector: 'app-addition',
  templateUrl: './addition.component.html',
  styleUrls: ['./addition.component.scss']
})
export class AdditionComponent implements OnInit {
  userImageSource: SafeUrl | string;
  fileToUpload: File | null = null;

  constructor(private sharedService:ServiceCallService,private sanitizer:DomSanitizer) {
    this.userImageSource='assets/Static/Images/user.png';

  }

  ngOnInit(): void {
  }

  // triggerFileChange($event: Event) {
  //   console.log($event);
  //   var fileObject=$event.target.files;
  // }
  /**
   *
   * @param $event
   * @param files
   */
  triggerFileChange($event: Event, files: FileList) {
    this.fileToUpload=files.item(0);
    this.uploadFileToActivity();
  }

  private uploadFileToActivity() {
    debugger;
    this.sharedService.postFile(this.fileToUpload).subscribe((data:UploadMetaDataDTO) => {
      // do something, if upload success
      console.log(data.filePath);
      if(data.fileUpload)
      {
        this.userImageSource=this.sanitizer.bypassSecurityTrustUrl(data.filePath.toString());
      }
    }, error => {
      console.log(error);
    });
  }
}
