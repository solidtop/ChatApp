import { UserSummary } from "../../user/interfaces/user-summary.interface";
import { ChatMessage } from "./chat-message.interface";

export interface ChannelMessage extends ChatMessage {
    user: UserSummary;
}