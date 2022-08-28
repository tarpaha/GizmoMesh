using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 1)]
public class Config : ScriptableObject
{
    [SerializeField]
    private float _minRadius = 1.0f;
    [SerializeField]
    private float _maxRadius = 5.0f;
    [SerializeField]
    private float _angle = 90.0f;
    [SerializeField]
    private int _sectors = 10;

    public float MinRadius => _minRadius;
    public float MaxRadius => _maxRadius;
    public float Angle => _angle;
    public int Sectors => _sectors;
}