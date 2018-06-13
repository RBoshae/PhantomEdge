using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { playing, gameover };

public static class GlobalRefs {

    public static GameState gameState;
    public static MainPlayer Player;
    public static Spawner Spawner;
    public static Transform PlayerRoomSpawn;
    public static Nexus redNexus;
    public static Nexus blueNexus;
    public static List<Text> PlayerViewText = new List<Text>();
    public static GameObject Gun;
    public static GameObject Sword;
}
