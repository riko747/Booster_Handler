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
        [SerializeField] private ParticleSystem starsParticles;
        
        private RectTransform _rectTransform;
        private Image _checkMarkImage;

        public bool ChosenBooster { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _checkMarkImage = checkMark.GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable() => starsParticles.gameObject.SetActive(false);

        protected override async UniTask OnButtonClicked()
        {
            starsParticles.gameObject.SetActive(true);
            starsParticles.Play();
            ChosenBooster = true;
            
            foreach (var booster in UIManager.Instance.BoosterSelectorManager.GetAllBoostersFromSelector().Where(booster => booster.isActiveAndEnabled && booster != this))
            {
                booster.ChosenBooster = false;
                booster.ChangeAnimatorState(false);
                booster.DisableHighLight();
            }
            
            EnableHighlight();
            ChangeAnimatorState(true);

            var button = UIManager.Instance.OkButton;
            if (button is OkButtonHandler okButtonHandler)
            {
                await okButtonHandler.Show();
            }
        }

        public async UniTask MoveBoosterToInventory(RectTransform inventoryCellToMove)
        {
            layoutElement.ignoreLayout = true;
            transform.SetParent(transform.root);
            var centerPosition = new Vector2(0, 0);
            
            await DoTweenManager.Instance.PlayMoveToPointAnimation(_rectTransform, centerPosition);
            
            await EnableCheckMark();
            
            if (UIManager.Instance.InventoryManager.SidePanelIsHidden 
                && UIManager.Instance.SidePanelButton is PanelVisibilityButtonHandler sidePanelButtonHandler)
            {
                await sidePanelButtonHandler.SwitchPanelState();
            }

            await UniTask.WaitForSeconds(0.5f);
            DisableHighLight();
            await DisableCheckMark();
            
            await DoTweenManager.Instance.PlayMoveToPointWithResizeInParentAnimation(_rectTransform, inventoryCellToMove,
                Ease.OutCubic);
            
            transform.SetParent(inventoryCellToMove);
        }

        public override List<Graphic> CollectImagesToAnimate()
        {
            var imagesToAnimate = new List<Graphic>
            {
                ButtonImage,
                highlight.GetComponent<Image>()
            };

            return imagesToAnimate;
        }
        
        private void ChangeVisibilityByAlpha(Graphic graphic, float alpha)
        {
            Color colorWithNullAlpha = graphic.color;
            colorWithNullAlpha.a = alpha;
            graphic.color = colorWithNullAlpha;
        }

        private async UniTask EnableCheckMark()
        {
            ChangeVisibilityByAlpha(_checkMarkImage, 0);
            checkMark.SetActive(true);
            
            await DoTweenManager.Instance.PlayFadeInAnimation(_checkMarkImage, Ease.InCirc);
        }

        private async UniTask DisableCheckMark()
        {
            await DoTweenManager.Instance.PlayFadeOutAnimation(_checkMarkImage, Ease.InCirc);
            
            ChangeVisibilityByAlpha(_checkMarkImage, 1);
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

        private void EnableHighlight()
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
    }
}
