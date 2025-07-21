
export interface TrainingVideo{
    id: number
    title: string
    description: string
    uploadDate: Date
    blobUrl: string
}

export interface UploadTrainingVideo{
    title: string
    description: string
    Video: File
}