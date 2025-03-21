import { UserSummary } from "../../user/interfaces/user-summary.interface";

export interface ChatMessageResponse {
    id: number;
    text: string;
    textColor?: string;
    timestamp: Date,
    user: UserSummary,
}
