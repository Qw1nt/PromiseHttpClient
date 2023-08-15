using UnityEngine;

namespace HttpClient.Core
{
    [CreateAssetMenu]
    public class UnityHttpClientConfiguration : ScriptableObject
    {
        [SerializeField] private string domain;

        public string Domain => domain;
    }
}