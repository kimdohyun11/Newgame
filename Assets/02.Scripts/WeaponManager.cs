using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject _gun1_1;
    [SerializeField] GameObject _gun1_2;
    [SerializeField] GameObject _gun2_1;
    [SerializeField] GameObject _gun2_2;

    private void Start()
    {
        _gun1_2.SetActive(false);
        _gun2_1.SetActive(false);
    }

    public void WeaponChange()
    {
            _gun1_1.SetActive(false);
            _gun2_2.SetActive(false);
            _gun1_2.SetActive(true);
            _gun2_1.SetActive(true);
            _gun1_1.tag = "Weapon2";  
            _gun1_2.tag = "Weapon2";  
            _gun2_1.tag = "Weapon2";  
            _gun2_2.tag = "Weapon2";  
    }

    public void WeaponChange2()
    {
        _gun1_1.SetActive(true);
        _gun2_2.SetActive(true);
        _gun1_2.SetActive(false);
        _gun2_1.SetActive(false);
        _gun1_1.tag = "Weapon";
        _gun1_2.tag = "Weapon";
        _gun2_1.tag = "Weapon";
        _gun2_2.tag = "Weapon";
    }
}
