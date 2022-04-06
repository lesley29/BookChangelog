export interface CreateBookRequest {
    title: string,
    description?: string | null,
    publicationDate: string,
    authors: string[]
}