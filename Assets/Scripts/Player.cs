
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

public class Player : MonoBehaviour
{

    #region selector

    
    public Camera cam;
    public GameObject selected;
    private bool isSelected = false;
    private ModernProvince selectionInfo;

    #endregion

    #region playerNation

    

    
    public Material playerColor;
    public GameObject startingProvince;
    public int playerMoney = 0;
    public List<GameObject> playerProvinces = new List<GameObject>();

    
    #endregion

    #region GUI

    private GUIStyle basic36;
    private int gameStage = 0;
    private GUIStyle button36;

    #endregion
    
    #region tick time variables

    private int tickSpeed = 1; //1 second

    private DateTime gameTime = new DateTime(1444,1,1);
    
    #endregion

    #region trade

    private bool displayTrade = false;
    

    #endregion

    public void claimProvince(GameObject province)
    {
        playerProvinces.Add(province);
        
        foreach (MeshRenderer subProvinceRenderer in province.GetComponentsInChildren<MeshRenderer>())
        {
            subProvinceRenderer.material = playerColor;
        }


        province.GetComponent<ModernProvince>().diplomaticRecognition = true;
        province.GetComponent<ModernProvince>().owner = "You";
        province.GetComponent<ModernProvince>().directIncome = province.GetComponent<ModernProvince>().developement;

    }    
    
    private void Start()
    {
        


        #region tick based update loop setup

        StartCoroutine("tickUpdate");

        #endregion

    }

    private void Update()
    {
        #region selection manager



        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Province"))
                {

                    if (selected == hit.transform.parent.gameObject)
                    {

                        selected.GetComponent<Outline>().enabled = false;
                        selected = null;
                        isSelected = false;
                    }
                    else
                    {
                        if (selected ?? false)
                        {
                            selected.GetComponent<Outline>().enabled = false;
                        }

                        selected = hit.transform.parent.gameObject;
                        selected.GetComponent<Outline>().enabled = true;
                        isSelected = true;
                    }

                }
            }
        }

        #endregion

        #region select starting province event

        if (startingProvince == null)
        {
            if (selected != null)
            {
                startingProvince = selected;
                selected.GetComponent<Outline>().enabled = false; 
                selected = null;

                claimProvince(startingProvince);

                gameStage = 1; //For lore/tutorial 
                isSelected = false; //
                startingProvince.GetComponent<ModernProvince>().owner = "You";

            }
        }


        #endregion
    }
    
    private void OnGUI() // TODO proper tutorial?   
    {
        
        #region basic36 text setup
        basic36 = new GUIStyle();
        basic36.fontSize = 36;
        basic36.normal.background = Texture2D.whiteTexture;
        basic36.padding.top = 10;
        basic36.padding.bottom = 10;
        basic36.padding.left = 10;
        basic36.padding.right = 10;
        #endregion

        #region button36 setup

        button36 = new GUIStyle(GUI.skin.button);
        button36.fontSize = 36;
        //button36.normal.background = Texture2D.blackTexture;
        

        #endregion
        
        #region gameIntro
        if (gameStage == 0 )
        {
            
            //GUI.Label(new Rect(560, 500, 800, 100), "click on a province as your starting province",basic36); //TODO: Button Work
            GUILayout.Label("Choose your starting province by clicking on it", basic36);
            
            
        }
        else if (gameStage == 1)
        {
            GUILayout.Label("now click on another province to begin influencing it ", basic36);
            if (isSelected)
            {
                gameStage = 2;
            }
            
        }
        #endregion

        #region provinceDisplay

        

        if (isSelected)
        {
            
            selectionInfo = selected.GetComponent<ModernProvince>();
            
            GUILayout.Label("Area: " + selected.name, basic36);
            
            if (selectionInfo.owner != "You")
            {
                GUILayout.Label("influence: " + selectionInfo.influence, basic36);
            }
            GUILayout.Label("Owner: " + selectionInfo.owner, basic36);
            GUILayout.Label("Recognized: " + selectionInfo.diplomaticRecognition, basic36);
            GUILayout.Label("Income: " + selectionInfo.directIncome, basic36);
            GUILayout.Label("Development: " + selectionInfo.developement, basic36);

        }
        #endregion
        
        #region playerStatsDisplay
        GUI.Label(new Rect(1620,0,300,60), "Date: " + gameTime.Date.ToShortDateString(), basic36);
        GUI.Label(new Rect(1620,60,300,60), "Money: " + playerMoney, basic36);
        #endregion

        #region provinceInteraction
        
        if (selected != null && selectionInfo.diplomaticRecognition && selectionInfo.owner != "You")
        {
            if (!displayTrade)
            {
                if (GUI.Button(new Rect(400, 0, 200, 50), "Trade", button36))
                {
                    displayTrade = true;
                }
            }

            if (displayTrade)
            {
                if (GUI.Button(new Rect(400, 0, 300, 50), "Hide Trade",button36))
                {
                    displayTrade = false;
                }
            }
        }

        #endregion

    }
    
    
    IEnumerator tickUpdate()
    {
        for (;;)
        {
            gameTime = gameTime.Add(new TimeSpan(1,0,0,0));
            foreach (GameObject province in playerProvinces)
            {
                playerMoney += province.GetComponent<ModernProvince>().directIncome;
            }
            
            
            
            yield return new WaitForSecondsRealtime(tickSpeed);
        }

    }
    
    
}

