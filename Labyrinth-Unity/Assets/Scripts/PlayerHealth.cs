using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float _health;
    private bool _regenerate = false;
    private bool _isAlive = true;
    private readonly int _maxHealth = 100;
    private int _unitHealth = 1;
    private void Start()
    {
        _health = _maxHealth;
        InvokeRepeating("HealthUpdate", 1, 2);
    }

    private void RegenerationUpdate()
    {
        if(_regenerate == true && _isAlive == false)
        {
            _health += _unitHealth;
            if(_health > _maxHealth)
            {
                _health = _maxHealth;
                _regenerate = false;
            }
        }
    }

    private void ApplyDamage(int damage)
    {
        _health -= damage;

        if(_health < 0)
        {
            _health = 0;
            _isAlive = false;
        } else
        {
            StartCoroutine(DelayRegenerationCoroutine());
        }   

    }

    private IEnumerator DelayRegenerationCoroutine()
    {
        _regenerate = false;       
        yield return new WaitForSeconds(1);
        _regenerate = true;
    }
}
