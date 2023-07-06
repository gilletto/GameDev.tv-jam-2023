using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{

    [SerializeField] int _level;
    [SerializeField] TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text.text = _level.ToString();
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene("level" + _level.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
