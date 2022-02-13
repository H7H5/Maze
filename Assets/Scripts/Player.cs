using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool move = false;
    public List<Vector3> positions;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color shieldColor;
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private GameObject destroyPref;
    [SerializeField]
    private GameObject Confety;

    private bool shield = false;
    private float speed = 0.8f;
    private Vector3 nextPosition;
    private int numberPosition;
    
    void Start()
    {
        Invoke("StartMove", 2.0f);
    }

    private void StartMove()
    {
        move = GameController.Instance.hintRenderer.pause;
    }

    public void Init(List<Vector3> positions)
    {
        this.positions = positions;
        if (positions.Count > 0)
        {
            numberPosition = positions.Count-1;
        }
        nextPosition = positions[numberPosition];
        nextPosition = new Vector3(nextPosition.x + 5f, 0, nextPosition.z + 5f);
    }
    private void FixedUpdate()
    {
        if (move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed);
            if (Vector3.Distance(transform.position, nextPosition) < 0.01f)
            {
                numberPosition--;
                if (numberPosition < 0)
                {
                    move = false;
                    Confety.SetActive(true);
                    GameController.Instance.RestartGame();
                }
                else
                {
                    nextPosition = positions[numberPosition];
                    nextPosition = new Vector3(nextPosition.x + 5f, 0, nextPosition.z + 5f);
                } 
            }
        }
    }
    public void ActivShield()
    {
        cube.GetComponent<MeshRenderer>().material.color = shieldColor;
        shield = true;
        Invoke("StartInvoke", 2.0f);
    }
    public void DeactiveShield()
    {
        cube.GetComponent<MeshRenderer>().material.color = defaultColor;
        shield = false;
        CancelInvoke("RestartTimeShild");
    }

    private void RestartTimeShild()
    {
        DeactiveShield();
    }
    public void ColisionRedZone()
    {
        if (shield == false) { 
            Vector3 point = gameObject.transform.position;
            point.y = point.y+10;
            cube.gameObject.SetActive(false);
            Instantiate(destroyPref, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameController.Instance.RestartPlayer();
        }
    }
    private void Restart()
    {
        Vector3 newPosition = positions[positions.Count - 1];
        newPosition = new Vector3(newPosition.x + 5f, 0, newPosition.z + 5f);
        transform.position = newPosition;
        cube.gameObject.SetActive(true);
    }
}
