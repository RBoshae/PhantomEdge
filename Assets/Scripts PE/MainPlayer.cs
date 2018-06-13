using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour {

    [SerializeField]
    private VRTK_BasicTeleport Teleporter;
    [SerializeField]
    private int HP = 10;
    [SerializeField]
    private int MaxHP = 10;
    [SerializeField]
    private Image BloodyPeripheral;
    private Transform CenterEye;

    public void Damage(int damage, float knockbackDist = 0)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Teleport(GlobalRefs.PlayerRoomSpawn.position, GlobalRefs.PlayerRoomSpawn.rotation, false, new Color(0.2f, 0, 0));
            HP = MaxHP;
            BloodyPeripheral.color = new Color(1, 1, 1, 0);
        }
        else
        {
            if (knockbackDist > 0)
            {
                Vector3 forward = new Vector3(CenterEye.forward.x, 0, CenterEye.forward.z).normalized;
                Teleport(CenterEye.position - (forward * knockbackDist), new Quaternion(), false, new Color(0.4f, 0, 0));
            }
            float HPpercent = 1 - (((float)HP) / MaxHP);
            BloodyPeripheral.color = new Color(1, 1, 1, HPpercent);
        }
    }

    private void Start()
    {
        HP = MaxHP;
        GlobalRefs.Player = this;
        BloodyPeripheral.color = new Color(1, 1, 1, 0);
        CenterEye = BloodyPeripheral.transform.parent.parent;
        Teleport(GlobalRefs.PlayerRoomSpawn.position, GlobalRefs.PlayerRoomSpawn.rotation, false, Color.black);
    }
    
    void Update () {
        //if (Input.GetKeyDown(KeyCode.Semicolon)) { Damage(3, 0.5f); }
    }

    private void Teleport(Vector3 position, Quaternion rotation, bool forceDestination, Color fadeColor)
    {
        Teleporter.blinkToColor = fadeColor;
        Teleporter.Teleport(transform, position, rotation, forceDestination);
        Teleporter.blinkToColor = Color.black;
    }
}
