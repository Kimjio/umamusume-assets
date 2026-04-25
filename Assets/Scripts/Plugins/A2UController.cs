using System;
using System.Collections.Generic;
using UnityEngine;

public class A2UController : MonoBehaviour
{
	public A2UController.HashNode[] a2uNodes
	{
		get
		{
			return this._a2uNodes;
		}
	}

	private void Update()
	{
		if (this._a2uNodes == null)
		{
			return;
		}
		int i = 0;
		int num = this._a2uNodes.Length;
		while (i < num)
		{
			if (this._a2uNodes[i]._isEnabled)
			{
				float duration = this._flicker.Duration;
				int num2 = 0;
				List<A2UController.SpritePair> spritePairs = this._a2uNodes[i]._spritePairs;
				int j = 0;
				int count = spritePairs.Count;
				while (j < count)
				{
					this.DoUpdateFrick(duration, i, num2);
					this.DoPreUpdateSpriteColor(spritePairs[j]._first, i, num2);
					this.DoPreUpdateSpriteColor(spritePairs[j]._second, i, num2);
					j++;
					num2++;
				}
				List<SpriteRenderer> sprites = this._a2uNodes[i]._sprites;
				int k = 0;
				int count2 = sprites.Count;
				while (k < count2)
				{
					this.DoUpdateFrick(duration, i, num2);
					this.DoPreUpdateSpriteColor(sprites[k], i, num2);
					k++;
					num2++;
				}
			}
			i++;
		}
	}

	private void LateUpdate()
	{
		if (this._a2uNodes == null)
		{
			return;
		}
		uint num = 0U;
		uint num2 = (uint)this._a2uNodes.Length;
		while (num < num2)
		{
			if (this._a2uNodes[(int)num]._isEnabled)
			{
				this.DoLateUpdateSprites(num);
			}
			num += 1U;
		}
	}

	public void Init(ref A2UController.InitContext context)
	{
		int num = context.prefabs.Length;
		A2U.Loader loader = A2U.loader;
		A2UController.GameObjectDesc[] gameObjecs = context.gameObjecs;
		int num2 = gameObjecs.Length;
		for (int i = 0; i < num2; i++)
		{
			A2UController.GameObjectDesc gameObjectDesc = gameObjecs[i];
			GameObject gameObject = loader.InstanciateGameObject(gameObjectDesc.prefabPath);
			if (gameObject != null)
			{
				gameObject.name = gameObjectDesc.name;
				GameObject gameObject2 = new GameObject();
				gameObject2.name = gameObjectDesc.name + "_root";
				gameObject.transform.SetParent(gameObject2.transform);
				gameObject2.transform.SetParent(base.transform);
				A2UUtil.SetLayer(30, gameObject2.transform);
			}
		}
		this.InitializeTexture(ref context);
		this.InitializeWork(ref context);
	}

	private void InitializeTexture(ref A2UController.InitContext context)
	{
		this._spriteList = A2U.loader.LoadSprites(context.texturePathList, context.multiSpritePathList);
	}

	private void InitializeWork(ref A2UController.InitContext context)
	{
		int childCount = base.transform.childCount;
		if (childCount == 0)
		{
			return;
		}
		this._a2uNodes = new A2UController.HashNode[childCount];
		int num = 0;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
			A2UController.HashNode hashNode = A2UController.MakeHashNode(gameObject, ref num);
			this._a2uNodes[i] = hashNode;
			A2UController.StopAnimation(gameObject);
		}
		this._appearance.Init(num);
		this.DoSetupFricker(context.flickRandomSeed, Math.Max(context.flickCount, 0U), Mathf.Max(context.flickStepSec, 0f), Math.Max(context.flickMin, 0U), Math.Max(context.flickMax, 0U));
	}

	public virtual void Final()
	{
	}

	public void UpdateComposition(int nameHash, ref A2UController.UpdateContext context)
	{
		int num = this.FindNodeIndex(nameHash);
		if (this._a2uNodes.Length <= num)
		{
			return;
		}
		GameObject composition = this._a2uNodes[num]._composition;
		this.DoUpdateCompositionTransform(composition.transform.parent.gameObject, context.position, context.rotationZ, context.scale);
		uint maxFrame = this._a2uNodes[num]._maxFrame;
		float num2 = Mathf.Max(context.startSec, 0f) % maxFrame;
		float num3 = Mathf.Max(0f, context.speed);
		this._a2uNodes[num]._desc._startSec = num2;
		this._a2uNodes[num]._desc._speed = num3;
		if (this._a2uNodes[num]._isEnabled != context.enable)
		{
			if (context.enable)
			{
				A2UController.StartAnimation(composition, num2);
				this.DoSetupEnabled(num, true);
			}
			else
			{
				A2UController.StopAnimation(composition);
				this.DoSetupEnabled(num, false);
			}
		}
		this._a2uNodes[num]._isEnabled = context.enable;
		if (!context.enable)
		{
			return;
		}
		A2UController.SetAnimationSpeed(composition, num3);
		A2UController.NodeDesc nodeDesc = default(A2UController.NodeDesc);
		nodeDesc.Init();
		nodeDesc._color = context.spriteColor;
		nodeDesc._opacity = (byte)Mathf.FloorToInt(Mathf.Max(new float[] { context.spriteOpacity }) * 100f + 0.5f);
		nodeDesc._useFlicker = context.isFlick;
		Sprite sprite = null;
		if (this._spriteList != null && (ulong)context.textureIndex < (ulong)((long)this._spriteList.Count))
		{
			sprite = this._spriteList[(int)context.textureIndex];
		}
		this.DoUpdateCompositionParameter(num, sprite, ref nodeDesc, Mathf.Max(0f, context.spriteScale), context.appearanceRandomSeed, context.spriteAppearance, context.slopeRandomSeed, context.spriteMinSlope, context.spriteMaxSlope);
	}

	public int FindNodeIndex(int compositionNameHash)
	{
		int num = this._a2uNodes.Length;
		for (int i = 0; i < num; i++)
		{
			if (compositionNameHash == this._a2uNodes[i]._nameHash)
			{
				return i;
			}
		}
		return num;
	}

	private void DoSetupFricker(int randomSeed, uint flickCount, float stepSec, uint min, uint max)
	{
		this._flicker.Generate(randomSeed, flickCount, stepSec, min, max);
	}

	public bool DoUpdateCompositionTransform(int namehash, Vector2 pos, float rotZ, Vector2 scale)
	{
		int num = this.FindNodeIndex(namehash);
		GameObject composition = this._a2uNodes[num]._composition;
		if (null == composition)
		{
			return false;
		}
		this.DoUpdateCompositionTransform(composition, pos, rotZ, scale);
		return true;
	}

	private void DoUpdateCompositionTransform(GameObject composition, Vector2 pos, float rotZ, Vector2 scale)
	{
		composition.transform.localPosition = new Vector3(pos.x, pos.y, 0f);
		composition.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rotZ));
		composition.transform.localScale = new Vector3(scale.x, scale.y, 0f);
	}

	public void DoUpdateCompositionParameter(int nodeIndex, Sprite texture, ref A2UController.NodeDesc desc, float scale, int appearanceRandomSeed, float appearanceCount, int slopeSeed, float minSlope, float maxSlope)
	{
		List<A2UController.SpritePair> spritePairs = this._a2uNodes[nodeIndex]._spritePairs;
		List<SpriteRenderer> sprites = this._a2uNodes[nodeIndex]._sprites;
		int num = spritePairs.Count + sprites.Count;
		uint num2 = (uint)Mathf.FloorToInt((float)num * Mathf.Clamp(appearanceCount, 0f, 1f) + 0.5f);
		this._appearance.Generate(appearanceRandomSeed, (uint)num, num2);
		this._a2uNodes[nodeIndex]._desc._opacity = desc._opacity;
		this._a2uNodes[nodeIndex]._desc._color = desc._color;
		this._a2uNodes[nodeIndex]._desc._useFlicker = desc._useFlicker;
		this.DoUpdateSprites(nodeIndex, texture, scale, slopeSeed, minSlope, maxSlope, this._a2uNodes[nodeIndex]._isEnabled);
	}

	private void DoUpdateSprites(int nodeIndex, Sprite texture, float scale, int slopeSeed, float minSlope, float maxSlope, bool isEnabled)
	{
		this._random.Begin(slopeSeed);
		float num = Mathf.Clamp(minSlope, -180f, 180f);
		float num2 = Mathf.Clamp(maxSlope, -180f, 180f);
		int num3 = 0;
		List<A2UController.SpritePair> spritePairs = this._a2uNodes[nodeIndex]._spritePairs;
		int i = 0;
		int count = spritePairs.Count;
		while (i < count)
		{
			A2UController.SpritePair spritePair = spritePairs[i];
			float num4 = this._random.Get(num, num2);
			bool flag = this._appearance.IsAppeared(num3);
			float originalScale = this._a2uNodes[nodeIndex]._work[num3]._originalScale;
			this.DoUpdateSprite(spritePair._first, texture, originalScale * scale, num4, flag, isEnabled);
			this.DoUpdateSprite(spritePair._second, texture, originalScale * scale, num4, flag, isEnabled);
			i++;
			num3++;
		}
		List<SpriteRenderer> sprites = this._a2uNodes[nodeIndex]._sprites;
		int j = 0;
		int count2 = sprites.Count;
		while (j < count2)
		{
			SpriteRenderer spriteRenderer = sprites[j];
			float num5 = this._random.Get(num, num2);
			bool flag2 = this._appearance.IsAppeared(num3);
			float originalScale2 = this._a2uNodes[nodeIndex]._work[num3]._originalScale;
			this.DoUpdateSprite(spriteRenderer, texture, originalScale2 * scale, num5, flag2, isEnabled);
			j++;
			num3++;
		}
		this._random.End();
	}

	private void DoUpdateSprite(SpriteRenderer sprite, Sprite texture, float scale, float rotZ, bool isAppearance, bool isEnabled)
	{
		if (sprite == null)
		{
			return;
		}
		if (null != texture)
		{
			sprite.sprite = texture;
		}
		sprite.transform.localScale = new Vector3(scale, scale, 1f);
		sprite.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
		sprite.enabled = isAppearance && isEnabled;
	}

	private void DoUpdateFrick(float duration, int nodeIndex, int workIndex)
	{
		if (this._a2uNodes[nodeIndex]._desc._useFlicker)
		{
			float num = this._a2uNodes[nodeIndex]._work[workIndex]._flickSec + Time.deltaTime;
			if (num > duration)
			{
				num -= duration;
			}
			this._a2uNodes[nodeIndex]._work[workIndex]._flickSec = num;
		}
	}

	private void DoPreUpdateSpriteColor(SpriteRenderer sprite, int nodeIndex, int workIndex)
	{
		if (sprite == null)
		{
			return;
		}
		Color color = sprite.color;
		this._a2uNodes[nodeIndex]._work[workIndex]._prevAlpha = color.a;
		color.a = -1f;
		sprite.color = color;
	}

	private void DoLateUpdateSprites(uint nodeIndex)
	{
		Color color = this._a2uNodes[(int)nodeIndex]._desc._color;
		int num = 0;
		List<A2UController.SpritePair> spritePairs = this._a2uNodes[(int)nodeIndex]._spritePairs;
		int i = 0;
		int count = spritePairs.Count;
		while (i < count)
		{
			A2UController.SpritePair spritePair = spritePairs[i];
			this.DoLateUpdateSprite(spritePair._first, nodeIndex, num, color);
			this.DoLateUpdateSprite(spritePair._second, nodeIndex, num, color);
			i++;
			num++;
		}
		List<SpriteRenderer> sprites = this._a2uNodes[(int)nodeIndex]._sprites;
		int j = 0;
		int count2 = sprites.Count;
		while (j < count2)
		{
			this.DoLateUpdateSprite(sprites[j], nodeIndex, num, color);
			j++;
			num++;
		}
	}

	private void DoLateUpdateSprite(SpriteRenderer sprite, uint nodeIndex, int workIndex, Color color)
	{
		if (sprite == null)
		{
			return;
		}
		float num = 1f;
		if (this._a2uNodes[(int)nodeIndex]._desc._useFlicker)
		{
			num = this._flicker.GetValue(this._a2uNodes[(int)nodeIndex]._work[workIndex]._flickSec);
		}
		Color color2 = color;
		if (sprite.color.a < 0f)
		{
			color2.a = this._a2uNodes[(int)nodeIndex]._work[workIndex]._prevAlpha;
		}
		else
		{
			color2.a = sprite.color.a * ((float)this._a2uNodes[(int)nodeIndex]._desc._opacity * 0.01f);
		}
		color2.a *= num;
		sprite.color = color2;
	}

	private void DoSetupEnabled(int index, bool isEnabled)
	{
		float startSec = this._a2uNodes[index]._desc._startSec;
		float step = this._flicker.step;
		int count = this._flicker.Count;
		int num = 0;
		List<A2UController.SpritePair> spritePairs = this._a2uNodes[index]._spritePairs;
		int i = 0;
		int count2 = spritePairs.Count;
		while (i < count2)
		{
			A2UController.SpritePair spritePair = spritePairs[i];
			float flickSec = this.GetFlickSec(startSec, step, num, count);
			this.DoSetupEnabledImpl(spritePair._first, index, num, isEnabled, flickSec);
			this.DoSetupEnabledImpl(spritePair._second, index, num, isEnabled, flickSec);
			i++;
			num++;
		}
		List<SpriteRenderer> sprites = this._a2uNodes[index]._sprites;
		int j = 0;
		int count3 = sprites.Count;
		while (j < count3)
		{
			float flickSec2 = this.GetFlickSec(startSec, step, num, count);
			this.DoSetupEnabledImpl(sprites[j], index, num, isEnabled, flickSec2);
			j++;
			num++;
		}
	}

	private void DoSetupEnabledImpl(SpriteRenderer sprite, int nodeIndex, int workIndex, bool isEnabled, float flickSec)
	{
		if (sprite == null)
		{
			return;
		}
		sprite.enabled = isEnabled;
		this._a2uNodes[nodeIndex]._work[workIndex]._flickSec = flickSec;
	}

	private float GetFlickSec(float startSec, float step, int workIndex, int flickCount)
	{
		float num = startSec + step * (float)workIndex;
		float num2 = num / step;
		int num3 = (int)num2 % (flickCount - 1);
		float num4 = num2 - (float)((int)num2);
		return this._flicker.NormalizeSec(num + ((float)num3 + num4) * step);
	}

	private static Vector2 DoFindAddSpriteRendererByDepthFirst(GameObject go, List<SpriteRenderer> outList, List<int> outIds)
	{
		Vector3 vector = new Vector3(0f, 0f, 0f);
		int childCount = go.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = go.transform.GetChild(i).gameObject;
			int nameNumber = A2UController.GetNameNumber(gameObject.name);
			A2UController.DoFindAddSpriteRendererByDepthFirstImpl(gameObject, outList, outIds, nameNumber);
			vector += gameObject.transform.position;
		}
		return vector / (float)childCount;
	}

	private static void DoFindAddSpriteRendererByDepthFirstImpl(GameObject go, List<SpriteRenderer> outList, List<int> outIds, int number)
	{
		int childCount = go.transform.childCount;
		if (childCount > 0)
		{
			for (int i = 0; i < childCount; i++)
			{
				A2UController.DoFindAddSpriteRendererByDepthFirstImpl(go.transform.GetChild(i).gameObject, outList, outIds, number);
			}
			return;
		}
		SpriteRenderer[] components = go.GetComponents<SpriteRenderer>();
		int j = 0;
		int num = components.Length;
		while (j < num)
		{
			outList.Add(components[j]);
			outIds.Add(number);
			j++;
		}
	}

	private static int GetNameNumber(string name)
	{
		int num = name.IndexOf("Pair");
		if (num < 0)
		{
			return -1;
		}
		num += "Pair".Length;
		int num2 = name.IndexOf("_L");
		if (num2 < 0)
		{
			num2 = name.Length;
		}
		num2 -= num;
		string text = name.Substring(num, num2);
		int num3 = -1;
		if (!int.TryParse(text, out num3))
		{
			return -1;
		}
		return num3;
	}

	private static void SetSpritePairs(List<A2UController.SpritePair> outPiars, List<SpriteRenderer> outSprites, List<SpriteRenderer> sprites, List<int> ids)
	{
		int i = 0;
		int count = ids.Count;
		while (i < count)
		{
			int num = ids[i];
			SpriteRenderer spriteRenderer = sprites[i];
			if (num < 0)
			{
				outSprites.Add(spriteRenderer);
			}
			else
			{
				int num2 = num;
				int j = outPiars.Count;
				int num3 = num2 + 1;
				while (j < num3)
				{
					outPiars.Add(new A2UController.SpritePair());
					j++;
				}
				if (null == outPiars[num2]._first)
				{
					outPiars[num2]._first = spriteRenderer;
				}
				else if (null == outPiars[num2]._second)
				{
					outPiars[num2]._second = spriteRenderer;
				}
			}
			i++;
		}
	}

	private static void GetAnimationInfo(ref A2UController.HashNode node, GameObject composition)
	{
		AnimationClip animationClip = composition.GetComponent<Animator>().runtimeAnimatorController.animationClips[0];
		float frameRate = animationClip.frameRate;
		float length = animationClip.length;
		node._frameRate = frameRate;
		node._maxFrame = (uint)Mathf.CeilToInt(length * frameRate);
	}

	private static void StartAnimation(GameObject composition, float sec = 0f)
	{
		Animator component = composition.GetComponent<Animator>();
		component.enabled = true;
		A2UController.DoStartAnimation(component, sec);
	}

	private static void DoStartAnimation(Animator animator, float sec)
	{
		animator.PlayInFixedTime(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, sec);
	}

	private static void StopAnimation(GameObject composition)
	{
		composition.GetComponent<Animator>().enabled = false;
	}

	private static void SetAnimationSec(GameObject composition, float sec, bool isPause = false)
	{
		Animator component = composition.GetComponent<Animator>();
		A2UController.DoStartAnimation(component, sec);
		A2UController.DoSetAnimationPause(component, isPause);
	}

	private static void DoSetAnimationPause(Animator animator, bool isPause)
	{
		animator.speed = (isPause ? 0f : 1f);
	}

	private static void SetAnimationSpeed(GameObject composition, float speed)
	{
		A2UController.DoSetAnimationSpeed(composition.GetComponent<Animator>(), speed);
	}

	private static void DoSetAnimationSpeed(Animator animator, float speed)
	{
		animator.speed = speed;
	}

	private static A2UController.HashNode MakeHashNode(GameObject target, ref int maxCount)
	{
		A2UController.HashNode hashNode = default(A2UController.HashNode);
		hashNode.Init(target);
		A2UController.GetAnimationInfo(ref hashNode, target);
		List<SpriteRenderer> list = new List<SpriteRenderer>();
		List<int> list2 = new List<int>();
		Vector2 vector = A2UController.DoFindAddSpriteRendererByDepthFirst(target, list, list2);
		A2UController.SetSpritePairs(hashNode._spritePairs, hashNode._sprites, list, list2);
		target.transform.localPosition = -vector;
		int num = hashNode._spritePairs.Count + hashNode._sprites.Count;
		if (num > 0)
		{
			if (maxCount < num)
			{
				maxCount = num;
			}
			if (hashNode._spritePairs.Count > 0)
			{
				hashNode._desc._color = hashNode._spritePairs[0]._first.color;
			}
			else
			{
				hashNode._desc._color = hashNode._sprites[0].color;
			}
			hashNode._work = new A2UController.NodeWork[num];
			for (int i = 0; i < num; i++)
			{
				hashNode._work[i].Init();
				if (i < hashNode._spritePairs.Count)
				{
					hashNode._work[i]._originalScale = hashNode._spritePairs[i]._first.transform.localScale.x;
				}
				else
				{
					hashNode._work[i]._originalScale = hashNode._sprites[i - hashNode._spritePairs.Count].transform.localScale.x;
				}
			}
		}
		return hashNode;
	}

	protected A2UController.HashNode[] _a2uNodes = new A2UController.HashNode[0];

	protected List<Sprite> _spriteList;

	protected A2U.Appearance _appearance = new A2U.Appearance();

	protected A2U.Flicker _flicker = new A2U.Flicker();

	protected A2U.Random _random = new A2U.Random();

	public struct NodeDesc
	{
		public void Init()
		{
			this._color = Color.white;
			this._startSec = 0f;
			this._speed = 1f;
			this._opacity = 100;
			this._useFlicker = true;
		}

		public Color _color;

		public float _startSec;

		public float _speed;

		public byte _opacity;

		public bool _useFlicker;
	}

	public struct NodeWork
	{
		public void Init()
		{
			this._flickSec = 0f;
			this._prevAlpha = 0f;
			this._originalScale = 1f;
		}

		public float _flickSec;

		public float _prevAlpha;

		public float _originalScale;
	}

	public class SpritePair
	{
		public SpritePair()
		{
			this._first = null;
			this._second = null;
		}

		public SpriteRenderer _first;

		public SpriteRenderer _second;
	}

	public struct HashNode
	{
		public void Init(GameObject composition)
		{
			this._composition = composition;
			this._nameHash = A2UUtil.FNVHash.Generate(composition.name);
			this._spritePairs = new List<A2UController.SpritePair>();
			this._sprites = new List<SpriteRenderer>();
			this._frameRate = 30f;
			this._maxFrame = 0U;
			this._desc = default(A2UController.NodeDesc);
			this._desc.Init();
			this._isEnabled = false;
			this._work = null;
		}

		public GameObject _composition;

		public List<A2UController.SpritePair> _spritePairs;

		public List<SpriteRenderer> _sprites;

		public int _nameHash;

		public float _frameRate;

		public uint _maxFrame;

		public A2UController.NodeDesc _desc;

		public bool _isEnabled;

		public A2UController.NodeWork[] _work;
	}

	public struct PrefabDesc
	{
		public string name;

		public string path;

		public GameObject prefab;
	}

	public struct GameObjectDesc
	{
		public string name;

		public string prefabPath;
	}

	public struct InitContext
	{
		public int flickRandomSeed;

		public uint flickCount;

		public float flickStepSec;

		public uint flickMin;

		public uint flickMax;

		public string[] texturePathList;

		public string[] multiSpritePathList;

		public A2UController.PrefabDesc[] prefabs;

		public A2UController.GameObjectDesc[] gameObjecs;
	}

	public struct UpdateContext
	{
		public Color spriteColor;

		public Vector2 position;

		public Vector2 scale;

		public float rotationZ;

		public uint textureIndex;

		public int appearanceRandomSeed;

		public float spriteAppearance;

		public int slopeRandomSeed;

		public float spriteMinSlope;

		public float spriteMaxSlope;

		public float spriteScale;

		public float spriteOpacity;

		public float startSec;

		public float speed;

		public bool isFlick;

		public bool enable;
	}
}
