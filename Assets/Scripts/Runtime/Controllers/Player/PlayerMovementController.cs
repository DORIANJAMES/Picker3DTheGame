using System;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Varibles

        [ShowInInspector] private PlayerMovementData _playerMovementData;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        [ShowInInspector] private float _xValue;
        private float2 _clampValues;

        #endregion

        #endregion

        internal void SetData(PlayerMovementData data)
        {
            _playerMovementData = data;
        }

        private void FixedUpdate()
        {
            if (!_isReadyToPlay)
            {
                StopPlayer();
                return;
            }

            if (_isReadyToMove)
            {
                MovePlayer();
            }
            else
            {
                StopPlayerHorizontally();
            }
        }

        private void MovePlayer()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_xValue * _playerMovementData.SidewaySpeed, velocity.y,
                _playerMovementData.ForwardSpeed);
            rigidbody.velocity = velocity;
            var position = rigidbody.position;
            position = new Vector3(Mathf.Clamp(position.x, _clampValues.x, _clampValues.y), (position = rigidbody.position).y, position.z);
            rigidbody.position = position;
        }
        
        private void StopPlayer()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        
        private void StopPlayerHorizontally()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _playerMovementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        internal void IsReadyToPlay(bool condition)
        {
            _isReadyToPlay = condition;
        }

        internal void IsReadyToMove(bool condition)
        {
            _isReadyToMove = condition;
        }

        internal void UpdateInputParams(HorizontalInputParams inputParams)
        {
            _xValue = inputParams.HorizontalValue;
            _clampValues = inputParams.ClampValues;
        }

        internal void OnReset()
        {
            StopPlayer();
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }

    }
}