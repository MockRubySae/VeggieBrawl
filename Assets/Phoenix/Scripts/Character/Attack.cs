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

    public GameObject pitchFork; //projectile
    public GameObject uiPitchFork;
    public GameObject hoeUiEnable;

    public GameObject carrotSpear; //projectile
    public GameObject uiCarrotSpear;
    public GameObject spearUiEnable;

    public GameObject playerHoe;
    public GameObject playerSpear;

    public float speedOfFists = 1500f;
    public float speedOfPitchFork = 3000f;
    public float speedOfCarrotSpear = 6000f;

    public PlayerStats playerStats;

    public bool isAttacking = false;
    public bool isFists = false;
    public bool isPitchFork = false;
    public bool isCarrotSpear = false;
    public bool currentlyAttacking = true;

    public bool hoeUpgrade = false;
    public bool spearUpgrade = false;

    Dictionary<string, WeaponsStates> states = new Dictionary<string, WeaponsStates>();

    WeaponsStates currentState = null;

    public string stateName = "";
    // Start is called before the first frame update
    void Start()
    {
        currentlyAttacking = true;
        states.Add("FistsEnabled", new WeaponFists(gameObject, this));
        states.Add("PitchForkEnabled", new WeaponPitchFork(gameObject, this));
        states.Add("CarrotSpearEnabled", new WeaponCarrotSpear(gameObject, this));
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
    /*public IEnumerator AttackSpeedOfFists()
    {
        currentlyAttacking = true;
        Fists();
        yield return new WaitForSeconds(0.5f/playerStats.AttackSpeed);
        currentlyAttacking = false;
    }*/
    public void PitchFork()
    {
        GameObject currentPitchFork = (GameObject)Instantiate(pitchFork, transform.position, transform.rotation);
        currentPitchFork.GetComponent<Rigidbody>().AddForce(currentPitchFork.transform.forward * speedOfPitchFork);
    }
    /*public IEnumerator AttackSpeedOfPitchFork()
    {
        currentlyAttacking = true;
        PitchFork();
        yield return new WaitForSeconds(1f / playerStats.AttackSpeed);
        currentlyAttacking = false;
    }*/
    public void CarrotSpear()
    {
        GameObject currentCarrotSpear = (GameObject)Instantiate(carrotSpear, transform.position, transform.rotation);
        currentCarrotSpear.GetComponent<Rigidbody>().AddForce(currentCarrotSpear.transform.forward * speedOfPitchFork);
    }
    /*public IEnumerator AttackSpeedOfCarrotSpear()
    {
        currentlyAttacking = true;
        CarrotSpear();
        yield return new WaitForSeconds(2f / playerStats.AttackSpeed);
        currentlyAttacking = false;
    }*/
    void AttackEnd() //keyframe event
    {
        currentlyAttacking = true;
        Debug.Log("Attack has ended");
    }
}
public class WeaponFists : WeaponsStates
{
    int activeLimb = 0;
    public WeaponFists(GameObject p, Attack at) { player = p; manager = at; }
    public override void EnterState()
    {
        manager.isFists = true;
        manager.uiFists.SetActive(true);
    }
    public override void ExitState()
    { 
        manager.isFists = false;
        manager.uiFists.SetActive(false);
    
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(manager.hoeUpgrade)
            {
                manager.ChangeState("PitchForkEnabled");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(manager.spearUpgrade)
            {
                manager.ChangeState("CarrotSpearEnabled");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (manager.currentlyAttacking)
            {
                manager.currentlyAttacking = false;
                if (activeLimb == 0)
                {
                    player.GetComponent<Animator>().Play("player_attackFistRight");
                    activeLimb = 1;
                }
                else if (activeLimb == 1)
                {
                    player.GetComponent<Animator>().Play("player_attackFistLeft");
                    activeLimb = 0;
                }
            }
        }
                /*if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    manager.isAttacking = true;
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    manager.isAttacking = false;
                }
                if (manager.isAttacking && manager.isFists)
                {
                    if (!manager.currentlyAttacking)
                    {
                        manager.StartCoroutine(manager.AttackSpeedOfFists());
                    }
                }*/

            }
}
public class WeaponPitchFork : WeaponsStates
{
    public WeaponPitchFork(GameObject p, Attack at) { player = p; manager = at; }
    public override void EnterState()
    {
        if (manager.hoeUpgrade)
        {
            manager.isPitchFork = true;
            manager.uiPitchFork.SetActive(true);
            manager.playerHoe.SetActive(true);
        }
    }
    public override void ExitState()
    {
        if (manager.hoeUpgrade)
        {
            manager.isPitchFork = false;
            manager.uiPitchFork.SetActive(false);
            manager.playerHoe.SetActive(false);
        }
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            manager.ChangeState("FistsEnabled");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (manager.spearUpgrade)
            {
                manager.ChangeState("CarrotSpearEnabled");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (manager.currentlyAttacking)
            {
                manager.currentlyAttacking = false;
                player.GetComponent<Animator>().Play("player_attackHoe");
            }
        }
        /*else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.isAttacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            manager.isAttacking = false;
        }
        if (manager.isAttacking && manager.isPitchFork)
        {
            if (!manager.currentlyAttacking)
            {
                manager.StartCoroutine(manager.AttackSpeedOfPitchFork());
            }

        }*/

    }
}
public class WeaponCarrotSpear : WeaponsStates
{
    public WeaponCarrotSpear(GameObject p, Attack at) { player = p; manager = at; }
    public override void EnterState()
    {
        if(manager.spearUpgrade)
        {
            manager.isCarrotSpear = true;
            manager.uiCarrotSpear.SetActive(true);
            manager.playerSpear.SetActive(true);
        }
    }
    public override void ExitState()
    {
        if (manager.spearUpgrade)
        {
            manager.uiCarrotSpear.SetActive(false);
            manager.isCarrotSpear = false;
            manager.playerSpear.SetActive(false);
        }
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            manager.ChangeState("FistsEnabled");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(manager.hoeUpgrade)
            {
                manager.ChangeState("PitchForkEnabled");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (manager.currentlyAttacking)
            {
                manager.currentlyAttacking = false;
                player.GetComponent<Animator>().Play("player_attackSpear");
            }
        }
        /*else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.isAttacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            manager.isAttacking = false;
        }
        if (manager.isAttacking && manager.isCarrotSpear)
        {
            if (!manager.currentlyAttacking)
            {
                manager.StartCoroutine(manager.AttackSpeedOfCarrotSpear());
            }

        }*/

    }
}
