using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace HttpClient.Promise
{
    public class PromiseUnityWebRequest<T> : PromiseUnityWebRequestBase
    {
        private readonly Action<T> _onSuccess;

        public PromiseUnityWebRequest(UnityWebRequest webRequest, Action<T> onSuccess) : base(webRequest)
        {
            _onSuccess = onSuccess;
        }

        public override async UniTask<PromiseUnityWebRequestBase> SendAsync()
        {
            try
            {
                var json = await SendInternalAsync();
                var result = JsonConvert.DeserializeObject<T>(json);
                _onSuccess?.Invoke(result);
            }
            catch (UnityWebRequestException exception)
            {
                _catch?.Invoke(exception);
            }
            finally
            {
                _finally?.Invoke();
                if (AutoDispose == true)
                    Dispose();
            }

            return this;
        }
    }
}