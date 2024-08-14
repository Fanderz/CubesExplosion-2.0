using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _explosionRadius = 10f;

    private Cube cube;

    private void Start()
    {
        cube = GetComponent<Cube>();
        cube.OnDestroyExplosion += ExplodeWithoutSeparation;
        cube.OnBaseExplosion += BaseExplosion;
    }

    private void ExplodeWithoutSeparation()
    {
        cube.OnBaseExplosion -= BaseExplosion;

        Collider[] hittedColliders = Physics.OverlapBox(transform.position, transform.localScale * _explosionRadius);
        float forceDecrement = transform.localScale.x;

        if (hittedColliders.Length > 0)
        {
            for (int i = 0; i < hittedColliders.Length; i++)
            {
                if (hittedColliders[i].attachedRigidbody)
                {
                    float distance = Vector3.Distance(transform.position, hittedColliders[i].transform.position);

                    if (distance != 0)
                    { 
                        hittedColliders[i].attachedRigidbody.AddExplosionForce(_explosionForce / distance / forceDecrement, transform.position, _explosionRadius / forceDecrement);
                    }
                }
            }
        }

        cube.OnDestroyExplosion -= ExplodeWithoutSeparation;
    }

    private void BaseExplosion()
    {
        GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        cube.OnBaseExplosion -= BaseExplosion;
    }
}
