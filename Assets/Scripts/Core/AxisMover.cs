using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AhmadAllahham.Shared;

namespace AhmadAllahham.Core
{
    [RequireComponent(typeof(Collider))]
    public class AxisMover : MonoBehaviour
    {
        public AxisType axisType;

        [SerializeField] private Collider XYPlaneCollider;
        [SerializeField] private Collider ZYPlaneCollider;

        private bool _dragging;

        private void FixedUpdate()
        {
            if (!_dragging) return;

            switch (axisType)
            {
                case AxisType.X:
                    MoveAlongX();
                    break;
                case AxisType.Y:
                    MoveAlongY();
                    break;
                case AxisType.Z:
                    MoveAlongZ();
                    break;

            }
        }

        private void MoveAlongX()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (XYPlaneCollider.Raycast(ray, out hit, 100.0f))
            {
                transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z);
            }
        }

        private void MoveAlongY()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (XYPlaneCollider.Raycast(ray, out hit, 100.0f))
            {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
        }

        private void MoveAlongZ()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (ZYPlaneCollider.Raycast(ray, out hit, 100.0f))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, hit.point.z);
            }
        }

        private void OnMouseDown()
        {
            _dragging = true;
        }

        private void OnMouseUp()
        {
            _dragging = false;
        }
    }
}