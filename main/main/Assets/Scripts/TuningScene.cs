using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TuningScene : MonoBehaviour
{
    private GameObject Car, Wrota, Position, caly;
    private Text Cash;
    private Text Cost;
    private Text Alert;
    private float[] CostParts = {0,5000,7500,12000};
    private float cost = 0;
    float cash = 0;
    private float FB,RB,E,W;
    private int FBO,RBO,EO,WO;

    void Start()
    {
        Position = GameObject.Find("Garazv2");
        Wrota = GameObject.Find("Wrota");
        Car = GameObject.FindGameObjectWithTag("Player");
        caly = GameObject.Find("Canvas");
        //Position.transform.position = Car.transform.position;
        Car.GetComponent<CarTuning>().rotateCar = true;
        cash = Car.GetComponent<CarController>().cash;
        FBO = Car.GetComponent<CarTuning>().FrontBumperId;
        RBO = Car.GetComponent<CarTuning>().RearBumperId;
        EO = Car.GetComponent<CarTuning>().EngineId;
        WO = Car.GetComponent<CarTuning>().WheelId;
        Cash = GameObject.Find("Cash").GetComponent<Text>();
        Cost = GameObject.Find("Cost").GetComponent<Text>();
        Alert = GameObject.Find("Alert").GetComponent<Text>();
        Cash.text = "Cash : \t" + cash.ToString();
        Cost.text = "Cost : \t" + cost.ToString();
        Alert.enabled = false;
        /*Button btn1 = FB2.GetComponent<Button>();
        btn.onClick.AddListener(Car.GetComponent<CarTuning>().TakeFrontBumper);
        */       
    }
    private void Update() {
        if(Wrota.GetComponent<WyjazdGaraz>().GetKoniec()){
            Car.transform.Rotate(0,0,0);
            Car.transform.position = new Vector3(0,0.5f,0);
            SceneManager.LoadScene("Clients");
        };
    }
    private void ClickUpdate() {
        if(Car.GetComponent<CarTuning>().FrontBumperId == FBO) FB = 0;
        else FB = CostParts[Car.GetComponent<CarTuning>().FrontBumperId];
        if(Car.GetComponent<CarTuning>().RearBumperId == RBO) RB = 0;
        else RB = CostParts[Car.GetComponent<CarTuning>().RearBumperId];
        if(Car.GetComponent<CarTuning>().EngineId == EO) E = 0;
        else E = CostParts[Car.GetComponent<CarTuning>().EngineId];
        if(Car.GetComponent<CarTuning>().WheelId == WO) W = 0;
        else W = CostParts[Car.GetComponent<CarTuning>().WheelId/4];
        cost = FB + RB + E + W;
        if(cost > cash) Cost.color = Color.red;
        else Cost.color = Color.green;
        Cost.text = "Cost : \t" + cost.ToString();
    }
    public void AddFrontBumper(){
        Car.GetComponent<CarTuning>().AddFrontBumper();
        Alert.enabled = false;
        ClickUpdate();
    }
    public void TakeFrontBumper(){
        Car.GetComponent<CarTuning>().TakeFrontBumper();
        Alert.enabled = false;
        ClickUpdate();
    }
    public void AddRearBumper(){
        Car.GetComponent<CarTuning>().AddRearBumper();
        Alert.enabled = false;
        ClickUpdate();
    }
    public void TakeRearBumper(){
        Car.GetComponent<CarTuning>().TakeRearBumper();
        Alert.enabled = false;
        ClickUpdate();
    }
    public void AddEngine(){
        Car.GetComponent<CarTuning>().AddEngine();
        Alert.enabled = false;
        ClickUpdate();
    }
    public void TakeEngine(){
        Car.GetComponent<CarTuning>().TakeEngine();
        Alert.enabled = false;
        ClickUpdate();
    }
    public void AddWheel(){
        Car.GetComponent<CarTuning>().AddWheel();
        Alert.enabled = false;
        ClickUpdate();
    }
    public void TakeWheel(){
        Car.GetComponent<CarTuning>().TakeWheel();
        Alert.enabled = false;
        ClickUpdate();
    }
    public void Rotate(){
        Car.GetComponent<CarTuning>().SetCarRotation();
        Alert.enabled = false;
        ClickUpdate();
    }
    
    public void toGame()
    {
        Wrota.GetComponent<WyjazdGaraz>().SetOpen();
        Car.GetComponent<CarTuning>().FrontBumperId = FBO;
        Car.GetComponent<CarTuning>().RearBumperId = RBO;
        Car.GetComponent<CarTuning>().WheelId = WO;
        Car.GetComponent<CarTuning>().EngineId = EO;

        Car.GetComponent<CarController>().enabled = false;
        Car.GetComponent<CarUserControll>().enabled = false;
        Car.GetComponent<CarAudio>().enabled = false;
        Car.GetComponent<CarTuning>().rotateCar = false;
        Car.transform.Rotate(0,0,0);
        Car.transform.Rotate(0,-190,0);
        Car.GetComponent<CarTuning>().UpdateOnce();
        Car.GetComponent<CarTuning>().enabled = false;
        caly.GetComponent<Canvas> ().enabled = false;
        Alert.enabled = false;
       
    }
    public void Buy(){
        if(cost <= cash){
            FBO = Car.GetComponent<CarTuning>().FrontBumperId;
            RBO = Car.GetComponent<CarTuning>().RearBumperId;
            WO = Car.GetComponent<CarTuning>().WheelId;
            EO = Car.GetComponent<CarTuning>().EngineId;
            Alert.enabled = true;
            Alert.color = Color.green;
            Alert.text = "You bought ✔";
            cash -= cost;
            Car.GetComponent<CarController>().cash = cash;
            Cash.text = "Cash : \t" + cash.ToString();
            Car.GetComponent<CarTuning>().UpdateOnce();
            ClickUpdate();
        }else{
            Alert.enabled = true;
            Alert.color = Color.red;
            Alert.text = "You didn't bought X";
            Car.GetComponent<CarTuning>().UpdateOnce();
            ClickUpdate();
        }
    }
}