using UnityEngine;

public class CustomizeModels : MonoBehaviour

{
    // initialize arrays to hold the body parts
    public GameObject[] arms; // array to hold arms
    private int currentArm; // index of the current set of arms (normal or cyborg)
    public GameObject[] bodies; // array to hold bodies (normal or armor)
    private int currentBody; // index of the current body

    void Update()
    {
        for (int i = 0; i < arms.Length; i++)
        {
            if (i == currentArm)
            {
                arms[i].SetActive(true);
            }
            else
            {
                arms[i].SetActive(false);
            }
        }
    }


    // Function to switch body parts
    // This will mostly be used to toggle between body parts
    //public void SwitchPart(GameObject[] array, int current)
    //{
    //    //check that we don't go out of array bounds
    //    if(current == array.Length - 1){
    //        current = 0;
    //    }
    //    else
    //    {
    //        current++;
    //    }          
    //}
}
