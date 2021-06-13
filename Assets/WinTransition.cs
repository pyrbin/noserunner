using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTransition : MonoBehaviour
{
    public SlimePuppeteer puppeteer;
    public GameObject winning;
    PhysicsEvents physicsEvents;
    // Start is called before the first frame update
    void Start()
    {
        physicsEvents = GetComponent<PhysicsEvents>();
        physicsEvents.TriggerEnter += (collider) =>
        {
            winning.SetActive(true);
            puppeteer.CurrentSlime = null;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
