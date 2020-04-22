using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    private const float MaxAngle = -20.0f;
    private const float MinAngle = 210.0f;
    private Transform needleTransform;
    private Transform SpeedLabelTransform;

    private CarController CarControllerScript;

    private float SpeedMax = 0.0f;
    private float SpeedCurrent;

    private void Start()
    {

    }

    private void Awake()
    {
        needleTransform = transform.Find("needle");
        SpeedLabelTransform = transform.Find("SpeedLabel");
        SpeedLabelTransform.gameObject.SetActive(false);

        CarControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>() as CarController;
        SpeedMax = CarControllerScript.topSpeed;
        CreateSpeedLabels();
    }

    private void Update()
    {
        SpeedCurrent = CarControllerScript.currentSpeed;
        needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }

    private void CreateSpeedLabels()
    {
        int LabelAmount = 10;
        float totalAngleSize = MinAngle - MaxAngle;

        for (int i = 0; i <= LabelAmount; i++)
        {
            Transform SpeedLabel = Instantiate(SpeedLabelTransform, transform);
            float SpeedNormalizedLabel = (float)i / LabelAmount;
            SpeedLabel.eulerAngles = new Vector3(0, 0, MinAngle - SpeedNormalizedLabel * totalAngleSize);
            SpeedLabel.Find("SpeedText").GetComponent<Text>().text = Mathf.RoundToInt(SpeedNormalizedLabel * SpeedMax).ToString();
            SpeedLabel.Find("SpeedText").eulerAngles = new Vector3(0, 0, 0);
            SpeedLabel.gameObject.SetActive(true);
        }

        needleTransform.SetAsLastSibling();
    }

    private float GetSpeedRotation()
    {
        float totalAngleSize = MinAngle - MaxAngle;
        float speedNormalized = SpeedCurrent / SpeedMax;

        //Debug.Log(totalAngleSize);
        //Debug.Log(speedNormalized);
        //Debug.Log(MinAngle - speedNormalized * totalAngleSize);
        
        return MinAngle - speedNormalized * totalAngleSize;
    }
}
