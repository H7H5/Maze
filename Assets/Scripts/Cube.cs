using UnityEngine;

public class Cube : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,10),
            Random.Range(0, 10),
            Random.Range(0, 10)), ForceMode.Impulse);
    }
}
