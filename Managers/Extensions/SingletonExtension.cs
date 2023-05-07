using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace SirenixPowered.ExtensionMethods
{
	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		protected static bool Quitting { get; private set; }

		private static readonly object Lock = new object();
		private static Dictionary<System.Type, Singleton<T>> _instances;

		public static T Instance
		{
			get
			{
				if (Quitting.IsFalse())
				{
					return null;
				}
				lock (Lock)
				{
					if (_instances == null)
					{
                        _instances = new Dictionary<System.Type, Singleton<T>>();
                    }	

					if (_instances.ContainsKey(typeof(T)))
					{
                        return (T)_instances[typeof(T)];
                    }
					else
					{
                        return null;
                    }	
				}
			}
		}

		private void OnEnable()
		{
			if (Quitting.IsFalse())
			{
				bool isSingleton = false;

				lock (Lock)
				{
					if (_instances == null)
					{
                        _instances = new Dictionary<System.Type, Singleton<T>>();
                    }	

					if (_instances.ContainsKey(this.GetType()))
					{
                        Destroy(this.gameObject);
                    }					
					else
					{
                        isSingleton = true;

						_instances.Add(this.GetType(), this);

						if (DontDestroyOnLoad() == true)
						{
                            transform.SetParent(null);
                            DontDestroyOnLoad(gameObject);
                        }					
					}
				}

				if (isSingleton)
				{
                    OnEnableCallback();
                }	
			}
		}

        private void OnDisable()
        {
            if(Application.isPlaying == true)
			{
				Quitting = false;
                _instances.Remove(this.GetType());
            }

			OnDisableCallback();
        }

        private void OnApplicationQuit()
		{
			Quitting = true;

			OnApplicationQuitCallback();
		}

		protected virtual void OnApplicationQuitCallback() { }

		protected virtual void OnEnableCallback() { }

        protected virtual void OnDisableCallback() { }

        protected abstract bool DontDestroyOnLoad();
    }

	public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				_instance = Resources.Load<T>(typeof(T).Name.ToString());

				if (_instance == null)
				{
					CreateScriptableObject();

					_instance = Resources.Load<T>(typeof(T).Name.ToString());

					(_instance as ScriptableSingleton<T>).OnInitialize();
				}

				void CreateScriptableObject()
				{
#if UNITY_EDITOR
					string directory = $"{Application.dataPath}/Resources";

					if (Directory.Exists(directory) == false)
					{
						Directory.CreateDirectory(directory);
						UnityEditor.AssetDatabase.Refresh();
					}

					ScriptableObjectExtension.CreateAsset(typeof(T).Name.ToString(), "Resources", typeof(T).Name.ToString());
					UnityEditor.AssetDatabase.Refresh();
#endif
				}

				return _instance;
			}
		}

		protected virtual void OnInitialize() { }
	}
}