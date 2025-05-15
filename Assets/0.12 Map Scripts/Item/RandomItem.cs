
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum ItemType {coin, potion, stat}
public enum RewardType {coin, potion, stat, boss, shop, non}
public enum Roomtype {BossRoom, ShopRroom, NormalRoom}

public class RandomItem : MonoBehaviour
{
    [SerializeField] private List<RoomProportion> _roomProportion;
    [SerializeField] private List<ItemGrideProportion> _itemGrideProportion;
    [SerializeField] private List<MonoBehaviour> _appearitemlist;

    private List<IItem> _statitemlist = new() , _bossitemlist = new List<IItem>(), _shopitemlist =new(), _overitemlist = new();
    private List<IItem> _potionList =new();
    private List<IItem> _coinList = new();

    private Dictionary<ItemType, Dictionary<ItemGrade, int>> itemproprtion = new Dictionary<ItemType, Dictionary<ItemGrade, int>>();
    private Dictionary<Roomtype, Dictionary<RewardType, int>> roomproprtion = new Dictionary < Roomtype, Dictionary<RewardType, int>>();

    private PlayerInventory _inventory;
    
    private void Start()
    {
        _inventory = GameObject.FindFirstObjectByType<PlayerInventory>().GetComponent<PlayerInventory>();
        foreach (var invenItem in _inventory.Inventory)
        {
            RemoveAppearanceItemList(invenItem);
        }
        ListSetting();
    }
    public IItem GetRandomItem(Roomtype room)
    {

        int probab = Random.Range(0, roomproprtion[room].Values.Sum());
        int sum = 0;
        RewardType compeclassif = RewardType.non;
        foreach (var classif in roomproprtion[room])
        {
            sum += classif.Value;
            if (probab < sum)
            {
                compeclassif = classif.Key;
                break;
            }
        }
        if (compeclassif == RewardType.non) return null;
        
        IItem compeitem = GetRandomReward(compeclassif);
        if (compeitem.GetItemData().type == ItemType.stat)
            RemoveItemList(compeitem);
        
        return compeitem;

    }

    private IItem GetRandomReward(RewardType rewardType) => rewardType switch
    {
        RewardType.stat => _statitemlist.Count != 0? _statitemlist[Random.Range(0, _statitemlist.Count)]: _overitemlist[Random.Range(0, _overitemlist.Count)],
        RewardType.boss => _bossitemlist.Count != 0? _bossitemlist[Random.Range(0, _bossitemlist.Count)]: GetRandomReward(RewardType.stat),
        RewardType.shop => _shopitemlist.Count != 0? _shopitemlist[Random.Range(0, _shopitemlist.Count)]: GetRandomReward(RewardType.stat),
        RewardType.coin => _coinList[Random.Range(0, _coinList.Count)],
        RewardType.potion => _potionList[Random.Range(0, _potionList.Count)],
        _=>null
        
    };

    private void RemoveItemList(IItem item)
    {
        for (int i = 0; i < Finditemproprtion(item); i++)
            FindItemList(item).Remove(item);
    }

    private void RemoveAppearanceItemList(IItem removeItem)
    {
        if(removeItem.GetItemData().type == ItemType.stat)
            _appearitemlist.Remove(removeItem as MonoBehaviour);
    }

    private void ListSetting()
    {
        foreach(var a in _itemGrideProportion)
        {
            if(!itemproprtion.TryGetValue(a.WhatItemType ,out Dictionary<ItemGrade,int> d))
            {
                d = new Dictionary<ItemGrade, int>();
                itemproprtion[a.WhatItemType] = d;
            }
            foreach(GradeWeight b in a.Weight)
            {
                itemproprtion[a.WhatItemType][b.Grade] = b.Weight ;
                
            }
        }

            foreach (var a in _roomProportion)
        {
            if(!roomproprtion.TryGetValue(a.Room, out Dictionary<RewardType, int> b))
            {
                b = new Dictionary<RewardType, int>();
                roomproprtion[a.Room] = b;
            }
            foreach (var d in a.itemProportions)
            {
                roomproprtion[a.Room][d.Reward] = d.Propor;
            }
        }
        foreach (MonoBehaviour c in _appearitemlist)
        {
            if(c is IItem a)
            {
                for (int i = 0; i < Finditemproprtion(a); i++)
                    FindItemList(a).Add(a);
                if (a.GetItemData().IsOverItem)
                {
                    _overitemlist.Add(a);
                }
            }
        }
    }

    private int Finditemproprtion(IItem a)
    {
        if(!itemproprtion.TryGetValue(a.GetItemData().type, out Dictionary<ItemGrade,int> b))
        {
            b = new Dictionary<ItemGrade, int>();
            itemproprtion[a.GetItemData().type] = b;
        }
        return itemproprtion[a.GetItemData().type][a.GetItemData().Grade];
    }
    private List<IItem> FindItemList(IItem item)
    {
        switch (item.GetItemData().type)
        {
            case ItemType.stat:
                return NewMethod(item);
            case ItemType.potion: return _potionList;
            case ItemType.coin: return _coinList;
            default: return null;
        }
    }

    private List<IItem> NewMethod(IItem item)
    {
        switch (item.GetItemData().Grade)
        {
            case ItemGrade.Shop: return _shopitemlist;
            case ItemGrade.Boss: return _bossitemlist;
            default: return _statitemlist;
        }
    }

}
