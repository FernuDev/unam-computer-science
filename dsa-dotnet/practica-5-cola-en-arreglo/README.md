[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-2e0aaae1b6195c2367325f4f02e2d04e9abb55f0b24a779b69b11b9e10269abc.svg)](https://classroom.github.com/online_ide?assignment_repo_id=16227384&assignment_repo_type=AssignmentRepo)
# ed-cola-en-arreglo-cs
Práctica para implementar una cola en un arreglo genérico.

## Instrucciones

Crea aquí el proyecto Cola con referencia a IColección:
```
dotnet new classlib -o Cola
dotnet sln add Cola/Cola.csproj
dotnet add Cola/Cola.csproj reference IColección/IColección.csproj
```

Para que las pruebas unitarias funcionen también debes agregar una referencia de las pruebas hacia el proyecto que creaste:
```
dotnet add NPruebasCola/NPruebasCola.csproj reference Cola/Cola.csproj
```

# Pruebas

Cuando hayas programado al menos el cascarón de la clase ColaEnArreglo, podrás probarla con:
```
dotnet test NPruebasCola/NPruebasCola.csproj
```