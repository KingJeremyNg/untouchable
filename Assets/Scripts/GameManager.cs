using UnityEngine;

public class GameManager : MonoBehaviour
{
    // private int rounds = 1;
    // private string phase = "Dodge"; // SHOOT, DODGE
    public PlayerController player;
    public ShootBullet shooter;

    // Initiate game
    public void PlayGame()
    {
        player.inputReady = true;
    }
}
