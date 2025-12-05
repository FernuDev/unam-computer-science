/**
 * Direcciones alrededor de cada celda.
 * @author blackzafiro
 */
using System;
using System.Reflection;

namespace Laberinto;
//https://stackoverflow.com/questions/469287/c-sharp-vs-java-enum-for-those-new-to-c
public static class Direcciones
{
	/**
	 * Devuelve el objeto enum de la dirección opuesta.
	 * @return 
	 */
	public static Dirección Opuesta(this Dirección d) {
		return (Dirección)(((int)d + 2) % 4);
	}
}
public enum Dirección
{
	Norte = 0,
	Este = 1,
	Sur = 2,
	Oeste = 3
}
