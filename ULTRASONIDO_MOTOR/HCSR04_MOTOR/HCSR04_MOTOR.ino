#include <HCSR04.h>

UltraSonicDistanceSensor mSensor(12, 13);  
int motor = 9;
float sensorVal = 0;

void setup () {
    Serial.begin(9600);  
    
}

void loop () {
    sensorVal = (float)mSensor.measureDistanceCm();
    Serial.print("dist:");
    Serial.println(sensorVal);
    int motorSpeed = map(sensorVal, 0,18,0,125);
    analogWrite(motor,motorSpeed);
    
    delay(10);
}
