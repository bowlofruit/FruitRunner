using System.Collections.Generic;
using System;
using UnityEngine;
using ECS.Adapters;

[Serializable]
public struct PlatformComponent
{
	public int MaxObjects;
	public Dictionary<int, Vector3> ObjectPositions;
	public HashSet<int> OccupiedPositions;
	public Dictionary<int, InteractableObjectType> ObjectTypes;
	public bool IsLast;
	public bool IsObjectInit;
	public float MovementDistance;
	public PlatformType PlatformType;
}
