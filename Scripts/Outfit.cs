using UnityEngine;
using System.Collections;

public class Outfit {

    public int novelty;
    public int modesty;
    public int luxury;
    public string style; // Styles: Frankish, Venezian, Albian and Catalan
    public int price;
    
    // Fully Featured Constructor
    public Outfit(int nov, int mod, int lux, string sty)
    {
        novelty = nov;
        modesty = mod;
        luxury = lux;
        style = sty;
        price = CalculatePrice();
    }

    // Empty/Default Constructor means random outfit
    public Outfit()
    {
        novelty = 100;
        modesty = GenerateRandom();
        luxury = GenerateRandom();
        int styleNumber = Random.Range(1, 5);
        if (styleNumber == 1)
        {
            style = "Frankish";
        } else if (styleNumber == 2)
        {
            style = "Venezian";
        } else if (styleNumber == 3)
        {
            style = "Albian";
        } else
        {
            style = "Catalan";
        }
        price = CalculatePrice();
    }

    //Just Style in the string means a randomly generated item of a specific style
    public Outfit(string sty)
    {
        novelty = 100;
        modesty = GenerateRandom();
        luxury = GenerateRandom();
        style = sty;
        price = CalculatePrice();
    }

    public void PrintValues()
    {
        Debug.Log("Novelty: " + novelty + ", Modesty: " + modesty + ", Luxury: " + luxury + ", Style : " + style);
    }

    public void UpdatePrice()
    {
        price = CalculatePrice();
    }

    int CalculatePrice()
    {
        float noveltyPercent = (float)novelty / 100;
        int calcPrice = (int)((Mathf.Abs(modesty) + Mathf.Abs(luxury))*noveltyPercent);
        if(style != GameData.currentStyle) //Check to see if this Outfit matches what's in Style
        {
            calcPrice = (int)(calcPrice*GameData.outOfStylePriceMultiplier);
        }
        if (calcPrice < 10) // If the Price is less than 10 make it 10. Will Sell for 5 at most (Sell price is 50% of Buy Price)
        {
            calcPrice = 10;
        }
        return calcPrice;
    }

    int GenerateRandom()
    {
        int value = 0;
        value += Random.Range(0, 51);
        value += Random.Range(0, 51);
        value += Random.Range(0, 51);
        value += Random.Range(0, 51);
        value -= 100;
        return value;
    }
    
    public string Name()
    {
        string name;
        name = numberToTextConversion(modesty, luxury) + " " + style + " Outfit";
        return name;
    }

    string numberToTextConversion(int mod, int lux)
    {
        string modestyString = null;
        string luxuryString = null;
        //Modesty Conversion
        if (mod > 60)
        {
            modestyString = "Virginal";
        }
        else if (mod > 20 && mod <= 60)
        {
            modestyString = "Conservative";
        }
        else if (mod >= -20 && mod <= 20)
        {
            modestyString = "Modest";
        }
        else if (mod >= -60 && mod < -20)
        {
            modestyString = "Racy";
        }
        else if (mod < -60)
        {
            modestyString = "Scandalous";
        }
        //Luxury Conversion
        if (lux > 60)
        {
            luxuryString = "Luxurious";
        }
        else if (lux > 20 && lux <= 60)
        {
            luxuryString = "Pricey";
        }
        else if (lux >= -20 && lux <= 20)
        {
            luxuryString = "Costly";
        }
        else if (lux >= -60 && lux < -20)
        {
            luxuryString = "Thrifty";
        }
        else if (lux < -60)
        {
            luxuryString = "Vintage";
        }
        //Return Dat Shit!
        return luxuryString + ", " + modestyString;
    }
}
