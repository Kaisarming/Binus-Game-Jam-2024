using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Items
{
    protected override void CollectItem()
    {
        base.CollectItem();
        GameSetting.Instance.CollectItem(ItemType.Coin);
    }
}
