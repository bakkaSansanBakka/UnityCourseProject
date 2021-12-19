using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTask : MonoBehaviour
{
    [SerializeField]
    Text textBox;
    [SerializeField]
    Text temperatureTextBox;

    [SerializeField]
    FixTubeInRack fixTubeInRackScript;
    [SerializeField]
    PullTubeWithMalachit pullTubeWithMalachitScript;
    [SerializeField]
    AddLimeWaterInTube addLimeWaterInTubeScript;
    [SerializeField]
    FixGasTubeInTestTube fixGasTubeInTestTubeScript;
    [SerializeField]
    MoveCap moveCapScript;
    [SerializeField]
    FireSpiritlamp fireSpiritlampScript;
    [SerializeField]
    TableManage tableManageScript;

    int index;
    Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = "Выберите задание";
        dropdown = transform.GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate { SelectTask(); });

        fixTubeInRackScript.propertyChanged += SelectTask;
        pullTubeWithMalachitScript.propertyChanged += SelectTask;
        addLimeWaterInTubeScript.propertyChanged += SelectTask;
        fixGasTubeInTestTubeScript.propertyChanged += SelectTask;
        moveCapScript.propertyChanged += SelectTask;
        fireSpiritlampScript.propertyChanged += SelectTask;
        tableManageScript.propertyChanged += SelectTask;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectTask()
    {
        index = dropdown.value;

        switch (index)
        {
            case 0:
                textBox.text = "Выберите задание";
                fixTubeInRackScript.SetInitialPosition();
                pullTubeWithMalachitScript.DestroyMalachitInTube();
                addLimeWaterInTubeScript.DestroyLimewaterInTube();
                fixGasTubeInTestTubeScript.SetInitialPosition();
                moveCapScript.SetInitialPosition();
                fireSpiritlampScript.PutOutFire();
                tableManageScript.isWrittenToTable = false;
                temperatureTextBox.text = "20";
                break;
            case 1:
                textBox.text = "1. Закрепите пробирку в держателе штатива";
                if (dropdown.value == 1 && fixTubeInRackScript.tubeIsFixed)
                {
                    dropdown.value = 2;
                }
                break;
            case 2:
                textBox.text = "2. Засыпьте в пробирку порошок малахита";
                if (dropdown.value == 2 && fixTubeInRackScript.tubeIsFixed && pullTubeWithMalachitScript.malachiteIsInTube)
                {
                    dropdown.value = 3;
                }
                break;
            case 3:
                textBox.text = "3. Налейте во вторую пробирку, стоящую в штативе для пробирок, известковую воду";
                if (dropdown.value == 3 && fixTubeInRackScript.tubeIsFixed && pullTubeWithMalachitScript.malachiteIsInTube 
                    && addLimeWaterInTubeScript.limeWaterIsInTube)
                {
                    dropdown.value = 4;
                }
                break;
            case 4:
                textBox.text = "4. Закройте пробирку пробкой с газоотводной трубкой и опустите второй конец газоотводной трубки во вторую пробирку";
                if (dropdown.value == 4 && fixTubeInRackScript.tubeIsFixed && pullTubeWithMalachitScript.malachiteIsInTube
                    && addLimeWaterInTubeScript.limeWaterIsInTube && fixGasTubeInTestTubeScript.gasTubeIsFixed)
                {
                    dropdown.value = 5;
                }
                break;
            case 5:
                textBox.text = "5. Снимите крышку со спиртовки и зажгите её";
                if (dropdown.value == 5 && fixTubeInRackScript.tubeIsFixed && pullTubeWithMalachitScript.malachiteIsInTube
                    && addLimeWaterInTubeScript.limeWaterIsInTube && fixGasTubeInTestTubeScript.gasTubeIsFixed
                    && moveCapScript.capIsPutOff && fireSpiritlampScript.fireIsLit)
                {
                    dropdown.value = 6;
                }
                break;
            case 6:
                textBox.text = "6. Прогрейте пробирку, закрепленную в штативе, до тех пор, пока малахит в ней не почернеет";
                if (dropdown.value == 6 && fixTubeInRackScript.tubeIsFixed && pullTubeWithMalachitScript.malachiteIsInTube
                    && addLimeWaterInTubeScript.limeWaterIsInTube && fixGasTubeInTestTubeScript.gasTubeIsFixed
                    && moveCapScript.capIsPutOff && fireSpiritlampScript.fireIsLit && fireSpiritlampScript.malachitIsDark)
                {
                    dropdown.value = 7;
                }
                break;
            case 7:
                textBox.text = "7. Занесите температуру пробирки в таблицу";
                if (dropdown.value == 7 && fixTubeInRackScript.tubeIsFixed && pullTubeWithMalachitScript.malachiteIsInTube
                    && addLimeWaterInTubeScript.limeWaterIsInTube && fixGasTubeInTestTubeScript.gasTubeIsFixed
                    && moveCapScript.capIsPutOff && fireSpiritlampScript.fireIsLit && fireSpiritlampScript.malachitIsDark
                    && tableManageScript.isWrittenToTable)
                {
                    dropdown.value = 8;
                }
                break;
            case 8:
                textBox.text = "8. Отсоедините пробку с газоотводной трубкой";
                if (dropdown.value == 8 && fixTubeInRackScript.tubeIsFixed && pullTubeWithMalachitScript.malachiteIsInTube
                    && addLimeWaterInTubeScript.limeWaterIsInTube && !fixGasTubeInTestTubeScript.gasTubeIsFixed
                    && moveCapScript.capIsPutOff && fireSpiritlampScript.fireIsLit && fireSpiritlampScript.malachitIsDark
                    && tableManageScript.isWrittenToTable)
                {
                    dropdown.value = 9;
                }
                break;
            case 9:
                textBox.text = "9. Потушите спиртовку и закройте её";
                if (dropdown.value == 9 && fixTubeInRackScript.tubeIsFixed && pullTubeWithMalachitScript.malachiteIsInTube
                    && addLimeWaterInTubeScript.limeWaterIsInTube && !fixGasTubeInTestTubeScript.gasTubeIsFixed
                    && !moveCapScript.capIsPutOff && !fireSpiritlampScript.fireIsLit && fireSpiritlampScript.malachitIsDark
                    && tableManageScript.isWrittenToTable)
                {
                    dropdown.value = 10;
                }
                break;
            case 10:
                textBox.text = "10. Налейте в пробирку раствор серной кислоты и наблюдайте изменение цвета раствора";
                break;
            default: 
                break;
        }
    }
}
