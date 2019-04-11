using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health { get => _health; }
    public bool IsAlive { get => _health > 0; }

    [SerializeField] private float _maxHealth = 100;
    private float _health;
    private bool _regenerate = false;
    private float _regeneraionUnit = 0.01f;
    private float _regenerationDelay = 2f;

    private void OnValidate()
    {
        _maxHealth = (_maxHealth < 0) ? 0 : _maxHealth;
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if (_regenerate)
        {
            _health += _regeneraionUnit;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
                _regenerate = false;
            }
        }
    }

    private void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health < 0)
        {
            _health = 0;
            _regenerate = false;
        }
        else
        {
            StartCoroutine(DelayRegenerationCoroutine());
        }
    }

    private IEnumerator DelayRegenerationCoroutine()
    {
        StopAllCoroutines();
        _regenerate = false;
        yield return new WaitForSeconds(_regenerationDelay);
        _regenerate = true;
    }
}