using System.Linq;
using System.Collections.Generic;

using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace RollCageDontRemoveTrims
{
    public class RollCageDontRemoveTrims : Mod
    {
        public override string ID => "RollCageDontRemoveTrims"; // Your (unique) mod ID 
        public override string Name => "RollCageDontRemoveTrims"; // Your mod name
        public override string Author => "Andrinall"; // Name of the Author (your name)
        public override string Version => "1.0"; // Version
        public override string Description => ""; // Short description of your mod

        public override void ModSetup()
        {
            SetupFunction(Setup.PreLoad, Mod_PreLoad);
        }

        private void Mod_PreLoad()
        {
            GameObject RollCageData = GameObject.Find("Database/DatabaseOrders/Roll Cage");
            GameObject RollCageInterior = GameObject.Find("SATSUMA(557kg, 248)/Interior/roll cage(xxxxx)");
            GameObject TrimInterior = GameObject.Find("SATSUMA(557kg, 248)/Interior/Trim");

            GameObject TrimDoorPanelLeft = GameObject.Find("CARPARTS/PartsCar/door left(Clone)/panel_door_left1")
                ?? GameObject.Find("door left(Clone)/panel_door_left1")
                ?? GameObject.Find("SATSUMA(557kg, 248)/Body/pivot_door_left/door left(Clone)/panel_door_left1");

            GameObject TrimDoorPanelRight = GameObject.Find("CARPARTS/PartsCar/door right(Clone)/panel_door_right1")
                ?? GameObject.Find("door right(Clone)/panel_door_right1")
                ?? GameObject.Find("SATSUMA(557kg, 248)/Body/pivot_door_right/door right(Clone)/panel_door_right1");

            PlayMakerFSM DataFSM = RollCageData.GetComponent<PlayMakerFSM>();
            DataFSM.Fsm.InitData();

            FsmState InstalledState = DataFSM.FsmStates.First(v => v.Name == "Installed");
            var StateActions = new List<FsmStateAction>(InstalledState.Actions);
            StateActions.RemoveRange(3, 3);
            InstalledState.Actions = StateActions.ToArray();

            TrimInterior?.SetActive(true);
            TrimDoorPanelLeft?.SetActive(true);
            TrimDoorPanelRight?.SetActive(true);
        }
    }
}
