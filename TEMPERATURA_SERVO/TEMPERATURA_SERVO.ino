#define sensorPin A0

uint16_t sensorVal;
double tempC, tempF;

void setup()
{
    Serial.begin(9600);
}

void loop()
{ 

    sensorVal=analogRead(sensorPin);
    tempC = (double) sensorVal * (5/10.24); 
    tempF = (double) (tempC * 9/5) + 32;
    
    Serial.print("temp:"); 
    Serial.print(tempC);
    Serial.print(",");
    Serial.println(tempF);
    delay(10);
}
