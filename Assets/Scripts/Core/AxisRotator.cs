using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AhmadAllahham.Shared;

namespace AhmadAllahham.Core
{
    public class AxisRotator : MonoBehaviour
    {
        public AxisType axisType;

        [SerializeField] private Transform objectToRotate;

        [SerializeField] private Collider XYPlaneCollider;
        [SerializeField] private Collider ZYPlaneCollider;

        Vector3 _initialXYHitPoint;
        Vector3 _initialZYHitPoint;
        Vector3 _prevEulerAngles;

        private bool _dragging;

        private void FixedUpdate()
        {
            if (!_dragging) return;

            switch (axisType)
            {
                case AxisType.X:
                    RotateAlongX();
                    break;
                case AxisType.Y:
                    RotateAlongY();
                    break;
                case AxisType.Z:
                    RotateAlongZ();
                    break;

            }
        }

        private void RotateAlongX()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (ZYPlaneCollider.Raycast(ray, out hit, 100.0f))
            {
                var lookRotation = Quaternion.LookRotation(_initialZYHitPoint-hit.point);
                float xEulerAngle = Mathf.Abs(lookRotation.eulerAngles.y) > 90 ? lookRotation.eulerAngles.x + 2 * (90 - lookRotation.eulerAngles.x) : lookRotation.eulerAngles.x;
                xEulerAngle += _prevEulerAngles.x;
                lookRotation.eulerAngles = new Vector3(xEulerAngle, objectToRotate.rotation.y, objectToRotate.rotation.z);
                objectToRotate.rotation = lookRotation;
            }
        }

        private void RotateAlongY()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (XYPlaneCollider.Raycast(ray, out hit, 100.0f))
            {
                var lookRotation = Quaternion.LookRotation(_initialXYHitPoint - hit.point);
                float yEulerAngle = Mathf.Abs(lookRotation.eulerAngles.x) > 90 ? lookRotation.eulerAngles.y + 2 * (90 - lookRotation.eulerAngles.y) : lookRotation.eulerAngles.y;
                yEulerAngle += _prevEulerAngles.y;
                lookRotation.eulerAngles = new Vector3(objectToRotate.rotation.x, yEulerAngle, objectToRotate.rotation.z);
                objectToRotate.rotation = lookRotation;
            }
        }

        private void RotateAlongZ()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (ZYPlaneCollider.Raycast(ray, out hit, 100.0f))
            {
                var lookRotation = Quaternion.LookRotation(_initialZYHitPoint - hit.point);
                float zEulerAngle = Mathf.Abs(lookRotation.eulerAngles.y) > 90 ? lookRotation.eulerAngles.z + 2 * (90 - lookRotation.eulerAngles.z) : lookRotation.eulerAngles.z;
                zEulerAngle += _prevEulerAngles.z;
                lookRotation.eulerAngles = new Vector3(objectToRotate.rotation.x, objectToRotate.rotation.y, zEulerAngle);
                objectToRotate.rotation = lookRotation;
            }
        }

        private void OnMouseDown()
        {
            Logger.Instance.LogInfo($"Rotating {gameObject.name} from {transform.eulerAngles}");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (XYPlaneCollider.Raycast(ray, out hit, 100.0f))
            {
                _initialXYHitPoint = hit.point;
            }

            if (ZYPlaneCollider.Raycast(ray, out hit, 100.0f))
            {
                _initialZYHitPoint = hit.point;
            }

            _dragging = true;
        }

        private void OnMouseUp()
        {
            Logger.Instance.LogInfo($"Rotating {gameObject.name} to {transform.eulerAngles}");

            _prevEulerAngles = objectToRotate.eulerAngles;

            _dragging = false;
        }
    }
}