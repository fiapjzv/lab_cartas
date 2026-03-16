using Game.Core;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    void Start()
    {
        var card = new CardDefinition("Some card");
        Debug.Log($"Got card {card}");
    }
}
