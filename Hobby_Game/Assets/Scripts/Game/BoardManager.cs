using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Game
{
    public class BoardManager : MonoBehaviour
    {

        public GameObject[] OuterWallTiles;
        public GameObject[] FloorTiles;
        public GameObject[] Enemies;
        public GameObject[] Pickups;
        public GameObject[] Walls;
        public GameObject Exit;
        public int StartingLevelSize = 20;

        public int BoardWidth { get{   return StartingLevelSize + boardLevel + boardLevel/5 * 6; }}
        public int BoardHeight { get { return BoardWidth; } }

        private int boardLevel;
        private Transform levelHolder;

        public void GenerateLevel(int level)
        {
            boardLevel = level;
            levelHolder = new GameObject("Level").transform;
            CreateBoard(BoardWidth, BoardHeight);
            InitializeList();

            LayoutObjectAtRandom(Pickups, level*4, level*5);
            LayoutObjectAtRandom(Walls, BoardWidth*BoardHeight/3, BoardWidth*BoardHeight/3);
            LayoutObjectAtRandom(Enemies, level +2, level);
            var exit = Instantiate(Exit, RandomExitPosition(), Quaternion.identity) as GameObject;
            if (exit != null)
            {
                exit.transform.SetParent(levelHolder);
            }
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
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x + 0.5f, y+0.5f, 0f), Quaternion.identity);
                    instance.transform.SetParent(boardHolder);
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
                    gridPositions.Add(new Vector3(x+0.5f, y+0.5f, 0f));
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
            var randomObjectCount = Random.Range(min, max + 1);

            for (var i = 0; i < randomObjectCount; i++)
            {
                var randomPosition = RandomPosition();
                var randomObject = tileArray[Random.Range(0, tileArray.Length)];
                var instance = Instantiate(randomObject, randomPosition, Quaternion.identity) as GameObject;
                if (instance != null)
                {
                    instance.transform.SetParent(levelHolder);
                }

            }
        }

        private List<Vector3> gridPositions = new List<Vector3>();

    }
}
