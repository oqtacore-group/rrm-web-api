using Amazon.S3;
using Amazon.S3.Model;
using Oqtacore.Rrm.Api.Helpers;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Infrastructure.Data;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace Oqtacore.Rrm.Api.Models
{
    public class TelegramBotReminder
    {
        public static bool active = false;
        private static TelegramBotReminder _telegramBot;
        private static IServiceScopeFactory scopeFactory;
        private static IAmazonS3 _s3Client;
        private static IConfiguration _configuration;
        private static string bucketName;
        public static TelegramBotReminder telegramBotClient
        {
            get
            {
                if (_telegramBot == null)
                {
                    _telegramBot = new TelegramBotReminder();
                }
                return _telegramBot;
            }

        }
        public void Start(string telegramId, IServiceScopeFactory _scopeFactory, IAmazonS3 s3Client, IConfiguration configuration, IWebHostEnvironment environment)
        {
            scopeFactory = _scopeFactory;
            _s3Client = s3Client;
            _configuration = configuration;
            string env = EnvironmentManager.GetEnvironmentName(environment);
            bucketName = $"{env}-rrm-candidate-files"; 
            Main(telegramId);
            active = true;
        }

        //b7uTtrudpdL35jQ8MVA7E  gmailBot
        // Start Bot
        private static Regex digitsOnly = new Regex(@"[^\d]");
        private static TelegramBotClient telegramBot;
        private static int messageOffset = 0;

        static void Main(string id)
        {

            telegramBot = new TelegramBotClient(id);
            telegramBot.OnMessage += OnMessageReceived;
            telegramBot.OnMessageEdited += OnMessageReceived;


            try
            {
                telegramBot.SetWebhookAsync("").Wait();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }

            var me = telegramBot.GetMeAsync();


            me.Wait();

            telegramBot.StartReceiving(Array.Empty<UpdateType>());


        }

        ~TelegramBotReminder()
        {
            if (telegramBot != null)
            {
                telegramBot.StopReceiving();
                active = false;
            }
        }
        private static string CleanPhone(string phone)
        {
            return digitsOnly.Replace(phone, "");
        }
        #region MessageProcessing
        private static async void OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            using (var scope = scopeFactory.CreateScope())
            {
                ApplicationContext thql = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                if (message.Text != null && message.Text != "" && message.Text.Contains("/ChatName:"))
                {
                    string messageId = message.From.Id.ToString();
                    var user = thql.RegisteredChat.SingleOrDefault(t => t.ChatId == messageId && t.TelegramBotName == "Reminder");
                    if (user == null)
                    {
                        thql.RegisteredChat.Add(new RegisteredChat()
                        {
                            ChatId = messageId,
                            DateTime = DateTime.UtcNow,
                            Name = message.Text.Replace("/ChatName:", ""),
                            Approved = false,
                            TelegramBotName = "Reminder",
                            TelegramBotId = telegramBot.BotId
                        });

                        thql.SaveChanges();
                        await messageRegister(message.From.Id);
                    }
                }
            }
        }

        #endregion

        #region StandartMessage

        //Start message
        private static async Task messageStart(long chatID)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                ApplicationContext thql = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                await telegramBot.SendChatActionAsync(chatID, ChatAction.Typing);

                await Task.Delay(500); // simulate longer running task
                Message replyHead = await telegramBot.SendTextMessageAsync(
     chatID,
     "Start Message for Telegram ThinkCell Bot");
            }
        }

        //Register message
        private static async Task messageRegister(long chatID)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                ApplicationContext thql = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                // await telegramBot.SendChatActionAsync(chatID, ChatAction.Typing);

                await Task.Delay(500); // simulate longer running task
                Message replyHead = await telegramBot.SendTextMessageAsync(
     chatID,
     "Good Job! We saved your chat id");
            }
        }

        //Send Test Result
        public async Task sendReminderAll(string message, List<CandidateFile> files = null)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                ApplicationContext thql = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                if (telegramBot != null)
                {
                    List<RegisteredChat> chatsToDeliver = thql.RegisteredChat.Where(t => t.Approved && t.TelegramBotName == "Reminder").ToList();
                    foreach (RegisteredChat chat in chatsToDeliver)
                    {
                        await messageReminder(thql, Convert.ToInt64(chat.ChatId), message, files);
                    }
                }
            }
        }

        public async Task sendReminderAllEarlyBirds(string message, List<CandidateFile> files = null)
        {

            using (var scope = scopeFactory.CreateScope())
            {
                ApplicationContext thql = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                if (telegramBot != null)
                {
                    List<RegisteredChat> chatsToDeliver = thql.RegisteredChat.Where(t => t.Approved && t.TelegramBotName == "EarlyRem").ToList();
                    foreach (RegisteredChat chat in chatsToDeliver)
                    {
                        await messageReminder(thql, Convert.ToInt64(chat.ChatId), message, files);
                    }
                }
            }
        }

        //Register message
        private static async Task messageReminder(ApplicationContext thql, long chatID, string resultText, List<CandidateFile> filesList = null)
        {

            try
            {
                //  await telegramBot.SendChatActionAsync(chatID, ChatAction.Typing);
                //Adding file with pdf
                if (filesList != null && filesList.Count > 0)
                {
                    int i = 0;
                    foreach (CandidateFile file in filesList)
                    {
                        if (i == 0)
                        {
                            Message replyHead = await telegramBot.SendTextMessageAsync(chatID, resultText);
                        }

                        if (await FileExists(file))
                        {
                            var fileResponse = await DownloadCandidateFile(file);
                            using (Stream responseStream = fileResponse.ResponseStream)
                            {
                                var fileType = file.fileUrl.Substring(file.fileUrl.LastIndexOf("."));
                                var fileTele = new InputOnlineFile(fileResponse.ResponseStream, "file" + fileType);
                                Message result = await telegramBot.SendDocumentAsync(chatID, fileTele);
                                thql.Logs.Add(new Log()
                                {
                                    Date = DateTime.UtcNow,
                                    Text = result.ToString() + result.MessageId.ToString() + result.Text
                                });
                                thql.SaveChanges();
                            }
                        }
                        else
                        {
                            thql.Logs.Add(new Log()
                            {
                                Date = DateTime.UtcNow,
                                Text = "ReminderCheck TelegramBot: \n Not Found:" + file.fileUrl
                            });
                            thql.SaveChanges();
                        }

                        i++;
                    }
                }
                else
                {
                    Message replyHead = await telegramBot.SendTextMessageAsync(chatID, resultText);
                }
            }
            catch (Exception ex)
            {
                thql.Logs.Add(new Log()
                {
                    Date = DateTime.UtcNow,
                    Text = "ReminderCheck TelegramBot: \nError:" + Convert.ToString(ex)
                });
                thql.SaveChanges();

            }
            return;
        }

        #endregion

        private static async Task<GetObjectResponse> DownloadCandidateFile(CandidateFile candidateFile)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = candidateFile.fileUrl
            };

            return await _s3Client.GetObjectAsync(request);
        }
        private static async Task<bool> FileExists(CandidateFile candidateFile)
        {
            try
            {
                var metadata = await _s3Client.GetObjectMetadataAsync(bucketName, candidateFile.fileUrl);
                return true; // File exists
            }
            catch (AmazonS3Exception e) when (e.ErrorCode == "NotFound" || e.ErrorCode == "NoSuchKey")
            {
                return false; // File does not exist
            }
        }
    }
}