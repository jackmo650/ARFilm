using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmData
{
    public Location location;
    public Northeast northeast;
    public Southwest southwest;
    public Viewport viewport;
    public Geometry geometry;
    public OpeningHours openingHours;
    public Photo photo;
    public Candidate candidate;
    public List<Candidate> candidates { get; set; }
    public string status { get; set; }
}
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public Viewport viewport { get; set; }
    }

    public class OpeningHours
    {
        public bool open_now { get; set; }
    }

    public class Photo
    {
        public int height { get; set; }
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }

    public class Candidate
    {
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string name { get; set; }
        public OpeningHours opening_hours { get; set; }
        public List<Photo> photos { get; set; }
        public string place_id { get; set; }
        public double rating { get; set; }
    }

    /*public class Root
    {
        public List<Candidate> candidates { get; set; }
        public string status { get; set; }
    }*/



