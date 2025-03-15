import { Avatar } from "./avatar.interface";

export interface AccountProfile {
    userId: string;
    username: string;
    email: string;
    roles: string[];
    displayColor?: string;
    avatar?: Avatar;
}