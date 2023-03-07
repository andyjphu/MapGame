
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    #region selector

    
    public Camera cam;
    public GameObject selected;
    private bool isSelected = false;

    #endregion

    #region playerNation

    

    
    public Material playerColor;
    public GameObject startingProvince;
    public int playerMoney = 0; 

    
    #endregion

    #region GUI

    private GUIStyle basic36;
    private int gameStage = 0; 

    #endregion
    
    #region tick time variables

    private int tickSpeed = 10; //1 second

    private int year = 0;
    
    #endregion

    private void Start()
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

        #region tick based update loop setup

        StartCoroutine("tickUpdate");

        #endregion

    }

    void Update()
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
                foreach (MeshRenderer subProvinceRenderer in startingProvince.GetComponentsInChildren<MeshRenderer>())
                {
                    subProvinceRenderer.material = playerColor;
                }

                gameStage = 1;
                isSelected = false;
                startingProvince.GetComponent<ModernProvince>().owner = "You";

            }
        }


        #endregion
    }
    private void OnGUI()
    {
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
            
            ModernProvince selectionInfo = selected.GetComponent<ModernProvince>();

            GUILayout.Label("selected: " + selected.name, basic36);
            GUILayout.Label("influence: " + selectionInfo.influence, basic36);
            GUILayout.Label("owner: " + selectionInfo.owner, basic36);
            GUILayout.Label("direct income: " + selectionInfo.directIncome, basic36);

        }
        #endregion
        
        #region playerStatsDisplay
        GUI.Label(new Rect(1720,0,200,60), "Money: " + playerMoney, basic36);
        #endregion
        

    }
    
    
    IEnumerator tickUpdate()
    {
        for (;;)
        {
            yield return new WaitForSecondsRealtime(tickSpeed);
        }

    }
}
