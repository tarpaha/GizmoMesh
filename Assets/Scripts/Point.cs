using UnityEngine;

public class Point : MonoBehaviour
{
    private const float GizmoSphereRadius = 0.1f;
    private static readonly Color ColorOutside = Color.blue;
    private static readonly Color ColorInside = Color.red;
    
    [SerializeField]
    private Config _config;

    [SerializeField]
    private Transform _areaTransform;

    private void OnDrawGizmos()
    {
        var pointPosition = transform.position;
        Gizmos.color = IsPointInsideArea(
                _config.MinRadius, _config.MaxRadius, _config.Angle,
                _areaTransform.position,
                _areaTransform.rotation.eulerAngles.y,
                pointPosition)
            ? ColorInside
            : ColorOutside;
        Gizmos.DrawSphere(pointPosition, GizmoSphereRadius);
    }

    private static bool IsPointInsideArea(float minRadius, float maxRadius, float angle, Vector3 areaPosition, float areaRotationY, Vector3 pointPosition)
    {
        // need to determine point position in area space
        var localPointPosition = Quaternion.Inverse(Quaternion.Euler(0, areaRotationY, 0)) * (pointPosition - areaPosition);
        localPointPosition.y = 0;
        
        var distanceSqr = localPointPosition.sqrMagnitude;
        if (distanceSqr < minRadius * minRadius)
            return false;
        if (distanceSqr > maxRadius * maxRadius)
            return false;
        
        // starting from Z clockwise 
        var pointPositionAngle = Mathf.Atan2(localPointPosition.x, localPointPosition.z) * Mathf.Rad2Deg;
        
        return Mathf.Abs(pointPositionAngle) <= angle / 2;
    }
}