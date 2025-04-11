using UnityEngine;

namespace Source.Data
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private bool locked;
        [SerializeField] private GameObject lockedIcon;
        
        public bool Locked => locked;
        
        private void Start()
        {
            lockedIcon.SetActive(locked);
        }
    }
}
