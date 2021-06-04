using Life.Logic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private Simulation simulation = null;
        private bool isRunning = false;
        private Cell[,] cells;
        private long lastUpdateTime = DateTime.Now.Ticks;

        private const long UpdateInterval = 5000000;

        private void Awake()
        {
            float halfLength = length / 2.0f;
            float halfWidth = width / 2.0f;
            float cellLength = cell.transform.localScale.x;
            float cellWidth = cell.transform.localScale.y;

            cells = new Cell[length, width];
            Logic.Cell[,] logicCells = new Logic.Cell[length, width];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0)
                    {
                        GameObject borderCube = Instantiate(cell, transform);
                        borderCube.transform.localPosition = new Vector3((i - 1 - halfLength) * cellLength, (j - halfWidth) * cellWidth, 0);

                        if (j == 0)
                        {
                            GameObject borderCornerCube = Instantiate(cell, transform);
                            borderCornerCube.transform.localPosition = new Vector3((i - 1 - halfLength) * cellLength, (j - 1 - halfWidth) * cellWidth, 0);
                        }
                        else if (j == width - 1)
                        {
                            GameObject borderCornerCube = Instantiate(cell, transform);
                            borderCornerCube.transform.localPosition = new Vector3((i - 1 - halfLength) * cellLength, (j + 1 - halfWidth) * cellWidth, 0);
                        }
                    }
                    else if (i == length - 1)
                    {
                        GameObject borderCube = Instantiate(cell, transform);
                        borderCube.transform.localPosition = new Vector3((i + 1 - halfLength) * cellLength, (j - halfWidth) * cellWidth, 0);

                        if (j == 0)
                        {
                            GameObject borderCornerCube = Instantiate(cell, transform);
                            borderCornerCube.transform.localPosition = new Vector3((i + 1 - halfLength) * cellLength, (j - 1 - halfWidth) * cellWidth, 0);
                        }
                        else if (j == width - 1)
                        {
                            GameObject borderCornerCube = Instantiate(cell, transform);
                            borderCornerCube.transform.localPosition = new Vector3((i + 1 - halfLength) * cellLength, (j + 1 - halfWidth) * cellWidth, 0);
                        }
                    }

                    if (j == 0)
                    {
                        GameObject borderCube = Instantiate(cell, transform);
                        borderCube.transform.localPosition = new Vector3((i - halfLength) * cellLength, (j - 1 - halfWidth) * cellWidth, 0);
                    }
                    else if (j == width - 1)
                    {
                        GameObject borderCube = Instantiate(cell, transform);
                        borderCube.transform.localPosition = new Vector3((i - halfLength) * cellLength, (j + 1 - halfWidth) * cellWidth, 0);
                    }

                    GameObject cube = Instantiate(cell, transform);
                    cube.transform.localPosition = new Vector3((i - halfLength) * cellLength, (j - halfWidth) * cellWidth, 0);
                    Cell cellComponent = cube.AddComponent<Cell>();
                    cellComponent.SetColors(aliveColor, hoverColor);
                    cells[i, j] = cellComponent;
                    logicCells[i, j] = cellComponent;
                }
            }

            simulation = new Simulation(length, width, logicCells);
        }

        private void Update()
        {
            if (Keyboard.current[Key.S].wasPressedThisFrame)
            {
                isRunning = !isRunning;
            }

            if (isRunning)
            {
                long currentTime = DateTime.Now.Ticks;
                if ((currentTime - lastUpdateTime) > UpdateInterval)
                {
                    lastUpdateTime = currentTime;
                    simulation.Update();
                }
            }
        }

        public void SetRunning(bool toStart)
        {
            isRunning = toStart;
        }
    }
}
