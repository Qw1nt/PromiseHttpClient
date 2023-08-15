using UnityEngine;

namespace HttpClient.Core
{
    [CreateAssetMenu(menuName = "Http Client/New Configuration", fileName = Config.ConfigDataPath, order = 0)]
    public class HttpConfigurationData : ScriptableObject
    {
        [SerializeField] private string domain;

        public string Domain => domain;
    }
}