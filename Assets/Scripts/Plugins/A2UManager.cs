using System;
using UnityEngine;

public class A2UManager : MonoBehaviour
{
	public A2UController a2uController
	{
		get
		{
			return this._a2uController;
		}
	}

	public A2UCamera a2uCamera
	{
		get
		{
			return this._a2uCamera;
		}
	}

	private void OnDestroy()
	{
		this.Final();
	}

	public void InitController(ref A2UController.InitContext context)
	{
		if (null != this._a2uController)
		{
			return;
		}
		this._a2uController = base.GetComponentInChildren<A2UController>();
		this._a2uController.Init(ref context);
	}

	public void InitCamera(int screenWidth, int screenHeight)
	{
		if (null != this._a2uCamera)
		{
			return;
		}
		this._a2uCamera = base.GetComponentInChildren<A2UCamera>();
		this._a2uCamera.Init(screenWidth, screenHeight);
	}

	public virtual void Final()
	{
		if (null != this._a2uController)
		{
			this._a2uController.Final();
			this._a2uController = null;
		}
		if (null != this._a2uCamera)
		{
			this._a2uCamera.Final();
			this._a2uCamera = null;
		}
	}

	public const int A2ULayer = 30;

	protected A2UController _a2uController;

	protected A2UCamera _a2uCamera;
}
