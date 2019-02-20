using UnityEngine;

[CreateAssetMenu(fileName = "StoreData", menuName = "StoreItem", order = 1)]
public class StoreProductInfo : ScriptableObject {
    //Consumables.
    public Sprite paracetamolImage;
    public string paracetamolName;
    public string paracetamolDesc;    
    public int paracetamolPrice;
    public Sprite armorImage;
    public string armorName;
    public string armorDesc;
    public int armorPrice;
    public Sprite coffeeImage;
    public string coffeeName;
    public string coffeeDesc;
    public int coffePrice;

    //Others.
    public Sprite miniradarImage;
    public string miniradarName;
    public string miniradarDesc;
    public int miniradarPrice;
    public Sprite multimeterImage;
    public string multimeterName;
    public string multimeterDesc;
    public int multimeterPrice;

    //Weapons.
    public Sprite batImage;
    public string batName;
    public string batDesc;
    public int batPrice;
    public Sprite smgImage;
    public string smgName;
    public string smgDesc;
    public int smgPrice;
    public Sprite akImage;
    public string akName;
    public string akDesc;
    public int akPrice;
    public Sprite shotgunImage;
    public string shotgunName;
    public string shotgunDesc;
    public int shotgunPrice;
    public Sprite rpgImage;
    public string rpgName;
    public string rpgDesc;
    public int rpgPrice;

    //Ammo.
    public Sprite pistolAmmoImage;
    public string pistolAmmoName;
    public string pistolAmmoDesc;
    public int pistolAmmoPrice;
    public Sprite smgAmmoImage;
    public string smgAmmoName;
    public string smgAmmoDesc;
    public int smgAmmoPrice;
    public Sprite akAmmoImage;
    public string akAmmoName;
    public string akAmmoDesc;
    public int akAmmoPrice;
    public Sprite shotgunAmmoImage;
    public string shotgunAmmoName;
    public string shotgunAmmoDesc;
    public int shotgunAmmoPrice;
}
