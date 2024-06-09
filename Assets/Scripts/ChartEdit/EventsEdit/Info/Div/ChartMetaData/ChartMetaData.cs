using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChartMetaData : MonoBehaviour
{
    public TMP_InputField musicName;
    public TMP_InputField musicWriter;
    public TMP_InputField musicBPMText;
    public TMP_InputField artWriter;
    public TMP_InputField chartWriter;
    public TMP_InputField chartLevel;
    public TMP_InputField description;

    public Button saveButton;
    private void Start()
    {
        Blophy.Chart.MetaData metaData = Chart.Instance.chartData.metaData;
        saveButton.onClick.AddListener(() =>
        {
            metaData.musicName = musicName.text;
            metaData.musicWriter = musicWriter.text;
            metaData.musicBPMText = musicBPMText.text;
            metaData.artWriter = artWriter.text;
            metaData.chartWriter = chartWriter.text;
            metaData.chartLevel = chartLevel.text;
            metaData.description = description.text;
        });
    }
    private void OnEnable()
    {
        Blophy.Chart.MetaData metaData = Chart.Instance.chartData.metaData;
        musicName.text = metaData.musicName;
        musicWriter.text = metaData.musicWriter;
        musicBPMText.text = metaData.musicBPMText;
        artWriter.text = metaData.artWriter;
        chartWriter.text = metaData.chartWriter;
        chartLevel.text = metaData.chartLevel;
        description.text = metaData.description;
    }
}
