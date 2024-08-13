using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _currentSeparationChance = 100;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;

    public event Action<Cube> OnHitObject;

    public int CurrentSeparationChance => _currentSeparationChance;

    private void OnMouseDown()
    {
        int maxSeparationChance = 100;

        if (_currentSeparationChance == maxSeparationChance)
        {
            OnHitObject?.Invoke(this);
        }
        else if (UnityEngine.Random.Range(0, maxSeparationChance / _currentSeparationChance) == 1)
        {
            OnHitObject?.Invoke(this);
        }

        Destroy(gameObject, Time.deltaTime);
    }

    public void UpdateSeparationChance(int chance) =>
        _currentSeparationChance = chance;

    public void SetColor(Color color) =>
        _renderer.material.color = color;
}
