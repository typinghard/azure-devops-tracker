using AzureDevopsTracker.Entities;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AzureDevopsTracker.Integrations
{
    public abstract class MessageIntegration
    {
        public abstract void Send(ChangeLog changeLog);
        private static readonly string MIMETYPE = "application/json";

        protected string GetTitle(ChangeLog changeLog)
        {
            return $"Nova atualização da plataforma";
        }

        protected string GetVersion(ChangeLog changeLog)
        {
            return $"Versao: { changeLog.Number}";
        }

        protected string GetNugetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return $"Powered by Typing Hard • { fileVersionInfo.FileVersion }";
        }

        protected HttpResponseMessage Notify(object body)
        {
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json,
                                            Encoding.UTF8,
                                            MIMETYPE);
            HttpClient client = new HttpClient();
            return client.PostAsync(new Uri(MessageConfig.URL),
                content).Result;
        }
    }
}
