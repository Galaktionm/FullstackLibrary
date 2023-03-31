
export interface LoginRequest {
    email: string
    password: string
}

export interface LoginResult {
    success: boolean
    token: string
    userId: string
    isAdmin:boolean
}