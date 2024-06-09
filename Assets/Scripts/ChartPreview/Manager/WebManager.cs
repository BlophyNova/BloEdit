using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.Chart;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

public class WebManager : MonoBehaviourSingleton<WebManager>
{
    public string chartDataPath;
    public AudioClip musicClip;
    public Texture2D background;
    public ChartData ChartData
    {
        get => Chart.Instance.chartData;
        set => Chart.Instance.chartData = value;
    }
    public AudioClip MusicClip
    {
        get => AssetManager.Instance.musicPlayer.clip;
        set => AssetManager.Instance.musicPlayer.clip = value;
    }
    public Image Background
    {
        get => AssetManager.Instance.background;
        set => AssetManager.Instance.background = value;
    }
    private void Start()
    {
        //chartData = JsonConvert.DeserializeObject<ChartData>(Resources.Load<TextAsset>(chartDataPath).text);
        //chartData = 
        if (File.Exists($"{Application.dataPath}/Music.ogg") && File.Exists($"{Application.dataPath}/Background.png"))
        {
            StartCoroutine(LoadMusicPack($"{Application.dataPath}/Music.ogg", $"{Application.dataPath}/Background.png"));
        }
        else
        {
            Debug.LogError("没有找到音乐文件或者背景文件，停止继续运行！");
            return;
        }





    }

    private void WebManager_completed(AsyncOperation obj)
    {
        LoadedMusicPack();
    }

    private void WebManager_completed()
    {
        if (File.Exists($"{Application.dataPath}/Music.ogg") && File.Exists($"{Application.dataPath}/Background.png"))
        {
            StartCoroutine(LoadMusicPack($"{Application.dataPath}/Music.ogg", $"{Application.dataPath}/Background.png"));
        }
        else
        {
            Debug.LogError("没找到音乐文件或者背景图片文件");
        }
    }

    private void LoadedMusicPack()
    {
        MusicClip = musicClip;
        Background.sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0.5f, 0.5f));
        //ChartData = ChartTools.CreateNew();

        if (File.Exists($"{Application.dataPath}/chartEdit.json"))
        {
            //ChartData = new();
            //ChartData.globalData = JsonConvert.DeserializeObject<GlobalData>(File.ReadAllText($"{Application.dataPath}/globalData.json"));
            //ChartData.metaData = JsonConvert.DeserializeObject<MetaData>(File.ReadAllText($"{Application.dataPath}/metaData.json"));
            //ChartData.boxes = new();
            //ChartData.boxes.Add(ChartTools.NewBox());

            Chart.Instance.chartEdit = JsonConvert.DeserializeObject<ChartEdit>(File.ReadAllText($"{Application.dataPath}/chartEdit.json"));
            /*
             * 
            SpeckleManager.Instance.allLineNoteControllers.Clear();
            Chart.Instance.chartData.boxes.Add(ChartTools.NewBox());
            Chart.Instance.chartEdit.boxesEdit.Add(ChartTools.CreateNewBoxEdit());
            BoxManager.Instance.RefreshList();
            boxList.RefreshList();
            Chart.Instance.Refresh();
             */
            ChartData.boxes.Clear();
            for (int i = 0; i < Chart.Instance.chartEdit.boxesEdit.Count; i++)
            {
                ChartData.boxes.Add(ChartTools.NewBox());
            }
            //BoxManager.Instance.RefreshList();
        }
        else
        {
            ChartData = ChartTools.CreateNew();
            Chart.Instance.chartEdit.boxesEdit = new();
            Chart.Instance.chartEdit.boxesEdit.Add(ChartTools.CreateNewBoxEdit());
        }

        //if (File.Exists($"{Application.dataPath}/boxesEdit.json"))
        //{
        //    Chart.Instance.chartEdit.boxesEdit = JsonConvert.DeserializeObject<List<Blophy.ChartEdit.Box>>(File.ReadAllText($"{Application.dataPath}/boxesEdit.json"));
        //}
        //else
        //{
        //}
        Chart.Instance.Refresh();
        for (int i = 0; i < Chart.Instance.chartEdit.boxesEdit.Count; i++)
        {
            for (int j = 0; j < Chart.Instance.chartEdit.boxesEdit[i].lines.Count; j++)
            {
                int exeBoxNumber = i;
                int exeLineNumber = j;
                ChartTools.EditNote2ChartDataNote(
        Chart.Instance.chartData.boxes[exeBoxNumber].lines[exeLineNumber],
        Chart.Instance.chartEdit.boxesEdit[exeBoxNumber].lines[exeLineNumber].onlineNotes);
            }
        }
    }
    IEnumerator LoadMusicPack(string musicPath, string CPPath)
    {
        if (File.Exists(musicPath) && File.Exists(CPPath))//如果文件存在
        {
            AudioType audioType = AudioType.MPEG;
            //string musicFilePath = musicPath.ToLower();
            //string getfilename = Path.GetExtension(musicPath.ToLower());
            switch (Path.GetExtension(musicPath.ToLower()))
            {
                case ".mp3":
                    audioType = AudioType.MPEG;
                    break;
                case ".ogg":
                    audioType = AudioType.OGGVORBIS;
                    break;
                case ".wav":
                    audioType = AudioType.WAV;
                    break;
            }
            musicPath = "file:///" + musicPath;//加入file:///
            string backgroundPath = "file:///" + CPPath;//加入file://
            UnityWebRequest getMusic = UnityWebRequestMultimedia.GetAudioClip(musicPath, audioType);//将文件读取到unityWenRequest
            UnityWebRequest getBackGround = UnityWebRequestTexture.GetTexture(backgroundPath);
            //UnityWebRequest getChart = 
            yield return getMusic.SendWebRequest();//跳出此方法，异步发送wen请求
            yield return getBackGround.SendWebRequest();
            if (getMusic.error != null && getBackGround.error != null)//如果发送完了，null里边有东西，就代表有error
            {
                Debug.Log(getMusic.error.ToString());//打印错误
                Debug.Log(getBackGround.error.ToString());//打印错误
            }
            else
            {
                musicClip = DownloadHandlerAudioClip.GetContent(getMusic);//声明一个临时的AudioClip
                Texture2D CP = DownloadHandlerTexture.GetContent(getBackGround);//获得到背景文件
                background = CP;
                if (SceneManager.GetActiveScene().name != "GamePlay")
                {
                    DontDestroyOnLoad(gameObject);
                    SceneManager.LoadSceneAsync("GamePlay", LoadSceneMode.Additive).completed += WebManager_completed;
                }
                else
                {
                    LoadedMusicPack();
                }
            }
        }
    }
}
