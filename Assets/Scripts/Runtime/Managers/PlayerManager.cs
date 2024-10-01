using Runtime.Commands.Player;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public byte StageValue;
        internal ForceBallsToPoolCommand ForceCommand;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private PlayerMeshController _meshController;
        [SerializeField] private PlayerPhysicsController _physicsController;

        #endregion

        #region Private Variables

        private PlayerData _playerData;

        #endregion

        #endregion

        private void Awake()
        {
            _playerData = GetPlayerData();
            SendDataToControllers();
            Init();
        }
        
        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        }

        private void SendDataToControllers()
        {
            _movementController.SetData(_playerData.MovementData);
            _meshController.SetData(_playerData.MeshData);
        }

        private void Init()
        {
            ForceCommand = new ForceBallsToPoolCommand(this, _playerData.ForceData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += () => _movementController.IsReadyToMove(true);
            InputSignals.Instance.onInputReleased += () => _movementController.IsReadyToMove(false);
            InputSignals.Instance.onInputDragged += OnInputDragged;
            UISignals.Instance.onPlay += () => _movementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.onLevelSuccessful += () => _movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelFailed += () => _movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaEntered += () => _movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered += OnFinishedAreaEntered;
            CoreGameSignals.Instance.onReset += OnReset;
        }
        
        private void OnFinishedAreaEntered()
        {
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            // TODO: Minigame yazılacak
        }

        private void OnStageAreaSuccessful(byte value)
        {
            StageValue = ++value;
        }
        
        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            _movementController.UpdateInputParams(inputParams);
        }
        
        private void OnReset()
        {
            StageValue = 0;
            _movementController.OnReset();
            _physicsController.OnReset();
            _meshController.OnReset();
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= () => _movementController.IsReadyToMove(true);
            InputSignals.Instance.onInputReleased -= () => _movementController.IsReadyToMove(false);
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= () => _movementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.onLevelSuccessful -= () => _movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelFailed -= () => _movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaEntered -= () => _movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishedAreaEntered;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}