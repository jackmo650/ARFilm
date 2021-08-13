using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
namespace Lab
{
    [Serializable]
    public class Film
    {
        public string title;
        public string release_year;
        public string locations;
        public string production_company;
        public string director;
        public string writer;
        public string actor_1;
        public string actor_2;
        public string actor_3;
        public string url;
    }
    // CREDIT: https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }
        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }
        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
    public class FilmLocations : MonoBehaviour
    {
        public string URL = "https://data.sfgov.org/resource/yitu-d5am.json?$limit=4000";
        public TMP_InputField locationInput;
        public TMP_Text resultsText;
        private Film[] films;
        void Start()
        {
            var request = new WWW(URL);
            while (!request.isDone)
            {
                // Wait until the down finishes
            }
            Debug.Log($"Response length: {request.text.Length}");
            var convertedText = $"{{\"Items\":{request.text}}}";
            films = JsonHelper.FromJson<Film>(convertedText);
            Debug.Log($"Found {films.Length} films");
        }
        public void OnLocationsSearchClicked()
        {
            
            var foundFilms = GetFilmsWithLocation(films, locationInput.text);
            Debug.Log($"Found {foundFilms.Count} films filmed at {locationInput.text}");
            // Film Name (2000)
            resultsText.text = string.Join("\n", foundFilms
                .OrderByDescending(film => film.release_year)
                .Select(film => $"{film.title} ({film.release_year})")
                .Distinct()) ;
            //
        }
        private List<Film> GetFilmsWithLocation(Film[] films, string location)
        {
            var foundFilms = new List<Film>();
            foreach (var film in films)
            {
                if (!string.IsNullOrEmpty(film.locations) && film.locations.ToLower().Contains(location.ToLower()))
                    foundFilms.Add(film);
            }
            return foundFilms;
            // LINQ Equivalent
            //return films.Where(film => !string.IsNullOrEmpty(film.locations) && film.locations.ToLower().Contains(location.ToLower())).ToList();
        }
        public void OnFilmsSearchClicked()
        {

            var foundLocationsFilms = GetLocationsofFilm(films, locationInput.text);
            // Film Name (2000)
            resultsText.text = string.Join("\n", foundLocationsFilms
                .Select(film => $"{film.locations})")
                //({film.actor_1}) ({film.actor_2})({film.release_year} ({film.actor_3})
                .Distinct());
            FilmManager.instance.films.Clear();
            for (int i = 0; i < foundLocationsFilms.Count; i++)
            {
                FilmManager.instance.films.Add(foundLocationsFilms[i]);
            }
            //Testing
            for (int i = 0; i < FilmManager.instance.films.Count; i++)
            {
                //FilmManager.instance.films[i].GoogleAPIRequest();
                FilmManager.instance.GoogleAPIRequest(i);
            }

        }
        private List<Film> GetLocationsofFilm(Film[] films, string location)
        {
            var foundLocationsFilms = new List<Film>();
            foreach (var film in films)
            {
                if (!string.IsNullOrEmpty(film.title) && film.title.ToLower().Contains(location.ToLower()))
                    foundLocationsFilms.Add(film);
            }
            return foundLocationsFilms;
            // LINQ Equivalent
            //return films.Where(film => !string.IsNullOrEmpty(film.locations) && film.locations.ToLower().Contains(location.ToLower())).ToList();
        }

        public void OnActorsSearchClicked()
        {

            var foundActorFilms = GetActorsofFilm(films, locationInput.text);
            // Film Name (2000)
            resultsText.text = string.Join("\n", foundActorFilms
                .OrderByDescending(film => film.release_year)
                .Select(film => $"{film.title} ({film.release_year})")
                .Distinct());
        }
        private List<Film> GetActorsofFilm(Film[] films, string location)
        {
            var foundActorFilms = new List<Film>();
            foreach (var film in films)
            {
                if (!string.IsNullOrEmpty(film.actor_1) && film.actor_1.ToLower().Contains(location.ToLower()))
                    foundActorFilms.Add(film);
                if (!string.IsNullOrEmpty(film.actor_2) && film.actor_2.ToLower().Contains(location.ToLower()))
                    foundActorFilms.Add(film);
                if (!string.IsNullOrEmpty(film.actor_3) && film.actor_3.ToLower().Contains(location.ToLower()))
                    foundActorFilms.Add(film);
            }
            return foundActorFilms;
            // LINQ Equivalent
            //return films.Where(film => !string.IsNullOrEmpty(film.locations) && film.locations.ToLower().Contains(location.ToLower())).ToList();
        }
    }
}
















