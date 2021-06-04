using Life.Logic;
using System;
using UnityEngine;

namespace Life.UI
{
    public class GameOfLifeSim : MonoBehaviour
    {
        [SerializeField]
        private Color aliveColor;

        [SerializeField]
        private Color hoverColor;

        [SerializeField]
        private int length = 100;

        [SerializeField]
        private int width = 100;

        [SerializeField]
        private GameObject cell;

        private bool isRunning = false;
        private Cell[,] cells;
        private int[,] neighbors;
        private long lastUpdateTime = DateTime.Now.Ticks;

        private void Awake()
        {
            float halfLength = length / 2.0f;
            float halfWidth = width / 2.0f;
            float cellLength = cell.transform.localScale.x;
            float cellWidth = cell.transform.localScale.y;

            cells = new Cell[length, width];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0)
                    {
                        GameObject borderCube = Instantiate(cell, transform);
                        borderCube.transform.localPosition = new Vector3((i - 1 - halfLength) * cellLength, (j - halfWidth) * cellWidth, 0);
                    }
                    if (i == length - 1)
                    {
                        GameObject borderCube = Instantiate(cell, transform);
                        borderCube.transform.localPosition = new Vector3((i + 1 - halfLength) * cellLength, (j - halfWidth) * cellWidth, 0);
                    }
                    if (j == 0)
                    {
                        GameObject borderCube = Instantiate(cell, transform);
                        borderCube.transform.localPosition = new Vector3((i - halfLength) * cellLength, (j - 1 - halfWidth) * cellWidth, 0);
                    }
                    if (j == width - 1)
                    {
                        GameObject borderCube = Instantiate(cell, transform);
                        borderCube.transform.localPosition = new Vector3((i - halfLength) * cellLength, (j + 1 - halfWidth) * cellWidth, 0);
                    }

                    GameObject cube = Instantiate(cell, transform);
                    cube.transform.localPosition = new Vector3((i - halfLength) * cellLength, (j - halfWidth) * cellWidth, 0);
                    Cell cellComponent = cube.AddComponent<Cell>();
                    cellComponent.SetColors(aliveColor, hoverColor);
                    cells[i, j] = cellComponent;
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
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            neighbors[i, j] = GetNumNeighbors(i, j);
                        }
                    }

                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < width; j++)
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

            bool iMaxEdgeValid = i + 1 < length;
            bool jMaxEdgeValid = j + 1 < width;
            bool iMinEdgeValid = i - 1 >= 0;
            bool jMinEdgeValid = j - 1 >= 0;

            if (iMaxEdgeValid)
            {
                if (cells[i + 1, j].IsAlive)
                {
                    neighbors++;
                }

                if (jMaxEdgeValid && cells[i + 1, j + 1].IsAlive)
                {
                    neighbors++;
                }

                if (jMinEdgeValid && cells[i + 1, j - 1].IsAlive)
                {
                    neighbors++;
                }
            }

            if (iMinEdgeValid)
            {
                if (cells[i - 1, j].IsAlive)
                {
                    neighbors++;
                }

                if (jMaxEdgeValid && cells[i - 1, j + 1].IsAlive)
                {
                    neighbors++;
                }

                if (jMinEdgeValid && cells[i - 1, j - 1].IsAlive)
                {
                    neighbors++;
                }
            }

            if (jMaxEdgeValid && cells[i, j + 1].IsAlive)
            {
                neighbors++;
            }

            if (jMinEdgeValid && cells[i, j - 1].IsAlive)
            {
                neighbors++;
            }

            return neighbors;
        }

        public Cell[,] GetCells => cells;
    }
}
