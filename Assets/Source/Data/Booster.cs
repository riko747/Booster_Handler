using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.ButtonHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Data
{
    public class Booster : ButtonHandler
    {
        private readonly int _pressed = Animator.StringToHash("Pressed");
        
        [SerializeField] private GameObject highlight;
        [SerializeField] private GameObject checkMark;
        [SerializeField] private Animator highlightAnimator;
        [SerializeField] private LayoutElement layoutElement;
        
        private RectTransform _rectTransform;

        public RectTransform RectTransform
        {
            get => _rectTransform;
            set => _rectTransform = value;
        }

        public bool ChosenBooster { get; private set; }

        public Image CheckMarkImage {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            CheckMarkImage = checkMark.GetComponent<Image>();
            RectTransform = GetComponent<RectTransform>();
        }

        protected override async UniTask OnButtonClicked()
        {
            ChosenBooster = true;
            
            foreach (var booster in UIManager.Instance.GetBoosters().Where(booster => booster.isActiveAndEnabled && booster != this))
            {
                booster.ChosenBooster = false;
                booster.ChangeAnimatorState(false);
                booster.DisableHighLight();
            }
            
            EnableHighlight();
            ChangeAnimatorState(true);
            await UIManager.Instance.GetOkButton().Show();
        }

        public override List<Graphic> CollectImagesToAnimate()
        {
            var imagesToAnimate = new List<Graphic> { ButtonImage };
            
            imagesToAnimate.Add(highlight.GetComponent<Image>());

            return imagesToAnimate;
        }

        #region BoosterView

        public void ChangeIgnoreLayoutState(bool ignore)
        {
            layoutElement.ignoreLayout = ignore;
        }
        
        public void EnableCheckMark()
        {
            Color colorWithNullAlpha = CheckMarkImage.color;
            colorWithNullAlpha.a = 0f;
            CheckMarkImage.color = colorWithNullAlpha;
            
            checkMark.SetActive(true);
        }

        public void DisableCheckMark()
        {
            Color colorWithNullAlpha = CheckMarkImage.color;
            colorWithNullAlpha.a = 1f;
            CheckMarkImage.color = colorWithNullAlpha;
            
            checkMark.SetActive(false);
        }

        public void EnableBooster()
        {
            gameObject.SetActive(true);
        }

        public void DisableBooster()
        {
            gameObject.SetActive(false);
        }

        public void EnableHighlight()
        {
            highlight.SetActive(true);
            highlightAnimator.enabled = true;
        }

        public void DisableHighLight()
        {
            highlight.SetActive(false);
            highlightAnimator.enabled = false;
        }

        private void ChangeAnimatorState(bool pressed)
        {
            if (highlight.activeInHierarchy)
            {
                highlightAnimator.SetBool(_pressed, pressed);
            }
        }
        
        #endregion
    }
}
