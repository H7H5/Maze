using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator 
{
    private int Width = 15;
    private int Height = 15;
   
    public Maze GenerateMaze()
    {
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[Width, Height];
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeGeneratorCell { X = x, Y = y };
            }
        }

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, Height - 1].WallLeft = false;
            cells[x, Height - 1].Ground = false;
        }
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[Width - 1, y].WallBottom = false;
            cells[Width - 1, y].Ground = false;
        }
        RemoveWallsWithBacktracker(cells);

        Vector2Int finishPosition = new Vector2Int(Width - 2, Height - 2);
        Maze maze = new Maze();
        maze.cells = cells;
        maze.finishPosition = finishPosition;

        return maze;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;
        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();
            int x = current.X;
            int y = current.Y;
            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y-1]);
            if (x < Width -2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y+1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);
                chosen.Visited = true;
                stack.Push(chosen);
                chosen.DistanceFromStart = current.DistanceFromStart + 1;
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell current, MazeGeneratorCell chosen)
    {
        if(current.X == chosen.X)
        {
            if (current.Y > chosen.Y)
            {
                current.WallBottom = false;
            }
            else
            {
                chosen.WallBottom = false;
            }
        }
        else
        {
            if (current.X > chosen.X)
            {
                current.WallLeft = false;
            }
            else
            {
                chosen.WallLeft = false;
            }
        }
    }

}
