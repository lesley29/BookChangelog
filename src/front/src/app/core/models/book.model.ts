import { Author } from "./author.model";

export interface Book {
    id: string,
    title: string,
    description?: string,
    publicationDate: string,
    authors: Author[]
}

export const enum BookAuthorChangeType {
    Added = "Added",
    Removed = "Removed"
}

export interface AuthorChange {
    id: string,
    name: string,
    changeType: BookAuthorChangeType
}

export interface BookChange {
    title?: string,
    description?: string,
    publicationDate?: string,
    authors?: AuthorChange[]
}

export interface BookChangeHistory {
    changeNumber: number,
    changeDateTime: Date,
    change: BookChange
}