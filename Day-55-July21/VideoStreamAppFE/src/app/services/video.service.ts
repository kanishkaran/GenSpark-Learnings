import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UploadTrainingVideo } from "../models/training-video";

@Injectable()
export class VideoService{

    private readonly baseUrl = "http://localhost:5170/api/Video"

    constructor(private http: HttpClient) { }

    uploadVideo(video: UploadTrainingVideo) {
        const formData = new FormData();
        formData.append("Title", video.title);
        formData.append("Description", video.description);
        formData.append("Video", video.Video);

        return this.http.post(`${this.baseUrl}`, formData);
    }

    getAllVideos() {
        return this.http.get(this.baseUrl);
    }

    getVideo(id : number) {
        return this.http.get(`${this.baseUrl}/${id}/stream`);
    }
}