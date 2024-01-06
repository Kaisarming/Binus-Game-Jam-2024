using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Items
{
    protected override void CollectItem()
    {
        base.CollectItem();
        AudioManager.Instance.MainkanSuara("Coin");
        GameSetting.Instance.CollectItem(ItemType.Coin);
    }
}
