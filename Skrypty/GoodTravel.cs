using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoodTravel : MonoBehaviour
{
    private GameObject car;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player");
        if(car.GetComponent<CarController>().Travel){
            text.text = "You took the customer to the destination.\n"+car.GetComponent<CarController>().ClientName;
        }else{
            text.text = "You didn't take the customer to the destination.";
        }
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarUserControll>().enabled = false;
        car.GetComponent<CarAudio>().StopSound();
        car.GetComponent<CarAudio>().enabled = false;
        car.GetComponent<CarTuning>().enabled = false;
        car.transform.Rotate(0,0,0);
    }
    public void ClientScene(){
        car.GetComponent<CarUserControll>().enabled = false;
        car.GetComponent<CarAudio>().enabled = false;
        car.GetComponent<CarTuning>().enabled = false;
        car.GetComponent<CarController>().Travel = false;
        car.GetComponent<CarController>().pivot = true;
        car.GetComponent<CarController>().enabled = false;
        SceneManager.LoadScene("Clients");
    }
}
