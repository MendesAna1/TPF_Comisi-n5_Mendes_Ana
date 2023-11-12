
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{
        //CALCULAR MOVIMIENTO CALCULA Y RETORNA MOVIMIENTO SEGÚN EL ESTADO DEL JUEGO. SE RECIBE EL ESTADO
        // DEL JUEGO EN EL PARÁMETRO ARBOL. CADA NODO DEL ÁRBOL CONTIENE COMO DATO UN PLANETA.
		private static Movimiento mov;
		private static int j = 0, x = 0;
		private static List<ArbolGeneral<Planeta>> lista = new List<ArbolGeneral<Planeta>>();
        public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
        {
            Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>(); 
            ArbolGeneral<Planeta> arbolAux;
            c.encolar(arbol); 

            while (!c.esVacia()) 
            {
                arbolAux = c.desencolar(); 
				if (arbolAux.getDatoRaiz().EsPlanetaDeLaIA()) 
				{
                  
					lista = RecorridoHijosBot(arbolAux, new List<ArbolGeneral<Planeta>>());
                    while (j < lista.Count - 1) 
                    {
						if (!lista[j + 1].getDatoRaiz().EsPlanetaDeLaIA())
                        {
							if (!lista[j + 1].getDatoRaiz().EsPlanetaDeLaIA() && lista[j + 1].esHoja())
                            {
                      
                                mov = new Movimiento(lista[j].getDatoRaiz(), lista[j + 1].getDatoRaiz());
                                j += 1;
                                return mov;
                            }
                            mov = new Movimiento(lista[0].getDatoRaiz(), lista[j + 1].getDatoRaiz());
                            j += 1;
                            return mov;
                        }
                    }
                    break;
                }
                else
                { 
                    if (arbolAux.getHijos().Count != 0)
                        foreach (var hijo in arbolAux.getHijos())
                            c.encolar(hijo);
                }
            }
            //Se ejecuta bucle secundario para intentar encontrar MOVIMIENTO, si no se encuentra se retorna NULL
			int t = (lista.Count - 1) - x;
			while(t > 0)
            {
				if(lista[t].esHoja())
                {
					mov = new Movimiento(lista[t].getDatoRaiz(), lista[t - 1].getDatoRaiz());
					x += 1;
					return mov;
                }
				mov = new Movimiento(lista[t].getDatoRaiz(), lista[0].getDatoRaiz());
				x += 1;
				return mov;
            }
			return null;
        }

        //Recorrido en profundidad de los hijos de un árbol de planetas y construye una lista de árboles
        private List<ArbolGeneral<Planeta>> RecorridoHijosBot(ArbolGeneral<Planeta> arbol, List<ArbolGeneral<Planeta>> lista)
		{
			List<ArbolGeneral<Planeta>> listaAux = null;
			lista.Add(arbol);
           
			if (arbol.esHoja())
				return lista;
			else
			{
				foreach (var item in arbol.getHijos())
					listaAux = RecorridoHijosBot(item, lista);
               
				if (listaAux != null)
					return listaAux;
			}
          
			return null;
		}



        //CONSULTA1: CALCULAR Y RETORNAR UN TEXTO CON DISTANCIA DEL CAMINO ENTRE PLANETAD EL BOT Y RAIZ
        public String Consulta1(ArbolGeneral<Planeta> arbol)
        {
            Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
            ArbolGeneral<Planeta> arbolAux;
            int contNiv = 0, nivel = 0;
            c.encolar(arbol);
            c.encolar(null);
            while (!c.esVacia())
            {
                arbolAux = c.desencolar();
                if (arbolAux == null)
                {
                    contNiv++;
                    if (!c.esVacia())
                        c.encolar(null);
                }
                if (arbolAux != null && arbolAux.getDatoRaiz().EsPlanetaDeLaIA())
                    nivel = contNiv;
                else
                {
                    if (arbolAux != null && arbolAux.getHijos().Count != 0)
                    {
                        foreach (var hijo in arbolAux.getHijos())
                            c.encolar(hijo);
                    }
                }
            }
            // Se retorna un mensaje indicando la distancia entre el planeta del Bot y la raíz
            return $"EXISTE ENTRE PLANETA DEL BOT Y RAÍZ UNA DISTANCIA DE: {nivel.ToString()}";
        }

        //CONSULTA2: RETORNA TEXTO CON LISTADO DE PLANETAS EN TODOS LOS DESCENDIENTES DEL NODO QUE CONTIENE
        // AL PLANETA DEL BOT
        public String Consulta2(ArbolGeneral<Planeta> arbol)
        {
            Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
            ArbolGeneral<Planeta> arbolAux = null;
            string cadena = " ";
            c.encolar(arbol);
            c.encolar(null);
            while (!c.esVacia())
            {
                arbolAux = c.desencolar();
                if (arbolAux == null)
                {
                    if (!c.esVacia())
                        c.encolar(null);
                }
            
                if (arbolAux != null && arbolAux.getDatoRaiz().EsPlanetaDeLaIA())
                {
                    cadena = arbolAux.porNiveles();
                    break;
                }
                else
                {
                   
                    if (arbolAux != null && arbolAux.getHijos().Count != 0)
                    {
                        foreach (var hijo in arbolAux.getHijos())
                            c.encolar(hijo);
                    }
                }
            }
            // Se retorna un mensaje que incluye el planeta del Bot y la lista de descendientes
            return "\nPLANETA DEL BOT: " + arbolAux.getDatoRaiz() + "\n" +
                    " DESCENDIENTES DEL BOT: " + cadena;
        }

       //CONSULTA3: CALCULA Y RETORNA LA POBLACIÓN TOTAL Y PROMEDIO POR CADA NIVEL DEL ÁRBOL
        public String Consulta3(ArbolGeneral<Planeta> arbol)
        {
            Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
            ArbolGeneral<Planeta> arbolAux;
            int contNiv = 0, cantNod = 0, contNodosNivel = 0;
            string cadena = "\n\n\nPOBLACIÓN POR NIVELES: \n";
            c.encolar(arbol);
            c.encolar(null);
            while (!c.esVacia())
            {
                arbolAux = c.desencolar();
                if (arbolAux == null)
                {
                    contNiv++;
                    contNodosNivel += cantNod;
                    cadena += " NIVEL: " + contNiv + " POBLACIÓN: " + cantNod + "\n";
                    if (!c.esVacia())
                        c.encolar(null);
                    cantNod = 0;
                }
                if (arbolAux != null && arbolAux.getHijos().Count == 0)
                {
                    cantNod++;
                }
                if (arbolAux != null && arbolAux.getHijos().Count != 0)
                {
                    cantNod++;
                    foreach (var hijo in arbolAux.getHijos())
                        c.encolar(hijo);
                }
            }
            // Se calcula el promedio de población por nivel
            double prom = contNodosNivel / contNiv;
            // Se retorna la cadena que contiene la información de población por nivel, población total y promedio
            return cadena += "\n POBLACIÓN TOTAL DEL ÁRBOL: " + contNodosNivel + "\n"
                          + " PROMEDIO DE POBLACIÓN POR NIVEL: " + prom;
        }

    }
}
