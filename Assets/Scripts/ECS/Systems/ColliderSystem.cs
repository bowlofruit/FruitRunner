﻿using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using UnityEngine;

public class ColliderSystem : AEntitySetSystem<float>
{
	private readonly World _world;

	public ColliderSystem(World world)
		: base(world.GetEntities()
				.With<ColliderComponent>()
				.With<PositionComponent>()
				.AsSet())
	{
		_world = world;
		Debug.Log("ColliderSystem initialized.");
	}

	protected override void Update(float deltaTime, in Entity playerEntity)
	{
		if (!playerEntity.Has<PlayerComponent>())
		{
			Debug.LogWarning("Player entity does not have PlayerComponent. Skipping...");
			return;
		}

		ref var playerCollider = ref playerEntity.Get<ColliderComponent>();
		ref var playerPosition = ref playerEntity.Get<PositionComponent>();
		ref var player = ref playerEntity.Get<PlayerComponent>();

		Debug.Log($"Checking collisions for player at position {playerPosition.Value} with radius {playerCollider.Radius}.");

		foreach (var entity in _world.GetEntities()
										.With<ColliderComponent>()
										.With<PositionComponent>()
										.Without<PlayerComponent>()
										.AsSet()
										.GetEntities())
		{
			ref var collider = ref entity.Get<ColliderComponent>();
			ref var position = ref entity.Get<PositionComponent>();

			Debug.Log($"Checking entity at position {position.Value} with radius {collider.Radius}.");

			if (IsColliding(playerPosition.Value, playerCollider.Radius, position.Value, collider.Radius))
			{
				Debug.Log($"Collision detected with entity at position {position.Value}.");
				HandleCollision(playerEntity, entity);
			}
		}
	}

	private bool IsColliding(Vector3 pos1, float radius1, Vector3 pos2, float radius2)
	{
		float distance = Vector3.Distance(pos1, pos2);
		bool colliding = distance <= (radius1 + radius2);

		Debug.Log($"Checking collision: Distance = {distance}, Combined Radius = {radius1 + radius2}, Colliding = {colliding}.");
		return colliding;
	}

	private void HandleCollision(Entity playerEntity, Entity otherEntity)
	{
		if (otherEntity.Has<FruitComponent>())
		{
			ref var fruit = ref otherEntity.Get<FruitComponent>();
			ref var player = ref playerEntity.Get<PlayerComponent>();

			player.CollectedFruits += fruit.Price;

			otherEntity.Dispose();
			Debug.Log("Fruit entity disposed.");
		}
		else if (otherEntity.Has<ObstacleComponent>())
		{
			ref var obstacle = ref otherEntity.Get<ObstacleComponent>();
			ref var player = ref playerEntity.Get<PlayerComponent>();

			if (obstacle.IsDeadly)
			{
				player.IsDead = true;
				Debug.LogError("Player hit a deadly obstacle! Game Over.");
			}
			else
			{
				Debug.Log("Player hit a harmless obstacle.");
			}
		}
	}
}