using System;
using UnityEngine;

namespace Systems
{
    public class UnityEventsListener : MonoBehaviour
    {
        public static event Action ApplicationFixedUpdate;
        public static event Action ApplicationUpdate;
        public static event Action ApplicationLateUpdate;

        private static UnityEventsListener _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BeforeSceneLoad()
        {
            if (_instance != null)
                return;

            var go = new GameObject("UnityEventsListener");
            _instance = go.AddComponent<UnityEventsListener>();

            DontDestroyOnLoad(go);
        }

        private void FixedUpdate()
        {
            ApplicationFixedUpdate?.Invoke();
        }

        private void Update()
        {
            ApplicationUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            ApplicationLateUpdate?.Invoke();
        }
    }
}