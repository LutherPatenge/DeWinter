using UnityEngine;
using System.Collections;

public class BuyAndSellPopUpController : MonoBehaviour {
    public string inventoryType;
    public int inventoryNumber;
    public OutfitInventoryList personalInventoryList;
    public OutfitInventoryList merchantInventoryList;
    public OutfitInventory outfitInventory;

    void Start()
    {
        outfitInventory = GameObject.Find("OutfitInventory").GetComponent<OutfitInventory>();
        personalInventoryList = GameObject.Find("WardrobeDisplay").transform.Find("OutfitListPanel").Find("GridWithElements").GetComponent<OutfitInventoryList>();
        merchantInventoryList = GameObject.Find("ShopDisplay").transform.Find("OutfitListPanel").Find("GridWithElements").GetComponent<OutfitInventoryList>();
    }

	public void BuyOrSell()
    {
        if (inventoryType == "personal") //Selling Things
        {
            //Add Money to our Account. Sold Items sell at 50% of their purchase price.
            GameData.moneyCount += (int)(OutfitInventory.outfitInventories[inventoryType][inventoryNumber].price * 0.5);
            Debug.Log("Sold for" + (int)(OutfitInventory.outfitInventories[inventoryType][inventoryNumber].price * 0.5));

            //Remove the Sold Item from the Personal Inventory
            OutfitInventory.outfitInventories[inventoryType].RemoveAt(inventoryNumber);

            //If that item was worn last at a party then reset the Last Party Outfit ID, so an item with its ID doesn't get a wrongful double Novelty hit
            if (inventoryNumber == GameData.lastPartyOutfitID)
            {
                GameData.lastPartyOutfitID = -1;
            }
            //Reset the Personal Inventory Display
            personalInventoryList.selectedInventoryOutfit = -1;
        }
        else if (inventoryType == "merchant") //Buying Things
        {
            //Remove Money from our account
            if (!GameData.seamstressHired) // Seamstress Discount?
            {
                GameData.moneyCount -= OutfitInventory.outfitInventories[inventoryType][inventoryNumber].price;
                Debug.Log("Bought for" + OutfitInventory.outfitInventories[inventoryType][inventoryNumber].price);
            }
            else
            {
                GameData.moneyCount -= (int)(OutfitInventory.outfitInventories[inventoryType][inventoryNumber].price * GameData.seamstressDiscount);
                Debug.Log("Bought for" + (int)(OutfitInventory.outfitInventories[inventoryType][inventoryNumber].price * GameData.seamstressDiscount));
            }
            //Add the Sold Item to the Personal Inventory
            OutfitInventory.outfitInventories["personal"].Add(OutfitInventory.outfitInventories[inventoryType][inventoryNumber]);

            //Remove the Sold Item from the Merchant Inventory
            OutfitInventory.outfitInventories[inventoryType].RemoveAt(inventoryNumber);

            //Reset the Merchant Inventory Display
            merchantInventoryList.selectedInventoryOutfit = -1;
        }
        personalInventoryList.ClearInventoryButtons();
        merchantInventoryList.ClearInventoryButtons();
        personalInventoryList.GenerateInventoryButtons();
        merchantInventoryList.GenerateInventoryButtons();
    }
}

