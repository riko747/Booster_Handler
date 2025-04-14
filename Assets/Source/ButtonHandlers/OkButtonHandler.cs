using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Data;
using Source.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.ButtonHandlers
{
    public class OkButtonHandler : ButtonHandler
    {
        [SerializeField] private TextMeshProUGUI buttonText;

        private RectTransform FindAvailableInventoryCell()
        {
            return (from inventoryCell in UIManager.Instance.InventoryManager.InventoryCells 
                where !inventoryCell.Locked 
                select inventoryCell.GetComponent<RectTransform>()).FirstOrDefault();
        }
        
        protected override async UniTask OnButtonClicked()
        {
            var cellToMove = FindAvailableInventoryCell();
            var graphicsToAnimateFadeOut = new List<Graphic>();
            foreach (var booster in UIManager.Instance.BoosterSelectorManager.GetAllBoostersFromSelector().Where(booster => !booster.ChosenBooster))
            {
                graphicsToAnimateFadeOut.AddRange(booster.CollectImagesToAnimate());
            }
            
            graphicsToAnimateFadeOut.AddRange(UIManager.Instance.OkButton.CollectImagesToAnimate());
            graphicsToAnimateFadeOut.AddRange(UIManager.Instance.BoosterSelectorManager.GetRefreshButton().CollectImagesToAnimate());
            await DoTweenManager.Instance.PlayFadeOutAnimation(graphicsToAnimateFadeOut);
            
            foreach (var boosterInGroup in UIManager.Instance.BoosterSelectorManager.GetAllBoostersFromSelector())
            {
                var booster = boosterInGroup.GetComponent<Booster>();
                if (!boosterInGroup.ChosenBooster)
                {
                    booster.DisableBooster();
                }
                else
                {
                    await booster.MoveBoosterToInventory(cellToMove);
                }
            }
        }

        public async UniTask Show()
        {
            gameObject.SetActive(true);
            await DoTweenManager.Instance.PlayFadeInAnimation(CollectImagesToAnimate());
        }

        public override List<Graphic> CollectImagesToAnimate()
        {
            var graphicsToAnimate = new List<Graphic>()
            {
                ButtonImage,
                buttonText
            };
            return graphicsToAnimate;
        }
    }
}
