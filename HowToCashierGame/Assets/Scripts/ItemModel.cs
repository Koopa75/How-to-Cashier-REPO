using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ItemModel
{
    public int Cost;
    public PhysicsItem Prop;

    public bool scanned;
    public bool bagged;
    public bool payed;
    public bool lame; // Lame means it is for any reason is not being bought by the current customer, and should be discarded/returned
}
