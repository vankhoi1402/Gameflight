using UnityEngine;

public class GroundCollider : MonoBehaviour ,IDamageSource
{
    public void OnHit(PlayerManager player)
    {
        GameManager.Instance.EndGame(GameState.Lose);
    }
}
