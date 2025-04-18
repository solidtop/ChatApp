import { UserSummary } from "../../user/interfaces/user-summary.interface";

export interface ChatMessage {
    id: number;
    timestamp: Date,
    user: UserSummary,
    text: string;
    textColor?: string;
}
