// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// the 4 other actions //////////

public class BuildAction : Action {

    public override bool CanPerformCustom() {
        City city = player.CurrentCity;
        bool hasNoResearchStation = !city.HasResearchStation;
        bool hasCityCard = player.HasCard(city.Nid);

        return hasNoResearchStation && hasCityCard;
    }

    public override void PerformCustom() {
        City city = player.CurrentCity;
        ResearchManager.instance.PlaceStation(city.Nid);
        player.Discard(city.Nid);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "built", player.CurrentCity.Nid);
    }

}

public class TreatAction : Action {

    public override bool CanPerformCustom() {
        City city = player.CurrentCity;
        bool hasDisease = city.HasDisease(DiseaseParam);
        return hasDisease;
    }

    public override void PerformCustom() {
        City city = player.CurrentCity;
        Disease.Get(DiseaseParam).Treat(city.Nid);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "treat", DiseaseParam);
    }

}

public class ShareAction : Action {

    public override bool CanPerformCustom() {
        City city = player.CurrentCity;
        Player otherPlayer = Player.Get(PlayerParam);

        bool otherPlayerInSameCity = otherPlayer.CurrentCity == city;
        bool hasCard = player.HasCard(city.Nid) || otherPlayer.HasCard(city.Nid);
        return otherPlayerInSameCity && hasCard;
    }

    public override void PerformCustom() {
        City city = player.CurrentCity;
        Player otherPlayer = Player.Get(PlayerParam);

        bool give = player.HasCard(city.Nid);
        Player giver, taker;
        if (give) {
            giver = player;
            taker = otherPlayer;
        } else {
            //take
            giver = otherPlayer;
            taker = player;
        }
        Card c = giver.personalDeck.GetCard(city.Nid);
        giver.personalDeck.Remove(c);
        taker.personalDeck.AddTop(c);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "share", PlayerParam);
    }
}


public class CureAction : Action {

    public override bool CanPerformCustom() {
        char diseaseColor = Disease.Get(DiseaseParam).Color;
        Card[] cardsRightColor = player.personalDeck.AllCardsSatisfying(c => c.color == diseaseColor);

        bool hasEnoughCards = cardsRightColor.Length>=5;
        return hasEnoughCards;
    }

    public override void PerformCustom() {
        char diseaseColor = Disease.Get(DiseaseParam).Color;
        Card[] cardsRightColor = player.personalDeck.AllCardsSatisfying(c => c.color == diseaseColor);

        for (int i = 0; i < 5; i++) {
            player.personalDeck.Remove(cardsRightColor[i]); // let player choose which
        }
        Disease.Get(DiseaseParam).Cure();
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "cure", DiseaseParam);
    }

}