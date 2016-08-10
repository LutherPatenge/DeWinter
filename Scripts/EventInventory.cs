using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventInventory : MonoBehaviour
{
    public static Dictionary<string, List<Event>> eventInventories = new Dictionary<string, List<Event>>();
    public static List<Event> partyEventInventory = new List<Event>();
    public static List<Event> nightEventInventory = new List<Event>();
    
    public void StockFullInventory()
    {
        eventInventories.Add("party", partyEventInventory);
        StockPartyInventory();
        eventInventories.Add("night", nightEventInventory);
        StockNightInventory();
        TotalWeights();
    }
    
    //This is all the Party Events
    // Priority Weight, "Event Title", Event Stage 0, Event Stage 1, etc...
    void StockPartyInventory()
    {
        //---- New Event----
        eventInventories["party"].Add(new Event(1, "An Insult",
            new EventStage("Someone bumps into you from behind, and you feel a sudden chill as their drink spills down your back." 
                         + "\n'Out of my way!' someone yells shrilly. You turn around to find a rather intoxicated woman, freshly emptied drink still in her hands. Two of her friends are trying to guide her outside for some fresh air."
                         + "\nHer friends mumble some half hearted apologies about your dress before their charge interjects 'I don't see the problem. I didn't ruin anything. I would never be seen in a dress that tacky.' Her friends giggle at the remark."
                         + "\n\nYou've just lost 25 Reputation.", -25, 0, //Stage 0
                new EventOption("'Then wear it next time, because I don't think anyone wants to see you either.'", 1, 2, 2, 1),
                new EventOption("'Haha... yes, very funny...'", 3, 4, 4, 1),
                new EventOption("'I don't have to put up with this.' <Walk Away>", 5)),
            new EventStage("The other guests hush for a moment before errupting in laughter. It seems like your comment was perfectly timed. The drunk guest and her compatriots skulk away."
                         + "\n\nYou've just gained 100 Reputation.", 100, 0, //Stage 1
                new EventOption("Return to the Party <End Event>", -1)),
            new EventStage("The room falls silent at your remark. Looking around, you see that many of the once sympathetic faces have turned to glares of disapproval. Perhaps that was a little too much?"
                         + "\n\nYou've just lost 50 Reputation.", -50, 0, //Stage 2
                new EventOption("Return to the Party <End Event>", -1)),
            new EventStage("One of the friends quietly thanks you for being a good sport. The drunk guest continues her slurred tirade to nobody in particular. You walk away, searching for a damp cloth to blot your dress with."
                         + "\n\nYou've just regained 15 Reputation.", 15, 0, //Stage 3
                new EventOption("Return to the Party <End Event>", -1)),
            new EventStage("One of the friends perks up. 'It is funny isn't it? Just like how it's funny that the wine actually makes your dress look better.'" 
                         + "\nLooking around, you notice that some of the once sympathetic faces have turned to quiet laughter. You walk away, searching for a damp cloth to blot your dress with."
                         + "\n\nYou've just lost 15 Reputation.", -15, 0, //Stage 4
                new EventOption("Return to the Party <End Event>", -1)),
            new EventStage("You walk away, searching for a damp cloth to blot your dress with. Behind you, you can hear the trio snickering at you.", 0, 0, //Stage 5
                new EventOption("Return to the Party <End Event>", -1))));
        //---- New Event----
        eventInventories["party"].Add(new Event(1, "A Time for Gossip",
            new EventStage("You run across a circle of ladies in a back corner, talking amongst themselves and laughing carefully behind their fans. As you get closer, you can overhear them gossiping about the Duchess of Anemour." 
                         + "\nThere are rumors going around about her and the Baron of Baden. The fact that the Duchess is actually here makes the dicussion that much more delicious.", 0, 0, //Stage 0
                new EventOption("Join in on the fun", 1, 1, 2, 1),
                new EventOption("Don't get involved", 4)),
            new EventStage("You work your way seamlessly into the group and provide some new fodder for their mockery, generating laughter and murmurs of approval. After a few minutes everyone in the cicle goes their seperate ways with new rumors and jabs to tell their friends." //Stage 1
                         + "\n\nYou've just regained 50 Reputation.", 50, 0,
                new EventOption("Return to the Party <End Event>", -1)),
            new EventStage("You join the conversation and launch into a miniature tirade of the Duchess's various failings and romantic misadventures." //Stage 2
                         + "\nIn fact, you get so wrapped up in your performance that it takes a moment to realize that nobody in the circle is laughing anymore." 
                         + "\nThey're just staring silently over your shoulder.", 0, 0,
                new EventOption("'...She's right behind me, isn't she?'", 3)),
            new EventStage("You're correct. She IS right behind you. This point is driven home when the Duchess starts screaming in your ear." //Stage 3
                         + "\nYou turn to defend yourself from her verbal barrage, but can barely get a word in amongst her retaliation." 
                         + "\nBy the time you turn back to the group, desperate for some support, you find that they've already vanished to other portions of the party. The Duchess storms away, leaving a trail of confused and upset party guests in her wake."
                         + "\nThere is a slight ringing in your ears." 
                         + "\n\nYou've just lost 50 Reputation.", -50, 0,
                new EventOption("'Well, that could have gone better.' <End Event>", -1)),
            new EventStage("Talking about someone who's actually at the party seems like a recipe for disaster, so you choose to avoid their circle." 
                         + "\nPerhaps they'll talk about you next.", 0, 0, //Stage 4
                new EventOption("Return to the Party <End Event>", -1))
            ));
        //---- New Event----
        eventInventories["party"].Add(new Event(1, "Unattended Valuables",
            new EventStage("While ascending the stairs up to the party you notice a small, silver statue of a duck left out on a short table." //Stage 0
                         + "It would easily fit inside your bag and a quick glance shows that you're alone on the stairs.", 0, 0,
                new EventOption("Take the statue", 1, 1, 2, 1),
                new EventOption("Leave it alone", 3)),
            new EventStage("With a single, swift movement you scoop up the statue and covertly place it in your bag."  //Stage 1
                         + "\nAnother glance about confirms that nobody was around to witness your indiscretion, though the extra weight in your bag reminds you of your new acquisition."
                         + "\n\nYou now have " + (GameData.moneyCount + 100) + "Livres", 0, 100,
                new EventOption("Return to the Party <End Event>", -1)),
            new EventStage("As you reach out for the statue, the hem of your sleeve gets caught on the corner of the table. You knock the statue to the floor with a heavy 'thud'." //Stage 2
                         + "\nWhile kneeling down to retrieve your prize, you notice the shoes of a servant out of the corner of your eye. Looking up at them suddenly and manage to stammer some sort of apology and return the item to its place on the table."
                         + "\nThey know exactly what you were trying to do and you're sure the host will hear about this." 
                         + "\n\nYou've just lost 150 Reputation.", -150, 0,  
                new EventOption("Return to the Party <End Event>", -1)),
            new EventStage("You decide against taking the statue, petty theivery doesn't really suit you. "   //Stage 3
                         + "\nFor now, at least.", 0, 0,
                new EventOption("Return to the Party <End Event>", -1))
            ));
        //---- New Event----
        // Maybe rewrite this to be Roulette? Bet on Red or Black, Use actual Roulette Odds, 
        // Needs an open better stage for the 4 options, could actually allow multiple rounds of betting with circular stage references
        eventInventories["party"].Add(new Event(1, "A Friendly Wager", 
            new EventStage("You are drawn to a side room by shouts of excitement and the clink of heavy coins. A handsome man, his face flush with drink, approaches you."
                         + "\n'Hello there! How do you feel about a game of dice? We've been playing all day and a new player might liven things up!'" 
                         + "\nHe smiles cheekily before leaning in closer 'How does 50 Livres sound?'", 0, 0, 
                new EventOption("'I'm game for a litte wager.' <Bet 50 Livres>", 1,1,2,1),
                new EventOption("'That's all? Please, let's make this interesting.' <Bet 100 Livres>", 3,1,4,1),
                new EventOption("'Sorry, I'm not interested.'", 5)),
            new EventStage("The dice turn up your way and you can't help but smile as the grumbling betters shovel the scattering of coins over to you."
                         + "\n\nYou now have " + (GameData.moneyCount + 50) + "Livres", 0, 50, //Stage 1
                new EventOption("'Thanks for the money, Gentlemen!' <End Event>", -1)),
            new EventStage("You grimace as the dice don't turn up in your favor. One of the men at a table gives you a knowing wink as he takes your money away. "
                         + "'Better Luck next time, eh?'"
                         + "\n\nYou now have " + (GameData.moneyCount - 50) + "Livres", 0, -50, //Stage 2
                new EventOption("'Oh well...' <End Event>", -1)),
            new EventStage("The dice turn up your way and you can't help but smile as the grumbling betters shovel the small pile of coins over to you."
                         + "\n\nYou now have " + (GameData.moneyCount + 100) + "Livres", 0, 100, //Stage 3
                new EventOption("'Thanks for the money, Gentlemen!' <End Event>", -1)),
            new EventStage("You grimace as the dice don't turn up in your favor. You suppress the urge to reach out for your coins as one of the men at a table takes your sizable amount of money away. "
                         + "\n'Better Luck next time, eh?'"
                         + "\n\nYou now have " + (GameData.moneyCount - 100) + "Livres", 0, -100, //Stage 4
                new EventOption("'Ugh!' <End Event>", -1)),
            new EventStage("There are some grumbles of distaste as you walk away, but you feel better for not trusting your precious funds to chance.", 0, 0, //Stage 5
                new EventOption("Return to the Party <End Event>", -1))
            ));
    }

    //This is all the Night Events
    void StockNightInventory()
    {
        //---- New Event----
        eventInventories["night"].Add(new Event(1, "Digging Up Dirt",
            new EventStage("A small, reedy man is at the door. He glances about furtively before he greets you in a low voice." 
                         + "\n'Bonjour Madamme. I am Pierre Baer from that most reputable gazzette, Le Mercure.'" 
                         + "\nHe looks down and shuffles his feet for a moment. 'I write for the Society Pages, you see, and given your rising status I was wondering if you could provide some anonymous insight into some of the more salacious goings on.'" 
                         + "\nHe flashes you a smile of crooked teeth. 'I could pay handsomely, you see, depending on the quality of your information.'", 0, 0, 
                new EventOption("'Well, I've heard some things...'", 1, 75, 2, 25), 
                new EventOption("'Buzz off, Pierre, I'm not interested.'", 3)),
            new EventStage("The next morning your handmaiden brings you a copy of Le Mercure with your breakfast, along with a grungy purse of 150 Livres! It looks like Pierre managed to conceal his sources and you get to reap the rewards of professional gossiping." 
                         + "\n\nYou now have " + (GameData.moneyCount + 150) + "Livres", 0, 150, 
                new EventOption("'It's good to be bad...' <End Event>", -1)),
            new EventStage("The next morning your handmaiden brings you a copy of Le Mercure with your breakfast, along with a grungy purse of 150 Livres! You nearly spit out your morning tea as you read the society pages. That twerp Pierre barely even tried to hide his sources! Everyone's going to know it was you sharing the gossip."
                         + "\n\nYou've just lost 50 Reputation.\nYou now have " + (GameData.moneyCount + 150) + "Livres", -50, 150, 
                new EventOption("'Ugh... well at least I have the money.' <End Event>", -1)),
            new EventStage("Pierre mutters a string of imaginative curses under his breath as your staff lead him back to the streets." 
                         + "\n\nYou have a feeling that this won't be the last you see of him.", 0, 0, new EventOption("Go back to bed <End Event>.", -1))));
        //---- New Event----
        eventInventories["night"].Add(new Event(1, "A Proposition",
            new EventStage("Your handmaiden brings a man into your study. You recognize him as the writer for the Le Mercure's Society Pages, Pierre Baer." 
                         + "\n'Bonsoir Madamme' he says as he doffs his cap. 'I have come to you as a humble writer with a proposition. This month, we ran into some budgetary issues as our esteemed editor...'" 
                         + "\nHe fidgets for a moment. 'Our editor drank the budget, you see. Not literally, but we're still in a difficult position, financially. In exchange for your temporary patronage, our staff would write very favorably about you in the next issues, benefitting for your social standing. Are you interested?'", 0, 0,
                new EventOption("'Go on...'", 1),
                new EventOption("'That's low, Pierre, even for you. No thanks.'", 5)),
            new EventStage("Pierre perks up suddenly 'Really? I mean, of course!'" 
                         + "'As you can imagine, we are quite flexible for generous souls, such as yourself. How would much would you like to donate?", 0, 0,
                new EventOption("'I could always use a little boost.' <Pay 50 Livres>", 2),
                new EventOption("'A good cause deserves a decent donation, don't you think?' <Pay 100 Livres>", 3),
                new EventOption("'Make me shine, Pierre.' <Pay 200 Livres>", 4),
                new EventOption("'On second thought, I can't do this. Sorry Pierre, I'm not interested.'", 5)),
            new EventStage("The next morning's paper throws in a few nice words about you, but not enough for it to be suspicious." 
                         + "\n\nYou've just gained 50 Reputation.\nYou now have " + (GameData.moneyCount - 50) + "Livres", 100, -50,
                new EventOption("'They get some help, I get some help, everyone wins.' <End Event>", -1)),
            new EventStage("The next morning's paper sings your praises appropriately. It looks like Pierre was true to his word." 
                         + "\n\nYou've just gained 100 Reputation.\nYou now have " + (GameData.moneyCount - 100) + "Livres", 200, -100,
                new EventOption("'It feels good to donate to such a good cause, like... myself.' <End Event>", -1)),
             new EventStage("The next morning's paper glows like an ode to your social grace and wit. It'll be obvious to anyone with half a brain that something fishy is going on here, but there's a certain social status to be gained by showing people that you can afford to buy the press."
                         + "\n\nYou've just gained 200 Reputation.\nYou now have " + (GameData.moneyCount - 200) + "Livres", 400, -200,
                new EventOption("'Glad to see I got my money's worth.' <End Event>", -1)),
            new EventStage("Pierre looks disappointed as your handmaiden shows him the door." 
                         + "'Ugh, looks like it's back to betting on rat fights again...' He mutters to himself sullenly.", 0, 0, 
                new EventOption("Go back to bed <End Event>.", -1))));
        //---- New Event----
        eventInventories["night"].Add(new Event(1, "A Gift?",
            new EventStage("'Madamme, there's a man outside and he's very insistent about seeing you.'"
                         + "\nThe man in question appears to be wearing a footman's uniform and is carrying a small package."
                         + "\n'Bonsoir Madamme!' he yells over your handmaiden's shoulder 'I serve an admirer who's eye you caught at the last party. He wishes to remain anonymous for now, but wants you to have this token of his affection. May I come in? It's quite drafty outside.", 0, 0,
                new EventOption("'Please, let him in'", 1, 75, 2, 25),
                new EventOption("'Tell you master that I don't take gifts in the middle of the night from stangers. Good night.'", 3)),
            new EventStage("With a glance back at you, your handmaiden steps aside to let the main in. He bows before presenting a package wrapped in fine paper." 
                         + "\nOpening the package reveals a jeweled brooch. Before you can ask any more questions, the footman is already out your front door and heading for the street." 
                         + "\n\nYou now have " + (GameData.moneyCount + 100) + "Livres", 0, 100,
                new EventOption("'Well that's nice, I wonder who this mystery admirer is...' <End Event>", -1)),
            new EventStage("Your handmaiden steps off to the side to let him in. As you step forward to take the package from his hands, you notice the grubby clothes under his footman's coat."
                         + "\nWithout warning the man bursts forward and shoves you to the ground. In the chaos he snatches a valuable painting from your wall and runs out the door."
                         + "\n\nYou now have " + (GameData.moneyCount - 50) + "Livres", 0, -50,
                new EventOption("'Looks like I need two things, a stiff drink and a bodyguard.' <End Event>", -1)),
            new EventStage("The man at the door remains extremely insistent but after a while your handmaiden manages to shoo him away." 
                         + "\nThe identity of your admirer is still a mystery, but it's nice to know that one is still out there.", 0, 0,
                new EventOption("Go back to bed <End Event>.", -1))));
    }

    void TotalWeights()
    {
        //Total the Party Event Weights at the beginning (called ONCE at Game Start)
        foreach (Event e in eventInventories["party"])
        {
            GameData.totalPartyEventWeight += e.eventWeight;
            Debug.Log("Total Party Event Weight = " + GameData.totalPartyEventWeight);
        }
        //Total the Night Event Weights at the beginning (called ONCE at Game Start)
        foreach (Event e in eventInventories["night"])
        {
            GameData.totalNightEventWeight += e.eventWeight;
            Debug.Log("Total Night Event Weight = " + GameData.totalNightEventWeight);
        }
    }
}
