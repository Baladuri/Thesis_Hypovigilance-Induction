using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadCSVData : MonoBehaviour
{
    public int currentId;
    BehaviouralData sendCurrentId;
    // Start is called before the first frame update
    void Start()
    {
        //Application.Quit();
        sendCurrentId = GetComponent<BehaviouralData>();
        string csvPath = Application.dataPath + "/Resources/User Data/userData.csv";
        //TextAsset userData = Resources.Load<TextAsset>("User Data/userData");
        
        //Debug.Log(userData);

        if (File.Exists(csvPath))
        {
            TextAsset userData = new TextAsset(System.IO.File.ReadAllText(csvPath));
            //Application.Quit();
            string[] data = userData.text.Split(new char[] { '\n' });
            Debug.Log(data.Length);
            Debug.Log(data[2]);

            string[] lastEntry = data[data.Length - 2].Split(new char[] { ',' });
            Debug.Log("The first array element is"+lastEntry[0]+"and shut up");
            if (int.TryParse(lastEntry[0], out currentId))
            {
                Debug.Log("pass");
                Debug.Log(currentId);
                sendCurrentId.getCurrentId(currentId);
            }
            else
            {
                Debug.Log("fail");
            }   
        }
        else {
            Debug.Log("The file is empty");
            sendCurrentId.firstID();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
