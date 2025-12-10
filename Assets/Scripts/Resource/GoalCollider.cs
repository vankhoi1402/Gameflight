using UnityEngine;

public class GoalCollider : MonoBehaviour, ICollectible
{
    public void OnCollected(PlayerManager manager)
    {
        float dotVector = Vector2.Dot(Vector2.up, manager.transform.up);
        float angle = Vector2.Angle(Vector2.up, manager.transform.up);

        float minDotVector = .50f;
        if (dotVector < minDotVector)
        {
            GameManager.Instance.EndGame(GameState.Lose);
            return;
        }
        else if (dotVector >0.999f)
        {
            AudioManager.Instance.PlaySFX("success");
            GameManager.Instance.EndGame(GameState.Win);
            GameManager.Instance.addScore(1000);
        }
    }
}
