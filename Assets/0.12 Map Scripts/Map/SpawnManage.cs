
using System.Collections.Generic;
using UnityEngine;

public class SpawnManage : MonoBehaviour
{
    [Header("enemysponpoint")]
    [SerializeField] private Transform[] _enemySponPoint;
    [SerializeField] private GameObject[] _sponEnmyList;


    [Header("clearCompen")]
    [SerializeField] private Transform[] _compenSponPoint;
    [Space]
    [SerializeField] private bool _isBossroom = false;
    private DoorManage _doorManage;
    private bool _isCurrentRoom = false;
    private RandomItem _randomItem;
    private ItempoolCreight _itempool;
    private void OnEnable()
    {
        _doorManage = GameObject.FindFirstObjectByType<DoorManage>().GetComponent<DoorManage>();
        _randomItem = GameObject.FindFirstObjectByType<RandomItem>().GetComponent<RandomItem>();
        _itempool = GameObject.FindFirstObjectByType<ItempoolCreight>().GetComponent<ItempoolCreight>();
        EnemySpawn();

    }
    private void Update()
    {
        //이곳이 currentRoom이고 에너미가 없다면
        if (!GameObject.FindFirstObjectByType<Enemy>() && _isCurrentRoom)
        {
            _doorManage.CurrentRoomClear();
            SponCompen();
            gameObject.SetActive(false);
        }
    }

    public void EnemySpawn()
    {
        for (int i = 0; i < _enemySponPoint.Length; i++)
            GameObject.Instantiate(_sponEnmyList[Random.Range(0, _sponEnmyList.Length)], _enemySponPoint[i].position, Quaternion.identity);
        _isCurrentRoom = true;
    }
    private void SponCompen()
    {
        for (int i = 0; i < _compenSponPoint.Length; i++)
        {
            IItem item = _randomItem.GetRandomItem(_isBossroom?Roomtype.BossRoom: Roomtype.NormalRoom);
            if (item == null) return;

            GameObject[] pool = _itempool.Itempool;
            for (int n = 0; n < pool.Length; n++)
            {
                if (!pool[n].activeSelf)
                {
                    pool[n].SetActive(true);
                    SpriteRenderer sprite = pool[n].GetComponentInChildren<SpriteRenderer>();
                    sprite.sprite = item.GetItemData().Icon;

                    pool[n].transform.SetParent(_compenSponPoint[i]);
                    pool[n].transform.position = _compenSponPoint[i].position;

                    if (item is MonoBehaviour itsc)
                    {
                        Component additem = pool[n].AddComponent(itsc.GetType());
                        ((IItem)additem).SetitemData(item.GetItemData());
                    }
                    break;
                }
            }

        }
    }

}
