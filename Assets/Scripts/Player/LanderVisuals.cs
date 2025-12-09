using UnityEngine;

public class LanderVisuals : MonoBehaviour
{

    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private GameObject landerExplosionVfx;
    private void Start()
    {
        PlayerController.Instance.OnUpForce += Lander_OnUpForce;
        PlayerController.Instance.OnLeftForce += Lander_OnLeftForce;
        PlayerController.Instance.OnRightForce += Lander_OnRightForce;
        PlayerController.Instance.OnBeforeForce += Lander_OnBeforeForce;
        PlayerManager.Instance.Ondie += Lander_OnLanded;

        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    }
    private void Lander_OnLanded(object sender, System.EventArgs e)
    {
                Instantiate(landerExplosionVfx, transform.position, Quaternion.identity);
                
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
        
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
        
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);
        
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);
        
    }

    private void SetEnabledThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}
