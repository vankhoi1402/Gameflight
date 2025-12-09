using UnityEngine;


namespace MyGame.UI.Core
{
    public abstract class UIPopup : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

