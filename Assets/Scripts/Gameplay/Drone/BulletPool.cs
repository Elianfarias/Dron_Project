using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int defaultCapacity = 5;
    [SerializeField] private int maxSize = 30;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        Instance = this;
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(bulletPrefab, transform),
            actionOnGet: bullet => bullet.SetActive(true),
            actionOnRelease: bullet => bullet.SetActive(false),
            actionOnDestroy: bullet => Destroy(bullet),
            collectionCheck: false,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public GameObject Get() => pool.Get();
    public void Return(GameObject bullet)
    {
        bullet.transform.SetParent(transform);
        pool.Release(bullet);
    }
}