using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Source.ButtonHandlers
{
    public abstract class ButtonHandler : MonoBehaviour
    {
        [SerializeField] protected Button uiButton;
        protected Image ButtonImage;

        protected virtual void Awake()
        {
            if (uiButton == null) return;
            
            ButtonImage = uiButton.GetComponent<Image>();
        }
        
        protected virtual void Start()
        {
            if (uiButton != null)
                uiButton.onClick.AddListener(() => OnButtonClicked().Forget());
        }

        protected abstract UniTask OnButtonClicked();
        
        public abstract List<Graphic> CollectImagesToAnimate();
        
        public void DisableInteraction() => uiButton.interactable = false;
        public void EnableInteraction() => uiButton.interactable = true;

        private void OnDestroy()
        {
            if (uiButton != null)
                uiButton.onClick.RemoveListener(() => OnButtonClicked().Forget());
        }
    }
}
