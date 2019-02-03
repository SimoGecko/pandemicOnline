// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// all 4 movement actions //////////
//parameter: moveTo cityNid

public class MoveAction : Action {

    public override bool CanPerformCustom() {
        bool isDifferentCity = player.CurrentCity.Nid != CityParam;
        bool isAdjacent = player.CurrentCity.IsAdjacentTo(City.Get(CityParam));

        return isDifferentCity && isAdjacent;
    }

    public override void PerformCustom() {
        player.Move(CityParam);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", "move", player.Nid, CityParam);
    }

}

public class DirectAction : Action {

    public override bool CanPerformCustom() {
        bool isDifferentCity = player.CurrentCity.Nid != CityParam;
        bool hasCard = player.HasCard(CityParam);

        return isDifferentCity && hasCard;
    }

    public override void PerformCustom() {
        player.Discard(CityParam);
        player.Move(CityParam);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", "direct", player.Nid, CityParam);
    }
}

public class CharterAction : Action {

    public override bool CanPerformCustom() {
        bool isDifferentCity = player.CurrentCity.Nid != CityParam;
        bool hasCard = player.HasCard(player.CurrentCity.Nid);

        return isDifferentCity && hasCard;
    }

    public override void PerformCustom() {
        player.Discard(player.CurrentCity.Nid);
        player.Move(CityParam);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", "charter", player.Nid, CityParam);
    }
}

public class ShuttleAction : Action {

    public override bool CanPerformCustom() {
        bool isDifferentCity = player.CurrentCity.Nid != CityParam;
        bool fromHasResearch = City.Get(player.CurrentCity.Nid).HasResearchStation;
        bool toHasresearch = City.Get(CityParam).HasResearchStation;

        return isDifferentCity && fromHasResearch && toHasresearch;
    }

    public override void PerformCustom() {
        player.Move(CityParam);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", "shuttle", player.Nid, CityParam);
    }
}