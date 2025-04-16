using System;
using System.Collections;
using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using WarnSystem_PepperFrog.Models;

namespace WarnSystem_PepperFrog
{
    public class Plugin : Plugin<Config, Translation>
    {
        public static Plugin Instance { get; private set; }

        public override string Author => "Build Modified by AntonioFo";

        public override string Name => "WarnSystem";

        public override string Prefix => "WarnSystem";

        public override Version Version { get; } = new(2, 0, 0);

        public override Version RequiredExiledVersion { get; } = new(9, 5, 1);

        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            base.OnDisabled();
        }

        public static IEnumerator<float> SendPostMessage(WWWForm content, string url)
        {
            using UnityWebRequest webRequest = UnityWebRequest.Post(url, content);


            yield return Timing.WaitUntilDone(webRequest.SendWebRequest());

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Log.Error(
                    $"An error occurred while sending http request: [{webRequest.responseCode}] => {webRequest.error}");
            }
            else
            {
                Log.Debug(webRequest.downloadHandler.text);
            }
        }

        public static IEnumerator<float> SendGetMessage(string url, Action<List<Warn>> callback)
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(url);

            yield return Timing.WaitUntilDone(webRequest.SendWebRequest());

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Log.Error(
                    $"An error occurred while sending http request: [{webRequest.responseCode}] => {webRequest.error}");
            }
            else
            {
                callback?.Invoke(JsonConvert.DeserializeObject<List<Warn>>(webRequest.downloadHandler.text));
                Log.Debug(webRequest.downloadHandler.text);
            }
        }
    }
}