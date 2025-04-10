using System.Collections.Generic;
using System.Linq;
using Source.Data;
using Source.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Source.ButtonHandlers
{
    public class RefreshButtonHandler : ButtonHandler
    {
        [SerializeField] private BoosterData boosters;
        [SerializeField] private Transform boostersGroupParent;
        
        private void Start()
        {
            UIButton = GetComponent<Button>();

            if (UIButton != null)
                UIButton.onClick.AddListener(OnButtonClicked);
        }
        
        protected override void OnButtonClicked()
        {
            var boostersToShow = new List<Booster>();
            var boostersOnScreen = boostersGroupParent.GetComponentsInChildren<Booster>().ToList();

            foreach (var booster in boostersOnScreen)
            {
                booster.ShowSelf(false);
            }
            
            for (var i = 0; i != 3; i++)
            {
                boostersToShow.Add(boosters.boosters.FirstOrDefault(booster => booster.ID == RandomizeUtilities.GetRandomID(0, boosters.boosters.Count + 1)));
            }

            foreach (var booster in boostersToShow)
            {
                if (boostersOnScreen.Contains(booster))
                {
                    booster.ShowSelf(true);
                }
                else
                {
                    Instantiate(booster, boostersGroupParent);
                }
            }
        }

        private void OnDestroy()
        {
            if (UIButton != null)
                UIButton.onClick.RemoveListener(OnButtonClicked);
        }
    }
}
