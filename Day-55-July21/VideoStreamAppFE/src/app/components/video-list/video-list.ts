import { Component, OnInit } from '@angular/core';
import { TrainingVideo } from '../../models/training-video';
import { VideoService } from '../../services/video.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-video-list',
  imports: [DatePipe],
  templateUrl: './video-list.html',
  styleUrl: './video-list.css'
})
export class VideoList  implements OnInit{
  videos: TrainingVideo[] = [];
  selectedVideo: TrainingVideo | null = null;
  isLoading: boolean = false;

  constructor(private videoService: VideoService) { }

  ngOnInit(): void {
    this.loadVideos();
  }

  loadVideos(){
    this.isLoading = true;

    this.videoService.getAllVideos().subscribe({
      next: (result : any) => {
        this.videos = result;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err.message);
        this.isLoading = false;
      }
    })
  }
  closeVideo(): void {
    this.selectedVideo = null;
  }

  selectVideo(video: TrainingVideo) {
    this.selectedVideo = video;
  }
}
