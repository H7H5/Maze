using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject prefPlayer;
    public Player player;
    public HintRenderer hintRenderer;
    public bool pause = true;
    public GameObject button;
    public AlphaController alphaController;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void RestartPlayer()
    {
        Invoke("CreatePlayer", 2.0f);
    }

    public void CreatePlayer()
    {
        Vector3 newPosition = hintRenderer.positions[hintRenderer.positions.Count - 1];
        newPosition = new Vector3(newPosition.x + 5f, 0, newPosition.z + 5f);
        transform.position = newPosition;
        player = Instantiate(prefPlayer, newPosition, Quaternion.identity).GetComponent<Player>();
        player.Init(hintRenderer.positions);
        //player.move = pause;
    }
    public void ActivShield()
    {
        if (player)
        {
            player.ActivShield();
        }

    }
    public void DeactiveShield()
    {
        if (player)
        {
            player.DeactiveShield();
        }

    }
    public void Pause()
    {
        pause = false;
        button.SetActive(true);
        player.move = pause;
    }
    public void Continue()
    {
        pause = true;
        button.SetActive(false);
        player.move = pause;
    }
    public void RestartGame()
    {
        Invoke("RestartGame1", 4.0f);
        Invoke("RestartGame2", 6.0f);
    }
    public void RestartGame1()
    {
        alphaController.Pause();
    }
    public void RestartGame2()
    {
        alphaController.Continue();
        Destroy(player.gameObject);
        hintRenderer.MazeSpawner.Start();
        CreatePlayer();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
