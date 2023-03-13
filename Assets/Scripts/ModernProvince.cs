using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModernProvince : MonoBehaviour
{
    public int influence = 0;
    public bool diplomaticRecognition = false;
    public int directIncome = 0;
    public int developement = 0;
    public string owner = "Not a formal nation";

    private enum ModernInfluenceLevels : int
    {
        Interested = 1, //diplomatic talks
        Investor = 10, //foreign investments, Franc Afrique
        Meddler = 20, //may affect elections
        InformalPuppet = 60, //Dominate affairs in all but name 
        DiplomaticAnnexation = 100 //Complete domination, taxes go directly to you, influence cannot dip
    }


    public void Update()
    {
     
        if (influence < (int) ModernInfluenceLevels.Interested)
        {
            
        }   
    }

    //TODO deprecate
    public void editOwner(string newOwner)
    {
        owner = newOwner;
    }
    
    
}
