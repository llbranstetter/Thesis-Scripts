using UnityEngine;

public class UnlockComponents : MonoBehaviour {

    /***************** VARIABLES  ******************/

    // booleans to keep track of what is currently unlocked
    public bool cyborg;
    public bool armor;
    public bool companion;
    public bool helmet;
    public bool sword;
    public bool hood;
    public bool jetpack;

    // Game objects we will toggle on and off
    public GameObject upperbody_0;
    public GameObject upperbody_2;
    public GameObject upperbody_3;
    public GameObject upperbody_6;
    public GameObject lowerbody_1;
    public GameObject lowerbody_2;
    public GameObject humanhead;
    public GameObject helmetmesh;
    public GameObject humanfeet;
    public GameObject shoes;
    public GameObject hair;
    public GameObject swordmesh;
    public GameObject hoodmesh;
    public GameObject jetpackmesh;
    public GameObject humanhands;
    public GameObject cyborghands;

    /******************   FUNCTIONS   ****************/

    // Function to set up all the game objects and booleans for the beginning:
    public void Start()
    {
        // starting meshes:
        upperbody_0.SetActive(true);
        lowerbody_1.SetActive(true);
        humanhead.SetActive(true);
        humanfeet.SetActive(true);
        hair.SetActive(true);
        humanhands.SetActive(true);

        // start with these toggled off:
        upperbody_2.SetActive(false);
        upperbody_3.SetActive(false);
        upperbody_6.SetActive(false);
        lowerbody_2.SetActive(false);
        helmetmesh.SetActive(false);
        shoes.SetActive(false);
        hoodmesh.SetActive(false);
        jetpackmesh.SetActive(false);
        cyborghands.SetActive(false);

        // set all the booleans to false to start:
        cyborg = false;
        armor = false;
        companion = false;
        helmet = false;
        sword = false;
        hood = false;
        jetpack = false;

    }

    /*******          unlock funtions           **********/

    // funtion to unlock the cyborg parts
    public void UnlockCyborg()
    {
        cyborg = true;

        // change the upper body depending on the armor:
        upperbody_0.SetActive(false);
        upperbody_2.SetActive(false);
        if (armor == false)
        {
            upperbody_6.SetActive(true);
        }
        else
        {
            upperbody_3.SetActive(true);
        }
        lowerbody_1.SetActive(false);
        lowerbody_2.SetActive(true);
        cyborghands.SetActive(true);
        humanhands.SetActive(false);
    }
    // funtion to unlock armor
    public void UnlockArmor()
    {   
        armor = true;
        upperbody_0.SetActive(false);
        upperbody_6.SetActive(false);
        if (cyborg == false) {
            upperbody_2.SetActive(true);
        }
        else {
            upperbody_3.SetActive(true);
        }
        shoes.SetActive(true);
        humanfeet.SetActive(false);
    }
    // funtion to unlock helmet
    public void UnlockHelmet()
    {
        helmet = true;
        humanhead.SetActive(false);
        helmetmesh.SetActive(true);
        hair.SetActive(false);
    }
    // funtion to unlock sword
    public void UnlockSword()
    {
        sword = true;
        swordmesh.SetActive(true);
    }
    // funtion to unlock hood
    public void UnlockHood()
    {
        hood = true;
        hoodmesh.SetActive(true);
    }
    // function to unlock jetpack
    public void UnlockJetPack()
    {
        jetpack = true;
        jetpackmesh.SetActive(true);
    }
}
