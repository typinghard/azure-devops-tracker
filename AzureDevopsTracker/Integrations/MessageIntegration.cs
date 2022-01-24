using AzureDevopsTracker.Entities;
using System;
using System.Net.Http;
using System.Text;

namespace AzureDevopsTracker.Integrations
{
    public abstract class MessageIntegration
    {
        internal abstract void Send(ChangeLog changeLog);
        private static readonly string MIMETYPE = "application/json";
        private static readonly string CDN_URL = "https://cdn.typinghard.tech/";
        private static readonly string MEGAFONE_GIF = "megafone.gif";
        private static readonly string LOGO_TYPINGHARD_16X16 = "logo-typinghard-16x16.png";

        protected internal string GetTitle(ChangeLog changeLog)
        {
            return $"Nova atualização da plataforma";
        }

        protected internal string GetVersion(ChangeLog changeLog)
        {
            return $"Versao: { changeLog.Number}";
        }

        protected internal string GetAnnouncementImageUrl()
        {
            return $"{CDN_URL}{MEGAFONE_GIF}";
        }

        protected internal string GetLogoTypingHard16x16Url()
        {
            return $"{CDN_URL}{LOGO_TYPINGHARD_16X16}";
        }

        protected internal string GetNugetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return $"Powered by **Typing Hard** • nuget version { fileVersionInfo.FileVersion }";
        }

        protected internal HttpResponseMessage Notify(object body)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            var content = new StringContent(json,
                                            Encoding.UTF8,
                                            MIMETYPE);
            HttpClient client = new HttpClient();
            return client.PostAsync(new Uri(MessageConfig.URL),
                content).Result;

        }
    }
}
