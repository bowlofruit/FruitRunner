using InputSystem;
using System.Numerics;

namespace Services
{
	public class PlayerInputService
	{
		private PlayerInputAction _action;

        public Vector2 MoveInput => _action.Player.Move.ReadValue<Vector2>();

        public PlayerInputService()
        {
            _action = new PlayerInputAction();
            _action.Enable();
        }

        public void Disable()
        {
            _action.Disable();
        }
    }
}
