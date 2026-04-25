using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnimateToUnity
{
	public abstract class AnMonoSingleton<T> : MonoBehaviour where T : AnMonoSingleton<T>
	{
		public static T Instance
		{
			get
			{
				if (AnMonoSingleton<T>._instance != null)
				{
					return AnMonoSingleton<T>._instance;
				}
				AnMonoSingleton<T>._instance = global::UnityEngine.Object.FindObjectOfType(typeof(T)) as T;
				if (AnMonoSingleton<T>._instance == null)
				{
					if (!AnMonoSingleton<T>._isQuitting)
					{
						GameObject gameObject = new GameObject();
						gameObject.AddComponent<T>();
						AnMonoSingleton<T>._instance = gameObject.GetComponent<T>();
						AnMonoSingleton<T>._instance._OnInitialize();
					}
				}
				else
				{
					AnMonoSingleton<T>._Initialize(AnMonoSingleton<T>._instance);
				}
				return AnMonoSingleton<T>._instance;
			}
		}

		public static bool HasInstance()
		{
			return AnMonoSingleton<T>._instance != null;
		}

		private void Awake()
		{
			AnMonoSingleton<T>._Initialize(this as T);
		}

		private void OnDestroy()
		{
			AnMonoSingleton<T>._Release(this as T);
		}

		private void SceneManagerSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			AnMonoSingleton<T>._Loaded(this as T);
		}

		private void OnApplicationQuit()
		{
			AnMonoSingleton<T>._isQuitting = true;
			AnMonoSingleton<T>._Release(this as T);
		}

		private static void _Initialize(T instance)
		{
			if (AnMonoSingleton<T>._instance == null)
			{
				AnMonoSingleton<T>._instance = instance;
				AnMonoSingleton<T>._instance._OnInitialize();
				return;
			}
			if (AnMonoSingleton<T>._instance != instance)
			{
				global::UnityEngine.Object.Destroy(instance.gameObject);
			}
		}

		private static void _Release(T instance)
		{
			if (AnMonoSingleton<T>._instance == instance)
			{
				AnMonoSingleton<T>._instance._OnFinalize();
				AnMonoSingleton<T>._instance = default(T);
			}
		}

		private static void _Loaded(T instance)
		{
			if (AnMonoSingleton<T>._instance == instance)
			{
				AnMonoSingleton<T>._instance._OnLoaded();
			}
		}

		public virtual void _OnInitialize()
		{
			SceneManager.sceneLoaded += this.SceneManagerSceneLoaded;
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public virtual void _OnFinalize()
		{
		}

		public virtual void _OnLoaded()
		{
		}

		public virtual void _Boot()
		{
		}

		private static T _instance;

		private static bool _isQuitting;
	}
}
