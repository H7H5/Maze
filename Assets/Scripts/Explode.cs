using UnityEngine;

public class Explode : MonoBehaviour
{
    private float spawn_radius = 4f;
    [SerializeField]
    private GameObject prefab;
    void Start()
    {
        SphereSpawnZero();
    }
    public void SphereSpawnZero()
    {
        int spawn_count = 100;
        while (spawn_count > 0)
        {
            spawn_count--;
            Instantiate(prefab, transform.position, Quaternion.identity, gameObject.transform);
        }
        Invoke("Delete", 5.0f);
    }
    private void Delete()
    {
        Destroy(gameObject);
    }
}
