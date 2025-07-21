import { Component } from '@angular/core';
import { UploadTrainingVideo } from '../../models/training-video';
import { VideoService } from '../../services/video.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-video-upload',
  imports: [FormsModule],
  templateUrl: './video-upload.html',
  styleUrl: './video-upload.css'
})
export class VideoUpload {
uploadData: Omit<UploadTrainingVideo, 'Video'> = {
    title: '',
    description: ''
  };
  
  selectedFile: File | null = null;
  isUploading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private videoService: VideoService) {}

  onFileSelected(event: Event): void {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
      this.selectedFile = target.files[0];
      this.errorMessage = '';
    }
  }

  onSubmit(): void {
    if (!this.selectedFile) {
      this.errorMessage = 'Please select a video file';
      return;
    }

    this.isUploading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const uploadDto: UploadTrainingVideo = {
      title: this.uploadData.title,
      description: this.uploadData.description,
      Video: this.selectedFile
    };

    this.videoService.uploadVideo(uploadDto).subscribe({
      next: (response : any) => {
        this.isUploading = false;
        this.successMessage = `Video "${response.title}" uploaded successfully!`;
        
      },
      error: (error) => {
        this.isUploading = false;
        this.errorMessage = error.error?.message || 'Upload failed. Please try again.';
        console.error('Upload error:', error);
      }
    });
  }

  resetForm(form?: any): void {
    this.uploadData = {
      title: '',
      description: ''
    };
    this.selectedFile = null;

    this.errorMessage = '';
    this.successMessage = '';
    
    if (form) {
      form.resetForm();
    }
  }

}
