using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Config")]
    public float speed = 100f;
    public float lifetime = 5f;

    [Header("VFX")]
    public GameObject rocketExplosion;
    public ParticleSystem disableOnHit;

    [Header("References")]
    public MeshRenderer projectileMesh;
    public AudioSource inFlightAudioSource;

    private bool targetHit;
    private float spawnTime;
    private Vector3 travelDirection;
    private Coroutine returnCoroutine;

    private void OnEnable()
    {
        targetHit = false;
        spawnTime = Time.time;
        travelDirection = Vector3.zero;

        if (projectileMesh != null) projectileMesh.enabled = true;
        if (inFlightAudioSource != null) inFlightAudioSource.Play();
        if (disableOnHit != null) disableOnHit.Play();

        foreach (Collider col in GetComponents<Collider>())
            col.enabled = true;
    }

    private void OnDisable()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }
    }

    private void Update()
    {
        if (targetHit) return;

        transform.position += travelDirection * (speed * Time.deltaTime);

        if (Time.time - spawnTime >= lifetime)
            ReturnToPool();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enabled || targetHit) return;

        targetHit = true;
        Explode();
        returnCoroutine = StartCoroutine(ReturnAfterDelay());
    }

    private void Explode()
    {
        Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation);

        projectileMesh.enabled = false;
        inFlightAudioSource.Stop();
        disableOnHit.Stop();

        foreach (Collider col in GetComponents<Collider>())
            col.enabled = false;
    }

    private System.Collections.IEnumerator ReturnAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (BulletPool.Instance != null)
            BulletPool.Instance.Return(gameObject);

        returnCoroutine = null;
    }

    private void ReturnToPool()
    {
        BulletPool.Instance.Return(gameObject);
    }

    public void Launch(Vector3 direction)
    {
        travelDirection = direction;
    }

    private void ResetState()
    {
        targetHit = false;
        spawnTime = Time.time;
        travelDirection = transform.forward;

        if (projectileMesh != null) projectileMesh.enabled = true;
        if (inFlightAudioSource != null) inFlightAudioSource.Play();
        if (disableOnHit != null) disableOnHit.Play();

        foreach (Collider col in GetComponents<Collider>())
            col.enabled = true;
    }
}