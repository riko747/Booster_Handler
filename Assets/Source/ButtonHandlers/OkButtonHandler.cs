using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Animation;
using Source.Data;
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
            foreach (var booster in UIManager.Instance.GetBoosters().Where(booster => !booster.ChosenBooster))
            {
                graphicsToAnimateFadeOut.AddRange(booster.CollectImagesToAnimate());
            }
            
            graphicsToAnimateFadeOut.AddRange(UIManager.Instance.GetOkButton().CollectImagesToAnimate());
            await DoTweenManager.Instance.PlayFadeOutAnimation(graphicsToAnimateFadeOut);
            
            foreach (var boosterInGroup in UIManager.Instance.GetBoosters())
            {
                var booster = boosterInGroup.GetComponent<Booster>();
                if (!boosterInGroup.ChosenBooster)
                {
                    booster.DisableBooster();
                }
                else
                {
                    booster.DisableHighLight();
                    booster.EnableCheckMark();
                    await DoTweenManager.Instance.PlayFadeInAnimation(booster.CheckMarkImage, Ease.InCirc);
                    booster.ChangeIgnoreLayoutState(true);
                    await DoTweenManager.Instance.PlayMoveToPointWithResizeAnimation(booster.RectTransform, cellToMove, Ease.OutCubic);
                    await DoTweenManager.Instance.PlayFadeOutAnimation(booster.CheckMarkImage, Ease.InCirc);
                }
            }
        }

        public RectTransform FindAvailableInventoryCell()
        {
            return (from inventoryCell in UIManager.Instance.InventoryCells 
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
