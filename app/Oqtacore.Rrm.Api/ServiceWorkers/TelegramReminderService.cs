using Oqtacore.Rrm.Api.Models;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Infrastructure.Data;
using System.Net;
using TimeZoneConverter;

namespace Oqtacore.Rrm.Api.ServiceWorkers
{

    public class TelegramReminderService : IHostedService, IDisposable
    {

        private Timer _timer;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IConfiguration _configuration;
        public TelegramReminderService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            this.scopeFactory = scopeFactory;
            this._configuration = configuration;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new System.Threading.Timer(DoWork, "",
            1000 * 60 * 1, 1000 * 60 * 1);

            return Task.CompletedTask;
        }


        private void DoWork(object state)
        {
            var applicationDomainUrl = _configuration["ApplicationDomainUrl"];
            using (var scope = scopeFactory.CreateScope())
            {
                ApplicationContext thql = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                DateTime checkLimit = DateTime.UtcNow.AddMinutes(5);
                DateTime currentTime = DateTime.UtcNow;
                List<CandidateEvent> eventList = thql.CandidateEvent.Where(t => t.ReminderSent != true && t.Completed != true && t.Date > currentTime && t.Date < checkLimit).ToList();
                foreach (CandidateEvent canEvent in eventList)
                {
                    canEvent.ReminderSent = true;
                    thql.SaveChanges();
                    Candidate candidate = thql.Candidate.Single(t => t.id == canEvent.CandidateId);
                    try
                    {
                        CandidatesVacancyStatu candidatesVacancy = thql.CandidatesVacancyStatus.Where(t => t.CandidateId == candidate.id && t.VacancyStatusId != 9).OrderByDescending(t => t.DateAdded).FirstOrDefault();
                        if (candidatesVacancy != null)
                        {
                            CandidatesVacancyStatu newConnect = new CandidatesVacancyStatu()
                            {
                                CandidateId = candidatesVacancy.CandidateId,
                                DateAdded = DateTime.UtcNow,
                                Note = "Automatic due to event",
                                VacancyId = candidatesVacancy.VacancyId,
                                VacancyStatusId = 11,
                                CreatedBy = 18//Service account for TelegramBot
                            };
                            canEvent.HashCode = Guid.NewGuid().ToString();//To reset ability to change vacancy status
                            thql.CandidatesVacancyStatus.Add(newConnect);
                            thql.SaveChanges();
                        }
                        Vacancy candidateLastVacancy = candidatesVacancy != null ? thql.Vacancy.SingleOrDefault(t => t.id == candidatesVacancy.VacancyId) : null;
                        DateTime eventTime = TimeZoneInfo.ConvertTimeFromUtc(canEvent.Date, TZConvert.GetTimeZoneInfo(TZConvert.IanaToWindows("Europe/Moscow")));
                        string message = String.Format("{0}\n{1}-{2}\n{3}\n{4}\n{5}/Vacancy/profile/0/Candidates/0/{6}\n{7}\n", eventTime.ToShortDateString(),
                            eventTime.ToString("HH:mm"),
                            eventTime.AddMinutes(45).ToString("HH:mm"), candidate.Name, candidateLastVacancy != null ? candidateLastVacancy.Name : "",
                            applicationDomainUrl,
                            candidate.id, canEvent.ZoomLink != null ? canEvent.ZoomLink : "");
                        //Link for changing status - {7}\n
                        if (canEvent.HashCode != null && candidateLastVacancy != null)
                        {
                            message = message + $"Link for changing status - {applicationDomainUrl}/Telegram/ChangeStatus/" + canEvent.HashCode;
                        }

                        if (thql.CandidateFile.Any(t => t.candidateId == canEvent.CandidateId))
                        {
                            List<CandidateFile> filesList = thql.CandidateFile.Where(t => t.candidateId == canEvent.CandidateId).ToList();


                            TelegramBotReminder.telegramBotClient.sendReminderAll(message, filesList).Wait();

                        }
                        else
                        {
                            TelegramBotReminder.telegramBotClient.sendReminderAll(message).Wait();
                        }

                    }
                    catch (Exception ex)
                    {
                        thql.Logs.Add(new Log()
                        {
                            Date = DateTime.UtcNow,
                            Text = "ReminderCheck:" + candidate.id + " \nError:" + Convert.ToString(ex)
                        });
                        thql.SaveChanges();
                    }
                }

                DateTime checkEarlyLimit = DateTime.UtcNow.AddMinutes(30);
                currentTime = DateTime.UtcNow;
                eventList = thql.CandidateEvent.Where(t => t.ReminderEarlySent != true && t.Completed != true && t.Date > currentTime && t.Date < checkEarlyLimit).ToList();
                foreach (CandidateEvent canEvent in eventList)
                {
                    canEvent.ReminderEarlySent = true;
                    thql.SaveChanges();
                    Candidate candidate = thql.Candidate.Single(t => t.id == canEvent.CandidateId);
                    try
                    {
                        CandidatesVacancyStatu candidatesVacancy = thql.CandidatesVacancyStatus.Where(t => t.CandidateId == candidate.id && t.VacancyStatusId != 9).OrderByDescending(t => t.DateAdded).FirstOrDefault();

                        Vacancy candidateLastVacancy = candidatesVacancy != null ? thql.Vacancy.SingleOrDefault(t => t.id == candidatesVacancy.VacancyId) : null;
                        DateTime eventTime = TimeZoneInfo.ConvertTimeFromUtc(canEvent.Date, TZConvert.GetTimeZoneInfo(TZConvert.IanaToWindows("Europe/Moscow")));
                        string message = String.Format("{0}\n{1}-{2}\n{3}\n{4}\n{5}/Vacancy/profile/0/Candidates/0/{6}\n{7}\n", eventTime.ToShortDateString(),
                            eventTime.ToString("HH:mm"),
                            eventTime.AddMinutes(45).ToString("HH:mm"), candidate.Name, candidateLastVacancy != null ? candidateLastVacancy.Name : "",
                            applicationDomainUrl,
                            candidate.id, canEvent.ZoomLink != null ? canEvent.ZoomLink : "");
                        //Link for changing status - {7}\n
                        if (canEvent.HashCode != null && candidateLastVacancy != null)
                        {
                            message = message + $"Link for changing status - {applicationDomainUrl}/Telegram/ChangeStatus/" + canEvent.HashCode;
                        }

                        if (thql.CandidateFile.Any(t => t.candidateId == canEvent.CandidateId))
                        {
                            List<CandidateFile> filesList = thql.CandidateFile.Where(t => t.candidateId == canEvent.CandidateId).ToList();


                            TelegramBotReminder.telegramBotClient.sendReminderAllEarlyBirds(message, filesList).Wait();

                        }
                        else
                        {
                            TelegramBotReminder.telegramBotClient.sendReminderAllEarlyBirds(message).Wait();
                        }

                    }
                    catch (Exception ex)
                    {
                        thql.Logs.Add(new Log()
                        {
                            Date = DateTime.UtcNow,
                            Text = "ReminderCheck:" + candidate.id + " \nError:" + Convert.ToString(ex)
                        });
                        thql.SaveChanges();
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
