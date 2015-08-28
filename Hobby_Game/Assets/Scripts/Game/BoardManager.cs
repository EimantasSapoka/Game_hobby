using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Linq;

public class BoardManager : MonoBehaviour
{

    public GameObject[] OuterWallTiles;
    public GameObject[] FloorTiles;
    public GameObject[] Enemies;
    public GameObject[] Pickups;
    public GameObject[] Walls;
    public GameObject Exit;
    public int StartingLevelSize = 20;

    public int BoardWidth { get{   return StartingLevelSize + boardLevel*2; }}
    public int BoardHeight { get { return BoardWidth/2; } }

    private int boardLevel;

    public void GenerateLevel(int level)
    {
        boardLevel = level;
        CreateBoard(BoardWidth, BoardHeight);
        InitializeList();
        LayoutObjectAtRandom(Pickups, level, (int)Math.Ceiling(level*1.5f));
        LayoutObjectAtRandom(Walls, BoardWidth*BoardHeight/5, BoardWidth*BoardHeight/3);
        LayoutObjectAtRandom(Enemies, level - 1, level);
        Instantiate(Exit, RandomExitPosition(), Quaternion.identity);

    }

    private Vector3 RandomExitPosition()
    {
        return gridPositions.OrderBy(vector => Random.Range(0, gridPositions.Count))
                            .First(vector => (vector.x > BoardWidth/1.6f && vector.y > BoardHeight/1.6f));
    }

    private void CreateBoard(int columns, int rows)
    {
        var boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject toInstantiate = FloorTiles[Random.Range(0, FloorTiles.Length)];
                if (x == 0 || x == columns-1 || y == 0 || y == rows-1)
                    toInstantiate = OuterWallTiles[Random.Range(0, OuterWallTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                if (instance != null) instance.transform.SetParent(boardHolder);
            }
        }

    }



    void InitializeList()
    {
        gridPositions.Clear();
        for (int x = 2; x < BoardWidth - 1; x++)
        {
            for (int y = 2; y < BoardHeight - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }


    private Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }


    private void LayoutObjectAtRandom(GameObject[] tileArray, int min, int max)
    {
        int randomObjectCount = Random.Range(min, max + 1);

        for (int i = 0; i < randomObjectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject randomObject = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(randomObject, randomPosition, Quaternion.identity);
        }
    }

    private List<Vector3> gridPositions = new List<Vector3>();

}
