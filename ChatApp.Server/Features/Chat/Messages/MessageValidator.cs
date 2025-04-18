using FluentValidation;

namespace ChatApp.Server.Features.Chat.Messages;

public class MessageValidator : AbstractValidator<ChatMessageRequest>
{
    public MessageValidator()
    {
        RuleFor(x => x.ChannelId).NotNull().NotEmpty();
        RuleFor(x => x.Text).NotNull().Length(1, 500).Must(NotHaveLeadingSpace);
    }

    private static bool NotHaveLeadingSpace(string text)
    {
        return !text.StartsWith(' ');
    }
}
