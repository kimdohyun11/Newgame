using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int _playerHp = 10;
    [SerializeField] private int _playerMaxHp = 20;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _invincibleTime = 1f;
    public int PlayerHp { get { return _playerHp; } set {_playerHp = Mathf.Clamp(value, 0, _playerMaxHp); } }
    public float PlayerSpeed{ get {return _speed;} set{_speed = value;}}
    public float InvincibleTime { get { return _invincibleTime; } }
    

    [Header("Inventory")]
    [SerializeField] private int _conis;
}
