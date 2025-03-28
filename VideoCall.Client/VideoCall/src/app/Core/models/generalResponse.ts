export interface ApiResponse<T> {
    Success: boolean;
    ErrorMessage: string;
    Data: T;
}