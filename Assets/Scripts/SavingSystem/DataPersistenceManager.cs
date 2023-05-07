using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gamaData; 
    public static DataPersistenceManager instance { get; private set; }
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    [SerializeField] private GameObject usernameField;
    //[SerializeField] private ProfilesManager manager;
    //private ProfileData newProfile;

    public void AddProfile()
    {
        string text = usernameField.GetComponent<TMP_InputField>().text;

        if (text != "")
        {
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            NewGame(text);
        }
        else
        {
            Debug.Log("Enter a valid name");
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    //private void Start()
    //{
    //    this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    //}

    //This happens when a new profile is created
    public void NewGame(string username)
    {
        this.gamaData = new GameData(username);
    }

    public void LoadGame()
    {
        this.gamaData = dataHandler.Load();

        //if no data can be loaded, initialize to a new game
        if (this.gamaData == null)
        {
            Debug.Log("No data was found. Creating a default profile");
            NewGame("Default");
        }

        //push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gamaData);
        }
    } 

    public void SaveGame()
    {
        //how to call from another class
        //call this from when making the profile and pass in a name from txt box
        //DataPersistenceManager.instance.NewGame("value");

        //pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gamaData);
        }

        dataHandler.Save(gamaData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
