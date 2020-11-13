using UnityEngine;

public class SingleProjectileInstantiater : ProjectileInstantiater
{
    private GameObject prefab;
    private Vector3 position;
    private Quaternion rotation;

    public SingleProjectileInstantiater(
        GameObject _prefab, Vector3 _position, Quaternion _rotation)
    {
        prefab = _prefab;
        position = _position;
        rotation = _rotation;
    }

    public override void Instantiate()
    {
        ObjectPooler.Instantiate(prefab, position, rotation);
    }
}
