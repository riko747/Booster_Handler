using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Managers;
using UnityEngine.UI;

namespace Source.ButtonHandlers
{
    public class PanelVisibilityButtonHandler : ButtonHandler
    {
        protected override async UniTask OnButtonClicked()
        {
            await SwitchPanelState();
        }

        public async UniTask  SwitchPanelState()
        {
            if (!UIManager.Instance.InventoryManager.SidePanelIsHidden)
            {
                await DoTweenManager.Instance.PlayMoveToAnchoredPosition(UIManager.Instance.InventoryManager.SidePanelRoot,
                    UIManager.Instance.InventoryManager.InventoryHidePoint, Ease.Flash);
                UIManager.Instance.InventoryManager.SidePanelIsHidden = true;
            }
            else
            {
                await DoTweenManager.Instance.PlayMoveToAnchoredPosition(UIManager.Instance.InventoryManager.SidePanelRoot,
                    UIManager.Instance.InventoryManager.InventoryVisibilityPoint, Ease.Flash);
                UIManager.Instance.InventoryManager.SidePanelIsHidden = false;
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
