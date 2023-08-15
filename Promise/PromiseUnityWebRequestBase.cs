using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace HttpClient.Promise
{
    public abstract class PromiseUnityWebRequestBase : IDisposable
    {
        private readonly UnityWebRequest _webRequest;
        protected Action<UnityWebRequestException> _catch;
        protected Action _finally;

        protected PromiseUnityWebRequestBase(UnityWebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public bool AutoDispose { get; private set; } = true;

        public void SetFailureCallback(Action<UnityWebRequestException> onFailure) => _catch = onFailure;

        public void SetFinally(Action onFinally) => _finally = onFinally;
        
        protected async UniTask<string> SendInternalAsync()
        {
            var response = await _webRequest.SendWebRequest();
            var json = response.downloadHandler.text;
            return json;
        }

        public PromiseUnityWebRequestBase DisableAutoDispose()
        {
            AutoDispose = true;
            return this;
        }

        public abstract UniTask<PromiseUnityWebRequestBase> SendAsync();

        public void Dispose()
        {
            _webRequest?.Dispose();
        }
    }
}