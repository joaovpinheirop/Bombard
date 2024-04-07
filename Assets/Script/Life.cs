using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int MaxHealth;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }


}
