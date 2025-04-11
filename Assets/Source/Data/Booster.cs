using System.Linq;
using Source.ButtonHandlers;
using UnityEngine;

namespace Source.Data
{
    public class Booster : ButtonHandler
    {
        private readonly int _pressed = Animator.StringToHash("Pressed");
        
        [SerializeField] private GameObject highlight;
        [SerializeField] private Animator highlightAnimator;
        
        private void Start()
        {
            if (uiButton != null)
                uiButton.onClick.AddListener(OnButtonClicked);
        }
        
        protected override void OnButtonClicked()
        {
            UIManager.Instance.EnableOKButton();
            
            foreach (var booster in UIManager.Instance.GetBoosters().Where(booster => booster.isActiveAndEnabled))
            {
                booster.DisableHighlightAnimation();
                booster.DisableBoosterHighlight();
            }
            
            EnableBoosterHighlight();
            EnableHighlightAnimation();
        }

        private void EnableBoosterHighlight() => highlight.SetActive(true);
        public void DisableBoosterHighlight() => highlight.SetActive(false);

        private void EnableHighlightAnimation()
        {
            if (highlight.activeInHierarchy)
            {
                highlightAnimator.SetBool(_pressed, true);
            }
        }

        public void DisableHighlightAnimation()
        {
            if (highlight.activeInHierarchy)
            {
                highlightAnimator.SetBool(_pressed, false);
            }
        }
    }
}
