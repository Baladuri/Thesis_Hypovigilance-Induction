using System.Collections.Generic;
public class JSONSerializables 
{

}

[System.Serializable]
public class SaveResponseTimeJSONvariables
{
    public string postSign;
    public string response;
    public float responseTime;
    public string timeStamp;
    public string relative_time;
}

[System.Serializable]
public class SaveRresponsTimeJSON
{
    public List<string> saveFinalList = new List<string>();
}

[System.Serializable]
public class SerializePupilData 
{
    public float both_pupils_invalid;
    public float left_pupil_valid;
    public float right_pupil_valid;
    public float both_pupils_valid;
    public string time_stamp;
    public string relative_time;
}
