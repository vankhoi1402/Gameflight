using UnityEngine;


namespace MyGame.UI.Core
{
    public abstract class UIScreen : MonoBehaviour
    {
        [SerializeField] protected GameObject panel;

        public virtual void Show()
        {
            if (panel != null) panel.SetActive(true);
            else gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            if (panel != null) panel.SetActive(false);
            else gameObject.SetActive(false);
        }
    }
}
