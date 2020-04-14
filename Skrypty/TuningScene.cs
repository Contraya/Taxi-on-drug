using UnityEngine;
using UnityEngine.UI;


public class TuningScene : MonoBehaviour
{
    private GameObject Car;
    public Text Cash;
    int cash = 0;
    

    //public Button FB2;
    // Start is called before the first frame update
    void Start()
    {
        Car = GameObject.FindGameObjectWithTag("Player");
        //Car.GetComponent<CarController>().cash = 2000; //ustawanie kasy
        cash = Car.GetComponent<CarController>().cash;
        Cash.text = "Cash : " + cash.ToString();
        
        /*Button btn1 = FB2.GetComponent<Button>();
        btn.onClick.AddListener(Car.GetComponent<CarTuning>().TakeFrontBumper);
        Button btn2 = RB1.GetComponent<Button>();
        btn2.onClick.AddListener(Car.GetComponent<CarTuning>().AddRearBumper);
        Button btn8 = RB2.GetComponent<Button>();
        btn8.onClick.AddListener(Car.GetComponent<CarTuning>().TakeRearBumper);
        Button btn3 = S1.GetComponent<Button>();
        btn3.onClick.AddListener(Car.GetComponent<CarTuning>().AddEngine);
        Button btn4 = S2.GetComponent<Button>();
        btn4.onClick.AddListener(Car.GetComponent<CarTuning>().TakeEngine);
        Button btn5 = W1.GetComponent<Button>();
        btn5.onClick.AddListener(Car.GetComponent<CarTuning>().AddWheel);
        Button btn6 = W2.GetComponent<Button>();
        btn6.onClick.AddListener(Car.GetComponent<CarTuning>().TakeWheel);  
        Button btn7 = Rotate.GetComponent<Button>();
        btn7.onClick.AddListener(Car.GetComponent<CarTuning>().SetCarRotation);
        */       
    }
    public void AddFrontBumper(){
        Car.GetComponent<CarTuning>().AddFrontBumper();
    }
    public void TakeFrontBumper(){
        Car.GetComponent<CarTuning>().TakeFrontBumper();
    }
    public void AddRearBumper(){
        Car.GetComponent<CarTuning>().AddRearBumper();
    }
    public void TakeRearBumper(){
        Car.GetComponent<CarTuning>().TakeRearBumper();
    }
    public void AddEngine(){
        Car.GetComponent<CarTuning>().AddEngine();
    }
    public void TakeEngine(){
        Car.GetComponent<CarTuning>().TakeEngine();
    }
    public void AddWheel(){
        Car.GetComponent<CarTuning>().AddWheel();
    }
    public void TakeWheel(){
        Car.GetComponent<CarTuning>().TakeWheel();
    }
    public void Rotate(){
        Car.GetComponent<CarTuning>().SetCarRotation();
    }
}
