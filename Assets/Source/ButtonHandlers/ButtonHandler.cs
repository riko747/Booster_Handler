using UnityEngine;
using UnityEngine.UI;

namespace Source.ButtonHandlers
{
    public abstract class ButtonHandler : MonoBehaviour
    {
        [SerializeField] protected Button uiButton;

        protected abstract void OnButtonClicked();
    }
}
