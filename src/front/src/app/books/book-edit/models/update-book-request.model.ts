export interface UpdateBookRequest {
    title?: string | null,
    description?: string | null,
    publicationDate?: string | null,
    authors?: string[] | null
}