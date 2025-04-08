using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Exiled.API.Features;
using Newtonsoft.Json;

namespace WarnSystem_PepperFrog.Models
{
    [Serializable]
    public class Warn
    {
        public Warn()
        {
        }
        
        public Warn(Id target, Id issuer, string reason)
        {
            TargetId = target.UserId;
            TargetName = target.Nickname;
            IssuerId = issuer.UserId;
            IssuerName = issuer.Nickname;
            Reason = reason;
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string TargetId { get; set; }

        public string TargetName { get; set; }

        public string IssuerId { get; set; }

        public string IssuerName { get; set; }

        public string Reason { get; set; }

        private static readonly HttpClient HttpClient = new HttpClient();

        public override string ToString() =>
            $"[{Date:MM/dd/yyyy}] {TargetName} ({TargetId}) | {IssuerName} ({IssuerId}) > {Reason}";

        public string ApplyWarn()
        {
            var postData = new Dictionary<string, string>
            {
                { "targetId", TargetId },
                { "targetName", TargetName },
                { "issuerId", IssuerId },
                { "issuerName", IssuerName },
                { "reason", Reason },
                { "API_KEY", Plugin.Instance.Config.APIKey }
            };


            HttpContent content = new FormUrlEncodedContent(postData);

            HttpResponseMessage response =
                HttpPostRequest(
                    Plugin.Instance.Config.Botip + ":" + Plugin.Instance.Config.Port + Plugin.Instance.Config.Uri,
                    content);
            if (response is not null)
            {
                Log.Debug(RetriveString(response.Content));
                return RetriveString(response.Content);
            }

            return "The response is null, error on the retrieve of the response";
        }

        public static List<Warn> GetWarnsOfPlayer(string steamid)
        {
            var postData = new Dictionary<string, string>
            {
                { "targetId", steamid },
                { "API_KEY", Plugin.Instance.Config.APIKey }
            };

            HttpContent content = new FormUrlEncodedContent(postData);

            HttpResponseMessage response =
                HttpPostRequest(
                    Plugin.Instance.Config.Botip + ":" + Plugin.Instance.Config.Port + Plugin.Instance.Config.Uri,
                    content);
            if (response is null)
            {
                return new List<Warn>();
            }

            string stringResp = RetriveString(response.Content);
            Log.Debug(stringResp);
            
            List<Warn> warns = JsonConvert.DeserializeObject<List<Warn>>(stringResp);
            return warns;
        }

        public static string RemoveWarnOfPlayer(string steamid, int index)
        {
            var postData = new Dictionary<string, string>
            {
                { "targetId", steamid },
                { "removeId", index.ToString() },
                { "API_KEY", Plugin.Instance.Config.APIKey }
            };

            HttpContent content = new FormUrlEncodedContent(postData);

            HttpResponseMessage response =
                HttpPostRequest(
                    Plugin.Instance.Config.Botip + ":" + Plugin.Instance.Config.Port + Plugin.Instance.Config.Uri,
                    content);
            if (response is not null)
            {
                Log.Debug(RetriveString(response.Content));
                return RetriveString(response.Content);
            }

            return "The response is null, error on the retrieve of the response";
        }

        private static HttpResponseMessage HttpPostRequest(string url, HttpContent content)
        {
            try
            {
                Task<HttpResponseMessage> response = Task.Run(() => HttpClient.PostAsync(url, content));

                response.Wait();

                return response.Result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return null;
            }
        }

        private static string RetriveString(HttpContent response)
        {
            if (response is null)
                return string.Empty;

            Task<string> result = Task.Run(response.ReadAsStringAsync);

            result.Wait();

            return result.Result;
        }
    }
}