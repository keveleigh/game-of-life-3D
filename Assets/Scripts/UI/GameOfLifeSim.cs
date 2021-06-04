using Life.Logic;
using System;
using UnityEngine;

namespace Life.UI
{
    public class GameOfLifeSim : MonoBehaviour
    {
        public Color AliveColor;
        public Color HoverColor;
        public int Length = 100;
        public int Width = 100;
        public GameObject Cell;

        private bool isRunning = false;
        private Cell[,] cells;
        private int[,] neighbors;
        private long lastUpdateTime = DateTime.Now.Ticks;

        private void Awake()
        {
            cells = new Cell[Length, Width];
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == 0)
                    {
                        GameObject borderCube = Instantiate(Cell);
                        borderCube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        borderCube.transform.SetParent(transform);
                        Vector3 newBorderPosition = new Vector3((i - 1 - (Length / 2)) / 10.0f, (j - (Width / 2)) / 10.0f, 0);
                        borderCube.transform.localPosition = newBorderPosition;
                    }
                    if (i == Length - 1)
                    {
                        GameObject borderCube = Instantiate(Cell);
                        borderCube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        borderCube.transform.SetParent(transform);
                        Vector3 newBorderPosition = new Vector3((i + 1 - (Length / 2)) / 10.0f, (j - (Width / 2)) / 10.0f, 0);
                        borderCube.transform.localPosition = newBorderPosition;
                    }
                    if (j == 0)
                    {
                        GameObject borderCube = Instantiate(Cell);
                        borderCube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        borderCube.transform.SetParent(transform);
                        Vector3 newBorderPosition = new Vector3((i - (Length / 2)) / 10.0f, (j - 1 - (Width / 2)) / 10.0f, 0);
                        borderCube.transform.localPosition = newBorderPosition;
                    }
                    if (j == Width - 1)
                    {
                        GameObject borderCube = Instantiate(Cell);
                        borderCube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        borderCube.transform.SetParent(transform);
                        Vector3 newBorderPosition = new Vector3((i - (Length / 2)) / 10.0f, (j + 1 - (Width / 2)) / 10.0f, 0);
                        borderCube.transform.localPosition = newBorderPosition;
                    }

                    GameObject cube = Instantiate(Cell);
                    cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    cube.transform.SetParent(transform);
                    Vector3 newPosition = new Vector3((i - (Length / 2)) / 10.0f, (j - (Width / 2)) / 10.0f, 0);
                    cube.transform.localPosition = newPosition;
                    Cell cell = cube.AddComponent<Cell>();
                    cell.SetColors(AliveColor, HoverColor);
                    cells[i, j] = cell;
                }
            }
        }

        private void Update()
        {
            if (isRunning)
            {
                long currentTime = DateTime.Now.Ticks;
                if ((currentTime - lastUpdateTime) > 5000000)
                {
                    lastUpdateTime = currentTime;

                    neighbors = new int[300, 300];
                    for (int i = 0; i < Length; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            neighbors[i, j] = GetNumNeighbors(i, j);
                        }
                    }

                    for (int i = 0; i < Length; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            if (neighbors[i, j] < 2 || neighbors[i, j] > 3)
                            {
                                cells[i, j].Die();
                            }
                            else if ((neighbors[i, j] == 2 && cells[i, j].IsAlive) || (neighbors[i, j] == 3))
                            {
                                cells[i, j].Live();
                            }
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    hit.transform.gameObject.GetComponent<Cell>().ChangeState();
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                isRunning = !isRunning;
            }
        }

        public void SetRunning(bool toStart)
        {
            isRunning = toStart;
        }

        private void UpdateNeighborsOf(int i, int j)
        {
            neighbors[i + 1, j + 1] = GetNumNeighbors(i + 1, j + 1);
            neighbors[i + 1, j] = GetNumNeighbors(i + 1, j);
            neighbors[i + 1, j - 1] = GetNumNeighbors(i + 1, j - 1);
            neighbors[i, j + 1] = GetNumNeighbors(i, j + 1);
            neighbors[i, j - 1] = GetNumNeighbors(i, j - 1);
            neighbors[i - 1, j + 1] = GetNumNeighbors(i - 1, j + 1);
            neighbors[i - 1, j] = GetNumNeighbors(i - 1, j);
            neighbors[i - 1, j - 1] = GetNumNeighbors(i - 1, j - 1);
        }

        private int GetNumNeighbors(int i, int j)
        {
            int neighbors = 0;
            try
            {
                if (cells[i + 1, j + 1].IsAlive)
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i + 1, j].IsAlive)
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i + 1, j - 1].IsAlive)
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i, j + 1].IsAlive)
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i, j - 1].IsAlive)
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i - 1, j + 1].IsAlive)
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i - 1, j].IsAlive)
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (cells[i - 1, j - 1].IsAlive)
                {
                    neighbors++;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            return neighbors;
        }

        public Cell[,] GetCells => cells;
    }
}