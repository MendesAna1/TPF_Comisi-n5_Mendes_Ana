using System;
using System.Collections.Generic;

namespace DeepSpace
{
	public class ArbolGeneral<T>
	{
		
		private T dato;
		private List<ArbolGeneral<T>> hijos = new List<ArbolGeneral<T>>();

		public ArbolGeneral(T dato) {
			this.dato = dato;
		}
	
		public T getDatoRaiz() {
			return this.dato;
		}
	
		public List<ArbolGeneral<T>> getHijos() {
			return hijos;
		}
	
		public void agregarHijo(ArbolGeneral<T> hijo) {
			this.getHijos().Add(hijo);
		}
	
		public void eliminarHijo(ArbolGeneral<T> hijo) {
			this.getHijos().Remove(hijo);
		}
	
		public bool esHoja() {
			return this.getHijos().Count == 0;
		}
	
		public int altura() {
			return 0;
		}

		public bool esVacio()
		{
			return this.dato == null;
		}


		public int nivel(T dato) {
			return 0;
		}

		public string porNiveles()
		{
            // Se inicializa la cadena que almacenará los resultados
            string cadena = "";
            // Se crea una cola para el recorrido por niveles
            Cola<ArbolGeneral<T>> c = new Cola<ArbolGeneral<T>>();
			ArbolGeneral<T> arbolAux;
            // Se agrega el árbol actual a la cola si no es una hoja
            if (!this.esHoja())
            {
				c.encolar(this);
                // Se inicia el bucle mientras la cola no esté vacía
                while (!c.esVacia())
				{
                    // Se desencola un nodo del frente de la cola
                    arbolAux = c.desencolar();
                    // Se agrega el dato del nodo actual a la cadena
                    cadena += "[" + arbolAux.getDatoRaiz() + "]" + " ";
                    // Se encolan los hijos del nodo actual
                    foreach (var hijos in arbolAux.getHijos())
					{
						c.encolar(hijos);
					}
				}
                // Se retorna la cadena eliminando los primeros cuatro caracteres (espacios y corchetes)
                return cadena.Remove(0, 4);
			}
			else
                // Si el árbol es una hoja, se devuelve un mensaje indicando que no hay descendientes
                return "No hay descendientes";
		}
	}
}