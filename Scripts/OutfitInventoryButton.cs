using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class OutfitInventoryButton : MonoBehaviour {
    public int outfitID;
    public string inventoryType;
    private Text myDescriptionText;
    private Text myPriceText;
    private Outline myOutline; // This is for highlighting buttons

    OutfitInventoryList outfitInventoryList;
 
    void Start()
    {
        myDescriptionText = this.transform.Find("DescriptionText").GetComponent<Text>();
        myPriceText = this.transform.Find("PriceText").GetComponent<Text>();
        myOutline = this.GetComponent<Outline>();
        outfitInventoryList = this.transform.parent.GetComponent<OutfitInventoryList>();
    }

    void Update()
    {
        //To Do: Outline if the Outfit button has been selected
        DisplayOutfitStats(inventoryType, outfitID);
        if (outfitInventoryList.selectedInventoryOutfit == outfitID)
        {
            myOutline.effectColor = Color.yellow;
        }
        else
        {
            myOutline.effectColor = Color.clear;
        }       
    }

    public void DisplayOutfitStats(string inv, int oID)
    {
        if(OutfitInventory.outfitInventories[inv].ElementAtOrDefault(oID) != null)
        {
            myDescriptionText.text = OutfitInventory.outfitInventories[inv][oID].Name();
            int price = OutfitInventory.outfitInventories[inv][oID].price;
            if (inv == "personal") // Half Price if it's in the Player Inventory
            {
                price = (int)(price * 0.5);
            }
            if (inv == "merchant" && GameData.seamstressHired) // Seamstress gives a Discount on stuff in the Merchant Shop
            {
                price = (int)(price * GameData.seamstressDiscount);
            }
            myPriceText.text = price.ToString("£" + "#,##0");
        } 
    }

    public void SetInventoryItem()
    {
        Debug.Log("Selected Inventory Item: " + outfitID.ToString());
        outfitInventoryList.selectedInventoryOutfit = outfitID;
    }
}
