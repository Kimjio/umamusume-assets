using System;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnCollisionParameter
	{
		public AnCollisionTypes CollisionType
		{
			get
			{
				return this._collisionType;
			}
			set
			{
				this._collisionType = value;
			}
		}

		public float Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		public Vector3 Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		public bool Through
		{
			get
			{
				return this._through;
			}
			set
			{
				this._through = value;
			}
		}

		public void _CreateHierarchy(AnRoot root, GameObject parentGameObject)
		{
			if (this.CollisionType == AnCollisionTypes.Square)
			{
				BoxCollider boxCollider = parentGameObject.AddComponent<BoxCollider>();
				boxCollider.isTrigger = true;
				boxCollider.size = new Vector3(root.Parameter.BaseNullSize * this.Scale, root.Parameter.BaseNullSize * this.Scale, root.DefaultColliderThickness);
				boxCollider.center = this.Offset;
				return;
			}
			if (this.CollisionType == AnCollisionTypes.Circle)
			{
				SphereCollider sphereCollider = parentGameObject.AddComponent<SphereCollider>();
				sphereCollider.isTrigger = true;
				sphereCollider.radius = root.Parameter.BaseNullSize * this.Scale * 0.5f;
				sphereCollider.center = this.Offset;
				return;
			}
			if (this.CollisionType == AnCollisionTypes.Square2D)
			{
				BoxCollider2D boxCollider2D = parentGameObject.AddComponent<BoxCollider2D>();
				boxCollider2D.isTrigger = true;
				boxCollider2D.size = new Vector2(root.Parameter.BaseNullSize, root.Parameter.BaseNullSize) * this.Scale;
				boxCollider2D.offset = new Vector2(this.Offset.x, this.Offset.y);
				return;
			}
			if (this.CollisionType == AnCollisionTypes.Circle2D)
			{
				CircleCollider2D circleCollider2D = parentGameObject.AddComponent<CircleCollider2D>();
				circleCollider2D.isTrigger = true;
				circleCollider2D.radius = root.Parameter.BaseNullSize * this.Scale * 0.5f;
				circleCollider2D.offset = new Vector2(this.Offset.x, this.Offset.y);
			}
		}

		public AnCollisionTypes _collisionType;

		public Vector3 _offset = Vector3.zero;

		public float _scale;

		public bool _through;
	}
}
