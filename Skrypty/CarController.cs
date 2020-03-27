using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField] public float topSpeed = 200;
    [SerializeField] public int noOfGears = 5;
    [SerializeField] private static float revsRange = 1f;
    public float currentSpeed {get {return rb.velocity.magnitude*3.6f; }}
    private int gearNum = 0; 
    private float gearFactor;
    public float revs { get; private set; }
    public float accel{ get; private set; }
    private Rigidbody rb;

    public Text text;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        text.text = "speed: " + currentSpeed.ToString() + "\ngear: " + (gearNum + 1).ToString() + "\nrevs: " + revs.ToString();
        
    }

    public void Drive(float v, float b, float h, float vr, float hr) {
        accel = Mathf.Clamp(vr, 0, 1);
        rb.AddForce(new Vector3(h, 0, 0));
        if(h == 0){
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
        }

        if(b < 0){
            rb.AddForce(new Vector3(0, 0, b));
            rb.rotation = Quaternion.Euler(vr*2, 180+hr*3, hr*-5);
        } else if (b > 0)
        {
        rb.rotation = Quaternion.Euler(vr*2, 180+hr*3, hr*-5);
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z+v);
        } else {
            rb.rotation = Quaternion.Euler(0, 180+hr*3, hr*-5);
        }

        CapSpeed();
        CalculateRevs();
        GearChanging();

    }

    private void CapSpeed() {
        float speed = rb.velocity.magnitude;
        speed *= 3.6f;
        if(speed > topSpeed){
            rb.velocity = (topSpeed/3.6f) * rb.velocity.normalized;
        }
    }

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

    private void CalculateGearFactor() {
            float f = (1/(float) noOfGears);
            var targetGearFactor = Mathf.InverseLerp(f*gearNum, f*(gearNum + 1), Mathf.Abs(currentSpeed/topSpeed));
            gearFactor = Mathf.Lerp(gearFactor, targetGearFactor, Time.deltaTime*5f);
        }

    private void CalculateRevs() {
            // calculate engine revs (for display / sound)
            // (this is done in retrospect - revs are not used in force/power calculations)
            CalculateGearFactor();
            var gearNumFactor = gearNum/(float) noOfGears;
            var revsRangeMin = ULerp(0f, revsRange, CurveFactor(gearNumFactor));
            var revsRangeMax = ULerp(revsRange, 1f, gearNumFactor);
            revs = ULerp(revsRangeMin, revsRangeMax, gearFactor);
        }
    
    private static float ULerp(float from, float to, float value) {
            return (1.0f - value)*from + value*to;
        }

    private static float CurveFactor(float factor) {
            return 1 - (1 - factor)*(1 - factor);
        }
}
