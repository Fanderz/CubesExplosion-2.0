using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _currentSeparationChance = 100;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;

    public event Action<Cube> OnHitObject;
    public event Action OnDestroyExplosion;
    public event Action OnBaseExplosion;

    public int CurrentSeparationChance => _currentSeparationChance;

    private void OnMouseDown()
    {
        int maxSeparationChance = 100;

        if (_currentSeparationChance == maxSeparationChance)
        {
            OnHitObject?.Invoke(this);
            OnBaseExplosion?.Invoke();
        }
        else if (UnityEngine.Random.Range(0, maxSeparationChance / _currentSeparationChance) == 1)
        {
            OnHitObject?.Invoke(this);
            OnBaseExplosion?.Invoke();
        }
        else
        {
            OnDestroyExplosion?.Invoke();
        }

        Destroy(gameObject, Time.deltaTime);
    }

    public void UpdateSeparationChance(int chance) =>
        _currentSeparationChance = chance;

    public void SetColor(Color color) =>
        _renderer.material.color = color;
}
