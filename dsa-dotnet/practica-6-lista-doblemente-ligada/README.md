[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-2e0aaae1b6195c2367325f4f02e2d04e9abb55f0b24a779b69b11b9e10269abc.svg)](https://classroom.github.com/online_ide?assignment_repo_id=16318600&assignment_repo_type=AssignmentRepo)
# ed-lista-doblemente-ligada-cs
Práctica de implementación de una lista doblemente ligada

## Instrucciones

Crea aquí el proyecto Lista con referencia a IColección:
```
dotnet new classlib -o Lista
dotnet sln add Lista/Listaa.csproj
dotnet add Lista/Lista.csproj reference IColección/IColección.csproj
```

Para que las pruebas unitarias funcionen también debes agregar una referencia de las pruebas hacia el proyecto que creaste:
```
dotnet add NPruebasLista/NPruebasLista.csproj reference Lista/Lista.csproj
```

# Pruebas

Cuando hayas programado al menos el cascarón de la clase ListaEnArreglo, podrás probarla con:
```
dotnet test NPruebasLista/NPruebasLista.csproj
```
