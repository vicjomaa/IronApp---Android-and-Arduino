#define NUMREADINGS 6  
#define MINSOUND 10   
#define MAXSOUND 125  

const int firstLED = 2;
const int lastLED = 6;
int leds;
int readings[NUMREADINGS];
int index = 0;
int total = 0;
int average = 0;
int sound = 0;

int x = 0;
int y = 0;

void setup() {
  Serial.begin(9600);
  for (int r = 0; r < NUMREADINGS; r++) {
      readings[r] = 0;
  }
  for (int p = firstLED; p <= lastLED; p++) {
      pinMode(p, OUTPUT);
  }
  leds = (lastLED - firstLED) + 1;
}

void loop() {
    total -= readings[index];
    readings[index] = analogRead(0);
    total += readings[index];
    index++;

    if (index >= NUMREADINGS) {
       index = 0;
    }

    average = abs((total / NUMREADINGS) - 338); // ((1024 * 300) / 1000) = ~338
    Serial.print("db:");
    Serial.println(average);
    sound = map(average, MINSOUND, MAXSOUND, 0, leds);


    for (int ledON = firstLED; ledON < (firstLED + sound); ledON++) {
        digitalWrite(ledON, HIGH);
    }

    for (int ledOFF = (firstLED + sound); ledOFF <= lastLED; ledOFF++) {
        digitalWrite(ledOFF, LOW);
    }



}

