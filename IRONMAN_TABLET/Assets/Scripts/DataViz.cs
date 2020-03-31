using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

//using TMPro;


public class DataViz : MonoBehaviour {

    private SerialController mSerialController;

    /*Textos Sensores*/
    private Text textoTemperatura1, textoTemperatura2, textoLDR, textoDistancia, textoMic;
    
    /*Texto Hora*/
    public Text horaActual;

    /*Active UI*/
    private GameObject ldrActive, micActive, distActive;
    private bool s1Active, s2Active, s3Active;

    /*Lights*/
    private GameObject[] mIronLights ;

    /*Sensor Values*/
    int ldrVal;
    float tempVal;

    //Scanner-Slider
    private Slider sliderScanner;

    //Scanner GameObject
    public GameObject mScanner;



    // Use this for initialization
    void Start () {

        mSerialController = GameObject.Find("SerialController").GetComponent<SerialController>();

        s1Active = false;
        s2Active = false;
        s3Active = false;
                
        ldrActive = GameObject.Find("s1Active");
        distActive = GameObject.Find("s2Active");
        micActive = GameObject.Find("s3Active");
      
        ldrActive.SetActive(s1Active);
        distActive.SetActive(s2Active);
        micActive.SetActive(s3Active);



        textoLDR = GameObject.Find("SENSOR1").GetComponent<Text>();
        textoTemperatura1 = GameObject.Find("SENSOR4tEMP").GetComponent<Text>();
        textoTemperatura2 = GameObject.Find("SENSOR4tEMP1").GetComponent<Text>();
        textoDistancia = GameObject.Find("SENSOR2").GetComponent<Text>();
        textoMic = GameObject.Find("SENSOR3").GetComponent<Text>();


        sliderScanner = GameObject.Find("sliderScanner").GetComponent<Slider>();

      

        mScanner = GameObject.Find("Mascaras_Main");
        scanner();



        ldrVal = 30;
        mIronLights = GameObject.FindGameObjectsWithTag("IronLlights");
        foreach (GameObject ironLight in mIronLights)
        {
            ironLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0070f, 1.0f, 1.0f, 1.0f) * (ldrVal * 1.0f));

        }

    }
	

	void Update () {
        horaActual.text = System.DateTime.Now.ToString();
        string message = mSerialController.ReadSerialMessage();

        if (message == null) {
            return;
        }

       

        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Conexion Ok");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Conexion Off");

        else {
/* ************************************************************************************** */
         // Debug.Log(message);
            string[] data = message.Split(':');

            if (data[0].Equals("ldr")) {

                setActiveZone(1, true);
                textoLDR.text = data[1];
                ldrVal = int.Parse(data[1]);
                foreach (GameObject ironLight in mIronLights)
                {
                    ironLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0070f, 1.0f, 1.0f, 1.0f) * (ldrVal * 0.1f));

                }

            } else if (data[0].Equals("temp")) {
                setActiveZone(0,false);
                string[] subArray = data[1].Split(',');
                textoTemperatura1.text = subArray[0];
                textoTemperatura2.text = subArray[1];

                tempVal = float.Parse(subArray[0]);
                if (tempVal > 30) {
                    /*foreach (GameObject ironLight in mIronLights)
                    {
                        ironLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1.0f, 0.0f, 0.0f, 1.0f) * (ldrVal * 0.1f));

                    }*/
                   
                }


            }
            else if (data[0].Equals("dist"))
            {
                setActiveZone(2, true);
                textoDistancia.text = data[1];  




            }

            else if (data[0].Equals("db"))
            {
                setActiveZone(2, true);
                textoMic.text = data[1];
              


            }
/* ************************************************************************************** */
        }

    }


   public void loadCamera() {
        SceneManager.LoadScene("visionJarvis");
        
    }

    public void scanner() {
        float scannerVal = sliderScanner.value;
       /* if (scannerVal > 0.65f) {
            scannerVal = 0.65f;
        }*/
        Debug.Log(scannerVal);
        //scannerVal = mMap(scannerVal,0f,1.0f,-10.0f,-3.0f);
        mScanner.transform.position = new Vector3(scannerVal, mScanner.transform.position.y, mScanner.transform.position.z);
    }

    public float mMap( float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }





    void setActiveZone(int indexSensor, bool sState) {

        switch (indexSensor) {
            case 0:
               
                    ldrActive.SetActive(false);
                    distActive.SetActive(false);
                    micActive.SetActive(false);
                break;

            case 1:
                if (sState)
                {
                    ldrActive.SetActive(sState);
                    distActive.SetActive(!sState);
                    micActive.SetActive(!sState);

                } 

                break;
            case 2:
                if (sState)
                {
                    ldrActive.SetActive(!sState);
                    distActive.SetActive(sState);
                    micActive.SetActive(!sState);

                }

                break;
            case 3:
                if (sState)
                {
                    ldrActive.SetActive(!sState);
                    distActive.SetActive(!sState);
                    micActive.SetActive(sState);

                }
                break;
        }
    }





}

    

