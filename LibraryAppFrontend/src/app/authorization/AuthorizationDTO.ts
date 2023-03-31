import { Checkout } from "../functionality/models"

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

export interface RegisterRequest {
    username: string
    email: string
    password: string
}

export interface User {
    userId: string,
    username: string,
    email: string,
    checkouts: Checkout[]
}