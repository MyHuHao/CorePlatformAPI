using System.Text;
using System.Text.Json;

namespace Infrastructure.Services;

public interface IDingTalkService
{
    Task SendMessageAsync(string content);
}

public class DingTalkService(HttpClient http) : IDingTalkService
{
    public async Task SendMessageAsync(string content)
    {
        const string url = "https://oapi.dingtalk.com/robot/send?access_token=YOUR_TOKEN";
        var payload = new { msgtype = "text", text = new { content } };
        var httpContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        await http.PostAsync(url, httpContent);
    }
}