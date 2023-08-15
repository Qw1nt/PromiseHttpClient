using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace HttpClient.Promise
{
    public class PromiseUnityWebRequestDefault : PromiseUnityWebRequestBase
    {
        private readonly Action _callback;

        public PromiseUnityWebRequestDefault(UnityWebRequest webRequest,  Action callback) : base(webRequest)
        {
            _callback = callback;
        }

        public override async UniTask<PromiseUnityWebRequestBase> SendAsync()
        {
            try
            {
                await SendInternalAsync();
                _callback?.Invoke();
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