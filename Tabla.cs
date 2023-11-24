using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    /**
     * Clase Tabla, solo existe un objeto tabla en el programa
     * Guarda direcciones de archivo de las cubetas guardadas en la tabla
     */
    public class Tabla
    {
        ///Numero de cajones en la tabla
        public int NoCajones;
        ///Numero de registros por cubeta
        public int NoRegistrosCub;
        ///Apuntador a cubetas vacias
        public long PtrCV;
        ///Arreglo de los cajones, guarda direcciones de archivos
        public long[] Cajones;

        /**
         * Constructor del Objeto Tabla
         * Existe una y solo una dentro del programa
         * Recibe el numero de Cajones y el numero de registros admitidos por cubeta
         * Asigna valores iniciales a todos los atributos
         */
        public Tabla(int NC, int NRC)
        {
            NoCajones = NC;
            NoRegistrosCub = NRC;
            PtrCV = -1;
            Cajones = new long[NC];
            for(int i = 0; i < NC; i++)
            {
                Cajones[i] = -1;
            }
        }

        /**
         * Regresa el valor actual del Numero de cajones
         */
        public int getNoCaj()
        {
            return NoCajones;
        }

        /**
         * Funcion Hash de la tabla
         * Se usa la funcion Hash Cuadrado
         * Recibe una clave, la eleva al cuadrado y toma los dos digitos centrales
         * Regresa los numeros centrales en una variable de tipo entero
         */
        public int Hash(long Cl)
        {
            long clv = Cl*Cl;
            string s = clv.ToString();
            char c1 = s[(s.Length/2)-1];
            char c2 = s[s.Length/2];
            s = c1.ToString();
            s += c2.ToString();
            int cll = int.Parse(s);
            //cll++;
            return cll;
        }

        /**
         * Obtiene una direccion del arreglo de cajones
         * Recibe un entero
         * Regresa la direccion guardada en el arreglo de Cajones guardada en el indice del entero recibido
         */
        public long getCajon(int cj)
        {
            return Cajones[cj];
        }

        /**
         * Asigna una direccion a un indice del arreglo de cajones
         * Recibe un entero (indice) y un Long (direccion)
         */
        public void setCajon(int cj, long df)
        {
            Cajones[cj] = df;
        }

        /**
         * Regresa la direccion actual guardada en el atrubuto de apuntador a cubetas vacias
         */
        public long getAPCV()
        {
            return PtrCV;
        }

        /**
         * Regresa el numero de registros admitidos en las cubetas
         */
        public int getNoRegCub()
        {
            return NoRegistrosCub;
        }

        /**
         * Asigna una direccion al atrubuto de cubetas vacias
         * Recibe un long (Direccion)
         */
        public void setCV(long dd)
        {
            PtrCV = dd;
        }

    }
}
