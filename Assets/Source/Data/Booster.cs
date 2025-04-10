using UnityEngine;

namespace Source.Data
{
    public class Booster : MonoBehaviour
    {
        [SerializeField] private int id;
        
        public int ID { get => id; private set => id = value; }

        public void ShowSelf(bool show) => gameObject.SetActive(show);
    }
}
