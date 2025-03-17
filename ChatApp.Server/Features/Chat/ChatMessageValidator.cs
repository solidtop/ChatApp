using FluentValidation;

namespace ChatApp.Server.Features.Chat;

public class ChatMessageValidator : AbstractValidator<ChatMessageRequest>
{
    public ChatMessageValidator()
    {
        RuleFor(x => x.Text).NotNull().Length(1, 500).Must(NotHaveLeadingSpace);
        RuleFor(x => x.ChannelId).NotNull().NotEmpty();
    }

    private static bool NotHaveLeadingSpace(string text)
    {
        return !text.StartsWith(' ');
    }
}
