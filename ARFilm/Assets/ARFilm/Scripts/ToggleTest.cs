using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Lab;

public class ToggleTest : MonoBehaviour  
{
    public ToggleGroup toggleGroupInstance;
    //Toggle mToggle;

    public Toggle locations;
    public Toggle films;
    public Toggle actors;
    public FilmLocations filmLoactions;

    public Toggle currentSelection
    {
        get
        {
            return toggleGroupInstance.ActiveToggles().FirstOrDefault();
        }
    }

    private void Awake()
    {
        filmLoactions = FindObjectOfType<FilmLocations>();
    }

    
    void Start()
    {
        toggleGroupInstance = GetComponent<ToggleGroup>();
        Debug.Log("First Selected" + currentSelection.name);


        
    }

    
    public void OnSearch()
    {
        toggleGroupInstance = GetComponent<ToggleGroup>();
        Debug.Log("Current Selected " + currentSelection.name);

        if (locations.isOn)
        {
            filmLoactions.OnLocationsSearchClicked();
        }
        if (actors.isOn)
        {
            filmLoactions.OnActorsSearchClicked();
        }
        if (films.isOn)
        {
            filmLoactions.OnFilmsSearchClicked();
        }
    }

}