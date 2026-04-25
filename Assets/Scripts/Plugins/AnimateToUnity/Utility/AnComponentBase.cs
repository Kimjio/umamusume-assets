using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnComponentBase : MonoBehaviour
	{
		public bool Exist
		{
			get
			{
				return this._exist;
			}
		}

		public AnObjectBase ObjectBase
		{
			get
			{
				return this._objectBase;
			}
		}

		public AnUIBase UIBase
		{
			get
			{
				return this._uiBase;
			}
		}

		private void OnDestroy()
		{
			this._Release();
		}

		public virtual T Initialize<T>() where T : AnUIBase, new()
		{
			this._Release();
			this._exist = false;
			if (this._objectBase == null)
			{
				this._objectBase = AnMonoSingleton<AnRootManager>.Instance._GetFlBaseFromGameObject<AnObjectBase>(base.gameObject);
			}
			if (this._objectBase == null)
			{
				return default(T);
			}
			T t = new T();
			this._uiBase = t;
			this._uiBase.ComponentBase = this;
			this._ApplyValue();
			t.Initialize();
			if (!t.Exist)
			{
				this._exist = false;
				return default(T);
			}
			this._exist = true;
			this._Initialize_PostProcess();
			return this._uiBase as T;
		}

		protected virtual void _ApplyValue()
		{
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("BaseMotion", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("HitObject", 0);
			if (!AnUtilityString.IsEmptyString(text))
			{
				this._uiBase.SetHitAreaObject(AnValue.MotionPrefix + text);
			}
			else
			{
				this._uiBase.SetBasePath(this._objectBase.Root, base.gameObject, null);
			}
			if (!AnUtilityString.IsEmptyString(text2))
			{
				this._uiBase.SetHitAreaObject(AnValue.ObjectPrefix + text2);
				return;
			}
			this._uiBase.SetHitAreaObject(null);
		}

		protected virtual void _Initialize_PostProcess()
		{
		}

		protected virtual void _Release()
		{
			if (this._uiBase != null)
			{
				this._uiBase._Release();
				this._uiBase = null;
			}
			this._exist = false;
		}

		protected bool _exist;

		protected AnObjectBase _objectBase;

		protected AnUIBase _uiBase;
	}
}
