using System;
using System.Collections;
using UnityEngine;

namespace GameOfLife.Simulation
{
    public class GameOfLifeSim : MonoBehaviour
    {
        public Color AliveColor;
        public Color HoverColor;
        public int Length = 100;
        public int Width = 100;

        private bool isRunning = false;
        private Cell[,] cells;
        private int[,] neighbors;
        private long lastUpdateTime = DateTime.Now.Ticks;

        void Awake()
        {
            cells = new Cell[Length, Width];
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(0); j++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    cube.transform.SetParent(transform);
                    Vector3 newPosition = new Vector3((i - (cells.GetLength(0) / 2)) / 10.0f, (j - (cells.GetLength(0) / 2)) / 10.0f, 0);
                    cube.transform.localPosition = newPosition;
                    Cell cell = cube.AddComponent<Cell>();
                    cell.SetColors(AliveColor, HoverColor);
                    cells[i, j] = cell;
                }
            }
        }

        void Update()
        {
            if (isRunning)
            {
                long currentTime = DateTime.Now.Ticks;
                if ((currentTime - lastUpdateTime) > 5000000)
                {
                    lastUpdateTime = currentTime;

                    neighbors = new int[300, 300];
                    for (int i = 0; i < cells.GetLength(0); i++)
                    {
                        for (int j = 0; j < cells.GetLength(0); j++)
                        {
                            neighbors[i, j] = getNumNeighbors(i, j);
                        }
                    }

                    for (int i = 0; i < cells.GetLength(0); i++)
                    {
                        for (int j = 0; j < cells.GetLength(0); j++)
                        {
                            if (neighbors[i, j] < 2 || neighbors[i, j] > 3)
                            {
                                cells[i, j].Die();
                            }
                            else if ((neighbors[i, j] == 2 && cells[i, j].IsAlive()) || (neighbors[i, j] == 3))
                            {
                                cells[i, j].Live();
                            }
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    hit.transform.gameObject.GetComponent<Cell>().ChangeState();
                    Debug.Log(hit.transform.gameObject.GetComponent<Cell>().IsAlive());
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                isRunning = !isRunning;
                Debug.Log(isRunning);
            }
        }

        public void updateNeighborsOf(int i, int j)
        {
            neighbors[i + 1, j + 1] = getNumNeighbors(i + 1, j + 1);
            neighbors[i + 1, j] = getNumNeighbors(i + 1, j);
            neighbors[i + 1, j - 1] = getNumNeighbors(i + 1, j - 1);
            neighbors[i, j + 1] = getNumNeighbors(i, j + 1);
            neighbors[i, j - 1] = getNumNeighbors(i, j - 1);
            neighbors[i - 1, j + 1] = getNumNeighbors(i - 1, j + 1);
            neighbors[i - 1, j] = getNumNeighbors(i - 1, j);
            neighbors[i - 1, j - 1] = getNumNeighbors(i - 1, j - 1);
        }

        public int getNumNeighbors(int i, int j)
        {
            int neighbors = 0;
            try
            {
                if (cells[i + 1, j + 1].IsAlive())
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i + 1, j].IsAlive())
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i + 1, j - 1].IsAlive())
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i, j + 1].IsAlive())
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i, j - 1].IsAlive())
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i - 1, j + 1].IsAlive())
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i - 1, j].IsAlive())
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i - 1, j - 1].IsAlive())
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            return neighbors;
        }

        public Cell[,] GetCells()
        {
            return cells;
        }
    }
}