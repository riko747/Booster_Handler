using UnityEngine;

namespace Source.Data
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private GameObject lockedIcon;

        public bool Locked { get; private set; }

        public void Lock(bool lockCell)
        {
            lockedIcon.SetActive(lockCell);
            Locked = lockCell;
        }
    }
}
