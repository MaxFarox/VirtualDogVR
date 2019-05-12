using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour {

    private int hunger;
    private int happy;
    private int clean;
    public TextMesh hungryText;
    public TextMesh happyText;
    public TextMesh cleanText;
    public Transform _happyBar;
    public Transform _hungryBar;
    public Transform _cleanBar;
    [SerializeField]
    private Material _mat;
    Color cleanColor = new Vector4(255, 255, 255, 1);


    // Use this for initialization
    void Start ()
    {
        
        UpdateStats();
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMaterial();
        hungryText.text = hunger.ToString();
        happyText.text =  happy.ToString();
        cleanText.text =  clean.ToString();

        UpdateBars();

        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            hunger = 100;
            happy = 100;
            clean = 100;
            UpdateDevice();
            Debug.Log("HUNGER " + hunger.ToString());
            Debug.Log("HAPPY " + happy.ToString());
            Debug.Log("CLEAN " + clean.ToString());
        }
	}
    private void SetUpStats()
    {
        
    }
    private void UpdateStats()
    {
        if (PlayerPrefs.HasKey("hunger"))
        {
            hunger = PlayerPrefs.GetInt("hunger");
        }
        else
        {
            hunger = 100;
            PlayerPrefs.SetInt("hunger", hunger);
        }
        if (PlayerPrefs.HasKey("happy"))
        {
            happy = PlayerPrefs.GetInt("happy");
        }
        else
        {
            happy = 100;
            PlayerPrefs.SetInt("happy", happy);
        }
        if (PlayerPrefs.HasKey("clean"))
        {
            clean = PlayerPrefs.GetInt("clean");
        }
        else
        {
            clean = 100;
            PlayerPrefs.SetInt("clean", clean);
        }
        if (!PlayerPrefs.HasKey("lastTime"))
        {
            PlayerPrefs.SetString("lastTime", GetStringTime());
        }

        TimeSpan timeSpent = GetTimeSpan();
        hunger -= (int)(timeSpent.TotalMinutes * 3);
        if (hunger<0)
        {
           hunger = 0;
        }
        else if (hunger>100)
        {
            hunger = 100;
        }
        
        clean -= (int)(timeSpent.TotalMinutes * 8);
        if (clean < 0)
        {
            clean = 0;
        }
        else if (clean > 100)
        {
            clean = 100;
        }
        happy -= (int)((100 - ((hunger+clean)/2)) * (timeSpent.TotalMinutes / 5));
        if (happy < 0)
        {
            happy = 0;
        }
        else if (happy > 100)
        {
            happy = 100;
        }

       

       // Debug.Log(GetTimeSpan().ToString());
       // Debug.Log(GetTimeSpan().TotalHours);
       // Debug.Log(hunger);
        //Debug.Log(happy);

      
            InvokeRepeating("UpdateDevice", 0f, 10f);
       
    }

    void UpdateDevice()
    {
        TimeSpan timeSpent = GetTimeSpan();

        hunger -= (int)(timeSpent.TotalMinutes * 10);
        if (hunger < 0)
        {
            hunger = 0;
        }
        else if (hunger > 100)
        {
            hunger = 100;
        }
        clean -= (int)(timeSpent.TotalMinutes * 15);
        if (clean < 0)
        {
            clean = 0;
        }
        else if (clean > 100)
        {
            clean = 100;
        }
        happy -= (int)((100 - ((hunger + clean) / 2)) * (timeSpent.TotalMinutes / 5));
        if (happy < 0)
        {
            happy = 0;
        }
        else if (happy > 100)
        {
            happy = 100;
        }

        PlayerPrefs.SetString("lastTime", GetStringTime());
        PlayerPrefs.SetInt("happy", happy);
        PlayerPrefs.SetInt("hunger", hunger);
        PlayerPrefs.SetInt("clean", clean);

    }
    

    string GetStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    TimeSpan GetTimeSpan()
    {
       
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("lastTime"));
        
    }

    public void SetHappy(int value)
    {
        happy += value;
        UpdateDevice();
    }
    public void SetHunger(int value)
    {
        hunger += value;
        UpdateDevice();
    }
    public void SetClean(int value)
    {
      
        if (clean+value > 100)
        {
           
        }
        else {
            clean += value;
        }
        
        UpdateDevice();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Water!!");
        }
    }

    private void UpdateBars()
    {
        _happyBar.localScale = new Vector3 (_happyBar.localScale.x, (float)happy / 100, _happyBar.localScale.z);
        _hungryBar.localScale = new Vector3(_hungryBar.localScale.x, (float)hunger / 100, _hungryBar.localScale.z);
        _cleanBar.localScale = new Vector3(_cleanBar.localScale.x, (float)clean / 100, _cleanBar.localScale.z);
    }

    private void UpdateMaterial()
    {
        _mat.SetFloat("_DirtyPct", 1-(float)clean/100);
    }
   

   
}
