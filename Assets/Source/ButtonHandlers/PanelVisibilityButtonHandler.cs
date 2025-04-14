using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Managers;
using UnityEngine.UI;

namespace Source.ButtonHandlers
{
    public class PanelVisibilityButtonHandler : ButtonHandler
    {
        private bool _panelIsHidden;
        private InventoryManager _inventoryManager;

        protected override void Start()
        {
            base.Start();
            
            _inventoryManager = transform.parent.GetComponent<InventoryManager>();
        }

        protected override async UniTask OnButtonClicked()
        {
            if (!_panelIsHidden)
            {
                await DoTweenManager.Instance.PlayMoveToPointAnimation(_inventoryManager.SidePanelRoot,
                    _inventoryManager.InventoryHidePoint, Ease.Flash);
                _panelIsHidden = true;
            }
            else
            {
                await DoTweenManager.Instance.PlayMoveToPointAnimation(_inventoryManager.SidePanelRoot,
                    _inventoryManager.InventoryVisibilityPoint, Ease.Flash);
                _panelIsHidden = false;
            }
        }

        public override List<Graphic> CollectImagesToAnimate()
        {
            var graphics = new List<Graphic>()
            {
                uiButton.GetComponent<Image>()
            };
            return graphics;
        }
    }
}
