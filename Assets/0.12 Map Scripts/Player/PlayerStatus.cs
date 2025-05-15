using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerStatus : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int _hp,_hpMax;
    [SerializeField] private float _invincibleTime;
    [SerializeField] private float _speed;
    [SerializeField] private int _coin,_coinMax;
    public int Hpmax {
        get => _hpMax;
        set => _hpMax = Mathf.Clamp(value, 0, 40); }

    public int Hp {
        get => _hp;
        set => _hp = Mathf.Clamp(value, 0, _hpMax); }

    public float Speed {
        get => _speed;
        set => _speed = Mathf.Clamp(value, 0.1f, 20f); }

    public float InvincibleTime {
        get => _invincibleTime; 
        set => _invincibleTime = Mathf.Clamp(value, 0.1f, 3f); }
    
    public int Coinmax {
        get => _coinMax;
        set => _coin = Mathf.Clamp(value, 1, 999); }
    public int Coin {
        get => _coin;
        set => _coin = Mathf.Clamp(value, 0, _coinMax); }

    private bool _isInvincible = false;


    #region Method

    public void BeingDamaged(int damge)
    {
        if (!_isInvincible)
        {
            _isInvincible = true;
            Damage(damge);
            StartCoroutine(startInvincible());
            _isInvincible = false;
        }
    }

    private void Damage(int damge)
    {
        Hp -= damge;
        if (Hp <= 0)
            Dead();
    }
    private IEnumerator startInvincible()
    {
        yield return new WaitForSeconds(InvincibleTime);
    }



    private void Dead()
    {
        Debug.Log("Dead");
    }
    #endregion
}
