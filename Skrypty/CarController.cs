using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
    [SerializeField] private GameObject[] WheelMeshes = new GameObject[2];
    [SerializeField] private Skid[] skid = new Skid[2];
    [SerializeField] public float topSpeed = 200; //maksymalna predkosc samochodu
    [SerializeField] public float speedDownLimit = 0;
    [SerializeField] public int noOfGears = 5; //ilosc biegow
    [SerializeField] private static float revsRange = 1f; //zakres obrotow silnika na jednym biegu (od 0)
    public float currentSpeed {get {return rb.velocity.z*3.6f; }}
    private int gearNum = 0; //aktualny bieg
    private float gearFactor;
    public float revs { get; private set; } //obroty silnika
    public float accel{ get; private set; }
    private Rigidbody rb;
    public Text text; //tekst na ekranie (domyslnie predkosc, aktualny bieg i obroty silnika)
    public Text distance;
    public bool breaking { get; private set; }
    float distanceTravelled = 0;
    Vector3 lastPosition;
    private CashManager cashManager;
    private int cash = 0;

    bool loaded = false;

    void Start()
    {
        breaking = false;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speedDownLimit/3.6f);
        lastPosition = transform.position;
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Game" && loaded is false)
        {
            text = GameObject.Find("Text").GetComponent<Text>();
            distance = GameObject.Find("DistanceText").GetComponent<Text>();
            loaded = true;
            cashManager = GameObject.Find("CashManager").GetComponent<CashManager>();
        }
        
        text.text = "speed: " + ((int)currentSpeed + 1).ToString() + "\ngear: " + (gearNum + 1).ToString() + "\nrevs: " + ((int)(revs*1000)).ToString() + "\ncash: " + cash.ToString() + " $";
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        distance.text = "Distance: " + (distanceTravelled / 1000 ).ToString("f2") + " KM";
        lastPosition = transform.position;
    }

    public void Drive(float v, float b, float h, float vr, float hr) {
        accel = Mathf.Clamp(vr, 0, 1);
        rb.velocity = new Vector3(rb.velocity.x+h, 0, rb.velocity.z);
        if(h == 0) {
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
        }
        if(b < 0 && rb.velocity.z * 3.6f > speedDownLimit + 1) {
            rb.AddForce(new Vector3(0, 0, b));
            rb.rotation = Quaternion.Euler(vr*2, 180+hr*3, hr*-5);
        } else if (b > 0) {
            rb.rotation = Quaternion.Euler(vr*2, 180+hr*3, hr*-5);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z+v);
        } else {
            rb.rotation = Quaternion.Euler(0, 180+hr*3, hr*-5);
        }

        CapSpeed();
        CalculateRevs();
        GearChanging();
        Break(vr);
    }

    private void Break(float br){
        if(br < 0 && !skid[0].PlayingAudio && rb.velocity.z * 3.6f > speedDownLimit + 1) {
            for (int i = 0; i < 2; i++){
                skid[i].PlayAudio();
                //StartCoroutine(skid[i].StartSkidTrail());
                breaking = true;
            }
        } else if (br >= 0 || rb.velocity.z * 3.6f <= speedDownLimit + 1) {
            for (int i = 0; i < 2; i++){
                skid[i].StopAudio();
                //skid[i].EndSkidTrail();
                breaking = false;
            }
        }
    }

    private void CapSpeed() {
        float speed = rb.velocity.z;
        speed *= 3.6f;
        if(speed > topSpeed){
            rb.velocity = (topSpeed/3.6f) * rb.velocity.normalized;
        }
        if(speed <= speedDownLimit ){
            rb.velocity = new Vector3(rb.velocity.x, 0, speedDownLimit/3.6f);
        }
    }

    //funkcja zmieniajaca bieg w zaleznosci od aktualnej predkosci
    private void GearChanging() {
            float f = Mathf.Abs(currentSpeed/topSpeed);
            float upgearlimit = (1/(float) noOfGears)*(gearNum + 1);
            float downgearlimit = (1/(float) noOfGears) * gearNum;

            if (gearNum > 0 && f < downgearlimit)
            {
                gearNum--;
            }

            if (f > upgearlimit && (gearNum < (noOfGears - 1)))
            {
                gearNum++;
            }
        }

    //wspolczynnik przekladni jest znormalizowana reprezentacja biezacej predkosci samochodu w zakresie predkosci aktualnego biegu
    private void CalculateGearFactor() {
            float f = (1/(float) noOfGears);
            var targetGearFactor = Mathf.InverseLerp(f*gearNum, f*(gearNum + 1), Mathf.Abs(currentSpeed/topSpeed));
            gearFactor = Mathf.Lerp(gearFactor, targetGearFactor, Time.deltaTime*5f);
        }

    //obliczanie obrotow silnika, wykorzystywane w odtwarzaniu dzwieku przyspieszania i zwalniania
    private void CalculateRevs() {
            CalculateGearFactor();
            var gearNumFactor = gearNum/(float) noOfGears;
            var revsRangeMin = ULerp(0f, revsRange, CurveFactor(gearNumFactor));
            var revsRangeMax = ULerp(revsRange, 1f, gearNumFactor);
            revs = ULerp(revsRangeMin, revsRangeMax, gearFactor);
        }
    
    //niezablokowana wersja funkcji Lerp pozwalająca na przekraczanie wartości zakresu od-do
    private static float ULerp(float from, float to, float value) {
            return (1.0f - value)*from + value*to;
        }

    //funkcja dodawania zakrzywionego odchylenia w kierunku 1 dla wartości z zakresu 0-1
    private static float CurveFactor(float factor) {
            return 1 - (1 - factor)*(1 - factor);
        }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Cash")){
            cash += 100;
            cashManager.DeleteCash();
        }
    }
}
