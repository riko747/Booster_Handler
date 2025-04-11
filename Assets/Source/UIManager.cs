 using System.Collections.Generic;
 using Source.Data;
 using UnityEngine;
 using UnityEngine.Serialization;
 using UnityEngine.UI;

 namespace Source
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Transform boostersGroup;
        [SerializeField] private GameObject okButton;
        
        private readonly List<Booster> _boosters = new();
        
        public static UIManager Instance {get; private set;}

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

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

        public List<Booster> GetBoosters() => _boosters;
        
        public void EnableOKButton() => okButton.SetActive(true); 
        public void DisableOKButton() => okButton.SetActive(false);
    }
}
