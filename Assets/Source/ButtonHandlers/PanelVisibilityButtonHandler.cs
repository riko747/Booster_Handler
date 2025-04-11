using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Source.ButtonHandlers
{
    public class PanelVisibilityButtonHandler : ButtonHandler
    {
        private bool _panelIsHidden = false;
        
        [SerializeField] private RectTransform sidePanelTransform;
        protected override async UniTask OnButtonClicked()
        {
            if (!_panelIsHidden)
            {
                await DoTweenManager.Instance.PlayMoveToPointAnimation(sidePanelTransform,
                    UIManager.Instance.SidePanelHidePoint, Ease.Flash);
                _panelIsHidden = true;
            }
            else
            {
                await DoTweenManager.Instance.PlayMoveToPointAnimation(sidePanelTransform,
                    UIManager.Instance.SidePanelVisibilityPoint, Ease.Flash);
                _panelIsHidden = false;
            }
        }

        public override List<Graphic> CollectImagesToAnimate()
        {
            List<Graphic> graphics = new List<Graphic>()
            {
                uiButton.GetComponent<Image>()
            };
            return graphics;
        }
    }
}
