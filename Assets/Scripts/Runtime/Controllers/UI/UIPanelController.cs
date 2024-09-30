using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> layers = new List<Transform>();

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanles += OnCloseAllPanels;
        }
        
        [Button("Open Panel")]
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            if (value > layers.Count-1)
            {
                value = layers.Count-1;
            }
            
            if (layers[value].childCount > 0)
            {
                OnClosePanel(value);
            }
            
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"), layers[value]);
        }
        
        [Button("Close Panel")]
        private void OnClosePanel(int value)
        {
            if (value > layers.Count-1)
            {
                value = layers.Count-1;
            }
            if (layers[value].childCount <= 0) return;
            
#if UNITY_EDITOR
            DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
            Destroy(layers[value].GetChild(0).gameObject);
#endif
        }

        [Button("Close All Panels")]
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount <= 0) return;

#if UNITY_EDITOR
                DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
            }
        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.onClosePanel -= OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanles -= OnCloseAllPanels;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}