using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour {
    #region visualsDeclaration
    public TextMeshProUGUI descriptions;
    //Consumables.
    public Image paracetamolImage;
    public TextMeshProUGUI paracetamolName;
    public TextMeshProUGUI paracetamolPrice;
    public Image armorImage;
    public TextMeshProUGUI armorName;
    public TextMeshProUGUI armorPrice;
    public Image coffeeImage;
    public TextMeshProUGUI coffeeName;
    public TextMeshProUGUI coffePrice;

    //Others.
    public Image miniradarImage;
    public TextMeshProUGUI miniradarName;
    public TextMeshProUGUI miniradarPrice;
    public Image multimeterImage;
    public TextMeshProUGUI multimeterName;
    public TextMeshProUGUI multimeterPrice;

    //Weapons.
    public Image batImage;
    public TextMeshProUGUI batName;
    public TextMeshProUGUI batPrice;
    public Image smgImage;
    public TextMeshProUGUI smgName;
    public TextMeshProUGUI smgPrice;
    public Image akImage;
    public TextMeshProUGUI akName;
    public TextMeshProUGUI akPrice;
    public Image shotgunImage;
    public TextMeshProUGUI shotgunName;
    public TextMeshProUGUI shotgunPrice;
    public Image rpgImage;
    public TextMeshProUGUI rpgName;
    public TextMeshProUGUI rpgPrice;

    //Ammo.
    public Image pistolAmmoImage;
    public TextMeshProUGUI pistolAmmoName;
    public TextMeshProUGUI pistolAmmoPrice;
    public Image smgAmmoImage;
    public TextMeshProUGUI smgAmmoName;
    public TextMeshProUGUI smgAmmoPrice;
    public Image akAmmoImage;
    public TextMeshProUGUI akAmmoName;
    public TextMeshProUGUI akAmmoPrice;
    public Image shotgunAmmoImage;
    public TextMeshProUGUI shotgunAmmoName;
    public TextMeshProUGUI shotgunAmmoPrice;
    #endregion

    //GUI
    private bool akAgotada = false;
    private bool multimVendido = false;
    public TextMeshProUGUI akButtonText;
    public TextMeshProUGUI multimButtonText;
    public TextMeshProUGUI shopText;
    public GameObject storePanel;
    
    //Sounds
    private AudioSource storeAudio;
    public AudioClip hover;
    public AudioClip error;
    public AudioClip sold;

    public StoreProductInfo info; 
    enum Guns { Pistol, AK47 };
    private PlayerController player;
    public static StoreManager instance;
    public bool storePanelOpen;

    void Start() {
        storePanelOpen = false;
        player = PlayerController.instance;
        instance = this;
        storeAudio = GetComponent<AudioSource>();

        //Asignación de valores de consumibles.
        paracetamolImage.sprite = info.paracetamolImage;
        paracetamolName.SetText(info.paracetamolName);
        paracetamolPrice.SetText(info.paracetamolPrice.ToString());
        armorImage.sprite = info.armorImage;
        armorName.SetText(info.armorName);
        armorPrice.SetText(info.armorPrice.ToString());
        coffeeImage.sprite = info.coffeeImage;
        coffeeName.SetText(info.coffeeName);
        coffePrice.SetText(info.coffePrice.ToString());
        //Asignación de valores de otros.
        miniradarImage.sprite = info.miniradarImage;
        miniradarName.SetText(info.miniradarName);
        miniradarPrice.SetText(info.miniradarPrice.ToString());
        multimeterImage.sprite = info.multimeterImage;
        multimeterName.SetText(info.multimeterName);
        multimeterPrice.SetText(info.multimeterPrice.ToString());
        //Asignación de valores de armas.
        batImage.sprite = info.batImage;
        batName.SetText(info.batName);
        batPrice.SetText(info.batPrice.ToString());
        smgImage.sprite = info.smgImage;
        smgName.SetText(info.smgName);
        smgPrice.SetText(info.smgPrice.ToString());
        akImage.sprite = info.akImage;
        akName.SetText(info.akName);
        akPrice.SetText(info.akPrice.ToString());
        shotgunImage.sprite = info.shotgunImage;
        shotgunName.SetText(info.shotgunName);
        shotgunPrice.SetText(info.shotgunPrice.ToString());
        rpgImage.sprite = info.rpgImage;
        rpgName.SetText(info.rpgName);
        rpgPrice.SetText(info.rpgPrice.ToString());
        //Asignación de valores de balas.
        pistolAmmoImage.sprite = info.pistolAmmoImage;
        pistolAmmoName.SetText(info.pistolAmmoName);
        pistolAmmoPrice.SetText(info.pistolAmmoPrice.ToString());
        smgAmmoImage.sprite = info.smgAmmoImage;
        smgAmmoName.SetText(info.smgAmmoName);
        smgAmmoPrice.SetText(info.smgAmmoPrice.ToString());
        akAmmoImage.sprite = info.akAmmoImage;
        akAmmoName.SetText(info.akAmmoName);
        akAmmoPrice.SetText(info.akAmmoPrice.ToString());
        shotgunAmmoImage.sprite = info.shotgunAmmoImage;
        shotgunAmmoName.SetText(info.shotgunAmmoName);
        shotgunAmmoPrice.SetText(info.shotgunAmmoPrice.ToString());


    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            shopText.SetText("Presiona 'E' para comprar.");
            shopText.gameObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.E)) {
                OpenShop();
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            shopText.gameObject.SetActive(false);
        }
    }

    public void OpenShop() {
        storePanelOpen = true;
        storePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePanel() {
        storePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        storePanelOpen = false;
    }

    public void BuyLife() {
        if (player.currentMoney >= info.paracetamolPrice) {
            player.currentMoney -= info.paracetamolPrice;
            player.currentLife = player.maxLife;
            HUDManager.instance.setLifeLevel(player.currentLife, player.maxLife);
            HUDManager.instance.setMoney(player.currentMoney);
            storeAudio.PlayOneShot(sold);
        }
        else {
            storeAudio.PlayOneShot(error);
            descriptions.SetText("No me alcanza para comprar ese producto.");
            descriptions.color = Color.red;
        }
    }

    public void BuyShield() {
        if (player.currentMoney >= info.armorPrice) {
            player.currentMoney -= info.armorPrice;
            player.currentShield = player.maxShield;
            HUDManager.instance.setShieldLevel(player.currentShield, player.maxShield);
            HUDManager.instance.setMoney(player.currentMoney);
            storeAudio.PlayOneShot(sold);
        }
        else {
            storeAudio.PlayOneShot(error);
            descriptions.SetText("No me alcanza para comprar ese producto.");
            descriptions.color = Color.red;
        }
    }

    public void BuySpeed() {
        if (player.currentMoney >= info.coffePrice) {
            player.currentMoney -= info.coffePrice;
            player.speed *= 2;
            StartCoroutine(ReturnSpeed());
            HUDManager.instance.setMoney(player.currentMoney);
            storeAudio.PlayOneShot(sold);
        }
        else {
            storeAudio.PlayOneShot(error);
            descriptions.SetText("No me alcanza para comprar ese producto.");
            descriptions.color = Color.red;
        }
    }

    public void BuyMultimeter() {
        if (player.currentMoney >= info.multimeterPrice && !multimVendido && ObjectivesManager.instance.currentObjective == 3) {
            player.currentMoney -= info.multimeterPrice;
            HUDManager.instance.setMoney(player.currentMoney);
            multimButtonText.SetText("Agotado");
            multimVendido = true;
            storeAudio.PlayOneShot(ObjectivesManager.instance.completedAudio);
            ObjectivesManager.instance.StartObjective4();
            ClosePanel();
        }
        else {
            storeAudio.PlayOneShot(error);
            descriptions.color = Color.red;
            if (multimVendido) descriptions.SetText("Este producto ya está en mi inventario.");
            else if (ObjectivesManager.instance.currentObjective != 2 && !multimVendido) descriptions.SetText("No me conviene gastar mi dinero en eso.");
            else descriptions.SetText("No me alcanza para comprar ese producto.");
        }
    }

    public void BuyAK() {
        if (player.currentMoney >= info.akPrice && !akAgotada) {
            player.currentMoney -= info.akPrice;
            player.inventory[(int)Guns.AK47].inInventory = true;
            HUDManager.instance.setMoney(player.currentMoney);
            akButtonText.SetText("Agotado");
            akAgotada = true;
            storeAudio.PlayOneShot(sold);
        }
        else {
            storeAudio.PlayOneShot(error);
            descriptions.color = Color.red;
            if (akAgotada) descriptions.SetText("Esta arma ya está en mi inventario.");
            else descriptions.SetText("No me alcanza para comprar ese producto.");
        }
    }

    public void BuyPistolBullets() {
        if (player.currentMoney >= info.pistolAmmoPrice) {
            player.currentMoney -= info.pistolAmmoPrice;
            player.inventory[(int)Guns.Pistol].currentClips += 3;
            HUDManager.instance.setClips(player.inventory[(int)Guns.Pistol].currentClips);
            HUDManager.instance.setMoney(player.currentMoney);
            storeAudio.PlayOneShot(sold);
        }
        else {
            storeAudio.PlayOneShot(error);
            descriptions.SetText("No me alcanza para comprar ese producto.");
            descriptions.color = Color.red;
        }
    }

    public void BuyAKBullets() {
        if (player.currentMoney >= info.akAmmoPrice) {
            player.currentMoney -= info.akAmmoPrice;
            player.inventory[(int)Guns.AK47].currentClips += 3;
            HUDManager.instance.setClips(player.inventory[(int)Guns.AK47].currentClips);
            HUDManager.instance.setMoney(player.currentMoney);
            storeAudio.PlayOneShot(sold);
        }
        else {
            storeAudio.PlayOneShot(error);
            descriptions.SetText("No me alcanza para comprar ese producto.");
            descriptions.color = Color.red;
        }        
    }

    IEnumerator ReturnSpeed() {
        yield return new WaitForSeconds(60.0f);
        player.speed /= 2;
    }

    public void ShowDefaultDesc() {
        descriptions.color = Color.gray;
        descriptions.SetText("Pasa el mouse sobre un objeto para ver su descripción.");
    }

    public void ShowParacetamolDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.paracetamolDesc);
    }

    public void ShowArmorDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.armorDesc);
    }

    public void ShowCoffeeDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.coffeeDesc);
    }

    public void ShowMiniradarDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.miniradarDesc);
    }

    public void ShowMultimeterDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.multimeterDesc);
    }

    public void ShowBatDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.batDesc);
    }

    public void ShowSMGDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.smgDesc);
    }

    public void ShowAKDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.akDesc);
    }

    public void ShowShotgunDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.shotgunDesc);
    }

    public void ShowRPGDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.rpgDesc);
    }

    public void ShowPistolAmmoDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.pistolAmmoDesc);
    }

    public void ShowSMGAmmoDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.smgAmmoDesc);
    }

    public void ShowAKAmmoDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.akAmmoDesc);
    }

    public void ShowShotgunAmmoDesc() {
        storeAudio.PlayOneShot(hover);
        descriptions.color = Color.white;
        descriptions.SetText(info.shotgunAmmoDesc);
    }
}
