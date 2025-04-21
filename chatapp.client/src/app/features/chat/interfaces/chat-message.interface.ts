import { MessageType } from "../enums/message-type.enum";

export interface ChatMessage {
    id: number;
    type: MessageType;
    timestamp: Date;
    text: string;
    textColor?: string;
}
