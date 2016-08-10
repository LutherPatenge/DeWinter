using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BuyAndSell : MonoBehaviour {
    public GameObject screenFader; // It's for the BuyAndSell pop-up
    public string inventoryType;

    public OutfitInventoryList personalInventoryList;
    public OutfitInventoryList merchantInventoryList;

    void Start()
    {
        personalInventoryList = GameObject.Find("WardrobeDisplay").transform.Find("OutfitListPanel").Find("GridWithElements").GetComponent<OutfitInventoryList>();
        merchantInventoryList = GameObject.Find("ShopDisplay").transform.Find("OutfitListPanel").Find("GridWithElements").GetComponent<OutfitInventoryList>();
    }

    public void createBuyAndSellPopUp()
    {
        if (inventoryType == "personal" && personalInventoryList.selectedInventoryOutfit != -1) 
        {
            object[] objectStorage = new object[2];
            objectStorage[0] = "personal";
            objectStorage[1] = personalInventoryList.selectedInventoryOutfit;
            screenFader.gameObject.SendMessage("CreateBuyOrSellModal", objectStorage);
        } else if (inventoryType == "merchant" && merchantInventoryList.selectedInventoryOutfit != -1)
        {
            if (OutfitInventory.personalInventory.Count < OutfitInventory.personalInventoryMaxSize) // Will it fit in the Player's inventory?
            {
                if (GameData.moneyCount >= OutfitInventory.merchantInventory[merchantInventoryList.selectedInventoryOutfit].price) // Can they afford it?
                {
                    object[] objectStorage = new object[2];
                    objectStorage[0] = "merchant";
                    objectStorage[1] = merchantInventoryList.selectedInventoryOutfit;
                    screenFader.gameObject.SendMessage("CreateBuyOrSellModal", objectStorage);
                } else //If the Player can't afford it give them the Can't Afford Modal
                {
                    object[] objectStorage = new object[1];
                    objectStorage[0] = merchantInventoryList.selectedInventoryOutfit;
                    screenFader.gameObject.SendMessage("CreateCantAffordModal", objectStorage);
                }
            } else // If it won't fit then give them the "Can't Fit" Modal (hurr hurr hurr)
            {
                object[] objectStorage = new object[2];
                objectStorage[0] = merchantInventoryList.selectedInventoryOutfit;
                objectStorage[1] = OutfitInventory.personalInventoryMaxSize;
                screenFader.gameObject.SendMessage("CreateCantFitModal", objectStorage);
            }
           
        }
    }
}
