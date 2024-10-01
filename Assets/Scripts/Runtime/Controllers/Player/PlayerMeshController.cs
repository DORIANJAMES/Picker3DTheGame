using System;
using DG.Tweening;
using Runtime.Data.ValueObjects;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Renderer renderer;
        [SerializeField] private TextMeshPro scaleText;
        [SerializeField] private ParticleSystem confetti;

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerMeshData _playerMeshData;

        #endregion

        #endregion
        

        internal void SetData(PlayerMeshData data)
        {
            _playerMeshData = data;
        }

        internal void ScaleUpPlayer()
        {
            renderer.gameObject.transform.DOScaleX(_playerMeshData.ScaleCounter,1).SetEase(Ease.Flash);
        }

        internal void ScaleUpText()
        {
            scaleText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
            {
                scaleText.DOFade(0, 0.30f).SetDelay(.35f);
                scaleText.rectTransform.DOAnchorPosY(1f, .65f).SetEase(Ease.Linear);
            });
        }

        internal void PlayConfetti()
        {
            confetti.Play();
        }

        internal void OnReset()
        {
            renderer.gameObject.transform.DOScaleX(1, 1).SetEase(Ease.Linear);
        }
    }
}