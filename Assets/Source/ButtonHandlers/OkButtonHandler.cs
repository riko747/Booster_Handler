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
       
        protected override async UniTask OnButtonClicked()
        {
            var cellToMove = FindAvailableInventoryCell();
            var graphicsToAnimateFadeOut = new List<Graphic>();
            foreach (var booster in UIManager.Instance.GetBoostersFromShuffle().Where(booster => !booster.ChosenBooster))
            {
                graphicsToAnimateFadeOut.AddRange(booster.CollectImagesToAnimate());
            }
            
            graphicsToAnimateFadeOut.AddRange(UIManager.Instance.GetOkButton().CollectImagesToAnimate());
            graphicsToAnimateFadeOut.AddRange(UIManager.Instance.GetRefreshButton().CollectImagesToAnimate());
            await DoTweenManager.Instance.PlayFadeOutAnimation(graphicsToAnimateFadeOut);
            
            foreach (var boosterInGroup in UIManager.Instance.GetBoostersFromShuffle())
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

        private RectTransform FindAvailableInventoryCell()
        {
            return (from inventoryCell in UIManager.Instance.GetInventoryCells() 
                where !inventoryCell.Locked 
                select inventoryCell.GetComponent<RectTransform>()).FirstOrDefault();
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
