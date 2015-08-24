using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour
{

    public GameObject[] outerWallTiles;
    public GameObject[] floorTiles;
    public GameObject[] enemies;
    public GameObject[] pickups;
    public int startingLevelSize = 20;

    public int BoardWidth
    {
        get
        {
            return startingLevelSize + GameManager.instance.level*2;
        }
    }

    public int BoardHeight
    {
        get { return BoardWidth/2; }
    }

    public void GenerateLevel(int level)
    {
        CreateBoard(BoardWidth, BoardHeight);
    }

    private void CreateBoard(int columns, int rows)
    {
        var boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == 0 || x == columns-1 || y == 0 || y == rows-1)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);

            }
        }

    }
    


}
