using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsStates
{
    public GameObject player;
    public Attack manager;
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void Update() { }
}
public class Attack : MonoBehaviour
{
    public GameObject fists;
    public GameObject uiFists;
    public GameObject pitchFork;
    public GameObject uiPitchFork;
    public float speedOfFists = 1500f;
    public float speedOfPitchFork = 1500f;
    public PlayerStats playerStats;
    public bool isAttacking = false;
    public bool isFists = false;
    public bool isPitchFork = false;
    public bool currentlyAttacking = false;
    Dictionary<string, WeaponsStates> states = new Dictionary<string, WeaponsStates>();
    WeaponsStates currentState = null;
    public string stateName = "";
    // Start is called before the first frame update
    void Start()
    {
        states.Add("FistsEnabled", new WeaponFists(gameObject, this));
        states.Add("PitchForkEnabled", new WeaponPitchFork(gameObject, this));
        ChangeState("FistsEnabled");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
    public void ChangeState(string WeaponsStates)
    {
        stateName = WeaponsStates;
        WeaponsStates previousState = currentState;
        currentState = states[WeaponsStates];
        if (previousState != currentState)
        {
            previousState?.ExitState();
            currentState?.EnterState();
        }
    }
    public void Fists()
    {
        GameObject currentFists = (GameObject)Instantiate(fists, transform.position, transform.rotation);
        currentFists.GetComponent<Rigidbody>().AddForce(currentFists.transform.forward * speedOfFists);
    }
    public IEnumerator AttackSpeedOfFists()
    {
        currentlyAttacking = true;
        Fists();
        yield return new WaitForSeconds(0.5f/playerStats.AttackSpeed);
        currentlyAttacking = false;
    }
    public void PitchFork()
    {
        GameObject currentPitchFork = (GameObject)Instantiate(pitchFork, transform.position, transform.rotation);
        currentPitchFork.GetComponent<Rigidbody>().AddForce(currentPitchFork.transform.forward * speedOfPitchFork);
    }
    public IEnumerator AttackSpeedOfPitchFork()
    {
        currentlyAttacking = true;
        PitchFork();
        yield return new WaitForSeconds(1f / playerStats.AttackSpeed);
        currentlyAttacking = false;
    }
}
public class WeaponFists : WeaponsStates
{
    public WeaponFists(GameObject p, Attack at) { player = p; manager = at; }
    public override void EnterState()
    {
        manager.uiFists.SetActive(true);
    }
    public override void ExitState()
    { 
        manager.isFists = false;
        manager.uiFists.SetActive(false);
    
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.isFists = true;
            manager.isAttacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            manager.isFists = false;
            manager.isAttacking = false;
        }
        if (manager.isAttacking && manager.isFists)
        {
            if (!manager.currentlyAttacking)
            {
                manager.StartCoroutine(manager.AttackSpeedOfFists());
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            manager.ChangeState("PitchForkEnabled");
        }
    }
}
public class WeaponPitchFork : WeaponsStates
{
    public WeaponPitchFork(GameObject p, Attack at) { player = p; manager = at; }
    public override void EnterState()
    {
        manager.uiPitchFork.SetActive(true);
    }
    public override void ExitState()
    {
        manager.isPitchFork = false;
        manager.uiPitchFork.SetActive(false);
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            manager.ChangeState("FistsEnabled");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.isPitchFork = true;
            manager.isAttacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            manager.isFists = false;
            manager.isAttacking = false;
        }
        if (manager.isAttacking && manager.isPitchFork)
        {
            if (!manager.currentlyAttacking)
            {
                manager.StartCoroutine(manager.AttackSpeedOfPitchFork());
            }

        }

    }
}
