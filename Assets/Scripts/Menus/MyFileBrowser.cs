using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using UnityEngine.EventSystems;

public class MyFileBrowser : MonoBehaviour {

    [SerializeField]private GridLayoutGroup fileLayout;
    [SerializeField]private GridLayoutGroup directoryLayout;

    [SerializeField]private Button fileButton;
    [SerializeField]private Button directoryButton;

    [SerializeField]private Text currentFiletext;
    [SerializeField]private Image pleaseWaitImage;

    private string currentFile;
    private string currentDirectory;

    private string[] fileList;
    private string[] directoryList;

    private Button fb;
    private Button db;

    private GetSamples samples;

    private bool shouldChange;

    // Use this for initialization
    void Start ()
    {
        currentDirectory = Directory.GetCurrentDirectory();

        currentFile = "";

        samples = FindObjectOfType<GetSamples>();

        shouldChange = true;

        currentFiletext.text = "No file has been selected";
    }

    // Update is called once per frame
    void Update ()
    {
        if (shouldChange)
        {
            fileList = Directory.GetFiles(currentDirectory, "*.OGG");
            directoryList = Directory.GetDirectories(currentDirectory);

            for (int i = 0; i < fileList.Length; i++)
            {
                fb = Instantiate(fileButton);
                fb.transform.SetParent(fileLayout.transform, false);
                FileInfo fileSelection = new FileInfo(fileList[i]);
                fb.GetComponentInChildren<Text>().text = fileSelection.Name;
                fb.onClick.AddListener(() => CurrentFile());
            }

            for (int i = 0; i < directoryList.Length; i++)
            {
                db = Instantiate(directoryButton);
                db.transform.SetParent(directoryLayout.transform, false);
                DirectoryInfo directorySelection = new DirectoryInfo(directoryList[i]);
                db.GetComponentInChildren<Text>().text = directorySelection.Name;
                db.onClick.AddListener(() => GoToDirectory());
            }

            shouldChange = false;
        }
    }

    public void GoBack()
    {
        if (fb != null || db != null)
        {
            GameObject[] files = GameObject.FindGameObjectsWithTag("Files");

            for (int i = 0; i < files.Length; i++)
                Destroy(files[i].gameObject);
        }

        DirectoryInfo di = new DirectoryInfo(currentDirectory);
        di = Directory.GetParent(currentDirectory);

        if (di != null)
            currentDirectory = di.FullName;

        shouldChange = true;
    }

    public void GoToDirectory()
    {
        if (fb != null || db != null)
        {
            GameObject[] files = GameObject.FindGameObjectsWithTag("Files");

            for (int i = 0; i < files.Length; i++)
                Destroy(files[i].gameObject);
        }

        Text text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        string directory = "";

        for (int i = 0; i < directoryList.Length; i++)
        {
            DirectoryInfo d = new DirectoryInfo(directoryList[i]);

            if (text.text == d.Name)
            {
                directory = d.FullName;
            }
        }

        if (directory.Length != 0)
            currentDirectory = directory;

        shouldChange = true;
    }

    public void CurrentFile()
    {
        Text text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();

        for (int i = 0; i < fileList.Length; i++)
        {
            FileInfo fi = new FileInfo(fileList[i]);

            if (text.text == fi.Name)
            {
                currentFile = fi.FullName;
                currentFiletext.text = fi.Name;
            }
        }
    }

    public void Load()
    {
        StartCoroutine("LoadMusic");
    }

    IEnumerator LoadMusic()
    {
        if (currentFile.Length != 0)
        {
            pleaseWaitImage.gameObject.SetActive(true);
            WWW www = new WWW("file:///" + currentFile);
            yield return www;
            MusicManager.ImportedSong = www.GetAudioClip(false, false, AudioType.OGGVORBIS);
            samples.NormaliseAudio();
            pleaseWaitImage.gameObject.SetActive(false);
        }
    }

}
