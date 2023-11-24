using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    /**
     * Clase Cubeta, modifica datos de las cubetas y se encarga de 
     * insertar registros en las cubetas
     */
    public class Cubeta
    {
        ///Direccion de archivo
        public long Dir;
        ///Numero de registros actuales
        public int NoRegistrosAct;
        ///Arreglo de registros (direcciones de archivos)
        public long[] Registros;
        ///Direccion a siguiente cubeta
        public long dirSigCub;
        ///Numero de registros admitidos por la cubeta
        public int nr;

        /**
         * Constructor del objeto Cubeta
         * Recibe el Numero de registros admitidos por las cubetas
         * Asigna valores iniciales a cada uno de los atributos
         */
        public Cubeta(int NR)
        {
            Dir = -1;
            NoRegistrosAct = 0;
            Registros = new long[NR];
            nr = NR;
            for(int i = 0; i < NR; i++)
            {
                Registros[i] = -1;
            }
            dirSigCub = -1;
        }

        /**
         * Asigna una direccion de archivo al objeto en el atributo Dir
         * Recibe un long (Direccion)
         */
        public void setDir(long drd)
        {
            Dir = drd;
        }

        /**
         * Regresa el atributo Dir (Direccion en el archivo)
         */
        public long getdir()
        {
            return Dir;
        }

        /**
         * Aumenta en 1 el atrubuto del Numero de registros actuales
         */
        public void AumNR()
        {
            NoRegistrosAct++;
        }

        /**
         * Regresa el numero de registros actuales
         */
        public long getRegAct()
        {
            return NoRegistrosAct;
        }

        /**
         * Regresa el arreglo de los registros
         * Es un arreglo de direcciones de archivo
         */
        public long[] getArr()
        {
            return Registros;
        }

        /**
         * Regresa el atributo de la direccion a la siguiente cubeta (long)
         */
        public long getsigCub()
        {
            return dirSigCub;
        }

        /**
         * Asigna una direccion de archivo a el atributo de direcciona siguiente cubeta
         * Recibe una direccion de archivo (long)
         */
        public void setsigCub(long dsc)
        {
            dirSigCub = dsc;
        }


        /**
         * Inserta una direccion de archivo a el arreglo de registros
         * Recibe una direccion de archivo (long)
         * Asigna la direccion a el arreglo de registros en la posicion adelante del numero de registros actuales
         */
        public void insertaReg(long al)
        {
            Registros[NoRegistrosAct] = al;
        }

        /**
         * Asigna un entero a la variable de numero de registros actuales
         * Recibe un entero
         */
        public void setNRA(int n)
        {
            NoRegistrosAct = n;
        }

        /**
         * Elimina un registro contenido en un determinado indice del arreglo de registros
         * Recibe un entero (indice del arreglo)
         * Busca dentro del arreglo lo que esta en ese indice y recorre los valores si los hay mas adelante
         * Reduce el numero de registros actuales
         */
        public void eliminaRegistro(int i)
        {
            for (int j = i; j < Registros.Length; j++)
            {
                if(j==nr-1)
                {
                    Registros[j] = -1;
                }else
                {
                    Registros[j] = Registros[j + 1];
                    Registros[j+1] = -1;
                }
            }
            NoRegistrosAct--;
        }

        /**
         * Revisa si el objeto es una cubeta vacia
         * Ve si el atributo de numero de registros actuales es 0
         * Regresa una variable booleana
         */
        public bool cubetaVa()
        {
            if(NoRegistrosAct==0)
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}
