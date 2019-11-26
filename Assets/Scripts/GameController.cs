using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingshooter;
    public List<Bird> Birds;
    // Start is called before the first frame update
    void Start()
    {
        slingshooter.InitiateBird(Birds[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
