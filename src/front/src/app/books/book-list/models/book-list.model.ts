export interface BookFilter {
    publishedFrom?: string | null,
    publishedTo?: string | null,
    sortBy: string,
    sortDirection: string
}

export const enum SortableField {
    Title = "Title",
    PublicationDate = "PublicationDate"
}

export const enum SortDirection {
    Ascending = "Ascending",
    Descending = "Descending"
}