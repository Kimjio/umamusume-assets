using System;
using UnityEngine;

public class A2UCamera : MonoBehaviour
{
	public RenderTexture renderTexture
	{
		get
		{
			return this._renderTexture;
		}
	}

	public A2U.Renderer a2uRenderer
	{
		get
		{
			return this._renderer;
		}
	}

	private void OnDestroy()
	{
		this.Final();
	}

	private void OnEnable()
	{
		this._renderer.isEnabled = true;
	}

	private void OnDisable()
	{
		this._renderer.isEnabled = false;
	}

	public void Init(int screenWidth, int screenHeight)
	{
		this._renderTexture = new RenderTexture(screenWidth, screenHeight, 0, RenderTextureFormat.ARGB32);
		if (!this._renderTexture.Create())
		{
			this._renderTexture.Release();
			this._renderTexture = null;
		}
		Camera component = base.GetComponent<Camera>();
		component.targetTexture = this._renderTexture;
		this._camera = component;
		if (this._camera != null)
		{
			this._camera.cullingMask = 1073741824;
		}
		this._renderer.Init(this._renderTexture, this._material);
	}

	public virtual void Final()
	{
		this._renderer.Final();
		if (null != this._renderTexture)
		{
			this._renderTexture.Release();
			this._renderTexture = null;
		}
		this._material = null;
	}

	public void SetBlendMode(A2U.Blend blendMode)
	{
		switch (blendMode)
		{
		case A2U.Blend.Normal:
		case A2U.Blend.Add:
		case A2U.Blend.Screen:
			this._camera.backgroundColor = new Color(1f, 1f, 1f, 0f);
			break;
		case A2U.Blend.Multiply:
			this._camera.backgroundColor = new Color(1f, 1f, 1f, 1f);
			break;
		case A2U.Blend.Overlay:
			this._camera.backgroundColor = new Color(1f, 1f, 1f, 0f);
			break;
		}
		this._renderer.SetBlendMode(blendMode);
	}

	public void SetRenderingOrder(A2U.Order order)
	{
		this._renderer.order = order;
	}

	private RenderTexture _renderTexture;

	[SerializeField]
	private Material _material;

	private Camera _camera;

	private A2U.Renderer _renderer = new A2U.Renderer();
}
