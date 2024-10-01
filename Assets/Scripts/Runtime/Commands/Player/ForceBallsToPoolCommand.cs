using Runtime.Data.ValueObjects;
using Runtime.Managers;

namespace Runtime.Commands.Player
{
    public class ForceBallsToPoolCommand
    {

        private PlayerManager _playerManager;
        private PlayerForceData _playerForceData;
        public ForceBallsToPoolCommand(PlayerManager playerManager, PlayerForceData playerForceData)
        {
            _playerManager = playerManager;
            _playerForceData = playerForceData;
        }

        internal void Execute()
        {
            
        }
    }
}