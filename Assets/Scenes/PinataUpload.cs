using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class PinataUpload : MonoBehaviour
{
    [SerializeField]
    string token = "";

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.U))
    //    {

    //        StartCoroutine(Upload());
    //    }
    //}

    public void Profile(
        TextField textField,
          string _name,
        string _description,
        string ipfsProfile,
        string hashProfile,
        string ipfsBackground,
        string hashBackground)
    {
        StartCoroutine(PinataUploadProfile(textField,_name, _description, ipfsProfile, hashProfile, ipfsBackground, hashBackground));
    }
    public IEnumerator PinataUploadProfile(
        TextField textField,
        string _name,
        string _description,
        string ipfsProfile,
        string hashProfile,
        string ipfsBackground,
        string hashBackground)
    {
        Pinataoptions _pinataoptions = new() { cidVersion = 1 };
        Pinatametadata _pinatametadata = new() { name = "unity" };
        Pinatacontent _pinatacontent = new() { LSP3Profile = JSONURL.EncodeJSONURL(_name, _description, ipfsProfile, hashProfile, ipfsBackground, hashBackground).Item1.LSP3Profile };
        var rootobject = new Rootobject() { pinataContent = _pinatacontent, pinataMetadata = _pinatametadata, pinataOptions = _pinataoptions };
        string jsonString = JsonUtility.ToJson(rootobject);
        using (UnityWebRequest request = UnityWebRequest.Post("https://api.pinata.cloud/pinning/pinJSONToIPFS", UnityWebRequest.kHttpVerbPOST))
        {
            byte[] postData = Encoding.ASCII.GetBytes(jsonString);
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.SetRequestHeader("Authorization", "Bearer " + token);
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            Debug.Log("Uploading");
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError($"Error: {request.error}");
                    Debug.LogError(request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"HTTP Error: {request.error}");
                    Debug.LogError(request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.Success:
                    var TextResult = request.downloadHandler.text;
                    Debug.Log(TextResult);
                    textField.value = TextResult.Split('"')[3];
                    PlayerPrefs.SetString("IpfsProfile", TextResult.Split('"')[3]);
                    break;
            }
            request.Dispose();

        }

    }
    [Serializable]
    public class Rootobject
    {
        public Pinataoptions pinataOptions ;
        public Pinatametadata pinataMetadata ;
        public Pinatacontent pinataContent ;
    }
    [Serializable]
    public class Pinataoptions
    {
        public int cidVersion ;
    }
    [Serializable]
    public class Pinatametadata
    {
        public string name;
    }
    [Serializable]
    public class Pinatacontent:ProfileMetadata
    {
    }

}
