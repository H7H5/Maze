using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintRenderer : MonoBehaviour
{
    public MazeSpawner MazeSpawner;
    public List<Vector3> positions = new List<Vector3>();
    public Player player;
    public bool pause = true;
    private LineRenderer componentLineRenderer;
    private void Awake()
    {
        componentLineRenderer = GetComponent<LineRenderer>();
    }
    public void DrawPath()
    {
        positions = new List<Vector3>();
        Maze maze = MazeSpawner.maze;
        Vector2Int curentPosition = maze.finishPosition;
        
        int d = 0;
        while(curentPosition != Vector2Int.zero)
        {
            int x = curentPosition.x;
            int y = curentPosition.y;
            positions.Add(new Vector3(x * MazeSpawner.CellSize.x, y * MazeSpawner.CellSize.y, y * MazeSpawner.CellSize.z));

            MazeGeneratorCell currentCell = maze.cells[curentPosition.x, curentPosition.y];
            if (curentPosition.x>0  && 
                maze.cells[curentPosition.x-1,curentPosition.y].DistanceFromStart==currentCell.DistanceFromStart -1)
            {
                curentPosition.x--;
            }
            else if (curentPosition.y > 0  &&
               maze.cells[curentPosition.x, curentPosition.y-1].DistanceFromStart == currentCell.DistanceFromStart - 1)
            {
                curentPosition.y--;
            }
            else if (curentPosition.x < maze.cells.GetLength(0)-1  &&
               maze.cells[curentPosition.x+1, curentPosition.y].DistanceFromStart == currentCell.DistanceFromStart - 1)
            {
                curentPosition.x++;
            }
            else if (curentPosition.y < maze.cells.GetLength(1) - 1  &&
              maze.cells[curentPosition.x, curentPosition.y+1].DistanceFromStart == currentCell.DistanceFromStart - 1)
            {
                curentPosition.y++;
            }
            if (d > 500)
            {
                break;
            }
        }
        positions.Add(Vector3.zero);
        componentLineRenderer.positionCount = positions.Count;
        componentLineRenderer.SetPositions(positions.ToArray());
        player.Init(positions);
        d++;
        
    }
  
}
