int sensorVal = 0;

int pitch = 0;

void setup() {
  Serial.begin(9600);
}

void loop() {
  
  sensorVal = analogRead(A0);
  Serial.print("ldr:");
  Serial.println(sensorVal);
  pitch = map(sensorVal, 400, 1000, 120, 1500);
  tone(9, pitch, 10);
  delay(1);        
}
