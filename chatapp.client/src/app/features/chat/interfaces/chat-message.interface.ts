import { UserSummary } from "../../user/interfaces/user-summary.interface";

export enum MessageType {
    Channel,
    Whisper,
    Notification,
    Announcement,
    Error,
}

export interface ChatMessage {
    id: number;
    type: MessageType;
    timestamp: Date;
    sender?: UserSummary;
    receiver?: UserSummary;
    content: string;
}
