using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using System;

public class PlayerMain : MonoBehaviour
{
    public List<Block> blocks;

    public IPlayerControl player;

    private void Update()
    {
        MoveKey();
    }

    private void MoveKey()
    {
        if (Input.anyKeyDown)
        {
            string pressedKeys = Input.inputString;

            for (int i = 0; i < 6; i++)
            {
                if (pressedKeys == ((Direction)i).ToString())
                {

                    //player.Move((Direction)i);
                }
            }
        }
    }
}
