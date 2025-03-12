import { IdentityError } from "./identity-error.interface";

export interface RegisterResponse {
    errors?: IdentityError[];
}