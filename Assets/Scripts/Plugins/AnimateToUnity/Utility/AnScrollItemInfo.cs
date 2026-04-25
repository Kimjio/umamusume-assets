using System;

namespace AnimateToUnity.Utility
{
	public class AnScrollItemInfo
	{
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		public int ExtendedIndex
		{
			get
			{
				return this._extendedIndex;
			}
			set
			{
				this._extendedIndex = value;
			}
		}

		public int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		public float StartPosition
		{
			get
			{
				return this._startPosition;
			}
			set
			{
				this._startPosition = value;
			}
		}

		public float CenterPosition
		{
			get
			{
				return this._centerPosition;
			}
			set
			{
				this._centerPosition = value;
			}
		}

		public float EndPosition
		{
			get
			{
				return this._endPosition;
			}
			set
			{
				this._endPosition = value;
			}
		}

		public AnScrollItemObject ItemObject
		{
			get
			{
				return this._itemObject;
			}
			set
			{
				this._itemObject = value;
			}
		}

		public void SetObjectID(int id)
		{
			this._objectID = id;
		}

		protected int _index;

		protected int _extendedIndex;

		protected int _objectID;

		protected float _startPosition;

		protected float _centerPosition;

		protected float _endPosition;

		protected AnScrollItemObject _itemObject;
	}
}
