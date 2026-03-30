using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private static readonly int State = Animator.StringToHash("State");
    public event Action<int, int, bool> OnLifeUpdated;
    public event Action<int, int, bool> OnHealing;
    public event Action OnDie;

    [SerializeField] private int maxLife = 100;
    [SerializeField] private Rigidbody rb;

    private int life = 100;
    private bool isTakingDamage;

    private void Start()
    {
        life = maxLife;
        OnLifeUpdated?.Invoke(life, maxLife, false);
    }

    public void DoDamage(int damage, bool takeDmgMyself = false)
    {
        if (damage < 0 || isTakingDamage)
            return;

        life -= damage;

        if (life <= 0)
            StartCoroutine(nameof(Die));
        else
        {
            if (!takeDmgMyself)
                StartCoroutine(nameof(TakeDamage));

            OnLifeUpdated?.Invoke(life, maxLife, takeDmgMyself);
        }
    }

    public void Heal(int plus)
    {
        if (plus < 0)
            return;

        life += plus;

        if (life > maxLife)
            life = maxLife;

        OnHealing?.Invoke(life, maxLife, false);
    }

    private IEnumerator TakeDamage()
    {
        isTakingDamage = true;

        yield return new WaitForSeconds(0.3f);
        
        isTakingDamage = false;
    }

    private IEnumerator Die()
    {

        yield return new WaitForSeconds(0.5f);
        
        life = 0;
        OnDie?.Invoke();
    }
}