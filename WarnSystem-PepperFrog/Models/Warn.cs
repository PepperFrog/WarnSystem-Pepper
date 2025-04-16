using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using MEC;
using Newtonsoft.Json;
using UnityEngine;

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

        public override string ToString() =>
            $"{Id}: [{Date:yyyy-MM-ddTHH:mm}] {TargetName} ({TargetId}) | {IssuerName} ({IssuerId}) > {Reason}";

        public string ToStringPlayer() =>
            $"{Id}: [{Date:yyyy-MM-ddTHH:mm}] {TargetName} | {IssuerName} > {Reason}";

        public void ApplyWarn()
        {
            WWWForm form = new WWWForm();
            form.AddField("action", "insertwarn");
            form.AddField("targetId", TargetId);
            form.AddField("targetName", TargetName);
            form.AddField("issuerId", IssuerId);
            form.AddField("issuerName", IssuerName);
            form.AddField("reason", Reason);
            form.AddField("API_KEY", Plugin.Instance.Config.APIKey);

            Timing.RunCoroutine(Plugin.SendPostMessage(form, Plugin.Instance.Config.Url));
        }

        public static void GetWarnsOfPlayer(string steamid, Action<List<Warn>> onComplet)
        {
            if (Regex.IsMatch(steamid, @"(?:7656119\d{10}@steam)|(?:\d{17,19}@discord)"))
            {
                string getUrl = Plugin.Instance.Config.Url + "?action=getwarnbyplayer&targetId=" + steamid;

                Timing.RunCoroutine(Plugin.SendGetMessage(getUrl, onComplet));
            }
        }

        public static void GetWarnsById(int id, Action<List<Warn>> onComplet)
        {
            string getUrl = Plugin.Instance.Config.Url + "?action=getwarnbyid&id=" + id;
            Log.Debug(getUrl);

            Timing.RunCoroutine(Plugin.SendGetMessage(getUrl, onComplet));
        }

        public static void RemoveWarnOfPlayer(int index)
        {
            WWWForm form = new WWWForm();
            form.AddField("action", "removewarn");
            form.AddField("removeId", index.ToString());
            form.AddField("API_KEY", Plugin.Instance.Config.APIKey);

            Timing.RunCoroutine(Plugin.SendPostMessage(form, Plugin.Instance.Config.Url));
        }

        public static string GenerateWarnList(List<Warn> warns, bool toPlayer)
        {
            string stringBuilder = "";
            foreach (Warn warn in warns)
            {
                stringBuilder += (toPlayer ? warn.ToStringPlayer() : warn.ToString()) + "\n";
            }

            return stringBuilder;
        }
    }
}