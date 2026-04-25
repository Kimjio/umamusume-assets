using System;
using System.Collections.Generic;
using UnityEngine;

public static class A2U
{
	public static A2UManager manager
	{
		get
		{
			return A2U._manager;
		}
	}

	public static T GetManager<T>() where T : A2UManager
	{
		return (T)((object)A2U._manager);
	}

	public static A2UCamera camera
	{
		get
		{
			return A2U._camera;
		}
	}

	public static A2U.Loader loader
	{
		get
		{
			return A2U._loader;
		}
	}

	public static A2UManager InitLive(GameObject parent, int screenWidth, int screenHeight, string a2uRootPrefabPath, A2U.Loader.ResourceItem[] resourceInfo)
	{
		A2U._loader = new A2U.Loader();
		A2U._loader.AddResourceInfo(resourceInfo);
		GameObject gameObject = A2U._loader.InstanciateManager(a2uRootPrefabPath);
		gameObject.transform.SetParent(parent.transform);
		A2U._manager = gameObject.GetComponent<A2UManager>();
		A2U._manager.InitCamera(screenWidth, screenHeight);
		A2U._camera = A2U._manager.a2uCamera;
		return A2U._manager;
	}

	public static A2UManager InitCutIn(GameObject parent, int screenWidth, int screenHeight, GameObject a2uRootPrefab)
	{
		A2U._loader = new A2U.Loader();
		GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(a2uRootPrefab);
		gameObject.transform.SetParent(parent.transform);
		A2U._manager = gameObject.GetComponent<A2UManager>();
		A2U._manager.InitCamera(screenWidth, screenHeight);
		A2U._camera = A2U._manager.a2uCamera;
		return A2U._manager;
	}

	public static void Final()
	{
		if (null != A2U._manager)
		{
			A2U._manager.Final();
			global::UnityEngine.Object.Destroy(A2U._manager.gameObject);
			A2U._manager = null;
		}
		A2U._camera = null;
		A2U._loader = null;
	}

	public static bool IsEnabled
	{
		get
		{
			return null != A2U._camera && A2U._camera.a2uRenderer.isEnabled;
		}
		set
		{
			if (null != A2U._camera)
			{
				A2U._camera.a2uRenderer.isEnabled = value;
			}
		}
	}

	public static bool IsRenderingOrder(A2U.Order order)
	{
		return null != A2U._camera && order == A2U._camera.a2uRenderer.order;
	}

	public static void DoRenderImage(RenderTexture src, RenderTexture dst)
	{
		A2U._camera.a2uRenderer.DoRenderImage(src, dst);
	}

	public static void SetDimmerColor(Color color)
	{
		A2U._camera.a2uRenderer.dimmerColor = color;
	}

	public const string PAIR_BODY = "Pair";

	public const string PAIR_SUFFIX = "_L";

	private static A2UManager _manager;

	private static A2UCamera _camera;

	private static A2U.Loader _loader;

	public enum Blend
	{
		Normal,
		Add,
		Multiply,
		Screen,
		Overlay
	}

	public enum Order
	{
		PreImageEffect,
		InImageEffect,
		PostImageEffect
	}

	public class Appearance
	{
		public void Init(int count)
		{
			this._data = new bool[count];
		}

		public void Generate(int seed, uint maxCount, uint count)
		{
			global::UnityEngine.Random.State state = global::UnityEngine.Random.state;
			this.DoGenerate(seed, maxCount, count);
			global::UnityEngine.Random.state = state;
		}

		private void DoGenerate(int seed, uint maxCount, uint count)
		{
			global::UnityEngine.Random.InitState(seed);
			int num = 0;
			while ((long)num < (long)((ulong)maxCount))
			{
				this._data[num] = (long)num < (long)((ulong)count);
				num++;
			}
			int num2 = 0;
			while ((long)num2 < (long)((ulong)maxCount))
			{
				bool flag = this._data[num2];
				int num3 = (int)global::UnityEngine.Random.Range(0f, maxCount);
				this._data[num2] = this._data[num3];
				this._data[num3] = flag;
				num2++;
			}
		}

		public bool IsAppeared(int index)
		{
			return index < this._data.Length && this._data[index];
		}

		private bool[] _data;
	}

	public class Flicker
	{
		public int Count
		{
			get
			{
				return this._data.Length;
			}
		}

		public float Duration
		{
			get
			{
				return (float)this._data.Length * this._step;
			}
		}

		public float step
		{
			get
			{
				return this._step;
			}
		}

		public void Generate(int seed, uint count, float step, uint min, uint max)
		{
			global::UnityEngine.Random.State state = global::UnityEngine.Random.state;
			global::UnityEngine.Random.InitState(seed);
			this.DoGenerate(count, step, min, max);
			global::UnityEngine.Random.state = state;
		}

		private void DoGenerate(uint count, float step, uint min, uint max)
		{
			if (count < 0U)
			{
				count = 1U;
			}
			float[] array = new float[count];
			int num = (int)Math.Min(min, 100U);
			int num2 = (int)Math.Min(max, 100U);
			int num3 = 0;
			while ((long)num3 < (long)((ulong)count))
			{
				int num4 = global::UnityEngine.Random.Range(num, num2 + 1);
				array[num3] = (float)num4 * 0.01f;
				num3++;
			}
			this._step = step;
			this._data = array;
		}

		public float GetValue(float sec)
		{
			float num = sec / this._step;
			int num2 = (int)num % this.Count;
			int num3 = (num2 + 1) % this.Count;
			float num4 = num - (float)((int)num);
			return Mathf.Lerp(this._data[num2], this._data[num3], num4);
		}

		public float NormalizeSec(float sec)
		{
			float num = sec / this._step;
			int num2 = (int)num % this.Count;
			float num3 = num - (float)((int)num);
			return ((float)num2 + num3) * this._step;
		}

		private float[] _data;

		private float _step;
	}

	public class Loader
	{
		public void ClearResourceInfo()
		{
			this._resourceInfo.Clear();
		}

		public void AddResourceInfo(A2U.Loader.ResourceItem item)
		{
			this._resourceInfo.Add(item);
		}

		public void AddResourceInfo(A2U.Loader.ResourceItem[] item)
		{
			this._resourceInfo.AddRange(item);
		}

		public GameObject LoadGameObject(string path)
		{
			int count = this._resourceInfo.Count;
			int hashCode = path.GetHashCode();
			GameObject gameObject = null;
			for (int i = 0; i < count; i++)
			{
				A2U.Loader.ResourceItem resourceItem = this._resourceInfo[i];
				if (resourceItem._hash == hashCode)
				{
					gameObject = (GameObject)resourceItem._resource;
					break;
				}
			}
			return gameObject;
		}

		public Texture2D LoadTexture2D(string path)
		{
			int count = this._resourceInfo.Count;
			int hashCode = path.GetHashCode();
			Texture2D texture2D = null;
			for (int i = 0; i < count; i++)
			{
				A2U.Loader.ResourceItem resourceItem = this._resourceInfo[i];
				if (resourceItem._hash == hashCode)
				{
					texture2D = (Texture2D)resourceItem._resource;
					break;
				}
			}
			return texture2D;
		}

		public A2UMultiSprite LoadMultiSprite(string path)
		{
			int count = this._resourceInfo.Count;
			int hashCode = path.GetHashCode();
			A2UMultiSprite a2UMultiSprite = null;
			for (int i = 0; i < count; i++)
			{
				A2U.Loader.ResourceItem resourceItem = this._resourceInfo[i];
				if (resourceItem._hash == hashCode)
				{
					a2UMultiSprite = (A2UMultiSprite)resourceItem._resource;
					break;
				}
			}
			return a2UMultiSprite;
		}

		public GameObject InstanciateGameObject(string path)
		{
			return global::UnityEngine.Object.Instantiate<GameObject>(this.LoadGameObject(path));
		}

		public List<Sprite> LoadSprites(string[] path, string[] multiSpritePath)
		{
			List<Sprite> list = new List<Sprite>();
			int i = 0;
			int num = path.Length;
			while (i < num)
			{
				Texture2D texture2D = this.LoadTexture2D(path[i]);
				A2UMultiSprite a2UMultiSprite = this.LoadMultiSprite(multiSpritePath[i]);
				if (a2UMultiSprite != null && texture2D != null)
				{
					int num2 = a2UMultiSprite._spriteInfos.Length;
					int j = 0;
					int num3 = num2;
					while (j < num3)
					{
						A2UMultiSprite.SpriteInfo spriteInfo = a2UMultiSprite._spriteInfos[j];
						Sprite sprite = Sprite.Create(texture2D, spriteInfo.rect, spriteInfo.pivot, spriteInfo.pixelsToUnits, spriteInfo.extrude, spriteInfo.meshType, spriteInfo.border);
						sprite.name = spriteInfo.name;
						list.Add(sprite);
						j++;
					}
				}
				i++;
			}
			return list;
		}

		public GameObject InstanciateManager(string path)
		{
			return this.InstanciateGameObject(path);
		}

		public const string ROOT_PREFAB = "A2URoot";

		private List<A2U.Loader.ResourceItem> _resourceInfo = new List<A2U.Loader.ResourceItem>();

		public class ResourceItem
		{
			public ResourceItem(string path, global::UnityEngine.Object resource)
			{
				this._path = path;
				this._hash = path.GetHashCode();
				this._resource = resource;
			}

			public string _path;

			public int _hash;

			public global::UnityEngine.Object _resource;
		}
	}

	public class Random
	{
		public void Begin(int seed)
		{
			this._origin = global::UnityEngine.Random.state;
			global::UnityEngine.Random.InitState(seed);
		}

		public int Get(int min, int max)
		{
			return global::UnityEngine.Random.Range(min, max);
		}

		public float Get(float min, float max)
		{
			return global::UnityEngine.Random.Range(min, max);
		}

		public void End()
		{
			global::UnityEngine.Random.state = this._origin;
		}

		private global::UnityEngine.Random.State _origin;
	}

	public class Renderer
	{
		public bool isEnabled
		{
			get
			{
				return this._isEnabled && null != this._texture && null != this._material;
			}
			set
			{
				this._isEnabled = value;
			}
		}

		public Color dimmerColor
		{
			get
			{
				return this._dimmerColor;
			}
			set
			{
				this._dimmerColor = value;
			}
		}

		public A2U.Order order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		public void Init(RenderTexture texture, Material material)
		{
			this._texturePropertyId = Shader.PropertyToID("_ColorBuffer");
			this._dimmerPropertyId = Shader.PropertyToID("_DimmerColor");
			this._texture = texture;
			this._material = material;
		}

		public virtual void Final()
		{
			this._texture = null;
			this._material = null;
		}

		public void DoRenderImage(RenderTexture src, RenderTexture dst)
		{
			if (!this.isEnabled)
			{
				Graphics.Blit(src, dst);
				return;
			}
			this._material.SetTexture(this._texturePropertyId, this._texture);
			this._material.SetColor(this._dimmerPropertyId, this._dimmerColor);
			Graphics.Blit(src, dst, this._material, (int)this._blendPass);
		}

		public void SetBlendMode(A2U.Blend blendMode)
		{
			switch (blendMode)
			{
			case A2U.Blend.Normal:
				this._blendPass = A2U.Renderer.BlendPass.Normal;
				return;
			case A2U.Blend.Add:
				this._blendPass = A2U.Renderer.BlendPass.Add;
				return;
			case A2U.Blend.Multiply:
				this._blendPass = A2U.Renderer.BlendPass.Multiply;
				return;
			case A2U.Blend.Screen:
				this._blendPass = A2U.Renderer.BlendPass.Screen;
				return;
			case A2U.Blend.Overlay:
				this._blendPass = A2U.Renderer.BlendPass.Overlay;
				return;
			default:
				return;
			}
		}

		private RenderTexture _texture;

		private Material _material;

		private Color _dimmerColor = Color.white;

		private bool _isEnabled;

		private A2U.Renderer.BlendPass _blendPass;

		private A2U.Order _order = A2U.Order.PostImageEffect;

		private int _texturePropertyId;

		private int _dimmerPropertyId;

		private enum BlendPass
		{
			Normal,
			Add,
			Multiply,
			Screen,
			Overlay
		}
	}
}
