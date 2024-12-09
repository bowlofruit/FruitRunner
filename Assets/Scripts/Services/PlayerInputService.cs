using InputSystem;
using MVP.Presenters;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Services
{
	public class PlayerInputService : IDisposable
	{
		private PlayerInputAction _action;

		public static Action OnDipsoseInputService;

        public Vector2 MoveInput => _action.Player.Move.ReadValue<Vector2>();

        public PlayerInputService()
        {
            _action = new PlayerInputAction();
            _action.Enable();
			OnDipsoseInputService = Dispose;
        }

		public void Dispose()
		{
			_action.Disable();
			_action = null;
		}
	}
}