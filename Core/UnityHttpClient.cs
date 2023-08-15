using System;
using Cysharp.Threading.Tasks;
using HttpClient.Promise;
using UnityEngine.Networking;

namespace HttpClient.Core
{
    public static class UnityHttpClient
    {
        public static UnityWebRequest Request(string endpoint)
        {
            var request = new UnityWebRequest(HttpTools.BuildUri(endpoint));
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }

        public static UnityWebRequest Get(string endpoint)
        {
            return Request(endpoint).Get();
        }

        public static UnityWebRequest Post(string endpoint, object payload)
        {
            return Request(endpoint).Post(payload);
        }

        public static UnityWebRequest Put(string endpoint, object payload)
        {
            return Request(endpoint).Put(payload);
        }

        public static UnityWebRequest Get(this UnityWebRequest webRequest)
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.uploadHandler = null;
            webRequest.method = "GET";
            return webRequest;
        }

        public static UnityWebRequest Post(this UnityWebRequest webRequest, object payload)
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.uploadHandler = new UploadHandlerRaw(payload?.ToJsonBody());
            webRequest.method = "POST";
            return webRequest;
        }

        public static UnityWebRequest Put(this UnityWebRequest webRequest, object payload)
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.uploadHandler = new UploadHandlerRaw(payload?.ToJsonBody());
            webRequest.method = "PUT";
            return webRequest;
        }

        public static UnityWebRequest Delete(this UnityWebRequest webRequest)
        {
            webRequest.downloadHandler = null;
            webRequest.uploadHandler = null;
            webRequest.method = "DELETE";
            return webRequest;
        }

        public static PromiseUnityWebRequestBase Then(this UnityWebRequest webRequest, Action callback)
        {
            PromiseUnityWebRequestDefault promiseUnityWebRequestDefaultBase = new(webRequest, callback);
            return promiseUnityWebRequestDefaultBase;
        }

        public static PromiseUnityWebRequest<T> Then<T>(this UnityWebRequest webRequest, Action<T> callback)
        {
            PromiseUnityWebRequest<T> promiseUnityWebRequest = new(webRequest, callback);
            return promiseUnityWebRequest;
        }

        public static PromiseUnityWebRequestBase Catch(this PromiseUnityWebRequestBase webRequest,
            Action<UnityWebRequestException> callback)
        {
            webRequest.SetFailureCallback(callback);
            return webRequest;
        }

        public static PromiseUnityWebRequestBase Finally(this PromiseUnityWebRequestBase webRequest, Action callback)
        {
            webRequest.SetFinally(callback);
            return webRequest;
        }

        public static async UniTask<UnityWebRequest> SendAsync(this UnityWebRequest webRequest)
        {
            return await webRequest.SendWebRequest();
        }
    }
}