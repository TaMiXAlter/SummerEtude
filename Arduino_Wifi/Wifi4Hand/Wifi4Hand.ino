

  /*
  ESP8266-NodeMCU作为TcpClient连接到服务器
*/
 
#include <ESP8266WiFi.h>                        // 本程序使用ESP8266WiFi库
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_ADXL345_U.h>

const char* ssid     = "雜膾兔套餐";                // 需要连接到的WiFi名
const char* password = "asdfghjkl";             // 连接的WiFi密码
Adafruit_ADXL345_Unified accel = Adafruit_ADXL345_Unified(12345);
                                  
void setup() {
  Serial.begin(115200);                         // 初始化串口通讯波特率为115200

//  WiFi.mode(WIFI_STA);                          // 设置Wifi工作模式为STA,默认为AP+STA模式
  WiFi.begin(ssid, password);                   // 通过wifi名和密码连接到Wifi
  Serial.print("\r\nConnecting to ");           // 串口监视器输出网络连接信息
  Serial.print(ssid); Serial.println(" ...");   // 显示NodeMCU正在尝试WiFi连接
  
  int i = 0;                                    // 检查WiFi是否连接成功
  while (WiFi.status() != WL_CONNECTED)         // WiFi.status()函数的返回值是由NodeMCU的WiFi连接状态所决定的。 
  {                                             // 如果WiFi连接成功则返回值为WL_CONNECTED
    delay(1000);                                // 此处通过While循环让NodeMCU每隔一秒钟检查一次WiFi.status()函数返回值
    Serial.print("waiting for ");                          
    Serial.print(i++); Serial.println("s...");       
  }                                             
                                               
  Serial.println("");                           // WiFi连接成功后
  Serial.println("WiFi connected!");            // NodeMCU将通过串口监视器输出"连接成功"信息。
  Serial.print("IP address: ");                 // 同时还将输出NodeMCU的IP地址。这一功能是通过调用
  Serial.println(WiFi.localIP());               // WiFi.localIP()函数来实现的。该函数的返回值即NodeMCU的IP地址。
  //================================================================================================
  #ifndef ESP8266
  while (!Serial); // for Leonardo/Micro/Zero
#endif
  Serial.begin(115200);
  Serial.println("Accelerometer Test"); Serial.println("");
  
  /* Initialise the sensor */
  if(!accel.begin())
  {
    /* There was a problem detecting the ADXL345 ... check your connections */
    Serial.println("Ooops, no ADXL345 detected ... Check your wiring!");
    while(1);
  }

   accel.setRange(ADXL345_RANGE_16_G);
   //==============
   pinMode(2, OUTPUT);
}

const char* host = "192.168.154.147";
const uint16_t port = 8052;

float lastvalue[3] = {0,0,0}; // 0 ==x 1 ==y 2 ==z
void loop() {
  /* 新建一个WiFiClient类对象，作为TCP客户端对象 */
  WiFiClient tcpclient;

  /* 建立TCP连接 */
 
  if (!tcpclient.connect(host, port)) {
     Serial.print("connecting to "); Serial.print(host); Serial.print(':'); Serial.println(port);
    Serial.println("connection failed");        // 如果连接失败，则打印连接失败信息，并返回
    delay(1000);
    return;
  }
  else{
    sensors_event_t event; 
      accel.getEvent(&event);
      float value[] = {event.acceleration.x,event.acceleration.y,event.acceleration.z};
      float sum;
      String msg = "H ";

      //全部加起來
    for(int i =0 ;i<3;i++){
      sum += abs(value[i]);
    } 
    //確認數值較大
    if(sum >50){
      msg += String(sum);
    }
    //若有數據傳輸資料
    if (tcpclient.available() == 0 and msg != "H " ) {
        tcpclient.println(msg);
        Serial.println(msg);
    }
    //儲存進陣列
    for(int i =0 ;i<3;i++){
        lastvalue[i] = value[i]; 
    } 
}
  /* 等待TCP服务器返回消息 */
//  Serial.println("waiting for receive from remote Tcpserver...");

//  
//  while (tcpclient.available()) {
//    
//  }
//  Serial.println();
//
//  /* 接受到服务器返回的消息后关闭TCP连接 */
//  Serial.println("closing connection");
//  tcpclient.stop();
  delay(20);
 }
