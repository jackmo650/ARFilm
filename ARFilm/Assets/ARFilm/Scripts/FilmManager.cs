using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Lab;
public class FilmManager : MonoBehaviour
{
    public static FilmManager instance;
    public List<FilmData> filmData = new List<FilmData>();
    public string locationData;
    public List<Film> films = new List<Film>();
    public string url;
    public List<string> URLs = new List<string>();
    void Awake()
    {


        if (instance == null)
        {
            instance = this;
        }
    }
    // https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=City%20Hall%20San%20Francisco%20California&inputtype=textquery&fields=place_id,photos,formatted_address,name,rating,opening_hours,geometry&key=AIzaSyBz8np_l277HrREjaL5A4E_Hs74_2SS7So
    public void GoogleAPIRequest(int index)
    {
        Debug.Log(index);
        films[index].url = $"https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input={films[index].locations}%20San%20Francisco%20California&inputtype=textquery&fields=place_id,photos,formatted_address,name,rating,opening_hours,geometry&key=AIzaSyBz8np_l277HrREjaL5A4E_Hs74_2SS7So";
        URLs.Add(films[index].url);
        StartCoroutine(MakeAPIRequest(films[index].url, index));
    }

    private IEnumerator MakeAPIRequest(string URL, int index)
    {
        //for(int i = 0; i < URLs.Count; i++)
        //{
            using (UnityWebRequest webrequest = UnityWebRequest.Get(URLs[0]))
            {
                // request and wait for the desired page
                yield return webrequest.SendWebRequest();

                if (webrequest.isNetworkError)
                {
                    Debug.Log("Error: " + webrequest.error);
                }
                else
                {
                    Debug.Log(webrequest.downloadHandler.text);
                    DeserialzedFilmData(webrequest.downloadHandler.text);

                }
            //}
        }
        url = $"https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input={films[0].locations}{films[1].locations}%20San%20Francisco%20California&inputtype=textquery&fields=place_id,photos,formatted_address,name,rating,opening_hours,geometry&key=AIzaSyBz8np_l277HrREjaL5A4E_Hs74_2SS7So";
        /*using (UnityWebRequest webrequest = UnityWebRequest.Get(url)) 
        {
            // request and wait for the desired page
            yield return webrequest.SendWebRequest();

            if (webrequest.isNetworkError)
            {
                Debug.Log("Error: " + webrequest.error);
            }
            else
            {
                Debug.Log(webrequest.downloadHandler.text);
                DeserialzedFilmData(webrequest.downloadHandler.text);
                
            }
        }*/
    }

    public void DeserialzedFilmData(string json)
    {
        FilmData filmData = JsonUtility.FromJson<FilmData>(json);

        Debug.Log($"{filmData.candidate.geometry.location.lat}, {filmData.candidate.geometry.location.lng}");
    }

    
}
