using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyjazdGaraz : MonoBehaviour
{
    // Start is called before the first frame update
  private bool Open = true;
//  bool NotOpen = false;
private GameObject Car;
[SerializeField]private Transform Cel1, Cel2;
public Camera cam1,cam2;
bool cel1 = true;
bool wyjazd = false;
bool koniec = false;
Vector3 kierunek;
  [SerializeField]private float rotationSpeed = 30;
  private void Start() {
    Cel1 = GameObject.Find("Cel1").transform;
    Cel2 = GameObject.Find("Cel2").transform;
    Car = GameObject.FindGameObjectWithTag("Player");
    cam1.enabled = true;
    cam2.enabled = false;
  }
    void Opening(){
       this.transform.Rotate(Vector3.left * ( rotationSpeed * Time.deltaTime));
       if(this.transform.rotation.eulerAngles.x < 190 ){
         Open = true;
         wyjazd = true;
       }
        
    }
    void Close(){
        this.transform.Rotate(Vector3.right * ( rotationSpeed * Time.deltaTime));
        if(this.transform.rotation.eulerAngles.x < 90 )Open = true;
    }
    public void Wyjazd(){
      if(!koniec){
        if(cel1){
          kierunek = Cel1.position - Car.transform.position;
          Car.transform.rotation = Quaternion.Slerp(Car.transform.rotation, Quaternion.LookRotation(kierunek), Time.deltaTime * 2);
          Car.transform.Translate(0,0, 5 * Time.deltaTime);
          if(kierunek.magnitude < 1){
            cel1 = false;
          }
        }else{
          kierunek = Cel2.position - Car.transform.position;
          Car.transform.rotation = Quaternion.Slerp(Car.transform.rotation, Quaternion.LookRotation(kierunek), Time.deltaTime * 5);
          Car.transform.Translate(0,0, 5 * Time.deltaTime);
          if(kierunek.magnitude < 1){
            Debug.Log("Koniec");
            koniec = true;
          }
        }
      }
    }
  void LateUpdate()
	{
    if(wyjazd)Wyjazd();
	}
   private void Update() {
       if(!Open)Opening();
    }
    public void SetOpen(){
      cam1.enabled = false;
      cam2.enabled = true;
        Open = !Open;
    }
    public bool GetKoniec(){
      return koniec;
    }

}
