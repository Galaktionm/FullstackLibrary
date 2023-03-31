import { Author, BookSummary } from "./models";

export interface BookApiResult {
    data: BookSummary[];
    pageIndex: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
}

export interface AuthorApiResult {
    data: Author[];
    pageIndex: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
}