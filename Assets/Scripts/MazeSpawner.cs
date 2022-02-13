using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Maze maze;
    public Vector3 CellSize = new Vector3(1, 1, 0);

    [SerializeField]
    private Cell CellPrefab;
    [SerializeField]
    private HintRenderer HintRenderer;
    [SerializeField]
    private Color FinishColor;
    [SerializeField]
    private GameObject cellParentpref;
    [SerializeField]
    private GameObject cellParent;
    public void Start()
    {
        Destroy(cellParent);
        cellParent = Instantiate(cellParentpref, new Vector3(0, 0, 0), Quaternion.identity);
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();
        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell cell = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity, cellParent.transform).GetComponent<Cell>();
                cell.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                cell.WallBottom.SetActive(maze.cells[x, y].WallBottom);
                cell.ground.SetActive(maze.cells[x, y].Ground);
                if (x == 13 && y == 13)
                {
                    cell.ground.GetComponent<MeshRenderer>().material.color = FinishColor;
                }
                else
                {
                    if (cell.ground.active)
                    {
                        if (x != 0 && y != 0) { 
                            int random = UnityEngine.Random.Range(0, 100);

                            if (random < 10)
                            {
                                cell.RedZone.SetActive(true);
                            }
                        }
                    }
                } 
            }
        }
        HintRenderer.DrawPath();
    }
}
