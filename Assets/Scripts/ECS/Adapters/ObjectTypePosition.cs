using System;
using UnityEngine;

namespace ECS.Adapters
{
	[Serializable]
	public struct ObjectTypePosition
	{
		public Transform Transform;
		public InteractableObjectType InteractableObjectType;
	}
}
