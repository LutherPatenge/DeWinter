using UnityEngine;
using System.Collections;

public class Faction {

    string name;
    public int playerReputation; //What's the Player's Rep with this faction?
    public int power; //How powerful is this faction in the game?
    public int modestyLike; //How modest do they like their outfits?
    public int luxuryLike; //How luxurious do they like their outfits?

    public Faction (string nme, int mL, int lL)
    {
        name = nme;
        modestyLike = mL;
        luxuryLike = lL;
        power = 10;
        playerReputation = 300;
    }

    public string Likes()
    {
        string likes;
        string ll;
        string ml;
        //----------
        if (luxuryLike > 0)
        {
            ll = "Luxurious";
        } else if (luxuryLike < 0 )
        {
            ll = "Vintage";
        } else
        {
            ll = "Doesn't Care"; // This will never get used, but it's here just in case...
        }
        //---------
        if (modestyLike > 0)
        {
            ml = "Modest";
        } else if (modestyLike < 0)
        {
            ml = "Racy";
        } else
        {
            ml = "Doesn't Care"; // This will never get used, but it's here just in case...
        }
        //---------
        if (luxuryLike == 0 && modestyLike == 0)
        {
            likes = "They don't care about your clothes";
        } else
        {
            likes = ll + " and " + ml + " Outfits";
        }
        return likes;
    }

    public string Dislikes()
    {
        string dislikes;
        string ld;
        string md;
        //----------
        if (luxuryLike > 0)
        {
            ld = "Vintage";
        }
        else if (luxuryLike < 0)
        {
            ld = "Luxurious";
        }
        else
        {
            ld = "Doesn't Care"; // This will never get used, but it's here just in case...
        }
        //---------
        if (modestyLike > 0)
        {
            md = "Racy";
        }
        else if (modestyLike < 0)
        {
            md = "Modest";
        }
        else
        {
            md = "Doesn't Care"; // This will never get used, but it's here just in case...
        }
        //---------
        if (luxuryLike == 0 && modestyLike == 0)
        {
            dislikes = "They don't care about your clothes";
        }
        else
        {
            dislikes = ld + " and " + md + " Outfits";
        }
        return dislikes;
    }
}
