using UnityEngine;

namespace Life.UI
{
    public class Cell : MonoBehaviour
    {
        private Renderer cellRenderer;

        private Color aliveColor;
        private Color hoverColor;

        private Logic.Cell cell = new Logic.Cell();

        void Awake()
        {
            cellRenderer = GetComponent<Renderer>();
            Die();
        }

        public void SetColors(Color AliveColor, Color HoverColor)
        {
            aliveColor = AliveColor;
            hoverColor = HoverColor;
        }

        public void ChangeState()
        {
            if (cell.IsAlive)
            {
                Die();
            }
            else
            {
                Live();
            }
        }

        public void Die()
        {
            cell.Die();
            cellRenderer.enabled = false;
        }

        public void Live()
        {
            cell.Live();
            cellRenderer.enabled = true;
            cellRenderer.material.color = aliveColor;
        }

        public bool IsAlive => cell.IsAlive;

        void OnGazeEnter()
        {
            cellRenderer.enabled = true;
            cellRenderer.material.color = hoverColor;
        }

        void OnGazeLeave()
        {
            if (cell.IsAlive)
            {
                cellRenderer.material.color = aliveColor;
            }
            else
            {
                cellRenderer.enabled = false;
            }
        }

        private void OnMouseEnter()
        {
            cellRenderer.enabled = true;
            cellRenderer.material.color = hoverColor;
        }

        private void OnMouseExit()
        {
            if (cell.IsAlive)
            {
                cellRenderer.material.color = aliveColor;
            }
            else
            {
                cellRenderer.enabled = false;
            }
        }

        void OnSelect()
        {
            ChangeState();
        }
    }
}
