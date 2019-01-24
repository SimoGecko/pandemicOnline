// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// all 4 movement actions //////////

public class MoveAction : Action {

    public override bool CanPerform() {
        bool isDifferentCity = player.CurrentCity.Nid != CityToNid;
        bool isAdjacent = player.CurrentCity.IsAdjacentTo(City.Get(CityToNid));

        return isDifferentCity && isAdjacent;
    }

    public override void Perform() {
        player.Move(CityToNid);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "move", CityToNid);
    }

}

public class Direct : Action {

    public override bool CanPerform() {
        bool isDifferentCity = player.CurrentCity.Nid != CityToNid;
        bool hasCard = player.HasCard(CityToNid);

        return isDifferentCity && hasCard;
    }

    public override void Perform() {
        player.Discard(CityToNid);
        player.Move(CityToNid);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "direct", CityToNid);
    }
}

public class Charter : Action {

    public override bool CanPerform() {
        bool isDifferentCity = player.CurrentCity.Nid != CityToNid;
        bool hasCard = player.HasCard(player.CurrentCity.Nid);

        return isDifferentCity && hasCard;
    }

    public override void Perform() {
        player.Discard(player.CurrentCity.Nid);
        player.Move(CityToNid);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "charter", CityToNid);
    }
}

public class Shuttle : Action {

    public override bool CanPerform() {
        bool isDifferentCity = player.CurrentCity.Nid != CityToNid;
        bool fromHasResearch = City.Get(player.CurrentCity.Nid).HasResearchStation;
        bool toHasresearch   = City.Get(CityToNid).HasResearchStation;

        return isDifferentCity && fromHasResearch && toHasresearch;
    }

    public override void Perform() {
        player.Move(CityToNid);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "shuttle", CityToNid);
    }
}