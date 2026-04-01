using UnityEngine;
using UnityEngine.InputSystem;

public class DroneShooter : MonoBehaviour
{
    [SerializeField] private PlayerSettingsSO data;
    [SerializeField] private Transform firePoint;

    private float nextTimeShoot;

    private void OnAttack(InputValue value)
    {
        if(value.isPressed && nextTimeShoot < Time.time)
        {
            nextTimeShoot = Time.time + data.CdAttack;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = BulletPool.Instance.Get();
        bullet.transform.SetParent(null);
        bullet.transform.SetPositionAndRotation(firePoint.position, gameObject.transform.rotation);
        bullet.GetComponent<ProjectileController>().Launch(firePoint.forward);
    }
}
