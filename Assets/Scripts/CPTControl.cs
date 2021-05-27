using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;


public class CPTControl : MonoBehaviour
{
    //[SerializeField] ERRoadNetwork roadNetwork;
    // public ERRoad road;
    // public ERRoad[] roads;
    // public GameObject myRoad;
    //  public float game;
    //  public List<ERRoad> myRoads;
    //  public Terrain terrain;



    // public GameObject rightObject;
    // public GameObject leftObject;
    //Vector3 changePosition = new Vector3(0, 0, -6);

    // Start is called before the first frame update

    //public GameObject SimpleSign1;
    //public Animation animation;
   // public SpriteRenderer thumbsUP;
    //public SpriteRenderer thumbsDown;

    //public Animator animatorUp;
    //public Animator animatorDown;
    
    //Mesh _Xsign;
    //Mesh _Osign;
    //Renderer viewedSign;
    //private GameObject taggedX;
    //private GameObject taggedO;
    private int trigger;
    private int keyPressSound;
    private string objectTag;
    private bool keyPressedOSign = false;
    private bool keyPressedXSign = false;
    BehaviouralData dataValues;

    private GameObject currentTrigger;

    void Start()
    {
        dataValues = GameObject.Find("CPTPosts").GetComponent<BehaviouralData>();

        // _Xsign = Resources.Load<Mesh>("Assets/X_OPTSign.fbx");
        //_Osign = Resources.Load<Mesh>("Assets/O_CPTSign.blend");


        //roadNetwork.GetRoadByGameObject(myRoad.GetComponent<GameObject>());

        //Debug.Log(roadNetwork);
        //road = myRoad.gameObject.GetComponent();
        //Debug.Log(road.GetWidth());
        // road.SetIndent(2f, 3);
        //SimpleSign1.GetComponent<MeshFilter>().mesh = _Xsign;
        //SimpleSign2.GetComponent<MeshFilter>().mesh = _Osign;
        //viewedSign = GetComponent<Renderer>();
        //taggedX = GameObject.FindGameObjectWithTag("xsign");
        //  taggedO = GameObject.FindGameObjectWithTag("osign");
      
    }
    private void Update()
    {
        /*if (viewedSign.isVisible == taggedX.GetComponent<Renderer>())
        {
            Debug.Log("IT IS THE X SIGN");
        }
        if (viewedSign.isVisible == taggedO.GetComponent<Renderer>())
        {
            //Debug.Log("IT IS THE OOOOO SIGN ");

        }*/
        
        if (trigger == 1 && objectTag == "xsign")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("XXXXXXXXX");
                currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                if (keyPressSound < 1)
                {
                    FindObjectOfType<AudioManager>().play("pressedCross");
                }
                keyPressSound++;
                dataValues.falsePositive = dataValues.falsePositive + 1;
                keyPressedXSign = true;
                //animatorDown.SetTrigger("start");
                //thumbsDown.enabled = true;
                //animatorDown.enabled = true;
                //StartCoroutine(stopAnimation());
            }
            
        }
        else if (trigger == 1 && objectTag == "osign")
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("OOOOOOOOO");
                currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                if (keyPressSound < 1)
                {
                    FindObjectOfType<AudioManager>().play("pressedCircle");
                }
                keyPressSound++;
                dataValues.truePositive = dataValues.truePositive + 1;
                keyPressedOSign = true;
                //animatorUp.SetTrigger("start");
                //thumbsUP.enabled = true;
                //animatorUp.enabled = true;
                //StartCoroutine(stopAnimation());
            }
        
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello");
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {        
            trigger = 1;
            keyPressSound = 0;
            Debug.Log("We have collided");
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", new Color(153/255f, 28/255f, 28/255f));
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(153 / 255f, 28 / 255f, 28 / 255f));
            currentTrigger = other.gameObject;       
            objectTag = other.gameObject.tag;
            //Debug.Log(" hit the " + objectTag);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {
            if (keyPressedOSign == false && objectTag == "osign") {
                Debug.Log("FALSE NEGATIVE");
                dataValues.falseNegative = dataValues.falseNegative + 1;
            }
            if (keyPressedXSign == false && objectTag == "xsign") {
                dataValues.trueNegative = dataValues.trueNegative + 1;
            }
            trigger = 0;
            keyPressSound = 0;
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
            keyPressedXSign = false;
            keyPressedOSign = false;
            Debug.Log(" no collision");
        }
    }

    /*IEnumerator stopAnimation() 
    {
        yield return new WaitForSeconds(1f);
        if (animatorUp.enabled == true)
        {
            thumbsUP.enabled = false;
            animatorUp.enabled = false;
        }
        if (animatorDown.enabled == true)
        {
            thumbsDown.enabled = false;
            animatorDown.enabled = false;
        }

    }*/
    // Update is called once per frame
    void FixedUpdate()
    {
      


    }

   /* void manageObjects()
    {
        if (rightObject.GetComponent<MeshRenderer>().enabled == false)
        {
            try
            {
                rightObject.GetComponent<MeshRenderer>().enabled = true;
                StartCoroutine(waitToTranslate());
                rightObject.transform.Translate(changePosition * Time.deltaTime);
                rightObject.transform.Rotate(2f, 0, 0);
            }
            catch (System.Exception e) {
                Debug.Log(e);
            }
           

            //rightObject.SetActive(false);
        }
    }

   
    IEnumerator waitForNext()
    {
        float time = Random.Range(5f, 10f);
        yield return new WaitForSeconds(time);
    }

    IEnumerator waitToTranslate()
    {
        yield return new WaitForSeconds(3);
        rightObject.transform.Translate(changePosition * Time.deltaTime);
    }*/
}
