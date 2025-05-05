using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services;

public class NotificationScheduler(IEmailService emailService, IDingTalkService dingService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // 示例：每天9点发送通知
            if (DateTime.Now.Hour == 9 && DateTime.Now.Minute == 0)
            {
                await emailService.SendEmailAsync("user@example.com", "每日公告", "这是今天的公告内容。");
                await dingService.SendMessageAsync("这是通过钉钉发送的公告内容。");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}