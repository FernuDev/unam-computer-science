# Proyecto Serial Reader

Proyecto simple de lectura serial para un sensor ultra sonico
con Arduino Mega, para manejo y post procesado de datos, con 
meta de cambios para crear una libreria .h para facilitar la lectura de datos mediante el lenguaje C y asi mejorar computacion 
y electronica. 

### Instalacion y uso

Si desea probarlo primero debe de conectar el micro controlador que necesite, y configure el codigo necesario para la lectura de datos del sensor sonico mediante el mismo.

Asegurese de que su puerto tenga los permisos necesarios, en sistemas GNU/Linux debe ejecutar el comando 

`sudo chmod a+rw /dev/ttyACM0`

Cambiando por su puesto el puerto por el suyo donde se este conectando el micro controlador, si utiliza el IDE Arduino podrá ver el puerto al conectar la placa.

Este es el codigo .ino (Arduino file) utilizado junto con el proyecto 

```
const int Echo = 2;
const int Trigger = 4;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(Echo, INPUT);
  pinMode(Trigger, OUTPUT);
  digitalWrite(Trigger, LOW);
}

void loop() {
  // put your main code here, to run repeatedly:
  long t; //timepo que demora en llegar el eco
  long d; //distancia en centimetros

  digitalWrite(Trigger, HIGH);
  delayMicroseconds(10);          //Enviamos un pulso de 10us
  digitalWrite(Trigger, LOW);
  
  t = pulseIn(Echo, HIGH); //obtenemos el ancho del pulso
  d = t/59;             //escalamos el tiempo a una distancia en cm
  
  Serial.print(d);      //Enviamos serialmente el valor de la distancia
  Serial.println();
  delay(100);          //Hacemos una pausa de 100ms
}

```
