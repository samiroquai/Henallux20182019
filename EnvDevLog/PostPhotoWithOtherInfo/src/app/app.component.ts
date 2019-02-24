import { Component, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse, HttpHeaders } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';
import { ChangeDetectorStatus } from '@angular/core/src/change_detection/constants';
import { UploadResult } from './upload-result';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'PostPhotoWithOtherInfo';
  uploadForm: FormGroup;
  file: File[];
  /**
   *
   */
  constructor(private http: HttpClient, private cd: ChangeDetectorRef) {
    this.uploadForm = new FormGroup({
      idConceptLie: new FormControl("Lieu", Validators.required),
      fichier: new FormControl(null, Validators.required)
    });
  }

  upload(): void {
    if(!this.file || this.file.length===0)
      return;
    const fileUploadInfo = this.uploadForm.value;
    const formData: FormData = new FormData();
    formData.append('file', this.file[0], this.file[0].name);
    formData.append('idConceptLie', fileUploadInfo.idConceptLie);
    //const headers = new HttpHeaders({ 'Accept': 'application/json' });

    this.http.post<UploadResult>("http://localhost:5000/api/Photos", formData)
      // .pipe(map((res: Response) => {
      //   res.json();
      // }
      // ))
      .subscribe((uploadResult) => {
        console.log("Le fichier a été uploadé");
        console.log(uploadResult);
      }, (error) => {
        console.error("Erreur lors de l'upload");
        console.error(error);
      });
  }

  onFileChange(event) {
    let reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      this.file = event.target.files;
    }
  }
}
