using System.Collections.Generic;
using Source.ButtonHandlers;
using Source.Data;
using UnityEngine;

namespace Source.Managers
{
    public class BoosterSelectorManager : MonoBehaviour
    {
        [SerializeField] private Transform boostersGroup;
        [SerializeField] private ButtonHandler refreshButton;
        
        private readonly List<Booster> _boosters = new();
        
        private void Start()
        {
            GetAllChildrenInBoosterGroup();
        }
        
        private void GetAllChildrenInBoosterGroup()
        {
            foreach (Transform booster in boostersGroup)
            {
                var currentBooster = booster.GetComponent<Booster>();
                _boosters.Add(currentBooster);
            }
        }
        
        public List<Booster> GetAllBoostersFromSelector() => _boosters;
        public ButtonHandler GetRefreshButton() => refreshButton;
    }
}
