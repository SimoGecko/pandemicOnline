﻿// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// all 4 movement actions //////////
//parameter: moveTo cityNid

public class MoveAction : Action {

    public override bool CanPerform() {
        bool isDifferentCity = player.CurrentCity.Nid != CityParam;
        bool isAdjacent = player.CurrentCity.IsAdjacentTo(City.Get(CityParam));

        return isDifferentCity && isAdjacent;
    }

    public override void Perform() {
        player.Move(CityParam);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "move", CityParam);
    }

}

public class Direct : Action {

    public override bool CanPerform() {
        bool isDifferentCity = player.CurrentCity.Nid != CityParam;
        bool hasCard = player.HasCard(CityParam);

        return isDifferentCity && hasCard;
    }

    public override void Perform() {
        player.Discard(CityParam);
        player.Move(CityParam);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "direct", CityParam);
    }
}

public class Charter : Action {

    public override bool CanPerform() {
        bool isDifferentCity = player.CurrentCity.Nid != CityParam;
        bool hasCard = player.HasCard(player.CurrentCity.Nid);

        return isDifferentCity && hasCard;
    }

    public override void Perform() {
        player.Discard(player.CurrentCity.Nid);
        player.Move(CityParam);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "charter", CityParam);
    }
}

public class Shuttle : Action {

    public override bool CanPerform() {
        bool isDifferentCity = player.CurrentCity.Nid != CityParam;
        bool fromHasResearch = City.Get(player.CurrentCity.Nid).HasResearchStation;
        bool toHasresearch   = City.Get(CityParam).HasResearchStation;

        return isDifferentCity && fromHasResearch && toHasresearch;
    }

    public override void Perform() {
        player.Move(CityParam);
    }

    public override string LogString() {
        return string.Format("{0} {1} {2}", player.Nid, "shuttle", CityParam);
    }
}