using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recording : MonoBehaviour
{
    [SerializeField] private float captureNOfSeconds;
    [SerializeField] private Transform recordingTarget;
    [SerializeField] private GameObject _ghostPrefab;

    private List<Vector3> _savedRecordingPosition;
    private List<Quaternion> _savedRecordingRotation;
    private List<Vector3> _currentRecordingFramesPosition;
    private List<Quaternion> _currentRecordingFramesRotation;
    private bool _isCurrentlyRecording;
    private bool _isCurrentlyPlaying;
    private GameObject _ghostObject;

    private float _t;
    private int _currentFrame;

    private void Start()
    {
        SetRecordingData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetRecording();
        }
        if (_isCurrentlyRecording)
        {
            _t += Time.deltaTime;

            if (_t > captureNOfSeconds)
            {
                _currentRecordingFramesPosition.Add(recordingTarget.position);
                _currentRecordingFramesRotation.Add(recordingTarget.rotation);
                _t -= captureNOfSeconds;
            }
        }

        if (_isCurrentlyPlaying)
        {
            _t += Time.deltaTime;
            if (_t > captureNOfSeconds)
            {
                if (_currentFrame < _savedRecordingPosition.Count)
                {
                    _ghostObject.transform.position = _savedRecordingPosition[_currentFrame];
                    _ghostObject.transform.rotation = _savedRecordingRotation[_currentFrame];
                    _currentFrame++;
                }
                else
                {
                    Destroy(_ghostObject);
                    _isCurrentlyPlaying = false;
                }
                _t -= captureNOfSeconds;
            }
        }
    }

    public void StartRecording()
    {
        if (_isCurrentlyRecording)
            return;
        _currentRecordingFramesPosition = new List<Vector3>();
        _currentRecordingFramesRotation = new List<Quaternion>();
        _isCurrentlyRecording = true;
    }

    public void SaveRecording()
    {
        _savedRecordingPosition = _currentRecordingFramesPosition.ToList();
        _savedRecordingRotation = _currentRecordingFramesRotation.ToList();
        _currentRecordingFramesPosition.Clear();
        _currentRecordingFramesRotation.Clear();
        SaveRecordingData();
    }

    public void FlipRecording()
    {
        string rotationJson = System.IO.File.ReadAllText(Application.persistentDataPath + "/recRot.json");

        SaveData<Quaternion> savedRotation = JsonUtility.FromJson<SaveData<Quaternion>>(rotationJson);
        _savedRecordingRotation = savedRotation.list;

        for (int i = 0; i < _savedRecordingRotation.Count; i++)
        {
            _savedRecordingRotation[i] = Quaternion.Euler(_savedRecordingRotation[i].x, _savedRecordingRotation[i].y + 180, _savedRecordingRotation[i].z);
        }
        
        
        SaveData<Quaternion> dataRotation = new SaveData<Quaternion>(_savedRecordingRotation);

        string rotationJSON = JsonUtility.ToJson(dataRotation);
        
        System.IO.File.WriteAllText(Application.persistentDataPath + "/recRot.json", rotationJSON);
    }

    public void StopRecording()
    {
        _isCurrentlyRecording = false;
    }

    public void ResetRecording()
    {
        if (_savedRecordingPosition != null) _savedRecordingPosition.Clear();
        if (_savedRecordingRotation != null) _savedRecordingRotation.Clear();
        
        System.IO.File.Delete(Application.persistentDataPath + "/recPos.json");
        System.IO.File.Delete(Application.persistentDataPath + "/recRot.json");
    }

    public void PlayRecording()
    {
        if (_savedRecordingPosition != null)
        {
            if (_ghostObject != null)
            {
                Destroy(_ghostObject);
            }
            _ghostObject = Instantiate(_ghostPrefab);
            _isCurrentlyPlaying = true;
            _currentFrame = 0;
            _t = 0;
        }
    }

    private void SaveRecordingData()
    {
        SaveData<Vector3> dataPosition = new SaveData<Vector3>(_savedRecordingPosition);
        SaveData<Quaternion> dataRotation = new SaveData<Quaternion>(_savedRecordingRotation);

        string positionJSON = JsonUtility.ToJson(dataPosition);
        string rotationJSON = JsonUtility.ToJson(dataRotation);
        
        System.IO.File.WriteAllText(Application.persistentDataPath + "/recPos.json", positionJSON);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/recRot.json", rotationJSON);
        
    }

    private void SetRecordingData()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/recPos.json"))
        {
            string positionJson = System.IO.File.ReadAllText(Application.persistentDataPath + "/recPos.json");
            string rotationJson = System.IO.File.ReadAllText(Application.persistentDataPath + "/recRot.json");

            SaveData<Vector3> savedPosition = JsonUtility.FromJson<SaveData<Vector3>>(positionJson);
            SaveData<Quaternion> savedRotation = JsonUtility.FromJson<SaveData<Quaternion>>(rotationJson);
            _savedRecordingPosition = savedPosition.list;
            _savedRecordingRotation = savedRotation.list;
        }
    }

}

[System.Serializable]
public class SaveData<T>
{
    public List<T> list;

    public SaveData(List<T> data)
    {
        this.list = data;
    }
}
