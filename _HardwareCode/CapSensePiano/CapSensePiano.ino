/*
  Capacitive-Touch Arduino Keyboard
*/

#include <CapacitiveSensor.h>

#define COMMON_PIN      2
#define NUM_OF_SAMPLES  20
#define CAP_THRESHOLD   100
#define NUM_OF_KEYS     8

#define CS(Y) CapacitiveSensor(COMMON_PIN, Y)

const char* keyNames[] = {
  "Key 0", "Key 1", "Key 2", "Key 3",
  "Key 4", "Key 5", "Key 6", "Key 7"
};

int thresholds[] = {
  CAP_THRESHOLD, CAP_THRESHOLD, CAP_THRESHOLD, CAP_THRESHOLD,
  CAP_THRESHOLD, CAP_THRESHOLD, CAP_THRESHOLD, CAP_THRESHOLD
};

CapacitiveSensor keys[] = {
  CS(3), CS(4), CS(5), CS(6),
  CS(7), CS(8), CS(9), CS(10)
};

int lastPressedKey = -1;

void setup() {
  Serial.begin(9600);
  Serial.println("Capacitive Touch Keyboard Started");

  for (int i = 0; i < NUM_OF_KEYS; ++i) {
    keys[i].set_CS_AutocaL_Millis(0xFFFFFFFF);
  }

  Serial.println("Setup complete. Touch a key...");
}

void loop() {
  int strongestKey = -1;
  long strongestValue = 0;

  for (int i = 0; i < NUM_OF_KEYS; ++i) {
    long sensorValue = keys[i].capacitiveSensor(NUM_OF_SAMPLES);

    if (sensorValue > thresholds[i] && sensorValue > strongestValue) {
      strongestValue = sensorValue;
      strongestKey = i;
    }
  }

  if (strongestKey != -1 && strongestKey != lastPressedKey) {
    Serial.print("Pressed: ");
    Serial.print(keyNames[strongestKey]);
    Serial.print(" - Value: ");
    Serial.println(strongestValue);
  }

  lastPressedKey = strongestKey;

  delay(50);
}