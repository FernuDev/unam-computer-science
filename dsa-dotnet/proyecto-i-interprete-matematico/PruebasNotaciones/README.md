# ed-interprete-matematico-cs-demo

# Instrucciones

Para ejecutar el intérprete:
```
dotnet run --project Calculadora/Calculadora.csproj
```
Ejecutar pruebas:
```
dotnet test PruebasNotaciones/PruebasNotaciones.csproj
```

# Creación de la solución
```
dotnet new sln
dotnet new classlib -o Notaciones
dotnet sln add Notaciones/Notaciones.csproj

dotnet new console -o Calculadora
dotnet sln add Calculadora/Calculadora.csproj
dotnet add Calculadora/Calculadora.csproj reference Notaciones/Notaciones.csproj

dotnet new nunit -o PruebasNotaciones
dotnet sln add PruebasNotaciones/PruebasNotaciones.csproj
dotnet add PruebasNotaciones/PruebasNotaciones.csproj reference Notaciones/Notaciones.csproj
dotnet add PruebasNotaciones/PruebasNotaciones.csproj reference Calculadora/Calculadora.csproj
```