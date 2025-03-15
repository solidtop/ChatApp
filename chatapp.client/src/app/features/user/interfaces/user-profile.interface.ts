import { Avatar } from "../../account/interfaces/avatar.interface";

export interface UserProfile {
    id: string;
    username: string;
    roles: string[];
    displayColor?: string;
    avatar?: Avatar;
}