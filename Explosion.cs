using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _explosionRadius = 10f;

    public event Action OnExplosion;

    private void Start()
    {
        OnExplosion += Explode;
    }

    private void OnMouseDown()
    {
        OnExplosion?.Invoke();
    }

    public void Explode()
    {
        Collider[] hittedColliders = Physics.OverlapBox(this.transform.position, transform.localScale * _explosionRadius);
        float forceDecrement = transform.localScale.x;

        if (hittedColliders.Length > 0)
        {
            for (int i = 0; i < hittedColliders.Length; i++)
            {
                if (hittedColliders[i].attachedRigidbody)
                {
                    float distance = Vector3.Distance(this.transform.position, hittedColliders[i].transform.position);

                    hittedColliders[i].attachedRigidbody.AddExplosionForce(_explosionForce / distance / forceDecrement, transform.position, _explosionRadius / forceDecrement);
                }
            }
        }

        OnExplosion -= Explode;
    }
}
