using UnityEngine;


namespace Diepio
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        private static bool isQuitting;

        public static T Instance
        {
            get
            {
                if (isQuitting)
                {
                    return null;  // OnDestroy,OndiseableéûÇ…êVÇµÇ≠çÏÇÁÇÍÇÈÇÃÇñhÇÆÇΩÇﬂ
                }

                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name);
                        instance = go.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }

        }

        protected virtual void OnApplicationQuit()
        {
            isQuitting = true;

        }
    }
}
