using System;
using Runtime.Enums;
using Runtime.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UISubscriptionTypes type;
        [SerializeField] private Button button;

        #endregion

        #region Private Variables

        [ShowInInspector] private UIManager _manager;

        #endregion

        #endregion

        private void Awake()
        {
             GetReferences();
        }

        private void GetReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscrivbeEvents();
        }

        private void SubscrivbeEvents()
        {
            switch (type)
            {
                case UISubscriptionTypes.OnPlay:
                    button.onClick.AddListener(_manager.Play);
                    break;
                case UISubscriptionTypes.OnNextLevel:
                    button.onClick.AddListener(_manager.NextLevel);
                    break;
                case UISubscriptionTypes.OnRestartLevel:
                    button.onClick.AddListener(_manager.RestratLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void UnSubscrivbeEvents()
        {
            switch (type)
            {
                case UISubscriptionTypes.OnPlay:
                    button.onClick.RemoveListener(_manager.Play);
                    break;
                case UISubscriptionTypes.OnNextLevel:
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                case UISubscriptionTypes.OnRestartLevel:
                    button.onClick.RemoveListener(_manager.RestratLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            UnSubscrivbeEvents();
        }
    }
}