const int LED = 2;
double Time = 0;
double Trust = 0;

double Empuje(long double Time){
    return -0.0057*pow(Time, 6) + 0.1924*pow(Time, 5) - 2.3563*pow(Time, 4) + 15.8692*pow(Time, 3) - 62.8779*pow(Time, 2) + 133.6287*Time;
}

void setup(){
    // Setting up the band for the Serial Transmision

    Serial.begin(9600);
    pinMode(LED, OUTPUT);

}

void loop(){
    
    Time ++;
    float empuje = Empuje(Time/10) / 100;
    if(empuje > 0){
      Serial.println( empuje);
    }else {
      Serial.println(0.0);
    }
    delay(100);
}


