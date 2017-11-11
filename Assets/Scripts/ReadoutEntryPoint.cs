using UnityEngine;
using UnityEngine.UI;
using System;

namespace UnityFacade
{
    public class ReadoutEntryPoint : MonoBehaviour
    {
        private static Vector2 upperLeft, bottomRight, minDevValue, maxDevValue, arrowBase, arrowPixel;
        private static Vector2[] inputBounds = new Vector2[10];
        private static int instructionNumber = 0;
        private static string curInstruction, maxValue = "";
        private static string[] instruction = new string[10];
        private WebCamTexture webCamTexture;
        private Color32[] data;
        public RawImage rawimage, chart;
        private GUIStyle currentStyle = null;
        //private static double curAns = -1;
        private double[] answers = new double[1000000];
        TimeSpan timeSpan;
        DateTime startTime;
        double ret, curAns;
        public GameObject lineRenderer;
        Vector3[] ans = new Vector3[10];

        struct StorArray
        {
            public Color value;
        }
        private static StorArray[,] storArr = new StorArray[700, 500];
        private static DateTime timer = DateTime.Now, timeOfStart;

        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < 10; i++)
                ans[i] = new Vector3(chart.transform.position.x - chart.rectTransform.rect.width / 2, chart.transform.position.y - chart.rectTransform.rect.height / 2, -1);
            for (int i = 0; i < 1000000; i++)
                answers[i] = 0;
            startTime = DateTime.Now;

            timeOfStart = DateTime.Now;
            //Screen.SetResolution(800, 600, false);
            var devices = WebCamTexture.devices;
            var camera = devices[0];
            webCamTexture = new WebCamTexture();

            webCamTexture.deviceName = devices[0].name;
            //webCamTexture.requestedFPS = 1;

            webCamTexture.Play();

            instruction[0] = "Позначте верхній лівий кут приладу";
            instruction[1] = "Позначте нижній правий кут приладу";
            instruction[2] = "Вкажіть на мінімальну відмітку на шкалі";
            instruction[3] = "Вкажіть на максимальну відмітку на шкалі";
            instruction[4] = "Вкажіть на основу стрілки приладу";
            instruction[5] = "Введіть максимальне значение на шкалі";
            curInstruction = instruction[0];
        }

        private void InitStyle()
        {
            //if (textStyle == null)
            //  textStyle.normal.textColor = Color.red;
            if (currentStyle == null)
            {
                currentStyle = new GUIStyle(GUI.skin.box);
                currentStyle.normal.background = MakeTex(2, 2, new Color(0.78f, 0.45f, 0.09f, 1f));
            }
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        private void OnGUI()
        {
            //GUI.Label(new Rect(50, Input.mousePosition.y, 100, 100), (Screen.height - Input.mousePosition.y).ToString());
            //if (Input.mousePosition.x > 0 && Input.mousePosition.x < rawimage.rectTransform.rect.width)
            //GUI.Label(new Rect(50, 70, 100, 100), (webCamTexture.GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y)).ToString());
            //GUI.Label(new Rect(50, 80, 100, 100), ((int)timeSpan.TotalSeconds).ToString());
            int cnt = 0;
            for (int i = (int)(chart.transform.position.x - chart.rectTransform.rect.width / 2); i <= (int)(chart.transform.position.x + chart.rectTransform.rect.width / 2); i += 30)
            {
                cnt++;
                GUI.Label(new Rect(i + 30, Screen.height - chart.transform.position.y + chart.rectTransform.rect.height / 2, 20, 20), ((int)timeSpan.TotalSeconds * cnt / 10).ToString());
                if (cnt >= 10)
                    break;
            }
            if (instructionNumber >= 5 && maxValue.Length > 0)
            {
                cnt = 0;
                for (int i = (int)(Screen.height - chart.transform.position.y + chart.rectTransform.rect.height / 2); i >= (int)(Screen.height - chart.transform.position.y - chart.rectTransform.rect.height / 2); i -= 20)
                {
                    cnt++;
                    GUI.Label(new Rect(chart.transform.position.x - chart.rectTransform.rect.width / 2, i - 20, 60, 20), (Convert.ToDouble(maxValue) / 10 * cnt).ToString());
                    if (cnt >= 10)
                        break;
                }
            }
            var pos = (int)rawimage.transform.position.x;
            GUI.Box(new Rect(10, 400, curInstruction.Length * 7 + 5, 30), "");
            GUI.Label(new Rect(15, 405, 400, 20), curInstruction);
            InitStyle();
            for (int i = 0; i <= 5; i++)
            {
                if (inputBounds[i].x != 0 && inputBounds[i].y != 0)
                    GUI.Box(new Rect(inputBounds[i].x - 5, inputBounds[i].y - 5, 10, 10), ""/*, currentStyle*/);
            }
            if (instructionNumber == 5)
            {
                GUI.Box(new Rect(300, 400, 180, 50), "Максимальне значення, A");
                maxValue = GUI.TextField(new Rect(350, 425, 80, 20), maxValue);

                for (int i = 0; i < maxValue.Length; i++)
                {
                    if (maxValue[i] < '0' || maxValue[i] > '9')
                    {
                        //GUI.Box(new Rect(495, 10, 205, 30), "");
                        GUI.Label(new Rect(500, 405, 200, 20), "Некоректно введені дані");
                        break;
                    }
                }

                /*if (ret != new Vector2(-1, -1))
                {
                    GUI.Box(new Rect(ret.x + 80, ret.y + 80, 5, 5), "", currentStyle);
                    //GUI.Box(new Rect(10, 10, 100, 100), ret.x.ToString());
                    //GUI.Label(new Rect(10, 20, 100, 100), ret.y.ToString());
                    curAns = ret;
                }
                else
                {
                    GUI.Box(new Rect(curAns.x + 80, curAns.y + 80, 5, 5), "", currentStyle);
                    //GUI.Box(new Rect(10, 10, 100, 100), curAns.x.ToString());
                    //GUI.Label(new Rect(10, 20, 100, 100), curAns.y.ToString());
                }*/
                if (ret != -1)
                {
                    ret = (int)ret;
                    answers[(int)(timeSpan.TotalSeconds)] = ret;
                    cnt = 0;
                    for (int i = (int)(chart.transform.position.x - chart.rectTransform.rect.width / 2); i <= (int)(chart.transform.position.x + chart.rectTransform.rect.width / 2); i += 30)
                    {
                        cnt++;
                        if ((int)answers[(int)(timeSpan.TotalSeconds * cnt / 10)] != 0)
                        {
                            ans[cnt - 1] = new Vector3(i + 30, chart.transform.position.y - chart.rectTransform.rect.height / 2 + (float)answers[(int)(timeSpan.TotalSeconds * cnt / 10)] / (float)Convert.ToDouble(maxValue) * chart.rectTransform.rect.height, -1);
                            //GUI.Label(new Rect(i + 30, Screen.height - chart.transform.position.y + chart.rectTransform.rect.height / 2 - (float)answers[(int)(timeSpan.TotalSeconds * cnt / 10)] / (float)Convert.ToDouble(maxValue) * chart.rectTransform.rect.height, 40, 40), ((int)answers[(int)(timeSpan.TotalSeconds * cnt / 10)]).ToString());
                        }
                        if (cnt >= 10)
                            break;
                    }
                    lineRenderer.GetComponent<LineRenderer>().SetPositions(ans);
                    GUI.Box(new Rect(10, 450, 130, 40), "показання приладу");
                    GUI.Label(new Rect(20, 465, 100, 100), ret.ToString());
                    GUI.Box(new Rect(chart.transform.position.x - chart.rectTransform.rect.width / 2, Screen.height - chart.transform.position.y + chart.rectTransform.rect.height / 2 - (float)ret / (float)Convert.ToDouble(maxValue) * chart.rectTransform.rect.height, 5, 5), "");
                    curAns = ret;
                }
                else
                if (curAns != -1)
                {
                    answers[(int)(timeSpan.TotalSeconds)] = curAns;
                    cnt = 0;
                    for (int i = (int)(chart.transform.position.x - chart.rectTransform.rect.width / 2); i <= (int)(chart.transform.position.x + chart.rectTransform.rect.width / 2); i += 30)
                    {
                        cnt++;
                        if ((int)answers[(int)(timeSpan.TotalSeconds * cnt / 10)] != 0)
                        {
                            ans[cnt - 1] = new Vector3(i + 30, chart.transform.position.y - chart.rectTransform.rect.height / 2 + (float)answers[(int)(timeSpan.TotalSeconds * cnt / 10)] / (float)Convert.ToDouble(maxValue) * chart.rectTransform.rect.height, -1);
                            //GUI.Label(new Rect(i + 30, Screen.height - chart.transform.position.y + chart.rectTransform.rect.height / 2 - (float)answers[(int)(timeSpan.TotalSeconds * cnt / 10)] / (float)Convert.ToDouble(maxValue) * chart.rectTransform.rect.height, 40, 40), ((int)answers[(int)(timeSpan.TotalSeconds * cnt / 10)]).ToString());
                        }
                        if (cnt >= 10)
                            break;
                    }
                    lineRenderer.GetComponent<LineRenderer>().SetPositions(ans);
                    GUI.Box(new Rect(10, 450, 130, 40), "показання приладу");
                    GUI.Label(new Rect(20, 465, 100, 100), ret.ToString());
                    //GUI.Box(new Rect(chart.transform.position.x - chart.rectTransform.rect.width / 2, Screen.height - (chart.transform.position.y - ((float)Convert.ToDouble(maxValue)) / chart.rectTransform.rect.height - chart.rectTransform.rect.height / 2), 5, 5), "");
                }
            }

        }

        // Update is called once per frame
        void Update()
        {
            timeSpan = DateTime.Now - startTime;
            if (webCamTexture.didUpdateThisFrame)
            {
                data = webCamTexture.GetPixels32();
                rawimage.texture = webCamTexture;
                DateTime curTime = DateTime.Now;

                ret = Readout(webCamTexture);
                timer = curTime;

            }
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (Input.GetMouseButtonDown(0))
            {
                if (instructionNumber < 5)
                {
                    inputBounds[instructionNumber] = mousePos;
                    instructionNumber++;
                }
            }
            curInstruction = instruction[instructionNumber];
        }

        public double Readout(WebCamTexture tex)
        {
            float maxDif = 0;
            arrowPixel = new Vector2(-1, -1);
            for (int i = (int)inputBounds[0].x; i < (int)inputBounds[1].x; i++)
            {
                for (int j = (int)inputBounds[0].y; j < (int)inputBounds[1].y; j++)
                {
                    var curPixel = tex.GetPixel(i, j);
                    //var dif = Math.Abs(pixel.r - storArr[i, j].value.r) + Math.Abs(pixel.b - storArr[i, j].value.b) + Math.Abs(pixel.g - storArr[i, j].value.g);
                    int res = 0;
                    for (int x = i - 2; x <= i + 2; x++)
                    {
                        for (int y = j - 2; y <= j + 2; y++)
                        {
                            if (x >= (int)inputBounds[0].x && x < (int)inputBounds[1].x && y >= (int)inputBounds[0].y && y < (int)inputBounds[1].y)
                            {
                                var pixel = tex.GetPixel(i, j);
                                if ((storArr[x, y].value.r <= 0.2 && storArr[x, y].value.g <= 0.2 && storArr[x, y].value.b <= 0.2) && (pixel.r >= 0.55 && pixel.g >= 0.55 && pixel.b >= 0.55))
                                {
                                    res++;
                                }
                            }
                        }
                    }
                    if (res > maxDif)
                    {
                        maxDif = res;
                        arrowPixel = new Vector2(i, j);
                    }
                    storArr[i, j].value = curPixel;
                }
            }
            //return arrowPixel;
            if (arrowPixel != new Vector2(-1, -1))
            {
                minDevValue = new Vector2(inputBounds[2].x, inputBounds[2].y);
                maxDevValue = new Vector2(inputBounds[3].x, inputBounds[3].y);
                arrowBase = new Vector2(inputBounds[4].x, inputBounds[4].y);

                double a = Math.Sqrt(Math.Pow(minDevValue.x - arrowBase.x, 2) + Math.Pow(minDevValue.y - arrowBase.y, 2));
                double b = Math.Sqrt(Math.Pow(maxDevValue.x - arrowBase.x, 2) + Math.Pow(maxDevValue.y - arrowBase.y, 2));
                double c = Math.Sqrt(Math.Pow(minDevValue.x - maxDevValue.x, 2) + Math.Pow(minDevValue.y - maxDevValue.y, 2));
                double d = Math.Sqrt(Math.Pow(arrowPixel.x - arrowBase.x, 2) + Math.Pow(arrowPixel.y - arrowBase.y, 2));
                double e = Math.Sqrt(Math.Pow(minDevValue.x - arrowPixel.x, 2) + Math.Pow(minDevValue.y - arrowPixel.y, 2));
                double alpha = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
                double gamma = Math.Acos((a * a + d * d - e * e) / (2 * a * d));
                return gamma / alpha * Convert.ToDouble(maxValue);
            }
            else
                return -1;
        }
    }
}













