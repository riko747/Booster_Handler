using UnityEngine;
using UnityEngine.UI;

namespace Source.ButtonHandlers
{
    public abstract class ButtonHandler : MonoBehaviour
    {
        protected Button UIButton;

        protected abstract void OnButtonClicked();
    }
}
