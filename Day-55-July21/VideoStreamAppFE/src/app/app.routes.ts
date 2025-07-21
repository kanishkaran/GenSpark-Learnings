import { Routes } from '@angular/router';
import { VideoList } from './components/video-list/video-list';
import { VideoUpload } from './components/video-upload/video-upload';

export const routes: Routes = [
    {path: "list", component:VideoList},
    {path: "upload", component: VideoUpload}
];
