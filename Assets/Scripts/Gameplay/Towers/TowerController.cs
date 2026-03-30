using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] EnemySettingsSO data;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem)
            && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            Attack(healthSystem);
    }

    private void Attack(HealthSystem healthSystem)
    {
        healthSystem.DoDamage(data.Damage);
    }
}
