﻿using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace Life.UI
{
    public class Cell : BaseFocusHandler, IMixedRealityPointerHandler
    {
        [SerializeField]
        private MixedRealityInputAction selectAction = MixedRealityInputAction.None;

        private Renderer cellRenderer;

        private bool isHovered = false;
        private Color aliveColor;
        private Color hoverColor;

        private readonly Logic.Cell cell = new Logic.Cell();

        public static implicit operator Logic.Cell(Cell c) => c.cell;

        private void Start()
        {
            cellRenderer = GetComponent<Renderer>();
            Die();
        }

        /// <summary>
        /// Syncs the state with the underlying simulation.
        /// </summary>
        private void Update() => SetAlive(cell.IsAlive);

        public void SetColors(Color AliveColor, Color HoverColor)
        {
            aliveColor = AliveColor;
            hoverColor = HoverColor;
        }

        private void ChangeState() => SetAlive(!cell.IsAlive);

        private void SetAlive(bool alive)
        {
            if (alive)
            {
                Live();
            }
            else
            {
                Die();
            }
        }

        private void Die()
        {
            cell.Die();

            if (!isHovered)
            {
                cellRenderer.enabled = false;
            }
        }

        private void Live()
        {
            cell.Live();

            if (!isHovered)
            {
                cellRenderer.material.color = aliveColor;
                cellRenderer.enabled = true;
            }
        }

        public override void OnFocusEnter(FocusEventData eventData) => OnHover();
        //void OnSelect() => ChangeState();
        public override void OnFocusExit(FocusEventData eventData) => OnReset();

        //private void OnMouseEnter() => OnHover();
        //private void OnMouseDown() => ChangeState();
        //private void OnMouseExit() => OnReset();

        private void OnHover()
        {
            isHovered = true;
            cellRenderer.enabled = true;
            cellRenderer.material.color = hoverColor;
        }

        private void OnReset()
        {
            isHovered = false;

            if (cell.IsAlive)
            {
                cellRenderer.material.color = aliveColor;
            }
            else
            {
                cellRenderer.enabled = false;
            }
        }

        void IMixedRealityPointerHandler.OnPointerDown(MixedRealityPointerEventData eventData)
        {
            if (eventData.MixedRealityInputAction == selectAction)
            {
                ChangeState();
            }
        }

        void IMixedRealityPointerHandler.OnPointerDragged(MixedRealityPointerEventData eventData) { }

        void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData) { }

        void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData) { }
    }
}
