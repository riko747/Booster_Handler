using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.ButtonHandlers;
using Source.Managers;
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
            
            foreach (var booster in UIManager.Instance.GetBoostersFromShuffle().Where(booster => booster.isActiveAndEnabled && booster != this))
            {
                booster.ChosenBooster = false;
                booster.ChangeAnimatorState(false);
                booster.DisableHighLight();
            }
            
            EnableHighlight();
            ChangeAnimatorState(true);

            var button = UIManager.Instance.GetOkButton();
            if (button is OkButtonHandler okButtonHandler)
            {
                await okButtonHandler.Show();
            }
        }

        public async UniTask MoveBoosterToInventory(RectTransform inventoryCellToMove)
        {
            await EnableCheckMark();
            ChangeIgnoreLayoutState(true);
            transform.parent = inventoryCellToMove;

            await UniTask.WaitForSeconds(0.5f);
            DisableHighLight();
            await DisableCheckMark();
            
            await DoTweenManager.Instance.PlayMoveToPointWithResizeInParentAnimation(RectTransform, inventoryCellToMove,
                Ease.OutCubic);
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
        
        private void ChangeVisibilityByAlpha(Graphic graphic, float alpha)
        {
            Color colorWithNullAlpha = graphic.color;
            colorWithNullAlpha.a = alpha;
            graphic.color = colorWithNullAlpha;
        }
        
        public async UniTask EnableCheckMark()
        {
            ChangeVisibilityByAlpha(CheckMarkImage, 0);
            checkMark.SetActive(true);
            
            await DoTweenManager.Instance.PlayFadeInAnimation(CheckMarkImage, Ease.InCirc);
        }

        public async UniTask DisableCheckMark()
        {
            await DoTweenManager.Instance.PlayFadeOutAnimation(CheckMarkImage, Ease.InCirc);
            
            ChangeVisibilityByAlpha(CheckMarkImage, 1);
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

        public void  DisableHighLight()
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
