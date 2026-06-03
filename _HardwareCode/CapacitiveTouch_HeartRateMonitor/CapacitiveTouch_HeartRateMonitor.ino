/*
  Capacitive-Touch Aluminum Foil Keyboard
  + DFRobot SEN0203 Gravity Heart Rate Monitor Sensor

  Board:
  - Elegoo Arduino UNO R3

  Heart rate sensor:
  - DFRobot SEN0203
  - Sensor switch must be set to D / Digital Mode
  - Signal pin connected to A1
  - VCC connected to 5V
  - GND connected to GND

  Serial output:
  - Touch keys are printed only when a new key is pressed
  - Heart rate is printed at a configurable interval
*/

#include <CapacitiveSensor.h>
#include "DFRobot_Heartrate.h"

// -------------------- Capacitive touch keyboard settings --------------------

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

// -------------------- Heart rate sensor settings --------------------

#define HEART_RATE_PIN A1

// Default: print heart rate every 3 seconds.
// Change this value if you want faster or slower serial output.
#define HEART_RATE_PRINT_INTERVAL_MS 3000

// The SEN0203 must be switched to Digital Mode for this setting.
DFRobot_Heartrate heartrate(DIGITAL_MODE);

unsigned long lastHeartRatePrintTime = 0;
uint8_t currentHeartRate = 0;

// -------------------- Setup --------------------

void setup() {
  Serial.begin(9600);
  Serial.println("Capacitive Touch Keyboard Started");
  Serial.println("Heart Rate Sensor Started");
  Serial.println("SEN0203: Digital Mode, signal pin A1");

  // Turn off auto-calibration on all capacitive touch channels.
  // This keeps the keyboard behavior close to the original working sketch.
  for (int i = 0; i < NUM_OF_KEYS; ++i) {
    keys[i].set_CS_AutocaL_Millis(0xFFFFFFFF);
  }

  Serial.println("Setup complete. Touch a key or place your finger on the sensor...");
}

// -------------------- Main loop --------------------

void loop() {
  readCapacitiveKeyboard();
  readHeartRateSensor();
}

// -------------------- Capacitive touch keyboard --------------------

void readCapacitiveKeyboard() {
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
}

// -------------------- Heart rate sensor --------------------

void readHeartRateSensor() {
  // Let the DFRobot library read and process the sensor signal.
  heartrate.getValue(HEART_RATE_PIN);

  // Get the latest calculated heart rate value.
  currentHeartRate = heartrate.getRate();

  // Print heart rate only at the configured interval.
  if (millis() - lastHeartRatePrintTime >= HEART_RATE_PRINT_INTERVAL_MS) {
    lastHeartRatePrintTime = millis();

    Serial.print("Heart Rate: ");

    if (currentHeartRate > 0) {
      Serial.print(currentHeartRate);
      Serial.println(" BPM");
    } else {
      Serial.println("No valid reading");
    }
  }
}