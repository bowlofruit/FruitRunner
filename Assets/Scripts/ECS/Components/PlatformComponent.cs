using DefaultEcs;
using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct PlatformComponent
	{
		public int MaxObjects;
		public Vector3[] ObjectPositions;
		public Entity[] ActiveObjects;
	}
}
