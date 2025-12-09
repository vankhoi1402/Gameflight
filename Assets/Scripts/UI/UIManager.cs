using System.Collections.Generic;
using MyGame.UI.Screens;
using UnityEngine;

namespace MyGame.UI.Core
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        private UIScreen currentScreen;
        private readonly Stack<UIPopup> popupStack = new();
        public GameOverUI GameOverUI { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            GameOverUI = GetComponentInChildren<GameOverUI>(true); 


        }
        

        // Hiện 1 màn hình (ẩn cái cũ)
        public void ShowScreen(UIScreen screen)
        {
            if (currentScreen != null)
                currentScreen.Hide();

            currentScreen = screen;
            currentScreen.Show();
        }

        // Hiện popup (có thể nhiều)
        public void ShowPopup(UIPopup popup)
        {
            popup.Show();
            popupStack.Push(popup);
        }

        // Đóng popup
        public void ClosePopup(UIPopup popup)
        {
            if (popupStack.Count > 0 && popupStack.Peek() == popup)
            {
                popup.Close();
                popupStack.Pop();
            }
        }

        // Đóng tất cả
        public void HideAll()
        {
            if (currentScreen != null)
                currentScreen.Hide();

            while (popupStack.Count > 0)
                popupStack.Pop().Close();
        }
    }
}
