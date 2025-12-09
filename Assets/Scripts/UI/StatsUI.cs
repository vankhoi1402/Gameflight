using MyGame.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : UIScreen
{
    [SerializeField]private TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private Image image;
    private void Update()
    {
        this.UpdateStatsTextMesh();
    }
    private void UpdateStatsTextMesh()
    {


        image.fillAmount = PlayerManager.Instance.currentFuel ;
        TextMeshProUGUI.text =
            Mathf.Round(GameManager.Instance.currentScore) + "\n" +
           Mathf.Round(GameManager.Instance.time) + "\n" ;
           
          
    }
}
